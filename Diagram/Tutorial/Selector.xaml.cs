using System.Collections.ObjectModel;
using Common;
using Syncfusion.UI.Xaml.Controls;
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
using System.Windows.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Diagram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Selector : SampleLayout, IDisposable
    {
     //   ObservableCollection<CustomNode> nodes = null;
        public Selector()
        {
            this.InitializeComponent();

            //Register CollectionChanged Event
            (this.diagramControl.Nodes as NodeVmCollection).CollectionChanged += Nodes_CollectionChanged;

            //Initialize PageSettings
            InitializeDiagram();

            //Unload the Diagram
            this.Unloaded += diagramControl_Unloaded;


            //Info - for Diagram Events and Commands
            IGraphInfo info = diagramControl.Info as IGraphInfo;

            //Event to notify the when Items added
            info.ItemAdded += info_ItemAdded;
        }

        private void info_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            //Items added due to clipboard operation
            if (args.ItemSource == ItemSource.ClipBoard)
            {
                if (args.Item is INode)
                {
                    NodeVm node = args.Item as NodeVm;
                    node.ShapeStyle = this.Resources["nodeshapestyle"] as Style;
                    if (node.Annotations != null)
                    {
                        foreach (AnnotationEditorViewModel vm in (node.Annotations as AnnotationCollection))
                        {
                            vm.ViewTemplate = this.Resources["nodeviewtemplate"] as DataTemplate;
                        }
                    }
                }
            }
        }

        private void InitializeDiagram()
        {
            diagramControl.PageSettings = null;
            diagramControl.PointerReleased += diagramControl_PointerReleased;
            diagramControl.CommandManager.View = (Control)Window.Current.Content;
            diagramControl.KnownTypes = GetKnownTypes;
        }

        void diagramControl_Unloaded(object sender, RoutedEventArgs e)
        {
            diagramControl.PointerReleased -= diagramControl_PointerReleased;
            diagramControl.KnownTypes = null;
            this.Unloaded -= diagramControl_Unloaded;
            //Info - for Diagram Events and Commands
            IGraphInfo info = diagramControl.Info as IGraphInfo;
            //Event to notify the when Items added
            info.ItemAdded -= info_ItemAdded;
            //Register CollectionChanged Event
            (this.diagramControl.Nodes as NodeVmCollection).CollectionChanged -= Nodes_CollectionChanged;
        }

        //Helps to serialize the shape
        private IEnumerable<Type> GetKnownTypes()
        {
            List<Type> known = new List<Type>()
            {
                //typeof(TicketBookingDemo.Shapes)
            };
            foreach (var item in known)
            {
                yield return item;
            }
        }

        void diagramControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }

        #region Private Functions

        //To Represent Different Style for ResizeHandler 
        private void ChangeResizer(string s)
        {
            if (diagramControl != null)
            {
                switch (s)
                {
                    case "VisioSelector":
                        diagramControl.SFSelector.Style = this.Resources["VisioSelector"] as Style;
                        break;
                    case "OldSelector":
                        diagramControl.SFSelector.Style = this.Resources["OldSelector"] as Style;
                        break;
                    case "CustomSelector":
                        diagramControl.SFSelector.Style = this.Resources["CustomSelector"] as Style;
                        break;
                }
            }

        }
        #endregion

        #region Events
        //Nodes CollectionChanged Event
        void Nodes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                NodeVm newNode = (e.NewItems[0] as NodeVm);
                //if (Style1.IsChecked == true)
                //{
                //    ChangeResizer("OldSelector");
                //}
                //else 
                if (Style2.IsChecked == true)
                {
                    ChangeResizer("VisioSelector");
                }
                else if (Style3.IsChecked == true)
                {
                    ChangeResizer("CustomSelector");
                }
            }
        }

        //DeFault Style
        private void Style1_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton send = sender as RadioButton;

            switch (send.Name)
            {
                case "Style1":
                    ChangeResizer("OldSelector");
                    break;
                case "Style2":
                    if (Style4 != null && Style3 != null)
                    {
                        Style4.IsChecked = false;
                        Style3.IsChecked = false;
                    }
                    ChangeResizer("VisioSelector");
                    break;
                case "Style3":
                    Style2.IsChecked = false;
                    Style4.IsChecked = false;
                    ChangeResizer("CustomSelector");
                    break;
                case "Style4":
                    Style2.IsChecked = false;
                    Style3.IsChecked = false;
                    diagramControl.SFSelector.ClearValue(Syncfusion.UI.Xaml.Diagram.Selector.StyleProperty);
                    break;
            }

        }

        //Visio Style
        private void Style2_Checked(object sender, RoutedEventArgs e)
        {

        }

        //Custom Style
        private void Style3_Checked(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void SampleView_Loaded_1(object sender, RoutedEventArgs e)
        {
            Style2.IsChecked = true;
            ChangeResizer("VisioSelector");
        }

        public override void Dispose()
        {
            if (this.diagramControl.Nodes as NodeVmCollection != null)
            {
                (this.diagramControl.Nodes as NodeVmCollection).CollectionChanged -= Nodes_CollectionChanged;
            }
            base.Dispose();
        }
    }

    //To Represent SfDiagram Commands
    public class CustomDiagramControl : SfDiagram
    {
        public CustomDiagramControl()
        {
            CustomSelector selector = new CustomSelector();
            selector.Graph = (this.Info as IGraphInfo);
            selector.Graph.Commands.Delete.Execute(null);
            selector.Graph.Commands.BringToFront.Execute(null);
            selector.Graph.Commands.SendToBack.Execute(null);
            selector.Graph.Commands.Draw.Execute(null);
            SelectedItems = selector;
            selector.ZIndex = 10000;
            selector.Nodes = new ObservableCollection<object>();
            selector.Connectors = new ObservableCollection<object>();
            selector.Groups = new ObservableCollection<object>();
        }

        public Syncfusion.UI.Xaml.Diagram.Selector SFSelector = new Syncfusion.UI.Xaml.Diagram.Selector();

        protected override Syncfusion.UI.Xaml.Diagram.Selector GetSelectorForItemOverride(object item)
        {
            return SFSelector;
        }

        protected override Connector GetConnectorForItemOverride(object item)
        {
            return new Connector();
        }
    }

    //To Represent Selector
    public class CustomSelector : SelectorViewModel
    {
        public IGraphInfo Graph { get; set; }

    }

    //To Represent ContentControl Class
    public class SelectorItem : ContentControl
    {
        public SelectorItem()
        {
            this.Loaded += SelectorItem_Loaded;

        }

        void SelectorItem_Loaded(object sender, RoutedEventArgs e)
        {
            this.Tapped += SelectorItem_Tapped;
            this.PointerPressed += SelectorItem_PointerPressed;
            this.PointerMoved += SelectorItem_PointerMoved;
            this.PointerReleased += SelectorItem_PointerReleased;
            this.Unloaded += SelectorItem_Unloaded;

        }

        void SelectorItem_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= SelectorItem_Unloaded;
            this.Tapped -= SelectorItem_Tapped;
            this.PointerPressed -= SelectorItem_PointerPressed;
            this.PointerMoved -= SelectorItem_PointerMoved;
            this.PointerReleased -= SelectorItem_PointerReleased;
            this.Loaded -= SelectorItem_Loaded;
        }
        private bool isPressed = false;

        void SelectorItem_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            isPressed = false;
        }

        void SelectorItem_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            CustomSelector selectorViewModel = this.DataContext as CustomSelector;
            if (isPressed && Command != null)
            {
                DrawParam param = new DrawParam(e, ((IEnumerable<object>)selectorViewModel.Nodes).FirstOrDefault());
                this.Command.Execute(param);
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

    //To Represent IDrawParameter Properties
    public class DrawParam : IDrawParameter
    {
        public DrawingTool Tool { get; set; }
        public Point? Point { get; set; }
        public object Node { get; set; }
        public object Port { get; set; }
        public PointerRoutedEventArgs PressedEventArgs { get; set; }
        public NullSourceTarget NullSourceTarget { get; private set; }

        public DrawParam(PointerRoutedEventArgs args, object node)
        {
            Tool = DrawingTool.Connector;
            PressedEventArgs = args;
            Node = node;
            NullSourceTarget = NullSourceTarget.None;
        }
    }
}
