using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Input.DateTimePickers
{
    public sealed partial class DatePickerView : SampleLayout
    {
        public DatePickerView()
        {
            this.InitializeComponent();
            (this.DataContext as DatePickerProperties).PropertyChanged += DatePickerDemo_PropertyChanged;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                DP1.FontSize = DP2.FontSize = 18;
                DP1.BorderBrush = DP2.BorderBrush = new SolidColorBrush(Color.FromArgb(0xA3, 0x00, 0x00, 0x00));
                DP1.Background = DP2.Background = new SolidColorBrush(Colors.White);
            }
           
        }
        void SelectorFormatStringChanged(object sender, SelectionChangedEventArgs e)
        {
            string formatstring = ((sender as ComboBox).SelectedItem as ComboBoxItem).Tag.ToString();
            if (DP1 != null)
                DP1.SelectorFormatString = formatstring;
            if (DP2 != null)
                DP2.SelectorFormatString = formatstring;
        }

        void DatePickerDemo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Format")
            {
                if (DP2 != null)
                    DP2.FormatString = (sender as DatePickerProperties).Format;
                if (DP1 != null)
                    DP1.FormatString = (sender as DatePickerProperties).Format;
            }
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ((ComboBox)sender).SelectedIndex = 0;
        }

        public override void Dispose()
        {
            if ((this.DataContext as DatePickerProperties) != null)
            {
                (this.DataContext as DatePickerProperties).PropertyChanged -= DatePickerDemo_PropertyChanged;
            }
            format.Loaded -= ComboBox_Loaded;
            selectorformat.Loaded -= ComboBox_Loaded;
            selectorformat.SelectionChanged -= SelectorFormatStringChanged;
            //DP1.Dispose();
            //DP2.Dispose();
        }        
     
    }
}
