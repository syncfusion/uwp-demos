using Common;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DataGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FreezePanes : SampleLayout
    {
        public FreezePanes()
        {
            this.InitializeComponent();
            var datacontext = new SalesInfoViewModel();
            this.DataContext = datacontext;
            this.sfGrid.ItemsSource = datacontext.YearlySalesDetails;                  
        }
     
        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();        
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
