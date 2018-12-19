#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Chartz : UserControl, IDisposable
    {
        public Chartz()
        {
            this.InitializeComponent();
            this.Unloaded += Chartz_Unloaded;
            this.DataContext = new SalesByCountry();

            pieChartAdornmentInfo.ShowConnectorLine = true;
            pieChartAdornmentInfo.ShowLabel = true;
            pieChartAdornmentInfo.SegmentLabelContent = LabelContent.LabelContentPath;
            pieChartAdornmentInfo.LabelTemplate = dashboardChart1.Resources["labelTemplate"] as DataTemplate;
            pieChartAdornmentInfo.ConnectorLineStyle = dashboardChart1.Resources["connectorLineStyle"] as Style;
        }

        private void Chartz_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            if (this.sfchart != null)
                this.sfchart = null;
            this.dashboardChart1.Resources = null;
        }
    }
}
