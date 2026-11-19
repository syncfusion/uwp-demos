using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using HeatMap;

namespace Syncfusion.SampleBrowser.UWP.HeatMap
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {


            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "GettingStarted",
                Category = Categories.DataVisualization,
                Product = "HeatMap",
                SearchKeys = new string[] { "HeatMap", "Basic" },
                SampleView = typeof(GettingStarted1).AssemblyQualifiedName,
                Tag = Tags.None,
                ProductIcons = "ms-appx:///Assets/HeatMap.png",
            });
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "ItemsMapping",
                    Category = Categories.DataVisualization,
                    Product = "HeatMap",
                    SearchKeys = new string[] { "HeatMap", "Basic", "ItemsMapping" },
                    SampleView = typeof(ItemsMapping).AssemblyQualifiedName,
                    Tag = Tags.None,

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Legend",
                    Category = Categories.DataVisualization,
                    Product = "HeatMap",
                    SearchKeys = new string[] { "HeatMap", "Basic", "Legend" },
                    SampleView = typeof(Legend).AssemblyQualifiedName,
                    Tag = Tags.None,
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Virtualization",
                    Category = Categories.DataVisualization,
                    Product = "HeatMap",
                    SearchKeys = new string[] { "HeatMap", "Basic", "Virtualization" },
                    SampleView = typeof(Virtualization).AssemblyQualifiedName,
                    Tag = Tags.None,
                });
            }

            else if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "TabelMapping",
                    Category = Categories.DataVisualization,
                    Product = "HeatMap",
                    SearchKeys = new string[] { "HeatMap", "Basic", "TabelMapping" },
                    SampleView = typeof(ItemsMapping_Mobile).AssemblyQualifiedName,
                    Tag = Tags.None,

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Legend",
                    Category = Categories.DataVisualization,
                    Product = "HeatMap",
                    SearchKeys = new string[] { "HeatMap", "Basic", "Legend" },
                    SampleView = typeof(Legend_Mobile).AssemblyQualifiedName,
                    Tag = Tags.None,
                });
            }
            
        }
    }
}
