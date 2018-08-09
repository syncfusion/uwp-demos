#region Copyright Syncfusion Inc. 2001-2017.
// Copyright Syncfusion Inc. 2001-2017. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Windows.Foundation.Metadata;

namespace Syncfusion.SampleBrowser.UWP.XlsIO
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            CollectSampleView();
            CollectShowcaseViews();
        }

        public void CollectSampleView()
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
#if !STORE_SB
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Create Sheet",
                    SampleCategory = "Getting Started",
                    ProductIcons = "Icons/xlsio.png",
                    Product = "XlsIO", Category = Categories.FileFormat,
                    SearchKeys = new string[] { "XlsIO", "create sheet" },
					SampleView = typeof(EssentialXlsIO.CreateSpreadsheet).AssemblyQualifiedName,
					Tag = Tags.None					
				});
		SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Find and Replace",
					SampleCategory = "Getting Started",
					Product = "XlsIO", Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "find", "replace" },
                    DesktopImage = "ms-appx:///Images/replace.jpg",
                    MobileImage = "ms-appx:///Images/replaceMobile.jpg",
            Description = "This sample illustrates how to find and replace data in a worksheet. ",
            SampleView = typeof(EssentialXlsIO.FindAndReplace).AssemblyQualifiedName,
					Tag = Tags.None					
				});
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Format Cells",
					SampleCategory = "Formatting",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "format cells", "style" },
					SampleView = typeof(EssentialXlsIO.FormattingCells).AssemblyQualifiedName,
					Tag = Tags.None					
				});
#endif

				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Formula",
					SampleCategory = "Data Management",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "formula", "calculation" },
					SampleView = typeof(EssentialXlsIO.Formulas).AssemblyQualifiedName,
					Tag = Tags.None					
				});
#if !STORE_SB
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Table Formula",
					SampleCategory = "Business Intelligence",
					Product = "XlsIO",Category = Categories.FileFormat,
                    Description = "This sample illutrates applying the table formula in a worksheet. ",
                    SearchKeys = new string[] { "XlsIO", "table formula", "table" },
                    DesktopImage = "ms-appx:///Images/table.jpg",
                    MobileImage = "ms-appx:///Images/tableMobile.jpg",
                    SampleView = typeof(EssentialXlsIO.TableFormula).AssemblyQualifiedName,
					Tag = Tags.None						
				});
#endif
                SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Range Manipulation",
					SampleCategory = "Sheet Management",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "range manipulation", "rows" , "columns" },
					SampleView = typeof(EssentialXlsIO.RangeManipulation).AssemblyQualifiedName,
					Tag = Tags.None					
				});
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Chart",
					SampleCategory = "Charts",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "Chart" },
					SampleView = typeof(EssentialXlsIO.Chart).AssemblyQualifiedName,
					Tag = Tags.None					
				});
#if !STORE_SB
   				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Chart Worksheet",
					SampleCategory = "Charts",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "chart worksheet" },
					SampleView = typeof(EssentialXlsIO.ChartWorksheet).AssemblyQualifiedName,
					Tag = Tags.None					
				});
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Sparklines",
					SampleCategory = "Charts",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "sparklines" },
					SampleView = typeof(EssentialXlsIO.Sparklines).AssemblyQualifiedName,
					Tag = Tags.None					
				});
                SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Data Sort",
					SampleCategory = "Data Management",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "sort" },
					SampleView = typeof(EssentialXlsIO.DataSort).AssemblyQualifiedName,
					Tag = Tags.None					
				});
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Filters",
					SampleCategory = "Data Management",
					Product = "XlsIO",Category = Categories.FileFormat,
                    Description = "This sample demonstrates applying filters in a worksheet.",
					SearchKeys = new string[] { "XlsIO", "filters" },
                    DesktopImage = "ms-appx:///Images/filter.jpg",
                    MobileImage = "ms-appx:///Images/filterMobile.jpg",
                    SampleView = typeof(EssentialXlsIO.Filters).AssemblyQualifiedName,
					Tag =  Tags.None					
				});
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Box and Whisker",
                    SampleCategory = "Charts",
                    Product = "XlsIO",
                    Category = Categories.FileFormat,
                    Description = "This sample showcases the creation of Box and Whisker chart using XlsIO",
                    SearchKeys = new string[] { "XlsIO", "Charts", "Box and Whisker" },
                    DesktopImage = "ms-appx:///Images/BoxAndWhisker.jpg",
                    MobileImage = "ms-appx:///Images/BoxAndWhiskerMobile.jpg",
                    SampleView = typeof(EssentialXlsIO.BoxAndWhisker).AssemblyQualifiedName,
                    Tag = Tags.None
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Funnel",
                    SampleCategory = "Charts",
                    Product = "XlsIO",
                    Category = Categories.FileFormat,
                    Description = "This sample showcases the creation of Funnel chart using XlsIO",
                    SearchKeys = new string[] { "XlsIO", "Charts", "Funnel Chart" },
                    DesktopImage = "ms-appx:///Images/Funnel.jpg",
                    MobileImage = "ms-appx:///Images/FunnelMobile.jpg",
                    SampleView = typeof(EssentialXlsIO.FunnelChart).AssemblyQualifiedName,
                    Tag = Tags.None
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Sunburst",
                    SampleCategory = "Charts",
                    Product = "XlsIO",
                    Category = Categories.FileFormat,
                    Description = "This sample showcases the creation of Sunburst chart using XlsIO",
                    SearchKeys = new string[] { "XlsIO", "Charts", "Sunburst" },
                    DesktopImage = "ms-appx:///Images/Sunburst.jpg",
                    MobileImage = "ms-appx:///Images/SunburstMobile.jpg",
                    SampleView = typeof(EssentialXlsIO.Sunburst).AssemblyQualifiedName,
                    Tag = Tags.None
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Treemap",
                    SampleCategory = "Charts",
                    Product = "XlsIO",
                    Category = Categories.FileFormat,
                    Description = "This sample showcases the creation of Treemap chart using XlsIO",
                    SearchKeys = new string[] { "XlsIO", "Charts", "Treemap" },
                    DesktopImage = "ms-appx:///Images/Treemap.jpg",
                    MobileImage = "ms-appx:///Images/TreemapMobile.jpg",
                    SampleView = typeof(EssentialXlsIO.Treemap).AssemblyQualifiedName,
                    Tag = Tags.None
                });               
                //SampleHelper.SampleViews.Add(new SampleInfo() 
                //{
                //	Header = "Data Validation",
                //	SampleCategory = "Data Management",
                //	Product = "XlsIO",Category = Categories.FileFormat,
                //	SearchKeys = new string[] { "XlsIO", "data validation" },
                //	SampleView = typeof(EssentialXlsIO.DataValidation).AssemblyQualifiedName,
                //	Tag = Tags.None					
                //});
                SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Import XML",
					SampleCategory = "Data Binding",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "xml", "import" },
					SampleView = typeof(EssentialXlsIO.ImportXML).AssemblyQualifiedName,
					Tag = Tags.None					
				});
#endif
                SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Business Object",
					SampleCategory = "Data Binding",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "business object", "import" },
					SampleView = typeof(EssentialXlsIO.ImportBusinessObject).AssemblyQualifiedName,
					Tag = Tags.None
                });
                SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Template Marker",
					SampleCategory = "Data Binding",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "template marker" },
					SampleView = typeof(EssentialXlsIO.TemplateMarker).AssemblyQualifiedName,
					Tag = Tags.None					
				});
#if !STORE_SB
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Conditional Format",
					SampleCategory = "Formatting",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "conditional format" },
					SampleView = typeof(EssentialXlsIO.CondFormat).AssemblyQualifiedName,
					Tag = Tags.None					
				});
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Tables",
					SampleCategory = "Business Intelligence",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "table" },
					SampleView = typeof(EssentialXlsIO.Tables).AssemblyQualifiedName,
					Tag = Tags.None					
				});
                //SampleHelper.SampleViews.Add(new SampleInfo() 
				//{
				//	Header = "Pivot Table",
				//	SampleCategory = "Business Intelligence",
				//	Product = "XlsIO",Category = Categories.FileFormat,
				//	SearchKeys = new string[] { "XlsIO", "pivot table" },
				//	SampleView = typeof(EssentialXlsIO.PivotTable).AssemblyQualifiedName,
				//	Tag = Tags.None					
				//});
				//SampleHelper.SampleViews.Add(new SampleInfo() 
				//{
				//	Header = "Pivot Chart",
				//	SampleCategory = "Business Intelligence",
				//	Product = "XlsIO",Category = Categories.FileFormat,
				//	SearchKeys = new string[] { "XlsIO", "pivot chart" },
				//	SampleView = typeof(EssentialXlsIO.PivotChart).AssemblyQualifiedName,
				//	Tag = Tags.None					
				//});
				//SampleHelper.SampleViews.Add(new SampleInfo() 
				//{
				//	Header = "AutoShapes",
				//	SampleCategory = "Shapes",
				//	Product = "XlsIO",Category = Categories.FileFormat,
				//	SearchKeys = new string[] { "XlsIO", "autoshape" },
				//	SampleView = typeof(EssentialXlsIO.AutoShapes).AssemblyQualifiedName,
				//	Tag = Tags.None					
				//});
#endif
                SampleHelper.SetTagsForProduct("XlsIO", Tags.Updated);
            }
            else
            {
#if !STORE_SB
                SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Create Sheet",
					SampleCategory = "Getting Started",
                    ProductIcons = "Icons/xlsio.png",
                    Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "create sheet" },
					SampleView = typeof(EssentialXlsIO.CreateSpreadsheet).AssemblyQualifiedName,
					Tag = Tags.None					
				});
		SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Find and Replace",
					SampleCategory = "Getting Started",
					Product = "XlsIO", Category = Categories.FileFormat,
                    Description = "This sample illustrates how to find and replace data in a worksheet. ",
					SearchKeys = new string[] { "XlsIO", "find", "replace" },
                    DesktopImage = "ms-appx:///Images/replace.jpg",
                    MobileImage = "ms-appx:///Images/replaceMobile.jpg",
                      SampleView = typeof(EssentialXlsIO.FindAndReplace).AssemblyQualifiedName,
					Tag = Tags.None						
				});
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Format Cells",
					SampleCategory = "Formatting",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "format cells", "style" },
					SampleView = typeof(EssentialXlsIO.FormattingCells).AssemblyQualifiedName,
					Tag = Tags.None					
				});
#endif

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Formula",
                    SampleCategory = "Data Management",
                    Product = "XlsIO",Category = Categories.FileFormat,
                    SearchKeys = new string[] { "XlsIO", "formula", "calculation" },
                    SampleView = typeof(EssentialXlsIO.Formulas).AssemblyQualifiedName,
                    Tag = Tags.None
                });
#if !STORE_SB
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Table Formula",
					SampleCategory = "Business Intelligence",
					Product = "XlsIO",Category = Categories.FileFormat,
                    Description = "This sample illutrates applying the table formula in a worksheet. ",
                    DesktopImage = "ms-appx:///Images/table.jpg",
                    MobileImage = "ms-appx:///Images/tableMobile.jpg",
                    SearchKeys = new string[] { "XlsIO", "table formula", "table" },
					SampleView = typeof(EssentialXlsIO.TableFormula).AssemblyQualifiedName,
					Tag = Tags.None						
				});
#endif
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Range Manipulation",
                    SampleCategory = "Sheet Management",
                    Product = "XlsIO",Category = Categories.FileFormat,
                    SearchKeys = new string[] { "XlsIO", "range manipulation", "rows", "columns" },
                    SampleView = typeof(EssentialXlsIO.RangeManipulation).AssemblyQualifiedName,
                    Tag = Tags.None
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Chart",
                    SampleCategory = "Charts",
                    Product = "XlsIO",Category = Categories.FileFormat,
                    SearchKeys = new string[] { "XlsIO", "Chart" },
                    SampleView = typeof(EssentialXlsIO.Chart).AssemblyQualifiedName,
                    Tag = Tags.None
                });
#if !STORE_SB
   				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Chart Worksheet",
					SampleCategory = "Charts",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "chart worksheet" },
					SampleView = typeof(EssentialXlsIO.ChartWorksheet).AssemblyQualifiedName,
					Tag = Tags.None					
				});
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Sparklines",
					SampleCategory = "Charts",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "sparklines" },
					SampleView = typeof(EssentialXlsIO.Sparklines).AssemblyQualifiedName,
					Tag = Tags.None					
				});
                SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Data Sort",
					SampleCategory = "Data Management",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "sort" },
					SampleView = typeof(EssentialXlsIO.DataSort).AssemblyQualifiedName,
					Tag = Tags.None					
				});
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Filters",
					SampleCategory = "Data Management",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "filters" },
                    DesktopImage = "ms-appx:///Images/filter.jpg",
                    MobileImage = "ms-appx:///Images/filterMobile.jpg",
                    Description = "This sample demonstrates applying filters in a worksheet.",
                    SampleView = typeof(EssentialXlsIO.Filters).AssemblyQualifiedName,
					Tag = Tags.None					
				});                
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Box and Whisker",
                    SampleCategory = "Charts",
                    Product = "XlsIO",
                    Category = Categories.FileFormat,
                    SearchKeys = new string[] { "XlsIO", "Box and Whisker", "Charts" },
                    DesktopImage = "ms-appx:///Images/BoxAndWhisker.jpg",
                    MobileImage = "ms-appx:///Images/BoxAndWhiskerMobile.jpg",
                    Description = "This sample showcases the creation of Box and Whisker chart using XlsIO",
                    SampleView = typeof(EssentialXlsIO.BoxAndWhisker).AssemblyQualifiedName,
                    Tag = Tags.None
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Funnel",
                    SampleCategory = "Charts",
                    Product = "XlsIO",
                    Category = Categories.FileFormat,
                    SearchKeys = new string[] { "XlsIO", "Funnel Chart", "Charts" },
                    DesktopImage = "ms-appx:///Images/funnel.jpg",
                    MobileImage = "ms-appx:///Images/FunnelMobile.jpg",
                    Description = "This sample showcases the creation of Funnel chart using XlsIO",
                    SampleView = typeof(EssentialXlsIO.FunnelChart).AssemblyQualifiedName,
                    Tag = Tags.None
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Sunburst",
                    SampleCategory = "Charts",
                    Product = "XlsIO",
                    Category = Categories.FileFormat,
                    SearchKeys = new string[] { "XlsIO", "Sunburst", "Charts" },
                    DesktopImage = "ms-appx:///Images/sunburst.jpg",
                    MobileImage = "ms-appx:///Images/sunburstMobile.jpg",
                    Description = "This sample showcases the creation of Sunburst chart using XlsIO",
                    SampleView = typeof(EssentialXlsIO.Sunburst).AssemblyQualifiedName,
                    Tag = Tags.None
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Treemap",
                    SampleCategory = "Charts",
                    Product = "XlsIO",
                    Category = Categories.FileFormat,
                    SearchKeys = new string[] { "XlsIO", "Treemap", "Charts" },
                    DesktopImage = "ms-appx:///Images/Treemap.jpg",
                    MobileImage = "ms-appx:///Images/TreemapMobile.jpg",
                    Description = "This sample showcases the creation of Treemap chart using XlsIO",
                    SampleView = typeof(EssentialXlsIO.Treemap).AssemblyQualifiedName,
                    Tag = Tags.None
                });
				SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Excel To ODS",
                    SampleCategory = "Export",
                    Product = "XlsIO",
                    Category = Categories.FileFormat,
                    Description = "This sample demonstrates exporting Excel document to OpenDocument Spreadsheet (ODS)",
                    SearchKeys = new string[] { "XlsIO", "ODS", "Excel to ODS" },
                    DesktopImage = "ms-appx:///Images/exceltoods.jpg",
                    MobileImage = "ms-appx:///Images/exceltoodsMobile.jpg",
                    SampleView = typeof(EssentialXlsIO.ExcelToODS).AssemblyQualifiedName,
                    Tag = Tags.None
                });
                SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Data Validation",
					SampleCategory = "Data Management",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "data validation" },
					SampleView = typeof(EssentialXlsIO.DataValidation).AssemblyQualifiedName,
					Tag = Tags.None					
				});
                SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Import XML",
					SampleCategory = "Data Binding",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "xml", "import" },
					SampleView = typeof(EssentialXlsIO.ImportXML).AssemblyQualifiedName,
					Tag = Tags.None					
				});
#endif
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Business Object",
                    SampleCategory = "Data Binding",
                    Product = "XlsIO",Category = Categories.FileFormat,
                    SearchKeys = new string[] { "XlsIO", "business object", "import" },
                    SampleView = typeof(EssentialXlsIO.ImportBusinessObject).AssemblyQualifiedName,
                    Tag = Tags.None
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Template Marker",
                    SampleCategory = "Data Binding",
                    Product = "XlsIO",Category = Categories.FileFormat,
                    SearchKeys = new string[] { "XlsIO", "template marker" },
                    SampleView = typeof(EssentialXlsIO.TemplateMarker).AssemblyQualifiedName,
                    Tag = Tags.None
                });
#if !STORE_SB
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Conditional Format",
					SampleCategory = "Formatting",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "conditional format" },
					SampleView = typeof(EssentialXlsIO.CondFormat).AssemblyQualifiedName,
					Tag = Tags.None					
				});
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Tables",
					SampleCategory = "Business Intelligence",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "table" },
					SampleView = typeof(EssentialXlsIO.Tables).AssemblyQualifiedName,
					Tag = Tags.None					
				});
                SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Pivot Table",
					SampleCategory = "Business Intelligence",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "pivot table" },
					SampleView = typeof(EssentialXlsIO.PivotTable).AssemblyQualifiedName,
					Tag = Tags.None					
				});
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "Pivot Chart",
					SampleCategory = "Business Intelligence",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "pivot chart" },
					SampleView = typeof(EssentialXlsIO.PivotChart).AssemblyQualifiedName,
					Tag = Tags.None					
				});
				SampleHelper.SampleViews.Add(new SampleInfo() 
				{
					Header = "AutoShapes",
					SampleCategory = "Shapes",
					Product = "XlsIO",Category = Categories.FileFormat,
					SearchKeys = new string[] { "XlsIO", "autoshape" },
					SampleView = typeof(EssentialXlsIO.AutoShapes).AssemblyQualifiedName,
					Tag = Tags.None					
				});
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Group Shape",
                    SampleCategory = "Shapes",
                    Product = "XlsIO",
                    Category = Categories.FileFormat,
                    SearchKeys = new string[] { "XlsIO", "group shape" },
                    SampleView = typeof(EssentialXlsIO.GroupShapes).AssemblyQualifiedName,
                    Tag = Tags.New
                });
#endif
                SampleHelper.SetTagsForProduct("XlsIO", Tags.Updated);
            }
        }

        public void CollectShowcaseViews()
        {
            
        }
    }
}
