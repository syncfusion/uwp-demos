using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.CellGrid;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Text;
using Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CellGridSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CellTypes : SampleLayout, IDisposable
    {
        #region Private Fields

        int rowIndex = 1;
        int colIndex = 3;
        List<MovieInfo> MovieDetails;

        #endregion

        #region Constructor
        public CellTypes()
        {
            InitializeComponent();
            InitializeGrid();
            MovieDetails = GetMovieDetails();

            Grid_DataTemplateCellType();
            Grid_StaticCellType();
            Grid_DateTimeCellType();
            Grid_HyperlinkCellType();
            Grid_ComboBoxCellType();
            Grid_TextBoxCellType();
            Grid_CheckBoxCellType();
            Grid_ButtonCellType();
        }
        #endregion

        #region Private Methods

        private void InitializeGrid()
        {
            this.cellGrid.RowCount = 15;
            this.cellGrid.ColumnCount = 10;
            // Header Rows and Columns are hided
            this.cellGrid.RowHeights[0] = 0;
            this.cellGrid.ColumnWidths[0] = 0;

            // Default Column Width and Default Row Height reduced for Mobile View
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                this.cellGrid.DefaultColumnWidth = 100;
                this.cellGrid.DefaultRowHeight = 30;
            }
            else
            {
                this.cellGrid.DefaultColumnWidth = 140;
                this.cellGrid.DefaultRowHeight = 40;

            }
            cellGrid.Model.QueryCellInfo += Model_QueryCellInfo;
        }

        private void Grid_DateTimeCellType()
        {
            rowIndex++;
            colIndex = 3;
            cellGrid.Model[rowIndex, 2].CellValue = "Date";
            cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;
            
            // Font Size reduced for Mobile View to overcome text clipping
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                cellGrid.Model[rowIndex, 2].Font.FontSize = 12;
                cellGrid.Model[rowIndex, 1].Font.FontSize = 14;
            }
            else
            {
                cellGrid.Model[rowIndex, 1].Font.FontSize = 16;
                cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            }

            cellGrid.Model[rowIndex, 1].CellValue = "DateTime";
            cellGrid.Model[rowIndex, 1].Font.FontWeight = FontWeights.Bold;
            cellGrid.Model[rowIndex, 1].HorizontalAlignment = HorizontalAlignment.Center;
            foreach (MovieInfo movie in MovieDetails)
            {
                cellGrid.Model[rowIndex, colIndex].CellType = "DateTimeEdit";
                cellGrid.Model[rowIndex, colIndex].CellValue = movie.Date;
                cellGrid.Model[rowIndex, colIndex].HorizontalAlignment = HorizontalAlignment.Center;
                colIndex += 2;
            }
        }

        private void Grid_ComboBoxCellType()
        {
            rowIndex++;
            colIndex = 3;
            cellGrid.Model[rowIndex, 2].CellValue = "Theatre Type";
            cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;

            // Font Size reduced for Mobile View to overcome text clipping
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                cellGrid.Model[rowIndex, 2].Font.FontSize = 12;
                cellGrid.Model[rowIndex, 1].Font.FontSize = 14;
            }
            else
            {
                cellGrid.Model[rowIndex, 1].Font.FontSize = 16;
                cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            }

            cellGrid.Model[rowIndex, 1].CellValue = "ComboBox";
            cellGrid.Model[rowIndex, 1].Font.FontWeight = FontWeights.Bold;
            cellGrid.Model[rowIndex, 1].HorizontalAlignment = HorizontalAlignment.Center;
            foreach (MovieInfo movie in MovieDetails)
            {
                cellGrid.Model[rowIndex, colIndex].CellType = "ComboBox";
                cellGrid.Model[rowIndex, colIndex].CellValue = movie.TheatreType;
                cellGrid.Model[rowIndex, colIndex].ComboBoxEdit.ItemSource = new List<String> { "First Class", "Second Class", "Third Class" };
                cellGrid.Model[rowIndex, colIndex].HorizontalAlignment = HorizontalAlignment.Center;
                colIndex += 2;
            }
        }

        private List<MovieInfo> GetMovieDetails()
        {
            List<MovieInfo> model = new List<MovieInfo>();

            model.Add(new MovieInfo()
            {
                MovieName = "Avatar",
                Theatre = "Lodo",
                Date = DateTime.Now,
                TheatreType = "First Class",
                City = "New Jersy",
                IsTicketAvailable = true,
                Poster = "Image1"
            });

            model.Add(new MovieInfo()
            {
                MovieName = "Ice Age 3",
                Theatre = "Modern",
                Date = DateTime.Now,
                TheatreType = "Second Class",
                City = "New York",
                IsTicketAvailable = true,
                Poster = "Image2"
            });

            model.Add(new MovieInfo()
            {
                MovieName = "Shrek",
                Theatre = "Lodo",
                Date = DateTime.Now,
                TheatreType = "Third Class",
                City = "New Jersy",
                IsTicketAvailable = true,
                Poster = "Image3"
            });
            return model;
        }

        private void Grid_StaticCellType()
        {
            rowIndex++;
            colIndex = 3;
            cellGrid.Model[rowIndex, 2].CellValue = "Movie Name";
            cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;

            // Font size reduced for Mobile View to avoid text clipping
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                cellGrid.Model[rowIndex, 2].Font.FontSize = 12;
                cellGrid.Model[rowIndex, 1].Font.FontSize = 14;
            }
            else
            {
                cellGrid.Model[rowIndex, 1].Font.FontSize = 16;
                cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            }

            cellGrid.Model[rowIndex, 1].CellValue = "Static";
            cellGrid.Model[rowIndex, 1].Font.FontWeight = FontWeights.Bold;
            cellGrid.Model[rowIndex, 1].HorizontalAlignment = HorizontalAlignment.Center;
            foreach (MovieInfo movie in MovieDetails)
            {
                cellGrid.Model[rowIndex, colIndex].CellType = "Static";
                cellGrid.Model[rowIndex, colIndex].CellValue = movie.MovieName;
                cellGrid.Model[rowIndex, colIndex].HorizontalAlignment = HorizontalAlignment.Center;
                colIndex += 2;
            }
        }

        private void Grid_HyperlinkCellType()
        {
            rowIndex++;
            colIndex = 3;
            cellGrid.Model[rowIndex, 2].CellValue = "Theater Name";
            cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;
            
            // Font size reduced for Mobile View to avoid text clipping
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                cellGrid.Model[rowIndex, 2].Font.FontSize = 12;
                cellGrid.Model[rowIndex, 1].Font.FontSize = 14;
            }
            else
            {
                cellGrid.Model[rowIndex, 1].Font.FontSize = 16;
                cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            }

            cellGrid.Model[rowIndex, 1].CellValue = "Hyperlink";
            cellGrid.Model[rowIndex, 1].Font.FontWeight = FontWeights.Bold;
            cellGrid.Model[rowIndex, 1].HorizontalAlignment = HorizontalAlignment.Center;
            foreach (MovieInfo movie in MovieDetails)
            {
                cellGrid.Model[rowIndex, colIndex].CellType = "Hyperlink";
                cellGrid.Model[rowIndex, colIndex].CellValue = movie.Theatre;
                cellGrid.Model[rowIndex, colIndex].Hyperlink = "http://en.wikipedia.org/wiki/" + movie.Theatre;
                cellGrid.Model[rowIndex, colIndex].HorizontalAlignment = HorizontalAlignment.Center;
                colIndex += 2;
            }
        }

        private void Grid_TextBoxCellType()
        {
            rowIndex++;
            colIndex = 3;
            cellGrid.Model[rowIndex, 2].CellValue = "City";
            cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;
            
            // Font size reduced for Mobile View to avoid text clipping
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                cellGrid.Model[rowIndex, 2].Font.FontSize = 12;
                cellGrid.Model[rowIndex, 1].Font.FontSize = 14;
            }
            else
            {
                cellGrid.Model[rowIndex, 1].Font.FontSize = 16;
                cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            }

            cellGrid.Model[rowIndex, 1].CellValue = "TextBox";
            cellGrid.Model[rowIndex, 1].Font.FontWeight = FontWeights.Bold;
            cellGrid.Model[rowIndex, 1].HorizontalAlignment = HorizontalAlignment.Center;
            foreach (MovieInfo movie in MovieDetails)
            {
                cellGrid.Model[rowIndex, colIndex].CellValue = movie.City;
                cellGrid.Model[rowIndex, colIndex].HorizontalAlignment = HorizontalAlignment.Center;
                colIndex += 2;
            }
        }
    
        private void Grid_CheckBoxCellType()
        {
            rowIndex++;
            colIndex = 3;
            cellGrid.Model[rowIndex, 2].CellValue = "Is Ticket Available";
            cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;

            // Font size reduced for Mobile View to avoid text clipping
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                cellGrid.Model[rowIndex, 2].Font.FontSize = 12;
                cellGrid.Model[rowIndex, 1].Font.FontSize = 14;
            }
            else
            {
                cellGrid.Model[rowIndex, 1].Font.FontSize = 16;
                cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            }

            cellGrid.Model[rowIndex, 1].CellValue = "CheckBox";
            cellGrid.Model[rowIndex, 1].Font.FontWeight = FontWeights.Bold;
            cellGrid.Model[rowIndex, 1].HorizontalAlignment = HorizontalAlignment.Center;
            foreach (MovieInfo movie in MovieDetails)
            {
                cellGrid.Model[rowIndex, colIndex].CellType = "CheckBox";
                cellGrid.Model[rowIndex, colIndex].CellValue = movie.IsTicketAvailable;
                cellGrid.Model[rowIndex, colIndex].HorizontalAlignment = HorizontalAlignment.Center;
                colIndex += 2;
            }
        }

        private void Grid_ButtonCellType()
        {
            rowIndex++;
            colIndex = 3;
            int index = 1;
            foreach (MovieInfo movie in MovieDetails)
            {
                cellGrid.Model[rowIndex, colIndex].Description = "Book Ticket";
                cellGrid.Model[rowIndex, colIndex].CellType = "DataTemplate";
                cellGrid.Model[rowIndex, colIndex].CellItemTemplate = this.Resources["ButtonTemplate" + index] as DataTemplate;
                colIndex += 2;
                index++;
            }
        }

        private void Grid_DataTemplateCellType()
        {
            rowIndex++;
            colIndex = 3;
            cellGrid.Model[rowIndex, 2].CellValue = "Movie Poster";
            cellGrid.Model[rowIndex, 2].HorizontalAlignment = HorizontalAlignment.Center;
            cellGrid.Model[rowIndex, 2].Font.FontWeight = FontWeights.SemiBold;
            
            // Font size reduced for Mobile View to avoid text clipping
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                cellGrid.Model[rowIndex, 2].Font.FontSize = 12;
                cellGrid.Model[rowIndex, 1].Font.FontSize = 14;
            }
            else
            {
                cellGrid.Model[rowIndex, 1].Font.FontSize = 16;
                cellGrid.Model[rowIndex, 2].Font.FontSize = 14;
            }


            cellGrid.Model[rowIndex, 1].CellValue = "Data Template";
            cellGrid.Model[rowIndex, 1].Font.FontWeight = FontWeights.Bold;

            cellGrid.Model[rowIndex, 1].HorizontalAlignment = HorizontalAlignment.Center;
            cellGrid.RowHeights[rowIndex] = 80;
            foreach (MovieInfo movie in MovieDetails)
            {
                cellGrid.Model[rowIndex, colIndex].CellType = "DataTemplate";
                cellGrid.Model[rowIndex, colIndex].CellItemTemplateKey = movie.Poster;
                colIndex += 2;
            }
        }

        #endregion

        #region Events

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cell = cellGrid.Model[8, Convert.ToInt32(button.Tag)];
            string message = "";
            if ((bool)cell.CellValue)
                message = "Ticket Booked...";
            else
                message = "Booking Failed... Ticket is Not Available...";
            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }

        private void Model_QueryCellInfo(object sender, Syncfusion.UI.Xaml.CellGrid.Styles.GridQueryCellInfoEventArgs e)
        {
            if (e.Cell.RowIndex == 0 || e.Cell.ColumnIndex == 0)
                e.Style.Background = new SolidColorBrush(Color.FromArgb(15, 0, 0, 0));
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

    #region Helper

    public class MovieInfo
    {

        public DateTime Date
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the index of the image.
        /// </summary>
        /// <value>The index of the image.</value>
        public string Poster
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the movie.
        /// </summary>
        /// <value>The name of the movie.</value>
        public string MovieName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of theatre
        /// </summary>
        public string TheatreType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the theatre.
        /// </summary>
        /// <value>The theatre.</value>
        public string Theatre
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the isTicketAvailable.
        /// </summary>
        /// <value>The city.</value>
        public bool IsTicketAvailable
        {
            get;
            set;
        }
    }

    #endregion
}

