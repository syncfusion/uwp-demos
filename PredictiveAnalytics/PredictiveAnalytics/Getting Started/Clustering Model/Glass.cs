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
    public sealed partial class ClusteringModel : Page, IDisposable
    {
        #region Private properties

        private ViewModel viewModel = null;
        private Table inputDataTable = null;
        private Table outputTable = null;
        private string pmmlPath = string.Empty;
        private Dictionary<string, object> predictedClusterDetails;

        #endregion Private properties

        #region Constructor

        public ClusteringModel()
        {
            this.InitializeComponent();
            viewModel = this.DataContext as ViewModel;

            // PMML path
            pmmlPath = "ms-appx:///PredictiveAnalytics/Data/Glass.pmml";

            //Initialize the predicted cluster details collection
            predictedClusterDetails = new Dictionary<string, object>();

            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                //Applies style to predicted column
                viewModel.PredictedColumnStyle = this.Resources["predictedColumnColor"] as Style;
                if (inputDataTable == null)
                {
                    //resource path
                    string resourcePath = "ms-appx:///PredictiveAnalytics/Getting Started/Clustering Model";
                    // input data
                    inputDataTable = new Table(resourcePath + "/Glass.csv", true, ',');

                    // Adds content for R, C# and PMML in the resulting tab control
                    viewModel.RCode = new SyntaxHighlighterR().FormatCode(Windows.Storage.PathIO.
                     ReadTextAsync(resourcePath + "/Glass.R").GetAwaiter().GetResult());
                    viewModel.CSharpCode = new SyntaxRichTextBoxCS().FormatCode(Windows.Storage.PathIO.
                     ReadTextAsync(resourcePath + "/Glass.cs").GetAwaiter().GetResult());
                    viewModel.PMML = SyntaxHighlighterXAML.FormatCode(Windows.Storage.PathIO.ReadTextAsync(pmmlPath).GetAwaiter().GetResult());

                    // Resizes grid column based on its value length
                    this.SfDataGrid.GridColumnSizer = new GridColumnSizerExt(this.SfDataGrid);
                }

                //InputRecord (combobox) loaded event trigger
                InputRecords.Loaded += InputRecords_Loaded;

                //Input record (combobox) selection changed event trigger
                InputRecords.SelectionChanged += InputRecords_SelectionChanged;
            }

            //Cluster size are added based on the values presented in PMML ("Glass.pmml")
            viewModel.ClusterInformation = new List<ClusterDetails>();
            viewModel.ClusterInformation.Add(new ClusterDetails() { ID = "Cluster 1", Size = 46 });
            viewModel.ClusterInformation.Add(new ClusterDetails() { ID = "Cluster 2", Size = 26 });
            viewModel.ClusterInformation.Add(new ClusterDetails() { ID = "Cluster 3", Size = 26 });
            viewModel.ClusterInformation.Add(new ClusterDetails() { ID = "Cluster 4", Size = 7 });
            viewModel.ClusterInformation.Add(new ClusterDetails() { ID = "Cluster 5", Size = 61 });
            viewModel.ClusterInformation.Add(new ClusterDetails() { ID = "Cluster 6", Size = 5 });
        }

        #endregion Constructor

        #region InputRecords Loaded Event

        private void InputRecords_Loaded(object sender, RoutedEventArgs e)
        {
            if (predictedClusterDetails.Count == 0)
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
                BindOutputResults((Dictionary<string, object>)predictedClusterDetails[(startIndex +
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
                //Gets input record as dictionary object
                Dictionary<string, object> glass = inputTable.ColumnNames.ToDictionary(column => column, column
                    => inputTable[i, column]);

                //Get result
                PredictedResult predictedResult = evaluator.GetResult(glass, null);

                //Adds predicted cluster details to the collection for visualization
                if (!predictedClusterDetails.ContainsKey((startIndex + i + 1).ToString()))
                {
                    glass.Add("predictedCluster", predictedResult.PredictedValue);
                    predictedClusterDetails.Add((startIndex + i + 1).ToString(), glass);
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
        /// <param name="predictedfield">predictedfield name</param>
        /// <param name="predictedCategories">probableFields</param>
        private void InitializeTable(int rowCount)
        {
            //Create instance to hold evaluated results
            outputTable = new Table(rowCount, 1);
            //Add predicted column names
            outputTable.ColumnNames[0] = "Predicted_Cluster";
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
                if (predictedClusterDetails.ContainsKey((index + 1).ToString()))
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
            //Output results initialization
            string[] centroidValues = null;
            viewModel.GlassCollection = new ObservableCollection<Glass>();
            string[] inputVariables = new string[] { "RI", "Na", "Mg", "Al", "Si", "K", "Ca", "Ba", "Fe" };

            //Highlights the predicted cluster by exploding its index from pie chart
            //The mean values are calculated manually here, but we will have an attribute "mean" in the PMML representing the mean value
            switch (inputRecord["predictedCluster"].ToString())
            {
                case "1":
                    pieSeries.ExplodeIndex = 0;
                    centroidValues = new string[] { "-1.69", "13.16", "3.46", "1.56", "72.80", "0.61", "8.11", "0.04", "0.05" };
                    break;

                case "2":
                    pieSeries.ExplodeIndex = 1;
                    centroidValues = new string[] { "3.95", "13.63", "2.89", "1.09", "71.94", "0.26", "9.96", "0.06", "0.08" };
                    break;

                case "3":
                    pieSeries.ExplodeIndex = 2;
                    centroidValues = new string[] { "-0.92", "14.29", "0.08", "2.04", "73.31", "0.21", "9.15", "0.83", "0.01" };
                    break;

                case "4":
                    pieSeries.ExplodeIndex = 3;
                    centroidValues = new string[] { "9.65", "12.25", "0.35", "1.19", "71.61", "0.24", "13.78", "0.35", "0.11" };
                    break;

                case "5":
                    pieSeries.ExplodeIndex = 4;
                    centroidValues = new string[] { "0.13", "13.22", "3.44", "1.29", "72.69", "0.51", "8.63", "0.04", "0.06" };
                    break;

                case "6":
                    pieSeries.ExplodeIndex = 5;
                    centroidValues = new string[] { "-5.67", "14.08", "1.40", "1.81", "72.82", "2.47", "6.98", "0.19", "0.05" };
                    break;
            }

            for (int i = 0; i < inputVariables.Length; i++)
            {
                viewModel.GlassCollection.Add(new Glass()
                {
                    Field = inputVariables[i],
                    Input_Value = inputRecord[inputVariables[i]].ToString(),
                    Centroid_Value = centroidValues[i].ToString()
                });
            }
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                this.PredictedCluster.Text = "Predicted group : \"Cluster " + inputRecord["predictedCluster"].ToString() + "\"";
            }
        }

        #endregion Methods

        #region Mobile Button Events
        public void Predicted_Button(object sender, RoutedEventArgs e)
        {
            //Get PMML Evaluator instance
            PMMLEvaluator evaluator = new PMMLEvaluatorFactory().
                      GetPMMLEvaluatorInstance(viewModel.GetPMMLPath(pmmlPath));
            
            // Input object as dictionary
            Dictionary<string, object> glass = new Dictionary<string, object>();
            glass.Add("RI", ri.Text);
            glass.Add("Na", na.Text);
            glass.Add("Mg", mg.Text);
            glass.Add("Al", al.Text);
            glass.Add("Si", si.Text);
            glass.Add("K", k.Text);
            glass.Add("Ca", ca.Text);
            glass.Add("Ba", ba.Text);
            glass.Add("Fe", fe.Text);

            //Get predicted result
            PredictedResult predictedResult = evaluator.GetResult(glass, null);
            glass.Add("predictedCluster", predictedResult.PredictedValue);
            this.PredictedPrice.Text = "Predicted Group : \"Cluster" + predictedResult.PredictedValue + "\"";

            //Binds output result for visualization
            BindOutputResults(glass);

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
        #endregion Mobile Button Events

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

        #region PieChart loaded event
        private void PieSeries_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBoxItems();
        }
        #endregion

        #region Dispose method
        public void Dispose()
        {
            //Disposes objects        
            if (predictedClusterDetails != null)
                predictedClusterDetails.Clear();
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
                this.analyticsTabControl.SelectionChanged -= analyticsTabControl_SelectionChanged;
                this.pieSeries.Loaded -= PieSeries_Loaded;
                visualizeTab.Dispose();
                predictedResultTab.Dispose();
                rTab.Dispose();
                cSharpTab.Dispose();
                pmmlTab.Dispose();
                analyticsTabControl.Items.Clear();
                analyticsTabControl = null;
                this.sfDataPager = null;
                statisticsTab.Dispose();
                inputDataTable.Dispose();
                outputTable.Dispose();                
                this.tabControl.Dispose();
                this.BusyIndicator.Dispose();
                this.ChartLegend1 = null;
            }
            viewModel.Dispose();
            viewModel = null;
            this.SfChart = null;
            this.pieSeries = null;
        }
        #endregion

        #region Chart event 
        private void SfChart_Loaded(object sender, RoutedEventArgs e)
        {
            //Chart adornments
            DataTemplate LabelTemplate = MainGrid.Resources["labelTemplate"] as DataTemplate ;
            ChartAdornmentInfo chartAdornmentInfo = new ChartAdornmentInfo();
            chartAdornmentInfo.ShowLabel = true;
            chartAdornmentInfo.AdornmentsPosition = AdornmentsPosition.Bottom;
            chartAdornmentInfo.ShowMarker = true;
            chartAdornmentInfo.HorizontalAlignment = HorizontalAlignment.Center;
            chartAdornmentInfo.VerticalAlignment = VerticalAlignment.Center;
            chartAdornmentInfo.ShowConnectorLine = true;
            chartAdornmentInfo.ConnectorHeight = 80;
            chartAdornmentInfo.LabelTemplate = LabelTemplate;
            chartAdornmentInfo.SegmentLabelContent = LabelContent.LabelContentPath;
            pieSeries.AdornmentsInfo = chartAdornmentInfo;
         }
        #endregion
    }
}