using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI;
namespace WorkFlowEditor
{
    public class AutomationDiagram : SfDiagram
    {

        public AutomationDiagram()
        {
            CustomSelector selector = new CustomSelector();
            selector.Graph = this;
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
        protected override object GetNewConnector(Type desiredType)
        {
            return new ProcessAutomationConnector()
            {
                ConnectorGeometryStyle = GetStyle()
               
            };
        }     

        private Windows.UI.Xaml.Style GetStyle()
        {
            Windows.UI.Xaml.Style s = new Windows.UI.Xaml.Style(typeof(Windows.UI.Xaml.Shapes.Path));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, new SolidColorBrush(Colors.Black)));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeThicknessProperty, 1d));
            return s;
        }

 
    }

    public class CustomSelector : SelectorViewModel
    {
        public IGraph Graph { get; set; }

        private object _mobj;
        public object SelectedObject
        {
            get
            {
                return _mobj;
            }
            set
            {
                if (_mobj != value)
                {
                    _mobj = value;
                    OnPropertyChanged("SelectedObject");
                }
            }
        }

        private SolidColorBrush _brush=new SolidColorBrush(Colors.White);

        public SolidColorBrush fillbrush
        {
            get
            {
                return _brush;
            }
            set
            {
                if (_brush != value)
                {
                    _brush = value;
                    OnPropertyChanged("fillbrush");
                }
            }
        }

        //private ICommand _draw;

        //public ICommand Drawing
        //{
        //    get
        //    {
        //        return _draw;
        //    }
        //    set
        //    {
        //        if (_draw != value)
        //        {
        //            _draw = value;
        //            OnPropertyChanged("Drawing");
        //        }
        //    }
        //}
    }

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
