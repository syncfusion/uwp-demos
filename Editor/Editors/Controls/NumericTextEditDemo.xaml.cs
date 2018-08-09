using Common;
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace SampleBrowser.Editors.Controls.NumericTextBox
{
    public sealed partial class NumericTextBoxDemo : SampleLayout 
    {
        public NumericTextBoxDemo()
        {
            this.InitializeComponent();
            if(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                numeric.FontSize = numeric1.FontSize = numeric2.FontSize = 18;
                numeric.BorderBrush = numeric1.BorderBrush = numeric2.BorderBrush = new SolidColorBrush(Color.FromArgb(0xA3, 0x00, 0x00, 0x00));
                numeric.Background = numeric1.Background = numeric2.Background = new SolidColorBrush(Colors.White);
                culture.FontSize = percentdisplaymode.FontSize = format.FontSize = 16;
            }
        }

        private void culture_Loaded_1(object sender, RoutedEventArgs e)
        {
            List<CultureInfo> culturelist = new List<CultureInfo>();
            culturelist.Add(new CultureInfo("en-US"));
            culturelist.Add(new CultureInfo("de-DE"));
            culturelist.Add(new CultureInfo("uk-UA"));
            culturelist.Add(new CultureInfo("vi-VN"));
            culturelist.Add(new CultureInfo("ja-JP"));
            culturelist.Add(new CultureInfo("bg-Bg"));
            culturelist.Add(new CultureInfo("fr-FR"));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = culturelist;
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 0;
        }
        
        private void percentdisplaymode_Loaded_1(object sender, RoutedEventArgs e)
        {
            percentdisplaymode.ItemsSource = Enum.GetValues(typeof(PercentDisplayMode));
            percentdisplaymode.SelectedIndex = 0;
        }
        public override void Dispose()
        {
            if (numeric != null)
                numeric.Dispose();
            if (numeric1 != null)
                numeric1.Dispose();
            if (numeric2 != null)
                numeric2.Dispose();
            this.format.ItemsSource = null;
            this.culture.ItemsSource = null;
            this.percentdisplaymode.ItemsSource = null;
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
