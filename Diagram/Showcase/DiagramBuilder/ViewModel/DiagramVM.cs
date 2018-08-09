using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using DiagramBuilder.Utility;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Xaml;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Orientation = Windows.UI.Xaml.Controls.Orientation;
using Windows.Storage;
using System.IO;
using Windows.Foundation;
using Windows.Storage.Pickers;
using Windows.Graphics.Imaging;
using System.Collections;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Media.Capture;
using Windows.Storage.Streams;
using Windows.Media.MediaProperties;

namespace DiagramBuilder.ViewModel
{
    public class DiagramVM : DiagramViewModel, IDiagramViewModel
    {
        public bool _isValidXml = false;
        public bool _isCustomVM = false;
        private bool _isSelected = false;
        private Brush _offPageBackground;
        private string _name;
        StorageFile _file;
        StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
        private Visibility _mIsBusy = Visibility.Visible;

        /// <summary>
        /// Gets or sets Diagram
        /// </summary>
        public ObservableCollection<DiagramVM> Diagrams { get; set; }
        public Visibility IsBusy
        {
            get { return _mIsBusy; }
            set
            {
                if (_mIsBusy != value)
                {
                    _mIsBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        public DiagramVM(StorageFile file, bool isValidXml)
        {
            _isValidXml = isValidXml;
            _file = file;
            Nodes = new ObservableCollection<NodeVM>();
            Connectors = new ObservableCollection<ConnectorVM>();
            Groups = new ObservableCollection<GroupVM>();
            SelectedItems = new SelectorVM(this);
            Select = new Command(param => IsSelected = true);
            FirstLoad = new Command(OnViewLoaded);

            SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.All,
                SnapToObject = SnapToObject.All
            };

            PageSettings = new PageVM();
            (PageSettings as PageVM).InitDiagram(this);
            this.HorizontalRuler = new Ruler() { Orientation = Orientation.Horizontal };
            this.VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };
            //OffPageBackground = new SolidColorBrush(new Color() { A = 0xFF, R = 0xEC, G = 0xEC, B = 0xEC });
            //OffPageBackground = new SolidColorBrush(new Color() { A = 0xFF, R = 0x2D, G = 0x2D, B = 0x2D });
            InitLocation();
#if SyncfusionFramework4_5_1
            ExportSettings = new ExportSettings()
            {
                ImageStretch = Stretch.Uniform,
                ExportMode = ExportMode.PageSettings
            };
            PrintingService = new PrintingService();
#endif
            Export = new Command(OnExportCommand);
            Captures = new Command(OnCapturesCommand);
            ClearDiagram = new Command(OnClearCommand);
            Upload = new Command(Onuploadcommand);
            Draw = new Command(OnDrawCommand);
            SingleSelect = new Command(OnSingleSelectCommand);
            SelectAll = new Command(OnSelectAllCommand);
            Manipulate = new Command(OnManipulateCommand);
            Port = new Command(OnPortCommand);
            Diagrams = new ObservableCollection<DiagramVM>();
        }
        public DiagramVM(bool IsCustomVM)
        {
            _isCustomVM = IsCustomVM;
            Nodes = new ObservableCollection<NodeVM>();
            Connectors = new ObservableCollection<ConnectorVM>();
            Groups = new ObservableCollection<GroupVM>();
            SelectedItems = new SelectorVM(this);

            Select = new Command(param => IsSelected = true);
            FirstLoad = new Command(OnViewLoaded);

            SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.All,
                SnapToObject = SnapToObject.All
            };

            PageSettings = new PageVM();
            (PageSettings as PageVM).InitDiagram(this);

            this.CommandManager.View = (Control)Window.Current.Content;
        }

         private void OnManipulateCommand(object obj)
       {
            Tool = Tool.MultipleSelect;
          
        }


        private void OnPortCommand(object obj)
        {
          Tool = Tool.MultipleSelect;
        }

        private async void InitLocation()
        {
            installedLocation = await installedLocation.CreateFolderAsync("DiagramBuilder", CreationCollisionOption.OpenIfExists);
        }

        void DiagramVM_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if (args.ItemSource == ItemSource.Stencil)
            {
                var dropedItem = args.Item as INode;
                if (dropedItem != null)
                {
                    dropedItem.UnitHeight = PageSettings.Unit.ToUnit(100);
                    dropedItem.UnitWidth = PageSettings.Unit.ToUnit(100);
                }
            }
            else if (args.ItemSource == ItemSource.DrawingTool)
            {
                IConnectorVM newCon = args.Item as IConnectorVM;
                if (newCon != null)
                {
                    switch (DefaultConnectorType)
                    {
                        case ConnectorType.Orthogonal:
                            newCon.Type = ConnectType.Orthogonal;
                            break;
                        case ConnectorType.Line:
                            newCon.Type = ConnectType.Straight;
                            break;
                        case ConnectorType.CubicBezier:
                            newCon.Type = ConnectType.Bezier;
                            break;
                    }
                }
                //DefaultConnectorType = ConnectorType.Orthogonal;
            }
            CheckEmpty();
        }

        void DiagramVM_ItemDeleted(object sender, DiagramEventArgs args)
        {
            CheckEmpty();        
        }

        private void CheckEmpty()
        {
            if ((NodeCollection != null && NodeCollection.Count > 0) ||
                (ConnectorCollection != null && ConnectorCollection.Count > 0) ||
                (GroupCollection != null && GroupCollection.Count > 0))
            {
                IsNonEmpty = true;
            }
            else
            {
                IsNonEmpty = false;
            }
        }

        private bool _mIsNonEmpty = false;
        public bool IsNonEmpty
        {
            get { return _mIsNonEmpty; }
            set
            {
                if (_mIsNonEmpty != value)
                {
                    _mIsNonEmpty = value;
                    OnPropertyChanged("IsNonEmpty");
                }
            }
        }

        private async void OnViewLoaded(object param)
        {
            if (!_isCustomVM)
            {
                IGraphInfo graph = Info as IGraphInfo;
                graph.ItemAdded += DiagramVM_ItemAdded;
                graph.ItemDeleted += DiagramVM_ItemDeleted;
                await Load();
                PageVM page = PageSettings as PageVM;
                if (_isValidXml)
                {
                    graph.Commands.Zoom.Execute(
                        new ZoomPositionParamenter()
                        {
                            ZoomTo = page.Scale
                        });
                    (this.Info as IGraph).ScrollSettings.ScrollInfo.PanTo(new Point(page.HOffset, page.VOffset));
                }
                else
                {
                    await Save();
                    (Info as IGraphInfo).Commands.FitToPage.Execute(
                        new FitToPageParameter
                        {
                            FitToPage = FitToPage.FitToPage,
                            Margin = new Thickness(20)
                        }
                        );
                    await this.Save();
                }
                IsBusy = Visibility.Collapsed;
            }
        }

        public string Title
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }



        public ObservableCollection<NodeVM> NodeCollection
        {
            get { return Nodes as ObservableCollection<NodeVM>; }
        }
        public ObservableCollection<ConnectorVM> ConnectorCollection
        {
            get { return Connectors as ObservableCollection<ConnectorVM>; }
        }
        public ObservableCollection<GroupVM> GroupCollection
        {
            get { return Groups as ObservableCollection<GroupVM>; }
        }

        public ICommand Select { get; set; }
        public ICommand FirstLoad { get; set; }
        public ICommand Export { get; set; }
        public ICommand ClearDiagram { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Draw { get; set; }
        public ICommand SingleSelect { get; set; }
        public ICommand SelectAll { get; set; }
        public ICommand Upload { get; set; }
        public ICommand Captures { get; set; }
        public ICommand Manipulate { get; set; }

        /// <summary>
        /// ICommand of port
        /// </summary>
        public ICommand Port { get; set; }
        public Brush OffPageBackground
        {
            get { return _offPageBackground; }
            set
            {
                _offPageBackground = value;
                OnPropertyChanged("OffpageBackground");
            }
        }

        private async Task Load()
        {
            try
            {
                if (_isValidXml)
                {
                    IGraphInfo graph = this.Info as IGraphInfo;
                    //using (Stream stream = _file.OpenStreamForReadAsync().GetAwaiter().GetResult())
                    using (Stream stream = await _file.OpenStreamForReadAsync())
                    {
                        graph.Load(stream);
                    }
                    (PageSettings as PageVM).InitDiagram();
                    (PageSettings as PageVM).InitDiagram(this);
                }
            }
            catch
            { }
        }

        public async Task Save()
        {
            try
            {
                IGraphInfo graph = this.Info as IGraphInfo;
                PageVM page = PageSettings as PageVM;
                if (graph != null && (this.Info as IGraph).ScrollSettings.ScrollInfo != null)
                {
                    page.HOffset = (this.Info as IGraph).ScrollSettings.ScrollInfo.HorizontalOffset;
                    page.VOffset = (this.Info as IGraph).ScrollSettings.ScrollInfo.VerticalOffset;
                    page.Scale = (this.Info as IGraph).ScrollSettings.ScrollInfo.CurrentZoom;
                    _file = await installedLocation.CreateFileAsync(_file.Name, CreationCollisionOption.ReplaceExisting);
                    using (Stream stream = await _file.OpenStreamForWriteAsync())
                    {
                        graph.Save(stream);
                    }
                    _isValidXml = true;
                }
            }
            catch
            { }
        }

        public async Task DeleteFile()
        {
            try
            {
                if (_file != null)
                {
                    await _file.DeleteAsync();
                }
            }
            catch
            { }
        }

        public string GetFileName()
        {
            if(_file!= null)
            {
                return _file.Name;
            }
            else
            {
                return string.Empty;
            }
        }

        public void OnExportCommand(object param)
        {
            try
            {
                string type = param as string;
                switch (type)
                {
                    case "Png":
                        SinglePageExporting(type, BitmapEncoder.PngEncoderId);
                        break;
                    case "Jpeg":
                        SinglePageExporting(type, BitmapEncoder.JpegEncoderId);
                        break;
                    case "Tiff":
                        SinglePageExporting(type, BitmapEncoder.TiffEncoderId);
                        break;
                    case "Gif":
                        SinglePageExporting(type, BitmapEncoder.GifEncoderId);
                        break;
                    case "Bitmap":
                        SinglePageExporting(type, BitmapEncoder.BmpEncoderId);
                        break;
                    case "JpegXR":
                        SinglePageExporting(type, BitmapEncoder.JpegEncoderId);
                        break;
                }
            }
            catch
            { }
        }

        private void SinglePageExporting(string p, Guid guid)
        {
#if SyncfusionFramework4_5_1
            IGraphInfo graph = this.Info as IGraphInfo;
            if (graph != null)
            {
                var savePicker = new FileSavePicker();
                savePicker.DefaultFileExtension = "." + p;
                savePicker.FileTypeChoices.Add("." + p, new List<string> { "." + p });
                savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                savePicker.SuggestedFileName = Title;

                // Prompt the user to select a file
                var saveFile = await savePicker.PickSaveFileAsync();

                // Verify the user selected a file
                if (saveFile == null)
                    return;
                // Encode the image to the selected file on disk
                using (var fileStream = await saveFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    ExportSettings.ExportBitmapEncoder = await BitmapEncoder.CreateAsync(guid, fileStream);
                    //Method to Export the SfDiagram
                    await graph.Export();
                }
            } 
#endif
        }

        public void OnClearCommand(object param)
        {
            NodeCollection.Clear();
            ConnectorCollection.Clear();
            GroupCollection.Clear();
        }

       async private void Onuploadcommand(object obj)
        {
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            var file = await picker.PickSingleFileAsync();
            if (file == null) return;
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            BitmapImage image = new BitmapImage();
            image.SetSource(stream);           
            IGraphInfo graph = this.Info as IGraphInfo;          
            NodeVM node = new NodeVM();          
            node.OffsetX = ((this.Info as IGraph).ScrollSettings.ScrollInfo.ViewportWidth) / 2;
            node.OffsetY = ((this.Info as IGraph).ScrollSettings.ScrollInfo.ViewportHeight)/2;
            node.UnitHeight = 100;
            node.UnitWidth = 100;
            node.Content = new Image() { Source = image,Stretch=Stretch.Fill};
            (Nodes as ObservableCollection<NodeVM>).Add(node);           
            
        }
        async public void OnCapturesCommand(object param)
        {

            MediaCapture takePhotoManager = new MediaCapture();
            await takePhotoManager.InitializeAsync();
            ImageEncodingProperties imgFormat = ImageEncodingProperties.CreateJpeg();
            IStorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                                        "Photo.jpg", CreationCollisionOption.GenerateUniqueName);
            await takePhotoManager.CapturePhotoToStorageFileAsync(imgFormat, file);
            //Windows.Storage.StorageFile file = await cameraUI.CaptureFileAsync(CameraCaptureUIMode.PhotoOrVideo);

            if (file != null)
            {
                IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                BitmapImage bitmap = new BitmapImage();
                bitmap.SetSource(fileStream);

                IGraphInfo graph = this.Info as IGraphInfo;
                NodeVM node = new NodeVM();
                node.OffsetX = ((this.Info as IGraph).ScrollSettings.ScrollInfo.ViewportWidth) / 2;
                node.OffsetY = ((this.Info as IGraph).ScrollSettings.ScrollInfo.ViewportHeight) / 2;
                node.UnitHeight = 100;
                node.UnitWidth = 100;
                node.Content = new Image() { Source = bitmap, Stretch = Stretch.Fill };
                (Nodes as ObservableCollection<NodeVM>).Add(node);

            }
        }
        public void OnDrawCommand(object param)
        {
            string type = param as string;
            switch (type)
            { 
                case "Straight":
                    DefaultConnectorType = ConnectorType.Line;
                    break;
                case "Ortho":
                    DefaultConnectorType = ConnectorType.Orthogonal;
                    break;
                case "Bezier":
                    DefaultConnectorType = ConnectorType.CubicBezier;
                    break;
                case "Polyline":
                    DefaultConnectorType = ConnectorType.PolyLine;
                    break;
                    //case "FreeHand":
                    //    DefaultConnectorType = ConnectorType.PolyCubicBezier;
                    //    break;
            }
            Tool |= Tool.ContinuesDraw;
            PortVisibility = PortVisibility.MouseOverOnConnect;
        }
        public void OnSingleSelectCommand(object param)
        {
            string type = param as string;

            if (type == "Node" && Nodes != null)
            {
                foreach (IConnector o in ConnectorsE)
                {
                    o.IsSelected = false;
                }

                foreach (INode o in NodesE)
                {
                    o.IsSelected = true;
                }
            }
            else if (type == "Connector" && ConnectorsE != null)
            {
                foreach (INode o in NodesE)
                {
                    o.IsSelected = false;
                }

                foreach (IConnector o in ConnectorsE)
                {
                    o.IsSelected = true;
                }
            }
        }
        public void OnSelectAllCommand(object param)
        {
            string type = param as string;

            if (type == "Node" && Nodes != null)
            {
                foreach (IConnector o in ConnectorsE)
                {
                    o.IsSelected = false;
                }

                foreach (INode o in NodesE)
                {
                    o.IsSelected = true;
                }
            }
            if (type == "Connector" && ConnectorsE != null)
            {
                foreach (INode o in NodesE)
                {
                    o.IsSelected = false;
                }

                foreach (IConnector o in ConnectorsE)
                {
                    o.IsSelected = true;
                }
            }
        }

#if !SyncfusionFramework4_5_1
        public object ExportSettings { get; set; }
        public object PrintingService { get; set; }        
#endif
    
        public IEnumerable ConnectorsE
        {
            get { return this.Connectors as IEnumerable; }
        }
        public IEnumerable NodesE
        {
            get { return this.Nodes as IEnumerable; }
        }
        public IEnumerable GroupsE
        {
            get { return this.Groups as IEnumerable; }
        }
    
    }


    public interface IDiagramViewModel : IGraph
    {
        ICommand Select { get; set; }
        ICommand FirstLoad { get; set; }
        string Title { get; set; }
        bool IsSelected { get; set; }
        ICommand Export { get; set; }
        ICommand ClearDiagram { get; set; }
        ICommand Delete { get; set; }
        ICommand Draw { get; set; }
        ICommand SingleSelect { get; set; }
        ICommand SelectAll { get; set; }
        ICommand Manipulate { get; set; }

        /// <summary>
        /// ICommand Port
        /// </summary>
        ICommand Port { get; set; }
    }
}
