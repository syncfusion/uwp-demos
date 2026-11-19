using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class AddNewRow : SampleLayout
    {
        public AddNewRow()
        {
            this.InitializeComponent();
            var datacontext = new OrderInfoViewModel();
            this.DataContext = datacontext;
            this.sfGrid.ItemsSource = datacontext.OrdersListDetails;
            this.sfGrid.RowValidating += OnRowValidating;           
        }

        /// <summary>
        /// Method to handle the Row Validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnRowValidating(object sender, Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs args)
        {
            if (args.RowData != null && (args.RowData as OrderInfo).OrderID <= 0)
            {
                args.ErrorMessages.Add("OrderID", "Order Id field should not contain null or minimum value.");
                args.IsValid = false;
            }
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();          
            this.sfGrid.RowValidating -= OnRowValidating;
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }     
    }
}
