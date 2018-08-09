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
using System.Xml.Linq;
using Syncfusion.Pdf.Grid;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{ 
  
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Invoice : UserControl
    {

        string shipName, address, shipCity, shipCountry;
        Double total = 0;   

        public Invoice()
        {
            this.InitializeComponent();
         
            for (int i = 10248; i < 10260; i++)
                this.OrderIdList.Items.Add(i);
            this.OrderIdList.SelectedIndex = 0;

            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10, 10, 10, 10);
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
            }
        }      

        private async void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            total = 0;
            string id = this.OrderIdList.SelectedValue.ToString();
            IEnumerable<CustOrders> products = Orders.GetProducts(id);

            List<CustOrders> list = new List<CustOrders>();
            foreach (CustOrders cust in products)
            {               
                list.Add(cust);
            }            
            var reducedList = list.Select(f => new {f.ProductID,f.ProductName,f.Quantity,f.UnitPrice,f.Discount,f.Price }).ToList();

            IEnumerable<ShipDetails> shipDetails = Orders.GetShipDetails();
            PdfDocument document = new PdfDocument();
            document.PageSettings.Orientation = PdfPageOrientation.Landscape;
            document.PageSettings.Margins.All = 50;
            PdfPage page = document.Pages.Add();
            PdfGraphics g = page.Graphics;
            PdfTextElement element = new PdfTextElement("Northwind Traders\n67, rue des Cinquante Otages,\nElgin,\nUnites States.");
            element.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            PdfLayoutResult result = element.Draw(page, new RectangleF(0, 0, page.Graphics.ClientSize.Width / 2, 200));
            Stream imgStream = typeof(Invoice).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.logo.jpg");

            PdfImage img = new PdfBitmap(imgStream);
            page.Graphics.DrawImage(img, new RectangleF(g.ClientSize.Width - 200, result.Bounds.Y, 190, 45));
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            g.DrawRectangle(new PdfSolidBrush(new PdfColor(126, 151, 173)), new RectangleF(0, result.Bounds.Bottom + 40, g.ClientSize.Width, 30));


            element = new PdfTextElement("INVOICE " + id.ToString(), subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 48));
            string currentDate = "DATE " + DateTime.Now.ToString("MM/dd/yyyy");
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            g.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(g.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));

            element = new PdfTextElement("BILL TO ", new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));

            g.DrawLine(new PdfPen(new PdfColor(126, 151, 173), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(g.ClientSize.Width, result.Bounds.Bottom + 3));

            GetShipDetails(shipDetails);

            element = new PdfTextElement(shipName, new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 5, g.ClientSize.Width / 2, 100));

            element = new PdfTextElement(string.Format("{0}, {1}, {2}", address, shipCity, shipCountry), new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, g.ClientSize.Width / 2, 100));

            PdfGrid grid = new PdfGrid();
            grid.DataSource = reducedList;              


            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = grid.Headers[0];

            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 1)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            }
            header.ApplyStyle(headerStyle);
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
            foreach (PdfGridRow row in grid.Rows)
            {
                row.ApplyStyle(cellStyle);
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    PdfGridCell cell = row.Cells[i];
                    if (i == 1)
                        cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else if (i == 0)
                        cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                    else
                        cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);

                    if (i > 1)
                    {
                        float val = float.MinValue;
                        float.TryParse(cell.Value.ToString(), out val);
                        cell.Value = val.ToString("C");
                    }
                }
            }

            grid.Columns[0].Width = 100;
            grid.Columns[1].Width = 200;

            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;

            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(g.ClientSize.Width, g.ClientSize.Height - 100)), layoutFormat);
            float pos = 0.0f;
            for (int i = 0; i < grid.Columns.Count - 1; i++)
                pos += grid.Columns[i].Width;

            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f);

            GetTotalPrice(products);

            gridResult.Page.Graphics.DrawString("Total Due", font, new PdfSolidBrush(new PdfColor(126, 151, 173)), new RectangleF(new PointF(pos, gridResult.Bounds.Bottom + 20), new SizeF(grid.Columns[3].Width - pos, 20)), new PdfStringFormat(PdfTextAlignment.Right));
            gridResult.Page.Graphics.DrawString("Thank you for your business!", new PdfStandardFont(PdfFontFamily.TimesRoman, 12), new PdfSolidBrush(new PdfColor(89, 89, 93)), new PointF(pos - 55, gridResult.Bounds.Bottom + 60));
            pos += grid.Columns[4].Width;
            gridResult.Page.Graphics.DrawString(total.ToString("C"), font, new PdfSolidBrush(new PdfColor(131, 130, 136)), new RectangleF(new PointF(pos, gridResult.Bounds.Bottom + 20), new SizeF(grid.Columns[4].Width - pos, 20)), new PdfStringFormat(PdfTextAlignment.Right));


            MemoryStream stream = new MemoryStream();
            await document.SaveAsync(stream);
            document.Close(true);
            Save(stream, "Invoice.pdf");
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

     public void GetTotalPrice(IEnumerable<CustOrders> orders)
        {
            foreach (CustOrders s in orders)
            {
                total += s.Price;
            }
        }

        public void GetShipDetails(IEnumerable<ShipDetails> details)
        {
            foreach (ShipDetails s in details)
            {
                shipCity = s.ShipCity;
                shipCountry = s.ShipCountry;
                shipName = s.ShipName;
                address = s.ShipAddress;
            }
        }      
    }
    public class CustOrders
    {      
        public string ProductID { get; set; }
        public string ProductName { get; set; }    
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
        public string Discount { get; set; }
        public Double Price { get; set; }
        public string OrderID { get; set; }

    }
    public class ShipDetails
    {
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }       
    }


    internal sealed class Orders
    {
        public static IEnumerable<CustOrders> GetProducts(string orderId)
        {
            Stream xmlStream = typeof(Invoice).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.CustOrders.xml");

            using (StreamReader reader = new StreamReader(xmlStream, true))
            {
                string data = reader.ReadToEnd();

                data = data.Replace("xmlns=\"http://www.tempuri.org/DataSet1.xsd\"", "");
              
               return  XElement.Parse(data)                    
                    .Elements("Order_x0020_Details")                                                   
                    .Select(c => new CustOrders
                    {
                        ProductID = c.Element("ProductID").Value,
                        ProductName = c.Element("ProductName").Value,
                        Quantity = c.Element("Quantity").Value,
                        UnitPrice = c.Element("UnitPrice").Value,
                        Discount = c.Element("Discount").Value,
                        Price = Convert.ToDouble(c.Element("Quantity").Value) * Convert.ToDouble(c.Element("UnitPrice").Value),                                            
                        OrderID=c.Element("OrderID").Value,
                    }).Where(r => (string)r.OrderID.ToString() == orderId);              
            }
        }

        public static IEnumerable<ShipDetails> GetShipDetails()
        {
            Stream xmlStream = typeof(Invoice).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.CustOrders.xml");

            using (StreamReader reader = new StreamReader(xmlStream, true))
            {
                string data = reader.ReadToEnd();


                data = data.Replace("xmlns=\"http://www.tempuri.org/DataSet1.xsd\"", "");

                return XElement.Parse(data)
                    .Elements("Order_x0020_Details")
                    .Select(c => new ShipDetails
                    {                      
                        ShipName = c.Element("ShipName").Value,
                        ShipAddress = c.Element("ShipAddress").Value,
                        ShipCity = c.Element("ShipCity").Value,
                        ShipCountry = c.Element("ShipCountry").Value,
                    });
            }
        }
    }
}
