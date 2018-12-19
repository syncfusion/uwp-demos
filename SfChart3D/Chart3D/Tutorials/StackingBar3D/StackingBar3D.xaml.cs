#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
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
    public sealed partial class StackingBar3D : SampleLayout
    {
        public StackingBar3D()
        {
            this.InitializeComponent();

            sbsChartAdornmentInfo3D1.SegmentLabelContent = LabelContent.LabelContentPath;
            sbsChartAdornmentInfo3D1.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            sbsChartAdornmentInfo3D1.ShowConnectorLine = false;
            sbsChartAdornmentInfo3D1.HorizontalAlignment = HorizontalAlignment.Center;
            sbsChartAdornmentInfo3D1.VerticalAlignment = VerticalAlignment.Center;
            sbsChartAdornmentInfo3D1.ShowLabel = true;
            sbsChartAdornmentInfo3D1.ShowMarker = true;
            sbsChartAdornmentInfo3D1.LabelTemplate = grid.Resources["labelTemplate1"] as DataTemplate;

            sbsChartAdornmentInfo3D2.SegmentLabelContent = LabelContent.LabelContentPath;
            sbsChartAdornmentInfo3D2.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            sbsChartAdornmentInfo3D2.ShowConnectorLine = false;
            sbsChartAdornmentInfo3D2.HorizontalAlignment = HorizontalAlignment.Center;
            sbsChartAdornmentInfo3D2.VerticalAlignment = VerticalAlignment.Center;
            sbsChartAdornmentInfo3D2.ShowLabel = true;
            sbsChartAdornmentInfo3D2.ShowMarker = true;
            sbsChartAdornmentInfo3D2.LabelTemplate = grid.Resources["labelTemplate1"] as DataTemplate;
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
