#region Copyright Syncfusion Inc. 2001-2017.
// Copyright Syncfusion Inc. 2001-2017. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.Controls.Navigation;
using Syncfusion.Pdf.Interactive;
using Windows.Graphics.Display;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PdfViewerDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FormFillings : Page
    {
        #region Fields
        DispatcherTimer m_sliderTimer;
        int m_sliderTickCount = 0;
        int m_sliderCurrentValue = 0;
        bool m_isButtonClicked;
        bool m_isSinglePageView;
        bool isSearchTabVisible = false;
        bool isAnnotationTabVisible = false;
        bool isAnnotationMode;
        bool isBookmarkClicked = false;
        enum AnnotationMode
        {
            None,
            InkAnnotationMode,
            TextMarkupAnnotationMode,
            ShapesAnnotationMode,
            PopupAnnotationMode
        }

        AnnotationMode currentAnnotationMode;

        #endregion
        public FormFillings()
        {
            this.InitializeComponent();
            this.DataContext = pdfViewer;
            this.Unloaded += FormFillings_Unloaded;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                this.Tapped += FormFillings_Tapped;
                m_sliderTimer = new DispatcherTimer();
                m_sliderTimer.Tick += sliderTimer_Tick;
            }
            else
            {
                this.pdfViewer.SemanitcZoomChanged += PdfViewer_SemanitcZoomChanged;
            }
        }
        
        private void FormFillings_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.pdfViewer != null)
                this.pdfViewer.Unload(true);
            UnlinkChildrens(this);
        }

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
                    if (childElement != null)
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
                UnlinkChildrens((element as UserControl).Content as UIElement);
                (element as UserControl).Content = null;
            }
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            // NavigationService.GoBack();
            e.Handled = true;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        private void PdfViewer_SemanitcZoomChanged(object sender, SemanticZoomViewChangedEventArgs e)
        {
            
        }
        private void PdfViewer_PopupAnnotationAdded(object sender, PopupAnnotationAddedEventArgs e)
        {
            PopupButton.IsChecked = false;
        }
        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //NavigationService.GoBack();
        }


        private void ViewModeChange(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == "FitWidthBtn")
            {
                pdfViewer.ViewMode = PageViewMode.FitWidth;
                FitWidthBtn.IsEnabled = false;
                OnePageBtn.IsEnabled = true;
                ZoomInBtn.IsEnabled = true;
                ZoomOutBtn.IsEnabled = true;
            }
            else if (btn.Name == "OnePageBtn")
            {
                pdfViewer.ViewMode = PageViewMode.OnePage;
                OnePageBtn.IsEnabled = false;
                FitWidthBtn.IsEnabled = true;
                ZoomOutBtn.IsEnabled = false;
                ZoomInBtn.IsEnabled = true;
            }
        }

        private void ChangeZoom(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (pdfViewer.Zoom <= 75)
            {
                ZoomOutBtn.IsEnabled = false;
                if (clickedButton.Name == "ZoomInBtn")
                    ZoomOutBtn.IsEnabled = true;
            }
            else if (pdfViewer.Zoom >= 300)
            {
                ZoomInBtn.IsEnabled = false;
                if (clickedButton.Name == "ZoomOutBtn")
                    ZoomInBtn.IsEnabled = true;
            }
            else
            {
                ZoomInBtn.IsEnabled = true;
                if (!OnePageBtn.IsEnabled)
                    ZoomOutBtn.IsEnabled = false;
                else
                    ZoomOutBtn.IsEnabled = true;
            }

            FitWidthBtn.IsEnabled = true;
            OnePageBtn.IsEnabled = true;
        }


        void FormFillings_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if ((!m_isButtonClicked) && (SearchBottomTab.Visibility == Windows.UI.Xaml.Visibility.Collapsed))
            {
                if (!isAnnotationMode)
                {
                    if (TopTab.Visibility == Windows.UI.Xaml.Visibility.Visible)
                    {
                        BottomTab.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        TopTab.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        MoreOptionsTab.Visibility = Visibility.Collapsed;
                        PageNumberPanel.SetValue(Grid.RowProperty, 3);
                        PageNumberPanel.Margin = new Thickness(0, 0, 0, 0);
                    }
                    else
                    {
                        BottomTab.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        TopTab.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        if (MoreButton.IsChecked.Value)
                        {
                            MoreOptionsTab.Visibility = Visibility.Collapsed;
                            MoreButton.IsChecked = false;
                        }
                        PageNumberPanel.Visibility = Visibility.Visible;
                        Grid.SetRow(PageNumberPanel, 2);
                        PageNumberPanel.Margin = new Thickness(0, 0, 0, 60);
                        Grid.SetRow(BottomTab, 3);
                    }
                }
                else
                {

                    if (BottomAnnotationTab.Visibility == Visibility.Visible)
                    {
                        PageNumberPanel.SetValue(Grid.RowProperty, 3);
                        PageNumberPanel.Margin = new Thickness(0, 0, 0, 0);
                        BottomAnnotationTab.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        BottomAnnotationTab.Visibility = Visibility.Visible;
                        Grid.SetRow(PageNumberPanel, 2);
                        PageNumberPanel.Margin = new Thickness(0, 0, 0, 60);
                        Grid.SetRow(BottomAnnotationTab, 3);
                    }

                    if (currentAnnotationMode == AnnotationMode.InkAnnotationMode)
                    {
                        if (InkAnnotationTab.Visibility == Visibility.Visible)
                            InkAnnotationTab.Visibility = Visibility.Collapsed;
                        else
                            InkAnnotationTab.Visibility = Visibility.Visible;
                    }
                    else if (currentAnnotationMode == AnnotationMode.TextMarkupAnnotationMode)
                    {
                        if (TextMarkupAnnotationTab.Visibility == Visibility.Visible)
                            TextMarkupAnnotationTab.Visibility = Visibility.Collapsed;
                        else
                            TextMarkupAnnotationTab.Visibility = Visibility.Visible;
                    }
                    else if (currentAnnotationMode == AnnotationMode.ShapesAnnotationMode)
                    {
                        if (ShapeAnnotationTab.Visibility == Visibility.Visible)
                            ShapeAnnotationTab.Visibility = Visibility.Collapsed;
                        else
                            ShapeAnnotationTab.Visibility = Visibility.Visible;
                    }
                    else if (currentAnnotationMode == AnnotationMode.PopupAnnotationMode)
                    {
                        if (PopupAnnotationTab.Visibility == Visibility.Visible)
                            PopupAnnotationTab.Visibility = Visibility.Collapsed;
                        else
                            PopupAnnotationTab.Visibility = Visibility.Visible;
                    }

                    if (isBookmarkClicked)
                        BottomAnnotationTab.Visibility = Visibility.Collapsed;
                    
                }
            }
            m_isButtonClicked = false;
        }

        void sliderTimer_Tick(object sender, object e)
        {
            if (m_sliderTickCount == 50)
            {
                if (m_sliderCurrentValue == pdfViewer.PageCount)
                    pdfViewer.GotoPage(m_sliderCurrentValue);
                else if ((m_sliderCurrentValue) > 0)
                    pdfViewer.GotoPage(m_sliderCurrentValue);
                m_sliderTickCount = 0;
                m_sliderTimer.Stop();
            }
            m_sliderTickCount++;
        }

        private void PdfSlider_GotFocus(object sender, RoutedEventArgs e)
        {
            Slider slider = sender as Slider;
            slider.Minimum = 1;
            slider.Maximum = pdfViewer.PageCount;
        }

        private void PdfSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;
            m_sliderCurrentValue = (int)e.NewValue;
            m_sliderTickCount = 0;
            m_sliderTimer.Start();
        }

        private void BtnSinglePage_OnClick(object sender, RoutedEventArgs e)
        {
            if (!m_isSinglePageView)
            {
                PageNumberPanel.SetValue(Grid.RowProperty, 3);
                PageNumberPanel.Margin = new Thickness(0, 0, 0, 0);
                pdfViewer.PageView = PdfViewerPageView.SinglePageView;
                ViewModeTab.Visibility = Visibility.Visible;
                SinglePageViewButton.Visibility = Visibility.Collapsed;
                btnSinglePage.Visibility = Visibility.Collapsed;
                BottomTab.Visibility = Visibility.Collapsed;
                m_isButtonClicked = true;
                m_isSinglePageView = true;
            }
        }
        private void NormalView_Click(object sender, RoutedEventArgs e)
        {
            PageNumberPanel.SetValue(Grid.RowProperty, 2);
            PageNumberPanel.Margin = new Thickness(0, 0, 0, 60);
            Grid.SetRow(BottomTab, 3);
            pdfViewer.PageView = PdfViewerPageView.ContinuousPageView;
            SinglePageViewButton.Visibility = Visibility.Visible;
            btnSinglePage.Visibility = Visibility.Visible;
            BottomTab.Visibility = Visibility.Visible;
            pdfViewer.SinglePageViewCommand.Execute(false);
            ViewModeTab.Visibility = Visibility.Collapsed;
            m_isSinglePageView = false;
            m_isButtonClicked = true;
        }



        private async void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                #region HideStatusBar
                StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                await statusBar.HideAsync();
                #endregion
            }

            Assembly assembly = typeof(FormFillings).GetTypeInfo().Assembly;
            Stream fileStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.FormFillingDocument.pdf");
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            PdfLoadedDocument ldoc = new PdfLoadedDocument(buffer);
            BookmarkButton.IsEnabled = false;
            pdfViewer.PageChanged += PdfViewer_PageChanged;
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                if (pdfViewer.PageCount == 1)
                    NextPageButton.IsEnabled = false;
            }
            pdfViewer.LoadDocument(ldoc);
            if (PageCounttext != null)
                PageCounttext.Text = string.Format("of {0}", pdfViewer.PageCount.ToString());
            pdfViewer.HyperlinkPointerPressed += PdfViewer_HyperlinkPointerPressed;
            pdfViewer.HyperlinkPointerMoved += PdfViewer_HyperlinkMouseOver;
            pdfViewer.PopupAnnotationAdded += PdfViewer_PopupAnnotationAdded;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                PdfSlider.Minimum = 1;
                PdfSlider.Maximum = pdfViewer.PageCount;
                PdfSlider.Value = 1;
                CurrentPage.Text = "1";
                PageNumber.Text = "/" + pdfViewer.PageCount.ToString();
                DocumentName.Text = "Windows Store Apps Succinctly.pdf";
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }

            if (ldoc.Bookmarks.Count > 0)
                BookmarkButton.IsEnabled = true;

            LoadNavigator(ldoc);
        }

        private void LoadNavigator(PdfLoadedDocument ldoc)
        {
            PdfBookmarkBase bookmarkBase = ldoc.Bookmarks;
            PdfBookmark bookmark;
            SfTreeNavigatorItem navigatorItem;

            treeNavigator.Items.Clear();

            for (int i = 0; i < bookmarkBase.Count; i++)
            {
                bookmark = bookmarkBase[i] as PdfLoadedBookmark;
                navigatorItem = AddChildBookmarks(bookmark);
                navigatorItem.ItemClicked += NavigatorItem_ItemClicked;
                if(!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                {
                    navigatorItem.PointerEntered += NavigatorItem_PointerEntered;
                    navigatorItem.PointerExited += NavigatorItem_PointerExited;
                }
                treeNavigator.Items.Add(navigatorItem);
            }
        }

        private void NavigatorItem_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            SfTreeNavigatorItem item = sender as SfTreeNavigatorItem;
            item.HeaderTemplate = Resources["HeaderTemplate1"] as DataTemplate;
        }

        private void NavigatorItem_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            SfTreeNavigatorItem item = sender as SfTreeNavigatorItem;
            item.HeaderTemplate = Resources["HeaderTemplate2"] as DataTemplate;
        }

        private SfTreeNavigatorItem AddChildBookmarks(PdfBookmark bookmark)
        {
            if (bookmark != null)
            {
                SfTreeNavigatorItem child = new SfTreeNavigatorItem() { Padding = new Thickness(10) };

                string header = bookmark.Title;
                if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
                {
                    if (header.Length > 60)
                        header = header.Substring(0, 58) + ". . . . . . . . ";
                }
                child.Header = header;
                child.ItemClicked += NavigatorItem_ItemClicked;
                if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                {
                    child.PointerEntered += NavigatorItem_PointerEntered;
                    child.PointerExited += NavigatorItem_PointerExited;
                }
                child.Tag = bookmark;

                if (bookmark.Count != 0)
                {
                    for (int i = 0; i < bookmark.Count; i++)
                    {
                        SfTreeNavigatorItem innerChild = new SfTreeNavigatorItem();
                        string innerHeader = bookmark.Title;
                        if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
                        {
                            if (innerHeader.Length > 60)
                                innerHeader = header.Substring(0, 58) + ". . . . . . . . ";
                        }
                        child.Header = innerHeader;
                        innerChild.ItemClicked += NavigatorItem_ItemClicked;
                        if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                        {
                            innerChild.PointerEntered += NavigatorItem_PointerEntered;
                            innerChild.PointerExited += NavigatorItem_PointerExited;
                        }
                        innerChild.Tag = bookmark[i];
                        child.Items.Add(innerChild);
                    }
                }

                if (child.Items != null)
                {
                    for (int i = 0; i < child.Items.Count; i++)
                        child.Items[i] = AddChildBookmarks(bookmark[i]);
                }
                return child;
            }
            else
                return null;
        }

        private void NavigatorItem_ItemClicked(object Sender, Syncfusion.UI.Xaml.Controls.Navigation.ItemClickEventArgs args)
        {
            SfTreeNavigatorItem item = Sender as SfTreeNavigatorItem;
            PdfBookmark bookmark = item.Tag as PdfBookmark;
            if(bookmarkScroller != null)
            {
                if (bookmark.Count > 0)
                    bookmarkScroller.ChangeView(null, 0, null);
            }
            pdfViewer.GoToBookmark(bookmark);

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                if(!(bookmark.Count > 0))
                {
                    BookmarkLayer.Visibility = Visibility.Collapsed;
                    isBookmarkClicked = false;
                    isAnnotationMode = false;
                }
            }
        }

        private void PdfViewer_HyperlinkMouseOver(object sender, HyperlinkEventArgs e)
        {
            string uri = e.URI;
            int pageIndex = e.PageIndex;
            RectangleF rect = e.Bounds;
        }

        private void PdfViewer_HyperlinkPointerPressed(object sender, HyperlinkEventArgs e)
        {
            string uri = e.URI;
            int pageIndex = e.PageIndex;
            RectangleF rect = e.Bounds;
        }
        private void PdfViewer_PageChanged(object sender, PageChangedEventArgs e)
        {
            PageDextTxtBox.Text = string.Format("{0}", pdfViewer.PageNumber.ToString());

            if (PdfSlider != null && CurrentPage != null)
            {
                PdfSlider.ValueChanged -= PdfSlider_ValueChanged;
                PdfSlider.Value = (sender as SfPdfViewerControl).PageNumber;
                PdfSlider.ValueChanged += PdfSlider_ValueChanged;
                CurrentPage.Text = (sender as SfPdfViewerControl).PageNumber.ToString();
            }
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                if (pdfViewer.PageNumber == 1)
                    PrevPageButton.IsEnabled = false;
                else
                    PrevPageButton.IsEnabled = true;
                if (pdfViewer.PageNumber == pdfViewer.PageCount)
                    NextPageButton.IsEnabled = false;
                else
                    NextPageButton.IsEnabled = true;
            }
        }

        async private void Open_Click(object sender, RoutedEventArgs e)
        {
            m_isButtonClicked = true;
            InkButton.IsChecked = false;
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.ViewMode = PickerViewMode.List;
            picker.FileTypeFilter.Add(".pdf");
            var file = await picker.PickSingleFileAsync();
            if (file == null) return;
            BookmarkButton.IsEnabled = false;
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            Stream fileStream = stream.AsStreamForRead();
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            PdfLoadedDocument ldoc = new PdfLoadedDocument(buffer);

            if (InkButton.IsChecked.Value)
                InkButton.IsChecked = false;

            pdfViewer.LoadDocument(ldoc);
                if (BookmarkGrid != null && BookmarkGrid.Visibility == Visibility.Visible)
                {
                    BookmarkGrid.Visibility = Visibility.Collapsed;
                    isBookmarkClicked = false;
                }

            if (PageCounttext != null)
                PageCounttext.Text = string.Format("of {0}", pdfViewer.PageCount.ToString());

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                MoreOptionsTab.Visibility = Visibility.Collapsed;
                MoreButton.IsChecked = false;

                if (pdfViewer.PageView == PdfViewerPageView.SinglePageView)
                {
                    pdfViewer.PageView = PdfViewerPageView.SinglePageView;
                    ViewModeTab.Visibility = Visibility.Visible;
                    SinglePageViewButton.Visibility = Visibility.Collapsed;
                    btnSinglePage.Visibility = Visibility.Collapsed;
                    BottomTab.Visibility = Visibility.Collapsed;
                    pdfViewer.SinglePageViewCommand.Execute(true);
                    m_isButtonClicked = true;
                    m_isSinglePageView = true;
                }
                PdfSlider.Minimum = 1;
                PdfSlider.Maximum = pdfViewer.PageCount;
                PdfSlider.Value = 1;
                DocumentName.Text = file.DisplayName + file.FileType;
                CurrentPage.Text = "1";
                PageNumber.Text = "/" + pdfViewer.PageCount.ToString();
            }
            else
            {
                if (pdfViewer.PageCount == 1)
                    NextPageButton.IsEnabled = false;
                PrevPageButton.IsEnabled = false;
            }

            if (ldoc.Bookmarks.Count > 0)
                BookmarkButton.IsEnabled = true;

            LoadNavigator(ldoc);
        }

        private void MoreButton_Checked(object sender, RoutedEventArgs e)
        {
            MoreOptionsTab.Visibility = Visibility.Visible;
            m_isButtonClicked = true;
        }

        private void MoreButton_Unchecked(object sender, RoutedEventArgs e)
        {
            MoreOptionsTab.Visibility = Visibility.Collapsed;
            m_isButtonClicked = true;
        }

        private void Ink_Click(object sender, RoutedEventArgs e)
        {
            currentAnnotationMode = AnnotationMode.InkAnnotationMode;
            SwitchToAnnotationTab();
            InkAnnotationTab.Visibility = Visibility.Visible;
            InkButton.IsChecked = true;
            m_isButtonClicked = true;
        }
        private void PopupAnnotation_Click(object sender, RoutedEventArgs e)
        {
            currentAnnotationMode = AnnotationMode.PopupAnnotationMode;
            SwitchToAnnotationTab();
            PopupAnnotationTab.Visibility = Visibility.Visible;
            PopupButton.IsChecked = true;
            m_isButtonClicked = true;
        }
        void SwitchToAnnotationTab()
        {
            isAnnotationMode = true;
            TopTab.Visibility = Visibility.Collapsed;
            BottomTab.Visibility = Visibility.Collapsed;
            MoreButton.IsChecked = false;
            BottomAnnotationTab.Visibility = Visibility.Visible;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            isAnnotationMode = false;
            if (InkAnnotationTab.Visibility == Visibility.Visible)
                InkAnnotationTab.Visibility = Visibility.Collapsed;
            else if (TextMarkupAnnotationTab.Visibility == Visibility.Visible)
                TextMarkupAnnotationTab.Visibility = Visibility.Collapsed;
            else if (ShapeAnnotationTab.Visibility == Visibility.Visible)
                ShapeAnnotationTab.Visibility = Visibility.Collapsed;
            else if (PopupAnnotationTab.Visibility == Visibility.Visible)
                PopupAnnotationTab.Visibility = Visibility.Collapsed;
            BottomAnnotationTab.Visibility = Visibility.Collapsed;
            UncheckOthers(sender);
            currentAnnotationMode = AnnotationMode.None;
            TopTab.Visibility = Visibility.Visible;
            BottomTab.Visibility = Visibility.Visible;
            m_isButtonClicked = true;
        }

        private void Ink_Checked(object sender, RoutedEventArgs e)
        {
            UncheckOthers(InkButton);
            pdfViewer.InkAnnotationCommand.Execute(true);
            m_isButtonClicked = true;
        }

        private void Highlight_Checked(object sender, RoutedEventArgs e)
        {
            UncheckOthers(HighlightButton);
            pdfViewer.HighlightAnnotationCommand.Execute(true);
            m_isButtonClicked = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Save(pdfViewer.Save(), "output.pdf");
            m_isButtonClicked = true;
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.UndoCommand.Execute(true);
            m_isButtonClicked = true;
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.RedoCommand.Execute(true);
            m_isButtonClicked = true;
        }

        async void Save(Stream stream, string filename)
        {

            stream.Position = 0;

            FileSavePicker savePicker = new FileSavePicker();
            savePicker.DefaultFileExtension = ".pdf";
            savePicker.SuggestedFileName = filename;
            savePicker.FileTypeChoices.Add("Adobe PDF Document", new List<string>() { ".pdf" });
            StorageFile stFile = await savePicker.PickSaveFileAsync();
            if (stFile != null)
            {
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();
                MessageDialog msgDialog = new MessageDialog("File has been saved successfully.");
                IUICommand cmd = await msgDialog.ShowAsync();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                TopTab.Visibility = Visibility.Collapsed;
                TopSearchTab.Visibility = Visibility.Visible;
                SearchBottomTab.Visibility = Visibility.Visible;
                BottomTab.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                PageSearchTxtBox.Focus(FocusState.Programmatic);
                m_isButtonClicked = true;
            }
            else
            {
                if (!isSearchTabVisible)
                {
                    AnnotationTab.Visibility = Visibility.Collapsed;
                    isAnnotationTabVisible = false;
                    PageSearchTxtBox.Focus(FocusState.Programmatic);
                    SearchTab.Visibility = Visibility.Visible;
                    isSearchTabVisible = true;
                }
                else
                {
                    SearchTab.Visibility = Visibility.Collapsed;
                    isSearchTabVisible = false;
                }
            }
        }

        private void SearchIcon_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PageSearchTxtBox.Text))
            {
                if (pdfViewer.SearchText(PageSearchTxtBox.Text))
                {
                    NextButton.Visibility = Visibility.Visible;
                    PrevButton.Visibility = Visibility.Visible;
                    NotFoundTextBlock.Visibility = Visibility.Collapsed;
                }
                else
                {

                    if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                    {
                        NotFoundTextBlock.Visibility = Visibility.Visible;
                        NotFoundTextBlock1.Text = "'" + PageSearchTxtBox.Text + "' not found";
                        NotFoundTextBlock.Height = Window.Current.Bounds.Height;
                        TopTab.Visibility = Visibility.Collapsed;
                        TopSearchTab.Visibility = Visibility.Collapsed;
                        BottomTab.Visibility = Visibility.Collapsed;
                        SearchBottomTab.Visibility = Visibility.Collapsed;
                        PageNumberPanel.Visibility = Visibility.Collapsed;
                    }
                    else
                        NotFoundTextBlock.Visibility = Visibility.Visible;
                    NextButton.Visibility = Visibility.Collapsed;
                    PrevButton.Visibility = Visibility.Collapsed;

                }
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                    m_isButtonClicked = true;
            }
        }

        private void CloseSearch_Click(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                NotFoundTextBlock.Visibility = Visibility.Collapsed;
                TopSearchTab.Visibility = Visibility.Collapsed;
                SearchBottomTab.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                TopTab.Visibility = Visibility.Visible;
                BottomTab.Visibility = Windows.UI.Xaml.Visibility.Visible;
                m_isButtonClicked = true;
            }
            else
            {
                SearchTab.Visibility = Visibility.Collapsed;
                isSearchTabVisible = false;
                NotFoundTextBlock.Visibility = Visibility.Collapsed;
            }
            PageSearchTxtBox.Text = "";
            pdfViewer.ClearTextSelectionCommand.Execute(true);
        }

        private void PageSearchTxtBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            NextButton.Visibility = Visibility.Collapsed;
            PrevButton.Visibility = Visibility.Collapsed;
        }
        private void LoseFocus(object sender)
        {
            var control = sender as Control;
            var isTabStop = control.IsTabStop;
            control.IsTabStop = false;
            control.IsEnabled = false;
            control.IsEnabled = true;
            control.IsTabStop = isTabStop;
        }
        private async void PageDextTxtBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {

            int destPage = 0;

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                if (!string.IsNullOrEmpty(CurrentPage.Text))
                {
                    bool gotoResult = int.TryParse(CurrentPage.Text, out destPage);
                    if (e.Key == VirtualKey.Enter && gotoResult)
                    {
                        pdfViewer.GotoPage(destPage);
                        PageDextTxtBox.Text = string.Empty;
                        e.Handled = true;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(PageDextTxtBox.Text))
                {
                    bool result = int.TryParse(PageDextTxtBox.Text, out destPage);
                    if (e.Key == VirtualKey.Enter && result)
                    {
                        if (destPage > 0 && destPage <= pdfViewer.PageCount)
                        {
                            pdfViewer.GotoPage(destPage);
                            e.Handled = true;
                            LoseFocus(sender);
                        }
                        else
                        {
                            Windows.UI.Popups.MessageDialog messageDialog = new Windows.UI.Popups.MessageDialog(string.Format("There is no page numbered '{0}' in this document.", destPage.ToString()));
                            messageDialog.Options = Windows.UI.Popups.MessageDialogOptions.None;
                            messageDialog.Title = "Syncfusion PDF Viewer";
                            messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("OK"));
                            await messageDialog.ShowAsync();
                        }
                    }
                }
            }
        }

        private void PageDextTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PageDextTxtBox.SelectAll();
        }

        private void Viewer_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            CoreVirtualKeyStates controlKeyState = Window.Current.CoreWindow.GetKeyState(VirtualKey.Control);
            if ((controlKeyState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down && e.Key == VirtualKey.F)
            {
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                {
                    TopSearchTab.Visibility = Visibility.Visible;
                    SearchBottomTab.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    TopTab.Visibility = Visibility.Collapsed;
                    BottomTab.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
                PageSearchTxtBox.Focus(FocusState.Programmatic);
            }
            if (e.Key == VirtualKey.Enter && PageSearchTxtBox.FocusState != FocusState.Unfocused)
            {
                if (pdfViewer.SearchText(PageSearchTxtBox.Text))
                {
                    NextButton.Visibility = Visibility.Visible;
                    PrevButton.Visibility = Visibility.Visible;
                    NotFoundTextBlock.Visibility = Visibility.Collapsed;
                }
                else
                {
                    if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                    {
                        NotFoundTextBlock.Visibility = Visibility.Visible;
                        NotFoundTextBlock1.Text = "'" + PageSearchTxtBox.Text + "' not found";
                        NotFoundTextBlock.Height = Window.Current.Bounds.Height;
                        TopTab.Visibility = Visibility.Collapsed;
                        TopSearchTab.Visibility = Visibility.Collapsed;
                        BottomTab.Visibility = Visibility.Collapsed;
                        SearchBottomTab.Visibility = Visibility.Collapsed;
                        PageNumberPanel.Visibility = Visibility.Collapsed;
                    }
                    else
                        NotFoundTextBlock.Visibility = Visibility.Visible;
                        NextButton.Visibility = Visibility.Collapsed;
                        PrevButton.Visibility = Visibility.Collapsed;
                }
                e.Handled = true;
            }
        }

        private void PageSearchTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PageSearchTxtBox.SelectAll();
            NotFoundTextBlock.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Collapsed;
            PrevButton.Visibility = Visibility.Collapsed;
        }

        private void ClosePopUp(object sender, TappedRoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                GotoPageBottom.Visibility = Visibility.Collapsed;
                BottomTab.Visibility = Visibility.Visible;
                m_isButtonClicked = true;
            }

            PageDextTxtBox.Text = string.Empty;
        }

        private void PageDextTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            var text = textBox.Text;
            int result;
            var isInterger = int.TryParse(text, out result);
            if (isInterger)
                return;
            if (text.Length > 0)
                textBox.Text = text.Remove(text.Length - 1);
        }


        private async void OKButton_Click(object sender, RoutedEventArgs e)
        {
            int destPage = 0;
            if (!string.IsNullOrEmpty(PageDextTxtBox.Text))
            {
                bool result = int.TryParse(PageDextTxtBox.Text, out destPage);
                if (result)
                {
                    if (destPage > 0 && destPage <= pdfViewer.PageCount)
                        pdfViewer.GotoPage(destPage);
                    else
                    {
                        Windows.UI.Popups.MessageDialog messageDialog = new Windows.UI.Popups.MessageDialog(string.Format("There is no page numbered '{0}' in this document.", destPage.ToString()));
                        messageDialog.Options = Windows.UI.Popups.MessageDialogOptions.None;
                        messageDialog.Title = "Syncfusion PDF Viewer";
                        messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("OK"));
                        await messageDialog.ShowAsync();
                    }
                }
                PageDextTxtBox.Text = string.Empty;
            }
        }

        private void CurrentPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox currentPage = sender as TextBox;
            char[] pageNum = currentPage.Text.ToCharArray();
            int targetPagenumber = 0;
            bool gotoResult = int.TryParse(CurrentPage.Text, out targetPagenumber);
            Brush tempBackground = new SolidColorBrush(Colors.Transparent);
            if (gotoResult)
            {
                if (targetPagenumber > pdfViewer.PageCount || targetPagenumber < 0)
                {
                    tempBackground = currentPage.Background;
                    currentPage.Background = new SolidColorBrush(Colors.Red);
                    currentPage.Text = "";
                }
                else
                {
                    currentPage.Background = (tempBackground) as SolidColorBrush;
                }
            }
            foreach (char pageNo in pageNum)
            {
                if (!char.IsNumber(pageNo))
                {
                    currentPage.Background = new SolidColorBrush(Colors.Red);
                    currentPage.Text = "";
                }
            }
        }

        private void NotFoundOkButton_Click(object sender, RoutedEventArgs e)
        {
            TopTab.Visibility = Visibility.Visible;
            NotFoundTextBlock.Visibility = Visibility.Collapsed;
            PageNumberPanel.Visibility = Visibility.Visible;
        }

        private void Ink_Unchecked(object sender, RoutedEventArgs e)
        {
            pdfViewer.InkAnnotationCommand.Execute(false);
            m_isButtonClicked = true;
        }

        private void Highlight_Unchecked(object sender, RoutedEventArgs e)
        {
            pdfViewer.HighlightAnnotationCommand.Execute(false);
            m_isButtonClicked = true;
        }

        private void AnnotationButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isAnnotationTabVisible)
            {
                SearchTab.Visibility = Visibility.Collapsed;
                isSearchTabVisible = false;
                AnnotationTab.Visibility = Visibility.Visible;
                isAnnotationTabVisible = true;
            }
            else
            {
                AnnotationTab.Visibility = Visibility.Collapsed;
                UncheckOthers(AnnotationButton);
                pdfViewer.ResetAnnotationModeCommand.Execute(true);
                m_isButtonClicked = true;
                isAnnotationTabVisible = false;
            }
        }

        private void UnderlineButton_Checked(object sender, RoutedEventArgs e)
        {
            UncheckOthers(UnderlineButton);
            pdfViewer.UnderlineAnnotationCommand.Execute(true);
            m_isButtonClicked = true;
        }

        private void UnderlineButton_Unchecked(object sender, RoutedEventArgs e)
        {
            pdfViewer.UnderlineAnnotationCommand.Execute(false);
            m_isButtonClicked = true;
        }

        private void StrikethroughButton_Checked(object sender, RoutedEventArgs e)
        {
            UncheckOthers(StrikethroughButton);
            pdfViewer.StrikeoutAnnotationCommand.Execute(true);
            m_isButtonClicked = true;
        }

        private void StrikethroughButton_Unchecked(object sender, RoutedEventArgs e)
        {
            pdfViewer.StrikeoutAnnotationCommand.Execute(false);
            m_isButtonClicked = true;
        }

        private void LineButton_Checked(object sender, RoutedEventArgs e)
        {
            UncheckOthers(LineButton);
            pdfViewer.LineAnnotationCommand.Execute(true);
            m_isButtonClicked = true;
        }

        private void LineButton_Unchecked(object sender, RoutedEventArgs e)
        {
            pdfViewer.LineAnnotationCommand.Execute(false);
            m_isButtonClicked = true;
        }
        private void PopupButton_Checked(object sender, RoutedEventArgs e)
        {
            UncheckOthers(PopupButton);
            pdfViewer.PopupAnnotationCommand.Execute(true);
            m_isButtonClicked = true;
        }
        private void PopupButton_Unchecked(object sender, RoutedEventArgs e)
        {
            pdfViewer.PopupAnnotationCommand.Execute(false);
            m_isButtonClicked = true;
        }
     
        private void RectangleButton_Checked(object sender, RoutedEventArgs e)
        {
            UncheckOthers(RectangleButton);
            pdfViewer.RectangleAnnotationCommand.Execute(true);
            m_isButtonClicked = true;
        }

        private void RectangleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            pdfViewer.RectangleAnnotationCommand.Execute(false);
            m_isButtonClicked = true;
        }

        private void EllipseButton_Checked(object sender, RoutedEventArgs e)
        {
            UncheckOthers(EllipseButton);
            pdfViewer.EllipseAnnotationCommand.Execute(true);
            m_isButtonClicked = true;
        }

        private void EllipseButton_Unchecked(object sender, RoutedEventArgs e)
        {
            pdfViewer.EllipseAnnotationCommand.Execute(false);
            m_isButtonClicked = true;
        }

        private void CloseAnnotations_Click(object sender, RoutedEventArgs e)
        {
            AnnotationTab.Visibility = Visibility.Collapsed;
            isAnnotationTabVisible = false;
            UncheckOthers(sender);
        }

        void UncheckOthers(object sender)
        {
            ToggleButton checkedButton = sender as ToggleButton;

            if ((checkedButton == null) || ((InkButton != checkedButton) && (InkButton.IsChecked.Value)))
                InkButton.IsChecked = false;
            if ((checkedButton == null) || ((HighlightButton != checkedButton) && (HighlightButton.IsChecked.Value)))
                HighlightButton.IsChecked = false;
            if ((checkedButton == null) || ((UnderlineButton != checkedButton) && (UnderlineButton.IsChecked.Value)))
                UnderlineButton.IsChecked = false;
            if ((checkedButton == null) || ((StrikethroughButton != checkedButton) && (StrikethroughButton.IsChecked.Value)))
                StrikethroughButton.IsChecked = false;
            if ((checkedButton == null) || ((LineButton != checkedButton) && (LineButton.IsChecked.Value)))
                LineButton.IsChecked = false;
            if ((checkedButton == null) || ((RectangleButton != checkedButton) && (RectangleButton.IsChecked.Value)))
                RectangleButton.IsChecked = false;
            if ((checkedButton == null) || ((EllipseButton != checkedButton) && (EllipseButton.IsChecked.Value)))
                EllipseButton.IsChecked = false;
            if ((checkedButton == null) || ((PopupButton != checkedButton) && (PopupButton.IsChecked.Value)))
                PopupButton.IsChecked = false;
        }

        private void TextMarkup_Click(object sender, RoutedEventArgs e)
        {
            currentAnnotationMode = AnnotationMode.TextMarkupAnnotationMode;
            SwitchToAnnotationTab();
            TextMarkupAnnotationTab.Visibility = Visibility.Visible;
            m_isButtonClicked = true;
        }

        private void Shapes_Click(object sender, RoutedEventArgs e)
        {
            currentAnnotationMode = AnnotationMode.ShapesAnnotationMode;
            SwitchToAnnotationTab();
            ShapeAnnotationTab.Visibility = Visibility.Visible;
            m_isButtonClicked = true;
        }

        private void BookmarkButton_Click(object sender, RoutedEventArgs e)
        {
            isBookmarkClicked = true;
            isAnnotationMode = true;
            TopTab.Visibility = Visibility.Collapsed;
            BottomTab.Visibility = Visibility.Collapsed;
            MoreButton.IsChecked = false;
            m_isButtonClicked = true;
            PageNumberPanel.Visibility = Visibility.Collapsed;
            BookmarkLayer.Visibility = Visibility.Visible;
        }

        private void Bookmark_Click(object sender, RoutedEventArgs e)
        {

            if (!isBookmarkClicked)
            {
                BookmarkGrid.Visibility = Visibility.Visible;
                isBookmarkClicked = true;
            }
            else
            {
                BookmarkGrid.Visibility = Visibility.Collapsed;
                isBookmarkClicked = false;
            }

        }

        private void BookmarkTabBackButton_Click(object sender, RoutedEventArgs e)
        {
            isBookmarkClicked = false;
            isAnnotationMode = false;
            currentAnnotationMode = AnnotationMode.None;
            m_isButtonClicked = true;
            BookmarkLayer.Visibility = Visibility.Collapsed;
            TopTab.Visibility = Visibility.Visible;
            BottomTab.Visibility = Visibility.Visible;
            PageNumberPanel.Visibility = Visibility.Visible;
        }

        private void PageDextTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PageDextTxtBox.Text = string.Format("{0}", pdfViewer.PageNumber);
        }

        private void closeBookmarks_Click(object sender, RoutedEventArgs e)
        {
            BookmarkGrid.Visibility = Visibility.Collapsed;
            isBookmarkClicked = false;
        }

        private void CurrentPage_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = string.Empty;
        }

        private void CurrentPage_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == string.Empty)
                (sender as TextBox).Text = pdfViewer.PageNumber.ToString();
        }
    }
}
