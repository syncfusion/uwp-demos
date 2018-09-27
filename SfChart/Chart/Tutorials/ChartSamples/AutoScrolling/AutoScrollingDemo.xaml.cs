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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public sealed partial class AutoScrollingDemo : SampleLayout
    {
        private Syncfusion.UI.Xaml.Charts.SfChart chart;
        DispatcherTimer timer = new DispatcherTimer();
        DateTime date;
        public AutoScrollingDemo()
        {
            this.InitializeComponent();
            date = new DateTime();
            chart = Chart1;
            timer.Start();
            timer.Interval = TimeSpan.FromMilliseconds(150);
            timer.Tick += timer_Tick;
            XAxis.ActualRangeChanged += XAxis_ActualRangeChanged;
        }

        private void XAxis_ActualRangeChanged(object sender, ActualRangeChangedEventArgs e)
        {
            if (!e.IsScrolling && e.ActualMaximum != null)
            {
                e.VisibleMinimum = (double)e.ActualMaximum - 50d;
            }
        }

        private void timer_Tick(object sender, object e)
        {
            if (this.chart.Series[0].ItemsSource == null) return;
            var data = this.chart.DataContext as ProductDetails;
            date = data[data.Count - 1].Speed.AddMinutes(1d);
            Random rand = new Random();
            data.Add(new Product() { Speed = date, Rate = rand.Next(100, 250) });
        }

        public override void Dispose()
        {
            if (XAxis != null)
                XAxis.ActualRangeChanged -= XAxis_ActualRangeChanged;

            if (timer != null)
                timer.Tick -= timer_Tick;

            if (chart != null && chart.DataContext is ProductDetails)
                (chart.DataContext as ProductDetails).Dispose();

            if (this.Chart1 != null)
            {
                foreach (var series in Chart1.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.Chart1 = null;
            }

            if (this.chart != null)
            {
                foreach (var series in chart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.chart = null;
            }

            this.mainGrid.Resources = null;

            base.Dispose();
        }
    }

    public class Product
    {
        public DateTime Speed
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public double Rate
        {
            get;
            set;
        }
    }

    public class ProductDetails : ObservableCollection<Product>
    {
        public ProductDetails()
        {
            Random rand = new Random();
            DateTime dt = DateTime.Now;

            for (int i = 11; i < 140; i++)
            {
                this.Add(new Product { Rate = rand.Next(110, 240), Speed = dt });
                dt = dt.AddMinutes(i);
            }

        }

        public void Dispose()
        {
            this.Clear();
        }
    }

    public class ProductDetails5 : ObservableCollection<Product>
    {
        public ProductDetails5()
        {
            Random rand = new Random();
            DateTime dt = DateTime.Now;
            int count = AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile" ? 30 : 20;
            for (int i = 10; i < count; i++)
            {
                this.Add(new Product { Rate = rand.Next(110, 240), Speed = dt, Date = dt.AddDays(i) });
                dt = dt.AddMinutes(i);
            }

        }
    }
}
