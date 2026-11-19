#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotGrid
{
    using viewModel = Tutorials.PivotGridSamples.ViewModel;
    using Windows.UI.Xaml;
    using Windows.Globalization;
    using Common;
    using Syncfusion.UI.Xaml.PivotGrid;
    using Syncfusion.PivotAnalysis.UWP;

    public sealed partial class ConditionalFormat : SampleLayout
    {
        #region Private Fields

        /// <summary>
        /// Initialize the view model class for further usage.
        /// </summary>
        viewModel.ViewModel vm = new Tutorials.PivotGridSamples.ViewModel.ViewModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="ConditionalFormat">ConditionalFormat.</see>/>
        /// </summary>
        public ConditionalFormat()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            InitializeComponent();
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

            if (DeviceFamily.GetDeviceFamily() != Devices.Desktop)
                ConditionalFormatEditorButton.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public sealed override void Dispose()
        {
            base.Dispose();
            if (pivotGrid != null)
                pivotGrid.ConditionalFormats.Clear();
                pivotGrid.Dispose();
            pivotGrid = null;
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
        /// The event handler method is hooked when the button was clicked.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">The event argument.</param>
        private void ConditionalFormatEditorButton_Click(object sender, RoutedEventArgs e)
        {
            ConditionalFormatEditor conditionalFormatEditor = new ConditionalFormatEditor(pivotGrid);
            ConditionalFormatEditorPopup.Child = conditionalFormatEditor;
            ConditionalFormatEditorPopup.IsOpen = true;
        }

        #endregion
    }
}