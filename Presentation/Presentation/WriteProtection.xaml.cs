#region Copyright Syncfusion Inc. 2001 - 2019
// Copyright Syncfusion Inc. 2001 - 2019. All rights reserved.
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
    public sealed partial class WriteProtectionPresentation : SampleLayout
    {
        #region Fields
        Assembly execAssm = typeof(Common.MainPage).GetTypeInfo().Assembly;
        #endregion

        public WriteProtectionPresentation()
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

            DisposeTextBlock(ref passwordTextBlock);
            passwordTextBlock = null;

            DisposeTextBlock(ref emptyTextBlock4);
            emptyTextBlock4 = null;

            DisposePassWord(ref txtUserPassword);
            txtUserPassword = null;

            Generate.Click -= Button_Click_1;
            DisposeButton(ref Generate);
            Generate = null;

            DisposeTextBlock(ref emptyTextBlock5);
            emptyTextBlock5 = null;
        }

        private void DisposePassWord(ref PasswordBox passwordBox)
        {
            //Clear the dependency properties of PasswordBox
            passwordBox.ClearValue(PasswordBox.PasswordProperty);
            passwordBox.ClearValue(PasswordBox.FontFamilyProperty);
            passwordBox.ClearValue(PasswordBox.FontSizeProperty);
            passwordBox.ClearValue(PasswordBox.ForegroundProperty);
            passwordBox.ClearValue(PasswordBox.VisibilityProperty);
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
            Assembly assembly = typeof(GettingStartedPresentation).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.Transition.pptx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);

            //Open existing PowerPoint presentation.
            IPresentation presentation = await Presentation.OpenAsync(fileStream);

            if (txtUserPassword.Password == null)
                txtUserPassword.Password = string.Empty;

            //Set write protection for the PowerPoint presentation.
            presentation.SetWriteProtection(txtUserPassword.Password);

            MemoryStream ms = new MemoryStream();

            SavePPTX(presentation);
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
                savePicker.SuggestedFileName = "WriteProtectionSample";
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
