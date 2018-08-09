using Syncfusion.UI.Xaml.Maps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace MapControlUWP_Samples
{
    public class Country : INotifyPropertyChanged, IDisposable
    {
        public string NAME { get; set; }

        private Visibility itemsvisibility = Visibility.Visible;
        public Visibility ItemsVisibility
        {
            get { return itemsvisibility; }
            set { itemsvisibility = value; }
        }

        private double weather { get; set; }
        public double Weather
        {
            get
            {
                return weather;
            }
            set
            {
                weather = value;
            }
        }

        private double population { get; set; }
        public double Population
        {
            get
            {
                return population;
            }
            set
            {
                population = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Population"));
            }

        }
        public string PopulationFormat { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            this.PopulationFormat = (String.Format("{0:0,0}", this.Population).Trim('$'));
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }

    public class Model
    {
        public string Name { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        
    }

    public class Countries
    {
        public Countries()
        {
        }
        #region Properties
        public string Continent { get; set; }
        public string Country { get; set; }
        
        public ShapeProperty ShapeCollection { get; set; }
        #endregion
    }

    public class Continents
    {
        public string NAME { get; set; }
    }

    public class ShapeProperty
    {
        public ShapeProperty()
        {
        }

        public String Country { get; set; }
        public String Uri { get; set; }
        public String ShapeFill { get; set; }
    }
}
