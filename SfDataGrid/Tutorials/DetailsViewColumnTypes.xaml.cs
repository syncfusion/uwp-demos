using Common;
using MasterDetailsViewDemo;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DataGrid
{
    public sealed partial class DetailsViewColumnTypes : SampleLayout
    {
        public DetailsViewColumnTypes()
        {
            this.InitializeComponent();
            var datacontext = new DetailsViewViewModel();
            this.DataContext = datacontext;
            this.dataGrid.ItemsSource = datacontext.OrdersDetails;
            this.dataGrid.Loaded += OnSfDataGridLoaded;            
        }       

        /// <summary>
        /// Occurs when the SfDataGrid is laid out, rendered, and ready for interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSfDataGridLoaded(object sender, RoutedEventArgs e)
        {            
            this.dataGrid.ExpandAllDetailsView();
            (this.dataGrid.DetailsViewDefinition[0] as GridViewDefinition).DataGrid.CurrentCellRequestNavigate += DataGrid_CurrentCellRequestNavigate;
            (this.dataGrid.DetailsViewDefinition[0] as GridViewDefinition).DataGrid.CurrentCellEndEdit += DataGrid_CurrentCellEndEdit;

        }

        /// <summary>
        /// Occurs when end edit the current cell in SfDataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DataGrid_CurrentCellEndEdit(object sender, CurrentCellEndEditEventArgs args)
        {
            var visibleColumnIndex = (this.dataGrid.DetailsViewDefinition[0] as GridViewDefinition).DataGrid.ResolveToGridVisibleColumnIndex(args.RowColumnIndex.ColumnIndex);
            var column = (this.dataGrid.DetailsViewDefinition[0] as GridViewDefinition).DataGrid.Columns[visibleColumnIndex];
            if (column.MappingName.Equals("UnitPrice") || column.MappingName.Equals("Discount"))
            {
                (args.OriginalSender as DetailsViewDataGrid).UpdateDataRow(args.RowColumnIndex.RowIndex);
            }
        }

        /// <summary>
        /// Occurs when navigate the hyper link to URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void DataGrid_CurrentCellRequestNavigate(object sender, CurrentCellRequestNavigateEventArgs args)
        {
            string str = "http://en.wikipedia.org/wiki/" + args.NavigateText;
            await Launcher.LaunchUriAsync(new Uri(str));
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            (this.dataGrid.DetailsViewDefinition[0] as GridViewDefinition).DataGrid.CurrentCellRequestNavigate -= DataGrid_CurrentCellRequestNavigate;
            (this.dataGrid.DetailsViewDefinition[0] as GridViewDefinition).DataGrid.CurrentCellEndEdit -= DataGrid_CurrentCellEndEdit;
            this.dataGrid.Loaded -= OnSfDataGridLoaded;            
            this.dataGrid.Dispose();
            this.dataGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
