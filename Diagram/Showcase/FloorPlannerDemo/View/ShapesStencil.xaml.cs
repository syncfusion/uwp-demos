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
    public sealed partial class ShapesStencil : Page
    {
        public ShapesStencil()
        {
            this.InitializeComponent();
            //MyStencil = stencil;
          //  stencil.SelectedFilter = new Syncfusion.UI.Xaml.Diagram.Stencil.SymbolFilterProvider() { Content = "Test", Filter = Filter };
        }

        private bool Filter(Syncfusion.UI.Xaml.Diagram.Stencil.SymbolFilterProvider sender, Syncfusion.UI.Xaml.Diagram.Stencil.ISymbol symbol)
        {
            return true;
        }

        public Stencil MyStencil { get; set; }


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
