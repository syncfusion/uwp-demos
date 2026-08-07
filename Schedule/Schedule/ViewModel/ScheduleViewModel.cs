using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Syncfusion.SampleBrowser.UWP.Schedule
{
    public class ScheduleViewModel
    {
        public ObservableCollection<Meeting> ListOfMeeting { get; set; }
        private List<Point> randomTimeCollection = new List<Point>();

        List<string> currentDayMeetings;
        List<Brush> color_collection;

        public ScheduleViewModel()
        {
            ListOfMeeting = new ObservableCollection<Meeting>();
            InitializeDataForBookings();
            BookingAppointments();
        }

        #region BookingAppointments

        private void BookingAppointments()
        {
            Random randomTime = new Random();
            randomTimeCollection = GettingTimeRanges();

            DateTime date;
            DateTime DateFrom = DateTime.Now.AddMonths(-1);
            DateTime DateTo = DateTime.Now.AddMonths(1);
            DateTime dateRangeStart = DateTime.Now.AddDays(-3);
            DateTime dateRangeEnd = DateTime.Now.AddDays(3);

            for (date = DateFrom; date < DateTo; date = date.AddDays(1))
            {
                if ((DateTime.Compare(date, dateRangeStart) > 0) && (DateTime.Compare(date, dateRangeEnd) < 0))
                {
                    for (int AdditionalAppointmentIndex = 0; AdditionalAppointmentIndex < 3; AdditionalAppointmentIndex++)
                    {
                        Meeting meeting = new Meeting();
                        int hour = (randomTime.Next((int)randomTimeCollection[AdditionalAppointmentIndex].X, (int)randomTimeCollection[AdditionalAppointmentIndex].Y));
                        meeting.From = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                        meeting.To = (meeting.From.AddHours(1));
                        meeting.EventName = currentDayMeetings[randomTime.Next(9)];
                        meeting.color = color_collection[randomTime.Next(9)];
                        ListOfMeeting.Add(meeting);
                    }
                }
                else
                {
                    Meeting meeting = new Meeting();
                    meeting.From = new DateTime(date.Year, date.Month, date.Day, randomTime.Next(9, 11), 0, 0);
                    meeting.To = (meeting.From.AddHours(1));
                    meeting.EventName = currentDayMeetings[randomTime.Next(9)];
                    meeting.color = color_collection[randomTime.Next(9)];
                    ListOfMeeting.Add(meeting);
                }
            }
        }

        #endregion BookingAppointments

        #region GettingTimeRanges

        private List<Point> GettingTimeRanges()
        {
            randomTimeCollection = new List<Point>();
            randomTimeCollection.Add(new Point(9, 11));
            randomTimeCollection.Add(new Point(12, 14));
            randomTimeCollection.Add(new Point(15, 17));

            return randomTimeCollection;
        }

        #endregion GettingTimeRanges

        #region InitializeDataForBookings

        private void InitializeDataForBookings()
        {
            currentDayMeetings = new List<string>();
            currentDayMeetings.Add("General Meeting");
            currentDayMeetings.Add("Plan Execution");
            currentDayMeetings.Add("Project Plan");
            currentDayMeetings.Add("Consulting");
            currentDayMeetings.Add("Performance Check");
            currentDayMeetings.Add("Yoga Therapy");
            currentDayMeetings.Add("Plan Execution");
            currentDayMeetings.Add("Project Plan");
            currentDayMeetings.Add("Consulting");
            currentDayMeetings.Add("Performance Check");

            color_collection = new List<Brush>();
            color_collection.Add(new SolidColorBrush(Color.FromArgb(0XFF, 0X11, 0X7E, 0XB4)));
            color_collection.Add(new SolidColorBrush(Color.FromArgb(0XFF, 0XB4, 0X11, 0X2E)));
            color_collection.Add(new SolidColorBrush(Color.FromArgb(0XFF, 0XC4, 0X43, 0X43)));
            color_collection.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0xC1, 0x39)));
            color_collection.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xD8, 0x00, 0x73)));
            color_collection.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0x1B, 0xA1, 0xE2)));
            color_collection.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xE6, 0x71, 0xB8)));
            color_collection.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0x96, 0x09)));
            color_collection.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x99, 0x33)));
            color_collection.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xAB, 0xA9)));
            color_collection.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xE6, 0x71, 0xB8)));
        }

        #endregion InitializeDataForBookings
    }
}
