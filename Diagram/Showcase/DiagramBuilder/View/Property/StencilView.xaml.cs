using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using DiagramBuilder.ViewModel;
using Syncfusion.UI.Xaml.Diagram;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DiagramBuilder.View
{
    public sealed partial class StencilView : UserControl
    {
        public StencilView()
        {
            this.InitializeComponent();
            stencil.DoubleTapped += stencil_DoubleTapped;
        }

        void stencil_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            //Grid g = null;
            //GetParent(sender, out  g);
            //if (stencil.SelectedSymbol != null)
            //{
            //    ISymbol clone = (stencil.SelectedSymbol.DataContext as ISymbol).Clone();
            //    if (clone != null)
            //    {
            //        ObservableCollection<NodeVM> nodes = new ObservableCollection<NodeVM>();
            //        NodeVM node = new NodeVM();
            //        TabbedDiagrams td = g.Children[0] as TabbedDiagrams;
            //        (td.DataContext as DiagramBuilderVM).SelectedDiagram.NodeCollection.Add(node);
            //        IGraphInfo graph = (td.DataContext as DiagramBuilderVM).SelectedDiagram.Info as IGraphInfo;
            //        node.OffsetX = (graph.ScrollInfo.ViewportWidth) / 2;
            //        node.OffsetY = (graph.ScrollInfo.ViewportHeight) / 2;
            //        node.UnitWidth = 100;
            //        node.UnitHeight = 100;
            //        node.Content = clone.Symbol;
            //        node.ContentTemplate = clone.SymbolTemplate;
            //        node.IsSelected = true;
            //    }
            //}
        }

        private void GetParent(object sender, out Grid g)
        {
            var parent = VisualTreeHelper.GetParent(sender as DependencyObject);

            if (parent.GetType() == typeof(Grid))
            {
                if ((parent as Grid).Name == "rootgrid")
                {
                    g = (parent as Grid);
                }
                else
                {
                    GetParent(parent, out g);
                }
            }

            else
            {
                GetParent(parent, out g);
            }
        }
    }

}
