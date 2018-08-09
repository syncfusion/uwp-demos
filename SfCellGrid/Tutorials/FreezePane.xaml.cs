using Common;
using Syncfusion.UI.Xaml.CellGrid;
using Syncfusion.UI.Xaml.CellGrid.Styles;
using Syncfusion.UI.Xaml.Grid.ScrollAxis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CellGridSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FreezePane : SampleLayout,IDisposable
    {
        #region Constructor

        public FreezePane()
        {
            this.InitializeComponent();
            InitializeGrid();
        }

        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            cellGrid.RowCount = 100;
            cellGrid.ColumnCount = 100;
            
            // Default Row Height and Default Column Width reduced
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                cellGrid.DefaultColumnWidth = 80;
                cellGrid.DefaultRowHeight = 20;
            }
            else
            {
                cellGrid.DefaultColumnWidth = 150;
                cellGrid.DefaultRowHeight = 45;
            }

            // By Default Frozen Rows and Frozen Columns are set
            cellGrid.FrozenRows = 3;
            cellGrid.FrozenColumns = 2;
            FreezePanes.Content = "Unfreeze";
            cellGrid.Model.QueryCellInfo += Model_QueryCellInfo;
        }

        #endregion

        #region Events

        private void Model_QueryCellInfo(object sender, GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex == 0 || e.Cell.ColumnIndex == 0)
                e.Style.Background = new SolidColorBrush(Color.FromArgb(15, 0, 0, 0));
            if (e.Cell.RowIndex == 0 && e.Cell.ColumnIndex == 0)
                return;
            e.Style.CellValue = "Row" + e.Cell.RowIndex + " Col" + e.Cell.ColumnIndex;
        }

        private async void FreezePane_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Unfreeze"))
            {
                cellGrid.FrozenRows = cellGrid.HeaderRows;
                cellGrid.FrozenColumns = cellGrid.HeaderColumns;
                (sender as Button).Content = "Freeze Pane";
            }
            else
            {
                if (cellGrid.CurrentCell.RowIndex < 0 || cellGrid.CurrentCell.ColumnIndex < 0)
                {
                    var message = new MessageDialog("Please select any cell...", "Error");
                    await message.ShowAsync();
                    return;
                }
                cellGrid.FrozenColumns = cellGrid.CurrentCell.ColumnIndex;
                cellGrid.FrozenRows = cellGrid.CurrentCell.RowIndex;
                cellGrid.ScrollInView(new RowColumnIndex(cellGrid.CurrentCell.RowIndex, cellGrid.CurrentCell.ColumnIndex));
                (sender as Button).Content = "Unfreeze";
            }
        }

        private void FreezeTopRow_Click(object sender, RoutedEventArgs e)
        {
            cellGrid.FrozenRows = cellGrid.ScrollRows.ScrollLineIndex + 1;
            cellGrid.FrozenColumns = cellGrid.HeaderColumns;
            FreezePanes.Content = "Unfreeze";
        }

        private void FreezeFirstColumn_Click(object sender, RoutedEventArgs e)
        {
            cellGrid.FrozenColumns = cellGrid.ScrollColumns.ScrollLineIndex + 1;
            cellGrid.FrozenRows = cellGrid.HeaderRows;
            FreezePanes.Content = "Unfreeze";
        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            if (cellGrid != null)
            {
                cellGrid.Model.QueryCellInfo -= Model_QueryCellInfo;
                cellGrid.Dispose();
                cellGrid = null;
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion

    }
}
