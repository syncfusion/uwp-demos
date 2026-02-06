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
    using ViewModel = Tutorials.PivotGridSamples.ViewModel;
    using Windows.Globalization;
    using Windows.UI.Xaml.Controls;

    public sealed partial class Editing : SampleLayout
    {
        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="Editing">Editing.</see>/>
        /// </summary>
        public Editing()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();
            this.DataContext = new ViewModel.ViewModel();
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
            {
                pivotGrid.EditManager.Dispose();
                pivotGrid.Dispose();
            }
            pivotGrid = null;
            if (chkEnableEdit != null)
                chkEnableEdit.Click -= chkEnableEdit_Click;
            chkEnableEdit = null;
            if (rdBtnBuiltIn != null)
                rdBtnBuiltIn.Click -= rdBtnBuiltIn_Click;
            rdBtnBuiltIn = null;
            if (rdBtnCustom != null)
                rdBtnCustom.Click -= rdBtnCustom_Click;
            rdBtnCustom = null;
            if (chkHideExpanders != null)
                chkHideExpanders.Click -= chkHideExpanders_Click;
            chkHideExpanders = null;
            if (chkEnableEditTotal != null)
                chkEnableEditTotal.Click -= chkEnableEditTotal_Click;
            chkEnableEditTotal = null;
            DataContext = null;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// The event handler is invoked when clicking the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkEnableEditTotal_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if ((sender as CheckBox)?.IsChecked != null)
                this.pivotGrid.EditManager.AllowEditingOfTotalCells = (sender as CheckBox).IsChecked.Value;
            this.pivotGrid.Refresh();
        }

        /// <summary>
        /// The event handler is invoked when clicking the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkHideExpanders_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if ((sender as CheckBox)?.IsChecked != null)
                this.pivotGrid.EditManager.HideExpanders = (sender as CheckBox).IsChecked.Value;
            this.pivotGrid.Refresh();
        }

        /// <summary>
        /// The event handler is invoked when clicking the button.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">The event argument.</param>
        private void rdBtnCustom_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.pivotGrid.EditManager.Dispose(); //dispose the current one...
            this.pivotGrid.EditManager = new ViewModel.CustomEditManager(this.pivotGrid) { HideExpanders = this.pivotGrid.EditManager.HideExpanders }; //set the derived one...
        }

        /// <summary>
        /// The event handler is invoked when clicking the button.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">The event argument.</param>
        private void rdBtnBuiltIn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.pivotGrid.EditManager.Dispose(); //dispose the current one...
            this.pivotGrid.EditManager = new Syncfusion.UI.Xaml.PivotGrid.PivotEditingManager(this.pivotGrid) { HideExpanders = this.pivotGrid.EditManager.HideExpanders }; //set the derived one...
        }

        /// <summary>
        /// The event handler is invoked when clicking the check box.
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>
        private void chkEnableEdit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked == true)
                this.pivotGrid.EnableValueEditing = true;
            else
            {
                this.pivotGrid.EnableValueEditing = false;
            }
        }

        #endregion
    }
}
