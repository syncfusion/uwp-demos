using Syncfusion.UI.Xaml.RichTextBoxAdv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace RTEDemo
{
    public sealed partial class ForumReplyControl : UserControl
    {
        #region Fields
        Page parentPage = null;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the parent page.
        /// </summary>
        /// <value>
        /// The parent page.
        /// </value>
        private object ParentPage
        {
            get
            {
                return parentPage;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ForumReplyControl"/> class.
        /// </summary>
        /// <param name="parentPage">The parent page.</param>
        public ForumReplyControl(Page parentPage)
        {
            this.InitializeComponent();
            this.parentPage = parentPage;
            this.Loaded += ForumReplyControl_Loaded;
            this.richTextBoxAdv.RequestNavigate += richTextBoxAdv_RequestNavigate;
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

        #region Events
        /// <summary>
        /// Handles the loaded event of the ForumReplyControl.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void ForumReplyControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (richTextBoxAdv.Document != null && richTextBoxAdv.Document.CharacterFormat != null)
                richTextBoxAdv.Document.CharacterFormat.FontColor = Windows.UI.Color.FromArgb(255, 153, 153, 153);
        }
        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (parentPage is Forum)
            {
                Stream stream = new MemoryStream();
                richTextBoxAdv.Save(stream, FormatType.Rtf);
                (parentPage as Forum).PostContent(stream, false, FormatType.Rtf);
                stream.Dispose();
            }
            ClearContent();
        }
        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearContent();
        }
        /// <summary>
        /// Clears the content.
        /// </summary>
        private void ClearContent()
        {
            richTextBoxAdv.NewDocumentCommand.Execute(null);
            if (richTextBoxAdv.Document.CharacterFormat != null)
                richTextBoxAdv.Document.CharacterFormat.FontColor = Windows.UI.Color.FromArgb(255, 153, 153, 153);
        }
        #endregion

        #region  Helper Methods
        /// <summary>
        /// Disposes the resources of ForumReplyControl.
        /// </summary>
        public void Dispose()
        {
            this.Loaded -= ForumReplyControl_Loaded;
            if (this.Content is Panel)
                (this.Content as Panel).Children.Clear();
            this.richTextBoxAdv.RequestNavigate -= richTextBoxAdv_RequestNavigate;
            //Disposes the SfRichTextBoxAdv contents explicitly.
            this.richTextBoxAdv.Dispose();
            this.richTextBoxAdv = null;
        }
        #endregion
    }
}
