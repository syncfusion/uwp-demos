using Common;
using Syncfusion.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editors
{
    public class NumericUpDownProperties : NotificationObject
    {
        private double smallChange = 1;

        public double SmallChange
        {
            get { return smallChange; }
            set { smallChange = value; RaisePropertyChanged("SmallChange"); }
        }

        private double largeChange = 5;

        public double LargeChange
        {
            get { return largeChange; }
            set { largeChange = value; RaisePropertyChanged("LargeChange"); }
        }

        private double minimum = 0;

        public double Minimum
        {
            get { return minimum; }
            set
            {
                if (value < Maximum)
                    minimum = value;
                else
                    minimum = maximum;
               
                RaisePropertyChanged("Minimum");
            }
        }

        private double maximum = 100;

        public double Maximum
        {
            get { return maximum; }
            set
            {
                if (value > Minimum)
                    maximum = value;
                RaisePropertyChanged("Maximum");
            }
        }

        private SpinButtonsAlignment spinButtonsAlignment;

        public SpinButtonsAlignment SpinButtonsAlignment
        {
            get { return spinButtonsAlignment; }
            set { spinButtonsAlignment = value; RaisePropertyChanged("SpinButtonsAlignment"); }
        }

        private bool autoreverse = true;

        public bool AutoReverse
        {
            get { return autoreverse; }
            set { autoreverse = value; RaisePropertyChanged("AutoReverse"); }
        }       
        

        

    }
}
