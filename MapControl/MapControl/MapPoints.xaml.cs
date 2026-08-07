using Common;
using Syncfusion.UI.Xaml.Maps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace MapControlUWP_Samples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPoints : UserControl,IDisposable
    {
        public MapPoints()
        {
            this.InitializeComponent();
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {                
                shapelayer.IsTileMap = true;
            }

            maps.Loaded += Maps_Loaded;           
        }

        private void Maps_Loaded(object sender, RoutedEventArgs e)
        {
            LoadShapeFile();
        }

        private void LoadShapeFile()
        {
            var assembly = typeof(MapPoints).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.Maps.Assets.ShapeFiles.states.shp";
            string valuePath = "Syncfusion.SampleBrowser.UWP.Maps.Assets.ShapeFiles.landslide.shp";
            string valuePath1 = "Syncfusion.SampleBrowser.UWP.Maps.Assets.ShapeFiles.landslide.dbf";
            var fileStream = assembly.GetManifestResourceStream(resourcePath);
            var file = assembly.GetManifestResourceStream(valuePath);
            var file1 = assembly.GetManifestResourceStream(valuePath1);
            this.shapelayer.LoadFromStream(fileStream);
            this.sublayer.LoadFromStream(file,file1);
        }

        public void Dispose()
        {           
            maps.Dispose();
            GC.Collect();
        }
    }
}
