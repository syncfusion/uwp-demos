#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotGrid.Tutorials.PivotGridSamples.ViewModel
{
    using Model;
    using Syncfusion.UI.Xaml.PivotGrid;
    using Syncfusion.PivotAnalysis.UWP;
    using Syncfusion.Olap.UWP.Manager;
    using System;
    using Syncfusion.Olap.UWP.Reports;
    using System.ServiceModel;
    /// <summary>
    /// This class contains the view model informations with respect to bind them into SfPivotGrid.
    /// It derives from <see cref="IDisposable">IDisposable.</see>/>
    /// </summary>
    public class ViewModel : IDisposable
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
        /// <summary>
        /// Gets or sets the item source for SfPivotGrid control.
        /// </summary>
        private object productSalesData;
        /// <summary>
        /// Gets or sets the item source for SfPivotGrid control.
        /// </summary>
        private object businessSalesData;
        /// <summary>
        /// Gets or sets the item source for SfPivotGrid control.
        /// </summary>

        #endregion

        #region Public Properties

        public object ProductSalesData
        {
            get
            {
                this.productSalesData = this.productSalesData ?? ProductSales.GetSalesData();
                return this.productSalesData;
            }
            set { this.productSalesData = value; }
        }
        /// <summary>
        /// Gets or sets the item source for SfPivotGrid control.
        /// </summary>
        public object BusinessSalesData
        {
            get
            {
                this.businessSalesData = this.businessSalesData ?? BusinessObjectCollection.GetList(2000);
                return this.businessSalesData;
            }
            set { this.businessSalesData = value; }
        }
        /// <summary>
        /// Gets or sets the OlapDataManager.
        /// </summary>
        public OlapDataManager OlapDataManager
        {
            get
            {
                this.olapDataManager = this.olapDataManager ?? this.SetDataManager();
                return this.olapDataManager;
            }
            set { this.olapDataManager = value; }
        }

        #endregion

        #region IDisposable Method

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// To set the OLAP data manager for SfPivotGrid.
        /// </summary>
        /// <returns>The OLAP data manager.</returns>
        private OlapDataManager SetDataManager()
        {
            olapDataManager = new OlapDataManager();
            olapDataManager.OlapDataChanged += OlapDataManager_OlapDataChanged;
            olapDataManager.GetCubeSchema += OlapDataManager_GetCubeSchema;
            olapDataManager.GetCubeInfoCollection += OlapDataManager_GetCubeInfoCollection;
            olapDataManager.GetChildMembers += OlapDataManager_GetChildMembers;
            olapDataManager.GetChildrenByMDX += OlapDataManager_GetChildrenByMDX;
            olapDataManager.GetOlapDataWithTotalCount += OlapDataManager_GetOlapDataWithTotalCount;
            olapDataManager.GetLevelMembersUsingMdx += OlapDataManager_GetLevelMembersUsingMDX;
            olapDataManager.GetExecuteMemberCount += OlapDataManager_GetExecuteMemberCount;
            olapDataManager.SetCurrentReport(CreateOlapReport());
            return this.olapDataManager;
        }

        /// <summary>
        /// Method is used to set the data connection from on-line cuber.
        /// </summary>
        private void SetConnection()
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.MaxBufferSize = 2147483647;
            EndpointAddress address = new EndpointAddress("http://bi.syncfusion.com/OlapUWPTestService/OlapManager.svc/");
            ChannelFactory<Syncfusion.SampleBrowser.UWP.PivotGrid.OlapManagerService.IOlapDataProvider> clientFactory = new ChannelFactory<Syncfusion.SampleBrowser.UWP.PivotGrid.OlapManagerService.IOlapDataProvider>(basicHttpBinding, address);
            this.clientChannel = clientFactory.CreateChannel();
        }


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

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        /// <param name="isDisposing">The boolean value</param>
        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (this.olapDataManager != null)
                {
                    olapDataManager.OlapDataChanged -= OlapDataManager_OlapDataChanged;
                    olapDataManager.GetCubeSchema -= OlapDataManager_GetCubeSchema;
                    olapDataManager.GetCubeInfoCollection -= OlapDataManager_GetCubeInfoCollection;
                    olapDataManager.GetChildMembers -= OlapDataManager_GetChildMembers;
                    olapDataManager.GetChildrenByMDX -= OlapDataManager_GetChildrenByMDX;
                    olapDataManager.GetOlapDataWithTotalCount -= OlapDataManager_GetOlapDataWithTotalCount;
                    olapDataManager.GetLevelMembersUsingMdx -= OlapDataManager_GetLevelMembersUsingMDX;
                    olapDataManager.GetExecuteMemberCount -= OlapDataManager_GetExecuteMemberCount;
                    olapDataManager.Dispose();
                    OlapDataManager = null;
                }
                ProductSalesData = null;
                BusinessSalesData = null;
            }
        }

        #endregion

        #region OLAP Data Manager Event Handlers

        /// <summary>
        /// The event handler method is hooked when the data manager was changed.
        /// </summary>
        /// <param name="sender">The OLAP data manager.</param>
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
        /// <param name="sender">The OLAP data manager.</param>
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
        /// The event handler method is hooked to get the current cube schema.
        /// </summary>
        /// <param name="sender">The OLAP data manager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetLevelMembersUsingMDX(object sender, GetLevelMembersUsingMdxEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONLevelMembersUsingMdxAsync(args.MemberUniqueName, args.AxisPosition, args.IsGrandTotalOn, args.CubeName, args.PagingParams).Result;
            }
            return null;
        }

        /// <summary>
        /// The event handler method is hooked to get the collection of cube information.
        /// </summary>
        /// <param name="sender">The OLAP data manager.</param>
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
        /// The event handler method is hooked to get the collection of child members.
        /// </summary>
        /// <param name="sender">The OLAP data manager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetChildMembers(object sender, GetChildMembersEventArgs args)
        {
            if (args.LevelName != null && sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONChildMembersAsync(args.CubeName, args.LevelName).Result;
            }
            return "";
        }

        /// <summary>
        /// The event handler method is hooked to get the children by using MDX query.
        /// </summary>
        /// <param name="sender">The OLAP data manager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetChildrenByMDX(object sender, GetChildrenByMDXEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.Command))
            {
                SetConnection();
                return clientChannel.GetJSONChildrenByMDXAsync(args.Command).Result;
            }
            return "";
        }

        /// <summary>
        /// The event handler method is hooked to get the total count of OLAP data.
        /// </summary>
        /// <param name="sender">The OLAP data manager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetOlapDataWithTotalCount(object sender, GetOlapDataWithTotalCountEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONDataWithTotalCountAsync(args.SerializedReport).Result;
            }
            return null;
        }

        /// <summary>
        /// The event handler method is hooked to get the member count.
        /// </summary>
        /// <param name="sender">The OLAP data manager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetExecuteMemberCount(object sender, GetExecuteMemberCountEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                SetConnection();
                return clientChannel.GetJSONExecuteMemberCountAsync(args.MdxQuery).Result;
            }
            return null;
        }

        #endregion
    }

    #region Custom EditingManager Class

    /// <summary>
    /// Custom editing manager class. 
    /// Override ChangeValue to affect how the change is made or shown.
    /// Here we add a * to the edited value cells.
    /// </summary>
    public class CustomEditManager : PivotEditingManager
    {
        /// <summary>
        /// Initializes the new instance of <see cref="CustomEditManager">CustomEditManager.</see>/>
        /// </summary>
        /// <param name="pg">The SfPivotGrid control.</param>
        public CustomEditManager(SfPivotGrid pg)
            : base(pg) { }
        /// <summary>
        /// The overridden method was called when changing the value.
        /// </summary>
        /// <param name="oldValue">The exiting cell value.</param>
        /// <param name="newValue">The current value.</param>
        /// <param name="row1">The row index.</param>
        /// <param name="col1">The column index.</param>
        /// <param name="pi">The pivot cell information.</param>
        protected override void ChangeValue(object oldValue, object newValue, int row1, int col1, PivotCellInfo pi)
        {
            //do the base change
            base.ChangeValue(oldValue, newValue, row1, col1, pi);

            //mark all the adjusted cell contents
            pi.FormattedText += "*";
        }
    }

    #endregion
}
