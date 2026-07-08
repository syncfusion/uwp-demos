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
    using Syncfusion.PivotAnalysis.UWP;
    using Windows.Globalization;
    using Windows.UI.Xaml.Controls;
    using ViewModel = Tutorials.PivotGridSamples.ViewModel;

    public sealed partial class Drill_Through : SampleLayout
    {
        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="Drill_Through">Dril-Through.</see>/>
        /// </summary>
        public Drill_Through()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            InitializeComponent();
            DataContext = new ViewModel.ViewModel();
            pivotGrid1.Loaded += PivotGrid1_Loaded;
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
            {
                pivotGrid1.Loaded -= PivotGrid1_Loaded;
                pivotGrid1.Dispose();
            }
            pivotGrid1 = null;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// This event handler method is invoked when the SfPviotGrid was loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PivotGrid1_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            pivotGrid1.InternalGrid.CellClick += InternalGrid_CellClick;
        }

        /// <summary>
        /// This event handler method is hooked when the particular cell was clicked. 
        /// </summary>
        /// <param name="sender">The current cell.</param>
        /// <param name="args">The event argument.</param>
        private void InternalGrid_CellClick(object sender, Syncfusion.UI.Xaml.CellGrid.Helpers.GridCellClickEventArgs args)
        {
            txtBox.Text = "";
            lstSelectedItems.ItemsSource = null;
            PivotCellInfo cellInfo = pivotGrid1.PivotEngine[args.RowIndex, args.ColumnIndex];
            if (cellInfo.CellType.ToString().Contains("ValueCell"))
            {
                var itemSource = pivotGrid1.PivotEngine.GetRawItemsFor(args.RowIndex, args.ColumnIndex);
                txtBox.Text = itemSource.Count.ToString();
                lstSelectedItems.ItemsSource = itemSource;
            }
        }

        /// <summary>
        /// This event handler method is invoked when clicking the check box. 
        /// </summary>
        /// <param name="sender">The check box.</param>
        /// <param name="e">The event argument.</param>

        private void ChkBox1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            pivotGrid1.PivotEngine.ShowGrandTotals = (sender as CheckBox).IsChecked.Value;
            pivotGrid1.Refresh();
        }

        #endregion
    }
}
