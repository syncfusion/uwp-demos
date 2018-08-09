using Common;
using Syncfusion.UI.Xaml.Controls.Input;
using Syncfusion.UI.Xaml.Controls.Navigation;
using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ScheduleUWP_Samples
{
    #region MainClass
    public sealed partial class CustomizationDemo : SampleLayout, IDisposable
    {
        #region Internal Properties

        internal Appointment SelectedAppointment;
        internal Appointment copiedAppointment;
        internal DateTime CurrentSelectedDate;
        internal BindingClass AddDataContext = null;
        internal SfSchedule schedule;
        Appointment selapp = null;
        ObservableCollection<DateTime> visibleDates = null;

        #endregion

        #region  Properties

        public ScheduleAppointmentCollection AppointmentCollection
        {
            get;
            set;
        }

        public ObservableCollection<DateTime> VisibleDatesCollection
        {
            get { return visibleDates; }
            set
            {
                visibleDates = value;
                if (RadialPopup.IsOpen)
                    RadialPopup.IsOpen = false;
            }
        }

        #endregion
        #region Constructor
        public CustomizationDemo()
        {
            InitializeComponent();
            customeEditor.DataContext = this;
            this.schedule = Schedule;
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            var visibleDatesBinding = new Binding { Source = this, Mode = BindingMode.TwoWay, Path = new PropertyPath("VisibleDatesCollection") };
            BindingOperations.SetBinding(schedule, SfSchedule.VisibleDatesProperty, visibleDatesBinding);

            DateTime currentdate = DateTime.Now.Date;
            if (currentdate.DayOfWeek == System.DayOfWeek.Friday || currentdate.DayOfWeek == System.DayOfWeek.Saturday)
                currentdate = currentdate.SubtractDays(3);
            else if (currentdate.DayOfWeek == System.DayOfWeek.Sunday || currentdate.DayOfWeek == System.DayOfWeek.Monday)
                currentdate = currentdate.AddDays(3);
            AppointmentCollection = new ScheduleAppointmentCollection();
            AppointmentCollection.Add(new Appointment() { AppointmentType = Appointment.AppointmentTypes.Health, Status = schedule.AppointmentStatusCollection[0], StartTime = currentdate.AddHours(12), AppointmentTime = currentdate.AddHours(12).ToString("hh:mm tt"), EndTime = currentdate.AddHours(15), Subject = "Checkup", Location = "Chennai", AppointmentBackground = new SolidColorBrush(ColorHelper.FromArgb(255, 236, 12, 12)) });
            currentdate = currentdate.AddHours(4);
            AppointmentCollection.Add(new Appointment() { AppointmentType = Appointment.AppointmentTypes.Family, Status = schedule.AppointmentStatusCollection[0], StartTime = currentdate.Date.SubtractDays(2).AddHours(1), AppointmentTime = currentdate.Date.SubtractDays(2).AddHours(1).ToString("hh:mm tt"), EndTime = currentdate.Date.SubtractDays(2).AddHours(4), Subject = "My B'day", AppointmentBackground = new SolidColorBrush(ColorHelper.FromArgb(255, 180, 31, 125)) });
            AppointmentCollection.Add(new Appointment() { AppointmentType = Appointment.AppointmentTypes.Office, Status = schedule.AppointmentStatusCollection[0], StartTime = currentdate.Date.AddDays(2).AddHours(9), AppointmentTime = currentdate.Date.AddDays(2).AddHours(9).ToString("hh:mm tt"), EndTime = currentdate.Date.AddDays(2).AddHours(12), Subject = "Meeting", AppointmentBackground = new SolidColorBrush(ColorHelper.FromArgb(255, 60, 181, 75)) });
            schedule.Appointments = AppointmentCollection;
            schedule.AppointmentEditorOpening += schedule_AppointmentEditorOpening;
            schedule.ContextMenuOpening += Schedule_PopupMenuOpening;
            schedule.ScheduleTapped += schedule_ScheduleTapped;
        }
        #endregion
        #region Events

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            RadialPopup.IsOpen = false;
        }
        void schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
            AddDataContext = new BindingClass { Appointment = e.Appointment, CurrentSelectedDate = e.StartTime };
            if (e.Appointment != null)
                EditAppointment();
            else
                AddAppointment();
        }

        void schedule_ScheduleTapped(object sender, ScheduleTappedEventArgs e)
        {
            if (selapp != null)
            {
                selapp.AppointmentSelectionBrush = new SolidColorBrush(Colors.Transparent);
            }
            if (e.Appointment != null)
            {
                (e.Appointment as Appointment).AppointmentSelectionBrush = new SolidColorBrush(Colors.Aqua);
                selapp = (Appointment)e.Appointment;
            }
            else
            {
                foreach (Appointment app in AppointmentCollection)
                {
                    app.AppointmentSelectionBrush = new SolidColorBrush(Colors.Transparent);
                }
            }
        }

        void Schedule_PopupMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            RadialPopup.IsOpen = false;
            e.Cancel = true;
            RadialPopup.IsOpen = true;
            radialMenu.IsOpen = true;
            //double borderBoundX = (e.CurrentEventArgs as TappedRoutedEventArgs).GetPosition(schedule).X + (1.2 * radialMenu.RadiusX);
            //double borderBoundY = (e.CurrentEventArgs as TappedRoutedEventArgs).GetPosition(schedule).Y + (1.2 * radialMenu.RadiusY);
            double borderBoundX = 0.0;
            double borderBoundY = 0.0;
            if (e.CurrentEventArgs is TappedRoutedEventArgs)
            {
                borderBoundX = (e.CurrentEventArgs as TappedRoutedEventArgs).GetPosition(schedule).X + (1.2 * radialMenu.RadiusX);
                borderBoundY = (e.CurrentEventArgs as TappedRoutedEventArgs).GetPosition(schedule).Y + (1.2 * radialMenu.RadiusY);
            }
            else if (e.CurrentEventArgs is HoldingRoutedEventArgs)
            {
                borderBoundX = (e.CurrentEventArgs as HoldingRoutedEventArgs).GetPosition(schedule).X;// +(1.2 * radialMenu.RadiusX);
                borderBoundY = (e.CurrentEventArgs as HoldingRoutedEventArgs).GetPosition(schedule).Y;// +(1.2 * radialMenu.RadiusY);
            }
            else if (e.CurrentEventArgs is PointerRoutedEventArgs)
            {
                borderBoundX = (e.CurrentEventArgs as PointerRoutedEventArgs).GetCurrentPoint(schedule).Position.X + (1.2 * radialMenu.RadiusX);
                borderBoundY = (e.CurrentEventArgs as PointerRoutedEventArgs).GetCurrentPoint(schedule).Position.Y + (1.2 * radialMenu.RadiusY);
            }
            if (borderBoundX > (sender as SfSchedule).ActualWidth)
            {
                RadialPopup.HorizontalOffset = borderBoundX - (2.5 * radialMenu.RadiusX);
            }
            else
            {
                if (e.CurrentEventArgs is TappedRoutedEventArgs)
                {
                    RadialPopup.HorizontalOffset = (e.CurrentEventArgs as TappedRoutedEventArgs).GetPosition(schedule).X;
                }
                else if (e.CurrentEventArgs is HoldingRoutedEventArgs)
                {
                    if ((e.CurrentEventArgs as HoldingRoutedEventArgs).GetPosition(schedule).X < 150)
                        RadialPopup.HorizontalOffset = (e.CurrentEventArgs as HoldingRoutedEventArgs).GetPosition(schedule).X;
                    else
                        RadialPopup.HorizontalOffset = (e.CurrentEventArgs as HoldingRoutedEventArgs).GetPosition(schedule).X - (1.5 * radialMenu.RadiusX);
                }
                else if (e.CurrentEventArgs is PointerRoutedEventArgs)
                {
                    RadialPopup.HorizontalOffset = (e.CurrentEventArgs as PointerRoutedEventArgs).GetCurrentPoint(schedule).Position.X;
                }
            }
            if (borderBoundY > (sender as SfSchedule).ActualHeight)
            {
                RadialPopup.VerticalOffset = borderBoundY - (2.5 * radialMenu.RadiusY);
            }
            else
            {
                if (e.CurrentEventArgs is TappedRoutedEventArgs)
                {
                    RadialPopup.VerticalOffset = (e.CurrentEventArgs as TappedRoutedEventArgs).GetPosition(schedule).Y;
                }
                else if (e.CurrentEventArgs is HoldingRoutedEventArgs)
                {
                    if ((e.CurrentEventArgs as HoldingRoutedEventArgs).GetPosition(schedule).Y < 430)
                        RadialPopup.VerticalOffset = (e.CurrentEventArgs as HoldingRoutedEventArgs).GetPosition(schedule).Y - (radialMenu.RadiusY);
                    else
                        RadialPopup.VerticalOffset = (e.CurrentEventArgs as HoldingRoutedEventArgs).GetPosition(schedule).Y - (2 * radialMenu.RadiusY);
                }
                else if (e.CurrentEventArgs is PointerRoutedEventArgs)
                {
                    RadialPopup.VerticalOffset = (e.CurrentEventArgs as PointerRoutedEventArgs).GetCurrentPoint(schedule).Position.Y;
                }
            }

            if (e.CurrentSelectedDate != null)
            {
                this.CurrentSelectedDate = (DateTime)e.CurrentSelectedDate;
            }
            AddDataContext = new BindingClass { Appointment = e.Appointment, CurrentSelectedDate = e.CurrentSelectedDate };

            if (e.Appointment != null)
            {
                for (int i = 0; i < radialMenu.Items.Count; i++)
                {
                    if (i == 3 && copiedAppointment == null)
                    {
                        (radialMenu.Items[i] as SfRadialMenuItem).IsEnabled = false;
                        (radialMenu.Items[i] as SfRadialMenuItem).Opacity = 0.5;
                    }
                    else
                    {
                        (radialMenu.Items[i] as SfRadialMenuItem).IsEnabled = true;
                        (radialMenu.Items[i] as SfRadialMenuItem).Opacity = 1;
                    }
                }

            }
            else
            {
                (radialMenu.Items[1] as SfRadialMenuItem).IsEnabled = false;
                (radialMenu.Items[1] as SfRadialMenuItem).Opacity = 0.5;
                (radialMenu.Items[2] as SfRadialMenuItem).IsEnabled = false;
                (radialMenu.Items[2] as SfRadialMenuItem).Opacity = 0.5;
                (radialMenu.Items[5] as SfRadialMenuItem).IsEnabled = false;
                (radialMenu.Items[5] as SfRadialMenuItem).Opacity = 0.5;
                (radialMenu.Items[0] as SfRadialMenuItem).IsEnabled = true;
                if (copiedAppointment != null)
                {
                    (radialMenu.Items[3] as SfRadialMenuItem).IsEnabled = true;
                    (radialMenu.Items[3] as SfRadialMenuItem).Opacity = 1;
                }
                else
                {
                    (radialMenu.Items[3] as SfRadialMenuItem).IsEnabled = false;
                    (radialMenu.Items[3] as SfRadialMenuItem).Opacity = 0.5;
                }

            }
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    ApplicationLanguages.PrimaryLanguageOverride = "en-US";
        //   // NavigationService.GoBack();
        //}

        void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        void pasteButton_Click(object sender, RoutedEventArgs e)
        {
            RadialPopup.IsOpen = false;
            if (this.copiedAppointment != null)
            {
                Appointment app = this.copiedAppointment;
                TimeSpan appTimeDiff = app.EndTime - app.StartTime;
                Appointment appointment = new Appointment();
                appointment.Subject = app.Subject;
                appointment.Notes = app.Notes;
                appointment.Location = app.Location;
                appointment.ReadOnly = app.ReadOnly;
                appointment.AppointmentBackground = app.AppointmentBackground;
                appointment.AppointmentTime = app.StartTime.ToString("hh:mm tt");
                appointment.AppointmentType = app.AppointmentType;
                appointment.StartTime = (DateTime)this.CurrentSelectedDate;
                appointment.EndTime = ((DateTime)this.CurrentSelectedDate).Add(appTimeDiff);
                Schedule.Appointments.Add(appointment);
            }
        }

        void copyButton_Click(object sender, RoutedEventArgs e)
        {
            RadialPopup.IsOpen = false;
            copiedAppointment = (Appointment)Schedule.SelectedAppointment;
        }

        void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            RadialPopup.IsOpen = false;
            if (Schedule.SelectedAppointment != null)
            {
                Schedule.Appointments.Remove(Schedule.SelectedAppointment);
            }
        }

        void editButton_Click(object sender, RoutedEventArgs e)
        {
            RadialPopup.IsOpen = false;
            EditAppointment();
        }

        void addButton_Click(object sender, RoutedEventArgs e)
        {
            RadialPopup.IsOpen = false;
            radialMenu.IsOpen = false;
            AddAppointment();
        }

        private void AddAppointment()
        {
            customeEditor.DataContext = null;
            customeEditor.DataContext = AddDataContext;
            customeEditor.eventDetails.IsSelected = true;
            customeEditor.Scroll1.ChangeView(0, 0, 0);
            customeEditor.Visibility = Visibility.Visible;
            SelectedAppointment = null;
            customeEditor.AppType.SelectedIndex = 0;
            customeEditor.AddReminder.SelectedIndex = 0;
            customeEditor.AddEndTimeMonth.Visibility = Visibility.Visible;
            customeEditor.AddEndTimeTime.Visibility = Visibility.Visible;
            customeEditor.AddStartTimeMonth.Visibility = Visibility.Visible;
            customeEditor.AddStartTimeTime.Visibility = Visibility.Visible;
            if (AddDataContext.CurrentSelectedDate != null)
            {
                customeEditor.AddStartTimeMonth.Value = customeEditor.AddStartTimeTime.Value = AddDataContext.CurrentSelectedDate.Value;
                customeEditor.AddEndTimeMonth.Value = customeEditor.AddEndTimeTime.Value = AddDataContext.CurrentSelectedDate.Value.AddHours(1);
            }
            else if (AddDataContext.Appointment != null)
            {
                customeEditor.AddStartTimeMonth.Value = customeEditor.AddEndTimeMonth.Value = ((AddDataContext as BindingClass).Appointment as Appointment).StartTime;
                customeEditor.AddStartTimeTime.Value = customeEditor.AddEndTimeTime.Value = ((AddDataContext as BindingClass).Appointment as Appointment).StartTime.AddHours(1);
            }
            customeEditor.AppType.Visibility = Visibility.Visible;
            customeEditor.AddReminder.Visibility = Visibility.Visible;
            customeEditor.AddAppType.Visibility = Visibility.Collapsed;
            customeEditor.Reminder.Visibility = Visibility.Collapsed;
            customeEditor.EditStartTimeMonth.Visibility = Visibility.Collapsed;
            customeEditor.EditStartTimeTime.Visibility = Visibility.Collapsed;
            customeEditor.EditEndTimeMonth.Visibility = Visibility.Collapsed;
            customeEditor.EditEndTimeTime.Visibility = Visibility.Collapsed;
            customeEditor.Delete.Visibility = Visibility.Collapsed;
            customeEditor.Subject.Text = string.Empty;
            customeEditor.Notes.Text = string.Empty;
            customeEditor.Location.Text = string.Empty;
        }

        private void EditAppointment()
        {
            customeEditor.DataContext = null;
            customeEditor.DataContext = AddDataContext;
            customeEditor.eventDetails.IsSelected = true;
            customeEditor.Scroll1.ChangeView(0, 0, 0);
            customeEditor.Visibility = Visibility.Visible;
            SelectedAppointment = AddDataContext.Appointment as Appointment;
            customeEditor.AddAppType.Visibility = Visibility.Collapsed;
            customeEditor.AddReminder.Visibility = Visibility.Collapsed;
            customeEditor.AppType.Visibility = Visibility.Visible;
            customeEditor.Reminder.Visibility = Visibility.Visible;
            customeEditor.AddEndTimeMonth.Visibility = Visibility.Collapsed;
            customeEditor.AddEndTimeTime.Visibility = Visibility.Collapsed;
            customeEditor.AddStartTimeMonth.Visibility = Visibility.Collapsed;
            customeEditor.AddStartTimeTime.Visibility = Visibility.Collapsed;
            customeEditor.EditStartTimeMonth.Visibility = Visibility.Visible;
            customeEditor.EditStartTimeTime.Visibility = Visibility.Visible;
            customeEditor.EditEndTimeMonth.Visibility = Visibility.Visible;
            customeEditor.EditEndTimeTime.Visibility = Visibility.Visible;
            customeEditor.Delete.Visibility = Visibility.Visible;
            if (AddDataContext.Appointment != null)
            {
                customeEditor.EditStartTimeMonth.Value = (AddDataContext.Appointment as Appointment).StartTime;
                customeEditor.EditStartTimeTime.Value = (AddDataContext.Appointment as Appointment).StartTime;
                customeEditor.EditEndTimeMonth.Value = (AddDataContext.Appointment as Appointment).EndTime;
                customeEditor.EditEndTimeTime.Value = (AddDataContext.Appointment as Appointment).EndTime;
            }
            customeEditor.Subject.Text = SelectedAppointment.Subject;
            customeEditor.Notes.Text = SelectedAppointment.Notes;
            customeEditor.Location.Text = SelectedAppointment.Location;
        }

        #endregion

        public override void Dispose()
        {
            if (schedule != null)
            {
                schedule.Dispose();
                AppointmentCollection.Clear();
                AppointmentCollection = null;
                if (this.DataContext != null && this.DataContext is IDisposable)
                {
                    (this.DataContext as IDisposable).Dispose();
                }
                this.DataContext = null;
            }
        }

    } 
    #endregion

    #region NewCustomEditor Class

    public class NewCustomEditor : Control
    {
        #region Constructor
        public NewCustomEditor()
        {
            this.DefaultStyleKey = typeof(NewCustomEditor);
            this.UpdateLayout();
        }
        #endregion

        #region Private Members

        public SfDateTimeCombo EditStartTimeMonth;
        public SfDateTimeCombo EditStartTimeTime;
        public SfDateTimeCombo EditEndTimeMonth;
        public SfDateTimeCombo EditEndTimeTime;
        public SfDateTimeCombo AddStartTimeMonth;
        public SfDateTimeCombo AddStartTimeTime;
        public SfDateTimeCombo AddEndTimeMonth;
        public SfDateTimeCombo AddEndTimeTime;
        public TextBox Subject;
        public TextBox Notes;
        public SfTextBoxExt Location;
        public Button Close;
        public Button Save;
        public StackPanel ShowMorePanel;
        public CustomizationDemo editorPage;
        public ScrollViewer Scroll1;
        public Grid grid;
        public ComboBox Reminder;
        public Button Delete;
        public ComboBox AddReminder;
        public ComboBox AppType;
        public ComboBox AddAppType;
        public SfTabControl startDateTabControl;
        public SfTabItem timeDetails;
        public SfTabItem eventDetails;


        #endregion

        #region Override Methods
        protected override void OnApplyTemplate()
        {
            editorPage = (CustomizationDemo)this.DataContext;
            AddStartTimeMonth = GetTemplateChild("addstartmonth") as SfDateTimeCombo;
            AddStartTimeTime = GetTemplateChild("addstarttime") as SfDateTimeCombo;
            AddEndTimeMonth = GetTemplateChild("addendmonth") as SfDateTimeCombo;
            AddEndTimeTime = GetTemplateChild("addendtime") as SfDateTimeCombo;
            EditStartTimeMonth = GetTemplateChild("editstartmonth") as SfDateTimeCombo;
            EditStartTimeTime = GetTemplateChild("editstarttime") as SfDateTimeCombo;
            EditEndTimeMonth = GetTemplateChild("editendmonth") as SfDateTimeCombo;
            EditEndTimeTime = GetTemplateChild("editendtime") as SfDateTimeCombo;
            Subject = GetTemplateChild("sub") as TextBox;
            Notes = GetTemplateChild("notes") as TextBox;
            grid = GetTemplateChild("mainGrid") as Grid;
            Location = GetTemplateChild("where") as SfTextBoxExt;
            Close = GetTemplateChild("close") as Button;
            Save = GetTemplateChild("save") as Button;
            Reminder = GetTemplateChild("reminder") as ComboBox;
            Delete = GetTemplateChild("delete") as Button;
            eventDetails = GetTemplateChild("eventDetails") as SfTabItem;
            timeDetails = GetTemplateChild("timeDetails") as SfTabItem;
            startDateTabControl = GetTemplateChild("StartDateTabControl") as SfTabControl;
            ShowMorePanel = GetTemplateChild("showmorepanel") as StackPanel;
            this.Visibility = Visibility.Collapsed;

            Scroll1 = GetTemplateChild("scroll1") as ScrollViewer;
            AddReminder = GetTemplateChild("addreminder") as ComboBox;
            AppType = GetTemplateChild("AppType") as ComboBox;
            AddAppType = GetTemplateChild("AddAppType") as ComboBox;
            Close.Click += Close_Click;
            Save.Click += Save_Click;
            Delete.Click += Delete_Click;
            AddReminder.ItemsSource = Reminder.ItemsSource = Enum.GetValues(typeof(ReminderTimeType));
            AddReminder.SelectedIndex = Reminder.SelectedIndex = 0;
            AppType.ItemsSource = AddAppType.ItemsSource = Enum.GetValues(typeof(Appointment.AppointmentTypes));
            AppType.SelectedIndex = AddAppType.SelectedIndex = 0;
            this.DataContext = editorPage.AddDataContext;
            this.Visibility = Visibility.Collapsed;
            base.OnApplyTemplate();
        }
        #endregion

        #region Events

        void Delete_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            if (editorPage.SelectedAppointment != null)
            {
                editorPage.schedule.Appointments.Remove(editorPage.SelectedAppointment);
            }
        }

        void Save_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            Appointment app;
            if (editorPage.SelectedAppointment == null)
            {
                app = new Appointment();
                DateTime date = (DateTime)AddStartTimeTime.Value;
                app.StartTime = ((DateTime)AddStartTimeMonth.Value).Date.Add(new TimeSpan(date.Hour, date.Minute, date.Second));
                app.AppointmentTime = app.StartTime.ToString("hh:mm tt");
                DateTime date1 = (DateTime)AddEndTimeTime.Value;
                app.ReminderTime = (ReminderTimeType)AddReminder.SelectedItem;
                app.AppointmentType = (Appointment.AppointmentTypes)AddAppType.SelectedItem;
                app.EndTime = ((DateTime)AddEndTimeMonth.Value).Date.Add(new TimeSpan(date1.Hour, date1.Minute, date1.Second));
                app.AppointmentBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0xC1, 0x39));
            }
            else
            {
                app = editorPage.SelectedAppointment;
                DateTime date = (DateTime)EditStartTimeTime.Value;
                app.ReminderTime = (ReminderTimeType)Reminder.SelectedItem;
                app.AppointmentType = (Appointment.AppointmentTypes)AppType.SelectedItem;
                app.StartTime = ((DateTime)EditStartTimeMonth.Value).Date.Add(new TimeSpan(date.Hour, date.Minute, date.Second));
                DateTime date1 = (DateTime)EditEndTimeTime.Value;
                app.EndTime = ((DateTime)EditEndTimeMonth.Value).Date.Add(new TimeSpan(date1.Hour, date1.Minute, date1.Second));
                app.AppointmentBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0xC1, 0x39));
                app.AppointmentTime = app.StartTime.ToString("hh:mm tt");
            }
            app.Subject = Subject.Text;
            app.Notes = Notes.Text;
            app.Location = Location.Text;

            if (AppType.SelectedItem != null)
            {
                app.AppointmentType = (Appointment.AppointmentTypes)AppType.SelectedItem;
            }
            else
            {
                app.AppointmentType = Appointment.AppointmentTypes.Family;
            }

            if (editorPage.SelectedAppointment == null)
            {
                editorPage.schedule.Appointments.Add(app);
            }
        }

        void Close_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        #endregion
    }

    #endregion

    #region Appointment Class

    public class Appointment : ScheduleAppointment, INotifyPropertyChanged
    {
        #region Public Properties

        public Appointment()
        {
            AppointmentSelectionBrush = new SolidColorBrush(Colors.Aqua);
        }

        public AppointmentTypes _appointmentType;
        public AppointmentTypes AppointmentType
        {
            get { return _appointmentType; }
            set
            {
                _appointmentType = value;
                OnPropertyChanged("AppointmentType");
            }
        }

        public enum AppointmentTypes
        {
            Office,
            Health,
            Family
        }

        public string _appointmentTime;
        public string AppointmentTime
        {
            get { return _appointmentTime; }
            set
            {
                _appointmentTime = value;
                OnPropertyChanged("AppointmentTime");
            }
        }

        private Brush _appselbrush;
        public Brush AppointmentSelectionBrush
        {
            get
            {
                return _appselbrush;
            }
            set
            {
                _appselbrush = value;
                OnPropertyChanged("AppointmentSelectionBrush");
            }
        }

        #endregion
        private void OnPropertyChanged(string propertyName)
        {
            var eventHandler = PropertyChangedEvent;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChangedEvent;
    }
    #endregion

    #region Binding Class

    public class BindingClass
    {
        public DateTime? CurrentSelectedDate { get; set; }

        public object Appointment { get; set; }
    }

    #endregion

    #region TypeToImageConverter class

    public class TypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string str = value.ToString();
            if (str.Equals("Family"))
            {
                //return null;
                return new BitmapImage(new Uri("ms-appx:///Schedule/Assets/family.png"));
            }
            else if (str.Equals("Health"))
            {
                //return null;
                return new BitmapImage(new Uri("ms-appx:///Schedule/Assets/Hospital.png"));
            }
            else
            {
                //return null;
                return new BitmapImage(new Uri("ms-appx:///Schedule/Assets/Team.png"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
    #endregion
}
