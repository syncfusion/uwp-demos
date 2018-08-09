
using Syncfusion.UI.Xaml.RichTextBoxAdv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RTEDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Forum : Page, IDisposable
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Forum"/> class.
        /// </summary>
        public Forum()
        {
            this.InitializeComponent();
            InitForumContent();
            this.Unloaded += Forum_Unloaded;
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Unloaded event of the Forum control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void Forum_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Clears the items from the ListView
        /// </summary>
        void ClearListViewItems()
        {
            if (listView == null || listView.Items == null || listView.Items.Count == 0)
                return;
            for (int i = 0; i < listView.Items.Count; i++)
            {
                object item = listView.Items[i];
                listView.Items.Remove(item);
                i--;
                if (item is ForumPostControl)
                    (item as ForumPostControl).Dispose();
                else if (item is ForumReplyControl)
                    (item as ForumReplyControl).Dispose();
            }
        }
        /// <summary>
        /// Initializes the content of the forum.
        /// </summary>
        private void InitForumContent()
        {
            AddReplyControl();
            //Sets title to the current forum thread.
            titleTextBlock.Text = "How to add the RichTextBoxAdv to a Windows Store app?";
            //Loads the initial query from a RTF file.
            Stream stream = GetManifestResourceStream("InitialQuery.docx");
            PostContent(stream, true, FormatType.Docx);
            stream.Dispose();
            //Loads the reply for initial query from a RTF file.
            stream = GetManifestResourceStream("ReplyContent.docx");
            PostContent(stream, false, FormatType.Docx);
            stream.Dispose();
        }
        /// <summary>
        /// Gets the specified resource file as stream.
        /// </summary>
        /// <param name="fileName">The resource file name</param>
        /// <returns>Stream of the specified resource file</returns>
        private Stream GetManifestResourceStream(string fileName)
        {
            // Common for Phone and Desktop
            Assembly execAssm = this.GetType().GetTypeInfo().Assembly;
            string[] resourceNames = execAssm.GetManifestResourceNames();
            foreach (string resourceName in resourceNames)
            {
                if (resourceName.EndsWith("." + fileName))
                {
                    fileName = resourceName;
                    break;
                }
            }
            return execAssm.GetManifestResourceStream(fileName);
        }
        /// <summary>
        /// Adds the forum reply control.
        /// </summary>
        private void AddReplyControl()
        {
            ForumReplyControl replyControl = new ForumReplyControl(this);
            ClearListViewItems();
            listView.Items.Add(replyControl);
        }
        /// <summary>
        /// Posts the content to RichTextBoxAdv in block layout type.
        /// </summary>
        /// <param name="contentStream">The content stream.</param>
        /// <param name="isInitialQuery">if set to <c>true</c> [is initial query].</param>
        /// <param name="formatType">Type of the format.</param>
        public void PostContent(Stream contentStream, bool isInitialQuery, FormatType formatType)
        {
            if (contentStream != null)
            {
                ForumPostControl forumPostControl = new ForumPostControl();
                forumPostControl.richTextBoxAdv.Load(contentStream, formatType);
                contentStream.Dispose();
                forumPostControl.updatedOnTextBlock.Text = "Updated On " + DateTime.Now.ToString();
                if (!isInitialQuery)
                    forumPostControl.imgPostedBy.Source = new BitmapImage(new Uri("ms-appx:/RTE/Assets/User32.png", UriKind.RelativeOrAbsolute));
                listView.Items.Insert(listView.Items.Count - 1, forumPostControl);
            }
        }

        #endregion
        
        #region Dispose
        /// <summary>
        /// Relesase the unmanaged resource.
        /// </summary>
        public void Dispose()
        {
            this.Unloaded -= Forum_Unloaded;
            //ClearListViewItems();
            UnlinkChildrens(this);
        }
        /// <summary>
        /// Unlink the elements from Page.
        /// </summary>
        /// <param name="element"></param>
        void UnlinkChildrens(UIElement element)
        {
            if (element == null)
                return;
            if (element is Panel)
            {
                for (int i = 0; i < (element as Panel).Children.Count; i++)
                {
                    UIElement childElement = (element as Panel).Children[i];
                    UnlinkChildrens(childElement);
                    (element as Panel).Children.Remove(childElement);
                    i--;
                }
            }
            else if (element is ItemsControl)
            {
                for (int j = 0; j < (element as ItemsControl).Items.Count; j++)
                {
                    UIElement childElement = ((element as ItemsControl).Items[j] as UIElement);
                    if (childElement == null)
                    {
                        //(element as ItemsControl).Items.RemoveAt(j);
                        //j--;
                    }
                    else
                    {
                        UnlinkChildrens(childElement);
                        (element as ItemsControl).Items.Remove(childElement);
                        j--;
                    }
                }
            }
            else if (element is ContentControl)
            {
                UnlinkChildrens((element as ContentControl).Content as UIElement);
                (element as ContentControl).Content = null;
            }
            else if (element is UserControl)
            {
                if (element is ForumPostControl)
                    (element as ForumPostControl).Dispose();
                else if (element is ForumReplyControl)
                    (element as ForumReplyControl).Dispose();
                UnlinkChildrens((element as UserControl).Content as UIElement);
                (element as UserControl).Content = null;
            }
        }
        #endregion
    }
}
