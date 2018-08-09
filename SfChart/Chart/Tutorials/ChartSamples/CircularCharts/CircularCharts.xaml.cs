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
    public sealed partial class CircularCharts : SampleLayout
    {
        public CircularCharts()
        {
            this.InitializeComponent();
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                Adornments_weed.AdornmentsPosition = AdornmentsPosition.Top;
                Adornments_weed.HorizontalAlignment = HorizontalAlignment.Center;
                Adornments_weed.VerticalAlignment = VerticalAlignment.Center;
                Adornments_weed.Symbol = ChartSymbol.Ellipse;
                Adornments_weed.SymbolHeight = 15;
                Adornments_weed.SymbolWidth = 15;

                chartAdornmentInfo1.AdornmentsPosition = AdornmentsPosition.Top;
                chartAdornmentInfo1.HorizontalAlignment = HorizontalAlignment.Center;
                chartAdornmentInfo1.VerticalAlignment = VerticalAlignment.Center;
                chartAdornmentInfo1.Symbol = ChartSymbol.Ellipse;
                chartAdornmentInfo1.SymbolHeight = 15;
                chartAdornmentInfo1.SymbolWidth = 15;

                chartAdornmentInfo2.AdornmentsPosition = AdornmentsPosition.Top;
                chartAdornmentInfo2.HorizontalAlignment = HorizontalAlignment.Center;
                chartAdornmentInfo2.VerticalAlignment = VerticalAlignment.Center;
                chartAdornmentInfo2.Symbol = ChartSymbol.Ellipse;
                chartAdornmentInfo2.SymbolHeight = 15;
                chartAdornmentInfo2.SymbolWidth = 15;

                radarChartAdornmentInfo1.AdornmentsPosition = AdornmentsPosition.Top;
                radarChartAdornmentInfo1.HorizontalAlignment = HorizontalAlignment.Center;
                radarChartAdornmentInfo1.VerticalAlignment = VerticalAlignment.Center;
                radarChartAdornmentInfo1.Symbol = ChartSymbol.Ellipse;
                radarChartAdornmentInfo1.SymbolHeight = 15;
                radarChartAdornmentInfo1.SymbolWidth = 15;

                radarChartAdornmentInfo2.AdornmentsPosition = AdornmentsPosition.Top;
                radarChartAdornmentInfo2.HorizontalAlignment = HorizontalAlignment.Center;
                radarChartAdornmentInfo2.VerticalAlignment = VerticalAlignment.Center;
                radarChartAdornmentInfo2.Symbol = ChartSymbol.Ellipse;
                radarChartAdornmentInfo2.SymbolHeight = 15;
                radarChartAdornmentInfo2.SymbolWidth = 15;

                radarChartAdornmentInfo3.AdornmentsPosition = AdornmentsPosition.Top;
                radarChartAdornmentInfo3.HorizontalAlignment = HorizontalAlignment.Center;
                radarChartAdornmentInfo3.VerticalAlignment = VerticalAlignment.Center;
                radarChartAdornmentInfo3.Symbol = ChartSymbol.Ellipse;
                radarChartAdornmentInfo3.SymbolHeight = 15;
                radarChartAdornmentInfo3.SymbolWidth = 15;
            }
        }

        private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (polarSeries == null) return;
            if (comboBox != null)
            {
                if (comboBox.SelectedIndex == 0)
                {
                    polarSeries.Visibility = Visibility.Visible;
                    radarSeries.Visibility = Visibility.Collapsed;
                }
                else if (comboBox.SelectedIndex == 1)
                {
                    radarSeries.Visibility = Visibility.Visible;
                    polarSeries.Visibility = Visibility.Collapsed;
                }
            }

        }

        public override void Dispose()
        {
            if (this.DataContext is PolarChartViewModel)
                (this.DataContext as PolarChartViewModel).Dispose();

            if (this.polarSeries != null)
            {
                foreach (var series in this.polarSeries.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.polarSeries = null;
            }

            if (this.radarSeries != null)
            {
                foreach (var series in this.radarSeries.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.radarSeries = null;
            }

            this.Maingrd.Resources = null;

            base.Dispose();
        }
    }

    public class PolarChartViewModel : IDisposable
    {
        public PolarChartViewModel()
        {
            this.PlantDetails = new ObservableCollection<PlantData>();
            this.PlantDetails.Add(new PlantData()
            {
                DirectionID = 0,
                Direction = "North",
                Weed = 63,
                Flower = 42,
                Tree = 80
            });
            this.PlantDetails.Add(new PlantData()
            {
                DirectionID = 1,
                Direction = "NorthEast",
                Weed = 70,
                Flower = 40,
                Tree = 85
            });
            this.PlantDetails.Add(new PlantData()
            {
                DirectionID = 2,
                Direction = "East",
                Weed = 45,
                Flower = 25,
                Tree = 78
            });
            this.PlantDetails.Add(new PlantData()
            {
                DirectionID = 3,
                Direction = "SouthEast",
                Weed = 70,
                Flower = 40,
                Tree = 90
            });
            this.PlantDetails.Add(new PlantData()
            {
                DirectionID = 4,
                Direction = "South",
                Weed = 47,
                Flower = 20,
                Tree = 78
            });
            this.PlantDetails.Add(new PlantData()
            {
                DirectionID = 5,
                Direction = "SouthWest",
                Weed = 65,
                Flower = 45,
                Tree = 83
            });
            this.PlantDetails.Add(new PlantData()
            {
                DirectionID = 6,
                Direction = "West",
                Weed = 58,
                Flower = 40,
                Tree = 79
            });
            this.PlantDetails.Add(new PlantData()
            {
                DirectionID = 7,
                Direction = "NorthWest",
                Weed = 73,
                Flower = 28,
                Tree = 88
            });
        }

        public ObservableCollection<PlantData> PlantDetails
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.PlantDetails != null)
                this.PlantDetails.Clear();
        }
    }

    public class PlantData
    {
        public int DirectionID
        {
            get;
            set;
        }

        public string Direction
        {
            get;
            set;
        }

        public double Weed
        {
            get;
            set;
        }

        public double Flower
        {
            get;
            set;
        }

        public double Tree
        {
            get;
            set;
        }
    }

    public class CircularLabelConverter : IValueConverter
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
