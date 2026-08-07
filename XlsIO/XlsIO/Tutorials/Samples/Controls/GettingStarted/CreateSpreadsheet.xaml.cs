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
using System.Globalization;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateSpreadsheet : SampleLayout
    {
        public CreateSpreadsheet()
        {
            this.InitializeComponent();
			if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
			{
                this.btnGenerateExcel.HorizontalAlignment = HorizontalAlignment.Center;
				this.grdMain.Padding = new Thickness(10,0,0,0);
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
                savePicker.SuggestedFileName = "SpreadsheetSample";
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
                    storageFile = await local.CreateFileAsync("SpreadsheetSample.xls", CreationCollisionOption.ReplaceExisting);
                else
                    storageFile = await local.CreateFileAsync("SpreadsheetSample.xlsx", CreationCollisionOption.ReplaceExisting);
            }

                      if (storageFile == null)
                return;
            

            #region Initializing Workbook
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            //Step 1 : Instantiate the spreadsheet creation engine.
            ExcelEngine excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            IApplication application = excelEngine.Excel;

            if (rdBtn2003.IsChecked.Value)
                application.DefaultVersion = ExcelVersion.Excel97to2003;
            else
                application.DefaultVersion = ExcelVersion.Excel2013;

            //A new workbook is created.[Equivalent to creating a new workbook in MS Excel]
            //The new workbook will have 5 worksheets
            IWorkbook workbook = application.Workbooks.Create(1);

            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            #endregion

            #region Generate Excel
			if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            sheet.Range["A2"].RowHeight = 34;

            sheet.Range["A2"].ColumnWidth = 30;
            sheet.Range["B2"].ColumnWidth = 30;
            sheet.Range["C2"].ColumnWidth = 30;
            sheet.Range["D2"].ColumnWidth = 30;

            sheet.Range["A2:D2"].Merge(true);

            

            //Inserting sample text into the first cell of the first worksheet.
            sheet.Range["A2"].Text = "EXPENSE REPORT";
            sheet.Range["A2"].CellStyle.Font.FontName = "Verdana";
            sheet.Range["A2"].CellStyle.Font.Bold = true;
            sheet.Range["A2"].CellStyle.Font.Size = 28;
            sheet.Range["A2"].CellStyle.Font.RGBColor = Windows.UI.Color.FromArgb(0, 0, 112, 192);
            sheet.Range["A2"].HorizontalAlignment = ExcelHAlign.HAlignCenter;

            sheet.Range["A4"].Text = "Employee";
            sheet.Range["B4"].Text = "Roger Federer";
            sheet.Range["A4:B7"].CellStyle.Font.FontName = "Verdana";
            sheet.Range["A4:B7"].CellStyle.Font.Bold = true;
            sheet.Range["A4:B7"].CellStyle.Font.Size = 11;
            sheet.Range["A4:A7"].CellStyle.Font.RGBColor = Windows.UI.Color.FromArgb(0, 128, 128, 128);
            sheet.Range["A4:A7"].HorizontalAlignment = ExcelHAlign.HAlignLeft;
            sheet.Range["B4:B7"].CellStyle.Font.RGBColor = Windows.UI.Color.FromArgb(0, 174, 170, 170);
            sheet.Range["B4:B7"].HorizontalAlignment = ExcelHAlign.HAlignRight;

            sheet.Range["A9:D20"].CellStyle.Font.FontName = "Verdana";
            sheet.Range["A9:D20"].CellStyle.Font.Size = 11;

            sheet.Range["A5"].Text = "Department";
            sheet.Range["B5"].Text = "Administration";

            sheet.Range["A6"].Text = "Week Ending";
            sheet.Range["B6"].NumberFormat = "m/d/yyyy";
            sheet.Range["B6"].DateTime = DateTime.Parse("10/10/2012", CultureInfo.InvariantCulture);

            sheet.Range["A7"].Text = "Mileage Rate";
            sheet.Range["B7"].NumberFormat = "$#,##0.00";
            sheet.Range["B7"].Number = 0.70;
           
            sheet.Range["A10"].Text = "Miles Driven";
            sheet.Range["A11"].Text = "Miles Reimbursement";
            sheet.Range["A12"].Text = "Parking and Tolls";
            sheet.Range["A13"].Text = "Auto Rental";
            sheet.Range["A14"].Text = "Lodging";
            sheet.Range["A15"].Text = "Breakfast";
            sheet.Range["A16"].Text = "Lunch";
            sheet.Range["A17"].Text = "Dinner";
            sheet.Range["A18"].Text = "Snacks";
            sheet.Range["A19"].Text = "Others";
            sheet.Range["A20"].Text = "Total";
            sheet.Range["A20:D20"].CellStyle.Color = Windows.UI.Color.FromArgb(0, 0, 112, 192);
            sheet.Range["A20:D20"].CellStyle.Font.Color = ExcelKnownColors.White;
            sheet.Range["A20:D20"].CellStyle.Font.Bold = true;
         
            IStyle style=sheet["B9:D9"].CellStyle;
            style.VerticalAlignment = ExcelVAlign.VAlignCenter;
            style.HorizontalAlignment = ExcelHAlign.HAlignRight;
            style.Color = Windows.UI.Color.FromArgb(0, 0, 112, 192);
            style.Font.Bold = true;
            style.Font.Color = ExcelKnownColors.White;

            sheet.Range["A9"].Text = "Expenses";
            sheet.Range["A9"].CellStyle.Color = Windows.UI.Color.FromArgb(0, 0, 112, 192);
            sheet.Range["A9"].CellStyle.Font.Color = ExcelKnownColors.White;
            sheet.Range["A9"].CellStyle.Font.Bold = true;
            sheet.Range["B9"].Text = "Day 1";
            sheet.Range["B10"].Number = 100;
            sheet.Range["B11"].NumberFormat = "$#,##0.00";
            sheet.Range["B11"].Formula = "=(B7*B10)";
            sheet.Range["B12"].NumberFormat = "$#,##0.00";
            sheet.Range["B12"].Number = 0;
            sheet.Range["B13"].NumberFormat = "$#,##0.00";
            sheet.Range["B13"].Number = 0;
            sheet.Range["B14"].NumberFormat = "$#,##0.00";
            sheet.Range["B14"].Number = 0;
            sheet.Range["B15"].NumberFormat = "$#,##0.00";
            sheet.Range["B15"].Number = 9;
            sheet.Range["B16"].NumberFormat = "$#,##0.00";
            sheet.Range["B16"].Number = 12;
            sheet.Range["B17"].NumberFormat = "$#,##0.00";
            sheet.Range["B17"].Number = 13;
            sheet.Range["B18"].NumberFormat = "$#,##0.00";
            sheet.Range["B18"].Number = 9.5;
            sheet.Range["B19"].NumberFormat = "$#,##0.00";
            sheet.Range["B19"].Number = 0;
            sheet.Range["B20"].NumberFormat = "$#,##0.00";
            sheet.Range["B20"].Formula = "=SUM(B11:B19)";

            sheet.Range["C9"].Text = "Day 2";
            sheet.Range["C10"].Number = 145;
            sheet.Range["C11"].NumberFormat = "$#,##0.00";
            sheet.Range["C11"].Formula = "=(B7*C10)";
            sheet.Range["C12"].NumberFormat = "$#,##0.00";
            sheet.Range["C12"].Number = 15;
            sheet.Range["C13"].NumberFormat = "$#,##0.00";
            sheet.Range["C13"].Number = 0;
            sheet.Range["C14"].NumberFormat = "$#,##0.00";
            sheet.Range["C14"].Number = 45;
            sheet.Range["C15"].NumberFormat = "$#,##0.00";
            sheet.Range["C15"].Number = 9;
            sheet.Range["C16"].NumberFormat = "$#,##0.00";
            sheet.Range["C16"].Number = 12;
            sheet.Range["C17"].NumberFormat = "$#,##0.00";
            sheet.Range["C17"].Number = 15;
            sheet.Range["C18"].NumberFormat = "$#,##0.00";
            sheet.Range["C18"].Number = 7;
            sheet.Range["C19"].NumberFormat = "$#,##0.00";
            sheet.Range["C19"].Number = 0;
            sheet.Range["C20"].NumberFormat = "$#,##0.00";
            sheet.Range["C20"].Formula = "=SUM(C11:C19)";

            sheet.Range["D9"].Text = "Day 3";
            sheet.Range["D10"].Number = 113;
            sheet.Range["D11"].NumberFormat = "$#,##0.00";
            sheet.Range["D11"].Formula = "=(B7*D10)";
            sheet.Range["D12"].NumberFormat = "$#,##0.00";
            sheet.Range["D12"].Number = 17;
            sheet.Range["D13"].NumberFormat = "$#,##0.00";
            sheet.Range["D13"].Number = 8;
            sheet.Range["D14"].NumberFormat = "$#,##0.00";
            sheet.Range["D14"].Number = 45;
            sheet.Range["D15"].NumberFormat = "$#,##0.00";
            sheet.Range["D15"].Number = 7;
            sheet.Range["D16"].NumberFormat = "$#,##0.00";
            sheet.Range["D16"].Number = 11;
            sheet.Range["D17"].NumberFormat = "$#,##0.00";
            sheet.Range["D17"].Number = 16;
            sheet.Range["D18"].NumberFormat = "$#,##0.00";
            sheet.Range["D18"].Number = 7;
            sheet.Range["D19"].NumberFormat = "$#,##0.00";
            sheet.Range["D19"].Number = 5;
            sheet.Range["D20"].NumberFormat = "$#,##0.00";
            sheet.Range["D20"].Formula = "=SUM(D11:D19)";
			if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            sheet.UsedRange.AutofitRows();

            #endregion

            #region Saving the workbook
            await workbook.SaveAsAsync(storageFile);
            workbook.Close();
            excelEngine.Dispose();
            #endregion

            #region Launching the saved workbook
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
            DisposeTextBlock(textBox4);
            textBox4 = null;
            DisposeTextBlock(textBox5);
            textBox5 = null;
            DisposeTextBlock(textBox6);
            textBox6 = null;

            DisposeRadioButton(rdBtn2003);
            rdBtn2003 = null;
            DisposeRadioButton(rdBtn2013);
            rdBtn2013 = null;

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
