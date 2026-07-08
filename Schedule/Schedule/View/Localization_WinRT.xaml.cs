using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class Localization_WinRT : Page
    {
        private ObservableCollection<DateTime> datecoll = new ObservableCollection<DateTime>();
        private ObservableCollection<DateTime> WorkWeekDays = new ObservableCollection<DateTime>();
        private ObservableCollection<string> SubjectColl = new ObservableCollection<string>()
                                                           { "Aller à l'Assemblée", "Réunion d'affaires", "conférence", "fête",
                                                              "audit", "Allez faire la fête", "Réunion du client",
                                                             "vacances", "Visite médicale", "salaire", "Payer Maison Louer" };

        public Localization_WinRT()
        {

            ApplicationLanguages.PrimaryLanguageOverride = "fr-fr";
            this.InitializeComponent();
            Random ran = new Random();
            DateTime today = DateTime.Now;
            if (today.Month == 12)
            {
                today = today.AddMonths(-1);
            }
            else if (today.Month == 1)
            {
                today = today.AddMonths(1);
            }
            DateTime startMonth = new DateTime(today.Year, today.Month - 1, 1, 0, 0, 0);
            DateTime endMonth = new DateTime(today.Year, today.Month + 1, 30, 0, 0, 0);
            DateTime dt = new DateTime(today.Year, today.Month, 15, 0, 0, 0);
            int day = (int)startMonth.DayOfWeek;
            DateTime CurrentStart = startMonth.AddDays(-day);
            ObservableCollection<SolidColorBrush> brush = new ObservableCollection<SolidColorBrush>();
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0xC1, 0x39)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xD8, 0x00, 0x73)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0x1B, 0xA1, 0xE2)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xE6, 0x71, 0xB8)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0x96, 0x09)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x99, 0x33)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xAB, 0xA9)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xE6, 0x71, 0xB8)));

            ScheduleAppointmentCollection AppointmentCollection = new ScheduleAppointmentCollection();
            for (int i = 0; i < 90; i++)
            {
                if (i % 7 == 0 || i % 7 == 6)
                {
                    //add weekend appointments
                    //NonWorkingDays.Add(CurrentStart.AddDays(i));
                }
                else
                {
                    //Add Workweek appointment
                    WorkWeekDays.Add(CurrentStart.AddDays(i));
                }
            }


            for (int i = 0; i < 40; i++)
            {
                DateTime date = WorkWeekDays[ran.Next(0, WorkWeekDays.Count)].AddHours(ran.Next(9, 17));
                AppointmentCollection.Add(new ScheduleAppointment()
                {
                    StartTime = date,
                    EndTime = date.AddHours(1),
                    AppointmentBackground = brush[i % brush.Count],
                    Subject = SubjectColl[i % SubjectColl.Count]
                });
            }
            //for (int i = 1; i < 20; i += 2)
            //{
            //    for (int j = -7; j < 7; j++)
            //    {
            //        datecoll.Add(DateTime.Now.Date.AddDays(j).AddHours(ran.Next(0, 10)));
            //    }
            //}
            //ObservableCollection<SolidColorBrush> brush = new ObservableCollection<SolidColorBrush>();
            //brush.Add(new SolidColorBrush(Color.FromArgb(0xFf, 0x8E, 0xC4, 0x41)));
            //brush.Add(new SolidColorBrush(Color.FromArgb(0xFf, 0x8D, 0xEA, 0xFF)));
            //brush.Add(new SolidColorBrush(Color.FromArgb(0xFf, 0xF7, 0x94, 0xD7)));
            //ScheduleAppointmentCollection AppCollection = new ScheduleAppointmentCollection();
            //for (int i = 0; i < 15; i++)
            //{
            //    DateTime date = datecoll[ran.Next(0, datecoll.Count)];
            //    AppCollection.Add(new ScheduleAppointment() { StartTime = date, EndTime = date.AddHours(1), AppointmentBackground = brush[i % brush.Count], Subject = SubjectColl[i % SubjectColl.Count] });
            //}
            this.schedule.Appointments = AppointmentCollection;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            //NavigationService.GoBack();

        }
    }
}
