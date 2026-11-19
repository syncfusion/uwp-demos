using Mockup.Utility;
using Mockup.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Mockup.View
{
    internal class Diagram : SfDiagram
    {
        public Diagram()
        {
            this.KnownTypes = new GetTypes(GetKnownTypes);

            this.SetBinding(TitleProperty,
                new Binding()
                {
                    Path = new PropertyPath("Title"),
                    Mode = BindingMode.TwoWay
                });

            this.SetBinding(IsSelectedProperty,
                new Binding()
                {
                    Path = new PropertyPath("IsSelected"),
                    Mode = BindingMode.TwoWay
                });

            this.SetBinding(PageDummyProperty,
                new Binding()
                {
                    Path = new PropertyPath("PageSettings"),
                    Mode = BindingMode.TwoWay
                });
            this.Loaded += Diagram_Loaded;
            this.Menu = null;
            this.Unloaded += Diagram_Unloaded;
            this.PointerReleased += Diagram_PointerReleased;
        }

        void Diagram_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }

        void Diagram_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is DiagramVM)
            {
                (this.DataContext as DiagramVM).ScrollSettings = this.ScrollSettings;
            }
            if (this.DataContext != null && (this.DataContext as DiagramVM)._isValidXml)
            {
                IGraphInfo graph = (sender as SfDiagram).Info as IGraphInfo;
                (sender as SfDiagram).Constraints = (sender as SfDiagram).Constraints | GraphConstraints.Undoable;
                PageVM page = (this.DataContext as DiagramVM).PageSettings as PageVM;
                graph.Commands.Zoom.Execute(
                   new ZoomPositionParamenter()
                   {
                       ZoomTo = page.Scale
                   });
                if (this.ScrollSettings.ScrollInfo != null)
                {
                    this.ScrollSettings.ScrollInfo.PanTo(new Point(page.HOffset, page.VOffset));
                }
                (this.DataContext as DiagramVM).HorizontalRuler = this.HorizontalRuler;
                (this.DataContext as DiagramVM).VerticalRuler = this.VerticalRuler;
            }
            this.CommandManager.View = (Control)Window.Current.Content;
        }

        void Diagram_Unloaded(object sender, RoutedEventArgs e)
        {

            this.Unloaded -= Diagram_Unloaded;
            KnownTypes = null;
        }

        //#if !SyncfusionFramework4_5_1

//        public object ExportSettings
//        {
//            get { return (object)GetValue(ExportSettingsProperty); }
//            set { SetValue(ExportSettingsProperty, value); }
//        }

//        // Using a DependencyProperty as the backing store for ExportSettings.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty ExportSettingsProperty =
//            DependencyProperty.Register("ExportSettings", typeof(object), typeof(Diagram), new PropertyMetadata(null));



//        public object PrintingService
//        {
//            get { return (object)GetValue(PrintingServiceProperty); }
//            set { SetValue(PrintingServiceProperty, value); }
//        }

//        // Using a DependencyProperty as the backing store for PrintingService.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty PrintingServiceProperty =
//            DependencyProperty.Register("PrintingService", typeof(object), typeof(Diagram), new PropertyMetadata(null));



//#endif

        [DataMember]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        [DataMember]
        public PageVM PageDummy
        {
            get { return (PageVM)GetValue(PageDummyProperty); }
            set { SetValue(PageDummyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageDummy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageDummyProperty =
            DependencyProperty.Register("PageDummy", typeof(PageVM), typeof(Diagram), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Diagram), new PropertyMetadata(string.Empty));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(Diagram), new PropertyMetadata(false));

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            (DataContext as IDiagramViewModel).FirstLoad.Execute(null);
        }

        public IEnumerable<Type> GetKnownTypes()
        {
            List<Type> known = new List<Type>()
            {
                typeof(FontStyle),
                typeof(TextAlignment),
                typeof(HorizontalAlignment),
                typeof(VerticalAlignment),
                typeof(Visibility),
                typeof(ConnectType),
                typeof(ConnectorType),
                typeof(Thickness),
                typeof(DoubleCollection),
                typeof(FontWeight),
                typeof(PageVM),
            };
            foreach (var item in known)
            {
                yield return item;
            }
        }

        protected override Node GetNodeForItemOverride(object item)
        {
            return new NodeView();
        }

        protected override Connector GetConnectorForItemOverride(object item)
        {
            return new ConnectorView();
        }

        //protected override Selector GetSelectorForItemOverride(object item)
        //{
        //    return new SelectorView();
        //}

        protected override object GetNewNode(Type desiredType)
        {
            //return new NodeVM();
            NodeVM nodevm = new NodeVM();
            return nodevm;
        }

        protected override object GetNewConnector(Type desiredType)
        {
            return new ConnectorVM();
        }

        protected override object GetNewGroup(Type desiredType)
        {
            return new GroupVM();
        }
    }
}
