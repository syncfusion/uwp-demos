using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.SfTreeGrid
{
    public sealed partial class EditingDemo : SampleLayout , IDisposable
    {
        public EditingDemo()
        {
            this.InitializeComponent();
            this.treeGrid.CurrentCellRequestNavigate += TreeGrid_CurrentCellRequestNavigate;
        }

        private async void TreeGrid_CurrentCellRequestNavigate(object sender, UI.Xaml.Grid.CurrentCellRequestNavigateEventArgs args)
        {
             var URI = "http://en.wikipedia.org/wiki/" + args.NavigateText;
             await Launcher.LaunchUriAsync(new Uri(URI));
        }
      
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.treeGrid.CurrentCellRequestNavigate -= TreeGrid_CurrentCellRequestNavigate;  
            this.treeGrid.ItemsSource = null;
            this.treeGrid.Dispose();
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }

    }
}
