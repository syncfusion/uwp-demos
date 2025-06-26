#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Common
{
    public  partial class SampleLayout : UserControl, IDisposable
    {
        public SampleLayout()
        {
            this.Name = "Root";
            this.InitializeComponent();
            _canExecute = true;
            FrameNavigationService.SampleLayout = this;
            this.Unloaded += SampleLayout_Unloaded;
            if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
            {
                if (!FrameNavigationService.IsInFeatureSample)
                {
                    if (!FrameNavigationService.IsInWhatsNewSample)
                    {
                        foreach (ContentControl content in FindVisualChildren<ContentControl>(this))
                        {
                            if (content.Tag != null && content.Tag.ToString() == "Sample")
                            {
                                content.Margin = new Thickness(10, 0, 10, 0);
                                break;
                            }
                        }
                        foreach (Grid grid in FindVisualChildren<Grid>(this))
                        {
                            if (grid.Tag != null && grid.Tag.ToString() == "OptionGrid")
                            {
                                grid.Margin = new Thickness(0, 0, 5, 0);
                                break;
                            }
                        }
                        if (FrameNavigationService.CurrentFrame != null)
                        {
                            FrameNavigationService.CurrentFrame.Margin = new Thickness(0, 39, 0, 0);
                        }
                    }
                    else
                    {
                        if (FrameNavigationService.CurrentFrame != null)
                        {
                            FrameNavigationService.CurrentFrame.Margin = new Thickness(0, 39, -5, 0);
                        }

                        SetOptionGridMargin();
                    }
                }
                else
                {
                    if (FrameNavigationService.CurrentFrame != null)
                    {
                        FrameNavigationService.CurrentFrame.Margin = new Thickness(0, 39, 0, 0);
                    }

                    foreach (ContentControl content in FindVisualChildren<ContentControl>(this))
                    {
                        if (content.Tag != null && content.Tag.ToString() == "Sample")
                        {
                            content.Margin = new Thickness(0);
                            break;
                        }
                    }

                    foreach (Grid grid in FindVisualChildren<Grid>(this))
                    {
                        if (grid.Tag != null && grid.Tag.ToString() == "OptionGrid")
                        {
                            grid.Margin = new Thickness(0, 0, -2, 20);
                            break;
                        }
                    }
                }
            }
        }

        private void SampleLayout_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= SampleLayout_Unloaded;
            try
            {
                Dispose();
            }
            catch (Exception)
            {

            }
        }

        public String Header
        {
            get { return (String)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(String), typeof(SampleLayout), new PropertyMetadata(String.Empty));



        public Control Sample
        {
            get { return ((Control)GetValue(SampleProperty)); }
            set { SetValue(SampleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SampleProperty =
            DependencyProperty.Register("Sample", typeof(Control), typeof(SampleLayout), new PropertyMetadata(null));

        public Control Setting
        {
            get { return ((Control)GetValue(SettingProperty)); }
            set { SetValue(SettingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingProperty =
            DependencyProperty.Register("Setting", typeof(Control), typeof(SampleLayout), new PropertyMetadata(null));

        public Visibility OptionVisibility
        {
            get { return (Visibility)GetValue(OptionVisibilityProperty); }
            set { SetValue(OptionVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OptionVisibilityProperty =
            DependencyProperty.Register("OptionVisibility", typeof(Visibility), typeof(SampleLayout), new PropertyMetadata(Visibility.Collapsed));


        public string FontIcons
        {
            get { return (string)GetValue(FontIconsProperty); }
            set { SetValue(FontIconsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontIconsProperty =
            DependencyProperty.Register("FontIcons", typeof(string), typeof(SampleLayout), new PropertyMetadata("\uE713"));



        public string Pathstring
        {
            get { return (string)GetValue(PathstringProperty); }
            set { SetValue(PathstringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pathstring.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathstringProperty =
            DependencyProperty.Register("Pathstring", typeof(string), typeof(SampleLayout), new PropertyMetadata("M16.153984,12.087952C13.92602,12.087952 12.113032,13.844971 12.113032,16.003967 12.113032,18.164001 13.92602,19.921997 16.153984,19.921997 18.382011,19.921997 20.195,18.164001 20.195,16.003967 20.195,13.844971 18.382011,12.087952 16.153984,12.087952z M16.153984,10.087952C19.484978,10.087952 22.194998,12.741943 22.194998,16.003967 22.194998,19.267944 19.484978,21.921997 16.153984,21.921997 12.822992,21.921997 10.113033,19.267944 10.113033,16.003967 10.113033,12.741943 12.822992,10.087952 16.153984,10.087952z M14.195978,1.9999999L14.195978,5.4509887 13.437006,5.638977C11.470027,6.1249999,9.6860313,7.1269531,8.2780113,8.5379638L7.7300382,9.086975 4.6240215,7.2529907 2.7770376,10.232971 5.8810401,12.066956 5.6390357,12.824951C5.3129859,13.852966 5.1470313,14.921997 5.1470313,16.003967 5.1470313,17.093994 5.3190284,18.180969 5.6580176,19.234985L5.9009986,19.991943 2.8120105,21.836975 4.6780376,24.80896 7.7570157,22.964966 8.3080406,23.511963C9.7089806,24.899963,11.48199,25.888,13.437006,26.370972L14.195978,26.55896 14.195978,30 17.804008,30 17.804008,26.613953 18.590019,26.441956C20.624014,25.995972,22.473989,25.004944,23.939993,23.574951L24.48699,23.040955 27.374989,24.743957 29.223988,21.764954 26.364979,20.08197 26.619007,19.315979C26.978992,18.231995 27.160999,17.116943 27.160999,16.003967 27.160999,14.869995 26.97002,13.736999 26.593983,12.636963L26.332996,11.869995 29.191029,10.161987 27.324024,7.1919555 24.433035,8.9179687 23.882986,8.3880004C22.424978,6.9839477,20.595022,6.0099487,18.590996,5.5699462L17.804008,5.3989868 17.804008,1.9999999z M12.195979,0L19.804008,0 19.804008,3.8149413C21.613028,4.3279418,23.282034,5.2199706,24.699026,6.4299926L27.976978,4.4729614 31.971972,10.829956 28.726002,12.770996C29.015003,13.832947 29.160999,14.916992 29.160999,16.003967 29.160999,17.065979 29.022999,18.125 28.749012,19.164978L32.001999,21.078979 28.043995,27.460998 24.767996,25.527954C23.337027,26.765991,21.64501,27.675964,19.804008,28.197998L19.804008,32 12.195979,32 12.195979,28.100952C10.445003,27.555969,8.8350182,26.656982,7.4730191,25.465942L4.0259991,27.528992 0.030029264,21.168945 3.5320418,19.076965C3.2770371,18.066956 3.1470323,17.036987 3.1470323,16.003967 3.1470323,14.983948 3.2709947,13.974976 3.5150135,12.991943L0,10.915955 3.9550152,4.5359496 7.4320035,6.5879516C8.8030357,5.3729858,10.428035,4.4599609,12.195979,3.9089965z"));


        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => MyAction(), _canExecute));
            }
        }
        private bool _canExecute;
        public void MyAction()
        {

            if (OptionVisibility == Visibility.Collapsed)
            {
                if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
                {
                    if (FrameNavigationService.IsInFeatureSample)
                    {
                        FrameNavigationService.Main.Margin = new Thickness(0, -16, 0, 0);
                        
                            foreach (TextBlock txtblock in FindVisualChildren<TextBlock>(this))
                            {
                                if (txtblock.Text == "Options")
                                {
                                    txtblock.Margin = new Thickness(5);
                                    break;
                                }
                            }
                        if (FrameNavigationService.CurrentFrame != null)
                            FrameNavigationService.CurrentFrame.Margin = new Thickness(0, 10, 0, 0);
                    }
                    else
                    {
                        if (FrameNavigationService.CurrentFrame != null)
                        {
                            if (FrameNavigationService.IsInWhatsNewSample)
                            {
                                FrameNavigationService.CurrentFrame.Margin = new Thickness(0, 25, -5, 0);
                            }
                            else
                            {
                                FrameNavigationService.CurrentFrame.Margin = new Thickness(0, 25, 0, 0);
                            }
                            SetOptionGridMargin();
                        }
                        
                        foreach (TextBlock txtblock in FindVisualChildren<TextBlock>(this))
                        {
                            if (txtblock.Text == "Options")
                            {
                                txtblock.Margin = new Thickness(15, 25, 5, 5);
                                break;
                            }
                        }
                    foreach (ContentControl control in FindVisualChildren<ContentControl>(this))
                        {
                            if (control.Tag != null && control.Tag.ToString() == "Setting")
                            {
                                control.Margin = new Thickness(10, 65, 0, 5);
                            }
                        }
                    }
                    FrameNavigationService.SelectedSampleLayout = this;
                    FrameNavigationService.Headergrid.Visibility = Visibility.Collapsed;
                    FrameNavigationService.IsSettingOpen = true;
                }
                this.OptionVisibility = Visibility.Visible;
                this.Pathstring = "M22.014996,10.204013L23.449993,11.597013 13.512005,21.845022 8.4560118,17.863018 9.6940098,16.292017 13.332006,19.157019z M15.937988,2C8.2529907,2,2,8.2520142,2,15.937988L2,16.062988C2,23.747986,8.2529907,30,15.937988,30L16.062988,30C23.747986,30,30.000977,23.747986,30.000977,16.062988L30.000977,15.937988C30.000977,8.2520142,23.747986,2,16.062988,2z M15.937988,0L16.062988,0C24.851013,0,32,7.1489868,32,15.937988L32,16.062988C32,24.851013,24.851013,32,16.062988,32L15.937988,32C7.1499634,32,0,24.851013,0,16.062988L0,15.937988C0,7.1489868,7.1499634,0,15.937988,0z";
                this.FontIcons = "\uE0AB";
               
            }
            else
            {
                if (DeviceFamily.GetDeviceFamily() == Devices.Mobile)
                {
                    FrameNavigationService.Main.Margin = new Thickness(0, 0, 0, 0);
                    FrameNavigationService.Headergrid.Visibility = Visibility.Visible;
                    FrameNavigationService.IsSettingOpen = false;
                    FrameNavigationService.SelectedSampleLayout = null;
                    if (FrameNavigationService.CurrentFrame != null)
                    {
                        if (FrameNavigationService.IsInWhatsNewSample)
                        {
                            FrameNavigationService.currentFrame.Margin = new Thickness(0, 39, -5, 0);
                            SetOptionGridMargin();
                        }
                        else
                        {
                            FrameNavigationService.CurrentFrame.Margin = new Thickness(0, 39, 0, 0);
                        }
                    }
                }
                this.OptionVisibility = Visibility.Collapsed;
                this.Pathstring = "M16.153984,12.087952C13.92602,12.087952 12.113032,13.844971 12.113032,16.003967 12.113032,18.164001 13.92602,19.921997 16.153984,19.921997 18.382011,19.921997 20.195,18.164001 20.195,16.003967 20.195,13.844971 18.382011,12.087952 16.153984,12.087952z M16.153984,10.087952C19.484978,10.087952 22.194998,12.741943 22.194998,16.003967 22.194998,19.267944 19.484978,21.921997 16.153984,21.921997 12.822992,21.921997 10.113033,19.267944 10.113033,16.003967 10.113033,12.741943 12.822992,10.087952 16.153984,10.087952z M14.195978,1.9999999L14.195978,5.4509887 13.437006,5.638977C11.470027,6.1249999,9.6860313,7.1269531,8.2780113,8.5379638L7.7300382,9.086975 4.6240215,7.2529907 2.7770376,10.232971 5.8810401,12.066956 5.6390357,12.824951C5.3129859,13.852966 5.1470313,14.921997 5.1470313,16.003967 5.1470313,17.093994 5.3190284,18.180969 5.6580176,19.234985L5.9009986,19.991943 2.8120105,21.836975 4.6780376,24.80896 7.7570157,22.964966 8.3080406,23.511963C9.7089806,24.899963,11.48199,25.888,13.437006,26.370972L14.195978,26.55896 14.195978,30 17.804008,30 17.804008,26.613953 18.590019,26.441956C20.624014,25.995972,22.473989,25.004944,23.939993,23.574951L24.48699,23.040955 27.374989,24.743957 29.223988,21.764954 26.364979,20.08197 26.619007,19.315979C26.978992,18.231995 27.160999,17.116943 27.160999,16.003967 27.160999,14.869995 26.97002,13.736999 26.593983,12.636963L26.332996,11.869995 29.191029,10.161987 27.324024,7.1919555 24.433035,8.9179687 23.882986,8.3880004C22.424978,6.9839477,20.595022,6.0099487,18.590996,5.5699462L17.804008,5.3989868 17.804008,1.9999999z M12.195979,0L19.804008,0 19.804008,3.8149413C21.613028,4.3279418,23.282034,5.2199706,24.699026,6.4299926L27.976978,4.4729614 31.971972,10.829956 28.726002,12.770996C29.015003,13.832947 29.160999,14.916992 29.160999,16.003967 29.160999,17.065979 29.022999,18.125 28.749012,19.164978L32.001999,21.078979 28.043995,27.460998 24.767996,25.527954C23.337027,26.765991,21.64501,27.675964,19.804008,28.197998L19.804008,32 12.195979,32 12.195979,28.100952C10.445003,27.555969,8.8350182,26.656982,7.4730191,25.465942L4.0259991,27.528992 0.030029264,21.168945 3.5320418,19.076965C3.2770371,18.066956 3.1470323,17.036987 3.1470323,16.003967 3.1470323,14.983948 3.2709947,13.974976 3.5150135,12.991943L0,10.915955 3.9550152,4.5359496 7.4320035,6.5879516C8.8030357,5.3729858,10.428035,4.4599609,12.195979,3.9089965z";
                this.FontIcons= "\uE713"; 
            }
        }

       internal void SetOptionGridMargin()
        {
            if (FrameNavigationService.IsInWhatsNewSample)
            {
                foreach (Grid grid in FindVisualChildren<Grid>(this))
                {
                    if (grid.Tag != null && grid.Tag.ToString() == "OptionGrid")
                    {
                        grid.Margin = new Thickness(0, 0, 10, 20);
                        break;
                    }
                }
            }
            else
            {
                foreach (Grid grid in FindVisualChildren<Grid>(this))
                {
                    if (grid.Tag != null && grid.Tag.ToString() == "OptionGrid")
                    {
                        grid.Margin = new Thickness(0, 0, 10, 0);
                        break;
                    }
                }
            }
        }
        internal static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent)
        where T : DependencyObject
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                var childType = child as T;
                if (childType != null)
                {
                    yield return (T)child;
                }

                foreach (var other in FindVisualChildren<T>(child))
                {
                    yield return other;
                }
            }
        }
        public virtual void Dispose()
        {
            Content = null;
            FrameNavigationService.SelectedSampleLayout = null;
            //Need to reset the sample layout while dispose to avoid memory leak.
            FrameNavigationService.SampleLayout = null;
        }
    }

    public class NullToCollapseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
