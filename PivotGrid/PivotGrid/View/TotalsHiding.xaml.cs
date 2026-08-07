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
    using System.Linq;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Syncfusion.UI.Xaml.PivotGrid;

    public sealed partial class TotalsHiding : SampleLayout
    {
        #region Private Fields

        /// <summary>
        /// Initializes the count value.
        /// </summary>
        static int count;
        /// <summary>
        /// Initialize the view model class for further usage.
        /// </summary>
        ViewModel.ViewModel vm = new ViewModel.ViewModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="TotalsHiding">TotalsHiding.</see>/>
        /// </summary>
        public TotalsHiding()
        { 
            this.InitializeComponent();
            this.pivotGrid3.ItemSource = vm.ProductSalesData;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public sealed override void Dispose()
        {
            base.Dispose();
            if (pivotGrid3 != null)
                pivotGrid3.Dispose();
            pivotGrid3 = null;
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
        /// This event handler method is invoked when selecting the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkshowSubTotal_Click(object sender, RoutedEventArgs e)
        {
            CheckBox current = e.OriginalSource as CheckBox;
            SfPivotGrid pivotGrid = this.pivotGrid3;
            CheckBox showAllSubTotals = this.chkshowSubTotal;
            CheckBox showProductSubTotals = this.chkShowProductSubTotals;
            CheckBox showCountrySubTotals = this.chkShowCountrySubTotals;
            CheckBox showRowSubTotals = this.chkShowRowSubTotals;
            CheckBox showColumnSubTotals = this.chkShowColumnSubTotals;
            if (current?.Content != null)
            {
                int index;
                switch (current.Content.ToString())
                {
                    case "Show Product Subtotals":

                        if (current.IsChecked == true)
                        {
                            index =
                                pivotGrid.PivotRows.IndexOf(
                                    pivotGrid.PivotRows.FirstOrDefault(x => x.FieldMappingName == "Product"));
                            if (index >= 0)
                            {
                                pivotGrid.PivotRows[index].ShowSubTotal = true;
                                count--;
                            }
                            else
                            {
                                index =
                                    pivotGrid.PivotColumns.IndexOf(
                                        pivotGrid.PivotColumns.FirstOrDefault(x => x.FieldMappingName == "Product"));
                                if (index >= 0)
                                {
                                    pivotGrid.PivotColumns[index].ShowSubTotal = true;
                                    count--;
                                }
                            }
                        }
                        else
                        {
                            index =
                                pivotGrid.PivotRows.IndexOf(
                                    pivotGrid.PivotRows.FirstOrDefault(x => x.FieldMappingName == "Product"));
                            if (index >= 0)
                            {
                                pivotGrid.PivotRows[index].ShowSubTotal = false;
                                count++;
                            }
                            else
                            {
                                index =
                                    pivotGrid.PivotColumns.IndexOf(
                                        pivotGrid.PivotColumns.FirstOrDefault(x => x.FieldMappingName == "Product"));
                                if (index >= 0)
                                {
                                    pivotGrid.PivotColumns[index].ShowSubTotal = false;
                                    count++;
                                }
                            }
                        }
                        pivotGrid.InternalGrid.InvalidateCells();

                        break;
                    case "Show Country Subtotals":
                        if (current.IsChecked == true)
                        {
                            index =
                                pivotGrid.PivotColumns.IndexOf(
                                    pivotGrid.PivotColumns.FirstOrDefault(x => x.FieldMappingName == "Country"));
                            if (index >= 0)
                            {
                                pivotGrid.PivotColumns[index].ShowSubTotal = true;
                                count--;
                            }
                            else
                            {
                                index =
                                    pivotGrid.PivotRows.IndexOf(
                                        pivotGrid.PivotRows.FirstOrDefault(x => x.FieldMappingName == "Country"));
                                if (index >= 0)
                                {
                                    pivotGrid.PivotRows[index].ShowSubTotal = true;
                                    count--;
                                }
                            }
                        }
                        else
                        {
                            index =
                                pivotGrid.PivotColumns.IndexOf(
                                    pivotGrid.PivotColumns.FirstOrDefault(x => x.FieldMappingName == "Country"));
                            if (index >= 0)
                            {

                                pivotGrid.PivotColumns[index].ShowSubTotal = false;
                                count++;
                            }
                            else
                            {
                                index =
                                    pivotGrid.PivotRows.IndexOf(
                                        pivotGrid.PivotRows.FirstOrDefault(x => x.FieldMappingName == "Country"));
                                if (index >= 0)
                                {
                                    pivotGrid.PivotRows[index].ShowSubTotal = false;
                                    count++;
                                }
                            }
                        }
                        pivotGrid.InternalGrid.InvalidateCells();
                        break;
                    case "Show Row Subtotals":
                        if (current.IsChecked == true)
                        {
                            pivotGrid.ShowRowSubTotals = true;
                            if (showProductSubTotals.IsEnabled)
                                showProductSubTotals.IsChecked = true;
                        }
                        else
                        {
                            pivotGrid.ShowRowSubTotals = false;
                            if (showProductSubTotals.IsEnabled)
                                showProductSubTotals.IsChecked = false;
                        }
                        break;
                    case "Show Column Subtotals":
                        if (current.IsChecked == true)
                        {
                            pivotGrid.ShowColumnSubTotals = true;
                            if (showCountrySubTotals.IsEnabled)
                                showCountrySubTotals.IsChecked = true;
                        }
                        else
                        {
                            pivotGrid.ShowColumnSubTotals = false;
                            if (showCountrySubTotals.IsEnabled)
                                showCountrySubTotals.IsChecked = false;
                        }
                        break;
                    case "Show Subtotals":
                        if (current.IsChecked == true)
                        {
                            pivotGrid.ShowSubTotals = true;
                            showProductSubTotals.IsChecked = true;
                            showCountrySubTotals.IsChecked = true;
                            showRowSubTotals.IsChecked = true;
                            showColumnSubTotals.IsChecked = true;

                            count = 0;
                        }
                        else
                        {
                            pivotGrid.ShowSubTotals = false;
                            showProductSubTotals.IsChecked = false;
                            showCountrySubTotals.IsChecked = false;
                            showRowSubTotals.IsChecked = false;
                            showColumnSubTotals.IsChecked = false;

                            count = 4;
                        }
                        pivotGrid.InternalGrid.InvalidateCells();
                        break;
                }
                if (count == 0)
                    showAllSubTotals.IsChecked = true;
                else if (count < 4)
                    showAllSubTotals.IsChecked = null;
                else if (count == 4)
                    showAllSubTotals.IsChecked = false;
            }
        }

        /// <summary>
        /// This event handler method is invoked when selecting the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkshowProductSubTotal_Click(object sender, RoutedEventArgs e)
        {
            chkshowSubTotal_Click(sender, e);
        }

        /// <summary>
        /// This event handler method is invoked when selecting the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkshowCountrySubTotal_Click(object sender, RoutedEventArgs e)
        {
            chkshowSubTotal_Click(sender, e);
        }

        /// <summary>
        /// This event handler method is invoked when selecting the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkshowRowSubTotal_Click(object sender, RoutedEventArgs e)
        {
            chkshowSubTotal_Click(sender, e);
        }

        /// <summary>
        /// This event handler method is invoked when selecting the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkshowColumnSubTotal_Click(object sender, RoutedEventArgs e)
        {
            chkshowSubTotal_Click(sender, e);
        }

        #endregion
    }
}
