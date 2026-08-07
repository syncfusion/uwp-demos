using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.Maps
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            CollectSampleView();
          
        }

        public void CollectSampleView()
        {
#if STORE_SAMPLE
           
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.DataMarkers).AssemblyQualifiedName, Product = "Maps",ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "DataMarkers", Tag = Tags.None, Category = "Data Visualization", HasOptions = false });            
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.ShapeSelection).AssemblyQualifiedName, Product = "Maps",ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "ShapeSelection", Tag = Tags.None, Category = "Data Visualization", HasOptions = true });

#else
            if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
            {
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.DataMarkers).AssemblyQualifiedName, Product = "Maps", ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "DataMarkers", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.ElectionResultMobile).AssemblyQualifiedName, Product = "Maps", ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "ShapeSelection", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
#if (!WINDOWS_STORE)
                
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.BasicMap).AssemblyQualifiedName, Product = "Maps", ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "HeatMap", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.TicketBooking).AssemblyQualifiedName, Product = "Maps", ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "Ticket Booking", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.BubbleVisualization).AssemblyQualifiedName, Product = "Maps", ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "BubbleVisualization", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });                
#endif
               
                
            }
            else if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.DataMarkers).AssemblyQualifiedName, Product = "Maps", ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "DataMarkers", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.ElectionResult).AssemblyQualifiedName, Product = "Maps", ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "ShapeSelection", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });

#if (!WINDOWS_STORE)
             
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.BasicMap).AssemblyQualifiedName, Product = "Maps", ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "HeatMap", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.BubbleVisualization).AssemblyQualifiedName, Product = "Maps", ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "BubbleVisualization", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.MapPoints).AssemblyQualifiedName, Product = "Maps", ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "MapPoints", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(MapControlUWP_Samples.TicketBooking).AssemblyQualifiedName, Product = "Maps", ProductIcons = "ms-appx:///Assets/Icons/maps.png", Header = "Ticket Booking", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
#endif
                
                
            }

#endif 

            }

        }
}
