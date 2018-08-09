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
    using System.ServiceModel;
    using System.Threading.Tasks;
    using Windows.Globalization;
    using Windows.UI.Xaml;
    using Syncfusion.Olap.UWP.Manager;
    using Syncfusion.Olap.UWP.Reports;
    using Common;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Paging : SampleLayout
    {
        #region Private Variables

        private Syncfusion.SampleBrowser.UWP.PivotClient.OlapManagerService.IOlapDataProvider clientChannel;
        private OlapDataManager olapDataManager;

        #endregion

        #region Constructor

        public Paging()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            if (IsConnectedToInternet())
            {
                InitializeComponent();
                pivotClient1.Loaded += PivotClient_Loaded;
            }
            else
            {
                var result = new Windows.UI.Popups.MessageDialog("Internet connection is required to run this sample.");
                Task.Run(async () => await result.ShowAsync());
            }
        }

        #endregion

        #region Dispose Method

        public sealed override void Dispose()
        {
            if (pivotClient1 != null)
            {
                pivotClient1.Loaded -= PivotClient_Loaded;
                pivotClient1.Dispose();
            }
            pivotClient1 = null;

            if (olapDataManager != null)
            {
                olapDataManager.OlapDataChanged -= OlapDataManager_OlapDataChanged;
                olapDataManager.GetCubeSchema -= OlapDataManager_GetCubeSchema;
                olapDataManager.GetCubeInfoCollection -= OlapDataManager_GetCubeInfoCollection;
                olapDataManager.GetChildMembers -= OlapDataManager_GetChildMembers;
                olapDataManager.GetChildrenByMDX -= OlapDataManager_GetChildrenByMDX;
                olapDataManager.GetOlapDataWithTotalCount -= OlapDataManager_GetOlapDataWithTotalCount;
                olapDataManager.GetLevelMembersUsingMdx -= OlapDataManager_GetLevelMembersUsingMdx;
                olapDataManager.GetParentMember -= OlapDataManager_GetParentMember;
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
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.MaxBufferSize = 2147483647;
            EndpointAddress address = new EndpointAddress("http://bi.syncfusion.com/OlapUWPTestService/OlapManager.svc/");
            ChannelFactory<Syncfusion.SampleBrowser.UWP.PivotClient.OlapManagerService.IOlapDataProvider> clientFactory = new ChannelFactory<Syncfusion.SampleBrowser.UWP.PivotClient.OlapManagerService.IOlapDataProvider>(basicHttpBinding, address);
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
            olapDataManager.GetParentMember += OlapDataManager_GetParentMember;
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
                Name = "OLAP Report",
                CurrentCubeName = "Adventure Works"
            };

            olapReport.PagerOptions.CategorialPageSize = 5;
            olapReport.PagerOptions.SeriesPageSize = 5;

            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Internet Sales Amount" });
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Sales Amount" });
            olapReport.CategoricalElements.Add(measureElementColumn);

            DimensionElement dimensionElement = new DimensionElement() { Name = "Customer" };
            dimensionElement.AddLevel("Customer Geography", "Country");
            olapReport.SeriesElements.Add(dimensionElement);

            dimensionElement = new DimensionElement() { Name = "Date" };
            dimensionElement.AddLevel("Fiscal", "Fiscal Year");
            olapReport.CategoricalElements.Add(dimensionElement);

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

        private string OlapDataManager_GetParentMember(object sender, ParentMemberEventArgs e)
        {
            if (sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONParentMemberAsync(e.UniqueName, e.CurrentCube).Result;
            }
            return null;
        }

        private void PivotClient_Loaded(object sender, RoutedEventArgs e)
        {
            SetDataManager();
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

        #endregion
    }
}