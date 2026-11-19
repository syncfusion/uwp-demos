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
    public class NumericTextBoxProperties : NotificationObject
    {
        private int decimalDigits = 3;
        private String formatString = "N";
        private PercentDisplayMode percentDisplayMode;
        private double value1 = 2;
        private double value2 = 4;
        private double value3 = 6;

        public String FormatString
        {
            get { return formatString; }
            set { formatString = value; RaisePropertyChanged("FormatString"); }
        }

        public PercentDisplayMode PercentDisplayMode
        {
            get { return percentDisplayMode; }
            set { percentDisplayMode = value; RaisePropertyChanged("PercentDisplayMode"); }
        }

        private CultureInfo culture = new CultureInfo("en-US");

        public CultureInfo Culture
        {
            get { return culture; }
            set { culture = value; RaisePropertyChanged("Culture"); }   
        }               
       
        public double Value1
        {
            get { return value1; }
            set { value1 = value; RaisePropertyChanged("Value1"); }
        }

        public double Value2
        {
            get { return value2; }
            set { value2 = value; RaisePropertyChanged("Value2"); }
        }

        public double Value3
        {
            get { return value3; }
            set { value3 = value; RaisePropertyChanged("Value3"); }
        }


        public int DecimalDigits
        {
            get { return decimalDigits; }
            set { decimalDigits = value; RaisePropertyChanged("DecimalDigits"); }
        }
    }
}
