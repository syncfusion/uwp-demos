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
    public sealed partial class CustomPrinting : SampleLayout
    {
        public CustomPrinting()
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
            sfGrid.Dispose();
            sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }

        private void OnPrintBtnClick(object sender, RoutedEventArgs e)
        {
            if (sfGrid != null)
            {
                this.sfGrid.PrintSettings.PrintManagerBase = new CustomPrintManager(sfGrid);
                this.sfGrid.PrintSettings.PrintManagerBase.Print();
            }
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
    }
}
