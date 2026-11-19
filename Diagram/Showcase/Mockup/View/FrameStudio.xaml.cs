using Mockup.BusinessObject;
using Mockup.Utility;
using Mockup.ViewModel;
using Syncfusion.UI.Xaml.Controls.Navigation;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Mockup.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FrameStudio : Page
    {
        private bool _mRulerEnabledOnEditMode = true;
        public FrameStudio()
        {
            this.InitializeComponent();
            TitleBackground = GetColorFromHexa("#3e3f42");
            
            MenuFlyout menu = new MenuFlyout();
            Binding bind = new Binding { Path = new PropertyPath("SelectedDiagram.Export") };
            List<string> formats = new List<string> { "Png", "Jpeg", "Gif", "Tiff", "Jpegxr" };
            foreach (var item in formats)
            {
                MenuFlyoutItem menuItem = new MenuFlyoutItem
                {
                    Text = item,
                    CommandParameter = item
                };
                menuItem.SetBinding(MenuFlyoutItem.CommandProperty, bind);
                menu.Items.Add(menuItem);
            }
           // exportbutton.Flyout = menu;
            Window.Current.Dispatcher.AcceleratorKeyActivated += Dispatcher_AcceleratorKeyActivated;
            Keys = new List<VirtualKey>();
            NewPage = new Command(OnNewPageCommand);
            ClonePage = new Command(OnClonePageCommand);
            Save = new Command(OnSaveCommand);
            SaveAs = new Command(OnSaveAsCommand);
            Open = new Command(OnOpenCommand);
            RulerVisibility = new Command(OnRulerVisibilityCommand);
            SmartGuide = new Command(OnSmartGuideCommand);
            GridLines = new Command(OnGridLinesCommand);
            Lock = new Command(OnLockCommand);
            UnLock = new Command(OnUnLockCommand);
            UnSelect = new Command(OnUnSelectCommand);
            Pan = new Command(OnPanCommand);
            Library = new Command(OnLibraryCommand);
            Copy = new Command(OnCopyCommand);
            Paste = new Command(OnPasteCommand);
        }

        #region Keyboard Commands
        bool _mEnablePan = false;
        private VirtualKeyModifiers KeyModifiers;
        List<VirtualKey> Keys;
        VirtualKey CurrentKey;
        CoreVirtualKeyStates KeyState { get; set; }
        void Dispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            if (args.VirtualKey == VirtualKey.Space)
            {
                switch (args.EventType)
                {
                    case CoreAcceleratorKeyEventType.KeyDown:
                        _mEnablePan = true;
                        Pan.Execute(null);
                        break;
                    case CoreAcceleratorKeyEventType.KeyUp:
                        _mEnablePan = false;
                        Pan.Execute(null);
                        break;
                }
            }

        }

        #region ICommand
        public ICommand NewPage { get; set; }
        public ICommand ClonePage { get; set; }
        public ICommand Save { get; set; }
        public ICommand SaveAs { get; set; }
        public ICommand Open { get; set; }
        public ICommand RulerVisibility { get; set; }
        public ICommand SmartGuide { get; set; }
        public ICommand GridLines { get; set; }
        public ICommand Lock { get; set; }
        public ICommand UnLock { get; set; }
        public ICommand UnSelect { get; set; }
        public ICommand Pan { get; set; }
        public ICommand Library { get; set; }
        public ICommand Copy { get; set; }
        public ICommand Paste { get; set; }
        #endregion

        #region Command Execution

        private void OnCopyCommand(object param)
        {
            (this.DataContext as DiagramBuilderVM).OnCopyCommand(null);
        }

        private void OnPasteCommand(object param)
        {
            (this.DataContext as DiagramBuilderVM).OnPasteCommand(null);
        }

        private void OnNewPageCommand(object param)
        {
            (this.DataContext as DiagramBuilderVM).OnNewCommand(null);
        }

        private void OnClonePageCommand(object param)
        {
            (this.DataContext as DiagramBuilderVM).OnDuplicateCommand(null); 
        }

        private void OnSaveCommand(object param)
        {
            (this.DataContext as DiagramBuilderVM).OnSaveCommand(null);
        }

        private void OnSaveAsCommand(object param)
        {
            (this.DataContext as DiagramBuilderVM).OnSaveAsCommand(null); 
        }

        private void OnOpenCommand(object param)
        {
            (this.DataContext as DiagramBuilderVM).OnImportCommand(null); 
        }

        private void OnRulerVisibilityCommand(object param)
        {
            if ((((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).PageSettings as PageVM).Ruler)
                (((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).PageSettings as PageVM).Ruler = false;
            else
                (((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).PageSettings as PageVM).Ruler = true;
        }

        private void OnSmartGuideCommand(object param)
        {
            if ((((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).PageSettings as PageVM).SnapToObject)
                (((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).PageSettings as PageVM).SnapToObject = false;
            else
                (((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).PageSettings as PageVM).SnapToObject = true;
        }

        private void OnGridLinesCommand(object param)
        {
            if ((((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).PageSettings as PageVM).GridLines)
                (((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).PageSettings as PageVM).GridLines = false;
            else
                (((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).PageSettings as PageVM).GridLines = true;
        }

        private void OnLockCommand(object param)
        {
            (((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).SelectedItems as SelectorVM).IsNodeLocked = true;
        }

        private void OnUnLockCommand(object param)
        {
            foreach (INode node in ((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).Nodes as ObservableCollection<NodeVM>)
            {
                node.Constraints = NodeConstraints.Default;
            }

            foreach (IGroup group in ((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).Groups as ObservableCollection<GroupVM>)
            {
                group.Constraints = NodeConstraints.Default;
            }
        }

        private void OnUnSelectCommand(object param)
        {
            foreach (INode node in ((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).Nodes as ObservableCollection<NodeVM>)
            {
                node.IsSelected = false;
            }

            foreach (IGroup group in ((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).Groups as ObservableCollection<GroupVM>)
            {
                group.IsSelected = false;
            }
        }

        private void OnPanCommand(object param)
        {
            if (_mEnablePan)
                ((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).IsPanEnabled = true;
            else
                ((this.DataContext as DiagramBuilderVM).SelectedDiagram as DiagramVM).IsPanEnabled = false;
        }

        

        #region Library Visibility
        private void OnLibraryCommand(object param)
        {
            if (childgrid.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                libraryToggleSwitch.IsOn = true;
            }
            else
            {
                libraryToggleSwitch.IsOn = false;
            }
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (childgrid != null && collapsedStencilGrid != null)
            {
                if ((sender as ToggleSwitch).IsOn)
                {
                    childgrid.Visibility = Visibility.Visible;
                    collapsedStencilGrid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    childgrid.Visibility = Visibility.Collapsed;
                    collapsedStencilGrid.Visibility = Visibility.Visible;
                }
            }
        }
        #endregion

        #endregion        

        private void Execute(KeyRoutedEventArgs e)
        {
            if (KeyModifiers == (VirtualKeyModifiers.Control | VirtualKeyModifiers.Shift) && CurrentKey == VirtualKey.N && KeyState == CoreVirtualKeyStates.Down)
                ClonePage.Execute(null);
            else if (KeyModifiers == VirtualKeyModifiers.Control && CurrentKey == VirtualKey.N && KeyState == CoreVirtualKeyStates.Down)
                NewPage.Execute(null);
            else if (KeyModifiers == (VirtualKeyModifiers.Control | VirtualKeyModifiers.Shift) && CurrentKey == VirtualKey.S && KeyState == CoreVirtualKeyStates.Down)
                SaveAs.Execute(null);
            else if (KeyModifiers == VirtualKeyModifiers.Control && CurrentKey == VirtualKey.S && KeyState == CoreVirtualKeyStates.Down)
                Save.Execute(null);
            else if (KeyModifiers == VirtualKeyModifiers.Control && CurrentKey == VirtualKey.O && KeyState == CoreVirtualKeyStates.Down)
                Open.Execute(null);
            else if (KeyModifiers == VirtualKeyModifiers.Control && CurrentKey == VirtualKey.L && KeyState == CoreVirtualKeyStates.Down)
                Library.Execute(null);
            else if (KeyModifiers == VirtualKeyModifiers.Control && CurrentKey == (VirtualKey)186 && KeyState == CoreVirtualKeyStates.Down)
                GridLines.Execute(null);
            else if (KeyModifiers == VirtualKeyModifiers.Control && CurrentKey == VirtualKey.U && KeyState == CoreVirtualKeyStates.Down)
                SmartGuide.Execute(null);
            else if (KeyModifiers == VirtualKeyModifiers.Control && CurrentKey == VirtualKey.R && KeyState == CoreVirtualKeyStates.Down)
                RulerVisibility.Execute(null);
            else if (KeyModifiers == (VirtualKeyModifiers.Control | VirtualKeyModifiers.Shift) && CurrentKey == VirtualKey.A && KeyState == CoreVirtualKeyStates.Down)
                UnSelect.Execute(null);
            else if (KeyModifiers == VirtualKeyModifiers.Control && CurrentKey == VirtualKey.Number2 && KeyState == CoreVirtualKeyStates.Down)
                Lock.Execute(null);
            else if (KeyModifiers == VirtualKeyModifiers.Control && CurrentKey == VirtualKey.Number3 && KeyState == CoreVirtualKeyStates.Down)
                UnLock.Execute(null);
            else if (KeyModifiers == VirtualKeyModifiers.Control && CurrentKey == VirtualKey.C && KeyState == CoreVirtualKeyStates.Down)
            {
                Copy.Execute(null);
                e.Handled = true;
            }
            else if (KeyModifiers == VirtualKeyModifiers.Control && CurrentKey == VirtualKey.V && KeyState == CoreVirtualKeyStates.Down)
            {
                Paste.Execute(null);
                e.Handled = true;
            }
        }        

        bool IsControl(VirtualKey s)
        {
            return s == VirtualKey.Control || s == VirtualKey.LeftControl || s == VirtualKey.RightControl;
        }

        bool IsShift(VirtualKey s)
        {
            return s == VirtualKey.LeftShift || s == VirtualKey.RightShift || s == VirtualKey.Shift;
        }

        protected override void OnKeyUp(KeyRoutedEventArgs e)
        {
            base.OnKeyUp(e);
            if (!IsControl(e.Key)
                && !IsShift(e.Key))
            {
                Keys.Remove(e.Key);
            }
            //CurrentKey = e.Key;
            KeyState = CoreVirtualKeyStates.None;
            Execute(e);
            if (Keys.Count > 0)
            {
                KeyState = CoreVirtualKeyStates.Down;
            }
            if (IsControl(e.Key))
            {
                KeyModifiers ^= VirtualKeyModifiers.Control;
            }
            else if (IsShift(e.Key))
            {
                KeyModifiers ^= VirtualKeyModifiers.Shift;
            }

            if (Keys.Count > 0)
            {
                CurrentKey = Keys.Last();
            }
            else
                CurrentKey = VirtualKey.None;
        }
        #endregion

        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            base.OnKeyDown(e);
            EditMode = true;
            settingbutton.IsChecked = false;
            #region Keyboard Commands
            KeyState = CoreVirtualKeyStates.Down;

            if (IsControl(e.Key))
            {
                KeyModifiers |= VirtualKeyModifiers.Control;
            }
            else if (IsShift(e.Key))
            {
                KeyModifiers |= VirtualKeyModifiers.Shift;
            }
            else
            {
                CurrentKey = e.Key;
                if (!Keys.Contains(e.Key))
                    Keys.Add(e.Key);
            }
            Execute(e);
            //e.Handled = true;
            #endregion
        }        

        #region Open / Close Setting Popup

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            base.OnPointerPressed(e);
            settingbutton.IsChecked = false;
            //settingpopup.IsOpen = false;
        }

        private void settingbutton_Checked(object sender, RoutedEventArgs e)
        {
            settingpopup.IsOpen = true;
        }

        private void settingbutton_Unchecked(object sender, RoutedEventArgs e)
        {
            settingpopup.IsOpen = false;
        }

        #endregion
        

        #region DependencyProperties
        public bool EditMode
        {
            get { return (bool)GetValue(EditModeProperty); }
            set { SetValue(EditModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditModeProperty =
            DependencyProperty.Register("EditMode", typeof(bool), typeof(FrameStudio), new PropertyMetadata(true, OnEditModeChanged));
        public bool ViewMode
        {
            get { return (bool)GetValue(ViewModeProperty); }
            set { SetValue(ViewModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModeProperty =
            DependencyProperty.Register("ViewMode", typeof(bool), typeof(FrameStudio), new PropertyMetadata(false, OnViewModeChanged));

        public bool EditTitle
        {
            get { return (bool)GetValue(EditTitleProperty); }
            set { SetValue(EditTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _mEditTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditTitleProperty =
            DependencyProperty.Register("EditTitle", typeof(bool), typeof(FrameStudio), new PropertyMetadata(true, OnEditTitleChanged));




        public Brush TitleBackground
        {
            get { return (Brush)GetValue(TitleBackgroundProperty); }
            set { SetValue(TitleBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleBackgroundProperty =
            DependencyProperty.Register("TitleBackground", typeof(Brush), typeof(FrameStudio), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        
        #endregion

        #region Callback
        private static void OnEditModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameStudio fs = d as FrameStudio;
            if ((bool)e.NewValue)
                fs.ViewMode = false;
            else
                fs.ViewMode = true;
        }
        
        private static void OnViewModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameStudio fs = d as FrameStudio;
            if ((bool)e.NewValue)
            {
                fs.EditMode = false;
                fs.linksbutton.IsChecked = true;
                foreach (var item in (fs.DataContext as DiagramBuilderVM).Diagrams)
                {
                    fs.settingpopup.IsOpen = false;
                    if (fs.rulerVisiblitySwitch.IsOn)
                        fs._mRulerEnabledOnEditMode = true;
                    else
                        fs._mRulerEnabledOnEditMode = false;

                    (item.PageSettings as PageVM).Ruler = false;
                    foreach (var node in item.NodeCollection)
                    {
                        node.IsSelected = false;
                        node.Constraints = node.Constraints & ~NodeConstraints.Selectable;
                        node.EditMode = false;
                        fs.AvoidLabelEditinViewMode(node as INodeVM);
                    }
                }
            }
            else
            {
                fs.EditMode = true;
                fs.linksbutton.IsChecked = false;

                if (fs._mRulerEnabledOnEditMode)
                    fs.rulerVisiblitySwitch.IsOn = true;
                else
                    fs.rulerVisiblitySwitch.IsOn = false;

                foreach (var item in (fs.DataContext as DiagramBuilderVM).Diagrams)
                {
                    foreach (var node in item.NodeCollection)
                    {
                        node.Constraints = node.Constraints | NodeConstraints.Selectable;
                        node.EditMode = true;
                    }
                }
            }
        }

        private static void OnEditTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //FrameStudio fs = d as FrameStudio;
            //if ((bool)e.NewValue)
            //    fs.TitleTemplate = fs.Resources["TabbedDiagramTitleTextBox"] as DataTemplate;            
            //else
            //    fs.TitleTemplate = fs.Resources["TabbedDiagramTitleTextBlock"] as DataTemplate;
        }

        private static void OnTitleTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        private void AvoidLabelEditinViewMode(INodeVM node)
        {
            foreach (LabelVM _mAnnotation in node.Annotations as List<IAnnotation>)
            {
                _mAnnotation.PropertyChanged += (s, e) =>
                {
                    if (this.ViewMode && e.PropertyName == "Mode")
                    {
                        if ((s as LabelVM).Mode == ContentEditorMode.Edit)
                            (s as LabelVM).Mode = ContentEditorMode.View;
                    }
                };
            }
        }

        private void TabbedDiagrams_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Point position = e.GetPosition(rootgrid);
        }

        private void GetParent(DependencyObject sender)
        {
            var parent = VisualTreeHelper.GetParent(sender as DependencyObject);
            if (parent.GetType() == typeof(SfRadialMenuItem))
            {
                //if ((parent as SfRadialMenuItem).Name == "Draw" || (parent as SfRadialMenuItem).Name == "Selection" || (parent as SfRadialMenuItem).Name == "Panels" || (parent as SfRadialMenuItem).Name == "Picture" || (parent as SfRadialMenuItem).Name == "Group" || (parent as SfRadialMenuItem).Name == "Copy")
                //{
                //    radialMenu.IsOpen = true;
                //}
                //else
                //{
                //    radialMenu.IsOpen = false;
                //}
            }
            else
            {
                GetParent(parent);
            }
        }        

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            childgrid.Visibility = Visibility.Visible;
            collapsedStencilGrid.Visibility = Visibility.Collapsed;
            VisualStateManager.GoToState(sender as ToggleButton, "UnChecked", true);

            if ((sender as ToggleButton).Name == "searchAllButton")
            {
                stencilview.All();
            }
            else if ((sender as ToggleButton).Name == "searchCategoryButton")
            {
                stencilview.Category();
            }
            else if ((sender as ToggleButton).Name == "searchsymbolButton")
            {
                stencilview.SearchSymbol();
            }
        }

        private void collapseStencilButton_Click(object sender, RoutedEventArgs e)
        {
            childgrid.Visibility = Visibility.Collapsed;
            collapsedStencilGrid.Visibility = Visibility.Visible;
        }

        #region Change Mode (View / Edit)
        private void viewmodebutton_Click(object sender, RoutedEventArgs e)
        {
            EditMode = false;
        } 

        private void editmodebutton_Click(object sender, RoutedEventArgs e)
        {
            ViewMode = false;
        }
        #endregion

        #region Highlight the Links in ViewMode

        private void HighlightLinks()
        {
            foreach (var item in (this.DataContext as DiagramBuilderVM).Diagrams)
            {
                foreach (var node in item.NodeCollection)
                {
                    if (node.Link != null)
                    {
                        if (!string.IsNullOrEmpty(node.Link.Link))
                        {
                            //node.EditModeFill = new SolidColorBrush(Colors.Blue);
                            node.EditModeFill = GetColorFromHexa("#6c129b");
                        }
                    }
                    if (node.Shape.Equals("checkbox") || node.Shape.Equals("radiobutton"))
                    {
                        if (node.Link != null)
                        {
                            if (!string.IsNullOrEmpty(node.Link.Link))
                            {
                                (node.DataContext as CheckBoxBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                            }
                        }
                    }
                    else if (node.Shape.Equals("combobox"))
                    {
                        if (node.Link != null)
                        {
                            if (!string.IsNullOrEmpty(node.Link.Link))
                            {
                                (node.DataContext as ComboBoxBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                            }
                        }
                    }
                    else if (node.Shape.Equals("searchbutton"))
                    {
                        if (node.Link != null)
                        {
                            if (!string.IsNullOrEmpty(node.Link.Link))
                            {
                                (node.DataContext as SearchBoxBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                            }
                        }
                    }
                    else if (node.Shape.Equals("tabsbar") || node.Shape.Equals("verticaltab"))
                    {
                        foreach (var i in (node.DataContext as TabBusinessClass).Items)
                        {
                            if ((i as TabItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as TabItemBusinessClass).Link.Link))
                            {
                                //(i as TabItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Blue);
                                (i as TabItemBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                            }
                        }
                    }
                    else if(node.Shape.Equals("buttonbar"))
                    {
                        foreach (var i in (node.DataContext as ButtonBarBusinessClass).Items)
                        {
                            if ((i as ButtonBarItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as ButtonBarItemBusinessClass).Link.Link))
                            {
                                //(i as ButtonBarItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Blue);
                                (i as ButtonBarItemBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                            }
                        }
                    }
                    else if (node.Shape.Equals("checkboxgroup"))
                    {
                        foreach (var i in (node.DataContext as CheckBoxGroupBusinessClass).Items)
                        {
                            if ((i as CheckBoxItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as CheckBoxItemBusinessClass).Link.Link))
                            {
                                //(i as CheckBoxItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Blue);
                                (i as CheckBoxItemBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                            }
                        }
                    }
                    else if (node.Shape.Equals("radiobuttongroup"))
                    {
                        foreach (var i in (node.DataContext as RadioButtonGroupBusinessClass).Items)
                        {
                            if ((i as RadioButtonItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as RadioButtonItemBusinessClass).Link.Link))
                            {
                                //(i as RadioButtonItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Blue);
                                (i as RadioButtonItemBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                            }
                        }
                    }
                    else if (node.Shape.Equals("menubar"))
                    {
                        foreach (var i in (node.DataContext as MenuBarBusinessClass).Items)
                        {
                            if ((i as MenuTitleBusinessClass).Link != null && !string.IsNullOrEmpty((i as MenuTitleBusinessClass).Link.Link))
                            {
                                //(i as MenuTitleBusinessClass).EditModeFill = new SolidColorBrush(Colors.Blue);
                                (i as MenuTitleBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                            }
                        }
                    }
                    else if (node.Shape.Equals("menu"))
                    {
                        foreach (var i in (node.DataContext as MenuBusinessClass).Items)
                        {
                            if ((i as MenuItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as MenuItemBusinessClass).Link.Link))
                            {
                                //(i as MenuItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Blue);
                                (i as MenuItemBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                            }
                        }
                    }
                    else if (node.Shape.Equals("list"))
                    {
                        foreach (var i in (node.DataContext as ListBusinessClass).Items)
                        {
                            if ((i as ListItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as ListItemBusinessClass).Link.Link))
                            {
                                //(i as ListItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Blue);
                                (i as ListItemBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                            }
                        }
                    }
                    else if (node.Shape.Equals("combobox"))
                    {
                        if ((node.DataContext as ComboBoxBusinessClass).Link != null && !string.IsNullOrEmpty((node.DataContext as ComboBoxBusinessClass).Link.Link))
                        {
                            (node.DataContext as ComboBoxBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                        }
                    }
                    else if (node.Shape.Equals("breadcrumbs"))
                    {
                        foreach (var i in (node.DataContext as BreadcrumbsBusinessClass).Items)
                        {
                            if ((i as BreadcrumbsItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as BreadcrumbsItemBusinessClass).Link.Link))
                            {
                                //(i as BreadcrumbsItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Blue);
                                (i as BreadcrumbsItemBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                            }
                        }
                    }
                    else if (node.Shape.Equals("linkbar"))
                    {
                        foreach (var i in (node.DataContext as LinkBarBusinessClass).Items)
                        {
                            if ((i as LinkBarItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as LinkBarItemBusinessClass).Link.Link))
                            {
                                //(i as LinkBarItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Blue);
                                (i as LinkBarItemBusinessClass).EditModeFill = GetColorFromHexa("#6c129b");
                            }
                        }
                    }
                    else if (node.Shape.Equals("alertbox"))
                    {
                        if ((node.DataContext as DialogBoxBusinessObject).OkLink != null
                            && !string.IsNullOrEmpty((node.DataContext as DialogBoxBusinessObject).OkLink.Link))
                        {
                            //(node.DataContext as DialogBoxBusinessObject).OkEditModeFill = new SolidColorBrush(Colors.Blue);
                            (node.DataContext as DialogBoxBusinessObject).OkEditModeFill = GetColorFromHexa("#6c129b");
                            (node.DataContext as DialogBoxBusinessObject).OkEditModeOpacity = 0.3;
                        }
                        
                        if ((node.DataContext as DialogBoxBusinessObject).CancelLink != null
                            && !string.IsNullOrEmpty((node.DataContext as DialogBoxBusinessObject).CancelLink.Link))
                        {
                            //(node.DataContext as DialogBoxBusinessObject).CancelEditModeFill = new SolidColorBrush(Colors.Blue);
                            (node.DataContext as DialogBoxBusinessObject).CancelEditModeFill = GetColorFromHexa("#6c129b");
                            (node.DataContext as DialogBoxBusinessObject).CancelEditModeOpacity = 0.3;
                        }
                    }
                    else if (node.Shape.Equals("messagebox"))
                    {
                        if ((node.DataContext as MessageBoxBusinessObject).Link != null
                            && !string.IsNullOrEmpty((node.DataContext as MessageBoxBusinessObject).Link.Link))
                        {
                            //(node.DataContext as MessageBoxBusinessObject).EditModeFill = new SolidColorBrush(Colors.Blue);
                            (node.DataContext as MessageBoxBusinessObject).EditModeFill = GetColorFromHexa("#6c129b");
                        }                        
                    }
                }
            }
        }

        private void RemoveLinkHighlight()
        {
            foreach (var item in (this.DataContext as DiagramBuilderVM).Diagrams)
            {
                foreach (var node in item.NodeCollection)
                {
                    if (node.Link != null)
                    {
                        if (!string.IsNullOrEmpty(node.Link.Link))
                        {
                            node.EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                    if (node.Shape.Equals("checkbox") || node.Shape.Equals("radiobutton"))
                    {
                        if (node.Link != null)
                        {
                            if (!string.IsNullOrEmpty(node.Link.Link))
                            {
                                (node.DataContext as CheckBoxBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                    else if (node.Shape.Equals("combobox"))
                    {
                        if (node.Link != null)
                        {
                            if (!string.IsNullOrEmpty(node.Link.Link))
                            {
                                (node.DataContext as ComboBoxBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                    else if (node.Shape.Equals("searchbutton"))
                    {
                        if (node.Link != null)
                        {
                            if (!string.IsNullOrEmpty(node.Link.Link))
                            {
                                (node.DataContext as SearchBoxBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                    else if (node.Shape.Equals("tabsbar") || node.Shape.Equals("verticaltab"))
                    {
                        foreach (var i in (node.DataContext as TabBusinessClass).Items)
                        {
                            if ((i as TabItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as TabItemBusinessClass).Link.Link))
                            {
                                (i as TabItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                    else if (node.Shape.Equals("buttonbar"))
                    {
                        foreach (var i in (node.DataContext as ButtonBarBusinessClass).Items)
                        {
                            if ((i as ButtonBarItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as ButtonBarItemBusinessClass).Link.Link))
                            {
                                (i as ButtonBarItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                    else if (node.Shape.Equals("checkboxgroup"))
                    {
                        foreach (var i in (node.DataContext as CheckBoxGroupBusinessClass).Items)
                        {
                            if ((i as CheckBoxItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as CheckBoxItemBusinessClass).Link.Link))
                            {
                                (i as CheckBoxItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                    else if (node.Shape.Equals("radiobuttongroup"))
                    {
                        foreach (var i in (node.DataContext as RadioButtonGroupBusinessClass).Items)
                        {
                            if ((i as RadioButtonItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as RadioButtonItemBusinessClass).Link.Link))
                            {
                                (i as RadioButtonItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                    else if (node.Shape.Equals("menubar"))
                    {
                        foreach (var i in (node.DataContext as MenuBarBusinessClass).Items)
                        {
                            if ((i as MenuTitleBusinessClass).Link != null && !string.IsNullOrEmpty((i as MenuTitleBusinessClass).Link.Link))
                            {
                                (i as MenuTitleBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                    else if (node.Shape.Equals("menu"))
                    {
                        foreach (var i in (node.DataContext as MenuBusinessClass).Items)
                        {
                            if ((i as MenuItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as MenuItemBusinessClass).Link.Link))
                            {
                                (i as MenuItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                    else if (node.Shape.Equals("list"))
                    {
                        foreach (var i in (node.DataContext as ListBusinessClass).Items)
                        {
                            if ((i as ListItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as ListItemBusinessClass).Link.Link))
                            {
                                (i as ListItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                    else if (node.Shape.Equals("combobox"))
                    {
                        if ((node.DataContext as ComboBoxBusinessClass).Link != null && !string.IsNullOrEmpty((node.DataContext as ComboBoxBusinessClass).Link.Link))
                        {
                            (node.DataContext as ComboBoxBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                    else if (node.Shape.Equals("breadcrumbs"))
                    {
                        foreach (var i in (node.DataContext as BreadcrumbsBusinessClass).Items)
                        {
                            if ((i as BreadcrumbsItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as BreadcrumbsItemBusinessClass).Link.Link))
                            {
                                (i as BreadcrumbsItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                    else if (node.Shape.Equals("linkbar"))
                    {
                        foreach (var i in (node.DataContext as LinkBarBusinessClass).Items)
                        {
                            if ((i as LinkBarItemBusinessClass).Link != null && !string.IsNullOrEmpty((i as LinkBarItemBusinessClass).Link.Link))
                            {
                                (i as LinkBarItemBusinessClass).EditModeFill = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                    else if (node.Shape.Equals("alertbox"))
                    {
                        if ((node.DataContext as DialogBoxBusinessObject).OkLink != null
                            && !string.IsNullOrEmpty((node.DataContext as DialogBoxBusinessObject).OkLink.Link))
                        {
                            (node.DataContext as DialogBoxBusinessObject).OkEditModeFill = new SolidColorBrush(Colors.Transparent);
                        }

                        if ((node.DataContext as DialogBoxBusinessObject).CancelLink != null
                            && !string.IsNullOrEmpty((node.DataContext as DialogBoxBusinessObject).CancelLink.Link))
                        {
                            (node.DataContext as DialogBoxBusinessObject).CancelEditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                    else if (node.Shape.Equals("messagebox"))
                    {
                        if ((node.DataContext as MessageBoxBusinessObject).Link != null
                            && !string.IsNullOrEmpty((node.DataContext as MessageBoxBusinessObject).Link.Link))
                        {
                            (node.DataContext as MessageBoxBusinessObject).EditModeFill = new SolidColorBrush(Colors.Transparent);
                        }
                    }
                }
            }
        }

        private void linksbutton_Checked(object sender, RoutedEventArgs e)
        {
            HighlightLinks();
        }

        private void linksbutton_Unchecked(object sender, RoutedEventArgs e)
        {
            RemoveLinkHighlight();
        }        
        #endregion

        #region Enable / Disable the Markups
        private void markupbutton_Click(object sender, RoutedEventArgs e)
        {
            markupbutton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            showmarkupbutton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            foreach (var item in (this.DataContext as DiagramBuilderVM).Diagrams)
            {
                foreach (var node in item.NodeCollection)
                {
                    if ((node as INodeVM).Shape.Equals("stickynote")
                        || (node as INodeVM).Shape.Equals("redx")
                        || (node as INodeVM).Shape.Equals("horizontalcurlybraces")
                        || (node as INodeVM).Shape.Equals("verticalcurlybraces")
                        || (node as INodeVM).Shape.Equals("scratchout")
                        )
                    {
                        (node as INodeVM).Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                }
            }
        }

        private void showmarkupbutton_Click(object sender, RoutedEventArgs e)
        {
            markupbutton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            showmarkupbutton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            foreach (var item in (this.DataContext as DiagramBuilderVM).Diagrams)
            {
                foreach (var node in item.NodeCollection)
                {
                    (node as INodeVM).Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
            }
        }
        #endregion

        
        private void StackPanel_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            EditTitle = true;
        }

        private void titletextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            EditTitle = false;
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

        private void addNewPageButton_Click(object sender, RoutedEventArgs e)
        {
            EditTitle = true;
        }

        #region While Closing the Page 
        // Click Yes / No button
        private void savepopupbutton_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).CommandParameter = closeDiagram;
            savepopup.IsOpen = false;
            closeDiagram = null;
        }
        // Click Cancel button
        private void cancelpopupbutton_Click(object sender, RoutedEventArgs e)
        {
            savepopup.IsOpen = false;
        }

        object closeDiagram;
        // Click on the Close mark in tabbed item 
        private void closebutton_Click(object sender, RoutedEventArgs e)
        {
            savepopup.IsOpen = true;            
            closeDiagram = (sender as Button).DataContext;
        }

        // While Opening the Save popup
        private void savepopup_Opened(object sender, object e)
        {
            rootgrid.IsHitTestVisible = false;
            rootgrid.Opacity = 0.45;
            this.TopAppBar.IsOpen = false;
            this.BottomAppBar.IsOpen = false;
        }

        // While Closing the Save Popup
        private void savepopup_Closed(object sender, object e)
        {
            rootgrid.IsHitTestVisible = true;
            rootgrid.Opacity = 1;
        }
        #endregion      

        private void selectDiagramToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            (sender as ToggleButton).Foreground = GetColorFromHexa("#ffffff");
            //TitleBackground = GetColorFromHexa("#50545b");
        }

        private void selectDiagramToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            (sender as ToggleButton).Foreground = GetColorFromHexa("#50545b");
            //TitleBackground = GetColorFromHexa("#3e3f42");
        }

        private void selectDiagramToggleButton_PointerMoved(object sender, PointerRoutedEventArgs e)
        {

        }

        private void This_KeyUp(object sender, KeyRoutedEventArgs e)
        {

        }                  
    }
}
