#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
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

namespace Input.Calendar
{
    public sealed partial class CellTemplateView : SampleLayout
    {
        public CellTemplateView()
        {
            this.InitializeComponent();
            if (IsMobileFamily())
            {
               
                calendar.FontSize = 12;

            }
            
        }
        public static bool IsMobileFamily()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                return true;
            }
            return false;
        }
        void CalendarView_Loaded(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                calendar.Width = 350;
                calendar.Height = 400;
            }
        }

        public override void Dispose()
        {
            this.Resources.Clear();
            this.Resources = null;
            Loaded -= CalendarView_Loaded;
            calendar.Dispose();
        }
    }
    public class CellTemplateSelector : DataTemplateSelector
    {
        private ResourceDictionary dictionary;

        private Random rndm = new Random();

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (dictionary == null)
            {
                dictionary = new ResourceDictionary();
                dictionary.Source = new Uri("ms-appx:///Calendar/Controls/Templates.xaml", UriKind.RelativeOrAbsolute);
            }

            var dateTime = (DateTime)item;
            if (dateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return dictionary["Holiday"] as DataTemplate;
            }

            int index = rndm.Next(0, 15);
            if (index <= 8)
            {
                return dictionary["Template" + index.ToString()] as DataTemplate;
            }
            return dictionary["default"] as DataTemplate;
        }
    }
}
