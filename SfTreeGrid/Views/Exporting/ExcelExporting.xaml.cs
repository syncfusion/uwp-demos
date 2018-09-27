using Common;
using Syncfusion.UI.Xaml.TreeGrid.Converter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.TreeGrid;
using Windows.System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfTreeGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExcelExporting : SampleLayout, IDisposable
    {
        ExcelEngine excelEngine = null;
        public ExcelExporting()
        {
            this.InitializeComponent();
            this.treeGrid.CurrentCellRequestNavigate += TreeGrid_CurrentCellRequestNavigate;
        }

        private async void TreeGrid_CurrentCellRequestNavigate(object sender, UI.Xaml.Grid.CurrentCellRequestNavigateEventArgs args)
        {
            var URI = "http://en.wikipedia.org/wiki/" + args.NavigateText;
            await Launcher.LaunchUriAsync(new Uri(URI));
        }
        private async void OnExportToExcel(object sender, RoutedEventArgs e)
        {
            var options = new TreeGridExcelExportingOptions()
            {
                AllowOutliningGroups = (bool)AllowOutlining.IsChecked,
                AllowIndentColumn = (bool)AllowIndentColumn.IsChecked,
                IsGridLinesVisible = (bool)IsGridLinesVisible.IsChecked,
                CanExportHyperLink = (bool)CanExportHyperLink.IsChecked,
                NodeExpandMode = (nodeexpandMode.SelectedIndex == 0 ? NodeExpandMode.Default :
                               (nodeexpandMode.SelectedIndex == 1 ? NodeExpandMode.ExpandAll : NodeExpandMode.CollapseAll)),
                ExcelVersion = ExcelVersion.Excel2013,
                ExportingEventHandler = ExportingHandler
            };

            if ((bool)customizeColumns.IsChecked)
                options.CellsExportingEventHandler = CustomizeCellExportingHandler;

            excelEngine = new ExcelEngine();
            IWorkbook workBook = excelEngine.Excel.Workbooks.Create();
            treeGrid.ExportToExcel(workBook, options);

            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
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

                options.CellsExportingEventHandler = null;
                options.ExportingEventHandler = null;
                var msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");

                var yesCmd = new UICommand("Yes");
                var noCmd = new UICommand("No");
                msgDialog.Commands.Add(yesCmd);
                msgDialog.Commands.Add(noCmd);
                var cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the saved file
                    bool success = await Launcher.LaunchFileAsync(storageFile);
                }
            }
        }

        #region ExportToExcel Event Handlers
        private static void CustomizeCellExportingHandler(object sender, TreeGridCellExcelExportingEventArgs e)
        {
            if (e.CellType == TreeGridCellType.RecordCell && e.ColumnName == "Salary")
                e.Range.CellStyle.ColorIndex = ExcelKnownColors.Sky_blue;
        }
        private static void ExportingHandler(object sender, TreeGridExcelExportingEventArgs e)
        {
            if (e.CellType == TreeGridCellType.HeaderCell)
                e.Style.Font.Bold = true;
            e.Style.BeginUpdate();
            e.Style.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
            e.Style.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
            e.Style.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
            e.Style.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
            e.Style.EndUpdate();
            e.Handled = true;
        }

        #endregion
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.treeGrid.CurrentCellRequestNavigate -= TreeGrid_CurrentCellRequestNavigate;
            this.treeGrid.ItemsSource = null;
            if (excelEngine != null)
                excelEngine.Dispose();
            this.treeGrid.Dispose();
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
