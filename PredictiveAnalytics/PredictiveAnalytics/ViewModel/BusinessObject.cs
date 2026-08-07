using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SampleBrowser.UWP.PredictiveAnalytics
{
    public class BusinessObject
    {


        Dictionary<string, object> temp = new Dictionary<string, object>();
        public Dictionary<string, object> DictValueProperty
        {
            get { return temp; }
            set { temp = value; }
        }


        public BusinessObject()
        {

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
