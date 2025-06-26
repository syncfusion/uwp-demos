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
    public sealed partial class CreateAnimationPresentation : SampleLayout
    {
        #region Constructor
        public CreateAnimationPresentation()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Dispose
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

        #endregion

        #region Event

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(GettingStartedPresentation).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.Animation.pptx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);

            IPresentation presentation = await Presentation.OpenAsync(fileStream);
            
			//Modify the existing animation
            CreateAnimation1(presentation);
			
            //Saves the presentation
			SavePPTX(presentation);
        }

        # region Create Animation
        private void CreateAnimation1(IPresentation presentation)
        {
            //Get the slide from the presentation
            ISlide slide = presentation.Slides[0];

            //Access the animation sequence to create effects
            ISequence sequence = slide.Timeline.MainSequence;

            //Add motion path effect to the shape
            IEffect line1 = sequence.AddEffect(slide.Shapes[8] as IShape, EffectType.PathUp, EffectSubtype.None, EffectTriggerType.OnClick);
            IMotionEffect motionEffect = line1.Behaviors[0] as IMotionEffect;
            motionEffect.Timing.Duration = 1f;
            IMotionPath motionPath = motionEffect.Path;
            motionPath[1].Points[0].X = 0.00365f;
            motionPath[1].Points[0].Y = -0.27431f;

            //Add motion path effect to the shape
            IEffect line2 = sequence.AddEffect(slide.Shapes[3] as IShape, EffectType.PathDown, EffectSubtype.None, EffectTriggerType.WithPrevious);
            motionEffect = line2.Behaviors[0] as IMotionEffect;
            motionEffect.Timing.Duration = 0.75f;
            motionPath = motionEffect.Path;
            motionPath[1].Points[0].X = 0.00234f;
            motionPath[1].Points[0].Y = 0.43449f;

            //Add wipe effect to the shape
            IEffect wipe1 = sequence.AddEffect(slide.Shapes[1] as IShape, EffectType.Wipe, EffectSubtype.None, EffectTriggerType.AfterPrevious);
            wipe1.Behaviors[1].Timing.Duration = 1f;

            //Add fly effect to the shape
            IEffect fly1 = sequence.AddEffect(slide.Shapes[5] as IShape, EffectType.Fly, EffectSubtype.Left, EffectTriggerType.AfterPrevious);
            fly1.Behaviors[1].Timing.Duration = 0.70f;
            fly1.Behaviors[2].Timing.Duration = 0.70f;

            ////Add wipe effect to the shape
            IEffect wipe2 = sequence.AddEffect(slide.Shapes[2] as IShape, EffectType.Wipe, EffectSubtype.None, EffectTriggerType.AfterPrevious);
            wipe2.Behaviors[1].Timing.Duration = 1f;

            ////Add fly effect to the shape
            IEffect fly2 = sequence.AddEffect(slide.Shapes[4] as IShape, EffectType.Fly, EffectSubtype.Right, EffectTriggerType.AfterPrevious);
            fly2.Behaviors[1].Timing.Duration = 0.70f;
            fly2.Behaviors[2].Timing.Duration = 0.70f;

            IEffect fly3 = sequence.AddEffect(slide.Shapes[6] as IShape, EffectType.Fly, EffectSubtype.Top, EffectTriggerType.AfterPrevious);
            fly3.Behaviors[1].Timing.Duration = 1.50f;
            fly3.Behaviors[2].Timing.Duration = 1.50f;

            ////Add flay effect to the shape
            IEffect fly4 = sequence.AddEffect(slide.Shapes[7] as IShape, EffectType.Fly, EffectSubtype.Left, EffectTriggerType.AfterPrevious);
            fly4.Behaviors[1].Timing.Duration = 0.50f;
            fly4.Behaviors[2].Timing.Duration = 0.50f;
        }
        #endregion

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
                savePicker.SuggestedFileName = "CreateAnimationSample";
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
        #endregion
    }
}
