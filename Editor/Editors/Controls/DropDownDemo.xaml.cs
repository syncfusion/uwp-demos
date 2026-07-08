using System;
using Syncfusion.UI.Xaml.Controls;
using Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SampleBrowser.Editors.Controls.DropDown
{
    public sealed partial class DropDownDemo : SampleLayout
    {
        public DropDownDemo()
        {
            this.InitializeComponent();
            this.Loaded += DropDownDemo_Loaded;
            if (DeviceFamily.GetDeviceFamily()==Devices.Mobile)
            {
                datedroptext.Visibility = Visibility.Collapsed;
                sliderdroptext.Visibility = Visibility.Collapsed;
                datedrop.Visibility = Visibility.Collapsed;
                sliderdrop.Visibility = Visibility.Collapsed;
               
                colorDropdown.FontSize = 12;
                calculateDropdown.Visibility = Visibility.Collapsed;
                calculateText.Visibility = Visibility.Collapsed;




            }
        }

        void DropDownDemo_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= DropDownDemo_Loaded;
            calendar.SelectedDate = DateTime.Now;
        }

        public override void Dispose()
        {
            if (datedrop != null)
                datedrop.Dispose();
            if (calculateDropdown != null)
                calculateDropdown.Dispose();
            if (sliderdrop != null)
                sliderdrop.Dispose();
            if (colorDropdown != null)
                colorDropdown.Dispose();
            this.Loaded -= DropDownDemo_Loaded;
        }
    }
}
