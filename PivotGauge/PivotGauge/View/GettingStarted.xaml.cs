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
    using ViewModel = Syncfusion.SampleBrowser.UWP.PivotGauge.PivotGauge.ViewModel;
    public sealed partial class GettingStarted : SampleLayout
    {

        #region Constructor
        /// <summary>
        /// Initializes the new instance of <see cref="GettingStarted">GettingStarted.</see>/>
        /// </summary>
        public GettingStarted()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();
            DataContext = new ViewModel.ViewModel();
        }

        #endregion

        #region IDispose Methods

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