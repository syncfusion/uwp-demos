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
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialDocIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AutoShapesDemo : SampleLayout
    {
        public AutoShapesDemo()
        {
            InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                TextBlock4.Visibility = Visibility.Collapsed;
            }
        }
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

            Button1.Click -= Button_Click_1;
            DisposeButton(Button1);
            Button1 = null;

            AutoShapes.ClearValue(Grid.BackgroundProperty);
            AutoShapes.ClearValue(Grid.PaddingProperty);
            AutoShapes.Children.Clear();
            AutoShapes.ColumnDefinitions.Clear();
            AutoShapes.RowDefinitions.Clear();
            AutoShapes = null;
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
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Initialize Word document
            WordDocument doc = new WordDocument();
            //Ensure Minimum
            doc.EnsureMinimal();
            //Append AutoShape
            Shape shape = doc.LastParagraph.AppendShape(AutoShapeType.RoundedRectangle, 130, 45);
            //Set horizontal alignment
            shape.HorizontalAlignment = ShapeHorizontalAlignment.Center;
            //Set horizontal origin
            shape.HorizontalOrigin = HorizontalOrigin.Page;
            //Set vertical origin
            shape.VerticalOrigin = VerticalOrigin.Page;
            //Set vertical position
            shape.VerticalPosition = 50;
            //Set AllowOverlap to true for overlapping shapes
            shape.WrapFormat.AllowOverlap = true;
            //Set Fill Color
            shape.FillFormat.Color = Color.Blue;
            //Set Content vertical alignment
            shape.TextFrame.TextVerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            //Add Texbody contents to Shape
            IWParagraph para = shape.TextBody.AddParagraph();
            para.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            para.AppendText("Requirement").ApplyCharacterFormat(new WCharacterFormat(doc) { Bold = true, TextColor = Color.White, FontSize = 12, FontName = "Verdana" });

            shape = doc.LastParagraph.AppendShape(AutoShapeType.DownArrow, 45, 45);
            shape.HorizontalAlignment = ShapeHorizontalAlignment.Center;
            shape.HorizontalOrigin = HorizontalOrigin.Page;
            shape.VerticalOrigin = VerticalOrigin.Page;
            shape.VerticalPosition = 95;
            shape.WrapFormat.AllowOverlap = true;

            shape = doc.LastParagraph.AppendShape(AutoShapeType.RoundedRectangle, 130, 45);
            shape.HorizontalAlignment = ShapeHorizontalAlignment.Center;
            shape.HorizontalOrigin = HorizontalOrigin.Page;
            shape.VerticalOrigin = VerticalOrigin.Page;
            shape.VerticalPosition = 140;
            shape.WrapFormat.AllowOverlap = true;
            shape.FillFormat.Color = Color.Orange;
            shape.TextFrame.TextVerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            para = shape.TextBody.AddParagraph();
            para.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            para.AppendText("Design").ApplyCharacterFormat(new WCharacterFormat(doc) { Bold = true, TextColor = Color.White, FontSize = 12, FontName = "Verdana" });

            shape = doc.LastParagraph.AppendShape(AutoShapeType.DownArrow, 45, 45);
            shape.HorizontalAlignment = ShapeHorizontalAlignment.Center;
            shape.HorizontalOrigin = HorizontalOrigin.Page;
            shape.VerticalOrigin = VerticalOrigin.Page;
            shape.VerticalPosition = 185;
            shape.WrapFormat.AllowOverlap = true;

            shape = doc.LastParagraph.AppendShape(AutoShapeType.RoundedRectangle, 130, 45);
            shape.HorizontalAlignment = ShapeHorizontalAlignment.Center;
            shape.HorizontalOrigin = HorizontalOrigin.Page;
            shape.VerticalOrigin = VerticalOrigin.Page;
            shape.VerticalPosition = 230;
            shape.WrapFormat.AllowOverlap = true;
            shape.FillFormat.Color = Color.Blue;
            shape.TextFrame.TextVerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            para = shape.TextBody.AddParagraph();
            para.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            para.AppendText("Execution").ApplyCharacterFormat(new WCharacterFormat(doc) { Bold = true, TextColor = Color.White, FontSize = 12, FontName = "Verdana" });

            shape = doc.LastParagraph.AppendShape(AutoShapeType.DownArrow, 45, 45);
            shape.HorizontalAlignment = ShapeHorizontalAlignment.Center;
            shape.HorizontalOrigin = HorizontalOrigin.Page;
            shape.VerticalOrigin = VerticalOrigin.Page;
            shape.VerticalPosition = 275;
            shape.WrapFormat.AllowOverlap = true;

            shape = doc.LastParagraph.AppendShape(AutoShapeType.RoundedRectangle, 130, 45);
            shape.HorizontalAlignment = ShapeHorizontalAlignment.Center;
            shape.HorizontalOrigin = HorizontalOrigin.Page;
            shape.VerticalOrigin = VerticalOrigin.Page;
            shape.VerticalPosition = 320;
            shape.WrapFormat.AllowOverlap = true;
            shape.FillFormat.Color = Color.Violet;
            shape.TextFrame.TextVerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            para = shape.TextBody.AddParagraph();
            para.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            para.AppendText("Testing").ApplyCharacterFormat(new WCharacterFormat(doc) { Bold = true, TextColor = Color.White, FontSize = 12, FontName = "Verdana" });

            shape = doc.LastParagraph.AppendShape(AutoShapeType.DownArrow, 45, 45);
            shape.HorizontalAlignment = ShapeHorizontalAlignment.Center;
            shape.HorizontalOrigin = HorizontalOrigin.Page;
            shape.VerticalOrigin = VerticalOrigin.Page;
            shape.VerticalPosition = 365;
            shape.WrapFormat.AllowOverlap = true;

            shape = doc.LastParagraph.AppendShape(AutoShapeType.RoundedRectangle, 130, 45);
            shape.HorizontalAlignment = ShapeHorizontalAlignment.Center;
            shape.HorizontalOrigin = HorizontalOrigin.Page;
            shape.VerticalOrigin = VerticalOrigin.Page;
            shape.VerticalPosition = 410;
            shape.WrapFormat.AllowOverlap = true;
            shape.FillFormat.Color = Color.PaleVioletRed;
            shape.TextFrame.TextVerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            para = shape.TextBody.AddParagraph();
            para.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            para.AppendText("Release").ApplyCharacterFormat(new WCharacterFormat(doc) { Bold = true, TextColor = Color.White, FontSize = 12, FontName = "Verdana" });
            SaveDocx(doc);
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
                savePicker.SuggestedFileName = "Sample";
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
