#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.UI.Xaml.Controls.Input;
using Syncfusion.UI.Xaml.Controls.Layout;
using Syncfusion.UI.Xaml.NavigationDrawer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.System.Profile;
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
    public sealed partial class ControlsHome : Page
    {
        public static  SplitView MySpiltView { get; set; }

        private FeatureSampleCategory SelectedItem;

    

        public static MainPage Main { get; set; }

        public List<Samples> SamplesCollection = new List<Samples>();

        public ControlsHome()
        {
            this.InitializeComponent();
            this.DataContext = SampleHelper.DataViewModel;
            //SystemNavigationManager.GetForCurrentView().BackRequested += ControlsHome_BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            IsNavigatedFrom = false;
            Main = e.Parameter as MainPage;
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                Main.ListSelection = 0;
            else
            {
                Main.ListSelection = 0;
                FrameNavigationService.Header.Text = "Controls";
            }

            FrameNavigationService.Main.ListSelection = 0;
            //   MySpiltView = (e.Parameter as MainPage).MainSpiltView;

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
            this.scroll.ScrollToVerticalOffset(FrameNavigationService.ControlScrollOffset);
        }

        private void ControlsHome_BackRequested(object sender, BackRequestedEventArgs e)
        {

              FrameNavigationService.GoBack();
            
            e.Handled = true;    
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SelectedItem = e.AddedItems[0] as FeatureSampleCategory;
                foreach (Products p in SampleHelper.DataViewModel.AllProductsCategories.Values)
                {
                    foreach (FeatureSampleCategory category in p.AllProducts.Values)
                    {
                        if (category.IsSelected && category != e.AddedItems[0] as FeatureSampleCategory)
                        {
                            category.IsSelected = false;
                            break;
                        }
                    }
                }
            }
            if (e.RemovedItems.Count > 0 && (!IsNavigatedFrom))
            {
                if(SelectedItem != null)
                SelectedItem.IsSelected = true;
            }
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                if (SelectedItem != null)
                    this.SampleContent.DataContext = SelectedItem;
            }
            else
            {
                FrameNavigationService.Header.Text = (e.AddedItems[0] as FeatureSampleCategory).Name;
                FrameNavigationService.CurrentFrame.Navigate(typeof(SampleContent), e.AddedItems[0]);
            }

        }

        private bool IsNavigatedFrom = false;
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
           
            IsNavigatedFrom = true;
            foreach (Products p in SampleHelper.DataViewModel.AllProductsCategories.Values)
            {
                foreach (FeatureSampleCategory category in p.AllProducts.Values)
                {
                    if (category.IsSelected)
                    {
                        category.IsSelected = false;
                        break;
                    }
                }
            }
            base.OnNavigatedFrom(e);
        }

        private bool fullView = false;
        public bool FullView
        {
            get
            {
                return fullView;
            }
            set
            {
                fullView = value;
                if (value)
                    this.treeview.Visibility = this.seperator.Visibility = Visibility.Collapsed;
                else
                    this.treeview.Visibility = this.seperator.Visibility = Visibility.Visible;
            }
        }

        private void scroll_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            FrameNavigationService.ControlScrollOffset = this.scroll.VerticalOffset;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.scroll.ScrollToVerticalOffset(FrameNavigationService.ControlScrollOffset);
        }
    }
    public class CustomListBox : ListBox
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            if (item is Tile )
            {
                var binding = new Binding
                {
                    Source = item,
                    Path = new PropertyPath("IsSelected"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };

                ((ListBoxItem)element).SetBinding(ListBoxItem.IsSelectedProperty, binding);
            }
        }
    }
}