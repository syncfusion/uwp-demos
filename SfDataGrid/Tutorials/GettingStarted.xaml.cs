#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DataGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GettingStarted : SampleLayout
    {
        public GettingStarted()
        {
            this.InitializeComponent();
            this.DataContext = new OrderInfoViewModel();
            this.sfGrid.ItemsSource = (this.DataContext as OrderInfoViewModel).OrdersListDetails;            
            this.sfGrid.AutoGeneratingColumn += OnSfDataGridAutoGeneratingColumn;
        }

        /// <summary>
        /// Occurs while loading the column in SfDataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSfDataGridAutoGeneratingColumn(object sender, Syncfusion.UI.Xaml.Grid.AutoGeneratingColumnArgs e)
        {
            if (e.Column.MappingName == "OrderDate")
            {
                ((e.Column) as GridDateTimeColumn).FormatString = "dd/MM/yy";
                e.Column.TextAlignment=TextAlignment.Right;
            }
            else if (e.Column.MappingName == "IsVisible")
                e.Cancel = true;
        }        

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {         
            this.sfGrid.AutoGeneratingColumn -= OnSfDataGridAutoGeneratingColumn;
            this.sfGrid.Dispose();
            this.sfGrid.ItemsSource = null;          
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
