#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotGrid
{
    using Syncfusion.Olap.UWP.Manager;
    using Syncfusion.Olap.UWP.Reports;
    using System;
    using System.ServiceModel;
    using Windows.UI.Xaml.Controls;
    using System.Threading.Tasks;
    using Windows.Globalization;
    using Common;

    public sealed partial class KPI : SampleLayout
    {
        #region Private Fields

        /// <summary>
        /// Gets or sets the client channel.
        /// </summary>
        Syncfusion.SampleBrowser.UWP.PivotGrid.OlapManagerService.IOlapDataProvider clientChannel;
        /// <summary>
        /// Gets or sets the OlapDataManager.
        /// </summary>
        OlapDataManager olapDataManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="KPI">KPI.</see>/>
        /// </summary>
        public KPI()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();
            if (IsConnectedToInternet())
            {
                SetDataManager();
            }
            else
            {
                var result = new Windows.UI.Popups.MessageDialog("Internet connection is required to run this sample.");
                Task.Run(async () => await result.ShowAsync());
            }
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
            if (olapDataManager != null)
            {
                olapDataManager.OlapDataChanged -= OlapDataManager_OlapDataChanged;
                olapDataManager.OlapDataChanged -= OlapDataManager_OlapDataChanged;
                olapDataManager.GetCubeSchema -= OlapDataManager_GetCubeSchema;
                olapDataManager.Dispose();
            }
            olapDataManager = null;
            if (cmbProduct != null)
                cmbProduct.SelectionChanged -= cmbProduct_SelectionChanged;
            cmbProduct = null;
            if (cmbReport != null)
                cmbReport.SelectionChanged -= cmbReport_SelectionChanged;
            cmbReport = null;
        }

        #endregion

        #region Setting OLAP Data Manager

        /// <summary>
        /// Method is used to set the data manager for SfPivotGrid.
        /// </summary>
        /// <returns></returns>
        private void SetDataManager()
        {
            olapDataManager = new OlapDataManager();
            olapDataManager.OlapDataChanged += OlapDataManager_OlapDataChanged;
            olapDataManager.OlapDataChanged += OlapDataManager_OlapDataChanged;
            olapDataManager.GetCubeSchema += OlapDataManager_GetCubeSchema;
            olapDataManager.SetCurrentReport(LoadBasicKPI());
            this.pivotGrid.OlapDataManager = olapDataManager;
            this.pivotGrid.RowHeaderStyle.IsHyperlinkCell = this.pivotGrid.ColumnHeaderStyle.IsHyperlinkCell = this.pivotGrid.SummaryColumnStyle.IsHyperlinkCell = this.pivotGrid.SummaryRowStyle.IsHyperlinkCell = this.pivotGrid.ValueCellStyle.IsHyperlinkCell = false;
        }

        #endregion

        #region Setting Connection to Service

        /// <summary>
        /// Method is used to set the data connection from on-line cuber.
        /// </summary>
        void SetConnection()
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.MaxBufferSize = 2147483647;
            EndpointAddress address = new EndpointAddress("http://bi.syncfusion.com/OlapUWPTestService/OlapManager.svc/");
            ChannelFactory<Syncfusion.SampleBrowser.UWP.PivotGrid.OlapManagerService.IOlapDataProvider> clientFactory = new ChannelFactory<Syncfusion.SampleBrowser.UWP.PivotGrid.OlapManagerService.IOlapDataProvider>(basicHttpBinding, address);
            clientChannel = clientFactory.CreateChannel();
        }

        /// <summary>
        /// This method is used to check whether the application is connected to Internet.
        /// </summary>
        /// <returns>The boolean.</returns>
        private bool IsConnectedToInternet()
        {
            Windows.Networking.Connectivity.ConnectionProfile connectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == Windows.Networking.Connectivity.NetworkConnectivityLevel.InternetAccess);
        }

        #endregion

        #region OLAP Report Creation

        #region LoadingBasicKPI

        /// <summary>
        /// This method is used to load the OLAP report with KPI elements.
        /// </summary>
        /// <returns></returns>
        OlapReport LoadBasicKPI()
        {
            OlapReport olapReport = new OlapReport();
            olapReport.CurrentCubeName = "Adventure Works";

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Product";
            dimensionElementColumn.AddLevel("Product Categories", "Category");
            dimensionElementColumn.Hierarchy.LevelElements["Category"].Add((this.cmbProduct.SelectedItem as ComboBoxItem).Content.ToString());
            dimensionElementColumn.Hierarchy.LevelElements["Category"].IncludeAvailableMembers = true;

            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Gross Profit" });

            KpiElements kpiElement = new KpiElements();
            kpiElement.Elements.Add(new KpiElement { Name = "Internet Revenue", ShowKPIGoal = true, ShowKPIStatus = true, ShowKPIValue = true, ShowKPITrend = true });

            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Date";
            dimensionElementRow.AddLevel("Fiscal", "Fiscal");

            olapReport.CategoricalElements.Add(dimensionElementColumn);
            olapReport.CategoricalElements.Add(kpiElement);
            olapReport.SeriesElements.Add(dimensionElementRow);

            return olapReport;
        }

        #endregion

        #region LoadingComplexKPI

        /// <summary>
        /// This method is used to load the OLAP report with complex KPI elements.
        /// </summary>
        /// <returns></returns>
        OlapReport LoadComplexKPI()
        {
            OlapReport olapReport = new OlapReport();
            olapReport.CurrentCubeName = "Adventure Works";

            KpiElements kpiElement = new KpiElements();
            kpiElement.Elements.Add(new KpiElement { Name = "Internet Revenue", ShowKPIGoal = true, ShowKPIStatus = true, ShowKPIValue = true, ShowKPITrend = true });

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Employee";
            dimensionElementColumn.AddLevel("Employee Department", "Department");

            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Date";
            dimensionElementRow.AddLevel("Fiscal", "Fiscal Year");

            olapReport.CategoricalElements.Add(dimensionElementRow);
            olapReport.CategoricalElements.Add(kpiElement);
            olapReport.SeriesElements.Add(dimensionElementColumn);
            return olapReport;
        }

        #endregion

        #endregion

        #region Event Handlers

        /// <summary>
        /// The event handler method is hooked when the data manager was changed.
        /// </summary>
        /// <param name="sender">The OlapDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private void OlapDataManager_OlapDataChanged(object sender, OlapDataChangedEventArgs args)
        {
            if (args.MDXQuery != null && sender is OlapDataManager)
            {
                SetConnection();
                (sender as OlapDataManager).JSONData = clientChannel.GetJSONDataAsync(args.MDXQuery, args.SerializedReport, args.AllowMdxToOlapReportParse).Result;
            }
        }

        /// <summary>
        /// The event handler method is hooked to get the current cube schema.
        /// </summary>
        /// <param name="sender">The OlapDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetCubeSchema(object sender, GetCubeSchemaEventArgs args)
        {
            if (args.CubeName != null && sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONCubeSchemaAsync(args.CubeName).Result;
            }
            return null;
        }

        /// <summary>
        /// The event handler method is hooked to get the collection of cube information.
        /// </summary>
        /// <param name="sender">The OlapDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetCubeInfoCollection(object sender, GetCubeInfoCollectionEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONCubesAsync().Result;
            }
            return null;
        }

        /// <summary>
        /// This event handler method is invoked when selecting the combo box item.
        /// </summary>
        /// <param name="sender">The combo box.</param>
        /// <param name="e">The event argument.</param>
        private void cmbReport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.pivotGrid.OlapDataManager != null)
            {
                string selectedReport = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
                if (selectedReport == "Complex KPI")
                {
                    this.cmbProduct.IsEnabled = false;
                    this.pivotGrid.OlapDataManager.SetCurrentReport(LoadComplexKPI());
                }
                else
                {
                    this.cmbProduct.IsEnabled = true;
                    this.pivotGrid.OlapDataManager.SetCurrentReport(LoadBasicKPI());
                }
                this.pivotGrid.DataBind();
            }
        }

        /// <summary>
        /// This event handler method is invoked when selecting the combo box item.
        /// </summary>
        /// <param name="sender">The combo box.</param>
        /// <param name="e">The event argument.</param>
        private void cmbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.pivotGrid.OlapDataManager != null)
            {
                this.pivotGrid.OlapDataManager.SetCurrentReport(LoadBasicKPI());
                this.pivotGrid.DataBind();
            }
        }

        #endregion
    }
}
