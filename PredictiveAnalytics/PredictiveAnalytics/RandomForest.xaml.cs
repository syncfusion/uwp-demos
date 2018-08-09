using Syncfusion.PMML;
using Syncfusion.UI.Xaml.Charts;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleBrowser.UWP.PredictiveAnalytics
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RandomForest : Page, IDisposable
    {
        #region Private properties

        private ViewModel viewModel = null;
        private Table inputDataTable = null;
        private Table outputTable = null;
        private string pmmlPath = string.Empty;
        private Dictionary<string, object> predictedSpeciesCollection;

        #endregion Private properties

        #region Constructor

        public RandomForest()
        {
            this.InitializeComponent();
            viewModel = this.DataContext as ViewModel;

            //PMML path
            pmmlPath = "ms-appx:///PredictiveAnalytics/Data/Iris.pmml";

            //Initialize the predicted species collection
            predictedSpeciesCollection = new Dictionary<string, object>();

            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                //Applies style to predicted column
                viewModel.PredictedColumnStyle = this.Resources["predictedColumnColor"] as Style;

                if (inputDataTable == null)
                {
                    //resource path
                    string resourcePath = "ms-appx:///PredictiveAnalytics/Getting Started/Random Forest";
                    // input data
                    inputDataTable = new Table(resourcePath + "/Iris.csv", true, ',');

                    // Adds content for R, C# and PMML in the resulting tab control
                    viewModel.RCode = new SyntaxHighlighterR().FormatCode(Windows.Storage.PathIO.
                     ReadTextAsync(resourcePath + "/Iris.R").GetAwaiter().GetResult());
                    viewModel.CSharpCode = new SyntaxRichTextBoxCS().FormatCode(Windows.Storage.PathIO.
                     ReadTextAsync(resourcePath + "/Iris.cs").GetAwaiter().GetResult());
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
            if (predictedSpeciesCollection.Count == 0)
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

        #region RandomForest Unloaded Event

        private void RandomForest_Unloaded(object sender, RoutedEventArgs e)
        {
            //Disposes objects on page unload
            if (predictedSpeciesCollection != null)
                predictedSpeciesCollection.Clear();
            if (InputRecords != null)
                InputRecords.SelectedItem = -1;
            if (SfDataGrid != null)
                SfDataGrid.Dispose();
            viewModel.Dispose();
            statisticsTab.Dispose();
            visualizeTab.Dispose();
        }

        #endregion RandomForest Unloaded Event

        #region InputRecords_SelectionChanged Event

        private void InputRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex != -1)
            {
                //Gets the start index of page selected
                int startIndex = sfDataPager.PageIndex * sfDataPager.PageSize;
                //Binds ouput result for visualization
                BindOutputResults((Dictionary<string, object>)predictedSpeciesCollection[(startIndex +
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
            PMMLEvaluator evaluator = new PMMLEvaluatorFactory().
              GetPMMLEvaluatorInstance(viewModel.GetPMMLPath(pmmlPath));

            //Initialize the output table
            InitializeTable(inputTable.RowCount);

            //Gets the start index of page selected
            int startIndex = sfDataPager.PageIndex * sfDataPager.PageSize;

            //Predict the value for each record using the PMML Evaluator instance
            for (int i = 0; i < inputTable.RowCount; i++)
            {
                //Get input values as dictionary object
                Dictionary<string, object> iris = inputTable.ColumnNames.ToDictionary(column => column, column
                    => inputTable[i, column]);

                //Get result
                PredictedResult predictedResult = evaluator.GetResult(iris, null);

                //Add predicted value
                outputTable[i, 0] = predictedResult.PredictedValue;

                for (int j = 1; j <= predictedResult.GetPredictedCategories().Length; j++)
                    outputTable[i, j] = predictedResult.GetPredictedProbability(predictedResult.GetPredictedCategories()[j - 1]);

                //Adds predicted species result to the collection for visualization
                if (!predictedSpeciesCollection.ContainsKey((startIndex + i + 1).ToString()))
                {
                    iris.Add("predictedSpecies", predictedResult.PredictedValue);
                    iris.Add("species_Setosa", predictedResult.GetPredictedProbability(predictedResult.GetPredictedCategories()[0]));
                    iris.Add("species_Versicolor", predictedResult.GetPredictedProbability(predictedResult.GetPredictedCategories()[1]));
                    iris.Add("species_Virginica", predictedResult.GetPredictedProbability(predictedResult.GetPredictedCategories()[2]));
                    predictedSpeciesCollection.Add((startIndex + i + 1).ToString(), iris);
                }
            }
            // Merges the selected page inputs and their output values
            var result = viewModel.MergeTable(inputTable, outputTable, inputDataTable);

            return result;
        }

        /// <summary>
        /// Initialize the outputTable
        /// </summary>
        /// <param name="rowCount">rowCount of output table</param>
        /// <param name="predictedfield">predictedfield name</param>
        /// <param name="predictedCategories">probableFields</param>
        private void InitializeTable(int rowCount)
        {
            //Create instance to hold evaluated results
            outputTable = new Table(rowCount, 4);
            //Add predicted column names
            outputTable.ColumnNames[0] = "Predicted_Species";
            outputTable.ColumnNames[1] = "SetosaSpeciesProbability";
            outputTable.ColumnNames[2] = "VersicolorSpeciesProbability";
            outputTable.ColumnNames[3] = "VirginicaSpeciesProbability";
        }

        /// <summary>
        /// Loads the items for Combo box (InputRecords)
        /// </summary>
        private void LoadComboBoxItems()
        {
            int count = 0;
            //Gets the start index of page selected
            int startIndex = sfDataPager.PageIndex * sfDataPager.PageSize;
            //Initializes the items list for transaction combo box
            string[] transactionItems = new string[sfDataPager.PageSize];
            for (int index = startIndex; index < (startIndex + sfDataPager.PageSize); index++)
            {
                if (predictedSpeciesCollection.ContainsKey((index + 1).ToString()))
                {
                    //Adds list of transaction ID's to the combo box
                    transactionItems[count] = (index + 1).ToString();
                    count++;
                }
            }
            InputRecords.ItemsSource = transactionItems;
            InputRecords.SelectedIndex = 0;
        }

        /// <summary>
        /// Binds the output results for visualization
        /// </summary>
        /// <param name="inputRecord">input record</param>
        private void BindOutputResults(Dictionary<string, object> inputRecord)
        {
            this.Sepal_Length.Text = inputRecord["Sepal_Length"].ToString();
            this.Sepal_Width.Text = inputRecord["Sepal_Width"].ToString();
            this.Petal_Length.Text = inputRecord["Petal_Length"].ToString();
            this.Petal_Width.Text = inputRecord["Petal_Width"].ToString();
            this.ActualSpecies.Text = "\"" + inputRecord["Species"].ToString() + "\"";

            viewModel.IrisCollection = new ObservableCollection<Iris>();
            viewModel.IrisCollection.Add(new Iris()
            {
                Species = string.Empty,
                Setosa_probability = Convert.ToDouble(inputRecord["species_Setosa"]),
                Versicolor_probability = Convert.ToDouble(inputRecord["species_Versicolor"]),
                Virginica_probability = Convert.ToDouble(inputRecord["species_Virginica"])
            });
        }

        #endregion Methods

        #region MobileButton events

        public void Predicted_Button(object sender, RoutedEventArgs e)
        {
            //Get PMML Evaluator instance
            PMMLEvaluator evaluator = new PMMLEvaluatorFactory().
                 GetPMMLEvaluatorInstance(viewModel.GetPMMLPath(pmmlPath));

            // Input object as dictionary
            var iris = new
            {
                Sepal_Length = sepallength.Text,
                Sepal_Width = sepalwidth.Text,
                Petal_Length = petallength.Text,
                Petal_Width = petalwidth.Text
            };

            //Get predicted result
            PredictedResult predictedResult = evaluator.GetResult(iris, null);
            viewModel.IrisCollection = new ObservableCollection<Iris>();
            viewModel.IrisCollection.Add(new Iris()
            {
                Species = string.Empty,
                Setosa_probability = Convert.ToDouble(predictedResult.GetPredictedProbability("setosa")),
                Versicolor_probability = Convert.ToDouble(predictedResult.GetPredictedProbability("versicolor")),
                Virginica_probability = Convert.ToDouble(predictedResult.GetPredictedProbability("virginica"))
            });

            //Change of visibility over input and resultant grid
            this.InputGrid.Visibility = Visibility.Collapsed;
            this.ResultGrid.Visibility = Visibility.Visible;
        }

        private void Previous_Button(object sender, RoutedEventArgs e)
        {
            //Change of visibility over input and resultant grid
            this.ResultGrid.Visibility = Visibility.Collapsed;
            this.InputGrid.Visibility = Visibility.Visible;
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
            viewModel.IsBusy = true;
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

        #region Chart loaded event
        private async void StackingColumnSeries_Loaded(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, LoadComboBoxItems);
        }
        #endregion

        public void Dispose()
        {
            //Disposes objects
            if (predictedSpeciesCollection != null)
                predictedSpeciesCollection.Clear();
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
                this.StackingColumnSeries3.Loaded -= StackingColumnSeries_Loaded;
                this.analyticsTabControl.SelectionChanged -= analyticsTabControl_SelectionChanged;
                statisticsTab.Dispose();
                visualizeTab.Dispose();
                //this.analyticsTabControl.Dispose();
                this.tabControl.Dispose();
                this.BusyIndicator.Dispose();
                inputDataTable.Dispose();
                outputTable.Dispose();
            }
            viewModel.Dispose();
            viewModel = null;
            this.SfChart = null;
            this.StackingColumnSeries1 = null;
            this.StackingColumnSeries2 = null;
            this.StackingColumnSeries3 = null;
            this.ChartLegend1 = null;
            this.primaryAxis = null;
            this.SecondaryAxis = null;
        }
        #region Chart event
        private void SfChart_Loaded(object sender, RoutedEventArgs e)
        {
            //Chart adornments
            DataTemplate DataLabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;
            ChartAdornmentInfo chartAdornmentInfo = new ChartAdornmentInfo();
            ChartAdornmentInfo chartAdornmentInfo2 = new ChartAdornmentInfo();
            ChartAdornmentInfo chartAdornmentInfo3 = new ChartAdornmentInfo();
            chartAdornmentInfo.ShowMarker = false;
            chartAdornmentInfo.ShowLabel = true;
            chartAdornmentInfo.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            chartAdornmentInfo.HorizontalAlignment = HorizontalAlignment.Center;
            chartAdornmentInfo.VerticalAlignment = VerticalAlignment.Center;
            chartAdornmentInfo.LabelTemplate = DataLabelTemplate;
            chartAdornmentInfo2.ShowMarker = false;
            chartAdornmentInfo2.ShowLabel = true;
            chartAdornmentInfo2.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            chartAdornmentInfo2.HorizontalAlignment = HorizontalAlignment.Center;
            chartAdornmentInfo2.VerticalAlignment = VerticalAlignment.Center;
            chartAdornmentInfo2.LabelTemplate = DataLabelTemplate;
            chartAdornmentInfo3.ShowMarker = false;
            chartAdornmentInfo3.ShowLabel = true;
            chartAdornmentInfo3.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            chartAdornmentInfo3.HorizontalAlignment = HorizontalAlignment.Center;
            chartAdornmentInfo3.VerticalAlignment = VerticalAlignment.Center;
            chartAdornmentInfo3.LabelTemplate = DataLabelTemplate;
            StackingColumnSeries1.AdornmentsInfo = chartAdornmentInfo;
            StackingColumnSeries2.AdornmentsInfo = chartAdornmentInfo2;
            StackingColumnSeries3.AdornmentsInfo = chartAdornmentInfo3;
        }
        #endregion
    }
}