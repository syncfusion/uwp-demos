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

    public sealed partial class GettingStarted : SampleLayout
    {
        #region Private Members

        ViewModel viewModel = new ViewModel();

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the new instance of <see cref="GettingStarted">GettingStarted.</see>/>
        /// </summary>
        public GettingStarted()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            InitializeComponent();
            DataContext = this.viewModel;
        }

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public sealed override void Dispose()
        {
            this.PivotChart?.Dispose();
            this.PivotChart = null;
            this.viewModel?.Dispose();
            this.viewModel = null;
            this.DataContext = null;
            base.Dispose();
        }
        #endregion
    }
}