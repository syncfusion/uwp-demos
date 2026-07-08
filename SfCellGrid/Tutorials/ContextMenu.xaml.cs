using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Syncfusion.UI.Xaml.CellGrid;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.Grid.ScrollAxis;
using Windows.UI.Popups;
using Windows.UI;
using Syncfusion.UI.Xaml.Controls.SfRibbon;
using Common;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CellGridSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContextMenu : SampleLayout,IDisposable
    {
        #region Constructor

        public ContextMenu()
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
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                cellGrid.ColumnWidths[0] = 50;
            else
                cellGrid.ColumnWidths[0] = 100;
            cellGrid.Model.QueryCellInfo += Model_QueryCellInfo;
            LoadData();
            cellGrid.CellContextMenu = GetContextMenu();
            cellGrid.CellContextMenuOpening += CellGrid_CellContextMenuOpening;
        }

        private void LoadData()
        {
            for (int i = 1; i < cellGrid.RowCount; i++)
            {
                for (int j = 1; j < cellGrid.ColumnCount; j++)
                {
                    this.cellGrid.Model[i, j].CellValue = "R" + i + " C" + j;
                }
            }
        }

        private Syncfusion.UI.Xaml.CellGrid.ContextMenu GetContextMenu()
        {
            var contextMenu = new Syncfusion.UI.Xaml.CellGrid.ContextMenu();
            var activeRange = cellGrid.SelectedRanges.ActiveRange;
            if (activeRange.RangeType == GridRangeInfoType.Rows || activeRange.RangeType == GridRangeInfoType.Cols)
            {
                var insert = new Button
                {
                    Style = this.Resources["ButtonStyle"] as Style,
                    Width = 120,
                    Height = 50
                };
                var delete = new Button
                {
                    Style = this.Resources["ButtonStyle"] as Style,
                    Width = 120,
                    Height = 50
                };
                var hide = new Button
                {
                    Style = this.Resources["ButtonStyle"] as Style,
                    Width = 120,
                    Height = 50
                };
                var unhide = new Button
                {
                    Style = this.Resources["ButtonStyle"] as Style,
                    Width = 120,
                    Height = 50
                };
                if (activeRange.RangeType == GridRangeInfoType.Rows)
                {
                    insert.Content = "Insert Rows";
                    insert.Click += InsertRows_Click;

                    delete.Content = "Delete Rows";
                    delete.Click += DeleteRows_Click;

                    hide.Content = "Hide Rows";
                    hide.Click += HideRows_Click;

                    unhide.Content = "Unhide Rows";
                    unhide.Click += UnhideRows_Click;
                }
                else
                {
                    insert.Content = "Insert Columns";
                    insert.Click += InsertColumns_Click;

                    delete.Content = "Delete Columns";
                    delete.Click += DeleteColumns_Click;

                    hide.Content = "Hide Columns";
                    hide.Click += HideColumns_Click;

                    unhide.Content = "Unhide Columns";
                    unhide.Click += UnhideColumns_Click;
                }

                contextMenu.Items.Add(insert);
                contextMenu.Items.Add(delete);
                contextMenu.Items.Add(hide);
                contextMenu.Items.Add(unhide);
            }
            else
            {
                var cut = new Button
                {
                    Content = "Cut",
                    Style = this.Resources["ButtonStyle"] as Style,
                    Width = 90,
                    Height = 50,
                };
                var copy = new Button
                {
                    Content = "Copy",
                    Style = this.Resources["ButtonStyle"] as Style,
                    Width = 90,
                    Height = 50,
                };
                var paste = new Button
                {
                    Content = "Paste",
                    Style = this.Resources["ButtonStyle"] as Style,
                    Width = 90,
                    Height = 50,
                };
                var clear = new Button
                {
                    Content = "Clear",
                    Style = this.Resources["ButtonStyle"] as Style,
                    Width = 90,
                    Height = 50,
                };
                cut.Click += Cut_Click;
                copy.Click += Copy_Click;
                paste.Click += Paste_Click;
                clear.Click += Clear_Click;

                contextMenu.Items.Add(cut);
                contextMenu.Items.Add(copy);
                contextMenu.Items.Add(paste);
                contextMenu.Items.Add(clear);
            }
            return contextMenu;
        }

        private async void ShowErrorMessage(string Msg)
        {
            var message = new MessageDialog(Msg, "Error");
            await message.ShowAsync();
        }

        #endregion

        #region Events

        private void Model_QueryCellInfo(object sender, Syncfusion.UI.Xaml.CellGrid.Styles.GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex == 0 && e.Cell.ColumnIndex == 0)
                return;
            else if (e.Cell.RowIndex == 0)
            {
                e.Style.CellValue = GridRangeInfo.GetAlphaLabel(e.Cell.ColumnIndex);
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
            }
            else if (e.Cell.ColumnIndex == 0)
            {
                e.Style.CellValue = e.Cell.RowIndex;
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = cellGrid.SelectedRanges.ActiveRange;
            if (Range.IsTable)
            {
                cellGrid.Model.ClearStyles();
                cellGrid.InvalidateCells();
            }
            else
            {
                if (Range.IsRows)
                    Range = GridRangeInfo.Cells(Range.Top, cellGrid.HeaderColumns, Range.Bottom, cellGrid.ColumnCount);
                else if (Range.IsCols)
                    Range = GridRangeInfo.Cells(cellGrid.HeaderRows, Range.Left, cellGrid.RowCount, Range.Right);

                for (int i = Range.Top; i <= Range.Bottom; i++)
                {
                    for (int j = Range.Left; j <= Range.Right; j++)
                    {
                        RowColumnIndex rowcol = new RowColumnIndex(i, j);
                        cellGrid.Model.ClearStyle(rowcol);
                    }
                }
                cellGrid.InvalidateCell(Range);
            }
            cellGrid.ShowHidePopup(false);
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            cellGrid.CopyPaste.Paste();
            cellGrid.CurrentCell.Refresh();
            cellGrid.ShowHidePopup(false);
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            cellGrid.CopyPaste.Copy();
            cellGrid.ShowHidePopup(false);
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            cellGrid.CopyPaste.Cut();
            cellGrid.ShowHidePopup(false);
        }

        private void UnhideColumns_Click(object sender, RoutedEventArgs e)
        {
            var activeRange = cellGrid.SelectedRanges.ActiveRange;
            if (activeRange.IsEmpty)
            {
                ShowErrorMessage("Please Select any Range...");
                return;
            }
            cellGrid.ColumnWidths.SetHidden(activeRange.Left, activeRange.Right, false);
            cellGrid.InvalidateCell(activeRange);
            cellGrid.ShowHidePopup(false);
        }

        private void HideColumns_Click(object sender, RoutedEventArgs e)
        {
            var activeRange = cellGrid.SelectedRanges.ActiveRange;
            if (activeRange.IsEmpty)
            {
                ShowErrorMessage("Please select any range...");
                return;
            }
            cellGrid.ColumnWidths.SetHidden(activeRange.Left, activeRange.Right, true);
            cellGrid.InvalidateCell(activeRange);
            cellGrid.ShowHidePopup(false);
        }

        private void DeleteColumns_Click(object sender, RoutedEventArgs e)
        {
            var activeRange = cellGrid.SelectedRanges.ActiveRange;
            if (activeRange.IsEmpty)
            {
                ShowErrorMessage("Please select any range...");
                return;
            }
            cellGrid.Model.RemoveColumns(activeRange.Left, activeRange.Width);
            cellGrid.InvalidateCells();
            cellGrid.ShowHidePopup(false);
        }

        private void InsertColumns_Click(object sender, RoutedEventArgs e)
        {
            var activeRange = cellGrid.SelectedRanges.ActiveRange;
            if (activeRange.IsEmpty)
            {
                ShowErrorMessage("Please select any range...");
                return;
            }
            cellGrid.Model.InsertColumns(activeRange.Left, activeRange.Width);
            cellGrid.InvalidateCells();
            cellGrid.ShowHidePopup(false);
        }

        private void UnhideRows_Click(object sender, RoutedEventArgs e)
        {
            var activeRange = cellGrid.SelectedRanges.ActiveRange;
            if (activeRange.IsEmpty)
            {
                ShowErrorMessage("Please select any range...");
                return;
            }
            cellGrid.RowHeights.SetHidden(activeRange.Top, activeRange.Bottom, false);
            cellGrid.InvalidateCell(activeRange);
            cellGrid.ShowHidePopup(false);
        }

        private void HideRows_Click(object sender, RoutedEventArgs e)
        {
            var activeRange = cellGrid.SelectedRanges.ActiveRange;
            if (activeRange.IsEmpty)
            {
                ShowErrorMessage("Please select any range...");
                return;
            }
            cellGrid.RowHeights.SetHidden(activeRange.Top, activeRange.Bottom, true);
            cellGrid.InvalidateCell(activeRange);
            cellGrid.ShowHidePopup(false);
        }

        private void DeleteRows_Click(object sender, RoutedEventArgs e)
        {
            var activeRange = cellGrid.SelectedRanges.ActiveRange;
            if (activeRange.IsEmpty)
            {
                ShowErrorMessage("Please select any range...");
                return;
            }
            cellGrid.Model.RemoveRows(activeRange.Top, activeRange.Height);
            cellGrid.InvalidateCells();
            cellGrid.ShowHidePopup(false);
        }

        private void InsertRows_Click(object sender, RoutedEventArgs e)
        {
            var activeRange = cellGrid.SelectedRanges.ActiveRange;
            if (activeRange.IsEmpty)
            {
                ShowErrorMessage("Please select any range...");
                return;
            }
            cellGrid.Model.InsertRows(activeRange.Top, activeRange.Height);
            cellGrid.InvalidateCells();
            cellGrid.ShowHidePopup(false);
        }

        private void CellGrid_CellContextMenuOpening(object sender, Syncfusion.UI.Xaml.CellGrid.Helpers.CellContextMenuOpeningEventArgs e)
        {
            if (cellGrid.SelectedRanges.Count < 0)
                return;
            e.CellContextMenu = GetContextMenu();
        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            if (cellGrid != null)
            {
                cellGrid.Model.QueryCellInfo -= Model_QueryCellInfo;
                cellGrid.CellContextMenuOpening -= CellGrid_CellContextMenuOpening;
                cellGrid.Dispose();
                cellGrid = null;
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion
    }
}
