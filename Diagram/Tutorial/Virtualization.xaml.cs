using Common;
using Syncfusion.SampleBrowser.UWP.Diagram;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Diagram
{
    public sealed partial class Virtualization :SampleLayout
    {
        public Virtualization()
        {
            this.InitializeComponent();

            //Enables the Virtualization.
            diagram.Constraints = diagram.Constraints | GraphConstraints.Virtualize;

            //Arrange the Node.
            CreateNode();

            //Connects the Nodes.
            CreateConnection();
        }

        //Connects the Hexagon arrangment Nodes.
        private void CreateConnection()
        {
            int increment = 0;
            int altenate = 0;
            NodeCollection nodecollection = diagram.Nodes as NodeCollection;
            ConnectorCollection connectorcollection = diagram.Connectors as ConnectorCollection;
            for (int j = 1; j < 101; j++)
            {
                for (int i = 0; i < 100; i++)
                {
                    if ((i + increment + 1) % 100 != 0)
                    {
                        ConnectorViewModel cv1 = new ConnectorViewModel()
                        {
                            SourceNode = nodecollection.ToList()[i + increment],
                            TargetNode = nodecollection.ToList()[i + 1 + increment],
                        };
                        connectorcollection.Add(cv1);
                        cv1.ConnectorGeometryStyle = App.Current.Resources["connectorstyle"] as Style;
                    }
                    if (j == 1 && i == 0)
                    {
                        ConnectorViewModel cv1 = new ConnectorViewModel()
                        {
                            SourceNode = nodecollection.ToList()[i],
                            TargetNode = nodecollection.ToList()[i + 101],
                        };
                        connectorcollection.Add(cv1);
                        cv1.ConnectorGeometryStyle = App.Current.Resources["connectorstyle"] as Style;
                    }
                    if (j != 1)
                    {
                        if (i == 0)
                        {
                            if (!((increment + 100) > 9900))
                            {
                                ConnectorViewModel cv1 = new ConnectorViewModel()
                                {
                                    SourceNode = nodecollection.ToList()[i + increment],
                                    TargetNode = nodecollection.ToList()[i + 1 + (increment + 100)],
                                };
                                connectorcollection.Add(cv1);
                                cv1.ConnectorGeometryStyle = App.Current.Resources["connectorstyle"] as Style;
                            }
                        }
                        else
                        {
                            altenate += 1;
                            if (altenate == 2 || altenate == 5)
                            {
                                if (altenate == 2)
                                {
                                    ConnectorViewModel cv1 = new ConnectorViewModel()
                                    {
                                        SourceNode = nodecollection.ToList()[i + increment],
                                        TargetNode = nodecollection.ToList()[i + 1 + (increment - 100)],
                                    };
                                    connectorcollection.Add(cv1);
                                    cv1.ConnectorGeometryStyle = App.Current.Resources["connectorstyle"] as Style;
                                }
                                else
                                {
                                    ConnectorViewModel cv1 = new ConnectorViewModel()
                                    {
                                        SourceNode = nodecollection.ToList()[i + increment],
                                        TargetNode = nodecollection.ToList()[i - 1 + (increment - 100)],
                                    };
                                    connectorcollection.Add(cv1);
                                    cv1.ConnectorGeometryStyle = App.Current.Resources["connectorstyle"] as Style;
                                }
                            }
                            if (altenate == 5)
                            {
                                altenate = 0;
                                altenate += 1;
                            }
                        }
                    }
                }
                increment += 100;
                altenate = 0;
            }
        }

        //Arrange the Nodes for Hexagon shape.
        private void CreateNode()
        {
            NodeCollection nodecollection = diagram.Nodes as NodeCollection;
            double commonfortop = 0;
            double alternate = 0;
            double levelfortop = 50;
            double levelforremaining = 0;
            for (int j = 1; j < 101; j++)
            {
                for (int i = 0; i < 100; ++i)
                {
                    NodeViewModel node = new NodeViewModel();
                    if (i == 0)
                    {
                        node.OffsetX = 100 + (levelfortop);
                        commonfortop = node.OffsetX;
                    }
                    else
                    {
                        alternate += 1;
                        if (alternate <= 2)
                        {
                            node.OffsetX = 40 + levelforremaining;

                        }
                        else
                        {
                            node.OffsetX = commonfortop;
                        }

                        if (alternate == 4)
                        {
                            alternate = 0;
                        }

                    }
                    node.UnitHeight = 30;
                    node.UnitWidth = 50;
                    node.Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) };
                    node.ShapeStyle = App.Current.Resources["nodestyle"] as Style;
                    node.OffsetY = 20 + (80 * i);
                    nodecollection.Add(node);
                }
                levelfortop += 220;
                levelforremaining += 220;
                alternate = 0;
            }
        }
    }
}
