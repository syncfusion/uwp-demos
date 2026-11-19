using Common;
using Syncfusion.UI.Xaml.CellGrid;
using Syncfusion.UI.Xaml.Grid.Utility;
//using Syncfusion.UI.Xaml.Grid.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
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
    public sealed partial class CellStyle : SampleLayout,IDisposable
    {
        #region Constructor

        public CellStyle()
        {
            this.InitializeComponent();

            cellGrid.RowCount = 28;
            cellGrid.ColumnCount = 10;
            cellGrid.DefaultRowHeight = 30;
            cellGrid.DefaultColumnWidth = 100;
            // Header Column width is reduced for Mobile view
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                cellGrid.ColumnWidths[0] = 50;
            SetInterior();
            SetFont();
            SetFontWeights();
            SetTextColor();
            SetBorders();
        }

        #endregion

        #region Private Methods

        private void SetBorders()
        {
            int col = cellGrid.ColumnCount / 2;
            
            this.cellGrid.Model[21, 1].CellValue = "Borders";
            this.cellGrid.Model[21, 1].Font.FontSize = 15;
            this.cellGrid.Model[21, 1].Background = new SolidColorBrush(Colors.BlanchedAlmond);

            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(22, 1, 22, cellGrid.ColumnCount - 1));
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(23, 1, 23, cellGrid.ColumnCount - 1));
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(24, 1, 24, cellGrid.ColumnCount - 1));
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(25, 1, 25, cellGrid.ColumnCount - 1));


            this.cellGrid.Model[22, 1].Borders.Bottom = new Pen(new SolidColorBrush(Colors.Blue), 1);
            this.cellGrid.Model[23, 1].Borders.Top = new Pen(new SolidColorBrush(Colors.Red), 1);
            this.cellGrid.Model[24, 1].Borders.Right = new Pen(new SolidColorBrush(Colors.Purple), 3);

            this.cellGrid.Model[22, 1].Borders.All = new Pen(new SolidColorBrush(Colors.Blue), 1,BorderStyle.Dashed);
            this.cellGrid.Model[23, 1].Borders.All = new Pen(new SolidColorBrush(Colors.Red), 1,BorderStyle.Dotted);
            this.cellGrid.Model[24, 1].Borders.All = new Pen(new SolidColorBrush(Colors.Green), 1,BorderStyle.DashDotDot);
        }

        private void SetTextColor()
        {
            int col = cellGrid.ColumnCount / 2;

            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(16, 1, 16, col));
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(17, 1, 17, col));
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(18, 1, 18, col));
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(19, 1, 19, col));

            this.cellGrid.Model[15, 1].CellValue = "Text Color";
            this.cellGrid.Model[15, 1].Font.FontSize = 15;
            this.cellGrid.Model[15, 1].Background = new SolidColorBrush(Colors.BlanchedAlmond);
            this.cellGrid.Model[15, 1].HorizontalAlignment = HorizontalAlignment.Center;

            this.cellGrid.Model[16, 1].CellValue = "The quick brown fox jumps over the lazy dog";
            this.cellGrid.Model[16, 1].Foreground = new SolidColorBrush(Colors.Gray);

            this.cellGrid.Model[17, 1].CellValue = "The quick brown fox jumps over the lazy dog";
            this.cellGrid.Model[17, 1].Foreground = new SolidColorBrush(Colors.Red);

            this.cellGrid.Model[18, 1].CellValue = "The quick brown fox jumps over the lazy dog";
            this.cellGrid.Model[18, 1].Foreground = new SolidColorBrush(Colors.Blue);

            this.cellGrid.Model[19, 1].CellValue = "The quick brown fox jumps over the lazy dog";
            this.cellGrid.Model[19, 1].Foreground = new SolidColorBrush(Colors.Green);
        }

        private void SetFontWeights()
        {
            this.cellGrid.Model[10, 1].CellValue = "Text Style";
            this.cellGrid.Model[10, 1].Font.FontSize = 15;
            this.cellGrid.Model[10, 1].Background = new SolidColorBrush(Colors.BlanchedAlmond);
            this.cellGrid.Model[10, 1].HorizontalAlignment = HorizontalAlignment.Center;

            int col = cellGrid.ColumnCount / 2;
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(11, 1, 11, cellGrid.ColumnCount - 1));
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(12, 1, 12, cellGrid.ColumnCount - 1));
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(13, 1, 13, cellGrid.ColumnCount - 1));

            this.cellGrid.Model[11, 1].Font.FontWeight = FontWeights.Bold;
            this.cellGrid.Model[11, 1].HorizontalAlignment = HorizontalAlignment.Left;
            this.cellGrid.Model[11, 1].CellValue = "Font weight is Bold";

            this.cellGrid.Model[12, 1].Font.FontStyle = FontStyle.Italic;
            this.cellGrid.Model[12, 1].HorizontalAlignment = HorizontalAlignment.Left;
            this.cellGrid.Model[12, 1].CellValue = "Font style is Itlaic";

            this.cellGrid.Model[13, 1].Font.FontStyle = FontStyle.Normal;
            this.cellGrid.Model[13, 1].HorizontalAlignment = HorizontalAlignment.Left;
            this.cellGrid.Model[13, 1].CellValue = "Font style is Normal";
        }

        private void SetInterior()
        {
            this.cellGrid.Model[1, 1].CellValue = "Interior";
            this.cellGrid.Model[1, 1].Font.FontSize = 15;
            this.cellGrid.Model[1, 1].Background = new SolidColorBrush(Colors.BlanchedAlmond);
            this.cellGrid.Model[1, 1].HorizontalAlignment = HorizontalAlignment.Center;

            var CellBackground = this.cellGrid.Model[2, 1];
            CellBackground.Background = new SolidColorBrush(Colors.Aquamarine);
            this.cellGrid.Model[2, 1].CellValue = "Aquamarine";

            this.cellGrid.Model[2, 2].Background = new SolidColorBrush(Colors.Violet);
            this.cellGrid.Model[2, 2].CellValue = "Violet";

            this.cellGrid.Model[2, 3].Background = new SolidColorBrush(Colors.LavenderBlush);
            this.cellGrid.Model[2, 3].CellValue = "LavenderBlush";

            this.cellGrid.Model[2, 4].Background = GetLinerBrush();
            this.cellGrid.Model[2, 4].CellValue = "Linear Brush";

            this.cellGrid.Model[2, 5].Background = new SolidColorBrush(Colors.CadetBlue);
            this.cellGrid.Model[2, 5].CellValue = "CadetBlue";

            this.cellGrid.Model[2, 6].Background = new SolidColorBrush(Colors.LemonChiffon);
            this.cellGrid.Model[2, 6].CellValue = "LemonChiffon";

        }

        private void SetFont()
        {
            this.cellGrid.Model[4, 1].CellValue = "Font";
            this.cellGrid.Model[4, 1].Font.FontSize = 15;
            this.cellGrid.Model[4, 1].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[4, 1].Background = new SolidColorBrush(Colors.BlanchedAlmond);

            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(5, 1, 5, cellGrid.ColumnCount));
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(6, 1, 6, cellGrid.ColumnCount));
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(7, 1, 7, cellGrid.ColumnCount));
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(8, 1, 8, cellGrid.ColumnCount));

            this.cellGrid.Model[5, 1].Font.FontSize = 10;
            this.cellGrid.Model[5, 1].CellValue = "The quick brown fox jumps over the lazy dog";

            this.cellGrid.Model[6, 1].Font.FontSize = 12;
            this.cellGrid.Model[6, 1].CellValue = "The quick brown fox jumps over the lazy dog";

            this.cellGrid.Model[7, 1].Font.FontSize = 14;
            this.cellGrid.Model[7, 1].CellValue = "The quick brown fox jumps over the lazy dog";

            this.cellGrid.Model[8, 1].Font.FontSize = 16;
            this.cellGrid.Model[8, 1].CellValue = "The quick brown fox jumps over the lazy dog";
        }

        private Brush GetLinerBrush()
        {
            var gradientStopCollection = new GradientStopCollection();

            GradientStop gradientStop1 = new GradientStop();
            gradientStop1.Color = Colors.Red;
            gradientStop1.Offset = 0.0d;

            GradientStop gradientStop2 = new GradientStop();
            gradientStop2.Color = Colors.Yellow;
            gradientStop2.Offset = 1.0d;

            gradientStopCollection.Add(gradientStop1);
            gradientStopCollection.Add(gradientStop2);

            LinearGradientBrush brush = new LinearGradientBrush(gradientStopCollection, 60);
            brush.StartPoint = new Point(0.500006, 1.01436);
            brush.EndPoint = new Point(0.500006, -0.0213787);
            return brush;
        }

        #endregion

        #region Dispose Method

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
