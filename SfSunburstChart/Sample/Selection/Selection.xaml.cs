using Common;
using Syncfusion.UI.Xaml.SunburstChart;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfSunburstChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Selection : SampleLayout
    {
        public Selection()
        {
            this.InitializeComponent();
        }

        public override void Dispose()
        {
            if (this.Sunburst.DataContext is ViewModel) (this.Sunburst.DataContext as ViewModel).Dispose();
            if (this.Sunburst.ItemsSource != null) this.Sunburst.ClearValue(UI.Xaml.SunburstChart.SfSunburstChart.ItemsSourceProperty);
            this.Sunburst.Behaviors.Clear();
            this.Sunburst.Levels.Clear();
            this.Sunburst = null;
            this.Grid.Children.Clear();
            base.Dispose();
        }

        private void SelectionMode_Loaded(object sender, RoutedEventArgs e)
        {
            selectionMode.ItemsSource = Enum.GetValues(typeof(UI.Xaml.SunburstChart.SelectionMode));
            selectionMode.SelectedIndex = 1;
        }

        private void SelecitonMode_Changed(object sender, SelectionChangedEventArgs e)
        {
            var mode = (UI.Xaml.SunburstChart.SelectionMode)((ComboBox)sender).SelectedValue;
            if (selectionBehav.SelectionMode != mode) selectionBehav.SelectionMode = mode;
        }

        private void SelectionType_Loaded(object sender, RoutedEventArgs e)
        {
            selectiontype.ItemsSource = Enum.GetValues(typeof(SelectionType));
            selectiontype.SelectedIndex = 0;
        }

        private void SelecitonType_Changed(object sender, SelectionChangedEventArgs e)
        {
            var type = (SelectionType)((ComboBox)sender).SelectedValue;
            if (selectionBehav.SelectionType != type) selectionBehav.SelectionType = type;
        }

        private void SelectionHighlightBy_Loaded(object sender, RoutedEventArgs e)
        {
            selectionHighlightBy.ItemsSource = Enum.GetValues(typeof(SelectionDisplayMode));
            selectionHighlightBy.SelectedIndex = 1;
        }

        private void SelectionHighlightBy_Changed(object sender, SelectionChangedEventArgs e)
        {
            var mode = (SelectionDisplayMode)((ComboBox)sender).SelectedValue;
            if (selectionBehav.SelectionDisplayMode != mode) selectionBehav.SelectionDisplayMode = mode;
        }
    }
}
