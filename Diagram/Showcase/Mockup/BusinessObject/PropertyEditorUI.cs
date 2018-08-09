using Syncfusion.CompoundFile.DocIO;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mockup.BusinessObject
{
    public class PropertyEditorUI : DiagramElementViewModel
    {
        private bool _mLabelFont;
        public bool LabelFont
        {
            get { return _mLabelFont; }
            set
            {
                if (_mLabelFont != value)
                {
                    _mLabelFont = value;
                    OnPropertyChanged("LabelFont");
                }
            }
        }

        private bool _mLabelAlignment;
        public bool LabelAlignment
        {
            get { return _mLabelAlignment; }
            set
            {
                if (_mLabelAlignment != value)
                {
                    _mLabelAlignment = value;
                    OnPropertyChanged("LabelAlignment");
                }
            }
        }

        private bool _mFill;
        public bool Fill
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
        

        private bool _mCollection;
        public bool Collection
        {
            get { return _mCollection; }
            set
            {
                if (_mCollection != value)
                {
                    _mCollection = value;
                    OnPropertyChanged("Collection");
                }
            }
        }

        private bool _mSliders;
        public bool Sliders
        {
            get { return _mSliders; }
            set
            {
                if (_mSliders != value)
                {
                    _mSliders = value;
                    OnPropertyChanged("Sliders");
                }
            }
        }

        private bool _mList;
        public bool List
        {
            get { return _mList; }
            set 
            {
                if (_mList != value)
                {
                    _mList = value;
                    OnPropertyChanged("List");
                }
            }
        }

        private bool _mOnOff;
        public bool OnOff
        {
            get { return _mOnOff; }
            set 
            { 
                if(_mOnOff != value)
                {
                    _mOnOff = value;
                    OnPropertyChanged("OnOff");
                }                    
             }
        }

        private bool _mWindowBox;
        public bool WindowBox
        {
            get { return _mWindowBox; }
            set
            {
                if (_mWindowBox != value)
                {
                    _mWindowBox = value;
                    OnPropertyChanged("WindowBox");
                }
            }
        }

        private bool _mLabelOrientation;
        public bool LabelOrientation
        {
            get { return _mLabelOrientation; }
            set 
            {
                if (_mLabelOrientation != value)
                {
                    _mLabelOrientation = value;
                    OnPropertyChanged("LabelOrientation");
                }
            }
        }

        private bool _mMenuBar;
        public bool MenuBar
        {
            get { return _mMenuBar; }
            set 
            {
                if (_mMenuBar != value)
                {
                    _mMenuBar = value;
                    OnPropertyChanged("MenuBar");
                }
            }
        }
        
    }

    public class PropertiesList : DiagramElementViewModel
    {
        private bool _mAlignmentTab =  false;
        public bool AlignmentTab
        {
            get { return _mAlignmentTab; }
            set
            {
                //if (_mAlignmentTab != value)
                {
                    _mAlignmentTab = value;
                    OnPropertyChanged("Alignment");
                }
            }
        }

        private TextTab _mEditTab;
        public TextTab EditTab
        {
            get { return _mEditTab; }
            set
            {
                //if (_mEditTab != value)
                {
                    _mEditTab = value;
                    OnPropertyChanged("EditTab");
                }
            }
        }

        private SettingTab _mSettingTab;
        public SettingTab SettingTab
        {
            get { return _mSettingTab; }
            set 
            {
                //if (_mSettingTab != value)
                {
                    _mSettingTab = value;
                    OnPropertyChanged("SettingTab");
                }
            } 
        }  

        private bool _mLinkTab;
        public bool LinkTab
        {
            get { return _mLinkTab; }
            set
            {
                //if (_mLinkTab != value)
                {
                    _mLinkTab = value;
                    OnPropertyChanged("LinkTab"); 
                }
            }
        }
    }

    public class TextTab : DiagramElementViewModel, ICloneable
    {
        private bool _mDefault = true;
        public bool Default
        {
            get { return _mDefault; }
            set
            {
                if (_mDefault != value)
                {
                    _mDefault = value;
                    OnPropertyChanged("Default");
                }
            }
        }

        private bool _mTextAlignment = true;
        public bool TextAlignment
        {
            get { return _mTextAlignment; }
            set
            {
                if (_mTextAlignment != value)
                {
                    _mTextAlignment = value;
                    OnPropertyChanged("TextAlignment");
                }
            }
        }

        private bool _mFontFamily = true;
        public bool FontFamily
        {
            get { return _mFontFamily; }
            set
            {
                if (_mFontFamily != value)
                {
                    _mFontFamily = value;
                    OnPropertyChanged("FontFamily");
                }
            }
        }

        private bool _mForeground = true;
        public bool Foreground
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

        private bool _mFontSize = true;
        public bool FontSize
        {
            get { return _mFontSize; }
            set
            {
                if (_mFontSize != value)
                {
                    _mFontSize = value;
                    OnPropertyChanged("FontSize");
                }
            }
        }

        private bool _mFontStyle = true;
        public bool FontStyle
        {
            get { return _mFontStyle; }
            set
            {
                if (_mFontStyle != value)
                {
                    _mFontStyle = value;
                    OnPropertyChanged("FontStyle");
                }
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SettingTab : DiagramElementViewModel, ICloneable
    {
        private bool _mFill = false;
        public bool Fill 
        {
            get { return _mFill; }
            set 
            { 
                _mFill = value;
                OnPropertyChanged("Fill");
            }
        }

        private bool _mOpacity = false;
        public bool Opacity
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

        private bool _mStroke = false;
        public bool Stroke
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

        private bool _mStrokeThickness = false;
        public bool StrokeThickness
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

        private bool _mCollection = false;
        public bool Collection
        {
            get { return _mCollection; }
            set
            {
                if (_mCollection != value)
                {
                    _mCollection = value;
                    OnPropertyChanged("Collection");
                }
            }
        }

        private bool _mSelection = false;
        public bool Selection
        {
            get { return _mSelection; }
            set
            {
                if (_mSelection != value)
                {
                    _mSelection = value;
                    OnPropertyChanged("Selection");
                }
            }
        }

        private bool _mScrollSlider = false;
        public bool ScrollSlider
        {
            get { return _mScrollSlider; }
            set
            {
                if (_mScrollSlider != value)
                {
                    _mScrollSlider = value;
                    OnPropertyChanged("ScrollSlider");
                }
            }
        }

        private bool _mVerticalScrollBar = false;
        public bool VerticalScrollBar
        {
            get { return _mVerticalScrollBar; }
            set
            {
                if (_mVerticalScrollBar != value)
                {
                    _mVerticalScrollBar = value;
                    OnPropertyChanged("VerticalScrollBar");
                }
            }
        }

        private bool _mStrokeStyle = false;
        public bool StrokeStyle
        {
            get { return _mStrokeStyle; }
            set
            {
                if (_mStrokeStyle != value)
                {
                    _mStrokeStyle = value;
                    OnPropertyChanged("StrokeStyle");
                }
            }
        }

        private bool _mListOddEven = false;
        public bool ListOddEven
        {
            get { return _mListOddEven; }
            set
            {
                if (_mListOddEven != value)
                {
                    _mListOddEven = value;
                    OnPropertyChanged("ListOddEven");
                }
            }
        }

        private bool _mOnOffSwitch = false;
        public bool OnOffSwitch
        {
            get { return _mOnOffSwitch; }
            set
            {
                if (_mOnOffSwitch != value)
                {
                    _mOnOffSwitch = value;
                    OnPropertyChanged("OnOffSwitch");
                }
            }
        }

        private bool _mPointyButton = false;
        public bool PointyButton
        {
            get { return _mPointyButton; }
            set
            {
                if (_mPointyButton != value)
                {
                    _mPointyButton = value;
                    OnPropertyChanged("PointyButton");
                }
            }
        }

        private bool _mToolTip = false;
        public bool ToolTip
        {
            get { return _mToolTip; }
            set
            {
                if (_mToolTip != value)
                {
                    _mToolTip = value;
                    OnPropertyChanged("ToolTip");
                }
            }
        }

        private bool _mVerticalTab = false;
        public bool VerticalTab
        {
            get { return _mVerticalTab; }
            set
            {
                if (_mVerticalTab != value)
                {
                    _mVerticalTab = value;
                    OnPropertyChanged("VerticalTab");
                }
            }
        }

        private bool _mHorizontalTab = false;
        public bool HorizontalTab
        {
            get { return _mHorizontalTab; }
            set
            {
                if (_mHorizontalTab != value)
                {
                    _mHorizontalTab = value;
                    OnPropertyChanged("HorizontalTab");
                }
            }
        }

        private bool _mGeometricShapes = false;
        public bool GeometricShapes
        {
            get { return _mGeometricShapes; }
            set
            {
                if (_mGeometricShapes != value)
                {
                    _mGeometricShapes = value;
                    OnPropertyChanged("GeometricShapes");
                }
            }
        }

        private bool _mVerticalCurlyBraces = false;
        public bool VerticalCurlyBraces
        {
            get { return _mVerticalCurlyBraces; }
            set
            {
                if (_mVerticalCurlyBraces != value)
                {
                    _mVerticalCurlyBraces = value;
                    OnPropertyChanged("VerticalCurlyBraces");
                }
            }
        }

        private bool _mHorizontalCurlyBraces = false;
        public bool HorizontalCurlyBraces
        {
            get { return _mHorizontalCurlyBraces; }
            set
            {
                if (_mHorizontalCurlyBraces != value)
                {
                    _mHorizontalCurlyBraces = value;
                    OnPropertyChanged("HorizontalCurlyBraces");
                }
            }
        }

        private bool _mPopover = false;
        public bool Popover
        {
            get { return _mPopover; }
            set
            {
                if (_mPopover != value)
                {
                    _mPopover = value;
                    OnPropertyChanged("Popover");
                }
            }
        }

        private bool _mShowBorder = false;
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

        private bool _mMenuIcon = false;
        public bool MenuIcon
        {
            get { return _mMenuIcon; }
            set
            {
                if (_mMenuIcon != value)
                {
                    _mMenuIcon = value;
                    OnPropertyChanged("MenuIcon");
                }
            }
        }

        private bool _mWindowBox = false;
        public bool WindowBox
        {
            get { return _mWindowBox; }
            set
            {
                if (_mWindowBox != value)
                {
                    _mWindowBox = value;
                    OnPropertyChanged("WindowBox");
                }
            }
        }

        private bool _mioskeyboard = false;
        public bool ioskeyboard
        {
            get { return _mioskeyboard; }
            set
            {
                if (_mioskeyboard != value)
                {
                    _mioskeyboard = value;
                    OnPropertyChanged("ioskeyboard");
                }
            }
        }

        private bool _mipad = false;
        public bool ipad
        {
            get { return _mipad; }
            set
            {
                if (_mipad != value)
                {
                    _mipad = value;
                    OnPropertyChanged("ipad");
                }
            }
        }

        private bool _mIPhone = false;
        public bool IPhone
        {
            get { return _mIPhone; }
            set
            {
                if (_mIPhone != value)
                {
                    _mIPhone = value;
                    OnPropertyChanged("IPhone");
                }
            }
        }

        private bool _mAccordion = false;
        public bool Accordion
        {
            get { return _mAccordion; }
            set
            {
                if (_mAccordion != value)
                {
                    _mAccordion = value;
                    OnPropertyChanged("Accordion");
                }
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
