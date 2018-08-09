using Syncfusion.UI.Xaml.CellGrid;
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

namespace tabcontrol
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class rowheight : Page
    {
        #region Private Fields

        internal bool IsRow;

        #endregion

        #region Constructor

        public rowheight()
        {
            this.InitializeComponent();
            Loaded += rowheight_Loaded;
        }

        #endregion

        #region Events

        void rowheight_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsRow)
            {
                headertext.Text = "Row Height for the Current Row";
                sideheadeing.Text = "Row Height";
            }
            else
            {
                headertext.Text = " Column width for Current Column";
                sideheadeing.Text = "Column Width";
            }
        }

        private void submit(object sender, RoutedEventArgs e)
        {
            var cellgrid = DataContext as SfCellGrid;
            GridRangeInfo Range = cellgrid.SelectedRanges.ActiveRange;
            if (IsRow)
            {
                for (int i = Range.Top; i <= Range.Bottom; i++)
                {
                    for (int j = Range.Left; j <= Range.Right; j++)
                    {
                        cellgrid.RowHeights[i] = double.Parse(rowheightsize.Text);
                    }
                }
                cellgrid.InvalidateVisual();
            }
            else
            {
                for (int i = Range.Top; i <= Range.Bottom; i++)
                {
                    for (int j = Range.Left; j <= Range.Right; j++)
                    {
                        cellgrid.ColumnWidths[j] = double.Parse(rowheightsize.Text);
                    }
                }
                cellgrid.InvalidateCell(Range);
            }
            (this.Tag as Popup).IsOpen = false;
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            (this.Tag as Popup).IsOpen = false;
        }

        #endregion
    }
}
