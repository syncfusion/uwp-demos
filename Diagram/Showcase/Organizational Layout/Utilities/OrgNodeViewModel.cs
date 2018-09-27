using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Syncfusion.SampleBrowser.UWP.Diagram;

namespace OrganizationalChartDemo
{
    public class OrgNodeViewModel :NodeViewModel
    {

        public OrgNodeViewModel()
        {
           UnitWidth = 185;
           UnitHeight = 65;
            var resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Template/OrgNodeContentTemplate.xaml", UriKind.Absolute)
            };

            ContentTemplate = resourceDictionary["employeeContentTemplate"] as DataTemplate;
            Constraints = Constraints | NodeConstraints.AllowPan;
        }

        private bool isadded=false;

        public bool IsNew
        {
            get
            {
                return isadded;
            }
            set
            {
                if (isadded != value)
                {
                    isadded = value;
                    OnPropertyChanged("IsNew");
                }
            }
        }
        private int index;
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                if (index != value)
                {
                    index = value;
                    OnPropertyChanged("Index");
                }
            }
        }
        private string _name;

        public string Display
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Display");
                }
            }
        }
        private bool visible = true;

        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    OnPropertyChanged("Visible");
                }
            }
        }


        private double _x;
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged("X");
                }
            }
        }

        private double _y;
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (_y != value)
                {
                   _y = value;
                    OnPropertyChanged("Y");
                }
            }
        }

        private Point calc;

        public Point Offset
        {
            get
            {
                return calc;
            }
            set
            {
                if (calc != value)
                {
                    calc = value;
                    OnPropertyChanged("Offset");
                }
            }
        }

        private Popup _custom = new Popup();
        public Popup CustomToolTip
        {
            get
            {
                return _custom;
            }
            set
            {
                if (_custom != value)
                {
                    _custom = value;
                    OnPropertyChanged("CustomToolTip");
                }
            }
        }

        private State _previousstate;

        public State PreviousState
        {
            get
            {
                return _previousstate;
            }
            set
            {
                if (_previousstate != value)
                {
                    _previousstate = value;
                    OnPropertyChanged("PreviousState");
                }
            }
        }
    }

    public class OrgConnectorViewModel : ConnectorViewModel
    {
        public OrgConnectorViewModel()
        {
            SolidColorBrush solid = new SolidColorBrush();
            solid.Color = Color.FromArgb(255, 186, 186, 186);
            this.ConnectorGeometryStyle = GetStyle(solid);
        }

        private Style GetStyle(SolidColorBrush solid)
        {
            Style s = new Style();
            s.TargetType = typeof(Windows.UI.Xaml.Shapes.Path);
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, solid));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeThicknessProperty, 1.20));
            return s;
        }

        private bool visible = true;

        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    OnPropertyChanged("Visible");
                }
            }
        }

        private double _opacity=1;

        public double ConnectorOpacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                if (_opacity != value)
                {
                    _opacity = value;
                    OnPropertyChanged("ConnectorOpacity");
                }
            }
        }
    }
}
