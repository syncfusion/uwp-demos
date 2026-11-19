using Common;
using Syncfusion.UI.Xaml.SunburstChart;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfSunburstChart
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

        public override void Dispose()
        {
            if (this.Sunburst.DataContext is ViewModel) (this.Sunburst.DataContext as ViewModel).Dispose();
            if (this.Sunburst.ItemsSource != null) this.Sunburst.ClearValue(UI.Xaml.SunburstChart.SfSunburstChart.ItemsSourceProperty);
            this.Sunburst.Levels.Clear();
            this.Sunburst = null;
            this.Grid.Children.Clear();
            base.Dispose();
        }

        private void Palette_Loaded(object sender, RoutedEventArgs e)
        {
            var unUsedPalettes = new SunburstColorPalette[] { SunburstColorPalette.Custom };
            paletteCombo.ItemsSource = Enum.GetValues(typeof(SunburstColorPalette)).Cast<SunburstColorPalette>().Except(unUsedPalettes);
            paletteCombo.SelectedIndex = 11;
        }

        private void Palette_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var palette = (SunburstColorPalette)((ComboBox)sender).SelectedValue;
            if (Sunburst.Palette != palette) Sunburst.Palette = palette;
        }

        private void VisiblityMode_Loaded(object sender, RoutedEventArgs e)
        {
            visiblityMode.ItemsSource = Enum.GetValues(typeof(LabelOverflowMode));
            visiblityMode.SelectedIndex = 0;
        }

        private void VisiblityMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mode = (LabelOverflowMode)((ComboBox)sender).SelectedValue;
            if (Sunburst.DataLabelInfo.LabelOverflowMode != mode) Sunburst.DataLabelInfo.LabelOverflowMode = mode;
        }

        private void RotationMode_Loaded(object sender, RoutedEventArgs e)
        {
            rotationMode.ItemsSource = Enum.GetValues(typeof(LabelRotationMode));
            rotationMode.SelectedIndex = 0;
        }

        private void RotationMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mode = (LabelRotationMode)((ComboBox)sender).SelectedValue;
            if (Sunburst.DataLabelInfo.LabelRotationMode != mode) Sunburst.DataLabelInfo.LabelRotationMode = mode;
        }
    }
}
