using Common;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.OfficeChart;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EssentialDocIO
{
    public sealed partial class PieChartDemo : SampleLayout
    {

        public PieChartDemo()
        {
            InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                PieChart.Margin = new Thickness(10, 10, 10, 10);
                WinRTText2.Visibility = Visibility.Collapsed;
            }
        }
        public override void Dispose()
        {
            base.Dispose();

            DisposeTextBlock(TextBlock1);
            TextBlock1 = null;
            DisposeTextBlock(TextBlock2);
            TextBlock2 = null;
            DisposeTextBlock(TextBlock3);
            TextBlock3 = null;
            DisposeTextBlock(TextBlock4);
            TextBlock4 = null;
            DisposeTextBlock(TextBlock5);
            TextBlock5 = null;
            DisposeTextBlock(WinRTText1);
            WinRTText1 = null;
            DisposeTextBlock(WinRTText2);
            WinRTText2 = null;

            Button1.Click -= Button_Click_1;
            DisposeButton(Button1);
            Button1 = null;

        }
       
        public void DisposeTextBlock(TextBlock textBlock)
        {
            textBlock.ClearValue(TextBlock.FontFamilyProperty);
            textBlock.ClearValue(TextBlock.FontSizeProperty);
            textBlock.ClearValue(TextBlock.TextProperty);
            textBlock.ClearValue(TextBlock.TextWrappingProperty);
            textBlock.ClearValue(TextBlock.ForegroundProperty);
        }
        public void DisposeButton(Button button)
        {
            button.ClearValue(Button.FontFamilyProperty);
            button.ClearValue(Button.FontSizeProperty);
            button.ClearValue(Button.PaddingProperty);
            button.ClearValue(Button.ForegroundProperty);
            button.ClearValue(Button.BackgroundProperty);
            button.ClearValue(Button.ContentProperty);
            button.ClearValue(Button.HeightProperty);
            button.ClearValue(Button.WidthProperty);
        }
        private  void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Assembly execAssem = typeof(Common.MainPage).GetTypeInfo().Assembly;
            //A new document is created.
            WordDocument document = new WordDocument();
            //Add new section to the Word document
            IWSection section = document.AddSection();
            //Set page margins of the section
            section.PageSetup.Margins.All = 72;
            //Add new paragraph to the section
            IWParagraph paragraph = section.AddParagraph();
            //Apply heading style to the title paragraph
            paragraph.ApplyStyle(BuiltinStyle.Heading1);
            //Apply center alignment to the paragraph
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            //Append text to the paragraph
            paragraph.AppendText("Northwind Management Report").CharacterFormat.TextColor = Syncfusion.DocIO.DLS.Color.FromArgb(46, 116, 181);
            //Add new paragraph
            paragraph = section.AddParagraph();
            //Get chart data from xml file
            List<ProductDetail> Products = LoadXMLData();
            //Create and Append chart to the paragraph
            WChart pieChart = document.LastParagraph.AppendChart(446, 270);
            //Set chart data
            pieChart.ChartType = OfficeChartType.Pie;
            pieChart.ChartTitle = "Best Selling Products";
            pieChart.ChartTitleArea.FontName = "Calibri (Body)";
            pieChart.ChartTitleArea.Size = 14;
            for (int i = 0; i < Products.Count; i++)
            {
                ProductDetail product = Products[i];
                pieChart.ChartData.SetValue(i + 2, 1, product.ProductName);
                pieChart.ChartData.SetValue(i + 2, 2, product.Sum);
            }
            //Create a new chart series with the name “Sales”
            IOfficeChartSerie pieSeries = pieChart.Series.Add("Sales");
            pieSeries.Values = pieChart.ChartData[2, 2, 11, 2];
            //Setting data label
            pieSeries.DataPoints.DefaultDataPoint.DataLabels.IsPercentage = true;
            pieSeries.DataPoints.DefaultDataPoint.DataLabels.Position = OfficeDataLabelPosition.Outside;
            //Setting background color
            pieChart.ChartArea.Fill.ForeColor = Syncfusion.Drawing.Color.FromArgb(242, 242, 242);
            pieChart.PlotArea.Fill.ForeColor = Syncfusion.Drawing.Color.FromArgb(242, 242, 242);
            pieChart.ChartArea.Border.LinePattern = OfficeChartLinePattern.None;
            pieChart.PrimaryCategoryAxis.CategoryLabels = pieChart.ChartData[2, 1, 11, 1];
            #region Saving Document
            MemoryStream ms = new MemoryStream();
            SaveDocx(document);
            #endregion
        }
        private async void SaveDocx(WordDocument doc)
        {
            StorageFile stgFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as 
                savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".docx" });
                // Default file name if the user does not type one in or select a file to replace 
                savePicker.SuggestedFileName = "Sample";
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("WordDocument.docx", CreationCollisionOption.ReplaceExisting);
            }
            if (stgFile != null)
            {
                MemoryStream stream = new MemoryStream();
                await doc.SaveAsync(stream, FormatType.Docx);
                doc.Close();
                stream.Position = 0;
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stgFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();

                //Save as Docx Format

                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");
                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new UICommand("No");
                msgDialog.Commands.Add(noCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the retrieved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(stgFile);
                }
            }
        }
        private List<ProductDetail> LoadXMLData()
        {
            XDocument productXml;
            List<ProductDetail> Products = new List<ProductDetail>();
            ProductDetail productDetails;
            Assembly assembly = typeof (PieChartDemo).GetTypeInfo().Assembly;
            Stream productXMLStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Products.xml");
            productXml = XDocument.Load(productXMLStream);
            IEnumerable<XElement> pc = from p in productXml.Descendants("Product") select p;
            string serailNo = string.Empty;
            string productName = string.Empty;
            string sum = string.Empty;
            foreach (XElement dt in pc)
            {

                foreach (XElement el in dt.Descendants())
                {
                    var xElement = dt.Element(el.Name);
                    if (xElement != null)
                    {
                        string value = xElement.Value;
                        string elementName = el.Name.ToString();
                        switch (elementName)
                        {
                            case "SNO":
                                serailNo = value;
                                break;
                            case "ProductName":
                                productName = value;
                                break;
                            case "Sum":
                                sum = value;
                                break;
                        }
                    }
                }
                productDetails = new ProductDetail(int.Parse(serailNo), productName, decimal.Parse(sum));
                Products.Add(productDetails);
            }
            return Products;
        }
    }

    public class ProductDetail
    {
        #region fields

        private int m_serialNo;
        private string m_productName;
        private decimal m_sum;

        #endregion

        #region properties

        public int SNO
        {
            get { return m_serialNo; }
            set { m_serialNo = value; }
        }

        public string ProductName
        {
            get { return m_productName; }
            set { m_productName = value; }
        }

        public decimal Sum
        {
            get { return m_sum; }
            set { m_sum = value; }
        }

        #endregion

        #region Constructor

        public ProductDetail(int serialNumber, string productName, decimal sum)
        {
            SNO = serialNumber;
            ProductName = productName;
            Sum = Math.Round(sum, 3);
        }

        #endregion
    }
}
