using Common;
using Syncfusion.UI.Xaml.CellGrid;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CellGridSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Virtualization : SampleLayout,IDisposable
    {
        #region Constructor

        public Virtualization()
        {
            this.InitializeComponent();
            InitializeGrid();
        }

        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            grid.RowCount = 100000;
            grid.ColumnCount = 10000;
            // Default Row Height and Default Column Width is reduced
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                grid.DefaultColumnWidth = 80;
                grid.DefaultRowHeight = 20;
            }
            grid.Model.QueryCellInfo += Model_QueryCellInfo;
        }

        #endregion

        #region Events

        void Model_QueryCellInfo(object sender, Syncfusion.UI.Xaml.CellGrid.Styles.GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex == 0 && e.Cell.ColumnIndex == 0)
                return;
            if (e.Cell.RowIndex == 0)
            {
                e.Style.CellValue = e.Cell.ColumnIndex;
                e.Style.Font.FontWeight = Windows.UI.Text.FontWeights.Bold;
                e.Style.VerticalAlignment = VerticalAlignment.Center;
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
            }
            else if (e.Cell.ColumnIndex == 0)
            {
                e.Style.CellValue = e.Cell.RowIndex;
                e.Style.Font.FontWeight = Windows.UI.Text.FontWeights.Bold;
                e.Style.VerticalAlignment = VerticalAlignment.Center;
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
            }
            else
                e.Style.CellValue = String.Format("({0} , {1})", e.Cell.RowIndex, e.Cell.ColumnIndex);
        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            if (grid != null)
            {
                grid.Model.QueryCellInfo -= Model_QueryCellInfo;
                grid.Dispose();
                grid = null;
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion
    }
}
