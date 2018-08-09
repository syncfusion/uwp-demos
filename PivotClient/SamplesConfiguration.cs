#region Copyright
//  Copyright (c) Syncfusion Inc. 2001 - 2018. All rights reserved.
//  Use of this code is subject to the terms of our license.
//  A copy of the current license can be obtained at any time by e-mailing
//  licensing@syncfusion.com. Re-distribution in any form is strictly
//  prohibited. Any infringement will be prosecuted under applicable laws. 
// </copyright>
#endregion

using Common;
using Windows.System.Profile;

namespace Syncfusion.SampleBrowser.UWP.PivotClient
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            this.CollectSampleView();
        }

        public void CollectSampleView()
        {
            SampleHelper.SetTagsForProduct("PivotClient", Tags.Preview);

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BI.PivotClient.Relational).AssemblyQualifiedName,
                Header = "Getting Started",
                SampleCategory = "Relational",
                Tag = Tags.None,
                Category = Categories.BusinessIntelligence,
                Product = "PivotClient",
                HasOptions = false,
                DesktopImage = "ms-appx:///WhatsNewImage/Relational.PNG",
                SearchKeys = new[] { "PivotClient", "Client", "GettingStarted" },
                Description = "This sample visualizes relational data through PivotChart and PivotGrid embedded in PivotClient control.",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BI.PivotClient.GettingStarted).AssemblyQualifiedName,
                Header = "Getting Started",
                SampleCategory = "OLAP",
                Tag = Tags.None,
                Category = Categories.BusinessIntelligence,
                Product = "PivotClient",
                HasOptions = false,
                SearchKeys = new[] { "PivotClient", "Client", "GettingStarted" },
                Description = "This sample visualize the OLAP data through PivotChart and PivotGrid embedded in PivotClient control.",
            });

            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(BI.PivotClient.PivotClientConfiguration).AssemblyQualifiedName,
                    Header = "Configuration",
                    SampleCategory = "Common",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotClient",
                    HasOptions = true,
                    SearchKeys = new[] { "Pivot Client", "Client", "PivotClient Configuration", "Configuration" },
                    Description = "This sample configures several properties which are exposed to handle OLAP Reports, filtering, sorting, VirtualKPI, Calculated Members and element visibility.",
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(BI.PivotClient.CalculatedMembers).AssemblyQualifiedName,
                    Header = "Calculated Members",
                    SampleCategory = "OLAP",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotClient",
                    HasOptions = false,
                    SearchKeys = new[] { "PivotClient", "Client", "CalculatedMembers", "Calculated", "Members" },
                    Description = "This sample showcases to create and manage the Calculated Measures and Members in PivotClient.",
                });

                SampleHelper.SampleViews.Add(new SampleInfo
                {
                    SampleView = typeof(BI.PivotClient.MDXQuery).AssemblyQualifiedName,
                    Header = "MDX Query",
                    SampleCategory = "OLAP",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotClient",
                    HasOptions = true,
                    SearchKeys = new[] { "PivotClient", "Client", "MDX Query" },
                    Description = "This sample showcases the ability to load OLAP data into PivotClient control with drill up and drill down functionalities in PivotGrid and PivotChart by passing different MDX queries to OlapDataManager.",
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(BI.PivotClient.VirtualKPI).AssemblyQualifiedName,
                    Header = "Virtual KPI",
                    SampleCategory = "OLAP",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotClient",
                    HasOptions = false,
                    SearchKeys = new[] { "PivotClient", "Client", "VirtualKPI" },
                    Description = "This sample showcases to create and manage Virtual KPI elements in PivotClient.",
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(BI.PivotClient.DeferUpdate).AssemblyQualifiedName,
                    Header = "Defer Update",
                    SampleCategory = "OLAP",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotClient",
                    HasOptions = true,
                    SearchKeys = new[] { "PivotClient", "Client", "AutoExecute", "DeferLayoutUpdate" },
                    Description = "This sample showcases on-demand execution option in the PivotClient control.",
                    DesktopImage = "ms-appx:///WhatsNewImage/AutoExecute.png",
                    MobileImage = "ms-appx:///WhatsNewImage/AutoExecute.png"
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(BI.PivotClient.AdvancedFiltering).AssemblyQualifiedName,
                    Header = "Advanced Filtering",
                    SampleCategory = "OLAP",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotClient",
                    HasOptions = false,
                    SearchKeys = new[] { "PivotClient", "Client", "Filtering", "Advanced Filtering" },
                    Description = "This sample showcases the label and value filtering option in the PivotClient control.",
                    DesktopImage = "ms-appx:///WhatsNewImage/AdvancedFiltering.png",
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(BI.PivotClient.PivotClientCustomization).AssemblyQualifiedName,
                    Header = "Customization",
                    SampleCategory = "Common",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotClient",
                    HasOptions = true,
                    SearchKeys = new[] { "PivotClient", "Client", "PivotClient Customization", "Customization" },
                    Description = "This sample showcases the appearance customization of PivotChart and PivotGrid embedded in PivotClient control.",
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(BI.PivotClient.Paging).AssemblyQualifiedName,
                    Header = "Paging",
                    SampleCategory = "OLAP",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotClient",
                    HasOptions = false,
                    SearchKeys = new[] { "PivotClient", "Client", "Paging Demo" },
                    Description = "This sample showcases the paging feature which segregates large cellset into small portions to get the control rendered in an efficient way.",
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(BI.PivotClient.Localization).AssemblyQualifiedName,
                    Header = "Localization",
                    SampleCategory = "Common",
                    Tag = Tags.None,
                    Category = Categories.BusinessIntelligence,
                    Product = "PivotClient",
                    HasOptions = true,
                    SearchKeys = new[] { "PivotClient", "Client", "PivotClient Localization", "Localization" },
                    Description = "This sample showcases the localization and RTL support in PivotClient control.",
                });
            }
        }
    }
}