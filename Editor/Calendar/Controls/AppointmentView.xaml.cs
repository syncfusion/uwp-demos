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
    public sealed partial class AppointmentView : SampleLayout
    {
        public AppointmentView()
        {
            this.InitializeComponent();
            calendar.SelectedDate = DateTime.Today;
            if (IsMobileFamily())
            {
                //viewbox.Width = 250;

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
                calendar.Height = 350;
            }
            else
                appointmentgrid.VerticalAlignment = VerticalAlignment.Center;
        }

        public override void Dispose()
        {
            Loaded -= CalendarView_Loaded;
            if (calendar != null)
                calendar.Dispose();
            calendar = null;
            this.Resources.Clear();
            this.Resources = null;
            base.Dispose();
        }
    }

    public class CellConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var appointments = parameter as Dictionary<DateTime, Appointment>;
            if (value is DateTime)
            {
                var date = ((DateTime)value).Date;
                if (appointments != null)
                {
                    if (appointments.ContainsKey(date))
                    {
                        return appointments[date].Title;
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class CellDescriptionConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var appointments = parameter as Dictionary<DateTime, Appointment>;
            if (value is DateTime)
            {
                var date = ((DateTime)value).Date;
                if (appointments != null)
                {
                    if (appointments.ContainsKey(date))
                    {
                        return appointments[date].Description;
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime)
            {
                var date = (DateTime)value;
                return date.ToString("D").ToUpper();
            }
            else
                return DateTime.Now;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
