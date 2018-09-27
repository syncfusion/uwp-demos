using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.Charts;
using Windows.System.Profile;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public sealed partial class Palettes : SampleLayout
    {
        public Palettes()
        {
            this.InitializeComponent();
            palettecombo.Loaded += OnPaletteCombo_Loaded;
        }

        void OnPaletteCombo_Loaded(object sender, RoutedEventArgs e)
        {
            palettecombo.SelectedIndex = 0;
        }

        void OnPaletteCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                        barChart.Palette = ChartColorPalette.Metro;
                    doughnutChart.Series[0].Palette = ChartColorPalette.Metro;
                    stackedChart.Palette = ChartColorPalette.Metro;
                    break;
                case 1:
                    if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                        barChart.Palette = ChartColorPalette.AutumnBrights;
                    doughnutChart.Series[0].Palette = ChartColorPalette.AutumnBrights;
                    stackedChart.Palette = ChartColorPalette.AutumnBrights;
                    break;
                case 2:
                    if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                        barChart.Palette = ChartColorPalette.FloraHues;
                    doughnutChart.Series[0].Palette = ChartColorPalette.FloraHues;
                    stackedChart.Palette = ChartColorPalette.FloraHues;
                    break;
                case 3:
                    if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                        barChart.Palette = ChartColorPalette.Pineapple;
                    doughnutChart.Series[0].Palette = ChartColorPalette.Pineapple;
                    stackedChart.Palette = ChartColorPalette.Pineapple;
                    break;
                case 4:
                    if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                        barChart.Palette = ChartColorPalette.TomatoSpectrum;
                    doughnutChart.Series[0].Palette = ChartColorPalette.TomatoSpectrum;
                    stackedChart.Palette = ChartColorPalette.TomatoSpectrum;
                    break;
                case 5:
                    if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                        barChart.Palette = ChartColorPalette.RedChrome;
                    doughnutChart.Series[0].Palette = ChartColorPalette.RedChrome;
                    stackedChart.Palette = ChartColorPalette.RedChrome;
                    break;
                case 6:
                    if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                        barChart.Palette = ChartColorPalette.BlueChrome;
                    doughnutChart.Series[0].Palette = ChartColorPalette.BlueChrome;
                    stackedChart.Palette = ChartColorPalette.BlueChrome;
                    break;
                case 7:
                    if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                        barChart.Palette = ChartColorPalette.PurpleChrome;
                    doughnutChart.Series[0].Palette = ChartColorPalette.PurpleChrome;
                    stackedChart.Palette = ChartColorPalette.PurpleChrome;
                    break;
                case 8:
                    if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                        barChart.Palette = ChartColorPalette.GreenChrome;
                    doughnutChart.Series[0].Palette = ChartColorPalette.GreenChrome;
                    stackedChart.Palette = ChartColorPalette.GreenChrome;
                    break;
                case 9:
                    if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                        barChart.Palette = ChartColorPalette.Elite;
                    doughnutChart.Series[0].Palette = ChartColorPalette.Elite;
                    stackedChart.Palette = ChartColorPalette.Elite;
                    break;
                case 10:
                    if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                        barChart.Palette = ChartColorPalette.LightCandy;
                    doughnutChart.Series[0].Palette = ChartColorPalette.LightCandy;
                    stackedChart.Palette = ChartColorPalette.LightCandy;
                    break;
                case 11:
                    if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                        barChart.Palette = ChartColorPalette.SandyBeach;
                    doughnutChart.Series[0].Palette = ChartColorPalette.SandyBeach;
                    stackedChart.Palette = ChartColorPalette.SandyBeach;
                    break;
            }
        }

        public override void Dispose()
        {
            if ((this.DataContext as PaletteViewModel) != null)
                (this.DataContext as PaletteViewModel).Dispose();

            if (this.barChart != null)
            {
                foreach (var series in this.barChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.barChart = null;
            }

            if (this.doughnutChart != null)
            {
                foreach (var series in this.doughnutChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.doughnutChart = null;
            }

            if (this.stackedChart != null)
            {
                foreach (var series in this.stackedChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.stackedChart = null;
            }

            if (this.grid.DataContext != null)
                this.grid.DataContext = null;
            this.grid.Resources = null;

            base.Dispose();
        }
    }
    public class PaletteModel
    {
        public string SocialSite
        {
               get;
            set;
        }
        public double UsersCount
        {
            get;
            set;
        }
        public double Year2012
        {
            get;
            set;
        }
        public double Year2014
        {
            get;
            set;
        }
        public double Year2015
        {
            get;
            set;
        }
        public string Country
        {
            get;
            set;
        }

    }
    public class PaletteViewModel : IDisposable
    {
        public ObservableCollection<ChartColorPalette> PaletteList
        {
            get;
            set;
        }
        public ObservableCollection<PaletteModel> FacebookStatistics
        {
            get;
            set;
        }
        public ObservableCollection<PaletteModel> ActiveUsers
        {
            get;
            set;
        }
        public ObservableCollection<PaletteModel> RegisteredUsers
        {
            get;
            set;
        }
        public ObservableCollection<PaletteModel> Users
        {
            get;
            set;
        }

        public PaletteViewModel()
        {
            this.PaletteList = new ObservableCollection<ChartColorPalette>();
            PaletteList.Add(ChartColorPalette.Metro);
            PaletteList.Add(ChartColorPalette.AutumnBrights);
            PaletteList.Add(ChartColorPalette.FloraHues);
            PaletteList.Add(ChartColorPalette.Pineapple);
            PaletteList.Add(ChartColorPalette.TomatoSpectrum);
            PaletteList.Add(ChartColorPalette.RedChrome);
            PaletteList.Add(ChartColorPalette.BlueChrome);
            PaletteList.Add(ChartColorPalette.PurpleChrome);
            PaletteList.Add(ChartColorPalette.GreenChrome);
            PaletteList.Add(ChartColorPalette.Elite);
            PaletteList.Add(ChartColorPalette.LightCandy);
            PaletteList.Add(ChartColorPalette.SandyBeach);

            //PieSeries
            this.FacebookStatistics = new ObservableCollection<PaletteModel>();
            FacebookStatistics.Add(new PaletteModel { Country = "Finland", UsersCount = 12.68 });
            FacebookStatistics.Add(new PaletteModel { Country = "Germany", UsersCount = 10.59 });
            FacebookStatistics.Add(new PaletteModel { Country = "Poland", UsersCount = 11.16 });
            FacebookStatistics.Add(new PaletteModel { Country = "France", UsersCount = 10.48 });
            FacebookStatistics.Add(new PaletteModel { Country = "Australia", UsersCount = 10.13 });
            FacebookStatistics.Add(new PaletteModel { Country = "Brazil", UsersCount = 7.99 });
            FacebookStatistics.Add(new PaletteModel { Country = "Switzerland", UsersCount = 7.25 });
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                FacebookStatistics.Add(new PaletteModel { Country = "Russia", UsersCount = 6.25 });
                FacebookStatistics.Add(new PaletteModel { Country = "Israel", UsersCount = 10.50 });
            }

            //Active User in Year of 2012,2014,2015
            this.Users = new ObservableCollection<PaletteModel>();
            Users.Add(new PaletteModel { SocialSite = "Facebook", Year2012 = 788, Year2014 = 1440, Year2015 = 1336 });
            Users.Add(new PaletteModel { SocialSite = "QZone", Year2012 = 210, Year2014 = 632, Year2015 = 629 });
            Users.Add(new PaletteModel { SocialSite = "WhatsApp", Year2012 = 800, Year2014 = 800, Year2015 = 600 });
            Users.Add(new PaletteModel { SocialSite = "WeChat", Year2012 = 668, Year2014 = 668, Year2015 = 468 });
            Users.Add(new PaletteModel { SocialSite = "Google+", Year2012 = 549, Year2014 = 549, Year2015 = 343 });
            Users.Add(new PaletteModel { SocialSite = "Twitter", Year2012 = 540, Year2014 = 540, Year2015 = 284 });
            Users.Add(new PaletteModel { SocialSite = "Instagram", Year2012 = 302, Year2014 = 302, Year2015 = 300 });
            Users.Add(new PaletteModel { SocialSite = "Skype", Year2012 = 300, Year2014 = 300, Year2015 = 300 });

            //Active Users
            this.ActiveUsers = new ObservableCollection<PaletteModel>();
            ActiveUsers.Add(new PaletteModel { SocialSite = "Twitter", UsersCount = 302 });
            ActiveUsers.Add(new PaletteModel { SocialSite = "WeChat", UsersCount = 559 });
            ActiveUsers.Add(new PaletteModel { SocialSite = "QZone", UsersCount = 668 });
            ActiveUsers.Add(new PaletteModel { SocialSite = "Skype", UsersCount = 300 });
            
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                ActiveUsers.Add(new PaletteModel { SocialSite = "Instagram", UsersCount = 300 });
                ActiveUsers.Add(new PaletteModel { SocialSite = "Google+", UsersCount = 540 });
                ActiveUsers.Add(new PaletteModel { SocialSite = "WhatsApp", UsersCount = 800 });
            }
            ActiveUsers.Add(new PaletteModel { SocialSite = "Facebook", UsersCount = 1184 });

            // Registered users
            this.RegisteredUsers = new ObservableCollection<PaletteModel>();
            RegisteredUsers.Add(new PaletteModel { SocialSite = "Twitter", UsersCount = 500 });
            RegisteredUsers.Add(new PaletteModel { SocialSite = "WeChat", UsersCount = 1120 });
            RegisteredUsers.Add(new PaletteModel { SocialSite = "Skype", UsersCount = 663 });
            RegisteredUsers.Add(new PaletteModel { SocialSite = "QZone", UsersCount = 1000 });
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                RegisteredUsers.Add(new PaletteModel { SocialSite = "Google+", UsersCount = 540 });
                RegisteredUsers.Add(new PaletteModel { SocialSite = "Instagram", UsersCount = 300 });
                RegisteredUsers.Add(new PaletteModel { SocialSite = "WhatsApp", UsersCount = 800 });
            }
            RegisteredUsers.Add(new PaletteModel { SocialSite = "Facebook", UsersCount = 2000 });
        }

        public void Dispose()
        {
            if (this.PaletteList != null)
                this.PaletteList.Clear();
            if (this.FacebookStatistics != null)
                this.FacebookStatistics.Clear();
            if (this.ActiveUsers != null)
                this.ActiveUsers.Clear();
            if (this.RegisteredUsers != null)
                this.RegisteredUsers.Clear();
            if (this.Users != null)
                this.Users.Clear();
        }
    }
}
