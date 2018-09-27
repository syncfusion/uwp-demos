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
    public class TimePickerProperties : NotificationObject
    {
        private String formatString = "t";

        public String FormatString
        {
            get { return formatString; }
            set { formatString = value; RaisePropertyChanged("FormatString"); }
        }
    }
}
