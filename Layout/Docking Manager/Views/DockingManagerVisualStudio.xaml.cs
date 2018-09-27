using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
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
    public sealed partial class DockingManagerVisualStudio : SampleLayout
    {
        public DockingManagerVisualStudio()
        {
            this.InitializeComponent();
            if(AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                DockingBorder.Margin = new Thickness(0, 0, 0, 35);
            }
        }

        public override void Dispose()
        {
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                docking.Dispose();
                docking = null;
                base.Dispose();
            }
        }
    }
}
