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
using System.Collections.ObjectModel;
using System.Globalization;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public sealed partial class PieChart : SampleLayout
    {
        PieChartViewModel viewModel;
        SolidColorBrush series1Interior;
        SolidColorBrush series2Interior;
        SolidColorBrush series3Interior;

        public PieChart()
        {
            Resources.MergedDictionaries.Add(SfChart.Resources.ColorModelResource.Resource);
            viewModel = new PieChartViewModel();
            this.InitializeComponent();
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                this.Accumulation_charts.Series[0].ItemsSource = viewModel.CountryDetails;

            series1Interior = Resources["SeriesInterior1"] as SolidColorBrush;
            series2Interior = Resources["SeriesInterior2"] as SolidColorBrush;
            series3Interior = Resources["SeriesInterior3"] as SolidColorBrush;
        }

        private void OnChartTypeSelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            Accumulation_charts.Legend = null;

            if (comboBox.SelectedIndex == 0 && viewModel != null)
            {
                if (Accumulation_charts != null)
                {
                    if (Accumulation_charts.Legend == null)
                    {
                        Accumulation_charts.Legend = new ChartLegend()
                        {
                            CornerRadius = new CornerRadius(0),
                            FontSize = 15,
                            DockPosition = ChartDock.Right,
                            BorderThickness = new Thickness(1)
                        };
                    }

                    PieSeries series1 = new PieSeries();
                    series1.Palette = ChartColorPalette.Custom;
                    series1.ColorModel = Resources["customBrush"] as ChartColorModel;
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

                    series1.XBindingPath = "Countries";
                    series1.YBindingPath = "Count";
                    series1.ItemsSource = viewModel.CountryDetails;
                    series1.PieCoefficient = 0.7;
                    series1.EnableSmartLabels = true;
                    series1.LabelPosition = CircularSeriesLabelPosition.OutsideExtended;
                    series1.AdornmentsInfo = adornmentInfo1;
                    series1.GroupMode = PieGroupMode.Value;
                    series1.GroupTo = 1000;

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

                color1.CustomBrushes.Add(series1Interior);
                color1.CustomBrushes.Add(series2Interior);
                color1.CustomBrushes.Add(series3Interior);

                color2.CustomBrushes.Add(series1Interior);
                color2.CustomBrushes.Add(series1Interior);
                color2.CustomBrushes.Add(series2Interior);
                color2.CustomBrushes.Add(series2Interior);
                color2.CustomBrushes.Add(series3Interior);
                color2.CustomBrushes.Add(series3Interior);

                color3.CustomBrushes.Add(series1Interior);
                color3.CustomBrushes.Add(series1Interior);
                color3.CustomBrushes.Add(series1Interior);
                color3.CustomBrushes.Add(series1Interior);
                color3.CustomBrushes.Add(series2Interior);
                color3.CustomBrushes.Add(series2Interior);
                color3.CustomBrushes.Add(series2Interior);
                color3.CustomBrushes.Add(series2Interior);
                color3.CustomBrushes.Add(series3Interior);
                color3.CustomBrushes.Add(series3Interior);
                color3.CustomBrushes.Add(series3Interior);
                color3.CustomBrushes.Add(series3Interior);

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
                series1.Palette = ChartColorPalette.Custom;
                series1.ColorModel = Resources["customBrush"] as ChartColorModel;
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
                if (Accumulation_charts.Legend == null)
                {
                    Accumulation_charts.Legend = new ChartLegend()
                    {
                        CornerRadius = new CornerRadius(0),
                        DockPosition = ChartDock.Right,
                        BorderThickness = new Thickness(1)
                    };
                }

                DoughnutSeries series1 = new DoughnutSeries();
                series1.Palette = ChartColorPalette.Custom;
                series1.ColorModel = Resources["customBrush"] as ChartColorModel;

                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();
                ChartLegend1.Visibility = Visibility.Visible;
                adornmentInfo1.ShowLabel = true;

                series1.XBindingPath = "CompanyName";
                series1.YBindingPath = "CompanyTurnover";
                series1.ItemsSource = viewModel.CompanyDetails;
                series1.AdornmentsInfo = adornmentInfo1;
                series1.ExplodeOnMouseClick = true;
                var centerView = new ContentControl() { ContentTemplate = MainGrid.Resources["doughnutTemplate"] as DataTemplate };
                centerView.DataContext = viewModel;
                series1.CenterView = centerView;

                Binding binding = new Binding();
                binding.Source = viewModel;
                binding.Mode = BindingMode.TwoWay;
                binding.Path = new PropertyPath("SelectedIndex");
                series1.SetBinding(DoughnutSeries.ExplodeIndexProperty, binding);

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

                color1.CustomBrushes.Add(series1Interior);
                color1.CustomBrushes.Add(series2Interior);
                color1.CustomBrushes.Add(series3Interior);

                color2.CustomBrushes.Add(series1Interior);
                color2.CustomBrushes.Add(series1Interior);
                color2.CustomBrushes.Add(series2Interior);
                color2.CustomBrushes.Add(series2Interior);
                color2.CustomBrushes.Add(series3Interior);
                color2.CustomBrushes.Add(series3Interior);

                color3.CustomBrushes.Add(series1Interior);
                color3.CustomBrushes.Add(series1Interior);
                color3.CustomBrushes.Add(series1Interior);
                color3.CustomBrushes.Add(series1Interior);
                color3.CustomBrushes.Add(series2Interior);
                color3.CustomBrushes.Add(series2Interior);
                color3.CustomBrushes.Add(series2Interior);
                color3.CustomBrushes.Add(series2Interior);
                color3.CustomBrushes.Add(series3Interior);
                color3.CustomBrushes.Add(series3Interior);
                color3.CustomBrushes.Add(series3Interior);
                color3.CustomBrushes.Add(series3Interior);

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

                series1.Palette = ChartColorPalette.Custom;
                series1.ColorModel = Resources["customBrush"] as ChartColorModel;

                series1.XBindingPath = "Utilization";
                series1.YBindingPath = "ResponseTime";
                series1.StartAngle = 180;
                series1.EndAngle = 360;
                series1.EnableSmartLabels = true;
                series1.LabelPosition = CircularSeriesLabelPosition.Outside;
                series1.Margin = new Thickness(0, 25, 0, 0);
                series1.ItemsSource = viewModel.Metric;
                series1.AdornmentsInfo = adornmentInfo1;

                Accumulation_charts.Header = "Application Performance Metrics";
                Accumulation_charts.Series.Clear();
                Accumulation_charts.Series.Add(series1);
            }
            if(comboBox.SelectedIndex == 6 && viewModel != null)
            {
                DoughnutSeries doughnutSeries = new DoughnutSeries();
                doughnutSeries.DataContext = viewModel;
                doughnutSeries.XBindingPath = "XValue";
                doughnutSeries.YBindingPath = "YValue";
                doughnutSeries.ItemsSource = (doughnutSeries.DataContext as PieChartViewModel).DoughnutSeriesData;
                doughnutSeries.IsStackedDoughnut = true;
                doughnutSeries.EnableAnimation = true;
                doughnutSeries.LegendIcon = ChartLegendIcon.Circle;
                doughnutSeries.StartAngle = -90;
                doughnutSeries.EndAngle = 270;
                doughnutSeries.SegmentSpacing = 0.2;
                doughnutSeries.DoughnutCoefficient = 0.8;
                doughnutSeries.CapStyle = DoughnutCapStyle.BothCurve;
                doughnutSeries.MaximumValue = 100;

                var colorModel = new ChartColorModel();
                var customBrushes = new List<Brush>();
                customBrushes.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0x47, 0xBA, 0x9F)));
                customBrushes.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xE5, 0x88, 0x70)));
                customBrushes.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0x96, 0x86, 0xC9)));
                customBrushes.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xE5, 0x65, 0x90)));
                
                colorModel.CustomBrushes = customBrushes;
                doughnutSeries.ColorModel = colorModel;
                doughnutSeries.Palette = ChartColorPalette.Custom;
                var image = new Image() { Source = new BitmapImage(new Uri("ms-appx:///Chart/Tutorials/ChartSamples/PieChart/Images/Person.png", UriKind.RelativeOrAbsolute)) };
                var centerView = new ContentControl() { Content = image, HorizontalAlignment= HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                
                var binding = new Binding();
                binding.Source = doughnutSeries;
                binding.Path = new PropertyPath("InnerRadius");
                binding.Converter = new ImageSizeConverter();
                binding.ConverterParameter = doughnutSeries;
                binding.Mode = BindingMode.TwoWay;
                centerView.SetBinding(ContentControl.WidthProperty, binding);
                centerView.SetBinding(ContentControl.HeightProperty, binding);
                doughnutSeries.CenterView = centerView;

                var legend = new ChartLegend() { DockPosition = ChartDock.Bottom };
                legend.ItemTemplate = MainGrid.Resources["stackedDoughnutTemplate"] as DataTemplate;

                Accumulation_charts.Legend = legend;
                Accumulation_charts.Series.Clear();
                Accumulation_charts.Series.Add(doughnutSeries);
                Accumulation_charts.Header = "Percentage of Loan Closure";
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
    }

    public class StackedDoughnutModel
    {
        public string XValue
        {
            get;
            set;
        }

        public double YValue
        {
            get;
            set;
        }

        public Uri Image
        {
            get;
            set;
        }

        public StackedDoughnutModel(string xValue, double yValue, Uri image)
        {
            XValue = xValue;
            YValue = yValue;
            Image = image;
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

        public double Count { get; set; }

        public string States { get; set; }

        public double PopulationinStates { get; set; }

        public double PopulationinCountries { get; set; }

        public double PopulationinContinents { get; set; }
    }

    public class PieChartViewModel : INotifyPropertyChanged, IDisposable
    {
        public PieChartViewModel()
        {
            DoughnutSeriesData = new ObservableCollection<StackedDoughnutModel>
            {
                new StackedDoughnutModel("Vehicle", 62.7, new Uri("ms-appx:///Chart/Tutorials/ChartSamples/PieChart/Images/Car.png", UriKind.RelativeOrAbsolute)),
                new StackedDoughnutModel("Education",29.5, new Uri("ms-appx:///Chart/Tutorials/ChartSamples/PieChart/Images/Chart_Book.png", UriKind.RelativeOrAbsolute)),
                new StackedDoughnutModel("Home", 85.2, new Uri("ms-appx:///Chart/Tutorials/ChartSamples/PieChart/Images/House.png", UriKind.RelativeOrAbsolute)),
                new StackedDoughnutModel("Personal", 45.6, new Uri("ms-appx:///Chart/Tutorials/ChartSamples/PieChart/Images/Personal.png", UriKind.RelativeOrAbsolute))
            };

            this.CountryDetails = new List<Populations>();
            CountryDetails.Add(new Populations() { Countries = "Uruguay", Count = 2807 });
            CountryDetails.Add(new Populations() { Countries = "Argentina", Count = 2577 });
            CountryDetails.Add(new Populations() { Countries = "USA", Count = 2473 });
            CountryDetails.Add(new Populations() { Countries = "Germany", Count = 2120 });
            CountryDetails.Add(new Populations() { Countries = "Netherlands", Count = 2071 });
            CountryDetails.Add(new Populations() { Countries = "Malta", Count = 960 });
            CountryDetails.Add(new Populations() { Countries = "Maldives", Count = 941 });
            CountryDetails.Add(new Populations() { Countries = "Monaco", Count = 908 });
            
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

            SelectedIndex = 0;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string SelectedItemName
        {
            get { return _selectedItemName; }
            set
            {
                _selectedItemName = value;
                RaisePropertyChanged(nameof(SelectedItemName));
            }
        }

        public double SelectedItemsPercentage
        {
            get { return _selectedItemsPercentage; }
            set
            {
                _selectedItemsPercentage = value;
                RaisePropertyChanged(nameof(SelectedItemsPercentage));
            }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (selectedIndex == -1) return;

                SelectedItemName = CompanyDetails[selectedIndex].CompanyName;

                var sum = CompanyDetails.Sum(item => item.CompanyTurnover);
                var selectedValue = CompanyDetails[selectedIndex].CompanyTurnover;

                SelectedItemsPercentage = (selectedValue / sum) * 100;
                SelectedItemsPercentage = Math.Floor(SelectedItemsPercentage * 100) / 100;
            }
        }

        private string _selectedItemName;

        private double _selectedItemsPercentage;

        private int selectedIndex = -1;

        public IList<Populations> CountryDetails { get; set; }

        public IList<StackedDoughnutModel> DoughnutSeriesData { get; set; }
        
        public void Dispose()
        {
            if (this.CountryDetails != null)
                this.CountryDetails.Clear();
        }
    }

    public class LabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartAdornment pieAdornment = value as ChartAdornment;

            if (pieAdornment == null) return value;

            var model = pieAdornment.Item as Populations;
            
            if (model != null)
            {
                return String.Format(model.Countries + " : " + pieAdornment.YData);
            }
            else
            {
                var list = pieAdornment.Item as List<object>;
                string labelData = "";

                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i] as Populations;
                    labelData = labelData + String.Format(item.Countries + " : " + item.Count);

                    if (i + 1 != list.Count)
                    {
                        labelData = labelData + Environment.NewLine;
                    }
                }

                return labelData;
            }
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

    public class ImageSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return value;
            }

            var size = (double)value;

            return double.IsNaN(size) ? 0 : size * 1.80;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class PercentageFormatConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var percentage = value.ToString() + " %";
            return percentage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
