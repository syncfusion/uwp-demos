using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using System.Reflection;
using System.Globalization;

namespace Invoice
{
    class PdfExport
    {
        public PdfExport()
        {
        }

        /// <summary>
        /// Creates PDF
        /// </summary>
        public async void CreatePDF(IList<InvoiceItem> dataSource, BillingInformation billInfo, double totalDue)
        {
            PdfDocument document = new PdfDocument();
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 50;
            PdfPage page = document.Pages.Add();
            PdfGraphics g = page.Graphics;
            PdfTextElement element = new PdfTextElement(@"Syncfusion Software 
2501 Aerial Center Parkway 
Suite 200 Morrisville, NC 27560 USA 
Tel +1 888.936.8638 Fax +1 919.573.0306");
            element.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
            element.Brush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            PdfLayoutResult result = element.Draw(page, new RectangleF(0, 0, page.Graphics.ClientSize.Width / 2, 200));

            Stream imgStream = typeof(MainPage).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Invoice.Assets.SyncfusionLogo.jpg");

            PdfImage img = PdfImage.FromStream(imgStream);
            page.Graphics.DrawImage(img, new RectangleF(g.ClientSize.Width - 200, result.Bounds.Y, 190, 45));
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            g.DrawRectangle(new PdfSolidBrush(new PdfColor(34, 83, 142)), new RectangleF(0, result.Bounds.Bottom + 40, g.ClientSize.Width, 30));
            element = new PdfTextElement("INVOICE " + billInfo.InvoiceNumber.ToString(), subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 48));
            string currentDate = "DATE " + billInfo.Date.ToString("d");
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            g.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(g.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));

            element = new PdfTextElement("BILL TO ", new PdfStandardFont(PdfFontFamily.TimesRoman, 12));
            element.Brush = new PdfSolidBrush(new PdfColor(34, 83, 142));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));

            g.DrawLine(new PdfPen(new PdfColor(34, 83, 142), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(g.ClientSize.Width, result.Bounds.Bottom + 3));

            element = new PdfTextElement(billInfo.Name, new PdfStandardFont(PdfFontFamily.TimesRoman, 11));
            element.Brush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 5, g.ClientSize.Width / 2, 100));

            element = new PdfTextElement(billInfo.Address, new PdfStandardFont(PdfFontFamily.TimesRoman, 11));
            element.Brush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, g.ClientSize.Width / 2, 100));
            string[] headers = new string[] { "Item", "Quantity", "Rate", "Taxes", "Amount" };
            PdfGrid grid = new PdfGrid();
            grid.Columns.Add(headers.Length);
            //Adding headers in to the grid
            grid.Headers.Add(1);
            PdfGridRow headerRow = grid.Headers[0];
            int count = 0;
            foreach (string columnName in headers)
            {
                headerRow.Cells[count].Value = columnName;
                count++;
            }
            //Adding rows into the grid
            foreach (var item in dataSource)
            {
                PdfGridRow row = grid.Rows.Add();
                row.Cells[0].Value = item.ItemName;
                row.Cells[1].Value = item.Quantity.ToString();
                row.Cells[2].Value = "$" + item.Rate.ToString("#,###.00", CultureInfo.InvariantCulture);
                row.Cells[3].Value = "$" + item.Taxes.ToString("#,###.00", CultureInfo.InvariantCulture);
                row.Cells[4].Value = "$" + item.TotalAmount.ToString("#,###.00", CultureInfo.InvariantCulture);

            }

            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = grid.Headers[0];

            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(34, 83, 142));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(34, 83, 142));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f, PdfFontStyle.Regular);

            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            }

            header.Cells[0].Value = "ITEM";
            header.Cells[1].Value = "QUANTITY";
            header.Cells[2].Value = "RATE";
            header.Cells[3].Value = "TAXES";
            header.Cells[4].Value = "AMOUNT";
            header.ApplyStyle(headerStyle);
            grid.Columns[0].Width = 180;
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(34, 83, 142), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 11f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            foreach (PdfGridRow row in grid.Rows)
            {
                row.ApplyStyle(cellStyle);
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    PdfGridCell cell = row.Cells[i];
                    if (i == 0)
                        cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else
                        cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);

                    if (i > 1)
                    {
                        if (cell.Value.ToString().Contains("$"))
                        {
                            cell.Value = cell.Value.ToString();
                        }
                        else
                        {
                            if (cell.Value is double)
                                cell.Value = "$" + ((double)cell.Value).ToString("#,###.00", CultureInfo.InvariantCulture);
                            else
                                cell.Value = "$" + cell.Value.ToString();
                        }
                    }
                }
            }

            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;

            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(g.ClientSize.Width, g.ClientSize.Height - 100)), layoutFormat);
            float pos = 0.0f;
            for (int i = 0; i < grid.Columns.Count - 1; i++)
                pos += grid.Columns[i].Width;

            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f, PdfFontStyle.Bold);
            gridResult.Page.Graphics.DrawString("TOTAL DUE", font, new PdfSolidBrush(new PdfColor(34, 83, 142)), new RectangleF(new PointF(pos, gridResult.Bounds.Bottom + 10), new SizeF(grid.Columns[3].Width - pos, 20)), new PdfStringFormat(PdfTextAlignment.Right));
            gridResult.Page.Graphics.DrawString("Thank you for your business!", new PdfStandardFont(PdfFontFamily.TimesRoman, 12), new PdfSolidBrush(new PdfColor(0, 0, 0)), new PointF(pos - 210, gridResult.Bounds.Bottom + 60));
            pos += grid.Columns[4].Width;
            gridResult.Page.Graphics.DrawString("$" + totalDue.ToString("#,###.00", CultureInfo.InvariantCulture), font, new PdfSolidBrush(new PdfColor(0, 0, 0)), new RectangleF(pos, gridResult.Bounds.Bottom + 10, grid.Columns[4].Width - pos, 20), new PdfStringFormat(PdfTextAlignment.Right));
            StorageFile stFile = null;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.DefaultFileExtension = ".pdf";
                savePicker.SuggestedFileName = "Invoice";
                savePicker.FileTypeChoices.Add("Adobe PDF Document", new List<string>() { ".pdf" });
                stFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stFile = await local.CreateFileAsync("Invoice.pdf", CreationCollisionOption.ReplaceExisting);
            }

            if (stFile != null)
            {
                Stream stream = await stFile.OpenStreamForWriteAsync();
                await document.SaveAsync(stream);
                stream.Flush();
                stream.Dispose();
                document.Close(true);

                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");

                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new UICommand("No");
                msgDialog.Commands.Add(noCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the saved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(stFile);
                }

            }

        }


    }
}
