#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotChart
{
    using System;
    using System.ServiceModel;
    using Syncfusion.Olap.UWP.Manager;
    using Syncfusion.Olap.UWP.Reports;
    using Syncfusion.SampleBrowser.UWP.PivotChart.OlapManagerService;

    #region View Model for normal PivotChart

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
        IOlapDataProvider clientChannel;
        /// <summary>
        /// Gets or sets the OlapDataManager.
        /// </summary>
        OlapDataManager olapDataManager;

        #endregion

        #region Public Properties

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

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Setting Connection to Service
        /// <summary>
        /// Method is used to set the data connection from on-line cube.
        /// </summary>

        private void SetConnection()
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.MaxBufferSize = 2147483647;
            EndpointAddress address = new EndpointAddress("http://bi.syncfusion.com/OlapUWPTestService/OlapManager.svc/");
            ChannelFactory<IOlapDataProvider> clientFactory = new ChannelFactory<IOlapDataProvider>(basicHttpBinding, address);
            this.clientChannel = clientFactory.CreateChannel();
        }

        #endregion

        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (this.olapDataManager != null)
                {
                    this.olapDataManager.OlapDataChanged -= this.OlapDataManager_OlapDataChanged;
                    this.olapDataManager.GetCubeSchema -= this.OlapDataManager_GetCubeSchema;
                    this.olapDataManager.GetCubeInfoCollection -= this.OlapDataManager_GetCubeInfoCollection;
                    this.olapDataManager.Dispose();
                }
                this.olapDataManager = null;
                this.clientChannel = null;
            }
        }

        #region Setting OLAP Data Manager
        /// <summary>
        /// The event handler method is hooked when the data manager was changed.
        /// </summary>
        /// <param name="sender">The OlpaDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private OlapDataManager SetDataManager()
        {
            this.olapDataManager = new OlapDataManager();
            this.olapDataManager.OlapDataChanged += this.OlapDataManager_OlapDataChanged;
            this.olapDataManager.GetCubeSchema += this.OlapDataManager_GetCubeSchema;
            this.olapDataManager.GetCubeInfoCollection += this.OlapDataManager_GetCubeInfoCollection;
            this.olapDataManager.SetCurrentReport(this.CreateOlapReport());
            return this.olapDataManager;
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

            olapReport.CategoricalElements.Add(dimensionElementColumn);
            olapReport.CategoricalElements.Add(measureElementColumn);
            olapReport.SeriesElements.Add(dimensionElementRow);

            return olapReport;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// The event handler method is hooked when the data manager was changed.
        /// </summary>
        /// <param name="sender">The OlpaDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private void OlapDataManager_OlapDataChanged(object sender, OlapDataChangedEventArgs args)
        {
            if (args.MDXQuery != null && sender is OlapDataManager)
            {
                this.SetConnection();
                (sender as OlapDataManager).JSONData = this.clientChannel.GetJSONDataAsync(args.MDXQuery, args.SerializedReport, args.AllowMdxToOlapReportParse).Result;
            }
        }

        /// <summary>
        /// The event handler method is hooked to get the current cube schema.
        /// </summary>
        /// <param name="sender">The OlpaDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetCubeSchema(object sender, GetCubeSchemaEventArgs args)
        {
            if (args.CubeName != null && sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONCubeSchemaAsync(args.CubeName).Result;
            }
            return null;
        }

        /// <summary>
        /// The event handler method is hooked to get the collection of cube information.
        /// </summary>
        /// <param name="sender">The OlpaDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetCubeInfoCollection(object sender, GetCubeInfoCollectionEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONCubesAsync().Result;
            }
            return null;
        }

        #endregion
    }

    #endregion

    #region View Model for PivotChart containing KPIs

    public class ViewModelKPI : IDisposable
    {
        #region Private Fields

        /// <summary>
        /// Gets or sets the client channel.
        /// </summary>
        IOlapDataProvider clientChannel;
        /// <summary>
        /// Gets or sets the OlapDataManager.
        /// </summary>
        OlapDataManager olapDataManager;

        #endregion

        #region Public Properties

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

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Setting Connection to Service
        /// <summary>
        /// Method is used to set the data connection from on-line cube.
        /// </summary>

        private void SetConnection()
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.MaxBufferSize = 2147483647;
            EndpointAddress address = new EndpointAddress("http://bi.syncfusion.com/OlapUWPTestService/OlapManager.svc/");
            ChannelFactory<IOlapDataProvider> clientFactory = new ChannelFactory<IOlapDataProvider>(basicHttpBinding, address);
            this.clientChannel = clientFactory.CreateChannel();
        }

        #endregion

        #region Setting OLAP Data Manager
        /// <summary>
        /// The event handler method is hooked when the data manager was changed.
        /// </summary>
        /// <param name="sender">The OlpaDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private OlapDataManager SetDataManager()
        {
            this.olapDataManager = new OlapDataManager();
            this.olapDataManager.OlapDataChanged += this.OlapDataManager_OlapDataChanged;
            this.olapDataManager.GetCubeSchema += this.OlapDataManager_GetCubeSchema;
            this.olapDataManager.GetCubeInfoCollection += this.OlapDataManager_GetCubeInfoCollection;
            this.olapDataManager.SetCurrentReport(this.CreateKPIReport());
            return this.olapDataManager;
        }

        #endregion

        #region OLAP Report Creation
        /// <summary>
        /// This method is used to create the new OLAP report with cube information.
        /// </summary>
        /// <returns></returns>
        private OlapReport CreateKPIReport()
        {
            OlapReport olapReport = new OlapReport() { Name = "VirtualKPI.Report" };
            olapReport.CurrentCubeName = "Adventure Works";

            #region KPI Elements

            KpiElements kpiElement = new KpiElements();
            kpiElement.Elements.Add(new KpiElement { Name = "Revenue", ShowKPIStatus = true, ShowKPIGoal = false, ShowKPITrend = true, ShowKPIValue = true });

            #endregion

            #region Measure Elements

            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Sales Amount" });

            #endregion

            #region Dimension Elements

            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Date";
            dimensionElementRow.AddLevel("Fiscal", "Fiscal Year");

            DimensionElement dimensionElement = new DimensionElement();
            dimensionElement.Name = "Employee";
            dimensionElement.AddLevel("Employee", "Employee");

            DimensionElement dimensionElement1 = new DimensionElement();
            dimensionElement1.Name = "Customer";
            dimensionElement1.AddLevel("Customer Geography", "Country");

            DimensionElement internalDimension = new DimensionElement();
            internalDimension.Name = "Customer";
            internalDimension.AddLevel("Customer", "Customer");

            #endregion

            #region Report Elements

            olapReport.CategoricalElements.Add(measureElementColumn);
            olapReport.CategoricalElements.Add(kpiElement);
            olapReport.SeriesElements.Add(dimensionElementRow);

            #endregion

            return olapReport;
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// The event handler method is hooked when the data manager was changed.
        /// </summary>
        /// <param name="sender">The OlpaDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private void OlapDataManager_OlapDataChanged(object sender, OlapDataChangedEventArgs args)
        {
            if (args.MDXQuery != null && sender is OlapDataManager)
            {
                this.SetConnection();
                (sender as OlapDataManager).JSONData = this.clientChannel.GetJSONDataAsync(args.MDXQuery, args.SerializedReport, args.AllowMdxToOlapReportParse).Result;
            }
        }

        /// <summary>
        /// The event handler method is hooked to get the current cube schema.
        /// </summary>
        /// <param name="sender">The OlpaDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetCubeSchema(object sender, GetCubeSchemaEventArgs args)
        {
            if (args.CubeName != null && sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONCubeSchemaAsync(args.CubeName).Result;
            }
            return null;
        }

        /// <summary>
        /// The event handler method is hooked to get the collection of cube information.
        /// </summary>
        /// <param name="sender">The OlpaDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetCubeInfoCollection(object sender, GetCubeInfoCollectionEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONCubesAsync().Result;
            }
            return null;
        }

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        /// <param name="isDisposing">The boolean value.</param>
        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (this.olapDataManager != null)
                {
                    this.olapDataManager.OlapDataChanged -= this.OlapDataManager_OlapDataChanged;
                    this.olapDataManager.GetCubeSchema -= this.OlapDataManager_GetCubeSchema;
                    this.olapDataManager.GetCubeInfoCollection -= this.OlapDataManager_GetCubeInfoCollection;
                    this.olapDataManager.Dispose();
                }
                this.olapDataManager = null;
                this.clientChannel = null;
            }
        }

        #endregion
    }

    #endregion

    #region View Model for PivotChart containing adornments

    /// <summary>
    /// Represents the ViewModel for AdornmetsInfo.
    /// </summary>
    public class ViewModelAdornments : IDisposable
    {
        #region Private Variables

        /// <summary>
        /// Gets or sets the client channel.
        /// </summary>
        IOlapDataProvider clientChannel;
        /// <summary>
        /// Gets or sets the OlapDataManager.
        /// </summary>
        OlapDataManager olapDataManager;

        #endregion

        #region Public Properties

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

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Setting Connection to Service
        /// <summary>
        /// Method is used to set the data connection from on-line cube.
        /// </summary>

        private void SetConnection()
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.MaxBufferSize = 2147483647;
            EndpointAddress address = new EndpointAddress("http://bi.syncfusion.com/OlapUWPTestService/OlapManager.svc/");
            ChannelFactory<IOlapDataProvider> clientFactory = new ChannelFactory<IOlapDataProvider>(basicHttpBinding, address);
            this.clientChannel = clientFactory.CreateChannel();
        }

        #endregion

        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (this.olapDataManager != null)
                {
                    this.olapDataManager.OlapDataChanged -= this.OlapDataManager_OlapDataChanged;
                    this.olapDataManager.GetCubeSchema -= this.OlapDataManager_GetCubeSchema;
                    this.olapDataManager.GetCubeInfoCollection -= this.OlapDataManager_GetCubeInfoCollection;
                    this.olapDataManager.Dispose();
                }
                this.olapDataManager = null;
                this.clientChannel = null;
            }
        }

        #region Setting OLAP Data Manager
        /// <summary>
        /// The event handler method is hooked when the data manager was changed.
        /// </summary>
        /// <param name="sender">The OlpaDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private OlapDataManager SetDataManager()
        {
            this.olapDataManager = new OlapDataManager();
            this.olapDataManager.OlapDataChanged += this.OlapDataManager_OlapDataChanged;
            this.olapDataManager.GetCubeSchema += this.OlapDataManager_GetCubeSchema;
            this.olapDataManager.GetCubeInfoCollection += this.OlapDataManager_GetCubeInfoCollection;
            this.olapDataManager.SetCurrentReport(this.CreateOlapReport());
            return this.olapDataManager;
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

            //Specifying the Column Name for the Dimension and measure elements
            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Customer";
            dimensionElementRow.AddLevel("Customer Geography", "Country");

            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Internet Sales Amount" });

            //Specifying the Row Name for the Dimension element
            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Date";
            dimensionElementColumn.AddLevel("Fiscal", "Fiscal Year");

            // Adding Column Members
            olapReport.CategoricalElements.Add(dimensionElementColumn);
            // Adding Measure Element
            olapReport.CategoricalElements.Add(measureElementColumn);
            // Adding Row Members
            olapReport.SeriesElements.Add(dimensionElementRow);
            return olapReport;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// The event handler method is hooked when the data manager was changed.
        /// </summary>
        /// <param name="sender">The OlpaDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private void OlapDataManager_OlapDataChanged(object sender, OlapDataChangedEventArgs args)
        {
            if (args.MDXQuery != null && sender is OlapDataManager)
            {
                this.SetConnection();
                (sender as OlapDataManager).JSONData = this.clientChannel.GetJSONDataAsync(args.MDXQuery, args.SerializedReport, args.AllowMdxToOlapReportParse).Result;
            }
        }

        /// <summary>
        /// The event handler method is hooked to get the current cube schema.
        /// </summary>
        /// <param name="sender">The OlpaDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetCubeSchema(object sender, GetCubeSchemaEventArgs args)
        {
            if (args.CubeName != null && sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONCubeSchemaAsync(args.CubeName).Result;
            }
            return null;
        }

        /// <summary>
        /// The event handler method is hooked to get the collection of cube information.
        /// </summary>
        /// <param name="sender">The OlpaDataManager.</param>
        /// <param name="args">The argument parameter.</param>
        private string OlapDataManager_GetCubeInfoCollection(object sender, GetCubeInfoCollectionEventArgs args)
        {
            if (sender is OlapDataManager)
            {
                this.SetConnection();
                return this.clientChannel.GetJSONCubesAsync().Result;
            }
            return null;
        }

        #endregion
    }

    #endregion
}