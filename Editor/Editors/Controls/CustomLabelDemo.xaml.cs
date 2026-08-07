using Common;
using Syncfusion.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SampleBrowser.Editors.Controls.RangeSlider
{
    public sealed partial class CustomLabelDemo : SampleLayout
    {
        public CustomLabelDemo()
        {
            this.InitializeComponent();
        }

        private void tickPlacement_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetValues(typeof(Syncfusion.UI.Xaml.Controls.Input.TickPlacement));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 0;
        }

        private void labelPlacement_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetValues(typeof(Syncfusion.UI.Xaml.Controls.Input.LabelPlacement));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 1;
        }

        private void valuePlacement_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetValues(typeof(Syncfusion.UI.Xaml.Controls.Input.ValuePlacement));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 0;
        }

        private void labelorientation_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetNames(typeof(Orientation));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 1;
        }

        private void labelorientation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (labelorientation.SelectedItem != null)
            {
                if (labelorientation.SelectedItem.ToString().Equals("Vertical"))
                {
                    Departure.LabelOrientation = Orientation.Vertical;
                }
                else
                {
                    Departure.LabelOrientation = Orientation.Horizontal;
                }
            }
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

        public override void Dispose()
        {
            Departure.Dispose();
            Departure = null;
        }
    }
}
