#region Copyright Syncfusion Inc. 2001 - 2016
// Copyright Syncfusion Inc. 2001 - 2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Presentation;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Reflection;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPresentation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImagesPresentation : SampleLayout
    {
        #region Fields
        Assembly execAssm = typeof(Common.MainPage).GetTypeInfo().Assembly;
        #endregion

        public ImagesPresentation()
        {
            this.InitializeComponent();
        }

        public override void Dispose()
        {
            base.Dispose();

            DisposeTextBlock(ref emptyTextBlock1);
            emptyTextBlock1 = null;

            DisposeTextBlock(ref description);
            description = null;

            DisposeTextBlock(ref emptyTextBlock2);
            emptyTextBlock2 = null;

            DisposeTextBlock(ref btn_Description);
            btn_Description = null;

            DisposeTextBlock(ref emptyTextBlock3);
            emptyTextBlock3 = null;

            DisposeTextBlock(ref emptyTextBlock4);
            emptyTextBlock4 = null;

            Generate.Click -= Button_Click_1;
            DisposeButton(ref Generate);
            Generate = null;

            DisposeTextBlock(ref emptyTextBlock5);
            emptyTextBlock5 = null;
        }

        private void DisposeTextBlock(ref TextBlock textBlock)
        {
            //Clear the dependency properties of TextBlock
            textBlock.ClearValue(TextBlock.TextProperty);
            textBlock.ClearValue(TextBlock.TextWrappingProperty);
            textBlock.ClearValue(TextBlock.FontFamilyProperty);
            textBlock.ClearValue(TextBlock.FontSizeProperty);
            textBlock.ClearValue(TextBlock.ForegroundProperty);
            textBlock.ClearValue(TextBlock.VisibilityProperty);
        }

        private void DisposeButton(ref Button button)
        {
            //Clear the dependency properties of Button
            button.ClearValue(Button.ContentProperty);
            button.ClearValue(Button.PaddingProperty);
            button.ClearValue(Button.FontFamilyProperty);
            button.ClearValue(Button.FontSizeProperty);
            button.ClearValue(Button.ForegroundProperty);
            button.ClearValue(Button.BackgroundProperty);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(ImagesPresentation).GetTypeInfo().Assembly;

            IPresentation pptxDoc = Presentation.Create();

            //Add a blank slide to Presentation.
            ISlide firstSlide = pptxDoc.Slides.Add(SlideLayoutType.Blank);

            //Add a title text box to the slide.
            IShape titleShape = firstSlide.Shapes.AddTextBox(55, 23, 853, 72);
            //Set vertical alignment to the text body.
            titleShape.TextBody.VerticalAlignment = VerticalAlignmentType.Bottom;
            //Add a title paragraph.
            IParagraph titleParagraph = titleShape.TextBody.AddParagraph();
            //Set the alignment properties for the paragraph.
            titleParagraph.HorizontalAlignment = HorizontalAlignmentType.Center;
            //Add a text part.
            ITextPart titleTextPart = titleParagraph.AddTextPart("Adventure Works Cycles");
            //Set the font properties for the text part.
            titleTextPart.Font.FontSize = 36;

            //Add a content text box to the slide.
            IShape textbox = firstSlide.Shapes.AddTextBox(66, 132, 543, 350);
            //Add the first paragraph.
            IParagraph paragraph1 = textbox.TextBody.AddParagraph();
            //Set bulleted list type.
            paragraph1.ListFormat.Type = ListType.Bulleted;
            //Set paragraph properties.
            paragraph1.LeftIndent = 22;
            paragraph1.FirstLineIndent = -22;
            paragraph1.LineSpacing = 38;
            paragraph1.SpaceBefore = 10;
            //Add a new text part.
            paragraph1.AddTextPart("Adventure Works Cycles, the fictitious company on which the Adventure Works sample databases are based, is a large, multinational manufacturing company.");

            //Add the second paragraph.
            IParagraph paragraph2 = textbox.TextBody.AddParagraph();
            //Set bulleted list type.
            paragraph2.ListFormat.Type = ListType.Bulleted;
            //Set paragraph properties.
            paragraph2.LeftIndent = 22;
            paragraph2.FirstLineIndent = -22;
            paragraph2.LineSpacing = 38;
            paragraph2.SpaceBefore = 10;
            //Add a new text part.
            paragraph2.AddTextPart("The company manufactures and sells metal and composite bicycles to North American, European, and Asian commercial markets. While its base operation is located in Bothell, Washington, with 290 employees, several regional sales teams are located throughout their market base.");

            //Create an instance for the image as a stream.
            string resourcePath = "Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.CycleLogo.jpg";
            Stream imageStream = assembly.GetManifestResourceStream(resourcePath);
            //Add a picture to the shape collection.
            IPicture picture = firstSlide.Shapes.AddPicture(imageStream, 610, 246, 328, 123);
            imageStream.Close();
            //Add alternate text to the picture.
            picture.Description = "Adventure Works Cycles Logo";
            //Apply bounding box size and position.
            picture.Crop.ContainerWidth = 328f;
            picture.Crop.ContainerHeight = 123f;
            picture.Crop.ContainerLeft = 609f;
            picture.Crop.ContainerTop = 246f;
            //Apply cropping size and offsets.
            picture.Crop.Width = 370f;
            picture.Crop.Height = 151f;
            picture.Crop.OffsetX = -4.32f;
            picture.Crop.OffsetY = 1.44f;

            //Add a title-only slide to Presentation.
            ISlide secondSlide = pptxDoc.Slides.Add(SlideLayoutType.TitleOnly);
            //Retrieve the first shape of the slide.
            IShape titleShape2 = secondSlide.Shapes[0] as IShape;
            //Add a title paragraph.
            IParagraph titleParagraph2 = titleShape2.TextBody.AddParagraph();
            //Set the alignment properties for the paragraph.
            titleParagraph2.HorizontalAlignment = HorizontalAlignmentType.Center;
            //Add a text part.
            ITextPart titleTextPart2 = titleParagraph2.AddTextPart("About Adventure Works Cycles");
            //Set the font properties for the text part.
            titleTextPart2.Font.FontName = "Calibri";
            titleTextPart2.Font.FontSize = 40;

            //Get an SVG image as a stream.
            resourcePath = "Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.About.svg";
            Stream svgImageStream = assembly.GetManifestResourceStream(resourcePath);
            //Get a fallback image as a stream.
            resourcePath = "Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.About.png";
            Stream fallbackImageStream = assembly.GetManifestResourceStream(resourcePath);
            //Add the SVG picture to a slide by specifying its size and position.
            IPicture svgPicture = secondSlide.Pictures.AddPicture(svgImageStream, fallbackImageStream, 172, 155, 643, 256);
            //Add alternate text to the picture.
            svgPicture.Description = "About Adventure Works Cycles";
            svgImageStream.Close();
            fallbackImageStream.Close();
            MemoryStream ms = new MemoryStream();
            SavePPTX(pptxDoc);
        }
        /// <summary>
        /// Save as PPTX Format
        /// </summary>
        /// <param name="presentation"></param>
        private async void SavePPTX(IPresentation presentation)
        {
            StorageFile stgFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as 
                savePicker.FileTypeChoices.Add("Presentation", new List<string>() { ".pptx" });
                // Default file name if the user does not type one in or select a file to replace 
                savePicker.SuggestedFileName = "ImagesSample";
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("PowerPointPresentation.pptx", CreationCollisionOption.ReplaceExisting);
            }
            if (stgFile != null)
            {
                //Save as PPTX Format
                await presentation.SaveAsync(stgFile);
                MessageDialog msgDialog = new MessageDialog("Do you want to view the Presentation?", "File has been created successfully.");
                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new UICommand("No");
                msgDialog.Commands.Add(noCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the retrieved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(stgFile);
                }
            }
        }

    }
}
