using Common;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DataGrid
{
    public sealed partial class ColumnChooserDemo : SampleLayout
    {
        ColumnChooser chooserWindow;
        OrderInfoViewModel viewModel;
        public ColumnChooserDemo()
        {
            this.InitializeComponent();
            this.DataContext = viewModel = new OrderInfoViewModel();
            this.sfGrid.Loaded += OnSfDataGridLoaded;
            this.sfGrid.Unloaded += OnSfDataGridUnloaded;
        }

        /// <summary>
        /// Occurs when the SfDataGrid is laid out, rendered, and ready for interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSfDataGridLoaded(object sender, RoutedEventArgs e)
        {
            chooserWindow = new ColumnChooser(this.sfGrid);
            chooserWindow.Resources.MergedDictionaries.Clear();
            this.sfGrid.GridColumnDragDropController = new GridColumnChooserController(this.sfGrid, chooserWindow);
            ShowColumnChooser();
            viewModel.toggled = OnToggled;
            viewModel.ColumnChooserCommand = new DelegateCommand<object>(ColumnChooserCommandHandler);
            chooserWindow.Popup.Closed += OnPopup_Closed;
        }

        /// <summary>
        /// Occurs when the SfDataGrid is removed from within an element tree of loaded elements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSfDataGridUnloaded(object sender, RoutedEventArgs e)
        {
            chooserWindow.Popup.IsOpen = false;
            viewModel.CustomChooser.IsOpen = false;
        }

        /// <summary>
        /// Column Chooser Command Handler
        /// </summary>
        /// <param name="param"></param>
        public void ColumnChooserCommandHandler(object param)
        {
            if (param == null)
                return;
            else if (param.ToString().Equals("ShowColumnChooser"))
            {
                if (viewModel.ShowColumnChooser)
                    ShowColumnChooser();
                else
                {
                    chooserWindow.Popup.IsOpen = false;
                    viewModel.CustomChooser.IsOpen = false;
                }
            }
            else
            {
                chooserWindow.Popup.IsOpen = false;
                viewModel.CustomChooser.IsOpen = false;
                viewModel.ShowColumnChooser = false;
            }

        }

        /// <summary>
        /// Shows the ColumnChosser
        /// </summary>
        void ShowColumnChooser()
        {
            if (viewModel.ShowColumnChooser && viewModel.UseDefaultColumnChooser)
            {
                chooserWindow.Popup.HorizontalOffset = (this.sfGrid.ActualWidth) / 2 + (200);
                chooserWindow.Popup.VerticalOffset = (this.sfGrid.ActualHeight) / 2 - (100);
                chooserWindow.Popup.IsOpen = true;
                viewModel.CustomChooser.IsOpen = false;
            }
            else if (viewModel.ShowColumnChooser && viewModel.UseCustomColumnChooser)
            {
                var visibleColumns = this.sfGrid.Columns;
                ObservableCollection<ColumnChooserItems> totalColumns = GetColumnsDetails(visibleColumns);
                CustomColumnChooserViewModel chooserViewModel = new CustomColumnChooserViewModel(totalColumns);
                CustomColumnChooser ColumnChooserView = new CustomColumnChooser(this.sfGrid, chooserViewModel, totalColumns);
                viewModel.CustomChooser.Child = ColumnChooserView;
                chooserWindow.Popup.IsOpen = false;
                viewModel.CustomChooser.HorizontalOffset = (this.sfGrid.ActualWidth / 2) - (75) + (200);
                viewModel.CustomChooser.VerticalOffset = (this.sfGrid.ActualHeight / 2) - (75)- (100);
                viewModel.CustomChooser.IsOpen = true;
                viewModel.CustomChooser.Closed += OnCustomChooser_Closed;
            }
        }

        /// <summary>
        /// Occurs when CustomColumnChosser is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnCustomChooser_Closed(object sender, object e)
        {
            viewModel.ShowColumnChooser = false;
        }

        /// <summary>
        /// Show the Toggled button
        /// </summary>
        private void OnToggled()
        {
            if (viewModel.ShowColumnChooser)
                ShowColumnChooser();
            else
            {
                chooserWindow.Popup.IsOpen = false;
                viewModel.CustomChooser.IsOpen = false;
            }
        }

        /// <summary>
        /// Occurs when the popup is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPopup_Closed(object sender, object e)
        {
            viewModel.ShowColumnChooser = false;
        }

        /// <summary>
        /// Get the column details
        /// </summary>
        /// <param name="totalVisibleColumns"></param>
        /// <returns></returns>
        public static ObservableCollection<ColumnChooserItems> GetColumnsDetails(Columns totalVisibleColumns)
        {
            ObservableCollection<ColumnChooserItems> totalColumns = new ObservableCollection<ColumnChooserItems>();
            foreach (var actualColumn in totalVisibleColumns)
            {
                ColumnChooserItems item = new ColumnChooserItems();
                if (actualColumn.IsHidden)
                {
                    item.IsChecked = false;
                    item.Name = actualColumn.MappingName;
                }
                else
                {
                    item.IsChecked = true;
                    item.Name = actualColumn.MappingName;
                }
                totalColumns.Add(item);
            }
            return totalColumns;
        }

        /// <summary>
        ///  Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            chooserWindow.Popup.IsOpen = false;
            viewModel.CustomChooser.IsOpen = false;
            this.sfGrid.Loaded -= OnSfDataGridLoaded;
            this.sfGrid.Unloaded -= OnSfDataGridUnloaded;
            if (viewModel != null && viewModel.CustomChooser != null)
                viewModel.CustomChooser.Closed -= OnCustomChooser_Closed;
            if (chooserWindow != null && chooserWindow.Popup != null)
                chooserWindow.Popup.Closed -= OnPopup_Closed;
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
