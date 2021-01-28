#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public sealed partial class Bar3D : SampleLayout
    {
        public Bar3D()
        {
            this.InitializeComponent();

            BarChart3D.Rotation = 25;
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                BarChart3D.Margin = new Thickness(10);
                BarChart3D.PerspectiveAngle = 120.39325842696624;
            }
            else
            {
                BarChart3D.Margin = new Thickness(70, 20, 75, 25);
            }
        }

        public override void Dispose()
        {
            if (this.BarChart3D != null)
            {
                foreach (var series in this.BarChart3D.Series)
                {
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                }
                this.BarChart3D = null;
            }

            if (this.grid.DataContext is CategoryDataViewModel)
                (this.grid.DataContext as CategoryDataViewModel).Dispose();

            this.grid.Resources = null;

            base.Dispose();
        }
    }
}
