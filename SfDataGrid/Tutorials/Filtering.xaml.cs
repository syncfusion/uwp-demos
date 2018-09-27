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
    public sealed partial class Filtering : SampleLayout
    {
        public Filtering()
        {
            this.InitializeComponent();
            var datacontext = new EmployeeInfoViewModel();
            this.DataContext = datacontext;
            this.sfGrid.ItemsSource = datacontext.EmployeeDetails;
            this.Loaded += FilteringLoaded;            
        }        

        /// <summary>
        /// Occurs when the Filter is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilteringLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as EmployeeInfoViewModel;
            viewModel.filterChanged = OnFilterChanged;
        }

        /// <summary>
        /// Occurs when the Filter value is changed
        /// </summary>
        private void OnFilterChanged()
        {
            var viewModel = this.DataContext as EmployeeInfoViewModel;
            if (this.sfGrid.View != null)
            {
                this.sfGrid.View.Filter = viewModel.FilerRecords;
                this.sfGrid.View.RefreshFilter();
            }
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();           
            this.Loaded -= FilteringLoaded;
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
