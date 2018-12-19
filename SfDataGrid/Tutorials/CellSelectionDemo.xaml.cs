using Common;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DataGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CellSelectionDemo : SampleLayout
    {
     
        public CellSelectionDemo()
        {
            this.InitializeComponent();
            var datacontext = new ProductInfoViewModel();
            this.dataGrid.SelectionController = new CellSelectionController(this.dataGrid);
            this.DataContext = datacontext;
            this.dataGrid.ItemsSource = datacontext.ProductList;
            this.dataGrid.SelectionChanged += DataGrid_SelectionChanged;
            this.comboBox.SelectionChanged += ComboBox_SelectionChanged;       
            (this.dataGrid.SelectionController as CellSelectionController).FilterApplied += DataGrid_FilterApplied;
        }      

        /// <summary>
        /// Occurs when the selection is changed in SfDataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            this.UpdateStatusBar();
        }
        
        private void DataGrid_FilterApplied(object sender, EventArgs e)
        {
            this.UpdateStatusBar();
        }

        private void ComboBox_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            switch(this.comboBox.SelectedIndex)
            {
                case 0:
                    this.dataGrid.SelectionMode = GridSelectionMode.Single;
                    break;
                case 1:
                    this.dataGrid.SelectionMode = GridSelectionMode.Multiple;
                    break;
                case 2:
                    this.dataGrid.SelectionMode = GridSelectionMode.Extended;
                    break;
                case 3:
                    this.dataGrid.SelectionMode = GridSelectionMode.None;
                    break;
                default:
                    this.dataGrid.SelectionMode = GridSelectionMode.Single;
                    break;
            }
            this.UpdateStatusBar();
        }

        public void UpdateStatusBar()
        {
            var viewModel = this.dataGrid.DataContext as ProductInfoViewModel;
            var SelectedCells = dataGrid.GetSelectedCells();
            var propertyCollection = dataGrid.View.GetPropertyAccessProvider();
            if (SelectedCells.Count > 0)
            {
                double sumValue = 0;
                int numCount = 0;
                List<double> cellValue = new List<double>();
                foreach (var cellInfo in SelectedCells)
                {
                    if (cellInfo.IsDataRowCell)
                    {
                        var value = propertyCollection.GetValue(cellInfo.RowData, cellInfo.Column.MappingName);
                        if (value is double)
                        {
                            cellValue.Add((double)value);
                            sumValue += (double)value;
                            numCount++;
                        }
                    }
                }
                if (numCount != 0)
                {
                    viewModel.Sum = sumValue;
                    viewModel.Average= Math.Round(sumValue / numCount, 2);
                    viewModel.Min = cellValue.Min();
                    viewModel.Max = cellValue.Max();
                }
                else
                {
                    this.Reset();
                }
                viewModel.Count = SelectedCells.Count;
                viewModel.NumCount = numCount;
            }
            else
            {
                this.Reset();
            }
        }

        private void Reset()
        {
            var viewModel = this.dataGrid.DataContext as ProductInfoViewModel;
            viewModel.Min = 0;
            viewModel.NumCount = 0;
            viewModel.Max = 0;
            viewModel.Sum = 0;
            viewModel.Average = 0;
            viewModel.Sum = 0;
            viewModel.Count = 0;
        }


        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();           
            this.dataGrid.SelectionChanged -= DataGrid_SelectionChanged;
            this.comboBox.SelectionChanged -= ComboBox_SelectionChanged;
            (this.dataGrid.SelectionController as CellSelectionController).FilterApplied -= DataGrid_FilterApplied;
            this.dataGrid.Dispose();
            this.dataGrid.ItemsSource = null;
            if (this.DataContext is IDisposable)
                (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
    public class CellSelectionController : GridCellSelectionController
    {
        public event GridFilterAppliedEventHandler FilterApplied;
        public delegate void GridFilterAppliedEventHandler(object sender, EventArgs e);

        public CellSelectionController(SfDataGrid dataGrid)
            : base(dataGrid)
        {
        }

        protected override void ProcessOnFilterApplied(GridFilteringEventArgs args)
        {
            base.ProcessOnFilterApplied(args);
            this.RaiseFilterApplied();
        }

        private void RaiseFilterApplied()
        {
            if (FilterApplied != null)
                this.FilterApplied(this, new EventArgs());
        }
    }
}
