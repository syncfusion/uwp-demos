using System.Collections.ObjectModel;
using System.ComponentModel;
using Common;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.UI.Xaml.Diagram.Panels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Windows.UI.Xaml.Shapes;
using System.Globalization;
using Syncfusion.SampleBrowser.UWP.Diagram;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Diagram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RulersAndUnit : SampleLayout
    {
        public RulersAndUnit()
        {
            this.InitializeComponent();


            //Initialize PageSettings and Snapping
            InitializeDiagram();
            var nodes = new ObservableCollection<CustomNode>();
            diagramControl.Connectors = new ObservableCollection<Connector>();
            diagramControl.Nodes = nodes;

            (diagramControl.Info as IGraphInfo).ItemAdded += RulersAndUnit_ItemAdded;

            //Collection Changed event to update the Node
            nodes.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    (e.NewItems[0] as CustomNode).Update();
                }
            };


            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                //Create Nodes and Connections
                createNode();
            }
            else
            {
                //Create Nodes and Connection
                createNodes();
            }

           
        }

        private void RulersAndUnit_ItemAdded(object sender, ItemAddedEventArgs args)
        {
          if(args.Item is Node)
            {
               if((args.Item as Node).Annotations != null)
                {
                    var annotation = (args.Item as Node).Annotations as ObservableCollection<IAnnotation>;
                    foreach (AnnotationEditorViewModel anno in annotation)
                    {                        
                            HorizontalAlignment = HorizontalAlignment.Right;
                            VerticalAlignment = VerticalAlignment.Center;                           
                            anno.ViewTemplate = this.Resources["viewtemplate"] as DataTemplate;
                            anno.EditTemplate = this.Resources["edittemplate"] as DataTemplate;                                                
                    }
                }
            }
            if (args.Item is Connector)
            {
                (args.Item as Connector).ConnectorGeometryStyle = this.Resources["NormalLine"] as Style;
                (args.Item as Connector).TargetDecoratorStyle = this.Resources["decoratorstyle"] as Style;
                if ((args.Item as Connector).Annotations != null)
                {
                    var annotation = (args.Item as Connector).Annotations as ObservableCollection<IAnnotation>;
                    foreach (AnnotationEditorViewModel anno in annotation)
                    {                        
                            HorizontalAlignment = HorizontalAlignment.Right;
                            VerticalAlignment = VerticalAlignment.Center;
                            anno.RotationReference = RotationReference.Page;
                            anno.ViewTemplate = this.Resources["connectorviewtemplate"] as DataTemplate;
                            anno.EditTemplate = this.Resources["edittemplate"] as DataTemplate;                        
                    }
                }
                else
                {
                    (args.Item as Connector).Annotations = new ObservableCollection<IAnnotation>()
                    {
                        new AnnotationEditorViewModel()
                        {
                            Content = "",
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = VerticalAlignment.Center,
                            RotationReference = RotationReference.Page,
                            ViewTemplate = this.Resources["connectorviewtemplate"] as DataTemplate,
                            EditTemplate = this.Resources["edittemplate"] as DataTemplate,
                        }
                    };
                }
            }
        }

        //Event to notify the Changes
        private void info_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            //Items added due to clipboard operation
            if (args.ItemSource == ItemSource.ClipBoard)
            {
                if (args.Item is INode)
                {
                    NodeVm node = args.Item as NodeVm;
                    if (!node.IsCustomStyle)
                    {
                        node.ShapeStyle = GetStyle("#65c7d0");
                    }
                    else
                    {
                        node.ShapeStyle = GetStyle("#858585");
                    }
                    AnnotationEditorViewModel vm = (node.Annotations as ICollection<IAnnotation>).ToList()[0] as AnnotationEditorViewModel;
                    if (!node.IsMultiline)
                    {
                        vm.ViewTemplate = this.Resources["viewtemplate1"] as DataTemplate;
                    }
                    else
                    {
                        vm.ViewTemplate = this.Resources["viewtemplate"] as DataTemplate;
                    }
                }
            }
        }

        private void InitializeDiagram()
        {
            diagramControl.PageSettings.PageBorderBrush = new SolidColorBrush(Colors.Transparent);
            diagramControl.KnownTypes = GetKnownTypes;
            diagramControl.CommandManager.View = (Control)Window.Current.Content;
            diagramControl.SnapSettings.SnapConstraints = SnapConstraints.ShowLines;
            diagramControl.SnapSettings.SnapToObject = SnapToObject.All;
        }

        //Create Nodes and Connections
        public void createNode()
        {
            //Create Nodes
            CustomNode NewIdea = addNode("NewIdea", 150, 60, 100, 70, Shapes.FlowChart_Start, "New idea identified", 1,
                "#65c7d0");
            CustomNode Meeting = addNode("Meeting", 150, 60, 100, 155, Shapes.Rectangle, "Meeting with board", 2,
                "#65c7d0");
            CustomNode BoardDecision = addNode("BoardDecision", 150, 100, 100, 260, Shapes.FlowChart_Decision,
                "Board decides whether to proceed", 3, "#65c7d0");
            CustomNode project = addNode("project", 150, 100, 100, 400, Shapes.FlowChart_Decision,
                "Find Project manager,write specification", 3, "#65c7d0");
            CustomNode End = addNode("End", 100, 60, 100, 515, Shapes.Rectangle, "Implement and Deliver", 4, "#65c7d0");
            CustomNode Decision = addNode("Decision", 180, 60, 360, 60, Shapes.FlowChart_Card,
                "Decision process for new software ideas", 5, "#858585");
            CustomNode Reject = addNode("Reject", 130, 60, 320, 260, Shapes.Rectangle, "Reject and write report", 4,
                "#65c7d0");
            CustomNode Resources = addNode("Resources", 130, 60, 320, 400, Shapes.Rectangle, "Hire new Resources", 2,
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

        void diagramControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }

        //Helps to serialize the shape
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

        //Create Nodes and Connections
        public void createNodes()
        {
            //Create Nodes
            CustomNode NewIdea = addNode("NewIdea", 150, 60, 300, 60, Shapes.FlowChart_Start, "New idea identified", 1, "#65c7d0");
            CustomNode Meeting = addNode("Meeting", 150, 60, 300, 150, Shapes.Rectangle, "Meeting with board", 2, "#65c7d0");
            CustomNode BoardDecision = addNode("BoardDecision", 150, 100, 300, 250, Shapes.FlowChart_Decision, "Board decides whether to proceed", 3, "#65c7d0");
            CustomNode project = addNode("project", 150, 100, 300, 400, Shapes.FlowChart_Decision, "Find Project manager,write specification", 3, "#65c7d0");
            CustomNode End = addNode("End", 120, 60, 300, 520, Shapes.Rectangle, "Implement and Deliver", 4, "#65c7d0");
            CustomNode Decision = addNode("Decision", 200, 60, 550, 60, Shapes.FlowChart_Card, "Decision process for new software ideas", 5, "#858585");
            CustomNode Reject = addNode("Reject", 150, 60, 550, 250, Shapes.Rectangle, "Reject and write report", 4, "#65c7d0");
            CustomNode Resources = addNode("Resources", 150, 60, 550, 400, Shapes.Rectangle, "Hire new Resources", 2, "#65c7d0");

            //Create Connections
            Connect(Meeting, NewIdea, "");
            Connect(BoardDecision, Meeting, "");
            Connect(Reject, BoardDecision, "No");
            Connect(project, BoardDecision, "Yes");
            Connect(Resources, project, "No");
            Connect(End, project, "Yes");
        }

        #endregion

        #region PrivateFunctions

        //Add Nodes
        private CustomNode addNode(String name, double width, double height, double offsetx, double offsety, Shapes shape, String content, Int32 level, string fill)
        {
            CustomNode node = new CustomNode();
            node.HorizontalContentAlignment = HorizontalAlignment.Center;
            node.UnitWidth = width;
            node.UnitHeight = height;
            node.OffsetX = offsetx;
            node.OffsetY = offsety;
            node.EnumShape = shape;
            node.Fill = fill;
            // node.Text = content;
            node.Annotations = new ObservableCollection<IAnnotation>()
            {
                new AnnotationEditorViewModel()
                {
                    Content = content,
                    UnitWidth = 75,
                    WrapText = TextWrapping.Wrap,
                    TextHorizontalAlignment = TextAlignment.Center,
                    TextVerticalAlignment = VerticalAlignment.Center,
                    ViewTemplate = this.Resources["viewtemplate"] as DataTemplate,
                }
            };
            (diagramControl.Nodes as ICollection<CustomNode>).Add(node);
            return node;
        }

        //Style for Node
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

        //Add Connections
        private void Connect(CustomNode headnode, CustomNode tailnode, string label)
        {
            Connector conn = new Connector();
            conn.SourceNode = tailnode;
            conn.TargetNode = headnode;
            conn.ConnectorGeometryStyle = this.Resources["NormalLine"] as Style;
            conn.TargetDecoratorStyle = this.Resources["decoratorstyle"] as Style;
            conn.Foreground = new SolidColorBrush(Colors.Black);           
                //To Represent Annotation Properties
                conn.Annotations = new ObservableCollection<IAnnotation>()
            {
                new AnnotationEditorViewModel()
                {
                    Content=label,
                    WrapText= TextWrapping.NoWrap,
                    HorizontalAlignment=HorizontalAlignment.Center,
                    VerticalAlignment=VerticalAlignment.Center,
                    ViewTemplate=this.Resources["connectorviewtemplate"] as DataTemplate,
                    //EditTemplate=this.Resources["edittemplate"] as DataTemplate
                }
            };                                  
            (diagramControl.Connectors as ICollection<Connector>).Add(conn);
        }
        #endregion

        //To Get Unit Selection 
        private void UnitsBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (diagramControl != null)
            {
                switch ((sender as ComboBox).SelectedIndex)
                {
                    case 0:
                        (diagramControl.PageSettings.Unit as LengthUnit).Unit = LengthUnits.Pixels;
                        break;
                    case 1:
                        (diagramControl.PageSettings.Unit as LengthUnit).Unit = LengthUnits.Inches;
                        break;
                    case 2:
                        (diagramControl.PageSettings.Unit as LengthUnit).Unit = LengthUnits.Feets;
                        break;
                    case 3:
                        (diagramControl.PageSettings.Unit as LengthUnit).Unit = LengthUnits.Yards;
                        break;
                    case 4:
                        (diagramControl.PageSettings.Unit as LengthUnit).Unit = LengthUnits.Millimeters;
                        break;
                    case 5:
                        (diagramControl.PageSettings.Unit as LengthUnit).Unit = LengthUnits.Centimeters;
                        break;
                    case 6:
                        (diagramControl.PageSettings.Unit as LengthUnit).Unit = LengthUnits.Meters;
                        break;
                }
            }
        }
    }
}
