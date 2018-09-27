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
    using Windows.Globalization;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Common;
    using Syncfusion.Olap.UWP.Manager;
    using Syncfusion.Olap.UWP.Reports;
    using Windows.UI.Popups;
    using System.Threading.Tasks;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MDXQuery : SampleLayout
    {
        #region Private Variables

        private Syncfusion.SampleBrowser.UWP.PivotClient.OlapManagerService.IOlapDataProvider clientChannel;
        private OlapDataManager olapDataManager;

        #endregion

        #region Constructor

        public MDXQuery()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            if (IsConnectedToInternet())
            {
                InitializeComponent();
                PivotClient1.Loaded += PivotClient1_Loaded;
            }
            else
            {
                var result = new MessageDialog("Internet connection is required to run this sample.");
                Task.Run(async () => await result.ShowAsync());
            }
        }

        #endregion

        #region Dipose Method

        public sealed override void Dispose()
        {
            if (PivotClient1 != null)
            {
                PivotClient1.Loaded -= PivotClient1_Loaded;
                PivotClient1.Dispose();
            }
            PivotClient1 = null;

            ShowMDXEditor.Click -= ShowMDXEditor_Click;
            ShowLevelTypeAll.Click -= ShowLevelTypeAll_Click;

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
            olapDataManager.SetCurrentReport(CreateSimpleReport());
            PivotClient1.OlapDataManager = olapDataManager;
        }

        private bool IsConnectedToInternet()
        {
            Windows.Networking.Connectivity.ConnectionProfile connectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == Windows.Networking.Connectivity.NetworkConnectivityLevel.InternetAccess);
        }

        #region Report with KPI

        private OlapReport CreateKPIReport()
        {
            OlapReport olapReport = new OlapReport { Name = "VirtualKPI.Report" };
            olapReport.CurrentCubeName = "Adventure Works";

            // KPI elements
            KpiElements kpiElement = new KpiElements();
            kpiElement.Elements.Add(new KpiElement { Name = "Revenue", ShowKPIStatus = true, ShowKPIGoal = false, ShowKPITrend = true, ShowKPIValue = true });

            // Measure elements
            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Sales Amount" });

            // Dimension elements
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

            // Adding measure element
            olapReport.CategoricalElements.Add(measureElementColumn);

            // Adding KPI element
            olapReport.CategoricalElements.Add(kpiElement);

            // Adding row members
            olapReport.SeriesElements.Add(dimensionElementRow);

            return olapReport;
        }

        #endregion

        #region Simple Report

        private OlapReport CreateSimpleReport()
        {
            OlapReport olapReport = new OlapReport
            {
                Name = "Simple Report",
                CurrentCubeName = "Adventure Works"
            };

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Customer";
            dimensionElementColumn.HierarchyName = "Customer Geography";
            dimensionElementColumn.AddLevel("Customer Geography", "Country");

            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Internet Sales Amount" });
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Sales Amount" });
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Customer Count" });

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

        #region Slicing by Dimensions Report

        private OlapReport CreateSlicingReport()
        {
            OlapReport olapReport = new OlapReport
            {
                Name = "Sliced Report",
                CurrentCubeName = "Adventure Works"
            };

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Customer";
            dimensionElementColumn.AddLevel("Customer Geography", "Country");

            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Date";
            dimensionElementRow.AddLevel("Fiscal", "Fiscal Year");

            DimensionElement dimensionElementSlicer = new DimensionElement();
            dimensionElementSlicer.Name = "Sales Channel";
            dimensionElementSlicer.AddLevel("Sales Channel", "Sales Channel");

            MeasureElements measureElementRow = new MeasureElements();
            measureElementRow.Elements.Add(new MeasureElement { Name = "Internet Sales Amount" });

            // Adding column members.
            olapReport.CategoricalElements.Add(dimensionElementColumn);

            // Adding row members.
            olapReport.SeriesElements.Add(dimensionElementRow);

            // Adding measure elements.
            olapReport.SeriesElements.Add(measureElementRow);

            // Adding slicer members
            olapReport.SlicerElements.Add(dimensionElementSlicer);

            return olapReport;
        }

        #endregion

        #region Filtered Dimensions Report

        private OlapReport CreateFilteringReport()
        {
            OlapReport olapReport = new OlapReport
            {
                Name = "Filtered Dimensions Report",
                CurrentCubeName = "Adventure Works"
            };

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Customer";
            dimensionElementColumn.AddLevel("Customer Geography", "Country");

            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Date";
            dimensionElementRow.AddLevel("Fiscal", "Fiscal Year");
            dimensionElementRow.Hierarchy.LevelElements["Fiscal Year"].IncludeAvailableMembers = true;
            dimensionElementRow.Hierarchy.LevelElements["Fiscal Year"].Add("FY 2002");

            MeasureElements measureElementRow = new MeasureElements();
            measureElementRow.Elements.Add(new MeasureElement { Name = "Internet Sales Amount" });

            // Adding column members
            olapReport.CategoricalElements.Add(dimensionElementColumn);

            // Adding row members
            olapReport.SeriesElements.Add(dimensionElementRow);

            // Adding measure element
            olapReport.SeriesElements.Add(measureElementRow);

            return olapReport;
        }

        #endregion

        #region Sorted Measures Report

        private OlapReport CreateSortingReport()
        {
            OlapReport olapReport = new OlapReport
            {
                Name = "Sorted Measures Report",
                CurrentCubeName = "Adventure Works"
            };

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Customer";
            dimensionElementColumn.HierarchyName = "Customer Geography";
            dimensionElementColumn.AddLevel("Customer Geography", "Country");

            // Creating measure elements
            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Internet Sales Amount" });

            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Date";
            dimensionElementRow.AddLevel("Fiscal", "Fiscal Year");

            // SortOrder.BDESC for Breaking Hierarchy with Descending
            // SortOrder.DESC for displaying values in Descending Order regardless of hierarchy
            // SortOrder.BASC for Breaking Hierarchy with Ascending
            // SortOrder.ASC for displaying values in Ascending Order regardless of hierarchy
            SortElement sortElement = new SortElement(AxisPosition.Categorical, SortOrder.BDESC, true);
            sortElement.Element.UniqueName = "[Measures].[Internet Sales Amount]";

            // Adding measure element
            olapReport.CategoricalElements.Add(measureElementColumn);

            //Adding sort element
            olapReport.CategoricalElements.Add(sortElement);

            // Adding column members
            olapReport.CategoricalElements.Add(dimensionElementColumn);

            // Adding row members
            olapReport.SeriesElements.Add(dimensionElementRow);

            return olapReport;

        }

        #endregion

        #region Top-Count Report

        private OlapReport CreateTopCountReport()
        {
            OlapReport olapReport = new OlapReport
            {
                Name = "Top-Count Report",
                CurrentCubeName = "Adventure Works"
            };

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Customer";
            dimensionElementColumn.AddLevel("Customer Geography", "Country");

            // Creating measure element
            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Internet Sales Amount" });

            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Date";
            dimensionElementRow.AddLevel("Fiscal", "Fiscal Year");

            // Filtering the top 5 elements of "Internet Sales Amount".
            TopCountElement topCountElement = new TopCountElement(AxisPosition.Categorical, 5);
            topCountElement.MeasureName = "Internet Sales Amount";

            // Adding column members
            olapReport.CategoricalElements.Add(dimensionElementColumn);

            // Adding measure element
            olapReport.CategoricalElements.Add(measureElementColumn);

            // Adding measure element
            olapReport.CategoricalElements.Add(topCountElement);

            // Adding row members
            olapReport.SeriesElements.Add(dimensionElementRow);

            return olapReport;
        }

        #endregion

        #region Exclude Report

        private OlapReport CreateExcludeReport()
        {
            OlapReport olapReport = new OlapReport
            {
                Name = "Subset Report",
                CurrentCubeName = "Adventure Works"
            };

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Customer";
            dimensionElementColumn.HierarchyName = "Customer Geography";
            dimensionElementColumn.AddLevel("Customer Geography", "Country");

            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Internet Sales Amount" });

            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Date";
            dimensionElementRow.AddLevel("Fiscal", "Fiscal Year");

            DimensionElement ExcludedDimension = new DimensionElement();
            ExcludedDimension.Name = "Date";
            ExcludedDimension.AddLevel("Fiscal", "Fiscal Year");
            ExcludedDimension.Hierarchy.LevelElements["Fiscal Year"].Add("FY 2005");
            ExcludedDimension.Hierarchy.LevelElements["Fiscal Year"].IncludeAvailableMembers = true;

            // Adding column members
            olapReport.CategoricalElements.Add(dimensionElementColumn);

            // Adding measure element
            olapReport.CategoricalElements.Add(measureElementColumn);

            // Adding row members
            olapReport.SeriesElements.Add(dimensionElementRow, ExcludedDimension);

            return olapReport;
        }

        #endregion

        #region Subset Report

        private OlapReport CreateSubsetReport()
        {
            OlapReport olapReport = new OlapReport
            {
                Name = "Subset Report",
                CurrentCubeName = "Adventure Works"
            };

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Customer";
            dimensionElementColumn.HierarchyName = "Customer Geography";
            dimensionElementColumn.AddLevel("Customer Geography", "Country");

            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Internet Sales Amount" });

            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Date";
            dimensionElementRow.AddLevel("Fiscal", "Fiscal Year");

            //Specifying the SubsetElement
            SubsetElement subSetElementColumn = new SubsetElement(5);
            subSetElementColumn.Name = "Top 5 Elements";

            SubsetElement subSetElementRow = new SubsetElement(3);
            subSetElementRow.Name = "Top 3 Elements";

            // Adding column members
            olapReport.CategoricalElements.Add(dimensionElementColumn);

            // Adding measure element
            olapReport.CategoricalElements.Add(measureElementColumn);
            olapReport.CategoricalElements.SubSetElement = subSetElementColumn;

            // Adding row members
            olapReport.SeriesElements.Add(dimensionElementRow);
            olapReport.SeriesElements.SubSetElement = subSetElementRow;


            return olapReport;
        }

        #endregion

        #region Named Set Report

        private OlapReport CreateNamedSetReport()
        {
            OlapReport olapReport = new OlapReport
            {
                Name = "Named Set Report",
                CurrentCubeName = "Adventure Works"
            };

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Customer";
            dimensionElementColumn.HierarchyName = "Customer Geography";
            dimensionElementColumn.AddLevel("Customer Geography", "Country");

            MeasureElements measureElementColumn = new MeasureElements();
            //Specifying the measure elements
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Internet Sales Amount" });

            //Specifying the named set element
            NamedSetElement dimensionElementRow = new NamedSetElement();
            dimensionElementRow.Name = "Core Product Group";

            // Adding the column members
            olapReport.CategoricalElements.Add(dimensionElementColumn);

            // Adding the measure elements
            olapReport.CategoricalElements.Add(measureElementColumn);

            // Adding the row members
            olapReport.SeriesElements.Add(dimensionElementRow);
            return olapReport;
        }

        #endregion

        #region Calculated Report

        private OlapReport CreateCalculatedReport()
        {
            OlapReport olapReport = new OlapReport
            {
                CurrentCubeName = "Adventure Works",
                Name = "Calculated Report"
            };

            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Customer";
            dimensionElementColumn.HierarchyName = "Customer Geography";
            dimensionElementColumn.AddLevel("Customer Geography", "Country");

            DimensionElement internalDimension = new DimensionElement();
            internalDimension.Name = "Product";
            internalDimension.AddLevel("Product Categories", "Category");

            // Calculated measure
            CalculatedMember calculatedMeasure1 = new CalculatedMember();
            calculatedMeasure1.Name = "Oder on Discount";
            calculatedMeasure1.Expression = "[Measures].[Order Quantity] + ([Measures].[Order Quantity] * 0.10)";
            calculatedMeasure1.AddElement(new MeasureElement { Name = "Order Quantity" });

            // Calculated measure
            CalculatedMember calculatedMeasure2 = new CalculatedMember();
            calculatedMeasure2.Name = "Sales Range";
            calculatedMeasure2.AddElement(new MeasureElement { Name = "Sales Amount" });
            calculatedMeasure2.Expression = "IIF([Measures].[Sales Amount]>200000,\"High\",\"Low\")";

            // Calculated dimension
            CalculatedMember calculateDimension = new CalculatedMember();
            calculateDimension.Name = "Bikes & Components";
            calculateDimension.Expression = "([Product].[Product Categories].[Category].[Bikes] + [Product].[Product Categories].[Category].[Components] )";
            calculateDimension.AddElement(internalDimension);

            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Sales Amount" });
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Order Quantity" });

            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Date";
            dimensionElementRow.AddLevel("Fiscal", "Fiscal Year");

            // Adding Calculated members in calculated member collection
            olapReport.CalculatedMembers.Add(calculatedMeasure1);
            olapReport.CalculatedMembers.Add(calculateDimension);
            olapReport.CalculatedMembers.Add(calculatedMeasure2);

            // Adding column members
            olapReport.CategoricalElements.Add(dimensionElementColumn);
            olapReport.CategoricalElements.Add(calculateDimension);

            // Adding measure element
            olapReport.CategoricalElements.Add(measureElementColumn);
            olapReport.CategoricalElements.Add(calculatedMeasure1);
            olapReport.CategoricalElements.Add(calculatedMeasure2);

            // Adding row members
            olapReport.SeriesElements.Add(dimensionElementRow);

            return olapReport;
        }

        #endregion

        #endregion

        #region Event Handlers

        private void PivotClient1_Loaded(object sender, RoutedEventArgs e)
        {
            SetDataManager();
        }

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

        private void ReportSelection_Click(object obj, RoutedEventArgs e)
        {
            var sender = obj as RadioButton;
            if (sender != null)
            {
                string m_content = sender.Content.ToString();
                switch (m_content)
                {
                    case "Report with Dimension and Measure":
                        olapDataManager.SetCurrentReport(CreateSimpleReport());
                        break;
                    case "Report with KPI":
                        olapDataManager.SetCurrentReport(CreateKPIReport());
                        break;
                    case "Report with Slicer":
                        olapDataManager.SetCurrentReport(CreateSlicingReport());
                        break;
                    case "Report with Filter":
                        olapDataManager.SetCurrentReport(CreateFilteringReport());
                        break;
                    case "Report with Order":
                        olapDataManager.SetCurrentReport(CreateSortingReport());
                        break;
                    case "Report with Top-Count":
                        olapDataManager.SetCurrentReport(CreateTopCountReport());
                        break;
                    case "Report with Exclude":
                        olapDataManager.SetCurrentReport(CreateExcludeReport());
                        break;
                    case "Report with Subset":
                        olapDataManager.SetCurrentReport(CreateSubsetReport());
                        break;
                    case "Report with Named Set":
                        olapDataManager.SetCurrentReport(CreateNamedSetReport());
                        break;
                    case "Report with Calculated Member":
                        olapDataManager.SetCurrentReport(CreateCalculatedReport());
                        break;
                }
            }
        }

        private void ShowLevelTypeAll_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                var checkBox = sender as CheckBox;
                olapDataManager.ShowLevelTypeAll = checkBox.IsChecked.HasValue && (bool)checkBox.IsChecked;
            }
        }

        private async void ShowMDXEditor_Click(object sender, RoutedEventArgs e)
        {
            if (PivotClient1.OlapDataManager != null && PivotClient1.OlapDataManager.CurrentReport != null)
            {
                MessageDialog dialog = new MessageDialog(PivotClient1.OlapDataManager.GetMdxQuery());
                await dialog.ShowAsync();
            }
        }

        #endregion
    }
}