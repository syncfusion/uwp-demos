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
    public sealed partial class Customization : SampleLayout
    {
        public Customization()
        {
            this.InitializeComponent();
        }

        private void paletteCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {                        
            var selectedValue = paletteCombo.SelectedItem.ToString();
            this.SmithChart.ColorModel.Palette = (ColorPalette)Enum.Parse(typeof(ColorPalette), selectedValue, true);
        }

        private void showDataLabel_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            this.SmithChart.Series[0].DataLabel.ShowLabel = (bool)checkBox.IsChecked;
            this.SmithChart.Series[1].DataLabel.ShowLabel = (bool)checkBox.IsChecked;
        }

        private void showHrzMinorGridLines_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            this.SmithChart.HorizontalAxis.ShowMinorGridlines = (bool)checkBox.IsChecked;
        }

        private void showRdlMinorGridLines_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            this.SmithChart.RadialAxis.ShowMinorGridlines = (bool)checkBox.IsChecked;
        }

        private void paletteCombo_Loaded(object sender, RoutedEventArgs e)
        {
            var paletteCombo = (sender as ComboBox);

            var paletteArray = new ColorPalette[]
            {
                ColorPalette.Metro ,
                ColorPalette.AutumnBrights,
                ColorPalette.FloraHues,
                ColorPalette.Pineapple,
                ColorPalette.TomatoSpectrum,
                ColorPalette.RedChrome,
                ColorPalette.BlueChrome,
                ColorPalette.PurpleChrome,
                ColorPalette.GreenChrome,
                ColorPalette.Elite,
                ColorPalette.LightCandy,
                ColorPalette.SandyBeach
            };

            paletteCombo.ItemsSource = paletteArray;
            paletteCombo.SelectedIndex = 0;
        }

        private void legendPosition_Loaded(object sender, RoutedEventArgs e)
        {
            Binding binding = new Binding();
            binding.Source = this.chartLegend;
            binding.Path = new PropertyPath("DockPosition");
            binding.Mode = BindingMode.TwoWay;
            legendPositionCombo.SetBinding(ComboBox.SelectedItemProperty, binding);
        }      
    }
}
