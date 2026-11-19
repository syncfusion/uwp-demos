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
using Syncfusion.UI.Xaml.Controls.Navigation;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Navigation.TabControl
{
    public sealed partial class Tab : SampleLayout
    {
        public Tab()
        {
            this.InitializeComponent();
            //if(IsMobileFamily())
            // this.GridColum.Width = GridLength.Auto;
        }
        
        public static bool IsMobileFamily()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                return true;
            }
            return false;
        }
        private void TabClosed(object sender, CloseTabEventArgs args)
        {
            if (tabControl.Items.Count == 0)
            {
                tabControl.ContentTemplate = null;
            }
        }
        public override void Dispose()
        {
            if (tabControl != null)
            {
                tabControl.TabClosed -= TabClosed;
                if (tabcontrolproperties != null)
                {
                    tabcontrolproperties.Dispose();
                    tabcontrolproperties = null;
                }
                tabControl.Dispose();
                if (tabControl.Items.Count > 0)
                    tabControl.Items.Clear();
                tabControl.ItemsSource = null;
                tabControl.ItemTemplate = null;
                tabControl.HeaderTemplate = null;
                tabControl.ContentTemplate = null;
                tabControl.DataContext = null;
            }

            GC.Collect();
        }
        private void ShowContextMenu_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton tb = sender as ToggleButton;
            if ((bool)ShowContext_Menu.IsChecked)
            {
                tabControl.ShowContextMenu = true;
            }
            else
            {
                tabControl.ShowContextMenu = false;
            }
        }

        //private void EnableSwipe_Click(object sender, RoutedEventArgs e)
        //{
        //    if ((bool)EnableSwipe.IsChecked)
        //    {
        //        tabControl.EnableSwipeGestures = true;
        //    }
        //    else
        //    {
        //        tabControl.EnableSwipeGestures = false;
        //    }
        //}
    }
}
