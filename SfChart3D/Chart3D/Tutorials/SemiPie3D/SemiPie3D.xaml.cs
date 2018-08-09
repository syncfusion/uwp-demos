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
    public sealed partial class SemiPie3D : SampleLayout
    {
        public SemiPie3D()
        {
            this.InitializeComponent();
            psChartAdornmentInfo3D.SegmentLabelContent = LabelContent.Percentage;
            psChartAdornmentInfo3D.SegmentLabelFormat = "##.#";
            psChartAdornmentInfo3D.ShowLabel = true;
            psChartAdornmentInfo3D.ShowConnectorLine = true;
            psChartAdornmentInfo3D.AdornmentsPosition = AdornmentsPosition.Bottom;
            psChartAdornmentInfo3D.UseSeriesPalette = true;
            psChartAdornmentInfo3D.ConnectorHeight = 25;
            psChartAdornmentInfo3D.HorizontalAlignment = HorizontalAlignment.Center;
            psChartAdornmentInfo3D.VerticalAlignment = VerticalAlignment.Center;
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                PieChart.Margin = new Thickness(10);
            }
            else
            {
                PieChart.Margin = new Thickness(70, 20, 75, 25);
            }
        }

        public override void Dispose()
        {
            if (this.grid.DataContext is DataViewModel)
                (this.grid.DataContext as DataViewModel).Dispose();

            if (this.PieChart != null)
            {
                foreach (var series in this.PieChart.Series)
                {
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                }
                this.PieChart = null;
            }

            this.grid.Resources = null;

            base.Dispose();
        }
    }

    public class DataViewModel : ObservableCollection<DataValues>, IDisposable
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
