#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotChart
{
    using Common;
    using System;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Core;
    using Windows.Globalization;
    using Syncfusion.UI.Xaml.PivotChart;
    using Syncfusion.PivotAnalysis.UWP;

    public sealed partial class ChartTypes : SampleLayout
    {
        #region Private Fields

        /// <summary>
        /// Initialize the view model class for further usage.
        /// </summary>
        ViewModel viewModel = new ViewModel();
        private SfPivotChart pivotChart;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChartTypes">ChartTypes</see> class. 
        /// </summary>
        public ChartTypes()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            InitializeComponent();
            cmb_ChartType.ItemsSource = Enum.GetValues(typeof(PivotChartType));
            cmb_ChartType.SelectedIndex = 0;
            pivotChart1.ItemSource = ProductSales.GetSalesData();
            // Adding PivotAxis to the Control
            pivotChart1.PivotAxis.Add(new PivotItem { FieldMappingName = "Product", TotalHeader = "Total" });
            pivotChart1.PivotAxis.Add(new PivotItem { FieldMappingName = "Country", TotalHeader = "Total" });
            pivotChart1.PivotAxis.Add(new PivotItem { FieldMappingName = "State", TotalHeader = "Total" });

            // Adding PivotLegend to the Control
            pivotChart1.PivotLegend.Add(new PivotItem { FieldMappingName = "Date", TotalHeader = "Total" });

            //Adding PivotCalculations to the Control
            pivotChart1.PivotCalculations.Add(new PivotComputationInfo { FieldName = "Quantity", Format = "#.#" });
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public sealed override void Dispose()
        {
            if (this.pivotChart1 != null)
            {
                pivotChart1.ItemSource = null;
                pivotChart1.OlapDataManager?.Dispose();
                pivotChart1.OlapDataManager = null;
                pivotChart1.Dispose();
            }
            if (this.pivotChart != null)
            {
                pivotChart.ItemSource = null;
                pivotChart.OlapDataManager?.Dispose();
                pivotChart.OlapDataManager = null;
                pivotChart.Dispose();
            }
            if (this.viewModel != null)
                viewModel.Dispose();
            if (this.busyIndicator != null)
                this.busyIndicator.Dispose();
            this.busyIndicator = null;
            if (this.cmb_ChartType != null)
                this.cmb_ChartType.SelectionChanged -= this.cmb_ChartType_SelectionChanged;
            this.cmb_ChartType = null;
            if (this.rdBtnOlapDataSource != null)
                this.rdBtnOlapDataSource.Click -= this.rdBtnOlapDataSource_Click;
            this.rdBtnOlapDataSource = null;
            if (this.rdBtnRelationalData != null)
                this.rdBtnRelationalData.Click -= this.rdBtnRelationalData_Click;
            this.rdBtnRelationalData = null;
            base.Dispose();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// The event handler method is hooked when the radio button was clicked.
        /// </summary>
        /// <param name="sender">The radio button.</param>
        /// <param name="e">The event argument.</param>
        private void rdBtnOlapDataSource_Click(object sender, RoutedEventArgs e)
        {
            parentGrid.Children.RemoveAt(0);
            pivotChart = new SfPivotChart
            {
                ShowToolTip = true,
                ChartType = cmb_ChartType.SelectedItem as PivotChartType? ?? PivotChartType.Column,
            };
            busyIndicator.IsBusy = true;
            Task.Run(async () =>
            {
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                var taskCompletionSource = new TaskCompletionSource<bool>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    pivotChart.OlapDataManager = viewModel.OlapDataManager;
                    parentGrid.Children.Insert(0, pivotChart);
                    taskCompletionSource.SetResult(true);
                    busyIndicator.IsBusy = false;
                });
                await taskCompletionSource.Task;
            });
        }

        /// <summary>
        /// The event handler method is hooked when the radio button was clicked.
        /// </summary>
        /// <param name="sender">The radio button.</param>
        /// <param name="e">The event argument.</param>
        private void rdBtnRelationalData_Click(object sender, RoutedEventArgs e)
        {
            parentGrid.Children.RemoveAt(0);
            pivotChart = new SfPivotChart
            {
                ShowToolTip = true,
                ChartType = cmb_ChartType.SelectedItem as PivotChartType? ?? PivotChartType.Column,
            };
            busyIndicator.IsBusy = true;
            Task t = Task.Run(async () =>
            {
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                var taskCompletionSource = new TaskCompletionSource<bool>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    pivotChart.ItemSource = ProductSales.GetSalesData();
                    // Adding PivotAxis to the Control
                    pivotChart.PivotAxis.Add(new PivotItem { FieldMappingName = "Product", TotalHeader = "Total" });
                    pivotChart.PivotAxis.Add(new PivotItem { FieldMappingName = "Country", TotalHeader = "Total" });
                    pivotChart.PivotAxis.Add(new PivotItem { FieldMappingName = "State", TotalHeader = "Total" });

                    // Adding PivotLegend to the Control
                    pivotChart.PivotLegend.Add(new PivotItem { FieldMappingName = "Date", TotalHeader = "Total" });

                    //Adding PivotCalculations to the Control
                    pivotChart.PivotCalculations.Add(new PivotComputationInfo { FieldName = "Quantity", Format = "#.#" });
                    parentGrid.Children.Insert(0, pivotChart);
                    taskCompletionSource.SetResult(true);
                    busyIndicator.IsBusy = false;
                });
                await taskCompletionSource.Task;
            });
        }

        #endregion

        private void cmb_ChartType_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            pivotChart1.ChartType = cmb_ChartType.SelectedItem as PivotChartType? ?? PivotChartType.Column;
            if (pivotChart != null) pivotChart.ChartType = cmb_ChartType.SelectedItem as PivotChartType? ?? PivotChartType.Column;
        }

    }
}