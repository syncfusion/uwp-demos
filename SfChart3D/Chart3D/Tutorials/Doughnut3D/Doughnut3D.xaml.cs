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
    public sealed partial class Doughnut3D : SampleLayout
    {
        public Doughnut3D()
        {
            this.InitializeComponent();

            DoughnutChart3D.Rotation = 44;
            dsChartAdornmentInfo3D.SegmentLabelContent = LabelContent.Percentage;
            dsChartAdornmentInfo3D.VerticalAlignment = VerticalAlignment.Center;
            dsChartAdornmentInfo3D.HorizontalAlignment = HorizontalAlignment.Center;
            dsChartAdornmentInfo3D.ShowLabel = true;

            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                DoughnutChart3D.Margin = new Thickness(-12, -10, -5, -10);
                DoughnutChart3D.Tilt = -5;
                DoughnutChart3D.Rotation = 28;
            }
            else
            {
                DoughnutChart3D.Margin = new Thickness(70, 20, 75, 25);
            }
        }

        public override void Dispose()
        {
            if (this.grid.DataContext is ViewModel)
                (this.grid.DataContext as ViewModel).Dispose();

            if (this.DoughnutChart3D != null)
            {
                foreach (var series in this.DoughnutChart3D.Series)
                {
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                }
                this.DoughnutChart3D = null;
            }

            this.grid.Resources = null;

            base.Dispose();
        }
    }
}
