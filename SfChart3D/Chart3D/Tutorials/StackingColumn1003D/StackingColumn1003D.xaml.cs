using Common;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
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

namespace Syncfusion.SampleBrowser.UWP.SfChart3D
{
    public sealed partial class StackingColumn1003D : SampleLayout
    {
        public StackingColumn1003D()
        {
            this.InitializeComponent();
            scChartAdornmentInfo3D1.SegmentLabelContent = LabelContent.LabelContentPath;
            scChartAdornmentInfo3D1.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            scChartAdornmentInfo3D1.LabelTemplate = grid.Resources["labelTemplate1"] as DataTemplate;
            scChartAdornmentInfo3D1.ShowLabel = true;
            scChartAdornmentInfo3D1.ShowMarker = true;
            scChartAdornmentInfo3D1.HorizontalAlignment = HorizontalAlignment.Center;
            scChartAdornmentInfo3D1.VerticalAlignment = VerticalAlignment.Center;

            scChartAdornmentInfo3D2.SegmentLabelContent = LabelContent.LabelContentPath;
            scChartAdornmentInfo3D2.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            scChartAdornmentInfo3D2.LabelTemplate = grid.Resources["labelTemplate1"] as DataTemplate;
            scChartAdornmentInfo3D2.ShowLabel = true;
            scChartAdornmentInfo3D2.ShowMarker = true;
            scChartAdornmentInfo3D2.HorizontalAlignment = HorizontalAlignment.Center;
            scChartAdornmentInfo3D2.VerticalAlignment = VerticalAlignment.Center;

            scChartAdornmentInfo3D3.SegmentLabelContent = LabelContent.LabelContentPath;
            scChartAdornmentInfo3D3.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            scChartAdornmentInfo3D3.LabelTemplate = grid.Resources["labelTemplate1"] as DataTemplate;
            scChartAdornmentInfo3D3.ShowLabel = true;
            scChartAdornmentInfo3D3.ShowMarker = true;
            scChartAdornmentInfo3D3.HorizontalAlignment = HorizontalAlignment.Center;
            scChartAdornmentInfo3D3.VerticalAlignment = VerticalAlignment.Center;
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                columnChart.Margin = new Thickness(10);
            }
            else
            {
                columnChart.Margin = new Thickness(70, 20, 75, 25);
            }
        }

        public override void Dispose()
        {
            if (this.grid.DataContext is CategoryDataViewModel)
                (this.grid.DataContext as CategoryDataViewModel).Dispose();

            if (this.columnChart != null)
            {
                foreach (var series in this.columnChart.Series)
                {
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                }
                this.columnChart = null;
            }

            this.grid.Resources = null;

            base.Dispose();
        }
    }
}
