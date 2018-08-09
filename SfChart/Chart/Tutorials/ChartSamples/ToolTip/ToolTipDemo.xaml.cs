using Common;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public sealed partial class ToolTipDemo : SampleLayout
    {
        public ToolTipDemo()
        {
            CompanyDetails = new ObservableCollection<CompanyDetail1>();
            CompanyDetails.Add(new CompanyDetail1()
            {
                CompanyName = "Mercedes",
                YTD = 983.502,
                Rank = 16,
                Imagepath = new Uri("ms-appx:///Chart/Tutorials/ChartSamples/ToolTip/Images/benz.png", UriKind.RelativeOrAbsolute)
            });
            CompanyDetails.Add(new CompanyDetail1()
            {
                CompanyName = "Audi",
                YTD = 1030.393,
                Rank = 12,
                Imagepath = new Uri("ms-appx:///Chart/Tutorials/ChartSamples/ToolTip/Images/audi.png", UriKind.RelativeOrAbsolute)
            });
            CompanyDetails.Add(new CompanyDetail1()
            {
                CompanyName = "BMW",
                YTD = 1061.330,
                Rank = 11,
                Imagepath = new Uri("ms-appx:///Chart/Tutorials/ChartSamples/ToolTip/Images/bmw.png", UriKind.RelativeOrAbsolute)
            });
            CompanyDetails.Add(new CompanyDetail1()
            {
                CompanyName = "Skoda",
                YTD = 590.897,
                Rank = 24,
                Imagepath = new Uri("ms-appx:///Chart/Tutorials/ChartSamples/ToolTip/Images/skoda.png", UriKind.RelativeOrAbsolute)
            });
            CompanyDetails.Add(new CompanyDetail1()
            {
                CompanyName = "Volvo",
                YTD = 250.758,
                Rank = 43,
                Imagepath = new Uri("ms-appx:///Chart/Tutorials/ChartSamples/ToolTip/Images/volvo.png", UriKind.RelativeOrAbsolute)
            });
            this.InitializeComponent();
            this.DataContext = CompanyDetails;
        }
        public ObservableCollection<CompanyDetail1> CompanyDetails
        {
            get;
            set;
        }

        private void OnHorizontalAlignChange(object sender, SelectionChangedEventArgs e)
        {
            if (tooltipChart == null) return;

            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    tooltipChart.Series[0].SetValue(ChartTooltip.HorizontalAlignmentProperty, HorizontalAlignment.Left);
                    break;

                case 1:
                    tooltipChart.Series[0].SetValue(ChartTooltip.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                    break;

                case 2:
                    tooltipChart.Series[0].SetValue(ChartTooltip.HorizontalAlignmentProperty, HorizontalAlignment.Right);
                    break;
            }
        }

        private void OnVerticalAlignChange(object sender, SelectionChangedEventArgs e)
        {
            if (tooltipChart == null) return;

            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    tooltipChart.Series[0].SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                    break;

                case 1:
                    tooltipChart.Series[0].SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Center);
                    break;

                case 2:
                    tooltipChart.Series[0].SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Bottom);
                    break;
            }
        }

        private void OnHorizontalOffsetChange(Object sender, TextChangedEventArgs e)
        {
            double i = 0d;
            var text = (sender as TextBox).Text.ToString();
            if (double.TryParse(text, out i))
            {
                tooltipChart.Series[0].SetValue(ChartTooltip.HorizontalOffsetProperty, Convert.ToDouble(text));
            }
        }

        private void OnVerticalOffsetChange(Object sender, TextChangedEventArgs e)
        {
            double i = 0d;
            var text = (sender as TextBox).Text.ToString();
            if (double.TryParse(text, out i))
            {
                tooltipChart.Series[0].SetValue(ChartTooltip.VerticalOffsetProperty, Convert.ToDouble(text));
            }
        }

        private void OnInitialDelaySliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (tooltipChart != null && tooltipChart.Series != null)
                tooltipChart.Series[0].SetValue(ChartTooltip.InitialShowDelayProperty, Convert.ToInt32(e.NewValue));
        }

        private void OnShowDurationSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (tooltipChart != null && tooltipChart.Series != null)
                tooltipChart.Series[0].SetValue(ChartTooltip.ShowDurationProperty, Convert.ToInt32(e.NewValue));
        }

        public override void Dispose()
        {
            if (this.tooltipChart != null)
            {
                foreach (var series in this.tooltipChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.tooltipChart = null;
            }

            this.grid.Resources = null;

            base.Dispose();
        }
    }

    public class CompanyDetail1
    {
        public Uri Imagepath
        {
            get;
            set;
        }

        public string CompanyName
        {
            get;
            set;
        }

        public double YTD
        {
            get;
            set;
        }

        public int Rank
        {
            get;
            set;
        }
    }
}
