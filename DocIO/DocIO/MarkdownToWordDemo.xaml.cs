#region Copyright Syncfusion Inc. 2001 - 2016
// Copyright Syncfusion Inc. 2001 - 2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using System.Reflection;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialDocIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MarkdownToWordDemo : SampleLayout
    {
        #region Fields
        Assembly execAssm = typeof(MarkdownToWordDemo).GetTypeInfo().Assembly;
        #endregion

        public MarkdownToWordDemo()
        {
            this.InitializeComponent();
        }
        Stream documentStream = null;
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
            DisposeTextBlock(WinRTText1);
            WinRTText1 = null;
            DisposeTextBlock(WinRTText2);
            WinRTText2 = null;

            Button1.Click -= Button_Click_1;
            DisposeButton(Button1);
            Button1 = null;

            Button2.Click -= Button_Click_2;
            DisposeButton(Button2);
            Button2 = null;

            MarkdownToWord.ClearValue(Grid.BackgroundProperty);
            MarkdownToWord.ClearValue(Grid.PaddingProperty);
            MarkdownToWord.Children.Clear();
            MarkdownToWord.ColumnDefinitions.Clear();
            MarkdownToWord.RowDefinitions.Clear();
            MarkdownToWord = null;
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
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           if(documentStream == null)
           {
              documentStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.MarkdownToWord.md");
           }
           //Creates Word document instance
           WordDocument document = new WordDocument(documentStream, FormatType.Markdown);
           Save(document);
        }
        private async void Save(WordDocument document)
        {
            StorageFile stgFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Default file name if the user does not type one in or select a file to replace 
                savePicker.SuggestedFileName = "MarkdownToWord";
                savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".docx" });
                stgFile = await savePicker.PickSaveFileAsync();
                if (stgFile != null)
                    await document.SaveAsync(stgFile, FormatType.Docx);

            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("Sample.docx", CreationCollisionOption.ReplaceExisting);
            }
            if (stgFile != null)
            {
                MemoryStream stream = new MemoryStream();
                await document.SaveAsync(stream, FormatType.Docx);
                document.Close();
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
        }
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".md");
            StorageFile tempFile = await openPicker.PickSingleFileAsync();
            if (tempFile != null)
            { 
                InputFile1.Text = tempFile.Name;
                Windows.Storage.Streams.IRandomAccessStream fileStream = await tempFile.OpenAsync(FileAccessMode.ReadWrite);
                documentStream = fileStream.AsStreamForWrite();
            }
        }

    }
}
