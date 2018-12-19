#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
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
    public sealed partial class BasicCharts : SampleLayout
    {
        LineChartViewModel viewModel= new LineChartViewModel();
        ScatterViewModel scatterViewModel= new ScatterViewModel();
        public BasicCharts()
        {
            this.InitializeComponent();
            this.Unloaded += BasicCharts_Unloaded;
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                ls1ChartAdornmentInfo.Symbol = ChartSymbol.Custom;
                ls1ChartAdornmentInfo.ShowLabel = true;
                ls1ChartAdornmentInfo.LabelTemplate = MainGrid.Resources["labelTemplate1"] as DataTemplate;

                ls2ChartAdornmentInfo.Symbol = ChartSymbol.Custom;
                ls2ChartAdornmentInfo.ShowLabel = true;
                ls2ChartAdornmentInfo.LabelTemplate = MainGrid.Resources["labelTemplate2"] as DataTemplate;

                ls3ChartAdornmentInfo.Symbol = ChartSymbol.Custom;
                ls3ChartAdornmentInfo.ShowLabel = true;
                ls3ChartAdornmentInfo.LabelTemplate = MainGrid.Resources["labelTemplate3"] as DataTemplate;
                
                columnSeriesAdornmentInfo.ShowLabel = true;
                columnSeriesAdornmentInfo.ShowConnectorLine = true;
                columnSeriesAdornmentInfo.LabelTemplate = MainGrid.Resources["labelTemplate"] as DataTemplate;
                columnSeriesAdornmentInfo.HorizontalAlignment = HorizontalAlignment.Center;
                columnSeriesAdornmentInfo.VerticalAlignment = VerticalAlignment.Center;
                columnSeriesAdornmentInfo.AdornmentsPosition = AdornmentsPosition.Top;

                hsChartAdornmentInfo.ShowLabel = true;
                hsChartAdornmentInfo.HorizontalAlignment = HorizontalAlignment.Center;
                hsChartAdornmentInfo.VerticalAlignment = VerticalAlignment.Center;
                hsChartAdornmentInfo.LabelTemplate = MainGrid.Resources["histogramLabelTemplate"] as DataTemplate;

                waterfallAdornmentInfo.LabelTemplate= MainGrid.Resources["waterfallAdornment"] as DataTemplate;
                waterfallAdornmentInfo.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
                waterfallAdornmentInfo.HorizontalAlignment = HorizontalAlignment.Center;
                waterfallAdornmentInfo.VerticalAlignment = VerticalAlignment.Center;
                waterfallAdornmentInfo.ShowLabel = true;
                waterfallAdornmentInfo.ShowMarker = true;
            }
        }

        private void BasicCharts_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void OnChartTypeSelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            if (Basic_Chart != null)
            {
                if (Basic_Chart.Behaviors.Count == 0)
                    Basic_Chart.Behaviors.Add(new ChartZoomPanBehavior());
                (Basic_Chart.SecondaryAxis as NumericalAxis).ClearValue(NumericalAxis.MaximumProperty);
            }
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedIndex == 0 && viewModel != null)
            {
                Histogram_Chart.Visibility = Visibility.Collapsed;
                Basic_Chart.Visibility = Visibility.Visible;
                Column_Chart.Visibility = Visibility.Collapsed;
                Scatter_Chart.Visibility = Visibility.Collapsed;
                Waterfall_Chart.Visibility = Visibility.Collapsed;
                boxWhiskerChart.Visibility = Visibility.Collapsed;
                SplineRangeArea_Chart.Visibility = Visibility.Collapsed;

                Basic_Chart.PrimaryAxis = this.primaryAxis;
                Basic_Chart.SecondaryAxis = this.secondaryAxis;
                Basic_Chart.Legend = this.legend;
                Basic_Chart.Header = this.header;
                LineSeries series1 = new LineSeries();
                LineSeries series2 = new LineSeries();
                LineSeries series3 = new LineSeries();
                DataTemplate template1 = MainGrid.Resources["labelTemplate1"] as DataTemplate;
                DataTemplate template2 = MainGrid.Resources["labelTemplate2"] as DataTemplate;
                DataTemplate template3 = MainGrid.Resources["labelTemplate3"] as DataTemplate;
                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();
                adornmentInfo1.ShowLabel = true;
                ChartAdornmentInfo adornmentInfo2 = new ChartAdornmentInfo();
                adornmentInfo2.ShowLabel = true;
                ChartAdornmentInfo adornmentInfo3 = new ChartAdornmentInfo();
                adornmentInfo3.ShowLabel = true;
                series1.XBindingPath = "Year";
                series1.YBindingPath = "IND";
                series1.ItemsSource = viewModel.power;
                series1.ShowTooltip = true;
                series1.EnableAnimation = true;
                adornmentInfo1.LabelTemplate = template1;
                series1.AdornmentsInfo = adornmentInfo1;
                series1.Label = "India";
                series2.XBindingPath = "Year";
                series2.YBindingPath = "GER";
                series2.ItemsSource = viewModel.power;
                series2.ShowTooltip = true;
                series2.EnableAnimation = true;
                adornmentInfo2.LabelTemplate = template2;
                series2.AdornmentsInfo = adornmentInfo2;
                series2.Label = "Germany";
                series3.XBindingPath = "Year";
                series3.YBindingPath = "FRA";
                series3.ItemsSource = viewModel.power;
                series3.EnableAnimation = true;
                series3.ShowTooltip = true;
                adornmentInfo3.LabelTemplate = template3;
                series3.AdornmentsInfo = adornmentInfo3;
                series3.Label = "France";
                Basic_Chart.Series.Clear();
                Basic_Chart.PrimaryAxis.ZoomFactor = 1;
                Basic_Chart.SecondaryAxis.ZoomFactor = 1;
                Basic_Chart.Series.Add(series1);
                Basic_Chart.Series.Add(series2);
                Basic_Chart.Series.Add(series3);
            }
            else if (comboBox.SelectedIndex == 1 && viewModel != null)
            {
                Histogram_Chart.Visibility = Visibility.Collapsed;
                Basic_Chart.Visibility = Visibility.Visible;
                Column_Chart.Visibility = Visibility.Collapsed;
                Scatter_Chart.Visibility = Visibility.Collapsed;
                Waterfall_Chart.Visibility = Visibility.Collapsed;
                boxWhiskerChart.Visibility = Visibility.Collapsed;
                SplineRangeArea_Chart.Visibility = Visibility.Collapsed;
                
                Basic_Chart.PrimaryAxis = this.primaryAxis;
                Basic_Chart.SecondaryAxis = this.secondaryAxis;
                Basic_Chart.Legend = this.legend;
                Basic_Chart.Header = this.header;
                SplineSeries series1 = new SplineSeries();
                SplineSeries series2 = new SplineSeries();
                SplineSeries series3 = new SplineSeries();
                DataTemplate template1 = MainGrid.Resources["labelTemplate1"] as DataTemplate;
                DataTemplate template2 = MainGrid.Resources["labelTemplate2"] as DataTemplate;
                DataTemplate template3 = MainGrid.Resources["labelTemplate3"] as DataTemplate;
                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();
                adornmentInfo1.ShowLabel = true;
                ChartAdornmentInfo adornmentInfo2 = new ChartAdornmentInfo();
                adornmentInfo2.ShowLabel = true;
                ChartAdornmentInfo adornmentInfo3 = new ChartAdornmentInfo();
                adornmentInfo3.ShowLabel = true;
                series1.XBindingPath = "Year";
                series1.YBindingPath = "IND";
                series1.ItemsSource = viewModel.power;
                series1.EnableAnimation = true;
                adornmentInfo1.LabelTemplate = template1;
                series1.AdornmentsInfo = adornmentInfo1;
                series1.ShowTooltip = true;
                series1.Label = "India";
                series2.XBindingPath = "Year";
                series2.YBindingPath = "GER";
                series2.ItemsSource = viewModel.power;
                series2.EnableAnimation = true;
                adornmentInfo2.LabelTemplate = template2;
                series2.AdornmentsInfo = adornmentInfo2;
                series2.ShowTooltip = true;
                series2.Label = "Germany";
                series3.XBindingPath = "Year";
                series3.YBindingPath = "FRA";
                series3.ItemsSource = viewModel.power;
                series3.EnableAnimation = true;
                adornmentInfo3.LabelTemplate = template3;
                series3.AdornmentsInfo = adornmentInfo3;
                series3.ShowTooltip = true;
                series3.Label = "France";
                Basic_Chart.Series.Clear();
                Basic_Chart.PrimaryAxis.ZoomFactor = 1;
                Basic_Chart.SecondaryAxis.ZoomFactor = 1;
                Basic_Chart.Series.Add(series1);
                Basic_Chart.Series.Add(series2);
                Basic_Chart.Series.Add(series3);
            }
            else if(comboBox.SelectedIndex == 2)
            {
                Histogram_Chart.Visibility = Visibility.Collapsed;
                Basic_Chart.Visibility = Visibility.Collapsed;
                Column_Chart.Visibility = Visibility.Collapsed;
                Waterfall_Chart.Visibility = Visibility.Collapsed;
                Scatter_Chart.Visibility = Visibility.Collapsed;
                boxWhiskerChart.Visibility = Visibility.Collapsed;
                SplineRangeArea_Chart.Visibility = Visibility.Visible;

                (SplineRangeArea_Chart.Series[0] as AdornmentSeries).AdornmentsInfo = new ChartAdornmentInfo()
                {
                    AdornmentsPosition = AdornmentsPosition.TopAndBottom,
                    Symbol = ChartSymbol.Ellipse,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    ShowLabel = true,
                    LabelTemplate = this.MainGrid.Resources["splineRangeAreaAdornment"] as DataTemplate
                };
            }
            else if (comboBox.SelectedIndex == 3 && viewModel != null)
            {
                Histogram_Chart.Visibility = Visibility.Collapsed;
                Basic_Chart.Visibility = Visibility.Collapsed;
                Column_Chart.Visibility = Visibility.Collapsed;
                Waterfall_Chart.Visibility = Visibility.Collapsed;
                Scatter_Chart.Visibility = Visibility.Visible;
                boxWhiskerChart.Visibility = Visibility.Collapsed;
                SplineRangeArea_Chart.Visibility = Visibility.Collapsed;

            }
            else if (comboBox.SelectedIndex == 4)
            {
                Basic_Chart.Visibility = Visibility.Collapsed;
                Scatter_Chart.Visibility = Visibility.Collapsed;
                Column_Chart.Visibility = Visibility.Collapsed;
                Histogram_Chart.Visibility = Visibility.Collapsed;
                Waterfall_Chart.Visibility = Visibility.Collapsed;
                boxWhiskerChart.Visibility = Visibility.Visible;
                SplineRangeArea_Chart.Visibility = Visibility.Collapsed;
            }
            else if (comboBox.SelectedIndex == 5 && viewModel != null)
            {
                Histogram_Chart.Visibility = Visibility.Collapsed;
                Basic_Chart.Visibility = Visibility.Collapsed;
                Column_Chart.Visibility = Visibility.Visible;
                Scatter_Chart.Visibility = Visibility.Collapsed;
                Waterfall_Chart.Visibility = Visibility.Collapsed;
                boxWhiskerChart.Visibility = Visibility.Collapsed;
                SplineRangeArea_Chart.Visibility = Visibility.Collapsed;
            }
            else if (comboBox.SelectedIndex == 6 && viewModel != null)
            {
                Histogram_Chart.Visibility = Visibility.Collapsed;
                Basic_Chart.Visibility = Visibility.Visible;
                Column_Chart.Visibility = Visibility.Collapsed;
                Scatter_Chart.Visibility = Visibility.Collapsed;
                Waterfall_Chart.Visibility = Visibility.Collapsed;
                boxWhiskerChart.Visibility = Visibility.Collapsed;
                SplineRangeArea_Chart.Visibility = Visibility.Collapsed;

                Basic_Chart.PrimaryAxis = this.primaryAxis;
                Basic_Chart.SecondaryAxis = this.secondaryAxis;
                Basic_Chart.Legend = this.legend;
                Basic_Chart.Header = this.header;
                StepLineSeries series1 = new StepLineSeries();
                StepLineSeries series2 = new StepLineSeries();
                StepLineSeries series3 = new StepLineSeries();
                DataTemplate template1 = MainGrid.Resources["labelTemplate1"] as DataTemplate;
                DataTemplate template2 = MainGrid.Resources["labelTemplate2"] as DataTemplate;
                DataTemplate template3 = MainGrid.Resources["labelTemplate3"] as DataTemplate;
                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();
                adornmentInfo1.ShowLabel = true;
                ChartAdornmentInfo adornmentInfo2 = new ChartAdornmentInfo();
                adornmentInfo2.ShowLabel = true;
                ChartAdornmentInfo adornmentInfo3 = new ChartAdornmentInfo();
                adornmentInfo3.ShowLabel = true;
                series1.XBindingPath = "Year";
                series1.YBindingPath = "IND";
                series1.ShowTooltip = true;
                series1.ItemsSource = viewModel.power;
                adornmentInfo1.LabelTemplate = template1;
                series1.AdornmentsInfo = adornmentInfo1;
                series1.Label = "India";
                series2.XBindingPath = "Year";
                series2.YBindingPath = "GER";
                series2.ItemsSource = viewModel.power;
                series2.ShowTooltip = true;
                adornmentInfo2.LabelTemplate = template2;
                series2.AdornmentsInfo = adornmentInfo2;
                series2.Label = "Germany";
                series3.XBindingPath = "Year";
                series3.YBindingPath = "FRA";
                series3.ItemsSource = viewModel.power;
                series3.ShowTooltip = true;
                adornmentInfo3.LabelTemplate = template3;
                series3.AdornmentsInfo = adornmentInfo3;
                series3.Label = "France";
                Basic_Chart.Series.Clear();
                Basic_Chart.PrimaryAxis.ZoomFactor = 1;
                Basic_Chart.SecondaryAxis.ZoomFactor = 1;
                Basic_Chart.Series.Add(series1);
                Basic_Chart.Series.Add(series2);
                Basic_Chart.Series.Add(series3);
            }
            else if (comboBox.SelectedIndex == 7 && viewModel != null)
            {
                Histogram_Chart.Visibility = Visibility.Collapsed;
                Basic_Chart.Visibility = Visibility.Visible;
                Column_Chart.Visibility = Visibility.Collapsed;
                Scatter_Chart.Visibility = Visibility.Collapsed;
                Waterfall_Chart.Visibility = Visibility.Collapsed;
                boxWhiskerChart.Visibility = Visibility.Collapsed;
                SplineRangeArea_Chart.Visibility = Visibility.Collapsed;
                
                Basic_Chart.PrimaryAxis = this.primaryAxis;
                Basic_Chart.SecondaryAxis = this.secondaryAxis;
                Basic_Chart.Legend = this.legend;
                Basic_Chart.Header = this.header;
                StepAreaSeries series1 = new StepAreaSeries();
                StepAreaSeries series2 = new StepAreaSeries();
                DataTemplate template1 = MainGrid.Resources["labelTemplateStepArea"] as DataTemplate;
                DataTemplate template2 = MainGrid.Resources["labelTemplateStepArea1"] as DataTemplate;
                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo
                {
                    ShowLabel = true,
                    LabelTemplate = template1,
                };
                ChartAdornmentInfo adornmentInfo2 = new ChartAdornmentInfo
                {
                    ShowLabel = true,
                    LabelTemplate = template2,
                };
                series1.AdornmentsInfo = adornmentInfo1;
                adornmentInfo1.SymbolInterior = new SolidColorBrush(Color.FromArgb(255, 51, 102, 204));
                series1.XBindingPath = "Year";
                series1.YBindingPath = "AUS";
                series1.Label = "Australia";
                series1.ItemsSource = viewModel.power;
                series1.StrokeThickness = 2;
                series1.Stroke = new SolidColorBrush(Color.FromArgb(255, 27, 161, 226));
                series1.Interior = new SolidColorBrush(Color.FromArgb(120, 27, 161, 226));
                series1.ShowTooltip = true;
                series2.AdornmentsInfo = adornmentInfo2;
                adornmentInfo1.SymbolInterior = new SolidColorBrush(Color.FromArgb(255, 73, 70, 133));
                series2.XBindingPath = "Year";
                series2.YBindingPath = "UK";
                series2.Label = "UK";
                series2.ItemsSource = viewModel.power;
                series2.StrokeThickness = 2;
                series2.Stroke = new SolidColorBrush(Color.FromArgb(250, 47, 180, 170));
                series2.Interior = new SolidColorBrush(Color.FromArgb(200, 47, 180, 170));
                series2.ShowTooltip = true;
                Basic_Chart.Series.Clear();
                Basic_Chart.PrimaryAxis.ZoomFactor = 1;
                Basic_Chart.SecondaryAxis.ZoomFactor = 1;
                Basic_Chart.Series.Add(series1);
                Basic_Chart.Series.Add(series2);
            }
            else if (comboBox.SelectedIndex == 8)
            {
                Basic_Chart.Visibility = Visibility.Collapsed;
                Scatter_Chart.Visibility = Visibility.Collapsed;
                Column_Chart.Visibility = Visibility.Collapsed;
                Histogram_Chart.Visibility = Visibility.Visible;
                boxWhiskerChart.Visibility = Visibility.Collapsed;
                Waterfall_Chart.Visibility = Visibility.Collapsed;
                SplineRangeArea_Chart.Visibility = Visibility.Collapsed;
            }
            else 
            {
                Basic_Chart.Visibility = Visibility.Collapsed;
                Scatter_Chart.Visibility = Visibility.Collapsed;
                Column_Chart.Visibility = Visibility.Collapsed;
                Histogram_Chart.Visibility = Visibility.Collapsed;
                boxWhiskerChart.Visibility = Visibility.Collapsed;
                Waterfall_Chart.Visibility = Visibility.Visible;
                SplineRangeArea_Chart.Visibility = Visibility.Collapsed;
            }
        }

        public override void Dispose()
        {
            if (this.Basic_Chart != null)
            {
                if (this.Basic_Chart.DataContext is LineChartViewModel)
                    (this.Basic_Chart.DataContext as LineChartViewModel).Dispose();
                foreach (var series in this.Basic_Chart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.Basic_Chart = null;
            }

            if (this.Scatter_Chart != null)
            {
                if (this.Scatter_Chart.DataContext is ScatterViewModel)
                    (this.Scatter_Chart.DataContext as ScatterViewModel).Dispose();
                foreach (var series in this.Scatter_Chart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.Scatter_Chart = null;
            }

            if (Histogram_Chart != null)
            {
                if (this.Histogram_Chart.DataContext is HistogramViewModel)
                    (this.Histogram_Chart.DataContext as HistogramViewModel).Dispose();
                foreach (var series in this.Histogram_Chart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.Histogram_Chart = null;
            }

            if (Waterfall_Chart != null)
            {
                if (this.Waterfall_Chart.DataContext is WaterfallViewModel)
                    (this.Waterfall_Chart.DataContext as WaterfallViewModel).Dispose();
                else if (this.Waterfall_Chart.DataContext is WaterfallViewModelMobile)
                    (this.Waterfall_Chart.DataContext as WaterfallViewModelMobile).Dispose();
                foreach (var series in this.Waterfall_Chart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.Waterfall_Chart = null;
            }

            if (boxWhiskerChart != null)
            {
                if (this.boxWhiskerChart.DataContext is BoxWhiskerViewModel)
                    (this.boxWhiskerChart.DataContext as BoxWhiskerViewModel).Dispose();
                else if (this.boxWhiskerChart.DataContext is BoxWhiskerViewModel)
                    (this.boxWhiskerChart.DataContext as BoxWhiskerViewModel).Dispose();
                foreach (var series in this.boxWhiskerChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.boxWhiskerChart = null;
            }

            this.Resources = null;

            base.Dispose();
        }
    }

    public class LineChartLabelConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return String.Format("{0}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
        #endregion
    }
}
