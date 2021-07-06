#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
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
using Windows.UI.Xaml;

namespace Syncfusion.SampleBrowser.UWP.Presentation
{

    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Tag = Tags.None,
                Product = "Presentation",
                ProductIcons = "Icons/presentation.png",
                Category = Categories.FileFormat,
                SampleCategory = "Default Functionalities",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "hello world", "getting started" },
                SampleView = typeof(EssentialPresentation.GettingStartedPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Slides",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Slide Elements",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "slide" },
                SampleView = typeof(EssentialPresentation.SlidesPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Tables",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Slide Elements",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "table" },
                SampleView = typeof(EssentialPresentation.TablesPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Images",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Slide Elements",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "image" },
                SampleView = typeof(EssentialPresentation.ImagesPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Comments",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Slide Elements",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "table" },
                SampleView = typeof(EssentialPresentation.CommentsPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Connectors",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Slide Elements",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "connector" },
                SampleView = typeof(EssentialPresentation.ConnectorsPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Header And Footer",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Slide Elements",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "HeaderFooter" },
                SampleView = typeof(EssentialPresentation.HeaderFooterPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Charts",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Working With Charts",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "chart" },
                SampleView = typeof(EssentialPresentation.ChartsPresentation).AssemblyQualifiedName,
            });
			 SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "PPTX To Image",
                Tag = Tags.Updated,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Conversion",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "image" },
                SampleView = typeof(EssentialPresentation.PPTXToImagePresentation).AssemblyQualifiedName,
            });

#if !STORE_SB
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Presentation Viewer",
                SampleType = SampleType.Showcase,
                DesktopImage = "ms-appx:///Presentation/Assets/PresentationViewer_Desktop.jpg",
                MobileImage = "ms-appx:///Presentation/Assets/PresentationViewer_Mobile.jpg",
                Description = "Presentation viewer is a sample application that can be used to view PowerPoint presentations in windows mobile and desktop environments, similar to Microsoft Powerpoint.",
                Product = "Presentation",
                SampleView = typeof(EssentialPresentation.PPTXViewer).AssemblyQualifiedName,
                Tag = Tags.None,
            });
#endif
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Write Protection",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Security",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "Protect", "Write", "Security" },
                SampleView = typeof(EssentialPresentation.WriteProtectionPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Create",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Animation",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "create", "animation" },
                SampleView = typeof(EssentialPresentation.CreateAnimationPresentation).AssemblyQualifiedName,
            });
			SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Modify",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Animation",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "modify", "animation" },
                SampleView = typeof(EssentialPresentation.ModifyAnimationPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Slide Transition",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Transition",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "slide", "transition" },
                SampleView = typeof(EssentialPresentation.SlideTransitionPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SetTagsForProduct("Presentation", Tags.Updated);
        }
    }
}