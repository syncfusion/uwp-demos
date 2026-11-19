using Common;
using Syncfusion.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Controls.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Editors
{
    public class TextBoxExtProperties1 : NotificationObject, IDataValidation, IDisposable
    {
        private string emailpattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged("Name"); RaisePropertyChanged("Error"); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged("Email"); RaisePropertyChanged("Error"); }
        }

        private DateTime dateofbirth = DateTime.Now;

        public DateTime DOB
        {
            get { return dateofbirth; }
            set { dateofbirth = value; RaisePropertyChanged("DOB"); RaisePropertyChanged("Error"); }
        }

        private string country;

        public string Country
        {
            get { return country; }
            set { country = value; RaisePropertyChanged("Country"); RaisePropertyChanged("Error"); }
        }

        private string city;

        public string City
        {
            get { return city; }
            set { city = value; RaisePropertyChanged("City"); RaisePropertyChanged("Error"); }
        }
        private ObservableCollection<string> cities;

        public ObservableCollection<string> Cities
        {
            get { return cities; }
            set { cities = value; RaisePropertyChanged("Cities"); }
        }
        
        private ObservableCollection<Country> countries;

        public ObservableCollection<Country> Countries
        {
            get { return countries; }
            set { countries = value; RaisePropertyChanged("Countries"); }
        }
        
        public string Error
        {
            get
            {
                if (String.IsNullOrEmpty(Name))
                {
                    return "Name field required.";
                }
				foreach (char c in Name)
                {
                    if (char.IsDigit(c) || char.IsSymbol(c))
                        return "Name field cannot be number.";
                }
                if (Email == null)
                {
                    return "Please enter valid email address.";
                }

                if (!Regex.IsMatch(Email, emailpattern, RegexOptions.None))
                {
                    return "Please enter valid email address.";
                }

                if (String.IsNullOrEmpty(Country))
                {
                    return "Country field required.";
                }
                else
                {
                    bool isAvailable = false;
                    for (int i = 0; i < Countries.ToArray().Length; i++)
                    {
                        if (Country.ToString().ToLower() == Countries[i].c_Name.ToString().ToLower())
                        {
                            isAvailable = true;
                            if(Cities == null)
                                Cities = new ObservableCollection<string>();
                            foreach (string s in Countries[i].Cities)
                                Cities.Add(s);
                            break;
                        }
                    }
                    if (!isAvailable)
                        return "Country not available";
                    
                }
                if (String.IsNullOrEmpty(City))
                {
                    return "City field required.";
                }
                else
                {
                    bool isAvailable = false;
                    for (int i = 0; Cities != null && i < Cities.ToArray().Length; i++)
                    {
                        if (City.ToString().ToLower() == Cities[i].ToString().ToLower())
                        {
                            isAvailable = true;
                            break;
                        }
                    }
                    if (!isAvailable)
                        return "City not available";
                }
                if (DateTime.Now.Year <= DOB.Year)
                {
                    return "Less than 18 years not allowed.";
                }
                else if (DateTime.Now.AddYears(-DOB.Year).Year < 18 || (DateTime.Now.AddYears(-DOB.Year).Year == 18 && DateTime.Now.Month < DOB.Month) || (DateTime.Now.AddYears(-DOB.Year).Year == 18 && DateTime.Now.Month > DOB.Month && DateTime.Now.Day < DOB.Day))
                {
                    return "Less than 18 years not allowed.";
                }
               return "";
            }
        }


        public string this[string columnname]
        {
            get
            {
                switch (columnname)
                {
                    case "Name":
                        if (String.IsNullOrEmpty(Name))
                        {
                            return "Name field required.";
                        }
						foreach (char c in Name)
						{
                            if (char.IsDigit(c) || char.IsSymbol(c))
                        return "Name field cannot be number.";
						}
                        break;
                    case "DOB":
						if (DOB > DateTime.Now)
                        {
                            return "Invalid Date";
                        }
                        if (DateTime.Now.Year <= DOB.Year)
                        {
                            return "Less than 18 years not allowed.";
                        }
                        else if (DateTime.Now.AddYears(-DOB.Year).Year < 18 || (DateTime.Now.AddYears(-DOB.Year).Year == 18 && DateTime.Now.Month < DOB.Month) || (DateTime.Now.AddYears(-DOB.Year).Year == 18 && DateTime.Now.Month >= DOB.Month && DateTime.Now.Day < DOB.Day))
                        {
                            return "Less than 18 years not allowed.";
                        }
                        break;
                    case "Country":
                        if (String.IsNullOrEmpty(Country))
                        {
                            return "Country field required.";
                        }
                        else
                        {
                            bool isAvailable = false;
                            for (int i = 0; i < Countries.ToArray().Length; i++)
                            {
                                if (Country.ToString().ToLower() == Countries[i].c_Name.ToString().ToLower())
                                {
                                    isAvailable = true;
                                    if(Cities == null)
                                        Cities = new ObservableCollection<string>();
                                    foreach (string s in Countries[i].Cities)
                                        Cities.Add(s);
                                    break;
                                }
                            }
                            if (!isAvailable)
                                return "Country not available";                          
                        }
                        break;
                    case "City":
                        if (String.IsNullOrEmpty(City))
                        {
                            return "City field required.";
                        }
                        else
                        {
                            bool isAvailable = false;
                            for (int i = 0; Cities != null && i < Cities.ToArray().Length; i++)
                            {
                                if (City.ToString().ToLower() == Cities[i].ToString().ToLower())
                                {
                                    isAvailable = true;
                                    break;
                                }
                            }
                            if (!isAvailable)
                                return "City not available";
                        }
                        break;
                    case "Email":
                        if (Email == null)
                            return "Please enter valid email address.";
                        if (!Regex.IsMatch(Email, emailpattern, RegexOptions.None))
                        {
                            return "Please enter valid email address.";
                        }
                        break;
                }
                return "";
            }
        }
        public TextBoxExtProperties1()
        {
            countries = new ObservableCollection<Country>();
            cities = new ObservableCollection<string>();
            countries.Add(new Country() { c_Name = "America", Cities = new ObservableCollection<string>() { "Boston","Chicago","Detroit","Houston", "Los Angeles", "New York","San Francisco", "Washington" } });
            countries.Add(new Country() { c_Name = "England", Cities = new ObservableCollection<string>() { "Birmingham", "Coventry", "Leeds", "Liverpool", "London", "Manchester", "Oxford", "Sheffield" } });
            countries.Add(new Country() { c_Name = "Germany", Cities = new ObservableCollection<string>() { "Bavaria", "Berlin","Cottbus", "Dortmund", "Hamburg", "Hesse", "Jena", "Saxony","Siegen" } });
            countries.Add(new Country() { c_Name = "India", Cities = new ObservableCollection<string>() { "Bangalore","Calcutta","Chennai","Delhi","Goa","Hyderabad","Kashmir", "Mumbai" } });
            countries.Add(new Country() { c_Name = "Ireland", Cities = new ObservableCollection<string>() {"Belfast", "Cork", "Derry", "Dublin", "Galway","Kilkenny","Limerick","Lisburn" } });
        }
        public void Dispose()
        {
            if (Countries != null)
            {
                Countries.Clear();
            }
            if (Cities != null)
            {
                Cities.Clear();
            }
        }
    }
    public class Country:NotificationObject
    {
        private string c_name;

        public string c_Name
        {
            get { return c_name; }
            set { c_name = value; RaisePropertyChanged("c_Name"); }
        }
        private ObservableCollection<string> cities;

        public ObservableCollection<string> Cities
        {
            get { return cities; }
            set { cities = value; RaisePropertyChanged("Cities"); }
        }
        
    }
}
