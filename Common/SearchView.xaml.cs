#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
    public sealed partial class SearchView : Page
    {

        Dictionary<string, ResultByProduct> Results { get; set; }

        public SearchView()
        {
           
            this.InitializeComponent();
           // SystemNavigationManager.GetForCurrentView().BackRequested += SearchViewPage_BackRequested;
            if(DeviceFamily.GetDeviceFamily()==Devices.Desktop)
            FrameNavigationService.FullScreenButton.Click += FullScreenButton_Click;
            if(DeviceFamily.GetDeviceFamily()==Devices.Mobile)
            {
                FrameNavigationService.Main.ListSelection = 2;
                FrameNavigationService.Header.Text = "Controls";
            }

        }

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            if (FrameNavigationService.IsFullScreen)
            {
                this.treeview.Visibility = Visibility.Collapsed;
            }
            else

            {
                this.treeview.Visibility = Visibility.Visible;
            }
        }

        private void SearchViewPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            FrameNavigationService.GoBack();
        }

        string searchResult;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FrameNavigationService.Main.EnableFullScreenButton(true);
            FrameNavigationService.Main.ListSelection = -1;
            Results = new Dictionary<string, ResultByProduct>();

             searchResult = e.Parameter.ToString().ToLower();

            if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
            {
               
                FrameNavigationService.Header.Text = "Search Result";
            }
            foreach (var item in SampleHelper.DataViewModel.FeaturedSamples)
            {
                if (item.SearchKeys == null)
                    continue;

                foreach (var key in item.SearchKeys)
                {
                    if (key.ToLower().Contains(searchResult) || searchResult.Contains(key.ToLower()))
                    {
                        if (Results.ContainsKey(item.Product))
                        {
                            var currentProduct = Results[item.Product];
                            if(!currentProduct.SearchResults.Contains(item))
                            currentProduct.SearchResults.Add(item);
                        }
                        else
                        {
                            ResultByProduct newProduct = new ResultByProduct();
                            newProduct.Name = item.Product;
                            newProduct.SearchResults = new List<SampleInfo>();
                            newProduct.SearchResults.Add(item);
                            Results.Add(item.Product, newProduct);

                        }
                    }
                }
            }
            this.CategoryAccordion.ItemsSource = Results;
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
              //  FrameNavigationService.BackButton.Visibility = Visibility.Visible;
            }

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

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            FrameNavigationService.Main.EnableFullScreenButton(false);
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
        }

        private void SfTextBoxExt_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<SampleInfo> Result = new List<SampleInfo>();

            //if (!string.IsNullOrEmpty(SearchTextBox.Text))
            //{
            //    SearchListBoxext.Visibility = Visibility.Visible;
            //    foreach (SampleInfo item in viewmodel.SearchViewCollection)
            //    {
            //        for(int i=0;i< item.SearchKeys.Count();i++)
            //        {
            //            if (item.SearchKeys[i].ToLower().Contains(SearchTextBox.Text) && !Result.Contains(item))
            //                Result.Add(item);
            //        }
                   
            //    }
            //    if(Result.Count > 0)
            //    SearchListBoxext.ItemsSource = Result;
          //  }
        }

        private void SearchListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
       {
            ////viewContent.Visibility = Visibility.Visible;
            ////viewContent.Children.Clear();
            ////if (SearchListBoxext.SelectedIndex != -1)
            ////{
            ////    var info = (sender as ListBox).SelectedItem as SampleInfo;
            ////    this.DataContext = info;
            ////    Type type = Type.GetType(info.SampleView.ToString());
            ////    this.viewContent.Children.Add(Activator.CreateInstance(type) as FrameworkElement);

            ////}
           
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (lastselecteditem != null && sender != lastselecteditem)
                {
                    if (lastselecteditem is CustomListBox)
                    {
                        (lastselecteditem as CustomListBox).SelectedIndex = -1;
                    }
                }
               
                else if (lastselecteditem == null && DeviceFamily.GetDeviceFamily()==Devices.Desktop)
                    {
                        (sender as ListBox).SelectedIndex = 0;
                    }
                    lastselecteditem = sender;
              
                if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                {
                    this.viewContent.Children.Clear();
                    if ((sender as ListBox).SelectedItem != null)
                    {
                        Type type = Type.GetType(((sender as ListBox).SelectedItem as SampleInfo).SampleView.ToString());
                        if (type != null)
                            this.viewContent.Children.Add(Activator.CreateInstance(type) as FrameworkElement);
                    }
                }
                if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
                {
                    FrameNavigationService.SampleDataContext = this.DataContext;
                    if (e.AddedItems.Count > 0)
                        FrameNavigationService.CurrentFrame.Navigate(typeof(MobileSearchResultView), e.AddedItems[0]);
                }
            }
        }

        object lastselecteditem;

        bool customlistboxload;
        private void samplelist_Loaded(object sender, RoutedEventArgs e)
        {
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                if (!customlistboxload)
                    (sender as CustomListBox).SelectedIndex = 0;
                customlistboxload = true;
            }
        }

        private void CategoryAccordion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class ResultByProduct
    {
        public string Name { get; set; }

        public List<SampleInfo> SearchResults { get; set; }
    }
}
