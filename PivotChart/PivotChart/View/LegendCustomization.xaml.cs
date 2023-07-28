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
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading.Tasks;
    using System.Text.RegularExpressions;
    using Windows.UI.Xaml.Input;
    using Windows.Globalization;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Syncfusion.Olap.UWP.Manager;
    using Syncfusion.Olap.UWP.Reports;
    using Syncfusion.UI.Xaml.Charts;
    using Windows.System.Profile;

    public sealed partial class LegendCustomization : SampleLayout
    {
        #region Private Variables

        /// <summary>
        /// Represents a new instance of the <see cref="T:OlapManagerService.IOlapDataProvider">IOlapDataProvider</see> class. 
        /// </summary>
        private Syncfusion.SampleBrowser.UWP.PivotChart.OlapManagerService.IOlapDataProvider clientChannel;

        /// <summary>
        /// Gets or sets the OLAP data manager.
        /// </summary>
        private OlapDataManager olapDataManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:LegendCustoization">LegendCustomization</see> class. 
        /// </summary>
        public LegendCustomization()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            InitializeComponent();
            cmb_Orientation.ItemsSource = Enum.GetValues(typeof(ChartOrientation));
            cmb_TextOverflow.ItemsSource = new List<KeyValuePair<string, TextWrapping>>
            {
                new KeyValuePair<string, TextWrapping>("NoWrap", TextWrapping.NoWrap),
                new KeyValuePair<string, TextWrapping>("Wrap", TextWrapping.Wrap),
                new KeyValuePair<string, TextWrapping>("WrapWholeWords", TextWrapping.WrapWholeWords),
            };
            PivotChart.Legend.TextOverflow = AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile" ? TextWrapping.NoWrap : TextWrapping.Wrap;
            PivotChart.Legend.ColumnCount = AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile" ? 3 : 6;
            PivotChart.Legend.RowCount = AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile" ? 3 : 1;
            PivotChart.Loaded += PivotChart_Loaded;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            if (this.PivotChart != null)
            {
                PivotChart.Loaded -= PivotChart_Loaded;
                PivotChart.Dispose();
            }
            this.PivotChart = null;
            if (olapDataManager != null)
            {
                olapDataManager.OlapDataChanged -= OlapDataManager_OlapDataChanged;
                olapDataManager.GetCubeSchema -= OlapDataManager_GetCubeSchema;
                olapDataManager.GetCubeInfoCollection -= OlapDataManager_GetCubeInfoCollection;
                olapDataManager.Dispose();
            }
            olapDataManager = null;
            if (this.cmb_Orientation != null)
                this.cmb_Orientation.SelectionChanged -= cmb_Orientation_SelectionChanged;
            this.cmb_Orientation = null;
            this.cmb_TextOverflow = null;
            if (this.txt_ColumnCount != null)
                this.txt_ColumnCount.KeyDown -= this.Txt_Count_OnKeyDown;
            this.txt_ColumnCount = null;
            if (this.txt_RowCount != null)
                this.txt_RowCount.KeyDown -= this.Txt_Count_OnKeyDown;
            this.clientChannel = null;
        }

        #endregion

        #region Setting Connection to Service

        /// <summary>
        /// This method used to establish connection with service.
        /// </summary>
        private void SetConnection()
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.MaxBufferSize = 2147483647;
            EndpointAddress address = new EndpointAddress("http://bi.syncfusion.com/OlapUWPTestService/OlapManager.svc/");
            ChannelFactory<Syncfusion.SampleBrowser.UWP.PivotChart.OlapManagerService.IOlapDataProvider> clientFactory = new ChannelFactory<Syncfusion.SampleBrowser.UWP.PivotChart.OlapManagerService.IOlapDataProvider>(basicHttpBinding, address);
            clientChannel = clientFactory.CreateChannel();
        }

        #endregion

        #region Setting OLAP Data Manager

        /// <summary>
        /// This used to set the OLAP data manager to control.
        /// </summary>
        /// <returns></returns>
        private void SetDataManager()
        {
            olapDataManager = new OlapDataManager();
            olapDataManager.OlapDataChanged += OlapDataManager_OlapDataChanged;
            olapDataManager.GetCubeSchema += OlapDataManager_GetCubeSchema;
            olapDataManager.GetCubeInfoCollection += OlapDataManager_GetCubeInfoCollection;
            olapDataManager.SetCurrentReport(CreateOlapReport());
            PivotChart.OlapDataManager = olapDataManager;
        }

        /// <summary>
        /// This method used to check the internet connection of the machine.
        /// </summary>
        /// <returns></returns>
        private bool IsConnectedToInternet()
        {
            Windows.Networking.Connectivity.ConnectionProfile connectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == Windows.Networking.Connectivity.NetworkConnectivityLevel.InternetAccess);
        }

        #endregion

        #region OLAP Report Creation

        /// <summary>
        /// OLAP report creation.
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

            olapReport.CategoricalElements.Add(dimensionElementColumn);
            olapReport.CategoricalElements.Add(measureElementColumn);
            olapReport.SeriesElements.Add(dimensionElementRow);

            return olapReport;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Occurs when pivot chart loading completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PivotChart_Loaded(object sender, RoutedEventArgs e)
        {
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

        /// <summary>
        /// Occurs when OlapDataManager changed.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="args">The event data.</param>
        private void OlapDataManager_OlapDataChanged(object sender, OlapDataChangedEventArgs args)
        {
            if (args.MDXQuery != null && sender is OlapDataManager)
            {
                SetConnection();
                (sender as OlapDataManager).JSONData = clientChannel.GetJSONDataAsync(args.MDXQuery, args.SerializedReport, args.AllowMdxToOlapReportParse).Result;
            }
        }

        /// <summary>
        /// Occurs when cube name changed.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="args">The event data.</param>
        /// <returns></returns>
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
        /// Occurs when current cube name changed.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="args">The event data.</param>
        /// <returns></returns>
        private string OlapDataManager_GetCubeInfoCollection(object sender, GetCubeInfoCollectionEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONCubesAsync().Result;
            }
            return null;
        }

        private void cmb_Orientation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_Orientation.SelectedItem != null)
            {
                txt_ColumnCount.IsEnabled = (ChartOrientation)cmb_Orientation.SelectedItem == ChartOrientation.Default;
                txt_RowCount.IsEnabled = (ChartOrientation)cmb_Orientation.SelectedItem == ChartOrientation.Default;
                cmb_TextOverflow.IsEnabled = (ChartOrientation)cmb_Orientation.SelectedItem == ChartOrientation.Default;
            }
        }

        private void Txt_Count_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch((sender as TextBox).Text);
            }
        }

        #endregion
    }
}