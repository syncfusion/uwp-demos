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
    public sealed partial class SeriesSelection : SampleLayout
    {
        public SeriesSelection()
        {
            this.InitializeComponent();
        }

        public override void Dispose()
        {
            if (this.DataContext is SeriesSelectionChartViewModel)
                (this.DataContext as SeriesSelectionChartViewModel).Dispose();

            if (chart != null)
            {
                foreach (var series in this.chart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.chart = null;
            }

            this.Resources = null;

            base.Dispose();
        }
    }
}
