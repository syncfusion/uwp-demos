using Common;
using System;
using System.Collections.Generic;
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

namespace GaugeUWP_Samples
{
    public sealed partial class DigitalGauge : SampleLayout, IDisposable
    {
        internal DispatcherTimer timer = new DispatcherTimer();
        DateTime datetime;
        public DigitalGauge()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                this.main_grid.ColumnDefinitions.Clear();
                this.m_grid.Margin = new Thickness(10, 100, 0, 100);
                this.LayoutRoot.Margin = new Thickness(0, 10, 0, 10);
            }
            else
            {
                this.m_grid.Width = 430;
            }
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(0, 0, 1);

            timer.Tick += timer_Tick;
            timer.Start();
            datetime = DateTime.Now;
            timeGauge.Value = DateTime.Now.ToString("HH-mm");
            secGauge.Value = datetime.Second.ToString();

            dateGauge.Value = datetime.Day.ToString();
            monthGauge.Value = datetime.Month.ToString();
            dayGauge.Value = datetime.DayOfWeek.ToString().Remove(3, datetime.DayOfWeek.ToString().Length - 3).ToUpper();


        }

        void timer_Tick(object sender, object e)
        {
            timeGauge.Value = DateTime.Now.ToString("HH-mm");
            secGauge.Value = DateTime.Now.Second.ToString();
        }

        public override void Dispose()
        {
            if (this.dayGauge != null)
            {
                this.dayGauge.Dispose();
                this.dayGauge = null;
            }
            if (this.dateGauge != null)
            {
                this.dateGauge.Dispose();
                this.dateGauge = null;
            }
            if (this.monthGauge != null)
            {
                this.monthGauge.Dispose();
                this.monthGauge = null;
            }
            if (this.secGauge != null)
            {
                this.secGauge.Dispose();
                this.secGauge = null;
            }
            if (this.timeGauge != null)
            {
                this.timeGauge.Dispose();
                this.timeGauge = null;
            }
            if (this.tempGauge != null)
            {
                this.tempGauge.Dispose();
                this.tempGauge = null;
            }
            timer.Stop();
            timer.Tick -= timer_Tick;
        }

        private void samplelayout_Loaded(object sender, RoutedEventArgs e)
        {
            main_grid.Width = (sender as DigitalGauge).ActualWidth;
            main_grid.Height = (sender as DigitalGauge).ActualHeight;
        }
    }
}
