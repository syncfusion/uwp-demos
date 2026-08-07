using Mockup.BusinessObject;
using Mockup.Utility;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Mockup.ViewModel
{
    public class SelectorVM : SelectorViewModel, ISelectorVM
    {
        double _mSelectorTabHeight = 50;
        double _mRulerHeight = 25;
        public SelectorVM(DiagramVM diagram)
        {                        
            Diagram = diagram;
            Diagrams = new ObservableCollection<DiagramVM>();
            Diagrams.CollectionChanged += Diagrams_CollectionChanged;
            LinkedPages = new ObservableCollection<string>();
             LinkedPages.Add("No Link");
            LinkedPages.Add("Web Address...");
            Diagram.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Info")
                {
                    (Diagram.Info as IGraphInfo).ItemSelectedEvent += ItemSelectedEvent;
                    (Diagram.Info as IGraphInfo).ItemUnSelectedEvent += ItemUnSelectedEvent;
                    (Diagram.Info as IGraphInfo).ViewPortChangedEvent += SelectorVM_ViewPortChangedEvent;
                    (Diagram.Info as IGraphInfo).ItemTappedEvent += SelectorVM_ItemTappedEvent;
                    //(Diagram.Info as IGraphInfo).PointerEnteredOnItem += SelectorVM_PointerEnteredOnItem;
                    //(Diagram.Info as IGraphInfo).PointerLeaveOnItem += SelectorVM_PointerLeaveOnItem;
                }
            };
            PickerCommand = new Command(param => CurrentBrush = (CurrentBrush)param);
            Diagram = diagram;
            
        }

        void Diagrams_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                string FileName = (e.NewItems[0] as DiagramVM).Title; //(e.NewItems[0] as DiagramVM).GetFileName();
                LinkedPages.Add(FileName);
            }
            else
            {
                string FileName = (e.OldItems[0] as DiagramVM).Title; //(e.OldItems[0] as DiagramVM).GetFileName();
                LinkedPages.Remove(FileName);
            }
        }

        //void SelectorVM_PointerLeaveOnItem(object sender, PointerEnterEventArgs args)
        //{
        //    if (args.Item is NodeVM)
        //    {
        //        if (!(args.Item as NodeVM).EditMode)
        //        {
        //            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
        //        }
        //    }
        //}

        //void SelectorVM_PointerEnteredOnItem(object sender, PointerEnterEventArgs args)
        //{
        //    if (args.Item is NodeVM)
        //    {
        //        if(!(args.Item as NodeVM).EditMode)
        //        {
        //            if((args.Item as NodeVM).Link != null && !string.IsNullOrEmpty((args.Item as NodeVM).Link.Link))
        //            {
        //                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
        //            }
        //        }
        //    }
        //}

        void SelectorVM_ItemTappedEvent(object sender, DiagramEventArgs args)
        {
            var node = (args.Item as INodeVM);
            if (node!=null&&!(node as NodeVM).EditMode)
            {
                if((node as NodeVM).Link !=null)
                {
                    if(!string.IsNullOrEmpty((node as NodeVM).Link.Link))
                    {
                        OpenLink((node as NodeVM).Link.Link);
                    }                      
                }
                if (node.Shape.Equals("tabsbar") || node.Shape.Equals("verticaltab"))
                {
                    OpenLink((node.DataContext as TabBusinessClass).ExecutedPath);
                }
                else if(node.Shape.Equals("buttonbar"))
                {
                    OpenLink((node.DataContext as ButtonBarBusinessClass).ExecutedPath);
                }
                else if (node.Shape.Equals("checkboxgroup"))
                {
                    OpenLink((node.DataContext as CheckBoxGroupBusinessClass).ExecutedPath);
                }
                else if (node.Shape.Equals("radiobuttongroup"))
                {
                    OpenLink((node.DataContext as RadioButtonGroupBusinessClass).ExecutedPath);
                }
                else if (node.Shape.Equals("menubar"))
                {
                    OpenLink((node.DataContext as MenuBarBusinessClass).ExecutedPath);
                }
                else if (node.Shape.Equals("menu"))
                {
                    OpenLink((node.DataContext as MenuBusinessClass).ExecutedPath);
                }
                else if (node.Shape.Equals("list"))
                {
                    OpenLink((node.DataContext as ListBusinessClass).ExecutedPath);
                }
                else if (node.Shape.Equals("breadcrumbs"))
                {
                    OpenLink((node.DataContext as BreadcrumbsBusinessClass).ExecutedPath);
                }
                else if (node.Shape.Equals("linkbar"))
                {
                    OpenLink((node.DataContext as LinkBarBusinessClass).ExecutedPath);
                }
                else if (node.Shape.Equals("alertbox"))
                {
                    OpenLink((node.DataContext as DialogBoxBusinessObject).ExecutedPath);
                }
                else if (node.Shape.Equals("messagebox"))
                {
                    OpenLink((node.DataContext as MessageBoxBusinessObject).ExecutedPath);
                }
            }
        }

        async void OpenLink(string uriToLaunch)
        {
            if (uriToLaunch.Contains("www."))
            {
                if (!uriToLaunch.Contains("http://")) { uriToLaunch = "http://" + uriToLaunch; }
                var uri = new Uri(uriToLaunch);
                await Windows.System.Launcher.LaunchUriAsync(uri);
            }
            else
            {
                foreach (var diagram in this.Diagrams) //this.Diagram.NeighbourDiagrams)
                {
                    //if (uriToLaunch.Equals(diagram.GetFileName()))
                    //{
                    //    diagram.IsSelected = true;
                    //}
                    if (uriToLaunch.Equals(diagram.Title))
                    {
                        diagram.IsSelected = true;
                    }
                }
            }
        }        

        void SelectorVM_ViewPortChangedEvent(object sender, ChangeEventArgs<object, ScrollChanged> args)
        {
            Scale = args.NewValue.CurrentZoom;
        }

        public DiagramVM Diagram { get; set; }
        public ObservableCollection<DiagramVM> Diagrams { get; set; }
        public ObservableCollection<string> LinkedPages { get; set; }

        List<IGroupableVM> SelectedItems = new List<IGroupableVM>();

        private void UpdateProperties(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case GroupableConstants.Label:
                    if (SelectedItems.Count == 1)
                    {
                        Label = SelectedItems.FirstOrDefault().Label;
                    }
                    UpdateSelectedKind();
                    break;
                case GroupableConstants.OffsetX:
                    if (SelectedItems.OfType<INode>().Count() == 1)
                    {
                        X = SelectedItems.OfType<INode>().FirstOrDefault().OffsetX;
                    }
                    break;
                case GroupableConstants.OffsetY:
                    if (SelectedItems.OfType<INode>().Count() == 1)
                    {
                        Y = SelectedItems.OfType<INode>().FirstOrDefault().OffsetY;
                    }
                    break;
                case GroupableConstants.UnitWidth:
                    if (SelectedItems.OfType<INode>().Count() == 1)
                    {
                        Width = SelectedItems.OfType<INode>().FirstOrDefault().UnitWidth;
                    }
                    break;
                case GroupableConstants.UnitHeight:
                    if (SelectedItems.OfType<INode>().Count() == 1)
                    {
                        Height = SelectedItems.OfType<INode>().FirstOrDefault().UnitHeight;
                    }
                    break;
                case GroupableConstants.RotateAngle:
                    if (SelectedItems.OfType<INode>().Count() == 1)
                    {
                        Angle = SelectedItems.OfType<INode>().FirstOrDefault().RotateAngle;

                    }
                    break;
                case GroupableConstants.HyperLink:
                    if (SelectedItems.Count == 1)
                    {
                        HyperLink = SelectedItems.FirstOrDefault().HyperLink;
                    }
                    UpdateSelectedKind();
                    break;
            }
        }

        void ItemSelectedEvent(object sender, DiagramEventArgs args)
        {
            //this.SelectorConstraints = SelectorConstraints.Default &~ SelectorConstraints.QuickCommands;

            var item = args.Item as IGroupableVM;
            var node = args.Item as INodeVM;            
            var con = args.Item as IConnectorVM;
            item.PropertyChanged += UpdateProperties;
            SelectedItems.Add(item);
            UpdateSelectedKind();
            if (SelectedItems.Count == 1)
            {
                if (item != null)
                {
                    this.Bold = item.Bold;
                    this.DashDot = item.DashDot;
                    this.Font = item.Font;  
                    this.FontSize = item.FontSize;
                    this.FontStyle = item.FontStyle;
                    this.FontWeight = item.FontWeight;
                    this.Italic = item.Italic;
                    this.Fill = node.Fill;
                    this.Label = item.Label;
                    //this.LabelHAlign = item.LabelHAlign;
                    this.LabelMargin = item.LabelMargin;
                    this.LabelVAlign = item.LabelVAlign;
                    this.LabelForeground = item.LabelForeground;
                    this.LabelBackground = item.LabelBackground;
                    this.Name = item.Name;
                    this.Stroke = item.Stroke;
                    this.Style = item.Style;
                    this.Thickness = item.Thickness;
                    this.Visibile = item.Visibility == Visibility.Visible;
                    this.Opacity = item.Opacity;
                    this.HyperLink = item.HyperLink;                    
                }
                if (node != null)
                {
                    this.DataContext = (item as INodeVM).DataContext;
                    if ((item as NodeVM).PropertiesList != null)
                    {
                        this.PropertiesList = new PropertiesList()
                        {
                            AlignmentTab = ((item as NodeVM).PropertiesList as PropertiesList).AlignmentTab,
                            EditTab = ((item as NodeVM).PropertiesList as PropertiesList).EditTab != null ? ((item as NodeVM).PropertiesList as PropertiesList).EditTab.Clone() as TextTab : null,
                            SettingTab = ((item as NodeVM).PropertiesList as PropertiesList).SettingTab != null ? ((item as NodeVM).PropertiesList as PropertiesList).SettingTab.Clone() as SettingTab : null,
                            LinkTab = ((item as NodeVM).PropertiesList as PropertiesList).LinkTab
                        };
                    }
                    this.X = node.OffsetX;
                    this.Y = node.OffsetY;
                    this.Px = node.Pivot.X;
                    this.Py = node.Pivot.Y;
                    if(item is IGroup)
                    {

                    }
                    //if (X.Value + this.Width / 2 > (this.Diagram.Info as IGraphInfo).ScrollInfo.HorizontalOffset + (this.Diagram.Info as IGraphInfo).ScrollInfo.ViewportWidth)
                    //    this.PropertyPanelOffsetX = X.Value - (this.Width.Value / 2 + 60) - (this.Diagram.Info as IGraphInfo).ScrollInfo.HorizontalOffset;
                    //else
                    //    this.PropertyPanelOffsetX = X.Value + this.Width.Value / 2 + 20 - (this.Diagram.Info as IGraphInfo).ScrollInfo.HorizontalOffset;

                    this.PropertyPanelOffsetY = node.OffsetY - node.UnitHeight / 2 - this.Diagram.ScrollSettings.ScrollInfo.VerticalOffset;
                    if ((item as INodeVM).Constraints.Contains(NodeConstraints.AspectRatio))
                        AspectRatioEnabled = true;
                    else
                        AspectRatioEnabled = false;

                    if ((item as INodeVM).Constraints == NodeConstraints.Selectable)
                        IsNodeLocked = true;
                    else
                        IsNodeLocked = false;

                    if ((item as NodeVM).Link != null)
                    {
                        this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                        this.Links.Add((item as NodeVM).Link);
                    }

                    if ((item as NodeVM).Shape != null)
                    {
                        if(this.DataContext is PropertyChange)
                        {
                            this.LabelHAlign = (this.DataContext as PropertyChange).HorizontalAlignment;
                        }

                        #region alertbox
                        if ((item as NodeVM).Shape.Equals("alertbox"))
                        {
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            Links.Add(new HyperlinkBusinessClass()
                            {
                                Title = ((item as NodeVM).DataContext as DialogBoxBusinessObject).Ok,
                                Link = ((item as NodeVM).DataContext as DialogBoxBusinessObject).OkLink.Link,
                                SelectedLinkIndex = ((item as NodeVM).DataContext as DialogBoxBusinessObject).OkLink.SelectedLinkIndex
                            });
                            Links.Add(new HyperlinkBusinessClass()
                            {
                                Title = ((item as NodeVM).DataContext as DialogBoxBusinessObject).Cancel,
                                Link = ((item as NodeVM).DataContext as DialogBoxBusinessObject).CancelLink.Link,
                                SelectedLinkIndex = ((item as NodeVM).DataContext as DialogBoxBusinessObject).CancelLink.SelectedLinkIndex
                            });
                        }
                        #endregion
                        #region messagebox
                        else if ((item as NodeVM).Shape.Equals("messagebox"))
                        {
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            Links.Add(new HyperlinkBusinessClass()
                            {
                                Title = ((item as NodeVM).DataContext as MessageBoxBusinessObject).Ok,
                                Link = ((item as NodeVM).DataContext as MessageBoxBusinessObject).Link.Link,
                                SelectedLinkIndex = ((item as NodeVM).DataContext as MessageBoxBusinessObject).Link.SelectedLinkIndex
                            });
                        }
                        #endregion
                        #region checkboxgroup
                        else if ((item as NodeVM).Shape.Equals("checkboxgroup"))
                        {
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            foreach (var i in ((item as INodeVM).DataContext as CheckBoxGroupBusinessClass).Items)
                            {
                                this.Links.Add(new HyperlinkBusinessClass() { Title = (i as CheckBoxItemBusinessClass).Item, Link = (i as CheckBoxItemBusinessClass).Link.Link, SelectedLinkIndex = (i as CheckBoxItemBusinessClass).Link.SelectedLinkIndex });
                            }
                        }
                        #endregion
                        #region radiobuttongroup
                        else if ((item as NodeVM).Shape.Equals("radiobuttongroup"))
                        {
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            foreach (var i in ((item as INodeVM).DataContext as RadioButtonGroupBusinessClass).Items)
                            {
                                this.Links.Add(new HyperlinkBusinessClass() { Title = (i as RadioButtonItemBusinessClass).Item, Link = (i as RadioButtonItemBusinessClass).Link.Link, SelectedLinkIndex = (i as RadioButtonItemBusinessClass).Link.SelectedLinkIndex });
                            }
                        }
                        #endregion
                        #region windowbox
                        else if ((item as NodeVM).Shape.Equals("windowbox"))
                        {
                            this.SliderValue = ((node.DataContext as WindowBoxBusinessObject).ScrollBar.SliderValue / ((node.DataContext as WindowBoxBusinessObject).WindowBodyHeight - ((node.DataContext as WindowBoxBusinessObject).ScrollBar.TopButtonSize + (node.DataContext as WindowBoxBusinessObject).ScrollBar.BottomButtonSize + (node.DataContext as WindowBoxBusinessObject).ScrollBar.ScrollThumbHeight))) * 100;
                        }
                        #endregion
                        #region breadcrumbs
                        else if ((item as NodeVM).Shape.Equals("breadcrumbs"))
                        {
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            foreach (var i in ((item as INodeVM).DataContext as BreadcrumbsBusinessClass).Items)
                            {
                                this.Links.Add(new HyperlinkBusinessClass() { Title = (i as BreadcrumbsItemBusinessClass).Item, Link = (i as BreadcrumbsItemBusinessClass).Link.Link, SelectedLinkIndex = (i as BreadcrumbsItemBusinessClass).Link.SelectedLinkIndex });
                            }
                        }
                        #endregion
                        #region horizotalslider / verticalslider
                        else if ((item as NodeVM).Shape.Equals("horizotalslider") || ((item as NodeVM).Shape.Equals("verticalslider")))
                        {
                            if ((item as NodeVM).Shape.Equals("horizotalslider"))
                                this.SliderValue = ((node.DataContext as SliderBusinessClass).SliderValue / (node.UnitWidth - (node.DataContext as SliderBusinessClass).SliderBallSize)) * 100;
                            else if ((item as NodeVM).Shape.Equals("verticalslider"))
                                this.SliderValue = ((node.DataContext as SliderBusinessClass).SliderValue / (node.UnitHeight - (node.DataContext as SliderBusinessClass).SliderBallSize)) * 100;

                            this.States = (node.DataContext as SliderBusinessClass).States;

                            if ((node.DataContext as SliderBusinessClass).Selected)
                                this.SelectedIndex = 0;
                            else
                                this.SelectedIndex = 1;
                        }
                        #endregion
                        #region progressbar
                        else if ((item as NodeVM).Shape.Equals("progressbar"))
                        {
                            this.SliderValue = ((node.DataContext as ProgressBarBusinessClass).ProgressValue / node.UnitWidth) * 100;
                        }
                        #endregion
                        #region browserWindow
                        else if ((item as NodeVM).Shape.Equals("browserWindow"))
                        {
                            this.SliderValue = ((node.DataContext as BrowserWindowBusinessClass).ScrollBar.SliderValue / ((node.DataContext as BrowserWindowBusinessClass).BrowserBodyHeight - ((node.DataContext as BrowserWindowBusinessClass).ScrollBar.TopButtonSize + (node.DataContext as BrowserWindowBusinessClass).ScrollBar.BottomButtonSize + (node.DataContext as BrowserWindowBusinessClass).ScrollBar.ScrollThumbHeight))) * 100;
                        }
                        #endregion
                        #region ScrollBar
                        else if ((item as NodeVM).Shape.Equals("horizontalscrollbar"))
                        {
                            this.SliderValue = ((node.DataContext as HorizontalScrollBarBusinessClass).SliderValue / (node.UnitWidth - ((node.DataContext as HorizontalScrollBarBusinessClass).LeftButtonSize + (node.DataContext as HorizontalScrollBarBusinessClass).RightButtonSize + (node.DataContext as HorizontalScrollBarBusinessClass).ScrollThumbWidth))) * 100;
                        }
                        else if ((item as NodeVM).Shape.Equals("verticalscrollbar"))
                        {
                            this.SliderValue = ((node.DataContext as VerticalScrollBarBusinessClass).SliderValue / (node.UnitHeight - ((node.DataContext as VerticalScrollBarBusinessClass).TopButtonSize + (node.DataContext as VerticalScrollBarBusinessClass).BottomButtonSize + (node.DataContext as VerticalScrollBarBusinessClass).ScrollThumbHeight))) * 100;
                        }
                        #endregion
                        #region volumeslider
                        else if ((item as NodeVM).Shape.Equals("volumeslider"))
                        {
                            this.SliderValue = ((node.DataContext as VolumeSliderBusinessClass).SliderValue / (node.UnitWidth - (node.DataContext as VolumeSliderBusinessClass).VolumeSymbolWidth - (node.DataContext as VolumeSliderBusinessClass).SliderBallSize)) * 100;
                        }
                        #endregion
                        #region checkbox / radiobutton
                        else if ((item as NodeVM).Shape.Equals("checkbox")
                            || (item as NodeVM).Shape.Equals("radiobutton")
                            )
                        {
                            this.States = (node.DataContext as CheckBoxBusinessClass).States;
                            this.SelectedIndex = (node.DataContext as CheckBoxBusinessClass).SelectedIndex;
                        }
                        #endregion
                        #region datechooser
                        else if ((item as NodeVM).Shape.Equals("datechooser"))
                        {
                            this.States = (node.DataContext as DateChoosetBusinessClass).States;
                            this.SelectedIndex = (node.DataContext as DateChoosetBusinessClass).SelectedIndex;
                        }
                        #endregion
                        #region searchbutton
                        else if ((item as NodeVM).Shape.Equals("searchbutton"))
                        {
                            this.States = (node.DataContext as SearchBoxBusinessClass).States;
                            SelectedIndex = ((item as NodeVM).DataContext as SearchBoxBusinessClass).SelectedIndex;
                        }
                        #endregion
                        #region numericstepper
                        else if ((item as NodeVM).Shape.Equals("numericstepper"))
                        {
                            this.States = (node.DataContext as NumericStepperBusinessClass).States;
                            if ((node.DataContext as NumericStepperBusinessClass).Selected)
                                this.SelectedIndex = 0;
                            else
                                this.SelectedIndex = 1;
                        }
                        #endregion
                        #region multilinebutton
                        else if ((item as NodeVM).Shape.Equals("multilinebutton"))
                        {
                            //(this.PropertyEditorUIVisibility as PropertyEditorUI).LabelFont = true;
                            //this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            //this.Links.Add(new HyperlinkBusinessClass()
                            //{
                            //    Title = ((item as NodeVM).DataContext as ButtonBusinessObject).Link.Title,
                            //    Link = ((item as NodeVM).DataContext as ButtonBusinessObject).Link.Link,
                            //    SelectedLinkIndex = ((item as NodeVM).DataContext as ButtonBusinessObject).Link.SelectedLinkIndex
                            //});
                        }
                        #endregion
                        #region button
                        else if ((item as NodeVM).Shape.Equals("button"))
                        {
                            this.States = (node.DataContext as ButtonBusinessObject).States;
                            this.SelectedIndex = (node.DataContext as ButtonBusinessObject).SelectedIndex;
                        }
                        #endregion
                        #region pointybutton
                        else if ((item as NodeVM).Shape.Equals("pointybutton"))
                        {
                            this.States = (node.DataContext as PointyButtonBusinessClass).States;
                            if (((item as NodeVM).DataContext as PointyButtonBusinessClass).Disabled)
                                SelectedIndex = 1;
                            else
                                SelectedIndex = 0;
                        }
                        #endregion
                        #region combobox
                        else if ((item as NodeVM).Shape.Equals("combobox"))
                        {
                            this.States = (node.DataContext as ComboBoxBusinessClass).States;
                            SelectedIndex = (node.DataContext as ComboBoxBusinessClass).SelectedIndex;
                        }
                        #endregion
                        #region menubar
                        else if ((item as NodeVM).Shape.Equals("menubar"))
                        {
                            this.States = new ObservableCollection<object>();
                            this.States.Add("None");
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            foreach (var i in (node.DataContext as MenuBarBusinessClass).Items)
                            {
                                this.States.Add((i as MenuTitleBusinessClass).Item);
                                this.Links.Add(new HyperlinkBusinessClass() { Title = (i as MenuTitleBusinessClass).Item, Link = (i as MenuTitleBusinessClass).Link.Link, SelectedLinkIndex = (i as MenuTitleBusinessClass).Link.SelectedLinkIndex });
                            }
                            if ((node.DataContext as MenuBarBusinessClass).SelectedIndex <= (node.DataContext as MenuBarBusinessClass).Items.Count)
                                this.SelectedIndex = (node.DataContext as MenuBarBusinessClass).SelectedIndex;
                            else
                                this.SelectedIndex = 0;
                        }
                        #endregion
                        #region menu
                        else if ((item as NodeVM).Shape.Equals("menu"))
                        {
                            this.States = new ObservableCollection<object>();
                            this.States.Add("None");
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            foreach (var i in (node.DataContext as MenuBusinessClass).Items)
                            {
                                this.States.Add((i as MenuItemBusinessClass).Item);
                                this.Links.Add(new HyperlinkBusinessClass() { Title = (i as MenuItemBusinessClass).Item, Link = (i as MenuItemBusinessClass).Link.Link, SelectedLinkIndex = (i as MenuItemBusinessClass).Link.SelectedLinkIndex });
                            }
                            if ((node.DataContext as MenuBusinessClass).Items.Count() > (node.DataContext as MenuBusinessClass).SelectedIndex)
                                this.SelectedIndex = (node.DataContext as MenuBusinessClass).SelectedIndex;
                            else
                                this.SelectedIndex = 0;
                        }
                        #endregion
                        #region buttonbar
                        else if ((item as NodeVM).Shape.Equals("buttonbar"))
                        {
                            this.States = new ObservableCollection<object>();
                            this.States.Add("None");
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            foreach (var i in (node.DataContext as ButtonBarBusinessClass).Items)
                            {
                                this.States.Add((i as ButtonBarItemBusinessClass).Item);
                                this.Links.Add(new HyperlinkBusinessClass()
                                {
                                    Title = (i as ButtonBarItemBusinessClass).Item,
                                    Link = (i as ButtonBarItemBusinessClass).Link.Link,
                                    SelectedLinkIndex = (i as ButtonBarItemBusinessClass).Link.SelectedLinkIndex
                                });
                            }

                            this.SelectedIndex = (node.DataContext as ButtonBarBusinessClass).SelectedIndex;
                        }
                        #endregion
                        #region tabsbar
                        else if ((item as NodeVM).Shape.Equals("tabsbar"))
                        {
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            this.SliderValue = ((node.DataContext as TabBusinessClass).ScrollBar.SliderValue / ((node.UnitHeight - (FontSize.Value + 2 * node.LabelMargin.Top + node.LabelMargin.Bottom)) - ((node.DataContext as TabBusinessClass).ScrollBar.TopButtonSize + (node.DataContext as TabBusinessClass).ScrollBar.BottomButtonSize + (node.DataContext as TabBusinessClass).ScrollBar.ScrollThumbHeight))) * 100;
                            this.States = new ObservableCollection<object>();
                            this.States.Add("None");
                            foreach (var i in (node.DataContext as TabBusinessClass).Items as ObservableCollection<object>)
                            {
                                this.States.Add((i as TabItemBusinessClass).Item);
                                Links.Add(new HyperlinkBusinessClass()
                                {
                                    Title = (i as TabItemBusinessClass).Link.Title,
                                    Link = (i as TabItemBusinessClass).Link.Link,
                                    SelectedLinkIndex = (i as TabItemBusinessClass).Link.SelectedLinkIndex
                                });
                            }

                            this.SelectedIndex = (node.DataContext as TabBusinessClass).SelectedIndex;
                        }
                        #endregion
                        #region verticaltab
                        else if ((item as NodeVM).Shape.Equals("verticaltab"))
                        {
                            this.SliderValue = ((node.DataContext as TabBusinessClass).ScrollBar.SliderValue / (node.UnitHeight - ((node.DataContext as TabBusinessClass).ScrollBar.TopButtonSize + (node.DataContext as TabBusinessClass).ScrollBar.BottomButtonSize + (node.DataContext as TabBusinessClass).ScrollBar.ScrollThumbHeight))) * 100;
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            this.States = new ObservableCollection<object>();
                            this.States.Add("None");
                            foreach (var i in (node.DataContext as TabBusinessClass).Items as ObservableCollection<object>)
                            {
                                this.States.Add((i as TabItemBusinessClass).Item);
                                Links.Add(new HyperlinkBusinessClass() { Title = (i as TabItemBusinessClass).Link.Title, Link = (i as TabItemBusinessClass).Link.Link, SelectedLinkIndex = (i as TabItemBusinessClass).Link.SelectedLinkIndex });
                            }
                            this.SelectedIndex = (node.DataContext as TabBusinessClass).SelectedIndex;
                        }
                        #endregion
                        #region accordion
                        else if ((item as NodeVM).Shape.Equals("accordion"))
                        {
                            int selectedindex = 0;
                            this.States = null;
                            this.States = new ObservableCollection<object>();
                            this.States.Add(new AccordionItemBusinessObject() { Item = "None" });                            
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            foreach (var i in (node.DataContext as AccordionBusinessObject).Items)
                            {
                                selectedindex += 1;
                                this.States.Add((i as ICommonForCollectionItem));
                                this.Links.Add(new HyperlinkBusinessClass()
                                {
                                    Title = (i as AccordionItemBusinessObject).Item,
                                    Link = (i as AccordionItemBusinessObject).Link.Link,
                                    SelectedLinkIndex = (i as AccordionItemBusinessObject).Link.SelectedLinkIndex
                                });
                                
                                if ((i as AccordionItemBusinessObject).Items != null)
                                {
                                    selectedindex += 1;
                                    foreach (var subitem in (i as AccordionItemBusinessObject).Items)
                                    {
                                        this.States.Add((subitem as ICommonForCollectionItem));
                                        this.Links.Add(new HyperlinkBusinessClass()
                                        {
                                            Title = (subitem as AccordionSubItemBusinessObject).Item,
                                            Link = (subitem as AccordionSubItemBusinessObject).Link.Link,
                                            SelectedLinkIndex = (subitem as AccordionSubItemBusinessObject).Link.SelectedLinkIndex
                                        });

                                        if((i as AccordionItemBusinessObject).IsExpanded)
                                        {
                                            if ((subitem as AccordionSubItemBusinessObject).IsSelected)
                                            {
                                                this.SelectedItem = subitem;
                                                this.SelectedIndex = selectedindex;
                                            }
                                        }
                                    }
                                }
                            }
                            //this.SelectedIndex = (node.DataContext as AccordionBusinessObject).SelectedIndex;
                        }
                        #endregion
                        #region list
                        else if ((item as NodeVM).Shape.Equals("list"))
                        {
                            this.SliderValue = ((node.DataContext as ListBusinessClass).ScrollBar.SliderValue / (node.UnitHeight - ((node.DataContext as ListBusinessClass).ScrollBar.TopButtonSize + (node.DataContext as ListBusinessClass).ScrollBar.BottomButtonSize + (node.DataContext as ListBusinessClass).ScrollBar.ScrollThumbHeight))) * 100;
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            this.States = new ObservableCollection<object>();
                            this.States.Add("None");
                            foreach (var i in (node.DataContext as ListBusinessClass).Items)
                            {
                                this.States.Add((i as ListItemBusinessClass).Item);
                                this.Links.Add(new HyperlinkBusinessClass() { Title = (i as ListItemBusinessClass).Item, Link = (i as ListItemBusinessClass).Link.Link, SelectedLinkIndex = (i as ListItemBusinessClass).Link.SelectedLinkIndex });
                            }
                            this.SelectedIndex = (node.DataContext as ListBusinessClass).SelectedIndex + 1;
                        }
                        #endregion
                        #region link
                        else if ((item as NodeVM).Shape.Equals("link"))
                        {
                            this.States = (node.DataContext as LinkBusinessClass).States;
                            this.SelectedIndex = ((item as NodeVM).DataContext as LinkBusinessClass).SelectedIndex;
                        }
                        #endregion
                        #region linkbar
                        else if ((item as NodeVM).Shape.Equals("linkbar"))
                        {
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            this.States = new ObservableCollection<object>();
                            this.States.Add("None");
                            foreach (var i in ((item as INodeVM).DataContext as LinkBarBusinessClass).Items)
                            {
                                this.States.Add((i as LinkBarItemBusinessClass).Item);
                                this.Links.Add(new HyperlinkBusinessClass() { Title = (i as LinkBarItemBusinessClass).Item, Link = (i as LinkBarItemBusinessClass).Link.Link, SelectedLinkIndex = (i as LinkBarItemBusinessClass).Link.SelectedLinkIndex });
                            }
                            if ((node.DataContext as LinkBarBusinessClass).SelectedIndex <= ((item as INodeVM).DataContext as LinkBarBusinessClass).Items.Count())
                                this.SelectedIndex = (node.DataContext as LinkBarBusinessClass).SelectedIndex;
                            else
                                this.SelectedIndex = 0;
                        }
                        #endregion
                        #region label
                        else if ((item as NodeVM).Shape.Equals("label"))
                        {
                            (this.PropertyEditorUIVisibility as PropertyEditorUI).Collection = true;
                            (this.PropertyEditorUIVisibility as PropertyEditorUI).LabelOrientation = true;
                            //this.TabItem = new ObservableCollection<object>();
                            this.Links = new ObservableCollection<HyperlinkBusinessClass>();
                            this.Links.Add(new HyperlinkBusinessClass()
                            {
                                Title = ((item as INodeVM).DataContext as LabelBusinessClass).Link.Title,
                                Link = ((item as INodeVM).DataContext as LabelBusinessClass).Link.Link,
                                SelectedLinkIndex = ((item as INodeVM).DataContext as LabelBusinessClass).Link.SelectedLinkIndex
                            });
                            this.States = ((item as INodeVM).DataContext as LabelBusinessClass).States;
                            //foreach (var i in (node.DataContext as LabelBusinessClass).States as ObservableCollection<CheckBoxState>)
                            //{
                            //    (this.TabItem as ObservableCollection<object>).Add(i);
                            //}

                            if ((node.DataContext as LabelBusinessClass).SelectedState == "Normal")
                                this.SelectedIndex = 0;
                            else if ((node.DataContext as LabelBusinessClass).SelectedState == "Disabled")
                                this.SelectedIndex = 1;
                        }
                        #endregion
                        #region textinput
                        else if ((item as NodeVM).Shape.Equals("textinput"))
                        {
                            this.States = (node.DataContext as TextAreaBusinessClass).States;
                            this.SelectedIndex = ((item as INodeVM).DataContext as TextAreaBusinessClass).SelectedIndex;
                        }
                        #endregion
                        #region textarea
                        else if ((item as NodeVM).Shape.Equals("textarea"))
                        {
                            this.SliderValue = ((node.DataContext as TextAreaBusinessClass).ScrollBar.SliderValue / (node.UnitHeight - ((node.DataContext as TextAreaBusinessClass).ScrollBar.TopButtonSize + (node.DataContext as TextAreaBusinessClass).ScrollBar.BottomButtonSize + (node.DataContext as TextAreaBusinessClass).ScrollBar.ScrollThumbHeight))) * 100;
                            this.States = (node.DataContext as TextAreaBusinessClass).States;
                            this.SelectedIndex = ((item as INodeVM).DataContext as TextAreaBusinessClass).SelectedIndex;
                        }
                        #endregion
                    }
                    this.Width = node.UnitWidth;
                    this.Height = node.UnitHeight;
                    this.Angle = node.RotateAngle;
                    this.AllowDrag = node.AllowDrag;
                    this.AllowResize = node.AllowResize;
                    this.AllowRotate = node.AllowRotate;
                    this.TabSelectedIndex = node.TabSelectedIndex;
                    this.TabSelectedItem = node.TabSelectedItem;
                }
                else
                {
                    this.DataContext = null;
                    this.X = null;
                    this.Y = null;
                    this.Px = null;
                    this.Py = null;
                    this.Width = null;
                    this.Height = null;
                    this.Angle = null;
                    this.AllowDrag = null;
                    this.AllowResize = null;
                    this.AllowRotate = null;
                    this.Fill = null;
                }
                if (con != null)
                {
                    this.SourceCap = con.SourceCap;
                    this.TargetCap = con.TargetCap;
                    this.Type = con.Type;
                }
                else
                {
                    SourceCap = null;
                    TargetCap = null;
                    Type = null;
                }
            }
            if (SelectedItems.OfType<INodeVM>().Count() == 1)
            {
                node = SelectedItems.OfType<INodeVM>().FirstOrDefault();
                this.X = node.OffsetX;
                this.Y = node.OffsetY;
                this.Px = node.Pivot.X;
                this.Py = node.Pivot.Y;                
                this.Width = node.UnitWidth;
                this.Height = node.UnitHeight;
                this.Angle = node.RotateAngle;
                this.AllowDrag = node.AllowDrag;
                this.AllowResize = node.AllowResize;
                this.AllowRotate = node.AllowRotate;
                //this.Fill = node.Fill; 
                this.TabSelectedIndex = node.TabSelectedIndex;
            }

            if (SelectedItems.OfType<IConnectorVM>().Count() == 1)
            {
                con = SelectedItems.OfType<IConnectorVM>().FirstOrDefault();
                this.SourceCap = con.SourceCap;
                this.TargetCap = con.TargetCap;
                this.Type = con.Type;
            }
            if (SelectedItems.Count > 1)
            {
                foreach(var i in SelectedItems.OfType<INodeVM>())
                { 
                    if ((i as NodeVM).PropertiesList != null && PropertiesList != null)
                    {
                        #region EditTab
                        if ((PropertiesList as PropertiesList).EditTab != null)
                        {
                            if (((i as NodeVM).PropertiesList as PropertiesList).EditTab == null)
                                (PropertiesList as PropertiesList).EditTab = null;
                            else
                            {
                                if (((PropertiesList as PropertiesList).EditTab as TextTab).Default)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).EditTab as TextTab).Default)
                                        ((PropertiesList as PropertiesList).EditTab as TextTab).Default = false;
                                }

                                if (((PropertiesList as PropertiesList).EditTab as TextTab).TextAlignment)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).EditTab as TextTab).TextAlignment)
                                        ((PropertiesList as PropertiesList).EditTab as TextTab).TextAlignment = false;
                                }

                                if (((PropertiesList as PropertiesList).EditTab as TextTab).FontFamily)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).EditTab as TextTab).FontFamily)
                                        ((PropertiesList as PropertiesList).EditTab as TextTab).FontFamily = false;
                                }

                                if (((PropertiesList as PropertiesList).EditTab as TextTab).Foreground)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).EditTab as TextTab).Foreground)
                                        ((PropertiesList as PropertiesList).EditTab as TextTab).Foreground = false;
                                }

                                if (((PropertiesList as PropertiesList).EditTab as TextTab).FontSize)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).EditTab as TextTab).FontSize)
                                        ((PropertiesList as PropertiesList).EditTab as TextTab).FontSize = false;
                                }

                                if (((PropertiesList as PropertiesList).EditTab as TextTab).FontStyle)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).EditTab as TextTab).FontStyle)
                                        ((PropertiesList as PropertiesList).EditTab as TextTab).FontStyle = false;
                                }
                            }
                        }
                        #endregion

                        #region SettingTab
                        if ((PropertiesList as PropertiesList).SettingTab != null)
                        {
                            if (((i as NodeVM).PropertiesList as PropertiesList).SettingTab == null)
                                (PropertiesList as PropertiesList).SettingTab = null;
                            else
                            {
                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).Fill)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).Fill)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).Fill = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).Opacity)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).Opacity)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).Opacity = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).Stroke)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).Stroke)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).Stroke = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).StrokeThickness)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).StrokeThickness)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).StrokeThickness = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).Collection)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).Collection)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).Collection = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).Selection)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).Selection)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).Selection = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ScrollSlider)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).ScrollSlider)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).ScrollSlider = false;
                                }

                                //if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ScrollBar)
                                //{
                                //    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).ScrollBar)
                                //        ((PropertiesList as PropertiesList).SettingTab as SettingTab).ScrollBar = false;
                                //}

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).VerticalScrollBar)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).VerticalScrollBar)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).VerticalScrollBar = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).StrokeStyle)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).StrokeStyle)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).StrokeStyle = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ListOddEven)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).ListOddEven)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).ListOddEven = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).OnOffSwitch)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).OnOffSwitch)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).OnOffSwitch = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).PointyButton)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).PointyButton)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).PointyButton = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ToolTip)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).ToolTip)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).ToolTip = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).VerticalTab)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).VerticalTab)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).VerticalTab = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).HorizontalTab)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).HorizontalTab)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).HorizontalTab = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).GeometricShapes)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).GeometricShapes)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).GeometricShapes = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).VerticalCurlyBraces)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).VerticalCurlyBraces)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).VerticalCurlyBraces = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).HorizontalCurlyBraces)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).HorizontalCurlyBraces)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).HorizontalCurlyBraces = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).Popover)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).Popover)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).Popover = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ShowBorder)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).ShowBorder)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).ShowBorder = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).MenuIcon)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).MenuIcon)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).MenuIcon = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).WindowBox)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).WindowBox)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).WindowBox = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ioskeyboard)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).ioskeyboard)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).ioskeyboard = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ipad)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).ipad)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).ipad = false;
                                }

                                if (((PropertiesList as PropertiesList).SettingTab as SettingTab).IPhone)
                                {
                                    if (!(((i as NodeVM).PropertiesList as PropertiesList).SettingTab as SettingTab).IPhone)
                                        ((PropertiesList as PropertiesList).SettingTab as SettingTab).IPhone = false;
                                }
                            }
                        }
                        #endregion

                        #region LinkTab
                        if ((PropertiesList as PropertiesList).LinkTab != false)
                        {
                            if (!((i as NodeVM).PropertiesList as PropertiesList).LinkTab)
                            {
                                (PropertiesList as PropertiesList).LinkTab = false;
                                this.OpenLinkPanel = null;
                            }
                        }
                        #endregion
                    }

                    if (PropertiesList != null && (PropertiesList as PropertiesList).SettingTab != null)
                    {
                        if ((PropertiesList as PropertiesList).SettingTab.Collection || (PropertiesList as PropertiesList).SettingTab.Selection)
                        {
                            if (i.DataContext is IState)
                            {
                                ObservableCollection<object> localstates = new ObservableCollection<object>(this.States);
                                foreach (var state in localstates)
                                {
                                    if (!(i.DataContext as IState).States.Contains(state))
                                        this.States.Remove(state);
                                }
                                localstates = null;
                            }
                        }
                    }
                }

                if (PropertiesList != null)
                {
                    #region EditTab
                    if ((PropertiesList as PropertiesList).EditTab != null)
                    {
                        bool flag = false;
                        if (((PropertiesList as PropertiesList).EditTab as TextTab).Default) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).EditTab as TextTab).TextAlignment) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).EditTab as TextTab).FontFamily) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).EditTab as TextTab).Foreground) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).EditTab as TextTab).FontSize) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).EditTab as TextTab).FontStyle) { flag = true; goto final; }

                    final: ;
                    if (!flag)
                    {
                        (PropertiesList as PropertiesList).EditTab = null;
                        this.OpenTextPanel = null;
                    }
                    }
                    #endregion

                    #region SettingTab
                    if ((PropertiesList as PropertiesList).SettingTab != null)
                    {
                        bool flag = false;
                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).Fill) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).Opacity) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).Stroke) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).StrokeThickness) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).Collection) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).Selection) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ScrollSlider) { flag = true; goto final; }

                        //if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ScrollBar) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).VerticalScrollBar) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).StrokeStyle) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ListOddEven) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).OnOffSwitch) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).PointyButton) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ToolTip) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).VerticalTab) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).HorizontalTab) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).GeometricShapes) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).VerticalCurlyBraces) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).HorizontalCurlyBraces) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).Popover) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ShowBorder) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).MenuIcon) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).WindowBox) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ioskeyboard) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).ipad) { flag = true; goto final; }

                        if (((PropertiesList as PropertiesList).SettingTab as SettingTab).IPhone) { flag = true; goto final; }

                    final: ;
                    if (!flag)
                    {
                        (PropertiesList as PropertiesList).SettingTab = null;
                        this.OpenSettingPanel = null;
                    }
                    }
                    #endregion

                    if ((PropertiesList as PropertiesList).SettingTab != null && (PropertiesList as PropertiesList).SettingTab.Selection)
                    {
                        if (States != null)
                            States.Clear();
                        else
                            States = new ObservableCollection<object>();

                        States.Add("None");
                        foreach (var i in SelectedItems.OfType<INodeVM>())
                        {
                            for (int x = 1; x <= (i.DataContext as ICommonForCollection).Items.Count; x++)
                            {
                                if(States.Count > x)
                                {
                                    States[x] = States[x] + " / " + ((i.DataContext as ICommonForCollection).Items[x-1] as ICommonForCollectionItem).Item;
                                }
                                else
                                {
                                    States.Add(((i.DataContext as ICommonForCollection).Items[x-1] as ICommonForCollectionItem).Item);
                                }
                            }
                        }                       
                    }
                }
                if (item != null)
                {                    
                    //if(DataContext != node.DataContext)
                    //{
                    //    this.DataContext = null;
                    //}                    
                    if (Bold != item.Bold)
                    {
                        Bold = null;
                    }
                    if (DashDot != item.DashDot)
                    {
                        DashDot = null;
                    }
                    if (Font != null &&
                        item.Font != null &&
                        Font.Source != item.Font.Source)
                    {
                        Font = null;
                    }
                    if (FontSize != item.FontSize)
                    {
                        FontSize = null;
                    }
                    if (FontStyle != item.FontStyle)
                    {
                        FontStyle = null;
                    }
                    if (FontWeight != null && FontWeight.Value.Weight != item.FontWeight.Weight)
                    {
                        FontWeight = null;
                    }
                    if (Italic != item.Italic)
                    {
                        Italic = null;
                    }
                    if (Label != item.Label)
                    {
                        Label = null;
                        //Label = string.Empty;
                        //foreach (var selectedNode in SelectedItems.OfType<INodeVM>())
                        //{
                        //    Label = selectedNode.Label;
                        //}
                    }
                    if (LabelHAlign != item.LabelHAlign)
                    {
                        LabelHAlign = null;
                    }
                    //if (LabelMargin != item.LabelMargin.Left)
                    if (LabelMargin != item.LabelMargin)
                    {
                        LabelMargin = null;
                    }
                    if (LabelVAlign != item.LabelVAlign)
                    {
                        LabelVAlign = null;
                    }
                    if (LabelForeground != null &&
                        item.LabelForeground != null &&
                        (LabelForeground as SolidColorBrush).Color !=
                        (item.LabelForeground as SolidColorBrush).Color)
                    {
                        LabelForeground = null;
                    }
                    if (LabelBackground != null &&
                        item.LabelBackground != null &&
                        (LabelBackground as SolidColorBrush).Color !=
                        (item.LabelBackground as SolidColorBrush).Color)
                    {
                        LabelBackground = null;
                    }
                    if (Name != item.Name)
                    {
                        Name = string.Empty;
                    }
                    if (Stroke != null &&
                        item.Stroke != null &&
                        (Stroke as SolidColorBrush).Color
                        != (item.Stroke as SolidColorBrush).Color)
                    {
                        Stroke = null;
                    }
                    if (Style != item.Style)
                    {
                        Style = null;
                    }
                    if (Thickness != item.Thickness)
                    {
                        Thickness = null;
                    }
                    if (Visibile != (item.Visibility == Visibility.Visible))
                    {
                        Visibile = null;
                    }
                    if (Opacity != item.Opacity)
                    {
                        Opacity = null;
                    }
                    if (HyperLink != item.HyperLink)
                    {
                        HyperLink = string.Empty;
                    }
                }
                if (node != null)
                {
                    if ((Fill != null) &&
                        (Fill as SolidColorBrush).Color !=
                        (node.Fill as SolidColorBrush).Color)
                    {
                        Fill = null;
                    }
                    if (AllowDrag != node.AllowDrag)
                    {
                        AllowDrag = null;
                    }
                    if (this.AllowResize != node.AllowResize)
                    {
                        AllowResize = null;
                    }
                    if (this.AllowRotate != node.AllowRotate)
                    {
                        AllowRotate = null;
                    }
                    if (this.X != node.OffsetX)
                    {
                        X = null;
                    }
                    if (this.Y != node.OffsetY)
                    {
                        Y = null;
                    }
                    if (this.Px != node.Pivot.X)
                    {
                        Px = null;
                    }
                    if (this.Py != node.Pivot.Y)
                    {
                        Py = null;
                    }
                    if (this.Width != node.UnitWidth)
                    {
                        Width = null;
                    }
                    if (this.Height != node.UnitHeight)
                    {
                        Height = null;
                    }
                    if (this.Angle != node.RotateAngle)
                    {
                        Angle = null;
                    }
                }
                if (con != null)
                {
                    if (SourceCap != con.SourceCap)
                    {
                        SourceCap = null;
                    }
                    if (TargetCap != con.TargetCap)
                    {
                        TargetCap = null;
                    }
                    if (Type != con.Type)
                    {
                        Type = null;
                    }
                }
            }
        }

        void ItemUnSelectedEvent(object sender, DiagramEventArgs args)
        {
            var item = args.Item as IGroupableVM;
            item.PropertyChanged += UpdateProperties;
            SelectedItems.Remove(args.Item as IGroupableVM);
            UpdateSelectedKind();
            if (SelectedItems.Count == 1)
            {
                var firstItem = SelectedItems[0];
                var firstNode = SelectedItems[0] as INodeVM;
                var firstCon = SelectedItems[0] as IConnectorVM;
                if (firstItem != null)
                {
                    this.Bold = firstItem.Bold;
                    this.DashDot = firstItem.DashDot;
                    this.Font = firstItem.Font;
                    this.FontSize = firstItem.FontSize;
                    this.FontStyle = firstItem.FontStyle;
                    this.FontWeight = firstItem.FontWeight;
                    this.Italic = firstItem.Italic;
                    this.Label = firstItem.Label;
                    this.LabelHAlign = firstItem.LabelHAlign;
                    this.LabelMargin = firstItem.LabelMargin;
                    this.LabelVAlign = firstItem.LabelVAlign;
                    this.LabelForeground = firstItem.LabelForeground;
                    this.LabelBackground = firstItem.LabelBackground;
                    this.Name = firstItem.Name;
                    this.Stroke = firstItem.Stroke;
                    this.Style = firstItem.Style;
                    this.Thickness = firstItem.Thickness;
                    this.Visibile = firstItem.Visibility == Visibility.Visible;
                    this.Opacity = firstItem.Opacity;
                }
                if (firstNode != null)
                {
                    this.X = firstNode.OffsetX;
                    this.Y = firstNode.OffsetY;
                    this.Px = firstNode.Pivot.X;
                    this.Py = firstNode.Pivot.Y;
                    this.Width = firstNode.UnitWidth;
                    this.Height = firstNode.UnitHeight;
                    this.Angle = firstNode.RotateAngle;
                    this.AllowDrag = firstNode.AllowDrag;
                    this.AllowResize = firstNode.AllowResize;
                    this.AllowRotate = firstNode.AllowRotate;
                    this.Fill = firstNode.Fill;                   
                }
                else
                {
                    this.X = null;
                    this.Y = null;
                    this.Px = null;
                    this.Py = null;
                    this.Width = null;
                    this.Height = null;
                    this.AllowDrag = null;
                    this.AllowResize = null;
                    this.AllowRotate = null;
                    this.Fill = null;
                }
                if (firstCon != null)
                {
                    this.SourceCap = firstCon.SourceCap;
                    this.TargetCap = firstCon.TargetCap;
                    this.Type = firstCon.Type;
                }
                else
                {
                    SourceCap = null;
                    TargetCap = null;
                    Type = null;
                }
            }
            if (SelectedItems.OfType<INodeVM>().Count() == 1)
            {
                var node = SelectedItems.OfType<INodeVM>().FirstOrDefault();
                this.X = node.OffsetX;
                this.Y = node.OffsetY;
                this.Px = node.Pivot.X;
                this.Py = node.Pivot.Y;
                this.Width = node.UnitWidth;
                this.Height = node.UnitHeight;
                this.Angle = node.RotateAngle;
                this.AllowDrag = node.AllowDrag;
                this.AllowResize = node.AllowResize;
                this.AllowRotate = node.AllowRotate;
                this.Fill = node.Fill;
            }

            if (SelectedItems.OfType<IConnectorVM>().Count() == 1)
            {
                var con = SelectedItems.OfType<IConnectorVM>().FirstOrDefault();
                this.SourceCap = con.SourceCap;
                this.TargetCap = con.TargetCap;
                this.Type = con.Type;
            }
            if (SelectedItems.Count > 1)
            {
                var firstItem = SelectedItems[0];
                if (firstItem != null)
                {
                    if (SelectedItems.Any(i => i.Bold != firstItem.Bold))
                    {
                        Bold = null;
                    }
                    else
                    {
                        Bold = firstItem.Bold;
                    }

                    if (SelectedItems.Any(i => i.DashDot != firstItem.DashDot))
                    {
                        DashDot = null;
                    }
                    else
                    {
                        DashDot = firstItem.DashDot;
                    }

                    if (SelectedItems.Any(i => i.Font != firstItem.Font))
                    {
                        Font = null;
                    }
                    else
                    {
                        Font = firstItem.Font;
                    }

                    if (SelectedItems.Any(i => i.FontSize != firstItem.FontSize))
                    {
                        FontSize = null;
                    }
                    else
                    {
                        FontSize = firstItem.FontSize;
                    }


                    if (SelectedItems.Any(i => i.FontStyle != firstItem.FontStyle))
                    {
                        FontStyle = null;
                    }
                    else
                    {
                        FontStyle = firstItem.FontStyle;
                    }

                    if (SelectedItems.Any(i => i.FontWeight.Weight != firstItem.FontWeight.Weight))
                    {
                        FontWeight = null;
                    }
                    else
                    {
                        FontWeight = firstItem.FontWeight;
                    }


                    if (SelectedItems.Any(i => i.LabelBackground != firstItem.LabelBackground))
                    {
                        LabelBackground = null;
                    }
                    else
                    {
                        LabelBackground = firstItem.LabelBackground;
                    }


                    if (SelectedItems.Any(i => i.LabelForeground != firstItem.LabelForeground))
                    {
                        LabelForeground = null;
                    }
                    else
                    {
                        LabelForeground = firstItem.LabelForeground;
                    }

                    if (SelectedItems.Any(i => i.Italic != firstItem.Italic))
                    {
                        Italic = null;
                    }
                    else
                    {
                        Italic = firstItem.Italic;
                    }

                    if (SelectedItems.Any(i => i.Label != firstItem.Label))
                    {
                        Label = null;
                    }
                    else
                    {
                        Label = firstItem.Label;
                    }


                    if (SelectedItems.Any(i => i.LabelHAlign != firstItem.LabelHAlign))
                    {
                        LabelHAlign = null;
                    }
                    else
                    {
                        LabelHAlign = firstItem.LabelHAlign;
                    }

                    if (SelectedItems.Any(i => i.LabelMargin != firstItem.LabelMargin))
                    {
                        LabelMargin = null;
                    }
                    else
                    {
                        LabelMargin = firstItem.LabelMargin;
                    }

                    if (SelectedItems.Any(i => i.LabelVAlign != firstItem.LabelVAlign))
                    {
                        LabelVAlign = null;
                    }
                    else
                    {
                        LabelVAlign = firstItem.LabelVAlign;
                    }

                    if (SelectedItems.Any(i => i.Name != firstItem.Name))
                    {
                        Name = string.Empty;
                    }
                    else
                    {
                        Name = firstItem.Name;
                    }

                    if (SelectedItems.Any(i => i.Stroke != firstItem.Stroke))
                    {
                        Stroke = null;
                    }
                    else
                    {
                        Stroke = firstItem.Stroke;
                    }

                    if (SelectedItems.Any(i => i.Style != firstItem.Style))
                    {
                        Style = null;
                    }
                    else
                    {
                        Style = firstItem.Style;
                    }

                    if (SelectedItems.Any(i => i.Thickness != firstItem.Thickness))
                    {
                        Thickness = null;
                    }
                    else
                    {
                        Thickness = firstItem.Thickness;
                    }

                    if (SelectedItems.Any(i => i.Visibility != firstItem.Visibility))
                    {
                        Visibile = null;
                    }
                    else
                    {
                        Visibile = firstItem.Visibility == Visibility.Visible;
                    }
                    if (SelectedItems.Any(i => i.Opacity != firstItem.Opacity))
                    {
                        Opacity = null;
                    }
                    else
                    {
                        Opacity = firstItem.Opacity;
                    }
                }
                var firstNode = SelectedItems.OfType<INodeVM>().FirstOrDefault();
                if (firstNode != null)
                {
                    if (SelectedItems.OfType<INodeVM>().Any(i => i.OffsetX != firstNode.OffsetX))
                    {
                        X = null;
                    }
                    else
                    {
                        X = firstNode.OffsetX;
                    }

                    if (SelectedItems.OfType<INodeVM>().Any(i => i.OffsetY != firstNode.OffsetY))
                    {
                        Y = null;
                    }
                    else
                    {
                        Y = firstNode.OffsetY;
                    }

                    if (SelectedItems.OfType<INodeVM>().Any(i => i.Pivot.X != firstNode.Pivot.X))
                    {
                        Px = null;
                    }
                    else
                    {
                        Px = firstNode.Pivot.X;
                    }

                    if (SelectedItems.OfType<INodeVM>().Any(i => i.Pivot.Y != firstNode.Pivot.Y))
                    {
                        Py = null;
                    }
                    else
                    {
                        Py = firstNode.Pivot.Y;
                    }
                    if (SelectedItems.OfType<INodeVM>().Any(i => i.UnitWidth != firstNode.UnitWidth))
                    {
                        Width = null;
                    }
                    else
                    {
                        Width = firstNode.UnitWidth;
                    }

                    if (SelectedItems.OfType<INodeVM>().Any(i => i.UnitHeight != firstNode.UnitHeight))
                    {
                        Height = null;
                    }
                    else
                    {
                        Height = firstNode.UnitHeight;
                    }

                    if (SelectedItems.OfType<INodeVM>().Any(i => i.RotateAngle != firstNode.RotateAngle))
                    {
                        Angle = null;
                    }
                    else
                    {
                        Angle = firstNode.RotateAngle;
                    }

                    if (SelectedItems.OfType<INodeVM>().Any(i => i.AllowDrag != firstNode.AllowDrag))
                    {
                        AllowDrag = null;
                    }
                    else
                    {
                        AllowDrag = firstNode.AllowDrag;
                    }

                    if (SelectedItems.OfType<INodeVM>().Any(i => i.AllowResize != firstNode.AllowResize))
                    {
                        AllowResize = null;
                    }
                    else
                    {
                        AllowResize = firstNode.AllowResize;
                    }

                    if (SelectedItems.OfType<INodeVM>().Any(i => i.AllowRotate != firstNode.AllowRotate))
                    {
                        AllowRotate = null;
                    }
                    else
                    {
                        AllowRotate = firstNode.AllowRotate;
                    }
                    if (SelectedItems.OfType<INodeVM>().Any(i => i.Fill != firstNode.Fill))
                    {
                        Fill = null;
                    }
                    else
                    {
                        Fill = firstNode.Fill;
                    }
                }
                var firstCon = SelectedItems.OfType<IConnectorVM>().FirstOrDefault();
                if (firstCon != null)
                {
                    if (SelectedItems.OfType<IConnectorVM>().Any(i => i.SourceCap != firstCon.SourceCap))
                    {
                        SourceCap = null;
                    }
                    else
                    {
                        SourceCap = firstCon.SourceCap;
                    }

                    if (SelectedItems.OfType<IConnectorVM>().Any(i => i.TargetCap != firstCon.TargetCap))
                    {
                        TargetCap = null;
                    }
                    else
                    {
                        TargetCap = firstCon.TargetCap;
                    }

                    if (SelectedItems.OfType<IConnectorVM>().Any(i => i.Type != firstCon.Type))
                    {
                        Type = null;
                    }
                    else
                    {
                        Type = firstCon.Type;
                    }
                }
            }
            if(SelectedItems.Count ==0)
            {
                this.Bold = false;
                this.Italic = false;
                this.Fill = null;
                this.Stroke = null;
                this.Thickness = null;
                this.Opacity = null;
                this.FontSize = null;
                this.SliderValue = 0;
                this.SelectedIndex = null;
                this.PropertyEditorUIVisibility = null;
                this.IsNodeLocked = false;
                this.AspectRatioEnabled = false;
                this.PropertiesList = null;
                this.DataContext = null;
                this.OpenEditPanel = false;
                this.OpenTextPanel = false;
                this.OpenSettingPanel = false;
                this.OpenLinkPanel = false;                
                //foreach(var i in Links)
                //{
                //    i.SelectedLinkIndex = 0;
                //}
                this.Links = null;
                this.LabelHAlign = null;
                //if (this.SelectedIndex != null)
                //    this.SelectedIndex = null;
                //this.TabItem = null;
                //this.TabItem.Clear();
            }
        }

        private void UpdateSelectedKind()
        {
            if (SelectedItems.OfType<INodeVM>().Any())
            {
                IsNodeSelected = true;

                if (SelectedItems.OfType<INodeVM>().Count() > 1)
                    IsMultiNodeSelected = true;

                if (SelectedItems.OfType<IGroup>().Any())
                    IsGroupSelected = true;
            }
            else
            {
                IsNodeSelected = false;
                IsMultiNodeSelected = false;
                IsGroupSelected = false;
            }
            if (SelectedItems.OfType<IConnectorVM>().Any())
            {
                IsConnectorSelected = true;
            }
            else
            {
                IsConnectorSelected = false;
            }
            if (IsNodeSelected || IsConnectorSelected)
            {
                IsAnythingSelected = true;
            }
            else
            {
                IsAnythingSelected = false;
            }

            if (IsAnythingSelected && SelectedItems.Any(item => !string.IsNullOrEmpty(item.Label)))
            {
                IsLabelSet = true;
            }
            else
            {
                IsLabelSet = false;
            }
            if (IsNodeSelected && SelectedItems.OfType<INodeVM>().Any(item => !string.IsNullOrEmpty((string)item.Label)))
            {
                IsNodeLabelSet = true;
            }
            else
            {
                IsNodeLabelSet = false;
            }
            if (IsConnectorSelected && SelectedItems.OfType<IConnectorVM>().Any(item => !string.IsNullOrEmpty((string)item.Label)))
            {
                IsConnectorLabelSet = true;
            }
            else
            {
                IsConnectorLabelSet = false;
            }
        }

        #region Dimension
        double? _mPx = 0d;
        public double? Px
        {
            get
            {
                return _mPx;
            }
            set
            {
                if (_mPx != value)
                {
                    _mPx = value;
                    OnPropertyChanged(SelectorConstants.Px);
                }
            }
        }

        double? _mPy = 0d;
        public double? Py
        {
            get
            {
                return _mPy;
            }
            set
            {
                if (_mPy != value)
                {
                    _mPy = value;
                    OnPropertyChanged(SelectorConstants.Py);
                }
            }
        }

        double? _mX = 0d;
        public double? X
        {
            get
            {
                return _mX;
            }
            set
            {
                if (_mX != value)
                {
                    _mX = value;
                    OnPropertyChanged(SelectorConstants.X);
                }
            }
        }

        

        double? _mY = 0d;
        public double? Y
        {
            get
            {
                return _mY;
            }
            set
            {
                if (_mY != value)
                {
                    _mY = value;
                    OnPropertyChanged(SelectorConstants.Y);
                }
            }
        }

        double? _mAngle = 0d;
        public double? Angle
        {
            get
            {
                return _mAngle;
            }
            set
            {
                if (_mAngle != value)
                {
                    _mAngle = value;
                    OnPropertyChanged(SelectorConstants.Angle);
                }
            }
        }

        double? _mWidth = double.NaN;
        public double? Width
        {
            get
            {
                return _mWidth;
            }
            set
            {
                if (_mWidth != value &&
                    !(_mWidth.HasValue && Double.IsNaN(_mWidth.Value) &&
                      value.HasValue && double.IsNaN(value.Value)))
                {
                    _mWidth = value;
                    OnPropertyChanged(SelectorConstants.Width);
                }
                //foreach (var node in SelectedItems.OfType<INodeVM>())
                //{
                //    SelectorToNode(node);
                //}
            }
        }

        double? _mHeight = double.NaN;
        public double? Height
        {
            get
            {
                return _mHeight;
            }
            set
            {
                if (_mHeight != value &&
                    !(_mHeight.HasValue && Double.IsNaN(_mHeight.Value) &&
                      value.HasValue && double.IsNaN(value.Value)))
                {
                    _mHeight = value;
                    OnPropertyChanged(SelectorConstants.Height);
                }
            }
        }

        private void OnXChanged()
        {
            if (X.HasValue)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    node.OffsetX = X.Value;
                }

                if (this.Width != null && !double.IsNaN(this.Width.Value) && X != null)
                {
                    // Check whether the Property Panel position is outside of the ViewPort if so place the panle to inside of the view port
                    if ((X.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) + (this.Width * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) / 2 + 300 > (this.Diagram.ScrollSettings.ScrollInfo.HorizontalOffset) + (this.Diagram.ScrollSettings.ScrollInfo.ViewportWidth ))
                    {
                        this.PropertyPanelOffsetX = (X.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) - ((this.Width.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) / 2) - (this.Diagram.ScrollSettings.ScrollInfo.HorizontalOffset) - 300;
                        this.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                    }
                    else
                    {
                        this.PropertyPanelOffsetX = (X.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) + (this.Width.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) / 2 - (this.Diagram.ScrollSettings.ScrollInfo.HorizontalOffset) + 100;
                        this.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
                    }
                }                
            }
            else if (X == null)
            {
                var offx = from INodeVM nod in this.SelectedItems select nod.OffsetX * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom - nod.UnitWidth / 2 * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom;
                var offxx = from INodeVM nod in this.SelectedItems select nod.OffsetX * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom + nod.UnitWidth / 2 * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom;
                double d1 = offx.ToList<double>().Min();
                double d2 = offxx.ToList<double>().Max();
                double d = (d2 - d1) / 2;
                if (d2 + 300 > this.Diagram.ScrollSettings.ScrollInfo.HorizontalOffset + this.Diagram.ScrollSettings.ScrollInfo.ViewportWidth)
                {
                    this.PropertyPanelOffsetX = d1 - this.Diagram.ScrollSettings.ScrollInfo.HorizontalOffset - 300;
                    this.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                }
                else
                {
                    this.PropertyPanelOffsetX = d2 + this.Diagram.ScrollSettings.ScrollInfo.HorizontalOffset + 100;
                    this.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
                }
            }
        }

        private void OnYChanged()
        {
            if (Y.HasValue)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    node.OffsetY = Y.Value;
                }

                double y = 0;
                if (this.PropertiesList != null)
                {
                    if ((this.PropertiesList as PropertiesList).AlignmentTab)
                        y += 1;
                    if ((this.PropertiesList as PropertiesList).EditTab != null)
                        y += 1;
                    if ((this.PropertiesList as PropertiesList).LinkTab)
                        y += 1;
                    if ((this.PropertiesList as PropertiesList).SettingTab != null)
                        y += 1;


                }                

                if (this.Height != null && !double.IsNaN(this.Height.Value) && Y != null)
                {
                    if ((Y.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) - (this.Height * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) / 2 + (y * _mSelectorTabHeight) > this.Diagram.ScrollSettings.ScrollInfo.VerticalOffset + this.Diagram.ScrollSettings.ScrollInfo.ViewportHeight)
                    {
                        double? diff = ((Y.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) - (this.Height * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) / 2 + (y * _mSelectorTabHeight)) - (this.Diagram.ScrollSettings.ScrollInfo.VerticalOffset + this.Diagram.ScrollSettings.ScrollInfo.ViewportHeight);
                        Margin = new Thickness(Margin.Left, -diff.Value, Margin.Right, Margin.Bottom);
                    }
                    else
                    {
                        Margin = new Thickness(Margin.Left, 0, Margin.Right, Margin.Bottom);
                    }

                    if ((Y.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) - (this.Height.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) / 2 + PropertyPanelHeight > this.Diagram.ScrollSettings.ScrollInfo.VerticalOffset + this.Diagram.ScrollSettings.ScrollInfo.ViewportHeight)
                    {
                        double diff = ((Y.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) - (this.Height.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) / 2 + PropertyPanelHeight) - (this.Diagram.ScrollSettings.ScrollInfo.VerticalOffset + this.Diagram.ScrollSettings.ScrollInfo.ViewportHeight);
                        this.PropertyPanelOffsetY = (Y.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) - ((this.Height.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) / 2) - this.Diagram.ScrollSettings.ScrollInfo.VerticalOffset - diff;
                    }
                    else
                    {
                        this.PropertyPanelOffsetY = (Y.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) - ((this.Height.Value * this.Diagram.ScrollSettings.ScrollInfo.CurrentZoom) / 2) - this.Diagram.ScrollSettings.ScrollInfo.VerticalOffset;
                    }
                }
                else if (Y == null)
                {
                    var offy = from INodeVM nod in this.SelectedItems select nod.OffsetY - nod.UnitHeight / 2;
                    var offyy = from INodeVM nod in this.SelectedItems select nod.OffsetY + nod.UnitHeight / 2;
                    double d1 = offy.ToList<double>().Min();
                    double d2 = offyy.ToList<double>().Max();
                    double d = (d2 - d1) / 2;


                    if (d1 + (y * _mSelectorTabHeight) > this.Diagram.ScrollSettings.ScrollInfo.VerticalOffset + this.Diagram.ScrollSettings.ScrollInfo.ViewportHeight)
                    {
                        double? diff = (Y.Value - this.Height / 2 + (y * _mSelectorTabHeight)) - (this.Diagram.ScrollSettings.ScrollInfo.VerticalOffset + this.Diagram.ScrollSettings.ScrollInfo.ViewportHeight);
                        Margin = new Thickness(Margin.Left, -diff.Value, Margin.Right, Margin.Bottom);
                    }
                    else
                    {
                        Margin = new Thickness(Margin.Left, 0, Margin.Right, Margin.Bottom);
                    }

                    if (d > this.Diagram.ScrollSettings.ScrollInfo.VerticalOffset + this.Diagram.ScrollSettings.ScrollInfo.ViewportHeight)
                    {
                        this.PropertyPanelOffsetY = d2 - this.Diagram.ScrollSettings.ScrollInfo.VerticalOffset;
                    }
                    else
                    {
                        this.PropertyPanelOffsetY = d2 - this.Diagram.ScrollSettings.ScrollInfo.VerticalOffset;
                    }
                }

                if ((Diagram.PageSettings as PageVM).Ruler)
                {
                    if (this.PropertyPanelOffsetY < _mRulerHeight)
                        this.PropertyPanelOffsetY = _mRulerHeight;
                }
                else
                {
                    if (this.PropertyPanelOffsetY < 0)
                        this.PropertyPanelOffsetY = 0;
                }
                
                
            }
        }

        private void OnPYChanged()
        {
            if (Py.HasValue)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    node.Pivot = new Point(node.Pivot.X, Py.Value);
                }
            }
        }

        private void OnPXChanged()
        {
            if (Px.HasValue)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    node.Pivot = new Point(Px.Value, node.Pivot.Y);
                }
            }
        }

        private void OnAngleChanged()
        {
            if (Angle.HasValue)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    node.RotateAngle = Angle.Value;
                }
            }
        }

        private void OnWidthChanged()
        {
            if (Width.HasValue)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {                    
                    node.UnitWidth = Width.Value;
                    SliderValueAdjust(node);
                }
            }
        }

        private void SelectorToNode(INodeVM node)
        {
            if ((node as NodeVM).Shape.Equals("dialogbox") 
                || (node as NodeVM).Shape.Equals("windowbox") 
                || (node as NodeVM).Shape.Equals("alertbox")  
                || (node as NodeVM).Shape.Equals("multilinebutton")
                || (node as NodeVM).Shape.Equals("accordion")
                )
            {
                foreach (LabelVM label in (node as NodeVM).Annotations as List<Syncfusion.UI.Xaml.Diagram.Controls.IAnnotation>)
                {
                        label.LabelWidth = node.UnitWidth;
                    if (!(node as NodeVM).Shape.Equals("windowbox") )
                        label.LabelHeight = node.UnitHeight;
                }
            }
        }

        private void OnHeightChanged()
        {
            if (Height.HasValue)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    node.UnitHeight = Height.Value;
                    SliderValueAdjust(node);
                }
            }
        }

        #endregion

        #region Shape

        #region Prop

        private string _mToolTipConstraint = "Default";

        public string ToolTipConstraint
        {
            get
            {
                return _mToolTipConstraint;
            }
            set
            {
                if (_mToolTipConstraint != value)
                {
                    _mToolTipConstraint = value;
                    OnPropertyChanged(SelectorConstants.ToolTipConstraint);
                }
            }
        }

        Brush _mFill = null;
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
                    OnPropertyChanged(NodeConstants.Fill);
                }
            }
        }

        Brush _mStroke = null;
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

        Brush _mListOdd = null;
        public Brush ListOdd
        {
            get
            {
                return _mListOdd;
            }
            set
            {
                if (_mListOdd != value)
                {
                    _mListOdd = value;
                    OnPropertyChanged("ListOdd");
                }
            }
        }

        Brush _mListEven = null;
        public Brush ListEven 
        {
            get
            {
                return _mListEven;
            }
            set
            {
                if (_mListEven != value)
                {
                    _mListEven = value;
                    OnPropertyChanged("ListEven");
                }
            }
        }

        double? _mThickness = 0d;
        public double? Thickness
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

        private DoubleCollection _mStrokeDashArray = new DoubleCollection();
        public DoubleCollection StrokeDashArray
        {
            get 
            { 
                return _mStrokeDashArray; 
            }
            set
            {
                if (_mStrokeDashArray != value)
                {
                    _mStrokeDashArray = value;
                    OnPropertyChanged("StrokeDashArray");
                }
            }
        }

        string _mDashDot = null;
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

        double? _mOpacity = null;
        public double? Opacity
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

        private void OnFillChanged()
        {
            if (Fill != null)
            { 
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    node.Fill = Fill;
                    if (node.Shape != null)
                    {
                        if (node.Shape.Equals("button"))
                        {
                            (node.DataContext as ButtonBusinessObject).Fill = Fill;
                        }
                        else if (node.Shape.Equals("tabsbar") || node.Shape.Equals("verticaltab"))
                        {
                            (node.DataContext as TabBusinessClass).Background = Fill;
                        }
                        else if (node.Shape.Equals("datechooser"))
                        {
                            (node.DataContext as DateChoosetBusinessClass).Fill = Fill;
                        }
                        else if ((node as INodeVM).Shape.Equals("messagebox"))
                        {
                            ((node as INodeVM).DataContext as MessageBoxBusinessObject).Fill = Fill;
                        }
                        else if ((node as INodeVM).Shape.Equals("onoffswitch"))
                        {
                            ((node as INodeVM).DataContext as OnOffSwitch).Fill = Fill;
                        }
                    }
                }
            }
        }

        private void OnListOddChanged()
        {
            if (ListOdd != null)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    (node.DataContext as ListBusinessClass).OddItemsBackgroud = ListOdd;
                }
            }
        }

        private void OnListEvenChanged()
        {
            if (ListEven != null)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    (node.DataContext as ListBusinessClass).EvenItemsBackgroud = ListEven;
                }
            }
        }

        private void OnSelectedIndexChanged()
        {
            if (SelectedIndex != null && SelectedIndex >= 0 && SelectedItems.Count > 0)
            {
                string firstShape = (SelectedItems.OfType<INodeVM>().First() as INodeVM).Shape.ToString();
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    //if (SelectedItems.OfType<INodeVM>().Count() == 1)
                    {
                        #region buttonbar
                        if (node.Shape.Equals("buttonbar"))
                        {
                            (node.DataContext as ButtonBarBusinessClass).SelectedIndex = SelectedIndex.Value;
                        }
                        #endregion
                        #region tabs
                        else if (node.Shape.Equals("tabsbar") || node.Shape.Equals("verticaltab"))
                        {
                            (node.DataContext as TabBusinessClass).SelectedIndex = SelectedIndex.Value;
                        }
                        #endregion
                        #region linkbar
                        else if (node.Shape.Equals("linkbar"))
                        {
                            (node.DataContext as LinkBarBusinessClass).SelectedIndex = SelectedIndex.Value;
                        }
                        #endregion 
                        #region list
                        else if (node.Shape.Equals("list"))
                        {
                            ((node as INodeVM).DataContext as ListBusinessClass).SelectedIndex = SelectedIndex.Value - 1;
                        }
                        #endregion
                        #region menubar
                        else if (node.Shape.Equals("menubar"))
                        {
                            (node.DataContext as MenuBarBusinessClass).SelectedIndex = SelectedIndex.Value;
                        }
                        #endregion
                        #region menu
                        else if (node.Shape.Equals("menu"))
                        {
                            (node.DataContext as MenuBusinessClass).SelectedIndex = SelectedIndex.Value;
                        }
                        #endregion 
                        #region accordion
                        else if (node.Shape.Equals("accordion"))
                        {
                            if(this.SelectedItem is AccordionItemBusinessObject)
                            {
                                (node.DataContext as AccordionBusinessObject).SelectedIndex = (this.SelectedItem as AccordionItemBusinessObject).Index;
                                this.SliderValue = ((node.DataContext as AccordionBusinessObject).ScrollBar.SliderValue / ((this.SelectedItem as AccordionItemBusinessObject).Height - ((node.DataContext as AccordionBusinessObject).ScrollBar.TopButtonSize + (node.DataContext as AccordionBusinessObject).ScrollBar.BottomButtonSize + (node.DataContext as AccordionBusinessObject).ScrollBar.ScrollThumbHeight))) * 100;
                            }
                            else if (this.SelectedItem is AccordionSubItemBusinessObject)
                            {
                                (node.DataContext as AccordionBusinessObject).SelectedIndex = (this.SelectedItem as AccordionSubItemBusinessObject).ParentIndex;
                                foreach (var item in ((node.DataContext as AccordionBusinessObject).Items[(node.DataContext as AccordionBusinessObject).SelectedIndex - 1] as AccordionItemBusinessObject).Items)
                                {
                                    (item as AccordionSubItemBusinessObject).Fill = new SolidColorBrush(Colors.Transparent);
                                    (item as AccordionSubItemBusinessObject).IsSelected = false;
                                }
                                (this.SelectedItem as AccordionSubItemBusinessObject).Fill = GetColorFromHexa("#83c1f0");
                                (this.SelectedItem as AccordionSubItemBusinessObject).IsSelected = true;
                            }

                            if(SelectedIndex.Value == 0)
                            {
                                foreach (LabelVM _mAnnotation in node.Annotations as List<IAnnotation>)
                                {
                                    //Size size = FindDummyTextSize(_mAnnotation, ((node.DataContext as AccordionBusinessObject).Items[0] as AccordionItemBusinessObject).Item);
                                    double height = (node.DataContext as AccordionBusinessObject).Items.Count() * (node.DataContext as AccordionBusinessObject).ItemTitleHeight;
                                    node.UnitHeight = height;
                                }                                
                            }
                            else
                            {
                                foreach (LabelVM _mAnnotation in node.Annotations as List<IAnnotation>)
                                {
                                    //Size size = FindDummyTextSize(_mAnnotation, ((node.DataContext as AccordionBusinessObject).Items[0] as AccordionItemBusinessObject).Item);
                                    double height = ((node.DataContext as AccordionBusinessObject).Items.Count() * (node.DataContext as AccordionBusinessObject).ItemTitleHeight) + ((node.DataContext as AccordionBusinessObject).Items[(node.DataContext as AccordionBusinessObject).SelectedIndex - 1] as AccordionItemBusinessObject).BottomSpace;
                                    double subItemsHeight = 0d;
                                    if (((node.DataContext as AccordionBusinessObject).Items[(node.DataContext as AccordionBusinessObject).SelectedIndex - 1] as AccordionItemBusinessObject).Items != null)
                                    {
                                        subItemsHeight = ((node.DataContext as AccordionBusinessObject).Items[(node.DataContext as AccordionBusinessObject).SelectedIndex - 1] as AccordionItemBusinessObject).Items.Count() * (node.DataContext as AccordionBusinessObject).ItemTitleHeight;
                                    }
                                    height += subItemsHeight;
                                    node.UnitHeight = height;
                                }
                            }
                        }
                        #endregion 
                    }
                    //else if (SelectedItems.OfType<INodeVM>().Count() > 1)
                    //{
                    //    #region menubar
                    //    if (node.Shape.Equals("menubar"))
                    //    {
                    //        (node.DataContext as MenuBarBusinessClass).SelectedIndex = SelectedIndex.Value;
                    //    }
                    //    #endregion
                    //}
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

        private void OnPropertiesListChanged()
        {
            if (this.PropertiesList != null)
            {
                if (!(this.PropertiesList as PropertiesList).AlignmentTab)
                    OpenEditPanel = null;
                else
                    OpenEditPanel = false;

                if ((this.PropertiesList as PropertiesList).EditTab == null)
                    OpenTextPanel = null;
                else
                    OpenTextPanel = false;

                if ((this.PropertiesList as PropertiesList).SettingTab == null)
                    OpenSettingPanel = null;
                else
                    OpenSettingPanel = false;

                if (!(this.PropertiesList as PropertiesList).LinkTab)
                    OpenLinkPanel = null;
                else
                    OpenLinkPanel = false;
            }
        }

        private void OnSelectedItemChanged()
        {
            if (SelectedItem != null)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    #region checkbox / radiobutton
                    if (node.Shape.Equals("checkbox") || node.Shape.Equals("radiobutton"))
                    {
                        if (SelectedItem.Equals("Normal"))
                            (node.DataContext as CheckBoxBusinessClass).SelectedIndex = 0;
                        else if(SelectedItem.Equals("Selected"))
                            (node.DataContext as CheckBoxBusinessClass).SelectedIndex = 1;
                        else if (SelectedItem.Equals("Disabled"))
                            (node.DataContext as CheckBoxBusinessClass).SelectedIndex  = 2;
                        else if (SelectedItem.Equals("Disabled And Selected"))
                            (node.DataContext as CheckBoxBusinessClass).SelectedIndex = 3;
                    }
                    #endregion  
                    #region datechooser
                    else if (node.Shape.Equals("datechooser"))
                    {
                        if (SelectedItem.Equals("Normal"))
                            (node.DataContext as DateChoosetBusinessClass).SelectedIndex = 0;
                        else if (SelectedItem.Equals("InFocus"))
                            (node.DataContext as DateChoosetBusinessClass).SelectedIndex = 1;
                        else if (SelectedItem.Equals("Disabled"))
                            (node.DataContext as DateChoosetBusinessClass).SelectedIndex = 2;
                    }
                    #endregion
                    #region textarea / textinput
                    else if (node.Shape.Equals("textarea") || node.Shape.Equals("textinput"))
                    {
                        if (SelectedItem.Equals("Normal"))
                            (node.DataContext as TextAreaBusinessClass).SelectedIndex = 0;
                        else if (SelectedItem.Equals("InFocus"))
                            (node.DataContext as TextAreaBusinessClass).SelectedIndex = 1;
                        else if (SelectedItem.Equals("Disabled"))
                            (node.DataContext as TextAreaBusinessClass).SelectedIndex = 2;
                        //(node.DataContext as TextAreaBusinessClass).SelectedIndex = SelectedIndex.Value;
                    }
                    #endregion
                    #region link
                    else if (node.Shape.Equals("link"))
                    {
                        if (SelectedItem.Equals("Normal"))
                        {
                            (node.DataContext as LinkBusinessClass).SelectedIndex = 0;
                            node.LabelForeground = new SolidColorBrush(Colors.Blue);
                        }
                        else if (SelectedItem.Equals("Disabled"))
                        {
                            (node.DataContext as LinkBusinessClass).SelectedIndex = 1;
                            node.LabelForeground = GetColorFromHexa("#AAAAAA");
                        }
                    }
                    #endregion
                    #region combobox
                    else if (node.Shape.Equals("combobox"))
                    {
                        if (SelectedItem.Equals("Normal"))
                            (node.DataContext as ComboBoxBusinessClass).SelectedIndex = 0;
                        else if (SelectedItem.Equals("Disabled"))
                            (node.DataContext as ComboBoxBusinessClass).SelectedIndex = 1;
                        else if (SelectedItem.Equals("Closed"))
                            (node.DataContext as ComboBoxBusinessClass).SelectedIndex = 2;
                        else if (SelectedItem.Equals("Closed and Disabled"))
                            (node.DataContext as ComboBoxBusinessClass).SelectedIndex = 3;
                    }
                    #endregion
                    #region button
                    else if (node.Shape.Equals("button"))
                    {
                        if (SelectedItem.Equals("Normal"))
                        {
                            (node.DataContext as ButtonBusinessObject).SelectedIndex = 0;
                            Stroke = new SolidColorBrush(Colors.Black);
                        }
                        else if (SelectedItem.Equals("Selected"))
                        {
                            (node.DataContext as ButtonBusinessObject).SelectedIndex = 1;
                            Stroke = new SolidColorBrush(Colors.Black);
                        }
                        else if (SelectedItem.Equals("InFocus"))
                        {
                            (node.DataContext as ButtonBusinessObject).SelectedIndex = 2;
                            Stroke = new SolidColorBrush(Colors.Blue);
                        }
                        else if (SelectedItem.Equals("Disabled"))
                        {
                            (node.DataContext as ButtonBusinessObject).SelectedIndex = 3;
                            Stroke = GetColorFromHexa("#AAAAAA");
                        }
                    }
                    #endregion
                    #region pointybutton
                    else if (node.Shape.Equals("pointybutton"))
                    {
                        if (SelectedItem.Equals("Normal"))
                        {
                            node.LabelForeground = new SolidColorBrush(Colors.Black);
                            (node.DataContext as PointyButtonBusinessClass).Disabled = false;
                        }
                        else if (SelectedItem.Equals("Disabled"))
                        {
                            node.LabelForeground = GetColorFromHexa("#AAAAAA");
                            (node.DataContext as PointyButtonBusinessClass).Disabled = true;
                        }
                    }
                    #endregion
                    #region sliders
                    else if (node.Shape.Equals("verticalslider") || node.Shape.Equals("horizotalslider"))
                    {
                        if (SelectedItem.Equals("Normal"))
                            (node.DataContext as SliderBusinessClass).Selected = true;
                        else if (SelectedItem.Equals("Disabled"))
                            (node.DataContext as SliderBusinessClass).Selected = false;
                        
                    }
                    #endregion
                    #region numericstepper
                    else if (node.Shape.Equals("numericstepper"))
                    {
                        if (SelectedItem.Equals("Normal"))
                        {
                            if (!(node.DataContext as NumericStepperBusinessClass).Selected)
                            {
                                (node.DataContext as NumericStepperBusinessClass).Selected = true;
                                node.LabelForeground = (node.DataContext as NumericStepperBusinessClass).Foreground;
                            }
                        }
                        else if (SelectedItem.Equals("Disabled"))
                        {
                            if ((node.DataContext as NumericStepperBusinessClass).Selected)
                            {
                                (node.DataContext as NumericStepperBusinessClass).Selected = false;
                                node.LabelForeground = GetColorFromHexa("#AAAAAA");
                            }
                        }
                    }
                    #endregion
                    #region searchbutton
                    else if (node.Shape.Equals("searchbutton"))
                    {
                        if (SelectedItem.Equals("Normal"))
                        {
                            (node.DataContext as SearchBoxBusinessClass).SelectedIndex = 0;
                            (node.DataContext as SearchBoxBusinessClass).Disabled = false;
                            (node.DataContext as SearchBoxBusinessClass).Stroke = new SolidColorBrush(Colors.Black);
                        }
                        else if (SelectedItem.Equals("InFocus"))
                        {
                            (node.DataContext as SearchBoxBusinessClass).SelectedIndex = 1;
                            (node.DataContext as SearchBoxBusinessClass).Disabled = false;
                            (node.DataContext as SearchBoxBusinessClass).Stroke = GetColorFromHexa("#83c1f0");
                        }
                        else if (SelectedItem.Equals("Disabled"))
                        {
                            (node.DataContext as SearchBoxBusinessClass).SelectedIndex = 2;
                            (node.DataContext as SearchBoxBusinessClass).Disabled = true;
                            (node.DataContext as SearchBoxBusinessClass).Stroke = GetColorFromHexa("#AAAAAA");
                        }
                    }
                    #endregion
                    #region label
                    else if (node.Shape.Equals("label"))
                    {
                        //foreach (LabelVM label in node.Annotations as List<IAnnotation>)
                        {
                            //if ((SelectedItem as CheckBoxState).State == "Normal")
                            //{
                            //    node.LabelForeground = (node.DataContext as LabelBusinessClass).ColorOnSelectedState;
                            //    (node.DataContext as LabelBusinessClass).SelectedState = "Normal";
                            //}
                            //else if ((SelectedItem as CheckBoxState).State == "Disabled")
                            //{
                            //    (node.DataContext as LabelBusinessClass).ColorOnSelectedState = node.LabelForeground;
                            //    node.LabelForeground = new SolidColorBrush(Colors.Red);
                            //    (node.DataContext as LabelBusinessClass).SelectedState = "Disabled";
                            //}
                        }
                    }
                    #endregion                    
                    #region accordion
                    else if (node.Shape.Equals("accordion"))
                    {
                        //foreach (LabelVM _mAnnotation in (node as INodeVM).Annotations as List<IAnnotation>)
                        //{
                        //    foreach (var item in (_mAnnotation.DataContext as ObservableCollection<AccordionBusinessObject>))
                        //    {
                        //        foreach (var subitem in item.SubItemsCollection)
                        //        {
                        //            if (subitem == SelectedItem && !subitem.Item.ToString().Equals("None"))
                        //            {
                        //                subitem.Fill = new SolidColorBrush(Colors.Red);
                        //            }
                        //            else if (!subitem.Item.ToString().Equals("None"))
                        //            {
                        //                subitem.Fill = new SolidColorBrush(Colors.AliceBlue);
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    #endregion
                }
            }
        }

        private void OnStrokeChanged()
        {
            if (Stroke != null)
            {
                foreach (var node in SelectedItems.OfType<IGroupableVM>())
                {
                    node.Stroke = Stroke;

                    // Have to move this code to right place
                    if ((node as INodeVM).Shape != null)
                    {
                        if ((node as INodeVM).Shape.Equals("linkbar"))
                        {
                            foreach (LabelVM _mAnnotation in (node as INodeVM).Annotations as List<IAnnotation>)
                            {
                                (_mAnnotation.DataContext as LinkBarBusinessClass).Stroke = Stroke;
                            }
                        }
                        else if ((node as INodeVM).Shape.Equals("verticalslider") || (node as INodeVM).Shape.Equals("horizotalslider"))
                        {
                            ((node as INodeVM).DataContext as SliderBusinessClass).Stroke = Stroke;
                        }
                        else if ((node as INodeVM).Shape.Equals("datechooser"))
                        {
                            ((node as INodeVM).DataContext as DateChoosetBusinessClass).Stroke = Stroke;
                        }
                        else if ((node as INodeVM).Shape.Equals("checkboxgroup"))
                        {
                            ((node as INodeVM).DataContext as CheckBoxGroupBusinessClass).Stroke = Stroke;
                        }
                        else if ((node as INodeVM).Shape.Equals("radiobuttongroup"))
                        {
                            ((node as INodeVM).DataContext as RadioButtonGroupBusinessClass).Stroke = Stroke;
                        }
                        else if ((node as INodeVM).Shape.Equals("textinput"))
                        {
                            ((node as INodeVM).DataContext as TextAreaBusinessClass).Stroke = Stroke;
                        }
                        else if ((node as INodeVM).Shape.Equals("textarea"))
                        {
                            ((node as INodeVM).DataContext as TextAreaBusinessClass).Stroke = Stroke;
                        }
                        else if ((node as INodeVM).Shape.Equals("numericstepper"))
                        {
                            ((node as INodeVM).DataContext as NumericStepperBusinessClass).Stroke = Stroke;
                        }
                        else if ((node as INodeVM).Shape.Equals("combobox"))
                        {
                            ((node as INodeVM).DataContext as ComboBoxBusinessClass).Stroke = Stroke;
                        }
                    }
                }
            }
        }

        private void OnThicknessChanged()
        {
            if (Thickness != null)
            {
                foreach (var node in SelectedItems.OfType<IGroupableVM>())
                {
                    node.Thickness = Thickness.Value;
                }
            }
        }

        private void OnStrokeDashArrayChanged()
        {
            if(StrokeDashArray != null)
            {
                foreach (var node in SelectedItems.OfType<IGroupableVM>())
                {
                    (node as GroupableVM).StrokeDashArray = StrokeDashArray;
                }
            }
        }

        private void OnDashDotChanged()
        {
            if (DashDot != null)
            {
                foreach (var node in SelectedItems.OfType<IGroupableVM>())
                {
                    node.DashDot = DashDot;
                }
            }
        }

        private void OnStyleChanged()
        {
            if (Style != null)
            {
                foreach (var node in SelectedItems.OfType<IGroupableVM>())
                {
                    node.Style = Style;
                }
            }
        }

        private void OnOpacityChanged()
        {
            if (Opacity != null)
            {
                foreach (var node in SelectedItems.OfType<IGroupableVM>())
                {
                    node.Opacity = Opacity.Value;
                    if ((node as INodeVM).Shape != null)
                    {
                        if ((node as INodeVM).Shape.Equals("tabsbar") || (node as INodeVM).Shape.Equals("verticaltab"))
                        {
                            ((node as INodeVM).DataContext as TabBusinessClass).Opacity = Opacity.Value;
                        }
                    }
                }
            }
        }

        //private Style GetCustomStyle()
        //{
        //    Style customStyle = new Style(typeof(Path));
        //    if (Stroke != null)
        //    {
        //        customStyle.Setters.Add(new Setter(Path.StrokeProperty, Stroke));
        //    }
        //    if (Thickness.HasValue)
        //    {
        //        customStyle.Setters.Add(new Setter(Path.StrokeThicknessProperty, Thickness.Value));
        //    }
        //    if (Fill != null)
        //    {
        //        customStyle.Setters.Add(new Setter(Path.FillProperty, Fill));
        //    }
        //    if (DashDot != null)
        //    {
        //        customStyle.Setters.Add(new Setter(Path.StrokeDashArrayProperty, DashDot));
        //    }
        //    customStyle.Setters.Add(new Setter(Path.StretchProperty, Stretch.Fill));
        //    return customStyle;
        //}

        #endregion

        #region Connector
        #region Prop

        string _mSourceCap = null;
        public string SourceCap
        {
            get
            {
                return _mSourceCap;
            }
            set
            {
                if (_mSourceCap != value)
                {
                    _mSourceCap = value;
                    OnPropertyChanged(ConnectorConstants.SourceCap);
                }
            }
        }

        string _mTargetCap = null;
        public string TargetCap
        {
            get
            {
                return _mTargetCap;
            }
            set
            {
                if (_mTargetCap != value)
                {
                    _mTargetCap = value;
                    OnPropertyChanged(ConnectorConstants.TargetCap);
                }
            }
        }

        ConnectType? _mType = ConnectType.Straight;
        public ConnectType? Type
        {
            get
            {
                return _mType;
            }
            set
            {
                if (_mType != value)
                {
                    _mType = value;
                    OnPropertyChanged(ConnectorConstants.Type);
                }
            }
        }
        #endregion

        private void OnSourceCapChanged()
        {
            if (SourceCap != null)
            {
                foreach (var con in SelectedItems.OfType<IConnectorVM>())
                {
                    con.SourceCap = SourceCap;
                }
            }
        }

        private void OnTagetCapChanged()
        {
            if (TargetCap != null)
            {
                foreach (var con in SelectedItems.OfType<IConnectorVM>())
                {
                    con.TargetCap = TargetCap;
                }
            }
        }

        private void OnTypeChanged()
        {
            if (Type.HasValue)
            {
                foreach (var con in SelectedItems.OfType<IConnectorVM>())
                {
                    con.Type = Type.Value;
                }
            }
        }
        #endregion

        #region Label
        #region Prop
        string _mLabel = string.Empty;
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

        FontFamily _mFont = new FontFamily("Segoe UI");
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

        double? _mFontSize = 8;
        public double? FontSize
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

        FontWeight? _mFontWeight;
        public FontWeight? FontWeight
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

        private FontStyle? _mFontStyle = null;
        public FontStyle? FontStyle
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

        bool? _mBold = false;
        public bool? Bold
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

        bool? _mItalic = false;
        public bool? Italic
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

        private TextAlignment? _mTextAlignment = null;
        public TextAlignment? TextAlignment
        {
            get
            {
                return _mTextAlignment;
            }
            set
            {
                if (_mTextAlignment != value)
                {
                    _mTextAlignment = value;
                    OnPropertyChanged(GroupableConstants.TextAlignment);
                }
            }
        }

        public bool TextLeft
        {
            get { return _textLeft; }
            set
            {
                if (_textLeft != value)
                {
                    _textLeft = value;
                    OnPropertyChanged(GroupableConstants.TextLeft);
                }
            }
        }

        public bool TextCenter
        {
            get { return _textCenter; }
            set
            {
                if (_textCenter != value)
                {
                    _textCenter = value;
                    OnPropertyChanged(GroupableConstants.TextCenter);
                }
            }
        }

        public bool TextRight
        {
            get { return _textRight; }
            set
            {
                if (_textRight != value)
                {
                    _textRight = value;
                    OnPropertyChanged(GroupableConstants.TextRight);
                }
            }
        }

        bool _mTopLeft = false;
        public bool TopLeft
        {
            get
            {
                return _mTopLeft;
            }
            set
            {
                if (_mTopLeft != value)
                {
                    _mTopLeft = value;
                    OnPropertyChanged(GroupableConstants.TopLeft);
                }
            }
        }

        bool _mtop = false;
        public bool Top
        {
            get
            {
                return _mtop;
            }
            set
            {
                if (_mtop != value)
                {
                    _mtop = value;
                    OnPropertyChanged(GroupableConstants.Top);
                }
            }
        }

        bool _mTopRight = false;
        public bool TopRight
        {
            get
            {
                return _mTopRight;
            }
            set
            {
                if (_mTopRight != value)
                {
                    _mTopRight = value;
                    OnPropertyChanged(GroupableConstants.TopRight);
                }
            }
        }

        bool _mLeft = false;
        public bool Left
        {
            get
            {
                return _mLeft;
            }
            set
            {
                if (_mLeft != value)
                {
                    _mLeft = value;
                    OnPropertyChanged(GroupableConstants.Left);
                }
            }
        }

        bool _mCenter = false;
        public bool Center
        {
            get
            {
                return _mCenter;
            }
            set
            {
                if (_mCenter != value)
                {
                    _mCenter = value;
                    OnPropertyChanged(GroupableConstants.Center);
                }
            }
        }

        bool _mRight = false;
        public bool Right
        {
            get
            {
                return _mRight;
            }
            set
            {
                if (_mRight != value)
                {
                    _mRight = value;
                    OnPropertyChanged(GroupableConstants.Right);
                }
            }
        }

        bool _mBottomLeft = false;
        public bool BottomLeft
        {
            get
            {
                return _mBottomLeft;
            }
            set
            {
                if (_mBottomLeft != value)
                {
                    _mBottomLeft = value;
                    OnPropertyChanged(GroupableConstants.BottomLeft);
                }
            }
        }

        bool _mBottom = false;
        public bool Bottom
        {
            get
            {
                return _mBottom;
            }
            set
            {
                if (_mBottom != value)
                {
                    _mBottom = value;
                    OnPropertyChanged(GroupableConstants.Bottom);
                }
            }
        }

        bool _mBottomRight = false;
        public bool BottomRight
        {
            get
            {
                return _mBottomRight;
            }
            set
            {
                if (_mBottomRight != value)
                {
                    _mBottomRight = value;
                    OnPropertyChanged(GroupableConstants.BottomRight);
                }
            }
        }

        private HorizontalAlignment? _mLabelHAlign = null;
        public HorizontalAlignment? LabelHAlign
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

        private VerticalAlignment? _mLabelVAlign = null;
        public VerticalAlignment? LabelVAlign
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

        Thickness? _mLabelMargin = null;
        public Thickness? LabelMargin
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

        #endregion
        private void OnLabelChanged()
        {
            if (Label != null)
            {
                foreach (var item in SelectedItems)
                {
                    item.Label = Label;
                }
            }
        }

        private void OnFontChanged()
        {
            if (Font != null)
            {
                foreach (var node in SelectedItems)
                {
                    node.Font = Font; 
                }
            }
        }

        private void OnFontSizeChanged()
        {
            if (FontSize.HasValue)
            {
                foreach (var node in SelectedItems)
                {
                    node.FontSize = (int)FontSize.Value;
                    //if ((node as INodeVM).Shape !=null && (node as INodeVM).Shape.Equals("list"))
                    //{
                    //    ((node as INodeVM).DataContext as ListBusinessClass).RowHeight = FontSize.Value + 5;
                    //}
                }
            }
        }

        private Size FindDummyTextSize(LabelVM _mAnnotation, string text, TextWrapping wrap = TextWrapping.NoWrap)
        {
            if (!string.IsNullOrEmpty(text))
            {
                TextBlock dummyTextBlock = new TextBlock();
                dummyTextBlock.FontFamily = new FontFamily("Tahoma");
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

        private void OnFontWeightChanged()
        {
            if (FontWeight.HasValue)
            {
                foreach (var item in SelectedItems)
                {
                    item.FontWeight = FontWeight.Value;
                }
            }
        }

        private void OnFontStyleChanged()
        {
            if (FontStyle.HasValue)
            {
                foreach (var item in SelectedItems)
                {
                    item.FontStyle = FontStyle.Value;
                }
            }
        }

        private void OnBoldChanged()
        {
            if (Bold.HasValue)
            {
                foreach (var item in SelectedItems)
                {
                    item.Bold = Bold.Value;
                }
            }
        }

        private void OnItalicChanged()
        {
            if (Italic.HasValue)
            {
                foreach (var item in SelectedItems)
                {
                    item.Italic = Italic.Value;
                }
            }
        }

        private bool _mHoldLabelAlignment = false;

        private void OnTextAlignToggled(string name)
        {
            if (_mHoldLabelAlignment)
            {
                return;
            }
            _mHoldLabelAlignment = true;
            switch (name)
            {
                case GroupableConstants.TextLeft:
                    TextAlignment = Windows.UI.Xaml.TextAlignment.Left;
                    break;
                case GroupableConstants.TextCenter:
                    TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
                    break;
                case GroupableConstants.TextRight:
                    TextAlignment = Windows.UI.Xaml.TextAlignment.Right;
                    break;
            }
            _mHoldLabelAlignment = false;
        }

        private void OnTextAlignmentChanged()
        {
            if (_mHoldLabelAlignment)
            {
                return;
            }
            _mHoldLabelAlignment = true;
            if (TextAlignment.HasValue)
            {
                switch (TextAlignment)
                {
                    case Windows.UI.Xaml.TextAlignment.Left:
                        TextLeft = true;
                        TextCenter = false;
                        TextRight = false;
                        break;
                    case Windows.UI.Xaml.TextAlignment.Center:
                        TextLeft = false;
                        TextCenter = true;
                        TextRight = false;
                        break;
                    case Windows.UI.Xaml.TextAlignment.Right:
                        TextLeft = false;
                        TextCenter = false;
                        TextRight = true;
                        break;
                }
            }
            _mHoldLabelAlignment = false;
        }

        private void AlignToggled(string corner)
        {
            if (_mHoldLabelAlignment)
            {
                return;
            }
            _mHoldLabelAlignment = true;
            switch (corner)
            {
                case GroupableConstants.TopLeft:
                    LabelHAlign = HorizontalAlignment.Left;
                    LabelVAlign = VerticalAlignment.Top;
                    break;
                case GroupableConstants.Top:
                    LabelHAlign = HorizontalAlignment.Center;
                    LabelVAlign = VerticalAlignment.Top;
                    break;
                case GroupableConstants.TopRight:
                    LabelHAlign = HorizontalAlignment.Right;
                    LabelVAlign = VerticalAlignment.Top;
                    break;
                case GroupableConstants.Left:
                    LabelHAlign = HorizontalAlignment.Left;
                    LabelVAlign = VerticalAlignment.Center;
                    break;
                case GroupableConstants.Center:
                    LabelHAlign = HorizontalAlignment.Center;
                    LabelVAlign = VerticalAlignment.Center;
                    break;
                case GroupableConstants.Right:
                    LabelHAlign = HorizontalAlignment.Right;
                    LabelVAlign = VerticalAlignment.Center;
                    break;
                case GroupableConstants.BottomLeft:
                    LabelHAlign = HorizontalAlignment.Left;
                    LabelVAlign = VerticalAlignment.Bottom;
                    break;
                case GroupableConstants.Bottom:
                    LabelHAlign = HorizontalAlignment.Center;
                    LabelVAlign = VerticalAlignment.Bottom;
                    break;
                case GroupableConstants.BottomRight:
                    LabelHAlign = HorizontalAlignment.Right;
                    LabelVAlign = VerticalAlignment.Bottom;
                    break;
            }
            _mHoldLabelAlignment = false;
            OnLabelHAlignChanged();
        }

        private void OnLabelHAlignChanged()
        {
            if (_mHoldLabelAlignment)
            {
                return;
            }
            _mHoldLabelAlignment = true;

            if (LabelHAlign.HasValue && LabelVAlign.HasValue)
            {
                switch (LabelHAlign.Value)
                {
                    case HorizontalAlignment.Left:
                        TopLeft = true;
                        Top = false;
                        TopRight = false;
                        Left = true;
                        Center = false;
                        Right = false;
                        BottomLeft = true;
                        Bottom = false;
                        BottomRight = false;
                        break;
                    case HorizontalAlignment.Center:
                        TopLeft = false;
                        Top = true;
                        TopRight = false;
                        Left = false;
                        Center = true;
                        Right = false;
                        BottomLeft = false;
                        Bottom = true;
                        BottomRight = false;
                        break;
                    case HorizontalAlignment.Right:
                        TopLeft = false;
                        Top = false;
                        TopRight = true;
                        Left = false;
                        Center = false;
                        Right = true;
                        BottomLeft = false;
                        Bottom = false;
                        BottomRight = true;
                        break;
                }
                //switch (LabelVAlign)
                //{
                //    case VerticalAlignment.Top:
                //        TopLeft = TopLeft;
                //        Top = Top;
                //        TopRight = TopRight;
                //        Left = false;
                //        Center = false;
                //        Right = false;
                //        BottomLeft = false;
                //        Bottom = false;
                //        BottomRight = false;
                //        break;
                //    case VerticalAlignment.Center:
                //        TopLeft = false;
                //        Top = false;
                //        TopRight = false;
                //        Left = Left;
                //        Center = Center;
                //        Right = Right;
                //        BottomLeft = false;
                //        Bottom = false;
                //        BottomRight = false;
                //        break;
                //    case VerticalAlignment.Bottom:
                //        TopLeft = false;
                //        Top = false;
                //        TopRight = false;
                //        Left = false;
                //        Center = false;
                //        Right = false;
                //        BottomLeft = BottomLeft;
                //        Bottom = Bottom;
                //        BottomRight = BottomRight;
                //        break;
                //}
            }
            else
            {
                TopLeft = false;
                Top = false;
                TopRight = false;
                Left = false;
                Center = false;
                Right = false;
                BottomLeft = false;
                Bottom = false;
                BottomRight = false;
            }


            if (LabelHAlign.HasValue)
            {
                foreach (var item in SelectedItems)
                {
                    if(item is INodeVM)
                    {
                        if(LabelHAlign != null)
                            (item as INodeVM).LabelHAlign = LabelHAlign.Value;
                        //if(LabelVAlign != null)
                        //    (item as INodeVM).LabelVAlign = LabelVAlign.Value;
                        if ((item as NodeVM).DataContext != null && (item as NodeVM).DataContext is PropertyChange)
                        {
                            ((item as NodeVM).DataContext as PropertyChange).HorizontalAlignment = LabelHAlign.Value;
                        }
                    }
                }
            }


            //if (LabelHAlign.HasValue)
            //{
            //    foreach (var item in SelectedItems)
            //    {
            //        item.LabelHAlign = LabelHAlign.Value;
            //    }
            //}
            //if (LabelVAlign.HasValue)
            //{
            //    foreach (var item in SelectedItems)
            //    {
            //        item.LabelVAlign = LabelVAlign.Value;
            //    }
            //}
            _mHoldLabelAlignment = false;
        }

        private void OnLabelVAlignChanged()
        {
            OnLabelHAlignChanged();
        }

        private void OnLabelForegroundChanged()
        {
            if (LabelForeground != null)
            {
                foreach (var item in SelectedItems)
                {
                    //item.LabelForeground = LabelForeground;
                    if ((item as INodeVM).Shape.Equals("label"))
                    {
                        foreach (LabelVM _mAnnotation in (item as INodeVM).Annotations as List<IAnnotation>)
                        {
                            if ((_mAnnotation.DataContext as LabelBusinessClass).SelectedState == "Normal")
                                item.LabelForeground = LabelForeground;
                        }
                    }
                    else
                    {
                        item.LabelForeground = LabelForeground;
                    }
                    foreach (LabelVM _mAnnotation in (item as INodeVM).Annotations as List<IAnnotation>)
                    {
                        if ((item as INodeVM).Shape.Equals("checkboxgroup") )
                        {
                            ((item as INodeVM).DataContext as CheckBoxGroupBusinessClass).Foreground = LabelForeground;
                        }
                        else if ((item as INodeVM).Shape.Equals("radiobuttongroup"))
                        {
                            ((item as INodeVM).DataContext as RadioButtonGroupBusinessClass).Foreground = LabelForeground;
                        }
                        else if (((item as INodeVM).Shape.Equals("checkbox"))
                        || (item as INodeVM).Shape.Equals("radiobutton")
                        )
                        {
                            ((item as INodeVM).DataContext as CheckBoxBusinessClass).Foreground = LabelForeground;
                        }
                        else if((item as INodeVM).Shape.Equals("datechooser"))
                        {
                            ((item as INodeVM).DataContext as DateChoosetBusinessClass).Foreground = LabelForeground;
                        }
                        else if ((item as INodeVM).Shape.Equals("searchbutton"))
                        {
                            ((item as INodeVM).DataContext as SearchBoxBusinessClass).Foreground = LabelForeground;
                        }
                            // Have to move this code to right place
                        else if ((item as INodeVM).Shape.Equals("linkbar"))
                        {
                            (_mAnnotation.DataContext as LinkBarBusinessClass).Foreground = LabelForeground;
                        }
                        else if ((item as INodeVM).Shape.Equals("numericstepper"))
                        {
                            (_mAnnotation.DataContext as NumericStepperBusinessClass).Foreground = LabelForeground;
                        }
                        else if ((item as INodeVM).Shape.Equals("combobox"))
                        {
                            (_mAnnotation.DataContext as ComboBoxBusinessClass).Foreground = LabelForeground;
                        }
                    }
                }
            }
        }

        private void OnLabelBackgroundChanged()
        {
            if (LabelBackground != null)
            {
                foreach (var item in SelectedItems)
                {
                    item.LabelBackground = LabelBackground;
                }
            }
        }

        private void OnLabelMarginChanged()
        {
            if (LabelMargin.HasValue)
            {
                foreach (var item in SelectedItems)
                {
                    item.LabelMargin = LabelMargin.Value;  //new Thickness(LabelMargin.Value.le);
                }
            }
        }

        private void OnHyperLinkChanged()
        {
            if (HyperLink != null)
            {
                foreach (var item in SelectedItems)
                {
                    item.HyperLink = HyperLink;
                }
            }
        }

        private void OnTabSelectedIndexChanged()
        {
            foreach (var item in SelectedItems)
            {
                if (item is INodeVM)
                    (item as INodeVM).TabSelectedIndex = TabSelectedIndex ;
            }
        }

        private void OnTabItemChanged()
        {
            //foreach (var item in SelectedItems)
            //{ 
            //    if (item is INodeVM)
            //        (item as INodeVM).TabItem = TabItem;
            //}
        }

        private void OnSliderValueChanged()
        {
            foreach (var node in SelectedItems.OfType<INodeVM>())
            {
                if (node is NodeVM)
                {
                    //(node as NodeVM).SliderValue = SliderValue;
                    //SelectorToNode(item as INodeVM);
                    SliderValueAdjust(node);
                }

            }
        }

        private void SliderValueAdjust(INodeVM node)
        {
            if ((node as NodeVM).Shape != null)
            {
                if (node.DataContext is SliderBusinessClass)
                {
                    if ((node as NodeVM).Shape.Equals("horizotalslider"))
                    {
                        (node.DataContext as SliderBusinessClass).SliderValue = (this.SliderValue / 100) * (node.UnitWidth - (node.DataContext as SliderBusinessClass).SliderBallSize);
                        (node.DataContext as SliderBusinessClass).ScrollMargin = new Thickness((node.DataContext as SliderBusinessClass).SliderValue, (node.DataContext as SliderBusinessClass).ScrollMargin.Top, (node.DataContext as SliderBusinessClass).ScrollMargin.Right, (node.DataContext as SliderBusinessClass).ScrollMargin.Bottom);
                    }
                    else if ((node as NodeVM).Shape.Equals("verticalslider"))
                    {
                        (node.DataContext as SliderBusinessClass).SliderValue = (this.SliderValue / 100) * (node.UnitHeight - (node.DataContext as SliderBusinessClass).SliderBallSize);
                        (node.DataContext as SliderBusinessClass).ScrollMargin = new Thickness((node.DataContext as SliderBusinessClass).ScrollMargin.Left, (node.DataContext as SliderBusinessClass).SliderValue, (node.DataContext as SliderBusinessClass).ScrollMargin.Right, (node.DataContext as SliderBusinessClass).ScrollMargin.Bottom);
                    }
                    #region ScrollBar
                    else if ((node as NodeVM).Shape.Equals("horizontalscrollbar"))
                    {
                        double val = ((this.SliderValue / 100) * (node.UnitWidth - ((node.DataContext as HorizontalScrollBarBusinessClass).LeftButtonSize + (node.DataContext as HorizontalScrollBarBusinessClass).RightButtonSize + (node.DataContext as HorizontalScrollBarBusinessClass).ScrollThumbWidth)));
                        (node.DataContext as HorizontalScrollBarBusinessClass).SliderValue = val;
                        (node.DataContext as HorizontalScrollBarBusinessClass).ScrollMargin = new Thickness(val, (node.DataContext as HorizontalScrollBarBusinessClass).ScrollMargin.Top, (node.DataContext as HorizontalScrollBarBusinessClass).ScrollMargin.Right, (node.DataContext as HorizontalScrollBarBusinessClass).ScrollMargin.Bottom);
                    }
                    else if ((node as NodeVM).Shape.Equals("verticalscrollbar"))
                    {
                        double val = ((this.SliderValue / 100) * (node.UnitHeight - ((node.DataContext as VerticalScrollBarBusinessClass).TopButtonSize + (node.DataContext as VerticalScrollBarBusinessClass).BottomButtonSize + (node.DataContext as VerticalScrollBarBusinessClass).ScrollThumbHeight)));
                        (node.DataContext as VerticalScrollBarBusinessClass).SliderValue = val;
                        (node.DataContext as VerticalScrollBarBusinessClass).ScrollMargin = new Thickness((node.DataContext as VerticalScrollBarBusinessClass).ScrollMargin.Left, val, (node.DataContext as VerticalScrollBarBusinessClass).ScrollMargin.Right, (node.DataContext as VerticalScrollBarBusinessClass).ScrollMargin.Bottom);
                    }
                    #endregion
                }
                #region volumeslider
                if ((node as NodeVM).Shape.Equals("volumeslider"))
                {
                    (node.DataContext as VolumeSliderBusinessClass).SliderValue = (this.SliderValue / 100) * (node.UnitWidth - (node.DataContext as VolumeSliderBusinessClass).VolumeSymbolWidth - (node.DataContext as VolumeSliderBusinessClass).SliderBallSize);
                    (node.DataContext as VolumeSliderBusinessClass).ScrollMargin = new Windows.UI.Xaml.Thickness((node.DataContext as VolumeSliderBusinessClass).SliderValue, (node.DataContext as VolumeSliderBusinessClass).ScrollMargin.Top, (node.DataContext as VolumeSliderBusinessClass).ScrollMargin.Right, (node.DataContext as VolumeSliderBusinessClass).ScrollMargin.Bottom);
                }
                #endregion
                #region progressbar
                if ((node as NodeVM).Shape.Equals("progressbar"))
                {
                    double val = ((this.SliderValue / 100) * (node.UnitWidth));
                    (node.DataContext as ProgressBarBusinessClass).ProgressValue = val;
                    //(node.DataContext as HorizontalScrollBarBusinessClass).ScrollMargin = new Thickness(val, (node.DataContext as HorizontalScrollBarBusinessClass).ScrollMargin.Top, (node.DataContext as HorizontalScrollBarBusinessClass).ScrollMargin.Right, (node.DataContext as HorizontalScrollBarBusinessClass).ScrollMargin.Bottom);
                }
                #endregion
                if ((node as NodeVM).Shape.Equals("windowbox"))
                {
                    double val = ((this.SliderValue / 100) * ((node.DataContext as WindowBoxBusinessObject).WindowBodyHeight - ((node.DataContext as WindowBoxBusinessObject).ScrollBar.TopButtonSize + (node.DataContext as WindowBoxBusinessObject).ScrollBar.BottomButtonSize + (node.DataContext as WindowBoxBusinessObject).ScrollBar.ScrollThumbHeight)));
                    (node.DataContext as WindowBoxBusinessObject).ScrollBar.SliderValue = val;
                    (node.DataContext as WindowBoxBusinessObject).ScrollBar.ScrollMargin = new Thickness((node.DataContext as WindowBoxBusinessObject).ScrollBar.ScrollMargin.Left, val, (node.DataContext as WindowBoxBusinessObject).ScrollBar.ScrollMargin.Right, (node.DataContext as WindowBoxBusinessObject).ScrollBar.ScrollMargin.Bottom);
                }
                if ((node as NodeVM).Shape.Equals("tabsbar"))
                {
                    double val = ((this.SliderValue / 100) * ((node.UnitHeight - (FontSize.Value + 2 * node.LabelMargin.Top + node.LabelMargin.Bottom)) - ((node.DataContext as TabBusinessClass).ScrollBar.TopButtonSize + (node.DataContext as TabBusinessClass).ScrollBar.BottomButtonSize + (node.DataContext as TabBusinessClass).ScrollBar.ScrollThumbHeight)));
                    (node.DataContext as TabBusinessClass).ScrollBar.SliderValue = val;
                    (node.DataContext as TabBusinessClass).ScrollBar.ScrollMargin = new Thickness((node.DataContext as TabBusinessClass).ScrollBar.ScrollMargin.Left, val, (node.DataContext as TabBusinessClass).ScrollBar.ScrollMargin.Right, (node.DataContext as TabBusinessClass).ScrollBar.ScrollMargin.Bottom);

                }
                else if ((node as NodeVM).Shape.Equals("verticaltab"))
                {
                    double val = ((this.SliderValue / 100) * (node.UnitHeight - ((node.DataContext as TabBusinessClass).ScrollBar.TopButtonSize + (node.DataContext as TabBusinessClass).ScrollBar.BottomButtonSize + (node.DataContext as TabBusinessClass).ScrollBar.ScrollThumbHeight)));
                    (node.DataContext as TabBusinessClass).ScrollBar.SliderValue = val;
                    (node.DataContext as TabBusinessClass).ScrollBar.ScrollMargin = new Thickness((node.DataContext as TabBusinessClass).ScrollBar.ScrollMargin.Left, val, (node.DataContext as TabBusinessClass).ScrollBar.ScrollMargin.Right, (node.DataContext as TabBusinessClass).ScrollBar.ScrollMargin.Bottom);
                }
                else if ((node as NodeVM).Shape.Equals("list"))
                {
                    double val = ((this.SliderValue / 100) * (node.UnitHeight - ((node.DataContext as ListBusinessClass).ScrollBar.TopButtonSize + (node.DataContext as ListBusinessClass).ScrollBar.BottomButtonSize + (node.DataContext as ListBusinessClass).ScrollBar.ScrollThumbHeight)));
                    (node.DataContext as ListBusinessClass).ScrollBar.SliderValue = val;
                    (node.DataContext as ListBusinessClass).ScrollBar.ScrollMargin = new Thickness((node.DataContext as ListBusinessClass).ScrollBar.ScrollMargin.Left, val, (node.DataContext as ListBusinessClass).ScrollBar.ScrollMargin.Right, (node.DataContext as ListBusinessClass).ScrollBar.ScrollMargin.Bottom);
                }
                else if ((node as NodeVM).Shape.Equals("textarea"))
                {
                    double val = ((this.SliderValue / 100) * (node.UnitHeight - ((node.DataContext as TextAreaBusinessClass).ScrollBar.TopButtonSize + (node.DataContext as TextAreaBusinessClass).ScrollBar.BottomButtonSize + (node.DataContext as TextAreaBusinessClass).ScrollBar.ScrollThumbHeight)));
                    (node.DataContext as TextAreaBusinessClass).ScrollBar.SliderValue = val;
                    (node.DataContext as TextAreaBusinessClass).ScrollBar.ScrollMargin = new Thickness((node.DataContext as TextAreaBusinessClass).ScrollBar.ScrollMargin.Left, val, (node.DataContext as TextAreaBusinessClass).ScrollBar.ScrollMargin.Right, (node.DataContext as TextAreaBusinessClass).ScrollBar.ScrollMargin.Bottom);
                }
                else if ((node as NodeVM).Shape.Equals("browserWindow"))
                {
                    double val = ((this.SliderValue / 100) * ((node.DataContext as BrowserWindowBusinessClass).BrowserBodyHeight - ((node.DataContext as BrowserWindowBusinessClass).ScrollBar.TopButtonSize + (node.DataContext as BrowserWindowBusinessClass).ScrollBar.BottomButtonSize + (node.DataContext as BrowserWindowBusinessClass).ScrollBar.ScrollThumbHeight)));
                    (node.DataContext as BrowserWindowBusinessClass).ScrollBar.SliderValue = val;
                    (node.DataContext as BrowserWindowBusinessClass).ScrollBar.ScrollMargin = new Thickness((node.DataContext as BrowserWindowBusinessClass).ScrollBar.ScrollMargin.Left, val, (node.DataContext as BrowserWindowBusinessClass).ScrollBar.ScrollMargin.Right, (node.DataContext as BrowserWindowBusinessClass).ScrollBar.ScrollMargin.Bottom);
                }
                else if ((node as NodeVM).Shape.Equals("accordion"))
                {
                    if (SelectedIndex != 0)
                    {
                        double val = ((this.SliderValue / 100) * (((node.DataContext as AccordionBusinessObject).Items[(node.DataContext as AccordionBusinessObject).SelectedIndex - 1] as AccordionItemBusinessObject).Height - ((node.DataContext as AccordionBusinessObject).ScrollBar.TopButtonSize + (node.DataContext as AccordionBusinessObject).ScrollBar.BottomButtonSize + (node.DataContext as AccordionBusinessObject).ScrollBar.ScrollThumbHeight)));
                        (node.DataContext as AccordionBusinessObject).ScrollBar.SliderValue = val;
                        (node.DataContext as AccordionBusinessObject).ScrollBar.ScrollMargin = new Thickness((node.DataContext as AccordionBusinessObject).ScrollBar.ScrollMargin.Left, val, (node.DataContext as AccordionBusinessObject).ScrollBar.ScrollMargin.Right, (node.DataContext as AccordionBusinessObject).ScrollBar.ScrollMargin.Bottom);
                    }
                }
            }
        } 

        private void OnTopMarginChanged()
        {
            foreach (var item in SelectedItems)
            {
                if (item is INodeVM)
                {
                    if((item as INodeVM).DataContext is WindowBoxBusinessObject)
                    {
                        ((item as INodeVM).DataContext as WindowBoxBusinessObject).TopMargin = new Thickness(0, TopMargin, 0, 0);
                    }
                }
            }
        }

        private void OnBottomMarginChanged()
        {
            foreach (var item in SelectedItems)
            {
                if (item is INodeVM)
                {
                    if ((item as INodeVM).DataContext is WindowBoxBusinessObject)
                    {
                        ((item as INodeVM).DataContext as WindowBoxBusinessObject).BottomMargin = new Thickness(0, 0, 0, BottomMargin);
                    }
                }
            }
        }

        #endregion

        string _mName = string.Empty;
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

        private void OnNameChanged()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    node.Name = Name;
                }
            }
        }

        #region State

        #region Prop

        private bool? _mVisibile = null;
        public bool? Visibile
        {
            get
            {
                return _mVisibile;
            }
            set
            {
                if (_mVisibile != value)
                {
                    _mVisibile = value;
                    OnPropertyChanged(GroupableConstants.Visibility);
                }
            }
        }

        bool? _mAllowDrag = true;
        public bool? AllowDrag
        {
            get
            {
                return _mAllowDrag;
            }
            set
            {
                if (_mAllowDrag != value)
                {
                    _mAllowDrag = value;
                    OnPropertyChanged(NodeConstants.AllowDrag);
                }
            }
        }

        bool? _mAllowResize = true;
        public bool? AllowResize
        {
            get
            {
                return _mAllowResize;
            }
            set
            {
                if (_mAllowResize != value)
                {
                    _mAllowResize = value;
                    OnPropertyChanged(NodeConstants.AllowResize);
                }
            }
        }

        bool? _mAllowRotate = true;
        private bool _textLeft;
        private bool _textCenter;
        private bool _textRight;
        private ICommand _pickerCommand;
        private Brush _pickedBrush;
        private CurrentBrush _currentBrush;
        private Visibility _isBrushEditing = Visibility.Collapsed;

        public bool? AllowRotate
        {
            get
            {
                return _mAllowRotate;
            }
            set
            {
                if (_mAllowRotate != value)
                {
                    _mAllowRotate = value;
                    OnPropertyChanged(NodeConstants.AllowRotate);
                }
            }
        }
        #endregion

        private void OnVisibilityChanged()
        {
            if (Visibile.HasValue)
            {
                foreach (var item in SelectedItems)
                {
                    if (Visibile.Value)
                    {
                        item.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        item.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void OnToolTipConstraintsChanged()
        {
            if (ToolTip != null)
            {
                ToolTip.Constraints = (ToolTipConstraints)Enum.Parse(typeof(ToolTipConstraints), ToolTipConstraint);
            }
        }

        private void OnAllowDragChanged()
        {
            if (AllowDrag.HasValue)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    node.AllowDrag = AllowDrag.Value;
                }
            }
        }

        private void OnAllowResizeChanged()
        {
            if (AllowResize.HasValue)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    node.AllowResize = AllowResize.Value;
                }
            }
        }

        private void OnAllowRotateChanged()
        {
            if (AllowRotate.HasValue)
            {
                foreach (var node in SelectedItems.OfType<INodeVM>())
                {
                    node.AllowRotate = AllowRotate.Value;
                }
            }
        }

        #endregion

        protected override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            switch (name)
            {
                case SelectorConstants.ToolTipConstraint:
                    OnToolTipConstraintsChanged();
                    break;
                case NodeConstants.AllowDrag:
                    OnAllowDragChanged();
                    break;
                case NodeConstants.AllowResize:
                    OnAllowResizeChanged();
                    break;
                case NodeConstants.AllowRotate:
                    OnAllowRotateChanged();
                    break;
                case SelectorConstants.Angle:
                    OnAngleChanged();
                    break;
                case GroupableConstants.Bold:
                    OnBoldChanged();
                    break;
                case ConnectorConstants.Caps:

                    break;
                case GroupableConstants.DashDot:
                    OnDashDotChanged();
                    break;
                case NodeConstants.Fill:
                    OnFillChanged();
                    break;
                case "ListOdd":
                    OnListOddChanged();
                    break;
                case "ListEven":
                    OnListEvenChanged();
                    break;
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
                case SelectorConstants.Height:
                    OnHeightChanged();
                    break;
                case GroupableConstants.Italic:
                    OnItalicChanged();
                    break;
                case GroupableConstants.Label:
                    OnLabelChanged();
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
                case GroupableConstants.Name:
                    OnNameChanged();
                    break;
                case ConnectorConstants.SourceCap:
                    OnSourceCapChanged();
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
                case ConnectorConstants.TargetCap:
                    OnTagetCapChanged();
                    break;
                case GroupableConstants.Thickness:
                    OnThicknessChanged();
                    break;
                case "StrokeDashArray":
                    OnStrokeDashArrayChanged();
                    break;
                case ConnectorConstants.Type:
                    OnTypeChanged();
                    break;
                case GroupableConstants.Visibility:
                    OnVisibilityChanged();
                    break;
                case SelectorConstants.Width:
                    OnWidthChanged();
                    break;
                case SelectorConstants.X:
                    OnXChanged();
                    break;
                case SelectorConstants.Y:
                    OnYChanged();
                    break;
                case SelectorConstants.Px:
                    OnPXChanged();
                    break;
                case SelectorConstants.Py:
                    OnPYChanged();
                    break;
                case GroupableConstants.TextLeft:
                case GroupableConstants.TextCenter:
                case GroupableConstants.TextRight:
                    OnTextAlignToggled(name);
                    break;
                case GroupableConstants.TopLeft:
                case GroupableConstants.Top:
                case GroupableConstants.TopRight:
                case GroupableConstants.Left:
                case GroupableConstants.Center:
                case GroupableConstants.Right:
                case GroupableConstants.BottomLeft:
                case GroupableConstants.Bottom:
                case GroupableConstants.BottomRight:
                    AlignToggled(name);
                    break;
                case SelectorConstants.CurrentBrush:
                    switch (CurrentBrush)
                    {
                        case CurrentBrush.Fill:
                            IsBrushEditing = Visibility.Visible;
                            PickedBrush = Fill;
                            break;
                        case CurrentBrush.Stroke:
                            IsBrushEditing = Visibility.Visible;
                            PickedBrush = Stroke;
                            break;
                        case CurrentBrush.LabelBackground:
                            IsBrushEditing = Visibility.Visible;
                            PickedBrush = LabelBackground;
                            break;
                        case CurrentBrush.LabelForeground:
                            IsBrushEditing = Visibility.Visible;
                            PickedBrush = LabelForeground;
                            break;
                        case CurrentBrush.None:
                            IsBrushEditing = Visibility.Collapsed;
                            break;
                    }
                    break;
                case SelectorConstants.PickedBrush:
                    switch (CurrentBrush)
                    {
                        case CurrentBrush.Fill:
                            Fill = PickedBrush;
                            break;
                        case CurrentBrush.ListOdd:
                            ListOdd = PickedBrush;
                            break;
                        case CurrentBrush.ListEven:
                            ListEven = PickedBrush;
                            break;
                        case CurrentBrush.Stroke:
                            Stroke = PickedBrush;
                            break;
                        case CurrentBrush.LabelBackground:
                            LabelBackground = PickedBrush;
                            break;
                        case CurrentBrush.LabelForeground:
                            LabelForeground = PickedBrush;
                            break;
                        case CurrentBrush.None:
                            break;
                    }
                    break;
                case GroupableConstants.LabelBackground:
                    OnLabelBackgroundChanged();
                    break;
                case GroupableConstants.LabelForeground:
                    OnLabelForegroundChanged();
                    break;
                case SelectorConstants.Scale:
                    OnScaleChanged();
                    break;
                case GroupableConstants.HyperLink:
                    OnHyperLinkChanged();
                    break;
                case SelectorConstants.TabSelectedIndex:
                    OnTabSelectedIndexChanged();
                    break;
                case SelectorConstants.SliderValue:
                    OnSliderValueChanged();
                    break;
                case "TopMargin":
                    OnTopMarginChanged();
                    break;
                case "BottomMargin":
                    OnBottomMarginChanged();
                    break;
                case SelectorConstants.TabItem:
                    OnTabItemChanged();
                    break;
                case "SelectedIndex":
                    OnSelectedIndexChanged();
                    break;
                case "SelectedItem":
                    OnSelectedItemChanged();
                    break;
                case "PropertiesList":
                    OnPropertiesListChanged();
                    break;
            }
        }

        private void OnScaleChanged()
        {
            IGraphInfo info = Diagram.Info as IGraphInfo;
            if (info != null)
            {
                info.Commands.Zoom.Execute(new ZoomPositionParamenter()
                {
                    ZoomTo = Scale
                });
            }
        }

        public ICommand PickerCommand
        {
            get { return _pickerCommand; }
            set
            {
                if (_pickerCommand != value)
                {
                    _pickerCommand = value;
                    OnPropertyChanged(SelectorConstants.PickerCommand);
                }
            }
        }

        public Brush PickedBrush
        {
            get { return _pickedBrush; }
            set
            {
                if (_pickedBrush != value)
                {
                    _pickedBrush = value;
                    OnPropertyChanged(SelectorConstants.PickedBrush);
                }
            }
        }

        public CurrentBrush CurrentBrush
        {
            get { return _currentBrush; }
            set
            {
                if (IsBrushEditing == Visibility.Collapsed)
                    IsBrushEditing = Visibility.Visible;
                else
                    IsBrushEditing = Visibility.Collapsed;
                if (_currentBrush != value)
                {
                    _currentBrush = value;                   
                    OnPropertyChanged(SelectorConstants.CurrentBrush);
                }
            }
        }

        public Visibility IsBrushEditing
        {
            get { return _isBrushEditing; }
            set
            {
                if (_isBrushEditing != value)
                {
                    _isBrushEditing = value;
                    OnPropertyChanged(SelectorConstants.IsBrushEditing);
                }
            }
        }

        private Brush _mLabelBackground;
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

        private Brush _mLabelForeground;
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

        private bool _mAnythingSelected = false;
        public bool IsAnythingSelected
        {
            get { return _mAnythingSelected; }
            set
            {
                if (_mAnythingSelected != value)
                {
                    _mAnythingSelected = value;
                    OnPropertyChanged(SelectorConstants.IsAnythingSelected);
                }
            }
        }

        private bool _mIsNodeSelected = false;
        public bool IsNodeSelected
        {
            get { return _mIsNodeSelected; }
            set
            {
                if (_mIsNodeSelected != value)
                {
                    _mIsNodeSelected = value;
                    OnPropertyChanged(SelectorConstants.IsNodeSelected);
                }                
            }
        }

        private bool _mIsMultiNodeSelected = false;
        public bool IsMultiNodeSelected
        {
            get { return _mIsMultiNodeSelected; }
            set
            {
                if (_mIsMultiNodeSelected != value)
                {
                    _mIsMultiNodeSelected = value;
                    OnPropertyChanged("IsMultiNodeSelected");
                }
            }
        }

        private bool _mIsGroupSelected = false;
        public bool IsGroupSelected
        {
            get { return _mIsGroupSelected; }
            set
            {
                if (_mIsGroupSelected != value)
                {
                    _mIsGroupSelected = value;
                    OnPropertyChanged("IsGroupSelected");
                }
            }
        }

        private bool _mIsNodeLocked = false;
        public bool IsNodeLocked
        {
            get { return _mIsNodeLocked; }
            set
            {
                if (_mIsNodeLocked != value)    
                {
                    _mIsNodeLocked = value;
                    OnPropertyChanged("IsNodeLocked");
                    if (value)
                    {
                        this.OpenEditPanel = null;
                        this.OpenSettingPanel = null;
                        this.OpenTextPanel = null;
                        this.OpenLinkPanel = null;
                        foreach (var node in SelectedItems.OfType<INodeVM>())
                        {
                            foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                            {
                                _mAnnotation.IsEditable = false;
                            }
                        }
                    }
                    else
                    {
                        this.OpenEditPanel = false;
                        this.OpenSettingPanel = false;
                        this.OpenTextPanel = false;
                        this.OpenLinkPanel = false;
                        foreach (var node in SelectedItems.OfType<INodeVM>())
                        {
                            foreach (LabelVM _mAnnotation in (node as NodeVM).Annotations as List<IAnnotation>)
                            {
                                _mAnnotation.IsEditable = true;                             
                            }
                        }
                    }
                }
            }
        }

        private bool _mIsConnectorSelected = false;
        public bool IsConnectorSelected
        {
            get { return _mIsConnectorSelected; }
            set
            {
                if (_mIsConnectorSelected != value)
                {
                    _mIsConnectorSelected = value;
                    OnPropertyChanged(SelectorConstants.IsConnectorSelected);
                }
            }
        }

        private bool _mIsLabelSet = false;
        public bool IsLabelSet
        {
            get { return _mIsLabelSet; }
            set
            {
                if (_mIsLabelSet != value)
                {
                    _mIsLabelSet = value;
                    OnPropertyChanged(SelectorConstants.IsLabelSet);
                }
            }
        }

        private bool _mIsNodeLabelSet = false;
        public bool IsNodeLabelSet
        {
            get { return _mIsNodeLabelSet; }
            set
            {
                if (_mIsNodeLabelSet != value)
                {
                    _mIsNodeLabelSet = value;
                    OnPropertyChanged(SelectorConstants.IsNodeLabelSet);
                }
            }
        }

        private bool _mIsConnectorLabelSet = false;
        public bool IsConnectorLabelSet
        {
            get { return _mIsConnectorLabelSet; }
            set
            {
                if (_mIsConnectorLabelSet != value)
                {
                    _mIsConnectorSelected = value;
                    OnPropertyChanged(SelectorConstants.IsConnectorLabelSet);
                }
            }
        }

        private double _mScale = 1d;
        public double Scale
        {
            get { return _mScale; }
            set
            {
                if (_mScale != value)
                {
                    _mScale = value;
                    OnPropertyChanged(SelectorConstants.Scale);
                }
            }
        }

        string _mHyperLink = "http://";
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

        private double _mSliderValue = 0;
        public double SliderValue
        {
            get { return _mSliderValue; }
            set {
                //if (_mSliderValue != value)
                {
                    _mSliderValue = value;
                    OnPropertyChanged("SliderValue");
                }
            }
        }

        private double _mTopMargin = 0;
        public double TopMargin
        {
            get { return _mTopMargin; }
            set
            {
                if (_mTopMargin != value)
                {
                    _mTopMargin = value;
                    OnPropertyChanged("TopMargin");
                }
            }
        }

        private double _mBottomMargin = 0;
        public double BottomMargin
        {
            get { return _mBottomMargin; }
            set
            {
                if (_mBottomMargin != value)
                {
                    _mBottomMargin = value;
                    OnPropertyChanged("BottomMargin");
                }
            }
        }
        
        private int _mTabSelectedIndex = 1;
        public int TabSelectedIndex
        {
            get { 
                return _mTabSelectedIndex; }
            set
            {
                if (_mTabSelectedIndex != value )
                {
                    _mTabSelectedIndex = value;
                    OnPropertyChanged("TabSelectedIndex");
                }
            }
        }

        private ObservableCollection<object> _mStates;
        public ObservableCollection<object> States
        {
            get
            {
                return _mStates;
            }
            set
            {
                if (_mStates != value)
                {
                    _mStates = value;
                    OnPropertyChanged("States");
                }
            }
        }        

        private ObservableCollection<HyperlinkBusinessClass> _mLinks;
        public ObservableCollection<HyperlinkBusinessClass> Links
        {
            get { return _mLinks; }
            set
            {
                if (_mLinks != value)
                {
                    _mLinks = value;
                    OnPropertyChanged("Links");
                }
            }
        }

        private object _mSelectedLink;
        public object SelectedLink
        {
            get { return _mSelectedLink; }
            set
            {
                if (_mSelectedLink != value)
                {
                    _mSelectedLink = value;
                    OnPropertyChanged("SelectedLink");
                }
            }
        }


        private object _mTabSelectedItem = "Tab1";

        public object TabSelectedItem
        {
            get { 
                return _mTabSelectedItem ; }
            set 
            { 

                _mTabSelectedItem = value;
                OnPropertyChanged("TabSelectedItem");            
            }
        }

        private int? _mSelectedIndex;
        public int? SelectedIndex
        {
            get { return _mSelectedIndex; }
            set 
            {               
                if (_mSelectedIndex != value)
                {
                    //if (value < 0) value = 0;
                    _mSelectedIndex = value;
                    OnPropertyChanged("SelectedIndex");
                }
            }
        }

        private object _mSelectedItem;
        public object SelectedItem
        {
            get { return _mSelectedItem; }
            set
            {
                if (_mSelectedItem != value)
                {
                    _mSelectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }           
        
        private object _mDataContext;
        public object DataContext
        {
            get { return _mDataContext; }
            set
            {
                if (_mDataContext != value)
                {
                    _mDataContext = value;
                    OnPropertyChanged("DataContext");
                }
            }
        }

        private object _mPropertyEditorUIVisibility;
        public object PropertyEditorUIVisibility
        {
            get { return _mPropertyEditorUIVisibility; }
            set 
            {
                if (_mPropertyEditorUIVisibility != value)
                {
                    _mPropertyEditorUIVisibility = value;
                    OnPropertyChanged("PropertyEditorUIVisibility");
                }
            }
        }

        private object _mPropertiesList;
        public object PropertiesList
        {
            get { return _mPropertiesList; }
            set 
            {
                _mPropertiesList = value;
                OnPropertyChanged("PropertiesList");
            }
        }


        public double PropertyPanelHeight { get; set; }

        private double _mPropertyPanelOffsetX;
        public double PropertyPanelOffsetX
        {
            get { return _mPropertyPanelOffsetX; }
            private set
            {
                if (_mPropertyPanelOffsetX != value)
                {
                    if (!double.IsNaN(value))
                    {
                        _mPropertyPanelOffsetX = value;
                        OnPropertyChanged("PropertyPanelOffsetX");
                    }
                }
            }
        }

        private double _mPropertyPanelOffsetY;
        public double PropertyPanelOffsetY
        {
            get { return _mPropertyPanelOffsetY; }
            private set
            {
                if (_mPropertyPanelOffsetY != value)
                {
                    if (!double.IsNaN(value))
                    {
                        _mPropertyPanelOffsetY = value;
                        OnPropertyChanged("PropertyPanelOffsetY");
                    }
                }
            }
        }

        private bool _mAspectRatioEnabled;
        public bool AspectRatioEnabled
        {
            get { return _mAspectRatioEnabled; }
            private set
            {
                _mAspectRatioEnabled = value;
                OnPropertyChanged("AspectRatioEnabled");
            }
        }

        private bool? _mOpenEditPanel;
        public bool? OpenEditPanel
        {
            get { return _mOpenEditPanel; }
            set
            {
                if (_mOpenEditPanel != value)
                {
                    _mOpenEditPanel = value;
                    OnPropertyChanged("OpenEditPanel");
                    if(value !=null && value.Value)
                    {
                        if(OpenTextPanel != null) OpenTextPanel = false;
                        if (OpenSettingPanel != null) OpenSettingPanel = false;
                        if (OpenLinkPanel != null) OpenLinkPanel = false;
                        OnXChanged();
                        OnYChanged();
                    }
                }
            }
        }

        private bool? _mOpenTextPanel;
        public bool? OpenTextPanel
        {
            get { return _mOpenTextPanel; }
            set
            {
                if (_mOpenTextPanel != value)
                {
                    _mOpenTextPanel = value;
                    OnPropertyChanged("OpenTextPanel");
                    if(value != null && value.Value)
                    {
                        if (OpenEditPanel != null) OpenEditPanel = false;
                        if (OpenSettingPanel != null) OpenSettingPanel = false;
                        if (OpenLinkPanel != null) OpenLinkPanel = false;
                        OnXChanged();
                        OnYChanged();
                    }
                }
            }
        }

        private bool? _mOpenSettingPanel;
        public bool? OpenSettingPanel
        {
            get { return _mOpenSettingPanel; }
            set
            {
                if (_mOpenSettingPanel != value)
                {
                    _mOpenSettingPanel = value;
                    OnPropertyChanged("OpenSettingPanel");
                    if (value != null && value.Value)
                    {
                        if (OpenEditPanel != null) OpenEditPanel = false;
                        if (OpenTextPanel != null) OpenTextPanel = false;
                        if (OpenLinkPanel != null) OpenLinkPanel = false;
                        OnXChanged();
                        OnYChanged();
                    }
                }
            }
        }

        private bool? _mOpenLinkPanel;
        public bool? OpenLinkPanel
        {
            get { return _mOpenLinkPanel; }
            set
            {
                if (_mOpenLinkPanel != value)
                {
                    _mOpenLinkPanel = value;
                    OnPropertyChanged("OpenLinkPanel");
                    if (value !=null && value.Value)
                    {
                        if (OpenEditPanel != null) OpenEditPanel = false;
                        if (OpenTextPanel != null) OpenTextPanel = false;
                        if (OpenSettingPanel != null) OpenSettingPanel = false;
                        OnXChanged();
                        OnYChanged();
                    }
                }
            }
        }

        private HorizontalAlignment _mHorizontalAlignment = HorizontalAlignment.Right;
        public HorizontalAlignment HorizontalAlignment
        {
            get { return _mHorizontalAlignment; }
            set
            {
                if (_mHorizontalAlignment != value)
                {
                    _mHorizontalAlignment = value;
                    OnPropertyChanged("HorizontalAlignment");
                    if (value == Windows.UI.Xaml.HorizontalAlignment.Left)
                        Margin = new Thickness(-50, Margin.Top, 0, Margin.Bottom);
                    else
                        Margin = new Thickness(0, Margin.Top, -50, Margin.Bottom);
                }
            }
        }

        private Thickness _mMargin = new Thickness(0, 0, -50, -200);
        public Thickness Margin
        {
            get { return _mMargin; }
            set
            {
                if (_mMargin != value)
                {
                    _mMargin = value;
                    OnPropertyChanged("Margin");
                }
            }
        }
        
        
    }

    public static class SelectorConstants
    {
        public const string X = "X";
        public const string Y = "Y";
        public const string Px = "Px";
        public const string Py = "Py";
        public const string Width = "Width";
        public const string Height = "Height";
        public const string Angle = "Angle";
        public const string PickerCommand = "PickerCommand";
        public const string PickedBrush = "PickedBrush";
        public const string CurrentBrush = "CurrentBrush";
        public const string IsBrushEditing = "IsBrushEditing";
        public const string IsAnythingSelected = "IsAnythingSelected";
        public const string IsNodeSelected = "IsNodeSelected";
        public const string IsConnectorSelected = "IsConnectorSelected";
        public const string Scale = "Scale";
        public const string IsLabelSet = "IsLabelSet";
        public const string IsNodeLabelSet = "IsNodeLabelSet";
        public const string IsConnectorLabelSet = "IsConnectorLabelSet";
        public const string ToolTipConstraint = "ToolTipConstraint";
        public const string TabSelectedIndex = "TabSelectedIndex";
        public const string SliderValue = "SliderValue";
        public const string TabItem = "TabItem";         
    }

    public interface ISelectorVM : ISelector
    {
        double? X { get; set; }
        double? Y { get; set; }
        double? Px { get; set; }
        double? Py { get; set; }
        double? Angle { get; set; }
        double? Width { get; set; }
        double? Height { get; set; }

        Brush Fill { get; set; }
        Brush Stroke { get; set; }
        double? Thickness { get; set; }
        string DashDot { get; set; }
        Style Style { get; set; }
        double? Opacity { get; set; }
        double Scale { get; set; }

        string SourceCap { get; set; }
        string TargetCap { get; set; }
        ConnectType? Type { get; set; }

        string Label { get; set; }
        FontFamily Font { get; set; }
        double? FontSize { get; set; }
        FontWeight? FontWeight { get; set; }
        FontStyle? FontStyle { get; set; }
        bool? Bold { get; set; }
        bool? Italic { get; set; }
        Brush LabelForeground { get; set; }
        Brush LabelBackground { get; set; }

        TextAlignment? TextAlignment { get; set; }
        HorizontalAlignment? LabelHAlign { get; set; }
        VerticalAlignment? LabelVAlign { get; set; }

        bool TextLeft { get; set; }
        bool TextCenter { get; set; }
        bool TextRight { get; set; }

        bool TopLeft { get; set; }
        bool Top { get; set; }
        bool TopRight { get; set; }

        bool Left { get; set; }
        bool Center { get; set; }
        bool Right { get; set; }

        bool BottomLeft { get; set; }
        bool Bottom { get; set; }
        bool BottomRight { get; set; }

        Thickness? LabelMargin { get; set; }

        string Name { get; set; }
        bool? Visibile { get; set; }
        bool? AllowDrag { get; set; }
        bool? AllowResize { get; set; }
        bool? AllowRotate { get; set; }

        ICommand PickerCommand { get; set; }
        Brush PickedBrush { get; set; }
        CurrentBrush CurrentBrush { get; set; }
        Visibility IsBrushEditing { get; set; }

        bool IsAnythingSelected { get; set; }
        bool IsNodeSelected { get; set; }
        bool IsConnectorSelected { get; set; }

        bool IsLabelSet { get; set; }
        bool IsNodeLabelSet { get; set; }
        bool IsConnectorLabelSet { get; set; }

        //Wireframe 
        double SliderValue { get; set; }
        int TabSelectedIndex { get; set; }
        //List<DoubleCollection> DashDots { get; set; }
        //List<Geometry> Caps { get; set; }
        //List<ConnectorType> Types { get; set; }
        //List<FontFamily> Fonts { get; set; }
    }

    public enum ConnectType
    {
        Straight,
        Orthogonal,
        Bezier
    }

    public enum CurrentBrush
    {
        None,
        Stroke,
        Fill,
        LabelForeground,
        LabelBackground,
        ListOdd,
        ListEven
    }
}

