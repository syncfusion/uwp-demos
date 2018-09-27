using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.UI.Input;
using Windows.Foundation.Metadata;
using Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public sealed partial class StockAnalysis : Page, IDisposable
    {
        public List<StockDatas> selectedDatas { get; set; }

        public List<StockDatas> SelectedData { get; set; }

        public StockAnalysis()
        {
            this.InitializeComponent();
            this.Unloaded += StockAnalysis_Unloaded1;
            SelectedData = new List<StockDatas>();

            if (IsMobileFamily())
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
        }

        private void StockAnalysis_Unloaded1(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private bool IsMobileFamily()
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                return true;
            }
            return false;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            FrameNavigationService.GoBack();
            if (IsMobileFamily())
            {
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            }
        }

        public void Dispose()
        {
            if(this.tileView!=null)
                this.tileView.Dispose();
            this.Resources = null;
        }

        private void GridLayout(object sender, RoutedEventArgs e)
        {
            StockViewModels svm = this.tileView.DataContext as StockViewModels;
            svm.GridVisibility = Visibility.Visible;
            svm.ChartVisibility = Visibility.Collapsed;
        }

        private void TileLayout(object sender, RoutedEventArgs e)
        {
            StockViewModels svm = this.tileView.DataContext as StockViewModels;
            svm.GridVisibility = Visibility.Collapsed;
            svm.ChartVisibility = Visibility.Visible;
        }

        private void Selecter_SelectionChanged(object sender, global::Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Stocks data = e.AddedItems[0] as Stocks;
                selectedDatas = data.Datas;
                FrameNavigationService.CurrentFrame.Navigate(typeof(Chart), selectedDatas);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.Content is Chart)
            {
                Chart currentDetails = e.Content as Chart;
                if (selectedDatas != null)
                    currentDetails.GenerateData(selectedDatas);
            }
            base.OnNavigatedFrom(e);
        }
    }

    public class CustomTemplateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var obj = value as StockDatas;
            return obj != null && obj.Open > obj.Last ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
