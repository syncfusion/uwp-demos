using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    public sealed partial class Styling : SampleLayout
    {
        public Styling()
        {
            this.InitializeComponent();
            var datacontext = new CountryInfoViewModel();
            this.DataContext = datacontext;
            this.sfGrid.ItemsSource = datacontext.CountryDetails;
            this.sfGrid.Loaded += OnSfDataGridLoaded;
            this.sfGrid.Unloaded += OnSfDataGridUnloaded;
            this.Unloaded += Styling_Unloaded;
        }

        /// <summary>
        /// Occurs when the SfDataGrid is laid out, rendered, and ready for interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnSfDataGridLoaded(object sender, RoutedEventArgs e)
        {           
            var  countryvm = (this.DataContext as CountryInfoViewModel);
            var r = new Random();
            for (int i = 0; i < countryvm.CountryDetails.Count; i++)
            {
                var item = countryvm.CountryDetails[i];
                var economy = new ObservableCollection<EconomyGrowth>();
                for (int j = 0; j < 5; j++)
                {
                    economy.Add(new EconomyGrowth() { Year = DateTime.Now.Year - j, GrowthPercentage = r.Next(99) });
                }

                //await Task.Delay(100);
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    item.EconomyRate = economy;
                });
            }
        }

        /// <summary>
        /// Occurs when the SfDataGrid is removed from within an element tree of loaded elements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSfDataGridUnloaded(object sender, RoutedEventArgs e)
        {
            this.sfGrid.ItemsSource = null;
        }

        void Styling_Unloaded(object sender, RoutedEventArgs e)
        {
            this.sfGrid.Loaded -= OnSfDataGridLoaded;
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.sfGrid.Loaded += OnSfDataGridLoaded;
            this.sfGrid.Unloaded += OnSfDataGridUnloaded;
            this.Unloaded -= Styling_Unloaded;
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
