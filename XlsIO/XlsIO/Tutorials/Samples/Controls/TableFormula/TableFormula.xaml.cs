using Common;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Syncfusion.XlsIO;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TableFormula : SampleLayout
    {
        public TableFormula()
        {
            this.InitializeComponent();
			if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
			{
				this.btnCreate.HorizontalAlignment = HorizontalAlignment.Center;
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
                this.stackPnlOptions.Visibility = Visibility.Collapsed;
				this.textBox1.Text = "Please click the 'Create Table Formula' button to view the excel file generated using Essential XlsIO.";
			}
			else
				this.textBox1.Text = "Please click the 'Create Table Formula' button to view the excel file generated using Essential XlsIO.";

        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            #region Setting output location
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "TableFormulaSample";
                savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                storageFile = await local.CreateFileAsync("TableFormulaSample.xlsx", CreationCollisionOption.ReplaceExisting);
            }
            
            if (storageFile == null)
                return;
            #endregion

            #region Workbook Initialize
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;

            //Set the default version as Excel 2013
            application.DefaultVersion = ExcelVersion.Excel2013;

            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet sheet = workbook.Worksheets[0];
            #endregion

            #region Create Table
            // Create table
            IListObject table1 = sheet.ListObjects.Create("Table1", sheet["A1:F6"]);

            # region Table data
            // Fill table data
            sheet[1, 1].Text = "Product ID";
            sheet[1, 2].Text = "Quantity";
            sheet[1, 3].Text = "Rate";
            sheet[1, 4].Text = "Tax";
            sheet[1, 5].Text = "Discount";
            sheet[1, 6].Text = "Amount";

            sheet[2, 1].Text = "ProductA";
            sheet[2, 2].Number = 2;
            sheet[2, 3].Number = 16.80;
            sheet[2, 4].Number = 9.83;
            sheet[2, 5].Number = 10;

            sheet[3, 1].Text = "ProductB";
            sheet[3, 2].Number = 3;
            sheet[3, 3].Number = 15.60;
            sheet[3, 4].Number = 9.83;
            sheet[3, 5].Number = 5;

            sheet[4, 1].Text = "ProductC";
            sheet[4, 2].Number = 2;
            sheet[4, 3].Number = 20.10;
            sheet[4, 4].Number = 9.83;
            sheet[4, 5].Number = 8;

            sheet[5, 1].Text = "ProductD";
            sheet[5, 2].Number = 1;
            sheet[5, 3].Number = 40.50;
            sheet[5, 4].Number = 9.83;
            sheet[5, 5].Number = 20;

            sheet[6, 1].Text = "ProductE";
            sheet[6, 2].Number = 2;
            sheet[6, 3].Number = 30.70;
            sheet[6, 4].Number = 9.83;
            sheet[6, 5].Number = 15;
            # endregion

            // Create style for table number format
            IStyle style1 = workbook.Styles.Add("CurrencyFormat");
            style1.NumberFormat = "_($* #,##0.00_);_($* (#,##0.00);_($* \" - \"??_);_(@_)";

            // Apply number format
            sheet["C2:F6"].CellStyleName = "CurrencyFormat";

            // Apply builtin table style
            table1.BuiltInTableStyle = TableBuiltInStyles.TableStyleMedium9;

            //Set table column calculated formula
            table1.Columns[5].CalculatedFormula = "[@Quantity]*[@Rate]+[@Tax]-[@Discount]";

            //Show Total row and set total calculation
            table1.ShowTotals = true;
            table1.Columns[0].TotalsRowLabel = "Total";
            table1.Columns[1].TotalsCalculation = ExcelTotalsCalculation.Sum;
            table1.Columns[2].TotalsCalculation = ExcelTotalsCalculation.Sum;
            table1.Columns[3].TotalsCalculation = ExcelTotalsCalculation.Sum;
            table1.Columns[4].TotalsCalculation = ExcelTotalsCalculation.Sum;
            table1.Columns[5].TotalsCalculation = ExcelTotalsCalculation.Sum;

            sheet.SetColumnWidth(1, 11.71);
            sheet.SetColumnWidth(2, 10.29);
            sheet.SetColumnWidth(3, 8.29);
            sheet.SetColumnWidth(4, 7.29);
            sheet.SetColumnWidth(5, 10.29);
            sheet.SetColumnWidth(6, 9.71);


            #endregion

            #region Saving workbook and disposing objects

            await workbook.SaveAsAsync(storageFile);
            workbook.Close();
            excelEngine.Dispose();
            
            #endregion

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
            DisposeTextBlock(textBox4);
            textBox4 = null;
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
