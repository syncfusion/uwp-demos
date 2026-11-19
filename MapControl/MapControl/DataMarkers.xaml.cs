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
using System.Reflection;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapControlUWP_Samples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataMarkers : UserControl,IDisposable
    {
        public DataMarkers()
        {
            this.InitializeComponent();
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {               
                shapelayer.IsTileMap = true;
            }
            map.Loaded += Map_Loaded;
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {                
                shapelayer.MarkerTemplate = (DataTemplate)Resources["MarkertemplateMobile"];
            }
                LoadShapeFile();
        }

        private void LoadShapeFile()
        {
            var assembly = typeof(DataMarkers).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.Maps.Assets.ShapeFiles.world1.shp";
            using (var fileStream = assembly.GetManifestResourceStream(resourcePath))
            {
                this.shapelayer.LoadFromStream(fileStream);
            }
        }

        public void Dispose()
        {
            (this.grid.DataContext as IDisposable).Dispose();
            this.grid.DataContext = null;
            this.map.Dispose();
            GC.Collect();
        }
    }
}
