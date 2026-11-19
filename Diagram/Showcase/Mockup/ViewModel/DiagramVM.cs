using Mockup.BusinessObject;
using Mockup.Utility;
using Syncfusion.SampleBrowser.UWP.Diagram;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Mockup.ViewModel
{
    public class DiagramVM : DiagramViewModel, IDiagramViewModel
    {
        public bool _isValidXml = false;
        public bool _isCustomVM = false;
        private bool _isSelected = false;
        private bool _isPanEnabled = false;
        private Brush _offPageBackground;
        private string _name;
        StorageFile _file;
        StorageFolder installedLocation = ApplicationData.Current.RoamingFolder;
        private Visibility _mIsBusy = Visibility.Visible;
        private Tool? _ToolBeforeEnablePanning = null;
        ResourceDictionary resourceDictionary = null;
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
            resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Showcase/Mockup/Theme/MockupUI.xaml", UriKind.Absolute)
            };
            _isValidXml = isValidXml;
            _file = file;
            Nodes = new ObservableCollection<NodeVM>();
            Connectors = new ObservableCollection<ConnectorVM>();
            Groups = new ObservableCollection<GroupVM>();
            SelectedItems = new SelectorVM(this);
            Rename = new Command(OnRenameCommand);
            Select = new Command(param => IsSelected = true);
            FirstLoad = new Command(OnViewLoaded);            
            SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.All &~ (SnapConstraints.SnapToLines),
                SnapToObject = SnapToObject.All
            };
            PageSettings = new PageVM();            
            (PageSettings as PageVM).InitDiagram(this);
            this.HorizontalRuler = new Ruler() { Orientation = Orientation.Horizontal };
            this.VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };
            //OffPageBackground = new SolidColorBrush(new Color() { A = 0xFF, R = 0xEC, G = 0xEC, B = 0xEC });
            //OffPageBackground = new SolidColorBrush(new Color() { A = 0xFF, R = 0x2D, G = 0x2D, B = 0x2D });
            InitLocation();
//#if SyncfusionFramework4_5_1
            //ExportSettings = new ExportSettings()
            //{
            //    ImageStretch = Stretch.Uniform,
            //    ExportMode = ExportMode.PageSettings
            //};
            //PrintingService = new PrintingService();
//#endif
            Export = new Command(OnExportCommand);
            Captures = new Command(OnCapturesCommand);
            ClearDiagram = new Command(OnClearCommand);
            Upload = new Command(Onuploadcommand);
            Draw = new Command(OnDrawCommand);
            SingleSelect = new Command(OnSingleSelectCommand);
            SelectAll = new Command(OnSelectAllCommand);
            Manipulate = new Command(OnManipulateCommand);
            MockupAttachedPropertiesCommand = new Command(OnMockupAttachedPropertiesCommand);
            NeighbourDiagrams = new ObservableCollection<DiagramVM>();

            _mTitleTemplate= resourceDictionary["TabbedDiagramTitleTextBlock"] as DataTemplate;
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

        }

        public ObservableCollection<DiagramVM> NeighbourDiagrams { get; set; }

        private void OnManipulateCommand(object obj)
        {
            Tool = Tool.MultipleSelect;

        }
        private async void InitLocation()
        {
            installedLocation = await installedLocation.CreateFolderAsync("Syncfusion", CreationCollisionOption.OpenIfExists);
        }

        #region Mockup
        private Size FindDummyTextSize(LabelVM _mAnnotation, string text, TextWrapping wrap = TextWrapping.NoWrap)
        {
            if (!string.IsNullOrEmpty(text))
            {
                TextBlock dummyTextBlock = new TextBlock();
                dummyTextBlock.FontFamily = _mAnnotation.Font;
                dummyTextBlock.FontSize = _mAnnotation.FontSize;
                dummyTextBlock.FontStyle = _mAnnotation.FontStyle;
                dummyTextBlock.FontWeight = _mAnnotation.FontWeight;
                dummyTextBlock.TextWrapping = wrap;
                dummyTextBlock.Text = text;
                if (wrap == TextWrapping.Wrap)
                {
                    //dummyTextBlock.Width = _mAnnotation.LabelWidth;
                    //dummyTextBlock.Height = _mAnnotation.LabelHeight;
                }
                //dummyTextBlock.Measure(new Size(double.PositiveInfinity,double.PositiveInfinity));
                dummyTextBlock.Measure(new Size(0, 0));
                dummyTextBlock.Arrange(new Rect(0, 0, 0, 0));
                return new Size(dummyTextBlock.ActualWidth, dummyTextBlock.ActualHeight);
                //return dummyTextBlock.DesiredSize;
                //return new Size(dummyTextBlock.DesiredSize.Width, dummyTextBlock.DesiredSize.Height);
            }
            else
            {
                return new Size(0, 0);
            }
        }
        private void AvoidLabelEditMode(INodeVM node)
        {
            foreach (LabelVM _mAnnotation in node.Annotations as List<IAnnotation>)
            {
                _mAnnotation.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "Mode")
                    {
                        if ((s as LabelVM).Mode == ContentEditorMode.Edit)
                            (s as LabelVM).Mode = ContentEditorMode.View;
                    }
                };
            }
        }
        private readonly char[] _mCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private SolidColorBrush GetColorFromHexa(string hexaColor)
        {
            return new SolidColorBrush(
                Color.FromArgb(
                    255,
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16)
                )
            );
        }
        #endregion

        void DiagramVM_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if (args.ItemSource == ItemSource.Stencil)
            {
                var dropedItem = args.Item as INode;
                if (dropedItem != null)
                {
                    if (double.IsNaN(dropedItem.UnitHeight))
                        dropedItem.UnitHeight = PageSettings.Unit.ToUnit(100);
                    if (double.IsNaN(dropedItem.UnitWidth))
                        dropedItem.UnitWidth = PageSettings.Unit.ToUnit(100);

                    #region Mockup
                    dropedItem.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == "ContentTemplate")
                        {
                            #region columnchart
                            if (dropedItem.Shape.Equals("columnchart"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("columnchart"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "columnchart";
                                    dropedItem.ContentTemplate = resourceDictionary["columnchart"] as DataTemplate;
                                    dropedItem.UnitWidth = 200;
                                    dropedItem.UnitHeight = 165;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region piechart
                            else if (dropedItem.Shape.Equals("piechart"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("piechart"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "piechart";
                                    dropedItem.ContentTemplate = resourceDictionary["piechart"] as DataTemplate;
                                    dropedItem.UnitWidth = 200;
                                    dropedItem.UnitHeight = 200;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    dropedItem.Constraints = dropedItem.Constraints | NodeConstraints.AspectRatio;
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region linechart
                            else if (dropedItem.Shape.Equals("linechart"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("linechart"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "linechart";
                                    dropedItem.ContentTemplate = resourceDictionary["linechart"] as DataTemplate;
                                    dropedItem.UnitWidth = 200;
                                    dropedItem.UnitHeight = 165;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region barchart
                            else if (dropedItem.Shape.Equals("barchart"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("barchart"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "barchart";
                                    dropedItem.ContentTemplate = resourceDictionary["barchart"] as DataTemplate;
                                    dropedItem.UnitWidth = 200;
                                    dropedItem.UnitHeight = 165;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region bubblechart
                            else if (dropedItem.Shape.Equals("bubblechart"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("bubblechart"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "bubblechart";
                                    dropedItem.ContentTemplate = resourceDictionary["bubblechart"] as DataTemplate;
                                    dropedItem.UnitWidth = 200;
                                    dropedItem.UnitHeight = 165;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region horizontalscrollbar
                            else if (dropedItem.Shape.Equals("horizontalscrollbar"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("horizontalscrollbar"))
                                {
                                    dropedItem.Constraints = dropedItem.Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).ContentTemplateKey = "horizontalscrollbar";
                                    dropedItem.ContentTemplate = resourceDictionary["horizontalscrollbar"] as DataTemplate;
                                    dropedItem.UnitHeight = 25;
                                    dropedItem.UnitWidth = 200;
                                    (dropedItem as INodeVM).DataContext = new HorizontalScrollBarBusinessClass() { SliderValue = 30};
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { ScrollSlider = true }, LinkTab = false };
                                }
                            }
                            #endregion
                            #region verticalscrollbar
                            else if (dropedItem.Shape.Equals("verticalscrollbar"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("verticalscrollbar"))
                                {
                                    dropedItem.Constraints = dropedItem.Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeSouth | NodeConstraints.ResizeNorth;
                                    (dropedItem as NodeVM).ContentTemplateKey = "verticalscrollbar";
                                    dropedItem.ContentTemplate = resourceDictionary["verticalscrollbar"] as DataTemplate;
                                    dropedItem.UnitWidth = 25;
                                    dropedItem.UnitHeight = 200;
                                    (dropedItem as INodeVM).DataContext = new VerticalScrollBarBusinessClass() { SliderValue = 30 };
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { ScrollSlider = true }, LinkTab = false };
                                }
                            }
                            #endregion
                            #region videoPlayer
                            else if (dropedItem.Shape.Equals("videoPlayer"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("videoPlayer"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "videoPlayer";
                                    dropedItem.ContentTemplate = resourceDictionary["videoPlayer"] as DataTemplate;
                                    dropedItem.UnitWidth = 300;
                                    dropedItem.UnitHeight = 200;
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region streetmap
                            else if (dropedItem.Shape.Equals("streetmap"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("streetmap"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "streetmap";
                                    dropedItem.ContentTemplate = resourceDictionary["streetmap"] as DataTemplate;
                                    dropedItem.UnitWidth = 250;
                                    dropedItem.UnitHeight = 150;
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region ioskeyboard
                            else if (dropedItem.Shape.Equals("ioskeyboard"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as INodeVM).DataContext = new IOSKeyboardBusinessClass();
                                    dropedItem.ContentTemplate = ((dropedItem as INodeVM).DataContext as IOSKeyboardBusinessClass).Template;
                                    dropedItem.UnitWidth = 380;
                                    dropedItem.UnitHeight = 200;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = new SettingTab() { ioskeyboard = true },
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region ipad
                            else if (dropedItem.Shape.Equals("ipad"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as INodeVM).DataContext = new IPADBusinessClass();
                                    dropedItem.ContentTemplate = ((dropedItem as INodeVM).DataContext as IPADBusinessClass).Template;
                                    dropedItem.UnitWidth = 320;
                                    dropedItem.UnitHeight = 420;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    AnnotationDataContextPropertyChanged(dropedItem as INodeVM);
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = new SettingTab() { ipad = true },
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region iphone
                            else if (dropedItem.Shape.Equals("iphone"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as INodeVM).DataContext = new IPhoneBusinessClass();
                                    dropedItem.ContentTemplate = ((dropedItem as INodeVM).DataContext as IPhoneBusinessClass).Template;
                                    dropedItem.UnitWidth = 150;
                                    dropedItem.UnitHeight = 300;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    AnnotationDataContextPropertyChanged(dropedItem as INodeVM);
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = new SettingTab() { IPhone = true },
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region captcha
                            else if (dropedItem.Shape.Equals("captcha"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("captcha"))
                                {
                                    dropedItem.UnitWidth = 180;
                                    (dropedItem as NodeVM).ContentTemplateKey = "captcha";
                                    dropedItem.ContentTemplate = resourceDictionary["captcha"] as DataTemplate;
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = null,
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region verticalslider
                            else if (dropedItem.Shape.Equals("verticalslider"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("verticalslider"))
                                {
                                    dropedItem.Constraints = dropedItem.Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeSouth | NodeConstraints.ResizeNorth;
                                    (dropedItem as NodeVM).ContentTemplateKey = "verticalslider";
                                    dropedItem.ContentTemplate = resourceDictionary["verticalslider"] as DataTemplate;
                                    dropedItem.UnitHeight = 200;
                                    dropedItem.UnitWidth = 28;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    (dropedItem as INodeVM).DataContext = new SliderBusinessClass() { SliderValue = 45.12 };
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = new SettingTab() { Stroke = true, StrokeThickness = true, ScrollSlider = true, Collection = true },
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region horizotalslider
                            else if (dropedItem.Shape.Equals("horizotalslider"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("horizotalslider"))
                                {
                                    dropedItem.Constraints = dropedItem.Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).ContentTemplateKey = "horizotalslider";
                                    dropedItem.ContentTemplate = resourceDictionary["horizotalslider"] as DataTemplate;
                                    dropedItem.UnitWidth = 200;
                                    dropedItem.UnitHeight = 28;
                                    (dropedItem as INodeVM).DataContext = new SliderBusinessClass() { SliderValue = 45.12 };
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = new SettingTab() { Stroke = true, StrokeThickness = true, ScrollSlider = true, Collection = true },
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region helpbutton
                            else if (dropedItem.Shape.Equals("helpbutton"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("helpbutton"))
                                {
                                    dropedItem.Constraints = (dropedItem.Constraints | NodeConstraints.AspectRatio) & ~NodeConstraints.Resizable;
                                    (dropedItem as NodeVM).ContentTemplateKey = "helpbutton";
                                    dropedItem.ContentTemplate = resourceDictionary["helpbutton"] as DataTemplate;
                                    dropedItem.UnitHeight = 50;
                                    dropedItem.UnitWidth = 50;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    foreach (LabelVM _mAnnotation in dropedItem.Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.ViewTemplate = resourceDictionary["hyperlinkonlyviewtemplate"] as DataTemplate;
                                    }
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region volume
                            else if (dropedItem.Shape.Equals("volume"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("volume"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "volume";
                                    dropedItem.ContentTemplate = resourceDictionary["volume"] as DataTemplate;
                                    dropedItem.UnitWidth = 50;
                                    dropedItem.UnitHeight = 50;
                                    dropedItem.Constraints = dropedItem.Constraints | NodeConstraints.AspectRatio;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = null,
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region menubar
                            else if (dropedItem.Shape.Equals("menubar"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    dropedItem.ContentTemplate = null;
                                    (dropedItem as INodeVM).DataContext = new MenuBarBusinessClass()
                                    {
                                        Items = new ObservableCollection<object>() 
                                        {
                                            new MenuTitleBusinessClass(){Item = "File",Selected = true},
                                            new MenuTitleBusinessClass(){Item = "Edit"},
                                            new MenuTitleBusinessClass(){Item = "View"},
                                            new MenuTitleBusinessClass(){Item = "Help"},
                                            new MenuTitleBusinessClass(){Item = "Tool"}
                                        }
                                    };
                                    dropedItem.UnitHeight = 40;
                                    dropedItem.UnitWidth = 200;
                                    (dropedItem as INodeVM).FontSize = 14;
                                    (dropedItem as INodeVM).LabelMargin = new Thickness(5);
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = new SettingTab() { Selection = true, ShowBorder = true },
                                        LinkTab = true
                                    };

                                    foreach (LabelVM _mAnnotation in dropedItem.Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["menubarviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as INodeVM).DataContext;
                                        string label = null;
                                        foreach (var str in (_mAnnotation.DataContext as MenuBarBusinessClass).Items)
                                        {
                                            if (label == null)
                                            {
                                                label = (str as MenuTitleBusinessClass).Item;
                                            }
                                            else
                                            {
                                                label = label.ToString() + "," + (str as MenuTitleBusinessClass).Item;
                                            }
                                        }
                                        _mAnnotation.Content = label;
                                        (dropedItem as INodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as NodeVM);
                                    }
                                }
                            }
                            #endregion
                            #region menu
                            else if (dropedItem.Shape.Equals("menu"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    dropedItem.Constraints = dropedItem.Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    dropedItem.ContentTemplate = null;
                                    dropedItem.UnitWidth = 240;
                                    (dropedItem as INodeVM).FontSize = 14;
                                    (dropedItem as INodeVM).LabelMargin = new Thickness(0, 5, 5, 5);
                                    (dropedItem as INodeVM).DataContext = new MenuBusinessClass()
                                    {
                                        Items = new ObservableCollection<object>()
                                        {
                                            new MenuItemBusinessClass(){Item = "Open", ShortcutKey = "Ctrl + O"},
                                            new MenuItemBusinessClass(){Item = "x Open Recent"},
                                            new MenuItemBusinessClass(){Item = "----"},
                                            new MenuItemBusinessClass(){Item = "o Option One"},
                                            new MenuItemBusinessClass(){Item = "Option Two"},
                                            new MenuItemBusinessClass(){Item = "----&&"},
                                            new MenuItemBusinessClass(){Item = "x Toggle Item"},
                                            new MenuItemBusinessClass(){Item = "-Disabled Item"},
                                            new MenuItemBusinessClass(){Item = "Exit", ShortcutKey = "Ctrl + Q" }
                                        }
                                    };
                                    foreach (LabelVM _mAnnotation in dropedItem.Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["menuviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as INodeVM).DataContext;
                                        string label = null;
                                        foreach (var str in (_mAnnotation.DataContext as MenuBusinessClass).Items)
                                        {
                                            if (label == null)
                                            {
                                                label = (str as MenuItemBusinessClass).EditModeItem;// +"," + (str as MenuItemBusinessClass).ShortcutKey;
                                                if (!string.IsNullOrEmpty((str as MenuItemBusinessClass).ShortcutKey))
                                                {
                                                    label = label + "," + (str as MenuItemBusinessClass).ShortcutKey;
                                                }
                                            }
                                            else
                                            {
                                                label = label + "\n" + (str as MenuItemBusinessClass).EditModeItem;// +"," + (str as MenuItemBusinessClass).ShortcutKey;
                                                if (!string.IsNullOrEmpty((str as MenuItemBusinessClass).ShortcutKey))
                                                {
                                                    label = label + "," + (str as MenuItemBusinessClass).ShortcutKey;
                                                }
                                            }


                                            int indexOf = (str as MenuItemBusinessClass).Item.IndexOfAny(_mCharacters);
                                            if (indexOf == -1)
                                            {
                                                (str as MenuItemBusinessClass).BreakLine = Visibility.Visible;
                                            }
                                        }
                                        _mAnnotation.Content = label;
                                        (dropedItem as INodeVM).Label = _mAnnotation.Content.ToString();

                                        Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                                        (dropedItem as INodeVM).UnitHeight = (_mAnnotation.DataContext as MenuBusinessClass).Items.Count() * (textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom);
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }

                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { TextAlignment = false , Foreground = false},
                                        SettingTab = new SettingTab() { Selection = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region playbackcontrol
                            else if (dropedItem.Shape.Equals("playbackcontrol"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "playbackcontrol";
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable;
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["playbackcontrol"] as DataTemplate;
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                    (dropedItem as NodeVM).UnitHeight = 45;
                                    (dropedItem as NodeVM).UnitWidth = 150;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                }
                            }
                            #endregion
                            #region webcam
                            else if (dropedItem.Shape.Equals("webcam"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "webcam";
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["webcam"] as DataTemplate;
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints | NodeConstraints.AspectRatio;
                                }
                            }
                            #endregion
                            #region tabsbar
                            else if (dropedItem.Shape.Equals("tabsbar"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    dropedItem.ContentTemplate = null;
                                    (dropedItem as NodeVM).UnitWidth = 325;
                                    (dropedItem as NodeVM).UnitHeight = 250;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelMargin = new Thickness(10, 5, 10, 5);
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.White);
                                    (dropedItem as NodeVM).DataContext = new TabBusinessClass(Orientation.Horizontal) { Top = true };
                                    ((dropedItem as NodeVM).DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab1", Selected = true });
                                    ((dropedItem as NodeVM).DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab2", Selected = false });
                                    ((dropedItem as NodeVM).DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab4", Selected = false });
                                    ((dropedItem as NodeVM).DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab3", Selected = false, IsLastItem = true });

                                    foreach (LabelVM _mAnnotation in dropedItem.Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                                    {
                                        _mAnnotation.UnitWidth = 100;
                                        _mAnnotation.UnitHeight = 100;
                                        _mAnnotation.Offset = new Point(0, 0);
                                        _mAnnotation.TextHorizontalAlignment = TextAlignment.Left;
                                        _mAnnotation.TextVerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                        _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["tabbarTopviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        foreach (var str in ((_mAnnotation.DataContext as TabBusinessClass).Items as ObservableCollection<object>))
                                        {
                                            if (_mAnnotation.Content == null)
                                            {
                                                _mAnnotation.Content = (str as TabItemBusinessClass).Item;
                                            }
                                            else
                                            {
                                                _mAnnotation.Content = _mAnnotation.Content.ToString() + "," + (str as TabItemBusinessClass).Item;
                                            }
                                        }
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as NodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { TextAlignment = false },
                                        SettingTab = new SettingTab() { Fill = true, Opacity = true, Selection = true, HorizontalTab = true, VerticalScrollBar = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region verticaltab
                            else if (dropedItem.Shape.Equals("verticaltab"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    dropedItem.ContentTemplate = null;
                                    dropedItem.UnitWidth = 250;
                                    dropedItem.UnitHeight = 250;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelMargin = new Thickness(10, 5, 10, 5);
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.White);
                                    (dropedItem as NodeVM).DataContext = new TabBusinessClass(Orientation.Vertical) { Left = true };
                                    ((dropedItem as NodeVM).DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab1", Selected = true });
                                    ((dropedItem as NodeVM).DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab2", Selected = false });
                                    ((dropedItem as NodeVM).DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab3", Selected = false, IsLastItem = true });
                                    foreach (LabelVM _mAnnotation in dropedItem.Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                                    {
                                        _mAnnotation.UnitWidth = 100;
                                        _mAnnotation.UnitHeight = 100;
                                        _mAnnotation.Offset = new Point(0, 0);
                                        _mAnnotation.TextHorizontalAlignment = TextAlignment.Left;
                                        _mAnnotation.TextVerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                        _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["verticaltabLeftviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        foreach (var str in ((_mAnnotation.DataContext as TabBusinessClass).Items as ObservableCollection<object>))
                                        {
                                            if (_mAnnotation.Content == null)
                                            {
                                                _mAnnotation.Content = (str as TabItemBusinessClass).Item;
                                            }
                                            else
                                            {
                                                _mAnnotation.Content = _mAnnotation.Content.ToString() + "," + (str as TabItemBusinessClass).Item;
                                            }
                                        }
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as NodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { TextAlignment = false },
                                        SettingTab = new SettingTab() { Fill = true, Opacity = true, Selection = true, VerticalTab = true, VerticalScrollBar = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region buttonbar
                            else if (dropedItem.Shape.Equals("buttonbar"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    dropedItem.ContentTemplate = null;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelMargin = new Thickness(10);
                                    (dropedItem as NodeVM).DataContext = new ButtonBarBusinessClass()
                                    {
                                        Items = new ObservableCollection<object>()
                                        {
                                            new ButtonBarItemBusinessClass(){Item = "Tab1", Selected = true},
                                            new ButtonBarItemBusinessClass(){Item = "Tab2", Selected = false},
                                            new ButtonBarItemBusinessClass(){Item = "Tab3", Selected = false}
                                        }
                                    };
                                    foreach (LabelVM _mAnnotation in dropedItem.Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                                    {
                                        _mAnnotation.UnitWidth = 100;
                                        _mAnnotation.UnitHeight = 100;
                                        _mAnnotation.TextHorizontalAlignment = TextAlignment.Left;
                                        _mAnnotation.TextVerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                        _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.Offset = new Point(0, 0);
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["buttonbarviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        Size buttonbarsize = new Size();
                                        foreach (var str in ((_mAnnotation.DataContext as ButtonBarBusinessClass).Items as ObservableCollection<object>))
                                        {

                                            if (_mAnnotation.Content == null)
                                            {
                                                _mAnnotation.Content = (str as ButtonBarItemBusinessClass).Item;
                                                Size textsize = FindDummyTextSize(_mAnnotation, (str as ButtonBarItemBusinessClass).Item, TextWrapping.NoWrap);
                                                (str as ButtonBarItemBusinessClass).Width = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                                buttonbarsize.Width = buttonbarsize.Width + (str as ButtonBarItemBusinessClass).Width;
                                                buttonbarsize.Height = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                            }
                                            else
                                            {
                                                _mAnnotation.Content = _mAnnotation.Content.ToString() + "," + (str as ButtonBarItemBusinessClass).Item;
                                                Size textsize = FindDummyTextSize(_mAnnotation, (str as ButtonBarItemBusinessClass).Item, TextWrapping.NoWrap);
                                                (str as ButtonBarItemBusinessClass).Width = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                                buttonbarsize.Width = buttonbarsize.Width + (str as ButtonBarItemBusinessClass).Width;
                                                buttonbarsize.Height = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                            }
                                        }
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();

                                        dropedItem.UnitWidth = buttonbarsize.Width;
                                        dropedItem.UnitHeight = buttonbarsize.Height;
                                    }
                                    AnnotationPropertyChanged(dropedItem as NodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { TextAlignment = false },
                                        SettingTab = new SettingTab() { Selection = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region searchbutton
                            else if (dropedItem.Shape.Equals("searchbutton"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    dropedItem.Constraints = dropedItem.Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    dropedItem.ContentTemplate = resourceDictionary["searchbutton"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new SearchBoxBusinessClass();
                                    (dropedItem as NodeVM).Stroke = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    dropedItem.UnitWidth = 160;
                                    dropedItem.UnitHeight = 30;
                                    (dropedItem as NodeVM).LabelMargin = new Thickness(10, 5, 12, 5);
                                    (dropedItem as NodeVM).LabelHAlign = HorizontalAlignment.Left;
                                    foreach (LabelVM _mAnnotation in dropedItem.Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.Offset = new Point(0, 0.5);
                                        _mAnnotation.LabelWidth = dropedItem.UnitWidth;
                                        _mAnnotation.LabelHeight = dropedItem.UnitHeight;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.ViewTemplate = resourceDictionary["searchbuttonviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplateWithLeftTopAlignment"] as DataTemplate;
                                        _mAnnotation.Content = "Search";
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        (_mAnnotation.DataContext as SearchBoxBusinessClass).CornerRadius = new CornerRadius(dropedItem.UnitHeight / 2);
                                        AnnotationPropertyChanged(dropedItem as NodeVM);
                                    }

                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab(),
                                        SettingTab = new SettingTab() { Collection = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region progressbar
                            else if (dropedItem.Shape.Equals("progressbar"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("progressbar"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "progressbar";
                                    dropedItem.ContentTemplate = resourceDictionary["progressbar"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new ProgressBarBusinessClass();
                                    dropedItem.UnitHeight = 20;
                                    dropedItem.UnitWidth = 200;
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { ScrollSlider = true }, LinkTab = false };
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                }
                            }
                            #endregion
                            #region volumeslider
                            else if (dropedItem.Shape.Equals("volumeslider"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("volumeslider"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "volumeslider";
                                    dropedItem.Constraints = dropedItem.Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    dropedItem.ContentTemplate = resourceDictionary["volumeslider"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new VolumeSliderBusinessClass();
                                    dropedItem.UnitWidth = 200;
                                    dropedItem.UnitHeight = 45;
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { ScrollSlider = true }, LinkTab = false };                                    
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                }
                            }
                            #endregion
                            #region browserWindow
                            else if (dropedItem.Shape.Equals("browserWindow"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("browserWindow"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "browserWindow";
                                    dropedItem.ContentTemplate = resourceDictionary["browserWindow"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new BrowserWindowBusinessClass()
                                    {
                                        WebsiteTitle = "A Web Page",
                                        WebsiteURL = "http:\\",
                                        ScrollBar = new VerticalScrollBarBusinessClass()
                                    };
                                    dropedItem.UnitHeight = 300;
                                    dropedItem.UnitWidth = 400;
                                    (dropedItem as INodeVM).Fill = new SolidColorBrush(Colors.White);
                                    (dropedItem as INodeVM).LabelMargin = new Thickness(65, 0, 20, 0);
                                    foreach (LabelVM _mAnnotation in dropedItem.Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.PropertyChanged += (ann, evtArgs) =>
                                        {
                                            if (evtArgs.PropertyName.Equals("Mode"))
                                            {
                                                if ((ann as LabelVM).Mode == ContentEditorMode.Edit)
                                                {
                                                    (ann as LabelVM).UnitHeight = double.NaN;
                                                    (ann as LabelVM).UnitWidth = double.NaN;
                                                }
                                                else
                                                {
                                                    (ann as LabelVM).UnitHeight = 100;
                                                    (ann as LabelVM).UnitWidth = 100;
                                                }
                                            }
                                        };
                                        _mAnnotation.UnitHeight = 100;
                                        _mAnnotation.UnitWidth = 100;
                                        _mAnnotation.Offset = new Point(0, 0);
                                        _mAnnotation.TextHorizontalAlignment = TextAlignment.Left;
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                        _mAnnotation.TextVerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["browserwindowviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.LabelWidth = dropedItem.UnitWidth - _mAnnotation.LabelMargin.Right;
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                        _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.Content = (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteTitle + "\t\n" + (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteURL;
                                        (dropedItem as INodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as NodeVM);
                                    }

                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = new SettingTab() { Fill = true, VerticalScrollBar = true },
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region checkbox / radiobutton
                            else if (dropedItem.Shape.Equals("checkbox") || dropedItem.Shape.Equals("radiobutton"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("checkbox"))
                                {
                                    if (dropedItem.Shape.Equals("checkbox"))
                                        (dropedItem as NodeVM).ContentTemplateKey = "checkbox";
                                    else if (dropedItem.Shape.Equals("radiobutton"))
                                        (dropedItem as NodeVM).ContentTemplateKey = "radiobutton";

                                    (dropedItem as NodeVM).Constraints = ((dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable) | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["checkbox"] as DataTemplate;
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    
                                    (dropedItem as NodeVM).DataContext = new CheckBoxBusinessClass();
                                    (dropedItem as NodeVM).UnitWidth = 150;
                                    (dropedItem as NodeVM).UnitHeight = 40;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.White);
                                    (dropedItem as NodeVM).Stroke = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).Thickness = 1;

                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                                    {
                                        _mAnnotation.UnitHeight = 100;
                                        _mAnnotation.UnitWidth = 100;
                                        _mAnnotation.Offset = new Point(0, 0.5);
                                        _mAnnotation.TextHorizontalAlignment = TextAlignment.Left;
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        Size textsize;
                                        _mAnnotation.LabelMargin = new Thickness(10, 0, 10, 0);
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        if (dropedItem.Shape.Equals("checkbox"))
                                        {                                            
                                            _mAnnotation.Content = "CheckBox";
                                            _mAnnotation.ViewTemplate = resourceDictionary["selectionboxviewtemplate"] as DataTemplate;
                                        }
                                        else if (dropedItem.Shape.Equals("radiobutton"))
                                        {
                                            _mAnnotation.Content = "RadioButton";
                                            _mAnnotation.ViewTemplate = resourceDictionary["radiobuttonviewtemplate"] as DataTemplate;
                                        }
                                        textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                                        dropedItem.UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                        dropedItem.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as NodeVM);
                                    }

                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { TextAlignment = false },
                                        SettingTab = new SettingTab() { Collection = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region datechooser
                            else if (dropedItem.Shape.Equals("datechooser"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).ContentTemplate = null;
                                    (dropedItem as NodeVM).DataContext = new DateChoosetBusinessClass();
                                    (dropedItem as NodeVM).UnitWidth = 150;
                                    (dropedItem as NodeVM).UnitHeight = 40;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.White);
                                    (dropedItem as NodeVM).Stroke = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).Thickness = 1;
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.LabelMargin = new Thickness(10, 5, 10, 5);
                                        _mAnnotation.ViewTemplate = resourceDictionary["datechooserviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.Content = " 01 / 01 / 2015";
                                        Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                                        (dropedItem as NodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(args.Item as NodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab(),
                                        SettingTab = new SettingTab() { Stroke = true, Collection = true },
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region windowbox
                            else if (dropedItem.Shape.Equals("windowbox"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("windowbox"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "windowbox";
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["windowbox"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new WindowBoxBusinessObject()
                                    {
                                        TopMargin = new Thickness(0, 0, 0, 0),
                                        BottomMargin = new Thickness(0, 0, 0, 0),
                                        ScrollBar = new VerticalScrollBarBusinessClass()
                                    };
                                    dropedItem.UnitWidth = 400;
                                    dropedItem.UnitHeight = 300;
                                    (dropedItem as NodeVM).LabelHAlign = HorizontalAlignment.Left;
                                    (dropedItem as NodeVM).LabelVAlign = VerticalAlignment.Top;
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.PropertyChanged += (ann, evtArgs) =>
                                        {
                                            if(evtArgs.PropertyName.Equals("Mode"))
                                            {
                                                if((ann as LabelVM).Mode == ContentEditorMode.Edit)
                                                {
                                                    (ann as LabelVM).UnitHeight = double.NaN;
                                                    (ann as LabelVM).UnitWidth = double.NaN;
                                                }
                                                else
                                                {
                                                    (ann as LabelVM).UnitHeight = 100;
                                                    (ann as LabelVM).UnitWidth = 100;
                                                }
                                            }                                            
                                        };
                                        _mAnnotation.UnitHeight = 100;
                                        _mAnnotation.UnitWidth = 100;
                                        _mAnnotation.Offset = new Point(0, 0);
                                        _mAnnotation.TextHorizontalAlignment = TextAlignment.Left;
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                        _mAnnotation.TextVerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.FontSize = 14;
                                        _mAnnotation.ViewTemplate = resourceDictionary["defaultviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.LabelHeight = (_mAnnotation.DataContext as WindowBoxBusinessObject).TitleBarHeight = _mAnnotation.FontSize + (_mAnnotation.DataContext as WindowBoxBusinessObject).TopIconHeight;
                                        //_mAnnotation.LabelHeight = (_mAnnotation.DataContext as WindowBoxBusinessObject).TitleBarHeight;
                                        _mAnnotation.LabelTextWrapping = TextWrapping.NoWrap;
                                        _mAnnotation.LabelForeground = new SolidColorBrush(Colors.Black);
                                        _mAnnotation.Content = "Window Name";
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as NodeVM);
                                        
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false }, SettingTab = new SettingTab() { WindowBox = true, VerticalScrollBar = true }, LinkTab = false };
                                }
                            }
                            #endregion
                            #region onoffswitch
                            else if (dropedItem.Shape.Equals("onoffswitch"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("onoffswitch"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "onoffswitch";
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable;
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["onoffswitch"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new OnOffSwitch();
                                    dropedItem.UnitWidth = 65;
                                    dropedItem.UnitHeight = 33;
                                    (dropedItem as NodeVM).Fill = GetColorFromHexa("#83c1f0");
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["onoffswitchviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = null;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.Content = "On";
                                        _mAnnotation.LabelMargin = new Thickness(0, 0, 10, 0);
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Right;
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AvoidLabelEditMode(dropedItem as INodeVM);
                                        AnnotationDataContextPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = new SettingTab() { Fill = true, OnOffSwitch = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region button
                            else if (dropedItem.Shape.Equals("button"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("button"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "button";
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["button"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new ButtonBusinessObject();
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).Fill = GetColorFromHexa("#E2E2E2");
                                    (dropedItem as NodeVM).Stroke = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.UnitHeight = 100;
                                        _mAnnotation.UnitWidth = 100;
                                        _mAnnotation.Offset = new Point(0.5, 0.5);
                                        _mAnnotation.TextHorizontalAlignment = TextAlignment.Center;
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Center;
                                        _mAnnotation.TextVerticalAlignment = VerticalAlignment.Center;
                                        _mAnnotation.VerticalAlignment = VerticalAlignment.Center;
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.LabelMargin = new Thickness(15, 5, 15, 5);
                                        _mAnnotation.ViewTemplate = resourceDictionary["defaultviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        //(dropedItem as NodeVM).Annotations = new List<IAnnotation>() { _mAnnotation };
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.Content = "Button";
                                        Size textsize;
                                        textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                                        (dropedItem as NodeVM).UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                        (dropedItem as NodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab(),
                                        SettingTab = new SettingTab() { Fill = true, Opacity = true, Collection = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region numericstepper
                            else if (dropedItem.Shape.Equals("numericstepper"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).ContentTemplate = null;
                                    (dropedItem as NodeVM).DataContext = new NumericStepperBusinessClass();
                                    //(dropedItem as NodeVM).UnitWidth = 60;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelMargin = new Thickness(10, 5, 10, 5);
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).Stroke = new SolidColorBrush(Colors.Black);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["numericstepperviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.Content = "2";
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                                        (dropedItem as NodeVM).UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                        (dropedItem as NodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { TextAlignment = false },
                                        SettingTab = new SettingTab() { Stroke = true, Collection = true },
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region multilinebutton
                            else if (dropedItem.Shape.Equals("multilinebutton"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("multilinebutton"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "multilinebutton";
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["multilinebutton"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new MultiLineButtonBusinessObject() { Title = "Multiline Button", Message = "Second line of text" };
                                    (dropedItem as NodeVM).UnitHeight = 60;
                                    (dropedItem as NodeVM).UnitWidth = 160;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.White);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["multilinebuttonviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.TextAlignment = TextAlignment.Center;
                                        _mAnnotation.LabelForeground = new SolidColorBrush(Colors.Black);
                                        //Annotations = new List<IAnnotation>() { _mAnnotation };
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.Content = (_mAnnotation.DataContext as MultiLineButtonBusinessObject).Title + "\t\n" + (_mAnnotation.DataContext as MultiLineButtonBusinessObject).Message;
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        _mAnnotation.LabelWidth = (dropedItem as NodeVM).UnitWidth;
                                        _mAnnotation.LabelHeight = (dropedItem as NodeVM).UnitHeight;
                                        (_mAnnotation.DataContext as MultiLineButtonBusinessObject).TitleHeight = FindDummyTextSize(_mAnnotation, (_mAnnotation.DataContext as MultiLineButtonBusinessObject).Title).Height + (_mAnnotation.DataContext as MultiLineButtonBusinessObject).TitleMargin.Top + (_mAnnotation.DataContext as MultiLineButtonBusinessObject).TitleMargin.Bottom;
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { TextAlignment = false },
                                        SettingTab = new SettingTab() { Fill = true },
                                        LinkTab = true
                                    };
                                    
                                }
                            }
                            #endregion
                            #region messagebox
                            else if (dropedItem.Shape.Equals("messagebox"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as NodeVM).ContentTemplate = null;
                                    (dropedItem as NodeVM).DataContext = new MessageBoxBusinessObject()
                                    {
                                        Title = "Title",
                                        Message = "Message",
                                        Ok = "Ok"
                                    };
                                    (dropedItem as NodeVM).UnitHeight = 140;
                                    (dropedItem as NodeVM).UnitWidth = 220;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.White);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["messageboxviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.Content = (_mAnnotation.DataContext as MessageBoxBusinessObject).Input;
                                        (dropedItem as NodeVM).Label = (_mAnnotation.DataContext as MessageBoxBusinessObject).Title + "\t\n" + (_mAnnotation.DataContext as MessageBoxBusinessObject).Message + "\t\n" + (_mAnnotation.DataContext as MessageBoxBusinessObject).Ok;
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = null,
                                        LinkTab = true
                                    };                                    
                                }
                            }
                            #endregion
                            #region tooltip
                            else if (dropedItem.Shape.Equals("tooltip"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).DataContext = new ToolTipBusinessClass() { HorizontalAlignment = HorizontalAlignment.Center };
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as NodeVM).ContentTemplate = ((dropedItem as NodeVM).DataContext as ToolTipBusinessClass).Template;
                                    (dropedItem as NodeVM).UnitHeight = 50;
                                    (dropedItem as NodeVM).UnitWidth = 200;
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.ViewTemplate = resourceDictionary["tooltipviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.LabelWidth = (dropedItem as NodeVM).UnitWidth;
                                        _mAnnotation.LabelHeight = (dropedItem as NodeVM).UnitHeight;
                                        _mAnnotation.LabelMargin = new Thickness(10, 10, 10, 0);
                                        _mAnnotation.Content = "a tooltip";
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab(),
                                        SettingTab = new SettingTab() { ToolTip = true },
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region alertbox
                            else if (dropedItem.Shape.Equals("alertbox"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("alertbox"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "alertbox";
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["alertbox"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new DialogBoxBusinessObject()
                                    {
                                        Title = "Alert",
                                        Message = "Message",
                                        Cancel = "No",
                                        Ok = "Yes"
                                    };

                                    (dropedItem as NodeVM).UnitHeight = 140;
                                    (dropedItem as NodeVM).UnitWidth = 220;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.White);

                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                                    {
                                        if (!(_mAnnotation is HyperLinkVM))
                                        {
                                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                                            _mAnnotation.ViewTemplate = resourceDictionary["alertboxviewtemplate"] as DataTemplate;
                                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                            _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                            _mAnnotation.Content = (_mAnnotation.DataContext as DialogBoxBusinessObject).Input;
                                            (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                            AnnotationPropertyChanged(dropedItem as INodeVM);
                                        }
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = null,
                                        LinkTab = true
                                    };
                                    
                                }
                            }
                            #endregion                                
                            #region calendar
                            else if (dropedItem.Shape.Equals("calendar"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("calendar"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "calendar";
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["calendar"] as DataTemplate;
                                    dropedItem.UnitWidth = 180;
                                    dropedItem.UnitHeight = 180;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region combobox
                            else if (dropedItem.Shape.Equals("combobox"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("combobox"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "combobox";
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    (dropedItem as NodeVM).ContentTemplate = null;// resourceDictionary["combobox"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new ComboBoxBusinessClass();
                                    (dropedItem as NodeVM).UnitWidth = 120;
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).Stroke = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelMargin = new Thickness(10, 5, 10, 5);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                                    {
                                        _mAnnotation.UnitHeight = 100;
                                        _mAnnotation.UnitWidth = 100;
                                        _mAnnotation.Offset = new Point(0, 0);
                                        _mAnnotation.TextHorizontalAlignment = TextAlignment.Left;
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                        _mAnnotation.TextVerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.ViewTemplate = resourceDictionary["comboboxviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.Content = (_mAnnotation.DataContext as ComboBoxBusinessClass).Title;
                                        Size textsize = FindDummyTextSize(_mAnnotation, (_mAnnotation.DataContext as ComboBoxBusinessClass).Title, TextWrapping.NoWrap);
                                        (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                        (dropedItem as NodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                        AnnotationDataContextPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false }, SettingTab = new SettingTab() { Stroke = true, Collection = true }, LinkTab = true };
                                }
                            }
                            #endregion
                            #region scratchout
                            else if (dropedItem.Shape.Equals("scratchout"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("scratchout"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "scratchout";
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["scratchout"] as DataTemplate;
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.Black);
                                    AvoidLabelEditMode(dropedItem as NodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { Fill = true, Opacity = true }, LinkTab = false };
                                }
                            }
                            #endregion
                            #region horizontalcurlybraces
                            else if (dropedItem.Shape.Equals("horizontalcurlybraces"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey!= null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as NodeVM).ContentTemplate = null;
                                    (dropedItem as NodeVM).DataContext = new HorizontalCurlyBracesBusinessClass();
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                         _mAnnotation.Offset = new Point(0.5, 0);
                                        _mAnnotation.ViewTemplate = resourceDictionary["horizontalcurlybracessviewtemplate"] as DataTemplate;
                                        _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.Content = "A Pararaph of Text";
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        (dropedItem as NodeVM).UnitWidth = 150;
                                        (dropedItem as NodeVM).UnitHeight = 100;
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = new SettingTab() { HorizontalCurlyBraces = true }, LinkTab = false };
                                }
                            }
                            #endregion
                            #region verticalcurlybraces
                            else if (dropedItem.Shape.Equals("verticalcurlybraces"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as NodeVM).ContentTemplate = null;
                                    (dropedItem as NodeVM).DataContext = new VerticalCurlyBracesBusinessClass();
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.Offset = new Point(0, 0.5);
                                        _mAnnotation.ViewTemplate = resourceDictionary["verticalcurlybracessviewtemplate"] as DataTemplate;
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        (dropedItem as NodeVM).UnitWidth = 100;
                                        (dropedItem as NodeVM).UnitHeight = 150;
                                        _mAnnotation.Content = "A Pararaph of Text";
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = new SettingTab() { VerticalCurlyBraces = true }, LinkTab = false };
                                }
                            }
                            #endregion
                            #region stickynote
                            else if (dropedItem.Shape.Equals("stickynote"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("stickynote"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "stickynote";
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["stickynote"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new StickyNoteBusinessClass() { HorizontalAlignment = HorizontalAlignment.Center };
                                    (dropedItem as NodeVM).UnitWidth = 150;
                                    (dropedItem as NodeVM).UnitHeight = 200;
                                    (dropedItem as NodeVM).Fill = GetColorFromHexa("#ffdf1b");
                                    (dropedItem as NodeVM).LabelMargin = new Thickness(10, 25, 10, 10);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["stickynoteviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                        //_mAnnotation.LabelMargin = new Thickness(10, 25, 10, 10);
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        //Annotations = new List<IAnnotation>() { _mAnnotation };
                                        _mAnnotation.Content = "Sticky Notes";
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                        AnnotationDataContextPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = new SettingTab() { Fill = true }, LinkTab = false };
                                }
                            }
                            #endregion
                            #region subtitle
                            else if (dropedItem.Shape.Equals("subtitle"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("subtitle"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "subtitle";
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["subtitle"] as DataTemplate;
                                    (dropedItem as NodeVM).UnitHeight = 30;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).DataContext = new PropertyChange() { HorizontalAlignment = HorizontalAlignment.Center };
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["subtitleviewtemplate"] as DataTemplate;
                                        _mAnnotation.Content = "A Subtitle";
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.LabelTextWrapping = TextWrapping.NoWrap;
                                        Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                                        (dropedItem as NodeVM).UnitWidth = textsize.Width;
                                        (dropedItem as NodeVM).UnitHeight = textsize.Height;
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                        SelectorPropertyChanged();
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = null, LinkTab = true };
                                }
                            }
                            #endregion
                            #region checkboxgroup
                            else if (dropedItem.Shape.Equals("checkboxgroup"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("checkboxgroup"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "checkboxgroup";
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["checkboxgroup"] as DataTemplate;
                                    
                                    (dropedItem as NodeVM).DataContext = new CheckBoxGroupBusinessClass()
                                    {
                                        Items = new ObservableCollection<object>()
                                        {
                                            new CheckBoxItemBusinessClass(){Item = "[x] Selected"},
                                            new CheckBoxItemBusinessClass(){Item = "[] Not Selected"},
                                            new CheckBoxItemBusinessClass(){Item = "-[] Disabled"},
                                            new CheckBoxItemBusinessClass(){Item = "-[x] Disabled Selected"},
                                            new CheckBoxItemBusinessClass(){Item = "Without Checkbox"}
                                        }
                                    };
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).Stroke = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).UnitHeight = 100;
                                    (dropedItem as NodeVM).UnitWidth = 160;
                                    (dropedItem as NodeVM).LabelMargin = new Thickness(0, 5, 0, 5);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["checkboxgroupviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem  as NodeVM).DataContext;
                                        //_mAnnotation.LabelTextWrapping = TextWrapping.NoWrap;
                                        Size textsize = new Size();
                                        //_mAnnotation.LabelMargin = new Thickness(0, 5, 0, 5);
                                        foreach (var str in ((_mAnnotation.DataContext as CheckBoxGroupBusinessClass).Items as ObservableCollection<object>))
                                        {
                                            if (_mAnnotation.Content == null)
                                            {
                                                _mAnnotation.Content = (str as CheckBoxItemBusinessClass).Item;
                                                Size localsize = FindDummyTextSize(_mAnnotation, (str as CheckBoxItemBusinessClass).Item, TextWrapping.NoWrap);
                                                if (localsize.Width > textsize.Width)
                                                    textsize.Width = localsize.Width;
                                            }
                                            else
                                            {
                                                _mAnnotation.Content = _mAnnotation.Content.ToString() + "\n" + (str as CheckBoxItemBusinessClass).Item;
                                                Size localsize = FindDummyTextSize(_mAnnotation, (str as CheckBoxItemBusinessClass).Item, TextWrapping.NoWrap);
                                                if (localsize.Width > textsize.Width)
                                                    textsize.Width = localsize.Width;
                                            }
                                        }
                                        if ((dropedItem as NodeVM).Shape.Equals("checkboxgroup"))
                                        {
                                            (dropedItem as NodeVM).Label = "[x] Selected" + "\n" + "[] Not Selected" + "\n" + "-[] Disabled" + "\n" + "-[x] Disabled Selected" + "\n" + "Without Checkbox";
                                        }

                                        (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                        (dropedItem as NodeVM).UnitHeight = (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight * (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).Items.Count();
                                        // Added FontSize also becuase checkbox is in front 
                                        (dropedItem as NodeVM).UnitWidth = textsize.Width + _mAnnotation.FontSize;
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { TextAlignment = false },
                                        SettingTab = null,
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region radiobuttongroup
                            else if (dropedItem.Shape.Equals("radiobuttongroup"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("radiobuttongroup"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "radiobuttongroup";
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["radiobuttongroup"] as DataTemplate;                                    
                                    (dropedItem as NodeVM).DataContext = new RadioButtonGroupBusinessClass()
                                    {
                                        Items = new ObservableCollection<object>()
                                        {
                                            new RadioButtonItemBusinessClass(){Item = "(o) Selected"},
                                            new RadioButtonItemBusinessClass(){Item = "() Not Selected"},
                                            new RadioButtonItemBusinessClass(){Item = "-() Disabled"},
                                            new RadioButtonItemBusinessClass(){Item = "-(o) Disabled Selected"},
                                            new RadioButtonItemBusinessClass(){Item = "Without RadioButton"},
                                        }
                                    };

                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).Stroke = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).UnitHeight = 100;
                                    (dropedItem as NodeVM).UnitWidth = 160;
                                    (dropedItem as NodeVM).LabelMargin = new Thickness(0, 5, 0, 5);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["radiobuttongroupviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        Size textsize = new Size();
                                        //_mAnnotation.LabelMargin = new Thickness(0, 5, 0, 5);
                                        foreach (var str in ((_mAnnotation.DataContext as RadioButtonGroupBusinessClass).Items as ObservableCollection<object>))
                                        {
                                            if (_mAnnotation.Content == null)
                                            {
                                                _mAnnotation.Content = (str as RadioButtonItemBusinessClass).Item;
                                                Size localsize = FindDummyTextSize(_mAnnotation, (str as RadioButtonItemBusinessClass).Item, TextWrapping.NoWrap);
                                                if (localsize.Width > textsize.Width)
                                                    textsize.Width = localsize.Width;
                                            }
                                            else
                                            {
                                                _mAnnotation.Content = _mAnnotation.Content.ToString() + "\t\n" + (str as RadioButtonItemBusinessClass).Item;
                                                Size localsize = FindDummyTextSize(_mAnnotation, (str as RadioButtonItemBusinessClass).Item, TextWrapping.NoWrap);
                                                if (localsize.Width > textsize.Width)
                                                    textsize.Width = localsize.Width;
                                            }
                                        }
                                        (dropedItem as NodeVM).Label = "(o) Selected" + "\t\n" + "() Not Selected" + "\t\n" + "-() Disabled" + "\t\n" + "-(o) Disabled Selected" + "\t\n" + "Without RadioButton";
                                        (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                        (dropedItem as NodeVM).UnitHeight = (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).ItemHeight * (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).Items.Count();
                                        // Added FontSize also becuase checkbox is in front 
                                        (dropedItem as NodeVM).UnitWidth = textsize.Width + _mAnnotation.FontSize;
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { TextAlignment = false },
                                        SettingTab = null,
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region colorpicker
                            else if (dropedItem.Shape.Equals("colorpicker"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("colorpicker"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "colorpicker";
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable;
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["colorpicker"] as DataTemplate;
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.Blue);
                                    (dropedItem as NodeVM).UnitHeight = 50;
                                    (dropedItem as NodeVM).UnitWidth = 50;
                                    AvoidLabelEditMode(dropedItem as INodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = new SettingTab() { Fill = true, Stroke = true, StrokeThickness = true },
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion  
                            #region redx
                            else if (dropedItem.Shape.Equals("redx"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("redx"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "redx";
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["redx"] as DataTemplate;
                                    (dropedItem as NodeVM).UnitWidth = 180;
                                    (dropedItem as NodeVM).UnitHeight = 90;
                                    AvoidLabelEditMode(dropedItem as NodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region fieldset
                            else if (dropedItem.Shape.Equals("fieldset"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("fieldset"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "fieldset";
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["fieldset"] as DataTemplate;
                                    (dropedItem as NodeVM).UnitWidth = 235;
                                    (dropedItem as NodeVM).UnitHeight = 200;
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.White);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["fieldsetviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        //_mAnnotation.VerticalAlignment = VerticalAlignment.Top;
                                        (dropedItem as INodeVM).Label = "Group Name";
                                        _mAnnotation.Content = "Group Name";
                                        
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false }, SettingTab = new SettingTab() { Fill = true }, LinkTab = false };
                                }
                            }
                            #endregion
                            #region popover
                            else if (dropedItem.Shape.Equals("popover"))
                            {
                                //if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("popover"))
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).DataContext = new PopoverBusinessClass();
                                    (dropedItem as NodeVM).ContentTemplateKey = null;// ((dropedItem as NodeVM).DataContext as PopoverBusinessClass).Template.ToString();
                                    (dropedItem as NodeVM).ContentTemplate = ((dropedItem as NodeVM).DataContext as PopoverBusinessClass).Template;
                                    (dropedItem as NodeVM).UnitWidth = 150;
                                    (dropedItem as NodeVM).UnitHeight = 150;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).LabelHAlign = HorizontalAlignment.Left;
                                    (dropedItem as NodeVM).LabelVAlign = VerticalAlignment.Top;
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.Offset = new Point(0, 0);
                                        _mAnnotation.LabelTextWrapping = TextWrapping.Wrap;
                                        _mAnnotation.LabelMargin = new Thickness(5);
                                        _mAnnotation.ViewTemplate = resourceDictionary["defaultviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplateWithLeftTopAlignment"] as DataTemplate;

                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false }, SettingTab = new SettingTab() { Fill = true, Popover = true }, LinkTab = false };
                                }
                            }
                            #endregion
                            #region pointybutton
                            else if (dropedItem.Shape.Equals("pointybutton"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("pointybutton"))
                                {
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    (dropedItem as NodeVM).DataContext = new PointyButtonBusinessClass() { Right = true };
                                    (dropedItem as NodeVM).ContentTemplateKey = "pointybutton";// ((dropedItem as NodeVM).DataContext as PointyButtonBusinessClass).Template.ToString();
                                    (dropedItem as NodeVM).ContentTemplate = ((dropedItem as NodeVM).DataContext as PointyButtonBusinessClass).Template;
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.White);
                                    (dropedItem as NodeVM).LabelMargin = new Thickness(10);
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["pointybuttonviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconSize = (dropedItem as NodeVM).FontSize / 2;
                                        //(dropedItem as NodeVM).Annotations = new List<IAnnotation>() { _mAnnotation };
                                        _mAnnotation.Content = "Button";
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                                        (dropedItem as NodeVM).UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Left + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Right;
                                        (dropedItem as NodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                        _mAnnotation.LabelWidth = (dropedItem as NodeVM).UnitWidth;
                                        _mAnnotation.LabelHeight = (dropedItem as NodeVM).UnitHeight;
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { Foreground = false },
                                        SettingTab = new SettingTab() { Fill = true, Opacity = true, Collection = true, PointyButton = true, MenuIcon = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region bigtitle
                            else if (dropedItem.Shape.Equals("bigtitle"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("bigtitle"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "bigtitle";
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["subtitle"] as DataTemplate;
                                    (dropedItem as NodeVM).FontSize = 40;
                                    (dropedItem as NodeVM).DataContext = new PropertyChange() { HorizontalAlignment = HorizontalAlignment.Center };
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.ViewTemplate = resourceDictionary["bigtitleviewtemplate"] as DataTemplate;
                                        _mAnnotation.Content = "A Bigtitle";
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;                                        
                                        Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                                        (dropedItem as NodeVM).UnitWidth = textsize.Width;
                                        (dropedItem as NodeVM).UnitHeight = textsize.Height;
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = null, LinkTab = true };
                                }
                            }
                            #endregion
                            #region link
                            else if (dropedItem.Shape.Equals("link"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("link"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "link";
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable;
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["link"] as DataTemplate;                                    
                                    (dropedItem as NodeVM).DataContext = new LinkBusinessClass();
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Blue);
                                    (dropedItem as NodeVM).LabelHAlign = HorizontalAlignment.Left;
                                    (dropedItem as NodeVM).LabelVAlign = VerticalAlignment.Top;
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.UnitWidth = 100;
                                        _mAnnotation.UnitHeight = 100;
                                        _mAnnotation.Offset = new Point(0, 0);
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["linkviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.Content = "href";
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                                        (dropedItem as NodeVM).UnitWidth = textsize.Width;
                                        (dropedItem as NodeVM).UnitHeight = textsize.Height;
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false, Foreground = false }, SettingTab = new SettingTab() { Collection = true }, LinkTab = true };
                                }
                            }
                            #endregion
                            #region linkbar
                            else if (dropedItem.Shape.Equals("linkbar"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable;
                                    (dropedItem as NodeVM).ContentTemplate = null;
                                    (dropedItem as NodeVM).DataContext = new LinkBarBusinessClass()
                                    {
                                        Items = new ObservableCollection<object>()
                                        {
                                            new LinkBarItemBusinessClass(){Item = "Home"},
                                            new LinkBarItemBusinessClass(){Item = "Products"},
                                            new LinkBarItemBusinessClass(){Item = "Company"},
                                            new LinkBarItemBusinessClass(){Item = "Blog", IsLastItem = true}
                                        }
                                    };

                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelMargin = new Thickness(5, 0, 16, 0);
                                    (dropedItem as NodeVM).Stroke = new SolidColorBrush(Colors.Black);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.UnitWidth = 100;
                                        _mAnnotation.UnitHeight = 100;
                                        _mAnnotation.Offset = new Point(0, 0);
                                        _mAnnotation.TextHorizontalAlignment = TextAlignment.Left;
                                        _mAnnotation.TextVerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["linkbarviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        Size textsize = new Size();
                                        foreach (var str in (_mAnnotation.DataContext as LinkBarBusinessClass).Items)
                                        {
                                            if (_mAnnotation.Content == null)
                                            {
                                                _mAnnotation.Content = (str as LinkBarItemBusinessClass).Item;
                                                Size localsize = FindDummyTextSize(_mAnnotation, (str as LinkBarItemBusinessClass).Item, TextWrapping.NoWrap);
                                                textsize.Width = textsize.Width + localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right ;
                                                textsize.Height = localsize.Height;
                                            }
                                            else
                                            {
                                                _mAnnotation.Content = _mAnnotation.Content.ToString() + "," + (str as LinkBarItemBusinessClass).Item;
                                                Size localsize = FindDummyTextSize(_mAnnotation, (str as LinkBarItemBusinessClass).Item, TextWrapping.NoWrap);
                                                textsize.Width = textsize.Width + localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                            }
                                        }
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        (dropedItem as NodeVM).UnitWidth = textsize.Width;
                                        (dropedItem as NodeVM).UnitHeight = textsize.Height;
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { TextAlignment = false },
                                        SettingTab = new SettingTab() { Fill = true, Stroke = true, Selection = true },
                                        LinkTab = true
                                    };                                    
                                }
                            }
                            #endregion
                            #region breadcrumbs
                            else if (dropedItem.Shape.Equals("breadcrumbs"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable;
                                    (dropedItem as NodeVM).ContentTemplate = null;                                    
                                    (dropedItem as NodeVM).DataContext = new BreadcrumbsBusinessClass()
                                    {
                                        Items = new ObservableCollection<object>()
                                        {
                                            new BreadcrumbsItemBusinessClass(){Item = "Level 1"},
                                            new BreadcrumbsItemBusinessClass(){Item = "Level 2"},
                                            new BreadcrumbsItemBusinessClass(){Item = "Level 3"},
                                            new BreadcrumbsItemBusinessClass(){Item = "Level 4", IsLastItem = true},
                                        }
                                    };
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelMargin = new Thickness(0, 0, 5, 0);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["breadcrumbsviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        Size textsize = new Size();
                                        foreach (var str in ((_mAnnotation.DataContext as BreadcrumbsBusinessClass).Items as ObservableCollection<object>))
                                        {
                                            if (_mAnnotation.Content == null)
                                            {
                                                _mAnnotation.Content = (str as BreadcrumbsItemBusinessClass).Item;
                                                Size localsize = FindDummyTextSize(_mAnnotation, (str as BreadcrumbsItemBusinessClass).Item, TextWrapping.NoWrap);
                                                textsize.Width = textsize.Width + localsize.Width + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconWidth + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                                textsize.Height = localsize.Height + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconHeight;
                                            }
                                            else
                                            {
                                                _mAnnotation.Content = _mAnnotation.Content.ToString() + "," + (str as BreadcrumbsItemBusinessClass).Item;
                                                Size localsize = FindDummyTextSize(_mAnnotation, (str as BreadcrumbsItemBusinessClass).Item, TextWrapping.NoWrap);
                                                textsize.Width = textsize.Width + localsize.Width + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconWidth + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                                textsize.Height = localsize.Height + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconHeight;
                                            }                                            
                                        }
                                        (dropedItem as NodeVM).UnitWidth = textsize.Width;
                                        (dropedItem as NodeVM).UnitHeight = textsize.Height;
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                    }
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false, Foreground = false }, SettingTab = null, LinkTab = true };
                                }
                            }
                            #endregion
                            #region formatingtool
                            else if (dropedItem.Shape.Equals("formatingtool"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("formatingtool"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "formatingtool";
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["formatingtool"] as DataTemplate;
                                    (dropedItem as NodeVM).UnitWidth = 235;
                                    (dropedItem as NodeVM).UnitHeight = 30;
                                    AvoidLabelEditMode(dropedItem as NodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region horizontalspliter
                            else if (dropedItem.Shape.Equals("horizontalspliter"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("horizontalspliter"))
                                {
                                    (dropedItem as NodeVM).UnitHeight = 20;
                                    (dropedItem as NodeVM).ContentTemplateKey = "horizontalspliter";
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["horizontalspliter"] as DataTemplate;
                                    AvoidLabelEditMode(dropedItem as NodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region verticalspliter
                            else if (dropedItem.Shape.Equals("verticalspliter"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("verticalspliter"))
                                {
                                    (dropedItem as NodeVM).UnitWidth = 20;
                                    (dropedItem as NodeVM).ContentTemplateKey = "verticalspliter";
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeSouth | NodeConstraints.ResizeNorth;
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["verticalspliter"] as DataTemplate;
                                    AvoidLabelEditMode(dropedItem as NodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region list
                            else if (dropedItem.Shape.Equals("list"))
                            {
                                if ((dropedItem as NodeVM).ContentTemplateKey!= null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as NodeVM).ContentTemplate = null;
                                    (dropedItem as NodeVM).DataContext = new ListBusinessClass()
                                    {
                                        SelectedIndex = 0,
                                        //ScrollBar = new VerticalScrollBarBusinessClass(),
                                        Items = new ObservableCollection<object>()
                                        {
                                            new ListItemBusinessClass(){Item = "Item1"},
                                            new ListItemBusinessClass(){Item = "Item2"},
                                            new ListItemBusinessClass(){Item = "Item3"},
                                        },
                                        DuplicateItems = new ObservableCollection<object>()
                                        {
                                            new ListItemBusinessClass(){Item = "Item1"},
                                            new ListItemBusinessClass(){Item = "Item2"},
                                            new ListItemBusinessClass(){Item = "Item3"},
                                        },
                                        OddItemsBackgroud = new SolidColorBrush(Colors.AliceBlue),
                                        EvenItemsBackgroud = new SolidColorBrush(Colors.AliceBlue)
                                    };
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.White);
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).UnitWidth = 120;
                                    (dropedItem as NodeVM).UnitHeight = 160;

                                    ((dropedItem as NodeVM).DataContext as ListBusinessClass).RowHeight = (dropedItem as NodeVM).FontSize + 5;
                                    double rowscount = (dropedItem as NodeVM).UnitHeight / ((dropedItem as NodeVM).DataContext as ListBusinessClass).RowHeight;
                                    bool isInt = rowscount == (int)rowscount;
                                    if (!isInt)
                                        rowscount += 1;
                                    if((int)rowscount > ((dropedItem as NodeVM).DataContext as ListBusinessClass).Items.Count)
                                    {
                                        int count = ((dropedItem as NodeVM).DataContext as ListBusinessClass).Items.Count;
                                        for (int i = 0; i < (int)rowscount - count; i++)
                                        {
                                            ((dropedItem as NodeVM).DataContext as ListBusinessClass).DuplicateItems.Add(new ListItemBusinessClass());
                                        }
                                    }
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.UnitWidth = 100;
                                        _mAnnotation.UnitHeight = 100;
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["listviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        (_mAnnotation.DataContext as ListBusinessClass).OddItemsBackgroud = new SolidColorBrush(Colors.White);
                                        (_mAnnotation.DataContext as ListBusinessClass).EvenItemsBackgroud = new SolidColorBrush(Colors.White);
                                        foreach (var str in (_mAnnotation.DataContext as ListBusinessClass).Items)
                                        {
                                            if (_mAnnotation.Content == null)
                                            {
                                                _mAnnotation.Content = (str as ListItemBusinessClass).Item;
                                            }
                                            else
                                            {
                                                _mAnnotation.Content = _mAnnotation.Content.ToString() + "\t\n" + (str as ListItemBusinessClass).Item;
                                            }
                                        }
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                    }
                                    AnnotationPropertyChanged(dropedItem as INodeVM);
                                    NodePropertyChanged(dropedItem as INodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { TextAlignment = false },
                                        SettingTab = new SettingTab() { ListOddEven = true, Selection = true, VerticalScrollBar = true, ShowBorder = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region sitemap
                            else if (dropedItem.Shape.Equals("sitemap"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("sitemap"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "sitemap";
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["sitemap"] as DataTemplate;
                                    AvoidLabelEditMode(dropedItem as NodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                                }
                            }
                            #endregion
                            #region rectangle
                            else if (dropedItem.Shape.Equals("rectangle"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("rectangle"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "rectangle";
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["rectangle"] as DataTemplate;
                                    (dropedItem as NodeVM).UnitWidth = 200;
                                    (dropedItem as NodeVM).UnitHeight = 100;
                                    AvoidLabelEditMode(dropedItem as NodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = new SettingTab() { Fill = true, Opacity = true, Stroke = true, StrokeThickness = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region modelscreen
                            else if (dropedItem.Shape.Equals("modelscreen"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("modelscreen"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "modelscreen";
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["modelscreen"] as DataTemplate;
                                    (dropedItem as NodeVM).UnitWidth = 320;
                                    (dropedItem as NodeVM).UnitHeight = 240;
                                    AvoidLabelEditMode(dropedItem as NodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = true };
                                }
                            }
                            #endregion
                            #region textarea
                            else if (dropedItem.Shape.Equals("textarea"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("textarea"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "textarea";
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["textarea"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new TextAreaBusinessClass()
                                    {
                                        //ScrollBar = new VerticalScrollBarBusinessClass()
                                    };
                                    (dropedItem as NodeVM).UnitWidth = 150;
                                    (dropedItem as NodeVM).Stroke = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.White);
                                    (dropedItem as NodeVM).LabelHAlign = HorizontalAlignment.Left;
                                    (dropedItem as NodeVM).LabelVAlign = VerticalAlignment.Top;
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.UnitWidth = 100;
                                        _mAnnotation.UnitHeight = 100;
                                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                        _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.TextHorizontalAlignment = TextAlignment.Left;
                                        _mAnnotation.TextVerticalAlignment = VerticalAlignment.Top;
                                        _mAnnotation.Offset = new Point(0, 0);
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.ViewTemplate = resourceDictionary["textareaviewtemplate"] as DataTemplate;
                                        //_mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplateWithLeftTopAlignment"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;                                        
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        //_mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                        _mAnnotation.Content = "";
                                        //(dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationDataContextPropertyChanged(dropedItem as NodeVM);
                                    }
                                    AnnotationPropertyChanged(dropedItem as INodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { Foreground = false },
                                        SettingTab = new SettingTab() { Fill = true, Stroke = true, Collection = true, VerticalScrollBar = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region textinput
                            else if (dropedItem.Shape.Equals("textinput"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("textinput"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "textinput";
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                                    (dropedItem as NodeVM).ContentTemplate = resourceDictionary["textinput"] as DataTemplate;
                                    (dropedItem as NodeVM).DataContext = new TextAreaBusinessClass();
                                    (dropedItem as NodeVM).UnitHeight = 35;
                                    (dropedItem as NodeVM).UnitWidth = 150;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).Stroke = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).Fill = new SolidColorBrush(Colors.White);
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.LabelWidth = (dropedItem as INodeVM).UnitWidth;
                                        _mAnnotation.LabelHeight = (dropedItem as INodeVM).UnitHeight;
                                        _mAnnotation.TextAlignment = TextAlignment.Left;
                                        _mAnnotation.ViewTemplate = resourceDictionary["textinputviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.Content = "";
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        AnnotationDataContextPropertyChanged(dropedItem as NodeVM);
                                    }
                                    AnnotationPropertyChanged(dropedItem as INodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab() { Foreground = false },
                                        SettingTab = new SettingTab() { Fill = true, Opacity = true, Stroke = true, Collection = true },
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region geometricshapes
                            else if (dropedItem.Shape.Equals("geometricshapes"))
                            {
                                //if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("textinput"))
                                if ((dropedItem as NodeVM).ContentTemplateKey != null)
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = null;
                                    (dropedItem as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                                    (dropedItem as NodeVM).DataContext = new GeometricShapesBusinessClass() { HorizontalAlignment = HorizontalAlignment.Center };
                                    (dropedItem as NodeVM).ContentTemplate = ((dropedItem as NodeVM).DataContext as GeometricShapesBusinessClass).Template;
                                    (dropedItem as NodeVM).UnitWidth = 50;
                                    (dropedItem as NodeVM).UnitHeight = 50;
                                    (dropedItem as NodeVM).FontSize = 14;
                                    (dropedItem as NodeVM).LabelForeground = new SolidColorBrush(Colors.Black);
                                    (dropedItem as NodeVM).Constraints = (dropedItem as NodeVM).Constraints | NodeConstraints.AspectRatio;
                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.WrapText = TextWrapping.NoWrap;
                                        _mAnnotation.LabelWidth = (dropedItem as NodeVM).UnitWidth;
                                        _mAnnotation.LabelHeight = (dropedItem as NodeVM).UnitHeight;
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.ViewTemplate = resourceDictionary["geometricshapesviewtemplate"] as DataTemplate;
                                        _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                        _mAnnotation.LabelTextWrapping = TextWrapping.Wrap;
                                        AnnotationDataContextPropertyChanged(dropedItem as NodeVM);
                                    }
                                    AnnotationPropertyChanged(dropedItem as INodeVM);
                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = new TextTab(),
                                        SettingTab = new SettingTab() { Fill = true, Opacity = true, Stroke = true, GeometricShapes = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                            #region loading
                            else if (dropedItem.Shape.Equals("loading"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("loading"))
                                {
                                    dropedItem.UnitWidth = 100;
                                    dropedItem.UnitHeight = 100;
                                    (dropedItem as NodeVM).ContentTemplateKey = "loading";
                                    dropedItem.ContentTemplate = resourceDictionary["loading"] as DataTemplate;
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = null,
                                        LinkTab = false
                                    };
                                }
                            }
                            else if (dropedItem.Shape.Equals("loading1"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("loading1"))
                                {
                                    dropedItem.UnitWidth = 100;
                                    dropedItem.UnitHeight = 100;
                                    (dropedItem as NodeVM).ContentTemplateKey = "loading1";
                                    dropedItem.ContentTemplate = resourceDictionary["loading1"] as DataTemplate;
                                    (dropedItem as INodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = null,
                                        LinkTab = false
                                    };
                                }
                            }
                            #endregion
                            #region accordion
                            else if (dropedItem.Shape.Equals("accordion"))
                            {
                                if (!(dropedItem as NodeVM).ContentTemplateKey.Equals("accordion"))
                                {
                                    (dropedItem as NodeVM).ContentTemplateKey = "accordion";
                                    (dropedItem as NodeVM).ContentTemplate = null;
                                    (dropedItem as NodeVM).DataContext = new AccordionBusinessObject()
                                    {
                                        Items = new ObservableCollection<object>()
                                                {
                                                    new AccordionItemBusinessObject()
                                                    {
                                                        Item = "Tab1", Index = 1,
                                                        Items = new ObservableCollection<object>()
                                                        {
                                                            new AccordionSubItemBusinessObject(){Item = "- SubItem 1-1", Index = 1, ParentIndex = 1},
                                                            new AccordionSubItemBusinessObject(){Item = "- SubItem 1-2", Index = 2, ParentIndex = 1}
                                                        },
                                                    },
                                                    new AccordionItemBusinessObject()
                                                    {
                                                        Item = "Tab2" , Index = 2,                                    
                                                        Items = new ObservableCollection<object>()
                                                        {
                                                            new AccordionSubItemBusinessObject(){Item = "- SubItem 2-1", Index = 1, ParentIndex = 2},
                                                            new AccordionSubItemBusinessObject(){Item = "- SubItem 2-2", Index = 2, ParentIndex = 2}
                                                        },                                    
                                                    },
                                                    new AccordionItemBusinessObject()
                                                    {
                                                        Item = "Tab3" , Index = 3,
                                                        Items = new ObservableCollection<object>()
                                                        {
                                                            new AccordionSubItemBusinessObject(){Item = "- SubItem 3-1", Index = 1, ParentIndex = 3},
                                                            new AccordionSubItemBusinessObject(){Item = "- SubItem 3-2", Index = 2, ParentIndex = 3},
                                                            new AccordionSubItemBusinessObject(){Item = "- SubItem 3-3", Index = 3, ParentIndex = 3}
                                                        }
                                                    }
                                                }
                                    };

                                    foreach (LabelVM _mAnnotation in (dropedItem as NodeVM).Annotations as List<IAnnotation>)
                                    {
                                        _mAnnotation.DataContext = (dropedItem as NodeVM).DataContext;
                                        _mAnnotation.FontSize = 14;
                                        _mAnnotation.LabelMargin = new Thickness(10, 0, 0, 0);
                                        _mAnnotation.ViewTemplate = resourceDictionary["accordionviewtempalte"] as DataTemplate;
                                        Size textSize = new Size();
                                        foreach (var i in ((dropedItem as NodeVM).DataContext as AccordionBusinessObject).Items)
                                        {
                                            if (_mAnnotation.Content == null || string.IsNullOrEmpty(_mAnnotation.Content.ToString()))
                                            {
                                                textSize = FindDummyTextSize(_mAnnotation, (i as AccordionItemBusinessObject).Item.ToString());
                                                _mAnnotation.Content = (i as AccordionItemBusinessObject).Item;
                                                double height = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString()).Height;
                                                if ((i as AccordionItemBusinessObject).Items != null && (i as AccordionItemBusinessObject).Items.Count > 0)
                                                {
                                                    foreach (var subitem in (i as AccordionItemBusinessObject).Items)
                                                    {
                                                        _mAnnotation.Content = _mAnnotation.Content.ToString() + "\t\n" + (subitem as AccordionSubItemBusinessObject).Item;
                                                        double localWidth = FindDummyTextSize(_mAnnotation, (subitem as AccordionSubItemBusinessObject).Item.ToString()).Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                                        if (textSize.Width < localWidth)
                                                        {
                                                            textSize.Width = localWidth;
                                                        }
                                                    }

                                                    (i as AccordionItemBusinessObject).Height = (((dropedItem as NodeVM).DataContext as AccordionBusinessObject).ItemTitleHeight * (i as AccordionItemBusinessObject).Items.Count) + (i as AccordionItemBusinessObject).BottomSpace;
                                                }
                                                else
                                                    (i as AccordionItemBusinessObject).Height = ((dropedItem as NodeVM).DataContext as AccordionBusinessObject).ItemTitleHeight + (i as AccordionItemBusinessObject).BottomSpace;
                                                //(i as AccordionItemBusinessObject).Height = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString()).Height + (i as AccordionItemBusinessObject).BottomSpace;
                                            }
                                            else
                                            {
                                                double localWidth = FindDummyTextSize(_mAnnotation, (i as AccordionItemBusinessObject).Item).Width;
                                                if (textSize.Width < localWidth)
                                                {
                                                    textSize.Width = localWidth;
                                                }
                                                _mAnnotation.Content = _mAnnotation.Content.ToString() + "\t\n" + (i as AccordionItemBusinessObject).Item;
                                                double height = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString()).Height;
                                                if ((i as AccordionItemBusinessObject).Items != null && (i as AccordionItemBusinessObject).Items.Count > 0)
                                                {
                                                    foreach (var subitem in (i as AccordionItemBusinessObject).Items)
                                                    {
                                                        _mAnnotation.Content = _mAnnotation.Content.ToString() + "\t\n" + (subitem as AccordionSubItemBusinessObject).Item;
                                                        localWidth = FindDummyTextSize(_mAnnotation, (subitem as AccordionSubItemBusinessObject).Item.ToString()).Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                                        if (textSize.Width < localWidth)
                                                        {
                                                            textSize.Width = localWidth;
                                                        }
                                                    }
                                                    (i as AccordionItemBusinessObject).Height = (((dropedItem as NodeVM).DataContext as AccordionBusinessObject).ItemTitleHeight * (i as AccordionItemBusinessObject).Items.Count) + (i as AccordionItemBusinessObject).BottomSpace;
                                                }
                                                else
                                                    (i as AccordionItemBusinessObject).Height = ((dropedItem as NodeVM).DataContext as AccordionBusinessObject).ItemTitleHeight + (i as AccordionItemBusinessObject).BottomSpace;
                                            }
                                        }
                                        AnnotationPropertyChanged(dropedItem as INodeVM);
                                        (dropedItem as NodeVM).Label = _mAnnotation.Content.ToString();
                                        (dropedItem as NodeVM).UnitWidth = textSize.Width;
                                    }

                                    (dropedItem as NodeVM).PropertiesList = new PropertiesList()
                                    {
                                        AlignmentTab = true,
                                        EditTab = null,
                                        SettingTab = new SettingTab() { Accordion = true, VerticalScrollBar = true },
                                        LinkTab = true
                                    };
                                }
                            }
                            #endregion
                        }
                    };
                    #endregion
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
            #region Mockup
            else if (args.ItemSource == ItemSource.UnKnown || args.ItemSource == ItemSource.Load || args.ItemSource == ItemSource.ClipBoard)
            {
                if (args.Item is INodeVM && (args.Item as INodeVM).Shape != null)
                {
                    #region columnchart
                    if ((args.Item as INodeVM).Shape.Equals("columnchart"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["columnchart"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        (args.Item as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region piechart
                    else if ((args.Item as INodeVM).Shape.Equals("piechart"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["piechart"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        (args.Item as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region linechart
                    else if ((args.Item as INodeVM).Shape.Equals("linechart"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["linechart"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        (args.Item as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region barchart
                    else if ((args.Item as INodeVM).Shape.Equals("barchart"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["barchart"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        (args.Item as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region bubblechart
                    else if ((args.Item as INodeVM).Shape.Equals("bubblechart"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["bubblechart"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        (args.Item as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region horizontalscrollbar
                    else if ((args.Item as INodeVM).Shape.Equals("horizontalscrollbar"))
                    {
                        (args.Item as INodeVM).Constraints = (args.Item as INodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["horizontalscrollbar"] as DataTemplate;
                        //(args.Item as INodeVM).DataContext = new HorizontalScrollBarBusinessClass();
                        (args.Item as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { ScrollSlider = true }, LinkTab = false };
                    }
                    #endregion
                    #region verticalscrollbar
                    else if ((args.Item as INodeVM).Shape.Equals("verticalscrollbar"))
                    {
                        (args.Item as INodeVM).Constraints = (args.Item as INodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeSouth | NodeConstraints.ResizeNorth;
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["verticalscrollbar"] as DataTemplate;
                        //(args.Item as INodeVM).DataContext = new VerticalScrollBarBusinessClass();
                        (args.Item as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { ScrollSlider = true }, LinkTab = false };
                    }
                    #endregion
                    #region videoPlayer
                    else if ((args.Item as INodeVM).Shape.Equals("videoPlayer"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["videoPlayer"] as DataTemplate;
                        (args.Item as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region streetmap
                    else if ((args.Item as INodeVM).Shape.Equals("streetmap"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["streetmap"] as DataTemplate;
                        (args.Item as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region ioskeyboard
                    else if ((args.Item as INodeVM).Shape.Equals("ioskeyboard"))
                    {
                        //(args.Item as INodeVM).DataContext = new IOSKeyboardBusinessClass();
                        (args.Item as INodeVM).ContentTemplate = ((args.Item as INodeVM).DataContext as IOSKeyboardBusinessClass).Template;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        (args.Item as INodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = new SettingTab() { ioskeyboard = true },
                            LinkTab = false
                        };
                    }
                    #endregion
                    #region ipad
                    else if ((args.Item as INodeVM).Shape.Equals("ipad"))
                    {
                        //(args.Item as INodeVM).DataContext = new IPADBusinessClass();
                        (args.Item as INodeVM).ContentTemplate = ((args.Item as INodeVM).DataContext as IPADBusinessClass).Template;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        AnnotationDataContextPropertyChanged(args.Item as INodeVM);
                        (args.Item as INodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = new SettingTab() { ipad = true },
                            LinkTab = false
                        };
                    }
                    #endregion
                    #region iphone
                    else if ((args.Item as INodeVM).Shape.Equals("iphone"))
                    {
                        //(args.Item as INodeVM).DataContext = new IPhoneBusinessClass();
                        (args.Item as INodeVM).ContentTemplate = ((args.Item as INodeVM).DataContext as IPhoneBusinessClass).Template;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        AnnotationDataContextPropertyChanged(args.Item as INodeVM);
                        (args.Item as INodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = new SettingTab() { IPhone = true },
                            LinkTab = false
                        };
                    }
                    #endregion
                    #region captcha
                    else if ((args.Item as INodeVM).Shape.Equals("captcha"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["captcha"] as DataTemplate;
                        (args.Item as INodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = null,
                            LinkTab = false
                        };
                    }
                    #endregion
                    #region verticalslider
                    else if ((args.Item as INodeVM).Shape.Equals("verticalslider"))
                    {
                        (args.Item as INodeVM).Constraints = (args.Item as INodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeSouth | NodeConstraints.ResizeNorth;
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["verticalslider"] as DataTemplate;
                        //(args.Item as INodeVM).DataContext = new SliderBusinessClass();
                        (args.Item as INodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = new SettingTab() { Stroke = true, StrokeThickness = true, ScrollSlider = true, Collection = true },
                            LinkTab = false
                        };
                    }
                    #endregion
                    #region horizotalslider
                    else if ((args.Item as INodeVM).Shape.Equals("horizotalslider"))
                    {
                        (args.Item as INodeVM).Constraints = (args.Item as INodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["horizotalslider"] as DataTemplate;
                        //(args.Item as INodeVM).DataContext = new SliderBusinessClass();
                        (args.Item as INodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = new SettingTab() { Stroke = true, StrokeThickness = true, ScrollSlider = true, Collection = true },
                            LinkTab = false
                        };
                    }
                    #endregion
                    #region helpbutton
                    else if ((args.Item as INodeVM).Shape.Equals("helpbutton"))
                    {
                        (args.Item as INodeVM).Constraints = (args.Item as INodeVM).Constraints & ~NodeConstraints.Resizable;
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["helpbutton"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        foreach (LabelVM _mAnnotation in (args.Item as INodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.ViewTemplate = resourceDictionary["hyperlinkonlyviewtemplate"] as DataTemplate;
                        }
                        (args.Item as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region volume
                    else if ((args.Item as INodeVM).Shape.Equals("volume"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["volume"] as DataTemplate;
                        (args.Item as INodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = null,
                            LinkTab = false
                        };
                        AvoidLabelEditMode(args.Item as INodeVM);
                    }
                    #endregion
                    #region menubar
                    else if ((args.Item as INodeVM).Shape.Equals("menubar"))
                    {
                        (args.Item as INodeVM).ContentTemplate = null;
                        (args.Item as INodeVM).LabelMargin = new Thickness(5);
                        (args.Item as INodeVM).FontSize = 14;
                        string[] words = (args.Item as INodeVM).Label.ToString().Split(',');
                        foreach (LabelVM _mAnnotation in (args.Item as INodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as INodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as INodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["menubarviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as INodeVM).DataContext;                            
                            AnnotationPropertyChanged(args.Item as NodeVM);
                        }


                        (args.Item as INodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = new SettingTab() { Selection = true, ShowBorder = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region menu
                    else if ((args.Item as INodeVM).Shape.Equals("menu"))
                    {
                        (args.Item as INodeVM).Constraints = (args.Item as INodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as INodeVM).ContentTemplate = null;
                        string[] words = (args.Item as INodeVM).Label.ToString().Split('\n');
                        foreach (LabelVM _mAnnotation in (args.Item as INodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as INodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as INodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["menuviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as INodeVM).DataContext;
                            (_mAnnotation.DataContext as MenuBusinessClass).Items.Clear();
                            foreach (string str in words)
                            {
                                string[] childsplit = str.Split(',');
                                var item = new MenuItemBusinessClass() { Item = childsplit[0], Foreground = _mAnnotation.LabelForeground };//, ShortcutKey=childsplit[1], Foreground = (s as LabelVM).LabelForeground };

                                if (childsplit.Count() > 1)
                                {
                                    item.ShortcutKey = childsplit[1];
                                }
                                (_mAnnotation.DataContext as MenuBusinessClass).Items.Add(item);
                                int indexOf = childsplit[0].IndexOfAny(_mCharacters);
                                if (indexOf == -1)
                                {
                                    item.BreakLine = Visibility.Visible;
                                }
                            }
                            (_mAnnotation.DataContext as MenuBusinessClass).SelectedIndex = (_mAnnotation.DataContext as MenuBusinessClass).SelectedIndex;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }

                        (args.Item as INodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { TextAlignment = false, Foreground = false },
                            SettingTab = new SettingTab() { Selection = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region playbackcontrol
                    else if ((args.Item as INodeVM).Shape.Equals("playbackcontrol"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["playbackcontrol"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region webcam
                    else if ((args.Item as INodeVM).Shape.Equals("webcam"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["webcam"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints | NodeConstraints.AspectRatio;
                    }
                    #endregion
                    #region tabsbar
                    else if ((args.Item as INodeVM).Shape.Equals("tabsbar"))
                    {
                        (args.Item as INodeVM).ContentTemplate = null;
                        //(args.Item as NodeVM).LabelMargin = new Thickness(10, 5, 10, 5);
                        (args.Item as NodeVM).LabelMargin = new Thickness(10, 5, 10, 5);
                        string[] words = (args.Item as INodeVM).Label.ToString().Split(',');
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as INodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as INodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["tabbarTopviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            if ((_mAnnotation.DataContext as TabBusinessClass).HorizontalTabAlignment == VerticalAlignment.Top)
                                ((_mAnnotation.DataContext as TabBusinessClass).Items[(_mAnnotation.DataContext as TabBusinessClass).Items.Count() - 1] as TabItemBusinessClass).IsLastItem = true;
                            else
                                _mAnnotation.ViewTemplate = resourceDictionary["tabbarBottomviewtemplate"] as DataTemplate;
                            AnnotationPropertyChanged(args.Item as NodeVM);
                        }                       

                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { TextAlignment = false },
                            SettingTab = new SettingTab() { Fill = true, Opacity = true, Selection = true, HorizontalTab = true, VerticalScrollBar = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region verticaltab
                    else if ((args.Item as INodeVM).Shape.Equals("verticaltab"))
                    {
                        (args.Item as INodeVM).ContentTemplate = null;
                        (args.Item as NodeVM).LabelMargin = new Thickness(10, 5, 10, 5);
                        string[] words = (args.Item as INodeVM).Label.ToString().Split(',');
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as INodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as INodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["verticaltabLeftviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            if ((_mAnnotation.DataContext as TabBusinessClass).VerticalTabAlignment == HorizontalAlignment.Left)
                                _mAnnotation.ViewTemplate = resourceDictionary["verticaltabLeftviewtemplate"] as DataTemplate;
                            else
                                _mAnnotation.ViewTemplate = resourceDictionary["verticaltabRightviewtemplate"] as DataTemplate;
                            AnnotationPropertyChanged(args.Item as NodeVM);
                        }

                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { TextAlignment = false },
                            SettingTab = new SettingTab() { Fill = true, Opacity = true, Selection = true, VerticalTab = true, VerticalScrollBar = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region buttonbar
                    else if ((args.Item as INodeVM).Shape.Equals("buttonbar"))
                    {
                        (args.Item as INodeVM).ContentTemplate = null;
                        (args.Item as NodeVM).LabelMargin = new Thickness(10);
                        string[] words = (args.Item as INodeVM).Label.ToString().Split(',');
                        foreach (LabelVM _mAnnotation in (args.Item as INodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as INodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as INodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["buttonbarviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            AnnotationPropertyChanged(args.Item as NodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { TextAlignment = false },
                            SettingTab = new SettingTab() { Selection = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region searchbutton
                    else if ((args.Item as INodeVM).Shape.Equals("searchbutton"))
                    {
                        (args.Item as INodeVM).Constraints = (args.Item as INodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["searchbutton"] as DataTemplate;
                        (args.Item as INodeVM).LabelMargin = new Thickness(10, 5, 12, 5);
                        (args.Item as INodeVM).LabelHAlign = HorizontalAlignment.Left;
                        foreach (LabelVM _mAnnotation in (args.Item as INodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.Offset = new Point(0, 0.5);
                            _mAnnotation.LabelWidth = (args.Item as INodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as INodeVM).UnitHeight;
                            _mAnnotation.DataContext = (args.Item as INodeVM).DataContext;
                            _mAnnotation.ViewTemplate = resourceDictionary["searchbuttonviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplateWithLeftTopAlignment"] as DataTemplate;
                            _mAnnotation.Content = (args.Item as INodeVM).Label;
                            AnnotationPropertyChanged(args.Item as NodeVM);
                        }

                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab(),
                            SettingTab = new SettingTab() { Collection = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region progressbar
                    else if ((args.Item as INodeVM).Shape.Equals("progressbar"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["progressbar"] as DataTemplate;
                        //(args.Item as INodeVM).DataContext = new ProgressBarBusinessClass();
                        ((args.Item as INodeVM) as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { ScrollSlider = true }, LinkTab = false };
                        AvoidLabelEditMode(args.Item as INodeVM);
                    }
                    #endregion
                    #region volumeslider
                    else if ((args.Item as INodeVM).Shape.Equals("volumeslider"))
                    {
                        (args.Item as INodeVM).Constraints = (args.Item as INodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["volumeslider"] as DataTemplate;
                        //(args.Item as INodeVM).DataContext = new VolumeSliderBusinessClass();
                        (args.Item as INodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { ScrollSlider = true }, LinkTab = false };
                        
                        AvoidLabelEditMode(args.Item as INodeVM);
                    }
                    #endregion
                    #region browserWindow
                    else if ((args.Item as INodeVM).Shape.Equals("browserWindow"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["browserWindow"] as DataTemplate;
                        string[] words = (args.Item as INodeVM).Label.ToString().Split('\n');                        
                        foreach (LabelVM _mAnnotation in (args.Item as INodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.Offset = new Point(0, 0);
                            _mAnnotation.LabelWidth = (args.Item as INodeVM).UnitWidth - (args.Item as INodeVM).LabelMargin.Right;
                            _mAnnotation.ViewTemplate = resourceDictionary["browserwindowviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplateWithLeftTopAlignment"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            
                        }
                        AnnotationPropertyChanged(args.Item as NodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = new SettingTab() { Fill = true, VerticalScrollBar = true },
                            LinkTab = false
                        };
                    }
                    #endregion
                    #region checkbox
                    else if ((args.Item as INodeVM).Shape.Equals("checkbox") || (args.Item as INodeVM).Shape.Equals("radiobutton"))
                    {
                        (args.Item as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["checkbox"] as DataTemplate;
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.LabelMargin = new Thickness(10, 0, 10, 0);
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            if ((args.Item as INodeVM).Shape.Equals("checkbox"))
                                _mAnnotation.ViewTemplate = resourceDictionary["selectionboxviewtemplate"] as DataTemplate;
                            else if ((args.Item as INodeVM).Shape.Equals("radiobutton"))
                                _mAnnotation.ViewTemplate = resourceDictionary["radiobuttonviewtemplate"] as DataTemplate;
                            AnnotationPropertyChanged(args.Item as NodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { TextAlignment = false },
                            SettingTab = new SettingTab() { Collection = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region datechooser
                    else if ((args.Item as INodeVM).Shape.Equals("datechooser"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as NodeVM).ContentTemplate = null;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.LabelMargin = new Thickness(10, 5, 10, 5);
                            _mAnnotation.ViewTemplate = resourceDictionary["datechooserviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as NodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab(),
                            SettingTab = new SettingTab() { Stroke = true, Collection = true },
                            LinkTab = false
                        };
                    }
                    #endregion
                    #region windowbox
                    else if ((args.Item as INodeVM).Shape.Equals("windowbox"))
                    {
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["windowbox"] as DataTemplate;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.Offset = new Point(0, 0);
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.ViewTemplate = resourceDictionary["defaultviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            
                            _mAnnotation.LabelHeight = (_mAnnotation.DataContext as WindowBoxBusinessObject).TitleBarHeight = _mAnnotation.FontSize + (_mAnnotation.DataContext as WindowBoxBusinessObject).TopIconHeight;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as NodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false }, SettingTab = new SettingTab() { WindowBox = true, VerticalScrollBar = true }, LinkTab = false };
                        (args.Item as NodeVM).IsSelected = true;
                    }
                    #endregion
                    #region onoffswitch
                    else if ((args.Item as INodeVM).Shape.Equals("onoffswitch"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable;
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["onoffswitch"] as DataTemplate;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["onoffswitchviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = null;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.LabelMargin = new Thickness(0, 0, 10, 0);
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AvoidLabelEditMode(args.Item as INodeVM);
                            AnnotationDataContextPropertyChanged(args.Item as INodeVM);
                        }

                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = new SettingTab() { Fill = true, OnOffSwitch = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region button
                    else if ((args.Item as INodeVM).Shape.Equals("button"))
                    {
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["button"] as DataTemplate;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelMargin = new Thickness(15, 5, 15, 5);
                            _mAnnotation.ViewTemplate = resourceDictionary["defaultviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab(),
                            SettingTab = new SettingTab() { Fill = true, Opacity = true, Collection = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region numericstepper
                    else if ((args.Item as INodeVM).Shape.Equals("numericstepper"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as NodeVM).ContentTemplate = null;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as INodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as INodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["numericstepperviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { TextAlignment = false },
                            SettingTab = new SettingTab() { Stroke = true, Collection = true },
                            LinkTab = false
                        };
                        (args.Item as NodeVM).IsSelected = true;
                    }
                    #endregion
                    #region multilinebutton
                    else if ((args.Item as INodeVM).Shape.Equals("multilinebutton"))
                    {
                        foreach (LabelVM _mAnnotation in (args.Item as INodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as INodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as INodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["multilinebuttonviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as INodeVM).DataContext;
                            AnnotationPropertyChanged(args.Item as NodeVM);
                        }
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["multilinebutton"] as DataTemplate;
                        (args.Item as INodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { TextAlignment = false },
                            SettingTab = new SettingTab() { Fill = true},
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region messagebox
                    else if ((args.Item as INodeVM).Shape.Equals("messagebox"))
                    {
                        (args.Item as NodeVM).ContentTemplate = null;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as INodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as INodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["messageboxviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.Content = (args.Item as INodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = null,
                            LinkTab = true
                        };   
                    }
                    #endregion
                    #region tooltip
                    else if ((args.Item as INodeVM).Shape.Equals("tooltip"))
                    {
                        //(args.Item as NodeVM).DataContext = new ToolTipBusinessClass();
                        (args.Item as NodeVM).ContentTemplate = ((args.Item as NodeVM).DataContext as ToolTipBusinessClass).Template;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.ViewTemplate = resourceDictionary["tooltipviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab(),
                            SettingTab = new SettingTab() { ToolTip = true },
                            LinkTab = false
                        };
                    }
                    #endregion
                    #region alertbox
                    else if ((args.Item as INodeVM).Shape.Equals("alertbox"))
                    {
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["alertbox"] as DataTemplate;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                        {
                            if (!(_mAnnotation is HyperLinkVM))
                            {
                                _mAnnotation.WrapText = TextWrapping.NoWrap;
                                _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                                _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                                _mAnnotation.ViewTemplate = resourceDictionary["alertboxviewtemplate"] as DataTemplate;
                                _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                                _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                                _mAnnotation.Content = (args.Item as NodeVM).Label;
                                AnnotationPropertyChanged(args.Item as INodeVM);
                            }
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = null,
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region calendar
                    else if ((args.Item as INodeVM).Shape.Equals("calendar"))
                    {
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["calendar"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region combobox
                    else if ((args.Item as INodeVM).Shape.Equals("combobox"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as NodeVM).ContentTemplate = null;// resourceDictionary["combobox"] as DataTemplate;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.ViewTemplate = resourceDictionary["comboboxviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                            AnnotationDataContextPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false }, SettingTab = new SettingTab() { Stroke = true, Collection = true }, LinkTab = true };
                    }
                    #endregion
                    #region scratchout
                    else if ((args.Item as INodeVM).Shape.Equals("scratchout"))
                    {
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["scratchout"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as NodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { Fill = true, Opacity = true }, LinkTab = false };
                    }
                    #endregion
                    #region horizontalcurlybraces
                    else if ((args.Item as INodeVM).Shape.Equals("horizontalcurlybraces"))
                    {
                        (args.Item as NodeVM).ContentTemplate = null;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.Offset = new Point(0.5, 0);
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["horizontalcurlybracessviewtemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                            AnnotationDataContextPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = new SettingTab() { HorizontalCurlyBraces = true }, LinkTab = false };
                    }
                    #endregion
                    #region verticalcurlybraces
                    else if ((args.Item as INodeVM).Shape.Equals("verticalcurlybraces"))
                    {
                        (args.Item as NodeVM).ContentTemplate = null;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.Offset = new Point(0, 0.5);
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["verticalcurlybracessviewtemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = new SettingTab() { VerticalCurlyBraces = true }, LinkTab = false };
                    }
                    #endregion
                    #region stickynote
                    else if ((args.Item as INodeVM).Shape.Equals("stickynote"))
                    {
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["stickynote"] as DataTemplate;
                        //(args.Item as NodeVM).DataContext = new StickyNoteBusinessClass();
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {                            
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["stickynoteviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            switch ((_mAnnotation.DataContext as StickyNoteBusinessClass).HorizontalAlignment)
                            {
                                case HorizontalAlignment.Left:
                                    _mAnnotation.TextAlignment = TextAlignment.Left;
                                    break;
                                case HorizontalAlignment.Right:
                                    _mAnnotation.TextAlignment = TextAlignment.Right;
                                    break;
                                case HorizontalAlignment.Center:
                                    _mAnnotation.TextAlignment = TextAlignment.Center;
                                    break;
                            }
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                            AnnotationDataContextPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = new SettingTab() { Fill = true }, LinkTab = false };
                    }
                    #endregion
                    #region subtitle
                    else if ((args.Item as INodeVM).Shape.Equals("subtitle"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["subtitle"] as DataTemplate;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.ViewTemplate = resourceDictionary["subtitleviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                            SelectorPropertyChanged();
                        }

                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = null, LinkTab = true };
                    }
                    #endregion
                    #region checkboxgroup
                    else if ((args.Item as INodeVM).Shape.Equals("checkboxgroup"))
                    {
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["checkboxgroup"] as DataTemplate;
                        //(args.Item as NodeVM).DataContext = new CheckBoxGroupBusinessClass() { Items = new ObservableCollection<object>() };
                        string[] words = (args.Item as NodeVM).Label.ToString().Split('\n');

                        Dictionary<string, int> remover = new Dictionary<string, int>();
                        for (int i = 0; i < words.Count(); i++)
                        {
                            if (words[i].Contains("\r"))
                                remover.Add(words[i].Remove(words[i].Length - 1, 1), i);
                        }
                        foreach (var i in remover)
                        {
                            words[i.Value] = i.Key;
                        }
                        remover = null;

                        if (words.Count() == (((args.Item as NodeVM).DataContext as CheckBoxGroupBusinessClass).Items as ObservableCollection<object>).Count())
                        {
                            for (int i = 0; i < words.Count(); i++)
                            {
                                ((((args.Item as NodeVM).DataContext as CheckBoxGroupBusinessClass).Items as ObservableCollection<object>)[i] as CheckBoxItemBusinessClass).Item = words[i];
                            }
                        }

                        //foreach (string str in words)
                        //{
                        //    (((args.Item as NodeVM).DataContext as CheckBoxGroupBusinessClass).Items as ObservableCollection<object>).Add(new CheckBoxItemBusinessClass()
                        //    {
                        //        Item = str,
                        //        Stroke = (args.Item as NodeVM).Stroke,
                        //        Foreground = (args.Item as NodeVM).LabelForeground
                        //    });
                        //}


                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["checkboxgroupviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { TextAlignment = false },
                            SettingTab = null,
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region radiobuttongroup
                    else if ((args.Item as INodeVM).Shape.Equals("radiobuttongroup"))
                    {
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["radiobuttongroup"] as DataTemplate;
                        //(args.Item as NodeVM).DataContext = new RadioButtonGroupBusinessClass() { Items = new ObservableCollection<object>() };
                        string[] words = (args.Item as NodeVM).Label.ToString().Split('\n');
                        
                        if (words.Count() == (((args.Item as NodeVM).DataContext as RadioButtonGroupBusinessClass).Items as ObservableCollection<object>).Count())
                        {
                            for (int i = 0; i < words.Count(); i++)
                            {
                                ((((args.Item as NodeVM).DataContext as RadioButtonGroupBusinessClass).Items as ObservableCollection<object>)[i] as RadioButtonItemBusinessClass).Item = words[i];
                            }
                        }

                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["radiobuttongroupviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { TextAlignment = false },
                            SettingTab = null,
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region colorpicker
                    else if ((args.Item as INodeVM).Shape.Equals("colorpicker"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable;
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["colorpicker"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as INodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = new SettingTab() { Fill = true, Stroke = true, StrokeThickness = true },
                            LinkTab = false
                        };
                    }
                    #endregion
                    #region redx
                    else if ((args.Item as INodeVM).Shape.Equals("redx"))
                    {
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["redx"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as NodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region fieldset
                    else if ((args.Item as INodeVM).Shape.Equals("fieldset"))
                    {
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["fieldset"] as DataTemplate;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["fieldsetviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { Fill = true }, LinkTab = false };
                    }
                    #endregion
                    #region popover
                    else if ((args.Item as INodeVM).Shape.Equals("popover"))
                    {
                        //(args.Item as NodeVM).DataContext = new PopoverBusinessClass();
                        (args.Item as NodeVM).ContentTemplate = ((args.Item as NodeVM).DataContext as PopoverBusinessClass).Template;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.Offset = new Point(0, 0);
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.LabelTextWrapping = TextWrapping.Wrap;
                            _mAnnotation.LabelMargin = new Thickness(5);
                            _mAnnotation.ViewTemplate = resourceDictionary["defaultviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplateWithLeftTopAlignment"] as DataTemplate;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false }, SettingTab = new SettingTab() { Fill = true, Popover = true }, LinkTab = false };
                    }
                    #endregion
                    #region pointybutton
                    else if ((args.Item as INodeVM).Shape.Equals("pointybutton"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        //(args.Item as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                        //(args.Item as NodeVM).DataContext = new PointyButtonBusinessClass();
                        (args.Item as NodeVM).ContentTemplate = ((args.Item as NodeVM).DataContext as PointyButtonBusinessClass).Template;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            //_mAnnotation.Offset = new Point(0, 0);
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["pointybuttonviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconSize = (args.Item as NodeVM).FontSize / 2;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { Foreground = false },
                            SettingTab = new SettingTab() { Fill = true, Opacity = true, Collection = true, PointyButton = true, MenuIcon = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region bigtitle
                    else if ((args.Item as INodeVM).Shape.Equals("bigtitle"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["subtitle"] as DataTemplate;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {   
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.ViewTemplate = resourceDictionary["bigtitleviewtemplate"] as DataTemplate;
                            //_mAnnotation.Content = "A Bigtitle";
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = null, LinkTab = true };
                    }
                    #endregion
                    #region link
                    else if ((args.Item as INodeVM).Shape.Equals("link"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable;
                        //(args.Item as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["link"] as DataTemplate;
                        //(args.Item as NodeVM).DataContext = new LinkBusinessClass();
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.Offset = new Point(0, 0);
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.ViewTemplate = resourceDictionary["linkviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false, Foreground = false }, SettingTab = new SettingTab() { Collection = true }, LinkTab = true };
                    }
                    #endregion
                    #region linkbar
                    else if ((args.Item as INodeVM).Shape.Equals("linkbar"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable;
                        (args.Item as NodeVM).ContentTemplate = null;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["linkbarviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                            ((_mAnnotation.DataContext as LinkBarBusinessClass).Items[(_mAnnotation.DataContext as LinkBarBusinessClass).Items.Count - 1] as LinkBarItemBusinessClass).IsLastItem = true;
                            (_mAnnotation.DataContext as LinkBarBusinessClass).SelectedIndex = (_mAnnotation.DataContext as LinkBarBusinessClass).SelectedIndex;
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { TextAlignment = false },
                            SettingTab = new SettingTab() { Fill = true, Stroke = true, Selection = true },
                            LinkTab = true
                        };  
                    }
                    #endregion
                    #region breadcrumbs
                    else if ((args.Item as INodeVM).Shape.Equals("breadcrumbs"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable;
                        (args.Item as NodeVM).ContentTemplate = null;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["breadcrumbsviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationPropertyChanged(args.Item as INodeVM);
                        }
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false, Foreground = false }, SettingTab = null, LinkTab = true };
                    }
                    #endregion
                    #region formatingtool
                    else if ((args.Item as INodeVM).Shape.Equals("formatingtool"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["formatingtool"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as NodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region horizontalspliter
                    else if ((args.Item as INodeVM).Shape.Equals("horizontalspliter"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["horizontalspliter"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as NodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region verticalspliter
                    else if ((args.Item as INodeVM).Shape.Equals("verticalspliter"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeSouth | NodeConstraints.ResizeNorth;
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["verticalspliter"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as NodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region list
                    else if ((args.Item as INodeVM).Shape.Equals("list"))
                    {
                        (args.Item as NodeVM).ContentTemplate = null;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["listviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                        }
                        ((args.Item as NodeVM).DataContext as ListBusinessClass).SelectedIndex = ((args.Item as NodeVM).DataContext as ListBusinessClass).SelectedIndex;
                        AnnotationPropertyChanged(args.Item as INodeVM);
                        NodePropertyChanged(args.Item as INodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { TextAlignment = false },
                            SettingTab = new SettingTab() { ListOddEven = true, Selection = true, VerticalScrollBar = true, ShowBorder = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region sitemap
                    else if ((args.Item as INodeVM).Shape.Equals("sitemap"))
                    {
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["sitemap"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as NodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                    }
                    #endregion
                    #region rectangle
                    else if ((args.Item as INodeVM).Shape.Equals("rectangle"))
                    {
                        //(args.Item as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["rectangle"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as NodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = new SettingTab() { Fill = true, Opacity = true, Stroke = true, StrokeThickness = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region modelscreen
                    else if ((args.Item as INodeVM).Shape.Equals("modelscreen"))
                    {
                        //(args.Item as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["modelscreen"] as DataTemplate;
                        AvoidLabelEditMode(args.Item as NodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = true };
                    }
                    #endregion
                    #region textarea
                    else if ((args.Item as INodeVM).Shape.Equals("textarea"))
                    {
                        //(args.Item as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["textarea"] as DataTemplate;
                        //(args.Item as NodeVM).DataContext = new TextAreaBusinessClass() { Stroke = (args.Item as NodeVM).Stroke, Foreground = (args.Item as NodeVM).LabelForeground };
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.Offset = new Point(0, 0);
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["textareaviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplateWithLeftTopAlignment"] as DataTemplate;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationDataContextPropertyChanged(args.Item as NodeVM);
                        }
                        AnnotationPropertyChanged(args.Item as INodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { Foreground = false },
                            SettingTab = new SettingTab() { Fill = true, Stroke = true, Collection = true, VerticalScrollBar = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region textinput
                    else if ((args.Item as INodeVM).Shape.Equals("textinput"))
                    {
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                        (args.Item as NodeVM).ContentTemplate = resourceDictionary["textinput"] as DataTemplate;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.LabelWidth = (args.Item as INodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as INodeVM).UnitHeight;
                            switch ((_mAnnotation.DataContext as TextAreaBusinessClass).HorizontalAlignment)
                            {
                                case HorizontalAlignment.Left:
                                    _mAnnotation.TextAlignment = TextAlignment.Left;
                                    break;
                                case HorizontalAlignment.Right:
                                    _mAnnotation.TextAlignment = TextAlignment.Right;
                                    break;
                                case HorizontalAlignment.Center:
                                    _mAnnotation.TextAlignment = TextAlignment.Center;
                                    break;
                            }
                            _mAnnotation.ViewTemplate = resourceDictionary["textinputviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultSingleLineEdittemplate"] as DataTemplate;
                            _mAnnotation.Content = (args.Item as NodeVM).Label;
                            AnnotationDataContextPropertyChanged(args.Item as NodeVM);
                        }
                        AnnotationPropertyChanged(args.Item as INodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab() { Foreground = false },
                            SettingTab = new SettingTab() { Fill = true, Opacity = true, Stroke = true, Collection = true },
                            LinkTab = false
                        };
                    }
                    #endregion
                    #region geometricshapes
                    else if ((args.Item as INodeVM).Shape.Equals("geometricshapes"))
                    {
                        (args.Item as NodeVM).Link = new HyperlinkBusinessClass() { Title = "Link" };
                        (args.Item as NodeVM).ContentTemplate = ((args.Item as NodeVM).DataContext as GeometricShapesBusinessClass).Template;
                        (args.Item as NodeVM).Constraints = (args.Item as NodeVM).Constraints | NodeConstraints.AspectRatio;
                        foreach (LabelVM _mAnnotation in (args.Item as NodeVM).Annotations as List<IAnnotation>)
                        {
                            _mAnnotation.WrapText = TextWrapping.NoWrap;
                            _mAnnotation.DataContext = (args.Item as NodeVM).DataContext;
                            _mAnnotation.LabelWidth = (args.Item as NodeVM).UnitWidth;
                            _mAnnotation.LabelHeight = (args.Item as NodeVM).UnitHeight;
                            _mAnnotation.ViewTemplate = resourceDictionary["geometricshapesviewtemplate"] as DataTemplate;
                            _mAnnotation.EditTemplate = resourceDictionary["DefaultMultiLineEdittemplate"] as DataTemplate;
                            _mAnnotation.LabelTextWrapping = TextWrapping.Wrap;
                            AnnotationDataContextPropertyChanged(args.Item as NodeVM);
                        }
                        AnnotationPropertyChanged(args.Item as INodeVM);
                        (args.Item as NodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = new TextTab(),
                            SettingTab = new SettingTab() { Fill = true, Opacity = true, Stroke = true, GeometricShapes = true },
                            LinkTab = true
                        };
                    }
                    #endregion
                    #region loading
                    else if ((args.Item as INodeVM).Shape.Equals("loading"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["loading"] as DataTemplate;
                        (args.Item as INodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = null,
                            LinkTab = false
                        };
                    }
                    else if ((args.Item as INodeVM).Shape.Equals("loading1"))
                    {
                        (args.Item as INodeVM).ContentTemplate = resourceDictionary["loading1"] as DataTemplate;
                        (args.Item as INodeVM).PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = true,
                            EditTab = null,
                            SettingTab = null,
                            LinkTab = false
                        };
                    }
                    #endregion
                    (args.Item as INodeVM).IsSelected = true;
                    (this.SelectedItems as SelectorVM).PropertiesList = (args.Item as NodeVM).PropertiesList;
                }
            }
            #endregion
            CheckEmpty();
        }

        #region Mockup

        void SelectorPropertyChanged()
        {
            (this.SelectedItems as SelectorVM).PropertyChanged += (s, e) =>
                {
                    if(e.PropertyName == "LabelHAlign")
                    {
                        
                    }
                };
        }
        void NodePropertyChanged(INodeVM node)
        {
            node.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "UnitHeight")
                {
                    if ((node as INodeVM).Shape.Equals("list"))
                    {
                        double rowscount = (node as NodeVM).UnitHeight / ((node as NodeVM).DataContext as ListBusinessClass).RowHeight;
                        bool isInt = rowscount == (int)rowscount;
                        if (!isInt)
                            rowscount += 1;

                        ((node as NodeVM).DataContext as ListBusinessClass).DuplicateItems.Clear();
                        for (int i = 0; i < (int)rowscount; i++)
                        {
                            if (i < ((node as NodeVM).DataContext as ListBusinessClass).Items.Count)
                            {
                                var localItem = ((node as NodeVM).DataContext as ListBusinessClass).Items[i];
                                ((node as NodeVM).DataContext as ListBusinessClass).DuplicateItems.Add(localItem);
                                if (i % 2 == 0)
                                    (((node as NodeVM).DataContext as ListBusinessClass).DuplicateItems[i] as ListItemBusinessClass).Background = ((node as NodeVM).DataContext as ListBusinessClass).EvenItemsBackgroud;
                                else
                                    (((node as NodeVM).DataContext as ListBusinessClass).DuplicateItems[i] as ListItemBusinessClass).Background = ((node as NodeVM).DataContext as ListBusinessClass).OddItemsBackgroud;
                            }
                            else
                            {
                                if (i % 2 == 0)
                                    ((node as NodeVM).DataContext as ListBusinessClass).DuplicateItems.Add(new ListItemBusinessClass() { Background = ((node as NodeVM).DataContext as ListBusinessClass).EvenItemsBackgroud });
                                else
                                    ((node as NodeVM).DataContext as ListBusinessClass).DuplicateItems.Add(new ListItemBusinessClass() { Background = ((node as NodeVM).DataContext as ListBusinessClass).OddItemsBackgroud });
                            }
                        }
                    }
                }
            };
        }
        void AnnotationPropertyChanged(INodeVM node)
        {
            foreach (LabelVM _mAnnotation in node.Annotations as List<IAnnotation>)
            {
                _mAnnotation.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "Mode")
                    {
                        if (!_mAnnotation.IsEditable)
                        {
                            if ((s as LabelVM).Mode == ContentEditorMode.Edit)
                                (s as LabelVM).Mode = ContentEditorMode.View;
                        }
                        else
                        {
                            if ((s as LabelVM).Mode == ContentEditorMode.Edit)
                            {
                                (this.SelectedItems as SelectorVM).OpenEditPanel = null;
                                (this.SelectedItems as SelectorVM).OpenLinkPanel = null;
                                (this.SelectedItems as SelectorVM).OpenSettingPanel = null;
                                (this.SelectedItems as SelectorVM).OpenTextPanel = null;
                            }

                            #region menu
                            if ((node as INodeVM).Shape.Equals("menu"))
                            {
                                if ((s as LabelVM).Mode == ContentEditorMode.Edit)
                                {
                                    string editableString = string.Empty;
                                    foreach (var str in ((s as LabelVM).DataContext as MenuBusinessClass).Items)
                                    {
                                        if (string.IsNullOrEmpty(editableString))
                                        {
                                            editableString = (str as MenuItemBusinessClass).EditModeItem;// +"," + (str as MenuItemBusinessClass).ShortcutKey;
                                            if (!string.IsNullOrEmpty((str as MenuItemBusinessClass).ShortcutKey))
                                            {
                                                editableString = editableString + "," + (str as MenuItemBusinessClass).ShortcutKey;
                                            }
                                        }
                                        else
                                        {
                                            editableString = editableString + "\n" + (str as MenuItemBusinessClass).EditModeItem;//  +"," + (str as MenuItemBusinessClass).ShortcutKey;
                                            if (!string.IsNullOrEmpty((str as MenuItemBusinessClass).ShortcutKey))
                                            {
                                                editableString = editableString + "," + (str as MenuItemBusinessClass).ShortcutKey;
                                            }
                                        }
                                    }
                                    (node as INodeVM).Label = editableString;
                                    _mAnnotation.Content = editableString;
                                }
                            }
                            #endregion
                            #region accordion
                            else if ((node as INodeVM).Shape.Equals("accordion"))
                            {
                                if ((s as LabelVM).Mode == ContentEditorMode.Edit)
                                {
                                    string localAnnotation = string.Empty;
                                    //_mAnnotation.Content = string.Empty;
                                    foreach (var i in ((node as INodeVM).DataContext as AccordionBusinessObject).Items)
                                    {
                                        if (string.IsNullOrEmpty(localAnnotation.ToString()))
                                        {
                                            localAnnotation = (i as AccordionItemBusinessObject).Item;
                                            if ((i as AccordionItemBusinessObject).Items != null && (i as AccordionItemBusinessObject).Items.Count > 0)
                                            {
                                                foreach (var subitem in (i as AccordionItemBusinessObject).Items)
                                                {
                                                    localAnnotation = localAnnotation.ToString() + "\n" + "- " + (subitem as AccordionSubItemBusinessObject).Item;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            localAnnotation = localAnnotation.ToString() + "\n" + (i as AccordionItemBusinessObject).Item;
                                            if ((i as AccordionItemBusinessObject).Items != null && (i as AccordionItemBusinessObject).Items.Count > 0)
                                            {
                                                foreach (var subitem in (i as AccordionItemBusinessObject).Items)
                                                {
                                                    localAnnotation = localAnnotation.ToString() + "\n" + "- " + (subitem as AccordionSubItemBusinessObject).Item;
                                                }
                                            }
                                        }
                                    }

                                    //_mAnnotation.Content = localAnnotation;
                                    (node as INodeVM).Label = localAnnotation;
                                }
                            }
                            #endregion
                        }
                    }
                    else if (e.PropertyName == "Content")
                    {
                        #region multilinebutton
                        if ((node as INodeVM).Shape.Equals("multilinebutton"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            string[] words = node.Label.Split('\n');
                            ((s as LabelVM).DataContext as MultiLineButtonBusinessObject).Title = string.Empty;
                            ((s as LabelVM).DataContext as MultiLineButtonBusinessObject).Message = string.Empty;
                            for (int x = 0; x < words.Count(); x++)
                            {
                                if (x == 0)
                                {
                                    ((s as LabelVM).DataContext as MultiLineButtonBusinessObject).Title = words[0];
                                }
                                else
                                {
                                    ((s as LabelVM).DataContext as MultiLineButtonBusinessObject).Message = ((s as LabelVM).DataContext as MultiLineButtonBusinessObject).Message + words[x];
                                }
                            }
                        }
                        #endregion
                        #region menubar
                        else if ((node as INodeVM).Shape.Equals("menubar"))
                        {
                            string[] words = (s as LabelVM).Content.ToString().Split(',');
                            #region For HyperLinks
                            Dictionary<string, int> links = new Dictionary<string, int>();
                            for (int i = 0; i < ((s as LabelVM).DataContext as MenuBarBusinessClass).Items.Count; i++)
                            {
                                if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as MenuBarBusinessClass).Items[i] as MenuTitleBusinessClass).Link.Link))
                                {
                                    links.Add((((s as LabelVM).DataContext as MenuBarBusinessClass).Items[i] as MenuTitleBusinessClass).Link.Link, i);
                                }
                            }
                            Dictionary<string, int> templinks = new Dictionary<string, int>();
                            int x = 0;
                            foreach (var i in links)
                            {
                                if (words.Contains((((s as LabelVM).DataContext as MenuBarBusinessClass).Items[i.Value] as MenuTitleBusinessClass).Item))
                                {
                                    int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as MenuBarBusinessClass).Items[i.Value] as MenuTitleBusinessClass).Item);
                                    templinks.Add(i.Key, mm);
                                }
                                else
                                    templinks.Add(i.Key, i.Value);
                                x++;
                            }
                            links = null;
                            #endregion
                            double textwidth = 0;
                            ((s as LabelVM).DataContext as MenuBarBusinessClass).Items.Clear();
                            foreach (string str in words)
                            {
                                ((s as LabelVM).DataContext as MenuBarBusinessClass).Items.Add(new MenuTitleBusinessClass() { Item = str });
                                Size textsize = FindDummyTextSize(_mAnnotation, str, TextWrapping.NoWrap);
                                textwidth = textwidth + textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                            }
                            ((s as LabelVM).DataContext as MenuBarBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as MenuBarBusinessClass).SelectedIndex;

                            #region For HyperLinks
                            foreach (var i in templinks)
                            {
                                (((s as LabelVM).DataContext as MenuBarBusinessClass).Items[i.Value] as MenuTitleBusinessClass).Link = new HyperlinkBusinessClass()
                                {
                                    Title = (((s as LabelVM).DataContext as MenuBarBusinessClass).Items[i.Value] as MenuTitleBusinessClass).Item,
                                    Link = i.Key
                                };
                            }
                            templinks = null;
                            #endregion

                            if (textwidth > (node as INodeVM).UnitWidth)
                                (node as INodeVM).UnitWidth = textwidth;

                            (node as INodeVM).Label = (s as LabelVM).Content.ToString();
                        }
                        #endregion
                        #region menu
                        else if ((node as INodeVM).Shape.Equals("menu"))
                        {
                            if ((s as LabelVM).Mode == ContentEditorMode.View)
                            {
                                (node as INodeVM).Label = (s as LabelVM).Content.ToString();
                                string[] words = (s as LabelVM).Content.ToString().Split('\n');

                                Dictionary<int, string> remover = new Dictionary<int, string>();
                                for (int i = 0; i < words.Count(); i++)
                                {
                                    if (words[i].Contains("\r"))
                                        remover.Add(i, words[i].Remove(words[i].Length - 1, 1));
                                }
                                foreach (var i in remover)
                                {
                                    words[i.Key] = i.Value;
                                }
                                remover = null;

                                #region For HyperLinks
                                Dictionary<string, int> links = new Dictionary<string, int>();
                                for (int i = 0; i < ((s as LabelVM).DataContext as MenuBusinessClass).Items.Count; i++)
                                {
                                    if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as MenuBusinessClass).Items[i] as MenuItemBusinessClass).Link.Link))
                                    {
                                        links.Add((((s as LabelVM).DataContext as MenuBusinessClass).Items[i] as MenuItemBusinessClass).Link.Link, i);
                                    }
                                }
                                Dictionary<string, int> templinks = new Dictionary<string, int>();
                                int x = 0;
                                foreach (var i in links)
                                {
                                    if (words.Contains((((s as LabelVM).DataContext as MenuBusinessClass).Items[i.Value] as MenuItemBusinessClass).Item))
                                    {
                                        int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as MenuBusinessClass).Items[i.Value] as MenuItemBusinessClass).Item);
                                        templinks.Add(i.Key, mm);
                                    }
                                    else
                                        templinks.Add(i.Key, i.Value);
                                    x++;
                                }
                                links = null;
                                #endregion

                                ((s as LabelVM).DataContext as MenuBusinessClass).Items.Clear();
                                foreach (string str in words)
                                {
                                    string[] childsplit = str.Split(',');
                                    var item = new MenuItemBusinessClass() { Item = childsplit[0], Foreground = (s as LabelVM).LabelForeground };//, ShortcutKey=childsplit[1], Foreground = (s as LabelVM).LabelForeground };

                                    if (childsplit.Count() > 1)
                                    {
                                        item.ShortcutKey = childsplit[1];
                                    }
                                    ((s as LabelVM).DataContext as MenuBusinessClass).Items.Add(item);
                                    int indexOf = childsplit[0].IndexOfAny(_mCharacters);
                                    if (indexOf == -1)
                                    {
                                        item.BreakLine = Visibility.Visible;
                                    }
                                }

                                #region For HyperLinks
                                foreach (var i in templinks)
                                {
                                    (((s as LabelVM).DataContext as MenuBusinessClass).Items[i.Value] as MenuItemBusinessClass).Link = new HyperlinkBusinessClass()
                                    {
                                        Title = (((s as LabelVM).DataContext as MenuBusinessClass).Items[i.Value] as MenuItemBusinessClass).Item,
                                        Link = i.Key
                                    };
                                }
                                templinks = null;
                                #endregion

                                ((s as LabelVM).DataContext as MenuBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as MenuBusinessClass).SelectedIndex;
                                Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                                node.UnitHeight = (_mAnnotation.DataContext as MenuBusinessClass).Items.Count() * (textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom);
                            }
                        }
                        #endregion
                        #region tabsbar
                        else if ((node as INodeVM).Shape.Equals("tabsbar"))
                        {
                            string[] words = (s as LabelVM).Content.ToString().Split(',');
                            #region For HyperLinks
                            Dictionary<string, int> links = new Dictionary<string, int>();
                            for (int i = 0; i < ((s as LabelVM).DataContext as TabBusinessClass).Items.Count; i++)
                            {
                                if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as TabBusinessClass).Items[i] as TabItemBusinessClass).Link.Link))
                                {
                                    links.Add((((s as LabelVM).DataContext as TabBusinessClass).Items[i] as TabItemBusinessClass).Link.Link, i);
                                }
                            }
                            Dictionary<string, int> templinks = new Dictionary<string, int>();
                            int x = 0;
                            foreach (var i in links)
                            {
                                if (words.Contains((((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Item))
                                {
                                    int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Item);
                                    templinks.Add(i.Key, mm);
                                }
                                else
                                    templinks.Add(i.Key, i.Value);
                                x++;
                            }
                            links = null;
                            #endregion
                            ((s as LabelVM).DataContext as TabBusinessClass).Items.Clear();

                            foreach (string str in words)
                            {
                                (((s as LabelVM).DataContext as TabBusinessClass).Items as ObservableCollection<object>).Add(new TabItemBusinessClass() { Item = str, Orientation = Orientation.Horizontal });
                            }

                            ((s as LabelVM).DataContext as TabBusinessClass).Background = (node as INodeVM).Fill;
                            ((s as LabelVM).DataContext as TabBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as TabBusinessClass).SelectedIndex;
                            #region For HyperLinks
                            foreach (var i in templinks)
                            {
                                (((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Link = new HyperlinkBusinessClass()
                                {
                                    Title = (((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Item,
                                    Link = i.Key
                                };
                                if((this.SelectedItems as SelectorVM).LinkedPages.Contains((((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Link.Title))
                                {
                                    (((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Link.InternalLink = true;
                                }
                                else
	                            {
                                    (((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Link.ExternalLink=true;
	                            }
                            }
                            templinks = null;
                            #endregion
                            

                            (((s as LabelVM).DataContext as TabBusinessClass).Items[((s as LabelVM).DataContext as TabBusinessClass).Items.Count() - 1] as TabItemBusinessClass).IsLastItem = true;
                            (node as INodeVM).Label = (s as LabelVM).Content.ToString();
                        }
                        #endregion
                        #region verticaltab
                        else if ((node as INodeVM).Shape.Equals("verticaltab"))
                        {
                            string[] words = (s as LabelVM).Content.ToString().Split(',');

                            #region For HyperLinks
                            Dictionary<string, int> links = new Dictionary<string, int>();
                            for (int i = 0; i < ((s as LabelVM).DataContext as TabBusinessClass).Items.Count; i++)
                            {
                                if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as TabBusinessClass).Items[i] as TabItemBusinessClass).Link.Link))
                                {
                                    links.Add((((s as LabelVM).DataContext as TabBusinessClass).Items[i] as TabItemBusinessClass).Link.Link, i);
                                }
                            }
                            Dictionary<string, int> templinks = new Dictionary<string, int>();
                            int x = 0;
                            foreach (var i in links)
                            {
                                if (words.Contains((((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Item))
                                {
                                    int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Item);
                                    templinks.Add(i.Key, mm);
                                }
                                else
                                    templinks.Add(i.Key, i.Value);
                                x++;
                            }
                            links = null;
                            #endregion

                            ((s as LabelVM).DataContext as TabBusinessClass).Items.Clear();

                            foreach (string str in words)
                            {
                                (((s as LabelVM).DataContext as TabBusinessClass).Items as ObservableCollection<object>).Add(new TabItemBusinessClass() { Item = str, Orientation = Orientation.Vertical });
                            }

                            ((s as LabelVM).DataContext as TabBusinessClass).Background =node.Fill;
                            ((s as LabelVM).DataContext as TabBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as TabBusinessClass).SelectedIndex;
                            
                            #region For HyperLinks
                            foreach (var i in templinks)
                            {
                                (((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Link = new HyperlinkBusinessClass()
                                {
                                    Title = (((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Item,
                                    Link = i.Key
                                };
                                if ((this.SelectedItems as SelectorVM).LinkedPages.Contains((((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Link.Title))
                                {
                                    (((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Link.InternalLink = true;
                                }
                                else
                                {
                                    (((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Link.ExternalLink = true;
                                }
                            }
                            templinks = null;
                            #endregion

                            (((s as LabelVM).DataContext as TabBusinessClass).Items[((s as LabelVM).DataContext as TabBusinessClass).Items.Count() - 1] as TabItemBusinessClass).IsLastItem = true;

                            (node as INodeVM).Label = (s as LabelVM).Content.ToString();
                        }
                        #endregion
                        #region buttonbar
                        else if ((node as INodeVM).Shape.Equals("buttonbar"))
                        {
                            string[] words = (s as LabelVM).Content.ToString().Split(',');

                            #region For HyperLinks
                            Dictionary<string, int> links = new Dictionary<string, int>();
                            for (int i = 0; i < ((s as LabelVM).DataContext as ButtonBarBusinessClass).Items.Count; i++)
                            {
                                if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i] as ButtonBarItemBusinessClass).Link.Link))
                                {
                                    links.Add((((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i] as ButtonBarItemBusinessClass).Link.Link, i);
                                }
                            }
                            Dictionary<string, int> templinks = new Dictionary<string, int>();
                            int x = 0;
                            foreach (var i in links)
                            {
                                if (words.Contains((((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i.Value] as ButtonBarItemBusinessClass).Item))
                                {
                                    int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i.Value] as ButtonBarItemBusinessClass).Item);
                                    templinks.Add(i.Key, mm);
                                }
                                else
                                    templinks.Add(i.Key, i.Value);
                                x++;
                            }
                            links = null;
                            #endregion

                            ((s as LabelVM).DataContext as ButtonBarBusinessClass).Items.Clear();
                            foreach (string str in words)
                            {
                                (((s as LabelVM).DataContext as ButtonBarBusinessClass).Items as ObservableCollection<object>).Add(
                                    new ButtonBarItemBusinessClass()
                                    {
                                        Item = str,
                                        Selected = false,
                                        Width = FindDummyTextSize(_mAnnotation, str, TextWrapping.NoWrap).Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right
                                    });
                            }
                            Size buttonbarsize = new Size();
                            foreach (var item in ((s as LabelVM).DataContext as ButtonBarBusinessClass).Items)
                            {
                                buttonbarsize.Width = buttonbarsize.Width + (item as ButtonBarItemBusinessClass).Width;
                            }
                            buttonbarsize.Height = FindDummyTextSize(_mAnnotation, (s as LabelVM).Content.ToString(), TextWrapping.NoWrap).Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            node.UnitWidth = buttonbarsize.Width;
                            node.UnitHeight = buttonbarsize.Height;
                            ((s as LabelVM).DataContext as ButtonBarBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as ButtonBarBusinessClass).SelectedIndex;

                            #region For HyperLinks
                            foreach (var i in templinks)
                            {
                                (((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i.Value] as ButtonBarItemBusinessClass).Link = new HyperlinkBusinessClass()
                                {
                                    Title = (((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i.Value] as ButtonBarItemBusinessClass).Item,
                                    Link = i.Key
                                };
                                if ((this.SelectedItems as SelectorVM).LinkedPages.Contains((((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i.Value] as ButtonBarItemBusinessClass).Link.Title))
                                {
                                    (((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i.Value] as ButtonBarItemBusinessClass).Link.InternalLink = true;
                                }
                                else
                                {
                                    (((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i.Value] as ButtonBarItemBusinessClass).Link.ExternalLink = true;
                                }
                            }
                            templinks = null;
                            #endregion

                            node.Label = (s as LabelVM).Content.ToString();
                        }
                        #endregion
                        #region searchbutton
                        else if ((node as INodeVM).Shape.Equals("searchbutton"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            Size textsize = FindDummyTextSize(s as LabelVM, (s as LabelVM).Content.ToString(), TextWrapping.NoWrap);
                            //if (UnitWidth < textsize.Width)
                            {
                                // Here we have added FontSize also. This search symbol width
                                node.UnitWidth = textsize.Width + (s as LabelVM).FontSize + (s as LabelVM).LabelMargin.Left + (2 * (s as LabelVM).LabelMargin.Right);
                            }
                            //if (UnitHeight < textsize.Height)
                            node.UnitHeight = textsize.Height + (s as LabelVM).LabelMargin.Top + (s as LabelVM).LabelMargin.Bottom;
                        }
                        #endregion
                        #region browserWindow
                        else if ((node as INodeVM).Shape.Equals("browserWindow"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            string[] words = (s as LabelVM).Content.ToString().Split('\n');
                            if (words.Count() > 0)
                            {
                                if (!string.IsNullOrEmpty(words[0]))
                                    (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteTitle = words[0];
                                else
                                    (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteTitle = string.Empty;

                                if (!string.IsNullOrEmpty(words[1]))
                                    (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteURL = words[1];
                                else
                                    (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteURL = string.Empty;
                            }
                            else
                            {
                                (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteTitle = string.Empty;
                                (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteURL = string.Empty;
                            }
                        }
                        #endregion
                        #region checkbox
                        else if ((node as INodeVM).Shape.Equals("checkbox") || (node as INodeVM).Shape.Equals("radiobutton"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region datechooser
                        else if ((node as INodeVM).Shape.Equals("datechooser"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            textsize.Width = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;

                            node.UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region windowbox
                        else if ((node as INodeVM).Shape.Equals("windowbox"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                        }
                        #endregion
                        #region button
                        else if ((node as INodeVM).Shape.Equals("button"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region numericstepper
                        else if ((node as INodeVM).Shape.Equals("numericstepper"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            node.UnitWidth = node.UnitHeight + textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                        }
                        #endregion
                        #region messagebox
                        else if ((node as INodeVM).Shape.Equals("messagebox"))
                        {
                            string[] words = (s as LabelVM).Content.ToString().Split('\n');
                            if (words.Count() > 0 && !string.IsNullOrEmpty(words[0]))
                                ((s as LabelVM).DataContext as MessageBoxBusinessObject).Title = string.IsNullOrEmpty(words[0]) ? string.Empty : words[0];
                            else
                                ((s as LabelVM).DataContext as MessageBoxBusinessObject).Title = string.Empty;

                            if (words.Count() > 1 && !string.IsNullOrEmpty(words[1]))
                                ((s as LabelVM).DataContext as MessageBoxBusinessObject).Message = string.IsNullOrEmpty(words[1]) ? string.Empty : words[1];
                            else
                                ((s as LabelVM).DataContext as MessageBoxBusinessObject).Message = string.Empty;

                            if (words.Count() == 3 && !string.IsNullOrEmpty(words[2]))
                            {
                                ((s as LabelVM).DataContext as MessageBoxBusinessObject).Ok = string.IsNullOrEmpty(words[2]) ? string.Empty : words[2];
                            }
                            else
                            {
                                ((s as LabelVM).DataContext as MessageBoxBusinessObject).Ok = string.Empty;
                            }
                            node.Label = (s as LabelVM).Content.ToString();
                        }
                         #endregion
                        #region tooltip
                        else if ((node as INodeVM).Shape.Equals("tooltip"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                        }
                        #endregion
                        #region alertbox
                        else if ((node as INodeVM).Shape.Equals("alertbox"))
                        {
                            string[] words = (s as LabelVM).Content.ToString().Split('\n');

                            if (words.Count() > 0 && !string.IsNullOrEmpty(words[0]))
                                ((s as LabelVM).DataContext as DialogBoxBusinessObject).Title = string.IsNullOrEmpty(words[0]) ? string.Empty : words[0];
                            else
                                ((s as LabelVM).DataContext as DialogBoxBusinessObject).Title = string.Empty;

                            if (words.Count() > 1 && !string.IsNullOrEmpty(words[1]))
                                ((s as LabelVM).DataContext as DialogBoxBusinessObject).Message = string.IsNullOrEmpty(words[1]) ? string.Empty : words[1];
                            else
                                ((s as LabelVM).DataContext as DialogBoxBusinessObject).Message = string.Empty;

                            if (node.Shape.Equals("alertbox") || node.Shape.Equals("dialogbox"))
                            {
                                if (words.Count() == 3 && !string.IsNullOrEmpty(words[2]))
                                {
                                    string[] okcancel = words[2].ToString().Split(',');
                                    ((s as LabelVM).DataContext as DialogBoxBusinessObject).Cancel = string.IsNullOrEmpty(okcancel[0]) ? string.Empty : okcancel[0];
                                    if (okcancel.Count() > 1)
                                        ((s as LabelVM).DataContext as DialogBoxBusinessObject).Ok = string.IsNullOrEmpty(okcancel[1]) ? string.Empty : okcancel[1];
                                    else
                                        ((s as LabelVM).DataContext as DialogBoxBusinessObject).Ok = string.Empty;
                                }
                                else
                                {
                                    ((s as LabelVM).DataContext as DialogBoxBusinessObject).Cancel = string.Empty;
                                    ((s as LabelVM).DataContext as DialogBoxBusinessObject).Ok = string.Empty;
                                }
                            }
                            node.Label = (s as LabelVM).Content.ToString();
                        }
                        #endregion
                        #region combobox
                        else if ((node as INodeVM).Shape.Equals("combobox"))
                        {
                            ((s as LabelVM).DataContext as ComboBoxBusinessClass).Items.Clear();
                            string[] words = (s as LabelVM).Content.ToString().Split('\n');
                            Size textsize = new Size();
                            for (int i = 0; i < words.Count(); i++)
                            {
                                if (i == 0)
                                    ((s as LabelVM).DataContext as ComboBoxBusinessClass).Title = words[i];
                                else
                                    ((s as LabelVM).DataContext as ComboBoxBusinessClass).Items.Add(new ComboBoxItemBusinessObject { Item = words[i] });

                                Size localsize = FindDummyTextSize(_mAnnotation, words[i], TextWrapping.NoWrap);
                                //if (textsize.Width < localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right)
                                //    textsize.Width = localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;

                                textsize.Height += localsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;

                                (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight = localsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            }
                            textsize.Width = FindDummyTextSize(_mAnnotation, ((s as LabelVM).DataContext as ComboBoxBusinessClass).Title, TextWrapping.NoWrap).Width + (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                            node.Label = (s as LabelVM).Content.ToString();
                        }
                        #endregion
                        #region horizontalcurlybraces / verticalcurlybraces / stickynote / fieldset / popover / textarea
                        else if ((node as INodeVM).Shape.Equals("horizontalcurlybraces") || (node as INodeVM).Shape.Equals("verticalcurlybraces") || (node as INodeVM).Shape.Equals("stickynote") || (node as INodeVM).Shape.Equals("fieldset") || (node as INodeVM).Shape.Equals("popover") || (node as INodeVM).Shape.Equals("textarea") || (node as INodeVM).Shape.Equals("geometricshapes"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                        }
                        #endregion
                        #region subtitle
                        else if ((node as INodeVM).Shape.Equals("subtitle"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                        }
                        #endregion
                        #region checkboxgroup
                        else if ((node as INodeVM).Shape.Equals("checkboxgroup"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            string[] words = (s as LabelVM).Content.ToString().Split('\n');
                            Size textsize = new Size();
                            Dictionary<string, int> remover = new Dictionary<string, int>();
                            for (int i = 0; i < words.Count(); i++)
                            {
                                if (words[i].Contains("\r"))
                                    remover.Add(words[i].Remove(words[i].Length - 1, 1), i);
                            }
                            foreach (var i in remover)
                            {
                                words[i.Value] = i.Key;
                            }
                            remover = null;

                            #region For HyperLinks
                            Dictionary<string, int> links = new Dictionary<string, int>();
                            for (int i = 0; i < ((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items.Count; i++)
                            {
                                if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i] as CheckBoxItemBusinessClass).Link.Link))
                                {
                                    links.Add((((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i] as CheckBoxItemBusinessClass).Link.Link, i);
                                }
                            }
                            Dictionary<string, int> templinks = new Dictionary<string, int>();
                            int x = 0;
                            foreach (var i in links)
                            {
                                if (words.Contains((((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i.Value] as CheckBoxItemBusinessClass).Item))
                                {
                                    int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i.Value] as CheckBoxItemBusinessClass).Item);
                                    templinks.Add(i.Key, mm);
                                }
                                else
                                    templinks.Add(i.Key, i.Value);
                                x++;
                            }
                            links = null;
                            #endregion

                            ((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items.Clear();
                            foreach (string str in words)
                            {
                                (((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items as ObservableCollection<object>).Add(new CheckBoxItemBusinessClass()
                                {
                                    Item = str,
                                    Stroke = ((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Stroke,
                                    Foreground = ((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Foreground
                                });
                                Size localsize = FindDummyTextSize(_mAnnotation, str, TextWrapping.NoWrap);
                                if (localsize.Width > textsize.Width)
                                    textsize.Width = localsize.Width;
                            }
                            (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            node.UnitHeight = (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight * (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).Items.Count();
                            // Added FontSize also becuase checkbox is in front 
                            node.UnitWidth = textsize.Width + _mAnnotation.FontSize;
                            #region For HyperLinks
                            foreach (var i in templinks)
                            {
                                (((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i.Value] as CheckBoxItemBusinessClass).Link = new HyperlinkBusinessClass()
                                {
                                    Title = (((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i.Value] as CheckBoxItemBusinessClass).Item,
                                    Link = i.Key
                                    
                                };
                                if ((this.SelectedItems as SelectorVM).LinkedPages.Contains((((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i.Value] as CheckBoxItemBusinessClass).Link.Title))
                                {
                                    (((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i.Value] as CheckBoxItemBusinessClass).Link.InternalLink = true;
                                }
                                else
                                {
                                    (((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i.Value] as CheckBoxItemBusinessClass).Link.ExternalLink = true;
                                }                                
                            }
                            templinks = null;
                            #endregion
                        }
                        #endregion
                        #region radiobuttongroup
                        else if ((node as INodeVM).Shape.Equals("radiobuttongroup"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            string[] words = (s as LabelVM).Content.ToString().Split('\n');
                            Size textsize = new Size();

                            Dictionary<string, int> remover = new Dictionary<string, int>();
                            for (int i = 0; i < words.Count(); i++)
                            {
                                if (words[i].Contains("\r"))
                                    remover.Add(words[i].Remove(words[i].Length - 1, 1), i);
                            }
                            foreach (var i in remover)
                            {
                                words[i.Value] = i.Key;
                            }
                            remover = null;

                            #region For HyperLinks
                            Dictionary<string, int> links = new Dictionary<string, int>();
                            for (int i = 0; i < ((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items.Count; i++)
                            {
                                if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items[i] as RadioButtonItemBusinessClass).Link.Link))
                                {
                                    links.Add((((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items[i] as RadioButtonItemBusinessClass).Link.Link, i);
                                }
                            }
                            Dictionary<string, int> templinks = new Dictionary<string, int>();
                            int x = 0;
                            foreach (var i in links)
                            {
                                if (words.Contains((((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items[i.Value] as RadioButtonItemBusinessClass).Item))
                                {
                                    int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items[i.Value] as RadioButtonItemBusinessClass).Item);
                                    templinks.Add(i.Key, mm);
                                }
                                else
                                    templinks.Add(i.Key, i.Value);
                                x++;
                            }
                            links = null;
                            #endregion

                            ((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items.Clear();
                            foreach (string str in words)
                            {
                                (((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items as ObservableCollection<object>).Add(new RadioButtonItemBusinessClass()
                                {
                                    Item = str,
                                    Stroke = ((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Stroke,
                                    Foreground = ((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Foreground
                                });
                                Size localsize = FindDummyTextSize(_mAnnotation, str, TextWrapping.NoWrap);
                                if (localsize.Width > textsize.Width)
                                    textsize.Width = localsize.Width;
                            }
                            (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            node.UnitHeight = (_mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom) * (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).Items.Count();
                            // Added FontSize also becuase checkbox is in front 
                            node.UnitWidth = textsize.Width + _mAnnotation.FontSize;

                            #region For HyperLinks
                            foreach (var i in templinks)
                            {
                                (((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items[i.Value] as RadioButtonItemBusinessClass).Link = new HyperlinkBusinessClass()
                                {
                                    Title = (((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items[i.Value] as RadioButtonItemBusinessClass).Item,
                                    Link = i.Key

                                };
                                if ((this.SelectedItems as SelectorVM).LinkedPages.Contains((((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items[i.Value] as RadioButtonItemBusinessClass).Link.Title))
                                {
                                    (((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items[i.Value] as RadioButtonItemBusinessClass).Link.InternalLink = true;
                                }
                                else
                                {
                                    (((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items[i.Value] as RadioButtonItemBusinessClass).Link.ExternalLink = true;
                                }
                            }
                            templinks = null;
                            #endregion
                        }
                        #endregion
                        #region pointybutton
                        else if ((node as INodeVM).Shape.Equals("pointybutton"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Left + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Right;
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region bigtitle
                        else if ((node as INodeVM).Shape.Equals("bigtitle"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                        }
                        #endregion
                        #region link
                        else if ((node as INodeVM).Shape.Equals("link"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            Size textsize = FindDummyTextSize(s as LabelVM, (s as LabelVM).Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                        }
                        #endregion
                        #region linkbar
                        else if ((node as INodeVM).Shape.Equals("linkbar"))
                        {
                            Size textsize = new Size();
                            string[] words = (s as LabelVM).Content.ToString().Split(',');
                            #region For HyperLinks
                            Dictionary<string, int> links = new Dictionary<string, int>();
                            for (int i = 0; i < ((s as LabelVM).DataContext as LinkBarBusinessClass).Items.Count; i++)
                            {
                                if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i] as LinkBarItemBusinessClass).Link.Link))
                                {
                                    links.Add((((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i] as LinkBarItemBusinessClass).Link.Link, i);
                                }
                            }
                            Dictionary<string, int> templinks = new Dictionary<string, int>();
                            int x = 0;
                            foreach (var i in links)
                            {
                                if (words.Contains((((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i.Value] as LinkBarItemBusinessClass).Item))
                                {
                                    int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i.Value] as LinkBarItemBusinessClass).Item);
                                    templinks.Add(i.Key, mm);
                                }
                                else
                                    templinks.Add(i.Key, i.Value);
                                x++;
                            }
                            links = null;
                            #endregion
                            //((s as LabelVM).DataContext as LinkBarBusinessClass).Items.Clear();

                            for (int i = 0; i < words.Length;i++)
                            {
                                if(i < ((s as LabelVM).DataContext as LinkBarBusinessClass).Items.Count)
                                {
                                    (((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i] as LinkBarItemBusinessClass).Item = words[i];
                                    (((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i] as LinkBarItemBusinessClass).IsLastItem = false;
                                }
                                else
                                {
                                    ((s as LabelVM).DataContext as LinkBarBusinessClass).Items.Add
                                    (
                                         new LinkBarItemBusinessClass() { Item = words[i] }
                                    );
                                }
                                Size localsize = FindDummyTextSize(_mAnnotation, words[i], TextWrapping.NoWrap);
                                textsize.Width = textsize.Width + localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                textsize.Height = localsize.Height;
                            }

                            (((s as LabelVM).DataContext as LinkBarBusinessClass).Items[((s as LabelVM).DataContext as LinkBarBusinessClass).Items.Count - 1] as LinkBarItemBusinessClass).IsLastItem = true;
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                            ((s as LabelVM).DataContext as LinkBarBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as LinkBarBusinessClass).SelectedIndex;
                            #region For HyperLinks
                            foreach (var i in templinks)
                            {
                                (((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i.Value] as LinkBarItemBusinessClass).Link = new HyperlinkBusinessClass()
                                {
                                    Title = (((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i.Value] as LinkBarItemBusinessClass).Item,
                                    Link = i.Key
                                };
                            }
                            templinks = null;
                            #endregion
                            node.Label = (s as LabelVM).Content.ToString();
                        }
                        #endregion
                        #region breadcrumbs
                        else if ((node as INodeVM).Shape.Equals("breadcrumbs"))
                        {
                            string[] words = (s as LabelVM).Content.ToString().Split(',');
                            Size textsize = new Size();
                            #region For HyperLinks
                            Dictionary<string, int> links = new Dictionary<string, int>();
                            for (int i = 0; i < ((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items.Count; i++)
                            {
                                if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[i] as BreadcrumbsItemBusinessClass).Link.Link))
                                {
                                    links.Add((((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[i] as BreadcrumbsItemBusinessClass).Link.Link, i);
                                }
                            }
                            Dictionary<string, int> templinks = new Dictionary<string, int>();
                            int x = 0;
                            foreach (var i in links)
                            {
                                if (words.Contains((((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[i.Value] as BreadcrumbsItemBusinessClass).Item))
                                {
                                    int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[i.Value] as BreadcrumbsItemBusinessClass).Item);
                                    templinks.Add(i.Key, mm);
                                }
                                else
                                    templinks.Add(i.Key, i.Value);
                                x++;
                            }
                            links = null;
                            #endregion
                            (((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items).Clear();

                            foreach (string str in words)
                            {
                                ((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items.Add
                                (
                                    new BreadcrumbsItemBusinessClass() { Item = str }
                                );
                                Size localsize = FindDummyTextSize(_mAnnotation, str, TextWrapping.NoWrap);
                                textsize.Width = textsize.Width + localsize.Width + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconWidth + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                textsize.Height = localsize.Height + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconHeight;
                            }
                            #region For HyperLinks
                            foreach (var i in templinks)
                            {
                                (((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[i.Value] as BreadcrumbsItemBusinessClass).Link = new HyperlinkBusinessClass()
                                {
                                    Title = (((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[i.Value] as BreadcrumbsItemBusinessClass).Item,
                                    Link = i.Key
                                };
                            }
                            templinks = null;
                            #endregion
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                            (((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[words.Count() - 1] as BreadcrumbsItemBusinessClass).IsLastItem = true;
                            node.Label = (s as LabelVM).Content.ToString();
                        }
                        #endregion
                        #region list
                        else if ((node as INodeVM).Shape.Equals("list"))
                        {
                            string[] words = (s as LabelVM).Content.ToString().Split('\n');

                            #region For HyperLinks
                            Dictionary<string, int> links = new Dictionary<string, int>();
                            for (int i = 0; i < ((s as LabelVM).DataContext as ListBusinessClass).Items.Count; i++)
                            {
                                if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as ListBusinessClass).Items[i] as ListItemBusinessClass).Link.Link))
                                {
                                    links.Add((((s as LabelVM).DataContext as ListBusinessClass).Items[i] as ListItemBusinessClass).Link.Link, i);
                                }
                            }
                            Dictionary<string, int> templinks = new Dictionary<string, int>();
                            int x = 0;
                            foreach (var i in links)
                            {
                                if (words.Contains((((s as LabelVM).DataContext as ListBusinessClass).Items[i.Value] as ListItemBusinessClass).Item))
                                {
                                    int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as ListBusinessClass).Items[i.Value] as ListItemBusinessClass).Item);
                                    templinks.Add(i.Key, mm);
                                }
                                else
                                    templinks.Add(i.Key, i.Value);
                                x++;
                            }
                            links = null;
                            #endregion

                            ((s as LabelVM).DataContext as ListBusinessClass).DuplicateItems.Clear();
                            //(this.SelectedItems as SelectorVM).TabItem.Clear();

                            for (int i = 0; i < words.Length; i++)
                            {
                                if (i < ((s as LabelVM).DataContext as ListBusinessClass).Items.Count)
                                {
                                    (((s as LabelVM).DataContext as ListBusinessClass).Items[i] as ListItemBusinessClass).Item = words[i];
                                    //(((s as LabelVM).DataContext as ListBusinessClass).DuplicateItems[i] as ListItemBusinessClass).Item = words[i];

                                    var localItem = ((s as LabelVM).DataContext as ListBusinessClass).Items[i];
                                    ((s as LabelVM).DataContext as ListBusinessClass).DuplicateItems.Add(localItem);
                                }
                                else
                                {
                                    ((s as LabelVM).DataContext as ListBusinessClass).Items.Add(new ListItemBusinessClass()
                                    {
                                        Item = words[i]
                                    });
                                    ((s as LabelVM).DataContext as ListBusinessClass).DuplicateItems.Add(new ListItemBusinessClass()
                                    {
                                        Item = words[i]
                                    });
                                    if (i % 2 == 0)
                                    {
                                        (((s as LabelVM).DataContext as ListBusinessClass).Items[i] as ListItemBusinessClass).Background = ((s as LabelVM).DataContext as ListBusinessClass).EvenItemsBackgroud;
                                        (((s as LabelVM).DataContext as ListBusinessClass).DuplicateItems[i] as ListItemBusinessClass).Background = ((s as LabelVM).DataContext as ListBusinessClass).EvenItemsBackgroud;
                                    }
                                    else
                                    {
                                        (((s as LabelVM).DataContext as ListBusinessClass).Items[i] as ListItemBusinessClass).Background = ((s as LabelVM).DataContext as ListBusinessClass).OddItemsBackgroud;
                                        (((s as LabelVM).DataContext as ListBusinessClass).DuplicateItems[i] as ListItemBusinessClass).Background = ((s as LabelVM).DataContext as ListBusinessClass).OddItemsBackgroud;
                                    }

                                }
                            }

                            double rowscount = (node as NodeVM).UnitHeight / ((node as NodeVM).DataContext as ListBusinessClass).RowHeight;
                            bool isInt = rowscount == (int)rowscount;
                            if (!isInt)
                                rowscount += 1;

                            if (rowscount > ((s as LabelVM).DataContext as ListBusinessClass).Items.Count)
                            {
                                int count = ((node as NodeVM).DataContext as ListBusinessClass).Items.Count;
                                for (int i = 0; i < (int)rowscount - count; i++)
                                {
                                    if (((node as NodeVM).DataContext as ListBusinessClass).DuplicateItems.Count % 2 == 0)
                                        ((node as NodeVM).DataContext as ListBusinessClass).DuplicateItems.Add(new ListItemBusinessClass() { Background = ((node as NodeVM).DataContext as ListBusinessClass).EvenItemsBackgroud });
                                    else
                                        ((node as NodeVM).DataContext as ListBusinessClass).DuplicateItems.Add(new ListItemBusinessClass() { Background = ((node as NodeVM).DataContext as ListBusinessClass).OddItemsBackgroud });
                                }
                            }

                            ((s as LabelVM).DataContext as ListBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as ListBusinessClass).SelectedIndex;

                            #region For HyperLinks
                            foreach (var i in templinks)
                            {
                                (((s as LabelVM).DataContext as ListBusinessClass).Items[i.Value] as ListItemBusinessClass).Link = new HyperlinkBusinessClass()
                                {
                                    Title = (((s as LabelVM).DataContext as ListBusinessClass).Items[i.Value] as ListItemBusinessClass).Item,
                                    Link = i.Key
                                };
                            }
                            templinks = null;
                            #endregion
                            node.Label = (s as LabelVM).Content.ToString();

                        }
                        #endregion
                        #region textinput
                        else if ((node as INodeVM).Shape.Equals("textinput"))
                        {
                            Size textsize = new Size();
                            (node as INodeVM).Label = (s as LabelVM).Content.ToString();
                            textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            (node as INodeVM).UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                            (node as INodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region accordion
                        else if ((node as INodeVM).Shape.Equals("accordion"))
                        {
                            if ((s as LabelVM).Mode == ContentEditorMode.View)
                            {
                                Size textSize;
                                //Label = _mAnnotation.Content.ToString();
                                string[] words = (node as INodeVM).Label.Split('\n');
                                ((node as INodeVM).DataContext as AccordionBusinessObject).Items.Clear();
                                string localAnnotation = string.Empty;
                                for (int i = 0; i < words.Count(); i++)
                                {
                                    if (i == 0 && words[i] != "\t\r")
                                    {
                                        ((node as INodeVM).DataContext as AccordionBusinessObject).Items.Add(new AccordionItemBusinessObject() { Item = words[0], Index = 1 });
                                        textSize = FindDummyTextSize(_mAnnotation, words[0]);
                                    }
                                    else if (words[i] != "\t\r")
                                    {
                                        if (char.Equals(words[i][0], '-'))
                                        {
                                            if (char.IsWhiteSpace(words[i][1]))
                                            {
                                                words[i] = words[i].Remove(0, 2);

                                                int parentIndex = ((node as INodeVM).DataContext as AccordionBusinessObject).Items.Count();
                                                if ((((node as INodeVM).DataContext as AccordionBusinessObject).Items[parentIndex - 1] as AccordionItemBusinessObject).Items == null)
                                                {
                                                    (((node as INodeVM).DataContext as AccordionBusinessObject).Items[parentIndex - 1] as AccordionItemBusinessObject).Items = new ObservableCollection<object>();
                                                    (((node as INodeVM).DataContext as AccordionBusinessObject).Items[parentIndex - 1] as AccordionItemBusinessObject).Items.Add
                                                    (
                                                        new AccordionSubItemBusinessObject() { Item = words[i], ParentIndex = parentIndex }
                                                    );
                                                    double localWidth = FindDummyTextSize(_mAnnotation, words[i]).Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                                    if (textSize.Width < localWidth)
                                                        textSize.Width = localWidth;
                                                }
                                                else
                                                {
                                                    (((node as INodeVM).DataContext as AccordionBusinessObject).Items[parentIndex - 1] as AccordionItemBusinessObject).Items.Add
                                                    (
                                                        new AccordionSubItemBusinessObject() { Item = words[i], ParentIndex = parentIndex }
                                                    );
                                                    double localWidth = FindDummyTextSize(_mAnnotation, words[i]).Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                                    if (textSize.Width < localWidth)
                                                        textSize.Width = localWidth;
                                                }
                                            }
                                            else
                                            {
                                                int parentIndex = ((node as INodeVM).DataContext as AccordionBusinessObject).Items.Count();
                                                ((node as INodeVM).DataContext as AccordionBusinessObject).Items.Add(new AccordionItemBusinessObject() { Item = words[i], Index = parentIndex + 1 });
                                                double localWidth = FindDummyTextSize(_mAnnotation, words[i]).Width;
                                                if (textSize.Width < localWidth)
                                                    textSize.Width = localWidth;
                                            }
                                        }
                                        else
                                        {
                                            int parentIndex = ((node as INodeVM).DataContext as AccordionBusinessObject).Items.Count();
                                            ((node as INodeVM).DataContext as AccordionBusinessObject).Items.Add(new AccordionItemBusinessObject() { Item = words[i], Index = parentIndex + 1 });
                                            double localWidth = FindDummyTextSize(_mAnnotation, words[i]).Width;
                                            if (textSize.Width < localWidth)
                                                textSize.Width = localWidth;
                                        }

                                    }
                                }

                                foreach (var i in ((node as INodeVM).DataContext as AccordionBusinessObject).Items)
                                {
                                    //if (_mAnnotation.Content == null || string.IsNullOrEmpty(_mAnnotation.Content.ToString()))
                                    {
                                        //double height = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString()).Height;
                                        if ((i as AccordionItemBusinessObject).Items != null && (i as AccordionItemBusinessObject).Items.Count > 0)
                                        {
                                            (i as AccordionItemBusinessObject).Height = (((node as INodeVM).DataContext as AccordionBusinessObject).ItemTitleHeight * (i as AccordionItemBusinessObject).Items.Count) + (i as AccordionItemBusinessObject).BottomSpace;
                                        }
                                        else
                                            (i as AccordionItemBusinessObject).Height = (i as AccordionItemBusinessObject).BottomSpace;
                                    }
                                }

                                ((node as INodeVM).DataContext as AccordionBusinessObject).SelectedIndex = ((node as INodeVM).DataContext as AccordionBusinessObject).SelectedIndex;
                                if (((node as INodeVM).DataContext as AccordionBusinessObject).SelectedIndex == 0)
                                {
                                    //Size size = FindDummyTextSize(_mAnnotation, ((node.DataContext as AccordionBusinessObject).Items[0] as AccordionItemBusinessObject).Item);
                                    double height = ((node as INodeVM).DataContext as AccordionBusinessObject).Items.Count() * ((node as INodeVM).DataContext as AccordionBusinessObject).ItemTitleHeight;
                                    (node as INodeVM).UnitHeight = height;
                                }
                                else
                                {
                                    double height = (((node as INodeVM).DataContext as AccordionBusinessObject).Items.Count() * ((node as INodeVM).DataContext as AccordionBusinessObject).ItemTitleHeight) + (((node as INodeVM).DataContext as AccordionBusinessObject).Items[((node as INodeVM).DataContext as AccordionBusinessObject).SelectedIndex - 1] as AccordionItemBusinessObject).BottomSpace;
                                    double subItemsHeight = 0d;
                                    if ((((node as INodeVM).DataContext as AccordionBusinessObject).Items[((node as INodeVM).DataContext as AccordionBusinessObject).SelectedIndex - 1] as AccordionItemBusinessObject).Items != null)
                                    {
                                        subItemsHeight = (((node as INodeVM).DataContext as AccordionBusinessObject).Items[((node as INodeVM).DataContext as AccordionBusinessObject).SelectedIndex - 1] as AccordionItemBusinessObject).Items.Count() * ((node as INodeVM).DataContext as AccordionBusinessObject).ItemTitleHeight;
                                    }
                                    height += subItemsHeight;
                                    (node as INodeVM).UnitHeight = height;
                                }
                                (node as INodeVM).UnitWidth = textSize.Width;
                                (node as INodeVM).Label = _mAnnotation.Content.ToString();
                            }
                        }
                        #endregion
                    }
                    else if (e.PropertyName == "FontSize")
                    {
                        #region multilinebutton
                        if ((node as INodeVM).Shape.Equals("multilinebutton"))
                        {
                            (_mAnnotation.DataContext as MultiLineButtonBusinessObject).TitleHeight = FindDummyTextSize(_mAnnotation, (_mAnnotation.DataContext as MultiLineButtonBusinessObject).Title).Height + (_mAnnotation.DataContext as MultiLineButtonBusinessObject).TitleMargin.Top + (_mAnnotation.DataContext as MultiLineButtonBusinessObject).TitleMargin.Bottom;
                        }
                        #endregion
                        #region searchbutton
                        else if ((node as INodeVM).Shape.Equals("searchbutton"))
                        {
                            (_mAnnotation.DataContext as SearchBoxBusinessClass).CornerRadius = new CornerRadius(node.UnitHeight / 2);
                            Size textsize = FindDummyTextSize(s as LabelVM, (s as LabelVM).Content.ToString(), TextWrapping.NoWrap);
                            //if (UnitWidth < textsize.Width)
                            {
                                // Here we have added FontSize also. This search symbol width
                                node.UnitWidth = textsize.Width + (s as LabelVM).FontSize + (s as LabelVM).LabelMargin.Left + (2 * (s as LabelVM).LabelMargin.Right);
                            }
                            //if (UnitHeight < textsize.Height)
                            node.UnitHeight = textsize.Height + (s as LabelVM).LabelMargin.Top + (s as LabelVM).LabelMargin.Bottom;
                        }
                        #endregion
                        #region checkbox
                        else if ((node as INodeVM).Shape.Equals("checkbox") || (node as INodeVM).Shape.Equals("radiobutton"))
                        {
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region datechooser
                        else if ((node as INodeVM).Shape.Equals("datechooser"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            textsize.Width = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;
                            node.UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region button
                        else if ((node as INodeVM).Shape.Equals("button"))
                        {
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region numericstepper
                        else if ((node as INodeVM).Shape.Equals("numericstepper"))
                        {
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            node.UnitWidth = node.UnitHeight + textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                        }
                        #endregion
                        #region combobox
                        else if ((node as INodeVM).Shape.Equals("combobox"))
                        {
                            Size textsize = new Size();
                            textsize = FindDummyTextSize(_mAnnotation, (_mAnnotation.DataContext as ComboBoxBusinessClass).Title, TextWrapping.NoWrap);
                            (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            textsize.Width += (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                            textsize.Height += _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            foreach (var item in ((s as LabelVM).DataContext as ComboBoxBusinessClass).Items)
                            {
                                Size localsize = FindDummyTextSize(_mAnnotation, item.Item, TextWrapping.NoWrap);
                                //if (textsize.Width < localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right)
                                //    textsize.Width = localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                textsize.Height += localsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            }
                            textsize = new Size(FindDummyTextSize(_mAnnotation, ((s as LabelVM).DataContext as ComboBoxBusinessClass).Title, TextWrapping.NoWrap).Width + (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right, textsize.Height);
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                        }
                        #endregion
                        #region subtitle
                        else if ((node as INodeVM).Shape.Equals("subtitle"))
                        {
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                        }
                        #endregion
                        #region checkboxgroup
                        else if ((node as INodeVM).Shape.Equals("checkboxgroup"))
                        {
                            Size textsize = new Size();
                            foreach (var item in ((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items)
                            {
                                Size localsize = FindDummyTextSize(_mAnnotation, (item as CheckBoxItemBusinessClass).Item, TextWrapping.NoWrap);
                                if (localsize.Width > textsize.Width)
                                    textsize.Width = localsize.Width;
                            }
                            (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            node.UnitHeight = (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight * (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).Items.Count();
                            // Added FontSize also becuase checkbox is in front 
                            node.UnitWidth = textsize.Width + _mAnnotation.FontSize;
                        }
                        #endregion
                        #region pointybutton
                        else if ((node as INodeVM).Shape.Equals("pointybutton"))
                        {
                            (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconSize = node.FontSize / 2;
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Left + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Right;
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region bigtitle
                        else if ((node as INodeVM).Shape.Equals("bigtitle"))
                        {
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                        }
                        #endregion
                        #region link
                        else if ((node as INodeVM).Shape.Equals("link"))
                        {
                            Size textsize = FindDummyTextSize(s as LabelVM, (s as LabelVM).Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                        }
                        #endregion
                        #region linkbar
                        else if ((node as INodeVM).Shape.Equals("linkbar"))
                        {
                            Size textsize = new Size();
                            foreach (var str in ((s as LabelVM).DataContext as LinkBarBusinessClass).Items)
                            {
                                Size localsize = FindDummyTextSize(_mAnnotation, (str as LinkBarItemBusinessClass).Item, TextWrapping.NoWrap);
                                textsize.Width = textsize.Width + localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                textsize.Height = localsize.Height;
                            }
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                        }
                        #endregion
                        #region breadcrumbs
                        else if ((node as INodeVM).Shape.Equals("breadcrumbs"))
                        {
                            Size textsize = new Size();
                            foreach (var item in ((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items)
                            {
                                Size localsize = FindDummyTextSize(_mAnnotation, (item as BreadcrumbsItemBusinessClass).Item, TextWrapping.NoWrap);
                                textsize.Width = textsize.Width + localsize.Width + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconWidth + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                textsize.Height = localsize.Height + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconHeight;
                            }
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                        }
                        #endregion
                        #region textinput
                        else if ((node as INodeVM).Shape.Equals("textinput"))
                        {
                            Size textsize = new Size();
                            textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            (node as INodeVM).UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                            (node as INodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region list
                        else if ((node as INodeVM).Shape.Equals("list"))
                        {
                            ((node as INodeVM).DataContext as ListBusinessClass).RowHeight = _mAnnotation.FontSize + 5;
                        }
                        #endregion
                        else if ((node as INodeVM).Shape.Equals("windowbox"))
                        {
                            ((node as INodeVM).DataContext as WindowBoxBusinessObject).TitleBarHeight = _mAnnotation.FontSize + ((node as INodeVM).DataContext as WindowBoxBusinessObject).TopIconHeight;
                            _mAnnotation.LabelHeight = ((node as INodeVM).DataContext as WindowBoxBusinessObject).TitleBarHeight;
                        }

                    }
                    else if (e.PropertyName == "FontWeight")
                    {
                        #region checkbox
                        if ((node as INodeVM).Shape.Equals("checkbox") || (node as INodeVM).Shape.Equals("radiobutton"))
                        {
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region datechooser
                        else if ((node as INodeVM).Shape.Equals("datechooser"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            textsize.Width = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;

                            node.UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region linkbar
                        else if ((node as INodeVM).Shape.Equals("linkbar"))
                        {
                            Size textsize = new Size();
                            foreach (var str in ((s as LabelVM).DataContext as LinkBarBusinessClass).Items)
                            {
                                Size localsize = FindDummyTextSize(_mAnnotation, (str as LinkBarItemBusinessClass).Item, TextWrapping.NoWrap);
                                textsize.Width = textsize.Width + localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                textsize.Height = localsize.Height;
                            }
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                        }
                        #endregion
                        #region breadcrumbs
                        else if ((node as INodeVM).Shape.Equals("breadcrumbs"))
                        {
                            Size textsize = new Size();
                            foreach (var item in ((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items)
                            {
                                Size localsize = FindDummyTextSize(_mAnnotation, (item as BreadcrumbsItemBusinessClass).Item, TextWrapping.NoWrap);
                                textsize.Width = textsize.Width + localsize.Width + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconWidth + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                textsize.Height = localsize.Height + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconHeight;
                            }
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                        }
                        #endregion
                        #region numericstepper
                        else if ((node as INodeVM).Shape.Equals("numericstepper"))
                        {
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            node.UnitWidth = node.UnitHeight + textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                        }
                        #endregion
                    }
                    else if (e.PropertyName == "Font")
                    {
                        #region checkbox
                        if ((node as INodeVM).Shape.Equals("checkbox") || (node as INodeVM).Shape.Equals("radiobutton"))
                        {
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        #endregion
                        #region searchbutton
                        else if ((node as INodeVM).Shape.Equals("searchbutton"))
                        {
                            node.Label = (s as LabelVM).Content.ToString();
                            Size textsize = FindDummyTextSize(s as LabelVM, (s as LabelVM).Content.ToString(), TextWrapping.NoWrap);
                            //if (UnitWidth < textsize.Width)
                            {
                                // Here we have added FontSize also. This search symbol width
                                node.UnitWidth = textsize.Width + (s as LabelVM).FontSize + (s as LabelVM).LabelMargin.Left + (2 * (s as LabelVM).LabelMargin.Right);
                            }
                            //if (UnitHeight < textsize.Height)
                            node.UnitHeight = textsize.Height + (s as LabelVM).LabelMargin.Top + (s as LabelVM).LabelMargin.Bottom;
                        }
                        #endregion
                        #region numericstepper
                        else if ((node as INodeVM).Shape.Equals("numericstepper"))
                        {
                            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                            node.UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                            node.UnitWidth = node.UnitHeight + textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                        }
                        else if ((node as INodeVM).Shape.Equals("linkbar"))
                        {
                            Size textsize = new Size();
                            foreach (var str in (_mAnnotation.DataContext as LinkBarBusinessClass).Items)
                            {
                                //if (_mAnnotation.Content == null)
                                //{
                                //    _mAnnotation.Content = (str as LinkBarItemBusinessClass).Item;
                                //    Size localsize = FindDummyTextSize(_mAnnotation, (str as LinkBarItemBusinessClass).Item, TextWrapping.NoWrap);
                                //    textsize.Width = textsize.Width + localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                //    textsize.Height = localsize.Height;
                                //}
                                //else
                                //{
                                //    _mAnnotation.Content = _mAnnotation.Content.ToString() + "," + (str as LinkBarItemBusinessClass).Item;
                                    
                                //}
                                Size localsize = FindDummyTextSize(_mAnnotation, (str as LinkBarItemBusinessClass).Item, TextWrapping.NoWrap);
                                textsize.Width = textsize.Width + localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                textsize.Height = localsize.Height + 5 ;
                            }
                            node.UnitWidth = textsize.Width;
                            node.UnitHeight = textsize.Height;
                        }
                        #endregion
                    }
                };
            }
        }
        void AnnotationDataContextPropertyChanged(INodeVM node)
        {
            foreach (LabelVM _mAnnotation in node.Annotations as List<IAnnotation>)
            {
                #region onoffswitch
                if (node.Shape.Equals("onoffswitch"))
                {
                    if(_mAnnotation.DataContext is OnOffSwitch)
                    {
                        (_mAnnotation.DataContext as OnOffSwitch).PropertyChanged += (s, e1) =>
                        {
                            if (e1.PropertyName == "On")
                            {
                                if ((s as OnOffSwitch).On)
                                    node.Label = "On";
                                    //_mAnnotation.Content = "On";
                            }
                            else if (e1.PropertyName == "Off")
                            {
                                if ((s as OnOffSwitch).Off)
                                    node.Label = "Off";
                                    //_mAnnotation.Content = "Off";
                            }
                            else if (e1.PropertyName == "HorizontalAlignment")
                            {
                                if ((s as OnOffSwitch).HorizontalAlignment == HorizontalAlignment.Right)
                                {
                                    _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                                    _mAnnotation.LabelMargin = new Thickness(5, 0, 0, 0);
                                }
                                else if ((s as OnOffSwitch).HorizontalAlignment == HorizontalAlignment.Left)
                                {
                                    _mAnnotation.HorizontalAlignment = HorizontalAlignment.Right;
                                    _mAnnotation.LabelMargin = new Thickness(0, 0, 5, 0);
                                }
                            }
                        };
                    }
                }
                #endregion
                #region ComboBox
                else if ((node as INodeVM).Shape.Equals("combobox"))
                    {
                        (_mAnnotation.DataContext as PropertyChange).PropertyChanged += (s, e) =>
                        {
                            if (e.PropertyName == "ShowItems")
                            {
                                if((_mAnnotation.DataContext as ComboBoxBusinessClass).ShowItems)
                                {
                                    Size textsize = new Size();
                                    textsize = FindDummyTextSize(_mAnnotation, (s as ComboBoxBusinessClass).Title, TextWrapping.NoWrap);
                                    (s as ComboBoxBusinessClass).ItemHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                    textsize.Width += (s as ComboBoxBusinessClass).ItemHeight + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                    textsize.Height += _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                    foreach (var item in ((s as ComboBoxBusinessClass).Items))
                                    {
                                        Size localsize = FindDummyTextSize(_mAnnotation, item.Item, TextWrapping.NoWrap);
                                        //if (textsize.Width < localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right)
                                        //    textsize.Width = localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                        textsize.Height += localsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                                    }
                                    //textsize.Width = FindDummyTextSize(_mAnnotation, ((s as LabelVM).DataContext as ComboBoxBusinessClass).Title, TextWrapping.NoWrap).Width + (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                                    node.UnitWidth = textsize.Width;
                                    node.UnitHeight = textsize.Height;
                                }
                                else
                                {
                                    node.UnitHeight = (s as ComboBoxBusinessClass).ItemHeight;
                                }
                            }
                        };
                    }
                    #endregion
                #region stickynote / textarea / textinput/geometricshapes
                else if ((node as INodeVM).Shape.Equals("stickynote") || (node as INodeVM).Shape.Equals("textarea") || (node as INodeVM).Shape.Equals("textinput") || (node as INodeVM).Shape.Equals("geometricshapes"))
                {
                    (_mAnnotation.DataContext as PropertyChange).PropertyChanged += (s, e1) =>
                    {
                        if(e1.PropertyName == "HorizontalAlignment")
                        {
                            switch ((s as PropertyChange).HorizontalAlignment)
                            {
                                case HorizontalAlignment.Left:
                                    _mAnnotation.TextAlignment = TextAlignment.Left;
                                    break;
                                case HorizontalAlignment.Right:
                                    _mAnnotation.TextAlignment = TextAlignment.Right;
                                    break;
                                case HorizontalAlignment.Center:
                                    _mAnnotation.TextAlignment = TextAlignment.Center;
                                    break;
                            }
                        }
                    };
                }
                #endregion               
                #region iphone
                else if ((node as INodeVM).Shape.Equals("iphone"))
                {
                    (node.DataContext as IPhoneBusinessClass).PropertyChanged += (s, e1) =>
                    {
                        if (e1.PropertyName == "Template")
                        {
                            node.ContentTemplate = (node.DataContext as IPhoneBusinessClass).Template;
                            double width = node.UnitWidth;
                            node.UnitWidth = node.UnitHeight;
                            node.UnitHeight = width;
                        }
                    };
                }
                #endregion
                #region ipad
                else if ((node as INodeVM).Shape.Equals("ipad"))
                {
                    (node.DataContext as IPADBusinessClass).PropertyChanged += (s, e1) =>
                    {
                        double width = node.UnitWidth;
                        node.UnitWidth = node.UnitHeight;
                        node.UnitHeight = width;
                    };
                }
                #endregion
            }
        }
        #endregion

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
                    //(Info as IGraphInfo).Commands.FitToPage.Execute(
                    //    new FitToPageParameter
                    //    {
                    //        FitToPage = FitToPage.FitToPage,
                    //        Margin = new Thickness(20)
                    //    }
                    //);
                    await this.Save();
                }
                IsBusy = Visibility.Collapsed;
            }
        }


        public void OnRenameCommand(object param)
        {
            EditTile = true;
        }

        public string Title
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    foreach(var item in (this.SelectedItems as SelectorVM).Diagrams)
                    {
                        int i = (item.SelectedItems as SelectorVM).LinkedPages.IndexOf(_name);
                        (item.SelectedItems as SelectorVM).LinkedPages[i] = value;
                    }
                    _name = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public bool IsPanEnabled
        {
            get { return _isPanEnabled; }
            set
            {
                if (_isPanEnabled != value)
                {
                    _isPanEnabled = value;
                    OnPropertyChanged("IsPanEnabled");

                    if (value)
                    {
                        _ToolBeforeEnablePanning = this.Tool;
                        this.Tool = Syncfusion.UI.Xaml.Diagram.Tool.ZoomPan;
                    }
                    else
                    {
                        if (_ToolBeforeEnablePanning != null)
                            this.Tool = _ToolBeforeEnablePanning.Value;

                    }
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
        public ICommand Rename
        {
            get;
            set;
        }
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
            catch(Exception)
            { }
        }

        public async Task Save()
        {
            try
            {
                IGraphInfo graph = this.Info as IGraphInfo;
                PageVM page = PageSettings as PageVM;
                if (graph != null && this.ScrollSettings.ScrollInfo != null)
                {
                    page.HOffset = this.ScrollSettings.ScrollInfo.HorizontalOffset;
                    page.VOffset = this.ScrollSettings.ScrollInfo.VerticalOffset;
                    page.Scale = this.ScrollSettings.ScrollInfo.CurrentZoom;
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
            if (_file != null)
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

        private async void SinglePageExporting(string p, Guid guid)
        {
//#if SyncfusionFramework4_5_1
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
                   // ExportSettings.ExportBitmapEncoder = await BitmapEncoder.CreateAsync(guid, fileStream);
                    //Method to Export the SfDiagram
                   // await graph.Export();
                }
            } 
//#endif
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
            node.OffsetX = (this.ScrollSettings.ScrollInfo.ViewportWidth) / 2;
            node.OffsetY = (this.ScrollSettings.ScrollInfo.ViewportHeight) / 2;
            node.UnitHeight = 100;
            node.UnitWidth = 100;
            node.Content = new Image() { Source = image, Stretch = Stretch.Fill };
            (Nodes as ObservableCollection<NodeVM>).Add(node);

        }
        async public void OnCapturesCommand(object param)
        {

            var ui = new CameraCaptureUI();
            var file = await ui.CaptureFileAsync(CameraCaptureUIMode.PhotoOrVideo);
            if (file != null)
            {
                IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                BitmapImage bitmap = new BitmapImage();
                bitmap.SetSource(fileStream);

                IGraphInfo graph = this.Info as IGraphInfo;
                NodeVM node = new NodeVM();
                node.OffsetX = (this.ScrollSettings.ScrollInfo.ViewportWidth) / 2;
                node.OffsetY = (this.ScrollSettings.ScrollInfo.ViewportHeight) / 2;
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
                //case "FreeHand":
                //  //  DefaultConnectorType = ConnectorType.PolyCubicBezier;
                    //break;
            }
            Tool |= Tool.ContinuesDraw;
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

            //if (type == "Node" && Nodes != null)
            {
                //foreach (IConnector o in ConnectorsE)
                //{
                //    o.IsSelected = false;
                //}

                foreach (INode o in NodesE)
                {
                    o.IsSelected = true;
                }
            }
            //if (type == "Connector" && ConnectorsE != null)
            //{
            //    foreach (INode o in NodesE)
            //    {
            //        o.IsSelected = false;
            //    }

            //    foreach (IConnector o in ConnectorsE)
            //    {
            //        o.IsSelected = true;
            //    }
            //}
        }

//#if !SyncfusionFramework4_5_1
//        public object ExportSettings { get; set; }
//        public object PrintingService { get; set; }
//#endif

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
        
        protected override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);            
        }

        public ICommand MockupAttachedPropertiesCommand { get; set; }

        public void OnMockupAttachedPropertiesCommand(object param)
        {
        }

        private bool _mEditTitle = false;
        public bool EditTile
        {
            get { return _mEditTitle; }
            set
            {
                if (_mEditTitle != value)
                {
                    _mEditTitle = value;
                    OnPropertyChanged("EditTile");
                    if (value)
                        TitleTemplate = resourceDictionary["TabbedDiagramTitleTextBox"] as DataTemplate;
                    else
                        TitleTemplate = resourceDictionary["TabbedDiagramTitleTextBlock"] as DataTemplate;
                }
            }
        }


        private DataTemplate _mTitleTemplate = null;
        public DataTemplate TitleTemplate
        {
            get { return _mTitleTemplate; }
            set
            {
                if (_mTitleTemplate != value)
                {
                    _mTitleTemplate = value;
                    OnPropertyChanged("TitleTemplate");
                }
            }
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
    }
}


