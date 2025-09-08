#region Copyright Syncfusion Inc. 2001 - 2019
// Copyright Syncfusion Inc. 2001 - 2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Pdf;
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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Conformance : UserControl
    {
        public Conformance()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10, 10, 10, 10);
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
            }
        }

        private async void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument document = null;
            if (this.PdfConformance.SelectionBoxItem.ToString() == "PDF/A-1a")
            {
                // Create a new instance of PdfDocument class with PDF/A standard.
                document = new PdfDocument(PdfConformanceLevel.Pdf_A1A);
            }
            else if (this.PdfConformance.SelectionBoxItem.ToString() == "PDF/A-1b")
            {
                document = new PdfDocument(PdfConformanceLevel.Pdf_A1B);
            }
            else if (this.PdfConformance.SelectionBoxItem.ToString() == "PDF/A-2a")
            {
                document = new PdfDocument(PdfConformanceLevel.Pdf_A2A);
            }
            else if (this.PdfConformance.SelectionBoxItem.ToString() == "PDF/A-2b")
            {
                document = new PdfDocument(PdfConformanceLevel.Pdf_A2B);
            }
            else if (this.PdfConformance.SelectionBoxItem.ToString() == "PDF/A-2u")
            {
                document = new PdfDocument(PdfConformanceLevel.Pdf_A2U);
            }
            else
            {
                if (this.PdfConformance.SelectionBoxItem.ToString() == "PDF/A-3a")
                {
                    document = new PdfDocument(PdfConformanceLevel.Pdf_A3A);
                }
                else if (this.PdfConformance.SelectionBoxItem.ToString() == "PDF/A-3b")
                {
                    document = new PdfDocument(PdfConformanceLevel.Pdf_A3B);
                }
                else if (this.PdfConformance.SelectionBoxItem.ToString() == "PDF/A-3u")
                {
                    document = new PdfDocument(PdfConformanceLevel.Pdf_A3U);
                }
                Stream imgStream = typeof(Conformance).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Products.xml");

                PdfAttachment attachment = new PdfAttachment("Products.xml", imgStream);
                attachment.Relationship = PdfAttachmentRelationship.Alternative;
                attachment.ModificationDate = DateTime.Now;

                attachment.Description = "PDF_A";

                attachment.MimeType = "application/xml";

                document.Attachments.Add(attachment);
            }
            //Add a page to the document.
            PdfPage page = document.Pages.Add();

            //Create Pdf graphics for the page
            PdfGraphics g = page.Graphics;

            Stream jpegImage = typeof(Conformance).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.AdventureCycle.jpg");

            //Drawing a jpeg image 
            PdfImage image = new PdfBitmap(jpegImage);
            g.DrawImage(image, new RectangleF(150, 30, 200, 100));

            //Text to draw in PDF
            string text = "Adventure Works Cycles, the fictitious company on which the AdventureWorks sample databases are based," +
                               " is a large, multinational manufacturing company. The company manufactures and sells metal and composite bicycles to North American, " +
                               "European and Asian commercial markets. While its base operation is located in Bothell, Washington with 290 employees, several regional" +
                               " sales teams are located throughout their market base";

            ////Set the formats for the text
            //PdfStringFormat format = new PdfStringFormat();
            //format.Alignment = PdfTextAlignment.Justify;
            //format.LineAlignment = PdfVerticalAlignment.Top;
            //format.ParagraphIndent = 35f;

            //Create a text element 
            PdfTextElement element = new PdfTextElement(text);
            element.Brush = PdfBrushes.Black;
            // element.StringFormat = format;
            //Set the font
            Stream fontStream = typeof(Conformance).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.arial.ttf");
            element.Font = new PdfTrueTypeFont(fontStream, 14);

            //Set the properties to paginate the text.
            PdfLayoutFormat layoutFormat = new PdfLayoutFormat();
            layoutFormat.Break = PdfLayoutBreakType.FitPage;
            layoutFormat.Layout = PdfLayoutType.Paginate;

            RectangleF bounds = new RectangleF(0, 150, page.GetClientSize().Width, page.GetClientSize().Height);

            //Draw the text element with the properties and formats set.
            element.Draw(page, bounds, layoutFormat);
            //Save and close the document.
            MemoryStream stream = new MemoryStream();
            await document.SaveAsync(stream);
            document.Close(true);
            Save(stream, "Sample.pdf");
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
