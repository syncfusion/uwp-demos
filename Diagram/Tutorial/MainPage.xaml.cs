using Common;
using Syncfusion.UI.Xaml.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Diagram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : SampleLayout
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            SampleViews views = new SampleViews();
            if (DeviceInfo.TargetDeviceOS == DeviceOS.Windows)
            {
                views.Add(new SampleViewItem() { Type = "Diagram.FlowDiagram", Header = "Flow Diagram" });
                views.Add(new SampleViewItem() { Type = "Diagram.NodeContent", Header = "Node Content" });
                views.Add(new SampleViewItem() { Type = "Diagram.HierarchicalTree", Header = "Hierarchical Tree" });
                views.Add(new SampleViewItem() { Type = "Diagram.RadialTree", Header = "Radial Tree" });
                //views.Add(new SampleViewItem() { Type = "Diagram.Annotation", Header = "Annotation" ,IsNew=true});
                //views.Add(new SampleViewItem() { Type = "Diagram.DrawingTools", Header = "Drawing Tools", IsNew = true });
                views.Add(new SampleViewItem() { Type = "Diagram.IshikawaDiagram", Header = "IshikawaDiagram" });
                views.Add(new SampleViewItem() { Type = "Diagram.RulerAndUnit", Header = "Rulers And Units" });
                //views.Add(new SampleViewItem() { Type = "Diagram.EdgeToEdge", Header = "Edge To Edge", IsNew = true });
                views.Add(new SampleViewItem() { Type = "Diagram.Overview", Header = "Overview" });
                views.Add(new SampleViewItem() { Type = "Diagram.CircuitDiagram", Header = "Circuit Diagram" });
                views.Add(new SampleViewItem() { Type = "Diagram.Virtualization", Header = "Virtualization" });
                views.Add(new SampleViewItem() { Type = "Diagram.Stencil", Header = "Stencil" });
            }
            else
            {
                views.Add(new SampleViewItem() { Type = "Diagram.FlowDiagram_WP", Header = "Flow Diagram" });
                views.Add(new SampleViewItem() { Type = "Diagram.NodeContent_WP", Header = "Node Content" });
                views.Add(new SampleViewItem() { Type = "Diagram.HierarchicalTree_WP", Header = "Hierarchical Tree" });
                views.Add(new SampleViewItem() { Type = "Diagram.RadialTree_WP", Header = "Radial Tree" });
                
                views.Add(new SampleViewItem() { Type = "Diagram.IshikawaDiagram_WP", Header = "IshikawaDiagram" });
                views.Add(new SampleViewItem() { Type = "Diagram.RulerandUnit_WP", Header = "Rulers And Units" });
               views.Add(new SampleViewItem() { Type = "Diagram.CircuitDiagram_WP", Header = "Circuit Diagram" });
                
               
                //views.Add(new SampleViewItem() { Type = "Diagram.FlowChart_WP",  Header = "Flow Diagram" });
                //views.Add(new SampleViewItem() { Type = "Diagram.HierarchicalLayout_WP ", Header = "Hierarchical Layout" });
                //views.Add(new SampleViewItem() { Type = "Diagram.RadialLayout_WP", Header = "Radial Layout" });
                //views.Add(new SampleViewItem() { Type = "Diagram.NodeContent_WP", Header = "Node Content" });
                //views.Add(new SampleViewItem() { Type = "Diagram.CustomPort_WP", Header = "Custom Port" });
                //views.Add(new SampleViewItem() { Type = "Diagram.CircuitDemo_WP", Header = "Circuit" });
                //views.Add(new SampleViewItem() { Type = "Diagram.RulerAndUnit_WP", Header = "Rulers And Units" });

            }
            this.SampleViews = views;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
