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
    public sealed partial class ShowcasePage : Page
    {
        private string FrameHeader = null;
        public ShowcasePage()
        {
            this.InitializeComponent();
           // SystemNavigationManager.GetForCurrentView().BackRequested += ShowCasePage_BackRequested;
        }

        private void ShowCasePage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            FrameNavigationService.GoBack();
            e.Handled = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
            {
                FrameHeader = FrameNavigationService.Header.Text.ToString();
                FrameNavigationService.Header.Text = (e.Parameter as SampleInfo).Header;
                ShowcaseContent.Content = (Activator.CreateInstance(Type.GetType((e.Parameter as SampleInfo).SampleView.ToString()))) as UIElement;
                ShowcaseContent.Margin = new Thickness(5, 0, 5, 0);
                ShowCaseGrid.Visibility = Visibility.Visible;
            }
            else
            {
                FrameNavigationService.Main.CheckFullPage(true);
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
                FrameHeader = FrameNavigationService.Header.Text.ToString();
                FrameNavigationService.Header.Text = (e.Parameter as SampleInfo).Header;
                showcasePage.Content = (Activator.CreateInstance(Type.GetType((e.Parameter as SampleInfo).SampleView.ToString()))) as UIElement;              
            }
            base.OnNavigatedTo(e);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            FrameNavigationService.Main.CheckFullPage(false);
            FrameNavigationService.Header.Text = FrameHeader;
            showcasePage.Content = null;
            base.OnNavigatedFrom(e);
        }
    }
}
