using Common;
using Syncfusion.UI.Xaml.Primitives;
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

namespace Input.DateTimePickers
{
    public sealed partial class CellTemplateView : SampleLayout
    {
        public CellTemplateView()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                this.Loaded += CellTemplateDemo_Loaded;
        }
        void CellTemplateDemo_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= CellTemplateDemo_Loaded;
            stackPanel.Orientation = Orientation.Vertical;
            int margin = 70;
            if (IsMobileFamily())
                margin = 10;
            date.Margin = new Thickness(margin);
           
        }

        public static bool IsMobileFamily()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                return true;
            }
            return false;
        }
        public override void Dispose()
        {
            Loaded -= CellTemplateDemo_Loaded;
            //date.Dispose();
            //time.Dispose();
        }
    }


    public class DayCellTemplateSelector : DataTemplateSelector
    {
        private ResourceDictionary dictionary;

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (dictionary == null)
            {
                dictionary = new ResourceDictionary();
                dictionary.Source = new Uri("ms-appx:///DateTimePickers/Controls/Templates.xaml", UriKind.RelativeOrAbsolute);
            }

            var dateTime = ((DateTimeWrapper)item).DateTime;
            if (dateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return dictionary["DayCellTemplate0"] as DataTemplate;
            }
            else if (dateTime.DayOfWeek == DayOfWeek.Monday)
            {
                return dictionary["DayCellTemplate1"] as DataTemplate;
            }
            else if (dateTime.DayOfWeek == DayOfWeek.Tuesday)
            {
                return dictionary["DayCellTemplate2"] as DataTemplate;
            }
            else if (dateTime.DayOfWeek == DayOfWeek.Wednesday)
            {
                return dictionary["DayCellTemplate3"] as DataTemplate;
            }
            else if (dateTime.DayOfWeek == DayOfWeek.Thursday)
            {
                return dictionary["DayCellTemplate4"] as DataTemplate;
            }
            else if (dateTime.DayOfWeek == DayOfWeek.Friday)
            {
                return dictionary["DayCellTemplate5"] as DataTemplate;
            }
            else
            {
                return dictionary["DayCellTemplate6"] as DataTemplate;
            }

        }
    }



    public class MonthCellTemplateSelector : DataTemplateSelector
    {
        private ResourceDictionary dictionary;

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (dictionary == null)
            {
                dictionary = new ResourceDictionary();
                dictionary.Source = new Uri("ms-appx:///DateTimePickers/Controls/Templates.xaml", UriKind.RelativeOrAbsolute);
            }

            var dateTime = ((DateTimeWrapper)item).DateTime;
            if (dateTime.Month == DateTime.Now.Month)
            {
                return dictionary["MonthCellTemplate0"] as DataTemplate;
            }
            else
            {
                if (dateTime.Month % 4 == 0)
                {
                    return dictionary["MonthCellTemplate1"] as DataTemplate;
                }
            }
            return dictionary["Default"] as DataTemplate;
        }
    }

    public class MeridienCellTemplateSelector : DataTemplateSelector
    {
        private ResourceDictionary dictionary;

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (dictionary == null)
            {
                dictionary = new ResourceDictionary();
                dictionary.Source = new Uri("ms-appx:///DateTimePickers/Controls/Templates.xaml", UriKind.RelativeOrAbsolute);
            }

            var dateTime = (DateTimeWrapper)item;

            if (dateTime.AmPmString == "AM")
            {
                return dictionary["AM"] as DataTemplate;
            }
            else
            {

                return dictionary["PM"] as DataTemplate;
            }
        }
    }
}
