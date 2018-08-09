#region Copyright Syncfusion Inc. 2001-2017.
// Copyright Syncfusion Inc. 2001-2017. All rights reserved.
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
using Syncfusion.UI.Xaml.Grid;
using Windows.Storage;
using Windows.Storage.Pickers;
using Syncfusion.XlsIO;
using Windows.UI.Popups;
using System.Reflection;
//using Syncfusion.XlsIO.WinRT.Samples;
using System.Xml.Linq;
using Common;
using System.ComponentModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImportBusinessObject : SampleLayout
    {
        public ImportBusinessObject()
        {
            this.InitializeComponent();
			if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
			{
                this.btnImportData.HorizontalAlignment = HorizontalAlignment.Center;
                this.grdMain.Padding = new Thickness(10, 0, 0, 0);
				this.textBox1.Text = "Please click the 'Export To Excel' button to view the excel file generated using Essential XlsIO.";
			}
			else
            this.textBox1.Text = "Please click the 'Export To Excel' button to view the excel file generated using Essential XlsIO in the selected format.";
            var datacontext = new ImportAndExport();
            this.DataContext = datacontext;
            this.grdViewExport.ItemsSource = datacontext.CustomersInfo;
        }
                
        private async void btnExportData_Click(object sender, RoutedEventArgs e)
        {
            #region Workbook initialization
            System.Diagnostics.Stopwatch watcher = new System.Diagnostics.Stopwatch();
            watcher.Start();
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            //Step 1 : Instantiate the spreadsheet creation engine.
            ExcelEngine excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            IApplication application = excelEngine.Excel;

            //Set the Default Version as Excel2016
            application.DefaultVersion = ExcelVersion.Excel2016;

            //A new workbook is created.[Equivalent to creating a new workbook in MS Excel]
            //The new workbook will have 3 worksheets
            IWorkbook workbook = application.Workbooks.Create(1);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            #endregion

            sheet.ImportData((List<BusinessObject>)this.grdViewExport.ItemsSource, 4, 1, true);

             #region Define Styles
            IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
            IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");

            pageHeader.Font.RGBColor = Windows.UI.Color.FromArgb(0, 83, 141, 213);
            pageHeader.Font.FontName = "Calibri";
            pageHeader.Font.Size = 18;
            pageHeader.Font.Bold = true;
            pageHeader.HorizontalAlignment = ExcelHAlign.HAlignCenter;
            pageHeader.VerticalAlignment = ExcelVAlign.VAlignCenter;

            tableHeader.Font.Color = ExcelKnownColors.White;
            tableHeader.Font.Bold = true;
            tableHeader.Font.Size = 11;
            tableHeader.Font.FontName = "Calibri";
            tableHeader.HorizontalAlignment = ExcelHAlign.HAlignCenter;
            tableHeader.VerticalAlignment = ExcelVAlign.VAlignCenter;
            tableHeader.Color = Windows.UI.Color.FromArgb(0, 118, 147, 60);
            tableHeader.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
            tableHeader.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
            tableHeader.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
            tableHeader.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
            #endregion

            #region Apply Styles
            // Apply style for header
            sheet["A1:D1"].Merge();
            sheet["A1"].Text = "Yearly Sales Report";
            sheet["A1"].CellStyle = pageHeader;

            sheet["A2:D2"].Merge();
            sheet["A2"].Text = "Namewise Sales Comparison Report";
            sheet["A2"].CellStyle = pageHeader;
            sheet["A2"].CellStyle.Font.Bold = false;
            sheet["A2"].CellStyle.Font.Size = 16;

            sheet["A3:A4"].Merge();
            sheet["D3:D4"].Merge();
            sheet["B3:C3"].Merge();
            sheet["B3"].Text = "Sales";
            sheet["A3:D4"].CellStyle = tableHeader;

            sheet["A3"].Text = "Sales Person";
            sheet["B4"].Text = "January - June";
            sheet["C4"].Text = "July - December";
            sheet["D3"].Text = "Change(%)";

            sheet.UsedRange.AutofitColumns();
            sheet.Columns[0].ColumnWidth = 24;
            sheet.Columns[1].ColumnWidth = 21;
            sheet.Columns[2].ColumnWidth = 21;
            sheet.Columns[3].ColumnWidth = 16;
            #endregion

            #region Save the Workbook
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "BusinessObjects";
                if (workbook.Version == ExcelVersion.Excel97to2003)
                    savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xls" });
                else
                    savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                if (workbook.Version == ExcelVersion.Excel97to2003)
                    storageFile = await local.CreateFileAsync("BusinessObjects.xls", CreationCollisionOption.ReplaceExisting);
                else
                    storageFile = await local.CreateFileAsync("BusinessObjects.xlsx", CreationCollisionOption.ReplaceExisting);
            }
           
            if (storageFile != null)
            {
                //Saving the workbook
                await workbook.SaveAsAsync(storageFile);
                workbook.Close();
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
            else
            {
                workbook.Close();
                excelEngine.Dispose();
            }
            #endregion
        }
      

        #region Dispose
        public override void Dispose()
        {
            base.Dispose();

            UnlinkChildrens(grdMain);
            DisposeTextBlock(textBox1);
            textBox1 = null;
            DisposeTextBlock(textBox2);
            textBox2 = null;
            
            DisposeButton(btnImportData);
            btnImportData = null;
            
            DisposeGrid(grdMain);
            grdMain = null;
        }

        private void DisposeTextBlock(TextBlock textBlock)
        {
            if (textBlock == null)
                return;
            textBlock.ClearValue(TextBlock.FontFamilyProperty);
            textBlock.ClearValue(TextBlock.FontSizeProperty);
            textBlock.ClearValue(TextBlock.TextProperty);
            textBlock.ClearValue(TextBlock.TextWrappingProperty);
            textBlock.ClearValue(TextBlock.ForegroundProperty);
        }

        private void DisposeButton(Button button)
        {
            if (button == null)
                return;
            button.ClearValue(Button.FontFamilyProperty);
            button.ClearValue(Button.FontSizeProperty);
            button.ClearValue(Button.PaddingProperty);
            button.ClearValue(Button.ForegroundProperty);
            button.ClearValue(Button.BackgroundProperty);
            button.ClearValue(Button.ContentProperty);
            button.ClearValue(Button.HeightProperty);
            button.ClearValue(Button.WidthProperty);
            button.Click -= btnImportData_Click;
        }

        private void DisposeRadioButton(RadioButton radioButton)
        {
            if (radioButton == null)
                return;
            radioButton.ClearValue(RadioButton.GroupNameProperty);
            radioButton.ClearValue(RadioButton.ContentProperty);
            radioButton.ClearValue(RadioButton.FontFamilyProperty);
            radioButton.ClearValue(RadioButton.FontSizeProperty);
            radioButton.ClearValue(RadioButton.ForegroundProperty);
            radioButton.ClearValue(RadioButton.WidthProperty);
            radioButton.ClearValue(RadioButton.IsCheckedProperty);
        }

        private void DisposeStackPanel(StackPanel stackPanel)
        {
            if (stackPanel == null)
                return;
            stackPanel.ClearValue(StackPanel.OrientationProperty);
            stackPanel.ClearValue(StackPanel.HorizontalAlignmentProperty);
        }

        private void DisposeGrid(Grid grid)
        {
            if (grid == null)
                return;
            grid.ClearValue(Grid.BackgroundProperty);
            grid.ClearValue(Grid.MarginProperty);
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
        }

        void UnlinkChildrens(UIElement element)
        {
            if (element == null)
                return;
            if (element is Panel)
            {
                for (int i = 0; i < (element as Panel).Children.Count; i++)
                {
                    UIElement childElement = (element as Panel).Children[i];
                    UnlinkChildrens(childElement);
                    (element as Panel).Children.Remove(childElement);
                    i--;
                }
            }
            else if (element is ItemsControl)
            {
                for (int j = 0; j < (element as ItemsControl).Items.Count; j++)
                {
                    UIElement childElement = ((element as ItemsControl).Items[j] as UIElement);
                    if (childElement == null)
                    {
                        //(element as ItemsControl).Items.RemoveAt(j);
                        //j--;
                    }
                    else
                    {
                        UnlinkChildrens(childElement);
                        (element as ItemsControl).Items.Remove(childElement);
                        j--;
                    }
                }
            }
            else if (element is ContentControl)
            {
                UnlinkChildrens((element as ContentControl).Content as UIElement);
                (element as ContentControl).Content = null;
            }
            else if (element is UserControl)
            {
                UnlinkChildrens((element as UserControl).Content as UIElement);
                (element as UserControl).Content = null;
            }
        }
        #endregion

        #region View the Input Template
        private async void btnInput_Click(object sender, RoutedEventArgs e)
        {
            #region Workbook initialization
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;

            application.DefaultVersion = ExcelVersion.Excel2013;

            Assembly assembly = typeof(TemplateMarker).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.ExportSales.xlsx";
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
                savePicker.SuggestedFileName = "ExportSales";
                savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                storageFile = await local.CreateFileAsync("ExportSales.xlsx", CreationCollisionOption.ReplaceExisting);
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
        #endregion

        private async void btnImportData_Click(object sender, RoutedEventArgs e)
        {
            #region Workbook initialization
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;

            application.DefaultVersion = ExcelVersion.Excel2013;
            // For Export Data
            Assembly assembly = typeof(TemplateMarker).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.ExportSales.xlsx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            IWorkbook workbook = await application.Workbooks.OpenAsync(fileStream);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet worksheet = workbook.Worksheets[0];

            List<BusinessObject> businessObjects = worksheet.ExportData<BusinessObject>(1, 1, 41, 4);

            this.grdViewExport.ItemsSource = businessObjects;
            btnExportData.IsEnabled = true;
            #endregion
        }
    }
    public class BusinessObject
    {
        public string SalesPerson { get; set; }
        public int SalesJanJune { get; set; }
        public int SalesJulyDec { get; set; }
        public int Change { get; set; }
    }


    /// <summary>
    /// This class represents the DataBoundViewModel
    /// </summary>
    public class ImportAndExport : INotifyPropertyChanged, IDisposable
    {

        public ImportAndExport()
        {
            CustomersInfo = new List<BusinessObject>();
            foreach (int i in Enumerable.Range(1, 20))
            {
                CustomersInfo.Add(new BusinessObject());
            }
        }
        private List<BusinessObject> customersInfo = null;

        /// <summary>
        /// Gets or sets the OrderListDetails.
        /// </summary>
        /// <value>The OrderListDetails.</value>
        ///         #region ItemsSource
        public List<BusinessObject> CustomersInfo
        {
            get { return customersInfo; }
            set { this.customersInfo = value;}
        }    

        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (CustomersInfo != null)
            {
                CustomersInfo.Clear();
            }
        }
    }
}
