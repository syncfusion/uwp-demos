using Common;
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

namespace Common_2015
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
           // SystemNavigationManager.GetForCurrentView().BackRequested += SearchPage_BackRequested;
            this.DataContextChanged += SearchPage_DataContextChanged;
         
        }


        private void SearchPage_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            this.maingrid.Children.Clear();
           
        }

        private void SearchPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            FrameNavigationService.GoBack();
            e.Handled = true;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FrameNavigationService.Main.EnableFullScreenButton(true);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            // FrameNavigationService.BackButton.Visibility = Visibility.Visible;
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
            base.OnNavigatedTo(e);
        }
    }
    
}
