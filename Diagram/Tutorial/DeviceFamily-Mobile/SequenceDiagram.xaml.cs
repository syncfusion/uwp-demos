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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Diagram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SequenceDiagram_WP : UserControl
    {
        public SequenceDiagram_WP()
        {
            this.InitializeComponent();
            diagramcontrol.Nodes = new ObservableCollection<NodeViewModel>();
            diagramcontrol.Connectors = new ObservableCollection<ConnectorViewModel>();
            diagramcontrol.PortVisibility = PortVisibility.Collapse;
            diagramcontrol.Menu = null;
            diagramcontrol.Tool = Tool.ZoomPan;
            diagramcontrol.PageSettings.PageBorderBrush = new SolidColorBrush(Colors.Transparent);
            CreateNodesandConnections();
        }


        /// <summary>
        /// Creating Nodes,Ports and Connections
        /// </summary>
        private void CreateNodesandConnections()
        {
            // Creating Nodes
            NodeViewModel node1 = AddNode(50, 100, 100, 50, label: "Employee");
            NodeViewModel node2 = AddNode(50, 100, 300, 50, label: "Team Lead");
            NodeViewModel node3 = AddNode(50, 100, 500, 50, label: "Dashboard");
            NodeViewModel node4 = AddNode(50, 100, 700, 50, label: "Manager");
            NodeViewModel node8 = AddNode(50, 100, 100, 500);
            NodeViewModel node9 = AddNode(50, 100, 300, 500);
            NodeViewModel node10 = AddNode(50, 100, 500, 500);
            NodeViewModel node11 = AddNode(50, 100, 700, 500);

            // Connecting Nodes and Ports
            ConnectorViewModel connector3 = Connect(node1, node8, label1: "c3", style: this.Resources["GetLineStyle1"] as Style);
            ConnectorViewModel connector4 = Connect(node2, node9, label1: "c4", style: this.Resources["GetLineStyle1"] as Style);
            ConnectorViewModel connector5 = Connect(node3, node10, label1: "c5", style: this.Resources["GetLineStyle1"] as Style);
            ConnectorViewModel connector6 = Connect(node4, node11, label1: "c6", style: this.Resources["GetLineStyle1"] as Style);

            NodeViewModel node5 = AddNode(180, 10, 300, 250, style: this.Resources["GetNodeStyle"] as Style);
            NodeViewModel node6 = AddNode(25, 10, 500, 250, style: this.Resources["GetNodeStyle"] as Style);
            NodeViewModel node7 = AddNode(48, 10, 700, 348, style: this.Resources["GetNodeStyle"] as Style);
            NodeViewModel node26 = AddNode(240, 10, 100, 278, style: this.Resources["GetNodeStyle"] as Style);

            // Creating Ports to the Nodes and Connectors
            NodePort port1 = addPort(node5, 0, 0.053);
            NodePort port111 = addPort(node5, 1, 0.5);
            NodePort port12 = addPort(node5, 1, 0.938);
            NodePort port2 = addPort(node6, 0, 0.5);
            NodePort port3 = addPort(node7, 0, 0.1);
            NodePort port10 = addPort(node7, 0, 0.91);
            NodePort port11 = addPort(node26, 1, 0.049);
            NodePort port7 = addPort(node26, 1, 0.97);
            node5.Ports = new ObservableCollection<INodePort>()
            {
                port1,            
                port111,            
                port12
            };

            node6.Ports = new ObservableCollection<INodePort>()
            {
                port2
            };

            node7.Ports = new ObservableCollection<INodePort>()
            {
                port3,                
                port10
            };

            node26.Ports = new ObservableCollection<INodePort>()
            {
                port11,                
                port7
            };

            ConnectorViewModel connector1 = Connect(node5, node6, headport: port111, tailport: port2, label2: "Check Employee availability and task status", style: this.Resources["GetLineStyle2"] as Style, margin: new Thickness(0, 20, 0, 0));
            ConnectorViewModel connector2 = Connect(node5, node7, headport: port12, tailport: port3, label2: "Forward Leave Request", style: this.Resources["GetLineStyle2"] as Style, margin: new Thickness(0, 10, 0, 0));
            ConnectorViewModel connector7 = Connect(node26, node5, headport: port11, tailport: port1, label2: "Leave Request", style: this.Resources["GetLineStyle2"] as Style, margin: new Thickness(0, 10, 0, 0));

            ConnectorPort port5 = addPort(connector4, 0.78);
            ConnectorPort port6 = addPort(connector4, 0.73);
            connector4.Ports = new ObservableCollection<IConnectorPort>()
            {
                port5,                
                port6
            };

            ConnectorViewModel connector8 = Connect(null, node26, tailport: port7, connector: connector4, connectorheadport: port5, label2: "Leave Approval", style: this.Resources["GetLineDashStyle"] as Style, margin: new Thickness(0, 10, 0, 0));
            ConnectorViewModel connector10 = Connect(node7, null, headport: port10, connector: connector4, connectortailport: port6, label2: "No Objection", style: this.Resources["GetLineDashStyle"] as Style, margin: new Thickness(0, 10, 0, 0));
        }

        /// <summary>
        /// Adding Connector
        /// </summary>
        /// <param name="headnode"></param>
        /// <param name="tailnode"></param>
        /// <param name="headport"></param>
        /// <param name="tailport"></param>
        /// <param name="connector"></param>
        /// <param name="connectorheadport"></param>
        /// <param name="connectortailport"></param>
        /// <param name="label1"></param>
        /// <param name="label2"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        private ConnectorViewModel Connect(NodeViewModel headnode, NodeViewModel tailnode, NodePort headport = null, NodePort tailport = null, ConnectorViewModel connector = null, ConnectorPort connectorheadport = null, ConnectorPort connectortailport = null, String label1 = null, String label2 = null, Style style = null, Thickness margin = new Thickness())
        {
            ConnectorViewModel conn = new ConnectorViewModel();
            conn.SourceNode = headnode;
            conn.TargetNode = tailnode;
            if (label2 != null)
            {
                conn.Annotations = new ObservableCollection<IAnnotation>()
                { 
                    new AnnotationEditorViewModel()
                    { 
                        Content=new TextBlock()
                        {
                            Text=label2, 
                            FontWeight=FontWeights.Normal,  
                            FontSize=13,
                            FontStyle=FontStyle.Normal,
                        }, 
                        Margin = margin,
                        ViewTemplate=this.Resources["viewtemplate"] as DataTemplate,
                        EditTemplate=this.Resources["edittemplate"] as DataTemplate 
                    } 
                };
            }

            if (headport != null)
            {
                conn.SourcePort = headport;
            }
            else
            {
                conn.SourcePort = connectorheadport;
            }

            if (tailport != null)
            {
                conn.TargetPort = tailport;
            }
            else
            {
                conn.TargetPort = connectortailport;
            }

            if (tailnode != null)
            {
                conn.SourceConnector = connector;
            }
            else
            {
                conn.TargetConnector = connector;
            }

            conn.TargetDecoratorStyle = this.Resources["DecoratorStyle"] as Style;

            if ((label1 == "c3") || (label1 == "c4") || (label1 == "c5") || (label1 == "c6"))
            {
                conn.TargetDecorator = null;

                conn.TargetDecoratorStyle = null;
            }

            if (style != null)
            {
                conn.ConnectorGeometryStyle = style;
            }

            conn.Constraints = conn.Constraints & ~ConnectorConstraints.Selectable;
            diagramcontrol.DefaultConnectorType = ConnectorType.Line;
            (diagramcontrol.Connectors as ICollection<ConnectorViewModel>).Add(conn);
            return conn;
        }

        /// <summary>
        /// Adding port to Node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        private NodePort addPort(NodeViewModel node, double offsetX, double offsetY)
        {
            NodePort v = new NodePort()
            {
                Width = 10,
                Height = 10,
                NodeOffsetX = offsetX,
                NodeOffsetY = offsetY,
                Node = node,
                ShapeStyle = this.Resources["GetPortStyle"] as Style,
            };
            return v;
        }

        /// <summary>
        /// Adding port to Connector
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private ConnectorPort addPort(ConnectorViewModel connector, double length)
        {
            ConnectorPort v = new ConnectorPort()
            {
                Width = 10,
                Height = 10,
                Length = length,
                Connector = connector,
                ShapeStyle = this.Resources["GetPortStyle"] as Style,
            };

            return v;
        }

        /// <summary>
        /// Adding Node
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="offx"></param>
        /// <param name="offy"></param>
        /// <param name="label"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        private NodeViewModel AddNode(double height, double width, double offx, double offy, string label = null, Style style = null)
        {
            NodeViewModel n = new NodeViewModel()
            {
                UnitHeight = height,
                UnitWidth = width,
                OffsetX = offx,
                OffsetY = offy,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 0, 50, 60) },
            };
            if (label != null)
            {
                n.Annotations = new ObservableCollection<IAnnotation>()
                {  
                    new AnnotationEditorViewModel()
                    {  
                        Content=new TextBlock()
                        {
                            Text=label, 
                            FontWeight=FontWeights.SemiBold,
                            FontSize=13,
                            FontStyle=FontStyle.Normal,
                        },
                        Alignment=ConnectorAnnotationAlignment.Center, 
                        ViewTemplate=this.Resources["viewtemplate"] as DataTemplate,
                        EditTemplate=this.Resources["edittemplate"] as DataTemplate ,
                    }
                };
            }

            if (style != null)
            {
                n.ShapeStyle = style;
            }

            n.Constraints = n.Constraints & ~NodeConstraints.Selectable;
            (diagramcontrol.Nodes as ICollection<NodeViewModel>).Add(n);
            return n;

        }
    }
}
