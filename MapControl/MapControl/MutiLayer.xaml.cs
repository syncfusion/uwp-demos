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
using Windows.UI.Xaml.Navigation;
using SampleBrowser;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleBrowser.UWP.MapControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MultiLayer : UserControl,IDisposable
    {
        public MultiLayer()
        {
            this.InitializeComponent();
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                grid.ColumnDefinitions.Clear();
                var groupTransform = new TransformGroup();
                groupTransform.Children.Add(new RotateTransform() { Angle = 90 });
                groupTransform.Children.Add(new TranslateTransform() { X = 380, Y = 20 });
                groupTransform.Children.Add(new ScaleTransform() { ScaleX = 1.2, ScaleY = 1.2 });
                map.RenderTransform = groupTransform;
            }
        }

       public void Dispose()
        {
            (this.grid.DataContext as IDisposable).Dispose();
            this.grid.DataContext = null;
            map.Dispose();
            GC.Collect();
        }
    }
}
