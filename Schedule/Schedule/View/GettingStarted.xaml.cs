using Common;
using Syncfusion.SampleBrowser.UWP.Schedule;
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
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ScheduleUWP_Samples
{
    public sealed partial class GettingStarted : SampleLayout,IDisposable
    {
        ScheduleAppointmentMapping dataMapping;
        ObservableCollection<Meeting> meetings;
        ScheduleViewModel viewModel;
        public GettingStarted()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();
            schedule_Type.ItemsSource = Enum.GetValues(typeof(ScheduleType));

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
            schedule.AppointmentMapping = dataMapping;
            meetings = viewModel.ListOfMeeting;
            schedule.ItemsSource = meetings;

            this.DataContext = viewModel;

        }

        public override void Dispose()
        {
            if (schedule != null)
            {
                schedule.Dispose();
                if (this.DataContext != null && this.DataContext is IDisposable)
                {
                    (this.DataContext as IDisposable).Dispose();
                }
                this.DataContext = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.schedule.Visibility == Visibility.Visible)
            {
                this.schedule.Visibility = Visibility.Collapsed;
                this.option.Visibility = Visibility.Visible;
                (sender as Button).Content = "Done";
            }
            else
            {
                this.schedule.Visibility = Visibility.Visible;
                this.option.Visibility = Visibility.Collapsed;
                (sender as Button).Content = "Options";
            }
        }


    }
}
