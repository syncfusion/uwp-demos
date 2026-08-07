using Common;

namespace Syncfusion.SampleBrowser.UWP.SfSmithChart
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
                Product = "Smith Chart",
                Header = "Getting Started",
                SampleCategory = "SmithChart",
                Tag = Tags.None,
                HasOptions = true,
                SearchKeys = new string[] { "g", "get", "start", "s", "smith", "smithchart", "sample" },
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Customization).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Smith Chart",
                Header = "Customization",
                SampleCategory = "SmithChart",
                Tag = Tags.None,
                HasOptions = true,
                SearchKeys = new string[] { "c", "cus", "custom", "s", "smith", "smithchart", "sample" },
            });
            
            #endregion

            #region Product Tags

            SampleHelper.SetTagsForProduct("Smith Chart", Tags.None);

            #endregion
        }
    }
}
