using Common;
using Syncfusion.UI.Xaml.CellGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Text;
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
    public sealed partial class CoveredCells : SampleLayout,IDisposable
    {
        #region Constructor

        public CoveredCells()
        {
            InitializeComponent();
            InitializeGrid();
        }

        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            cellGrid.RowCount = 15;
            cellGrid.ColumnCount = 15;

            // Default Row Height and Default Column Width reduced for Mobile View
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                cellGrid.DefaultColumnWidth = 100;
                cellGrid.DefaultRowHeight = 30;
            }
            else
            {
                cellGrid.DefaultColumnWidth = 125;
                cellGrid.DefaultRowHeight = 40;
            }
            this.cellGrid.Model.QueryCellInfo += Model_QueryCellInfo;

            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(1, 1, 2, 7));
            this.cellGrid.Model[1, 1].CellValue = "Covered Cells Demo";
            this.cellGrid.Model[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[1, 1].VerticalAlignment = VerticalAlignment.Center;
            this.cellGrid.Model[1, 1].Font.FontSize = 24;
            this.cellGrid.Model[1, 1].Font.FontWeight = FontWeights.SemiBold;
            this.cellGrid.Model[1, 1].Background = new SolidColorBrush(Colors.LightBlue);

            var cell = cellGrid.Model[3, 1];
            cell.HorizontalAlignment = HorizontalAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Font.FontWeight = FontWeights.Bold;

            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(4, 2, 5, 2));
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(4, 6, 5, 6));

            cell = this.cellGrid.Model[4, 2];
            cell.CellValue = "Row spanned cell";
            cell.Background = new SolidColorBrush(Colors.BlanchedAlmond);
            cell.HorizontalAlignment = HorizontalAlignment.Center;

            cell = this.cellGrid.Model[4, 6];
            cell.CellValue = "Row spanned cell";
            cell.HorizontalAlignment = HorizontalAlignment.Center;
            cell.Background = new SolidColorBrush(Colors.BlanchedAlmond);

            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(7, 3, 9, 5));
            cell = this.cellGrid.Model[7, 3];
            cell.CellValue = "Column and row spanned cell";
            cell.HorizontalAlignment = HorizontalAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Background = new SolidColorBrush(Colors.BlanchedAlmond);

            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(11, 3, 11, 5));
            cell = this.cellGrid.Model[11, 3];
            cell.CellValue = "Column  spanned cell";
            cell.Background = new SolidColorBrush(Colors.BlanchedAlmond);
            cell.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private async void ShowErrorMessage(string message)
        {
            var msg = new MessageDialog(message, "Error");
            await msg.ShowAsync();
        }

        private void Unmerge_Cells()
        {
            GridRangeInfo Range = cellGrid.SelectedRanges.ActiveRange;
            var rowcol = cellGrid.CurrentCell.CellRowColumnIndex;
            for (int row = Range.Top; row <= Range.Bottom; row++)
            {
                for (int col = Range.Left; col <= Range.Right; col++)
                {
                    cellGrid.CoveredCells.Remove(cellGrid.CoveredCells.GetCoveredCell(row, col));
                }
            }
            var style = cellGrid.Model[Range.Top, Range.Left];
            style.HorizontalAlignment = HorizontalAlignment.Left;
            cellGrid.InvalidateCell(Range);
        }

        #endregion

        #region Events

        private void Model_QueryCellInfo(object sender, Syncfusion.UI.Xaml.CellGrid.Styles.GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex == 0 || e.Cell.ColumnIndex == 0)
                e.Style.Background = new SolidColorBrush(Color.FromArgb(15, 0, 0, 0));
        }

        private void MergeAndCenter_Click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = cellGrid.SelectedRanges.ActiveRange;
            if (cellGrid.SelectedRanges.Count < 1)
            {
                ShowErrorMessage("Please select any range");
                return;
            }
            var intersectedRange = cellGrid.CoveredCells.Ranges.FirstOrDefault(range => range.IntersectsWith(Range));
            if (intersectedRange != null)
            {
                Unmerge_Cells();
                return;
            }

            cellGrid.CoveredCells.Add(new CoveredCellInfo(Range.Top, Range.Left, Range.Bottom, Range.Right));
            var style = cellGrid.Model[Range.Top, Range.Left];
            style.HorizontalAlignment = HorizontalAlignment.Center;
            cellGrid.InvalidateCell(Range);
        }

        private void MergeCells_Click(object sender, RoutedEventArgs e)
        {
            if (cellGrid.SelectedRanges.Count < 1)
            {
                ShowErrorMessage("Please select any range");
                return;
            }
            Unmerge_Cells();
            GridRangeInfo Range = cellGrid.SelectedRanges.ActiveRange;
            cellGrid.CoveredCells.Add(new CoveredCellInfo(Range.Top, Range.Left, Range.Bottom, Range.Right));
            cellGrid.InvalidateCell(Range);
        }

        private void MergeAcross_Click(object sender, RoutedEventArgs e)
        {
            if (cellGrid.SelectedRanges.Count < 1)
            {
                ShowErrorMessage("Please select any range");
                return;
            }
            Unmerge_Cells();
            GridRangeInfo Range = cellGrid.SelectedRanges.ActiveRange;
            int left = Range.Top, right = Range.Bottom;
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                cellGrid.CoveredCells.Add(new CoveredCellInfo(i, Range.Left, i, Range.Right));
            }
            cellGrid.InvalidateCell(Range);
        }

        private void UnMerge_Click(object sender, RoutedEventArgs e)
        {
            if (cellGrid.SelectedRanges.Count < 1)
            {
                ShowErrorMessage("Please select any range");
                return;
            }
            Unmerge_Cells();
        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            if (cellGrid != null)
            {
                cellGrid.Dispose();
                cellGrid = null;
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion
    }
}
