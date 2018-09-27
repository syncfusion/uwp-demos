using Common;
using SampleBrowser;
using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ScheduleUWP_Samples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
   #region ResourceDemo Class

    public sealed partial class ResourceDemo_WinRT : SampleLayout, IDisposable
    {

        #region Public Properties

        private ScheduleAppointmentCollection appointmentcollection;
        public ScheduleAppointmentCollection AppointmentCollection
        {
            get { return appointmentcollection; }
            set { appointmentcollection = value; }
        }
        string[] subject = new string[]
        {
            "Oncological Robotic Surgery",
            "Free Plastic Surgery Camp",
            "Seminar on Recent Advances in Management of Benign Brain Tumours",
            "Patient and Public Forum",
            "4th Clinical Nutrition Update",
            "Robotic GI Surgery International Congress",
            "National CME on Contemporary Developments in Transplants",
            "Meet the Expert",
            "Free Pediatric Cardiac Camp",
            "Join Hands for Patient Safety",
            "Transforming Healthcare with Information Technology",
            "World Continence Week - Free Check-up Camp for women",
            "CME (Continuing Medical Education) Program",
            "International Bariatric Conference",
            "Global Association of Physicians",
            "Interactive Live Workshop on Robotic Surgery in Gynecology",
            "World Ostomy Day and Awareness Program",
            "Billion Hearts Beating brings you Happy Heart Fest to celebrate World Heart Day",
            "CME (Continuing Medical Education) Program",
            "International Bariatric Conference",
            "Global Association of Physicians",
            "Interactive Live Workshop on Robotic Surgery in Gynecology",
            "World Ostomy Day and Awareness Program",
            "Program on Neurology in the Future"
        };

        private ObservableCollection<DateTime> datecoll = new ObservableCollection<DateTime>();
        DateTime currentdate;

        #endregion

        #region Constructor

        public ResourceDemo_WinRT()
        {
            this.InitializeComponent();
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.OrderByResource.IsChecked = false;
            Random randomValue = new Random();
            DateTime today = DateTime.Now;
            if (today.Month == 12)
            {
                today = today.AddMonths(-1);
            }
            else if (today.Month == 1)
            {
                today = today.AddMonths(1);
            }
            int day = (int)today.DayOfWeek;
            DateTime currentWeek = DateTime.Now.AddDays(-day);

            DateTime startMonth = new DateTime(today.Year, today.Month - 1, 1, 0, 0, 0);
            for (int i = 1; i < 30; i += 2)
            {
                for (int j = -7; j < 14; j++)
                {
                    datecoll.Add(currentWeek.Date.AddDays(j).AddHours(randomValue.Next(9, 18)));
                }
            }

            ObservableCollection<SolidColorBrush> brush = new ObservableCollection<SolidColorBrush>();
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0xC1, 0x39)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xD8, 0x00, 0x73)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0x1B, 0xA1, 0xE2)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xE6, 0x71, 0xB8)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0x96, 0x09)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x99, 0x33)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xAB, 0xA9)));
            brush.Add(new SolidColorBrush(Color.FromArgb(0xFF, 0xE6, 0x71, 0xB8)));

            AppointmentCollection = new ScheduleAppointmentCollection();
            ScheduleAppointmentCollection tempcollection = new ScheduleAppointmentCollection();

            AppointmentCollection = tempcollection;
            int count = 0;
            for (int index = 0; index < 30; index++)
            {
                currentdate = datecoll[randomValue.Next(0, datecoll.Count)];
                DateTime nextdate = datecoll[randomValue.Next(0, datecoll.Count)];
                count++;
                ScheduleAppointment appointment1 = new ScheduleAppointment() { StartTime = currentdate, EndTime = currentdate.AddHours(randomValue.Next(0, 2)), Subject = subject[count % subject.Length], Location = "Chennai", AppointmentBackground = brush[index % 3] };
                appointment1.ResourceCollection.Add(new Resource() { TypeName = "Doctors", ResourceName = "Dr.Jacob" });
                count++;
                ScheduleAppointment appointment2 = new ScheduleAppointment() { StartTime = nextdate, EndTime = nextdate.AddHours(randomValue.Next(0, 2)), Subject = subject[count % subject.Length], Location = "Chennai", AppointmentBackground = brush[(index + 2) % 3] };
                appointment2.ResourceCollection.Add(new Resource() { TypeName = "Doctors", ResourceName = "Dr.Darsy" });
                tempcollection.Add(appointment1);
                tempcollection.Add(appointment2);
            }

            Schedule1.AppointmentStatusCollection.Add(new ScheduleAppointmentStatus() { Status = "Temporary" });
            this.DataContext = this;

            (this.Schedule1.ScheduleResourceTypeCollection[0].ResourceCollection[0] as CustomResource).ResourceImageUri = new BitmapImage(new Uri("ms-appx:/Schedule/Assets/emp1.png"));
            (this.Schedule1.ScheduleResourceTypeCollection[0].ResourceCollection[1] as CustomResource).ResourceImageUri = new BitmapImage(new Uri("ms-appx:/Schedule/Assets/emp2.png"));

            ScheduleHeaderStyle mainHeader = new ScheduleHeaderStyle();
            mainHeader.HeaderHeight = 0;
            Schedule1.ScheduleHeaderStyle = mainHeader;

            Schedule1.VisibleDatesChanging += Schedule_VisibleDatesChanging;
        }


        #endregion

        #region Events
        private void Schedule_VisibleDatesChanging(object sender, VisibleDatesChangingEventArgs e)
        {
            DateTime dt = ((ObservableCollection<System.DateTime>)e.NewValue).LastOrDefault();
            String month = null;
            if (Schedule1.ScheduleType == ScheduleType.Month)
            {
                month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(CultureInfo.CurrentCulture.Calendar.GetMonth((e.NewValue as ObservableCollection<System.DateTime>)[(e.NewValue as ObservableCollection<System.DateTime>).Count / 2].Date));
                headerText.Text = month + "," + " " + (e.NewValue as ObservableCollection<System.DateTime>)[(e.NewValue as ObservableCollection<System.DateTime>).Count / 2].Year.ToString();
                headerText.Margin = new Thickness(10, 0, 0, 0);
            }
            else
                headerText.Text = String.Format("{0:MMMM, yyyy}", dt);
        }
        private void HeaderOrder_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as ToggleButton).Name)
            {
                case "OrderByResource":
                    {
                        this.OrderByDate.IsChecked = false;
                        Schedule1.DayHeaderOrder = DayHeaderOrder.OrderByResource;
                        break;
                    }
                case "OrderByDate":
                    {
                        this.OrderByResource.IsChecked = false;
                        Schedule1.DayHeaderOrder = DayHeaderOrder.OrderByDate;
                        break;
                    }
            }
        }

        #endregion

        #region Dispose

        public override void Dispose()
        {
            if (Schedule1 != null)
            {
                Schedule1.Dispose();
                if (AppointmentCollection != null)
                {
                    AppointmentCollection.Clear();
                    AppointmentCollection = null;
                }
                if (this.DataContext != null)
                    this.DataContext = null;

                Schedule1 = null;
            }
        }

        #endregion
    }

    #endregion

    #region CustomResource Class

    public class CustomResource : Resource
    {
        #region Public Properties

        public ImageSource ResourceImageUri { get; set; }

        public Brush BackgroundBrush { get; set; }

        #endregion
    }

    #endregion
}
