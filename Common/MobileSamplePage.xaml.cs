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
    public sealed partial class MobileSamplePage : Page ,  IDisposable
    {
        public MobileSamplePage()
        {
            this.InitializeComponent();
           // SystemNavigationManager.GetForCurrentView().BackRequested += MabileSamplePage_BackRequested;
        }

        private void MabileSamplePage_BackRequested(object sender, BackRequestedEventArgs e)
        {

            FrameNavigationService.CurrentFrame.Navigate(typeof(SampleContent), FrameNavigationService.SampleDataContext);
            e.Handled = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.tabPivot.DataContext = e.Parameter;
            this.tabPivot.SelectedIndex = FrameNavigationService.SelectedIndex;
            var sampleInfo = (this.tabPivot.SelectedItem as SampleInfo);

            base.OnNavigatedTo(e);
        }
        private async void contentPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if(busyIndicator != null)
                busyIndicator.IsBusy = true;
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, (() =>
            {
                var sampleInfo = (this.tabPivot.SelectedItem as SampleInfo);
            Type type = Type.GetType(sampleInfo.SampleView.ToString());
            if (type != null)
                (sender as UserControl).Content = Activator.CreateInstance(type) as FrameworkElement;
            //Backed up the sample page specific to RichTextEditor to handle UI elements visibility while navigating between the samples.
                if (sampleInfo.Product == "RichTextEditor")
                    sampleInfo.SamplePage = (sender as UserControl).Content;
            }));
            if (busyIndicator != null)
                busyIndicator.IsBusy = false;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if(this.tabPivot.Items.Count == 1)
            {
                FrameNavigationService.IsDirectNavigation = true;
            }
            //Clear the backed sample page view while navigating to other page specific to RichTextEditor.
            if (this.tabPivot.Items.Count > 0)
            {
                foreach (object item in this.tabPivot.Items)
                {
                    SampleInfo sample = null;
                    if (item is SampleInfo)
                    {
                        sample = item as SampleInfo;
                        if (sample.Product == "RichTextEditor")
                        {
                            if (sample.SamplePage != null)
                                sample.SamplePage = null;
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            this.tabPivot.DataContext = null;
            //Unhook the selection changed event.
            this.tabPivot.SelectionChanged -= tabPivot_SelectionChanged;
        }
        /// <summary>
        /// Called when the selection si changed in pivot item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event data.</param>
        private void tabPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot pivot = sender as Pivot;
            SampleInfo sampleInfo = (pivot.SelectedItem as SampleInfo);

            if (FrameNavigationService.SelectedSampleLayout != null)
            {
                if (FrameNavigationService.SelectedSampleLayout.OptionVisibility == Visibility.Visible)
                {
                    FrameNavigationService.SelectedSampleLayout.OptionVisibility = Visibility.Collapsed;
                    FrameNavigationService.SelectedSampleLayout.Pathstring = "M16.153984,12.087952C13.92602,12.087952 12.113032,13.844971 12.113032,16.003967 12.113032,18.164001 13.92602,19.921997 16.153984,19.921997 18.382011,19.921997 20.195,18.164001 20.195,16.003967 20.195,13.844971 18.382011,12.087952 16.153984,12.087952z M16.153984,10.087952C19.484978,10.087952 22.194998,12.741943 22.194998,16.003967 22.194998,19.267944 19.484978,21.921997 16.153984,21.921997 12.822992,21.921997 10.113033,19.267944 10.113033,16.003967 10.113033,12.741943 12.822992,10.087952 16.153984,10.087952z M14.195978,1.9999999L14.195978,5.4509887 13.437006,5.638977C11.470027,6.1249999,9.6860313,7.1269531,8.2780113,8.5379638L7.7300382,9.086975 4.6240215,7.2529907 2.7770376,10.232971 5.8810401,12.066956 5.6390357,12.824951C5.3129859,13.852966 5.1470313,14.921997 5.1470313,16.003967 5.1470313,17.093994 5.3190284,18.180969 5.6580176,19.234985L5.9009986,19.991943 2.8120105,21.836975 4.6780376,24.80896 7.7570157,22.964966 8.3080406,23.511963C9.7089806,24.899963,11.48199,25.888,13.437006,26.370972L14.195978,26.55896 14.195978,30 17.804008,30 17.804008,26.613953 18.590019,26.441956C20.624014,25.995972,22.473989,25.004944,23.939993,23.574951L24.48699,23.040955 27.374989,24.743957 29.223988,21.764954 26.364979,20.08197 26.619007,19.315979C26.978992,18.231995 27.160999,17.116943 27.160999,16.003967 27.160999,14.869995 26.97002,13.736999 26.593983,12.636963L26.332996,11.869995 29.191029,10.161987 27.324024,7.1919555 24.433035,8.9179687 23.882986,8.3880004C22.424978,6.9839477,20.595022,6.0099487,18.590996,5.5699462L17.804008,5.3989868 17.804008,1.9999999z M12.195979,0L19.804008,0 19.804008,3.8149413C21.613028,4.3279418,23.282034,5.2199706,24.699026,6.4299926L27.976978,4.4729614 31.971972,10.829956 28.726002,12.770996C29.015003,13.832947 29.160999,14.916992 29.160999,16.003967 29.160999,17.065979 29.022999,18.125 28.749012,19.164978L32.001999,21.078979 28.043995,27.460998 24.767996,25.527954C23.337027,26.765991,21.64501,27.675964,19.804008,28.197998L19.804008,32 12.195979,32 12.195979,28.100952C10.445003,27.555969,8.8350182,26.656982,7.4730191,25.465942L4.0259991,27.528992 0.030029264,21.168945 3.5320418,19.076965C3.2770371,18.066956 3.1470323,17.036987 3.1470323,16.003967 3.1470323,14.983948 3.2709947,13.974976 3.5150135,12.991943L0,10.915955 3.9550152,4.5359496 7.4320035,6.5879516C8.8030357,5.3729858,10.428035,4.4599609,12.195979,3.9089965z";
                    FrameNavigationService.SelectedSampleLayout.FontIcons = "\uE713";
                }
            }

            if (sampleInfo == null)
                return;
            //Hanlded the App bar visibility of the page while navigating the samples within pivot.
            //The following code is specific to RichTextEditor.
            if (sampleInfo.Product == "RichTextEditor")
            {
                if (sampleInfo.Header == "Customized Editor")
                {
                    //Show the bottom app bar for Customized Editor sample.
                    if (sampleInfo.SamplePage is Page && (sampleInfo.SamplePage as Page).BottomAppBar is AppBar)
                        (sampleInfo.SamplePage as Page).BottomAppBar.Visibility = Visibility.Visible;
                }
                else
                {
                    foreach (object item in pivot.Items)
                    {
                        SampleInfo sample = null;
                        if (item is SampleInfo)
                        {
                            sample = item as SampleInfo;
                            if (sample.Header == "Customized Editor")
                            {
                                //Hides the bottom app bar of customized editor for other samples of RTE.
                                if (sample.SamplePage is Page && (sample.SamplePage as Page).BottomAppBar is AppBar)
                                    (sample.SamplePage as Page).BottomAppBar.Visibility = Visibility.Collapsed;
                            }
                        }  
                    }
                }
            }
        }
    }
}
