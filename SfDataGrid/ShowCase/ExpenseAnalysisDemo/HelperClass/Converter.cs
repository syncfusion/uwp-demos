using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Syncfusion.UI.Xaml.Charts;
using System.Collections.ObjectModel;

namespace ExpenseAnalysisDemo
{
    class DateConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {            
            return Convert.ToDateTime(value).ToString(@"MMM dd yyyy");            
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return string.Format("{0:C2}", value);            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }    

    class BoolToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    class GridCellColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var source = value as ExpenseData;
            if (source.AccountType == AccountType.Positve)
                return new SolidColorBrush(Colors.Green);
            else
                return new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    public class LabelColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartPieAdornment pieAdornment = value as ChartPieAdornment;
            int index = pieAdornment.Series.Adornments.IndexOf(pieAdornment);
            return pieAdornment.Series.ColorModel.GetMetroBrushes()[index];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class LabelTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartPieAdornment pieAdornment = value as ChartPieAdornment;
            int index = pieAdornment.Series.Adornments.IndexOf(pieAdornment);
            ObservableCollection<CompanyExpense> items = pieAdornment.Series.ItemsSource as ObservableCollection<CompanyExpense>;

            return (string)items[index].Category + "\n" + " $" + items[index].Amount;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
