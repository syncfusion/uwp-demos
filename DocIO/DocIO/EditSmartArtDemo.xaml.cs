using Common;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.Office;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
    public sealed partial class EditSmartArtDemo : SampleLayout
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

            viewTemplateButton.Click -= Button_Click_1;
            DisposeButton(viewTemplateButton);
            viewTemplateButton = null;

            generateDocumentButton.Click -= Button_Click_2;
            DisposeButton(generateDocumentButton);
            generateDocumentButton = null;


            EditSmartArt.ClearValue(Grid.BackgroundProperty);
            EditSmartArt.ClearValue(Grid.PaddingProperty);
            EditSmartArt.Children.Clear();
            EditSmartArt.ColumnDefinitions.Clear();
            EditSmartArt.RowDefinitions.Clear();
            EditSmartArt = null;
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
        public EditSmartArtDemo()
        {
            InitializeComponent();
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Assembly execAssm = typeof(EditSmartArtDemo).GetTypeInfo().Assembly;
            Stream inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.EditSmartArtInput.docx");
            MemoryStream stream = new MemoryStream();
            inputStream.CopyTo(stream);
            inputStream.Dispose();
            stream.Position = 0;
            ViewTemplate(stream);
        }
        private async void ViewTemplate(MemoryStream stream)
        {
            StorageFile stgFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as 
                savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".docx" });
                // Default file name if the user does not type one in or select a file to replace 
                savePicker.SuggestedFileName = "EditSmartArtInput";
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("EditSmartArtInput.docx", CreationCollisionOption.ReplaceExisting);
            }
            if (stgFile != null)
            {
                stream.Position = 0;
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stgFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();
                //Save as Docx Format
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
            Assembly execAssem = typeof(EditSmartArtDemo).GetTypeInfo().Assembly;
            //Load an existing Word document.
            WordDocument document = new WordDocument();
            Stream inputStream = execAssem.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.EditSmartArtInput.docx");
            // Open an existing template document.
            await document.OpenAsync(inputStream, FormatType.Docx);
            inputStream.Dispose();
            //Gets the last paragraph in the document.
            WParagraph paragraph = document.LastParagraph;
            //Retrieves the SmartArt object from the paragraph.
            WSmartArt smartArt = paragraph.ChildEntities[0] as WSmartArt;
            //Sets the background fill type of the SmartArt to solid.
            smartArt.Background.FillType = OfficeShapeFillType.Solid;
            //Sets the background color of the SmartArt.
            smartArt.Background.SolidFill.Color = Color.FromArgb(255, 242, 169, 132);
            //Gets the first node of the SmartArt.
            IOfficeSmartArtNode node = smartArt.Nodes[0];
            //Modifies the text content of the first node.
            node.TextBody.Text = "Goals";
            //Retrieves the first shape of the node.
            IOfficeSmartArtShape shape = node.Shapes[0];
            //Sets the fill color of the shape.
            shape.Fill.SolidFill.Color = Color.FromArgb(255, 160, 43, 147);
            //Sets the line format color of the shape.
            shape.LineFormat.Fill.SolidFill.Color = Color.FromArgb(255, 160, 43, 147);
            //Gets the first child node of the current node.
            IOfficeSmartArtNode childNode = node.ChildNodes[0];
            //Modifies the text content of the child node.
            childNode.TextBody.Text = "Set clear goals to the team.";
            //Sets the line format color of the first shape in the child node.
            childNode.Shapes[0].LineFormat.Fill.SolidFill.Color = Color.FromArgb(255, 160, 43, 147);

            //Retrieves the second node in the SmartArt and updates its text content.
            node = smartArt.Nodes[1];
            node.TextBody.Text = "Progress";

            //Retrieves the third node in the SmartArt and updates its text content.
            node = smartArt.Nodes[2];
            node.TextBody.Text = "Result";
            //Retrieves the first shape of the third node.
            shape = node.Shapes[0];
            //Sets the fill color of the shape.
            shape.Fill.SolidFill.Color = Color.FromArgb(255, 78, 167, 46);
            //Sets the line format color of the shape.
            shape.LineFormat.Fill.SolidFill.Color = Color.FromArgb(255, 78, 167, 46);
            //Sets the line format color of the first shape in the child node.
            node.ChildNodes[0].Shapes[0].LineFormat.Fill.SolidFill.Color = Color.FromArgb(255, 78, 167, 46);

            SaveDocx(document);
        }
        /// <summary>
        /// Save as Docx Format
        /// </summary>
        /// <param name="doc"></param>
        private async void SaveDocx(WordDocument doc)
        {
            StorageFile stgFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as 
                savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".docx" });
                // Default file name if the user does not type one in or select a file to replace 
                savePicker.SuggestedFileName = "EditSmartArt";
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("WordDocument.docx", CreationCollisionOption.ReplaceExisting);
            }
            if (stgFile != null)
            {
                MemoryStream stream = new MemoryStream();
                await doc.SaveAsync(stream, FormatType.Docx);
                doc.Close();
                stream.Position = 0;
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stgFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();

                //Save as Docx Format

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
}
