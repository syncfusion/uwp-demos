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
    public sealed partial class SelectionBehavior : SampleLayout
    {
        public SelectionBehavior()
        {
            this.InitializeComponent();
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                selectionMode.Loaded += selectionMode_Loaded;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton select = sender as RadioButton;
            if ((bool)select.IsChecked && chart != null && (chart.Behaviors[0] as ChartSelectionBehavior) != null)
            {
                (chart.Behaviors[0] as ChartSelectionBehavior).EnableSeriesSelection = false;
                (chart.Behaviors[0] as ChartSelectionBehavior).EnableSegmentSelection = true;
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            RadioButton select = sender as RadioButton;
            if ((bool)select.IsChecked)
            {
                (chart.Behaviors[0] as ChartSelectionBehavior).EnableSegmentSelection = false;
                (chart.Behaviors[0] as ChartSelectionBehavior).EnableSeriesSelection = true;
            }
        }

        private void chart_SelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            if (e.IsDataPointSelection && e.SelectedSeries != null)
            {
                if (e.SelectedSeries.Label == "2013")
                {
                    (chart.Series[1] as ColumnSeries).SelectedIndex = -1;
                }
                else
                    (chart.Series[0] as ColumnSeries).SelectedIndex = -1;
            }
        }

        private void selectionMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var select = sender as ComboBox;
            if (select.SelectedIndex == 0 && chart != null && (chart.Behaviors[0] as ChartSelectionBehavior) != null)
            {
                (chart.Behaviors[0] as ChartSelectionBehavior).SelectionMode = Syncfusion.UI.Xaml.Charts.SelectionMode.MouseClick;
                selectionStyle.IsEnabled = true;
                SelectionStyles.Opacity = 1;
            }
            if (select.SelectedIndex == 1)
            {
                (chart.Behaviors[0] as ChartSelectionBehavior).SelectionMode = Syncfusion.UI.Xaml.Charts.SelectionMode.MouseMove;
                selectionStyle.IsEnabled = false;
                selectionStyle.SelectedIndex = 0;
                SelectionStyles.Opacity = 0.4;
            }
        }

        private void selectionMode_Loaded(object sender, RoutedEventArgs e)
        {
            selectionMode.SelectedIndex = 0;
            selectionStyle.SelectedIndex = 0;
        }

        public override void Dispose()
        {
            if ((this.DataContext as SeriesSelectionChartViewModel) != null)
                (this.DataContext as SeriesSelectionChartViewModel).Dispose();

            if (this.chart != null)
            {
                foreach (var series in this.chart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.chart = null;
            }

            this.Resources = null;

            base.Dispose();
        }

        private void selectionStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var select = sender as ComboBox;
            if (select.SelectedIndex == 0 && chart != null && (chart.Behaviors[0] as ChartSelectionBehavior) != null)
            {
                (chart.Behaviors[0] as ChartSelectionBehavior).SelectionStyle = SelectionStyle.Single;
            }
            if (select.SelectedIndex == 1)
            {
                (chart.Behaviors[0] as ChartSelectionBehavior).SelectionStyle = SelectionStyle.Multiple;
            }
        }

        private void chart_Loaded(object sender, global::Windows.UI.Xaml.RoutedEventArgs e)
        {
            DataTemplate labelTemplate1 = MainGrid.Resources["labeltemplate"] as DataTemplate;
            DataTemplate labelTemplate2 = MainGrid.Resources["labeltemplate"] as DataTemplate;

            ChartAdornmentInfo chartAdornmentInfo1 = new ChartAdornmentInfo();
            ChartAdornmentInfo chartAdornmentInfo2 = new ChartAdornmentInfo();

            chartAdornmentInfo1.ShowLabel = true;
            chartAdornmentInfo1.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            chartAdornmentInfo1.LabelTemplate = labelTemplate1;

            chartAdornmentInfo2.ShowLabel = true;
            chartAdornmentInfo2.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            chartAdornmentInfo2.LabelTemplate = labelTemplate2;

            column.AdornmentsInfo = chartAdornmentInfo1;
            column1.AdornmentsInfo = chartAdornmentInfo2;
        }
    }

    public class Sales
    {
        public double Year2013
        {
            get;
            set;
        }
        public double Year2014
        {
            get;
            set;
        }
        public string Category
        {
            get;
            set;
        }
    }

    public class SeriesSelectionChartViewModel : IDisposable
    {
        public ObservableCollection<Sales> SalesCollection
        {
            get;
            set;
        }
        public SeriesSelectionChartViewModel()
        {
            SalesCollection = new ObservableCollection<Sales>();
            SalesCollection.Add(new Sales() { Category = "Samsung", Year2013 = 32.5, Year2014 = 28.0 });
            SalesCollection.Add(new Sales() { Category = "Apple", Year2013 = 16.6, Year2014 = 16.4 });
            SalesCollection.Add(new Sales() { Category = "Sony", Year2013 = 4.1, Year2014 = 3.9 });
            SalesCollection.Add(new Sales() { Category = "LG", Year2013 = 4.3, Year2014 = 6.0 });
            SalesCollection.Add(new Sales() { Category = "ZTE", Year2013 = 3.2, Year2014 = 3.1 });

            AdornmentInfo = new ChartAdornmentInfo();
            AdornmentInfo.Foreground = new SolidColorBrush(Colors.White);
            AdornmentInfo.FontSize = 12;
            AdornmentInfo.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdornmentInfo.ShowLabel = true;
        }
        public ChartAdornmentInfo AdornmentInfo { get; set; }

        public Array SelectionModes
        {
            get
            {
                return Enum.GetValues(typeof(Syncfusion.UI.Xaml.Charts.SelectionMode));
            }
        }

        public Array SelectionStyles
        {
            get
            {
                return Enum.GetValues(typeof(SelectionStyle));
            }
        }

        public void Dispose()
        {
            if (this.SalesCollection != null)
                this.SalesCollection.Clear();
        }
    }
}
