#region Copyright
//  Copyright (c) Syncfusion Inc. 2001 - 2018. All rights reserved.
//  Use of this code is subject to the terms of our license.
//  A copy of the current license can be obtained at any time by e-mailing
//  licensing@syncfusion.com. Re-distribution in any form is strictly
//  prohibited. Any infringement will be prosecuted under applicable laws. 
// </copyright>
#endregion

namespace BI.PivotClient
{
    using System;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Windows.Globalization;
    using Windows.UI.Xaml;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Controls;
    using Syncfusion.PivotAnalysis.UWP;
    using Syncfusion.UI.Xaml.PivotClient;
    using ViewModel = Syncfusion.SampleBrowser.UWP.PivotClient.PivotClient.ViewModel;
    using Common;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PivotClientCustomization : SampleLayout
    {
        #region Private Variables

        /// <summary>
        /// Initializes the Pivot Client control.
        /// </summary>
        private SfPivotClient pivotClient;
        /// <summary>
        /// Initializes the view model class for further usage.
        /// </summary>
        ViewModel.ViewModel viewModel = new ViewModel.ViewModel();
        private Binding allowMultiFunctionalSortFilterBinding;
        private Binding showValueCellToolTipBinding;
        private Binding showHyperlinkBinding;
        private Binding freezeHeadersBinding;
        private Binding showLegendBinding;

        #endregion

        #region Constructor

        public PivotClientCustomization()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            InitializeComponent();

            allowMultiFunctionalSortFilterBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_AllowMultiFunctionalSortFilter", Mode = BindingMode.TwoWay };
            showValueCellToolTipBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "ShowValueCellToolTip", Mode = BindingMode.TwoWay };
            showHyperlinkBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "ShowHyperlink", Mode = BindingMode.TwoWay };
            freezeHeadersBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "FreezeHeaders", Mode = BindingMode.TwoWay };
            showLegendBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "ShowLegend", Mode = BindingMode.TwoWay, Converter = new PivotClientSamples.Converters.BooleanVisibilityConverter() };

            PivotClient1.ItemsSource = viewModel.ProductSalesData;
            // Adding rows to SfPivotGrid

            PivotClient1.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Product", TotalHeader = "Total" });
            PivotClient1.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Date", TotalHeader = "Total" });

            // Adding columns to SfPivotGrid
            PivotClient1.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Country", TotalHeader = "Total" });

            // Adding calculations to SfPivotGrid

            PivotClient1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Amount", FieldName = "Amount", Format = "C", SummaryType = Syncfusion.PivotAnalysis.UWP.SummaryType.DoubleTotalSum });
            PivotClient1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Quantity", FieldName = "Quantity", Format = "#.##", SummaryType = Syncfusion.PivotAnalysis.UWP.SummaryType.Count });

            PivotClient1.SetBinding(SfPivotClient.AllowMultiFunctionalSortFilterProperty, allowMultiFunctionalSortFilterBinding);

        }

        #endregion

        #region Dispose Method

        public sealed override void Dispose()
        {
            ShowGrandTotal.Click -= ShowGrandTotal_Click;
            ShowLevelTypeAll.Click -= ShowLevelTypeAll_Click;
            if (rdBtn_Olap != null)
                this.rdBtn_Olap.Click -= OlapDataSource_Click;
            rdBtn_Olap = null;
            if (rdBtn_Relational != null)
                this.rdBtn_Relational.Click -= RelationalDataSource_Click;
            rdBtn_Relational = null;
            allowMultiFunctionalSortFilterBinding = null;
            showValueCellToolTipBinding = null;
            showHyperlinkBinding = null;
            freezeHeadersBinding = null;
            showLegendBinding = null;

            if (PivotClient1 != null)
                PivotClient1.Dispose();
            PivotClient1 = null;

            if (pivotClient != null)
            {
                pivotClient.Loaded -= PivotClient_Loaded;
                pivotClient.Dispose();
            }
            pivotClient = null;

            if (viewModel != null)
                viewModel.Dispose();

            base.Dispose();
        }

        #endregion

        #region Private Method

        private bool IsConnectedToInternet()
        {
            Windows.Networking.Connectivity.ConnectionProfile connectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == Windows.Networking.Connectivity.NetworkConnectivityLevel.InternetAccess);
        }

        #endregion

        #region Event Handlers

        private void OlapDataSource_Click(object sender, RoutedEventArgs args)
        {
            if (IsConnectedToInternet())
            {
                RootLayout.Children.RemoveAt(0);
                pivotClient = new SfPivotClient { Margin = new Thickness(5) };
                pivotClient.Loaded += PivotClient_Loaded;
                chk_AllowMultiFunctionalSortFilter.Visibility = Visibility.Collapsed;
                busyIndicator.IsBusy = true;
                Task.Run(async () =>
                {
                    var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                    var taskCompletionSource = new TaskCompletionSource<bool>();
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        pivotClient.OlapDataManager = viewModel.OlapDataManager;
                        RootLayout.Children.Insert(0, pivotClient);
                        pivotClient.Loaded += (s, e) =>
                        {
                            if (pivotClient.PivotGrid != null)
                            {
                                pivotClient.PivotGrid.Layout = (ShowGrandTotal.IsChecked.HasValue && (bool)ShowGrandTotal.IsChecked) ? Syncfusion.Olap.UWP.Engine.GridLayout.Normal : Syncfusion.Olap.UWP.Engine.GridLayout.NoSummaries;
                                pivotClient.PivotGrid.ShowValueCellToolTip = (bool)ShowValueCellToolTip.IsChecked;
                                pivotClient.PivotGrid.ValueCellStyle.IsHyperlinkCell = (bool)ShowHyperlink.IsChecked;
                                pivotClient.PivotGrid.FreezeHeaders = (bool)FreezeHeaders.IsChecked;
                            }
                            if (pivotClient.PivotChart != null && pivotClient.PivotChart.Legend != null)
                            {
                                if (ShowLegend.IsChecked.HasValue && (bool)ShowLegend.IsChecked)
                                    pivotClient.PivotChart.Legend.Visibility = Visibility.Visible;
                                else
                                    pivotClient.PivotChart.Legend.Visibility = Visibility.Collapsed;
                            }

                        };
                        taskCompletionSource.SetResult(true);
                        busyIndicator.IsBusy = false;
                    });
                    await taskCompletionSource.Task;
                });
            }
        }

        private void RelationalDataSource_Click(object sender, RoutedEventArgs args)
        {
            RootLayout.Children.RemoveAt(0);
            pivotClient = new SfPivotClient { Margin = new Thickness(5) };
            pivotClient.Loaded += PivotClient_Loaded;

            chk_AllowMultiFunctionalSortFilter.Visibility = Visibility.Visible;
            busyIndicator.IsBusy = true;
            Task.Run(async () =>
            {
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                var taskCompletionSource = new TaskCompletionSource<bool>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    pivotClient.ItemsSource = viewModel.ProductSalesData;

                    // Adding rows to SfPivotGrid
                    pivotClient.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Product", TotalHeader = "Total" });
                    pivotClient.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Date", TotalHeader = "Total" });

                    // Adding columns to SfPivotGrid
                    pivotClient.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Country", TotalHeader = "Total" });

                    // Adding calculations to SfPivotGrid
                    pivotClient.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Amount", FieldName = "Amount", Format = "C", SummaryType = Syncfusion.PivotAnalysis.UWP.SummaryType.DoubleTotalSum });
                    pivotClient.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Quantity", FieldName = "Quantity", Format = "#.##", SummaryType = Syncfusion.PivotAnalysis.UWP.SummaryType.Count });

                    RootLayout.Children.Insert(0, pivotClient);
                    pivotClient.Loaded += (s, e) =>
                    {
                        if (pivotClient.PivotGrid != null)
                        {
                            if (pivotClient.PivotGrid.PivotEngine != null)
                            {
                                pivotClient.PivotGrid.PivotEngine.ShowGrandTotals = (bool)ShowGrandTotal.IsChecked;
                                pivotClient.PivotGrid.Refresh();
                            }
                            pivotClient.PivotGrid.ShowValueCellToolTip = (bool)ShowValueCellToolTip.IsChecked;
                            pivotClient.PivotGrid.ValueCellStyle.IsHyperlinkCell = (bool)ShowHyperlink.IsChecked;
                            pivotClient.PivotGrid.FreezeHeaders = (bool)FreezeHeaders.IsChecked;
                        }
                        if (pivotClient.PivotChart != null && pivotClient.PivotChart.Legend != null)
                        {
                            if (ShowLegend.IsChecked.HasValue && (bool)ShowLegend.IsChecked)
                                pivotClient.PivotChart.Legend.Visibility = Visibility.Visible;
                            else
                                pivotClient.PivotChart.Legend.Visibility = Visibility.Collapsed;
                        }
                    };
                    taskCompletionSource.SetResult(true);
                    busyIndicator.IsBusy = false;
                });
                await taskCompletionSource.Task;
            });
        }

        private void PivotClient_Loaded(object sender, RoutedEventArgs e)
        {
            pivotClient.SetBinding(SfPivotClient.AllowMultiFunctionalSortFilterProperty, allowMultiFunctionalSortFilterBinding);
            pivotClient.PivotGrid.SetBinding(Syncfusion.UI.Xaml.PivotGrid.SfPivotGrid.ShowValueCellToolTipProperty, showValueCellToolTipBinding);
            pivotClient.PivotGrid.SetBinding(Syncfusion.UI.Xaml.PivotGrid.PivotGridCellStyle.IsHyperlinkCellProperty, showHyperlinkBinding);
            pivotClient.PivotGrid.SetBinding(Syncfusion.UI.Xaml.PivotGrid.SfPivotGrid.FreezeHeadersProperty, freezeHeadersBinding);
            pivotClient.PivotChart.Legend.SetBinding(Syncfusion.UI.Xaml.PivotChart.PivotChartLegend.VisibilityProperty, showLegendBinding);
        }

        private void ShowGrandTotal_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                var checkBox = sender as CheckBox;
                if (PivotClient1.PivotGrid != null && PivotClient1.OlapDataManager != null)
                {
                    if (checkBox.IsChecked.HasValue && (bool)checkBox.IsChecked)
                        PivotClient1.PivotGrid.Layout = Syncfusion.Olap.UWP.Engine.GridLayout.Normal;
                    else
                        PivotClient1.PivotGrid.Layout = Syncfusion.Olap.UWP.Engine.GridLayout.NoSummaries;
                }
                else if (PivotClient1.PivotGrid != null && PivotClient1.PivotGrid.PivotEngine != null)
                {
                    PivotClient1.PivotGrid.PivotEngine.ShowGrandTotals = checkBox.IsChecked.HasValue && (bool)checkBox.IsChecked;
                    PivotClient1.PivotGrid.Refresh();
                }
                if (pivotClient != null && pivotClient.PivotGrid != null && pivotClient.OlapDataManager != null)
                {
                    if (checkBox.IsChecked.HasValue && (bool)checkBox.IsChecked)
                        pivotClient.PivotGrid.Layout = Syncfusion.Olap.UWP.Engine.GridLayout.Normal;
                    else
                        pivotClient.PivotGrid.Layout = Syncfusion.Olap.UWP.Engine.GridLayout.NoSummaries;
                }
                else if (pivotClient != null && pivotClient.PivotGrid != null && pivotClient.PivotGrid.PivotEngine != null)
                {
                    pivotClient.PivotGrid.PivotEngine.ShowGrandTotals = checkBox.IsChecked.HasValue && (bool)checkBox.IsChecked;
                    pivotClient.PivotGrid.Refresh();
                }
            }
        }

        private void ShowLevelTypeAll_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                var checkBox = sender as CheckBox;
                viewModel.OlapDataManager.ShowLevelTypeAll = checkBox.IsChecked.HasValue && (bool)checkBox.IsChecked;
            }
        }

        #endregion
    }
}