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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleBrowser.Chart3D
{
    public sealed partial class SemiPie : SampleLayout, IDisposable
    {
        public SemiPie()
        {
            this.InitializeComponent();
        }

        public void Dispose()
        {
            if (this.PieChart != null)
            {
                foreach (var series in this.PieChart.Series)
                {
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                }
            }
            if(this.DataContext is DataViewModel)
                (this.DataContext as DataViewModel).Dispose();
        }
    }
  
    public class DataViewModel : ObservableCollection<DataValues>,IDisposable
    {
        public DataViewModel()
        {
            Add(new DataValues(43, 32));
            Add(new DataValues(20, 34));
            Add(new DataValues(67, 41));
            Add(new DataValues(52, 42));
            Add(new DataValues(71, 48));
            Add(new DataValues(30, 45));
        }

        public void Dispose()
        {
            if (this != null)
                this.Clear();
        }
    }
  
    public class DataValues
    {
        public double Utilization
        { 
            get;
            set;
        }

        public double ResponseTime
        { 
            get; 
            set;
        }

        public DataValues(double utilization, double responseTime)
        {
            Utilization = utilization;
            ResponseTime = responseTime;
        }
    }
}
