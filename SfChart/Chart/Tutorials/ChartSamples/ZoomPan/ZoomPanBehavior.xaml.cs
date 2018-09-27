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
using Windows.UI;
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
    public sealed partial class ZoomPanBehavior : SampleLayout
    {
        public ZoomPanBehavior()
        {
            this.InitializeComponent();
        }

        public override void Dispose()
        {
            if ((this.MainGrid.DataContext as LoadDetailViewModel) != null)
                (this.MainGrid.DataContext as LoadDetailViewModel).Dispose();

            if (this.zoomPan != null)
            {
                foreach (var series in this.zoomPan.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.zoomPan = null;
            }

            this.MainGrid.Resources = null;

            base.Dispose();
        }

        private void zoomPan_Loaded(object sender, global::Windows.UI.Xaml.RoutedEventArgs e)
        {
            ChartZoomPanBehavior zoomBehavior = new ChartZoomPanBehavior();
            zoomBehavior.EnablePanning = true;
            zoomBehavior.ZoomMode = UI.Xaml.Charts.ZoomMode.XY;
            zoomBehavior.HorizontalPosition = HorizontalAlignment.Right;
            zoomBehavior.EnableZoomingToolBar = true;
            zoomBehavior.ToolBarBackground = new SolidColorBrush(Colors.LightGray);
            zoomPan.Behaviors.Add(zoomBehavior);

            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                zoomBehavior.EnableSelectionZooming = true;
            }
            else
                zoomBehavior.EnableSelectionZooming = false;
        }
    }

    public class StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return String.Format("${0}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
