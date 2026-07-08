#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
//using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Syncfusion.XlsIO;
using Windows.UI.Xaml.Controls;
using Common;
using System.Globalization;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExpensesReport : SampleLayout
    {    
        public ExpensesReport()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                this.btnCreate.HorizontalAlignment = HorizontalAlignment.Center;

                this.grdMain.Margin = new Thickness(10);
            }           
            this.textBox1.Text = "Please click the 'Generate Excel' button to view the expense report Excel document.";
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            #region Setting output location
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "ExpensesReport";
                savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                storageFile = await local.CreateFileAsync("ExpensesReport.xlsx", CreationCollisionOption.ReplaceExisting);
            }

            if (storageFile == null)
                return;
            #endregion

            #region Workbook Initialize
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;

                application.DefaultVersion = ExcelVersion.Xlsx;

                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet sheet1 = workbook.Worksheets[0];
                sheet1.Name = "Budget";
                sheet1.IsGridLinesVisible = false;
                sheet1.EnableSheetCalculations();

                sheet1.Range[1, 1].ColumnWidth = 19.86;
                sheet1.Range[1, 2].ColumnWidth = 14.38;
                sheet1.Range[1, 3].ColumnWidth = 12.98;
                sheet1.Range[1, 4].ColumnWidth = 12.08;
                sheet1.Range[1, 5].ColumnWidth = 8.82;
                sheet1.Range["A1:A18"].RowHeight = 20.2;

                //Adding cell style.               
                IStyle style1 = workbook.Styles.Add("style1");
                style1.Color = Windows.UI.Color.FromArgb(0, 217, 225, 242);
                style1.HorizontalAlignment = ExcelHAlign.HAlignLeft;
                style1.VerticalAlignment = ExcelVAlign.VAlignCenter;
                style1.Font.Bold = true;

                IStyle style2 = workbook.Styles.Add("style2");
                style2.Color = Windows.UI.Color.FromArgb(0, 142, 169, 219);
                style2.VerticalAlignment = ExcelVAlign.VAlignCenter;
                style2.NumberFormat = "[Red]($#,###)";
                style2.Font.Bold = true;

                sheet1.Range["A10"].CellStyle = style1;
                sheet1.Range["B10:D10"].CellStyle.Color = Windows.UI.Color.FromArgb(0, 217, 225, 242);
                sheet1.Range["B10:D10"].HorizontalAlignment = ExcelHAlign.HAlignRight;
                sheet1.Range["B10:D10"].VerticalAlignment = ExcelVAlign.VAlignCenter;
                sheet1.Range["B10:D10"].CellStyle.Font.Bold = true;

                sheet1.Range["A11:A17"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                sheet1.Range["A11:D17"].Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                sheet1.Range["A11:D17"].Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Grey_25_percent;

                sheet1.Range["D18"].CellStyle = style2;
                sheet1.Range["D18"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                sheet1.Range["A18:C18"].CellStyle.Color = Windows.UI.Color.FromArgb(0, 142, 169, 219);
                sheet1.Range["A18:C18"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                sheet1.Range["A18:C18"].CellStyle.Font.Bold = true;
                sheet1.Range["A18:C18"].NumberFormat = "$#,###";

                sheet1.Range[10, 1].Text = "Category";
                sheet1.Range[10, 2].Text = "Expected cost";
                sheet1.Range[10, 3].Text = "Actual Cost";
                sheet1.Range[10, 4].Text = "Difference";
                sheet1.Range[11, 1].Text = "Venue";
                sheet1.Range[12, 1].Text = "Seating & Decor";
                sheet1.Range[13, 1].Text = "Technical team";
                sheet1.Range[14, 1].Text = "Performers";
                sheet1.Range[15, 1].Text = "Performer\'s transport";
                sheet1.Range[16, 1].Text = "Performer\'s stay";
                sheet1.Range[17, 1].Text = "Marketing";
                sheet1.Range[18, 1].Text = "Total";

                sheet1.Range["B11:D17"].NumberFormat = "$#,###";
                sheet1.Range["D11"].NumberFormat = "[Red]($#,###)";
                sheet1.Range["D12"].NumberFormat = "[Red]($#,###)";
                sheet1.Range["D14"].NumberFormat = "[Red]($#,###)";

                sheet1.Range["B11"].Number = 16250;
                sheet1.Range["B12"].Number = 1600;
                sheet1.Range["B13"].Number = 1000;
                sheet1.Range["B14"].Number = 12400;
                sheet1.Range["B15"].Number = 3000;
                sheet1.Range["B16"].Number = 4500;
                sheet1.Range["B17"].Number = 3000;
                sheet1.Range["B18"].Formula = "=SUM(B11:B17)";

                sheet1.Range["C11"].Number = 17500;
                sheet1.Range["C12"].Number = 1828;
                sheet1.Range["C13"].Number = 800;
                sheet1.Range["C14"].Number = 14000;
                sheet1.Range["C15"].Number = 2600;
                sheet1.Range["C16"].Number = 4464;
                sheet1.Range["C17"].Number = 2700;
                sheet1.Range["C18"].Formula = "=SUM(C11:C17)";

                sheet1.Range["D11"].Formula = "=IF(C11>B11,C11-B11,B11-C11)";
                sheet1.Range["D12"].Formula = "=IF(C12>B12,C12-B12,B12-C12)";
                sheet1.Range["D13"].Formula = "=IF(C13>B13,C13-B13,B13-C13)";
                sheet1.Range["D14"].Formula = "=IF(C14>B14,C14-B14,B14-C14)";
                sheet1.Range["D15"].Formula = "=IF(C15>B15,C15-B15,B15-C15)";
                sheet1.Range["D16"].Formula = "=IF(C16>B16,C16-B16,B16-C16)";
                sheet1.Range["D17"].Formula = "=IF(C17>B17,C17-B17,B17-C17)";
                sheet1.Range["D18"].Formula = "=IF(C18>B18,C18-B18,B18-C18)";

                IChartShape chart = sheet1.Charts.Add();
                chart.ChartType = ExcelChartType.Pie;
                chart.DataRange = sheet1.Range["A11:B17"];
                chart.IsSeriesInRows = false;
                chart.ChartTitle = "Event Expenses";
                chart.ChartTitleArea.Bold = true;
                chart.ChartTitleArea.Size = 16;
                chart.TopRow = 1;
                chart.BottomRow = 10;
                chart.LeftColumn = 1;
                chart.RightColumn = 5;
                chart.ChartArea.Border.LinePattern = ExcelChartLinePattern.None;

                #endregion

                #region Saving workbook and disposing objects

                await workbook.SaveAsAsync(storageFile);
                workbook.Close();
                
                #endregion
            }

            #region Save accknowledgement and Launching of output file
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
            DisposeTextBlock(textBox5);
            textBox5 = null;
            DisposeTextBlock(textBox6);
            textBox6 = null;

            DisposeButton(btnCreate);
            btnCreate = null;

            DisposeStackPanel(stackPnlOptions);
            stackPnlOptions = null;

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
            button.Click -= btnCreate_Click;
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
