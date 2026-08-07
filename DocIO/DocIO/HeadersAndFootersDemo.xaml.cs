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
    public sealed partial class HeadersAndFootersDemo : SampleLayout
    {
        public HeadersAndFootersDemo()
        {
            InitializeComponent();

            //Associate PageNumberStyle Enum values to comboBox
            foreach (String str in Enum.GetNames(typeof(PageNumberStyle)))
                comboBox1.Items.Add(str);
            comboBox1.SelectedIndex = 2;
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

            Button1.Click -= Button_Click_1;
            DisposeButton(Button1);
            Button1 = null;

            DisposeRadioButton(rdDoc);
            rdDoc = null;
            DisposeRadioButton(rdDocx);
            rdDocx = null;

            DisposeCheckBox(checkBox1);
            checkBox1 = null;
            DisposeCheckBox(checkBox2);
            checkBox2 = null;

            comboBox1.ClearValue(ComboBox.FontFamilyProperty);
            comboBox1.ClearValue(ComboBox.FontSizeProperty);
            comboBox1.ClearValue(ComboBox.ForegroundProperty);
            comboBox1 = null;

            stackPnlOptions.ClearValue(StackPanel.OrientationProperty);
            stackPnlOptions.ClearValue(StackPanel.HorizontalAlignmentProperty);
            stackPnlOptions = null;

            HeaderFooter.ClearValue(Grid.BackgroundProperty);
            HeaderFooter.ClearValue(Grid.PaddingProperty);
            HeaderFooter.Children.Clear();
            HeaderFooter.ColumnDefinitions.Clear();
            HeaderFooter.RowDefinitions.Clear();
            HeaderFooter = null;
        }
        public void DisposeCheckBox(CheckBox checkBox)
        {
            checkBox.ClearValue(CheckBox.FontFamilyProperty);
            checkBox.ClearValue(CheckBox.FontSizeProperty);
            checkBox.ClearValue(CheckBox.FontSizeProperty);
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
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Creating a new document.
            WordDocument doc = new WordDocument();

            // Add a new section to the document.
            IWSection section1 = doc.AddSection();

            Assembly execAssm = typeof(HeadersAndFootersDemo).GetTypeInfo().Assembly;
            #region Header Footer
            //Add different Header Footer for first and other pages
            if (checkBox1.IsChecked == true && checkBox2.IsChecked == true)
            {
                // Set the header/footer setup.
                section1.PageSetup.DifferentFirstPage = true;
                // Inserting Header Footer to first page
                InsertFirstPageHeaderFooter(doc, section1);
                // Inserting Header Footer to all pages
                InsertPageHeaderFooter(doc, section1);
            }
            //Add Header Footer only for first page
            if (checkBox1.IsChecked == true && checkBox2.IsChecked != true)
            {
                // Set the header/footer setup.
                section1.PageSetup.DifferentFirstPage = true;
                // Inserting Header Footer to first page
                InsertFirstPageHeaderFooter(doc, section1);
            }
            //Add same Header Footer for all the pages
            if (checkBox2.IsChecked == true && checkBox1.IsChecked != true)
            {
                // Inserting Header Footer to all pages
                InsertPageHeaderFooter(doc, section1);
            }
            #endregion

            // Add text to the document body section.
            IWParagraph par;
            par = section1.AddParagraph();
            //Insert Text into the word Document.
            Stream inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.WinFAQ.txt");
            StreamReader reader = new StreamReader(inputStream, System.Text.Encoding.Unicode);
            string text = reader.ReadToEnd();
            par.AppendText(text);
            Save(rdDoc.IsChecked == true, doc);
        }
        private async void Save(bool isDocFormat, WordDocument document)
        {
            StorageFile stgFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as 
                if (isDocFormat)
                    savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".doc" });
                else
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
                if (isDocFormat)
                    await document.SaveAsync(stream, FormatType.Doc);
                else
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
        #region Helper Methods

        #region InsertFirstPageHeaderFooter
        private void InsertFirstPageHeaderFooter(WordDocument doc, IWSection section)
        {
            Assembly execAssm = typeof(HeadersAndFootersDemo).GetTypeInfo().Assembly;

            Stream inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Northwind_logo.png");

            // Add a new paragraph for header to the document.
            IWParagraph headerPar = new WParagraph(doc);

            // Add a new table to the header.
            IWTable table = section.HeadersFooters.FirstPageHeader.AddTable();

            RowFormat format = new RowFormat();

            // Setting cleared table border style.
            format.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Cleared;

            // Inserting table with a row and two columns.
            table.ResetCells(1, 2, format, 265);

            // Inserting logo image to the table first cell.
            headerPar = table[0, 0].AddParagraph() as WParagraph;
            headerPar.AppendPicture(inputStream);
            //Set Image size
            (headerPar.Items[0] as WPicture).Width = 232.5f;
            (headerPar.Items[0] as WPicture).Height = 54.75f;
            // Inserting text to the table second cell.
            headerPar = table[0, 1].AddParagraph() as WParagraph;
            IWTextRange txt = headerPar.AppendText("Company Headquarters,\n2501 Aerial Center Parkway,\nSuite 110, Morrisville, NC 27560,\nTEL 1-888-936-8638.");
            txt.CharacterFormat.FontSize = 12;
            txt.CharacterFormat.CharacterSpacing = 1.7f;
            headerPar.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;
            // Add a new paragraph to the header with address text.
            headerPar = new WParagraph(doc);
            headerPar.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            txt = headerPar.AppendText("\nFirst Page Header");
            txt.CharacterFormat.CharacterSpacing = 1.7f;
            section.HeadersFooters.FirstPageHeader.Paragraphs.Add(headerPar);

            // Add a footer paragraph text to the document.
            WParagraph footerPar = new WParagraph(doc);
            footerPar.ParagraphFormat.Tabs.AddTab(523f, TabJustification.Right, TabLeader.NoLeader);
            // Add text.
            footerPar.AppendText("Copyright Northwind Inc. 2001 - 2015");
            // Add page and Number of pages field to the document.
            footerPar.AppendText("\tFirst Page ");
            footerPar.AppendField("Page", FieldType.FieldPage);
            section.HeadersFooters.FirstPageFooter.Paragraphs.Add(footerPar);
            #region Page Number Settings
            section.PageSetup.RestartPageNumbering = true;
            section.PageSetup.PageStartingNumber = Convert.ToInt32(this.numericUpDown1.Value);
            section.PageSetup.PageNumberStyle = (PageNumberStyle)Enum.Parse(typeof(PageNumberStyle), this.comboBox1.SelectedItem.ToString(), true);
            #endregion
        }

        #endregion

        #region InsertPageHeaderFooter
        private void InsertPageHeaderFooter(WordDocument doc, IWSection section1)
        {
            Assembly execAssm = typeof(HeadersAndFootersDemo).GetTypeInfo().Assembly;

            Stream inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Northwind_logo.png");


            // Add a new paragraph for header to the document.
            IWParagraph headerPar = new WParagraph(doc);

            // Add a new table to the header
            IWTable table = section1.HeadersFooters.Header.AddTable();

            RowFormat format = new RowFormat();

            // Setting Single table border style.
            format.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

            // Inserting table with a row and two columns.
            table.ResetCells(1, 2, format, 265);

            // Inserting logo image to the table first cell.
            headerPar = table[0, 0].AddParagraph() as WParagraph;
            headerPar.AppendPicture(inputStream);
            //Set Image size.
            (headerPar.Items[0] as WPicture).Width = 232.5f;
            (headerPar.Items[0] as WPicture).Height = 54.75f;
            // Inserting text to the table second cell.
            headerPar = table[0, 1].AddParagraph() as WParagraph;
            IWTextRange txt = headerPar.AppendText("Company Headquarters,\n2501 Aerial Center Parkway,\nSuite 110, Morrisville, NC 27560,\nTEL 1-888-936-8638.");
            txt.CharacterFormat.FontSize = 12;
            txt.CharacterFormat.CharacterSpacing = 1.7f;
            headerPar.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

            // Add a footer paragraph text to the document.
            WParagraph footerPar = new WParagraph(doc);
            footerPar.ParagraphFormat.Tabs.AddTab(523f, TabJustification.Right, TabLeader.NoLeader);
            // Add text.
            footerPar.AppendText("Copyright Northwind Inc. 2001 - 2015");
            // Add page and Number of pages field to the document.
            footerPar.AppendText("\tPage ");
            IWField ff = footerPar.AppendField("Page", FieldType.FieldPage);

            section1.HeadersFooters.Footer.Paragraphs.Add(footerPar);

            #region Page Number Settings
            section1.PageSetup.RestartPageNumbering = true;
            section1.PageSetup.PageStartingNumber = Convert.ToInt32(this.numericUpDown1.Value);
            section1.PageSetup.PageNumberStyle = (PageNumberStyle)Enum.Parse(typeof(PageNumberStyle), this.comboBox1.SelectedItem.ToString(), true);
            #endregion

        }



        #endregion


        #endregion
    }
}
