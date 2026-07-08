using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace Syncfusion.SampleBrowser.UWP.SfCellGrid
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            // Product Showcase
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.ExcelLikeUI).AssemblyQualifiedName,
                Header = "Excel Like UI",
                Product = "Cell Grid",
                ProductIcons = "Icons/CellGrid.png",
                Category = Categories.Grids,
                SearchKeys = new string[] { "Excel", "Showcase" },
                Tag = Tags.None,
                SampleCategory = "Product Showcase"
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.TraderGrid).AssemblyQualifiedName,
                Header = "Trader Grid Test",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Trader", "Showcase" },
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Product Showcase"
            });

            // Cell Types
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.CellTypes).AssemblyQualifiedName,
                Header = "Basic Cell Types",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Basic", "Cell Types" },
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Cell Types"
            });
#if !CELLGRID_STORE
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.CellTemplate).AssemblyQualifiedName,
                Header = "Cell Template",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Template", "Cell" },
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Cell Types",
            });

            // Appearance
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.CellStyle).AssemblyQualifiedName,
                Header = "Cell Styles",
                SearchKeys = new string[] { "Styles", "Cell", "Appearance" },
                Product = "Cell Grid",
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Appearance",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.TextFormat).AssemblyQualifiedName,
                Header = "Text Format",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Format", "Text" },
                Tag = Tags.None,
                Category = Categories.Grids,
                SampleCategory = "Appearance"
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.ConditionalFormatting).AssemblyQualifiedName,
                Header = "Conditional Formatting",
                SearchKeys = new string[] { "Conditional", "Formatting","Appearance" },
                Product = "Cell Grid",
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Appearance"
            });
            if (!ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(CellGridSamples.Cell_Comments).AssemblyQualifiedName,
                    Header = "Cell Comments",
                    SearchKeys = new string[] { "Comments", "Appearance" },
                    Product = "Cell Grid",
                    Category = Categories.Grids,
                    Tag = Tags.None,
                    SampleCategory = "Appearance",
                });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.CoveredCells).AssemblyQualifiedName,
                Header = "Covered Cells",
                SearchKeys = new string[] { "Covered", "Merge", "Appearance", "Cells" },
                Product = "Cell Grid",
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Appearance",
            });

            // Performance
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.Virtualization).AssemblyQualifiedName,
                Header = "Virtual Grid",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Virtual", "Grid", "Performance" },
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Performance",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.ScrollPerformance).AssemblyQualifiedName,
                Header = "Scroll Performance",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Scroll", "Performance" },
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Performance",
            });

            // Interactive Features
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.Selection).AssemblyQualifiedName,
                Header = "Selection",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Selection", "Interactive" },
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Interactive Features",
            });
            if (!ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(CellGridSamples.CopyPaste).AssemblyQualifiedName,
                    Header = "Clipboard Operations",
                    Product = "Cell Grid",
                    SearchKeys = new string[] { "Clipboard", "Interactive" },
                    Category = Categories.Grids,
                    Tag = Tags.None,
                    SampleCategory = "Interactive Features",
                });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.ContextMenu).AssemblyQualifiedName,
                Header = "Context Menu",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Context Menu", "Interactive" },
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Interactive Features",
            });

            // Excel Like Features
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.DataValidation).AssemblyQualifiedName,
                Header = "Data Validation",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Data", "Validation", "Interactive" },
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Excel Like Features",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.Formula).AssemblyQualifiedName,
                Header = "Formula Support",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Formula", "Excel Features" },
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Excel Like Features",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.RowColumnManipulation).AssemblyQualifiedName,
                Header = "Row Column Manipulation",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Row Column", "Excel Features" },
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Excel Like Features",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.FloatingCells).AssemblyQualifiedName,
                Header = "Floating Cells",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Floating", "Excel Features" },
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Excel Like Features",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CellGridSamples.FreezePane).AssemblyQualifiedName,
                Header = "Freeze Panes",
                Product = "Cell Grid",
                SearchKeys = new string[] { "Freeze", "Excel Features" },
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Excel Like Features",
            });
#endif
            SampleHelper.SetTagsForProduct("Cell Grid", Tags.None);
        }
    }
}
