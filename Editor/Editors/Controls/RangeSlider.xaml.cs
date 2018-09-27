using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Syncfusion.UI.Xaml.Controls.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SampleBrowser.Editors.Controls.RangeSlider
{
    public sealed partial class RangeSliderDemo : SampleLayout
    {
        public RangeSliderDemo()
        {
            this.InitializeComponent();
        }

        void maxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Convert.ToDouble(max.Value) > 10000)
            {
                max.Value = 10000;
            }
        }

        void minTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Convert.ToDouble(min.Value) > 30)
            {
                min.Value = 30;
            }
        }

        private void tickPlacement_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetValues(typeof(Syncfusion.UI.Xaml.Controls.Input.TickPlacement));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 0;
        }

        private void orientation_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetNames(typeof(Orientation));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 1;
        }

        private void orientation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (orientation.SelectedItem != null)
            {
                if (orientation.SelectedItem.ToString().Equals("Vertical"))
                {
                    Arrival.Orientation = Orientation.Vertical;
                    Departure.Orientation = Orientation.Vertical;
                    Grid.SetRow(ArrivalText, 0);
                    Grid.SetColumn(ArrivalText, 1);
                    ArrivalText.Margin = new Thickness(135, 0, 0, 0);
                    Grid.SetRow(Arrival, 1);
                    Grid.SetColumn(Arrival, 1);

                    Arrival.MinHeight = 0;
                    Arrival.MinWidth = 300;
                    Departure.MinWidth = 0;
                    Departure.MinHeight = 300;
                }
                else
                {
                    Arrival.Orientation = Orientation.Horizontal;
                    Departure.Orientation = Orientation.Horizontal;
                    Grid.SetRow(ArrivalText, 2);
                    Grid.SetColumn(ArrivalText, 0);
                    Grid.SetRow(Arrival, 3);
                    Grid.SetColumn(Arrival, 0);

                    ArrivalText.Margin = new Thickness(10, 5, 10, 5);
                    Arrival.MinWidth = 300;
                    Arrival.MinHeight = 0;
                    Departure.MinWidth = 300;
                    Departure.MinHeight = 0;

                }
            }
        }

        private void snapto_loaded(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetNames(typeof(Syncfusion.UI.Xaml.Controls.Input.SliderSnapsTo));
           ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedItem = ((Windows.UI.Xaml.Controls.ComboBox)sender).Items[2];
        }

        private void snapsto_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Arrival.SnapsTo = (Syncfusion.UI.Xaml.Controls.Input.SliderSnapsTo)(snapto.SelectedIndex);
            Departure.SnapsTo = (Syncfusion.UI.Xaml.Controls.Input.SliderSnapsTo)(snapto.SelectedIndex);
        }

        public override void Dispose()
        {
            min.TextChanged -= minTextChanged;
            max.TextChanged -= maxTextChanged;
            tickPlacement.Loaded -= tickPlacement_Loaded_1;
            orientation.Loaded -= orientation_Loaded_1;
            orientation.SelectionChanged -= orientation_SelectionChanged;
            snapto.Loaded -= snapto_loaded;
            snapto.SelectionChanged -= snapsto_OnSelectionChanged;
            this.orientation.ItemsSource = null;
            this.tickPlacement.ItemsSource = null;
            this.snapto.ItemsSource = null;

            if (Departure != null)
            {
                Departure.Dispose();
                Departure = null;
            }

            if(Arrival != null)
            {
                Arrival.Dispose();
                Arrival = null;
            }
            this.DataContext = null;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.controlView.Visibility == Visibility.Visible)
            {
                this.controlView.Visibility = Visibility.Collapsed;
                this.settings.Visibility = Visibility.Visible;
                (sender as Button).Content = "Done";
            }
            else
            {
                this.controlView.Visibility = Visibility.Visible;
                this.settings.Visibility = Visibility.Collapsed;
                (sender as Button).Content = "Options";
            }
        }

        private void movepointto_loaded(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetNames(typeof(MovePoint));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedItem = ((Windows.UI.Xaml.Controls.ComboBox)sender).Items[2];
        }

        private void movepointto_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Arrival.MoveToPoint = (Syncfusion.UI.Xaml.Controls.Input.MovePoint)(movepointto.SelectedIndex);
            Departure.MoveToPoint = (Syncfusion.UI.Xaml.Controls.Input.MovePoint)(movepointto.SelectedIndex);
        }
    }

}
