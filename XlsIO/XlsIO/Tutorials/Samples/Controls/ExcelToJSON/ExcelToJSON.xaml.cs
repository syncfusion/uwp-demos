#region Copyright Syncfusion Inc. 2001-2020.
// Copyright Syncfusion Inc. 2001-2020. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
//using Common;
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
using Windows.Storage;
using Windows.Storage.Pickers;
using Syncfusion.XlsIO;
using Windows.UI.Popups;
using System.Reflection;
using Syncfusion.XlsIO.WinRT.Samples;
using Common;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExcelToJSON : SampleLayout
    {
        /// <summary>
        /// Window constructor
        /// </summary>
        public ExcelToJSON()
        {
            InitializeComponent();

            this.textBox1.Text = "Please click the 'Input Template' button to view the input Excel document and click the 'Convert to JSON' button to view the converted JSON file.";

            string[] findOptions = { "Workbook", "Worksheet", "Range" };

            for (int i = 0; i < findOptions.Length; i++)
            {
                combo1.Items.Add(findOptions[i]);
            }
            combo1.SelectedIndex = 0;
            check1.IsChecked = true;
        }

        private async void InputTemplte(object sender, RoutedEventArgs e)
        {
            #region Workbook initialization
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;

            application.DefaultVersion = ExcelVersion.Excel2013;

            Assembly assembly = typeof(ExcelToJSON).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.ExcelToJSON.xlsx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            IWorkbook workbook = await application.Workbooks.OpenAsync(fileStream);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet worksheet = workbook.Worksheets[0];

            #endregion

            #region Save the Workbook
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "ExcelToJSON";
                savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                storageFile = await local.CreateFileAsync("ExcelToJSON.xlsx", CreationCollisionOption.ReplaceExisting);
            }


            if (storageFile != null)
            {
                //Saving the workbook
                await workbook.SaveAsAsync(storageFile);
                workbook.Close();
                excelEngine.Dispose();

                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been saved successfully.");

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
            else
            {
                workbook.Close();
                excelEngine.Dispose();
            }
            #endregion
        }

        private async void ConvertToJson(object sender, RoutedEventArgs e)
        {
            try
            {
                StorageFile storageFile;
                if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
                {
                    FileSavePicker savePicker = new FileSavePicker();
                    savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                    savePicker.SuggestedFileName = "ExcelToJSON";
                    savePicker.FileTypeChoices.Add("JSON files", new List<string>() { ".json", });
                    storageFile = await savePicker.PickSaveFileAsync();
                }
                else
                {
                    StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                    storageFile = await local.CreateFileAsync("ExcelToJSON.json", CreationCollisionOption.ReplaceExisting);
                }

                if (storageFile == null)
                    return;
                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;

                Assembly assembly = typeof(ExcelToJSON).GetTypeInfo().Assembly;
                string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.ExcelToJSON.xlsx";
                Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
                IWorkbook workbook = await application.Workbooks.OpenAsync(fileStream);

                IWorksheet sheet = workbook.Worksheets[0];
                IRange range = sheet.Range["A2:B10"];

                bool isSchema = check1.IsChecked.Value;

                if (combo1.SelectedIndex == 0)
                   await workbook.SaveAsJsonAsync(storageFile, isSchema);
                else if (combo1.SelectedIndex == 1)
                   await workbook.SaveAsJsonAsync(storageFile, sheet, isSchema);
                else if (combo1.SelectedIndex == 2)
                   await workbook.SaveAsJsonAsync(storageFile, range, isSchema);

                workbook.Close();
                excelEngine.Dispose();

                #region Launching the saved workbook
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
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
