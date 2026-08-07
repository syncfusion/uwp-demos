using Syncfusion.UI.Xaml.RichTextBoxAdv;
using System;
using System.Collections.Generic;
using Syncfusion.UI.Xaml.Controls.Input;
using Syncfusion.UI.Xaml.Controls.Media;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Printing;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.Controls.SfRibbon;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Printing;
using Windows.Phone.UI.Input;
using Windows.UI.ViewManagement;
using Syncfusion.SampleBrowser.UWP.RichTextEditor;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RTEDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DocumentEditor : Page, IDisposable
    {
        #region Fields
        IPrintDocumentSource printDocumentSource = null;
        Task<bool> loadAsync = null;
        CancellationTokenSource cancellationTokenSource = null;
        // For phone
        Windows.UI.ViewManagement.InputPane inputPane = null;
        StatusBar statusBar = null;
        double ZoomFactor = 100;
        bool isRibbonTabPanelOpened = false, isRibbonBackstageOpened = false, isSetZoomFactor = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentEditor"/> class.
        /// </summary>
        public DocumentEditor()
        {
            this.InitializeComponent();
            this.Loaded += DocumentEditor_Loaded;
            this.Unloaded += DocumentEditor_Unloaded;
            this.richTextBox.SelectionChanged += RichTextBox_SelectionChanged;
            this.richTextBox.ContentChanged += RichTextBox_ContentChanged;
            this.richTextBox.RequestNavigate += RichTextBoxAdv_RequestNavigate;
            this.richTextBox.PrintCompleted += RichTextBoxAdv_PrintCompleted;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                statusBar = StatusBar.GetForCurrentView();
                inputPane = Windows.UI.ViewManagement.InputPane.GetForCurrentView();
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
                richTextBox.IsTextPredictionEnabled = true;
                richTextBox.GripperSize = 16;
                richTextBox.GotFocus += RichTextBox_GotFocus;
                richTextBox.Tapped += RichTextBox_Tapped;
                richTextBox.ZoomFactorChanged += RichTextBox_ZoomFactorChanged;
                inputPane.Showing += InputPane_Showing;
                inputPane.Hiding += InputPane_Hiding;
                ribbon.RibbonTabPanelOpened += Ribbon_RibbonTabPanelOpened;
                ribbon.RibbonTabPanelClosed += Ribbon_RibbonTabPanelClosed;
            }
            else
            {
                this.highlightcolorpicker.HighlightColorGridView.SetBinding(GridView.SelectedIndexProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.HighlightColor"), Mode = BindingMode.TwoWay, Converter = new HighlightColorConverter() });
                this.highlightcolorpicker.HighlightColorGridView.SelectionChanged += HighlightColorGridView_SelectionChanged;
            }
            ribbon.BackStageOpened += Ribbon_BackStageOpened;
            ribbon.BackStageClosed += Ribbon_BackStageClosed;
            Stream inputStream = GetManifestResourceStream("GettingStarted_Phone.docx");
            this.richTextBox.Load(inputStream, FormatType.Docx);
            this.richTextBox.DocumentTitle = "Getting Started";
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
        /// <summary>
        /// Sets the read only property of RichTextBox.
        /// </summary>
        /// <param name="isReadOnly"></param>
        private void SetReadOnlyForRichTextBox(bool isReadOnly)
        {
            if (isReadOnly == richTextBox.IsReadOnly)
                return;
            if (FocusManager.GetFocusedElement() == richTextBox)
                richTextBox.IsReadOnly = isReadOnly;
            else
            {
                richTextBox.IsReadOnly = isReadOnly;
                richTextBox.Focus(FocusState.Pointer);
            }
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
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void DocumentEditor_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }
        #endregion

        #region Phone Events
        /// <summary>
        /// Handles the BackPressed event of the HardwareButtons control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BackPressedEventArgs"/> instance containing the event data.</param>
        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            bool isHandled = true;
            if (isRibbonTabPanelOpened)
                ribbon.HideRibbonTabPanel();
            else if (isRibbonBackstageOpened)
                ribbon.CloseBackStage();
            else if (!richTextBox.IsReadOnly)
                SetReadOnlyForRichTextBox(true);
            else
                isHandled = false;
            if (isHandled)
                e.Handled = true;
        }
        /// <summary>
        /// Inputs the pane_ hiding.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="InputPaneVisibilityEventArgs"/> instance containing the event data.</param>
        void InputPane_Hiding(Windows.UI.ViewManagement.InputPane sender, InputPaneVisibilityEventArgs args)
        {
            inputPaneGrid.Height = 0;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons") && !sender.Visible)
            {
                ribbon.HideRibbonTabPanel();
                SetReadOnlyForRichTextBox(true);
                richTextBox.PageFitCommand.Execute(PageFitType.FitPageWidth);
                ZoomFactor = richTextBox.ZoomFactor;
            }
        }
        /// <summary>
        /// Inputs the pane_ showing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="InputPaneVisibilityEventArgs"/> instance containing the event data.</param>
        void InputPane_Showing(Windows.UI.ViewManagement.InputPane sender, InputPaneVisibilityEventArgs args)
        {
            inputPaneGrid.Height = sender.OccludedRect.Height;
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
        /// <summary>
        /// Invoked immediately after the Page is unloaded and is no longer the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the navigation that has unloaded the current Page.</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
        }
        #endregion

        #region RichTextBoxAdv Events
        /// <summary>
        /// Called when the zoom factor of SfRichTextBoxAdv changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containig event data.</param>
        private void RichTextBox_ZoomFactorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (isRibbonTabPanelOpened && isSetZoomFactor)
            {
                richTextBox.ZoomFactor = ZoomFactor;
                isSetZoomFactor = false;
            }
        }
        /// <summary>
        /// Called when RichTextBoxAdv got focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                ribbon.HideRibbonTabPanel();
                if (richTextBox.IsReadOnly)
                    richTextBox.PageFitCommand.Execute(PageFitType.FitFullPage);
                else
                    richTextBox.ResetZooming();
            }
        }
        /// <summary>
        /// Called when RichtextBox tapped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RichTextBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ribbon.HideRibbonTabPanel();
            ribbon.CloseBackStage();
            SetReadOnlyForRichTextBox(false);
        }
        /// <summary>
        /// Handles the SelectionChanged event of the richTextBox control.
        /// </summary>
        /// <param name="obj">The source of the event.</param>
        /// <param name="args">The <see cref="Syncfusion.UI.Xaml.RichTextBoxAdv.SelectionChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void RichTextBox_SelectionChanged(object obj, Syncfusion.UI.Xaml.RichTextBoxAdv.SelectionChangedEventArgs args)
        {
        }
        /// <summary>
        /// Handles the ContentChanged event of the RichTextBox control.
        /// </summary>
        /// <param name="obj">The source of the event.</param>
        /// <param name="args">The <see cref="ContentChangedEventArgs"/> instance containing the event data.</param>
        private void RichTextBox_ContentChanged(object obj, ContentChangedEventArgs args)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons") && richTextBox.IsReadOnly)
                SetReadOnlyForRichTextBox(false);
        }
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

        #region BackStage Events
        /// <summary>
        /// Handles the Click event of the WordImport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void WordImport_Click(object sender, RoutedEventArgs e)
        {
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
            CloseBackStage();
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
                catch (Exception ex)
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
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                SetReadOnlyForRichTextBox(true);
        }
        /// <summary>
        /// Handles the Click event of the WordExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void WordExport_Click(object sender, RoutedEventArgs e)
        {
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
            fileSavePicker.SuggestedFileName = string.IsNullOrEmpty(richTextBox.DocumentTitle) ? "New Document" : richTextBox.DocumentTitle; ;
            StorageFile stgFile = await fileSavePicker.PickSaveFileAsync();
            CloseBackStage();
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
        /// Handles the Click event of the BlankDocumentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BlankDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.DocumentTitle = "Blank Document";
            CloseBackStage();
        }
        /// <summary>
        /// Handles the OnClick event of the PrintDocument control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void PrintDocument_OnClick(object sender, RoutedEventArgs e)
        {
            CloseBackStage();
        }
        /// <summary>
        /// Handles the Clicked event of the HelpButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void HelpButton_Clicked(object sender, RoutedEventArgs e)
        {
            LaunchUri(new Uri("https://help.syncfusion.com/uwp/sfrichtextboxadv/overview"));
            CloseBackStage();
        }
        /// <summary>
        /// Closes the back stage.
        /// </summary>
        private void CloseBackStage()
        {
            ribbon.CloseBackStage();
            richTextBox.Focus(FocusState.Pointer);
        }
        #endregion

        #region Desktop Ribbon Events
        /// <summary>
        /// Handles the Click event of the NewDocumentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void NewDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.DocumentTitle = "New Document";
        }
        /// <summary>
        /// Handles the SelectionChanged event of the HighlightColorGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        void HighlightColorGridView_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            highlightColorDropDownButton.IsDropDownOpen = false;
        }
        /// <summary>
        /// Handles the Click event of the BulletButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BulletButton_Click(object sender, RoutedEventArgs e)
        {
            if (richTextBox != null && richTextBox.Selection != null)
            {
                ListAdv list = richTextBox.Selection.ParagraphFormat.GetList();
                if (list == null || (list != null && list.AbstractList.Levels[richTextBox.Selection.Start.Paragraph.ParagraphFormat.ListFormat.ListLevelNumber].ListLevelPattern != ListLevelPattern.Bullet && list.AbstractList.Levels[richTextBox.Selection.Start.Paragraph.ParagraphFormat.ListFormat.ListLevelNumber].NumberFormat != "\uf0b7"))
                {
                    list = new ListAdv(null);
                    list.AbstractList = new AbstractListAdv(null);
                    ListLevelAdv listLevel = new ListLevelAdv(list.AbstractList);
                    listLevel.ListLevelPattern = ListLevelPattern.Bullet;
                    listLevel.NumberFormat = "\uf0b7";
                    listLevel.CharacterFormat.FontFamily = new FontFamily("Symbol");
                    listLevel.ParagraphFormat.LeftIndent = 48;
                    listLevel.ParagraphFormat.FirstLineIndent = -24;
                    list.AbstractList.Levels.Add(listLevel);
                    richTextBox.Selection.ParagraphFormat.SetList(list);
                    richTextBox.Selection.ParagraphFormat.ListLevelNumber = 0;
                }
                else
                    richTextBox.Selection.ParagraphFormat.ListLevelNumber = -1;
            }
        }
        /// <summary>
        /// Handles the Click event of the NumberingButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void NumberingButton_Click(object sender, RoutedEventArgs e)
        {
            if (richTextBox != null && richTextBox.Selection != null)
            {
                ListAdv list = richTextBox.Selection.ParagraphFormat.GetList();
                if (list == null || (list != null && list.AbstractList.Levels[richTextBox.Selection.Start.Paragraph.ParagraphFormat.ListFormat.ListLevelNumber].ListLevelPattern != ListLevelPattern.Number && list.AbstractList.Levels[richTextBox.Selection.Start.Paragraph.ParagraphFormat.ListFormat.ListLevelNumber].NumberFormat != "%1."))
                {
                    list = new ListAdv(null);
                    list.AbstractList = new AbstractListAdv(null);
                    ListLevelAdv listLevel = new ListLevelAdv(list.AbstractList);
                    listLevel.ListLevelPattern = ListLevelPattern.Number;
                    listLevel.NumberFormat = "%1.";
                    listLevel.StartAt = 1;
                    listLevel.CharacterFormat.FontFamily = new FontFamily("Verdana");
                    listLevel.ParagraphFormat.LeftIndent = 48;
                    listLevel.ParagraphFormat.FirstLineIndent = -24;
                    list.AbstractList.Levels.Add(listLevel);
                    richTextBox.Selection.ParagraphFormat.SetList(list);
                    richTextBox.Selection.ParagraphFormat.ListLevelNumber = 0;
                }
                else
                    richTextBox.Selection.ParagraphFormat.ListLevelNumber = -1;
            }
        }
        /// <summary>
        /// Handles the Click event of the TableAddButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TableAddButton_Click(object sender, RoutedEventArgs e)
        {
            tabledropdown.IsDropDownOpen = false;
            string tableSize = (string)rowSize.Text + "," + (string)columnSize.Text;
            richTextBox.InsertTableCommand.Execute(tableSize);
        }
        /// <summary>
        /// Handles the Click event of the TableCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TableCancelButton_Click(object sender, RoutedEventArgs e)
        {
            tabledropdown.IsDropDownOpen = false;
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

        #region Phone Ribbon Events
        /// <summary>
        /// Called when ribbon tab panel opened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_RibbonTabPanelOpened(object sender, EventArgs e)
        {
            ZoomFactor = richTextBox.ZoomFactor;
            isSetZoomFactor = true;
            isRibbonTabPanelOpened = true;
            inputPane.TryHide();
            SetReadOnlyForRichTextBox(false);
        }
        /// <summary>
        /// Called when ribbon tab panel closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_RibbonTabPanelClosed(object sender, EventArgs e)
        {
            isRibbonTabPanelOpened = false;
        }
        /// <summary>
        /// Called when backstage opened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_BackStageOpened(object sender, EventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                isRibbonBackstageOpened = true;
                SetReadOnlyForRichTextBox(true);
            }
        }
        /// <summary>
        /// Called when backstage closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_BackStageClosed(object sender, EventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                isRibbonBackstageOpened = false;
                SetReadOnlyForRichTextBox(false);
            }
        }
        /// <summary>
        /// Called when highlight color drop down button clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HighlightColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (richTextBox.Selection.CharacterFormat.HighlightColor.HasValue && richTextBox.Selection.CharacterFormat.HighlightColor.Value == HighlightColor.Yellow)
                richTextBox.Selection.CharacterFormat.HighlightColor = HighlightColor.NoColor;
            else
                richTextBox.Selection.CharacterFormat.HighlightColor = HighlightColor.Yellow;
        }
        /// <summary>
        /// Called when font color button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (richTextBox.Selection.CharacterFormat.HighlightColor.HasValue && richTextBox.Selection.CharacterFormat.FontColor.Value == Colors.Red)
                richTextBox.Selection.CharacterFormat.FontColor = Colors.Black;
            else
                richTextBox.Selection.CharacterFormat.FontColor = Colors.Red;
        }
        /// <summary>
        /// Called when the insert table button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertTableButton_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.InsertTableCommand.Execute("3,3");
        }
        /// <summary>
        /// Called when insert picture button clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void InsertPictureButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.FileTypeFilter.Add(".png");
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.FileTypeFilter.Add(".bmp");
            StorageFile stgFile = await fileOpenPicker.PickSingleFileAsync();
            if (stgFile != null)
                richTextBox.InsertPictureCommand.Execute(stgFile);
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                SetReadOnlyForRichTextBox(true);
        }
        #endregion

        #region Dispose
        public async void Dispose()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                statusBar.BackgroundOpacity = 0;
                this.statusBar = null;
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
                this.inputPane.Showing -= InputPane_Showing;
                this.inputPane.Hiding -= InputPane_Hiding;
                this.inputPane = null;
                richTextBox.GotFocus -= RichTextBox_GotFocus;
                richTextBox.Tapped -= RichTextBox_Tapped;
                richTextBox.ZoomFactorChanged -= RichTextBox_ZoomFactorChanged;
                ribbon.RibbonTabPanelOpened -= Ribbon_RibbonTabPanelOpened;
                ribbon.RibbonTabPanelClosed -= Ribbon_RibbonTabPanelClosed;
            }
            ribbon.BackStageOpened -= Ribbon_BackStageOpened;
            ribbon.BackStageClosed -= Ribbon_BackStageClosed;
            this.Unloaded -= DocumentEditor_Unloaded;
            this.Loaded -= DocumentEditor_Loaded;
            if (highlightcolorpicker != null)
            {
                this.highlightcolorpicker.HighlightColorGridView.SelectionChanged -= HighlightColorGridView_SelectionChanged;
                this.highlightcolorpicker.HighlightColorGridView.SetBinding(GridView.SelectedIndexProperty, new Binding());
            }
            this.highlightcolorpicker = null;
            //Handled to cancel the asynchronous load operation.
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
            this.richTextBox.ContentChanged -= RichTextBox_ContentChanged;
            //Disposes the SfRichTextBoxAdv contents explicitly.
            this.richTextBox.Dispose();
            printDocumentSource = null;
            this.richTextBox = null;
            //Un hook backstage events
            printBackStageButton.Click -= PrintDocument_OnClick;
            printBackStageButton.Dispose();
            openBackStageButton.Click -= WordImport_Click;
            openBackStageButton.Dispose();
            openBackStageButton = null;
            saveAsBackStageButton.Click -= WordExport_Click;
            saveAsBackStageButton.Dispose();
            saveAsBackStageButton = null;
            helpBackaStageButton.Click -= HelpButton_Clicked;
            helpBackaStageButton.Dispose();
            helpBackaStageButton = null;
            //Disposing the BackStage
            this.ribbon.BackStage.Dispose();
            //Disposing the QAT
            this.ribbon.QuickAccessToolBar.Dispose();
            //Disposing the Ribbon.
            this.ribbon.Dispose();
            //Disposing the RibbonPage
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                this.ribbonPage.Dispose();
            mainGrid.Resources.Clear();
            mainGrid.Resources = null;
            this.Resources.Clear();
            this.Resources = null;
            busy.ClearValue(ProgressBar.IsIndeterminateProperty);
            busy.ClearValue(ProgressBar.ForegroundProperty);
            busy.ClearValue(ProgressBar.VerticalAlignmentProperty);
            UnlinkChildrens(this);
            UnlinkChildrens(this.ribbon.BackStage);
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
}
