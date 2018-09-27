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
    public sealed partial class AssociationRules : Page, IDisposable
    {
        #region Private properties

        private ViewModel viewModel = null;
        private Table inputDataTable = null;
        private Table outputTable = null;
        private string pmmlPath = string.Empty;
        private Dictionary<string, object> recommendedItemCollection;

        #endregion Private properties

        #region Constructor

        public AssociationRules()
        {
            this.InitializeComponent();
            viewModel = this.DataContext as ViewModel;            

            //PMML path
            pmmlPath = "ms-appx:///PredictiveAnalytics/Data/Groceries.pmml";

            //Initialize the recommended item collection
            recommendedItemCollection = new Dictionary<string, object>();

            //Transaction (combobox) selection changed event trigger
            Transactions.SelectionChanged += Transactions_SelectionChanged;

            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                //Applies style to predicted column
                viewModel.PredictedColumnStyle = this.Resources["predictedColumnColor"] as Style;
                if (inputDataTable == null)
                {
                    //resource path
                    string resourcePath = "ms-appx:///PredictiveAnalytics/Getting Started/Association Rules";
                    // input data
                    inputDataTable = new Table(resourcePath + "/Groceries.txt", true, '\t');

                    // Adds content for R, C# and PMML in the resulting tab control
                    viewModel.RCode = new SyntaxHighlighterR().FormatCode(Windows.Storage.PathIO.
                     ReadTextAsync(resourcePath + "/Groceries.R").GetAwaiter().GetResult());
                    viewModel.CSharpCode = new SyntaxRichTextBoxCS().FormatCode(Windows.Storage.PathIO.
                     ReadTextAsync(resourcePath + "/Groceries.cs").GetAwaiter().GetResult());
                    viewModel.PMML = SyntaxHighlighterXAML.FormatCode(Windows.Storage.PathIO.ReadTextAsync(pmmlPath).GetAwaiter().GetResult());

                    // Resizes grid column based on its value length
                    this.SfDataGrid.GridColumnSizer = new GridColumnSizerExt(this.SfDataGrid);
                }

                //Transaction loaded event trigger
                Transactions.Loaded += Transactions_Loaded;
            }
            else
            {
                Transactions.SelectedIndex = 0;
            }
        }

        #endregion Constructor

        #region Transaction Loaded Event

        private void Transactions_Loaded(object sender, RoutedEventArgs e)
        {
            if (recommendedItemCollection.Count == 0)
            {
                if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                {
                    //Gets the input values based on page size
                    Table currentPageTable = viewModel.Take(inputDataTable, 0, sfDataPager.PageSize);
                    PredictResult(currentPageTable, sfDataPager.PageSize);
                }
                //Loads items for transaction combo box
                LoadTransactionItems();
                viewModel.IsBusy = false;
            }
        }

        #endregion Transaction Loaded Event

        #region Transactions_SelectionChanged Event
        private void Transactions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dictionary<string, decimal> recommendedItems = null;
            //Initializes the collection of recommended items for visualization
            viewModel.RecommendedGroceries = new ObservableCollection<RecommendedGroceries>();
            if ((sender as ComboBox).SelectedIndex != -1)
            {
                //Mobile visualization
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                {
                    PMMLEvaluator evaluator = new PMMLEvaluatorFactory().
                             GetPMMLEvaluatorInstance(viewModel.GetPMMLPath(pmmlPath));

                    switch (((ComboBoxItem)(sender as ComboBox).SelectedValue).Content.ToString())
                    {
                        case "1":
                            viewModel.PurchasedItems = "citrus fruit,semi-finished bread";
                            break;

                        case "2":
                            viewModel.PurchasedItems = "tropical fruit,yogurt,coffee";
                            break;

                        case "3":
                            viewModel.PurchasedItems = "pip fruit,yogurt,cream cheese";
                            break;

                        case "4":
                            viewModel.PurchasedItems = "other vegetables,whole milk";
                            break;

                        case "5":
                            viewModel.PurchasedItems = "butter,sugar,fruit/vegetable juice";
                            break;
                    }

                    // Groups list of items for each 'transaction ID' as collection
                    List<string> input = viewModel.PurchasedItems.Split(',').ToList();

                    //Get result
                    PredictedResult predictedResult = evaluator.GetResult(input, null);

                    //Adds Recommended items
                    recommendedItems = ((AssociationModelResult)predictedResult).GetConfidences(RecommendationType.Recommendation);
                }
                else
                {
                    //Gets the start index of page selected
                    int startIndex = sfDataPager.PageIndex * sfDataPager.PageSize;
                    //Adds purchased items for selected transaction
                    viewModel.PurchasedItems = inputDataTable[(startIndex + Transactions.SelectedIndex), 1].ToString().Replace("{", "").
                        Replace("}", "").Replace(",", ", ");
                    //Adds Recommended items as collection
                    recommendedItems = (Dictionary<string, decimal>)recommendedItemCollection[(startIndex +
                        Transactions.SelectedIndex + 1).ToString()];
                }
                foreach (var item in recommendedItems)
                {
                    if (!viewModel.PurchasedItems.Contains(item.Key))
                    {
                        //Adds recommended items with its confidence value as item source for visualization
                        viewModel.RecommendedGroceries.Add(new RecommendedGroceries()
                        {
                            Item = item.Key,
                            Confidence = item.Value
                        });
                    }

                }
            }
        }

        #endregion Transactions_SelectionChanged Event

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
            PMMLEvaluator evaluator = new PMMLEvaluatorFactory().GetPMMLEvaluatorInstance(viewModel.GetPMMLPath(pmmlPath));

            //Initialize the output table
            InitializeTable(inputTable.RowCount);

            //Predict the recommendations, exclusiveRecommendations and ruleAssociations for each transactions using the PMML Evaluator instance
            for (int i = 0; i < inputTable.RowCount; i++)
            {
                // Groups list of items for each 'transaction ID' as collection
                List<string> input = inputTable[i, 1].ToString().Replace("{", "").Replace("}", "").Split(new char[] { ',' }).ToList();

                //Get result
                PredictedResult predictedResult = evaluator.GetResult(input, null);

                //Get recommended items with their confidences and here it is added to collection for visualization
                if (!recommendedItemCollection.ContainsKey(inputTable[i, 0].ToString()))
                {
                    recommendedItemCollection.Add(inputTable[i, 0].ToString(), ((AssociationModelResult)predictedResult).GetConfidences(RecommendationType.Recommendation));
                }

                // Binds recommended items array in the following format to display it in the grid
                outputTable[i, 0] = "[" + string.Join(",", predictedResult.GetRecommendations()) + "]";
                outputTable[i, 1] = "[" + string.Join(",", predictedResult.GetExclusiveRecommendations()) + "]";
                outputTable[i, 2] = "[" + string.Join(",", predictedResult.GetRuleAssociations()) + "]";
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
            outputTable = new Table(rowCount, 3);
            //Add predicted column names
            outputTable.ColumnNames[0] = "Recommendation";
            outputTable.ColumnNames[1] = "Exclusive_Recommendation";
            outputTable.ColumnNames[2] = "Rule_Association";
        }

        /// <summary>
        /// Loads the items for transaction (combo box)
        /// </summary>
        private void LoadTransactionItems()
        {
            int count = 0;
            viewModel.IsBusy = true;
            //Gets the start index of page selected
            int startIndex = sfDataPager.PageIndex * sfDataPager.PageSize;
            //Initializes the items list for transaction combo box
            string[] transactionItems = new string[sfDataPager.PageSize];
            for (int index = startIndex; index < (startIndex + sfDataPager.PageSize); index++)
            {
                if (recommendedItemCollection.ContainsKey((index + 1).ToString()))
                {
                    //Adds list of transaction ID's to the combo box
                    transactionItems[count] = (index + 1).ToString();
                    count++;
                }
            }
            Transactions.ItemsSource = transactionItems;
            Transactions.SelectedIndex = 0;
        }

        #endregion Methods

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
            LoadTransactionItems();
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

        private void sfDataPager_OnDemandLoading(object sender, Syncfusion.UI.Xaml.Controls.DataPager.OnDemandLoadingEventArgs args)
        {
            if (args.StartIndex > 0)
            {
                // Takes input values based on page index and gets its predicted result
                int index = (args.StartIndex + sfDataPager.PageSize) >= inputDataTable.RowCount ? inputDataTable.RowCount :
                    args.StartIndex + sfDataPager.PageSize;
                // Gets selected page input values
                Table currentPageTable = viewModel.Take(inputDataTable, args.StartIndex, index);
                // Gets result for selected page input values
                var result = PredictResult(currentPageTable, sfDataPager.PageSize);
                sfDataPager.LoadDynamicItems(args.StartIndex, result);
                // Refreshes grid items
                if (SfDataGrid.View != null)
                {
                    SfDataGrid.View.Refresh();
                    SfDataGrid.GridColumnSizer.ResetAutoCalculationforAllColumns();
                    SfDataGrid.GridColumnSizer.Refresh();
                }
            }
        }

        #endregion DataPager events

        #region Dispose method
        public void Dispose()
        {
            //Disposes objects  
            if (recommendedItemCollection != null)
                recommendedItemCollection.Clear();
            if (Transactions != null)
            {
                Transactions.SelectedItem = -1;
                this.Transactions.Loaded -= Transactions_Loaded;
                this.Transactions.SelectionChanged -= Transactions_SelectionChanged;
            }               
            if (SfDataGrid != null)
            {
                SfDataGrid.ItemsSource = null;
                SfDataGrid.Dispose();
            }
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                this.sfDataPager.Loaded -= sfDataPager_Loaded;
                this.sfDataPager.OnDemandLoading -= sfDataPager_OnDemandLoading;
                this.analyticsTabControl.SelectionChanged -= analyticsTabControl_SelectionChanged;
                outputTable.Dispose();
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
                this.tabControl.Dispose();
                this.BusyIndicator.Dispose();                
            }
            viewModel.Dispose();
            viewModel = null;
            this.SfChart = null;
            this.categoricalAxis = null;
            this.numericalAxis = null;
            this.StackingColumnSeries = null;
        }
        #endregion

        #region Chart event 
        private void SfChart_Loaded(object sender, RoutedEventArgs e)
        {
            //Chart adornments
            DataTemplate DataLabelTemplate = MainGrid.Resources["DataLabelTemplate"] as DataTemplate;
            ChartAdornmentInfo chartAdornmentInfo = new ChartAdornmentInfo();
            chartAdornmentInfo.ShowLabel = true;
            chartAdornmentInfo.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            chartAdornmentInfo.ShowMarker = true;
            chartAdornmentInfo.HorizontalAlignment = HorizontalAlignment.Center;
            chartAdornmentInfo.VerticalAlignment = VerticalAlignment.Center;
            chartAdornmentInfo.LabelTemplate = DataLabelTemplate;
            chartAdornmentInfo.FontSize = 30;
            StackingColumnSeries.AdornmentsInfo = chartAdornmentInfo;
        }
        #endregion
    }
}
