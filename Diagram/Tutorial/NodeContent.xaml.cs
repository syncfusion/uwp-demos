using System.Collections.ObjectModel;
using Common;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Diagram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NodeContent : SampleLayout 
    {
        public NodeContent()
        {
            this.InitializeComponent();

            //To Disable ContextMenu
            diagramControl.Menu = null;
            diagramControl.Tool = Tool.ZoomPan;

            //Initialize Constraints
            InitializeDiagram();

            diagramControl.PointerReleased += diagramControl_PointerReleased;

            //Unload diagram
            this.Unloaded += diagramControl_Unloaded;
            this.Loaded += NodeContent_Loaded;
            connector2.Segments = new SegmentPoint()
            {
                new StraightSegment() {Point = new Point(282.5, 100.0)}
            };
            connector3.Segments = new SegmentPoint()
            {
                new StraightSegment() {Point = new Point(282.5,500)}
            };

            connector6.Segments = new SegmentPoint()
            {
                new StraightSegment() {Point = new Point(635,500)}
            };
        }

        private void NodeContent_Loaded(object sender, RoutedEventArgs e)
        {
            if (connector3 != null)
            {
                connector3.Segments = new ObservableCollection<IConnectorSegment>()
               {
                 new StraightSegment()
                 {
                   Point=new Point(282.5,500)
                 }
               };
            }
        }

        private void InitializeDiagram()
        {
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                diagramControl.Constraints |= GraphConstraints.AllowPan;
                diagramControl.Tool = Tool.ZoomPan;
            }
                //Constraints used to enable/disable the Draggable.
               // diagramControl.Constraints = diagramControl.Constraints &
               //                          ~(GraphConstraints.Selectable | GraphConstraints.Draggable);
            diagramControl.KnownTypes = GetKnownTypes;
            diagramControl.CommandManager.View = (Control)Window.Current.Content;
        }

        void diagramControl_Unloaded(object sender, RoutedEventArgs e)
        {
            diagramControl.PointerReleased -= diagramControl_PointerReleased;
            diagramControl.KnownTypes = null;
            this.Unloaded -= diagramControl_Unloaded;
        }

        void diagramControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }

        //Helps to Serialize the Shape
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
    }

    public class PortCollection : ObservableCollection<IPort>
    {
        
    }

    public class SegmentPoint : ObservableCollection<IConnectorSegment>
    {

    }
}
