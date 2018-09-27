using Common;
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
    public sealed partial class DropDownAndReadOnlyColumns : SampleLayout
    {
        public DropDownAndReadOnlyColumns()
        {
            this.InitializeComponent();
            var datacontext = new OrderInfoViewModel();
            this.DataContext = datacontext;
            this.sfGrid.ItemsSource = datacontext.OrdersListDetails;
            this.sfGrid.CurrentCellRequestNavigate += OnSfDataGridCurrentCellRequestNavigate;
        }   

        async void OnSfDataGridCurrentCellRequestNavigate(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellRequestNavigateEventArgs args)
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
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
