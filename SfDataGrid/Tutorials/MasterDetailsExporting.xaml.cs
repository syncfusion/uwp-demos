using Common;
using MasterDetailsViewDemo;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DataGrid
{
    public sealed partial class MasterDetailsExporting : SampleLayout
    {
        public MasterDetailsExporting()
        {
            this.InitializeComponent();
            var datacontext = new DetailsViewViewModel();
            this.DataContext = datacontext;
            this.dataGrid.ItemsSource = datacontext.OrdersDetails;
            this.dataGrid.Loaded += OnSfDataGridLoaded;            
        }

        /// <summary>
        /// Occurs when the SfDataGrid is laid out, rendered, and ready for interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSfDataGridLoaded(object sender, RoutedEventArgs e)
        {           
            this.dataGrid.ExpandAllDetailsView();
        }
       
        /// <summary>
        /// Method is export to selected records in SfDataGrid to Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnExportSelectedExcel(object sender, RoutedEventArgs e)
        {
            ExcelEngine excelEngine = new ExcelEngine();
            var workBook = excelEngine.Excel.Workbooks.Create();
            workBook.Version = ExcelVersion.Excel2010;
            workBook.Worksheets.Create();
            if ((bool)this.customizeSelectedRow.IsChecked)
            {
                this.dataGrid.ExportToExcel(this.dataGrid.SelectedItems, new ExcelExportingOptions()
                {
                    CellsExportingEventHandler = CellExportingHandler,
                    ExportingEventHandler = CustomizedexportingHandler
                }, workBook.Worksheets[0]);
            }
            else
            {
                this.dataGrid.ExportToExcel(this.dataGrid.SelectedItems, new ExcelExportingOptions()
                {
                    CellsExportingEventHandler = CellExportingHandler,
                    ExportingEventHandler = exportingHandler
                }, workBook.Worksheets[0]);
            }

            workBook = excelEngine.Excel.Workbooks[0];
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.Desktop,
                SuggestedFileName = "Sample"
            };

            if (workBook.Version == ExcelVersion.Excel97to2003)
            {
                savePicker.FileTypeChoices.Add("Excel File (.xls)", new List<string>() { ".xls" });
            }
            else
            {
                savePicker.FileTypeChoices.Add("Excel File (.xlsx)", new List<string>() { ".xlsx" });
            }

            var storageFile = await savePicker.PickSaveFileAsync();

            if (storageFile != null)
            {
                await workBook.SaveAsAsync(storageFile);

                var msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");

                var yesCmd = new UICommand("Yes");
                var noCmd = new UICommand("No");
                msgDialog.Commands.Add(yesCmd);
                msgDialog.Commands.Add(noCmd);
                var cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the saved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(storageFile);
                }
            }
            excelEngine.Dispose();
        }

        /// <summary>
        /// Method is export SfDataGrid to Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnExcelExportDataGrid(object sender, RoutedEventArgs e)
        {
            ExcelExportingOptions exportingOptions = new ExcelExportingOptions();
            exportingOptions.ExportingEventHandler = exportingHandler;
            if ((bool)this.customizeColumns.IsChecked)
                exportingOptions.CellsExportingEventHandler = CustomizedCellExportingHandler;
            else
                exportingOptions.CellsExportingEventHandler = CellExportingHandler;
            var excelEngine = this.dataGrid.ExportToExcel(this.dataGrid.View, exportingOptions);
            var workBook = excelEngine.Excel.Workbooks[0];
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.Desktop,
                SuggestedFileName = "Sample"
            };

            if (workBook.Version == ExcelVersion.Excel97to2003)
            {
                savePicker.FileTypeChoices.Add("Excel File (.xls)", new List<string>() { ".xls" });
            }
            else
            {
                savePicker.FileTypeChoices.Add("Excel File (.xlsx)", new List<string>() { ".xlsx" });
            }

            var storageFile = await savePicker.PickSaveFileAsync();

            if (storageFile != null)
            {
                await workBook.SaveAsAsync(storageFile);

                var msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");

                var yesCmd = new UICommand("Yes");
                var noCmd = new UICommand("No");
                msgDialog.Commands.Add(yesCmd);
                msgDialog.Commands.Add(noCmd);
                var cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the saved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(storageFile);
                }
            }
            excelEngine.Dispose();
        }

        /// <summary>
        /// CellEvent handler for Exporting the SfDataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void CellExportingHandler(object sender, GridCellExcelExportingEventArgs args)
        {
            args.Range.CellStyle.Font.FontName = "Segoe UI";
        }
        
        public void CustomizedCellExportingHandler(object sender, GridCellExcelExportingEventArgs args)
        {
            if (args.ColumnName == "Discount")
            {
                args.Range.CellStyle.ColorIndex = ExcelKnownColors.Violet;
            }
        }
        
        public void exportingHandler(object sender, GridExcelExportingEventArgs e)
        {
            if (e.CellType == ExportCellType.HeaderCell)
            {
                if (e.Level == 0)
                    e.CellStyle.BackGroundBrush = new SolidColorBrush(Color.FromArgb(255, 155, 194, 230));
                else
                    e.CellStyle.BackGroundBrush = new SolidColorBrush(Color.FromArgb(255, 146, 208, 80));

                e.CellStyle.ForeGroundBrush = new SolidColorBrush(Colors.White);
                e.CellStyle.FontInfo.Bold = true;
            }

            e.CellStyle.FontInfo.FontName = "Segoe UI";
            e.Handled = true;
        }
        
        public void CustomizedexportingHandler(object sender, GridExcelExportingEventArgs e)
        {
            if ((e.CellType == ExportCellType.RecordCell))
            {
                e.CellStyle.BackGroundBrush = new SolidColorBrush(Colors.Violet);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.dataGrid.Loaded -= OnSfDataGridLoaded;            
            this.dataGrid.Dispose();
            this.dataGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
