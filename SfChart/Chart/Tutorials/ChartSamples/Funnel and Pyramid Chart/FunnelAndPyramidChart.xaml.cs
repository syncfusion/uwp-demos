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
using Windows.UI;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Globalization;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FunnelAndPyramidChart : SampleLayout
    {
        FunnelChartViewModel viewModel;
        public FunnelAndPyramidChart()
        {
            Resources.MergedDictionaries.Add(SfChart.Resources.ColorModelResource.Resource);
            viewModel = new FunnelChartViewModel();
            this.InitializeComponent();
        }

        private void OnChartTypeSelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            Accumulation_charts.Legend = null;

            if (comboBox.SelectedIndex == 0 && viewModel != null)
            {
                FunnelSeries series1 = new FunnelSeries();
                series1.Palette = ChartColorPalette.Custom;
                series1.ColorModel = Resources["customBrush"] as ChartColorModel;

                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();

                ChartLegend1.Visibility = Visibility.Collapsed;

                adornmentInfo1.ShowLabel = true;

                series1.XBindingPath = "Category";
                series1.YBindingPath = "Percentage";
                series1.ItemsSource = viewModel.list;
                series1.AdornmentsInfo = adornmentInfo1;
                series1.Margin = new Thickness(20, 0, 20, 20);

                Accumulation_charts.Header = "Top car company's turnover";
                Accumulation_charts.Margin = new Thickness(20, 20, 20, 20);
                Accumulation_charts.Series.Clear();
                Accumulation_charts.Series.Add(series1);
            }
            if (comboBox.SelectedIndex == 1 && viewModel != null)
            {
                PyramidSeries series1 = new PyramidSeries();
                series1.Palette = ChartColorPalette.Custom;
                series1.ColorModel = Resources["customBrush"] as ChartColorModel;

                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();

                ChartLegend1.Visibility = Visibility.Collapsed;

                adornmentInfo1.ShowLabel = true;

                series1.XBindingPath = "Category";
                series1.YBindingPath = "Percentage";
                series1.ItemsSource = viewModel.list;
                series1.AdornmentsInfo = adornmentInfo1;
                series1.Margin = new Thickness(20, 0, 20, 20);

                Accumulation_charts.Header = "Top car company's turnover";
                Accumulation_charts.Margin = new Thickness(20, 20, 20, 20);
                Accumulation_charts.Series.Clear();
                Accumulation_charts.Series.Add(series1);
            }
        }

        public override void Dispose()
        {
            if (this.DataContext is PieChartViewModel)
                (this.DataContext as PieChartViewModel).Dispose();

            if (this.Accumulation_charts != null)
            {
                foreach (var series in this.Accumulation_charts.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.Accumulation_charts = null;
            }

            this.Resources = null;

            base.Dispose();
        }
    }

    public class FunnelChartViewModel
    {
        public FunnelChartViewModel()
        {
            this.list = new ObservableCollection<FunnelChartModel>();
            DateTime yr = new DateTime(2010, 5, 1);

            list.Add(new FunnelChartModel() { Category = "Iron", Percentage = 36 });
            list.Add(new FunnelChartModel() { Category = "Zinc", Percentage = 32 });
            list.Add(new FunnelChartModel() { Category = "Copper", Percentage = 34 });
            list.Add(new FunnelChartModel() { Category = "Aluminium", Percentage = 41 });
            list.Add(new FunnelChartModel() { Category = "Gold", Percentage = 42 });
            list.Add(new FunnelChartModel() { Category = "Silver", Percentage = 42 });
            list.Add(new FunnelChartModel() { Category = "Diamond", Percentage = 43 });
        }

        public ObservableCollection<FunnelChartModel> list
        {
            get;
            set;
        }
    }

    public class FunnelChartModel
    {
        public String Category { get; set; }
        public double Percentage { get; set; }
    }
}
