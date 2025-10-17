#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Controls.Navigation;
using Syncfusion.UI.Xaml.Rotator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public sealed partial class WhatsNewPage : Page, IDisposable
    {

        public WhatsNewPage()
        {
            this.InitializeComponent();    
            DataContext = SampleHelper.DataViewModel;
            if (IsMobileFamily())
            {
                root.Margin = new Thickness(10);
                newtemplate.Margin = new Thickness(0);
            }
        }
        public static bool IsMobileFamily()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                return true;
            }
            return false;
        }
        private void WhatsNewPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            FrameNavigationService.GoBack();
            e.Handled = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MainPage Main = e.Parameter as MainPage;
            Main.ListSelection = 1;
            FrameNavigationService.Header.Text = "Whats New";
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                FrameNavigationService.Main.AllControlButtonvisibility(true);
            base.OnNavigatedTo(e);
        }


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

            FrameNavigationService.WhatsnewScrollOffset = this.scroll.VerticalOffset;
            base.OnNavigatingFrom(e);
            Dispose();


        }

        private void root_Loaded(object sender, RoutedEventArgs e)
        {
            this.scroll.ScrollToVerticalOffset(FrameNavigationService.WhatsnewScrollOffset);

        }

        public void Dispose()
        {
            this.Loaded -= root_Loaded;
            this.newtemplate.DataContext = null;
            this.newtemplate.Dispose();
            this.newtemplate = null;
            GC.Collect();
        }
    }


}
