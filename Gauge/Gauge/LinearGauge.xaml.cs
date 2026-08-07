using Common;
using Syncfusion.UI.Xaml.Gauges;
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

namespace GaugeUWP_Samples
{
    public sealed partial class LinearGauge : SampleLayout,IDisposable
    {
        public LinearGauge()
        {
            InitializeComponent();



            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                this.LinearScale1.ScaleBarLength = 450;
            }

            cmb_RangePosition.ItemsSource = Enum.GetValues(typeof(LinearRangesPosition));
            cmb_TickPosition.ItemsSource = Enum.GetValues(typeof(LinearTicksPosition));
            cmb_LabelPosition.ItemsSource = Enum.GetValues(typeof(LinearLabelsPosition));
        }

        private void samplelayout_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                grid.Height = (sender as LinearGauge).ActualHeight;
                grid.Width = (sender as LinearGauge).ActualWidth;
            }
        
        }
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (this.lineargauge.Visibility == Visibility.Visible)
        //    {
        //        this.lineargauge.Visibility = Visibility.Collapsed;
        //        this.option.Visibility = Visibility.Visible;
        //        (sender as Button).Content = "Done";
        //    }
        //    else
        //    {
        //        this.lineargauge.Visibility = Visibility.Visible;
        //        this.option.Visibility = Visibility.Collapsed;
        //        (sender as Button).Content = "Options";
        //    }
        //}

        public override void Dispose()
        {
            this.SfLinearGauge1.Dispose();
            this.SfLinearGauge1 = null;
            this.LinearScale1 = null;
        }
    }
}
