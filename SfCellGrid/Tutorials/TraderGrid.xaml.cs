using Common;
using Syncfusion.UI.Xaml.CellGrid;
using Syncfusion.UI.Xaml.CellGrid.Styles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class TraderGrid : SampleLayout,IDisposable
    {
        #region Private Fields

        private DispatcherTimer timer;
        private Random random = new Random();
        int[] values = { 15, 23, 52, 23, 42, 23, 49, 180, 192, 83, 40, 95, 20, 34, 108, 923, 48, 173, 25, 100, 329, 65, 78, 195, 111, 24, 28, 492 };

        #endregion

        #region Constructor

        public TraderGrid()
        {
            this.InitializeComponent();
            InitializeGrid();
        }

        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            cellGrid.RowCount = 50;
            cellGrid.ColumnCount = 50;
            cellGrid.DefaultRowHeight = 35;
            cellGrid.DefaultColumnWidth = 100;
            SetupTimer();
            // Header Column Width is reduced for Mobile View
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                cellGrid.ColumnWidths[0] = 50;
            cellGrid.Model.QueryCellInfo += Model_QueryCellInfo;
        }

        private void SetupTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }

        #endregion

        #region Events
        
        private void Model_QueryCellInfo(object sender, GridQueryCellInfoEventArgs e)
        {
            int row = e.Cell.RowIndex;
            int col = e.Cell.ColumnIndex;
            if (row == 0 && col == 0)
                return;
            if (row == 0)
            {
                e.Style.CellValue = e.Cell.ColumnIndex;
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
                return;
            }
            else if (col == 0)
            {
                e.Style.CellValue = e.Cell.RowIndex;
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
                return;
            }
            var randomValue = Convert.ToInt32(values[random.Next(0, values.Count() - 1)]);
            e.Style.CellValue = randomValue;
            if ((randomValue > 50 && randomValue < 100) || randomValue < 50)
                e.Style.Background = new SolidColorBrush(Colors.White);
            else if (randomValue > 100 && randomValue < 150)
                e.Style.Background = new SolidColorBrush(Colors.Cyan);
            else if (randomValue > 150 && randomValue < 200)
                e.Style.Background = new SolidColorBrush(Color.FromArgb(255, 104, 255, 104));      // Light Green
            else
                e.Style.Background = new SolidColorBrush(Color.FromArgb(255, 255, 104, 104));      // Light Red
        }

        private void Timer_Tick(object sender, object e)
        {
            if (cellGrid != null)
            {
                cellGrid.Model.InsertRows(5, 2);
                cellGrid.Model.RemoveRows(7, 2);
                cellGrid.InvalidateCells();
            }
        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
            }
            if (cellGrid != null)
            {
                cellGrid.Model.QueryCellInfo -= Model_QueryCellInfo;
                cellGrid.Dispose();
                cellGrid = null;
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion
    }
}
