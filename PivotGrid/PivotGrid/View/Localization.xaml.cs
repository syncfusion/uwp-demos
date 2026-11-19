#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotGrid
{
    using ViewModel = Tutorials.PivotGridSamples.ViewModel;
    using Windows.Globalization;
    using Common;
    using Windows.UI.Xaml;

    public sealed partial class Localization : SampleLayout
    {
        #region Private Fields

        /// <summary>
        /// Initialize the view model class for further usage.
        /// </summary>
        ViewModel.ViewModel vm = new ViewModel.ViewModel();

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the new instance of <see cref="Localization">Localization.</see>/>
        /// </summary>
        public Localization()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "ar-AE";
            InitializeComponent();
            if (Application.Current != null)
            {
                Application.Current.Suspending += Current_Suspending;
            }
            pivotGrid1.ItemSource = vm.ProductSalesData;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public sealed override void Dispose()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            if (Application.Current != null)
            {
                Application.Current.Suspending -= Current_Suspending;
            }
            base.Dispose();
            pivotGrid1.Dispose();
            vm.ProductSalesData = null;
            vm.Dispose();
            vm = null;
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
