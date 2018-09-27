using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Serializer;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using System.Runtime.Serialization;
using DiagramBuilder.Utility;
using System.ComponentModel;
using System.Collections;
using Windows.Foundation;

namespace DiagramBuilder.ViewModel
{
    public abstract class GroupableVM : GroupableViewModel, IGroupableVM
    {
        LabelVM _mAnnotation = new LabelVM() { UnitWidth = 100, UnitHeight = 100, TextHorizontalAlignment = TextAlignment.Center, TextVerticalAlignment = VerticalAlignment.Center };
        LabelVM _mAnnotationHyperLink = new LabelVM("hyperlink");

        public GroupableVM()
        {
            this.Annotations = new List<IAnnotation>() { _mAnnotation, _mAnnotationHyperLink };
            _mAnnotation.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "Content")
                    {
                        this.Label = (string)_mAnnotation.Content;
                        this.HyperLink = (string)_mAnnotationHyperLink.HyperLink;
                    }
                };
        }

        #region Shape

        #region Prop
        Brush _mStroke = new SolidColorBrush(Colors.Red);
        public Brush Stroke
        {
            get
            {
                return _mStroke;
            }
            set
            {
                if (_mStroke != value)
                {
                    _mStroke = value;
                    OnPropertyChanged(GroupableConstants.Stroke);
                }
            }
        }

        [DataMember]
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

        double _mThickness = 1d;
        [DataMember]
        public double Thickness
        {
            get
            {
                return _mThickness;
            }
            set
            {
                if (_mThickness != value)
                {
                    _mThickness = value;
                    OnPropertyChanged(GroupableConstants.Thickness);
                }
            }
        }

        string _mDashDot = "1, 0";
        [DataMember]
        public string DashDot
        {
            get
            {
                return _mDashDot;
            }
            set
            {
                if (_mDashDot != value)
                {
                    _mDashDot = value;
                    OnPropertyChanged(GroupableConstants.DashDot);
                }
            }
        }

        Style _mStyle = null;
        public Style Style
        {
            get
            {
                return _mStyle;
            }
            set
            {
                if (_mStyle != value)
                {
                    _mStyle = value;
                    OnPropertyChanged(GroupableConstants.Style);
                }
            }
        }

        double _mOpacity = 1d;
        [DataMember]
        public double Opacity
        {
            get
            {
                return _mOpacity;
            }
            set
            {
                if (_mOpacity != value)
                {
                    _mOpacity = value;
                    OnPropertyChanged(GroupableConstants.Opacity);
                }
            }
        }
        #endregion

        private void OnThicknessChanged()
        {
            ApplyStyle(GetCustomStyle());
        }

        private void OnStyleChanged()
        {
            DecodeStyle(Style);
        }

        private void OnStrokeChanged()
        {
            ApplyStyle(GetCustomStyle());
        }

        private void OnDashDotChanged()
        {
            ApplyStyle(GetCustomStyle());
        }

        private void OnOpacityChanged()
        {
            ApplyStyle(GetCustomStyle());
        }

        protected virtual void DecodeStyle(Style style)
        {
            foreach (Setter setter in style.Setters)
            {
                if (setter.Property == Path.StrokeProperty)
                {
                    Stroke = setter.Value as Brush;
                }
                else if (setter.Property == Path.StrokeThicknessProperty)
                {
                    Thickness = (double)setter.Value;
                }
                else if (setter.Property == Path.StrokeDashArrayProperty)
                {
                    string data = "";
                    foreach (double item in setter.Value as DoubleCollection)
                    {
                        data += item + ", ";
                    }
                    data = data.Substring(0, data.Length - 3);
                    DashDot = data;
                }
                else if (setter.Property == Path.OpacityProperty)
                {
                    Opacity = (double)setter.Value;
                }
            }
        }

        protected virtual Style GetCustomStyle()
        {
            Style customStyle = new Style(typeof(Path));
            if (Stroke != null)
            {
                customStyle.Setters.Add(new Setter(Path.StrokeProperty, Stroke));
            }
            if (!double.IsNaN(Thickness) && !double.IsInfinity(Thickness))
            {
                customStyle.Setters.Add(new Setter(Path.StrokeThicknessProperty, Thickness));
            }
            customStyle.Setters.Add(new Setter(Path.OpacityProperty, Opacity));
            if (DashDot != null)
            {
                customStyle.Setters.Add(new Setter(Path.StrokeDashArrayProperty, DashDot.ToString()));
            }
            return customStyle;
        }

        protected abstract void ApplyStyle(Style style);

        #endregion

        #region Label
        string _mLabel = string.Empty;
        [DataMember]
        public string Label
        {
            get
            {
                return _mLabel;
            }
            set
            {
                if (_mLabel != value)
                {
                    _mLabel = value;
                    OnPropertyChanged(GroupableConstants.Label);
                }
            }
        }

        FontFamily _mFont = DiagramBuilderVM.Fonts[0];
        public FontFamily Font
        {
            get
            {
                return _mFont;
            }
            set
            {
                if (_mFont != value)
                {
                    _mFont = value;
                    OnPropertyChanged(GroupableConstants.Font);
                }
            }
        }

        public string FontDummy
        {
            get
            {
                if (Font != null)
                {
                    return Font.Source;
                }
                return null;
            }
            set
            {
                Font = DiagramBuilderVM.Fonts.Where(ft => ft.Source == value).FirstOrDefault();
            }
        }

        int _mFontSize = 12;
        [DataMember]
        public int FontSize
        {
            get
            {
                return _mFontSize;
            }
            set
            {
                if (_mFontSize != value)
                {
                    _mFontSize = value;
                    OnPropertyChanged(GroupableConstants.FontSize);
                }
            }
        }

        FontWeight _mFontWeight = FontWeights.Normal;
        [DataMember]
        public FontWeight FontWeight
        {
            get
            {
                return _mFontWeight;
            }
            set
            {
                //if (_mFontWeight != value)
                {
                    _mFontWeight = value;
                    _mAnnotation.FontWeight = value;
                    OnPropertyChanged(GroupableConstants.FontWeight);
                }
            }
        }

        private FontStyle _mFontStyle = FontStyle.Normal;
        [DataMember]
        public FontStyle FontStyle
        {
            get
            {
                return _mFontStyle;
            }
            set
            {
                if (_mFontStyle != value)
                {
                    _mFontStyle = value;
                    _mAnnotation.FontStyle = value;
                    OnPropertyChanged(GroupableConstants.FontStyle);
                }
            }
        }

        bool _mBold = false;
        [DataMember]
        public bool Bold
        {
            get
            {
                return _mBold;
            }
            set
            {
                if (_mBold != value)
                {
                    _mBold = value;
                    OnPropertyChanged(GroupableConstants.Bold);
                }
            }
        }

        bool _mItalic = false;
        [DataMember]
        public bool Italic
        {
            get
            {
                return _mItalic;
            }
            set
            {
                if (_mItalic != value)
                {
                    _mItalic = value;
                    OnPropertyChanged(GroupableConstants.Italic);
                }
            }
        }

        TextAlignment _mTextAlign = TextAlignment.Center;
        [DataMember]
        public TextAlignment TextAlignment
        {
            get
            {
                return _mTextAlign;
            }
            set
            {
                if (_mTextAlign != value)
                {
                    _mTextAlign = value;
                    OnPropertyChanged(GroupableConstants.TextAlignment);
                }
            }
        }

        HorizontalAlignment _mLabelHAlign = HorizontalAlignment.Center;
        [DataMember]
        public HorizontalAlignment LabelHAlign
        {
            get
            {
                return _mLabelHAlign;
            }
            set
            {
                if (_mLabelHAlign != value)
                {
                    _mLabelHAlign = value;
                    OnPropertyChanged(GroupableConstants.LabelHAlign);
                }
            }
        }

        VerticalAlignment _mLabelVAlign = VerticalAlignment.Center;
        [DataMember]
        public VerticalAlignment LabelVAlign
        {
            get
            {
                return _mLabelVAlign;
            }
            set
            {
                if (_mLabelVAlign != value)
                {
                    _mLabelVAlign = value;
                    OnPropertyChanged(GroupableConstants.LabelVAlign);
                }
            }
        }

        Thickness _mLabelMargin = new Thickness(5);
        [DataMember]
        public Thickness LabelMargin
        {
            get
            {
                return _mLabelMargin;
            }
            set
            {
                if (_mLabelMargin != value)
                {
                    _mLabelMargin = value;
                    _mAnnotation.LabelMargin = value;
                    OnPropertyChanged(GroupableConstants.LabelMargin);
                }
            }
        }

        private Brush _mLabelBackground = new SolidColorBrush(Colors.Transparent);
        public Brush LabelBackground
        {
            get { return _mLabelBackground; }
            set
            {
                if (_mLabelBackground != value)
                {
                    _mLabelBackground = value;
                    OnPropertyChanged(GroupableConstants.LabelBackground);
                }
            }
        }

     //   private double _mlabelx = 5;
     //   public double LabelX
     //   {
      //      get
      //      {
       //         return _mlabelx;
      //      }
       //     set
       //     {
       //         if (_mlabelx != value)
       //         {
       //             _mlabelx = value;
        //            OnPropertyChanged(GroupableConstants.LabelX);
       //         }
       //     }
      //  }

       // private double _mlabely = 5;
       // public double LabelY
        //{
        //    get
        //    {
         //       return _mlabely;
         //   }
          //  set
          //  {
           //     if (_mlabely != value)
            //    {
             //       _mlabely = value;
             //       OnPropertyChanged(GroupableConstants.LabelY);
              //  }
           // }
       // }

        private double _mLabelMarginLeft = 0d;

        public double LabelMarginLeft
        {
            get
            {
                return _mLabelMarginLeft;
            }
            set
            {
                if (_mLabelMarginLeft != value)
                {
                    _mLabelMarginLeft = value;
                    OnPropertyChanged(GroupableConstants.LabelMarginLeft);
                }
            }
        }

        private double _mLabelMarginRight = 0d;

        public double LabelMarginRight
        {
            get
            {
                return _mLabelMarginRight;
            }
            set
            {
                if (_mLabelMarginRight != value)
                {
                    _mLabelMarginRight = value;
                    OnPropertyChanged(GroupableConstants.LabelMarginRight);
                }
            }
        }

        private double _mLabelMarginTop = 0d;

        public double LabelMarginTop
        {
            get
            {
                return _mLabelMarginTop;
            }
            set
            {
                if (_mLabelMarginTop != value)
                {
                    _mLabelMarginTop = value;
                    OnPropertyChanged(GroupableConstants.LabelMarginTop);
                }
            }
        }

        private double _mLabelMarginBottom = 0d;

        public double LabelMarginBottom
        {
            get
            {
                return _mLabelMarginBottom;
            }
            set
            {
                if (_mLabelMarginBottom != value)
                {
                    _mLabelMarginBottom = value;
                    OnPropertyChanged(GroupableConstants.LabelMarginBottom);
                }
            }
        }
        private TextWrapping _mwrapping;
        public TextWrapping LabelTextWrapping
        {
            get
            {
                return _mwrapping;
            }
            set
            {
                if (_mwrapping != value)
                {
                    _mwrapping = value;
                    OnPropertyChanged(GroupableConstants.LabelTextWrapping);
                }
            }
        }
        //private bool _drag = false;
        //public bool LabelDrag
        //{
        //    get
        //    {
        //        return _drag;
        //    }
        //    set
        //    {
        //        if(_drag!=value)
        //        {
        //            _drag = value;
        //            OnPropertyChanged(GroupableConstants.LabelDrag);
        //        }
        //    }
        //}

        private Brush _mLabelForeground = new SolidColorBrush(Colors.Black);
        public Brush LabelForeground
        {
            get { return _mLabelForeground; }
            set
            {
                if (_mLabelForeground != value)
                {
                    _mLabelForeground = value;
                    OnPropertyChanged(GroupableConstants.LabelForeground);
                }
            }
        }

        [DataMember]
        public string LabelBgDummy
        {
            get
            {
                if (LabelBackground != null && LabelBackground is SolidColorBrush)
                    return (LabelBackground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                LabelBackground = new SolidColorBrush(value.ToColor());
            }
        }

        [DataMember]
        public string LabelFgDummy
        {
            get
            {
                if (LabelForeground != null && LabelForeground is SolidColorBrush)
                    return (LabelForeground as SolidColorBrush).Color.ToString();
                else
                    return null;
            }
            set
            {
                LabelForeground = new SolidColorBrush(value.ToColor());
            }
        }

        #endregion

        public IEnumerable<IAnnotation> AnnotationsE
        {
            get { return this.Annotations as IEnumerable<IAnnotation>; }
        }

        string _mName = string.Empty;
        [DataMember]
        public string Name
        {
            get
            {
                return _mName;
            }
            set
            {
                if (_mName != value)
                {
                    _mName = value;
                    OnPropertyChanged(GroupableConstants.Name);
                }
            }
        }

        #region State

        private Visibility _mVisibility = Visibility.Visible;
        [DataMember]
        public Visibility Visibility
        {
            get
            {
                return _mVisibility;
            }
            set
            {
                if (_mVisibility != value)
                {
                    _mVisibility = value;
                    OnPropertyChanged(GroupableConstants.Visibility);
                }
            }
        }

        #endregion

        string _mHyperLink = "http://";
        [DataMember]
        public string HyperLink
        {
            get
            {
                return _mHyperLink;
            }
            set
            {
                if (_mHyperLink != value)
                {
                    _mHyperLink = value;
                    OnPropertyChanged(GroupableConstants.HyperLink);
                }
            }
        }

        protected override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            switch (name)
            {
                case GroupableConstants.RotateAngle:
                    OnAngleChanged();
                    break;
                case GroupableConstants.Bold:
                    OnBoldChanged();
                    break;
                case GroupableConstants.DashDot:
                    OnDashDotChanged();
                    break;
                //case GroupableConstants.Fill:
                //    OnFillChanged();
                //    break;
                case GroupableConstants.Font:
                    OnFontChanged();
                    break;
                case GroupableConstants.FontSize:
                    OnFontSizeChanged();
                    break;
                case GroupableConstants.FontStyle:
                    OnFontStyleChanged();
                    break;
                case GroupableConstants.FontWeight:
                    OnFontWeightChanged();
                    break;
                case GroupableConstants.UnitHeight:
                    OnHeightChanged();
                    break;
                case GroupableConstants.Italic:
                    OnItalicChanged();
                    break;
                case GroupableConstants.Label:
                    OnLabelChanged();
                    break;
                case GroupableConstants.LabelTextWrapping:
                    OnLabelTextWrappingChanged();
                    break;
                case GroupableConstants.TextAlignment:
                    OnTextAlignmentChanged();
                    break;
                case GroupableConstants.LabelHAlign:
                    OnLabelHAlignChanged();
                    break;
                case GroupableConstants.LabelMargin:
                    OnLabelMarginChanged();
                    break;
                case GroupableConstants.LabelVAlign:
                    OnLabelVAlignChanged();
                    break;
                case GroupableConstants.LabelX:
                    OnLabelXChanged();
                    break;
                case GroupableConstants.LabelY:
                    OnLabelYChanged();
                    break;
                case GroupableConstants.Name:
                    OnNameChanged();
                    break;
                case GroupableConstants.Stroke:
                    OnStrokeChanged();
                    break;
                case GroupableConstants.Style:
                    OnStyleChanged();
                    break;
                case GroupableConstants.Opacity:
                    OnOpacityChanged();
                    break;
                case GroupableConstants.Thickness:
                    OnThicknessChanged();
                    break;
                case GroupableConstants.Visibility:
                    OnVisibilityChanged();
                    break;
                case GroupableConstants.UnitWidth:
                    OnWidthChanged();
                    break;
                case GroupableConstants.OffsetX:
                    OnXChanged();
                    break;
                case GroupableConstants.OffsetY:
                    OnYChanged();
                    break;
                case GroupableConstants.LabelForeground:
                    OnLabelForegroundChanged();
                    break;
                case GroupableConstants.LabelBackground:
                    OnLabelBackground();
                    break;
                case "Annotations":
                    if (AnnotationsE != null &&
                        AnnotationsE.FirstOrDefault() == null ||
                        (AnnotationsE.FirstOrDefault() != null &&
                        AnnotationsE.FirstOrDefault() != _mAnnotation))
                    {
                        this.Annotations = new List<IAnnotation>() { _mAnnotation };
                    }
                    break;
                case GroupableConstants.HyperLink:
                    OnHyperLinkChanged();
                    break;
                case GroupableConstants.LabelMarginLeft:
                case GroupableConstants.LabelMarginRight:
                case GroupableConstants.LabelMarginTop:
                case GroupableConstants.LabelMarginBottom:
                    OnLabelMarginLeftChanged();
                    break;
            }
        }

        private void OnLabelMarginLeftChanged()
        {
            _mAnnotation.Margin = new Thickness(LabelMarginLeft, LabelMarginTop, LabelMarginRight, LabelMarginBottom);
        }

        private void OnLabelYChanged()
        {
           // double y = this.LabelY;
           // double x = 0;
           // if (LabelX > 1)
            //{
             //   x = LabelX / 10;

          //  }
           // _mAnnotation.Offset = new Point(x, y);
        }

        private void OnLabelTextWrappingChanged()
        {
            _mAnnotation.WrapText = this.LabelTextWrapping;
        }

        private void OnLabelXChanged()
        {
           // double x = this.LabelX;
          //  double y = 0;
          //  if (LabelY > 1)
           // {
           //     y = LabelY / 10;
           // }
           // _mAnnotation.Offset = new Point(x, y);
        }
        private void OnLabelBackground()
        {
            _mAnnotation.LabelBackground = LabelBackground;
        }

        private void OnLabelForegroundChanged()
        {
            _mAnnotation.LabelForeground = LabelForeground;
        }

        private void OnTextAlignmentChanged()
        {
        }

        private void OnVisibilityChanged()
        {
        }

        private void OnNameChanged()
        {
            throw new NotImplementedException();
        }

        #region Label
        private void OnLabelVAlignChanged()
        {
            _mAnnotation.VerticalAlignment = LabelVAlign;
        }

        private void OnLabelMarginChanged()
        {
            _mAnnotation.LabelMargin = LabelMargin;
        }

        private void OnLabelHAlignChanged()
        {
            _mAnnotation.HorizontalAlignment = LabelHAlign;
            switch (LabelHAlign)
            {
                case HorizontalAlignment.Left:
                    _mAnnotation.Alignment = ConnectorAnnotationAlignment.Source;
                    break;
                case HorizontalAlignment.Center:
                    _mAnnotation.Alignment = ConnectorAnnotationAlignment.Center;
                    break;
                case HorizontalAlignment.Right:
                    _mAnnotation.Alignment = ConnectorAnnotationAlignment.Target;
                    break;
            }
        }

        private void OnLabelChanged()
        {
            _mAnnotation.Content = Label;
        }

        private void OnItalicChanged()
        {
            if (Italic)
            {
                FontStyle = FontStyle.Italic;
            }
            else
            {
                FontStyle = FontStyle.Normal;
            }
        }

        private void OnFontStyleChanged()
        {

        }

        private void OnBoldChanged()
        {
            if (Bold)
            {
                FontWeight = FontWeights.Bold;
            }
            else
            {
                FontWeight = FontWeights.Normal;
            }
        }

        private void OnFontWeightChanged()
        {

        }

        private void OnFontSizeChanged()
        {
            _mAnnotation.FontSize = FontSize;
        }

        private void OnFontChanged()
        {
            _mAnnotation.Font = Font;
        }
        #endregion

        private void OnYChanged()
        {
        }

        private void OnXChanged()
        {
        }

        private void OnWidthChanged()
        {
        }

        private void OnHeightChanged()
        {
        }

        private void OnAngleChanged()
        {
        }

        private void OnHyperLinkChanged()
        {
            _mAnnotationHyperLink.HyperLink = HyperLink;
        }
    }

    internal static class GroupableConstants
    {
        public const string AnnotationConstraints = "AnnotationConstraints";
        public const string OffsetX = "OffsetX";
        public const string OffsetY = "OffsetY";
        public const string RotateAngle = "RotateAngle";
        public const string UnitWidth = "UnitWidth";
        public const string UnitHeight = "UnitHeight";
        public const string Stroke = "Stroke";
        public const string Thickness = "Thickness";
        public const string DashDot = "DashDot";
        public const string DashDots = "DashDots";
        public const string Opacity = "Opacity";
        public const string Style = "Style";
        public const string Label = "Label";
        public const string Fonts = "Fonts";
        public const string Font = "Font";
        public const string FontSize = "FontSize";
        public const string FontWeight = "FontWeight";
        public const string FontStyle = "FontStyle";
        public const string Bold = "Bold";
        public const string Italic = "Italic";

        public const string TextLeft = "TextLeft";
        public const string TextCenter = "TextCenter";
        public const string TextRight = "TextRight";
        public const string TextAlignment = "TextAlignment";

        public const string TopLeft = "TopLeft";
        public const string Top = "Top";
        public const string TopRight = "TopRight";

        public const string Left = "Left";
        public const string Center = "Center";
        public const string Right = "Right";

        public const string BottomLeft = "BottomLeft";
        public const string Bottom = "Bottom";
        public const string BottomRight = "BottomRight";

        public const string LabelHAlign = "LabelHAlign";
        public const string LabelVAlign = "LabelVAlign";
        public const string LabelMargin = "LabelMargin";
        public const string Name = "Name";
        public const string Visibility = "Visibility";
        public const string LabelForeground = "LabelForeground";
        public const string LabelBackground = "LabelBackground";
        public const string HyperLink = "HyperLink";

        public const string LabelX = "LabelX";
        public const string LabelY = "LabelY";
        public const string LabelTextWrapping = "LabelTextWrapping";
        public const string LabelMarginLeft = "LabelMarginLeft";
        public const string LabelMarginRight = "LabelMarginRight";
        public const string LabelMarginTop = "LabelMarginTop";
        public const string LabelMarginBottom = "LabelMarginBottom";
    }

    public interface IGroupableVM : INotifyPropertyChanged
    {
        Brush Stroke { get; set; }
        double Thickness { get; set; }
        string DashDot { get; set; }
        Style Style { get; set; }
        double Opacity { get; set; }

        string Label { get; set; }
        FontFamily Font { get; set; }
        int FontSize { get; set; }
        FontWeight FontWeight { get; set; }
        FontStyle FontStyle { get; set; }
        bool Bold { get; set; }
        bool Italic { get; set; }
        TextAlignment TextAlignment { get; set; }
        HorizontalAlignment LabelHAlign { get; set; }
        VerticalAlignment LabelVAlign { get; set; }
        Thickness LabelMargin { get; set; }
        Brush LabelForeground { get; set; }
        Brush LabelBackground { get; set; }

        string Name { get; set; }
        Visibility Visibility { get; set; }
        string HyperLink { get; set; }
        //double LabelX { get; set; }
        //double LabelY { get; set; }
        TextWrapping LabelTextWrapping { get; set; }
        double LabelMarginBottom { get; set; }

        double LabelMarginTop { get; set; }
        double LabelMarginLeft { get; set; }
        double LabelMarginRight { get; set; }
    }

    public class LabelVM : AnnotationEditorViewModel
    {
        FontFamily _mFont = DiagramBuilderVM.Fonts[0];
        FontWeight _mFontWeight = FontWeights.Normal;
        private FontStyle _mFontStyle = FontStyle.Normal;
        private TextAlignment _textAlignment;

        public LabelVM()
        {

            const string vTemplate = "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                                     "<Border Padding=\"0\" BorderThickness=\"0\" Background=\"{Binding Path=LabelBackground}\">" +
                                     "<TextBlock TextAlignment=\"{Binding Path=TextHorizontalAlignment}\"" +
                                                " FontFamily=\"{Binding Path=Font}\"" +
                                                " FontWeight=\"{Binding Path=FontWeight}\"" +
                                                " FontStyle=\"{Binding Path=FontStyle}\"" +
                                                " TextWrapping=\"{Binding Path=WrapText}\"" +
                                                " FontSize=\"{Binding Path=FontSize}\"" +
                                                " Margin=\"{Binding Path=LabelMargin}\"" +
                                                " Foreground=\"{Binding Path=LabelForeground}\"" +
                                                " Text=\"{Binding Path=Content, Mode=TwoWay,UpdateSourceTrigger=PropertyChanged}\"/>" +
                                     "</Border>" +
                                     "</DataTemplate>";

            const string eTemplate = "<DataTemplate" +
                    " xmlns:util=\"using:Syncfusion.UI.Xaml.Diagram.Utility\"" +
                    " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                    "<TextBox util:FocusUtility.FocusOnLoad=\"True\" Padding=\"0,0,0,0\" AcceptsReturn=\"True\" Text=\"{Binding Path=Content, Mode=TwoWay,UpdateSourceTrigger=PropertyChanged}\"  TextWrapping=\"{Binding Path=WrapText, Mode=TwoWay,UpdateSourceTrigger=PropertyChanged}\"/>" +
                    "</DataTemplate>";

            ViewTemplate = XamlReader.Load(vTemplate) as DataTemplate;
            EditTemplate = XamlReader.Load(eTemplate) as DataTemplate;
        }

        public LabelVM(string hyperlink)
        {
            const string vTemplate = "<DataTemplate" +
                                    " xmlns:viewModel=\"using:DiagramBuilder.ViewModel\"" +
                                    " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                                    "<Border Padding=\"0\" BorderThickness=\"0\" Background=\"{Binding Path=LabelBackground}\">" +
                                    "<viewModel:CustomHyperlinkButton" +
                                               " FontFamily=\"{Binding Path=Font}\"" +
                                               " FontWeight=\"{Binding Path=FontWeight}\"" +
                                               " FontStyle=\"{Binding Path=FontStyle}\"" +
                                               " FontSize=\"{Binding Path=FontSize}\"" +
                                               " Margin=\"{Binding Path=LabelMargin}\"" +
                                               " Foreground=\"{Binding Path=LabelForeground}\"" +
                                               " Content=\"{Binding Path=HyperLink, Mode=TwoWay}\"/>" +
                                    "</Border>" +
                                    "</DataTemplate>";

            const string eTemplate = "<DataTemplate" +
                                     " xmlns:util=\"using:Syncfusion.UI.Xaml.Diagram.Utility\"" +
                                     " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                                      "<TextBox util:FocusUtility.FocusOnLoad=\"True\" ManipulationMode=\"None\"" +
                                                " FontFamily=\"{Binding Path=Font}\"" +
                                                " FontWeight=\"{Binding Path=FontWeight}\"" +
                                                " FontStyle=\"{Binding Path=FontStyle}\"" +
                                                " TextAlignment=\"{Binding Path=TextAlignment}\" AcceptsReturn=\"True\"" +
                                                " FontSize=\"{Binding Path=FontSize}\"" +
                                                " Margin=\"{Binding Path=LabelMargin}\"" +
                                                " Foreground=\"{Binding Path=LabelForeground}\"" +
                                                " Background=\"{Binding Path=LabelBackground}\"" +
                                                " Text=\"{Binding Path=Content, Mode=TwoWay}\"/>" +
                                      "</DataTemplate>";
            ViewTemplate = XamlReader.Load(vTemplate) as DataTemplate;
            EditTemplate = XamlReader.Load(eTemplate) as DataTemplate;
            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;
        }

        public FontFamily Font
        {
            get
            {
                return _mFont;
            }
            set
            {
                //if (_mFontWeight != value)
                {
                    _mFont = value;
                    OnPropertyChanged(GroupableConstants.Font);
                }
            }
        }

        public FontWeight FontWeight
        {
            get
            {
                return _mFontWeight;
            }
            set
            {
                //if (_mFontWeight != value)
                {
                    _mFontWeight = value;
                    OnPropertyChanged(GroupableConstants.FontWeight);
                }
            }
        }

        public FontStyle FontStyle
        {
            get
            {
                return _mFontStyle;
            }
            set
            {
                if (_mFontStyle != value)
                {
                    _mFontStyle = value;
                    OnPropertyChanged(GroupableConstants.FontStyle);
                }
            }
        }

        int _mFontSize = 14;
        public int FontSize
        {
            get
            {
                return _mFontSize;
            }
            set
            {
                if (_mFontSize != value)
                {
                    _mFontSize = value;
                    OnPropertyChanged(GroupableConstants.FontSize);
                }
            }
        }

        public TextAlignment TextAlignment
        {
            get { return _textAlignment; }
            set
            {
                if (_textAlignment != value)
                {
                    _textAlignment = value;
                    OnPropertyChanged("TextAlignment");
                }
            }
        }

        Thickness _mLabelMargin = new Thickness(5);
        public Thickness LabelMargin
        {
            get
            {
                return _mLabelMargin;
            }
            set
            {
                if (_mLabelMargin != value)
                {
                    _mLabelMargin = value;
                    OnPropertyChanged(GroupableConstants.LabelMargin);
                }
            }
        }


        private Brush _mLabelBackground = new SolidColorBrush(Colors.Transparent);
        public Brush LabelBackground
        {
            get { return _mLabelBackground; }
            set
            {
                if (_mLabelBackground != value)
                {
                    _mLabelBackground = value;
                    OnPropertyChanged(GroupableConstants.LabelBackground);
                }
            }
        }

        private Brush _mLabelForeground = new SolidColorBrush(Colors.Black);
        public Brush LabelForeground
        {
            get { return _mLabelForeground; }
            set
            {
                if (_mLabelForeground != value)
                {
                    _mLabelForeground = value;
                    OnPropertyChanged(GroupableConstants.LabelForeground);
                }
            }
        }

        private string _mHyperLink = string.Empty;
        public string HyperLink
        {
            get
            {
                return _mHyperLink;
            }
            set
            {
                if (_mHyperLink != value)
                {
                    _mHyperLink = value;
                    OnPropertyChanged(GroupableConstants.HyperLink);
                }
            }
        }
    }

    public class CustomHyperlinkButton : HyperlinkButton
    {
        public CustomHyperlinkButton()
        {
            this.Click += CustomHyperlinkButton_Click;
        }

        async void CustomHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = (sender as HyperlinkButton).Content.ToString();
            if (!uriToLaunch.Contains("http://")) { uriToLaunch = "http://" + uriToLaunch; }
            var uri = new Uri(uriToLaunch);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
