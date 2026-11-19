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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TreeMapWinRTSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TreeMapDrillDown : UserControl, IDisposable
    {
        public TreeMapDrillDown()
        {
            this.InitializeComponent();
            if(Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                mainGrid.ColumnDefinitions.Clear();               
            }
        }

        private void chk_EnableDrillDown_Click(object sender, RoutedEventArgs e)
        {
            TreeMap.Levels[0].ShowLabels = TreeMap.EnableDrillDown;
            TreeMap.Levels[1].ShowLabels = TreeMap.EnableDrillDown;
        }

        public void Dispose()
        {
            TreeMap.Dispose();
        }
    }
}
