using Syncfusion.UI.Xaml.Maps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapControlUWP_Samples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TicketBooking : Page
    {
        public TicketBooking()
        {
            this.InitializeComponent();
            this.map.Loaded += Map_Loaded;
            this.shapelayer.ItemsSource = GetDataSource();
            this.shapelayer.SelectedMapShapes.CollectionChanged += SelectedMapShapes_CollectionChanged;
        }

        private void SelectedMapShapes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                UpdateSelection();
            }
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            LoadShapefile();
        }

        private void LoadShapefile()
        {
            var assembly = typeof(TicketBooking).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.Maps.Assets.ShapeFiles.Custom.shp";
            string valuePath = "Syncfusion.SampleBrowser.UWP.Maps.Assets.ShapeFiles.Custom.dbf";
            var fileStream = assembly.GetManifestResourceStream(resourcePath);
            var file = assembly.GetManifestResourceStream(valuePath);
            this.shapelayer.LoadFromStream(fileStream, file);

        }
        List<TicketData> GetDataSource()
        {
            List<TicketData> list = new List<TicketData>();
            for (int i = 1; i < 22; i++)
            {
                list.Add(new TicketData("" + i));


            }
            return list;
        }

        private void shapelayer_ShapesSelected(object sender, Syncfusion.UI.Xaml.Maps.SelectionEventArgs args)
        {
            UpdateSelection();

        }


        void UpdateSelection()
        {
            List<MapShape> selectedShapes = new List<MapShape>();

            foreach (MapShape shape in shapelayer.SelectedMapShapes)
            {
                TicketData data = (shape.DataContext as TicketData);

                if (data.SeatNumber == "1" || data.SeatNumber == "2" || data.SeatNumber == "8" || data.SeatNumber == "9")
                {
                    shape.Shape.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 165, 0));
                }
                else
                {
                    selectedShapes.Add(shape);
                }
            }

            string selected = "";
            if (selectedShapes.Count == 0)
            {
                SelectedLabel.Text = selected;
                SelectedLabelCount.Text = selected;
                clearButton.IsEnabled = false;
                clearButton.Opacity = 0.5;
            }
            else
            {
                int count = 0;

                foreach (MapShape shape in selectedShapes)
                {
                    TicketData data = (shape.DataContext as TicketData);

                    if (data.SeatNumber == "1" || data.SeatNumber == "2" || data.SeatNumber == "8" || data.SeatNumber == "9")
                    {
                        shape.Shape.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 165, 0));
                    }

                    else
                    {
                        count++;
                        if ((selectedShapes.Count > 1 && count < selectedShapes.Count) && selectedShapes.Count != 0)
                        {
                            selected += ("S" + data.SeatNumber + ", ");
                        }
                        else if (count == selectedShapes.Count || selectedShapes.Count <= 1)
                        {
                            selected += ("S" + data.SeatNumber);
                        }

                        this.clearButton.Opacity = 1;
                        this.clearButton.IsEnabled = true;
                        SelectedLabel.Text = selected;
                        SelectedLabelCount.Text = "" + count;

                    }
                }

            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            if (shapelayer.SelectedMapShapes.Count != 0)
            {
                shapelayer.SelectedMapShapes.Clear();
                SelectedLabel.Text = "";
                SelectedLabelCount.Text = "" + shapelayer.SelectedMapShapes.Count;
                this.clearButton.IsEnabled = false;
                this.clearButton.Opacity = 0.5;

            }
        }
    }

    public class TicketData
    {
        public TicketData(string seatNumber)
        {
            SeatNumber = seatNumber;

        }

        public string SeatNumber
        {
            get;
            set;
        }


    }
}
