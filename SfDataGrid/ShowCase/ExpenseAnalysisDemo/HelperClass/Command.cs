using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace ExpenseAnalysisDemo
{

    public static class ExportHelper
    {
        public static async void Export(this GridView gridView)
        {
            ExcelEngine excelEngine;
            SfDataGrid dataGrid = FindDescendantChildByType(gridView, typeof(SfDataGrid)) as SfDataGrid;
            if (dataGrid == null) return;

            excelEngine = dataGrid.ExportCollectionToExcel(dataGrid.View, ExcelVersion.Excel2007, exportingHandler, cellsExportingHandler, true);

            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            savePicker.SuggestedFileName = "Sample";
            IWorkbook workBook = excelEngine.Excel.Workbooks[0];
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
                workBook.ActiveSheet.SetRowHeightInPixels(1, 42);
                await workBook.SaveAsAsync(storageFile);
                workBook.Close();
                excelEngine.Dispose();

                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");

                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the saved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(storageFile);
                }
            }
        }

        public static async void Export(this GridView gridView, StorageFile storageFile)
        {
            SfDataGrid dataGrid = FindDescendantChildByType(gridView, typeof(SfDataGrid)) as SfDataGrid;
            if (dataGrid == null) return;

            var excelEngine = dataGrid.ExportCollectionToExcel(dataGrid.View, ExcelVersion.Excel2007,
                                                                   exportingHandler, cellsExportingHandler, false);
            var workBook = excelEngine.Excel.Workbooks[0];
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

        private static void exportingHandler(object sender, GridExcelExportingEventArgs args)
        {
            if (args.CellType == ExportCellType.HeaderCell)
            {
                args.CellStyle.BackGroundBrush = new SolidColorBrush(Colors.Red);
                args.CellStyle.ForeGroundBrush = new SolidColorBrush(Colors.White);
                args.CellStyle.FontInfo.Bold = true;
                args.CellStyle.FontInfo.Size = 16;
            }
        }

        private static void cellsExportingHandler(object sender, GridCellExcelExportingEventArgs args)
        {
            args.Range.CellStyle.Font.FontName = "Segoe UI";
            args.Range.CellStyle.Font.Size = 11;
            if (args.CellType == ExportCellType.RecordCell)
            {
                if (args.ColumnName == "DateTime")
                {
                    args.Range.Text = Convert.ToDateTime(args.CellValue).ToString(@"MMM dd yyyy");
                    args.Range.HorizontalAlignment = ExcelHAlign.HAlignRight;
                    args.Handled = true;
                }
                else if (args.ColumnName == "Amount")
                {
                    args.Range.Number = (double)args.CellValue;
                    args.Range.CellStyle.NumberFormatIndex = 7;
                    args.Handled = true;
                }
            }
        }

        public static FrameworkElement FindDescendantChildByType(FrameworkElement element, Type type)
        {
            if (element == null) { return null; }

            if (element.GetType() == type)
            {
                return element;
            }
            var childCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childCount; i++)
            {
                var child = (VisualTreeHelper.GetChild(element, i) as FrameworkElement);
                if (child == null) return null;
                var result = FindDescendantChildByType(child, type);
                if (result != null) { return result; }
            }
            return null;
        }
    }

}
