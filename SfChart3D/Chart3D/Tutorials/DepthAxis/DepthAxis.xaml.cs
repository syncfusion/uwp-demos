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
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfChart3D
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DepthAxis : SampleLayout
    {
        public DepthAxis()
        {
            this.InitializeComponent();
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                chart.Margin = new Thickness(16);
            }
        }

        public override void Dispose()
        {
            if (grid.DataContext is ZAxisViewModel) (grid.DataContext as ZAxisViewModel).Dispose();
            if (chart != null)
            {
                foreach (var series in chart.Series)
                {
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                }
                chart = null;
            }
            GC.Collect();
            this.grid.Resources = null;
            base.Dispose();
        }

        private void seriesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            ZAxisViewModel viewModel = new ZAxisViewModel();
            if(combo.SelectedIndex == 0)
            {
                chart.Series.Clear();
                ColumnSeries3D series = new ColumnSeries3D();
                series.ShowTooltip = true;
                series.Palette = ChartColorPalette.Metro;
                series.ItemsSource = viewModel.FruitDetails;
                series.XBindingPath = "FruitsName";
                series.YBindingPath = "Count";
                series.ZBindingPath = "Day";
                chart.Series.Add(series);
            }
            else if (combo.SelectedIndex == 1)
            {
                chart.Series.Clear();
                BarSeries3D series = new BarSeries3D();
                series.ShowTooltip = true;
                series.Palette = ChartColorPalette.Metro;
                series.ItemsSource = viewModel.FruitDetails;
                series.XBindingPath = "FruitsName";
                series.YBindingPath = "Count";
                series.ZBindingPath = "Day";
                chart.Series.Add(series);
            }
            else if (combo.SelectedIndex == 2)
            {
                chart.Series.Clear();
                ScatterSeries3D series = new ScatterSeries3D();
                series.ShowTooltip = true;
                series.Palette = ChartColorPalette.Metro;
                series.ItemsSource = viewModel.FruitDetails;
                series.XBindingPath = "FruitsName";
                series.YBindingPath = "Count";
                series.ZBindingPath = "Day";
                chart.Series.Add(series);
            }
        }
    }

    public class ZAxisModel
    {
        public string FruitsName
        {
            get;
            set;
        }
        public string Day
        {
            get;
            set;
        }

        public double Count
        {
            get;
            set;
        }

    }

    public class ZAxisViewModel
    {
        public ObservableCollection<ZAxisModel> FruitDetails
        {
            get;
            set;
        }

        public ZAxisViewModel()
        {
            FruitDetails = new ObservableCollection<ZAxisModel>();
            FruitDetails.Add(new ZAxisModel() { FruitsName = "Apple", Day = "Sun", Count = 50 });
            FruitDetails.Add(new ZAxisModel() { FruitsName = "Orange", Day = "Mon", Count = 30 });
            FruitDetails.Add(new ZAxisModel() { FruitsName = "Mango", Day = "Tue", Count = 60 });
            FruitDetails.Add(new ZAxisModel() { FruitsName = "Banana", Day = "Wed", Count = 80 });
            FruitDetails.Add(new ZAxisModel() { FruitsName = "Grape", Day = "Thur", Count = 60 });
            FruitDetails.Add(new ZAxisModel() { FruitsName = "Jackfruit", Day = "Fri", Count = 30 });
            FruitDetails.Add(new ZAxisModel() { FruitsName = "Guava", Day = "Sat", Count = 75 });
        }

        internal void Dispose()
        {
            if (this.FruitDetails != null) this.FruitDetails.Clear();
        }
    }
}
