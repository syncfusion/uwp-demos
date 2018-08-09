using Common;
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

namespace Layout.DockingManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DockingManagerSerialization : SampleLayout
    {
        public DockingManagerSerialization()
        {
            this.InitializeComponent();
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                if(description != null)
                    description.Visibility = Visibility.Collapsed;
                if(DockingBorder != null)
                    DockingBorder.Margin = new Thickness(0, 0, 0, 35);
            }
        }

        private void SaveState(object sender, RoutedEventArgs e)
        {
            dockingManager.SaveDockState();
            loadLayout.IsEnabled = true;
        }

        private void LoadState(object sender, RoutedEventArgs e)
        {
            dockingManager.LoadDockState();
        }

        private void ResetState(object sender, RoutedEventArgs e)
        {
            dockingManager.ResetDockState();
        }

        public override void Dispose()
        {
            dockingManager.Dispose();
            dockingManager = null;
            GC.Collect();
            base.Dispose();
        }
    }
}
