using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.Schedule
{
    class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
#if STORE_SAMPLE
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(ScheduleUWP_Samples.GettingStarted_WinRT).AssemblyQualifiedName, Product = "Schedule", Header = "GettingStarted", Tag = Tags.None, Category = Categories.DataVisualization,  HasOptions = true });
#else
                SampleHelper.SetTagsForProduct("Schedule", Tags.None);
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(ScheduleUWP_Samples.GettingStarted_WinRT).AssemblyQualifiedName, Product = "Schedule", Header = "GettingStarted", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = true });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(ScheduleUWP_Samples.CustomizationDemo_WinRT).AssemblyQualifiedName, Product = "Schedule", Header = "Customization", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(ScheduleUWP_Samples.RecurrenceAppointment_WinRT).AssemblyQualifiedName, Product = "Schedule", Header = "Recurrence Appointments", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(ScheduleUWP_Samples.ResourceDemo_WinRT).AssemblyQualifiedName, Product = "Schedule", Header = "Resource", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
#endif
            }
            else
            {
#if STORE_SAMPLE
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(ScheduleUWP_Samples.GettingStarted).AssemblyQualifiedName , Product = "Schedule",ProductIcons = "Icons/Schedule.png", Header = "GettingStarted", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = true });
#else
                SampleHelper.SetTagsForProduct("Schedule", Tags.None);
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(ScheduleUWP_Samples.GettingStarted).AssemblyQualifiedName, Product = "Schedule", Header = "GettingStarted", ProductIcons = "Icons/Schedule.png", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = true });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(ScheduleUWP_Samples.CustomizationDemo).AssemblyQualifiedName, Product = "Schedule", Header = "Customization", ProductIcons = "Icons/Schedule.png", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
                SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(ScheduleUWP_Samples.RecurrenceAppointment).AssemblyQualifiedName, Product = "Schedule", Header = "Recurrence Appointments",Tag = Tags.None, ProductIcons= "Icons/Schedule.png" ,Category = Categories.DataVisualization, HasOptions = false });
#endif
            }

        }
      
    }
}
