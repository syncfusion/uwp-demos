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

    public sealed partial class Relational : SampleLayout
    {
        #region Private Fields

        /// <summary>
        /// Initialize the view model class for further usage.
        /// </summary>
        ViewModel.ViewModel vm = new ViewModel.ViewModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="Relational">Relational.</see>/>
        /// </summary>
        public Relational()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();
            this.pivotGrid1.ItemSource = vm.ProductSalesData;
            this.pivotGrid1.RowHeaderStyle.IsHyperlinkCell = this.pivotGrid1.ColumnHeaderStyle.IsHyperlinkCell = this.pivotGrid1.SummaryColumnStyle.IsHyperlinkCell = this.pivotGrid1.SummaryRowStyle.IsHyperlinkCell = this.pivotGrid1.ValueCellStyle.IsHyperlinkCell = false;
            if (DeviceFamily.GetDeviceFamily() != Devices.Desktop)
            {
                if (pivotGrid1.ConditionalFormats != null && pivotGrid1.ConditionalFormats.Count> 0)
                    pivotGrid1.ConditionalFormats.Clear();
            }
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public sealed override void Dispose()
        {
            base.Dispose();
            if (pivotGrid1 != null)
                pivotGrid1.Dispose();
            pivotGrid1 = null;
            if (vm != null)
            {
                vm.ProductSalesData = null;
                vm.BusinessSalesData = null;
                vm.OlapDataManager = null;
                vm.Dispose();
            }
            vm = null;
            DataContext = null;
        }

        #endregion

        /// <summary>
        /// Invoked when the grid layout is changed.
        /// </summary><
        private void cmbLayout_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString() == "Normal")
                pivotGrid1.GridLayout = Syncfusion.PivotAnalysis.UWP.GridLayout.Normal;
            else if (((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString() == "Top Summary")
                pivotGrid1.GridLayout = Syncfusion.PivotAnalysis.UWP.GridLayout.TopSummary;
        }
    }
}
