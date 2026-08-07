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
using Common;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    public class Adventure
    {
        public string OrderID { get; set; }
        public string CustomerID { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
    }


    internal sealed class Provider
    {
        public static IEnumerable<Adventure> GetProducts()
        {
            Stream xmlStream = typeof(AdventureCycle).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Orders.xml");

            using (StreamReader reader = new StreamReader(xmlStream, true))
            {
                string data = reader.ReadToEnd();
                data = data.Replace("xmlns=\"http://www.tempuri.org/DataSet1.xsd\"", "");
                return XElement.Parse(data)
                    .Elements("Orders")
                    .Select(c => new Adventure
                    {
                        OrderID = c.Element("OrderID").Value,
                        CustomerID = c.Element("CustomerID").Value,
                        ShipName = c.Element("ShipName").Value,
                        ShipAddress = c.Element("ShipAddress").Value,
                        ShipCity = c.Element("ShipCity").Value,
                        ShipCountry = c.Element("ShipCountry").Value,
                    });
            }
        }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdventureCycle : UserControl
    {
        string styleName;
        public AdventureCycle()
        {
            this.InitializeComponent();
            Array styleArray = Enum.GetValues(typeof(PdfGridBuiltinStyle));
            foreach (PdfGridBuiltinStyle style in styleArray)
            {
                this.gridBuiltinstyleComboBox.Items.Add(style);
            }
            this.gridBuiltinstyleComboBox.SelectedIndex = 26;          
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10.0, 0, 0, 0);
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
            }
        }

        PdfGrid grid = null;
        private async void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            #region Field Definitions
            IEnumerable<Adventure> products = Provider.GetProducts();
            Stream fontStream = typeof(MainPage).GetTypeInfo().Assembly.GetManifestResourceStream("SampleBrowser.Pdf.Assets.verdana.ttf");
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);           
            #endregion

            styleName = this.gridBuiltinstyleComboBox.SelectedItem.ToString();

            PdfDocument document = new PdfDocument();          

            PdfPage page = document.Pages.Add();
            
            #region PdfGrid
            grid = new PdfGrid();
            grid.DataSource = products;                 

            PdfGridBuiltinStyle style = ConvertToPdfGridBuiltinStyle(styleName);

            PdfGridBuiltinStyleSettings setting = new PdfGridBuiltinStyleSettings();
            setting.ApplyStyleForHeaderRow = true;
            setting.ApplyStyleForBandedRows = true;           

            grid.Style.CellPadding.All = 2;

            grid.ApplyBuiltinStyle(style, setting);

            PdfGridLayoutFormat gridLayoutFormat = new PdfGridLayoutFormat();
            gridLayoutFormat.Layout = PdfLayoutType.Paginate;
            gridLayoutFormat.Break = PdfLayoutBreakType.FitElement;

            grid.Draw(page, PointF.Empty,gridLayoutFormat);            

            #endregion        

            MemoryStream stream = new MemoryStream();
            await document.SaveAsync(stream);
            document.Close(true);
            SaveFile(stream, "GridBuiltinStyle.pdf");


        }
        async void SaveFile(Stream stream, string filename)
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

      

        #region Help method
        private PdfGridBuiltinStyle ConvertToPdfGridBuiltinStyle(string styleName)
        {
            PdfGridBuiltinStyle value = (PdfGridBuiltinStyle)Enum.Parse(typeof(PdfGridBuiltinStyle), styleName);
            return value;
        }
        #endregion
    }
}
