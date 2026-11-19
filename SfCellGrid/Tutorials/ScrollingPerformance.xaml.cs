using Common;
using Syncfusion.UI.Xaml.CellGrid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CellGridSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScrollPerformance : SampleLayout, IDisposable
    {
        #region Private Fields

        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();

        #endregion

        #region Constructor

        public ScrollPerformance()
        {
            this.InitializeComponent();
            InitializeGrid();
        }

        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            grid.Model.HeaderStyle.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Model.HeaderStyle.VerticalAlignment = VerticalAlignment.Center;
            grid.RowCount = 1000;
            grid.ColumnCount = 1000;
            // Default Row Height and Default Column Width is reduced for Mobile View
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                grid.DefaultColumnWidth = 80;
                grid.DefaultRowHeight = 20;
                //Header Column Width is reduced for Mobile View
                grid.ColumnWidths[0] = 50;
            }
            {
                grid.DefaultColumnWidth = 150;
                grid.DefaultRowHeight = 45;
            }

            grid.Model.TableStyle.ReadOnly = true;
            grid.Model.QueryCellInfo += Model_QueryCellInfo;
        }

        #endregion

        #region Events

        private void Model_QueryCellInfo(object sender, Syncfusion.UI.Xaml.CellGrid.Styles.GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex == 0)
            {
                if (e.Cell.ColumnIndex > 0)
                    e.Style.CellValue = e.Cell.ColumnIndex;
            }
            else if (e.Cell.RowIndex > 0)
            {
                if (e.Cell.ColumnIndex == 0)
                    e.Style.CellValue = e.Cell.RowIndex;
                else if (e.Cell.ColumnIndex > 0)
                {
                    e.Style.CellValue = String.Format("{0}/{1}", e.Cell.RowIndex, e.Cell.ColumnIndex);
                }
            }
        }

        private void HScrollTimer_Click(object sender, RoutedEventArgs e)
        {
            dt.Tick += HScrollTimer_Tick;
            sw.Start();
            dt.Start();
        }

        private void VScrollTimer_Click(object sender, RoutedEventArgs e)
        {
            dt.Tick += VScrollTimer_Tick;
            sw.Start();
            dt.Start();
        }

        private async void VScrollTimer_Tick(object sender, object e)
        {
            if ((bool)scrollBottom.IsChecked)
            {
                grid.ScrollRows.ScrollToNextPage();
                grid.InvalidateVisual();
                if (grid.ScrollRows.LastBodyVisibleLineIndex >= grid.RowCount - 1)
                {
                    sw.Stop();
                    dt.Stop();
                    var messageDialog = new MessageDialog(sw.Elapsed.Seconds.ToString() + " Seconds  " + sw.Elapsed.Milliseconds.ToString() + " MilliSeconds", "Scroll Timer");
                    await messageDialog.ShowAsync();
                    sw.Reset();
                    dt.Tick -= VScrollTimer_Tick;
                }
            }
            else
            {
                grid.ScrollRows.ScrollToPreviousPage();
                grid.InvalidateVisual();
                if (grid.ScrollRows.ScrollLineIndex <= grid.HeaderRows)
                {
                    sw.Stop();
                    dt.Stop();
                    var messageDialog = new MessageDialog(sw.Elapsed.Seconds.ToString() + " Seconds  " + sw.Elapsed.Milliseconds.ToString() + " MilliSeconds", "Scroll Timer");
                    await messageDialog.ShowAsync();
                    sw.Reset();
                    dt.Tick -= VScrollTimer_Tick;
                }
            }
        }

        private async void HScrollTimer_Tick(object sender, object e)
        {
            if ((bool)scrollRight.IsChecked)
            {
                grid.ScrollColumns.ScrollToNextPage();
                grid.InvalidateVisual();
                if (grid.ScrollColumns.LastBodyVisibleLineIndex >= grid.ColumnCount - 1)
                {
                    sw.Stop();
                    dt.Stop();
                    var messageDialog = new MessageDialog(sw.Elapsed.Seconds.ToString() + " Seconds  " + sw.Elapsed.Milliseconds.ToString() + " MilliSeconds", "Scroll Timer");
                    await messageDialog.ShowAsync();
                    sw.Reset();
                    dt.Tick -= HScrollTimer_Tick;
                }
            }
            else
            {
                grid.ScrollColumns.ScrollToPreviousPage();
                grid.InvalidateVisual();
                if (grid.ScrollColumns.ScrollLineIndex <= grid.HeaderColumns)
                {
                    sw.Stop();
                    dt.Stop();
                    var messageDialog = new MessageDialog(sw.Elapsed.Seconds.ToString() + " Seconds  " + sw.Elapsed.Milliseconds.ToString() + " MilliSeconds", "Scroll Timer");
                    await messageDialog.ShowAsync();
                    sw.Reset();
                    dt.Tick -= HScrollTimer_Tick;
                }
            }
        }

        private void rdo1_Checked(object sender, RoutedEventArgs e)
        {
            grid.RowCount = 1000;
        }

        private void rdo2_Checked(object sender, RoutedEventArgs e)
        {
            grid.RowCount = 10000;
        }

        private void rdo3_Checked(object sender, RoutedEventArgs e)
        {
            grid.RowCount = 100000;
        }

        private void rdoCol1_Checked(object sender, RoutedEventArgs e)
        {
            grid.ColumnCount = 1000;
        }

        private void rdoCol2_Checked(object sender, RoutedEventArgs e)
        {
            grid.ColumnCount = 10000;
        }

        private void rdoCol3_Checked(object sender, RoutedEventArgs e)
        {
            grid.ColumnCount = 100000;
        }

        private void scrollHzntl_Changed(object sender, RoutedEventArgs e)
        {
            if ((bool)scrollRight.IsChecked)
                grid.ScrollInView(new Syncfusion.UI.Xaml.Grid.ScrollAxis.RowColumnIndex(1, 1));
            else
                grid.ScrollInView(new Syncfusion.UI.Xaml.Grid.ScrollAxis.RowColumnIndex(0, grid.ColumnCount - 1));
        }

        private void scrollVtcl_Changed(object sender, RoutedEventArgs e)
        {
            if ((bool)scrollTop.IsChecked)
                grid.ScrollInView(new Syncfusion.UI.Xaml.Grid.ScrollAxis.RowColumnIndex(grid.RowCount - 1, 0));
            else
                grid.ScrollInView(new Syncfusion.UI.Xaml.Grid.ScrollAxis.RowColumnIndex(1, 1));
        }

        private void Options_Loaded(object sender, RoutedEventArgs e)
        {
            // if we set IsChecked property in Xaml then the last CheckBox which contains IsChecked property to true only Checked, 
            // so IsChecked property is set after page is loaded
            rdo1.IsChecked = true;
            rdoCol1.IsChecked = true;
            scrollRight.IsChecked = true;
            scrollBottom.IsChecked = true;
        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            if (dt != null)
            {
                dt.Stop();
                dt.Tick -= VScrollTimer_Tick;
                dt.Tick -= HScrollTimer_Tick;
            }
            if (this.grid != null)
            {
                grid.Model.QueryCellInfo -= Model_QueryCellInfo;
                this.grid.Dispose();
                this.grid = null;
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion

    }
}
