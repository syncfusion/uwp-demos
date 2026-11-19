using Common;
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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

namespace SampleBrowser.Editors.Controls.MaskedEdit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MaskedEditDemo : SampleLayout
    {
        public MaskedEditDemo()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                name.FontSize = email.FontSize = amount.FontSize = deptime.FontSize =culture.FontSize =validation.FontSize = 16;
                name.BorderBrush = email.BorderBrush = amount.BorderBrush = deptime.BorderBrush = new SolidColorBrush(Color.FromArgb(0xA3, 0x00, 0x00, 0x00));
                name.Background = email.Background = amount.Background = deptime.Background = new SolidColorBrush(Colors.White);
            }
        }

        List<CultureInfo> culturelist = new List<CultureInfo>();
        private void culture_Loaded_1(object sender, RoutedEventArgs e)
        {
            culturelist.Add(new CultureInfo("en-US"));
            culturelist.Add(new CultureInfo("de-DE"));
            culturelist.Add(new CultureInfo("uk-UA"));
            culturelist.Add(new CultureInfo("vi-VN"));
            culturelist.Add(new CultureInfo("ja-JP"));
            culturelist.Add(new CultureInfo("en-IN"));
            culturelist.Add(new CultureInfo("fr-FR"));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = culturelist;
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 0;
        }

        private void Validation_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetValues(typeof(InputValidationMode));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 0;
        }



        public override void Dispose()
        {
            if (name != null)
                name.Dispose();
            if (deptime != null)
                deptime.Dispose();
            if (amount != null)
                amount.Dispose();
            if (email != null)
                email.Dispose();

            culturelist.Clear();
            culturelist = null;
            this.DataContext = null;
            this.name.Dispose();
            this.amount.Dispose();
            this.deptime.Dispose();
            this.email.Dispose();
            if(this.culture.Items != null && this.culture.Items.Count>0)
            this.culture.Items.Clear();
          
            this.culture = this.validation = null;
            this.controlView.Resources.MergedDictionaries.Clear();
            this.controlView.Resources.Clear();
            this.controlView.Resources = null;
            this.Resources = null;
            base.Dispose();

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
