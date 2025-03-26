#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Common
{
    public class TagNameToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                Tags tag = (Tags)value;

                if (tag == Tags.New || tag == Tags.NewWithWhatsNew)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 253, 172, 3));
                }
                else if (tag == Tags.Preview || tag == Tags.PreviewWithWhatsNew)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 2, 110, 170));
                }
                else if (tag == Tags.Updated || tag == Tags.UpdatedWithWhatsNew)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 31, 190, 188));
                }
                else
                    return new SolidColorBrush(Colors.Transparent);
            }
            else
                return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class TagEnumToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                Tags tag = (Tags)value;

                if (tag == Tags.New || tag == Tags.NewWithWhatsNew)
                {
                    return "NEW";
                }
                else if (tag == Tags.Preview || tag == Tags.PreviewWithWhatsNew)
                {
                    return "PREVIEW";
                }
                else if (tag == Tags.Updated || tag == Tags.UpdatedWithWhatsNew)
                {
                    return "UPDATED";
                }
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class TagVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Tags tag = (Tags)value;

                if (tag != Tags.None)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class AccordiancounttoMargin : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if((value as Dictionary<string, FeatureSampleCollection>).Count == 1)
            {


                return new Thickness(0, (DeviceFamily.GetDeviceFamily()== Devices.Desktop)?-48:-40, 0, 0);
            }
            else
            return new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
