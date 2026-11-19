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

namespace Syncfusion.SampleBrowser.UWP.SfTreeGrid
{
    public sealed partial class SelectionDemo : SampleLayout , IDisposable
    {
        public SelectionDemo()
        {
            this.InitializeComponent();
            this.treeGrid.Loaded += TreeGrid_Loaded;
            this.treeGrid.Unloaded += TreeGrid_Unloaded;
        }

        private void TreeGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            this.treeGrid.SelectedItem = null;
        }
        private void TreeGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var items = (this.DataContext as SelectionViewModel).EmployeeInfo;
            this.treeGrid.SelectedItem = items[0];
        }
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.treeGrid.Loaded -= TreeGrid_Loaded;
            this.treeGrid.Unloaded -= TreeGrid_Unloaded;
            this.treeGrid.ItemsSource = null;
            this.treeGrid.Dispose();
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
