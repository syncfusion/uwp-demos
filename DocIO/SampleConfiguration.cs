using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;

namespace Syncfusion.SampleBrowser.UWP.DocIO
{
    /// <summary>
    /// Represents the helper class for SamplesConfiguration.
    /// </summary>
    public class SamplesConfiguration
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SamplesConfigurationClass"/> class.
        /// </summary>
        public SamplesConfiguration()
        {

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.GettingStartedDemo).AssemblyQualifiedName,
                Header = "Hello World",
                Category = Categories.FileFormat,
                Product = "DocIO",
				ProductIcons = "Icons/docIo.png",
                SampleCategory = "Getting Started",
                SearchKeys = new string[] { "GettingStarted", "Basic", "Usage" },
                Tag = Tags.None
            });
#if !STORE_SB
            if (!ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                //This sample is not included for Windows Phone device.
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(EssentialDocIO.HeadersAndFootersDemo).AssemblyQualifiedName,
                    Header = "Headers And Footers",
                    Category = Categories.FileFormat,
                    SampleCategory = "Insert Content",
                    Product = "DocIO",
                    SearchKeys = new string[] { "HeadersAndFooters", "Header", "Footer" },
                    Tag = Tags.None
                });
            }
#endif
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.TableFormattingDemo).AssemblyQualifiedName,
                Header = "Table",
                Category = Categories.FileFormat,
                SampleCategory = "Formatting",
                Product = "DocIO",
                SearchKeys = new string[] { "TableFormatting", "Table", "Format" },
                Tag = Tags.None
            });
            if (!ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                //This sample is not included for Windows Phone device.
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(EssentialDocIO.FindAndReplaceDemo).AssemblyQualifiedName,
                    Header = "Find And Replace",
                    Category = Categories.FileFormat,
                    SampleCategory = "Editing",
                    Product = "DocIO",
                    Tag = Tags.None,
                    SearchKeys = new string[] { "FindAndReplace", "Find", "Replace" }
                });
            }

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.NestedMailMergeDemo).AssemblyQualifiedName,
                Header = "Nested Mail merge",
                Category = Categories.FileFormat,
                SampleCategory = "Mail merge",
                Product = "DocIO",
                SearchKeys = new string[] { "NestedMailMerge", "MailMerge", "Merge" },
                 Tag = Tags.None
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Invoice.MainPage).AssemblyQualifiedName,
                Header = "Invoice",
                Category = Categories.FileFormat,
                SampleCategory = "Product Showcase",
                Product = "DocIO",
                Description = "This sample demonstrates how the file format manipulation components can be used to create an invoice in Excel, PDF and Word formats.",
                SampleType = SampleType.Showcase,
                DesktopImage = "ms-appx:///DocIO/Invoice/Assets/invoice.png",
                MobileImage = "ms-appx:///DocIO/Invoice/Assets/invoice.png",
                SearchKeys = new string[] { "invoice", "basic" },
                Tag = Tags.None
            });
#if !STORE_SB
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.StylesDemo).AssemblyQualifiedName,
                Header = "Styles",
                Category = Categories.FileFormat,
                SampleCategory = "Formatting",
                Product = "DocIO",
                SearchKeys = new string[] { "Styles", "Appearance", "Format" },
                 Tag = Tags.None
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.FormFillingAndProtectionDemo).AssemblyQualifiedName,
                Header = "Form Filling and Protection",
                Category = Categories.FileFormat,
                Product = "DocIO",
                SampleCategory = "Content Control",
                SearchKeys = new string[] { "Content Control", "Form Filling and Protection", "Protection", "Form", "Filling" }
            });
			SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.XMLMappingDemo).AssemblyQualifiedName,
                Header = "XML Mapping",
                Category = Categories.FileFormat,
                Product = "DocIO",
                SampleCategory = "Content Control",
                SearchKeys = new string[] { "Content Control", "XML Mapping", "XML", "Mapping" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.BookmarkNavigationDemo).AssemblyQualifiedName,
                Header = "Bookmark Navigation",
                Category = Categories.FileFormat,
                SampleCategory = "Editing",
                Product = "DocIO",
                SearchKeys = new string[] { "Editing", "Bookmark", "Navigation" },
                Tag = Tags.None
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.BarChartDemo).AssemblyQualifiedName,
                Header = "Bar Chart",
                Category = Categories.FileFormat,
                SampleCategory = "Chart",
                Product = "DocIO",
                SearchKeys = new string[] { "Chart", "BarChart" },
                Tag = Tags.None
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.PieChartDemo).AssemblyQualifiedName,
                 Header = "Pie Chart",
                Category = Categories.FileFormat,
                SampleCategory = "Chart",
                Product = "DocIO",
                SearchKeys = new string[] { "Chart", "PieChart" },
                Tag = Tags.None
            });
			if (!ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                //This sample is not included for Windows Phone device.
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(EssentialDocIO.WordToODTDemo).AssemblyQualifiedName,
                    Header = "Word to ODT",
                    Category = Categories.FileFormat,
                    SampleCategory = "Import and Export",
                    Product = "DocIO",
					Description = "This sample demonstrates exporting Word document to OpenDocument Text (ODT)",
					DesktopImage = "ms-appx:///DocIO/Assets/wordtoodt.png",
                    Tag = Tags.None,
                    SearchKeys = new string[] { "WordToODT", "ODT"}
                });
            }

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.LetterFormattingDemo).AssemblyQualifiedName,
                Header = "Letter Formatting",
                Category = Categories.FileFormat,
                SampleCategory = "Mail merge",
                Product = "DocIO",
                SearchKeys = new string[] { "Letter", "LetterFormatting" },
                Tag = Tags.None
            });

           SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.AutoShapesDemo).AssemblyQualifiedName,
                Header = "AutoShapes",
               Category = Categories.FileFormat,
               SampleCategory = "Shapes",
               Product = "DocIO",
               SearchKeys = new string[] { "Shapes", "AutoShapes" },
               Tag = Tags.None
           });
#endif
           SampleHelper.SetTagsForProduct("DocIO", Tags.None);
        }
#endregion
    }
}

