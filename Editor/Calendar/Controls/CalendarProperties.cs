using Common;
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input.Calendar
{
    class CalendarProperties : NotificationObject
    {
        private SelectionMode selectionModes;

        public SelectionMode SelectionModes
        {
            get { return selectionModes; }
            set { selectionModes = value;
            RaisePropertyChanged("SelectionModes");
            }
        }

        private DayOfWeek firstDay;

        public DayOfWeek FirstDay
        {
            get { return firstDay; }
            set
            {
                firstDay = value;
                RaisePropertyChanged("FirstDay");
            }
        }

        private DateTime mindate=DateTime.Now.AddMonths(-1);   

        public DateTime Mindate
        {
            get { return mindate; }
            set { mindate = value;
			RaisePropertyChanged("Mindate");			}
        }

        private DateTime maxdate = DateTime.Now.AddMonths(1);

        public DateTime Maxdate
        {
            get { return maxdate; }
            set { maxdate = value;
			RaisePropertyChanged("Maxdate");	
			}
        }
        
    }
}
