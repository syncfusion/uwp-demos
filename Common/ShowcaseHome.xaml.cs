#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Rotator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Common
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShowcaseHome : Page , IDisposable
    {
        ResourceDictionary res = new ResourceDictionary();
        public ShowcaseHome()
        {
            this.InitializeComponent();
            this.DataContext = SampleHelper.DataViewModel;
            res.Source = new Uri("ms-appx:///Syncfusion.SampleBrowser.UWP.Common/TileStyle.xaml", UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Add(res);

        }     
        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrameNavigationService.CurrentFrame.Navigate(typeof(ShowcasePage),e.ClickedItem);
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                (FrameNavigationService.Main as MainPage).backvisibility();
        }

     

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MainPage Main = e.Parameter as MainPage;
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                Main.ListSelection = 2;
            else
            {
                Main.ListSelection = 2;
               // FrameNavigationService.Header.Text = "Showcase";
            }
            //     SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
            //AppViewBackButtonVisibility.Collapsed;

            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                FrameNavigationService.Main.AllControlButtonvisibility(true);

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
          
            base.OnNavigatingFrom(e);
            Dispose();

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void root_Loaded(object sender, RoutedEventArgs e)
        {
            this.scroll.ScrollToVerticalOffset(FrameNavigationService.ShowcaseScrollOffset);

        }

        private void scroll_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            FrameNavigationService.ShowcaseScrollOffset = this.scroll.VerticalOffset;
        }

        public void Dispose()
        {
            this.Resources.MergedDictionaries.Remove(res);
            this.Resources.Clear();
            this.Resources = null;
            this.gridview = null;
            this.parentgrid = null;
        }
    }
}
