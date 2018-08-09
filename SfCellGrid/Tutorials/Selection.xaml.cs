
using Common;
using Syncfusion.UI.Xaml.CellGrid;
using Syncfusion.UI.Xaml.CellGrid.Helpers;
using Syncfusion.UI.Xaml.CellGrid.Styles;
using Syncfusion.UI.Xaml.Grid.ScrollAxis;
using Syncfusion.UI.Xaml.Grid.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class Selection : SampleLayout, IDisposable
    {

        #region Private Fields

        Brush red = new SolidColorBrush(Colors.Red);
        Brush yellow = new SolidColorBrush(Colors.Yellow);
        List<ColorList> colorlist;
        int[] thickness = { 0, 1, 2, 3, 4, 5 };
        bool disableCell = false;

        #endregion

        #region Constructor

        public Selection()
        {
            this.InitializeComponent();
            InitializeGrid();
        }

        #endregion

        #region Properties

        public List<ColorList> ColorList
        {
            get { return colorlist; }
            set { colorlist = value; }
        }

        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            this.InitializeColorList();
            this.cellgrid.RowCount = 1000;
            this.cellgrid.ColumnCount = 1000;
            this.cellgrid.AllowExcelLikeKeyNavigation = true;
            // Default Row Height and Default Column Width is reduced for Mobile View
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                cellgrid.DefaultColumnWidth = 80;
                cellgrid.DefaultRowHeight = 20;
                // Header Column Width is reduced for Mobile View
                cellgrid.ColumnWidths[0] = 50;
            }
            WireEvents();

            this.selectionborderCombo.ItemsSource = colorlist;
            this.selectionborderCombo.SelectedItem = colorlist[6];
            this.selectionborderCombo.DisplayMemberPath = "Name";

            this.selectionbrushCombo.ItemsSource = colorlist;
            this.selectionbrushCombo.SelectedItem = colorlist[11];
            this.selectionbrushCombo.DisplayMemberPath = "Name";

            this.selectionborderThick.ItemsSource = thickness.ToList();
            this.selectionborderThick.SelectedItem = thickness[2];

            cellgrid.CurrentCell.MoveCurrentCell(5, 3);
            cellgrid.SelectionController.AddSelection(GridRangeInfo.Cells(3, 1, 6, 3));
            cellgrid.SelectionController.AddSelection(GridRangeInfo.Cells(5, 3, 8, 5));
        }

        private void InitializeColorList()
        {
            this.colorlist = new List<ColorList>();

            this.ColorList.Add(new ColorList("AliceBlue", Windows.UI.Colors.AliceBlue));
            this.ColorList.Add(new ColorList("AntiqueWhite", Windows.UI.Colors.AntiqueWhite));
            this.ColorList.Add(new ColorList("Aqua", Windows.UI.Colors.Aqua));
            this.ColorList.Add(new ColorList("Aquamarine", Windows.UI.Colors.Aquamarine));
            this.ColorList.Add(new ColorList("BlanchedAlmond", Windows.UI.Colors.BlanchedAlmond));
            this.ColorList.Add(new ColorList("Black", Windows.UI.Colors.Black));
            this.ColorList.Add(new ColorList("Blue", Windows.UI.Colors.Blue));
            this.ColorList.Add(new ColorList("BlueViolet", Windows.UI.Colors.BlueViolet));
            this.ColorList.Add(new ColorList("Brown", Windows.UI.Colors.Brown));
            this.ColorList.Add(new ColorList("BurlyWood", Windows.UI.Colors.BurlyWood));
            this.ColorList.Add(new ColorList("Cyan", Windows.UI.Colors.Cyan));
            this.ColorList.Add(new ColorList("DarkBlue", Windows.UI.Colors.DarkBlue));
            this.ColorList.Add(new ColorList("DarkCyan", Windows.UI.Colors.DarkCyan));
            this.ColorList.Add(new ColorList("DarkGoldenrod", Windows.UI.Colors.DarkGoldenrod));
            this.ColorList.Add(new ColorList("DarkGray", Windows.UI.Colors.DarkGray));
            this.ColorList.Add(new ColorList("DarkGreen", Windows.UI.Colors.DarkGreen));
            this.ColorList.Add(new ColorList("DarkOliveGreen", Windows.UI.Colors.DarkOliveGreen));
            this.ColorList.Add(new ColorList("Gold", Windows.UI.Colors.Gold));
            this.ColorList.Add(new ColorList("Gray", Windows.UI.Colors.Gray));
            this.colorlist.Add(new ColorList("Red", Colors.Red));
            this.colorlist.Add(new ColorList("Purple", Colors.Purple));
            this.colorlist.Add(new ColorList("YellowGreen", Colors.YellowGreen));
            this.colorlist.Add(new ColorList("Pink", Colors.Pink));
            this.colorlist.Add(new ColorList("Orange", Colors.Orange));
        }

        private void WireEvents()
        {
            this.cellgrid.Model.QueryCellInfo += Model_QueryCellInfo;
            this.cellgrid.CurrentCellBeginEdit += cellgrid_CurrentCellBeginEdit;
            this.cellgrid.CurrentCellEndEdit += cellgrid_CurrentCellEndEdit;
            this.cellgrid.CurrentCellValidating += cellgrid_CurrentCellValidating;
            this.cellgrid.CurrentCellValidated += cellgrid_CurrentCellValidated;
            this.cellgrid.CurrentCellActivating += cellgrid_CurrentCellActivating;
            this.cellgrid.CurrentCellActivated += cellgrid_CurrentCellActivated;
            this.cellgrid.SelectionChanging += cellgrid_SelectionChanging;
            this.cellgrid.SelectionChanged += cellgrid_SelectionChanged;
            this.cellgrid.CellClick += Cellgrid_CellClick;
        }

        private void UnWireEvents()
        {
            this.cellgrid.Model.QueryCellInfo -= Model_QueryCellInfo;
            this.cellgrid.CurrentCellBeginEdit -= cellgrid_CurrentCellBeginEdit;
            this.cellgrid.CurrentCellEndEdit -= cellgrid_CurrentCellEndEdit;
            this.cellgrid.CurrentCellValidating -= cellgrid_CurrentCellValidating;
            this.cellgrid.CurrentCellValidated -= cellgrid_CurrentCellValidated;
            this.cellgrid.CurrentCellActivating -= cellgrid_CurrentCellActivating;
            this.cellgrid.CurrentCellActivated -= cellgrid_CurrentCellActivated;
            this.cellgrid.SelectionChanging -= cellgrid_SelectionChanging;
            this.cellgrid.SelectionChanged -= cellgrid_SelectionChanged;
            this.cellgrid.CellClick -= Cellgrid_CellClick;
        }


        #endregion

        #region Events

        private void Cellgrid_CellClick(object sender, GridCellClickEventArgs args)
        {
            eventsTxtBox.Text += "(" + args.RowIndex + "," + args.ColumnIndex + ")" + "\nCell Clicked";
        }

        void Model_QueryCellInfo(object sender, GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex == 0 && e.Cell.ColumnIndex == 0)
                return;
            if (e.Cell.RowIndex == 0)
            {
                e.Style.CellValue = e.Cell.ColumnIndex;
                e.Style.VerticalAlignment = VerticalAlignment.Center;
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
            }
            else if (e.Cell.ColumnIndex == 0)
            {
                e.Style.CellValue = e.Cell.RowIndex;
                e.Style.VerticalAlignment = VerticalAlignment.Center;
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
            }
            else
            {
                e.Style.CellValue = "R" + e.Cell.RowIndex + " C" + e.Cell.ColumnIndex;
            }
        }

        void cellgrid_SelectionChanged(object sender, Syncfusion.UI.Xaml.CellGrid.Helpers.SelectionChangedEventArgs args)
        {
            eventsTxtBox.Text += "\nSelection changed";
        }

        void cellgrid_SelectionChanging(object sender, SelectionChangingEventArgs args)
        {
            if (disableCell && args.ActiveRange.Contains(GridRangeInfo.Cell(3, 3)))
                args.Cancel = true;

            eventsTxtBox.Text += "\nSelection changing";
        }

        void cellgrid_CurrentCellActivated(object sender, CurrentCellActivatedEventArgs args)
        {
            eventsTxtBox.Text += "\nCurrent Cell activated : " + args.CurrentRowColumnIndex.ToString();
        }

        void cellgrid_CurrentCellActivating(object sender, CurrentCellActivatingEventArgs args)
        {
            eventsTxtBox.Text = "\nCurrent Cell activating : " + args.CurrentRowColumnIndex.ToString();

        }

        void cellgrid_CurrentCellValidated(object sender, CurrentCellValidatedEventArgs args)
        {
            eventsTxtBox.Text += "\nCurrent Cell validated";

        }

        void cellgrid_CurrentCellValidating(object sender, CurrentCellValidatingEventArgs args)
        {
            eventsTxtBox.Text += "\nCurrent cell validating";
        }

        void cellgrid_CurrentCellEndEdit(object sender, CurrentCellEndEditEventArgs args)
        {
            eventsTxtBox.Text += "\ncurrentcell end editing";
        }

        void cellgrid_CurrentCellBeginEdit(object sender, CurrentCellBeginEditEventArgs args)
        {
            eventsTxtBox.Text += "\ncurrentcell begin editing";
        }

        private void selectionchkBox_Click(object sender, RoutedEventArgs e)
        {
            var chkbox = sender as CheckBox;
            this.cellgrid.AllowSelection = (bool)chkbox.IsChecked;
            this.cellgrid.InvalidateSelection();
        }

        private void editingchkbox_Click(object sender, RoutedEventArgs e)
        {
            var chkbox = sender as CheckBox;
            this.cellgrid.AllowEditing = (bool)chkbox.IsChecked;
        }

        private void selectionbrushCombo_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            var brush = new SolidColorBrush(((sender as ComboBox).SelectedItem as ColorList).Color);
            this.cellgrid.SelectionBrush = brush;
            this.cellgrid.InvalidateSelection();
        }

        private void selectionborderCombo_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            var brush = new SolidColorBrush(((sender as ComboBox).SelectedItem as ColorList).Color);
            this.cellgrid.SelectionBorderBrush = brush;
            this.cellgrid.InvalidateSelection();
        }

        private void selectionborderThick_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            var thick = selectionborderThick.SelectedItem;
            double d = 0;
            if (thick != null && Double.TryParse(thick.ToString(), out d))
            {
                this.cellgrid.SelectionBorderThickness = d;
                this.cellgrid.InvalidateSelection();
            }
        }

        private void dontselectchkbox_Click(object sender, RoutedEventArgs e)
        {
            var chkbox = sender as CheckBox;
            disableCell = (bool)chkbox.IsChecked;
        }

        private void autoscrollchkbox_Click(object sender, RoutedEventArgs e)
        {
            var chkbox = sender as CheckBox;
            this.cellgrid.AutoScroller.IsEnabled = (bool)chkbox.IsChecked;
        }

        private void clearEventsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.eventsTxtBox.Text = string.Empty;
        }

        private void allowKeyNavigation_Click(object sender, RoutedEventArgs e)
        {
            cellgrid.AllowExcelLikeKeyNavigation = (bool)allowKeyNavigation.IsChecked;
        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            UnWireEvents();
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

    #region Helper Class

    public class ColorList
    {
        public ColorList(string name, Color clr)
        {
            this.Name = name;
            this.Color = clr;
        }
        public string Name { get; set; }
        public Color Color { get; set; }
    }

    #endregion
}
