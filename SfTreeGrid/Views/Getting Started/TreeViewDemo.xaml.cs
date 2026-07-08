using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.UI.Xaml.TreeGrid;
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
using Windows;
using TreeGrid;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.SfTreeGrid
{
    
    public sealed partial class TreeViewDemo : Common.SampleLayout, IDisposable
    {
        TreeViewModel viewModel;
        public TreeViewDemo()
        {
            this.InitializeComponent();
            this.viewModel = this.treeGrid.DataContext as TreeViewModel;
            this.treeGrid.Loaded += TreeGrid_Loaded;
            this.treeGrid.Unloaded += TreeGrid_Unloaded;
            this.treeGrid.SelectionController = new TreeGridSelectionControllerExt(this.treeGrid);
        }

        private void TreeGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            this.treeGrid.RequestTreeItems -= treeGrid_RequestChildSource;
            this.treeGrid.RepopulateTree();
        }

        private void TreeGrid_Loaded(object sender, RoutedEventArgs e)
        {
            this.treeGrid.RequestTreeItems += treeGrid_RequestChildSource;
            this.treeGrid.RepopulateTree();
            this.treeGrid.ExpandNode(1);
        }

        private void treeGrid_RequestChildSource(object sender, UI.Xaml.TreeGrid.TreeGridRequestTreeItemsEventArgs args)
        {
            if (args.ParentItem == null)
            {
                args.ChildItems = viewModel.ItemsCollection;
            }
            else
            {
                args.ChildItems = viewModel.GetSubItems((args.ParentItem as TreeModel).Name);
            }
        }

        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.treeGrid.RequestTreeItems -= treeGrid_RequestChildSource;
            this.treeGrid.Loaded -= TreeGrid_Loaded;
            this.treeGrid.Unloaded -= TreeGrid_Unloaded;
            this.treeGrid.Dispose();
            (this.treeGrid.DataContext as IDisposable).Dispose();
            this.treeGrid.DataContext = null;
            base.Dispose();
        }

    }
}
