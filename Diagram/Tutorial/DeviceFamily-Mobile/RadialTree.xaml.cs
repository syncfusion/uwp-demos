using Windows.Security.ExchangeActiveSyncProvisioning;
using Common;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Diagram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RadialTree_WP : UserControl
    {
        private EasClientDeviceInformation deviceinfo;
        public RadialTree_WP()
        {
            this.InitializeComponent();
            deviceinfo = new EasClientDeviceInformation();
            diagramControl.Menu = null;
           
                diagramControl.Tool = Tool.None;
           
            
            diagramControl.LayoutManager = new LayoutManager()
            {
                Layout = new RadialTreeLayout()

            };

           
                (diagramControl.LayoutManager.Layout as RadialTreeLayout).HorizontalSpacing = 5;
                (diagramControl.LayoutManager.Layout as RadialTreeLayout).VerticalSpacing = 15;
           
            Node head = addNode(toggle, 0);
            ConnectNode(head, Flower(4));
            diagramControl.PageSettings = null;
            (diagramControl.LayoutManager.Layout as RadialTreeLayout).LayoutRoot = head;

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

        bool toggle = true;
        private Windows.UI.Xaml.Style GetStyle()
        {
            //this would be initialized somewhere else, I assume
            string hex = "#669ad3";

            //strip out any # if they exist
            hex = hex.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, r, g, b));
            Style s = new Style();
            s.TargetType = typeof(Windows.UI.Xaml.Shapes.Path);
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.FillProperty, myBrush));
            //s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, new SolidColorBrush(Colors.Black)));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StretchProperty, Stretch.Fill));
            return s;
        }

        //Connect a node with a collection of nodes
        private void ConnectNode(Node head, List<Node> tail)
        {
            foreach (Node t in tail)
            {
                Connector lc = new Connector();
                diagramControl.DefaultConnectorType = ConnectorType.Line;
                // lc.ConnectorType = ConnectorType.Straight;
                lc.SourceNode = head;
                lc.TargetNode = t;
                lc.TargetDecoratorStyle = this.Resources["decoratorstyle"] as Style;
                lc.ConnectorGeometryStyle = this.Resources["NormalLine"] as Style;
                (diagramControl.Connectors as ICollection<object>).Add(lc);
                //diagramModel.Connections.Add(lc);

                ////Applying style to the LineConnector
                //lc.LineStyle.Stroke = Brushes.Black;
                //lc.LineStyle.StrokeThickness = 1;

                ////Applying Style to the DecoratorShape
                //lc.TailDecoratorStyle.Stroke = Brushes.Black;
                //lc.TailDecoratorStyle.Fill = Brushes.Black;
            }
        }

        //Create a tree of nodes
        private List<Node> Flower(int p)
        {
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < p; i++)
            {
                Node n = addNode(toggle, p);
                nodes.Add(n);
                toggle = !toggle;
                ConnectNode(n, Flower(p - 2));
                toggle = !toggle;
            }
            if (p <= 0)
            {
                nodes.Add(addNode(toggle, -1));
                nodes.Add(addNode(toggle, -1));
            }
            return nodes;
        }

        //Create a Node
        private Node addNode(bool toggle, int lev)
        {
            Node n = new Node();
            //n.Level = lev + 1;
            n.Shape = new EllipseGeometry() { RadiusX = 15, RadiusY = 15 };
            n.ShapeStyle = GetStyle();
            n.UnitHeight = n.UnitWidth;
            (diagramControl.Nodes as ICollection<object>).Add(n);
            //diagramModel.Nodes.Add(n);
            return n;
        }

        #endregion
    }
}
