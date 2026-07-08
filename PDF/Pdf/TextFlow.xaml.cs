#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Pdf;
using Syncfusion.Pdf.Lists;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Drawing;
using Windows.Storage.Pickers;
using Windows.Storage;
using Common;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI;
using System.Reflection;
using Syncfusion.Pdf.Interactive;
using Windows.UI.Popups;
using System.Text;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TextFlow : UserControl
    {
        public TextFlow()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10, 10, 10, 10);
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
            }
        }
        #region Fields
        PdfDocument doc=null;
        PdfPage page=null;
        PdfFont font = null;

        System.Drawing.Color gray = System.Drawing.Color.FromArgb(255, 77, 77, 77);
        System.Drawing.Color black = System.Drawing.Color.FromArgb(255, 0, 0, 0);
        System.Drawing.Color white = System.Drawing.Color.FromArgb(255, 255, 255, 255);
        System.Drawing.Color violet = System.Drawing.Color.FromArgb(255, 151, 108, 174);
        #endregion

        private async void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            //Create a new instance of PdfDocument class.
            doc = new PdfDocument();

            //Add a new page to the document.
            page = doc.Pages.Add();

            //Read the text from the text file
            Stream txtStream = typeof(TextFlow).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Manual.txt");

         

            StreamReader reader = new StreamReader(txtStream, System.Text.Encoding.ASCII);
            string text = reader.ReadToEnd();
            reader.Dispose();

            //Set the font
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);         

            //Set the format for the text
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Justify, PdfVerticalAlignment.Top);
            format.ParagraphIndent = 15f;

            //Create a text element 
            PdfTextElement element = new PdfTextElement(text, font);
            element.Brush = PdfBrushes.Black;
            element.StringFormat = format;
            element.Font = font;

            //Set the properties to paginate the text.
            PdfLayoutFormat layoutFormat = new PdfLayoutFormat();
            layoutFormat.Break = PdfLayoutBreakType.FitPage;
            layoutFormat.Layout = PdfLayoutType.Paginate;

            RectangleF bounds = new RectangleF(new PointF(10, 10), new SizeF(page.Graphics.ClientSize.Width - 20, page.Graphics.ClientSize.Height - 10));

            // Stamps the document with text.
            if (chbStamp.IsChecked.Value)
            {
                // Set font.
                PdfFont stampFont = new PdfStandardFont(PdfFontFamily.Helvetica, 60, PdfFontStyle.Bold);
                PdfPageTemplateElement stamp = new PdfPageTemplateElement(new SizeF(500, 500));
                stamp.Background = true;
                stamp.Graphics.SetTransparency(0.25f);
                stamp.Graphics.RotateTransform(-45);
                stamp.Graphics.DrawString("DEMO", stampFont, PdfBrushes.Blue, new PointF(-150, 400), format);
                doc.Template.Stamps.Add(stamp);
            }
            else
                //Raise begin page event to draw the graphic elements for each page.
                element.BeginPageLayout += new BeginPageLayoutEventHandler(BeginPageLayout);

            element.EndPageLayout += new EndPageLayoutEventHandler(EndPageLayout);

            // Draw the text element.
            element.Draw(page, bounds, layoutFormat);        

            MemoryStream stream = new MemoryStream();
            await doc.SaveAsync(stream);
            doc.Close(true);
            Save(stream, "TextFlow.pdf");
        }


        /// <summary>
        /// Draw graphic elements in the page.
        /// </summary>
        private void BeginPageLayout(object sender, BeginPageLayoutEventArgs e)
        {
            int index = e.Page.Section.Pages.IndexOf(e.Page);
            float offset = 50;

            PdfSolidBrush brush = new PdfSolidBrush(new PdfColor(System.Drawing.Color.FromArgb(255, 173, 216, 230)));

            // Ellipse for odd numbered pages.
            if (index % 2 == 0)
            {
                RectangleF bounds = e.Bounds;
                e.Page.Graphics.DrawEllipse(brush, bounds.Width / 2 - offset, bounds.Height / 2 - offset, 2 * offset, 2 * offset);
            }
            // Rectangle for even numbered pages.
            else
            {
                RectangleF bounds = e.Bounds;
                e.Page.Graphics.DrawRectangle(brush, bounds.Width / 2 - offset, bounds.Height / 2 - offset, 2 * offset, 2 * offset);
            }
        }

        /// <summary>
        /// Draw border for each page in the document.
        /// </summary>
        private void EndPageLayout(object sender, EndPageLayoutEventArgs e)
        {
            PdfPage page = e.Result.Page;
            page.Graphics.DrawRectangle(PdfPens.Black, new RectangleF(PointF.Empty, page.Graphics.ClientSize));
        }

        async void Save(Stream stream, string filename)
        {

            stream.Position = 0;
            StorageFile stFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.DefaultFileExtension = ".pdf";
                savePicker.SuggestedFileName = "Sample";
                savePicker.FileTypeChoices.Add("Adobe PDF Document", new List<string>() { ".pdf" });
                stFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stFile = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            }

            if (stFile != null)
            {
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();
                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File created.");
                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new UICommand("No");
                msgDialog.Commands.Add(noCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the retrieved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(stFile);
                }
            }


        }
    }
}
