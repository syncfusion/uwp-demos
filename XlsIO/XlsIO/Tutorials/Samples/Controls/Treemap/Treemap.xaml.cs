#region Copyright Syncfusion Inc. 2001 - 2016
// Copyright Syncfusion Inc. 2001 - 2016. All rights reserved.
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
using System.Reflection;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
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
    public sealed partial class Treemap : SampleLayout
    {
        public Treemap()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Padding = new Thickness(10, 0, 0, 0);
                this.stackPnlOptions.Visibility = Visibility.Collapsed;
                this.btnGenerateExcel.HorizontalAlignment = HorizontalAlignment.Center;
                this.textBox1.Text = "Please click the 'Create Chart' button to view the Excel file generated using Essential XlsIO.";
            }
            else
                this.textBox1.Text = "Please click the 'Create Chart' button to view the Excel file generated using Essential XlsIO in the selected format.";
        }

        private async void btnGenerateExcel_Click(object sender, RoutedEventArgs e)
        {
            StorageFile storageFile;
            string fileName = "Treemap_Chart.xlsx";
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = fileName;
                savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                storageFile = await local.CreateFileAsync("Treemap_Chart.xlsx", CreationCollisionOption.ReplaceExisting);
            }

            if (storageFile == null)
                return;


            #region Initializing Workbook
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            //Step 1 : Instantiate the spreadsheet creation engine.
            ExcelEngine excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the Excel application object.
            IApplication application = excelEngine.Excel;

            application.DefaultVersion = ExcelVersion.Excel2016;

            Assembly assembly = typeof(Treemap).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.TreemapTemplate.xlsx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            IWorkbook workbook = await application.Workbooks.OpenAsync(fileStream);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            #endregion

            #region Chart Creation
            IChart chart = null;

            if (this.rdBtnSheet.IsChecked != null && this.rdBtnSheet.IsChecked.Value)
                chart = workbook.Charts.Add();
            else
                chart = workbook.Worksheets[0].Charts.Add();

            #region Treemap Chart Settings
            chart.ChartType = ExcelChartType.TreeMap;
            chart.DataRange = sheet["A1:F13"];
            chart.ChartTitle = "Daily Food Sales";
            foreach (IChartSerie serie in chart.Series)
            {
                serie.SerieFormat.TreeMapLabelOption = ExcelTreeMapLabelOption.Banner;
            }
            #endregion         

            chart.Legend.Position = ExcelLegendPosition.Top;

            if (this.rdBtnSheet.IsChecked != null && this.rdBtnSheet.IsChecked.Value)
                chart.Activate();
            else
            {
                workbook.Worksheets[0].Activate();
                IChartShape chartShape = chart as IChartShape;
                chartShape.TopRow = 1;
                chartShape.BottomRow = 22;
                chartShape.LeftColumn = 8;
                chartShape.RightColumn = 15;
            }
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

        private async void btnInputTemplate_Click(object sender, RoutedEventArgs e)
        {
            #region Workbook initialization
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;

            application.DefaultVersion = ExcelVersion.Excel2016;

            Assembly assembly = typeof(Treemap).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.TreemapTemplate.xlsx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            IWorkbook workbook = await application.Workbooks.OpenAsync(fileStream);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet worksheet = workbook.Worksheets[0];
            #endregion

            #region Save the Workbook
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "InputTemplate";
                savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                storageFile = await local.CreateFileAsync("InputTemplate.xlsx", CreationCollisionOption.ReplaceExisting);
            }

            if (storageFile != null)
            {
                //Saving the workbook
                await workbook.SaveAsAsync(storageFile);
                workbook.Close();
                excelEngine.Dispose();

                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been saved successfully.");

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

            DisposeRadioButton(rdBtnEmbed);
            rdBtnEmbed = null;
            DisposeRadioButton(rdBtnSheet);
            rdBtnSheet = null;

            DisposeButton(btnGenerateExcel);
            btnGenerateExcel.Click -= btnGenerateExcel_Click;
            btnGenerateExcel = null;

            DisposeButton(btnTemplate);
            btnTemplate.Click -= btnInputTemplate_Click;
            btnTemplate = null;

            DisposeStackPanel(stackPnlOptions);
            stackPnlOptions = null;

            DisposeStackPanel(stackPnlOptions_3);
            stackPnlOptions_3 = null;

            DisposeGrid(grd1);
            grd1 = null;

            DisposeGrid(grd2);
            grd2 = null;

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
