using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Common;
using Syncfusion.UI.Xaml.Maps;
using Windows.UI;
using System.Collections.ObjectModel;
using System.Reflection;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapControlUWP_Samples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShapeSelection : UserControl,IDisposable
    {
        public ShapeSelection()
        {
             this.InitializeComponent();
             this.map.PointerReleased += map_PointerReleased;
             (this.map.Layers[0] as ShapeFileLayer).ShapesSelected += ShapeSelection_ShapesSelected;
             (this.map.Layers[0] as ShapeFileLayer).ShapesUnSelected += ShapeSelection_ShapesUnSelected;
            //this.Unloaded += ShapeSelection_Unloaded;
            map.Loaded += Map_Loaded;
             listbox.SelectionChanged += listbox_SelectionChanged;
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            LoadShapeFile();
        }

        private void LoadShapeFile()
        {
            var assembly = typeof(ShapeSelection).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.Maps.Assets.ShapeFiles.world1.shp";
            string resourcePath1 = "Syncfusion.SampleBrowser.UWP.Maps.Assets.ShapeFiles.world1.dbf";
            var fileStream = assembly.GetManifestResourceStream(resourcePath);
            var fileStream1 = assembly.GetManifestResourceStream(resourcePath1);
            this.shapeLayer.LoadFromStream(fileStream,fileStream1);
        }

        void ShapeSelection_ShapesUnSelected(object sender, SelectionEventArgs args)
        {
            ShapeFileLayer shapeLayer = sender as ShapeFileLayer;
            int count = listbox.Items.Count;
            ObservableCollection<MapShape> items = args.Items as ObservableCollection<MapShape>;
            int j = -1;
            if (items.Count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (items[0].ShapeValue != null)
                    {
                        if (shapeLayer.EnableSelection && !shapeLayer.EnableMultiSelection && (listbox.Items[i] as ListBoxItem)!=null && items[0].ShapeValue.ToString() == (listbox.Items[i] as ListBoxItem).Content.ToString())
                        {
                            j = i;
                            break;
                        }
                        else
                        {
                            if (shapeLayer.EnableMultiSelection && items[0].ShapeValue.ToString() == listbox.Items[i].ToString())
                            {
                                j = i;
                                break;
                            }
                        }
                    }
                }
                if (j != -1)
                    listbox.Items.RemoveAt(j);
            }
        }

        void ShapeSelection_Unloaded(object sender, RoutedEventArgs e)
        {
            if(listbox!=null && listbox.Items!=null)
                 this.listbox.Items.Clear();
            textblock.Visibility = Visibility.Collapsed;
        }
              
        void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null && (sender as ListBox).SelectedValue != null && this.map != null)
            {
                string str = (sender as ListBox).SelectedValue.ToString();
                foreach (MapShape item in (this.map.Layers[0] as ShapeFileLayer).SelectedMapShapes)
                {
                    if (item != null && item.ShapeValue != null)
                    {
                        if (item.ShapeValue.ToString().Equals(str))
                        {
                            item.Shape.Fill = new SolidColorBrush(Color.FromArgb(0xFF,0x8A,0xC6,0x3C));
                        }
                        else
                        {
                            item.Shape.Fill = (this.map.Layers[0] as ShapeFileLayer).ShapeSettings.SelectedShapeColor;
                        }
                    }
                }
            }
           
        }

      
        void ShapeSelection_ShapesSelected(object sender, SelectionEventArgs args)
        {
            ShapeFileLayer shapefilelayer1 = sender as ShapeFileLayer;
            int count = shapefilelayer1.SelectedMapShapes.Count;
            if (count > 0 && shapefilelayer1.SelectedMapShapes[0].ShapeValue != null)
            {
                textblock.Visibility = Visibility.Visible;
                listbox.Items.Clear();
                for (int i = 0; i < count; i++)
                {
                    if (shapefilelayer1.SelectedMapShapes[i].ShapeValue != null)
						    listbox.Items.Add(new ListBoxItem() { Content = shapefilelayer1.SelectedMapShapes[i].ShapeValue.ToString() });
                }
            }
        }
              
       
        
        void map_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
           
            listbox.Items.Clear();
                   
            ShapeFileLayer  shapefilelayer1= (sender as SfMap).Layers[0] as ShapeFileLayer;
            
            if (shapefilelayer1.EnableMultiSelection == true && shapefilelayer1.SelectedMapShapes.Count > 0)
            {
                var selectedshapes = shapefilelayer1.SelectedMapShapes;
                foreach (MapShape mapshape in selectedshapes)
                {
                    if (mapshape.ShapeValue != null)
                    {
                        listbox.Items.Add( mapshape.ShapeValue.ToString());
                    }
                }
                
            }
            if (listbox.Items != null && listbox.Items.Count > 0)
            {
                textblock.Visibility = Visibility.Visible;
            }
            else
            {
                textblock.Visibility = Visibility.Collapsed;
            }
            
            
        }
        
        public void Dispose()
        {
            if(this.map != null)
            { 
                this.map.PointerReleased -= map_PointerReleased;
                if ((this.map.Layers[0] as ShapeFileLayer) != null)
                {
                    (this.map.Layers[0] as ShapeFileLayer).ShapesSelected -= ShapeSelection_ShapesSelected;
                }
                this.Unloaded -= ShapeSelection_Unloaded;
                if (listbox != null)
                    listbox.SelectionChanged -= listbox_SelectionChanged;
                if (this.grid.DataContext != null)
                {
                    //(this.grid.DataContext as IDisposable).Dispose();
                    this.grid.DataContext = null;
                }
                if (this.shapeLayer.ItemsSource != null)
                    this.shapeLayer.ItemsSource = null;
                this.map.Dispose();
            }

            GC.Collect();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (settingsGrid.Visibility == Visibility.Collapsed)
            {
                settingsGrid.Visibility = Visibility.Visible;
            }
            else
            {
                settingsGrid.Visibility = Visibility.Collapsed;
            }
        }
    }

    
}
