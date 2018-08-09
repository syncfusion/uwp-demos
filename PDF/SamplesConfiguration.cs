using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.Pdf
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
#if !STORE_SB
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.JobApplication).AssemblyQualifiedName,
                Header = "Job Application",
                Category = Categories.FileFormat,
                SampleCategory = "Product Showcase",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "JobApplication", "Job", "Application" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.Invoice).AssemblyQualifiedName,
                Header = "Invoice",
                Category = Categories.FileFormat,
                SampleCategory = "Product Showcase",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "Invoice", "Interactive", "Feature" }
            });
#endif
            //Getting Started
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.GettingStarted).AssemblyQualifiedName,
                Header = "Getting Started",
                Category = Categories.FileFormat,
                SampleCategory = "Getting Started",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "GettingStarted", "Basic", "Usage" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.HelloWorld).AssemblyQualifiedName,
                Header = "Hello World",
                Category = Categories.FileFormat,
                SampleCategory="Getting Started",
                Product="PDF",
                Tag=Tags.None,
                SearchKeys = new string[] { "HelloWorld", "Hello", "World" }
            });
#if !STORE_SB
            //Drawing Text
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.BulletsAndLists).AssemblyQualifiedName,
                Header = "Bullets and Lists",
                Category = Categories.FileFormat,
                SampleCategory = "Drawing Text",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "BulletsAndLists", "Bullets", "Styles" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.TextFlow).AssemblyQualifiedName,
                Header = "Text Flow",
                Category = Categories.FileFormat,
                SampleCategory = "Drawing Text",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "TextFlow", "Text", "Draw" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.RTLSupport).AssemblyQualifiedName,
                Header = "RTL Text",
                Category = Categories.FileFormat,
                SampleCategory = "Drawing Text",
                Product = "PDF",
				Description= "This sample demonstrates drawing right-to-left language text in the PDF document.",
                Tag = Tags.NewWithWhatsNew,
				DesktopImage = "ms-appx:///Images/RTLSupport.png",
                MobileImage = "ms-appx:///Images/RTLSupport.png",
                SearchKeys = new string[] { "RTL Text", "RTL", "Right to Left" }
            });
#endif
            //Graphics
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.ImageInsertion).AssemblyQualifiedName,
                Header = "Image Insertion",
                Category = Categories.FileFormat,
                SampleCategory = "Graphics",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "ImageInsertion", "Image", "Drawing" }
            });
#if !STORE_SB
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.Layers).AssemblyQualifiedName,
                Header = "Layers",
                Category = Categories.FileFormat,
                SampleCategory = "Graphics",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "Layers", "Interactive" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.Barcode).AssemblyQualifiedName,
                Header = "Barcode",
                Category = Categories.FileFormat,
                SampleCategory = "Graphics",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "Barcode" }
            });

            //Tables
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.NorthWindReport).AssemblyQualifiedName,
                Header = "NorthWind Report",
                Category = Categories.FileFormat,
                SampleCategory = "Tables",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "NorthWindReport", "Table", "Data" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.AdventureCycle).AssemblyQualifiedName,
                Header = "Adventure Cycle",
                Category = Categories.FileFormat,
                SampleCategory = "Tables",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "AdventureCycle", "Interactive", "Usage" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.TableFeatures).AssemblyQualifiedName,
                Header = "Table Features",
                Category = Categories.FileFormat,
                SampleCategory = "Tables",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "TableFeatures", "Table", "Data" }
            });
            //Settings
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.HeadersAndFooters).AssemblyQualifiedName,
                Header = "Headers and Footers",
                Category = Categories.FileFormat,
                SampleCategory = "Settings",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "HeadersAndFooters", "Headers", "Footers" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.PageSettings).AssemblyQualifiedName,
                Header = "Page Settings",
                Category = Categories.FileFormat,
                SampleCategory = "Settings",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "PageSettings", "Page", "settings" }
            });
#endif
            //Security
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.Encryption).AssemblyQualifiedName,
                Header = "Encryption",
                Category = Categories.FileFormat,
                SampleCategory = "Security",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "Encryption", "Security", "password" }
            });
            
            //User Interaction
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.FormFilling).AssemblyQualifiedName,
                Header = "Form Filling",
                Category = Categories.FileFormat,
                SampleCategory = "User Interaction",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "FormFilling", "Form", "Fields" }
            });
#if !STORE_SB
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.DigitalSignature).AssemblyQualifiedName,
                Header = "Digital Signature",
                Category = Categories.FileFormat,
                SampleCategory = "Security",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "Signature", "Digital", "security" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.XFAFormCreation).AssemblyQualifiedName,
                Header = "XFA Form Creation",
                SampleCategory = "User Interaction",
                Category = Categories.FileFormat,
                Product = "PDF",               
                Tag = Tags.None,
                SearchKeys = new string[] { "XFA Form Creation", "XFA", "Form" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.XFAFormFilling).AssemblyQualifiedName,
                Header = "XFA Form Filling",
                SampleCategory = "User Interaction",
                Category = Categories.FileFormat,
                Product = "PDF",               
                Tag = Tags.None,
                SearchKeys = new string[] { "XFA Form Filling", "XFA", "Form" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.NamedDestination).AssemblyQualifiedName,
                Header = "Named Destination",
                SampleCategory = "User Interaction",
                Category = Categories.FileFormat,
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "NamedDestination", "Name", "Destination" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.InteractiveFeatures).AssemblyQualifiedName,
                Header = "Interactive Features",
                Category = Categories.FileFormat,
                SampleCategory = "User Interaction",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "InteractiveFeatures", "Interactive", "Feature" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.AnnotationFlatten).AssemblyQualifiedName,
                Header = "Annotation Flatten",
                Category = Categories.FileFormat,
                SampleCategory = "User Interaction",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "AnnotationFlatten", "Annotation", "Flatten" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.Portfolio).AssemblyQualifiedName,
                Header = "Portfolio",
                Category = Categories.FileFormat,
                ProductIcons = "Icons/pdf.png",
                SampleCategory = "User Interaction",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "Portfolio" }
            });
            //Import and Export
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.XPSToPDF).AssemblyQualifiedName,
                Header = "XPS to PDF",
                Category = Categories.FileFormat,
                SampleCategory = "Import and Export",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "XPS", "XPSToPDF" }
            });
            //Modify Documents
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.StampDocument).AssemblyQualifiedName,
                Header = "Stamp Document",
                Category = Categories.FileFormat,
                SampleCategory = "Modify Documents",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "StampDocument", "Pdf", "Stamp" }
            });            
            
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.OverlayDocuments).AssemblyQualifiedName,
                Header = "Overlay Documents",
                Category = Categories.FileFormat,
                SampleCategory = "Modify Documents",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "OverlayDocuments", "Overlay", "Documents" }
            });           
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.Template).AssemblyQualifiedName,
                Header = "Template",
                Category = Categories.FileFormat,
                SampleCategory = "Modify Documents",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "Template" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.MergePDF).AssemblyQualifiedName,
                Header = "Merge PDF",
                Category = Categories.FileFormat,
                SampleCategory = "Modify Documents",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "MergePDF", "Merge", "Pdf" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.SplitPDF).AssemblyQualifiedName,
                Header = "Split PDF",
                Category = Categories.FileFormat,
                SampleCategory = "Modify Documents",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "SplitPDF", "Split", "Pdf" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.AutoTag).AssemblyQualifiedName,
                Header = "Autotag",
                Category = Categories.FileFormat,
                SampleCategory = "Accessibility",
                Product = "PDF",
               Tag = Tags.None,
                SearchKeys = new string[] { "Autotag", "Tag", "Pdf" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EssentialPdf.CustomTag).AssemblyQualifiedName,
                Header = "Customtag",
                Category = Categories.FileFormat,
                SampleCategory = "Accessibility",
                Product = "PDF",
                Tag = Tags.None,
                SearchKeys = new string[] { "Customtag", "Tag", "Pdf" }
            });

#endif
#if !STORE_SB
            SampleHelper.SetTagsForProduct("PDF", Tags.Updated);
#else
            SampleHelper.SetTagsForProduct("PDF", Tags.None);
#endif
        }
    }
}
