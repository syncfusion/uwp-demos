using Common;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace Diagram
{
    public sealed partial class Stencil : SampleLayout
    {
        public Stencil()
        {
            this.InitializeComponent();
            diagram.PortVisibility = PortVisibility.Visible;
            (diagram.Info as IGraphInfo).ItemAdded += MainWindow_ItemAdded;
            stencil.SelectedFilter = new SymbolFilterProvider() { SymbolFilter = SymbolFilter };
        }

        private void MainWindow_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if (args.ItemSource == ItemSource.Stencil)
            {
                var dropedItem = args.Item as NodeViewModel;

                if (dropedItem != null)
                {
                    dropedItem.ShapeStyle = this.Resources["shapeStyle1"] as Style;
                    if (dropedItem.Ports != null)
                    {
                        foreach (var item in (dropedItem.Ports as ObservableCollection<INodePort>))
                        {
                            item.PortVisibility = PortVisibility.Visible;
                        }
                    }
                }
            }
        }
        //Filter event
        private bool SymbolFilter(SymbolFilterProvider sender, object symbol)
        {
            return true;
        }
    }
    //Collection of Symbols
    public class SymbolCollection : ObservableCollection<object>
    {

    }
}
