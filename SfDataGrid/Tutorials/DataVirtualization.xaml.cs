using Common;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public sealed partial class DataVirtualization : SampleLayout
    {
        EmployeeInfoRepository repository;
        int itemCount = 0;
        public DataVirtualization()
        {
            this.InitializeComponent();
            this.Loaded += DataVirtualization_Loaded;
            this.Unloaded += DataVirtualization_Unloaded;
            repository = new EmployeeInfoRepository();
        }

        /// <summary>
        /// Occurs when the SfDataGrid is removed from within an element tree of loaded elements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataVirtualization_Unloaded(object sender, RoutedEventArgs e)
        {
            if (employees != null)
            {
                this.employees.Clear();
                this.syncGrid.ItemsSource = null;
                this.employees = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// Occurs when the SfDataGrid is laid out, rendered, and ready for interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataVirtualization_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadView.Visibility = Visibility.Visible;
            this.GridView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.syncGrid.Dispose();
            this.syncGrid.ItemsSource = null;
            base.Dispose();
        }
        ObservableCollection<Employees> employees;

        private void Item1_Checked(object sender, RoutedEventArgs e)
        {
            if (Item2 != null && Item3 != null)
            {
                Item2.IsChecked = false;
                Item3.IsChecked = false;
            }
            itemCount = 100000;
        }

        private void Item2_Checked(object sender, RoutedEventArgs e)
        {
            Item1.IsChecked = false;
            Item3.IsChecked = false;
            itemCount = 300000;
        }

        private void Item3_Checked(object sender, RoutedEventArgs e)
        {
            Item1.IsChecked = false;
            Item2.IsChecked = false;
            itemCount = 500000;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.LoadView.Visibility = Visibility.Collapsed;
            progrssBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            await Task.Delay(50);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            employees = repository.GetEmployeesDetails(itemCount);
            watch.Stop();
            PopulationTime.Text = watch.ElapsedMilliseconds.ToString() + " ms";
            watch.Reset();
            watch.Start();
            this.syncGrid.ItemsSource = employees;
            watch.Stop();
            LoadingTime.Text = watch.ElapsedMilliseconds.ToString() + " ms";            
            GridView.Visibility = Visibility.Visible;
            progrssBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        
    }
}
