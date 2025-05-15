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
    public sealed partial class LogarithmicAxisDemo : SampleLayout
    {
        public LogarithmicAxisDemo()
        {
            Resources.MergedDictionaries.Add(SfChart.Resources.ColorModelResource.Resource);
            this.InitializeComponent();

            SeriesAdornmentsInfo.ShowMarker = true;
            SeriesAdornmentsInfo.Symbol = ChartSymbol.Ellipse;
            SeriesAdornmentsInfo.SymbolInterior = new SolidColorBrush(Colors.White);
            SeriesAdornmentsInfo.SymbolStroke = Resources["SeriesInterior1"] as SolidColorBrush;
        }

        public override void Dispose()
        {
            if ((this.DataContext as LogarithmicViewModel) != null)
                (this.DataContext as LogarithmicViewModel).Dispose();

            if (this.lineChart != null)
            {
                foreach (var series in this.lineChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.lineChart = null;
            }

            if (this.grid.DataContext != null)
                this.grid.DataContext = null;
            this.grid.Resources = null;

            base.Dispose();
        }
    }
}
