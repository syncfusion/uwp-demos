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
    public sealed partial class ConditionalCheckBox : SampleLayout , IDisposable
    {
        EmployeeDetailsRepository viewModel;
        public ConditionalCheckBox()
        {
            this.InitializeComponent();
            this.viewModel = this.treeGrid.DataContext as EmployeeDetailsRepository;
            this.treeGrid.Loaded += TreeGrid_Loaded;
            this.treeGrid.Unloaded += TreeGrid_Unloaded;
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
        }   

        private void treeGrid_RequestChildSource(object sender, UI.Xaml.TreeGrid.TreeGridRequestTreeItemsEventArgs args)
        {
            if (args.ParentItem == null)
            {
                //get the root list - get all employees who have no boss 
                args.ChildItems = viewModel.EmployeeDetails.Where(x => x.ReportsTo == -1); //get all employees whose boss's id is -1 (no boss)
            }
            else //if ParentItem not null, then set args.ChildList to the child items for the given ParentItem.
            {   //get the children of the parent object
                EmployeeInfo emp = args.ParentItem as EmployeeInfo;
                if (emp != null)
                {
                    //get all employees that report to the parent employee
                    args.ChildItems = viewModel.GetReportees(emp.ID);
                }
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
            this.DataContext = null;
            base.Dispose();
        }

    }
}
