using Syncfusion.PMML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleBrowser.UWP.PredictiveAnalytics
{
    /// <summary>    
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SupportVectorMachine : Page, IDisposable
    {
        #region Private properties

        private ViewModel viewModel = null;
        private Table inputDataTable = null;
        private Table outputTable = null;
        private string pmmlPath = string.Empty;
        private Dictionary<string, object> predictedOzoneReadings;
        private bool monthInvalid = false;
        private bool dayInvalid = false;
        private bool weekInvalid = false;

        #endregion Private properties

        #region Constructor

        public SupportVectorMachine()
        {
            this.InitializeComponent();
            viewModel = this.DataContext as ViewModel;

            //PMML path
            pmmlPath = "ms-appx:///PredictiveAnalytics/Data/Ozone.pmml";

            //Initialize the predicted ozone reading values collection
            predictedOzoneReadings = new Dictionary<string, object>();

            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {

                //Applies style to predicted column
                viewModel.PredictedColumnStyle = this.Resources["predictedColumnColor"] as Style;
                if (inputDataTable == null)
                {
                    //resource path
                    string resourcePath = "ms-appx:///PredictiveAnalytics/Getting Started/Support Vector Machine";
                    // input data
                    inputDataTable = new Table(resourcePath + "/Ozone.csv", true, ',');

                    // Adds content for R, C# and PMML in the resulting tab control
                    viewModel.RCode = new SyntaxHighlighterR().FormatCode(Windows.Storage.PathIO.
                     ReadTextAsync(resourcePath + "/Ozone.R").GetAwaiter().GetResult());
                    viewModel.CSharpCode = new SyntaxRichTextBoxCS().FormatCode(Windows.Storage.PathIO.
                     ReadTextAsync(resourcePath + "/Ozone.cs").GetAwaiter().GetResult());
                    viewModel.PMML = SyntaxHighlighterXAML.FormatCode(Windows.Storage.PathIO.ReadTextAsync(pmmlPath).GetAwaiter().GetResult());

                    // Resizes grid column based on its value length
                    this.SfDataGrid.GridColumnSizer = new GridColumnSizerExt(this.SfDataGrid);
                }

                //InputRecord (combobox) loaded event trigger
                InputRecords.Loaded += InputRecords_Loaded;

                //Input record (combo box) selection changed event trigger
                InputRecords.SelectionChanged += InputRecords_SelectionChanged;
            }
        }

        #endregion Constructor

        #region InputRecords Loaded Event

        private void InputRecords_Loaded(object sender, RoutedEventArgs e)
        {
            if (predictedOzoneReadings.Count == 0)
            {
                //Gets the input values based on page size
                Table currentPageTable = viewModel.Take(inputDataTable, 0, sfDataPager.PageSize);
                PredictResult(currentPageTable, sfDataPager.PageSize);
                //Loads items for combo box
                LoadComboBoxItems();
                viewModel.IsBusy = false;
            }
        }

        #endregion InputRecords Loaded Event

        #region InputRecords_SelectionChanged Event

        private void InputRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex != -1)
            {
                //Gets the start index of page selected
                int startIndex = sfDataPager.PageIndex * sfDataPager.PageSize;
                //Binds ouput result for visualization
                BindOutputResults((Dictionary<string, object>)predictedOzoneReadings[(startIndex +
                    (sender as ComboBox).SelectedIndex + 1).ToString()]);
            }
        }

        #endregion InputRecords_SelectionChanged Event

        #region Methods

        /// <summary>
        /// Gets predicted result of input values as observable collection
        /// </summary>
        /// <param name="inputTable">current page input values</param>
        /// <param name="pageSize">page size</param>
        /// <returns>observable collection of predicted results</returns>
        private ObservableCollection<BusinessObject> PredictResult(Table inputTable, int pageSize)
        {
            //Get PMML Evaluator instance
            Syncfusion.PMML.PMMLEvaluator evaluator = new Syncfusion.PMML.PMMLEvaluatorFactory().
              GetPMMLEvaluatorInstance(viewModel.GetPMMLPath(pmmlPath));

            //Initialize the output table
            InitializeTable(inputTable.RowCount);

            //Gets the start index of page selected
            int startIndex = sfDataPager.PageIndex * sfDataPager.PageSize;

            //Predict the value for each record using the PMML Evaluator instance
            for (int i = 0; i < inputTable.RowCount; i++)
            {
                //Get input values as dictionary object
                Dictionary<string, object> ozone = inputTable.ColumnNames.ToDictionary(column => column, column
                    => inputTable[i, column]);

                //Get result
                PredictedResult predictedResult = evaluator.GetResult(ozone, null);

                //Adds predicted ozone reading values to the collection for visualization
                if (!predictedOzoneReadings.ContainsKey((startIndex + i + 1).ToString()))
                {
                    ozone.Add("predictedReading", predictedResult.PredictedDoubleValue);
                    predictedOzoneReadings.Add((startIndex + i + 1).ToString(), ozone);
                }
                //Add predicted value
                outputTable[i, 0] = predictedResult.PredictedValue;
            }
            // Merges the selected page inputs and their output values
            var result = viewModel.MergeTable(inputTable, outputTable, inputDataTable);

            return result;
        }

        /// <summary>
        /// Initialize the outputTable
        /// </summary>
        /// <param name="rowCount">rowCount of output table</param>
        private void InitializeTable(int rowCount)
        {
            //Create instance to hold evaluated results
            outputTable = new Table(rowCount, 1);
            //Add predicted column names
            outputTable.ColumnNames[0] = "Predicted_OzoneReading";
        }

        /// <summary>
        /// Loads the items for Combo box (InputRecords)
        /// </summary>
        private void LoadComboBoxItems()
        {
            int count = 0;
            viewModel.IsBusy = true;
            //Gets the start index of page selected
            int startIndex = sfDataPager.PageIndex * sfDataPager.PageSize;
            //Initializes the items list for transaction combo box
            string[] transactionItems = new string[sfDataPager.PageSize];
            for (int index = startIndex; index < (startIndex + sfDataPager.PageSize); index++)
            {
                if (predictedOzoneReadings.ContainsKey((index + 1).ToString()))
                {
                    //Adds list of transaction ID's to the combo box
                    transactionItems[count] = (index + 1).ToString();
                    count++;
                }
            }
            InputRecords.ItemsSource = transactionItems;
            InputRecords.SelectedIndex = 0;
        }

        // <summary>
        /// Binds the output results for visualization
        /// </summary>
        /// <param name="inputRecord">input record</param>
        private void BindOutputResults(Dictionary<string, object> inputRecord)
        {
            this.Month.Text = inputRecord["Month"].ToString();
            this.Day.Text = inputRecord["Day_of_month"].ToString();
            this.DayofWeek.Text = inputRecord["Day_of_week"].ToString();
            this.Pressure.Text = inputRecord["pressure_height"].ToString();
            this.WindSpeed.Text = inputRecord["Wind_speed"].ToString();
            this.Humidity.Text = inputRecord["Humidity"].ToString();
            this.Temp_SandBurg.Text = inputRecord["Temperature_at_Sandburg"].ToString();
            this.Temp_ElMonte.Text = inputRecord["Temperature_at_El_Monte"].ToString();
            this.InversionHeight.Text = inputRecord["Inversion_base_height"].ToString();
            this.PressureGradient.Text = inputRecord["Pressure_gradient"].ToString();
            this.InversionTemperature.Text = inputRecord["Inversion_base_temperature"].ToString();
            this.OzoneVisibility.Text = inputRecord["Visibility"].ToString();

            this.ActualReading.Text = "\"" + Math.Round(Convert.ToDouble(inputRecord["average_ozone_reading"]), 2).ToString() + "\"";
            this.PredictedReading.Text = "Ozone reading: \"" + Math.Round(Convert.ToDouble(inputRecord["predictedReading"]), 2).ToString() + "\"";
        }

        #endregion Methods

        #region MobileButton events

        public void Predicted_Button(object sender, RoutedEventArgs e)
        {
            //Get PMML Evaluator instance
            PMMLEvaluator evaluator = new PMMLEvaluatorFactory().
                 GetPMMLEvaluatorInstance(viewModel.GetPMMLPath(pmmlPath));

            // Input object as dictionary
            var ozone = new
            {
                Month = month.Text,
                Day_of_month = dayofmonth.Text,
                Day_of_week = dayofweek.Text,
                pressure_height = pressureheight.Text,
                Wind_speed = windspeed.Text,
                Humidity = humidity.Text,
                Temperature_at_Sandburg = temperatureatsandburg.Text,
                Temperature_at_El_Monte = temperartureatelmonte.Text,
                Inversion_base_height = inversionbaseheight.Text,
                Pressure_gradient = pressuregradient.Text,
                Inversion_base_temperature = inversionbasetemperature.Text,
                Visibility = visibility.Text
            };

            //Get predicted result
            PredictedResult predictedResult = evaluator.GetResult(ozone, null);
            this.PredictedReading.Text = "Ozone Reading : " + "\"" + Math.Round(predictedResult.PredictedDoubleValue, 2).ToString() + "\"";

            //Change of visibility over input and resultant grid
            this.InputGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.ResultGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void Previous_Button(object sender, RoutedEventArgs e)
        {
            //Change of visibility over input and resultant grid
            this.ResultGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.InputGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        #endregion MobileButton events

        #region TabControl events

        // "IsBusy" represents the loading indicator status
        private void analyticsTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.IsBusy = true;
        }

        private void predictedResultTab_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            viewModel.IsBusy = true;
        }

        private void predictedResultTab_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            viewModel.IsBusy = false;
        }

        private void cSharpTab_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            viewModel.IsBusy = true;
        }

        private void cSharpTab_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            viewModel.IsBusy = false;
        }

        private void rTab_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            viewModel.IsBusy = true;
        }

        private void rTab_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            viewModel.IsBusy = false;
        }

        private void pmmlTab_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            viewModel.IsBusy = false;
        }

        private void pmmlTab_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            viewModel.IsBusy = true;
        }

        private void statisticsTab_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            viewModel.IsBusy = true;
        }

        private void statisticsTab_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            viewModel.IsBusy = false;
        }

        private void visualizeTab_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            LoadComboBoxItems();
        }

        private void visualizeTab_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            viewModel.IsBusy = false;
        }

        private void statisticsTab_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor =
                new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void statisticsTab_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor =
                new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void visualizeTab_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor =
                new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void visualizeTab_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor =
                new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        #endregion TabControl events

        #region DataPager events

        private void sfDataPager_Loaded(object sender, RoutedEventArgs e)
        {
            int value = sfDataPager.PageSize;
            //Gets the input values based on page size
            Table currentPageTable = viewModel.Take(inputDataTable, 0, sfDataPager.PageSize);
            ObservableCollection<BusinessObject> result = PredictResult(currentPageTable, value);

            //computes total page count
            int pageCount = inputDataTable.RowCount / value;
            if (inputDataTable.RowCount % value > 0)
            {
                pageCount += 1;
            }

            sfDataPager.PageCount = pageCount;
            //Adds column values to the resulting grid
            this.SfDataGrid = viewModel.AddColumnToGrid(result, this.SfDataGrid, currentPageTable);
            sfDataPager.LoadDynamicItems(0, result);

            // Refreshes grid items
            if (SfDataGrid.View != null)
            {
                SfDataGrid.View.Refresh();

                SfDataGrid.GridColumnSizer.ResetAutoCalculationforAllColumns();

                SfDataGrid.GridColumnSizer.Refresh();
            }
            viewModel.IsBusy = false;
        }

        private void sfDataPager_PageIndexChanged(object sender, Syncfusion.UI.Xaml.Controls.DataPager.PageIndexChangedEventArgs args)
        {
            //Gets the start index of page selected
            int startIndex = args.NewPageIndex * sfDataPager.PageSize;
            // Takes input values based on page index and gets its predicted result
            int index = (startIndex + sfDataPager.PageSize) >= inputDataTable.RowCount ? inputDataTable.RowCount :
                startIndex + sfDataPager.PageSize;
            // Gets selected page input values
            Table currentPageTable = viewModel.Take(inputDataTable, startIndex, index);
            // Gets result for selected page input values
            var result = PredictResult(currentPageTable, sfDataPager.PageSize);
            sfDataPager.LoadDynamicItems(startIndex, result);
            // Refreshes grid items
            if (SfDataGrid.View != null)
            {
                SfDataGrid.View.Refresh();
                SfDataGrid.GridColumnSizer.ResetAutoCalculationforAllColumns();
                SfDataGrid.GridColumnSizer.Refresh();
            }
        }

        #endregion DataPager events

        #region Validation event

        private void InputValidation(object sender, RoutedEventArgs e)
        {
            //Selected item
            TextBox selectedItem = e.OriginalSource as TextBox;
            this.PredictButton.IsEnabled = false;

            //Validates selected item
            if (selectedItem != null)
            {
                switch (selectedItem.Name)
                {
                    case "month":

                        monthInvalid = (string.IsNullOrEmpty(selectedItem.Text) || (Convert.ToDouble(selectedItem.Text) < 1) ||
                                        (Convert.ToDouble(selectedItem.Text) > 12)) ? true : false;
                        selectedItem.BorderBrush = monthInvalid ? new SolidColorBrush(Windows.UI.Colors.Red) :
                                      new SolidColorBrush(Windows.UI.Colors.LightGray);
                        monthInvalidText.Visibility= monthInvalid? Windows.UI.Xaml.Visibility.Visible:Windows.UI.Xaml.Visibility.Collapsed;
                        break;

                    case "dayofmonth":

                        dayInvalid = (string.IsNullOrEmpty(selectedItem.Text) || (Convert.ToDouble(selectedItem.Text) < 1) ||
                                         (Convert.ToDouble(selectedItem.Text) > 31)) ? true : false;
                        selectedItem.BorderBrush = dayInvalid ? new SolidColorBrush(Windows.UI.Colors.Red) :
                                      new SolidColorBrush(Windows.UI.Colors.LightGray);
                        dayInvalidText.Visibility = dayInvalid ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
                        break;

                    case "dayofweek":

                        weekInvalid = (string.IsNullOrEmpty(selectedItem.Text) || (Convert.ToDouble(selectedItem.Text) < 1) ||
                                         (Convert.ToDouble(selectedItem.Text) > 7)) ? true : false;
                        selectedItem.BorderBrush = weekInvalid ? new SolidColorBrush(Windows.UI.Colors.Red) :
                                      new SolidColorBrush(Windows.UI.Colors.LightGray);
                        weekInvalidText.Visibility = weekInvalid ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
                        break;
                }
            }
            if (!monthInvalid && !dayInvalid && !weekInvalid)
            {
                month.BorderBrush = dayofmonth.BorderBrush = dayofweek.BorderBrush = new SolidColorBrush(Windows.UI.Colors.LightGray);
                monthInvalidText.Visibility = dayInvalidText.Visibility = weekInvalidText.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.PredictButton.IsEnabled = true;
            }
        }

        #endregion Validation event

        #region Dispose method
        public void Dispose()
        {
            //Disposes objects
            if (predictedOzoneReadings != null)
                predictedOzoneReadings.Clear();
            if (InputRecords != null)
                InputRecords.SelectedItem = -1;
            if (SfDataGrid != null)
            {
                SfDataGrid.ItemsSource = null;
                SfDataGrid.Dispose();
            }
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                this.InputRecords.Loaded -= InputRecords_Loaded;
                this.InputRecords.SelectionChanged -= InputRecords_SelectionChanged;
                this.sfDataPager.Loaded -= sfDataPager_Loaded;
                this.sfDataPager.PageIndexChanged -= sfDataPager_PageIndexChanged;
                analyticsTabControl.SelectionChanged -= analyticsTabControl_SelectionChanged;
                visualizeTab.Dispose();
                predictedResultTab.Dispose();
                rTab.Dispose();
                cSharpTab.Dispose();
                pmmlTab.Dispose();
                analyticsTabControl.Items.Clear();
                analyticsTabControl = null;
                this.sfDataPager = null;
                statisticsTab.Dispose();
                this.tabControl.Dispose();
                this.BusyIndicator.Dispose();
                inputDataTable.Dispose();
                outputTable.Dispose();                
            }
            viewModel.Dispose();
            viewModel = null;
        }
        #endregion
    }
}