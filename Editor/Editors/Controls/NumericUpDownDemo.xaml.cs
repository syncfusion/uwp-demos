using Common;
using Syncfusion.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SampleBrowser.Editors.Controls.NumericUpDown
{
    public sealed partial class NumericUpDownDemo : SampleLayout
    {
        public NumericUpDownDemo()
        {
            this.InitializeComponent();
           
            minimum.TextChanged += TextChanged;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                updown.FontSize = infants.FontSize = 18;
                updown.BorderBrush = infants.BorderBrush =  maximum.BorderBrush = minimum.BorderBrush = smallChange.BorderBrush = largechange.BorderBrush = new SolidColorBrush(Color.FromArgb(0xA3, 0x00, 0x00, 0x00));
                updown.Background = infants.Background =  maximum.Background = minimum.Background = smallChange.Background = largechange.Background = new SolidColorBrush(Colors.White);
                autoreverse.OnContentTemplate = autoreverse.OffContentTemplate = Resources["ToggleContentTemplate"] as DataTemplate;
                autoreverse.Header = string.Empty;
                autoreversetxt.Visibility = Visibility.Visible;
                spinalignment.FontSize = autoreversetxt.FontSize = maximum.FontSize = minimum.FontSize = smallChange.FontSize = largechange.FontSize = 16;
            }
        }

        void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Convert.ToDouble(minimum.Value) > updown.Maximum)
            {
                minimum.Value = updown.Maximum;                
            }
        }       

        private void spinalignment_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetValues(typeof(SpinButtonsAlignment));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 0;
        }

        public override void Dispose()
        {
            if (updown != null)
                updown.Dispose();
            if (infants != null)
                infants.Dispose();
           if(minimum!=null)
           {
              minimum.TextChanged -= TextChanged;
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
    }
}
