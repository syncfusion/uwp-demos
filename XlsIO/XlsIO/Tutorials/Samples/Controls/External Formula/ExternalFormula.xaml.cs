#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
using System.Reflection;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExternalFormula : SampleLayout
    {
        #region Constants
        private const string DEFAULTPATH = @"..\..\..\..\..\XlsIO\XlsIO\Tutorials\Samples\Assets\Resources\Templates\";
        #endregion

        public ExternalFormula()
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
            this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using Essential XlsIO.";
        }

        private async void btnGenerateExcel_Click(object sender, RoutedEventArgs e)
        {
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "ExternalFormula";
                savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                storageFile = await local.CreateFileAsync("ExternalFormula.xlsx", CreationCollisionOption.ReplaceExisting);
            }

            if (storageFile == null)
                return;
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            //Step 1 : Instantiate the spreadsheet creation engine.
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;

            application.DefaultVersion = ExcelVersion.Excel2013;

            //Assembly assembly = typeof(TemplateMarker).GetTypeInfo().Assembly;
            //string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.ExternalFormula.xlsx";
            //Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            //IWorkbook workbook = await application.Workbooks.OpenAsync(fileStream);
            //A new workbook is created.[Equivalent to creating a new workbook in MS Excel]
            //The new workbook will have 3 worksheets
            IWorkbook workbook = application.Workbooks.Create(1);

            IWorksheet worksheet  = workbook.Worksheets[0];

            string fullPath = Path.GetFullPath(DEFAULTPATH);
            fullPath = fullPath.Replace("\\SampleBrowser\\SampleBrowser\\", "\\SampleBrowser\\XlsIO\\");

            //External formula from another workboook
            worksheet.Range["A1"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$A$1";
            worksheet.Range["A2"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$A$2";
            worksheet.Range["A3"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$A$3";
            worksheet.Range["A4"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$A$4";
            worksheet.Range["A5"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$A$5";
            worksheet.Range["A6"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$A$6";
            worksheet.Range["A7"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$A$7";
            worksheet.Range["B1"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$B$1";
            worksheet.Range["B2"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$B$2";
            worksheet.Range["B3"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$B$3";
            worksheet.Range["B4"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$B$4";
            worksheet.Range["B5"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$B$5";
            worksheet.Range["B6"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$B$6";
            worksheet.Range["C1"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$C$1";
            worksheet.Range["C1"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$C$1";
            worksheet.Range["C2"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$C$2";
            worksheet.Range["C3"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$C$3";
            worksheet.Range["C4"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$C$4";
            worksheet.Range["C5"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$C$5";
            worksheet.Range["C6"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$C$6";
            worksheet.Range["D1"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$D$1";
            worksheet.Range["D2"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$D$2";
            worksheet.Range["D3"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$D$3";
            worksheet.Range["D4"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$D$4";
            worksheet.Range["D5"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$D$5";
            worksheet.Range["D6"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$D$6";
            worksheet.Range["E1"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$E$1";
            worksheet.Range["E2"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$E$2";
            worksheet.Range["E3"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$E$3";
            worksheet.Range["E4"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$E$4";
            worksheet.Range["E5"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$E$5";
            worksheet.Range["E6"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$E$6";
            worksheet.Range["F1"].Formula = @"='" + fullPath + "[External_Input.xlsx]Sheet1'!$F$1";
            worksheet.Range["B7"].Formula = "=SUM(B2:B6)";
            worksheet.Range["C7"].Formula = "=SUM(C2:C6)";
            worksheet.Range["D7"].Formula = "=SUM(D2:D6)";
            worksheet.Range["E7"].Formula = "=SUM(E2:E6)";
            worksheet.Range["F7"].Formula = "=SUM(F2:F6)";
            worksheet.Range["F2"].Formula = "=B2*C2+D2-E2";
            worksheet.Range["F3"].Formula = "=B3*C3+D3-E3";
            worksheet.Range["F4"].Formula = "=B4*C4+D4-E4";
            worksheet.Range["F5"].Formula = "=B5*C5+D5-E5";
            worksheet.Range["F6"].Formula = "=B6*C6+D6-E6";
            worksheet.Range["A1:F7"].CellStyle.Font.FontName = "Verdana";
            worksheet.Range["C2:F7"].NumberFormat = "_($* #,##0.00_)";
            worksheet.Range["A1:F1"].CellStyle.Color = Windows.UI.Color.FromArgb(0, 0, 112, 192);
            worksheet.Range["A7:F7"].CellStyle.Color = Windows.UI.Color.FromArgb(0, 0, 112, 192);
            worksheet.Range["A1:F1"].CellStyle.Font.Bold = true;
            worksheet.Range["A1:F1"].CellStyle.Font.Size = 11;
            worksheet.Columns[0].ColumnWidth = 17;
            worksheet.Columns[1].ColumnWidth = 13;
            worksheet.Columns[2].ColumnWidth = 11;
            worksheet.Columns[3].ColumnWidth = 11;
            worksheet.Columns[4].ColumnWidth = 13;
            worksheet.Columns[5].ColumnWidth = 13;

            worksheet.EnableSheetCalculations();
            worksheet["A1"].CalculatedValue.ToString();
            //workbook.CalculationOptions.CalculationMode = ExcelCalculationMode.Automatic;
            //workbook.CalculationOptions.RecalcOnSave = true;

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

            DisposeButton(btnGenerateExcel);
            btnGenerateExcel = null;

            DisposeStackPanel(stackPnlOptions);
            stackPnlOptions = null;

            DisposeStackPanel(StackCreate);
            StackCreate = null;

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
