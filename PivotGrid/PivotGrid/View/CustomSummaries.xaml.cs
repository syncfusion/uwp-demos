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
    using Syncfusion.PivotAnalysis.UWP;
    using System.Linq;
    using Windows.Globalization;
    using Windows.UI.Xaml.Controls;
    using ViewModel = Tutorials.PivotGridSamples.ViewModel;

    public sealed partial class CustomSummaries : SampleLayout
    {
        #region Private Fields

        /// <summary>
        /// Initialize the view model class for further usage.
        /// </summary>
        ViewModel.ViewModelForCustomSummary vm = new ViewModel.ViewModelForCustomSummary();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="CustomSummaries">CustomSummaries.</see>/>
        /// </summary>
        public CustomSummaries()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            InitializeComponent();
            pivotGrid1.ItemSource = vm.Data;

            // Adding rows to SfPivotGrid

            pivotGrid1.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Category", TotalHeader = "Total" });
            pivotGrid1.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Item", TotalHeader = "Total" });

            // Adding columns to SfPivotGrid
            pivotGrid1.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Country", TotalHeader = "Total" });
            pivotGrid1.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "State", TotalHeader = "Total" });

            // Adding calculations to SfPivotGrid

            pivotGrid1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Shipped!", FieldName = "Value1", Format = "#,##0.00", SummaryType = SummaryType.Custom, Summary = new MyCustomSummaryBase1() });
            pivotGrid1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Scrap!", FieldName = "Value3", PadString = "***", SummaryType = SummaryType.DisplayIfDiscreteValuesEqual });
            pivotGrid1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Received!", FieldName = "Value2", SummaryType = SummaryType.DoubleTotalSum });
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>

        public sealed override void Dispose()
        {
            base.Dispose();
            if (this.pivotGrid1 != null)
                pivotGrid1.Dispose();
            this.pivotGrid1 = null;
            if (vm != null)
                vm.Data = null;
            vm = null;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// This event handler method is hooked when clicking the button.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">The event argument.</param>
        private void chkBoxCustom_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.Value)
                pivotGrid1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Shipped!", FieldName = "Value1", Format = "#,##0.00", SummaryType = SummaryType.Custom, Summary = new MyCustomSummaryBase1() });
            else if (pivotGrid1.PivotCalculations.Any(x => x.FieldName == "Value1"))
                pivotGrid1.PivotCalculations.Remove(pivotGrid1.PivotCalculations.FirstOrDefault(x => x.FieldName == "Value1"));
        }

        /// <summary>
        /// This event handler method is hooked when clicking the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkBoxDiscrete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.Value)
            {
                var padString = (cmbBoxPadString.SelectedValue as ComboBoxItem).Content.ToString();
                pivotGrid1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Scrap!", FieldName = "Value3", PadString = padString, SummaryType = SummaryType.DisplayIfDiscreteValuesEqual });
                cmbBoxPadString.IsEnabled = true;
            }
            else if (pivotGrid1.PivotCalculations.Any(x => x.FieldName == "Value3"))
            {
                pivotGrid1.PivotCalculations.Remove(pivotGrid1.PivotCalculations.FirstOrDefault(x => x.FieldName == "Value3"));
                cmbBoxPadString.IsEnabled = false;
            }
        }

        /// <summary>
        /// This event handler method is hooked when the combo box item was selected.
        /// </summary>
        /// <param name="sender">The combo box.</param>
        /// <param name="e">The event argument.</param>
        private void cmbBoxPadString_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pivotGrid1.PivotCalculations.Count == 0)
                return;
            int i = pivotGrid1.PivotCalculations.IndexOf(pivotGrid1.PivotCalculations.FirstOrDefault(x => x.FieldName == "Value3"));
            pivotGrid1.PivotCalculations[i].PadString = ((sender as ComboBox).SelectedValue as ComboBoxItem).Content.ToString();
            pivotGrid1.Refresh();
        }

        #endregion
    }
}
