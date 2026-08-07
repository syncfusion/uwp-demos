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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AutoShapes : SampleLayout
    {
        public AutoShapes()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                this.btnCreate.HorizontalAlignment = HorizontalAlignment.Center;

                this.grdMain.Margin = new Thickness(10);
            }

            this.textBox1.Text = "Please click the 'Create AutoShapes' button to view the excel file generated using Essential XlsIO.";
        }
        
        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            #region Setting output location

           
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "AutoShapesSample";
                savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                storageFile = await local.CreateFileAsync("AutoShapesSample.xlsx", CreationCollisionOption.ReplaceExisting);
            }
            
            if (storageFile == null)
                return;
            #endregion

            #region Initializing Workbook
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;

            //if (this.rdBtn2013.IsChecked.Value)
               application.DefaultVersion = ExcelVersion.Excel2013;
            //else if (this.rdBtn2010.IsChecked.Value)
            //    application.DefaultVersion = ExcelVersion.Excel2010;
            //else
             //   application.DefaultVersion = ExcelVersion.Excel2007;


            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet worksheet= workbook.Worksheets[0];
            #endregion

           	#region AddAutoShapes
            IShape shape;
            string text;

            IFont font = workbook.CreateFont();
            font.Color = ExcelKnownColors.White;
            font.Italic = true;
            font.Size = 12;


            IFont font2 = workbook.CreateFont();
            font2.Color = ExcelKnownColors.Black;
            font2.Size = 15;
            font2.Italic = true;
            font2.Bold = true;

            text = "Requirement";
            shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.RoundedRectangle, 2, 7, 60, 192);
            shape.TextFrame.TextRange.Text = text;
            shape.TextFrame.TextRange.RichText.SetFont(0, text.Length - 1, font);
            shape.TextFrame.TextRange.RichText.SetFont(0,0, font2);
            shape.Fill.ForeColorIndex = ExcelKnownColors.Light_blue;
            shape.Line.Visible = false;
            shape.TextFrame.VerticalAlignment = ExcelVerticalAlignment.MiddleCentered;

            shape=worksheet.Shapes.AddAutoShapes(AutoShapeType.DownArrow, 5, 8, 40, 64);
            shape.Fill.ForeColorIndex = ExcelKnownColors.White;
            shape.Line.ForeColorIndex = ExcelKnownColors.Blue;
            shape.Line.Weight = 1;

            text = "Design";
            shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.RoundedRectangle, 7, 7, 60, 192);
            shape.TextFrame.TextRange.Text = text;
            shape.TextFrame.TextRange.RichText.SetFont(0, text.Length - 1, font);
            shape.TextFrame.TextRange.RichText.SetFont(0, 0, font2);
            shape.Line.Visible = false;
            shape.Fill.ForeColorIndex = ExcelKnownColors.Light_orange;
            shape.TextFrame.VerticalAlignment = ExcelVerticalAlignment.MiddleCentered;


            shape=worksheet.Shapes.AddAutoShapes(AutoShapeType.DownArrow, 10, 8, 40, 64);
            shape.Fill.ForeColorIndex = ExcelKnownColors.White;
            shape.Line.ForeColorIndex = ExcelKnownColors.Blue;
            shape.Line.Weight = 1;

            text = "Execution";
            shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.RoundedRectangle, 12, 7, 60, 192);
            shape.TextFrame.TextRange.Text = text;
            shape.TextFrame.TextRange.RichText.SetFont(0, text.Length - 1, font);
            shape.TextFrame.TextRange.RichText.SetFont(0, 0, font2);
            shape.Line.Visible = false;
            shape.Fill.ForeColorIndex = ExcelKnownColors.Blue;
            shape.TextFrame.VerticalAlignment = ExcelVerticalAlignment.MiddleCentered;

            shape=worksheet.Shapes.AddAutoShapes(AutoShapeType.DownArrow, 15, 8, 40, 64);
            shape.Fill.ForeColorIndex = ExcelKnownColors.White;
            shape.Line.ForeColorIndex = ExcelKnownColors.Blue;
            shape.Line.Weight = 1;

            text = "Testing";
            shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.RoundedRectangle, 17, 7, 60, 192);
            shape.TextFrame.TextRange.Text = text;
            shape.TextFrame.TextRange.RichText.SetFont(0, text.Length - 1, font);
            shape.TextFrame.TextRange.RichText.SetFont(0, 0, font2);
            shape.Line.Visible = false;
            shape.Fill.ForeColorIndex = ExcelKnownColors.Green;
            shape.TextFrame.VerticalAlignment = ExcelVerticalAlignment.MiddleCentered;

            shape=worksheet.Shapes.AddAutoShapes(AutoShapeType.DownArrow, 20, 8, 40, 64);
            shape.Fill.ForeColorIndex = ExcelKnownColors.White;
            shape.Line.ForeColorIndex = ExcelKnownColors.Blue;
            shape.Line.Weight = 1;

            text = "Release";
            shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.RoundedRectangle, 22, 7, 60, 192);
            shape.TextFrame.TextRange.Text = text;
            shape.TextFrame.TextRange.RichText.SetFont(0, text.Length - 1, font);
            shape.TextFrame.TextRange.RichText.SetFont(0, 0, font2);
            shape.Line.Visible = false;
            shape.Fill.ForeColorIndex = ExcelKnownColors.Lavender;
            shape.TextFrame.VerticalAlignment = ExcelVerticalAlignment.MiddleCentered;
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
                return ;
            textBlock.ClearValue(TextBlock.FontFamilyProperty);
            textBlock.ClearValue(TextBlock.FontSizeProperty);
            textBlock.ClearValue(TextBlock.TextProperty);
            textBlock.ClearValue(TextBlock.TextWrappingProperty);
            textBlock.ClearValue(TextBlock.ForegroundProperty);
        }

        private void DisposeButton(Button button)
        {
            if (button == null)
                return ;
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
            if (stackPanel== null)
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
