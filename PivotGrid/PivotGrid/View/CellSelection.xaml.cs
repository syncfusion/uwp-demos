#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotGrid
{
    using Windows.UI.Xaml;
    using ViewModel = Tutorials.PivotGridSamples.ViewModel;
    using Windows.Globalization;
    using Common;
    using Syncfusion.PivotAnalysis.UWP;

    public sealed partial class CellSelection : SampleLayout
    {
        #region Private Fields

        /// <summary>
        /// Initialize the view model class for further usage.
        /// </summary>
        ViewModel.ViewModel vm = new ViewModel.ViewModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="CellSelection">CellSelection.</see>/>
        /// </summary>
        public CellSelection()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();
            pivotGrid.ItemSource = vm.ProductSalesData;

            // Adding rows to SfPivotGrid

            pivotGrid.PivotRows.Add(new PivotItem() { FieldMappingName = "Product", TotalHeader = "Total" });
            pivotGrid.PivotRows.Add(new PivotItem() { FieldMappingName = "Date", TotalHeader = "Total" });

            // Adding columns to SfPivotGrid
            pivotGrid.PivotColumns.Add(new PivotItem() { FieldMappingName = "Country", TotalHeader = "Total" });
            pivotGrid.PivotColumns.Add(new PivotItem() { FieldMappingName = "State", TotalHeader = "Total" });

            // Adding calculations to SfPivotGrid

            pivotGrid.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Amount", FieldName = "Amount", Format = "C", SummaryType = SummaryType.DoubleTotalSum });
            pivotGrid.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Quantity", FieldName = "Quantity", Format = "#.##", SummaryType = SummaryType.Count });

            pivotGrid.ColumnHeaderStyle.IsHyperlinkCell = pivotGrid.RowHeaderStyle.IsHyperlinkCell = pivotGrid.SummaryColumnStyle.IsHyperlinkCell = pivotGrid.SummaryRowStyle.IsHyperlinkCell = pivotGrid.ValueCellStyle.IsHyperlinkCell = false;
            this.pivotGrid.SelectionChanged += PivotGrid_SelectionChanged;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public sealed override void Dispose()
        {
            base.Dispose();
            if (this.pivotGrid != null)
            {
                pivotGrid.Dispose();
                this.pivotGrid.SelectionChanged -= PivotGrid_SelectionChanged;
                this.pivotGrid = null;
            }
            if (vm != null)
            {
                vm.ProductSalesData = null;
                vm.Dispose();
            }
            vm = null;
            if (this.rdBtnEnableCellSelection != null)
                this.rdBtnEnableCellSelection.Click -= rdBtnEnableCellSelection_Click;
            this.rdBtnEnableCellSelection = null;
            if (this.rdBtnEnableCellSelectionWithHeaders != null)
                this.rdBtnEnableCellSelectionWithHeaders.Click -= rdBtnEnableCellSelectionWithHeaders_Click;
            this.rdBtnEnableCellSelectionWithHeaders = null;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// The event handler method is hooked when the selection was changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PivotGrid_SelectionChanged(object sender, Syncfusion.UI.Xaml.PivotGrid.PivotGridSelectionChangedEventArgs e)
        {
            this.lstSelectedItems.ItemsSource = e.SelectedItems;
        }

        /// <summary>
        /// The event handler method is hooked when the radio button was checked.
        /// </summary>
        /// <param name="sender">The radio button.</param>
        /// <param name="e">The event argument.</param>
        private void rdBtnEnableCellSelection_Click(object sender, RoutedEventArgs e)
        {
            this.lstSelectedItems.ItemsSource = null;
        }

        /// <summary>
        /// The event handler method is hooked when the radio button was unchecked.
        /// </summary>
        /// <param name="sender">The radio button.</param>
        /// <param name="e">The event argument.</param>
        private void rdBtnEnableCellSelectionWithHeaders_Click(object sender, RoutedEventArgs e)
        {
            this.lstSelectedItems.ItemsSource = null;
        }

        #endregion
    }
}