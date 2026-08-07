#region Copyright Syncfusion Inc. 2001 - 2016
// Copyright Syncfusion Inc. 2001 - 2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Presentation;
using Syncfusion.OfficeChartToImageConverter;
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
    public sealed partial class PPTXToImagePresentation : SampleLayout
    {
        #region fields
        IPresentation presentation;        
        #endregion

        #region Constructor
        public PPTXToImagePresentation()
        {
            this.InitializeComponent();

            // Add document types into combo box
            documentOptions.Items.Add("GettingStarted.pptx");
            documentOptions.Items.Add("Charts.pptx");
            documentOptions.Items.Add("Choose from file");
            documentOptions.SelectedIndex = 0;
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

            DisposeTextBlock(ref browseBtn_Description);
            browseBtn_Description = null;

            DisposeTextBlock(ref emptyTextBlock3);
            emptyTextBlock3 = null;

            Choose.Click -= Choose_BtnClick;
            DisposeButton(ref Choose);
            Choose = null;

            DisposeTextBlock(ref inputFilePath);
            inputFilePath = null;

            DisposeTextBlock(ref emptyTextBlock4);
            emptyTextBlock4 = null;

            DisposeTextBlock(ref converBtn_Description);
            converBtn_Description = null;

            documentOptions.ClearValue(ComboBox.FontFamilyProperty);
            documentOptions.ClearValue(ComboBox.FontSizeProperty);
            documentOptions.ClearValue(ComboBox.ForegroundProperty);
            documentOptions = null;

            DisposeTextBlock(ref emptyTextBlock5);
            emptyTextBlock5 = null;

            Convert.Click -= Convert_BtnClick;
            DisposeButton(ref Convert);
            Convert = null;

            DisposeTextBlock(ref emptyTextBlock6);
            emptyTextBlock6 = null;

            DisposeTextBlock(ref emptyTextBlock7);
            emptyTextBlock7 = null;
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

        #region Open button
        private async void Choose_BtnClick(object sender, RoutedEventArgs e)
        {
            // Open the input presentation file
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".pptx");
            StorageFile inputFile = await openPicker.PickSingleFileAsync();
            if (inputFile != null)
            {
                presentation = await Presentation.OpenAsync(inputFile);
                inputFilePath.Text = inputFile.Path;
            }
        }
        #endregion

        #region Convert button
        private async void Convert_BtnClick(System.Object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(PPTXToImagePresentation).GetTypeInfo().Assembly;
            switch (documentOptions.SelectedIndex)
            {
                case 0:
                    string resourcePath = "Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.GettingStarted.pptx";
                    Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
                    presentation = await Presentation.OpenAsync(fileStream);
                    fileStream.Dispose();
                    break;

                case 1:
                    resourcePath = "Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.Charts.pptx";
                    fileStream = assembly.GetManifestResourceStream(resourcePath);
                    presentation = await Presentation.OpenAsync(fileStream);
                    fileStream.Dispose();
                    break;
            }
            if (presentation != null)
            {
                StorageFolder storageFolder;
                if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
                {
                    // Pick the folder to save the converted images.
                    FolderPicker folderPicker = new FolderPicker();                    
                    folderPicker.ViewMode = PickerViewMode.Thumbnail;
                    folderPicker.FileTypeFilter.Add("*");
                    storageFolder = await folderPicker.PickSingleFolderAsync();
                }
                else
                {
                    storageFolder = ApplicationData.Current.LocalFolder;
                }
                if (storageFolder != null)
                {
                    // Intialize the ChartToImageConverter instance to convert the chart to image.
                    presentation.ChartToImageConverter = new ChartToImageConverter();
                    int i = 1;                    
                    // Convert the slide to image.
                    foreach (ISlide slide in presentation.Slides)
                    {
                        StorageFile imageFile = await storageFolder.CreateFileAsync("Slide" + i++ + ".jpg", CreationCollisionOption.ReplaceExisting);
                        await slide.SaveAsImageAsync(imageFile);
                    }
                    // Close the presentation instance
                    presentation.Close();
                    presentation = null;

                    // View the popup message
                    MessageDialog msgDialog = new MessageDialog("Do you want to view the generated images?", "Slides has been saved as images successfully.");
                    UICommand yesCmd = new UICommand("Yes");
                    msgDialog.Commands.Add(yesCmd);
                    UICommand noCmd = new UICommand("No");
                    msgDialog.Commands.Add(noCmd);
                    IUICommand cmd = await msgDialog.ShowAsync();
                    if (cmd == yesCmd)
                    {                        
                        // Launch the image generated folder
                        bool success = await Windows.System.Launcher.LaunchFolderAsync(storageFolder);
                    }
                }
            }
            else
            {
                // View the popup message
                MessageDialog msgDialog = new MessageDialog("Please browse the input file to convert as image.");
                await msgDialog.ShowAsync();
            }
            inputFilePath.Text = "No file chosen";
        }
        #endregion

        #region Helper method
        private void InputDocumentOptionChanged(System.Object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(PPTXToImagePresentation).GetTypeInfo().Assembly;
            switch (documentOptions.SelectedIndex)
            {
                case 0:
                    Choose.IsEnabled = false;
                    inputFilePath.Text = "No file chosen";
                    break;

                case 1:
                    Choose.IsEnabled = false;
                    inputFilePath.Text = "No file chosen";
                    break;

                case 2:
                    presentation = null;
                    Choose.IsEnabled = true;
                    break;
            }
        }
        #endregion
    }
}
