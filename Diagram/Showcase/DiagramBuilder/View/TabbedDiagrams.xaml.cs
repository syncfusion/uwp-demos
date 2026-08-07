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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DiagramBuilder.View
{
    public sealed partial class TabbedDiagrams : ItemsControl
    {
        public TabbedDiagrams()
        {
            this.InitializeComponent();
        }

        //public object SelectedDiagram
        //{
        //    get { return (object)GetValue(SelectedDiagramProperty); }
        //    set { SetValue(SelectedDiagramProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for SelectedDiagram.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty SelectedDiagramProperty =
        //    DependencyProperty.Register("SelectedDiagram", typeof(object), typeof(TabbedDiagrams), new PropertyMetadata(null));

        //public object Diagrams
        //{
        //    get { return (object)GetValue(DiagramsProperty); }
        //    set { SetValue(DiagramsProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Diagrams.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DiagramsProperty =
        //    DependencyProperty.Register("Diagrams", typeof(object), typeof(TabbedDiagrams), new PropertyMetadata(null));



        //public DataTemplate HeaderTemplate
        //{
        //    get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
        //    set { SetValue(HeaderTemplateProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for HeaderTemplate.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty HeaderTemplateProperty =
        //    DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(TabbedDiagrams), new PropertyMetadata(null));


        //protected override DependencyObject GetContainerForItemOverride()
        //{
        //    return new SfDiagramView();
        //}
    }
}
