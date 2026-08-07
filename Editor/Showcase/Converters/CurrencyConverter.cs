using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace UnitConverter
{
    public class CurrencyConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter == null)
                return value;
 			if (value == null)
            {
                return null;
            }
            double _value = Double.Parse(value.ToString());

            if (parameter.Equals("IN"))
            {
                return Math.Round(_value * 59.66,2);
            }
            else if (parameter.Equals("UK"))
            {
                return Math.Round(_value * 0.65,2);
            }
            else if (parameter.Equals("CH"))
            {
                return Math.Round(_value * 6.18,2);
            }
            else if (parameter.Equals("GM"))
            {
                return Math.Round(_value * 0.7644,2);
            }
            else if (parameter.Equals("FR"))
            {
                return Math.Round(_value * 4.99,2);
            }
            else if (parameter.Equals("ZA"))
            {
                return Math.Round((_value * 10),2);
            }
            else if (parameter.Equals("Ja"))
            {
                return Math.Round(_value * 97.75,2);
            }
            return Math.Round(_value, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (parameter == null)
                return value;
			 if (value == null)
            {
                return null;
            }
            double _value = Double.Parse(value.ToString());

            if (parameter.Equals("IN"))
            {
                return Math.Round(_value / 59.66, 2);
            }
            else if (parameter.Equals("UK"))
            {
                return Math.Round(_value / 0.65, 2);
            }
            else if (parameter.Equals("CH"))
            {
                return Math.Round(_value / 6.18, 2);
            }
            else if (parameter.Equals("GM"))
            {
                return Math.Round(_value / 0.7644, 2);
            }
            else if (parameter.Equals("FR"))
            {
                return Math.Round(_value / 4.99, 2);
            }
            else if (parameter.Equals("ZA"))
            {
                return Math.Round((_value / 10), 2);
            }
            else if (parameter.Equals("Ja"))
            {
                return Math.Round(_value / 97.75, 2);
            }
            return Math.Round(_value, 2);
        }
    }
}
