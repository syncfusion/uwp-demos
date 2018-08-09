using Syncfusion.UI.Xaml.Controls.SfRibbon;
using Syncfusion.UI.Xaml.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RibbonSample
{





    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.DataContext = this;
            this.InitializeComponent();
            this.mainbackstage.SelectedItem = new UserView();
            string[] Fonts = {
    "Arial", "Calibri", "Cambria", "Cambria Math", "Comic Sans MS", "Courier New",
    "Ebrima", "Gadugi", "Georgia",
     "Leelawadee UI",
    "Lucida Console", "Malgun Gothic", "Microsoft Himalaya", "Microsoft JhengHei",
    "Microsoft JhengHei UI", "Microsoft New Tai Lue", "Microsoft PhagsPa",
    "Microsoft Tai Le", "Microsoft YaHei", "Microsoft YaHei UI",
    "Microsoft Yi Baiti", "Mongolian Baiti", "MV Boli", "Myanmar Text",
    "Nirmala UI", "Segoe MDL2 Assets", "Segoe Print", "Segoe UI", "Segoe UI Emoji",
    "Segoe UI Historic", "Segoe UI Symbol", "SimSun", "Times New Roman",
    "Trebuchet MS", "Verdana", "Webdings", "Wingdings", "Yu Gothic",
    "Yu Gothic UI"
};

            InstalledFonts = new ObservableCollection<string>();
            foreach (var item in Fonts)
            {
                InstalledFonts.Add(item);
            }

            int[] size = { 8, 9, 10, 11, 12, 13, 14, 15, 16, 18, 20, 22, 24, 26, 28, 32, 74 };
            FontsSize = new ObservableCollection<int>();
            foreach (var item in size)
            {
                FontsSize.Add(item);
            }

            string[] fontColors = { "#FFFFFFFF", "#FFEF4532", "#FF123456", "#FFADADAD", "#FFACDEFD", "#FFEF4532", "#FF123456", "#FFADADAD", "#FFACDEFD", "#FFEF4532", "#FF123456", "#FFADADAD", "#FFACDEFD", "#FFEF4532", "#FF123456", "#FFADADAD", "#FFACDEFD", "#FFEF4532", "#FF123456", "#FFADADAD", "#FFACDEFD", "#FFEF4532", "#FF123456", "#FFADADAD", "#FFACDEFD", "#FFEF4532", "#FF123456", "#FFADADAD", "#FFACDEFD", "#FFEF4532", "#FF123456", "#FFADADAD", "#FFACDEFD" };

            CollectColors();

            pictures = new ObservableCollection<string>();

            for (int i = 1; i <= 16; i++)
            {
                pictures.Add("ms-appx:///Assets/Pictures/" + i.ToString() + ".png");
            }

            _ExcuteCommand = new DelegateCommand(CommandExcuted);

             description = @" Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula. Lorem tortor neque, purus taciti quis id. Elementum integer orci accumsan minim phasellus vel.";
            description += description;
            description += description;
            this.RichTextContent.Document.SetText(Windows.UI.Text.TextSetOptions.None, description);
            var deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
            if(deviceFamily != "Windows.Desktop")
            MobileSettings();
        }

        public void MobileSettings()
        {
            this.fontdropdown.FontIcon = "?";
            this.paradropdown.FontIcon = "%";
            this.tablebutton.FontIcon = "t";

        }

        string description;
        private async void CommandExcuted(object param)
        {
            if (param.ToString() == "New")
            {
                RichTextContent.Document.SetText(Windows.UI.Text.TextSetOptions.None, string.Empty);
            }
            else if (param.ToString() == "Cut")
            {
                RichTextContent.Document.Selection.Cut();
            }
            else if (param.ToString() == "Copy")
            {
                RichTextContent.Document.Selection.Copy();
            }
            else if (param.ToString() == "Paste")
            {
                RichTextContent.Document.Selection.Paste(1);
            }
            else if (param.ToString() == "Format")
            {
                RichTextContent.Document.Selection.CharacterFormat.Bold = FormatEffect.On;
                RichTextContent.Document.Selection.CharacterFormat.Italic = FormatEffect.On;
                RichTextContent.Document.Selection.CharacterFormat.Underline = UnderlineType.Single;
            }
            else if (param.ToString() == "Bold")
            {
                RichTextContent.Document.Selection.CharacterFormat.Bold = FormatEffect.Toggle;
            }
            else if (param.ToString() == "Italic")
            {
                RichTextContent.Document.Selection.CharacterFormat.Italic = FormatEffect.Toggle;
            }
            else if (param.ToString() == "Underline")
            {
                RichTextContent.Document.Selection.CharacterFormat.Underline = UnderlineType.Single;
            }
            else if (param.ToString() == "Strike")
            {

                    RichTextContent.Document.Selection.CharacterFormat.Strikethrough = FormatEffect.Toggle;
            }
            else if (param.ToString() == "TextDecor")
            {
                RichTextContent.Document.Selection.CharacterFormat.Subscript = FormatEffect.Toggle;
            }
            else if (param.ToString() == "Delete")
            {
                RichTextContent.Document.Selection.CharacterFormat.Hidden = FormatEffect.Toggle;
            }
            else if (param.ToString() == "Left")
            {
                RichTextContent.TextAlignment = TextAlignment.Left;
            }
            else if (param.ToString() == "Right")
            {
                RichTextContent.TextAlignment = TextAlignment.Right;
            }
            else if (param.ToString() == "Center")
            {
                RichTextContent.TextAlignment = TextAlignment.Center;
            }
            else if (param.ToString() == "Justify")
            {
                RichTextContent.TextAlignment = TextAlignment.Justify;
            }
            else if (param.ToString() == "Bullets")
            {
                if(RichTextContent.Document.Selection.ParagraphFormat.ListType == Windows.UI.Text.MarkerType.None)
                    RichTextContent.Document.Selection.ParagraphFormat.ListType = Windows.UI.Text.MarkerType.Bullet;
                else
                    RichTextContent.Document.Selection.ParagraphFormat.ListType = Windows.UI.Text.MarkerType.None;
    
            }
            else if (param.ToString() == "Number")
            {
                if (RichTextContent.Document.Selection.ParagraphFormat.ListType == Windows.UI.Text.MarkerType.None)
                    RichTextContent.Document.Selection.ParagraphFormat.ListType = Windows.UI.Text.MarkerType.CircledNumber;
                else
                    RichTextContent.Document.Selection.ParagraphFormat.ListType = Windows.UI.Text.MarkerType.None;

            }
            else if (param.ToString() != string.Empty)
            {
                var dialog = new MessageDialog( param.ToString() +" command executed");
                await dialog.ShowAsync();
            }
        }

        private ObservableCollection<string> pictures;

        public ObservableCollection<string> Pictures
        {
            get { return pictures; }
            set { pictures = value; }
        }

       

    private void CollectColors()
        {
            this.FontColors = new ObservableCollection<SolidColorBrush>();
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.AliceBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.AntiqueWhite));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Aqua));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Aquamarine));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Azure));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Beige));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Bisque));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Black));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.BlanchedAlmond));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Blue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.BlueViolet));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Brown));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.BurlyWood));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.CadetBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Chartreuse));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Chocolate));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Coral));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.CornflowerBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Cornsilk));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Crimson));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Cyan));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkCyan));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkGoldenrod));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkGray));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkKhaki));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkMagenta));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkOliveGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkOrange));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkOrchid));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkRed));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkSalmon));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkSeaGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkSlateBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkSlateGray));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkTurquoise));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DarkViolet));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DeepPink));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DeepSkyBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DimGray));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.DodgerBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Firebrick));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.FloralWhite));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.ForestGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Fuchsia));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Gainsboro));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.GhostWhite));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Gold));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Goldenrod));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Gray));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Green));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.GreenYellow));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Honeydew));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.HotPink));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.IndianRed));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Indigo));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Ivory));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Khaki));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Lavender));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LavenderBlush));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LawnGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LemonChiffon));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightCoral));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightCyan));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightGoldenrodYellow));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightGray));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightPink));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightSalmon));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightSeaGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightSkyBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightSlateGray));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightSteelBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LightYellow));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Lime));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.LimeGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Linen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Magenta));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Maroon));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.MediumAquamarine));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.MediumBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.MediumOrchid));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.MediumPurple));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.MediumSeaGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.MediumSlateBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.MediumSpringGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.MediumTurquoise));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.MediumVioletRed));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.MidnightBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.MintCream));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.MistyRose));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Moccasin));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.NavajoWhite));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Navy));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.OldLace));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Olive));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.OliveDrab));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Orange));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Orchid));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.PaleGoldenrod));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.PaleGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.PaleTurquoise));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.PaleVioletRed));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.PapayaWhip));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.PeachPuff));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Peru));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Pink));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Plum));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.PowderBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Purple));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Red));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.RosyBrown));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.RoyalBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.SaddleBrown));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Salmon));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.SandyBrown));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.SeaGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.SeaShell));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Sienna));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Silver));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.SkyBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.SlateBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.SlateGray));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Snow));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.SpringGreen));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.SteelBlue));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Tan));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Teal));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Thistle));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Tomato));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Transparent));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Turquoise));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Violet));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Wheat));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.White));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.WhiteSmoke));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.Yellow));
            this.FontColors.Add(new SolidColorBrush(Windows.UI.Colors.YellowGreen));
        }

        public ObservableCollection<SolidColorBrush> FontColors
        {
            get { return (ObservableCollection<SolidColorBrush>)GetValue(FontColorsProperty); }
            set { SetValue(FontColorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontColorsProperty =
            DependencyProperty.Register("FontColors", typeof(ObservableCollection<SolidColorBrush>), typeof(MainPage), new PropertyMetadata(null));



        public ObservableCollection<int> FontsSize
        {
            get { return (ObservableCollection<int>)GetValue(FontsSizeProperty); }
            set { SetValue(FontsSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontsSizeProperty =
            DependencyProperty.Register("FontsSize", typeof(ObservableCollection<int>), typeof(MainPage), new PropertyMetadata(null));



        public ObservableCollection<string> InstalledFonts
        {
            get { return (ObservableCollection<string>)GetValue(InstalledFontsProperty); }
            set { SetValue(InstalledFontsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InstalledFontsProperty =
            DependencyProperty.Register("InstalledFonts", typeof(ObservableCollection<string>), typeof(MainPage), new PropertyMetadata(null));

        private void Redbutton_Click(object sender, RoutedEventArgs e)
        {
            this.pencilbutton.Foreground = this.budgetbutton.Foreground = this.brushbutton.Foreground = (sender as SfRibbonToggleButton).Foreground;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.pencilbutton.Foreground = this.budgetbutton.Foreground = this.brushbutton.Foreground = (sender as Button).Background;
        }

        private void displaytextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.addresstextbox.Text == string.Empty && this.displaytextbox.Text == string.Empty)
                this.linknsertbutton.IsEnabled = false;
            else
                this.linknsertbutton.IsEnabled = true;
        }

        private void findtextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.findtextbox.Text == string.Empty && this.replacetextbox.Text == string.Empty)
                this.findbutton.IsEnabled =  this.replacebutton.IsEnabled = this.replaceallbutton.IsEnabled= false;
            else
                this.findbutton.IsEnabled = this.replacebutton.IsEnabled = this.replaceallbutton.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.mainribbon.AccentBrush = (sender as Button).Background;
        }

        private void collapsebutton_Click(object sender, RoutedEventArgs e)
        {
            this.mainribbon.RibbonState = RibbonState.Hide;
            
        }

        private void openbackstagebutton_Click(object sender, RoutedEventArgs e)
        {
            this.mainribbon.OpenBackStage();
        }

        private void TextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            this.mainribbon.Title = (sender as TextBox).Text;
        }

        private async void SfBackStageButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Print command executed");
            await dialog.ShowAsync();


        }

        private async void SfBackStageButton_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula. Lorem tortor neque, purus taciti quis id. Elementum integer orci accumsan minim phasellus vel.");
            await dialog.ShowAsync();
        }

        private void SfBackStageButton_Click_2(object sender, RoutedEventArgs e)
        {
            this.mainribbon.CloseBackStage();
        }

        private void SfBackStageButton_Click_3(object sender, RoutedEventArgs e)
        {
            this.mainribbon.CloseBackStage();
            this.mainribbon.SelectedIndex = 3;
        }

        private ICommand _ExcuteCommand;
        public ICommand ExcuteCommand
        {
            get
            {
                return _ExcuteCommand;
            }
        }


        public Brush SelectedColor { get; set; }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RichTextContent.Document.Selection.CharacterFormat.BackgroundColor =( (sender as Button).Background as SolidColorBrush).Color;
        }

        private void SfRibbonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RichTextContent.FontSize = (int) (sender as SfRibbonComboBox).SelectedItem;
        }

        private void findbutton_Click(object sender, RoutedEventArgs e)
        {
         var gg  =  RichTextContent.Document.Selection.FindText(this.findtextbox.Text, 100, FindOptions.None);

        }

        private async void replacebutton_Click(object sender, RoutedEventArgs e)
        {
            RichTextContent.Document.GetText(TextGetOptions.None,out description);
            description = description.Replace(this.findtextbox.Text, this.replacetextbox.Text);
            RichTextContent.Document.SetText(TextSetOptions.None, description);

            var dialog = new MessageDialog("Replaced Successfully");
            await dialog.ShowAsync();
        }

    } 




}
