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
using System.Globalization;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FormattingCells : SampleLayout
    {
        #region Class Members
        Color m_foreColor = Color.FromArgb(0, 255, 255, 255);
        Color m_darkBorder = Color.FromArgb(0, 57, 93, 148);
        Color m_lightBody = Color.FromArgb(0, 198, 215, 239);
        Color m_lightContent = Color.FromArgb(0, 231, 235, 247);
        #endregion

        #region Class Properties
        private Color ForeColor
        {
            get
            {
                return m_foreColor;
            }
            set
            {
                m_foreColor = value;
            }
        }
        private Color DarkBorder
        {
            get
            {
                return m_darkBorder;
            }
            set
            {
                m_darkBorder = value;
            }
        }
        private Color LightBody
        {
            get
            {
                return m_lightBody;
            }
            set
            {
                m_lightBody = value;
            }
        }
        private Color LightContent
        {
            get
            {
                return m_lightContent;
            }
            set
            {
                m_lightContent = value;
            }
        }
        #endregion

        #region Initializing Methods
        public FormattingCells()
        {
            this.InitializeComponent();
            InitializeControls();          
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
			{
				this.btnGenerateExcel.HorizontalAlignment = HorizontalAlignment.Center;            
				this.grdMain.Padding = new Thickness(10,0,0,0);
				this.stackPnlOptions.Visibility = Visibility.Collapsed;
				this.txtBlock1.Visibility = Visibility.Collapsed;
				this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using Essential XlsIO.";
			}
			else
            this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using Essential XlsIO in the selected format.";

        }
        /// <summary>
        /// Initializing controls to load default values
        /// </summary>
        private void InitializeControls()
        {
            InitializeColorCombo();
        }

        private void InitializeColorCombo()
        {
            var colors = typeof(Colors).GetTypeInfo().DeclaredProperties;
            foreach (var item in colors)
            {
                cbBorderColor.Items.Add(item);
            }
            cbBorderColor.SelectedIndex = 130;
        }
        #endregion

        #region Theme Selection Event
        private void cbBorderColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBorderColor.SelectedIndex != -1)
            {
                PropertyInfo propInfo = cbBorderColor.SelectedItem as PropertyInfo;
                Color color = (Color)propInfo.GetValue(null);

                byte red = color.R;
                byte green = color.G;
                byte blue = color.B;

                DarkBorder = Color.FromArgb(0, red, green, blue);

                LightBody = Color.FromArgb(0, (((red + 100) >= 255) ? Convert.ToByte(255) : Convert.ToByte(red + 100)),
                                              (((green + 100) >= 255) ? Convert.ToByte(255) : Convert.ToByte(green + 100)),
                                              (((blue + 100) >= 255) ? Convert.ToByte(255) : Convert.ToByte(blue + 100)));

                LightContent = Color.FromArgb(0, (((red + 150) >= 255) ? Convert.ToByte(255) : Convert.ToByte(red + 150)),
                                                 (((green + 150) >= 255) ? Convert.ToByte(255) : Convert.ToByte(green + 150)),
                                                 (((blue + 150) >= 255) ? Convert.ToByte(255) : Convert.ToByte(blue + 150)));
            }



        }
        #endregion

        #region Generate Excel
        private async void btnGenerateExcel_Click(object sender, RoutedEventArgs e)
        {
            #region Workbook initialization
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
            else
            {
                application.DefaultVersion = ExcelVersion.Excel2013;
            }

            //A new workbook is created.[Equivalent to creating a new workbook in MS Excel]
            //The new workbook will have 3 worksheets
            IWorkbook workbook = application.Workbooks.Create(1);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet worksheet = workbook.Worksheets[0];
            worksheet.IsGridLinesVisible = false;
            #endregion

            # region RTF
            //Insert Rich Text
            IRange range = worksheet.Range["D3"];
            range.Text = "Employee Report";
            IRichTextString rtf1 = range.RichText;

            //Formatting the text
            IFont redFont = workbook.CreateFont();
            redFont.Size = 16;
            redFont.Bold = true;
            redFont.Italic = true;
            redFont.RGBColor = DarkBorder;
            rtf1.SetFont(0, 14, redFont);
            #endregion

            #region Number Formatting
            worksheet.Range["H24"].Number = 5000;
            worksheet.Range["H24"].NumberFormat = "$#,##0.00";
            worksheet.Range["H14"].NumberFormat = "dd/mm/yyyy";
            worksheet.Range["H14"].DateTime = DateTime.Parse(" 8/3/1963", CultureInfo.InvariantCulture);
            worksheet.Range["H16"].NumberFormat = "mm/dd/yyyy";
            worksheet.Range["H16"].DateTime = DateTime.Parse(" 4/1/1992", CultureInfo.InvariantCulture);
            #endregion

            # region Alignment settings

            # region Text alignment
            worksheet.Range["F10:F24"].HorizontalAlignment = ExcelHAlign.HAlignRight;
            worksheet.Range["D3"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
            worksheet.Range["H10:H23"].HorizontalAlignment = ExcelHAlign.HAlignLeft;
            worksheet.Range["F10:F24"].VerticalAlignment = ExcelVAlign.VAlignCenter;
            #endregion

            #region Text Control
            worksheet.Range["F26"].WrapText = true;
            worksheet.Range["F26"].Text = "Antony Jose graduated from St. Andrews University, Scotland, with a BSC degree in 1976.  Upon joining the company as a sales representative in 1992, he spent 6 months in an orientation program at the Seattle office and then returned to his permanent post in London.  He was promoted to sales manager in March 1993.";
            worksheet.Range["F26"].VerticalAlignment = ExcelVAlign.VAlignCenter;
            #region RTF
            IRichTextString rtf2 = worksheet.Range["F26"].RichText;

            //Formatting the text
            IFont boldFont = workbook.CreateFont();
            IFont boldItalicFont = workbook.CreateFont();
            IFont italicFont = workbook.CreateFont();
            IFont boldItalicColorFont = workbook.CreateFont();

            boldFont.Bold = true;

            boldItalicFont.Bold = true;
            boldItalicFont.Italic = true;

            italicFont.Italic = true;

            boldItalicColorFont.Bold = true;
            boldItalicColorFont.Italic = true;
            boldItalicColorFont.Color = Syncfusion.XlsIO.ExcelKnownColors.Blue;

            rtf2.SetFont(0, 10, boldFont);
            rtf2.SetFont(82, 85, italicFont);
            rtf2.SetFont(118, 138, boldFont);
            rtf2.SetFont(143, 146, boldItalicFont);
            rtf2.SetFont(286, 299, boldItalicColorFont);
            rtf2.SetFont(309, 312, italicFont);
            #endregion

            #endregion

            #region Cell merging
            worksheet.Range["F26:H28"].Merge();
            worksheet.Range["D3:F3"].Merge();
            worksheet.Range["B7:J8"].Merge();
            worksheet.Range["F26"].RowHeight = 15;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                worksheet.Range["F27"].RowHeight = 78;
            else
            {
                if (workbook.Version != ExcelVersion.Excel97to2003)
                    worksheet.Range["F27"].RowHeight = 89;
                else
                    worksheet.Range["F27"].RowHeight = 65;
            }
            worksheet.Range["H10:H24"].ColumnWidth = 25;
            worksheet.Range["B10:C28"].ColumnWidth = 1;
            worksheet.Range["E10:E28"].ColumnWidth = 5;
            worksheet.Range["D10:D28"].ColumnWidth = 15;
            #endregion

            #region Text Direction
            worksheet.Range["B7"].Text = "Antony Jose";
            worksheet.Range["B7"].CellStyle.ReadingOrder = Syncfusion.XlsIO.ExcelReadingOrderType.LeftToRight;
            worksheet.Range["B7"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
            worksheet.Range["B7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
            #endregion

            #region Text Indent

            worksheet.Range["D7"].CellStyle.IndentLevel = 6;

            #endregion


            #endregion

            #region Font settings

            // Setting Font Type
            worksheet.Range["F10:F24"].CellStyle.Font.FontName = "Arial";
            worksheet.Range["D3"].CellStyle.Font.FontName = "Arial";
            worksheet.Range["B7"].CellStyle.Font.FontName = "Arial";
            worksheet.Range["B7"].CellStyle.Font.Size = 12f;
            worksheet.Range["B7"].CellStyle.Font.Bold = true;

            // Setting Font Styles
            worksheet.Range["F10:F24"].CellStyle.Font.Bold = true;
            worksheet.Range["D3"].CellStyle.Font.Bold = true;

            // Setting Font Size
            worksheet.Range["F10:F24"].CellStyle.Font.Size = 10;
            worksheet.Range["D3"].CellStyle.Font.Size = 14;
            worksheet.Range["F27:H28"].CellStyle.Font.Size = 9f;




            // Setting UnderLine 
            worksheet.Range["D3"].CellStyle.Font.Underline = ExcelUnderline.Double;

            worksheet.Range["F10"].Text = "Name";
            worksheet.Range["F12"].Text = "Title";
            worksheet.Range["F14"].Text = "Birth Date";
            worksheet.Range["F16"].Text = "Hire date";
            worksheet.Range["F18"].Text = "Home phone";
            worksheet.Range["F20"].Text = "Extension";
            worksheet.Range["F22"].Text = "Home address";
            worksheet.Range["F24"].Text = "Salary";

            worksheet.Range["H10"].Text = "Antony Jose";
            worksheet.Range["H12"].Text = "Sales Manager";
            worksheet.Range["H18"].Text = "(206) 555-3412";
            worksheet.Range["H20"].Number = 3355;
            worksheet.Range["H22"].Text = "722 Moss Bay Blvd";


            #endregion

            #region Insert Image
            //Get the Path of the Image
            Assembly assembly = typeof(FormattingCells).GetTypeInfo().Assembly;
            Stream imageStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.EMPID1.png");
            int scaleWidth = (int)application.ConvertUnits((int)worksheet["C10"].ColumnWidth, MeasureUnits.Millimeter, MeasureUnits.Pixel) +
                             (int)application.ConvertUnits((int)worksheet["D10"].ColumnWidth, MeasureUnits.Millimeter, MeasureUnits.Pixel);
            worksheet.Pictures.AddPicture(10, 3, imageStream, scaleWidth, 65);
            #endregion

            #region Border setting

            worksheet.Range["H10"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            worksheet.Range["H10"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            worksheet.Range["H10"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            worksheet.Range["H12"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            worksheet.Range["H12"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            worksheet.Range["H12"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            worksheet.Range["H14"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            worksheet.Range["H14"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            worksheet.Range["H14"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            worksheet.Range["H16"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            worksheet.Range["H16"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            worksheet.Range["H16"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            worksheet.Range["H18"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            worksheet.Range["H18"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            worksheet.Range["H18"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            worksheet.Range["H20"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            worksheet.Range["H20"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            worksheet.Range["H20"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            worksheet.Range["H22"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            worksheet.Range["H22"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            worksheet.Range["H22"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            worksheet.Range["H24"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            worksheet.Range["H24"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            worksheet.Range["H24"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            worksheet.Range["F26:H28"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            worksheet.Range["F26:H28"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            worksheet.Range["F26:H28"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            #endregion

            #region Applying Theme
            // Setting Font Color
            worksheet.Range["B7"].CellStyle.Font.RGBColor = ForeColor;
            worksheet.Range["D28"].CellStyle.Font.RGBColor = ForeColor;

            #region Pattern settings
            worksheet.Range["B9:I31"].CellStyle.Color = LightBody;
            worksheet.Range["A7:J8"].CellStyle.Color = DarkBorder;
            worksheet.Range["A7:A32"].CellStyle.Color = DarkBorder;
            worksheet.Range["A7:A32"].ColumnWidth = 2.29;
            worksheet.Range["J7:J32"].CellStyle.Color = DarkBorder;
            worksheet.Range["J7:J32"].ColumnWidth = 2.29;
            worksheet.Range["A32:J32"].CellStyle.Color = DarkBorder;

            worksheet.Range["F26:H28"].CellStyle.Color = LightContent;
            #endregion

            #endregion

            #region Save the Workbook
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "FormattingCellsSample";
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
                    storageFile = await local.CreateFileAsync("FormattingCellsSample.xls", CreationCollisionOption.ReplaceExisting);
                else
                    storageFile = await local.CreateFileAsync("FormattingCellsSample.xlsx", CreationCollisionOption.ReplaceExisting);
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
            DisposeTextBlock(txtBlock1);
            txtBlock1 = null;
            DisposeTextBlock(txtBlock2);
            txtBlock2 = null;
            DisposeTextBlock(txtBlock3);
            txtBlock3 = null;

            DisposeRadioButton(rdbExcel2003);
            rdbExcel2003 = null;
            DisposeRadioButton(rdbExcel2003);
            rdbExcel2003 = null;

            DisposeButton(btnGenerateExcel);
            btnGenerateExcel = null;

            DisposeStackPanel(stackPnlOptions);
            stackPnlOptions = null;

            DisposeStackPanel(stackPnlOptions_1);
            stackPnlOptions_1 = null;

            cbBorderColor = null;

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
