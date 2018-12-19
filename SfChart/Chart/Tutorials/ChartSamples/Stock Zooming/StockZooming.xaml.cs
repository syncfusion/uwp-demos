#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Syncfusion.UI.Xaml.Charts;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.Text;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StockZooming : SampleLayout
    {
        ZoomModel Data = new ZoomModel();
        public StockZooming()
        {
            this.InitializeComponent();
            series.StrokeThickness = 0.3;
            series.Stroke = new SolidColorBrush(Color.FromArgb(255, 27, 161, 226));
            series1.StrokeThickness = 0.3;
            series1.Stroke = new SolidColorBrush(Color.FromArgb(255, 27, 161, 226));

            // Since the control is placed in a list view. The range navigators's manipulation has to be checked for good interactive behavior support.
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                this.RangeNavigator.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            }         
        }

        private void Primary_LabelCreated(object sender, Syncfusion.UI.Xaml.Charts.LabelCreatedEventArgs e)
        {
            if (Primary.ZoomPosition == 0 && Primary.ZoomFactor > 0.9 && RangeNavigator.Intervals[1].IntervalType == Syncfusion.UI.Xaml.Charts.NavigatorIntervalType.Year)
                e.AxisLabel.LabelContent = e.AxisLabel.Position.FromOADate().ToString("MMM,yyyy");
            else
                e.AxisLabel.LabelContent = e.AxisLabel.Position.FromOADate().ToString("dd,MMM");
        }


        private void onemonthdata_Click(object sender, RoutedEventArgs e)
        {
            var dateTime = Convert.ToDateTime(RangeNavigator.ViewRangeEnd);
            RangeNavigator.ViewRangeEnd = dateTime;
            RangeNavigator.ViewRangeStart = dateTime.AddMonths(-1);
        }

        private void threemonthsdata_Click(object sender, RoutedEventArgs e)
        {
            var dateTime = Convert.ToDateTime(RangeNavigator.ViewRangeEnd);
            RangeNavigator.ViewRangeEnd = dateTime;
            RangeNavigator.ViewRangeStart = dateTime.AddMonths(-3);
        }

        private void sixmonthsdata_Click(object sender, RoutedEventArgs e)
        {
            var dateTime = Convert.ToDateTime(RangeNavigator.ViewRangeEnd);
            RangeNavigator.ViewRangeEnd = dateTime;
            RangeNavigator.ViewRangeStart = dateTime.AddMonths(-6);
        }

        private void yeartodate_Click(object sender, RoutedEventArgs e)
        {
            int count = Data.StockPriceDetails.Count;
            var dateTime = Data.StockPriceDetails[count - 1].Date;
            RangeNavigator.ViewRangeEnd = dateTime;
            RangeNavigator.ViewRangeStart = new DateTime(dateTime.Year, 1, 1);
        }

        private void oneyeardata_Click(object sender, RoutedEventArgs e)
        {
            var dateTime = Convert.ToDateTime(RangeNavigator.ViewRangeEnd);
            RangeNavigator.ViewRangeEnd = dateTime;
            RangeNavigator.ViewRangeStart = dateTime.AddYears(-1);
        }

        private void Alldatas_Click(object sender, RoutedEventArgs e)
        {
            int count = Data.StockPriceDetails.Count;
            RangeNavigator.ViewRangeStart = Data.StockPriceDetails[0].Date;
            RangeNavigator.ViewRangeEnd = Data.StockPriceDetails[count - 1].Date;
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Hand, 1);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 0);
        }

      
    }

    public class StockPrice
    {
        public DateTime Date
        {
            get;
            set;
        }
        public double YValue
        {
            get;
            set;
        }
    }

    public class ZoomModel
    {
        public ZoomModel()
        {
            this.StockPriceDetails = new ObservableCollection<StockPrice>();
            DateTime date = new DateTime(2010, 5, 19);
            Random rd = new Random();
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                double value = 70;
                for (int i = 0; i < 2555; i++)
                {
                    if (rd.NextDouble() > .5)
                        value += rd.NextDouble();
                    else
                        value -= rd.NextDouble();
                    if (value >= 110) value = 110;
                    if (value <= 20) value = 20;

                    this.StockPriceDetails.Add(new StockPrice()
                    {

                        Date = date.AddDays(i),
                        YValue = value,

                    });
                }
            }
            else
            {
                DateTime date1 = new DateTime(2009, 1, 1);
                double value = 70;
                for (int i = 0; i < 730; i++)
                {
                    if (rd.NextDouble() > .5)
                        value += rd.NextDouble();
                    else
                        value -= rd.NextDouble();
                    if (value >= 110) value = 110;
                    if (value <= 20) value = 20;

                    this.StockPriceDetails.Add(new StockPrice()
                    {
                        Date = date1.AddDays(i),
                        YValue = value,

                    });
                }
            }
        }

        public ObservableCollection<StockPrice> StockPriceDetails
        {
            get;
            set;
        }
    }

    public class TrackballLabelConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime date = (DateTime) value;
            return date.ToString("D");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}