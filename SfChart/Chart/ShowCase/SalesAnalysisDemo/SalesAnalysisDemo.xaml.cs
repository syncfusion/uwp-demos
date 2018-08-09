using Syncfusion.UI.Xaml.Charts;
using Syncfusion.UI.Xaml.Controls.Notification;
using Syncfusion.UI.Xaml.Gauges;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System.Profile;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Input;
using Windows.Phone.UI.Input;
using Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SalesAnalysisDemo : Page, IDisposable
    {
        UI.Xaml.Charts.SfChart revenueChart;
        UI.Xaml.Charts.SfChart chart1;
        SfCircularGauge gauge1;
        SfCircularGauge gauge2;
        SfCircularGauge gauge3;
        SfCircularGauge gauge4;
        SfDateTimeRangeNavigator rangeNavigator;
        public SalesAnalysisDemo()
        {
            this.InitializeComponent();
            this.Unloaded += SalesAnalysisDemo_Unloaded;

            revenueChart = grid.FindName("revenueChart") as UI.Xaml.Charts.SfChart;
            revenueChart.SelectionChanged += RevenueChart_SelectionChanged;
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                chart1 = chart.FindName("sfchart") as UI.Xaml.Charts.SfChart;
                gauge1 = gauge.FindName("SalesGauge") as SfCircularGauge;
                gauge2 = gauge.FindName("SalesGauge2") as SfCircularGauge;
                gauge3 = gauge.FindName("SalesGauge3") as SfCircularGauge;
                gauge4 = gauge.FindName("SalesGauge4") as SfCircularGauge;
                revenueChart.Loaded += dataGrid_Loaded;
            }

            rangeNavigator = navigator.FindName("chart_navigator") as SfDateTimeRangeNavigator;
            rangeNavigator.Loaded += RangeNavigator_Loaded;
            rangeNavigator.ValueChanged += rangeNavigator_ValueChanged;
        }

        private void SalesAnalysisDemo_Unloaded(object sender, RoutedEventArgs e)
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

        private void RevenueChart_SelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            chart1.DataContext = new SalesByCountry();

            gauge1.Scales[0].Pointers[0].Value = (chart1.Series[0].ItemsSource as ObservableCollection<SalesByCountry>)[2].Sales;
            gauge2.Scales[0].Pointers[0].Value = (chart1.Series[0].ItemsSource as ObservableCollection<SalesByCountry>)[1].Sales;
            gauge3.Scales[0].Pointers[0].Value = (chart1.Series[0].ItemsSource as ObservableCollection<SalesByCountry>)[3].Sales;
            gauge4.Scales[0].Pointers[0].Value = (chart1.Series[0].ItemsSource as ObservableCollection<SalesByCountry>)[4].Sales;
        }

        private void RangeNavigator_Loaded(object sender, RoutedEventArgs e)
        {
            revenueChart.PrimaryAxis.ZoomPosition = rangeNavigator.ZoomPosition;
            revenueChart.PrimaryAxis.ZoomFactor = rangeNavigator.ZoomFactor;
        }

        //void dataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        //{
        //    chart1.DataContext = new SalesByCountry();
        //    gauge1.Scales[0].Pointers[0].Value = (chart1.Series[0].ItemsSource as ObservableCollection<SalesByCountry>)[2].Sales;
        //    gauge2.Scales[0].Pointers[0].Value = (chart1.Series[0].ItemsSource as ObservableCollection<SalesByCountry>)[1].Sales;
        //    gauge3.Scales[0].Pointers[0].Value = (chart1.Series[0].ItemsSource as ObservableCollection<SalesByCountry>)[3].Sales;
        //    gauge4.Scales[0].Pointers[0].Value = (chart1.Series[0].ItemsSource as ObservableCollection<SalesByCountry>)[4].Sales;
        //}

        void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            gauge1.Scales[0].Pointers[0].Value = (chart1.Series[0].ItemsSource as ObservableCollection<SalesByCountry>)[2].Sales;
            gauge2.Scales[0].Pointers[0].Value = (chart1.Series[0].ItemsSource as ObservableCollection<SalesByCountry>)[1].Sales;
            gauge3.Scales[0].Pointers[0].Value = (chart1.Series[0].ItemsSource as ObservableCollection<SalesByCountry>)[3].Sales;
            gauge4.Scales[0].Pointers[0].Value = (chart1.Series[0].ItemsSource as ObservableCollection<SalesByCountry>)[4].Sales;
        }

        private void rangeNavigator_ValueChanged(object sender, EventArgs e)
        {
            revenueChart.PrimaryAxis.ZoomFactor = (sender as SfDateTimeRangeNavigator).ZoomFactor;
            revenueChart.PrimaryAxis.ZoomPosition = (sender as SfDateTimeRangeNavigator).ZoomPosition;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>

        private void navigator_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            scrollviewer.VerticalScrollMode = ScrollMode.Disabled;
            scrollviewer.HorizontalScrollMode = ScrollMode.Disabled;
        }

        private void navigator_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            scrollviewer.VerticalScrollMode = ScrollMode.Enabled;
            scrollviewer.HorizontalScrollMode = ScrollMode.Enabled;
        }

        public void Dispose()
        {
            if (this.grid != null)
                this.grid = null;
            if (this.navigator != null)
                this.navigator = null;
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                revenueChart.Loaded -= dataGrid_Loaded;
            if (rangeNavigator != null)
            {
                rangeNavigator.Loaded -= RangeNavigator_Loaded;
                rangeNavigator.ValueChanged -= rangeNavigator_ValueChanged;
                revenueChart.SelectionChanged -= RevenueChart_SelectionChanged;
            }
            this.Unloaded -= SalesAnalysisDemo_Unloaded;
            this.Resources = null;
        }
    }
}
