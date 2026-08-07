using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using System.Collections.ObjectModel;
using Mockup.BusinessObject;
using Mockup.ViewModel;
using Windows.UI.Xaml.Controls.Primitives;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.SampleBrowser.UWP.Diagram;

namespace Mockup.BusinessObject 
{
    class CustomBinding
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(CustomBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            TextBox textbox = depObj as TextBox;
            if (textbox != null)
            {
                textbox.Loaded += textbox_Loaded;
                //textbox.LostFocus += textbox_LostFocus;
                textbox.GotFocus += textbox_GotFocus;
                textbox.PointerCaptureLost += textbox_PointerCaptureLost;
            }

        }

        static void textbox_PointerCaptureLost(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            
        }

        static void textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
            (sender as TextBox).LostFocus += textbox_LostFocus;
        }

        static void textbox_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        static void textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).DataContext != null)
                ((sender as TextBox).DataContext as DiagramVM).EditTile = false;
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class HyperLinkBinding
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(HyperLinkBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            TextBox textbox = depObj as TextBox;
            if (textbox != null)
            {
                textbox.TextChanged += textbox_TextChanged;
            }

        }

        static void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var datacontext = (sender as TextBox).DataContext;

            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                //if ((node as INodeVM).Shape.Equals("dialogbox") || (node as INodeVM).Shape.Equals("alertbox"))
                //{
                //    foreach (LabelVM hyperlink in (node as NodeVM).Annotations as List<IAnnotation>)
                //    {
                //        if ((sender as TextBox).Name == "hyperlink1")
                //            {
                //                (hyperlink.DataContext as DialogBoxBusinessObject).CancelHyperLink = (sender as TextBox).Text;
                //            }
                //            else if ((sender as TextBox).Name == "hyperlink2")
                //                (hyperlink.DataContext as DialogBoxBusinessObject).OkHyperLink = (sender as TextBox).Text;
                       
                //    }
                //}
                //else if ((node as INodeVM).Shape.Equals("messagebox"))
                //{
                //    foreach (LabelVM hyperlink in (node as NodeVM).Annotations as List<IAnnotation>)
                //    {
                //        if ((sender as TextBox).Name == "hyperlink1")
                //        {
                //            (hyperlink.DataContext as DialogBoxBusinessObject).OkHyperLink = (sender as TextBox).Text;
                //        }
                //    }
                //}
                if ((node as INodeVM).Shape.Equals("multilinebutton")
                    || (node as INodeVM).Shape.Equals("searchbutton")
                    || (node as INodeVM).Shape.Equals("combobox")
                    )
                {
                    foreach (LabelVM label in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                        if ((sender as TextBox).Name == "hyperlink1")
                        {
                            label.HyperLink = (sender as TextBox).Text;
                        }
                    }
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class LinkBinding
    {
        static object datacontext;
        static Popup popup;
        static ComboBox combobox;
        static string weburl;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
                typeof(LinkBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ComboBox combo = depObj as ComboBox;
            combo.SelectionChanged += combo_SelectionChanged;
            datacontext = combo.DataContext;
        }

        static private SolidColorBrush GetColorFromHexa(string hexaColor)
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

        static void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            combobox = sender as ComboBox;
            if ((sender as ComboBox).SelectedIndex >= 0)
            {
                foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
                {
                    #region SelectedIndex = 0
                    if ((sender as ComboBox).SelectedIndex == 0)
                    {
                        if ((node as NodeVM).Link != null)
                        {
                            (node as NodeVM).Link.Link = string.Empty;
                            (node as NodeVM).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                        }
                    }
                    #endregion
                    #region SelectedIndex = 1
                    else if ((sender as ComboBox).SelectedIndex == 1)
                    {
                       // int? selectedIndex = null;

                        //if ((node as NodeVM).Link != null)
                        //{
                        //    selectedIndex = (node as NodeVM).Link.SelectedLinkIndex;
                        //}
                        //else
                        //{
                        //    foreach (var item in ((node as NodeVM).DataContext as ICommonForCollection).Items)
                        //    {
                        //        if ((item as ICommonForCollectionItem).Item == combobox.Tag.ToString())
                        //        {
                        //            if ((item as ICommonForCollectionItem).Link != null)
                        //                selectedIndex = (item as ICommonForCollectionItem).Link.SelectedLinkIndex;
                        //        }
                        //    }
                        //}

                        //if (selectedIndex != null && selectedIndex != (sender as ComboBox).SelectedIndex)
                        {
                            string hyperlink = string.Empty;
                            if ((node as NodeVM).Shape.Equals("alertbox"))
                            { 
                                if(((node as NodeVM).DataContext as DialogBoxBusinessObject).Ok == combobox.Tag.ToString())
                                {
                                    ((node as NodeVM).DataContext as DialogBoxBusinessObject).OkLink.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                                else if(((node as NodeVM).DataContext as DialogBoxBusinessObject).Cancel == combobox.Tag.ToString())
                                {
                                    ((node as NodeVM).DataContext as DialogBoxBusinessObject).CancelLink.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                            }
                            else if ((node as NodeVM).Shape.Equals("messagebox"))
                            {
                                if (((node as NodeVM).DataContext as MessageBoxBusinessObject).Ok == combobox.Tag.ToString())
                                {
                                    ((node as NodeVM).DataContext as MessageBoxBusinessObject).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                            }
                            else if ((node as NodeVM).Link != null)
                            {
                                //hyperlink = (node as NodeVM).Link.Link;
                                hyperlink = (node as NodeVM).Link.WebURL;
                                (node as NodeVM).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                            }
                            else
                            {
                                if ((node as NodeVM).DataContext is ICommonForCollection && ((node as NodeVM).DataContext as ICommonForCollection).Items != null)
                                {
                                    foreach (var item in ((node as NodeVM).DataContext as ICommonForCollection).Items)
                                    {
                                        if ((item as ICommonForCollectionItem).Item == combobox.Tag.ToString())
                                        {
                                            if ((item as ICommonForCollectionItem).Link != null)
                                            {
                                                //hyperlink = (item as ICommonForCollectionItem).Link.Link;
                                                hyperlink = (item as ICommonForCollectionItem).Link.WebURL;
                                                (item as ICommonForCollectionItem).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                            }
                                        }
                                    }
                                }
                            }
                            popup = new Popup();
                            
                            popup.HorizontalAlignment = HorizontalAlignment.Center;
                            popup.VerticalAlignment = VerticalAlignment.Center;

                            popup.HorizontalOffset = Window.Current.Bounds.Width / 2 - 250;
                            popup.VerticalOffset = Window.Current.Bounds.Height / 2 - 90;
                            Border border = new Border() { Background = new SolidColorBrush(Colors.White), Width = 500, Height = 180, BorderThickness = new Thickness(2), BorderBrush = GetColorFromHexa("#808285") };
                            
                            StackPanel verticalpanel = new StackPanel();
                            border.Child = verticalpanel;
                            //verticalpanel.Background = new SolidColorBrush(Colors.Gray);
                            TextBlock textblock = new TextBlock() { Margin =new Thickness(10,20,0,0), FontSize = 16, Foreground = new SolidColorBrush(Colors.Black) };
                            textblock.Text = "Link to Web Page URL";

                            TextBox textbox = new TextBox() {Margin = new Thickness(10, 20, 10, 0), BorderThickness = new Thickness(2), BorderBrush = new SolidColorBrush(Colors.Black), HorizontalAlignment = HorizontalAlignment.Stretch};
                            
                            if (hyperlink != null)
                                textbox.Text = hyperlink;
                            textbox.TextChanged += textbox_TextChanged;
                            verticalpanel.Children.Add(textblock);
                            verticalpanel.Children.Add(textbox);

                            StackPanel buttonPanel = new StackPanel() { Margin = new Thickness(0,20,10,10), HorizontalAlignment = HorizontalAlignment.Right};
                            buttonPanel.Orientation = Orientation.Horizontal;
                            Button okButton = new Button() {Width = 100, Height = 40, Background = GetColorFromHexa("#d1d3d4"), Content = "Ok" };
                            okButton.Click += okButton_Click;

                            Button cancelButton = new Button() { Width = 100, Height = 40, Background = GetColorFromHexa("#d1d3d4"), Content = "Cancel" };
                            cancelButton.Click += cancelButton_Click;
                            buttonPanel.Children.Add(okButton);
                            buttonPanel.Children.Add(cancelButton);

                            
                            //verticalpanel.Children.Add(panel);
                            verticalpanel.Children.Add(buttonPanel);
                            popup.Child = border;
                            popup.IsOpen = true;
                        }
                    }
                    #endregion
                    else
                    {
                        if ((node as NodeVM).Link != null)
                        {
                            (node as NodeVM).Link.InternalLink = true;
                            (node as NodeVM).Link.Link = (sender as ComboBox).SelectedItem.ToString();
                            (node as NodeVM).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                        }
                        if ((node as INodeVM).Shape.Equals("alertbox"))
                        {
                            if (combobox.Tag.ToString() == ((node as INodeVM).DataContext as DialogBoxBusinessObject).Ok)
                            {
                                ((node as INodeVM).DataContext as DialogBoxBusinessObject).OkLink.Link = (sender as ComboBox).SelectedItem.ToString();
                                ((node as INodeVM).DataContext as DialogBoxBusinessObject).OkLink.InternalLink = true;
                                ((node as INodeVM).DataContext as DialogBoxBusinessObject).OkLink.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                            }
                            else if (combobox.Tag.ToString() == ((node as INodeVM).DataContext as DialogBoxBusinessObject).Cancel)
                            {
                                ((node as INodeVM).DataContext as DialogBoxBusinessObject).CancelLink.Link = (sender as ComboBox).SelectedItem.ToString();
                                ((node as INodeVM).DataContext as DialogBoxBusinessObject).CancelLink.InternalLink = true;
                                ((node as INodeVM).DataContext as DialogBoxBusinessObject).CancelLink.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                            }
                        }
                        if ((node as INodeVM).Shape.Equals("messagebox"))
                        {
                            if (combobox.Tag.ToString() == ((node as INodeVM).DataContext as MessageBoxBusinessObject).Ok)
                            {
                                ((node as INodeVM).DataContext as MessageBoxBusinessObject).Link.Link = (sender as ComboBox).SelectedItem.ToString();
                                ((node as INodeVM).DataContext as MessageBoxBusinessObject).Link.InternalLink = true;
                                ((node as INodeVM).DataContext as MessageBoxBusinessObject).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                            }
                        }
                        else if ((node as INodeVM).Shape.Equals("tabsbar") || (node as INodeVM).Shape.Equals("verticaltab"))
                        {
                            foreach (var tabitem in ((node as INodeVM).DataContext as TabBusinessClass).Items)
                            {
                                if (combobox.Tag.ToString() == (tabitem as TabItemBusinessClass).Item)
                                {
                                    (tabitem as TabItemBusinessClass).Link.Link = (sender as ComboBox).SelectedItem.ToString();
                                    (tabitem as TabItemBusinessClass).Link.InternalLink = true;
                                    (tabitem as TabItemBusinessClass).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                            }
                        }
                        else if ((node as INodeVM).Shape.Equals("buttonbar"))
                        {
                            foreach (var tabitem in ((node as INodeVM).DataContext as ButtonBarBusinessClass).Items)
                            {
                                if (combobox.Tag.ToString() == (tabitem as ButtonBarItemBusinessClass).Item)
                                {
                                    (tabitem as ButtonBarItemBusinessClass).Link.Link = (sender as ComboBox).SelectedItem.ToString();
                                    (tabitem as ButtonBarItemBusinessClass).Link.InternalLink = true;
                                    (tabitem as ButtonBarItemBusinessClass).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                            }
                        }
                        else if ((node as INodeVM).Shape.Equals("checkboxgroup"))
                        {
                            foreach (var tabitem in ((node as INodeVM).DataContext as CheckBoxGroupBusinessClass).Items)
                            {
                                if (combobox.Tag.ToString() == (tabitem as CheckBoxItemBusinessClass).Item)
                                {
                                    (tabitem as CheckBoxItemBusinessClass).Link.Link = (sender as ComboBox).SelectedItem.ToString();
                                    (tabitem as CheckBoxItemBusinessClass).Link.InternalLink = true;
                                    (tabitem as CheckBoxItemBusinessClass).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                            }
                        }
                        else if ((node as INodeVM).Shape.Equals("radiobuttongroup"))
                        {
                            foreach (var tabitem in ((node as INodeVM).DataContext as RadioButtonGroupBusinessClass).Items)
                            {
                                if (combobox.Tag.ToString() == (tabitem as RadioButtonItemBusinessClass).Item)
                                {
                                    (tabitem as RadioButtonItemBusinessClass).Link.Link = (sender as ComboBox).SelectedItem.ToString();
                                    (tabitem as RadioButtonItemBusinessClass).Link.InternalLink = true;
                                    (tabitem as RadioButtonItemBusinessClass).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                            }
                        }
                        else if ((node as INodeVM).Shape.Equals("menubar"))
                        {
                            foreach (var tabitem in ((node as INodeVM).DataContext as MenuBarBusinessClass).Items)
                            {
                                if (combobox.Tag.ToString() == (tabitem as MenuTitleBusinessClass).Item)
                                {
                                    (tabitem as MenuTitleBusinessClass).Link.Link = (sender as ComboBox).SelectedItem.ToString();
                                    (tabitem as MenuTitleBusinessClass).Link.InternalLink = true;
                                    (tabitem as MenuTitleBusinessClass).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                            }
                        }
                        else if ((node as INodeVM).Shape.Equals("menu"))
                        {
                            foreach (var tabitem in ((node as INodeVM).DataContext as MenuBusinessClass).Items)
                            {
                                if (combobox.Tag.ToString() == (tabitem as MenuItemBusinessClass).Item)
                                {
                                    (tabitem as MenuItemBusinessClass).Link.Link = (sender as ComboBox).SelectedItem.ToString();
                                    (tabitem as MenuItemBusinessClass).Link.InternalLink = true;
                                    (tabitem as MenuItemBusinessClass).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                            }
                        }
                        else if ((node as INodeVM).Shape.Equals("list"))
                        {
                            foreach (var tabitem in ((node as INodeVM).DataContext as ListBusinessClass).Items)
                            {
                                if (combobox.Tag.ToString() == (tabitem as ListItemBusinessClass).Item)
                                {
                                    (tabitem as ListItemBusinessClass).Link.Link = (sender as ComboBox).SelectedItem.ToString();
                                    (tabitem as ListItemBusinessClass).Link.InternalLink = true;
                                    (tabitem as ListItemBusinessClass).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                            }
                        }
                        else if ((node as INodeVM).Shape.Equals("combobox"))
                        {
                            //foreach (var tabitem in ((node as INodeVM).DataContext as ComboBoxBusinessClass).)
                            {
                                //if (combobox.Tag.ToString() == ((node as INodeVM).DataContext as ComboBoxBusinessClass).Item)
                                {
                                    ((node as INodeVM).DataContext as ComboBoxBusinessClass).Link.Link = (sender as ComboBox).SelectedItem.ToString();
                                    ((node as INodeVM).DataContext as ComboBoxBusinessClass).Link.InternalLink = true;
                                    ((node as INodeVM).DataContext as ComboBoxBusinessClass).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                            }
                        }
                        else if ((node as INodeVM).Shape.Equals("breadcrumbs"))
                        {
                            foreach (var tabitem in ((node as INodeVM).DataContext as BreadcrumbsBusinessClass).Items)
                            {
                                if (combobox.Tag.ToString() == (tabitem as BreadcrumbsItemBusinessClass).Item)
                                {
                                    (tabitem as BreadcrumbsItemBusinessClass).Link.Link = (sender as ComboBox).SelectedItem.ToString();
                                    (tabitem as BreadcrumbsItemBusinessClass).Link.InternalLink = true;
                                    (tabitem as BreadcrumbsItemBusinessClass).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                            }
                        }
                        else if ((node as INodeVM).Shape.Equals("linkbar"))
                        {
                            foreach (var tabitem in ((node as INodeVM).DataContext as LinkBarBusinessClass).Items)
                            {
                                if (combobox.Tag.ToString() == (tabitem as LinkBarItemBusinessClass).Item)
                                {
                                    (tabitem as LinkBarItemBusinessClass).Link.Link = (sender as ComboBox).SelectedItem.ToString();
                                    (tabitem as LinkBarItemBusinessClass).Link.InternalLink = true;
                                    (tabitem as LinkBarItemBusinessClass).Link.SelectedLinkIndex = (sender as ComboBox).SelectedIndex;
                                }
                            }
                        }
                    }
                }
            }
        }

        static void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            weburl = (sender as TextBox).Text;
        }

        static void okButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("alertbox"))
                {
                    if (combobox.Tag.ToString() == ((node as INodeVM).DataContext as DialogBoxBusinessObject).Ok)
                    {
                        ((node as INodeVM).DataContext as DialogBoxBusinessObject).OkLink.Link = weburl;
                        ((node as INodeVM).DataContext as DialogBoxBusinessObject).OkLink.ExternalLink = true;
                    }
                    else if (combobox.Tag.ToString() == ((node as INodeVM).DataContext as DialogBoxBusinessObject).Cancel)
                    {
                        ((node as INodeVM).DataContext as DialogBoxBusinessObject).CancelLink.Link = weburl;
                        ((node as INodeVM).DataContext as DialogBoxBusinessObject).CancelLink.ExternalLink = true;
                    }
                }
                else if ((node as INodeVM).Shape.Equals("messagebox"))
                {
                    if (combobox.Tag.ToString() == ((node as INodeVM).DataContext as MessageBoxBusinessObject).Ok)
                    {
                        ((node as INodeVM).DataContext as MessageBoxBusinessObject).Link.Link = weburl;
                        ((node as INodeVM).DataContext as MessageBoxBusinessObject).Link.ExternalLink = true;
                    }
                }
                else if ((node as INodeVM).Shape.Equals("buttonbar"))
                {
                    foreach (var tabitem in ((node as INodeVM).DataContext as ButtonBarBusinessClass).Items)
                    {
                        if (combobox.Tag.ToString() == (tabitem as ButtonBarItemBusinessClass).Item)
                        {
                            (tabitem as ButtonBarItemBusinessClass).Link.Link = weburl;
                            (tabitem as ButtonBarItemBusinessClass).Link.ExternalLink = true;
                        }
                    }
                }
                else if ((node as INodeVM).Shape.Equals("tabsbar") || (node as INodeVM).Shape.Equals("verticaltab"))
                {
                    foreach (var tabitem in ((node as INodeVM).DataContext as TabBusinessClass).Items)
                    {
                        if (combobox.Tag.ToString() == (tabitem as TabItemBusinessClass).Item)
                        {
                            (tabitem as TabItemBusinessClass).Link.Link = weburl;
                            (tabitem as TabItemBusinessClass).Link.ExternalLink = true;
                        }
                    }
                }
                else if ((node as INodeVM).Shape.Equals("checkboxgroup"))
                {
                    foreach (var tabitem in ((node as INodeVM).DataContext as CheckBoxGroupBusinessClass).Items)
                    {
                        if (combobox.Tag.ToString() == (tabitem as CheckBoxItemBusinessClass).Item)
                        {
                            (tabitem as CheckBoxItemBusinessClass).Link.Link = weburl;
                            (tabitem as CheckBoxItemBusinessClass).Link.ExternalLink = true;
                        }
                    }
                }
                else if ((node as INodeVM).Shape.Equals("radiobuttongroup"))
                {
                    foreach (var tabitem in ((node as INodeVM).DataContext as RadioButtonGroupBusinessClass).Items)
                    {
                        if (combobox.Tag.ToString() == (tabitem as RadioButtonItemBusinessClass).Item)
                        {
                            (tabitem as RadioButtonItemBusinessClass).Link.Link = weburl;
                            (tabitem as RadioButtonItemBusinessClass).Link.ExternalLink = true;
                        }
                    }
                }
                else if ((node as INodeVM).Shape.Equals("menubar"))
                {
                    foreach (var tabitem in ((node as INodeVM).DataContext as MenuBarBusinessClass).Items)
                    {
                        if (combobox.Tag.ToString() == (tabitem as MenuTitleBusinessClass).Item)
                        {
                            (tabitem as MenuTitleBusinessClass).Link.Link = weburl;
                            (tabitem as MenuTitleBusinessClass).Link.ExternalLink = true;
                        }
                    }
                }
                else if ((node as INodeVM).Shape.Equals("menu"))
                {
                    foreach (var tabitem in ((node as INodeVM).DataContext as MenuBusinessClass).Items)
                    {
                        if (combobox.Tag.ToString() == (tabitem as MenuItemBusinessClass).Item)
                        {
                            (tabitem as MenuItemBusinessClass).Link.Link = weburl;
                            (tabitem as MenuItemBusinessClass).Link.ExternalLink = true;
                        }
                    }
                }
                else if ((node as INodeVM).Shape.Equals("list"))
                {
                    foreach (var tabitem in ((node as INodeVM).DataContext as ListBusinessClass).Items)
                    {
                        if (combobox.Tag.ToString() == (tabitem as ListItemBusinessClass).Item)
                        {
                            (tabitem as ListItemBusinessClass).Link.Link = weburl;
                            (tabitem as ListItemBusinessClass).Link.ExternalLink = true;
                        }
                    }
                }
                else if ((node as INodeVM).Shape.Equals("combobox"))
                {
                    ((node as INodeVM).DataContext as ComboBoxBusinessClass).Link.Link = weburl;
                    ((node as INodeVM).DataContext as ComboBoxBusinessClass).Link.ExternalLink = true;
                }
                else if ((node as INodeVM).Shape.Equals("breadcrumbs"))
                {
                    foreach (var tabitem in ((node as INodeVM).DataContext as BreadcrumbsBusinessClass).Items)
                    {
                        if (combobox.Tag.ToString() == (tabitem as BreadcrumbsItemBusinessClass).Item)
                        {
                            (tabitem as BreadcrumbsItemBusinessClass).Link.Link = weburl;
                            (tabitem as BreadcrumbsItemBusinessClass).Link.ExternalLink = true;
                        }
                    }
                }
                else if ((node as INodeVM).Shape.Equals("linkbar"))
                {
                    foreach (var tabitem in ((node as INodeVM).DataContext as LinkBarBusinessClass).Items)
                    {
                        if (combobox.Tag.ToString() == (tabitem as LinkBarItemBusinessClass).Item)
                        {
                            (tabitem as LinkBarItemBusinessClass).Link.Link = weburl;
                            (tabitem as LinkBarItemBusinessClass).Link.ExternalLink = true;
                        }
                    }
                }
                else
                {
                    (node as NodeVM).Link.Link = weburl;
                    (node as NodeVM).Link.ExternalLink = true;
                }
            }
            
            popup.IsOpen = false;
        }

        static void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }
    
    //class LinksBinding
    //{
    //    static ComboBox combobox;
    //    public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
    //            typeof(LinksBinding), new PropertyMetadata(null, PropertyChangedCallback));

    //    public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
    //    {
    //        ComboBox combo = depObj as ComboBox;
    //        combo.SelectionChanged += combobox_SelectionChanged;
    //    }
    //    static Popup popup;
    //    static object datacontext;

    //    //static void combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //    //{
    //    //    combobox = sender as ComboBox;
    //    //    datacontext = (sender as ComboBox).DataContext;

    //    //    if (e.AddedItems != null && e.AddedItems.Count > 0)
    //    //    {
    //    //        foreach (var item in (datacontext as DiagramBuilderVM).Diagrams)
    //    //        {
    //    //            if(e.AddedItems[0].Equals(item.GetFileName()))
    //    //            {
    //    //                item.IsSelected = true;
    //    //            }
    //    //        }
    //    //    }
    //    //}

    //    static void combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //    {
    //        combobox = sender as ComboBox;
    //        datacontext = (sender as ComboBox).DataContext;
    //        #region Selected Index= 0
    //        if ((sender as ComboBox).SelectedIndex == 0)
    //        {
    //            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
    //            {
    //                if ((node as INodeVM).Shape.Equals("alertbox"))
    //                {
    //                    if (combobox.Tag.ToString() == ((node as INodeVM).DataContext as DialogBoxBusinessObject).Ok)
    //                        ((node as INodeVM).DataContext as DialogBoxBusinessObject).OkHyperLink = string.Empty;
    //                    else
    //                        ((node as INodeVM).DataContext as DialogBoxBusinessObject).CancelHyperLink = string.Empty;
    //                }
    //                else if ((node as INodeVM).Shape.Equals("tabsbar") || (node as INodeVM).Shape.Equals("verticaltab"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as TabBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            tabitem.Link.Link = string.Empty;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("breadcrumbs"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as BreadcrumbsBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            tabitem.Link.Link = string.Empty;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("linkbar"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as LinkBarBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            tabitem.Link.Link = string.Empty;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("buttonbar"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as ButtonBarBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            tabitem.Link.Link = string.Empty;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("menubar"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as MenuBarBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            tabitem.Link.Link = string.Empty;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("menu"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as MenuBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            tabitem.Link.Link = string.Empty;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("list"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as ListBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            tabitem.Link.Link = string.Empty;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("checkboxgroup"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as CheckBoxGroupBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            tabitem.Link.Link = string.Empty;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("onoffswitch"))
    //                {
    //                    (node as NodeVM).Link.Link = string.Empty;
    //                    //((node as INodeVM).DataContext as OnOffSwitch).Link.Link = string.Empty;
    //                }
    //                else if ((node as INodeVM).Shape.Equals("label"))
    //                {
    //                    ((node as INodeVM).DataContext as LabelBusinessClass).Link.Link = string.Empty;
    //                }
    //                foreach (LabelVM _mAnnotation in (node as INodeVM).Annotations as List<IAnnotation>)
    //                {
    //                    _mAnnotation.HyperLink = string.Empty;
    //                }
    //            }
    //        }
    //        #endregion
    //        #region Selected Index = 1
    //        else if ((sender as ComboBox).SelectedIndex == 1)
    //        {
    //            popup = new Popup();
    //            popup.Width = 300;
    //            popup.Height = 200;
    //            popup.HorizontalOffset = 500;
    //            popup.VerticalOffset = 300;

    //            StackPanel verticalpanel = new StackPanel();
    //            verticalpanel.Background = new SolidColorBrush(Colors.Gray);
    //            TextBlock textblock = new TextBlock();
    //            textblock.Text = "Link to Web Page URL";

    //            StackPanel panel = new StackPanel();
    //            panel.Orientation = Orientation.Horizontal;
    //            TextBlock textblock1 = new TextBlock();
    //            textblock1.Text = "Enter URL";
    //            string hyperlink = string.Empty;
    //            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
    //            {
    //                if ((node as INodeVM).Shape.Equals("alertbox"))
    //                {
    //                    if (combobox.Tag.ToString() == ((node as INodeVM).DataContext as DialogBoxBusinessObject).Ok)
    //                        hyperlink = ((node as INodeVM).DataContext as DialogBoxBusinessObject).OkHyperLink;
    //                    else
    //                        hyperlink = ((node as INodeVM).DataContext as DialogBoxBusinessObject).CancelHyperLink;
    //                }
    //                else if ((node as INodeVM).Shape.Equals("tabsbar") || (node as INodeVM).Shape.Equals("verticaltab"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as TabBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            hyperlink = tabitem.Link.Link;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("breadcrumbs"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as BreadcrumbsBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            hyperlink = tabitem.Link.Link;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("linkbar"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as LinkBarBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            hyperlink = tabitem.Link.Link;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("buttonbar"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as ButtonBarBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            hyperlink = tabitem.Link.Link;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("menubar"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as MenuBarBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            hyperlink = tabitem.Link.Link;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("menu"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as MenuBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            hyperlink = tabitem.Link.Link;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("list"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as ListBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            hyperlink = tabitem.Link.Link;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("checkboxgroup"))
    //                {
    //                    foreach (var tabitem in ((node as INodeVM).DataContext as CheckBoxGroupBusinessClass).Items)
    //                    {
    //                        if (combobox.Tag.ToString() == tabitem.Item)
    //                            hyperlink = tabitem.Link.Link;
    //                    }
    //                }
    //                else if ((node as INodeVM).Shape.Equals("onoffswitch"))
    //                {
    //                    hyperlink = (node as NodeVM).Link.Link;
    //                    //hyperlink = ((node as INodeVM).DataContext as OnOffSwitch).Link.Link;
    //                }
    //                else if ((node as INodeVM).Shape.Equals("label"))
    //                {
    //                    hyperlink = ((node as INodeVM).DataContext as LabelBusinessClass).Link.Link;
    //                }
    //                else
    //                {
    //                    foreach (LabelVM _mAnnotation in (node as INodeVM).Annotations as List<IAnnotation>)
    //                    {
    //                        hyperlink = _mAnnotation.HyperLink;
    //                    }
    //                }
    //            }
    //            TextBox textbox = new TextBox();
    //            textbox.Width = 200;
    //            textbox.Height = 50;
    //            if (hyperlink != null)
    //                textbox.Text = hyperlink;
    //            textbox.TextChanged += textbox_TextChanged;
    //            panel.Children.Add(textblock1);
    //            panel.Children.Add(textbox);

    //            StackPanel buttonPanel = new StackPanel();
    //            buttonPanel.Orientation = Orientation.Horizontal;
    //            Button okButton = new Button();
    //            okButton.Width = 100;
    //            okButton.Height = 30;
    //            okButton.Content = "Ok";
    //            okButton.Click += okButton_Click;

    //            Button cancelButton = new Button();
    //            cancelButton.Width = 100;
    //            cancelButton.Height = 30;
    //            cancelButton.Content = "Cancel";
    //            cancelButton.Click += okButton_Click;
    //            buttonPanel.Children.Add(okButton);
    //            buttonPanel.Children.Add(cancelButton);

    //            verticalpanel.Children.Add(textblock);
    //            verticalpanel.Children.Add(panel);
    //            verticalpanel.Children.Add(buttonPanel);
    //            popup.Child = verticalpanel;
    //            popup.IsOpen = true;


    //        }
    //        #endregion
    //        else
    //        {
    //            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
    //            {
    //                if((node as NodeVM).Link != null)
    //                {
    //                    (node as NodeVM).Link.Link = (sender as ComboBox).SelectedItem.ToString();
    //                }
    //                //if ((node as INodeVM).Shape.Equals("onoffswitch"))
    //                //{
    //                //    ((node as INodeVM).DataContext as OnOffSwitch).Link.Link = (sender as ComboBox).SelectedItem.ToString();
    //                //}
    //            }
    //        }
    //    }

    //    static void textbox_TextChanged(object sender, TextChangedEventArgs e)
    //    {
    //        foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
    //        {
    //            if ((node as INodeVM).Shape.Equals("alertbox"))
    //            {
    //                if (combobox.Tag.ToString() == ((node as INodeVM).DataContext as DialogBoxBusinessObject).Ok)
    //                    ((node as INodeVM).DataContext as DialogBoxBusinessObject).OkHyperLink = (sender as TextBox).Text;
    //                else
    //                    ((node as INodeVM).DataContext as DialogBoxBusinessObject).CancelHyperLink = (sender as TextBox).Text;
    //            }
    //            else if ((node as INodeVM).Shape.Equals("tabsbar") || (node as INodeVM).Shape.Equals("verticaltab"))
    //            {
    //                foreach(var tabitem in ((node as INodeVM).DataContext as TabBusinessClass).Items)
    //                {
    //                    if(combobox.Tag.ToString() == tabitem.Item)
    //                        tabitem.Link.Link = (sender as TextBox).Text;
    //                }
    //            }
    //            else if ((node as INodeVM).Shape.Equals("breadcrumbs"))
    //            {
    //                foreach (var tabitem in ((node as INodeVM).DataContext as BreadcrumbsBusinessClass).Items)
    //                {
    //                    if (combobox.Tag.ToString() == tabitem.Item)
    //                        tabitem.Link.Link = (sender as TextBox).Text;
    //                }
    //            }
    //            else if ((node as INodeVM).Shape.Equals("linkbar"))
    //            {
    //                foreach (var tabitem in ((node as INodeVM).DataContext as LinkBarBusinessClass).Items)
    //                {
    //                    if (combobox.Tag.ToString() == tabitem.Item)
    //                        tabitem.Link.Link = (sender as TextBox).Text;
    //                }
    //            }
    //            else if ((node as INodeVM).Shape.Equals("buttonbar"))
    //            {
    //                foreach (var tabitem in ((node as INodeVM).DataContext as ButtonBarBusinessClass).Items)
    //                {
    //                    if (combobox.Tag.ToString() == tabitem.Item)
    //                        tabitem.Link.Link = (sender as TextBox).Text;
    //                }
    //            }
    //            else if ((node as INodeVM).Shape.Equals("menubar"))
    //            {
    //                foreach (var tabitem in ((node as INodeVM).DataContext as MenuBarBusinessClass).Items)
    //                {
    //                    if (combobox.Tag.ToString() == tabitem.Item)
    //                        tabitem.Link.Link = (sender as TextBox).Text;
    //                }
    //            }
    //            else if ((node as INodeVM).Shape.Equals("menu"))
    //            {
    //                foreach (var tabitem in ((node as INodeVM).DataContext as MenuBusinessClass).Items)
    //                {
    //                    if (combobox.Tag.ToString() == tabitem.Item)
    //                        tabitem.Link.Link = (sender as TextBox).Text;
    //                }
    //            }
    //            else if ((node as INodeVM).Shape.Equals("list"))
    //            {
    //                foreach (var tabitem in ((node as INodeVM).DataContext as ListBusinessClass).Items)
    //                {
    //                    if (combobox.Tag.ToString() == tabitem.Item)
    //                        tabitem.Link.Link = (sender as TextBox).Text;
    //                }
    //            }
    //            else if ((node as INodeVM).Shape.Equals("checkboxgroup"))
    //            {
    //                foreach (var tabitem in ((node as INodeVM).DataContext as CheckBoxGroupBusinessClass).Items)
    //                {
    //                    if (combobox.Tag.ToString() == tabitem.Item)
    //                        tabitem.Link.Link = (sender as TextBox).Text;
    //                }
    //            }
    //            else if ((node as INodeVM).Shape.Equals("onoffswitch"))
    //            {
    //                (node as NodeVM).Link.Link = (sender as TextBox).Text;
    //                //((node as INodeVM).DataContext as OnOffSwitch).Link.Link = (sender as TextBox).Text;
    //            }
    //            else if ((node as INodeVM).Shape.Equals("label"))
    //            {
    //                ((node as INodeVM).DataContext as LabelBusinessClass).Link.Link = (sender as TextBox).Text;
    //            }
    //            else
    //            {
    //                foreach (LabelVM _mAnnotation in (node as INodeVM).Annotations as List<IAnnotation>)
    //                {
    //                    _mAnnotation.HyperLink = (sender as TextBox).Text;
    //                }
    //            }

    //            if(combobox.Items.Count == 3)
    //            {
    //                //(combobox.Items[2] as ComboBoxItem).Content = (sender as TextBox).Text;
    //            }
    //            else
    //            {
    //                //ComboBoxItem item = new ComboBoxItem();
    //                //item.Content = (sender as TextBox).Text;
    //                //combobox.Items.Add(item);
    //            }
                
    //            //combobox.SelectedItem = item;
    //        }
    //    }

    //    static void okButton_Click(object sender, RoutedEventArgs e)
    //    {
    //        if(combobox.Items.Count==3)
    //            combobox.SelectedIndex = 2;
    //        popup.IsOpen = false;
    //    }

    //    public static ICommand GetCommand(UIElement element)
    //    {
    //        return (ICommand)element.GetValue(CommandProperty);
    //    }

    //    public static void SetCommand(UIElement element, ICommand command)
    //    {
    //        element.SetValue(CommandProperty, command);
    //    }
    //}

    class StrokeDashArrayBinding
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(StrokeDashArrayBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
        }

        static object datacontext =null;
        static object node = null;
        static void button_Checked(object sender, RoutedEventArgs e)
        {
            if(datacontext == null)
                datacontext = (sender as ToggleButton).DataContext;
            if(node == null)
                node = (datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM;


            DoubleCollection _mStrokeDashArray = new DoubleCollection();
            if ((sender as ToggleButton).Name.ToString().Equals("first"))
            {
            }
            else if ((sender as ToggleButton).Name.ToString().Equals("second"))
            {
                _mStrokeDashArray.Add(2);
                _mStrokeDashArray.Add(1);
            }
            else if ((sender as ToggleButton).Name.ToString().Equals("third"))
            {
                _mStrokeDashArray.Add(2);
                _mStrokeDashArray.Add(0.5);
                _mStrokeDashArray.Add(0.5);
                _mStrokeDashArray.Add(2);
            }
            else if ((sender as ToggleButton).Name.ToString().Equals("fourth"))
            {
                _mStrokeDashArray.Add(1);
            }
            ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).StrokeDashArray = _mStrokeDashArray;

            //foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            //{
            //    (node as GroupableVM).StrokeDashArray.Clear();
            //    if ((sender as ToggleButton).Name.ToString().Equals("first"))
            //    {
            //    }
            //    else if ((sender as ToggleButton).Name.ToString().Equals("second"))
            //    {
            //        (node as GroupableVM).StrokeDashArray.Add(2);
            //        (node as GroupableVM).StrokeDashArray.Add(1);
            //    }
            //    else if ((sender as ToggleButton).Name.ToString().Equals("third"))
            //    {
            //        (node as GroupableVM).StrokeDashArray.Add(2);
            //        (node as GroupableVM).StrokeDashArray.Add(0.5);
            //        (node as GroupableVM).StrokeDashArray.Add(0.5);
            //        (node as GroupableVM).StrokeDashArray.Add(2);
            //    }
            //    else if ((sender as ToggleButton).Name.ToString().Equals("fourth"))
            //    {
            //        (node as GroupableVM).StrokeDashArray.Add(1);
            //    }
            //}
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class ScrollBarVisibilityBinding
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(ScrollBarVisibilityBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
            button.Unchecked += button_Unchecked;
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {
            var datacontext = (sender as ToggleButton).DataContext;
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
            }
        }

        static void button_Unchecked(object sender, RoutedEventArgs e)
        {
            var datacontext = (sender as ToggleButton).DataContext;
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class OnOffSwitchBinding
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(OnOffSwitchBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            if (depObj is ToggleButton)
            {
                ToggleButton button = depObj as ToggleButton;
                button.Checked += button_Checked;
            }
            else
            {
                Button button = depObj as Button;
                button.Click += button_Click;
            }
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {
            var datacontext = (sender as ToggleButton).DataContext;
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("onoffswitch"))
                {
                    foreach (LabelVM _mAnnotation in (node as INodeVM).Annotations as List<IAnnotation>)
                    {
                        if ((sender as ToggleButton).Name.ToString().Equals("on"))
                        {
                            //((node as INodeVM).DataContext as OnOffSwitch).On = true;
                            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                            _mAnnotation.LabelMargin = new Thickness(10, 0, 0, 0);
                            _mAnnotation.Content = "On";
                        }
                        else if ((sender as ToggleButton).Name.ToString().Equals("off"))
                        {
                            //((node as INodeVM).DataContext as OnOffSwitch).Off = false;
                            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Right;
                            _mAnnotation.LabelMargin = new Thickness(0, 0, 10, 0);
                            _mAnnotation.Content = "Off";
                        }
                    }
                }                
                else if ((node as INodeVM).Shape.Equals("label"))
                {
                    if ((sender as ToggleButton).Name.ToString().Equals("left"))
                        (node as IGroupableVM).LabelHAlign = HorizontalAlignment.Left;
                    else if ((sender as ToggleButton).Name.ToString().Equals("right"))
                        (node as IGroupableVM).LabelHAlign = HorizontalAlignment.Right;
                    else if ((sender as ToggleButton).Name.ToString().Equals("horizontolcenter"))
                        (node as IGroupableVM).LabelHAlign = HorizontalAlignment.Center;


                    foreach (LabelVM _mAnnotation in (node as INodeVM).Annotations as List<IAnnotation>)
                    {
                        if ((sender as ToggleButton).Name.ToString().Equals("o90"))
                            (_mAnnotation.DataContext as LabelBusinessClass).Orientation = 90;
                        else if ((sender as ToggleButton).Name.ToString().Equals("o0"))
                            (_mAnnotation.DataContext as LabelBusinessClass).Orientation = 0;
                        if ((sender as ToggleButton).Name.ToString().Equals("o45"))
                            (_mAnnotation.DataContext as LabelBusinessClass).Orientation = 45;
                        else if ((sender as ToggleButton).Name.ToString().Equals("o360"))
                            (_mAnnotation.DataContext as LabelBusinessClass).Orientation = 270;

                    }
                }
            }
        }

        static void button_Click(object sender, RoutedEventArgs e)
        {
            var datacontext = (sender as Button).DataContext;
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).DataContext is OnOffSwitch)
                {
                    foreach(LabelVM _mAnnotation in (node as INodeVM).Annotations as List<IAnnotation>)
                    {
                        if ((sender as Button).Name.ToString().Equals("on"))
                        {
                            //((node as INodeVM).DataContext as OnOffSwitch).OnOff = true;
                            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Left;
                            _mAnnotation.LabelMargin = new Thickness(10, 0, 0, 0);
                            _mAnnotation.Content = "On";
                        }
                        else if ((sender as Button).Name.ToString().Equals("off"))
                        {
                            //((node as INodeVM).DataContext as OnOffSwitch).OnOff = false;
                            _mAnnotation.HorizontalAlignment = HorizontalAlignment.Right;
                            _mAnnotation.LabelMargin = new Thickness(0, 0, 10, 0);
                            _mAnnotation.Content = "Off";
                        }
                    }                    
                }
                else if ((node as INodeVM).Shape.Equals("label"))
                {
                    if ((sender as Button).Name.ToString().Equals("left"))
                        (node as IGroupableVM).LabelHAlign = HorizontalAlignment.Left;
                    else if ((sender as Button).Name.ToString().Equals("right"))
                        (node as IGroupableVM).LabelHAlign = HorizontalAlignment.Right;
                    else if ((sender as Button).Name.ToString().Equals("horizontolcenter"))
                        (node as IGroupableVM).LabelHAlign = HorizontalAlignment.Center;
                    

                    foreach (LabelVM _mAnnotation in (node as INodeVM).Annotations as List<IAnnotation>)
                    {
                        if ((sender as Button).Name.ToString().Equals("o90"))
                            (_mAnnotation.DataContext as LabelBusinessClass).Orientation = 90;
                        else if ((sender as Button).Name.ToString().Equals("o0"))
                            (_mAnnotation.DataContext as LabelBusinessClass).Orientation = 0;
                        if ((sender as Button).Name.ToString().Equals("o45"))
                            (_mAnnotation.DataContext as LabelBusinessClass).Orientation = 45;
                        else if ((sender as Button).Name.ToString().Equals("o360"))
                            (_mAnnotation.DataContext as LabelBusinessClass).Orientation = 270;

                    }
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class AlignmentBinding
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(AlignmentBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {
            var datacontext = (sender as ToggleButton).DataContext;
            

            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape != null && ((node as INodeVM).Shape.Equals("stickynote") || (node as INodeVM).Shape.Equals("verticalcurlybraces")
                    || (node as INodeVM).Shape.Equals("horizontalcurlybraces"))
                    )
                {
                    foreach (LabelVM _mAnnotation in (node as INodeVM).Annotations as List<IAnnotation>)
                    {
                        if ((sender as ToggleButton).Name.ToString().Equals("left"))
                            _mAnnotation.TextAlignment = TextAlignment.Left;
                        else if ((sender as ToggleButton).Name.ToString().Equals("right"))
                            _mAnnotation.TextAlignment = TextAlignment.Right;
                        else if ((sender as ToggleButton).Name.ToString().Equals("horizontolcenter"))
                            _mAnnotation.TextAlignment = TextAlignment.Center;
                        else if ((sender as ToggleButton).Name.ToString().Equals("justify"))
                            _mAnnotation.TextAlignment = TextAlignment.Justify;
                    }
                }
                //else
                //{
                //    if ((sender as ToggleButton).Name.ToString().Equals("left"))
                //        (node as IGroupableVM).LabelHAlign = HorizontalAlignment.Left;
                //    else if ((sender as ToggleButton).Name.ToString().Equals("right"))
                //        (node as IGroupableVM).LabelHAlign = HorizontalAlignment.Right;
                //    else if ((sender as ToggleButton).Name.ToString().Equals("horizontolcenter"))
                //        (node as IGroupableVM).LabelHAlign = HorizontalAlignment.Center;
                //    else if ((sender as ToggleButton).Name.ToString().Equals("justify"))
                //        (node as IGroupableVM).LabelHAlign = HorizontalAlignment.Stretch;
                //}
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class PointyButtonBinding
    {
        static object datacontext;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(PointyButtonBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
            button.Unchecked += button_Unchecked;
            datacontext = button.DataContext;
        }

        static void button_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("pointybutton"))
                {
                    if ((sender as ToggleButton).Name == "menuicon")
                    {
                        ((node as INodeVM).DataContext as PointyButtonBusinessClass).MenuIconEnabled = false;
                    }
                }
            }
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("pointybutton"))
                { 
                    (node as INodeVM).ContentTemplate = ((node as INodeVM).DataContext as PointyButtonBusinessClass).Template;
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class ToolTipBinding
    {
        static object datacontext;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(ToolTipBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
            datacontext = button.DataContext;
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("tooltip"))
                {
                    (node as INodeVM).ContentTemplate = ((node as INodeVM).DataContext as ToolTipBusinessClass).Template;
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class VerticalTabBinding
    {
       static ResourceDictionary resourceDictionary = null;
        static object datacontext;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(VerticalTabBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Showcase/Mockup/Theme/MockupUI.xaml", UriKind.Absolute)
            };
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
            datacontext = button.DataContext;
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {
         
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("verticaltab"))
                {
                    foreach (LabelVM _mAnnotation in (node as INodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                    {
                        if ((sender as ToggleButton).Name == "leftVerticalTab")
                        {
                            _mAnnotation.ViewTemplate = resourceDictionary["verticaltabLeftviewtemplate"] as DataTemplate;
                            (_mAnnotation.DataContext as TabBusinessClass).VerticalTabAlignment = HorizontalAlignment.Left;
                        }
                        else if ((sender as ToggleButton).Name == "rightVerticalTab")
                        {
                            _mAnnotation.ViewTemplate = resourceDictionary["verticaltabRightviewtemplate"] as DataTemplate;
                            (_mAnnotation.DataContext as TabBusinessClass).VerticalTabAlignment = HorizontalAlignment.Right;
                        }
                    }
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class HorizontalTabBinding
    {
        static object datacontext;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(HorizontalTabBinding), new PropertyMetadata(null, PropertyChangedCallback));
        static ResourceDictionary resourceDictionary = null;
      
        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Showcase/Mockup/Theme/MockupUI.xaml", UriKind.Absolute)
            };
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
            datacontext = button.DataContext;
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("tabsbar"))
                {
                    foreach (LabelVM _mAnnotation in (node as INodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                    {
                        if ((sender as ToggleButton).Name == "topHorizontalTab")
                        {
                            _mAnnotation.ViewTemplate = resourceDictionary["tabbarTopviewtemplate"] as DataTemplate;
                            (_mAnnotation.DataContext as TabBusinessClass).HorizontalTabAlignment = VerticalAlignment.Top;
                        }
                        else if ((sender as ToggleButton).Name == "bottomHorizontalTab")
                        {
                            _mAnnotation.ViewTemplate = resourceDictionary["tabbarBottomviewtemplate"] as DataTemplate;
                            (_mAnnotation.DataContext as TabBusinessClass).HorizontalTabAlignment = VerticalAlignment.Bottom;
                        }
                    }
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class GeometricShapesBinding
    {
        static object datacontext;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(GeometricShapesBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
            datacontext = button.DataContext;
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("geometricshapes"))
                {
                    if ((sender as ToggleButton).Name == "ellipse")
                    {
                        ((node as INodeVM).DataContext as GeometricShapesBusinessClass).Ellipse = true;  
                    }
                    else if ((sender as ToggleButton).Name == "rectangle")
                    {
                        ((node as INodeVM).DataContext as GeometricShapesBusinessClass).Rectangle = true;  
                    }
                    if ((sender as ToggleButton).Name == "roundedcorner")
                    {
                        ((node as INodeVM).DataContext as GeometricShapesBusinessClass).RoundedRectangle = true;  
                    }
                    else if ((sender as ToggleButton).Name == "pentagon")
                    {
                        ((node as INodeVM).DataContext as GeometricShapesBusinessClass).Pentagon = true;  
                    }
                    else if ((sender as ToggleButton).Name == "diamond")
                    {
                        ((node as INodeVM).DataContext as GeometricShapesBusinessClass).Diamond = true;  
                    }
                    else if ((sender as ToggleButton).Name == "star")
                    {
                        ((node as INodeVM).DataContext as GeometricShapesBusinessClass).Star = true;  
                    }
                    if ((sender as ToggleButton).Name == "parallelogram")
                    {
                        ((node as INodeVM).DataContext as GeometricShapesBusinessClass).Parallelogram = true;  
                    }
                    else if ((sender as ToggleButton).Name == "triangle")
                    {
                        ((node as INodeVM).DataContext as GeometricShapesBusinessClass).Triangle = true;  
                    }

                    (node as INodeVM).ContentTemplate = ((node as INodeVM).DataContext as GeometricShapesBusinessClass).Template;
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class PopoverBinding
    {
        static object datacontext;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(PopoverBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
            datacontext = button.DataContext;
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("popover"))
                {
                    if ((sender as ToggleButton).Name == "leftpopover")
                    {
                        ((node as INodeVM).DataContext as PopoverBusinessClass).Left = true;                        
                    }
                    else if ((sender as ToggleButton).Name == "rightpopover")
                    {
                        ((node as INodeVM).DataContext as PopoverBusinessClass).Right = true;   
                    }
                    else if ((sender as ToggleButton).Name == "toppopover")
                    {
                        ((node as INodeVM).DataContext as PopoverBusinessClass).Top = true;   
                    }
                    else if ((sender as ToggleButton).Name == "bottompopover")
                    {
                        ((node as INodeVM).DataContext as PopoverBusinessClass).Bottom = true;   
                    }
                    (node as INodeVM).ContentTemplate = ((node as INodeVM).DataContext as PopoverBusinessClass).Template;
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }    

    class LockButtonBinding
    {
        static object datacontext;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(LockButtonBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
            button.Unchecked += button_Unchecked;
            datacontext = button.DataContext;
        }

        static void button_Unchecked(object sender, RoutedEventArgs e)
        {
            //((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).IsNodeLocked = false;
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                (node as INodeVM).Constraints = NodeConstraints.Default;
            }
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Groups as IEnumerable)
            {
                (node as INodeVM).Constraints = NodeConstraints.Default;
            }
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {
            //((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).IsNodeLocked = true;
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                (node as INodeVM).Constraints = NodeConstraints.Selectable;
            }
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Groups as IEnumerable)
            {
                (node as INodeVM).Constraints = NodeConstraints.Selectable;
            }
        }

        

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class AspectRatioBinding
    {
        static object datacontext;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(AspectRatioBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            CheckBox checkbox = depObj as CheckBox;
            checkbox.Checked += checkbox_Checked;
            checkbox.Unchecked += checkbox_Unchecked;
            datacontext = checkbox.DataContext;
        }

        static void checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                    (node as INodeVM).Constraints = (node as INodeVM).Constraints &~ NodeConstraints.AspectRatio;
            }
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Groups as IEnumerable)
            {
                (node as INodeVM).Constraints = (node as INodeVM).Constraints & ~NodeConstraints.AspectRatio;
            }
        }

        static void checkbox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if (!(node as INodeVM).Constraints.Contains(NodeConstraints.AspectRatio))
                    (node as INodeVM).Constraints = (node as INodeVM).Constraints | NodeConstraints.AspectRatio;
            }

            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Groups as IEnumerable)
            {
                if (!(node as INodeVM).Constraints.Contains(NodeConstraints.AspectRatio))
                    (node as INodeVM).Constraints = (node as INodeVM).Constraints | NodeConstraints.AspectRatio;
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class ResetButtonBinding
    {
        static object datacontext;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(ResetButtonBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            Button button = depObj as Button;
            button.Click += button_Click;
            datacontext = button.DataContext;
        }

        static void button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("button") || (node as INodeVM).Shape.Equals("subtitle")
                    || (node as INodeVM).Shape.Equals("bigtitle")
                    )
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                        Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                        (node as INodeVM).UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                        (node as INodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom; 
                    }
                }
                else if ((node as INodeVM).Shape.Equals("menubar"))
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                        double textwidth = new double();
                        foreach (var item in (_mAnnotation.DataContext as MenuBarBusinessClass).Items)
                        {
                            Size textsize = FindDummyTextSize(_mAnnotation, (item as MenuTitleBusinessClass).Item, TextWrapping.NoWrap);
                            textwidth = textwidth + textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                        }
                        (node as INodeVM).UnitWidth = textwidth;
                    }
                }
                else if ((node as INodeVM).Shape.Equals("buttonbar"))
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                        Size buttonbarsize = new Size();
                        foreach (var item in (_mAnnotation.DataContext as ButtonBarBusinessClass).Items)
                        {
                            (item as ButtonBarItemBusinessClass).Width = FindDummyTextSize(_mAnnotation, (item as ButtonBarItemBusinessClass).Item, TextWrapping.NoWrap).Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                            buttonbarsize.Width = buttonbarsize.Width + (item as ButtonBarItemBusinessClass).Width;
                        }
                        buttonbarsize.Height = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap).Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        (node as INodeVM).UnitWidth = buttonbarsize.Width;
                        (node as INodeVM).UnitHeight = buttonbarsize.Height;
                    }
                }
                else if ((node as INodeVM).Shape.Equals("searchbutton"))
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                        Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                        if (textsize.Width > (node as INodeVM).DefaultSize.Width)
                            (node as INodeVM).UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + (2 * _mAnnotation.LabelMargin.Right);
                        if (textsize.Height > (node as INodeVM).DefaultSize.Height)
                            (node as INodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                    }
                }
                else if ((node as INodeVM).Shape.Equals("checkbox") || (node as INodeVM).Shape.Equals("radiobutton"))
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                        Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                        (node as INodeVM).UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                        (node as INodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                    }
                }
                else if ((node as INodeVM).Shape.Equals("datechooser"))
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                        Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                        textsize.Width = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;
                        if (textsize.Width > (node as INodeVM).DefaultSize.Width)
                            (node as INodeVM).UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarWidth + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Left + (_mAnnotation.DataContext as DateChoosetBusinessClass).CalendarMargin.Right;
                        else
                            if ((node as INodeVM).UnitWidth > (node as INodeVM).DefaultSize.Width)
                                (node as INodeVM).UnitWidth = (node as INodeVM).DefaultSize.Width;
                    }
                }
                else if ((node as INodeVM).Shape.Equals("numericstepper"))
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                        Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                        (node as INodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        (node as INodeVM).UnitWidth = (node as INodeVM).UnitHeight + textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                    }
                }
                else if ((node as INodeVM).Shape.Equals("combobox"))
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                        Size textsize = new Size();
                        textsize = FindDummyTextSize(_mAnnotation, (_mAnnotation.DataContext as ComboBoxBusinessClass).Title, TextWrapping.NoWrap);
                        (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        textsize.Width += (_mAnnotation.DataContext as ComboBoxBusinessClass).ItemHeight + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                        textsize.Height += _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        foreach (var item in (_mAnnotation.DataContext as ComboBoxBusinessClass).Items)
                        {
                            Size localsize = FindDummyTextSize(_mAnnotation, item.Item, TextWrapping.NoWrap);
                            textsize.Height += localsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        }
                        (node as INodeVM).UnitWidth = textsize.Width;
                        (node as INodeVM).UnitHeight = textsize.Height;
                    }
                }
                else if ((node as INodeVM).Shape.Equals("checkboxgroup"))
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                        Size textsize = new Size();
                        foreach (var item in (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).Items)
                        {
                            Size localsize = FindDummyTextSize(_mAnnotation, (item as CheckBoxItemBusinessClass).Item, TextWrapping.NoWrap);
                            if (localsize.Width > textsize.Width)
                                textsize.Width = localsize.Width;
                        }
                        (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        (node as NodeVM).UnitHeight = (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).ItemHeight * (_mAnnotation.DataContext as CheckBoxGroupBusinessClass).Items.Count();
                        // Added FontSize also becuase checkbox is in front 
                        (node as NodeVM).UnitWidth = textsize.Width + _mAnnotation.FontSize;
                    }
                }
                else if ((node as INodeVM).Shape.Equals("radiobuttongroup"))
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                        Size textsize = new Size();
                        foreach (var item in (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).Items)
                        {
                            Size localsize = FindDummyTextSize(_mAnnotation, (item as RadioButtonItemBusinessClass).Item, TextWrapping.NoWrap);
                            if (localsize.Width > textsize.Width)
                                textsize.Width = localsize.Width;
                        }
                        (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).ItemHeight = _mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                        (node as NodeVM).UnitHeight = (_mAnnotation.FontSize + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom) * (_mAnnotation.DataContext as RadioButtonGroupBusinessClass).Items.Count();
                        // Added FontSize also becuase checkbox is in front 
                        (node as NodeVM).UnitWidth = textsize.Width + _mAnnotation.FontSize;
                    }
                }
                else if ((node as INodeVM).Shape.Equals("pointybutton"))
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                         Size textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                        (node as INodeVM).UnitWidth = textsize.Width + _mAnnotation.FontSize + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Left + (_mAnnotation.DataContext as PointyButtonBusinessClass).MenuIconMargin.Right;
                        (node as INodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                    }
                }
                else if ((node as INodeVM).Shape.Equals("textinput"))
                {
                    foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                    {
                        Size textsize = new Size();
                        textsize = FindDummyTextSize(_mAnnotation, _mAnnotation.Content.ToString(), TextWrapping.NoWrap);
                        (node as INodeVM).UnitWidth = textsize.Width + _mAnnotation.LabelMargin.Left + _mAnnotation.LabelMargin.Right;
                        (node as INodeVM).UnitHeight = textsize.Height + _mAnnotation.LabelMargin.Top + _mAnnotation.LabelMargin.Bottom;
                    }
                }
                else
                {
                    (node as INodeVM).UnitWidth = (node as INodeVM).DefaultSize.Width;
                    (node as INodeVM).UnitHeight = (node as INodeVM).DefaultSize.Height;
                }
            }
        }

        private static Size FindDummyTextSize(LabelVM _mAnnotation, string text, TextWrapping wrap = TextWrapping.NoWrap)
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

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class IOSKeyboardBinding
    {
        static object datacontext;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(IOSKeyboardBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
            datacontext = button.DataContext;
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("ioskeyboard"))
                {
                    if ((sender as ToggleButton).Name == "iphone")
                    {
                        ((node as INodeVM).DataContext as IOSKeyboardBusinessClass).IPhone = true;
                    }
                    else if ((sender as ToggleButton).Name == "ipad")
                    {
                        ((node as INodeVM).DataContext as IOSKeyboardBusinessClass).IPad = true;
                    }
                    else if ((sender as ToggleButton).Name == "wide")
                    {
                        ((node as INodeVM).DataContext as IOSKeyboardBusinessClass).Wide = true;
                    }
                    else if ((sender as ToggleButton).Name == "standard")
                    {
                        ((node as INodeVM).DataContext as IOSKeyboardBusinessClass).Standard = true;
                    }
                    (node as INodeVM).ContentTemplate = ((node as INodeVM).DataContext as IOSKeyboardBusinessClass).Template;
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class IPADBinding
    {
        static object datacontext;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(IPADBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
            button.Unchecked += button_Unchecked;
            datacontext = button.DataContext;
        }

        static void button_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("ipad"))
                {
                    if ((sender as ToggleButton).Name == "ipadtransparent")
                    {
                        ((node as INodeVM).DataContext as IPADBusinessClass).TransparentBakground = false;
                    }
                    else if ((sender as ToggleButton).Name == "ipadtopbar")
                    {
                        ((node as INodeVM).DataContext as IPADBusinessClass).TopBar = false;
                    }
                    (node as INodeVM).ContentTemplate = ((node as INodeVM).DataContext as IPADBusinessClass).Template;
                }
            }
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("ipad"))
                {
                    if ((sender as ToggleButton).Name == "verticalipad")
                    {
                        ((node as INodeVM).DataContext as IPADBusinessClass).VerticalOrientation = true;
                    }
                    else if ((sender as ToggleButton).Name == "horizontalipad")
                    {
                        ((node as INodeVM).DataContext as IPADBusinessClass).HorizontalOrientation = true;
                    }
                    else if ((sender as ToggleButton).Name == "ipadtransparent")
                    {
                        ((node as INodeVM).DataContext as IPADBusinessClass).TransparentBakground = true;
                    }
                    else if ((sender as ToggleButton).Name == "ipadtopbar")
                    {
                        ((node as INodeVM).DataContext as IPADBusinessClass).TopBar = true;
                    }
                    (node as INodeVM).ContentTemplate = ((node as INodeVM).DataContext as IPADBusinessClass).Template;
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }            

    class RadioButtonGroupItemBinding
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(RadioButtonGroupItemBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            Border border = depObj as Border;
            border.PointerEntered += border_PointerEntered;
            border.PointerExited += border_PointerExited;
            border.PointerReleased += border_PointerReleased;
            border.PointerPressed += border_PointerPressed;
        }

        static void border_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (((sender as Border).DataContext is LabelVM))
            {
                LabelVM annotation = (sender as Border).DataContext as LabelVM;
                if (annotation.DataContext is DialogBoxBusinessObject)
                {
                    if (!(annotation.DataContext as DialogBoxBusinessObject).EditMode)
                    {
                        if ((sender as Border).Name.Equals("cancelborder"))
                        {
                            if ((annotation.DataContext as DialogBoxBusinessObject).CancelLink != null
                                && !string.IsNullOrEmpty((annotation.DataContext as DialogBoxBusinessObject).CancelLink.Link))
                            {
                                (annotation.DataContext as DialogBoxBusinessObject).ExecutedPath = (annotation.DataContext as DialogBoxBusinessObject).CancelLink.Link;
                            }
                        }
                        else if ((sender as Border).Name.Equals("okborder"))
                        {
                            if ((annotation.DataContext as DialogBoxBusinessObject).OkLink != null
                                && !string.IsNullOrEmpty((annotation.DataContext as DialogBoxBusinessObject).OkLink.Link))
                            {
                                (annotation.DataContext as DialogBoxBusinessObject).ExecutedPath = (annotation.DataContext as DialogBoxBusinessObject).OkLink.Link;
                            }
                        }
                    }
                }
                else if (annotation.DataContext is MessageBoxBusinessObject)
                {
                    if (!(annotation.DataContext as MessageBoxBusinessObject).EditMode)
                    {
                        if ((annotation.DataContext as MessageBoxBusinessObject).Link != null
                                && !string.IsNullOrEmpty((annotation.DataContext as MessageBoxBusinessObject).Link.Link))
                        {
                            (annotation.DataContext as MessageBoxBusinessObject).ExecutedPath = (annotation.DataContext as MessageBoxBusinessObject).Link.Link;
                        }
                    }
                }
            }
            else if (((sender as Border).DataContext is CheckBoxItemBusinessClass))
            {
                if (((sender as Border).DataContext as CheckBoxItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as CheckBoxItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent((VisualTreeHelper.GetParent(sender as Border) as ContentPresenter)) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as CheckBoxGroupBusinessClass).EditMode)
                        (annotation.DataContext as CheckBoxGroupBusinessClass).ExecutedPath = ((sender as Border).DataContext as CheckBoxItemBusinessClass).Link.Link;
                }
            }
            else if (((sender as Border).DataContext is ButtonBarItemBusinessClass))
            {
                if (((sender as Border).DataContext as ButtonBarItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as ButtonBarItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent((VisualTreeHelper.GetParent((VisualTreeHelper.GetParent(sender as Border) as StackPanel)) as ContentPresenter)) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as ButtonBarBusinessClass).EditMode)
                        (annotation.DataContext as ButtonBarBusinessClass).ExecutedPath = ((sender as Border).DataContext as ButtonBarItemBusinessClass).Link.Link;
                }
            }
            else if (((sender as Border).DataContext is TabItemBusinessClass))
            {
                if (((sender as Border).DataContext as TabItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as TabItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent(VisualTreeHelper.GetParent((sender as Border)) as ContentPresenter) as StackPanel).DataContext as LabelVM;
                    (annotation.DataContext as TabBusinessClass).ExecutedPath = ((sender as Border).DataContext as TabItemBusinessClass).Link.Link;
                }
            }
            else if (((sender as Border).DataContext is RadioButtonItemBusinessClass))
            {
                if (((sender as Border).DataContext as RadioButtonItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as RadioButtonItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent((VisualTreeHelper.GetParent(sender as Border) as ContentPresenter)) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as RadioButtonGroupBusinessClass).EditMode)
                        (annotation.DataContext as RadioButtonGroupBusinessClass).ExecutedPath = ((sender as Border).DataContext as RadioButtonItemBusinessClass).Link.Link;
                }
            }
            else if (((sender as Border).DataContext is MenuTitleBusinessClass))
            {
                if (((sender as Border).DataContext as MenuTitleBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as MenuTitleBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent((VisualTreeHelper.GetParent(sender as Border) as ContentPresenter)) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as MenuBarBusinessClass).EditMode)
                        (annotation.DataContext as MenuBarBusinessClass).ExecutedPath = ((sender as Border).DataContext as MenuTitleBusinessClass).Link.Link;
                }
            }
            else if (((sender as Border).DataContext is MenuItemBusinessClass))
            {
                if (((sender as Border).DataContext as MenuItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as MenuItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent((VisualTreeHelper.GetParent(sender as Border) as ContentPresenter)) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as MenuBusinessClass).EditMode)
                        (annotation.DataContext as MenuBusinessClass).ExecutedPath = ((sender as Border).DataContext as MenuItemBusinessClass).Link.Link;
                }
            }
            else if (((sender as Border).DataContext is ListItemBusinessClass))
            {
                if (((sender as Border).DataContext as ListItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as ListItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent((VisualTreeHelper.GetParent(sender as Border) as ContentPresenter)) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as ListBusinessClass).EditMode)
                        (annotation.DataContext as ListBusinessClass).ExecutedPath = ((sender as Border).DataContext as ListItemBusinessClass).Link.Link;
                }
            }
            else if (((sender as Border).DataContext is BreadcrumbsItemBusinessClass))
            {
                if (((sender as Border).DataContext as BreadcrumbsItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as BreadcrumbsItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(sender as Border) as StackPanel) as ContentPresenter) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as BreadcrumbsBusinessClass).EditMode)
                        (annotation.DataContext as BreadcrumbsBusinessClass).ExecutedPath = ((sender as Border).DataContext as BreadcrumbsItemBusinessClass).Link.Link;
                }
            }
            else if (((sender as Border).DataContext is LinkBarItemBusinessClass))
            {
                if (((sender as Border).DataContext as LinkBarItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as LinkBarItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(sender as Border) as Grid) as ContentPresenter) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as LinkBarBusinessClass).EditMode)
                        (annotation.DataContext as LinkBarBusinessClass).ExecutedPath = ((sender as Border).DataContext as LinkBarItemBusinessClass).Link.Link;
                }
            }
        }

        static void border_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
        }

        static void border_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
        }

        static void border_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (((sender as Border).DataContext is LabelVM))
            {
                LabelVM annotation = (sender as Border).DataContext as LabelVM;
                if (annotation.DataContext is DialogBoxBusinessObject)
                {
                    if (!(annotation.DataContext as DialogBoxBusinessObject).EditMode)
                    {
                        if ((sender as Border).Name.Equals("cancelborder"))
                        {
                            if ((annotation.DataContext as DialogBoxBusinessObject).CancelLink != null
                                && !string.IsNullOrEmpty((annotation.DataContext as DialogBoxBusinessObject).CancelLink.Link))
                            {
                                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
                            }
                        }
                        else if ((sender as Border).Name.Equals("okborder"))
                        {
                            if ((annotation.DataContext as DialogBoxBusinessObject).OkLink != null
                                && !string.IsNullOrEmpty((annotation.DataContext as DialogBoxBusinessObject).OkLink.Link))
                            {
                                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
                            }
                        }
                    }
                }
                else if (annotation.DataContext is MessageBoxBusinessObject)
                {
                    if (!(annotation.DataContext as MessageBoxBusinessObject).EditMode)
                    {
                        if ((annotation.DataContext as MessageBoxBusinessObject).Link != null
                                && !string.IsNullOrEmpty((annotation.DataContext as MessageBoxBusinessObject).Link.Link))
                        {
                            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
                        }
                    }
                }
            }
            else if (((sender as Border).DataContext is CheckBoxItemBusinessClass))
            {
                if (((sender as Border).DataContext as CheckBoxItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as CheckBoxItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent((VisualTreeHelper.GetParent(sender as Border) as ContentPresenter)) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as CheckBoxGroupBusinessClass).EditMode)
                        Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
                }
            }
            else if (((sender as Border).DataContext is ButtonBarItemBusinessClass))
            {
                if (((sender as Border).DataContext as ButtonBarItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as ButtonBarItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent((VisualTreeHelper.GetParent((VisualTreeHelper.GetParent(sender as Border) as StackPanel)) as ContentPresenter)) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as ButtonBarBusinessClass).EditMode)
                        Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
                }
            }
            else if (((sender as Border).DataContext is TabItemBusinessClass))
            {
                if (((sender as Border).DataContext as TabItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as TabItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent(VisualTreeHelper.GetParent((sender as Border)) as ContentPresenter) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as TabBusinessClass).EditMode)
                        Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
                }
            }
            else if (((sender as Border).DataContext is RadioButtonItemBusinessClass))
            {
                if (((sender as Border).DataContext as RadioButtonItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as RadioButtonItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent((VisualTreeHelper.GetParent(sender as Border) as ContentPresenter)) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as RadioButtonGroupBusinessClass).EditMode)
                        Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
                }
            }
            else if (((sender as Border).DataContext is MenuTitleBusinessClass))
            {
                if (((sender as Border).DataContext as MenuTitleBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as MenuTitleBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent((VisualTreeHelper.GetParent(sender as Border) as ContentPresenter)) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as MenuBarBusinessClass).EditMode)
                        Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
                }
            }
            else if (((sender as Border).DataContext is MenuItemBusinessClass))
            {
                if (((sender as Border).DataContext as MenuItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as MenuItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent((VisualTreeHelper.GetParent(sender as Border) as ContentPresenter)) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as MenuBusinessClass).EditMode)
                        Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
                }
            }
            else if (((sender as Border).DataContext is ListItemBusinessClass))
            {
                if (((sender as Border).DataContext as ListItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as ListItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent((VisualTreeHelper.GetParent(sender as Border) as ContentPresenter)) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as ListBusinessClass).EditMode)
                        Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
                }
            }
            else if (((sender as Border).DataContext is BreadcrumbsItemBusinessClass))
            {
                if (((sender as Border).DataContext as BreadcrumbsItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as BreadcrumbsItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(sender as Border) as StackPanel) as ContentPresenter) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as BreadcrumbsBusinessClass).EditMode)
                        Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
                }                
            }
            else if (((sender as Border).DataContext is LinkBarItemBusinessClass))
            {
                if (((sender as Border).DataContext as LinkBarItemBusinessClass).Link != null
                    && !string.IsNullOrEmpty(((sender as Border).DataContext as LinkBarItemBusinessClass).Link.Link)
                    )
                {
                    LabelVM annotation = (VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(sender as Border) as Grid) as ContentPresenter) as StackPanel).DataContext as LabelVM;
                    if (!(annotation.DataContext as LinkBarBusinessClass).EditMode)
                        Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
                }    
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class OnOffBinding
    {
        static object datacontext;
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(OnOffBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ToggleButton button = depObj as ToggleButton;
            button.Checked += button_Checked;
            datacontext = button.DataContext;
        }

        static void button_Checked(object sender, RoutedEventArgs e)
        {            
            foreach (var node in ((datacontext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).Nodes as IEnumerable)
            {
                if ((node as INodeVM).Shape.Equals("onoffswitch"))
                {
                    foreach (LabelVM _mAnnotation in (node as INodeVM).Annotations as List<IAnnotation>)
                    {
                        if ((sender as ToggleButton).Name.ToString().Equals("on"))
                        {
                            ((node as INodeVM).DataContext as OnOffSwitch).On = true;
                        }
                        else if ((sender as ToggleButton).Name.ToString().Equals("off"))
                        {
                            ((node as INodeVM).DataContext as OnOffSwitch).Off = true;
                        }
                    }
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }

    class TextBoxGotFocusEventBinding
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(TextBoxGotFocusEventBinding), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            TextBox textbox = depObj as TextBox;
            textbox.SelectAll();
            //textbox.GotFocus += textbox_GotFocus;
        }

        static void textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }
}

