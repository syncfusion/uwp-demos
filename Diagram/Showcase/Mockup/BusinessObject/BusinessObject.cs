using Mockup.Utility;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Syncfusion.SampleBrowser.UWP.Diagram;

namespace Mockup.BusinessObject
{
    public interface IHyperLink 
    {
        HyperlinkBusinessClass Link { get; set; }
    }

    public interface ICommonForCollection
    {
        ObservableCollection<object> Items { get; set; }
    }

    public interface ICommonForCollectionItem : IHyperLink
    {
        string Item { get; set; }
    }

    public interface IState
    {
        ObservableCollection<object> States { get; set; }
    }

    [XmlInclude(typeof(PropertyChange))]
    [XmlInclude(typeof(GeometricShapesBusinessClass))]
    [XmlInclude(typeof(TextAreaBusinessClass))]
    [XmlInclude(typeof(ListBusinessClass))]
    [XmlInclude(typeof(ListItemBusinessClass))]
    [XmlInclude(typeof(BreadcrumbsBusinessClass))]
    [XmlInclude(typeof(BreadcrumbsItemBusinessClass))]
    [XmlInclude(typeof(LinkBarItemBusinessClass))]
    [XmlInclude(typeof(LinkBarBusinessClass))]
    [XmlInclude(typeof(LinkBusinessClass))]
    [XmlInclude(typeof(PointyButtonBusinessClass))]
    [XmlInclude(typeof(PopoverBusinessClass))]
    [XmlInclude(typeof(RadioButtonGroupBusinessClass))]
    [XmlInclude(typeof(RadioButtonItemBusinessClass))]
    [XmlInclude(typeof(CheckBoxGroupBusinessClass))]
    [XmlInclude(typeof(CheckBoxItemBusinessClass))]
    [XmlInclude(typeof(CommonForCheckBoxRadioButtonBusinessClass))]
    [XmlInclude(typeof(StickyNoteBusinessClass))]
    [XmlInclude(typeof(VerticalCurlyBracesBusinessClass))]
    [XmlInclude(typeof(HorizontalCurlyBracesBusinessClass))]
    [XmlInclude(typeof(ComboBoxBusinessClass))]
    [XmlInclude(typeof(DialogBoxBusinessObject))]
    [XmlInclude(typeof(ToolTipBusinessClass))]
    [XmlInclude(typeof(MessageBoxBusinessObject))]
    [XmlInclude(typeof(MultiLineButtonBusinessObject))]
    [XmlInclude(typeof(NumericStepperBusinessClass))]
    [XmlInclude(typeof(OnOffSwitch))]
    [XmlInclude(typeof(WindowBoxBusinessObject))]
    [XmlInclude(typeof(DateChoosetBusinessClass))]
    [XmlInclude(typeof(CheckBoxBusinessClass))]
    [XmlInclude(typeof(BrowserWindowBusinessClass))]
    [XmlInclude(typeof(VolumeSliderBusinessClass))]
    [XmlInclude(typeof(ProgressBarBusinessClass))]
    [XmlInclude(typeof(SearchBoxBusinessClass))]
    [XmlInclude(typeof(ButtonBarItemBusinessClass))]
    [XmlInclude(typeof(ButtonBarBusinessClass))]
    [XmlInclude(typeof(TabItemBusinessClass))]
    [XmlInclude(typeof(TabBusinessClass))]
    [XmlInclude(typeof(MenuBusinessClass))]
    [XmlInclude(typeof(MenuItemBusinessClass))]
    [XmlInclude(typeof(MenuTitleBusinessClass))]
    [XmlInclude(typeof(MenuBarBusinessClass))]
    [XmlInclude(typeof(VerticalScrollBarBusinessClass))]
    [XmlInclude(typeof(HorizontalScrollBarBusinessClass))]
    [XmlInclude(typeof(SliderBusinessClass))]
    [XmlInclude(typeof(ButtonBusinessObject))]
    [XmlInclude(typeof(IPhoneBusinessClass))]
    [XmlInclude(typeof(IPADBusinessClass))]
    [XmlInclude(typeof(IOSKeyboardBusinessClass))]
    [XmlInclude(typeof(HorizontalCurlyBracesBusinessClass))]
    [XmlInclude(typeof(VerticalCurlyBracesBusinessClass))]
    public class PropertyChange : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private HorizontalAlignment _mHorizontalAlignment = HorizontalAlignment.Left;        
        public HorizontalAlignment HorizontalAlignment
        {
            get { return _mHorizontalAlignment; }
            set
            {
                if (_mHorizontalAlignment != value)
                {
                    _mHorizontalAlignment = value;
                    OnPropertyChanged("HorizontalAlignment");
                }

            }
        }
    }

    public class BusinessObject
    {
    }

    public class HyperlinkBusinessClass : DiagramElementViewModel
    {
        private string _mTitle;
        public string Title
        {
            get { return _mTitle; }
            set
            {
                if (_mTitle != value)
                {
                    _mTitle = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private string _mLink = string.Empty;
        public string Link
        {
            get { return _mLink; }
            set
            {
                if (_mLink != value)
                {
                    _mLink = value;
                    OnPropertyChanged("Link");
                    if (ExternalLink)
                        WebURL = Link;
                }
            }
        }

        private int? _mSelectedLinkIndex = 0;
        public int? SelectedLinkIndex
        {
            get { return _mSelectedLinkIndex; }
            set
            {
                if (_mSelectedLinkIndex != value)
                {
                    _mSelectedLinkIndex = value;
                    OnPropertyChanged("SelectedLinkIndex");
                }
            }
        }

        private bool _mInternalLink;
        public bool InternalLink
        {
            get { return _mInternalLink; }
            set 
            {
                if (_mInternalLink != value)
                {
                    _mInternalLink = value;
                    OnPropertyChanged("InternalLink");
                    if (value)
                    {
                        ExternalLink = false;
                        WebURL = string.Empty;
                    }
                }
            }
        }

        private bool _mExternalLink;
        public bool ExternalLink 
        {
            get { return _mExternalLink; }
            set
            {
                if (_mExternalLink != value)
                {
                    _mExternalLink = value;
                    OnPropertyChanged("ExternalLink");
                    if (value)
                    {
                        InternalLink = false;
                        WebURL = Link;
                    }
                }
            }
        }

        private string _mWebURL = string.Empty;
        public string WebURL
        {
            get { return _mWebURL; }
            set
            {
                if (_mWebURL != value)
                {
                    _mWebURL = value;
                    OnPropertyChanged("WebURL");
                    if(!string.IsNullOrEmpty(value))
                    {
                        SelectedLinkIndex = null;
                    }
                }
            }
        }        
    }

    public class DialogBoxBusinessObject : PropertyChange, INotifyPropertyChanged
    {
        public DialogBoxBusinessObject()
        {
            OkLink = new HyperlinkBusinessClass();
            CancelLink = new HyperlinkBusinessClass();
            PointerEntered = new Command(OnPointerEnteredCommand);
        }

        private string _mTitle = string.Empty;
        public string Title
        {
            get { return _mTitle; }
            set
            {
                if (_mTitle != value)
                {
                    _mTitle = value;
                    TextChanged();
                    OnPropertyChanged("Title");
                }
            }
        }

        private string _mMessage = string.Empty;
        public string Message
        {
            get { return _mMessage; }
            set
            {
                _mMessage = value;
                TextChanged();
                OnPropertyChanged("Message");
            }
        }

        private string _mCancel = string.Empty;
        public string Cancel
        {
            get { return _mCancel; }
            set
            {
                _mCancel = value;
                TextChanged();
                OnPropertyChanged("Cancel");
                CancelLink.Title = value;
                if (string.IsNullOrEmpty(value) && string.IsNullOrEmpty(Ok))
                {
                    EnableBottomBorder = false;
                }
                else
                    EnableBottomBorder = true;
            }
        }

        private string _mOk = string.Empty;
        public string Ok
        {
            get { return _mOk; }
            set
            {
                _mOk = value;
                TextChanged();
                OnPropertyChanged("Ok");
                OkLink.Title = value;

                if (string.IsNullOrEmpty(value) && string.IsNullOrEmpty(Cancel))
                {
                    EnableBottomBorder = false;
                }
                else
                    EnableBottomBorder = true;
            }
        }

        private bool _mEnableBottomBorder;
        [XmlIgnore]
        public bool EnableBottomBorder
        {
            get { return _mEnableBottomBorder; }
            private set
            {
                if (_mEnableBottomBorder != value)
                {
                    _mEnableBottomBorder = value;
                    OnPropertyChanged("EnableBottomBorder");
                }
            }
        }
        
        
        private string _mInput = string.Empty;
        public string Input
        {
            get { return _mInput; }
            set
            {
                _mInput = value;
                //OnPropertyChanged("Input");
            }
        }

        private void TextChanged()
        {
            Input = Title + "\r\n" + Message + "\r\n" + Cancel + "," + Ok;
        }

        private Brush _mFill;
        [XmlIgnore]
        public Brush Fill
        {
            get { return _mFill; }
            set
            {
                if (_mFill != value)
                {
                    _mFill = value;
                    OnPropertyChanged("Fill");
                }
            }
        }

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

        private string _mExecutedPath = string.Empty;
        public string ExecutedPath
        {
            get { return _mExecutedPath; }
            set { _mExecutedPath = value; }
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
                    OkEditModeFill = new SolidColorBrush(Colors.Transparent);
                    CancelEditModeFill = new SolidColorBrush(Colors.Transparent);
                }
            }
        }

        private HyperlinkBusinessClass _mOkLink;
        [XmlIgnore]
        public HyperlinkBusinessClass OkLink
        {
            get { return _mOkLink; }
            set
            {
                _mOkLink = value;
            }
        }

        public string DummyOkLink
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

                        serializer.Serialize(xmlWriter, OkLink as HyperlinkBusinessClass);
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
                this.OkLink = obj as HyperlinkBusinessClass;
            }
        }

        private HyperlinkBusinessClass _mCancelLink;
        [XmlIgnore]
        public HyperlinkBusinessClass CancelLink
        {
            get { return _mCancelLink; }
            set
            {
                _mCancelLink = value;
            }
        }

        public string DummyCancelLink
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

                        serializer.Serialize(xmlWriter, CancelLink as HyperlinkBusinessClass);
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
                this.CancelLink = obj as HyperlinkBusinessClass;
            }
        }

        private Brush _mOkEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
        public Brush OkEditModeFill
        {
            get { return _mOkEditModeFill; }
            set
            {
                if (_mOkEditModeFill != value)
                {
                    _mOkEditModeFill = value;
                    OnPropertyChanged("OkEditModeFill");
                }
            }
        }


        private Brush _mCancelEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
        public Brush CancelEditModeFill
        {
            get { return _mCancelEditModeFill; }
            set
            {
                if (_mCancelEditModeFill != value)
                {
                    _mCancelEditModeFill = value;
                    OnPropertyChanged("CancelEditModeFill");
                }
            }
        }

        private double _mOkEditModeOpacity = 1;
        public double OkEditModeOpacity
        {
            get { return _mOkEditModeOpacity; }
            set
            {
                if (_mOkEditModeOpacity != value)
                {
                    _mOkEditModeOpacity = value;
                    OnPropertyChanged("OkEditModeOpacity");
                }
            }
        }

        private double _mCancelEditModeOpacity = 1;
        public double CancelEditModeOpacity
        {
            get { return _mCancelEditModeOpacity; }
            set
            {
                if (_mCancelEditModeOpacity != value)
                {
                    _mCancelEditModeOpacity = value;
                    OnPropertyChanged("CancelEditModeOpacity");
                }
            }
        }

        [XmlIgnore]
        public ICommand PointerEntered
        {
            get;
            set;
        }

        public void OnPointerEnteredCommand(object param)
        {
        }
        
        
    }

    public class MessageBoxBusinessObject : PropertyChange, INotifyPropertyChanged
    {
        public MessageBoxBusinessObject()
        {
            Link = new HyperlinkBusinessClass();
            PointerEntered = new Command(OnPointerEnteredCommand);
        }

        private string _mTitle = string.Empty;
        public string Title
        {
            get { return _mTitle; }
            set
            {
                if (_mTitle != value)
                {
                    _mTitle = value;
                    TextChanged();
                    OnPropertyChanged("Title");
                }
            }
        }

        private string _mMessage = string.Empty;
        public string Message
        {
            get { return _mMessage; }
            set
            {
                _mMessage = value;
                TextChanged();
                OnPropertyChanged("Message");
            }
        }        

        private string _mOk = string.Empty;
        public string Ok
        {
            get { return _mOk; }
            set
            {
                _mOk = value;
                TextChanged();
                OnPropertyChanged("Ok");
                Link.Title = value;

                if (string.IsNullOrEmpty(value))
                {
                    EnableBottomBorder = false;
                }
                else
                    EnableBottomBorder = true;
            }
        }

        private bool _mEnableBottomBorder;
        [XmlIgnore]
        public bool EnableBottomBorder
        {
            get { return _mEnableBottomBorder; }
            private set
            {
                if (_mEnableBottomBorder != value)
                {
                    _mEnableBottomBorder = value;
                    OnPropertyChanged("EnableBottomBorder");
                }
            }
        }


        private string _mInput = string.Empty;
        public string Input
        {
            get { return _mInput; }
            set
            {
                _mInput = value;
                //OnPropertyChanged("Input");
            }
        }

        private void TextChanged()
        {
            Input = Title + "\r\n" + Message + "\r\n" + "," + Ok;
        }

        private Brush _mFill;
        [XmlIgnore]
        public Brush Fill
        {
            get { return _mFill; }
            set
            {
                if (_mFill != value)
                {
                    _mFill = value;
                    OnPropertyChanged("Fill");
                }
            }
        }

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

        private string _mExecutedPath = string.Empty;
        public string ExecutedPath
        {
            get { return _mExecutedPath; }
            set { _mExecutedPath = value; }
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
                    EditModeFill = new SolidColorBrush(Colors.Transparent);
                }
            }
        }

        private HyperlinkBusinessClass _mLink;
        [XmlIgnore]
        public HyperlinkBusinessClass Link
        {
            get { return _mLink; }
            set
            {
                _mLink = value;
            }
        }

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

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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

        [XmlIgnore]
        public ICommand PointerEntered
        {
            get;
            set;
        }

        public void OnPointerEnteredCommand(object param)
        {
        }


    }

    public class WindowBoxBusinessObject : PropertyChange, INotifyPropertyChanged
    {
        public WindowBoxBusinessObject() { }

        private Thickness _mTopMargin;
        public Thickness TopMargin
        {
            get { return _mTopMargin; }
            set
            {
                if (_mTopMargin != value)
                {
                    _mTopMargin = value;
                    OnPropertyChanged("TopMargin");
                    WindowBodyHeight = WindowHeight - (value.Top + TitleBarHeight + BottomMargin.Bottom + BottomIconHeight);
                }
            }
        }

        private Thickness _mBottomMargin;
        public Thickness BottomMargin
        {
            get { return _mBottomMargin; }
            set
            {
                if (_mBottomMargin != value)
                {
                    _mBottomMargin = value;
                    OnPropertyChanged("BottomMargin");
                    WindowBodyHeight = WindowHeight - (TopMargin.Top + TitleBarHeight + value.Bottom + BottomIconHeight);
                }
            }
        }

        private double _mWindowBodyHeight;
        public double WindowBodyHeight
        {
            get { return _mWindowBodyHeight; }
            set
            {
                if (_mWindowBodyHeight != value)
                {
                    _mWindowBodyHeight = value;
                    OnPropertyChanged("WindowBodyHeight");
                }
            }
        }

        private double _mWindowHeight;
        public double WindowHeight
        {
            get { return _mWindowHeight; }
            set
            {
                if (_mWindowHeight != value)
                {
                    _mWindowHeight = value;
                    OnPropertyChanged("WindowHeight");
                    WindowBodyHeight = value - (TopMargin.Top + TitleBarHeight + BottomMargin.Bottom + BottomIconHeight);
                }
            }
        }

        private bool _mMinimizeButtonEnabled;
        public bool MinimizeButtonEnabled
        {
            get { return _mMinimizeButtonEnabled; }
            set 
            {
                if (_mMinimizeButtonEnabled != value)
                {
                    _mMinimizeButtonEnabled = value;
                    OnPropertyChanged("MinimizeButtonEnabled");
                }
            }
        }

        private bool _mMaximizeButtonEnabled;
        public bool MaximizeButtonEnabled
        {
            get { return _mMaximizeButtonEnabled; }
            set
            {
                if (_mMaximizeButtonEnabled != value)
                {
                    _mMaximizeButtonEnabled = value;
                    OnPropertyChanged("MaximizeButtonEnabled");
                }
            }
        }

        private bool _mCloseButtonEnabled;
        public bool CloseButtonEnabled
        {
            get { return _mCloseButtonEnabled; }
            set
            {
                if (_mCloseButtonEnabled != value)
                {
                    _mCloseButtonEnabled = value;
                    OnPropertyChanged("CloseButtonEnabled");
                }
            }
        }

        private double _mBottomIconHeight = 20;
        [XmlIgnore]
        public double BottomIconHeight
        {
            get { return _mBottomIconHeight; }
            private set
            {
                if (_mBottomIconHeight != value)
                {
                    _mBottomIconHeight = value;
                    OnPropertyChanged("BottomIconHeight");
                }
            }
        }

        private double _mTopIconHeight = 16;
        [XmlIgnore]
        public double TopIconHeight
        {
            get { return _mTopIconHeight; }
            private set
            {
                if (_mTopIconHeight != value)
                {
                    _mTopIconHeight = value;
                    OnPropertyChanged("TopIconHeight");
                }
            }
        }

        private double _mTitleBarHeight = 0;
        public double TitleBarHeight   
        {
            get { return _mTitleBarHeight; }
            set
            {
                if (_mTitleBarHeight != value)
                {
                    _mTitleBarHeight = value;
                    OnPropertyChanged("TitleBarHeight");
                }
            }
        }

        private VerticalScrollBarBusinessClass _mScrollBar;
        public VerticalScrollBarBusinessClass ScrollBar
        {
            get { return _mScrollBar; }
            set
            {
                if (_mScrollBar != value)
                {
                    _mScrollBar = value;
                    OnPropertyChanged("ScrollBar");
                }
            }
        }
    }

    public class VerticalCurlyBracesBusinessClass : PropertyChange
    {
        public VerticalCurlyBracesBusinessClass()
        {

        }

        private bool _mLeft = true;
        public bool Left
        {
            get { return _mLeft; }
            set
            {
                if (_mLeft != value)
                {
                    _mLeft = value;
                    OnPropertyChanged("Left");
                    if (value)
                        Right = false;
                }
            }
        }

        private bool _mRight = false;
        public bool Right
        {
            get { return _mRight; }
            set
            {
                if (_mRight != value)
                {
                    _mRight = value;
                    OnPropertyChanged("Right");
                    if (value)
                        Left = false;
                }
            }
        }
    }

    public class HorizontalCurlyBracesBusinessClass : PropertyChange
    {
        public HorizontalCurlyBracesBusinessClass()
        {

        }

        private bool _mTop = true;
        public bool Top
        {
            get { return _mTop; }
            set
            {
                if (_mTop != value)
                {
                    _mTop = value;
                    OnPropertyChanged("Top");
                    if (value)
                        Bottom = false;
                }
            }
        }

        private bool _mBottom = false;
        public bool Bottom
        {
            get { return _mBottom; }
            set
            {
                if (_mBottom != value)
                {
                    _mBottom = value;
                    OnPropertyChanged("Bottom");
                    if (value)
                        Top  = false;
                }
            }
        }
    }

    public class OnOffSwitch : PropertyChange, INotifyPropertyChanged
    {
        private Brush _mOnFill;
        private bool _mFillFromDummy = false;
        public OnOffSwitch() 
        {
            On = true;
        }

        private bool _mOn;
        public bool On
        {
            get { return _mOn; }
            set
            {
                if (_mOn != value)
                {
                    _mOn = value;
                    OnPropertyChanged("On");
                    if (value)
                    {
                        if (_mOnFill != null)
                            Fill = _mOnFill;
                        Off = false;
                        HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                    }
                }
            }
        }

        private bool _mOff;
        public bool Off
        {
            get { return _mOff; }
            set
            {
                if (_mOff != value)
                {
                    _mOff = value;
                    OnPropertyChanged("Off");
                    if (value)
                    {
                        _mOnFill = Fill;
                        Fill = GetColorFromHexa("#AAAAAA");
                        On = false;
                        HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
                    }
                }
            }
        }

        private Brush _mFill;
        [XmlIgnore]
        public Brush Fill
        {
            get { return _mFill; }
            set 
            {
                if (_mFill != value)
                {
                    if (On)
                    {
                        _mFill = value;
                        OnPropertyChanged("Fill");
                    }
                    else
                    {
                        _mOnFill = value;
                        if(_mFillFromDummy)
                        {
                            _mFill = value;
                            OnPropertyChanged("Fill");
                            _mFillFromDummy = false;
                        }

                    }
                }
            }
        }

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
                _mFillFromDummy = true;
                Fill = new SolidColorBrush(value.ToColor());
            }
        }

        private HorizontalAlignment _mHorizontalAlignment = HorizontalAlignment.Stretch;
        public new HorizontalAlignment HorizontalAlignment
        {
            get { return _mHorizontalAlignment; }
            set
            {
                if (_mHorizontalAlignment != value)
                {
                    _mHorizontalAlignment = value;
                    OnPropertyChanged("HorizontalAlignment");
                }
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
    }

    public class AccordionSubItemBusinessObject : DiagramElementViewModel, ICommonForCollectionItem
    {
        public AccordionSubItemBusinessObject()
        {
            Link = new HyperlinkBusinessClass();
        }
        private string _mmText;
        public string Item
        {
            get { return _mmText; }
            set
            {
                if (_mmText != value)
                {
                    _mmText = TextChecking(value);
                    OnPropertyChanged("Text");
                }
            }
        }

        private string TextChecking(string text)
        {
            if(char.Equals(text[0],'-'))
            {
                if(char.IsWhiteSpace(text[1]))
                {
                    text = text.Remove(0, 2);
                }
            }
            return text;
        }

        private HyperlinkBusinessClass _mLink;
        public HyperlinkBusinessClass Link
        {
            get
            {
                return _mLink;
            }
            set
            {
                _mLink = value;
            }
        }        

        private int _mIndex;
        public int Index
        {
            get { return _mIndex; }
            set { _mIndex = value; }
        }

        private int _mParentIndex;
        public int ParentIndex
        {
            get { return _mParentIndex; }
            set { _mParentIndex = value; }
        }

        private Brush _mFill;
        public Brush Fill
        {
            get { return _mFill; }
            set
            {
                if (_mFill != value)
                {
                    _mFill = value;
                    OnPropertyChanged("Fill");
                }
            }
        }

        private bool _mIsSelected;
        public bool IsSelected
        {
            get { return _mIsSelected; }
            set { _mIsSelected = value; }
        }
        
    }

    public class AccordionItemBusinessObject : DiagramElementViewModel, ICommonForCollectionItem, ICommonForCollection
    {
        public AccordionItemBusinessObject() 
        {
            Link = new HyperlinkBusinessClass();
            IsExpanded = false;
        }

        private string _mmText;
        public string Item
        {
            get { return _mmText; }
            set
            {
                if (_mmText != value)
                {
                    _mmText = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        private HyperlinkBusinessClass _mLink;
        public HyperlinkBusinessClass Link
        {
            get
            {
                return _mLink;
            }
            set
            {
                _mLink = value;
            }
        }

        private ObservableCollection<object> _mItems;
        public ObservableCollection<object> Items
        {
            get
            {
                return _mItems;
            }
            set
            {
                _mItems = value;
            }
        }

        private bool _mIsExpanded;
        public bool IsExpanded
        {
            get { return _mIsExpanded; }
            set 
            {
                if (_mIsExpanded != value)
                {
                    _mIsExpanded = value;
                    OnPropertyChanged("IsExpanded");
                    if (value)
                        Fill = GetColorFromHexa("#83c1f0");
                    else
                    {
                        Fill = new SolidColorBrush(Colors.Transparent);
                        if(Items != null)
                        {
                            foreach(var i in Items)
                            {
                                (i as AccordionSubItemBusinessObject).Fill = new SolidColorBrush(Colors.Transparent);
                                (i as AccordionSubItemBusinessObject).IsSelected = false;
                            }
                        }
                    }
                }
            }
        }

        private int _mIndex;
        public int Index
        {
            get { return _mIndex; }
            set { _mIndex = value; }
        }

        private Brush _mFill;
        public Brush Fill
        {
            get { return _mFill; }
            set 
            {
                if (_mFill != value)
                {
                    _mFill = value;
                    OnPropertyChanged("Fill");
                }
            }
        }

        private double _mHeight;
        public double Height
        {
            get { return _mHeight; }
            set 
            {
                if (_mHeight != value)
                {
                    _mHeight = value;
                    OnPropertyChanged("Height");
                }
            }
        }

        private double _mBottomSpace = 50;
        public double BottomSpace
        {
            get { return _mBottomSpace; }
            private set 
            {
                _mBottomSpace = value; 
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
    }

    public class AccordionBusinessObject : DiagramElementViewModel, INotifyPropertyChanged, ICommonForCollection
    {
        public AccordionBusinessObject() 
        {
            ScrollBar = new VerticalScrollBarBusinessClass();
        }

        private ObservableCollection<object> _mItems;
        public ObservableCollection<object> Items
        {
            get
            {
                return _mItems;
            }
            set
            {
                _mItems = value;
            }
        }

        private int _mSelectedIndex = 1;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set 
            {
                _mSelectedIndex = value;
                if (value == 0)
                {
                    foreach (var item in Items)
                    {
                        (item as AccordionItemBusinessObject).IsExpanded = false;
                    }
                }
                else
                {
                    for (int x = 1; x <= Items.Count; x++)
                    {
                        if (value == x)
                        {
                            (Items[x - 1] as AccordionItemBusinessObject).IsExpanded = true;
                        }
                        else
                        {
                            (Items[x - 1] as AccordionItemBusinessObject).IsExpanded = false;
                        }
                    }

                    //foreach (var i in Items)
                    //{
                    //    //if (_mAnnotation.Content == null || string.IsNullOrEmpty(_mAnnotation.Content.ToString()))
                    //    {
                    //        //double height = FindDummyTextSize((i as AccordionItemBusinessObject).Item).Height;
                    //        if ((i as AccordionItemBusinessObject).Items != null && (i as AccordionItemBusinessObject).Items.Count > 0)
                    //        {
                    //            if ((i as AccordionItemBusinessObject).IsExpanded)
                    //                (i as AccordionItemBusinessObject).Height = (ItemTitleHeight * (i as AccordionItemBusinessObject).Items.Count) + (i as AccordionItemBusinessObject).BottomSpace;
                    //            else
                    //                (i as AccordionItemBusinessObject).Height = ItemTitleHeight;
                    //        }
                    //        else
                    //        {
                    //            (i as AccordionItemBusinessObject).Height = ItemTitleHeight;
                    //        }
                    //    }
                    //}
                }
            }
        }        

        private VerticalScrollBarBusinessClass _mScrollBar;
        public VerticalScrollBarBusinessClass ScrollBar
        {
            get { return _mScrollBar; }
            set
            {
                if (_mScrollBar != value)
                {
                    _mScrollBar = value;
                    OnPropertyChanged("ScrollBar");
                }
            }
        }

        private double _mItemTitleHeight = 30;
        public double ItemTitleHeight
        {
            get { return _mItemTitleHeight; }
            private set
            {
                if (_mItemTitleHeight != value)
                {
                    _mItemTitleHeight = value;
                    OnPropertyChanged("ItemTitleHeight");
                }
            }
        }
        
        
    }

    public class ComboBoxItemBusinessObject : DiagramElementViewModel
    {
        private string _mItem;
        public string Item
        {
            get { return _mItem; }
            set
            {
                if (_mItem != value)
                {
                    _mItem = value;
                    OnPropertyChanged("Item");
                }
            }
        }

        private double _mLineHeight = 0;
        public double ItemHeight
        {
            get { return _mLineHeight; }
            set
            {
                if (_mLineHeight != value)
                {
                    _mLineHeight = value;
                    OnPropertyChanged("ItemHeight");
                }
            }
        }
    }

    public class ComboBoxBusinessClass : PropertyChange, IState
    {
        private Brush _mStrokeOnSelectedState;
        private Brush _mForegroundOnNormalState;
        public ComboBoxBusinessClass() 
        { 
            Title = "Combobox";
            Link = new HyperlinkBusinessClass();
            Items = new ObservableCollection<ComboBoxItemBusinessObject>();
            States = new ObservableCollection<object>()
            {
                "Normal", 
                "Disabled",
                "Closed",
                "Closed and Disabled"
            };
        }

        private string _mTitle;
        public string Title
        {
            get { return _mTitle; }
            set 
            {
                if (_mTitle != value)
                {
                    _mTitle = value;
                    OnPropertyChanged("Title");
                }
            }
        }        

        private ObservableCollection<ComboBoxItemBusinessObject> _mItems;
        public ObservableCollection<ComboBoxItemBusinessObject> Items
        {
            get { return _mItems; }
            set
            {
                if (_mItems != value)
                {
                    _mItems = value;
                    OnPropertyChanged("Items");
                }
            }
        }

        private double _mItemHeight = 0;
        public double ItemHeight
        {
            get { return _mItemHeight; }
            set
            {
                if (_mItemHeight != value)
                {
                    _mItemHeight = value;
                    OnPropertyChanged("ItemHeight");
                }
            }
        }

        private bool _mShowItems = true;
        public bool ShowItems
        {
            get { return _mShowItems; }
            set 
            {
                if (_mShowItems != value)
                {
                    _mShowItems = value;
                    OnPropertyChanged("ShowItems");
                }
            }
        }

        private bool _mDisabled;
        public bool Disabled
        {
            get { return _mDisabled; }
            set 
            {
                if (_mDisabled != value)
                {
                    if(value)
                    {
                        _mStrokeOnSelectedState = Stroke;
                        Stroke = GetColorFromHexa("#AAAAAA");
                        _mForegroundOnNormalState = Foreground;
                        Foreground = GetColorFromHexa("#AAAAAA");
                    }
                    _mDisabled = value;
                    if(!value)
                    {
                        if (_mStrokeOnSelectedState != null)
                            Stroke = _mStrokeOnSelectedState;
                        if (_mForegroundOnNormalState != null)
                            Foreground = _mForegroundOnNormalState;
                    }
                }
            }
        }
        

        private int _mSelectedIndex = 0;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set 
            {
                if (value == 1 || value == 3)
                    Disabled = true;
                else
                    Disabled = false;

                _mSelectedIndex = value;
                if (value == 0 || value == 1)
                    ShowItems = true;
                else
                    ShowItems = false;
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

        private Brush _mStroke;
        [XmlIgnore]
        public Brush Stroke
        {
            get { return _mStroke; }
            set 
            {
                if (_mStroke != value)
                {
                    if(!Disabled)
                    {
                        _mStroke = value;
                        OnPropertyChanged("Stroke");
                    }
                    else
                        _mStrokeOnSelectedState = value;
                }
            }
        }

        public string StrokeDummy
        {
            get
            {
                if (Stroke != null && Stroke is SolidColorBrush)
                    return (Stroke as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Stroke = new SolidColorBrush(value.ToColor());
            }
        }

        private Brush _mForeground;
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set
            {
                if (_mForeground != value)
                {
                    if (!Disabled)
                    {
                        _mForeground = value;
                        OnPropertyChanged("Foreground");
                    }
                    else
                        _mForegroundOnNormalState = value;
                }
            }
        }

        public string ForegroundDummy
        {
            get
            {
                if (Foreground != null && Foreground is SolidColorBrush)
                    return (Foreground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Foreground = new SolidColorBrush(value.ToColor());
            }
        }

        [XmlIgnore]
        public ObservableCollection<object> States { get; set; }

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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

        private HyperlinkBusinessClass _mLink;
        [XmlIgnore]
        public HyperlinkBusinessClass Link
        {
            get { return _mLink; }
            set
            {
                _mLink = value;
            }
        }

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
                    }
                }
            }
        }
    }

    public class ComboBoxBusinessObject : DiagramElementViewModel
    {
        private double _mHeightOnSelectedState;

        // Used to check whether the Fill / Stroke values changed because of Disabled value changed from false to true 
        private bool _mModifiedFromDisabledState = false;

        //These private fields will maintain the Fill / Stroke at Disabled state 
        private Brush _mFillOnSelectedState;
        private Brush _mStrokeOnSelectedState;

        private ObservableCollection<ComboBoxItemBusinessObject> _mItems;
        public ObservableCollection<ComboBoxItemBusinessObject> Items
        {
            get { return _mItems; }
            set
            {
                if (_mItems != value)
                {
                    _mItems = value;
                    OnPropertyChanged("Items");
                }
            }
        }

        private double _mLineHeight;
        public double ItemHeight
        {
            get { return _mLineHeight; }
            set
            {
                if (_mLineHeight != value)
                {
                    _mLineHeight = value;
                    OnPropertyChanged("ItemHeight");
                }

                for (int x = 0; x < Items.Count; x++)
                {
                    if (x == 0)
                        Items[x].ItemHeight = value;
                    else
                        Items[x].ItemHeight = 0;
                }
            }
        }

        private Brush _mFill;
        public Brush Fill
        {
            get { return _mFill; }
            set
            {
                if (_mFill != value)
                {
                    if (!_mDisabled || _mModifiedFromDisabledState)
                        _mFill = value;
                    OnPropertyChanged("Fill");

                    if (!_mModifiedFromDisabledState)
                        _mFillOnSelectedState = value;
                }
            }
        }

        private Brush _mStroke;
        public Brush Stroke
        {
            get { return _mStroke; }
            set
            {
                if (_mStroke != value)
                {
                    if (!_mDisabled || _mModifiedFromDisabledState)
                        _mStroke = value;
                    OnPropertyChanged("Stroke");

                    if (!_mModifiedFromDisabledState)
                        _mStrokeOnSelectedState = value;
                }
            }
        }

        private double _mStrokeThickness;
        public double StrokeThickness
        {
            get { return _mStrokeThickness; }
            set
            {
                if (_mStrokeThickness != value)
                {
                    _mStrokeThickness = value;
                    OnPropertyChanged("StrokeThickness");
                }
            }
        }

        private double _mOpacity;
        public double Opacity
        {
            get { return _mOpacity; }
            set
            {
                if (_mOpacity != value)
                {
                    _mOpacity = value;
                    OnPropertyChanged("Opacity");
                }
            }
        }

        private bool _mDisabled;
        public bool Disabled
        {
            get { return _mDisabled; }
            set
            {
                if (_mDisabled != value)
                {
                    _mModifiedFromDisabledState = true;
                    bool temp = _mDisabled;
                    _mDisabled = value;
                    OnPropertyChanged("Disabled");
                    if (!temp && value)
                    {
                        _mStrokeOnSelectedState = Stroke;
                        _mFillOnSelectedState = Fill;
                        Stroke = new SolidColorBrush(Colors.Red);
                        Fill = new SolidColorBrush(Colors.Red);
                    }
                    else if (temp && !value)
                    {
                        Stroke = _mStrokeOnSelectedState;
                        Fill = _mFillOnSelectedState;
                    }
                    _mModifiedFromDisabledState = false;
                }
            }
        }

        private bool? _mExpanded = true;
        public bool? Expanded
        {
            get { return _mExpanded; }
            set
            {
                if (_mExpanded != value)
                {
                    _mExpanded = value;
                    OnPropertyChanged("Expanded");

                    if (value != null && (bool)value)
                    {
                        Height = _mHeightOnSelectedState;
                    }
                    else
                    {
                        _mHeightOnSelectedState = Height;
                        Height = ItemHeight + 11;
                    }
                }
            }
        }

        private double _mWidth;
        public double Width
        {
            get { return _mWidth; }
            set
            {
                if (_mWidth != value)
                {
                    _mWidth = value;
                    OnPropertyChanged("Width");
                }
            }
        }

        private double _mHeight;
        public double Height
        {
            get { return _mHeight; }
            set
            {
                if (_mHeight != value)
                {
                    _mHeight = value;
                    OnPropertyChanged("Height");

                    if (Expanded != null && Expanded.Value && _mHeightOnSelectedState != Height)
                        _mHeightOnSelectedState = Height;
                }
            }
        }
    }

    public class MultiLineButtonBusinessObject : PropertyChange
    {
        public MultiLineButtonBusinessObject() 
        {
            TitleMargin = new Thickness(0, 3, 0, 3);
        }

        private string _mTitle = string.Empty;
        public string Title
        {
            get { return _mTitle; }
            set
            {
                if (_mTitle != value)
                {
                    _mTitle = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private string _mMessage = string.Empty;
        public string Message
        {
            get { return _mMessage; }
            set
            {
                _mMessage = value;
                OnPropertyChanged("Message");
            }
        }

        private double _mTitleHeight;
        public double TitleHeight
        {
            get { return _mTitleHeight; }
            set
            {
                if (_mTitleHeight != value)
                {
                    _mTitleHeight = value;
                    OnPropertyChanged("TitleHeight");
                }
            }
        }

        private Thickness _mTitleMargin;
        [XmlIgnore]
        public Thickness TitleMargin
        {
            get { return _mTitleMargin; }
            private set
            {
                if (_mTitleMargin != value)
                {
                    _mTitleMargin = value;
                    OnPropertyChanged("TitleMargin");
                }
            }
        }
    }

    public class ButtonBusinessObject : PropertyChange, IState 
    {
        public ButtonBusinessObject()
        {
            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            States = new ObservableCollection<object>()
            {
                "Normal", 
                "Selected",
                "InFocus",
                "Disabled"
            };
        }

        Brush _mNormalStateFill;

        private Brush _mFill;
        [XmlIgnore]
        public Brush Fill
        {
            get { return _mFill; }
            set
            {
                if (_mFill != value)
                {
                    if (!Selected)
                    {
                        _mFill = value;
                        OnPropertyChanged("Fill");
                    }
                    else
                        _mNormalStateFill = value;
                }
            }
        }

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

        private bool _mSelected;        
        public bool Selected
        {
            get { return _mSelected; }
            set 
            {
                if (_mSelected != value)
                {
                    if (value)
                    {
                        _mNormalStateFill = Fill;
                        Fill = GetColorFromHexa("#83c1f0");
                    }
                    _mSelected = value;
                    if(!value)
                    {
                        if (_mNormalStateFill != null)
                            Fill = _mNormalStateFill;
                    }
                }
            }
        }        

        private int _mSelectedIndex;         
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set 
            {
                _mSelectedIndex = value;
                if(value == 1)
                {
                    Selected = true;
                }
                else
                {
                    Selected = false;
                }
            }
        }        

        // For MultiLine Button
        private string _mTitle = string.Empty;
        [XmlIgnore]
        public string Title
        {
            get { return _mTitle; }
            set
            {
                if (_mTitle != value)
                {
                    _mTitle = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private string _mMessage = string.Empty;
        [XmlIgnore]
        public string Message
        {
            get { return _mMessage; }
            set
            {
                _mMessage = value;
                OnPropertyChanged("Message");
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

        [XmlIgnore]
        public ObservableCollection<object> States { get; set; }        
    }    

    public class CheckBoxBusinessClass : PropertyChange, IState
    {
        public CheckBoxBusinessClass()
        {
            States = new ObservableCollection<object>()
            {
                "Normal", 
                "Selected",
                "Disabled",
                "Disabled And Selected"
            };
        }

        // Used to check whether the Fill / Stroke values changed because of Disabled value changed from false to true 
        private bool _mModifiedFromDisabledState = false;

        //These private fields will maintain the Fill / Stroke at Disabled state 
        private Brush _mForegroundOnSelectedState;
        //private Brush _mStrokeOnSelectedState;

        private bool _mSelected = false;
        [XmlIgnore]
        public bool Selected
        {
            get { return _mSelected; }
            private set
            {
                if (_mSelected != value)
                {
                    _mSelected = value;
                    OnPropertyChanged("Selected");
                }
            }
        }

        private bool _mDisabled = false;
        [XmlIgnore]
        public bool Disabled
        {
            get { return _mDisabled; }
            private set
            {
                if (_mDisabled != value)
                {
                    _mModifiedFromDisabledState = true;
                    bool temp = _mDisabled;
                    _mDisabled = value;
                    OnPropertyChanged("Disabled");
                    if (!temp && value)
                    {
                        //_mStrokeOnSelectedState = Stroke;
                        _mForegroundOnSelectedState = Foreground;
                        //Stroke = GetColorFromHexa("#AAAAAA"); //new SolidColorBrush(Colors.Red);
                        Foreground = GetColorFromHexa("#AAAAAA"); //new SolidColorBrush(Colors.Red);
                    }
                    else if (temp && !value)
                    {
                        //Stroke = _mStrokeOnSelectedState;
                        Foreground = _mForegroundOnSelectedState;
                    }
                    _mModifiedFromDisabledState = false;
                }
            }
        }

        private int _mSelectedIndex;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set 
            {
                _mSelectedIndex = value;
                if (value == 0)
                {
                    Disabled = false;
                    Selected = false;
                }
                if (value == 1)
                {
                    Disabled = false;
                    Selected = true;
                }
                if (value == 2)
                {
                    Disabled = true;
                    Selected = false;
                }
                else if (value == 3)
                {
                    Disabled = true;
                    Selected = true;
                }
            }
        }       

        private Brush _mForeground;
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set
            {
                if (_mForeground != value)
                {
                    if (!_mDisabled || _mModifiedFromDisabledState)
                        _mForeground = value;
                    OnPropertyChanged("Foreground");

                    if (!_mModifiedFromDisabledState)
                        _mForegroundOnSelectedState = value;
                }
            }
        }

        public string DummyForeground
        {
            get
            {
                if (Foreground != null && Foreground is SolidColorBrush)
                    return (Foreground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Foreground = new SolidColorBrush(value.ToColor());
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

        [XmlIgnore]
        public ObservableCollection<object> States { get; set; }

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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
    }

    public class CommonForCheckBoxRadioButtonBusinessClass : PropertyChange
    {
        public CommonForCheckBoxRadioButtonBusinessClass() { }
        private Brush _mSelectedStroke;
        private Brush _mSelectedForeground;

        private bool _mSelected;
        public bool Selected
        {
            get { return _mSelected; }
            set
            {
                if (_mSelected != value)
                {
                    _mSelected = value;
                    OnPropertyChanged("Selected");
                }
            }
        }

        private bool _mDisabled;
        public bool Disabled
        {
            get { return _mDisabled; }
            set
            {
                if (_mDisabled != value)
                {
                    if (value)
                    {
                        Foreground = GetColorFromHexa("#AAAAAA");
                        Stroke = GetColorFromHexa("#AAAAAA");
                    }
                    _mDisabled = value;
                    OnPropertyChanged("Disabled");
                    if (!value)
                    {
                        Foreground = _mSelectedForeground;
                        Stroke = _mSelectedStroke;
                    }
                }
            }
        }

        private bool _mCheckBox;
        public bool CheckBox
        {
            get { return _mCheckBox; }
            set
            {
                if (_mCheckBox != value)
                {
                    _mCheckBox = value;
                    OnPropertyChanged("CheckBox");
                    if (!_mCheckBox)
                    {
                        Selected = false;
                        Disabled = false;
                    }
                }
            }
        }

        private Brush _mForeground;
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set
            {
                if (_mForeground != value)
                {
                    if (!_mDisabled)
                    {
                        _mForeground = value;
                        OnPropertyChanged("Foreground");
                    }
                    else
                        _mSelectedForeground = value;
                }
            }
        }

        //public string ForegroundDummy
        //{
        //    get
        //    {
        //        if (Foreground != null && Foreground is SolidColorBrush)
        //            return (Foreground as SolidColorBrush).Color.ToString();
        //        else
        //            return null;
        //    }
        //    set
        //    {
        //        Foreground = new SolidColorBrush(value.ToColor());
        //    }
        //}

        private Brush _mStroke;
        [XmlIgnore]
        public Brush Stroke
        {
            get { return _mStroke; }
            set
            {
                if (_mStroke != value)
                {
                    if (!_mDisabled)
                    {
                        _mStroke = value;
                        OnPropertyChanged("Stroke");
                    }
                    else
                        _mSelectedStroke = Stroke;
                }
            }
        }

        //public string StrokeDummy
        //{
        //    get
        //    {
        //        if (Stroke != null && Stroke is SolidColorBrush)
        //            return (Stroke as SolidColorBrush).Color.ToString();
        //        else
        //            return null;
        //    }
        //    set
        //    {
        //        Stroke = new SolidColorBrush(value.ToColor());
        //    }
        //}

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
    }

    public class CheckBoxItemBusinessClass : CommonForCheckBoxRadioButtonBusinessClass, ICommonForCollectionItem
    {
        public CheckBoxItemBusinessClass()
        {
            Link = new HyperlinkBusinessClass();
            PointerEntered = new Command(OnPointerEnteredCommand);
        }

        private string _mmText;
        public string Item
        {
            get { return _mmText; }
            set
            {
                if (_mmText != value)
                {
                    _mmText = CheckBoxText(value);
                    OnPropertyChanged("Text");

                }
            }
        }

        private string CheckBoxText(string _mText)
        {
            if (_mText.Contains("-[x]"))
            {
                Disabled = true;
                Selected = true;
                CheckBox = true;

                //MarkerVisibility = Visibility.Visible;
                //SelectionBoxVisibility = Visibility.Visible;
                ////Foreground = new SolidColorBrush(Colors.Red);
                //Foreground = GetColorFromHexa("#E2E2E2");
                return _mText.Split(new string[] { "-[x]" }, StringSplitOptions.None).Last();
            }
            else if (_mText.Contains("[x]"))
            {
                Disabled = false;
                Selected = true;
                CheckBox = true;
                //MarkerVisibility = Visibility.Visible;
                //SelectionBoxVisibility = Visibility.Visible;
                return _mText.Split(new string[] { "[x]" }, StringSplitOptions.None).Last();
            }
            else if (_mText.Contains("-[]"))
            {
                Disabled = true;
                Selected = false;
                CheckBox = true;
                //MarkerVisibility = Visibility.Collapsed;
                //SelectionBoxVisibility = Visibility.Visible;
                ////Foreground = new SolidColorBrush(Colors.Red);
                //Foreground = GetColorFromHexa("#E2E2E2");
                return _mText.Split(new string[] { "-[]" }, StringSplitOptions.None).Last();
            }
            else if (_mText.Contains("[]"))
            {
                Disabled = false;
                Selected = false;
                CheckBox = true;
                //MarkerVisibility = Visibility.Collapsed;
                //SelectionBoxVisibility = Visibility.Visible;
                return _mText.Split(new string[] { "[]" }, StringSplitOptions.None).Last();
            }
            else
            {
                CheckBox = false;
                //MarkerVisibility = Visibility.Collapsed;
                //SelectionBoxVisibility = Visibility.Collapsed;
                return _mText;
            }
        }

        private HyperlinkBusinessClass _mLink;
        [XmlIgnore]
        public HyperlinkBusinessClass Link
        {
            get { return _mLink; }
            set
            {
                _mLink = value;
            }
        }

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

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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

        [XmlIgnore]
        public ICommand PointerEntered
        {
            get;
            set;
        }

        public void OnPointerEnteredCommand(object param)
        {
        }
    }

    public class CheckBoxGroupBusinessClass : PropertyChange, ICommonForCollection
    {
        public CheckBoxGroupBusinessClass() { }

        //private ObservableCollection<CheckBoxItemBusinessClass> _mItems;
        //public ObservableCollection<CheckBoxItemBusinessClass> Items
        //{
        //    get { return _mItems; }
        //    set
        //    {
        //        if (_mItems != value)
        //        {
        //            _mItems = value;
        //        }
        //    }
        //}
        
        private ObservableCollection<object> _mItems;
        public ObservableCollection<object> Items
        {
            get { return _mItems; }
            set
            {
                if (_mItems != value)
                {
                    _mItems = value;
                }
            }
        }

        private Brush _mForeground;
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set
            {
                if (_mForeground != value)
                {
                    _mForeground = value;
                    OnPropertyChanged("Foreground");
                    foreach (var item in Items)
                    {
                        (item as CheckBoxItemBusinessClass).Foreground = value;
                    }
                }
            }
        }

        public string ForegroundDummy
        {
            get
            {
                if (Foreground != null && Foreground is SolidColorBrush)
                    return (Foreground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Foreground = new SolidColorBrush(value.ToColor());
            }
        }

        private Brush _mStroke;
        [XmlIgnore]
        public Brush Stroke
        {
            get { return _mStroke; }
            set
            {
                if (_mStroke != value)
                {
                    _mStroke = value;
                    OnPropertyChanged("Stroke");
                    foreach (var item in Items)
                    {
                        (item as CheckBoxItemBusinessClass).Stroke = value;
                    }
                }
            }
        }

        public string StrokeDummy
        {
            get
            {
                if (Stroke != null && Stroke is SolidColorBrush)
                    return (Stroke as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Stroke = new SolidColorBrush(value.ToColor());
            }
        }

        private double _mItemHeight;
        public double ItemHeight
        {
            get { return _mItemHeight; }
            set
            {
                if (_mItemHeight != value)
                {
                    _mItemHeight = value;
                    OnPropertyChanged("ItemHeight");
                }
            }
        }

        private string _mExecutedPath = string.Empty;
        public string ExecutedPath
        {
            get { return _mExecutedPath; }
            set { _mExecutedPath = value; }
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
                        foreach (var item in Items)
                        {
                            (item as CheckBoxItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                }
            }
        }
    }


    public class RadioButtonItemBusinessClass : CommonForCheckBoxRadioButtonBusinessClass, ICommonForCollectionItem
    {
        public RadioButtonItemBusinessClass()
        {
            Link = new HyperlinkBusinessClass();
            PointerEntered = new Command(OnPointerEnteredCommand);
        }

        private string _mmText;
        public string Item
        {
            get { return _mmText; }
            set
            {
                if (_mmText != value)
                {
                    _mmText = RadioButtonText(value);
                    OnPropertyChanged("Text");

                }
            }
        }

        private string RadioButtonText(string _mText)
        {
            if (_mText.Contains("-(o)"))
            {
                Disabled = true;
                Selected = true;
                CheckBox = true;
                //MarkerVisibility = Visibility.Visible;
                //SelectionBoxVisibility = Visibility.Visible;
                ////Foreground = new SolidColorBrush(Colors.Red);
                //Foreground = GetColorFromHexa("#E2E2E2");
                return _mText.Split(new string[] { "-(o)" }, StringSplitOptions.None).Last();
            }
            else if (_mText.Contains("(o)"))
            {
                Disabled = false;
                Selected = true;
                CheckBox = true;
                //MarkerVisibility = Visibility.Visible;
                //SelectionBoxVisibility = Visibility.Visible;
                return _mText.Split(new string[] { "(o)" }, StringSplitOptions.None).Last();
            }
            else if (_mText.Contains("-()"))
            {
                Disabled = true;
                Selected = false;
                CheckBox = true;
                //MarkerVisibility = Visibility.Collapsed;
                //SelectionBoxVisibility = Visibility.Visible;
                ////Foreground = new SolidColorBrush(Colors.Red);
                //Foreground = GetColorFromHexa("#E2E2E2");
                return _mText.Split(new string[] { "-()" }, StringSplitOptions.None).Last();
            }
            else if (_mText.Contains("()"))
            {
                Disabled = false;
                Selected = false;
                CheckBox = true;
                //MarkerVisibility = Visibility.Collapsed;
                //SelectionBoxVisibility = Visibility.Visible;
                return _mText.Split(new string[] { "()" }, StringSplitOptions.None).Last();
            }
            else
            {
                CheckBox = false;
                //MarkerVisibility = Visibility.Collapsed;
                //SelectionBoxVisibility = Visibility.Collapsed;
                return _mText;
            }
        }

        private HyperlinkBusinessClass _mLink;
        [XmlIgnore]
        public HyperlinkBusinessClass Link
        {
            get { return _mLink; }
            set
            {
                _mLink = value;
            }
        }

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

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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

        [XmlIgnore]
        public ICommand PointerEntered
        {
            get;
            set;
        }

        public void OnPointerEnteredCommand(object param)
        {
        }
    }

    public class RadioButtonGroupBusinessClass : PropertyChange, ICommonForCollection
    {
        public RadioButtonGroupBusinessClass() { }

        //private ObservableCollection<RadioButtonItemBusinessClass> _mItems;
        //public ObservableCollection<RadioButtonItemBusinessClass> Items
        //{
        //    get { return _mItems; }
        //    set
        //    {
        //        if (_mItems != value)
        //        {
        //            _mItems = value;
        //        }
        //    }
        //}

        private ObservableCollection<object> _mItems;
        public ObservableCollection<object> Items
        {
            get { return _mItems; }
            set
            {
                if (_mItems != value)
                {
                    _mItems = value;
                }
            }
        }

        private Brush _mForeground;
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set
            {
                if (_mForeground != value)
                {
                    _mForeground = value;
                    OnPropertyChanged("Foreground");
                    foreach (var item in Items)
                    {
                        (item as RadioButtonItemBusinessClass).Foreground = value;
                    }
                }
            }
        }

        public string ForegroundDummy
        {
            get
            {
                if (Foreground != null && Foreground is SolidColorBrush)
                    return (Foreground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Foreground = new SolidColorBrush(value.ToColor());
            }
        }

        private Brush _mStroke;
        [XmlIgnore]
        public Brush Stroke
        {
            get { return _mStroke; }
            set
            {
                if (_mStroke != value)
                {
                    _mStroke = value;
                    OnPropertyChanged("Stroke");
                    foreach (var item in Items)
                    {
                        (item as RadioButtonItemBusinessClass).Stroke = value;
                    }
                }
            }
        }

        public string StrokeDummy
        {
            get
            {
                if (Stroke != null && Stroke is SolidColorBrush)
                    return (Stroke as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Stroke = new SolidColorBrush(value.ToColor());
            }
        }

        private double _mItemHeight;
        public double ItemHeight
        {
            get { return _mItemHeight; }
            set
            {
                if (_mItemHeight != value)
                {
                    _mItemHeight = value;
                    OnPropertyChanged("ItemHeight");
                }
            }
        }

        private string _mExecutedPath = string.Empty;
        public string ExecutedPath
        {
            get { return _mExecutedPath; }
            set { _mExecutedPath = value; }
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
                        foreach (var item in Items)
                        {
                            (item as RadioButtonItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                }
            }
        }
    }

    public class DateChoosetBusinessClass : PropertyChange, IState
    {
        public DateChoosetBusinessClass()
        {
            CalendarStroke = GetColorFromHexa("#666666");
            States = new ObservableCollection<object>()
            {
                "Normal", 
                "InFocus",
                "Disabled"
            };
        }

        private bool _mModifiedFromDisabledState = false;

        //These private fields will maintain the Fill / Stroke at Disabled state 
        private Brush _mForegroundOnSelectedState;
        private Brush _mStrokeOnSelectedState;
        private Brush _mFillOnSelectedState;

        private bool _mSelected;
        [XmlIgnore]
        public bool Selected
        {
            get { return _mSelected; }
            private set
            {
                if (_mSelected != value)
                {
                    _mModifiedFromDisabledState = true;
                    bool temp = _mSelected;
                    _mSelected = value;
                    OnPropertyChanged("Selected");
                    if (!temp && value)
                    {
                        if (!_mDisabled)
                        {
                            _mStrokeOnSelectedState = Stroke;
                            _mForegroundOnSelectedState = Foreground;
                        }
                        Stroke = GetColorFromHexa("#83c1f0");
                        Foreground = new SolidColorBrush(Colors.Black);
                        Fill = GetColorFromHexa("#83c1f0");
                    }
                    else if (temp && !value && !_mDisabled)
                    {
                        Stroke = _mStrokeOnSelectedState;
                        Foreground = _mForegroundOnSelectedState;
                        Fill = _mFillOnSelectedState;
                    }
                    if(value)
                    {
                        CalendarStroke = GetColorFromHexa("#666666");
                    }
                    else
                    {
                        if(Disabled)
                            CalendarStroke = GetColorFromHexa("#AAAAAA");
                        else
                            CalendarStroke = GetColorFromHexa("#666666");
                    }
                    _mModifiedFromDisabledState = false;
                }
            }
        }

        private bool _mDisabled;
        [XmlIgnore]
        public bool Disabled
        {
            get { return _mDisabled; }
            private set
            {
                if (_mDisabled != value)
                {
                    _mModifiedFromDisabledState = true;
                    bool temp = _mDisabled;
                    _mDisabled = value;
                    OnPropertyChanged("Disabled");
                    if (!temp && value)
                    {
                        if (!_mSelected)
                        {
                            _mStrokeOnSelectedState = Stroke;
                            _mForegroundOnSelectedState = Foreground;
                        }
                        Stroke = GetColorFromHexa("#AAAAAA"); //new SolidColorBrush(Colors.Red);
                        Foreground = GetColorFromHexa("#AAAAAA"); //new SolidColorBrush(Colors.Red);
                        Fill = GetColorFromHexa("#AAAAAA"); //new SolidColorBrush(Colors.Red);
                        CalendarStroke = GetColorFromHexa("#AAAAAA");
                    }
                    else if (temp && !value && !_mSelected)
                    {
                        Stroke = _mStrokeOnSelectedState;
                        Foreground = _mForegroundOnSelectedState;
                        Fill = _mFillOnSelectedState;
                    }
                    if(!value)
                    {
                        CalendarStroke = GetColorFromHexa("#666666");
                    }
                    _mModifiedFromDisabledState = false;
                }
            }
        }

        private Brush _mStroke;
        [XmlIgnore]
        public Brush Stroke
        {
            get { return _mStroke; }
            set
            {
                if (_mStroke != value)
                {
                    if ((!_mDisabled && !_mSelected) || _mModifiedFromDisabledState)
                        _mStroke = value;
                    OnPropertyChanged("Stroke");

                    if (!_mModifiedFromDisabledState)
                        _mStrokeOnSelectedState = value;
                }
            }
        }

        public string DummyStroke
        {
            get
            {
                if (Stroke != null && Stroke is SolidColorBrush)
                    return (Stroke as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Stroke = new SolidColorBrush(value.ToColor());
            }
        }

        private Brush _mCalendarStroke;
        [XmlIgnore]
        public Brush CalendarStroke
        {
            get { return _mCalendarStroke; }
            set
            {
                if (_mCalendarStroke != value)
                {
                    _mCalendarStroke = value;
                    OnPropertyChanged("CalendarStroke");
                }
            }
        }

        public string DummyCalendarStroke
        {
            get
            {
                if (CalendarStroke != null && CalendarStroke is SolidColorBrush)
                    return (CalendarStroke as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                CalendarStroke = new SolidColorBrush(value.ToColor());
            }
        }

        private Brush _mForeground;
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set
            {
                if (_mForeground != value)
                {
                    if ((!_mDisabled && !_mSelected) || _mModifiedFromDisabledState)
                        _mForeground = value;
                    OnPropertyChanged("Foreground");

                    if (!_mModifiedFromDisabledState)
                        _mForegroundOnSelectedState = value;
                }
            }
        }

        public string DummyForeground
        {
            get
            {
                if (Foreground != null && Foreground is SolidColorBrush)
                    return (Foreground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Foreground = new SolidColorBrush(value.ToColor());
            }
        }

        private Brush _mFill;
        [XmlIgnore]
        public Brush Fill
        {
            get { return _mFill; }
            set
            {
                if (_mFill != value)
                {
                    if ((!_mDisabled && !_mSelected) || _mModifiedFromDisabledState)
                        _mFill = value;
                    OnPropertyChanged("Fill");

                    if (!_mModifiedFromDisabledState)
                        _mFillOnSelectedState = value;
                }
            }
        }

        public string DummyFill
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

        private int _mSelectedIndex;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set 
            { 
                _mSelectedIndex = value;
                if (value == 0)
                {
                    Disabled = false;
                    Selected = false;
                }
                if (value == 1)
                {
                    Disabled = false;
                    Selected = true;
                }
                if (value == 2)
                {
                    Disabled = true;
                    Selected = false;
                }
            }
        }

        private double _mWidth = 16;
        [XmlIgnore]
        public double CalendarWidth
        {
            get { return _mWidth; }
            private set
            {
                if (_mWidth != value)
                {
                    _mWidth = value;
                    OnPropertyChanged("Width");
                }
            }
        }

        private Thickness _mCalendarMargin = new Thickness(5,0,0,0);
        public Thickness CalendarMargin
        {
            get { return _mCalendarMargin; }
            set
            {
                if (_mCalendarMargin != value)
                {
                    _mCalendarMargin = value;
                    OnPropertyChanged("CalendarMargin");
                }
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
        
        [XmlIgnore]
        public ObservableCollection<object> States { get; set; }
    }

    public class SearchBoxBusinessClass : PropertyChange, IState
    {
        public SearchBoxBusinessClass()
        {
            States = new ObservableCollection<object>()
            {
                "Normal", 
                "InFocus",
                "Disabled"
            };
        }

        Brush _mNormalStateForeground;
        private bool _mDisabled;
        public bool Disabled
        {
            get { return _mDisabled; }
            set
            {
                if (_mDisabled != value)
                {
                    if(value)
                    {
                        _mNormalStateForeground = Foreground;
                        Foreground = GetColorFromHexa("#AAAAAA");
                    }                    
                    _mDisabled = value;
                    if (!value)
                    {
                        Foreground = _mNormalStateForeground;
                    }
                }
            }
        }

        private Brush _mForeground;
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set 
            {
                if (_mForeground != value)
                {
                    if (!Disabled)
                    {
                        _mForeground = value;
                        OnPropertyChanged("Foreground");
                    }
                    if (Disabled)
                        _mNormalStateForeground = value;
                }
            }
        }

        public string DummyForeground
        {
            get
            {
                if (Foreground != null && Foreground is SolidColorBrush)
                    return (Foreground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Foreground = new SolidColorBrush(value.ToColor());
            }
        }

        private int _mSelectedIndex = 0;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set 
            { 
                _mSelectedIndex = value; 
            }
        }

        private Brush _mStroke;
        [XmlIgnore]
        public Brush Stroke
        {
            get { return _mStroke; }
            set 
            {
                if (_mStroke != value)
                {
                    _mStroke = value;
                    OnPropertyChanged("Stroke");
                }
            }
        }

        public string DummyStroke
        {
            get
            {
                if (Stroke != null && Stroke is SolidColorBrush)
                    return (Stroke as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Stroke = new SolidColorBrush(value.ToColor());
            }
        }

        private CornerRadius _mCornerRadius;
        public CornerRadius CornerRadius
        {
            get { return _mCornerRadius; }
            set 
            {
                if (_mCornerRadius != value)
                {
                    _mCornerRadius = value;
                    OnPropertyChanged("CornerRadius");
                }
            }
        }

        [XmlIgnore]
        public ObservableCollection<object> States { get; set; }

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

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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
    }    

    public class NumericStepperBusinessClass : PropertyChange, IState
    {
        public NumericStepperBusinessClass()
        {
            States = new ObservableCollection<object>()
            {
                "Normal",
                "Disabled"
            };
        }

        Brush _mStrokeOnSelectedState;
        Brush _mForegroundOnSelectedState;
        private bool _mSelected = true;
        public bool Selected
        {
            get { return _mSelected; }
            set 
            { 
                if(!value)
                {
                    _mStrokeOnSelectedState = Stroke;
                    _mForegroundOnSelectedState = Foreground;                   
                    Stroke = GetColorFromHexa("#AAAAAA");
                    Foreground = GetColorFromHexa("#AAAAAA");
                }
                _mSelected = value; 
                if(value)
                {
                    if (_mStrokeOnSelectedState != null)
                        Stroke = _mStrokeOnSelectedState;
                    if (_mForegroundOnSelectedState != null)
                        Foreground = _mForegroundOnSelectedState;
                }
            }
        }

        private Brush _mStroke = new SolidColorBrush(Colors.Black);
        [XmlIgnore]
        public Brush Stroke
        {
            get { return _mStroke; }
            set
            {
                if (_mStroke != value)
                {
                    if (Selected)
                    {
                        _mStroke = value;
                        OnPropertyChanged("Stroke");
                    }
                    else
                    {
                        if (value != GetColorFromHexa("#FFAAAAAA"))
                            _mStrokeOnSelectedState = value;
                    }
                }
            }
        }

        public string DummyStroke
        {
            get
            {
                if (Stroke != null && Stroke is SolidColorBrush)
                    return (Stroke as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Stroke = new SolidColorBrush(value.ToColor());
            }
        }

        private Brush _mForeground;
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set
            {
                if (_mForeground != value)
                {
                    if (Selected)
                    {
                        _mForeground = value;
                        OnPropertyChanged("Foreground");
                    }
                    else
                    {
                        if (value != GetColorFromHexa("#FFAAAAAA"))
                            _mForegroundOnSelectedState = value;
                    }
                }
            }
        }

        public string DummyForeground
        {
            get
            {
                if (Foreground != null && Foreground is SolidColorBrush)
                    return (Foreground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Foreground = new SolidColorBrush(value.ToColor());
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

        [XmlIgnore]
        public ObservableCollection<object> States { get; set; }
    }

    public class ButtonBarItemBusinessClass : PropertyChange, ICommonForCollectionItem
    {
        public ButtonBarItemBusinessClass()
        {
            Link = new HyperlinkBusinessClass();
            PointerEntered = new Command(OnPointerEnteredCommand);
        }

        private string _mmText;
        public string Item
        {
            get { return _mmText; }
            set
            {
                if (_mmText != value)
                {
                    _mmText = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        private bool _mSelected;
        public bool Selected
        {
            get { return _mSelected; }
            set
            {
                if(value)
                {
                    Fill = GetColorFromHexa("#83c1f0");
                }
                else
                {
                    Fill = new SolidColorBrush(Colors.White);
                }
                _mSelected = value;
                        
            }
        }

        private Brush _mFill;
        [XmlIgnore]
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
                    OnPropertyChanged("Fill");
                }
            }
        }

        private HyperlinkBusinessClass _mLink;
        [XmlIgnore]
        public HyperlinkBusinessClass Link
        {
            get { return _mLink; }
            set
            {
                _mLink = value;
            }
        }

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

        private double _mWidth;
        public double Width
        {
            get { return _mWidth; }
            set
            {
                if (_mWidth != value)
                {
                    _mWidth = value;
                    OnPropertyChanged("Width");
                }
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

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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

        [XmlIgnore]
        public ICommand PointerEntered
        {
            get;
            set;
        }

        public void OnPointerEnteredCommand(object param)
        {
        }
    }

    
    public class ButtonBarBusinessClass : PropertyChange, ICommonForCollection 
    {
        //private ObservableCollection<ButtonBarItemBusinessClass> _mItems;
        //public ObservableCollection<ButtonBarItemBusinessClass> Items
        //{
        //    get { return _mItems; }
        //    set
        //    {
        //        if (_mItems != value)
        //        {
        //            _mItems = value;
        //            OnPropertyChanged("Items");
        //        }
        //    }
        //}

        private ObservableCollection<object> _mItems;
        public ObservableCollection<object> Items
        {
            get { return _mItems; }
            set
            {
                if (_mItems != value)
                {
                    _mItems = value;
                    OnPropertyChanged("Items");
                }
            }
        }

        private int _mSelectedIndex = 1;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set
            {
                _mSelectedIndex = value;
                for (int x = 0; x < Items.Count; x++)
                {
                    if (_mSelectedIndex != 0 && x == _mSelectedIndex - 1)
                    {
                        (Items[x] as ButtonBarItemBusinessClass).Selected = true;
                    }
                    else
                        (Items[x] as ButtonBarItemBusinessClass).Selected = false;
                }
            }
        }

        private string _mExecutedPath = string.Empty;
        public string ExecutedPath
        {
            get { return _mExecutedPath; }
            set { _mExecutedPath = value; }
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
                    if(value)
                    {
                        foreach(var item in Items)
                        {
                            (item as ButtonBarItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                }
            }
        }
    }

    public class TabItemBusinessClass : PropertyChange, ICommonForCollectionItem
    {
        public TabItemBusinessClass()
        {
            Link = new HyperlinkBusinessClass();
            PointerEntered = new Command(OnPointerEnteredCommand);
        }

        private string _mmText;
        public string Item
        {
            get { return _mmText; }
            set
            {
                if (_mmText != value)
                {
                    _mmText = value;
                    OnPropertyChanged("Text");
                    Link.Title = value;
                }
            }
        }

        private bool _mSelected;
        public bool Selected
        {
            get { return _mSelected; }
            set
            {
                _mSelected = value;
                if ((bool)value)
                {
                    if (Orientation == Orientation.Horizontal)
                    {
                        if (IsLastItem)
                            BorderThickenss = new Thickness(1, 1, 1, 0);
                        else
                            BorderThickenss = new Thickness(1, 1, 0, 0);
                    }
                    else
                    {
                        if (IsLastItem)
                            BorderThickenss = new Thickness(1, 1, 0, 1);
                        else
                            BorderThickenss = new Thickness(1, 1, 0, 0);
                    }
                }
                else
                {
                    if (Orientation == Orientation.Horizontal)
                    {
                        if (IsLastItem)
                            BorderThickenss = new Thickness(1);
                        else
                            BorderThickenss = new Thickness(1, 1, 0, 1);
                    }
                    else
                    {
                        if (IsLastItem)
                            BorderThickenss = new Thickness(1);
                        else
                            BorderThickenss = new Thickness(1, 1, 1, 0);
                    }
                    Background = GetColorFromHexa("#E2E2E2");
                }
            }
        }

        private Thickness _mBorderThickenss = new Thickness(1);
        public Thickness BorderThickenss
        {
            get { return _mBorderThickenss; }
            set
            {
                //if (_mBorderThickenss != value)
                {
                    if (Orientation == Orientation.Horizontal)
                    {
                        if (HorizontalTabAlignment == VerticalAlignment.Top)
                        {
                            _mBorderThickenss = value;
                        }
                        else if (HorizontalTabAlignment == VerticalAlignment.Bottom)
                        {
                            Thickness temp = new Thickness(value.Left, value.Bottom, value.Right, value.Top);
                            _mBorderThickenss = temp;
                        }
                    }
                    else
                    {
                        if (VerticalTabAlignment == HorizontalAlignment.Left)
                        {
                            _mBorderThickenss = value;
                        }
                        else if (VerticalTabAlignment == HorizontalAlignment.Right)
                        {
                            Thickness temp = new Thickness(value.Right, value.Top, value.Left, value.Bottom);
                            _mBorderThickenss = temp;
                        }
                    }
                    OnPropertyChanged("BorderThickenss");
                }
            }
        }

        private Brush _mBackground = new SolidColorBrush(new Color() { A = 0xFF, R = 0xEF, G = 0xEF, B = 0xEF });
        [XmlIgnore]
        public Brush Background
        {
            get
            {
                return _mBackground;
            }
            set
            {
                if (_mBackground != value)
                {
                    if (_mSelected)
                        _mBackground = value;
                    else
                        _mBackground = GetColorFromHexa("#E2E2E2");
                    OnPropertyChanged("Background");
                }
            }
        }

        //public string DummyBackground
        //{
        //    get
        //    {
        //        if (Background != null && Background is SolidColorBrush)
        //            return (Background as SolidColorBrush).Color.ToString();
        //        else
        //            return null;
        //    }
        //    set
        //    {
        //        Background = new SolidColorBrush(value.ToColor());
        //    }
        //}

        public Orientation Orientation { get; set; }

        private VerticalAlignment _mHorizontalTabAlignment;

        public VerticalAlignment HorizontalTabAlignment
        {
            get { return _mHorizontalTabAlignment; }
            set 
            {
                //if (_mHorizontalTabAlignment != value)
                {
                    _mHorizontalTabAlignment = value;
                    BorderThickenss = new Thickness();
                    if (Selected)
                    {
                        if (IsLastItem)
                            BorderThickenss = new Thickness(1, 1, 1, 0);
                        else
                            BorderThickenss = new Thickness(1, 1, 0, 0);
                    }
                    else
                    {
                        if (IsLastItem)
                            BorderThickenss = new Thickness(1);
                        else
                            BorderThickenss = new Thickness(1, 1, 0, 1);
                    }
                }
            }
        }

        private HorizontalAlignment _mVerticalTabAlignment;

        public HorizontalAlignment VerticalTabAlignment
        {
            get { return _mVerticalTabAlignment; }
            set 
            {
                //if (_mVerticalTabAlignment != value)
                {
                    _mVerticalTabAlignment = value;
                    BorderThickenss = new Thickness();
                    if(Selected)
                    {
                        if (IsLastItem)
                            BorderThickenss = new Thickness(1, 1, 0, 1);
                        else
                            BorderThickenss = new Thickness(1, 1, 0, 0);
                    }
                    else
                    {
                        if (IsLastItem)
                            BorderThickenss = new Thickness(1);
                        else
                            BorderThickenss = new Thickness(1, 1, 1, 0);
                    }
                }
            }
        }
        
        //public HorizontalAlignment VerticalTabAlignment { get; set; }

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

        private bool _mIsLastItem;
        public bool IsLastItem
        {
            get { return _mIsLastItem; }
            set
            {
                _mIsLastItem = value;
                if (value)
                {
                    if (Orientation == Orientation.Horizontal)
                    {
                        if (Selected)
                            BorderThickenss = new Thickness(1, 1, 1, 0);
                        else
                            BorderThickenss = new Thickness(1);
                    }
                    else
                    {
                        if (Selected)
                            BorderThickenss = new Thickness(1, 1, 0, 1);
                        else
                            BorderThickenss = new Thickness(1);
                    }
                }
                else
                {
                    if (Orientation == Orientation.Horizontal)
                    {
                        if (Selected)
                            BorderThickenss = new Thickness(1, 1, 0, 0);
                        else
                            BorderThickenss = new Thickness(1, 1, 0, 1);
                    }
                    else
                    {
                        if (Selected)
                            BorderThickenss = new Thickness(1, 1, 0, 0);
                        else
                            BorderThickenss = new Thickness(1,1,1,0);
                    }
                }
            }
        }

        private HyperlinkBusinessClass _mLink;
        [XmlIgnore]
        public HyperlinkBusinessClass Link
        {
            get { return _mLink; }
            set
            {
                _mLink = value;
            }
        }

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

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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

        [XmlIgnore]
        public ICommand PointerEntered
        {
            get;
            set;
        }

        public void OnPointerEnteredCommand(object param)
        {
        }
    }

    public class TabBusinessClass : PropertyChange, ICommonForCollection
    {
        private Brush _mSelectedBackground;
        public TabBusinessClass() { }
        public TabBusinessClass(Orientation orientation)
        {
            Orientation = orientation;
            ScrollBar = new VerticalScrollBarBusinessClass();
            Items = new ObservableCollection<object>();
            //ItemMargin = new Thickness(10, 5, 10, 5);
            Items.CollectionChanged += Items_CollectionChanged;
        }

        //private ObservableCollection<TabItemBusinessClass> _mItems;
        //public ObservableCollection<TabItemBusinessClass> Items
        //{
        //    get { return _mItems; }
        //    set
        //    {
        //        if (_mItems != value)
        //        {

        //            _mItems = value;
        //            //_mItems.CollectionChanged += Items_CollectionChanged;
        //            OnPropertyChanged("Items");
        //        }
        //    }
        //}


        private ObservableCollection<object> _mItems;
        public ObservableCollection<object> Items
        {
            get { return _mItems; }
            set
            {
                if (_mItems != value)
                {

                    _mItems = value;
                    //_mItems.CollectionChanged += Items_CollectionChanged;
                    OnPropertyChanged("Items");
                }
            }
        }

        void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                (e.NewItems[0] as TabItemBusinessClass).Orientation = this.Orientation;
        
                if (this.Orientation == Orientation.Horizontal)
                    (e.NewItems[0] as TabItemBusinessClass).HorizontalTabAlignment = this.HorizontalTabAlignment;
                else
                    (e.NewItems[0] as TabItemBusinessClass).VerticalTabAlignment = this.VerticalTabAlignment;
            }
        }        

        private double _mOpacity = 1;
        public double Opacity
        {
            get { return _mOpacity; }
            set
            {
                if (_mOpacity != value)
                {
                    _mOpacity = value;
                    OnPropertyChanged("Opacity");
                }
            }
        }

        //private Thickness _mTabBorderThickness;
        //public Thickness TabBorderThickness
        //{
        //    get { return _mTabBorderThickness; }
        //    set
        //    {
        //        if (_mTabBorderThickness != value)
        //        {
        //            _mTabBorderThickness = value;
        //            OnPropertyChanged("TabBorderThickness");
        //        }
        //    }
        //}

        private Brush _mBackground = new SolidColorBrush(new Color() { A = 0xFF, R = 0xEF, G = 0xEF, B = 0xEF });
        [XmlIgnore]
        public Brush Background
        {
            get
            {
                return _mBackground;
            }
            set
            {
                if (_mBackground != value)
                {
                    _mBackground = value;
                    OnPropertyChanged("Background");
                    if (_mSelectedIndex != 0)
                        _mSelectedBackground = value;
                    foreach (var child in Items)
                    {
                        if ((child as TabItemBusinessClass).Selected)
                            (child as TabItemBusinessClass).Background = value;
                    }
                }
            }
        }

        public string DummyBackground
        {
            get
            {
                if (Background != null && Background is SolidColorBrush)
                    return (Background as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Background = new SolidColorBrush(value.ToColor());
            }
        }

        private VerticalScrollBarBusinessClass _mScrollBar;
        public VerticalScrollBarBusinessClass ScrollBar
        {
            get { return _mScrollBar; }
            set
            {
                if (_mScrollBar != value)
                {
                    _mScrollBar = value;
                    OnPropertyChanged("ScrollBar");
                }
            }
        }

        private HorizontalScrollBarBusinessClass _mHorizontalScrollBar;
        public HorizontalScrollBarBusinessClass HorizontalScrollBar
        {
            get { return _mHorizontalScrollBar; }
            set
            {
                if (_mHorizontalScrollBar != value)
                {
                    _mHorizontalScrollBar = value;
                    OnPropertyChanged("HorizontalScrollBar");
                }
            }
        }

        private Orientation _mOrientation;
        public Orientation Orientation
        {
            get { return _mOrientation; }
            set
            {
                if (_mOrientation != value)
                {
                    _mOrientation = value;
                    OnPropertyChanged("Orientation");
                }
            }
        }

        private VerticalAlignment _mHorizontalTabAlignment = VerticalAlignment.Top;
        public VerticalAlignment HorizontalTabAlignment
        {
            get { return _mHorizontalTabAlignment; }
            set 
            { 
                _mHorizontalTabAlignment = value;
                foreach (var item in Items)
                    (item as TabItemBusinessClass).HorizontalTabAlignment = value;
                if (value == VerticalAlignment.Top)
                    Top = true;
                else
                    Bottom = true;
            }
        }

        private HorizontalAlignment _mVerticalTabAlignment = HorizontalAlignment.Left;        
        public HorizontalAlignment VerticalTabAlignment
        {
            get { return _mVerticalTabAlignment; }
            set 
            {
                _mVerticalTabAlignment = value;
                foreach (var item in Items)
                    (item as TabItemBusinessClass).VerticalTabAlignment = value;
                if (value == HorizontalAlignment.Left)
                    Left = true;
                else
                    Right = true;
            }
        }
        
        private int _mSelectedIndex = 1;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set
            {
                _mSelectedIndex = value;

                if (_mSelectedIndex != 0)
                {
                    if (_mSelectedBackground == null)
                        _mSelectedBackground = Background;
                    else
                        Background = _mSelectedBackground;
                }

                for (int x = 0; x < Items.Count; x++)
                {
                    if (_mSelectedIndex == 0)
                    {
                        (Items[x] as TabItemBusinessClass).Selected = false;
                        this.Background = GetColorFromHexa("#E2E2E2");
                    }
                    else
                    {
                        if (_mSelectedIndex != 0 && x == _mSelectedIndex - 1)
                        {
                            (Items[x] as TabItemBusinessClass).Selected = true;
                            (Items[x] as TabItemBusinessClass).Background = Background;
                        }
                        else
                            (Items[x] as TabItemBusinessClass).Selected = false;
                    }
                }
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


        private string _mExecutedPath = string.Empty;
        public string ExecutedPath
        {
            get { return _mExecutedPath; }
            set { _mExecutedPath = value; }
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
                    foreach(var item in Items)
                    {
                        (item as TabItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                    }
                }
            }
        }


        private bool _mLeft;

        public bool Left
        {
            get { return _mLeft; }
            set
            {
                if (_mLeft != value)
                {
                    _mLeft = value;
                    OnPropertyChanged("Left");
                    if(value)
                    {
                        Right = false;
                        Top = false;
                        Bottom = false;
                    }
                }
            }
        }

        private bool _mRight;

        public bool Right   
        {
            get { return _mRight; }
            set
            {
                if (_mRight != value)
                {
                    _mRight = value;
                    OnPropertyChanged("Right");
                    if (value)
                    {
                        Left = false;
                        Top = false;
                        Bottom = false;
                    }
                }
            }
        }

        private bool _mTop;

        public bool Top
        {
            get { return _mTop; }
            set
            {
                if (_mTop != value)
                {
                    _mTop = value;
                    OnPropertyChanged("Top");
                    if (value)
                    {
                        Left = false;
                        Right = false;
                        Bottom = false;
                    }
                }
            }
        }


        private bool _mBottom;

        public bool Bottom
        {
            get { return _mBottom; }
            set
            {
                if (_mBottom != value)
                {
                    _mBottom = value;
                    OnPropertyChanged("Bottom");
                    if (value)
                    {
                        Left = false;
                        Right = false;
                        Top = false;
                    }
                }
            }
        }
    }

    public class SliderBusinessClass : PropertyChange, INotifyPropertyChanged, IState 
    {
        public SliderBusinessClass() 
        {
            States = new ObservableCollection<object>()
            {
                "Normal",
                "Disabled"
            };
        }

        bool _mStrokeChangedFromDisabeldState;        
        Brush _mSelectedStateStroke;

        public string DummyStroke
        {
            get
            {
                if (Stroke != null && Stroke is SolidColorBrush)
                    return (Stroke as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Stroke = new SolidColorBrush(value.ToColor());
            }
        }

        private Brush _mStroke = new SolidColorBrush(Colors.Black);
        [XmlIgnore]
        public Brush Stroke
        {
            get { return _mStroke; }
            set 
            {
                if (_mStroke != value)
                {
                    if (Selected || _mStrokeChangedFromDisabeldState)
                    {
                        _mStroke = value;
                        OnPropertyChanged("Stroke");
                    }
                    if (!_mStrokeChangedFromDisabeldState)
                        _mSelectedStateStroke = value;
                }
            }
        }        

        private double _mSliderValue = 0;
        public double SliderValue
        {
            get { return _mSliderValue; }
            set
            {
                if (_mSliderValue != value)
                {
                    _mSliderValue = value;
                    OnPropertyChanged("SliderValue");
                }
            }
        }

        private Thickness _mScrollMargin = new Thickness(0);
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

        private bool _mSelected = true;
        public bool Selected
        {
            get { return _mSelected; }
            set
            {
                if (_mSelected != value)
                {
                    _mStrokeChangedFromDisabeldState = true;
                    if (value)
                    {
                        if (_mSelectedStateStroke != null)
                            Stroke = _mSelectedStateStroke;
                    }
                    else
                    {                        
                        if (Stroke != null)
                            _mSelectedStateStroke = Stroke;
                        Stroke = GetColorFromHexa("#AAAAAA");
                    }

                    _mSelected = value;
                    OnPropertyChanged("Selected");

                    _mStrokeChangedFromDisabeldState = false;

                }
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

        [XmlIgnore]
        public ObservableCollection<object> States { get; set; }

        
        private int _mSliderBallSize = 12;
        [XmlIgnore]
        public int SliderBallSize
        {
            get { return _mSliderBallSize; }
        }
        
    }


    public class HorizontalScrollBarBusinessClass : SliderBusinessClass
    {
        public HorizontalScrollBarBusinessClass() { }

        private double _mScrollThumbWidth = 35;
        public double ScrollThumbWidth
        {
            get { return _mScrollThumbWidth; }
            set
            {
                if (_mScrollThumbWidth != value)
                {
                    _mScrollThumbWidth = value;
                    OnPropertyChanged("ScrollThumbWidth");
                }
            }
        }

        private double _mLeftButtonSize = 20;
        public double LeftButtonSize
        {
            get { return _mLeftButtonSize; }
            set
            {
                if (_mLeftButtonSize != value)
                {
                    _mLeftButtonSize = value;
                    OnPropertyChanged("LeftButtonSize");
                }
            }
        }

        private double _mRightButtonSize = 20;
        public double RightButtonSize
        {
            get { return _mRightButtonSize; }
            set
            {
                if (_mRightButtonSize != value)
                {
                    _mRightButtonSize = value;
                    OnPropertyChanged("RightButtonSize");
                }
            }
        }

        // Browser Window and Tab
        private bool _mEnabled = false;
        public bool Enabled
        {
            get { return _mEnabled; }
            set
            {
                if (_mEnabled != value)
                {
                    _mEnabled = value;
                    OnPropertyChanged("Enabled");
                }
            }
        }
    }

    public class VerticalScrollBarBusinessClass : SliderBusinessClass
    {
        public VerticalScrollBarBusinessClass() { }

        private double _mScrollThumbHeight = 35;
        public double ScrollThumbHeight
        {
            get { return _mScrollThumbHeight; }
            set
            {
                if (_mScrollThumbHeight != value)
                {
                    _mScrollThumbHeight = value;
                    OnPropertyChanged("ScrollThumbHeight");
                }
            }
        }

        private double _mTopButtonSize = 20;
        public double TopButtonSize
        {
            get { return _mTopButtonSize; }
            set
            {
                if (_mTopButtonSize != value)
                {
                    _mTopButtonSize = value;
                    OnPropertyChanged("TopButtonSize");
                }
            }
        }

        private double _mBottomButtonSize = 20;
        public double BottomButtonSize
        {
            get { return _mBottomButtonSize; }
            set
            {
                if (_mBottomButtonSize != value)
                {
                    _mBottomButtonSize = value;
                    OnPropertyChanged("BottomButtonSize");
                }
            }
        }

        // Browser Window and Tab
        private bool _mEnabled = false;
        public bool Enabled
        {
            get { return _mEnabled; }
            set
            {
                if (_mEnabled != value)
                {
                    _mEnabled = value;
                    OnPropertyChanged("Enabled");
                }
            }
        }

    }

    public class VolumeSliderBusinessClass : SliderBusinessClass
    {
        public VolumeSliderBusinessClass()
        {
            ScrollMargin = new Thickness(30, 0, 0, 0);
        }

        private double _mVolumeSymbolWidth = 35;
        public double VolumeSymbolWidth
        {
            get { return _mVolumeSymbolWidth; }
            set
            {
                if (_mVolumeSymbolWidth != value)
                {
                    _mVolumeSymbolWidth = value;
                    OnPropertyChanged("VolumeSymbolWidth");
                }
            }
        }
    }

    public class PointyButtonBusinessClass : PropertyChange, IState
    {
        ResourceDictionary resourceDictionary = null;
        public PointyButtonBusinessClass()
        {
            resourceDictionary=  new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Showcase/Mockup/Theme/MockupUI.xaml", UriKind.Absolute)
            };
            States = new ObservableCollection<object>()
            {
                "Normal", 
                "Disabled"
            };
        }

        private bool _mDisabled;
        public bool Disabled
        {
            get { return _mDisabled; }
            set { _mDisabled = value; }
        }

        private bool _mMenuIconEnabled = false;
        public bool MenuIconEnabled
        {
            get { return _mMenuIconEnabled; }
            set
            {
                if (_mMenuIconEnabled != value)
                {
                    _mMenuIconEnabled = value;
                    OnPropertyChanged("MenuIconEnabled");
                }
            }
        }

        private Thickness _mMenuIconMargin = new Thickness(5);
        [XmlIgnore]
        public Thickness MenuIconMargin
        {
            get { return _mMenuIconMargin; }
            private set
            {
                if (_mMenuIconMargin != value)
                {
                    _mMenuIconMargin = value;
                    OnPropertyChanged("MenuIconMargin");
                }
            }
        }

        private double _mMenuIconSize;
        public double MenuIconSize
        {
            get { return _mMenuIconSize; }
            set
            {
                if (_mMenuIconSize != value)
                {
                    _mMenuIconSize = value;
                    OnPropertyChanged("MenuIconSize");
                }
            }
        }

        [XmlIgnore]
        public ObservableCollection<object> States { get; set; }

        private DataTemplate _mTemplate;
        [XmlIgnore]
        public DataTemplate Template
        {
            get { return _mTemplate; }
            set
            {
                if (_mTemplate != value)
                {
                    _mTemplate = value;
                    OnPropertyChanged("Template");
                }
            }
        }

        private bool _mLeft = false;
        public bool Left
        {
            get { return _mLeft; }
            set
            {
                if (_mLeft != value)
                {
                    _mLeft = value;
                    OnPropertyChanged("Left");
                    if (value)
                    {
                        Template = resourceDictionary["leftpointybutton"] as DataTemplate;
                        Right = false;
                        Center = false;
                    }
                }
            }
        }

        private bool _mRight = false;
        public bool Right
        {
            get { return _mRight; }
            set
            {
                if (_mRight != value)
                {
                    _mRight = value;
                    OnPropertyChanged("Right");
                    if (value)
                    {
                        Template = resourceDictionary["rightpointybutton"] as DataTemplate;
                        Left = false;
                        Center = false;
                    }
                }
            }
        }

        private bool _mCenter = false;
        public bool Center
        {
            get { return _mCenter; }
            set
            {
                if (_mCenter != value)
                {
                    _mCenter = value;
                    OnPropertyChanged("Center");
                    if (value)
                    {
                        Template = resourceDictionary["centerpointybutton"] as DataTemplate;
                        Left = false;
                        Right = false;
                    }
                }
            }
        }
    }

    public class LinkBusinessClass : PropertyChange, IState
    {
        public LinkBusinessClass()
        {
            States = new ObservableCollection<object>()
            {
                "Normal", 
                "Disabled"
            };
        }

        private int _mSelectedIndex = 0;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set { _mSelectedIndex = value; }
        }

        [XmlIgnore]
        public ObservableCollection<object> States { get; set; }
    }

    public class BreadcrumbsItemBusinessClass : PropertyChange, ICommonForCollectionItem
    {
        public BreadcrumbsItemBusinessClass()
        {
            Link = new HyperlinkBusinessClass();
            PointerEntered = new Command(OnPointerEnteredCommand);
        }

        private string _mItem;
        public string Item
        {
            get { return _mItem; }
            set
            {
                if (_mItem != value)
                {
                    _mItem = value;
                    OnPropertyChanged("Item");
                }
            }
        }

        private Brush _mForeground = new SolidColorBrush(Colors.Blue);
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set 
            {
                if (_mForeground != value)
                {
                    _mForeground = value;
                    OnPropertyChanged("Foreground");
                }
            }
        }

        public string ForegroundDummy
        {
            get
            {
                if (Foreground != null && Foreground is SolidColorBrush)
                    return (Foreground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Foreground = new SolidColorBrush(value.ToColor());
            }
        }

        private bool _mEnabledUnderLine = false;
        public bool IsLastItem
        {
            get { return _mEnabledUnderLine; }
            set
            {
                if (_mEnabledUnderLine != value)
                {
                    _mEnabledUnderLine = value;
                    OnPropertyChanged("EnabledUnderLine");
                    if (value)
                        Foreground = new SolidColorBrush(Colors.Black);
                    else
                        Foreground = new SolidColorBrush(Colors.Blue);
                }
            }
        }       
        
        private HyperlinkBusinessClass _mLink;
        [XmlIgnore]
        public HyperlinkBusinessClass Link
        {
            get { return _mLink; }
            set
            {
                _mLink = value;
            }
        }

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

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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

        [XmlIgnore]
        public ICommand PointerEntered
        {
            get;
            set;
        }

        public void OnPointerEnteredCommand(object param)
        {
        }
    }

    public class BreadcrumbsBusinessClass : PropertyChange, ICommonForCollection
    {
        //private ObservableCollection<BreadcrumbsItemBusinessClass> _mItems;
        //public ObservableCollection<BreadcrumbsItemBusinessClass> Items
        //{
        //    get { return _mItems; }
        //    set
        //    {
        //        _mItems = value;
        //        OnPropertyChanged("Items");
        //    }
        //}

        private ObservableCollection<object> _mItems;
        public ObservableCollection<object> Items
        {
            get { return _mItems; }
            set
            {
                _mItems = value;
                OnPropertyChanged("Items");
            }
        }

        private double _mArrowIconWidth = 10;
        [XmlIgnore]
        public double ArrowIconWidth
        {
            get { return _mArrowIconWidth; }
            private set
            {
                if (_mArrowIconWidth != value)
                {
                    _mArrowIconWidth = value;
                    OnPropertyChanged("ArrowIconWidth");
                }
            }
        }

        private double _mArrowIconHeight = 10;
        [XmlIgnore]
        public double ArrowIconHeight
        {
            get { return _mArrowIconHeight; }
            private set
            {
                if (_mArrowIconHeight != value)
                {
                    _mArrowIconHeight = value;
                    OnPropertyChanged("ArrowIconHeight");
                }
            }
        }

        private string _mExecutedPath = string.Empty;
        public string ExecutedPath
        {
            get { return _mExecutedPath; }
            set { _mExecutedPath = value; }
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
                        foreach (var item in Items)
                        {
                            (item as BreadcrumbsItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                }
            }
        }
    }

    public class ListItemBusinessClass : PropertyChange, ICommonForCollectionItem
    {
        public ListItemBusinessClass()
        {
            Link = new HyperlinkBusinessClass();
            PointerEntered = new Command(OnPointerEnteredCommand);
        }

        private string _mItem;
        public string Item
        {
            get { return _mItem; }
            set
            {
                if (_mItem != value)
                {
                    _mItem = value;
                    OnPropertyChanged("Item");
                }
            }
        }

        private Brush _mBackground;
        [XmlIgnore]
        public Brush Background
        {
            get { return _mBackground; }
            set
            {
                if (_mBackground != value)
                {
                    _mBackground = value;
                    OnPropertyChanged("Background");
                }
            }
        }

        public string BackgroundDummy
        {
            get
            {
                if (Background != null && Background is SolidColorBrush)
                    return (Background as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Background = new SolidColorBrush(value.ToColor());
            }
        }

        private HyperlinkBusinessClass _mLink;
        [XmlIgnore]
        public HyperlinkBusinessClass Link
        {
            get { return _mLink; }
            set
            {
                _mLink = value;
            }
        }
        
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

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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

        [XmlIgnore]
        public ICommand PointerEntered
        {
            get;
            set;
        }

        public void OnPointerEnteredCommand(object param)
        {
        }
    }

    public class ListBusinessClass : PropertyChange, ICommonForCollection
    {
        public ListBusinessClass()
        {
            ScrollBar = new VerticalScrollBarBusinessClass();
        }

        private ObservableCollection<object> _mItems;
        public ObservableCollection<object> Items
        {
            get { return _mItems; }
            set
            {
                if (_mItems != value)
                {
                    _mItems = value;
                    OnPropertyChanged("Items");
                }
            }
        }

        private ObservableCollection<object> _mDuplicateItems;
        public ObservableCollection<object> DuplicateItems
        {
            get { return _mDuplicateItems; }
            set
            {
                if (_mDuplicateItems != value)
                {
                    _mDuplicateItems = value;
                    OnPropertyChanged("DuplicateItems");
                }
            }
        }

        private int _mSelectedIndex = 0;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set
            {
                if (Items != null && Items.Count > 0)
                {
                    _mSelectedIndex = value;
                    for (int i = 0; i < Items.Count; i++)
                    {
                        if (i == value)
                        {
                            (DuplicateItems[i] as ListItemBusinessClass).Background = GetColorFromHexa("#83c1f0");
                        }
                        else
                        {
                            if (i % 2 == 0)
                            {
                                (DuplicateItems[i] as ListItemBusinessClass).Background = EvenItemsBackgroud;
                            }
                            else
                            {
                                (DuplicateItems[i] as ListItemBusinessClass).Background = OddItemsBackgroud;
                            }
                        }
                    }
                }
            }
        }

        private double _mRowHeight;
        public double RowHeight
        {
            get { return _mRowHeight; }
            set
            {
                if (_mRowHeight != value)
                {
                    _mRowHeight = value;
                    OnPropertyChanged("RowHeight");
                }
            }
        }

        private Brush _mOddItemsBackgroud;
        [XmlIgnore]
        public Brush OddItemsBackgroud
        {
            get { return _mOddItemsBackgroud; }
            set
            {
                if (_mOddItemsBackgroud != value)
                {
                    _mOddItemsBackgroud = value;
                    OnPropertyChanged("OddItemsBackgroud");

                    int x = 0;
                    foreach (var item in DuplicateItems)
                    {
                        if (x % 2 != 0 && x != SelectedIndex)
                        {
                            (item as ListItemBusinessClass).Background = value;
                        }
                        x++;
                    }
                }
            }
        }

        public string OddItemsBackgroudDummy
        {
            get
            {
                if (OddItemsBackgroud != null && OddItemsBackgroud is SolidColorBrush)
                    return (OddItemsBackgroud as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                OddItemsBackgroud = new SolidColorBrush(value.ToColor());
            }
        }

        private Brush _mEvenItemsBackgroud;
        [XmlIgnore]
        public Brush EvenItemsBackgroud
        {
            get { return _mEvenItemsBackgroud; }
            set
            {
                if (_mEvenItemsBackgroud != value)
                {
                    _mEvenItemsBackgroud = value;
                    OnPropertyChanged("EvenItemsBackgroud");

                    //for(int i = 0; i < Items.Count;i++)
                    //{
                    //    if(i%2==0 && i!=SelectedIndex)
                    //}
                    int x = 0;
                    foreach (var item in DuplicateItems)
                    {
                        if (x % 2 == 0 && x != SelectedIndex)
                        {
                            (item as ListItemBusinessClass).Background = value;
                        }
                        x++;
                    }
                }
            }
        }

        public string EvenItemsBackgroudDummy
        {
            get
            {
                if (EvenItemsBackgroud != null && EvenItemsBackgroud is SolidColorBrush)
                    return (EvenItemsBackgroud as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                EvenItemsBackgroud = new SolidColorBrush(value.ToColor());
            }
        }

        private bool _mShowBorder = true;
        public bool ShowBorder
        {
            get { return _mShowBorder; }
            set
            {
                if (_mShowBorder != value)
                {
                    _mShowBorder = value;
                    OnPropertyChanged("ShowBorder");
                }
            }
        }

        private VerticalScrollBarBusinessClass _mScrollBar;
        public VerticalScrollBarBusinessClass ScrollBar
        {
            get { return _mScrollBar; }
            set
            {
                if (_mScrollBar != value)
                {
                    _mScrollBar = value;
                    OnPropertyChanged("ScrollBar");
                }
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

        private string _mExecutedPath = string.Empty;
        public string ExecutedPath
        {
            get { return _mExecutedPath; }
            set { _mExecutedPath = value; }
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
                        foreach (var item in Items)
                        {
                            (item as ListItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                }
            }
        }
    }

    public class MenuTitleBusinessClass : PropertyChange, ICommonForCollectionItem
    {
        public MenuTitleBusinessClass()
        {
            Link = new HyperlinkBusinessClass();
            PointerEntered = new Command(OnPointerEnteredCommand);
        }

        private string _mItem;
        public string Item
        {
            get { return _mItem; }
            set
            {
                if (_mItem != value)
                {
                    _mItem = value;
                    OnPropertyChanged("Item");
                }
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

        private bool _mSelected = false;
        public bool Selected
        {
            get { return _mSelected; }
            set
            {
                _mSelected = value;
                if (_mSelected)
                {
                    Background = GetColorFromHexa("#83c1f0");
                }
                else
                {
                    Background = new SolidColorBrush(Colors.White);
                }
            }
        }

        private Brush _mBackground = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
        public Brush Background
        {
            get { return _mBackground; }
            set
            {
                if (_mBackground != value)
                {
                    _mBackground = value;
                    OnPropertyChanged("Background");
                }
            }
        }

       

        private HyperlinkBusinessClass _mLink;
        [XmlIgnore]
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

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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

        [XmlIgnore]
        public ICommand PointerEntered
        {
            get;
            set;
        }

        public void OnPointerEnteredCommand(object param)
        {
        }
    }

    public class MenuBarBusinessClass : PropertyChange, ICommonForCollection
    {
        //private ObservableCollection<MenuTitleBusinessClass> _mItems;
        //public ObservableCollection<MenuTitleBusinessClass> Items
        //{
        //    get { return _mItems; }
        //    set
        //    {
        //        if (_mItems != value)
        //        {
        //            _mItems = value;
        //            OnPropertyChanged("Items");
        //        }
        //    }
        //}

        private ObservableCollection<object> _mItems;        
        public ObservableCollection<object> Items
        {
            get { return _mItems; }
            set
            {
                if (_mItems != value)
                {
                    _mItems = value;
                    OnPropertyChanged("Items");
                }
            }
        }

        private bool _mShowBorder = true;
        public bool ShowBorder
        {
            get { return _mShowBorder; }
            set
            {
                if (_mShowBorder != value)
                {
                    _mShowBorder = value;
                    OnPropertyChanged("ShowBorder");
                }
            }
        }

        private int _mSelectedIndex = 0;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set
            {
                _mSelectedIndex = value;
                for (int i = 0; i < Items.Count; i++)
                {
                    if (i == value - 1)
                    {
                        (Items[i] as MenuTitleBusinessClass).Selected = true;
                    }
                    else
                        (Items[i] as MenuTitleBusinessClass).Selected = false;
                }
            }
        }

        private string _mExecutedPath = string.Empty;
        public string ExecutedPath
        {
            get { return _mExecutedPath; }
            set { _mExecutedPath = value; }
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
                        foreach (var item in Items)
                        {
                            (item as MenuTitleBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                }
            }
        }
    }

    public class MenuItemBusinessClass : PropertyChange, ICommonForCollectionItem
    {
        public MenuItemBusinessClass()
        {
            Link = new HyperlinkBusinessClass();
            PointerEntered = new Command(OnPointerEnteredCommand);
        }

        private string _mItem;
        public string Item
        {
            get { return _mItem; }
            set
            {
                if (_mItem != value)
                {
                    EditModeItem = value;
                    _mItem = TextVerify(value);
                    OnPropertyChanged("Item");
                }
            }
        }

        internal string EditModeItem { get; private set; }

        private readonly char[] _mCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        private string TextVerify(string text)
        {
            int indexOf = text.IndexOfAny(_mCharacters);
            //Foreground = new SolidColorBrush(Colors.Black);
            if (text.Contains('o') && Char.IsLower(text[0]))
            {
                ShowIcon = Visibility.Visible;
                text = text.Remove(0, 1);
            }
            else if (text.Contains('x') && Char.IsLower(text[0]))
            {
                ShowMarker = Visibility.Visible;
                text = text.Remove(0, 1);
            }
            else if (text.Contains('-') && indexOf != -1)
            {
                int index = text.IndexOf('-');
                if (index == 0)
                {
                    text = text.Remove(0, 1);
                    Disabled = true;
                    //Foreground = new SolidColorBrush(Colors.Red);
                }
            }
            return text;
        }

        private string _mShortcutKey;
        public string ShortcutKey
        {
            get { return _mShortcutKey; }
            set
            {
                if (_mShortcutKey != value)
                {
                    _mShortcutKey = value;
                    OnPropertyChanged("ShortcutKey");
                }
            }
        }

        private Visibility _mHyperLinkVisibility = Visibility.Collapsed;
        public Visibility HyperLinkVisibility
        {
            get { return _mHyperLinkVisibility; }
            set
            {
                if (_mHyperLinkVisibility != value)
                {
                    _mHyperLinkVisibility = value;
                    OnPropertyChanged("HyperLinkVisibility");
                }
            }
        }

        private Brush _mBackground = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
        public Brush Background
        {
            get { return _mBackground; }
            set
            {
                if (_mBackground != value)
                {
                    _mBackground = value;
                    OnPropertyChanged("Background");
                }
            }
        }

        private Brush _mForeground = new SolidColorBrush(Colors.Black);
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set
            {
                if (_mForeground != value)
                {
                    if (!Disabled)
                        _mForeground = value;
                    OnPropertyChanged("Foreground");
                }
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

        private bool _mIsSelected = false;
        public bool IsSelected
        {
            get { return _mIsSelected; }
            set
            {
                _mIsSelected = value;
                if (_mIsSelected)
                {
                    Background = GetColorFromHexa("#83c1f0");
                }
                else
                {
                    Background = new SolidColorBrush(Colors.White);
                }
            }
        }

        private bool _mDisabled;
        public bool Disabled
        {
            get { return _mDisabled; }
            set
            {
                if (value)
                    Foreground = GetColorFromHexa("#AAAAAA");
                else
                    Foreground = new SolidColorBrush(Colors.Black);
                _mDisabled = value;
            }
        }

        private Visibility _mShowMarker = Visibility.Collapsed;
        public Visibility ShowMarker
        {
            get { return _mShowMarker; }
            set
            {
                if (_mShowMarker != value)
                {
                    _mShowMarker = value;
                    OnPropertyChanged("ShowMarker");
                }
            }
        }

        private Visibility _mBreakLine = Visibility.Collapsed;
        public Visibility BreakLine
        {
            get { return _mBreakLine; }
            set
            {
                if (_mBreakLine != value)
                {
                    _mBreakLine = value;
                    OnPropertyChanged("BreakLine");
                    if (_mBreakLine == Visibility.Visible)
                    {
                        ShowMarker = Visibility.Collapsed;
                        ShowIcon = Visibility.Collapsed;
                        string temp = Item;
                        Item = string.Empty;
                        EditModeItem = temp;
                    }
                }
            }
        }

        private Visibility _mShowIcon = Visibility.Collapsed;
        public Visibility ShowIcon
        {
            get { return _mShowIcon; }
            set
            {
                if (_mShowIcon != value)
                {
                    _mShowIcon = value;
                    OnPropertyChanged("ShowIcon");
                }
            }
        }

        private HyperlinkBusinessClass _mLink;
        [XmlIgnore]
        public HyperlinkBusinessClass Link
        {
            get { return _mLink; }
            set
            {
                _mLink = value;
            }
        }
        
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

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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

        [XmlIgnore]
        public ICommand PointerEntered
        {
            get;
            set;
        }

        public void OnPointerEnteredCommand(object param)
        {
        }
    }

    public class MenuBusinessClass : PropertyChange, ICommonForCollection
    {
        //private ObservableCollection<MenuItemBusinessClass> _mItems;
        //public ObservableCollection<MenuItemBusinessClass> Items
        //{
        //    get { return _mItems; }
        //    set
        //    {
        //        if (_mItems != value)
        //        {
        //            _mItems = value;
        //            OnPropertyChanged("Items");
        //        }
        //    }
        //}

        private ObservableCollection<object> _mItems;
        public ObservableCollection<object> Items
        {
            get { return _mItems; }
            set
            {
                if (_mItems != value)
                {
                    _mItems = value;
                    OnPropertyChanged("Items");
                }
            }
        }

        private int _mSelectedIndex = 0;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set
            {
                _mSelectedIndex = value;
                for (int i = 0; i < Items.Count; i++)
                {
                    if (i == value - 1)
                    {
                        (Items[i] as MenuItemBusinessClass).IsSelected = true;
                    }
                    else
                        (Items[i] as MenuItemBusinessClass).IsSelected = false;
                }
            }
        }

        private string _mExecutedPath = string.Empty;
        public string ExecutedPath
        {
            get { return _mExecutedPath; }
            set { _mExecutedPath = value; }
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
                        foreach (var item in Items)
                        {
                            (item as MenuItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                }
            }
        }
    }

    public class LabelBusinessClass : DiagramElementViewModel, IState
    {
        public LabelBusinessClass()
        {
            Link = new HyperlinkBusinessClass() {Title = "Link" };
            States = new ObservableCollection<object>()
            {
                "Normal", 
                "Disabled"
            };
        }

        private int _mOrientation;
        public int Orientation
        {
            get { return _mOrientation; }
            set
            {
                if (_mOrientation != value)
                {
                    _mOrientation = value;
                    OnPropertyChanged("Orientation");
                }
            }
        }

        //private ObservableCollection<CheckBoxState> _mStates;
        //public ObservableCollection<CheckBoxState> States
        //{
        //    get { return _mStates; }
        //    set
        //    {
        //        if (_mStates != value)
        //        {
        //            _mStates = value;
        //            OnPropertyChanged("States");
        //        }
        //    }
        //}

        private string _mSelectedState;
        public string SelectedState
        {
            get { return _mSelectedState; }
            set
            {
                if (_mSelectedState != value)
                {
                    _mSelectedState = value;
                    OnPropertyChanged("SelectedState");
                }
            }
        }

        private Brush _mColorOnSelectedState;
        public Brush ColorOnSelectedState
        {
            get { return _mColorOnSelectedState; }
            set
            {
                if (_mColorOnSelectedState != value)
                {
                    _mColorOnSelectedState = value;
                }
            }
        }

        private HyperlinkBusinessClass _mLink;
        public HyperlinkBusinessClass Link
        {
            get { return _mLink; }
            set { _mLink = value; }
        }

        public ObservableCollection<object> States { get; set; }
    }

    public class TextInputBusinessClass : DiagramElementViewModel
    {
        private Brush _mFillOnNormalState;
        private Brush _mStrokeOnNormalState;

        private Brush _mFill = new SolidColorBrush(Colors.Transparent);
        public Brush Fill
        {
            get { return _mFill; }
            set
            {
                if (_mFill != value)
                {
                    _mFill = value;
                    OnPropertyChanged("Fill");
                }
            }
        }

        private Brush _mStroke;
        public Brush Stroke
        {
            get { return _mStroke; }
            set
            {
                if (_mStroke != value)
                {
                    if (!Disabled && !Focused)
                    {
                        _mStroke = value;
                    }
                    OnPropertyChanged("Stroke");
                }
            }
        }

        private double _mStrokeThickness;
        public double StrokeThickness
        {
            get { return _mStrokeThickness; }
            set
            {
                if (_mStrokeThickness != value)
                {
                    _mStrokeThickness = value;
                    OnPropertyChanged("StrokeThickness");
                }
            }
        }

        private bool _mFocused;
        public bool Focused
        {
            get { return _mFocused; }
            set
            {
                //if (_mFocused != value)
                {
                    if (value)
                    {
                        if (!Disabled)
                        {
                            _mFillOnNormalState = Fill;
                            _mStrokeOnNormalState = Stroke;
                        }
                        Disabled = false;
                        Fill = new SolidColorBrush(Colors.Blue);
                        Stroke = new SolidColorBrush(Colors.LightBlue);
                    }

                    _mFocused = value;

                    if (!value)
                    {
                        if (_mFillOnNormalState != null)
                            Fill = _mFillOnNormalState;
                        if (_mStrokeOnNormalState != null)
                            Stroke = _mStrokeOnNormalState;
                    }
                }
            }
        }

        private bool _mDisabled;
        public bool Disabled
        {
            get { return _mDisabled; }
            set
            {
                if (value)
                {
                    if (!Focused)
                    {
                        _mFillOnNormalState = Fill;
                        _mStrokeOnNormalState = Stroke;
                    }
                    Focused = false;
                    Fill = new SolidColorBrush(Colors.Red);
                    Stroke = new SolidColorBrush(Colors.LightBlue);
                }
                _mDisabled = value;
                if (!value)
                {
                    if (_mFillOnNormalState != null)
                        Fill = _mFillOnNormalState;
                    if (_mStrokeOnNormalState != null)
                        Stroke = _mStrokeOnNormalState;
                }
            }
        }
    }

    public class TextAreaBusinessClass : PropertyChange, IState
    {
        private Brush _mStrokeOnNormalState;
        private bool _mStrokeChangedBySelectedIndex;

        public TextAreaBusinessClass()
        {
            ScrollBar = new VerticalScrollBarBusinessClass();
            States = new ObservableCollection<object>()
            {
                "Normal", 
                "InFocus",
                "Disabled"
            };
        }

        private int _mSelectedIndex;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set 
            {
                _mStrokeChangedBySelectedIndex = true;
                if (value == 0)
                {                    
                    if(_mStrokeOnNormalState != null)
                    {
                        Stroke = _mStrokeOnNormalState;
                    }
                    Foreground = new SolidColorBrush(Colors.Black);
                }
                else if(value == 1)
                {
                    if (_mSelectedIndex != 2)
                        _mStrokeOnNormalState = Stroke;

                    Stroke = GetColorFromHexa("#83c1f0");
                    Foreground = new SolidColorBrush(Colors.Black);
                }
                else if(value == 2)
                {
                    if (_mSelectedIndex != 1)
                        _mStrokeOnNormalState = Stroke;
                    
                    Stroke = GetColorFromHexa("#AAAAAA");
                    Foreground = GetColorFromHexa("#AAAAAA");
                }
                _mStrokeChangedBySelectedIndex = false;
                _mSelectedIndex = value; 
            }
        }

        private Brush _mStroke;
        [XmlIgnore]
        public Brush Stroke
        {
            get { return _mStroke; }
            set 
            {
                if (_mStroke != value)
                {
                    if(SelectedIndex == 0 || _mStrokeChangedBySelectedIndex)
                    {
                        _mStroke = value;
                        OnPropertyChanged("Stroke");
                    }
                    else
                        _mStrokeOnNormalState = value;
                }
            }
        }

        public string StrokeDummy
        {
            get
            {
                if (Stroke != null && Stroke is SolidColorBrush)
                    return (Stroke as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Stroke = new SolidColorBrush(value.ToColor());
            }
        }

        private Brush _mForeground = new SolidColorBrush(Colors.Black);
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set
            {
                if (_mForeground != value)
                {
                    _mForeground = value;
                    OnPropertyChanged("Foreground");
                }
            }
        }

        public string ForegroundDummy
        {
            get
            {
                if (Foreground != null && Foreground is SolidColorBrush)
                    return (Foreground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Foreground = new SolidColorBrush(value.ToColor());
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

        private VerticalScrollBarBusinessClass _mScrollBar;
        public VerticalScrollBarBusinessClass ScrollBar
        {
            get { return _mScrollBar; }
            set
            {
                if (_mScrollBar != value)
                {
                    _mScrollBar = value;
                    OnPropertyChanged("ScrollBar");
                }
            }
        }

        [XmlIgnore]
        public ObservableCollection<object> States { get; set; }
    }

    public class LinkBarBusinessClass : PropertyChange, ICommonForCollection
    {
        //private ObservableCollection<LinkBarItemBusinessClass> _mItems;
        //public ObservableCollection<LinkBarItemBusinessClass> Items
        //{
        //    get { return _mItems; }
        //    set
        //    {
        //        if (_mItems != value)
        //        {
        //            _mItems = value;
        //            OnPropertyChanged("Items");
        //        }
        //    }
        //}

        private ObservableCollection<object> _mItems;
        public ObservableCollection<object> Items
        {
            get { return _mItems; }
            set
            {
                if (_mItems != value)
                {
                    _mItems = value;
                    OnPropertyChanged("Items");
                }
            }
        }

        private int _mSelectedIndex;
        public int SelectedIndex
        {
            get { return _mSelectedIndex; }
            set 
            {
                    _mSelectedIndex = value;
                    for (int i = 0; i < Items.Count(); i++ )
                    {
                        if(value==0)
                        {
                            (Items[i] as LinkBarItemBusinessClass).Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {
                            if (value - 1 == i)
                            {
                                (Items[i] as LinkBarItemBusinessClass).Foreground = GetColorFromHexa("#83c1f0");
                                (Items[i] as LinkBarItemBusinessClass).IsSelected = true;
                            }
                            else
                            {
                                (Items[i] as LinkBarItemBusinessClass).Foreground = Foreground;
                                (Items[i] as LinkBarItemBusinessClass).IsSelected = false;
                            }
                        }
                }
            }
        }

        private Brush _mStroke = new SolidColorBrush(Colors.Black);
        [XmlIgnore]
        public Brush Stroke
        {
            get { return _mStroke; }
            set
            {
                if (_mStroke != value)
                {
                    _mStroke = value;
                    OnPropertyChanged("Stroke");
                }
            }
        }

        public string StrokeDummy
        {
            get
            {
                if (Stroke != null && Stroke is SolidColorBrush)
                    return (Stroke as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Stroke = new SolidColorBrush(value.ToColor());
            }
        }

        private Brush _mForeground = new SolidColorBrush(Colors.Black);
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set
            {
                _mForeground = value;
                OnPropertyChanged("Foreground");

                for (int i = 0; i < Items.Count(); i++)
                {
                    if (SelectedIndex - 1 == i)
                    {
                        (Items[i] as LinkBarItemBusinessClass).Foreground = GetColorFromHexa("#83c1f0");
                    }
                    else
                    {
                        (Items[i] as LinkBarItemBusinessClass).Foreground = value;
                    }
                }
            }
        }

        public string ForegroundDummy
        {
            get
            {
                if (Foreground != null && Foreground is SolidColorBrush)
                    return (Foreground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Foreground = new SolidColorBrush(value.ToColor());
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

        private string _mExecutedPath = string.Empty;
        public string ExecutedPath
        {
            get { return _mExecutedPath; }
            set { _mExecutedPath = value; }
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
                        foreach (var item in Items)
                        {
                            (item as LinkBarItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                }
            }
        }
    }

    public class LinkBarItemBusinessClass : PropertyChange, ICommonForCollectionItem
    {
        public LinkBarItemBusinessClass()
        {
            Link = new HyperlinkBusinessClass();
            PointerEntered = new Command(OnPointerEnteredCommand);
        }

        private string _mItem = string.Empty;
        public string Item
        {
            get { return _mItem; }
            set
            {
                if (_mItem != value)
                {
                    _mItem = value;
                    OnPropertyChanged("Item");
                }
            }
        }

        private Brush _mForeground;
        [XmlIgnore]
        public Brush Foreground
        {
            get { return _mForeground; }
            set
            {
                if (_mForeground != value)
                {
                    _mForeground = value;
                    OnPropertyChanged("Foreground");
                }
            }
        }

        public string ForegroundDummy
        {
            get
            {
                if (Foreground != null && Foreground is SolidColorBrush)
                    return (Foreground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Foreground = new SolidColorBrush(value.ToColor());
            }
        }

        private bool _mIsSelected;
        public bool IsSelected
        {
            get { return _mIsSelected; }
            set 
            {
                if (_mIsSelected != value)
                {
                    _mIsSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        private bool _mIsLastItem;
        public bool IsLastItem
        {
            get { return _mIsLastItem; }
            set
            {
                if (_mIsLastItem != value)
                {
                    _mIsLastItem = value;
                    OnPropertyChanged("IsLastItem");
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

        private Brush _mEditModeFill = new SolidColorBrush(Colors.Transparent);
        [XmlIgnore]
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

        [XmlIgnore]
        public ICommand PointerEntered
        {
            get;
            set;
        }

        public void OnPointerEnteredCommand(object param)
        {
        }
    }       

    public class PopoverBusinessClass : PropertyChange
    {
        ResourceDictionary resourceDictionary = null;
        public PopoverBusinessClass() {
            resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Showcase/Mockup/Theme/MockupUI.xaml", UriKind.Absolute)
            };
            Left = true;
        }

        private DataTemplate _mTemplate;
        [XmlIgnore]
        public DataTemplate Template
        {
            get { return _mTemplate; }
            set 
            {
                if (_mTemplate != value)
                {
                    _mTemplate = value;
                    OnPropertyChanged("Template");
                }
            }
        }        

        private bool _mLeft = false;
        public bool Left
        {
            get { return _mLeft; }
            set 
            {
                if (_mLeft != value)
                {
                    _mLeft = value;
                    OnPropertyChanged("Left");
                    if(value)
                    {
                        Template = resourceDictionary["leftpopover"] as DataTemplate;
                        Right = false;
                        Top = false;
                        Bottom = false;
                    }
                }
            }
        }

        private bool _mRight = false;
        public bool Right
        {
            get { return _mRight; }
            set
            {
                if (_mRight != value)
                {
                    _mRight = value;
                    OnPropertyChanged("Right");
                    if (value)
                    {
                        Template = resourceDictionary["rightpopover"] as DataTemplate;
                        Left = false;
                        Top = false;
                        Bottom = false;
                    }
                }
            }
        }

        private bool _mTop =false;
        public bool Top
        {
            get { return _mTop; }
            set
            {
                if (_mTop != value)
                {
                    _mTop = value;
                    OnPropertyChanged("Top");
                    if (value)
                    {
                        Template = resourceDictionary["toppopover"] as DataTemplate;
                        Left = false;
                        Right = false;
                        Bottom = false;
                    }
                }
            }
        }

        private bool _mBottom = false;
        public bool Bottom
        {
            get { return _mBottom; }
            set
            {
                if (_mBottom != value)
                {
                    _mBottom = value;
                    OnPropertyChanged("Bottom");
                    if (value)
                    {
                        Template = resourceDictionary["bottompopover"] as DataTemplate;
                        Left = false;
                        Right = false;
                        Top = false;
                    }
                }
            }
        }
        
    }

    public class GeometricShapesBusinessClass : PropertyChange
    {
        ResourceDictionary resourceDictionary = null;
        public GeometricShapesBusinessClass()
        {
            resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Showcase/Mockup/Theme/MockupUI.xaml", UriKind.Absolute)
            };
            Pentagon = true; }

        private DataTemplate _mTemplate;
        [XmlIgnore]
        public DataTemplate Template
        {
            get { return _mTemplate; }
            set
            {
                if (_mTemplate != value)
                {
                    _mTemplate = value;
                    OnPropertyChanged("Template");
                }
            }
        }        


        private bool _mEllipse;
        public bool Ellipse
        {
            get { return _mEllipse; }
            set
            {
                if (_mEllipse != value)
                {
                    _mEllipse = value;
                    OnPropertyChanged("Ellipse");
                    if(value)
                    {
                        Template = resourceDictionary["gemometricellipse"] as DataTemplate;
                        Rectangle = false;
                        RoundedRectangle = false;
                        Pentagon = false;
                        Parallelogram = false;
                        Triangle = false;
                        Diamond = false;
                        Star = false;
                    }
                }
            }
        }

        private bool _mRectangle;
        public bool Rectangle
        {
            get { return _mRectangle; }
            set
            {
                if (_mRectangle != value)
                {
                    _mRectangle = value;
                    OnPropertyChanged("Rectangle");
                    if (value)
                    {
                        Template = resourceDictionary["gemometricrectangle"] as DataTemplate;
                        Ellipse = false;
                        RoundedRectangle = false;
                        Pentagon = false;
                        Parallelogram = false;
                        Triangle = false;
                        Diamond = false;
                        Star = false;
                    }
                }
            }
        }

        private bool _mRoundedRectangle;
        public bool RoundedRectangle
        {
            get { return _mRoundedRectangle; }
            set
            {
                if (_mRoundedRectangle != value)
                {
                    _mRoundedRectangle = value;
                    OnPropertyChanged("RoundedRectangle");
                    if (value)
                    {
                        Template = resourceDictionary["gemometricroundedcorner"] as DataTemplate;
                        Ellipse = false;
                        Rectangle = false;
                        Pentagon = false;
                        Parallelogram = false;
                        Triangle = false;
                        Diamond = false;
                        Star = false;
                    }
                }
            }
        }

        private bool _mPentagon;
        public bool Pentagon
        {
            get { return _mPentagon; }
            set
            {
                if (_mPentagon != value)
                {
                    _mPentagon = value;
                    OnPropertyChanged("Pentagon");
                    if (value)
                    {
                        Template = resourceDictionary["gemometricpentagon"] as DataTemplate;
                        Ellipse = false;
                        Rectangle = false;
                        RoundedRectangle = false;
                        Parallelogram = false;
                        Triangle = false;
                        Diamond = false;
                        Star = false;
                    }
                }
            }
        }

        private bool _mParallelogram;
        public bool Parallelogram
        {
            get { return _mParallelogram; }
            set
            {
                if (_mParallelogram != value)
                {
                    _mParallelogram = value;
                    OnPropertyChanged("Parallelogram");
                    if (value)
                    {
                        Template = resourceDictionary["gemometricparallelogram"] as DataTemplate;
                        Ellipse = false;
                        Rectangle = false;
                        RoundedRectangle = false;
                        Pentagon = false;
                        Triangle = false;
                        Diamond = false;
                        Star = false;
                    }
                }
            }
        }

        private bool _mTriangle;
        public bool Triangle
        {
            get { return _mTriangle; }
            set
            {
                if (_mTriangle != value)
                {
                    _mTriangle = value;
                    OnPropertyChanged("Triangle");
                    if (value)
                    {
                        Template = resourceDictionary["gemometrictriangle"] as DataTemplate;
                        Ellipse = false;
                        Rectangle = false;
                        RoundedRectangle = false;
                        Pentagon = false;
                        Parallelogram = false;
                        Diamond = false;
                        Star = false;
                    }
                }
            }
        }
         
        private bool _mStar;
        public bool Star
        {
            get { return _mStar; }
            set
            {
                if (_mStar != value)
                {
                    _mStar = value;
                    OnPropertyChanged("Star");
                    if (value)
                    {
                        Template = resourceDictionary["gemometricstar"] as DataTemplate;
                        Ellipse = false;
                        Rectangle = false;
                        RoundedRectangle = false;
                        Pentagon = false;
                        Parallelogram = false;
                        Diamond = false;
                        Triangle = false;
                    }
                }
            }
        }

        private bool _mDiamond;
        public bool Diamond
        {
            get { return _mDiamond; }
            set
            {
                if (_mDiamond != value)
                {
                    _mDiamond = value;
                    OnPropertyChanged("Diamond");
                    if (value)
                    {
                        Template = resourceDictionary["gemometricdiamond"] as DataTemplate;
                        Ellipse = false;
                        Rectangle = false;
                        RoundedRectangle = false;
                        Pentagon = false;
                        Parallelogram = false;
                        Star = false;
                        Triangle = false;
                    }
                }
            }
        }
    }

    public class StickyNoteBusinessClass : PropertyChange
    {
        public StickyNoteBusinessClass() { }

        private double _mHeaderBorderWidth = 70;
        public double HeaderBorderWidth
        {
            get { return _mHeaderBorderWidth; }
            set
            {
                if (_mHeaderBorderWidth != value)
                {
                    _mHeaderBorderWidth = value;
                    OnPropertyChanged("HeaderBorderWidth");
                }
            }
        }

        //private TextAlignment _mTextAlignment;
        //[XmlIgnore]
        //public TextAlignment TextAlignment
        //{
        //    get { return _mTextAlignment; }
        //    set
        //    {
        //        if (_mTextAlignment != value)
        //        {
        //            _mTextAlignment = value;
        //            OnPropertyChanged("TextAlignment");
        //        }
        //    }
        //}
        
    }

    public class BrowserWindowBusinessClass : PropertyChange
    {
        public BrowserWindowBusinessClass() 
        {
            ScrollBar = new VerticalScrollBarBusinessClass();
        }

        private string _mWebsiteTitle;
        public string WebsiteTitle
        {
            get { return _mWebsiteTitle; }
            set
            {
                if (_mWebsiteTitle != value)
                {
                    _mWebsiteTitle = value;
                    OnPropertyChanged("WebsiteTitle");
                }
            }
        }

        private string _mWebsiteURL;
        public string WebsiteURL
        {
            get { return _mWebsiteURL; }
            set
            {
                if (_mWebsiteURL != value)
                {
                    _mWebsiteURL = value;
                    OnPropertyChanged("WebsiteURL");
                }
            }
        }

        private double _mBrowserBodyHeight;
        public double BrowserBodyHeight
        {
            get { return _mBrowserBodyHeight; }
            set
            {
                if (_mBrowserBodyHeight != value)
                {
                    _mBrowserBodyHeight = value;
                    OnPropertyChanged("BrowserBodyHeight");
                }
            }
        }

        private double _mTitleBarHeight = 78;
        [XmlIgnore]
        public double TitleBarHeight
        {
            get { return _mTitleBarHeight; }
            private set
            {
                if (_mTitleBarHeight != value)
                {
                    _mTitleBarHeight = value;
                    OnPropertyChanged("TitleBarHeight");
                }
            }
        }

        private double _mBottomBarHeight = 22;
        public double BottomBarHeight
        {
            get { return _mBottomBarHeight; }
            set
            {
                if (_mBottomBarHeight != value)
                {
                    _mBottomBarHeight = value;
                    OnPropertyChanged("BottomBarHeight");
                }
            }
        }

        private VerticalScrollBarBusinessClass _mScrollBar;
        public VerticalScrollBarBusinessClass ScrollBar
        {
            get { return _mScrollBar; }
            set
            {
                if (_mScrollBar != value)
                {
                    _mScrollBar = value;
                    OnPropertyChanged("ScrollBar");
                }
            }
        }
    }

    public class ProgressBarBusinessClass : PropertyChange
    {
        public ProgressBarBusinessClass() { }

        private double _mProgressValue = 45;
        public double ProgressValue
        {
            get { return _mProgressValue; }
            set 
            {
                if (_mProgressValue != value)
                {
                    _mProgressValue = value;
                    OnPropertyChanged("ProgressValue");
                }
            }
        }
        
    }

    public class IOSKeyboardBusinessClass : PropertyChange
    {
        ResourceDictionary resourceDictionary = null;
        public IOSKeyboardBusinessClass()
        {
            resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Showcase/Mockup/Theme/MockupUI.xaml", UriKind.Absolute)
            };
            IPhone = true; Wide = true;
        }

        private DataTemplate _mTemplate;
        [XmlIgnore]
        public DataTemplate Template
        {
            get { return _mTemplate; }
            set
            {
                if (_mTemplate != value)
                {
                    _mTemplate = value;
                    OnPropertyChanged("Template");
                }
            }
        }        

        private bool _mIPhone;
        public bool IPhone
        {
            get { return _mIPhone; }
            set
            {
                if (_mIPhone != value)
                {
                    _mIPhone = value;
                    OnPropertyChanged("IPhone");
                    if (value)
                    {
                        IPad = false;
                        if(Wide)
                            Template = resourceDictionary["iosiphonewidekeyboard"] as DataTemplate;
                        else
                            Template = resourceDictionary["iosiphonestandardkeyboard"] as DataTemplate;
                    }
                }
            }
        }

        private bool _mIPad;
        public bool IPad
        {
            get { return _mIPad; }
            set
            {
                if (_mIPad != value)
                {
                    _mIPad = value;
                    OnPropertyChanged("IPad");
                    if (value)
                    {
                        IPhone = false;
                        if (Wide)
                            Template = resourceDictionary["iosipadwidekeyboard"] as DataTemplate;
                        else
                            Template = resourceDictionary["iosipadstandardkeyboard"] as DataTemplate;
                    }
                }
            }
        }

        private bool _mWide;
        public bool Wide
        {
            get { return _mWide; }
            set
            {
                if (_mWide != value)
                {
                    _mWide = value;
                    OnPropertyChanged("Wide");
                    if (value)
                    {
                        Standard = false;
                        if (IPhone)
                            Template = resourceDictionary["iosiphonewidekeyboard"] as DataTemplate;
                        else
                            Template = resourceDictionary["iosipadwidekeyboard"] as DataTemplate;
                    }
                }
            }
        }

        private bool _mStandard;
        public bool Standard
        {
            get { return _mStandard; }
            set
            {
                if (_mStandard != value)
                {
                    _mStandard = value;
                    OnPropertyChanged("Standard");
                    if (value)
                    {
                        Wide = false;
                        if (IPhone)
                            Template = resourceDictionary["iosiphonestandardkeyboard"] as DataTemplate;
                        else
                            Template = resourceDictionary["iosipadstandardkeyboard"] as DataTemplate;
                    }
                }
            }
        }
    }

    public class IPADBusinessClass : PropertyChange
    {
        ResourceDictionary resourceDictionary = null;
        public IPADBusinessClass()
        {
            resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Showcase/Mockup/Theme/MockupUI.xaml", UriKind.Absolute)
            };
            VerticalOrientation = true; TopBar = true;
        }

        private DataTemplate _mTemplate;
        [XmlIgnore]
        public DataTemplate Template
        {
            get { return _mTemplate; }
            set
            {
                if (_mTemplate != value)
                {
                    _mTemplate = value;
                    OnPropertyChanged("Template");
                }
            }
        }

        private bool _mVerticalOrientation;
        public bool VerticalOrientation
        {
            get { return _mVerticalOrientation; }
            set
            {
                if (_mVerticalOrientation != value)
                {
                    _mVerticalOrientation = value;
                    OnPropertyChanged("VerticalOrientation");
                    if(value)
                    {
                        HorizontalOrientation = false;
                        if(TransparentBakground)
                        {
                            if(TopBar)
                                Template = resourceDictionary["ipadTransparentWithTopBar"] as DataTemplate;
                            else
                                Template = resourceDictionary["ipadTransparent"] as DataTemplate;
                        }
                        else
                        {
                            if (TopBar)
                                Template = resourceDictionary["ipadNonTransparentWithTopBar"] as DataTemplate;
                            else
                                Template = resourceDictionary["ipadNonTransparent"] as DataTemplate;
                        }
                    }
                }
            }
        }

        private bool _mHorizontalOrientation;
        public bool HorizontalOrientation
        {
            get { return _mHorizontalOrientation; }
            set
            {
                if (_mHorizontalOrientation != value)
                {
                    _mHorizontalOrientation = value;
                    OnPropertyChanged("HorizontalOrientation");
                    if(value)
                    {
                        VerticalOrientation = false;
                            if (TransparentBakground)
                            {
                                if (TopBar)
                                    Template = resourceDictionary["ipadhorizotalTransparentWithTopBar"] as DataTemplate;
                                else
                                    Template = resourceDictionary["ipadhorizotalTransparent"] as DataTemplate;
                            }
                            else
                            {
                                if (TopBar)
                                    Template = resourceDictionary["ipadhorizotalNonTransparentWithTopBar"] as DataTemplate;
                                else
                                    Template = resourceDictionary["ipadhorizotalNonTransparent"] as DataTemplate;
                            }
                    }
                }
            }
        }

        private bool _mTransparentBakground;
        public bool TransparentBakground
        {
            get { return _mTransparentBakground; }
            set
            {
                if (_mTransparentBakground != value)
                {
                    _mTransparentBakground = value;
                    OnPropertyChanged("TransparentBakground");
                    if(value)
                    {
                        if(VerticalOrientation)
                        {
                            if (TopBar)
                                Template = resourceDictionary["ipadTransparentWithTopBar"] as DataTemplate;
                            else
                                Template = resourceDictionary["ipadTransparent"] as DataTemplate;
                        }
                        else
                        {
                            if (TopBar)
                                Template = resourceDictionary["ipadhorizotalTransparentWithTopBar"] as DataTemplate;
                            else
                                Template = resourceDictionary["ipadhorizotalTransparent"] as DataTemplate;
                        }
                    }
                    else
                    {
                        if (VerticalOrientation)
                        {
                            if (TopBar)
                                Template = resourceDictionary["ipadNonTransparentWithTopBar"] as DataTemplate;
                            else
                                Template = resourceDictionary["ipadNonTransparent"] as DataTemplate;
                        }
                        else
                        {
                            if (TopBar)
                                Template = resourceDictionary["ipadhorizotalNonTransparentWithTopBar"] as DataTemplate;
                            else
                                Template = resourceDictionary["ipadhorizotalNonTransparent"] as DataTemplate;
                        }
                    }
                }
            }
        }

        private bool _mTopBar;
        public bool TopBar
        {
            get { return _mTopBar; }
            set
            {
                if (_mTopBar != value)
                {
                    _mTopBar = value;
                    OnPropertyChanged("TopBar");
                    if(value)
                    {
                        if(VerticalOrientation)
                        {
                            if(TransparentBakground)
                                Template = resourceDictionary["ipadTransparentWithTopBar"] as DataTemplate;
                            else
                                Template = resourceDictionary["ipadNonTransparentWithTopBar"] as DataTemplate;
                        }
                        else
                        {
                            if (TransparentBakground)
                                Template = resourceDictionary["ipadhorizotalTransparentWithTopBar"] as DataTemplate;
                            else
                                Template = resourceDictionary["ipadhorizotalNonTransparentWithTopBar"] as DataTemplate;
                        }
                    }
                    else
                    {
                        if (VerticalOrientation)
                        {
                            if (TransparentBakground)
                                Template = resourceDictionary["ipadTransparent"] as DataTemplate;
                            else
                                Template = resourceDictionary["ipadNonTransparent"] as DataTemplate;
                        }
                        else
                        {
                            if (TransparentBakground)
                                Template = resourceDictionary["ipadhorizotalTransparent"] as DataTemplate;
                            else
                                Template = resourceDictionary["ipadhorizotalNonTransparent"] as DataTemplate;
                        }
                    }
                }
            }
        }        
    }

    public class IPhoneBusinessClass : PropertyChange
    {
        ResourceDictionary resourceDictionary = null;
        public IPhoneBusinessClass() {


            resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Showcase/Mockup/Theme/MockupUI.xaml", UriKind.Absolute)
            };
            VerticalOrientation = true; TransparentBakground = false; TopBar = true;
        }

        private DataTemplate _mTemplate;
        [XmlIgnore]
        public DataTemplate Template
        {
            get { return _mTemplate; }
            set
            {
                if (_mTemplate != value)
                {
                    _mTemplate = value;
                    OnPropertyChanged("Template");
                }
            }
        }

        private bool _mVerticalOrientation;
        public bool VerticalOrientation
        {
            get { return _mVerticalOrientation; }
            set
            {
                if (_mVerticalOrientation != value)
                {
                    _mVerticalOrientation = value;
                    OnPropertyChanged("VerticalOrientation");
                    if(value)
                    {
                        HorizontalOrientation = false;
                        Template = resourceDictionary["iphoneverticalorientation"] as DataTemplate;
                    }
                }
            }
        }

        private bool _mHorizontalOrientation;
        public bool HorizontalOrientation
        {
            get { return _mHorizontalOrientation; }
            set
            {
                if (_mHorizontalOrientation != value)
                {
                    _mHorizontalOrientation = value;
                    OnPropertyChanged("HorizontalOrientation");
                    if (value)
                    {
                        VerticalOrientation = false;
                        Template = resourceDictionary["iphonehorizontalorientation"] as DataTemplate;
                    }
                }
            }
        }

        private bool _mTransparentBakground;
        public bool TransparentBakground
        {
            get { return _mTransparentBakground; }
            set
            {
                if (_mTransparentBakground != value)
                {
                    _mTransparentBakground = value;
                    OnPropertyChanged("TransparentBakground");
                    if(value)
                    {
                        Background = new SolidColorBrush(Colors.Transparent);
                    }
                    else
                    {
                        if(Pattern5)
                        {
                            Background = new SolidColorBrush(Colors.DarkGreen);
                        }
                        else
                            Background = new SolidColorBrush(Colors.White);
                    }
                }
            }
        }

        private bool _mTopBar;
        public bool TopBar
        {
            get { return _mTopBar; }
            set
            {
                if (_mTopBar != value)
                {
                    _mTopBar = value;
                    OnPropertyChanged("TopBar");
                }
            }
        }

        private bool _mPattern1;
        public bool Pattern1
        {
            get { return _mPattern1; }
            set
            {
                if (_mPattern1 != value)
                {
                    _mPattern1 = value;
                    OnPropertyChanged("Pattern1");
                    if(value)
                    {
                        Background = new SolidColorBrush(Colors.White);
                        Pattern2 = false;
                        Pattern3 = false;
                        Pattern4 = false;
                        Pattern5 = false;
                        TopPatternBorder = false;
                    }
                }
            }
        }

        private bool _mPattern2;
        public bool Pattern2
        {
            get { return _mPattern2; }
            set
            {
                if (_mPattern2 != value)
                {
                    _mPattern2 = value;
                    OnPropertyChanged("Pattern2");
                    if (value)
                    {
                        Pattern1 = false;
                        Pattern3 = false;
                        Pattern4 = false;
                        Pattern5 = false;
                        TopPatternBorder = true;
                    }
                }
            }
        }

        private bool _mPattern3;
        public bool Pattern3
        {
            get { return _mPattern3; }
            set
            {
                if (_mPattern3 != value)
                {
                    _mPattern3 = value;
                    OnPropertyChanged("Pattern3");
                    if (value)
                    {
                        Pattern1 = false;
                        Pattern2 = false;
                        Pattern4 = false;
                        Pattern5 = false;
                        TopPatternBorder = true;
                    }
                }
            }
        }

        private bool _mPattern4;
        public bool Pattern4
        {
            get { return _mPattern4; }
            set
            {
                if (_mPattern4 != value)
                {
                    _mPattern4 = value;
                    OnPropertyChanged("Pattern4");
                    if (value)
                    {                        
                        Pattern1 = false;
                        Pattern2 = false;
                        Pattern3 = false;
                        Pattern5 = false;
                        TopPatternBorder = true;
                    }
                }
            }
        }

        private bool _mPattern5;
        public bool Pattern5
        {
            get { return _mPattern5; }
            set
            {
                if (_mPattern5 != value)
                {
                    _mPattern5 = value;
                    OnPropertyChanged("Pattern5");
                    if (value)
                    {
                        if (!TransparentBakground)
                            Background = new SolidColorBrush(Colors.DarkGreen);
                        Pattern1 = false;
                        Pattern2 = false;
                        Pattern3 = false;
                        Pattern4 = false;
                        TopPatternBorder = false;
                    }
                    else
                    {
                        if (!TransparentBakground)
                            Background = new SolidColorBrush(Colors.White);
                        else
                            Background = new SolidColorBrush(Colors.Transparent);
                    }
                }
            }
        }

        private bool _mTopPatternBorder;
        public bool TopPatternBorder
        {
            get { return _mTopPatternBorder; }
            set
            {
                if (_mTopPatternBorder != value)
                {
                    _mTopPatternBorder = value;
                    OnPropertyChanged("TopPatternBorder");
                }
            }
        }

        private Brush _mBackground = new SolidColorBrush(Colors.White);
        [XmlIgnore]
        public Brush Background
        {
            get { return _mBackground; }
            set
            {
                if (_mBackground != value)
                {
                    _mBackground = value;
                    OnPropertyChanged("Background");
                }
            }
        }

        public string DummyBackground
        {
            get
            {
                if (Background != null && Background is SolidColorBrush)
                    return (Background as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                Background = new SolidColorBrush(value.ToColor());
            }
        }
    }

    public class ToolTipBusinessClass : PropertyChange
    {
        ResourceDictionary resourceDictionary = null;
        public ToolTipBusinessClass()
        {
            resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Showcase/Mockup/Theme/MockupUI.xaml", UriKind.Absolute)
            };
            TopLeft = true;
        }

        private DataTemplate _mTemplate;
        [XmlIgnore]
        public DataTemplate Template
        {
            get { return _mTemplate; }
            set
            {
                if (_mTemplate != value)
                {
                    _mTemplate = value;
                    OnPropertyChanged("Template");
                }
            }
        }        

        private bool _mTopLeft;
        public bool TopLeft
        {
            get { return _mTopLeft; }
            set
            {
                if (_mTopLeft != value)
                {
                    _mTopLeft = value;
                    OnPropertyChanged("TopLeft");
                    if (value)
                    {
                        Template = resourceDictionary["tooltiptopleft"] as DataTemplate;
                        VerticalAlignment = VerticalAlignment.Center;
                        TopRight = false;
                        BottomLeft = false;
                        BottomRight = false;
                    }
                }
            }
        }

        private bool _mTopRight;
        public bool TopRight
        {
            get { return _mTopRight; }
            set
            {
                if (_mTopRight != value)
                {
                    _mTopRight = value;
                    OnPropertyChanged("TopRight");
                    if (value)
                    {
                        Template = resourceDictionary["tooltiptopright"] as DataTemplate;
                        VerticalAlignment = VerticalAlignment.Center;
                        TopLeft = false;
                        BottomLeft = false;
                        BottomRight = false;
                    }
                }
            }
        }

        private bool _mBottomLeft;
        public bool BottomLeft
        {
            get { return _mBottomLeft; }
            set
            {
                if (_mBottomLeft != value)
                {
                    _mBottomLeft = value;
                    OnPropertyChanged("BottomLeft");
                    if (value)
                    {
                        Template = resourceDictionary["tooltipbottomleft"] as DataTemplate;
                        VerticalAlignment = VerticalAlignment.Top;
                        TopLeft = false;
                        TopRight = false;
                        BottomRight = false;
                    }
                }
            }
        }

        private bool _mBottomRight;
        public bool BottomRight
        {
            get { return _mBottomRight; }
            set
            {
                if (_mBottomRight != value)
                {
                    _mBottomRight = value;
                    OnPropertyChanged("BottomRight");
                    if (value)
                    {
                        Template = resourceDictionary["tooltipbottomright"] as DataTemplate;
                        VerticalAlignment = VerticalAlignment.Top;
                        TopLeft = false;
                        TopRight = false;
                        BottomLeft = false;
                    }
                }
            }
        }

        private VerticalAlignment _mVerticalAlignment;
        public VerticalAlignment VerticalAlignment
        {
            get { return _mVerticalAlignment; }
            set
            {
                if (_mVerticalAlignment != value)
                {
                    _mVerticalAlignment = value;
                    OnPropertyChanged("VerticalAlignment");
                }
            }
        }
    }

    public class CustomToggleButton : ToggleButton
    {
        protected override void OnToggle()
        {
            if (!IsLocked)
            {
                base.OnToggle();
            }
        }

        public bool IsLocked
        {
            get { return (bool)GetValue(IsLockedProperty); }
            set { SetValue(IsLockedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Locked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLockedProperty =
            DependencyProperty.Register("IsLocked", typeof(bool), typeof(CustomToggleButton), new PropertyMetadata(false));

        
        
    }

    public class CustomSerialization
    {
        public CustomSerialization() { }

        private int myVar = 5;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        
        private Brush _mFill = new SolidColorBrush(Colors.Red);
        [XmlIgnore]
        public Brush Fill
        {
            get { return _mFill; }
            set
            {
                _mFill = value;
            }
        }
    }
}
