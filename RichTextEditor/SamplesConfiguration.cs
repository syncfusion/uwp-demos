using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;

namespace Syncfusion.SampleBrowser.UWP.RichTextEditor
{

    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Document Editor",
                    SampleType = SampleType.Showcase,
                    DesktopImage = "ms-appx:///RTE/Assets/Showcase/DocumentEditor_Desktop.jpg",
                    MobileImage = "ms-appx:///RTE/Assets/Showcase/DocumentEditor_Phone.jpg",
                    Description = "The RichTextBoxAdv and Ribbon controls have been used to build a sample application that looks similar to Microsoft Word.",
                    Product = "RichTextEditor",
                    SampleView = typeof(RTEDemo.DocumentEditor).AssemblyQualifiedName,
                    Tag = Tags.Updated,
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Document Editor",
                    Category = Categories.Editors,
                    Product = "RichTextEditor",
                    SampleCategory = "Showcase",
                    ProductIcons = "Icons/RTE.png",
                    SearchKeys = new string[] { "document", "editor", "word", "richtextbox" },
                    SampleView = typeof(RTEDemo.DocumentEditor).AssemblyQualifiedName,
                    Tag = Tags.Updated,
                });
#if !STORE_SB
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Customized Editor",
                    Category = Categories.Editors,
                    Product = "RichTextEditor",
                    SampleCategory = "Customization",
                    SearchKeys = new string[] { "document", "editor", "word", "richtextbox" },
                    SampleView = typeof(RTEDemo.CustomizedEditor).AssemblyQualifiedName,
                    Tag = Tags.None,
                });
#endif
            }
            else
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Document Editor",
                    SampleType = SampleType.Showcase,
                    DesktopImage = "ms-appx:///RTE/Assets/Showcase/DocumentEditor_Desktop.jpg",
                    MobileImage = "ms-appx:///RTE/Assets/Showcase/DocumentEditor_Phone.jpg",
                    Description = "The RichTextBoxAdv and Ribbon controls have been used to build a sample application that looks similar to Microsoft Word.",
                    Product = "RichTextEditor",
                    SampleView = typeof(RTEDemo.DocumentEditor).AssemblyQualifiedName,
                    Tag = Tags.Updated,
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Document Editor",
                    Category = Categories.Editors,
                    Product = "RichTextEditor",
                    SampleCategory = "Showcase",
                    ProductIcons = "Icons/RTE.png",
                    SearchKeys = new string[] { "document", "editor", "word", "richtextbox" },
                    SampleView = typeof(RTEDemo.DocumentEditor).AssemblyQualifiedName,
                    Tag = Tags.Updated,
                });
#if !STORE_SB
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Customized Editor",
                    Category = Categories.Editors,
                    Product = "RichTextEditor",
                    SampleCategory = "Customization",
                    SearchKeys = new string[] { "document", "editor", "word", "richtextbox" },
                    SampleView = typeof(RTEDemo.CustomizedEditor).AssemblyQualifiedName,
                    Tag = Tags.None,
                });
#endif
            }
            if (!ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
#if !STORE_SB
                //This sample is not included for Windows Phone device.
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Forum",
                    Category = Categories.Editors,
                    Product = "RichTextEditor",
                    SampleCategory = "Customization",
                    SearchKeys = new string[] { "forum", "block", "editor", "richtextbox" },
                    SampleView = typeof(RTEDemo.Forum).AssemblyQualifiedName,
                    Tag = Tags.None,
                });
#endif
            }
            SampleHelper.SetTagsForProduct("RichTextEditor", Tags.Updated);
        }
    }
}
