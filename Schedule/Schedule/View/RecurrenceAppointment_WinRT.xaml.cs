using Common;
using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ScheduleUWP_Samples
{
    public sealed partial class RecurrenceAppointment_WinRT : SampleLayout,IDisposable
    {
        public RecurrenceAppointment_WinRT()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();
            DateTime today = DateTime.Now;
            if (today.Month == 12)
            {
                today = today.AddMonths(-1);
            }
            else if (today.Month == 1)
            {
                today = today.AddMonths(1);
            }
            DateTime currentdate = new DateTime(today.Year, today.Month - 1, 1, 0, 0, 0);
            ScheduleAppointmentCollection AppointmentCollection = new ScheduleAppointmentCollection();

            // Daily Recursive Appointment

            ScheduleAppointment SchApp = new ScheduleAppointment();
            SchApp.Subject = "Team Meeting";
            SchApp.Notes = "Daily Recurrence";
            SchApp.Location = "Meeting Hall 1";
            SchApp.StartTime = currentdate;
            SchApp.EndTime = currentdate.AddHours(4);
            SchApp.AppointmentBackground = new SolidColorBrush((Color.FromArgb(0xFF, 0xD8, 0x00, 0x73)));

            // Setting Recurrence Properties

            RecurrenceProperties RecurrenceProperty = new RecurrenceProperties();
            RecurrenceProperty.RecurrenceType = RecurrenceType.Daily;
            RecurrenceProperty.IsDailyEveryNDays = false;
            //RecProp.DailyNDays = 2;
            RecurrenceProperty.IsRangeRecurrenceCount = true;
            RecurrenceProperty.IsRangeNoEndDate = false;
            RecurrenceProperty.IsRangeEndDate = false;
            RecurrenceProperty.RangeRecurrenceCount = 100;

            // Generating RRule using ScheduleHelper

            SchApp.RecurrenceRule = ScheduleHelper.RRuleGenerator(RecurrenceProperty, SchApp.StartTime, SchApp.EndTime);
            SchApp.IsRecursive = true;
            AppointmentCollection.Add(SchApp);

            Schedule.Appointments = AppointmentCollection;

            // Weekly Recursive Appointment

            ScheduleAppointment SchAppointmentWeek = new ScheduleAppointment();
            SchAppointmentWeek.Subject = "Doctor Appointment";
            SchAppointmentWeek.Notes = "Weekly Recurrence";
            SchAppointmentWeek.Location = "Global Hospital";
            SchAppointmentWeek.StartTime = currentdate;
            SchAppointmentWeek.EndTime = currentdate.AddHours(4);
            SchAppointmentWeek.AppointmentBackground = new SolidColorBrush((Color.FromArgb(0xFF, 0xA2, 0xC1, 0x39)));

            // Setting Recurrence Properties

            RecurrenceProperties RecurrencePropertyWeek = new RecurrenceProperties();
            RecurrencePropertyWeek.RecurrenceType = RecurrenceType.Weekly;
            RecurrencePropertyWeek.WeeklyEveryNWeeks = 1;
            RecurrencePropertyWeek.IsWeeklySunday = true;
            RecurrencePropertyWeek.IsRangeRecurrenceCount = true;
            RecurrencePropertyWeek.IsRangeNoEndDate = false;
            RecurrencePropertyWeek.IsRangeEndDate = false;
            RecurrencePropertyWeek.RangeRecurrenceCount = 20;

            // Generating RRule using ScheduleHelper

            SchAppointmentWeek.RecurrenceRule = ScheduleHelper.RRuleGenerator(RecurrencePropertyWeek, SchAppointmentWeek.StartTime, SchAppointmentWeek.EndTime);
            SchAppointmentWeek.IsRecursive = true;
            AppointmentCollection.Add(SchAppointmentWeek);


            // Yearly Recursive Appointment

            ScheduleAppointment SchAppointmentYear = new ScheduleAppointment();
            SchAppointmentYear.Subject = "Wedding Anniversary";
            SchAppointmentYear.Notes = "Yearly Recurrence";
            SchAppointmentYear.Location = "Home";
            SchAppointmentYear.StartTime = currentdate;
            SchAppointmentYear.EndTime = currentdate.AddHours(4);
            SchAppointmentYear.AppointmentBackground = new SolidColorBrush((Color.FromArgb(0xFF, 0x00, 0xAB, 0xA9)));

            // Setting Recurrence Properties using RRule

            SchAppointmentYear.RecurrenceRule = "FREQ=YEARLY;COUNT=3;BYMONTHDAY=" + (DateTime.Now.Day).ToString() + ";BYMONTH=" + (DateTime.Now.Month).ToString() + "";
            SchAppointmentYear.IsRecursive = true;
            AppointmentCollection.Add(SchAppointmentYear);

            ScheduleHeaderStyle mainHeader = new ScheduleHeaderStyle();
            mainHeader.HeaderHeight = 0;
            Schedule.ScheduleHeaderStyle = mainHeader;

            Schedule.VisibleDatesChanging += Schedule_VisibleDatesChanging;
        }

        private void Schedule_VisibleDatesChanging(object sender, VisibleDatesChangingEventArgs e)
        {
            DateTime dt = ((ObservableCollection<System.DateTime>)e.NewValue).LastOrDefault();
            String month = null;
            if (Schedule.ScheduleType == ScheduleType.Month)
            {
                month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(CultureInfo.CurrentCulture.Calendar.GetMonth((e.NewValue as ObservableCollection<System.DateTime>)[(e.NewValue as ObservableCollection<System.DateTime>).Count / 2].Date));
                headerText.Text = month + "," + " " + (e.NewValue as ObservableCollection<System.DateTime>)[(e.NewValue as ObservableCollection<System.DateTime>).Count / 2].Year.ToString();
                headerText.Margin = new Thickness(10, 0, 0, 0);
            }
            else
                headerText.Text = String.Format("{0:MMMM, yyyy}", dt);
        }

        public override void Dispose()
        {
            if (Schedule != null)
            {
                Schedule.Dispose();
                if (this.DataContext != null && this.DataContext is IDisposable)
                {
                    (this.DataContext as IDisposable).Dispose();
                }
                this.DataContext = null;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
           // NavigationService.GoBack();
            Dispose();
        }
    }
}
