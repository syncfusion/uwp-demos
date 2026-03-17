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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Common
{
    public sealed partial class WhatsNewTemplate : UserControl , IDisposable
    {
        ResourceDictionary res = new ResourceDictionary();
        public WhatsNewTemplate()
        {
            this.InitializeComponent();
            res.Source = new Uri("ms-appx:///Syncfusion.SampleBrowser.UWP.Common/TileStyle.xaml", UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Add(res);
            this.Unloaded += WhatsNewTemplate_Unloaded;
        }

        private void WhatsNewTemplate_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {

            this.Resources.MergedDictionaries.Remove(res);
            res = null;
            this.Resources.MergedDictionaries.Clear();
            this.Resources.Clear();
            this.Resources = null;
            this.gridview.ItemClick -= GridView_ItemClick;
            this.gridview = null;
            this.Unloaded -= WhatsNewTemplate_Unloaded;

        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrameNavigationService.CurrentFrame.Navigate(typeof(WhatsNewContentPage), e.ClickedItem);        
        }
    }
}
