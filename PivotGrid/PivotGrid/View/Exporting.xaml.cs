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
    using Windows.UI.Xaml.Controls;
    using Windows.Globalization;
    using ViewModel = Tutorials.PivotGridSamples.ViewModel;
    using Syncfusion.PivotAnalysis.UWP;
    using Windows.Storage.Pickers;
    using Syncfusion.UI.Xaml.PivotGrid;
    using Syncfusion.UI.Xaml.PivotGridConverter;
    using Common;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;
    using System;

    public sealed partial class Exporting : SampleLayout
    {
        #region Private Fields

        /// <summary>
        /// Initialize the view model class for further usage.
        /// </summary>
        ViewModel.ViewModel vm = new ViewModel.ViewModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="Exporting">Exporting.</see>/>
        /// </summary>
        public Exporting()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();

            pivotGrid.ItemSource = vm.ProductSalesData;
            // Adding rows to SfPivotGrid

            pivotGrid.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Product", TotalHeader = "Total" });
            pivotGrid.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Date", TotalHeader = "Total" });

            // Adding columns to SfPivotGrid
            pivotGrid.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Country", TotalHeader = "Total" });
            pivotGrid.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "State", TotalHeader = "Total" });

            // Adding calculations to SfPivotGrid

            pivotGrid.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Amount", FieldName = "Amount", Format = "C", SummaryType = SummaryType.DoubleTotalSum });
            pivotGrid.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Quantity", FieldName = "Quantity", Format = "#.##", SummaryType = SummaryType.Count });
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
            if (rdBtnOlapDataSource != null)
                rdBtnOlapDataSource.Click -= rdBtnOlapDataSource_Click;
            rdBtnOlapDataSource = null;
            if (rdBtnRelationalData != null)
                rdBtnRelationalData.Click -= rdBtnRelationalData_Click;
            rdBtnRelationalData = null;
            DataContext = null;
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
            SfPivotGrid pivotGrid = new SfPivotGrid();
            busyIndicator.IsBusy = true;
            Task t = Task.Run(async () =>
            {
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                var taskCompletionSource = new TaskCompletionSource<bool>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    pivotGrid.OlapDataManager = vm.OlapDataManager;
                    parentGrid.Children.Insert(0, pivotGrid);
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
            SfPivotGrid pivotGrid1 = new SfPivotGrid();
            busyIndicator.IsBusy = true;
            Task t = Task.Run(async () =>
            {
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                var taskCompletionSource = new TaskCompletionSource<bool>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {

                    pivotGrid1.ItemSource = vm.ProductSalesData;
                    // Adding rows to SfPivotGrid

                    pivotGrid1.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Product", TotalHeader = "Total" });
                    pivotGrid1.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Date", TotalHeader = "Total" });

                    // Adding columns to SfPivotGrid
                    pivotGrid1.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Country", TotalHeader = "Total" });
                    pivotGrid1.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "State", TotalHeader = "Total" });

                    // Adding calculations to SfPivotGrid

                    pivotGrid1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Amount", FieldName = "Amount", Format = "C", SummaryType = SummaryType.DoubleTotalSum });
                    pivotGrid1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Quantity", FieldName = "Quantity", Format = "#.##", SummaryType = SummaryType.Count });

                    parentGrid.Children.Insert(0, pivotGrid1);
                    taskCompletionSource.SetResult(true);
                    busyIndicator.IsBusy = false;
                });
                await taskCompletionSource.Task;
            });
        }

        /// <summary>
        /// The event handler is invoked when clicking the button.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">The event argument.</param>
        private void btnExportToWord_Click(object sender, RoutedEventArgs e)
        {
            btnExport_Click(sender, e);
        }

        /// <summary>
        /// The event handler is invoked when clicking the button.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">The event argument.</param>
        private void btnExportToCSV_Click(object sender, RoutedEventArgs e)
        {
            btnExport_Click(sender, e);
        }

        /// <summary>
        /// The event handler is invoked when clicking the button.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">The event argument.</param>
        private void btnExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            btnExport_Click(sender, e);
        }

        /// <summary>
        /// The event handler is invoked when clicking the button.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">The event argument.</param>
        private void btnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            btnExport_Click(sender, e);
        }

        /// <summary>
        /// The event handler is invoked when clicking the button.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">The event argument.</param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SfPivotGrid pivot = parentGrid.Children[0] as SfPivotGrid;
            Button exportButton = e.OriginalSource as Button;
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedFileName = "Sample";
            switch (exportButton.Content.ToString())
            {
                case "Export To CSV":

                    {
                        ExportPivotGridToCsv csvExport = null;
                        if (pivot.OlapDataManager == null)
                            csvExport = new ExportPivotGridToCsv(pivot);
                        else
                            csvExport = new ExportPivotGridToCsv(pivot.OlapDataManager.PivotEngine);
                        csvExport.ExportToDocument("Sample");
                    }
                    break;
                case "Export To PDF":
                    {
                        ExportingGridStyleInfo gridStyleInfo = pivotGrid.GridStyleInfo ?? pivotGrid.GetExportingGridStyleInfo();

                        ExportPivotGridToPdf pdfExport = null;
                        if (pivot.OlapDataManager == null)
                            pdfExport = new ExportPivotGridToPdf(pivot, gridStyleInfo);
                        else
                            pdfExport = new ExportPivotGridToPdf(pivot.OlapDataManager.PivotEngine, gridStyleInfo);
                        pdfExport.ExportToDocument("Sample");
                    }
                    break;
                case "Export To Word":
                    {
                        ExportingGridStyleInfo gridStyleInfo = pivotGrid.GridStyleInfo ?? pivotGrid.GetExportingGridStyleInfo();
                        ExportPivotGridToWord wordExport = null;
                        if (pivot.OlapDataManager == null)
                            wordExport = new ExportPivotGridToWord(pivot, gridStyleInfo);
                        else
                            wordExport = new ExportPivotGridToWord(pivot.OlapDataManager.PivotEngine, pivot.Layout, gridStyleInfo);
                        wordExport.ExportToDocument("Sample");
                    }
                    break;
                case "Export To Excel":
                    {
                        ExportingGridStyleInfo gridStyleInfo = pivotGrid.GridStyleInfo ?? pivotGrid.GetExportingGridStyleInfo();
                        ExportPivotGridToExcel excelExport = null;
                        if (pivot.OlapDataManager == null)
                            excelExport = new ExportPivotGridToExcel(pivot, gridStyleInfo, "xlsx", true);
                        else
                            excelExport = new ExportPivotGridToExcel(pivot.OlapDataManager.PivotEngine, gridStyleInfo, pivot.Layout, "xlsx", true);
                        excelExport.ExportToDocument("Sample");

                    }
                    break;
            }
        }

        #endregion
    }
}
