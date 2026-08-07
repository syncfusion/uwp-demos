#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotGrid
{
    using System;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Windows.Globalization;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using ViewModel = Tutorials.PivotGridSamples.ViewModel;
    using Syncfusion.PivotAnalysis.UWP;
    using Syncfusion.UI.Xaml.PivotGrid;
    using Common;
    using Windows.UI.Xaml.Controls;

    public sealed partial class GroupingBar : SampleLayout
    {
        #region Private Fields

        /// <summary>
        /// Initializes the view model class for further usage.
        /// </summary>
        private ViewModel.ViewModel vm = new ViewModel.ViewModel();

        /// <summary>
        /// Initializes the Pivot Grid control.
        /// </summary>
        private SfPivotGrid pivotGrid;

        private Binding showGroupingBarBinding;

        private Binding enableGroupingBarFilteringBinding;

        private Binding enableGroupingBarRemovingBinding;

        private Binding enableGroupingBarSortingBinding;

        private Binding enableRowHeaderBinding;

        private Binding enableColumnHeaderBinding;

        private Binding enableValueHeaderBinding;

        private Binding enableFilterHeaderBinding;

        private Binding allowMultiFunctionalSortFilterBinding;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="GroupingBar">GroupingBar.</see>/>
        /// </summary>
        public GroupingBar()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();
            showGroupingBarBinding = new Binding
            {
                Path = new PropertyPath("IsChecked"),
                ElementName = "chk_ShowGroupingBar",
                Mode = BindingMode.TwoWay
            };
            enableGroupingBarFilteringBinding = new Binding
            {
                Path = new PropertyPath("IsChecked"),
                ElementName = "chk_EnableGroupingBarFiltering",
                Mode = BindingMode.TwoWay
            };
            enableGroupingBarRemovingBinding = new Binding
            {
                Path = new PropertyPath("IsChecked"),
                ElementName = "chk_EnableGroupingBarRemoving",
                Mode = BindingMode.TwoWay
            };
            enableGroupingBarSortingBinding = new Binding
            {
                Path = new PropertyPath("IsChecked"),
                ElementName = "chk_EnableGroupingBarSorting",
                Mode = BindingMode.TwoWay
            };
            enableRowHeaderBinding = new Binding
            {
                Path = new PropertyPath("IsChecked"),
                ElementName = "chkBox_EnableRowHeader",
                Mode = BindingMode.TwoWay
            };

            enableColumnHeaderBinding = new Binding
            {
                Path = new PropertyPath("IsChecked"),
                ElementName = "chkBox_EnableColumnHeader",
                Mode = BindingMode.TwoWay
            };
            enableValueHeaderBinding = new Binding
            {
                Path = new PropertyPath("IsChecked"),
                ElementName = "chkBox_EnableValueHeader",
                Mode = BindingMode.TwoWay
            };
            enableFilterHeaderBinding = new Binding
            {
                Path = new PropertyPath("IsChecked"),
                ElementName = "chkBox_EnableFilterHeader",
                Mode = BindingMode.TwoWay
            };
            allowMultiFunctionalSortFilterBinding = new Binding
            {
                Path = new PropertyPath("IsChecked"),
                ElementName = "chk_AllowMultiFunctionalSortFilter",
                Mode = BindingMode.TwoWay
            };
            PivotGrid1.ItemSource = vm.ProductSalesData;
            // Adding rows to SfPivotGrid

            PivotGrid1.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Product", TotalHeader = "Total" });
            PivotGrid1.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Date", TotalHeader = "Total" });

            // Adding columns to SfPivotGrid
            PivotGrid1.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Country", TotalHeader = "Total" });
            PivotGrid1.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "State", TotalHeader = "Total" });

            // Adding calculations to SfPivotGrid

            PivotGrid1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Amount", FieldName = "Amount", Format = "C", SummaryType = SummaryType.DoubleTotalSum });
            PivotGrid1.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Quantity", FieldName = "Quantity", Format = "#.##", SummaryType = SummaryType.Count });

            PivotGrid1.SetBinding(SfPivotGrid.ShowGroupingBarProperty, showGroupingBarBinding);
            PivotGrid1.SetBinding(SfPivotGrid.EnableGroupingBarFilteringProperty, enableGroupingBarFilteringBinding);
            PivotGrid1.SetBinding(SfPivotGrid.EnableGroupingBarRemovingProperty, enableGroupingBarRemovingBinding);
            PivotGrid1.SetBinding(SfPivotGrid.EnableGroupingBarSortingProperty, enableGroupingBarSortingBinding);
            PivotGrid1.SetBinding(SfPivotGrid.EnableRowHeaderAreaProperty, enableRowHeaderBinding);
            PivotGrid1.SetBinding(SfPivotGrid.EnableColumnHeaderAreaProperty, enableColumnHeaderBinding);
            PivotGrid1.SetBinding(SfPivotGrid.EnableFilterHeaderAreaProperty, enableFilterHeaderBinding);
            PivotGrid1.SetBinding(SfPivotGrid.EnableValueHeaderAreaProperty, enableValueHeaderBinding);
            PivotGrid1.SetBinding(SfPivotGrid.AllowMultiFunctionalSortFilterProperty, allowMultiFunctionalSortFilterBinding);
        }

        #endregion

        #region IDisposable Method

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public sealed override void Dispose()
        {
            base.Dispose();
            if (PivotGrid1 != null)
                PivotGrid1.Dispose();
            if (pivotGrid != null)
                pivotGrid.Dispose();
            showGroupingBarBinding = null;
            vm.Dispose();
            DataContext = null;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// The event handler method is hooked when the radio button was clicked.
        /// </summary>
        /// <param name="sender">The radio button.</param>
        /// <param name="e">The event argument.</param>
        private void OlapDataSource_Checked(object sender, RoutedEventArgs e)
        {
            RootLayout.Children.RemoveAt(0);
            pivotGrid = new SfPivotGrid { Margin = new Thickness(5) };
            pivotGrid.SetBinding(SfPivotGrid.ShowGroupingBarProperty, showGroupingBarBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableGroupingBarFilteringProperty, enableGroupingBarFilteringBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableGroupingBarRemovingProperty, enableGroupingBarRemovingBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableRowHeaderAreaProperty, enableRowHeaderBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableColumnHeaderAreaProperty, enableColumnHeaderBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableFilterHeaderAreaProperty, enableFilterHeaderBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableValueHeaderAreaProperty, enableValueHeaderBinding);
            chk_EnableGroupingBarSorting.Visibility = Visibility.Collapsed;
            pivotGrid.SetBinding(SfPivotGrid.AllowMultiFunctionalSortFilterProperty, allowMultiFunctionalSortFilterBinding);
            chk_AllowMultiFunctionalSortFilter.Visibility = Visibility.Collapsed;
            busyIndicator.IsBusy = true;
            Task.Run(async () =>
            {
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                var taskCompletionSource = new TaskCompletionSource<bool>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    pivotGrid.OlapDataManager = vm.OlapDataManager;
                    RootLayout.Children.Insert(0, pivotGrid);
                    taskCompletionSource.SetResult(true);
                    busyIndicator.IsBusy = false;
                });
                await taskCompletionSource.Task;
            });
        }

        /// <summary>
        /// The event handler method is hooked when the radio button was clicked.
        /// </summary>
        /// <param name="sender">The radio button.</param>
        /// <param name="e">The event argument.</param>
        private void RelationalDataSource_Checked(object sender, RoutedEventArgs e)
        {
            RootLayout.Children.RemoveAt(0);
            pivotGrid = new SfPivotGrid { Margin = new Thickness(5) };
            if (DeviceFamily.GetDeviceFamily() == Devices.Mobile && pivotGrid.ConditionalFormats != null)
                pivotGrid.ConditionalFormats.Clear();
            pivotGrid.SetBinding(SfPivotGrid.ShowGroupingBarProperty, showGroupingBarBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableGroupingBarFilteringProperty, enableGroupingBarFilteringBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableGroupingBarRemovingProperty, enableGroupingBarRemovingBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableGroupingBarSortingProperty, enableGroupingBarSortingBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableRowHeaderAreaProperty, enableRowHeaderBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableColumnHeaderAreaProperty, enableColumnHeaderBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableFilterHeaderAreaProperty, enableFilterHeaderBinding);
            pivotGrid.SetBinding(SfPivotGrid.EnableValueHeaderAreaProperty, enableValueHeaderBinding);
            chk_EnableGroupingBarSorting.Visibility = Visibility.Visible;
            pivotGrid.SetBinding(SfPivotGrid.AllowMultiFunctionalSortFilterProperty, allowMultiFunctionalSortFilterBinding);
            chk_AllowMultiFunctionalSortFilter.Visibility = Visibility.Visible;
            busyIndicator.IsBusy = true;
            Task.Run(async () =>
            {
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                var taskCompletionSource = new TaskCompletionSource<bool>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    pivotGrid.ItemSource = vm.ProductSalesData;
                    // Adding rows to SfPivotGrid

                    pivotGrid.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Product", TotalHeader = "Total" });
                    pivotGrid.PivotRows.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Date", TotalHeader = "Total" });

                    // Adding columns to SfPivotGrid
                    pivotGrid.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "Country", TotalHeader = "Total" });
                    pivotGrid.PivotColumns.Add(new Syncfusion.PivotAnalysis.UWP.PivotItem() { FieldMappingName = "State", TotalHeader = "Total" });

                    // Adding calculations to SfPivotGrid

                    pivotGrid.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Amount", FieldName = "Amount", Format = "C", SummaryType = SummaryType.DoubleTotalSum });
                    pivotGrid.PivotCalculations.Add(new PivotComputationInfo() { FieldHeader = "Quantity", FieldName = "Quantity", Format = "#.##", SummaryType = SummaryType.Count });

                    RootLayout.Children.Insert(0, pivotGrid);
                    taskCompletionSource.SetResult(true);
                    busyIndicator.IsBusy = false;
                });
                await taskCompletionSource.Task;
            });
        }

        #endregion
        private void chk_ShowGroupingBar_Click(object sender, RoutedEventArgs e)
        {
            CheckBox current = e.OriginalSource as CheckBox;
            SfPivotGrid pivotGrid = this.PivotGrid1;
            CheckBox showGroupingBar = this.chk_ShowGroupingBar;
            CheckBox enableRowHeaderArea = this.chkBox_EnableRowHeader;
            CheckBox enableColumnHeaderArea = this.chkBox_EnableColumnHeader;
            CheckBox enableFilterHeaderArea = this.chkBox_EnableFilterHeader;
            CheckBox enableValueHeaderArea = this.chkBox_EnableValueHeader;

            if (enableRowHeaderArea.IsChecked == false && enableColumnHeaderArea.IsChecked == false && enableFilterHeaderArea.IsChecked == false && enableValueHeaderArea.IsChecked == false)
                showGroupingBar.IsChecked = false;
        }
        private void chkBox_EnableRowHeader_Click(object sender, RoutedEventArgs e)
        {
            chk_ShowGroupingBar_Click(sender, e);
        }

        private void chkBox_EnableColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            chk_ShowGroupingBar_Click(sender, e);
        }

        private void chkBox_EnableValueHeader_Click(object sender, RoutedEventArgs e)
        {
            chk_ShowGroupingBar_Click(sender, e);
        }

        private void chkBox_EnableFilterHeader_Click(object sender, RoutedEventArgs e)
        {
            chk_ShowGroupingBar_Click(sender, e);
        }
    }
}
