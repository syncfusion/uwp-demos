#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace Syncfusion.SampleBrowser.UWP.PivotChart
{
    using Common;
    /// <summary>
    /// This class contains the information about how the samples are needs to be configured with SampleBrowser.
    /// </summary>
    public class SamplesConfiguration
    {
        /// <summary>
        /// Initializes the new instance of <see cref="SamplesConfiguration">SamplesConfiguration.</see>/>
        /// </summary>
        public SamplesConfiguration()
        {
            this.CollectSampleView();
        }
        /// <summary>
        /// Method is used to get the collection of samples are needs to included in SampleBrowser.
        /// </summary>
        public void CollectSampleView()
        {
            SampleHelper.SampleViews.Add(new SampleInfo
            {
                SampleView = typeof(BI.PivotChart.Relational).AssemblyQualifiedName,
                Header = DeviceFamily.GetDeviceFamily() == Devices.Desktop ? "Getting Started" : "Getting Started (Relational)",
                Tag = Tags.None,
                Category = Categories.BusinessIntelligence,
                Product = "PivotChart",
                ProductIcons = "Icons/PivotChart.png",
                HasOptions = false,
                SearchKeys = new[] { "PivotChart", "Chart", "GettingStarted" },
                Description = "This sample showcases the PivotChart control bound to Relational data source along with drill down operations",
                SampleCategory = "Relational"
            });

            SampleHelper.SampleViews.Add(new SampleInfo
            {
                SampleView = typeof(BI.PivotChart.Serialization).AssemblyQualifiedName,
                Header = "Serialization",
                Tag = Tags.None,
                Category = Categories.BusinessIntelligence,
                Product = "PivotChart",               
                HasOptions = false,
                SearchKeys = new[] { "PivotChart", "Chart", "Serialization" },
                Description = "This sample illustrates the serialization support available in the SfPivotChart control.",
                SampleCategory = "Relational"
            });

            SampleHelper.SampleViews.Add(new SampleInfo
            {
                SampleView = typeof(BI.PivotChart.GettingStarted).AssemblyQualifiedName,
                Header = DeviceFamily.GetDeviceFamily() == Devices.Desktop ? "Getting Started" : "Getting Started (OLAP)",
                Tag = Tags.None,
                Category = Categories.BusinessIntelligence,
                Product = "PivotChart",
                ProductIcons = "Icons/PivotChart.png",
                HasOptions = false,
                SearchKeys = new[] { "PivotChart", "Chart", "GettingStarted" },
                SampleCategory = "OLAP"
            });

            SampleHelper.SampleViews.Add(new SampleInfo
            {
                SampleView = typeof(BI.PivotChart.KPI).AssemblyQualifiedName,
                Header = "KPI",
                Tag = Tags.None,
                Category = Categories.BusinessIntelligence,
                Product = "PivotChart",
                HasOptions = false,
                SearchKeys = new[] { "PivotChart", "Chart", "KPI" },
                SampleCategory = "OLAP"
            });

            SampleHelper.SampleViews.Add(new SampleInfo
            {
                SampleView = typeof(BI.PivotChart.ChartTypes).AssemblyQualifiedName,
                Header = "Chart Types",
                Tag = Tags.None,
                Category = Categories.BusinessIntelligence,
                Product = "PivotChart",
                HasOptions = true,
                SearchKeys = new[] { "PivotChart", "Chart", "ChartTypes" },
                SampleCategory = "Common"
            });

            SampleHelper.SampleViews.Add(new SampleInfo
            {
                SampleView = typeof(BI.PivotChart.LegendCustomization).AssemblyQualifiedName,
                Header = "Legend Customization",
                Tag = Tags.None,
                Category = Categories.BusinessIntelligence,
                Product = "PivotChart",
                HasOptions = true,
                SearchKeys = new[] { "PivotChart", "Chart", "Legend", "Customization" },
                SampleCategory = "Common"
            });

            SampleHelper.SampleViews.Add(new SampleInfo
            {
                SampleView = typeof(BI.PivotChart.Adornments).AssemblyQualifiedName,
                Header = "Adornments",
                Tag = Tags.None,
                Category = Categories.BusinessIntelligence,
                Product = "PivotChart",
                HasOptions = false,
                SearchKeys = new[] { "PivotChart", "Chart", "Adornments" },
                SampleCategory = "Common"
            });

            SampleHelper.SampleViews.Add(new SampleInfo
            {
                SampleView = typeof(BI.PivotChart.Annotations).AssemblyQualifiedName,
                Header = "Annotations",
                Tag = Tags.None,
                Category = Categories.BusinessIntelligence,
                Product = "PivotChart",
                HasOptions = false,
                SearchKeys = new[] { "PivotChart", "Chart", "Annotations" },
                SampleCategory = "Common"
            });

            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SampleHelper.SampleViews.Add(new SampleInfo
                {
                    SampleView = typeof(BI.PivotChart.Localization).AssemblyQualifiedName,
                    Header = "Localization",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotChart",
                    HasOptions = false,
                    Description = "This sample showcases the localization applied for SfPivotChart that supports specific culture",
                    SearchKeys = new[] { "PivotChart", "Chart", "Localization" },
                    SampleCategory = "Common"
                });
            }

            SampleHelper.SetTagsForProduct("PivotChart", Tags.None);
        }
    }
}