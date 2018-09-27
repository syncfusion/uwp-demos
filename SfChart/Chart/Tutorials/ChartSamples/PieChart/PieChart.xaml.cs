using Common;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class PieChart : SampleLayout
    {
        PieChartViewModel viewModel;
        public PieChart()
        {
            viewModel = new PieChartViewModel();
            this.InitializeComponent();
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                this.Accumulation_charts.Series[0].ItemsSource = viewModel.Expenditure;
        }

        private void OnChartTypeSelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedIndex == 0 && viewModel != null)
            {
                if (Accumulation_charts != null)
                {
                    PieSeries series1 = new PieSeries();
                    ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();

                    ChartLegend1.Visibility = Visibility.Visible;

                    DataTemplate template1 = MainGrid.Resources["labelTemplate"] as DataTemplate;
                    adornmentInfo1.ShowLabel = true;
                    adornmentInfo1.LabelTemplate = template1;
                    adornmentInfo1.AdornmentsPosition = AdornmentsPosition.Bottom;
                    adornmentInfo1.HorizontalAlignment = HorizontalAlignment.Center;
                    adornmentInfo1.VerticalAlignment = VerticalAlignment.Center;
                    adornmentInfo1.ShowConnectorLine = true;
                    adornmentInfo1.ConnectorHeight = 80;
                    adornmentInfo1.ShowLabel = true;
                    adornmentInfo1.UseSeriesPalette = true;
                    adornmentInfo1.SegmentLabelContent = LabelContent.LabelContentPath;

                    series1.XBindingPath = "Expense";
                    series1.YBindingPath = "Amount";
                    series1.ItemsSource = viewModel.Expenditure;
                    series1.ConnectorType = ConnectorMode.Bezier;
                    series1.PieCoefficient = 0.7;
                    series1.EnableSmartLabels = true;
                    series1.LabelPosition = CircularSeriesLabelPosition.OutsideExtended;
                    series1.AdornmentsInfo = adornmentInfo1;

                    Accumulation_charts.Header = "Agriculture Expenses Comparison";
                    Accumulation_charts.Series.Clear();
                    Accumulation_charts.Series.Add(series1);
                }
            }
            if (comboBox.SelectedIndex == 1 && viewModel != null)
            {
                PieSeries series1 = new PieSeries();
                PieSeries series2 = new PieSeries();
                PieSeries series3 = new PieSeries();

                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();
                ChartAdornmentInfo adornmentInfo2 = new ChartAdornmentInfo();
                ChartAdornmentInfo adornmentInfo3 = new ChartAdornmentInfo();

                ChartColorModel color1 = new ChartColorModel();
                ChartColorModel color2 = new ChartColorModel();
                ChartColorModel color3 = new ChartColorModel();

                color1.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color1.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color1.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));

                color2.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color2.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color2.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color2.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color2.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));
                color2.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));

                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));

                DataTemplate template1 = MainGrid.Resources["populationLabelTemplate1"] as DataTemplate;
                DataTemplate template2 = MainGrid.Resources["populationLabelTemplate2"] as DataTemplate;
                DataTemplate template3 = MainGrid.Resources["populationLabelTemplate3"] as DataTemplate;

                ChartLegend1.Visibility = Visibility.Collapsed;

                adornmentInfo1.ShowLabel = true;
                adornmentInfo1.SegmentLabelContent = LabelContent.LabelContentPath;
                adornmentInfo1.LabelTemplate = template1;

                adornmentInfo2.ShowLabel = true;
                adornmentInfo2.SegmentLabelContent = LabelContent.LabelContentPath;
                adornmentInfo2.LabelTemplate = template2;

                adornmentInfo3.ShowLabel = true;
                adornmentInfo3.SegmentLabelContent = LabelContent.LabelContentPath;
                adornmentInfo3.LabelTemplate = template3;

                series1.ItemsSource = viewModel.Population;
                series1.XBindingPath = "Continent";
                series1.YBindingPath = "PopulationinContinents";
                series1.AdornmentsInfo = adornmentInfo1;
                series1.Stroke = new SolidColorBrush(Colors.White);
                series1.PieCoefficient = 1;
                series1.Label = "Continents";
                series1.Palette = ChartColorPalette.Custom;
                series1.ColorModel = color1;

                series2.ItemsSource = viewModel.Population;
                series2.XBindingPath = "Countries";
                series2.YBindingPath = "PopulationinCountries";
                series2.AdornmentsInfo = adornmentInfo2;
                series2.Stroke = new SolidColorBrush(Colors.White);
                series2.PieCoefficient = 1;
                series2.Label = "Countries";
                series2.Palette = ChartColorPalette.Custom;
                series2.ColorModel = color2;

                series3.ItemsSource = viewModel.Population;
                series3.XBindingPath = "States";
                series3.YBindingPath = "PopulationinStates";
                series3.AdornmentsInfo = adornmentInfo3;
                series3.Stroke = new SolidColorBrush(Colors.White);
                series3.PieCoefficient = 1;
                series3.Label = "States";
                series3.Palette = ChartColorPalette.Custom;
                series3.ColorModel = color3;

                Accumulation_charts.Header = "Most populated continents";
                Accumulation_charts.Series.Clear();
                Accumulation_charts.Series.Add(series1);
                Accumulation_charts.Series.Add(series2);
                Accumulation_charts.Series.Add(series3);
            }
            if (comboBox.SelectedIndex == 2 && viewModel != null)
            {
                PieSeries series1 = new PieSeries();
                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();

                ChartLegend1.Visibility = Visibility.Collapsed;

                series1.Margin = new Thickness(0, 25, 0, 0);
                adornmentInfo1.ShowLabel = true;
                adornmentInfo1.ShowConnectorLine = true;
                adornmentInfo1.UseSeriesPalette = true;
                adornmentInfo1.ConnectorHeight = 37;
                adornmentInfo1.SegmentLabelContent = LabelContent.Percentage;
                adornmentInfo1.SegmentLabelFormat = "##.#";

                series1.XBindingPath = "Utilization";
                series1.YBindingPath = "ResponseTime";
                series1.StartAngle = 180;
                series1.EndAngle = 360;
                series1.ConnectorType = ConnectorMode.Bezier;
                series1.EnableSmartLabels = true;
                series1.LabelPosition = CircularSeriesLabelPosition.Outside;
                series1.ItemsSource = viewModel.Metric;
                series1.AdornmentsInfo = adornmentInfo1;

                Accumulation_charts.Header = "Application Performance Metrics";
                Accumulation_charts.Series.Clear();
                Accumulation_charts.Series.Add(series1);
            }
            if (comboBox.SelectedIndex == 3 && viewModel != null)
            {
                DoughnutSeries series1 = new DoughnutSeries();
                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();

                ChartLegend1.Visibility = Visibility.Visible;

                DataTemplate template1 = MainGrid.Resources["labeltemplate"] as DataTemplate;
                adornmentInfo1.ShowLabel = true;
                adornmentInfo1.LabelTemplate = template1;

                series1.XBindingPath = "CompanyName";
                series1.YBindingPath = "CompanyTurnover";
                series1.ItemsSource = viewModel.CompanyDetails;
                series1.AdornmentsInfo = adornmentInfo1;

                Accumulation_charts.Series.Clear();
                Accumulation_charts.Header = "Top car company's turnover";
                Accumulation_charts.Series.Add(series1);
            }
            if (comboBox.SelectedIndex == 4 && viewModel != null)
            {
                DoughnutSeries series1 = new DoughnutSeries();
                DoughnutSeries series2 = new DoughnutSeries();
                DoughnutSeries series3 = new DoughnutSeries();

                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();
                ChartAdornmentInfo adornmentInfo2 = new ChartAdornmentInfo();
                ChartAdornmentInfo adornmentInfo3 = new ChartAdornmentInfo();

                ChartColorModel color1 = new ChartColorModel();
                ChartColorModel color2 = new ChartColorModel();
                ChartColorModel color3 = new ChartColorModel();

                color1.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color1.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color1.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));

                color2.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color2.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color2.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color2.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color2.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));
                color2.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));

                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 233, 70, 73)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 15, 185, 84)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));
                color3.CustomBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 82, 119)));

                DataTemplate template1 = MainGrid.Resources["populationLabelTemplate1"] as DataTemplate;
                DataTemplate template2 = MainGrid.Resources["populationLabelTemplate2"] as DataTemplate;
                DataTemplate template3 = MainGrid.Resources["populationLabelTemplate3"] as DataTemplate;

                ChartLegend1.Visibility = Visibility.Collapsed;

                adornmentInfo1.ShowLabel = true;
                adornmentInfo1.SegmentLabelContent = LabelContent.LabelContentPath;
                adornmentInfo1.LabelTemplate = template1;

                adornmentInfo2.ShowLabel = true;
                adornmentInfo2.SegmentLabelContent = LabelContent.LabelContentPath;
                adornmentInfo2.LabelTemplate = template2;

                adornmentInfo3.ShowLabel = true;
                adornmentInfo3.SegmentLabelContent = LabelContent.LabelContentPath;
                adornmentInfo3.LabelTemplate = template3;

                series1.ItemsSource = viewModel.Population;
                series1.XBindingPath = "Continent";
                series1.YBindingPath = "PopulationinContinents";
                series1.AdornmentsInfo = adornmentInfo1;
                series1.Stroke = new SolidColorBrush(Colors.White);
                series1.DoughnutCoefficient = 1;
                series1.DoughnutSize = 1;
                series1.Label = "Continents";
                series1.Palette = ChartColorPalette.Custom;
                series1.ColorModel = color1;

                series2.ItemsSource = viewModel.Population;
                series2.XBindingPath = "Countries";
                series2.YBindingPath = "PopulationinCountries";
                series2.AdornmentsInfo = adornmentInfo2;
                series2.Stroke = new SolidColorBrush(Colors.White);
                series2.DoughnutCoefficient = 1;
                series2.DoughnutSize = 1;
                series2.Label = "Countries";
                series2.Palette = ChartColorPalette.Custom;
                series2.ColorModel = color2;

                series3.ItemsSource = viewModel.Population;
                series3.XBindingPath = "States";
                series3.YBindingPath = "PopulationinStates";
                series3.AdornmentsInfo = adornmentInfo3;
                series3.Stroke = new SolidColorBrush(Colors.White);
                series3.DoughnutCoefficient = 1;
                series3.DoughnutSize = 1;
                series3.Label = "States";
                series3.Palette = ChartColorPalette.Custom;
                series3.ColorModel = color3;

                Accumulation_charts.Header = "Most populated continents";
                Accumulation_charts.Series.Clear();
                Accumulation_charts.Series.Add(series1);
                Accumulation_charts.Series.Add(series2);
                Accumulation_charts.Series.Add(series3);
            }
            if (comboBox.SelectedIndex == 5 && viewModel != null)
            {
                DoughnutSeries series1 = new DoughnutSeries();
                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();

                ChartLegend1.Visibility = Visibility.Collapsed;

                adornmentInfo1.ShowLabel = true;
                adornmentInfo1.ShowConnectorLine = true;
                adornmentInfo1.UseSeriesPalette = true;
                adornmentInfo1.ConnectorHeight = 37;
                adornmentInfo1.SegmentLabelContent = LabelContent.Percentage;
                adornmentInfo1.SegmentLabelFormat = "##.#";

                series1.XBindingPath = "Utilization";
                series1.YBindingPath = "ResponseTime";
                series1.StartAngle = 180;
                series1.EndAngle = 360;
                series1.ConnectorType = ConnectorMode.Bezier;
                series1.EnableSmartLabels = true;
                series1.LabelPosition = CircularSeriesLabelPosition.Outside;
                series1.Margin = new Thickness(0, 25, 0, 0);
                series1.ItemsSource = viewModel.Metric;
                series1.AdornmentsInfo = adornmentInfo1;

                Accumulation_charts.Header = "Application Performance Metrics";
                Accumulation_charts.Series.Clear();
                Accumulation_charts.Series.Add(series1);
            }
            if (comboBox.SelectedIndex == 6 && viewModel != null)
            {
                PyramidSeries series1 = new PyramidSeries();
                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();

                ChartLegend1.Visibility = Visibility.Collapsed;

                DataTemplate template1 = MainGrid.Resources["labelTemplate"] as DataTemplate;
                adornmentInfo1.ShowLabel = true;
                adornmentInfo1.LabelTemplate = template1;

                series1.XBindingPath = "CompanyName";
                series1.YBindingPath = "CompanyTurnover";
                series1.ItemsSource = viewModel.CompanyDetails;
                series1.AdornmentsInfo = adornmentInfo1;
                series1.Margin = new Thickness(20, 0, 20, 20);

                Accumulation_charts.Header = "Top car company's turnover";
                Accumulation_charts.Margin = new Thickness(20, 20, 20, 20);
                Accumulation_charts.Series.Clear();
                Accumulation_charts.Series.Add(series1);
            }
            if (comboBox.SelectedIndex == 7 && viewModel != null)
            {
                FunnelSeries series1 = new FunnelSeries();
                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();

                ChartLegend1.Visibility = Visibility.Collapsed;

                DataTemplate template1 = MainGrid.Resources["labelTemplate"] as DataTemplate;
                adornmentInfo1.ShowLabel = true;
                adornmentInfo1.LabelTemplate = template1;

                series1.XBindingPath = "CompanyName";
                series1.YBindingPath = "CompanyTurnover";
                series1.ItemsSource = viewModel.CompanyDetails;
                series1.AdornmentsInfo = adornmentInfo1;
                series1.Margin = new Thickness(20, 0, 20, 20);

                Accumulation_charts.Header = "Top car company's turnover";
                Accumulation_charts.Margin = new Thickness(20, 20, 20, 20);
                Accumulation_charts.Series.Clear();
                Accumulation_charts.Series.Add(series1);
            }
        }

        public override void Dispose()
        {
            if (this.DataContext is PieChartViewModel)
                (this.DataContext as PieChartViewModel).Dispose();

            if (this.Accumulation_charts != null)
            {
                foreach (var series in this.Accumulation_charts.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.Accumulation_charts = null;
            }

            this.Resources = null;

            base.Dispose();
        }

        private void Accumulation_charts_Loaded(object sender, global::Windows.UI.Xaml.RoutedEventArgs e)
        {
            DataTemplate labelTemplate = MainGrid.Resources["labelTemplate"] as DataTemplate;
            ChartAdornmentInfo chartAdornmentInfo = new ChartAdornmentInfo();
            chartAdornmentInfo.AdornmentsPosition = AdornmentsPosition.Bottom;
            chartAdornmentInfo.ShowConnectorLine = true;
            chartAdornmentInfo.UseSeriesPalette = true;
            chartAdornmentInfo.ShowLabel = true;
            chartAdornmentInfo.ConnectorHeight = 80;
            chartAdornmentInfo.SegmentLabelContent = LabelContent.LabelContentPath;
            chartAdornmentInfo.LabelTemplate = labelTemplate;
            chartAdornmentInfo.HorizontalAlignment = HorizontalAlignment.Center;
            chartAdornmentInfo.VerticalAlignment = VerticalAlignment.Center;
            pieSeries.AdornmentsInfo = chartAdornmentInfo;
        }
    }

    public class CompanyExpense
    {
        public string Expense
        {
            get;
            set;
        }
        public double Amount
        {
            get;
            set;
        }
    }

    public class CompanyDetail
    {
        public string CompanyName
        {
            get;
            set;
        }
        public double CompanyTurnover
        {
            get;
            set;
        }
    }

    public class Metrics
    {
        public double Utilization
        {
            get;
            set;
        }
        public double ResponseTime
        {
            get;
            set;
        }
    }

    public class Populations
    {
        public string Continent { get; set; }

        public string Countries { get; set; }

        public string States { get; set; }

        public double PopulationinStates { get; set; }

        public double PopulationinCountries { get; set; }

        public double PopulationinContinents { get; set; }
    }

    public class PieChartViewModel : IDisposable
    {
        public PieChartViewModel()
        {
            this.Expenditure = new List<CompanyExpense>();
            Expenditure.Add(new CompanyExpense() { Expense = "Seeds", Amount = 7658d });
            Expenditure.Add(new CompanyExpense() { Expense = "Fertilizers", Amount = 7090d });
            Expenditure.Add(new CompanyExpense() { Expense = "Insurance", Amount = 3577d });
            Expenditure.Add(new CompanyExpense() { Expense = "Labor", Amount = 1473d });
            Expenditure.Add(new CompanyExpense() { Expense = "Warehousing", Amount = 820d });
            Expenditure.Add(new CompanyExpense() { Expense = "Taxes", Amount = 571d });
            Expenditure.Add(new CompanyExpense() { Expense = "Truck", Amount = 462d });

            CompanyDetails = new List<CompanyDetail>();
            CompanyDetails.Add(new CompanyDetail() { CompanyName = "Rolls Royce", CompanyTurnover = 750000 });
            CompanyDetails.Add(new CompanyDetail() { CompanyName = "Benz", CompanyTurnover = 500000 });
            CompanyDetails.Add(new CompanyDetail() { CompanyName = "Audi", CompanyTurnover = 450000 });
            CompanyDetails.Add(new CompanyDetail() { CompanyName = "BMW", CompanyTurnover = 700000 });
            CompanyDetails.Add(new CompanyDetail() { CompanyName = "Mahindra", CompanyTurnover = 350000 });
            CompanyDetails.Add(new CompanyDetail() { CompanyName = "Jaguar", CompanyTurnover = 650000 });
            CompanyDetails.Add(new CompanyDetail() { CompanyName = "Hero Honda", CompanyTurnover = 250000 });

            Metric = new List<Metrics>();
            Metric.Add(new Metrics() { ResponseTime = 43, Utilization = 32 });
            Metric.Add(new Metrics() { ResponseTime = 20, Utilization = 34 });
            Metric.Add(new Metrics() { ResponseTime = 67, Utilization = 41 });
            Metric.Add(new Metrics() { ResponseTime = 52, Utilization = 42 });
            Metric.Add(new Metrics() { ResponseTime = 71, Utilization = 48 });
            Metric.Add(new Metrics() { ResponseTime = 30, Utilization = 45 });

            this.Population = new List<Populations>();
            Population.Add(new Populations() { Continent = "Asia", Countries = "China", States = "Taiwan", PopulationinContinents = 50.02, PopulationinCountries = 26.02, PopulationinStates = 18.02 });
            Population.Add(new Populations() { Continent = "Africa", Countries = "India", States = "Shandong", PopulationinContinents = 20.81, PopulationinCountries = 24, PopulationinStates = 8 });
            Population.Add(new Populations() { Continent = "Europe", Countries = "Nigeria", States = "UP", PopulationinContinents = 15.37, PopulationinCountries = 12.81, PopulationinStates = 14.5 });
            Population.Add(new Populations() { Countries = "Ethiopia", States = "Maharashtra", PopulationinCountries = 8, PopulationinStates = 9.5 });
            Population.Add(new Populations() { Countries = "Germany", States = "Kano", PopulationinCountries = 8.37, PopulationinStates = 7.81 });
            Population.Add(new Populations() { Countries = "Turkey", States = "Lagos", PopulationinCountries = 7, PopulationinStates = 5 });
            Population.Add(new Populations() { States = "Oromia", PopulationinStates = 5 });
            Population.Add(new Populations() { States = "Amhara", PopulationinStates = 3 });
            Population.Add(new Populations() { States = "Hessen", PopulationinStates = 5.37 });
            Population.Add(new Populations() { States = "Bayern", PopulationinStates = 3 });
            Population.Add(new Populations() { States = "Istanbul", PopulationinStates = 4.5 });
            Population.Add(new Populations() { States = "Ankara", PopulationinStates = 2.5 });

            AdornmentsFac = new ResourceFactory();
            AdornmentInfo = new ChartAdornmentInfo();
            AdornmentInfo1 = new ChartAdornmentInfo();
            AdornmentInfo2 = new ChartAdornmentInfo();
            AdornmentInfo3 = new ChartAdornmentInfo();
            AdornmentInfo4 = new ChartAdornmentInfo();
            AdornmentInfo5 = new ChartAdornmentInfo();
            AdornmentInfo6 = new ChartAdornmentInfo();
            AdornmentInfo7 = new ChartAdornmentInfo();
            AdornmentInfo8 = new ChartAdornmentInfo();
            AdornmentInfo9 = new ChartAdornmentInfo();
            AdornmentInfo10 = new ChartAdornmentInfo();
            AdornmentInfo11 = new ChartAdornmentInfo();

            AdornmentInfo.HorizontalAlignment = HorizontalAlignment.Center;
            AdornmentInfo.VerticalAlignment = VerticalAlignment.Center;
            AdornmentInfo.ShowConnectorLine = true;
            AdornmentInfo.UseSeriesPalette = true;
            AdornmentInfo.ConnectorHeight = 30;
            AdornmentInfo.ShowLabel = true;
            AdornmentInfo.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo.LabelTemplate = AdornmentsFac.labelTemplate8;

            AdornmentInfo1.ShowLabel = true;
            AdornmentInfo1.FontSize = 10;
            AdornmentInfo1.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo1.LabelTemplate = AdornmentsFac.labelTemplate21;

            AdornmentInfo2.ShowLabel = true;
            AdornmentInfo2.FontSize = 10;
            AdornmentInfo2.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo2.LabelTemplate = AdornmentsFac.labelTemplate22;

            AdornmentInfo3.ShowLabel = true;
            AdornmentInfo3.FontSize = 10;
            AdornmentInfo3.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo3.LabelTemplate = AdornmentsFac.labelTemplate23;

            AdornmentInfo4.ShowLabel = true;
            AdornmentInfo4.SegmentLabelContent = LabelContent.Percentage;
            AdornmentInfo4.SegmentLabelFormat = "##.#";
            AdornmentInfo4.AdornmentsPosition = AdornmentsPosition.Bottom;
            AdornmentInfo4.HorizontalAlignment = HorizontalAlignment.Center;
            AdornmentInfo4.ConnectorHeight = 30;
            AdornmentInfo4.VerticalAlignment = VerticalAlignment.Center;
            AdornmentInfo4.ShowConnectorLine = true;
            AdornmentInfo4.UseSeriesPalette = true;

            AdornmentInfo5.ShowLabel = true;
            AdornmentInfo5.UseSeriesPalette = true;

            AdornmentInfo6.ShowLabel = true;
            AdornmentInfo6.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo6.AdornmentsPosition = AdornmentsPosition.Bottom;
            AdornmentInfo6.LabelTemplate = AdornmentsFac.labelTemplate21;

            AdornmentInfo7.ShowLabel = true;
            AdornmentInfo7.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo7.AdornmentsPosition = AdornmentsPosition.Bottom;
            AdornmentInfo7.LabelTemplate = AdornmentsFac.labelTemplate22;

            AdornmentInfo8.ShowLabel = true;
            AdornmentInfo8.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo8.AdornmentsPosition = AdornmentsPosition.Bottom;
            AdornmentInfo8.LabelTemplate = AdornmentsFac.labelTemplate23;

            AdornmentInfo9.ShowLabel = true;
            AdornmentInfo9.ShowConnectorLine = true;
            AdornmentInfo9.UseSeriesPalette = true;
            AdornmentInfo9.ConnectorHeight = 17;

            AdornmentInfo10.ShowLabel = true;
            AdornmentInfo11.ShowLabel = true;
        }
        public ResourceFactory AdornmentsFac { get; set; }

        public ChartAdornmentInfo AdornmentInfo { get; set; }

        public ChartAdornmentInfo AdornmentInfo1 { get; set; }

        public ChartAdornmentInfo AdornmentInfo2 { get; set; }

        public ChartAdornmentInfo AdornmentInfo3 { get; set; }

        public ChartAdornmentInfo AdornmentInfo4 { get; set; }

        public ChartAdornmentInfo AdornmentInfo5 { get; set; }

        public ChartAdornmentInfo AdornmentInfo6 { get; set; }

        public ChartAdornmentInfo AdornmentInfo7 { get; set; }

        public ChartAdornmentInfo AdornmentInfo8 { get; set; }

        public ChartAdornmentInfo AdornmentInfo9 { get; set; }

        public ChartAdornmentInfo AdornmentInfo10 { get; set; }

        public ChartAdornmentInfo AdornmentInfo11 { get; set; }

        public IList<CompanyExpense> Expenditure
        {
            get;
            set;
        }
        public IList<CompanyDetail> CompanyDetails
        {
            get;
            set;
        }

        public IList<Metrics> Metric
        {
            get;
            set;
        }

        public IList<Populations> Population
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.Expenditure != null)
                this.Expenditure.Clear();
        }
    }

    public class LabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ChartAdornment)
            {
                ChartAdornment pieAdornment = value as ChartAdornment;
                return String.Format((pieAdornment.Item as CompanyExpense).Expense + " : $ " + pieAdornment.YData);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class ColorConverter : IValueConverter
    {
        private SolidColorBrush ApplyLight(Color color)
        {
            return new SolidColorBrush(Color.FromArgb(color.A, (byte)(color.R * 0.9), (byte)(color.G * 0.9), (byte)(color.B * 0.9)));
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && (value is ChartAdornment))
            {
                ChartAdornment pieAdornment = value as ChartAdornment;
                int index = pieAdornment.Series.Adornments.IndexOf(pieAdornment);
                SolidColorBrush brush = pieAdornment.Series.ColorModel.GetBrush(index) as SolidColorBrush;
                return ApplyLight(brush.Color);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class PopulationLabelConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartAdornment pieAdornment = value as ChartAdornment;
            if ((pieAdornment.Item as Populations).Continent != null)
            {
                return String.Format((pieAdornment.Item as Populations).Continent);
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class PopulationLabelConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartAdornment pieAdornment = value as ChartAdornment;
            if ((pieAdornment.Item as Populations).Countries != null)
            {
                return String.Format((pieAdornment.Item as Populations).Countries);
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class PopulationLabelConverter3 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartAdornment pieAdornment = value as ChartAdornment;
            return String.Format((pieAdornment.Item as Populations).States);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
