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
    public sealed partial class StackingGroup : SampleLayout
    {
        public StackingGroup()
        {
            this.InitializeComponent();
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                scChartAdornmentInfo1.ShowMarker = false;
                scChartAdornmentInfo1.ShowLabel = true;
                scChartAdornmentInfo1.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
                scChartAdornmentInfo1.HorizontalAlignment = HorizontalAlignment.Center;
                scChartAdornmentInfo1.VerticalAlignment = VerticalAlignment.Center;
                scChartAdornmentInfo1.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                scChartAdornmentInfo2.ShowMarker = false;
                scChartAdornmentInfo2.ShowLabel = true;
                scChartAdornmentInfo2.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
                scChartAdornmentInfo2.HorizontalAlignment = HorizontalAlignment.Center;
                scChartAdornmentInfo2.VerticalAlignment = VerticalAlignment.Center;
                scChartAdornmentInfo2.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                scChartAdornmentInfo3.ShowMarker = false;
                scChartAdornmentInfo3.ShowLabel = true;
                scChartAdornmentInfo3.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
                scChartAdornmentInfo3.HorizontalAlignment = HorizontalAlignment.Center;
                scChartAdornmentInfo3.VerticalAlignment = VerticalAlignment.Center;
                scChartAdornmentInfo3.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;

                scChartAdornmentInfo4.ShowMarker = false;
                scChartAdornmentInfo4.ShowLabel = true;
                scChartAdornmentInfo4.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
                scChartAdornmentInfo4.HorizontalAlignment = HorizontalAlignment.Center;
                scChartAdornmentInfo4.VerticalAlignment = VerticalAlignment.Center;
                scChartAdornmentInfo4.LabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;
            }
        }

        public override void Dispose()
        {
            if (this.DataContext is StackingGroupViewModel)
                (this.DataContext as StackingGroupViewModel).Dispose();

            if (this.StackingColumnChart != null)
            {
                foreach (var series in this.StackingColumnChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.StackingColumnChart = null;
            }

            this.Resources = null;

            base.Dispose();
        }
    }

    public class StackingGroupViewModel : IDisposable
    {
        public StackingGroupViewModel()
        {
            this.AnnualDetails = new ObservableCollection<StackingGroupProductDetails>();
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                AnnualDetails.Add(new StackingGroupProductDetails()
                {
                    Year = "2009",
                    Quarter1 = 34,
                    Quarter2 = 31,
                    Quarter3 = 29,
                    Quarter4 = 30
                });
            }
            AnnualDetails.Add(new StackingGroupProductDetails()
            {
                Year = "2010",
                Quarter1 = 24,
                Quarter2 = 28,
                Quarter3 = 32,
                Quarter4 = 33
            });
            AnnualDetails.Add(new StackingGroupProductDetails()
            {
                Year = "2011",
                Quarter1 = 20,
                Quarter2 = 25,
                Quarter3 = 25,
                Quarter4 = 26
            });
            AnnualDetails.Add(new StackingGroupProductDetails()
            {
                Year = "2012",
                Quarter1 = 19,
                Quarter2 = 21,
                Quarter3 = 23,
                Quarter4 = 26
            });
            AnnualDetails.Add(new StackingGroupProductDetails()
            {
                Year = "2013",
                Quarter1 = 19,
                Quarter2 = 15,
                Quarter3 = 17,
                Quarter4 = 21
            });

            ResourceFac = new ResourceFactory();
            AdronmentInfo1 = new ChartAdornmentInfo();
            AdronmentInfo2 = new ChartAdornmentInfo();
            AdronmentInfo3 = new ChartAdornmentInfo();
            AdronmentInfo4 = new ChartAdornmentInfo();

            AdronmentInfo1.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdronmentInfo1.ShowMarker = false;
            AdronmentInfo1.ShowLabel = true;
            AdronmentInfo1.HorizontalAlignment = HorizontalAlignment.Center;
            AdronmentInfo1.VerticalAlignment = VerticalAlignment.Center;

            AdronmentInfo2.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdronmentInfo2.ShowMarker = false;
            AdronmentInfo2.ShowLabel = true;
            AdronmentInfo2.HorizontalAlignment = HorizontalAlignment.Center;
            AdronmentInfo2.VerticalAlignment = VerticalAlignment.Center;

            AdronmentInfo3.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdronmentInfo3.ShowMarker = false;
            AdronmentInfo3.ShowLabel = true;
            AdronmentInfo3.HorizontalAlignment = HorizontalAlignment.Center;
            AdronmentInfo3.VerticalAlignment = VerticalAlignment.Center;

            AdronmentInfo4.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdronmentInfo4.ShowMarker = false;
            AdronmentInfo4.ShowLabel = true;
            AdronmentInfo4.HorizontalAlignment = HorizontalAlignment.Center;
            AdronmentInfo4.VerticalAlignment = VerticalAlignment.Center;
        }

        public ResourceFactory ResourceFac { get; set; }

        public ChartAdornmentInfo AdronmentInfo1 { get; set; }
        public ChartAdornmentInfo AdronmentInfo2 { get; set; }
        public ChartAdornmentInfo AdronmentInfo3 { get; set; }
        public ChartAdornmentInfo AdronmentInfo4 { get; set; }

        public ObservableCollection<StackingGroupProductDetails> AnnualDetails
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.AnnualDetails != null)
                this.AnnualDetails.Clear();
        }
    }

    public class StackingGroupProductDetails
    {
        public string Year
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        public double Quarter1
        {
            get;
            set;
        }

        public double Quarter2
        {
            get;
            set;
        }

        public double Quarter3
        {
            get;
            set;
        }

        public double Quarter4
        {
            get;
            set;
        }
    }
}
