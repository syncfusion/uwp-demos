#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.UI.Xaml.SunburstChart;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfSunburstChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Zoom : SampleLayout
    {
        public Zoom()
        {
            this.InitializeComponent();
        }

        public override void Dispose()
        {
            if (this.Sunburst.DataContext is ViewModel) (this.Sunburst.DataContext as ViewModel).Dispose();
            if (this.Sunburst.ItemsSource != null) this.Sunburst.ClearValue(UI.Xaml.SunburstChart.SfSunburstChart.ItemsSourceProperty);
            this.Sunburst.Behaviors.Clear();
            this.Sunburst.Levels.Clear();
            this.Sunburst = null;
            this.Grid.Children.Clear();
            base.Dispose();
        }
    }
}
