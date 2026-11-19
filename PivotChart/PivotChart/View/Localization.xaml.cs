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
    using Windows.Globalization;
    using Windows.UI.Xaml;
    using Syncfusion.PivotAnalysis.UWP;

    public sealed partial class Localization : SampleLayout
    {
        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="Localization">Localization</see>/>
        /// </summary>
        public Localization()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "ar-AE";
            InitializeComponent();
            if (Application.Current != null)
            {
                Application.Current.Suspending += Current_Suspending;
            }

            // Adding ItemSource to the Control
            this.pivotChart.ItemSource = ProductSales.GetSalesData();
            this.pivotChart.ShowToolTip = true;
            // Adding PivotAxis to the Control
            this.pivotChart.PivotAxis.Add(new PivotItem { FieldMappingName = "Product", TotalHeader = "Total" });
            this.pivotChart.PivotAxis.Add(new PivotItem { FieldMappingName = "Country", TotalHeader = "Total" });
            this.pivotChart.PivotAxis.Add(new PivotItem { FieldMappingName = "State", TotalHeader = "Total" });

            // Adding PivotLegend to the Control
            this.pivotChart.PivotLegend.Add(new PivotItem { FieldMappingName = "Date", TotalHeader = "Total" });

            //Adding PivotCalculations to the Control
            this.pivotChart.PivotCalculations.Add(new PivotComputationInfo { FieldName = "Quantity", Format = "#.#" });
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            if (Application.Current != null)
            {
                Application.Current.Suspending -= Current_Suspending;
            }
            pivotChart?.Dispose();
            base.Dispose();
        }

        #endregion

        #region Event Handlers

        private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
        }

        #endregion
    }
}