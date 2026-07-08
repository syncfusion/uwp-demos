using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Popups;
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
    public sealed partial class Printing : SampleLayout
    {
        public Printing()
        {
            this.InitializeComponent();
            var datacontext = new CountryInfoViewModel();
            this.DataContext = datacontext;
            this.sfGrid.ItemsSource = datacontext.CountryDetails;            
        }
      
        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();            
            this.sfGrid.Dispose();
            sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }

        private void OnPrintBtnClick(object sender, RoutedEventArgs e)
        {
            if (sfGrid != null)
                this.sfGrid.Print();
        }

        private void OnAllowFitColumnsChecked(object sender, RoutedEventArgs e)
        {
            if (sfGrid != null)
                this.sfGrid.PrintSettings.AllowColumnWidthFitToPrintPage = true;
        }

        private void OnAllowFitColumnsUnChecked(object sender, RoutedEventArgs e)
        {
            if (sfGrid != null)
                this.sfGrid.PrintSettings.AllowColumnWidthFitToPrintPage = false;
        }

        private void OnAllowRepeatHeaderChecked(object sender, RoutedEventArgs e)
        {
            if (sfGrid != null)
                this.sfGrid.PrintSettings.AllowRepeatHeaders = true;
        }

        private void OnAllowRepeatHeaderUnChecked(object sender, RoutedEventArgs e)
        {
            if (sfGrid != null)
                this.sfGrid.PrintSettings.AllowRepeatHeaders = false;
        }

        private void CanPrintStackedHeaderChecked(object sender, RoutedEventArgs e)
        {
            if (sfGrid != null)
                this.sfGrid.PrintSettings.CanPrintStackedHeaders = true;
        }

        private void CanPrintStackedHeaderUnChecked(object sender, RoutedEventArgs e)
        {
            if (sfGrid != null)
                this.sfGrid.PrintSettings.CanPrintStackedHeaders = false;
        }
    }
}
