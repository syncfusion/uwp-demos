using Common;
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
using SampleBrowser;
using Windows.UI.Xaml.Navigation;
using System.Reflection;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MapControlUWP_Samples
{
    public sealed partial class BubbleVisualization : UserControl,IDisposable
    {
        public BubbleVisualization()
        {
            
            this.InitializeComponent();
            var m = this.map;
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                shapelayer.IsTileMap = true;
            }
            map.Loaded += Map_Loaded;
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            LoadShapefile();
        }

        private void LoadShapefile()
        {
            var assembly = typeof(BubbleVisualization).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.Maps.Assets.ShapeFiles.world1.shp";
            string valuePath = "Syncfusion.SampleBrowser.UWP.Maps.Assets.ShapeFiles.world1.dbf";
            var fileStream = assembly.GetManifestResourceStream(resourcePath);
            var file = assembly.GetManifestResourceStream(valuePath);               
            this.shapelayer.LoadFromStream(fileStream, file);

        }
      
        public void Dispose()
        {
            (this.grid.DataContext as IDisposable).Dispose();
            this.grid.DataContext = null;
            if (this.shapelayer.ItemsSource != null)
                this.shapelayer.ItemsSource = null;
            this.map.Dispose();
            GC.Collect();
        }
    }
}
