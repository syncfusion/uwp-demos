using Common;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml;
using Syncfusion.DocIO;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Popups;
using System.Reflection;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialDocIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TableFormattingDemo : SampleLayout
    {
        public TableFormattingDemo()
        {
            InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                TableFormatting.Margin = new Thickness(10, 10, 10, 10);
                stackPnlOptions.Visibility = Visibility.Collapsed;
                saveas.Visibility = Visibility.Collapsed;
                text1.Visibility = Visibility.Collapsed;
                text2.Visibility = Visibility.Collapsed;
                WinRTText2.Visibility = Visibility.Collapsed;
            }
            else
            {
                stackPnlOptions.Visibility = Visibility.Visible;
                saveas.Visibility = Visibility.Visible;
                text1.Visibility = Visibility.Visible;
                text2.Visibility = Visibility.Visible;
                text3.Visibility = Visibility.Visible;
                text3.Visibility = Visibility.Visible;
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
            DisposeTextBlock(WinRTText1);
            WinRTText1 = null;
            DisposeTextBlock(WinRTText2);
            WinRTText2 = null;
            DisposeTextBlock(text1);
            text1 = null;
            DisposeTextBlock(text2);
            text2 = null;
            DisposeTextBlock(text3);
            text3 = null;
            DisposeTextBlock(text4);
            text4 = null;
            DisposeTextBlock(saveas);
            saveas = null;

            Button1.Click -= Button_Click_1;
            DisposeButton(Button1);
            Button1 = null;

            DisposeRadioButton(rdDoc);
            rdDoc = null;
            DisposeRadioButton(rdDocx);
            rdDocx = null;

            stackPnlOptions.ClearValue(StackPanel.OrientationProperty);
            stackPnlOptions.ClearValue(StackPanel.HorizontalAlignmentProperty);
            stackPnlOptions = null;

            TableFormatting.ClearValue(Grid.BackgroundProperty);
            TableFormatting.ClearValue(Grid.PaddingProperty);
            TableFormatting.Children.Clear();
            TableFormatting.ColumnDefinitions.Clear();
            TableFormatting.RowDefinitions.Clear();
            TableFormatting = null;
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
            // Create a new document.
            WordDocument document = new WordDocument();

            // Adding a new section to the document.
            IWSection section = document.AddSection();
            section.PageSetup.DifferentFirstPage = true;
            IWTextRange textRange;
            IWParagraph paragraph = section.AddParagraph();

            // --------------------------------------------
            // Table in page header 
            // --------------------------------------------


            IWParagraph hParagraph = new WParagraph(document);
            hParagraph.AppendText("Header text\r\n").CharacterFormat.FontSize = 14;

            section.HeadersFooters.FirstPageHeader.Paragraphs.Add(hParagraph);


            IWTable hTable = document.LastSection.HeadersFooters.FirstPageHeader.AddTable();

            hTable.ResetCells(2, 2);

            hTable[0, 0].AddParagraph().AppendText("1");
            hTable[0, 1].AddParagraph().AppendText("2");
            hTable[1, 0].AddParagraph().AppendText("3");
            hTable[1, 1].AddParagraph().AppendText("4");


            // --------------------------------------------
            // Tiny table
            // --------------------------------------------

            paragraph = section.AddParagraph();

            paragraph.AppendText("Tiny table\r\n").CharacterFormat.FontSize = 14;
            paragraph = section.AddParagraph();
            WTextBody textBody = section.Body;
            IWTable table = textBody.AddTable();
            table.ResetCells(2, 2);

            WTableRow row_0 = table.Rows[0];
            row_0.Cells[0].AddParagraph().AppendText("A");
            row_0.Cells[0].AddParagraph().AppendText("AA");
            row_0.Cells[0].AddParagraph().AppendText("AAA");

            WTableRow row_1 = table.Rows[1];
            row_1.Cells[1].AddParagraph().AppendText("B");
            row_1.Cells[1].AddParagraph().AppendText("BB\r\nBBB");
            row_1.Cells[1].AddParagraph().AppendText("BBB");

            textBody.AddParagraph().AppendText("Text after table...").CharacterFormat.FontSize = 14;

            // --------------------------------------------
            // Table with different formatting 
            // --------------------------------------------

            section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.AppendText("Table with different formatting\r\n").CharacterFormat.FontSize = 14;
            paragraph = section.AddParagraph();
            textBody = section.Body;
            table = textBody.AddTable();
            table.ResetCells(3, 3);

            /* ------- First Row -------- */

            WTableRow row0 = table.Rows[0];

            paragraph = (IWParagraph)row0.Cells[0].AddParagraph();
            textRange = paragraph.AppendText("1");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontSize = 14f;
            row0.Cells[0].CellFormat.Borders.LineWidth = 2f;
            row0.Cells[0].CellFormat.Borders.Color = Color.Magenta;

            paragraph = (IWParagraph)row0.Cells[1].AddParagraph();
            textRange = paragraph.AppendText("2");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;
            textRange.CharacterFormat.Emboss = true;
            textRange.CharacterFormat.FontSize = 15f;
            row0.Cells[1].CellFormat.Borders.LineWidth = 1.3f;
            row0.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.DoubleWave;

            paragraph = (IWParagraph)row0.Cells[2].AddParagraph();
            textRange = paragraph.AppendText("3");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            textRange.CharacterFormat.Engrave = true;
            textRange.CharacterFormat.FontSize = 15f;
            row0.Cells[2].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Emboss3D;

            /* ------- Second Row -------- */

            WTableRow row1 = table.Rows[1];

            paragraph = (IWParagraph)row1.Cells[0].AddParagraph();
            textRange = paragraph.AppendText("4");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            textRange.CharacterFormat.SmallCaps = true;
            textRange.CharacterFormat.FontName = "Comic Sans MS";
            textRange.CharacterFormat.FontSize = 16;
            row1.Cells[0].CellFormat.Borders.LineWidth = 2f;
            row1.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.DashDotStroker;

            paragraph = (IWParagraph)row1.Cells[1].AddParagraph();
            textRange = paragraph.AppendText("5");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.Shadow = true;
            textRange.CharacterFormat.TextBackgroundColor = Color.Orange;
            textRange.CharacterFormat.FontSize = 15f;
            row1.Cells[1].CellFormat.Borders.LineWidth = 2f;
            row1.Cells[1].CellFormat.Borders.Color = Color.Brown;

            paragraph = (IWParagraph)row1.Cells[2].AddParagraph();
            textRange = paragraph.AppendText("6");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontSize = 14f;
            row1.Cells[2].CellFormat.BackColor = Color.FromArgb(51, 51, 101);
            row1.Cells[2].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;

            /* ------- Third Row -------- */

            WTableRow row2 = table.Rows[2];

            paragraph = (IWParagraph)row2.Cells[0].AddParagraph();
            textRange = paragraph.AppendText("7");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;
            textRange.CharacterFormat.FontSize = 13f;
            row2.Cells[0].CellFormat.Borders.LineWidth = 1.5f;
            row2.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.DashLargeGap;

            paragraph = (IWParagraph)row2.Cells[1].AddParagraph();
            textRange = paragraph.AppendText("8");
            textRange.CharacterFormat.TextColor = Color.Blue;
            textRange.CharacterFormat.FontSize = 16f;
            row2.Cells[1].CellFormat.Borders.LineWidth = 3f;
            row2.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Wave;

            paragraph = (IWParagraph)row2.Cells[2].AddParagraph();
            textRange = paragraph.AppendText("9");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;
            row2.Cells[2].CellFormat.Borders.LineWidth = 2f;
            row2.Cells[2].CellFormat.Borders.Color = Color.Cyan;
            row2.Cells[2].CellFormat.Borders.Shadow = true;
            row2.Cells[2].CellFormat.Borders.Space = 20;

            // --------------------------------------------
            // Table Cell Merging.
            // --------------------------------------------

            section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.AppendText("Table Cell Merging...").CharacterFormat.FontSize = 14;
            section.AddParagraph();
            paragraph = section.AddParagraph();
            textBody = section.Body;

            // Adding a new Table to the textbody.
            table = textBody.AddTable();

            RowFormat format = new RowFormat();
            format.Paddings.All = 5;
            format.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Dot;
            format.Borders.LineWidth = 2;

            // Inserting rows to the table.
            table.ResetCells(6, 6, format, 80);

            // Table formatting with cell merging.
            table.Rows[0].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[0].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            table.Rows[0].Cells[0].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            table.Rows[0].Cells[0].CellFormat.BackColor = Color.FromArgb(218, 230, 246);
            IWParagraph par = table.Rows[0].Cells[0].AddParagraph();
            par.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            par.AppendText("Horizontal Merge").CharacterFormat.Bold = true;


            table.Rows[2].Cells[3].CellFormat.VerticalMerge = CellMerge.Start;
            table.Rows[3].Cells[3].CellFormat.VerticalMerge = CellMerge.Continue;

            table.Rows[2].Cells[3].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            par = table.Rows[2].Cells[3].AddParagraph();
            table.Rows[2].Cells[3].CellFormat.BackColor = Color.FromArgb(252, 172, 85);
            par.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            par.AppendText("Vertical Merge").CharacterFormat.Bold = true;

            #region Table Cell Spacing.
            // --------------------------------------------
            // Table Cell Spacing.
            // --------------------------------------------

            section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.AppendText("Table Cell spacing...").CharacterFormat.FontSize = 14;

            section.AddParagraph();
            paragraph = section.AddParagraph();
            textBody = section.Body;

            // Adding a new Table to the textbody.
            table = textBody.AddTable();

            format = new RowFormat();

            format.CellSpacing = 5;
            format.Paddings.All = 5;
            format.CellSpacing = 2.5f;
            format.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.DotDash;
            format.IsBreakAcrossPages = true;
            table.ResetCells(25, 5, format, 100);
            IWTextRange text;
            table.Rows[0].IsHeader = true;

            for (int i = 0; i < table.Rows[0].Cells.Count; i++)
            {
                paragraph = table[0, i].AddParagraph() as WParagraph;
                paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                text = paragraph.AppendText(string.Format("Header {0}", i + 1));
                text.CharacterFormat.Font = new Font("Bitstream Vera Serif", 10);
                text.CharacterFormat.Bold = true;
                text.CharacterFormat.TextColor = Color.FromArgb(0, 21, 84);
                table[0, i].CellFormat.BackColor = Color.FromArgb(203, 211, 226);
            }

            for (int i = 1; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    paragraph = table[i, j].AddParagraph() as WParagraph;
                    paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                    text = paragraph.AppendText(string.Format("Cell {0} , {1}", i, j + 1));
                    text.CharacterFormat.TextColor = Color.FromArgb(242, 151, 50);
                    text.CharacterFormat.Bold = true;
                    if (i % 2 != 1)
                        table[i, j].CellFormat.BackColor = Color.FromArgb(231, 235, 245);
                    else
                        table[i, j].CellFormat.BackColor = Color.FromArgb(246, 249, 255);

                }

            }
            #endregion Table Cell Spacing.

            #region Nested Table
            // --------------------------------------------
            // Nested Table.
            // --------------------------------------------

            section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.PageBreakBefore = true;
            paragraph.AppendText("Nested Table...").CharacterFormat.FontSize = 14;

            section.AddParagraph();
            paragraph = section.AddParagraph();
            textBody = section.Body;

            // Adding a new Table to the textbody.
            table = textBody.AddTable();

            format = new RowFormat();
            format.Paddings.All = 5;
            format.CellSpacing = 2.5f;
            format.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.DotDash;
            table.ResetCells(5, 3, format, 100);


            for (int i = 0; i < table.Rows[0].Cells.Count; i++)
            {
                paragraph = table[0, i].AddParagraph() as WParagraph;
                paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                text = paragraph.AppendText(string.Format("Header {0}", i + 1));
                text.CharacterFormat.Font = new Font("Bitstream Vera Serif", 10);
                text.CharacterFormat.Bold = true;
                text.CharacterFormat.TextColor = Color.FromArgb(0, 21, 84);
                table[0, i].CellFormat.BackColor = Color.FromArgb(242, 151, 50);
            }
            table[0, 2].Width = 200;
            for (int i = 1; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    paragraph = table[i, j].AddParagraph() as WParagraph;
                    paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                    if ((i == 2) && (j == 2))
                    {
                        text = paragraph.AppendText("Nested Table");
                    }

                    else
                    {
                        text = paragraph.AppendText(string.Format("Cell {0} , {1}", i, j + 1));
                    }

                    if ((j == 2))
                        table[i, j].Width = 200;

                    text.CharacterFormat.TextColor = Color.FromArgb(242, 151, 50);
                    text.CharacterFormat.Bold = true;

                }

            }

            // Adding a nested Table.
            IWTable nestTable = table[2, 2].AddTable();

            format = new RowFormat();

            format.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.DotDash;
            format.HorizontalAlignment = RowAlignment.Center;
            nestTable.ResetCells(3, 3, format, 50);

            for (int i = 0; i < nestTable.Rows.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    paragraph = nestTable[i, j].AddParagraph() as WParagraph;
                    paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                    nestTable[i, j].CellFormat.BackColor = Color.FromArgb(231, 235, 245);
                    text = paragraph.AppendText(string.Format("Cell {0} , {1}", i, j + 1));
                    text.CharacterFormat.TextColor = Color.Black;
                    text.CharacterFormat.Bold = true;
                }
            }
            #endregion Nested Table

            #region Table with Images

            //Add a new section to the document.
            section = document.AddSection();
            //Add paragraph to the section.
            paragraph = section.AddParagraph();
            //Writing text.
            textRange = paragraph.AppendText("Table with Images");
            textRange.CharacterFormat.FontSize = 13f;
            textRange.CharacterFormat.TextColor = Color.DarkBlue;
            textRange.CharacterFormat.Bold = true;

            //Add paragraph to the section.
            section.AddParagraph();
            paragraph = section.AddParagraph();

            text = null;

            //Adding a new Table to the paragraph.
            table = section.Body.AddTable();
            table.ResetCells(1, 3);

            //Adding rows to the table.
            WTableRow row = table.Rows[0];
            //Set heading row height
            row.Height = 25f;
            //set heading values to the Table.
            for (int i = 0; i < 3; i++)
            {
                //Add paragraph for writing Text to the cells.
                paragraph = (IWParagraph)row.Cells[i].AddParagraph();
                //Set Horizontal Alignment as Center.
                paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //Writing Row Heading
                switch (i)
                {
                    case 0:
                        text = paragraph.AppendText("SNO");
                        row.Cells[i].Width = 50f; break;
                    case 1: text = paragraph.AppendText("Drinks"); break;
                    case 2: text = paragraph.AppendText("Showcase Image"); row.Cells[i].Width = 200f; break;
                }
                //Set row Heading formatting
                text.CharacterFormat.Bold = true;
                text.CharacterFormat.FontName = "Cambria";
                text.CharacterFormat.FontSize = 11f;
                text.CharacterFormat.TextColor = Color.White;

                //Set row cells formatting
                row.Cells[i].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
                row.Cells[i].CellFormat.BackColor = Color.FromArgb(157, 161, 190);

                row.Cells[i].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            }

            int sno = 1;
            //Writing Sno, Product name and Product Images to the Table.

            row1 = table.AddRow(false);

            //Writing SNO to the table with formatting text.
            paragraph = (IWParagraph)row1.Cells[0].AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            text = paragraph.AppendText(sno.ToString());
            text.CharacterFormat.Bold = true;
            text.CharacterFormat.FontSize = 10f;
            row1.Cells[0].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            row1.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            row1.Cells[0].CellFormat.BackColor = Color.FromArgb(217, 223, 239);
            //Writing Product Name to the table with Formatting.
            paragraph = (IWParagraph)row1.Cells[1].AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            text = paragraph.AppendText("Apple Juice");
            text.CharacterFormat.Bold = true;
            text.CharacterFormat.FontSize = 10f;
            text.CharacterFormat.TextColor = Color.FromArgb(50, 65, 124);
            row1.Cells[1].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            row1.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            row1.Cells[1].CellFormat.BackColor = Color.FromArgb(217, 223, 239);

            Assembly execAssm = typeof(TableFormattingDemo).GetTypeInfo().Assembly;

            //Writing Product Images to the Table.
            paragraph = (IWParagraph)row1.Cells[2].AddParagraph();

            Stream imageStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Apple.png");

            paragraph.AppendPicture(imageStream);
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            row1.Cells[2].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            row1.Cells[2].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            row1.Cells[2].CellFormat.BackColor = Color.FromArgb(217, 223, 239);
            sno++;
            row1 = table.AddRow(false);

            //Writing SNO to the table with formatting text.
            paragraph = (IWParagraph)row1.Cells[0].AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            text = paragraph.AppendText(sno.ToString());
            text.CharacterFormat.Bold = true;
            text.CharacterFormat.FontSize = 10f;
            row1.Cells[0].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            row1.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            row1.Cells[0].CellFormat.BackColor = Color.FromArgb(217, 223, 239);
            //Writing Product Name to the table with Formatting.
            paragraph = (IWParagraph)row1.Cells[1].AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            text = paragraph.AppendText("Grape Juice");
            text.CharacterFormat.Bold = true;
            text.CharacterFormat.FontSize = 10f;
            text.CharacterFormat.TextColor = Color.FromArgb(50, 65, 124);
            row1.Cells[1].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            row1.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            row1.Cells[1].CellFormat.BackColor = Color.FromArgb(217, 223, 239);

            //Writing Product Images to the Table.
            paragraph = (IWParagraph)row1.Cells[2].AddParagraph();

            imageStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Grape.png");

            paragraph.AppendPicture(imageStream);
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            row1.Cells[2].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            row1.Cells[2].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            row1.Cells[2].CellFormat.BackColor = Color.FromArgb(217, 223, 239);
            sno++;
            row1 = table.AddRow(false);

            //Writing SNO to the table with formatting text.
            paragraph = (IWParagraph)row1.Cells[0].AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            text = paragraph.AppendText(sno.ToString());
            text.CharacterFormat.Bold = true;
            text.CharacterFormat.FontSize = 10f;
            row1.Cells[0].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            row1.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            row1.Cells[0].CellFormat.BackColor = Color.FromArgb(217, 223, 239);
            //Writing Product Name to the table with Formatting.
            paragraph = (IWParagraph)row1.Cells[1].AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            text = paragraph.AppendText("Hot Soup");
            text.CharacterFormat.Bold = true;
            text.CharacterFormat.FontSize = 10f;
            text.CharacterFormat.TextColor = Color.FromArgb(50, 65, 124);
            row1.Cells[1].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            row1.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            row1.Cells[1].CellFormat.BackColor = Color.FromArgb(217, 223, 239);

            //Writing Product Images to the Table.
            paragraph = (IWParagraph)row1.Cells[2].AddParagraph();

            imageStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Soup.png");

            paragraph.AppendPicture(imageStream);
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            row1.Cells[2].CellFormat.VerticalAlignment = Syncfusion.DocIO.DLS.VerticalAlignment.Middle;
            row1.Cells[2].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            row1.Cells[2].CellFormat.BackColor = Color.FromArgb(217, 223, 239);
            sno++;
#endregion Table with Images
            Save(rdDoc.IsChecked == true, document);
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
    }
}
