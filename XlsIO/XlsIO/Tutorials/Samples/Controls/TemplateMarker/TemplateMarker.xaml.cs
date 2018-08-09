using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Windows.UI;
using Windows.UI.Xaml;
using Common;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Pickers;
using Syncfusion.XlsIO;
using Windows.UI.Popups;
using System.Xml.Linq;
using System.Linq;
//using SampleBrowser;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TemplateMarker : SampleLayout
    {

        #region Initializing Methods
        public TemplateMarker()
        {
            this.InitializeComponent();
            InitializeControls();          
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
			{
				this.btnGenerateExcel.HorizontalAlignment = HorizontalAlignment.Center;
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
                this.stackPnlOptions.Visibility = Visibility.Collapsed;
				this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using template marker feature in Essential XlsIO.";
            }
			else
            this.textBox1.Text = "Please click the 'Generate Excel' button to view the excel file generated using template marker feature in Essential XlsIO.";

        }
        /// <summary>
        /// Initializing controls to load default values
        /// </summary>
        private void InitializeControls()
        {
        }
        #endregion

  
        private async void btnGenerateExcel_Click(object sender, RoutedEventArgs e)
        {
            #region Workbook initialization
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;

            application.DefaultVersion = ExcelVersion.Excel2013;

            Assembly assembly = typeof(TemplateMarker).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.TemplateMarker.xlsx";
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
                savePicker.SuggestedFileName = "TemplateFile";
                savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                storageFile = await local.CreateFileAsync("TemplateFile.xlsx", CreationCollisionOption.ReplaceExisting);
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

        private async void btnGenerateExcel_Click_2(object sender, RoutedEventArgs e)
        {
            #region Workbook initialization
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;

            application.DefaultVersion = ExcelVersion.Excel2013;

            Assembly assembly = typeof(TemplateMarker).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.TemplateMarker.xlsx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            IWorkbook workbook = await application.Workbooks.OpenAsync(fileStream);
            IWorksheet worksheet1 = workbook.Worksheets[0];

            fileStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.XlsIO.XlsIO.Tutorials.Samples.Assets.Resources.Templates.BusinessObjects.xml");
            IList<Customers> list = GetList(fileStream);
            #endregion

            #region Applying Marker
            //Create Template Marker Processor
            ITemplateMarkersProcessor marker = workbook.CreateTemplateMarkersProcessor();
            
            marker.AddVariable("Customers", list);

            //Process the markers in the template.
            marker.ApplyMarkers();
            #endregion

            #region Save the Workbook
            StorageFile storageFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "TemplateMarker";
                if (rdbExcel2003.IsChecked == true)
                {
                    workbook.Version = ExcelVersion.Excel97to2003;
                    savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xls" });
                }
                else
                    savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
                storageFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                if (rdbExcel2003.IsChecked == true)
                    storageFile = await local.CreateFileAsync("TemplateMarker.xls", CreationCollisionOption.ReplaceExisting);
                else
                    storageFile = await local.CreateFileAsync("TemplateMarker.xlsx", CreationCollisionOption.ReplaceExisting);
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
        
        #region Helper Methods

        public IList<Customers> GetList(Stream fileStream)
        {
            IList<Customers> lstCustomers = new List<Customers>();
            StreamReader reader = new StreamReader(fileStream);
            IEnumerable<Customers> customers = GetData<Customers>(reader.ReadToEnd());
            foreach (Customers cust in customers)
            {
                lstCustomers.Add(cust);
            }
            return lstCustomers;
        }

        public static IEnumerable<T> GetData<T>(string xml)
        where T : Customers, new()
        {
            return XElement.Parse(xml)
               .Elements("Customers")
               .Select(c => new T
               {
                   SalesPerson = (string)c.Element("SalesPerson"),
                   SalesJanJune = (int)c.Element("SalesJanJune"),
                   SalesJulyDec = (int)c.Element("SalesJulyDec"),
                   Change = (int)c.Element("Change")
               });
        }

        #endregion

        #region Dispose
        public override void Dispose()
        {
            base.Dispose();

            UnlinkChildrens(grdMain);
            DisposeTextBlock(textBox1);
            textBox1 = null;
            DisposeTextBlock(textbox2);
            textbox2 = null;
            DisposeTextBlock(textBox3);
            textBox3 = null;
            DisposeTextBlock(textBox4);
            textBox4 = null;
            DisposeTextBlock(txtBlock2);
            txtBlock2 = null;
            DisposeTextBlock(txtBlock3);
            txtBlock3 = null;
            DisposeTextBlock(txtBlock4);
            txtBlock4 = null;

            DisposeRadioButton(rdbExcel2003);
            rdbExcel2003 = null;
            DisposeRadioButton(rdbExcel2013);
            rdbExcel2013 = null;

            DisposeButton(btnGenerateExcel);
            btnGenerateExcel.Click -= btnGenerateExcel_Click;
            btnGenerateExcel = null;
            DisposeButton(btnGenerateExcel_1);
            btnGenerateExcel_1.Click -= btnGenerateExcel_Click_2;
            btnGenerateExcel_1 = null;

            DisposeStackPanel(stackPnlOptions);
            stackPnlOptions = null;
            DisposeStackPanel(stackPnlOptions_1);
            stackPnlOptions_1 = null;

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
