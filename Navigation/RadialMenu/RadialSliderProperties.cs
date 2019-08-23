#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.RadialMenu
{
   public class RadialSliderProperties : NotificationObject
   {
       private double sliderValue;
       public double SliderValue
       {
           get { return sliderValue; }
           set { sliderValue = value; RaisePropertyChanged("SliderValue"); }
       }

       private double pointerFactor = 0.75d;
       public double PointerFactor
       {
           get { return pointerFactor; }
           set { pointerFactor = value; RaisePropertyChanged("PointerFactor"); }

       }

        private int tickFrequency = 10;
        public int TickFrequency
        {
            get
            {
                return tickFrequency;
            }
            set
            {
                tickFrequency = value;
                RaisePropertyChanged("TickFrequency");
            }
        }

        private bool showMaximum = false;
        public bool ShowMaximum
        {
            get
            {
                return showMaximum;
            }
            set
            {
                showMaximum = value;
                RaisePropertyChanged("ShowMaximum");
            }
        }
   }
}
