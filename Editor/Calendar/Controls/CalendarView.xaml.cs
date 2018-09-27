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

namespace Input.Calendar
{
    public sealed partial class CalendarView : SampleLayout
    {
        public CalendarView()
        {
            this.InitializeComponent();
            if(IsMobileFamily())
            {
                calendar.FontSize = 10;
            }
            
        }

        public static bool IsMobileFamily()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                return true;
            }
            return false;
        }
        void CalendarView_Loaded(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                calendar.Width = 350;
                calendar.Height = 400;
                Mindate.FontSize = Maxdate.FontSize = FirstDay_Combo.FontSize = 16;
                Mindate.BorderBrush = Maxdate.BorderBrush = new SolidColorBrush(Color.FromArgb(0xA3, 0x00, 0x00, 0x00));
                Mindate.Background = Maxdate.Background = new SolidColorBrush(Colors.White);
                TextSelectionMode.Visibility = Visibility.Collapsed;
                SelectionMode_Combo.Visibility = Visibility.Collapsed;
                showNavigation.OnContentTemplate = showNavigation.OffContentTemplate = Resources["ToggleContentTemplate"] as DataTemplate;
            }
        }

        private void SelectionMode_loaded(object sender, RoutedEventArgs e)
        {
            ((ComboBox)sender).ItemsSource = Enum.GetValues(typeof(Syncfusion.UI.Xaml.Controls.Input.SelectionMode));
            ((ComboBox)sender).SelectedIndex = 0;
        }


        private void SelectionMode_Combo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calendar.SelectionMode = (Syncfusion.UI.Xaml.Controls.Input.SelectionMode)(SelectionMode_Combo.SelectedIndex);
        }

        private void FirstDay_loaded(object sender, RoutedEventArgs e)
        {
            ((ComboBox)sender).ItemsSource = Enum.GetValues(typeof(DayOfWeek));
            ((ComboBox)sender).SelectedIndex = 0;
        }

        private void FirtDay_Combo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FirstDay_Combo.SelectedIndex >= 0)
            {
                calendar.FirstDayofWeek = (DayOfWeek)(FirstDay_Combo.SelectedIndex);
                calendar.Refresh();
            }
        }

        public override void Dispose()
        {
            Loaded -= CalendarView_Loaded;
            SelectionMode_Combo.Loaded -= SelectionMode_loaded;
            SelectionMode_Combo.SelectionChanged -= SelectionMode_Combo_OnSelectionChanged;
            FirstDay_Combo.Loaded -= FirstDay_loaded;
            FirstDay_Combo.SelectionChanged -= FirtDay_Combo_OnSelectionChanged;
            if (calendar != null)
                calendar.Dispose();
            calendar = null;
        }
    }
}
