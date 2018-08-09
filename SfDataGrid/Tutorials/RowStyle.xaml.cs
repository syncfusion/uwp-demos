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
using Common;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DataGrid
{
    public sealed partial class RowStyle : SampleLayout
    {
        public RowStyle()
        {
            this.InitializeComponent();
            var datacontext = new EmployeeInfoViewModel();
            this.DataContext = datacontext;
            this.sfDataGrid.ItemsSource = datacontext.EmployeeDetails200;
        }        
        /// <summary>
        /// Dispose of unmanaged resources. 
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();         
            this.sfDataGrid.Dispose();
            this.sfDataGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
