//using Common;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Common;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Formulas : SampleLayout
    {
        public Formulas()
        {
            this.InitializeComponent();			
			if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
			{
                this.grdMain.Padding = new Thickness(10, 0, 0, 0);
                this.stackPnlOptions.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.StackCreate.HorizontalAlignment = HorizontalAlignment.Center;
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
                savePicker.SuggestedFileName = "FormulaSample";
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
                    storageFile = await local.CreateFileAsync("FormulaSample.xls", CreationCollisionOption.ReplaceExisting);
                else
                    storageFile = await local.CreateFileAsync("FormulaSample.xlsx", CreationCollisionOption.ReplaceExisting);
            }
           
            if (storageFile == null)
                return;
                //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
                //The instantiation process consists of two steps.

                //Step 1 : Instantiate the spreadsheet creation engine.
                ExcelEngine excelEngine = new ExcelEngine();
                //Step 2 : Instantiate the excel application object.
                IApplication application = excelEngine.Excel;
                if (this.rdBtn2003.IsChecked.Value)
                    application.DefaultVersion = ExcelVersion.Excel97to2003;             
                else
                    application.DefaultVersion = ExcelVersion.Excel2013;
                //A new workbook is created.[Equivalent to creating a new workbook in MS Excel]
                //The new workbook will have 3 worksheets
                IWorkbook workbook = application.Workbooks.Create(1);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];

                #region Insert Array Formula

                sheet.Range["A2"].Text = "Array formulas";
                sheet.Range["B2:E2"].Number = 3;
                sheet.Names.Add("ArrayRange", sheet.Range["B2:E2"]);
                sheet.Range["B3:E3"].Number = 5;
                sheet.Range["A2"].CellStyle.Font.Bold = true;
                sheet.Range["A2"].CellStyle.Font.Size = 14;

                #endregion

                #region Excel functions

                sheet.Range["A5"].Text = "Formula";
                sheet.Range["B5"].Text = "Result";

                sheet.Range["A7"].Text = "ABS(ABS(-B3))";
                sheet.Range["B7"].Formula = "ABS(ABS(-B3))";

                sheet.Range["A9"].Text = "SUM(B3,C3)";
                sheet.Range["B9"].Formula = "SUM(B3,C3)";

                sheet.Range["A11"].Text = "MIN(10,20,30,5,15,35,6,16,36)";
                sheet.Range["B11"].Formula = "MIN(10,20,30,5,15,35,6,16,36)";

                sheet.Range["A13"].Text = "MAX(10,20,30,5,15,35,6,16,36)";
                sheet.Range["B13"].Formula = "MAX(10,20,30,5,15,35,6,16,36)";

                sheet.Range["A5:B5"].CellStyle.Font.Bold = true;
                sheet.Range["A5:B5"].CellStyle.Font.Size = 14;

                #endregion

                #region Simple formulas
                sheet.Range["C7"].Number = 10;
                sheet.Range["C9"].Number = 10;
                sheet.Range["A15"].Text = "C7+C9";
                sheet.Range["B15"].Formula = "C7+C9";

                #endregion

                sheet.Range["B1"].Text = "Excel formula support";
                sheet.Range["B1"].CellStyle.Font.Bold = true;
                sheet.Range["B1"].CellStyle.Font.Size = 14;
                sheet.Range["B1:E1"].Merge();
                sheet.Range["A1:A15"].AutofitColumns();
                sheet.UsedRange.AutofitRows();
                sheet.UsedRange.RowHeight = 19;
                sheet["A1"].ColumnWidth = 29;
				
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

            DisposeRadioButton(rdBtn2003);
            rdBtn2003 = null;
            DisposeRadioButton(rdBtn2013);
            rdBtn2013 = null;

            DisposeButton(btnGenerateExcel);
            btnGenerateExcel = null;

            DisposeStackPanel(stackPnlOptions);
            stackPnlOptions = null;

            DisposeStackPanel(StackCreate);
            StackCreate = null;

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
