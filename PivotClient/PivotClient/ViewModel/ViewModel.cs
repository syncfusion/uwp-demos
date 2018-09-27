#region Copyright
//  Copyright (c) Syncfusion Inc. 2001 - 2018. All rights reserved.
//  Use of this code is subject to the terms of our license.
//  A copy of the current license can be obtained at any time by e-mailing
//  licensing@syncfusion.com. Re-distribution in any form is strictly
//  prohibited. Any infringement will be prosecuted under applicable laws. 
// </copyright>
#endregion

namespace Syncfusion.SampleBrowser.UWP.PivotClient.PivotClient.ViewModel
{
    using System;
    using Syncfusion.Olap.UWP.Manager;
    using Syncfusion.Olap.UWP.Reports;
    using System.ServiceModel;
    using PivotChart.Model;

    public class ViewModel : IDisposable
    {
        #region Private Variables

        private OlapManagerService.IOlapDataProvider clientChannel;
        private OlapDataManager olapDataManager;
        /// <summary>
        /// Gets or sets the item source for SfPivotGrid control.
        /// </summary>
        private object productSalesData;

        #endregion

        #region Properties

        public OlapDataManager OlapDataManager
        {
            get
            {
                this.olapDataManager = this.olapDataManager ?? this.SetDataManager();
                return this.olapDataManager;
            }
            set { this.olapDataManager = value; }
        }

        /// <summary>
        /// Gets or sets the item source for SfPivotGrid control.
        /// </summary>
        public object ProductSalesData
        {
            get
            {
                this.productSalesData = this.productSalesData ?? ProductSales.GetSalesData();
                return this.productSalesData;
            }
            set { this.productSalesData = value; }
        }

        #endregion

        #region Dispose Method

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Private Methods

        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                ProductSalesData = null;
                if (this.olapDataManager != null)
                {
                    this.olapDataManager.OlapDataChanged -= this.OlapDataManager_OlapDataChanged;
                    this.olapDataManager.GetCubeSchema -= this.OlapDataManager_GetCubeSchema;
                    this.olapDataManager.GetCubeInfoCollection -= this.OlapDataManager_GetCubeInfoCollection;
                    this.olapDataManager.GetChildMembers -= this.OlapDataManager_GetChildMembers;
                    this.olapDataManager.GetChildrenByMDX -= this.OlapDataManager_GetChildrenByMDX;
                    this.olapDataManager.GetOlapDataWithTotalCount -= this.OlapDataManager_GetOlapDataWithTotalCount;
                    this.olapDataManager.GetLevelMembersUsingMdx -= this.OlapDataManager_GetLevelMembersUsingMdx;
                    this.olapDataManager.GetExecuteMemberCount -= OlapDataManager_GetExecuteMemberCount;
                    this.olapDataManager.GetParentMember -= OlapDataManager_GetParentMember;
                    this.olapDataManager.GetMeasureGroupsDimensions -= OlapDataManager_GetMeasureGroupsDimensions;
                    this.olapDataManager.ExecuteDrillThroughQuery -= OlapDataManager_ExecuteDrillThroughQuery;
                    this.olapDataManager.Dispose();
                }
                this.olapDataManager = null;
            }
        }

        private OlapDataManager SetDataManager()
        {
            this.olapDataManager = new OlapDataManager();
            this.olapDataManager.OlapDataChanged += this.OlapDataManager_OlapDataChanged;
            this.olapDataManager.GetCubeSchema += this.OlapDataManager_GetCubeSchema;
            this.olapDataManager.GetCubeInfoCollection += this.OlapDataManager_GetCubeInfoCollection;
            this.olapDataManager.GetChildMembers += this.OlapDataManager_GetChildMembers;
            this.olapDataManager.GetChildrenByMDX += this.OlapDataManager_GetChildrenByMDX;
            this.olapDataManager.GetOlapDataWithTotalCount += this.OlapDataManager_GetOlapDataWithTotalCount;
            this.olapDataManager.GetLevelMembersUsingMdx += this.OlapDataManager_GetLevelMembersUsingMdx;
            this.olapDataManager.GetExecuteMemberCount += OlapDataManager_GetExecuteMemberCount;
            this.olapDataManager.GetParentMember += OlapDataManager_GetParentMember;
            this.olapDataManager.GetMeasureGroupsDimensions += OlapDataManager_GetMeasureGroupsDimensions;
            this.olapDataManager.ExecuteDrillThroughQuery += OlapDataManager_ExecuteDrillThroughQuery;
            this.olapDataManager.SetCurrentReport(this.CreateOLAPReport());
            return this.olapDataManager;
        }

        private void SetConnection()
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.MaxBufferSize = 2147483647;
            EndpointAddress address = new EndpointAddress("http://bi.syncfusion.com/OlapUWPTestService/OlapManager.svc/");
            ChannelFactory<Syncfusion.SampleBrowser.UWP.PivotClient.OlapManagerService.IOlapDataProvider> clientFactory = new ChannelFactory<Syncfusion.SampleBrowser.UWP.PivotClient.OlapManagerService.IOlapDataProvider>(basicHttpBinding, address);
            this.clientChannel = clientFactory.CreateChannel();
        }

        private OlapReport CreateOLAPReport()
        {
            OlapReport olapReport = new OlapReport
            {
                Name = "OLAP Report",
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
                this.SetConnection();
                (sender as OlapDataManager).JSONData = this.clientChannel.GetJSONDataAsync(args.MDXQuery, args.SerializedReport, args.AllowMdxToOlapReportParse).Result;
            }
        }

        private string OlapDataManager_GetCubeSchema(object sender, GetCubeSchemaEventArgs args)
        {
            if (args.CubeName != null && sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONCubeSchemaAsync(args.CubeName).Result;
            }
            return null;
        }

        private string OlapDataManager_GetCubeInfoCollection(object sender, GetCubeInfoCollectionEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONCubesAsync().Result;
            }
            return null;
        }

        private string OlapDataManager_GetChildMembers(object sender, GetChildMembersEventArgs args)
        {
            if (args.LevelName != null && sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONChildMembersAsync(args.CubeName, args.LevelName).Result;
            }
            return "";
        }

        private string OlapDataManager_GetChildrenByMDX(object sender, GetChildrenByMDXEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.Command))
            {
                this.SetConnection();
                return this.clientChannel.GetJSONChildrenByMDXAsync(args.Command).Result;
            }
            return "";
        }

        private string OlapDataManager_GetOlapDataWithTotalCount(object sender, GetOlapDataWithTotalCountEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONDataWithTotalCountAsync(args.SerializedReport).Result;
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

        private string OlapDataManager_GetLevelMembersUsingMdx(object sender, GetLevelMembersUsingMdxEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONLevelMembersUsingMdxAsync(args.MemberUniqueName, args.AxisPosition, args.IsGrandTotalOn, args.CubeName, args.PagingParams).Result;
            }
            return null;
        }


        private string OlapDataManager_ExecuteDrillThroughQuery(object sender, ExecuteDrillThroughQueryEventArgs e)
        {
            if (sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONExecuteDrillThroughQueryAsync(e.MdxQuery).Result;
            }
            return null;
        }

        private string OlapDataManager_GetExecuteMemberCount(object sender, GetExecuteMemberCountEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONExecuteMemberCountAsync(args.MdxQuery).Result;
            }
            return null;
        }

        private string OlapDataManager_GetMeasureGroupsDimensions(object sender, GetMeasureGroupsDimensionsEventArgs e)
        {
            if (sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONMeasureGroupsDimensionsAsync(e.CubeName, e.MeasureGroupName).Result;
            }
            return null;
        }

        #endregion
    }
}