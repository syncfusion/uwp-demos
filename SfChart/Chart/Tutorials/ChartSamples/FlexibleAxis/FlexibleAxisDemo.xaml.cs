using Common;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
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
    public sealed partial class FlexibleAxisDemo : SampleLayout
    {
        public FlexibleAxisDemo()
        {
            this.InitializeComponent();
        }

        public override void Dispose()
        {
            if ((this.DataContext as FlexibleAxisDemoViewModel) != null)
                (this.DataContext as FlexibleAxisDemoViewModel).Dispose();

            if (this.columnChart != null)
            {
                foreach (var series in this.columnChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.columnChart = null;
            }

            if (this.grid.DataContext != null)
                this.grid.DataContext = null;
            this.grid.Resources = null;

            base.Dispose();
        }
    }


    public class FlexibleAxisDemoViewModel : IDisposable
    {
        public List<FlexibleAxisDemoModel> stocks
        {
            get;
            set;
        }

        public FlexibleAxisDemoViewModel()
        {
            stocks = new List<FlexibleAxisDemoModel>();
            DateTime date = new DateTime(2009, 1, 1);
            Random randomNumber = new Random();
            double value = 100d;
            int count = 0;
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                count = 100;
            }
            else
                count = 40;
            for (int i = 0; i < count; i++)
            {
                if (randomNumber.NextDouble() > .6)
                {
                    value += randomNumber.NextDouble();
                }
                else
                {
                    value -= randomNumber.NextDouble();
                }
                double high = value;
                double low = value - randomNumber.Next(4, 6);
                double open = randomNumber.Next((int)low + 1, (int)high - 1);
                double close = randomNumber.Next((int)low + 1, (int)high - 1);
                double volume = randomNumber.Next(1, 2000);
                var dateTime = date.AddDays(i);
                stocks.Add(new FlexibleAxisDemoModel(dateTime, high, low, open, close, volume));
            }
        }

        public void Dispose()
        {
            if (this.stocks != null)
                this.stocks.Clear();
        }
    }

    public class FlexibleAxisDemoModel
    {
        public FlexibleAxisDemoModel(DateTime date, double high, double low, double open, double close, double volume)
        {
            TimeStamp = date;
            High = high;
            Low = low;
            Open = open;
            Close = close;
            Volume = volume;
        }

        public DateTime TimeStamp
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

        public double Volume
        {
            get;
            set;
        }
    }
}
