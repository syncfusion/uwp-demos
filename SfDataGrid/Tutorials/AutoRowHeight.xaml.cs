using Common;
using DataGrid;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DataGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AutoRowHeight : SampleLayout
    {
        GridRowSizingOptions gridRowResizingOptions = new GridRowSizingOptions();
        List<string> excludeColumns = new List<string>();
        double autoHeight = double.NaN;

        public AutoRowHeight()
        {
            this.InitializeComponent();
            var datacontext = new CustomerInfoViewModel();
            this.DataContext = datacontext;
            this.sfGrid.ItemsSource = datacontext.CustomerDetails;
            this.sfGrid.ItemsSourceChanged += OnSfDataGridItemsSourceChanged;
            this.sfGrid.QueryRowHeight += OnSfDataGridQueryRowHeight;          
        }
     

        /// <summary>
        /// Occurs when the row height is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSfDataGridQueryRowHeight(object sender, QueryRowHeightEventArgs e)
        {
            if (this.sfGrid.IsTableSummaryIndex(e.RowIndex))
            {
                e.Height = 40;
                e.Handled = true;
            }
            else if (this.sfGrid.GridColumnSizer.GetAutoRowHeight(e.RowIndex, gridRowResizingOptions, out autoHeight))
            {
                if (autoHeight > this.sfGrid.RowHeight)
                {
                    e.Height = autoHeight;
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Occurs when the collection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSfDataGridItemsSourceChanged(object sender, GridItemsSourceChangedEventArgs e)
        {
            foreach (var column in this.sfGrid.Columns)
                if (!column.MappingName.Equals("Address") && !column.MappingName.Equals("CompanyName"))
                    excludeColumns.Add(column.MappingName);

            gridRowResizingOptions.ExcludeColumns = excludeColumns;
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.sfGrid.ItemsSourceChanged -= OnSfDataGridItemsSourceChanged;
            this.sfGrid.QueryRowHeight -= OnSfDataGridQueryRowHeight;         
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
