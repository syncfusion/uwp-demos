using Common;
using Syncfusion.UI.Xaml.Controls.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SampleBrowser.RadialMenu
{
    public sealed partial class RadialMenuView : SampleLayout
    {
        DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(0.01) };
        //Used to count the undo/operation.
        private int OperationCount = 0;
        public RadialMenuView()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                selectionmenu.RadiusX = selectionmenu.RadiusY = 175;
                selectionmenu.HorizontalAlignment = HorizontalAlignment.Right;
                selectionmenu.VerticalAlignment = VerticalAlignment.Center;
                selectionmenu.Margin = new Thickness(0, -4, -183, 4);
                selectionmenu.Padding = new Thickness(0);
                selectionmenu.EnableFreeRotation = true;
                selectionmenu.Background = new SolidColorBrush(Colors.White);
                selectionmenu.RimBackground = new SolidColorBrush(Colors.DarkGray);
                selectionmenu.RimActiveBrush = new SolidColorBrush(Colors.LightGray);
                selectionmenu.NavigationButtonStyle = Resources["NavigationButtonStyle"] as Style;
                selectionmenu.Items.Clear();
                selectionmenu.Icon = null;
                text.Background = new SolidColorBrush(Colors.White);
                text.SelectionChanged -= text_SelectionChanged_1;
                CreateAndAddItems();
            }
            this.Loaded += RadialMenuDemo_Loaded;
        }

        public override void Dispose()
        {
            Loaded -= RadialMenuDemo_Loaded;

            if(selectionmenu != null)
            {
                selectionmenu.Dispose();
                if (selectionmenu.Items.Count > 0)
                    selectionmenu.Items.Clear();
                selectionmenu = null;
            }

            if(defaultmenu != null)
            {
                defaultmenu.Dispose();
                if (defaultmenu.Items.Count > 0)
                    defaultmenu.Items.Clear();
            }

            timer.Stop();
            timer.Tick -= timer_Tick;
            text.SelectionChanged -= text_SelectionChanged_1;
            FontsizeSlider.ValueChanged -= RadialSlider_Value_Changed;
            foreach (var item in colors.Items)
            {
                SfRadialColorItem coloritem = item as SfRadialColorItem;
                if (coloritem != null)
                {
                    coloritem.Click -= Color;
                    if (coloritem.HasItems)
                    {
                        foreach (var inneritem in colors.Items)
                        {
                            SfRadialColorItem innercoloritem = inneritem as SfRadialColorItem;
                            if (innercoloritem != null)
                            {
                                innercoloritem.Click -= Color;
                            }
                        }
                    }
                }
            }

            GC.Collect();
        }

        string description = string.Empty;

        void RadialMenuDemo_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= RadialMenuDemo_Loaded;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                description = @"Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.
Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.
Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula.
Lorem tortor neque, purus taciti quis id. Elementum integer orci accumsan minim phasellus vel.

Vestibulum duis integer diam mi libero felis, sollicitudin id dictum etiam blandit lacus, ac condimentum magna dictumst interdum et,
nam commodo mi habitasse enim fringilla nunc, amet aliquam sapien per tortor luctus. Conubia voluptates at nunc, congue lectus, malesuada nulla.
Rutrum quo morbi, feugiat sed mi turpis, ac cursus integer ornare dolor. Purus dui in et tincidunt, sed eros pede adipiscing tellus, est suscipit nulla,
arcu nec fringilla vel aliquam, mollis lorem rerum hac vestibulum ante nullam. Volutpat a lectus, lorem pulvinar quis. Lobortis vehicula in imperdiet orci urna.

Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.
Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.
Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula.
Lorem tortor neque, purus taciti quis id. Elementum integer orci accumsan minim phasellus vel.

Vestibulum duis integer diam mi libero felis, sollicitudin id dictum etiam blandit lacus, ac condimentum magna dictumst interdum et,
nam commodo mi habitasse enim fringilla nunc, amet aliquam sapien per tortor luctus. Conubia voluptates at nunc, congue lectus, malesuada nulla.
Rutrum quo morbi, feugiat sed mi turpis, ac cursus integer ornare dolor. Purus dui in et tincidunt, sed eros pede adipiscing tellus, est suscipit nulla,
arcu nec fringilla vel aliquam, mollis lorem rerum hac vestibulum ante nullam. Volutpat a lectus, lorem pulvinar quis. Lobortis vehicula in imperdiet orci urna.

";
                selectionmenu.IsOpen = true;
            }
            else
            {
                description = @"Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.
Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.
Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula.
Lorem tortor neque, purus taciti quis id. Elementum integer orci accumsan minim phasellus vel.

Vestibulum duis integer diam mi libero felis, sollicitudin id dictum etiam blandit lacus, ac condimentum magna dictumst interdum et,
nam commodo mi habitasse enim fringilla nunc, amet aliquam sapien per tortor luctus. Conubia voluptates at nunc, congue lectus, malesuada nulla.
Rutrum quo morbi, feugiat sed mi turpis, ac cursus integer ornare dolor. Purus dui in et tincidunt, sed eros pede adipiscing tellus, est suscipit nulla,
arcu nec fringilla vel aliquam, mollis lorem rerum hac vestibulum ante nullam. Volutpat a lectus, lorem pulvinar quis. Lobortis vehicula in imperdiet orci urna.
";
                timer.Tick += timer_Tick;
                timer.Start();
            }

            using (MemoryStream ms = new MemoryStream())
            {
                UnicodeEncoding uniEncoding = new UnicodeEncoding();
                var sw = new StreamWriter(ms, uniEncoding);
                sw.Write(description);
                sw.Flush();
                ms.Seek(0, SeekOrigin.Begin);

                text.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.None, ms.AsRandomAccessStream());
            }

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                text.MaxWidth = (Window.Current.Content as FrameworkElement).ActualWidth;
                text.TextWrapping = TextWrapping.Wrap;
                text.Padding = new Thickness(10);
                text.Margin = new Thickness(0);
                text.IsReadOnly = true;
            }
            OperationCount++;
            text.Document.Selection.SetRange(0, 10);
        }

        void timer_Tick(object sender, object e)
        {
            timer.Stop();
            timer.Tick -= timer_Tick;
            selectionmenu.IsOpen = true;
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            var range = text.Document.GetRange(text.Document.Selection.StartPosition, text.Document.Selection.EndPosition);
            range.Cut();
            OperationCount++;
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            var range = text.Document.GetRange(text.Document.Selection.StartPosition, text.Document.Selection.EndPosition);
            range.Copy();
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            var range = text.Document.GetRange(text.Document.Selection.StartPosition, text.Document.Selection.EndPosition);
            if (range != null && Clipboard.GetContent() != null && Clipboard.GetContent().AvailableFormats != null && Clipboard.GetContent().AvailableFormats.Contains("Text"))
            {
                range.Paste(0);
                OperationCount++;
            }
        }

        private void Bold(object sender, RoutedEventArgs e)
        {
            var range = text.Document.GetRange(text.Document.Selection.StartPosition, text.Document.Selection.EndPosition);
            if (range != null)
            {
                if (!(sender as SfRadialMenuItem).IsChecked)
                    range.CharacterFormat.Bold = Windows.UI.Text.FormatEffect.Toggle;
                else
                    range.CharacterFormat.Bold = Windows.UI.Text.FormatEffect.Off;
            }
            OperationCount++;
        }

        private void Color(object sender, RoutedEventArgs e)
        {
            var range = text.Document.GetRange(text.Document.Selection.StartPosition, text.Document.Selection.EndPosition);
            range.CharacterFormat.ForegroundColor = (sender as SfRadialColorItem).Color;
            OperationCount++;
        }
        private void RadialSlider_Value_Changed(object sender, RoutedEventArgs e)
        {
            if (text != null)
            {
                var range = text.Document.GetRange(text.Document.Selection.StartPosition, text.Document.Selection.EndPosition);
                if (range != null && (float)(sender as SfRadialSlider).Value >= 5)
                {
                    range.CharacterFormat.Size = ((float)(sender as SfRadialSlider).Value);
                    OperationCount++;
                }
            }
        }
        private void Undo(object sender, RoutedEventArgs e)
        {           
            text.Document.Undo();
            OperationCount--;
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            if (text.Document.CanRedo())
            {
                text.Document.Redo();
                OperationCount++;
            }
        }

        private void Italic(object sender, RoutedEventArgs e)
        {
            var range = text.Document.GetRange(text.Document.Selection.StartPosition, text.Document.Selection.EndPosition);
            if (range != null)
            {
                if (!(sender as SfRadialMenuItem).IsChecked)
                    range.CharacterFormat.Italic = Windows.UI.Text.FormatEffect.Toggle;
                else
                    range.CharacterFormat.Italic = Windows.UI.Text.FormatEffect.Off;
            }
            OperationCount++;
        }

        private void Strike(object sender, RoutedEventArgs e)
        {
            var range = text.Document.GetRange(text.Document.Selection.StartPosition, text.Document.Selection.EndPosition);
            if (range != null)
            {
                if (!(sender as SfRadialMenuItem).IsChecked)
                    range.CharacterFormat.Strikethrough = Windows.UI.Text.FormatEffect.Toggle;
                else
                    range.CharacterFormat.Strikethrough = Windows.UI.Text.FormatEffect.Off;
            }
            OperationCount++;
        }

        private void superScript(object sender, RoutedEventArgs e)
        {
            var range = text.Document.GetRange(text.Document.Selection.StartPosition, text.Document.Selection.EndPosition);
            if (range != null)
            {
                if (!(sender as SfRadialMenuItem).IsChecked)
                    range.CharacterFormat.Superscript = Windows.UI.Text.FormatEffect.Toggle;
                else
                    range.CharacterFormat.Superscript = Windows.UI.Text.FormatEffect.Off;
            }
            OperationCount++;
        }

        private void SubScript(object sender, RoutedEventArgs e)
        {
            var range = text.Document.GetRange(text.Document.Selection.StartPosition, text.Document.Selection.EndPosition);
            if (range != null)
            {
                if (!(sender as SfRadialMenuItem).IsChecked)
                    range.CharacterFormat.Subscript = Windows.UI.Text.FormatEffect.Toggle;
                else
                    range.CharacterFormat.Subscript = Windows.UI.Text.FormatEffect.Off;
            }
            OperationCount++;
        }

        private void Underline(object sender, RoutedEventArgs e)
        {
            var range = text.Document.GetRange(text.Document.Selection.StartPosition, text.Document.Selection.EndPosition);
            if (range != null)
            {
                if (range.CharacterFormat.Underline == Windows.UI.Text.UnderlineType.Single)
                    range.CharacterFormat.Underline = Windows.UI.Text.UnderlineType.None;
                else
                    range.CharacterFormat.Underline = Windows.UI.Text.UnderlineType.Single;
            }
            OperationCount++;
        }

        private void text_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            Point rect = new Point();
            text.Document.Selection.GetPoint(Windows.UI.Text.HorizontalCharacterAlignment.Left, Windows.UI.Text.VerticalCharacterAlignment.Top, Windows.UI.Text.PointOptions.NoHorizontalScroll, out rect);

            if (String.IsNullOrEmpty(text.Document.Selection.Text))
            {
                double left = text.ActualWidth - 200;
                double top = text.ActualHeight - 200;
                if (rect.X > left)
                {
                    rect.X = Math.Abs(left - (rect.X - left));
                }
                transform1.TranslateX = transform2.TranslateX = rect.X - 50;

                if (rect.Y > top)
                {
                    rect.Y = Math.Abs(top - (rect.Y - top));
                }

                transform1.TranslateY = transform2.TranslateY = rect.Y - 100;
                defaultmenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
                selectionmenu.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                selectionmenu.IsOpen = false;
            }
            else
            {
                defaultmenu.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                defaultmenu.IsOpen = false;
                selectionmenu.Visibility = Windows.UI.Xaml.Visibility.Visible;

            }
        }

        private void CreateAndAddItems()
        {
            SfRadialMenuItem Cut, Copy, Paste, Undo, Redo, Bold, Underline, Italic;
            TransformGroup transformGroup = new TransformGroup();
            RotateTransform rotateTransform = new RotateTransform() { Angle = 3 };
            ScaleTransform scaleTransform = new ScaleTransform() { ScaleX = 1, ScaleY = 1 };
            transformGroup.Children.Add(rotateTransform);
            transformGroup.Children.Add(scaleTransform);
            Bold = new SfRadialMenuItem();
            Grid BoldHeader = new Grid() { Height = 75, Background = new SolidColorBrush(Colors.Transparent) };
            Viewbox BoldViewbox = new Viewbox() { Margin = new Thickness(18) };
            Windows.UI.Xaml.Shapes.Path BoldPath = new Windows.UI.Xaml.Shapes.Path()
            {
                Stretch = Stretch.Uniform,
                Height = 26,
                Width = 26,
                Fill = new SolidColorBrush(Colors.Black),
                RenderTransformOrigin = new Point(0.5, 0.5),
                Data = PathXamlToGeometry("F1M-240.817,-5140.75L-215.236,-5140.75C-210.17,-5140.75 -206.394,-5140.54 -203.906,-5140.12 -201.417,-5139.7 -199.189,-5138.82 -197.225,-5137.48 -195.261,-5136.14 -193.624,-5134.36 -192.314,-5132.13 -191.003,-5129.9 -190.351,-5127.41 -190.351,-5124.64 -190.351,-5121.65 -191.158,-5118.9 -192.771,-5116.39 -194.389,-5113.89 -196.579,-5112.01 -199.342,-5110.76 -195.443,-5109.62 -192.446,-5107.69 -190.351,-5104.95 -188.253,-5102.22 -187.208,-5099 -187.208,-5095.3 -187.208,-5092.39 -187.883,-5089.56 -189.236,-5086.81 -190.589,-5084.06 -192.439,-5081.86 -194.781,-5080.22 -197.124,-5078.58 -200.014,-5077.56 -203.446,-5077.19 -205.6,-5076.95 -210.796,-5076.81 -219.032,-5076.75L-240.817,-5076.75 -240.817,-5140.75z M-227.897,-5130.1L-227.897,-5115.3 -219.426,-5115.3C-214.391,-5115.3 -211.264,-5115.37 -210.041,-5115.52 -207.829,-5115.78 -206.088,-5116.54 -204.822,-5117.81 -203.557,-5119.07 -202.923,-5120.74 -202.923,-5122.81 -202.923,-5124.79 -203.469,-5126.39 -204.561,-5127.63 -205.652,-5128.87 -207.275,-5129.62 -209.426,-5129.88 -210.708,-5130.03 -214.391,-5130.1 -220.473,-5130.1L-227.897,-5130.1z M-227.897,-5104.65L-227.897,-5087.53 -215.933,-5087.53C-211.277,-5087.53 -208.324,-5087.67 -207.071,-5087.93 -205.15,-5088.28 -203.587,-5089.13 -202.377,-5090.48 -201.17,-5091.83 -200.564,-5093.65 -200.564,-5095.92 -200.564,-5097.84 -201.032,-5099.47 -201.964,-5100.81 -202.895,-5102.14 -204.24,-5103.12 -205.999,-5103.73 -207.761,-5104.34 -211.583,-5104.65 -217.46,-5104.65L-227.897,-5104.65z")
            };
            BoldPath.RenderTransform = transformGroup;
            BoldViewbox.Child = BoldPath;
            BoldHeader.Children.Add(BoldViewbox);
            Bold.Header = BoldHeader;
            Bold.Click += WPBold;
            Underline = new SfRadialMenuItem();
            Grid UnderlineHeader = new Grid() { Height = 75, Background = new SolidColorBrush(Colors.Transparent) };
            Viewbox UnderlineViewbox = new Viewbox() { Margin = new Thickness(18) };
            Windows.UI.Xaml.Shapes.Path UnderlinePath = new Windows.UI.Xaml.Shapes.Path()
            {
                Stretch = Stretch.Uniform,
                Height = 26,
                Width = 26,
                Fill = new SolidColorBrush(Colors.Black),
                RenderTransformOrigin = new Point(0.5, 0.5),
                Data = PathXamlToGeometry("M7.96874952316284,36.3745803833008L15.84375,36.3745803833008 15.84375,76.9683303833008 16.1162109375,81.8133010864258 16.93359375,86.0122756958008 18.2958984375,89.5652542114258 20.203125,92.4722366333008 22.6552734375,94.7332229614258 25.65234375,96.3482131958008 29.1943359375,97.3172073364258 33.28125,97.6402053833008 37.225341796875,97.3281936645508 40.6435508728027,96.3921585083008 43.5358848571777,94.8320999145508 45.9023399353027,92.6480178833008 47.7429161071777,89.8399124145508 49.0576133728027,86.4077835083008 49.846435546875,82.3516311645508 50.109375,77.6714553833008 50.109375,36.3745803833008 57.984375,36.3745803833008 57.984375,76.4058303833008 57.585205078125,83.0415725708008 56.3876914978027,88.7925491333008 54.3918418884277,93.6587600708008 51.5976524353027,97.6402053833008 48.0051231384277,100.736885070801 43.6142539978027,102.948799133301 38.425048828125,104.275947570801 32.4375,104.718330383301 26.70263671875,104.292793273926 21.732421875,103.016181945801 17.52685546875,100.888496398926 14.0859375,97.9097366333008 11.40966796875,94.0799026489258 9.498046875,89.3989944458008 8.35107421875,83.8670120239258 7.96874952316284,77.4839553833008 7.96874952316284,36.3745803833008z")
            };
            UnderlinePath.RenderTransform = transformGroup;
            UnderlineViewbox.Child = UnderlinePath;
            UnderlineHeader.Children.Add(UnderlineViewbox);
            Underline.Header = UnderlineHeader;
            Underline.Click += WPUnderline;
            Italic = new SfRadialMenuItem();
            Grid ItalicHeader = new Grid() { Height = 75, Background = new SolidColorBrush(Colors.Transparent) };
            Viewbox ItalicViewbox = new Viewbox() { Margin = new Thickness(18) };
            Windows.UI.Xaml.Shapes.Path ItalicPath = new Windows.UI.Xaml.Shapes.Path()
            {
                Stretch = Stretch.Uniform,
                Height = 26,
                Width = 26,
                Fill = new SolidColorBrush(Colors.Black),
                RenderTransformOrigin = new Point(0.5, 0.5),
                Data = PathXamlToGeometry("F1M-208.49,-4719.89L-209.008,-4718.14 -235.346,-4718.14 -234.683,-4719.89C-232.041,-4719.95 -230.295,-4720.17 -229.447,-4720.55 -228.062,-4721.08 -227.04,-4721.82 -226.376,-4722.77 -225.339,-4724.25 -224.27,-4726.89 -223.17,-4730.7L-212.029,-4769.31C-211.085,-4772.52 -210.614,-4774.94 -210.614,-4776.58 -210.614,-4777.39 -210.817,-4778.08 -211.226,-4778.65 -211.639,-4779.22 -212.256,-4779.65 -213.091,-4779.95 -213.926,-4780.25 -215.554,-4780.4 -217.977,-4780.4L-217.409,-4782.14 -192.679,-4782.14 -193.197,-4780.4C-195.213,-4780.43 -196.706,-4780.21 -197.682,-4779.74 -199.097,-4779.11 -200.174,-4778.21 -200.916,-4777.04 -201.654,-4775.88 -202.606,-4773.3 -203.77,-4769.31L-214.863,-4730.7C-215.868,-4727.14 -216.373,-4724.88 -216.373,-4723.9 -216.373,-4723.11 -216.175,-4722.45 -215.782,-4721.89 -215.389,-4721.34 -214.761,-4720.92 -213.891,-4720.62 -213.031,-4720.32 -211.226,-4720.08 -208.49,-4719.89z")
            };
            ItalicPath.RenderTransform = transformGroup;
            ItalicViewbox.Child = ItalicPath;
            ItalicHeader.Children.Add(ItalicViewbox);
            Italic.Header = ItalicHeader;
            Italic.Click += WPItalic;
            Cut = new SfRadialMenuItem();
            Grid CutHeader = new Grid() { Height = 75, Background = new SolidColorBrush(Colors.Transparent) };
            Viewbox CutViewbox = new Viewbox() { Margin = new Thickness(18) };
            Windows.UI.Xaml.Shapes.Path CutPath = new Windows.UI.Xaml.Shapes.Path()
            {
                Stretch = Stretch.Uniform,
                Height = 26,
                Width = 26,
                Fill = new SolidColorBrush(Colors.Black),
                RenderTransformOrigin = new Point(0.5, 0.5),
                Data = PathXamlToGeometry("M26.93272,40.905998L26.451015,41.976318 24.699699,46.260094 24.439299,46.437298 21.630575,52.664005C21.368874,53.533619 21.357073,54.478939 21.699675,55.054646 22.090179,55.712357 23.079786,56.020966 24.280296,55.861763 26.486116,55.570259 29.517443,53.617123 30.788252,49.075542 31.509558,46.499798 31.469257,44.511963 30.671051,43.16914 29.822144,41.747314 28.163231,41.153507 26.93272,40.905998z M11.809452,21.572449C8.7251377,21.600742 6.5700688,22.9643 5.5466814,24.369844 4.210753,26.208536 4.8657231,27.786457 7.3722467,28.260103L14.150891,28.755148 14.254991,28.702854 15.780988,28.872335 19.955578,29.176001C20.325277,27.981735 20.596077,26.236832 19.749779,24.812695 18.95038,23.469145 17.223885,22.487257 14.61439,21.891125 13.609521,21.661287 12.673059,21.564526 11.809452,21.572449z M43.601677,0L31.104164,30.593931 64,34.285347 55.42709,39.385468 38.033871,39.015766 31.391865,35.604153 29.14706,35.375153 28.790461,36.250156C30.869762,36.812656 33.401066,38.005566 34.957165,40.618874 36.48177,43.182285 36.69537,46.476799 35.591068,50.419617 33.666668,57.299545 28.55196,60.151161 24.841057,60.64336 21.628952,61.070564 18.648352,59.679455 17.22135,57.279045 16.519447,56.099339 16.921848,51.107117 16.990749,50.893318L18.683651,47.164303 23.837255,34.88295 11.005141,33.620747 6.8593082,33.422047C6.5311871,33.361946 1.6901219,31.174433 1.002491,30.01823 -0.4245705,27.617319 -0.39203876,24.054804 1.5142117,21.435192 3.7186239,18.408779 8.7628794,15.437566 15.726547,17.028872 19.718651,17.937576 22.511755,19.700785 24.036356,22.263296 25.624956,24.933706 25.423157,27.788721 24.901056,29.896128L25.817659,30.00013 26.742159,27.78722 26.984358,20.442987 35.029865,5.097682z")
            };
            CutPath.RenderTransform = transformGroup;
            CutViewbox.Child = CutPath;
            CutHeader.Children.Add(CutViewbox);
            Cut.Header = CutHeader;
            Cut.Click += WPCut;
            Copy = new SfRadialMenuItem();
            Grid CopyHeader = new Grid() { Height = 75, Background = new SolidColorBrush(Colors.Transparent) };
            Viewbox CopyViewbox = new Viewbox() { Margin = new Thickness(18) };
            Windows.UI.Xaml.Shapes.Path CopyPath = new Windows.UI.Xaml.Shapes.Path()
            {
                Stretch = Stretch.Uniform,
                Height = 26,
                Width = 26,
                Fill = new SolidColorBrush(Colors.Black),
                RenderTransformOrigin = new Point(0.5, 0.5),
                Data = PathXamlToGeometry("M31.687001,23.116L31.687001,31.394341C31.687001,31.394341,31.526705,36.832023,25.52624,36.359949L18.506,36.388395 18.506,49.587002 18.506001,54.153999 18.506,59.292614C18.506,59.812107,18.929218,60.233997,19.448625,60.233997L45.808704,60.233997C46.326101,60.233997,46.749998,59.812107,46.749998,59.292614L46.749998,24.057384C46.749998,23.539322,46.326101,23.116,45.808704,23.116z M20.552001,4.5669994L20.552001,14.60861C20.552001,14.60861,20.358706,21.203206,13.080177,20.631915L4.565999,20.665694 4.565999,48.4459C4.566,49.076302,5.0797424,49.587002,5.7100554,49.587002L14.742001,49.587002 14.742001,35.400336 18.161097,31.792807 18.166127,31.774705 26.474542,22.972 26.521163,22.972 29.953973,19.349999 38.822001,19.349999 38.822001,5.7076302C38.822001,5.0806808,38.309198,4.5669994,37.680792,4.5669994z M18.449971,0L37.680651,0C40.833681,0,43.391001,2.5571156,43.391001,5.707489L43.391001,19.349999 45.80884,19.349999C48.409018,19.35,50.517,21.458894,50.517,24.05704L50.517,59.292992C50.517,61.893593,48.409018,64,45.80884,64L19.448812,64C16.849223,64,14.742,61.893593,14.742001,59.292992L14.742001,54.153999 5.709774,54.153999C2.555994,54.153999,0,51.599316,0,48.445534L0,19.465691 4.1473293,15.090039 4.1532602,15.068708 14.229262,4.3929996 14.286199,4.3929996z")
            };
            CopyPath.RenderTransform = transformGroup;
            CopyViewbox.Child = CopyPath;
            CopyHeader.Children.Add(CopyViewbox);
            Copy.Header = CopyHeader;
            Copy.Click += WPCopy;
            Paste = new SfRadialMenuItem();
            Grid PasteHeader = new Grid() { Height = 75, Background = new SolidColorBrush(Colors.Transparent) };
            Viewbox PasteViewbox = new Viewbox() { Margin = new Thickness(18) };
            Windows.UI.Xaml.Shapes.Path PastePath = new Windows.UI.Xaml.Shapes.Path()
            {
                Stretch = Stretch.Uniform,
                Height = 26,
                Width = 26,
                Fill = new SolidColorBrush(Colors.Black),
                RenderTransformOrigin = new Point(0.5, 0.5),
                Data = PathXamlToGeometry("M31.948999,57.859001L45.188999,57.859001 45.188999,59.699001 31.948999,59.699001z M31.948999,51.950001L45.188999,51.950001 45.188999,53.791 31.948999,53.791z M28.396044,35.408001C28.009256,35.408001,27.689001,35.726357,27.689001,36.116974L27.689001,62.648006C27.689001,63.039406,28.009256,63.357002,28.396044,63.357002L48.248085,63.357002C48.638691,63.357002,48.957001,63.039406,48.957001,62.648006L48.957001,45.402447 43.671608,45.381203C39.153995,45.736,39.033001,41.642223,39.033001,41.642223L39.033001,35.408001z M28.395853,32.571999L40.336986,32.571999 42.92213,35.299999 42.958431,35.299999 49.212662,41.928822 49.216327,41.942017 51.790001,44.657917 51.790001,62.648022C51.790001,64.605804,50.204025,66.191002,48.247158,66.191002L28.395853,66.191002C26.438854,66.191002,24.853,64.605804,24.853,62.648022L24.853,36.116901C24.853,34.16061,26.438854,32.571999,28.395853,32.571999z M23.4165,2.7189999C21.472765,2.7190001 19.896,4.211246 19.896,6.0526261 19.896,7.8918042 21.472765,9.3859997 23.4165,9.3859997 25.360433,9.3859997 26.936998,7.8918042 26.936998,6.0526261 26.936998,4.211246 25.360433,2.7190001 23.4165,2.7189999z M23.4165,0C26.836176,0,29.627563,2.5418639,29.798687,5.7406974L29.806993,6.0519996 35.229999,6.0519996 35.229999,7.9879994 46.833999,7.9879994 46.833999,34.156097 41.895698,28.945999 41.695999,28.945999 41.695999,14.389 35.229999,14.389 35.229999,20.865 11.604001,20.865 11.604001,14.389 5.136998,14.389 5.136998,59.294998 21.228,59.294998 21.228,62.648151C21.228,63.142387,21.27823,63.625092,21.373855,64.091423L21.431106,64.338997 0,64.338997 0,7.9879994 11.604001,7.9879994 11.604001,6.0519996 17.026007,6.0519996 17.034315,5.7406974C17.205442,2.5418639,19.996919,0,23.4165,0z")
            };
            PastePath.RenderTransform = transformGroup;
            PasteViewbox.Child = PastePath;
            PasteHeader.Children.Add(PasteViewbox);
            Paste.Header = PasteHeader;
            Paste.Click += WPPaste;
            Undo = new SfRadialMenuItem();
            Grid UndoHeader = new Grid() { Height = 75, Background = new SolidColorBrush(Colors.Transparent) };
            Viewbox UndoViewbox = new Viewbox() { Margin = new Thickness(18) };
            Windows.UI.Xaml.Shapes.Path UndoPath = new Windows.UI.Xaml.Shapes.Path()
            {
                Stretch = Stretch.Uniform,
                Height = 26,
                Width = 26,
                Fill = new SolidColorBrush(Colors.Black),
                RenderTransformOrigin = new Point(0.5, 0.5),
                Data = PathXamlToGeometry("F1M1146.68,2290.95C1147.82,2288.38 1149.41,2286.02 1151.46,2283.97 1155.82,2279.6 1161.62,2277.2 1167.8,2277.2 1173.97,2277.2 1179.78,2279.6 1184.14,2283.97 1188.5,2288.33 1190.91,2294.14 1190.91,2300.31 1190.91,2306.49 1188.5,2312.29 1184.14,2316.65 1183.44,2317.34 1182.53,2317.69 1181.62,2317.69 1180.71,2317.69 1179.8,2317.34 1179.1,2316.65 1177.72,2315.26 1177.72,2313.01 1179.11,2311.62 1182.13,2308.6 1183.8,2304.58 1183.8,2300.31 1183.8,2296.04 1182.13,2292.02 1179.1,2289 1176.09,2285.97 1172.07,2284.31 1167.8,2284.31 1163.52,2284.31 1159.51,2285.97 1156.48,2289 1154.24,2291.24 1152.76,2294.03 1152.13,2297.06L1159.8,2305.64 1137.57,2305.64 1139.35,2282.74 1146.68,2290.95z")
            };
            UndoPath.RenderTransform = transformGroup;
            UndoViewbox.Child = UndoPath;
            UndoHeader.Children.Add(UndoViewbox);
            Undo.Header = UndoHeader;
            Undo.Click += WPUndo;
            Redo = new SfRadialMenuItem();
            Grid RedoHeader = new Grid() { Height = 75, Background = new SolidColorBrush(Colors.Transparent) };
            Viewbox RedoViewbox = new Viewbox() { Margin = new Thickness(18) };
            Windows.UI.Xaml.Shapes.Path RedoPath = new Windows.UI.Xaml.Shapes.Path()
            {
                Stretch = Stretch.Uniform,
                Height = 26,
                Width = 26,
                Fill = new SolidColorBrush(Colors.Black),
                RenderTransformOrigin = new Point(0.5, 0.5),
                Data = PathXamlToGeometry("F1M1182.69,1926.92C1181.55,1924.36 1179.96,1921.99 1177.91,1919.94 1173.55,1915.58 1167.74,1913.17 1161.57,1913.17 1155.39,1913.17 1149.59,1915.58 1145.23,1919.94 1140.86,1924.31 1138.46,1930.11 1138.46,1936.28 1138.46,1942.46 1140.86,1948.26 1145.23,1952.62 1145.92,1953.32 1146.83,1953.66 1147.74,1953.66 1148.65,1953.66 1149.56,1953.32 1150.26,1952.62 1151.64,1951.24 1151.64,1948.99 1150.26,1947.59 1147.23,1944.57 1145.57,1940.56 1145.57,1936.28 1145.57,1932.01 1147.23,1927.99 1150.26,1924.97 1153.28,1921.94 1157.29,1920.28 1161.57,1920.28 1165.84,1920.28 1169.86,1921.94 1172.88,1924.97 1175.12,1927.21 1176.61,1930 1177.23,1933.04L1169.57,1941.62 1191.79,1941.62 1190.01,1918.71 1182.69,1926.92z")
            };
            RedoPath.RenderTransform = transformGroup;
            RedoViewbox.Child = RedoPath;
            RedoHeader.Children.Add(RedoViewbox);
            Redo.Header = RedoHeader;
            Redo.Click += WPRedo;
            selectionmenu.Items.Add(Bold);
            selectionmenu.Items.Add(Italic);
            selectionmenu.Items.Add(Underline);
            selectionmenu.Items.Add(Cut);
            selectionmenu.Items.Add(Copy);
            selectionmenu.Items.Add(Paste);
            selectionmenu.Items.Add(Undo);
            selectionmenu.Items.Add(Redo);
        }

        private Geometry PathXamlToGeometry(string pathXaml)
        {
            string xaml =
            "<Path " +
            "xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>" +
            "<Path.Data>" + pathXaml + "</Path.Data></Path>";
            var path = XamlReader.Load(xaml) as Windows.UI.Xaml.Shapes.Path;
            // Detach the PathGeometry from the Path
            Geometry geometry = path.Data;
            path.Data = null;
            return geometry;
        }

        private async void WPCut(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Cut operation executed");
            await dialog.ShowAsync();
        }

        private async void WPCopy(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Copy operation executed.");
            await dialog.ShowAsync();
        }
        private async void WPPaste(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Paste operation executed.");
            await dialog.ShowAsync();
        }
        private void WPBold(object sender, RoutedEventArgs e)
        {
            if (text.FontWeight.Equals(FontWeights.Bold))
                text.FontWeight = FontWeights.Normal;
            else
                text.FontWeight = FontWeights.Bold;
        }
        private async void WPItalic(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Italic operation executed.");
            await dialog.ShowAsync();
        }
        private async void WPUnderline(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Underline operation executed.");
            await dialog.ShowAsync();
        }

        private async void WPUndo(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Undo operation executed.");
            await dialog.ShowAsync();
        }

        private async void WPRedo(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Redo operation executed.");
            await dialog.ShowAsync();
        }

        private void Bullet(object sender, RoutedEventArgs e)
        {
            if (!bullet.IsChecked)
                text.Document.Selection.ParagraphFormat.ListType = Windows.UI.Text.MarkerType.Bullet;
            else
                text.Document.Selection.ParagraphFormat.ListType = Windows.UI.Text.MarkerType.None;
        }
    }
}
