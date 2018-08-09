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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CellGridSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CellTemplate : SampleLayout,IDisposable
    {
        #region Constructor

        public CellTemplate()
        {
            this.InitializeComponent();

            InitializeGrid();
            this.LoadData();
            this.LoadCellValues();
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

        #region Private Methods

        private void InitializeGrid()
        {
            cellGrid.RowCount = 18;
            cellGrid.ColumnCount = 10;
            cellGrid.DefaultRowHeight = 45;
            cellGrid.DefaultColumnWidth = 150;
            cellGrid.Model.QueryCellInfo += Model_QueryCellInfo;
        }

        private void LoadData()
        {
            var mod = cellGrid.Model;
            cellGrid.Model[0, 0].CellValue = "Country/Population";
            cellGrid.Model[1, 0].CellValue = "USA";
            cellGrid.Model[2, 0].CellValue = "India";
            cellGrid.Model[3, 0].CellValue = "China";
            cellGrid.Model[4, 0].CellValue = "Japan";
            cellGrid.Model[5, 0].CellValue = "Russia";
            cellGrid.Model[6, 0].CellValue = "Pakistan";
            cellGrid.Model[7, 0].CellValue = "Brazil";
            mod[8, 0].CellValue = "Indonesia";
            mod[9, 0].CellValue = "Nigeria";
            mod[10, 0].CellValue = "Bangladesh";
            mod[11, 0].CellValue = "Mexico";
            mod[12, 0].CellValue = "Philippines";
            mod[13, 0].CellValue = "Egypt";
            mod[14, 0].CellValue = "Unistates";
            mod[0, 1].CellValue = "Flag";
            mod[0, 2].CellValue = "2006 (%)";
            mod[0, 3].CellValue = "2007 (%)";
            mod[0, 4].CellValue = "2008 (%)";
            mod[0, 5].CellValue = "2009 (%)";
            mod[0, 6].CellValue = "2010 (%)";
            mod[0, 7].CellValue = "2011 (%)";
        }

        private void LoadCellValues()
        {
            Random r = new Random();
            for (int row = 0; row <= 14; row++)
            {
                for (int coll = 0; coll <= 7; coll++)
                {
                    if (coll != 0 && row != 0 && coll != 1)
                        cellGrid.Model[row, coll].CellValue = ((double)r.Next(2, 18)).ToString();

                    if (coll == 1 && row > 0)
                    {
                        cellGrid.Model[row, coll].CellType = "DataTemplate";
                        cellGrid.Model[row, coll].CellValue = "ms-appx:///Assets/Images/" + cellGrid.Model[row, 0].CellValue.ToString() + ".jpg";
                        cellGrid.Model[row, coll].CellItemTemplateKey = "ImageTemplate";
                    }

                    if (coll == 2 && row > 0)
                    {
                        cellGrid.Model[row, coll].CellType = "DataTemplate";
                        cellGrid.Model[row, coll].CellItemTemplateKey = "TextTemplate";
                    }

                    if (coll == 4 && row > 0)
                    {
                        cellGrid.Model[row, coll].CellType = "DataTemplate";
                        cellGrid.Model[row, coll].CellItemTemplateKey = "ProgressbarTemplate";
                    }
                    if (coll == 6 && row > 0)
                    {
                        cellGrid.Model[row, coll].CellType = "DataTemplate";
                        cellGrid.Model[row, coll].CellItemTemplateKey = "SliderTemplate";
                    }
                }
            }
        }

        #endregion

        #region Dispose
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
