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
    public class NorthWind
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

    }

    internal sealed class DataClass
    {
        public static IEnumerable<NorthWind> GetProducts()
        {
            Stream xmlStream = typeof(NorthWindReport).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.GDBDMultiHeader.XML");

            using (StreamReader reader = new StreamReader(xmlStream, true))
            {
                string data = reader.ReadToEnd();
                data = data.Replace("xmlns=\"http://www.tempuri.org/DataSet1.xsd\"", "");
                return XElement.Parse(data)
                    .Elements("Customers")
                    .Select(c => new NorthWind
                    {
                        CustomerID = c.Element("CustomerID").Value,
                        CompanyName = c.Element("CompanyName").Value,
                        ContactName = c.Element("ContactName").Value,
                        ContactTitle = c.Element("ContactTitle").Value,
                        Address = c.Element("Address").Value,
                        City = c.Element("City").Value,
                        Phone = c.Element("Phone").Value,

                    });
            }
        }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NorthWindReport : UserControl
    {
        public NorthWindReport()
        {
            this.InitializeComponent();
            Array styleArray = Enum.GetValues(typeof(PdfLightTableBuiltinStyle));
            foreach (PdfLightTableBuiltinStyle style in styleArray)
            {
                this.lightTablecomboBoxstyle.Items.Add(style);
            }
            this.lightTablecomboBoxstyle.SelectedIndex = 26;          
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10.0, 0, 0, 0);
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
            }
        }
        PdfLightTable table = null;
        private async void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            # region Field Definitions
            IEnumerable<NorthWind> products = DataClass.GetProducts();
            Stream fontStream = typeof(MainPage).GetTypeInfo().Assembly.GetManifestResourceStream("SampleBrowser.Pdf.Assets.verdana.ttf");

            #endregion

            string styleName = this.lightTablecomboBoxstyle.SelectedItem.ToString();

            PdfDocument document = new PdfDocument();          

            PdfPage page = document.Pages.Add();

            #region PdfLightTable
            table = new PdfLightTable();

            table.DataSource = products;                

            PdfLightTableBuiltinStyle style = ConvertToPdfLightTableBuiltinStyle(styleName);

            PdfLightTableBuiltinStyleSettings setting = new PdfLightTableBuiltinStyleSettings();
            setting.ApplyStyleForHeaderRow =true;
            setting.ApplyStyleForBandedRows = true;           

            table.ApplyBuiltinStyle(style, setting);

            table.Draw(page, PointF.Empty);

            #endregion         

            MemoryStream stream = new MemoryStream();
            await document.SaveAsync(stream);
            document.Close(true);
            Save1(stream, "NorthWind.pdf");


        }
        async void Save1(Stream stream, string filename)
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
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                disposedValue = true;
            }
        }

        #region Help method
        private PdfLightTableBuiltinStyle ConvertToPdfLightTableBuiltinStyle(string styleName)
        {
            PdfLightTableBuiltinStyle value = (PdfLightTableBuiltinStyle)Enum.Parse(typeof(PdfLightTableBuiltinStyle), styleName);
            return value;
        }
        #endregion
    }
}
