using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MapControlUWP_Samples
{
    public class MapViewModel : IDisposable
    {
        public ObservableCollection<Country> Countries { get; set; }
        public ObservableCollection<Countries> CountriesList { get; set; }
        public ObservableCollection<Country> Countries1 { get; set; }
        public ObservableCollection<Model> Models { get; set; }
        public ObservableCollection<Country> MuliLayerList { get; set; }
        public ObservableCollection<Country> AfricaList { get; set; }
        public ObservableCollection<Country> OceaniaList { get; set; }
        public ObservableCollection<Continents> ContinentList         
        {
            get;
            set;
        }

        private ObservableCollection<ElectionData> electionResults;
        public ObservableCollection<ElectionData> ElectionResults
        {
            get { return electionResults; }
            set { electionResults = value; }
        } 

        public MapViewModel()
        {
            Countries = new ObservableCollection<Country>();
            Countries = GetCountriesAndPopulation();
            Countries1 = GetCountriesAndPopulationForBubbles();
            ContinentList = GetContinents();
            CountriesList = GetCountries();
            ElectionResults = GetElectionResults();
            this.Models = new ObservableCollection<Model>();
            this.OceaniaList = new ObservableCollection<Country>();
            this.OceaniaList.Add(new Country() { NAME = "New South Wales", Weather = 26 });
            this.OceaniaList.Add(new Country() { NAME = "Queensland", Weather = 30 });
            this.OceaniaList.Add(new Country() { NAME = "Tasmania", Weather = 21 });
            this.OceaniaList.Add(new Country() { NAME = "Western Australia", Weather = 32 });
            this.AfricaList = new ObservableCollection<Country>();
            this.AfricaList.Add(new Country() { NAME = "Algeria", Weather = 47 });
            this.AfricaList.Add(new Country() { NAME = "Congo (Brazzaville)", Weather = 45 });
            this.AfricaList.Add(new Country() { NAME = "Ethiopia", Weather=50 });
            this.AfricaList.Add(new Country() { NAME = "South Africa", Weather = 30 });
            this.MuliLayerList = new ObservableCollection<Country>();
            this.MuliLayerList.Add(new Country() { NAME = "Asia", Weather = 40 });
            this.MuliLayerList.Add(new Country() { NAME = "South America", Weather = 45 });
            this.MuliLayerList.Add(new Country() { NAME = "North America", Weather = 52 });
            this.MuliLayerList.Add(new Country() { NAME = "Antarctica",ItemsVisibility=Visibility.Collapsed });
            this.MuliLayerList.Add(new Country() { NAME = "Oceania", ItemsVisibility = Visibility.Collapsed });
            this.MuliLayerList.Add(new Country() { NAME = "Europe", ItemsVisibility = Visibility.Collapsed });
            this.MuliLayerList.Add(new Country() { NAME = "Africa", ItemsVisibility = Visibility.Collapsed });
            this.Models.Add(new Model() { Name = "USA ", Latitude = "38.8833N", Longitude = "77.0167W" });
            this.Models.Add(new Model() { Name = "Brazil ", Latitude = "15.7833S", Longitude = "47.8667W" });
            this.Models.Add(new Model() { Name = "India ", Latitude = "21.0000N", Longitude = "78.0000E" });
            this.Models.Add(new Model() { Name = "China ", Latitude = "35.0000N", Longitude = "103.0000E" });
            this.Models.Add(new Model() { Name = "Indonesia ", Latitude = "6.1750S", Longitude = "106.8283E" });

        }

        public void Dispose()
        {
            this.AfricaList.Clear();
            this.ContinentList.Clear();
            this.Countries.Clear();
            this.Countries1.Clear();
            this.CountriesList.Clear();
            this.ElectionResults.Clear();
            this.Models.Clear();
            this.MuliLayerList.Clear();
            this.OceaniaList.Clear();
        }

        public static ObservableCollection<Countries> GetCountries()
        {
            ObservableCollection<Countries> countriesList = new ObservableCollection<Countries>();
            countriesList.Add(new Countries() { Country = "Afghanistan", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Angola", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Albania", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "United Arab Emirates", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Argentina", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "Armenia", Continent = "Europe" });
            //countriesList.Add(new Countries() { Country = "Antarctica", Continent = "Antarctica" });
            //countriesList.Add(new Countries() { Country = "Fr. S. and Antarctic Lands", Continent = "Antarctica" });
            countriesList.Add(new Countries() { Country = "Australia", Continent = "Oceania" });
            countriesList.Add(new Countries() { Country = "Austria", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Azerbaijan", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Burundi", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Belgium", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Benin", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Burkina Faso", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Bangladesh", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Bulgaria", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Bahamas", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Bosnia and Herz.", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Belarus", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Belize", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Bolivia", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "Brazil", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "Brunei", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Bhutan", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Botswana", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Central African Rep.", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Canada", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Switzerland", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Chile", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "China", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Ivory Coast", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Cameroon", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Congo (Kinshasa)", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Congo (Brazzaville)", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Colombia", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "Costa Rica", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Cuba", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "N. Cyprus", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Cyprus", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Czech Rep.", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Germany", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Djibouti", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Denmark", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Dominican Rep.", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Algeria", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Ecuador", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "Egypt", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Eritrea", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Spain", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Estonia", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Ethiopia", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Finland", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Fiji", Continent = "Oceania" });
            countriesList.Add(new Countries() { Country = "Falkland Is.", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "France", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Gabon", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "United Kingdom", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Georgia", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Ghana", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Guinea", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Gambia", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Guinea Bissau", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Eq. Guinea", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Greece", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Greenland", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Guatemala", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Guyana", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "Honduras", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Croatia", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Haiti", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Hungary", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Indonesia", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "India", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Ireland", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Iran", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Iraq", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Iceland", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Israel", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Italy", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Jamaica", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Jordan", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Japan", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Kazakhstan", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Kenya", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Kyrgyzstan", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Cambodia", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "S. Korea", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Kosovo", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Kuwait", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Laos", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Lebanon", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Liberia", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Libya", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Sri Lanka", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Lesotho", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Lithuania", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Luxembourg", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Latvia", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Morocco", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Moldova", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Madagascar", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Mexico", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Macedonia", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Mali", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Myanmar", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Montenegro", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Mongolia", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Mozambique", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Mauritania", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Malawi", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Malaysia", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Namibia", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "New Caledonia", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Niger", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Nigeria", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Nicaragua", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Netherlands", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Norway", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Nepal", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "New Zealand", Continent = "Oceania" });
            countriesList.Add(new Countries() { Country = "Oman", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Pakistan", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Panama", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Peru", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "Philippines", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Papua New Guinea", Continent = "Oceania" });
            countriesList.Add(new Countries() { Country = "Poland", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Puerto Rico", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "N. Korea", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Portugal", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Paraguay", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "Qatar", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Romania", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Russia", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Rwanda", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "W. Sahara", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Saudi Arabia", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Sudan", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "S. Sudan", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Senegal", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Solomon Is.", Continent = "Oceania" });
            countriesList.Add(new Countries() { Country = "Sierra Leone", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "El Salvador", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Somaliland", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Somalia", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Serbia", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Suriname", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "Slovakia", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Slovenia", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Sweden", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Swaziland", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Syria", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Chad", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Togo", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Thailand", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Tajikistan", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Turkmenistan", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "East Timor", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Trinidad and Tobago", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Tunisia", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Turkey", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Taiwan", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Tanzania", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Uganda", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Ukraine", Continent = "Europe" });
            countriesList.Add(new Countries() { Country = "Uruguay", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "United States", Continent = "North America" });
            countriesList.Add(new Countries() { Country = "Uzbekistan", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Venezuela", Continent = "South America" });
            countriesList.Add(new Countries() { Country = "Vietnam", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Vanuatu", Continent = "Oceania" });
            countriesList.Add(new Countries() { Country = "West Bank", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "Yemen", Continent = "Asia" });
            countriesList.Add(new Countries() { Country = "South Africa", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Zambia", Continent = "Africa" });
            countriesList.Add(new Countries() { Country = "Zimbabwe", Continent = "Africa" });

            return countriesList;
        }
        private ObservableCollection<Continents> GetContinents()
        {
            ObservableCollection<Continents> ContinentList = new ObservableCollection<Continents>();
            ContinentList.Add(new Continents { NAME = "Asia" });
            ContinentList.Add(new Continents { NAME = "Africa" });
            ContinentList.Add(new Continents { NAME = "Europe" });
            ContinentList.Add(new Continents { NAME = "Antarctica" });
            ContinentList.Add(new Continents { NAME = "North America" });
            ContinentList.Add(new Continents { NAME = "South America" });
            ContinentList.Add(new Continents { NAME = "Oceania" });
            return ContinentList;
        }
       
        private ObservableCollection<Country> GetCountriesAndPopulation()
        {
            ObservableCollection<Country> countries = new ObservableCollection<Country>();
            countries.Add(new Country() { NAME = "Kazakhstan", Population = 3706690 });
            countries.Add(new Country() { NAME = "India", Population = 1210193422 });
            countries.Add(new Country() { NAME = "United States", Population = 314623000 });
            countries.Add(new Country() { NAME = "Australia", Population = 22789701 });
            countries.Add(new Country() { NAME = "Mexico", Population = 112336538 });
            countries.Add(new Country() { NAME = "Russia", Population = 143228300 });
            countries.Add(new Country() { NAME = "Canada", Population = 34955100 });
            countries.Add(new Country() { NAME = "Brazil", Population = 193946886 });
            countries.Add(new Country() { NAME = "Algeria", Population = 37100000 });
            return countries;
        }
        private ObservableCollection<Country> GetCountriesAndPopulationForBubbles()
        {
            ObservableCollection<Country> countries = new ObservableCollection<Country>();
            countries.Add(new Country() { NAME = "China", Population = 1347350000 });
            countries.Add(new Country() { NAME = "United States", Population = 314623000 });
            countries.Add(new Country() { NAME = "Australia", Population = 22789701 });
            countries.Add(new Country() { NAME = "Russia", Population = 143228300 });
            countries.Add(new Country() { NAME = "Egypt", Population = 82724000 });
            countries.Add(new Country() { NAME = "South Africa", Population = 50586757 });
            return countries;
        }

        private ObservableCollection<ElectionData> GetElectionResults()
        {
            ObservableCollection<ElectionData> electionResults = new ObservableCollection<ElectionData>
            {
                new ElectionData {State = "Alabama", Candidate = "Romney", Electors = 9 },
                new ElectionData { State = "Alaska", Candidate = "Romney", Electors = 3 },
                new ElectionData { State = "Arizona", Candidate = "Romney", Electors = 11 },
                new ElectionData { State = "Arkansas", Candidate = "Romney", Electors = 6 },
                new ElectionData { State = "California", Candidate = "Obama", Electors = 55 },
                new ElectionData { State = "Colorado", Candidate = "Obama", Electors = 9 },
                new ElectionData { State = "Connecticut", Candidate = "Obama", Electors = 7 },
                new ElectionData { State = "Delaware", Candidate = "Obama", Electors = 3 },
                new ElectionData { State = "District of Columbia", Candidate = "Obama", Electors = 3 },
                new ElectionData { State = "Florida", Candidate = "Obama", Electors = 29 },
                new ElectionData { State = "Georgia", Candidate = "Romney", Electors = 16 },
                new ElectionData { State = "Hawaii", Candidate = "Obama", Electors = 4 },
                new ElectionData { State = "Idaho", Candidate = "Romney", Electors = 4 },
                new ElectionData { State = "Illinois", Candidate = "Obama", Electors = 20 },
                new ElectionData { State = "Indiana", Candidate = "Romney", Electors = 11 },
                new ElectionData { State = "Iowa", Candidate = "Obama", Electors = 6 },
                new ElectionData { State = "Kansas", Candidate = "Romney", Electors = 6 },
                new ElectionData { State = "Kentucky", Candidate = "Romney", Electors = 8 },
                new ElectionData { State = "Louisiana", Candidate = "Romney", Electors = 8 },
                new ElectionData { State = "Maine", Candidate = "Obama", Electors = 4 },
                new ElectionData { State = "Maryland", Candidate = "Obama", Electors = 10 },
                new ElectionData { State = "Massachusetts", Candidate = "Obama", Electors = 11 },
                new ElectionData { State = "Michigan", Candidate = "Obama", Electors = 16 },
                new ElectionData { State = "Minnesota", Candidate = "Obama", Electors = 10 },
                new ElectionData { State = "Mississippi", Candidate = "Romney", Electors = 6 },
                new ElectionData { State = "Missouri", Candidate = "Romney", Electors = 10 },
                new ElectionData { State = "Montana", Candidate = "Romney", Electors = 3 },
                new ElectionData { State = "Nebraska", Candidate = "Romney", Electors = 5 },
                new ElectionData { State = "Nevada", Candidate = "Obama", Electors = 6 },
                new ElectionData { State = "New Hampshire", Candidate = "Obama", Electors = 4 },
                new ElectionData { State = "New Jersey", Candidate = "Obama", Electors = 14 },
                new ElectionData { State = "New Mexico", Candidate = "Obama", Electors = 5 },
                new ElectionData { State = "New York", Candidate = "Obama", Electors = 29 },
                new ElectionData { State = "North Carolina", Candidate = "Romney", Electors = 15 },
                new ElectionData { State = "North Dakota", Candidate = "Romney", Electors = 3 },
                new ElectionData { State = "Ohio", Candidate = "Obama", Electors = 18 },
                new ElectionData { State = "Oklahoma", Candidate = "Romney", Electors = 7 },
                new ElectionData { State = "Oregon", Candidate = "Obama", Electors = 7 },
                new ElectionData { State = "Pennsylvania", Candidate = "Obama", Electors = 20 },
                new ElectionData { State = "Rhode Island", Candidate = "Obama", Electors = 4 },
                new ElectionData { State = "South Carolina", Candidate = "Romney", Electors = 9 },
                new ElectionData { State = "South Dakota", Candidate = "Romney", Electors = 3 },
                new ElectionData { State = "Tennessee", Candidate = "Romney", Electors = 11 },
                new ElectionData { State = "Texas", Candidate = "Romney", Electors = 38 },
                new ElectionData { State = "Utah", Candidate = "Romney", Electors = 6 },
                new ElectionData { State = "Vermont", Candidate = "Obama", Electors = 3 },
                new ElectionData { State = "Virginia", Candidate = "Obama", Electors = 13 },
                new ElectionData { State = "Washington", Candidate = "Obama", Electors = 12 },
                new ElectionData { State = "West Virginia", Candidate = "Romney", Electors = 5 },
                new ElectionData { State = "Wisconsin", Candidate = "Obama", Electors = 10 },
                new ElectionData { State = "Wyoming", Candidate = "Romney", Electors = 3 }
            };
            return electionResults;
        }
    }

    public class ShapeViewModel
    {
        public ObservableCollection<Continents> ContinentList
        {
            get;
            set;
        }
        public ShapeViewModel()
        {
            ContinentList = new ObservableCollection<Continents>();
            ContinentList.Add(new Continents { NAME = "Asia" });
            ContinentList.Add(new Continents { NAME = "Africa" });
            ContinentList.Add(new Continents { NAME = "Europe" });
            ContinentList.Add(new Continents { NAME = "Antarctica" });
            ContinentList.Add(new Continents { NAME = "North America" });
            ContinentList.Add(new Continents { NAME = "South America" });
            ContinentList.Add(new Continents { NAME = "Oceania" });


        }
    }
   
    public class CountryList : INotifyPropertyChanged,IDisposable
    {
        public CountryList()
        {
           
            this.CountriesData = new ObservableCollection<Countries>();
            CountriesData.Add(new Countries() { ShapeCollection = new ShapeProperty() { Country = "USA", ShapeFill = "LightBlue",Uri = "SampleBrowser.Assets.ShapeFiles.states.shp" } });
            CountriesData.Add(new Countries() { ShapeCollection = new ShapeProperty() { Country = "Argentina", ShapeFill = "Orange", Uri = "SampleBrowser.Assets.ShapeFiles.Argentina.shp" } });
            CountriesData.Add(new Countries() { ShapeCollection = new ShapeProperty() { Country = "Brazil", ShapeFill = "#88B061", Uri = "SampleBrowser.Assets.ShapeFiles.Brazil.shp" } });
            CountriesData.Add(new Countries() { ShapeCollection = new ShapeProperty() { Country = "Colombia", ShapeFill = "#68A79B", Uri = "SampleBrowser.Assets.ShapeFiles.Colombia.shp" } });
            CountriesData.Add(new Countries() { ShapeCollection = new ShapeProperty() { Country = "WorldMap", ShapeFill = "#DF9C61", Uri = "SampleBrowser.Assets.ShapeFiles.world1.shp" } });
            this.SelectedValue = this.CountriesData[0].ShapeCollection;

        }
        public ObservableCollection<Countries> CountriesData { get; set; }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private ShapeProperty _selectedValue;
      
        public ShapeProperty SelectedValue
        {

            get { return _selectedValue; }
            set
            {
                if (_selectedValue != value)
                {
                    _selectedValue = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedValue"));
                }
            }
        }

        public void Dispose()
        {
            this.CountriesData.Clear();
        }
    }

    public class ElectionData
    {
        private string state;
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string candidate;
        public string Candidate
        {
            get { return candidate; }
            set { candidate = value; }
        }

        private double electors;
        public double Electors
        {
            get { return electors; }
            set { electors = value; }
        }
    }
}
