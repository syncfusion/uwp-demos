using Windows.Security.ExchangeActiveSyncProvisioning;
using Common;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Diagram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FlowDiagram_WP : UserControl
    {
        private EasClientDeviceInformation deviceinfo;
        public FlowDiagram_WP()
        {
            this.InitializeComponent();

            deviceinfo = new EasClientDeviceInformation();
            var nodes = new ObservableCollection<Node>();
            nodes.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    //(e.NewItems[0] as CustomNode).Update();
                }
            };
            diagramControl.Nodes = nodes;
            diagramControl.Connectors = new ObservableCollection<Connector>();
            //Initialize the Nodes and Connection
            createNodes();
            diagramControl.PageSettings = null;
            diagramControl.Constraints |= GraphConstraints.Pannable;
            diagramControl.DefaultConnectorType = ConnectorType.Line;
            diagramControl.SnapSettings.SnapConstraints = SnapConstraints.All;
            diagramControl.SnapSettings.SnapToObject = SnapToObject.All;
            diagramControl.KnownTypes = GetKnownTypes;

           
            diagramControl.PointerReleased += diagramControl_PointerReleased;
           
            //this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        void diagramControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }
        private IEnumerable<Type> GetKnownTypes()
        {
            List<Type> known = new List<Type>()
            {
                typeof(Shapes)
            };
            foreach (var item in known)
            {
                yield return item;
            }
        }

        #region Helper Methods

        //Defines the nodes and adds them to the model.
        public void createNodes()
        {
            
                diagramControl.Tool = Tool.None;
                Node NewIdea = addNode("NewIdea", 150, 60, 100, 70, Shapes.FlowChart_Start, "New idea identified", 1,
                    "#65c7d0");
                Node Meeting = addNode("Meeting", 150, 60, 100, 150, Shapes.Rectangle, "Meeting with board", 2,
                    "#65c7d0");
                Node BoardDecision = addNode("BoardDecision", 150, 100, 100, 250, Shapes.FlowChart_Decision,
                    "Board decides whether to proceed", 3, "#65c7d0");
                Node project = addNode("project", 150, 100, 100, 400, Shapes.FlowChart_Decision,
                    "Find Project manager,write specification", 3, "#65c7d0");
                Node End = addNode("End", 100, 60, 100, 520, Shapes.Rectangle, "Implement and Deliver", 4, "#65c7d0");
                Node Decision = addNode("Decision", 180, 60, 360, 60, Shapes.FlowChart_Card,
                    "Decision process for new software ideas", 5, "#858585");
                Node Reject = addNode("Reject", 130, 60, 320, 250, Shapes.Rectangle, "Reject and write report", 4,
                    "#65c7d0");
                Node Resources = addNode("Resources", 130, 60, 320, 400, Shapes.Rectangle, "Hire new Resources", 2,
                    "#65c7d0");
                //Node Yes2 = addNode("Yes2", 50, 40, 320, 330, Shapes.None, "Yes", 0, null);
                //Node Yes3 = addNode("Yes3", 50, 40, 320, 460, Shapes.None, "Yes", 0, null);
                //Node No1 = addNode("No1", 50, 40, 420, 260, Shapes.None, "No", 0, null);
                //Node No2 = addNode("No2", 50, 40, 420, 380, Shapes.None, "No", 0, null);

                //Creates the connections for the nodes
                Connect(Meeting, NewIdea, "");
                Connect(BoardDecision, Meeting, "");
                Connect(Reject, BoardDecision, "No");
                Connect(project, BoardDecision, "Yes");
                Connect(Resources, project, "No");
                Connect(End, project, "Yes");
            
           
        }

        #endregion

        #region PrivateFunctions

        //Create the  Nodes and add into the DiagramModel
        private Node addNode(String name, double width, double height, double offsetx, double offsety, Shapes shape, String content, Int32 level, string fill)
        {
            Node node = new Node();
            node.HorizontalContentAlignment = HorizontalAlignment.Center;
            node.UnitWidth = width;
            node.UnitHeight = height;
            node.OffsetX = offsetx;
            node.OffsetY = offsety;

            node.Shape = shape.ToGeometry();
            node.ShapeStyle = GetStyle(fill);
            TextBlock txtblock = new TextBlock();
            if (shape == Shapes.FlowChart_Decision)
            {
                txtblock.Margin = new Thickness(25);
            }
            if (shape == Shapes.FlowChart_Card)
            {
                txtblock.Margin = new Thickness(10);
            }
            txtblock.Text = content;           
            txtblock.Foreground = new SolidColorBrush(Colors.White);
            FontFamily ff = new Windows.UI.Xaml.Media.FontFamily("Segoe UI");
            txtblock.FontFamily = ff;
            txtblock.FontSize = 13;
            txtblock.TextWrapping = TextWrapping.Wrap;
            txtblock.TextAlignment = TextAlignment.Center;
            txtblock.HorizontalAlignment = HorizontalAlignment.Center;
            txtblock.VerticalAlignment = VerticalAlignment.Center;            
            node.Content = txtblock;
            (diagramControl.Nodes as ICollection<Node>).Add(node);
            return node;
        }

        private Style GetStyle(string fill)
        {
            string hex = fill;

            //strip out any # if they exist
            hex = hex.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, r, g, b));
            Style sty = new Style();
            sty.TargetType = typeof(Windows.UI.Xaml.Shapes.Path);
            sty.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.FillProperty, myBrush));
            sty.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, new SolidColorBrush(Colors.Black)));
            sty.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StretchProperty, Stretch.Fill));


            return sty;
        }

        //Create the LineConnector and add  into  DiagramModel
        private void Connect(Node headnode, Node tailnode, string label)
        {
            Connector conn = new Connector();
            conn.SourceNode = tailnode;
            conn.TargetNode = headnode;
            conn.ConnectorGeometryStyle = this.Resources["NormalLine"] as Style;
            conn.TargetDecoratorStyle = this.Resources["decoratorstyle"] as Style;
            conn.Foreground = new SolidColorBrush(Colors.Black);
            if (label == "Yes")
            {

                conn.Annotations = new ObservableCollection<IAnnotation>()
            {
                new AnnotationEditorViewModel()
                {
                    Content=label,                    
                    HorizontalAlignment=HorizontalAlignment.Right,
                    VerticalAlignment=VerticalAlignment.Center,
                    ViewTemplate=this.Resources["viewtemplate"] as DataTemplate
                }
            };
            }
            else if (label == "No")
            {
                conn.Annotations = new ObservableCollection<IAnnotation>()
            {
                new AnnotationEditorViewModel()
                {
                    Content=label,                    
                    HorizontalAlignment=HorizontalAlignment.Right,
                    VerticalAlignment=VerticalAlignment.Top
                }
            };

            }


            (diagramControl.Connectors as ICollection<Connector>).Add(conn);
        }
        #endregion
    }

    /// <summary>
    /// Specifies the shapes.
    /// </summary>
    //public enum Shapes
    //{
    //    None,

    //    /// <summary>
    //    /// Rectangle shape
    //    /// </summary>
    //    Rectangle,

    //    /// <summary>
    //    /// Star shape
    //    /// </summary>
    //    Star,

    //    /// <summary>
    //    /// Hexagon shape
    //    /// </summary>
    //    Hexagon,

    //    /// <summary>
    //    /// Octagon shape
    //    /// </summary>
    //    Octagon,

    //    /// <summary>
    //    /// Pentagon shape
    //    /// </summary>
    //    Pentagon,

    //    /// <summary>
    //    /// Heptagon shape
    //    /// </summary>
    //    Heptagon,

    //    /// <summary>
    //    /// Triangle shape
    //    /// </summary>
    //    Triangle,

    //    /// <summary>
    //    /// Ellipse shape
    //    /// </summary>
    //    Ellipse,

    //    /// <summary>
    //    /// Plus shape
    //    /// </summary>
    //    Plus,

    //    /// <summary>
    //    /// Rounded Rectangle
    //    /// </summary>
    //    RoundedRectangle,

    //    /// <summary>
    //    /// Rounded Square
    //    /// </summary>
    //    RoundedSquare,

    //    /// <summary>
    //    /// Right Triangle
    //    /// </summary>
    //    RightTriangle,

    //    /// <summary>
    //    /// ThreeDBox shape
    //    /// </summary>
    //    ThreeDBox,

    //    /// <summary>
    //    /// FlowChart Process shape
    //    /// </summary>
    //    FlowChart_Process,

    //    /// <summary>
    //    /// FlowChart Start shape
    //    /// </summary>
    //    FlowChart_Start,

    //    /// <summary>
    //    /// FlowChart Decision shape
    //    /// </summary>
    //    FlowChart_Decision,

    //    /// <summary>
    //    /// FlowChart_Predefined shape
    //    /// </summary>
    //    FlowChart_Predefined,

    //    /// <summary>
    //    /// FlowChart_StoredData shape
    //    /// </summary>
    //    FlowChart_StoredData,

    //    /// <summary>
    //    /// FlowChart_Document shape
    //    /// </summary>
    //    FlowChart_Document,

    //    /// <summary>
    //    /// FlowChart_Data shape
    //    /// </summary>
    //    FlowChart_Data,

    //    /// <summary>
    //    /// FlowChart_InternalStorage shape
    //    /// </summary>
    //    FlowChart_InternalStorage,

    //    /// <summary>
    //    /// FlowChart_PaperTape shape
    //    /// </summary>
    //    FlowChart_PaperTape,

    //    /// <summary>
    //    /// FlowChart_SequentialData shape
    //    /// </summary>
    //    FlowChart_SequentialData,

    //    /// <summary>
    //    /// FlowChart_DirectData shape
    //    /// </summary>
    //    FlowChart_DirectData,

    //    /// <summary>
    //    /// FlowChart_ManualInput shape
    //    /// </summary>
    //    FlowChart_ManualInput,

    //    /// <summary>
    //    /// FlowChart_Card shape
    //    /// </summary>
    //    FlowChart_Card,

    //    /// <summary>
    //    /// FlowChart_Delay shape
    //    /// </summary>
    //    FlowChart_Delay,

    //    /// <summary>
    //    /// FlowChart_Terminator shape
    //    /// </summary>
    //    FlowChart_Terminator,

    //    /// <summary>
    //    /// FlowChart_Display shape
    //    /// </summary>
    //    FlowChart_Display,

    //    /// <summary>
    //    /// FlowChart_LoopLimit shape
    //    /// </summary>
    //    FlowChart_LoopLimit,

    //    /// <summary>
    //    /// FlowChart_Preparation shape
    //    /// </summary>
    //    FlowChart_Preparation,

    //    /// <summary>
    //    /// FlowChart_ManualOperation shape
    //    /// </summary>
    //    FlowChart_ManualOperation,

    //    /// <summary>
    //    /// FlowChart_OffPageReference shape
    //    /// </summary>
    //    FlowChart_OffPageReference,

    //    /// <summary>
    //    /// FlowChart_Star shape
    //    /// </summary>
    //    FlowChart_Star,

    //    Basic,
    //    Composition,
    //    UniDirectional,
    //    Inherit,
    //    Pc,
    //    Gate1,
    //    Gate2,
    //    Cloud
    //}

    //internal static class ShapeHelper
    //{
    //    private static Dictionary<Shapes, string> GeometryDictionary = new Dictionary<Shapes, string>();

    //    static ShapeHelper()
    //    {
    //        GeometryDictionary.Add(Shapes.Rectangle, "M0,0 L0,1 1,1 1,0z");
    //        GeometryDictionary.Add(Shapes.FlowChart_Start, "M 10,20 A 20,20 0 1 1 50,20 A 20,20 0 1 1 10,20");
    //        GeometryDictionary.Add(Shapes.FlowChart_Decision, "M 0,20 L 30 0 L 60,20 L 30,40 Z");
    //        GeometryDictionary.Add(Shapes.FlowChart_Predefined, "M 50,0 V 40 M 10,0 V 40 M 0 0 H 60 V 40 H 0 Z");
    //        GeometryDictionary.Add(Shapes.FlowChart_Card, "M 0 10 L 10,0 H 60 V 40 H 0 Z");
    //        GeometryDictionary.Add(Shapes.Ellipse, "M305.772,123.75C305.772,191.819095416645,237.434535075173,247,153.136,247C68.837464924827,247,0.5,191.819095416645,0.5,123.75C0.5,55.6809045833547,68.837464924827,0.5,153.136,0.5C237.434535075173,0.5,305.772,55.6809045833547,305.772,123.75z");
    //        GeometryDictionary.Add(Shapes.FlowChart_Preparation, "M 0,20 L 10,0  H 50 L 60,20 L 50,40 H10 Z");
    //        GeometryDictionary.Add(Shapes.RightTriangle, "M200,200L200,397.5 397.5,399.5z");
    //        GeometryDictionary.Add(Shapes.Star, "M 9,2 11,7 17,7 12,10 14,15 9,12 4,15 6,10 1,7 7,7 Z");
    //        GeometryDictionary.Add(Shapes.Basic, "M50,0 L100,50 L50,100 L0,50 z");
    //        GeometryDictionary.Add(Shapes.Composition, "M50,0 L100,50 L50,100 L0,50 z");
    //        GeometryDictionary.Add(Shapes.UniDirectional, "M0,0 L60,25 L0,50");
    //        GeometryDictionary.Add(Shapes.Inherit, "M2,0.5 L51.5,50 L2,99.5 L0.5,98 L0.5,2 z");
    //        GeometryDictionary.Add(Shapes.Cloud, "M696.37,65.0742C696.37,61.8815 694.284,59.1783 691.401,58.2474 692.442,55.9102 692.195,53.0886 690.516,50.9283 688.767,48.6745 685.942,47.7487 683.33,48.3138 683.287,45.418 681.483,42.7148 678.597,41.6706 675.755,40.6419 672.687,41.5222 670.793,43.6523 669.618,41.2944 667.185,39.6731 664.37,39.6731 661.556,39.6731 659.122,41.2944 657.947,43.6523 656.054,41.5222 652.987,40.6419 650.143,41.6706 647.259,42.7148 645.453,45.418 645.412,48.3138 642.798,47.7487 639.974,48.6745 638.225,50.9283 636.546,53.0886 636.3,55.9102 637.34,58.2474 634.456,59.1783 632.37,61.8815 632.37,65.0742 632.37,68.2668 634.456,70.9714 637.34,71.9009 636.3,74.2395 636.546,77.0599 638.225,79.2239 639.974,81.4766 642.798,82.4009 645.412,81.8347 645.453,84.7332 647.259,87.4349 650.143,88.4792 652.987,89.5053 656.054,88.6288 657.947,86.4974 659.122,88.8567 661.556,90.478 664.37,90.478 667.185,90.478 669.618,88.8567 670.793,86.4974 672.687,88.6288 675.755,89.5053 678.597,88.4792 681.483,87.4349 683.287,84.7332 683.33,81.8347 685.942,82.4009 688.767,81.4766 690.516,79.2239 692.195,77.0599 692.442,74.2395 691.401,71.9009 694.284,70.9714 696.37,68.2668 696.37,65.0742z");
    //        GeometryDictionary.Add(Shapes.Pc, "M24.433001,51.601002L39.202999,51.601002 39.202999,56.341999 43.396,56.341999 43.396,61.265999 20.056999,61.265999 20.056999,56.341999 24.433001,56.341999z M31.908949,38.654999C30.297743,38.654999 28.992001,39.962269 28.992001,41.573151 28.992001,43.183933 30.297743,44.489998 31.908949,44.489998 33.520157,44.489998 34.826,43.183933 34.826,41.573151 34.826,39.962269 33.520157,38.654999 31.908949,38.654999z M5.835,5.8339996L5.835,35.555996 57.983002,35.555996 57.983002,5.8339996z M0,0L64,0 64,46.677998 0,46.677998z");
    //        GeometryDictionary.Add(Shapes.ThreeDBox, "M 32.8333,33.3333L 127.726,33.3195L 127.538,128.09L 32.8292,127.556L 32.8333,33.3333 Z M 127.797,33.3333L 139.13,43.5L 139.13,136.853L 42.961,136.676L 32.8298,127.569L 127.601,127.965L 127.797,33.3333 Z M 127.496,128.028L 139.069,136.917");
    //        GeometryDictionary.Add(Shapes.Gate1, "M256.3325,267.8423L250.8335,267.8423L239.9995,267.8423L233.3335,267.8423L233.3335,302.8423L239.9995,302.8423L250.8335,302.8423L256.3325,302.8423C265.9965,302.8423,273.8325,295.0073,273.8325,285.3423C273.8325,275.6773,265.9965,267.8423,256.3325,267.8423z");
    //        GeometryDictionary.Add(Shapes.Gate2, "M314.6387,267.8423C309.0157,266.6363,297.6397,267.8423,297.6397,267.8423C297.6397,267.8423,303.5567,274.1753,303.5567,285.3423C303.5567,296.5093,297.6397,302.8423,297.6397,302.8423C297.6397,302.8423,309.0157,304.0483,314.6387,302.8423C333.8067,298.7313,338.1387,285.3423,338.1387,285.3423C338.1387,285.3423,333.8067,271.9533,314.6387,267.8423z");
    //    }

    //    public static Geometry ToGeometry(this Shapes source)
    //    {
    //        if (source == Shapes.None)
    //        {
    //            return null;
    //        }
    //        return Clone(GeometryDictionary[source]);
    //    }

    //    private static PathGeometry Clone(string data)
    //    {
    //        string p = "<Path xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Data=\"" + data + "\"/>";
    //        Windows.UI.Xaml.Shapes.Path o = XamlReader.Load(p) as Windows.UI.Xaml.Shapes.Path;
    //        PathGeometry geo = Clone(o.Data as PathGeometry);
    //        return geo;
    //    }

    //    private static PathGeometry Clone(PathGeometry pathGeometry)
    //    {
    //        PathFigureCollection collClone = new PathFigureCollection();
    //        foreach (PathFigure item in pathGeometry.Figures)
    //        {
    //            PathFigure clone = new PathFigure()
    //            {
    //                IsClosed = item.IsClosed,
    //                IsFilled = item.IsFilled,
    //                Segments = Clone(item.Segments),
    //                StartPoint = item.StartPoint
    //            };
    //            collClone.Add(clone);
    //        }
    //        return new PathGeometry()
    //        {
    //            Figures = collClone,
    //            FillRule = pathGeometry.FillRule,
    //            Transform = pathGeometry.Transform
    //        };
    //    }

    //    private static PathSegmentCollection Clone(PathSegmentCollection pathSegColl)
    //    {
    //        PathSegmentCollection collClone = new PathSegmentCollection();

    //        foreach (PathSegment item in pathSegColl)
    //        {
    //            PathSegment clone = null;
    //            if (item is LineSegment)
    //            {
    //                LineSegment seg = item as LineSegment;
    //                clone = new LineSegment() { Point = seg.Point };
    //            }
    //            else if (item is PolyLineSegment)
    //            {
    //                PolyLineSegment seg = item as PolyLineSegment;
    //                clone = new PolyLineSegment() { Points = getPoints(seg.Points) };
    //            }
    //            else if (item is BezierSegment)
    //            {
    //                BezierSegment seg = item as BezierSegment;
    //                clone = new BezierSegment()
    //                {
    //                    Point1 = seg.Point1,
    //                    Point2 = seg.Point2,
    //                    Point3 = seg.Point3
    //                };
    //            }
    //            else if (item is PolyBezierSegment)
    //            {
    //                PolyBezierSegment seg = item as PolyBezierSegment;
    //                clone = new PolyBezierSegment() { Points = getPoints(seg.Points) };
    //            }
    //            else if (item is PolyQuadraticBezierSegment)
    //            {
    //                PolyQuadraticBezierSegment seg = item as PolyQuadraticBezierSegment;
    //                clone = new PolyQuadraticBezierSegment() { Points = getPoints(seg.Points) };
    //            }
    //            else if (item is QuadraticBezierSegment)
    //            {
    //                QuadraticBezierSegment seg = item as QuadraticBezierSegment;
    //                clone = new QuadraticBezierSegment() { Point1 = seg.Point1, Point2 = seg.Point2 };
    //            }
    //            else if (item is ArcSegment)
    //            {
    //                ArcSegment seg = item as ArcSegment;
    //                clone = new ArcSegment()
    //                {
    //                    IsLargeArc = seg.IsLargeArc,
    //                    Point = seg.Point,
    //                    RotationAngle = seg.RotationAngle,
    //                    Size = seg.Size,
    //                    SweepDirection = seg.SweepDirection
    //                };
    //            }

    //            collClone.Add(clone);
    //        }
    //        return collClone;
    //    }

    //    private static PointCollection getPoints(PointCollection pointCollection)
    //    {
    //        PointCollection coll = new PointCollection();
    //        foreach (var item in pointCollection)
    //        {
    //            coll.Add(item);
    //        }
    //        return coll;
    //    }

    //}
}

