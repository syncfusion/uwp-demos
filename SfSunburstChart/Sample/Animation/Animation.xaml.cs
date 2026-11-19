using Common;
using Syncfusion.UI.Xaml.SunburstChart;
using System;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfSunburstChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Animation : SampleLayout
    {
        public Animation()
        {
            this.InitializeComponent();

            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                tooltipBehavior.VerticalOffset = 25;
            }
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

        private void AnimationTypes_Loaded(object sender, RoutedEventArgs e)
        {
            animationTypes.ItemsSource = Enum.GetValues(typeof(AnimationType));
            animationTypes.SelectedIndex = 1;
        }

        private void AnimationTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var animationType = (AnimationType) ((ComboBox) sender).SelectedValue;
            if (Sunburst.AnimationType != animationType) Sunburst.AnimationType = animationType;
        }
    }

    public class ToolTipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Math.Round((double)value / 1000000) + " M";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
