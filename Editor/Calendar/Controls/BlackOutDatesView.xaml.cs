using Common;
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

namespace Input.Calendar
{
    public sealed partial class BlackOutDatesView : SampleLayout
    {
        public BlackOutDatesView()
        {
            this.InitializeComponent();
            
            calendar.BlackOutDates.Add(DateTime.Now);
            calendar.BlackOutDates.Add(DateTime.Now.AddDays(1));
            calendar.BlackOutDates.Add(DateTime.Now.AddDays(2));
            calendar.BlackOutDates.Add(DateTime.Now.AddDays(3));
            calendar.BlackOutDates.Add(DateTime.Now.AddDays(4));
            calendar.BlackOutDates.Add(DateTime.Now.AddDays(12));
            calendar.BlackOutDates.Add(DateTime.Now.AddDays(13));
            calendar.BlackOutDates.Add(DateTime.Now.AddDays(14));
            calendar.BlackOutDates.Add(DateTime.Now.AddDays(-7));
            calendar.BlackOutDates.Add(DateTime.Now.AddDays(-8));

        }
        void CalendarView_Loaded(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                calendar.Width = 350;
                calendar.Height = 400;
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
        public override void Dispose()
        {
            Loaded -= CalendarView_Loaded;
            if (calendar != null)
                calendar.Dispose();
            calendar = null;
        }
    }
}
