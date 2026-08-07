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
using SampleBrowser;
using Syncfusion.UI.Xaml.Maps;
using System.Reflection;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapControlUWP_Samples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BasicMap : UserControl,IDisposable
    {
        public BasicMap()
        {
            this.InitializeComponent();
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                shapelayer.IsTileMap = true;
            }
            map.Loaded += Map_Loaded;
           
        }

         void Map_Loaded(object sender, RoutedEventArgs e)
        {
            LoadShapeFile();
        }

         void LoadShapeFile()
        {
            var assembly = typeof(BasicMap).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.Maps.Assets.ShapeFiles.world1.shp";
            string valuePath = "Syncfusion.SampleBrowser.UWP.Maps.Assets.ShapeFiles.world1.dbf";
            var fileStream = assembly.GetManifestResourceStream(resourcePath);
            var file = assembly.GetManifestResourceStream(valuePath);
            this.shapelayer.LoadFromStream(fileStream, file);
        }

        public void Dispose()
        {
            map.Dispose();
            if (map != null)
                map = null;
            if (this.grid.DataContext != null)
            {
                //(this.grid.DataContext as IDisposable).Dispose();
                this.grid.DataContext = null;
            }
            if (this.shapelayer!=null && this.shapelayer.ItemsSource != null)
                this.shapelayer.ItemsSource = null;
            GC.Collect();
        }

    }
}
