using Common;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.UI.Xaml.ScrollAxis;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DataGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilterRow : SampleLayout
    {
        public FilterRow()
        {
            this.InitializeComponent();
            this.DataContext = new OrderInfoViewModel();
            this.sfGrid.Loaded += OnSfDataGridLoaded;
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.sfGrid.Loaded -= OnSfDataGridLoaded;
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }

        /// <summary>
        /// Occurs when the SfDataGrid is laid out, rendered, and ready for interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSfDataGridLoaded(object sender, RoutedEventArgs e)
        {
            this.sfGrid.MoveCurrentCell(new RowColumnIndex(this.sfGrid.GetFilterRowIndex(), this.sfGrid.GetFirstColumnIndex()));
            this.sfGrid.SelectionController.CurrentCellManager.BeginEdit();
        }
    }
}
