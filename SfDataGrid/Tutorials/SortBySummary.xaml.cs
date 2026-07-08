using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataGrid;
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
    public sealed partial class SortBySummary : SampleLayout
    {
        public SortBySummary()
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
            this.Resources.Clear();        
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }

        public void AvgAggreGate_Checked(object sender, RoutedEventArgs e)
        {
            if (this.sfGrid != null)
                this.sfGrid.SummaryGroupComparer = new AvgAggregateGroupComparer();
        }

        public void SumAggreGate_Checked(object sender, RoutedEventArgs e)
        {
            if (this.sfGrid != null)
                this.sfGrid.SummaryGroupComparer = new SumAggregateGroupComparer();
        }
    }
}
