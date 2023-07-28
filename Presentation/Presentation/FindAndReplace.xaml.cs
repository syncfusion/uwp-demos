#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.Presentation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPresentation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FindAndReplaceDemo : SampleLayout
    {
        public override void Dispose()
        {
            base.Dispose();

            DisposeTextBlock(TextBlock1);
            TextBlock1 = null;
            DisposeTextBlock(TextBlock2);
            TextBlock2 = null;
            DisposeTextBlock(TextBlock3);
            TextBlock3 = null;
            DisposeTextBlock(TextBlock5);
            TextBlock5 = null;
            DisposeTextBlock(TextBlock6);
            TextBlock6 = null;
            DisposeTextBlock(TextBlock7);
            TextBlock7 = null;
            DisposeTextBlock(TextBlock8);
            TextBlock8 = null;
            DisposeTextBlock(TextBlock9);
            TextBlock9 = null;
            DisposeTextBlock(TextBlock10);
            TextBlock10 = null;
            DisposeTextBlock(TextBlock11);
            TextBlock11 = null;
            DisposeTextBlock(TextBlock12);
            TextBlock12 = null;
            DisposeTextBlock(TextBlock13);
            TextBlock13 = null;
            DisposeTextBlock(TextBlock14);
            TextBlock14 = null;
            DisposeTextBlock(TextBlock15);
            TextBlock15 = null;

            DisposeCheckBox(checkBoxCase);
            checkBoxCase = null;
            DisposeCheckBox(checkBoxFirstWord);
            checkBoxFirstWord = null;
            DisposeCheckBox(checkBoxWord);
            checkBoxWord = null;

            DisposeTextBox(textFind);
            textFind = null;
            DisposeTextBox(textReplace);
            textReplace = null;
            DisposeTextBox(regexFind);
            regexFind = null;

            viewTemplateButton.Click -= Button_Click_1;
            DisposeButton(viewTemplateButton);
            viewTemplateButton = null;

            replaceButton.Click -= Button_Click_2;
            DisposeButton(replaceButton);
            replaceButton = null;


            FindAndReplace.ClearValue(Grid.BackgroundProperty);
            FindAndReplace.ClearValue(Grid.PaddingProperty);
            FindAndReplace.Children.Clear();
            FindAndReplace.ColumnDefinitions.Clear();
            FindAndReplace.RowDefinitions.Clear();
            FindAndReplace = null;
        }

        public void DisposeCheckBox(CheckBox checkBox)
        {
            checkBox.ClearValue(CheckBox.FontFamilyProperty);
            checkBox.ClearValue(CheckBox.FontSizeProperty);
            checkBox.ClearValue(CheckBox.FontSizeProperty);
        }
        public void DisposeTextBox(TextBox textBox)
        {
            textBox.ClearValue(TextBox.FontFamilyProperty);
            textBox.ClearValue(TextBox.FontSizeProperty);
            textBox.ClearValue(TextBox.ForegroundProperty);
        }
        
        public void DisposeTextBlock(TextBlock textBlock)
        {
            textBlock.ClearValue(TextBlock.FontFamilyProperty);
            textBlock.ClearValue(TextBlock.FontSizeProperty);
            textBlock.ClearValue(TextBlock.TextProperty);
            textBlock.ClearValue(TextBlock.TextWrappingProperty);
            textBlock.ClearValue(TextBlock.ForegroundProperty);
        }
        public void DisposeButton(Button button)
        {
            button.ClearValue(Button.FontFamilyProperty);
            button.ClearValue(Button.FontSizeProperty);
            button.ClearValue(Button.PaddingProperty);
            button.ClearValue(Button.ForegroundProperty);
            button.ClearValue(Button.BackgroundProperty);
            button.ClearValue(Button.ContentProperty);
            button.ClearValue(Button.HeightProperty);
            button.ClearValue(Button.WidthProperty);
            button.ClearValue(Button.IsEnabledProperty);
        }
        public FindAndReplaceDemo()
        {
            InitializeComponent();
            textFind.Text = "{product}";
            regexFind.Text = "{[A-Za-z]+}";
            textReplace.Text = "Service";
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(GettingStartedPresentation).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.InputTemplate.pptx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            IPresentation pptxDoc = await Presentation.OpenAsync(fileStream);
            SavePPTX(pptxDoc, "Input Template");
        }
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(GettingStartedPresentation).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.InputTemplate.pptx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            MessageDialog messageBox;
            IPresentation pptxDoc = await Presentation.OpenAsync(fileStream);
            // Checking whether the find and replacement text boxes are filled.
            if (textFind.Text.Trim() == "")
            {
                messageBox = new MessageDialog("Browse a file to perform find and replace functionality", "Error");
                await messageBox.ShowAsync();
                return;
            }
            if (textFind.Text.Trim() == "" && textReplace.Text.Trim() == "")
            {
                messageBox = new MessageDialog("Please fill the find and replacement text in appropriate textboxes...", "Error");
                await messageBox.ShowAsync();
                return;
            }
            if (regexFind.Text.Trim() == "" && textReplace.Text.Trim() == "")
            {
                messageBox = new MessageDialog("Please fill the find regex and replacement text in appropriate textboxes...", "Error");
                await messageBox.ShowAsync();
                return;
            }
            if (regexFind.Text.Trim() == "")
            {
                messageBox = new MessageDialog("Please fill the find regex in the appropriate textbox.", "Error");
                await messageBox.ShowAsync();
                return;
            }
            if (textReplace.Text.Trim() == "")
            {
                messageBox = new MessageDialog("Please fill the replace text in the appropriate textbox.", "Error");
                await messageBox.ShowAsync();
                return;
            }

            if (checkBoxFirstWord.IsChecked.Value)
            {
                Syncfusion.Presentation.ITextSelection textSelection = null;
                if (comboBox1.SelectedIndex == 0)
                {
                    //Finds the first occurrence of a particular text.
                    textSelection = pptxDoc.Find(textFind.Text, checkBoxCase.IsChecked.Value, checkBoxWord.IsChecked.Value);
                }
                else
                {
                    Regex regex = new Regex(regexFind.Text);
                    //Finds the first occurrence of a particular text pattern.
                    textSelection = pptxDoc.Find(regex);
                }
                if (textSelection != null)
                {
                    //Gets the found text as single text part
                    ITextPart textPart = textSelection.GetAsOneTextPart();
                    //Replace the text
                    textPart.Text = textReplace.Text;
                }
            }
            else
            {
                Syncfusion.Presentation.ITextSelection[] textSelections = null;
                if (comboBox1.SelectedIndex == 0)
                {
                    //Finds all the occurrence of a particular text
                    textSelections = pptxDoc.FindAll(textFind.Text, checkBoxCase.IsChecked.Value, checkBoxWord.IsChecked.Value);
                }
                else
                {
                    Regex regex = new Regex(regexFind.Text);
                    //Finds all the occurrence of a particular text pattern
                    textSelections = pptxDoc.FindAll(regex);
                }

                if (textSelections != null)
                {
                    foreach (Syncfusion.Presentation.ITextSelection textSelection in textSelections)
                    {
                        //Gets the found text as single text part
                        ITextPart textPart = textSelection.GetAsOneTextPart();
                        //Replace the text
                        textPart.Text = textReplace.Text;
                    }
                }
            }
            SavePPTX(pptxDoc, "FindAndReplace");
        }
        /// <summary>
        /// Save as PPTX Format
        /// </summary>
        /// <param name="presentation"></param>
        private async void SavePPTX(IPresentation presentation, string suggestedFileName)
        {
            StorageFile stgFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as 
                savePicker.FileTypeChoices.Add("Presentation", new List<string>() { ".pptx" });
                // Default file name if the user does not type one in or select a file to replace 
                savePicker.SuggestedFileName = suggestedFileName;
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync(suggestedFileName+".pptx", CreationCollisionOption.ReplaceExisting);
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
        /// <summary>
        /// Modifies the required changes if combo box value changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxSelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            //If none author selected disable the radio button, otherwise enabled
            if (comboBox1.SelectedIndex != 0)
            {
                if (checkBoxCase != null || checkBoxFirstWord != null || checkBoxWord != null)
                {
                    checkBoxCase.IsChecked = false;
                    checkBoxFirstWord.IsChecked = false;
                    checkBoxWord.IsChecked = false;
                    checkBoxCase.IsEnabled = false;
                    checkBoxWord.IsEnabled = false;
                    checkBoxFirstWord.IsChecked = false;
                }
                if (regexFind != null && TextBlock10 != null)
                {
                    regexFind.Visibility = Visibility.Visible;
                    TextBlock11.Visibility = Visibility.Visible;
                    textFind.Visibility = Visibility.Collapsed;
                    TextBlock10.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (checkBoxCase != null || checkBoxFirstWord != null || checkBoxWord != null)
                {
                    checkBoxCase.IsChecked = false;
                    checkBoxFirstWord.IsChecked = false;
                    checkBoxWord.IsChecked = false;
                    checkBoxCase.IsEnabled = true;
                    checkBoxWord.IsEnabled = true;
                }
                if (regexFind != null && TextBlock11 != null)
                {
                    regexFind.Visibility = Visibility.Collapsed;
                    TextBlock11.Visibility = Visibility.Collapsed;
                    textFind.Visibility = Visibility.Visible;
                    TextBlock10.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
