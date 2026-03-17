#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
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
using Windows.UI.Text;
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
    public sealed partial class CustomTemplate : SampleLayout
    {
        private ScatterChartViewModel viewModelScatter;
        private SplineChartViewModel viewModelSpline;
        private BarChartViewModel viewModelBar;
        public CustomTemplate()
        {
            Resources.MergedDictionaries.Add(SfChart.Resources.ColorModelResource.Resource);
            this.InitializeComponent();
            viewModelScatter = new ScatterChartViewModel();
            viewModelSpline = new SplineChartViewModel();
            viewModelBar = new BarChartViewModel();

            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                Basic_Chart5.Series[0].ItemsSource = viewModelBar;
            }

            temp1 = MainGrid3.Resources["DataTemplate1"] as DataTemplate;
            temp2 = MainGrid3.Resources["DataTemplate2"] as DataTemplate;
            temp3 = MainGrid3.Resources["DataTemplate3"] as DataTemplate;
            temp4 = MainGrid3.Resources["DataTemplate4"] as DataTemplate;
            temp5 = MainGrid3.Resources["DataTemplate5"] as DataTemplate;
        }

        public DataTemplate temp1 { get; set; }
        public DataTemplate temp2 { get; set; }
        public DataTemplate temp3 { get; set; }
        public DataTemplate temp4 { get; set; }
        public DataTemplate temp5 { get; set; }

        public override void Dispose()
        {
            if (this.Basic_Chart5 != null)
            {
                foreach (var series in this.Basic_Chart5.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.Basic_Chart5 = null;
            }

            this.MainGrid3.Resources = null;

            base.Dispose();
        }

        private void OnChartTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (Basic_Chart5 == null) return;

            if (comboBox.SelectedIndex == 0 && viewModelBar != null)
            {
                Basic_Chart5.SetValue(Grid.RowSpanProperty, 2);
                Basic_Chart5.Axes.Clear();
                Basic_Chart5.Height = 515;
                Basic_Chart5.Header = "Car Speed Comparison";
                Basic_Chart5.AreaBorderThickness = new Thickness(0, 0, 0, 0);
                BarSeries series1 = new BarSeries();
                DataTemplate template1 = MainGrid3.Resources["newBarTemplate"] as DataTemplate;

                CategoryAxis axis1 = new CategoryAxis()
                {
                    ShowGridLines = false
                };

                NumericalAxis axis2 = new NumericalAxis()
                {
                    Header = "Acceleration rate",
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    LabelFormat = "0Kmph",
                    Minimum = "0",
                    Maximum = "300",
                    Interval = "75",
                    ShowGridLines = false
                };

                series1.XBindingPath = "Month";
                series1.YBindingPath = "Value1";
                series1.CustomTemplate = template1;
                series1.SegmentSpacing = 0.9;
                series1.ItemsSource = viewModelBar;
                series1.XAxis = axis1;
                series1.YAxis = axis2;
                Basic_Chart5.Series.Clear();
                Basic_Chart5.Series.Add(series1);
            }

            else if (comboBox.SelectedIndex == 1 && viewModelScatter != null)
            {
                Basic_Chart5.SetValue(Grid.RowSpanProperty, 2);
                Basic_Chart5.Axes.Clear();
                Basic_Chart5.Header = "Global Stock Trend Comparison (2013 - 2014)";
                Basic_Chart5.AreaBorderThickness = new Thickness(0, 0.5, 0.5, 0);
                Basic_Chart5.Height = 515;
                Basic_Chart5.AreaBorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xD1, 0xD3, 0xD4));
                DataTemplate template1 = MainGrid3.Resources["scattertemplate"] as DataTemplate;
                DataTemplate template2 = MainGrid3.Resources["scatterAdornmentTemplate"] as DataTemplate;
                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();
                DateTimeAxis axis1 = new DateTimeAxis()
                {
                    PlotOffset = 30,
                    Interval = 1,
                    IntervalType = DateTimeIntervalType.Months,
                    LabelFormat = "MMM",
                    Header = "Year 2014",
                    ShowGridLines = false
                };

                NumericalAxis axis2 = new NumericalAxis()
                {
                    Minimum = 10,
                    Maximum = 80,
                    Interval = 10,
                    Header = "Stock Price",
                    LabelFormat = "$0",
                };

                ScatterSeries series = new ScatterSeries();
                adornmentInfo1.ShowLabel = true;
                adornmentInfo1.SegmentLabelContent = LabelContent.LabelContentPath;
                adornmentInfo1.LabelTemplate = template2;
                series.XBindingPath = "Year";
                series.YBindingPath = "Count";
                series.CustomTemplate = template1;
                series.YAxis = axis2;
                series.XAxis = axis1;
                series.ItemsSource = viewModelScatter;
                series.ScatterWidth = 64;
                series.ScatterHeight = 64;
                series.AdornmentsInfo = adornmentInfo1;
                Basic_Chart5.Series.Clear();
                Basic_Chart5.Series.Add(series);
            }
            else if (comboBox.SelectedIndex == 2 && viewModelSpline != null)
            {
                Basic_Chart5.SetValue(Grid.RowSpanProperty, 2);
                Basic_Chart5.Axes.Clear();
                Basic_Chart5.Height = 515;
                Basic_Chart5.Header = "Climate Graph";

                Basic_Chart5.AreaBorderThickness = new Thickness(1, 1, 1, 1);
                Basic_Chart5.AreaBorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xD1, 0xD3, 0xD4));
                Basic_Chart5.AreaBorderThickness = new Thickness(0, 0.5, 0.5, 0);
                SplineSeries series = new SplineSeries();
                DataTemplate template1 = MainGrid3.Resources["splinetemplate"] as DataTemplate;
                DateTimeAxis axis1 = new DateTimeAxis()
                {
                    PlotOffset = 20,
                    Interval = 1,
                    IntervalType = DateTimeIntervalType.Months,
                    LabelFormat = "MMM",
                    Header = "Year 2014",
                    ShowGridLines = false,
                };

                NumericalAxis axis2 = new NumericalAxis()
                {
                    Minimum = -10,
                    Maximum = 30,
                    Interval = 5,
                    Header = "Temperature(in Celsius)",
                };
                
                ChartStripLine stripline = new ChartStripLine()
                {
                    Start = -0.2,
                    Width = 0.2,
                    Background = new SolidColorBrush(Colors.Red)
                };
                axis2.StripLines.Add(stripline);

                series.XBindingPath = "Month";
                series.YBindingPath = "Value2";
                series.YAxis = axis2;
                series.XAxis = axis1;
                series.ItemsSource = viewModelSpline;
                series.CustomTemplate = template1;
                Basic_Chart5.Series.Clear();
                Basic_Chart5.Series.Add(series);
            }
            
        }
    }

    public class ScatterChartViewModel : ObservableCollection<ScatterDataValues>
    {
        public ScatterChartViewModel()
        {
            DateTime date = new DateTime(2014, 1, 1);
            Add(new ScatterDataValues(date.AddMonths(0), 62, 6));
            Add(new ScatterDataValues(date.AddMonths(1), 50, 12));
            Add(new ScatterDataValues(date.AddMonths(2), 17, 2));
            Add(new ScatterDataValues(date.AddMonths(3), 34, 7));
            Add(new ScatterDataValues(date.AddMonths(4), 70, 15));
            Add(new ScatterDataValues(date.AddMonths(5), 22, 8));
            Add(new ScatterDataValues(date.AddMonths(6), 48, 3));
            Add(new ScatterDataValues(date.AddMonths(7), 53, 10));
            Add(new ScatterDataValues(date.AddMonths(8), 48, 14));

            res = new ResourceFactory();
            cai = new ChartAdornmentInfo();
            cai.ShowLabel = true;
            cai.SegmentLabelContent = LabelContent.LabelContentPath;
            cai.LabelTemplate = res.labelTemplate210;
        }
        public ChartAdornmentInfo cai { get; set; }
        public ResourceFactory res { get; set; }
    }

    public class BarChartViewModel : ObservableCollection<DataValuesBar>
    {

        public BarChartViewModel()
        {
            Add(new DataValuesBar("Convertible", 150, 350));
            Add(new DataValuesBar("Sedan", 220, 450));
            Add(new DataValuesBar("Hatchback", 100, 550));
            Add(new DataValuesBar("SUV", 240, 400));
            Add(new DataValuesBar("Truck", 180, 14));
        }
    }

    public class SplineChartViewModel : ObservableCollection<DataValuesSpline>
    {

        public SplineChartViewModel()
        {
            DateTime date = new DateTime(2014, 1, 1);
            Add(new DataValuesSpline(date.AddMonths(0), 6, -5, 64));
            Add(new DataValuesSpline(date.AddMonths(1), 7, 0.1, 30));
            Add(new DataValuesSpline(date.AddMonths(2), 8, 10, 22));
            Add(new DataValuesSpline(date.AddMonths(3), 10, 15, 19));
            Add(new DataValuesSpline(date.AddMonths(4), 13, 20, 20));
            Add(new DataValuesSpline(date.AddMonths(5), 17, 25, 34));
            Add(new DataValuesSpline(date.AddMonths(6), 18, 20, 45));
            Add(new DataValuesSpline(date.AddMonths(7), 17, 15, 47));
            Add(new DataValuesSpline(date.AddMonths(8), 14, 10, 55));
            Add(new DataValuesSpline(date.AddMonths(9), 12, 0, 60));
            Add(new DataValuesSpline(date.AddMonths(10), 8, -4, 64));
            Add(new DataValuesSpline(date.AddMonths(11), 8, -8, 68));
        }
    }

    public class DataValuesBar
    {
        public String Month
        {
            get;
            set;
        }

        public double Value1
        {
            get;
            set;
        }

        public decimal Value2
        {
            get;
            set;
        }

        public DataValuesBar(string month, double value1, decimal value2)
        {
            Month = month;
            Value1 = value1;
            Value2 = value2;
        }
    }

    public class ScatterDataValues
    {
        public DateTime Year
        {
            get;
            set;
        }

        public double Count
        {
            get;
            set;
        }

        public double Variation
        {
            get;
            set;
        }

        public ScatterDataValues(DateTime year, double count, double variation)
        {
            Year = year;
            Count = count;
            Variation = variation;
        }
    }

    public class DataValuesColumn1
    {
        public String Gadget
        {
            get;
            set;
        }

        public String Month
        {
            get;
            set;
        }

        public double Value1
        {
            get;
            set;
        }

        public double Value2
        {
            get;
            set;
        }

        public DataValuesColumn1(string gadget, string month, double value1, double value2)
        {
            Gadget = gadget;
            Value1 = value1;
            Value2 = value2;
            Month = month;
        }
    }

    public class DataValuesColumn
    {
        public String Gadget
        {
            get;
            set;
        }

        public String Month
        {
            get;
            set;
        }

        public double Value1
        {
            get;
            set;
        }

        public double Value2
        {
            get;
            set;
        }

        public DataValuesColumn(string gadget, string month, double value1, double value2)
        {
            Gadget = gadget;
            Value1 = value1;
            Value2 = value2;
            Month = month;
        }
    }

    public class DataValuesSpline
    {
        public DateTime Month
        {
            get;
            set;
        }

        public double Value1
        {
            get;
            set;
        }

        public double Value2
        {
            get;
            set;
        }

        public double Value3
        {
            get;
            set;
        }

        public DataValuesSpline(DateTime month, double value1, double value2, double value3)
        {
            Month = month;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }
    }

    public class ScatterAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var ydata = (value as ScatterSegment).YData;
            var angle = (ydata >= 25) ? 180 : 0;
            return angle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value;
        }
    }

    public class ScatterAdornmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var ydata = (value as ChartAdornment).YData;
            var variate = ((value as ChartAdornment).Item as ScatterDataValues).Variation;
            if (ydata >= 25)
                return String.Format("+" + variate);
            return String.Format("-" + variate);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value;
        }
    }

    public class ScatterAdornmentForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var ydata = (value as ChartAdornment).YData;
            if (ydata >= 25)
                return new SolidColorBrush(Colors.White);
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value;
        }
    }

    public class BubbleAdornmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var ydata = (value as ChartAdornment).YData;

            if (ydata == 40)
                return String.Format("1" + "\n" + "H");
            else if (ydata == 31)
                return String.Format(" 2" + "\n" + "He");
            else if (ydata == 22)
                return String.Format(" 8" + "\n" + " O");
            else if (ydata == 10)
                return String.Format(" 6" + "\n" + " C");
            else if (ydata == 18)
                return String.Format(" 7" + "\n" + " N");
            else if (ydata == 8)
                return String.Format("10" + "\n" + "Ne");
            return String.Format("14" + "\n" + " Si");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value;
        }
    }

    public class ScatterInteriorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var ydata = (value as ScatterSegment).YData;
            Brush interior;

            interior = ydata >= 25 ? new SolidColorBrush(Color.FromArgb(0xFF, 0x8B, 0xC3, 0x4A))
                                   : new SolidColorBrush(Color.FromArgb(0xFF, 0xD1, 0xD3, 0xD4));

            return interior;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value;
        }
    }

    public class ColumnPointsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var ydata = (value as ColumnSegment).XData;
            Point point = new Point();
            if (ydata == 0.0)
            {
                point.X = 0;
                point.Y = 0;
            }
            else if (ydata == 1.0)
            {
                point.X = 0;
                point.Y = 0.4;
            }
            else if (ydata == 3.0)
            {
                point.X = 0;
                point.Y = 0.4;
            }
            else if (ydata == 2.0)
            {
                point.X = 0;
                point.Y = 0.2;
            }
            else if (ydata == 4.0)
            {
                point.X = 0;
                point.Y = 0.1;
            }

            return point;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value;
        }
    }

    public class ColumnPointsConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var ydata = (value as ColumnSegment).XData;
            Point point = new Point();
            if (ydata == 0.0)
            {
                point.X = 0;
                point.Y = 0.25;
            }
            else if (ydata == 1.0)
            {
                point.X = 0;
                point.Y = 0.1;
            }
            else if (ydata == 3.0)
            {
                point.X = 0;
                point.Y = 0.25;
            }
            else if (ydata == 2.0)
            {
                point.X = 0;
                point.Y = 0;
            }
            else if (ydata == 4.0)
            {
                point.X = 0;
                point.Y = 0.2;
            }

            return point;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value;
        }
    }

    public class SplineValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var ydata = (value as SplineSegment).YData;
            Brush interior;
            interior = ydata > 0 ? new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xC1, 0x07))
                                 : new SolidColorBrush(Color.FromArgb(0xFF, 0xD1, 0xD3, 0xD4));
            return interior;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value;
        }
    }

    public class LineValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var ydata = (value as Syncfusion.UI.Xaml.Charts.StepLineSegment).X2;
            ydata = ydata - 12;
            return ydata;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value;
        }
    }

    public class LineValue2Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var ydata = (value as Syncfusion.UI.Xaml.Charts.StepLineSegment).Y2;
            ydata = ydata - 7;
            return ydata;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value;
        }
    }

    public class BarTopConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            CustomTemplate customTemplate = new CustomTemplate();
            var itemData = (value as BarSegment).XData;
            if (itemData == 0.0)
                return customTemplate.temp1;
            else if (itemData == 1.0)
                return customTemplate.temp2;
            else if (itemData == 2.0)
                return customTemplate.temp3;
            else if (itemData == 3.0)
                return customTemplate.temp4;
            else if (itemData == 4.0)
                return customTemplate.temp5;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value;
        }
    }
}
