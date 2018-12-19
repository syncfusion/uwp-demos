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

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaletteOption : Page
    {
        public PaletteOption()
        {
            this.InitializeComponent();
        }

        private void selecter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Frame.Navigate(typeof(Palettes),sender);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Palettes view = e.Content as Palettes;
            if (selecter.SelectedItem != null)
            {
                Syncfusion.UI.Xaml.Charts.SfChart pie_chart = view.FindName("pieChart") as Syncfusion.UI.Xaml.Charts.SfChart;
                pie_chart.Series[0].Palette = (ChartColorPalette)selecter.SelectedItem;
                Syncfusion.UI.Xaml.Charts.SfChart bar_chart = view.FindName("barChart") as Syncfusion.UI.Xaml.Charts.SfChart;
                bar_chart.Palette = (ChartColorPalette)selecter.SelectedItem;
                base.OnNavigatedFrom(e);
            }
        }
    }

   
}
