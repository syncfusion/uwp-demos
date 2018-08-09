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
                Tag = Tags.None,
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
                Tag = Tags.Updated,
            });
#endif
			SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Create",
                Tag = Tags.New,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Animation",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "create", "animation" },
                SampleView = typeof(EssentialPresentation.CreateAnimationPresentation).AssemblyQualifiedName,
            });
			SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Modify",
                Tag = Tags.New,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Animation",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "modify", "animation" },
                SampleView = typeof(EssentialPresentation.ModifyAnimationPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SetTagsForProduct("Presentation", Tags.Updated);
        }
    }
}