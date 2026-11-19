using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Mockup.ViewModel
{
    public class ConnectorVM : GroupableVM, IConnectorVM
    {
        public ConnectorVM()
        {
            Stroke = new SolidColorBrush(new Color() { A = 0xFF, R = 0x7F, G = 0x7F, B = 0x7F });
        }

        #region IConnector
        AnnotationConstraints _mAnnotationConstraints = AnnotationConstraints.Default;
        /// <summary>
        ///  AnnotationConstraints
        /// </summary>
        public AnnotationConstraints AnnotationConstraints
        {
            get
            {
                return _mAnnotationConstraints;
            }
            set
            {
                if (_mAnnotationConstraints != value)
                {
                    _mAnnotationConstraints = value;
                    OnPropertyChanged(ConnectorConstants.AnnotationConstraints);
                }
            }
        }
        double _mHitPadding = 23d;
        /// <summary>
        ///  HitPadding
        /// </summary>
        public double HitPadding
        {
            get
            {
                return _mHitPadding;
            }
            set
            {
                if (_mHitPadding != value)
                {
                    _mHitPadding = value;
                    OnPropertyChanged(ConnectorConstants.HitPadding);
                }
            }
        }

        object _mSegmentDecorators = null;
        /// <summary>
        ///  SegmentDecorators
        /// </summary>
        public object SegmentDecorators
        {
            get
            {
                return _mSegmentDecorators;
            }
            set
            {
                if (_mSegmentDecorators != value)
                {
                    _mSegmentDecorators = value;
                    OnPropertyChanged(ConnectorConstants.SegmentDecorators);
                }
            }
        }

        Style _mSegmentDecoratorStyle = null;
        /// <summary>
        ///  SegmentDecoratorStyle
        /// </summary>
        public Style SegmentDecoratorStyle
        {
            get
            {
                return _mSegmentDecoratorStyle;
            }
            set
            {
                if (_mSegmentDecoratorStyle != value)
                {
                    _mSegmentDecoratorStyle = value;
                    OnPropertyChanged(ConnectorConstants.SegmentDecoratorStyle);
                }
            }
        }

        Point _mSourceDecoratorPivot = new Point(1, 0.5);
        /// <summary>
        ///  SourceDecoratorPivot
        /// </summary>
        public Point SourceDecoratorPivot
        {
            get
            {
                return _mSourceDecoratorPivot;
            }
            set
            {
                if (_mSourceDecoratorPivot != value)
                {
                    _mSourceDecoratorPivot = value;
                    OnPropertyChanged(ConnectorConstants.SourceDecoratorPivot);
                }
            }
        }


        Point _mTargetDecoratorPivot = new Point(1, 0.5);
        /// <summary>
        ///  TargetDecoratorPivot
        /// </summary>
        public Point TargetDecoratorPivot
        {
            get
            {
                return _mTargetDecoratorPivot;
            }
            set
            {
                if (_mTargetDecoratorPivot != value)
                {
                    _mTargetDecoratorPivot = value;
                    OnPropertyChanged(ConnectorConstants.TargetDecoratorPivot);
                }
            }
        }
        double _mCornerRadius = 0;
        public double CornerRadius
        {
            get
            {
                return _mCornerRadius;
            }
            set
            {
                if (_mCornerRadius != value)
                {
                    _mCornerRadius = value;
                    OnPropertyChanged("CornerRadius");
                }
            }
        }


        object _mSourceNode = null;
        public object SourceNode
        {
            get
            {
                return _mSourceNode;
            }
            set
            {
                if (_mSourceNode == null || !_mSourceNode.Equals(value))
                {
                    _mSourceNode = value;
                    OnPropertyChanged(ConnectorConstants.SourceNode);
                }
            }
        }


        object _mTargetNode = null;
        public object TargetNode
        {
            get
            {
                return _mTargetNode;
            }
            set
            {
                if (_mTargetNode == null || !_mTargetNode.Equals(value))
                {
                    _mTargetNode = value;
                    OnPropertyChanged(ConnectorConstants.TargetNode);
                }
            }
        }


        IPort _mSourcePort = null;
        public IPort SourcePort
        {
            get
            {
                return _mSourcePort;
            }
            set
            {
                if (_mSourcePort != value)
                {
                    _mSourcePort = value;
                    OnPropertyChanged(ConnectorConstants.SourcePort);
                }
            }
        }


        IPort _mTargetPort = null;
        public IPort TargetPort
        {
            get
            {
                return _mTargetPort;
            }
            set
            {
                if (_mTargetPort != value)
                {
                    _mTargetPort = value;
                    OnPropertyChanged(ConnectorConstants.TargetPort);
                }
            }
        }


        object _mSegments = null;
        public object Segments
        {
            get
            {
                return _mSegments;
            }
            set
            {
                if (_mSegments != value)
                {
                    _mSegments = value;
                    OnPropertyChanged(ConnectorConstants.Segments);
                }
            }
        }


        Point _mSourcePoint = new Point(0, 0);
        public Point SourcePoint
        {
            get
            {
                return _mSourcePoint;
            }
            set
            {
                if (_mSourcePoint != value)
                {
                    _mSourcePoint = value;
                    OnPropertyChanged(ConnectorConstants.SourcePoint);
                }
            }
        }


        Point _mTargetPoint = new Point(0, 0);
        public Point TargetPoint
        {
            get
            {
                return _mTargetPoint;
            }
            set
            {
                if (_mTargetPoint != value)
                {
                    _mTargetPoint = value;
                    OnPropertyChanged(ConnectorConstants.TargetPoint);
                }
            }
        }


        Style _mConnectorGeometryStyle = null;
        public Style ConnectorGeometryStyle
        {
            get
            {
                return _mConnectorGeometryStyle;
            }
            set
            {
                if (_mConnectorGeometryStyle != value)
                {
                    _mConnectorGeometryStyle = value;
                    OnPropertyChanged(ConnectorConstants.ConnectorGeometryStyle);
                }
            }
        }


        object _mSourceDecorator = null;
        public object SourceDecorator
        {
            get
            {
                return _mSourceDecorator;
            }
            set
            {
                if (_mSourceDecorator != value)
                {
                    _mSourceDecorator = value;
                    OnPropertyChanged(ConnectorConstants.SourceDecorator);
                }
            }
        }


        object _mTargetDecorator = null;
        public object TargetDecorator
        {
            get
            {
                return _mTargetDecorator;
            }
            set
            {
                if (_mTargetDecorator != value)
                {
                    _mTargetDecorator = value;
                    OnPropertyChanged(ConnectorConstants.TargetDecorator);
                }
            }
        }


        Style _mSourceDecoratorStyle = null;
        public Style SourceDecoratorStyle
        {
            get
            {
                return _mSourceDecoratorStyle;
            }
            set
            {
                if (_mSourceDecoratorStyle != value)
                {
                    _mSourceDecoratorStyle = value;
                    OnPropertyChanged(ConnectorConstants.SourceDecoratorStyle);
                }
            }
        }


        Style _mTargetDecoratorStyle = null;
        public Style TargetDecoratorStyle
        {
            get
            {
                return _mTargetDecoratorStyle;
            }
            set
            {
                if (_mTargetDecoratorStyle != value)
                {
                    _mTargetDecoratorStyle = value;
                    OnPropertyChanged(ConnectorConstants.TargetDecoratorStyle);
                }
            }
        }


        ConnectorConstraints _mConstraints = ConnectorConstraints.Default;
        public ConnectorConstraints Constraints
        {
            get
            {
                return _mConstraints;
            }
            set
            {
                if (_mConstraints != value)
                {
                    _mConstraints = value;
                    OnPropertyChanged(ConnectorConstants.Constraints);
                }
            }
        }


        BezierSmoothness _mBezierSmoothness = BezierSmoothness.None;
        public BezierSmoothness BezierSmoothness
        {
            get
            {
                return _mBezierSmoothness;
            }
            set
            {
                if (_mBezierSmoothness != value)
                {
                    _mBezierSmoothness = value;
                    OnPropertyChanged(ConnectorConstants.BezierSmoothness);
                }
            }
        }


        double _mBridgeSpace = 15d;
        public double BridgeSpace
        {
            get
            {
                return _mBridgeSpace;
            }
            set
            {
                if (_mBridgeSpace != value)
                {
                    _mBridgeSpace = value;
                    OnPropertyChanged(ConnectorConstants.BridgeSpace);
                }
            }
        }

        public DiagramMenu Menu { get; set; }
        public object Ports { get; set; }
        public PortVisibility PortVisibility { get; set; }
        public object SourceConnector { get; set; }
        public object TargetConnector { get; set; }
        #endregion

        #region IConnectorVM
        List<object> _mCaps = null;
        public List<object> Caps
        {
            get
            {
                return _mCaps;
            }
            set
            {
                if (_mCaps != value)
                {
                    _mCaps = value;
                    OnPropertyChanged(ConnectorConstants.Caps);
                }
            }
        }

        string _mSourceCap = "M0,0 z";
        [DataMember]
        public string SourceCap
        {
            get
            {
                return _mSourceCap;
            }
            set
            {
                if (_mSourceCap != value)
                {
                    _mSourceCap = value;
                    OnPropertyChanged(ConnectorConstants.SourceCap);
                }
            }
        }

        string _mTargetCap = "M0,0 L10,5 L0,10 L 0,0";
        [DataMember]
        public string TargetCap
        {
            get
            {
                return _mTargetCap;
            }
            set
            {
                if (_mTargetCap != value)
                {
                    _mTargetCap = value;
                    OnPropertyChanged(ConnectorConstants.TargetCap);
                }
            }
        }

        ConnectType _mType = ConnectType.Orthogonal;
        [DataMember]
        public ConnectType Type
        {
            get
            {
                return _mType;
            }
            set
            {
                if (_mType != value)
                {
                    _mType = value;
                    OnPropertyChanged(ConnectorConstants.Type);
                }
            }
        }
        #endregion

        protected override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            switch (name)
            {
                case ConnectorConstants.SourceCap:
                    OnSourceCapChanged();
                    break;
                case ConnectorConstants.TargetCap:
                    OnTargetCapChanged();
                    break;
                case ConnectorConstants.Type:
                    OnTypeChanged();
                    break;
            }
        }

        private void OnTypeChanged()
        {
            switch (Type)
            {
                case ConnectType.Bezier:
                    Segments = new ObservableCollection<IConnectorSegment> { new CubicCurveSegment() };
                    break;
                case ConnectType.Orthogonal:
                    Segments = new ObservableCollection<IConnectorSegment> { new OrthogonalSegment() };
                    break;
                case ConnectType.Straight:
                    Segments = new ObservableCollection<IConnectorSegment> { new StraightSegment() };
                    break;
            }
        }

        private void OnTargetCapChanged()
        {
            TargetDecorator = TargetCap;
        }

        private void OnSourceCapChanged()
        {
            SourceDecorator = SourceCap;
        }

        protected override void ApplyStyle(Style style)
        {
            ConnectorGeometryStyle = style;

            SourceDecoratorStyle = getDecoratorStyle();
            TargetDecoratorStyle = getDecoratorStyle();
        }

        private Style getDecoratorStyle()
        {
            Style decoratorStyle = GetCustomStyle();
            decoratorStyle.Setters.Add(new Setter(Path.StretchProperty, Stretch.Fill));
            decoratorStyle.Setters.Add(new Setter(Path.WidthProperty, 10));
            decoratorStyle.Setters.Add(new Setter(Path.HeightProperty, 10));
            if (Stroke != null)
            {
                decoratorStyle.Setters.Add(new Setter(Path.FillProperty, Stroke));
            }
            return decoratorStyle;
        }





        public double SourcePadding { get; set; }

        public double TargetPadding { get; set; }

        public object TargetNodeID
        {
            get;
            set;
        }

        public object TargetConnectorID
        {
            get;
            set;
        }

        public object SourceNodeID
        {
            get;
            set;
        }

        public object SourceConnectorID
        {
            get;
            set;
        }

        public object TargetPortID
        {
            get;
            set;
        }

        public object SourcePortID
        {
            get;
            set;
        }
    }

    public interface IConnectorVM : IGroupableVM, IConnector
    {
        List<object> Caps { get; set; }
        string SourceCap { get; set; }
        string TargetCap { get; set; }
        ConnectType Type { get; set; }
    }

    internal static class ConnectorConstants
    {
        public const string SourceNode = "SourceNode";
        public const string TargetNode = "TargetNode";
        public const string SourcePort = "SourcePort";
        public const string TargetPort = "TargetPort";
        public const string Segments = "Segments";
        public const string SourcePoint = "SourcePoint";
        public const string TargetPoint = "TargetPoint";
        public const string ConnectorGeometryStyle = "ConnectorGeometryStyle";
        public const string SourceDecorator = "SourceDecorator";
        public const string TargetDecorator = "TargetDecorator";
        public const string SourceDecoratorStyle = "SourceDecoratorStyle";
        public const string TargetDecoratorStyle = "TargetDecoratorStyle";
        public const string AutoBind = "AutoBind";
        public const string ID = "ID";
        public const string Key = "Key";
        public const string IsSelected = "IsSelected";
        public const string ZIndex = "ZIndex";
        public const string BridgeSpace = "BridgeSpace";
        public const string Annotations = "Annotations";
        public const string Constraints = "Constraints";
        public const string BezierSmoothness = "BezierSmoothness";
        public const string ParentGroup = "ParentGroup";

        public const string Caps = "Caps";
        public const string SourceCap = "SourceCap";
        public const string TargetCap = "TargetCap";
        public const string Type = "Type";
        public const string Types = "Types";
        public const string AnnotationConstraints = "AnnotationConstraints";
        public const string HitPadding = "HitPadding";
        public const string SegmentDecorators = "SegmentDecorators";
        public const string SegmentDecoratorStyle = "SegmentDecoratorStyle";
        public const string SourceDecoratorPivot = "SourceDecoratorPivot";
        public const string TargetDecoratorPivot = "TargetDecoratorPivot";
    }
}
