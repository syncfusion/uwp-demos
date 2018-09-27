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
using Windows.UI;
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
    public sealed partial class TrendLineDemo : SampleLayout
    {
        public TrendLineDemo()
        {
            this.InitializeComponent();
        }

        private void OnTrendLineTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;
            if (combo == null) return;
            if (sfchart != null)
            {
                if (select.SelectedIndex == 0)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].Type = TrendlineType.Linear;
                    combo.IsEnabled = false;
                }
                else if (select.SelectedIndex == 1)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].Type = TrendlineType.Exponential;
                    combo.IsEnabled = false;
                }
                else if (select.SelectedIndex == 2)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].Type = TrendlineType.Power;
                    combo.IsEnabled = false;
                }
                else if (select.SelectedIndex == 3)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].Type = TrendlineType.Logarithmic;
                    combo.IsEnabled = false;
                }
                else if (select.SelectedIndex == 4)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].Type = TrendlineType.Polynomial;
                    combo.IsEnabled = true;
                }
            }
        }

        private void OnPolynimialOrderSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;

            if (sfchart != null)
            {
                if (select.SelectedIndex == 0)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].PolynomialOrder = 2;
                }
                else if (select.SelectedIndex == 1)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].PolynomialOrder = 3;
                }
                else if (select.SelectedIndex == 2)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].PolynomialOrder = 4;
                }
                else if (select.SelectedIndex == 3)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].PolynomialOrder = 5;
                }
                else if (select.SelectedIndex == 4)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].PolynomialOrder = 6;
                }
            }
        }

        private void OnStrokeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;

            if (sfchart != null)
            {
                if (select.SelectedIndex == 0)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].Stroke = new SolidColorBrush(Colors.Blue);
                }
                else if (select.SelectedIndex == 1)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].Stroke = new SolidColorBrush(Colors.Green);
                }
                else if (select.SelectedIndex == 2)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].Stroke = new SolidColorBrush(Colors.Orange);
                }
                else if (select.SelectedIndex == 3)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].Stroke = new SolidColorBrush(Colors.Purple);
                }
                else if (select.SelectedIndex == 4)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].Stroke = new SolidColorBrush(Colors.RoyalBlue);
                }
            }
        }

        private void OnStrokeDashArraySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;

            if (sfchart != null)
            {
                if (select.SelectedIndex == 0)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].StrokeDashArray = new DoubleCollection() { 1, 1 };
                }
                else if (select.SelectedIndex == 1)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].StrokeDashArray = new DoubleCollection() { 4, 4 };
                }
                else if (select.SelectedIndex == 2)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].StrokeDashArray = new DoubleCollection() { 4, 8 };
                }
                else if (select.SelectedIndex == 3)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].StrokeDashArray = new DoubleCollection() { 4, 2 };
                }
                else if (select.SelectedIndex == 4)
                {
                    (sfchart.Series[0] as FastLineSeries).Trendlines[0].StrokeDashArray = new DoubleCollection() { 1, 0 };
                }
            }
        }
        private void OnBackwardValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;
            (sfchart.Series[0] as FastLineSeries).Trendlines[0].BackwardForecast = Math.Round(slider.Value);
        }

        private void OnForwardValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;
            (sfchart.Series[0] as FastLineSeries).Trendlines[0].ForwardForecast = Math.Round(slider.Value);
        }

        private void OnStrokeThicknessValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;
            if (sfchart != null)
                (sfchart.Series[0] as FastLineSeries).Trendlines[0].StrokeThickness = Math.Round(slider.Value);
        }

        public override void Dispose()
        {
            if ((this.grid.DataContext as Collection) != null)
                (this.grid.DataContext as Collection).Dispose();

            if (this.sfchart != null)
            {
                foreach (var series in this.sfchart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.sfchart = null;
            }

            this.grid.Resources = null;

            base.Dispose();
        }
    }

    public class Collection : IDisposable
    {
        public Collection()
        {
            this.StockPriceDetails = new ObservableCollection<TrendModel>();
            DateTime date = new DateTime(2013, 1, 1);
            Random rd = new Random();
            double value = 1000;

            for (int i = 0; i < 100; i++)
            {
                if (rd.NextDouble() > .5)
                {
                    value += rd.NextDouble();
                    this.StockPriceDetails.Add(new TrendModel() { Value = value, Date = date.AddDays(i) });
                }
                else
                {
                    value -= rd.NextDouble();
                    this.StockPriceDetails.Add(new TrendModel() { Value = value, Date = date.AddDays(i) });
                }
            }
        }
        public ObservableCollection<TrendModel> StockPriceDetails
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.StockPriceDetails != null)
                this.StockPriceDetails.Clear();
        }
    }

    public class TrendModel
    {
        public DateTime Date
        {
            get;
            set;
        }

        public double Value
        {
            get;
            set;
        }
    }
}
