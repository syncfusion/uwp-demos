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
    public sealed partial class CrossHairDemo : SampleLayout
    {
        public CrossHairDemo()
        {
            this.InitializeComponent(); if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                chChartAdornmentInfo.ShowLabel = true;
                chChartAdornmentInfo.SegmentLabelContent = LabelContent.LabelContentPath;
                chChartAdornmentInfo.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
                chChartAdornmentInfo.LabelTemplate = MainGrid.Resources["labeltemplate"] as DataTemplate;
            }

            // Since the control is placed in a list view. The grid's manipulation has to be checked for good interactive behavior support.
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                this.MainGrid.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            }
        }

        public override void Dispose()
        {
            if ((this.DataContext as CurrencyData) != null)
                (this.DataContext as CurrencyData).Dispose();

            if (this.crossHair != null)
            {
                foreach (var series in this.crossHair.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.crossHair = null;
            }

            this.MainGrid.Resources = null;

            base.Dispose();
        }

        private void crossHair_Loaded(object sender, global::Windows.UI.Xaml.RoutedEventArgs e)
        {
            DataTemplate labelTemplate1 = MainGrid.Resources["labeltemplate"] as DataTemplate;
            ChartAdornmentInfo chartAdornmentInfo1 = new ChartAdornmentInfo();

            chartAdornmentInfo1.ShowLabel = true;
            chartAdornmentInfo1.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            chartAdornmentInfo1.SegmentLabelContent = LabelContent.YValue;
            chartAdornmentInfo1.LabelTemplate = labelTemplate1;

            columnSeries.AdornmentsInfo = chartAdornmentInfo1;
        }
    }

    public class CurrencyData : IDisposable
    {
        public CurrencyData()
        {
            CurrencyDetails = new ObservableCollection<CurrencyDetail>();
            CurrencyDetails.Add(new CurrencyDetail() { CurrencyName = "Rupee", CurrencyValue = 58.76 });
            CurrencyDetails.Add(new CurrencyDetail() { CurrencyName = "Yen", CurrencyValue = 100.94 });
            CurrencyDetails.Add(new CurrencyDetail() { CurrencyName = "Naira", CurrencyValue = 163.27 });
            CurrencyDetails.Add(new CurrencyDetail() { CurrencyName = "Rand", CurrencyValue = 10.45 });
            CurrencyDetails.Add(new CurrencyDetail() { CurrencyName = "Pound", CurrencyValue = 7.12 });
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                CurrencyDetails.Add(new CurrencyDetail() { CurrencyName = "Ruble", CurrencyValue = 34.51 });
                CurrencyDetails.Add(new CurrencyDetail() { CurrencyName = "Peso", CurrencyValue = 12.91 });
                CurrencyDetails.Add(new CurrencyDetail() { CurrencyName = "Yuan", CurrencyValue = 6.24 });
                CurrencyDetails.Add(new CurrencyDetail() { CurrencyName = "Shilling", CurrencyValue = 87.80 });
                CurrencyDetails.Add(new CurrencyDetail() { CurrencyName = "Ringgit", CurrencyValue = 5.21 });
            }

            SelectedIndexes = new ObservableCollection<int>();
            for (int index = -1; index <= 9; index++)
            {
                SelectedIndexes.Add(index);
            }

            AdornmentInfo = new ChartAdornmentInfo();
            AdornmentInfo.ShowLabel = true;
            AdornmentInfo.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdornmentInfo.SegmentLabelContent = LabelContent.YValue;
            AdornmentInfo.FontSize = 12;
            AdornmentInfo.Foreground = new SolidColorBrush(Colors.White);
        }
        public ChartAdornmentInfo AdornmentInfo { get; set; }

        public ObservableCollection<CurrencyDetail> CurrencyDetails
        {
            get;
            set;
        }

        public ObservableCollection<int> SelectedIndexes
        {
            get;
            set;
        }

        public Array SelectionModes
        {
            get
            {
                return Enum.GetValues(typeof(Syncfusion.UI.Xaml.Charts.SelectionMode));
            }
        }

        public void Dispose()
        {
            if (this.CurrencyDetails != null)
                this.CurrencyDetails.Clear();
        }
    }

    public class CurrencyDetail
    {
        public string CurrencyName
        {
            get;
            set;
        }

        public double CurrencyValue
        {
            get;
            set;
        }
    }
}
