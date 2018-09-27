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
    public sealed partial class TrackBallDemo : SampleLayout
    {
        public TrackBallDemo()
        {
            this.InitializeComponent();

            // Since the control is placed in a list view. The grid's manipulation has to be checked for good interactive behavior support.
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                this.grid.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            }
        }

        public override void Dispose()
        {
            if (this.trackBall != null)
            {
                foreach (var series in this.trackBall.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.trackBall = null;
            }

            base.Dispose();
        }

        private void trackBall_Loaded(object sender, global::Windows.UI.Xaml.RoutedEventArgs e)
        {
            DataTemplate adornmentTemplate = trackBall.Resources["adornmentTemplate"] as DataTemplate;
            ChartAdornmentInfo cai = new ChartAdornmentInfo();
            cai.ShowLabel = true;
            cai.Symbol = ChartSymbol.Ellipse;
            cai.LabelTemplate = adornmentTemplate;
            splineAreaSeries.AdornmentsInfo = cai;
        }
    }

    public class CommoditiesPrices : IDisposable
    {
        public void Dispose()
        {
            if (this.CommodityDetails != null)
                this.CommodityDetails.Clear();
        }

        public CommoditiesPrices()
        {
            CommodityDetails = new ObservableCollection<Commodities>();
            CommodityDetails.Add(new Commodities()
            {
                Commodity = "Gold",
                High = 2700,
                Low = 2650,
                ChangingPrice = 46,
                XValue = 1
            });
            CommodityDetails.Add(new Commodities()
            {
                Commodity = "Silver",
                High = 68,
                Low = 58,
                ChangingPrice = -10,
                XValue = 2
            });
            CommodityDetails.Add(new Commodities()
            {
                Commodity = "Crudeoil",
                High = 1500,
                Low = 1380,
                ChangingPrice = -34,
                XValue = 3
            });
            CommodityDetails.Add(new Commodities()
            {
                Commodity = "Naturalgas",
                High = 2200,
                Low = 2100,
                ChangingPrice = -45,
                XValue = 4
            });
            CommodityDetails.Add(new Commodities()
            {
                Commodity = "Aluminium",
                High = 120,
                Low = 112,
                ChangingPrice = 8,
                XValue = 5
            });
            CommodityDetails.Add(new Commodities()
            {
                Commodity = "Copper",
                High = 410,
                Low = 393,
                ChangingPrice = 23,
                XValue = 6
            });
            CommodityDetails.Add(new Commodities()
            {
                Commodity = "Nickel",
                High = 110,
                Low = 90,
                ChangingPrice = -15,
                XValue = 7
            });
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                CommodityDetails.Add(new Commodities()
                {
                    Commodity = "Lead",
                    High = 133,
                    Low = 123,
                    ChangingPrice = 16,
                    XValue = 8
                });

                CommodityDetails.Add(new Commodities()
                {
                    Commodity = "Cotton",
                    High = 2005,
                    Low = 1990,
                    ChangingPrice = 37,
                    XValue = 9
                });
                CommodityDetails.Add(new Commodities()
                {
                    Commodity = "Menthaoil",
                    High = 835,
                    Low = 797,
                    ChangingPrice = -22,
                    XValue = 10
                });
                CommodityDetails.Add(new Commodities()
                {
                    Commodity = "Zinc",
                    High = 123,
                    Low = 110,
                    ChangingPrice = 13,
                    XValue = 11
                });
            }

            ResourceFac = new ResourceFactory();
            AdInfo = new ChartAdornmentInfo();
            AdInfo1 = new ChartAdornmentInfo();

            AdInfo.ShowLabel = true;
            AdInfo.Symbol = ChartSymbol.Ellipse;
            AdInfo.LabelTemplate = ResourceFac.labelTemplate17;

            AdInfo1.ShowLabel = true;
            AdInfo1.LabelTemplate = ResourceFac.labelTemplate20;
        }
        public ResourceFactory ResourceFac { get; set; }

        public ChartAdornmentInfo AdInfo { get; set; }

        public ChartAdornmentInfo AdInfo1 { get; set; }

        public ObservableCollection<Commodities> CommodityDetails
        {
            get;
            set;
        }
    }

    public class Commodities
    {
        public string Commodity
        {
            get;
            set;
        }

        public double High
        {
            get;
            set;
        }

        public double Low
        {
            get;
            set;
        }

        public double ChangingPrice
        {
            get;
            set;
        }

        public double XValue
        {
            get;
            set;
        }

    }

    public class TrackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartPointInfo val = value as ChartPointInfo;
            return String.Format("$ {0}", val.ValueY);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
