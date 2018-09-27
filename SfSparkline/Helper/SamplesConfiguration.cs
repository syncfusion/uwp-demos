using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.SfSparkline
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SparklineDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Sparkline",
                ProductIcons = "Icons/sparkline_chart.png",
                Header = "Getting Started",
                SampleCategory = "Sparkline",
                SearchKeys = new string[] { "sparkline", "spark", "line", "getting", "sample" },
                Tag = Tags.None,
                HasOptions = false
            });
        }
    }
}
