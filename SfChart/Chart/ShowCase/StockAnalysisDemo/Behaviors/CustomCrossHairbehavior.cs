#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Windows.UI.Xaml.Input;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.System.Profile;

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public class CustomCrossHairbehavior : ChartCrossHairBehavior
    {

        public ItemsControl SummaryControl;

        public Style SummaryItemsStyle
        {
            get { return (Style)GetValue(SummaryItemsStyleProperty); }
            set { SetValue(SummaryItemsStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SummaryItemsStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SummaryItemsStyleProperty =
            DependencyProperty.Register("SummaryItemsStyle", typeof(Style), typeof(CustomCrossHairbehavior), new PropertyMetadata(null));

        int yCount;

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            base.OnPointerReleased(e);
            if (customInfo != null)
                customInfo.Visibility = Visibility.Collapsed;
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                SummaryControl.Visibility = Visibility.Collapsed;
        }

        protected override void OnPointerMoved(PointerRoutedEventArgs e)
        {
            base.OnPointerMoved(e);
            SummaryControl.Visibility = Visibility.Visible;
            if (PointInfos != null)
                PointInfos.Clear();
            if (ChartArea != null && IsActivated)
            {
                SetItemsSource(CurrentPoint, ChartArea.Series[0] as ISupportAxes);
                SummaryControl.ItemsSource = PointInfos.Reverse().ToList();
            }
        }

        protected override void OnLayoutUpdated()
        {
            if (SummaryControl != null && SummaryControl.Visibility == Visibility.Visible)
            {
                if (PointInfos != null)
                    PointInfos.Clear();
                if (ChartArea != null && IsActivated)
                {
                    SetItemsSource(CurrentPoint, ChartArea.Series[0] as ISupportAxes);
                    SummaryControl.ItemsSource = PointInfos.Reverse().ToList();
                }
            }
            base.OnLayoutUpdated();
        }

        ChartCustomInfo customInfo;
        private void SetItemsSource(Point point, ISupportAxes series)
        {
            var chartSeries = series as CartesianSeries;
            var pointx = Math.Truncate(ChartArea.PointToValue(series.ActualXAxis, point));
            var pointy = Math.Truncate(ChartArea.PointToValue(series.ActualYAxis, point));
            if (chartSeries == null || (!(pointx >= 0) || !(pointx < chartSeries.DataCount))) return;
            var dataSource = chartSeries.ItemsSource as List<StockDatas>;
            if (dataSource != null)
                customInfo = new ChartCustomInfo
                {
                    LabelX = chartSeries.Label,
                    ValueX = dataSource[(int) pointx].TimeStamp.ToString("MM-dd-yyyy"),
                    ValueY = pointy.ToString(series.ActualYAxis.LabelFormat, CultureInfo.CurrentCulture),
                    YValues = GetYValuesBasedOnIndex(pointx, chartSeries)
                };
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                customInfo.LabelY = ChartArea.SecondaryAxis.Header.ToString();
            yCount = customInfo.YValues.Count;
            customInfo.LabelYValues = GetLabelYValues();
            customInfo.Visibility = Visibility.Visible;
            PointInfos.Add(customInfo);
            PositionSummaryControl(point, chartSeries);
        }

        internal void ResetElements()
        {
            if (customInfo != null)
                customInfo.Visibility = Visibility.Collapsed;
            if (SummaryControl != null)
                SummaryControl.Visibility = Visibility.Collapsed;
            var point = new Point(0 + ChartArea.AxisThickness.Left, 0);
            CurrentPoint = point;
            SetPosition(point);
        }

        private List<string> GetLabelYValues()
        {
            var values = yCount > 1 ? new List<string> { "High", "Low", "Open", "Close" } : new List<string> { "Volume" };
            return values;
        }

        protected override void AttachElements()
        {
            base.AttachElements();
            if (SummaryControl == null)
            {
                SummaryControl = new ItemsControl();
                var binding = new Binding {Source = this, Path = new PropertyPath("SummaryItemsStyle")};
                SummaryControl.SetBinding(FrameworkElement.StyleProperty, binding);
                AdorningCanvas.Children.Add(SummaryControl);
                Canvas.SetZIndex(SummaryControl, 120);
            }
        }

        private void PositionSummaryControl(Point point,ChartSeries series)
        {
            var rect=ChartArea.SeriesClipRect;
            var supportAxes = series as ISupportAxes;
            if (supportAxes == null) return;
            double width = Math.Truncate(ChartArea.PointToValue(supportAxes.ActualXAxis, new Point(SummaryControl.ActualWidth,0)));
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                if (point.Y < SummaryControl.ActualHeight + 25)
                {
                    Canvas.SetTop(SummaryControl, 183);
                    Canvas.SetLeft(SummaryControl, 6);
                }
                else
                {

                    Canvas.SetTop(SummaryControl, -33);
                    Canvas.SetLeft(SummaryControl, 6);
                }
            }
            if (point.X < rect.Left + width)
            {
                Canvas.SetLeft(SummaryControl, point.X + SummaryControl.ActualWidth);
                Canvas.SetTop(SummaryControl, Convert.ToDouble(ChartArea.SeriesClipRect.Top));
            }
            else
            {
                Canvas.SetLeft(SummaryControl, ChartArea.SeriesClipRect.Left);
                Canvas.SetTop(SummaryControl, Convert.ToDouble(ChartArea.SeriesClipRect.Top));
            }
        }

        protected override void GenerateLabel(ChartPointInfo pointInfo, ChartAxis axis)
        {

        }
    }

    public class ChartCustomInfo : ChartPointInfo
    {
        private IList<double> yValues;

        public IList<double> YValues
        {
            get
            {
                return yValues;
            }
            set
            {
                if (value == yValues) return;
                yValues = value;
                OnPropertyChanged("YValues");
            }
        }

        private Visibility visibility;

        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                if (value != visibility)
                {
                    visibility = value;
                    OnPropertyChanged("Visibility");
                }
            }
        }

        private List<string> ylabelValues;

        public List<string> LabelYValues
        {
            get
            {
                return ylabelValues;
            }
            set
            {
                if (value != ylabelValues)
                {
                    ylabelValues = value;
                    OnPropertyChanged("LabelYValues");
                }
            }
        }

        private string labelX;
        private string labelY;

        /// <summary>
        /// Gets or Sets the x label.
        /// </summary>
        public string LabelX
        {
            get
            {
                return labelX;
            }
            set
            {
                if (value != labelX)
                {
                    labelX = value;
                    OnPropertyChanged("LabelX");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the y label.
        /// </summary>
        public string LabelY
        {
            get
            {
                return labelY;
            }
            set
            {
                if (value != labelY)
                {
                    labelY = value;
                    OnPropertyChanged("LabelY");
                }
            }
        }
    }
}
