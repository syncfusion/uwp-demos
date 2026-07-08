using Common;
using Syncfusion.UI.Xaml.Controls.Notification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SampleBrowser.ProgressBar
{
    public class ProgressTypesProperties : NotificationObject
    {
        DispatcherTimer timer;

        private ProgressTypes progressType = ProgressTypes.SolidCircular;
        public ProgressTypes ProgressType
        {
            get { return progressType; }
            set
            {
                progressType = value;
                UpdateLayout();
                RaisePropertyChanged("ProgressType");
            }
        }

        private Directions fillDirection=Directions.Horizontal;
        public Directions FillDirection
        {
            get { return fillDirection; }
            set
            {
                fillDirection = value;
                RaisePropertyChanged("FillDirection");
            }
        }

        private DisplayContentMode displaycontent = DisplayContentMode.Percentage;
        public DisplayContentMode DisplayContentMode
        {
            get { return displaycontent; }
            set
            {
                displaycontent = value;
                RaisePropertyChanged("DisplayContentMode");
            }
        }

        private double height = 200.0;
        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                RaisePropertyChanged("Height");
            }
        }

        private double width = 200.0;
        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                RaisePropertyChanged("Width");
            }
        }
              
        private double _value = 0.0;
        public double Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged("Value"); }
        }

        private double fontSize = 14.0;
        public double FontSize
        {
            get { return fontSize; }
            set { fontSize = value; RaisePropertyChanged("FontSize"); }
        }
        
        private bool customFill = false;
        public bool IsShapes
        {
            get { return customFill; }
            set { customFill = value; RaisePropertyChanged("IsShapes"); }
        }
               
        private Shapes shape;
        public Shapes Shape
        {
            get { return shape; }
            set { shape = value;
                RaisePropertyChanged("Shape"); }
        }
        public ProgressTypesProperties()
        {           
            timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.1) };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            if (Value == 100.0)
                Value = 0.0;
            else
                Value += 1.0;            
        }

        private void UpdateLayout()
        {
            timer.Stop();
            IsShapes = false;
            Value = 0.0;
            DisplayContentMode = DisplayContentMode.Percentage;
            switch (ProgressType)
            {
                case ProgressTypes.SolidCircular:
                    {
                        Width = 200.0;
                        Height = 200.0;
                        FontSize = 30.0;
                        timer.Start();
                    }
                    break;
                case ProgressTypes.SolidLinear:
                    {
                        if (DeviceFamily.GetDeviceFamily()==Devices.Mobile)
                            Width = 300.0;
                        else
                            Width = 500.0;
                        Height = 40.0;
                        FontSize = 20.0;
                        timer.Start();
                    }
                    break;
                case ProgressTypes.SegmentedCircular:
                    {
                        Width = 150.0;
                        Height = 150.0;
                        FontSize = 30.0;
                        timer.Start();
                    }
                    break;
                case ProgressTypes.SegmentedLinear:
                    {
                        if (DeviceFamily.GetDeviceFamily()==Devices.Mobile)
                            Width = 300.0;
                        else
                            Width = 500.0;
                        Height = 40.0;
                        FontSize = 20.0;
                        timer.Start();
                    }
                    break;
                case ProgressTypes.Shape:
                    {
                        DisplayContentMode = DisplayContentMode.None;
                        SetFillDirection();
                        SetSize();
                        IsShapes = true;
                        timer.Start();
                    }
                    break;
                default:
                    break;
            }
        }

        internal void SetFillDirection()
        {
            if(Shape == Shapes.Ball
                || Shape == Shapes.Man
                || Shape == Shapes.Woman
                || Shape == Shapes.DNA
                || Shape == Shapes.Flower
                || shape == Shapes.Droplet)
            {
               FillDirection = Directions.Vertical;
            }
            else
                FillDirection = Directions.Horizontal;
        }

        private void SetSize()
        {
            if (Shape == Shapes.Cloud
                            || Shape == Shapes.Droplet
                            || Shape == Shapes.Circle
                            || Shape == Shapes.Hexagon)
            {
                Height = 170.0;
                Width = 170.0;
            }
            else
            {
                Height = 300.0;
                Width = 300.0;
            }
        }

        internal void Dispose()
        {
            timer.Stop();
            timer.Tick -= Timer_Tick;
            timer = null;
        }
    }
}
