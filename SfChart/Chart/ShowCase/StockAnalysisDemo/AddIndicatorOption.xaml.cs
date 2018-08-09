using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.System.Profile;
using Windows.Foundation.Metadata;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public sealed partial class AddIndicatorOption : Page, IDisposable
    {
        public List<string> IndicatorList { get; set; }
        public List<StockDatas> SelectedData { get; set; }
        public AddIndicatorOption()
        {
            this.DataContext = this;
            this.Unloaded += AddIndicatorOption_Unloaded;
            IndicatorList = new List<string>();
            IndicatorList.Add("Accumulation Distribution");
            IndicatorList.Add("Average True Range");
            IndicatorList.Add("BollingerBand");
            IndicatorList.Add("Exponential Average");

            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                IndicatorList.Add("MACDTechnical");

            IndicatorList.Add("Momentum Technical");
            IndicatorList.Add("RSITechnical");
            IndicatorList.Add("Simple Average");
            IndicatorList.Add("Triangular Average");

            this.InitializeComponent();

            //if (IsMobileFamily())
            //{
            //    HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            //}
        }

        private void AddIndicatorOption_Unloaded(object sender, RoutedEventArgs e)
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

        //private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        //{
        //    e.Handled = true;
        //    if (Frame != null)
        //        Frame.Navigate(typeof(Chart), SelectedData);
        //    //if (IsMobileFamily())
        //    //{
        //    //    HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        //    //}
        //}

        Chart view;
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            view = e.Content as Chart;
            if (view != null)
            {
                if (selecter.SelectedItem != null)
                {
                    view.SelectedIndicator = this.selecter.SelectedItem.ToString();
                    base.OnNavigatedFrom(e);
                }
                view.SelectedData = SelectedData;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SelectedData = e.Parameter as List<StockDatas>;
            base.OnNavigatedTo(e);
        }
        private void ListPicker_SelectionChanged_1(object sender, global::Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(Chart));
        }

        public void Dispose()
        {
            selecter.SelectionChanged -= ListPicker_SelectionChanged_1;
            if (IndicatorList != null)
                IndicatorList.Clear();
            if (this.DataContext != null)
                this.DataContext = null;
        }
    }
}
