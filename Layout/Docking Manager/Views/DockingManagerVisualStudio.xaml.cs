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
using Windows.UI.Xaml.Media.Imaging;
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
            this.DataContext = this;
            this.InitializeComponent();
            InitializeImageSources();
            if(AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                DockingBorder.Margin = new Thickness(0, 0, 0, 35);
            }
        }

        /// <summary>
        /// Initializes image sources using PathHelper to support both standalone and Sample Browser contexts.
        /// </summary>
        private void InitializeImageSources()
        {
            // Icon images for dock windows
            OutputIcon.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/Output.png"), UriKind.RelativeOrAbsolute));
            FindResultsIcon.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/FindResults.png"), UriKind.RelativeOrAbsolute));
            ErrorListIcon.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/ErrorList.png"), UriKind.RelativeOrAbsolute));
            ErrorIcon.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/Error.png"), UriKind.RelativeOrAbsolute));
            WarningIcon.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/Warning.png"), UriKind.RelativeOrAbsolute));
            MessageIcon.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/Message.png"), UriKind.RelativeOrAbsolute));
            FindSymbolResultsIcon.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/FindSymbolResults.png"), UriKind.RelativeOrAbsolute));
            SolutionIcon.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/Solution.png"), UriKind.RelativeOrAbsolute));
            ClassViewIcon.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/ClassView.png"), UriKind.RelativeOrAbsolute));
            PropertiesIcon.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/Properties.png"), UriKind.RelativeOrAbsolute));
            ToolboxIcon.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/Toolbox.png"), UriKind.RelativeOrAbsolute));

            // Content images for dock window content areas
            SolutionExplorerImage.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/Images/SolutionExplorer.png"), UriKind.RelativeOrAbsolute));
            ClassViewImage.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/Images/ClassView.png"), UriKind.RelativeOrAbsolute));
            StackpanelImage.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/Images/Stackpanel.png"), UriKind.RelativeOrAbsolute));
            ToolboxImage.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/Images/Toolbox.png"), UriKind.RelativeOrAbsolute));
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
