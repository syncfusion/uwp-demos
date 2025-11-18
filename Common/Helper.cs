#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Common
{

    public enum Categories
    {
        Grids,
        DataVisualization,
        Layout,
        Editors,
        Navigation,
        Notification,
        FileFormat,
        Reporting,
        BusinessIntelligence,
        DataScience,
        Miscellaneous,

    }
    public enum Devices
    {
        Desktop,
        Mobile
    }
    public enum Tags
    {
        None,
        Preview,
        Updated,
        New,
        PreviewWithWhatsNew,
        UpdatedWithWhatsNew,
        NewWithWhatsNew
    }
    public enum SampleType
    {
        Showcase,
        Featured
    }
    public static class DeviceFamily
    {
        public static Devices GetDeviceFamily()
        {
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                return Devices.Mobile;
            return Devices.Desktop;
        }

        public static object GetSampleViewFromAssembly(string assemblyName)
        {
            return null;
        }
    }
    public static class FrameNavigationService
    {

        public static Button BackButton;
        public static TextBlock Arrow; 
        public static Button FullScreenButton;
        public static TextBlock SampleHeader;
        public static bool IsInFeatureSample = false;
        public static bool IsInWhatsNewSample = false;
        public static ListBox Listbox;
        public static bool IsSettingOpen;
        public static SampleLayout SelectedSampleLayout;
        public static Windows.UI.Xaml.Shapes.Path Path;
        public static UserControl SampleLayout;
        public static double ControlScrollOffset = 0;
        public static double ShowcaseScrollOffset = 0;
        public static double WhatsnewScrollOffset = 0;
        public static double SampleScrollOffset = 0;
        public static Grid Headergrid;
        public static MainPage Main;
        public static object SampleDataContext;
        public static TextBlock HeaderTextBlock;
        public static bool IsDirectNavigation;

        public static string SearchString;

        public static SplitView SplitView;
        public static TextBlock Header;

        public static bool IsFullScreen;

        public static int SelectedIndex;

        public static Frame currentFrame;
        public static Frame CurrentFrame
        {
            get
            {
                return currentFrame;
            }
            set
            {
                currentFrame = value;
            }
        }

        public static void GoToFrame()
        {
            while(CurrentFrame.CanGoBack)
            {
                CurrentFrame.GoBack();
            }
            if (!CurrentFrame.CanGoBack)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

        }

        public static void GoBack()
        {


            if (CurrentFrame.CanGoBack)
            {
                CurrentFrame.GoBack();
            }

            if (!CurrentFrame.CanGoBack)
            {
                if (BackButton != null)
                    BackButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }

        public static void GoForward()
        {
            CurrentFrame.GoForward();
        }
    }
    public class SampleInfo: NotificationObject
    {

        public Categories Category = Categories.Grids;

        public string Product { get; set; }

        public string SampleCategory = "Overview";

        public string Header { get; set; }

        public object SampleView { get; set; }

        public SampleType SampleType = SampleType.Featured;

        public bool HasOptions = false;

        public Tags Tag { get; set; }

        public string ProductIcons { get; set; }

        public string Description { get; set; }

        private string desktopImage;

        public string DesktopImage
        {
            get { return desktopImage; }
            set
            {
                desktopImage = value;
                
                if(this.MobileImage == null)
                {
                    this.MobileImage = value;
                }
            }
        }

        public string MobileImage { get; set; }

        public string[] SearchKeys { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }
        //Added specific to handle the UIElements visibility for Mobile View sample navigation.
        public object SamplePage { get; set; }

    }
    public static class SampleHelper
    {
        public static List<SampleInfo> SampleViews = new List<SampleInfo>();

        public static ViewModel DataViewModel;
        public static string UWPText = "Comprehensive suite of over 70 components including charts, grids, gauges, maps, diagrams and much more. All the components render adaptively based on the current device family that it is being rendered on";
        public static string HeaderTextDesktop = "Essential Studio® for UWP - 2025 Volume 3";
        public static string HeaderTextMobile = "Essential Studio® for UWP";
        internal static List<string> NewProduct { get; set; }
        internal static List<string> PreviewProduct { get; set; }
        internal static List<string> UpdatedProduct { get; set; }

        public static void SetTagsForProduct(string product, Tags tag)
        {

            if (tag == Tags.New || tag == Tags.NewWithWhatsNew)
            {
                if (NewProduct == null)
                    NewProduct = new List<string>();
                NewProduct.Add(product);
            }
            else if (tag == Tags.Preview || tag == Tags.PreviewWithWhatsNew)
            {
                if (PreviewProduct == null)
                    PreviewProduct = new List<string>();
                PreviewProduct.Add(product);
            }
            else if (tag == Tags.Updated || tag == Tags.UpdatedWithWhatsNew)
            {
                if (UpdatedProduct == null)
                    UpdatedProduct = new List<string>();
                UpdatedProduct.Add(product);
            }
        }

    }
    public class VisualTreeHelperExtension
    {
        public static IEnumerable<T> FindVisualChildrenOfType<T>(DependencyObject parent)
       where T : DependencyObject
        {
            List<T> foundChildren = new List<T>();
            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;
                if (childType == null)
                {
                    foreach (var other in FindVisualChildrenOfType<T>(child))
                        yield return other;
                }
                else
                {
                    yield return (T)child;
                }
            }
        }
    }
}
