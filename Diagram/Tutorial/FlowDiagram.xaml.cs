using System.Collections.ObjectModel;
using Common;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Runtime.Serialization;

using Syncfusion.SampleBrowser.UWP.Diagram;
using Windows.UI.Xaml.Markup;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Diagram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FlowDiagram : SampleLayout
    {
        public FlowDiagram()
        {
            this.InitializeComponent();

            //Initialize PageSettings and Snapping
            InitializeDiagram();

            var nodes = new ObservableCollection<CustomNode>();
            diagramControl.Connectors = new ObservableCollection<Connector>();
            diagramControl.Nodes = nodes;

           


            //Collection Changed event to update the Node
            nodes.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null && (e.NewItems[0] is CustomNode))
                {
                    (e.NewItems[0] as CustomNode).Update();
                }
            };


            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                diagramControl.Constraints |= GraphConstraints.AllowPan;
                diagramControl.Tool = Tool.ZoomPan;
                //Create Nodes and Connections
                createNode();
            }
            else
            {
                //Create Nodes and Connection
                createNodes();
                (diagramControl.Info as IGraphInfo).ItemAdded += FlowDiagram_ItemAdded;
            }

          
        }

        private void FlowDiagram_ItemAdded(object sender, ItemAddedEventArgs args)
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
                        anno.ViewTemplate = this.Resources["flowdiagramconnectorviewtemplate"] as DataTemplate;
                        anno.EditTemplate = this.Resources["edittemplate"] as DataTemplate;
                    }
                }
                else
                {
                    (args.Item as Connector).Annotations = new ObservableCollection<IAnnotation>()
                    {
                        new AnnotationEditorViewModel
                        {
                            Content = "",
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = VerticalAlignment.Center,
                            RotationReference = RotationReference.Page,
                            ViewTemplate = this.Resources["flowdiagramconnectorviewtemplate"] as DataTemplate,
                            EditTemplate = this.Resources["edittemplate"] as DataTemplate,
                        }
                    };
                }
            }
        }


      

        private void InitializeDiagram()
        {
            diagramControl.PageSettings.PageBorderBrush = new SolidColorBrush(Colors.Transparent);
            //diagramControl.PageSettings = null;
            diagramControl.Loaded += FlowDiagram_Loaded;
            diagramControl.Unloaded += diagramControl_Unloaded;
            diagramControl.SnapSettings.SnapConstraints = SnapConstraints.ShowLines;
            diagramControl.SnapSettings.SnapToObject = SnapToObject.All;
        }

        private void FlowDiagram_Loaded(object sender, RoutedEventArgs e)
        {
            diagramControl.KnownTypes = GetKnownTypes;
            diagramControl.PointerReleased += diagramControl_PointerReleased;
            diagramControl.CommandManager.View = (Control)Window.Current.Content;
            diagramControl.Loaded -= FlowDiagram_Loaded;
        }

        void diagramControl_Unloaded(object sender, RoutedEventArgs e)
        {
            diagramControl.PointerReleased -= diagramControl_PointerReleased;
            diagramControl.KnownTypes = null;
            diagramControl.Unloaded -= diagramControl_Unloaded;
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
            CustomNode Decision = addNode("Decision", 180, 60, 345, 60, Shapes.FlowChart_Card,
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
            if (name != "Decision") { node.Fill = "#5B9BD5"; }
            else
            {
                node.Fill = fill;

            }
            //node.Text = content;
            node.Annotations = new ObservableCollection<IAnnotation>()
            {
                new AnnotationEditorViewModel()
                {
                    Content = content,
                    UnitWidth = 75,
                    WrapText = TextWrapping.Wrap,
                    TextHorizontalAlignment = TextAlignment.Center,
                    TextVerticalAlignment = VerticalAlignment.Center,
                    ViewTemplate = XamlReader.Load(FlowDiagramTemplate.vTemplate1) as DataTemplate,
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
            sty.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, new SolidColorBrush(Colors.WhiteSmoke)));
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
            //conn.Foreground = new SolidColorBrush(Colors.Black);           
                //To Represent Annotation Properties
                conn.Annotations = new ObservableCollection<IAnnotation>()
            {
                new AnnotationEditorViewModel()
                {
                    Content=label,
                    WrapText= TextWrapping.NoWrap,
                    HorizontalAlignment= HorizontalAlignment.Center,
                    VerticalAlignment=VerticalAlignment.Center,
                    ViewTemplate=XamlReader.Load(FlowDiagramTemplate.vTemplate) as DataTemplate
                    //EditTemplate=this.Resources["edittemplate"] as DataTemplate
                }
            };                                            
            (diagramControl.Connectors as ICollection<Connector>).Add(conn);
        }
        #endregion
    }

    #region CustomClasses
    public class AnnotationCollection : ObservableCollection<IAnnotation>
    {

    }

    public class NodeVmCollection : ObservableCollection<NodeVm>
    {

    }

    //Custom NodeViewModel with new properties
    public class NodeVm : NodeViewModel
    {
        private bool _multiline = false;
        private bool _customstyle = false;
        [DataMember]
        public bool IsMultiline
        {
            get { return _multiline; }
            set
            {
                if (_multiline != value)
                {
                    _multiline = value;
                    OnPropertyChanged("IsMultiline");
                }
            }
        }
        [DataMember]
        public bool IsCustomStyle
        {
            get { return _customstyle; }
            set
            {
                if (_customstyle != value)
                {
                    _customstyle = value;
                    OnPropertyChanged("IsCustomStyle");
                }
            }
        }
    }
    #endregion

    public class ConnectorVm : ConnectorViewModel
    {

    }
    public class ConnectorVmCollection : ObservableCollection<ConnectorVm>
    {

    }

    //To Represent Node class Properties
    public class CustomNode : Node
    {
        public CustomNode()
        {
        }

        private Shapes _mShape;
        private string _mFill;
        private string _mText;

        [DataMember]
        public Shapes EnumShape
        {
            get { return _mShape; }
            set
            {
                _mShape = value;
            }
        }

        [DataMember]
        public string Text
        {
            get { return _mText; }
            set { _mText = value; }
        }

        [DataMember]
        public string Fill
        {
            get { return _mFill; }
            set { _mFill = value; }
        }

        public void Update()
        {
          //  string content = Text;
            Shapes shape = EnumShape;
            string fill = Fill;
            Shape = shape.ToGeometry();
            if (fill != null)
            {
                ShapeStyle = GetStyle(fill);
            }
            TextBlock txtblock = new TextBlock();
            if (shape == Shapes.FlowChart_Decision)
            {
                txtblock.Margin = new Thickness(25);
            }
            if (shape == Shapes.FlowChart_Card)
            {
                txtblock.Margin = new Thickness(10);
            }
           // txtblock.Text = content;           
            txtblock.Foreground = new SolidColorBrush(Colors.White);
            FontFamily ff = new Windows.UI.Xaml.Media.FontFamily("Segoe UI");
            txtblock.FontFamily = ff;
            txtblock.FontSize = 13;
            txtblock.TextWrapping = TextWrapping.Wrap;
            txtblock.TextAlignment = TextAlignment.Center;
            txtblock.HorizontalAlignment = HorizontalAlignment.Center;
            txtblock.VerticalAlignment = VerticalAlignment.Center;
            Content = txtblock;

        }

        //Node CustomPathStyle
        private Style GetStyle(string fill)
        {
            Node node = this;
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
            //sty.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, new SolidColorBrush(Colors.Black)));
            sty.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StretchProperty, Stretch.Fill));
            return sty;
        }
    }
    public static class FlowDiagramTemplate
    {
        public const string vTemplate = "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                                      "<Border  Background=\"White\">" +
                                      "<TextBlock TextAlignment=\"Center\"" +
                                                 " TextWrapping=\"{Binding Path = WrapText, Mode = OneWay}\"" +
                                                 " FontFamily=\"Times New Roman\"" +
                                                 " FontSize=\"12\"" +
                                                 " HorizontalAlignment=\"Center\"" +
                                                 " VerticalAlignment=\"Center\"" +
                                                 " Foreground=\"Black\"" +
                                                 " Text=\"{Binding Path=Content, Mode=TwoWay}\"/>" +
                                      "</Border>" +
                                      "</DataTemplate>";

        public const string vTemplate1 = "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                                      "<Border >" +
                                      "<TextBlock TextAlignment=\"Center\"" +
                                                 " TextWrapping=\"{Binding Path = WrapText, Mode = OneWay}\"" +
                                                 " FontFamily=\"Times New Roman\"" +
                                                 " FontSize=\"12\"" +
                                                 " HorizontalAlignment=\"Center\"" +
                                                 " VerticalAlignment=\"Center\"" +
                                                 " Foreground=\"White\"" +
                                                 " Text=\"{Binding Path=Content, Mode=TwoWay}\"/>" +
                                      "</Border>" +
                                      "</DataTemplate>";



    }

}
