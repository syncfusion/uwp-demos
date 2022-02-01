#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace PdfViewerDemo
{
    public sealed partial class CommentView : UserControl
    {
        private SfPdfViewerControl pdfViewer;
        private const int commentViewHeight = 102, textBoxCollapsedHeight = 30;
        public CommentView()
        {
            this.InitializeComponent();
            PointerPressed += CommentView_PointerPressed;
            pdfViewer = MainPageAccesser.MainPage.PdfViewer;
            textBox.Focus(FocusState.Pointer);
            if (MainPageAccesser.MainPage.SelectedCommentView != null)
                MainPageAccesser.MainPage.SelectedCommentView.CollapseSelectedCommentView();
            if (MainPageAccesser.MainPage.SelectedCommentListView != null)
                MainPageAccesser.MainPage.SelectedCommentListView.CommentList.SelectedItem = null;
            MainPageAccesser.MainPage.SelectedCommentView = this;
        }

        private void CollapseSelectedCommentView()
        {
            if (MainPageAccesser.MainPage.SelectedCommentView != null)
            {
                if (MainPageAccesser.MainPage.SelectedCommentView.textBlock.Text.Length == 0)
                    MainPageAccesser.MainPage.SelectedCommentView.parent.Height = textBoxCollapsedHeight;
                else
                {
                    MainPageAccesser.MainPage.SelectedCommentView.textBlock.Visibility = Visibility.Visible;
                    MainPageAccesser.MainPage.SelectedCommentView.textBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ShowCommentBox()
        {
            if (MainPageAccesser.MainPage.SelectedCommentView != null && MainPageAccesser.MainPage.SelectedCommentView != this)
            {
                if (MainPageAccesser.MainPage.SelectedCommentView.textBlock.Text.Length == 0)
                    MainPageAccesser.MainPage.SelectedCommentView.parent.Height = textBoxCollapsedHeight;
                else
                {
                    MainPageAccesser.MainPage.SelectedCommentView.textBlock.Visibility = Visibility.Visible;
                    MainPageAccesser.MainPage.SelectedCommentView.textBox.Visibility = Visibility.Collapsed;
                }
            }

            if (DataContext != null)
            {
                if ((DataContext as Comment).Text == "")
                    textBox.Visibility = Visibility.Visible;
                else if(commentBox.Visibility == Visibility.Collapsed)
                {
                    textBox.Visibility = Visibility.Collapsed;
                    textBlock.Visibility = Visibility.Visible;
                }
                commentBox.Visibility = Visibility.Visible;
                parent.Height = commentViewHeight;
                MainPageAccesser.MainPage.SelectedCommentView = this;
            }
        }

        private void CommentView_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (pdfViewer != null && DataContext != null)
            {
                IAnnotation annotation = (DataContext as Comment).Annotation;
                pdfViewer.SelectAnnotation(annotation);
            }
            ShowCommentBox();
        }

        public static readonly DependencyProperty AnnotationProperty =
           DependencyProperty.Register("Annotation", typeof(IAnnotation), typeof(CommentView), new PropertyMetadata(null, new PropertyChangedCallback(AnnotationCallBack)));

        private void SetIcon(Grid icon)
        {
            ink.Visibility = Visibility.Collapsed;
            highlight.Visibility = Visibility.Collapsed;
            underline.Visibility = Visibility.Collapsed;
            strike.Visibility = Visibility.Collapsed;
            rectangle.Visibility = Visibility.Collapsed;
            line.Visibility = Visibility.Collapsed;
            ellipse.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
            callout.Visibility = Visibility.Collapsed;
            freeText.Visibility = Visibility.Collapsed;
            stamp.Visibility = Visibility.Collapsed;
            icon.Visibility = Visibility.Visible;
        }
        private static void AnnotationCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IAnnotation)
            {
                CommentView commentView = d as CommentView;
                if (e.NewValue is ShapeAnnotation)
                {
                    ShapeAnnotation shape = e.NewValue as ShapeAnnotation;
                    switch (shape.AnnotationType)
                    {
                        case "Line":
                            commentView.SetIcon(commentView.line);
                            break;

                        case "Rectangle":
                            commentView.SetIcon(commentView.rectangle);
                            break;

                        case "Ellipse":
                            commentView.SetIcon(commentView.ellipse);
                            break;
                    }
                }
                else if (e.NewValue is InkAnnotation)
                {
                    commentView.SetIcon(commentView.ink);
                }
                else if (e.NewValue is TextMarkupAnnotation)
                {
                    TextMarkupAnnotation text = e.NewValue as TextMarkupAnnotation;
                    switch (text.AnnotationType)
                    {
                        case "Highlight":
                            commentView.SetIcon(commentView.highlight);
                            break;

                        case "Underline":
                            commentView.SetIcon(commentView.underline);
                            break;

                        case "Strikethrough":
                            commentView.SetIcon(commentView.strike);
                            break;
                    }
                }
                else if (e.NewValue is PopupAnnotation)
                    commentView.SetIcon(commentView.popup);
                else if (e.NewValue is FreeTextAnnotation)
                {
                    if (e.NewValue is FreeTextCalloutAnnotation)
                        commentView.SetIcon(commentView.callout);
                    else
                        commentView.SetIcon(commentView.freeText);
                }
                else if (e.NewValue is StampAnnotation)
                {

                    commentView.SetIcon(commentView.stamp);
                }
                if ((e.NewValue as IAnnotation).Comments[0].Text != "")
                {
                    commentView.commentBox.Visibility = Visibility.Visible;
                    commentView.textBlock.Visibility = Visibility.Visible;
                    commentView.textBox.Visibility = Visibility.Collapsed;
                    commentView.parent.Height = CommentView.commentViewHeight;
                }
            }
        }
        public IAnnotation Annotation
        {
            get { return ((IAnnotation)GetValue(AnnotationProperty)); }
            set { SetValue(AnnotationProperty, value); }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateText(sender as TextBox);
        }

        private void UpdateText(TextBox textBox)
        {
            textBox.Visibility = Visibility.Collapsed;
            if (DataContext != null)
            {
                Comment comment = DataContext as Comment;
                if (textBox.Text != "")
                {
                    textBlock.Visibility = Visibility.Visible;
                    if (comment != null)
                        comment.Text = textBox.Text;
                }
                else if(comment.Text != "")
                {
                    if (comment != null)
                        comment.Text = textBox.Text;
                    parent.Height = textBoxCollapsedHeight;
                    textBlock.Visibility = Visibility.Collapsed;
                    textBox.Visibility = Visibility.Collapsed;
                    commentBox.Visibility = Visibility.Collapsed;
                }
                else
                    parent.Height = textBoxCollapsedHeight;
            }
            postButton.IsEnabled = false;
        }

        private void textBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
                textBox.Visibility = Visibility.Collapsed;
                if (textBlock.Text != "")
                    textBlock.Visibility = Visibility.Visible;
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            postButton.IsEnabled = (sender as TextBox).Text != "";
        }

        private void postButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateText(textBox);
        }

        private void textBlock_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            textBox.Text = textBlock.Text;
            textBox.Visibility = Visibility.Visible;
            textBlock.Visibility = Visibility.Collapsed;
        }
    }
}
