using Common;
using Syncfusion.UI.Xaml.CellGrid;
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
    public sealed partial class FloatingCells : SampleLayout, IDisposable
    {
        #region Constructor

        public FloatingCells()
        {
            this.InitializeComponent();
            InitGrid();
        }

        #endregion

        #region Private Methods

        private void InitGrid()
        {
            this.grid.RowCount = 25;
            this.grid.ColumnCount = 10;

            this.grid.DefaultColumnWidth = 100;
            this.grid.DefaultRowHeight = 30;

            // Header Column Width reduced for Mobile View
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                grid.ColumnWidths[0] = 50;

            grid.Model.TableStyle.TextWrapping = TextWrapping.NoWrap;
            this.grid.AllowFloatingCell = true;
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                this.grid.CoveredCells.Add(new CoveredCellInfo(1, 1, 2, 5));
            else
                this.grid.CoveredCells.Add(new CoveredCellInfo(1, 1, 2, 8));

            grid.Model[1, 1].CellValue = "Floating Cell Demo";
            grid.Model[1, 1].Font.FontSize = 20;
            grid.Model[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
            grid.Model[1, 1].VerticalAlignment = VerticalAlignment.Center;
            grid.Model[1, 1].Font.FontWeight = FontWeights.Bold;

            this.grid.Model[4, 1].CellValue = "Control overview";
            this.grid.Model[5, 1].CellValue = "Public Constructor";
            this.grid.Model[5, 2].CellValue = "Gets the AccessibleObject assigned to the control";
            this.grid.Model[6, 1].CellValue = "Control Constructor Overloaded";
            this.grid.Model[6, 2].CellValue = "Initializes a new instance of the Control class.";
            this.grid.Model[7, 1].CellValue = "Public Properties";
            this.grid.Model[8, 1].CellValue = "Accessibility Object";
            this.grid.Model[8, 2].CellValue = "Gets the AccessibleObject assigned to the control.";
            this.grid.Model[9, 1].CellValue = "AccessibleDefaultAction";
            this.grid.Model[9, 2].CellValue = "Gets or sets the default action description of the control for use by accessibility client applications.";
            this.grid.Model[10, 1].CellValue = "AccessibleDescription";
            this.grid.Model[10, 2].CellValue = "Gets or sets the description of the control used by accessibility client applications.";
            this.grid.Model[11, 1].CellValue = "AccessibleName";
            this.grid.Model[11, 2].CellValue = "Gets or sets the name of the control used by accessibility client applications.";
            this.grid.Model[12, 1].CellValue = "AccessibleRole";
            this.grid.Model[12, 2].CellValue = "Gets or sets the accessible role of the control";
            this.grid.Model[13, 1].CellValue = "AllowDrop";
            this.grid.Model[13, 2].CellValue = "Gets or sets a value indicating whether the control can accept data that the user drags onto it.";
            this.grid.Model[14, 1].CellValue = "Anchor";
            this.grid.Model[14, 2].CellValue = "Gets or sets which edges of the control are anchored to the edges of its container.";
            this.grid.Model[15, 1].CellValue = "BackColor";
            this.grid.Model[15, 2].CellValue = "Gets or sets the background color for the control.";
            this.grid.Model[16, 1].CellValue = "BackgroundImage";
            this.grid.Model[16, 2].CellValue = "Gets or sets the background image displayed in the control. ";
            this.grid.Model[17, 1].CellValue = "BindingContext";
            this.grid.Model[17, 2].CellValue = "Gets or sets the BindingContext for the control.";
            this.grid.Model[18, 1].CellValue = "Bottom";
            this.grid.Model[18, 2].CellValue = "Gets the distance between the bottom edge of the control and the top edge of its container's client area.";
            this.grid.Model.QueryCellInfo += Model_QueryCellInfo;
        }

        #endregion

        #region Events

        private void Model_QueryCellInfo(object sender, Syncfusion.UI.Xaml.CellGrid.Styles.GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex == 0 || e.Cell.ColumnIndex == 0)
                e.Style.Background = new SolidColorBrush(Color.FromArgb(15,0,0,0));
        }

        private void floatingcells_Click(object sender, RoutedEventArgs e)
        {
            this.grid.AllowFloatingCell = (bool)this.floatingcells.IsChecked;
        }

        private void floatingCellOnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.grid.AllowFloatingCellInEdit = (bool)this.floatingCellOnEdit.IsChecked;
        }

        #endregion

        #region Dispose
        public sealed override void Dispose()
        {
            if (grid != null)
            {
                this.grid.Model.QueryCellInfo -= Model_QueryCellInfo;
                grid.Dispose();
                grid = null;
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion
    }
}
