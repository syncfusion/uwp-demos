using Common;
using Syncfusion.UI.Xaml.Gauges;
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
    public sealed partial class CircularGauge : SampleLayout,IDisposable
    {

        public CircularGauge()
        {
            InitializeComponent();
            this.cmb_RangePosition.ItemsSource = Enum.GetValues(typeof(RangePosition));
            this.cmb_TickPosition.ItemsSource = Enum.GetValues(typeof(TickPosition));
            this.cmb_LabelPosition.ItemsSource = Enum.GetValues(typeof(LabelPosition));

        }

        private void samplelayout_Loaded(object sender, RoutedEventArgs e)
        {
            grid.Width = samplelayout.ActualWidth;
            grid.Height = samplelayout.ActualHeight;
        }

        public override void Dispose()
        {
            this.gauge1.Dispose();
            this.gauge3.Dispose();
            this.gauge1 = null;
            this.gauge3 = null;
        }
    }
}
