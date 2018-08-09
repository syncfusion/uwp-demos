using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Syncfusion.UI.Xaml.Grid;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using System.Globalization;
using Syncfusion.SampleBrowser.UWP.SfDataGrid;
using Windows.UI.Xaml.Markup;

namespace DataGrid
{

#if !SFDATAGRID_STORE
    /// <summary>
    /// This class represents the boolean to Visibility converter 
    /// </summary>
    public class BoolToVisiblityConverter : IValueConverter
    {
        /// <summary>
        /// Convert value to Visibility
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Visibility)value == Visibility.Visible;
        }
    }

    /// <summary>
    /// This class represents the Visibility based on condition 
    /// </summary>
    class ConditionHeadingVisibility : IValueConverter
    {
        /// <summary>
        /// Convert value to Visibility
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var index = value as int?;
            return index > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class represents the EditTriggerOption 
    /// </summary>
    class EditTriggerOptionConverter : IValueConverter
    {
        /// <summary>
        /// Convert value to EditTrigger
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var index = value as int?;
            return index == 0 ? EditTrigger.OnTap : EditTrigger.OnDoubleTap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    
    /// <summary>
    /// This class represents the AddNewRowPosition
    /// </summary>
    class AddNewRowPositionConverter : IValueConverter
    {
        /// <summary>
        /// Convert value to AddNewRowPosition
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var index = value as int?;

            return index == 0 ? AddNewRowPosition.None : index == 1 ? AddNewRowPosition.Top : index == 2 ? AddNewRowPosition.Bottom : AddNewRowPosition.FixedTop;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class represents the StringComboVisibility
    /// </summary>
    class StringComboVisibility : IValueConverter
    {
        /// <summary>
        /// Convert value to Visibility
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var index = value as int?;
            if (index == 3 || index == 4)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class represents the NumericComboVisibility 
    /// </summary>
    class NumericComboVisibility : IValueConverter
    {
        /// <summary>
        /// Convert value to Visibility
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var index = value as int?;
            if (index == 1 || index == 2)
                return Visibility.Visible;
            return Visibility.Collapsed; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class represents the SelectionMode 
    /// </summary>
    class SelectionModeConverter : IValueConverter
    {
        /// <summary>
        /// Convert value to GridSelectionMode
        /// </summary>
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            var index = value as int?;
            switch (index)
            {
                case 0:
                    return GridSelectionMode.Single;
                case 1:
                    return GridSelectionMode.Multiple;
                case 2:
                    return GridSelectionMode.Extended;
                case 3:
                    return GridSelectionMode.None;
                default:
                    return null;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class represents the GridOrientation
    /// </summary>
    class GridOrientationConverter : IValueConverter
    {
        /// <summary>
        /// Convert value to Orientation
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var selectedIndex = (int) value;
            return selectedIndex == 0 ? Orientation.Horizontal : Orientation.Vertical;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }


    /// <summary>
    /// This class represents the SelectedImageConverter
    /// </summary>
    class SelectedImageConverter : IValueConverter
    {
        /// <summary>
        /// Convert string to image
        /// </summary>
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter is Storyboard)
                (parameter as Storyboard).Begin();

            if (value != null && language.ToString() == "selected")
            {
                var product = value as ProductInfo;
                return @"Assets/"+product.ProductModel+".png";
            }
            else if (value != null && parameter.ToString() == "Availability" && (bool)value)
                return @"Assets/yes.png";
            else if (value != null && parameter.ToString() == "Availability")
                return @"Assets/no.png";
            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class represents the SortClickActionConveter
    /// </summary>
    class SortClickActionConveter : IValueConverter
    {
        /// <summary>
        /// Convert int to SortClickAction
        /// </summary>
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            var index = value as int?;
            return index == 0 ? SortClickAction.SingleClick : SortClickAction.DoubleClick;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class represents the ChartVisiblityConverter
    /// </summary>
    class ChartVisiblityConverter : IValueConverter
    {
        /// <summary>
        /// Convert value to Visibility
        /// </summary>
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null && parameter is Storyboard)
                ((Storyboard)parameter).Begin();

            if (language.Equals("indicator") && value == null)
                return Visibility.Visible;
            if (language.Equals("indicator") && value != null)
                return Visibility.Collapsed;
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    
    /// <summary>
    /// This class represents the ValidationOptionConverter
    /// </summary>
    internal class ValidationOptionConverter : IValueConverter
    {
        /// <summary>
        /// Convert int to GridValidationMode
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int? index = value as int?;
            switch (index)
            {
                case 0:
                    return GridValidationMode.InView;
                default:
                    return GridValidationMode.None;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class represents the GridColumnSizerConverter
    /// </summary>
    internal class GridColumnSizerConverter : IValueConverter
    {
        /// <summary>
        /// Convert int to GridLengthUnitType
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int? index = value as int?;
            if (index == 0)
                return GridLengthUnitType.Auto;
            else if (index == 1)
                return GridLengthUnitType.AutoLastColumnFill;
            else if (index == 2)
                return GridLengthUnitType.AutoWithLastColumnFill;
            else if (index == 3)
                return GridLengthUnitType.None;
            else if (index == 4)
                return GridLengthUnitType.SizeToCells;
            else if (index == 5)
                return GridLengthUnitType.SizeToHeader;
            else
                return GridLengthUnitType.Star;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
#endif
    /// <summary>
    /// This class represents the StringToImageConverter
    /// </summary>
    public class StringToImageConverter : IValueConverter
   {
        /// <summary>
        /// Convert string to Image
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
       {           
           string imagename = value as string;           

           if (value == null)
               return value;
           
           imagename = value.ToString();

           if (imagename.Equals("Total Sales By Month"))
               imagename = "icon_total.png";
           else if (imagename.Equals("Average of Selected Rows"))
               imagename = "icon_add.png";
           else if (imagename.Equals("Summary of Selected Rows"))
               imagename = "icon_sum.png";
           else if (imagename.Equals("Percent of Selected Rows"))
               imagename = "icon_percentage.png";
           else
               return "Assets/" + imagename + ".png";

           return @"Assets/" + imagename;

       }

       public object ConvertBack(object value, Type targetType, object parameter, string language)
       {
           throw new NotImplementedException();
       }
   }

#if !SFDATAGRID_STORE
    /// <summary>
    /// This class represents the MergedCellImageConverter
    /// </summary>
    public class MergedCellImageConverter : IValueConverter
    {
        /// <summary>
        /// Convert string to Image
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string imagename = value as string;
            return "Assets/" + imagename + ".png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    /// <summary>
    /// This class represents the SelectionUnitConverter
    /// </summary>
    public class SelectionUnitConverter : IValueConverter
    {
        /// <summary>
        ///  Convert int to GridSelectionUnit
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int? index = value as int?;
            switch (index)
            {
                case 0:
                    return GridSelectionUnit.Cell;
                case 1:
                    return GridSelectionUnit.Row;
                case 2:
                    return GridSelectionUnit.Any;
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
#endif
    /// <summary>
    /// This class represents the StockChangeConverter
    /// </summary>
    class StockChangeConverter : IValueConverter
    {
        /// <summary>
        ///  Convert value to string
        /// </summary>
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var data = value as double?;
                if (data > 0)
                    return "F1M1719.66,218.12L1735.66,246.166 1751.66,274.21 1719.66,274.21 1687.66,274.21 1703.66,246.166 1719.66,218.12z";
                else
                    return "F1M1464.78,374.21C1466.31,374.21,1466.94,375.538,1466.17,376.861L1435.89,429.439C1435.12,430.759,1433.87,430.823,1433.11,429.5L1402.82,376.827C1402.06,375.507,1402.69,374.21,1404.21,374.21L1464.78,374.21z";
            }
            return "";
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    
#if !SFDATAGRID_STORE

    /// <summary>
    /// This class represents the GroupDataTimeConverter
    /// </summary>
    public class GroupDataTimeConverter : IValueConverter
    {
        /// <summary>
        /// Convert value to DateTime
        /// </summary>
        public object Convert(object value, System.Type targetType, object parameter, string language)
        {
            var saleinfo = value as SalesByDate;
            var dt = DateTime.Now;
            var days = (int)Math.Floor((dt - saleinfo.Date).TotalDays);
            var dayofweek = (int)dt.DayOfWeek;
            var diff = days - dayofweek;

            if (days <= dayofweek)
            {
                if (days == 0)
                    return "TODAY";
                if (days == 1)
                    return "YESTERDAY";
                return saleinfo.Date.DayOfWeek.ToString().ToUpper();
            }
            if (diff > 0 && diff <= 7)
                return "LAST WEEK";
            if (diff > 7 && diff <= 14)
                return "TWO WEEKS AGO";
            if (diff > 14 && diff <= 21)
                return "THREE WEEKS AGO";
            if (dt.Year == saleinfo.Date.Year && dt.Month == saleinfo.Date.Month)
                return "EARLIER THIS MONTH";
            if (DateTime.Now.AddMonths(-1).Month == saleinfo.Date.Month)
                return "LAST MONTH";
            return "OLDER";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language)
        {
            throw new System.NotImplementedException();
        }
    }
#endif
    /// <summary>
    /// This class represents the ImageConverter
    /// </summary>
    public class ImageConverter : IValueConverter
    {
        /// <summary>
        /// Convert value to Image
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var data = value as double?;
                if (data > 30000.00)
                    return "F1M1719.66,218.12L1735.66,246.166 1751.66,274.21 1719.66,274.21 1687.66,274.21 1703.66,246.166 1719.66,218.12z";
                else
                    return
                        "F1M1464.78,374.21C1466.31,374.21,1466.94,375.538,1466.17,376.861L1435.89,429.439C1435.12,430.759,1433.87,430.823,1433.11,429.5L1402.82,376.827C1402.06,375.507,1402.69,374.21,1404.21,374.21L1464.78,374.21z";
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
