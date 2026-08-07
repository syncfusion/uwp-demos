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
    public sealed partial class Localization : UserControl
    {
        
         private ObservableCollection<DateTime> datecoll = new ObservableCollection<DateTime>();
        private ObservableCollection<DateTime> WorkWeekDays = new ObservableCollection<DateTime>();
        private ObservableCollection<string> SubjectColl = new ObservableCollection<string>()
                                                           { "Aller à l'Assemblée", "Réunion d'affaires", "conférence", "fête",
                                                              "audit", "Allez faire la fête", "Réunion du client",
                                                             "vacances", "Visite médicale", "salaire", "Payer Maison Louer" };

        public Localization()
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "fr-fr";

            this.InitializeComponent();

            Random ran = new Random();
            DateTime today = DateTime.Now;

            DateTime startMonth = today.AddDays(-14);
            DateTime endMonth = today.AddDays(14);
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
            for (int i = 0; i < 50; i++)
            {
                if (i % 7 == 0 || i % 7 == 6)
                {
                    //add weekend appointments
                    // NonWorkingDays.Add(CurrentStart.AddDays(i));
                }
                else
                {
                    //Add Workweek appointment
                    WorkWeekDays.Add(CurrentStart.AddDays(i));
                }
            }


            for (int i = 0; i < 30; i++)
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

            schedule.Appointments = AppointmentCollection;
        }
        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    ApplicationLanguages.PrimaryLanguageOverride = "en-US";
        //    //NavigationService.GoBack();

        //}
    }
}
