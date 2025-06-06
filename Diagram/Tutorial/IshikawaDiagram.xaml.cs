#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
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
    public sealed partial class IshikawaDiagram : SampleLayout
    {

        public IshikawaDiagram()
        {
            InitializeComponent();
            //Initialize Nodes and Connectors
            diagramcontrol.Nodes = new ObservableCollection<NodeViewModel>();
            diagramcontrol.Connectors = new ObservableCollection<IConnector>();

            //Initialize PageSettings and Constraints
            InitializeDiagram();
            
            //To disable ContextMenu
            diagramcontrol.Menu = null;

            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                //To enable Zooming and Panning
                diagramcontrol.Tool = Tool.ZoomPan;
                //diagramcontrol.Constraints = diagramcontrol.Constraints &
                //                             ~(GraphConstraints.Zoomable | GraphConstraints.Pannable);
            }
            else
            {
                diagramcontrol.Tool = Tool.ZoomPan;
                diagramcontrol.Constraints |= GraphConstraints.AllowPan;
            }
            //diagramcontrol.Constraints = diagramcontrol.Constraints & ~GraphConstraints.PanRails;

            //Create Nodes and Connections
            Nodeviewmodel();     
        }

        private void InitializeDiagram()
        {
            diagramcontrol.DefaultConnectorType = ConnectorType.Line;
            diagramcontrol.Background = new SolidColorBrush(Colors.White);
            //PageSettings used to enable the Appearance of Diagram Page.
            diagramcontrol.ScrollSettings.ScrollLimit = ScrollLimit.Diagram;
            diagramcontrol.PageSettings.PageBackground = new SolidColorBrush(Colors.White);
            diagramcontrol.PageSettings.PageBorderBrush = new SolidColorBrush(Colors.Transparent);
            //Draggable Constraints used to enable/disable Draggable.
            diagramcontrol.Constraints = GraphConstraints.Default & ~(GraphConstraints.Selectable | GraphConstraints.Draggable);
        }
        //Create Node and Connection
        private void Nodeviewmodel()
        {
            int height = 20;
            int width = 75;
            //Node Creation
            NodeViewModel n1 = AddNode(50, 100, 50, 320, null);
            NodeViewModel n2 = AddNode(75, 180, 900, 320, "");
            NodeViewModel n3 = AddNode(height, width, 150, 100, "MACHINERY");
            NodeViewModel n4 = AddNode(height, width, 400, 100, "METHOD");
            NodeViewModel n5 = AddNode(height, width, 650, 100, "PEOPLE");
            NodeViewModel n6 = AddNode(height, width, 150, 540, "MATERIAL");
            NodeViewModel n7 = AddNode(height, width, 400, 540, "MEASUREMENT");
            NodeViewModel n8 = AddNode(height, width, 650, 540, "ENVIRONMENT");
           
            //Create lineconnector from node to node
            Connector line = addlineconnector(n1, n2, null, null, new Point(0, 0), "", new Thickness(0, 0, 0, 0));
            
            //Fixed a port in a lineconnector
            ConnectorPort p1 = addport(line, 0.25);
            ConnectorPort p2 = addport(line, 0.28);
            ConnectorPort p3 = addport(line, 0.58);
            ConnectorPort p4 = addport(line, 0.60);
            ConnectorPort p5 = addport(line, 0.90);
            ConnectorPort p6 = addport(line, 0.93);

            //Add lineconnector from Node to Port
            Connector c1 = addlineconnector(n3, null, p1, line, new Point(0, 0), "", new Thickness(0, 0, 0, 0));
            Connector c2 = addlineconnector(n6, null, p2, line, new Point(0, 0), "", new Thickness(0, 0, 0, 0));
            Connector c3 = addlineconnector(n4, null, p3, line, new Point(0, 0), "", new Thickness(0, 0, 0, 0));
            Connector c4 = addlineconnector(n7, null, p4, line, new Point(0, 0), "", new Thickness(0, 0, 0, 0));
            Connector c5 = addlineconnector(n5, null, p5, line, new Point(0, 0), "", new Thickness(0, 0, 0, 0));
            Connector c6 = addlineconnector(n8, null, p6, line, new Point(0, 0), "", new Thickness(0, 0, 0, 0));

            ConnectorPort m1 = addport(c1, 0.25);
            Connector mp1 = addlineconnector(null, null, m1, c1, new Point(65, 162), "Worn-out Pistons", new Thickness(55, 15, 0, 0));

            ConnectorPort m2 = addport(c1, 0.54);
            Connector mp2 = addlineconnector(null, null, m2, c1, new Point(80, 225), "Wrong Tire Pressure", new Thickness(65, 15, 0, 0));
           
            ConnectorPort m3 = addport(c3, 0.25);
            Connector mp3 = addlineconnector(null, null, m3, c3, new Point(310, 162), "Fast Driving", new Thickness(40, 15, 0, 0));

            ConnectorPort m4 = addport(c3, 0.54);
            Connector mp4 = addlineconnector(null, null, m4, c3, new Point(330, 224), "Wrong Gear", new Thickness(40, 15, 0, 0));
           
            ConnectorPort m5 = addport(c5, 0.55);
            Connector mp5 = addlineconnector(null, null, m5, c5, new Point(510, 224), "Maintenance Habit", new Thickness(60, 15, 0, 0));

            ConnectorPort m6 = addport(mp5, 0.80);
            Connector mp6 = addlineconnector(null, null, m6, mp5, new Point(625, 185), "No Owner Manual", new Thickness(0, 0, 10, 10));

            ConnectorPort m7 = addport(c2, 0.73);
            Connector mp7 = addlineconnector(null, null, m7, c2, new Point(95, 375), "Poor Quality Petrol", new Thickness(60, 15, 0, 0));

            ConnectorPort m8 = addport(c2, 0.26);
            Connector mp8 = addlineconnector(null, null, m8, c2, new Point(60, 474), "Incorrect Lubricant", new Thickness(60, 15, 0, 0));

            ConnectorPort m9 = addport(mp8, 0.73);
            Connector mp9 = addlineconnector(null, null, m9, mp4, new Point(130, 435), "Wrong Oil", new Thickness(0, 0, 10, 10));

            ConnectorPort m10 = addport(c4, 0.6);
            Connector mp10 = addlineconnector(null, null, m10, c4, new Point(300, 403), "Do not Reset\nOdometer Properly", new Thickness(60, 25, 0, 0));

            ConnectorPort m11 = addport(c6, 0.6);
            Connector mp11 = addlineconnector(null, null, m11, c6, new Point(520, 403), "Extreme Weather\nConditions", new Thickness(55, 25, 0, 0));
        }

        //Add Nodes
        private NodeViewModel AddNode(double height, double width, double offx, double offy, string label)
        {
            NodeViewModel n = new NodeViewModel();
            if (label == "")
            {
                n.UnitHeight = height;
                n.UnitWidth = width;
                n.OffsetX = offx;
                n.OffsetY = offy;
                n.Shape = new RectangleGeometry() { Rect = new Rect(100, 400, 20, 40) };
                n.ShapeStyle = this.Resources["nvmstyle"] as Style;

                //To Represent Annotation Properties
                n.Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "HIGH PETROL \nCONSUMPTION IN BIKE",
                        TextHorizontalAlignment = TextAlignment.Center,
                        TextVerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        ReadOnly = true,
                        ViewTemplate=XamlReader.Load(IshikawaDiagramTemplate.vTemplate2) as DataTemplate,
                        //ViewTemplate=this.Resources["viewtemplate2"] as DataTemplate,
                    }
                };
                (diagramcontrol.Nodes as ICollection<NodeViewModel>).Add(n);
            }
            else
            {
                if (label == "MATERIAL" || label == "PEOPLE" || label == "METHOD" || label == "MACHINERY" || label == "MEASUREMENT" || label == "ENVIRONMENT")
                {
                    n.UnitHeight = height;
                    n.UnitWidth = width;
                    n.OffsetX = offx;
                    n.OffsetY = offy;
                    n.Shape = new RectangleGeometry() { Rect = new Rect(100, 500, 20, 40) };
                    n.ShapeStyle = this.Resources["style"] as Style;

                    //To Represent Annotation Properties
                    n.Annotations = new ObservableCollection<IAnnotation>()
                    {
                        new AnnotationEditorViewModel()
                        {
                            Content=label,
                            WrapText = TextWrapping.NoWrap,
                            //TextVerticalAlignment = VerticalAlignment.Bottom,
                            ReadOnly = true,
                             ViewTemplate=XamlReader.Load(IshikawaDiagramTemplate.vTemplate1) as DataTemplate,
                           // ViewTemplate=this.Resources["viewtemplate1"] as DataTemplate,
                            Alignment=ConnectorAnnotationAlignment.Center ,
                        }
                    };
                    (diagramcontrol.Nodes as ICollection<NodeViewModel>).Add(n);
                }
                else
                {
                    n.UnitHeight = height;
                    n.UnitWidth = width;
                    n.OffsetX = offx;
                    n.OffsetY = offy;
                    n.Shape = new RectangleGeometry() { Rect = new Rect(100, 300, 20, 40) };
                    n.ShapeStyle = this.Resources["style"] as Style;

                    (diagramcontrol.Nodes as ICollection<NodeViewModel>).Add(n);
                }
            }
            return n;
        }
      
        //Add Ports
        private ConnectorPort addport(Connector line, double p)
        {
            ConnectorPort cp1 = new ConnectorPort()
            {
                Width = 10,
                Height = 10,
                Length = p,
                Connector = line,
                Shape = new EllipseGeometry() { RadiusX = 5, RadiusY = 5 },
                PortVisibility = PortVisibility.Visible,
                ShapeStyle = this.Resources["style"] as Style,
                Constraints = PortConstraints.Default & ~PortConstraints.Inherit
            };
            (line.Ports as ICollection<IConnectorPort>).Add(cp1);
            return cp1;
        }     
       
        //Add LineConnector
        private Connector addlineconnector(NodeViewModel headnode, NodeViewModel tailnode, ConnectorPort port, Connector connect, Point sourcepoint, string label, Thickness margin)
        {
            Connector line = new Connector();
            line.TargetDecoratorStyle = this.Resources["DecoratorStyle"] as Style;
            line.Ports = new ObservableCollection<IConnectorPort>();
            //To Represent Annotation Properties
            line.Annotations = new ObservableCollection<IAnnotation>()
            {
                new AnnotationEditorViewModel()
                {
                    Content = label,
                    //ViewTemplate = this.Resources["viewtemplate"] as DataTemplate,
                    ViewTemplate=XamlReader.Load(IshikawaDiagramTemplate.vTemplate) as DataTemplate,
                    Length=0,
                    Margin = margin,
                    WrapText = TextWrapping.NoWrap,
                    ReadOnly = true,
                }
            };
            if (tailnode == null)
            {
                if (!Point.Equals(sourcepoint, new Point(0, 0)))
                {
                    line.SourcePoint = sourcepoint;
                }
                else
                {
                    line.SourceNode = headnode;
                }
                line.ConnectorGeometryStyle = this.Resources["style1"] as Style;
                line.TargetPort = port;
                line.TargetConnector = connect;
            }
            else
            {
                line.ConnectorGeometryStyle = this.Resources["connectorstyle"] as Style;
                line.SourceNode = headnode;
                line.TargetNode = tailnode;
                line.TargetConnector = connect;
            }
            (diagramcontrol.Connectors as ICollection<IConnector>).Add(line);
            line.Constraints = line.Constraints & ~ConnectorConstraints.Selectable;
            return line;
        }

    }
    public static class IshikawaDiagramTemplate
    {


       public const string vTemplate = "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                                      "<TextBlock " +
                                                 " FontFamily=\"Segoe\"" +
                                                 " FontSize=\"14\"" +
                                                 " FontWeight=\"Normal\"" +
                                                 " Foreground=\"Black\"" +
                                                 " Text=\"{Binding Path=Content, Mode=TwoWay}\"/>" +
                                      "</DataTemplate>";
        public const string vTemplate1 = "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                                      "<TextBlock" +
                                                 " FontFamily=\"Segoe\"" +
                                                 " FontSize=\"14\"" +
                                                 " FontWeight=\"Normal\"" +
                                                 " Foreground=\"SteelBlue\"" +
                                                 " Text=\"{Binding Path=Content, Mode=TwoWay}\"/>" +
                                      "</DataTemplate>";
        public const string vTemplate2 = "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                                      "<TextBlock TextAlignment=\"{Binding Path=TextHorizontalAlignment, Mode=TwoWay}\"" +
                                                 " FontFamily=\"Segoe\"" +
                                                 " FontSize=\"12\"" +
                                                 " FontWeight=\"SemiBold\"" +
                                                 " Foreground=\"White\"" +
                                                 " LineHeight=\"18\"" +
                                                 " Text=\"{Binding Path=Content, Mode=TwoWay}\"/>" +
                                      "</DataTemplate>";
    }
}
