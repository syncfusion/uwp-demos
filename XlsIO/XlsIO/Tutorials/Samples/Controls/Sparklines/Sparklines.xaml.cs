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
    public sealed partial class Sparklines : SampleLayout
    {
        public Sparklines()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
                stackPnlOptions.Visibility = Visibility.Collapsed;
                this.btnGenerateExcel.HorizontalAlignment = HorizontalAlignment.Center;
            }
            this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using Essential XlsIO.";
        }

        private async void btnGenerateExcel_Click(object sender, RoutedEventArgs e)
        {
            #region workbook Initialization
            //Instantiate the spreadsheet creation engine.
            ExcelEngine excelEngine = new ExcelEngine();

            //Instantiate the excel application object.
            IApplication application = excelEngine.Excel;

            //Set the default version as Excel 2010.
            application.DefaultVersion = ExcelVersion.Excel2010;

            Assembly assembly = typeof(Chart).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.Sparkline.xlsx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            IWorkbook workbook = await application.Workbooks.OpenAsync(fileStream);
                        
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];

            #endregion

            #region WholeSale Report

            //A new Sparkline group is added to the sheet sparklinegroups
            ISparklineGroup sparklineGroup = sheet.SparklineGroups.Add();

            //Set the Sparkline group type as line
            sparklineGroup.SparklineType = SparklineType.Line;

            //Set to display the empty cell as line
            sparklineGroup.DisplayEmptyCellsAs = SparklineEmptyCells.Line;

            //Sparkline group style properties
            sparklineGroup.ShowFirstPoint = true;
            sparklineGroup.FirstPointColor = Color.FromArgb(255, 0, 128, 0);
            sparklineGroup.ShowLastPoint = true;
            sparklineGroup.LastPointColor = Color.FromArgb(255, 255, 140, 0);
            sparklineGroup.ShowHighPoint = true;
            sparklineGroup.HighPointColor = Color.FromArgb(255, 0, 0, 139);
            sparklineGroup.ShowLowPoint = true;
            sparklineGroup.LowPointColor = Color.FromArgb(255, 148, 0 ,211);
            sparklineGroup.ShowMarkers = true;
            sparklineGroup.MarkersColor = Color.FromArgb(255, 0, 0, 0);
            sparklineGroup.ShowNegativePoint = true;
            sparklineGroup.NegativePointColor = Color.FromArgb(255, 255, 0, 0);

            //set the line weight
            sparklineGroup.LineWeight = 0.3;

            //The sparklines are added to the sparklinegroup.
            ISparklines sparklines = sparklineGroup.Add();

            //Set the Sparkline Datarange .
            IRange dataRange = sheet.Range["D6:G17"];
            //Set the Sparkline Reference range.
            IRange referenceRange = sheet.Range["H6:H17"];

            //Create a sparkline with the datarange and reference range.
            sparklines.Add(dataRange, referenceRange);



            #endregion

            #region Retail Trade

            //A new Sparkline group is added to the sheet sparklinegroups
            sparklineGroup = sheet.SparklineGroups.Add();

            //Set the Sparkline group type as column
            sparklineGroup.SparklineType = SparklineType.Column;

            //Set to display the empty cell as zero
            sparklineGroup.DisplayEmptyCellsAs = SparklineEmptyCells.Zero;

            //Sparkline group style properties
            sparklineGroup.ShowHighPoint = true;
            sparklineGroup.HighPointColor = Color.FromArgb(255, 0, 128, 0);
            sparklineGroup.ShowLowPoint = true;
            sparklineGroup.LowPointColor = Color.FromArgb(255, 255, 0, 0);
            sparklineGroup.ShowNegativePoint = true;
            sparklineGroup.NegativePointColor = Color.FromArgb(255, 0, 0 , 0);

            //The sparklines are added to the sparklinegroup.
            sparklines = sparklineGroup.Add();

            //Set the Sparkline Datarange .
            dataRange = sheet.Range["D21:G32"];
            //Set the Sparkline Reference range.
            referenceRange = sheet.Range["H21:H32"];

            //Create a sparkline with the datarange and reference range.
            sparklines.Add(dataRange, referenceRange);

            #endregion

            #region Manufacturing Trade

            //A new Sparkline group is added to the sheet sparklinegroups
            sparklineGroup = sheet.SparklineGroups.Add();

            //Set the Sparkline group type as win/loss
            sparklineGroup.SparklineType = SparklineType.ColumnStacked100;

            sparklineGroup.DisplayEmptyCellsAs = SparklineEmptyCells.Zero;

            sparklineGroup.DisplayAxis = true;
            sparklineGroup.AxisColor = Color.FromArgb(255, 0, 0, 0);
            sparklineGroup.ShowFirstPoint = true;
            sparklineGroup.FirstPointColor = Color.FromArgb(255, 0, 128, 0);
            sparklineGroup.ShowLastPoint = true;
            sparklineGroup.LastPointColor = Color.FromArgb(255, 255, 165, 0);
            sparklineGroup.ShowNegativePoint = true;
            sparklineGroup.NegativePointColor = Color.FromArgb(255, 255, 0, 0);

            sparklines = sparklineGroup.Add();

            dataRange = sheet.Range["D36:G46"];
            referenceRange = sheet.Range["H36:H46"];

            sparklines.Add(dataRange, referenceRange);

            #endregion

            #region Save the Workbook
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "Sparklines";
                savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                storageFile = await local.CreateFileAsync("Sparklines.xlsx", CreationCollisionOption.ReplaceExisting);
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
