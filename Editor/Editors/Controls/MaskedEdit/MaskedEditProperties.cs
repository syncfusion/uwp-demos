using Common;
using Syncfusion.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editors
{
    public class MaskedEditProperties : NotificationObject
    {
        
        private CultureInfo culture = new CultureInfo("en-US");

        public CultureInfo Culture
        {
            get { return culture; }
            set { culture = value; RaisePropertyChanged("Culture"); }   
        }

        private InputValidationMode validationMode=InputValidationMode.KeyPress;
        public InputValidationMode ValidationMode
        {
            get { return validationMode; }
            set { validationMode = value; RaisePropertyChanged("ValidationMode"); }
        }
                
        private object departureTime = DateTime.Now.ToString("hh:mm:ss tt", new CultureInfo("en-US").DateTimeFormat);

        public object DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; RaisePropertyChanged("DepartureTime"); }
        }
        
    }   
}
