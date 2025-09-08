#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotGrid
{
    using Windows.Globalization;
    using Common;
    using ViewModel = Tutorials.PivotGridSamples.ViewModel;

    public sealed partial class GettingStarted : SampleLayout
    {
        #region Private Fields

        /// <summary>
        /// Initialize the view model class for further usage.
        /// </summary>
        ViewModel.ViewModel vm = new ViewModel.ViewModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="GettingStarted">GettingStarted.</see>/>
        /// </summary>
        public GettingStarted()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            InitializeComponent();
            pivotGrid.OlapDataManager = vm.OlapDataManager;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>

        public sealed override void Dispose()
        {
            base.Dispose();
            if (pivotGrid != null)
                pivotGrid.Dispose();
            pivotGrid = null;
            if (vm != null)
            {
                vm.BusinessSalesData = null;
                vm.OlapDataManager = null;
                vm.Dispose();
            }
            vm = null;
            DataContext = null;
        }

        #endregion
    }
}
