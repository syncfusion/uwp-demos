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
using System.ComponentModel;
using System.IO;
using System.Linq;
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

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Gauge : UserControl, IDisposable
    {
        public Gauge()
        {
            this.InitializeComponent();
            this.Unloaded += Gauge_Unloaded;
        }

        private void Gauge_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            if (SalesGauge != null)
                SalesGauge = null;

            if (SalesGauge2 != null)
                SalesGauge2 = null;

            if (SalesGauge3 != null)
                SalesGauge3 = null;

            if (SalesGauge4 != null)
                SalesGauge4 = null;

            this.grid.Resources = null;
            if(this.grid.DataContext != null)
                this.grid.DataContext = null;
            this.grid.ChildrenTransitions = null;
        }
    }
}
