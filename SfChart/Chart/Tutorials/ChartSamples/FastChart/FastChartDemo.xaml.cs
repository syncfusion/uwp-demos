#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
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
    public sealed partial class FastChartDemo : SampleLayout
    {
        ViewModel viewModel;
        ChartTrackBallBehavior tackballBehavior;
        FastHiLoOpenCloseBitmapSeries hiLoOpenCloseBitmap1;
        public FastChartDemo()
        {
            Resources.MergedDictionaries.Add(SfChart.Resources.ColorModelResource.Resource);
            viewModel = new ViewModel();
            this.InitializeComponent();

            // Since the control is placed in a list view. The grid's manipulation has to be checked for good interactive behavior support.
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                this.grid.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            }

            tackballBehavior = new ChartTrackBallBehavior() { ShowLine = false, UseSeriesPalette = true };

            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                OHLCBitXAML.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);

                FBitMapXAML.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
            }
        }

        public IList<Data> GenerateData()
        {
            List<Data> datas = new List<Data>();
            Random randomNumber = new Random();
            DateTime date = new DateTime(2009, 1, 1);
            double value = 1000;

            for (int i = 0; i < 100000; i++)
            {
                datas.Add(new Data(date, value));
                date = date.Add(TimeSpan.FromSeconds(5));

                if (randomNumber.NextDouble() > .5)
                {
                    value += randomNumber.NextDouble();
                }
                else
                {
                    value -= randomNumber.NextDouble();
                }
            }
            return datas;
        }

        private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            // Trackball is used only for the Fast Range Area Series.
            if (financialChart.Behaviors.Contains(tackballBehavior))
                financialChart.Behaviors.Remove(tackballBehavior);

            if (comboBox.SelectedIndex == 3 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                StackingColumnChart.Visibility = Visibility.Collapsed;
                FastScatterChart.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.SecondaryAxis.Header = "Stock Price";
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                FastColumnBitmapSeries hiLoSeries = new FastColumnBitmapSeries();
                hiLoSeries.VisibilityOnLegend = Visibility.Collapsed;
                hiLoSeries.XBindingPath = "Date";
                hiLoSeries.YBindingPath = "Price";
                hiLoSeries.ItemsSource = viewModel.List;
                hiLoSeries.ShowTooltip = true;
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Clear();
                financialChart.Series.Add(hiLoSeries);
            }
            else if (comboBox.SelectedIndex == 1 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                StackingColumnChart.Visibility = Visibility.Collapsed;
                FastScatterChart.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.SecondaryAxis.Header = "Stock Price";
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                FastLineBitmapSeries hiLoSeries = new FastLineBitmapSeries();
                hiLoSeries.VisibilityOnLegend = Visibility.Collapsed;
                hiLoSeries.XBindingPath = "Date";
                hiLoSeries.YBindingPath = "Price";
                hiLoSeries.ItemsSource = viewModel.LineData;
                hiLoSeries.ShowTooltip = true;
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Clear();
                financialChart.Series.Add(hiLoSeries);
            }
            else if (comboBox.SelectedIndex == 2 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                StackingColumnChart.Visibility = Visibility.Collapsed;
                FastScatterChart.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                financialChart.Behaviors.Add(tackballBehavior);
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                financialChart.SecondaryAxis.Header = "Stock Price";
                FastRangeAreaBitmapSeries fastRangeAreaBitmapSeries = new FastRangeAreaBitmapSeries();
                fastRangeAreaBitmapSeries.VisibilityOnLegend = Visibility.Collapsed;
                fastRangeAreaBitmapSeries.XBindingPath = "Date";
                fastRangeAreaBitmapSeries.High = "Stock1";
                fastRangeAreaBitmapSeries.Low = "Stock2";
                fastRangeAreaBitmapSeries.ItemsSource = viewModel.RangeData;
                fastRangeAreaBitmapSeries.TrackBallLabelTemplate = this.grid.Resources["rangeSeriesTrackBallLabel"] as DataTemplate;
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Clear();
                financialChart.Series.Add(fastRangeAreaBitmapSeries);
            }
            else if (comboBox.SelectedIndex == 0 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                StackingColumnChart.Visibility = Visibility.Collapsed;
                FastScatterChart.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.SecondaryAxis.Header = "Stock Price";
                hiLoOpenCloseBitmap1 = new FastHiLoOpenCloseBitmapSeries();
                hiLoOpenCloseBitmap1.VisibilityOnLegend = Visibility.Collapsed;
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                hiLoOpenCloseBitmap1.XBindingPath = "TimeStamp";
                hiLoOpenCloseBitmap1.High = "High";
                hiLoOpenCloseBitmap1.Low = "Low";
                hiLoOpenCloseBitmap1.Open = "Open";
                hiLoOpenCloseBitmap1.Close = "Last";
                hiLoOpenCloseBitmap1.ItemsSource = viewModel.HiloViewModel;
                hiLoOpenCloseBitmap1.ShowTooltip = true;
                financialChart.Series.Clear();
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Add(hiLoOpenCloseBitmap1);
            }
            else if (comboBox.SelectedIndex == 8 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                StackingColumnChart.Visibility = Visibility.Collapsed;
                FastScatterChart.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                FastHiLoBitmapSeries hiLoOpenClose = new FastHiLoBitmapSeries();
                hiLoOpenClose.VisibilityOnLegend = Visibility.Collapsed;
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.SecondaryAxis.Header = "Stock Price";
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                hiLoOpenClose.XBindingPath = "TimeStamp";
                hiLoOpenClose.High = "High";
                hiLoOpenClose.Low = "Low";
                hiLoOpenClose.ItemsSource = viewModel.HiloViewModel;
                hiLoOpenClose.ShowTooltip = true;
                financialChart.Series.Clear();
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Add(hiLoOpenClose);
            }
            else if (comboBox.SelectedIndex == 4 && financialChart != null)
            {
                financialChart.Visibility = Visibility.Collapsed;
                StackingColumnChart.Visibility = Visibility.Collapsed;
                BarSeries.Visibility = Visibility.Visible;
                FastScatterChart.Visibility = Visibility.Collapsed;
            }
            else if (comboBox.SelectedIndex == 5 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                StackingColumnChart.Visibility = Visibility.Collapsed;
                FastScatterChart.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.SecondaryAxis.Header = "Stock Price";
                FastStepLineBitmapSeries hiLoSeries = new FastStepLineBitmapSeries();
                hiLoSeries.VisibilityOnLegend = Visibility.Collapsed;
                hiLoSeries.XBindingPath = "Date";
                hiLoSeries.YBindingPath = "Price";
                hiLoSeries.ItemsSource = viewModel.List;
                hiLoSeries.ShowTooltip = true;
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Clear();
                financialChart.Series.Add(hiLoSeries);
            }
            else if (comboBox.SelectedIndex == 6 && FastScatterChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                StackingColumnChart.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Collapsed;
                FastScatterChart.Visibility = Visibility.Visible;
                FastScatterChart.Series[0].ItemsSource = viewModel.ScatterData;
            }
            else if (comboBox.SelectedIndex == 7 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                StackingColumnChart.Visibility = Visibility.Collapsed;
                FastScatterChart.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.SecondaryAxis.Header = "Stock Price";
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                FastCandleBitmapSeries hiLoOpenClose = new FastCandleBitmapSeries();
                hiLoOpenClose.VisibilityOnLegend = Visibility.Collapsed;
                hiLoOpenClose.XBindingPath = "TimeStamp";
                hiLoOpenClose.High = "High";
                hiLoOpenClose.Low = "Low";
                hiLoOpenClose.Open = "Open";
                hiLoOpenClose.Close = "Last";
                hiLoOpenClose.ItemsSource = viewModel.HiloViewModel1;
                hiLoOpenClose.ShowTooltip = true;
                financialChart.Series.Clear();
                financialChart.Series.Add(hiLoOpenClose);
            }
            else if (comboBox.SelectedIndex == 9 && StackingColumnChart != null && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                FastScatterChart.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Collapsed;
                StackingColumnChart.Visibility = Visibility.Visible;
            }
        }

        public override void Dispose()
        {
            if (this.DataContext is ViewModel)
                (this.DataContext as ViewModel).Dispose();

            if (this.financialChart != null)
            {
                foreach (var series in this.financialChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.financialChart = null;
            }

            if (this.BarSeries != null)
            {
                foreach (var series in this.BarSeries.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.BarSeries = null;
            }

            if (this.StackingColumnChart != null)
            {
                foreach (var series in this.StackingColumnChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.StackingColumnChart = null;
            }
			
			if (this.FastScatterChart != null)
            {
                foreach (var series in this.FastScatterChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.FastScatterChart = null;
            }

            if (this.grid.DataContext != null)
                this.grid.DataContext = null;
            this.grid.Resources = null;

            base.Dispose();
        }
    }

    public class FastChartDataModel
    {
        public DateTime Date
        {
            get;
            set;
        }

        public string ProdName
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }

        public double Stock
        {
            get;
            set;
        }
    }

    public class ViewModel : IDisposable
    {
        public ObservableCollection<TestingValues> TestingModel
        {
            get;
            set;
        }

        public ObservableCollection<TestingValues> CandleData
        {
            get;
            set;
        }

        DateTime date = new DateTime(1999, 1, 1);
        DateTime dateday = new DateTime(2010, 1, 1);
        private const int pricesCount = 100;

        public ViewModel()
        {
            int count = 0;
            List = new ObservableCollection<ColumnData>();
            LineData = new ObservableCollection<ColumnData>();
            CandleData = new ObservableCollection<TestingValues>();
            HiloViewModel = new ObservableCollection<StockPriceData>();
            HiloViewModel1 = new ObservableCollection<StockPriceData>();
            RangeData = new ObservableCollection<RangeData>();
            HiloViewModel = this.GetPricesFromCSVFile("Syncfusion.SampleBrowser.UWP.SfChart.Chart.Tutorials.Data.Data.txt", pricesCount);
            HiloViewModel1 = this.GetPricesFromCSVFile1("Syncfusion.SampleBrowser.UWP.SfChart.Chart.Tutorials.Data.Data.txt", pricesCount);
            Random random = new Random();
            count = AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile" ? 100 : 50;
            for (int i = 0; i < count; i++)
            {
                List.Add(new ColumnData() { Date = date.AddDays(i), Stock = random.Next(1, 20), Price = random.Next(1, 20) });
            }

            TestingModel = new ObservableCollection<TestingValues>();
            Random rd = new Random();
            DateTime d = DateTime.Now;
            count = AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile" ? 50 : 25;
            for (int i = 0; i < count; i++)
            {
                this.TestingModel.Add(new TestingValues()
                {
                    Date = d.AddDays(i),
                    X = i + 1000,
                    Y = rd.Next(40, 50),
                    Y1 = rd.Next(0, 10),
                    Y2 = rd.Next(10, 40),
                    Y3 = rd.Next(20, 40)
                });
            }

            for (int i = 0; i < 15; i++)
            {
                this.CandleData.Add(new TestingValues()
                {
                    Date = d.AddDays(i),
                    X = i + 1000,
                    Y = rd.Next(70, 100),
                    Y1 = rd.Next(0, 10),
                    Y2 = rd.Next(10, 40),
                    Y3 = rd.Next(15, 30)
                });
            }

            double value = 100;
            DateTime now = DateTime.Now;
            count = AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile" ? 2000 : 1000;
            for (int i = 0; i < count; i++)
            {
                if (random.NextDouble() > .5)
                {
                    value += random.NextDouble();
                }
                else
                {
                    value -= random.NextDouble();
                }
                LineData.Add(new ColumnData() { Date = now.AddHours(i), Price = value });
                RangeData.Add(new RangeData() { Date = now.AddHours(i), Stock1 = value + 2, Stock2 = value - 2 });
            }

            ScatterData = GenerateData();
        }

        public ObservableCollection<ScatterModel> GenerateData()
        {
            ObservableCollection<ScatterModel> datas = new ObservableCollection<ScatterModel>();

            datas.Add(new ScatterModel() { Eruptions = 3.600f, WaitingTime = 79 });
            datas.Add(new ScatterModel() { Eruptions = 1.800f, WaitingTime = 54 });
            datas.Add(new ScatterModel() { Eruptions = 3.333f, WaitingTime = 74 });
            datas.Add(new ScatterModel() { Eruptions = 2.283f, WaitingTime = 62 });
            datas.Add(new ScatterModel() { Eruptions = 4.533f, WaitingTime = 85 });
            datas.Add(new ScatterModel() { Eruptions = 2.883f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 4.700f, WaitingTime = 88 });
            datas.Add(new ScatterModel() { Eruptions = 3.600f, WaitingTime = 85 });
            datas.Add(new ScatterModel() { Eruptions = 1.950f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 4.350f, WaitingTime = 85 });
            datas.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 54 });
            datas.Add(new ScatterModel() { Eruptions = 3.917f, WaitingTime = 84 });
            datas.Add(new ScatterModel() { Eruptions = 4.200f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 1.750f, WaitingTime = 47 });
            datas.Add(new ScatterModel() { Eruptions = 4.700f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 2.167f, WaitingTime = 52 });
            datas.Add(new ScatterModel() { Eruptions = 1.750f, WaitingTime = 62 });
            datas.Add(new ScatterModel() { Eruptions = 4.800f, WaitingTime = 84 });
            datas.Add(new ScatterModel() { Eruptions = 1.600f, WaitingTime = 52 });
            datas.Add(new ScatterModel() { Eruptions = 4.250f, WaitingTime = 79 });
            datas.Add(new ScatterModel() { Eruptions = 1.800f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 1.750f, WaitingTime = 47 });
            datas.Add(new ScatterModel() { Eruptions = 3.450f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 3.067f, WaitingTime = 69 });
            datas.Add(new ScatterModel() { Eruptions = 4.533f, WaitingTime = 74 });
            datas.Add(new ScatterModel() { Eruptions = 3.600f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 1.967f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 4.083f, WaitingTime = 76 });
            datas.Add(new ScatterModel() { Eruptions = 3.850f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 4.433f, WaitingTime = 79 });
            datas.Add(new ScatterModel() { Eruptions = 4.300f, WaitingTime = 73 });
            datas.Add(new ScatterModel() { Eruptions = 4.467f, WaitingTime = 77 });
            datas.Add(new ScatterModel() { Eruptions = 3.367f, WaitingTime = 66 });
            datas.Add(new ScatterModel() { Eruptions = 4.033f, WaitingTime = 80 });
            datas.Add(new ScatterModel() { Eruptions = 3.833f, WaitingTime = 74 });
            datas.Add(new ScatterModel() { Eruptions = 2.017f, WaitingTime = 52 });
            datas.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 48 });
            datas.Add(new ScatterModel() { Eruptions = 4.833f, WaitingTime = 80 });
            datas.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 59 });
            datas.Add(new ScatterModel() { Eruptions = 4.783f, WaitingTime = 90 });
            datas.Add(new ScatterModel() { Eruptions = 4.350f, WaitingTime = 80 });
            datas.Add(new ScatterModel() { Eruptions = 1.883f, WaitingTime = 58 });
            datas.Add(new ScatterModel() { Eruptions = 4.567f, WaitingTime = 84 });
            datas.Add(new ScatterModel() { Eruptions = 1.750f, WaitingTime = 58 });
            datas.Add(new ScatterModel() { Eruptions = 4.533f, WaitingTime = 73 });
            datas.Add(new ScatterModel() { Eruptions = 3.317f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 3.833f, WaitingTime = 64 });
            datas.Add(new ScatterModel() { Eruptions = 2.100f, WaitingTime = 53 });
            datas.Add(new ScatterModel() { Eruptions = 4.633f, WaitingTime = 82 });
            datas.Add(new ScatterModel() { Eruptions = 2.000f, WaitingTime = 59 });
            datas.Add(new ScatterModel() { Eruptions = 4.800f, WaitingTime = 75 });
            datas.Add(new ScatterModel() { Eruptions = 4.716f, WaitingTime = 90 });
            datas.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 54 });
            datas.Add(new ScatterModel() { Eruptions = 4.833f, WaitingTime = 80 });
            datas.Add(new ScatterModel() { Eruptions = 1.733f, WaitingTime = 54 });
            datas.Add(new ScatterModel() { Eruptions = 4.883f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 3.717f, WaitingTime = 71 });
            datas.Add(new ScatterModel() { Eruptions = 1.667f, WaitingTime = 64 });
            datas.Add(new ScatterModel() { Eruptions = 4.567f, WaitingTime = 77 });
            datas.Add(new ScatterModel() { Eruptions = 4.317f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 2.233f, WaitingTime = 59 });
            datas.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 84 });
            datas.Add(new ScatterModel() { Eruptions = 1.750f, WaitingTime = 48 });
            datas.Add(new ScatterModel() { Eruptions = 4.800f, WaitingTime = 82 });
            datas.Add(new ScatterModel() { Eruptions = 1.817f, WaitingTime = 60 });
            datas.Add(new ScatterModel() { Eruptions = 4.400f, WaitingTime = 92 });
            datas.Add(new ScatterModel() { Eruptions = 4.167f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 4.700f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 2.067f, WaitingTime = 65 });
            datas.Add(new ScatterModel() { Eruptions = 4.700f, WaitingTime = 73 });
            datas.Add(new ScatterModel() { Eruptions = 4.033f, WaitingTime = 82 });
            datas.Add(new ScatterModel() { Eruptions = 1.967f, WaitingTime = 56 });
            datas.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 79 });
            datas.Add(new ScatterModel() { Eruptions = 4.000f, WaitingTime = 71 });
            datas.Add(new ScatterModel() { Eruptions = 1.983f, WaitingTime = 62 });
            datas.Add(new ScatterModel() { Eruptions = 5.067f, WaitingTime = 76 });
            datas.Add(new ScatterModel() { Eruptions = 2.017f, WaitingTime = 60 });
            datas.Add(new ScatterModel() { Eruptions = 4.567f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 3.883f, WaitingTime = 76 });
            datas.Add(new ScatterModel() { Eruptions = 3.600f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 4.133f, WaitingTime = 75 });
            datas.Add(new ScatterModel() { Eruptions = 4.333f, WaitingTime = 82 });
            datas.Add(new ScatterModel() { Eruptions = 4.100f, WaitingTime = 70 });
            datas.Add(new ScatterModel() { Eruptions = 2.633f, WaitingTime = 65 });
            datas.Add(new ScatterModel() { Eruptions = 4.067f, WaitingTime = 73 });
            datas.Add(new ScatterModel() { Eruptions = 4.933f, WaitingTime = 88 });
            datas.Add(new ScatterModel() { Eruptions = 3.950f, WaitingTime = 76 });
            datas.Add(new ScatterModel() { Eruptions = 4.517f, WaitingTime = 80 });
            datas.Add(new ScatterModel() { Eruptions = 2.167f, WaitingTime = 48 });
            datas.Add(new ScatterModel() { Eruptions = 4.000f, WaitingTime = 86 });
            datas.Add(new ScatterModel() { Eruptions = 2.200f, WaitingTime = 60 });
            datas.Add(new ScatterModel() { Eruptions = 4.333f, WaitingTime = 90 });
            datas.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 50 });
            datas.Add(new ScatterModel() { Eruptions = 4.817f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 63 });
            datas.Add(new ScatterModel() { Eruptions = 4.300f, WaitingTime = 72 });
            datas.Add(new ScatterModel() { Eruptions = 4.667f, WaitingTime = 84 });
            datas.Add(new ScatterModel() { Eruptions = 3.750f, WaitingTime = 75 });
            datas.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 4.900f, WaitingTime = 82 });
            datas.Add(new ScatterModel() { Eruptions = 2.483f, WaitingTime = 62 });
            datas.Add(new ScatterModel() { Eruptions = 4.367f, WaitingTime = 88 });
            datas.Add(new ScatterModel() { Eruptions = 2.100f, WaitingTime = 49 });
            datas.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 4.050f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 47 });
            datas.Add(new ScatterModel() { Eruptions = 4.700f, WaitingTime = 84 });
            datas.Add(new ScatterModel() { Eruptions = 1.783f, WaitingTime = 52 });
            datas.Add(new ScatterModel() { Eruptions = 4.850f, WaitingTime = 86 });
            datas.Add(new ScatterModel() { Eruptions = 3.683f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 4.733f, WaitingTime = 75 });
            datas.Add(new ScatterModel() { Eruptions = 2.300f, WaitingTime = 59 });
            datas.Add(new ScatterModel() { Eruptions = 4.900f, WaitingTime = 89 });
            datas.Add(new ScatterModel() { Eruptions = 4.417f, WaitingTime = 79 });
            datas.Add(new ScatterModel() { Eruptions = 1.700f, WaitingTime = 59 });
            datas.Add(new ScatterModel() { Eruptions = 4.633f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 2.317f, WaitingTime = 50 });
            datas.Add(new ScatterModel() { Eruptions = 4.600f, WaitingTime = 85 });
            datas.Add(new ScatterModel() { Eruptions = 1.817f, WaitingTime = 59 });
            datas.Add(new ScatterModel() { Eruptions = 4.417f, WaitingTime = 87 });
            datas.Add(new ScatterModel() { Eruptions = 2.617f, WaitingTime = 53 });
            datas.Add(new ScatterModel() { Eruptions = 4.067f, WaitingTime = 69 });
            datas.Add(new ScatterModel() { Eruptions = 4.250f, WaitingTime = 77 });
            datas.Add(new ScatterModel() { Eruptions = 1.967f, WaitingTime = 56 });
            datas.Add(new ScatterModel() { Eruptions = 4.600f, WaitingTime = 88 });
            datas.Add(new ScatterModel() { Eruptions = 3.767f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 1.917f, WaitingTime = 45 });
            datas.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 82 });
            datas.Add(new ScatterModel() { Eruptions = 2.267f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 4.650f, WaitingTime = 90 });
            datas.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 45 });
            datas.Add(new ScatterModel() { Eruptions = 4.167f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 2.800f, WaitingTime = 56 });
            datas.Add(new ScatterModel() { Eruptions = 4.333f, WaitingTime = 89 });
            datas.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 46 });
            datas.Add(new ScatterModel() { Eruptions = 4.383f, WaitingTime = 82 });
            datas.Add(new ScatterModel() { Eruptions = 1.883f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 4.933f, WaitingTime = 86 });
            datas.Add(new ScatterModel() { Eruptions = 2.033f, WaitingTime = 53 });
            datas.Add(new ScatterModel() { Eruptions = 3.733f, WaitingTime = 79 });
            datas.Add(new ScatterModel() { Eruptions = 4.233f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 2.233f, WaitingTime = 60 });
            datas.Add(new ScatterModel() { Eruptions = 4.533f, WaitingTime = 82 });
            datas.Add(new ScatterModel() { Eruptions = 4.817f, WaitingTime = 77 });
            datas.Add(new ScatterModel() { Eruptions = 4.333f, WaitingTime = 76 });
            datas.Add(new ScatterModel() { Eruptions = 1.983f, WaitingTime = 59 });
            datas.Add(new ScatterModel() { Eruptions = 4.633f, WaitingTime = 80 });
            datas.Add(new ScatterModel() { Eruptions = 2.017f, WaitingTime = 49 });
            datas.Add(new ScatterModel() { Eruptions = 5.100f, WaitingTime = 96 });
            datas.Add(new ScatterModel() { Eruptions = 1.800f, WaitingTime = 53 });
            datas.Add(new ScatterModel() { Eruptions = 5.033f, WaitingTime = 77 });
            datas.Add(new ScatterModel() { Eruptions = 4.000f, WaitingTime = 77 });
            datas.Add(new ScatterModel() { Eruptions = 2.400f, WaitingTime = 65 });
            datas.Add(new ScatterModel() { Eruptions = 4.600f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 3.567f, WaitingTime = 71 });
            datas.Add(new ScatterModel() { Eruptions = 4.000f, WaitingTime = 70 });
            datas.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 4.083f, WaitingTime = 93 });
            datas.Add(new ScatterModel() { Eruptions = 1.800f, WaitingTime = 53 });
            datas.Add(new ScatterModel() { Eruptions = 3.967f, WaitingTime = 89 });
            datas.Add(new ScatterModel() { Eruptions = 2.200f, WaitingTime = 45 });
            datas.Add(new ScatterModel() { Eruptions = 4.150f, WaitingTime = 86 });
            datas.Add(new ScatterModel() { Eruptions = 2.000f, WaitingTime = 58 });
            datas.Add(new ScatterModel() { Eruptions = 3.833f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 3.500f, WaitingTime = 66 });
            datas.Add(new ScatterModel() { Eruptions = 4.583f, WaitingTime = 76 });
            datas.Add(new ScatterModel() { Eruptions = 2.367f, WaitingTime = 63 });
            datas.Add(new ScatterModel() { Eruptions = 5.000f, WaitingTime = 88 });
            datas.Add(new ScatterModel() { Eruptions = 1.933f, WaitingTime = 52 });
            datas.Add(new ScatterModel() { Eruptions = 4.617f, WaitingTime = 93 });
            datas.Add(new ScatterModel() { Eruptions = 1.917f, WaitingTime = 49 });
            datas.Add(new ScatterModel() { Eruptions = 2.083f, WaitingTime = 57 });
            datas.Add(new ScatterModel() { Eruptions = 4.583f, WaitingTime = 77 });
            datas.Add(new ScatterModel() { Eruptions = 3.333f, WaitingTime = 68 });
            datas.Add(new ScatterModel() { Eruptions = 4.167f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 4.333f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 73 });
            datas.Add(new ScatterModel() { Eruptions = 2.417f, WaitingTime = 50 });
            datas.Add(new ScatterModel() { Eruptions = 4.000f, WaitingTime = 85 });
            datas.Add(new ScatterModel() { Eruptions = 4.167f, WaitingTime = 74 });
            datas.Add(new ScatterModel() { Eruptions = 1.883f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 4.583f, WaitingTime = 77 });
            datas.Add(new ScatterModel() { Eruptions = 4.250f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 3.767f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 2.033f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 4.433f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 4.083f, WaitingTime = 84 });
            datas.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 46 });
            datas.Add(new ScatterModel() { Eruptions = 4.417f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 2.183f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 4.800f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 57 });
            datas.Add(new ScatterModel() { Eruptions = 4.800f, WaitingTime = 76 });
            datas.Add(new ScatterModel() { Eruptions = 4.100f, WaitingTime = 84 });
            datas.Add(new ScatterModel() { Eruptions = 3.966f, WaitingTime = 77 });
            datas.Add(new ScatterModel() { Eruptions = 4.233f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 3.500f, WaitingTime = 87 });
            datas.Add(new ScatterModel() { Eruptions = 4.366f, WaitingTime = 77 });
            datas.Add(new ScatterModel() { Eruptions = 2.250f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 4.667f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 2.100f, WaitingTime = 60 });
            datas.Add(new ScatterModel() { Eruptions = 4.350f, WaitingTime = 82 });
            datas.Add(new ScatterModel() { Eruptions = 4.133f, WaitingTime = 91 });
            datas.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 53 });
            datas.Add(new ScatterModel() { Eruptions = 4.600f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 1.783f, WaitingTime = 46 });
            datas.Add(new ScatterModel() { Eruptions = 4.367f, WaitingTime = 77 });
            datas.Add(new ScatterModel() { Eruptions = 3.850f, WaitingTime = 84 });
            datas.Add(new ScatterModel() { Eruptions = 1.933f, WaitingTime = 49 });
            datas.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 2.383f, WaitingTime = 71 });
            datas.Add(new ScatterModel() { Eruptions = 4.700f, WaitingTime = 80 });
            datas.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 49 });
            datas.Add(new ScatterModel() { Eruptions = 3.833f, WaitingTime = 75 });
            datas.Add(new ScatterModel() { Eruptions = 3.417f, WaitingTime = 64 });
            datas.Add(new ScatterModel() { Eruptions = 4.233f, WaitingTime = 76 });
            datas.Add(new ScatterModel() { Eruptions = 2.400f, WaitingTime = 53 });
            datas.Add(new ScatterModel() { Eruptions = 4.800f, WaitingTime = 94 });
            datas.Add(new ScatterModel() { Eruptions = 2.000f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 4.150f, WaitingTime = 76 });
            datas.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 50 });
            datas.Add(new ScatterModel() { Eruptions = 4.267f, WaitingTime = 82 });
            datas.Add(new ScatterModel() { Eruptions = 1.750f, WaitingTime = 54 });
            datas.Add(new ScatterModel() { Eruptions = 4.483f, WaitingTime = 75 });
            datas.Add(new ScatterModel() { Eruptions = 4.000f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 4.117f, WaitingTime = 79 });
            datas.Add(new ScatterModel() { Eruptions = 4.083f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 4.267f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 3.917f, WaitingTime = 70 });
            datas.Add(new ScatterModel() { Eruptions = 4.550f, WaitingTime = 79 });
            datas.Add(new ScatterModel() { Eruptions = 4.083f, WaitingTime = 70 });
            datas.Add(new ScatterModel() { Eruptions = 2.417f, WaitingTime = 54 });
            datas.Add(new ScatterModel() { Eruptions = 4.183f, WaitingTime = 86 });
            datas.Add(new ScatterModel() { Eruptions = 2.217f, WaitingTime = 50 });
            datas.Add(new ScatterModel() { Eruptions = 4.450f, WaitingTime = 90 });
            datas.Add(new ScatterModel() { Eruptions = 1.883f, WaitingTime = 54 });
            datas.Add(new ScatterModel() { Eruptions = 1.850f, WaitingTime = 54 });
            datas.Add(new ScatterModel() { Eruptions = 4.283f, WaitingTime = 77 });
            datas.Add(new ScatterModel() { Eruptions = 3.950f, WaitingTime = 79 });
            datas.Add(new ScatterModel() { Eruptions = 2.333f, WaitingTime = 64 });
            datas.Add(new ScatterModel() { Eruptions = 4.150f, WaitingTime = 75 });
            datas.Add(new ScatterModel() { Eruptions = 2.350f, WaitingTime = 47 });
            datas.Add(new ScatterModel() { Eruptions = 4.933f, WaitingTime = 86 });
            datas.Add(new ScatterModel() { Eruptions = 2.900f, WaitingTime = 63 });
            datas.Add(new ScatterModel() { Eruptions = 4.583f, WaitingTime = 85 });
            datas.Add(new ScatterModel() { Eruptions = 3.833f, WaitingTime = 82 });
            datas.Add(new ScatterModel() { Eruptions = 2.083f, WaitingTime = 57 });
            datas.Add(new ScatterModel() { Eruptions = 4.367f, WaitingTime = 82 });
            datas.Add(new ScatterModel() { Eruptions = 2.133f, WaitingTime = 67 });
            datas.Add(new ScatterModel() { Eruptions = 4.350f, WaitingTime = 74 });
            datas.Add(new ScatterModel() { Eruptions = 2.200f, WaitingTime = 54 });
            datas.Add(new ScatterModel() { Eruptions = 4.450f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 3.567f, WaitingTime = 73 });
            datas.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 73 });
            datas.Add(new ScatterModel() { Eruptions = 4.150f, WaitingTime = 88 });
            datas.Add(new ScatterModel() { Eruptions = 3.817f, WaitingTime = 80 });
            datas.Add(new ScatterModel() { Eruptions = 3.917f, WaitingTime = 71 });
            datas.Add(new ScatterModel() { Eruptions = 4.450f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 2.000f, WaitingTime = 56 });
            datas.Add(new ScatterModel() { Eruptions = 4.283f, WaitingTime = 79 });
            datas.Add(new ScatterModel() { Eruptions = 4.767f, WaitingTime = 78 });
            datas.Add(new ScatterModel() { Eruptions = 4.533f, WaitingTime = 84 });
            datas.Add(new ScatterModel() { Eruptions = 1.850f, WaitingTime = 58 });
            datas.Add(new ScatterModel() { Eruptions = 4.250f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 1.983f, WaitingTime = 43 });
            datas.Add(new ScatterModel() { Eruptions = 2.250f, WaitingTime = 60 });
            datas.Add(new ScatterModel() { Eruptions = 4.750f, WaitingTime = 75 });
            datas.Add(new ScatterModel() { Eruptions = 4.117f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 2.150f, WaitingTime = 46 });
            datas.Add(new ScatterModel() { Eruptions = 4.417f, WaitingTime = 90 });
            datas.Add(new ScatterModel() { Eruptions = 1.817f, WaitingTime = 46 });
            datas.Add(new ScatterModel() { Eruptions = 4.467f, WaitingTime = 74 });




            datas.Add(new ScatterModel() { Eruptions = 2.283f, WaitingTime = 45 });
            datas.Add(new ScatterModel() { Eruptions = 2.767f, WaitingTime = 67 });
            datas.Add(new ScatterModel() { Eruptions = 2.533f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 2.850f, WaitingTime = 58 });
            datas.Add(new ScatterModel() { Eruptions = 2.250f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 2.983f, WaitingTime = 43 });
            datas.Add(new ScatterModel() { Eruptions = 2.250f, WaitingTime = 60 });
            datas.Add(new ScatterModel() { Eruptions = 2.750f, WaitingTime = 59 });
            datas.Add(new ScatterModel() { Eruptions = 2.117f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 2.150f, WaitingTime = 46 });
            datas.Add(new ScatterModel() { Eruptions = 2.417f, WaitingTime = 58 });
            datas.Add(new ScatterModel() { Eruptions = 2.817f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 2.467f, WaitingTime = 48 });

            datas.Add(new ScatterModel() { Eruptions = 2.283f, WaitingTime = 43 });
            datas.Add(new ScatterModel() { Eruptions = 2.233f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 2.250f, WaitingTime = 58 });
            datas.Add(new ScatterModel() { Eruptions = 2.267f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 2.250f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 2.283f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 2.250f, WaitingTime = 56 });
            datas.Add(new ScatterModel() { Eruptions = 2.250f, WaitingTime = 59 });
            datas.Add(new ScatterModel() { Eruptions = 2.217f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 2.250f, WaitingTime = 60 });
            datas.Add(new ScatterModel() { Eruptions = 2.317f, WaitingTime = 58 });
            datas.Add(new ScatterModel() { Eruptions = 2.317f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 2.367f, WaitingTime = 48 });

            datas.Add(new ScatterModel() { Eruptions = 2.183f, WaitingTime = 43 });
            datas.Add(new ScatterModel() { Eruptions = 2.133f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 2.150f, WaitingTime = 58 });
            datas.Add(new ScatterModel() { Eruptions = 2.167f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 2.150f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 2.183f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 2.150f, WaitingTime = 56 });
            datas.Add(new ScatterModel() { Eruptions = 2.150f, WaitingTime = 59 });
            datas.Add(new ScatterModel() { Eruptions = 2.117f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 2.150f, WaitingTime = 60 });
            datas.Add(new ScatterModel() { Eruptions = 2.117f, WaitingTime = 58 });
            datas.Add(new ScatterModel() { Eruptions = 2.117f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 2.167f, WaitingTime = 48 });

            datas.Add(new ScatterModel() { Eruptions = 2.083f, WaitingTime = 43 });
            datas.Add(new ScatterModel() { Eruptions = 2.033f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 2.050f, WaitingTime = 58 });
            datas.Add(new ScatterModel() { Eruptions = 2.067f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 2.050f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 2.083f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 2.050f, WaitingTime = 56 });
            datas.Add(new ScatterModel() { Eruptions = 2.050f, WaitingTime = 59 });
            datas.Add(new ScatterModel() { Eruptions = 2.017f, WaitingTime = 55 });
            datas.Add(new ScatterModel() { Eruptions = 2.050f, WaitingTime = 60 });
            datas.Add(new ScatterModel() { Eruptions = 2.017f, WaitingTime = 58 });
            datas.Add(new ScatterModel() { Eruptions = 2.017f, WaitingTime = 51 });
            datas.Add(new ScatterModel() { Eruptions = 2.067f, WaitingTime = 48 });


            datas.Add(new ScatterModel() { Eruptions = 4.583f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 4.533f, WaitingTime = 85 });
            datas.Add(new ScatterModel() { Eruptions = 4.550f, WaitingTime = 88 });
            datas.Add(new ScatterModel() { Eruptions = 4.567f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 4.550f, WaitingTime = 87 });
            datas.Add(new ScatterModel() { Eruptions = 4.583f, WaitingTime = 85 });
            datas.Add(new ScatterModel() { Eruptions = 4.550f, WaitingTime = 86 });
            datas.Add(new ScatterModel() { Eruptions = 4.550f, WaitingTime = 84 });
            datas.Add(new ScatterModel() { Eruptions = 4.517f, WaitingTime = 85 });
            datas.Add(new ScatterModel() { Eruptions = 4.550f, WaitingTime = 80 });
            datas.Add(new ScatterModel() { Eruptions = 4.517f, WaitingTime = 86 });
            datas.Add(new ScatterModel() { Eruptions = 4.517f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 4.567f, WaitingTime = 88 });

            datas.Add(new ScatterModel() { Eruptions = 4.483f, WaitingTime = 83 });
            datas.Add(new ScatterModel() { Eruptions = 4.433f, WaitingTime = 85 });
            datas.Add(new ScatterModel() { Eruptions = 4.450f, WaitingTime = 88 });
            datas.Add(new ScatterModel() { Eruptions = 4.467f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 4.450f, WaitingTime = 87 });
            datas.Add(new ScatterModel() { Eruptions = 4.483f, WaitingTime = 85 });
            datas.Add(new ScatterModel() { Eruptions = 4.450f, WaitingTime = 86 });
            datas.Add(new ScatterModel() { Eruptions = 4.450f, WaitingTime = 84 });
            datas.Add(new ScatterModel() { Eruptions = 4.417f, WaitingTime = 85 });
            datas.Add(new ScatterModel() { Eruptions = 4.450f, WaitingTime = 80 });
            datas.Add(new ScatterModel() { Eruptions = 4.417f, WaitingTime = 86 });
            datas.Add(new ScatterModel() { Eruptions = 4.417f, WaitingTime = 81 });
            datas.Add(new ScatterModel() { Eruptions = 4.467f, WaitingTime = 88 });

            return datas;
        }

        public ObservableCollection<StockPriceData> GetPricesFromCSVFile1(string fileName, int count)
        {
            char[] comma = new char[] { ',' };
            char[] slashN = new char[] { '\n' };
            char[] slashT = new char[] { '\t' };
            ObservableCollection<StockPriceData> list = new ObservableCollection<StockPriceData>();

            IList<string> lines = new List<string>();
            var assembly = typeof(FastChartDemo).GetTypeInfo().Assembly;
            var fileStream = assembly.GetManifestResourceStream(fileName);
            StreamReader reader = new StreamReader(fileStream);
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }

            bool firstLine = true;
            string[] values;
            //int count = lines.Count() - 2;            
            StockPriceData priceInfo;
            int index = 0;
            int index1 = 0;
            foreach (string line in lines)
            {
                if (index > 200)
                    break;
                if (!firstLine && index > count + 1 && index <= 200)
                {
                    values = line.Split(slashT);
                    if (values.GetLength(0) > 5)
                    {
                        priceInfo = new StockPriceData()
                        {
                            TimeStamp = DateTime.Parse(values[0], CultureInfo.InvariantCulture),
                            Open = double.Parse(values[1]),
                            High = double.Parse(values[2]),
                            Low = double.Parse(values[3]),
                            Last = double.Parse(values[4]),
                            //Volume = double.Parse(values[5])
                        };
                        list.Insert(index1, priceInfo);
                        index1++;
                    }
                }
                else
                {
                    firstLine = false;
                }
                index++;
            }
            return list;
        }

        public ObservableCollection<StockPriceData> GetPricesFromCSVFile(string fileName, int count)
        {
            char[] comma = new char[] { ',' };
            char[] slashN = new char[] { '\n' };
            char[] slashT = new char[] { '\t' };
            ObservableCollection<StockPriceData> list = new ObservableCollection<StockPriceData>();
            //string s = File.ReadAllText(fileName);
            //string[] lines = s.Split(slashN);

            IList<string> lines = new List<string>();
            var assembly = typeof(FastChartDemo).GetTypeInfo().Assembly;
            var fileStream = assembly.GetManifestResourceStream(fileName);
            StreamReader reader = new StreamReader(fileStream);
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }

            bool firstLine = true;
            string[] values;
            //int count = lines.Count() - 2;            
            StockPriceData priceInfo;
            int index = 0;
            foreach (string line in lines)
            {
                if (count != -1 && index >= count)
                    break;
                if (!firstLine)
                {
                    values = line.Split(slashT);
                    if (values.GetLength(0) > 5)
                    {
                        priceInfo = new StockPriceData()
                        {
                            TimeStamp = DateTime.Parse(values[0], CultureInfo.InvariantCulture),
                            Open = double.Parse(values[1]),
                            High = double.Parse(values[2]),
                            Low = double.Parse(values[3]),
                            Last = double.Parse(values[4]),
                            //Volume = double.Parse(values[5])
                        };
                        list.Insert(index, priceInfo);
                        index++;
                    }
                }
                else
                {
                    firstLine = false;
                }
            }
            return list;
        }

        public ObservableCollection<StockPriceData> HiloViewModel
        {
            get;
            set;
        }

        public ObservableCollection<StockPriceData> HiloViewModel1
        {
            get;
            set;
        }
        public ObservableCollection<ColumnData> List
        {
            get;
            set;
        }

        public ObservableCollection<ColumnData> LineData
        {
            get;
            set;
        }

        public ObservableCollection<ScatterModel> ScatterData
        {
            get;
            set;
        }

        public ObservableCollection<RangeData> RangeData
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.TestingModel != null)
                this.TestingModel.Clear();
            if (this.List != null)
                this.List.Clear();
            if (this.LineData != null)
                this.LineData.Clear();
            if (this.CandleData != null)
                this.CandleData.Clear();
            if (this.RangeData != null)
                this.RangeData.Clear();
        }
    }

    public class TestingValues
    {
        public DateTime Date
        {
            get;
            set;
        }

        public double X
        {
            get;
            set;
        }

        public double Y
        {
            get;
            set;
        }

        public double Y1
        {
            get;
            set;
        }

        public double Y2
        {
            get;
            set;
        }

        public double Y3
        {
            get;
            set;
        }
    }

    public class RangeData
    {
        public DateTime Date
        {
            get;
            set;
        }

        public double Stock1
        {
            get;
            set;
        }

        public double Stock2
        {
            get;
            set;
        }
    }

    public class ColumnData
    {
        public DateTime Date
        {
            get;
            set;
        }

        public string ProdName
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }

        public double Stock
        {
            get;
            set;
        }
    }

    public class FastStackingColumnChartViewModel
    {
        public FastStackingColumnChartViewModel()
        {
            this.MedalDetails = new ObservableCollection<Medals>();
            Random rd = new Random();
            int count = AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile" ? 100 : 50;
            for (int i = 0; i < count; i++)
            {
                MedalDetails.Add(new Medals()
                {
                    CountryName = i,
                    GoldMedals = rd.Next(20, 60),
                    SilverMedals = rd.Next(30, 40),
                    BronzeMedals = rd.Next(20, 30)
                });
            }
        }

        public ObservableCollection<Medals> MedalDetails
        {
            get;
            set;
        }
    }

    public class Medals
    {
        public int CountryName
        {
            get;
            set;
        }

        public double GoldMedals
        {
            get;
            set;
        }

        public double SilverMedals
        {
            get;
            set;
        }

        public double BronzeMedals
        {
            get;
            set;
        }
    }

    public class DecimalFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return string.Format("{0:0.00}", double.Parse(value.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
