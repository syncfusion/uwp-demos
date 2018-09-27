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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DataGrid
{
    public sealed partial class ListBinding : UserControl
    {
        public ListBinding()
        {
            this.InitializeComponent();
            var datacontext = new DataBoundViewModel();
            this.syncgrid.ItemsSource = datacontext.OrdersListDetails;            
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.syncgrid.ItemsSource = null;
            this.syncgrid.Dispose();
            if (this.DataContext != null)
                (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
        }
    }
}
