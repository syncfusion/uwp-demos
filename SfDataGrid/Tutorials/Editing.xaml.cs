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
    public sealed partial class Editing : SampleLayout
    {
        ManipulatorView view;
        public Editing()
        {
            this.InitializeComponent();
            var datacontext = new DataBoundViewModel();
            this.DataContext = datacontext;
            this.syncgrid.ItemsSource = datacontext.OrdersListDetails;
            this.syncgrid.DoubleTapped += OnSfDataGridDoubleTapped;
            this.syncgrid.Loaded += OnSfDataGridLoaded;        
        }       

        /// <summary>
        /// Occurs when the SfDataGrid is laid out, rendered, and ready for interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSfDataGridLoaded(object sender, RoutedEventArgs e)
        {         
            this.layer.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.Touch1.Begin();
        }

        /// <summary>
        /// Occurs when double tap the SfDataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSfDataGridDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Point pp = e.GetPosition(null);
            var uielements = VisualTreeHelper.FindElementsInHostCoordinates(pp, this.syncgrid);
            if (uielements.Count() > 0 && uielements.Any(element => element is GridHeaderCellControl))
                return;
            if (this.syncgrid.SelectedItem != null)
            {
                if (view == null)
                {
                    view = new ManipulatorView();
                    popup.Child = view;
                    view.Tag = popup;
                }
                view.DataContext = this.syncgrid.SelectedItem;
                view.Width = this.syncgrid.ActualWidth + 10 + 55;
                view.Height = this.syncgrid.ActualHeight + 80;
                popup.Margin = new Thickness(-25, -50, 0, 0);
                popup.IsOpen = true;
            }
        }

        /// <summary>
        /// Occurs when pressed the SfDataGrid
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            this.Touch1.Stop();
            this.layer.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            base.OnPointerPressed(e);
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            this.syncgrid.DoubleTapped -= OnSfDataGridDoubleTapped;
            this.syncgrid.Loaded -= OnSfDataGridLoaded;           
            this.syncgrid.Dispose();
            this.syncgrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
