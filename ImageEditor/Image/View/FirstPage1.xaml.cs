#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
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
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.ImageEditor.Enums;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.ImageEditor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FirstPage1 : Page
    {
        public FirstPage1()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenImageEditorSource(image1.Source);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenImageEditorSource(image2.Source);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenImageEditorSource(image3.Source);
        }

        private void OpenImageEditorSource(ImageSource stream)
        {
            editor.ImageSource = stream;
            editor.ToolbarSettings.VisibleShapesItems = ImageEditorShapes.Rectangle | ImageEditorShapes.Circle | ImageEditorShapes.Arrow | ImageEditorShapes.Line
                | ImageEditorShapes.DoubleArrow | ImageEditorShapes.Dotted | ImageEditorShapes.DottedArrow | ImageEditorShapes.DottedDoubleArrow;
            firstPage.Visibility = Visibility.Collapsed;
            secondPage.Visibility = Visibility.Visible;
        }

        private void appBarButton_Click(object sender, RoutedEventArgs e)
        {
            firstPage.Visibility = Visibility.Visible;
            secondPage.Visibility = Visibility.Collapsed;

        }
    }
}
