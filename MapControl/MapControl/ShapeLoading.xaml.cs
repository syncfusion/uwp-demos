using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Common;
using Syncfusion.UI.Xaml.Maps;
using Windows.UI;
using System.Collections.ObjectModel;
using SampleBrowser;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapControlUWP_Samples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShapeLoading : UserControl,IDisposable
    {
        public ShapeLoading()
        {
            this.InitializeComponent();
            (this.map.Layers[0] as ShapeFileLayer).ShapesSelected += Maps_ShapesSelected;
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                grid.ColumnDefinitions.Clear();
                var groupTransform = new TransformGroup();
                groupTransform.Children.Add(new RotateTransform() { Angle = 90 });
                groupTransform.Children.Add(new TranslateTransform() { X = 380, Y = 20 });
                groupTransform.Children.Add(new ScaleTransform() { ScaleX = 1.2, ScaleY = 1.2 });
                map.RenderTransform = groupTransform;
            }
        }

        void Maps_ShapesSelected(object sender, SelectionEventArgs args)
        {
            Continents salesByContinent = ((args.Items as ObservableCollection<MapShape>)[0].DataContext as Continents);
            if (salesByContinent != null)
            {
                if (((args.Items as ObservableCollection<MapShape>)[0].DataContext as Continents).NAME == "Africa")
                {
                    this.map.BaseMapIndex = 1;

                }
                else if (((args.Items as ObservableCollection<MapShape>)[0].DataContext as Continents).NAME == "Antarctica")
                {
                    this.map.BaseMapIndex = 2;

                }
                else if (((args.Items as ObservableCollection<MapShape>)[0].DataContext as Continents).NAME == "Asia")
                {
                    this.map.BaseMapIndex = 3;

                }
                else if (((args.Items as ObservableCollection<MapShape>)[0].DataContext as Continents).NAME == "Europe")
                {
                    this.map.BaseMapIndex = 4;

                }
                else if (((args.Items as ObservableCollection<MapShape>)[0].DataContext as Continents).NAME == "North America")
                {
                    this.map.BaseMapIndex = 5;

                }
                else if (((args.Items as ObservableCollection<MapShape>)[0].DataContext as Continents).NAME == "South America")
                {
                    this.map.BaseMapIndex = 6;

                }
                else if (((args.Items as ObservableCollection<MapShape>)[0].DataContext as Continents).NAME == "Oceania")
                {
                    this.map.BaseMapIndex = 7;

                }

                this.DrillUpButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void DrillUpButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.DrillUpButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.map.BaseMapIndex = 0;
        }

        public void Dispose()
        {
            if ((this.map.Layers[0] as ShapeFileLayer) != null)
            {
                (this.map.Layers[0] as ShapeFileLayer).ShapesSelected -= Maps_ShapesSelected;
            }
			if (this.grid.DataContext != null && this.grid.DataContext is IDisposable)
				(this.grid.DataContext as IDisposable).Dispose();
            this.grid.DataContext = null;
        }
    }
}
