#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Common
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WhatsNewContentPage : Page
    {
        public WhatsNewContentPage()
        {
            this.InitializeComponent();
            if(DeviceFamily.GetDeviceFamily()==Devices.Mobile)
            {
                viewContent.Margin = new Thickness(0, 0, 5, -22);
            }
           // SystemNavigationManager.GetForCurrentView().BackRequested += WhatsNewContentPage_BackRequested;
        }

        private void WhatsNewContentPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            FrameNavigationService.GoBack();
            e.Handled = true;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
           // if (FrameNavigationService.CurrentFrame.CanGoBack)
           // {
           //     SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
           //AppViewBackButtonVisibility.Visible;
           // }
           // else
           // {
           //     SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
           //AppViewBackButtonVisibility.Collapsed;
           // }
            var info = e.Parameter as SampleInfo;
            this.DataContext = info;
            FrameNavigationService.Header.Text = info.Header;
            FrameNavigationService.Main.ListSelection = -1;
            Type type = Type.GetType(info.SampleView.ToString());
            this.viewContent.Content = Activator.CreateInstance(type) as FrameworkElement;
             
        }
    }
}
