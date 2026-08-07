using Common;
using Syncfusion.SampleBrowser.UWP.Schedule;
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
    public sealed partial class GettingStarted_WinRT : SampleLayout, IDisposable
    {
       
        ScheduleAppointmentMapping dataMapping;
        ObservableCollection<Meeting> meetings;
        ScheduleViewModel viewModel;
        public GettingStarted_WinRT()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";

            this.InitializeComponent();

            /// <summary>
            /// custom data mapping
            /// </summary>
            meetings = new ObservableCollection<Meeting>();
            viewModel = new ScheduleViewModel();

            dataMapping = new ScheduleAppointmentMapping();
            dataMapping.SubjectMapping = "EventName";
            dataMapping.StartTimeMapping = "From";
            dataMapping.EndTimeMapping = "To";
            dataMapping.AppointmentBackgroundMapping = "color";
            dataMapping.AllDayMapping = "AllDay";
            Schedule.AppointmentMapping = dataMapping;
            meetings = viewModel.ListOfMeeting;
            Schedule.ItemsSource = meetings;

            this.DataContext = viewModel;

            Schedule.AppointmentEditorOpening += Schedule1_AppointmentEditorOpening;
            Schedule.AppointmentEditorClosed += Schedule1_AppointmentEditorClosed;
            Schedule.VisibleDatesChanging += Schedule_VisibleDatesChanging;
            this.Week.IsChecked = true;
       
  
            ScheduleHeaderStyle mainHeader = new ScheduleHeaderStyle();
            mainHeader.HeaderHeight = 0;
            Schedule.ScheduleHeaderStyle = mainHeader;
            Schedule.AppointmentEditorOpening += Schedule_AppointmentEditorOpening;
            Schedule.ScheduleTypeChanging += Schedule_ScheduleTypeChanging;

        
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

        private void Schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            this.OptionVisibility = Visibility.Collapsed;
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
        void Schedule_ScheduleTypeChanging(object sender, ScheduleTypeChangingEventArgs e)
        {
            if (Month.IsChecked == true && Schedule.ScheduleType == ScheduleType.Day)
            {
                Day.IsChecked = true;
                Month.IsChecked = false;
            }
        }
        void Schedule1_AppointmentEditorClosed(object sender, AppointmentEditorClosedEventArgs e)
        {
            ViewSelection.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        void Schedule1_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            ViewSelection.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void Btn_ScheduleType_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as ToggleButton).Name)
            {
                case "Day":
                    {
                        this.Week.IsChecked = false;
                        this.Month.IsChecked = false;
                        this.TimeLine.IsChecked = false;
                        this.WorkWeek.IsChecked = false;
                        Schedule.ScheduleType = ScheduleType.Day;
                        break;
                    }
                case "Week":
                    {
                        this.Day.IsChecked = false;
                        this.Month.IsChecked = false;
                        this.TimeLine.IsChecked = false;
                        this.WorkWeek.IsChecked = false;
                        Schedule.ScheduleType = ScheduleType.Week;
                        break;
                    }
                case "WorkWeek":
                    {
                        this.Day.IsChecked = false;
                        this.Month.IsChecked = false;
                        this.TimeLine.IsChecked = false;
                        this.Week.IsChecked = false;
                        Schedule.ScheduleType = ScheduleType.WorkWeek;
                        break;
                    }
                case "Month":
                    {
                        this.Week.IsChecked = false;
                        this.Day.IsChecked = false;
                        this.TimeLine.IsChecked = false;
                        this.WorkWeek.IsChecked = false;
                        Schedule.ScheduleType = ScheduleType.Month;
                        break;
                    }
                case "TimeLine":
                    {
                        this.Week.IsChecked = false;
                        this.Month.IsChecked = false;
                        this.Day.IsChecked = false;
                        this.WorkWeek.IsChecked = false;
                        Schedule.ScheduleType = ScheduleType.TimeLine;
                        break;
                    }
            }
        }

      
    }
}
