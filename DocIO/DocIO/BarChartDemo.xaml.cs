using Common;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.OfficeChart;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EssentialDocIO
{
    public sealed partial class BarChartDemo : SampleLayout
    {

        public BarChartDemo()
        {
            InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                BarChart.Margin = new Thickness(10, 10, 10, 10);
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

            BarChart.ClearValue(Grid.BackgroundProperty);
            BarChart.ClearValue(Grid.PaddingProperty);
            BarChart.Children.Clear();
            BarChart.ColumnDefinitions.Clear();
            BarChart.RowDefinitions.Clear();
            BarChart = null;
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
            Assembly execAssem = typeof(BarChartDemo).GetTypeInfo().Assembly;
            //A new document is created.
            WordDocument document = new WordDocument();
            //Add new section to the Word document
            IWSection section = document.AddSection();
            //Set page margins of the section
            section.PageSetup.Margins.All = 72;
            //Add new paragraph to the section
            IWParagraph paragraph = section.AddParagraph();
            //Apply heading style to the title paragraph
            paragraph.ApplyStyle(BuiltinStyle.Heading1);
            //Apply center alignment to the paragraph
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            //Append text to the paragraph
            paragraph.AppendText("Northwind Management Report").CharacterFormat.TextColor = Syncfusion.DocIO.DLS.Color.FromArgb(46, 116, 181);
            //Add new paragraph
            paragraph = section.AddParagraph();
            //Set before spacing to the paragraph
            paragraph.ParagraphFormat.BeforeSpacing = 20;
            //Load the excel template as stream
            Stream excelStream = execAssem.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Excel_Template.xlsx");
            //Create and Append chart to the paragraph with excel stream as parameter
            WChart barChart = paragraph.AppendChart(excelStream, 1, "B2:C6", 470, 300);
            //Set chart data
            barChart.ChartType = OfficeChartType.Bar_Clustered;
            barChart.ChartTitle = "Purchase Details";
            barChart.ChartTitleArea.FontName = "Calibri (Body)";
            barChart.ChartTitleArea.Size = 14;
            //Set name to chart series            
            barChart.Series[0].Name = "Sum of Purchases";
            barChart.Series[1].Name = "Sum of Future Expenses";
            //Set Chart Data table
            barChart.HasDataTable = true;
            barChart.DataTable.HasBorders = true;
            barChart.DataTable.HasHorzBorder = true;
            barChart.DataTable.HasVertBorder = true;
            barChart.DataTable.ShowSeriesKeys = true;
            barChart.HasLegend = false;
            //Setting background color
            barChart.ChartArea.Fill.ForeColor = Syncfusion.Drawing.Color.FromArgb(208, 206, 206);
            barChart.PlotArea.Fill.ForeColor = Syncfusion.Drawing.Color.FromArgb(208, 206, 206);
            //Setting line pattern to the chart area
            barChart.PrimaryCategoryAxis.Border.LinePattern = OfficeChartLinePattern.None;
            barChart.PrimaryValueAxis.Border.LinePattern = OfficeChartLinePattern.None;
            barChart.ChartArea.Border.LinePattern = OfficeChartLinePattern.None;
            barChart.PrimaryValueAxis.MajorGridLines.Border.LineColor = Syncfusion.Drawing.Color.FromArgb(175, 171, 171);
            //Set label for primary catagory axis
            barChart.PrimaryCategoryAxis.CategoryLabels = barChart.ChartData[2, 1, 6, 1];
            #region Saving Document
            SaveDocx(document);
            #endregion
        }
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
