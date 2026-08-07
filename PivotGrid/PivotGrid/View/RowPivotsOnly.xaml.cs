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
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml;
    using ViewModel = Tutorials.PivotGridSamples.ViewModel;
    using Syncfusion.UI.Xaml.PivotGrid;
    using Common;
    using System.Linq;

    public sealed partial class RowPivotsOnly : SampleLayout
    {
        #region Private Fields

        ViewModel.ViewModel vm = new ViewModel.ViewModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="GroupingBar">GroupingBar.</see>/>
        /// </summary>
        public RowPivotsOnly()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();
            PivotGrid1.ItemSource = vm.ProductSalesData;
            PivotGrid1.Loaded += (s, e) =>
            {
                PivotGrid1.RowHeaderStyle.EnableContextMenu = PivotGrid1.ColumnHeaderStyle.EnableContextMenu = PART_ContextMenu.IsChecked.Value;
                for (int i = 0; i < this.PivotGrid1.PivotEngine.PivotCalculations.Count; i++)
                    this.PivotGrid1.PivotEngine.PivotCalculations[i].AllowFilter = PART_Filtering.IsChecked.Value;
            };
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public sealed override void Dispose()
        {
            base.Dispose();
            if (PivotGrid1 != null)
            {
                if (PivotGrid1.RowHeaderStyle != null)
                    PivotGrid1.RowHeaderStyle.EnableContextMenu = false;
                if (PivotGrid1.ColumnHeaderStyle != null)
                    PivotGrid1.ColumnHeaderStyle.EnableContextMenu = false;
                PivotGrid1.Dispose();
            }
            PivotGrid1 = null;
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

        #region Event Handlers

        private void PART_Filtering_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.Value)
            {
                for (int i = 0; i < this.PivotGrid1.PivotEngine.PivotCalculations.Count; i++)
                    this.PivotGrid1.PivotEngine.PivotCalculations[i].AllowFilter = true;
            }
            else
            {
                for (int i = 0; i < this.PivotGrid1.PivotEngine.PivotCalculations.Count; i++)
                    this.PivotGrid1.PivotEngine.PivotCalculations[i].AllowFilter = false;
            }
            this.PivotGrid1.InternalGrid.InvalidateCells();
        }

        private void PART_Sorting_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.Value)
            {
                this.PivotGrid1.SortOption = SortOptions.All;
                for (int i = 0; i < this.PivotGrid1.PivotEngine.PivotCalculations.Count; i++)
                    this.PivotGrid1.PivotEngine.PivotCalculations[i].AllowSort = true;
            }
            else
            {
                this.PivotGrid1.SortOption = SortOptions.None;
                for (int i = 0; i < this.PivotGrid1.PivotEngine.PivotCalculations.Count; i++)
                {
                    this.PivotGrid1.PivotEngine.PivotCalculations[i].AllowSort = false;
                }
                this.PivotGrid1.PivotEngine.sortColIndexes.Clear();
            }
            this.PivotGrid1.InternalGrid.InvalidateCells();
        }

        private void PART_HideSummary_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.Value)
            {
                for (int i = 0; i < this.PivotGrid1.PivotEngine.PivotCalculations.Count; i++)
                    this.PivotGrid1.PivotEngine.PivotCalculations[i].InnerMostComputationsOnly = Syncfusion.PivotAnalysis.UWP.SummaryDisplayLevel.InnerMostOnly;
            }
            else
            {
                for (int i = 0; i < this.PivotGrid1.PivotEngine.PivotCalculations.Count; i++)
                    this.PivotGrid1.PivotEngine.PivotCalculations[i].InnerMostComputationsOnly = Syncfusion.PivotAnalysis.UWP.SummaryDisplayLevel.All;
            }
            this.PivotGrid1.InternalGrid.InvalidateCells();
        }

        private void PART_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.Value)
            {
                this.PivotGrid1.EnableHyperlinkOnMouseOver = true;
                for (int i = 0; i < this.PivotGrid1.PivotEngine.PivotCalculations.Count; i++)
                    this.PivotGrid1.PivotEngine.PivotCalculations[i].EnableHyperlinks = true;
            }
            else
            {
                this.PivotGrid1.EnableHyperlinkOnMouseOver = false;
                for (int i = 0; i < this.PivotGrid1.PivotEngine.PivotCalculations.Count; i++)
                    this.PivotGrid1.PivotEngine.PivotCalculations[i].EnableHyperlinks = false;
            }
            this.PivotGrid1.InternalGrid.InvalidateCells();
        }

        private void PART_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.Value)
            {
                this.PivotGrid1.RowHeaderStyle.EnableContextMenu = true;
                this.PivotGrid1.ColumnHeaderStyle.EnableContextMenu = true;
            }
            else
            {
                this.PivotGrid1.RowHeaderStyle.EnableContextMenu = false;
                this.PivotGrid1.ColumnHeaderStyle.EnableContextMenu = false;
            }
            this.PivotGrid1.InternalGrid.InvalidateCells();
        }        

        private void PART_PadString_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.Value)
            {
                for (int i = 0; i < this.PivotGrid1.PivotCalculations.Count; i++)
                {
                    if (this.PivotGrid1.PivotCalculations[i].PadString != "*")
                        this.PivotGrid1.InternalGrid.ColumnWidths.SetHidden((i + PivotGrid1.PivotRows.Count), (i + PivotGrid1.PivotRows.Count), false);
                    else
                        this.PivotGrid1.InternalGrid.ColumnWidths.SetHidden((i + PivotGrid1.PivotRows.Count), (i + PivotGrid1.PivotRows.Count), true);
                }
            }
            else
            {
                for (int i = 0; i < this.PivotGrid1.PivotCalculations.Count; i++)
                {
                    if (this.PivotGrid1.PivotCalculations[i].PadString != "*")
                        this.PivotGrid1.InternalGrid.ColumnWidths.SetHidden((i + PivotGrid1.PivotRows.Count), (i + PivotGrid1.PivotRows.Count), true);
                    else
                        this.PivotGrid1.InternalGrid.ColumnWidths.SetHidden((i + PivotGrid1.PivotRows.Count), (i + PivotGrid1.PivotRows.Count), false);
                }
            }
            this.PivotGrid1.InternalGrid.InvalidateCells();
        }

        #endregion
    }
}