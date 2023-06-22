#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
    public sealed partial class StackingBar1003D : SampleLayout
    {
        public StackingBar1003D()
        {
            this.InitializeComponent();

            StackingBar100Chart3D.Rotation = 24;
            sb100ChartAdornmentInfo3D1.SegmentLabelContent = LabelContent.LabelContentPath;
            sb100ChartAdornmentInfo3D1.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            sb100ChartAdornmentInfo3D1.LabelTemplate = grid.Resources["labelTemplate1"] as DataTemplate;
            sb100ChartAdornmentInfo3D1.HorizontalAlignment = HorizontalAlignment.Center;
            sb100ChartAdornmentInfo3D1.VerticalAlignment = VerticalAlignment.Center;
            sb100ChartAdornmentInfo3D1.ShowLabel = true;

            sb100ChartAdornmentInfo3D2.SegmentLabelContent = LabelContent.LabelContentPath;
            sb100ChartAdornmentInfo3D2.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            sb100ChartAdornmentInfo3D2.LabelTemplate = grid.Resources["labelTemplate1"] as DataTemplate;
            sb100ChartAdornmentInfo3D2.HorizontalAlignment = HorizontalAlignment.Center;
            sb100ChartAdornmentInfo3D2.VerticalAlignment = VerticalAlignment.Center;
            sb100ChartAdornmentInfo3D2.ShowLabel = true;

            sb100ChartAdornmentInfo3D3.SegmentLabelContent = LabelContent.LabelContentPath;
            sb100ChartAdornmentInfo3D3.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            sb100ChartAdornmentInfo3D3.LabelTemplate = grid.Resources["labelTemplate1"] as DataTemplate;
            sb100ChartAdornmentInfo3D3.HorizontalAlignment = HorizontalAlignment.Center;
            sb100ChartAdornmentInfo3D3.VerticalAlignment = VerticalAlignment.Center;
            sb100ChartAdornmentInfo3D3.ShowLabel = true;

            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                StackingBar100Chart3D.Margin = new Thickness(10);
            }
            else
            {
                StackingBar100Chart3D.Margin = new Thickness(70, 20, 75, 25);
            }
        }

        public override void Dispose()
        {
            if (this.grid.DataContext is CategoryDataViewModel)
                (this.grid.DataContext as CategoryDataViewModel).Dispose();

            if (this.StackingBar100Chart3D != null)
            {
                foreach (var series in this.StackingBar100Chart3D.Series)
                {
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                }
                this.StackingBar100Chart3D = null;
            }

            this.grid.Resources = null;

            base.Dispose();
        }
    }
}
