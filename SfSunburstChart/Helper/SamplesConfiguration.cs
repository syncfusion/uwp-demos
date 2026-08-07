using Common;

namespace Syncfusion.SampleBrowser.UWP.SfSunburstChart
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            #region SampleViews

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(GettingStarted).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Sunburst Chart",
                Header = "Getting Started",
                SampleCategory = "SunburstChart",
                Tag = Tags.None,
                HasOptions = true,
                SearchKeys = new string[] { "g", "get", "start", "s", "sun", "sunburst", "sample" },
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Animation).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Sunburst Chart",
                Header = "Animation",
                SampleCategory = "SunburstChart",
                Tag = Tags.None,
                HasOptions = true,
                SearchKeys = new string[] { "a", "ani", "animate", "s", "sun", "sunburst", "sample" },
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Selection).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Sunburst Chart",
                Header = "Selection",
                SampleCategory = "SunburstChart",
                Tag = Tags.None,
                HasOptions = true,
                SearchKeys = new string[] { "s", "sel", "select", "selectable", "sun", "sunburst", "sample" },
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Zoom).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Sunburst Chart",
                Header = "Zoom",
                SampleCategory = "SunburstChart",
                Tag = Tags.None,
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSunburstChart/Assets/Zoom.png",
                MobileImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSunburstChart/Assets/GettingStarted.png",
                HasOptions = true,
                SearchKeys = new string[] { "z", "zoo", "zoom", "sun", "sunburst", "sample" },
                Description = "This sample demonstrates interactive zooming functionality in sunburst chart."
            });

            #endregion

            #region Product Tags

            SampleHelper.SetTagsForProduct("Sunburst Chart", Tags.None);

            #endregion
        }
    }
}
