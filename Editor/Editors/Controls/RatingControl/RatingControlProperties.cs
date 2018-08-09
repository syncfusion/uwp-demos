using Common;
using Syncfusion.UI.Xaml.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Editors.Controls.RatingControl
{
    public class RatingProperties : NotificationObject
    {
        private Precision precision;

        private bool showToolTip = true;
        private bool isReadOnly = false;

        public Precision Precision
        {
            get { return precision; }
            set { precision = value; RaisePropertyChanged("Precision"); }
        }


        public bool ShowToolTip  
        {
            get { return showToolTip; }
            set { showToolTip = value; RaisePropertyChanged("ShowToolTip"); }
        }

        public bool IsReadOnly  
        {
            get { return isReadOnly; }
            set { isReadOnly = value; RaisePropertyChanged("IsReadOnly"); }
        }
    }
}
