#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
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
                    Tag = Tags.None,
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
                    Tag = Tags.None,
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
                SampleHelper.SetTagsForProduct("RichTextEditor", Tags.None);
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
                    Tag = Tags.None,
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
                    Tag = Tags.None,
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
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "UI Container",
                    Category = Categories.Editors,
                    Product = "RichTextEditor",
                    SampleCategory = "Customization",
                    SearchKeys = new string[] { "create-letters", "uielement", "uicontainer", "editor", "richtextbox" },
                    SampleView = typeof(RTEDemo.CreateLetters).AssemblyQualifiedName,
                    Tag = Tags.None,
                });
                SampleHelper.SetTagsForProduct("RichTextEditor", Tags.None);
#else
                SampleHelper.SetTagsForProduct("RichTextEditor", Tags.None);
#endif
            }
        }
    }
}
