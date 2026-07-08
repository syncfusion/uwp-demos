using Common;
using System;
using System.Collections.Generic;
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

namespace Syncfusion.SampleBrowser.UWP.SfTreeGrid
{
    public sealed partial class SelfRelational : SampleLayout , IDisposable
    {
        public SelfRelational()
        {
            this.InitializeComponent();
        }   
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            treeGrid.ExpandAllNodes();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            treeGrid.CollapseAllNodes();
        }

        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.treeGrid.ItemsSource = null;
            this.treeGrid.Dispose();
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }

    }
}
