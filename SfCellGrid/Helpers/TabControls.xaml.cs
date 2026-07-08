using Syncfusion.UI.Xaml.CellGrid;
using Syncfusion.UI.Xaml.CellGrid.Converter;
using Syncfusion.UI.Xaml.CellGrid.Helpers;
using Syncfusion.UI.Xaml.CellGrid.Styles;
using Syncfusion.UI.Xaml.Controls.Navigation;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.ScrollAxis;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace tabcontrol
{
    public sealed partial class tabs : UserControl , IDisposable
    {

        #region Private Fields

        int index,sheetvalue=1;
        private SfCellGrid _activegrid;

        #endregion

        #region Constructor

        public tabs()
        {
            this.InitializeComponent();
            Gridload(1);
        }

        #endregion

        #region Properties

        internal ObservableCollection<SfCellGrid> Gridcollection = new ObservableCollection<SfCellGrid>();

        internal SfCellGrid ActiveGrid
        {
            get
            {
                return _activegrid;
            }
            set
            {
                _activegrid = value;
                
            }
        }

        #endregion

        #region Events

        private void tabcontrol_SelectionChanged_1(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            UnWireEvnets(_activegrid);
            var tab = sender as SfTabControl;
            if (tab.SelectedItem == null)
                return;
            var selected = tab.SelectedItem as SfTabItem;
            index = tab.SelectedIndex;
            if (_activegrid != selected.Content as SfCellGrid)
            {
                _activegrid = selected.Content as SfCellGrid;
                WireEvnets(_activegrid);
            }
        }

        void Model_QueryCellInfo(object sender, GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex == 0 && e.Cell.ColumnIndex == 0)
                return;
            if (e.Cell.RowIndex == 0)
            {
                e.Style.CellValue = GridRangeInfo.GetAlphaLabel(e.Cell.ColumnIndex);
                e.Style.VerticalAlignment = VerticalAlignment.Center;
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
            }
            else if (e.Cell.ColumnIndex == 0)
            {
                e.Style.CellValue = e.Cell.RowIndex;
                e.Style.VerticalAlignment = VerticalAlignment.Center;
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
            }
        }

        #endregion

        #region Internal Methods

        internal async void LoadExcel(StorageFile file)
        {
            tabcontrol.Items.Clear();
            Gridcollection.Clear();
            var engine = new ExcelEngine();
            IWorkbook book = await engine.Excel.Workbooks.OpenAsync(file);

            for (int i = 0; i < book.Worksheets.Count; i++)
            {
                SfCellGrid cellgrid = new SfCellGrid();
                cellgrid.RowCount = 1000;
                cellgrid.ColumnCount = 1000;
                cellgrid.DefaultColumnWidth = 64;
                cellgrid.DefaultRowHeight = 20;
                cellgrid.Model.TableStyle.CellType = "FormulaCell";
                ExcelImportExtension.ImportFromExcel(cellgrid, book.Worksheets[i], null);
                cellgrid.RowCount = cellgrid.RowCount < 100 ? 100 : cellgrid.RowCount;
                cellgrid.ColumnCount = cellgrid.ColumnCount < 100 ? 100 : cellgrid.ColumnCount;
                SfTabItem item = new SfTabItem();
                item.Content = cellgrid;
                item.Header = book.Worksheets[i].Name;
                item.Foreground = new SolidColorBrush(Colors.Black);
                item.FontSize = 16;
                item.Height = 40;
                tabcontrol.Items.Add(item);
                Gridcollection.Add(cellgrid);
            }
            tabcontrol.SelectedIndex = 0;
        }

        internal void Gridload(int count)
        {
            tabcontrol.Items.Clear();
            Gridcollection.Clear();
            sheetvalue = 1;
            for (int i = 0; i < count; i++)
            {
                SfCellGrid cellgrid = new SfCellGrid();
                //cellgrid.Loaded += cellgrid_Loaded;
                cellgrid.RowCount = 1000;
                cellgrid.ColumnCount = 1000;
                cellgrid.DefaultColumnWidth = 64;
                cellgrid.DefaultRowHeight = 20;
                cellgrid.Model.TableStyle.CellType = "FormulaCell";
                SfTabItem item = new SfTabItem();
                cellgrid.Name = "Sheet" + (sheetvalue++);
                item.Content = cellgrid;
                item.Header = cellgrid.Name;
                item.Foreground = new SolidColorBrush(Colors.Black);
                item.FontSize = 16;
                item.Height = 40;
                tabcontrol.Items.Add(item);
                Gridcollection.Add(cellgrid);
            }
            tabcontrol.SelectedIndex = 0;
        }

        #endregion

        #region Private Methods

        private void WireEvnets(SfCellGrid cell)
        {
            cell.Model.QueryCellInfo += Model_QueryCellInfo;
        }

        private void UnWireEvnets(SfCellGrid cell)
        {
            if(cell!=null)
                cell.Model.QueryCellInfo -= Model_QueryCellInfo;
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            if (tabcontrol != null)
                tabcontrol.Dispose();
        }

        #endregion
    }
}
