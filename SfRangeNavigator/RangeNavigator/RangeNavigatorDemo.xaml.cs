#region Copyright Syncfusion Inc. 2001-2017.
// Copyright Syncfusion Inc. 2001-2017. All rights reserved.
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

namespace Syncfusion.SampleBrowser.UWP.SfRangeNavigator
{
    public sealed partial class RangeNavigatorDemo : SampleLayout
    {
        Model Data;
        public RangeNavigatorDemo()
        {
            this.InitializeComponent();
            Data = new Model();
        }
        public override void Dispose()
        {
            if(grid != null)
            {
                var dataContext = grid.DataContext as Model;
                if (dataContext != null)
                    dataContext.Dispose();
                if (financialChart != null)
                {
                    foreach (var series in financialChart.Series)
                        series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                    financialChart = null;
                }
                if (RangeNavigator != null)
                {
                    var contentChart = RangeNavigator.Content as Syncfusion.UI.Xaml.Charts.SfChart;
                    if (contentChart != null)
                    {
                        foreach (var series in contentChart.Series)
                            series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                        contentChart = null;
                    }
                    RangeNavigator.ClearValue(SfDateTimeRangeNavigator.ItemsSourceProperty);
                    RangeNavigator = null;
                }
            }        
            base.Dispose();
        }
    }

    public class StockPrice
    {
        public DateTime Date
        {
            get;
            set;
        }
        public string Level
        {
            get;
            set;
        }
        public double NumLevel
        {
            get;
            set;
        }
        public double High
        {
            get;
            set;
        }
        public double Low
        {
            get;
            set;
        }
        public double Open
        {
            get;
            set;
        }
        public double Close
        {
            get;
            set;
        }
    }

    public class Model
    {
        public Model()
        {
            this.StockPriceDetails = new ObservableCollection<StockPrice>();
            DateTime date = new DateTime(2012, 1, 1);
            Random rd = new Random();
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                for (int i = 0; i < 182; i++)
                {
                    this.StockPriceDetails.Add(new StockPrice()
                    {
                        Date = date.AddDays(i),
                        Open = rd.Next(870, 875),
                        High = rd.Next(876, 890),
                        Low = rd.Next(850, 855),
                        Close = rd.Next(856, 860)
                    });
                }
            }
            else
            {
                for (int i = 0; i < 91; i++)
                {
                    this.StockPriceDetails.Add(new StockPrice()
                    {
                        Date = date.AddDays(i),
                        Open = rd.Next(870, 875),
                        High = rd.Next(876, 890),
                        Low = rd.Next(850, 855),
                        Close = rd.Next(856, 860)
                    });
                }
            }
        }

        public void Dispose()
        {
            if (StockPriceDetails != null)
                StockPriceDetails.Clear();
        }

        public ObservableCollection<StockPrice> StockPriceDetails
        {
            get;
            set;
        }
    }
}
