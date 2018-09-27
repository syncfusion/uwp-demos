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
    public sealed partial class Cell_Comments : SampleLayout, IDisposable
    {
        #region Constructor

        public Cell_Comments()
        {
            InitializeComponent();
            InitializeGrid();
            
            this.LoadData();
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            var random = new Random();
            cellGrid.Model[0, 0].CellValue = "Country/Population";
            cellGrid.Model[1, 0].CellValue = "USA";
            cellGrid.Model[2, 0].CellValue = "India";
            cellGrid.Model[3, 0].CellValue = "China";
            cellGrid.Model[4, 0].CellValue = "Japan";
            cellGrid.Model[5, 0].CellValue = "Russia";
            cellGrid.Model[6, 0].CellValue = "Pakistan";
            cellGrid.Model[7, 0].CellValue = "Brazil";
            cellGrid.Model[8, 0].CellValue = "Indonesia";
            cellGrid.Model[9, 0].CellValue = "Nigeria";
            cellGrid.Model[10, 0].CellValue = "Bangladesh";
            cellGrid.Model[11, 0].CellValue = "Mexico";
            cellGrid.Model[12, 0].CellValue = "Philippines";
            cellGrid.Model[13, 0].CellValue = "Egypt";
            cellGrid.Model[0, 1].CellValue = "Flag";
            cellGrid.Model[0, 2].CellValue = "2005";
            cellGrid.Model[0, 3].CellValue = "2006";
            cellGrid.Model[0, 4].CellValue = "2007";
            cellGrid.Model[0, 5].CellValue = "2008";
            cellGrid.Model[0, 6].CellValue = "2009";
            cellGrid.Model[0, 7].CellValue = "2010";
            cellGrid.Model[0, 8].CellValue = "2011";

            for (int row = 1; row <= 13; row++)
            {
                for (int col = 1; col <= 8; col++)
                {
                    if (col == 1)
                    {
                        cellGrid.Model[row, col].CellType = "DataTemplate";
                        cellGrid.Model[row, col].HorizontalAlignment = HorizontalAlignment.Center;
                        cellGrid.Model[row, col].CellItemTemplateKey = "ImageTemplate";
                        cellGrid.Model[row, col].Tooltip = cellGrid.Model[row, 0].CellValue.ToString();
                        cellGrid.Model[row, col].CellValue = "ms-appx:///Assets/Images/" + cellGrid.Model[row, 0].CellValue.ToString() + ".jpg";
                    }
                    else
                    {
                        cellGrid.Model[row, col].CellValue = random.Next(1, 20) + "%";
                        if (col == 2)
                            cellGrid.Model[row, col].CommentBrush = new SolidColorBrush(Colors.Black);
                        else if (col == 3)
                            cellGrid.Model[row, col].CommentBrush = new SolidColorBrush(Colors.Green);
                        else if (col == 4)
                            cellGrid.Model[row, col].CommentBrush = new SolidColorBrush(Colors.Red);
                        else if (col == 5)
                            cellGrid.Model[row, col].CommentBrush = new SolidColorBrush(Colors.Blue);
                        else if (col == 6)
                            cellGrid.Model[row, col].CommentBrush = new SolidColorBrush(Colors.Violet);
                        else if (col == 7)
                            cellGrid.Model[row, col].CommentBrush = new SolidColorBrush(Colors.Brown);
                        cellGrid.Model[row, col].Comment = "Population rate in " + cellGrid.Model[0, col].CellValue + " is " + cellGrid.Model[row, col].CellValue;
                    }
                }
            }
        }
  
        private void InitializeGrid()
        {
            cellGrid.RowCount = 15;
            cellGrid.ColumnCount = 10;
            cellGrid.DefaultRowHeight = 45;
            cellGrid.DefaultColumnWidth = 150;
            cellGrid.ShowComment = true;
            cellGrid.ShowTooltip = true;
            cellGrid.Model.QueryCellInfo += Model_QueryCellInfo;
        }

        #endregion

        #region Events

        private void Model_QueryCellInfo(object sender, Syncfusion.UI.Xaml.CellGrid.Styles.GridQueryCellInfoEventArgs e)
        {
            if (e.Style.ColumnIndex == 0 || e.Style.RowIndex == 0)
            {
                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
                e.Style.Background = new SolidColorBrush(Color.FromArgb(15, 0, 0, 0));
            }
        }

        #endregion

        #region IDisposable

        public sealed override void Dispose()
        {
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
