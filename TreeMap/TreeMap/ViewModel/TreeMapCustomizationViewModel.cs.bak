using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TreeMapWinRTSamples
{
    public class OlymicMedalsViewModel : IDisposable
    {
        public ObservableCollection<OlympicMedals> OlympicMedalsDetails { get; set; }

        public OlymicMedalsViewModel()
        {
            this.OlympicMedalsDetails = new ObservableCollection<OlympicMedals>();
            this.OlympicMedalsDetails.Add(new OlympicMedals { Country = "US", GameName = "Swimming", GoldMedals = 16, SilverMedals = 9, BronzeMedals = 6, TotalMedals = 31, GameImgSource = new BitmapImage(new Uri("ms-appx:/TreeMap/Assets/Swimming.png")) });
            this.OlympicMedalsDetails.Add(new OlympicMedals { Country = "US", GameName = "Track and Field", GoldMedals = 9, SilverMedals = 13, BronzeMedals = 7, TotalMedals = 29, GameImgSource = new BitmapImage(new Uri("ms-appx:/TreeMap/Assets/TrackAndField.png")) });
            this.OlympicMedalsDetails.Add(new OlympicMedals { Country = "US", GameName = "Gymnastics", GoldMedals = 3, SilverMedals = 1, BronzeMedals = 2, TotalMedals = 6, GameImgSource = new BitmapImage(new Uri("ms-appx:/TreeMap/Assets/Gymnastics.png")) });
            this.OlympicMedalsDetails.Add(new OlympicMedals { Country = "US", GameName = "Boxing", GoldMedals = 1, SilverMedals = 0, BronzeMedals = 1, TotalMedals = 2, GameImgSource = new BitmapImage(new Uri("ms-appx:/TreeMap/Assets/Boxing.png")) });
            this.OlympicMedalsDetails.Add(new OlympicMedals { Country = "US", GameName = "Cycling", GoldMedals = 1, SilverMedals = 2, BronzeMedals = 1, TotalMedals = 4, GameImgSource = new BitmapImage(new Uri("ms-appx:/TreeMap/Assets/Cycling.png")) });
            this.OlympicMedalsDetails.Add(new OlympicMedals { Country = "US", GameName = "Shooting", GoldMedals = 3, SilverMedals = 0, BronzeMedals = 1, TotalMedals = 4, GameImgSource = new BitmapImage(new Uri("ms-appx:/TreeMap/Assets/Shooting.png")) });
            this.OlympicMedalsDetails.Add(new OlympicMedals { Country = "US", GameName = "Wrestling", GoldMedals = 2, SilverMedals = 0, BronzeMedals = 2, TotalMedals = 4, GameImgSource = new BitmapImage(new Uri("ms-appx:/TreeMap/Assets/Wrestling.png")) });
            this.OlympicMedalsDetails.Add(new OlympicMedals { Country = "US", GameName = "Archery", GoldMedals = 0, SilverMedals = 1, BronzeMedals = 0, TotalMedals = 1, GameImgSource = new BitmapImage(new Uri("ms-appx:/TreeMap/Assets/Archery.png")) });
            this.OlympicMedalsDetails.Add(new OlympicMedals { Country = "US", GameName = "Soccer", GoldMedals = 1, SilverMedals = 0, BronzeMedals = 0, TotalMedals = 1, GameImgSource = new BitmapImage(new Uri("ms-appx:/TreeMap/Assets/Soccer.png")) });
            this.OlympicMedalsDetails.Add(new OlympicMedals { Country = "US", GameName = "Diving", GoldMedals = 1, SilverMedals = 1, BronzeMedals = 2, TotalMedals = 4, GameImgSource = new BitmapImage(new Uri("ms-appx:/TreeMap/Assets/Diving.png")) });
        }

        public void Dispose()
        {
            this.OlympicMedalsDetails.Clear();
        }
    }

    public class OlympicMedals
    {
        public string Country { get; set; }
        public string GameName { get; set; }
        public double GoldMedals { get; set; }
        public double SilverMedals { get; set; }
        public double BronzeMedals { get; set; }
        public double TotalMedals { get; set; }
        public ImageSource GameImgSource { get; set; }
    }
}
