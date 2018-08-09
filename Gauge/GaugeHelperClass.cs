using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.Gauge
{
    class SamplesConfiguration
    {
        public SamplesConfiguration()
        {

#if STORE_SAMPLE
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(GaugeUWP_Samples.CircularGauge).AssemblyQualifiedName, Product = "Gauge",ProductIcons= "Icons/guage.png", Header = "CircularGauge", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(GaugeUWP_Samples.DigitalGauge).AssemblyQualifiedName, Product = "Gauge",ProductIcons= "Icons/guage.png", Header = "DigitalGauge", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(GaugeUWP_Samples.LinearGauge).AssemblyQualifiedName, Product = "Gauge",ProductIcons= "Icons/guage.png", Header = "LinearGauge", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
#else
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(GaugeUWP_Samples.CircularGauge).AssemblyQualifiedName, Product = "Gauge", ProductIcons= "Icons/guage.png", Header = "CircularGauge", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(GaugeUWP_Samples.DigitalGauge).AssemblyQualifiedName, Product = "Gauge", ProductIcons = "Icons/guage.png", Header = "DigitalGauge", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(GaugeUWP_Samples.LinearGauge).AssemblyQualifiedName, Product = "Gauge", ProductIcons = "Icons/guage.png", Header = "LinearGauge", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(GaugeUWP_Samples.SpeedTrackDemo).AssemblyQualifiedName, SampleType = SampleType.Showcase, Product = "Gauge", ProductIcons = "Icons/guage.png", Header = "CarDashboard", Description = "This sample simulates the radial gauges in a car dashboard", DesktopImage = "ms-appx:///Gauge/Assets/Car-Dashborad.jpg", MobileImage = "ms-appx:///Gauge/Assets/Car-Dashborad-Mobile.jpg" });
#endif

        }
     
    }
}
