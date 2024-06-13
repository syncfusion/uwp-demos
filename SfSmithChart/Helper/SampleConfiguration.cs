#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
