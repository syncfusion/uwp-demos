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
    using Windows.Globalization;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using Syncfusion.Olap.UWP.Manager;
    using Syncfusion.Olap.UWP.Reports;
    using Syncfusion.PivotAnalysis.UWP;
    using Syncfusion.UI.Xaml.PivotClient;
    using PivotClientSamples.Converters;
    using Common;
    using ViewModel = Syncfusion.SampleBrowser.UWP.PivotClient.PivotClient.ViewModel;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PivotClientConfiguration : SampleLayout
    {
        #region Private Variables

        /// <summary>
        /// Initializes the Pivot Client control.
        /// </summary>
        private SfPivotClient pivotClient;
        /// <summary>
        /// Initializes the view model class for further usage.
        /// </summary>
        private ViewModel.ViewModel viewModel = new ViewModel.ViewModel();
        private Syncfusion.SampleBrowser.UWP.PivotClient.OlapManagerService.IOlapDataProvider clientChannel;
        private OlapDataManager olapDataManager;
        private Binding clientToolbarBinding;
        private Binding gridToolbarBinding;
        private Binding chartToolbarBinding;
        private Binding showTogglePivotButtonBinding;
        private Binding showMDXButtonBinding;
        private Binding showExpanderButtonBinding;
        private Binding showReportListBinding;
        private Binding showReportMenuBinding;
        private Binding showCubeDimensionBrowserBinding;
        private Binding showSearchBoxBinding;
        private Binding showSlicerAxisBinding;
        private Binding showColumnAxisBinding;
        private Binding showRowAxisBinding;
        private Binding showValueAxisBinding;
        private Binding displayModeBinding;

        #endregion

        #region Constructor

        public PivotClientConfiguration()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            InitializeComponent();
            cmb_DisplayMode.ItemsSource = Enum.GetValues(typeof(PivotClientDisplayMode));
            cmb_DisplayMode.SelectedIndex = 0;
            pivotClient1.ItemsSource = viewModel.ProductSalesData;

            // Adding rows to SfPivotGrid
            pivotClient1.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Product", TotalHeader = "Total" });
            pivotClient1.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Date", TotalHeader = "Total" });

            // Adding columns to SfPivotGrid
            pivotClient1.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Country", TotalHeader = "Total" });

            // Adding calculations to SfPivotGrid
            pivotClient1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Amount", FieldName = "Amount", Format = "C", SummaryType = SummaryType.DoubleTotalSum });
            pivotClient1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Quantity", FieldName = "Quantity", Format = "#.##", SummaryType = SummaryType.Count });

            #region Binding
            displayModeBinding = new Binding { Path = new PropertyPath("SelectedItem"), ElementName = "cmb_DisplayMode", Mode = BindingMode.TwoWay };
            clientToolbarBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowClientToolbar", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            gridToolbarBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowGridToolbar", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            chartToolbarBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowChartToolBar", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            showColumnAxisBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowColumnAxis", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            showCubeDimensionBrowserBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowCubeDimensionBrowser", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            showExpanderButtonBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowExpanderButton", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            showMDXButtonBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowMDXButton", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            showReportListBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowReportList", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            showReportMenuBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowReportMenuButton", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            showRowAxisBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowRowAxis", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            showSearchBoxBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowSearchBox", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            showSlicerAxisBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowSlicerAxis", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            showTogglePivotButtonBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowTogglePivotButton", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            showValueAxisBinding = new Binding { Path = new PropertyPath("IsChecked"), ElementName = "chk_ShowValueAxis", Converter = new BooleanVisibilityConverter(), Mode = BindingMode.TwoWay };
            pivotClient1.SetBinding(SfPivotClient.DisplayModeProperty, displayModeBinding);
            pivotClient1.SetBinding(SfPivotClient.ToolBarVisibilityProperty, clientToolbarBinding);
            pivotClient1.SetBinding(SfPivotClient.GridToolBarVisibilityProperty, gridToolbarBinding);
            pivotClient1.SetBinding(SfPivotClient.ChartToolBarVisibilityProperty, chartToolbarBinding);
            pivotClient1.SetBinding(SfPivotClient.TogglePivotButtonVisibilityProperty, showTogglePivotButtonBinding);
            pivotClient1.SetBinding(SfPivotClient.ExpanderButtonVisibilityProperty, showExpanderButtonBinding);
            pivotClient1.SetBinding(SfPivotClient.MDXQueryButtonVisibilityProperty, showMDXButtonBinding);
            pivotClient1.SetBinding(SfPivotClient.ReportListVisibilityProperty, showReportListBinding);
            pivotClient1.SetBinding(SfPivotClient.ReportMenuButtonVisibilityProperty, showReportMenuBinding);
            pivotClient1.SetBinding(SfPivotClient.CubeDimensionBrowserVisibilityProperty, showCubeDimensionBrowserBinding);
            pivotClient1.SetBinding(SfPivotClient.SearchBoxVisibilityProperty, showSearchBoxBinding);
            pivotClient1.SetBinding(SfPivotClient.ColumnAxisElementBuilderVisibilityProperty, showColumnAxisBinding);
            pivotClient1.SetBinding(SfPivotClient.RowAxisElementBuilderVisibilityProperty, showRowAxisBinding);
            pivotClient1.SetBinding(SfPivotClient.SlicerAxisElementBuilderVisibilityProperty, showSlicerAxisBinding);
            pivotClient1.SetBinding(SfPivotClient.ValueAxisElementBuilderVisibilityProperty, showValueAxisBinding);
            #endregion
        }

        #endregion

        #region Dispose Method

        public sealed override void Dispose()
        {
            if (pivotClient1 != null)
                pivotClient1.Dispose();
            pivotClient1 = null;

            if (pivotClient != null)
                pivotClient.Dispose();
            pivotClient = null;

            clientToolbarBinding = null;
            gridToolbarBinding = null;
            chartToolbarBinding = null;
            showTogglePivotButtonBinding = null;
            showMDXButtonBinding = null;
            showExpanderButtonBinding = null;
            showReportListBinding = null;
            showReportMenuBinding = null;
            showCubeDimensionBrowserBinding = null;
            showSearchBoxBinding = null;
            showSlicerAxisBinding = null;
            showColumnAxisBinding = null;
            showRowAxisBinding = null;
            showValueAxisBinding = null;
            displayModeBinding = null;

            if (rdBtn_Olap != null)
                this.rdBtn_Olap.Click -= OlapDataSource_Click;
            rdBtn_Olap = null;
            if (rdBtn_Relational != null)
                this.rdBtn_Relational.Click -= RelationalDataSource_Click;
            rdBtn_Relational = null;

            if (viewModel != null)
                viewModel.Dispose();
            viewModel = null;

            if (olapDataManager != null)
            {
                olapDataManager.OlapDataChanged -= OlapDataManager_OlapDataChanged;
                olapDataManager.GetCubeSchema -= OlapDataManager_GetCubeSchema;
                olapDataManager.GetCubeInfoCollection -= OlapDataManager_GetCubeInfoCollection;
                olapDataManager.GetChildMembers -= OlapDataManager_GetChildMembers;
                olapDataManager.GetChildrenByMDX -= OlapDataManager_GetChildrenByMDX;
                olapDataManager.GetOlapDataWithTotalCount -= OlapDataManager_GetOlapDataWithTotalCount;
                olapDataManager.GetLevelMembersUsingMdx -= OlapDataManager_GetLevelMembersUsingMdx;
                olapDataManager.Dispose();
            }
            olapDataManager = null;

            clientChannel = null;

            base.Dispose();
        }

        #endregion

        #region Private Methods

        private void SetConnection()
        {
            System.ServiceModel.BasicHttpBinding basicHttpBinding = new System.ServiceModel.BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.MaxBufferSize = 2147483647;
            System.ServiceModel.EndpointAddress address = new System.ServiceModel.EndpointAddress("http://bi.syncfusion.com/OlapUWPTestService/OlapManager.svc/");
            System.ServiceModel.ChannelFactory<Syncfusion.SampleBrowser.UWP.PivotClient.OlapManagerService.IOlapDataProvider> clientFactory = new System.ServiceModel.ChannelFactory<Syncfusion.SampleBrowser.UWP.PivotClient.OlapManagerService.IOlapDataProvider>(basicHttpBinding, address);
            clientChannel = clientFactory.CreateChannel();
        }

        private void SetDataManager()
        {
            olapDataManager = new OlapDataManager();
            olapDataManager.OlapDataChanged += OlapDataManager_OlapDataChanged;
            olapDataManager.GetCubeSchema += OlapDataManager_GetCubeSchema;
            olapDataManager.GetCubeInfoCollection += OlapDataManager_GetCubeInfoCollection;
            olapDataManager.GetChildMembers += OlapDataManager_GetChildMembers;
            olapDataManager.GetChildrenByMDX += OlapDataManager_GetChildrenByMDX;
            olapDataManager.GetOlapDataWithTotalCount += OlapDataManager_GetOlapDataWithTotalCount;
            olapDataManager.GetLevelMembersUsingMdx += OlapDataManager_GetLevelMembersUsingMdx;
            olapDataManager.SetCurrentReport(CreateOLAPReport());
            pivotClient1.OlapDataManager = olapDataManager;
        }
        private bool IsConnectedToInternet()
        {
            Windows.Networking.Connectivity.ConnectionProfile connectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == Windows.Networking.Connectivity.NetworkConnectivityLevel.InternetAccess);
        }

        private OlapReport CreateOLAPReport()
        {
            OlapReport olapReport = new OlapReport
            {
                Name = "Sales Report",
                CurrentCubeName = "Adventure Works"
            };

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Customer";
            dimensionElementColumn.HierarchyName = "Customer Geography";
            dimensionElementColumn.AddLevel("Customer Geography", "Country");

            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Internet Sales Amount" });
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Sales Amount" });

            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Date";
            dimensionElementRow.AddLevel("Fiscal", "Fiscal Year");

            // Adding column members
            olapReport.CategoricalElements.Add(dimensionElementColumn);

            // Adding measure element
            olapReport.CategoricalElements.Add(measureElementColumn);

            // Adding row members
            olapReport.SeriesElements.Add(dimensionElementRow);

            return olapReport;
        }

        #endregion

        #region Event Handlers       

        private void OlapDataManager_OlapDataChanged(object sender, OlapDataChangedEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                SetConnection();
                (sender as OlapDataManager).JSONData = clientChannel.GetJSONDataAsync(args.MDXQuery, args.SerializedReport, args.AllowMdxToOlapReportParse).Result;
            }
        }

        private string OlapDataManager_GetCubeSchema(object sender, GetCubeSchemaEventArgs args)
        {
            if (args.CubeName != null && sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONCubeSchemaAsync(args.CubeName).Result;
            }
            return null;
        }

        private string OlapDataManager_GetCubeInfoCollection(object sender, GetCubeInfoCollectionEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONCubesAsync().Result;
            }
            return null;
        }

        private string OlapDataManager_GetChildMembers(object sender, GetChildMembersEventArgs args)
        {
            if (args.LevelName != null && sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONChildMembersAsync(args.CubeName, args.LevelName).Result;
            }
            return "";
        }

        private string OlapDataManager_GetChildrenByMDX(object sender, GetChildrenByMDXEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.Command))
            {
                SetConnection();
                return clientChannel.GetJSONChildrenByMDXAsync(args.Command).Result;
            }
            return "";
        }

        private string OlapDataManager_GetOlapDataWithTotalCount(object sender, GetOlapDataWithTotalCountEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONDataWithTotalCountAsync(args.SerializedReport).Result;
            }
            return null;
        }

        private string OlapDataManager_GetLevelMembersUsingMdx(object sender, GetLevelMembersUsingMdxEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONLevelMembersUsingMdxAsync(args.MemberUniqueName, args.AxisPosition, args.IsGrandTotalOn, args.CubeName, args.PagingParams).Result;
            }
            return null;
        }
        private void OlapDataSource_Click(object sender, RoutedEventArgs e)
        {
            RootLayout.Children.RemoveAt(0);
            pivotClient = new SfPivotClient { Margin = new Thickness(5) };
            pivotClient.SetBinding(SfPivotClient.DisplayModeProperty, displayModeBinding);
            pivotClient.SetBinding(SfPivotClient.ToolBarVisibilityProperty, clientToolbarBinding);
            pivotClient.SetBinding(SfPivotClient.GridToolBarVisibilityProperty, gridToolbarBinding);
            pivotClient.SetBinding(SfPivotClient.ChartToolBarVisibilityProperty, chartToolbarBinding);
            pivotClient.SetBinding(SfPivotClient.TogglePivotButtonVisibilityProperty, showTogglePivotButtonBinding);
            pivotClient.SetBinding(SfPivotClient.ExpanderButtonVisibilityProperty, showExpanderButtonBinding);
            pivotClient.SetBinding(SfPivotClient.MDXQueryButtonVisibilityProperty, showMDXButtonBinding);
            pivotClient.SetBinding(SfPivotClient.ReportListVisibilityProperty, showReportListBinding);
            pivotClient.SetBinding(SfPivotClient.ReportMenuButtonVisibilityProperty, showReportMenuBinding);
            pivotClient.SetBinding(SfPivotClient.CubeDimensionBrowserVisibilityProperty, showCubeDimensionBrowserBinding);
            pivotClient.SetBinding(SfPivotClient.SearchBoxVisibilityProperty, showSearchBoxBinding);
            pivotClient.SetBinding(SfPivotClient.ColumnAxisElementBuilderVisibilityProperty, showColumnAxisBinding);
            pivotClient.SetBinding(SfPivotClient.RowAxisElementBuilderVisibilityProperty, showRowAxisBinding);
            pivotClient.SetBinding(SfPivotClient.SlicerAxisElementBuilderVisibilityProperty, showSlicerAxisBinding);
            pivotClient.SetBinding(SfPivotClient.ValueAxisElementBuilderVisibilityProperty, showValueAxisBinding);
            busyIndicator.IsBusy = true;
            Task.Run(async () =>
            {
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                var taskCompletionSource = new TaskCompletionSource<bool>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    pivotClient.OlapDataManager = viewModel.OlapDataManager;
                    RootLayout.Children.Insert(0, pivotClient);
                    taskCompletionSource.SetResult(true);
                    busyIndicator.IsBusy = false;
                });
                await taskCompletionSource.Task;
            });
        }

        private void RelationalDataSource_Click(object sender, RoutedEventArgs e)
        {
            RootLayout.Children.RemoveAt(0);
            pivotClient = new SfPivotClient { Margin = new Thickness(5) };
            pivotClient.SetBinding(SfPivotClient.DisplayModeProperty, displayModeBinding);
            pivotClient.SetBinding(SfPivotClient.ToolBarVisibilityProperty, clientToolbarBinding);
            pivotClient.SetBinding(SfPivotClient.GridToolBarVisibilityProperty, gridToolbarBinding);
            pivotClient.SetBinding(SfPivotClient.ChartToolBarVisibilityProperty, chartToolbarBinding);
            pivotClient.SetBinding(SfPivotClient.TogglePivotButtonVisibilityProperty, showTogglePivotButtonBinding);
            pivotClient.SetBinding(SfPivotClient.ExpanderButtonVisibilityProperty, showExpanderButtonBinding);
            pivotClient.SetBinding(SfPivotClient.MDXQueryButtonVisibilityProperty, showMDXButtonBinding);
            pivotClient.SetBinding(SfPivotClient.ReportListVisibilityProperty, showReportListBinding);
            pivotClient.SetBinding(SfPivotClient.ReportMenuButtonVisibilityProperty, showReportMenuBinding);
            pivotClient.SetBinding(SfPivotClient.CubeDimensionBrowserVisibilityProperty, showCubeDimensionBrowserBinding);
            pivotClient.SetBinding(SfPivotClient.SearchBoxVisibilityProperty, showSearchBoxBinding);
            pivotClient.SetBinding(SfPivotClient.ColumnAxisElementBuilderVisibilityProperty, showColumnAxisBinding);
            pivotClient.SetBinding(SfPivotClient.RowAxisElementBuilderVisibilityProperty, showRowAxisBinding);
            pivotClient.SetBinding(SfPivotClient.SlicerAxisElementBuilderVisibilityProperty, showSlicerAxisBinding);
            pivotClient1.SetBinding(SfPivotClient.ValueAxisElementBuilderVisibilityProperty, showValueAxisBinding);
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
                    pivotClient.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Amount", FieldName = "Amount", Format = "C", SummaryType = SummaryType.DoubleTotalSum });
                    pivotClient.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Quantity", FieldName = "Quantity", Format = "#.##", SummaryType = SummaryType.Count });

                    RootLayout.Children.Insert(0, pivotClient);
                    taskCompletionSource.SetResult(true);
                    busyIndicator.IsBusy = false;
                });
                await taskCompletionSource.Task;
            });
        }

        #endregion
    }
}