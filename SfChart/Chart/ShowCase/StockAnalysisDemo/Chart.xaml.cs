using Common;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
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
    public sealed partial class Chart : Page, IDisposable
    {
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            StockChart.Visibility = Visibility.Visible;
            //busy.IsBusy = false;
            if (e.Parameter != null)
                this.SelectedData = e.Parameter as List<StockDatas>;
            base.OnNavigatedTo(e);
        }

        private string selectedIndicator;

        public string SelectedIndicator
        {
            get { return selectedIndicator; }
            set
            {
                selectedIndicator = value;
                RaisePropertyChanged("SelectedIndicator");
            }
        }

        public List<string> IndicatorList { get; set; }
        public ObservableCollection<StockDatas> Collections { get; set; }
        public List<StockDatas> SelectedData { get; set; }
        public string Names
        {
            get;
            set;
        }
        public Chart()
        {
            this.InitializeComponent();
            this.PropertyChanged += ChartView_PropertyChanged;
            Collections = new ObservableCollection<StockDatas>();
            SelectedData = new List<StockDatas>();
            this.DataContext = this;
            this.Unloaded += Chart_Unloaded;
            if (IsMobileFamily())
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
        }

        private void Chart_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            FrameNavigationService.CurrentFrame.Navigate(typeof(StockAnalysis));
            if (IsMobileFamily())
            {
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            }
        }

        private bool IsMobileFamily()
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                return true;
            }
            return false;
        }

        void ChartView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (selectedIndicator)
            {
                case "Accumulation Distribution":
                    {

                        HiLoOpenCloseSeries series = StockChart.Series[0] as HiLoOpenCloseSeries;
                        ColumnSeries series1 = StockChart.Series[1] as ColumnSeries;
                        AccumulationDistributionIndicator indicator = new AccumulationDistributionIndicator();
                        NumericalAxis axis1 = new NumericalAxis() { Visibility = Visibility.Collapsed ,ShowGridLines=false};
                        indicator.YAxis = axis1;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        if (StockChart.TechnicalIndicators.Count > 0)
                        {
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                        }
                        StockChart.TechnicalIndicators.Add(indicator);
                        indicator = new AccumulationDistributionIndicator();
                        NumericalAxis axis = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        UI.Xaml.Charts.SfChart.SetRow(axis, 1);
                        indicator.YAxis = axis;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        StockChart.TechnicalIndicators.Add(indicator);
                    }
                    break;
                case "Average True Range":
                    {
                        HiLoOpenCloseSeries series = StockChart.Series[0] as HiLoOpenCloseSeries;
                        ColumnSeries series1 = StockChart.Series[1] as ColumnSeries;
                        AverageTrueRangeIndicator indicator = new AverageTrueRangeIndicator();
                        NumericalAxis axis1 = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        indicator.YAxis = axis1;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        if (StockChart.TechnicalIndicators.Count > 0)
                        {
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                        }
                        StockChart.TechnicalIndicators.Add(indicator);
                        indicator = new AverageTrueRangeIndicator();
                        NumericalAxis axis = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        UI.Xaml.Charts.SfChart.SetRow(axis, 1);
                        indicator.YAxis = axis;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        StockChart.TechnicalIndicators.Add(indicator);
                    }
                    break;
                case "BollingerBand":
                    {
                        HiLoOpenCloseSeries series = StockChart.Series[0] as HiLoOpenCloseSeries;
                        ColumnSeries series1 = StockChart.Series[1] as ColumnSeries;
                        BollingerBandIndicator indicator = new BollingerBandIndicator();
                        NumericalAxis axis1 = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        indicator.YAxis = axis1;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        if (StockChart.TechnicalIndicators.Count > 0)
                        {
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                        }
                        StockChart.TechnicalIndicators.Add(indicator);
                        indicator = new BollingerBandIndicator();
                        NumericalAxis axis = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        UI.Xaml.Charts.SfChart.SetRow(axis, 1);
                        indicator.YAxis = axis;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        StockChart.TechnicalIndicators.Add(indicator);
                    }
                    break;
                case "Exponential Average":
                    {
                        HiLoOpenCloseSeries series = StockChart.Series[0] as HiLoOpenCloseSeries;
                        ColumnSeries series1 = StockChart.Series[1] as ColumnSeries;
                        ExponentialAverageIndicator indicator = new ExponentialAverageIndicator();
                        NumericalAxis axis1 = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        indicator.YAxis = axis1;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        if (StockChart.TechnicalIndicators.Count > 0)
                        {
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                        }
                        StockChart.TechnicalIndicators.Add(indicator);
                        indicator = new ExponentialAverageIndicator();
                        NumericalAxis axis = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        UI.Xaml.Charts.SfChart.SetRow(axis, 1);
                        indicator.YAxis = axis;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        StockChart.TechnicalIndicators.Add(indicator);
                    }
                    break;
                case "MACDTechnical":
                    {
                        HiLoOpenCloseSeries series = StockChart.Series[0] as HiLoOpenCloseSeries;
                        ColumnSeries series1 = StockChart.Series[1] as ColumnSeries;
                        MACDTechnicalIndicator indicator = new MACDTechnicalIndicator();
                        NumericalAxis axis1 = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        indicator.YAxis = axis1;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        if (StockChart.TechnicalIndicators.Count > 0)
                        {
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                        }
                        StockChart.TechnicalIndicators.Add(indicator);
                        indicator = new MACDTechnicalIndicator();
                        NumericalAxis axis = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        UI.Xaml.Charts.SfChart.SetRow(axis, 1);
                        indicator.YAxis = axis;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        StockChart.TechnicalIndicators.Add(indicator);
                    }
                    break;
                case "Momentum Technical":
                    {
                        HiLoOpenCloseSeries series = StockChart.Series[0] as HiLoOpenCloseSeries;
                        ColumnSeries series1 = StockChart.Series[1] as ColumnSeries;
                        MomentumTechnicalIndicator indicator = new MomentumTechnicalIndicator();
                        NumericalAxis axis1 = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        indicator.YAxis = axis1;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        if (StockChart.TechnicalIndicators.Count > 0)
                        {
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                        }
                        StockChart.TechnicalIndicators.Add(indicator);
                        indicator = new MomentumTechnicalIndicator();
                        NumericalAxis axis = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        UI.Xaml.Charts.SfChart.SetRow(axis, 1);
                        indicator.YAxis = axis;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        StockChart.TechnicalIndicators.Add(indicator);
                    }
                    break;
                case "RSITechnical":
                    {
                        HiLoOpenCloseSeries series = StockChart.Series[0] as HiLoOpenCloseSeries;
                        ColumnSeries series1 = StockChart.Series[1] as ColumnSeries;
                        RSITechnicalIndicator indicator = new RSITechnicalIndicator();
                        NumericalAxis axis1 = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        indicator.YAxis = axis1;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        if (StockChart.TechnicalIndicators.Count > 0)
                        {
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                        }
                        StockChart.TechnicalIndicators.Add(indicator);
                        indicator = new RSITechnicalIndicator();
                        NumericalAxis axis = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        UI.Xaml.Charts.SfChart.SetRow(axis, 1);
                        indicator.YAxis = axis;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        StockChart.TechnicalIndicators.Add(indicator);
                    }
                    break;
                case "Simple Average":
                    {
                        HiLoOpenCloseSeries series = StockChart.Series[0] as HiLoOpenCloseSeries;
                        ColumnSeries series1 = StockChart.Series[1] as ColumnSeries;
                        SimpleAverageIndicator indicator = new SimpleAverageIndicator();
                        NumericalAxis axis1 = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        indicator.YAxis = axis1;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        if (StockChart.TechnicalIndicators.Count > 0)
                        {
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                        }
                        StockChart.TechnicalIndicators.Add(indicator);
                        indicator = new SimpleAverageIndicator();
                        NumericalAxis axis = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        UI.Xaml.Charts.SfChart.SetRow(axis, 1);
                        indicator.YAxis = axis;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        StockChart.TechnicalIndicators.Add(indicator);
                    }
                    break;
                case "Triangular Average":
                    {
                        HiLoOpenCloseSeries series = StockChart.Series[0] as HiLoOpenCloseSeries;
                        ColumnSeries series1 = StockChart.Series[1] as ColumnSeries;
                        TriangularAverageIndicator indicator = new TriangularAverageIndicator();
                        NumericalAxis axis1 = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        indicator.YAxis = axis1;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        if (StockChart.TechnicalIndicators.Count > 0)
                        {
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                            StockChart.TechnicalIndicators.Remove(StockChart.TechnicalIndicators[0]);
                        }
                        StockChart.TechnicalIndicators.Add(indicator);
                        indicator = new TriangularAverageIndicator();
                        NumericalAxis axis = new NumericalAxis() { Visibility = Visibility.Collapsed, ShowGridLines = false };
                        UI.Xaml.Charts.SfChart.SetRow(axis, 1);
                        indicator.YAxis = axis;
                        indicator.ItemsSource = series.ItemsSource;
                        indicator.High = series.High;
                        indicator.Low = series.Low;
                        indicator.Open = series.Open;
                        indicator.Close = series.Close;
                        indicator.XBindingPath = series.XBindingPath;
                        indicator.Volume = series.Open;
                        StockChart.TechnicalIndicators.Add(indicator);
                    }
                    break;
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            AddIndicatorOption indicator = e.Content as AddIndicatorOption;
            if (indicator!=null)
            indicator.SelectedData = SelectedData;
            base.OnNavigatedFrom(e);
        }
        public void GenerateData(List<StockDatas> data)
        {
            for (int i = 29; i < data.Count - 1; i++)
            {
                SelectedData.Add(data[i]);
            }

        }

        private void OpenOptions(object sender, EventArgs e)
        {
           // string url = string.Format("/Showcase/StockDetails/View/AddIndicatorOption.xaml");
            // App.RootFrame.Navigate(new Uri(url, UriKind.Relative));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void GridLayout(object sender, RoutedEventArgs e)
        {
            this.StockChart.Visibility = Visibility.Collapsed;
        }

        private void TileLayout(object sender, RoutedEventArgs e)
        {
            this.StockChart.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationService.CurrentFrame.Navigate(typeof(AddIndicatorOption),SelectedData);
        }
        
        public void Dispose()
        {
            if (this.StockChart != null)
            {
                foreach (var series in StockChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                foreach (var series in StockChart.TechnicalIndicators)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.StockChart = null;
            }
            
            this.mainGrid.Resources = null;
        }
    }

}
