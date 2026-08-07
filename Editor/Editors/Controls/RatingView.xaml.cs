using Common;
using Syncfusion.UI.Xaml.Primitives;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SampleBrowser.Editors.Controls.RatingControl
{
    public sealed partial class RatingView : SampleLayout
    {
        public RatingView()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {

                rating1.ItemSize = 25;
                rating2.ItemSize = 25;
                rating3.ItemSize = 25;

            }
            return;

        }
        private void precision_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetValues(typeof(Precision));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 0;
        }
        public override void Dispose()
        {
            this.rating1.ItemsSource = null;
            this.rating2.ItemsSource = null;
            this.rating3.ItemsSource = null;
            this.precision.ItemsSource = null;
            this.DataContext = null;
            precision.Loaded -= precision_Loaded_1;
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
    }
}
