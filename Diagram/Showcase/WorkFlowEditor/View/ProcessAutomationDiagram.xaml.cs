using Syncfusion.UI.Xaml.Controls.Navigation;
using Syncfusion.UI.Xaml.Controls.Notification;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System.Diagnostics;
using Syncfusion.SampleBrowser.UWP.Diagram;

//The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WorkFlowEditor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProcessAutomationDiagram : Page
    {
        private IGraphInfo graphInfo;
        public ProcessAutomationDiagram()
        {
            this.InitializeComponent();
            graphInfo = sfdiagram.Info as IGraphInfo;
            sfdiagram.DefaultConnectorType = ConnectorType.Line;
            (sfdiagram.SelectedItems as SelectorViewModel).SelectorConstraints &= ~SelectorConstraints.QuickCommands;
            sfdiagram.Tapped += sfdiagram_Tapped;
            graphInfo.ItemTappedEvent += sfdiagram_ItemTappedEvent;
            sfdiagram.SFSelector.Style = this.Resources["selector"] as Style;
            graphInfo.ItemSelectedEvent += sfdiagram_ItemSelectedEvent;
            sfdiagram.SnapSettings.SnapConstraints = SnapConstraints.All;
            sfdiagram.KnownTypes = () => new List<Type>()
                {
                    typeof(NodeContent)
                };
            sfdiagram.PageSettings = null;
            sfdiagram.Menu = null;
            graphInfo.ConnectorTargetChangedEvent += sfdiagram_ConnectorTargetChangedEvent;
            this.Unloaded += ProcessAutomationDiagram_Unloaded;
            //(sfdiagram.SelectedItems as CustomSelector).Drawing = new DelegateCommand<object>(OnDraw, args => { return true; });

           // CreateNodes();
        }

        void ProcessAutomationDiagram_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sfdiagram != null)
            {
                sfdiagram.Tapped -= sfdiagram_Tapped;
                graphInfo.ItemTappedEvent -= sfdiagram_ItemTappedEvent;
                graphInfo.ItemSelectedEvent -= sfdiagram_ItemSelectedEvent;
                graphInfo.ConnectorTargetChangedEvent -= sfdiagram_ConnectorTargetChangedEvent;
            }
            this.Unloaded -= ProcessAutomationDiagram_Unloaded;
            foreach (INode item in (IEnumerable<object>)sfdiagram.Nodes)
            {
                NodeContent cont = item.Content as NodeContent;
                if (cont != null)
                {
                    cont.Dispose();
                }
            }
            (sfdiagram.Nodes as DiagramCollection).Clear();
            (sfdiagram.Connectors as DiagramCollection).Clear();
            sfdiagram.Nodes = null;
            sfdiagram.Connectors = null;
            graphInfo = null;
            sfdiagram = null;
            DataContext = null;
            this.ProcessAutomationViewModel = null;
            backButton.Tapped -= Back_Clicked;
            foreach (SfRadialMenuItem item in radialMenu.Items)
            {
                if (item != null)
                {
                    item.Click -= SfRadialMenuItem_Click_1;
                    Button but = item.Header as Button;
                    if (but != null)
                    {
                        but.Click -= Button_Click_2;
                    }
                }
            }
            radialMenu.Items.Clear();
        }

       IScrollInfo sv = null;
    
        private void CreateNodes()
        {
            ProcessAutomationNode node = CreateStartNode1(35, 35, "Start", "Start", 100, 200);

            ProcessAutomationNode node1 = CreateStartNode1(100, 35, "Task1", "Send Doctor Request", 200, 200);
            ProcessAutomationNode node2 = CreateStartNode1(100, 35, "Task1", "Receive Doctor Request", 200, 350);

            ProcessAutomationNode node3 = CreateStartNode1(100, 35, "Task1", " Send Availability Request", 200, 450);
            ProcessAutomationNode node4 = CreateStartNode1(100, 35, "Task1", " Receive Availability Request", 550, 450);
            ProcessAutomationNode node5 = CreateStartNode1(100, 35, "Task1", "Send Booking" , 650, 450);
            ProcessAutomationNode node6 = CreateStartNode1(100, 35, "Task1", " Receive Booking", 750, 450);
            ProcessAutomationNode node7 = CreateStartNode1(100, 35, "Task1", "Send Appointment", 850, 450);
            ProcessAutomationNode node8 = CreateStartNode1(100, 35, "Task1", "Receive Appointment", 850,200);
            ProcessAutomationNode node9 = CreateStartNode1(100, 35, "Task1", "Go to the Doctor", 950, 200);
            ProcessAutomationNode node10 = CreateStartNode1(100, 35, "Task1", "Receive Prescription  ", 1050,200);
            //ProcessAutomationNode node11 = CreateStartNode1(100, 35, "Task1", "Cooling/Ageing", 900, 425);
            //ProcessAutomationNode node12 = CreateStartNode1(100, 35, "Task1", " Packaging ", 1050, 425);
            //ProcessAutomationNode node13 = CreateStartNode1(100, 35, "Task1", "Storage/Distribution", 1200, 425);
            ProcessAutomationNode node14 = CreateStartNode1(35, 35, "End", "End", 1150, 200);


            Connect(node, node1);
            Connect(node1, node2);
            Connect(node2, node3);
            Connect(node3, node4);
            Connect(node4, node5);
            Connect(node5, node6);
            Connect(node6, node7);
            Connect(node7, node8);
            Connect(node8, node9);
            Connect(node9, node10);
            Connect(node10, node14);
            Connect(node1, node7);
        }

        private void Connect(ProcessAutomationNode node, ProcessAutomationNode node1)
        {
            ProcessAutomationConnector con = new ProcessAutomationConnector()
            {
                SourceNode = node,
                TargetNode = node1
            };
            (sfdiagram.Connectors as DiagramCollection).Add(con);
        }

        private ProcessAutomationNode CreateStartNode1(double p1, double p2, string p3, string p4, double p5, double p6)
        {
            ProcessAutomationNode node = new ProcessAutomationNode()
            {
                OffsetX = p5,
                OffsetY = p6,
                //Width = p1,
                UnitHeight = p2,
                Content = new NodeContent()
                {
                    DispalyText = p4
                }
            };
            node.NodeContent = node.Content as NodeContent;
            if (p3 == "Task1")
            {
                //node.MinWidth = 100;
                (node.Content as NodeContent).IsTask = true;
                node.ContentTemplate = this.Resources["tasknodetemplate"] as DataTemplate;
            }
            else
            {
                node.ContentTemplate = this.Resources["nodetemplate"] as DataTemplate;
            }
            (sfdiagram.Nodes as DiagramCollection).Add(node);
            return node;
        }

        ObservableCollection<ProcessAutomationNode> intersectednodes = new ObservableCollection<ProcessAutomationNode> ();
        void sfdiagram_ConnectorTargetChangedEvent(object sender, ChangeEventArgs<object, ConnectorChangedEventArgs> args)
        {
            if (args.NewValue.DragState == DragState.Completed)
            {
                UpdatePreviousSelection((args.Item as ProcessAutomationConnector).SourceNode as ProcessAutomationNode);
                if (args.NewValue.Node == null)
                {
                    (args.Item as ProcessAutomationConnector).SourceNode = null;
                    (args.Item as ProcessAutomationConnector).TargetNode = null;
                    (sfdiagram.Connectors as DiagramCollection).Remove(args.Item as ProcessAutomationConnector);
                }

                foreach (Node n in sfdiagram.Page.Children.OfType<Node>())
                {
                    foreach(ProcessAutomationNode o in intersectednodes)
                    {
                        if (n.DataContext == o)
                        {
                            (n.Content as NodeContent).FillBrush = new SolidColorBrush(Colors.Transparent);             
                        }
                    }
                }
                    
                
            }
            else if (args.NewValue.DragState == DragState.Dragging)
            {
                if (args.NewValue.Node != null)
                {
                    if (!intersectednodes.Contains((args.Item as ProcessAutomationConnector).TargetNode as ProcessAutomationNode))
                    {
                        intersectednodes.Add((args.Item as ProcessAutomationConnector).TargetNode as ProcessAutomationNode);
                    }
                    foreach (Node n in sfdiagram.Page.Children.OfType<Node>())
                    {
                        if (n.DataContext == (args.Item as ProcessAutomationConnector).TargetNode)
                        {
                            (n.Content as NodeContent).FillBrush = new SolidColorBrush(Colors.Black);
                        }
                    }
                }
                else
                {
                    foreach (Node n in sfdiagram.Page.Children.OfType<Node>())
                    {
                        foreach (ProcessAutomationNode o in intersectednodes)
                        {
                            if (n.DataContext == o)
                            {
                                (n.Content as NodeContent).FillBrush = new SolidColorBrush(Colors.Transparent);  
                            }
                        }
                    }
                }
                    
            }
            (sfdiagram.SelectedItems as CustomSelector).fillbrush = new SolidColorBrush(Colors.White);
        }    

        private void UpdatePreviousSelection(ProcessAutomationNode processAutomationNode)
        {
            foreach (Node node in sfdiagram.Page.Children.OfType<Node>())
            {
                if (node.DataContext == processAutomationNode)
                {
                    (node.Content as NodeContent).BorderBrush = new SolidColorBrush(Colors.Transparent);
                    (node.Content as NodeContent).BorderThick = new Thickness(0);                    
                }
            }
        }

        private void OnDraw(object obj)
        {
            //DrawParam param = new DrawParam(e, selectorViewModel.Nodes.FirstOrDefault());
            //this.Command.Execute(param);
        }

        ProcessAutomationNode PreviousSelectedNode = null;
        void sfdiagram_ItemTappedEvent(object sender, DiagramEventArgs args)
        {
            if (args.Item is IConnector)
            {

                (sfdiagram.SelectedItems as CustomSelector).SelectedObject = args.Item as ProcessAutomationConnector;
                ProcessAutomationViewModel.SelectedObject = args.Item as ProcessAutomationConnector;
            }
            else if (args.Item is INode)
            {
                if (PreviousSelectedNode != null)
                {
                    UpdatePreviousSelection(PreviousSelectedNode);
                }

                (sfdiagram.SelectedItems as CustomSelector).SelectedObject = args.Item as ProcessAutomationNode;
                PreviousSelectedNode = (args.Item as ProcessAutomationNode);
                ProcessAutomationViewModel.SelectedObject = args.Item as ProcessAutomationNode;
            }
        }

        
        void sfdiagram_ItemSelectedEvent(object sender, DiagramEventArgs args)
        {
            if (args.Item is IConnector)
            {
                (sfdiagram.SelectedItems as CustomSelector).SelectedObject = args.Item as ProcessAutomationConnector;

            }
            else if (args.Item is INode)
            {
                (sfdiagram.SelectedItems as CustomSelector).SelectedObject = args.Item as ProcessAutomationNode;
                ProcessAutomationViewModel.SelectedObject = args.Item as ProcessAutomationNode;
            }
            sfdiagram.CommandManager.View = (Control)Window.Current.Content;
        }

        private SolidColorBrush ColorConverter(string hexaColor)
        {
            if (hexaColor != null)
            {
                byte r = Convert.ToByte(hexaColor.Substring(1, 2), 16);
                byte g = Convert.ToByte(hexaColor.Substring(3, 2), 16);
                byte b = Convert.ToByte(hexaColor.Substring(5, 2), 16);
                byte a = Convert.ToByte(hexaColor.Substring(7, 2), 16);
                SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(r, g, b, a));
                return myBrush;
            }
            return null;
        }

        private Style GetNodeStyle()
        {
            //string stroke=
            Style s = new Style(typeof(Windows.UI.Xaml.Shapes.Path));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.FillProperty, ColorConverter("#FF70C923")));
            //s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, new SolidColorBrush(Colors.Black)));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeThicknessProperty, 3d));
            //s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.DataProperty, "M41.58,0 L52.2686,10.6886 L41.58,21.3773 L41.58,13.8557 L6.49995,13.8557 L6.49995,7.85574 L41.58,7.85574 z"));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StretchProperty, Stretch.Fill));
            return s;
        }

        private Style GetNodeStyle1()
        {
            Style s = new Style(typeof(Windows.UI.Xaml.Shapes.Path));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.FillProperty, Colors.DeepSkyBlue));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StretchProperty, Stretch.Fill));
            return s;
        }

        Point TapPosition = new Point();

         void sfdiagram_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //if (IsShowRadialMenu == true)
            //{
                UIElement ui = e.OriginalSource as UIElement;
                foreach (var elements in VisualTreeHelper.FindElementsInHostCoordinates(e.GetPosition(null), null))
                {
                    if (elements is Node)
                    {
                        if (!(elements is ISelector))
                        {
                            ProcessAutomationViewModel.SelectedObject = (elements as Node).DataContext as ProcessAutomationNode;
                            if ((elements as Node).DataContext != null &&
                                ((elements as Node).DataContext as ProcessAutomationNode) != null &&
                                (((elements as Node).DataContext as ProcessAutomationNode).Content as NodeContent).IsTask)
                                ShowContextMenu();
                            HideRadialMenu();
                            return;
                        }
                        return;
                    }
                    else if (elements is Connector)
                    {
                        HideContextMenu();
                        HideRadialMenu();
                        HideEditors();
                        return;
                    }
                    else if (elements is SfDiagram)
                    {
                        ClearSelection();
                    }

                    else if (elements is PropertyEditor)
                    {

                        HideRadialMenu();
                        return;
                    }

                }
            //}
             foreach (ProcessAutomationNode nodes in ((IEnumerable<object>)sfdiagram.Nodes).OfType<ProcessAutomationNode>())
             {
                 if ((nodes.Content as NodeContent).DispalyText == "Start")
                 {
                     //Start.IsHitTestVisible = false;
                     Start.Opacity = 0.40;
                 }
                 else if ((nodes.Content as NodeContent).DispalyText == "End")
                 {
                     //End.IsHitTestVisible = false;
                     End.Opacity = 0.40;
                 }
                 //if (ProcessAutomationViewModel.IsStartAdded)
                 //{
                 //    Start.IsEnabled = false;
                 //}
                 //if (ProcessAutomationViewModel.IsEndAdded)
                 //{
                 //    End.IsEnabled = false;
                 //}
                 //  if (!ProcessAutomationViewModel.IsStartAdded)
                 //    {
                 //        Start.IsEnabled = true;
                 //    }
                 //    if (!ProcessAutomationViewModel.IsEndAdded)
                 //    {
                 //        End.IsEnabled = true;
                 //    }
             }

             if (((IEnumerable<object>)sfdiagram.Nodes).OfType<ProcessAutomationNode>().Count() == 0)
             {
                 //Start.IsHitTestVisible = true;
                 //End.IsHitTestVisible = true;

                 Start.Opacity = 1;
                 End.Opacity = 1;
             }

             ShowRadailMenu(e.GetPosition(this));
             TapPosition = e.GetPosition(this);
             HideEditors();
             HideContextMenu();
        }
            //Control co = ui.GetParent<Node>();
            //if (co != null && co is Node && co.GetType() != typeof(Syncfusion.UI.Xaml.Diagram.Selector))
            //{
               
            //}
            //else
            //{
            //    co = ui.GetParent<Connector>();
            //    if (co != null)
            //    {
            //        HideContextMenu();
            //        HideRadialMenu();
            //        HideEditors();
            //    }
            //    else
            //    {
            //        co = ui.GetParent<SelectorItem>();
            //        if (co != null)
            //        {

            //        }
            //        else
            //        {
                        

            //    }
            //}

         private void ClearSelection()
         {
             if (sfdiagram.SelectedItems != null)
             {
                 CustomSelector selector = sfdiagram.SelectedItems as CustomSelector;
                 if (selector.SelectedObject != null)
                 {
                     if (selector.SelectedObject is ProcessAutomationNode)
                     {
                         (selector.SelectedObject as ProcessAutomationNode).IsSelected = false;
                     }
                     else if (selector.SelectedObject != null)
                     {
                         (selector.SelectedObject as ProcessAutomationConnector).IsSelected = false;
                     }
                 }
             }
         }

        private void ShowRadailMenu(Point position)
        {
            radialMenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
            radialMenu.IsOpen = false;
            radialMenu.UpdateLayout();
            radialMenu.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            radialMenu.IsOpen = true;
            Point tap = sfdiagram.Page.TransformToVisual(sfdiagram.Page).TransformPoint(position);
            double x = tap.X - radialMenu.RadiusX;
            double y = tap.Y - radialMenu.RadiusY;
            x = Math.Min(Math.Max(0, x), ActualWidth - radialMenu.RadiusX * 2);
            y = Math.Min(Math.Max(0, y), ActualHeight - radialMenu.RadiusY * 2);
            radialMenu.RenderTransform = new TranslateTransform() { X = x, Y = y };
            radialMenu.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }

      

        public ProcessAutomationViewModel ProcessAutomationViewModel
        {
            get { return (ProcessAutomationViewModel)GetValue(ProcessAutomationViewModelProperty); }
            set { SetValue(ProcessAutomationViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChartViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProcessAutomationViewModelProperty =
            DependencyProperty.Register("ProcessAutomationViewModel", typeof(ProcessAutomationViewModel), typeof(ProcessAutomationDiagram), new PropertyMetadata(null, OnPropertyChangedCallBack));

        private static void OnPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProcessAutomationDiagram p = d as ProcessAutomationDiagram;
            if (e.OldValue != null)
            {
                ProcessAutomationViewModel vm = e.OldValue as ProcessAutomationViewModel;
                vm.Save = null;
                vm.Load = null;
                vm.Create = null;
                vm.Back = null;
                vm.Play = null;
                vm.Delete = null;
                vm.Clear = null;
            }
            if (e.NewValue == null)
            {
                return;
            }
            p.ProcessAutomationViewModel = e.NewValue as ProcessAutomationViewModel;
            p.ProcessAutomationViewModel.Save = new DelegateCommand<object>(p.OnSave, args => { return true; });
            p.ProcessAutomationViewModel.Load = new DelegateCommand<object>(p.OnLoad, args => { return true; });
            p.ProcessAutomationViewModel.Create = new DelegateCommand<object>(p.OnCreate, args => { return true; });
            p.ProcessAutomationViewModel.Back = new DelegateCommand<object>(p.OnBack, args => { return true; });
            p.ProcessAutomationViewModel.Play = new DelegateCommand<object>(p.OnPlay, args => { return true; });
            p.ProcessAutomationViewModel.Delete = new DelegateCommand<object>(p.OnDelete, args => { return true; });
            p.ProcessAutomationViewModel.Clear = new DelegateCommand<object>(p.OnClear, args => { return true; });
        }

        private void OnClear(object obj)
        {
            (sfdiagram.Nodes as DiagramCollection).Clear();
            (sfdiagram.Connectors as DiagramCollection).Clear();
            HideContextMenu();
        }

        private void OnDelete(object obj)
        {
            if (this.ProcessAutomationViewModel.SelectedObject is ProcessAutomationNode)
            {
                ObservableCollection<ProcessAutomationConnector> outcon = new ObservableCollection<ProcessAutomationConnector>();
                ObservableCollection<ProcessAutomationConnector> incon = new ObservableCollection<ProcessAutomationConnector>();
                if (((this.ProcessAutomationViewModel.SelectedObject as ProcessAutomationNode).Info as INodeInfo).OutConnectors != null)
                {
                    foreach (ProcessAutomationConnector con in ((this.ProcessAutomationViewModel.SelectedObject as ProcessAutomationNode).Info as INodeInfo).OutConnectors)
                    {
                        outcon.Add(con);
                        
                    }
                    foreach (ProcessAutomationConnector c in outcon)
                    {
                        (c as ProcessAutomationConnector).TargetNode = null;
                        (c as ProcessAutomationConnector).SourceNode = null;
                        (sfdiagram.Connectors as DiagramCollection).Remove(c as ProcessAutomationConnector);
                    }
                }

                
                if (((this.ProcessAutomationViewModel.SelectedObject as ProcessAutomationNode).Info as INodeInfo).InConnectors != null)
                {
                    foreach (ProcessAutomationConnector con in ((this.ProcessAutomationViewModel.SelectedObject as ProcessAutomationNode).Info as INodeInfo).InConnectors)
                    {
                        incon.Add(con);                        
                    }
                    foreach (ProcessAutomationConnector c in incon)
                    {
                        (c as ProcessAutomationConnector).TargetNode = null;
                        (c as ProcessAutomationConnector).SourceNode = null;
                        (sfdiagram.Connectors as DiagramCollection).Remove(c as ProcessAutomationConnector);
                    }
                }
                (sfdiagram.Nodes as DiagramCollection).Remove(this.ProcessAutomationViewModel.SelectedObject as ProcessAutomationNode);
            }
            else if ((sfdiagram.SelectedItems as SelectorViewModel).Connectors != null)
            {
                foreach (var con in ((IEnumerable<object>)(sfdiagram.SelectedItems as SelectorViewModel).Connectors)
                                    .OfType<ProcessAutomationConnector>().ToList())
                {
                    con.TargetNode = null;
                    con.SourceNode = null;
                    (sfdiagram.Connectors as DiagramCollection).Remove(con);
                }
            }
            HideContextMenu();
        }


        private void OnBack(object obj)
        {
            this.ProcessAutomationViewModel.DiagramVisibility = Visibility.Collapsed;
            this.ProcessAutomationViewModel.Save.Execute(null);
            HideContextMenu();
            HideRadialMenu();
        }

        private void Back_Clicked(object sender, TappedRoutedEventArgs e)
        {
            this.ProcessAutomationViewModel.Back.Execute(null);
        }

        private async void OnCreate(object parameter)
        {
            (sfdiagram.Nodes as DiagramCollection).Clear();
            (sfdiagram.Connectors as DiagramCollection).Clear();
            StorageFile s = null;
            //if (EnsureUnsnapped())
            {
                StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
                StorageFolder st = await installedLocation.GetFolderAsync("WorkFlow");
                s = await st.CreateFileAsync(parameter.ToString() + ".xml", CreationCollisionOption.FailIfExists);
                this.ProcessAutomationViewModel.CurrentlyLoaded = parameter.ToString();
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

                ProcessAutomationViewModel.DiagramVisibility = Visibility.Visible;
                this.GetParent<WorkFlowEditor.MainPage>().BottomAppBar.IsSticky = true;
                this.GetParent<WorkFlowEditor.MainPage>().BottomAppBar.IsOpen = true;
                //OnLoad(parameter);
            }
        }

        private async void OnLoad(object param)
        {
            (sfdiagram.Nodes as DiagramCollection).Clear();
                (sfdiagram.Connectors as DiagramCollection).Clear();
            //if (EnsureUnsnapped())
            {
                StorageFile s;
                if (param == null)
                {
                    FileOpenPicker file = new FileOpenPicker();
                    file.CommitButtonText = "Load";
                    file.FileTypeFilter.Add(".xml");
                    this.ProcessAutomationViewModel.CurrentlyLoaded = string.Empty;
                    s = await file.PickSingleFileAsync();
                }
                else
                {
                    StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
                    StorageFolder st = await installedLocation.GetFolderAsync("WorkFlow");
                    s = await st.GetFileAsync(param.ToString() + ".xml");
                    this.ProcessAutomationViewModel.CurrentlyLoaded = param.ToString();
                    if (((IEnumerable<object>)sfdiagram.Nodes).OfType<ProcessAutomationNode>().Count() == 0)
                    {
                        //Start.IsHitTestVisible = false;
                        //End.IsHitTestVisible = false;

                        Start.Opacity = 0.40;
                        End.Opacity = 0.40;
                    }
                    else
                    {
                        foreach (ProcessAutomationNode n in ((IEnumerable<object>)sfdiagram.Nodes).OfType<ProcessAutomationNode>())
                        {
                            if ((n.Content as NodeContent).DispalyText == "Start")
                            {
                                //Start.IsHitTestVisible = true;
                                Start.Opacity = 1;
                            }

                            else if ((n.Content as NodeContent).DispalyText == "End")
                            {
                                //End.IsHitTestVisible = true;
                                End.Opacity = 1;
                            }
                        }
                    }
                }
               
                if (s != null)
                {
                    //(sfdiagram.Nodes as DiagramCollection).Clear();
                    using (IRandomAccessStream readStream = await s.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        if (readStream != null)
                        {
                            Stream str = readStream.AsStream();
                            sfdiagram.Load(str);
                            ProcessAutomationViewModel.IsPlay = true;
                            foreach (ProcessAutomationNode node in ((IEnumerable<object>)sfdiagram.Nodes).OfType<ProcessAutomationNode>())
                            {
                                node.Constraints = node.Constraints | NodeConstraints.Draggable;
                                node.Content = new NodeContent();
                                //node.Content =;
                                if (node.NodeContent != null)
                                {
                                    (node.Content as NodeContent).DispalyText = node.NodeContent.DispalyText;                               
                                    (node.Content as NodeContent).Save = new DelegateCommand<object>(OnSave1, args => { return true; });
                                    (node.Content as NodeContent).OpenEditor = new DelegateCommand<object>(OnOpen, args => { return true; });
                                    (node.Content as NodeContent).Done = new DelegateCommand<object>(OnDone, args => { return true; });                             

                                    if ((node.NodeContent is NodeContent) && (node.NodeContent as NodeContent).IsTask)
                                    {
                                        node.ContentTemplate = this.Resources["tasknodetemplate"] as DataTemplate;
                                    }
                                    else
                                    {
                                        if (node.NodeType == "Decision")
                                        {
                                            node.ContentTemplate = this.Resources["decisionnodetemplate"] as DataTemplate;
                                        }
                                        else
                                        {
                                            if ((node.Content as NodeContent).DispalyText == "Start")
                                            {
                                                node.ContentTemplate = this.Resources["Startnodetemplate"] as DataTemplate;
                                            }
                                            else if ((node.Content as NodeContent).DispalyText == "End")
                                            {
                                                node.ContentTemplate = this.Resources["Endnodetemplate"] as DataTemplate;
                                            }
                                            else
                                            {
                                                node.ContentTemplate = this.Resources["nodetemplate"] as DataTemplate;
                                            }
                                        }
                                    }
                                }
                            }

                            this.ProcessAutomationViewModel.DiagramVisibility = Visibility.Visible;
                        }
                    }
                }
            }
            this.GetParent<WorkFlowEditor.MainPage>().BottomAppBar.IsSticky = true;
            this.GetParent<WorkFlowEditor.MainPage>().BottomAppBar.IsOpen = true;
        }

        
        private async void OnSave(object param)
        {
            StorageFile s = null;
            //if (EnsureUnsnapped())
            {
                if (param == null)
                {
                    if (!string.IsNullOrEmpty(this.ProcessAutomationViewModel.CurrentlyLoaded))
                    {
                        StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
                        StorageFolder st = await installedLocation.GetFolderAsync("WorkFlow");

                        s = await st.CreateFileAsync(this.ProcessAutomationViewModel.CurrentlyLoaded + ".xml", CreationCollisionOption.ReplaceExisting);
                    }
                }
                else
                {
                    StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
                    StorageFolder st = await installedLocation.GetFolderAsync("WorkFlow");
                    s = await st.CreateFileAsync(param.ToString() + ".xml", CreationCollisionOption.ReplaceExisting);
                    this.ProcessAutomationViewModel.CurrentlyLoaded = param.ToString();
                }
                if (s != null)
                {
                    using (IRandomAccessStream writeStream = await s.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        if (writeStream != null)
                        {
                            Stream str = writeStream.AsStream();
                            sfdiagram.Save(str);
                        }
                    }
                }
            }
        }
        private void ShowContextMenu()
        {
            //if (!contextmenu.IsOpen)
            //{
            //    Rect node;
            //    if (this.ProcessAutomationViewModel.SelectedObject is ProcessAutomationNode)
            //    {
            //        ProcessAutomationNode n = this.ProcessAutomationViewModel.SelectedObject as ProcessAutomationNode;
            //        node = (n.Info as INodeInfo).Bounds;
            //    }
            //    else
            //    {
            //        return;
            //    }

            //    node = sfdiagram.Page.TransformToVisual(this).TransformBounds(node);
            //    contextmenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //    //contextmenu.IsOpen = false;
            //    contextmenu.UpdateLayout();
            //    contextmenu.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            //    contextmenu.IsOpen = true;

            //    double x = node.Right + 150 - contextmenu.RadiusX;
            //    double y = node.Bottom - node.Height / 2 - contextmenu.RadiusY;
            //    x = Math.Min(Math.Max(0, x), ActualWidth - contextmenu.RadiusX * 2);
            //    y = Math.Min(Math.Max(0, y), ActualHeight - contextmenu.RadiusY * 2);
            //    contextmenu.RenderTransform = new TranslateTransform() { X = x, Y = y };
            //    contextmenu.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            //}
            //else
            //{
            //    contextmenu.IsOpen = false;
            //}
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void SfRadialMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ClearSelection();
            //SfRadialMenuItem sf = (sender as SfRadialMenuItem);
        }

        private void HideRadialMenu()
        {
            radialMenu.Visibility = Visibility.Collapsed;
        }

        public void HideEditors()
        {
            {
                if (NodeContentEditor.Visibility != Visibility.Collapsed &&
                    ((NodeContentEditor.Resources["PropertyEditor_Storyboard_Collapse"] as Storyboard).GetCurrentState() == ClockState.Stopped ||
                    (NodeContentEditor.Resources["PropertyEditor_Storyboard_Collapse"] as Storyboard).GetCurrentState() == ClockState.Filling))
                {
                    (NodeContentEditor.Resources["PropertyEditor_Storyboard_Collapse"] as Storyboard).Begin();
                }

                if (NodeValueEditor.Visibility != Visibility.Collapsed &&
                   ((NodeValueEditor.Resources["PropertyEditor_Storyboard_Collapse"] as Storyboard).GetCurrentState() == ClockState.Stopped ||
                   (NodeValueEditor.Resources["PropertyEditor_Storyboard_Collapse"] as Storyboard).GetCurrentState() == ClockState.Filling))
                {
                    (NodeValueEditor.Resources["PropertyEditor_Storyboard_Collapse"] as Storyboard).Begin();
                }
            }
        }

        private void CreateStartNode(double p1, double p2, string p3, string label)
        {
            ProcessAutomationNode node = new ProcessAutomationNode()
            {
                Content = new NodeContent()
                {
                    DispalyText = label
                },
                NodeType = p3
            };

            if (label == "Task1")
            {
                (node.Content as NodeContent).IsTask = true;
                (node.Content as NodeContent).Properties.Add(new Property() { Name = "", Type = "String" });
            }
          
            node.IsSelected = true;
            ProcessAutomationViewModel.SelectedObject = node;
            Point position = sfdiagram.Page.TransformToVisual(sfdiagram.Page).TransformPoint(TapPosition);
            node.OffsetX = position.X;
            node.OffsetY = position.Y;
            if ((node.Content is NodeContent) && (node.Content as NodeContent).IsTask)
            {
                node.ContentTemplate = this.Resources["tasknodetemplate"] as DataTemplate;                
                OnOpen(null);
            }
            else
            {
                if (p3 == "Decision")
                {
                    OnOpen(null);
                    (node.Content as NodeContent).IsNotDecision = false;
                    node.UnitHeight = 50;
                    node.ContentTemplate = this.Resources["decisionnodetemplate"] as DataTemplate;
                    for (int i = 0; i <= 1; i++)
                    {
                        ProcessAutomationNode node1 = new ProcessAutomationNode()
                        {
                            UnitWidth = 35,
                            UnitHeight = 35,
                            ContentTemplate = this.Resources["nodetemplate"] as DataTemplate,
                        };


                        if (i == 0)
                        {
                            node1.OffsetX = node.OffsetX + 100;
                            node1.OffsetY = node.OffsetY;

                            node1.Content = new NodeContent()
                                {
                                    DispalyText = "Yes"
                                };
                        }
                        else if (i == 1)
                        {
                            node1.OffsetX = node.OffsetX;
                            node1.OffsetY = node.OffsetY + 100;

                            node1.Content = new NodeContent()
                                {
                                    DispalyText = "No"
                                };
                        }
                        node1.NodeContent = node1.Content as NodeContent;
                        (sfdiagram.Nodes as DiagramCollection).Add(node1);
                        ProcessAutomationConnector cvm = new ProcessAutomationConnector()
                        {
                            SourceNode = node,
                            TargetNode = node1
                        };
                        cvm.ConnectorGeometryStyle = SetDefaultStyle();
                        (sfdiagram.Connectors as DiagramCollection).Add(cvm);

                    }
                }
                else
                {
                    node.UnitWidth = p1;
                    node.UnitHeight = p2;
                    if (label == "Start")
                    {
                        node.ContentTemplate = this.Resources["Startnodetemplate"] as DataTemplate;
                        //ProcessAutomationViewModel.IsStartAdded = true;
                    }
                    else if (label == "End")
                    {
                        node.ContentTemplate = this.Resources["Endnodetemplate"] as DataTemplate;
                        //ProcessAutomationViewModel.IsEndAdded = true;
                    }
                    
                }
            }

            node.NodeContent = node.Content as NodeContent;
            (node.Content as NodeContent).Save = new DelegateCommand<object>(OnSave1, args => { return true; });
            (node.Content as NodeContent).OpenEditor = new DelegateCommand<object>(OnOpen, args => { return true; });
            (node.Content as NodeContent).Done = new DelegateCommand<object>(OnDone, args => { return true; });
            (sfdiagram.Nodes as DiagramCollection).Add(node);

            sfdiagram.Page.InvalidateMeasure();

            foreach(Node n in sfdiagram.Page.Children.OfType<Node>())
            {
                if(n.DataContext==node)
                {
                    n.Loaded += n_Loaded;
                }
            }
        }

        void n_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as Control).Loaded -= n_Loaded;
            Ellipse e1 = (sender as Node).FindName("start") as Ellipse;
        }

        private void OnDone(object obj)
        {
            HideEditors();
        }

        private void OnOpen(object obj)
        {
            NodeValueEditor.DataContext = null;
            NodeContentEditor.Visibility = Visibility.Collapsed;
            NodeValueEditor.DataContext = (ProcessAutomationViewModel.SelectedObject as ProcessAutomationNode).Content;
            NodeValueEditor.Visibility = Visibility.Visible;
            (NodeValueEditor.Resources["PropertyEditor_Storyboard_Visible"] as Storyboard).Begin();
        }

        private void OnSave1(object obj)
        {
            NodeContentEditor.DataContext = null;
            NodeValueEditor.Visibility = Visibility.Collapsed;
            NodeContentEditor.DataContext = (ProcessAutomationViewModel.SelectedObject as ProcessAutomationNode).Content;
            NodeContentEditor.Visibility = Visibility.Visible;
            (NodeContentEditor.Resources["PropertyEditor_Storyboard_Visible"] as Storyboard).Begin();
        }

        private void AddPort(ProcessAutomationNode node)
        {
            node.Ports = new ObservableCollection<INodePort>();

            //Getting the Number of Ports to be Added
            int count = 4;
            double angle = 0;
            //Calculate the Radius
            double radius = (node as ProcessAutomationNode).UnitWidth / 2;
            //Calculate the CenterPoint
            Point center = new Point((node as ProcessAutomationNode).UnitWidth / 2, (node as ProcessAutomationNode).UnitHeight / 2);
            //Calculating the Angle
            angle = 360 / count;
            for (int i = 1; i <= count; i++)
            {
                //Calculating the Points for Ports
                double Top = center.X + radius * Math.Cos((angle * Math.PI / 180) * i);
                double Left = center.Y + radius * Math.Sin((angle * Math.PI / 180) * i);
                //Adding the Ports to Node
                //ConnectionPort port1 = Addcport((o as Node), Top, Left, PortShapes.Circle, new SolidColorBrush(Colors.Black));
                NodePort n = AddcPort(node, Top, Left, new SolidColorBrush(Colors.Red));
                n.ShapeStyle = GetStyle();
                (node.Ports as ICollection<INodePort>).Add(n);
            }

        }

        private Style GetStyle()
        {
            Style s = new Style(typeof(Windows.UI.Xaml.Shapes.Path));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.WidthProperty, 12));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.HeightProperty, 12));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.FillProperty, new SolidColorBrush(Colors.Red)));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, new SolidColorBrush(Colors.Black)));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StretchProperty, Stretch.Fill));
            return s;
        }

        private NodePort AddcPort(ProcessAutomationNode node, double Top, double Left, SolidColorBrush solidColorBrush)
        {
            NodePort n = new NodePort()
            {
                NodeOffsetX = Left,
                NodeOffsetY = Top,
                Shape = new EllipseGeometry()
            };
            n.Node = node;
            return n;
        }

        private Style GetStyle(SolidColorBrush solidColorBrush2, SolidColorBrush solidColorBrush1)
        {
            Style s = new Style(typeof(Windows.UI.Xaml.Shapes.Path));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.FillProperty, solidColorBrush2));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, solidColorBrush1));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StretchProperty, Stretch.Fill));
            return s;
        }

        private void Property_Click_1(object sender, RoutedEventArgs e)
        {
            SfRadialMenuItem sf = (sender as SfRadialMenuItem);
            NodeContentEditor.DataContext = null;
            NodeValueEditor.DataContext = null;
            switch (sf.Name)
            {
                case "Property":
                    OnSave1(null);
                    break;

                case "Value":
                    OnOpen(null);
                    break;
            }
            HideContextMenu();
        }

        private void HideContextMenu()
        {
            //contextmenu.Visibility = Visibility.Collapsed;
        }
        
        private async void Refresh_Click_1(object sender, RoutedEventArgs e)
        {
            //if (EnsureUnsnapped())
            {
                StorageFile s;
                FileOpenPicker file = new FileOpenPicker();
                file.CommitButtonText = "Load";
                file.FileTypeFilter.Add(".xml");
                s = await file.PickSingleFileAsync();
                if (s != null)
                {
                    (sfdiagram.Nodes as DiagramCollection).Clear();
                    using (IRandomAccessStream readStream = await s.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        if (readStream != null)
                        {
                            Stream str = readStream.AsStream();
                            sfdiagram.Load(str);

                            foreach (ProcessAutomationNode node in ((IEnumerable<object>)sfdiagram.Nodes).OfType<ProcessAutomationNode>())
                            {
                                node.Content = node.NodeContent;
                                if ((node.Content is NodeContent) && (node.Content as NodeContent).IsTask)
                                {
                                    node.ContentTemplate = this.Resources["tasknodetemplate"] as DataTemplate;
                                }
                                else
                                {
                                    node.ContentTemplate = this.Resources["nodetemplate"] as DataTemplate;
                                }

                            }
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


        ObservableCollection<ProcessAutomationNode> nodescollection = null;
        ObservableCollection<Node> animationNodes = null;
        
        ObservableCollection<Node> PreviousNodes = new ObservableCollection<Node>();
        private void exe_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void OnPlay(object obj)
        {
            ProcessAutomationViewModel.IsPlay = false;
            outcheck = new ObservableCollection<ProcessAutomationConnector>();
            foreach (ProcessAutomationConnector con in ((IEnumerable<object>)sfdiagram.Connectors).OfType<ProcessAutomationConnector>())
            {
                con.ConnectorGeometryStyle = SetDefaultStyle();
                con.IsAnimationApplied = false;
            }

            ObservableCollection<Node> nodes = new ObservableCollection<Node>();

            foreach (Node n in ((IEnumerable<object>)sfdiagram.Nodes).OfType<Node>())
            {
                nodes.Add(n);
            }

            foreach (Node n1 in nodes)
            {
                (sfdiagram.Nodes as DiagramCollection).Remove(n1);
            }
            nodescollection = new ObservableCollection<ProcessAutomationNode>();
            animationNodes = new ObservableCollection<Node>();
            foreach (ProcessAutomationNode node1 in ((IEnumerable<object>)sfdiagram.Nodes).OfType<ProcessAutomationNode>())
            {
                node1.Constraints = node1.Constraints & ~NodeConstraints.Draggable;
                nodescollection.Add(node1);
            }

            foreach (ProcessAutomationNode aninode in nodescollection)
            {
                if ((aninode.Content as NodeContent).DispalyText == "Start")
                {
                    if ((aninode.Info as INodeInfo).OutConnectors != null)
                        ApplyAnimation(aninode);
                }
            }
        }

        Random r = new Random();
        private void ApplyAnimation(ProcessAutomationNode node)
        {
            sv = this.sfdiagram.ScrollSettings.ScrollInfo;
            if ((node.Info as INodeInfo).OutConnectors != null)
            {
                IConnector dec = null;
                if ((node.Info as INodeInfo).OutConnectors.Count() > 1)
                {
                    if (r.Next(0, 100) > 50)
                    {
                        //if (node.NodeType == "Decision")
                        //{
                           
                        //}
                        //else
                        //{
                            dec = ((node.Info as INodeInfo).OutConnectors.ToList())[0];
                        //}
                    }
                    else
                    {
                        dec = ((node.Info as INodeInfo).OutConnectors.ToList())[1];
                    }
                }


                foreach (ProcessAutomationConnector con in (node.Info as INodeInfo).OutConnectors)
                {
                    if (con.TargetNode != null)
                    {
                        ProcessAutomationNode checknode = (con.TargetNode as ProcessAutomationNode);
                        //if (!sfdiagram.Viewport.Contains(new Point(checknode.OffsetX , checknode.OffsetY )))
                        {
                            if (sv != null)
                            {
                                (sv as Syncfusion.UI.Xaml.Diagram.Controls.ScrollViewer).BringIntoViewport((checknode.Info as INodeInfo).Bounds);
                            }
                        }
                    }
                    Node animation = null;
                    if (node.NodeType == "Decision")
                    {
                        foreach (ProcessAutomationConnector c in (node.Info as INodeInfo).OutConnectors)
                        {
                            //if (((c.TargetNode as ProcessAutomationNode).Content as NodeContent).DispalyText == "New issue")
                            //{
                            //    dec = c;
                            //}
                            //else if (((c.TargetNode as ProcessAutomationNode).Content as NodeContent).DispalyText == "Yes")
                            //{
                            //    dec = c;
                            //}
                            //else
                            //{
                            //    c.UseforDecisionAnimation = true;
                            //}

                            if (!c.UseforDecisionAnimation)
                            {
                                dec = c;
                            }
                        }
                        if (con == dec)
                        {
                            if (!con.UseforDecisionAnimation)
                            {
                                animation = AddNode();
                            }
                        }
                    }
                    else
                    {
                        animation = AddNode();
                    }

                    if (animation != null)
                    {
                        animation.OffsetX = con.SourcePoint.X - (animation.UnitWidth / 2);
                        animation.OffsetY = con.SourcePoint.Y - (animation.UnitHeight / 2);
                        double xDiff = animation.OffsetX - (con.TargetPoint.X - (animation.UnitWidth / 2));
                        double yDiff = animation.OffsetY - (con.TargetPoint.Y - (animation.UnitHeight / 2));
                        double angle = Math.Atan2(yDiff, xDiff) * (180 / Math.PI);
                        (animation.RenderTransform as CompositeTransform).Rotation = angle - 180;
                        Storyboard s = new Storyboard();
                        s.Duration = new TimeSpan(0, 0, 0, 1);
                        DoubleAnimation da = new DoubleAnimation();
                        da.From = con.SourcePoint.X - (animation.UnitWidth / 2);
                        da.To = con.TargetPoint.X - (animation.UnitWidth / 2);
                        Storyboard.SetTarget(da, ((animation as Node).RenderTransform as CompositeTransform));
                        Storyboard.SetTargetProperty(da, "TranslateX");
                        DoubleAnimation da1 = new DoubleAnimation();
                        da1.From = con.SourcePoint.Y - (animation.UnitHeight / 2);
                        da1.To = con.TargetPoint.Y - (animation.UnitHeight / 2);
                        Storyboard.SetTarget(da1, ((animation as Node).RenderTransform as CompositeTransform));
                        Storyboard.SetTargetProperty(da1, "TranslateY");
                        s.Children.Add(da);
                        s.Children.Add(da1);
                        s.Begin();
                        con.ConnectorGeometryStyle = GetConStyle();
                        s.SpeedRatio = 0.25;
                        s.Completed += (sender, args) =>
                        {
                            if (sfdiagram!=null)
                            {
                                (sender as Storyboard).Stop();
                                con.ConnectorGeometryStyle = SetConStyle();
                                if ((sender as Storyboard).GetCurrentState() == ClockState.Stopped)
                                {
                                    con.IsAnimationApplied = true;
                                    (((IEnumerable<object>)sfdiagram.Nodes) as DiagramCollection).Remove(animation);
                                    ObservableCollection<ProcessAutomationConnector> check = new ObservableCollection<ProcessAutomationConnector>();
                                    if (con.TargetNode != null && ((con.TargetNode as ProcessAutomationNode).Info as INodeInfo).InConnectors != null && ((con.TargetNode as ProcessAutomationNode).Info as INodeInfo).InConnectors.Count() > 1)
                                    {
                                        //if (!con.IsAnimationApplied)
                                        //{
                                        //checknode = null;
                                        foreach (ProcessAutomationConnector v in
                                                ((con.TargetNode as ProcessAutomationNode).Info as INodeInfo).InConnectors)
                                        {
                                            if (v.SourceNode != null && ((v.SourceNode as ProcessAutomationNode).Content as NodeContent).DispalyText != "No")
                                            {
                                                checknode = new List<ProcessAutomationNode>();
                                                outcheck = new ObservableCollection<ProcessAutomationConnector>();
                                                if (CheckforCycleInConnectors(v.SourceNode as ProcessAutomationNode))
                                                {
                                                    check.Add(con);
                                                }
                                            }

                                        }
                                        //if (outcheck != null && outcheck.Count > 0)
                                        //    {
                                        //        foreach (ProcessAutomationConnector c in outcheck)
                                        //        {
                                        //            //c.IsAnimationApplied = true;
                                        //            check.Add(c);
                                        //        }
                                        //    }

                                        //}
                                        foreach (ProcessAutomationConnector checkline in ((con.TargetNode as ProcessAutomationNode).Info as INodeInfo).InConnectors)
                                        {
                                            if (checkline.IsAnimationApplied)
                                            {
                                                check.Add(checkline);
                                            }
                                            if (check.Count == ((con.TargetNode as ProcessAutomationNode).Info as INodeInfo).InConnectors.OfType<ProcessAutomationConnector>().Count())
                                            {
                                                if (checkline.IsAnimationApplied)
                                                    ApplyWaitingAnimation((con.TargetNode as ProcessAutomationNode));
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (con.IsAnimationApplied)
                                            ApplyWaitingAnimation((con.TargetNode as ProcessAutomationNode));
                                    }

                                } 
                            }

                        };
                    }
                }

                ObservableCollection<Node> watingnodes = new ObservableCollection<Node>();
                if ((node.Content as NodeContent).DispalyText == "End")
                {
                    foreach (Node n in ((IEnumerable<object>)sfdiagram.Nodes).OfType<Node>())
                    {
                        watingnodes.Remove(n);
                    }

                    foreach (Node n1 in watingnodes)
                    {
                        (sfdiagram.Nodes as DiagramCollection).Remove(n1);
                    }

                    foreach (ProcessAutomationNode con in ((IEnumerable<object>)sfdiagram.Nodes).OfType<ProcessAutomationNode>())
                    {
                        con.Constraints = con.Constraints | NodeConstraints.Draggable;
                    }

                    ProcessAutomationViewModel.IsPlay = true;
                }

            }
        }
        List<ProcessAutomationNode> checknode = null;
        ObservableCollection<ProcessAutomationConnector> outcheck = null;
        private bool CheckforCycleInConnectors(ProcessAutomationNode processAutomationNode)
        {
            if (processAutomationNode != null)
            {
                checknode.Add(processAutomationNode);
                if (((processAutomationNode).Info as INodeInfo).InConnectors != null)
                {
                    foreach (ProcessAutomationConnector con in ((processAutomationNode).Info as INodeInfo).InConnectors)
                    {
                        if (checknode.Contains(con.SourceNode as ProcessAutomationNode))
                        {
                            return true;
                        }
                        else
                        {
                            if (CheckforCycleInConnectors((con.SourceNode as ProcessAutomationNode)))
                            {
                                return true;
                            }
                        }
                    }
                    checknode.Remove(processAutomationNode);
                }
            }
            return false;
        }
        private async  void ApplyWaitingAnimation(ProcessAutomationNode processAutomationNode)
        {
            foreach (Node n in sfdiagram.Page.Children.OfType<Node>())
            {
                if (processAutomationNode != null)
                {
                    if (n.DataContext == processAutomationNode)
                    {
                        Node node = new Node();
                         node = AddWatingNode();
                        node.OffsetX = processAutomationNode.OffsetX + (processAutomationNode.Info as INodeInfo).Bounds.Width / 2;
                        node.OffsetY = processAutomationNode.OffsetY - ((processAutomationNode.Info as INodeInfo).Bounds.Height / 2) - 20;
                        Storyboard sb = new Storyboard();
                        //DoubleAnimationUsingKeyFrames da = new DoubleAnimationUsingKeyFrames();
                        //EasingDoubleKeyFrame e = new EasingDoubleKeyFrame()
                        //{
                        //    KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 8)),
                        //    Value = 0
                        //};

                        //EasingDoubleKeyFrame e1 = new EasingDoubleKeyFrame()
                        //{
                        //    KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 2, 6)),
                        //    Value = 1
                        //};
                        //da.KeyFrames.Add(e);
                        //da.KeyFrames.Add(e1);
                        //Storyboard.SetTarget(da, n);
                        //Storyboard.SetTargetProperty(da, "Opacity");
                        //ObjectAnimationUsingKeyFrames oa = new ObjectAnimationUsingKeyFrames();
                        //DiscreteObjectKeyFrame dof = new DiscreteObjectKeyFrame()
                        //{
                        //    KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0)),
                        //    Value = Visibility.Visible
                        //};
                        //oa.KeyFrames.Add(dof);
                        //Storyboard.SetTarget(oa, n);
                        //Storyboard.SetTargetProperty(oa, "Visibility");
                        //sb.Children.Add(da);
                        //sb.Children.Add(oa);

                        ColorAnimationUsingKeyFrames ca = new ColorAnimationUsingKeyFrames();
                        EasingColorKeyFrame ca1 = new EasingColorKeyFrame()
                        {
                            KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0)),
                            Value = new SolidColorBrush(Colors.DeepSkyBlue).Color
                        };

                        EasingColorKeyFrame ca2 = new EasingColorKeyFrame()
                        {
                            KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 1, 7)),
                            Value = ColorConverter("#FF5CE02A").Color
                        };
                        EasingColorKeyFrame ca3 = new EasingColorKeyFrame()
                           {
                               KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 2, 7)),
                               Value = new SolidColorBrush(Colors.DeepSkyBlue).Color
                           };

                        ca.KeyFrames.Add(ca1);
                        ca.KeyFrames.Add(ca2);
                        ca.KeyFrames.Add(ca3);
                        sb.Children.Add(ca);

                        //Storyboard.SetTarget(ca, n);
                        //Storyboard.SetTargetProperty(ca, "(Node.FillBrushoo).(SolidColorBrush.Color)");
                        //node
                        //foreach (var elements in VisualTreeHelper.FindElementsInHostCoordinates(new Point(processAutomationNode.OffsetX + (processAutomationNode.Info as INodeInfo).Bounds.Width / 2, processAutomationNode.OffsetY), n))
                        //{
                        //    if(elements is Ellipse)
                        //    {
                        //        if((elements as Ellipse).Name=="start")
                        //        {
                        //            Storyboard.SetTarget(ca, (elements as Ellipse));
                        //            Storyboard.SetTargetProperty(ca, "(Ellipse.Fill).(SolidColorBrush.Color)");
                        //        }
                        //        else if((elements as Ellipse).Name=="end")
                        //        {
                        //            Storyboard.SetTarget(ca, (elements as Ellipse));
                        //            Storyboard.SetTargetProperty(ca, "(Ellipse.Fill).(SolidColorBrush.Color)");
                        //        }
                        //        else if ((elements as Ellipse).Name == "node")
                        //        {
                        //            Storyboard.SetTarget(ca, (elements as Ellipse));
                        //            Storyboard.SetTargetProperty(ca, "(Ellipse.Fill).(SolidColorBrush.Color)");
                        //        }
                        //    }
                        //    else if(elements is Rectangle)
                        //    {
                        //        if((elements as Rectangle).Name=="task")
                        //        {
                        //            Storyboard.SetTarget(ca, (elements as Rectangle));
                        //            Storyboard.SetTargetProperty(ca, "(Rectangle.Fill).(SolidColorBrush.Color)");
                        //        }
                        //    }
                        //    else if(elements is Windows.UI.Xaml.Shapes.Path)
                        //    {
                        //        if ((elements as Windows.UI.Xaml.Shapes.Path).Name == "decision")
                        //        {
                        //            Storyboard.SetTarget(ca, (elements as Windows.UI.Xaml.Shapes.Path));
                        //            Storyboard.SetTargetProperty(ca, "(Shapes.Fill).(SolidColorBrush.Color)");
                        //        }
                        //    }
                        //}
                        //Rectangle b = n.FindDescendantByName("task") as Rectangle;

                        //if (b != null)
                        //{
                        //    Storyboard.SetTarget(ca, b);
                        //    Storyboard.SetTargetProperty(ca, "(Rectangle.Fill).(SolidColorBrush.Color)");
                        //}
                        //else
                        //{
                        //    Ellipse ell = n.FindDescendantByName("start") as Ellipse;
                        //    if (ell != null)
                        //    {

                        //    }
                        //    else
                        //    {
                        //        Ellipse e1 = n.FindDescendantByName("end") as Ellipse;
                        //        if (e1 != null)
                        //        {
                        //            Storyboard.SetTarget(ca, e1);
                        //            Storyboard.SetTargetProperty(ca, "(Ellipse.Fill).(SolidColorBrush.Color)");
                        //        }
                        //        else
                        //        {
                        //            Ellipse e2 = n.FindDescendantByName("node") as Ellipse;

                        //            if (e2 != null)
                        //            {
                        //                Storyboard.SetTarget(ca, e2);
                        //                Storyboard.SetTargetProperty(ca, "(Ellipse.Fill).(SolidColorBrush.Color)");
                        //            }
                        //            else
                        //            {
                        //                Windows.UI.Xaml.Shapes.Path path = n.FindDescendantByName("decision") as Windows.UI.Xaml.Shapes.Path;
                        //                if (path != null)
                        //                {
                        //                    Storyboard.SetTarget(ca, path);
                        //                    Storyboard.SetTargetProperty(ca, "(Shapes.Fill).(SolidColorBrush.Color)");
                        //                }

                        //            }
                        //        }

                        //    }
                        //}


                        //sb.SkipToFill();
                        //sb.Begin();
                        //sb.Completed += (sender, args) =>
                        //{

                        //    (sender as Storyboard).Stop();
                        //    if ((sender as Storyboard).GetCurrentState() == ClockState.Stopped)
                        //    {
                        //        node.Content = null;
                        //        node.Content = CreatePath(node);
                        //        ApplyAnimation(processAutomationNode);
                        //    }
                        //};
                        if (this.DataContext != null)
                        {
                           await WaitAnimation(processAutomationNode, node);
                        }
                    }
                }
            }
        }

        private async System.Threading.Tasks.Task WaitAnimation(ProcessAutomationNode processAutomationNode,Node node)
        {        
        
                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 0, 4, 4));
                    node.Content = null;
                    node.Content = CreatePath(node);
                    ApplyAnimation(processAutomationNode);
        }

        private object CreatePath(Node node)
        {
            Windows.UI.Xaml.Shapes.Path path = new Windows.UI.Xaml.Shapes.Path();
            node.Shape = Shapes.Ellipse.ToGeometry();
            node.ShapeStyle = GetNodeStyle2();
            node.Constraints = NodeConstraints.None;
            path.Style = ApplyStyle();
            path.Stretch = Stretch.Fill;
            path.MaxHeight = 10;
            path.MaxWidth = 10;
            path.Width = 10;
            path.Height = 15;
            return path;
        }


        private Style ApplyStyle()
        {
            Style s = new Style(typeof(Windows.UI.Xaml.Shapes.Path));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, new SolidColorBrush(Colors.White)));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeThicknessProperty, 2d));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.DataProperty, "M1088.92,-800.625 L1099.34,-789.7 L1121,-815"));
            //s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StretchProperty, Stretch.Fill));
            return s;
        }


        private Node AddWatingNode()
        {
            Node node = new Node()
            {
                UnitWidth = 20,
                UnitHeight = 20                //Shape = Shapes.Ellipse.ToGeometry()

            };
            SfBusyIndicator busyIndicator = new SfBusyIndicator();
            busyIndicator.ViewboxWidth = 18;
            busyIndicator.ViewboxHeight = 18;
            //busyIndicator.Foreground = new SolidColorBrush(Colors.White);
            busyIndicator.AnimationType = AnimationTypes.Rotation;
            node.Content = busyIndicator;
            //node.ShapeStyle = GetNodeStyle2();
            (sfdiagram.Nodes as DiagramCollection).Add(node);
            return node;
        }

        private Style GetNodeStyle2()
        {
            Style s = new Style(typeof(Windows.UI.Xaml.Shapes.Path));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.FillProperty, ColorConverter("#FF7AE03F")));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StretchProperty, Stretch.Fill));
            return s;
        }


        //ProcessAutomationNode Previous = null;
        //private void ApplyAnimation(ProcessAutomationNode connode)
        //{
        //    if ((connode.Content as NodeContent).DispalyText != "Start")
        //    {
        //        if ((connode.Info as INodeInfo).OutConnectors != null && (connode.Info as INodeInfo).OutConnectors.Count() > 1)
        //        {
        //            if (((connode.Info as INodeInfo).OutConnectors.FirstOrDefault().TargetNode) != null)
        //            {
        //                ((connode.Info as INodeInfo).OutConnectors.FirstOrDefault().TargetNode as ProcessAutomationNode).AnimationNode = connode.AnimationNode;

        //            }
        //            foreach (ProcessAutomationConnector con in (connode.Info as INodeInfo).OutConnectors)
        //            {
        //                if ((con.SourceNode as ProcessAutomationNode) == connode)
        //                {
        //                    if ((con.TargetNode as ProcessAutomationNode).AnimationNode == null)
        //                    {
        //                        Node node = AddNode();
        //                        (con.TargetNode as ProcessAutomationNode).AnimationNode = node;
        //                        animationNodes.Add(node);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (connode.NodeType != "Decision" && (connode.Info as INodeInfo).OutConnectors != null)
        //    {
        //        foreach (IConnector con in (connode.Info as INodeInfo).OutConnectors)
        //        {
        //            //if ((connode.Content as NodeContent).DispalyText != "Start")
        //            //{
        //            //    if (((con.TargetNode as ProcessAutomationNode).Info as INodeInfo).InConnectors != null && ((con.TargetNode as ProcessAutomationNode).Info as INodeInfo).InConnectors.Count() > 1)
        //            //    {
        //            //        animation = ((connode.Info as INodeInfo).InConnectors.FirstOrDefault().SourceNode as ProcessAutomationNode).AnimationNode;

        //            //    }
        //            //    else
        //            //    {
        //            //        animation = (con.SourceNode as ProcessAutomationNode).AnimationNode;
        //            //    }
        //            //}

        //            if ((con.TargetNode as ProcessAutomationNode) != null && (con.TargetNode as ProcessAutomationNode).AnimationNode != null)
        //            {
        //                animation = (con.TargetNode as ProcessAutomationNode).AnimationNode;
        //            }
        //            else if ((con.SourceNode as ProcessAutomationNode).AnimationNode != null)
        //            {
        //                animation = (con.SourceNode as ProcessAutomationNode).AnimationNode;
        //                if (con.TargetNode != null && ((con.TargetNode as ProcessAutomationNode).Content as NodeContent).DispalyText != "End")
        //                {

        //                    (con.TargetNode as ProcessAutomationNode).AnimationNode = animation;
        //                }
        //            }

        //            if (animation != null)
        //            {
        //                animation.OffsetX = (con as ProcessAutomationConnector).SourcePoint.X - animation.Width;
        //                animation.OffsetY = (con as ProcessAutomationConnector).SourcePoint.Y - animation.Height;

        //                double xDiff = animation.OffsetX - (con.TargetPoint.X - animation.Width);
        //                double yDiff = animation.OffsetY - (con.TargetPoint.Y - animation.Height);
        //                double angle = Math.Atan2(yDiff, xDiff) * (180 / Math.PI);
        //                (animation.RenderTransform as CompositeTransform).Rotation = angle-180;
        //                Storyboard s = new Storyboard();
        //                s.Duration = new TimeSpan(0, 0, 0, 1);
        //                DoubleAnimation da = new DoubleAnimation();
        //                da.From = con.SourcePoint.X-(animation.Width/2);
        //                da.To = con.TargetPoint.X - (animation.Width / 2);
        //                Storyboard.SetTarget(da, ((animation as Node).RenderTransform as CompositeTransform));
        //                Storyboard.SetTargetProperty(da, "TranslateX");

        //                DoubleAnimation da1 = new DoubleAnimation();
        //                da1.From = con.SourcePoint.Y - (animation.Height/2);
        //                da1.To = con.TargetPoint.Y - (animation.Height/2);
        //                Storyboard.SetTarget(da1, ((animation as Node).RenderTransform as CompositeTransform));
        //                Storyboard.SetTargetProperty(da1, "TranslateY");
        //                s.Children.Add(da);
        //                s.Children.Add(da1);
        //                //s.SkipToFill();
        //                s.Begin();
        //                //s.FillBehavior = FillBehavior.Stop;
        //                con.ConnectorGeometryStyle = GetConStyle();
        //                s.SpeedRatio = 0.25;

        //                s.Completed += (sender, e) =>
        //                {
        //                    (sender as Storyboard).Stop();
        //                    con.ConnectorGeometryStyle = SetConStyle();
        //                    if ((sender as Storyboard).GetCurrentState() == ClockState.Stopped)
        //                    {
        //                        if ((con.TargetNode as ProcessAutomationNode).AnimationNode != null)
        //                        {
        //                            if (((con.TargetNode as ProcessAutomationNode).Content as NodeContent).DispalyText != "End")
        //                            {
        //                                (con.TargetNode as ProcessAutomationNode).AnimationNode.Visibility = Visibility.Collapsed;
        //                            }
        //                            else
        //                            {
        //                                (con.SourceNode as ProcessAutomationNode).AnimationNode.Visibility = Visibility.Collapsed;
        //                            }
        //                        }
        //                        if (((con.TargetNode as ProcessAutomationNode).Content as NodeContent).DispalyText == "End")
        //                        {
        //                            (con.SourceNode as ProcessAutomationNode).AnimationNode.Visibility = Visibility.Collapsed;
        //                            PreviousNodes.Add((con.SourceNode as ProcessAutomationNode).AnimationNode);
        //                        }
        //                        ApplySecondAnimation((con.TargetNode as ProcessAutomationNode));
        //                    }
        //                };
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ProcessAutomationConnector conv = (connode.Info as INodeInfo).OutConnectors.FirstOrDefault() as ProcessAutomationConnector;
        //        if ((conv.TargetNode != null) && (conv.TargetNode as ProcessAutomationNode).AnimationNode != null)
        //        {
        //            animation = (conv.TargetNode as ProcessAutomationNode).AnimationNode;

        //        }
        //        else
        //        {
        //            if ((conv.SourceNode != null))
        //            {
        //                animation = (conv.SourceNode as ProcessAutomationNode).AnimationNode;
        //            }
        //        }
        //        if (animation != null)
        //        {
        //            animation.OffsetX = (conv as ProcessAutomationConnector).SourcePoint.X - animation.Width;
        //            animation.OffsetY = (conv as ProcessAutomationConnector).SourcePoint.Y - animation.Height;
        //            Storyboard s = new Storyboard();
        //            s.Duration = new TimeSpan(0, 0, 0, 1);
        //            DoubleAnimation da = new DoubleAnimation();
        //            da.From = conv.SourcePoint.X;
        //            da.To = conv.TargetPoint.X;
        //            Storyboard.SetTarget(da, ((animation as Node).RenderTransform as CompositeTransform));
        //            Storyboard.SetTargetProperty(da, "TranslateX");

        //            DoubleAnimation da1 = new DoubleAnimation();
        //            da1.From = conv.SourcePoint.Y - animation.Width;
        //            da1.To = conv.TargetPoint.Y - animation.Height;
        //            Storyboard.SetTarget(da1, ((animation as Node).RenderTransform as CompositeTransform));
        //            Storyboard.SetTargetProperty(da1, "TranslateY");
        //            s.Children.Add(da);
        //            s.Children.Add(da1);
        //            //s.SkipToFill();
        //            s.Begin();
        //            //s.FillBehavior = FillBehavior.Stop;
        //            conv.ConnectorGeometryStyle = GetConStyle();
        //            s.SpeedRatio = 0.25;

        //            s.Completed += (sender, e) =>
        //            {
        //                (sender as Storyboard).Stop();
        //                conv.ConnectorGeometryStyle = SetConStyle();
        //                if ((sender as Storyboard).GetCurrentState() == ClockState.Stopped)
        //                {
        //                    if ((conv.TargetNode as ProcessAutomationNode).AnimationNode != null)
        //                    {
        //                        if (((conv.TargetNode as ProcessAutomationNode).Content as NodeContent).DispalyText != "End")
        //                        {
        //                            (conv.TargetNode as ProcessAutomationNode).AnimationNode.Visibility = Visibility.Collapsed;
        //                        }
        //                        else
        //                        {
        //                            (conv.SourceNode as ProcessAutomationNode).AnimationNode.Visibility = Visibility.Collapsed;
        //                        }
        //                    }
        //                    if (((conv.TargetNode as ProcessAutomationNode).Content as NodeContent).DispalyText == "End")
        //                    {
        //                        (conv.SourceNode as ProcessAutomationNode).AnimationNode.Visibility = Visibility.Collapsed;
        //                        PreviousNodes.Add((conv.SourceNode as ProcessAutomationNode).AnimationNode);
        //                    }
        //                    ApplySecondAnimation((conv.TargetNode as ProcessAutomationNode));
        //                }
        //            };
        //        }
        //    }
        //}

        //private void ApplySecondAnimation(ProcessAutomationNode node)
        //{
        //    foreach (Node n in sfdiagram.Page.Children.OfType<Node>())
        //    {
        //        if (n.DataContext == node)
        //        {
        //            Storyboard sb = new Storyboard();
        //            DoubleAnimationUsingKeyFrames da = new DoubleAnimationUsingKeyFrames();
        //            EasingDoubleKeyFrame e = new EasingDoubleKeyFrame()
        //            {
        //                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 8)),
        //                Value = 0
        //            };

        //            EasingDoubleKeyFrame e1 = new EasingDoubleKeyFrame()
        //            {
        //                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 2, 6)),
        //                Value = 1
        //            };
        //            da.KeyFrames.Add(e);
        //            da.KeyFrames.Add(e1);
        //            Storyboard.SetTarget(da, n);
        //            Storyboard.SetTargetProperty(da, "Opacity");
        //            ObjectAnimationUsingKeyFrames oa = new ObjectAnimationUsingKeyFrames();
        //            DiscreteObjectKeyFrame dof = new DiscreteObjectKeyFrame()
        //            {
        //                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0)),
        //                Value = Visibility.Visible
        //            };
        //            oa.KeyFrames.Add(dof);
        //            Storyboard.SetTarget(oa, n);
        //            Storyboard.SetTargetProperty(oa, "Visibility");
        //            sb.Children.Add(da);
        //            sb.Children.Add(oa);
        //            //sb.SkipToFill();
        //            sb.Begin();
        //            sb.Completed += (sender, args) =>
        //            {
        //                if (node.AnimationNode != null)
        //                {
        //                    node.AnimationNode.Visibility = Visibility.Visible;
        //                }

        //                if ((node.Content as NodeContent).DispalyText == "End")
        //                {
        //                    foreach (Node no in PreviousNodes)
        //                    {
        //                        no.Visibility = Visibility.Collapsed;
        //                    }
        //                }
        //                (sender as Storyboard).Stop();
        //                ApplyAnimation(node);

        //            };
        //        }
        //    }

        //}

        private void CreateAnimationNodes(ProcessAutomationNode con)
        {
            foreach (IConnector connector in (con.Info as INodeInfo).OutConnectors)
            {
                Node node = AddNode();
                //(connector.SourceNode as ProcessAutomationNode).AnimationNode = node;
                (connector.TargetNode as ProcessAutomationNode).AnimationNode = node;
                animationNodes.Add(node);
            }
        }

        private Windows.UI.Xaml.Style GetConStyle()
        {
            Windows.UI.Xaml.Style s = new Windows.UI.Xaml.Style(typeof(Windows.UI.Xaml.Shapes.Path));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, ColorConverter("#FEDAE324")));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeThicknessProperty, 1d));
            return s;
        }

        private Windows.UI.Xaml.Style SetDefaultStyle()
        {
            Windows.UI.Xaml.Style s = new Windows.UI.Xaml.Style(typeof(Windows.UI.Xaml.Shapes.Path));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, new SolidColorBrush(Colors.Black)));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeThicknessProperty, 1d));
            return s;
        }

        private Windows.UI.Xaml.Style SetConStyle()
        {
            Windows.UI.Xaml.Style s = new Windows.UI.Xaml.Style(typeof(Windows.UI.Xaml.Shapes.Path));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, ColorConverter("#FF1DBB32")));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeThicknessProperty, 1d));
            return s;
        }

        private Node AddNode()
        {
            Node node = new Node()
            {
                UnitWidth = 10,
                UnitHeight = 10,
                Shape = Shapes.Ellipse.ToGeometry()

            };

            node.RenderTransform = new CompositeTransform();
            //enode.RenderTransform as CompositeTransform)
            node.RenderTransformOrigin = new Point(0.5, 0.5);
            node.ZIndex = 0;
            node.ShapeStyle = GetNodeStyle();
            (sfdiagram.Nodes as DiagramCollection).Add(node);
            return node;
        }
        
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClearSelection();
            Button sf = (sender as Button);
            switch (sf.Name)
            {
                case "Starts":
                    CreateStartNode(35, 35, "Ellipse", "Start");
                    break;
                case "Tasks":
                    CreateStartNode(50, 50, "Rectangle", "Task1");
                    break;
                case "Ends":
                    CreateStartNode(35, 35, "Ellipse", "End");
                    break;
                case "Decisions":
                    CreateStartNode(75, 75, "Decision", "Decision");
                    break;
            }
            HideRadialMenu();
        }

        public bool IsShowRadialMenu;
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (((sender as Button).Parent as Viewbox).Parent is SelectorItem)
            {
                IsShowRadialMenu = false;
            }

            OnSave1(null);

        }

        private void Grid_PointerEntered_1(object sender, PointerRoutedEventArgs e)
        {
            (sfdiagram.SelectedItems as CustomSelector).fillbrush = new SolidColorBrush(Colors.LightGray);
        }

        private void Grid_PointerExited_1(object sender, PointerRoutedEventArgs e)
        {
            (sfdiagram.SelectedItems as CustomSelector).fillbrush = new SolidColorBrush(Colors.White);
        }

        private void Grid_PointerPressed_1(object sender, PointerRoutedEventArgs e)
        {
            (sfdiagram.SelectedItems as CustomSelector).fillbrush = new SolidColorBrush(Colors.Black);
        }

        private void Grid_PointerReleased_1(object sender, PointerRoutedEventArgs e)
        {
            (sfdiagram.SelectedItems as CustomSelector).fillbrush = new SolidColorBrush(Colors.White);
        }
        
        private void GetScrollViewer(DependencyObject sfd)
        {
            DependencyObject d = sfd;

         //   sv = (sfd as AutomationDiagram).FindDescendantByName("Part_ScrollViwer") as Syncfusion.UI.Xaml.Diagram.Controls.ScrollViewer;
            //int childindex = 0;
            //if (d is Grid)
            //{
            //    childindex = 1;
            //}
            //DependencyObject val = VisualTreeHelper.GetChild(d, childindex);
            //if (val.GetType() != typeof(ScrollViewer))
            //{
            //    GetScrollViewer(val);
            //}
            //else
            //{
            //    sv = (val as ScrollViewer);
            //}
        }

        private void Grid_Unloaded_1(object sender, RoutedEventArgs e)
        {
            Grid send = sender as Grid;
            send.Unloaded -= Grid_Unloaded_1;
            send.PointerEntered -= Grid_PointerEntered_1;
            send.PointerExited -= Grid_PointerExited_1;
            send.PointerPressed -= Grid_PointerPressed_1;
            send.PointerReleased -= Grid_PointerReleased_1;
        }

        private void Button_Unload_3(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (but != null)
            {
                but.Unloaded -= Button_Unload_3;
                but.Click -= Button_Click_3;
            }
        }
        
        //private void CreateAnimationNodes(ProcessAutomationNode con)
        //{
        //    foreach (IConnector connector in (con.Info as INodeInfo).OutConnectors)
        //    {
        //        Node node = new Node()
        //        {
        //            Width = 10,
        //            Height = 10,
        //            Shape = new EllipseGeometry() { RadiusX = 10, RadiusY = 10 }

        //        };
        //        node.ZIndex = 0;
        //        node.ShapeStyle = GetNodeStyle();
        //        node.Content = new TextBlock()
        //        {
        //            Text = "syncfusion",
        //            Foreground = new SolidColorBrush(Colors.Red)
        //        };
        //        (sfdiagram.Nodes as DiagramCollection).Add(node);
        //        //(connector.SourceNode as ProcessAutomationNode).AnimationNode = node;
        //        (connector.TargetNode as ProcessAutomationNode).AnimationNode = node;
        //        animationNodes.Add(node);
        //    }
        //}
    }

    public static class Extension
    {
        public static T GetParent<T>(this DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = null;
            try
            {
                if (child.Dispatcher != null)
                {
                    parent = (child as FrameworkElement).Parent ?? VisualTreeHelper.GetParent(child);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            if (parent is T)
            {
                return parent as T;
            }
            else if (parent == null)
            {
                return null;
            }
            else
            {
                return parent.GetParent<T>();
            }
        }

        public static T FindChild<T>(this DependencyObject parent) where T : DependencyObject
        {
            if (parent == null)
            {
                return null;
            }

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (!(child is T))
                {
                    foundChild = FindChild<T>(child);

                    if (foundChild != null)
                        break;
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }
    }

    public static class ColorHelper
    {
        public static SolidColorBrush GetColorFromHexa(string hexaColor)
        {
            return new SolidColorBrush(
                Color.FromArgb(
                    255, Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16)
                )
            );
        }
    }

    public class SelectorItem : ContentControl
    {
        public SelectorItem()
        {
            this.Tapped += SelectorItem_Tapped;
            this.PointerPressed += SelectorItem_PointerPressed;
            this.PointerMoved += SelectorItem_PointerMoved;
            this.PointerReleased += SelectorItem_PointerReleased;
            this.Unloaded += SelectorItem_Unloaded;
        }

        void SelectorItem_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Tapped -= SelectorItem_Tapped;
            this.PointerPressed -= SelectorItem_PointerPressed;
            this.PointerMoved -= SelectorItem_PointerMoved;
            this.PointerReleased -= SelectorItem_PointerReleased;
            this.Unloaded -= SelectorItem_Unloaded;
        }

        private bool isPressed = false;

        void SelectorItem_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            isPressed = false;
        }

        void SelectorItem_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            CustomSelector selectorViewModel = this.DataContext as CustomSelector;
            if (isPressed)
            {
                //DrawParam param = new DrawParam(e, selectorViewModel.Nodes.FirstOrDefault(),(selectorViewModel.Nodes.FirstOrDefault() as ProcessAutomationNode).Ports.FirstOrDefault());
                DrawParam param = new DrawParam(e, ((IEnumerable<object>)selectorViewModel.Nodes).FirstOrDefault());
                if (this.Command != null)
                {
                    this.Command.Execute(param);

                }
                isPressed = false;
            }
        }

        void SelectorItem_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            isPressed = true;
        }

        public bool DragCommand { get; set; }

        void SelectorItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Command != null)
            {
                Command.Execute(null);
            }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(SelectorItem), new PropertyMetadata(null));


    }

    public static class FrameworkElementExtensions
    {
        public static FrameworkElement FindDescendantByName(this FrameworkElement element, string name)
        {
            if (element == null || string.IsNullOrWhiteSpace(name)) { return null; }

            if (name.Equals(element.Name, StringComparison.OrdinalIgnoreCase))
            {
                return element;
            }
            var childCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childCount; i++)
            {
                var result = (VisualTreeHelper.GetChild(element, i) as FrameworkElement).FindDescendantByName(name);
                if (result != null) { return result; }
            }
            return null;
        }
    }

    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                object obj = (object)value;
                if (obj.GetType() == typeof(ProcessAutomationConnector))
                {
                    return Visibility.Collapsed;
                }
                else if (obj.GetType() == typeof(ProcessAutomationNode))
                {
                    if ((obj as ProcessAutomationNode).Content != null)
                    {
                        if (((obj as ProcessAutomationNode).Content as NodeContent).DispalyText == "Yes" || ((obj as ProcessAutomationNode).Content as NodeContent).DispalyText == "No" || ((obj as ProcessAutomationNode).Content as NodeContent).DispalyText == "Start" || ((obj as ProcessAutomationNode).Content as NodeContent).DispalyText == "End")
                        {
                            return Visibility.Collapsed;
                        }
                    }
                    return Visibility.Visible;
                }
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class VisibilityConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                object obj = (object)value;
                if (obj.GetType() == typeof(ProcessAutomationConnector))
                {
                    return Visibility.Collapsed;
                }
                else if (obj.GetType() == typeof(ProcessAutomationNode))
                {
                    return Visibility.Visible;
                }
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
