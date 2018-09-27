using Common;
using System;
using Syncfusion.UI.Xaml.TreeGrid;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfTreeGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ValidationDemo : SampleLayout, IDisposable
    {
        public ValidationDemo()
        {
            this.InitializeComponent();
            this.treeGrid.CurrentCellValidating += TreeGrid_CurrentCellValidating;
            this.treeGrid.RowValidating += TreeGrid_RowValidating;
        }

        private void TreeGrid_RowValidating(object sender, TreeGridRowValidatingEventArgs args)
        {
            var data = args.RowData as PersonInfo;
            if (data.Salary > 50000)
            {
                args.ErrorMessages.Add("Salary", "The value should be a below of 50000.");
                args.IsValid = false;
            }
        }

        private void TreeGrid_CurrentCellValidating(object sender, TreeGridCurrentCellValidatingEventArgs args)
        {
            if (args.Column.MappingName == "LastName" && (args.NewValue == null || string.IsNullOrEmpty(args.NewValue.ToString())))
            {
                args.ErrorMessage = "LastName should not be null value";
                args.IsValid = false;
            }
        }

        public sealed override void Dispose()
        {            
            this.Resources.Clear();
            this.treeGrid.CurrentCellValidating -= TreeGrid_CurrentCellValidating;
            this.treeGrid.RowValidating -= TreeGrid_RowValidating;
            this.treeGrid.ItemsSource = null;
            this.treeGrid.Dispose();
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
