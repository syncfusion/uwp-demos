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
    public sealed partial class MarkdownToPPTXPresentation : SampleLayout
    {
        #region fields
        IPresentation presentation;
        #endregion

        #region Constructor
        public MarkdownToPPTXPresentation()
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

            inputFilePath.ClearValue(TextBox.TextProperty);
            inputFilePath = null;

            Browse.Click -= Browse_BtnClick;
            DisposeButton(ref Browse);
            Browse = null;

            DisposeTextBlock(ref emptyTextBlock3);
            emptyTextBlock3 = null;

            DisposeTextBlock(ref converBtn_Description);
            converBtn_Description = null;

            DisposeTextBlock(ref emptyTextBlock4);
            emptyTextBlock4 = null;

            Convert.Click -= Convert_BtnClick;
            DisposeButton(ref Convert);
            Convert = null;

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

        #region Browse button
        private async void Browse_BtnClick(object sender, RoutedEventArgs e)
        {
            // Open the input markdown file
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".md");
            StorageFile inputFile = await openPicker.PickSingleFileAsync();
             if (inputFile != null)
             {
                Stream fileStream = await inputFile.OpenStreamForReadAsync();
                presentation = Presentation.Open(fileStream);
                fileStream.Dispose();
                inputFilePath.Text = inputFile.Name;
             }
        }
        #endregion

        #region Convert button
        private async void Convert_BtnClick(System.Object sender, RoutedEventArgs e)
        {
            if (presentation == null)
            {
                Assembly assembly = typeof(MarkdownToPPTXPresentation).GetTypeInfo().Assembly;
                string resourcePath = "Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.MarkdownToPPTX.md";
                Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
                presentation = Presentation.Open(fileStream);
                fileStream.Dispose();
            }

            Save(presentation);
        }
        #endregion

        #region Save method
        private async void Save(IPresentation presentation)
        {
            StorageFile stgFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Default file name if the user does not type one in or select a file to replace
                savePicker.SuggestedFileName = "MarkdownToPPTX";
                savePicker.FileTypeChoices.Add("Presentation", new List<string>() { ".pptx" });
                stgFile = await savePicker.PickSaveFileAsync();
                if (stgFile != null)
                    await presentation.SaveAsync(stgFile);
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("Sample.pptx", CreationCollisionOption.ReplaceExisting);
            }
            if (stgFile != null)
            {
                MemoryStream stream = new MemoryStream();
                await presentation.SaveAsync(stream);
                presentation.Close();
                this.presentation = null;
                stream.Position = 0;
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stgFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();
                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");
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
            inputFilePath.Text = "MarkdownToPPTX.md";
        }
        #endregion
    }
}
