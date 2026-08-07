using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Runtime.Serialization;
using Syncfusion.SampleBrowser.UWP.Diagram;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MindMapDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MindMapDiagram : Page
    {
        CustomNode SelectedNode;
        Popup p = new Popup();
        ColorPicker c = new ColorPicker();
        MapViewModel m;
        bool IsPickerClicked = false;
        private IGraphInfo graphInfo;
        //ManipulationStartedEventHandler started;
        //ManipulationCompletedEventHandler complete;
        //ManipulationDeltaEventHandler delta;
        private ObservableCollection<CustomNode> _nodes=new ObservableCollection<CustomNode>();
        private ObservableCollection<CustomConnector> _connectors=new ObservableCollection<CustomConnector>(); 
        public MindMapDiagram() 
        {
            this.InitializeComponent();
            diagramcontrol.Nodes = _nodes;
            diagramcontrol.Connectors = _connectors;
            graphInfo = diagramcontrol.Info as IGraphInfo;
            this.gird.Children.Add(p);
            diagramcontrol.PointerPressed += diagramcontrol_PointerPressed;
            diagramcontrol.DefaultConnectorType = ConnectorType.Line;
            graphInfo.ItemTappedEvent += diagramcontrol_ItemTappedEvent;
            graphInfo.ItemSelectedEvent += diagramcontrol_ItemSelectedEvent;
            diagramcontrol.DoubleTapped += diagramcontrol_DoubleTapped;
            diagramcontrol.Tapped += diagramcontrol_Tapped;
            diagramcontrol.KnownTypes = () => new List<Type>
                {
                    typeof(CustomLabel),
                    typeof(NodeCustom)
                };

            diagramcontrol.started = new ManipulationStartedEventHandler(n_ManipulationStarted);
            diagramcontrol.complete = new ManipulationCompletedEventHandler(n_ManipulationCompleted);
            diagramcontrol.delta = new ManipulationDeltaEventHandler(n_ManipulationDelta);

            //diagramcontrol.AddHandler(Node.ManipulationStartedEvent, started, true);
            //diagramcontrol.AddHandler(Node.ManipulationCompletedEvent, complete, true);
            //diagramcontrol.AddHandler(Node.ManipulationDeltaEvent, delta, true);
            diagramcontrol.PageSettings = null;
            diagramcontrol.Menu = null;
            //LayoutType = "Bowtie";
            diagramcontrol.LayoutUpdated += diagramcontrol_LayoutUpdated;
            this.Unloaded += MindMapDiagram_Unloaded;
        }

        void MindMapDiagram_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= MindMapDiagram_Unloaded;
            if (diagramcontrol != null)
            {
                diagramcontrol.LayoutUpdated -= diagramcontrol_LayoutUpdated;
                diagramcontrol.PointerPressed -= diagramcontrol_PointerPressed;
                graphInfo.ItemTappedEvent -= diagramcontrol_ItemTappedEvent;
                graphInfo.ItemSelectedEvent -= diagramcontrol_ItemSelectedEvent;
                diagramcontrol.DoubleTapped -= diagramcontrol_DoubleTapped;
                diagramcontrol.Tapped -= diagramcontrol_Tapped;
                foreach (Node node in diagramcontrol.Page.Children.OfType<CNode>())
                {
                    node.RemoveHandler(Node.ManipulationStartedEvent, diagramcontrol.started);
                    node.RemoveHandler(Node.ManipulationCompletedEvent, diagramcontrol.complete);
                    node.RemoveHandler(Node.ManipulationDeltaEvent, diagramcontrol.delta);
                }
            }
            //diagramcontrol.RemoveHandler(Node.ManipulationStartedEvent, started);
            //diagramcontrol.RemoveHandler(Node.ManipulationCompletedEvent, complete);
            //diagramcontrol.RemoveHandler(Node.ManipulationDeltaEvent, delta);
            MapViewModel.Nodes.Clear();
            MapViewModel.Lines.Clear();
            _nodes = null;
            _connectors = null;
            diagramcontrol.started = null;
            diagramcontrol.complete = null;
            diagramcontrol.delta = null;
            this.DataContext = null;
            MapViewModel = null;
            backButton.Tapped -= Back_Clicked;
            diagramcontrol = null;
        }

        void diagramcontrol_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var v=MapViewModel.Nodes.ToList().Find(c => (c as CustomNode)._havetoUpdateLayout);
            if (SelectedNode != null && v != null && diagramcontrol.LayoutType.Equals("Bowtie"))
            {
                Updatebowtielayout("left");
                Updatebowtielayout("right");
                (v as CustomNode)._havetoUpdateLayout = false;
            }
        }

        void diagramcontrol_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (IsPickerClicked)
                p.IsOpen = false;
            else
                IsPickerClicked = false;
        }

        public MapViewModel MapViewModel
        {
            get { return (MapViewModel)GetValue(MapViewModelProperty); }
            set { SetValue(MapViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapViewModelProperty =
            DependencyProperty.Register("MapViewModel", typeof(MapViewModel), typeof(MindMapDiagram), new PropertyMetadata(null,OnPropertyChanged));


        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MindMapDiagram md = d as MindMapDiagram;
            if (e.OldValue != null)
            {
                MapViewModel vm = e.OldValue as MapViewModel;
                //md.m = md.MapViewModel;
                //md.m.Data = md;
                //md.UpdateCollection();
                md.m.Data = null;
                md.m = null;
                vm.Back = null;
                vm.Create = null;
                vm.Save = null;
                vm.Load = null;
                vm.Clear = null;
            }
            if (e.NewValue == null)
            {
                return;
            }
            md.MapViewModel = e.NewValue as MapViewModel;
            md.m = md.MapViewModel;
            md.m.Data = md;
            md.UpdateCollection();
            md.MapViewModel.Back = new DelegateCommand<object>(md.OnBack, args => { return true; });
            md.MapViewModel.Create = new DelegateCommand<object>(md.OnCreate, args => { return true; });
            md.MapViewModel.Save = new DelegateCommand<object>(md.OnSave, args => { return true; });
            md.MapViewModel.Load = new DelegateCommand<object>(md.OnLoad, args => { return true; });
            md.MapViewModel.Clear = new DelegateCommand<object>(md.OnClear, args => { return true; });
        }

        private async void OnLoad(object param)
        {
            //if (EnsureUnsnapped())
            {
                _nodes.Clear();
                _connectors.Clear();
                diagramcontrol.Nodes = _nodes;
                diagramcontrol.Connectors = _connectors;
                StorageFile s;

                if (param == null)
                {
                    FileOpenPicker file = new FileOpenPicker();
                    file.CommitButtonText = "Load";
                    file.FileTypeFilter.Add(".xml");
                    this.MapViewModel.CurrentlyLoaded = string.Empty;
                    s = await file.PickSingleFileAsync();
                }
                else
                {
                    StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
                    StorageFolder st = await installedLocation.GetFolderAsync("MindMap");
                    s = await st.GetFileAsync(param.ToString() + ".xml");
                    this.MapViewModel.CurrentlyLoaded = param.ToString();
                }
                if (s != null)
                {
                    using (IRandomAccessStream readStream = await s.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        if (readStream != null)
                        {
                            Stream str = readStream.AsStream();
                            diagramcontrol.Load(str);
                            this.MapViewModel.DiagramVisibility = Visibility.Visible;
                        }
                    }
                }
                LayoutType = diagramcontrol.LayoutType;
                if (diagramcontrol.LayoutType.Equals("Bowtie"))
                {
                    diagramcontrol.DefaultConnectorType = ConnectorType.CubicBezier;
                    foreach (CustomNode node in _nodes)
                    {
                        if (node.IsSelected)
                        {
                            MapViewModel.SelectedObject = node;
                            (diagramcontrol.SelectedItems as CustomSelector).SelectedObject = MapViewModel.SelectedObject;
                        }
                        if (node.Type.Equals("root") || node.Type.Equals("left") || node.Type.Equals("right"))
                        {
                            if (node.Type.Equals("root"))
                            {
                                Rootnode = node;
                            }
                            var resourceDictionary = new ResourceDictionary()
                            {
                                Source = new Uri("ms-appx:///Template/ProcessNodeEditorTemplate.xaml", UriKind.RelativeOrAbsolute)
                            };
                            node.ContentTemplate =resourceDictionary["CNodeTemplate"] as DataTemplate;

                            (node.Content as NodeCustom).type = node.Type;
                        }
                        else
                        {
                            LineGeometry path = CreateContent(0, node.UnitWidth, node.UnitHeight);
                            (node.Content as NodeCustom).path = path;
                            var resourceDictionary = new ResourceDictionary()
                            {
                                Source = new Uri("ms-appx:///Template/ProcessNodeEditorTemplate.xaml", UriKind.RelativeOrAbsolute)
                            };
                            node.ContentTemplate = resourceDictionary["LeafNodeTemplate"] as DataTemplate;
                            (node.Content as NodeCustom).Foreground = (node.Content as NodeCustom).SelectedBrush;
                            (node.Content as NodeCustom).type = node.Type;
                        }
                        
                    }
                    _isloaded = true;
                }
                else
                {
                    diagramcontrol.DefaultConnectorType = ConnectorType.Line;
                    foreach (CustomNode node in _nodes)
                    {
                        if (node.IsSelected)
                        {
                            MapViewModel.SelectedObject = node;
                            (diagramcontrol.SelectedItems as CustomSelector).SelectedObject = MapViewModel.SelectedObject;
                        }
                    }
                }
            }
        }
        bool _isloaded = false;
        void diagramcontrol_LayoutUpdated(object sender, object e)
        {
            if (Rootnode != null && _isloaded && diagramcontrol.LayoutType.Equals("Bowtie"))
            {
                Updatebowtielayout("left");
                Updatebowtielayout("right");
                diagramcontrol.InvalidateMeasure();
                diagramcontrol.InvalidateArrange();
                _isloaded = false;
            }
            //throw new NotImplementedException();
        }

        private string _type;

        public string LayoutType
        {
            get { return _type; }
            set { _type = value;
            
            {
                OnLayoutTypeChanged(_type);
            }
           
            }
        }
        private CustomNode _rootnode = null;


        [DataMember]
        public CustomNode Rootnode
        {
            get { return _rootnode; }
            set
            {
                _rootnode = value;

            }
        }
       
        private void OnLayoutTypeChanged(string _type)
        {
            if(_type.Equals("Bowtie") && diagramcontrol.LayoutManager==null)
            {
                diagramcontrol.LayoutManager = new Syncfusion.UI.Xaml.Diagram.Layout.LayoutManager()
                {
                    Layout = new CustomLayout()
                };

                (diagramcontrol.LayoutManager.Layout as CustomLayout).HorizontalSpacing
                    = 20;
                (diagramcontrol.LayoutManager.Layout as CustomLayout).VerticalSpacing = 50;
                (diagramcontrol.LayoutManager.Layout as CustomLayout).SpaceBetweenSubTrees = 30;
                diagramcontrol.DefaultConnectorType = ConnectorType.CubicBezier;
            }
            else if (diagramcontrol.LayoutManager != null && _type.Equals("Circular"))
            {
                diagramcontrol.DefaultConnectorType = ConnectorType.Line;

            }
            //throw new NotImplementedException();
        }

        
        private NodePort AddPort(double p1, double p2)
        {
            NodePort port = new NodePort();

            port.NodeOffsetX = p1;

            port.NodeOffsetY = p2-0.5;
            return port;
        }

       

        private void Updatebowtielayout(string type)
        {
            if ((Rootnode.Info as INodeInfo).OutNeighbors != null && (Rootnode.Info as INodeInfo).OutNeighbors.ToList().Find(c => (c as CustomNode).Type == "left") != null && type != null && type.EndsWith("left"))
            {
                foreach (CustomNode item in MapViewModel.Nodes)
                {
                    if (item.Type.Equals("left") || item.Type.Equals("root") || item.Type.Equals("subleft"))
                    {
                        item.Constraints = item.Constraints & ~NodeConstraints.ExcludeFromLayout;
                    }
                    else
                    {
                        item.Constraints = item.Constraints | NodeConstraints.ExcludeFromLayout;
                    }
                }
                 (diagramcontrol.LayoutManager.Layout as CustomLayout).Orientation = TreeOrientation.RightToLeft;
                (diagramcontrol.LayoutManager.Layout as CustomLayout).UpdateLayout(Rootnode);
            }
            if ((Rootnode.Info as INodeInfo).OutNeighbors != null && (Rootnode.Info as INodeInfo).OutNeighbors.ToList().Find(c => (c as CustomNode).Type == "right") != null && type != null && type.EndsWith("right"))
            {
                foreach (CustomNode item in MapViewModel.Nodes)
                {
                    if (item.Type.Equals("right") || item.Type.Equals("root") || item.Type.Equals("subright"))
                    {
                        item.Constraints = item.Constraints & ~NodeConstraints.ExcludeFromLayout;
                    }
                    else
                    {
                        item.Constraints = item.Constraints | NodeConstraints.ExcludeFromLayout;
                    }
                }
                (diagramcontrol.LayoutManager.Layout as CustomLayout).Orientation = TreeOrientation.LeftToRight;
                (diagramcontrol.LayoutManager.Layout as CustomLayout).UpdateLayout(Rootnode);
            }

        }
      
        private async void OnSave(object param)
        {
            StorageFile s = null;
            //if (EnsureUnsnapped())
            {
                if (param == null)
                {
                    if (!string.IsNullOrEmpty(this.MapViewModel.CurrentlyLoaded))
                    {
                        StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
                        StorageFolder st = await installedLocation.GetFolderAsync("MindMap");

                        s = await st.CreateFileAsync(this.MapViewModel.CurrentlyLoaded + ".xml", CreationCollisionOption.ReplaceExisting);
                    }
                }
                else
                {
                    StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
                    StorageFolder st = await installedLocation.GetFolderAsync("MindMap");
                    s = await st.CreateFileAsync(param.ToString() + ".xml", CreationCollisionOption.ReplaceExisting);
                    this.MapViewModel.CurrentlyLoaded = param.ToString();
                }
                if (s != null)
                {
                    using (IRandomAccessStream writeStream = await s.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        if (writeStream != null)
                        {
                            Stream str = writeStream.AsStream();
                            diagramcontrol.Save(str);
                        }
                    }
                }
            }
        }

        //private bool EnsureUnsnapped()
        //{
        //    bool unsnapped = ((ApplicationView.Value != ApplicationViewState.Snapped) || ApplicationView.TryUnsnap());
        //    if (!unsnapped)
        //    {

        //    }
        //    return unsnapped;
        //}

        
        private async void OnCreate(object parameter)
        {
            int index = parameter.ToString().IndexOf("_type");
            string sttr = parameter.ToString().Substring(index + 5);
            parameter = parameter.ToString().Substring(0, index);
            StorageFile s = null;
            //if (EnsureUnsnapped())
            {
                StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
                StorageFolder st = await installedLocation.GetFolderAsync("MindMap");
                s = await st.CreateFileAsync(parameter.ToString() + ".xml", CreationCollisionOption.FailIfExists);
                this.MapViewModel.CurrentlyLoaded = parameter.ToString();
            }
            if (s != null)
            {
                using (StorageStreamTransaction writeStream = await s.OpenTransactedWriteAsync())
                {
                    SfDiagram empty = new SfDiagram();
                    empty.Nodes = new ObservableCollection<Node>();
                    empty.Connectors = new ObservableCollection<Connector>();
                    empty.Save(writeStream.Stream.AsStreamForWrite());
                    await writeStream.CommitAsync();
                }
                diagramcontrol.LayoutType = sttr;
                LayoutType = sttr;
                OnLoad(parameter);
               Rootnode= MapViewModel.RootNode();
               if (LayoutType.Equals("Bowtie"))
               {
                   Rootnode.Type = "root";
                   Rootnode.UnitWidth = 90;
                   Rootnode.UnitHeight = 40;
                   Rootnode.Constraints = Rootnode.Constraints ^ NodeConstraints.Draggable;
               }
            }
            
        }

        private void OnClear(object parameter)
        {
            foreach (CustomNode n in MapViewModel.Nodes.Where(n => n.AllowDelete.Equals(true)).ToList())
            {
                _nodes.Remove(n);
            }
            foreach (CustomConnector conn in MapViewModel.Lines)
            {
                conn.SourceNode = null;
                conn.TargetNode = null;
            }
            MapViewModel.Lines.Clear();
        }

        private void Back_Clicked(object sender, TappedRoutedEventArgs e)
        {
            this.MapViewModel.Back.Execute(null);
        }

        private  void OnBack(object obj)
        {
            p.IsOpen = false;
            this.MapViewModel.DiagramVisibility = Visibility.Collapsed;
            this.MapViewModel.Save.Execute(null);           
        }

        private void UpdateCollection()
        {
            _nodes = MapViewModel.Nodes;
            _connectors = MapViewModel.Lines;
        }
        
         void diagramcontrol_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            p.IsOpen = false;
        }

        void diagramcontrol_ItemSelectedEvent(object sender, DiagramEventArgs args)
        {
            if (args.Item is INode)
            {
                MapViewModel.SelectedObject = args.Item as CustomNode;
                (diagramcontrol.SelectedItems as CustomSelector).SelectedObject = MapViewModel.SelectedObject;
                SelectedNode = args.Item as CustomNode;
            }
        }
        void diagramcontrol_ItemTappedEvent(object sender, DiagramEventArgs args)
        {
            if (args.Item is INode)
            {
                p.IsOpen = false;
                MapViewModel.SelectedObject = args.Item as CustomNode;
                (diagramcontrol.SelectedItems as CustomSelector).SelectedObject = MapViewModel.SelectedObject;
                if (!(args.Item as CustomNode).AllowDelete)
                    (diagramcontrol.SelectedItems as CustomSelector).AllowDelete = Visibility.Collapsed;
                else
                    (diagramcontrol.SelectedItems as CustomSelector).AllowDelete = Visibility.Visible;
                
                
                SelectedNode = args.Item as CustomNode;
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


           
        }

        public Node GetNode(CustomNode c)
        {
            Node n = new Node();
            foreach(Node node in diagramcontrol.Page.Children.OfType<Node>())
            {
                CustomNode cus = (node.DataContext as CustomNode);
                if (cus == c)
                    n = node;
            }
            return n;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((diagramcontrol.SelectedItems as SelectorViewModel).Nodes != null && diagramcontrol.LayoutType.Equals("Bowtie"))
            {
                SelectedNode = ((IEnumerable<object>)(diagramcontrol.SelectedItems as SelectorViewModel).Nodes).First() as CustomNode;
                if (SelectedNode is CustomNode)
                {
                    if ((SelectedNode as CustomNode).Type.Equals("root"))
                    {
                        CustomNode n = new CustomNode();
                        n.UnitWidth = 60;
                        n.UnitHeight = 40;
                        n.Type = "right";
                        n.Constraints = n.Constraints ^ NodeConstraints.Draggable;
                        NodePort port = AddPort(60, 20);
                        NodePort port1 = AddPort(0, 20);

                        n.NodePorts = new ObservableCollection<INodePort>()
            {
                port,
                port1
            };
                        var resourceDictionary = new ResourceDictionary()
                        {
                            Source = new Uri("ms-appx:///Template/ProcessNodeEditorTemplate.xaml", UriKind.Absolute)
                        };
                        n.ContentTemplate = resourceDictionary["CNodeTemplate"] as DataTemplate;
                        MapViewModel.Nodes.Add(n);
                        Connect(SelectedNode, n, null);
                    }
                    else
                    {
                        CreateNode(SelectedNode, "subright");
                    }
                }
                Updatebowtielayout("right");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if ((diagramcontrol.SelectedItems as SelectorViewModel).Nodes != null)
            {
                SelectedNode = ((IEnumerable<object>)(diagramcontrol.SelectedItems as SelectorViewModel).Nodes).First() as CustomNode;
                if (diagramcontrol.LayoutType.Equals("Bowtie"))
                {
                    CreateBowtieLayout(SelectedNode);
                }
                else
                {
                    CustomNode n = new CustomNode();
                   // n.Constraints = n.Constraints &~ NodeConstraints.Draggable;
                    CustomConnector line = new CustomConnector();
                    n.InitialPair = new Nodepair(SelectedNode, n, line);
                    MapViewModel.Nodes.Add(n);
                    MapViewModel.Lines.Add(line);
                    MapViewModel.AddNode(n.Pair);
                }
            }
        }

        private void CreateBowtieLayout(CustomNode SelectedNode)
        {
            string type = null;
            
            if (SelectedNode is CustomNode)
            {
                CustomNode n=null;
                CustomConnector line=null;
                if ((SelectedNode as CustomNode).Type.Equals("root"))
                {
                    n = new CustomNode();
                    n.Constraints = n.Constraints ^ NodeConstraints.Draggable;
                    n.UnitWidth = 60;
                    n.UnitHeight = 40;
                    n.Type = "left";
                    type = "left";
                    NodePort port = AddPort(0, 20);
                    NodePort port1 = AddPort(60, 20);
                    n.NodePorts = new ObservableCollection<INodePort>()
            {
                port,
                port1
            };
                    var resourceDictionary = new ResourceDictionary()
                    {
                        Source = new Uri("ms-appx:///Template/ProcessNodeEditorTemplate.xaml", UriKind.Absolute)
                    };
                    n.ContentTemplate = resourceDictionary["CNodeTemplate"] as DataTemplate;
                    MapViewModel.Nodes.Add(n);
                    line=Connect(SelectedNode, n, null);
                }
                else
                {
                    if (SelectedNode.Type.Equals("right") || SelectedNode.Type.Equals("subright"))
                    {
                        CreateNode(SelectedNode, "subright");
                        type = "right";
                    }
                    else
                    {
                        CreateNode(SelectedNode, "subleft");
                        type = "left";
                    }
                }
               
            }

            Updatebowtielayout(type);
        }
            //throw new NotImplementedException();
        
    
        private void CreateNode(CustomNode SelectedNode, string type)
        {
            CustomNode n = new CustomNode();
            n.UnitWidth = 60;
            n.UnitHeight = 40;
            n.Type = type;
            string name = "node";
            n.Constraints = n.Constraints ^ NodeConstraints.Draggable;
            NodePort port = null;
            NodePort port1 = null;
            if (type.StartsWith("sub"))
            {
                port = AddPort(0, 40);
                port1 = AddPort(60, 40);
            }
           
            if (type.Equals("subleft") || type.Equals("subright"))
            {
                LineGeometry path = CreateContent(0, n.UnitWidth, n.UnitHeight);
                (n.Content as NodeCustom).path = path;
                var resourceDictionary = new ResourceDictionary()
                {
                    Source = new Uri("ms-appx:///Template/ProcessNodeEditorTemplate.xaml", UriKind.Absolute)
                };
                n.ContentTemplate = resourceDictionary["LeafNodeTemplate"] as DataTemplate;
            }
           
            if (port != null)
            {
                n.NodePorts = new ObservableCollection<INodePort>()
            {
                port,
                port1
            };
            }
            n.NodeAnnotations = new ObservableCollection<IAnnotation>()
            {
                new CustomLabel(){Content=name}
                
            };
            MapViewModel.Nodes.Add(n);
            if (type.Equals("subleft"))
                Connect(SelectedNode, n, port1);
            else
                Connect(SelectedNode, n, port);
        }

        private LineGeometry CreateContent(double p1, double p2, double p3)
        {
            LineGeometry line = new LineGeometry();
            line.StartPoint = new Point(p1, p3);
            line.EndPoint = new Point(p2, p3);
            return line;
               
           // throw new NotImplementedException();
        }

        private CustomConnector Connect(CustomNode SelectedNode, CustomNode n, NodePort port)
        {
            CustomConnector conn = new CustomConnector();
            conn.SourceNode = SelectedNode;
            conn.TargetNode = n;
            if (port != null)
                conn.TargetPort = port;
            if (SelectedNode.NodePorts!=null && SelectedNode.NodePorts.Count() > 1 && SelectedNode.Type.StartsWith("sub"))
            {
                if (SelectedNode.Type.Equals("subleft"))
                    conn.SourcePort = SelectedNode.NodePorts.ElementAt(0);
                else
                    conn.SourcePort = SelectedNode.NodePorts.ElementAt(1);
            }
            else if (SelectedNode.Type.Equals("left") || SelectedNode.Type.Equals("right")) 
            {
                if (SelectedNode.NodePorts != null && SelectedNode.NodePorts.Count() > 0)
                {
                    conn.SourcePort = SelectedNode.NodePorts.ElementAt(0);
                }
                
            }
            //else if (SelectedNode.Type.Equals("right"))
            //{
            //    if (SelectedNode.NodePorts != null && SelectedNode.NodePorts.Count() > 0)
            //    {
            //        conn.SourcePort = SelectedNode.NodePorts.ElementAt(0);
            //    }
            //}
            else if (SelectedNode.Type.Equals("root"))
            {
                if (n.Type.Equals("left"))
                {
                    conn.SourcePort = SelectedNode.NodePorts.ElementAt(0);
                    conn.TargetPort = n.NodePorts.ElementAt(1);
                }
                else
                {
                    conn.SourcePort = SelectedNode.NodePorts.ElementAt(1);
                    conn.TargetPort = n.NodePorts.ElementAt(1);

                }
            }
            conn.TargetDecorator = null;
            MapViewModel.Lines.Add(conn);
            conn.Loaded += conn_Loaded;
            return conn;
        }

        void conn_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as CustomConnector).Loaded -= conn_Loaded;
        }

        private void n_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            {
                if (!ManipulateOnNode(e))
                {
                    return;
                }
                CustomNode c = (e.OriginalSource as Node).DataContext as CustomNode;
                if (diagramcontrol.LayoutType.Equals("Circular"))
                {
                    foreach (CustomNode n in _nodes)
                    {
                        var resourceDictionary = new ResourceDictionary()
                        {
                            Source = new Uri("ms-appx:///Template/ProcessNodeEditorTemplate.xaml", UriKind.Absolute)
                        };

                        n.IsIntersect = false;
                        if (c != null)
                        {
                            if (c.Pair.Root != null)
                            {
                                if (c != (n as CustomNode) && (n as CustomNode != (c.Pair.Root)) && (c != n.Pair.Root))
                                {
                                    Rect r = (n.Info as INodeInfo).Bounds;
                                    m.CheckingNode = n;
                                    if ((r.Contains(new Point((c.Info as INodeInfo).Bounds.Left, (c.Info as INodeInfo).Bounds.Top)) || r.Contains(new Point((c.Info as INodeInfo).Bounds.Right, (c.Info as INodeInfo).Bounds.Top)) || r.Contains(new Point((c.Info as INodeInfo).Bounds.Left, (c.Info as INodeInfo).Bounds.Bottom)) || r.Contains(new Point((c.Info as INodeInfo).Bounds.Right, (c.Info as INodeInfo).Bounds.Bottom)) || r.Contains(new Point(c.OffsetX, c.OffsetY))) && !m.ChildCheck(c))
                                    {
                                        n.IsIntersect = true;
                                        if (n.Pair.Link != null)
                                            n.Pair.Link.ConnectorGeometryStyle = resourceDictionary["IntersectLine"] as Style;
                                        n.ContentTemplate = resourceDictionary["IntersectTemplate"] as DataTemplate;
                                    }
                                }
                            }
                        }
                        if (!n.IsIntersect && n != SelectedNode)
                        {
                            if (n.Pair.Link != null)
                                n.Pair.Link.ConnectorGeometryStyle = resourceDictionary["NormalLine"] as Style;
                            n.ContentTemplate = resourceDictionary["CNodeTemplate"] as DataTemplate;
                        }
                        n.IsIntersect = false;
                    } 
                }         
            }
            e.Handled = true;
        }

        private bool ManipulateOnNode(RoutedEventArgs e)
        {
            UIElement element = e.OriginalSource as UIElement;
            if (element is Node) { return true; }
            if (element == null)
                return false;
            Node n = FindVisualParent<Node>(element);
            if (n == null)
                return false;
            return true;
        }

        void n_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            if (diagramcontrol.LayoutType.Equals("Circular"))
            {
                try
                {
                    graphInfo.Commands.BringToFront.Execute(diagramcontrol);
                }
                catch { }
            }
            e.Handled = true;
        }

        public T FindVisualParent<T>(DependencyObject dobj) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(dobj);
            if (parent == null && parent is FrameworkElement)
            {
                parent = (parent as FrameworkElement).Parent;
            }
            if (parent is T)
            {
                return parent as T;
            }
            else if (parent != null)
            {
                return FindVisualParent<T>(parent);
            }
            else
            {
                return null;
            }
        }

        void n_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (diagramcontrol.LayoutType.Equals("Circular"))
            {
                var resourceDictionary = new ResourceDictionary()
                {
                    Source = new Uri("ms-appx:///Template/ProcessNodeEditorTemplate.xaml", UriKind.Absolute)
                };
                if (!ManipulateOnNode(e))
                {
                    return;
                }
                CustomNode c = (e.OriginalSource as Node).DataContext as CustomNode;
                if (c != null)
                {
                    if (c.Pair.Link != null)
                        c.Pair.Link.ConnectorGeometryStyle = resourceDictionary["NormalLine"] as Style;
                    c.ContentTemplate = resourceDictionary["CNodeTemplate"] as DataTemplate;
                    c.Angle = MapViewModel.FindAngle(c);
                    ObservableCollection<CustomNode> coll = new ObservableCollection<CustomNode>();
                    foreach (CustomNode n in _nodes)
                    {
                        if (!n.IsIntersect && n != SelectedNode)
                        {
                            if (n.Pair.Link != null)
                                n.Pair.Link.ConnectorGeometryStyle = resourceDictionary["NormalLine"] as Style;
                            n.ContentTemplate = resourceDictionary["CNodeTemplate"] as DataTemplate;
                        }
                        if (c != (n as CustomNode))
                        {
                            Rect r = (n.Info as INodeInfo).Bounds;
                            if (r.Contains(new Point((c.Info as INodeInfo).Bounds.Left, (c.Info as INodeInfo).Bounds.Top)) || r.Contains(new Point((c.Info as INodeInfo).Bounds.Right, (c.Info as INodeInfo).Bounds.Top)) || r.Contains(new Point((c.Info as INodeInfo).Bounds.Left, (c.Info as INodeInfo).Bounds.Bottom)) || r.Contains(new Point((c.Info as INodeInfo).Bounds.Right, (c.Info as INodeInfo).Bounds.Bottom)) || r.Contains(new Point(c.OffsetX, c.OffsetY)))
                            {
                                m.CheckingNode = n;
                                if (!m.ChildCheck(c))
                                {
                                    m.AllowUpdate = true;
                                    c.InitialPair = new Nodepair(n, c, c.Pair.Link);
                                    m.AddNode(c.Pair);
                                }
                            }
                        }
                    }
                    if (c.Childs != null)
                    {
                        foreach (CustomNode node in c.Childs)
                            coll.Add(node);
                        if (m.AllowUpdate && !m.ChildCheck(c))
                        {
                            foreach (CustomNode cn in coll)
                            {
                                m.RemoveNode(cn);
                            }
                            m.Update(c);
                        }
                    }
                }
                MapViewModel.AllowUpdate = false;
            }
            e.Handled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if ((diagramcontrol.SelectedItems as SelectorViewModel).Nodes != null && ((IEnumerable<object>)(diagramcontrol.SelectedItems as SelectorViewModel).Nodes).Count()>0)
            {
                SelectedNode = ((IEnumerable<object>)(diagramcontrol.SelectedItems as SelectorViewModel).Nodes).First() as CustomNode;
                if (SelectedNode.AllowDelete)
                {
                    if (diagramcontrol.LayoutType.Equals("Circular"))
                    {
                        MapViewModel.RemoveNode(SelectedNode);
                        SelectedNode.Pair.Root.Childs.Remove(SelectedNode);
                    }
                    else
                    {
                        DeleteNode(SelectedNode);

                        Updatebowtielayout(SelectedNode.Type);
                    }
                }
            }
           
        }


        private void DeleteNode(CustomNode SelectedNode)
        {
            if ((SelectedNode.Info as INodeInfo).OutNeighbors != null && (SelectedNode.Info as INodeInfo).OutNeighbors.Count() > 0)
            {
                for (int i = (SelectedNode.Info as INodeInfo).OutNeighbors.Count() - 1; (SelectedNode.Info as INodeInfo).OutNeighbors.Count() - 1 >= i && i >= 0; i--)
                {
                    CustomNode c = (SelectedNode.Info as INodeInfo).OutNeighbors.ElementAt(i) as CustomNode;
                    DeleteNode(c);
                }
            }
            if ((SelectedNode.Info as INodeInfo).InOutConnectors != null)
            {
                for (int i = (SelectedNode.Info as INodeInfo).InOutConnectors.Count() - 1; (SelectedNode.Info as INodeInfo).InOutConnectors.Count() - 1 >= i && i >= 0; i--)
                {
                    CustomConnector con = (SelectedNode.Info as INodeInfo).InOutConnectors.ElementAt(i) as CustomConnector;
                    con.SourceNode = null;
                    con.TargetNode = null;
                    MapViewModel.Lines.Remove(con);
                }
            }
            MapViewModel.Nodes.Remove(SelectedNode);

            //throw new NotImplementedException();
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            IsPickerClicked = true;
            if (SelectedNode is CustomNode && !(p.IsOpen))
            {
                double x = SelectedNode.OffsetX;
                double y = SelectedNode.OffsetY;
                double w1 = (SelectedNode.Info as INodeInfo).ActualWidth;
                double h1 = (SelectedNode.Info as INodeInfo).ActualHeight;
                double w = graphInfo.Viewport.Width;
                double h = graphInfo.Viewport.Height;
                double x1 = graphInfo.Viewport.Left;
                double y1 = graphInfo.Viewport.Top;
                double width = (((c as ColorPicker).Content as Grid).Children.First() as Syncfusion.UI.Xaml.Controls.Media.SfColorPicker).Width;
                double height = (((c as ColorPicker).Content as Grid).Children.First() as Syncfusion.UI.Xaml.Controls.Media.SfColorPicker).Height;
                if (width > (w - x + w1 + x1))
                    p.HorizontalOffset = SelectedNode.OffsetX - width - (SelectedNode.Info as INodeInfo).DesiredSize.Width / 2 - x1;
                else
                    p.HorizontalOffset = (SelectedNode.OffsetX + (SelectedNode.Info as INodeInfo).DesiredSize.Width / 2) - x1;
                if (height > h - y + y1)
                    p.VerticalOffset = SelectedNode.OffsetY - height - y1;
                else
                    p.VerticalOffset = SelectedNode.OffsetY - y1;
                p.Child = c;
                (p.Child as ColorPicker).DataContext = (m.SelectedObject as CustomNode).Content;
               // ((m.SelectedObject as CustomNode).Content as NodeCustom).PropertyChanged += MainPage_PropertyChanged;
                p.IsOpen = true;
            }
            
        }

        //void MainPage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals("SelectedBrush"))
        //    {
        //        Color forecolor = PerceivedBrightness((SelectedNode.Content as NodeCustom).SelectedBrush.Color) > 130 ? Colors.Black : Colors.White;
        //        Node n = GetNode(SelectedNode);
        //        n.Foreground = new SolidColorBrush(forecolor);
        //    }

        //}

        private int PerceivedBrightness(Color c)
        {
            return (int)Math.Sqrt(
            c.R * c.R * .241 +
            c.G * c.G * .691 +
            c.B * c.B * .068);
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            StorageFile s = null;
            FileSavePicker save = new FileSavePicker();
            StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
            save.FileTypeChoices.Add("xml", new List<string>() { ".xml" });
            s = await save.PickSaveFileAsync();
            if (s != null)
            {
                using (IRandomAccessStream writeStream = await s.OpenAsync(FileAccessMode.ReadWrite))
                {
                    if (writeStream != null)
                    {
                        Stream str = writeStream.AsStream();
                        diagramcontrol.Save(str);
                    }
                }
            }
        }

        private void Button_Unloaded(object sender, RoutedEventArgs e)
        {
            (sender as Button).Unloaded -= Button_Unloaded;
            (sender as Button).Click -= Button_Click;
        }
        private void Button_Unloaded_1(object sender, RoutedEventArgs e)
        {
            (sender as Button).Unloaded -= Button_Unloaded_1;
            (sender as Button).Click -= Button_Click_1;
        }
        private void Button_Unloaded_2(object sender, RoutedEventArgs e)
        {
            (sender as Button).Unloaded -= Button_Unloaded_2;
            (sender as Button).Click -= Button_Click_2;
        }
        private void Button_Unloaded_3(object sender, RoutedEventArgs e)
        {
            (sender as Button).Unloaded -= Button_Unloaded_3;
            (sender as Button).Click -= Button_Click_3;
        }

       
    }

    public class CustomConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ColortoBrushConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                SolidColorBrush s = value as SolidColorBrush;
                return s.Color;
            }
            else
                return null;
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Color c = (Color)value;
            return new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B));
        }   
       
    }

    public class VisibilityConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                object obj = (object)value;

                if (obj.GetType() == typeof(CustomNode))
                {
                   // if (MindMapDiagram._Layout.Equals("Bowtie"))
                    {
                        if ((obj as CustomNode).Type != null && ((obj as CustomNode).Type.Equals("root") || (obj as CustomNode).Type.EndsWith("right")))
                        {
                            return Visibility.Visible;
                        }
                    }

                }
                return Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                object obj = (object)value;

                if (obj.GetType() == typeof(CustomNode))
                {
                   // if (MindMapDiagram._Layout.Equals("Bowtie"))
                    {
                        if ((obj as CustomNode).Type != null && (obj as CustomNode).Type.EndsWith("right"))
                        {
                            return Visibility.Visible;
                        }
                    }

                }
                return Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    class CustomLayout : DirectedTreeLayout
    {
        protected override INodeInfo GetNextSibling(INode node)
        {
            INodeInfo nodeinfo = node.Info as INodeInfo;
            INode _parent = nodeinfo.InNeighbors.ElementAt(0) as CustomNode;
            INodeInfo parent = _parent.Info as INodeInfo;
            if ((_parent as CustomNode).Type.Equals("root"))
            {
                if (Orientation == TreeOrientation.RightToLeft)
                {
                    if (parent != null)
                    {
                        return GetNextChild(parent, node, "left");

                    }
                    else
                        return null;
                }
                else if (Orientation == TreeOrientation.LeftToRight)
                {
                    if (parent != null)
                    {
                        return GetNextChild(parent, node, "right");

                    }
                    else
                        return null;
                }
                else
                {
                    return base.GetNextSibling(node);
                }
            }
            else
            {
                return base.GetNextSibling(node);
            }
        }

        private INodeInfo GetNextChild(INodeInfo parent, INode node, string type)
        {
            var children = parent.OutNeighbors.ToList().FindAll(c => (c as CustomNode).Type.Equals(type));
            if (children.Contains(node as CustomNode))
            {
                int index = children.IndexOf(node as CustomNode);
                if (children != null && index < children.Count() - 1 && index >= 0)
                {
                    return (children.ElementAt(index + 1) as CustomNode).Info as INodeInfo;
                }
                else
                    return null;
            }
            return null;
        }

        protected override INodeInfo GetPreviousSibling(INode node)
        {
            INodeInfo nodeinfo = node.Info as INodeInfo;
            INode _parent = nodeinfo.InNeighbors.ElementAt(0) as CustomNode;
            INodeInfo parent = _parent.Info as INodeInfo;
            if ((_parent as CustomNode).Type.Equals("root"))
            {
                if (Orientation == TreeOrientation.RightToLeft)
                {
                    if (parent != null)
                    {
                        return GetPrevChild(parent, node, "left");

                    }
                    else
                        return null;
                }
                else if (Orientation == TreeOrientation.LeftToRight)
                {
                    if (parent != null)
                    {
                        return GetPrevChild(parent, node, "right");
                    }
                    else
                        return null;
                }
                else
                    return base.GetPreviousSibling(node);
            }
            else
            {
                return base.GetPreviousSibling(node);
            }
        }

        private INodeInfo GetPrevChild(INodeInfo parent, INode node, string type)
        {
            var children = parent.OutNeighbors.ToList().FindAll(c => (c as CustomNode).Type.Equals(type));
            if (children.Contains(node as CustomNode))
            {
                int index = children.IndexOf(node as CustomNode);
                if (children != null && index > 0 && index <= children.Count())
                {
                    return (children.ElementAt(index - 1) as CustomNode).Info as INodeInfo;

                }
                else
                    return null;
            }
            else
                return null;
        }

        protected override INodeInfo GetFirstChild(INode node)
        {
            if ((node as CustomNode).Type.Equals("root"))
            {
                INodeInfo parent = node.Info as INodeInfo;
                if (Orientation == TreeOrientation.RightToLeft && parent.OutNeighbors.Count() > 0)
                {
                    return parent.OutNeighbors.ToList().Find(c => (c as CustomNode).Type == "left").Info as INodeInfo;
                }
                else if (Orientation == TreeOrientation.LeftToRight && parent.OutNeighbors.Count() > 0)
                {
                    return parent.OutNeighbors.ToList().Find(c => (c as CustomNode).Type == "right").Info as INodeInfo;
                }
                else
                    return base.GetFirstChild(node);
            }
            else
            {
                return base.GetFirstChild(node);
            }
        }

        protected override INodeInfo GetLastChild(INode node)
        {
            if ((node as CustomNode).Type.Equals("root"))
            {
                INodeInfo parent = node.Info as INodeInfo;
                if (Orientation == TreeOrientation.RightToLeft && parent.OutNeighbors.Count() > 0)
                {
                    return (parent.OutNeighbors.ToList().FindAll(c => (c as CustomNode).Type == "left")).Last().Info as INodeInfo;
                }
                else if (Orientation == TreeOrientation.LeftToRight && parent.OutNeighbors.Count() > 0)
                {
                    return (parent.OutNeighbors.ToList().FindAll(c => (c as CustomNode).Type == "right")).Last().Info as INodeInfo;
                }
                else
                    return base.GetLastChild(node);
            }
            else
            {
                return base.GetLastChild(node);
            }
        }
    }
    
    
    public class CustomDiagram : SfDiagram
    {
        public ManipulationStartedEventHandler started { get; set; }
        public ManipulationCompletedEventHandler complete { get; set; }
        public ManipulationDeltaEventHandler delta { get; set; }
        private string _type = "";
        [DataMember]
        public string LayoutType
        {
            get { return _type; }
            set { _type = value; }
        }

        protected override Node GetNodeForItemOverride(object item)
        {
            CNode n = new CNode()
            {
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch
            };
            Binding foregroundBinding = new Binding();
            foregroundBinding.Path = new PropertyPath("Content.Foreground");
            foregroundBinding.Source = n;
            n.SetBinding(ForegroundProperty, foregroundBinding);
            n.AddHandler(Node.ManipulationStartedEvent, started, true);
            n.AddHandler(Node.ManipulationCompletedEvent, complete, true);
            n.AddHandler(Node.ManipulationDeltaEvent, delta, true);
            n.Unloaded += n_Unloaded;
            return n;
        }

        void n_Unloaded(object sender, RoutedEventArgs e)
        {
            if (started!=null && complete!=null && delta!=null)
            {
                (sender as Node).RemoveHandler(Node.ManipulationStartedEvent, started);
                (sender as Node).RemoveHandler(Node.ManipulationCompletedEvent, complete);
                (sender as Node).RemoveHandler(Node.ManipulationDeltaEvent, delta); 
            }
            (sender as Node).Unloaded -= n_Unloaded;
        }
    }
}
