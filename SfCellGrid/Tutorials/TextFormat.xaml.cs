using Common;
using Syncfusion.UI.Xaml.CellGrid;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public sealed partial class TextFormat : SampleLayout,IDisposable
    {
        #region Constructor

        public TextFormat()
        {
            this.InitializeComponent();
            InitializeGrid();
        }

        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            cellGrid.RowCount = 18;
            cellGrid.ColumnCount = 10;
            cellGrid.DefaultRowHeight = 40;
            cellGrid.DefaultColumnWidth = 130;
            // Header Column Width is reduced
            cellGrid.ColumnWidths[0] = 50;

            // For Mobile View, Column Width is set to hide to dislay the content in Visual
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                cellGrid.ColumnWidths[1] = 0;

            int rowIndex = 1;
            int colIndex = 2;
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(rowIndex, colIndex, rowIndex, colIndex + 1));
            cellGrid.Model[rowIndex, colIndex].CellValue = "Text Formats";
            cellGrid.Model[rowIndex, colIndex].Background = new SolidColorBrush(Colors.DarkBlue);
            cellGrid.Model[rowIndex, colIndex].Foreground = new SolidColorBrush(Colors.White);
            cellGrid.Model[rowIndex, colIndex].Font.FontSize = 15;
            cellGrid.Model[rowIndex, colIndex].HorizontalAlignment = HorizontalAlignment.Center;

            rowIndex++;
            cellGrid.Model[rowIndex, colIndex].CellValue = "Format";
            cellGrid.Model[rowIndex, colIndex].Background = new SolidColorBrush(Colors.Green);
            cellGrid.Model[rowIndex, colIndex].Foreground = new SolidColorBrush(Colors.White);

            cellGrid.Model[rowIndex, colIndex + 1].CellValue = "Example";
            cellGrid.Model[rowIndex, colIndex + 1].Background = new SolidColorBrush(Colors.Green);
            cellGrid.Model[rowIndex, colIndex + 1].Foreground = new SolidColorBrush(Colors.White);

            rowIndex++;

            //NumberFormat
            string[] NumberFormat = new string[]
                {
                    "0.00",
                    "C",
                    "0.00;(0.00)",
                    "###0.##%",
                    "#0.#E+00",
                    "10:##,##0.#"
                };

            GridModel model = this.cellGrid.Model;
            foreach (string format in NumberFormat)
            {
                model[rowIndex, colIndex].CellValue = format;
                model[rowIndex, colIndex].HorizontalAlignment = HorizontalAlignment.Center;
                model[rowIndex, colIndex].CellType = "Static";

                model[rowIndex, colIndex + 1].Format = format;
                model[rowIndex, colIndex + 1].CellValue = Math.PI;
                rowIndex += 2;
            }

            rowIndex = 1;
            colIndex = 5;
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(rowIndex, colIndex, rowIndex, colIndex + 1));
            model[rowIndex, colIndex].CellValue = "DateTime Formats";
            cellGrid.Model[rowIndex, colIndex].Background = new SolidColorBrush(Colors.DarkBlue);
            cellGrid.Model[rowIndex, colIndex].Foreground = new SolidColorBrush(Colors.White);
            cellGrid.Model[rowIndex, colIndex].Font.FontSize = 15;
            model[rowIndex, colIndex].HorizontalAlignment = HorizontalAlignment.Center;

            rowIndex++;
            cellGrid.Model[rowIndex, colIndex].CellValue = "Format";
            cellGrid.Model[rowIndex, colIndex].Background = new SolidColorBrush(Colors.Green);
            cellGrid.Model[rowIndex, colIndex].Foreground = new SolidColorBrush(Colors.White);

            cellGrid.Model[rowIndex, colIndex + 1].CellValue = "Example";
            cellGrid.Model[rowIndex, colIndex + 1].Background = new SolidColorBrush(Colors.Green);
            cellGrid.Model[rowIndex, colIndex + 1].Foreground = new SolidColorBrush(Colors.White);
            rowIndex++;

            //DateTimeFormat
            string[] DateTimeFormat = new string[]
                {
                    "d",
                    "D",
                    "f",
                    "dddd, dd MMMM yyyy",
                    "t",
                    "s"
                };

            foreach (string format in DateTimeFormat)
            {
                model[rowIndex, colIndex].CellValue = format;
                model[rowIndex, colIndex].HorizontalAlignment = HorizontalAlignment.Center;
                model[rowIndex, colIndex].CellType = "Static";

                model[rowIndex, colIndex + 1].Format = format;
                model[rowIndex, colIndex + 1].CellValue = DateTime.Now;
                rowIndex += 2;
            }
        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            if (cellGrid != null)
            {
                cellGrid.Dispose();
                cellGrid = null;
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion

    }
}
