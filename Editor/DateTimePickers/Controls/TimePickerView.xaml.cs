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
    public sealed partial class TimePickerView : SampleLayout
    {
        public TimePickerView()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
               // Grid.Width = 250;
                TP1.FontSize = TP2.FontSize = 18;
                TP1.BorderBrush = TP2.BorderBrush = new SolidColorBrush(Color.FromArgb(0xA3, 0x00, 0x00, 0x00));
                TP1.Background = TP2.Background = new SolidColorBrush(Colors.White);
            }
        }
        void SelectorFormatStringChanged(object sender, SelectionChangedEventArgs e)
        {
            string formatstring = ((sender as ComboBox).SelectedItem as ComboBoxItem).Tag.ToString();
            if (TP1 != null)
                TP1.SelectorFormatString = formatstring;
            if (TP2 != null)
                TP2.SelectorFormatString = formatstring;
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ((ComboBox)sender).SelectedIndex = 0;
        }
                
        public override void Dispose()
        {
            format.Loaded -= ComboBox_Loaded;
            selectorformat.Loaded -= ComboBox_Loaded;
            selectorformat.SelectionChanged -= SelectorFormatStringChanged;
            if (TP1 != null)
            {
                TP1.Dispose();
                TP1.SelectorStyle = null;
                TP1 = null;
            }
            if (TP2 != null)
            {
                TP2.Dispose();
                TP2.SelectorStyle = null;
                TP2 = null;
            }

            GC.Collect();
        }
     
    }
}
