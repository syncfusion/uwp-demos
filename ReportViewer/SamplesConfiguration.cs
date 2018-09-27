using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Syncfusion.SampleBrowser.UWP.ReportViewer
{

    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleType = SampleType.Showcase,
                Header = "Sales Order",
                Category = Categories.Reporting,
                DesktopImage = "ms-appx:///ReportViewer/Assets/Sales Order.png",
                MobileImage = "ms-appx:///ReportViewer/Assets/Sales Order mobile.png",
                ProductIcons = "ms-appx:///ReportViewer/Assets/reportviewer.png",
                Description = "This sample demonstrates how the report viewer control can be used for viewing local RDLC files.",
                Product = "ReportViewer",
                SampleView = typeof(SalesOrderView).AssemblyQualifiedName,
                Tag = Tags.None,
            });

#if UWP_STORE
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SalesDashboardView).AssemblyQualifiedName,
                Header = "Sales Dashboard",
                SampleCategory = "Product Showcase",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(IndicatorView).AssemblyQualifiedName,
                Header = "Indicator Demo",
                SampleCategory = "Product Showcase",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(ConditionalParameterView).AssemblyQualifiedName,
                Header = "Conditional Parameter",
                SampleCategory = "Product Showcase",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            //Report Export
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(ReportWriterSamples.ReportExportDemo).AssemblyQualifiedName,
                Header = "Offline Export",
                SampleCategory = "Product Showcase",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                DesktopImage = "ms-appx:///ReportViewer/Assets/Table Summeries.jpg",
                MobileImage = "ms-appx:///ReportViewer/Assets/Table Summeries mobile.jpg",
                Description = "This sample demonstrates how the Report Writer's local exporting mode can be used to export RDLC reports into various file formats.",
                Tag = Tags.None,
            });
#else
            //SSRS Report
            //SampleHelper.SampleViews.Add(new SampleInfo()
            //{
                //SampleView = typeof(SSRSReportDemo).AssemblyQualifiedName,
                //Header = "Territory Sales",
                //SampleCategory = "SSRS Report",
                //Category = Categories.Reporting,
                //Product = "ReportViewer",
                //Tag = Tags.None,
            //});

            ////Product Showcase
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(ProductCatalogView).AssemblyQualifiedName,
                Header = "Product Catalog",
                SampleCategory = "Product Showcase",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(ProductLineSalesView).AssemblyQualifiedName,
                Header = "Product Line Sales",
                SampleCategory = "Product Showcase",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SalesDashboardView).AssemblyQualifiedName,
                Header = "Sales Dashboard",
                SampleCategory = "Product Showcase",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SalesOrderView).AssemblyQualifiedName,
                Header = "Sales Order",
                SampleCategory = "Product Showcase",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(TableFormattingView).AssemblyQualifiedName,
                Header = "Table Formatting",
                SampleCategory = "Product Showcase",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });


            //Report Elements
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(PivotTableView).AssemblyQualifiedName,
                Header = "Pivot Table",
                SampleCategory = "Report Elements",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(TemplateListView).AssemblyQualifiedName,
                Header = "Template List",
                SampleCategory = "Report Elements",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(LineChartView).AssemblyQualifiedName,
                Header = "Line Chart",
                SampleCategory = "Report Elements",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(RadialGaugeView).AssemblyQualifiedName,
                Header = "Radial Gauge",
                SampleCategory = "Report Elements",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(TableSummariesView).AssemblyQualifiedName,
                Header = "Table Summaries",
                SampleCategory = "Report Elements",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(GroupingTableView).AssemblyQualifiedName,
                Header = "Grouping Table",
                SampleCategory = "Report Elements",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(IndicatorView).AssemblyQualifiedName,
                Header = "Indicator Demo",
                SampleCategory = "Report Elements",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(MapView).AssemblyQualifiedName,
                Header = "Map Demo",
                SampleCategory = "Report Elements",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            //Report Parameters

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(QueryParameterView).AssemblyQualifiedName,
                Header = "Query Parameter",
                SampleCategory = "Report Parameters",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(ConditionalParameterView).AssemblyQualifiedName,
                Header = "Conditional Parameter",
                SampleCategory = "Report Parameters",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                Tag = Tags.None,
            });

            //Report Export
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(ReportWriterSamples.ReportExportDemo).AssemblyQualifiedName,
                Header = "Offline Export",
                SampleCategory = "Report Export",
                Category = Categories.Reporting,
                Product = "ReportViewer",
                DesktopImage = "ms-appx:///ReportViewer/Assets/Table Summeries.jpg",
                MobileImage = "ms-appx:///ReportViewer/Assets/Table Summeries mobile.jpg",
                Description = "This sample demonstrates how the Report Writer's local exporting mode can be used to export RDLC reports into various file formats. ",
                Tag = Tags.None,
            });
#endif
            SampleHelper.SetTagsForProduct("ReportViewer", Tags.None);
        }
    }
}
