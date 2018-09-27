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
    public sealed partial class RowColumnManipulation : SampleLayout,IDisposable
    {
        #region Constructor

        public RowColumnManipulation()
        {
            this.InitializeComponent();
            InitializeGrid();
            this.Loaded += MainPage_Loaded;
            LoadValues();
        }

        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            cellGrid.RowCount = 100;
            cellGrid.ColumnCount = 100;
            cellGrid.DefaultRowHeight = 30;
            cellGrid.DefaultColumnWidth = 100;
            cellGrid.ColumnWidths[0] = 50;
            cellGrid.Model.QueryCellInfo += Model_QueryCellInfo;
        }

        private void LoadValues()
        {
            for (int i = 1; i < cellGrid.RowCount; i++)
            {
                for (int j = 1; j < cellGrid.ColumnCount; j++)
                {
                    this.cellGrid.Model[i, j].CellValue = "R" + i + " C" + j;
                }
            }
        }

        private async void ShowErrorMessage(string Message)
        {
            var msg = new MessageDialog(Message, "Error");
            await msg.ShowAsync();
        }

        #endregion

        #region Events

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            rowCount.Text = cellGrid.RowCount.ToString();
            columnCount.Text = cellGrid.ColumnCount.ToString();
            rowHeight.Text = cellGrid.DefaultRowHeight.ToString();
            columnWidth.Text = cellGrid.DefaultColumnWidth.ToString();
            sRange.Text = "5";
            eRange.Text = "10";
            HeightWidth.Text = "30";
            startRange.Text = "3";
            endRange.Text = "3";
            startIndex.Text = "5";
            Count.Text = "5";
        }

        private void Model_QueryCellInfo(object sender, Syncfusion.UI.Xaml.CellGrid.Styles.GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex == 0 || e.Cell.ColumnIndex == 0)
            {
                e.Style.Background = new SolidColorBrush(Color.FromArgb(15, 0, 0, 0));
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
            }
            if (e.Cell.RowIndex == 0 && e.Cell.ColumnIndex == 0)
                return;
            if (e.Cell.RowIndex == 0)
                e.Style.CellValue = e.Cell.ColumnIndex;
            else if (e.Cell.ColumnIndex == 0)
                e.Style.CellValue = e.Cell.RowIndex;
        }

        private void rowColumnCount_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (int.TryParse(rowCount.Text, out count) && count > 0)
                cellGrid.RowCount = count;
            else
                ShowErrorMessage("Enter valid row count...");

            if (int.TryParse(columnCount.Text, out count) && count > 0)
                cellGrid.ColumnCount = count;
            else
                ShowErrorMessage("Enter valid column count...");

            cellGrid.Focus(FocusState.Programmatic);
        }

        private void defaultRowHeight_Click(object sender, RoutedEventArgs e)
        {
            double value = 0;
            if (Double.TryParse(rowHeight.Text, out value))
            {
                if (value < 0 || value > 409)
                    ShowErrorMessage("Row Height must be between 0 and 409");
                else
                    cellGrid.DefaultRowHeight = value;
            }
            else
                ShowErrorMessage("Enter a valid row height...");
            if (Double.TryParse(columnWidth.Text, out value))
            {
                if (value < 0 || value > 409)
                    ShowErrorMessage("Column Width must be between 0 and 255");
                else
                    cellGrid.DefaultColumnWidth = value;
            }
            else
                ShowErrorMessage("Enter a valid column width...");

            cellGrid.Focus(FocusState.Programmatic);
        }

        private void InsertRows_Click(object sender, RoutedEventArgs e)
        {
            int start = 0;
            int count = 0;
            if (int.TryParse(startIndex.Text, out start) && int.TryParse(Count.Text, out count))
            {
                if (start < 1 || count < 1 || start > cellGrid.RowCount)
                    ShowErrorMessage("Enter valid start index and count");
                else
                {
                    cellGrid.Model.InsertRows(start, count);
                    cellGrid.InvalidateCells();
                }
            }
            else
                ShowErrorMessage("Enter a valid value...");
            cellGrid.Focus(FocusState.Programmatic);
        }

        private void InsertColumns_Click(object sender, RoutedEventArgs e)
        {
            int start = 0;
            int count = 0;
            if (int.TryParse(startIndex.Text, out start) && int.TryParse(Count.Text, out count))
            {
                if (start < 1 || count < 1 || start > cellGrid.ColumnCount)
                    ShowErrorMessage("Enter valid start index and count");
                else
                {
                    cellGrid.Model.InsertColumns(start, count);
                    cellGrid.InvalidateCells();
                }
            }
            else
                ShowErrorMessage("Enter a valid value...");
            cellGrid.Focus(FocusState.Programmatic);
        }

        private void HideRows_Click(object sender, RoutedEventArgs e)
        {
            int sRange = 0;
            int eRange = 0;
            if (int.TryParse(startRange.Text, out sRange) && int.TryParse(endRange.Text, out eRange))
            {
                if (sRange < 1 || eRange < 1 || (sRange + (eRange - sRange)) > cellGrid.ColumnCount - 1)
                    ShowErrorMessage("Enter valid start and end Range");
                else if (eRange < sRange)
                    ShowErrorMessage("Enter end range greater than start range");
                else
                {
                    cellGrid.RowHeights.SetHidden(sRange, eRange, true);
                    cellGrid.InvalidateCell(GridRangeInfo.Rows(sRange, eRange));
                }
            }
            else
                ShowErrorMessage("Enter a valid Range...");
            cellGrid.Focus(FocusState.Programmatic);
        }

        private void HideColumns_Click(object sender, RoutedEventArgs e)
        {
            int sRange = 0;
            int eRange = 0;
            if (int.TryParse(startRange.Text, out sRange) && int.TryParse(endRange.Text, out eRange))
            {
                if (sRange < 1 || eRange < 1 || (sRange + (eRange - sRange)) > cellGrid.ColumnCount - 1)
                    ShowErrorMessage("Enter valid start and end range");
                else if (eRange < sRange)
                    ShowErrorMessage("Enter end range greater than start range");
                else
                {
                    cellGrid.ColumnWidths.SetHidden(sRange, eRange, true);
                    cellGrid.InvalidateCell(GridRangeInfo.Cols(sRange, eRange));
                }
            }
            else
                ShowErrorMessage("Enter a valid Range...");
            cellGrid.Focus(FocusState.Programmatic);
        }

        private void UnHideRows_Click(object sender, RoutedEventArgs e)
        {
            int sRange = 0;
            int eRange = 0;
            if (int.TryParse(startRange.Text, out sRange) && int.TryParse(endRange.Text, out eRange))
            {
                if (sRange < 1 || eRange < 1 || (sRange + (eRange - sRange)) > cellGrid.ColumnCount - 1)
                    ShowErrorMessage("Enter valid start range and end range");
                else if (eRange < sRange)
                    ShowErrorMessage("Enter end range greater than start range");
                else
                {
                    cellGrid.RowHeights.SetHidden(sRange, eRange, false);
                    cellGrid.InvalidateCell(GridRangeInfo.Rows(sRange, eRange));
                }
            }
            else
                ShowErrorMessage("Enter a valid range...");
            cellGrid.Focus(FocusState.Programmatic);
        }

        private void UnHideColumns_Click(object sender, RoutedEventArgs e)
        {
            int sRange = 0;
            int eRange = 0;
            if (int.TryParse(startRange.Text, out sRange) && int.TryParse(endRange.Text, out eRange))
            {
                if (sRange < 1 || eRange < 1 || (sRange + (eRange - sRange)) > cellGrid.ColumnCount - 1)
                    ShowErrorMessage("Enter valid start range and end range");
                else if (eRange < sRange)
                    ShowErrorMessage("Enter end range greater than start range");
                else
                {
                    cellGrid.ColumnWidths.SetHidden(sRange, eRange, false);
                    cellGrid.InvalidateCell(GridRangeInfo.Cols(sRange, eRange));
                }
            }
            else
                ShowErrorMessage("Enter a valid Range...");
            cellGrid.Focus(FocusState.Programmatic);
        }

        private void DeleteRows_Click(object sender, RoutedEventArgs e)
        {
            int start = 0;
            int count = 0;
            if (int.TryParse(startIndex.Text, out start) && int.TryParse(Count.Text, out count))
            {
                if (start < 1 || count < 1 || (start + count) > cellGrid.RowCount)
                    ShowErrorMessage("Enter valid start index and count");
                else
                {
                    cellGrid.Model.RemoveRows(start, count);
                    cellGrid.InvalidateCells();
                }
            }
            else
                ShowErrorMessage("Enter a valid value...");
            cellGrid.Focus(FocusState.Programmatic);
        }

        private void DeleteColumns_Click(object sender, RoutedEventArgs e)
        {
            int start = 0;
            int count = 0;
            if (int.TryParse(startIndex.Text, out start) && int.TryParse(Count.Text, out count))
            {
                if (start < 1 || count < 1 || (start + count) > cellGrid.ColumnCount)
                    ShowErrorMessage("Enter valid start index and count");
                else
                {
                    cellGrid.Model.RemoveColumns(start, count);
                    cellGrid.InvalidateCells();
                }
            }
            else
                ShowErrorMessage("Enter a valid value...");
            cellGrid.Focus(FocusState.Programmatic);
        }

        private void SetRowHeight_Click(object sender, RoutedEventArgs e)
        {
            int start = 0;
            int end = 0;
            double height = 0;
            if (int.TryParse(sRange.Text, out start) && int.TryParse(eRange.Text, out end) && Double.TryParse(HeightWidth.Text, out height))
            {
                if (start < 1 || end < 1 || (start + (end - start)) > cellGrid.RowCount - 1)
                    ShowErrorMessage("Enter valid range");
                else if (end < start)
                    ShowErrorMessage("Enter end index greater than start index");
                else if (height < 1 || height > 409)
                    ShowErrorMessage("Row Height must be between 0 and 409");
                else
                {
                    cellGrid.RowHeights.SetRange(start, end, height);
                    cellGrid.InvalidateCell(GridRangeInfo.Rows(start, end));
                }
            }
            else
                ShowErrorMessage("Enter a valid range and value");
            cellGrid.Focus(FocusState.Programmatic);
        }

        private void SetColumnWidth_Click(object sender, RoutedEventArgs e)
        {
            int start = 0;
            int end = 0;
            double width = 0;
            if (int.TryParse(sRange.Text, out start) && int.TryParse(eRange.Text, out end) && Double.TryParse(HeightWidth.Text, out width))
            {
                if (start < 1 || end < 1 || (start + (end - start)) > cellGrid.ColumnCount - 1)
                    ShowErrorMessage("Enter valid range");
                else if (end < start)
                    ShowErrorMessage("Enter end index greater than start index");
                else if (width < 1 || width > 255)
                    ShowErrorMessage("Column Width must be between 0 and 255");
                else
                {
                    cellGrid.ColumnWidths.SetRange(start, end, width);
                    cellGrid.InvalidateCell(GridRangeInfo.Cols(start, end));
                }
            }
            else
                ShowErrorMessage("Enter a valid range and valid value");
            cellGrid.Focus(FocusState.Programmatic);
        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            this.Loaded -= MainPage_Loaded;
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
