using Common;
using Syncfusion.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input.DateTimePickers
{
    public class DatePickerProperties : NotificationObject
    {
        private String formatString = "d";

        public String Format
        {
            get { return formatString; }
            set { formatString = value; RaisePropertyChanged("Format"); }
        }
    }
}
