#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Controls.Navigation;
using Syncfusion.UI.Xaml.NavigationDrawer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
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
    public sealed partial class ControlsPage : UserControl
    {
        public SfNavigationDrawer NavigationDrawer { get; set; }
        public List<string> ToolsControls { get; set; }

        public ControlsPage()
        {
            this.InitializeComponent();
            this.tabcontrol.SelectionChanged += Tabcontrol_SelectionChanged;
            this.DataContextChanged += new TypedEventHandler<FrameworkElement, DataContextChangedEventArgs>(OnDataContextChanged);
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if(DeviceFamily.GetDeviceFamily()==Devices.Mobile)
            {
                if (categoryselector.Items.Count > 0)
                    categoryselector.SelectedIndex = 0;
                if (categoryselector.Items.Count <= 1)
                    headergrid.Visibility = Visibility.Collapsed;
                else
                    headergrid.Visibility = Visibility.Visible;
            }
        }

        private void Tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (this.tabcontrol.SelectedItem as SfTabItem);
            if (selectedItem != null && selectedItem.Content != null)
            {
                Type type = Type.GetType(selectedItem.Content.ToString());
                if (type != null)
                    selectedItem.Content = Activator.CreateInstance(type) as FrameworkElement;
            }

        }

    


        private void categoryselector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region To clear previous tab items

            for (int i = 0; i < tabcontrol.Items.Count; i++)
            {
                SfTabItem item = tabcontrol.Items[i] as SfTabItem;
                item.Content = null;
            }
            tabcontrol.Items.Clear();

            #endregion
            foreach (var item in this.sourcelistbox.Items)
            {
               
                var info = item as SampleInfo;
                SfTabItem tabItem = new SfTabItem();
                tabItem.Header = info.Header;
                tabItem.Content = info.SampleView;
                tabItem.Tag = info.Tag;
                this.tabcontrol.Items.Add(tabItem);
               
            }

            #region To Select default Items

            if (tabcontrol.Items.Count > 0)
                tabcontrol.SelectedIndex = 0;

            #endregion

        }

        bool flag = true;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ((((this.Parent as Grid).Parent as Grid).Parent) as ControlsHome).FullView = flag;
            if (flag)
            {
                this.headergrid.Visibility = Visibility.Collapsed;
                this.tabcontrol.Margin = new Thickness(0, -85, 0, 0);
                this.fullscreentext.Text = "Normal View";
                
            }
            else
            {
                this.headergrid.Visibility = Visibility.Visible;
                this.tabcontrol.Margin = new Thickness(0, 0, 0, 0);
                this.fullscreentext.Text = "Full Page View";
            }
            flag = !flag;
        }

    
        private void categoryselector_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if(args.NewValue == null)
            {
                
                tabcontrol.Items.Clear();
            }
            if ((sender as ComboBox).Items.Count > 0)
                (sender as ComboBox).SelectedIndex = 0;
            if ((sender as ComboBox).Items.Count <= 1)
            {
                (sender as ComboBox).Visibility = Visibility.Collapsed;
            }
            else
                (sender as ComboBox).Visibility = Visibility.Visible;
        }
    }
 
}
