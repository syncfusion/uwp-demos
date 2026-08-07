using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Windows.UI;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Diagram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HierarchicalTree_WP : UserControl
    {
        private EasClientDeviceInformation deviceinfo;
        public HierarchicalTree_WP()
        {
            this.InitializeComponent();
            deviceinfo = new EasClientDeviceInformation();
            diagramControl.Menu = null;
            diagramControl.Constraints = GraphConstraints.Relationship | GraphConstraints.Events;
            diagramControl.Constraints |= GraphConstraints.Pannable;
            diagramControl.Tool = Tool.None;
            diagramControl.LayoutManager = new Syncfusion.UI.Xaml.Diagram.Layout.LayoutManager()
            {
                Layout = new DirectedTreeLayout()
            };
            diagramControl.PageSettings = null;
            (diagramControl.LayoutManager.Layout as DirectedTreeLayout).HorizontalSpacing = 30;
            (diagramControl.LayoutManager.Layout as DirectedTreeLayout).VerticalSpacing = 70;
            (diagramControl.LayoutManager.Layout as DirectedTreeLayout).SpaceBetweenSubTrees = 30;
            
           
            createNodes();
            // this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    // TODO: Prepare page for display here.

        //    // TODO: If your application contains multiple pages, ensure that you are
        //    // handling the hardware Back button by registering for the
        //    // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
        //    // If you are using the NavigationHelper provided by some templates,
        //    // this event is handled for you.
        //}

        #region Helper Methods



        //Defines the nodes 
        public void createNodes()
        {

            //Creating the Nodes
            Node n1 = AddNode("n1", "#cf2027", "#cf2027");
            Node n2 = AddNode("n2", "#878382", "#878382");
            Node n3 = AddNode("n3", "#878382", "#878382");
            Node n4 = AddNode("n4", "#878382", "#878382");
            Node n5 = AddNode("n5", "#9457a4", "#e3ad27");
            Node n6 = AddNode("n6", "#9457a4", "#9457a4");
            Node n7 = AddNode("n7", "#9457a4", "#9457a4");
            Node n8 = AddNode("n8", "#e3ad27", "#e3ad27");
            Node n9 = AddNode("n9", "#e3ad27", "#e3ad27");
            Node n10 = AddNode("n10", "#e3ad27", "#e3ad27");
            Node n11 = AddNode("n11", "#e3ad27", "#e3ad27");
            Node n12 = AddNode("n12", "#e3ad27", "#e3ad27");
            Node n13 = AddNode("n13", "#e3ad27", "#e3ad27");


            //Creating conections between nodes
            Connect(n1, n2);
            Connect(n1, n3);
            Connect(n1, n4);
            Connect(n2, n5);
            Connect(n3, n6);
            Connect(n4, n7);
            Connect(n5, n8);
            Connect(n5, n9);
            Connect(n6, n10);
            Connect(n6, n11);
            Connect(n7, n12);
            Connect(n7, n13);
        }

        //Creating connection and adding to the model
        void Connect(Node HeadNode, Node TailNode)
        {
            Connector connection = new Connector();
            //connection.Name = name;
            diagramControl.DefaultConnectorType = ConnectorType.Line;
            // Specify the TailNode node
            connection.TargetNode = TailNode;
            //Specifying the Head Node
            connection.SourceNode = HeadNode;
            connection.ConnectorGeometryStyle = this.Resources["NormalLine"] as Style;
            connection.TargetDecoratorStyle = this.Resources["decoratorstyle"] as Style;
            //Adding to the Diagram Model
            (diagramControl.Connectors as ICollection<object>).Add(connection);
        }

        //Add the Node to DiagramModel
        Node AddNode(String name, string pathfill, string pathstroke)
        {
            Node n = new Node();
            n.UnitWidth = 50;
            n.UnitHeight = 50;
            //n.OffsetX = offsetX;
            //n.OffsetY = offsetY;
            n.Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 20, 20) };
            TextBlock txtblock = new TextBlock();
            //txtblock.Text = name;
            txtblock.Foreground = new SolidColorBrush(Colors.White);
            n.Content = txtblock;
            txtblock.FontSize = 18;
            n.ShapeStyle = GetStyle(n, pathfill);
            (diagramControl.Nodes as ICollection<object>).Add(n);
            return n;
        }

        //Node CustomPathStyle
        private Style GetStyle(Node node, string pathfill)
        {

            string hex = pathfill;

            //strip out any # if they exist
            hex = hex.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, r, g, b));
            Style sty = new Style();

            sty.TargetType = typeof(Windows.UI.Xaml.Shapes.Path);
            sty.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.FillProperty, myBrush));
            sty.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, myBrush));
            // sty.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeThicknessProperty, new Thickness(2)));
            sty.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StretchProperty, Stretch.Fill));
            return sty;
        }
        #endregion
    }
}
