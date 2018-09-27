using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Syncfusion.UI.Xaml.Diagram;
using System.Collections.Specialized;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.ViewManagement;
using DiagramBuilder.Utility;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//using Windows.Graphics.Printing;

namespace DiagramBuilder.ViewModel
{
    public class DiagramBuilderVM : DiagramElementViewModel, IDiagramBuilderVM
    {
        #region Fields
        List<string> _mCaps = new List<string>
        {
            "M0,0 z",
            "M91.5617,-70.747L 103.107,-63.818L 91.5617,-56.8884L 91.5617,-70.747 Z M 92.8951,-68.3916L 92.8951,-59.2439L 100.516,-63.818L 92.8951,-68.3916 Z ",
            "M90.9239,-19.0316L 97.3249,-25.721L 103.743,-19.0108L 97.3444,-12.3193L 90.9239,-19.0316 Z M 97.3249,-23.7926L 92.7701,-19.0316L 97.3444,-14.2491L 101.899,-19.0108L 97.3249,-23.7926 Z ",
            "M91.39,27.9294C 91.39,24.652 94.0567,21.9854 97.334,21.9854C 100.613,21.9854 103.279,24.652 103.279,27.9294C 103.279,31.208 100.613,33.8747 97.334,33.8747C 94.0567,33.8747 91.39,31.208 91.39,27.9294 Z M 92.7234,27.9294C 92.7234,30.4723 94.7911,32.5413 97.334,32.5413C 99.8769,32.5413 101.946,30.4723 101.946,27.9294C 101.946,25.3864 99.8769,23.3187 97.334,23.3187C 94.7911,23.3187 92.7234,25.3864 92.7234,27.9294 Z ",
            "M94.1555,77.1443L 95.8989,73.6572L 94.1548,70.1696L 100.726,73.6559L 94.1555,77.1443 Z M 94.0747,74.3239L 91.0981,80.2771L 103.571,73.6559L 91.0981,67.0382L 94.0747,72.9906L 94.2336,73.3083L 94.408,73.6572L 94.2336,74.0061L 94.0747,74.3239 Z ",
            "M91.5617,106.39L 103.107,113.319L 91.5617,120.248L 91.5617,106.39 Z " ,
            "M90.9239,158.105L 97.3249,151.415L 103.743,158.126L 97.3444,164.817L 90.9239,158.105 Z ",
            "M91.39,205.066C 91.39,201.789 94.0567,199.122 97.334,199.122C 100.613,199.122 103.279,201.789 103.279,205.066C 103.279,208.345 100.613,211.011 97.334,211.011C 94.0567,211.011 91.39,208.345 91.39,205.066 Z ",
            "M94.0747,251.461L 91.0981,257.414L 103.571,250.793L 91.0981,244.175L 94.0747,250.127L 94.2336,250.445L 94.408,250.794L 94.2336,251.143L 94.0747,251.461 Z ",
            "M158.96817,399.85825 L202.42397,421.32401 157.92062,442.78977 181.48207,421.32422 z",
            "M0,0 L10,10 M10,0 L0,10",
        };

        private List<string> _mToolTipConstraints = new List<string>
        {
            "None",
            "Position",
            "Size",
            "Angle",
            "Default"
        };
        Visibility _mEditorVisbility { get; set; }
        Visibility _mEditorPickerVisibility { get; set; }

        StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
        private DiagramVM _selectedDiagram;
        private IPropertyEditor _mSelectedEditor = null;
        private List<PageSize> _mFormat = new List<PageSize>
        {
            PageSize.Letter,
            PageSize.Folio,
            PageSize.Legal,
            PageSize.Ledger,

            PageSize.A5,
            PageSize.A4,
            PageSize.A3,
            PageSize.A2,
            PageSize.A1,
            PageSize.A0,

            PageSize.Custom
        };
        private List<PageOrientation> _mOrientations = new List<PageOrientation>
        {
            PageOrientation.Landscape,
            PageOrientation.Portrait
        };
        private List<LengthUnits> _mUnits = new List<LengthUnits>
        {
            LengthUnits.Inches,
            LengthUnits.Feets,
            LengthUnits.Yards,
            LengthUnits.Millimeters,
            LengthUnits.Centimeters,
            LengthUnits.Meters,
            LengthUnits.Pixels,
        };
        private List<ConnectType> _mTypes = new List<ConnectType>
        {
            ConnectType.Straight,
            ConnectType.Orthogonal,
            ConnectType.Bezier
        };
        private static List<FontFamily> _mFonts = new List<FontFamily> { 
            new FontFamily("Segoe UI"),
            new FontFamily("Calibri"),
            new FontFamily("Arial"),
            new FontFamily("Comic Sans MS"),
            new FontFamily("Georgia"),
            new FontFamily("Times New Roman")
        };
        List<string> _mDashDots = new List<string> 
                                    { 
                                      "1,0",
                                       "1,2",
                                       "5,5,5,5",
                                       "4,4,1,2"
                                    };
        #endregion

        #region Diagrams

        public DiagramBuilderVM()
        {
            New = new Command(OnNewCommand);
            Import = new Command(OnImportCommand);
            Export = new Command(OnExportCommand);
            Save = new Command(OnSaveCommand);
            SaveAll = new Command(OnSaveAllCommand);
            Delete = new Command(OnDeleteCommand);
            Print = new Command(OnPrintCommand);
            ZoomIn = new Command(OnZoomInCommand);
            ZoomOut = new Command(OnZoomOutCommand);
            Stencil = new StencilVM();
            Diagrams = new ObservableCollection<DiagramVM>();
            LoadDiagram();
            //Diagrams.Add(GetNewDiagramVM());
            //SelectedDiagram = Diagrams[0];


#if !SyncfusionFramework4_5_1
            Framework = Visibility.Collapsed;
#else
            Framework = Visibility.Visible; 
#endif
            Stencil.PropertyChanged += Stencil_PropertyChanged;
            PropertyPanelVM();
        }

        void Stencil_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedSymbol")
            {
                if ((sender as StencilVM).SelectedSymbol!=null)
                {
                  (sender as StencilVM).SelectedSymbol.DoubleTapped += SelectedSymbol_DoubleTapped;
                }
               
            }
        }

        void SelectedSymbol_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            if (this.SelectedDiagram.SelectedItems !=null && ((this.SelectedDiagram.SelectedItems as SelectorVM).Nodes as ICollection<object>).Count > 0)
            {
                ((this.SelectedDiagram.SelectedItems as SelectorVM).Nodes as ICollection<object>).Clear();
            }
                NodeVM node = new NodeVM();
                IGraph graph = this.SelectedDiagram.Info as IGraph;
                node.OffsetX = graph.ScrollSettings.ScrollInfo.ViewportWidth / 2;
                node.OffsetY = graph.ScrollSettings.ScrollInfo.ViewportHeight / 2;
                node.UnitWidth = 100;
                node.UnitHeight = 100;
                node.Content = Stencil.SelectedSymbol.Content;
                node.ContentTemplate = Stencil.SelectedSymbol.ContentTemplate;
                node.IsSelected = true;
                this.SelectedDiagram.NodeCollection.Add(node);
                e.Handled = true;
        }

        private DiagramVM GetNewDiagramVM(StorageFile file, bool isValidXml)
        {
            DiagramVM diagram = new DiagramVM(file, isValidXml) { Title = "Untitled" };
            diagram.PortVisibility = PortVisibility.Collapse;
            diagram.Delete = Delete;
            diagram.PropertyChanged += (s, e) =>
                {
                    DiagramVM sender = s as DiagramVM;
                    if (e.PropertyName == "IsSelected")
                    {
                        if (sender.IsSelected == true)
                        {
                            this.SelectedDiagram = sender;
                        }
                    }
                };
            return diagram;
        }

        public DiagramVM SelectedDiagram
        {
            get { return _selectedDiagram; }
            set
            {
                var old = _selectedDiagram;
                if (_selectedDiagram != value)
                {
                    if (_selectedDiagram != null)
                    {
                        _selectedDiagram.IsSelected = false;
                    }
                    _selectedDiagram = value;
                    if (_selectedDiagram != null)
                    {
                        _selectedDiagram.IsSelected = true;
                    }
                    OnPropertyChanged("SelectedDiagram");
                }
                //if (_selectedDiagram == null && SelectedDiagram != old)
                //{
                //    SelectedDiagram = old;
                //}
            }
        }

        public ObservableCollection<DiagramVM> Diagrams { get; set; }

        #endregion

        #region PropertyPanel

        private void PropertyPanelVM()
        {
            Editors = new ObservableCollection<IPropertyEditor>();
            Editors.CollectionChanged += Editors_CollectionChanged;
            SelectEditor = new Command(OnSelectEditorCommand);
            EditorPicker = new Command(OnEditorPickerCommand);
            EditorVisbility = Visibility.Visible;
            EditorPickerVisibility = Visibility.Visible;
        }

        void Editors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (IPropertyEditor item in e.NewItems)
                {
                    item.PropertyChanged += (src, evt) =>
                        {
                            if (item.IsSelected)
                            {
                                SelectedEditor = item;
                            }
                        };
                    if (item.IsSelected)
                    {
                        SelectedEditor = item;
                    }
                }
            }
        }

        public StencilVM Stencil { get; set; }
        public ObservableCollection<IPropertyEditor> Editors { get; set; }
        public IPropertyEditor SelectedEditor
        {
            get
            {
                return _mSelectedEditor;
            }
            set
            {
                if (_mSelectedEditor != value)
                {
                    if (_mSelectedEditor != null)
                    {
                        _mSelectedEditor.IsSelected = false;
                    }
                    _mSelectedEditor = value;
                    if (_mSelectedEditor != null)
                    {
                        _mSelectedEditor.IsSelected = true;
                    }
                    OnPropertyChanged("SelectedEditor");
                    if(SelectedDiagram != null)
                    {
                        DiagramVM diagram = SelectedDiagram as DiagramVM;
                        SelectorVM sel = diagram.SelectedItems as SelectorVM;
                        sel.IsBrushEditing = Visibility.Collapsed;
                    }
                }
            }
        }

        private Visibility _mPanelVisibility=Visibility.Visible;
        private Visibility _mPanelPickerVisibility = Visibility.Visible;

        public Visibility PanelVisibility
        {
            get
            {
                return _mPanelVisibility;
            }
            set
            {
                if (_mPanelVisibility != value)
                {
                    _mPanelVisibility = value;
                    OnPropertyChanged("PanelVisibility");
                }
            }
        }
         
        public Visibility PanelPickerVisibility
        {
            get
            {
                return _mPanelPickerVisibility;
            }
            set
            {
                if (_mPanelPickerVisibility != value)
                {
                    _mPanelPickerVisibility = value;
                    OnPropertyChanged("PanelPickerVisibility");
                }
            }
        }
        #endregion

        #region Options

        public List<string> ToolTipConstraints
        {
            get
            {
                return _mToolTipConstraints;
            }
            set
            {
                if (_mToolTipConstraints != value)
                {
                    _mToolTipConstraints = value;
                }
            }
        }

        public List<string> Caps
        {
            get
            {
                return _mCaps;
            }
            set
            {
                if (_mCaps != value)
                {
                    _mCaps = value;
                    OnPropertyChanged(ConnectorConstants.Caps);
                }
            }
        }
        public List<ConnectType> Types
        {
            get
            {
                return _mTypes;
            }
            set
            {
                if (_mTypes != value)
                {
                    _mTypes = value;
                    OnPropertyChanged(ConnectorConstants.Type);
                }
            }
        }
        public static List<FontFamily> Fonts
        {
            get
            {
                return _mFonts;
            }
            set
            {
                if (_mFonts != value)
                {
                    _mFonts = value;
                }
            }
        }
        public List<PageSize> Format
        {
            get
            {
                return _mFormat;
            }
            set
            {
                if (_mFormat != value)
                {
                    _mFormat = value;
                    OnPropertyChanged(PageConstants.Format);
                }
            }
        }
        public List<PageOrientation> Orientations
        {
            get
            {
                return _mOrientations;
            }
            set
            {
                if (_mOrientations != value)
                {
                    _mOrientations = value;
                    OnPropertyChanged(PageConstants.Orientations);
                }
            }
        }
        public List<LengthUnits> Units
        {
            get
            {
                return _mUnits;
            }
            set
            {
                if (_mUnits != value)
                {
                    _mUnits = value;
                    OnPropertyChanged(PageConstants.Units);
                }
            }
        }
        public List<string> DashDots { get { return _mDashDots; } }
        public Visibility EditorVisbility
        {
            get
            {
                return _mEditorVisbility;
            }
            set
            {
                if (_mEditorVisbility != value)
                {                   
                    _mEditorVisbility = value;
                    OnPropertyChanged("EditorVisbility");
                }
            }
        }
        public Visibility EditorPickerVisibility
        {
            get
            {
                return _mEditorPickerVisibility;
            }
            set
            {
                if (_mEditorPickerVisibility != value)
                {
                    _mEditorPickerVisibility = value;
                    OnPropertyChanged("EditorPickerVisibility");
                }
            }
        }

        #endregion

        #region Commands

        public ICommand New
        {
            get;
            set;
        }

        public ICommand Save
        {
            get;
            set;
        }

        public ICommand SaveAll
        {
            get;
            set;
        }

        public ICommand Import
        {
            get;
            set;
        }

        public ICommand Delete { get; set; }

        public ICommand Export
        {
            get;
            set;
        }

        public ICommand Print
        {
            get;
            set;
        }

        public ICommand SelectEditor
        {
            get;
            set;
        }


      
        public ICommand EditorPicker { get; set; }

        public ICommand Exit { get; set; }
        public ICommand ZoomIn { get; set; }
        public ICommand ZoomOut { get; set; }
       
        public async void OnNewCommand(object param)
        {
            StorageFile s = null;
            string parameter = Guid.NewGuid().ToString("N");
            //if (EnsureUnsnapped())
            {
                s = await installedLocation.CreateFileAsync(parameter + ".xml", CreationCollisionOption.FailIfExists);
            }
            if (s != null)
            {
                DiagramVM newDiagram = GetNewDiagramVM(s, false);
                Diagrams.Add(newDiagram);
                SelectedDiagram = newDiagram;
            }
            await SaveFileIndex();
        }
   
        //private bool EnsureUnsnapped()
        //{
        //    bool unsnapped = ((ApplicationView.Value != ApplicationViewState.Snapped) || ApplicationView.TryUnsnap());
        //    if (!unsnapped)
        //    {
        //        //NotifyUser("Cannot unsnap the sample.", NotifyType.StatusMessage);
        //    }
        //    return unsnapped;
        //}

        public async void OnSaveCommand(object param)
        {
            await SelectedDiagram.Save();
            await SaveFileIndex();
        }

        public async void OnDeleteCommand(object param)
        {
            DiagramVM diag = param as DiagramVM;
            int index = Diagrams.IndexOf(diag);
            Diagrams.Remove(diag);
            if (diag.IsSelected && Diagrams.Count > 0)
            {
                if (Diagrams.Count == index)
                {
                    Diagrams[index - 1].IsSelected = true;
                }
                else if (Diagrams.Count > index)
                {
                    Diagrams[index].IsSelected = true;
                }
            }
            await SaveFileIndex();
            if (Diagrams.Count == 0)
            {
                Exit.Execute(null);
            }
        }

        public async void OnSaveAllCommand(object param)
        {
            await SaveAllDiagrams();
        }
       

        public void OnImportCommand(object param)
        {

        }

        public void OnExportCommand(object param)
        {
            SelectedDiagram.Export.Execute(param);
        }

        public void OnPrintCommand(object param)
        {
#if SyncfusionFramework4_5_1
            PageVM page = SelectedDiagram.PageSettings as PageVM;

            PrintingService print = SelectedDiagram.PrintingService;
            print.PrintMargin = page.PrintMargin;
            switch (page.SelectedFormat)
            {
                case PageSize.A0:
                    print.PrintMediaSize = PrintMediaSize.IsoA0;
                    break;
                case PageSize.A1:
                    print.PrintMediaSize = PrintMediaSize.IsoA1;
                    break;
                case PageSize.A2:
                    print.PrintMediaSize = PrintMediaSize.IsoA2;
                    break;
                case PageSize.A3:
                    print.PrintMediaSize = PrintMediaSize.IsoA3;
                    break;
                case PageSize.A4:
                    print.PrintMediaSize = PrintMediaSize.IsoA4;
                    break;
                case PageSize.A5:
                    print.PrintMediaSize = PrintMediaSize.IsoA5;
                    break;
                case PageSize.Folio:
                    print.PrintMediaSize = PrintMediaSize.OtherMetricFolio;
                    break;
                case PageSize.Ledger:
                    print.PrintMediaSize = PrintMediaSize.NorthAmericaTabloid;
                    break;
                case PageSize.Legal:
                    print.PrintMediaSize = PrintMediaSize.NorthAmericaLegal;
                    break;
                case PageSize.Letter:
                    print.PrintMediaSize = PrintMediaSize.NorthAmericaLetter;
                    break;
            }
            if (page.PageOrientation == PageOrientation.Landscape)
            {
                print.PrintOrientation = PrintOrientation.Landscape;
            }
            else
            {
                print.PrintOrientation = PrintOrientation.Portrait;
            }

            print.UnregisterForPrinting();
             print.RegisterForPrinting();
            //await Windows.Graphics.Printing.PrintManager.ShowPrintUIAsync(); 
#endif
        }

        public void OnSelectEditorCommand(object param)
        {
            string type = param as string;
            switch (type)
            { 
                case "Show":
                    EditorVisbility = Visibility.Visible;
                    break;
                case "Arrange":
                    SelectedEditor = Editors.FirstOrDefault(edit => edit.Title == type);
                    EditorVisbility = Visibility.Visible;
                    break;
                case "Property":
                    SelectedEditor = Editors.FirstOrDefault(edit => edit.Title == type);
                    EditorVisbility = Visibility.Visible;
                    break;
                case "Text":
                    SelectedEditor = Editors.FirstOrDefault(edit => edit.Title == type);
                    EditorVisbility = Visibility.Visible;
                    break;
                case "Stencil":
                    SelectedEditor = Editors.FirstOrDefault(edit => edit.Title == type);
                    EditorVisbility = Visibility.Visible;
                    break;
                case "Page":
                    SelectedEditor = Editors.FirstOrDefault(edit => edit.Title == type);
                    EditorVisbility = Visibility.Visible;
                    break;
                case "Hide":
                    EditorVisbility = Visibility.Collapsed;
                    break;
                case "Toggle":
                    if (EditorVisbility == Visibility.Collapsed)
                    {
                        EditorVisbility = Visibility.Visible;
                    }
                    else
                    {
                        EditorVisbility = Visibility.Collapsed;
                    }
                    break;
            }
        }

        public void OnEditorPickerCommand(object param)
        {
            string type = param as string;
            switch (type)
            { 
                case "Current":
                    EditorPickerVisibility = Visibility.Visible;
                    break;
                case "Toggle":
                    if (EditorPickerVisibility == Visibility.Collapsed)
                    {
                        EditorPickerVisibility = Visibility.Visible;
                    }
                    else
                    {
                        EditorPickerVisibility = Visibility.Collapsed;
                    }
                    break;
            }
        }

        public async void LoadDiagram()
        {
            try
            {
                installedLocation = await installedLocation.CreateFolderAsync("DiagramBuilder", CreationCollisionOption.OpenIfExists);
                StorageFile indexFile = null;
                IReadOnlyList<StorageFile> files = await installedLocation.GetFilesAsync();
                indexFile = files.Where(f => f.Name == "index.xml").FirstOrDefault();
                if (indexFile != null)
                {
                    XmlSerializer deSerializer = new XmlSerializer(typeof(List<FileInfo>), new Type[] { typeof(FileInfo) });
                    List<FileInfo> fileIndex = null;
                    using (Stream stream = await indexFile.OpenStreamForReadAsync())
                    {
                        fileIndex = deSerializer.Deserialize(stream) as List<FileInfo>;
                    }

                    foreach (FileInfo fileInfo in fileIndex.OrderBy(e => e.Index))
                    {
                        if (!files.Any(f => f.Name == fileInfo.FileName))
                        {
                            continue;
                        }
                        StorageFile file = await installedLocation.GetFileAsync(fileInfo.FileName);
                        DiagramVM newdiagram = GetNewDiagramVM(file, true);
                        newdiagram.IsSelected = fileInfo.Selected;
                        newdiagram.Title = fileInfo.Title;
                        Diagrams.Add(newdiagram);

                        //FileItem item = new FileItem();
                        //item.Name = file.DisplayName;
                        //item.Load = this.Load;
                        //BasicProperties properties = await file.GetBasicPropertiesAsync();
                        //item.LastUpdated = properties.DateModified;
                        //items.Add(item);
                    }
                }
            }
            catch
            { 
            }
            if (Diagrams.Count == 0)
            {
                OnNewCommand(null);
            }
            //else
            //{
            //    SelectedDiagram = Diagrams[0];
            //}
        }
        public void OnZoomInCommand(object param)
        {
            (SelectedDiagram.Info as IGraphInfo).Commands.Zoom.Execute(new ZoomPositionParamenter() { ZoomFactor=0.2, ZoomCommand = ZoomCommand.ZoomIn});
         

        }
        public void OnZoomOutCommand(object param)
        {
            (SelectedDiagram.Info as IGraphInfo).Commands.Zoom.Execute(new ZoomPositionParamenter() { ZoomFactor = 0.2, ZoomCommand = ZoomCommand.ZoomOut });
        }

        private StorageFile indexFile;
        private async Task SaveFileIndex()
        {
            if(indexFile != null)
            {
                return;
            }
            List<FileInfo> fileIndex = new List<FileInfo>();
            int i=0;
            foreach (var diagram in Diagrams)
            {
                FileInfo info = new FileInfo()
                {
                    FileName = diagram.GetFileName(),
                    Index = i++,
                    Selected = SelectedDiagram == diagram,
                    Title = diagram.Title
                };
                fileIndex.Add(info);
            }
            try
            {
                indexFile = await installedLocation.CreateFileAsync("index.xml", CreationCollisionOption.ReplaceExisting);
                XmlSerializer serializer = new XmlSerializer(typeof(List<FileInfo>), new Type[] { typeof(FileInfo) });
                using (Stream s = await indexFile.OpenStreamForWriteAsync())
                {
                    serializer.Serialize(s, fileIndex);
                }
            }
            //catch { }
            finally
            {
                indexFile = null;
            }
        }

        private async Task SaveAllDiagrams()
        {
            foreach (var diagram in Diagrams)
            {
                await diagram.Save();
            }
        }

        public async Task PrepareExit()
        {
            await SaveAllDiagrams();
            await SaveFileIndex();
        }
       
        #endregion

        public Visibility Framework { get; set; }
    }

    public interface IDiagramBuilderVM
    {
        ICommand New { get; set; }
        ICommand Delete { get; set; }
        ICommand Save { get; set; }
        ICommand SaveAll { get; set; }
        ICommand Import { get; set; }
        ICommand Export { get; set; }
        ICommand Print { get; set; }
        ICommand SelectEditor { get; set; }
        ICommand EditorPicker { get; set; }
        ICommand ZoomIn { get; set; }
        ICommand ZoomOut { get; set; }
        Visibility EditorVisbility { get; set; }
        Visibility EditorPickerVisibility { get; set; }
        ICommand Exit { get; set; }
        Task PrepareExit();
        
        
      
        
    }

    public class FileInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public int Index { get; set; }
        public bool Selected { get; set; }
    }
}
