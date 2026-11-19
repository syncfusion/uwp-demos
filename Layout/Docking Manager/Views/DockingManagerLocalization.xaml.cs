using Common;
using Syncfusion.UI.Xaml.Controls.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Layout.DockingManager
{
    public sealed partial class DockingManagerLocalization:SampleLayout
    {
        string previouslanguage;
        public DockingManagerLocalization()
        {            
            InitializeComponent();
            InitializeImageSources();
            previouslanguage = Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride;
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "fr-FR";
            if (docking != null && docking.DocumentContainer != null)
                docking.DocumentContainer.SelectionChanged += DocumentContainer_SelectionChanged;
            if (Application.Current != null)
				Application.Current.Suspending += Current_Suspending;
        }

        /// <summary>
        /// Initializes image sources using PathHelper to support both standalone and Sample Browser contexts.
        /// </summary>
        private void InitializeImageSources()
        {
            // Background image
            BackgroundImage.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/background.jpg"), UriKind.RelativeOrAbsolute));
            
            // Weather icons for top section
            RainyIcon1.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/rainy.png"), UriKind.RelativeOrAbsolute));
            SunnyIcon1.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/sunny.png"), UriKind.RelativeOrAbsolute));
            
            // Weather report images (set based on selected document)
            WeatherToNight.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/rainy1.png"), UriKind.RelativeOrAbsolute));
            
            // City-specific weather images
            SunnyBeijing.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/sunny.png"), UriKind.RelativeOrAbsolute));
            SunnyMadagascar.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/sunny.png"), UriKind.RelativeOrAbsolute));
            CloudyLondon.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/cloudy.png"), UriKind.RelativeOrAbsolute));
            CloudyBrussels.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/cloudy.png"), UriKind.RelativeOrAbsolute));
            CloudyNewDelhi.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/cloudy.png"), UriKind.RelativeOrAbsolute));
            RainyNewYork.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/rainy.png"), UriKind.RelativeOrAbsolute));
            RainyChennai.Source = new BitmapImage(new Uri(PathHelper.GetResourcePath("Docking Manager/Assets/rainy.png"), UriKind.RelativeOrAbsolute));
        }

        private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = previouslanguage;
            previouslanguage = string.Empty;
        }

        private void DocumentContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "Pékin")
            {
                ConditionToNight.Text = "Dégagé";
                ConditionTomoNight.Text = "Nuages ​​épars";
                TemperatureToNight.Text = "29";
                TemperatureTomoNight.Text = "28";
                WeatherReport(PathHelper.GetResourcePath("Docking Manager/Assets/showerClear.png"), PathHelper.GetResourcePath("Docking Manager/Assets/Showers Late.jpg"));
            }
            else if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "Madagascar")
            {
                ConditionToNight.Text = "Averses modérées";
                ConditionTomoNight.Text = "Pluie";
                TemperatureToNight.Text = "22";
                TemperatureTomoNight.Text = "25";
                WeatherReport(PathHelper.GetResourcePath("Docking Manager/Assets/Rain.png"), PathHelper.GetResourcePath("Docking Manager/Assets/rainy1.png"));
            }
            else if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "New York")
            {
                ConditionToNight.Text = "Partiellement nuageux";
                ConditionTomoNight.Text = "Plutôt nuageux";
                TemperatureToNight.Text = "26";
                TemperatureTomoNight.Text = "27";
                WeatherReport(PathHelper.GetResourcePath("Docking Manager/Assets/Mostly Cloudy.png"), PathHelper.GetResourcePath("Docking Manager/Assets/Partly Cloudy.png"));
            }
            else if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "Londres")
            {
                ConditionToNight.Text = "Tonnerre";
                ConditionTomoNight.Text = "Orages";
                TemperatureToNight.Text = "20";
                TemperatureTomoNight.Text = "22";
                WeatherReport(PathHelper.GetResourcePath("Docking Manager/Assets/Scattered Thunderstorms.png"), PathHelper.GetResourcePath("Docking Manager/Assets/Thunderstorms.png"));
            }
            else if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "Bruxelles")
            {
                ConditionToNight.Text = "Pluie";
                ConditionTomoNight.Text = "Averses modérées";
                TemperatureToNight.Text = "26";
                TemperatureTomoNight.Text = "25";
                WeatherReport(PathHelper.GetResourcePath("Docking Manager/Assets/Rain.png"), PathHelper.GetResourcePath("Docking Manager/Assets/rainy1.png"));
            }
            else if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "New Delhi")
            {
                ConditionToNight.Text = "Partiellement nuageux";
                ConditionTomoNight.Text = "Plutôt nuageux";
                TemperatureToNight.Text = "23";
                TemperatureTomoNight.Text = "24";
                WeatherReport(PathHelper.GetResourcePath("Docking Manager/Assets/Mostly Cloudy.png"), PathHelper.GetResourcePath("Docking Manager/Assets/Partly Cloudy.png"));
            }
            else if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "Chennai")
            {
                ConditionToNight.Text = "Averses modérées";
                ConditionTomoNight.Text = "Dégagé";
                TemperatureToNight.Text = "23";
                TemperatureTomoNight.Text = "24";
                WeatherReport(PathHelper.GetResourcePath("Docking Manager/Assets/showerClear.png"), PathHelper.GetResourcePath("Docking Manager/Assets/rainy1.png"));
            }
        }

        private void WeatherReport(String WeatherTomoNightimgpath, String WeatherToNightimgpath)
        {
            BitmapImage WeatherTomoNightimg = new BitmapImage();
            WeatherTomoNightimg.UriSource = new Uri(WeatherTomoNightimgpath, UriKind.RelativeOrAbsolute);
            WeatherTomoNight.Source = WeatherTomoNightimg;
            BitmapImage WeatherToNightimg = new BitmapImage();
            WeatherToNightimg.UriSource = new Uri(WeatherToNightimgpath, UriKind.RelativeOrAbsolute);
            WeatherToNight.Source = WeatherToNightimg;
        }
        public override void Dispose()
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = previouslanguage;
            previouslanguage = string.Empty;
            if (docking != null && docking.DocumentContainer != null)
            {
                docking.DocumentContainer.SelectionChanged -= DocumentContainer_SelectionChanged;
                docking.Dispose();
                docking = null;
            }
            if (Application.Current != null)
                Application.Current.Suspending -= Current_Suspending;
            GC.Collect();
            base.Dispose();
        }
    }
    
}
