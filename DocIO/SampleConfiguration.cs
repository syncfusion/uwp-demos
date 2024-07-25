#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.MarkdownToWordDemo).AssemblyQualifiedName,
                Header = "Markdown to Word",
                Category = Categories.FileFormat,
                SampleCategory = "Import and Export",
                Product = "DocIO",
                Description = "This sample demonstrates how to convert the Markdown file to Word document using .NET Word (DocIO) library.",
                Tag = Tags.None,
                SearchKeys = new string[] { "MarkdownToWord", "Word"}
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
                SampleView = typeof(EssentialDocIO.CreateEquationDemo).AssemblyQualifiedName,
                Header = "Create Equation",
                Category = Categories.FileFormat,
                SampleCategory = "Mathematical Equation",
                Product = "DocIO",
                SearchKeys = new string[] { "Equations", "MathML" },
                Tag = Tags.None
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.CreateUsingLaTeXDemo).AssemblyQualifiedName,
                Header = "Create using LaTeX",
                Category = Categories.FileFormat,
                SampleCategory = "Mathematical Equation",
                Product = "DocIO",
                SearchKeys = new string[] { "Equations", "LaTeX" },
                Tag = Tags.None
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.EditUsingLaTeXDemo).AssemblyQualifiedName,
                Header = "Edit using LaTeX",
                Category = Categories.FileFormat,
                SampleCategory = "Mathematical Equation",
                Product = "DocIO",
                SearchKeys = new string[] { "Equations", "LaTeX" },
                Tag = Tags.New
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
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.EncryptAndDecryptDemo).AssemblyQualifiedName,
                Header = "Encrypt and Decrypt",
                Category = Categories.FileFormat,
                SampleCategory = "Security",
                Product = "DocIO",
                SearchKeys = new string[] { "Security", "EncryptAndDecrypt" },
                Tag = Tags.None
            });
			SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.TrackChangesDemo).AssemblyQualifiedName,
                Header = "Track Changes",
                Category = Categories.FileFormat,
                SampleCategory = "Review",
                Product = "DocIO",
                SearchKeys = new string[] { "Review", "TrackChanges" },
                Tag = Tags.None
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialDocIO.CompareDocumentsDemo).AssemblyQualifiedName,
                Header = "Compare Documents",
                Category = Categories.FileFormat,
                SampleCategory = "Review",
                Product = "DocIO",
                SearchKeys = new string[] { "Compare", "Compare Word" },
                Tag = Tags.None
            });
#endif
            SampleHelper.SetTagsForProduct("DocIO", Tags.Updated);
        }
#endregion
    }
}

