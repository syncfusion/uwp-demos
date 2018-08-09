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
        private ColumnChartViewModel viewModelColumn;
        private ColumnChartViewModel1 viewModelColumn1;
        private ScatterChartViewModel viewModelScatter;
        private SplineChartViewModel viewModelSpline;
        private BarChartViewModel viewModelBar;
        public CustomTemplate()
        {
            this.InitializeComponent();
            viewModelColumn = new ColumnChartViewModel();
            viewModelColumn1 = new ColumnChartViewModel1();
            viewModelScatter = new ScatterChartViewModel();
            viewModelSpline = new SplineChartViewModel();
            viewModelBar = new BarChartViewModel();

            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                Basic_Chart5.Series[0].ItemsSource = viewModelColumn;
                Basic_Chart5.Series[1].ItemsSource = viewModelColumn;
                Basic_Chart51.Series[0].ItemsSource = viewModelColumn1;
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

            if (this.Basic_Chart51 != null)
            {
                foreach (var series in this.Basic_Chart51.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.Basic_Chart51 = null;
            }

            this.MainGrid3.Resources = null;

            base.Dispose();
        }

        private void OnChartTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;
            if (Basic_Chart5 == null) return;

            if (comboBox.SelectedIndex == 0 && viewModelColumn != null && viewModelColumn1 != null)
            {
                (Basic_Chart5.Legend as ChartLegend).Visibility = Visibility.Visible;
                Basic_Chart51.Visibility = Visibility.Visible;
                Basic_Chart5.SetValue(Grid.RowSpanProperty, 1);
                Basic_Chart5.Axes.Clear();
                Basic_Chart51.Axes.Clear();
                Basic_Chart5.AreaBorderThickness = new Thickness(1, 0, 0, 1);
                Basic_Chart5.AreaBorderThickness = new Thickness(1, 0, 0, 1);
                Basic_Chart5.Height = 257;
                Basic_Chart5.Width = 894;
                Basic_Chart51.Height = 257;
                Basic_Chart51.Width = 894;
                Basic_Chart5.Header = new TextBlock() { Text = "Literacy rate in Nation", FontWeight = FontWeights.SemiBold };
                Basic_Chart51.Header = new TextBlock()
                {
                    Text = "Economic Growth - Year 2014",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(50,20,0,0),
                    FontWeight = FontWeights.Medium,
                    FontFamily = new FontFamily("Segoe UI")
                };
                ColumnSeries series1 = new ColumnSeries();
                ColumnSeries series2 = new ColumnSeries();
                ColumnSeries series3 = new ColumnSeries();
                DataTemplate template1 = MainGrid3.Resources["labelTemplate"] as DataTemplate;
                DataTemplate template2 = MainGrid3.Resources["headerTemplate"] as DataTemplate;
                DataTemplate template3 = MainGrid3.Resources["columnnewtemp1"] as DataTemplate;
                DataTemplate template7 = MainGrid3.Resources["columnnewtemp2"] as DataTemplate;
                Style template5 = MainGrid3.Resources["axisLineStyle"] as Style;
                Style template6 = MainGrid3.Resources["minorTickLineStyle"] as Style;
                DataTemplate template8 = MainGrid3.Resources["columnnewtemp3"] as DataTemplate;
                DataTemplate template4 = MainGrid3.Resources["postfixTemplatecc"] as DataTemplate;
                CategoryAxis axis1 = new CategoryAxis()
                {
                    ShowGridLines = false,
                    LabelPlacement = Syncfusion.UI.Xaml.Charts.LabelPlacement.BetweenTicks,
                    AxisLineStyle = template5,
                    MajorTickLineStyle = template6
                };
                NumericalAxis axis2 = new NumericalAxis()
                {
                    ShowGridLines = false,
                    AxisLineStyle = template5,
                    MajorTickLineStyle = template6,
                    PostfixLabelTemplate = template4,
                    Minimum = "0",
                    Maximum = "100",
                    Interval = "20",
                    PlotOffset = 20
                };
                CategoryAxis axis3 = new CategoryAxis()
                {
                    ShowGridLines = false,
                    LabelPlacement = Syncfusion.UI.Xaml.Charts.LabelPlacement.BetweenTicks,
                    AxisLineStyle = template5,
                    MajorTickLineStyle = template6
                };
                NumericalAxis axis4 = new NumericalAxis()
                {
                    ShowGridLines = false,
                    AxisLineStyle = template5,
                    MajorTickLineStyle = template6,
                    PostfixLabelTemplate = template4,
                    Minimum = "0",
                    Maximum = "30",
                    Interval = "10",
                    PlotOffset = 20
                };
                series3.XAxis = axis3;
                series3.YAxis = axis4;
                Basic_Chart5.PrimaryAxis = axis1;
                Basic_Chart5.SecondaryAxis = axis2;
                series3.XBindingPath = "Gadget";
                series3.YBindingPath = "Value2";
                series3.ItemsSource = viewModelColumn1;
                series3.LegendIcon = ChartLegendIcon.Rectangle;
                series3.SegmentSpacing = 0.5;
                series3.Interior = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xC1, 0x07));
                series3.CustomTemplate = template8;
                series1.XBindingPath = "Gadget";
                series1.YBindingPath = "Value1";
                series1.Label = "Educated";
                series3.Label = "Year 2014";
                series2.Label = "Uneducated";
                series1.ItemsSource = viewModelColumn;
                series1.LegendIcon = ChartLegendIcon.Rectangle;
                series1.Interior = new SolidColorBrush(Color.FromArgb(0xFF, 0x8B, 0xC3, 0x4A));
                series2.Interior = new SolidColorBrush(Color.FromArgb(0xFF, 0xD1, 0xD3, 0xD4));
                series1.CustomTemplate = template3;
                series2.LegendIcon = ChartLegendIcon.Rectangle;
                series2.XBindingPath = "Gadget";
                series2.YBindingPath = "Value1";
                series2.ItemsSource = viewModelColumn;
                series2.CustomTemplate = template7;
                Basic_Chart5.Series.Clear();
                Basic_Chart51.Series.Clear();
                Basic_Chart5.Series.Add(series1);
                Basic_Chart5.Series.Add(series2);
                Basic_Chart51.Series.Add(series3);

            }
            else if (comboBox.SelectedIndex == 2 && viewModelScatter != null)
            {
                Basic_Chart51.Visibility = Visibility.Collapsed;
                (Basic_Chart5.Legend as ChartLegend).Visibility = Visibility.Collapsed;
                Basic_Chart5.SetValue(Grid.RowSpanProperty, 2);
                Basic_Chart5.Axes.Clear();
                Basic_Chart51.Axes.Clear();
                Basic_Chart51.Series.Clear();
                Basic_Chart5.Header = new TextBlock()
                { Text = "Global Stock Trend Comparison (2013 - 2014)", Margin = new Thickness(50, 10, 0, 10),
                    FontSize = 20, FontWeight = FontWeights.SemiBold };
                Basic_Chart5.AreaBorderThickness = new Thickness(0, 0.5, 0.5, 0);
                Basic_Chart5.Height = 515;
                Basic_Chart5.AreaBorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xD1, 0xD3, 0xD4));
                DataTemplate template1 = MainGrid3.Resources["scattertemplate"] as DataTemplate;
                DataTemplate template2 = MainGrid3.Resources["scatterAdornmentTemplate"] as DataTemplate;
                ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo();
                DataTemplate template3 = MainGrid3.Resources["labelTemplate"] as DataTemplate;
                DataTemplate template4 = MainGrid3.Resources["headerTemplate"] as DataTemplate;
                Style axisstyle = MainGrid3.Resources["axisLineStyle"] as Style;
                Style ticklinestyle = MainGrid3.Resources["minorTickLineStyle"] as Style;
                DateTimeAxis axis1 = new DateTimeAxis()
                {
                    PlotOffset = 30,
                    Interval = 1,
                    IntervalType = DateTimeIntervalType.Months,
                    LabelFormat = "MMM",
                    Header = "Year 2014",
                    LabelTemplate = template3,
                    HeaderTemplate = template4,
                    AxisLineStyle = axisstyle,
                    MajorTickLineStyle = ticklinestyle,
                    ShowGridLines = false
                };
                NumericalAxis axis2 = new NumericalAxis()
                {
                    Minimum = 10,
                    Maximum = 80,
                    Interval = 10,
                    Header = "Stock Price",
                    LabelFormat = "$0",
                    LabelTemplate = template3,
                    HeaderTemplate = template4,
                    AxisLineStyle = axisstyle,
                    MajorTickLineStyle = ticklinestyle,
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
            else if (comboBox.SelectedIndex == 3 && viewModelSpline != null)
            {
                Basic_Chart51.Visibility = Visibility.Collapsed;
                (Basic_Chart5.Legend as ChartLegend).Visibility = Visibility.Collapsed;
                Basic_Chart5.SetValue(Grid.RowSpanProperty, 2);
                Basic_Chart5.Axes.Clear();
                Basic_Chart51.Axes.Clear();
                Basic_Chart51.Series.Clear();
                Basic_Chart5.Height = 515;
                Basic_Chart5.Header = new TextBlock()
                { Text = "Climate Graph", Margin = new Thickness(50, 10, 0, 10), FontSize = 20, FontWeight = FontWeights.SemiBold };
                Basic_Chart5.AreaBorderThickness = new Thickness(1, 1, 1, 1);
                Basic_Chart5.AreaBorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xD1, 0xD3, 0xD4));
                Basic_Chart5.AreaBorderThickness = new Thickness(0, 0.5, 0.5, 0);
                SplineSeries series = new SplineSeries();
                DataTemplate template1 = MainGrid3.Resources["splinetemplate"] as DataTemplate;
                DataTemplate template3 = MainGrid3.Resources["labelTemplate"] as DataTemplate;
                DataTemplate template4 = MainGrid3.Resources["headerTemplate"] as DataTemplate;
                DataTemplate template5 = MainGrid3.Resources["postfixTemplateSpline"] as DataTemplate;
                Style axisstyle = MainGrid3.Resources["axisLineStyle"] as Style;
                Style ticklinestyle = MainGrid3.Resources["minorTickLineStyle"] as Style;
                DateTimeAxis axis1 = new DateTimeAxis()
                {
                    PlotOffset = 20,
                    Interval = 1,
                    IntervalType = DateTimeIntervalType.Months,
                    LabelFormat = "MMM",
                    Header = "Year 2014",
                    LabelTemplate = template3,
                    ShowGridLines = false,
                    AxisLineStyle = axisstyle,
                    MajorTickLineStyle = ticklinestyle,
                    HeaderTemplate = template4
                };
                NumericalAxis axis2 = new NumericalAxis()
                {
                    Minimum = -10,
                    Maximum = 30,
                    Interval = 5,
                    Header = "Temperature(in Celsius)",
                    HeaderTemplate = template4,
                    LabelTemplate = template3,
                    AxisLineStyle = axisstyle,
                    MajorTickLineStyle = ticklinestyle

                };
                series.XBindingPath = "Month";
                series.YBindingPath = "Value2";
                series.YAxis = axis2;
                series.XAxis = axis1;
                series.ItemsSource = viewModelSpline;
                series.CustomTemplate = template1;
                Basic_Chart5.Series.Clear();
                Basic_Chart5.Series.Add(series);

            }
            else if (comboBox.SelectedIndex == 1 && viewModelBar != null)
            {
                (Basic_Chart5.Legend as ChartLegend).Visibility = Visibility.Collapsed;
                Basic_Chart51.Visibility = Visibility.Collapsed;
                Basic_Chart5.SetValue(Grid.RowSpanProperty, 2);
                Basic_Chart5.Axes.Clear();
                Basic_Chart51.Axes.Clear();
                Basic_Chart51.Series.Clear();
                Basic_Chart5.Height = 515;
                Basic_Chart5.Header = new TextBlock()
                { Text = "Car Speed Comparison", Margin = new Thickness(50, 10, 0, 10), FontSize = 20, FontWeight = FontWeights.SemiBold };
                Basic_Chart5.AreaBorderThickness = new Thickness(0, 0, 0, 0);
                BarSeries series1 = new BarSeries();
                DataTemplate template3 = MainGrid3.Resources["labelTemplate"] as DataTemplate;
                DataTemplate template7 = MainGrid3.Resources["labelTemplate1"] as DataTemplate;
                DataTemplate template4 = MainGrid3.Resources["headerTemplate"] as DataTemplate;
                DataTemplate template1 = MainGrid3.Resources["newBarTemplate"] as DataTemplate;
                Style template5 = MainGrid3.Resources["axisLineStyle"] as Style;
                Style template6 = MainGrid3.Resources["minorTickLineStyle"] as Style;
                CategoryAxis axis1 = new CategoryAxis()
                {
                    LabelTemplate = template3,
                    AxisLineStyle = template5,
                    MajorTickLineStyle = template6,
                    ShowGridLines = false
                };
                NumericalAxis axis2 = new NumericalAxis()
                {
                    Header = "Acceleration rate",
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    LabelTemplate = template7,
                    AxisLineStyle = template5,
                    MajorTickLineStyle = template6,
                    HeaderTemplate = template4,
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

    public class ColumnChartViewModel : ObservableCollection<DataValuesColumn>
    {

        public ColumnChartViewModel()
        {
            Add(new DataValuesColumn("State 1", "2009", 100, 18));
            Add(new DataValuesColumn("State 2", "2010", 100, 23));
            Add(new DataValuesColumn("State 3", "2011", 100, 26));
            Add(new DataValuesColumn("State 4", "2012", 100, 18));
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                Add(new DataValuesColumn("State 5", "2013", 100, 24));
        }
    }

    public class ColumnChartViewModel1 : ObservableCollection<DataValuesColumn1>
    {
        public ColumnChartViewModel1()
        {
            Add(new DataValuesColumn1("Quarter 1", "2009", 100, 18));
            Add(new DataValuesColumn1("Quarter 2", "2010", 100, 23));
            Add(new DataValuesColumn1("Quarter 3", "2011", 100, 26));
            Add(new DataValuesColumn1("Quarter 4", "2012", 100, 24));
        }
    }

    public class BarChartViewModel : ObservableCollection<DataValuesBar>
    {

        public BarChartViewModel()
        {
            Add(new DataValuesBar("Convertible", 260, 350));
            Add(new DataValuesBar("Sedan", 245, 450));
            Add(new DataValuesBar("Hatchback", 235, 550));
            Add(new DataValuesBar("SUV", 240, 400));
            Add(new DataValuesBar("Truck", 230, 14));
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
