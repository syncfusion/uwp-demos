using Common;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DataGrid
{
    public sealed partial class IntervalGroupingDemo : SampleLayout
    {
        public IntervalGroupingDemo()
        {
            this.InitializeComponent();
            var datacontext = new OrderInfoViewModel();
            this.DataContext = datacontext;
            this.sfGrid.ItemsSource = datacontext.OrdersListDetails;          
            this.sfGrid.GroupColumnDescriptions.CollectionChanged += GroupColumnDescriptions_CollectionChanged;            
        }

        /// <summary>
        /// Occurs when the GroupColumnDescription is changed. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupColumnDescriptions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                var groupDesc = e.NewItems[0] as GroupColumnDescription;
                if (groupDesc.ColumnName == "OrderDate")
                {
                    groupDesc.Converter = new GridDateTimeRangeConverter() { GroupMode = DateGroupingMode.Range };
                    groupDesc.Comparer = new CustomGroupDateTimeComparer() { GroupMode = DateGroupingMode.Range };
                }
                if (groupDesc.ColumnName == "OrderID")
                {
                    groupDesc.Converter = new GridNumericRangeConverter() { GroupInterval = 10 };
                    groupDesc.Comparer = new CustomGroupNumericComparer();
                }
                if (groupDesc.ColumnName == "CustomerID")
                {
                    groupDesc.Converter = new GridTextRangeConverter();
                    groupDesc.Comparer = new CustomGroupTextComparer();
                }
                if (groupDesc.ColumnName == "Shipping")
                {
                    groupDesc.Converter = new GridDateTimeRangeConverter() { GroupMode = DateGroupingMode.Month };
                    groupDesc.Comparer = new CustomGroupDateTimeComparer() { GroupMode = DateGroupingMode.Month };
                }
                if (groupDesc.ColumnName == "Freight")
                {
                    groupDesc.Converter = new GridNumericRangeConverter() { GroupInterval = 10 };
                    groupDesc.Comparer = new CustomGroupNumericComparer();
                }
                if (groupDesc.ColumnName == "ShipCountry")
                {
                    groupDesc.Converter = new GridTextRangeConverter();
                    groupDesc.Comparer = new CustomGroupTextComparer();
                }
            }
        }
               
        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.sfGrid.GroupColumnDescriptions.CollectionChanged -= GroupColumnDescriptions_CollectionChanged;           
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
        
    }
}
