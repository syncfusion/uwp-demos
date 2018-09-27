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
    public sealed partial class DateTimeComboView : SampleLayout
    {
        public DateTimeComboView()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                DP2.Height = 40;
                MinuteValue.FontSize = SecondsValue.FontSize = Mindate.FontSize = Maxdate.FontSize = 18;
                MinuteValue.BorderBrush = SecondsValue.BorderBrush = Mindate.BorderBrush = Maxdate.BorderBrush = new SolidColorBrush(Color.FromArgb(0xA3, 0x00, 0x00, 0x00));
                MinuteValue.Background = SecondsValue.Background = Mindate.Background = Maxdate.Background = new SolidColorBrush(Colors.White);
            }
           
        }

        public override void Dispose()
        {
           
        }
    }

}
