#region Copyright Syncfusion Inc. 2001 - 2016
// Copyright Syncfusion Inc. 2001 - 2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotGauge
{
    using Windows.Globalization;
    using Common;
    using Syncfusion.SampleBrowser.UWP.PivotGauge.PivotGauge.Model;
    using Syncfusion.PivotAnalysis.UWP;
    public sealed partial class Relational : SampleLayout
    {
        #region Constructor
        /// <summary>
        /// Initializes the new instance of <see cref="Relational">Relational.</see>/>
        /// </summary>
        public Relational()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";

            InitializeComponent();

            PivotGauge1.ItemSource = ProductSales.GetSalesData();

            // Adding PivotRows to the Control
            PivotGauge1.PivotRows.Add(new PivotItem { FieldMappingName = "Product", TotalHeader = "Total" });
            PivotGauge1.PivotRows.Add(new PivotItem { FieldMappingName = "Date", TotalHeader = "Total" });
            // Adding PivotColumns to the Control

            PivotGauge1.PivotColumns.Add(new PivotItem { FieldMappingName = "Country", TotalHeader = "Total" });
            PivotGauge1.PivotColumns.Add(new PivotItem { FieldMappingName = "State", TotalHeader = "Total" });

            // Adding PivotCalculations to the Control
            PivotGauge1.PivotCalculations.Add(new PivotComputationInfo { FieldName = "Amount", Format = "C", SummaryType = SummaryType.DoubleTotalSum });
            PivotGauge1.PivotCalculations.Add(new PivotComputationInfo { FieldName = "Quantity", Format = "#,##0" });
            PivotGauge1.PivotCalculations.Add(new PivotComputationInfo { FieldName = "UnitPrice", Format = "C", SummaryType = SummaryType.DoubleTotalSum });
            PivotGauge1.PivotCalculations.Add(new PivotComputationInfo { FieldName = "TotalPrice", Format = "C", SummaryType = SummaryType.DoubleTotalSum });
        }

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects
        /// </summary>

        public sealed override void Dispose()
        {
            base.Dispose();
            PivotGauge1.Dispose();
            DataContext = null;
        }
        #endregion
    }
}