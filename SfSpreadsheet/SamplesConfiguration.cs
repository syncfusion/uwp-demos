using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.SfSpreadsheet
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SpreadsheetSamples.SpreadsheetShowcase).AssemblyQualifiedName,
                Header = "Spreadsheet",
                Category = Categories.Grids,
                Product = "Spreadsheet",
                Tag = Tags.None,
                SampleType = SampleType.Showcase,
                Description = "This demo imports the excel workbook which has expense trends for an year using SfSpreadsheet.",
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/Assets/Images/Spreadsheet-Desktop.jpg",
                MobileImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/Assets/Images/Spreadsheet-Mobile.jpg"
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SpreadsheetSamples.GettingStarted).AssemblyQualifiedName,
                Header = "Getting Started",
                Product = "Spreadsheet",
                SearchKeys = new string[] { "Getting Started" },
                Description = "The SfSpreadsheet is an Excel inspired control that allows you to create, edit, view and format the Microsoft Excel files without Excel installed.",
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/WhatsNewImages/GettingStarted.png",
                ProductIcons = "Icons/Spreadsheet.png",
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Getting Started"
            });

#if !SPREADSHEET_STORE
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SpreadsheetSamples.Formula).AssemblyQualifiedName,
                Header = "Formula",
                Category = Categories.Grids,
                Product = "Spreadsheet",
                SearchKeys = new string[] { "Formula" },
                Description = "SfSpreadsheet calculation engine offers automated calculation over a formula, expression or cross sheet references with 409 formulas covering a broad range of business functions.",
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/WhatsNewImages/Formula.png",
                Tag = Tags.None,
                SampleCategory = "Getting Started"
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SpreadsheetSamples.CellFormatting).AssemblyQualifiedName,
                Header = "Cell Formatting",
                Product = "Spreadsheet",
                SearchKeys = new string[] { "Cell Formatting,Formatting" },
                Description = "This sample showcases various cell formatting options like font, background, alignment, cell borders, etc., in the SfSpreadsheet.",
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/WhatsNewImages/CellFormatting.png",
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Getting Started"
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SpreadsheetSamples.ConditionalFormatting).AssemblyQualifiedName,
                Header = "Conditional Formatting",
                Product = "Spreadsheet",
                SearchKeys = new string[] { "Formatting,Conditional Formatting" },
                Description = "This sample showcases the conditional formatting support that allows you to categorize cell styles that are dynamically applied, based on cell values and conditions that you define.",
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/WhatsNewImages/ConditionalFormatting.png",
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Data Visualization"
            });
#endif
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SpreadsheetSamples.AdvancedConditionalFormatting).AssemblyQualifiedName,
                Header = "Advanced Conditional Formatting",
                Product = "Spreadsheet",
                SearchKeys = new string[] { "Formatting,Conditional Formatting,Advanced" },
                Description = "This sample showcases advanced conditional formatting like data bars, icon sets and color scales in the SfSpreadsheet.",
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/WhatsNewImages/AdvancedConditionalFormatting.png",
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleCategory = "Data Visualization"
            });
#if !SPREADSHEET_STORE
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SpreadsheetSamples.Outline).AssemblyQualifiedName,
                Header = "Outlining",
                Category = Categories.Grids,
                SearchKeys = new string[] { "Outlining" },
                Description = "This sample showcases the outline support. SfSpreadsheet provides support for both row and column-wise grouping, and multi-level grouping like Excel",
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/WhatsNewImages/Outline.png",
                Product = "Spreadsheet",
                Tag = Tags.None,
                SampleCategory = "Data Visualization"
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SpreadsheetSamples.Chart).AssemblyQualifiedName,
                Header = "Chart",
                Category = Categories.Grids,
                SearchKeys = new string[] { "Chart" },
                Description = "This sample showcases how to import the Excel chart into the SfSpreadsheet.",
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/WhatsNewImages/Chart.png",
                Product = "Spreadsheet",
                Tag = Tags.None,
                SampleCategory = "Data Visualization"
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SpreadsheetSamples.GraphicCell).AssemblyQualifiedName,
                Header = "Graphic Cells",
                Category = Categories.Grids,
                SearchKeys = new string[] { "Graphics Cells", "Graphics" },
                Description = "This sample showcases the Image and RichTextBox importing support in SfSpreadsheet.",
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/WhatsNewImages/GraphicsCell.png",
                Product = "Spreadsheet",
                Tag = Tags.None,
                SampleCategory = "Data Visualization"
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SpreadsheetSamples.Sparkline).AssemblyQualifiedName,
                Header = "Sparklines",
                Description = "This sample showcases the Sparkline importing support in the SfSpreadsheet",
                SearchKeys = new string[] { "Sparklines" },
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/WhatsNewImages/Sparklines.png",
                Category = Categories.Grids,
                Product = "Spreadsheet",
                Tag = Tags.None,
                SampleCategory = "Data Visualization"
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SpreadsheetSamples.CellCustomization).AssemblyQualifiedName,
                Header = "Cell Customization",
                Description = "This sample showcases the cell customization support in Spreadsheet control.",
                SearchKeys = new string[] { "Cell Customization","Customization"},
                Category = Categories.Grids,
				DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/WhatsNewImages/CellCustomization.png",
                MobileImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfSpreadsheet/WhatsNewImages/CellCustomization.png",
                Product = "Spreadsheet",
                Tag = Tags.None,
                SampleCategory = "Miscellaneous"
            });
            // Miscellaneous
            //SampleHelper.SampleViews.Add(new SampleInfo()
            //{
            //    SampleView = typeof(SpreadsheetSamples.Localization).AssemblyQualifiedName,
            //    Header = "Localization",
            //    Product = "Spreadsheet",
            //    Description = "This sample showcases the localization support in SfSpreadsheet. It allows you to set custom resources through a .resx file.",
            //    SearchKeys = new string[] { "Localization" },
            //    DesktopImage = "ms-appx:///WhatsNewImages/Localization.png",
            //    Category = Categories.Grids,
            //    Tag = Tags.None,
            //    SampleCategory = "Miscellaneous"
            //});
#endif
            SampleHelper.SetTagsForProduct("Spreadsheet", Tags.None);
        }
    }
}
