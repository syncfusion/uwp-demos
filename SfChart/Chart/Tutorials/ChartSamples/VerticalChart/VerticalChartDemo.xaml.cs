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
    public sealed partial class VerticalChartDemo : SampleLayout
    {
        DispatcherTimer timer = new DispatcherTimer();
        DateTime date;
        int count = 0;
        public VerticalChartDemo()
        {
            this.InitializeComponent();
            date = DateTime.Now;
            timer.Start();
            timer.Interval = TimeSpan.FromMilliseconds(2);
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, object e)
        {
            var datas = (this.chart.Series[0].ItemsSource as DataDetails);
            if (datas == null) return;
            date = datas[datas.Count - 1].Speed.AddSeconds(1);
            count = count + 1;
            Random rand = new Random();
            if (count > 350)
            {
                timer.Stop();
            }
            else if (count > 300)
            {
                (this.chart.Series[0].ItemsSource as DataDetails).Add(new Data1()
                {
                    Speed = date,
                    Rate = rand.Next(0, 0),
                    Rate1 = rand.Next(0, 1),
                    Rate2 = rand.Next(100, 250)
                });
            }
            else if (count > 250)
            {
                (this.chart.Series[0].ItemsSource as DataDetails).Add(new Data1()
                {
                    Speed = date,
                    Rate = rand.Next(-2, 1),
                    Rate1 = rand.Next(-2, 2),
                    Rate2 = rand.Next(100, 250)
                });
            }
            else if (count > 180)
            {
                (this.chart.Series[0].ItemsSource as DataDetails).Add(new Data1()
                {
                    Speed = date,
                    Rate = rand.Next(-3, 2),
                    Rate1 = rand.Next(-2, 3),
                    Rate2 = rand.Next(100, 250)
                });
            }
            else if (count > 100)
            {
                (this.chart.Series[0].ItemsSource as DataDetails).Add(new Data1()
                {
                    Speed = date,
                    Rate = rand.Next(-7, 6),
                    Rate1 = rand.Next(-6, 7),
                    Rate2 = rand.Next(100, 250)
                });
            }
            else
            {
                (this.chart.Series[0].ItemsSource as DataDetails).Add(new Data1()
                {
                    Speed = date,
                    Rate = rand.Next(-9, 9),
                    Rate1 = rand.Next(-9, 9),
                    Rate2 = rand.Next(100, 250)
                });
            }
        }

        public override void Dispose()
        {
            if (this.chart != null)
            {
                foreach (var series in this.chart.Series)
                {
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                }
            }

            this.grid.Resources = null;

            base.Dispose();
        }
    }

    public class DataDetails : ObservableCollection<Data1>
    {
        public DataDetails()
        {
            Random rand = new Random();
            DateTime dt = new DateTime(2014, 7, 8, 5, 20, 10);

            for (int i = 11; i < 50; i++)
            {
                dt = dt.AddSeconds(1);
                this.Add(new Data1
                {
                    Rate = rand.Next(-3, 3),
                    Speed = dt,
                    Rate1 = rand.Next(-2, 2),
                    Rate2 = rand.Next(110, 240)
                });
                dt = dt.AddSeconds(1);
            }

        }
    }

    public class Data1
    {
        public DateTime Speed
        {
            get;
            set;
        }

        public double Rate
        {
            get;
            set;
        }

        public double Rate1
        {
            get;
            set;
        }

        public double Rate2
        {
            get;
            set;
        }
    }
}
