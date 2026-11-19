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
    public sealed partial class Performance : SampleLayout
    {
        public Performance()
        {
            this.InitializeComponent();
            this.DataContext = new PerformanceStocksViewModel();
        }

        private void OnSfDataGridLoaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as PerformanceStocksViewModel).StartTimer();
        }

        private void OnSfDataGridUnloaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
                (this.DataContext as PerformanceStocksViewModel).StopTimer();
        }

        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.datagrid.Loaded -= OnSfDataGridLoaded;
            this.datagrid.Unloaded -= OnSfDataGridUnloaded;
            this.datagrid.Dispose();
            this.datagrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
