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
    public sealed partial class SamplePage : Page , IDisposable
    {
        ResourceDictionary res = new ResourceDictionary();
        public SamplePage()
        {
            this.InitializeComponent();
            this.DataContext = SampleHelper.DataViewModel;
            res.Source = new Uri("ms-appx:///Syncfusion.SampleBrowser.UWP.Common/Generic.xaml", UriKind.RelativeOrAbsolute) ;
            this.Resources.MergedDictionaries.Add(res);

            this.Loaded += SamplePage_Loaded;
            Window.Current.CoreWindow.PointerWheelChanged += CoreWindow_PointerWheelChanged;
        }

        private void CoreWindow_PointerWheelChanged(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.PointerEventArgs args)
        {
            if (args.CurrentPoint.Properties.MouseWheelDelta == (-120))
            {
                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + 50);
            }
            if (args.CurrentPoint.Properties.MouseWheelDelta == (120))
            {
                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - 50);

            }
        }

        private void SamplePage_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
   
        }

        private void SamplePage_Loaded(object sender, RoutedEventArgs e)
        {
            this.scroll.ScrollToHorizontalOffset(FrameNavigationService.ControlScrollOffset);
        }

        public static MainPage Main { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            FrameNavigationService.Header.Text = "Controls";
            Main = e.Parameter as MainPage;
            if (Main != null && DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                Main.ListSelection = 0;
                FrameNavigationService.Main.AllControlButtonvisibility(false);
            }
       
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            FrameNavigationService.ControlScrollOffset = this.scroll.HorizontalOffset;
            base.OnNavigatingFrom(e);
            Dispose();
        }


        public void Dispose()
        {
            this.Loaded -= SamplePage_Loaded;
            Window.Current.CoreWindow.PointerWheelChanged -= CoreWindow_PointerWheelChanged;
            this.Resources.MergedDictionaries.Remove(res);
            this.Resources.MergedDictionaries.Clear();
            this.Resources.Clear();
            this.Resources = null;
            this.gridview.DataContext = null;
            this.gridview = null;
            this.DataContext = null;

        }
    }
}
