#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotGrid
{
    using Common;
    using System;
    using Windows.Globalization;
    using Windows.UI.Xaml.Controls;
    using ViewModel = Tutorials.PivotGridSamples.ViewModel;

    public sealed partial class Updating : SampleLayout
    {
        #region Private members

        ViewModel.UpdateViewModel vm = new ViewModel.UpdateViewModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="Updating">Updating.</see>/>
        /// </summary>
        public Updating()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();
            this.DataContext = vm;
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
            if (vm != null)
                vm.Dispose();
            vm = null;
            DataContext = null;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// This event handler method is invoked when selecting the combo box item.
        /// </summary>
        /// <param name="sender">The combo box.</param>
        /// <param name="e">The event argument.</param>
        private void cmbUpdateRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.pivotGrid.UpdateManager.ThrottleUpdateRate = Convert.ToInt32(((sender as ComboBox).SelectedValue as ComboBoxItem).Content);
        }

        #endregion
    }
}
