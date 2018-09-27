using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Syncfusion.UI.Xaml.Grid;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Animation;
using Syncfusion.UI.Xaml.Controls.Navigation;
using Windows.UI;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.Devices.Input;
using Windows.System;
using System.Threading;
using ScrollViewer = Syncfusion.UI.Xaml.Diagram.Controls.ScrollViewer;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Syncfusion.SampleBrowser.UWP.Diagram;
using System.Xml;
using Windows.Storage;
using Windows.Storage.Streams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OrganizationalChartDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrgDiagram : Page
    {
        private IGraphInfo graphInfo;
        public OrgDiagram()
        {
            this.InitializeComponent();
           
            graphInfo = sfdiagram.Info as IGraphInfo;
            UpdateEmployee();
            graphInfo.ItemTappedEvent += sfdiagram_ItemTappedEvent;
            sfdiagram.Loaded += sfdiagram_Loaded;
            graphInfo.ItemTappedEvent += graphInfo_ItemTappedEvent;
            sfdiagram.SFSelector.Style = this.Resources["CustomSelector"] as Style;
            sfdiagram.PointerMoved += sfdiagram_PointerMoved;
            sfdiagram.PageSettings = null;
            this.Loaded += OrgDiagram_Loaded;
            this.Unloaded += OrgDiagram_Unloaded;
            sfdiagram.Menu = null;


            sfdiagram.Constraints = sfdiagram.Constraints & ~GraphConstraints.Selectable;
            sfdiagram.PropertyChanged += Sfdiagram_PropertyChanged;
        }

        private void Sfdiagram_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName== "LayoutManager")
            {
                if (sfdiagram.LayoutManager != null)
                {
                    sfdiagram.LayoutManager.RefreshFrequency = RefreshFrequency.FirstLoad;
                }
            }
        }

        private async void UpdateEmployee()
        {
            bool load = await UpdateEmployee1();

        }

        private async Task<bool> UpdateEmployee1()
        {
            StorageFolder mindmapfile = await ApplicationData.Current.RoamingFolder.CreateFolderAsync("Layout", CreationCollisionOption.OpenIfExists);
            IReadOnlyList<StorageFile> files = await mindmapfile.GetFilesAsync();
            string[] predefinedDiagrams = new string[] { "Employee.xml" };
            if (files.Where(file => file.FileType == ".xml").Count() == 0)
            {
                foreach (string file in predefinedDiagrams)
                {
                    String ResourceReference = "ms-appx:///Showcase/Organizational Layout/" + file;
                    StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(new Uri(ResourceReference, UriKind.Absolute));
                    await f.CopyAsync(mindmapfile, f.Name);
                }
                files = await mindmapfile.GetFilesAsync();
            }
            return true;
        }

        void graphInfo_ItemTappedEvent(object sender, DiagramEventArgs args)
        {
            this.isSingleTap = false;
            if (args.Item is OrgNodeViewModel)
            {
                ChartViewModel.SelectedObject = (args.Item as OrgNodeViewModel);
                if (((args.Item as OrgNodeViewModel).Content as Employee).IsExpand == State.Expand)
                {
                    ((args.Item as OrgNodeViewModel).Content as Employee).IsExpand = State.Collapse;
                    (args.Item as OrgNodeViewModel).IsExpanded = false;
                    Hide((args.Item as OrgNodeViewModel));

                }
                else if (((args.Item as OrgNodeViewModel).Content as Employee).IsExpand == State.Collapse)
                {
                    ((args.Item as OrgNodeViewModel).Content as Employee).IsExpand = State.Expand;
                    (args.Item as OrgNodeViewModel).IsExpanded = true;
                    Show((args.Item as OrgNodeViewModel));
                }
                UpdateLayout((args.Item as OrgNodeViewModel));

                CheckSearch((sender as OrgChartDiagram));
            }
            TimeSpan tapsDelay =
               DateTime.Now - this.singleTapTime;
        }

        void OrgDiagram_Unloaded(object sender, RoutedEventArgs e)
        {
            sfdiagram.LayoutManager = null;
            //sfdiagram.ViewDictionary = null;
            this.Unloaded -= OrgDiagram_Unloaded;
            ChartViewModel.Dispose();
            foreach (IConnector connector in (IEnumerable<object>)sfdiagram.Connectors)
            {
                connector.SourceNode = null;
                connector.TargetNode = null;
            }

            foreach (FrameworkElement child in sfdiagram.Page.Children)
            {
                if (child is Node)
                {
                    OrgNodeViewModel vm = child.DataContext as OrgNodeViewModel;
                    if (vm != null)
                    {
                        vm.CustomToolTip = null;
                    }
                    child.Holding -= Node_Holding_1;
                    child.PointerEntered -= Node_PointerEntered_1;
                    child.PointerExited -= Node_PointerExited_1;
                    child.PointerPressed -= Node_PointerPressed_1;
                }
                child.DataContext = null;
            }

            sfdiagram.Nodes = null;
            sfdiagram.Connectors = null;

            SelectorViewModel selec = sfdiagram.SelectedItems as SelectorViewModel;
            if (selec != null)
            {
                selec.Connectors = null;
                selec.Nodes = null;
                selec.Groups = null;
            }
            sfdiagram.DataContext = null;
            sfdiagram.SFSelector = null;
            sfdiagram.SelectedItems = null;
            this.DataContext = null;
            DiagramCommand.SetCommand(sfdiagram, null);

            this.Loaded -= OrgDiagram_Loaded;
            this.Unloaded -= OrgDiagram_Unloaded;
            if (sfdiagram != null)
            {
                graphInfo.ItemTappedEvent -= sfdiagram_ItemTappedEvent;
                sfdiagram.Loaded -= sfdiagram_Loaded;
                sfdiagram.PointerMoved -= sfdiagram_PointerMoved;
            }
        }

        void OrgDiagram_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OrgDiagram_Loaded;
            foreach (FrameworkElement child in sfdiagram.Page.Children)
            {
                if (child is Node && !(child is Syncfusion.UI.Xaml.Diagram.Selector))
                {
                    child.Holding += Node_Holding_1;
                    child.PointerEntered += Node_PointerEntered_1;
                    child.PointerExited += Node_PointerExited_1;
                    child.PointerPressed += Node_PointerPressed_1;

                    Binding vis = new Binding();
                    vis.Path = new PropertyPath("Visible");
                    vis.Mode = BindingMode.TwoWay;
                    vis.Converter = new BoolToVisibiltyConverter();
                    child.SetBinding(Node.VisibilityProperty, vis);
                }
                else if (child is Connector)
                {
                    Binding bin = new Binding();
                    bin.Path = new PropertyPath("Visible");
                    bin.Mode = BindingMode.TwoWay;
                    bin.Converter = new BoolToVisibiltyConverter();
                    child.SetBinding(Connector.VisibilityProperty, bin);


                    bin = new Binding();
                    bin.Path = new PropertyPath("ConnectorOpacity");
                    child.SetBinding(Connector.OpacityProperty, bin);
                }
            }

          
        }

        void OrgDiagram_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                if (e.NewItems[0] is OrgNodeViewModel)
                {
                    ChartViewModel.NodeCollection.Add(e.NewItems[0] as OrgNodeViewModel);
                }
            }
        }

        #region Events

        System.DateTime entertime;

        #region ToolTip
        Node ToolTipnode { get; set; }
        private void Node_PointerEntered_1(object sender, PointerRoutedEventArgs e)
        {
            entertime = DateTime.Now;
            if ((((sender as Node).DataContext as OrgNodeViewModel).Content as Employee).IsFocus != NodeFocusState.Focused)
            {
                (((sender as Node).DataContext as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.MouseHover;
            }
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Touch)
            {
                return;
            }
            foreach (OrgNodeViewModel n in ((IEnumerable<object>)sfdiagram.Nodes).OfType<OrgNodeViewModel>())
            {
                if (n != (sender as Node).DataContext as OrgNodeViewModel)
                {
                    n.CustomToolTip.IsOpen = false;
                }
            }

            ShowToolTip((sender as Node));
            //          CancellationTokenSource source = new CancellationTokenSource();
            //source.CancelAfter(TimeSpan.FromSeconds(2));
            //this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            //{
            //    Task t = Task.Run(() => ShowToolTip((sender as Node)), source.Token);
            //}).AsTask().Wai;


            //ToolTipnode = (sender as Node);
            //Task t = DisplayToolTip((sender as Node));

        }
        
        private void Node_PointerExited_1(object sender, PointerRoutedEventArgs e)
        {
            if (entertime.Subtract(System.DateTime.Now).Milliseconds < 200)
            {
                ToolTipnode = null;
            }
            if ((((sender as Node).DataContext as OrgNodeViewModel).CustomToolTip as Popup) != null)
            {
                (((sender as Node).DataContext as OrgNodeViewModel).CustomToolTip as Popup).IsOpen = false;
            }
            if ((((sender as Node).DataContext as OrgNodeViewModel).Content as Employee).IsFocus != NodeFocusState.Focused)
            {
                (((sender as Node).DataContext as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Normal;
            }
        }

        private void Node_PointerPressed_1(object sender, PointerRoutedEventArgs e)
        {
            if ((((sender as Node).DataContext as OrgNodeViewModel).CustomToolTip as Popup) != null)
            {
                (((sender as Node).DataContext as OrgNodeViewModel).CustomToolTip as Popup).IsOpen = false;
            }
        }

   
        private void ShowToolTip(Node node)
        {
            Storyboard t = new Storyboard();
            DoubleAnimationUsingKeyFrames da1 = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame e11 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0)),
                Value = 0
            };
            EasingDoubleKeyFrame e21 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 600)),
                Value = 0
            };

            EasingDoubleKeyFrame e31 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 1, 100)),
                Value = 1,
                EasingFunction = new CircleEase()
            };

            t.Children.Add(da1);
            da1.KeyFrames.Add(e11);
            da1.KeyFrames.Add(e21);
            da1.KeyFrames.Add(e31);
            Storyboard.SetTarget(da1, ((node.DataContext as OrgNodeViewModel).CustomToolTip as Popup));
            Storyboard.SetTargetProperty(da1, "Opacity");
            t.Begin();

            if (((node.DataContext as OrgNodeViewModel).CustomToolTip as Popup).IsOpen == false)
            {
                ((node.DataContext as OrgNodeViewModel).CustomToolTip as Popup).Opacity = 0.85;
                ContentControl cc = new ContentControl();
                cc.Width = 230;
                cc.Height = 130;
                cc.Content = ((node.DataContext as OrgNodeViewModel).Content as Employee);
                cc.ContentTemplate = this.Resources["tooltip"] as DataTemplate;
                ((node.DataContext as OrgNodeViewModel).CustomToolTip as Popup).Child = cc;
                Rect n = ((node.DataContext as OrgNodeViewModel).Info as INodeInfo).Bounds;
                n = sfdiagram.Page.TransformToVisual(this).TransformBounds(n);
                //Point point = sfdiagram.Page.TransformToVisual(this).TransformPoint(new Point((node.DataContext as OrgNodeViewModel).OffsetX + (node.Width / 2), (node.DataContext as OrgNodeViewModel).OffsetY ));
                // Point point = sfdiagram.Page.TransformToVisual(this).TransformPoint(new Point((node.DataContext as OrgNodeViewModel).OffsetX + (node.Width / 2), (node.DataContext as OrgNodeViewModel).OffsetY - (node.Height / 2)));
                ((node.DataContext as OrgNodeViewModel).CustomToolTip as Popup).HorizontalOffset = n.Right;
                ((node.DataContext as OrgNodeViewModel).CustomToolTip as Popup).VerticalOffset = n.Top + (cc.Height / 1.5);
                ((node.DataContext as OrgNodeViewModel).CustomToolTip as Popup).IsOpen = true;


            }

            foreach (OrgNodeViewModel org in ((IEnumerable<object>)sfdiagram.Nodes).OfType<OrgNodeViewModel>())
            {
                if (org != node.DataContext)
                {
                    if ((org.CustomToolTip as Popup).IsOpen)
                    {
                        ((node.DataContext as OrgNodeViewModel).CustomToolTip as Popup).IsOpen = false;
                    }
                }
            }
        }

        private void Node_RightTapped_1(object sender, RightTappedRoutedEventArgs e)
        {
            foreach (var item in sfdiagram.Page.Children.OfType<INode>())
            {
                if (item.GetType() != typeof(Syncfusion.UI.Xaml.Diagram.Selector))
                {
                    if (((item as Node).DataContext as OrgNodeViewModel) != (sender as Node).DataContext)
                    {
                        (item as Node).Opacity = 1;
                    }
                }
            }

            ((sender as Node).DataContext as OrgNodeViewModel).Constraints = ((sender as Node).DataContext as OrgNodeViewModel).Constraints | NodeConstraints.AllowPan;
        }

        void sfdiagram_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            onUpdate();
        }
        #endregion

        private void Node_Holding_1(object sender, HoldingRoutedEventArgs e)
        {
            ShowToolTip((sender as Node));
        }


       
        private const int DOUBLETAP_DELAY_MILLIS = 700;

        private DateTime singleTapTime { get; set; }
        private bool isSingleTap
        {
            get { return true; }
            set { isSingleTap = value; }
        }
        
        void child_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        void child_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            e.Handled = true;
        }

          object PreviousSelectedItem = null;
        void sfdiagram_ItemTappedEvent(object sender, DiagramEventArgs args)
        {
            if (args.Item is INode)
            {
                if ((args.Item as OrgNodeViewModel).Content is Employee)
                {

                    if (PreviousSelectedItem != null)
                    {
                        ((PreviousSelectedItem as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Normal;
                    }
                    ChartViewModel.SelectedObject = (args.Item as OrgNodeViewModel);
                    ((ChartViewModel.SelectedObject as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Focused;
                    PreviousSelectedItem = ChartViewModel.SelectedObject;
                    ((args.Item as OrgNodeViewModel).Content as Employee).PathClickCommand = new DelegateCommand<object>(OnPress, argus => { return true; });
                    ((args.Item as OrgNodeViewModel).Content as Employee).RatingCommand = new DelegateCommand<object>(OnRatingChanged, argus => { return true; });
                }
            }
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

        private void OnPress(object obj)
        {
            if (PreviousSelectedItem != null)
            {
                //if (ChartViewModel.SelectedObject != (PreviousSelectedItem as OrgNodeViewModel))
                {
                    ((PreviousSelectedItem as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Normal;
                }
            }

            if (obj != null && obj is Windows.UI.Xaml.Shapes.Path)
            {
                Employee dc = (Employee)(obj as Windows.UI.Xaml.Shapes.Path).DataContext;

                foreach (Node n in sfdiagram.Page.Children.OfType<Node>())
                {
                    if (((n.DataContext as OrgNodeViewModel).Content as Employee) == dc)
                    {
                        ChartViewModel.SelectedObject = (n as Node).DataContext as OrgNodeViewModel;
                        ((ChartViewModel.SelectedObject as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Focused;


                        PreviousSelectedItem = ChartViewModel.SelectedObject;

                    }
                }
            }
            if (ChartViewModel.SelectedObject != null && ChartViewModel.SelectedObject is OrgNodeViewModel)
            {
                if ((ChartViewModel.SelectedObject as OrgNodeViewModel).Content is Employee)
                {
                    if (((ChartViewModel.SelectedObject as OrgNodeViewModel).Content as Employee).IsExpand == State.Expand)
                        // && ((ChartViewModel.SelectedObject as OrgNodeViewModel).Content as Employee).IsFocus == NodeFocusState.Focused)
                    {
                        ((ChartViewModel.SelectedObject as OrgNodeViewModel).Content as Employee).IsExpand = State.Collapse;
                        (ChartViewModel.SelectedObject as OrgNodeViewModel).IsExpanded = false;
                        Hide((ChartViewModel.SelectedObject as OrgNodeViewModel));
                    }
                    else if (((ChartViewModel.SelectedObject as OrgNodeViewModel).Content as Employee).IsExpand == State.Collapse)
                        //&& ((ChartViewModel.SelectedObject as OrgNodeViewModel).Content as Employee).IsFocus == NodeFocusState.Focused)
                    {
                        ((ChartViewModel.SelectedObject as OrgNodeViewModel).Content as Employee).IsExpand = State.Expand;
                        (ChartViewModel.SelectedObject as OrgNodeViewModel).IsExpanded = true;
                        Show((ChartViewModel.SelectedObject as OrgNodeViewModel));
                    }
                    UpdateLayout(ChartViewModel.SelectedObject as OrgNodeViewModel);
                    CheckSearch((sfdiagram as OrgChartDiagram));
                }
            }
        }

        void sfdiagram_Loaded(object sender, RoutedEventArgs e)
        {
            GetScrollViewer((sender as SfDiagram));
          
        }
                
        private bool GetScrollViewer(DependencyObject sfd)
        {
            if (sfd is ScrollViewer)
            {
                (this.DataContext as ChartViewModel).ScrollViewer = (sfd as ScrollViewer);
                return true;
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(sfd); i++)
            {
                if (GetScrollViewer(VisualTreeHelper.GetChild(sfd, i)))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        public ChartViewModel ChartViewModel
        {
            get { return (ChartViewModel)GetValue(ChartViewModelProperty); }
            set { SetValue(ChartViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChartViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartViewModelProperty =
            DependencyProperty.Register("ChartViewModel", typeof(ChartViewModel), typeof(OrgDiagram), new PropertyMetadata(null, OnPropertyChangedCallBack));

        private static void OnPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OrgDiagram diagram = d as OrgDiagram;

            ChartViewModel chartvm = e.NewValue as ChartViewModel;
            diagram.UpdateCollection();
            chartvm.Previous = new DelegateCommand<object>(diagram.OnPrevious, args => { return true; });
            chartvm.Close = new DelegateCommand<object>(diagram.OnClose, args => { return true; });
            chartvm.Next = new DelegateCommand<object>(diagram.OnNext, args => { return true; });
            chartvm.Search = new DelegateCommand<object>(diagram.OnSearch, args => { return true; });
        }

        #region Commands

         private async void UpdateCollection()
        {
          
          bool load= await LoadEmployeeFile();
             if (load)
             {
                 sfdiagram.LayoutManager = new Syncfusion.UI.Xaml.Diagram.Layout.LayoutManager()
                 {
                     Layout = new DirectedTreeLayout()
                     {
                         Type = Syncfusion.UI.Xaml.Diagram.Layout.LayoutType.Organization,
                         HorizontalSpacing = 25,
                         VerticalSpacing = 35,
                         SpaceBetweenSubTrees = 20

                     }
                 };
                 (sfdiagram.Info as IGraphInfo).GetLayoutInfo += OrgDiagram_GetLayoutInfo;
             }

        }

        private async Task<bool> LoadEmployeeFile()
        {
            sfdiagram.Nodes = new DiagramCollection<OrgNodeViewModel>();
            sfdiagram.Connectors = new DiagramCollection<OrgConnectorViewModel>();
            ObservableCollection<Employee> employee = new ObservableCollection<Employee>();
            (sfdiagram.Nodes as DiagramCollection<OrgNodeViewModel>).CollectionChanged += OrgDiagram_CollectionChanged;
            DataSourceSettings settings = new DataSourceSettings();
            settings.ParentId = "ReportingPerson";
            settings.Id = "Name";
            StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
            StorageFolder st = await installedLocation.GetFolderAsync("Layout");
            StorageFile s = await st.GetFileAsync("Employee" + ".xml");
            XDocument loadedData = null;
            using (IRandomAccessStream readStream = await s.OpenAsync(FileAccessMode.ReadWrite))
            {
                if (readStream != null)
                {
                    Stream str = readStream.AsStream();
                    loadedData = XDocument.Load(str);
                }
            }


            foreach (var item in loadedData.Descendants("Employee"))
            {
                employee.Add(new Employee()
                {
                    Name = item.Attribute("Name").Value,
                    Destination = item.Attribute("Destination").Value,
                    Doj = item.Attribute("Doj").Value,
                    ImageUrl = "ms-appx://" + item.Attribute("ImageUrl").Value,
                    RatingColor = item.Attribute("RatingColor").Value,
                    Salary = Convert.ToDouble(item.Attribute("Salary").Value),
                    IsExpand = returnValue(item.Attribute("IsExpand").Value),
                    ReportingPerson = IsCheck(item.Attribute("ReportingPerson"))

                });
            }

            settings.DataSource = employee;
            sfdiagram.DataSourceSettings = settings;

            return true;
        }
        void OrgDiagram_GetLayoutInfo(object sender, LayoutInfoArgs args)
        {
            bool _mchildren = false;
            if ((args.Item.Info as INodeInfo).OutConnectors.Count() > 0)
            {
                foreach (var item in (args.Item.Info as INodeInfo).OutNeighbors)
                {
                    if ((item.Info as INodeInfo).OutConnectors.Count() > 0)
                    {
                        _mchildren = false;
                        break;
                    }
                    else
                        _mchildren = true;
                }
            }

            if (_mchildren)
                args.Type = ChartType.Right;
            else
                args.Type = ChartType.Alternate;


        }
        private string IsCheck(XAttribute xAttribute)
        {
            if(xAttribute==null)
            {
                return string.Empty;
            }

            return xAttribute.Value;
        }


        private State returnValue(string p)
        {
            foreach (State s in Enum.GetValues(typeof(State)))
            {
                if (s.ToString() == p)
                {
                    return s;
                }
            }
            return State.None;
        }


        private Style GetStyle(SolidColorBrush fill)
        {
            Style s = new Style(typeof(Windows.UI.Xaml.Shapes.Path));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.FillProperty, fill));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StretchProperty, Stretch.Fill));
            return s;
        }
        private void OnClose(object obj)
        {
            CheckSearcheItem();
            ChartViewModel.Searchview = new SearchViewModel();
            this.GetParent<OrganizationalChartDemo.MainPage>().BottomAppBar.IsSticky = false;
            this.GetParent<OrganizationalChartDemo.MainPage>().BottomAppBar.IsOpen = false;
            searchgrid.Visibility = Visibility.Collapsed;
            ChartViewModel.SearchVisibility = Visibility.Collapsed;
           
        }

        private void OnSearch(object obj)
        {
            if (ChartViewModel.SelectedObject != null)
            {
                ((ChartViewModel.SelectedObject as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Normal;
            }
            searchgrid.Visibility = Visibility.Visible;
            ChartViewModel.Searchview = new SearchViewModel();
            Searchcombo.ClearValue(ComboBox.SelectedIndexProperty);
            textbox.Text = "";
            ChartViewModel.SearchVisibility = Visibility.Visible;
            foreach (OrgNodeViewModel org in ((IEnumerable<object>)sfdiagram.Nodes).OfType<OrgNodeViewModel>())
            {
                if ((org.Content as Employee).IsFocus == NodeFocusState.Focused)
                {
                    (org.Content as Employee).IsFocus = NodeFocusState.Normal;
                    ((org as OrgNodeViewModel).Content as Employee).IsSearched = false;
                }
            }

        }

        int click = -1;
        object CurrentItem = null;
        object PreviousItem = null;

        Stack<OrgNodeViewModel> previousstack = null;
        IEnumerator<OrgNodeViewModel> previous = null;
        private void OnNext(object obj)
        {
            if (obj.ToString() == "Next")
            {
                click += 1;
                if (ChartViewModel.Item == null)
                {
                    if (CurrentItem != null)
                    {
                        ((CurrentItem as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Normal;
                    }
                    if (PreviousItem != null)
                    {
                        ((PreviousItem as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Normal;

                    }
                    ChartViewModel.StartSearch();
                    ChartViewModel.Item = ChartViewModel.Searchview.SearchResult.GetEnumerator();
                    MoveNext();
                    previousstack = new Stack<OrgNodeViewModel>();
                }
                else
                {
                    PreviousItem = CurrentItem;
                    if (PreviousItem != null)
                    {
                        ((PreviousItem as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Normal;
                    }

                    if (PreviousItem != null && previousstack != null)
                    {
                        ((PreviousItem as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Normal;
                        if (!previousstack.Contains((PreviousItem as OrgNodeViewModel)))
                        {
                            (previousstack as Stack<OrgNodeViewModel>).Push((PreviousItem as OrgNodeViewModel));
                        }
                    }
                    MoveNext();
                    if (CurrentItem == null)
                    {
                        ChartViewModel.Item.Reset();
                        MoveNext();
                    }
                }
            }
            else if (obj.ToString() == "Previous")
            {
                if (previousstack == null)
                {
                    previousstack = new Stack<OrgNodeViewModel>();
                    foreach (var item in ChartViewModel.Searchview.SearchResult)
                    {
                        previousstack.Push(item as OrgNodeViewModel);
                    }
                }
                if (previousstack != null && previousstack.Count > 0)
                {
                    if (CurrentItem != null)
                    {
                        ((CurrentItem as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Normal;
                    }
                    if (previous == null)
                    {
                        previous = previousstack.GetEnumerator();
                        MovePrevious();
                    }
                    else
                    {
                        PreviousItem = previous.Current;
                        if (previous.Current != null)
                        {
                            if (previousstack.Last<OrgNodeViewModel>() == previous.Current)
                            {
                                ((PreviousItem as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Normal;
                                previous.Reset();
                            }
                            else
                            {
                                ((PreviousItem as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Normal;
                            }

                        }
                        else
                        {
                            previous.Reset();
                        }
                        MovePrevious();
                    }
                }

            }
        }

        private void MoveNext()
        {
            ChartViewModel.Item.MoveNext();
            CurrentItem = ChartViewModel.Item.Current;
            if (CurrentItem != null)
            {
                ((CurrentItem as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Focused;
                (ChartViewModel.ScrollViewer as ScrollViewer).BringIntoCenter(((CurrentItem as OrgNodeViewModel).Info as INodeInfo).Bounds);
            }
        }

        private void MovePrevious()
        {
            previous.MoveNext();
            OrgNodeViewModel org = previous.Current;
            if (org != null)
            {
                ((org as OrgNodeViewModel).Content as Employee).IsSearched = true;
                ((org as OrgNodeViewModel).Content as Employee).IsFocus = NodeFocusState.Focused;
                (ChartViewModel.ScrollViewer as ScrollViewer).BringIntoCenter(((org as OrgNodeViewModel).Info as INodeInfo).Bounds);
            }
        }

        private void CheckSearcheItem()
        {
            foreach (var o in (ChartViewModel.Searchview as SearchViewModel).SearchResult)
            {
                if ((o.Content as Employee).IsSearched)
                {
                    (o.Content as Employee).IsSearched = false;
                    (o.Content as Employee).IsFocus = NodeFocusState.Normal;
                }
            }
        }

        private void OnPrevious(object obj)
        {
            if ((ChartViewModel.Searchview as SearchViewModel).SearchResult.Count > 0)
            {
                if (ChartViewModel.ScrollViewer != null)
                {
                    if (click < 0 || click >= (ChartViewModel.Searchview as SearchViewModel).SearchResult.Count - 1)
                    {
                        click = 0;
                    }
                    else
                    {
                        
                    }
                    CheckSearcheItem();
                    if (click >= 0)
                    {
                        OrgNodeViewModel org = ((ChartViewModel.Searchview as SearchViewModel).SearchResult[click] as OrgNodeViewModel);
                        (ChartViewModel.ScrollViewer as ScrollViewer).BringIntoCenter((org.Info as INodeInfo).Bounds);
                        (org.Content as Employee).IsSearched = true;
                    }
                }
            }
        }

   
        private void OnRatingChanged(object obj)
        {
            if (ChartViewModel.SelectedObject != null)
            {

            }
        }
        private void onUpdate()
        {
            (this.ChartViewModel.ScrollViewer as ScrollViewer).MaxZoom = 2;
            (this.ChartViewModel.ScrollViewer as ScrollViewer).MinZoom = 0.4;
            if ((this.ChartViewModel.ScrollViewer as ScrollViewer).CurrentZoom >= 0.8)
            {
                foreach (INode org in sfdiagram.Page.Children.OfType<INode>())
                {
                    org.ContentTemplate = this.Resources["employeeContentTemplate"] as DataTemplate;
                }
            }
            else if ((this.ChartViewModel.ScrollViewer as ScrollViewer).CurrentZoom < 0.8)
            {
                foreach (INode org in sfdiagram.Page.Children.OfType<INode>())
                {
                    org.ContentTemplate = this.Resources["ZoomOutContentTemplate"] as DataTemplate;
                }
            }
        }
        #endregion
           
        #region Helper Functions

        //Expands the Child Nodes
        private void Show(OrgNodeViewModel n)
        {
            if ((n.Content as Employee).IsExpand == State.Expand)
            {
                foreach (OrgConnectorViewModel con in (n.Info as INodeInfo).OutConnectors)
                {
                   foreach (Connector line in sfdiagram.Page.Children.OfType<Connector>())
                   {
                        if (line.DataContext == con)
                        {
                            ExpandAnimation(line);
                        }
                    }
                    con.Visible = true;

                    foreach (Node n1 in sfdiagram.Page.Children.OfType<Node>())
                    {
                        if ((con.TargetNode as OrgNodeViewModel) == n1.DataContext)
                        {
                            ExpandAnimation(n1);
                        }
                    }
                    (con.TargetNode as OrgNodeViewModel).Visible = true;
                    Show((con.TargetNode as OrgNodeViewModel));
                }
               
            }
        }

        //Collapses the Child Nodes
        private void Hide(OrgNodeViewModel n)
        {
            foreach (OrgNodeViewModel node in sfdiagram.Page.Children.OfType<Connector>()
                                  .Where(connector => (connector.SourceNode as OrgNodeViewModel) == n)
                                  .Select(connector => (connector.TargetNode as OrgNodeViewModel)))
            {
                foreach (Connector line in sfdiagram.Page.Children.OfType<Connector>()
                                  .Where(connector => connector.TargetNode == node || connector.SourceNode == node))
                {

                    (line.DataContext as OrgConnectorViewModel).Visible = false;
                    CollapseAnimation(line);
                }

                foreach (Node v in sfdiagram.Page.Children.OfType<Node>())
                {
                    if (v.DataContext == node)
                    {
                        CollapseAnimation(v);
                        (v.DataContext as OrgNodeViewModel).Visible = false;
                    }
                }
                Hide(node);
            }
        }
        //Fade in animation 
        private void ExpandAnimation(DependencyObject obj)
        {
            Duration duration = new Duration(TimeSpan.FromMilliseconds(300));
            DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();
            myDoubleAnimation1.Duration = duration;
            Storyboard sb = new Storyboard();
            sb.Duration = duration;
            sb.Children.Add(myDoubleAnimation1);
            Storyboard.SetTarget(myDoubleAnimation1, obj);
            Storyboard.SetTargetProperty(myDoubleAnimation1, "Opacity");
            myDoubleAnimation1.From = 0d;
            myDoubleAnimation1.To = 1d;
            sb.Begin();
        }
        //Fade in animation 
        void ExpandAnimation1(DependencyObject obj)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimationUsingKeyFrames rotation = new DoubleAnimationUsingKeyFrames();
            double rotate = ((obj as Node).RenderTransform as CompositeTransform).Rotation;
            EasingDoubleKeyFrame ed = new EasingDoubleKeyFrame()
            {

                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0)),
                Value = 0,
                EasingFunction = new BackEase()
                {
                    EasingMode = EasingMode.EaseIn
                }
            };

            EasingDoubleKeyFrame ed1 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 300)),
                Value = 0.556,
                EasingFunction = new BackEase()
                {
                    Amplitude = 2
                }
            };

            EasingDoubleKeyFrame ed2 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 600)),
                Value = 0,
                EasingFunction = new BackEase()
                {
                    Amplitude = 2
                }
            };

            rotation.KeyFrames.Add(ed);
            rotation.KeyFrames.Add(ed1);
            rotation.KeyFrames.Add(ed2);

            DoubleAnimationUsingKeyFrames scalex = new DoubleAnimationUsingKeyFrames();
            double scale = ((obj as Node).RenderTransform as CompositeTransform).ScaleX;
            EasingDoubleKeyFrame scalex1 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0)),
                Value = 1,
                EasingFunction = new BackEase()
                {
                    EasingMode = EasingMode.EaseIn
                }
            };

            EasingDoubleKeyFrame scalex2 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 300)),
                Value = 0.999,
                EasingFunction = new BackEase()
                {
                    Amplitude = 2
                }
            };

            EasingDoubleKeyFrame scalex3 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 600)),
                Value = 0.996,
                EasingFunction = new BackEase()
                {
                    Amplitude = 2
                }
            };


            scalex.KeyFrames.Add(scalex1);
            scalex.KeyFrames.Add(scalex2);
            scalex.KeyFrames.Add(scalex3);

            DoubleAnimationUsingKeyFrames scaley = new DoubleAnimationUsingKeyFrames();

            EasingDoubleKeyFrame scaley1 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0)),
                Value = 1,
                EasingFunction = new BackEase()
                {
                    EasingMode = EasingMode.EaseIn
                }
            };

            EasingDoubleKeyFrame scaley2 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 300)),
                Value = 0.999,
                EasingFunction = new BackEase()
                {
                    Amplitude = 2
                }
            };

            EasingDoubleKeyFrame scaley3 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 600)),
                Value = 0.996,
                EasingFunction = new BackEase()
                {
                    Amplitude = 2
                }
            };

            scaley.KeyFrames.Add(scaley1);
            scaley.KeyFrames.Add(scaley2);
            scaley.KeyFrames.Add(scaley3);

            DoubleAnimationUsingKeyFrames translateX = new DoubleAnimationUsingKeyFrames();
            double x = ((obj as Node).RenderTransform as CompositeTransform).TranslateX;
            EasingDoubleKeyFrame translateX1 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0)),
                Value = x,
                EasingFunction = new BackEase()
                {
                    EasingMode = EasingMode.EaseIn
                }
            };

            EasingDoubleKeyFrame translateX2 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 300)),
                Value = x - 0.096,
                EasingFunction = new BackEase()
                {
                    Amplitude = 2
                }
            };

            EasingDoubleKeyFrame translateX3 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 600)),
                Value = x - 0.5,
                EasingFunction = new BackEase()
                {
                    Amplitude = 2
                }
            };
            translateX.KeyFrames.Add(translateX1);
            translateX.KeyFrames.Add(translateX2);
            translateX.KeyFrames.Add(translateX3);

            DoubleAnimationUsingKeyFrames translateY = new DoubleAnimationUsingKeyFrames();
            double y = ((obj as Node).RenderTransform as CompositeTransform).TranslateY;
            EasingDoubleKeyFrame translateY1 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0)),
                Value = y,
                EasingFunction = new BackEase()
                {
                    EasingMode = EasingMode.EaseIn
                }
            };

            EasingDoubleKeyFrame translateY2 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 300)),
                Value = y - 0.067,
                EasingFunction = new BackEase()
                {
                    Amplitude = 2
                }
            };

            EasingDoubleKeyFrame translateY3 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 600)),
                Value = y + 0.246,
                EasingFunction = new BackEase()
                {
                    Amplitude = 2
                }
            };
            translateY.KeyFrames.Add(translateY1);
            translateY.KeyFrames.Add(translateY2);
            translateY.KeyFrames.Add(translateY3);


            Storyboard.SetTarget(rotation, ((obj as Node).RenderTransform as CompositeTransform));
            Storyboard.SetTargetProperty(rotation, "Rotation");

            Storyboard.SetTarget(scalex, ((obj as Node).RenderTransform as CompositeTransform));
            Storyboard.SetTargetProperty(scalex, "ScaleX");

            Storyboard.SetTarget(scaley, ((obj as Node).RenderTransform as CompositeTransform));
            Storyboard.SetTargetProperty(scaley, "ScaleY");

            Storyboard.SetTarget(translateX, ((obj as Node).RenderTransform as CompositeTransform));
            Storyboard.SetTargetProperty(translateX, "TranslateX");


            Storyboard.SetTarget(translateY, ((obj as Node).RenderTransform as CompositeTransform));
            Storyboard.SetTargetProperty(translateY, "TranslateY");

            sb.Children.Add(rotation);
            sb.Children.Add(scalex);
            sb.Children.Add(scaley);
            sb.Children.Add(translateX);
            sb.Children.Add(translateY);
            sb.Begin();
            sb.RepeatBehavior = RepeatBehavior.Forever;
        }

        //Fade out animation
        void CollapseAnimation(DependencyObject obj)
        {
            Duration duration = new Duration(TimeSpan.FromMilliseconds(300));
            // Create two DoubleAnimations and set their properties.
            DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();
            myDoubleAnimation1.Duration = duration;
            Storyboard sb = new Storyboard();
            sb.Duration = duration;
            sb.Children.Add(myDoubleAnimation1);
            Storyboard.SetTarget(myDoubleAnimation1, obj);
            Storyboard.SetTargetProperty(myDoubleAnimation1, "Opacity");
            myDoubleAnimation1.From = 1d;
            myDoubleAnimation1.To = 0d;
            sb.Begin();
        }

        private void CheckSearch(OrgChartDiagram org)
        {
            if ((org.DataContext as ChartViewModel).SearchVisibility == Visibility.Visible)
            {
                (org.DataContext as ChartViewModel).Item = null;
            }
            else if ((org.DataContext as ChartViewModel).SearchVisibility == Visibility.Visible)
            {
                (org.DataContext as ChartViewModel).Item = null;
            }
        }

        private void UpdateLayout(INode node)
        {
            Node fixednode = null;            
            foreach (Node n in sfdiagram.Page.Children.OfType<Node>())
            {
                if (n.GetType() != typeof(Syncfusion.UI.Xaml.Diagram.Selector))
                {
                    if (n.DataContext == node)
                    {
                        fixednode = n;
                    }
                }
            }

            sfdiagram.LayoutManager.Layout.UpdateLayout(node);

            foreach (Node n in sfdiagram.Page.Children.OfType<Node>())
            {
                if (n.GetType() != typeof(Syncfusion.UI.Xaml.Diagram.Selector))
                {
                    if (n.DataContext != node)
                    {
                        foreach (OrgNodeViewModel o in sfdiagram.Page.Children.OfType<Connector>()
                                  .Where(connector => connector.SourceNode == node)
                                  .Select(connector => connector.TargetNode))
                        {
                            if (n.DataContext != o)
                            {
                                ApplyMovement(n);
                            }
                        }
                    }
                }
            }

            ApplyAnimation(node as OrgNodeViewModel);
        }


        private void ApplyMovement(Node source)
        {
            Storyboard sb1 = new Storyboard();
            RepositionThemeAnimation rt = new RepositionThemeAnimation();
            rt.SpeedRatio = 0.80;
            Point offx = new Point(((source.DataContext as OrgNodeViewModel).OffsetX - source.UnitWidth / 2), ((source.DataContext as OrgNodeViewModel).OffsetY + (source.DataContext as OrgNodeViewModel).UnitHeight / 2));
            rt.FromHorizontalOffset = -sfdiagram.Page.TransformToVisual(source).TransformPoint(offx).X;
            sb1.Children.Add(rt);
            Storyboard.SetTarget(rt, source);
            sb1.Begin();
        }

        //Expands the Child Nodes
        private void ApplyAnimation(OrgNodeViewModel n)
        {
            Connector con = null;
            if ((n.Content as Employee).IsExpand == State.Expand)
            {
                foreach (OrgConnectorViewModel con1 in (n.Info as INodeInfo).OutConnectors)
                {
                    foreach (Connector line in sfdiagram.Page.Children.OfType<Connector>()
                                 .Where(connector => connector.TargetNode == con1.TargetNode || connector.SourceNode == con1.TargetNode))
                    {
                        if (line.SourceNode == n || line.TargetNode == n)
                            con = line;
                    }

                    foreach (Node v in sfdiagram.Page.Children.OfType<Node>())
                    {
                        if (v.DataContext == con1.TargetNode)
                        {
                            ExpandAnimation(v, n as OrgNodeViewModel, con);
                        }
                    }

                    ApplyAnimation(con1.TargetNode as OrgNodeViewModel);
                }
            }
        }

        private void OpacityAnimation(DependencyObject obj)
        {
            Duration duration = new Duration(TimeSpan.FromMilliseconds(300));
            DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();
            myDoubleAnimation1.Duration = duration;
            Storyboard sb = new Storyboard();
            sb.Duration = duration;
            sb.Children.Add(myDoubleAnimation1);
            Storyboard.SetTarget(myDoubleAnimation1, obj);
            Storyboard.SetTargetProperty(myDoubleAnimation1, "Opacity");
            myDoubleAnimation1.From = 0d;
            myDoubleAnimation1.To = 1d;
            sb.Begin();
        }
        Storyboard sb1;
        private void ExpandAnimation(Node children, OrgNodeViewModel source, Connector org)
        {
            Node sourcenode = null;
            foreach (Node node in sfdiagram.Page.Children.OfType<Node>())
            {
                if (node.DataContext == source)
                {
                    sourcenode = node;
                }
            }
            OpacityAnimation(children);
            if (sourcenode != null)
            {
                DateTime dt = DateTime.Now;
                sb1 = new Storyboard();
                TransitionCollection tc = new TransitionCollection();
                RepositionThemeAnimation rt = new RepositionThemeAnimation();
                rt.SpeedRatio = 0.80;
                Point offx = new Point(((source as OrgNodeViewModel).OffsetX - source.UnitWidth / 2), ((source as OrgNodeViewModel).OffsetY + (source as OrgNodeViewModel).UnitHeight / 2));
                rt.FromHorizontalOffset = -sfdiagram.Page.TransformToVisual(sourcenode).TransformPoint(offx).X;
                rt.FromVerticalOffset = -sfdiagram.Page.TransformToVisual(sourcenode).TransformPoint(offx).Y;
                sb1.Children.Add(rt);
                Storyboard.SetTarget(rt, children);
                sb1.Begin();
                sb1.Completed += (s, e) =>
                    {
                        if (ChartViewModel.SelectedObject != null)
                        {
                            if (((ChartViewModel.SelectedObject as OrgNodeViewModel).Content as Employee).IsExpand == State.Expand)
                            {
                                Update((ChartViewModel.SelectedObject as OrgNodeViewModel));
                            }
                        }
                    };
            }
        }


        private void Update(OrgNodeViewModel n)
        {
            if ((n.Content as Employee).IsExpand == State.Expand)
            {
                foreach (OrgConnectorViewModel con in (n.Info as INodeInfo).OutConnectors)
                {
                    (con.TargetNode as OrgNodeViewModel).Visible = true;
                    foreach (Connector line in sfdiagram.Page.Children.OfType<Connector>()
                                 .Where(connector => connector.TargetNode == con.TargetNode || connector.SourceNode == con.TargetNode))
                    {
                        if (line.SourceNode == n || line.TargetNode == n)
                        {
                            (line.DataContext as OrgConnectorViewModel).Visible = true;
                            OpacityAnimation(line);
                        }
                    }
                    Update((con.TargetNode as OrgNodeViewModel));
                }

            }
        }
        private object ClickedItem = "";
        #endregion
    
        private void searchgrid_KeyDown_1(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                ChartViewModel.Next.Execute("Next");
            }
        }
    }
    public static class Extension
    {
        public static T GetParent<T>(this DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = null;

            if (child.Dispatcher != null)
            {
                parent = VisualTreeHelper.GetParent(child);
            }
            else
            {
                parent = (parent as FrameworkElement).Parent ?? VisualTreeHelper.GetParent(child);
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

}
