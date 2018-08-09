//using Common;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Common;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataValidation : SampleLayout
    {
        public DataValidation()
        {
            this.InitializeComponent();
			if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
			{
                this.btnGenerateExcel.HorizontalAlignment = HorizontalAlignment.Center;

            this.grdMain.Margin = new Thickness(10);
            this.stackPnlOptions.Visibility = Visibility.Collapsed;
             this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using Essential XlsIO.";
			}
			else
            this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using Essential XlsIO in the selected format.";

        }


        private async void btnGenerateExcel_Click(object sender, RoutedEventArgs e)
        {
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "DataValidationSample";
                if (rdBtn2003.IsChecked.Value)
                    savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xls" });
                else
                    savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                if (rdBtn2003.IsChecked.Value)
                    storageFile = await local.CreateFileAsync("DataValidationSample.xls", CreationCollisionOption.ReplaceExisting);
                else
                    storageFile = await local.CreateFileAsync("DataValidationSample.xlsx", CreationCollisionOption.ReplaceExisting);
            }

           
            if (storageFile == null)
                return;
            //Instantiate excel Engine
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;

            if (rdBtn2003.IsChecked.Value)
                application.DefaultVersion = ExcelVersion.Excel97to2003;
            else
                application.DefaultVersion = ExcelVersion.Excel2013;

            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet sheet = workbook.Worksheets[0];

            //Data validation to list the values in the first cell
            IDataValidation validation = sheet.Range["C7"].DataValidation;
            sheet.Range["B7"].Text = "Select an item from the validation list";

            validation.ListOfValues = new string[] { "PDF", "XlsIO", "DocIO" };
            validation.PromptBoxText = "Data Validation list";
            validation.IsPromptBoxVisible = true;
            validation.ShowPromptBox = true;

            sheet.Range["C7"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["C7"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["C7"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            // Data Validation for Numbers
            IDataValidation validation1 = sheet.Range["C9"].DataValidation;
            sheet.Range["B9"].Text = "Enter a Number to validate";
            validation1.AllowType = ExcelDataType.Integer;
            validation1.CompareOperator = ExcelDataValidationComparisonOperator.Between;
            validation1.FirstFormula = "0";
            validation1.SecondFormula = "10";
            validation1.ShowErrorBox = true;
            validation1.ErrorBoxText = "Value must be between 0 and 10";
            validation1.ErrorBoxTitle = "ERROR";
            validation1.PromptBoxText = "Value must be between 0 and 10";
            validation1.ShowPromptBox = true;
            sheet.Range["C9"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["C9"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["C9"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            // Data Validation for Date
            IDataValidation validation2 = sheet.Range["C11"].DataValidation;
            sheet.Range["B11"].Text = "Enter the Date to validate";
            validation2.AllowType = ExcelDataType.Date;
            validation2.CompareOperator = ExcelDataValidationComparisonOperator.Between;
            validation2.FirstDateTime = new DateTime(2012, 1, 1);
            validation2.SecondDateTime = new DateTime(2012, 12, 31);
            validation2.ShowErrorBox = true;
            validation2.ErrorBoxText = "Value must be from 01/1/2012 to 31/12/2012";
            validation2.ErrorBoxTitle = "ERROR";
            validation2.PromptBoxText = "Value must be from 01/1/2012 to 31/12/2012";
            validation2.ShowPromptBox = true;
            sheet.Range["C11"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["C11"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["C11"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            // Data Validation for TextLength
            IDataValidation validation3 = sheet.Range["C13"].DataValidation;
            sheet.Range["B13"].Text = "Enter the Text to validate";
            validation3.AllowType = ExcelDataType.TextLength;
            validation3.CompareOperator = ExcelDataValidationComparisonOperator.Between;
            validation3.FirstFormula = "1";
            validation3.SecondFormula = "6";
            validation3.ShowErrorBox = true;
            validation3.ErrorBoxText = "Maximum 6 characters are allowed";
            validation3.ErrorBoxTitle = "ERROR";
            validation3.PromptBoxText = "Maximum 6 characters are allowed";
            validation3.ShowPromptBox = true;
            sheet.Range["C13"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["C13"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["C13"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            // Data Validation for Time
            IDataValidation validation4 = sheet.Range["C15"].DataValidation;
            sheet.Range["B15"].Text = "Enter the Time to validate";
            validation4.AllowType = ExcelDataType.Time;
            validation4.CompareOperator = ExcelDataValidationComparisonOperator.Between;
            validation4.FirstFormula = "10";
            validation4.SecondFormula = "12";
            validation4.ShowErrorBox = true;
            validation4.ErrorBoxText = "Time must be between 10 and 12";
            validation4.ErrorBoxTitle = "ERROR";
            validation4.PromptBoxText = "Time must be between 10 and 12";
            validation4.ShowPromptBox = true;
            sheet.Range["C15"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["C15"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["C15"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
            sheet.Range["B2:C2"].Merge();
            sheet.Range["B2"].Text = "Simple Data validation";
            sheet.Range["B5"].Text = "Validation criteria";
            sheet.Range["C5"].Text = "Validation";
            sheet.Range["B5"].CellStyle.Font.Bold = true;
            sheet.Range["C5"].CellStyle.Font.Bold = true;
            sheet.Range["B2"].CellStyle.Font.Bold = true;
            sheet.Range["B2"].CellStyle.Font.Size = 16;
            sheet.Range["B2"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
            sheet.AutofitColumn(2);
            sheet["B7"].ColumnWidth = 40.3;
            sheet.UsedRange.AutofitRows();

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
