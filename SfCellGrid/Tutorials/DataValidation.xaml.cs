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
using Windows.UI.Popups;
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
    public sealed partial class DataValidation : SampleLayout, IDisposable
    {

        #region Constructor

        public DataValidation()
        {
            this.InitializeComponent();
            InitializeGrid();
        }

        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            cellGrid.RowCount = 24;
            cellGrid.ColumnCount = 10;
            cellGrid.DefaultRowHeight = 40;
            // Default Row Height and Default Column Width reduced for Mobile View
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                cellGrid.DefaultRowHeight = 30;
                cellGrid.DefaultColumnWidth = 100;
                // Column Width increased to avoid Text Clipping in Mobile View
                cellGrid.ColumnWidths[5] = 180;
                cellGrid.ColumnWidths[3] = 150;
            }
            cellGrid.ColumnWidths[0] = 50;
            cellGrid.CurrentCellValidating += CellGrid_CurrentCellValidating;
            LoadData();
        }

        private void LoadData()
        {
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(1, 1, 2, 7));
            this.cellGrid.Model[1, 1].CellValue = "DATA VALIDATION";
            this.cellGrid.Model[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[1, 1].VerticalAlignment = VerticalAlignment.Center;
            this.cellGrid.Model[1, 1].Font.FontWeight = Windows.UI.Text.FontWeights.Bold;

            // Font size decreased to avoid Text Clipping
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                this.cellGrid.Model[1, 1].Font.FontSize = 23;
            else
                this.cellGrid.Model[1, 1].Font.FontSize = 28;
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(4, 2, 4, 3));

            int rowIndex = 4;
            this.cellGrid.Model[rowIndex, 2].CellValue = "Enter a value Greater than 10";
            this.cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;
            this.cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            this.cellGrid.Model[rowIndex, 5].CellValue = 20;
            this.cellGrid.Model[rowIndex, 5].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[rowIndex, 5].Background = new SolidColorBrush(Colors.LightGreen);
            rowIndex += 2;

            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(rowIndex, 2, rowIndex, 3));
            this.cellGrid.Model[rowIndex, 2].CellValue = "Enter a value between 10 and 20";
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                this.cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;
            this.cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            this.cellGrid.Model[rowIndex, 5].CellValue = 18;
            this.cellGrid.Model[rowIndex, 5].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[rowIndex, 5].Background = new SolidColorBrush(Colors.LightGreen);

            rowIndex += 2;
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(rowIndex, 2, rowIndex, 3));
            this.cellGrid.Model[rowIndex, 2].CellValue = "Enter string length greater than 5";
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                this.cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;
            this.cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            this.cellGrid.Model[rowIndex, 5].CellValue = "Cell Grid";
            this.cellGrid.Model[rowIndex, 5].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[rowIndex, 5].Background = new SolidColorBrush(Colors.LightGreen);

            rowIndex += 2;
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(rowIndex, 2, rowIndex, 3));
            this.cellGrid.Model[rowIndex, 2].CellValue = "Enter a value Less than 50";
            this.cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;
            this.cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            this.cellGrid.Model[rowIndex, 5].CellValue = 25;
            this.cellGrid.Model[rowIndex, 5].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[rowIndex, 5].Background = new SolidColorBrush(Colors.LightGreen);

            rowIndex += 2;
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(rowIndex, 2, rowIndex, 3));
            this.cellGrid.Model[rowIndex, 2].CellValue = "Enter a Date from this Month";
            this.cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;
            this.cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            this.cellGrid.Model[rowIndex, 5].CellValue = DateTime.Now;
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                this.cellGrid.Model[rowIndex, 5].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[rowIndex, 5].Background = new SolidColorBrush(Colors.LightGreen);

            rowIndex += 2;
            this.cellGrid.CoveredCells.Add(new CoveredCellInfo(rowIndex, 2, rowIndex, 3));
            this.cellGrid.Model[rowIndex, 2].CellValue = "Enter a Date from this Year";
            this.cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;
            this.cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            this.cellGrid.Model[rowIndex, 5].CellValue = DateTime.Now;
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                this.cellGrid.Model[rowIndex, 5].HorizontalAlignment = HorizontalAlignment.Center;
            this.cellGrid.Model[rowIndex, 5].Background = new SolidColorBrush(Colors.LightGreen);
        }

        #endregion

        #region Events

        private async void CellGrid_CurrentCellValidating(object sender, Syncfusion.UI.Xaml.CellGrid.Helpers.CurrentCellValidatingEventArgs args)
        {
            if (args.RowcolumnIndex.RowIndex == 4 && args.RowcolumnIndex.ColumnIndex == 5)
            {
                int value = 0;
                if (int.TryParse(args.NewValue.ToString(), out value))
                {
                    if (value < 10)
                    {
                        args.IsValid = false;
                        args.Cancel = true;
                        var message = new MessageDialog("Enter a value greater than 10...", "Validation Error");
                        await message.ShowAsync();
                    }
                }
                else
                {
                    args.IsValid = false;
                    args.Cancel = true;
                    var message = new MessageDialog("Enter valid integer value...", "Validation Error");
                    await message.ShowAsync();
                }
            }
            if (args.RowcolumnIndex.RowIndex == 6 && args.RowcolumnIndex.ColumnIndex == 5)
            {
                int value = 0;
                if (int.TryParse(args.NewValue.ToString(), out value))
                {
                    if (value < 10 || value > 20)
                    {
                        args.IsValid = false;
                        args.Cancel = true;
                        var message = new MessageDialog("Enter a value between 10 and 20...", "Validation Error");
                        await message.ShowAsync();
                    }
                }
                else
                {
                    args.IsValid = false;
                    args.Cancel = true;
                    var message = new MessageDialog("Enter valid integer value...", "Validation Error");
                    await message.ShowAsync();
                }
            }
            if (args.RowcolumnIndex.RowIndex == 8 && args.RowcolumnIndex.ColumnIndex == 5)
            {
                if (args.NewValue.ToString().Length < 5)
                {
                    args.IsValid = false;
                    args.Cancel = true;
                    var message = new MessageDialog("Enter string length greater than 5...", "Validation Error");
                    await message.ShowAsync();
                }
            }
            if (args.RowcolumnIndex.RowIndex == 10 && args.RowcolumnIndex.ColumnIndex == 5)
            {
                int value = 0;
                if (int.TryParse(args.NewValue.ToString(), out value))
                {
                    if (value > 50)
                    {
                        args.IsValid = false;
                        args.Cancel = true;
                        var message = new MessageDialog("Enter a value Less than 50...", "Validation Error");
                        await message.ShowAsync();
                    }
                }
                else
                {
                    args.IsValid = false;
                    args.Cancel = true;
                    var message = new MessageDialog("Enter valid integer value...", "Validation Error");
                    await message.ShowAsync();
                }
            }
            if (args.RowcolumnIndex.RowIndex == 12 && args.RowcolumnIndex.ColumnIndex == 5)
            {
                DateTime date;
                if (DateTime.TryParse(args.NewValue.ToString(), out date))
                {
                    if (date.Month != DateTime.Now.Month)
                    {
                        args.IsValid = false;
                        args.Cancel = true;
                        var message = new MessageDialog("Enter a date from this month...", "Validation Error");
                        await message.ShowAsync();
                    }
                }
                else
                {
                    args.IsValid = false;
                    args.Cancel = true;
                    var message = new MessageDialog("Enter valid Date...", "Validation Error");
                    await message.ShowAsync();
                }
            }
            if (args.RowcolumnIndex.RowIndex == 14 && args.RowcolumnIndex.ColumnIndex == 5)
            {
                DateTime date;
                if (DateTime.TryParse(args.NewValue.ToString(), out date))
                {
                    if (date.Year != DateTime.Now.Year)
                    {
                        args.IsValid = false;
                        args.Cancel = true;
                        var message = new MessageDialog("Enter a date from this year...", "Validation Error");
                        await message.ShowAsync();
                    }
                }
                else
                {
                    args.IsValid = false;
                    args.Cancel = true;
                    var message = new MessageDialog("Enter valid Date...", "Validation Error");
                    await message.ShowAsync();
                }
            }
        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            if (cellGrid != null)
            {
                cellGrid.CurrentCellValidating -= CellGrid_CurrentCellValidating;
                cellGrid.Dispose();
                cellGrid = null;
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion
    }
}

