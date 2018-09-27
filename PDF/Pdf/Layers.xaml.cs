#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
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
    public sealed partial class Layers : UserControl
    {
        public Layers()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10, 10, 10, 10);
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
            }
        }
        #region Fields
        PdfDocument doc;
        PdfPage page;
        System.Drawing.Color gray = System.Drawing.Color.FromArgb(255, 77, 77, 77);
        System.Drawing.Color black = System.Drawing.Color.FromArgb(255, 0, 0, 0);
        System.Drawing.Color white = System.Drawing.Color.FromArgb(255, 255, 255, 255);
        System.Drawing.Color violet = System.Drawing.Color.FromArgb(255, 151, 108, 174);
        #endregion

        #region Color
        PdfColor greenColor = new PdfColor(System.Drawing.Color.FromArgb(255, 0, 128, 0));
        PdfColor redColor = new PdfColor(System.Drawing.Color.FromArgb(255, 255, 0, 0));
        PdfColor yellowColor = new PdfColor(System.Drawing.Color.FromArgb(255, 255, 255, 0));
        PdfColor blackColor = new PdfColor(System.Drawing.Color.FromArgb(255, 0, 0, 0));
        PdfColor blueColor = new PdfColor(System.Drawing.Color.FromArgb(255, 0, 0, 255));
        #endregion

        private async void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            //Create a new instance of PdfDocument class.
            doc = new PdfDocument();

            // Set page size.
            doc.PageSettings = new PdfPageSettings(new SizeF(350, 300));

            // Add a new page to the document.
            page = doc.Pages.Add();

            page.Graphics.DrawString("Layers", new PdfStandardFont(PdfFontFamily.Helvetica, 16), PdfBrushes.DarkBlue, new PointF(150, 10));

            //Add first layer
            PdfPageLayer layer = page.Layers.Add("Layer1");

            PdfGraphics graphics = layer.Graphics;
            graphics.TranslateTransform(100, 60);

            //Draw Arc 
            PdfPen pen = new PdfPen(redColor, 50);
            RectangleF rect = new RectangleF(0, 0, 50, 50);
            graphics.DrawArc(pen, rect, 360, 360);

            pen = new PdfPen(blueColor, 30);
            graphics.DrawArc(pen, 0, 0, 50, 50, 360, 360);

            pen = new PdfPen(yellowColor, 20);
            graphics.DrawArc(pen, rect, 360, 360);

            pen = new PdfPen(greenColor, 10);
            graphics.DrawArc(pen, 0, 0, 50, 50, 360, 360);

            //Add another layer on the page
            layer = page.Layers.Add("Layer2");

            graphics = layer.Graphics;
            graphics.TranslateTransform(100, 180);         

            //Draw another set of elements
            pen = new PdfPen(redColor, 50);
            graphics.DrawArc(pen, rect, 360, 360);
            pen = new PdfPen(blueColor, 30);
            graphics.DrawArc(pen, 0, 0, 50, 50, 360, 360);
            pen = new PdfPen(yellowColor, 20);
            graphics.DrawArc(pen, rect, 360, 360);
            pen = new PdfPen(greenColor, 10);
            graphics.DrawArc(pen, 0, 0, 50, 50, 360, 360);

            //Add third layer.
            layer = page.Layers.Add("Layer3");
            graphics = layer.Graphics;
            graphics.TranslateTransform(160, 120);

            //Draw another set of elements.
            pen = new PdfPen(redColor, 50);
            graphics.DrawArc(pen, rect, -60, 60);
            pen = new PdfPen(blueColor, 30);
            graphics.DrawArc(pen, 0, 0, 50, 50, -60, 60);
            pen = new PdfPen(yellowColor, 20);
            graphics.DrawArc(pen, rect, -60, 60);
            pen = new PdfPen(greenColor, 10);
            graphics.DrawArc(pen, 0, 0, 50, 50, -60, 60);

            // Save and close the document.
            MemoryStream stream = new MemoryStream();
            await doc.SaveAsync(stream);
            doc.Close(true);
            Save(stream, "Layers.pdf");
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
