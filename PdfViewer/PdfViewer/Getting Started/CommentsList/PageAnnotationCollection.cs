#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace PdfViewerDemo
{
    public class PageAnnotationCollection : INotifyPropertyChanged
    {
        public ObservableCollection<Comment> CommentCollection { get; set; }
        private int pageNumber, removedIndex;
        private string page, annotationCount;
        public event PropertyChangedEventHandler PropertyChanged;
        private IAnnotation removedAnnotation;
        internal MainPage mainPage;
        private int selectedCommentIndex = -1;

        public int PageNUmber
        {
            get
            {
                return pageNumber;
            }

            set
            {
                pageNumber = value;
                Page = "Page " + (pageNumber + 1).ToString();
            }
        }

        public string AnnotationCount
        {
            get
            {
                return annotationCount;
            }
            set
            {
                annotationCount = value;
                NotifyPropertyChanged("AnnotationCount");
            }
        }

        public string Page
        {
            get
            {
                return page;
            }

            set
            {
                page = value;
                NotifyPropertyChanged("Page");
            }
        }

        public PageAnnotationCollection(int index)
        {
            CommentCollection = new ObservableCollection<Comment>();
            PageNUmber = index;
            this.PropertyChanged += PropertyChanged;
        }

        public void AddAnnotation(IAnnotation annotation)
        {
            if (annotation.Comments.Count != 0)
            {
                if (removedAnnotation == null || removedAnnotation != null && removedAnnotation.AnnotationId != annotation.AnnotationId)
                {
                    CommentCollection.Add(annotation.Comments[0]);
                    AnnotationCount = CommentCollection.Count.ToString();
                }
                else if(removedAnnotation.AnnotationId == annotation.AnnotationId)
                {
                    CommentCollection.Insert(removedIndex, annotation.Comments[0]);
                    AnnotationCount = CommentCollection.Count.ToString();
                }
            }
        }

        public void RemoveAnnotation(IAnnotation annotation)
        {
            removedAnnotation = annotation;
            removedIndex = CommentCollection.IndexOf(annotation.Comments[0]);
            CommentCollection.Remove(annotation.Comments[0]);
            AnnotationCount = CommentCollection.Count.ToString();
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    internal static class MainPageAccesser
    {
        internal static MainPage MainPage { get; set; }
    }

}
