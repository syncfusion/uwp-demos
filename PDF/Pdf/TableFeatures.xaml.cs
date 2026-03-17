#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Reflection;
using Windows.UI;
using Windows.Storage.Pickers;
using Windows.Storage;
//using Common;
using Windows.UI.Popups;
using Common;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    #region Products
    public class Products
    {
        public string Image1 { get; set; }
        public string Description1 { get; set; }
        public string Image2 { get; set; }
        public string Description2 { get; set; }
        public string Image3 { get; set; }
        public string Description3 { get; set; }
    }
    #endregion

    #region Report
    public class Report
    {
        public string Image { get; set; }
        public string Description { get; set; }
    }
    #endregion
    internal sealed class DataProvider
    {
        public static IEnumerable<Products> GetProducts()
        {
            Stream xmlStream = typeof(TableFeatures).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Products.xml");

            using (StreamReader reader = new StreamReader(xmlStream, true))
            {
                return XElement.Parse(reader.ReadToEnd())
                    .Elements("Products")
                    .Select(c => new Products
                    {
                        Image1 = c.Element("Image1").Value,
                        Description1 = c.Element("Description1").Value,
                        Image2 = c.Element("Image2").Value,
                        Description2 = c.Element("Description2").Value,
                        Image3 = (c.Element("Image3") != null) ? c.Element("Image3").Value : "dummy",
                        Description3 = (c.Element("Description3") != null) ? c.Element("Description3").Value : "dummy",
                    });
            }
        }

        public static IEnumerable<Report> GetReport()
        {
            Stream xmlStream = typeof(TableFeatures).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Report.xml"); 

            using (StreamReader reader = new StreamReader(xmlStream, true))
            {
                return XElement.Parse(reader.ReadToEnd())
                    .Elements("Report")
                    .Select(c => new Report
                    {
                        Image = c.Element("Image").Value,
                        Description = c.Element("Description").Value,
                    });
            }
        }
    }
    public sealed partial class TableFeatures : UserControl,IDisposable 
    {
        # region Fields
        PdfPen borderPen;
        PdfPen transparentPen;
        float cellSpacing = 7f;
        float margin = 40f;
        PdfFont smallFont;
        # endregion
        public TableFeatures()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10, 10, 10, 10);
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //}
        PdfLightTable pdfLightTable=null;
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            # region Field Definitions
            IEnumerable<Products> products = DataProvider.GetProducts();
            Stream fontStream = typeof(MainPage).GetTypeInfo().Assembly.GetManifestResourceStream("SampleBrowser.Pdf.Assets.verdana.ttf");

            //PdfTrueTypeFont font = new PdfTrueTypeFont(fontStream, 8f);
            //smallFont = new PdfTrueTypeFont(font, 5f);
            //PdfFont bigFont = new PdfTrueTypeFont(font, 16f);
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);
            smallFont = new PdfStandardFont(font, 5f);
            PdfFont bigFont = new PdfStandardFont(font, 16f);
            PdfBrush orangeBrush = new PdfSolidBrush(new PdfColor(247, 148, 29));
            PdfBrush grayBrush = new PdfSolidBrush(new PdfColor(170, 171, 171));

            borderPen = new PdfPen(new PdfColor(System.Drawing.Color.FromArgb(Colors.DarkGray.A,Colors.DarkGray.R,Colors.DarkGray.G,Colors.DarkGray.B)), .3f);
            borderPen.LineCap = PdfLineCap.Square;
            transparentPen = new PdfPen(new PdfColor(System.Drawing.Color.FromArgb(Colors.Transparent.A,Colors.Transparent.R,Colors.Transparent.G,Colors.Transparent.B)), .3f);
            transparentPen.LineCap = PdfLineCap.Square;
            # endregion

            PdfDocument document = new PdfDocument();
            document.PageSettings.Margins.All = 0;

            # region Footer
            PdfPageTemplateElement footer = new PdfPageTemplateElement(new RectangleF(new PointF(0, document.PageSettings.Height - 40), new SizeF(document.PageSettings.Width, 40)));
            footer.Graphics.DrawRectangle(new PdfSolidBrush(new PdfColor(77, 77, 77)), footer.Bounds);
            footer.Graphics.DrawString("http://www.syncfusion.com", font, grayBrush, new PointF(footer.Width - (footer.Width / 4), 15));
            footer.Graphics.DrawString("Copyright © 2001 - 2015 Syncfusion Inc.", font, grayBrush, new PointF(0, 15));
            document.Template.Bottom = footer;
            # endregion

            PdfPage page = document.Pages.Add();

            page.Graphics.DrawRectangle(orangeBrush, new RectangleF(PointF.Empty, new SizeF(page.Graphics.ClientSize.Width, margin)));
            page.Graphics.DrawString("Essential Studio Reporting Edition", bigFont, PdfBrushes.White, new PointF(10, 10));

            # region PdfLightTable
            pdfLightTable = new PdfLightTable();
            pdfLightTable.DataSource = products;
            pdfLightTable.Style.DefaultStyle.BorderPen = transparentPen;

            for (int i = 0; i < pdfLightTable.Columns.Count; i++)
            {
                if (i % 2 == 0)
                    pdfLightTable.Columns[i].Width = 16.5f;
            }

            pdfLightTable.Style.CellSpacing = cellSpacing;
            pdfLightTable.BeginRowLayout += new BeginRowLayoutEventHandler(pdfLightTable_BeginRowLayout);
            pdfLightTable.BeginCellLayout += new BeginCellLayoutEventHandler(pdfLightTable_BeginCellLayout);
            pdfLightTable.Style.DefaultStyle.Font = font;
            PdfLayoutResult result = pdfLightTable.Draw(page, new RectangleF(new PointF(margin, 70), new SizeF(page.Graphics.ClientSize.Width - (2 * margin), page.Graphics.ClientSize.Height - margin)));

            # endregion

            page.Graphics.DrawString("What You Get with Syncfusion", bigFont, orangeBrush, new PointF(margin, result.Bounds.Bottom + 50));

            # region PdfGrid
            PdfGrid pdfGrid = new PdfGrid();
            IEnumerable<Report> report = DataProvider.GetReport();

            pdfGrid.DataSource = report;
            pdfGrid.Headers.Clear();
            pdfGrid.Columns[0].Width = 80;
            pdfGrid.Style.Font = font;
            pdfGrid.Style.CellSpacing = 15f;

            for (int i = 0; i < pdfGrid.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    PdfGridCell cell = pdfGrid.Rows[i].Cells[0];
                    cell.RowSpan = 2;

                    cell.Style.BackgroundImage = new PdfBitmap(typeof(TableFeatures).GetTypeInfo().Assembly.GetManifestResourceStream(string.Format("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.{0}.jpg", cell.Value.ToString().ToLower())));
                    cell.Value = "";

                    cell = pdfGrid.Rows[i].Cells[1];
                    cell.Style.Font = bigFont;
                }
                for (int j = 0; j < pdfGrid.Columns.Count; j++)
                {
                    pdfGrid.Rows[i].Cells[j].Style.Borders.All = transparentPen;
                }
            }

            PdfGridLayoutFormat gridLayoutFormat = new PdfGridLayoutFormat();
            gridLayoutFormat.Layout = PdfLayoutType.Paginate;

            pdfGrid.Draw(page, new RectangleF(new PointF(margin, result.Bounds.Bottom + 75), new SizeF(page.Graphics.ClientSize.Width - (2 * margin), page.Graphics.ClientSize.Height - margin)), gridLayoutFormat);

            # endregion
            MemoryStream stream = new MemoryStream();
            await document.SaveAsync(stream);
            document.Close(true);
            Save(stream, "TableFeatures.pdf");
           
        }
        #region Helper Methods
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
        #endregion
        # region PdfLightTable Events

        void pdfLightTable_BeginRowLayout(object sender, BeginRowLayoutEventArgs args)
        {
            if (args.RowIndex % 2 == 0)
                args.MinimalHeight = 20;
            else
                args.MinimalHeight = 30;
        }

        void pdfLightTable_BeginCellLayout(object sender, BeginCellLayoutEventArgs args)
        {
            if (args.RowIndex > -1 && args.CellIndex > -1)
            {
                float x = args.Bounds.X;
                float y = args.Bounds.Y;
                float width = args.Bounds.Right;
                float height = args.Bounds.Bottom;

                if (args.Value == "dummy")
                {
                    args.Skip = true;
                    return;
                }

                if (args.CellIndex % 2 == 0 && !string.IsNullOrEmpty(args.Value))
                {
                    args.Skip = true;
                    PdfBitmap img = null;
                    if (args.Value.ToLower() == "xlsio" || args.Value.ToLower() == "docio" || args.Value.ToLower() == "pdf")
                    {
                        string val = args.Value.Substring(0, 1) + args.Value.Substring(1).ToLower();
                        img = new PdfBitmap(typeof(TableFeatures).GetTypeInfo().Assembly.GetManifestResourceStream(string.Format("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.{0}.jpg", val)));
                    }
                    else
                        img = new PdfBitmap(typeof(TableFeatures).GetTypeInfo().Assembly.GetManifestResourceStream(string.Format("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.{0}.jpg", args.Value.ToString().ToLower())));
                    RectangleF rect = args.Bounds;
                    args.Graphics.DrawImage(img, new RectangleF(rect.X + 2, rect.Y + 2, rect.Width - 2, rect.Height - 2));
                }

                if (args.Value.Contains("http"))
                {
                    args.Skip = true;

                    // Create the Text Web Link
                    PdfTextWebLink textLink = new PdfTextWebLink();
                    textLink.Url = args.Value;
                    textLink.Text = "Know more...";
                    textLink.Brush = PdfBrushes.Black;
                    textLink.Font = smallFont;
                    textLink.DrawTextWebLink(args.Graphics, new PointF(args.Bounds.X + 2 * args.Bounds.Width / 3, args.Bounds.Y));
                }

                # region Draw manual borders
                if (args.RowIndex % 3 == 0)//top
                {
                    if (args.CellIndex % 2 == 0)
                        width += cellSpacing;
                    args.Graphics.DrawLine(borderPen, new PointF(x, y), new PointF(width, y));
                }
                else if (args.RowIndex % 3 == 2)//bottom
                {
                    if (args.CellIndex % 2 == 0)
                        width += cellSpacing;
                    args.Graphics.DrawLine(borderPen, new PointF(x, height), new PointF(width, height));
                }

                if (args.CellIndex % 2 == 0)//left
                {
                    if (args.RowIndex % 3 != 2)
                        height += cellSpacing;
                    args.Graphics.DrawLine(borderPen, new PointF(x, y), new PointF(x, height));
                }
                else if (args.CellIndex % 2 != 0)//right
                {
                    if (args.RowIndex % 3 != 2)
                        height += cellSpacing;
                    args.Graphics.DrawLine(borderPen, new PointF(width, y), new PointF(width, height));
                }
                # endregion
            }
        }

        # endregion

        public void Dispose()
        {
            if (pdfLightTable != null)
            {
                pdfLightTable.BeginRowLayout -= new BeginRowLayoutEventHandler(pdfLightTable_BeginRowLayout);
                pdfLightTable.BeginCellLayout -= new BeginCellLayoutEventHandler(pdfLightTable_BeginCellLayout);
            }
        }
    }
}
