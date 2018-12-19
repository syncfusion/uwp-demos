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
    public sealed partial class TradingGrid : SampleLayout
    {
        public TradingGrid()
        {
            this.InitializeComponent();
            var datacontext = new StocksViewModel();
            this.DataContext = datacontext;            
            this.sfGrid.ItemsSource = datacontext.Stocks;
            this.sfGrid.Loaded += OnSfDataGridLoaded;
            this.sfGrid.Unloaded += OnSfDataGridUnloaded;
        }


        /// <summary>
        /// Occurs when the SfDataGrid is laid out, rendered, and ready for interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSfDataGridLoaded(object sender, RoutedEventArgs e)
        {            
            (this.DataContext as StocksViewModel).StartTimer();
        }

        /// <summary>
        /// Occurs when the SfDataGrid is removed from within an element tree of loaded elements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSfDataGridUnloaded(object sender, RoutedEventArgs e)
        {           
            if (this.DataContext != null)
                (this.DataContext as StocksViewModel).StopTimer();            
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.sfGrid.Loaded -= OnSfDataGridLoaded;
            this.sfGrid.Unloaded -= OnSfDataGridUnloaded;
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
