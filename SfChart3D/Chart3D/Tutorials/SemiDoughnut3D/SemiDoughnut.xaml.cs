using Common;
using Syncfusion.UI.Xaml.Charts;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleBrowser.Chart3D
{
    public sealed partial class SemiDoughnut : SampleLayout, IDisposable
    {
        public SemiDoughnut()
        {
            this.InitializeComponent();
        }

        public void Dispose()
        {
            if (this.DoughnutChart != null)
            {
                foreach (var series in this.DoughnutChart.Series)
                {
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                }
            }
            if(this.DataContext is DataViewModel)
                (this.DataContext as DataViewModel).Dispose();
        }
    }
}
