using Common;
using Syncfusion.UI.Xaml.Controls.Navigation;
using Syncfusion.UI.Xaml.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SampleBrowser.Menu
{
    public sealed partial class MenuView : SampleLayout
    {
        bool isTextCopied = false;
        public MenuView()
        {
            this.InitializeComponent();
            _ExcuteCommand = new DelegateCommand(CommandExcuted);
            this.Loaded += MenuView_Loaded;
            this.DataContext = this;
        }
        string description = string.Empty;
        private void MenuView_Loaded(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                description = @"Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula.Lorem tortor neque, purus taciti quis id. Elementum integer orci accumsan minim phasellus vel.";
            }
            else
            {
                description = @"Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula. Lorem tortor neque, purus taciti quis id. Elementum integer orci accumsan minim phasellus vel." + "\n" + "Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula. Lorem tortor neque, purus taciti quis id. Elementum integer orci accumsan minim phasellus vel." + "\n" + "Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula. Lorem tortor neque, purus taciti quis id. Elementum integer orci accumsan minim phasellus vel.";
            }
            txt.Document.SetText(Windows.UI.Text.TextSetOptions.None, description);
           
        }

        private async  void CommandExcuted(object param)
        {
            if(param.ToString()== "New")
            {
                txt.Document.SetText(Windows.UI.Text.TextSetOptions.None, string.Empty);
            }
            else if(param.ToString() == "Save")
            {
              var messagedialog=new MessageDialog("Save Command Excuted", "");
               await messagedialog.ShowAsync();
            }
            else if (param.ToString() == "Save As")
            {
                var messagedialog = new MessageDialog("Save As Command Excuted", "");
               await messagedialog.ShowAsync();
            }
            else if (param.ToString() == "Exit")
            {
                var messagedialog = new MessageDialog("Exit Command Excuted", "");
                await messagedialog.ShowAsync();
            }
            else if (param.ToString() == "Cut")
            {
                if (!isTextCopied && txt.Document.Selection.Length > 0)
                {
                    txt.Document.Selection.Cut();
                    isTextCopied = true;
                }
                else if (isTextCopied)
                {
                    txt.Document.Selection.Cut();
                }

            }
            else if (param.ToString() == "Copy")
            {

                if (!isTextCopied && txt.Document.Selection.Length > 0)
                {
                    txt.Document.Selection.Copy();
                    isTextCopied = true;
                }
                else if (isTextCopied)
                {
                    txt.Document.Selection.Copy();
                }
            }
            else if (param.ToString() == "Paste")
            {
                if(isTextCopied)
                txt.Document.Selection.Paste(0);
            }
            else if (param.ToString() == "Bold")
            {
                txt.Document.Selection.CharacterFormat.Bold = FormatEffect.On;
            }
            else if (param.ToString() == "Italic")
            {
                txt.Document.Selection.CharacterFormat.Italic = FormatEffect.On;
            }
            else if (param.ToString() == "Underline")
            {
                txt.Document.Selection.CharacterFormat.Underline = UnderlineType.Dash;
            }
            else if (param.ToString() == "Red")
            {
                txt.Document.Selection.CharacterFormat.BackgroundColor = Colors.Red;
            }
            else if (param.ToString() == "Blue")
            {
                txt.Document.Selection.CharacterFormat.BackgroundColor = Colors.Blue;
            }
            else if (param.ToString() == "Green")
            {
                txt.Document.Selection.CharacterFormat.BackgroundColor = Colors.Green;
            }
            else if (param.ToString() == "Left")
            {
                txt.TextAlignment = TextAlignment.Left;
            }
            else if (param.ToString() == "Right")
            {
                txt.TextAlignment = TextAlignment.Right;
            }
            else if (param.ToString() == "Center")
            {
                txt.TextAlignment = TextAlignment.Center;
            }
            else if (param.ToString() == "Justify")
            {
                txt.TextAlignment = TextAlignment.Justify;
            }
            else if (param.ToString() == "Help")
            {
                var messagedialog = new MessageDialog("Help Command Excuted", "");
                await messagedialog.ShowAsync();
            }
            else if (param.ToString() == "About")
            {
                var messagedialog = new MessageDialog("About Command Excuted", "");
               await messagedialog.ShowAsync();
            }
            else if (param.ToString() == "Bullets")
            {
                if (!bullet.IsChecked)
                    txt.Document.Selection.ParagraphFormat.ListType = Windows.UI.Text.MarkerType.Bullet;
                else
                {
                    txt.Document.Selection.ParagraphFormat.ListType = MarkerType.None;
                   
                    
                }                    
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.controlView.Visibility == Visibility.Visible)
            {
                this.controlView.Visibility = Visibility.Collapsed;
                this.setting.Visibility = Visibility.Visible;
                (sender as Button).Content = "Done";
            }
            else
            {
                this.controlView.Visibility = Visibility.Visible;
                this.setting.Visibility = Visibility.Collapsed;
                (sender as Button).Content = "Options";
            }
        }

        private void Expand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            ComboBoxItem cmbitm = cmb.SelectedValue as ComboBoxItem;
            if (cmbitm.Content.ToString() == "ExpandOnClick")
            {
                Sfmenu.ExpandMode = ExpandModes.ExpandOnClick;
            }
            else if (cmbitm.Content.ToString() == "ExpandOnMouseOver")
            {
                Sfmenu.ExpandMode = ExpandModes.ExpandOnMouseOver;
            }
        }

        private void menuorientation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            ComboBoxItem cmbitm = cmb.SelectedValue as ComboBoxItem;
            if (cmbitm.Content.ToString() == "Horizontal")
            {
                txt.Margin = new Thickness(5, 60, 5, 10);
                Sfmenu.HorizontalAlignment = HorizontalAlignment.Stretch;
                Sfmenu.VerticalAlignment = VerticalAlignment.Top;
                Sfmenu.Orientation = Orientation.Horizontal; 
            }
            else if (cmbitm.Content.ToString() == "Vertical")
            {
               
                 txt.Margin = new Thickness(125, 10, 0, 10);
                  Sfmenu.HorizontalAlignment = HorizontalAlignment.Left;
                Sfmenu.VerticalAlignment = VerticalAlignment.Stretch;
                Sfmenu.Orientation = Orientation.Vertical;
            }
        }

        private void PopupAnimation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            ComboBoxItem cmbitm = cmb.SelectedValue as ComboBoxItem;
            if (cmbitm.Content.ToString() == "Fade")
            {
                Sfmenu.PopUpAnimationType = PopUpAnimationTypes.Fade;
            }
            else if (cmbitm.Content.ToString() == "Scroll")
            {
                Sfmenu.PopUpAnimationType = PopUpAnimationTypes.Scroll;
            }
            else if (cmbitm.Content.ToString() == "Slide")
            {
                Sfmenu.PopUpAnimationType = PopUpAnimationTypes.Slide;
            }
            else
            {
                Sfmenu.PopUpAnimationType = PopUpAnimationTypes.None;
            }
        }

        private ICommand _ExcuteCommand;
        public ICommand ExcuteCommand
        {
            get
            {
                return _ExcuteCommand;
            }
        }

        private void ShowSuggestion_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton tb = sender as ToggleButton;
            if ((bool)ShowSuggestion.IsChecked)
            {
                Sfmenu.IconGridVisibility = Visibility.Visible;
            }
            else
            {
                Sfmenu.IconGridVisibility = Visibility.Collapsed;
            }
        }

        public override void Dispose()
        {
            Loaded -= MenuView_Loaded;
            if (Sfmenu != null)
            {
                Sfmenu.Dispose();
                if (_ExcuteCommand != null)
                    _ExcuteCommand = null;
                if (Sfmenu.Items.Count > 0)
                    Sfmenu.Items.Clear();
                Sfmenu = null;
                GC.Collect();
            }

            if (Expand != null)
                Expand.SelectionChanged -= Expand_SelectionChanged;
            if (menuorientation != null)
                menuorientation.SelectionChanged -= menuorientation_SelectionChanged;
            if (PopupAnimation != null)
                PopupAnimation.SelectionChanged -= PopupAnimation_SelectionChanged;
            if (ShowSuggestion != null)
                ShowSuggestion.Click -= ShowSuggestion_Click;
            GC.Collect();
        }
    }
}
