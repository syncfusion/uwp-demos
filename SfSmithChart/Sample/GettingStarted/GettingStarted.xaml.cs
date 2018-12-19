using Common;
using Syncfusion.UI.Xaml.SmithChart;
using System;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfSmithChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GettingStarted : SampleLayout
    {
        public GettingStarted()
        {
            this.InitializeComponent();
        }

        private void renderingTypeCombo_Loaded(object sender, RoutedEventArgs e)
        {
            this.renderingTypeCombo.ItemsSource = Enum.GetValues(typeof(RenderingType));
            this.renderingTypeCombo.SelectedIndex = 0;
        }
    }
}
