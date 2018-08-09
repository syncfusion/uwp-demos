using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UMLDiagramDesigner;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DiagramUtility
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DiagramFrontPage : Page
    {
        public DiagramFrontPage()
        {
            this.InitializeComponent();
            this.Cancel = new DelegateCommand<object>(OnCancel);
            fileList.SelectionChanged += fileList_SelectionChanged;
            fileList.ItemClick += fileList_ItemClick;
            fileList.IsItemClickEnabled = true;
            this.Unloaded += DiagramFrontPage_Unloaded;
        }

        void DiagramFrontPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Cancel = null;
            this.Unloaded -= DiagramFrontPage_Unloaded;
            if (fileList != null)
            {
                fileList.SelectionChanged -= fileList_SelectionChanged;
                fileList.ItemClick -= fileList_ItemClick;
            }
            ObservableCollection<FileItem> items = fileList.DataContext as ObservableCollection<FileItem>;
            foreach (FileItem item in items)
	        {
                item.Dispose();
            }
            fileList.DataContext = null;
            creator = null;
        }

        private string SubFolder { get; set; }

        private string _mFrontPageImge;

        public string FrontPageImge
        {
            get
            {
                return _mFrontPageImge;
            }
            set
            {
                if (_mFrontPageImge != value)
                {
                    _mFrontPageImge = value;
                    //OnPropertyChanged("FrontPageImge");
                }
            }
        }
        public DiagramCommonViewModel DiagramCommonViewModel
        {
            get { return (DiagramCommonViewModel)GetValue(UMLViewModelProperty); }
            set { SetValue(UMLViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DiagramCommonViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UMLViewModelProperty =
            DependencyProperty.Register("DiagramCommonViewModel", typeof(DiagramCommonViewModel), typeof(DiagramFrontPage), new PropertyMetadata(null, OnUMLViewModelChanged));

        private static void OnUMLViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DiagramFrontPage front = d as DiagramFrontPage;
            if (e.OldValue != null)
            {
                DiagramCommonViewModel oldValue = e.OldValue as DiagramCommonViewModel;
                oldValue.ExportFiles = null;
                oldValue.ImportFiles = null;
                oldValue.DeleteFiles = null;
                oldValue.SelectAll = null;
                oldValue.ClearSelection = null;
                oldValue.RenameFile = null;
                oldValue.PropertyChanged -= (d as DiagramFrontPage).FrontPage_PropertyChanged;
            }
            if (e.NewValue == null)
            {
                return;
            }

            //if (front.DiagramType == DiagramType.UML)
            //{
            (e.NewValue as DiagramCommonViewModel).PropertyChanged += (d as DiagramFrontPage).FrontPage_PropertyChanged;
            //}
            //else if (front.DiagramType == DiagramType.WorkFlow)
            //{
            //    (e.NewValue as WorkFlowEditor.ProcessAutomationViewModel).PropertyChanged += (d as DiagramFrontPage).FrontPage_PropertyChanged;
            //}

            front.DiagramCommonViewModel.ExportFiles = new DelegateCommand<object>(front.OnExport, front.CanExport);
            front.DiagramCommonViewModel.ImportFiles = new DelegateCommand<object>(front.OnImport);
            front.DiagramCommonViewModel.DeleteFiles = new DelegateCommand<object>(front.OnDeleteFiles);
            front.DiagramCommonViewModel.SelectAll = new DelegateCommand<object>(front.OnSelectAll);
            front.DiagramCommonViewModel.ClearSelection = new DelegateCommand<object>(front.OnClearSelection);
            front.DiagramCommonViewModel.RenameFile = new DelegateCommand<object>(front.OnRename, front.CanRename);
        }

        void FrontPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DiagramVisibility")
            {
                UpdateIsSelected();
            }
            else if (e.PropertyName == "CanExportImport")
            {
                if (this.DiagramCommonViewModel.CanExportImport == Windows.UI.Xaml.Visibility.Visible)
                {
                    if (DiagramType == DiagramType.UML)
                    {
                        this.GetParent<UMLDiagramDesigner.MainPage>().BottomAppBar.IsSticky = true;
                        this.GetParent<UMLDiagramDesigner.MainPage>().BottomAppBar.IsOpen = true;
                    }
                    else if (DiagramType == DiagramType.WorkFlow)
                    {
                        this.GetParent<WorkFlowEditor.MainPage>().BottomAppBar.IsSticky = true;
                        this.GetParent<WorkFlowEditor.MainPage>().BottomAppBar.IsOpen = true;
                    }
                    else if (DiagramType == DiagramType.MindMap)
                    {
                        this.GetParent<MindMapDemo.MainPage>().BottomAppBar.IsSticky = true;
                        this.GetParent<MindMapDemo.MainPage>().BottomAppBar.IsOpen = true;
                    }
                    else if (DiagramType == DiagramType.FloorPlan)
                    {
                        this.GetParent<FloorPlannerDemo.MainPage>().BottomAppBar.IsSticky = true;
                        this.GetParent<FloorPlannerDemo.MainPage>().BottomAppBar.IsOpen = true;
                    }
                }
                else
                {
                    if (DiagramType == DiagramType.UML)
                    {
                        this.GetParent<UMLDiagramDesigner.MainPage>().BottomAppBar.IsSticky = false;
                        this.GetParent<UMLDiagramDesigner.MainPage>().BottomAppBar.IsOpen = false;
                    }
                    else if (DiagramType == DiagramType.WorkFlow)
                    {
                        this.GetParent<WorkFlowEditor.MainPage>().BottomAppBar.IsSticky = false;
                        this.GetParent<WorkFlowEditor.MainPage>().BottomAppBar.IsOpen = false;
                    }
                    else if (DiagramType == DiagramType.MindMap)
                    {
                        this.GetParent<MindMapDemo.MainPage>().BottomAppBar.IsSticky = false;
                        this.GetParent<MindMapDemo.MainPage>().BottomAppBar.IsOpen = false;
                    }
                    else if (DiagramType == DiagramType.FloorPlan)
                    {
                        this.GetParent<FloorPlannerDemo.MainPage>().BottomAppBar.IsSticky = false;
                        this.GetParent<FloorPlannerDemo.MainPage>().BottomAppBar.IsOpen = false;
                    }
                }
            }
        }

        private void UpdateIsSelected()
        {
            if (fileList.SelectedItems.Count > 0 && DiagramCommonViewModel.DiagramVisibility == Visibility.Collapsed)
            {
                this.DiagramCommonViewModel.CanExportImport = Visibility.Visible;
            }
            else
            {
                this.DiagramCommonViewModel.CanExportImport = Visibility.Collapsed;
            }
        }

        void fileList_ItemClick(object sender, ItemClickEventArgs e)
        {
            FileItem clicked = e.ClickedItem as FileItem;
            clicked.Load.Execute(clicked.Name);
        }

        void fileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DiagramCommonViewModel.SelectedItemsCount = fileList.SelectedItems.Count;
            UpdateIsSelected();
        }

        private async Task<DialogResult> ShowDialog(IDialog replaceDialog)
        {
            Dialog.Content = replaceDialog;
            Dialog.Visibility = Visibility.Visible;
            DialogResult result = await replaceDialog.ShowDialog();
            Dialog.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Dialog.Content = null;
            return result;
        }

        private async void OnImport(object parameter)
        {
            DialogResult result = DialogResult.None;
            bool repeatResult = false;
            //if (EnsureUnsnapped())
            {
                FileOpenPicker fileDialog = new FileOpenPicker();
                fileDialog.CommitButtonText = "Import";
                fileDialog.FileTypeFilter.Add(".xml");

                var files = await fileDialog.PickMultipleFilesAsync();
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
                        if (!string.IsNullOrEmpty(SubFolder))
                        {
                            installedLocation = await installedLocation.GetFolderAsync(SubFolder);
                        }
                        bool fileExist = false;
                        StorageFile newFile = null;
                        var fs = await installedLocation.GetFilesAsync();
                        foreach (var f in fs)
                        {
                            if (f.Name == file.Name)
                            {
                                newFile = f;
                                fileExist = true;
                            }
                        }
                        if (fileExist)
                        {
                            if (!repeatResult)
                            {
                                ReplaceFilesDialog replaceDialog = new ReplaceFilesDialog();
                                replaceDialog.FileName = " " + file.Name + ".xml";
                                result = await ShowDialog(replaceDialog);
                                repeatResult = replaceDialog.Repeat;
                            }
                            switch (result)
                            {
                                case DialogResult.Cancel:
                                    return;
                                case DialogResult.No:
                                    continue;
                                case DialogResult.Yes:
                                    await file.CopyAndReplaceAsync(newFile);
                                    break;
                            }
                        }
                        else
                        {
                            newFile = await file.CopyAsync(installedLocation, file.Name);
                            ObservableCollection<FileItem> items =
                                fileList.DataContext as ObservableCollection<FileItem>;
                            items.Add(new FileItem() {Name = newFile.DisplayName, Load = this.Load});
                        }
                    }
                }
            }
        }

        private async void OnExport(object parameter)
        {
            DialogResult result = DialogResult.None;
            bool repeatResult = false;
            //if (EnsureUnsnapped())
            {
                FolderPicker exportDialog = new FolderPicker();
                exportDialog.CommitButtonText = "Export";
                exportDialog.FileTypeFilter.Add(".xml");
                StorageFolder targetFolder = await exportDialog.PickSingleFolderAsync();
                if (targetFolder != null)
                {
                    foreach (FileItem item in fileList.SelectedItems)
                    {
                        StorageFolder mindlocation = ApplicationData.Current.RoamingFolder;
                        if (!string.IsNullOrEmpty(SubFolder))
                        {
                            mindlocation = await mindlocation.GetFolderAsync(SubFolder);
                        }
                        StorageFile file = await mindlocation.GetFileAsync(item.Name + ".xml");

                        bool fileExist = false;
                        StorageFile newFile = null;
                        var fs = await targetFolder.GetFilesAsync();
                        foreach (var f in fs)
                        {
                            if (f.Name == file.Name)
                            {
                                newFile = f;
                                fileExist = true;
                            }
                        }
                        if (fileExist)
                        {
                            if (!repeatResult)
                            {
                                ReplaceFilesDialog replaceDialog = new ReplaceFilesDialog();
                                replaceDialog.FileName = " " + item.Name + ".xml";
                                result = await ShowDialog(replaceDialog);
                                repeatResult = replaceDialog.Repeat;
                            }
                            switch (result)
                            {
                                case DialogResult.Cancel:
                                    return;
                                case DialogResult.No:
                                    continue;
                                case DialogResult.Yes:
                                    await file.CopyAndReplaceAsync(newFile);
                                    break;
                            }
                        }
                        else
                        {
                            newFile = await file.CopyAsync(targetFolder, file.Name);
                        }
                    }
                }
            }
        }

        private bool CanExport(object parameter)
        {
            return fileList.SelectedItems.Count > 0;
        }

        private async void OnDeleteFiles(object parameter)
        {
            DeleteFileDialog delete = new DeleteFileDialog();
            DialogResult result = await ShowDialog(delete);
            switch (result)
            {
                case DialogResult.No:
                    return;
                case DialogResult.Cancel:
                    return;
            }
            List<FileItem> files = new List<FileItem>();
            foreach (FileItem item in fileList.SelectedItems)
            {
                StorageFolder mindlocation = ApplicationData.Current.RoamingFolder;
                if (!string.IsNullOrEmpty(SubFolder))
                {
                    mindlocation = await mindlocation.GetFolderAsync(SubFolder);
                }
                StorageFile file = await mindlocation.GetFileAsync(item.Name + ".xml");
                await file.DeleteAsync();
                files.Add(item);
            }
            foreach (FileItem item in files)
            {
                (fileList.DataContext as ObservableCollection<FileItem>).Remove(item);
            }
        }

        private async void OnRename(object parameter)
        {
            bool NameAlreadyExist = false;
            while (true)
            {
                if (fileList.SelectedItems.Count == 1)
                {
                    FileItem item = fileList.SelectedItems[0] as FileItem;
                    RenameFileDialog rename = new RenameFileDialog();
                    if (NameAlreadyExist)
                    {
                        rename.FileExist = Windows.UI.Xaml.Visibility.Visible;
                    }
                    else
                    {
                        rename.FileExist = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                    rename.NewFileName = item.Name;
                    DialogResult result = await ShowDialog(rename);
                    if (string.IsNullOrEmpty(rename.NewFileName) || rename.NewFileName == item.Name)
                    {
                        return;
                    }
                    switch (result)
                    {
                        case DialogResult.No:
                            return;
                        case DialogResult.Cancel:
                            return;
                    }
                    StorageFolder mindlocation = ApplicationData.Current.RoamingFolder;
                    if (!string.IsNullOrEmpty(SubFolder))
                    {
                        mindlocation = await mindlocation.GetFolderAsync(SubFolder);
                    }
                    StorageFile file = await mindlocation.GetFileAsync(item.Name + ".xml");
                    try
                    {
                        await file.RenameAsync(rename.NewFileName + ".xml");
                    }
                    catch
                    {
                        NameAlreadyExist = true;
                        continue;
                    }
                    item.Name = rename.NewFileName;
                    return;
                }
            }
        }

        private bool CanRename(object parameter)
        {
            return fileList.SelectedItems.Count == 1;
        }

        private void OnClearSelection(object parameter)
        {
            fileList.SelectedItems.Clear();
        }

        private void OnSelectAll(object parameter)
        {
            fileList.SelectAll();
        }

        //internal bool EnsureUnsnapped()
        //{
        //    bool unsnapped = ((ApplicationView.Value != ApplicationViewState.Snapped) || ApplicationView.TryUnsnap());
        //    if (!unsnapped)
        //    {
        //        //NotifyUser("Cannot unsnap the sample.", NotifyType.StatusMessage);
        //    }
        //    return unsnapped;
        //}

        public ICommand Create
        {
            get { return (ICommand)GetValue(CreateProperty); }
            set { SetValue(CreateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Create.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CreateProperty =
            DependencyProperty.Register("Create", typeof(ICommand), typeof(DiagramFrontPage), new PropertyMetadata(null, OnCreateChanged));

        private static void OnCreateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DiagramFrontPage).LoadDiagrams();
        }

        public ICommand Load
        {
            get { return (ICommand)GetValue(LoadProperty); }
            set { SetValue(LoadProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Load.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadProperty =
            DependencyProperty.Register("Load", typeof(ICommand), typeof(DiagramFrontPage), new PropertyMetadata(null));

        public ICommand Cancel { get; set; }

        private void OnCancel(object param)
        {
            if (prop != null)
            {
                Storyboard animation = prop.Resources["PropertyEditor_Storyboard_Collapse"] as Storyboard;
                animation.Stop();
                animation.Begin();
            }
        }


        private DiagramType _mtype;

        public DiagramType DiagramType
        {
            get
            {
                return _mtype;
            }
            set
            {
                if (_mtype != value)
                {
                    _mtype = value;               
                }
                switch (_mtype)
                {
                    case DiagramType.MindMap:
                        SubFolder = "MindMap";
                        uml.Visibility = Visibility.Collapsed;
                        wf.Visibility = Visibility.Collapsed;
                        fp.Visibility = Visibility.Collapsed;
                        break;
                    case DiagramType.WorkFlow:
                        SubFolder = "WorkFlow";
                        headingimage.Visibility = Visibility.Collapsed;
                        uml.Visibility = Visibility.Collapsed;
                        fp.Visibility = Visibility.Collapsed;
                        break;
                    case DiagramType.UML:
                        SubFolder = "UML";
                        headingimage.Visibility = Visibility.Collapsed;
                        wf.Visibility = Visibility.Collapsed;
                        fp.Visibility = Visibility.Collapsed;
                        break;
                    case DiagramType.FloorPlan:
                        SubFolder = "FloorPlan";
                        headingimage.Visibility = Visibility.Collapsed;
                        uml.Visibility = Visibility.Collapsed;
                        wf.Visibility = Visibility.Collapsed;

                        break;
           
                }
            }
        }

      
        FileItem creator;
        StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
        private async void LoadDiagrams()
        {  
           
            if (this.DiagramType == DiagramType.UML)
            {
                StorageFolder umlfile = await installedLocation.CreateFolderAsync("UML", CreationCollisionOption.OpenIfExists);
                IReadOnlyList<StorageFile> files = await umlfile.GetFilesAsync();
                ObservableCollection<FileItem> items = new ObservableCollection<FileItem>();
                string[] predefinedDiagrams = new string[] { "Diagram.xml", "Hospital.xml", "University.xml" };
                if (files.Where(file => file.FileType == ".xml").Count() == 0)
                {
                    foreach (string file in predefinedDiagrams)
                    {
                        String ResourceReference = "ms-appx:///Showcase/UMLDiagramDesigner/PreSavedUML/" + file;
                        StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(new Uri(ResourceReference, UriKind.Absolute));
                        await f.CopyAsync(umlfile, f.Name);
                    }
                    files = await umlfile.GetFilesAsync();
                }
                PopulateGridView(files, items);      
            }
            else if (this.DiagramType == DiagramType.MindMap)
            {
                StorageFolder mindmapfile = await installedLocation.CreateFolderAsync("MindMap",CreationCollisionOption.OpenIfExists);
                IReadOnlyList<StorageFile> files = await mindmapfile.GetFilesAsync();
                ObservableCollection<FileItem> items = new ObservableCollection<FileItem>();
                string[] predefinedDiagrams = new string[] { "ElementTree.xml", "Feelings.xml","MindMap.xml" };
                if (files.Where(file => file.FileType == ".xml").Count() == 0)
                {
                    foreach (string file in predefinedDiagrams)
                    {
                        String ResourceReference = "ms-appx:///Showcase/MindMapDemo/PreSavedMindMap/" + file;
                        StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(new Uri(ResourceReference, UriKind.Absolute));
                        await f.CopyAsync(mindmapfile, f.Name);
                    }
                    files = await mindmapfile.GetFilesAsync();
                }
                PopulateGridView(files, items);      
            }
            else if (this.DiagramType == DiagramType.WorkFlow)
            {
                StorageFolder workflowfile = await installedLocation.CreateFolderAsync("WorkFlow", CreationCollisionOption.OpenIfExists);
                IReadOnlyList<StorageFile> files = await workflowfile.GetFilesAsync();
                ObservableCollection<FileItem> items = new ObservableCollection<FileItem>();
                string[] predefinedDiagrams = new string[] { "Make ice cream.xml", "Doctor appointment.xml","Support Workflow.xml" };
                if (files.Where(file => file.FileType == ".xml").Count() == 0)
                {
                    foreach (string file in predefinedDiagrams)
                    {
                        String ResourceReference = "ms-appx:///Showcase/WorkFlowEditor/PreSavedWorkFlow/" + file;
                        StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(new Uri(ResourceReference, UriKind.Absolute));
                        await f.CopyAsync(workflowfile, f.Name);
                    }
                    files = await workflowfile.GetFilesAsync();
                }
                PopulateGridView(files, items);      
            }
            else if (this.DiagramType == DiagramType.FloorPlan)
            {
                StorageFolder floorplanfile = await installedLocation.CreateFolderAsync("FloorPlan", CreationCollisionOption.OpenIfExists);
                IReadOnlyList<StorageFile> files = await floorplanfile.GetFilesAsync();
                ObservableCollection<FileItem> items = new ObservableCollection<FileItem>();
                //"FloorPlan1.xml",
                string[] predefinedDiagrams = new string[] { "FloorPlan.xml" };
                if (files.Where(file => file.FileType == ".xml").Count() == 0)
                {
                    foreach (string file in predefinedDiagrams)
                    {
                        String ResourceReference = "ms-appx:///Showcase/FloorPlannerDemo/PresavedFloorPlan/" + file;
                        StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(new Uri(ResourceReference, UriKind.Absolute));
                        await f.CopyAsync(floorplanfile, f.Name);
                    }
                    files = await floorplanfile.GetFilesAsync();
                }
                PopulateGridView(files, items);  
            }
        }

        private async void PopulateGridView(IReadOnlyList<StorageFile> files, ObservableCollection<FileItem> items)
        {
            foreach (StorageFile file in files.Where(file => file.FileType == ".xml"))
            {
                FileItem item = new FileItem();
                item.Name = file.DisplayName;
                item.Load = this.Load;
                BasicProperties properties = await file.GetBasicPropertiesAsync();
                item.LastUpdated = properties.DateModified;
                items.Add(item);
            }

            creator = new FileItem { Name = "Untitled", Create = Create, Cancel = Cancel };
            NewFile.DataContext = creator;
            fileList.DataContext = items;
            checkName(items);
            Binding bin = new Binding();
            bin.Path = new PropertyPath("Name");
            textBox.SetBinding(TextBox.TextProperty, bin);
        }

     

        private void checkName(ObservableCollection<FileItem> items)
        {
            if (items != null)
            {
                foreach (FileItem item in items.Where(i => i.Name == creator.Name))
                {
                    creator.Name = "Untitled" + getFileCount(items);
                }
            }
        }

        private int getFileCount(ObservableCollection<FileItem> items)
        {
            int max = 1;
            foreach (string fileName in items.Where(i => i.Name.StartsWith("Untitled")).Select(i => i.Name.Replace("Untitled", "")))
            {
                int parsed = 0;
                if (int.TryParse(fileName, out parsed))
                {
                    if (max <= parsed)
                    {
                        max = parsed + 1;
                    }
                }
            }
            return max;
        }

        Grid prop;

        private void Pop_NewFileCreator(object sender, PointerRoutedEventArgs e)
        {
            prop = ((sender as Grid).Children[0] as Grid);//.Children[0] as Grid;

            if (prop.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                checkName(fileList.DataContext as ObservableCollection<FileItem>);
                prop.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Storyboard animation;
                if (DiagramType == DiagramType.MindMap)
                {
                    animation = prop.Resources["PropertyEditor_Storyboard_Visible"] as Storyboard;
                    animation.Stop();
                    animation.Begin();
                    prop.Children[1].Visibility = Visibility.Collapsed;
                }
                else
                {
                    animation = prop.Resources["PropertyEditor_Storyboard_Visible1"] as Storyboard;
                    animation.Stop();
                    animation.Begin();
                    prop.Height = 60;
                    prop.Children[0].Visibility = Visibility.Collapsed;
                }
                
                
                if (textBox != null)
                {
                    textBox.Focus(FocusState.Keyboard);
                }
            }
            else
            {
                HideCreator();
            }
        }

        private void HideCreator()
        {
            if (prop.Visibility != Windows.UI.Xaml.Visibility.Collapsed)
            {
                if (DiagramType == DiagramType.MindMap)
                {
                    Storyboard animation = prop.Resources["PropertyEditor_Storyboard_Collapse"] as Storyboard;
                    animation.Stop();
                    animation.Begin();
                }
                else
                {
                    Storyboard animation = prop.Resources["PropertyEditor_Storyboard_Collapse1"] as Storyboard;
                    animation.Stop();
                    animation.Begin();
                }
            }
        }


        private void NewFileCreate_Clicked(object sender, RoutedEventArgs e)
        {
            HideCreator();
            //if (fileList.DataContext as ObservableCollection<FileItem> != null)
            //{
                ObservableCollection<FileItem> data = fileList.DataContext as ObservableCollection<FileItem>;
            //}
            //else
            //{
                
            //}
            Button src = sender as Button;
            if (data != null)
            {

                if (data.Where(i => i.Name == src.CommandParameter.ToString()).Count() == 0)
                {
                    data.Add(
                        new FileItem
                        {
                            Name = (sender as Button).CommandParameter.ToString(),
                            Load = this.Load,
                            LastUpdated = DateTimeOffset.Now
                        });
                    if (DiagramType == DiagramType.MindMap)
                    {
                        string str = LayoutTypeSelection.SelectedIndex == 0 ? "Bowtie" : "Circular";
                        this.Create.Execute(src.CommandParameter + "_type" + str);
                    }
                    else
                    {
                        this.Create.Execute(src.CommandParameter + "");
                    }
                    checkName(data);
                }
            }
            else
            {
                
               
            }
        }

        TextBox textBox;

        private void textBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (DiagramType != DiagramType.MindMap)
            {
                textBox = sender as TextBox;
            }
        }
        private void fileName_Loaded(object sender, RoutedEventArgs e)
        {
            if (DiagramType == DiagramType.MindMap)
            {
                textBox = sender as TextBox;
            }
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HideCreator();
        }

       
    }
    public class FileItem : INotifyPropertyChanged, IDisposable
    {
        private string m_Name;

        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private ICommand m_Create;

        public ICommand Create
        {
            get { return m_Create; }
            set
            {
                if (m_Create != value)
                {
                    m_Create = value;
                    OnPropertyChanged("Create");
                }
            }
        }

        private ICommand m_Load;

        public ICommand Load
        {
            get { return m_Load; }
            set
            {
                if (m_Load != value)
                {
                    m_Load = value;
                    OnPropertyChanged("Load");
                }
            }
        }

        private ICommand m_Cancel;

        public ICommand Cancel
        {
            get { return m_Cancel; }
            set
            {
                if (m_Cancel != value)
                {
                    m_Cancel = value;
                    OnPropertyChanged("Cancel");
                }
            }
        }

        private DateTimeOffset m_LastUpdated;

        public DateTimeOffset LastUpdated
        {
            get { return m_LastUpdated; }
            set
            {
                if (m_LastUpdated != value)
                {
                    m_LastUpdated = value;
                    OnPropertyChanged("LastUpdated");
                }
            }
        }

        protected virtual void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            PropertyChanged = null;
            Create = null;
            Load = null;
            Cancel = null;
        }
    }
    public enum DiagramType
    {
        UML,
        MindMap,
        WorkFlow,
        FloorPlan
    }

    public class DiagramTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {

            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
