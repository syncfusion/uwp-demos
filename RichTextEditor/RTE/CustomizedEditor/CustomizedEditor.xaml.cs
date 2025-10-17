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
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;
using Syncfusion.UI.Xaml.Controls.Media;
using Syncfusion.UI.Xaml.Controls.Input;
using Windows.UI.Popups;
using Windows.UI;
using Windows.UI.Xaml.Shapes;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Printing;
using Windows.Graphics.Printing;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.Phone.UI.Input;
using Windows.ApplicationModel.Activation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RTEDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomizedEditor : Page, INotifyPropertyChanged, IDisposable
    {
        #region Fields
        IPrintDocumentSource printDocumentSource = null;
        Task<bool> loadAsync = null;
        CancellationTokenSource cancellationTokenSource = null;
        Popup popup;
        Popup colorPopup;
        Windows.UI.Xaml.Controls.Border popupBorder;
        HighlightColorPicker highlightColorPicker;
        SpacingsFormatGrid spacingsFormatGrid;
        FontFormatGrid fontFormatGrid;
        ParaFormatGrid paraFormatGrid;
        ReviewOptionsGrid reviewOptionsGrid;
        InsertTableGrid tableGrid;
        SfColorPicker colorPicker;
        ListFormatGrid listMenuGrid;
        ListAdv list;
        int levelNumber;
        List<BitmapImage> pageImages = new List<BitmapImage>();
        ClipBoardOptionsGrid clipboardOptionsGrid;
        // For phone
        Windows.UI.ViewManagement.InputPane inputPane = null;
        bool isInsertingImage = false;
        bool isContentChanged = false;
        bool isFormatting = false;
        bool isFormatPaneOpening = false;
        bool isOpenFile = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizedEditor"/> class.
        /// </summary>
        public CustomizedEditor()
        {
            this.InitializeComponent();
            Stream inputStream = null;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                inputPane = Windows.UI.ViewManagement.InputPane.GetForCurrentView();
                richTextBox.IsTextPredictionEnabled = true;
                richTextBox.GripperSize = 16;
                InitializeAppbarBindings();
                inputPane.Showing += InputPane_Showing;
                inputPane.Hiding += InputPane_Hiding;
                this.richTextBox.ContentChanged += richTextBox_ContentChanged;
                this.richTextBox.GotFocus += RichTextBox_GotFocus;
                inputStream = GetManifestResourceStream("GettingStarted_Phone.docx");
            }
            else
            {
                this.richTextBox.SelectionChanged += RichTextBox_SelectionChanged;
                InitClipBoardOptionsGrid();
                inputStream = GetManifestResourceStream("GettingStarted.docx");
                InitPopupUI();
                InitColorPickerUI();
                InitHighlightColorGridViewUI();
                InitFontFormatGridUI();
                InitParaFormatGridUI();
                InitListFormatGridUI();
                InitTableGridUI();
                InitSpacingsFormatGridUI();
                InitCommentsGridUI();
                InitSpellChecker();
            }
            #region Common for Phone and Windows 
            this.richTextBox.Load(inputStream, FormatType.Docx);
            this.richTextBox.DocumentTitle = "GettingStarted";
            this.Unloaded += DocumentEditor_Unloaded;
            this.Loaded += DocumentEditor_Loaded;
            this.richTextBox.PrintCompleted += RichTextBoxAdv_PrintCompleted;
            this.richTextBox.RequestNavigate += RichTextBoxAdv_RequestNavigate;
            #endregion
        }
        #endregion

        #region Helper Methods
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
        #endregion

        #region Specific for Phone

        #region Implementation
        /// <summary>
        /// Initializes the appbar bindings.
        /// </summary>
        private void InitializeAppbarBindings()
        {
            createButton.Command = richTextBox.NewDocumentCommand;
            // Font color buttons binding
            orangeFCButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.FontColor"), Mode = BindingMode.TwoWay, Converter = new FontColorToggleConverter(), ConverterParameter = "Orange" });
            greenFCButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.FontColor"), Mode = BindingMode.TwoWay, Converter = new FontColorToggleConverter(), ConverterParameter = "Green" });
            redFCButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.FontColor"), Mode = BindingMode.TwoWay, Converter = new FontColorToggleConverter(), ConverterParameter = "Red" });
            // Highlight color buttons binding
            yellowHCButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.HighlightColor"), Mode = BindingMode.TwoWay, Converter = new HighlightColorToggleConverter(), ConverterParameter = "Yellow" });
            greenHCButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.HighlightColor"), Mode = BindingMode.TwoWay, Converter = new HighlightColorToggleConverter(), ConverterParameter = "Green" });
            // Bold,italic,underline,strikethrough buttons binding
            redHCButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.HighlightColor"), Mode = BindingMode.TwoWay, Converter = new HighlightColorToggleConverter(), ConverterParameter = "Red" });
            boldButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.Bold"), Mode = BindingMode.TwoWay });
            italicButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.Italic"), Mode = BindingMode.TwoWay });
            underlineButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.Underline"), Mode = BindingMode.TwoWay, Converter = new UnderlineToggleConverter() });
            strikeButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.StrikeThrough"), Mode = BindingMode.TwoWay, Converter = new SingleStrikeThroughToggleConverter() });
        }
        /// <summary>
        /// Disposes the appbar bindings.
        /// </summary>
        private void DisposeAppbarBindings()
        {
            createButton.Command = null;
            // Font color buttons binding
            orangeFCButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            greenFCButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            redFCButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            // Highlight color buttons binding
            yellowHCButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            greenHCButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            // Bold,italic,underline,strikethrough buttons binding
            redHCButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            boldButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            italicButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            underlineButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            strikeButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
        }
        /// <summary>
        /// Saves this instance.
        /// </summary>
        public async void Save()
        {
            isContentChanged = false;
            FileSavePicker savePicker = new FileSavePicker()
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".docx" });
            savePicker.FileTypeChoices.Add("Word 97 - 2003 Document", new List<string>() { ".doc" });
            savePicker.FileTypeChoices.Add("Word Template", new List<string>() { ".dotx" });
            savePicker.FileTypeChoices.Add("Word 97 - 2003 Template", new List<string>() { ".dot" });
            savePicker.FileTypeChoices.Add("Rich Text Document", new List<string>() { ".rtf" });
            savePicker.DefaultFileExtension = ".docx";
            savePicker.SuggestedFileName = string.IsNullOrEmpty(richTextBox.DocumentTitle) ? "New Document" : richTextBox.DocumentTitle;
            StorageFile file = await savePicker.PickSaveFileAsync();
            ContinueFileSavePicker(file);
        }
        #endregion

        #region Click Events
        /// <summary>
        /// Handles the Click event of the EditButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (richTextBox.IsReadOnly)
                richTextBox.IsReadOnly = false;
            else
                richTextBox.IsReadOnly = true;
            richTextBox.Focus(FocusState.Pointer);
        }
        /// <summary>
        /// Handles the Click event of the NextButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.SelectionChanged -= RichTextBox_SelectionChanged;
            FindText();
            richTextBox.Focus(FocusState.Pointer);
            richTextBox.SelectionChanged += RichTextBox_SelectionChanged;
        }
        /// <summary>
        /// Handles the Click event of the FindButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            openButton.Visibility = Visibility.Collapsed;
            createButton.Visibility = Visibility.Collapsed;
            saveButton.Visibility = Visibility.Collapsed;
            editButton.Visibility = Visibility.Collapsed;
            formatButton.Visibility = Visibility.Collapsed;
            findButton.Visibility = Visibility.Collapsed;
            insertImageButton.Visibility = Visibility.Collapsed;
            nextButton.Visibility = Visibility.Visible;
            richTextBox.HorizontalScrollBarVisibility = false;
            findGrid.Visibility = Visibility.Visible;
            if (string.IsNullOrEmpty(richTextBox.Selection.Text) || richTextBox.Selection.Text.Contains("/r"))
                findTextBox.Text = "";
            else
                findTextBox.Text = richTextBox.Selection.Text;
            findTextBox.SelectionStart = richTextBox.Selection.Text.Length;
            BottomAppBar.Visibility = Visibility.Collapsed;
            richTextBox.IsReadOnly = true;
            findTextBox.Focus(FocusState.Pointer);
        }
        /// <summary>
        /// Handles the Click event of the OpenFileButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (isContentChanged)
            {
                isOpenFile = true;
                PopupToSave();
            }
            else
                Open();
        }
        /// <summary>
        /// Opens this instance.
        /// </summary>
        private async void Open()
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker()
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            fileOpenPicker.FileTypeFilter.Add(".doc");
            fileOpenPicker.FileTypeFilter.Add(".dot");
            fileOpenPicker.FileTypeFilter.Add(".docx");
            fileOpenPicker.FileTypeFilter.Add(".dotx");
            fileOpenPicker.FileTypeFilter.Add(".rtf");
            StorageFile file = await fileOpenPicker.PickSingleFileAsync();
            ContinueFileOpenPicker(file);
        }
        /// <summary>
        /// Handles the Click event of the SaveFileButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
        /// <summary>
        /// Handles the Click event of the PrintButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.PrintDocumentCommand.Execute(null);
        }
        /// <summary>
        /// Handles the Click event of the InsertImageButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void InsertImageButton_Click(object sender, RoutedEventArgs e)
        {
            isInsertingImage = true;
            FileOpenPicker fileOpenPicker = new FileOpenPicker()
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.FileTypeFilter.Add(".jpeg");
            fileOpenPicker.FileTypeFilter.Add(".bmp");
            fileOpenPicker.FileTypeFilter.Add(".png");
            StorageFile file= await fileOpenPicker.PickSingleFileAsync();
            ContinueFileOpenPicker(file);
        }
        /// <summary>
        /// Handles the Click event of the CreateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (isContentChanged)
                PopupToSave();
            richTextBox.NewDocumentCommand.Execute(null);
        }
        /// <summary>
        /// Handles the Click event of the FormattingOptionButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void FormattingOptionButton_Click(object sender, RoutedEventArgs e)
        {
            HideFormatOptions();
        }
        /// <summary>
        /// Hides the format options.
        /// </summary>
        private void HideFormatOptions()
        {
            richTextBox.IsReadOnly = true;
            richTextBox.Focus(FocusState.Pointer);
            formatGrid.Visibility = Visibility.Collapsed;
            BottomAppBar.Visibility = Visibility.Visible;
            richTextBox.HorizontalScrollBarVisibility = true;
        }
        /// <summary>
        /// Hides the find options.
        /// </summary>
        private void HideFindOptions()
        {
            openButton.Visibility = Visibility.Visible;
            createButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Visible;
            editButton.Visibility = Visibility.Visible;
            formatButton.Visibility = Visibility.Visible;
            findButton.Visibility = Visibility.Visible;
            insertImageButton.Visibility = Visibility.Visible;
            nextButton.Visibility = Visibility.Collapsed;
            richTextBox.HorizontalScrollBarVisibility = true;
            findGrid.Visibility = Visibility.Collapsed;
            BottomAppBar.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Finds the text.
        /// </summary>
        private void FindText()
        {
            TextSearchResult textresult = richTextBox.Find(findTextBox.Text, FindOptions.None);
            if (textresult == null)
                NotifyNoResultsFound();
            else if (textresult != null && BottomAppBar.Visibility != Visibility.Visible)
                BottomAppBar.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Notifies the no results found.
        /// </summary>
        async private void NotifyNoResultsFound()
        {
            MessageDialog result = new MessageDialog("The search item wasn't found");
            result.Title = "Can't Find";
            result.Commands.Add(new UICommand("ok", CommandInvokedHandler));
            await result.ShowAsync();
        }
        /// <summary>
        /// Handles the Click event of the GrowFontSizeButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void IncreaseFontSizeButton_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.Selection.CharacterFormat.FontSize += 1;
            HideFormatOptions();
        }
        /// <summary>
        /// Handles the Click event of the ShrinkFontSizeButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DecreaseFontSizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (richTextBox.Selection.CharacterFormat.FontSize >= 2)
                richTextBox.Selection.CharacterFormat.FontSize -= 1;
            HideFormatOptions();
        }
        /// <summary>
        /// Handles the Click event of the FormatButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void FormatButton_Click(object sender, RoutedEventArgs e)
        {
            isFormatPaneOpening = true;
            BottomAppBar.Visibility = Visibility.Collapsed;
            if (inputPaneGrid.Visibility == Visibility.Visible)
                richTextBox.IsReadOnly = true;
            else
                richTextBox.IsReadOnly = false;
            isFormatting = true;
            richTextBox.Focus(FocusState.Pointer);
            richTextBox.HorizontalScrollBarVisibility = false;
            formatGrid.Visibility = Visibility.Visible;
        }
        #endregion

        #region Find Implementation
        /// <summary>
        /// Handles the KeyDown event of the findTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyRoutedEventArgs"/> instance containing the event data.</param>
        private void FindTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            richTextBox.SelectionChanged -= RichTextBox_SelectionChanged;
            if (e.Key.Equals(Windows.System.VirtualKey.Enter))
            {
                FindText();
                richTextBox.IsReadOnly = true;
                richTextBox.Focus(Windows.UI.Xaml.FocusState.Pointer);
            }
            richTextBox.SelectionChanged += RichTextBox_SelectionChanged;
        }
        /// <summary>
        /// Handles the TextChanged event of the findTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void FindTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (findTextBox.Text == "")
            {
                deleteButton.Visibility = Visibility.Collapsed;
                findIcon.Visibility = Visibility.Visible;
            }
            else
            {
                findIcon.Visibility = Visibility.Collapsed;
                if (findTextBox.FocusState != FocusState.Unfocused)
                    deleteButton.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Handles the Click event of the deleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            findTextBox.Text = "";
        }
        /// <summary>
        /// Handles the LostFocus event of the findTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void FindTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            deleteButton.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Handles the GotFocus event of the findTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void FindTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (findTextBox.Text != "")
                deleteButton.Visibility = Visibility.Visible;
            if (BottomAppBar.Visibility == Visibility.Visible && findGrid.Visibility == Visibility.Visible)
                BottomAppBar.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Continuation Manager
        /// <summary>
        /// Continues the file open picker.
        /// </summary>
        /// <param name="fileOpenPickerContinuationEventArgs">The <see cref="FileOpenPickerContinuationEventArgs"/> instance containing the event data.</param>
        public async void ContinueFileOpenPicker(StorageFile storagefile)
        {
            if (storagefile != null)
            {
                StorageFile file = storagefile;
                if (isInsertingImage)
                {
                    richTextBox.IsReadOnly = false;
                    richTextBox.InsertPictureCommand.Execute(file);
                    richTextBox.IsReadOnly = true;
                    isInsertingImage = false;
                }
                else
                {
                    busy.Visibility = Visibility.Visible;
                    bool showErrorMsg = false;
                    bool showPasswordProtectedMsg = false;
                    string errormessage = string.Empty;
                    if (file != null)
                    {
                        try
                        {
                            cancellationTokenSource = new CancellationTokenSource();
                            loadAsync = this.richTextBox.LoadAsync(file, cancellationTokenSource.Token);
                            await loadAsync;
                        }
                        catch (Exception ex)
                        {
                            showErrorMsg = true;
                            if (ex.Message.Contains("password"))
                                showPasswordProtectedMsg = true;
                            errormessage = ex.Message;
                        }
                        if (showErrorMsg)
                        {
                            if (!cancellationTokenSource.IsCancellationRequested)
                            {
                                if (showPasswordProtectedMsg)
                                {
                                    MessageDialog result = new MessageDialog("Password-protected files can't be opened", "Can't Open");
                                    result.Commands.Add(new UICommand("Ok", CommandInvokedHandler));
                                    await result.ShowAsync();
                                }
                                else
                                {
                                    MessageDialog dialog = null;
                                    if (errormessage.Contains("Access is denied"))
                                        dialog = new MessageDialog("This file cannot be open because it is being used by another process");
                                    else
                                        dialog = new MessageDialog("The input document could not be processed completely, Could you please email the document to support@syncfusion.com for troubleshooting.");
                                    await dialog.ShowAsync();
                                }
                            }
                        }
                    }
                    if (cancellationTokenSource != null)
                        cancellationTokenSource.Dispose();
                    cancellationTokenSource = null;
                    loadAsync = null;
                    busy.Visibility = Visibility.Collapsed;
                }
            }
        }
        /// <summary>
        /// Continues the file save picker.
        /// </summary>
        /// <param name="fileSavePickerContinuationEventArgs">The <see cref="FileSavePickerContinuationEventArgs"/> instance containing the event data.</param>
        public void ContinueFileSavePicker(StorageFile storagefile)
        {
            if (storagefile != null)
            {
                System.Threading.CancellationTokenSource src = new System.Threading.CancellationTokenSource();
                richTextBox.Save(storagefile);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Inputs the pane_ hiding.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="InputPaneVisibilityEventArgs"/> instance containing the event data.</param>
        void InputPane_Hiding(Windows.UI.ViewManagement.InputPane sender, InputPaneVisibilityEventArgs args)
        {
            inputPaneGrid.Height = 0;
            inputPaneGrid.Visibility = Visibility.Collapsed;
            if (isFormatPaneOpening)
            {
                isFormatPaneOpening = false;
                return;
            }
            richTextBox.HorizontalScrollBarVisibility = true;
            if (findGrid.Visibility == Visibility.Visible)
                nextButton.Visibility = Visibility.Visible;
            if (nextButton.Visibility != Visibility.Visible)
                editButton.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Inputs the pane_ showing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="InputPaneVisibilityEventArgs"/> instance containing the event data.</param>
        void InputPane_Showing(Windows.UI.ViewManagement.InputPane sender, InputPaneVisibilityEventArgs args)
        {
            if (isFormatPaneOpening)
                return;
            nextButton.Visibility = Visibility.Collapsed;
            richTextBox.HorizontalScrollBarVisibility = false;
            inputPaneGrid.Height = sender.OccludedRect.Height;
            inputPaneGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
            if (formatGrid.Visibility == Visibility.Visible)
            {
                formatGrid.Visibility = Visibility.Collapsed;
                BottomAppBar.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Handles the GotFocus event of the richTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (isFormatting)
            {
                richTextBox.IsReadOnly = false;
                isFormatting = false;
            }
        }
        /// <summary>
        /// Handles the ContentChanged event of the richTextBox control.
        /// </summary>
        /// <param name="obj">The source of the event.</param>
        /// <param name="args">The <see cref="ContentChangedEventArgs"/> instance containing the event data.</param>
        void richTextBox_ContentChanged(object obj, ContentChangedEventArgs args)
        {
            isContentChanged = true;
        }
        /// <summary>
        /// Popups to save.
        /// </summary>
        async private void PopupToSave()
        {
            MessageDialog result = new MessageDialog("Do you want to save the changes to \"Sample.docx\"?", "Save changes?");
            result.Commands.Add(new UICommand("Yes", CommandInvokedHandler));
            result.Commands.Add(new UICommand("No", CommandInvokedHandler));
            await result.ShowAsync();
        }
        /// <summary>
        /// Commands the invoked handler.
        /// </summary>
        /// <param name="command">The command.</param>
        void CommandInvokedHandler(IUICommand command)
        {
            // Password protected document loading
            if (command.Label == "Ok")
            {
                if (cancellationTokenSource != null)
                    cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
                loadAsync = null;
                busy.Visibility = Visibility.Collapsed;
                richTextBox.Document = new DocumentAdv();
            }
            // Find result empty
            else if (command.Label == "ok")
                richTextBox.Focus(FocusState.Pointer);
            else
            {
                if (command.Label == "Yes")
                    Save();
                else
                {
                    isContentChanged = false;
                    if (isOpenFile)
                        Open();
                    ////else
                    ////    SampleBrowser.NavigationService.GoBack();
                }
                isOpenFile = false;
            }
        }
        #endregion

        #endregion

        #region Override methods
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        /// <summary>
        /// Invoked immediately after the Page is unloaded and is no longer the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the navigation that has unloaded the current Page.</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
        }
        #endregion

        #region Hyperlink navigation
        /// <summary>
        /// Handles the request navigate event of the richTextBoxAdv control.
        /// </summary>
        /// <param name="obj">The source of the event.</param>
        /// <param name="args">The <see cref="RequestNavigateEventArgs"/> instance containing the event data.</param>
        void RichTextBoxAdv_RequestNavigate(object obj, RequestNavigateEventArgs args)
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

        #region Print implementation
        /// <summary>
        /// Prints the task requested.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PrintTaskRequestedEventArgs"/> instance containing the event data.</param>
        private void PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs e)
        {
            PrintTask printTask = null;
            printTask = e.Request.CreatePrintTask("SamplePDF", sourceRequested => sourceRequested.SetSource(printDocumentSource));
        }
        /// <summary>
        /// Handles the PrintCompleted event of the richTextBoxAdv control.
        /// </summary>
        /// <param name="obj">The source of the event.</param>
        /// <param name="args">The <see cref="PrintCompletedEventArgs"/> instance containing the event data.</param>
        void RichTextBoxAdv_PrintCompleted(object obj, PrintCompletedEventArgs args)
        {
        }
        /// <summary>
        /// This method registers the app for printing with Windows and sets up the necessary event handlers for the print process.
        /// </summary>
        private void RegisterForPrinting()
        {
            // Create a PrintManager and add a handler for printing initialization.
            PrintManager printMan = PrintManager.GetForCurrentView();
            printMan.PrintTaskRequested += PrintTaskRequested;
        }
        /// <summary>
        /// This method unregisters the app for printing with Windows.
        /// </summary>
        private void UnregisterPrinting()
        {
            // Remove the handler for printing initialization.
            PrintManager printMan = PrintManager.GetForCurrentView();
            printMan.PrintTaskRequested -= PrintTaskRequested;
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Loaded event of the DocumentEditor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void DocumentEditor_Loaded(object sender, RoutedEventArgs e)
        {
            richTextBox.Focus(FocusState.Pointer);
        }
        /// <summary>
        /// Handles the Unloaded event of the DocumentEditor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        void DocumentEditor_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }
        /// <summary>
        /// Handles the Click event of the WordImport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void WordImport_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            colorPopup.IsOpen = false;
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.FileTypeFilter.Add(".doc");
            fileOpenPicker.FileTypeFilter.Add(".docx");
            fileOpenPicker.FileTypeFilter.Add(".dotx");
            fileOpenPicker.FileTypeFilter.Add(".dot");
            fileOpenPicker.FileTypeFilter.Add(".rtf");
            fileOpenPicker.FileTypeFilter.Add(".htm");
            fileOpenPicker.FileTypeFilter.Add(".html");
            fileOpenPicker.FileTypeFilter.Add(".xml");
            fileOpenPicker.FileTypeFilter.Add(".txt");
            StorageFile stgFile = await fileOpenPicker.PickSingleFileAsync();
            busy.Visibility = Visibility.Visible;
            bool showErrorMsg = false;
            string errormessage = string.Empty;
            if (stgFile != null)
            {
                try
                {
                    cancellationTokenSource = new CancellationTokenSource();
                    loadAsync = this.richTextBox.LoadAsync(stgFile, cancellationTokenSource.Token);
                    await loadAsync;
                }
                catch(Exception ex)
                {
                    showErrorMsg = true;
                    errormessage = ex.Message;
                }
                if (showErrorMsg)
                {
                    if (!cancellationTokenSource.IsCancellationRequested)
                    {
                        MessageDialog dialog = null;
                        if (errormessage.Contains("Access is denied"))
                            dialog = new MessageDialog("This file cannot be open because it is being used by another process");
                        else
                            dialog = new MessageDialog("The input document could not be processed completely, Could you please email the document to support@syncfusion.com for troubleshooting.");
                        await dialog.ShowAsync();
                    }
                }
            }
            if (cancellationTokenSource != null)
                cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
            loadAsync = null;
            busy.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Handles the OnClick event of the PrintDocument control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void PrintDocument_OnClick(object sender, RoutedEventArgs e)
        {
        }
        /// <summary>
        /// Handles the Click event of the WordExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void WordExport_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            colorPopup.IsOpen = false;
            FileSavePicker fileSavePicker = new FileSavePicker();
            fileSavePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            fileSavePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".docx" });
            fileSavePicker.FileTypeChoices.Add("Word 97-2003 Document", new List<string>() { ".doc" });
            fileSavePicker.FileTypeChoices.Add("Word Template", new List<string>() { ".dotx" });
            fileSavePicker.FileTypeChoices.Add("Word 97-2003 Template", new List<string>() { ".dot" });
            fileSavePicker.FileTypeChoices.Add("Rich Text Document", new List<string>() { ".rtf" });
            fileSavePicker.FileTypeChoices.Add("Web Page, Filtered", new List<string>() { ".html" });
            fileSavePicker.FileTypeChoices.Add("Word XML Document", new List<string>() { ".xml" });
            fileSavePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            fileSavePicker.SuggestedFileName = string.IsNullOrEmpty(richTextBox.DocumentTitle) ? "New Document" : richTextBox.DocumentTitle;
            StorageFile stgFile = await fileSavePicker.PickSaveFileAsync();
            bool showErrorMsg = false;
            if (stgFile != null)
            {
                try
                {
                    await this.richTextBox.SaveAsync(stgFile);
                }
                catch
                {
                    showErrorMsg = true;
                }
                if (showErrorMsg)
                {
                    MessageDialog dialog = new MessageDialog("Could you please email the error details along with the input document to support@syncfusion.com for troubleshooting.");
                    await dialog.ShowAsync();
                }
            }
        }
        /// <summary>
        /// Handles the SelectionChanged event of the richTextBox control.
        /// </summary>
        /// <param name="obj">The source of the event.</param>
        /// <param name="args">The <see cref="Syncfusion.UI.Xaml.RichTextBoxAdv.SelectionChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void RichTextBox_SelectionChanged(object obj, Syncfusion.UI.Xaml.RichTextBoxAdv.SelectionChangedEventArgs args)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                if (nextButton.Visibility == Visibility.Visible || findGrid.Visibility == Visibility.Visible)
                {
                    openButton.Visibility = Visibility.Visible;
                    createButton.Visibility = Visibility.Visible;
                    saveButton.Visibility = Visibility.Visible;
                    editButton.Visibility = Visibility.Visible;
                    formatButton.Visibility = Visibility.Visible;
                    findButton.Visibility = Visibility.Visible;
                    insertImageButton.Visibility = Visibility.Visible;
                    nextButton.Visibility = Visibility.Collapsed;
                    richTextBox.HorizontalScrollBarVisibility = true;
                    findGrid.Visibility = Visibility.Collapsed;
                    richTextBox.Focus(FocusState.Pointer);
                    BottomAppBar.Visibility = Visibility.Visible;
                    findTextBox.Text = "";
                }
                else if (formatGrid.Visibility == Visibility.Visible)
                {
                    formatGrid.Visibility = Visibility.Collapsed;
                    BottomAppBar.Visibility = Visibility.Visible;
                    richTextBox.HorizontalScrollBarVisibility = true;
                    richTextBox.IsReadOnly = false;
                    isFormatting = false;
                    richTextBox.Focus(FocusState.Pointer);
                }
            }
            else
            {
                popup.IsOpen = false;
                colorPopup.IsOpen = false;
                if (popup.Child == listMenuGrid)
                    DisposeBindingForListUI();
                if (richTextBox.Selection.ParagraphFormat.ListLevelNumber >= 0)
                    LevelNumber = richTextBox.Selection.ParagraphFormat.ListLevelNumber;
            }
        }
        /// <summary>
        /// Handles the Click event of the NewDocumentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void NewDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            colorPopup.IsOpen = false;
            richTextBox.DocumentTitle = "New Document";
        }
        #endregion

        #region Formatting options UI Implementation
        /// <summary>
        /// Initializes the Popup UI.
        /// </summary>
        internal void InitPopupUI()
        {
            popup = new Popup() { IsOpen = false };
            colorPopup = new Popup() { IsOpen = false };
            popupBorder = new Windows.UI.Xaml.Controls.Border() { BorderThickness = new Thickness(1), BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0x1f, 0x1f, 0x1f)) };
        }
        /// <summary>
        /// Initializes the Color picker UI.
        /// </summary>
        private void InitColorPickerUI()
        {
            colorPicker = new SfColorPicker();
            Binding binding = new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.FontColor"), Mode = BindingMode.TwoWay };
            colorPicker.SetBinding(SfColorPicker.SelectedColorProperty, binding);
            colorPicker.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));
            colorPicker.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));
        }
        /// <summary>
        /// Initializes the Highlight color grid view UI.
        /// </summary>
        private void InitHighlightColorGridViewUI()
        {
            highlightColorPicker = new HighlightColorPicker();
            Binding binding = new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.HighlightColor"), Mode = BindingMode.TwoWay, Converter = new HighlightColorConverter(), ConverterParameter = highlightColorPicker.HighlightColorGridView };
            highlightColorPicker.HighlightColorGridView.SetBinding(GridView.SelectedIndexProperty, binding);
            highlightColorPicker.HighlightColorGridView.SelectionChanged += HighlightColorGridView_SelectionChanged;
        }
        /// <summary>
        /// Handles the SelectionChanged event of the HighlightColorGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        void HighlightColorGridView_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (colorPopup.IsOpen && colorPopup.Child == highlightColorPicker)
                colorPopup.IsOpen = false;
        }
        #endregion

        #region Font Formatting UI Implementation
        /// <summary>
        /// Initializes the font format grid UI.
        /// </summary>
        private void InitFontFormatGridUI()
        {
            fontFormatGrid = new FontFormatGrid(richTextBox);

            fontFormatGrid.FontColorButton.SetBinding(Button.BackgroundProperty, new Binding() { Source = colorPicker, Path = new PropertyPath("SelectedColor"), Mode = BindingMode.TwoWay, Converter = new SelectedColorConverter(), ConverterParameter = colorPopup });
            fontFormatGrid.HighlightColorButton.SetBinding(Button.BackgroundProperty, new Binding() { Source = highlightColorPicker.HighlightColorGridView, Path = new PropertyPath("SelectedItem"), Mode = BindingMode.OneWay, Converter = new HighlightColorButtonBackgroundConverter() });
            fontFormatGrid.HighlightColorButton.Click += HighlightColorButton_Click;
            fontFormatGrid.FontColorButton.Click += FontColorButton_Click;
            fontFormatGrid.BoldButton.Click += ToggleButton_Click;
            fontFormatGrid.ItalicButton.Click += ToggleButton_Click;
            fontFormatGrid.UnderlineButton.Click += ToggleButton_Click;
            fontFormatGrid.SingleStrikeThroughButton.Click += ToggleButton_Click;
            fontFormatGrid.DoubleStrikeThroughButton.Click += ToggleButton_Click;
            fontFormatGrid.SubscriptButton.Click += ToggleButton_Click;
            fontFormatGrid.SuperscriptButton.Click += ToggleButton_Click;
        }
        /// <summary>
        /// Handles the Click event of the fontButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void FontMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (colorPopup.IsOpen)
                colorPopup.IsOpen = false;
            if (popup.IsOpen && popup.Child == popupBorder && popupBorder.Child == fontFormatGrid)
                popup.IsOpen = false;
            else
            {
                popupBorder.Child = fontFormatGrid;
                popupBorder.Width = 374;
                popupBorder.Height = 224;
                popup.Child = popupBorder;
                popup.VerticalOffset = this.ActualHeight - appBarGrid.ActualHeight - popupBorder.Height - 8;
                popup.HorizontalOffset = 20;
                popup.IsOpen = true;
            }
        }
        /// <summary>
        /// Handles the Click event of the highlightColorButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void HighlightColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (colorPopup.IsOpen && colorPopup.Child == highlightColorPicker)
                colorPopup.IsOpen = false;
            else
            {
                colorPopup.IsOpen = true;
                colorPopup.Child = highlightColorPicker;
                colorPopup.HorizontalOffset = 396;
                colorPopup.VerticalOffset = this.ActualHeight - appBarGrid.ActualHeight - highlightColorPicker.Height - 8;
            }
        }
        /// <summary>
        /// Handles the Click event of the fontColorButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void FontColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (colorPopup.IsOpen && colorPopup.Child == colorPicker)
            {
                colorPopup.IsOpen = false;
                popup.IsOpen = false;
                richTextBox.Focus(FocusState.Pointer);
            }
            else
            {
                colorPopup.IsOpen = true;
                colorPopup.Child = colorPicker;
                colorPopup.Width = 240;
                colorPopup.HorizontalOffset = 396;
                colorPopup.VerticalOffset = this.ActualHeight - appBarGrid.ActualHeight - 308;
            }
        }
        /// <summary>
        /// Handles the Click event of the toggleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            colorPopup.IsOpen = false;
        }
        #endregion

        #region Paragraph Formatting UI Implementation
        /// <summary>
        /// Initializes the para format grid UI.
        /// </summary>
        private void InitParaFormatGridUI()
        {
            paraFormatGrid = new ParaFormatGrid(richTextBox);
            paraFormatGrid.SpacingsMenuButton.Click += SpacingsButton_Click;
            paraFormatGrid.ListMenuButton.Click += ListButton_Click;
        }
        /// <summary>
        /// Handles the Click event of the paraButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ParaMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (colorPopup.IsOpen)
                colorPopup.IsOpen = false;
            if (popup.IsOpen && popup.Child == popupBorder && popupBorder.Child == paraFormatGrid)
                popup.IsOpen = false;
            else
            {
                popupBorder.Child = paraFormatGrid;
                popupBorder.Width = 236;
                popupBorder.Height = 136;
                popup.Child = popupBorder;
                popup.VerticalOffset = this.ActualHeight - appBarGrid.ActualHeight - popupBorder.Height - 8;
                popup.HorizontalOffset = 20;
                popup.IsOpen = true;
            }
        }
        #endregion

        #region ListImplementation
        #region Properties
        /// <summary>
        /// Gets or sets the level number.
        /// </summary>
        /// <value>
        /// The level number.
        /// </value>
        public int LevelNumber
        {
            get
            {
                return levelNumber;
            }
            set
            {
                levelNumber = value;
                NotifyPropertyChanged("ListLevel");
                NotifyPropertyChanged("LevelNumber");
                NotifyPropertyChanged("ListLevelPattern");
                NotifyPropertyChanged("FollowCharacter");
            }
        }
        /// <summary>
        /// Gets the list level.
        /// </summary>
        /// <value>
        /// The list level.
        /// </value>
        public ListLevelAdv ListLevel
        {
            get
            {
                if (list != null && levelNumber >= 0 && levelNumber < 9)
                {
                    if (list.AbstractList.Levels.Count <= levelNumber)
                    {
                        AddListLevels();
                    }
                    return list.AbstractList.Levels[levelNumber];
                }
                return null;
            }
        }
        /// <summary>
        /// Gets or sets the list.
        /// </summary>
        /// <value>
        /// The list.
        /// </value>
        public ListAdv List
        {
            get
            {
                return list;
            }
            set
            {
                if (value == null)
                    CreateList();
                else
                    list = value;
            }
        }
        /// <summary>
        /// Gets or sets the list level pattern.
        /// </summary>
        /// <value>
        /// The list level pattern.
        /// </value>
        public ListLevelPattern ListLevelPattern
        {
            get
            {
                if (ListLevel != null)
                    return ListLevel.ListLevelPattern;
                return ListLevelPattern.Arabic;
            }
            set
            {
                if (ListLevel != null)
                    ListLevel.ListLevelPattern = value;
                NotifyPropertyChanged("ListLevelPattern");
            }
        }
        /// <summary>
        /// Gets or sets the follow character.
        /// </summary>
        /// <value>
        /// The follow character.
        /// </value>
        public FollowCharacterType FollowCharacter
        {
            get
            {
                if (ListLevel != null)
                    return ListLevel.FollowCharacter;
                return FollowCharacterType.None;
            }
            set
            {
                if (ListLevel != null)
                    ListLevel.FollowCharacter = value;
                NotifyPropertyChanged("FollowCharacter");
            }
        }
        #endregion
        #region Event
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Handles the Click event of the ListButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void ListButton_Click(object sender, RoutedEventArgs e)
        {
            InitBindingForListUI();
            popup.IsOpen = true;
            listMenuGrid.Width = Window.Current.Bounds.Width;
            listMenuGrid.Height = Window.Current.Bounds.Height;
            popup.Child = listMenuGrid;
            popup.HorizontalOffset = 0;
            popup.VerticalOffset = 0;
        }
        /// <summary>
        /// Handles the Click event of the okButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void ListFormatOkButton_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.Selection.ParagraphFormat.SetList(list);
            DisposeBindingForListUI();
            popup.IsOpen = false;
        }
        /// <summary>
        /// Handles the Click event of the cancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void ListFormatCancelButton_Click(object sender, RoutedEventArgs e)
        {
            DisposeBindingForListUI();
            popup.IsOpen = false;
        }
        #endregion

        #region Implementations
        /// <summary>
        /// Initializes the list format grid UI.
        /// </summary>
        private void InitListFormatGridUI()
        {
            listMenuGrid = new ListFormatGrid();
            listMenuGrid.ListApplyButton.Click += ListFormatOkButton_Click;
            listMenuGrid.ListCancelButton.Click += ListFormatCancelButton_Click;
        }
        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Creates the list.
        /// </summary>
        private void CreateList()
        {
            list = new ListAdv(null);
            list.AbstractList = new AbstractListAdv(null);
            ListLevelAdv listLevel = new ListLevelAdv(list.AbstractList);
            listLevel.ParagraphFormat = new ParagraphFormat(listLevel);
            listLevel.ParagraphFormat.LeftIndent = 48;
            listLevel.ParagraphFormat.FirstLineIndent = -24;
            listLevel.CharacterFormat = new CharacterFormat(listLevel);
            listLevel.NumberFormat = "%1.";
            listLevel.StartAt = 1;
            list.AbstractList.Levels.Add(listLevel);
        }
        /// <summary>
        /// Adds the list levels.
        /// </summary>
        private void AddListLevels()
        {
            if (list != null && list.AbstractList != null)
            {
                for (int i = list.AbstractList.Levels.Count; i < 9; i++)
                {
                    ListLevelAdv listLevelAdv = new ListLevelAdv(list.AbstractList);
                    listLevelAdv.CharacterFormat = new CharacterFormat(listLevelAdv);
                    listLevelAdv.ParagraphFormat = new ParagraphFormat(listLevelAdv);
                    listLevelAdv.ParagraphFormat.LeftIndent = (i + 1) * 48;
                    listLevelAdv.ParagraphFormat.FirstLineIndent = -24;
                    listLevelAdv.NumberFormat = "%" + (i + 1).ToString() + ".";
                    listLevelAdv.ListLevelPattern = (ListLevelPattern)(i % 3);
                    listLevelAdv.FollowCharacter = FollowCharacterType.Tab;
                    listLevelAdv.StartAt = 1;
                    listLevelAdv.RestartLevel = i;
                    list.AbstractList.Levels.Add(listLevelAdv);
                }
            }
        }
        /// <summary>
        /// Initializes the binding for list UI.
        /// </summary>
        private void InitBindingForListUI()
        {
            List = richTextBox.Selection.ParagraphFormat.GetList();
            if (ListLevel == null)
                return;
            if (ListLevel.CharacterFormat == null)
                ListLevel.CharacterFormat = new CharacterFormat(ListLevel);
            if (ListLevel.ParagraphFormat == null)
                ListLevel.ParagraphFormat = new ParagraphFormat(ListLevel);
            listMenuGrid.ListLevelBox.SelectionChanged += ListLevelBox_SelectionChanged;
            listMenuGrid.ListLevelBox.SetBinding(ComboBox.SelectedIndexProperty, new Binding() { Source = this, Path = new PropertyPath("LevelNumber"), Mode = BindingMode.TwoWay });
            listMenuGrid.StartAtBox.SetBinding(SfNumericUpDown.ValueProperty, new Binding() { Source = this, Path = new PropertyPath("ListLevel.StartAt"), Mode = BindingMode.TwoWay, Converter = new StartAtConverter() });
            listMenuGrid.FontFamilyBox.SetBinding(ComboBox.SelectedValueProperty, new Binding() { Source = this, Path = new PropertyPath("ListLevel.CharacterFormat.FontFamily"), Mode = BindingMode.TwoWay, Converter = new FontFamilyStringConverter() });
            listMenuGrid.FontSizeBox.SetBinding(SfNumericUpDown.ValueProperty, new Binding() { Source = this, Path = new PropertyPath("ListLevel.CharacterFormat.FontSize"), Mode = BindingMode.TwoWay });
            listMenuGrid.UnderlineButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = this, Path = new PropertyPath("ListLevel.CharacterFormat.Underline"), Mode = BindingMode.TwoWay, Converter = new UnderlineToggleConverter() });
            listMenuGrid.SingleStrikeThroughButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = this, Path = new PropertyPath("ListLevel.CharacterFormat.StrikeThrough"), Mode = BindingMode.TwoWay, Converter = new SingleStrikeThroughToggleConverter() });
            listMenuGrid.DoubleStrikeThroughButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = this, Path = new PropertyPath("ListLevel.CharacterFormat.StrikeThrough"), Mode = BindingMode.TwoWay, Converter = new DoubleStrikeThroughToggleConverter() });
            listMenuGrid.SuperscriptButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = this, Path = new PropertyPath("ListLevel.CharacterFormat.BaselineAlignment"), Mode = BindingMode.TwoWay, Converter = new SuperscriptToggleConverter() });
            listMenuGrid.SubscriptButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = this, Path = new PropertyPath("ListLevel.CharacterFormat.BaselineAlignment"), Mode = BindingMode.TwoWay, Converter = new SubscriptToggleConverter() });
            listMenuGrid.FirstlineIndentBox.SetBinding(SfNumericUpDown.ValueProperty, new Binding() { Source = this, Path = new PropertyPath("ListLevel.ParagraphFormat.FirstLineIndent"), Mode = BindingMode.TwoWay });
            listMenuGrid.LeftIndentBox.SetBinding(SfNumericUpDown.ValueProperty, new Binding() { Source = this, Path = new PropertyPath("ListLevel.ParagraphFormat.LeftIndent"), Mode = BindingMode.TwoWay });
            listMenuGrid.NumberFormatBox.SetBinding(TextBox.TextProperty, new Binding() { Source = this, Path = new PropertyPath("ListLevel.NumberFormat"), Mode = BindingMode.TwoWay });
            listMenuGrid.FollowCharacterBox.SetBinding(ComboBox.SelectedIndexProperty, new Binding() { Source = this, Path = new PropertyPath("FollowCharacter"), Mode = BindingMode.TwoWay, Converter = new FollowCharacterConverter() });
            listMenuGrid.LevelPatternBox.SetBinding(ComboBox.SelectedIndexProperty, new Binding() { Source = this, Path = new PropertyPath("ListLevelPattern"), Mode = BindingMode.TwoWay, Converter = new ListLevelPatternConverter() });
            listMenuGrid.LevelPatternBox.SelectionChanged += LevelPatternBox_SelectionChanged;
            listMenuGrid.FontFamilyBox.SelectionChanged += FontFamilyBox_SelectionChanged;
            listMenuGrid.FontStyleBox.SelectionChanged += FontStyleBox_SelectionChanged;
            listMenuGrid.RestartLevelBox.SelectionChanged += RestartLevelBox_SelectionChanged;
            UpdateRestartLevelBox();
            UpdateFontStyleBoxSelection();
            if (listMenuGrid.FontFamilyBox.SelectedValue != null)
            {
                listMenuGrid.NumberFormatBox.FontFamily = new FontFamily((string)listMenuGrid.FontFamilyBox.SelectedValue);
            }
            LevelNumber = levelNumber;
        }
        /// <summary>
        /// Updates the restart level box selection.
        /// </summary>
        void UpdateRestartLevelBox()
        {
            if (listMenuGrid.RestartLevelBox != null)
            {
                listMenuGrid.RestartLevelBox.Items.Clear();
                for (int i = levelNumber; i > 0; i--)
                {
                    listMenuGrid.RestartLevelBox.Items.Add("Level" + i);
                }
                listMenuGrid.RestartLevelBox.Items.Add("No Restart");

                listMenuGrid.RestartLevelBox.SelectedIndex = listMenuGrid.RestartLevelBox.Items.Count - ListLevel.RestartLevel - 1;
            }
        }
        /// <summary>
        /// Handles the SelectionChanged event of the RestartLevelBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        void RestartLevelBox_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (listMenuGrid.RestartLevelBox.SelectedIndex >= 0)
                ListLevel.RestartLevel = listMenuGrid.RestartLevelBox.Items.Count - listMenuGrid.RestartLevelBox.SelectedIndex - 1;
        }
        /// <summary>
        /// Handles the SelectionChanged event of the FontStyleBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        void FontStyleBox_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (ListLevel != null && ListLevel.CharacterFormat != null)
            {
                switch ((int)listMenuGrid.FontStyleBox.SelectedIndex)
                {
                    case 1:
                        ListLevel.CharacterFormat.Bold = true;
                        ListLevel.CharacterFormat.Italic = false;
                        break;
                    case 2:
                        ListLevel.CharacterFormat.Bold = false;
                        ListLevel.CharacterFormat.Italic = true;
                        break;
                    case 3:
                        ListLevel.CharacterFormat.Bold = true;
                        ListLevel.CharacterFormat.Italic = true;
                        break;
                    default:
                        ListLevel.CharacterFormat.Bold = false;
                        ListLevel.CharacterFormat.Italic = false;
                        break;
                }
            }
        }
        /// <summary>
        /// Handles the SelectionChanged event of the ListLevelBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        void ListLevelBox_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            UpdateFontStyleBoxSelection();
            UpdateRestartLevelBox();
        }
        /// <summary>
        /// Updates the font style box selection.
        /// </summary>
        private void UpdateFontStyleBoxSelection()
        {
            if (ListLevel != null && ListLevel.CharacterFormat != null)
            {
                if (ListLevel.CharacterFormat.Bold && ListLevel.CharacterFormat.Italic)
                    listMenuGrid.FontStyleBox.SelectedIndex = 3;
                else if (ListLevel.CharacterFormat.Italic)
                    listMenuGrid.FontStyleBox.SelectedIndex = 2;
                else if (ListLevel.CharacterFormat.Bold)
                    listMenuGrid.FontStyleBox.SelectedIndex = 1;
                else
                    listMenuGrid.FontStyleBox.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Handles the SelectionChanged event of the FontFamilyBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        void FontFamilyBox_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (listMenuGrid.FontFamilyBox.SelectedIndex != -1)
                listMenuGrid.NumberFormatBox.FontFamily = new FontFamily((string)listMenuGrid.FontFamilyBox.SelectedValue);
        }
        /// <summary>
        /// Handles the SelectionChanged event of the LevelPatternBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        void LevelPatternBox_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == 7)
            {
                listMenuGrid.FontFamilyBox.SelectedValue = ListLevel.LevelNumber % 3 == 1 ? "Wingdings" : "Symbol";
                ListLevel.NumberFormat = ListLevel.LevelNumber % 3 == 0 ? "\uf0b7" : ListLevel.LevelNumber % 3 == 1 ? "\uf0a7" : "\u27a4";
            }
            else
            {
                string numberFormat = "%" + (ListLevel.LevelNumber + 1).ToString();
                if (ListLevel.ListLevelPattern == ListLevelPattern.None)
                    ListLevel.NumberFormat = "";
                if (!ListLevel.NumberFormat.Contains(numberFormat) && ListLevel.ListLevelPattern != ListLevelPattern.None)
                    ListLevel.NumberFormat = numberFormat + ".";
                if (ListLevel.CharacterFormat.ReadLocalValue(CharacterFormat.FontFamilyProperty) == DependencyProperty.UnsetValue ||
                    ListLevel.CharacterFormat.FontFamily.Source.Equals("Symbol") || ListLevel.CharacterFormat.FontFamily.Source.Equals("Wingdings"))
                    listMenuGrid.FontFamilyBox.SelectedValue = "Verdana";
            }
        }
        /// <summary>
        /// Disposes the binding for list UI.
        /// </summary>
        private void DisposeBindingForListUI()
        {
            listMenuGrid.RestartLevelBox.SelectionChanged -= RestartLevelBox_SelectionChanged;
            listMenuGrid.FontStyleBox.SelectionChanged -= FontStyleBox_SelectionChanged;
            listMenuGrid.ListLevelBox.SelectionChanged -= ListLevelBox_SelectionChanged;
            listMenuGrid.LevelPatternBox.SelectionChanged -= LevelPatternBox_SelectionChanged;
            listMenuGrid.FontFamilyBox.SelectionChanged -= FontFamilyBox_SelectionChanged;
            listMenuGrid.StartAtBox.SetBinding(SfNumericUpDown.ValueProperty, new Binding());
            listMenuGrid.FontFamilyBox.SetBinding(ComboBox.SelectedValueProperty, new Binding());
            listMenuGrid.FontSizeBox.SetBinding(ComboBox.SelectedValueProperty, new Binding());
            listMenuGrid.UnderlineButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            listMenuGrid.SingleStrikeThroughButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            listMenuGrid.DoubleStrikeThroughButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            listMenuGrid.SuperscriptButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            listMenuGrid.SubscriptButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            listMenuGrid.FontStyleBox.SetBinding(ComboBox.SelectedIndexProperty, new Binding());
            listMenuGrid.FirstlineIndentBox.SetBinding(SfNumericUpDown.ValueProperty, new Binding());
            listMenuGrid.LeftIndentBox.SetBinding(SfNumericUpDown.ValueProperty, new Binding());
            listMenuGrid.NumberFormatBox.SetBinding(TextBox.TextProperty, new Binding());
            listMenuGrid.FollowCharacterBox.SetBinding(ComboBox.SelectedIndexProperty, new Binding());
            listMenuGrid.LevelPatternBox.SetBinding(ComboBox.SelectedIndexProperty, new Binding());
            listMenuGrid.ListLevelBox.SetBinding(ComboBox.SelectedIndexProperty, new Binding());
            if (list != null)
                list = null;
            if (richTextBox.Selection.ParagraphFormat.ListLevelNumber >= 0)
                LevelNumber = richTextBox.Selection.ParagraphFormat.ListLevelNumber;
            else
                LevelNumber = 0;
            popup.Child = null;
        }
        #endregion
        #endregion

        #region Spacings Formatting UI Implementation
        /// <summary>
        /// Initializes the spacings format grid UI.
        /// </summary>
        private void InitSpacingsFormatGridUI()
        {
            spacingsFormatGrid = new SpacingsFormatGrid();
            spacingsFormatGrid.SpacingsApplyButton.Click += SpacingsApplyButton_Click;
            spacingsFormatGrid.SpacingsCancelButton.Click += SpacingsCancelButton_Click;
        }
        /// <summary>
        /// Handles the Click event of the spacingsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void SpacingsButton_Click(object sender, RoutedEventArgs e)
        {
            InitValuesForSpacingsUI();
            popup.IsOpen = true;
            spacingsFormatGrid.Width = Window.Current.Bounds.Width;
            spacingsFormatGrid.Height = Window.Current.Bounds.Height;
            popup.Child = spacingsFormatGrid;
            popup.HorizontalOffset = 0;
            popup.VerticalOffset = 0;
        }
        /// <summary>
        /// Initializes the values for spacings UI.
        /// </summary>
        void InitValuesForSpacingsUI()
        {
            InitValueForNumericBox(spacingsFormatGrid.LeftIndentBox, richTextBox.Selection.ParagraphFormat.LeftIndent);
            InitValueForNumericBox(spacingsFormatGrid.RightIndentBox, richTextBox.Selection.ParagraphFormat.RightIndent);
            InitValueForNumericBox(spacingsFormatGrid.FirstlineIndentBox, richTextBox.Selection.ParagraphFormat.FirstLineIndent);
            InitValueForNumericBox(spacingsFormatGrid.BeforeSpacingBox, richTextBox.Selection.ParagraphFormat.BeforeSpacing < 0 ? double.NaN : richTextBox.Selection.ParagraphFormat.BeforeSpacing);
            InitValueForNumericBox(spacingsFormatGrid.AfterSpacingBox, richTextBox.Selection.ParagraphFormat.AfterSpacing < 0 ? double.NaN : richTextBox.Selection.ParagraphFormat.AfterSpacing);
            InitValueForNumericBox(spacingsFormatGrid.LineSpacingBox, richTextBox.Selection.ParagraphFormat.LineSpacing > 0 ? richTextBox.Selection.ParagraphFormat.LineSpacing : double.NaN);
            if (richTextBox.Selection.ParagraphFormat.LineSpacingType.HasValue)
            {
                LineSpacingType type = richTextBox.Selection.ParagraphFormat.LineSpacingType.Value;
                switch (type)
                {
                    case LineSpacingType.AtLeast:
                        spacingsFormatGrid.LineSpacingTypeBox.SelectedIndex = 1; break;
                    case LineSpacingType.Exactly:
                        spacingsFormatGrid.LineSpacingTypeBox.SelectedIndex = 2; break;
                    default:
                        spacingsFormatGrid.LineSpacingTypeBox.SelectedIndex = 0; break;
                }
            }
            else
                spacingsFormatGrid.LineSpacingTypeBox.SelectedIndex = -1;

        }
        /// <summary>
        /// Initializes the value for numeric box.
        /// </summary>
        /// <param name="numericBox">The numeric box.</param>
        /// <param name="value">The value.</param>
        private void InitValueForNumericBox(SfNumericUpDown numericBox, double value)
        {
            if (double.IsNaN(value))
            {
                numericBox.AllowNull = true;
                numericBox.Value = null;
            }
            else
            {
                numericBox.AllowNull = false;
                numericBox.Value = Math.Round(value, 2);
            }
        }
        /// <summary>
        /// Handles the Click event of the SpacingsFormatOkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void SpacingsApplyButton_Click(object sender, RoutedEventArgs e)
        {
            ////Left Indent
            if (spacingsFormatGrid.LeftIndentBox.Value != null)
            {
                if (spacingsFormatGrid.LeftIndentBox.Value is double && (double)spacingsFormatGrid.LeftIndentBox.Value != richTextBox.Selection.ParagraphFormat.LeftIndent)
                {
                    richTextBox.Selection.ParagraphFormat.LeftIndent = (double)spacingsFormatGrid.LeftIndentBox.Value;
                }
                else if (spacingsFormatGrid.LeftIndentBox.Value is float && (float)spacingsFormatGrid.LeftIndentBox.Value != richTextBox.Selection.ParagraphFormat.LeftIndent)
                {
                    richTextBox.Selection.ParagraphFormat.LeftIndent = (float)spacingsFormatGrid.LeftIndentBox.Value;
                }
            }

            ////Right Indent
            if (spacingsFormatGrid.RightIndentBox.Value != null)
            {
                if (spacingsFormatGrid.RightIndentBox.Value is double && (double)spacingsFormatGrid.RightIndentBox.Value != richTextBox.Selection.ParagraphFormat.RightIndent)
                {
                    richTextBox.Selection.ParagraphFormat.RightIndent = (double)spacingsFormatGrid.RightIndentBox.Value;
                }
                else if (spacingsFormatGrid.RightIndentBox.Value is float && (float)spacingsFormatGrid.RightIndentBox.Value != richTextBox.Selection.ParagraphFormat.RightIndent)
                {
                    richTextBox.Selection.ParagraphFormat.RightIndent = (float)spacingsFormatGrid.RightIndentBox.Value;
                }
            }

            ////First Line Indent
            if (spacingsFormatGrid.FirstlineIndentBox.Value != null)
            {
                if (spacingsFormatGrid.FirstlineIndentBox.Value is double && (double)spacingsFormatGrid.FirstlineIndentBox.Value != richTextBox.Selection.ParagraphFormat.FirstLineIndent)
                {
                    richTextBox.Selection.ParagraphFormat.FirstLineIndent = (double)spacingsFormatGrid.FirstlineIndentBox.Value;
                }
                else if (spacingsFormatGrid.FirstlineIndentBox.Value is float && (float)spacingsFormatGrid.FirstlineIndentBox.Value != richTextBox.Selection.ParagraphFormat.FirstLineIndent)
                {
                    richTextBox.Selection.ParagraphFormat.FirstLineIndent = (float)spacingsFormatGrid.FirstlineIndentBox.Value;
                }
            }

            ////Before spacing
            if (spacingsFormatGrid.BeforeSpacingBox.Value != null)
            {
                if (spacingsFormatGrid.BeforeSpacingBox.Value is double && (double)spacingsFormatGrid.BeforeSpacingBox.Value != richTextBox.Selection.ParagraphFormat.BeforeSpacing)
                {
                    richTextBox.Selection.ParagraphFormat.BeforeSpacing = (double)spacingsFormatGrid.BeforeSpacingBox.Value;
                }
                else if (spacingsFormatGrid.BeforeSpacingBox.Value is float && (float)spacingsFormatGrid.BeforeSpacingBox.Value != richTextBox.Selection.ParagraphFormat.BeforeSpacing)
                {
                    richTextBox.Selection.ParagraphFormat.BeforeSpacing = (float)spacingsFormatGrid.BeforeSpacingBox.Value;
                }
            }

            ////After Spacing
            if (spacingsFormatGrid.AfterSpacingBox.Value != null)
            {
                if (spacingsFormatGrid.AfterSpacingBox.Value is double && (double)spacingsFormatGrid.AfterSpacingBox.Value != richTextBox.Selection.ParagraphFormat.AfterSpacing)
                {
                    richTextBox.Selection.ParagraphFormat.AfterSpacing = (double)spacingsFormatGrid.AfterSpacingBox.Value;
                }
                else if (spacingsFormatGrid.AfterSpacingBox.Value is float && (float)spacingsFormatGrid.AfterSpacingBox.Value != richTextBox.Selection.ParagraphFormat.AfterSpacing)
                {
                    richTextBox.Selection.ParagraphFormat.AfterSpacing = (float)spacingsFormatGrid.AfterSpacingBox.Value;
                }
            }

            ////Line Spacing
            if (spacingsFormatGrid.LineSpacingBox.Value != null)
            {
                if (spacingsFormatGrid.LineSpacingBox.Value is double && (double)spacingsFormatGrid.LineSpacingBox.Value != richTextBox.Selection.ParagraphFormat.LineSpacing)
                {
                    richTextBox.Selection.ParagraphFormat.LineSpacing = (double)spacingsFormatGrid.LineSpacingBox.Value;
                }
                else if (spacingsFormatGrid.LineSpacingBox.Value is float && (float)spacingsFormatGrid.LineSpacingBox.Value != richTextBox.Selection.ParagraphFormat.LineSpacing)
                {
                    richTextBox.Selection.ParagraphFormat.LineSpacing = (float)spacingsFormatGrid.LineSpacingBox.Value;
                }
            }

            switch (spacingsFormatGrid.LineSpacingTypeBox.SelectedIndex)
            {
                case 0:
                    if (richTextBox.Selection.ParagraphFormat.LineSpacingType.HasValue && richTextBox.Selection.ParagraphFormat.LineSpacingType.Value != LineSpacingType.Multiple)
                        richTextBox.Selection.ParagraphFormat.LineSpacingType = LineSpacingType.Multiple;
                    break;
                case 1:
                    if (richTextBox.Selection.ParagraphFormat.LineSpacingType.HasValue && richTextBox.Selection.ParagraphFormat.LineSpacingType.Value != LineSpacingType.AtLeast)
                        richTextBox.Selection.ParagraphFormat.LineSpacingType = LineSpacingType.AtLeast;
                    break;
                case 2:
                    if (richTextBox.Selection.ParagraphFormat.LineSpacingType.HasValue && richTextBox.Selection.ParagraphFormat.LineSpacingType.Value != LineSpacingType.Exactly)
                        richTextBox.Selection.ParagraphFormat.LineSpacingType = LineSpacingType.Exactly;
                    break;
            }
            popup.IsOpen = false;
        }
        /// <summary>
        /// Handles the Click event of the SpacingsFormatCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SpacingsCancelButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }
        #endregion

        #region Table Formatting UI Implementation
        /// <summary>
        /// Initializes the table grid UI.
        /// </summary>
        private void InitTableGridUI()
        {
            tableGrid = new InsertTableGrid();
            tableGrid.TableInsertButton.Click += TableInsertButton_Click;
            tableGrid.TableCancelButton.Click += TableCancelButton_Click;
        }
        /// <summary>
        /// Handles the Click event of the TableButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            if (colorPopup.IsOpen)
                colorPopup.IsOpen = false;
            if (popup.IsOpen && popup.Child == popupBorder && popupBorder.Child == tableGrid)
                popup.IsOpen = false;
            else
            {
                popupBorder.Child = tableGrid;
                popupBorder.Width = 240;
                popupBorder.Height = 186;
                popup.Child = popupBorder;
                popup.VerticalOffset = this.ActualHeight - appBarGrid.ActualHeight - popupBorder.Height - 8;
                popup.HorizontalOffset = 160;
                popup.IsOpen = true;
            }
        }
        /// <summary>
        /// Handles the Click event of the TableMenuOkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TableInsertButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            string tableSize = (string)tableGrid.RowSizeBox.SelectedValue + "," + (string)tableGrid.ColumnSizeBox.SelectedValue;
            richTextBox.InsertTableCommand.Execute(tableSize);
        }
        /// <summary>
        /// Handles the Click event of the TableMenuCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TableCancelButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }
        #endregion

        #region Comment Options UI Implementation
        /// <summary>
        /// Initializes the comments grid UI.
        /// </summary>
        private void InitCommentsGridUI()
        {
            reviewOptionsGrid = new ReviewOptionsGrid();
            reviewOptionsGrid.NewCommentButton.Command = richTextBox.NewCommentCommand;
            reviewOptionsGrid.DeleteCommentButton.Command = richTextBox.DeleteCommentCommand;
            reviewOptionsGrid.PreviousCommentButton.Command = richTextBox.PreviousCommentCommand;
            reviewOptionsGrid.NextCommentButton.Command = richTextBox.NextCommentCommand;
            reviewOptionsGrid.ShowCommentsButton.Command = richTextBox.ShowCommentsCommand;
            reviewOptionsGrid.NewCommentButton.Click += CommentsOptionButton_Click;
            reviewOptionsGrid.DeleteCommentButton.Click += CommentsOptionButton_Click;
            reviewOptionsGrid.PreviousCommentButton.Click += CommentsOptionButton_Click;
            reviewOptionsGrid.NextCommentButton.Click += CommentsOptionButton_Click;
            reviewOptionsGrid.ShowCommentsButton.Click += CommentsOptionButton_Click;
        }
        /// <summary>
        /// Handles the Click event of the CommentsOptionButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void CommentsOptionButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }
        /// <summary>
        /// Handles the Click event of the CommentsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CommentsButton_Click(object sender, RoutedEventArgs e)
        {
            if (colorPopup.IsOpen)
                colorPopup.IsOpen = false;
            if (popup.IsOpen && popup.Child == popupBorder && popupBorder.Child == reviewOptionsGrid)
                popup.IsOpen = false;
            else
            {
                popupBorder.Child = reviewOptionsGrid;
                popupBorder.Width = 280;
                popupBorder.Height = 300;
                popup.Child = popupBorder;
                popup.VerticalOffset = this.ActualHeight - appBarGrid.ActualHeight - popupBorder.Height - 8;
                popup.HorizontalOffset = 220;
                popup.IsOpen = true;
            }
        }
        #endregion

        #region Clipboard Options Implementation
        /// <summary>
        /// Initializes the clip board options grid.
        /// </summary>
        private void InitClipBoardOptionsGrid()
        {
            clipboardOptionsGrid = new ClipBoardOptionsGrid();
            clipboardOptionsGrid.CopyButton.Style = (Windows.UI.Xaml.Style)this.Resources["RTECopyStyle"];
            clipboardOptionsGrid.CutButton.Style = (Windows.UI.Xaml.Style)this.Resources["RTECutStyle"];
            clipboardOptionsGrid.PasteButton.Style = (Windows.UI.Xaml.Style)this.Resources["RTEPasteStyle"];
            clipboardOptionsGrid.CutButton.Command = richTextBox.CutCommand;
            clipboardOptionsGrid.CopyButton.Command = richTextBox.CopyCommand;
            clipboardOptionsGrid.PasteButton.Command = richTextBox.PasteCommand;
        }
        /// <summary>
        /// Handles the Click event of the ClipboardButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ClipboardButton_Click(object sender, RoutedEventArgs e)
        {
            if (colorPopup.IsOpen)
                colorPopup.IsOpen = false;
            if (popup.IsOpen && popup.Child == popupBorder && popupBorder.Child == clipboardOptionsGrid)
                popup.IsOpen = false;
            else
            {
                popupBorder.Child = clipboardOptionsGrid;
                popupBorder.Width = 200;
                popupBorder.Height = 96;
                popup.Child = popupBorder;
                popup.VerticalOffset = this.ActualHeight - appBarGrid.ActualHeight - popupBorder.Height - 8;
                popup.HorizontalOffset = 340;
                popup.IsOpen = true;
            }
        }
        #endregion

        #region Spellchecker
        /// <summary>
        /// Initialize the spellchecker for SfRichTextBoxAdv.
        /// </summary>
        private void InitSpellChecker()
        {
            SpellChecker spellchecker = new SpellChecker();
            spellchecker.IsEnabled = false;

            //Adding the language dictionary to spellchecker.
            spellchecker.Dictionaries.Add("ms-appx:///RTE/Assets/en_US.dic");
            this.richTextBox.SpellChecker = spellchecker;
        }
        #endregion

        #region Dispose
        public async void Dispose()
        {
            // Unhooking events
            this.Unloaded -= DocumentEditor_Unloaded;
            this.Loaded -= DocumentEditor_Loaded;
            //Phone
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                DisposeAppbarBindings();
                this.inputPane.Showing -= InputPane_Showing;
                this.inputPane.Hiding -= InputPane_Hiding;
                this.inputPane = null;
                this.formatButton.Click -= FormatButton_Click;
                this.editButton.Click -= EditButton_Click;
                this.findButton.Click -= FindButton_Click;
                this.openButton.Click -= OpenFileButton_Click;
                this.saveButton.Click -= SaveFileButton_Click;
                this.insertImageButton.Click -= InsertImageButton_Click;
                this.boldButton.Click -= FormattingOptionButton_Click;
                this.italicButton.Click -= FormattingOptionButton_Click;
                this.underlineButton.Click -= FormattingOptionButton_Click;
                this.strikeButton.Click -= FormattingOptionButton_Click;
                this.orangeFCButton.Click -= FormattingOptionButton_Click;
                this.greenFCButton.Click -= FormattingOptionButton_Click;
                this.redFCButton.Click -= FormattingOptionButton_Click;
                this.yellowHCButton.Click -= FormattingOptionButton_Click;
                this.greenHCButton.Click -= FormattingOptionButton_Click;
                this.redHCButton.Click -= FormattingOptionButton_Click;
                this.increaseFontSizeButton.Click -= IncreaseFontSizeButton_Click;
                this.decreaseFontSizeButton.Click -= DecreaseFontSizeButton_Click;
                this.richTextBox.GotFocus -= RichTextBox_GotFocus;
                this.richTextBox.ContentChanged -= richTextBox_ContentChanged;
                this.nextButton.Click -= NextButton_Click;
                this.deleteButton.Click -= DeleteButton_Click;

                this.BottomAppBar = null;
            }
            else
            {
                this.LoadDocument.Click -= WordImport_Click;
                this.SaveDocument.Click -= WordExport_Click;
                this.PrintDocument.Click -= PrintDocument_OnClick;
                this.fontMenuButton.Click -= FontMenuButton_Click;
                this.paraMenuButton.Click -= ParaMenuButton_Click;
                this.tableButton.Click -= TableButton_Click;
                this.tableGrid.TableInsertButton.Click -= TableInsertButton_Click;
                this.tableGrid.TableCancelButton.Click -= TableCancelButton_Click;
                this.spacingsFormatGrid.SpacingsApplyButton.Click -= SpacingsApplyButton_Click;
                this.spacingsFormatGrid.SpacingsCancelButton.Click -= SpacingsCancelButton_Click;
                this.listMenuGrid.ListApplyButton.Click -= ListFormatOkButton_Click;
                this.listMenuGrid.ListCancelButton.Click -= ListFormatCancelButton_Click;
                reviewOptionsGrid.NewCommentButton.Click -= CommentsOptionButton_Click;
                reviewOptionsGrid.DeleteCommentButton.Click -= CommentsOptionButton_Click;
                reviewOptionsGrid.PreviousCommentButton.Click -= CommentsOptionButton_Click;
                reviewOptionsGrid.NextCommentButton.Click -= CommentsOptionButton_Click;
                reviewOptionsGrid.ShowCommentsButton.Click -= CommentsOptionButton_Click;
                //Disposes bindings
                highlightColorPicker.HighlightColorGridView.SetBinding(GridView.SelectedIndexProperty, new Binding());
                colorPicker.SetBinding(SfColorPicker.SelectedColorProperty, new Binding());
                //Disposes UI explicitly
                clipboardOptionsGrid.Dispose();
                this.richTextBox.SelectionChanged -= RichTextBox_SelectionChanged;
                spacingsFormatGrid.Dispose();
                listMenuGrid.Dispose();
                fontFormatGrid.Dispose();
                tableGrid.Dispose();
                paraFormatGrid.Dispose();
                reviewOptionsGrid.Dispose();
                popupBorder.Child = null;
                popupBorder = null;
                colorPopup.Child = null;
                colorPopup = null;
                popup.Child = null;
                popup = null;
                highlightColorPicker = null;
                spacingsFormatGrid = null;
                listMenuGrid = null;
                fontFormatGrid = null;
                paraFormatGrid = null;
                clipboardOptionsGrid = null;
                tableGrid = null;
                reviewOptionsGrid = null;
                colorPicker = null;
            }
            //Handled to cancel the asynchronous load operation.
            //Common for phone and windows
            if (loadAsync != null && !loadAsync.IsCompleted && !loadAsync.IsFaulted && cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                try
                {
                    await loadAsync;
                }
                catch
                { }
            }
            this.richTextBox.PrintCompleted -= RichTextBoxAdv_PrintCompleted;
            this.richTextBox.RequestNavigate -= RichTextBoxAdv_RequestNavigate;
            this.richTextBox.SelectionChanged -= RichTextBox_SelectionChanged;
            //Disposes the SfRichTextBoxAdv contents explicitly.
            this.richTextBox.Dispose();
            printDocumentSource = null;
            this.richTextBox = null;
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
                UnlinkChildrens((element as UserControl).Content as UIElement);
                (element as UserControl).Content = null;
            }
        }
        #endregion
    }

    /// <summary>
    /// Specifies the Clip Board options.
    /// </summary>
    public class ClipBoardOptionsGrid : Grid
    {
        #region fields
        Button cutButton;
        Button copyButton;
        Button pasteButton;
        #endregion

        #region Properties
        public Button CutButton
        {
            get
            {
                return cutButton;
            }
        }
        public Button CopyButton
        {
            get
            {
                return copyButton;
            }
        }
        public Button PasteButton
        {
            get
            {
                return pasteButton;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClipBoardOptionsGrid"/> class.
        /// </summary>
        public ClipBoardOptionsGrid()
        {
            Width = 200;
            Height = 96;
            ManipulationMode = ManipulationModes.All;
            Background = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));

            //Add 3 rows
            RowDefinition row = new RowDefinition() { Height = new GridLength(18) };
            RowDefinitions.Add(row);
            row = new RowDefinition() { Height = new GridLength(60) };
            RowDefinitions.Add(row);
            row = new RowDefinition() { Height = new GridLength(18) };
            RowDefinitions.Add(row);

            //Add 5 columns
            ColumnDefinition column = new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Star) };
            ColumnDefinitions.Add(column);
            column = new ColumnDefinition() { Width = new GridLength(60, GridUnitType.Star) };
            ColumnDefinitions.Add(column);
            column = new ColumnDefinition() { Width = new GridLength(12, GridUnitType.Star) };
            ColumnDefinitions.Add(column);
            column = new ColumnDefinition() { Width = new GridLength(60, GridUnitType.Star) };
            ColumnDefinitions.Add(column);
            column = new ColumnDefinition() { Width = new GridLength(16, GridUnitType.Star) };
            ColumnDefinitions.Add(column);
            column = new ColumnDefinition() { Width = new GridLength(60, GridUnitType.Star) };
            ColumnDefinitions.Add(column);
            column = new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Star) };
            ColumnDefinitions.Add(column);

            cutButton = new Button() { Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x1f, 0x1f, 0x1f)) };
            ToolTip tip = new ToolTip() { Content = "Cut" };
            ToolTipService.SetToolTip(cutButton, tip);
            Children.Add(cutButton);
            Grid.SetRow(cutButton, 1);
            Grid.SetColumn(cutButton, 1);

            copyButton = new Button() { Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x1f, 0x1f, 0x1f)) };
            tip = new ToolTip() { Content = "Copy" };
            ToolTipService.SetToolTip(copyButton, tip);
            Children.Add(copyButton);
            Grid.SetRow(copyButton, 1);
            Grid.SetColumn(copyButton, 3);

            pasteButton = new Button() { Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x1f, 0x1f, 0x1f)) };
            tip = new ToolTip() { Content = "Paste" };
            ToolTipService.SetToolTip(pasteButton, tip);
            Children.Add(pasteButton);
            Grid.SetRow(pasteButton, 1);
            Grid.SetColumn(pasteButton, 5);
        }
        #endregion

        #region Implementations
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Children.Clear();
            cutButton = null;
            copyButton = null;
            pasteButton = null;
        }
        #endregion
    }
}
