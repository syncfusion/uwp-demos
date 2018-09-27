using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataChart : UserControl, IDisposable
    {
        public DataChart()
        {
            this.InitializeComponent();
            this.Unloaded += DataChart_Unloaded;
            ViewModelz model=new ViewModelz();
            model.Selectedindex=0;
            this.DataContext = model;
        }

        private void DataChart_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            if (this.DataContext != null)
                this.DataContext = null;
            if (this.revenueChart != null)
                this.revenueChart = null;

            this.dataChartGrid.Resources = null;
        }
    }
}
