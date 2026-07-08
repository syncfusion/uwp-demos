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
using System.Text;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HeadersAndFooters : UserControl
    {
        private bool m_paginateStart = true;
        RectangleF bounds;
        private RectangleF m_columnBounds;
        public HeadersAndFooters()
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
            //Create a PDF document
            PdfDocument doc = new PdfDocument();
            PdfColor BlackColor = new PdfColor(System.Drawing.Color.FromArgb(255, 0, 0, 0));

            //Add a page
            PdfPage page = doc.Pages.Add();
            RectangleF rect = new RectangleF(0, 0, page.GetClientSize().Width, 50);

            PdfSolidBrush brush = new PdfSolidBrush(BlackColor);
            PdfPen pen = new PdfPen(BlackColor, 1f);

            //Create font
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 11.5f);

          
            PdfStandardFont heading = new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold);

            //Adding Header
            this.AddHeader(doc, "Syncfusion Essential PDF", "Header and Footer Demo");

            //Adding Footer
            this.AddFooter(doc, "@Copyright 2015");

            //Set formats
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Justify;
            Stream txtStream = typeof(HeadersAndFooters).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Essential studio.txt");
            StreamReader reader = new StreamReader(txtStream, System.Text.Encoding.ASCII);
            string text = reader.ReadToEnd();
            reader.Dispose();           
            RectangleF column = new RectangleF(0, 20, page.Graphics.ClientSize.Width / 2f - 10f, page.Graphics.ClientSize.Height);
            bounds = column;
            m_columnBounds = column;

            //Create text element to control the text flow
            PdfTextElement element = new PdfTextElement(text, font);
            element.Brush = new PdfSolidBrush(BlackColor);
            element.StringFormat = format;
            PdfLayoutFormat layoutFormat = new PdfLayoutFormat();
            layoutFormat.Break = PdfLayoutBreakType.FitPage;
            layoutFormat.Layout = PdfLayoutType.Paginate;

            //Get the remaining text that flows beyond the boundary.
            PdfTextLayoutResult result = element.Draw(page, bounds, layoutFormat);
            txtStream = typeof(HeadersAndFooters).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Essential PDF.txt");
            reader = new StreamReader(txtStream, System.Text.Encoding.ASCII);
            text = reader.ReadToEnd();
            reader.Dispose();

            element = new PdfTextElement(text, font);
            element.Brush = new PdfSolidBrush(BlackColor);
            element.StringFormat = format;

            // Next paragraph flow from last line of the previous paragraph.
            bounds.Y = result.LastLineBounds.Y + 35f;

            //Raise the event when the text flows to next page.
            element.BeginPageLayout += new BeginPageLayoutEventHandler(BeginPageLayout2);

            //Raise the event when the text reaches the end of the page.
            element.EndPageLayout += new EndPageLayoutEventHandler(EndPageLayout2);
            result.Page.Graphics.DrawString("Essential PDF", heading, PdfBrushes.DarkBlue, new PointF(bounds.X, bounds.Y));

            bounds.Y = result.LastLineBounds.Y + 60f;
            result = element.Draw(result.Page, new RectangleF(bounds.X, bounds.Y, bounds.Width, -1), layoutFormat);
            Stream imgStream = typeof(HeadersAndFooters).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Essen PDF.gif");
            PdfImage image = new PdfBitmap(imgStream);         
            bounds.Y = result.LastLineBounds.Y + 20f;
            bounds.X = bounds.Width + 20f;

            result.Page.Graphics.DrawImage(image, new PointF(bounds.X, bounds.Y),new SizeF(image.Width,image.Height-20));
            PointF a = new PointF(bounds.X, bounds.Y + image.Height - 20);
            result.Page.Graphics.DrawString("Essential DocIO", heading, PdfBrushes.DarkBlue, new PointF(bounds.X, bounds.Y + image.Height - 20));
            txtStream = typeof(HeadersAndFooters).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Essential DocIO.txt");           
            reader = new StreamReader(txtStream, Encoding.ASCII);
            text = reader.ReadToEnd();
            reader.Dispose();

            element = new PdfTextElement(text, font);
            element.Brush = new PdfSolidBrush(BlackColor);
            element.StringFormat = format;

            bounds.Y = result.LastLineBounds.Y + image.Height + 20;
            bounds.X = bounds.Width + 20f;


            result = element.Draw(result.Page, new RectangleF(bounds.X, bounds.Y, bounds.Width, -1), layoutFormat);
            imgStream = typeof(HeadersAndFooters).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Essen DocIO.gif");
            image = new PdfBitmap(imgStream);
            bounds.Y = result.LastLineBounds.Y + 20f;
            bounds.X = bounds.Width + 20f;

            result.Page.Graphics.DrawImage(image, new PointF(bounds.X, bounds.Y), new SizeF(image.Width, image.Height - 20));
            txtStream = typeof(HeadersAndFooters).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Essential XlsIO.txt");
            
            reader = new StreamReader(txtStream, Encoding.ASCII);
            text = reader.ReadToEnd();
            reader.Dispose();

            element = new PdfTextElement(text, font);
            element.Brush = new PdfSolidBrush(BlackColor);
            element.StringFormat = format;

            result.Page.Graphics.DrawString("Essential XlsIO", heading, PdfBrushes.DarkBlue, new PointF(bounds.X, bounds.Y + image.Height - 20));

            bounds.Y = result.LastLineBounds.Y + image.Height + 20;
            bounds.X = bounds.Width + 20f;
            result = element.Draw(result.Page, new RectangleF(bounds.X, bounds.Y, bounds.Width, -1), layoutFormat);
            imgStream = typeof(HeadersAndFooters).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Essen XlsIO.gif");
            image = new PdfBitmap(imgStream);
            bounds.Y = result.LastLineBounds.Y + 20f;
            bounds.X = bounds.Width + 20f;

            result.Page.Graphics.DrawImage(image, new PointF(bounds.X, bounds.Y), new SizeF(image.Width, image.Height - 20));
            MemoryStream stream = new MemoryStream();
            await doc.SaveAsync(stream);
            doc.Close(true);
            Save(stream, "HeadersAndFooters.pdf");

        }

        private void EndPageLayout2(object sender, EndPageLayoutEventArgs e)
        {
            EndTextPageLayoutEventArgs args = (EndTextPageLayoutEventArgs)e;
            PdfTextLayoutResult tlr = args.Result;
            RectangleF bounds = tlr.Bounds;
            args.NextPage = tlr.Page;
        }
        private void BeginPageLayout2(object sender, BeginPageLayoutEventArgs e)
        {

            RectangleF bounds = e.Bounds;

            // First column.
            if (!m_paginateStart)
            {
                bounds.X = bounds.Width + 20f;
                bounds.Y = 10f;
            }
            else
            {
                // Second column.
                bounds.X = 0f;
                m_paginateStart = false;
            }

            e.Bounds = bounds;
            m_columnBounds = bounds;
        }

        private void AddHeader(PdfDocument doc, string title, string description)
        {
            RectangleF rect = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);
            PdfColor blueColor = new PdfColor(System.Drawing.Color.FromArgb(255, 0, 0, 255));
            PdfColor GrayColor = new PdfColor(System.Drawing.Color.FromArgb(255, 128, 128, 128));

            //Create page template
            PdfPageTemplateElement header = new PdfPageTemplateElement(rect);
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 24);
            float doubleHeight = font.Height * 2;
            System.Drawing.Color activeColor = System.Drawing.Color.FromArgb(255,44, 71, 120);
            SizeF imageSize = new SizeF(110f, 35f);
            //Locating the logo on the right corner of the Drawing Surface
            PointF imageLocation = new PointF(doc.Pages[0].GetClientSize().Width - imageSize.Width - 20, 5);

            Stream  imgStream = typeof(HeadersAndFooters).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.logo.png");
            PdfImage img = new PdfBitmap(imgStream);

            //Draw the image in the Header.
            header.Graphics.DrawImage(img, imageLocation, imageSize);

            PdfSolidBrush brush = new PdfSolidBrush(activeColor);

            PdfPen pen = new PdfPen(blueColor, 3f);
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold);

            //Set formattings for the text
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Middle;

            //Draw title
            header.Graphics.DrawString(title, font, brush, new RectangleF(0, 0, header.Width, header.Height), format);
            brush = new PdfSolidBrush(GrayColor);
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

            format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Left;
            format.LineAlignment = PdfVerticalAlignment.Bottom;

            //Draw description
            header.Graphics.DrawString(description, font, brush, new RectangleF(0, 0, header.Width, header.Height - 8), format);

            //Draw some lines in the header
            pen = new PdfPen(blueColor, 0.7f);
            header.Graphics.DrawLine(pen, 0, 0, header.Width, 0);
            pen = new PdfPen(blueColor, 2f);
            header.Graphics.DrawLine(pen, 0, 03, header.Width + 3, 03);
            pen = new PdfPen(blueColor, 2f);
            header.Graphics.DrawLine(pen, 0, header.Height - 3, header.Width, header.Height - 3);
            header.Graphics.DrawLine(pen, 0, header.Height, header.Width, header.Height);

            //Add header template at the top.
            doc.Template.Top = header;

        }

        private void AddFooter(PdfDocument doc, string footerText)
        {
            RectangleF rect = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);
            PdfColor blueColor = new PdfColor(System.Drawing.Color.FromArgb(255, 0, 0, 255));
            PdfColor GrayColor = new PdfColor(System.Drawing.Color.FromArgb(255, 128, 128, 128));
            //Create a page template
            PdfPageTemplateElement footer = new PdfPageTemplateElement(rect);
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 8);

            PdfSolidBrush brush = new PdfSolidBrush(GrayColor);

            PdfPen pen = new PdfPen(blueColor, 3f);
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Middle;
            footer.Graphics.DrawString(footerText, font, brush, new RectangleF(0, 18, footer.Width, footer.Height), format);

            format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Right;
            format.LineAlignment = PdfVerticalAlignment.Bottom;

            //Create page number field
            PdfPageNumberField pageNumber = new PdfPageNumberField(font, brush);

            //Create page count field
            PdfPageCountField count = new PdfPageCountField(font, brush);

            PdfCompositeField compositeField = new PdfCompositeField(font, brush, "Page {0} of {1}", pageNumber, count);
            compositeField.Bounds = footer.Bounds;
            compositeField.Draw(footer.Graphics, new PointF(470, 40));

            //Add the footer template at the bottom
            doc.Template.Bottom = footer;

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
