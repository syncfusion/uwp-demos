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
    public sealed partial class Paging : SampleLayout
    {
        public Paging()
        {
            this.InitializeComponent();
            this.OrientationComboBox.SelectionChanged += OnOrientationChanged;
            var datacontext = new OrderInfoViewModel();
            this.DataContext = datacontext;
            this.sfDataPager.Source = datacontext.OrdersListDetails;           
        }      

        private void OnOrientationChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                if (comboBox.SelectedIndex == 0)
                {
                    Grid.SetRow(sfDataPager, 1);
                    Grid.SetColumn(sfDataPager, 0);
                }
                else
                {
                    Grid.SetRow(sfDataPager, 0);
                    Grid.SetColumn(sfDataPager, 1);
                }
            }
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();           
            if (this.OrientationComboBox != null)
                this.OrientationComboBox.SelectionChanged -= OnOrientationChanged;
            sfGrid.Dispose();
            sfGrid.ItemsSource = null;            
            sfDataPager.Dispose();
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
