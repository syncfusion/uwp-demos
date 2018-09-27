using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.System.Profile;

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            #region SampleViews

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (BasicCharts).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                ProductIcons = "Icons/chart.PNG",
                Header = "Basic Charts",
                SampleCategory = "Charts",
                Tag = Tags.None,                
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (FinancialCharts).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Financial Charts",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (StackedCharts).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Stacked Charts",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (CircularCharts).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Circular Charts",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (PieChart).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Accumulation Charts",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (Performance).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Performance",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (AnnotationDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Annotation",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (FastChartDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Fast Charts",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (StackingGroup).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Stacking Group",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (ErrorBars).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Error Bars",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = true
            });           

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Category = Categories.DataVisualization,
                Header = "Export",
                Product = "Chart",
                SampleCategory = "Charts",
                SampleView = typeof(Export).AssemblyQualifiedName,
                Tag = Tags.None
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Category = Categories.DataVisualization,
                Header = "ScaleBreak",
                Product = "Chart",
                SampleCategory = "Charts",
                SampleView = typeof(ScaleBreak).AssemblyQualifiedName,
                Tag = Tags.None             
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (FlexibleAxisDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Flexible Axis",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Category = Categories.DataVisualization,
                Header = "Multi Level Labels",
                Product = "Chart",
                SampleCategory = "Charts",
                SampleView = typeof(MultiLevelLabels).AssemblyQualifiedName,
                Tag = Tags.None                
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (StriplinesDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Striplines",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (AutoScrollingDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Auto Scrolling",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (VisualDataEditing).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Visual Data Editing",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(ScatterDataEditing).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Scatter Data Editing",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = true
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (CustomTemplate).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Series Template",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = true
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (Palettes).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Palettes",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = true
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (ToolTipDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "ToolTip",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = true
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (WaterMarkDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Watermark",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (TrackBallDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "TrackBall",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (SelectionBehavior).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Selection",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = true
            });
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof (SeriesSelection).AssemblyQualifiedName,
                    Category = Categories.DataVisualization,
                    Product = "Chart",
                    Header = "SeriesSelection",
                    SampleCategory = "Charts",
                    Tag = Tags.None,
                    HasOptions = true
                });
            }
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (CrossHairDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "CrossHair",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (ZoomPanBehavior).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Zoom and Pan",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(StockZooming).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Stock Zooming",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false,
                DesktopImage = "ms-appx:///Assets/StockZooming.png",
                MobileImage = "ms-appx:///Assets/StockZooming.png",
                Description = "This sample demonstrates the stock chart zooming."
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (MultipleLegendsDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Multiple Legends",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = true
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (EmptyValues).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Empty Values",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = true
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (TrendLineDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Trend Lines",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = true
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (TechnicalIndicator).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Indicators",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = true
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof (VerticalChartDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Vertical Chart",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });

            #endregion

            #region ShowcaseViews

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Category = Categories.DataVisualization,
                Header = "Sales Analysis",
                Product = "Sales Analysis",
                SampleType = SampleType.Showcase,
                SampleView = typeof (SalesAnalysisDemo).AssemblyQualifiedName,
                DesktopImage = "ms-appx:///Assets/Sales Analysis.jpg",
                MobileImage = "ms-appx:///Assets/Sales Analysis.jpg",
                Tag = Tags.None,
                Description = "This sample demonstrates analyzing the sales report for a product using Chart, Maps and Gauge controls.",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Category = Categories.DataVisualization,
                Header = "Stock Analysis",
                Product = "Stock Analysis",
                SampleType = SampleType.Showcase,
                SampleView = typeof (StockAnalysis).AssemblyQualifiedName,
                DesktopImage = "ms-appx:///Assets/Stock Analysis.jpg",
                MobileImage = "ms-appx:///Assets/Stock Analysis.jpg",
                Tag = Tags.None,
                Description = "This demo illustrates the use of Chart and Range Navigator for a real world Stock Market application.",
            });

            #endregion

            #region Product Tags

            SampleHelper.SetTagsForProduct("Chart", Tags.None);

            #endregion
        }
    }
}
