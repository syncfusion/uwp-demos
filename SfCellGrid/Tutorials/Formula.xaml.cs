using Common;
using Syncfusion.UI.Xaml.CellGrid;
using Syncfusion.UI.Xaml.CellGrid.Converter;
using Syncfusion.UI.Xaml.Controls.Navigation;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
    public sealed partial class Formula : SampleLayout,IDisposable
    {
        #region Constructor

        public Formula()
        {
            this.InitializeComponent();
            cellgrid.IsHitTestVisible = false;
            InitializeGrid();
            OpenWorkbook();
        }

        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            cellgrid.RowCount = 100;
            cellgrid.ColumnCount = 100;
            cellgrid.DefaultRowHeight = 30;
            cellgrid.DefaultColumnWidth = 80;
            cellgrid.Model.QueryCellInfo += Model_QueryCellInfo;
        }

        private async void OpenWorkbook()
        {
            var assembly = typeof(CellGridSamples.Formula).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.SfCellGrid.Assets.Formula.xlsx";
            try
            {
                using (var fileStream = assembly.GetManifestResourceStream(resourcePath))
                {
                    var engine = new ExcelEngine();
                    IWorkbook book = await engine.Excel.Workbooks.OpenAsync(fileStream);
                    LoadExcel(book);
                }
            }
            catch
            {

            }
            finally
            {
                cellgrid.IsHitTestVisible = true;
                ProgressRing.IsActive = false;
            }
        }

        private void LoadExcel(IWorkbook book)
        {
            ExcelImportExtension.ImportFromExcel(cellgrid, book.ActiveSheet, null);
            cellgrid.RowCount = cellgrid.RowCount < 100 ? 100 : cellgrid.RowCount;
            cellgrid.ColumnCount = cellgrid.ColumnCount < 100 ? 100 : cellgrid.ColumnCount;
        }

        #endregion

        #region Events

        private void Model_QueryCellInfo(object sender, Syncfusion.UI.Xaml.CellGrid.Styles.GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex == 0 && e.Cell.ColumnIndex == 0)
                return;
            if (e.Cell.RowIndex == 0)
            {
                e.Style.CellValue = GridRangeInfo.GetAlphaLabel(e.Cell.ColumnIndex);
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
            }
            else if (e.Cell.ColumnIndex == 0)
            {
                e.Style.CellValue = e.Cell.RowIndex;
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
            }
        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            if (cellgrid != null)
            {
                cellgrid.Dispose();
                cellgrid = null;
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion
    }
}

