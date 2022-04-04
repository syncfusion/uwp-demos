#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace PdfViewerDemo
{
    public sealed partial class CommentListView : UserControl
    {
        public CommentListView()
        {
            this.InitializeComponent();
            if(MainPageAccesser.MainPage.SelectedCommentListView != null)
            {
                MainPageAccesser.MainPage.SelectedCommentListView.HideList();
            }
            MainPageAccesser.MainPage.SelectedCommentListView = this;
        }

        void ShowList()
        {
            if (MainPageAccesser.MainPage.SelectedCommentListView != null && MainPageAccesser.MainPage.SelectedCommentListView != this)
                MainPageAccesser.MainPage.SelectedCommentListView.HideList();
            listView.Visibility = Visibility.Visible;
            expandIcon.Visibility = Visibility.Collapsed;
            collapseIcon.Visibility = Visibility.Visible;
            MainPageAccesser.MainPage.SelectedCommentListView = this;
        }

        void HideList()
        {
            listView.Visibility = Visibility.Collapsed;
            expandIcon.Visibility = Visibility.Visible;
            collapseIcon.Visibility = Visibility.Collapsed;
            MainPageAccesser.MainPage.SelectedCommentListView = null;
        }

        internal ListView CommentList
        {
            get
            {
                return listView;
            }
        }

        void ToggleListView()
        {
            if (listView.Visibility == Visibility.Collapsed)
                ShowList();
            else
                HideList();
        }
        private void expandButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleListView();
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ToggleListView();
        }
    }
}
