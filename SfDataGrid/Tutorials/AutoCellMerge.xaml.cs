using Common;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.UI.Xaml.Grid;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DataGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AutoCellMerge : SampleLayout
    {
        List<OrderInfo> OrdersListDetails = new List<OrderInfo>();

        /// <summary>
        /// get the particular column's data from the record.
        /// </summary>
        IPropertyAccessProvider reflector = null;

        /// <summary>
        /// to remove the conflict range once the given range has been edited.
        /// </summary>
        bool isEditted = false;

        /// <summary>
        /// To ensure the current cell value has been changed.
        /// </summary>
        bool isCurrentCellValueChanged = false;

        public AutoCellMerge()
        {
            this.InitializeComponent();
            var datacontext = new OrderInfoViewModel();
            this.DataContext = datacontext;
            OrdersListDetails = datacontext.OrdersListDetails.Take(200).ToList();
            this.sfGrid.ItemsSource = OrdersListDetails;
            this.sfGrid.QueryCoveredRange += OnSfGridQueryCoveredRange;
            sfGrid.CurrentCellEndEdit += OnSfGridCurrentCellEndEdit;
            sfGrid.AutoGeneratingColumn += OnAutoGeneratingColumns;
            this.sfGrid.ItemsSourceChanged += OnSfGridItemsSourceChanged;
            this.sfGrid.CurrentCellValueChanged += OnSfGridCurrentCellValueChanged;           
        }

        private void OnAutoGeneratingColumns(object sender, AutoGeneratingColumnArgs e)
        {
            if (e.Column.MappingName == "IsVisible")
                e.Cancel = true;
        }

        /// <summary>
        /// Method to handle current cell value changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnSfGridCurrentCellValueChanged(object sender, CurrentCellValueChangedEventArgs args)
        {
            isCurrentCellValueChanged = true;
        }

        /// <summary>
        /// Method that handles ItemsSourceChanged event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSfGridItemsSourceChanged(object sender, Syncfusion.UI.Xaml.Grid.GridItemsSourceChangedEventArgs e)
        {
            if (sfGrid.View != null)
                reflector = sfGrid.View.GetPropertyAccessProvider();
            else
                reflector = null;
        }

        /// <summary>
        /// Method to handle end edit event for SfDataGrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnSfGridCurrentCellEndEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs args)
        {
            if (!isCurrentCellValueChanged)
                return;

            var rowIndex = args.RowColumnIndex.RowIndex;
            var columnIndex = args.RowColumnIndex.ColumnIndex;

            var range = sfGrid.CoveredCells.GetCoveredCellInfo(rowIndex, columnIndex);
            sfGrid.RemoveRange(range);
            isEditted = true;
            sfGrid.GetVisualContainer().InvalidateMeasure();
        }

        /// <summary>
        /// Method to handle the QueryCoveredRange event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSfGridQueryCoveredRange(object sender, Syncfusion.UI.Xaml.Grid.GridQueryCoveredRangeEventArgs e)
        {
            // Merging cells for flat grid
            var range = GetRange(e.GridColumn, e.RowColumnIndex.RowIndex, e.RowColumnIndex.ColumnIndex, e.Record);
            if (range == null)
                return;

            // While editing we need to remove the range.
            if (this.sfGrid.CoveredCells.IsInRange(range))
            {
                var coveredCellInfo = this.sfGrid.GetConflictRange(range);
                if (isEditted)
                {
                    this.sfGrid.CoveredCells.Remove(coveredCellInfo);
                    isEditted = false;
                }
            }

            e.Range = range;
            e.Handled = true;
        }

        /// <summary>
        /// returns the covered cell info based on same data.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="rowData"></param>
        /// <returns></returns>
        private CoveredCellInfo GetRange(GridColumn column, int rowIndex, int columnIndex, object rowData)
        {
            var range = new CoveredCellInfo(columnIndex, columnIndex, rowIndex, rowIndex);
            object data = reflector.GetFormattedValue(rowData, column.MappingName);

            GridColumn leftColumn = null;
            GridColumn rightColumn = null;


            // total rows count.
            int recordsCount = this.sfGrid.GroupColumnDescriptions.Count != 0 ? (this.sfGrid.View.TopLevelGroup.DisplayElements.Count + this.sfGrid.TableSummaryRows.Count + this.sfGrid.UnBoundRows.Count + (this.sfGrid.AddNewRowPosition == AddNewRowPosition.Top ? +1 : 0)) : (this.sfGrid.View.Records.Count + this.sfGrid.TableSummaryRows.Count + this.sfGrid.UnBoundRows.Count + (this.sfGrid.AddNewRowPosition == AddNewRowPosition.Top ? +1 : 0));

            // Merge Horizontally
            // compare right column               
            for (int i = sfGrid.Columns.IndexOf(column); i < this.sfGrid.Columns.Count - 1; i++)
            {
                var compareData = reflector.GetFormattedValue(rowData, sfGrid.Columns[i + 1].MappingName);

                if (compareData == null)
                    break;

                if (!compareData.Equals(data))
                    break;
                rightColumn = sfGrid.Columns[i + 1];
            }

            // compare left column.
            for (int i = sfGrid.Columns.IndexOf(column); i > 0; i--)
            {
                var compareData = reflector.GetFormattedValue(rowData, sfGrid.Columns[i - 1].MappingName);

                if (compareData == null)
                    break;

                if (!compareData.Equals(data))
                    break;
                leftColumn = sfGrid.Columns[i - 1];
            }

            if (leftColumn != null || rightColumn != null)
            {
                // set left index
                if (leftColumn != null)
                {
                    var leftColumnIndex = this.sfGrid.ResolveToScrollColumnIndex(this.sfGrid.Columns.IndexOf(leftColumn));
                    range = new CoveredCellInfo(leftColumnIndex, range.Right, range.Top, range.Bottom);
                }

                // set right index
                if (rightColumn != null)
                {
                    var rightColumIndex = this.sfGrid.ResolveToScrollColumnIndex(this.sfGrid.Columns.IndexOf(rightColumn));
                    range = new CoveredCellInfo(range.Left, rightColumIndex, range.Top, range.Bottom);
                }
                return range;
            }

            // Merge Vertically from the row index.

            int previousRowIndex = -1;
            int nextRowIndex = -1;

            // Get previous row data.                
            var startIndex = sfGrid.ResolveStartIndexBasedOnPosition();
            for (int i = rowIndex - 1; i >= startIndex; i--)
            {
                var previousData = this.sfGrid.GetRecordEntryAtRowIndex(i);
                if (previousData == null || !previousData.IsRecords)
                    break;

                var compareData = reflector.GetFormattedValue((previousData as RecordEntry).Data, column.MappingName);

                if (compareData == null)
                    break;

                if (!compareData.Equals(data))
                    break;
                previousRowIndex = i;
            }

            // get next row data.
            for (int i = rowIndex + 1; i < recordsCount + 1; i++)
            {
                var nextData = this.sfGrid.GetRecordEntryAtRowIndex(i);
                if (nextData == null || !nextData.IsRecords)
                    break;

                var compareData = reflector.GetFormattedValue((nextData as RecordEntry).Data, column.MappingName);

                if (compareData == null)
                    break;

                if (!compareData.Equals(data))
                    break;
                nextRowIndex = i;
            }

            if (previousRowIndex != -1 || nextRowIndex != -1)
            {
                if (previousRowIndex != -1)
                    range = new CoveredCellInfo(range.Left, range.Right, previousRowIndex, range.Bottom);

                if (nextRowIndex != -1)
                    range = new CoveredCellInfo(range.Left, range.Right, range.Top, nextRowIndex);
                return range;
            }

            return null;
        }       
        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.sfGrid.QueryCoveredRange -= OnSfGridQueryCoveredRange;
            sfGrid.AutoGeneratingColumn -= OnAutoGeneratingColumns;
            sfGrid.CurrentCellEndEdit -= OnSfGridCurrentCellEndEdit;
            this.sfGrid.ItemsSourceChanged -= OnSfGridItemsSourceChanged;          
            this.sfGrid.CurrentCellValueChanged -= OnSfGridCurrentCellValueChanged;
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }     
    }
}
