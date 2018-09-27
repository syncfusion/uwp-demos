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
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using System.Threading.Tasks;
    using Windows.Globalization;
    using Common;

    public sealed partial class Hyperlink : SampleLayout
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
        /// Initializes the new instance of <see cref="Hyperlink">Hyperlink.</see>/>
        /// </summary>
        public Hyperlink()
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
            if (olapDataManager != null)
            {
                olapDataManager.OlapDataChanged -= OlapDataManager_OlapDataChanged;
                olapDataManager.GetCubeSchema -= OlapDataManager_GetCubeSchema;
                olapDataManager.GetCubeInfoCollection -= OlapDataManager_GetCubeInfoCollection;
                olapDataManager.Dispose();
            }
            if (chkEnableColumnHeaderHyperlink != null)
                chkEnableColumnHeaderHyperlink.Checked -= chkEnableColumnHeaderHyperlink_Checked;
            chkEnableColumnHeaderHyperlink = null;
            if (chkEnableRowHeaderHyperlink != null)
                chkEnableRowHeaderHyperlink.Checked -= chkEnableRowHeaderHyperlink_Checked;
            chkEnableRowHeaderHyperlink = null;
            if (chkEnableSummaryColumnHeaderHyperlink != null)
                chkEnableSummaryColumnHeaderHyperlink.Checked -= chkEnableSummaryColumnHeaderHyperlink_Checked;
            chkEnableSummaryColumnHeaderHyperlink = null;
            if (chkEnableSummaryRowHeaderHyperlink != null)
                chkEnableSummaryRowHeaderHyperlink.Checked -= chkEnableSummaryRowHeaderHyperlink_Checked;
            chkEnableSummaryRowHeaderHyperlink = null;
            if (chkEnableValueCellsHyperlink != null)
                chkEnableValueCellsHyperlink.Checked -= chkEnableValueCellsHyperlink_Checked;
            chkEnableValueCellsHyperlink = null;
            if (pivotGrid != null)
            {
                pivotGrid.ValueCellStyle.IsHyperlinkCell = false;
                pivotGrid.RowHeaderStyle.IsHyperlinkCell = false;
                pivotGrid.ColumnHeaderStyle.IsHyperlinkCell = false;
                pivotGrid.SummaryColumnStyle.IsHyperlinkCell = false;
                pivotGrid.SummaryRowStyle.IsHyperlinkCell = false;
                pivotGrid.Dispose();
            }
            pivotGrid = null;
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
            olapDataManager.GetCubeSchema += OlapDataManager_GetCubeSchema;
            olapDataManager.GetCubeInfoCollection += OlapDataManager_GetCubeInfoCollection;
            olapDataManager.SetCurrentReport(CreateOlapReport());
            this.pivotGrid.OlapDataManager = olapDataManager;
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

        /// <summary>
        /// This method is used to create the new OLAP report with cube information.
        /// </summary>
        /// <returns></returns>
        private OlapReport CreateOlapReport()
        {
            OlapReport olapReport = new OlapReport();
            olapReport.CurrentCubeName = "Adventure Works";

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Customer";
            dimensionElementColumn.HierarchyName = "Customer Geography";
            dimensionElementColumn.AddLevel("Customer Geography", "Country");

            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Internet Sales Amount" });

            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Date";
            dimensionElementRow.AddLevel("Fiscal", "Fiscal Year");

            olapReport.CategoricalElements.Add(new Item { ElementValue = dimensionElementColumn });
            olapReport.CategoricalElements.Add(new Item { ElementValue = measureElementColumn });
            olapReport.SeriesElements.Add(new Item { ElementValue = dimensionElementRow });

            return olapReport;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// The event handler method is hooked when the data manager was changed.
        /// </summary>
        /// <param name="sender">The OlapDataManager.</param>
        /// <param name="args">The arguement parameter.</param>
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
        /// <param name="args">The arguement parameter.</param>
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
        /// <param name="args">The arguement parameter.</param>
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
        /// The event handler is invoked when clicking the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkEnableColumnHeaderHyperlink_Checked(object sender, RoutedEventArgs e)
        {
            this.pivotGrid.ColumnHeaderStyle.IsHyperlinkCell = (bool)(sender as CheckBox).IsChecked.Value;
        }

        /// <summary>
        /// The event handler is invoked when clicking the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkEnableRowHeaderHyperlink_Checked(object sender, RoutedEventArgs e)
        {
            this.pivotGrid.RowHeaderStyle.IsHyperlinkCell = (bool)(sender as CheckBox).IsChecked.Value;
        }

        /// <summary>
        /// The event handler is invoked when clicking the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkEnableSummaryColumnHeaderHyperlink_Checked(object sender, RoutedEventArgs e)
        {
            this.pivotGrid.SummaryColumnStyle.IsHyperlinkCell = (bool)(sender as CheckBox).IsChecked.Value;
        }

        /// <summary>
        /// The event handler is invoked when clicking the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkEnableSummaryRowHeaderHyperlink_Checked(object sender, RoutedEventArgs e)
        {
            this.pivotGrid.SummaryRowStyle.IsHyperlinkCell = (bool)(sender as CheckBox).IsChecked.Value;
        }

        /// <summary>
        /// The event handler is invoked when clicking the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkEnableValueCellsHyperlink_Checked(object sender, RoutedEventArgs e)
        {
            this.pivotGrid.ValueCellStyle.IsHyperlinkCell = (bool)(sender as CheckBox).IsChecked.Value;
        }

        #endregion
    }
}
