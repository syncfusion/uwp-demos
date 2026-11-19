using Common;
using Syncfusion.UI.Xaml.CellGrid;
using Syncfusion.UI.Xaml.Spreadsheet;
using Syncfusion.XlsIO;
using Syncfusion.XlsIO.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.Spreadsheet.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SpreadsheetSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CellCustomization : SampleLayout, IDisposable
    {
        public CellCustomization()
        {
            this.InitializeComponent();
            OpenWorkbook();
            spreadsheet.WorkbookLoaded += Spreadsheet_WorkbookLoaded;
            spreadsheet.WorkbookUnloaded += Spreadsheet_WorkbookUnloaded;
            spreadsheet.DisplayAlerts = false;
        }

        private void Spreadsheet_WorkbookUnloaded(object sender, WorkbookUnloadedEventArgs args)
        {
            foreach(var grid in args.GridCollection)
            {
                grid.QueryRange -= Grid_QueryRange;
            }
        }

        private void Spreadsheet_WorkbookLoaded(object sender, WorkbookLoadedEventArgs args)
        {
            foreach(var grid in args.GridCollection)
            {
                grid.CreateGridColumn = CreateSpreadsheetColumnExt;
                var renderer = new SpreadsheetTemplateCellRenderer();
                grid.CellRenderers.Add("ArrowTemplate", renderer);
                grid.QueryRange += Grid_QueryRange;
                spreadsheet.SetRowColumnHeadersVisibility(false);
            }
            spreadsheet.Protect(true, true, "");
            spreadsheet.ProtectSheet(spreadsheet.ActiveSheet, "", Syncfusion.XlsIO.ExcelSheetProtection.None);
        }

        private void Grid_QueryRange(object sender, SpreadsheetQueryRangeEventArgs e)
        {
            if (e.CellType == "Header")
                return;

            var outlineWrappers = (spreadsheet.ActiveSheet as WorksheetImpl).OutlineWrappers;
            foreach (var item in outlineWrappers)
            {
                if (e.Cell.RowIndex.Equals(item.FirstIndex - 1))
                {
                    e.CellType = "ArrowTemplate";
                    e.Handled = true;
                }
            }
        }

        private GridColumn CreateSpreadsheetColumnExt(SfCellGrid arg)
        {
            return new SpreadsheetColumnExt(arg as SpreadsheetGrid, this.spreadsheet);
        }

        private async void OpenWorkbook()
        {
            var assembly = typeof(CellCustomization).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.SfSpreadsheet.Assets.CellCustomization.xlsx";
            try
            {
                using (var fileStream = assembly.GetManifestResourceStream(resourcePath))
                {
                    await this.spreadsheet.Open(fileStream);
                }
            }
            catch
            {

            }
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var outlineWrappers = (spreadsheet.ActiveSheet as WorksheetImpl).OutlineWrappers;
            var buttonTemplate = (sender as Button);

            DependencyObject parent = sender as Button;
            while (parent != null)
            {
                parent = VisualTreeHelper.GetParent(parent);
                if (parent is GridCell)
                    break;
            }
            var rowIndex = (parent as GridCell).RowIndex;

            foreach (var item in outlineWrappers)
            {
                if (rowIndex.Equals(item.FirstIndex - 1))
                {
                    if (item.IsCollapsed)
                    {
                        VisualStateManager.GoToState(buttonTemplate, "Expanded", true);
                        spreadsheet.ActiveSheet.Range[item.OutlineRange.AddressLocal].ExpandGroup(ExcelGroupBy.ByRows);
                        spreadsheet.ActiveGrid.RowHeights.SetHidden(item.FirstIndex, item.LastIndex, false);
                    }
                    else
                    {
                        VisualStateManager.GoToState(buttonTemplate, "Collapsed", true);
                        spreadsheet.ActiveSheet.Range[item.OutlineRange.AddressLocal].CollapseGroup(ExcelGroupBy.ByRows);
                        spreadsheet.ActiveGrid.RowHeights.SetHidden(item.FirstIndex, item.LastIndex, true);
                    }
                    break;
                }
            }
        }
        #region IDisposable Members
        public override void Dispose()
        {
           
            this.spreadsheet.WorkbookLoaded -= Spreadsheet_WorkbookLoaded;
            this.spreadsheet.WorkbookUnloaded -= Spreadsheet_WorkbookUnloaded;
            this.spreadsheet.Dispose();
            Resources.Clear();
            base.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion

    }
}
