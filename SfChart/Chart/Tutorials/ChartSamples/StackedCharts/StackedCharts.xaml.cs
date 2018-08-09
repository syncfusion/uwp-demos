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
    public sealed partial class StackedCharts : SampleLayout
    {
        public StackingColumnChartViewModel ColumnChartViewModel
        {
            get;
            set;
        }

        public StackingBarChartViewModel BarChartViewModel
        {
            get;
            set;
        }

        public StackingAreaChartViewModel AreaChartViewModel
        {
            get;
            set;
        }

        public StackedCharts()
        {
            this.InitializeComponent();
            ColumnChartViewModel = new StackingColumnChartViewModel();
            BarChartViewModel = new StackingBarChartViewModel();
            AreaChartViewModel = new StackingAreaChartViewModel();
            this.DataContext = this;

            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                sbsChartAdornmentInfo1.ShowMarker = false;
                sbsChartAdornmentInfo1.ShowLabel = true;
                sbsChartAdornmentInfo1.HorizontalAlignment = HorizontalAlignment.Left;
                sbsChartAdornmentInfo1.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                sbsChartAdornmentInfo2.ShowMarker = false;
                sbsChartAdornmentInfo2.ShowLabel = true;
                sbsChartAdornmentInfo2.HorizontalAlignment = HorizontalAlignment.Left;
                sbsChartAdornmentInfo2.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                sbsChartAdornmentInfo3.ShowMarker = false;
                sbsChartAdornmentInfo3.ShowLabel = true;
                sbsChartAdornmentInfo3.HorizontalAlignment = HorizontalAlignment.Left;
                sbsChartAdornmentInfo3.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                sb100ChartAdornmentInfo1.ShowMarker = false;
                sb100ChartAdornmentInfo1.ShowLabel = true;
                sb100ChartAdornmentInfo1.HorizontalAlignment = HorizontalAlignment.Left;
                sb100ChartAdornmentInfo1.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                sb100ChartAdornmentInfo2.ShowMarker = false;
                sb100ChartAdornmentInfo2.ShowLabel = true;
                sb100ChartAdornmentInfo2.HorizontalAlignment = HorizontalAlignment.Left;
                sb100ChartAdornmentInfo2.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                sb100ChartAdornmentInfo3.ShowMarker = false;
                sb100ChartAdornmentInfo3.ShowLabel = true;
                sb100ChartAdornmentInfo3.HorizontalAlignment = HorizontalAlignment.Left;
                sb100ChartAdornmentInfo3.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                saChartAdornmentInfo1.ShowMarker = false;
                saChartAdornmentInfo1.ShowLabel = true;
                saChartAdornmentInfo1.SegmentLabelContent = LabelContent.LabelContentPath;
                saChartAdornmentInfo1.LabelTemplate = MainGrid.Resources["stackingAreaSeriesLT1"] as DataTemplate;

                saChartAdornmentInfo2.ShowMarker = false;
                saChartAdornmentInfo2.ShowLabel = true;
                saChartAdornmentInfo2.SegmentLabelContent = LabelContent.LabelContentPath;
                saChartAdornmentInfo2.LabelTemplate = MainGrid.Resources["stackingAreaSeriesLT2"] as DataTemplate;

                saChartAdornmentInfo3.ShowMarker = false;
                saChartAdornmentInfo3.ShowLabel = true;
                saChartAdornmentInfo3.SegmentLabelContent = LabelContent.LabelContentPath;
                saChartAdornmentInfo3.LabelTemplate = MainGrid.Resources["stackingAreaSeriesLT3"] as DataTemplate;

                sa100ChartAdornmentInfo1.ShowMarker = false;
                sa100ChartAdornmentInfo1.ShowLabel = true;
                sa100ChartAdornmentInfo1.SegmentLabelContent = LabelContent.LabelContentPath;
                sa100ChartAdornmentInfo1.LabelTemplate = MainGrid.Resources["stackingArea100SeriesLT1"] as DataTemplate;

                sa100ChartAdornmentInfo2.ShowMarker = false;
                sa100ChartAdornmentInfo2.ShowLabel = true;
                sa100ChartAdornmentInfo2.SegmentLabelContent = LabelContent.LabelContentPath;
                sa100ChartAdornmentInfo2.LabelTemplate = MainGrid.Resources["stackingArea100SeriesLT2"] as DataTemplate;

                sa100ChartAdornmentInfo3.ShowMarker = false;
                sa100ChartAdornmentInfo3.ShowLabel = true;
                sa100ChartAdornmentInfo3.SegmentLabelContent = LabelContent.LabelContentPath;
                sa100ChartAdornmentInfo3.LabelTemplate = MainGrid.Resources["stackingArea100SeriesLT3"] as DataTemplate;

                scChartAdornmentInfo1.ShowMarker = false;
                scChartAdornmentInfo1.ShowLabel = true;
                scChartAdornmentInfo1.HorizontalAlignment = HorizontalAlignment.Center;
                scChartAdornmentInfo1.VerticalAlignment = VerticalAlignment.Bottom;
                scChartAdornmentInfo1.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                scChartAdornmentInfo2.ShowMarker = false;
                scChartAdornmentInfo2.ShowLabel = true;
                scChartAdornmentInfo2.HorizontalAlignment = HorizontalAlignment.Center;
                scChartAdornmentInfo2.VerticalAlignment = VerticalAlignment.Bottom;
                scChartAdornmentInfo2.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                scChartAdornmentInfo3.ShowMarker = false;
                scChartAdornmentInfo3.ShowLabel = true;
                scChartAdornmentInfo3.HorizontalAlignment = HorizontalAlignment.Center;
                scChartAdornmentInfo3.VerticalAlignment = VerticalAlignment.Bottom;
                scChartAdornmentInfo3.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                sc100ChartAdornmentInfo1.ShowMarker = false;
                sc100ChartAdornmentInfo1.ShowLabel = true;
                sc100ChartAdornmentInfo1.HorizontalAlignment = HorizontalAlignment.Center;
                sc100ChartAdornmentInfo1.VerticalAlignment = VerticalAlignment.Bottom;
                sc100ChartAdornmentInfo1.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                sc100ChartAdornmentInfo2.ShowMarker = false;
                sc100ChartAdornmentInfo2.ShowLabel = true;
                sc100ChartAdornmentInfo2.HorizontalAlignment = HorizontalAlignment.Center;
                sc100ChartAdornmentInfo2.VerticalAlignment = VerticalAlignment.Bottom;
                sc100ChartAdornmentInfo2.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                sc100ChartAdornmentInfo3.ShowMarker = false;
                sc100ChartAdornmentInfo3.ShowLabel = true;
                sc100ChartAdornmentInfo3.HorizontalAlignment = HorizontalAlignment.Center;
                sc100ChartAdornmentInfo3.VerticalAlignment = VerticalAlignment.Bottom;
                sc100ChartAdornmentInfo3.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;
            }
        }

        private void OnSelectStackSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stBarChart == null) return;
            if (selectStack != null)
            {
                if (selectStack.SelectedIndex == 0)
                {
                    stBarChart.PrimaryAxis.ZoomFactor = 1;
                    stBarChart.SecondaryAxis.ZoomFactor = 1;
                    stBarChart.Visibility = Visibility.Visible;
                    stColumnChart.Visibility = Visibility.Collapsed;
                    stAreaChart.Visibility = Visibility.Collapsed;
                    stBar100Chart.Visibility = Visibility.Collapsed;
                    stColumn100Chart.Visibility = Visibility.Collapsed;
                    stArea100Chart.Visibility = Visibility.Collapsed;
                }
                else if (selectStack.SelectedIndex == 1)
                {
                    stColumnChart.PrimaryAxis.ZoomFactor = 1;
                    stColumnChart.SecondaryAxis.ZoomFactor = 1;
                    stBarChart.Visibility = Visibility.Collapsed;
                    stColumnChart.Visibility = Visibility.Visible;
                    stAreaChart.Visibility = Visibility.Collapsed;
                    stBar100Chart.Visibility = Visibility.Collapsed;
                    stColumn100Chart.Visibility = Visibility.Collapsed;
                    stArea100Chart.Visibility = Visibility.Collapsed;
                }
                else if (selectStack.SelectedIndex == 2)
                {
                    stAreaChart.PrimaryAxis.ZoomFactor = 1;
                    stAreaChart.SecondaryAxis.ZoomFactor = 1;
                    stBarChart.Visibility = Visibility.Collapsed;
                    stColumnChart.Visibility = Visibility.Collapsed;
                    stAreaChart.Visibility = Visibility.Visible;
                    stBar100Chart.Visibility = Visibility.Collapsed;
                    stColumn100Chart.Visibility = Visibility.Collapsed;
                    stArea100Chart.Visibility = Visibility.Collapsed;
                }
                else if (selectStack.SelectedIndex == 3)
                {
                    stAreaChart.PrimaryAxis.ZoomFactor = 1;
                    stAreaChart.SecondaryAxis.ZoomFactor = 1;
                    stBarChart.Visibility = Visibility.Collapsed;
                    stColumnChart.Visibility = Visibility.Collapsed;
                    stAreaChart.Visibility = Visibility.Collapsed;
                    stBar100Chart.Visibility = Visibility.Visible;
                    stColumn100Chart.Visibility = Visibility.Collapsed;
                    stArea100Chart.Visibility = Visibility.Collapsed;
                }
                else if (selectStack.SelectedIndex == 4)
                {
                    stAreaChart.PrimaryAxis.ZoomFactor = 1;
                    stAreaChart.SecondaryAxis.ZoomFactor = 1;
                    stBarChart.Visibility = Visibility.Collapsed;
                    stColumnChart.Visibility = Visibility.Collapsed;
                    stAreaChart.Visibility = Visibility.Collapsed;
                    stBar100Chart.Visibility = Visibility.Collapsed;
                    stColumn100Chart.Visibility = Visibility.Visible;
                    stArea100Chart.Visibility = Visibility.Collapsed;
                }
                else if (selectStack.SelectedIndex == 5)
                {
                    stAreaChart.PrimaryAxis.ZoomFactor = 1;
                    stAreaChart.SecondaryAxis.ZoomFactor = 1;
                    stBarChart.Visibility = Visibility.Collapsed;
                    stColumnChart.Visibility = Visibility.Collapsed;
                    stAreaChart.Visibility = Visibility.Collapsed;
                    stBar100Chart.Visibility = Visibility.Collapsed;
                    stColumn100Chart.Visibility = Visibility.Collapsed;
                    stArea100Chart.Visibility = Visibility.Visible;
                }
            }
        }

        public override void Dispose()
        {
            if (this.DataContext is StackingColumnChartViewModel)
                (this.DataContext as StackingColumnChartViewModel).Dispose();

            if (this.DataContext is StackingBarChartViewModel)
                (this.DataContext as StackingBarChartViewModel).Dispose();

            if (this.DataContext is StackingAreaChartViewModel)
                (this.DataContext as StackingAreaChartViewModel).Dispose();

            if (this.stBarChart != null)
            {
                foreach (var series in this.stBarChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.stBarChart = null;
            }

            if (this.stBar100Chart != null)
            {
                foreach (var series in this.stBar100Chart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.stBar100Chart = null;
            }

            if (this.stAreaChart != null)
            {
                foreach (var series in this.stAreaChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.stAreaChart = null;
            }

            if (this.stArea100Chart != null)
            {
                foreach (var series in this.stArea100Chart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.stArea100Chart = null;
            }

            if (this.stColumnChart != null)
            {
                foreach (var series in this.stColumnChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.stColumnChart = null;
            }

            if (this.stColumn100Chart != null)
            {
                foreach (var series in this.stColumn100Chart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.stColumn100Chart = null;
            }

            this.Resources = null;

            base.Dispose();
        }
    }

    public class StackingColumnChartViewModel : IDisposable
    {
        public StackingColumnChartViewModel()
        {
            this.MedalDetails = new ObservableCollection<Medal>();

            MedalDetails.Add(new Medal() { CountryName = "USA", GoldMedals = 395, SilverMedals = 319, BronzeMedals = 296 });
            MedalDetails.Add(new Medal() { CountryName = "Germany", GoldMedals = 247, SilverMedals = 284, BronzeMedals = 320 });
            MedalDetails.Add(new Medal() { CountryName = "UK", GoldMedals = 207, SilverMedals = 255, BronzeMedals = 253 });
            MedalDetails.Add(new Medal() { CountryName = "France", GoldMedals = 191, SilverMedals = 212, BronzeMedals = 233 });
            MedalDetails.Add(new Medal() { CountryName = "Italy", GoldMedals = 190, SilverMedals = 157, BronzeMedals = 174 });
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                MedalDetails.Add(new Medal() { CountryName = "Sweden", GoldMedals = 142, SilverMedals = 160, BronzeMedals = 173 });
                MedalDetails.Add(new Medal() { CountryName = "Australia", GoldMedals = 131, SilverMedals = 137, BronzeMedals = 164 });
                MedalDetails.Add(new Medal() { CountryName = "Japan", GoldMedals = 123, SilverMedals = 112, BronzeMedals = 126 });
            }
            ResourceFac = new ResourceFactory();
            AdornmentInfo5 = new ChartAdornmentInfo();
            AdornmentInfo51 = new ChartAdornmentInfo();
            AdornmentInfo52 = new ChartAdornmentInfo();
            AdornmentInfo50 = new ChartAdornmentInfo();
            AdornmentInfo510 = new ChartAdornmentInfo();
            AdornmentInfo520 = new ChartAdornmentInfo();

            AdornmentInfo5.HorizontalAlignment = HorizontalAlignment.Center;
            AdornmentInfo5.VerticalAlignment = VerticalAlignment.Bottom;
            AdornmentInfo5.ShowLabel = true;
            AdornmentInfo5.LabelTemplate = ResourceFac.labelTemplate10;

            AdornmentInfo51.HorizontalAlignment = HorizontalAlignment.Center;
            AdornmentInfo51.VerticalAlignment = VerticalAlignment.Bottom;
            AdornmentInfo51.ShowLabel = true;
            AdornmentInfo51.LabelTemplate = ResourceFac.labelTemplate10;

            AdornmentInfo52.HorizontalAlignment = HorizontalAlignment.Center;
            AdornmentInfo52.VerticalAlignment = VerticalAlignment.Bottom;
            AdornmentInfo52.ShowLabel = true;
            AdornmentInfo52.LabelTemplate = ResourceFac.labelTemplate10;

            AdornmentInfo50.HorizontalAlignment = HorizontalAlignment.Center;
            AdornmentInfo50.VerticalAlignment = VerticalAlignment.Bottom;
            AdornmentInfo50.ShowLabel = true;
            AdornmentInfo50.LabelTemplate = ResourceFac.labelTemplate10;

            AdornmentInfo510.HorizontalAlignment = HorizontalAlignment.Center;
            AdornmentInfo510.VerticalAlignment = VerticalAlignment.Bottom;
            AdornmentInfo510.ShowLabel = true;
            AdornmentInfo510.LabelTemplate = ResourceFac.labelTemplate10;

            AdornmentInfo520.HorizontalAlignment = HorizontalAlignment.Center;
            AdornmentInfo520.VerticalAlignment = VerticalAlignment.Bottom;
            AdornmentInfo520.ShowLabel = true;
            AdornmentInfo520.LabelTemplate = ResourceFac.labelTemplate10;
        }
        public ResourceFactory ResourceFac { get; set; }
        public ChartAdornmentInfo AdornmentInfo5 { get; set; }
        public ChartAdornmentInfo AdornmentInfo51 { get; set; }
        public ChartAdornmentInfo AdornmentInfo52 { get; set; }
        public ChartAdornmentInfo AdornmentInfo50 { get; set; }
        public ChartAdornmentInfo AdornmentInfo510 { get; set; }
        public ChartAdornmentInfo AdornmentInfo520 { get; set; }

        public ObservableCollection<Medal> MedalDetails
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.MedalDetails != null)
                this.MedalDetails.Clear();
        }
    }

    public class StackingBarChartViewModel : IDisposable
    {

        public StackingBarChartViewModel()
        {
            this.GoldInventoryDetails = new ObservableCollection<GoldInventory>();

            GoldInventoryDetails.Add(new GoldInventory() { Year = 2005, Inferred = 1100, Measured = 750, Reserves = 900 });
            GoldInventoryDetails.Add(new GoldInventory() { Year = 2006, Inferred = 1200, Measured = 500, Reserves = 1000 });
            GoldInventoryDetails.Add(new GoldInventory() { Year = 2007, Inferred = 900, Measured = 700, Reserves = 1200 });
            GoldInventoryDetails.Add(new GoldInventory() { Year = 2008, Inferred = 950, Measured = 1000, Reserves = 900 });
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                GoldInventoryDetails.Add(new GoldInventory() { Year = 2009, Inferred = 900, Measured = 1100, Reserves = 1000 });
                GoldInventoryDetails.Add(new GoldInventory() { Year = 2010, Inferred = 900, Measured = 1200, Reserves = 1000 });
                GoldInventoryDetails.Add(new GoldInventory() { Year = 2011, Inferred = 1000, Measured = 1100, Reserves = 1050 });
            }

            ResourceFac = new ResourceFactory();

            AdornmentInfo1 = new ChartAdornmentInfo();
            AdornmentInfo11 = new ChartAdornmentInfo();
            AdornmentInfo12 = new ChartAdornmentInfo();
            AdornmentInfo10 = new ChartAdornmentInfo();
            AdornmentInfo110 = new ChartAdornmentInfo();
            AdornmentInfo120 = new ChartAdornmentInfo();
            AdornmentInfo2 = new ChartAdornmentInfo();
            AdornmentInfo5 = new ChartAdornmentInfo();
            AdornmentInfo51 = new ChartAdornmentInfo();
            AdornmentInfo52 = new ChartAdornmentInfo();

            AdornmentInfo1.ShowLabel = true;
            AdornmentInfo1.FontSize = 10;
            AdornmentInfo1.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdornmentInfo1.Foreground = new SolidColorBrush(Colors.White);

            AdornmentInfo11.ShowLabel = true;
            AdornmentInfo11.FontSize = 10;
            AdornmentInfo11.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdornmentInfo11.Foreground = new SolidColorBrush(Colors.White);

            AdornmentInfo12.ShowLabel = true;
            AdornmentInfo12.FontSize = 10;
            AdornmentInfo12.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdornmentInfo12.Foreground = new SolidColorBrush(Colors.White);

            AdornmentInfo10.ShowLabel = true;
            AdornmentInfo10.FontSize = 10;
            AdornmentInfo10.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdornmentInfo10.Foreground = new SolidColorBrush(Colors.White);

            AdornmentInfo110.ShowLabel = true;
            AdornmentInfo110.FontSize = 10;
            AdornmentInfo110.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdornmentInfo110.Foreground = new SolidColorBrush(Colors.White);

            AdornmentInfo120.ShowLabel = true;
            AdornmentInfo120.FontSize = 10;
            AdornmentInfo120.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdornmentInfo120.Foreground = new SolidColorBrush(Colors.White);

            AdornmentInfo2.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo2.ShowLabel = true;
            AdornmentInfo2.LabelTemplate = ResourceFac.labelTemplate12;

            AdornmentInfo5.HorizontalAlignment = HorizontalAlignment.Center;
            AdornmentInfo5.VerticalAlignment = VerticalAlignment.Bottom;
            AdornmentInfo5.ShowLabel = true;
            AdornmentInfo5.LabelTemplate = ResourceFac.labelTemplate10;

            AdornmentInfo51.HorizontalAlignment = HorizontalAlignment.Center;
            AdornmentInfo51.VerticalAlignment = VerticalAlignment.Bottom;
            AdornmentInfo51.ShowLabel = true;
            AdornmentInfo51.LabelTemplate = ResourceFac.labelTemplate10;

            AdornmentInfo52.HorizontalAlignment = HorizontalAlignment.Center;
            AdornmentInfo52.VerticalAlignment = VerticalAlignment.Bottom;
            AdornmentInfo52.ShowLabel = true;
            AdornmentInfo52.LabelTemplate = ResourceFac.labelTemplate10;
        }
        public ResourceFactory ResourceFac { get; set; }
        public ChartAdornmentInfo AdornmentInfo1 { get; set; }
        public ChartAdornmentInfo AdornmentInfo11 { get; set; }
        public ChartAdornmentInfo AdornmentInfo12 { get; set; }
        public ChartAdornmentInfo AdornmentInfo10 { get; set; }
        public ChartAdornmentInfo AdornmentInfo110 { get; set; }
        public ChartAdornmentInfo AdornmentInfo120 { get; set; }
        public ChartAdornmentInfo AdornmentInfo2 { get; set; }
        public ChartAdornmentInfo AdornmentInfo5 { get; set; }
        public ChartAdornmentInfo AdornmentInfo51 { get; set; }
        public ChartAdornmentInfo AdornmentInfo52 { get; set; }

        public ObservableCollection<GoldInventory> GoldInventoryDetails
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.GoldInventoryDetails != null)
                this.GoldInventoryDetails.Clear();
        }
    }

    public class StackingAreaChartViewModel : IDisposable
    {
        public ResourceFactory ResourceFac { get; set; }
        public ChartAdornmentInfo AdornmentInfo1 { get; set; }
        public ChartAdornmentInfo AdornmentInfo2 { get; set; }
        public ChartAdornmentInfo AdornmentInfo3 { get; set; }
        public ChartAdornmentInfo AdornmentInfo11 { get; set; }
        public ChartAdornmentInfo AdornmentInfo12 { get; set; }
        public ChartAdornmentInfo AdornmentInfo13 { get; set; }
        public ChartAdornmentInfo AdornmentInfo4 { get; set; }

        public StackingAreaChartViewModel()
        {
            AdornmentInfo1 = new ChartAdornmentInfo();
            AdornmentInfo2 = new ChartAdornmentInfo();
            AdornmentInfo3 = new ChartAdornmentInfo();
            AdornmentInfo11 = new ChartAdornmentInfo();
            AdornmentInfo12 = new ChartAdornmentInfo();
            AdornmentInfo13 = new ChartAdornmentInfo();
            AdornmentInfo4 = new ChartAdornmentInfo();
            ResourceFac = new ResourceFactory();

            AdornmentInfo1.ShowLabel = true;
            AdornmentInfo1.FontSize = 10;
            AdornmentInfo1.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdornmentInfo1.Foreground = new SolidColorBrush(Colors.White);

            AdornmentInfo2.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo2.ShowLabel = true;
            AdornmentInfo2.LabelTemplate = ResourceFac.labelTemplate12;

            AdornmentInfo3.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo3.ShowLabel = true;
            AdornmentInfo3.LabelTemplate = ResourceFac.labelTemplate13;

            AdornmentInfo11.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo11.ShowLabel = true;
            AdornmentInfo11.LabelTemplate = ResourceFac.labelTemplate12;

            AdornmentInfo12.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo12.ShowLabel = true;
            AdornmentInfo12.LabelTemplate = ResourceFac.labelTemplate13;

            AdornmentInfo13.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo13.ShowLabel = true;
            AdornmentInfo13.VerticalAlignment = VerticalAlignment.Bottom;
            AdornmentInfo13.LabelTemplate = ResourceFac.labelTemplate14;

            AdornmentInfo4.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo4.ShowLabel = true;
            AdornmentInfo4.LabelTemplate = ResourceFac.labelTemplate14;

            this.TemperatureData = new ObservableCollection<Temperatue>();

            TemperatureData.Add(new Temperatue()
            {
                Margin = new Thickness(25, 0, 0, 0),
                Year = "2008",
                Autumn = 30,
                Spring = 35,
                Summer = 43,
                Winter = 29
            });
            TemperatureData.Add(new Temperatue()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Year = "2009",
                Autumn = 30,
                Spring = 35,
                Summer = 43,
                Winter = 29
            });
            TemperatureData.Add(new Temperatue()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Year = "2010",
                Autumn = 32,
                Spring = 37,
                Summer = 47,
                Winter = 27
            });
            TemperatureData.Add(new Temperatue()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Year = "2011",
                Autumn = 34,
                Spring = 35,
                Summer = 43,
                Winter = 25
            });
            TemperatureData.Add(new Temperatue()
            {
                Margin = AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile" ?
                                                        new Thickness(0, 0, 0, 0) : new Thickness(0, 0, 25, 0),
                Year = "2012",
                Autumn = 28,
                Spring = 36,
                Summer = 49,
                Winter = 27
            });
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                TemperatureData.Add(new Temperatue()
                {
                    Margin = new Thickness(0, 0, 25, 0),
                    Year = "2013",
                    Autumn = 31,
                    Spring = 30,
                    Summer = 52,
                    Winter = 30
                });
            }
        }
        public ObservableCollection<Temperatue> TemperatureData
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.TemperatureData != null)
                this.TemperatureData.Clear();
        }
    }

    public class Temperatue
    {
        public Thickness Margin
        {
            get;
            set;
        }

        public string Year
        {
            get;
            set;
        }

        public double Spring
        {
            get;
            set;
        }

        public double Summer
        {
            get;
            set;
        }

        public double Autumn
        {
            get;
            set;
        }

        public double Winter
        {
            get;
            set;
        }

    }

    public class Medal
    {
        public string CountryName
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

    public class GoldInventory
    {
        public double Year
        {
            get;
            set;
        }

        public double Inferred
        {
            get;
            set;
        }

        public double Measured
        {
            get;
            set;
        }

        public double Reserves
        {
            get;
            set;
        }
    }
}
