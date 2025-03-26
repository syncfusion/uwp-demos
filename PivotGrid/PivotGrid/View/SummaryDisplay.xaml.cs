#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotGrid
{
    using Common;
    using ViewModel = Tutorials.PivotGridSamples.ViewModel;
    using Syncfusion.PivotAnalysis.UWP;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Syncfusion.UI.Xaml.PivotGrid;
    using System.Linq;

    public sealed partial class SummaryDisplay : SampleLayout
    {
        #region Private Fields

        /// <summary>
        /// Initialize the view model class for further usage.
        /// </summary>
        ViewModel.ViewModel vm = new ViewModel.ViewModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="SummaryDisplay">SummaryDisplay.</see>/>
        /// </summary>
        public SummaryDisplay()
        {
            this.InitializeComponent();
            this.pivotGrid2.ItemSource = vm.ProductSalesData;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public sealed override void Dispose()
        {
            base.Dispose();
            if (pivotGrid2 != null)
                pivotGrid2.Dispose();
            pivotGrid2 = null;
            if (vm != null)
            {
                vm.ProductSalesData = null;
                vm.BusinessSalesData = null;
                vm.OlapDataManager = null;
                vm.Dispose();
            }
            vm = null;
            DataContext = null;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// This event handler method is invoked when selecting the combo box item.
        /// </summary>
        /// <param name="sender">The combo box.</param>
        /// <param name="e">The event argument.</param>
        private void DisplayOptionChanged(object sender, RoutedEventArgs e)
        { 
            ComboBox current = sender as ComboBox;
            SfPivotGrid pivotGrid = (this.pivotGrid2) as SfPivotGrid;
            string selectedOption = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
            int index = pivotGrid.PivotCalculations.Count> 1 && current.Name == "DisplayOptionBox1" ? 1 : 0;
            if (index == 0)
                index = pivotGrid.PivotCalculations.IndexOf(pivotGrid.PivotCalculations.Where(x => x.FieldName == "Amount").FirstOrDefault());
            else
                index = pivotGrid.PivotCalculations.IndexOf(pivotGrid.PivotCalculations.Where(x => x.FieldName == "Quantity").FirstOrDefault());
            if (selectedOption == "None")
                pivotGrid.PivotCalculations[index].DisplayOption = DisplayOption.None;
            else if (selectedOption == "All")
                pivotGrid.PivotCalculations[index].DisplayOption = DisplayOption.All;
            else if (selectedOption == "Calculations")
                pivotGrid.PivotCalculations[index].DisplayOption = DisplayOption.Calculations;
            else if (selectedOption == "Summaries")
                pivotGrid.PivotCalculations[index].DisplayOption = DisplayOption.Summary;
            else if (selectedOption == "GrandTotals")
                pivotGrid.PivotCalculations[index].DisplayOption = DisplayOption.GrandTotals;
            else if (selectedOption == "Summaries and Calculations")
                pivotGrid.PivotCalculations[index].DisplayOption = DisplayOption.Calculations | DisplayOption.Summary;
            else if (selectedOption == "Summaries and GrandTotals")
                pivotGrid.PivotCalculations[index].DisplayOption = DisplayOption.Summary | DisplayOption.GrandTotals;
            else if (selectedOption == "Calculations and GrandTotals")
                pivotGrid.PivotCalculations[index].DisplayOption = DisplayOption.Calculations | DisplayOption.GrandTotals;

            pivotGrid2.Refresh();
        }

        /// <summary>
        /// This event handler method is invoked when selecting the combo box item.
        /// </summary>
        /// <param name="sender">The combo box.</param>
        /// <param name="e">The event argument.</param>
        private void DisplayOptionBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            DisplayOptionChanged(sender, e);
        }

        #endregion
    }
}
