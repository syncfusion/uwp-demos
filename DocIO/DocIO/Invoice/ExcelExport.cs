using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Notifications;
using Windows.UI.Popups;

namespace Invoice
{
    public class ExportToExcel
    {
        public async void GenerateReport(IList<InvoiceItem> items, BillingInformation billInfo)
        {
            Assembly executingAssembly = typeof(MainPage).GetTypeInfo().Assembly;

            Stream inputStream = executingAssembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Invoice.Assets.InvoiceTemplate.xlsx");

            ExcelEngine excelEngine = new ExcelEngine();
            IWorkbook book = await excelEngine.Excel.Workbooks.OpenAsync(inputStream);
            inputStream.Dispose();

            //Create Template Marker Processor
            ITemplateMarkersProcessor marker = book.CreateTemplateMarkersProcessor();
            //Binding the business object with the marker.
            marker.AddVariable("InvoiceItem", items);
            marker.AddVariable("BillInfo", billInfo);
            marker.AddVariable("Company", billInfo);
            //Applies the marker.
            marker.ApplyMarkers(UnknownVariableAction.Skip);
            StorageFile storageFile = null;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                savePicker.SuggestedFileName = "Invoice";

                savePicker.FileTypeChoices.Add("Invoice", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                storageFile = await local.CreateFileAsync("SpreadsheetSample.xlsx", CreationCollisionOption.ReplaceExisting);
            }
            if (storageFile != null)
            {
                IWorksheet sheet = book.Worksheets[0];
                sheet["G1"].ColumnWidth = 10;
                //XlsIO saves the file Asynchronously
                await book.SaveAsAsync(storageFile);
                book.Close();
                excelEngine.Dispose();
                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");

                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new UICommand("No");
                msgDialog.Commands.Add(noCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the saved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(storageFile);
                }
            }

        }
    }
}
