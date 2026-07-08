using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ExpenseAnalysisDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page, IDisposable
    {
        public HomePage()
        {
            this.InitializeComponent();
            ViewModel vm = new ViewModel();
            vm.ExportAction = ExportData;
            this.DataContext = vm;
            this.Unloaded += HomePage_Unloaded;
        }

        private void HomePage_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void ExportData()
        {
            if (gridView == null)
                return;            
            
                gridView.Export();
            
        }

        public void Dispose()
        {
            this.Resources.Clear();
            this.Unloaded -= HomePage_Unloaded;
            if (this.gridView != null)
                this.gridView.Dispose();
            if (this.chartView != null)
                this.chartView.Dispose();
            if (this.transactionDetailsView != null)
                this.transactionDetailsView.Dispose();
            if (this.personInfoView != null)
                this.personInfoView.Dispose();
            if (DataContext != null)
                (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            this.Content = null;
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                gridView = new GridView();
                gridContentHolder.Content = gridView;

            });
        }        


        private void LockButton_Click(object sender, RoutedEventArgs e)
        {
            //mainPivot.IsLocked = !mainPivot.IsLocked;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
        }

        private void mainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                //PivotItem currentItem = e.AddedItems[0] as PivotItem;
                //if (currentItem.Header.ToString() == "Overview")
                //    exportButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                //else
                //    exportButton.Visibility = Windows.UI.Xaml.Visibility.Visible;

        }
    }
}
