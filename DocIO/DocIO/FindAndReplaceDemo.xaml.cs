using Common;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialDocIO
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
            DisposeTextBlock(TextBlock4);
            TextBlock4 = null;
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
            DisposeTextBlock(TextBlock16);
            TextBlock16 = null;
            DisposeTextBlock(TextBlock17);
            TextBlock17 = null;
            DisposeTextBlock(TextBlock18);
            TextBlock18 = null;

            DisposeTextBox(InputFile1);
            InputFile1 = null;
            DisposeTextBox(FindTextBox);
            FindTextBox = null;
            DisposeTextBox(ReplaceTextBox);
            ReplaceTextBox = null;

            DisposeCheckBox(chkBoxMatchCase);
            chkBoxMatchCase = null;
            DisposeCheckBox(chkBoxWholeWord);
            chkBoxWholeWord = null;

            Button1.Click -= Button_Click_3;
            DisposeButton(Button1);
            Button1 = null;
            generateButton.Click -= Button_Click_1;
            DisposeButton(generateButton);
            generateButton = null;

            DisposeRadioButton(rdDoc);
            rdDoc = null;
            DisposeRadioButton(rdDocx);
            rdDocx = null;

            stackPnlOptions.ClearValue(StackPanel.OrientationProperty);
            stackPnlOptions.ClearValue(StackPanel.HorizontalAlignmentProperty);
            stackPnlOptions = null;

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
        public void DisposeRadioButton(RadioButton radioButton)
        {
            radioButton.ClearValue(RadioButton.GroupNameProperty);
            radioButton.ClearValue(RadioButton.ContentProperty);
            radioButton.ClearValue(RadioButton.FontFamilyProperty);
            radioButton.ClearValue(RadioButton.FontSizeProperty);
            radioButton.ClearValue(RadioButton.ForegroundProperty);
            radioButton.ClearValue(RadioButton.WidthProperty);
            radioButton.ClearValue(RadioButton.IsCheckedProperty);
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
        }
        StorageFile file;
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WordDocument document = new WordDocument();
            //Open an existing word document.
            await document.OpenAsync(file);
            if (FindTextBox.Text.Trim() == string.Empty || ReplaceTextBox.Text.Trim() == string.Empty)
            {
                MessageDialog msgDialog = new MessageDialog("Please fill the find and replacement text in appropriate textboxes...");
                UICommand okCmd = new UICommand("Ok");
                msgDialog.Commands.Add(okCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
            }
            else
            {
                document.Replace(FindTextBox.Text, ReplaceTextBox.Text, chkBoxMatchCase.IsChecked.Value, chkBoxWholeWord.IsChecked.Value);
                StorageFile stgFile = null;
                if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
                {
                    FileSavePicker savePicker = new FileSavePicker();
                    savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                    savePicker.SuggestedFileName = "Sample";

                    if (rdDoc.IsChecked.Value)
                    {
                        savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".doc" });
                        stgFile = await savePicker.PickSaveFileAsync();
                        if (stgFile != null)
                            await document.SaveAsync(stgFile, FormatType.Doc);
                    }
                    else
                    {
                        savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".docx" });
                        stgFile = await savePicker.PickSaveFileAsync();
                        if (stgFile != null)
                        {
                            await document.SaveAsync(stgFile, FormatType.Docx);
                        }
                    }
                }
                else
                {
                    StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                    stgFile = await local.CreateFileAsync("WordDocument.docx", CreationCollisionOption.ReplaceExisting);
                    if (stgFile != null)
                    {
                        await document.SaveAsync(stgFile, FormatType.Docx);
                    }
                }
                if (stgFile != null)
                {
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
            }
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".doc");
            openPicker.FileTypeFilter.Add(".docx");
            StorageFile tempFile = await openPicker.PickSingleFileAsync();
            if (tempFile != null)
                file = tempFile;
            if (file == null)
                generateButton.IsEnabled = false;
            else
             {
                 // Application now has read/write access to the picked file
                 InputFile1.Text = file.Name;
                 generateButton.IsEnabled = true;
             }
        }
    }
}
