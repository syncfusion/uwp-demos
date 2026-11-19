using Common;
using Syncfusion.UI.Xaml.Grid;
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

namespace DataGrid
{
    public sealed partial class ClipboardOperation : SampleLayout
    {
        public ClipboardOperation()
        {
            this.InitializeComponent();
            var datacontext = new OrderInfoViewModel();
            this.DataContext = datacontext;
            this.PasteList.SelectionChanged += PasteList_SelectionChanged;
            this.CopyList.SelectionChanged += CopyList_SelectionChanged;
            this.CopyList.Loaded += CopyList_Loaded;
            this.PasteList.Loaded += PasteList_Loaded;
            this.SfDataGrid.ItemsSource = datacontext.OrdersListDetails;            
        }

        /// <summary>
        /// Set the default value for paste option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PasteList_Loaded(object sender, RoutedEventArgs e)
        {
            PasteList.SelectedIndex = 1;
        }

        /// <summary>
        /// Set the default value for copy option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CopyList_Loaded(object sender, RoutedEventArgs e)
        {
            CopyList.SelectedIndex = 1;
        }

        /// <summary>
        /// Set the value for CopyOption
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CopyList_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            var data = (sender as ListBox);
            var selecteditem = data.SelectedItems;
            if (data.SelectedItems != null)
            {
                for (int i = 0; i < selecteditem.Count; i++)
                {
                    if (i == 0)
                        this.SfDataGrid.GridCopyOption = (GridCopyOption)selecteditem[i];
                    else
                        this.SfDataGrid.GridCopyOption = this.SfDataGrid.GridCopyOption | (GridCopyOption)selecteditem[i];
                }
            }
        }

        /// <summary>
        /// Set the value for Paste Option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PasteList_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            var data = (sender as ListBox);
            var selecteditem = data.SelectedItems;
            if (data.SelectedItems != null)
            {

                for (int i = 0; i < selecteditem.Count; i++)
                {
                    if (i == 0)
                        this.SfDataGrid.GridPasteOption = (GridPasteOption)selecteditem[i];
                    else
                        this.SfDataGrid.GridPasteOption = this.SfDataGrid.GridPasteOption | (GridPasteOption)selecteditem[i];
                }
            }
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.PasteList.SelectionChanged -= PasteList_SelectionChanged;
            this.CopyList.SelectionChanged -= CopyList_SelectionChanged;
            this.CopyList.Loaded -= CopyList_Loaded;
            this.PasteList.Loaded -= PasteList_Loaded;          
            this.SfDataGrid.Dispose();
            this.SfDataGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
