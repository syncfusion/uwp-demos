using Common;
using Syncfusion.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;



namespace Editors
{
    public class RangeSliderProperties : NotificationObject
    {

        private bool showrange = true;
        private bool reversedirection = false;
        private int minimum = 0;
        private int maximum = 100;
        private int ticks = 20;
        private Syncfusion.UI.Xaml.Controls.Input.TickPlacement tickplacement = Syncfusion.UI.Xaml.Controls.Input.TickPlacement.BottomRight;

        public bool ShowRange
        {
            get { return showrange; }
            set { showrange = value; RaisePropertyChanged("ShowRange"); }
        }

        public int Ticks
        {
            get { return ticks; }
            set { ticks = value; RaisePropertyChanged("Ticks"); }
        }

        public bool ReverseDirection
        {
            get { return reversedirection; }
            set { reversedirection = value; RaisePropertyChanged("ReverseDirection"); }
        }

        public TickPlacement TickPlacement  
        {
            get { return tickplacement; }
            set { tickplacement = value; RaisePropertyChanged("TickPlacement"); }
        }       

        public int Minimum
        {
            get { return minimum; }
            set
            {
                if (value <= 30)
                {
                    minimum = value;
                }
                else
                    minimum = 30;
                RaisePropertyChanged("Minimum");
            }
        }
        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value <= 10000)
                {
                    maximum = value;
                }
                else
                    maximum = 10000; 
                RaisePropertyChanged("Maximum");
            }
        }

        private MovePoint _moveToPoint=MovePoint.IncrementByLargeChange;

        public MovePoint MoveToPoint
        {
            get { return _moveToPoint; }
            set { _moveToPoint = value; RaisePropertyChanged("MoveToPoint"); }
        }
      
        private int largechange = 20;

        public int Largechange
        {
            get { return largechange; }
            set { largechange = value; RaisePropertyChanged("Largechange"); }
        }

        private int smallchange = 5;

        public int SmallChange
        {
            get { return smallchange; }
            set { smallchange = value; RaisePropertyChanged("SmallChange"); }
        }

        private SliderSnapsTo snapto = SliderSnapsTo.Ticks;

        public SliderSnapsTo SnapTo
        {
            get { return snapto; }
            set { snapto = value; RaisePropertyChanged("SnapTo"); }
        }
        

    }

    public enum SliderOrientation
    {
        Vertical,
        Horizontal
    }
}
