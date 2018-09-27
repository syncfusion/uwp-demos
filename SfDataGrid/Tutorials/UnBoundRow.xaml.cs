using Common;
using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DataGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UnBoundRow : SampleLayout
    {
        // totalSales used to store summary of each column.
        Dictionary<string, double> totalSales;
        // totalSelectedSales used to store selected rows.
        Dictionary<string, double> totalSelectedSales;
        // percentSales used to store percentage of selected Rows summary.
        Dictionary<string, double> percentSales;

        public UnBoundRow()
        {
            this.InitializeComponent();
            var datacontext = new SalesInfoViewModel();
            this.DataContext = datacontext;

            totalSales = new Dictionary<string, double>();
            totalSelectedSales = new Dictionary<string, double>();
            percentSales = new Dictionary<string, double>();

            this.SfDataGrid.ItemsSource = datacontext.YearlySalesDetails;
            this.SfDataGrid.Loaded += OnSfDataGridLoaded;
            this.SfDataGrid.QueryRowHeight += OnSfDataGridQueryRowHeight;
            this.SfDataGrid.QueryUnBoundRow += OnSfDataGridQueryUnBoundRow;
            this.SfDataGrid.SelectionChanged += OnSfDataGridSelectionChanged;
            this.SfDataGrid.CurrentCellEndEdit += OnSfDataGridCurrentCellEndEdit;
        }

        /// <summary>
        /// Occurs when the SfDataGrid is laid out, rendered, and ready for interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSfDataGridLoaded(object sender, RoutedEventArgs e)
        {            
            // populate the totalSales by summary value.
            foreach (GridColumn column in this.SfDataGrid.Columns)
                totalSales[column.MappingName] = GetSummaryValue(column.MappingName);

            // populate the percentSales by default value fro QS1 column alone.
            foreach (GridColumn column in this.SfDataGrid.Columns)
                percentSales[column.MappingName] = column.MappingName.Equals("QS1") ? 2.25 : 0;

            // Refresh the UnboundRows.
            this.SfDataGrid.InValidateUnBoundRow(this.SfDataGrid.UnBoundRows[0]);
            this.SfDataGrid.GetVisualContainer().InvalidateMeasureInfo();

            var collection = this.SfDataGrid.DataContext as SalesInfoViewModel;
            foreach (SalesByYear sales in collection.YearlySalesDetails.Skip(3).Take(3))
                this.SfDataGrid.SelectedItems.Add(sales);
        }
       
        /// <summary>
        /// Occurs when the selection is change in SfDataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSfDataGridSelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            // Populate selected rows summary values.
            foreach (GridColumn column in this.SfDataGrid.Columns)
                totalSelectedSales[column.MappingName] = GetSummaryValue(column.MappingName, false);

            // Refresh the UnBound rows.
            this.SfDataGrid.InValidateUnBoundRow(this.SfDataGrid.UnBoundRows[1]);
            this.SfDataGrid.InValidateUnBoundRow(this.SfDataGrid.UnBoundRows[2]);
            this.SfDataGrid.InValidateUnBoundRow(this.SfDataGrid.UnBoundRows[3]);
            this.SfDataGrid.InValidateUnBoundRow(this.SfDataGrid.UnBoundRows[4]);
            this.SfDataGrid.GetVisualContainer().InvalidateMeasureInfo();
        }

        /// <summary>
        /// Occurs when end edit the cell in SfDataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnSfDataGridCurrentCellEndEdit(object sender, CurrentCellEndEditEventArgs args)
        {
            // Updates the totals with edited values.
            foreach (GridColumn column in this.SfDataGrid.Columns)
                totalSales[column.MappingName] = GetSummaryValue(column.MappingName);

            foreach (GridColumn column in this.SfDataGrid.Columns)
                totalSelectedSales[column.MappingName] = GetSummaryValue(column.MappingName, false);

            // Refresh unboundRow after complete editing.
            this.SfDataGrid.InValidateUnBoundRow(this.SfDataGrid.UnBoundRows[0]);
            this.SfDataGrid.InValidateUnBoundRow(this.SfDataGrid.UnBoundRows[1]);
            this.SfDataGrid.InValidateUnBoundRow(this.SfDataGrid.UnBoundRows[2]);
            this.SfDataGrid.InValidateUnBoundRow(this.SfDataGrid.UnBoundRows[3]);
            this.SfDataGrid.InValidateUnBoundRow(this.SfDataGrid.UnBoundRows[4]);
            this.SfDataGrid.GetVisualContainer().InvalidateMeasureInfo();
        }

        // which customize the UnBoundRow values.
        void OnSfDataGridQueryUnBoundRow(object sender, GridUnBoundRowEventsArgs e)
        {
            if (!(totalSales.ContainsKey(e.Column.MappingName)))
                return;

            if (e.UnBoundAction == UnBoundActions.QueryData)
            {
                if (e.RowColumnIndex.RowIndex == 1)
                {
                    if (e.RowColumnIndex.ColumnIndex == 0)
                    {
                        e.Value = "Total Sales By Month";
                        e.CellTemplate = this.Resources["UnBoundCellTemplate"] as DataTemplate;
                    }
                    else
                    {
                        e.Value = totalSales[e.Column.MappingName];
                        e.CellTemplate = this.Resources["UnBoundRowCellTemplate"] as DataTemplate;
                    }
                }
                else if (e.GridUnboundRow.UnBoundRowIndex == 0 && e.GridUnboundRow.Position == UnBoundRowsPosition.Bottom && e.GridUnboundRow.ShowBelowSummary == true)
                {
                    if (e.RowColumnIndex.ColumnIndex == 0)
                    {
                        e.Value = "Percent of Selected Rows";
                        e.CellTemplate = this.Resources["UnBoundCellTemplate"] as DataTemplate;
                    }
                    else
                    {
                        if (!(totalSelectedSales.ContainsKey(e.Column.MappingName)))
                            return;

                        e.Value = totalSelectedSales[e.Column.MappingName] * (percentSales[e.Column.MappingName] / 100);
                        e.CellTemplate = this.Resources["UnBoundRowCellTemplate"] as DataTemplate;
                    }
                }
                else if (e.GridUnboundRow.UnBoundRowIndex == 1 && e.GridUnboundRow.Position == UnBoundRowsPosition.Bottom && e.GridUnboundRow.ShowBelowSummary == true)
                {
                    if (e.RowColumnIndex.ColumnIndex == 0)
                    {
                        e.Value = "Summary of Selected Rows";
                        e.CellTemplate = this.Resources["UnBoundCellTemplate"] as DataTemplate;
                    }
                    else
                    {
                        if (!(totalSelectedSales.ContainsKey(e.Column.MappingName)))
                            return;

                        e.Value = totalSelectedSales[e.Column.MappingName];
                        e.CellTemplate = this.Resources["UnBoundRowCellTemplate"] as DataTemplate;
                    }
                }

                else if (e.GridUnboundRow.UnBoundRowIndex == 2 && e.GridUnboundRow.Position == UnBoundRowsPosition.Bottom && e.GridUnboundRow.ShowBelowSummary == true)
                {
                    int count = this.SfDataGrid.SelectedItems.Count;
                    if (e.RowColumnIndex.ColumnIndex == 0)
                    {
                        e.Value = "Average of Selected Rows";
                        e.CellTemplate = this.Resources["UnBoundCellTemplate"] as DataTemplate;
                    }
                    else
                    {
                        if (!(totalSelectedSales.ContainsKey(e.Column.MappingName)))
                            return;

                        e.Value = (totalSelectedSales[e.Column.MappingName] / count);
                        e.Value = double.IsNaN((double)e.Value) ? 0.0 : e.Value;
                        e.CellTemplate = this.Resources["UnBoundRowCellTemplate"] as DataTemplate;

                        if (e.RowColumnIndex.ColumnIndex > 7)
                            e.CellTemplate = null;
                    }
                }
                else if (e.GridUnboundRow.Position == UnBoundRowsPosition.Bottom && e.GridUnboundRow.ShowBelowSummary == false)
                {
                    if (e.RowColumnIndex.ColumnIndex == 0)
                    {
                        e.Value = "Edit the Row to get the Percent of Selected Rows Summary";
                    }
                    else
                    {
                        e.Value = percentSales[e.Column.MappingName];
                        e.EditTemplate = this.Resources["UnBoundRowEditTemplate"] as DataTemplate;
                        e.CellTemplate = this.Resources["UnBoundRowCellPercentTemplate"] as DataTemplate;
                    }
                }
                e.Handled = true;
            }
            else if (e.UnBoundAction == UnBoundActions.CommitData)
            {
                if (e.Value.ToString().Equals(string.Empty))
                    return;
                double data;
                foreach (char character in e.Value.ToString().ToCharArray())
                    if (char.IsLetter(character))
                        return;

                if (e.Value.ToString().Contains("$"))
                    data = Convert.ToDouble(e.Value.ToString().Substring(1, e.Value.ToString().Length - 1));
                else
                    data = Convert.ToDouble(e.Value);
                var value = Convert.ToDouble(data);
                percentSales[e.Column.MappingName] = value;
                e.CellTemplate = this.Resources["UnBoundRowCellTemplate"] as DataTemplate;
            }
        }

        void OnSfDataGridQueryRowHeight(object sender, QueryRowHeightEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                e.Height = 40;
                e.Handled = true;
            }
            // Which customize the height of UnBoundRow.
            else if (this.SfDataGrid.IsUnBoundRow(e.RowIndex))
            {
                e.Height = 50;
                e.Handled = true;
            }
            else
            {
                e.Height = this.SfDataGrid.RowHeight;
                e.Handled = true;
            }
        }

        // Calculates summary value based on column names.
        double GetSummaryValue(string column, bool totalSummary = true)
        {
            double summary = 0.0;
            var view = this.SfDataGrid.View;
            if (this.SfDataGrid.SelectedItems.Count != 0 && !totalSummary)
            {
                foreach (var data in this.SfDataGrid.SelectedItems)
                {
                    if (column.Equals("QS1"))
                        summary += (data as SalesByYear).QS1;
                    else if (column.Equals("QS2"))
                        summary += (data as SalesByYear).QS2;
                    else if (column.Equals("QS3"))
                        summary += (data as SalesByYear).QS3;
                    else if (column.Equals("QS4"))
                        summary += (data as SalesByYear).QS4;
                    else if (column.Equals("Total"))
                        summary += (data as SalesByYear).Total;
                }
            }
            else if (totalSummary)
            {
                foreach (var data in view.Records)
                {
                    if (column.Equals("QS1"))
                        summary += ((data as RecordEntry).Data as SalesByYear).QS1;
                    else if (column.Equals("QS2"))
                        summary += ((data as RecordEntry).Data as SalesByYear).QS2;
                    else if (column.Equals("QS3"))
                        summary += ((data as RecordEntry).Data as SalesByYear).QS3;
                    else if (column.Equals("QS4"))
                        summary += ((data as RecordEntry).Data as SalesByYear).QS4;
                    else if (column.Equals("Total"))
                        summary += ((data as RecordEntry).Data as SalesByYear).Total;
                }
            }
            return summary;
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.SfDataGrid.Loaded -= OnSfDataGridLoaded;            
            this.SfDataGrid.QueryRowHeight -= OnSfDataGridQueryRowHeight;
            this.SfDataGrid.QueryUnBoundRow -= OnSfDataGridQueryUnBoundRow;
            this.SfDataGrid.SelectionChanged -= OnSfDataGridSelectionChanged;
            this.SfDataGrid.CurrentCellEndEdit -= OnSfDataGridCurrentCellEndEdit;
            this.SfDataGrid.Dispose();
            this.SfDataGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
