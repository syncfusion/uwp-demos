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
    public sealed partial class RangeNavigator_Chart : UserControl, IDisposable
    {
        public RangeNavigator_Chart()
        {
            this.InitializeComponent();
            this.Unloaded += RangeNavigator_Chart_Unloaded;
            ViewModelz model = new ViewModelz();
            model.Selectedindex = 0;
            this.DataContext = model;
        }

        private void RangeNavigator_Chart_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            if (chart_navigator != null)
                chart_navigator = null;
            this.WindowRangeNavigator.Resources = null;
            this.WindowRangeNavigator.ChildrenTransitions = null;
        }
    }   
}
