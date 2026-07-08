using Mockup.BusinessObject;
using Mockup.Utility;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using System.Reflection;

namespace Mockup.ViewModel
{
    public class NodeVM : GroupableVM, INodeVM, IHyperLink
    {
        
        public NodeVM()
        {
            Stroke = new SolidColorBrush(new Color() { A = 0xFF, R = 0x7F, G = 0x7F, B = 0x7F });
            OnConstraintsChanged();
        }

        #region INode
        AnnotationConstraints _mAnnotationConstraints = AnnotationConstraints.Default;
        /// <summary>
        ///  AnnotationConstraints
        /// </summary>
        public AnnotationConstraints AnnotationConstraints
        {
            get
            {
                return _mAnnotationConstraints;
            }
            set
            {
                if (_mAnnotationConstraints != value)
                {
                    _mAnnotationConstraints = value;
                    OnPropertyChanged(GroupableConstants.AnnotationConstraints);
                }
            }
        }

        FlipMode _mFlipMode = FlipMode.FlipMode;
        /// <summary>
        ///  FlipMode
        /// </summary>
        public FlipMode FlipMode
        {
            get
            {
                return _mFlipMode;
            }
            set
            {
                if (_mFlipMode != value)
                {
                    _mFlipMode = value;
                    OnPropertyChanged(NodeConstants.FlipMode);
                }
            }
        }
        double _mOffsetX = 0d;
        public double OffsetX
        {
            get
            {
                return _mOffsetX;
            }
            set
            {
                if (_mOffsetX != value)
                {
                    _mOffsetX = value;
                    OnPropertyChanged(GroupableConstants.OffsetX);
                }
            }
        }


        double _mOffsetY = 0d;
        public double OffsetY
        {
            get
            {
                return _mOffsetY;
            }
            set
            {
                if (_mOffsetY != value)
                {
                    _mOffsetY = value;
                    OnPropertyChanged(GroupableConstants.OffsetY);
                }
            }
        }


        double _mRotateAngle = 0d;
        public double RotateAngle
        {
            get
            {
                return _mRotateAngle;
            }
            set
            {
                if (_mRotateAngle != value)
                {
                    _mRotateAngle = value;
                    OnPropertyChanged(GroupableConstants.RotateAngle);
                }
            }
        }


        SnapToObject _mGuidelines = SnapToObject.None;
        public SnapToObject SnapToObject
        {
            get
            {
                return _mGuidelines;
            }
            set
            {
                if (_mGuidelines != value)
                {
                    _mGuidelines = value;
                    OnPropertyChanged(NodeConstants.SnapToObject);
                }
            }
        }


        Flip _mFlip = Flip.None;
        public Flip Flip
        {
            get
            {
                return _mFlip;
            }
            set
            {
                if (_mFlip != value)
                {
                    _mFlip = value;
                    OnPropertyChanged(NodeConstants.Flip);
                }
            }
        }


        double _mMinWidth = 0d;
        public double MinWidth
        {
            get
            {
                return _mMinWidth;
            }
            set
            {
                if (_mMinWidth != value)
                {
                    _mMinWidth = value;
                    OnPropertyChanged(NodeConstants.MinWidth);
                }
            }
        }


        double _mMaxWidth = double.PositiveInfinity;
        public double MaxWidth
        {
            get
            {
                return _mMaxWidth;
            }
            set
            {
                if (_mMaxWidth != value)
                {
                    _mMaxWidth = value;
                    OnPropertyChanged(NodeConstants.MaxWidth);
                }
            }
        }


        double _mUnitWidth = double.NaN;
        public double UnitWidth
        {
            get
            {
                return _mUnitWidth;
            }
            set
            {
                if (_mUnitWidth != value)
                {
                    _mUnitWidth = value;
                    OnPropertyChanged(GroupableConstants.UnitWidth);
                    SelectorToNode(this);
                }
            }
        }

        
        double _mMinHeight = 0d;
        public double MinHeight
        {
            get
            {
                return _mMinHeight;
            }
            set
            {
                if (_mMinHeight != value)
                {
                    _mMinHeight = value;
                    OnPropertyChanged(NodeConstants.MinHeight);
                }
            }
        }


        double _mMaxHeight = double.PositiveInfinity;
        public double MaxHeight
        {
            get
            {
                return _mMaxHeight;
            }
            set
            {
                if (_mMaxHeight != value)
                {
                    _mMaxHeight = value;
                    OnPropertyChanged(NodeConstants.MaxHeight);
                }
            }
        }


        double _mUnitHeight = double.NaN;
        public double UnitHeight
        {
            get
            {
                return _mUnitHeight;
            }
            set
            {
                if (_mUnitHeight != value)
                {
                    _mUnitHeight = value;
                    OnPropertyChanged(GroupableConstants.UnitHeight);
                    SelectorToNode(this);
                }
            }
        }


        object _mContent = null;
        public object Content
        {
            get
            {
                return _mContent;
            }
            set
            {
                if (_mContent != value)
                {
                    _mContent = value;
                    OnPropertyChanged(NodeConstants.Content);
                }
            }
        }

        private void AvoidLabelEditMode()
        {
            foreach (LabelVM _mAnnotation in this.Annotations as List<IAnnotation>)
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

        public SolidColorBrush GetColorFromHexa(string hexaColor)
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

                dummyTextBlock.Measure(new Size(0, 0));
                dummyTextBlock.Arrange(new Rect(0, 0, 0, 0));
                return new Size(dummyTextBlock.ActualWidth, dummyTextBlock.ActualHeight);
            }
            else
            {
                return new Size(0, 0);
            }
        }

        private string _mContentTemplateKey = string.Empty;
        public string ContentTemplateKey
        {
            get { return _mContentTemplateKey; }
            set
            { _mContentTemplateKey = value; }
        }

        DataTemplate _mContentTemplate = null;
        public DataTemplate ContentTemplate
        {
            get
            {
                return _mContentTemplate;
            }
            set
            {
                #region Wireframe
                // for Wireframe
                //if (_mContentTemplate == null)
                //{
                //    #region Chart
                //    if (this.Shape.Equals("columnchart"))
                //    {
                //        _mContentTemplate = App.Current.Resources["columnchart"] as DataTemplate;
                //        UnitWidth = 200;
                //        UnitHeight = 165;
                //        AvoidLabelEditMode();
                //        this.PropertiesList = new PropertiesList() {AlignmentTab =true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    else if (this.Shape.Equals("piechart"))
                //    {
                //        _mContentTemplate = App.Current.Resources["piechart"] as DataTemplate;
                //        UnitWidth = 200;
                //        UnitHeight = 200;
                //        AvoidLabelEditMode();
                //        Constraints = Constraints | NodeConstraints.AspectRatio;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    else if (this.Shape.Equals("linechart"))
                //    {
                //        _mContentTemplate = App.Current.Resources["linechart"] as DataTemplate;
                //        UnitWidth = 200;
                //        UnitHeight = 165;
                //        AvoidLabelEditMode();
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    else if (this.Shape.Equals("barchart"))
                //    {
                //        _mContentTemplate = App.Current.Resources["barchart"] as DataTemplate;
                //        UnitWidth = 200;
                //        UnitHeight = 165;
                //        AvoidLabelEditMode();
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    else if (this.Shape.Equals("bubblechart"))
                //    {
                //        _mContentTemplate = resourceDictionary["bubblechart"] as DataTemplate;
                //        UnitWidth = 200;
                //        UnitHeight = 165;
                //        AvoidLabelEditMode();
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    #endregion
                //    #region ScrollBar
                //    else if (this.Shape.Equals("horizontalscrollbar"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        _mContentTemplate = resourceDictionary["horizontalscrollbar"] as DataTemplate;
                //        this.DataContext = new HorizontalScrollBarBusinessClass();
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { ScrollSlider = true }, LinkTab = false };
                //    }
                //    else if (this.Shape.Equals("verticalscrollbar"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeSouth | NodeConstraints.ResizeNorth;
                //        _mContentTemplate = resourceDictionary["verticalscrollbar"] as DataTemplate;
                //        this.DataContext = new VerticalScrollBarBusinessClass();
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { ScrollSlider = true }, LinkTab = false };
                //    }
                //    #endregion
                //    #region videoPlayer
                //    else if (this.Shape.Equals("videoPlayer"))
                //    {
                //        _mContentTemplate = resourceDictionary["videoPlayer"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    #endregion
                //    #region streetmap
                //    else if (this.Shape.Equals("streetmap"))
                //    {
                //        _mContentTemplate = App.Current.Resources["streetmap"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    #endregion
                //    #region ioskeyboard
                //    else if (this.Shape.Equals("ioskeyboard"))
                //    {
                //        DataContext = new IOSKeyboardBusinessClass();
                //        this._mContentTemplate = (this.DataContext as IOSKeyboardBusinessClass).Template;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = null,
                //            SettingTab = new SettingTab() {ioskeyboard = true },
                //            LinkTab = false
                //        };
                //    }
                //    #endregion
                //    #region ipad
                //    else if (this.Shape.Equals("ipad"))
                //    {
                //        DataContext = new IPADBusinessClass();
                //        this._mContentTemplate = (this.DataContext as IPADBusinessClass).Template;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = null,
                //            SettingTab = new SettingTab() { ipad = true },
                //            LinkTab = false
                //        };
                //    }
                //    #endregion
                //    else if (this.Shape.Equals("iphone"))
                //    {
                //        DataContext = new IPhoneBusinessClass();
                //        this._mContentTemplate = (this.DataContext as IPhoneBusinessClass).Template;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = null,
                //            SettingTab = new SettingTab() { IPhone = true },
                //            LinkTab = false
                //        };
                //    }
                //    #region captcha
                //    else if (this.Shape.Equals("captcha"))
                //    {
                //        UnitWidth = 180;
                //        _mContentTemplate = App.Current.Resources["captcha"] as DataTemplate;
                //    }
                //    #endregion
                //    #region Sliders 
                //    else if (this.Shape.Equals("verticalslider"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeSouth | NodeConstraints.ResizeNorth;
                //        _mContentTemplate = App.Current.Resources["verticalslider"] as DataTemplate;
                //        this.DataContext = new SliderBusinessClass();
                //        this.PropertiesList = new PropertiesList() 
                //        { 
                //            AlignmentTab = true, EditTab = null, 
                //            SettingTab = new SettingTab() { Stroke = true, StrokeThickness = true, ScrollSlider = true, Collection = true }, 
                //            LinkTab = false 
                //        };
                //    }
                //    else if (this.Shape.Equals("horizotalslider"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        _mContentTemplate = App.Current.Resources["horizotalslider"] as DataTemplate;
                //        this.DataContext = new SliderBusinessClass();
                //        this.PropertiesList = new PropertiesList() 
                //        { 
                //            AlignmentTab = true, EditTab = null, 
                //            SettingTab = new SettingTab() { Stroke = true, StrokeThickness = true, ScrollSlider = true, Collection = true }, 
                //            LinkTab = false 
                //        };
                //    }
                //    #endregion
                //    else if (this.Shape.Equals("callout1"))
                //    {
                //        _mContentTemplate = App.Current.Resources["callout1"] as DataTemplate;
                //    }
                //    else if (this.Shape.Equals("callout2"))
                //    {
                //        _mContentTemplate = App.Current.Resources["callout2"] as DataTemplate;
                //    }
                //    else if (this.Shape.Equals("callout3"))
                //    {
                //        _mContentTemplate = App.Current.Resources["callout3"] as DataTemplate;
                //    }
                //    else if (this.Shape.Equals("callout4"))
                //    {
                //        _mContentTemplate = App.Current.Resources["callout4"] as DataTemplate;
                //    }
                //    #region Helpbuttons
                //    else if (this.Shape.Equals("helpbutton"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable;
                //        _mContentTemplate = App.Current.Resources["helpbutton"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    else if (this.Shape.Equals("helpbutton1"))
                //    {
                //        _mContentTemplate = App.Current.Resources["helpbutton1"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    #endregion
                //    #region volume
                //    else if (this.Shape.Equals("volume"))
                //    {
                //        _mContentTemplate = App.Current.Resources["volume"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = null,
                //            SettingTab = null,
                //            LinkTab = false
                //        };
                //    }
                //    #endregion
                //    #region menubar
                //    else if (this.Shape.Equals("menubar"))
                //    {
                //        _mContentTemplate = null;
                //        this.DataContext = new MenuBarBusinessClass()
                //        {
                //            Items = new ObservableCollection<object>() 
                //            {
                //                new MenuTitleBusinessClass(){Item = "File",Selected = true},
                //                new MenuTitleBusinessClass(){Item = "Edit"},
                //                new MenuTitleBusinessClass(){Item = "View"},
                //                new MenuTitleBusinessClass(){Item = "Help"},
                //                new MenuTitleBusinessClass(){Item = "Tool"}
                //            }
                //        };
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = null,
                //            SettingTab = new SettingTab() {Selection = true, ShowBorder = true },
                //            LinkTab = true
                //        };
                //    }
                //    #endregion
                //    #region menu
                //    else if (this.Shape.Equals("menu"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        _mContentTemplate = null;
                //        this.DataContext = new MenuBusinessClass()
                //        {
                //            Items = new ObservableCollection<object>()
                //            {
                //                new MenuItemBusinessClass(){Item = "Open", ShortcutKey = "Ctrl + O"},
                //                new MenuItemBusinessClass(){Item = "x Open Recent"},
                //                new MenuItemBusinessClass(){Item = "----"},
                //                new MenuItemBusinessClass(){Item = "o Option One"},
                //                new MenuItemBusinessClass(){Item = "Option Two"},
                //                new MenuItemBusinessClass(){Item = "----&&"},
                //                new MenuItemBusinessClass(){Item = "x Toggle Item"},
                //                new MenuItemBusinessClass(){Item = "-Disabled Item"},
                //                new MenuItemBusinessClass(){Item = "Exit", ShortcutKey = "Ctrl + Q" }
                //            }   
                //        };
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab() { TextAlignment = false },
                //            SettingTab = new SettingTab() {Selection = true },
                //            LinkTab = true
                //        };
                //    }
                //    #endregion
                //    #region playbackcontrol
                //    else if (this.Shape.Equals("playbackcontrol"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable;
                //        _mContentTemplate = App.Current.Resources["playbackcontrol"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    #endregion
                //    #region webcam
                //    else if (this.Shape.Equals("webcam"))
                //    {
                //        _mContentTemplate = App.Current.Resources["webcam"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    #endregion
                //    else if (this.Shape.Equals("image"))
                //    {
                //        _mContentTemplate = App.Current.Resources["image"] as DataTemplate;
                //    }
                //    #region Tab Bars
                //    else if (this.Shape.Equals("tabsbar"))
                //    {
                //        _mContentTemplate = null;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab() { TextAlignment = false },
                //            SettingTab = new SettingTab() {Fill =true, Opacity = true, Selection = true, HorizontalTab = true,VerticalScrollBar = true },
                //            LinkTab = true
                //        };
                //        this.DataContext = new TabBusinessClass(Orientation.Horizontal)
                //        {
                //            ScrollBar = new VerticalScrollBarBusinessClass()
                //        };
                //        (this.DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab1", Selected = true });
                //        (this.DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab2", Selected = false });
                //        (this.DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab4", Selected = false });
                //        (this.DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab3", Selected = false, IsLastItem = true });
                //    }                    
                //    else if (this.Shape.Equals("verticaltab"))
                //    {
                //        _mContentTemplate = null;
                //        this.PropertiesList = new PropertiesList() 
                //        { 
                //            AlignmentTab = true, 
                //            EditTab = new TextTab() { TextAlignment = false },
                //            SettingTab = new SettingTab() {Fill =true, Opacity = true, Selection = true, VerticalTab=true, VerticalScrollBar = true },
                //            LinkTab = true 
                //        };
                //        this.DataContext = new TabBusinessClass(Orientation.Vertical)
                //        {
                //            ScrollBar = new VerticalScrollBarBusinessClass()
                //        };

                //        (this.DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab1", Selected = true });
                //        (this.DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab2", Selected = false });                        
                //        (this.DataContext as TabBusinessClass).Items.Add(new TabItemBusinessClass() { Item = "Tab3", Selected = false, IsLastItem = true });
                //    }
                //    #endregion
                //    #region ButtonBar
                //    else if (this.Shape.Equals("buttonbar"))
                //    {
                //        _mContentTemplate = null;
                //        this.PropertiesList = new PropertiesList() 
                //        { 
                //            AlignmentTab = true, 
                //            EditTab = new TextTab() { TextAlignment = false },
                //            SettingTab = new SettingTab() { Selection = true },
                //            LinkTab = true
                //        };
                //        this.DataContext = new ButtonBarBusinessClass()
                //        {
                //            Items = new ObservableCollection<object>()
                //            {
                //                new ButtonBarItemBusinessClass(){Item = "Tab1", Selected = true},
                //                new ButtonBarItemBusinessClass(){Item = "Tab2", Selected = false},
                //                new ButtonBarItemBusinessClass(){Item = "Tab3", Selected = false}
                //            }
                //        };
                //    }
                //    #endregion
                //    #region searchbutton
                //    else if (this.Shape.Equals("searchbutton"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        _mContentTemplate = App.Current.Resources["searchbutton"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() 
                //        { 
                //            AlignmentTab = true, 
                //            EditTab = new TextTab(),
                //            SettingTab = new SettingTab() { Collection = true },
                //            LinkTab = true 
                //        };
                //        this.DataContext = new SearchBoxBusinessClass();
                //    }
                //    #endregion
                //    #region progressbar
                //    else if (this.Shape.Equals("progressbar"))
                //    {
                //        _mContentTemplate = App.Current.Resources["progressbar"] as DataTemplate;
                //        DataContext = new ProgressBarBusinessClass();
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { ScrollSlider = true }, LinkTab = false };
                //    }
                //    #endregion
                //    #region volumeslider
                //    else if (this.Shape.Equals("volumeslider"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        _mContentTemplate = App.Current.Resources["volumeslider"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { ScrollSlider = true }, LinkTab = false };
                //        this.DataContext = new VolumeSliderBusinessClass();
                //    }
                //    #endregion
                //    #region browserWindow
                //    else if (this.Shape.Equals("browserWindow"))
                //    {
                //        _mContentTemplate = App.Current.Resources["browserWindow"] as DataTemplate;
                //        this.DataContext = new BrowserWindowBusinessClass()
                //        {
                //            WebsiteTitle = "A Web Page",
                //            WebsiteURL = "http:\\",
                //            ScrollBar = new VerticalScrollBarBusinessClass()
                //        };
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = null,
                //            SettingTab = new SettingTab() { Fill = true, Opacity = true, VerticalScrollBar = true },
                //            LinkTab = false
                //        };
                //    }
                //    #endregion 
                //    #region checkbox / radiobutton
                //    else if (this.Shape.Equals("checkbox") || this.Shape.Equals("radiobutton"))
                //    {
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        _mContentTemplate = App.Current.Resources["checkbox"] as DataTemplate;
                //        this.DataContext = new CheckBoxBusinessClass();
                //        this.PropertiesList = new PropertiesList() 
                //        { 
                //            AlignmentTab = true, 
                //            EditTab = new TextTab() { TextAlignment = false }, 
                //            SettingTab = new SettingTab(){Collection = true},
                //            LinkTab = true 
                //        };
                //    }
                //    #endregion
                //    #region datechooser
                //    else if (this.Shape.Equals("datechooser"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        _mContentTemplate = null;
                //        this.DataContext = new DateChoosetBusinessClass();
                //        this.PropertiesList = new PropertiesList() 
                //        { 
                //            AlignmentTab = true, 
                //            EditTab = new TextTab(), 
                //            SettingTab = new SettingTab(){Stroke = true, Collection = true},
                //            LinkTab = false
                //        };
                //    }
                //    #endregion
                //    #region windowbox
                //    else if (this.Shape.Equals("windowbox"))
                //    {
                //        _mContentTemplate = App.Current.Resources["windowbox"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false }, SettingTab = new SettingTab() { WindowBox = true , VerticalScrollBar = true}, LinkTab = false };
                //        this.DataContext = new WindowBoxBusinessObject() 
                //        { 
                //            TopMargin = new Thickness(0, 0, 0, 0), 
                //            BottomMargin = new Thickness(0, 0, 0, 0),
                //            ScrollBar = new VerticalScrollBarBusinessClass()
                //        };
                //    }
                //    #endregion
                //    #region onoffswitch
                //    else if (this.Shape.Equals("onoffswitch"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable;
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        _mContentTemplate = App.Current.Resources["onoffswitch"] as DataTemplate;
                //        this.DataContext = new OnOffSwitch();                       

                //        this.PropertiesList = new PropertiesList() 
                //        { 
                //            AlignmentTab = true, 
                //            EditTab = null, 
                //            SettingTab = new SettingTab(){Fill =true, Opacity = true, OnOffSwitch = true},
                //            LinkTab = true 
                //        };
                //    }
                //    #endregion
                //    #region button
                //    else if (this.Shape.Equals("button"))
                //    {
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        _mContentTemplate = App.Current.Resources["button"] as DataTemplate;
                //        this.DataContext = new ButtonBusinessObject();
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab(),
                //            SettingTab = new SettingTab() {Fill = true, Opacity =true, Collection =true },
                //            LinkTab = true
                //        };
                //    }
                //    #endregion
                //    #region geometricshapes
                //    else if (this.Shape.Equals("geometricshapes"))
                //    {
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        this.DataContext = new GeometricShapesBusinessClass();
                //        this._mContentTemplate = (this.DataContext as GeometricShapesBusinessClass).Template;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab(),
                //            SettingTab = new SettingTab() { Fill = true, Opacity = true, Stroke = true, GeometricShapes = true},
                //            LinkTab = true
                //        };
                //    }
                //    #endregion
                //    #region numericstepper
                //    else if (this.Shape.Equals("numericstepper"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        _mContentTemplate = null;
                //        this.DataContext = new NumericStepperBusinessClass();
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab() { TextAlignment = false },
                //            SettingTab = new SettingTab() { Stroke = true, Collection = true },
                //            LinkTab = false
                //        };
                //    }
                //    #endregion
                //    else if (this.Shape.Equals("multilinebutton"))
                //    {
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        _mContentTemplate = App.Current.Resources["multilinebutton"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab() { TextAlignment = false },
                //            SettingTab = new SettingTab() { Fill = true, Opacity = true },
                //            LinkTab = true
                //        };
                //        this.DataContext = new MultiLineButtonBusinessObject() { Title = "Multiline Button", Message = "Second line of text" };
                //    }
                //    #region messagebox
                //    else if (this.Shape.Equals("messagebox"))
                //    {
                //        _mContentTemplate = null;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = null,
                //            SettingTab = null,
                //            LinkTab = true
                //        };
                //        this.DataContext = new MessageBoxBusinessObject()
                //        {
                //            Title = "Title",
                //            Message = "Message",
                //            Ok = "Ok"
                //        };
                //    }
                //    #endregion
                //    else if (this.Shape.Equals("tooltip"))
                //    {
                //        //_mContentTemplate = App.Current.Resources["tooltiptopleft"] as DataTemplate;
                //        DataContext = new ToolTipBusinessClass();
                //        _mContentTemplate = (DataContext as ToolTipBusinessClass).Template;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab(),
                //            SettingTab = new SettingTab() { ToolTip = true },
                //            LinkTab = false
                //        };
                //    }
                //    #region alertbox
                //    else if (this.Shape.Equals("alertbox"))
                //    {
                //        _mContentTemplate = App.Current.Resources["alertbox"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = null,
                //            SettingTab = null,
                //            LinkTab = true
                //        };
                //        this.DataContext = new DialogBoxBusinessObject()
                //        {
                //            Title = "Alert",
                //            Message = "Message",
                //            Cancel = "No",
                //            Ok = "Yes"
                //        };
                //    }
                //    #endregion
                //    #region calendar
                //    else if (this.Shape.Equals("calendar"))
                //    {
                //        _mContentTemplate = App.Current.Resources["calendar"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    #endregion
                //    else if (this.Shape.Equals("combobox"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        _mContentTemplate = App.Current.Resources["combobox"] as DataTemplate;
                //        this.DataContext = new ComboBoxBusinessClass();                        
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false }, SettingTab = new SettingTab() {Stroke = true, Collection = true }, LinkTab = true };
                //    }
                //    #region scratchout
                //    else if (this.Shape.Equals("scratchout"))
                //    {
                //        _mContentTemplate = App.Current.Resources["scratchout"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() { Fill = true, Opacity = true }, LinkTab = false };
                //    }
                //    #endregion
                //    #region Curlybraces
                //    else if (this.Shape.Equals("horizontalcurlybraces"))
                //    {
                //        _mContentTemplate = null;
                //        this.DataContext = new HorizontalCurlyBracesBusinessClass();
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = new SettingTab() { HorizontalCurlyBraces = true }, LinkTab = false };
                //    }
                //    else if (this.Shape.Equals("verticalcurlybraces"))
                //    {
                //        _mContentTemplate = null;
                //        this.DataContext = new VerticalCurlyBracesBusinessClass();
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = new SettingTab() { VerticalCurlyBraces = true }, LinkTab = false };
                //    }
                //    #endregion
                //    #region stickynote
                //    else if (this.Shape.Equals("stickynote"))
                //    {
                //        _mContentTemplate = App.Current.Resources["stickynote"] as DataTemplate;
                //        this.DataContext = new StickyNoteBusinessClass();
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = new SettingTab() { Fill = true}, LinkTab = false };
                //    }
                //    #endregion
                //    #region subtitle
                //    else if (this.Shape.Equals("subtitle"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        _mContentTemplate = App.Current.Resources["subtitle"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = null, LinkTab = true };
                //    }
                //    #endregion
                //    #region checkboxgroup
                //    else if (this.Shape.Equals("checkboxgroup"))
                //    {
                //        _mContentTemplate = App.Current.Resources["checkboxgroup"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab() { TextAlignment = false },
                //            SettingTab = null,
                //            LinkTab = true
                //        };
                //        this.DataContext = new CheckBoxGroupBusinessClass()
                //        {
                //            Items = new ObservableCollection<object>()
                //            {
                //                new CheckBoxItemBusinessClass(){Item = "[x] Selected"},
                //                new CheckBoxItemBusinessClass(){Item = "[] Not Selected"},
                //                new CheckBoxItemBusinessClass(){Item = "-[] Disabled"},
                //                new CheckBoxItemBusinessClass(){Item = "-[x] Disabled Selected"},
                //                new CheckBoxItemBusinessClass(){Item = "Without Checkbox"}
                //            }
                //        };
                //    }
                //    #endregion
                //    #region radiobuttongroup
                //    else if (this.Shape.Equals("radiobuttongroup"))
                //    {
                //        _mContentTemplate = App.Current.Resources["radiobuttongroup"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab() { TextAlignment = false },
                //            SettingTab = null,
                //            LinkTab = true
                //        };
                //        this.DataContext = new RadioButtonGroupBusinessClass()
                //        {
                //            Items = new ObservableCollection<object>()
                //            {
                //                new RadioButtonItemBusinessClass(){Item = "(o) Selected"},
                //                new RadioButtonItemBusinessClass(){Item = "() Not Selected"},
                //                new RadioButtonItemBusinessClass(){Item = "-() Disabled"},
                //                new RadioButtonItemBusinessClass(){Item = "-(o) Disabled Selected"},
                //                new RadioButtonItemBusinessClass(){Item = "Without RadioButton"},
                //            }
                //        };
                //    }
                //    #endregion
                //    else if (this.Shape.Equals("accordion"))
                //    {
                //        _mContentTemplate = null;
                //        this.DataContext = new AccordionBusinessObject()
                //        {
                //            Items = new ObservableCollection<object>()
                //            {
                //                new AccordionItemBusinessObject()
                //                {
                //                    Item = "Tab1", Index = 1,
                //                    Items = new ObservableCollection<object>()
                //                    {
                //                        new AccordionSubItemBusinessObject(){Item = "- SubItem 1-1", Index = 1, ParentIndex = 1},
                //                        new AccordionSubItemBusinessObject(){Item = "- SubItem 1-2", Index = 2, ParentIndex = 1}
                //                    },
                //                },
                //                new AccordionItemBusinessObject()
                //                {
                //                    Item = "Tab2" , Index = 2,                                    
                //                    Items = new ObservableCollection<object>()
                //                    {
                //                        new AccordionSubItemBusinessObject(){Item = "- SubItem 2-1", Index = 1, ParentIndex = 2},
                //                        new AccordionSubItemBusinessObject(){Item = "- SubItem 2-2", Index = 2, ParentIndex = 2}
                //                    },                                    
                //                },
                //                new AccordionItemBusinessObject()
                //                {
                //                    Item = "Tab3" , Index = 3,
                //                    Items = new ObservableCollection<object>()
                //                    {
                //                        new AccordionSubItemBusinessObject(){Item = "- SubItem 3-1", Index = 1, ParentIndex = 3},
                //                        new AccordionSubItemBusinessObject(){Item = "- SubItem 3-2", Index = 2, ParentIndex = 3},
                //                        new AccordionSubItemBusinessObject(){Item = "- SubItem 3-3", Index = 3, ParentIndex = 3}
                //                    }
                //                }
                //            }
                //        };

                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = null,
                //            SettingTab = new SettingTab() { Accordion = true, VerticalScrollBar = true },
                //            LinkTab = true
                //        };

                //        //DataContext = new AccordionBusinessObject()
                //        //{
                            
                //        //    //Items = new ObservableCollection<AccordionItemBusinessObject>() 
                //        //    //{
                //        //    //    new AccordionItemBusinessObject(){Item = "Item 1"},
                //        //    //    new AccordionItemBusinessObject()
                //        //    //    {
                //        //    //        Item = "Item 2",
                //        //    //        Items = new ObservableCollection<AccordionSubItemBusinessObject>()
                //        //    //        {
                //        //    //            new AccordionSubItemBusinessObject(){Item = "SubItem 2-1"},
                //        //    //            new AccordionSubItemBusinessObject(){Item = "SubItem 2-2"}
                //        //    //        }
                //        //    //    }
                //        //    //}
                //        //};
                //    }
                //    #region colorpicker
                //    else if (this.Shape.Equals("colorpicker"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable;
                //        _mContentTemplate = App.Current.Resources["colorpicker"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = null,
                //            SettingTab = new SettingTab() { Fill = true, Stroke = true, StrokeThickness = true },
                //            LinkTab = false
                //        };
                //    }
                //    #endregion
                //    #region redx
                //    else if (this.Shape.Equals("redx"))
                //    {
                //        _mContentTemplate = App.Current.Resources["redx"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    #endregion
                //    #region fieldset
                //    else if (this.Shape.Equals("fieldset"))
                //    {
                //        _mContentTemplate = App.Current.Resources["fieldset"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = new SettingTab() {Fill = true, Opacity = true }, LinkTab = false };
                //    }
                //    #endregion
                //    #region popover
                //    else if (this.Shape.Equals("popover"))
                //    {
                //        //_mContentTemplate = App.Current.Resources["toppopover"] as DataTemplate;
                //        this.DataContext = new PopoverBusinessClass();
                //        this._mContentTemplate = (this.DataContext as PopoverBusinessClass).Template;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false }, SettingTab = new SettingTab() { Fill =true, Popover =true }, LinkTab = false };
                //    }
                //    #endregion
                //    #region pointybutton
                //    else if (this.Shape.Equals("pointybutton"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        //_mContentTemplate = App.Current.Resources["rightpointybutton"] as DataTemplate;
                //        this.DataContext = new PointyButtonBusinessClass();
                //        _mContentTemplate = (this.DataContext as PointyButtonBusinessClass).Template;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab() { Foreground = false },
                //            SettingTab = new SettingTab() { Fill = true, Opacity = true, Collection = true, PointyButton = true, MenuIcon = true },
                //            LinkTab = true
                //        };
                //    }
                //    #endregion
                //    #region bigtitle
                //    else if (this.Shape.Equals("bigtitle"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        _mContentTemplate = App.Current.Resources["subtitle"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = null, LinkTab = true };
                //    }
                //    #endregion
                //    #region link
                //    else if (this.Shape.Equals("link"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable;
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        _mContentTemplate = App.Current.Resources["link"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false, Foreground = false }, SettingTab = new SettingTab() {Collection = true }, LinkTab = true };
                //        this.DataContext = new LinkBusinessClass();
                //    }
                //    #endregion
                //    else if (this.Shape.Equals("linkbar"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable;
                //        _mContentTemplate = null;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab() { TextAlignment = false },
                //            SettingTab = new SettingTab() { Fill = true, Stroke = true, Selection = true },
                //            LinkTab = true
                //        };
                //        this.DataContext = new LinkBarBusinessClass()
                //        {
                //            Items = new ObservableCollection<object>()
                //            {
                //                new LinkBarItemBusinessClass(){Item = "Home"},
                //                new LinkBarItemBusinessClass(){Item = "Products"},
                //                new LinkBarItemBusinessClass(){Item = "Company"},
                //                new LinkBarItemBusinessClass(){Item = "Blog", IsLastItem = true}
                //            }
                //        };
                //    }
                //    #region breadcrumbs
                //    else if (this.Shape.Equals("breadcrumbs"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable;
                //        _mContentTemplate = null;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab() { TextAlignment = false, Foreground = false }, SettingTab = null, LinkTab = true };
                //        this.DataContext = new BreadcrumbsBusinessClass()
                //        {
                //            Items = new ObservableCollection<object>()
                //            {
                //                new BreadcrumbsItemBusinessClass(){Item = "Level 1"},
                //                new BreadcrumbsItemBusinessClass(){Item = "Level 2"},
                //                new BreadcrumbsItemBusinessClass(){Item = "Level 3"},
                //                new BreadcrumbsItemBusinessClass(){Item = "Level 4", IsLastItem = true},
                //            }
                //        };
                //    }
                //    #endregion
                //    #region formatingtool
                //    else if (this.Shape.Equals("formatingtool"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        _mContentTemplate = App.Current.Resources["formatingtool"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    #endregion
                //    #region Spliter
                //    else if (this.Shape.Equals("horizontalspliter"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        _mContentTemplate = App.Current.Resources["horizontalspliter"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    else if (this.Shape.Equals("verticalspliter"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeSouth | NodeConstraints.ResizeNorth;
                //        _mContentTemplate = App.Current.Resources["verticalspliter"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };
                //    }
                //    #endregion
                //    #region list
                //    else if (this.Shape.Equals("list"))
                //    {
                //        _mContentTemplate = null;
                //        this.DataContext = new ListBusinessClass()
                //        {
                //            SelectedIndex = 0,
                //            ScrollBar = new VerticalScrollBarBusinessClass(),
                //            Items = new ObservableCollection<object>()
                //            {
                //                new ListItemBusinessClass(){Item = "Item1"},
                //                new ListItemBusinessClass(){Item = "Item2"},
                //                new ListItemBusinessClass(){Item = "Item3"},
                //            },
                //            OddItemsBackgroud = new SolidColorBrush(Colors.AliceBlue),
                //            EvenItemsBackgroud = new SolidColorBrush(Colors.AliceBlue)
                //        };
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab() { TextAlignment = false },
                //            SettingTab = new SettingTab() { ListOddEven = true, Selection = true, VerticalScrollBar = true, ShowBorder = true },
                //            LinkTab = true
                //        };
                //    }
                //    #endregion
                //    else if (this.Shape.Equals("label"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        _mContentTemplate = null;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = new TextTab(), SettingTab = null, LinkTab = true };
                //        this.DataContext = new LabelBusinessClass()
                //        {
                //            Orientation = 0,
                //            SelectedState = "Normal"
                //        };
                //    }
                //    #region sitemap
                //    else if (this.Shape.Equals("sitemap"))
                //    {
                //        _mContentTemplate = App.Current.Resources["sitemap"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = false };

                //    }
                //    #endregion
                //    else if (this.Shape.Equals("rectangle"))
                //    {
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        _mContentTemplate = App.Current.Resources["rectangle"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = null,
                //            SettingTab = new SettingTab() { Fill = true, Opacity = true, Stroke = true, StrokeThickness = true },
                //            LinkTab = true
                //        };
                //    }
                //    #region modelscreen
                //    else if (this.Shape.Equals("modelscreen"))
                //    {
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        _mContentTemplate = App.Current.Resources["modelscreen"] as DataTemplate;
                //        this.PropertiesList = new PropertiesList() { AlignmentTab = true, EditTab = null, SettingTab = null, LinkTab = true };
                //    }
                //    #endregion
                //    #region textarea
                //    else if (this.Shape.Equals("textarea"))
                //    {
                //        Link = new HyperlinkBusinessClass() { Title = "Link" };
                //        _mContentTemplate = App.Current.Resources["textarea"] as DataTemplate;
                //        this.DataContext = new TextAreaBusinessClass()
                //        {
                //            ScrollBar = new VerticalScrollBarBusinessClass()
                //        };
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab() {Foreground = false },
                //            SettingTab = new SettingTab() { Fill = true, Opacity = true, Stroke = true, Collection = true, VerticalScrollBar = true },
                //            LinkTab = true
                //        };
                //    }
                //    #endregion
                //    #region textinput
                //    else if (this.Shape.Equals("textinput"))
                //    {
                //        Constraints = Constraints & ~NodeConstraints.Resizable | NodeConstraints.ResizeEast | NodeConstraints.ResizeWest;
                //        _mContentTemplate = App.Current.Resources["textinput"] as DataTemplate;
                //        this.DataContext = new TextAreaBusinessClass();
                //        this.PropertiesList = new PropertiesList()
                //        {
                //            AlignmentTab = true,
                //            EditTab = new TextTab() { Foreground = false },
                //            SettingTab = new SettingTab() { Fill = true, Opacity = true, Stroke = true, Collection = true },
                //            LinkTab = false
                //        };
                //    }
                //    #endregion
                //    else
                //    {
                //        _mContentTemplate = value;
                //    }
                //    OnPropertyChanged(NodeConstants.ContentTemplate);
                //}
                //else if (_mContentTemplate != value)
                #endregion
                if (_mContentTemplate != value || Shape.Equals("tooltip") || Shape.Equals("popover") || Shape.Equals("geometricshapes"))
                {
                    _mContentTemplate = value;
                    OnPropertyChanged(NodeConstants.ContentTemplate);
                }
            }
        }     

       
        object _mShape = null;
        public object Shape
        {
            get
            {
                return _mShape;
            }
            set
            {
                if (_mShape != value)
                {
                    _mShape = value;
                    OnPropertyChanged(NodeConstants.Shape);
                }
            }
        }


        Style _mShapeStyle = null;
        public Style ShapeStyle
        {
            get
            {
                return _mShapeStyle;
            }
            set
            {
                if (_mShapeStyle != value)
                {
                    _mShapeStyle = value;
                    OnPropertyChanged(NodeConstants.ShapeStyle);
                }
            }
        }


        bool _mIsExpanded = true;
        public bool IsExpanded
        {
            get
            {
                return _mIsExpanded;
            }
            set
            {
                if (_mIsExpanded != value)
                {
                    _mIsExpanded = value;
                    OnPropertyChanged(NodeConstants.IsExpanded);
                }
            }
        }


        Point _mPivot = new Point(0.5, 0.5);
        public Point Pivot
        {
            get
            {
                return _mPivot;
            }
            set
            {
                if (_mPivot != value)
                {
                    _mPivot = value;
                    OnPropertyChanged(NodeConstants.Pivot);
                }
            }
        }


        NodeConstraints _mConstraints = NodeConstraints.Default;
        public NodeConstraints Constraints
        {
            get
            {
                return _mConstraints;
            }
            set
            {
                if (_mConstraints != value)
                {
                    _mConstraints = value;
                    OnPropertyChanged(NodeConstants.Constraints);
                }
            }
        }


        object _mPorts = null;
        public object Ports
        {
            get
            {
                return _mPorts;
            }
            set
            {
                if (_mPorts != value)
                {
                    _mPorts = value;
                    OnPropertyChanged(NodeConstants.Ports);
                }
            }
        }
        #endregion

        #region Basamiq

        public System.IO.Stream GenerateStreamFromString(string s)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private string _mDummyDataContext = string.Empty;
        [DataMember]
        public string DummyDataContext 
        {
            get
            {

                XmlSerializer serializer = new XmlSerializer(typeof(PropertyChange));
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = Encoding.Unicode; // new UnicodeEncoding(false, false); // no BOM in a .NET string
                settings.Indent = false;
                settings.OmitXmlDeclaration = false;
                string str;
                using (System.IO.StringWriter textWriter = new System.IO.StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                    {

                        serializer.Serialize(xmlWriter, DataContext as PropertyChange);
                    }
                    str = textWriter.ToString();
                }
                return str;
            }
            set 
            {
                if (value.Contains("utf-16"))
                    value = value.Replace("utf-16", "utf-8");
                System.IO.Stream stream = GenerateStreamFromString(value);

                XmlSerializer deserializer = new XmlSerializer(typeof(PropertyChange));

                // Encode the XML string in a UTF-8 byte array
                byte[] encodedString = Encoding.UTF8.GetBytes(value);

                // Put the byte array into a stream and rewind it to the beginning
                System.IO.MemoryStream ms = new System.IO.MemoryStream(encodedString);
                ms.Flush();
                ms.Position = 0;

                object obj = deserializer.Deserialize(ms);
                this.DataContext = obj;
                this.IsSelected = false;
                //this.IsSelected = true;
            }
        }

        private object _mDataContext;        
        public object DataContext
        {
            get 
            {
                return _mDataContext; 
            }
            set 
            {
                if (_mDataContext != value)
                {
                    _mDataContext = value;
                    OnPropertyChanged("DataContext");
                }
            }
        }

        private void Test()
        {

        }

        private Size _mDefaultSize;
        public Size DefaultSize
        {
            get { return _mDefaultSize; }
            set { _mDefaultSize = value; }
        }
        

        private object _mPropertiesList;
        public object PropertiesList
        {
            get { return _mPropertiesList; }
            set { _mPropertiesList = value; }
        }       
        

        

        private int _mTabSelectedIndex = 1;
        public int TabSelectedIndex
        {
            get
            {
                return _mTabSelectedIndex;
            }
            set
            {
                if (_mTabSelectedIndex != value)
                {
                    _mTabSelectedIndex = value;
                    OnPropertyChanged("TabSelectedIndex");
                }
            }
        }

        private object _mTabSelectedItem = "Tab1";
        public object TabSelectedItem
        {
            get
            {
                return _mTabSelectedItem;
            }
            set
            {
                _mTabSelectedItem = value;
                OnPropertyChanged("TabSelectedItem");
            }
        }

        private Thickness _mScrollMargin = new Thickness();
        public Thickness ScrollMargin
        {
            get
            {
                return _mScrollMargin;
            }
            set
            {
                _mScrollMargin = value;
                OnPropertyChanged("ScrollMargin");
            }
        }

        private double _mBrowserWindowWidth = 50;
        public double BrowserWindowWidth
        {
            get { return _mBrowserWindowWidth; }
            set
            {
                _mBrowserWindowWidth = value;
                OnPropertyChanged("BrowserWindowWidth");
            }
        }
        
        #endregion

        private Brush _mFill = new SolidColorBrush(new Color() { A = 0xFF, R = 0xEF, G = 0xEF, B = 0xEF });
        public Brush Fill
        {
            get
            {
                return _mFill;
            }
            set
            {
                if (_mFill != value)
                {
                    _mFill = value;
                    OnPropertyChanged(NodeConstants.Fill);
                }
            }
        }

        [DataMember]
        public string FillDummy
        {
            get
            {
                if (Fill != null && Fill is SolidColorBrush)
                    return (Fill as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Fill = new SolidColorBrush(value.ToColor());
            }
        }

        bool _mAllowDrag = true;
        [DataMember]
        public bool AllowDrag
        {
            get
            {
                return _mAllowDrag;
            }
            set
            {
                if (_mAllowDrag != value)
                {
                    _mAllowDrag = value;
                    OnPropertyChanged(NodeConstants.AllowDrag);
                }
            }
        }

        double _mHitPadding = 0d;
        /// <summary>
        ///  HitPadding
        /// </summary>
        public double HitPadding
        {
            get
            {
                return _mHitPadding;
            }
            set
            {
                if (_mHitPadding != value)
                {
                    _mHitPadding = value;
                    OnPropertyChanged(NodeConstants.HitPadding);
                }
            }
        }


        bool _mAllowResize = true;
        [DataMember]
        public bool AllowResize
        {
            get
            {
                return _mAllowResize;
            }
            set
            {
                if (_mAllowResize != value)
                {
                    _mAllowResize = value;
                    OnPropertyChanged(NodeConstants.AllowResize);
                }
            }
        }

        bool _mAllowRotate = true;
        [DataMember]
        public bool AllowRotate
        {
            get
            {
                return _mAllowRotate;
            }
            set
            {
                if (_mAllowRotate != value)
                {
                    _mAllowRotate = value;
                    OnPropertyChanged(NodeConstants.AllowRotate);
                }
            }
        }

        private string _content;

        [DataMember]
        public string ContentDummy
        {
            get { return _content; }
            set
            {
                _content = value;
                Shape = value; //Clone(value);
                Content = null;
            }
        }

        private bool _mEditMode = true;
        public bool EditMode
        {
            get { return _mEditMode; }
            set
            {
                if (_mEditMode != value)
                {
                    _mEditMode = value;
                    OnPropertyChanged("EditMode");
                    if (value)
                    {
                        EditModeFill = new SolidColorBrush(Colors.Transparent);
                        EditModeOpacity = 1;

                        if(Shape.Equals("checkbox") || Shape.Equals("radiobutton"))
                        {
                            (DataContext as CheckBoxBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                        else if (Shape.Equals("combobox"))
                        {
                            (DataContext as ComboBoxBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                        else if (Shape.Equals("searchbutton"))
                        {
                            (DataContext as SearchBoxBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                    else
                    {
                        EditModeOpacity = 0.3;
                    }
                    if (Shape.Equals("tabsbar") || Shape.Equals("verticaltab"))
                    {
                        (DataContext as TabBusinessClass).EditMode = value;
                    }
                    else if (Shape.Equals("buttonbar"))
                    {
                        (DataContext as ButtonBarBusinessClass).EditMode = value;
                    }
                    else if (Shape.Equals("checkboxgroup"))
                    {
                        (DataContext as CheckBoxGroupBusinessClass).EditMode = value;
                    }
                    else if (Shape.Equals("radiobuttongroup"))
                    {
                        (DataContext as RadioButtonGroupBusinessClass).EditMode = value;
                    }
                    else if(Shape.Equals("menubar"))
                    {
                        (DataContext as MenuBarBusinessClass).EditMode = value;
                    }
                    else if(Shape.Equals("menu"))
                    {
                        (DataContext as MenuBusinessClass).EditMode = value;
                    }
                    else if(Shape.Equals("list"))
                    {
                        (DataContext as ListBusinessClass).EditMode = value;
                    }
                    else if (Shape.Equals("combobox"))
                    {
                        (DataContext as ComboBoxBusinessClass).EditMode = value;
                    }
                    else if(Shape.Equals("breadcrumbs"))
                    {
                        (DataContext as BreadcrumbsBusinessClass).EditMode = value;
                    }
                    else if(Shape.Equals("linkbar"))
                    {
                        (DataContext as LinkBarBusinessClass).EditMode = value;
                    }
                    else if (Shape.Equals("alertbox"))
                    {
                        (DataContext as DialogBoxBusinessObject).EditMode = value;
                        if (value)
                        {
                            (DataContext as DialogBoxBusinessObject).OkEditModeOpacity = 1;
                            (DataContext as DialogBoxBusinessObject).CancelEditModeOpacity = 1;
                        }
                    }
                    else if (Shape.Equals("messagebox"))
                    {
                        (DataContext as MessageBoxBusinessObject).EditMode = value;
                    }
                }
            }
        }

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        public Brush EditModeFill
        {
            get { return _mEditModeFill; }
            set
            {
                if (_mEditModeFill != value)
                {
                    _mEditModeFill = value;
                    OnPropertyChanged("EditModeFill");
                }
            }
        }

        private double _mEditModeOpacity;
        public double EditModeOpacity
        {
            get { return _mEditModeOpacity; }
            set
            {
                if (_mEditModeOpacity != value)
                {
                    _mEditModeOpacity = value;
                    OnPropertyChanged("EditModeOpacity");
                }
            }
        }
        

        private HyperlinkBusinessClass _mLink;        
        public HyperlinkBusinessClass Link
        {
            get { return _mLink; }
            set
            {
                _mLink = value;
            }
        }

        private string _mDummyLink = string.Empty;
        [DataMember]
        public string DummyLink
        {
            get
            {
                XmlSerializer serializer = new XmlSerializer(typeof(HyperlinkBusinessClass));
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = Encoding.Unicode; // new UnicodeEncoding(false, false); // no BOM in a .NET string
                settings.Indent = false;
                settings.OmitXmlDeclaration = false;
                string str;
                using (System.IO.StringWriter textWriter = new System.IO.StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                    {

                        serializer.Serialize(xmlWriter, Link as HyperlinkBusinessClass);
                    }
                    str = textWriter.ToString();
                }

                return str;
            }
            set
            {
                if (value.Contains("utf-16"))
                    value = value.Replace("utf-16", "utf-8");
                //System.IO.Stream stream = GenerateStreamFromString(value);

                XmlSerializer deserializer = new XmlSerializer(typeof(HyperlinkBusinessClass));

                // Encode the XML string in a UTF-8 byte array
                byte[] encodedString = Encoding.UTF8.GetBytes(value);

                // Put the byte array into a stream and rewind it to the beginning
                System.IO.MemoryStream ms = new System.IO.MemoryStream(encodedString);
                ms.Flush();
                ms.Position = 0;

                object obj = deserializer.Deserialize(ms);
                this.Link = obj as HyperlinkBusinessClass;
            }
        }

        public DiagramMenu Menu { get; set; }

        protected override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            switch (name)
            {
                case NodeConstants.Content:
                    OnContentChanged();
                    break;
                case NodeConstants.ContentTemplate:
                    OnContentTemplateChanged();
                    break;
                case NodeConstants.Constraints:
                    OnConstraintsChanged();
                    break;
                case NodeConstants.Fill:
                    OnFillChanged();
                    break;
                case NodeConstants.AllowDrag:
                    OnAllowDragChanged();
                    break;
                case NodeConstants.AllowResize:
                    OnAllowResizeChanged();
                    break;
                case NodeConstants.AllowRotate:
                    OnAllowRotateChanged();
                    break;
                case GroupableConstants.UnitWidth:
                    OnUnitWidthChanged();
                    break;
            }
        }

        private void OnContentChanged()
        {
            if (Content is string)
            {
                ContentDummy = (string)Content;
                OnFillChanged();
            }
            //if (this.ContentTemplate != App.Current.Resources["NodeShapeTemplate"])
            //{
            //    this.ContentTemplate = App.Current.Resources["NodeShapeTemplate"] as DataTemplate;
            //}
        }

        private readonly char[] _mCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        private void OnContentTemplateChanged()
        {
#if DiagramBuilder
            this.ContentTemplate = null;
#endif 
            #region Wireframe
            //#region Popover
            //if (Shape.Equals("popover"))
            //    {
            //        UnitWidth = 150;
            //        UnitHeight = 150;
            //        FontSize = 14;
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        LabelHAlign = HorizontalAlignment.Left;
            //        LabelVAlign = VerticalAlignment.Top;                    
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.LabelTextWrapping = TextWrapping.Wrap;
            //            _mAnnotation.LabelMargin = new Thickness(5);
            //            _mAnnotation.ViewTemplate = App.Current.Resources["defaultviewtemplate"] as DataTemplate;
            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region geometricshapes
            //    else if (Shape.Equals("geometricshapes"))
            //    {
            //        UnitWidth = 50;
            //        UnitHeight = 50;
            //        FontSize = 14;
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        Constraints = Constraints | NodeConstraints.AspectRatio;
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["defaultviewtemplate"] as DataTemplate;
            //            _mAnnotation.LabelTextWrapping = TextWrapping.Wrap;
            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region volume
            //    else if (Shape.Equals("volume"))
            //    {
            //        UnitWidth = 50;
            //        UnitHeight = 50;
            //        Constraints = Constraints | NodeConstraints.AspectRatio;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region fieldset
            //    else if (Shape.Equals("fieldset"))
            //    {
            //        UnitWidth = 235;
            //        UnitHeight = 200;
            //        Fill = new SolidColorBrush(Colors.White);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["fieldsetviewtemplate"] as DataTemplate;
            //            _mAnnotation.EditTemplate = App.Current.Resources["textinputedittemplate"] as DataTemplate;
            //            _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
            //            _mAnnotation.Content = "Group Name";
            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region formatingtool
            //    else if (Shape.Equals("formatingtool"))
            //    {
            //        UnitWidth = 235;
            //        AvoidLabelEditMode();
            //    }
            // #endregion
            //#region redx
            //    else if (Shape.Equals("redx"))
            //    {
            //        UnitWidth = 180;
            //        UnitHeight = 90;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region calendar
            //    else if (Shape.Equals("calendar"))
            //    {
            //        UnitWidth = 180;
            //        UnitHeight = 180;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region ipad
            //    else if (Shape.Equals("ipad"))
            //    {
            //        UnitWidth = 320;
            //        UnitHeight = 420;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region iphone
            //    else if (Shape.Equals("iphone"))
            //    {
            //        UnitWidth = 150;
            //        UnitHeight = 300;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region ioskeyboard
            //    else if (Shape.Equals("ioskeyboard"))
            //    {
            //        //UnitWidth = 380;
            //        //UnitHeight = 200;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region webcam
            //    else if (Shape.Equals("webcam"))
            //    {
            //        UnitWidth = 180;
            //        UnitHeight = 160;
            //        Constraints = Constraints | NodeConstraints.AspectRatio;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region textarea
            //    else if (Shape.Equals("textarea"))
            //    {
            //        UnitWidth = 150;
            //        Stroke = new SolidColorBrush(Colors.Black);
            //        Fill = new SolidColorBrush(Colors.White);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["textareaviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
            //            _mAnnotation.Content = "";
            //            Label = _mAnnotation.Content.ToString();
            //        }
            //    }
            //    #endregion
            //#region textinput
            //    else if (Shape.Equals("textinput"))
            //    {
            //        UnitHeight = 35;
            //        UnitWidth = 150;
            //        FontSize = 14;
            //        LabelMargin = new Thickness(10);
            //        Stroke = new SolidColorBrush(Colors.Black);
            //        Fill = new SolidColorBrush(Colors.White);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            Size textsize = new Size();
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.ViewTemplate = App.Current.Resources["textinputviewtemplate"] as DataTemplate;
            //            _mAnnotation.EditTemplate = App.Current.Resources["textinputedittemplate"] as DataTemplate;
            //            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
            //            _mAnnotation.Content = "";
            //            Label = _mAnnotation.Content.ToString();
            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    textsize = new Size();
            //                    Label = (s as LabelVM).Content.ToString();
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    textsize = new Size();
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region label
            //    else if (Shape.Equals("label"))
            //    {
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["labelviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.LabelForeground = new SolidColorBrush(Colors.Black);
            //            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
            //            _mAnnotation.Content = "Some Text";
            //            Label = _mAnnotation.Content.ToString();
            //        }
            //    }
            //    #endregion
            //#region rectangle
            //    else if (Shape.Equals("rectangle"))
            //    {
            //        UnitWidth = 200;
            //        UnitHeight = 100;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region modelscreen
            //    else if (Shape.Equals("modelscreen"))
            //    {
            //        UnitWidth = 320;
            //        UnitHeight = 240;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region menubar
            //    else if (Shape.Equals("menubar"))
            //    {
            //        UnitHeight = 40;
            //        UnitWidth = 200;
            //        FontSize = 14;
            //        LabelMargin = new Thickness(5);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["menubarviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            foreach (var str in (_mAnnotation.DataContext as MenuBarBusinessClass).Items)
            //            {
            //                if (_mAnnotation.Content == null)
            //                {
            //                    _mAnnotation.Content = (str as MenuTitleBusinessClass).Item;
            //                }
            //                else
            //                {
            //                    _mAnnotation.Content = _mAnnotation.Content.ToString() + "," + (str as MenuTitleBusinessClass).Item;
            //                }
            //            }
            //            Label = _mAnnotation.Content.ToString();

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    string[] words = (s as LabelVM).Content.ToString().Split(',');

            //                    #region For HyperLinks
            //                    Dictionary<string, int> links = new Dictionary<string, int>();
            //                    for (int i = 0; i < ((s as LabelVM).DataContext as MenuBarBusinessClass).Items.Count; i++)
            //                    {
            //                        if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as MenuBarBusinessClass).Items[i] as MenuTitleBusinessClass).Link.Link))
            //                        {
            //                            links.Add((((s as LabelVM).DataContext as MenuBarBusinessClass).Items[i] as MenuTitleBusinessClass).Link.Link, i);
            //                        }
            //                    }
            //                    Dictionary<string, int> templinks = new Dictionary<string, int>();
            //                    int x = 0;
            //                    foreach (var i in links)
            //                    {
            //                        if (words.Contains((((s as LabelVM).DataContext as MenuBarBusinessClass).Items[i.Value] as MenuTitleBusinessClass).Item))
            //                        {
            //                            int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as MenuBarBusinessClass).Items[i.Value] as MenuTitleBusinessClass).Item);
            //                            templinks.Add(i.Key, mm);
            //                        }
            //                        else
            //                            templinks.Add(i.Key, i.Value);
            //                        x++;
            //                    }
            //                    links = null;
            //                    #endregion

            //                    double textwidth = 0;
            //                    ((s as LabelVM).DataContext as MenuBarBusinessClass).Items.Clear();
            //                    foreach (string str in words)
            //                    {
            //                        ((s as LabelVM).DataContext as MenuBarBusinessClass).Items.Add(new MenuTitleBusinessClass() { Item = str });
            //                        Size textsize = FindDummyTextSize(_mAnnotation, str, TextWrapping.NoWrap);
            //                        textwidth = textwidth + textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    }
            //                    ((s as LabelVM).DataContext as MenuBarBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as MenuBarBusinessClass).SelectedIndex;

            //                    #region For HyperLinks
            //                    foreach (var i in templinks)
            //                    {
            //                        (((s as LabelVM).DataContext as MenuBarBusinessClass).Items[i.Value] as MenuTitleBusinessClass).Link = new HyperlinkBusinessClass()
            //                        {
            //                            Title = (((s as LabelVM).DataContext as MenuBarBusinessClass).Items[i.Value] as MenuTitleBusinessClass).Item,
            //                            Link = i.Key
            //                        };
            //                    }
            //                    templinks = null;
            //                    #endregion

            //                    if (textwidth > UnitWidth)
            //                        UnitWidth = textwidth;
            //                }
            //                Label = (s as LabelVM).Content.ToString();
            //            };
            //        }
            //    }
            //    #endregion
            //#region menu
            //    else if (Shape.Equals("menu"))
            //    {
            //        UnitWidth = 240;
            //        FontSize = 14;
            //        LabelMargin = new Thickness(0, 5, 5, 5);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["menuviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            foreach (var str in (_mAnnotation.DataContext as MenuBusinessClass).Items)
            //            {
            //                if (_mAnnotation.Content == null)
            //                {
            //                    _mAnnotation.Content = (str as MenuItemBusinessClass).Item;// +"," + (str as MenuItemBusinessClass).ShortcutKey;
            //                    if (!string.IsNullOrEmpty((str as MenuItemBusinessClass).ShortcutKey))
            //                    {
            //                        _mAnnotation.Content = _mAnnotation.Content + "," + (str as MenuItemBusinessClass).ShortcutKey;
            //                    }
            //                }
            //                else
            //                {
            //                    _mAnnotation.Content = _mAnnotation.Content.ToString() + "\n" + (str as MenuItemBusinessClass).Item;// +"," + (str as MenuItemBusinessClass).ShortcutKey;
            //                    if (!string.IsNullOrEmpty((str as MenuItemBusinessClass).ShortcutKey))
            //                    {
            //                        _mAnnotation.Content = _mAnnotation.Content + "," + (str as MenuItemBusinessClass).ShortcutKey;
            //                    }
            //                }

            //                int indexOf = (str as MenuItemBusinessClass).Item.IndexOfAny(_mCharacters);
            //                if (indexOf == -1)
            //                {
            //                    (str as MenuItemBusinessClass).BreakLine = Visibility.Visible;
            //                }
            //            }

            //            Label = _mAnnotation.Content.ToString();

            //            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //            UnitHeight = (_mAnnotation.DataContext as MenuBusinessClass).Items.Count() * (textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom);

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Mode")
            //                {
            //                    if ((s as LabelVM).Mode == ContentEditorMode.Edit)
            //                    {
            //                        string editableString = string.Empty;
            //                        foreach (var str in ((s as LabelVM).DataContext as MenuBusinessClass).Items)
            //                        {
            //                            if (string.IsNullOrEmpty(editableString))
            //                            {
            //                                editableString = (str as MenuItemBusinessClass).EditModeItem;// +"," + (str as MenuItemBusinessClass).ShortcutKey;
            //                                if (!string.IsNullOrEmpty((str as MenuItemBusinessClass).ShortcutKey))
            //                                {
            //                                    editableString = editableString + "," + (str as MenuItemBusinessClass).ShortcutKey;
            //                                }
            //                            }
            //                            else
            //                            {
            //                                editableString = editableString + "\n" + (str as MenuItemBusinessClass).EditModeItem;//  +"," + (str as MenuItemBusinessClass).ShortcutKey;
            //                                if (!string.IsNullOrEmpty((str as MenuItemBusinessClass).ShortcutKey))
            //                                {
            //                                    editableString = editableString + "," + (str as MenuItemBusinessClass).ShortcutKey;
            //                                }
            //                            }
            //                        }
            //                        Label = editableString;
            //                    }
            //                }
            //                else if (e.PropertyName == "Content" && (s as LabelVM).Mode == ContentEditorMode.View)
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                    string[] words = (s as LabelVM).Content.ToString().Split('\n');

            //                    Dictionary<int, string> remover = new Dictionary<int, string>();
            //                    for (int i = 0; i < words.Count(); i++)
            //                    {
            //                        if (words[i].Contains("\r"))
            //                            remover.Add(i, words[i].Remove(words[i].Length - 1, 1));
            //                    }
            //                    foreach (var i in remover)
            //                    {
            //                        words[i.Key] = i.Value;
            //                    }
            //                    remover = null;

            //                    #region For HyperLinks
            //                    Dictionary<string, int> links = new Dictionary<string, int>();
            //                    for (int i = 0; i < ((s as LabelVM).DataContext as MenuBusinessClass).Items.Count; i++)
            //                    {
            //                        if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as MenuBusinessClass).Items[i] as MenuItemBusinessClass).Link.Link))
            //                        {
            //                            links.Add((((s as LabelVM).DataContext as MenuBusinessClass).Items[i] as MenuItemBusinessClass).Link.Link, i);
            //                        }
            //                    }
            //                    Dictionary<string, int> templinks = new Dictionary<string, int>();
            //                    int x = 0;
            //                    foreach (var i in links)
            //                    {
            //                        if (words.Contains((((s as LabelVM).DataContext as MenuBusinessClass).Items[i.Value] as MenuItemBusinessClass).Item))
            //                        {
            //                            int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as MenuBusinessClass).Items[i.Value] as MenuItemBusinessClass).Item);
            //                            templinks.Add(i.Key, mm);
            //                        }
            //                        else
            //                            templinks.Add(i.Key, i.Value);
            //                        x++;
            //                    }
            //                    links = null;
            //                    #endregion

            //                    ((s as LabelVM).DataContext as MenuBusinessClass).Items.Clear();
            //                    foreach (string str in words)
            //                    {
            //                        string[] childsplit = str.Split(',');
            //                        var item = new MenuItemBusinessClass() { Item = childsplit[0], Foreground = (s as LabelVM).LabelForeground };//, ShortcutKey=childsplit[1], Foreground = (s as LabelVM).LabelForeground };

            //                        if (childsplit.Count() > 1)
            //                        {
            //                            item.ShortcutKey = childsplit[1];
            //                        }
            //                        ((s as LabelVM).DataContext as MenuBusinessClass).Items.Add(item);
            //                        int indexOf = childsplit[0].IndexOfAny(_mCharacters);
            //                        if (indexOf == -1)
            //                        {
            //                            item.BreakLine = Visibility.Visible;
            //                        }
            //                    }

            //                    #region For HyperLinks
            //                    foreach (var i in templinks)
            //                    {
            //                        (((s as LabelVM).DataContext as MenuBusinessClass).Items[i.Value] as MenuItemBusinessClass).Link = new HyperlinkBusinessClass()
            //                        {
            //                            Title = (((s as LabelVM).DataContext as MenuBusinessClass).Items[i.Value] as MenuItemBusinessClass).Item,
            //                            Link = i.Key
            //                        };
            //                    }
            //                    templinks = null;
            //                    #endregion

            //                    ((s as LabelVM).DataContext as MenuBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as MenuBusinessClass).SelectedIndex;
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    UnitHeight = (_mAnnotation.DataContext as MenuBusinessClass).Items.Count() * (textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom);
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region breadcrumbs
            //    else if (Shape.Equals("breadcrumbs"))
            //    {
            //        FontSize = 14;
            //        LabelMargin = new Thickness(0, 0, 5, 0);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["breadcrumbsviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            Size textsize = new Size();
            //            foreach (var str in ((_mAnnotation.DataContext as BreadcrumbsBusinessClass).Items as ObservableCollection<object>))
            //            {
            //                if (_mAnnotation.Content == null)
            //                {
            //                    _mAnnotation.Content = (str as BreadcrumbsItemBusinessClass).Item;
            //                    Size localsize = FindDummyTextSize(_mAnnotation, (str as BreadcrumbsItemBusinessClass).Item, TextWrapping.NoWrap);
            //                    textsize.Width = textsize.Width + localsize.Width + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconWidth + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    textsize.Height = localsize.Height + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconHeight;
            //                }
            //                else
            //                {
            //                    _mAnnotation.Content = _mAnnotation.Content.ToString() + "," + (str as BreadcrumbsItemBusinessClass).Item;
            //                    Size localsize = FindDummyTextSize(_mAnnotation, (str as BreadcrumbsItemBusinessClass).Item, TextWrapping.NoWrap);
            //                    textsize.Width = textsize.Width + localsize.Width + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconWidth + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    textsize.Height = localsize.Height + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconHeight;
            //                }
            //            }
            //            Label = _mAnnotation.Content.ToString();
            //            //Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //            //(args.Item as INodeVM).UnitWidth = textsize.Width + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconWidth + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //            //(args.Item as INodeVM).UnitHeight = textsize.Height + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconHeight;

            //            UnitWidth = textsize.Width;
            //            UnitHeight = textsize.Height;

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    string[] words = (s as LabelVM).Content.ToString().Split(',');
            //                    textsize = new Size();
            //                    #region For HyperLinks
            //                    Dictionary<string, int> links = new Dictionary<string, int>();
            //                    for (int i = 0; i < ((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items.Count; i++)
            //                    {
            //                        if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[i] as BreadcrumbsItemBusinessClass).Link.Link))
            //                        {
            //                            links.Add((((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[i] as BreadcrumbsItemBusinessClass).Link.Link, i);
            //                        }
            //                    }
            //                    Dictionary<string, int> templinks = new Dictionary<string, int>();
            //                    int x = 0;
            //                    foreach (var i in links)
            //                    {
            //                        if (words.Contains((((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[i.Value] as BreadcrumbsItemBusinessClass).Item))
            //                        {
            //                            int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[i.Value] as BreadcrumbsItemBusinessClass).Item);
            //                            templinks.Add(i.Key, mm);
            //                        }
            //                        else
            //                            templinks.Add(i.Key, i.Value);
            //                        x++;
            //                    }
            //                    links = null;
            //                    #endregion

            //                    (((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items).Clear();

            //                    foreach (string str in words)
            //                    {
            //                        ((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items.Add
            //                        (
            //                            new BreadcrumbsItemBusinessClass() { Item = str }
            //                        );
            //                        Size localsize = FindDummyTextSize(_mAnnotation, str, TextWrapping.NoWrap);
            //                        textsize.Width = textsize.Width + localsize.Width + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconWidth + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                        textsize.Height = localsize.Height + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconHeight;
            //                    }



            //                    #region For HyperLinks
            //                    foreach (var i in templinks)
            //                    {
            //                        (((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[i.Value] as BreadcrumbsItemBusinessClass).Link = new HyperlinkBusinessClass()
            //                        {
            //                            Title = (((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[i.Value] as BreadcrumbsItemBusinessClass).Item,
            //                            Link = i.Key
            //                        };
            //                    }
            //                    templinks = null;
            //                    #endregion
            //                    UnitWidth = textsize.Width;
            //                    UnitHeight = textsize.Height;
            //                    (((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items[words.Count() - 1] as BreadcrumbsItemBusinessClass).IsLastItem = true;

            //                    //textsize = FindDummyTextSize((s as LabelVM), (s as LabelVM).Content.ToString(), TextWrapping.NoWrap);
            //                    //(args.Item as INodeVM).UnitWidth = textsize.Width + ((s as LabelVM).DataContext as BreadcrumbsBusinessClass).ArrowIconWidth + (s as LabelVM).LabelMargin.Left + (s as LabelVM).LabelMargin.Right;
            //                    //(args.Item as INodeVM).UnitHeight = textsize.Height + ((s as LabelVM).DataContext as BreadcrumbsBusinessClass).ArrowIconHeight;
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    textsize = new Size();
            //                    foreach (var item in ((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items)
            //                    {
            //                        Size localsize = FindDummyTextSize(_mAnnotation, (item as BreadcrumbsItemBusinessClass).Item, TextWrapping.NoWrap);
            //                        textsize.Width = textsize.Width + localsize.Width + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconWidth + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                        textsize.Height = localsize.Height + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconHeight;
            //                    }
            //                    UnitWidth = textsize.Width;
            //                    UnitHeight = textsize.Height;
            //                }
            //                else if (e.PropertyName == "FontWeight")
            //                {
            //                    textsize = new Size();
            //                    foreach (var item in ((s as LabelVM).DataContext as BreadcrumbsBusinessClass).Items)
            //                    {
            //                        Size localsize = FindDummyTextSize(_mAnnotation, (item as BreadcrumbsItemBusinessClass).Item, TextWrapping.NoWrap);
            //                        textsize.Width = textsize.Width + localsize.Width + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconWidth + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                        textsize.Height = localsize.Height + (_mAnnotation.DataContext as BreadcrumbsBusinessClass).ArrowIconHeight;
            //                    }
            //                    UnitWidth = textsize.Width;
            //                    UnitHeight = textsize.Height;
            //                }
            //                Label = (s as LabelVM).Content.ToString();
            //            };
            //        }
            //    }
            //    #endregion
            //#region list
            //    else if (Shape.Equals("list"))
            //    {
            //        Fill = new SolidColorBrush(Colors.White);
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        FontSize = 14;
            //        UnitWidth = 120;
            //        UnitHeight = 160;
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["listviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            (_mAnnotation.DataContext as ListBusinessClass).OddItemsBackgroud = new SolidColorBrush(Colors.White);
            //            (_mAnnotation.DataContext as ListBusinessClass).EvenItemsBackgroud = new SolidColorBrush(Colors.White);
            //            foreach (var str in (_mAnnotation.DataContext as ListBusinessClass).Items)
            //            {
            //                if (_mAnnotation.Content == null)
            //                {
            //                    _mAnnotation.Content = (str as ListItemBusinessClass).Item;
            //                }
            //                else
            //                {
            //                    _mAnnotation.Content = _mAnnotation.Content.ToString() + "\t\n" + (str as ListItemBusinessClass).Item;
            //                }
            //            }
            //            Label = _mAnnotation.Content.ToString();

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    string[] words = (s as LabelVM).Content.ToString().Split('\n');

            //                    #region For HyperLinks
            //                    Dictionary<string, int> links = new Dictionary<string, int>();
            //                    for (int i = 0; i < ((s as LabelVM).DataContext as ListBusinessClass).Items.Count; i++)
            //                    {
            //                        if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as ListBusinessClass).Items[i] as ListItemBusinessClass).Link.Link))
            //                        {
            //                            links.Add((((s as LabelVM).DataContext as ListBusinessClass).Items[i] as ListItemBusinessClass).Link.Link, i);
            //                        }
            //                    }
            //                    Dictionary<string, int> templinks = new Dictionary<string, int>();
            //                    int x = 0;
            //                    foreach (var i in links)
            //                    {
            //                        if (words.Contains((((s as LabelVM).DataContext as ListBusinessClass).Items[i.Value] as ListItemBusinessClass).Item))
            //                        {
            //                            int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as ListBusinessClass).Items[i.Value] as ListItemBusinessClass).Item);
            //                            templinks.Add(i.Key, mm);
            //                        }
            //                        else
            //                            templinks.Add(i.Key, i.Value);
            //                        x++;
            //                    }
            //                    links = null;
            //                    #endregion

            //                    ((s as LabelVM).DataContext as ListBusinessClass).Items.Clear();
            //                    //(this.SelectedItems as SelectorVM).TabItem.Clear();

            //                    foreach (var str in words)
            //                    {
            //                       // (this.SelectedItems as SelectorVM).TabItem.Add(new ListItemBusinessClass() { Item = str });

            //                        ((s as LabelVM).DataContext as ListBusinessClass).Items.Add(new ListItemBusinessClass()
            //                        {
            //                            Item = str,
            //                        }
            //                        );
            //                    }
            //                    ((s as LabelVM).DataContext as ListBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as ListBusinessClass).SelectedIndex;

            //                    #region For HyperLinks
            //                    foreach (var i in templinks)
            //                    {
            //                        (((s as LabelVM).DataContext as ListBusinessClass).Items[i.Value] as ListItemBusinessClass).Link = new HyperlinkBusinessClass()
            //                        {
            //                            Title = (((s as LabelVM).DataContext as ListBusinessClass).Items[i.Value] as ListItemBusinessClass).Item,
            //                            Link = i.Key
            //                        };
            //                    }
            //                    templinks = null;
            //                    #endregion
            //                }
            //                Label = (s as LabelVM).Content.ToString();
            //            };
            //        }
            //    }
            //    #endregion
            //#region link
            //    else if (Shape.Equals("link"))
            //    {
            //        LabelForeground = new SolidColorBrush(Colors.Blue);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["linkviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
            //            _mAnnotation.VerticalAlignment = VerticalAlignment.Center;
            //            _mAnnotation.Content = "href";
            //            Label = _mAnnotation.Content.ToString();
            //            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //            UnitWidth = textsize.Width;
            //            UnitHeight = textsize.Height;

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                    textsize = FindDummyTextSize(s as LabelVM, (s as LabelVM).Content.ToString(), TextWrapping.NoWrap);
            //                    UnitWidth = textsize.Width;
            //                    UnitHeight = textsize.Height;
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    textsize = FindDummyTextSize(s as LabelVM, (s as LabelVM).Content.ToString(), TextWrapping.NoWrap);
            //                    UnitWidth = textsize.Width;
            //                    UnitHeight = textsize.Height;
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region linkbar
            //    else if (Shape.Equals("linkbar"))
            //    {
            //        FontSize = 14;
            //        LabelMargin = new Thickness(5, 0, 5, 0);
            //        Stroke = new SolidColorBrush(Colors.Black);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["linkbarviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            Size textsize = new Size();
            //            foreach (var str in (_mAnnotation.DataContext as LinkBarBusinessClass).Items)
            //            {
            //                if (_mAnnotation.Content == null)
            //                {
            //                    _mAnnotation.Content = (str as LinkBarItemBusinessClass).Item;
            //                    Size localsize = FindDummyTextSize(_mAnnotation, (str as LinkBarItemBusinessClass).Item, TextWrapping.NoWrap);
            //                    textsize.Width = textsize.Width + localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    textsize.Height = localsize.Height;
            //                }
            //                else
            //                {
            //                    _mAnnotation.Content = _mAnnotation.Content.ToString() + "," + (str as LinkBarItemBusinessClass).Item;
            //                    Size localsize = FindDummyTextSize(_mAnnotation, (str as LinkBarItemBusinessClass).Item, TextWrapping.NoWrap);
            //                    textsize.Width = textsize.Width + localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                }
            //            }
            //            Label = _mAnnotation.Content.ToString();
            //            UnitWidth = textsize.Width;
            //            UnitHeight = textsize.Height;

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    textsize = new Size();
            //                    string[] words = (s as LabelVM).Content.ToString().Split(',');

            //                    #region For HyperLinks
            //                    Dictionary<string, int> links = new Dictionary<string, int>();
            //                    for (int i = 0; i < ((s as LabelVM).DataContext as LinkBarBusinessClass).Items.Count; i++)
            //                    {
            //                        if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i] as LinkBarItemBusinessClass).Link.Link))
            //                        {
            //                            links.Add((((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i] as LinkBarItemBusinessClass).Link.Link, i);
            //                        }
            //                    }
            //                    Dictionary<string, int> templinks = new Dictionary<string, int>();
            //                    int x = 0;
            //                    foreach (var i in links)
            //                    {
            //                        if (words.Contains((((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i.Value] as LinkBarItemBusinessClass).Item))
            //                        {
            //                            int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i.Value] as LinkBarItemBusinessClass).Item);
            //                            templinks.Add(i.Key, mm);
            //                        }
            //                        else
            //                            templinks.Add(i.Key, i.Value);
            //                        x++;
            //                    }
            //                    links = null;
            //                    #endregion

            //                    ((s as LabelVM).DataContext as LinkBarBusinessClass).Items.Clear();
            //                    foreach (string str in words)
            //                    {
            //                        ((s as LabelVM).DataContext as LinkBarBusinessClass).Items.Add
            //                        (
            //                             new LinkBarItemBusinessClass() { Item = str }
            //                        );
            //                        Size localsize = FindDummyTextSize(_mAnnotation, str, TextWrapping.NoWrap);
            //                        textsize.Width = textsize.Width + localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                        textsize.Height = localsize.Height;
            //                    }

            //                    (((s as LabelVM).DataContext as LinkBarBusinessClass).Items[((s as LabelVM).DataContext as LinkBarBusinessClass).Items.Count - 1] as LinkBarItemBusinessClass).IsLastItem = true;
            //                    UnitWidth = textsize.Width;
            //                    UnitHeight = textsize.Height;
            //                    ((s as LabelVM).DataContext as LinkBarBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as LinkBarBusinessClass).SelectedIndex;

            //                    #region For HyperLinks
            //                    foreach (var i in templinks)
            //                    {
            //                        (((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i.Value] as LinkBarItemBusinessClass).Link = new HyperlinkBusinessClass()
            //                        {
            //                            Title = (((s as LabelVM).DataContext as LinkBarBusinessClass).Items[i.Value] as LinkBarItemBusinessClass).Item,
            //                            Link = i.Key
            //                        };
            //                    }
            //                    templinks = null;
            //                    #endregion

            //                    Label = (s as LabelVM).Content.ToString();
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    textsize = new Size();
            //                    foreach (var str in ((s as LabelVM).DataContext as LinkBarBusinessClass).Items)
            //                    {
            //                        Size localsize = FindDummyTextSize(_mAnnotation, (str as LinkBarItemBusinessClass).Item, TextWrapping.NoWrap);
            //                        textsize.Width = textsize.Width + localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                        textsize.Height = localsize.Height;
            //                    }
            //                    UnitWidth = textsize.Width;
            //                    UnitHeight = textsize.Height;
            //                }
            //                else if (e.PropertyName == "FontWeight")
            //                {
            //                    textsize = new Size();
            //                    foreach (var str in ((s as LabelVM).DataContext as LinkBarBusinessClass).Items)
            //                    {
            //                        Size localsize = FindDummyTextSize(_mAnnotation, (str as LinkBarItemBusinessClass).Item, TextWrapping.NoWrap);
            //                        textsize.Width = textsize.Width + localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                        textsize.Height = localsize.Height;
            //                    }
            //                    UnitWidth = textsize.Width;
            //                    UnitHeight = textsize.Height;
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region streetmap
            //    else if (Shape.Equals("streetmap"))
            //    {
            //        UnitHeight = 160;
            //        UnitWidth = 200;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region videoPlayer
            //    else if (Shape.Equals("videoPlayer"))
            //    {
            //        UnitHeight = 200;
            //        UnitWidth = 300;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region Color Picker
            //    else if (Shape.Equals("colorpicker"))
            //    {
            //        Fill = new SolidColorBrush(Colors.Blue);
            //        UnitHeight = 50;
            //        UnitWidth = 50;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region playbackcontrol
            //    else if (Shape.Equals("playbackcontrol"))
            //    {
            //        UnitHeight = 45;
            //        UnitWidth = 150;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region Helpbuttons
            //    else if (Shape.Equals("helpbutton"))
            //    {
            //        UnitHeight = 50;
            //        UnitWidth = 50;
            //        Constraints = NodeConstraints.Default | NodeConstraints.AspectRatio;
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["hyperlinkonlyviewtemplate"] as DataTemplate;
            //            AvoidLabelEditMode();
            //        }
            //    }
            //    #endregion
            //#region onoffswitch
            //    else if (Shape.Equals("onoffswitch"))
            //    {
            //        this.UnitWidth = 65;
            //        this.UnitHeight = 33;
            //        Fill = GetColorFromHexa("#83c1f0");
            //        #region Annotation
            //        foreach (LabelVM _mAnnotation in this.Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["onoffswitchviewtemplate"] as DataTemplate;
            //            _mAnnotation.EditTemplate = null;
            //            _mAnnotation.DataContext = this.DataContext;
            //            _mAnnotation.Content = "On";
            //            _mAnnotation.LabelMargin = new Thickness(0, 0, 10, 0);
            //            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Right;
            //            this.Label = _mAnnotation.Content.ToString();

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Mode")
            //                {
            //                    if ((s as LabelVM).Mode == ContentEditorMode.Edit)
            //                        (s as LabelVM).Mode = ContentEditorMode.View;
            //                }
            //                else if (e.PropertyName == "Content")
            //                {
            //                    this.Label = (s as LabelVM).Content.ToString();
            //                }
            //            };
            //            (_mAnnotation.DataContext as OnOffSwitch).PropertyChanged += (s, e1) =>
            //            {
            //                if (e1.PropertyName == "On")
            //                {
            //                    if ((s as OnOffSwitch).On)
            //                        _mAnnotation.Content = "On";
            //                }
            //                else if (e1.PropertyName == "Off")
            //                {
            //                    if ((s as OnOffSwitch).Off)
            //                        _mAnnotation.Content = "Off";
            //                }
            //                else if (e1.PropertyName == "HorizontalAlignment")
            //                {
            //                    if ((s as OnOffSwitch).HorizontalAlignment == HorizontalAlignment.Right)
            //                    {
            //                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
            //                        _mAnnotation.LabelMargin = new Thickness(10, 0, 0, 0);
            //                    }
            //                    else if ((s as OnOffSwitch).HorizontalAlignment == HorizontalAlignment.Left)
            //                    {
            //                        _mAnnotation.HorizontalAlignment = HorizontalAlignment.Right;
            //                        _mAnnotation.LabelMargin = new Thickness(0, 0, 10, 0);
            //                    }
            //                }
            //            };

            //        }
            //        #endregion
            //    }
            //    #endregion
            //#region radiobuttongroup
            //    else if (Shape.Equals("radiobuttongroup"))
            //    {
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        FontSize = 14;
            //        Stroke = new SolidColorBrush(Colors.Black);
            //        UnitHeight = 100;
            //        UnitWidth = 160;
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["radiobuttongroupviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            Size textsize = new Size();
            //            _mAnnotation.LabelMargin = new Thickness(0, 5, 0, 5);
            //            foreach (var str in ((_mAnnotation.DataContext as RadioButtonGroupBusinessClass).Items as ObservableCollection<object>))
            //            {
            //                if (_mAnnotation.Content == null)
            //                {
            //                    _mAnnotation.Content = (str as RadioButtonItemBusinessClass).Item;
            //                    Size localsize = FindDummyTextSize(_mAnnotation, (str as RadioButtonItemBusinessClass).Item, TextWrapping.NoWrap);
            //                    if (localsize.Width > textsize.Width)
            //                        textsize.Width = localsize.Width;
            //                }
            //                else
            //                {
            //                    _mAnnotation.Content = _mAnnotation.Content.ToString() + "\t\n" + (str as RadioButtonItemBusinessClass).Item;
            //                    Size localsize = FindDummyTextSize(_mAnnotation, (str as RadioButtonItemBusinessClass).Item, TextWrapping.NoWrap);
            //                    if (localsize.Width > textsize.Width)
            //                        textsize.Width = localsize.Width;
            //                }
            //            }
            //            Label = "(o) Selected" + "\t\n" + "() Not Selected" + "\t\n" + "-() Disabled" + "\t\n" + "-(o) Disabled Selected" + "\t\n" + "Without RadioButton";
            //            (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //            UnitHeight = (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).ItemHeight * (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).Items.Count();
            //            // Added FontSize also becuase checkbox is in front 
            //            UnitWidth = textsize.Width + _mAnnotation.FontSize;

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                    string[] words = (s as LabelVM).Content.ToString().Split('\n');
            //                    textsize = new Size();
            //                    ((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items.Clear();
            //                    foreach (string str in words)
            //                    {
            //                        (((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items as ObservableCollection<object>).Add(new RadioButtonItemBusinessClass()
            //                        {
            //                            Item = str,
            //                            Stroke = ((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Stroke,
            //                            Foreground = ((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Foreground
            //                        });
            //                        Size localsize = FindDummyTextSize(_mAnnotation, str, TextWrapping.NoWrap);
            //                        if (localsize.Width > textsize.Width)
            //                            textsize.Width = localsize.Width;
            //                    }
            //                    (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    UnitHeight = (_mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom) * (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).Items.Count();
            //                    // Added FontSize also becuase checkbox is in front 
            //                    UnitWidth = textsize.Width + _mAnnotation.FontSize;
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    textsize = new Size();
            //                    foreach (var item in ((s as LabelVM).DataContext as RadioButtonGroupBusinessClass).Items)
            //                    {
            //                        Size localsize = FindDummyTextSize(_mAnnotation, (item as RadioButtonItemBusinessClass).Item, TextWrapping.NoWrap);
            //                        if (localsize.Width > textsize.Width)
            //                            textsize.Width = localsize.Width;
            //                    }
            //                    (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    UnitHeight = (_mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom) * (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).Items.Count();
            //                    // Added FontSize also becuase checkbox is in front 
            //                    UnitWidth = textsize.Width + _mAnnotation.FontSize;
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region checkboxgroup
            //    else if (Shape.Equals("checkboxgroup"))
            //    {
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        FontSize = 14;
            //        Stroke = new SolidColorBrush(Colors.Black);
            //        UnitHeight = 100;
            //        UnitWidth = 160;
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["checkboxgroupviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.LabelTextWrapping = TextWrapping.NoWrap;
            //            Size textsize = new Size();
            //            _mAnnotation.LabelMargin = new Thickness(0, 5, 0, 5);
            //            foreach (var str in ((_mAnnotation.DataContext as CheckBoxGroupBusinessClass).Items as ObservableCollection<object>))
            //            {
            //                if (_mAnnotation.Content == null)
            //                {
            //                    _mAnnotation.Content = (str as CheckBoxItemBusinessClass).Item;
            //                    Size localsize = FindDummyTextSize(_mAnnotation, (str as CheckBoxItemBusinessClass).Item, TextWrapping.NoWrap);
            //                    if (localsize.Width > textsize.Width)
            //                        textsize.Width = localsize.Width;
            //                }
            //                else
            //                {
            //                    _mAnnotation.Content = _mAnnotation.Content.ToString() + "\n" + (str as CheckBoxItemBusinessClass).Item;
            //                    Size localsize = FindDummyTextSize(_mAnnotation, (str as CheckBoxItemBusinessClass).Item, TextWrapping.NoWrap);
            //                    if (localsize.Width > textsize.Width)
            //                        textsize.Width = localsize.Width;
            //                }
            //            }
            //            if (Shape.Equals("checkboxgroup"))
            //            {
            //                Label = "[x] Selected" + "\n" + "[] Not Selected" + "\n" + "-[] Disabled" + "\n" + "-[x] Disabled Selected" + "\n" + "Without Checkbox";
            //            }

            //            (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //            UnitHeight = (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight * (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).Items.Count();
            //            // Added FontSize also becuase checkbox is in front 
            //            UnitWidth = textsize.Width + _mAnnotation.FontSize; ;

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                    string[] words = (s as LabelVM).Content.ToString().Split('\n');
            //                    textsize = new Size();
            //                    Dictionary<string, int> remover = new Dictionary<string, int>();
            //                    for (int i = 0; i < words.Count(); i++)
            //                    {
            //                        if (words[i].Contains("\r"))
            //                            remover.Add(words[i].Remove(words[i].Length - 1, 1), i);
            //                    }
            //                    foreach (var i in remover)
            //                    {
            //                        words[i.Value] = i.Key;
            //                    }
            //                    remover = null;

            //                    #region For HyperLinks
            //                    Dictionary<string, int> links = new Dictionary<string, int>();
            //                    for (int i = 0; i < ((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items.Count; i++)
            //                    {
            //                        if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i] as CheckBoxItemBusinessClass).Link.Link))
            //                        {
            //                            links.Add((((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i] as CheckBoxItemBusinessClass).Link.Link, i);
            //                        }
            //                    }
            //                    Dictionary<string, int> templinks = new Dictionary<string, int>();
            //                    int x = 0;
            //                    foreach (var i in links)
            //                    {
            //                        if (words.Contains((((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i.Value] as CheckBoxItemBusinessClass).Item))
            //                        {
            //                            int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i.Value] as CheckBoxItemBusinessClass).Item);
            //                            templinks.Add(i.Key, mm);
            //                        }
            //                        else
            //                            templinks.Add(i.Key, i.Value);
            //                        x++;
            //                    }
            //                    links = null;
            //                    #endregion

            //                    ((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items.Clear();
            //                    foreach (string str in words)
            //                    {
            //                        (((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items as ObservableCollection<object>).Add(new CheckBoxItemBusinessClass()
            //                        {
            //                            Item = str,
            //                            Stroke = ((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Stroke,
            //                            Foreground = ((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Foreground
            //                        });
            //                        Size localsize = FindDummyTextSize(_mAnnotation, str, TextWrapping.NoWrap);
            //                        if (localsize.Width > textsize.Width)
            //                            textsize.Width = localsize.Width;
            //                    }
            //                    (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    UnitHeight = (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight * (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).Items.Count();
            //                    // Added FontSize also becuase checkbox is in front 
            //                    UnitWidth = textsize.Width + _mAnnotation.FontSize;
            //                    #region For HyperLinks
            //                    foreach (var i in templinks)
            //                    {
            //                        (((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i.Value] as CheckBoxItemBusinessClass).Link = new HyperlinkBusinessClass()
            //                        {
            //                            Title = (((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items[i.Value] as CheckBoxItemBusinessClass).Item,
            //                            Link = i.Key
            //                        };
            //                    }
            //                    templinks = null;
            //                    #endregion
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    textsize = new Size();
            //                    foreach (var item in ((s as LabelVM).DataContext as CheckBoxGroupBusinessClass).Items)
            //                    {
            //                        Size localsize = FindDummyTextSize(_mAnnotation, (item as CheckBoxItemBusinessClass).Item, TextWrapping.NoWrap);
            //                        if (localsize.Width > textsize.Width)
            //                            textsize.Width = localsize.Width;
            //                    }
            //                    (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    UnitHeight = (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight * (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).Items.Count();
            //                    // Added FontSize also becuase checkbox is in front 
            //                    UnitWidth = textsize.Width + _mAnnotation.FontSize;
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region stickynote
            //    else if (Shape.Equals("stickynote"))
            //    {
            //        UnitWidth = 150;
            //        UnitHeight = 200;
            //        Fill = GetColorFromHexa("#ffdf1b");
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["stickynoteviewtemplate"] as DataTemplate;
            //            _mAnnotation.LabelMargin = new Thickness(10, 25, 10, 10);
            //            _mAnnotation.DataContext = DataContext;
            //            Annotations = new List<IAnnotation>() { _mAnnotation };
            //            _mAnnotation.Content = "Sticky Notes";
            //            Label = _mAnnotation.Content.ToString();

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region subtitle / bigtitle
            //    else if (Shape.Equals("subtitle") || Shape.Equals("bigtitle"))
            //    {
            //        UnitHeight = 30;
            //        FontSize = 14;
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            if (Shape.Equals("subtitle"))
            //            {
            //                _mAnnotation.ViewTemplate = App.Current.Resources["subtitleviewtemplate"] as DataTemplate;
            //                _mAnnotation.Content = "A Subtitle";
            //            }
            //            else
            //            {
            //                _mAnnotation.ViewTemplate = App.Current.Resources["bigtitleviewtemplate"] as DataTemplate;
            //                _mAnnotation.Content = "A Bigtitle";
            //                FontSize = 40;
            //            }
            //            _mAnnotation.EditTemplate = App.Current.Resources["textinputedittemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.LabelMargin = new Thickness(0);
            //            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
            //            _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
            //            _mAnnotation.LabelTextWrapping = TextWrapping.NoWrap;
            //            _mAnnotation.TextAlignment = TextAlignment.Left;

            //            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //            UnitWidth = textsize.Width;
            //            UnitHeight = textsize.Height;
            //            Label = _mAnnotation.Content.ToString();

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    UnitWidth = textsize.Width;
            //                    UnitHeight = textsize.Height;
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    UnitWidth = textsize.Width;
            //                    UnitHeight = textsize.Height;
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region horizontalcurlybraces
            //    else if (Shape.Equals("horizontalcurlybraces"))
            //    {
            //        Fill = new SolidColorBrush(Colors.Black);
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["horizontalcurlybracessviewtemplate"] as DataTemplate;
            //            _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.Content = "A Pararaph of Text";
            //            Label = _mAnnotation.Content.ToString();
            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region verticalcurlybraces
            //    else if (Shape.Equals("verticalcurlybraces"))
            //    {
            //        Fill = new SolidColorBrush(Colors.Black);
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["verticalcurlybracessviewtemplate"] as DataTemplate;
            //            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
            //            _mAnnotation.DataContext = DataContext;
            //            Annotations = new List<IAnnotation>() { _mAnnotation };
            //            _mAnnotation.Content = "A Pararaph of Text";
            //            Label = _mAnnotation.Content.ToString();
            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region numericstepper
            //    else if (Shape.Equals("numericstepper"))
            //    {
            //        UnitWidth = 60;
            //        FontSize = 14;
            //        LabelMargin = new Thickness(10, 5, 10, 5);
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        Stroke = new SolidColorBrush(Colors.Black);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["numericstepperviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.Content = "2";
            //            Label = _mAnnotation.Content.ToString();
            //            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //            UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    UnitWidth = UnitHeight + textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    UnitWidth = UnitHeight + textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region scratchout
            //    else if (Shape.Equals("scratchout"))
            //    {
            //        Fill = new SolidColorBrush(Colors.Black);
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region volumeslider
            //    else if (Shape.Equals("volumeslider"))
            //    {
            //        UnitWidth = 200;
            //        UnitHeight = 45;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region horizotalslider / verticalslider
            //    else if (Shape.Equals("horizotalslider") || Shape.Equals("verticalslider"))
            //    {
            //        if (Shape.Equals("horizotalslider"))
            //        {
            //            UnitWidth = 150;
            //            UnitHeight = 28;
            //        }
            //        else if (Shape.Equals("verticalslider"))
            //        {
            //            UnitHeight = 150;
            //            UnitWidth = 28;
            //        }
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region progressbar
            //    else if (Shape.Equals("progressbar"))
            //    {
            //        UnitHeight = 20;
            //        UnitWidth = 200;
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region Scrollbar
            //    else if (Shape.Equals("horizontalscrollbar") || Shape.Equals("verticalscrollbar"))
            //    {
            //        if (Shape.Equals("horizontalscrollbar"))
            //        {
            //            UnitWidth = 150;
            //            UnitHeight = 20;
            //        }
            //        else if (Shape.Equals("verticalscrollbar"))
            //        {
            //            UnitWidth = 20;
            //            UnitHeight = 150;
            //        }
            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region pointybutton
            //    else if (Shape.Equals("pointybutton"))
            //    {
            //        Fill = new SolidColorBrush(Colors.White);
            //        LabelMargin = new Thickness(10);
            //        FontSize = 14;
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["pointybuttonviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconSize = FontSize / 2;
            //            Annotations = new List<IAnnotation>() { _mAnnotation };
            //            _mAnnotation.Content = "Button";
            //            Label = _mAnnotation.Content.ToString();
            //            Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //            UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Left + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Right;
            //            UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Left + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Right;
            //                    UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconSize = FontSize / 2;
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Left + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Right;
            //                    UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region tooltip
            //    else if (Shape.Equals("tooltip"))
            //    {
            //        UnitHeight = 50;
            //        UnitWidth = 200;
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["tooltipviewtemplate"] as DataTemplate;
            //            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Center;
            //            _mAnnotation.VerticalAlignment = VerticalAlignment.Center;
            //            _mAnnotation.LabelMargin = new Thickness(10, 10, 10, 0);
            //            _mAnnotation.Content = "a tooltip";
            //            Label = _mAnnotation.Content.ToString();
            //            Size textsize = new Size();

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    //if (textsize.Width > UnitWidth)
            //                    //    UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    //if (textsize.Height > UnitHeight)
            //                    //    UnitHeight = textsize.Height  + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    //if (textsize.Width > UnitWidth)
            //                    //    UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    //if (textsize.Height > UnitHeight)
            //                    //    UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                }
            //            };
            //        }

            //    }
            //    #endregion
            //#region searchbutton
            //    else if (Shape.Equals("searchbutton")
            //        )
            //    {
            //        Stroke = new SolidColorBrush(Colors.Black);
            //        FontSize = 14;
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        UnitWidth = 160;
            //        UnitHeight = 30;
            //        LabelMargin = new Thickness(10, 5, 12, 5);
            //        LabelHAlign = HorizontalAlignment.Left;
            //        foreach (LabelVM _mAnnotation in Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
            //        {
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.ViewTemplate = App.Current.Resources["searchbuttonviewtemplate"] as DataTemplate;
            //            _mAnnotation.Content = "Search";
            //            Label = _mAnnotation.Content.ToString();
            //            Size textsize;
            //            (_mAnnotation.DataContext as SearchBoxBusinessClass).CornerRadius = new CornerRadius(UnitHeight / 2);
            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                    textsize = FindDummyTextSize(s as LabelVM, (s as LabelVM).Content.ToString(), TextWrapping.NoWrap);
            //                    //if (UnitWidth < textsize.Width)
            //                    {
            //                        // Here we have added FontSize also. This search symbol width
            //                        UnitWidth = textsize.Width + (s as LabelVM).FontSize + (s as LabelVM).LabelMargin.Left + (2 * (s as LabelVM).LabelMargin.Right);
            //                    }
            //                    //if (UnitHeight < textsize.Height)
            //                    UnitHeight = textsize.Height + (s as LabelVM).LabelMargin.Top + (s as LabelVM).LabelMargin.Bottom;
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    (_mAnnotation.DataContext as SearchBoxBusinessClass).CornerRadius = new CornerRadius(UnitHeight / 2);
            //                    textsize = FindDummyTextSize(s as LabelVM, (s as LabelVM).Content.ToString(), TextWrapping.NoWrap);
            //                    //if (UnitWidth < textsize.Width)
            //                    {
            //                        // Here we have added FontSize also. This search symbol width
            //                        UnitWidth = textsize.Width + (s as LabelVM).FontSize + (s as LabelVM).LabelMargin.Left + (2 * (s as LabelVM).LabelMargin.Right);
            //                    }
            //                    //if (UnitHeight < textsize.Height)
            //                    UnitHeight = textsize.Height + (s as LabelVM).LabelMargin.Top + (s as LabelVM).LabelMargin.Bottom;
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region button
            //    else if (Shape.Equals("button"))
            //    {
            //        FontSize = 14;
            //        Fill = GetColorFromHexa("#E2E2E2");
            //        Stroke = new SolidColorBrush(Colors.Black);
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.LabelMargin = new Thickness(15, 5, 15, 5);
            //            _mAnnotation.ViewTemplate = App.Current.Resources["buttonviewtemplate"] as DataTemplate;
            //            Annotations = new List<IAnnotation>() { _mAnnotation };
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.Content = "Button";
            //            Size textsize;
            //            textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //            UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //            UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //            Label = _mAnnotation.Content.ToString();

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                    UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                }
            //            };
            //        }


            //    }
            //    #endregion
            //#region combox
            //    else if (Shape.Equals("combobox"))
            //    {
            //        UnitWidth = 120;
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        Stroke = new SolidColorBrush(Colors.Black);
            //        foreach (LabelVM _mAnnotation in Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
            //        {
            //            _mAnnotation.FontSize = 14;
            //            _mAnnotation.LabelMargin = new Thickness(10, 5, 10, 5);
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.ViewTemplate = App.Current.Resources["comboboxviewtemplate"] as DataTemplate;
            //            _mAnnotation.EditTemplate = App.Current.Resources["comboboxedittemplate"] as DataTemplate;
            //            _mAnnotation.Content = (_mAnnotation.DataContext as ComboBoxBusinessClass).Title;
            //            Size textsize = FindDummyTextSize(_mAnnotation, (_mAnnotation.DataContext as ComboBoxBusinessClass).Title, TextWrapping.NoWrap);
            //            (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //            //UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //            UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //            Label = _mAnnotation.Content.ToString();
            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    ((s as LabelVM).DataContext as ComboBoxBusinessClass).Items.Clear();
            //                    string[] words = (s as LabelVM).Content.ToString().Split('\n');
            //                    textsize = new Size();
            //                    for (int i = 0; i < words.Count(); i++)
            //                    {
            //                        if (i == 0)
            //                            ((s as LabelVM).DataContext as ComboBoxBusinessClass).Title = words[i];
            //                        else
            //                            ((s as LabelVM).DataContext as ComboBoxBusinessClass).Items.Add(new ComboBoxItemBusinessObject { Item = words[i] });

            //                        Size localsize = FindDummyTextSize(_mAnnotation, words[i], TextWrapping.NoWrap);
            //                        //if (textsize.Width < localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right)
            //                        //    textsize.Width = localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;

            //                        textsize.Height += localsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;

            //                        (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight = localsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    }
            //                    textsize.Width = FindDummyTextSize(_mAnnotation, ((s as LabelVM).DataContext as ComboBoxBusinessClass).Title, TextWrapping.NoWrap).Width + (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    UnitWidth = textsize.Width;
            //                    UnitHeight = textsize.Height;
            //                    Label = (s as LabelVM).Content.ToString();
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    textsize = new Size();
            //                    textsize = FindDummyTextSize(_mAnnotation, (_mAnnotation.DataContext as ComboBoxBusinessClass).Title, TextWrapping.NoWrap);
            //                    (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    textsize.Width += (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    textsize.Height += _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    foreach (var item in ((s as LabelVM).DataContext as ComboBoxBusinessClass).Items)
            //                    {
            //                        Size localsize = FindDummyTextSize(_mAnnotation, item.Item, TextWrapping.NoWrap);
            //                        //if (textsize.Width < localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right)
            //                        //    textsize.Width = localsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                        textsize.Height += localsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    }
            //                    //textsize.Width = FindDummyTextSize(_mAnnotation, ((s as LabelVM).DataContext as ComboBoxBusinessClass).Title, TextWrapping.NoWrap).Width + (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    UnitWidth = textsize.Width;
            //                    UnitHeight = textsize.Height;
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region checkbox / radiobutton / datechooser
            //    else if (Shape.Equals("checkbox")
            //        || Shape.Equals("radiobutton")
            //        || Shape.Equals("datechooser")
            //        )
            //    {
            //        UnitWidth = 150;
            //        UnitHeight = 40;
            //        FontSize = 14;
            //        LabelForeground = new SolidColorBrush(Colors.Black);
            //        Fill = new SolidColorBrush(Colors.White);
            //        Stroke = new SolidColorBrush(Colors.Black);
            //        Thickness = 1;
            //        foreach (LabelVM _mAnnotation in Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
            //        {
            //            _mAnnotation.DataContext = DataContext;
            //            Size textsize;

            //            if (Shape.Equals("checkbox"))
            //            {
            //                _mAnnotation.LabelMargin = new Thickness(10, 0, 10, 0);
            //                _mAnnotation.Content = "CheckBox";
            //                _mAnnotation.ViewTemplate = App.Current.Resources["selectionboxviewtemplate"] as DataTemplate;
            //                textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //            }
            //            else if (Shape.Equals("radiobutton"))
            //            {
            //                _mAnnotation.LabelMargin = new Thickness(10, 0, 10, 0);
            //                _mAnnotation.Content = "RadioButton";
            //                _mAnnotation.ViewTemplate = App.Current.Resources["radiobuttonviewtemplate"] as DataTemplate;
            //                textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //            }
            //            else if (Shape.Equals("datechooser"))
            //            {
            //                _mAnnotation.LabelMargin = new Thickness(10, 5, 10, 5);
            //                _mAnnotation.ViewTemplate = App.Current.Resources["datechooserviewtemplate"] as DataTemplate;
            //                _mAnnotation.Content = " 01 / 01 / 2015";
            //                textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //            }
            //            Label = _mAnnotation.Content.ToString();
            //            (_mAnnotation as IAnnotation).Mode = ContentEditorMode.Edit;

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();

            //                    if (Shape.Equals("checkbox") || Shape.Equals("radiobutton"))
            //                    {
            //                        textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                        UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                        UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    }
            //                    else
            //                    {
            //                        Label = (s as LabelVM).Content.ToString();
            //                        textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                        textsize.Width = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;

            //                        UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;
            //                        UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    }
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    if (Shape.Equals("checkbox") || Shape.Equals("radiobutton"))
            //                    {
            //                        textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                        UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                        UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    }
            //                    else
            //                    {
            //                        Label = (s as LabelVM).Content.ToString();
            //                        textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                        textsize.Width = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;
            //                        UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;
            //                        UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    }
            //                }
            //                else if (e.PropertyName == "FontWeight")
            //                {
            //                    if (Shape.Equals("checkbox") || Shape.Equals("radiobutton"))
            //                    {
            //                        textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                        UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                        UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    }
            //                    else
            //                    {
            //                        Label = (s as LabelVM).Content.ToString();
            //                        textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
            //                        textsize.Width = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;

            //                        UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;
            //                        UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    }
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region buttonbar
            //    else if (Shape.Equals("buttonbar"))
            //    {
            //        FontSize = 14;
            //        LabelMargin = new Thickness(10);
            //        foreach (LabelVM _mAnnotation in Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["buttonbarviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            Size buttonbarsize = new Size();
            //            foreach (var str in ((_mAnnotation.DataContext as ButtonBarBusinessClass).Items as ObservableCollection<object>))
            //            {

            //                if (_mAnnotation.Content == null)
            //                {
            //                    _mAnnotation.Content = (str as ButtonBarItemBusinessClass).Item;
            //                    Size textsize = FindDummyTextSize(_mAnnotation, (str as ButtonBarItemBusinessClass).Item, TextWrapping.NoWrap);
            //                    (str as ButtonBarItemBusinessClass).Width = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    buttonbarsize.Width = buttonbarsize.Width + (str as ButtonBarItemBusinessClass).Width;
            //                    buttonbarsize.Height = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                }
            //                else
            //                {
            //                    _mAnnotation.Content = _mAnnotation.Content.ToString() + "," + (str as ButtonBarItemBusinessClass).Item;
            //                    Size textsize = FindDummyTextSize(_mAnnotation, (str as ButtonBarItemBusinessClass).Item, TextWrapping.NoWrap);
            //                    (str as ButtonBarItemBusinessClass).Width = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                    buttonbarsize.Width = buttonbarsize.Width + (str as ButtonBarItemBusinessClass).Width;
            //                    buttonbarsize.Height = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                }
            //            }
            //            Label = _mAnnotation.Content.ToString();

            //            UnitWidth = buttonbarsize.Width;
            //            UnitHeight = buttonbarsize.Height;

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    string[] words = (s as LabelVM).Content.ToString().Split(',');

            //                    #region For HyperLinks
            //                    Dictionary<string, int> links = new Dictionary<string, int>();
            //                    for (int i = 0; i < ((s as LabelVM).DataContext as ButtonBarBusinessClass).Items.Count; i++)
            //                    {
            //                        if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i] as ButtonBarItemBusinessClass).Link.Link))
            //                        {
            //                            links.Add((((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i] as ButtonBarItemBusinessClass).Link.Link, i);
            //                        }
            //                    }
            //                    Dictionary<string, int> templinks = new Dictionary<string, int>();
            //                    int x = 0;
            //                    foreach (var i in links)
            //                    {
            //                        if (words.Contains((((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i.Value] as ButtonBarItemBusinessClass).Item))
            //                        {
            //                            int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i.Value] as ButtonBarItemBusinessClass).Item);
            //                            templinks.Add(i.Key, mm);
            //                        }
            //                        else
            //                            templinks.Add(i.Key, i.Value);
            //                        x++;
            //                    }
            //                    links = null;
            //                    #endregion

            //                    ((s as LabelVM).DataContext as ButtonBarBusinessClass).Items.Clear();
            //                    foreach (string str in words)
            //                    {
            //                        (((s as LabelVM).DataContext as ButtonBarBusinessClass).Items as ObservableCollection<object>).Add(
            //                            new ButtonBarItemBusinessClass()
            //                            {
            //                                Item = str,
            //                                Selected = false,
            //                                Width = FindDummyTextSize(_mAnnotation, str, TextWrapping.NoWrap).Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right
            //                            });
            //                    }
            //                    buttonbarsize = new Size();
            //                    foreach (var item in ((s as LabelVM).DataContext as ButtonBarBusinessClass).Items)
            //                    {
            //                        buttonbarsize.Width = buttonbarsize.Width + (item as ButtonBarItemBusinessClass).Width;
            //                    }
            //                    buttonbarsize.Height = FindDummyTextSize(_mAnnotation, (s as LabelVM).Content.ToString(), TextWrapping.NoWrap).Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    UnitWidth = buttonbarsize.Width;
            //                    UnitHeight = buttonbarsize.Height;
            //                    ((s as LabelVM).DataContext as ButtonBarBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as ButtonBarBusinessClass).SelectedIndex;

            //                    #region For HyperLinks
            //                    foreach (var i in templinks)
            //                    {
            //                        (((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i.Value] as ButtonBarItemBusinessClass).Link = new HyperlinkBusinessClass()
            //                        {
            //                            Title = (((s as LabelVM).DataContext as ButtonBarBusinessClass).Items[i.Value] as ButtonBarItemBusinessClass).Item,
            //                            Link = i.Key
            //                        };
            //                    }
            //                    templinks = null;
            //                    #endregion

            //                    Label = (s as LabelVM).Content.ToString();
            //                }
            //                else if (e.PropertyName == "FontSize")
            //                {
            //                    buttonbarsize = new Size();
            //                    foreach (var item in ((s as LabelVM).DataContext as ButtonBarBusinessClass).Items)
            //                    {
            //                        (item as ButtonBarItemBusinessClass).Width = FindDummyTextSize(_mAnnotation, (item as ButtonBarItemBusinessClass).Item, TextWrapping.NoWrap).Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                        buttonbarsize.Width = buttonbarsize.Width + (item as ButtonBarItemBusinessClass).Width;
            //                    }
            //                    buttonbarsize.Height = FindDummyTextSize(_mAnnotation, (s as LabelVM).Content.ToString(), TextWrapping.NoWrap).Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
            //                    UnitWidth = buttonbarsize.Width;
            //                    UnitHeight = buttonbarsize.Height;
            //                }
            //            };


            //        }
            //    }
            //    #endregion
            //#region tab
            //    else if (Shape.Equals("tabsbar")
            //        || Shape.Equals("verticaltab")
            //        )
            //    {
            //        UnitWidth = 250;
            //        UnitHeight = 250;
            //        FontSize = 14;
            //        LabelMargin = new Thickness(10, 5, 10, 5);
            //        Fill = new SolidColorBrush(Colors.White);
            //        foreach (LabelVM _mAnnotation in Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
            //        {
            //            if (Shape.Equals("tabsbar"))
            //            {
            //                _mAnnotation.ViewTemplate = App.Current.Resources["tabbarTopviewtemplate"] as DataTemplate;
            //                UnitWidth = 325;
            //            }
            //            else if (Shape.Equals("verticaltab"))
            //                _mAnnotation.ViewTemplate = App.Current.Resources["verticaltabLeftviewtemplate"] as DataTemplate;

            //            _mAnnotation.DataContext = DataContext;
            //            foreach (var str in ((_mAnnotation.DataContext as TabBusinessClass).Items as ObservableCollection<object>))
            //            {
            //                if (_mAnnotation.Content == null)
            //                {
            //                    _mAnnotation.Content = (str as TabItemBusinessClass).Item;
            //                }
            //                else
            //                {
            //                    _mAnnotation.Content = _mAnnotation.Content.ToString() + "," + (str as TabItemBusinessClass).Item;
            //                }
            //            }
            //            Label = _mAnnotation.Content.ToString();

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    string[] words = (s as LabelVM).Content.ToString().Split(',');

            //                    #region For HyperLinks
            //                    Dictionary<string, int> links = new Dictionary<string, int>();
            //                    for (int i = 0; i < ((s as LabelVM).DataContext as TabBusinessClass).Items.Count; i++)
            //                    {
            //                        if (!string.IsNullOrEmpty((((s as LabelVM).DataContext as TabBusinessClass).Items[i] as TabItemBusinessClass).Link.Link))
            //                        {
            //                            links.Add((((s as LabelVM).DataContext as TabBusinessClass).Items[i] as TabItemBusinessClass).Link.Link, i);
            //                        }
            //                    }
            //                    Dictionary<string, int> templinks = new Dictionary<string, int>();
            //                    int x = 0;
            //                    foreach (var i in links)
            //                    {
            //                        if (words.Contains((((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Item))
            //                        {
            //                            int mm = words.ToList().IndexOf((((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Item);
            //                            templinks.Add(i.Key, mm);
            //                        }
            //                        else
            //                            templinks.Add(i.Key, i.Value);
            //                        x++;
            //                    }
            //                    links = null;
            //                    #endregion

            //                    ((s as LabelVM).DataContext as TabBusinessClass).Items.Clear();

            //                    foreach (string str in words)
            //                    {
            //                        (((s as LabelVM).DataContext as TabBusinessClass).Items as ObservableCollection<object>).Add(new TabItemBusinessClass() { Item = str });
            //                    }

            //                    ((s as LabelVM).DataContext as TabBusinessClass).Background = Fill;
            //                    ((s as LabelVM).DataContext as TabBusinessClass).SelectedIndex = ((s as LabelVM).DataContext as TabBusinessClass).SelectedIndex;

            //                    #region For HyperLinks
            //                    foreach (var i in templinks)
            //                    {
            //                        (((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Link = new HyperlinkBusinessClass()
            //                        {
            //                            Title = (((s as LabelVM).DataContext as TabBusinessClass).Items[i.Value] as TabItemBusinessClass).Item,
            //                            Link = i.Key
            //                        };
            //                    }
            //                    templinks = null;
            //                    #endregion

            //                    (((s as LabelVM).DataContext as TabBusinessClass).Items[((s as LabelVM).DataContext as TabBusinessClass).Items.Count() - 1] as TabItemBusinessClass).IsLastItem = true;
            //                }
            //                Label = (s as LabelVM).Content.ToString();
            //            };

            //        }
            //    }
            //    #endregion
            //#region messagebox
            //    else if (Shape.Equals("messagebox"))
            //    {
            //        UnitHeight = 140;
            //        UnitWidth = 220;
            //        FontSize = 14;
            //        Fill = new SolidColorBrush(Colors.White);
            //        foreach (LabelVM _mAnnotation in Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["messageboxviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.Content = (_mAnnotation.DataContext as MessageBoxBusinessObject).Input;
            //            Label = (_mAnnotation.DataContext as MessageBoxBusinessObject).Title + "\t\n" + (_mAnnotation.DataContext as MessageBoxBusinessObject).Message + "\t\n" + (_mAnnotation.DataContext as MessageBoxBusinessObject).Ok;
            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    string[] words = (s as LabelVM).Content.ToString().Split('\n');
            //                    if (words.Count() > 0 && !string.IsNullOrEmpty(words[0]))
            //                        ((s as LabelVM).DataContext as MessageBoxBusinessObject).Title = string.IsNullOrEmpty(words[0]) ? string.Empty : words[0];
            //                    else
            //                        ((s as LabelVM).DataContext as MessageBoxBusinessObject).Title = string.Empty;

            //                    if (words.Count() > 1 && !string.IsNullOrEmpty(words[1]))
            //                        ((s as LabelVM).DataContext as MessageBoxBusinessObject).Message = string.IsNullOrEmpty(words[1]) ? string.Empty : words[1];
            //                    else
            //                        ((s as LabelVM).DataContext as MessageBoxBusinessObject).Message = string.Empty;

            //                    if (words.Count() == 3 && !string.IsNullOrEmpty(words[2]))
            //                    {
            //                        ((s as LabelVM).DataContext as MessageBoxBusinessObject).Ok = string.IsNullOrEmpty(words[2]) ? string.Empty : words[2];
            //                    }
            //                    else
            //                    {
            //                        ((s as LabelVM).DataContext as MessageBoxBusinessObject).Ok = string.Empty;
            //                    }
            //                    Label = (s as LabelVM).Content.ToString();
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region alertbox
            //    else if (Shape.Equals("alertbox"))
            //    {
            //        UnitHeight = 140;
            //        UnitWidth = 220;
            //        FontSize = 14;
            //        Fill = new SolidColorBrush(Colors.White);
            //        foreach (LabelVM _mAnnotation in Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
            //        {
            //            if (!(_mAnnotation is HyperLinkVM))
            //            {
            //                _mAnnotation.ViewTemplate = App.Current.Resources["alertboxviewtemplate"] as DataTemplate;
            //                _mAnnotation.EditTemplate = App.Current.Resources["dialogboxedittemplate"] as DataTemplate;
            //                _mAnnotation.DataContext = DataContext;
            //                _mAnnotation.Content = (_mAnnotation.DataContext as DialogBoxBusinessObject).Input;
            //                Label = _mAnnotation.Content.ToString();

            //                _mAnnotation.PropertyChanged += (s, e) =>
            //                {
            //                    if (e.PropertyName == "Content")
            //                    {
            //                        string[] words = (s as LabelVM).Content.ToString().Split('\n');

            //                        if (words.Count() > 0 && !string.IsNullOrEmpty(words[0]))
            //                            ((s as LabelVM).DataContext as DialogBoxBusinessObject).Title = string.IsNullOrEmpty(words[0]) ? string.Empty : words[0];
            //                        else
            //                            ((s as LabelVM).DataContext as DialogBoxBusinessObject).Title = string.Empty;

            //                        if (words.Count() > 1 && !string.IsNullOrEmpty(words[1]))
            //                            ((s as LabelVM).DataContext as DialogBoxBusinessObject).Message = string.IsNullOrEmpty(words[1]) ? string.Empty : words[1];
            //                        else
            //                            ((s as LabelVM).DataContext as DialogBoxBusinessObject).Message = string.Empty;

            //                        if (Shape.Equals("alertbox") || Shape.Equals("dialogbox"))
            //                        {
            //                            if (words.Count() == 3 && !string.IsNullOrEmpty(words[2]))
            //                            {
            //                                string[] okcancel = words[2].ToString().Split(',');
            //                                ((s as LabelVM).DataContext as DialogBoxBusinessObject).Cancel = string.IsNullOrEmpty(okcancel[0]) ? string.Empty : okcancel[0];
            //                                if (okcancel.Count() > 1)
            //                                    ((s as LabelVM).DataContext as DialogBoxBusinessObject).Ok = string.IsNullOrEmpty(okcancel[1]) ? string.Empty : okcancel[1];
            //                                else
            //                                    ((s as LabelVM).DataContext as DialogBoxBusinessObject).Ok = string.Empty;
            //                            }
            //                            else
            //                            {
            //                                ((s as LabelVM).DataContext as DialogBoxBusinessObject).Cancel = string.Empty;
            //                                ((s as LabelVM).DataContext as DialogBoxBusinessObject).Ok = string.Empty;
            //                            }
            //                        }
            //                        Label = (s as LabelVM).Content.ToString();
            //                    }
            //                };
            //            }
            //        }
            //    }
            //    #endregion
            //#region windowbox
            //    else if (Shape.Equals("windowbox"))
            //    {
            //        UnitWidth = 400;
            //        UnitHeight = 300;
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["defaultviewtemplate"] as DataTemplate;
            //            _mAnnotation.LabelHeight = 30;
            //            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
            //            _mAnnotation.VerticalAlignment = VerticalAlignment.Top;
            //            _mAnnotation.LabelTextWrapping = TextWrapping.NoWrap;
            //            _mAnnotation.LabelForeground = new SolidColorBrush(Colors.Black);
            //            Annotations = new List<IAnnotation>() { _mAnnotation };

            //            _mAnnotation.Content = "Window Name";
            //            Label = _mAnnotation.Content.ToString();

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region browserWindow
            //    else if (Shape.Equals("browserWindow"))
            //    {
            //        UnitHeight = 300;
            //        UnitWidth = 400;
            //        Fill = new SolidColorBrush(Colors.White);
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["browserwindowviewtemplate"] as DataTemplate;
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.LabelMargin = new Thickness(65, 0, 20, 0);
            //            _mAnnotation.LabelWidth = UnitWidth - _mAnnotation.LabelMargin.Right;
            //            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
            //            _mAnnotation.VerticalAlignment = VerticalAlignment.Top;

            //            _mAnnotation.Content = (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteTitle + "\t\n" + (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteURL;
            //            Label = _mAnnotation.Content.ToString();

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                    string[] words = (s as LabelVM).Content.ToString().Split('\n');
            //                    if (words.Count() > 0)
            //                    {
            //                        if (!string.IsNullOrEmpty(words[0]))
            //                            (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteTitle = words[0];
            //                        else
            //                            (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteTitle = string.Empty;

            //                        if (!string.IsNullOrEmpty(words[1]))
            //                            (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteURL = words[1];
            //                        else
            //                            (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteURL = string.Empty;
            //                    }
            //                    else
            //                    {
            //                        (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteTitle = string.Empty;
            //                        (_mAnnotation.DataContext as BrowserWindowBusinessClass).WebsiteURL = string.Empty;
            //                    }
            //                }
            //            };
            //        }
            //    }
            //    #endregion                
            //#region Chart
            //    if (Shape.ToString().Contains("chart"))
            //    {
            //        UnitWidth = 200;
            //        UnitHeight = 165;
            //        if (Shape.Equals("piechart"))
            //        {
            //            UnitWidth = 200;
            //            UnitHeight = 200;
            //            Constraints = Constraints | NodeConstraints.AspectRatio;
            //        }
            //        else if (Shape.Equals("bubblechart"))
            //        {
            //            Constraints = Constraints | NodeConstraints.AspectRatio;
            //        }

            //        AvoidLabelEditMode();
            //    }
            //    #endregion
            //#region Accordion
            //    else if (Shape.Equals("accordion"))
            //    {
            //        foreach (LabelVM _mAnnotation in Annotations as List<IAnnotation>)
            //        {
            //            _mAnnotation.DataContext = DataContext;
            //            _mAnnotation.FontSize = 14;
            //            _mAnnotation.LabelMargin = new Thickness(10, 0, 0, 0);
            //            _mAnnotation.ViewTemplate = App.Current.Resources["accordionviewtempalte"] as DataTemplate;
            //            Size textSize = new Size();
            //            foreach (var i in (DataContext as AccordionBusinessObject).Items)
            //            {
            //                if (_mAnnotation.Content == null || string.IsNullOrEmpty(_mAnnotation.Content.ToString()))
            //                {
            //                    textSize = FindDummyTextSize(_mAnnotation, (i as AccordionItemBusinessObject).Item.ToString());
            //                    _mAnnotation.Content = (i as AccordionItemBusinessObject).Item;
            //                    double height = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString()).Height;
            //                    if ((i as AccordionItemBusinessObject).Items != null && (i as AccordionItemBusinessObject).Items.Count > 0)
            //                    {                                   
            //                        foreach (var subitem in (i as AccordionItemBusinessObject).Items)
            //                        {
            //                            _mAnnotation.Content = _mAnnotation.Content.ToString() + "\t\n" + (subitem as AccordionSubItemBusinessObject).Item;
            //                            double localWidth = FindDummyTextSize(_mAnnotation, (subitem as AccordionSubItemBusinessObject).Item.ToString()).Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                            if(textSize.Width < localWidth)
            //                            {
            //                                textSize.Width = localWidth;
            //                            }
            //                        }

            //                        (i as AccordionItemBusinessObject).Height = ((DataContext as AccordionBusinessObject).ItemTitleHeight * (i as AccordionItemBusinessObject).Items.Count) + (i as AccordionItemBusinessObject).BottomSpace;
            //                    }
            //                    else
            //                        (i as AccordionItemBusinessObject).Height = (DataContext as AccordionBusinessObject).ItemTitleHeight + (i as AccordionItemBusinessObject).BottomSpace;
            //                    //(i as AccordionItemBusinessObject).Height = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString()).Height + (i as AccordionItemBusinessObject).BottomSpace;
            //                }
            //                else
            //                {
            //                    double localWidth = FindDummyTextSize(_mAnnotation, (i as AccordionItemBusinessObject).Item).Width;
            //                    if (textSize.Width < localWidth)
            //                    {
            //                        textSize.Width = localWidth;
            //                    }
            //                    _mAnnotation.Content = _mAnnotation.Content.ToString() + "\t\n" + (i as AccordionItemBusinessObject).Item;
            //                    double height = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString()).Height;
            //                    if ((i as AccordionItemBusinessObject).Items != null && (i as AccordionItemBusinessObject).Items.Count > 0)
            //                    {
            //                        foreach (var subitem in (i as AccordionItemBusinessObject).Items)
            //                        {
            //                            _mAnnotation.Content = _mAnnotation.Content.ToString() + "\t\n" + (subitem as AccordionSubItemBusinessObject).Item;
            //                            localWidth = FindDummyTextSize(_mAnnotation, (subitem as AccordionSubItemBusinessObject).Item.ToString()).Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                            if (textSize.Width < localWidth)
            //                            {
            //                                textSize.Width = localWidth;
            //                            }
            //                        }
            //                        (i as AccordionItemBusinessObject).Height = ((DataContext as AccordionBusinessObject).ItemTitleHeight * (i as AccordionItemBusinessObject).Items.Count) + (i as AccordionItemBusinessObject).BottomSpace;
            //                    }
            //                    else
            //                        (i as AccordionItemBusinessObject).Height = (DataContext as AccordionBusinessObject).ItemTitleHeight + (i as AccordionItemBusinessObject).BottomSpace;
            //                }
            //            }
            //            Label = _mAnnotation.Content.ToString();
            //            UnitWidth = textSize.Width;

            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Mode")
            //                {
            //                    if ((s as LabelVM).Mode == ContentEditorMode.Edit)
            //                    {
            //                        string localAnnotation = string.Empty;
            //                        //_mAnnotation.Content = string.Empty;
            //                        foreach (var i in (DataContext as AccordionBusinessObject).Items)
            //                        {
            //                            if (string.IsNullOrEmpty(localAnnotation.ToString()))
            //                            {
            //                                localAnnotation = (i as AccordionItemBusinessObject).Item;
            //                                if ((i as AccordionItemBusinessObject).Items != null && (i as AccordionItemBusinessObject).Items.Count > 0)
            //                                {
            //                                    foreach (var subitem in (i as AccordionItemBusinessObject).Items)
            //                                    {
            //                                        localAnnotation = localAnnotation.ToString() + "\n" + "- " + (subitem as AccordionSubItemBusinessObject).Item;
            //                                    }
            //                                }
            //                            }
            //                            else
            //                            {
            //                                localAnnotation = localAnnotation.ToString() + "\n" + (i as AccordionItemBusinessObject).Item;
            //                                if ((i as AccordionItemBusinessObject).Items != null && (i as AccordionItemBusinessObject).Items.Count > 0)
            //                                {
            //                                    foreach (var subitem in (i as AccordionItemBusinessObject).Items)
            //                                    {
            //                                        localAnnotation = localAnnotation.ToString() + "\n" + "- " + (subitem as AccordionSubItemBusinessObject).Item;
            //                                    }
            //                                }
            //                            }
            //                        }

            //                        //_mAnnotation.Content = localAnnotation;
            //                        Label = localAnnotation;
            //                    }
            //                }
            //                else if (e.PropertyName == "Content")
            //                {
            //                    if ((s as LabelVM).Mode == ContentEditorMode.View)
            //                    {
            //                        //Label = _mAnnotation.Content.ToString();
            //                        string[] words = Label.Split('\n');
            //                        (DataContext as AccordionBusinessObject).Items.Clear();
            //                        string localAnnotation = string.Empty;
            //                        for(int i = 0; i< words.Count(); i++)
            //                        {
            //                            if (i == 0 && words[i] != "\t\r")
            //                            {
            //                                (DataContext as AccordionBusinessObject).Items.Add(new AccordionItemBusinessObject() {Item = words[0],Index = 1 });
            //                                textSize = FindDummyTextSize(_mAnnotation, words[0]);
            //                            }
            //                            else if (words[i] != "\t\r")
            //                            {
            //                                if (char.Equals(words[i][0], '-'))
            //                                {
            //                                    if (char.IsWhiteSpace(words[i][1]))
            //                                    {
            //                                        words[i] = words[i].Remove(0, 2);

            //                                        int parentIndex = (DataContext as AccordionBusinessObject).Items.Count();
            //                                        if (((DataContext as AccordionBusinessObject).Items[parentIndex - 1] as AccordionItemBusinessObject).Items == null)
            //                                        {
            //                                            ((DataContext as AccordionBusinessObject).Items[parentIndex - 1] as AccordionItemBusinessObject).Items = new ObservableCollection<object>();
            //                                            ((DataContext as AccordionBusinessObject).Items[parentIndex - 1] as AccordionItemBusinessObject).Items.Add
            //                                            (
            //                                                new AccordionSubItemBusinessObject() { Item = words[i], ParentIndex = parentIndex }
            //                                            );
            //                                            double localWidth = FindDummyTextSize(_mAnnotation, words[i]).Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                                            if (textSize.Width < localWidth)
            //                                                textSize.Width = localWidth;
            //                                        }
            //                                        else
            //                                        {
            //                                            ((DataContext as AccordionBusinessObject).Items[parentIndex - 1] as AccordionItemBusinessObject).Items.Add
            //                                            (
            //                                                new AccordionSubItemBusinessObject() { Item = words[i], ParentIndex = parentIndex }
            //                                            );
            //                                            double localWidth = FindDummyTextSize(_mAnnotation, words[i]).Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
            //                                            if (textSize.Width < localWidth)
            //                                                textSize.Width = localWidth;
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        int parentIndex = (DataContext as AccordionBusinessObject).Items.Count();
            //                                        (DataContext as AccordionBusinessObject).Items.Add(new AccordionItemBusinessObject() { Item = words[i], Index = parentIndex + 1 });
            //                                        double localWidth = FindDummyTextSize(_mAnnotation, words[i]).Width;
            //                                        if (textSize.Width < localWidth)
            //                                            textSize.Width = localWidth;
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    int parentIndex = (DataContext as AccordionBusinessObject).Items.Count();
            //                                    (DataContext as AccordionBusinessObject).Items.Add(new AccordionItemBusinessObject() { Item = words[i], Index = parentIndex + 1 });
            //                                    double localWidth = FindDummyTextSize(_mAnnotation, words[i]).Width;
            //                                    if (textSize.Width < localWidth)
            //                                        textSize.Width = localWidth;
            //                                }
                                            
            //                            }
            //                        }

            //                        foreach (var i in (DataContext as AccordionBusinessObject).Items)
            //                        {
            //                            //if (_mAnnotation.Content == null || string.IsNullOrEmpty(_mAnnotation.Content.ToString()))
            //                            {
            //                                //double height = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString()).Height;
            //                                if ((i as AccordionItemBusinessObject).Items != null && (i as AccordionItemBusinessObject).Items.Count > 0)
            //                                {
            //                                    (i as AccordionItemBusinessObject).Height = ((DataContext as AccordionBusinessObject).ItemTitleHeight * (i as AccordionItemBusinessObject).Items.Count) + (i as AccordionItemBusinessObject).BottomSpace;
            //                                }
            //                                else
            //                                    (i as AccordionItemBusinessObject).Height = (i as AccordionItemBusinessObject).BottomSpace;
            //                            }
            //                        }
                                    
            //                        (DataContext as AccordionBusinessObject).SelectedIndex = (DataContext as AccordionBusinessObject).SelectedIndex;
            //                        if ((DataContext as AccordionBusinessObject).SelectedIndex == 0)
            //                        {
            //                            //Size size = FindDummyTextSize(_mAnnotation, ((node.DataContext as AccordionBusinessObject).Items[0] as AccordionItemBusinessObject).Item);
            //                                double height = (DataContext as AccordionBusinessObject).Items.Count() * (DataContext as AccordionBusinessObject).ItemTitleHeight;
            //                                UnitHeight = height;
            //                        }
            //                        else
            //                        {
            //                            double height = ((DataContext as AccordionBusinessObject).Items.Count() * (DataContext as AccordionBusinessObject).ItemTitleHeight) + ((DataContext as AccordionBusinessObject).Items[(DataContext as AccordionBusinessObject).SelectedIndex - 1] as AccordionItemBusinessObject).BottomSpace;
            //                            double subItemsHeight = 0d;
            //                            if (((DataContext as AccordionBusinessObject).Items[(DataContext as AccordionBusinessObject).SelectedIndex - 1] as AccordionItemBusinessObject).Items != null)
            //                            {
            //                                subItemsHeight = ((DataContext as AccordionBusinessObject).Items[(DataContext as AccordionBusinessObject).SelectedIndex - 1] as AccordionItemBusinessObject).Items.Count() * (DataContext as AccordionBusinessObject).ItemTitleHeight;
            //                            }
            //                            height += subItemsHeight;
            //                            UnitHeight = height;
            //                        }
            //                        UnitWidth = textSize.Width;
            //                        Label = _mAnnotation.Content.ToString();
            //                    }
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            //#region multilinebutton
            //    if (Shape.Equals("multilinebutton"))
            //    {
                    
            //        UnitHeight = 60;
            //        UnitWidth = 160;
            //        FontSize = 14;
            //        Fill = new SolidColorBrush(Colors.White);
            //        foreach (LabelVM _mAnnotation in Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
            //        {
            //            _mAnnotation.ViewTemplate = App.Current.Resources["multilinebuttonviewtemplate"] as DataTemplate;
            //            //_mAnnotation.EditTemplate = App.Current.Resources["multilineboxedittemplate"] as DataTemplate;
            //            _mAnnotation.TextAlignment = TextAlignment.Center;
            //            _mAnnotation.LabelForeground = new SolidColorBrush(Colors.Black);
            //            Annotations = new List<IAnnotation>() { _mAnnotation };
            //            //_mAnnotation.DataContext = new DialogBoxBusinessObject() { Title = "Multiline Button", Message = "Second line of text" };
            //            _mAnnotation.DataContext = DataContext;
            //            //_mAnnotation.Content = (_mAnnotation.DataContext as DialogBoxBusinessObject).Input;
            //            _mAnnotation.Content = (_mAnnotation.DataContext as MultiLineButtonBusinessObject).Title + "\t\n" + (_mAnnotation.DataContext as MultiLineButtonBusinessObject).Message;
            //            Label = _mAnnotation.Content.ToString();
            //            _mAnnotation.LabelWidth = UnitWidth;
            //            _mAnnotation.LabelHeight = UnitHeight;

            //            (_mAnnotation.DataContext as MultiLineButtonBusinessObject).TitleHeight = FindDummyTextSize(_mAnnotation, (_mAnnotation.DataContext as MultiLineButtonBusinessObject).Title).Height + (_mAnnotation.DataContext as MultiLineButtonBusinessObject).TitleMargin.Top + (_mAnnotation.DataContext as MultiLineButtonBusinessObject).TitleMargin.Bottom;
            //            _mAnnotation.PropertyChanged += (s, e) =>
            //            {
            //                if (e.PropertyName == "Content")
            //                {
            //                    Label = (s as LabelVM).Content.ToString();
            //                    string[] words = Label.Split('\n');
            //                    ((s as LabelVM).DataContext as MultiLineButtonBusinessObject).Title = string.Empty;
            //                    ((s as LabelVM).DataContext as MultiLineButtonBusinessObject).Message = string.Empty;
            //                    for(int x=0;x<words.Count();x++)
            //                    {
            //                        if(x == 0)
            //                        {
            //                            ((s as LabelVM).DataContext as MultiLineButtonBusinessObject).Title = words[0];
            //                        }
            //                        else
            //                        {
            //                            ((s as LabelVM).DataContext as MultiLineButtonBusinessObject).Message = ((s as LabelVM).DataContext as MultiLineButtonBusinessObject).Message + words[x];
            //                        }
            //                    }
            //                }
            //                if (e.PropertyName == "FontSize")
            //                {
            //                    (_mAnnotation.DataContext as MultiLineButtonBusinessObject).TitleHeight = FindDummyTextSize(_mAnnotation, (_mAnnotation.DataContext as MultiLineButtonBusinessObject).Title).Height + (_mAnnotation.DataContext as MultiLineButtonBusinessObject).TitleMargin.Top + (_mAnnotation.DataContext as MultiLineButtonBusinessObject).TitleMargin.Bottom;
            //                }
            //            };
            //        }
            //    }
            //    #endregion
            #endregion
                DefaultSize = new Size(UnitWidth, UnitHeight);
        }

        public static PathGeometry Clone(string data)
        {
            string p = "<Path xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Data=\"" + data + "\"/>";
            Path o = XamlReader.Load(p) as Path;
            PathGeometry geo = Clone(o.Data as PathGeometry);
            return geo;
        }

        private static PathGeometry Clone(PathGeometry pathGeometry)
        {
            PathFigureCollection collClone = new PathFigureCollection();
            foreach (PathFigure item in pathGeometry.Figures)
            {
                PathFigure clone = new PathFigure()
                {
                    IsClosed = item.IsClosed,
                    IsFilled = item.IsFilled,
                    Segments = Clone(item.Segments),
                    StartPoint = item.StartPoint
                };
                collClone.Add(clone);
            }
            return new PathGeometry()
            {
                Figures = collClone,
                FillRule = pathGeometry.FillRule,
                Transform = pathGeometry.Transform
            };
        }

        private static PathSegmentCollection Clone(PathSegmentCollection pathSegColl)
        {
            PathSegmentCollection collClone = new PathSegmentCollection();

            foreach (PathSegment item in pathSegColl)
            {
                PathSegment clone = null;
                if (item is LineSegment)
                {
                    LineSegment seg = item as LineSegment;
                    clone = new LineSegment() { Point = seg.Point };
                }
                else if (item is PolyLineSegment)
                {
                    PolyLineSegment seg = item as PolyLineSegment;
                    clone = new PolyLineSegment() { Points = getPoints(seg.Points) };
                }
                else if (item is BezierSegment)
                {
                    BezierSegment seg = item as BezierSegment;
                    clone = new BezierSegment()
                    {
                        Point1 = seg.Point1,
                        Point2 = seg.Point2,
                        Point3 = seg.Point3
                    };
                }
                else if (item is PolyBezierSegment)
                {
                    PolyBezierSegment seg = item as PolyBezierSegment;
                    clone = new PolyBezierSegment() { Points = getPoints(seg.Points) };
                }
                else if (item is PolyQuadraticBezierSegment)
                {
                    PolyQuadraticBezierSegment seg = item as PolyQuadraticBezierSegment;
                    clone = new PolyQuadraticBezierSegment() { Points = getPoints(seg.Points) };
                }
                else if (item is QuadraticBezierSegment)
                {
                    QuadraticBezierSegment seg = item as QuadraticBezierSegment;
                    clone = new QuadraticBezierSegment() { Point1 = seg.Point1, Point2 = seg.Point2 };
                }
                else if (item is ArcSegment)
                {
                    ArcSegment seg = item as ArcSegment;
                    clone = new ArcSegment()
                    {
                        IsLargeArc = seg.IsLargeArc,
                        Point = seg.Point,
                        RotationAngle = seg.RotationAngle,
                        Size = seg.Size,
                        SweepDirection = seg.SweepDirection
                    };
                }

                collClone.Add(clone);
            }
            return collClone;
        }

        private static PointCollection getPoints(PointCollection pointCollection)
        {
            PointCollection coll = new PointCollection();
            foreach (var item in pointCollection)
            {
                coll.Add(item);
            }
            return coll;
        }

        private void OnConstraintsChanged()
        {
            if (Constraints.Contains(NodeConstraints.Draggable))
            {
                AllowDrag = true;
            }
            else
            {
                AllowDrag = false;
            }
            if (Constraints.Contains(NodeConstraints.Resizable))
            {
                AllowResize = true;
            }
            else
            {
                AllowResize = false;
            }
            if (Constraints.Contains(NodeConstraints.Rotatable))
            {
                AllowRotate = true;
            }
            else
            {
                AllowRotate = false;
            }
        }

        #region Shape

        private void OnFillChanged()
        {
            ApplyStyle(GetCustomStyle());
        }

        private void OnAllowRotateChanged()
        {
            if (AllowRotate)
            {
                (this as INode).Constraints |= NodeConstraints.Rotatable;
            }
            else
            {
                (this as INode).Constraints &= ~NodeConstraints.Rotatable;
            }
        }

        private void OnAllowResizeChanged()
        {
            if (AllowResize)
            {
                (this as INode).Constraints |= NodeConstraints.Resizable;
            }
            else
            {
                (this as INode).Constraints &= ~NodeConstraints.Resizable;
            }
        }

        private void OnAllowDragChanged()
        {
            if (AllowDrag)
            {
                (this as INode).Constraints |= NodeConstraints.Draggable;
            }
            else
            {
                (this as INode).Constraints &= ~NodeConstraints.Draggable;
            }
            //else if (this is IConnector)
            //{
            //    if (AllowDrag)
            //    {
            //        (this as IConnector).Constraints |= ConnectorConstraints.EndDraggable;
            //    }
            //    else
            //    {
            //        (this as IConnector).Constraints &= ~ConnectorConstraints.EndDraggable;
            //    }
            //}
        }

        private void OnUnitWidthChanged()
        {
        }
        #endregion

        protected override void DecodeStyle(Style style)
        {
            base.DecodeStyle(style);

            foreach (Setter setter in style.Setters)
            {
                if (setter.Property == Path.FillProperty)
                {
                    Fill = setter.Value as Brush;
                }
            }
        }

        protected override Style GetCustomStyle()
        {
            Style s = base.GetCustomStyle();
            if (Fill != null)
            {
                s.Setters.Add(new Setter(Path.FillProperty, Fill));
                s.Setters.Add(new Setter(Path.StretchProperty, Stretch.Fill));
            }
            return s;
        }

        protected override void ApplyStyle(Style style)
        {
            ShapeStyle = style;
        }


        public PortVisibility PortVisibility { get; set; }

        public ICommand TestCommand { get { return new DelegateCommand<object>(VTapped, args => { return true; }); } }

        private void VTapped(object obj)
        {

        }

        private void SelectorToNode(INodeVM node)
        {
            if ((node as NodeVM).Shape != null)
            {
                if ((node as NodeVM).Shape.Equals("stickynote"))
                {
                    foreach (LabelVM label in (node as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                    {
                        label.LabelWidth = node.UnitWidth;
                        label.LabelHeight = node.UnitHeight;
                    }
                    if (node.UnitWidth > 150)
                    {
                        (node.DataContext as StickyNoteBusinessClass).HeaderBorderWidth = 70 + (node.UnitWidth - 150);
                    }
                    else
                        (node.DataContext as StickyNoteBusinessClass).HeaderBorderWidth = 70;
                }
                else if ((node as NodeVM).Shape.Equals("browserWindow"))
                {
                    (node.DataContext as BrowserWindowBusinessClass).BrowserBodyHeight = node.UnitHeight - ((node.DataContext as BrowserWindowBusinessClass).TitleBarHeight + (node.DataContext as BrowserWindowBusinessClass).BottomBarHeight);
                    foreach (LabelVM label in (node as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                    {
                        label.LabelWidth = node.UnitWidth - label.LabelMargin.Right;
                    }
                }
                else if ((node as NodeVM).Shape.Equals("windowbox"))
                {
                    //(node.DataContext as WindowBoxBusinessObject).WindowBodyHeight = node.UnitHeight - ((node.DataContext as WindowBoxBusinessObject).TopMargin.Top + (node.DataContext as WindowBoxBusinessObject).BottomMargin.Bottom);

                    (node.DataContext as WindowBoxBusinessObject).WindowHeight = node.UnitHeight;
                    foreach (LabelVM label in (node as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                    {
                        label.LabelWidth = node.UnitWidth;
                    }
                }
                else if ((node as NodeVM).Shape.Equals("dialogbox")
                    || (node as NodeVM).Shape.Equals("messagebox")
                    || (node as NodeVM).Shape.Equals("alertbox")
                    || (node as NodeVM).Shape.Equals("horizontalcurlybraces")
                    || (node as NodeVM).Shape.Equals("verticalcurlybraces")
                    || (node as NodeVM).Shape.Equals("checkboxgroup")
                    || (node as NodeVM).Shape.Equals("radiobuttongroup")
                    || (node as NodeVM).Shape.Equals("multilinebutton")
                    || (node as NodeVM).Shape.Equals("button")
                    || (node as NodeVM).Shape.Equals("subtitle")
                    || (node as NodeVM).Shape.Equals("accordion")
                    || (node as NodeVM).Shape.Equals("menubar")
                    || (node as NodeVM).Shape.Equals("menu")
                    || (node as NodeVM).Shape.Equals("label")
                    || (node as NodeVM).Shape.Equals("list")
                    || (node as NodeVM).Shape.Equals("textinput")
                    || (node as NodeVM).Shape.Equals("numericstepper")
                    || (node as NodeVM).Shape.Equals("searchbutton")
                    || (node as NodeVM).Shape.Equals("onoffswitch")
                    || (node as NodeVM).Shape.Equals("pointybutton")
                    || (node as NodeVM).Shape.Equals("bigtitle")
                    || (node as NodeVM).Shape.Equals("textarea")
                    || (node as NodeVM).Shape.Equals("fieldset")
                    || (node as NodeVM).Shape.Equals("tooltip")
                    || (node as NodeVM).Shape.Equals("breadcrumbs")
                    || (node as NodeVM).Shape.Equals("datechooser")
                    || (node as NodeVM).Shape.Equals("geometricshapes")
                    || (node as NodeVM).Shape.Equals("popover")
                    || (node as NodeVM).Shape.Equals("linkbar")
                    )
                {
                    foreach (LabelVM label in (node as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                    {
                        label.LabelWidth = node.UnitWidth;
                        label.LabelHeight = node.UnitHeight;
                    }
                }
                else if ((node as NodeVM).Shape.Equals("combobox"))
                {
                    foreach (LabelVM label in (node as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                    {
                        label.LabelWidth = node.UnitWidth;
                        label.LabelHeight = node.UnitHeight;
                    }
                }
                else if ((node as NodeVM).Shape.Equals("tabsbar") || (node as NodeVM).Shape.Equals("verticaltab"))
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                    {
                        _mAnnotation.LabelWidth = node.UnitWidth;
                        _mAnnotation.LabelHeight = node.UnitHeight;
                    }
                }
                else if ((node as NodeVM).Shape.Equals("buttonbar"))
                {
                    foreach (LabelVM label in (node as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                    {
                        label.LabelWidth = node.UnitWidth;
                        label.LabelHeight = node.UnitHeight;

                        foreach(var item in (label.DataContext as ButtonBarBusinessClass).Items)
                        {
                            (item as ButtonBarItemBusinessClass).Width = label.LabelWidth / (label.DataContext as ButtonBarBusinessClass).Items.Count;
                        }
                    }
                }
                else if ((node as NodeVM).Shape.Equals("radiobutton")
                    || (node as NodeVM).Shape.Equals("checkbox")
                    || (node as NodeVM).Shape.Equals("link")

                    )
                {
                    foreach (LabelVM label in (node as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                    {
                        label.LabelWidth = node.UnitWidth;
                    }
                }
            }
        }


        public double ConnectorPadding { get; set; }
        public bool ExcludeFromLayout { get; set; }
    }

    public static class Ext
    {
        public static bool Contains(this NodeConstraints s, NodeConstraints t)
        {
            return ((s & t) != NodeConstraints.None);
        }
    }

    public interface INodeVM : IGroupableVM, INode
    {
        Brush Fill { get; set; }
        bool AllowDrag { get; set; }
        bool AllowResize { get; set; }
        bool AllowRotate { get; set; }

        //Wireframe 
        int TabSelectedIndex { get; set; }
        object TabSelectedItem { get; set; }
        Thickness ScrollMargin { get; set; }
        object DataContext { get; set; }
        Size DefaultSize { get; set; }
        object PropertiesList { get; set; }
    }

    internal static class NodeConstants
    {
        //public const string OffsetX = "OffsetX";
        //public const string OffsetY = "OffsetY";
        //public const string RotateAngle = "RotateAngle";
        public const string SnapToObject = "SnapToObject";
        public const string Flip = "Flip";
        public const string MinWidth = "MinWidth";
        public const string MaxWidth = "MaxWidth";
        //public const string UnitWidth = "UnitWidth";
        public const string MinHeight = "MinHeight";
        public const string MaxHeight = "MaxHeight";
        //public const string UnitHeight = "UnitHeight";
        public const string Content = "Content";
        public const string ContentTemplate = "ContentTemplate";
        public const string Shape = "Shape";
        public const string ShapeStyle = "ShapeStyle";
        public const string ParentGroup = "ParentGroup";
        public const string IsExpanded = "IsExpanded";
        public const string Pivot = "Pivot";
        public const string AutoBind = "AutoBind";
        public const string ID = "ID";
        public const string Key = "Key";
        public const string IsSelected = "IsSelected";
        public const string ZIndex = "ZIndex";
        public const string Annotations = "Annotations";
        public const string Constraints = "Constraints";
        public const string Ports = "Ports";
        public const string InternalPorts = "InternalPorts";
        public const string HitPadding = "HitPadding";
        public const string Fill = "Fill";
        public const string AllowDrag = "AllowDrag";
        public const string AllowResize = "AllowResize";
        public const string AllowRotate = "AllowRotate";
        public const string SliderValue = "SliderValue";
        public const string FlipMode = "FlipMode";
    }
}
