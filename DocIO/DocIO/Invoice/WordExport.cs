using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Notifications;
using Windows.UI.Popups;

namespace Invoice
{
    /// <summary>
    /// Helper Class to export invoice details to a Word document
    /// </summary>
    class ExportWord
    {
        /// <summary>
        /// Export the UI data to Word document.
        /// </summary>
        /// <param name="invoiceItems">The InvoiceItems.</param>
        /// <param name="billInfo">The BillingInformation.</param>
        /// <param name="totalDue">The TotalDue.</param>
        public async void CreateWord(IList<InvoiceItem> invoiceItems, BillingInformation billInfo, double totalDue)
        {
            //Load Template document stream

            Stream inputStream = typeof(MainPage).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Invoice.Assets.Template.docx");

            //Create instance
            WordDocument document = new WordDocument();
            //Open Template document
            await document.OpenAsync(inputStream, FormatType.Word2013);
            //Dispose input stream
            inputStream.Dispose();

            //Set Clear Fields to false
            document.MailMerge.ClearFields = false;
            //Create Mail Merge Data Table
            MailMergeDataTable mailMergeDataTable = new MailMergeDataTable("Invoice", invoiceItems);
            //Executes mail merge using the data generated in the app.
            document.MailMerge.ExecuteGroup(mailMergeDataTable);

            //Mail Merge Billing information
            string[] fieldNames = { "Name", "Address", "Date", "InvoiceNumber", "DueDate", "TotalDue" };
            string[] fieldValues = { billInfo.Name, billInfo.Address, billInfo.Date.ToString("d"), billInfo.InvoiceNumber, billInfo.DueDate.ToString("d"), totalDue.ToString("#,###.00", CultureInfo.InvariantCulture) };
            document.MailMerge.Execute(fieldNames, fieldValues);

            StorageFile stgFile = null;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as 
                savePicker.FileTypeChoices.Add("Invoice", new List<string>() { ".docx" });
                // Default file name if the user does not type one in or select a file to replace 
                savePicker.SuggestedFileName = "Invoice";
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("Invoice.docx", CreationCollisionOption.ReplaceExisting);
            }
            if (stgFile != null)
            {
                //Save as Docx format
                await document.SaveAsync(stgFile, FormatType.Word2013);
                //Close instance
                document.Close();

                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");

                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new UICommand("No");
                msgDialog.Commands.Add(noCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the saved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(stgFile);
                }
            }
        }
    }
}
