using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Windows.UI;
using Windows.UI.Xaml;
//using Common;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Pickers;
using Syncfusion.XlsIO;
using Windows.UI.Popups;
using Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Performance : SampleLayout
    {
        

        #region Initializing Methods
        public Performance()
        {
            this.InitializeComponent();
            this.InitializeVariables();
			if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
			{
				this.btnGenerateExcel.HorizontalAlignment = HorizontalAlignment.Center;
                this.grdMain.Padding = new Thickness(10, 0, 0, 0);
                this.stackPnlOptions.Visibility = Visibility.Collapsed;
				this.cbRows.Text = "10000";
				this.textBlock1.Visibility = Visibility.Collapsed;
				this.textBlock2.Visibility = Visibility.Collapsed;
				this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using Essential XlsIO.";
			}
			else
				this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using Essential XlsIO in the selected format.";

        }

        private void InitializeVariables()
        {
           
        }
        #endregion

        #region Generate Excel
        private async void btnGenerateExcel_Click(object sender, RoutedEventArgs e)
        {
            #region Workbook initialization
            int rowCount = Convert.ToInt32(cbRows.Text);
            int colCount = Convert.ToInt32(cbColumns.Text);
			MessageDialog dialog=null;
            if (rdbExcel2003.IsChecked.Value && colCount > 256)
            {
                dialog = new MessageDialog("Maximum number of columns allowed for Excel 2003 format is 256. Please select Excel 2007 to 2013 format if you need more than 256 columns.");
                await dialog.ShowAsync();
            }
            else if (rdbExcel2003.IsChecked.Value && rowCount > 65536)
            {
                dialog = new MessageDialog("Maximum number of rows allowed for Excel 2003 format is 65,536. Please select Excel 2007 to 2013 format if you need more than 65,536 rows.");
                await dialog.ShowAsync();
            }
            else
            {
                System.Diagnostics.Stopwatch watcher = new System.Diagnostics.Stopwatch();
                watcher.Start();
                //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
                //The instantiation process consists of two steps.

                //Step 1 : Instantiate the spreadsheet creation engine.
                ExcelEngine excelEngine = new ExcelEngine();
                //Step 2 : Instantiate the excel application object.
                IApplication application = excelEngine.Excel;

                //Set the Default Version as Excel 97to2003
                if (this.rdbExcel2003.IsChecked == true)
                {
                    application.DefaultVersion = ExcelVersion.Excel97to2003;
                }
                ////Set the Default Version as Excel 2007
                //else if (this.rdbExcel2007.IsChecked == true)
                //{
                //    application.DefaultVersion = ExcelVersion.Excel2007;
                //}
                //else if (this.rdbExcel2010.IsChecked == true)
                //{
                //    application.DefaultVersion = ExcelVersion.Excel2010;
                //}
                else if (this.rdbExcel2013.IsChecked == true)
                {
                    application.DefaultVersion = ExcelVersion.Excel2013;
                }

                //A new workbook is created.[Equivalent to creating a new workbook in MS Excel]
                //The new workbook will have 3 worksheets
                IWorkbook workbook = application.Workbooks.Create(1);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet worksheet = workbook.Worksheets[0];
            #endregion

                #region FillData

                IMigrantRange migrantRange = worksheet.MigrantRange;


                for (int column = 1; column <= colCount; column++)
                {
                    migrantRange.ResetRowColumn(1, column);
                    migrantRange.Text = "Column: " + column.ToString();
//#if WINDOWS_PHONE_APP
                    worksheet.SetColumnWidth(column,9);
//#endif
                }

                //Writing Data using normal interface
                for (int row = 2; row <= rowCount; row++)
                {
                    //double columnSum = 0.0; 
                    for (int column = 1; column <= colCount; column++)
                    {
                        //Writing number
                        migrantRange.ResetRowColumn(row, column);
                        migrantRange.Number = row * column;
                    }
                }
                watcher.Stop();
                LogDetails(watcher.Elapsed);
                #endregion

                #region Save the Workbook
                StorageFile storageFile;
                if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
                {
                    FileSavePicker savePicker = new FileSavePicker();
                    savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                    savePicker.SuggestedFileName = "Performance";
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
                        storageFile = await local.CreateFileAsync("Performance.xls", CreationCollisionOption.ReplaceExisting);
                    else
                        storageFile = await local.CreateFileAsync("Performance.xlsx", CreationCollisionOption.ReplaceExisting);
                }

                //Saving the workbook to disk.
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
        }

        private void LogDetails(TimeSpan timeSpan)
        {
            //#if WINDOWS_APP
            string s = "Number of rows : " + this.cbRows.Text + "\r\n" + "Number of columns: " + this.cbColumns.Text+"\r\n";
            this.lblStatus.Text = s + "\r\n" + "Time taken : " + timeSpan.Minutes + "Mins : " + timeSpan.Seconds + "secs : " + timeSpan.Milliseconds + "msec";
//#else
//            string s = "Number of rows : " + this.cbRows.Text + "\r\n" + "Number of columns: " + this.cbColumns.Text;
//            this.lblStatus.Text = s + "\r\n" + "Time taken : " + timeSpan.Minutes + "Mins : " + timeSpan.Seconds + "secs : " + timeSpan.Milliseconds + "msec";
//#endif
        }
        #endregion

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
            DisposeTextBlock(textBlock1);
            textBlock1 = null;
            DisposeTextBlock(textBlock2);
            textBlock2 = null;
            DisposeTextBlock(textBlock3);
            textBlock3 = null;
            DisposeTextBlock(lblStatus);
            lblStatus = null;
                       
            DisposeRadioButton(rdbExcel2003);
            rdbExcel2003 = null;
            DisposeRadioButton(rdbExcel2013);
            rdbExcel2013 = null;

            DisposeButton(btnGenerateExcel);
            btnGenerateExcel = null;

            DisposeStackPanel(stackPnlOptions);
            stackPnlOptions = null;

            DisposeStackPanel(stackPnlOptions_1);
            stackPnlOptions_1 = null;

            DisposeStackPanel(stackPnlOptions_2);
            stackPnlOptions_2 = null;

            cbColumns = null;
            cbRows = null;

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
