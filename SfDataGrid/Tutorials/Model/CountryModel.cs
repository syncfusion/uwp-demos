using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid
{
    /// <summary>
    /// This class represents the CountriesList
    /// </summary>
    public class CountriesList : INotifyPropertyChanged
    {
        #region Properties

        private string serialNumber;

        /// <summary>
        /// Gets or sets the Serial Number.
        /// </summary>
        /// <value>The SerialNumber.</value>
        public string SerialNumber
        {
            get
            {
                return serialNumber;
            }
            set
            {
                serialNumber = value;
                OnPropertyChanged("SerialNumber");
            }
        }

        private string countryName;

        /// <summary>
        /// Gets or sets the Country Name.
        /// </summary>
        /// <value>The CountryName.</value>
        public string Country
        {
            get
            {
                return countryName;
            }
            set
            {
                countryName = value;
                OnPropertyChanged("Country");
            }
        }

        private string countryCapital;

        /// <summary>
        /// Gets or sets the Capital.
        /// </summary>
        /// <value>The Capital.</value>
        public string Captial
        {
            get
            {
                return countryCapital;
            }
            set
            {
                countryCapital = value;
                OnPropertyChanged("Captial");
            }
        }

        private string officialName;

        /// <summary>
        /// Gets or sets the OfficialName.
        /// </summary>
        /// <value>The Official Name.</value>
        public string OfficialName
        {
            get
            {
                return officialName;
            }
            set
            {
                officialName = value;
                OnPropertyChanged("OfficialName");
            }
        }

        private string location;

        /// <summary>
        /// Gets or sets the Location.
        /// </summary>
        /// <value>The Location.</value>
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }

        private string language;

        /// <summary>
        /// Gets or sets the Language.
        /// </summary>
        /// <value>The Language.</value>
        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
                OnPropertyChanged("Language");
            }
        }

        private string currency;

        /// <summary>
        /// Gets or sets the Currency.
        /// </summary>
        /// <value>The Currency.</value>
        public string Currency
        {
            get
            {
                return currency;
            }
            set
            {
                currency = value;
                OnPropertyChanged("Currency");
            }
        }

        private long population;

        /// <summary>
        /// Gets or sets the Population.
        /// </summary>
        /// <value>The Population.</value>
        public long Population
        {
            get
            {
                return population;
            }
            set
            {
                population = value;
                OnPropertyChanged("Population");
            }
        }

        private double literacyRate;

        /// <summary>
        /// Gets or sets the LiteracyRate.
        /// </summary>
        /// <value>The Literacy Rate.</value>
        public double LiteracyRate
        {
            get
            {
                return literacyRate;
            }
            set
            {
                literacyRate = value;
                OnPropertyChanged("LiteracyRate");
            }
        }

        private ObservableCollection<EconomyGrowth> economyGrowth;
        /// <summary>
        /// Get or set the Economy Rate
        /// </summary>
        public ObservableCollection<EconomyGrowth> EconomyRate
        {
            get
            {
                return economyGrowth;
            }
            set
            {
                economyGrowth = value;
                OnPropertyChanged("EconomyRate");
            }
        }


        private double economyPercentage;

        /// <summary>
        /// Gets or sets the Economy Percentage.
        /// </summary>
        /// <value>The Economy Percentage.</value>
        public double EconomyPercentage 
        {
            get
            {
                return economyPercentage;
            }
            set
            {
                economyPercentage = value;
                OnPropertyChanged("EconomyPercentage");
            }
        }

        

        #endregion

        #region Ctor

        public CountriesList()
        {

        }

        public CountriesList(string _serialNumber, string _countryName, string _countryCapital, string _officialName, string _location, string _language, string _currency, long _Population, double _LiteracyRate, double _percentage)
        {
            this.SerialNumber = _serialNumber;
            this.Country = _countryName;
            this.Captial = _countryCapital;
            this.OfficialName = _officialName;
            this.Location = _location;
            this.Language = _language;
            this.Currency = _currency;
            this.Population = _Population;
            this.LiteracyRate = _LiteracyRate;
            this.EconomyPercentage=_percentage;
        }

        #endregion

        #region INotifyEventChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion
    }

    /// <summary>
    /// This class represents the PopulationGrowth
    /// </summary>
    public class PopulationGrowth : INotifyPropertyChanged
    {
        private string name;
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private long population;

        /// <summary>
        /// Gets or sets the Population.
        /// </summary>
        /// <value>The Population.</value>
        public long Population
        {
            get
            {
                return population;
            }
            set
            {
                population = value;
                OnPropertyChanged("Population");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }

    /// <summary>
    /// This class represents the EconomyGrowth
    /// </summary>
    public class EconomyGrowth : INotifyPropertyChanged
    {
        private int year;

        /// <summary>
        /// Gets or sets the Year.
        /// </summary>
        /// <value>The Year.</value>
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
                OnPropertyChanged("Year");
            }
        }

        private double growthPercentage;

        /// <summary>
        /// Gets or sets the GrowthPercentage.
        /// </summary>
        /// <value>The GrowthPercentage.</value>
        public double GrowthPercentage
        {
            get
            {
                return growthPercentage;
            }
            set
            {
                growthPercentage = value;
                OnPropertyChanged("GrowthPercentage");
            }
        }


        #region INotifyEventChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion
    }
}
