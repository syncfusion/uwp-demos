#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Controls.DataPager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace ExpenseAnalysisDemo
{
    public sealed partial class GridView : UserControl,IDisposable
    {
        public GridView()
        {
            this.InitializeComponent();

            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                sfDataPager.DisplayMode = PageDisplayMode.PreviousNextNumeric;

            }
            this.Unloaded += OnGridViewUnloaded;
        }

        private void OnGridViewUnloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            this.Resources.Clear();
            this.SyncfusionGrid.ItemsSource = null;
            this.Unloaded -= OnGridViewUnloaded;
            this.SyncfusionGrid.Dispose();
            this.sfDataPager.Dispose();
            if (DataContext != null)
                (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            this.Content = null;
        }
    }
}
