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

namespace Syncfusion.SampleBrowser.UWP.SfChart3D
{
    public sealed partial class Area3D : SampleLayout
    {
        public Area3D()
        {
            this.InitializeComponent();
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                columnChart.Margin = new Thickness(10);
            }
            else
            {
                columnChart.Margin = new Thickness(70,20,75,25);
            }
        }

        public override void Dispose()
        {

            if (this.grid.DataContext is ViewModel1)
                (this.grid.DataContext as ViewModel1).Dispose();

            if (this.columnChart != null)
            {
                foreach (var series in this.columnChart.Series)
                {
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                }
                this.columnChart = null;
            }

            this.grid.Resources = null;
            base.Dispose();
        }
    }

    public class Model1
    {
        public double Server1
        {
            get;
            set;
        }

        public double Server2
        {
            get;
            set;
        }

        public double Server3
        {
            get;
            set;
        }

        public double ServerLoad
        {
            get;
            set;
        }

    }

    public class ViewModel1 : IDisposable
    {
        public ViewModel1()
        {
            this.Performance = new ObservableCollection<Model1>();
            Performance.Add(new Model1() { ServerLoad = 5, Server1 = 13, Server2 = 10, Server3 = 29 });
            Performance.Add(new Model1() { ServerLoad = 10, Server1 = 30, Server2 = 14, Server3 = 15 });
            Performance.Add(new Model1() { ServerLoad = 15, Server1 = 33, Server2 = 19, Server3 = 18 });
            Performance.Add(new Model1() { ServerLoad = 20, Server1 = 15, Server2 = 10, Server3 = 33 });
            Performance.Add(new Model1() { ServerLoad = 25, Server1 = 25, Server2 = 15, Server3 = 19 });
            Performance.Add(new Model1() { ServerLoad = 30, Server1 = 20, Server2 = 13, Server3 = 26 });
            Performance.Add(new Model1() { ServerLoad = 35, Server1 = 23, Server2 = 17, Server3 = 28 });
            Performance.Add(new Model1() { ServerLoad = 40, Server1 = 20, Server2 = 13, Server3 = 25 });
            Performance.Add(new Model1() { ServerLoad = 45, Server1 = 23, Server2 = 13, Server3 = 26 });
        }

        public ObservableCollection<Model1> Performance
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.Performance != null)
                this.Performance.Clear();
        }
    }
}
