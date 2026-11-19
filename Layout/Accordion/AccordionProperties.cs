using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Animation;
using Syncfusion.UI.Xaml.Controls.Layout;
using System.ComponentModel;

namespace Layout.Accordion
{
    public class AccordionProperties : NotificationObject
    {
       
        private AccordionSelectionMode mode;

        public AccordionSelectionMode Mode
        {
            get { return mode; }
            set { mode = value; RaisePropertyChanged("Mode");}
        }

    }

    public class NotificationObject : INotifyPropertyChanged
    {
        public void RaisePropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
