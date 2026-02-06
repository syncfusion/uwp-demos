#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Common
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static bool NavigationFromWhatsNew;
        public SplitView MainSpiltView { get; set; }
        public Frame MainFrame { get; set; }

        public int ListSelection
        {
            get { return (int)GetValue(ListSelectionProperty); }
            set { SetValue(ListSelectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListSelection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListSelectionProperty =
            DependencyProperty.Register("ListSelection", typeof(int), typeof(MainPage), new PropertyMetadata(-1,new PropertyChangedCallback(OnListSelectionChanged)));

        private static void OnListSelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           int a = (int)e.NewValue;
           MainPage main = d as MainPage;
           main.SpiltViewPaneListBox.SelectedIndex = a;
            if (a == 0)
                FrameNavigationService.Header.Text = "Controls";
            else if (a == 1)
                FrameNavigationService.Header.Text = "Whats New";
            else if (a == 2)

            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                FrameNavigationService.Header.Text = SampleHelper.HeaderTextDesktop;
            else
                FrameNavigationService.Header.Text = SampleHelper.HeaderTextMobile;
        }

    
        public MainPage()
        {
            this.InitializeComponent();
            SampleHelper.DataViewModel = new ViewModel();
            FrameNavigationService.CurrentFrame = this.MyFrame;
            FrameNavigationService.BackButton = this.backbutton;
            FrameNavigationService.Main = this;
            FrameNavigationService.Listbox = this.SpiltViewPaneListBox;
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                FrameNavigationService.Header = this.layoutheader;
                FrameNavigationService.Arrow = this.arrow;
                FrameNavigationService.SampleHeader = this.sampleheader;
            }
            else
            {
                FrameNavigationService.Header = this.title;
                FrameNavigationService.Headergrid = this.grid;
                FrameNavigationService.SplitView = this.MySplitView;
            }
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;

            FrameNavigationService.FullScreenButton = this.fullscreenbutton;
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.White;
                //ApplicationView.GetForCurrentView().TitleBar.ButtonHoverBackgroundColor = Colors.FromArgb(255, 58, 159, 221);
                //ApplicationView.GetForCurrentView().TitleBar.ButtonHoverForegroundColor = Colors.White;
                ApplicationView.GetForCurrentView().TitleBar.ButtonForegroundColor = Colors.Black;
                ApplicationView.GetForCurrentView().TitleBar.BackgroundColor = Colors.White;
                ApplicationView.GetForCurrentView().TitleBar.ForegroundColor = Colors.Black;
                 ApplicationView.GetForCurrentView().TitleBar.ForegroundColor = Colors.Black;
            }
            else
            {
                ApplicationView view = ApplicationView.GetForCurrentView();
                view.TryEnterFullScreenMode();
            }
            this.DataContext = SampleHelper.DataViewModel;

            if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
            {
                DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            }


        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (FrameNavigationService.CurrentFrame.CanGoBack)
                e.Handled = true;
           
            if (FrameNavigationService.IsSettingOpen)
             {
                FrameNavigationService.Main.Margin = new Thickness(0, 0, 0, 0);
                if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
                {
                    FrameNavigationService.IsSettingOpen = false;
                    FrameNavigationService.SelectedSampleLayout = null;
                    if (FrameNavigationService.CurrentFrame != null)
                    {
                        if (FrameNavigationService.IsInWhatsNewSample)
                        {
                            FrameNavigationService.currentFrame.Margin = new Thickness(0, 39, -5, 0);
                            ((FrameNavigationService.SampleLayout) as SampleLayout).SetOptionGridMargin();
                        }
                        else
                        {
                            FrameNavigationService.CurrentFrame.Margin = new Thickness(0, 39, 0, 0);
                        }
                    }
                }
                ((FrameNavigationService.SampleLayout) as SampleLayout).FontIcons = "\uE713";
                FrameNavigationService.Headergrid.Visibility = Visibility.Visible;
                ((FrameNavigationService.SampleLayout) as SampleLayout).Pathstring = "M16.153984,12.087952C13.92602,12.087952 12.113032,13.844971 12.113032,16.003967 12.113032,18.164001 13.92602,19.921997 16.153984,19.921997 18.382011,19.921997 20.195,18.164001 20.195,16.003967 20.195,13.844971 18.382011,12.087952 16.153984,12.087952z M16.153984,10.087952C19.484978,10.087952 22.194998,12.741943 22.194998,16.003967 22.194998,19.267944 19.484978,21.921997 16.153984,21.921997 12.822992,21.921997 10.113033,19.267944 10.113033,16.003967 10.113033,12.741943 12.822992,10.087952 16.153984,10.087952z M14.195978,1.9999999L14.195978,5.4509887 13.437006,5.638977C11.470027,6.1249999,9.6860313,7.1269531,8.2780113,8.5379638L7.7300382,9.086975 4.6240215,7.2529907 2.7770376,10.232971 5.8810401,12.066956 5.6390357,12.824951C5.3129859,13.852966 5.1470313,14.921997 5.1470313,16.003967 5.1470313,17.093994 5.3190284,18.180969 5.6580176,19.234985L5.9009986,19.991943 2.8120105,21.836975 4.6780376,24.80896 7.7570157,22.964966 8.3080406,23.511963C9.7089806,24.899963,11.48199,25.888,13.437006,26.370972L14.195978,26.55896 14.195978,30 17.804008,30 17.804008,26.613953 18.590019,26.441956C20.624014,25.995972,22.473989,25.004944,23.939993,23.574951L24.48699,23.040955 27.374989,24.743957 29.223988,21.764954 26.364979,20.08197 26.619007,19.315979C26.978992,18.231995 27.160999,17.116943 27.160999,16.003967 27.160999,14.869995 26.97002,13.736999 26.593983,12.636963L26.332996,11.869995 29.191029,10.161987 27.324024,7.1919555 24.433035,8.9179687 23.882986,8.3880004C22.424978,6.9839477,20.595022,6.0099487,18.590996,5.5699462L17.804008,5.3989868 17.804008,1.9999999z M12.195979,0L19.804008,0 19.804008,3.8149413C21.613028,4.3279418,23.282034,5.2199706,24.699026,6.4299926L27.976978,4.4729614 31.971972,10.829956 28.726002,12.770996C29.015003,13.832947 29.160999,14.916992 29.160999,16.003967 29.160999,17.065979 29.022999,18.125 28.749012,19.164978L32.001999,21.078979 28.043995,27.460998 24.767996,25.527954C23.337027,26.765991,21.64501,27.675964,19.804008,28.197998L19.804008,32 12.195979,32 12.195979,28.100952C10.445003,27.555969,8.8350182,26.656982,7.4730191,25.465942L4.0259991,27.528992 0.030029264,21.168945 3.5320418,19.076965C3.2770371,18.066956 3.1470323,17.036987 3.1470323,16.003967 3.1470323,14.983948 3.2709947,13.974976 3.5150135,12.991943L0,10.915955 3.9550152,4.5359496 7.4320035,6.5879516C8.8030357,5.3729858,10.428035,4.4599609,12.195979,3.9089965z";
               ((FrameNavigationService.SampleLayout) as SampleLayout).OptionVisibility = Visibility.Collapsed;
                FrameNavigationService.IsSettingOpen = false;
                return;
            }
            FrameNavigationService.GoBack();

            if (this.searchtextbox.Text != String.Empty)
            {
                this.searchtextbox.Text = string.Empty;
                if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                {                   
                    FrameNavigationService.GoToFrame();
                }
            }

            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                if (searchtextbox.FocusState == FocusState.Pointer)
                {
                    searchtextbox.Padding = new Thickness(8, 0, 0, 0);
                }
                else if (searchtextbox.FocusState == FocusState.Unfocused)
                {
                    searchtextbox.Padding = new Thickness(8, 8, 0, 0);
                }
            }
            isEnterPressed = false;
            GC.Collect();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

          
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                if (SampleHelper.DataViewModel.AllProductsCategories.Count > 0)
                {
                    if (SampleHelper.DataViewModel.SidePanelVisibility == Visibility.Collapsed)
                    {
                        this.HamBurgerButton.Visibility = this.allcontrolbutton.Visibility = Visibility.Collapsed;
                        this.MySplitView.CompactPaneLength = 0;
                    }
                    MySplitView.Content = FrameNavigationService.CurrentFrame;

                    if (SampleHelper.DataViewModel.AllProductsCategories.Count == 1)
                    {
                        foreach (var item in SampleHelper.DataViewModel.AllProductsCategories)
                        {
                            if (item.Value.AllProducts.Count == 1)
                            {
                                foreach (var product in item.Value.AllProducts)
                                {
                                    FrameNavigationService.CurrentFrame.Navigate(typeof(SampleContent), product.Value);
                                    return;
                                }

                            }
                        }
                    }

                    FrameNavigationService.CurrentFrame.Navigate(typeof(SamplePage), this);
                  
                }
                else if (SampleHelper.DataViewModel.WhatsNewVisibility == Visibility.Visible && !(SampleHelper.DataViewModel.ShowcaseVisibility == Visibility.Visible))
                    FrameNavigationService.CurrentFrame.Navigate(typeof(WhatsNewPage), this);
                else if(SampleHelper.DataViewModel.ShowcaseVisibility == Visibility.Visible)
                    FrameNavigationService.CurrentFrame.Navigate(typeof(ShowcaseHome), this);
            }
            else
            {

                if (SampleHelper.DataViewModel.AllProductsCategories.Count > 0)
                {
                    if (this.HamBurgerButton != null && this.allcontrolbutton != null)
                        this.HamBurgerButton.Visibility = this.allcontrolbutton.Visibility = Visibility.Collapsed;
                    this.MySplitView.CompactPaneLength = 0;
                    MySplitView.Content = FrameNavigationService.CurrentFrame;

                    if (SampleHelper.DataViewModel.AllProductsCategories.Count == 1)
                    {
                        foreach (var item in SampleHelper.DataViewModel.AllProductsCategories)
                        {
                            if (item.Value.AllProducts.Count == 1)
                            {
                                foreach (var product in item.Value.AllProducts)
                                {
                                    if((product.Value as FeatureSampleCategory).AllSampleCategory.Count==1)
                                    {
                                        foreach (var sam1 in (product.Value as FeatureSampleCategory).AllSampleCategory)
                                        {
                                            if((sam1.Value as FeatureSampleCollection).AllSamples.Count==1)
                                            {
                                                FrameNavigationService.CurrentFrame.Navigate(typeof(MobileSamplePage), (sam1.Value as FeatureSampleCollection).AllSamples);
                                            }
                                            else
                                            {
                                                FrameNavigationService.CurrentFrame.Navigate(typeof(SampleContent), product.Value );
                                            }
                                        }

                                    }
                                    else
                                    {
                                        FrameNavigationService.CurrentFrame.Navigate(typeof(SampleContent), product.Value);
                                    }
                                    break;
                                }

                            }

                        }
                    }

                    FrameNavigationService.CurrentFrame.Navigate(typeof(ControlsHome), this);

                }
                else if (SampleHelper.DataViewModel.WhatsNewVisibility == Visibility.Visible && !(SampleHelper.DataViewModel.ShowcaseVisibility == Visibility.Visible))
                    FrameNavigationService.CurrentFrame.Navigate(typeof(WhatsNewPage), this);
                else
                    FrameNavigationService.CurrentFrame.Navigate(typeof(ShowcaseHome), this);
                this.WhatsNewButton.Visibility = SampleHelper.DataViewModel.WhatsNewVisibility;
                this.ShowCaseButton.Visibility = SampleHelper.DataViewModel.ShowcaseVisibility;


            }

        }

        public void backvisibility()
        {
            this.backbutton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void AllControlButtonvisibility(bool visibility)
        {
            if (!visibility)
                this.allcontrolbutton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            else 
                this.allcontrolbutton.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        internal string temp;
        internal bool isEnterPressed = false;
        private void SpiltViewPaneListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearSearchText();
            var listBox = sender as ListBox;
            //FrameNavigationService.IsDirectNavigation = false;
            if (ListSelection != SpiltViewPaneListBox.SelectedIndex)
            {
               if(FrameNavigationService.CurrentFrame == null && DeviceFamily.GetDeviceFamily() == Devices.Mobile)
                {
                    if(listBox.SelectedIndex == 0)
                    {
                        FrameNavigationService.IsInFeatureSample = true;
                    }
                }
                if (FrameNavigationService.CurrentFrame == null)
                    return;
               
                if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                {
                    MySplitView.Content = FrameNavigationService.CurrentFrame;
                    MainSpiltView = MySplitView;

                    if (listBox.SelectedIndex == 0)
                    {
                        this.layoutheader.Text = "Controls";
                        //FrameNavigationService.GoToFrame();
                        this.allcontrolbutton.Visibility = Visibility.Collapsed;
                        FrameNavigationService.CurrentFrame.Navigate(typeof(SamplePage), this);
                        if (MySplitView.IsPaneOpen)
                            MySplitView.IsPaneOpen = false;
                       
                        // this.backbutton.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    }
                    else if (listBox.SelectedIndex == 1)
                    {
                        this.allcontrolbutton.Visibility = Visibility.Visible;
                        this.layoutheader.Text = "Whats New";
                        //FrameNavigationService.GoToFrame();
                        FrameNavigationService.CurrentFrame.Navigate(typeof(WhatsNewPage), this);
                        if (MySplitView.IsPaneOpen)
                            MySplitView.IsPaneOpen = false;
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                        //this.backbutton.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    }

                  
                    else if (listBox.SelectedIndex == 2)
                    {
                        this.layoutheader.Text = "Essential Studio® UWP - 2025 Volume 4 ";
                        this.allcontrolbutton.Visibility = Visibility.Visible;
                        //FrameNavigationService.GoToFrame();
                        FrameNavigationService.CurrentFrame.Navigate(typeof(ShowcaseHome), this);
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                        if (MySplitView.IsPaneOpen)
                            MySplitView.IsPaneOpen = false;

                    }
                }
                else
                {
                    if (listBox.SelectedIndex == 0)
                    {

                        FrameNavigationService.Header.Text = "CONTROLS";
                        FrameNavigationService.IsInFeatureSample = true;
                        FrameNavigationService.IsInWhatsNewSample = false;

                        FrameNavigationService.CurrentFrame.Navigate(typeof(ControlsHome), this);
                        if (MySplitView.IsPaneOpen)
                            MySplitView.IsPaneOpen = false;

                    }

                    else if (listBox.SelectedIndex == 1)
                    {
                        FrameNavigationService.IsInFeatureSample = false;
                        FrameNavigationService.Header.Text = "WHATS NEW";
                        FrameNavigationService.IsInWhatsNewSample = true;

                        FrameNavigationService.CurrentFrame.Navigate(typeof(WhatsNewPage), this);
                        if (MySplitView.IsPaneOpen)
                            MySplitView.IsPaneOpen = false;
                    }
                    else if (listBox.SelectedIndex == 2)
                    {
                        FrameNavigationService.IsInFeatureSample = false;
                        FrameNavigationService.CurrentFrame.Navigate(typeof(ShowcaseHome), this);
                        FrameNavigationService.IsInWhatsNewSample = false ;
                        if (MySplitView.IsPaneOpen)
                            MySplitView.IsPaneOpen = false;
                    }
                    if (!MySplitView.IsPaneOpen)
                    {
                        //SpiltViewPaneListBox.SelectedIndex = -1;
                        //ListSelection = -1;
                    }

                }
            }
        }

     
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

            if(MySplitView.IsPaneOpen)
                MySplitView.Content.Opacity=0.4;
            ClearSearchText();
        }

      
        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
           
            if(DeviceFamily.GetDeviceFamily()==Devices.Mobile)
            {
                searchtextbox.Text = string.Empty;
                searchtextbox.Visibility = Visibility.Collapsed;
                search.Visibility = Visibility.Visible;
                title.Visibility = Visibility.Visible;
                button.Visibility = Visibility.Visible;
            }
            else
            {
                if(string.IsNullOrEmpty(searchtextbox.Text))
                searchtextbox.Padding = new Thickness(8, 8, 0, 0);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FrameNavigationService.CurrentFrame.Navigate(typeof(SamplePage), this);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (this.searchtextbox.Text == string.Empty)
            {
                this.searchtextbox.Focus(FocusState.Keyboard);
            }
            else
            {
                this.layoutheader.Text = "Results for \"" + this.searchtextbox.Text + "\"";
                FrameNavigationService.CurrentFrame.Navigate(typeof(SearchView), this.searchtextbox.Text);
            }
        }

        private void searchtextbox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key==Windows.System.VirtualKey.Enter)
            {
                isEnterPressed = true;
                if (this.searchtextbox.Text == string.Empty)
                {
                    this.searchtextbox.Focus(FocusState.Keyboard);
                }
                else
                {
                    if (DeviceFamily.GetDeviceFamily() == Devices.Desktop && this.layoutheader.Text != "Results for \"" + this.searchtextbox.Text + "\"")
                    {
                        this.layoutheader.Text = "Results for \"" + this.searchtextbox.Text + "\"";
                        FrameNavigationService.CurrentFrame.Navigate(typeof(SearchView), this.searchtextbox.Text);
                    }
                    if(DeviceFamily.GetDeviceFamily()==Devices.Mobile)
                    {
                        FrameNavigationService.IsInFeatureSample = false;
                        FrameNavigationService.IsInWhatsNewSample = false;
                        FrameNavigationService.GoToFrame();
                        FrameNavigationService.CurrentFrame.Navigate(typeof(SearchView), this.searchtextbox.Text);
                        this.searchtextbox.Text = string.Empty;
                        searchtextbox.Visibility = Visibility.Collapsed;
                        search.Visibility = Visibility.Visible;
                        title.Visibility = Visibility.Visible;
                        button.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void allcontrolsbutton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
           Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Hand, 1);
        }

        private void allcontrolsbutton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 1);
        }

        private void backbutton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationService.GoBack();
        }

        public void EnableFullScreenButton(bool isEnabled)
        {
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                if (isEnabled)
                {
                    this.fullscreenbutton.Visibility = Visibility.Visible;
                    this.searchcontent.Margin = new Thickness(0, 0, 46, 0);

                }
                else
                {
                    CheckFullPage(false);
                    this.fullscreenbutton.Visibility = Visibility.Collapsed;
                    this.searchcontent.Margin = new Thickness(0);
                }
            }
        }

        internal void ClearSearchText()
        {
            if (searchtextbox != null && !isEnterPressed)
            {
                searchtextbox.Text = string.Empty;
                searchtextbox.Padding = new Thickness(8, 8, 0, 0);
            }
        }

        public void CheckFullPage(bool isFullScreen)
        {
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                if (isFullScreen)
                {
                    this.titlebar.Visibility = Visibility.Collapsed;
                    this.MyFrame.Margin = new Thickness(0, -42, 0, 0);
                    this.MySplitView.CompactPaneLength = 0;
                    if (FrameNavigationService.SampleLayout != null && FrameNavigationService.SampleLayout as SampleLayout != null)
                        (FrameNavigationService.SampleLayout as SampleLayout).FontIcons = null;
                }
                else
                {
                    if (FrameNavigationService.SampleLayout != null && FrameNavigationService.SampleLayout as SampleLayout != null)
                        (FrameNavigationService.SampleLayout as SampleLayout).FontIcons = temp;
                    this.titlebar.Visibility = Visibility.Visible;
                    this.MyFrame.Margin = new Thickness(0, 0, 0, 0);
                    if(SampleHelper.DataViewModel.SidePanelVisibility != Visibility.Collapsed)
                    this.MySplitView.CompactPaneLength = 48;
                    temp = null;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(temp == null && FrameNavigationService.SampleLayout != null && FrameNavigationService.SampleLayout as SampleLayout != null)
            temp = (FrameNavigationService.SampleLayout as SampleLayout).FontIcons;
            CheckFullPage(FrameNavigationService.IsFullScreen = !FrameNavigationService.IsFullScreen);
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            searchtextbox.Visibility = Visibility.Visible;
            search.Visibility = Visibility.Collapsed;
            title.Visibility = Visibility.Collapsed;
            button.Visibility = Visibility.Collapsed;
            if (MySplitView.IsPaneOpen)
                MySplitView.IsPaneOpen = false;
            this.searchtextbox.Focus(FocusState.Keyboard);
            FrameNavigationService.IsInFeatureSample = false;
            FrameNavigationService.IsInWhatsNewSample = false;
        }

        private void search_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void MySplitView_PaneClosed(SplitView sender, object args)
        {
            MySplitView.Content.Opacity = 1;
        }

        private void searchtextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MySplitView.IsPaneOpen)
                MySplitView.IsPaneOpen = false;
            searchtextbox.Padding = new Thickness(8, 0, 0, 0);
        }
        private void ControlsTileButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            isEnterPressed = false;
            ClearSearchText();
        }

        private void WhatsNewButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            isEnterPressed = false;
            ClearSearchText();
        }

        private void ShowCaseButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            isEnterPressed = false;
            ClearSearchText();
        }

    }
}
