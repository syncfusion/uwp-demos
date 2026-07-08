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
using System.Reflection;
using Syncfusion.Office;

namespace EssentialXlsIO
{
    public sealed partial class CreateMacro : SampleLayout
    {
        public CreateMacro()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                this.btnGenerateExcel.HorizontalAlignment = HorizontalAlignment.Center;
                this.grdMain.Padding = new Thickness(10, 0, 0, 0);
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
                savePicker.SuggestedFileName = "Create";
                if (rdBtnXls.IsChecked.Value)
                    savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xls" });
                else if (rdBtnXltm.IsChecked.Value)
                    savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xltm" });
                else
                    savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsm", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                if (rdBtnXls.IsChecked.Value)
                    storageFile = await local.CreateFileAsync("Create.xls", CreationCollisionOption.ReplaceExisting);
                else if(rdBtnXltm.IsChecked.Value)
                    storageFile = await local.CreateFileAsync("Create.xltm", CreationCollisionOption.ReplaceExisting);
                else
                    storageFile = await local.CreateFileAsync("Create.xlsm", CreationCollisionOption.ReplaceExisting);
            }

            if (storageFile == null)
                return;

            //Step 1 : Instantiate the spreadsheet creation engine.
            ExcelEngine excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            IApplication application = excelEngine.Excel;

            Assembly assembly = typeof(CreateMacro).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.CreateMacroTemplate.xlsx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            IWorkbook workbook = await application.Workbooks.OpenAsync(fileStream);
            workbook.Version = ExcelVersion.Excel2016;

            #region VbaProject
            //Access Vba Project from workbook
            IVbaProject vbaProject = workbook.VbaProject;

            IVbaModule vbaModule = vbaProject.Modules.Add("Module1", VbaModuleType.StdModule);
            vbaModule.Code = GetVbaCode();
            #endregion


            #region Saving the workbook
            ExcelSaveType type = ExcelSaveType.SaveAsMacro;

            if (storageFile.Name.EndsWith(".xls"))
            {
                workbook.Version = ExcelVersion.Excel97to2003;
                type = ExcelSaveType.SaveAsMacro;
            }
            else if (storageFile.Name.EndsWith(".xltm"))
                type = ExcelSaveType.SaveAsMacroTemplate;

            await workbook.SaveAsAsync(storageFile, type);
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


        /// <summary>
        /// Get the Vba code as string
        /// </summary>
        /// <param name="inputFile"></param>
        /// <returns>Vba</returns>
        private string GetVbaCode()
        {
            string vbaCode = "Sub Auto_Open()\n" +
                             "'\n" +
                             "' Auto_Open Macro\n" +
                             "'\n" +
                             "\n" +
                             "'\n" +
                                "Range(\"B3:C28\").Select\n" +
                                "Range(\"E3\").Activate\n" +
                                "Sheet1.Activate\n" +
                                "ActiveSheet.Shapes.AddChart2(276, xlAreaStacked).Select\n" +
                                "ActiveChart.SetSourceData Source:= Range(\"'Exchange Report'!$B$3:$C$28\")\n" +
                                "ActiveChart.Parent.Left = Range(\"F3\").Left + 3\n" +
                                "ActiveChart.Parent.Top = Range(\"F3\").Top\n" +
                                "ActiveChart.Parent.Width = Range(\"M3\").Left - ActiveChart.Parent.Left\n" +
                                "ActiveChart.Parent.Height = Range(\"F16\").Top - ActiveChart.Parent.Top\n" +
                                "ActiveChart.ChartTitle.Select\n" +
                                "ActiveChart.ChartTitle.Text = \"Open-Close Statistics\"\n" +
                                "Selection.Format.TextFrame2.TextRange.Characters.Text = \"Open-Close Statistics\"\n" +
                                "With Selection.Format.TextFrame2.TextRange.Characters(1, 21).Font\n" +
                                "    .BaselineOffset = 0\n" +
                                "    .Bold = msoFalse\n" +
                                "    .NameComplexScript = \" +mn-cs\"\n" +
                                "    .NameFarEast = \" +mn-ea\"\n" +
                                "    .Fill.Visible = msoTrue\n" +
                                "    .Fill.ForeColor.RGB = RGB(89, 89, 89)\n" +
                                "    .Fill.Transparency = 0\n" +
                                "    .Fill.Solid\n" +
                                "    .Size = 14\n" +
                                "    .Italic = msoFalse\n" +
                                "    .Kerning = 12\n" +
                                "    .Name = \" +mn-lt\"\n" +
                                "    .UnderlineStyle = msoNoUnderline\n" +
                                "    .Spacing = 0\n" +
                                "    .Strike = msoNoStrike\n" +
                                "End With\n" +
                                "ActiveChart.FullSeriesCollection(1).XValues = \"='Exchange Report'!$A$4:$A$28\"\n" +
                                "ActiveChart.ChartArea.Select\n" +
                                "ActiveChart.ChartTitle.Select\n" +
                                "Selection.Format.TextFrame2.TextRange.Font.Bold = msoTrue\n" +
                                "ActiveChart.ChartArea.Select\n" +
                                "ActiveChart.ChartTitle.Select\n" +
                                "Selection.Top = 8\n" +
                                "ActiveChart.ChartColor = 13\n" +
                             "End Sub";
            return vbaCode;
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

            DisposeRadioButton(rdBtnXls);
            rdBtnXls = null;
            DisposeRadioButton(rdBtnXltm);
            rdBtnXltm = null;
            DisposeRadioButton(rdBtnXlsm);
            rdBtnXlsm = null;

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