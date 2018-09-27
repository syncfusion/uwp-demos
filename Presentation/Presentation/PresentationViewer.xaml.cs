#region File Using Directives
using Syncfusion.OfficeChartToImageConverter;
using Syncfusion.Presentation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Graphics.Printing;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Printing;
#endregion

namespace EssentialPresentation
{
    /// <summary>
    /// Created a Presentation Viewer to view PowerPoint slides
    /// </summary>
    public sealed partial class PPTXViewer : Page
    {
        #region Fields
        private int _currentIndex = -1;
        private int _slideCount;
        private StorageFolder _localFolder;
        private bool _isWindowsPhone;
        private IPresentation _presentation;
        private CancellationTokenSource _cancellationToken;
        private bool _isTaskRunning;
        private Task[] _task = new Task[1];
        private Dictionary<int, BitmapImage> _pageImages = null;
        private StorageFolder _printFolder;
        private int _convertedSlides;
        #endregion

        #region Constructor
        public PPTXViewer()
        {
            this.InitializeComponent();
            if (_isWindowsPhone)
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            if (_isWindowsPhone)
                Window.Current.SizeChanged += CurrentView_Changed;
            _pageImages = new Dictionary<int, BitmapImage>();
            _localFolder = ApplicationData.Current.LocalFolder.CreateFolderAsync("images", CreationCollisionOption.ReplaceExisting).AsTask().Result;
            _isWindowsPhone = ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");
            _cancellationToken = new CancellationTokenSource(); ;
        }
        #endregion

        #region Conversion

        /// <summary>
        /// Load the PowerPoint presentation for the initial view
        /// </summary>
        private async void LoadDefaultPPTXFile()
        {
            if (_isWindowsPhone)
                ThumbnailStackPanel.Background = new SolidColorBrush(Colors.White);
            Assembly assembly = this.GetType().GetTypeInfo().Assembly;
            using (Stream resource = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.Template.pptx"))
            {
                TitleTextBlock.Text = "Template.pptx";
                LoadPresentation(resource);
                if (!_isWindowsPhone)
                    await UpdateDeskTopLayout(_presentation);
                else if (_isWindowsPhone)
                    await UpdateMobileLayout(_presentation);
            }
        }

        /// <summary>
        /// Load the PowerPoint presentation to convert images
        /// </summary>
        /// <param name="pptxStream">The stream instance of PowerPoint file</param>
        private void LoadPresentation(Stream pptxStream)
        {
            _presentation = Presentation.Open(pptxStream);
            _slideCount = _presentation.Slides.Count;
            _presentation.ChartToImageConverter = new ChartToImageConverter();
        }

        /// <summary>
        /// Update the layout for thumbnail and main views for mobile environment
        /// </summary>
        /// <param name="presentation">The presentation to be viewed</param>
        /// <returns></returns>
        private async Task UpdateMobileLayout(IPresentation presentation)
        {
            borderHeader.Visibility = Visibility.Visible;
            loadingRing.Visibility = Visibility.Visible;
            loadingRing.IsActive = true;
            ThumbNailScrollViewer.Visibility = Visibility.Collapsed;
            LoadingStatusCanvas.Visibility = Visibility.Visible;
            ThumbNailScrollViewer.ChangeView(null, 1, null);
            int renderedSlides = 0;
            for (int i = 0; i < _presentation.Slides.Count; i++)
            {
                ISlide slide = _presentation.Slides[i];
                await ConvertSlide(slide);
                renderedSlides++;
                if (renderedSlides == 4 || slide.SlideNumber == _slideCount)
                {
                    for (int j = slide.SlideNumber - (renderedSlides - 1); j <= slide.SlideNumber; j++)
                    {
                        await AddMobileThumbnailView(j);
                    }
                    renderedSlides = 0;
                    loadingRing.Visibility = Visibility.Collapsed;
                }

                if ((_slideCount >= 4 && slide.SlideNumber == 4) || _slideCount < 4)
                {
                    loadingRing.Visibility = Visibility.Collapsed;
                    loadingRing.IsActive = false;
                    ThumbNailScrollViewer.Visibility = Visibility.Visible;
                    //LoadingStatusCanvas.Visibility = Visibility.Visible;
                }
            }
            LoadingStatusCanvas.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Update the layout for thumbnail and main views for desktop environment
        /// </summary>
        /// <param name="presentation">The presentation to be viewed</param>
        /// <returns></returns>
        private async Task UpdateDeskTopLayout(IPresentation presentation)
        {
            ThumbNailScrollViewer.Visibility = Visibility.Collapsed;
            TempThumbNailScrollViewer.Visibility = Visibility.Visible;
            MainViewImageGrid.Visibility = Visibility.Collapsed;
            loadingRing.Visibility = Visibility.Visible;
            loadingRing.IsActive = true;
            for (int i = 0; i < _presentation.Slides.Count; i++)
            {
                ISlide slide = _presentation.Slides[i];
                var filePath = await ConvertSlide(slide);
                RowDefinition row = new RowDefinition();
                ThumbnailGrid.RowDefinitions.Add(row);
                if (_presentation.Slides[0].SlideNumber == 0)
                    UpdateThumbnailGrid(filePath, slide.SlideNumber + 1, _cancellationToken);
                else
                    UpdateThumbnailGrid(filePath, slide.SlideNumber, _cancellationToken);
            }
            TempThumbNailScrollViewer.Visibility = Visibility.Collapsed;
            ThumbNailScrollViewer.Visibility = Visibility.Visible;
            MainViewImageGrid.Visibility = Visibility.Visible;
            loadingRing.IsActive = false;
            loadingRing.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Convert the slides to images and update the layout
        /// </summary>
        private async Task Render()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".pptx");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file == null)
            {
                if (!_isWindowsPhone)
                {
                    TempThumbNailScrollViewer.Visibility = Visibility.Collapsed;
                    ThumbNailScrollViewer.Visibility = Visibility.Visible;
                    MainViewImageGrid.Visibility = Visibility.Visible;
                    loadingRing.IsActive = false;
                    loadingRing.Visibility = Visibility.Collapsed;
                }
                return;
            }
            if (!_isWindowsPhone)
            {
                CancelRunningTask();
                _currentIndex = -1;
                ThumbNailScrollViewer.ChangeView(null, 1, null);
            }
            if (_isWindowsPhone)
            {
                ThumbNailScrollViewer.Visibility = Visibility.Collapsed;
                ThumbNailScrollViewer.ChangeView(null, 1, null);
                loadingRing.Visibility = Visibility.Visible;
                loadingRing.IsActive = true;
            }
            Dispose();
            _isTaskRunning = true;
            //Open the presentation stream and retrieve it in the presentation instance.
            Stream pptxStream = file.OpenStreamForReadAsync().Result;
            TitleTextBlock.Text = file.Name;
            LoadPresentation(pptxStream);
            if (!_isWindowsPhone)
            {
                foreach (ISlide slide in _presentation.Slides)
                {
                    if (_cancellationToken.IsCancellationRequested)
                        return;
                    string filePath = await ConvertSlide(slide);
                    RowDefinition row = new RowDefinition();
                    ThumbnailGrid.RowDefinitions.Add(row);
                    if (_cancellationToken.IsCancellationRequested)
                        return;
                    if (_presentation.Slides[0].SlideNumber == 0)
                        UpdateThumbnailGrid(filePath, slide.SlideNumber + 1, _cancellationToken);
                    else
                        UpdateThumbnailGrid(filePath, slide.SlideNumber, _cancellationToken);
                    if ((_presentation.Slides.Count >= 5 && slide.SlideNumber == 5) || _presentation.Slides.Count < 5)
                    {
                        TempThumbNailScrollViewer.Visibility = Visibility.Collapsed;
                        ThumbNailScrollViewer.Visibility = Visibility.Visible;
                        MainViewImageGrid.Visibility = Visibility.Visible;
                        loadingRing.IsActive = false;
                        loadingRing.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else if (_isWindowsPhone)
            {
                if (_cancellationToken.IsCancellationRequested)
                    return;
                LoadingStatusCanvas.Visibility = Visibility.Collapsed;
                int renderedSlides = 0;
                for (int i = 0; i < _slideCount; i++)
                {
                    ISlide slide = _presentation.Slides[i];
                    if (_cancellationToken.IsCancellationRequested)
                        return;
                    await ConvertSlide(slide);
                    renderedSlides++;
                    if (renderedSlides == 4 || slide.SlideNumber == _slideCount)
                    {
                        for (int j = slide.SlideNumber - (renderedSlides - 1); j <= slide.SlideNumber; j++)
                        {
                            await AddMobileThumbnailView(j);
                        }
                        renderedSlides = 0;
                    }
                    if ((_slideCount >= 4 && slide.SlideNumber == 4) || _slideCount < 4)
                    {
                        loadingRing.Visibility = Visibility.Collapsed;
                        loadingRing.IsActive = false;
                        ThumbNailScrollViewer.Visibility = Visibility.Visible;
                        LoadingStatusCanvas.Visibility = Visibility.Visible;
                    }
                }
                LoadingStatusCanvas.Visibility = Visibility.Collapsed;
            }
            pptxStream.Dispose();
            _isTaskRunning = false;
        }

        /// <summary>
        /// Convert a slide as image
        /// </summary>
        /// <param name="slide">The slide to be converted</param>
        /// <returns></returns>
        private async Task<String> ConvertSlide(ISlide slide)
        {
            string mainImageFileName;
            string thumbImageFileName;
            if (_presentation.Slides[0].SlideNumber > 0)
            {
                mainImageFileName = "Slide " + slide.SlideNumber.ToString() + ".jpg";
                thumbImageFileName = "ThumbSlide " + slide.SlideNumber.ToString() + ".jpg";
            }
            else
            {
                mainImageFileName = "Slide " + (slide.SlideNumber + 1) + ".jpg";
                thumbImageFileName = "ThumbSlide " + (slide.SlideNumber + 1) + ".jpg";
            }
            StorageFile mainImageStorageFile = await _localFolder.CreateFileAsync(mainImageFileName, CreationCollisionOption.ReplaceExisting);
            StorageFile thumbImageStorageFile = await _localFolder.CreateFileAsync(thumbImageFileName, CreationCollisionOption.ReplaceExisting);
            RenderingOptions options = new RenderingOptions() { ScaleX = 0.25f, ScaleY = 0.25f };
            await slide.SaveAsImageAsync(mainImageStorageFile, _cancellationToken.Token);
            await slide.SaveAsImageAsync(thumbImageStorageFile, options, _cancellationToken.Token);
            if (LoadingStatusCanvas != null && slide.SlideNumber == _slideCount)
                LoadingStatusCanvas.Visibility = Visibility.Collapsed;
            _convertedSlides = slide.SlideNumber;
            return mainImageStorageFile.Path;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Convert the slides as thumbnail images and udpate the thumbnail layout
        /// </summary>
        /// <param name="filePath">The path to save the thumbnail images</param>
        /// <param name="slideNumber">The slide number to convert the slide</param>
        /// <param name="cancellationToken">The cancellation token to cancel the task</param>
        private async void UpdateThumbnailGrid(string filePath, int slideNumber, CancellationTokenSource cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;
            Grid grid = new Grid();
            Grid imageGrid = new Grid();
            TextBlock textBlock = new TextBlock();
            Border textBorder = new Border();
            BitmapImage bitmap = new BitmapImage();

            string[] filePaths = filePath.Split('\\');
            string path = Path.GetFullPath(filePath).Replace(Path.GetFileNameWithoutExtension(filePath), "Thumb" + Path.GetFileNameWithoutExtension(filePath));

            bitmap.UriSource = new Uri(path);
            Image image = new Image();
            if (slideNumber == 1)
                grid.Margin = new Thickness(0, DefaultMargin20, DefaultMargin20, DefaultMargin10);
            else
                grid.Margin = new Thickness(0, DefaultMargin10, DefaultMargin20, DefaultMargin10);
            int origHeight = bitmap.PixelHeight;
            int origWidth = bitmap.PixelWidth;
            float ratioX = Default200Constant / (float)origWidth;
            float ratioY = Default200Constant / (float)origHeight;
            var ratio = Math.Min(ratioX, ratioY);
            int newHeight = (int)(origHeight * ratio);
            int newWidth = (int)(origWidth * ratio);
            bitmap.DecodePixelWidth = newWidth;
            bitmap.DecodePixelHeight = newHeight;
            image.Source = bitmap;
            image.PointerPressed += Image_PointerPressed;
            image.Stretch = Stretch.Fill;
            textBorder.Margin = new Thickness(0, 0, DefaultTextBorderMargin, 0);
            textBlock.FontFamily = new FontFamily(DefaultFontName);
            textBlock.FontSize = DefaultFontSize;
            textBlock.FontStyle = Windows.UI.Text.FontStyle.Normal;
            textBlock.Text = (slideNumber).ToString();
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            textBorder.HorizontalAlignment = HorizontalAlignment.Left;
            textBorder.VerticalAlignment = VerticalAlignment.Top;
            textBorder.Child = textBlock;
            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            col1.Width = new GridLength(DefaultMargin20);
            grid.ColumnDefinitions.Add(col1);
            grid.ColumnDefinitions.Add(col2);
            ThumbnailGrid.Children.Add(grid);
            Grid.SetRow(grid, (slideNumber - 1));
            imageGrid.Children.Add(image);
            imageGrid.BorderBrush = new SolidColorBrush(Color.FromArgb(Alpha, GreyRGB, GreyRGB, GreyRGB));
            imageGrid.BorderThickness = new Thickness(DefaultBorderThickness);
            grid.Children.Add(textBorder);
            Grid.SetColumn(textBorder, 0);
            grid.Children.Add(imageGrid);
            Grid.SetColumn(imageGrid, 1);
            if (slideNumber == 1)
            {
                _currentIndex = ThumbnailGrid.Children.IndexOf(grid);
                ApplyHighlightingToThumbnail(imageGrid);
                ApplyHighlightingToSlideNumber(textBlock);
                Image indexImage = await GetImage(0);
                UpdateMainViewImage(indexImage);
            }
        }

        /// <summary>
        /// Get the image for mobile view
        /// </summary>
        /// <param name="slideNumber">The slide index</param>
        private async Task AddMobileThumbnailView(int slideNumber)
        {
            if (_cancellationToken.IsCancellationRequested)
                return;
            Grid grid = new Grid();
            grid.Margin = new Thickness(Thickness * DefaultGridThickness, (Thickness / DivideByTwoConstant) * DefaultGridThickness, Thickness * DefaultGridThickness, (Thickness / DivideByTwoConstant) * DefaultGridThickness);
            grid.BorderThickness = new Thickness(DefaultBorderThickness);
            grid.BorderBrush = new SolidColorBrush(Colors.DarkGray);
            if (ApplicationView.GetForCurrentView().Orientation == ApplicationViewOrientation.Landscape)
            {
                grid.Height = LandscapeGridHeight;
                grid.Width = LandscapeGridWidth;
            }
            else
            {
                grid.Height = Default200Constant;
                grid.Width = Default300Constant;
            }
            grid.Background = new SolidColorBrush(Colors.White);
            grid.DoubleTapped += Grid_DoubleTapped;
            grid.Tapped += Grid_Tapped;
            Image image = new Image();
            BitmapImage bitmap = await GetBitmapImage(slideNumber - 1);
            image.Source = bitmap;
            image.Stretch = Stretch.Fill;
            grid.Children.Add(image);
            Border textBorder = new Border();
            textBorder.Background = new SolidColorBrush(Colors.Black);
            TextBlock textBlock = new TextBlock();
            textBlock.Text = (slideNumber).ToString();
            textBlock.Foreground = new SolidColorBrush(Colors.White);
            textBorder.HorizontalAlignment = HorizontalAlignment.Left;
            textBorder.VerticalAlignment = VerticalAlignment.Bottom;
            textBorder.Child = textBlock;
            grid.Children.Add(textBorder);
            LoadingStatusTextBlock.Text = "" + (slideNumber) + "/" + _slideCount + " Slides loaded successfully";
            if (_cancellationToken.IsCancellationRequested)
                return;
            ThumbnailStackPanel.Children.Add(grid);
        }

        /// <summary>
        /// Check if the image alredy existis in the location
        /// </summary>
        /// <param name="fileName">The file name to check</param>
        public async Task<bool> IsFilePresent(string fileName)
        {
            var item = await _localFolder.TryGetItemAsync(fileName);
            return item != null;
        }


        /// <summary>
        /// Check if the image alredy existis in the location
        /// </summary>
        /// <param name="fileName">The file name to check</param>
        public async Task<bool> IsPrintFilePresent(string fileName)
        {
            var item = await _printFolder.TryGetItemAsync(fileName);
            return item != null;
        }

        /// <summary>
        /// Read the image from the temp location for viewing
        /// </summary>
        /// <param name="currentIndex">The index of the slide to be viewed</param>
        /// <returns>Returns the specific image</returns>
        private async Task<BitmapImage> GetBitmapImage(int currentIndex)
        {
            string fileName = "ThumbSlide " + (currentIndex + 1).ToString() + ".jpg";
            if (await IsFilePresent(fileName))
            {
                StorageFile file = await _localFolder.GetFileAsync(fileName);
                return new BitmapImage(new Uri(file.Path));
            }
            else return new BitmapImage();
        }


        /// <summary>
        /// Check whether the image is visible to user
        /// </summary>
        /// <param name="element">The framework element to be verified</param>
        /// <param name="container">The framework element which have the element</param>
        /// <returns></returns>
        private bool IsVisibileToUser(FrameworkElement element, FrameworkElement container)
        {
            if (element == null || container == null || element.Visibility != Visibility.Visible)
                return false;
            Rect elementBounds = element.TransformToVisual(container).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
            Rect containerBounds = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);
            return (elementBounds.Top < (containerBounds.Height - element.ActualHeight) && elementBounds.Bottom > containerBounds.Top);
        }


        /// <summary>
        /// Read the image from the temp location for viewing
        /// </summary>
        /// <param name="currentIndex">The index of the slide to be viewed</param>
        /// <returns>Returns the specific image</returns>
        private async Task<Image> GetImage(int currentIndex)
        {
            Image image = new Image();
            string fileName = "Slide " + (currentIndex + 1).ToString() + ".jpg";
            BitmapImage img = null;
            if (await IsFilePresent(fileName))
            {
                StorageFile file = await _localFolder.GetFileAsync(fileName);
                img = new BitmapImage(new Uri(file.Path));
            }
            else img = new BitmapImage();
            image.Source = img;
            return image;
        }


        /// <summary>
        /// Update the scroll viewer of the thumbnail panel
        /// </summary>
        private void UpdateScrollViewer(bool isScrollDown, double thumbnailHeight)
        {
            if (isScrollDown)
            {
                if (ThumbNailScrollViewer.ViewportHeight < (_currentIndex * (thumbnailHeight + DefaultMargin20) + ViewPortRoundingConstant))
                {
                    var total = (_currentIndex * (thumbnailHeight + DefaultMargin20) + ViewPortRoundingConstant);
                    ThumbNailScrollViewer.ChangeView(null, total, 1.0F, false);
                }
            }
            else
            {
                if (ThumbNailScrollViewer.ViewportHeight > 0)
                {
                    var total = (_currentIndex * (thumbnailHeight + DefaultMargin20)) - (thumbnailHeight + DefaultMargin20);
                    ThumbNailScrollViewer.ChangeView(null, total, 1.0F, false);
                }
            }
        }

        /// <summary>
        /// Udpate the thumbnail panel for the unsection
        /// </summary>
        /// <param name="prevImageGrid">The previous slide to be unselected</param>
        private void UpdateUnSelectedSlide(Grid prevImageGrid)
        {
            if (prevImageGrid != null)
            {
                prevImageGrid.BorderBrush = new SolidColorBrush(Color.FromArgb(Alpha, GreyRGB, GreyRGB, GreyRGB));
                prevImageGrid.BorderThickness = new Thickness(DefaultBorderThickness);
            }
        }

        /// <summary>
        /// Udpate the thumbnail panel for the unsection
        /// </summary>
        /// <param name="textBlock">The previous slide to be unselected</param>
        private void UpdateUnSelectedSlideNumber(TextBlock textBlock)
        {
            if (textBlock != null)
                textBlock.Foreground = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Udpate the thumbnail panel for the new selection
        /// </summary>
        private void ApplyHighlightingToThumbnail(Grid thumbnailGrid)
        {
            if (thumbnailGrid != null)
            {
                thumbnailGrid.BorderBrush = new SolidColorBrush(Colors.Red);
                thumbnailGrid.BorderThickness = new Thickness(2);
            }
        }

        /// <summary>
        /// Udpate the thumbnail panel for the new selection
        /// </summary>
        private void ApplyHighlightingToSlideNumber(TextBlock textBlock)
        {
            if (textBlock != null)
                textBlock.Foreground = new SolidColorBrush(Colors.Red);
        }

        /// <summary>
        /// Udpate the main view for the new selection
        /// </summary>
        private void UpdateMainViewImage(Image image)
        {
            Image centerImage = new Image();
            MainViewImageGrid.Padding = new Thickness(DefaultBorderPadding);
            MainViewImageGrid.BorderThickness = new Thickness(DefaultBorderThickness);
            MainViewImageGrid.BorderBrush = new SolidColorBrush(Color.FromArgb(Alpha, GreyRGB, GreyRGB, GreyRGB));
            centerImage.Source = image.Source;
            centerImage.Stretch = Stretch.Fill;
            MainViewImageGrid.VerticalAlignment = VerticalAlignment.Center;
            MainViewImageGrid.HorizontalAlignment = HorizontalAlignment.Center;
            MainViewImageGrid.Margin = new Thickness(DefaultMargin20);
            if (MainViewImageGrid.Children.Count == 0)
                MainViewImageGrid.Children.Add(centerImage);
            else
                MainViewImageGrid.Children[0] = centerImage;
        }

        #endregion

        #region Events
        /// <summary>
        /// Triggered when thee mobile environment back button is pressed
        /// </summary>
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            DeleteLocalFolder();
            if (_isWindowsPhone)
                ThumbnailStackPanel.Children.Clear();
        }

        /// <summary>
        /// Trigger when the current view is changed 
        /// </summary>
        private void CurrentView_Changed(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            ApplicationView currentView = ApplicationView.GetForCurrentView();
            if (currentView.Orientation == ApplicationViewOrientation.Landscape)
            {
                ThumbnailStackPanel.Orientation = Orientation.Horizontal;
                foreach (Grid grid in ThumbnailStackPanel.Children)
                {
                    grid.Width = LandscapeGridWidth;
                    grid.Height = LandscapeGridHeight;
                }
                MainImageScrollViewer.ChangeView(MainImageScrollViewer.HorizontalOffset, MainImageScrollViewer.VerticalOffset, 1F);
                ThumbNailScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                ThumbNailScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                ThumbNailScrollViewer.HorizontalSnapPointsType = SnapPointsType.OptionalSingle;
            }
            else if (currentView.Orientation == ApplicationViewOrientation.Portrait)
            {
                ThumbnailStackPanel.Orientation = Orientation.Vertical;
                foreach (Grid grid in ThumbnailStackPanel.Children)
                {
                    grid.Width = Default300Constant;
                    grid.Height = Default200Constant;
                }
                MainImageScrollViewer.ChangeView(MainImageScrollViewer.HorizontalOffset, MainImageScrollViewer.VerticalOffset, 1F);
                ThumbNailScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                ThumbNailScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
        }

        /// <summary>
        /// Layout the display for mobile environment
        /// </summary>
        private void MobileOpenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isTaskRunning)
                {
                    DeleteLocalFolder();
                    _cancellationToken.Cancel();
                }
                if (_task[0] != null)
                    Task.WaitAll(_task, 500);
            }
            catch (AggregateException)
            {
            }
            finally
            {
                _cancellationToken = new CancellationTokenSource();
            }
            _currentIndex = -1;
            _task[0] = Render();
        }

        /// <summary>
        /// Layout the window for desktop enviromnet
        /// </summary>
        private void DesktopOpenButton_Click(object sender, PointerRoutedEventArgs e)
        {
            _cancellationToken.Cancel();
            TempThumbNailScrollViewer.Visibility = Visibility.Visible;
            MainViewImageGrid.Visibility = Visibility.Collapsed;
            ThumbNailScrollViewer.Visibility = Visibility.Collapsed;
            loadingRing.Visibility = Visibility.Visible;
            loadingRing.IsActive = true;
            _task[0] = Render();
        }


        /// <summary>
        /// Triggered when the entire display is double tapped. Used in mobile view
        /// </summary>
        private async void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (ApplicationView.GetForCurrentView().Orientation == ApplicationViewOrientation.Landscape)
            {
                return;
            }
            int index;
            OpenTextBlock.Visibility = Visibility.Collapsed;
            MainThumnailFooter.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            LoadingStatusCanvas.Visibility = Visibility.Collapsed;
            MainImageScrollViewer.Background = new SolidColorBrush(Colors.DarkGray);
            BackLabelButton.Visibility = Visibility.Visible;
            NextLabelButton.Visibility = Visibility.Visible;
            ThumbNailScrollViewer.Visibility = Visibility.Collapsed;
            MainImageScrollViewer.Visibility = Visibility.Visible;
            openButton.Visibility = Visibility.Collapsed;
            Grid mainImageGrid = sender as Grid;
            _currentIndex = ThumbnailStackPanel.Children.IndexOf(mainImageGrid);
            UpdateUnSelectedSlide(mainImageGrid);
            Image mainViewImage = await UpdateMainView();
            if (MainImageScrollViewer.ViewportHeight == 0 && MainImageScrollViewer.ViewportWidth == 0)
            {
                mainViewImage.Height = Default200Constant;
                mainViewImage.Width = Default300Constant;
            }
            else
            {
                mainViewImage.Height = MainImageScrollViewer.ViewportHeight / DivideByViewPortHeightConstant;
                mainViewImage.Width = MainImageScrollViewer.ViewportWidth / DivideByViewPortWidthConstant;
            }
            MainImageScrollViewer.Content = mainViewImage;
            Slidestatus.Visibility = Visibility.Visible;
            index = _currentIndex + 1;
            Slidestatus.Text = "" + index + "/" + _slideCount;
        }

        /// <summary>
        /// Update the center display image
        /// </summary>
        private async Task<Image> UpdateMainView()
        {
            Image indexImage = await GetImage(_currentIndex);
            return new Image
            {
                Source = indexImage.Source,
                Stretch = Stretch.Fill,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
        }

        /// <summary>
        /// Triggered to switch the view is mobile from thumbnail view to main view
        /// </summary>
        private void BackImage_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Slidestatus.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Collapsed;
            OpenTextBlock.Visibility = Visibility.Visible;
            MainThumnailFooter.Visibility = Visibility.Collapsed;
            BackLabelButton.Visibility = Visibility.Collapsed;
            NextLabelButton.Visibility = Visibility.Collapsed;
            ThumbNailScrollViewer.Visibility = Visibility.Visible;
            openButton.Visibility = Visibility.Visible;
            MainImageScrollViewer.ChangeView(null, null, 1);
            MainImageScrollViewer.Visibility = Visibility.Collapsed;
            if (_isTaskRunning)
                LoadingStatusCanvas.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Triggered to change the thumbnail view to main view in mobile environment
        /// </summary>
        private async void PreviousLabelButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 1;
            Image prevImg = MainImageScrollViewer.Content as Image;
            if (_currentIndex > 0)
            {
                _currentIndex--;
                Image mainViewImage = await UpdateMainView();
                if (MainImageScrollViewer.ViewportHeight == 0 && MainImageScrollViewer.ViewportWidth == 0)
                {
                    mainViewImage.Height = Default200Constant;
                    mainViewImage.Width = Default300Constant;
                }
                else
                {
                    if (ApplicationView.GetForCurrentView().Orientation == ApplicationViewOrientation.Landscape)
                    {
                        mainViewImage.Height = prevImg.Height;
                        mainViewImage.Width = prevImg.Width;
                    }
                    else
                    {
                        mainViewImage.Height = MainImageScrollViewer.ViewportHeight / DivideByViewPortHeightConstant;
                        mainViewImage.Width = MainImageScrollViewer.ViewportWidth / DivideByViewPortWidthConstant;
                    }
                }
                MainImageScrollViewer.Content = mainViewImage;
                MainImageScrollViewer.ChangeView(MainImageScrollViewer.HorizontalOffset, MainImageScrollViewer.VerticalOffset, 1F);
                Slidestatus.Visibility = Visibility.Visible;
                index = _currentIndex + 1;
                Slidestatus.Text = "" + index + "/" + _slideCount;
            }
        }

        /// <summary>
        /// Set the layout for display. Initialize the layout in both mobile and desktop environments
        /// </summary>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Dispose();
            LoadDefaultPPTXFile();
        }

        /// <summary>
        /// Triggered to view the next image in main view of mobile environments
        /// </summary>
        private async void NextLabelButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 1;
            Image prevImg = MainImageScrollViewer.Content as Image;
            if (_currentIndex < _slideCount - 1)
            {
                _currentIndex++;
                Image mainViewImage = await UpdateMainView();
                if (MainImageScrollViewer.ViewportHeight == 0 && MainImageScrollViewer.ViewportWidth == 0)
                {
                    mainViewImage.Height = Default200Constant;
                    mainViewImage.Width = Default300Constant;
                }
                else
                {
                    if (ApplicationView.GetForCurrentView().Orientation == ApplicationViewOrientation.Landscape)
                    {
                        mainViewImage.Height = prevImg.Height;
                        mainViewImage.Width = prevImg.Width;
                    }
                    else
                    {
                        mainViewImage.Height = MainImageScrollViewer.ViewportHeight / DivideByViewPortHeightConstant;
                        mainViewImage.Width = MainImageScrollViewer.ViewportWidth / DivideByViewPortWidthConstant;
                    }
                }
                MainImageScrollViewer.Content = mainViewImage;
                MainImageScrollViewer.ChangeView(MainImageScrollViewer.HorizontalOffset, MainImageScrollViewer.VerticalOffset, 1F);
                Slidestatus.Visibility = Visibility.Visible;
                index = _currentIndex + 1;
                Slidestatus.Text = "" + index + "/" + _slideCount;
            }
        }

        /// <summary>
        /// Triggered to view the main image in mobile environment
        /// </summary>
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_currentIndex > -1)
            {
                Grid prevGrid = ((ThumbnailStackPanel.Children[_currentIndex] as Grid));
                UpdateUnSelectedSlide(prevGrid);
            }
            Grid thumbnailGrid = sender as Grid;
            _currentIndex = ThumbnailStackPanel.Children.IndexOf(thumbnailGrid);
            ApplyHighlightingToThumbnail(thumbnailGrid);
        }

        /// <summary>
        /// Cancel the active task to render images
        /// </summary>
        private void CancelRunningTask()
        {
            if (_isTaskRunning)
                _cancellationToken.Cancel();

            try
            {
                if (_task[0] != null)
                    Task.WaitAll(_task, 500);
            }
            catch (AggregateException)
            {
            }
            finally
            {
                _cancellationToken = new CancellationTokenSource();
            }
        }

        /// <summary>
        /// The pointer device initiates a Press action within the element
        /// </summary>
        private async void Image_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (_currentIndex == -1)
            {
                Grid prevGrid = ThumbnailGrid.Children[0] as Grid;
                Grid imageGrid = prevGrid.Children[1] as Grid;
                Border border = prevGrid.Children[0] as Border;
                TextBlock text = border.Child as TextBlock;
                UpdateUnSelectedSlide(imageGrid);
                UpdateUnSelectedSlideNumber(text);
            }
            if (_currentIndex > -1)
            {
                Grid prevGrid = (ThumbnailGrid.Children[_currentIndex] as Grid);
                Grid imageGrid = prevGrid.Children[1] as Grid;
                Border border = prevGrid.Children[0] as Border;
                TextBlock text = border.Child as TextBlock;
                UpdateUnSelectedSlide(imageGrid);
                UpdateUnSelectedSlideNumber(text);
            }
            Grid thumbnailGrid = (sender as Image).Parent as Grid;
            ApplyHighlightingToThumbnail(thumbnailGrid);
            Grid mainGrid = (thumbnailGrid).Parent as Grid;
            Border textBorder = mainGrid.Children[0] as Border;
            TextBlock textBlock = textBorder.Child as TextBlock;
            ApplyHighlightingToSlideNumber(textBlock);
            _currentIndex = ThumbnailGrid.Children.IndexOf(mainGrid);
            Image indexImage = await GetImage(_currentIndex);
            UpdateMainViewImage(indexImage);
        }
        /// <summary>
        /// Get the key down events to handle the thumbnail navigation
        /// </summary>
        /// <param name="sender">The windows that sends the events</param>
        /// <param name="args">The key event properties those were triggered</param>
        private async void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            Grid grid = ThumbnailGrid.Children[0] as Grid;
            if (args.VirtualKey == VirtualKey.Down)
            {
                if (_currentIndex < ThumbnailGrid.Children.Count - 1)
                {
                    Grid prevGrid = (ThumbnailGrid.Children[_currentIndex] as Grid);
                    Grid prevImageGrid = prevGrid.Children[1] as Grid;
                    Border border = prevGrid.Children[0] as Border;
                    TextBlock text = border.Child as TextBlock;
                    UpdateUnSelectedSlideNumber(text);
                    UpdateUnSelectedSlide(prevImageGrid);
                    _currentIndex++;
                    Grid thumbnailGrid = ThumbnailGrid.Children[_currentIndex] as Grid;
                    Grid thumbnailImageGrid = thumbnailGrid.Children[1] as Grid;
                    ApplyHighlightingToThumbnail(thumbnailImageGrid);
                    Border textBorder = thumbnailGrid.Children[0] as Border;
                    TextBlock textBlock = textBorder.Child as TextBlock;
                    ApplyHighlightingToSlideNumber(textBlock);
                    Image indexImage = await GetImage(_currentIndex);
                    UpdateMainViewImage(indexImage);
                    bool value = IsVisibileToUser(thumbnailGrid, ThumbNailScrollViewer);
                    if (value == false)
                        UpdateScrollViewer(true, grid.ActualHeight);
                }
            }
            else if (args.VirtualKey == VirtualKey.Up)
            {
                UpdateScrollViewer(false, grid.ActualHeight);
                if (_currentIndex > 0)
                {
                    Grid prevGrid = (ThumbnailGrid.Children[_currentIndex] as Grid);
                    Grid imageGrid = prevGrid.Children[1] as Grid;
                    Border border = prevGrid.Children[0] as Border;
                    TextBlock text = border.Child as TextBlock;
                    UpdateUnSelectedSlideNumber(text);
                    UpdateUnSelectedSlide(imageGrid);
                    _currentIndex--;
                    Grid thumbnailGrid = ThumbnailGrid.Children[_currentIndex] as Grid;
                    Grid thumbnailImageGrid = thumbnailGrid.Children[1] as Grid;
                    ApplyHighlightingToThumbnail(thumbnailImageGrid);
                    Border textBorder = thumbnailGrid.Children[0] as Border;
                    TextBlock textBlock = textBorder.Child as TextBlock;
                    ApplyHighlightingToSlideNumber(textBlock);
                    Image indexImage = await GetImage(_currentIndex);
                    UpdateMainViewImage(indexImage);
                }
            }
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Dispose the elements
        /// </summary>
        private void Dispose()
        {
            DeleteLocalFolder();
            if (_presentation != null)
            {
                _presentation.Dispose();
                _presentation = null;
            }
            if (!_isWindowsPhone)
            {
                Image image = MainViewImageGrid.Children[0] as Image;
                if (image != null)
                {
                    image.ClearValue(Image.SourceProperty);
                    image.ClearValue(Image.StretchProperty);
                }
                MainViewImageGrid.Children.Clear();
                ThumbnailGrid.Children.Clear();
            }
            if (_isWindowsPhone)
                if (ThumbnailStackPanel != null)
                    ThumbnailStackPanel.Children.Clear();
        }

        /// <summary>
        /// Delete the temporary folders created by this application
        /// </summary>
        private async void DeleteLocalFolder()
        {
            await _localFolder.DeleteAsync(StorageDeleteOption.PermanentDelete);
        }
        #endregion
    }
}
