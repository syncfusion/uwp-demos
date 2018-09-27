using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using System.Windows.Input;
using Syncfusion.SampleBrowser.UWP.Diagram;

namespace WorkFlowEditor
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool val = (bool)value;
            if (val)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class StringTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                string val = (string)value;

                if (val == "String")
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool val = (bool)value;
            if (val)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }
    }

    public class NumberTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string val = (string)value;

            if (val == "Double")
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class CalendarTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string val = (string)value;

            if (val == "DatePicker")
                return Visibility.Visible;
            return Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class RatingTypeToVisiblityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string val = (string)value;
            if (val == "Rating")
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeTypeToVisibiltyConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string val = (string)value;
            if (val == "TimePicker")
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorPickerToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string val = (string)value;

            if (val == "ColorPicker")
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class StringTypeToBoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string val = (string)value;
            if (val == "String")
                return true;
            return false;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class NumericTypeToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string val = (string)value;
            if (val == "Double" || val == "DateTime" || val == "ColorPicker")
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //bool val = (bool)value;
            //if (val)
            //    return Visibility.Visible;
            //return Visibility.Collapsed;
            throw new NotImplementedException();
        }
    }

    public class TypeToVisibiltyConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                string val = (string)value;
                if (val == "String")
                    return Visibility.Collapsed;
                return Visibility.Visible;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class NodeDataTemplator : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore
           (object item, DependencyObject container)
        {
            if (item is NodeContent)
            {
                var resourceDictionary = new ResourceDictionary()
                {
                    Source = new Uri("ms-appx:///Template/ProcessNodeEditorTemplate.xaml", UriKind.Absolute)
                };
                return resourceDictionary["propertyeditor"] as DataTemplate;
            }
            return null;
        }
    }

    public class NodeValueDataTemplate : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore
          (object item, DependencyObject container)
        {
            var resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Template/ProcessNodeEditorTemplate.xaml", UriKind.Absolute)
            };
            if (item is NodeContent)
            {
                return resourceDictionary["ValueEditor"] as DataTemplate;
            }
            return null;
        }

    }



    public class RadioButtonCommand
    {
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(RadioButtonCommand), new PropertyMetadata(null, OnPropertyChangedcallBack));

        private static void OnPropertyChangedcallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RadioButton rb = d as RadioButton;
            if (rb != null)
            {
                rb.Checked += rb_Checked;
            }
        }

        static void rb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            ICommand cmd = rb.GetValue(CommandProperty) as ICommand;
            if (cmd != null)
            {
                cmd.Execute(rb.Content);
            }
        }
    }
}
