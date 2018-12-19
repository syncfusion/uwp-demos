#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace Syncfusion.SampleBrowser.UWP.PivotGauge
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
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SampleHelper.SampleViews.Add(new SampleInfo
                {
                    SampleView = typeof(BI.PivotGauge.Relational).AssemblyQualifiedName,
                    Header = "Getting Started",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotGauge",
                    ProductIcons = "Icons/PivotGauge.png",
                    HasOptions = false,
                    SearchKeys = new[] { "PivotGauge", "Gauge", "GettingStarted", "Relational" },
                    SampleCategory = "Relational",
                    Description = "This sample showcases the PivotGauge control bound to Relational data source",
                    
                });
                SampleHelper.SampleViews.Add(new SampleInfo
                {
                    SampleView = typeof(BI.PivotGauge.GettingStarted).AssemblyQualifiedName,
                    Header = "Getting Started",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotGauge",
                    ProductIcons = "Icons/PivotGauge.png",
                    HasOptions = false,
                    SearchKeys = new[] { "PivotGauge", "Gauge", "Getting Started", "OLAP" },
                    SampleCategory = "OLAP"
                });
            }
            else
            {
                SampleHelper.SampleViews.Add(new SampleInfo
                {
                    SampleView = typeof(BI.PivotGauge.Relational).AssemblyQualifiedName,
                    Header = "Getting Started (Relational)",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotGauge",
                    ProductIcons = "Icons/PivotGauge.png",
                    HasOptions = false,
                    SearchKeys = new[] { "PivotGauge", "Gauge", "GettingStarted", "Relational" },
                    SampleCategory = "Relational",
                    Description = "This sample showcases the PivotGauge control bound to Relational data source",
                    
                });
                SampleHelper.SampleViews.Add(new SampleInfo
                {
                    SampleView = typeof(BI.PivotGauge.GettingStarted).AssemblyQualifiedName,
                    Header = "Getting Started (OLAP)",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotGauge",
                    ProductIcons = "Icons/PivotGauge.png",
                    HasOptions = false,
                    SearchKeys = new[] { "PivotGauge", "Gauge", "Getting Started", "OLAP" },
                    SampleCategory = "OLAP"
                });
            }
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SampleHelper.SampleViews.Add(new SampleInfo
                {
                    SampleView = typeof(BI.PivotGauge.Localization).AssemblyQualifiedName,
                    Header = "Localization",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotGauge",
                    ProductIcons = "Icons/PivotGauge.png",
                    HasOptions = false,
                    SearchKeys = new[] { "PivotGauge", "Gauge", "Localization" },
                    SampleCategory = "Common",
                    Description = "This sample showcases the localization applied for SfPivotGauge that supports specific culture",
                    
                });
            }
			
			SampleHelper.SetTagsForProduct("PivotGauge", Tags.None);
        }
    }
}
