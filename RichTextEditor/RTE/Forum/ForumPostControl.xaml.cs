#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.RichTextBoxAdv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RTEDemo
{
    public sealed partial class ForumPostControl : UserControl
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ForumPostControl"/> class.
        /// </summary>
        public ForumPostControl()
        {
            this.InitializeComponent();
            this.richTextBoxAdv.RequestNavigate += richTextBoxAdv_RequestNavigate;
            this.Loaded += ForumPostControl_Loaded;
            this.Unloaded += ForumPostControl_Unloaded;
            this.richTextBoxAdv.Unloaded += RichTextBoxAdv_Unloaded;
        }
        #endregion

        #region Hyperlink navigation
        /// <summary>
        /// Handles the request navigate event of the richTextBoxAdv control.
        /// </summary>
        /// <param name="obj">The source of the event.</param>
        /// <param name="args">The <see cref="RequestNavigateEventArgs"/> instance containing the event data.</param>
        void richTextBoxAdv_RequestNavigate(object obj, RequestNavigateEventArgs args)
        {
            if (args.Hyperlink.LinkType == HyperlinkType.Webpage || args.Hyperlink.LinkType == HyperlinkType.Email)
            {
                Uri uri = new Uri(args.Hyperlink.NavigationLink);
                LaunchUri(uri);
            }
        }
        /// <summary>
        /// Launches the URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        async void LaunchUri(Uri uri)
        {
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        #endregion

        #region  Helper Methods
        MemoryStream memoryStream;
        private void ForumPostControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (memoryStream != null)
            {
                //Create new instance of SfRichTextBoxAdv
                richTextBoxAdv = new SfRichTextBoxAdv();
                Grid.SetRow(richTextBoxAdv, 2);
                Grid.SetColumnSpan(richTextBoxAdv, 3);
                richTextBoxAdv.FontSize = 14;
                richTextBoxAdv.Background = new SolidColorBrush(Colors.Transparent);
                richTextBoxAdv.Foreground = new SolidColorBrush(Color.FromArgb(255, 153, 153, 153));
                richTextBoxAdv.FontFamily = new FontFamily("Global User Interface");
                richTextBoxAdv.LayoutType = LayoutType.Block;
                richTextBoxAdv.ManipulationMode = ManipulationModes.All;
                richTextBoxAdv.IsDoubleTapEnabled = true;
                if (this.Content is Panel)
                    (this.Content as Panel).Children.Add(richTextBoxAdv);
                //Load the saved stream into SfRichTextBoxAdv
                richTextBoxAdv.Load(memoryStream, FormatType.Docx);
                memoryStream.Dispose();
                memoryStream = null;
            }
        }
        private void RichTextBoxAdv_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.Content is Panel && richTextBoxAdv != null)
                (this.Content as Panel).Children.Remove(richTextBoxAdv);
        }
        private void ForumPostControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (richTextBoxAdv != null)
            {
                memoryStream = new MemoryStream();
                richTextBoxAdv.Save(memoryStream, FormatType.Docx);
            }
        }
        /// <summary>
        /// Disposes the resources of ForumPostControl.
        /// </summary>
        public void Dispose()
        {
            if (this.Content is Panel)
                (this.Content as Panel).Children.Clear();
            this.richTextBoxAdv.RequestNavigate -= richTextBoxAdv_RequestNavigate;
            //Disposes the SfRichTextBoxAdv contents explicitly.
            this.richTextBoxAdv.Dispose();
            this.richTextBoxAdv = null;
            if (memoryStream != null)
            {
                memoryStream.Dispose();
                memoryStream = null;
            }
        }
        #endregion
    }
}
