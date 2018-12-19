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

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public sealed partial class MultipleLegendsDemo : SampleLayout
    {
        public MultipleLegendsDemo()
        {
            this.InitializeComponent();

            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                (Chart1.Series[3] as CartesianSeries).XAxis = (Chart1.Series[2] as CartesianSeries).XAxis;
            }
        }

        private void OnDockPositionSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;
            if (Chart1 != null)
            {
                if (select.SelectedIndex == 0)
                {
                    (Chart1.Legend as ChartLegendCollection)[0].DockPosition = ChartDock.Bottom;

                }
                else if (select.SelectedIndex == 1)
                {
                    (Chart1.Legend as ChartLegendCollection)[0].DockPosition = ChartDock.Floating;

                }
                else if (select.SelectedIndex == 2)
                {
                    (Chart1.Legend as ChartLegendCollection)[0].DockPosition = ChartDock.Left;

                }
                else if (select.SelectedIndex == 3)
                {
                    (Chart1.Legend as ChartLegendCollection)[0].DockPosition = ChartDock.Right;

                }
                else if (select.SelectedIndex == 4)
                {
                    (Chart1.Legend as ChartLegendCollection)[0].DockPosition = ChartDock.Top;

                }
            }

        }

        private void OnLegendPositionSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;
            if (Chart1 != null)
            {
                if (select.SelectedIndex == 0)
                {
                    (Chart1.Legend as ChartLegendCollection)[0].LegendPosition = LegendPosition.Inside;

                }
                else if (select.SelectedIndex == 1)
                {
                    (Chart1.Legend as ChartLegendCollection)[0].LegendPosition = LegendPosition.Outside;

                }

            }
        }

        private void OnDockPosition1SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;
            if (Chart1 != null)
            {
                if (select.SelectedIndex == 0)
                {

                    (Chart1.Legend as ChartLegendCollection)[1].DockPosition = ChartDock.Bottom;
                }
                else if (select.SelectedIndex == 1)
                {

                    (Chart1.Legend as ChartLegendCollection)[1].DockPosition = ChartDock.Floating;
                }
                else if (select.SelectedIndex == 2)
                {
                    (Chart1.Legend as ChartLegendCollection)[1].DockPosition = ChartDock.Left;
                }
                else if (select.SelectedIndex == 3)
                {
                    (Chart1.Legend as ChartLegendCollection)[1].DockPosition = ChartDock.Right;
                }
                else if (select.SelectedIndex == 4)
                {
                    (Chart1.Legend as ChartLegendCollection)[1].DockPosition = ChartDock.Top;
                }
            }
        }

        private void OnLegendPosition1SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;
            if (Chart1 != null)
            {
                if (select.SelectedIndex == 0)
                {
                    (Chart1.Legend as ChartLegendCollection)[1].LegendPosition = LegendPosition.Inside;

                }
                else if (select.SelectedIndex == 1)
                {
                    (Chart1.Legend as ChartLegendCollection)[1].LegendPosition = LegendPosition.Outside;
                }

            }
        }

        public override void Dispose()
        {
            if (this.Chart1 != null)
            {
                foreach (var series in this.Chart1.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.Chart1 = null;
            }

            if (this.grid.DataContext != null)
                this.grid.DataContext = null;
            this.grid.Resources = null;

            base.Dispose();
        }
    }
}
