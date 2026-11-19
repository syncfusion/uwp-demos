using Common;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DataGrid
{
    public sealed partial class CustomValidations : SampleLayout
    {
        public CustomValidations()
        {
            this.InitializeComponent();
            var datacontext = new OrderInfoViewModel();
            this.DataContext = datacontext;
            this.sfGrid.ItemsSource = datacontext.OrdersListDetails;      
        }
        
        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {          
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }

        /// <summary>
        /// Occurs when the CurrentCellValidating of SfDataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnSfDataGridCurrentCellValidating(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs args)
        {
            if (args.Column.MappingName == "Discount" && (args.NewValue == null || string.IsNullOrEmpty(args.NewValue.ToString()) || Convert.ToDouble(args.NewValue) > 40))
            {
                args.ErrorMessage = "Discount should not exceed 40 percent.";
                args.IsValid = false;
            }
        }

        /// <summary>
        /// Occurs when the RowValidating of SfDataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnSfDataGridRowValidating(object sender, Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs args)
        {
            var data = args.RowData as OrderInfo;
            if ((data.Expense + data.Freight) < 3000)
            {
                args.ErrorMessages.Add("Expense", "Sum of Expense and Freight should be a minimum of 3000 to be eligible for Discount.");
                args.IsValid = false;
            }
        }

    }
}
