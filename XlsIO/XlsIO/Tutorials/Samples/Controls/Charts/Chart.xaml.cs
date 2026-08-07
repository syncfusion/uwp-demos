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
    public sealed partial class Chart : SampleLayout
    {
        public Chart()
        {
            this.InitializeComponent();
			if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
			{
                this.grdMain.Padding = new Thickness(10, 0, 0, 0);
                this.stackPnlOptions.Visibility = Visibility.Collapsed;
				this.btnGenerateExcel.HorizontalAlignment = HorizontalAlignment.Center;
				this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using Essential XlsIO.";
			}
			else
				this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using Essential XlsIO in the selected format.";
        }


        private async void btnGenerateExcel_Click(object sender, RoutedEventArgs e)
        {
            #region Setting output location
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "ChartSample";
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
                    storageFile = await local.CreateFileAsync("ChartSample.xls", CreationCollisionOption.ReplaceExisting);
                else
                    storageFile = await local.CreateFileAsync("ChartSample.xlsx", CreationCollisionOption.ReplaceExisting);
            }
                       
            if (storageFile == null)
                return;
            #endregion

            #region Initializing workbook
            //Instantiate excel Engine
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;

            if (rdBtn2003.IsChecked.Value)
                application.DefaultVersion = ExcelVersion.Excel97to2003;
            else
                application.DefaultVersion = ExcelVersion.Excel2013;

            IWorkbook workbook;

            if (application.DefaultVersion != ExcelVersion.Excel97to2003)
            {
                Assembly assembly = typeof(Chart).GetTypeInfo().Assembly;
                string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.Sparkline.xlsx";
                Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
                workbook = await application.Workbooks.OpenAsync(fileStream);
            }
            else
            {
                workbook = application.Workbooks.Create(1);
            }
            IWorksheet sheet = workbook.Worksheets[0];
            #endregion

//#if WINDOWS_APP
            if (application.DefaultVersion != ExcelVersion.Excel97to2003)
            {
                #region Sparklines

                #region WholeSale Report

                //A new Sparkline group is added to the sheet sparklinegroups
                ISparklineGroup sparklineGroup = sheet.SparklineGroups.Add();

                //Set the Sparkline group type as line
                sparklineGroup.SparklineType = SparklineType.Line;

                //Set to display the empty cell as line
                sparklineGroup.DisplayEmptyCellsAs = SparklineEmptyCells.Line;

                //Sparkline group style properties
                sparklineGroup.ShowFirstPoint = true;
                sparklineGroup.FirstPointColor = Color.FromArgb(Colors.Green.A, Colors.Green.R, Colors.Green.G, Colors.Green.B);
                sparklineGroup.ShowLastPoint = true;
                sparklineGroup.LastPointColor = Color.FromArgb(Colors.Orange.A, Colors.Orange.R, Colors.Orange.G, Colors.Orange.B);
                sparklineGroup.ShowHighPoint = true;
                sparklineGroup.HighPointColor = Color.FromArgb(Colors.Blue.A, Colors.Blue.R, Colors.Blue.G, Colors.Blue.B);
                sparklineGroup.ShowLowPoint = true;
                sparklineGroup.LowPointColor = Color.FromArgb(Colors.Purple.A, Colors.Purple.R, Colors.Purple.G, Colors.Purple.B);
                sparklineGroup.ShowMarkers = true;
                sparklineGroup.MarkersColor = Color.FromArgb(Colors.Black.A, Colors.Black.R, Colors.Black.G, Colors.Black.B);
                sparklineGroup.ShowNegativePoint = true;
                sparklineGroup.NegativePointColor = Color.FromArgb(Colors.Red.A, Colors.Red.R, Colors.Red.G, Colors.Red.B);

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
                sparklineGroup.HighPointColor = Color.FromArgb(Colors.Green.A, Colors.Green.R, Colors.Green.G, Colors.Green.B);
                sparklineGroup.ShowLowPoint = true;
                sparklineGroup.LowPointColor = Color.FromArgb(Colors.Red.A, Colors.Red.R, Colors.Red.G, Colors.Red.B);
                sparklineGroup.ShowNegativePoint = true;
                sparklineGroup.NegativePointColor = Color.FromArgb(Colors.Black.A, Colors.Black.R, Colors.Black.G, Colors.Black.B);

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
                sparklineGroup.AxisColor = Color.FromArgb(Colors.Black.A, Colors.Black.R, Colors.Black.G, Colors.Black.B);
                sparklineGroup.ShowFirstPoint = true;
                sparklineGroup.FirstPointColor = Color.FromArgb(Colors.Green.A, Colors.Green.R, Colors.Green.G, Colors.Green.B);
                sparklineGroup.ShowLastPoint = true;
                sparklineGroup.LastPointColor = Color.FromArgb(Colors.Orange.A, Colors.Orange.R, Colors.Orange.G, Colors.Orange.B);
                sparklineGroup.ShowNegativePoint = true;
                sparklineGroup.NegativePointColor = Color.FromArgb(Colors.Red.A, Colors.Red.R, Colors.Red.G, Colors.Red.B);

                sparklines = sparklineGroup.Add();

                dataRange = sheet.Range["D36:G46"];
                referenceRange = sheet.Range["H36:H46"];

                sparklines.Add(dataRange, referenceRange);

                #endregion

                #endregion
            }
            //#endif
            #region Creating chart datasource

            IWorksheet chartSheet;

            if (application.DefaultVersion != ExcelVersion.Excel97to2003)
            {
                chartSheet = workbook.Worksheets.Create("Chart Data");
            }
            else
            {
                chartSheet = workbook.Worksheets[0];
            }

            // Entering the Datas for the chart
            chartSheet.Range["A1"].Text = "Crescent City, CA";
            chartSheet.Range["A1:D1"].Merge();
            chartSheet.Range["A1"].CellStyle.Font.Bold = true;

            chartSheet.Range["B3"].Text = "Precipitation,in.";
            chartSheet.Range["C3"].Text = "Temperature,deg.F";

            chartSheet.Range["A4"].Text = "Jan";
            chartSheet.Range["A5"].Text = "Feb";
            chartSheet.Range["A6"].Text = "March";
            chartSheet.Range["A7"].Text = "Apr";
            chartSheet.Range["A8"].Text = "May";
            chartSheet.Range["A9"].Text = "June";
            chartSheet.Range["A10"].Text = "July";
            chartSheet.Range["A11"].Text = "Aug";
            chartSheet.Range["A12"].Text = "Sept";
            chartSheet.Range["A13"].Text = "Oct";
            chartSheet.Range["A14"].Text = "Nov";
            chartSheet.Range["A15"].Text = "Dec";

            chartSheet.Range["B4"].Number = 10.9;
            chartSheet.Range["B5"].Number = 8.9;
            chartSheet.Range["B6"].Number = 8.6;
            chartSheet.Range["B7"].Number = 4.8;
            chartSheet.Range["B8"].Number = 3.2;
            chartSheet.Range["B9"].Number = 1.4;
            chartSheet.Range["B10"].Number = 0.6;
            chartSheet.Range["B11"].Number = 0.7;
            chartSheet.Range["B12"].Number = 1.7;
            chartSheet.Range["B13"].Number = 5.4;
            chartSheet.Range["B14"].Number = 9.0;
            chartSheet.Range["B15"].Number = 10.4;

            chartSheet.Range["C4"].Number = 4.5;
            chartSheet.Range["C5"].Number = 2.7;
            chartSheet.Range["C6"].Number = 9.9;
            chartSheet.Range["C7"].Number = 4.2;
            chartSheet.Range["C8"].Number = 6.1;
            chartSheet.Range["C9"].Number = 5.3;
            chartSheet.Range["C10"].Number = 3.1;
            chartSheet.Range["C11"].Number = 7;
            chartSheet.Range["C12"].Number = 4.5;
            chartSheet.Range["C13"].Number = 8.4;
            chartSheet.Range["C14"].Number = 3.1;
            chartSheet.Range["C15"].Number = 8.8;
            chartSheet.UsedRange.AutofitColumns();
            #endregion

            //#if WINDOWS_APP
            #region Column Chart

            #region Creating ChartSheet with Wall implementation
            // Adding a New chart to the Existing Worksheet   
            IChart chart = workbook.Charts.Add("Column Chart");

                chart.DataRange = chartSheet.Range["A3:C15"];
                chart.ChartTitle = "Crescent City, CA";
                chart.IsSeriesInRows = false;

                chart.PrimaryValueAxis.Title = "Precipitation,in.";
                chart.PrimaryValueAxis.TitleArea.TextRotationAngle = 90;
                chart.PrimaryValueAxis.MaximumValue = 14.0;
                chart.PrimaryValueAxis.NumberFormat = "0.0";

                chart.PrimaryCategoryAxis.Title = "Month";

                IChartSerie serieOne = chart.Series[0];

                //set the Chart Type
                chart.ChartType = ExcelChartType.Column_Clustered_3D;

                //set the Backwall fill option
                chart.BackWall.Fill.FillType = ExcelFillType.Gradient;

                //set the Texture Type
                chart.BackWall.Fill.GradientColorType = ExcelGradientColor.TwoColor;
                chart.BackWall.Fill.GradientStyle = ExcelGradientStyle.Diagonl_Down;
                chart.BackWall.Fill.ForeColor = Colors.White;
                chart.BackWall.Fill.BackColor = Colors.Blue;

                //set the Border Linecolor 
                chart.BackWall.Border.LineColor = Colors.Brown;

                //set the Picture Type     
                chart.BackWall.PictureUnit = ExcelChartPictureType.stretch;

                //set the Backwall thickness
                chart.BackWall.Thickness = 10;

                //set the sidewall fill option
                chart.SideWall.Fill.FillType = ExcelFillType.SolidColor;

                //set the sidewall foreground and backcolor
                chart.SideWall.Fill.BackColor = Colors.White;
                chart.SideWall.Fill.ForeColor = Colors.White;

                //set the side wall Border color
                chart.SideWall.Border.LineColor = Colors.Red;

                //set floor fill option
                chart.Floor.Fill.FillType = ExcelFillType.Pattern;

                //set the floor pattern Type
                chart.Floor.Fill.Pattern = ExcelGradientPattern.Pat_Divot;

                //Set the floor fore and Back ground color
                chart.Floor.Fill.ForeColor = Colors.Blue;
                chart.Floor.Fill.BackColor = Colors.White;

                //set the floor thickness
                chart.Floor.Thickness = 3;
				
                //Set rotation angles
                chart.Rotation = 20;
                chart.Elevation = 15;				

                IChartSerie serieTwo = chart.Series[1];
                //Show value as data labels
                serieOne.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;
                serieTwo.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;


                serieTwo.Name = "Temperature,deg.F";

                // Legend setting
                chart.Legend.Position = ExcelLegendPosition.Right;
                chart.Legend.IsVerticalLegend = false;

                //Move the worksheet
                chartSheet.Move(1);

                
                #endregion

                #endregion
//#endif

                #region Pie Chart

                IWorksheet embeddedChartSheet = workbook.Worksheets.Create("Pie Chart");
            IChartShape embeddedChart = embeddedChartSheet.Charts.Add();

            embeddedChartSheet.Activate();
            embeddedChart.ChartTitle = "Precipitation in Months";
            embeddedChart.IsSeriesInRows = false;
            embeddedChart.ChartType = ExcelChartType.Pie;
            embeddedChart.DataRange = chartSheet["A4:B15"];
            embeddedChart.Series[0].DataPoints.DefaultDataPoint.DataLabels.IsValue = true;
            embeddedChart.Series[0].DataPoints.DefaultDataPoint.DataLabels.ShowLeaderLines = true;

            #endregion
#if WINDOWS_PHONE_APP
            workbook.Worksheets[0].Remove();
            embeddedChartSheet.Activate();
#endif

            #region Saving workbook and disposing objects

            await workbook.SaveAsAsync(storageFile);
            workbook.Close();
            excelEngine.Dispose();

            #endregion

            #region Save acknowledgement and launching of ouput file
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
