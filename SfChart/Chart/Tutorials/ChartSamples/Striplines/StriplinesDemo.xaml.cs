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
    public sealed partial class StriplinesDemo : SampleLayout
    {
        public StriplinesDemo()
        {
            this.InitializeComponent();
        }

        public override void Dispose()
        {
            if ((this.DataContext as StriplineDemoViewModel) != null)
                (this.DataContext as StriplineDemoViewModel).Dispose();

            if (this.StriplineDemoChart != null)
            {
                foreach (var series in this.StriplineDemoChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.StriplineDemoChart = null;
            }

            if (this.StriplineDemoChart1 != null)
            {
                foreach (var series in this.StriplineDemoChart1.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.StriplineDemoChart1 = null;
            }

            this.Resources = null;

            base.Dispose();
        }

        private void splineSeries_Loaded(object sender, RoutedEventArgs e)
        {
            DataTemplate labelTemplate1 = MainGrid.Resources["adornment"] as DataTemplate;
            DataTemplate labelTemplate2 = MainGrid.Resources["adornment"] as DataTemplate;
            DataTemplate adornShape = MainGrid.Resources["adornShape"] as DataTemplate;
            DataTemplate adornShape1 = MainGrid.Resources["adornShape"] as DataTemplate;

            ChartAdornmentInfo chartAdornmentInfo1 = new ChartAdornmentInfo();
            ChartAdornmentInfo chartAdornmentInfo2 = new ChartAdornmentInfo();

            chartAdornmentInfo1.ShowMarker = true;
            chartAdornmentInfo1.ShowLabel = true;
            chartAdornmentInfo1.Symbol = ChartSymbol.Custom;
            chartAdornmentInfo1.LabelTemplate = labelTemplate1;
            chartAdornmentInfo1.SymbolTemplate = adornShape;

            chartAdornmentInfo2.ShowMarker = true;
            chartAdornmentInfo2.ShowLabel = true;
            chartAdornmentInfo2.Symbol = ChartSymbol.Custom;
            chartAdornmentInfo2.LabelTemplate = labelTemplate2;
            chartAdornmentInfo2.SymbolTemplate = adornShape1;

            splineSeries.AdornmentsInfo = chartAdornmentInfo1;
            splineSeries2.AdornmentsInfo = chartAdornmentInfo2;
        }
    }

    public class StriplineDemoViewModel : IDisposable
    {
        public ObservableCollection<StriplineDemoDataModel> ClimateData
        {
            get;
            set;
        }

        public StriplineDemoViewModel()
        {
            ClimateData = new ObservableCollection<StriplineDemoDataModel>();
            ClimateData.Add(new StriplineDemoDataModel()
            {
                ID = 1,
                Month = "Jan",
                Quarter = "Q1",
                Temperature = 33,
                Rainfall = 24,
                RelativeHumidity = 75
            });
            ClimateData.Add(new StriplineDemoDataModel()
            {
                ID = 2,
                Month = "Feb",
                Quarter = "Q1",
                Temperature = 37,
                Rainfall = 7,
                RelativeHumidity = 73
            });
            ClimateData.Add(new StriplineDemoDataModel()
            {
                ID = 3,
                Month = "Mar",
                Quarter = "Q1",
                Temperature = 39,
                Rainfall = 15,
                RelativeHumidity = 71
            });
            ClimateData.Add(new StriplineDemoDataModel()
            {
                ID = 4,
                Month = "Apr",
                Quarter = "Q2",
                Temperature = 43,
                Rainfall = 25,
                RelativeHumidity = 71
            });
            ClimateData.Add(new StriplineDemoDataModel()
            {
                ID = 5,
                Month = "May",
                Quarter = "Q2",
                Temperature = 45,
                Rainfall = 52,
                RelativeHumidity = 65
            });
            ClimateData.Add(new StriplineDemoDataModel()
            {
                ID = 6,
                Month = "Jun",
                Quarter = "Q2",
                Temperature = 43,
                Rainfall = 53,
                RelativeHumidity = 59
            });
            ClimateData.Add(new StriplineDemoDataModel()
            {
                ID = 7,
                Month = "Jul",
                Quarter = "Q3",
                Temperature = 41,
                Rainfall = 84,
                RelativeHumidity = 64
            });
            ClimateData.Add(new StriplineDemoDataModel()
            {
                ID = 8,
                Month = "Aug",
                Quarter = "Q3",
                Temperature = 40,
                Rainfall = 124,
                RelativeHumidity = 68
            });
            ClimateData.Add(new StriplineDemoDataModel()
            {
                ID = 9,
                Month = "Sep",
                Quarter = "Q3",
                Temperature = 39,
                Rainfall = 118,
                RelativeHumidity = 71
            });
            ClimateData.Add(new StriplineDemoDataModel()
            {
                ID = 10,
                Month = "Oct",
                Quarter = "Q4",
                Temperature = 39,
                Rainfall = 267,
                RelativeHumidity = 79
            });
            ClimateData.Add(new StriplineDemoDataModel()
            {
                ID = 11,
                Month = "Nov",
                Quarter = "Q4",
                Temperature = 34,
                Rainfall = 289,
                RelativeHumidity = 80
            });
            ClimateData.Add(new StriplineDemoDataModel()
            {
                ID = 12,
                Month = "Dec",
                Quarter = "Q4",
                Temperature = 33,
                Rainfall = 139,
                RelativeHumidity = 77
            });

            ResourceFac = new ResourceFactory();
            AdornmentInfo = new ChartAdornmentInfo();
            AdornmentInfo1 = new ChartAdornmentInfo();
            AdornmentInfo.ShowLabel = true;
            AdornmentInfo.ShowMarker = true;
            AdornmentInfo.LabelTemplate = ResourceFac.labelTemplate15;
            AdornmentInfo.SymbolTemplate = ResourceFac.labelTemplate16;

            AdornmentInfo1 = new ChartAdornmentInfo();
            AdornmentInfo1.ShowLabel = true;
            AdornmentInfo1.ShowMarker = true;
            AdornmentInfo1.LabelTemplate = ResourceFac.labelTemplate15;
            AdornmentInfo1.SymbolTemplate = ResourceFac.labelTemplate16;
        }
        public ResourceFactory ResourceFac { get; set; }
        public ChartAdornmentInfo AdornmentInfo { get; set; }
        public ChartAdornmentInfo AdornmentInfo1 { get; set; }

        public void Dispose()
        {
            if (this.ClimateData != null)
                this.ClimateData.Clear();
        }
    }

    public class StriplineDemoDataModel
    {
        public int ID
        {
            get;
            set;
        }

        public string Month
        {
            get;
            set;
        }

        public string Quarter
        {
            get;
            set;
        }

        public double Temperature
        {
            get;
            set;
        }

        public double RelativeHumidity
        {
            get;
            set;
        }

        public double Rainfall
        {
            get;
            set;
        }
    }
}
