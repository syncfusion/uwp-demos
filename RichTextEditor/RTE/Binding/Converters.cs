#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.RichTextBoxAdv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;
using Syncfusion.UI.Xaml.Controls.Media;
using Syncfusion.UI.Xaml.Controls.Input;
using Windows.UI.Popups;
using Windows.UI;
using Windows.UI.Xaml.Shapes;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Printing;
using Windows.Graphics.Printing;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.Phone.UI.Input;
using Windows.ApplicationModel.Activation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RTEDemo
{
    /// <summary>
    /// Specifies the double to string converter.
    /// </summary>
    public class DoubleStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts the specified double to string type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((double)value == 0)
                return "";
            return (Math.Round((double)value)).ToString();
        }
        /// <summary>
        /// Converts the specified string to double value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Double.Parse((string)value);
        }
    }
    /// <summary>
    /// Specifies the highlight color to toggle value.
    /// </summary>
    public class HighlightColorToggleConverter : IValueConverter
    {
        /// <summary>
        /// Converts the specified Highlight color value to toggle.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || parameter == null)
                return false;
            HighlightColor highlightColor = (HighlightColor)value;
            if (parameter.ToString().Equals("Red") && highlightColor == HighlightColor.Red)
                return true;
            else if (parameter.ToString().Equals("Yellow") && highlightColor == HighlightColor.Yellow)
                return true;
            else if (parameter.ToString().Equals("Green") && highlightColor == HighlightColor.Green)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Converts the toggle to Highlight color.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string highlightColor = parameter == null ? string.Empty : parameter.ToString();
            if ((bool)value)
            {
                switch (highlightColor)
                {
                    case "Yellow": return HighlightColor.Yellow;
                    case "Red": return HighlightColor.Red;
                    case "Green": return HighlightColor.Green;
                }
            }
            return HighlightColor.NoColor;
        }
    }
    /// <summary>
    /// Specifies the highlight color to selected index converter.
    /// </summary>
    public class HighlightColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts the specified Highlight color value to selected index.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            if (value == null)
                return -1;
            HighlightColor color = (HighlightColor)value;

            switch (color)
            {
                case HighlightColor.Yellow: return 0;
                case HighlightColor.BrightGreen: return 1;
                case HighlightColor.Turquoise: return 2;
                case HighlightColor.Pink: return 3;
                case HighlightColor.Blue: return 4;
                case HighlightColor.Red: return 5;
                case HighlightColor.DarkBlue: return 6;
                case HighlightColor.Teal: return 7;
                case HighlightColor.Green: return 8;
                case HighlightColor.Violet: return 9;
                case HighlightColor.DarkRed: return 10;
                case HighlightColor.DarkYellow: return 11;
                case HighlightColor.Gray50: return 12;
                case HighlightColor.Gray25: return 13;
                case HighlightColor.Black: return 14;
                default: return 15;
            }
        }
        /// <summary>
        /// Converts the selected index to Highlight color.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            switch ((int)value)
            {
                case 0: return HighlightColor.Yellow;
                case 1: return HighlightColor.BrightGreen;
                case 2: return HighlightColor.Turquoise;
                case 3: return HighlightColor.Pink;
                case 4: return HighlightColor.Blue;
                case 5: return HighlightColor.Red;
                case 6: return HighlightColor.DarkBlue;
                case 7: return HighlightColor.Teal;
                case 8: return HighlightColor.Green;
                case 9: return HighlightColor.Violet;
                case 10: return HighlightColor.DarkRed;
                case 11: return HighlightColor.DarkYellow;
                case 12: return HighlightColor.Gray50;
                case 13: return HighlightColor.Gray25;
                case 14: return HighlightColor.Black;
                default: return HighlightColor.NoColor;
            }
        }
    }
    /// <summary>
    /// Specifies the font color to toggle value.
    /// </summary>
    public class FontColorToggleConverter : IValueConverter
    {
        /// <summary>
        /// Converts the specified font color value to toggle.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || parameter == null)
                return false;
            Color fontColor = (Color)value;
            if (parameter.ToString().Equals("Red") && fontColor.R == 255 && fontColor.G == 0 && fontColor.B == 0)
                return true;
            else if (parameter.ToString().Equals("Green") && fontColor.R == 0 && fontColor.G == 176 && fontColor.B == 0)
                return true;
            else if (parameter.ToString().Equals("Orange") && fontColor.R == 227 && fontColor.G == 108 && fontColor.B == 9)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Converts the toggle to Font color.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string fontColor = parameter == null ? string.Empty : parameter.ToString();
            if ((bool)value)
            {
                switch (fontColor)
                {
                    case "Orange": return Color.FromArgb(255, 227, 108, 9);
                    case "Red": return Color.FromArgb(255, 255, 0, 0);
                    case "Green": return Color.FromArgb(255, 0, 176, 0);
                }
            }
            return Color.FromArgb(255, 0, 0, 0);
        }
    }
    /// <summary>
    /// Specifies the Slected color value.
    /// </summary>
    public class SelectedColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new SolidColorBrush((Color)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
    /// <summary>
    /// Specifies the Start At value for list.
    /// </summary>
    public class StartAtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ToDouble((int)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is double)
            {
                return System.Convert.ToInt32((double)value);
            }
            else
            {
                return System.Convert.ToInt32((float)value);
            }
        }
    }
    /// <summary>
    /// Specifies the Follow Character for list.
    /// </summary>
    public class FollowCharacterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            FollowCharacterType followCharacter = (FollowCharacterType)value;
            switch (followCharacter)
            {
                case FollowCharacterType.Tab:
                    return 0;
                case FollowCharacterType.Space:
                    return 1;
                default:
                    return 2;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            switch ((int)value)
            {
                case 0:
                    return FollowCharacterType.Tab;
                case 1:
                    return FollowCharacterType.Space;
                default:
                    return FollowCharacterType.None;
            }
        }
    }
    /// <summary>
    /// Specifies the List Level Pattern for list.
    /// </summary>
    public class ListLevelPatternConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ListLevelPattern levelPattern = (ListLevelPattern)value;
            switch (levelPattern)
            {
                case ListLevelPattern.Arabic: return 0;
                case ListLevelPattern.UpRoman: return 1;
                case ListLevelPattern.LowRoman: return 2;
                case ListLevelPattern.UpLetter: return 3;
                case ListLevelPattern.LowLetter: return 4;
                case ListLevelPattern.Number: return 5;
                case ListLevelPattern.LeadingZero: return 6;
                case ListLevelPattern.Bullet: return 7;
                case ListLevelPattern.Ordinal: return 8;
                case ListLevelPattern.OrdinalText: return 9;
                case ListLevelPattern.Special: return 10;
                case ListLevelPattern.FarEast: return 11;
                default: return 12;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            switch ((int)value)
            {
                case 0: return ListLevelPattern.Arabic;
                case 1: return ListLevelPattern.UpRoman;
                case 2: return ListLevelPattern.LowRoman;
                case 3: return ListLevelPattern.UpLetter;
                case 4: return ListLevelPattern.LowLetter;
                case 5: return ListLevelPattern.Number;
                case 6: return ListLevelPattern.LeadingZero;
                case 7: return ListLevelPattern.Bullet;
                case 8: return ListLevelPattern.Ordinal;
                case 9: return ListLevelPattern.OrdinalText;
                case 10: return ListLevelPattern.Special;
                case 11: return ListLevelPattern.FarEast;
                default: return ListLevelPattern.None;
            }
        }
    }
    /// <summary>
    /// Specifies the Hilight color for button background.
    /// </summary>
    public class HighlightColorButtonBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Rectangle)
                return (value as Rectangle).Fill;
            return new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (parameter is Popup)
                (parameter as Popup).IsOpen = false;
            return value;
        }
    }
    /// <summary>
    /// Specifies the Boolean to Toggle Button checked state converter.
    /// </summary>
    public class InverseBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts the boolean value to button checked state.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType"> The type of the target property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The ToggleButton checked state.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return false;
            return value.Equals(parameter);
        }
        /// <summary>
        /// Convert back the toggle button checked state to boolean value.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The type of the target property, specified by a helper structure that wraps the type name.</param>
        /// <param name="parameter"> An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>Returns the paramere value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return parameter;
        }
    }
}
