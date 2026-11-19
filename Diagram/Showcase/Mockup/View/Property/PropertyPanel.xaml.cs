using Mockup.BusinessObject;
using Mockup.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Mockup.View.Property
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PropertyPanel : Page
    {
        public PropertyPanel()
        {
            this.InitializeComponent();
        } 

        private void leftVerticalTab_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).Name == "leftVerticalTab")
            {
                if (rightVerticalTab != null)
                    rightVerticalTab.IsChecked = false;
            }
            else if ((sender as ToggleButton).Name == "rightVerticalTab")
            {
                leftVerticalTab.IsChecked = false;
            }
        }

        private void topHorizontalTab_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).Name == "topHorizontalTab")
            {
                if (bottomHorizontalTab != null)
                    bottomHorizontalTab.IsChecked = false;
            }
            else if ((sender as ToggleButton).Name == "bottomHorizontalTab")
            {
                topHorizontalTab.IsChecked = false;
            }
        }        

        private void SettingPropertiesButton_Click_4(object sender, RoutedEventArgs e)
        {
            //if (colorpickerpopup.IsOpen)
                //colorpickerpopup.IsOpen = false;
            if (brushpicker.IsOpen)
                brushpicker.IsOpen = false;
            var ttv = (sender as Button).TransformToVisual(properties);
            Point screenCoords = ttv.TransformPoint(new Point(0, 0));
            brushpicker.IsOpen = true;
            //colorpickerpopup.IsOpen = true;
            colorpickerpopup.HorizontalOffset = properties.HorizontalOffset;
            colorpickerpopup.VerticalOffset = screenCoords.Y ;
        }

        private void TextPropertiesButton_Click_4(object sender, RoutedEventArgs e)
        {
            //if (colorpickerpopup.IsOpen)
                //colorpickerpopup.IsOpen = false;
            if (brushpicker.IsOpen)
                brushpicker.IsOpen = false;
            var ttv = (sender as Button).TransformToVisual(textProperties);
            Point screenCoords = ttv.TransformPoint(new Point(0, 0));
            brushpicker.IsOpen = true;
            //colorpickerpopup.IsOpen = true;
            colorpickerpopup.HorizontalOffset = textProperties.HorizontalOffset;
            colorpickerpopup.VerticalOffset = screenCoords.Y;
        }

        #region WindowBox 
        Point pointpressedOnWindowBox;
        bool pointerPressedOnWindowBoxtopMargin;
        bool pointerPressedOnWindowBoxbottomMargin;

        private void windowboxpanel_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.IsInContact)
            {
                if (pointerPressedOnWindowBoxtopMargin)
                {
                    Point po = e.GetCurrentPoint(windowboxpanel).Position;
                    if (topmarginborder.Margin.Top + (po.Y - pointpressedOnWindowBox.Y) > 0 && topmarginborder.Margin.Top + (po.Y - pointpressedOnWindowBox.Y) < 62)
                        topmarginborder.Margin = new Thickness(topmarginborder.Margin.Left, topmarginborder.Margin.Top + (po.Y - pointpressedOnWindowBox.Y), topmarginborder.Margin.Right, topmarginborder.Margin.Bottom);
                    pointpressedOnWindowBox = po;
                }
                else if (pointerPressedOnWindowBoxbottomMargin)
                {
                    Point po = e.GetCurrentPoint(windowboxpanel).Position;
                    if (bottommarginborder.Margin.Bottom + (pointpressedOnWindowBox.Y - po.Y) > 0 && po.Y > 30)
                        bottommarginborder.Margin = new Thickness(bottommarginborder.Margin.Left, bottommarginborder.Margin.Top, bottommarginborder.Margin.Right, bottommarginborder.Margin.Bottom + (pointpressedOnWindowBox.Y - po.Y));
                    pointpressedOnWindowBox = po;
                }
            }
        }

        private void topmarginicongrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            pointpressedOnWindowBox = e.GetCurrentPoint(windowboxpanel).Position;
            pointerPressedOnWindowBoxtopMargin = true;
        }        

        private void windowboxpanel_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            pointerPressedOnWindowBoxtopMargin = false;
            pointerPressedOnWindowBoxbottomMargin = false;
        }

        private void bottommarginicongrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            pointpressedOnWindowBox = e.GetCurrentPoint(windowboxpanel).Position;
            pointerPressedOnWindowBoxbottomMargin = true;
        }

        #endregion 

        private void iphone_Checked(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as ToggleButton, "Checked", true);
            if ((sender as ToggleButton).Name == "iphone")
            {
                ipad.IsChecked = false;
            }
            else if ((sender as ToggleButton).Name == "ipad")
            {
                iphone.IsChecked = false;
            }

        }

        private void wide_Checked(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as ToggleButton, "Checked", true);
            if ((sender as ToggleButton).Name == "wide")
            {
                standard.IsChecked = false;
            }
            else if ((sender as ToggleButton).Name == "standard")
            {
                wide.IsChecked = false;
            }
        }

        private void verticalipad_Checked(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as ToggleButton, "Checked", true);
            if ((sender as ToggleButton).Name == "verticalipad")
            {
                horizontalipad.IsChecked = false;
            }
            else if ((sender as ToggleButton).Name == "horizontalipad")
            {
                verticalipad.IsChecked = false;
            }
        }

        #region While MouseEnter / Leave on the Tab icons 
        private void editicon_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as ToggleButton, "MouseEnter", true);
        }

        private void editicon_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if((sender as ToggleButton).IsChecked.Value)
                VisualStateManager.GoToState(sender as ToggleButton, "MouseLeaveOnChecked", true);
            else
                VisualStateManager.GoToState(sender as ToggleButton, "MouseLeaveOnUnChecked", true);
        }
        #endregion

        #region Moving property panel
        //Point pointerPressedOnPropertyPanel;
        private void textProperties_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            //if (e.Pointer.IsInContact && !pointerPressedOnWindowBoxtopMargin && !pointerPressedOnWindowBoxbottomMargin)
            //{
            //    Point pointFromWindow = e.GetCurrentPoint((this.Parent as Grid).Parent as Page).Position;
            //    Point po = new Point(pointFromWindow.X - pointerPressedOnPropertyPanel.X, pointFromWindow.Y - pointerPressedOnPropertyPanel.Y);
            //    if ((sender as Panel).Parent is Popup)
            //    {
            //        ((sender as Panel).Parent as Popup).HorizontalOffset += po.X;
            //        ((sender as Panel).Parent as Popup).VerticalOffset += po.Y;
            //    }
            //    pointerPressedOnPropertyPanel = pointFromWindow;
            //}
        }

        private void textpropertiespanel_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //pointerPressedOnPropertyPanel = e.GetCurrentPoint((this.Parent as Grid).Parent as Page).Position;
        }

        private void textpropertiespanel_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
        }

        private void textpropertiespanel_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            //if (e.Pointer.IsInContact && !pointerPressedOnWindowBoxtopMargin && !pointerPressedOnWindowBoxbottomMargin)
            //{
            //    Point pointFromWindow = e.GetCurrentPoint((this.Parent as Grid).Parent as Page).Position;
            //    Point po = new Point(pointFromWindow.X - pointerPressedOnPropertyPanel.X, pointFromWindow.Y - pointerPressedOnPropertyPanel.Y);
            //    if ((sender as Panel).Parent is Popup)
            //    {
            //        ((sender as Panel).Parent as Popup).HorizontalOffset += po.X;
            //        ((sender as Panel).Parent as Popup).VerticalOffset += po.Y;
            //    }
            //    pointerPressedOnPropertyPanel = pointFromWindow;
            //}
        }
        #endregion        

        private void CustomToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if((sender as CustomToggleButton).Name == "left")
            {
                VisualStateManager.GoToState(right, "UnChecked", true);
                VisualStateManager.GoToState(horizontolcenter, "UnChecked", true);
            }
            else if((sender as CustomToggleButton).Name == "right")
            {
                VisualStateManager.GoToState(left, "UnChecked", true);
                VisualStateManager.GoToState(horizontolcenter, "UnChecked", true);
            }
            else if ((sender as CustomToggleButton).Name == "horizontolcenter")
            {
                VisualStateManager.GoToState(right, "UnChecked", true);
                VisualStateManager.GoToState(left, "UnChecked", true);
            }
        }

        private void Popup_Opened(object sender, object e)
        {
            //this.textpropertiespanel.Loaded += textpropertiespanel_Loaded;
            ((this.DataContext as DiagramBuilderVM).SelectedDiagram.SelectedItems as SelectorVM).PropertyPanelHeight = ((sender as Popup).Child as Panel).ActualHeight;
        }

        
    }
}
