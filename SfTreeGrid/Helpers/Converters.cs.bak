#region Copyright Syncfusion Inc. 2001 - 2016
// Copyright Syncfusion Inc. 2001 - 2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Cells;
using Syncfusion.UI.Xaml.TreeGrid;
using Syncfusion.UI.Xaml.TreeGrid.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Syncfusion.SampleBrowser.UWP.SfTreeGrid
{   
    public class SearchConditionVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Convert value to Visibility
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var index = value as int?;
            string text = parameter.ToString();
            if (text.Equals("SearchCondition"))
            {
                if (index > 0)
                    return Visibility.Visible;
            }
            else if (text.Equals("NumericComboBox"))
            {
                if (index == 3 || index == 6)
                    return Visibility.Visible;
            }
            else if (text.Equals("StringComboBox"))
            {
                if (index == 1 || index == 2 || index == 4 || index == 5)
                    return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class FilterLevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int? index = value as int?;
            if (index == 0)
                return FilterLevel.All;
            else if (index == 1)
                return FilterLevel.Root;
            else
                return FilterLevel.Extended;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    } 

    internal class CurrencyFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return String.Format("{0:C}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
#if !SFTREEGRID_STORE
    internal class SortClickActionConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var index = value as int?;
            return index == 0 ? SortClickAction.SingleClick : SortClickAction.DoubleClick;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class represents the GridColumnSizerConverter
    /// </summary>
    internal class TreeGridColumnSizerConverter : IValueConverter
    {
        /// <summary>
        /// Convert int to TreeColumnSizer
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int? index = value as int?;
            if (index == 0)
                return TreeColumnSizer.Auto;
            else if (index == 1)
                return TreeColumnSizer.AutoFillColumn;
            else if (index == 2)
                return TreeColumnSizer.FillColumn;
            else if (index == 3)
                return TreeColumnSizer.None;
            else if (index == 4)
                return TreeColumnSizer.SizeToCells;
            else if (index == 5)
                return TreeColumnSizer.SizeToHeader;
            else
                return TreeColumnSizer.Star;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    internal class EditTriggerOptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int? index = value as int?;
            if (index == 0)
                return EditTrigger.OnTap;
            else
                return EditTrigger.OnDoubleTap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    
    internal class StyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((value as PersonInfo).Salary < 10000)
                return new SolidColorBrush(Colors.Transparent);
            else if ((value as PersonInfo).Salary > 10000 && (value as PersonInfo).Salary < 50000)
                return new SolidColorBrush(Color.FromArgb(255, 255, 211, 86));
            else if ((value as PersonInfo).Salary > 50000 && (value as PersonInfo).Salary < 100000)
                return new SolidColorBrush(Color.FromArgb(255, 112, 252, 160));
            return new SolidColorBrush();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    internal class GridValidationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int? index = value as int?;
            if (index == 0)
                return GridValidationMode.InView;
            else
                return GridValidationMode.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

#endif 
    internal class CheckBoxSelectionModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int? index = value as int?;
            switch (index)
            {
                case 0:
                    return CheckBoxSelectionMode.Default;
                case 1:
                    return CheckBoxSelectionMode.SelectOnCheck;
                case 2:
                    return CheckBoxSelectionMode.SynchronizeSelection;

                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    internal class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var datacontext = value as DataContextHelper;
            if (datacontext.Value.ToString() == "Inbox" || datacontext.Value.ToString() == "Drafts" || datacontext.Value.ToString() == "Sent Items"
                || datacontext.Value.ToString() == "Deleted Items" || datacontext.Value.ToString() == "Calendar" || datacontext.Value.ToString() == "Contacts")
                return "ms-appx:///Images///" + datacontext.Value.ToString() + ".png";
            return "ms-appx:///Images///" + "Folder.png";

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

}

