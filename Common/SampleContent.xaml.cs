#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Controls.Layout;
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
    public sealed partial class SampleContent : Page , IDisposable
    {
        
        public SampleContent()
        {
            this.InitializeComponent();
            if(DeviceFamily.GetDeviceFamily()==Devices.Desktop)
            FrameNavigationService.FullScreenButton.Click += FullScreenButton_Click;
            // SystemNavigationManager.GetForCurrentView().BackRequested += SampleContent_BackRequested;
            this.Loaded += SampleContent_Loaded;
            this.Unloaded += (sender, e) =>
            {
                this.Loaded -= SampleContent_Loaded;
                DataContext = null;
                try
                {                   
                    Dispose();                   
                }
                catch
                {
                    GC.Collect();
                }                
            };
        }

       

        ~SampleContent()
        {
            GC.Collect();
        }

        private void SampleContent_Loaded(object sender, RoutedEventArgs e)
        {
            if (FrameNavigationService.IsDirectNavigation && DeviceFamily.GetDeviceFamily() == Devices.Mobile)
            {
                FrameNavigationService.IsDirectNavigation = false;
                FrameNavigationService.GoBack();
                return;
            }
            
            if (CategoryAccordion.Margin.Top != 0)
            {
                foreach (AccordionButton button in VisualTreeHelperExtension.FindVisualChildrenOfType<AccordionButton>(CategoryAccordion))
                {
                    if (button.Name == "ExpanderButton")
                    {
                        button.Height = 0.0;
                        CategoryAccordion.Margin = new Thickness(0.0);
                        break;
                    }
                }
                if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
                {
                    var allSample = new List<SampleInfo>();
                    FrameNavigationService.SampleDataContext = this.DataContext;
                    foreach (var item in (this.DataContext as FeatureSampleCategory).AllSampleCategory)
                    {
                        var samples = item.Value;
                        foreach (var sam in (samples as FeatureSampleCollection).AllSamples)
                        {
                            allSample.Add(sam);
                        }

                    }

                    if (allSample.Count > 1)
                        return;
                    FrameNavigationService.SelectedIndex = 0;


                    FrameNavigationService.CurrentFrame.Navigate(typeof(MobileSamplePage), allSample);
                }
            }
        }

        private void SampleContent_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
               FrameNavigationService.GoBack();
            e.Handled = true;
        }

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            if(FrameNavigationService.IsFullScreen)
            {
                this.treeview.Visibility = Visibility.Collapsed;
            }
            else

            {
                this.treeview.Visibility = Visibility.Visible;
            }
        }
        private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender != lastselecteditem)
            {
                if(lastselecteditem is CustomListBox)
                {
                    (lastselecteditem as CustomListBox).SelectedIndex = -1;
                }
            }
            lastselecteditem = sender;

            for (int i = 0; i < contentgrid.Children.Count; i++)
            {
                object content = contentgrid.Children[i];
                //if (content is IDisposable)
                //    (content as IDisposable).Dispose();
                content = null;
            }
            contentgrid.Children.Clear();
            GC.Collect();
            if ((sender as ListBox).SelectedItem != null)
            {
                if (DeviceFamily.GetDeviceFamily() != Devices.Mobile && busyIndicator != null)
                    busyIndicator.IsBusy = true;
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, (() =>
                {
                    Type type = Type.GetType(((sender as ListBox).SelectedItem as SampleInfo).SampleView.ToString());
                    if (type != null && DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                        contentgrid.Children.Add(Activator.CreateInstance(type) as FrameworkElement);
                    if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                    {
                        FrameNavigationService.SampleHeader.Text = ((sender as ListBox).SelectedItem as SampleInfo).Header;
                        FrameNavigationService.SampleHeader.Visibility = Visibility.Visible;
                        FrameNavigationService.Arrow.Visibility = Visibility.Visible;
                    }
                }));
                if (DeviceFamily.GetDeviceFamily() != Devices.Mobile && busyIndicator != null)
                    busyIndicator.IsBusy = false;
            }


            var allSample = new List<SampleInfo>();
            var selectedindex = 0;
            if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
            {
                FrameNavigationService.SampleDataContext = this.DataContext;
                if (e.AddedItems.Count > 0)

                    foreach (var item in (this.DataContext as FeatureSampleCategory).AllSampleCategory)
                    {

                        var samples = item.Value;

                        foreach (var sam in (samples as FeatureSampleCollection).AllSamples)
                        {
                            allSample.Add(sam);
                            if (sam.Header == ((sender as ListBox).SelectedItem as SampleInfo).Header && sam.SampleCategory == ((sender as ListBox).SelectedItem as SampleInfo).SampleCategory)
                                selectedindex = allSample.Count - 1;
                        }

                    }
                FrameNavigationService.SelectedIndex = selectedindex;

                FrameNavigationService.CurrentFrame.Navigate(typeof(MobileSamplePage), allSample);
            }



        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FrameNavigationService.Main.EnableFullScreenButton(true);

            if (e.Parameter != null)
            {
                this.DataContext = e.Parameter;
                FrameNavigationService.SampleDataContext = this.DataContext;
                if(e.Parameter is FeatureSampleCategory)
                FrameNavigationService.Header.Text = ((e.Parameter as FeatureSampleCategory).Name);
            }
            else
            {
                if (FrameNavigationService.SampleDataContext != null)
                {
                    this.DataContext = FrameNavigationService.SampleDataContext;
                    FrameNavigationService.Header.Text = ((FrameNavigationService.SampleDataContext as FeatureSampleCategory).Name);
                }
            }
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                FrameNavigationService.Main.AllControlButtonvisibility(true);
            }
            this.DataContext = e.Parameter;
            FrameNavigationService.Main.ListSelection = -1;

            base.OnNavigatedTo(e);

        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            FrameNavigationService.Main.EnableFullScreenButton(false);
            base.OnNavigatingFrom(e);
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                FrameNavigationService.Header.Text = "Controls";
                FrameNavigationService.SampleHeader.Visibility = Visibility.Collapsed;
                FrameNavigationService.Arrow.Visibility = Visibility.Collapsed;
            }
            foreach (Products p in SampleHelper.DataViewModel.AllProductsCategories.Values)
            {
                foreach (FeatureSampleCategory category in p.AllProducts.Values)
                {
                    foreach (FeatureSampleCollection samples in category.AllSampleCategory.Values)
                    {
                        foreach (SampleInfo sample in samples.AllSamples)
                        {
                            if (sample.IsSelected)
                            {
                                sample.IsSelected = false;
                                break;
                            }
                        }
                    }
                }
            }
            Dispose();
            if (lastselecteditem is CustomListBox)
            {
                (lastselecteditem as CustomListBox).ItemsSource = null;
                (lastselecteditem as CustomListBox).DataContext = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        object lastselecteditem;

        bool customlistboxload;
        private void samplelist_Loaded(object sender, RoutedEventArgs e)
        {
            if (!customlistboxload)
                (sender as CustomListBox).SelectedIndex = 0;
            customlistboxload = true;
        }

        public void Dispose()
        {
            if(FrameNavigationService.FullScreenButton != null)
            FrameNavigationService.FullScreenButton.Click -= FullScreenButton_Click;
            this.Loaded -= SampleContent_Loaded;
            if (this.Resources != null)
            {
                this.Resources.MergedDictionaries.Clear();
                this.Resources.Clear();
                this.Resources = null;
            }
            for (int i = 0; i < contentgrid.Children.Count; i++)
            {
                object content = contentgrid.Children[i];
                //if (content is IDisposable)
                //    (content as IDisposable).Dispose();
                content = null;
            }

            this.CategoryAccordion = null;
            this.DataContext = null;
            if (lastselecteditem is CustomListBox)
            {
                lastselecteditem = null;
            }
            GC.Collect();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.scroll.ScrollToVerticalOffset(FrameNavigationService.SampleScrollOffset);
        }

        private void scroll_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            FrameNavigationService.SampleScrollOffset = this.scroll.VerticalOffset;
        }
    }
}
