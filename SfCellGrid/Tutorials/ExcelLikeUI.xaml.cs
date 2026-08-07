using Syncfusion.UI.Xaml.Controls.SfRibbon;
using Syncfusion.UI.Xaml.Controls.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Text;
using System.Text.RegularExpressions;
//using Syncfusion.UI.Xaml.ScrollAxis;
using System.Collections.ObjectModel;
//using Syncfusion.UI.Xaml.Grid.Converter;
//using Syncfusion.UI.Xaml.Grid.GridCellRenderer;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Threading.Tasks;
using Syncfusion.UI.Xaml.CellGrid.Styles;
using Syncfusion.UI.Xaml.Grid.ScrollAxis;
using Syncfusion.UI.Xaml.CellGrid;
using Syncfusion.UI.Xaml.Grid.Utility;
using Syncfusion.UI.Xaml.CellGrid.Helpers;
using tabcontrol;
using Windows.UI.Popups;
using Common;
using Syncfusion.SampleBrowser.UWP.SfCellGrid;
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CellGridSamples
{
    public sealed partial class ExcelLikeUI : SampleLayout, IDisposable
    {
        #region Private Fields

        List<double> fontsizes = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        ConditionalFormat condition = new ConditionalFormat();
        bool frezzepanes = false;

        #endregion

        #region Constructor

        public ExcelLikeUI()
        {
            this.InitializeComponent();
            this.DataContext = this;
            if (Font != null)
                Font.SelectedIndex = 2;
            if (FontSize_Combo != null)
                FontSize_Combo.SelectedIndex = 3;
            WireEvents();
        }

        #endregion

        #region Private Methods

        private void WireEvents()
        {
            backgroundcolor.PointerPressed += backgroundcolor_PointerPressed;
            backgroundcolor.PointerPressed += backgroundcolor_PointerPressed;
            foregroundcolor.PointerPressed += foregroundcolor_PointerPressed;
            gridlinecolor.PointerPressed += gridlinecolor_PointerPressed;
            gridlines.Checked += gridlines_Checked;
            gridlines.Unchecked += gridlines_Unchecked;
            ribbon.BackStageOpened += Ribbon_BackStageOpened;
            ribbon.Loaded += Ribbon_Loaded;
            ribbon.Unloaded += Ribbon_Unloaded;
        }

        private void UnWireEvents()
        {
            backgroundcolor.PointerPressed -= backgroundcolor_PointerPressed;
            backgroundcolor.PointerPressed -= backgroundcolor_PointerPressed;
            foregroundcolor.PointerPressed -= foregroundcolor_PointerPressed;
            gridlinecolor.PointerPressed -= gridlinecolor_PointerPressed;
            gridlines.Checked -= gridlines_Checked;
            gridlines.Unchecked -= gridlines_Unchecked;
            ribbon.BackStageOpened -= Ribbon_BackStageOpened;
            ribbon.Loaded -= Ribbon_Loaded;
            ribbon.Unloaded -= Ribbon_Unloaded;
        }

        private GridRangeInfo GetExpandRange(GridRangeInfo Range)
        {
            if (Range.IsTable)
                Range = GridRangeInfo.Cells(tabscontrol.ActiveGrid.HeaderRows, tabscontrol.ActiveGrid.HeaderColumns, tabscontrol.ActiveGrid.RowCount, tabscontrol.ActiveGrid.ColumnCount);
            else if (Range.IsRows)
                Range = GridRangeInfo.Cells(Range.Top, tabscontrol.ActiveGrid.HeaderColumns, Range.Bottom, tabscontrol.ActiveGrid.ColumnCount);
            else if (Range.IsCols)
                Range = GridRangeInfo.Cells(tabscontrol.ActiveGrid.HeaderRows, Range.Left, tabscontrol.ActiveGrid.ColumnCount, Range.Right);
            return Range;
        }

        private void Unmerge_Cells()
        {
            var cellGrid = tabscontrol.ActiveGrid;
            GridRangeInfo Range = cellGrid.SelectedRanges.ActiveRange;
            var rowcol = cellGrid.CurrentCell.CellRowColumnIndex;
            for (int row = Range.Top; row <= Range.Bottom; row++)
            {
                for (int col = Range.Left; col <= Range.Right; col++)
                {
                    cellGrid.CoveredCells.Remove(cellGrid.CoveredCells.GetCoveredCell(row, col));
                }
            }
            var style = cellGrid.Model[Range.Top, Range.Left];
            style.HorizontalAlignment = HorizontalAlignment.Left;
            cellGrid.InvalidateCell(Range);
        }

        private async void ShowErrorMessage(string message)
        {
            var msg = new MessageDialog(message, "Error");
            await msg.ShowAsync();
        }

        private void ValidateFormat(GridStyleInfo style)
        {
            var text = style.CellValue;
            DateTime date;
            double dates;
            if (style.Format != null)
            {
                if (style.Format.Equals("dd/MM/yy") || style.Format.Equals("dddd,MMMM dd,yyyy") || style.Format.Equals("hh:mm:ss tt"))
                {
                    if (DateTime.TryParse(text.ToString(), out date))
                    {
                        dates = CalcEngineHelper.ToOADate(date);
                        style.CellValue = CalcEngineHelper.FromOADate(dates - 1);
                    }
                    else
                    {
                        if (double.TryParse(text.ToString(), out dates))
                            try
                            {
                                style.CellValue = CalcEngineHelper.FromOADate(dates - 1);
                            }
                            catch
                            {
                                style.CellValue = dates;
                            }
                    }
                }
                else
                {
                    if (DateTime.TryParse(text.ToString(), out date))
                        style.CellValue = CalcEngineHelper.ToOADate(date);
                }
            }
            else
            {
                if (DateTime.TryParse(text.ToString(), out date))
                    style.CellValue = CalcEngineHelper.ToOADate(date);
            }
        }

        private string IncreaseDecrease_Decimals(RowColumnIndex rowcol, bool typeincrese)
        {
            double numbers = 0;
            var style = tabscontrol.ActiveGrid.Model[rowcol.RowIndex, rowcol.ColumnIndex];
            var cellvalue = tabscontrol.ActiveGrid.Model[rowcol.RowIndex, rowcol.ColumnIndex].CellValue.ToString();
            Match m = Match.Empty;
            if ((style.HasFormulaTag || double.TryParse(cellvalue, out numbers)) && !Regex.IsMatch(style.Format, @"([mdMy][,][ mdMy])|([mdMy][-/][mdMy])|([mdMy][-/](.*)[-/][mdMy])|([Mdy][\s][Mdy])", RegexOptions.IgnorePatternWhitespace))
            {
                if (style.Format == "0")
                {
                    if (typeincrese)
                        return "0.0";
                    else
                        return style.Format;
                }
                if (style.HasFormulaTag)
                {
                    var formattedtext = tabscontrol.ActiveGrid.Model[rowcol.RowIndex, rowcol.ColumnIndex].FormattedText;
                    if (Regex.IsMatch(formattedtext, @"^\d+$") || style.Format.Contains("."))
                    {
                        if (style.Format.Contains("."))
                            m = Regex.Match(style.Format, @"\d+[.]\d+");
                        else
                            m = Regex.Match(formattedtext, @"\d+[.]\d+");
                    }
                }
                else if (double.TryParse(cellvalue, out numbers))
                {
                    if (style.Format.Contains("."))
                        m = Regex.Match(style.Format, @"\d+[.]\d+");
                    else if (cellvalue.Contains("."))
                        m = Regex.Match(cellvalue, @"\d+[.]\d+");
                }

                if (m.Success)
                {
                    string replace;
                    if (typeincrese)
                        replace = m.Value + "0";
                    else
                    {
                        var length = m.Value.Length;
                        replace = m.Value[length - 2] != '.' ? m.Value.Substring(0, length - 1) : m.Value.Substring(0, length - 2);
                    }

                    replace = Regex.Replace(replace, @"[1-9\-]", "0");
                    int indexOfPoint = replace.IndexOf('.');
                    replace = indexOfPoint > 1 ? replace.Substring(indexOfPoint - 1) : replace;

                    style.Format = Regex.Replace(style.Format, m.Value, replace);

                    return style.Format;
                }
                else
                {
                    style.Format = "0.0";
                }
            }
            return style.Format;
        }

        private async Task Load()
        {
            FileOpenPicker filepicker = new FileOpenPicker();
            filepicker.ViewMode = PickerViewMode.List;
            filepicker.SuggestedStartLocation = PickerLocationId.Desktop;
            filepicker.FileTypeFilter.Add(".xls");
            filepicker.FileTypeFilter.Add(".xlsx");
            StorageFile file = await filepicker.PickSingleFileAsync();
            if (file != null)
            {
                try
                {
                    tabscontrol.LoadExcel(file);
                }
                catch
                {

                }
                ribbon.CloseBackStage();
                tabscontrol.Focus(FocusState.Pointer);
            }
        }

        #endregion

        #region Events

        private void Ribbon_BackStageOpened(object sender, EventArgs e)
        {
            tabscontrol.ActiveGrid.ShowHidePopup(false);
        }

        private void Ribbon_Unloaded(object sender, RoutedEventArgs e)
        {
            tabscontrol.ActiveGrid.CellClick -= ActiveGrid_CellClick;
        }

        private void Ribbon_Loaded(object sender, RoutedEventArgs e)
        {
            tabscontrol.ActiveGrid.CellClick += ActiveGrid_CellClick;
        }

        private void ActiveGrid_CellClick(object sender, GridCellClickEventArgs args)
        {
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                ribbon.CloseBackStage();
        }

        private void gridlinecolor_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Color gridcolor = gridlinecolor.SelectedColor;
            tabscontrol.ActiveGrid.GridLineColor = new SolidColorBrush(gridcolor);
            tabscontrol.ActiveGrid.InvalidateVisual();
        }

        private void foregroundcolor_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Color forecolor = foregroundcolor.SelectedColor;
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Foreground = new SolidColorBrush(forecolor);
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void backgroundcolor_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Color backcolor = backgroundcolor.SelectedColor;
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Background = new SolidColorBrush(backcolor);
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        void gridlines_Unchecked(object sender, RoutedEventArgs e)
        {
            tabscontrol.ActiveGrid.ShowGridLines = false;
            tabscontrol.ActiveGrid.InvalidateVisual();
        }

        void gridlines_Checked(object sender, RoutedEventArgs e)
        {
            tabscontrol.ActiveGrid.ShowGridLines = true;
            tabscontrol.ActiveGrid.InvalidateVisual();
        }

        private void copy_Click(object sender, RoutedEventArgs e)
        {
            tabscontrol.ActiveGrid.CopyPaste.Copy();
        }

        private void cut_Click(object sender, RoutedEventArgs e)
        {
            tabscontrol.ActiveGrid.CopyPaste.Cut();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            tabscontrol.ActiveGrid.CopyPaste.Paste();
        }

        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            Color backcolor = backgroundcolor.SelectedColor;
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    if (!style.Font.FontWeight.Equals(FontWeights.Bold))
                        style.Font.FontWeight = FontWeights.Bold;
                    else
                        style.Font.FontWeight = FontWeights.Normal;
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void Font_SelectionChanged_1(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (tabscontrol.ActiveGrid == null || (Font != null && Font.SelectedIndex < 0))
                return;
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            Color backcolor = backgroundcolor.SelectedColor;
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Font.FontFamily = new FontFamily(((sender as SfRibbonComboBox).SelectedItem as SfRibbonComboBoxItem).Tag.ToString());
                    //int selectedIndex = (sender as SfRibbonComboBox).SelectedIndex;
                    //style.Font.FontFamily = new FontFamily(((sender as SfRibbonComboBox).Items[selectedIndex] as SfRibbonComboBoxItem).Tag.ToString());
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            Color backcolor = backgroundcolor.SelectedColor;
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    if (style.Font.FontStyle != Windows.UI.Text.FontStyle.Italic)
                        style.Font.FontStyle = Windows.UI.Text.FontStyle.Italic;
                    else
                        style.Font.FontStyle = Windows.UI.Text.FontStyle.Normal;
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void FontSize_SelectionChanged_1(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (tabscontrol == null || tabscontrol.ActiveGrid == null || (FontSize_Combo != null && FontSize_Combo.SelectedIndex < 0))
                return;
            double size = double.Parse(FontSize_Combo.SelectedItem.ToString());
            //double size = double.Parse(FontSize_Combo.Items[FontSize_Combo.SelectedIndex].ToString());
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Font.FontSize = size;
                    tabscontrol.ActiveGrid.RowHeights[i] = size;
                }

                if (tabscontrol.ActiveGrid.RowHeights[i] < tabscontrol.ActiveGrid.DefaultRowHeight)
                    tabscontrol.ActiveGrid.RowHeights[i] = tabscontrol.ActiveGrid.DefaultRowHeight;
            }
            tabscontrol.ActiveGrid.Model.ResizeRowsToFit(Range, GridResizeToFitOptions.None);
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void TopAlign_click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.VerticalAlignment = VerticalAlignment.Top;
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void CenterverticalAlign_click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.VerticalAlignment = VerticalAlignment.Center;
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void BottomAlign_click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.VerticalAlignment = VerticalAlignment.Bottom;
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void CenterHorizantalAlign_click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.HorizontalAlignment = HorizontalAlignment.Center;
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void LeftAlign_click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.HorizontalAlignment = HorizontalAlignment.Left;
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void RightAlign_click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.HorizontalAlignment = HorizontalAlignment.Right;
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void IncreaseIndent_Click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    if (style.HorizontalAlignment == HorizontalAlignment.Right)
                    {
                        double pad = double.Parse(style.Padding.Right.ToString());
                        style.Padding = new Thickness(style.Padding.Left, style.Padding.Top, pad + 2, style.Padding.Bottom);
                    }
                    else
                    {
                        double pad = double.Parse(style.Padding.Left.ToString());
                        style.Padding = new Thickness(pad + 2, style.Padding.Top, style.Padding.Right, style.Padding.Bottom);
                    }
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void DecreaseIndent_Click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    if (style.HorizontalAlignment == HorizontalAlignment.Right)
                    {
                        double pad = double.Parse(style.Padding.Right.ToString());
                        pad = pad - 2;
                        if (pad < 2)
                            pad = 2;
                        style.Padding = new Thickness(style.Padding.Left, style.Padding.Top, pad, style.Padding.Bottom);
                    }
                    else
                    {
                        double pad = double.Parse(style.Padding.Left.ToString());
                        pad = pad - 2;
                        if (pad < 2)
                            pad = 2;
                        style.Padding = new Thickness(pad, style.Padding.Top, style.Padding.Right, style.Padding.Bottom);
                    }
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void TextWrapping_Click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    if (style.TextWrapping != TextWrapping.Wrap)
                    {
                        style.TextWrapping = TextWrapping.Wrap;
                        style.TextTrimming = TextTrimming.None;
                        tabscontrol.ActiveGrid.Model.ResizeRowsToFit(Range, GridResizeToFitOptions.None);
                    }
                    else
                    {
                        style.TextWrapping = TextWrapping.NoWrap;
                        style.TextTrimming = TextTrimming.WordEllipsis;
                        tabscontrol.ActiveGrid.RowHeights[i] = tabscontrol.ActiveGrid.DefaultRowHeight;
                    }
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void MergeandCenter_Click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            if (tabscontrol.ActiveGrid.SelectedRanges.Count < 1)
            {
                ShowErrorMessage("Please select any range");
                return;
            }
            var intersectedRange = tabscontrol.ActiveGrid.CoveredCells.Ranges.FirstOrDefault(range => range.IntersectsWith(Range));
            if (intersectedRange != null)
            {
                Unmerge_Cells();
                return;
            }
            tabscontrol.ActiveGrid.CoveredCells.Add(new CoveredCellInfo(Range.Top, Range.Left, Range.Bottom, Range.Right));
            var style = tabscontrol.ActiveGrid.Model[Range.Top, Range.Left];
            style.HorizontalAlignment = HorizontalAlignment.Center;
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void Mergeaccross_Click(object sender, RoutedEventArgs e)
        {
            Unmerge_Cells();
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            int left = Range.Top, right = Range.Bottom;
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                tabscontrol.ActiveGrid.CoveredCells.Add(new CoveredCellInfo(i, Range.Left, i, Range.Right));
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void Mergecells_Click(object sender, RoutedEventArgs e)
        {
            Unmerge_Cells();
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            tabscontrol.ActiveGrid.CoveredCells.Add(new CoveredCellInfo(Range.Top, Range.Left, Range.Bottom, Range.Right));
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void Unmergedcells_Click(object sender, RoutedEventArgs e)
        {
            Unmerge_Cells();
        }

        private void greaterthan(object sender, RoutedEventArgs e)
        {
            popup.Child = condition;
            condition.Tag = popup;
            condition.Height = 768;
            condition.Width = 1366;
            condition.Type = GridConditionType.GreaterThan;
            condition.DataContext = tabscontrol.ActiveGrid;
            popup.IsOpen = true;
        }

        private void lessthan(object sender, RoutedEventArgs e)
        {
            popup.Child = condition;
            condition.Tag = popup;
            condition.Height = 768;
            condition.Width = 1366;
            condition.Type = GridConditionType.LessThan;
            condition.DataContext = tabscontrol.ActiveGrid;
            popup.IsOpen = true;
        }

        private void between(object sender, RoutedEventArgs e)
        {
            ConditionalFormat condition = new ConditionalFormat();
            popup.Child = condition;
            condition.Tag = popup;
            condition.Height = 768;
            condition.Width = 1366;
            condition.Type = GridConditionType.Between;
            condition.DataContext = tabscontrol.ActiveGrid;
            popup.IsOpen = true;
        }

        private void equalto(object sender, RoutedEventArgs e)
        {
            ConditionalFormat condition = new ConditionalFormat();
            popup.Child = condition;
            condition.Tag = popup;
            condition.Height = 768;
            condition.Width = 1366;
            condition.Type = GridConditionType.Equal;
            condition.DataContext = tabscontrol.ActiveGrid;
            popup.IsOpen = true;
        }

        private void textcontains(object sender, RoutedEventArgs e)
        {
            ConditionalFormat condition = new ConditionalFormat();
            popup.Child = condition;
            condition.Tag = popup;
            condition.Height = 768;
            condition.Width = 1366;
            condition.Type = GridConditionType.Contains;
            condition.DataContext = tabscontrol.ActiveGrid;
            popup.IsOpen = true;
        }

        private void Adateoccurs(object sender, RoutedEventArgs e)
        {
            dateoccuring date = new dateoccuring();
            popup.Child = date;
            date.Tag = popup;
            date.Height = 768;
            date.Width = 1366;
            date.DataContext = tabscontrol.ActiveGrid;
            popup.IsOpen = true;
        }

        private void bottomborder_click(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Borders.Bottom = new Pen(new SolidColorBrush(Colors.Black), 2);
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void TopBorderClick(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Borders.Top = new Pen(new SolidColorBrush(Colors.Black), 2);
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void LeftBorderClick(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Borders.Left = new Pen(new SolidColorBrush(Colors.Black), 2);
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void RightBorderClick(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Borders.Right = new Pen(new SolidColorBrush(Colors.Black), 2);
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void NoBorderClick(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Borders.ResetAll();
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void AllBorderClick(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Borders.Right = new Pen(new SolidColorBrush(Colors.Black), 2);
                    style.Borders.Left = new Pen(new SolidColorBrush(Colors.Black), 2);
                    style.Borders.Top = new Pen(new SolidColorBrush(Colors.Black), 2);
                    style.Borders.Bottom = new Pen(new SolidColorBrush(Colors.Black), 2);
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void OutsideBorderClick(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                var style = tabscontrol.ActiveGrid.Model[i, Range.Left];
                var style1 = tabscontrol.ActiveGrid.Model[i, Range.Right];
                style.Borders.Left = new Pen(new SolidColorBrush(Colors.Black), 2);
                style1.Borders.Right = new Pen(new SolidColorBrush(Colors.Black), 2);
            }
            for (int j = Range.Left; j <= Range.Right; j++)
            {
                var style = tabscontrol.ActiveGrid.Model[Range.Top, j];
                var style1 = tabscontrol.ActiveGrid.Model[Range.Bottom, j];
                style.Borders.Top = new Pen(new SolidColorBrush(Colors.Black), 2);
                style1.Borders.Bottom = new Pen(new SolidColorBrush(Colors.Black), 2);
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void ThickBoxBorder(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Borders.Right = new Pen(new SolidColorBrush(Colors.Black), 4);
                    style.Borders.Left = new Pen(new SolidColorBrush(Colors.Black), 4);
                    style.Borders.Top = new Pen(new SolidColorBrush(Colors.Black), 4);
                    style.Borders.Bottom = new Pen(new SolidColorBrush(Colors.Black), 4);
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void ThickBottomBorder(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Borders.Bottom = new Pen(new SolidColorBrush(Colors.Black), 4);
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void TopandBottomBorder(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Borders.Bottom = new Pen(new SolidColorBrush(Colors.Black), 2);
                    style.Borders.Top = new Pen(new SolidColorBrush(Colors.Black), 2);
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void TopandThickBottomBorder(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    style.Borders.Bottom = new Pen(new SolidColorBrush(Colors.Black), 4);
                    style.Borders.Top = new Pen(new SolidColorBrush(Colors.Black), 2);
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void cellformatcombo_SelectionChanged_1(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (tabscontrol == null || tabscontrol.ActiveGrid == null || (cellformatcombo != null && cellformatcombo.SelectedIndex < 0))
                return;
            var format = (this.cellformatcombo.SelectedItem as SfRibbonComboBoxItem).Content.ToString();
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    switch (format)
                    {
                        case "General":
                            style.Format = null;
                            break;
                        case "Text":
                            style.Format = null;
                            break;
                        case "Number":
                            style.Format = "#,##.00";
                            break;
                        case "Currency":
                            style.Format = "$ #,##.00";
                            break;
                        case "Shortdate":
                            style.Format = "dd/MM/yy";
                            break;
                        case "Longdate":
                            style.Format = "dddd,MMMM dd,yyyy";
                            break;
                        case "Time":
                            style.Format = "h:mm:ss tt";
                            break;
                        case "Percentage":
                            style.Format = "#0.##%";
                            break;
                        case "Scientific":
                            style.Format = "0.00###E+00";
                            break;
                    }
                    ValidateFormat(style);
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void ClearAll(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;

            if (Range.IsTable)
            {
                tabscontrol.ActiveGrid.Model.ClearStyles();
                tabscontrol.ActiveGrid.InvalidateCells();
            }
            else
            {
                Range = GetExpandRange(Range);
                for (int i = Range.Top; i <= Range.Bottom; i++)
                {
                    for (int j = Range.Left; j <= Range.Right; j++)
                    {
                        RowColumnIndex rowcol = new RowColumnIndex(i, j);
                        tabscontrol.ActiveGrid.Model.ClearStyle(rowcol);
                    }
                }
                tabscontrol.ActiveGrid.InvalidateCell(Range);
            }
        }

        private void ClearFromats(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    var cellvalue = style.CellValue;
                    tabscontrol.ActiveGrid.Model.ClearStyle(tabscontrol.ActiveGrid.Model[i, j].CellRowColumnIndex);
                    tabscontrol.ActiveGrid.Model[i, j].CellValue = cellvalue;
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void ClearContents(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    tabscontrol.ActiveGrid.Model[i, j].ResetCellValue();
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void FreezePanes(object sender, RoutedEventArgs e)
        {
            var rowcolindex = tabscontrol.ActiveGrid.CurrentCell.CellRowColumnIndex;
            if (rowcolindex.IsEmpty || rowcolindex.RowIndex < 0 || rowcolindex.ColumnIndex < 0)
            {
                ShowErrorMessage("Select any Cell...");
                return;
            }
            if (rowcolindex.ColumnIndex != 0 && rowcolindex.RowIndex != 0)
            {
                tabscontrol.ActiveGrid.FrozenRows = rowcolindex.RowIndex;
                tabscontrol.ActiveGrid.FrozenColumns = rowcolindex.ColumnIndex;
                tabscontrol.ActiveGrid.InvalidateVisual();
                frezzepanes = true;
            }
        }

        private void FreezeTopRow(object sender, RoutedEventArgs e)
        {
            tabscontrol.ActiveGrid.FrozenRows = tabscontrol.ActiveGrid.ScrollRows.ScrollLineIndex + 1;
            tabscontrol.ActiveGrid.FrozenColumns = tabscontrol.ActiveGrid.HeaderColumns;
            tabscontrol.ActiveGrid.InvalidateVisual();
            frezzepanes = true;
        }

        private void FreezeFirstColumn(object sender, RoutedEventArgs e)
        {
            tabscontrol.ActiveGrid.FrozenColumns = tabscontrol.ActiveGrid.ScrollColumns.ScrollLineIndex + 1;
            tabscontrol.ActiveGrid.FrozenRows = tabscontrol.ActiveGrid.HeaderRows;
            tabscontrol.ActiveGrid.InvalidateVisual();
            frezzepanes = true;

        }

        private void UnFreezePanes(object sender, RoutedEventArgs e)
        {
            if (frezzepanes)
            {
                var rowcolindex = tabscontrol.ActiveGrid.CurrentCell.CellRowColumnIndex;
                tabscontrol.ActiveGrid.FrozenRows = tabscontrol.ActiveGrid.HeaderRows;
                tabscontrol.ActiveGrid.FrozenColumns = tabscontrol.ActiveGrid.HeaderColumns;
                frezzepanes = false;
                tabscontrol.ActiveGrid.InvalidateVisual();
            }
        }

        private void HideRow(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                tabscontrol.ActiveGrid.RowHeights.SetHidden(i, i, true);
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void HideColumn(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            for (int i = Range.Left; i <= Range.Right; i++)
            {
                tabscontrol.ActiveGrid.ColumnWidths.SetHidden(i, i, true);
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void UnHideRow(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                tabscontrol.ActiveGrid.RowHeights.SetHidden(i, i, false);
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);

        }

        private void UnHideColumn(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            for (int i = Range.Left; i <= Range.Right; i++)
            {
                tabscontrol.ActiveGrid.ColumnWidths.SetHidden(i, i, false);
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void insertRow(object sender, RoutedEventArgs e)
        {
            var Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            if (Range.IsEmpty)
            {
                ShowErrorMessage("Select any Range...");
                return;
            }
            else if (Range.IsRows || Range.IsCells)
                tabscontrol.ActiveGrid.Model.InsertRows(Range.Top, Range.Height);
            else
                ShowErrorMessage("Select any range of cells/rows");
        }

        private void insertColumn(object sender, RoutedEventArgs e)
        {
            var Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            if (Range.IsEmpty)
            {
                ShowErrorMessage("Select any Range...");
                return;
            }
            else if (Range.IsCols || Range.IsCells)
                tabscontrol.ActiveGrid.Model.InsertColumns(Range.Left, Range.Width);
            else
                ShowErrorMessage("Select any range of cells/columns");
        }

        private void deleteRow(object sender, RoutedEventArgs e)
        {
            var Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            if (Range.IsEmpty)
            {
                ShowErrorMessage("Select any Range...");
                return;
            }
            else if (Range.IsRows || Range.IsCells)
                tabscontrol.ActiveGrid.Model.RemoveRows(Range.Top, Range.Height);
            else
                ShowErrorMessage("Select any range of cells/rows");
        }

        private void deleteColumn(object sender, RoutedEventArgs e)
        {
            var Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            if (Range.IsEmpty)
            {
                ShowErrorMessage("Select any Range...");
                return;
            }
            else if (Range.IsCols || Range.IsCells)
                tabscontrol.ActiveGrid.Model.RemoveColumns(Range.Left, Range.Width);
            else
                ShowErrorMessage("Select any range of cells/columns");
        }

        private void rowheight(object sender, RoutedEventArgs e)
        {
            rowheight row = new rowheight();
            popup.Child = row;
            row.Tag = popup;
            row.Height = 768;
            row.Width = 1366;
            row.IsRow = true;
            row.DataContext = tabscontrol.ActiveGrid;
            popup.IsOpen = true;
        }

        private void ColumnWidths(object sender, RoutedEventArgs e)
        {
            rowheight row = new rowheight();
            popup.Child = row;
            row.Tag = popup;
            row.Height = 768;
            row.Width = 1366;
            row.IsRow = false;
            row.DataContext = tabscontrol.ActiveGrid;
            popup.IsOpen = true;
        }

        private void AutoRowFit(object sender, RoutedEventArgs e)
        {
            var Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            tabscontrol.ActiveGrid.Model.ResizeRowsToFit(Range, GridResizeToFitOptions.None);
            tabscontrol.ActiveGrid.InvalidateVisual();
        }

        private void AutoColumnFit(object sender, RoutedEventArgs e)
        {
            var Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            tabscontrol.ActiveGrid.Model.ResizeColumnsToFit(Range, GridResizeToFitOptions.None);
            tabscontrol.ActiveGrid.InvalidateCell(tabscontrol.ActiveGrid.CurrentCell.RowIndex, tabscontrol.ActiveGrid.CurrentCell.ColumnIndex);
        }

        private void DecreseDecimal(object sender, RoutedEventArgs e)
        {
            var rowcol = tabscontrol.ActiveGrid.CurrentCell.CellRowColumnIndex;
            var style = tabscontrol.ActiveGrid.Model[rowcol.RowIndex, rowcol.ColumnIndex];
            style.Format = IncreaseDecrease_Decimals(rowcol, false);
            tabscontrol.ActiveGrid.InvalidateCell(rowcol.RowIndex, rowcol.ColumnIndex);
        }

        private void IncreseDecimal(object sender, RoutedEventArgs e)
        {
            var rowcol = tabscontrol.ActiveGrid.CurrentCell.CellRowColumnIndex;
            var style = tabscontrol.ActiveGrid.Model[rowcol.RowIndex, rowcol.ColumnIndex];
            style.Format = IncreaseDecrease_Decimals(rowcol, true);
            tabscontrol.ActiveGrid.InvalidateCell(rowcol.RowIndex, rowcol.ColumnIndex);
        }

        private void clearfromselected(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    if (tabscontrol.ActiveGrid.Model[i, j].HasConditionalFormats)
                    {
                        for (int k = 0; k < tabscontrol.ActiveGrid.Model[i, j].ConditionalFormats.Count; k++)
                        {
                            tabscontrol.ActiveGrid.Model[i, j].ConditionalFormats.RemoveAt(k);
                        }
                    }
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void Clearfromthesheet(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= tabscontrol.ActiveGrid.RowCount; i++)
            {
                for (int j = 1; j <= tabscontrol.ActiveGrid.ColumnCount; j++)
                {
                    if (tabscontrol.ActiveGrid.Model[i, j].HasConditionalFormats)
                    {
                        for (int k = 0; k < tabscontrol.ActiveGrid.Model[i, j].ConditionalFormats.Count; k++)
                        {
                            tabscontrol.ActiveGrid.Model[i, j].ConditionalFormats.RemoveAt(k);
                        }
                    }
                }
            }
            tabscontrol.ActiveGrid.InvalidateCells();
        }

        private void incresefontsize(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    double size = style.Font.FontSize;
                    var index = fontsizes.IndexOf(size);
                    if (index < fontsizes.Count - 1)
                        style.Font.FontSize = fontsizes[index + 1];
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private void decresefontsize(object sender, RoutedEventArgs e)
        {
            GridRangeInfo Range = tabscontrol.ActiveGrid.SelectedRanges.ActiveRange;
            Range = GetExpandRange(Range);
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var style = tabscontrol.ActiveGrid.Model[i, j];
                    double size = style.Font.FontSize;
                    var index = fontsizes.IndexOf(size);
                    if (index >= 1)
                        style.Font.FontSize = fontsizes[index - 1];
                }
            }
            tabscontrol.ActiveGrid.InvalidateCell(Range);
        }

        private async void import(object sender, RoutedEventArgs e)
        {
            await Load();
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            ribbon.CloseBackStage();
            tabscontrol.Gridload(1);
        }

        private async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            await Load();
            ribbon.CloseBackStage();
        }

        #endregion

        #region Icon Path Properties

        private string boldIconPath;
        public string BoldIconPath
        {
            get { return GetIconPath(ref boldIconPath, "bold"); }
        }

        private string italicIconPath;
        public string ItalicIconPath
        {
            get { return GetIconPath(ref italicIconPath, "Italic"); }
        }

        private string underlineIconPath;
        public string UnderlineIconPath
        {
            get { return GetIconPath(ref underlineIconPath, "Underline"); }
        }

        private string bottomBorderIconPath;
        public string BottomBorderIconPath
        {
            get { return GetIconPath(ref bottomBorderIconPath, "BottomBorder"); }
        }

        private string topBorderIconPath;
        public string TopBorderIconPath
        {
            get { return GetIconPath(ref topBorderIconPath, "TopBorder"); }
        }

        private string leftBorderIconPath;
        public string LeftBorderIconPath
        {
            get { return GetIconPath(ref leftBorderIconPath, "LeftBorder"); }
        }

        private string rightBorderIconPath;
        public string RightBorderIconPath
        {
            get { return GetIconPath(ref rightBorderIconPath, "RightBorder"); }
        }

        private string allBorderIconPath;
        public string AllBorderIconPath
        {
            get { return GetIconPath(ref allBorderIconPath, "all-border"); }
        }

        private string outsideBorderIconPath;
        public string OutsideBorderIconPath
        {
            get { return GetIconPath(ref outsideBorderIconPath, "OutSideBorder"); }
        }

        private string thickBoxBorderIconPath;
        public string ThickBoxBorderIconPath
        {
            get { return GetIconPath(ref thickBoxBorderIconPath, "ThickBoxBorder"); }
        }

        private string thickBottomBorderIconPath;
        public string ThickBottomBorderIconPath
        {
            get { return GetIconPath(ref thickBottomBorderIconPath, "ThickBottomBorder"); }
        }

        private string topAndBottomBorderIconPath;
        public string TopAndBottomBorderIconPath
        {
            get { return GetIconPath(ref topAndBottomBorderIconPath, "TopAndBottomBorder"); }
        }

        private string topAndThickBottomBorderIconPath;
        public string TopAndThickBottomBorderIconPath
        {
            get { return GetIconPath(ref topAndThickBottomBorderIconPath, "TopAndThickBottomBorder"); }
        }

        private string noBorderIconPath;
        public string NoBorderIconPath
        {
            get { return GetIconPath(ref noBorderIconPath, "NoBorder"); }
        }

        private string fillColorIconPath;
        public string FillColorIconPath
        {
            get { return GetIconPath(ref fillColorIconPath, "fillcolor"); }
        }

        private string fontColorIconPath;
        public string FontColorIconPath
        {
            get { return GetIconPath(ref fontColorIconPath, "fontcolor"); }
        }

        private string topAlignIconPath;
        public string TopAlignIconPath
        {
            get { return GetIconPath(ref topAlignIconPath, "TopAlign"); }
        }

        private string middleAlignIconPath;
        public string MiddleAlignIconPath
        {
            get { return GetIconPath(ref middleAlignIconPath, "MiddleAlign"); }
        }

        private string bottomAlignIconPath;
        public string BottomAlignIconPath
        {
            get { return GetIconPath(ref bottomAlignIconPath, "BottomAlign"); }
        }

        private string leftAlignIconPath;
        public string LeftAlignIconPath
        {
            get { return GetIconPath(ref leftAlignIconPath, "LeftAlign"); }
        }

        private string centerAlignIconPath;
        public string CenterAlignIconPath
        {
            get { return GetIconPath(ref centerAlignIconPath, "CenterAlign"); }
        }

        private string rightAlignIconPath;
        public string RightAlignIconPath
        {
            get { return GetIconPath(ref rightAlignIconPath, "RightAlign"); }
        }

        private string mergeAndCenterIconPath;
        public string MergeAndCenterIconPath
        {
            get { return GetIconPath(ref mergeAndCenterIconPath, "MergeandCenter"); }
        }

        private string wrapTextIconPath;
        public string WrapTextIconPath
        {
            get { return GetIconPath(ref wrapTextIconPath, "WrapText"); }
        }

        private string insertRowsIconPath;
        public string InsertRowsIconPath
        {
            get { return GetIconPath(ref insertRowsIconPath, "insertrows"); }
        }

        private string insertColsIconPath;
        public string InsertColsIconPath
        {
            get { return GetIconPath(ref insertColsIconPath, "insertcols"); }
        }

        private string deleteRowsIconPath;
        public string DeleteRowsIconPath
        {
            get { return GetIconPath(ref deleteRowsIconPath, "deleterows"); }
        }

        private string deleteColsIconPath;
        public string DeleteColsIconPath
        {
            get { return GetIconPath(ref deleteColsIconPath, "deletecols"); }
        }

        private string gridlineColorIconPath;
        public string GridlineColorIconPath
        {
            get { return GetIconPath(ref gridlineColorIconPath, "fillcolor"); }
        }

        private string freezePanesIconPath;
        public string FreezePanesIconPath
        {
            get { return GetIconPath(ref freezePanesIconPath, "freezepanes"); }
        }

        private string freezeTopRowIconPath;
        public string FreezeTopRowIconPath
        {
            get { return GetIconPath(ref freezeTopRowIconPath, "freezetoprow"); }
        }

        private string freezeFirstColumnIconPath;
        public string FreezeFirstColumnIconPath
        {
            get { return GetIconPath(ref freezeFirstColumnIconPath, "freezefirstcolumn"); }
        }

        private string highlightCellRulesIconPath;
        public string HighlightCellRulesIconPath
        {
            get { return GetIconPath(ref highlightCellRulesIconPath, "high-light-cell-rules"); }
        }

        private string clearRulesIconPath;
        public string ClearRulesIconPath
        {
            get { return GetIconPath(ref clearRulesIconPath, "clear-rules"); }
        }

        private string pasteIconPath;
        public string PasteIconPath
        {
            get { return GetIconPath(ref pasteIconPath, "paste"); }
        }

        private string cutIconPath;
        public string CutIconPath
        {
            get { return GetIconPath(ref cutIconPath, "cut"); }
        }

        private string copyIconPath;
        public string CopyIconPath
        {
            get { return GetIconPath(ref copyIconPath, "copy"); }
        }

        private string increaseFontSizeIconPath;
        public string IncreaseFontSizeIconPath
        {
            get { return GetIconPath(ref increaseFontSizeIconPath, "increasefontsize"); }
        }

        private string decreaseFontSizeIconPath;
        public string DecreaseFontSizeIconPath
        {
            get { return GetIconPath(ref decreaseFontSizeIconPath, "decreasefontsize"); }
        }

        private string increaseDecimalIconPath;
        public string IncreaseDecimalIconPath
        {
            get { return GetIconPath(ref increaseDecimalIconPath, "increasedecimal"); }
        }

        private string decreaseDecimalIconPath;
        public string DecreaseDecimalIconPath
        {
            get { return GetIconPath(ref decreaseDecimalIconPath, "decreasedecimal"); }
        }

        private string conditionalFormattingIconPath;
        public string ConditionalFormattingIconPath
        {
            get { return GetIconPath(ref conditionalFormattingIconPath, "conditional-formatting"); }
        }

        private string greaterThanIconPath;
        public string GreaterThanIconPath
        {
            get { return GetIconPath(ref greaterThanIconPath, "greater-than"); }
        }

        private string lessThanIconPath;
        public string LessThanIconPath
        {
            get { return GetIconPath(ref lessThanIconPath, "less-than"); }
        }

        private string betweenIconPath;
        public string BetweenIconPath
        {
            get { return GetIconPath(ref betweenIconPath, "between"); }
        }

        private string equalToIconPath;
        public string EqualToIconPath
        {
            get { return GetIconPath(ref equalToIconPath, "equal-to"); }
        }

        private string textThatContainsIconPath;
        public string TextThatContainsIconPath
        {
            get { return GetIconPath(ref textThatContainsIconPath, "text-that-contains"); }
        }

        private string dateOccurredIconPath;
        public string DateOccurredIconPath
        {
            get { return GetIconPath(ref dateOccurredIconPath, "date-occurred"); }
        }

        private string mergeAndAcrossIconPath;
        public string MergeAndAcrossIconPath
        {
            get { return GetIconPath(ref mergeAndAcrossIconPath, "merge-across"); }
        }

        private string formatIconPath;
        public string FormatIconPath
        {
            get { return GetIconPath(ref formatIconPath, "format"); }
        }

        private string rowHeightIconPath;
        public string RowHeightIconPath
        {
            get { return GetIconPath(ref rowHeightIconPath, "rowheight"); }
        }

        private string columnWidthIconPath;
        public string ColumnWidthIconPath
        {
            get { return GetIconPath(ref columnWidthIconPath, "columnwidth"); }
        }

        private string insertIconPath;
        public string InsertIconPath
        {
            get { return GetIconPath(ref insertIconPath, "insert"); }
        }

        private string deleteIconPath;
        public string DeleteIconPath
        {
            get { return GetIconPath(ref deleteIconPath, "delete"); }
        }

        private string clearIconPath;
        public string ClearIconPath
        {
            get { return GetIconPath(ref clearIconPath, "clear"); }
        }

        private string clearAllIconPath;
        public string ClearAllIconPath
        {
            get { return GetIconPath(ref clearAllIconPath, "clear-all"); }
        }

        private string clearFormatsIconPath;
        public string ClearFormatsIconPath
        {
            get { return GetIconPath(ref clearFormatsIconPath, "clear-formats"); }
        }

        private string increaseIndentIconPath;
        public string IncreaseIndentIconPath
        {
            get { return GetIconPath(ref increaseIndentIconPath, "increaseindent"); }
        }

        private string decreaseIndentIconPath;
        public string DecreaseIndentIconPath
        {
            get { return GetIconPath(ref decreaseIndentIconPath, "decreaseindent"); }
        }

        private string mergeCellsIconPath;
        public string MergeCellsIconPath
        {
            get { return GetIconPath(ref mergeCellsIconPath, "merge-cells"); }
        }

        private string unMergeCellsIconPath;
        public string UnMergeCellsIconPath
        {
            get { return GetIconPath(ref unMergeCellsIconPath, "unmerge-cells"); }
        }

        //private string duplicateValuesIconPath;
        //public string DuplicateValuesIconPath
        //{
        //    get { return GetIconPath(ref duplicateValuesIconPath, "duplicate-values"); }
        //}

        /// <summary>
        /// Loads and caches the icon path for the given icon name.
        /// If the path is not already initialized, it constructs the path using PathHelper,
        /// assigns it to the provided field (using ref), and returns it.
        ///</summary>
        private string GetIconPath(ref string iconPath, string iconName)
        {
            if (string.IsNullOrEmpty(iconPath))
                iconPath = PathHelper.GetResourcePath($"Icons/{iconName}.png");
            return iconPath;
        }
        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            UnWireEvents();
            if (ribbon != null)
                ribbon.Dispose();
            if (tabscontrol != null)
            {
                if (tabscontrol.Gridcollection != null)
                {
                    foreach (var grid in tabscontrol.Gridcollection)
                    {
                        grid.Dispose();
                    }
                    tabscontrol.Gridcollection.Clear();
                }
                tabscontrol.Dispose();
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion
    }
}
