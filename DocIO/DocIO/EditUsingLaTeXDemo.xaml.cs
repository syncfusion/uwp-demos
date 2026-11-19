using Common;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
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
    public sealed partial class EditUsingLaTeXDemo : SampleLayout
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
            

            Button1.Click -= Button_Click_1;
            DisposeButton(Button1);
            Button1 = null;

            Button2.Click -= Button_Click_2;
            DisposeButton(Button2);
            Button2 = null;

            EditUsingLaTeX.ClearValue(Grid.BackgroundProperty);
            EditUsingLaTeX.ClearValue(Grid.PaddingProperty);
            EditUsingLaTeX.Children.Clear();
            EditUsingLaTeX.ColumnDefinitions.Clear();
            EditUsingLaTeX.RowDefinitions.Clear();
            EditUsingLaTeX = null;
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
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Assembly execAssm = typeof(EditUsingLaTeXDemo).GetTypeInfo().Assembly;
            Stream inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.EditEquationLaTeXInput.docx");
            MemoryStream stream = new MemoryStream();
            inputStream.CopyTo(stream);
            inputStream.Dispose();
            stream.Position = 0;
            ViewTemplate(stream);
        }
        public EditUsingLaTeXDemo()
        {
            InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                EditUsingLaTeX.Margin = new Thickness(10, 10, 10, 10);
                TextBlock5.Visibility = Visibility.Collapsed;
                TextBlock6.Visibility = Visibility.Collapsed;
                TextBlock4.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBlock6.Visibility = Visibility.Visible;
                TextBlock5.Visibility = Visibility.Visible;
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Assembly execAssem = typeof(EditUsingLaTeXDemo).GetTypeInfo().Assembly;
            //Load an existing Word document.
            WordDocument document = new WordDocument();
            Stream inputStream = execAssem.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.EditEquationLaTeXInput.docx");
            // Open an existing template document.
            await document.OpenAsync(inputStream, FormatType.Docx);
            inputStream.Dispose();
            //Find all the equations in the Word document.
            List<Entity> entities = document.FindAllItemsByProperty(EntityType.Math, null, null);

            //Iterate through each equation in the Word document.
            foreach (Entity entity in entities)
            {
                WMath math = entity as WMath;
                //Get the laTeX code of equation.
                string laTeX = math.MathParagraph.LaTeX;

                //Modify the laTeX code of derivative equation.
                if (laTeX.StartsWith("\\frac"))
                    laTeX = laTeX.Replace("n", "k");
                //Modify the laTeX code of expansion of the sum equation.
                else if (laTeX.StartsWith("{\\left"))
                    laTeX = laTeX.Replace("n", "k");
                //Modify the laTeX code of quadratic equation.
                else if (laTeX.StartsWith("x=\\frac"))
                    laTeX = laTeX.Replace("x", "y");

                //Update the modified laTeX code to the equation.
                math.MathParagraph.LaTeX = laTeX;
            }
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
                savePicker.SuggestedFileName = "EditEquationLaTeX";
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("EditEquationLaTeX.docx", CreationCollisionOption.ReplaceExisting);
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
                savePicker.SuggestedFileName = "EditEquationLaTeXInput";
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("EditEquationLaTeXInput.docx", CreationCollisionOption.ReplaceExisting);
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
    }
}
