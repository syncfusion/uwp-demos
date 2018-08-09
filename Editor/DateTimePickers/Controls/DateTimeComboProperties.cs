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
    public class DateTimeComboProperties : NotificationObject
    {
        private String dateformatString = "M/d/yyyy";

        public String DateFormat
        {
            get { return dateformatString; }
            set { dateformatString = value; RaisePropertyChanged("DateFormat"); }
        }   

        private int _minute=5;

        public int Minute
        {
            get { return _minute; }
            set { _minute = value; RaisePropertyChanged("Minute"); }
        }

        private int _second = 15;

        public int Second
        {
            get { return _second; }
            set { _second = value; RaisePropertyChanged("Second"); }
        }

        private DateTime _mindate=Convert.ToDateTime("11/9/2012");
        public DateTime Mindate
        {
            get { return _mindate; }
            set { _mindate = value; RaisePropertyChanged("Mindate"); }
        }

        private DateTime _maxdate = Convert.ToDateTime("11/9/2014");
                
        public DateTime Maxdate
        {
            get { return _maxdate; }
            set { _maxdate = value; RaisePropertyChanged("Maxdate"); }
        }
        
        

        private String timeformatString = "h/m/s/t";

        public String TimeFormat
        {
            get { return timeformatString; }
            set { timeformatString = value; RaisePropertyChanged("TimeFormat"); }
        }
    }
}
