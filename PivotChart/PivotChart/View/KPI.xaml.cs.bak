#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotChart
{
    using Windows.Globalization;
    using Common;

    /// <summary>
    /// This class represent the KPI report 
    /// </summary>
    public sealed partial class KPI : SampleLayout
    {
        #region Private Members

        ViewModelKPI kpiViewModel = new ViewModelKPI();

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the new instance of <see cref="KPI">KPI></see>/>
        /// </summary>
        public KPI()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            InitializeComponent();
            DataContext = this.kpiViewModel;
        }

        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.PivotChart?.Dispose();
            this.PivotChart = null;
            this.kpiViewModel?.Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}