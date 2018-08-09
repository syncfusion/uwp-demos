using Common;
using Navigation.TabControl;
using Navigation.TreeNavigator;
using SampleBrowser.GroupBar;
using SampleBrowser.Menu;
using SampleBrowser.ProgressBar;
using SampleBrowser.RadialMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.Navigation
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Category = Categories.Miscellaneous,
                SampleType = SampleType.Featured,
               
                SearchKeys = new string[] { "ProgressBar", "MISCELLANEOUS","Getting","Started" },
                Product = "ProgressBar",
                SampleView = typeof(ProgressBarGettingStartedView).AssemblyQualifiedName,
                Tag = Tags.None

            });

            #if (!WINDOWS_STORE)
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Customization",
                Category = Categories.Miscellaneous,
                SampleType = SampleType.Featured,

                SearchKeys = new string[] { "ProgressBar", "MISCELLANEOUS","Custom" },
                Product = "ProgressBar",
                SampleView = typeof(ProgressBarCustomView).AssemblyQualifiedName,
                Tag = Tags.None

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Progress Types",
                Category = Categories.Miscellaneous,
                SampleType = SampleType.Featured,

                SearchKeys = new string[] { "ProgressBar", "MISCELLANEOUS","Types" },
                Product = "ProgressBar",
                SampleView = typeof(ProgressBarProgressTypesView).AssemblyQualifiedName,
                Tag = Tags.None

            });
     #endif

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Radial Menu",
                Category = Categories.Navigation,
                SampleType = SampleType.Featured,
                SampleCategory = "Overview",
                SearchKeys = new string[] { "Menu", "Navigation" },
                Product = "RadialMenu",
                SampleView = typeof(RadialMenuView).AssemblyQualifiedName,
                Tag = Tags.None

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Radial Slider",
                Category = Categories.Navigation,
                SampleType = SampleType.Featured,
                SearchKeys = new string[] { "Menu", "Navigation" },
                SampleCategory= "Overview",
                Product = "RadialMenu",
                SampleView = typeof(RadialSliderView).AssemblyQualifiedName,
                Tag = Tags.Updated

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Category = Categories.Navigation,
                SampleType = SampleType.Featured,
                SearchKeys = new string[] { "Menu", "Navigation" },
                Product = "Menu",
                SampleView = typeof(MenuView).AssemblyQualifiedName,
                Tag = Tags.None

            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Category = Categories.Navigation,
                SampleType = SampleType.Featured,
                SearchKeys = new string[] { "GroupBar", "Navigation", "Group" },
                Product = "GroupBar",
              
                SampleView = typeof(GroupBarView).AssemblyQualifiedName,
                Tag = Tags.None

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Category = Categories.Miscellaneous,
                SampleType = SampleType.Featured,
                SearchKeys = new string[] { "Grid Splitter", "MISCELLANEOUS","Splitter" },
                Product = "Grid Splitter",
                ProductIcons = "Icons/Splitter.png",
                SampleView = typeof(GridSplitter.GridSplitterDemo).AssemblyQualifiedName,
                Tag = Tags.None

            });

           
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "ArrowTrack",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.ArrowTrackDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Ball",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.BallDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Battery",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.BatteryDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Box",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.BoxDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Bulb",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.BulbDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Delete",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.DeleteDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "DoubleCircle",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.DoubleCircleDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Drop",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.DropDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "ECG",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.ECGDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Flight",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.FlightDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Flower",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.FlowerDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Gear",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.GearDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Globe",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.GlobeDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "GPS",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.GPSDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "HorizontalPulsingBox",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.HorizontalPulsingBoxDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Liquid",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.LiquidDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Pen",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.PenDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Print",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.PrintDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Rainy",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.RainyDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Rectangle",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.RectangleDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Rotation",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.RotationDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "SingleCircle",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.SingleCircleDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "SliceBox",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.SliceBoxDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "SlicedCircle",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.SlicedCircleDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Snow",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.SnowDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Sunny",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.SunnyDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Sunrise",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.SunriseDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Temperature",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.TemperatureDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Thunder",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.ThunderDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Umbrella",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.UmbrellaDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Windmill",
                    Category = Categories.Notification,
                    SampleType = SampleType.Featured,
                    SearchKeys = new string[] { "BusyIndicator", "Notification" },
                    Product = "Busy Indicator",
                    ProductIcons = "Icons/LoadingIndicator.png",
                    SampleView = typeof(Notification.BusyIndicator.WindmillDemo).AssemblyQualifiedName,
                    Tag = Tags.None

                });
            
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Hub Tile",
                Category = Categories.Notification,
                SampleType = SampleType.Featured,
                SearchKeys = new string[] { "HubTile", "Notification" },
                Product = "Hub Tiles",
                ProductIcons = "Icons/Hub Tile.png",
                SampleView = typeof(Notification.HubTile.HubTileView).AssemblyQualifiedName,
                Tag = Tags.None

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Mosaic Tile",
                Category = Categories.Notification,
                SearchKeys = new string[] { "HubTile", "Notification" ,"MosaicTile"},
                SampleType = SampleType.Featured,
                Product = "Hub Tiles",
                ProductIcons = "Icons/Hub Tile.png",
                SampleView = typeof(Notification.HubTile.MosaicTileView).AssemblyQualifiedName,
                Tag = Tags.None
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Pulsing Tile",
                Category = Categories.Notification,
                SampleType = SampleType.Featured,
                Product = "Hub Tiles",
                ProductIcons = "Icons/Hub Tile.png",
                SearchKeys = new string[] { "HubTile", "Notification","PulsingTile" },
                SampleView = typeof(Notification.HubTile.PulsingTileView).AssemblyQualifiedName,
                Tag = Tags.None

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Split Mosaic Tile",
                Category = Categories.Notification,
                SampleType = SampleType.Featured,
                Product = "Hub Tiles",
                ProductIcons = "Icons/Hub Tile.png",
                SearchKeys = new string[] { "HubTile", "Notification" ,"SplitMosaicTile"},
                SampleView = typeof(Notification.HubTile.SplitMosaicTileView).AssemblyQualifiedName,
                Tag = Tags.None

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Category = Categories.Navigation,
                SampleType = SampleType.Featured,
                SearchKeys = new string[] { "Getting","Started", "Navigation","tab" },
                Product = "Tab Control",
                ProductIcons = "Icons/Tab Control.png",
                SampleView = typeof(Tab).AssemblyQualifiedName,
                Tag = Tags.None
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Category = Categories.Navigation,
                SampleType = SampleType.Featured,
                Product = "Tree Navigator",
                ProductIcons = "Icons/Tree Navigator.png",
                SearchKeys = new string[] { "Getting", "Started", "Navigation", "treenavigator" },
                SampleView = typeof(TreeNavigatorView).AssemblyQualifiedName,
                Tag = Tags.None
            });

        }

    }
}
