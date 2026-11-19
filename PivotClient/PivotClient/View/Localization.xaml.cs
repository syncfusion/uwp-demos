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
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Syncfusion.PivotAnalysis.UWP;
    using Syncfusion.UI.Xaml.PivotClient;
    using Common;
    using ViewModel = Syncfusion.SampleBrowser.UWP.PivotClient.PivotClient.ViewModel;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Localization : SampleLayout
    {
        #region Private Variables

        ViewModel.ViewModel viewModel = new ViewModel.ViewModel();
        private SfPivotClient pivotClient;

        #endregion

        #region Constructor

        public Localization()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "ar-AE";
            if (IsConnectedToInternet())
            {
                InitializeComponent();
                if (Application.Current != null)
                    Application.Current.Suspending += Current_Suspending;
                PivotClient1.ItemsSource = viewModel.ProductSalesData;

                PivotClient1.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Product", TotalHeader = "Total" });
                PivotClient1.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Date", TotalHeader = "Total" });

                // Adding columns to SfPivotGrid
                PivotClient1.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Country", TotalHeader = "Total" });

                // Adding calculations to SfPivotGrid

                PivotClient1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Amount", FieldName = "Amount", Format = "C", SummaryType = SummaryType.DoubleTotalSum });
                PivotClient1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Quantity", FieldName = "Quantity", Format = "#.##", SummaryType = SummaryType.Count });
            }
            else
            {
                var result = new Windows.UI.Popups.MessageDialog("Internet connection is required to run this sample.");
                Task.Run(async () => await result.ShowAsync());
            }
        }

        #endregion

        #region Dispose Method

        public override void Dispose()
        {
            if (Application.Current != null)
                Application.Current.Suspending -= Current_Suspending;

            if (rdBtn_Olap != null)
                this.rdBtn_Olap.Click -= OlapDataSource_Click;
            rdBtn_Olap = null;
            if (rdBtn_Relational != null)
                this.rdBtn_Relational.Click -= RelationalDataSource_Click;
            rdBtn_Relational = null;

            if (this.PivotClient1 != null)
                this.PivotClient1.Dispose();
            this.PivotClient1 = null;

            if (this.pivotClient != null)
                this.pivotClient.Dispose();
            this.pivotClient = null;

            if (viewModel != null)
                viewModel.Dispose();

            base.Dispose();
        }

        #endregion

        #region Private Methods

        private bool IsConnectedToInternet()
        {
            Windows.Networking.Connectivity.ConnectionProfile connectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == Windows.Networking.Connectivity.NetworkConnectivityLevel.InternetAccess);
        }

        #endregion

        #region Event Handlers

        private void OlapDataSource_Click(object sender, RoutedEventArgs args)
        {
            RootLayout.Children.RemoveAt(0);
            pivotClient = new SfPivotClient();
            pivotClient.FlowDirection = FlowDirection.RightToLeft;
            pivotClient.PagerButtonVisibility = Visibility.Collapsed;
            busyIndicator.IsBusy = true;
            Task.Run(async () =>
            {
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                var taskCompletionSource = new TaskCompletionSource<bool>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    pivotClient.OlapDataManager = viewModel.OlapDataManager;
                    pivotClient.EnableCalculatedMembers = true;
                    pivotClient.EnableVirtualKpi = true;
                    RootLayout.Children.Insert(0, pivotClient);
                    taskCompletionSource.SetResult(true);
                    busyIndicator.IsBusy = false;
                });
                await taskCompletionSource.Task;
            });
        }

        private void RelationalDataSource_Click(object sender, RoutedEventArgs args)
        {
            RootLayout.Children.RemoveAt(0);
            pivotClient = new SfPivotClient();
            pivotClient.FlowDirection = FlowDirection.RightToLeft;
            pivotClient.PagerButtonVisibility = Visibility.Collapsed;
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
                    taskCompletionSource.SetResult(true);
                    busyIndicator.IsBusy = false;
                });
                await taskCompletionSource.Task;
            });
        }

        private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
        }

        #endregion
    }
}