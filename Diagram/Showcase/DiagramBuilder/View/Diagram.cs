using Windows.UI.Xaml.Controls;
using DiagramBuilder.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Windows.UI.Text;
using Windows.UI.Xaml.Media;
using System.Runtime.Serialization;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Markup;

namespace DiagramBuilder.View
{
    public class Diagram : SfDiagram
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
            if(this.DataContext is DiagramVM)
            {
                (this.DataContext as DiagramVM).ScrollSettings = this.ScrollSettings;
            }
            //if (this.DataContext != null && (this.DataContext as DiagramVM)._isValidXml)
            //{
            //    IGraphInfo graph = (sender as SfDiagram).Info as IGraphInfo;
            //    PageVM page = (this.DataContext as DiagramVM).PageSettings as PageVM;
            //    graph.Commands.Zoom.Execute(
            //       new ZoomPositionParamenter()
            //       {
            //           ZoomTo = page.Scale
            //       });
            //    if (graph.ScrollInfo != null)
            //    {
            //        graph.ScrollInfo.PanTo(new Point(page.HOffset, page.VOffset));
            //    }
            //}
            (this.SelectedItems as SelectorViewModel).Commands = new QuickCommandCollection();
            QuickCommandViewModel Quickcommands_Delete = AddQuickCommand(new Thickness(0, 50, 0, 0), new Point(.5, 1), "Delete", (this.Info as IGraphInfo).Commands.Delete);
            QuickCommandViewModel Quickcommands_Draw = AddQuickCommand(new Thickness(50, 0, 0, 0), new Point(1, 0.5), "Draw", (this.Info as IGraphInfo).Commands.Draw);
            QuickCommandViewModel Quickcommands_Duplicate = AddQuickCommand(new Thickness(50, 50, 0, 0), new Point(1, 1), "Duplicate", (this.Info as IGraphInfo).Commands.Duplicate);
        }

        private QuickCommandViewModel AddQuickCommand(Thickness margin, Point offset, string content, ICommand command)
        {
            QuickCommandViewModel quickCommand = new QuickCommandViewModel()
            {
                Margin = margin,
                OffsetX = offset.X,
                OffsetY = offset.Y,
                Command = command,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Content = GetTemplate(content),
                VisibilityMode = VisibilityMode.Node,
                Shape ="F1M23.467,11.733C23.467,18.213 18.214,23.466 11.734,23.466 5.253,23.466 0,18.213 0,11.733 0,5.253 5.253,0 11.734,0 18.214,0 23.467,5.253 23.467,11.733",
                ShapeStyle=GetQuickCommandShapeStyle(),
            };

            if (content == "Draw" || content == "Duplicate")
            {
                DrawParameter drawParameter = new DrawParameter(DrawingTool.Connector, null, null, null, null,
                    NullSourceTarget.SelectionAsSource | NullSourceTarget.CloneSourceAsTarget);
                DupilicateParameter duplicateParameter = new DupilicateParameter() {DragClone = true};
                quickCommand.DragCommand = command;
                if (content == "Duplicate")
                {
                    quickCommand.CommandParameter = duplicateParameter;
                }
                else
                {
                    quickCommand.CommandParameter = drawParameter;
                }
            }
            ((this.SelectedItems as SelectorViewModel).Commands as QuickCommandCollection).Add(quickCommand);
            return quickCommand;
        }

        public Style GetQuickCommandShapeStyle()   
        {
            const string cTemplate = "<Style TargetType=\"Path\"" +
                      " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" >" +
                      " <Setter  Property=\"Fill\" Value=\"#4D4D4D\">" +
                       " </Setter>" +
                       " <Setter  Property=\"StrokeThickness\" Value=\"1\">" +
                       " </Setter>" +
                       " </Style>";

            return XamlReader.Load(cTemplate) as Style;
        }
       
        /// <summary>
        /// Constructs a new instance of the <see cref="GetTemplate(string)"/> method.
        /// </summary>
        /// <param name="item">Gets the item value</param>
        /// <returns>Returns the string</returns>
        private object GetTemplate(string item)
        {
            object s = null;
            if (item == "Delete")
            {
                s = "F1M377.5879,673.9355L376.1439,675.4095L369.8539,668.9945L363.5639,675.4095L362.1189,673.9355L368.4079,667.5205L362.1189,661.1035L363.5639,659.6295L369.8539,666.0465L376.1439,659.6295L377.5879,661.1035L371.2989,667.5205z";
            }
            else if (item == "Draw")
            {
                s = "F1M423.144,677.5312L423.144,671.2692L410.147,671.2692L410.147,666.4982L423.144,666.4982L423.144,661.0822L431.368,669.3062z";
            }
            else if (item == "Duplicate")
            {
                s = "M13.671994,14.169995 L1.0569996,14.169995 1.0569996,3.6649869 1.8049993,3.6649869 1.8049993,2.609986 0,2.609986 0,15.224996 14.724994,15.224996 14.724994,13.377994 13.671994,13.377994 z M2.6349954,12.613986 L17.358003,12.613986 17.358003,1.323489E-23 2.6349954,1.323489E-23 z";
            }

            return s;
        }
       void Diagram_Unloaded(object sender, RoutedEventArgs e)
        {
            
            this.Unloaded -= Diagram_Unloaded;
            KnownTypes = null;
        }

#if !SyncfusionFramework4_5_1

        public object ExportSettings
        {
            get { return (object)GetValue(ExportSettingsProperty); }
            set { SetValue(ExportSettingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExportSettings.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExportSettingsProperty =
            DependencyProperty.Register("ExportSettings", typeof(object), typeof(Diagram), new PropertyMetadata(null));



        public object PrintingService
        {
            get { return (object)GetValue(PrintingServiceProperty); }
            set { SetValue(PrintingServiceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PrintingService.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrintingServiceProperty =
            DependencyProperty.Register("PrintingService", typeof(object), typeof(Diagram), new PropertyMetadata(null));



#endif

        [DataMember]
        public string Title
        {
            get { return (string )GetValue(TitleProperty); }
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
            DependencyProperty.Register("Title", typeof(string ), typeof(Diagram), new PropertyMetadata(string.Empty));

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

        protected override Selector GetSelectorForItemOverride(object item)
        {
            return new SelectorView();
        }

        protected override object GetNewNode(Type desiredType)
        {
            return new NodeVM();
        }

        protected override object GetNewConnector(Type desiredType)
        {
            return new ConnectorVM();
        }

        protected override object GetNewGroup(Type desiredType)
        {
            return new GroupVM();
        }

        protected override void SetTool(SetToolArgs args)
        {
            if (args.Source is NodePortVM && this.Tool != Tool.ContinuesDraw && this.Tool != Tool.DrawOnce)
            {
                args.Action = ActiveTool.Drag;
            }
            if (args.Source is NodePortVM && (this.Tool == Tool.ContinuesDraw || this.Tool == Tool.DrawOnce))
            {
                args.Action = ActiveTool.Draw;
            }
            else
            {
                base.SetTool(args);
            }
        }
    }
}