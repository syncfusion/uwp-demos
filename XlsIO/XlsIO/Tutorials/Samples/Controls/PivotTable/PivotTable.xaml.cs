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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PivotTable : SampleLayout
    {
        public PivotTable()
        {
            this.InitializeComponent();
			if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
			{
				this.btnCreate.HorizontalAlignment = HorizontalAlignment.Center;           
				this.grdMain.Margin = new Thickness(10);
				this.stackPnlOptions.Visibility = Visibility.Collapsed;
				this.textBox1.Text = "Please click the 'Create Pivot Table' button to view the excel file generated using Essential XlsIO.";
			}
			else
				this.textBox1.Text = "Please click the 'Create Pivot Table' button to view the excel file generated using Essential XlsIO.";

        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            #region Setting output location
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "PivotTableCreateSample";
                savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                storageFile = await local.CreateFileAsync("PivotTableCreateSample.xlsx", CreationCollisionOption.ReplaceExisting);
            }
            
            if (storageFile == null)
                return;
            #endregion

            #region Initializing Workbook
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            
            application.DefaultVersion = ExcelVersion.Excel2013;
           
            Assembly assembly = typeof(PivotTable).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.PivotCodeData.xlsx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            IWorkbook workbook = await application.Workbooks.OpenAsync(fileStream);
            IWorksheet dataSheet = workbook.Worksheets[0];
            IWorksheet pivotSheet = workbook.Worksheets[1];
            #endregion

            #region Creating Pivot Table 
            IPivotCache cache = workbook.PivotCaches.Add(dataSheet["A1:H50"]);

            //Insert the pivot table. 
            IPivotTable pivotTable = pivotSheet.PivotTables.Add("PivotTable1", pivotSheet["A1"], cache);
            pivotTable.Fields[2].Axis = PivotAxisTypes.Row;
            pivotTable.Fields[4].Axis = PivotAxisTypes.Row;
            pivotTable.Fields[3].Axis = PivotAxisTypes.Column;

            IPivotField field1 = pivotSheet.PivotTables[0].Fields[5];
            pivotTable.DataFields.Add(field1, "Sum of Units", PivotSubtotalTypes.Sum);

            pivotTable.ShowDrillIndicators = true;
            pivotTable.RowGrand = true;
            pivotTable.DisplayFieldCaptions = true;
            pivotTable.BuiltInStyle = PivotBuiltInStyles.PivotStyleMedium2;
            pivotSheet.Activate();
            
            #region Dynamic source change

            #region Adding rows
            string accountingFormat = "_($* #,##0.00_);_($* (#,##0.00);_($* \"-\"??_);_(@_)";
            string dateFormat = "[$-409]d-mmm-yy;@";

            dataSheet["A51"].NumberFormat = dateFormat;
            dataSheet["A51"].DateTime = DateTime.Parse("5/12/2012", CultureInfo.InvariantCulture);
            dataSheet["B51"].Formula = "=TEXT(A51,\"dddd\")";
            dataSheet["C51"].Value = "Central";
            dataSheet["D51"].Value = "Allan";
            dataSheet["E51"].Value = "Binder";
            dataSheet["F51"].Number = 87;
            dataSheet["G51"].Number = 3.21;
            dataSheet["G51"].NumberFormat = accountingFormat;
            dataSheet["H51"].Formula = "G51*F51";
            dataSheet["H51"].NumberFormat = accountingFormat;

            dataSheet["A52"].NumberFormat = dateFormat;
            dataSheet["A52"].DateTime = DateTime.Parse("5/15/2012", CultureInfo.InvariantCulture);
            dataSheet["B52"].Formula = "=TEXT(A52,\"dddd\")";
            dataSheet["C52"].Value = "Central";
            dataSheet["D52"].Value = "Andrew";
            dataSheet["E52"].Value = "Binder";
            dataSheet["F52"].Number = 95;
            dataSheet["G52"].Number = 2.48;
            dataSheet["G52"].NumberFormat = accountingFormat;
            dataSheet["H52"].Formula = "G52*F52";
            dataSheet["H52"].NumberFormat = accountingFormat;

            dataSheet["A53"].NumberFormat = dateFormat;
            dataSheet["A53"].DateTime = DateTime.Parse("5/18/2012", CultureInfo.InvariantCulture);
            dataSheet["B53"].Formula = "=TEXT(A53,\"dddd\")";
            dataSheet["C53"].Value = "West";
            dataSheet["D53"].Value = "Kevin";
            dataSheet["E53"].Value = "Binder";
            dataSheet["F53"].Number = 68;
            dataSheet["G53"].Number = 1.75;
            dataSheet["G53"].NumberFormat = accountingFormat;
            dataSheet["H53"].Formula = "G53*F53";
            dataSheet["H53"].NumberFormat = accountingFormat;

            dataSheet["A54"].NumberFormat = dateFormat;
            dataSheet["A54"].DateTime = DateTime.Parse("5/21/2012", CultureInfo.InvariantCulture);
            dataSheet["B54"].Formula = "=TEXT(A54,\"dddd\")";
            dataSheet["C54"].Value = "West";
            dataSheet["D54"].Value = "Jack";
            dataSheet["E54"].Value = "Binder";
            dataSheet["F54"].Number = 19;
            dataSheet["G54"].Number = 1.01;
            dataSheet["G54"].NumberFormat = accountingFormat;
            dataSheet["H54"].Formula = "G54*F54";
            dataSheet["H54"].NumberFormat = accountingFormat;

            dataSheet["A55"].NumberFormat = dateFormat;
            dataSheet["A55"].DateTime = DateTime.Parse("5/24/2012", CultureInfo.InvariantCulture);
            dataSheet["B55"].Formula = "=TEXT(A55,\"dddd\")";
            dataSheet["C55"].Value = "East";
            dataSheet["D55"].Value = "Allan";
            dataSheet["E55"].Value = "File Folder";
            dataSheet["F55"].Number = 20;
            dataSheet["G55"].Number = 0.28;
            dataSheet["G55"].NumberFormat = accountingFormat;
            dataSheet["H55"].Formula = "G55*F55";
            dataSheet["H55"].NumberFormat = accountingFormat;

            dataSheet["A56"].NumberFormat = dateFormat;
            dataSheet["A56"].DateTime = DateTime.Parse("5/27/2012", CultureInfo.InvariantCulture);
            dataSheet["B56"].Formula = "=TEXT(A56,\"dddd\")";
            dataSheet["C56"].Value = "East";
            dataSheet["D56"].Value = "Jack";
            dataSheet["E56"].Value = "File Folder";
            dataSheet["F56"].Number = 32;
            dataSheet["G56"].Number = 0.45;
            dataSheet["G56"].NumberFormat = accountingFormat;
            dataSheet["H56"].Formula = "G56*F56";
            dataSheet["H56"].NumberFormat = accountingFormat;

            dataSheet["A57"].NumberFormat = dateFormat;
            dataSheet["A57"].DateTime = DateTime.Parse("5/30/2012", CultureInfo.InvariantCulture);
            dataSheet["B57"].Formula = "=TEXT(A57,\"dddd\")";
            dataSheet["C57"].Value = "West";
            dataSheet["D57"].Value = "Kevin";
            dataSheet["E57"].Value = "Binder";
            dataSheet["F57"].Number = 23;
            dataSheet["G57"].Number = 1.19;
            dataSheet["G57"].NumberFormat = accountingFormat;
            dataSheet["H57"].Formula = "G57*F57";
            dataSheet["H57"].NumberFormat = accountingFormat;

            dataSheet["A58"].NumberFormat = dateFormat;
            dataSheet["A58"].DateTime = DateTime.Parse("6/2/2012", CultureInfo.InvariantCulture);
            dataSheet["B58"].Formula = "=TEXT(A58,\"dddd\")";
            dataSheet["C58"].Value = "West";
            dataSheet["D58"].Value = "Andrew";
            dataSheet["E58"].Value = "Binder";
            dataSheet["F58"].Number = 43;
            dataSheet["G58"].Number = 1.92;
            dataSheet["G58"].NumberFormat = accountingFormat;
            dataSheet["H58"].Formula = "G58*F58";
            dataSheet["H58"].NumberFormat = accountingFormat;
            #endregion

            //Adding new named range to the workbook
            IName name = workbook.Names.Add("DynamicRange", dataSheet.UsedRange);

            //Setting pivot table source range from named range
            workbook.PivotCaches[0].SourceRange = name.RefersToRange;
			pivotSheet.SetColumnWidth(1, 15.29);
            pivotSheet.SetColumnWidth(2, 15.29);
            pivotSheet.SetColumnWidth(14, 10.43);
            #endregion

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
