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
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.Globalization;
    using Syncfusion.PivotAnalysis.UWP;
    using Syncfusion.UI.Xaml.Charts;
    using Syncfusion.UI.Xaml.PivotChart;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Adornments : SampleLayout
    {

        #region Private Members

        SfPivotChart pivotChart;

        ViewModelAdornments adornmentsViewModel;

        #endregion

        #region Constructor

        /// <summary>
        ///  Initializes the new instance of <see cref="Adornments"> class.</see>/>
        /// </summary>
        public Adornments()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();
            this.adornmentsViewModel = new ViewModelAdornments();
            pivotChart1.ItemSource = ProductSales.GetSalesData();
            // Adding PivotAxis to the Control
            pivotChart1.PivotAxis.Add(new PivotItem { FieldMappingName = "Product", TotalHeader = "Total" });
            pivotChart1.PivotAxis.Add(new PivotItem { FieldMappingName = "Country", TotalHeader = "Total" });
            pivotChart1.PivotAxis.Add(new PivotItem { FieldMappingName = "State", TotalHeader = "Total" });

            // Adding PivotLegend to the Control
            pivotChart1.PivotLegend.Add(new PivotItem { FieldMappingName = "Date", TotalHeader = "Total" });

            //Adding PivotCalculations to the Control
            pivotChart1.PivotCalculations.Add(new PivotComputationInfo { FieldName = "Quantity", Format = "#.#" });
            pivotChart1.AdornmentsInfo = GetAdornmentsInfo();
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public sealed override void Dispose()
        {
            this.pivotChart1?.Dispose();
            this.pivotChart1 = null;
            this.pivotChart?.Dispose();
            this.pivotChart = null;
            this.adornmentsViewModel?.Dispose();
            this.adornmentsViewModel = null;
            this.busyIndicator?.Dispose();
            this.busyIndicator = null;
            if (this.rdBtnOlapDataSource != null)
                this.rdBtnOlapDataSource.Click -= rdBtnOlapDataSource_Click;
            this.rdBtnOlapDataSource = null;
            if (this.rdBtnRelationalData != null)
                this.rdBtnRelationalData.Click -= rdBtnRelationalData_Click;
            this.rdBtnRelationalData = null;
            base.Dispose();
        }

        #endregion

        #region Private Methods

        private PivotChartAdornmentsInfo GetAdornmentsInfo()
        {
            var adornmentsInfo = new PivotChartAdornmentsInfo();
            adornmentsInfo.ShowLabel = true;
            adornmentsInfo.ShowConnectorLine = true;
            adornmentsInfo.ConnectorHeight = 150;
            adornmentsInfo.SegmentLabelContent = LabelContent.LabelContentPath;
            adornmentsInfo.SegmentLabelFormat = "0.00";
            adornmentsInfo.LabelPosition = AdornmentsLabelPosition.Auto;
            adornmentsInfo.UseSeriesPalette = true;
            adornmentsInfo.LabelTemplate = parentGrid.Resources["AdornmentsData"] as DataTemplate;
            return adornmentsInfo;
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
                Margin = new Thickness(25),
                ShowToolTip = false,
                ChartType = PivotChartType.Pie,
                AdornmentsInfo = GetAdornmentsInfo(),
                Legend = null
            };
            busyIndicator.IsBusy = true;
            Task t = Task.Run(async () =>
            {
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                var taskCompletionSource = new TaskCompletionSource<bool>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    pivotChart.OlapDataManager = this.adornmentsViewModel?.OlapDataManager;
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
                Margin = new Thickness(25),
                ShowToolTip = false,
                ChartType = PivotChartType.Pie,
                AdornmentsInfo = GetAdornmentsInfo(),
                Legend = null
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

                    parentGrid.Children.Insert(0, pivotChart1);
                    taskCompletionSource.SetResult(true);
                    busyIndicator.IsBusy = false;
                });
                await taskCompletionSource.Task;
            });
        }

        #endregion
    }
}