#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DataGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CheckBoxSelectorColumn : SampleLayout
    {
        public CheckBoxSelectorColumn()
        {
            this.InitializeComponent();
            this.syncgrid.Loaded += Syncgrid_Loaded;
            var dataContext = new SelectionViewModel();
            this.DataContext = dataContext;
            this.syncgrid.ItemsSource = dataContext.ProductDetails;
        }

        private void Syncgrid_Loaded(object sender, RoutedEventArgs e)
        {
            var dataContext = this.DataContext as SelectionViewModel;
            this.syncgrid.SelectedItems = new System.Collections.ObjectModel.ObservableCollection<object>();
            this.syncgrid.SelectedItems.Add(dataContext.ProductDetails[1]);
            this.syncgrid.SelectedItems.Add(dataContext.ProductDetails[3]);
            this.syncgrid.SelectedItems.Add(dataContext.ProductDetails[6]);
            this.syncgrid.SelectedItems.Add(dataContext.ProductDetails[7]);
            this.syncgrid.SelectedItems.Add(dataContext.ProductDetails[11]);
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.syncgrid.Loaded -= Syncgrid_Loaded;
            this.syncgrid.Dispose();
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
