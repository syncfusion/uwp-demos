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
        double size = 30;
        ChartTrackBallBehavior tackballBehavior;
        FastHiLoOpenCloseBitmapSeries hiLoOpenCloseBitmap1;
        public FastChartDemo()
        {
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
                OHLCBitXAML.SetValue(ChartTooltip.VerticalOffsetProperty, size);

                FBitMapXAML.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                FBitMapXAML.SetValue(ChartTooltip.VerticalOffsetProperty, size);
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
                financialChart.Visibility = Visibility.Visible;
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.SecondaryAxis.Header = "Stock Price";
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                FastColumnBitmapSeries hiLoSeries = new FastColumnBitmapSeries();
                hiLoSeries.XBindingPath = "Date";
                hiLoSeries.YBindingPath = "Price";
                hiLoSeries.ItemsSource = viewModel.List;
                hiLoSeries.ShowTooltip = true;
                hiLoSeries.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                hiLoSeries.SetValue(ChartTooltip.VerticalOffsetProperty, size);
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Clear();
                financialChart.Series.Add(hiLoSeries);
            }
            else if (comboBox.SelectedIndex == 1 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.SecondaryAxis.Header = "Stock Price";
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                FastLineBitmapSeries hiLoSeries = new FastLineBitmapSeries();
                hiLoSeries.XBindingPath = "Date";
                hiLoSeries.YBindingPath = "Price";
                hiLoSeries.ItemsSource = viewModel.LineData;
                hiLoSeries.ShowTooltip = false;
                hiLoSeries.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                hiLoSeries.SetValue(ChartTooltip.VerticalOffsetProperty, size);
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Clear();
                financialChart.Series.Add(hiLoSeries);
            }
            else if (comboBox.SelectedIndex == 2 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                financialChart.Behaviors.Add(tackballBehavior);
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                financialChart.SecondaryAxis.Header = "Stock Price";
                FastRangeAreaBitmapSeries fastRangeAreaBitmapSeries = new FastRangeAreaBitmapSeries();
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
                financialChart.Visibility = Visibility.Visible;
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.SecondaryAxis.Header = "Stock Price";
                DataTemplate tooltipTemplate = grid.Resources["tooltipTemplate"] as DataTemplate;
                hiLoOpenCloseBitmap1 = new FastHiLoOpenCloseBitmapSeries();
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                hiLoOpenCloseBitmap1.XBindingPath = "TimeStamp";
                hiLoOpenCloseBitmap1.High = "High";
                hiLoOpenCloseBitmap1.Low = "Low";
                hiLoOpenCloseBitmap1.Open = "Open";
                hiLoOpenCloseBitmap1.Close = "Last";
                hiLoOpenCloseBitmap1.ItemsSource = viewModel.HiloViewModel;
                hiLoOpenCloseBitmap1.ShowTooltip = true;
                hiLoOpenCloseBitmap1.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                hiLoOpenCloseBitmap1.SetValue(ChartTooltip.VerticalOffsetProperty, size);
                hiLoOpenCloseBitmap1.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                hiLoOpenCloseBitmap1.SetValue(ChartTooltip.VerticalOffsetProperty, size);
                financialChart.Series.Clear();
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Add(hiLoOpenCloseBitmap1);
            }
            else if (comboBox.SelectedIndex == 8 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                DataTemplate tooltipTemplate = grid.Resources["tooltipTemplate1"] as DataTemplate;
                FastHiLoBitmapSeries hiLoOpenClose = new FastHiLoBitmapSeries();
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
                hiLoOpenClose.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                hiLoOpenClose.SetValue(ChartTooltip.VerticalOffsetProperty, size);
            }
            else if (comboBox.SelectedIndex == 4 && financialChart != null)
            {
                financialChart.Visibility = Visibility.Collapsed;
                BarSeries.Visibility = Visibility.Visible;
            }
            else if (comboBox.SelectedIndex == 5 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.SecondaryAxis.Header = "Stock Price";
                FastStepLineBitmapSeries hiLoSeries = new FastStepLineBitmapSeries();
                hiLoSeries.XBindingPath = "Date";
                hiLoSeries.YBindingPath = "Price";
                hiLoSeries.ItemsSource = viewModel.List;
                hiLoSeries.ShowTooltip = true;
                hiLoSeries.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                hiLoSeries.SetValue(ChartTooltip.VerticalOffsetProperty, size);
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Clear();
                financialChart.Series.Add(hiLoSeries);
            }
            else if (comboBox.SelectedIndex == 6 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                financialChart.SecondaryAxis.Header = "Stock Price";
                FastScatterBitmapSeries hiLoSeries = new FastScatterBitmapSeries();
                hiLoSeries.XBindingPath = "Date";
                hiLoSeries.YBindingPath = "Price";
                hiLoSeries.ItemsSource = viewModel.LineData;
                hiLoSeries.ShowTooltip = true;
                hiLoSeries.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                hiLoSeries.SetValue(ChartTooltip.VerticalOffsetProperty, size);
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Clear();
                financialChart.Series.Add(hiLoSeries);
            }
            else if (comboBox.SelectedIndex == 7 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                financialChart.PrimaryAxis.Header = "Date";
                financialChart.SecondaryAxis.Header = "Stock Price";
                financialChart.PrimaryAxis.LabelFormat = "MMM/dd";
                DataTemplate tooltipTemplate = grid.Resources["tooltipTemplate"] as DataTemplate;
                FastCandleBitmapSeries hiLoOpenClose = new FastCandleBitmapSeries();
                hiLoOpenClose.XBindingPath = "TimeStamp";
                hiLoOpenClose.High = "High";
                hiLoOpenClose.Low = "Low";
                hiLoOpenClose.Open = "Open";
                hiLoOpenClose.Close = "Last";
                hiLoOpenClose.ItemsSource = viewModel.HiloViewModel1;
                hiLoOpenClose.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                hiLoOpenClose.SetValue(ChartTooltip.VerticalOffsetProperty, size);
                hiLoOpenClose.ShowTooltip = true;
                financialChart.Series.Clear();
                financialChart.Series.Add(hiLoOpenClose);
            }
            else if (comboBox.SelectedIndex == 9 && financialChart != null)
            {
                BarSeries.Visibility = Visibility.Collapsed;
                financialChart.Visibility = Visibility.Visible;
                financialChart.DataContext = new FastStackingColumnChartViewModel();
                financialChart.PrimaryAxis.LabelFormat = "";
                financialChart.PrimaryAxis.Header = "Product Id";
                financialChart.SecondaryAxis.Header = "Stock";
                FastStackingColumnBitmapSeries stackingSeries = new FastStackingColumnBitmapSeries();
                stackingSeries.XBindingPath = "CountryName";
                stackingSeries.YBindingPath = "GoldMedals";
                stackingSeries.ItemsSource = (financialChart.DataContext as FastStackingColumnChartViewModel).MedalDetails;
                stackingSeries.ShowTooltip = true;
                stackingSeries.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                stackingSeries.SetValue(ChartTooltip.VerticalOffsetProperty, size);
                FastStackingColumnBitmapSeries stackingSeries1 = new FastStackingColumnBitmapSeries();
                stackingSeries1.XBindingPath = "CountryName";
                stackingSeries1.YBindingPath = "SilverMedals";
                stackingSeries1.ItemsSource = (financialChart.DataContext as FastStackingColumnChartViewModel).MedalDetails;
                stackingSeries1.ShowTooltip = true;
                stackingSeries1.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                stackingSeries1.SetValue(ChartTooltip.VerticalOffsetProperty, size);
                FastStackingColumnBitmapSeries stackingSeries2 = new FastStackingColumnBitmapSeries();
                stackingSeries2.XBindingPath = "CountryName";
                stackingSeries2.YBindingPath = "BronzeMedals";
                stackingSeries2.ItemsSource = (financialChart.DataContext as FastStackingColumnChartViewModel).MedalDetails;
                stackingSeries2.ShowTooltip = true;
                stackingSeries2.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                stackingSeries2.SetValue(ChartTooltip.VerticalOffsetProperty, size);
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Clear();
                financialChart.Series.Add(stackingSeries);
                financialChart.Series.Add(stackingSeries1);
                financialChart.Series.Add(stackingSeries2);
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
