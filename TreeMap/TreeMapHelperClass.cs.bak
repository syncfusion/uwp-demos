using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.TreeMap
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
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(TreeMapWinRTSamples.FlatCollectionTreeMap).AssemblyQualifiedName, Product = "TreeMap", ProductIcons= "ms-appx:///Assets/Icons/TreeMap.png", Header = "FlatCollectionTreeMap", Tag = Tags.None, Category = "Data Visualization", HasOptions = false });
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(TreeMapWinRTSamples.HierarchicalCollectionTreeMap).AssemblyQualifiedName,  Product = "TreeMap",ProductIcons= "ms-appx:///Assets/Icons/TreeMap.png", Header = "HierarchicalCollectionTreeMap", Tag = Tags.None, Category = "Data Visualization", HasOptions = false });
            
#else
            if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
            {
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(TreeMapWinRTSamples.FlatCollectionTreeMap).AssemblyQualifiedName, Product = "TreeMap", ProductIcons = "ms-appx:///Assets/Icons/TreeMap.png", Header = "FlatCollectionTreeMap", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
            }

            else if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(TreeMapWinRTSamples.FlatCollectionTreeMap).AssemblyQualifiedName, Product = "TreeMap", ProductIcons = "ms-appx:///Assets/Icons/TreeMap.png", Header = "FlatCollectionTreeMap", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(TreeMapWinRTSamples.HierarchicalCollectionTreeMap).AssemblyQualifiedName, Product = "TreeMap", ProductIcons = "ms-appx:///Assets/Icons/TreeMap.png", Header = "HierarchicalCollectionTreeMap", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
#if (!WINDOWS_STORE)
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(TreeMapWinRTSamples.TreeMapCustomization).AssemblyQualifiedName, Product = "TreeMap", ProductIcons = "ms-appx:///Assets/Icons/TreeMap.png", Header = "TreeMapCustomization", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(TreeMapWinRTSamples.TreeMapDrillDown).AssemblyQualifiedName, Product = "TreeMap", ProductIcons = "ms-appx:///Assets/Icons/TreeMap.png", Header = "TreeMapDrillDown", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
#endif
                }
#endif

            }
        }
}
