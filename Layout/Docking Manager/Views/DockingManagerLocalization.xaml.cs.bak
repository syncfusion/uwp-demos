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
            previouslanguage = Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride;
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "fr-FR";
            if (docking != null && docking.DocumentContainer != null)
                docking.DocumentContainer.SelectionChanged += DocumentContainer_SelectionChanged;
            if (Application.Current != null)
				Application.Current.Suspending += Current_Suspending;
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
                ConditionToNight.Text = "dégagé";
                ConditionTomoNight.Text = "Nuages ​​épars";
                TemperatureToNight.Text = "29";
                TemperatureTomoNight.Text = "28";
                WeatherReport("ms-appx:///Docking Manager/Assets/showerClear.png", "ms-appx:///Docking Manager/Assets/Showers Late.jpg");
            }
            else if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "Madagascar")
            {
                ConditionToNight.Text = "Averses modérées";
                ConditionTomoNight.Text = "Pluie";
                TemperatureToNight.Text = "22";
                TemperatureTomoNight.Text = "25";
                WeatherReport("ms-appx:///Docking Manager/Assets/Rain.png", "ms-appx:///Docking Manager/Assets/rainy1.png");
            }
            else if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "New York")
            {
                ConditionToNight.Text = "Partiellement nuageux";
                ConditionTomoNight.Text = "Plutôt nuageux";
                TemperatureToNight.Text = "26";
                TemperatureTomoNight.Text = "27";
                WeatherReport("ms-appx:///Docking Manager/Assets/Mostly Cloudy.png", "ms-appx:///Docking Manager/Assets/Partly Cloudy.png");
            }
            else if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "Londres")
            {
                ConditionToNight.Text = "Tonnerre";
                ConditionTomoNight.Text = "Des orages";
                TemperatureToNight.Text = "20";
                TemperatureTomoNight.Text = "22";
                WeatherReport("ms-appx:///Docking Manager/Assets/Scattered Thunderstorms.png", "ms-appx:///Docking Manager/Assets/Thunderstorms.png");
            }
            else if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "Bruxelles")
            {
                ConditionToNight.Text = "Pluie";
                ConditionTomoNight.Text = "Averses modérées";
                TemperatureToNight.Text = "26";
                TemperatureTomoNight.Text = "25";
                WeatherReport("ms-appx:///Docking Manager/Assets/Rain.png", "ms-appx:///Docking Manager/Assets/rainy1.png");
            }
            else if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "New Delhi")
            {
                ConditionToNight.Text = "Partiellement nuageux";
                ConditionTomoNight.Text = "Plutôt nuageux";
                TemperatureToNight.Text = "23";
                TemperatureTomoNight.Text = "24";
                WeatherReport("ms-appx:///Docking Manager/Assets/Mostly Cloudy.png", "ms-appx:///Docking Manager/Assets/Partly Cloudy.png");
            }
            else if (docking.DocumentContainer.SelectedItem != null && (docking.DocumentContainer.SelectedItem as DocumentTabItem).Header.ToString() == "Chennai")
            {
                ConditionToNight.Text = "Averses modérées";
                ConditionTomoNight.Text = "dégagé";
                TemperatureToNight.Text = "23";
                TemperatureTomoNight.Text = "24";
                WeatherReport("ms-appx:///Docking Manager/Assets/showerClear.png", "ms-appx:///Docking Manager/Assets/rainy1.png");
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
