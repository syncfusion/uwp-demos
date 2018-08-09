using Common;
using Syncfusion.UI.Xaml.CellGrid;
using Syncfusion.UI.Xaml.CellGrid.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
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
    public sealed partial class CopyPaste : SampleLayout,IDisposable
    {
        #region Constructor

        public CopyPaste()
        {
            this.InitializeComponent();
            InitializeGrid();
        }

        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            cellGrid.RowCount = 20;
            cellGrid.ColumnCount = 10;
            cellGrid.DefaultRowHeight = 35;
            cellGrid.DefaultColumnWidth = 120;
            cellGrid.ColumnWidths[0] = 50;
            cellGrid.Model.QueryCellInfo += Model_QueryCellInfo;
            cellGrid.Model.TableStyle.CellType = "FormulaCell";
            cellGrid.ShowComment = true;

            Options.SelectionChanged += Options_SelectionChanged;
            Options.Items.Add("All");
            Options.Items.Add("ClipboardOnly");
            Options.Items.Add("Comments");
            Options.Items.Add("Formats");
            Options.Items.Add("Formulas");
            Options.Items.Add("Transpose");
            Options.Items.Add("Values");
            Options.SelectedIndex = 0;

            SetInterior();
            SetTextColor();
            SetComments();
            SetFormulas();
        }

        private void SetFormulas()
        {
            this.cellGrid.Model[13, 1].CellValue = "Formulas";
            this.cellGrid.Model[13, 1].Font.FontSize = 15;
            this.cellGrid.Model[13, 1].Background = new SolidColorBrush(Colors.BlanchedAlmond);
            this.cellGrid.Model[13, 1].HorizontalAlignment = HorizontalAlignment.Center;

            cellGrid.Model[14, 2].CellValue = 100;
            cellGrid.Model[14, 1].CellValue = "=B14";
            cellGrid.Model[14, 3].CellValue = "=SUM(A14,B14)";
        }

        private void SetComments()
        {
            this.cellGrid.Model[10, 1].CellValue = "Comments";
            this.cellGrid.Model[10, 1].Font.FontSize = 15;
            this.cellGrid.Model[10, 1].Background = new SolidColorBrush(Colors.BlanchedAlmond);
            this.cellGrid.Model[10, 1].HorizontalAlignment = HorizontalAlignment.Center;

            this.cellGrid.Model[11, 1].Comment = "11th Row, 1st Column";
            this.cellGrid.Model[11, 1].CellValue = "Comment";
            this.cellGrid.Model[11, 1].Foreground = new SolidColorBrush(Colors.Green);
            this.cellGrid.Model[11, 1].CommentBrush = new SolidColorBrush(Colors.Green);

            this.cellGrid.Model[11, 2].Comment = "11th Row, 2nd Column";
            this.cellGrid.Model[11, 2].Foreground = new SolidColorBrush(Colors.Blue);
            this.cellGrid.Model[11, 2].CommentBrush = new SolidColorBrush(Colors.Blue);
            this.cellGrid.Model[11, 2].CellValue = "11,2";

            this.cellGrid.Model[11, 3].Comment = "11th Row, 3rd Column";
            this.cellGrid.Model[11, 3].Foreground = new SolidColorBrush(Colors.Red);
            this.cellGrid.Model[11, 3].CellValue = "11,3";
        }

        private void SetTextColor()
        {
            int col = cellGrid.ColumnCount / 2;

            this.cellGrid.Model[4, 1].CellValue = "Text Color";
            this.cellGrid.Model[4, 1].Font.FontSize = 15;
            this.cellGrid.Model[4, 1].Background = new SolidColorBrush(Colors.BlanchedAlmond);
            this.cellGrid.Model[4, 1].HorizontalAlignment = HorizontalAlignment.Center;

            this.cellGrid.Model[5, 1].CellValue = "The quick brown fox jumps over the lazy dog";
            this.cellGrid.Model[5, 1].Foreground = new SolidColorBrush(Colors.Gray);

            this.cellGrid.Model[6, 1].CellValue = "The quick brown fox jumps over the lazy dog";
            this.cellGrid.Model[6, 1].Foreground = new SolidColorBrush(Colors.Red);

            this.cellGrid.Model[7, 1].CellValue = "The quick brown fox jumps over the lazy dog";
            this.cellGrid.Model[7, 1].Foreground = new SolidColorBrush(Colors.Blue);

            this.cellGrid.Model[8, 1].CellValue = "The quick brown fox jumps over the lazy dog";
            this.cellGrid.Model[8, 1].Foreground = new SolidColorBrush(Colors.Green);
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

            this.cellGrid.Model[2, 4].Background = new SolidColorBrush(Colors.MistyRose);
            this.cellGrid.Model[2, 4].CellValue = "MistyRose";

            this.cellGrid.Model[2, 5].Background = new SolidColorBrush(Colors.CadetBlue);
            this.cellGrid.Model[2, 5].CellValue = "CadetBlue";

            this.cellGrid.Model[2, 6].Background = new SolidColorBrush(Colors.LemonChiffon);
            this.cellGrid.Model[2, 6].CellValue = "LemonChiffon";

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

        private void Options_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            switch(Options.SelectedValue.ToString())
            {
                case "All":
                    cellGrid.Model.CopyPasteOptions = CopyPasteOptions.All;
                    break;
                case "ClipboardOnly":
                    cellGrid.Model.CopyPasteOptions = CopyPasteOptions.ClipboardOnly;
                    break;
                case "Comments":
                    cellGrid.Model.CopyPasteOptions = CopyPasteOptions.Comments;
                    break;
                case "Formats":
                    cellGrid.Model.CopyPasteOptions = CopyPasteOptions.Formats;
                    break;
                case "Formulas":
                    cellGrid.Model.CopyPasteOptions = CopyPasteOptions.Formulas;
                    break;
                case "Transpose":
                    cellGrid.Model.CopyPasteOptions = CopyPasteOptions.Transpose;
                    break;
                case "Values":
                    cellGrid.Model.CopyPasteOptions = CopyPasteOptions.Values;
                    break;
            }
        }

        private async void ShowText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var text = await Clipboard.GetContent().GetTextAsync();
                ClipboardText.Text = text;
            }
            catch
            {
                ClipboardText.Text = string.Empty;
            }
        }

        private void ClearText_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.Clear();
            ClipboardText.Text = string.Empty;
        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            if (cellGrid != null)
            {
                cellGrid.Model.QueryCellInfo -= Model_QueryCellInfo;
                if (Options != null)
                    Options.SelectionChanged -= Options_SelectionChanged;
                cellGrid.Dispose();
                cellGrid = null;
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion
    }
}
