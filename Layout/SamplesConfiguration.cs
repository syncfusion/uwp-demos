using Common;
using Layout.Accordion;
using Layout.Carousel;
using Layout.DockingManager;
using Layout.TileView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.System.Profile;

namespace Syncfusion.SampleBrowser.UWP.Layout
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Visual Studio",
                Category = Categories.Layout,
                SampleType = SampleType.Featured,
                Product = "Docking Manager",
                ProductIcons = "Icons/Ribbon.png",
                MobileImage = "ms-appx:///WhatsNewImage/dockingmanagermobile.png",
                DesktopImage = "ms-appx:///WhatsNewImage/dockingmanager.png",                                
                Description = "This sample demonstrates how the DockingManager control can be used to create Visual Studio style docking windows.",
                SearchKeys = new string[] { "Visual Studio", "Docking", "Docking Manager" },
                SampleView = typeof(DockingManagerVisualStudio).AssemblyQualifiedName,
                Tag = Tags.None
            });
#if (!WINDOWS_STORE)
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Serialization",
                Category = Categories.Layout,
                SampleType = SampleType.Featured,
                Product = "Docking Manager",
                ProductIcons = "Icons/Ribbon.png",
                SearchKeys = new string[] { "Docking", "Serialization", "Persistence", "Deserialization" },
                SampleView = typeof(DockingManagerSerialization).AssemblyQualifiedName,
                Tag = Tags.None
            });

            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Localization",
                    Category = Categories.Layout,
                    SampleType = SampleType.Featured,
                    Product = "Docking Manager",
                    ProductIcons = "Icons/Ribbon.png",
                    SearchKeys = new string[] { "Localization", "DockingManager", "Docking", "Docking Manager" },
                    SampleView = typeof(DockingManagerLocalization).AssemblyQualifiedName,
                    Tag = Tags.None
                });

            }
#endif
        

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Category = Categories.Layout,
                SampleType = SampleType.Featured,
                Product = "TileView",
                ProductIcons = "Icons/tile-view.png",
                SearchKeys = new string[] { "TileView", "Started" },
                SampleView = typeof(TileView).AssemblyQualifiedName,
                Tag = Tags.None
            });


            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Category = Categories.Layout,
                SampleType = SampleType.Featured,
                Product = "Carousel",
                ProductIcons = "Icons/Carousel.png",
                SearchKeys = new string[] { "Carousel", "Started" },
                SampleView=typeof(CarouselView).AssemblyQualifiedName,
                Tag=Tags.None
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Category = Categories.Layout,
                SampleType = SampleType.Featured,
                Product = "Accordion",
                ProductIcons = "Icons/Accordion.png",
                SearchKeys = new string[] { "Accordion", "Started" },
                SampleView = typeof(AccordionView).AssemblyQualifiedName,
                Tag = Tags.None
            });

#if (!WINDOWS_STORE)
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "DataBinding",
                Category = Categories.Layout,
                SampleType = SampleType.Featured,
                Product = "Accordion",
                ProductIcons = "Icons/Accordion.png",
                SearchKeys = new string[] { "Accordion", "Data","Binding" },
                SampleView = typeof(DataBindingView).AssemblyQualifiedName,
                Tag = Tags.None
            });
#endif
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Category = Categories.Layout,
                SampleType = SampleType.Featured,
               
                Product = "Ribbon",
                ProductIcons = "Icons/Ribbon.png",
                SearchKeys = new string[] { "Ribbon","Started" },
                SampleView = typeof(SfRibbon.Ribbon.RibbonSamplePage).AssemblyQualifiedName,
                Tag = Tags.None
            });

#if (!WINDOWS_STORE)
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Custom Backstage",
                Category = Categories.Layout,
                SampleType = SampleType.Featured,
               
                Product = "Ribbon",
                ProductIcons = "Icons/Ribbon.png",
                SearchKeys = new string[] { "Ribbon", "Backstage", "Customization" },
                SampleView = typeof(SfRibbon.Ribbon.CustomBackStagePage).AssemblyQualifiedName,
                Tag = Tags.None
            });
#endif
           
        }
    }
}
