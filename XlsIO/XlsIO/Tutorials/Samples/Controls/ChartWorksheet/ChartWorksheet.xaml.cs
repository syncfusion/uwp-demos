using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EssentialXlsIO
{
    public sealed partial class ChartWorksheet : SampleLayout
    {
        public ChartWorksheet()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
                stackPnlOptions.Visibility = Visibility.Collapsed;
                this.btnGenerateExcel.HorizontalAlignment = HorizontalAlignment.Center;
                this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using Essential XlsIO.";
            }
            else
                this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using Essential XlsIO in the selected format.";
        }

        private async void btnGenerateExcel_Click(object sender, RoutedEventArgs e)
        {
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            //Step 1 : Instantiate the spreadsheet creation engine.
            ExcelEngine excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            IApplication application = excelEngine.Excel;

            // Default version is set as Excel 2007
            if (this.rdbExcel2013.IsChecked.Value)
                application.DefaultVersion = ExcelVersion.Excel2013;
            else
                application.DefaultVersion = ExcelVersion.Excel97to2003;

            //A new workbook is created.[Equivalent to creating a new workbook in MS Excel]
            //The new workbook will have 1 worksheet.
            IWorkbook workbook = application.Workbooks.Create(1);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];

            // Entering the Datas for the chart
            sheet.Range["A1"].Text = "Crescent City, CA";
            sheet.Range["A1:D1"].Merge();
            sheet.Range["A1"].CellStyle.Font.Bold = true;

            sheet.Range["B3"].Text = "Precipitation,in.";
            sheet.Range["C3"].Text = "Temperature,deg.F";

            sheet.Range["A4"].Text = "Jan";
            sheet.Range["A5"].Text = "Feb";
            sheet.Range["A6"].Text = "March";
            sheet.Range["A7"].Text = "Apr";
            sheet.Range["A8"].Text = "May";
            sheet.Range["A9"].Text = "June";
            sheet.Range["A10"].Text = "July";
            sheet.Range["A11"].Text = "Aug";
            sheet.Range["A12"].Text = "Sept";
            sheet.Range["A13"].Text = "Oct";
            sheet.Range["A14"].Text = "Nov";
            sheet.Range["A15"].Text = "Dec";

            sheet.Range["B4"].Number = 10.9;
            sheet.Range["B5"].Number = 8.9;
            sheet.Range["B6"].Number = 8.6;
            sheet.Range["B7"].Number = 4.8;
            sheet.Range["B8"].Number = 3.2;
            sheet.Range["B9"].Number = 1.4;
            sheet.Range["B10"].Number = 0.6;
            sheet.Range["B11"].Number = 0.7;
            sheet.Range["B12"].Number = 1.7;
            sheet.Range["B13"].Number = 5.4;
            sheet.Range["B14"].Number = 9.0;
            sheet.Range["B15"].Number = 10.4;

            sheet.Range["C4"].Number = 47.5;
            sheet.Range["C5"].Number = 48.7;
            sheet.Range["C6"].Number = 48.9;
            sheet.Range["C7"].Number = 50.2;
            sheet.Range["C8"].Number = 53.1;
            sheet.Range["C9"].Number = 56.3;
            sheet.Range["C10"].Number = 58.1;
            sheet.Range["C11"].Number = 59.0;
            sheet.Range["C12"].Number = 58.5;
            sheet.Range["C13"].Number = 55.4;
            sheet.Range["C14"].Number = 51.1;
            sheet.Range["C15"].Number = 47.8;
            sheet.UsedRange.AutofitColumns();

            // Adding a New chart to the Existing Worksheet   
            IChart chart = workbook.Charts.Add();
            chart.DataRange = sheet.Range["A3:C15"];
            chart.Name = "CrescentCity,CA";
            chart.ChartTitle = "Crescent City, CA";
            chart.IsSeriesInRows = false;

            chart.PrimaryValueAxis.Title = "Precipitation,in.";
            chart.PrimaryValueAxis.TitleArea.TextRotationAngle = 90;
            chart.PrimaryValueAxis.MaximumValue = 14.0;
            chart.PrimaryValueAxis.NumberFormat = "0.0";

            // Format serie
            IChartSerie serieOne = chart.Series[0];
            serieOne.Name = "Precipitation,in.";
            serieOne.SerieFormat.Fill.FillType = ExcelFillType.Gradient;
            serieOne.SerieFormat.Fill.TwoColorGradient(ExcelGradientStyle.Vertical, ExcelGradientVariants.ShadingVariants_2);
            serieOne.SerieFormat.Fill.GradientColorType = ExcelGradientColor.TwoColor;
            serieOne.SerieFormat.Fill.ForeColor = Color.FromArgb(255, 221, 160, 221);

            //Show value as data labels
            serieOne.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;
            serieOne.DataPoints.DefaultDataPoint.DataLabels.Position = ExcelDataLabelPosition.Outside;

            //Format the second serie
            IChartSerie serieTwo = chart.Series[1];
            serieTwo.SerieType = ExcelChartType.Line_Markers;
            serieTwo.Name = "Temperature,deg.F";

            //Format marker
            serieTwo.SerieFormat.MarkerStyle = ExcelChartMarkerType.Diamond;
            serieTwo.SerieFormat.MarkerSize = 8;
            serieTwo.SerieFormat.MarkerBackgroundColor = Color.FromArgb(255, 0, 100, 0);
            serieTwo.SerieFormat.MarkerForegroundColor = Color.FromArgb(255, 0, 100, 0);
            serieTwo.SerieFormat.LineProperties.LineColor = Color.FromArgb(255, 0, 100, 0);

            //Use Secondary Axis
            serieTwo.UsePrimaryAxis = false;

            //Display secondary axis for the series.
            chart.SecondaryCategoryAxis.IsMaxCross = true;
            chart.SecondaryValueAxis.IsMaxCross = true;

            //Set the title
            chart.SecondaryValueAxis.Title = "Temperature,deg.F";
            chart.SecondaryValueAxis.TitleArea.TextRotationAngle = 90;

            //Hide the secondary category axis
            chart.SecondaryCategoryAxis.Border.LineColor = Color.FromArgb(0, 0, 0 , 0);
            chart.SecondaryCategoryAxis.MajorTickMark = ExcelTickMark.TickMark_None;
            chart.SecondaryCategoryAxis.TickLabelPosition = ExcelTickLabelPosition.TickLabelPosition_None;

            chart.Legend.Position = ExcelLegendPosition.Bottom;
            chart.Legend.IsVerticalLegend = false;

            //Excel2013 Filter option
            if (rdbExcel2013.IsChecked.Value)
            {
                chart.Series[1].IsFiltered = true;
                chart.Categories[0].IsFiltered = true;
                chart.Categories[1].IsFiltered = true;
            }

            sheet.Move(1);
            chart.Activate();

            #region Save the Workbook
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "ChartWorksheet";
                if (workbook.Version == ExcelVersion.Excel97to2003)
                    savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xls" });
                else
                    savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                if (workbook.Version == ExcelVersion.Excel97to2003)
                    storageFile = await local.CreateFileAsync("ChartWorksheet.xls", CreationCollisionOption.ReplaceExisting);
                else
                    storageFile = await local.CreateFileAsync("ChartWorksheet.xlsx", CreationCollisionOption.ReplaceExisting);
            }

            if (storageFile != null)
            {
                //Saving the workbook
                await workbook.SaveAsAsync(storageFile);
                workbook.Close();
                excelEngine.Dispose();

                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");

                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new UICommand("No");
                msgDialog.Commands.Add(noCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the saved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(storageFile);
                }
            }
            else
            {
                workbook.Close();
                excelEngine.Dispose();
            }
            #endregion
        }

        #region Dispose
        public override void Dispose()
        {
            base.Dispose();

            UnlinkChildrens(grdMain);
            DisposeTextBlock(textBox1);
            textBox1 = null;
            DisposeTextBlock(textBox2);
            textBox2 = null;
            DisposeTextBlock(textBox3);
            textBox3 = null;
            DisposeTextBlock(textBox4);
            textBox4 = null;
            DisposeTextBlock(textBox5);
            textBox5 = null;
            DisposeTextBlock(textBox6);
            textBox6 = null;

            DisposeRadioButton(rdbExcel2003);
            rdbExcel2003 = null;
            DisposeRadioButton(rdbExcel2013);
            rdbExcel2013 = null;

            DisposeButton(btnGenerateExcel);
            btnGenerateExcel = null;

            DisposeStackPanel(stackPnlOptions);
            stackPnlOptions = null;

            DisposeGrid(grd1);
            grd1 = null;

            DisposeGrid(grdMain);
            grdMain = null;
        }

        private void DisposeTextBlock(TextBlock textBlock)
        {
            if (textBlock == null)
                return;
            textBlock.ClearValue(TextBlock.FontFamilyProperty);
            textBlock.ClearValue(TextBlock.FontSizeProperty);
            textBlock.ClearValue(TextBlock.TextProperty);
            textBlock.ClearValue(TextBlock.TextWrappingProperty);
            textBlock.ClearValue(TextBlock.ForegroundProperty);
        }

        private void DisposeButton(Button button)
        {
            if (button == null)
                return;
            button.ClearValue(Button.FontFamilyProperty);
            button.ClearValue(Button.FontSizeProperty);
            button.ClearValue(Button.PaddingProperty);
            button.ClearValue(Button.ForegroundProperty);
            button.ClearValue(Button.BackgroundProperty);
            button.ClearValue(Button.ContentProperty);
            button.ClearValue(Button.HeightProperty);
            button.ClearValue(Button.WidthProperty);
            button.Click -= btnGenerateExcel_Click;
        }

        private void DisposeRadioButton(RadioButton radioButton)
        {
            if (radioButton == null)
                return;
            radioButton.ClearValue(RadioButton.GroupNameProperty);
            radioButton.ClearValue(RadioButton.ContentProperty);
            radioButton.ClearValue(RadioButton.FontFamilyProperty);
            radioButton.ClearValue(RadioButton.FontSizeProperty);
            radioButton.ClearValue(RadioButton.ForegroundProperty);
            radioButton.ClearValue(RadioButton.WidthProperty);
            radioButton.ClearValue(RadioButton.IsCheckedProperty);
        }

        private void DisposeStackPanel(StackPanel stackPanel)
        {
            if (stackPanel == null)
                return;
            stackPanel.ClearValue(StackPanel.OrientationProperty);
            stackPanel.ClearValue(StackPanel.HorizontalAlignmentProperty);
        }

        private void DisposeGrid(Grid grid)
        {
            if (grid == null)
                return;
            grid.ClearValue(Grid.BackgroundProperty);
            grid.ClearValue(Grid.MarginProperty);
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
        }

        void UnlinkChildrens(UIElement element)
        {
            if (element == null)
                return;
            if (element is Panel)
            {
                for (int i = 0; i < (element as Panel).Children.Count; i++)
                {
                    UIElement childElement = (element as Panel).Children[i];
                    UnlinkChildrens(childElement);
                    (element as Panel).Children.Remove(childElement);
                    i--;
                }
            }
            else if (element is ItemsControl)
            {
                for (int j = 0; j < (element as ItemsControl).Items.Count; j++)
                {
                    UIElement childElement = ((element as ItemsControl).Items[j] as UIElement);
                    if (childElement == null)
                    {
                        //(element as ItemsControl).Items.RemoveAt(j);
                        //j--;
                    }
                    else
                    {
                        UnlinkChildrens(childElement);
                        (element as ItemsControl).Items.Remove(childElement);
                        j--;
                    }
                }
            }
            else if (element is ContentControl)
            {
                UnlinkChildrens((element as ContentControl).Content as UIElement);
                (element as ContentControl).Content = null;
            }
            else if (element is UserControl)
            {
                UnlinkChildrens((element as UserControl).Content as UIElement);
                (element as UserControl).Content = null;
            }
        }
        #endregion
    }
}
