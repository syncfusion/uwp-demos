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
    public sealed partial class ConditionalFormatting : SampleLayout,IDisposable
    {
        #region Constructor

        public ConditionalFormatting()
        {
            this.InitializeComponent();
            InitializeGrid();
        }

        #endregion

        #region Private Fields

        double[] unitPrice = new double[]
        {
            28.5, 336.2, 88.3, 86, 512, 41, 253.3, 33, 87, 45.1, 78.3, 19, 56.7, 23.3, 59, 91, 32.8, 264.5, 63.7, 434.2, 15.9, 21.9, 45, 70.3, 42.5, 67.2, 34.9, 379.9, 0, 59.2, 412.6, 19.8, 42.7, 78, 26.8
        };
        string[] type = new string[]
        {
            "Standard", "New"
        };
        string[] customerID = new string[]
        {
             "Michael Allen",
             "Cecil Allison",
             "Oscar Alpuerto",
            "Sandra Altamirano",
             "Selena Alvarad",
             "Mae Anderson",
            "Ramona Antrim",
             "Sabria Appelbaum",
             "Tom Johnson",
             "Thomas Armstrong",
             "John Ault",
            "Robert Avalos"
        };

        string[] ShipCountry = new string[]
        {
            "Argentina",
            "Austria",
            "Belgium",
            "Brazil",
            "Canada",
            "Denmark",
            "Finland",
            "France",
            "Germany",
            "Ireland",
            "Italy",
            "Mexico",
            "Norway",
            "Poland",
            "Portugal",
            "Spain",
            "Sweden",
            "Switzerland",
            "UK",
            "USA",
            "Venezuela"
        };

        #endregion

        #region Events

        private void Model_QueryCellInfo(object sender, Syncfusion.UI.Xaml.CellGrid.Styles.GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex == 0 || e.Cell.ColumnIndex == 0)
                e.Style.Background = new SolidColorBrush(Color.FromArgb(15, 0, 0, 0));
        }

        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            grid.RowCount = 20;
            grid.ColumnCount = 12;
            // Reduced Defaut Row Height and Default Column Width to display more cells in view
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                grid.DefaultColumnWidth = 80;
                grid.DefaultRowHeight = 20;
            }
            else
            {
                grid.DefaultColumnWidth = 100;
                grid.DefaultRowHeight = 30;
            }
            LoadData();
            ApplyConditionalFormat();
            // Below code is used to avoid text clipping
            grid.ColumnWidths[0] = 50;
            grid.ColumnWidths[1] = 110;
            grid.ColumnWidths[3] = 110;
            grid.ColumnWidths[8] = 80;
            grid.Model.QueryCellInfo += Model_QueryCellInfo;
        }

        private void ApplyConditionalFormat()
        {
            for (int i = 4; i < 14; i++)
            {
                //Condition for Unit price > 60
                var conditionalformat = new GridConditionalFormat(GridConditionalFormatType.CellValue, GridConditionType.GreaterThan, 60);
                conditionalformat.Style.Background = new SolidColorBrush(Colors.DeepSkyBlue);
                this.grid.Model[i, 5].ConditionalFormats = new GridConditionalFormats();
                this.grid.Model[i, 5].ConditionalFormats.Add(conditionalformat);

                //condition for Quantity < 40
                var conditionalformat1 = new GridConditionalFormat(GridConditionalFormatType.CellValue, GridConditionType.LessThan, 40);
                conditionalformat1.Style.Background = new SolidColorBrush(Colors.OrangeRed);
                this.grid.Model[i, 7].ConditionalFormats = new GridConditionalFormats();
                this.grid.Model[i, 7].ConditionalFormats.Add(conditionalformat1);

                //condition for Expense Between 2000 and 3000
                var conditionalformat2 = new GridConditionalFormat(GridConditionalFormatType.CellValue, GridConditionType.Between, 2000, 3000);
                conditionalformat2.Style.Background = new SolidColorBrush(Colors.DarkCyan);
                this.grid.Model[i, 9].ConditionalFormats = new GridConditionalFormats();
                this.grid.Model[i, 9].ConditionalFormats.Add(conditionalformat2);
            }

        }

        private void LoadData()
        {
            var random = new Random();

            this.grid.CoveredCells.Add(new CoveredCellInfo(1, 1, 2, 9));
            this.grid.Model[1, 1].CellValue = "CONDITIONAL FORMATTING";
            this.grid.Model[1, 1].Font.FontWeight = Windows.UI.Text.FontWeights.Bold;
            
            // Font Size decreased to avoid Text Clipping
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                this.grid.Model[1, 1].Font.FontSize = 23;
            else
                this.grid.Model[1, 1].Font.FontSize = 28;

            this.grid.Model[1, 1].Foreground = new SolidColorBrush(Colors.Black);
            this.grid.Model[1, 1].HorizontalAlignment = HorizontalAlignment.Center;
            this.grid.Model[1, 1].VerticalAlignment = VerticalAlignment.Center;

            this.grid.Model[2, 1].HorizontalAlignment = HorizontalAlignment.Center;
            this.grid.Model[2, 1].VerticalAlignment = VerticalAlignment.Center;
            for (int i = 1; i < 10; i++)
            {
                this.grid.Model[3, i].Background = new SolidColorBrush(Colors.Black);
                this.grid.Model[3, i].Foreground = new SolidColorBrush(Colors.White);
                
                // Font Size decreased to avoid Text Clipping
                if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                    this.grid.Model[3, i].Font.FontSize = 13;
                else
                    this.grid.Model[3, i].Font.FontSize = 14;

                this.grid.Model[3, i].Font.FontWeight = Windows.UI.Text.FontWeights.Bold;
                this.grid.Model[3, i].HorizontalAlignment = HorizontalAlignment.Center;
            }

            this.grid.Model[3, 1].CellValue = "SHIP COUNTRY";
            this.grid.Model[3, 2].CellValue = "ORDER ID";
            this.grid.Model[3, 3].CellValue = "CUSTOMER ID";
            this.grid.Model[3, 4].CellValue = "DISCOUNT";
            this.grid.Model[3, 5].CellValue = "UNITPRICE";
            this.grid.Model[3, 6].CellValue = "SHIPPING";
            this.grid.Model[3, 7].CellValue = "QUANTITY";
            this.grid.Model[3, 8].CellValue = "TYPE";
            this.grid.Model[3, 9].CellValue = "EXPENSE";

            for (int i = 4; i < 14; i++)
            {
                this.grid.Model[i, 2].CellValue = "ID" + random.Next(1000, 9000);
                this.grid.Model[i, 2].HorizontalAlignment = HorizontalAlignment.Center;

                this.grid.Model[i, 3].CellValue = customerID[random.Next(customerID.Count() - 1)];

                this.grid.Model[i, 4].CellValue = ((random.Next(20, 40)));
                this.grid.Model[i, 4].HorizontalAlignment = HorizontalAlignment.Center;

                this.grid.Model[i, 1].CellValue = ShipCountry[random.Next(ShipCountry.Count() - 1)];
                this.grid.Model[i, 1].HorizontalAlignment = HorizontalAlignment.Center;

                this.grid.Model[i, 9].CellValue = random.Next(2000, 4000);
                this.grid.Model[i, 9].HorizontalAlignment = HorizontalAlignment.Center;

                this.grid.Model[i, 5].CellValue = unitPrice[random.Next(35)];
                this.grid.Model[i, 5].HorizontalAlignment = HorizontalAlignment.Center;

                this.grid.Model[i, 7].CellValue = random.Next(20, 60);
                this.grid.Model[i, 7].HorizontalAlignment = HorizontalAlignment.Center;

                this.grid.Model[i, 6].CellType = "DateTimeEdit";
                this.grid.Model[i, 6].CellValue = new DateTime(random.Next(2012, 2014), random.Next(1, 12), random.Next(1, 28));
                this.grid.Model[i, 6].HorizontalAlignment = HorizontalAlignment.Center;

                this.grid.Model[i, 8].CellValue = type[random.Next(type.Count())];
                this.grid.Model[i, 8].HorizontalAlignment = HorizontalAlignment.Center;
            }

            this.grid.ColumnWidths[10] = 120;
            this.grid.Model[15, 4].Background = new SolidColorBrush(Colors.OrangeRed);
            this.grid.Model[15, 3].Font.FontSize = 14;
            this.grid.Model[15, 3].Font.FontWeight = FontWeights.Bold;
            this.grid.Model[15, 3].CellValue = " Quantity < 40";

            this.grid.Model[16, 4].Background = new SolidColorBrush(Colors.DeepSkyBlue);
            this.grid.Model[16, 4].HorizontalAlignment = HorizontalAlignment.Center;
            this.grid.Model[16, 3].Font.FontSize = 14;
            this.grid.Model[16, 3].Font.FontWeight = FontWeights.Bold;
            this.grid.Model[16, 3].CellValue = " UnitPrice > 60";

            this.grid.CoveredCells.Add(new CoveredCellInfo(17, 1, 17, 3));
            this.grid.Model[17, 1].HorizontalAlignment = HorizontalAlignment.Center;
            this.grid.Model[17, 4].Background = new SolidColorBrush(Colors.DarkCyan);
            this.grid.Model[17, 1].Font.FontSize = 14;
            this.grid.Model[17, 1].Font.FontWeight = FontWeights.Bold;
            this.grid.Model[17, 1].CellValue = " Expense Between 2000 and 3000";

        }

        #endregion

        #region Dispose

        public sealed override void Dispose()
        {
            if (grid != null)
            {
                grid.Model.QueryCellInfo -= Model_QueryCellInfo;
                grid.Dispose();
                grid = null;
            }
            Resources.Clear();
            base.Dispose();
        }

        #endregion
    }
}
