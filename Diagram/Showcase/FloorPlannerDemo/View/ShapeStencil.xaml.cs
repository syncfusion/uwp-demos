using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
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

namespace FloorPlannerDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShapeStencil : Grid
    {
        public ShapeStencil()
        {
            this.InitializeComponent();
            stencil.SelectedFilter = new SymbolFilterProvider() { SymbolFilter = Filter1, Content = "test" };
            stencil.Loaded += stencil_Loaded;
        }
        public FloorPlanSymbolGroup sg;
        void stencil_Loaded(object sender, RoutedEventArgs e)
        {
            //sg = stencil.FindDescendantByName("group") as FloorPlanSymbolGroup;
            //foreach (FloorPlanSymbolGroup f in sg.fsg)
            //{
            //    f.VModel = this.DataViewModel as FloorPlannerViewModel;
            //}
            //sg.VModel = this.DataViewModel as FloorPlannerViewModel;
            //stencil.Tag = this.DataViewModel;
        }
        public FloorPlannerViewModel DataViewModel
        {
            get { return (FloorPlannerViewModel)GetValue(DataViewModelProperty); }
            set { SetValue(DataViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataViewModelProperty =
            DependencyProperty.Register("DataViewModel", typeof(FloorPlannerViewModel), typeof(ShapeStencil), new PropertyMetadata(null, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //ShapeStencil c = d as ShapeStencil;
            //if (c.sg != null)
            //{
            //    c.sg.VModel = (FloorPlannerViewModel)e.NewValue;
            //}
        }
        private bool Filter1(SymbolFilterProvider sender, object symbol)
        {
            return true;
        }
    }
}
