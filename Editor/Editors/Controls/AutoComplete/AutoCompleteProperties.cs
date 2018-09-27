using Common;
using Syncfusion.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Editors;

namespace SampleBrowser.Editors.Controls.AutoComplete
{
    public class AutoCompleteProperties : NotificationObject , IDisposable
    {       
        
        private SuggestionMode suggestionMode = SuggestionMode.StartsWith;

        public SuggestionMode SuggestionMode
        {
            get { return suggestionMode; }
            set { suggestionMode = value; RaisePropertyChanged("SuggestionMode"); }
        }

        private AutoCompleteMode autoCompleteMode = DeviceFamily.GetDeviceFamily() == Devices.Mobile ? AutoCompleteMode.SuggestAppend : AutoCompleteMode.Suggest;

        public AutoCompleteMode AutoCompleteMode
        {
            get { return autoCompleteMode; }
            set { autoCompleteMode = value; RaisePropertyChanged("AutoCompleteMode"); }
        }

        private ObservableCollection<Person> images;

        public ObservableCollection<Person> Images
        {
            get { return images; }
            set { images = value; }
        }

        private int _value = 1;

        public int Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged("Value"); }
        }

        public AutoCompleteProperties()
        {
            Images = new ObservableCollection<Person>();
            Images.Add(new Person("Eric Joplin", "ms-appx:///TileView/Assets/Emp_02.png", 0.0, "Chairman", "Management", "27/09/1973", "Boston", "+800 9899 9929", "ericjoplin@syncfusion.com", "#FFA400", "#E78E00"));
            Images.Add(new Person("Paul Vent", "ms-appx:///TileView/Assets/Emp_04.png", 0.0, "Chief Executive Officer", "Management", "27/09/1975", "New York", "+800 9899 9930", "paulvent@syncfusion.com", "#6DA4A3", "#4E7F7D"));
            Images.Add(new Person("Clara Venus", "ms-appx:///TileView/Assets/Emp_06.png", 0.0, "Chief Executive Assistant", "Management", "27/09/1978", "California", "+800 9899 9931", "claravenus@syncfusion.com", "#A45378", "#883F64"));
            Images.Add(new Person("Maria Even", "ms-appx:///TileView/Assets/Emp_11.png", 0.0, "Executive Manager", "Operational Unit", "27/09/1970", "New York", "+800 9899 9932", "mariaeven@syncfusion.com", "#DA9545", "#BB7731"));
            Images.Add(new Person("Mark Zuen", "ms-appx:///TileView/Assets/Emp_13.png", 0.0, "Senior Executive", "Operational Unit", "27/09/1983", "Boston", "+800 9899 9933", "markzuen@syncfusion.com", "#AC3832", "#8B2826"));
            Images.Add(new Person("Robin Rane", "ms-appx:///TileView/Assets/Emp_16.png", 0.0, "Manager", "Customer Service", "27/09/1985", "New Jersey", "+800 9899 9934", "robinrane@syncfusion.com", "#31A1FF", "#2394E1"));
            Images.Add(new Person("Chris Marker", "ms-appx:///TileView/Assets/Emp_21.png", 0.0, "Team Manager", "Customer Service", "27/09/1963", "California", "+800 9899 9935", "chrismarker@syncfusion.com", "#5B5BA9", "#484892"));
            Images.Add(new Person("Seria Sum", "ms-appx:///TileView/Assets/Emp_23.png", 0.0, "Coordinator", "Customer Service", "27/09/1961", "New York", "+800 9899 9936", "seriasum@syncfusion.com", "#597C2A", "#46601D"));
            Images.Add(new Person("Mathew Fleming", "ms-appx:///TileView/Assets/Emp_25.png", 0.0, "Recruitment Manager", "Human Resource", "27/09/1986", "Boston", "+800 9899 9937", "mathewdfleming@syncfusion.com", "#BCCBD3", "#8BA0A9"));
            Images.Add(new Person("Steven Joplin", "ms-appx:///TileView/Assets/Emp_02.png", 0.0, "Chairman", "Management", "27/09/1973", "Boston", "+800 9899 9929", "ericjosplin@syncfusion.com", "#FFA400", "#E78E00"));
            Images.Add(new Person("Carl Vent", "ms-appx:///TileView/Assets/Emp_04.png", 0.0, "Chief Executive Officer", "Management", "27/09/1975", "New York", "+800 9899 9930", "paulavent@syncfusion.com", "#6DA4A3", "#4E7F7D"));
            Images.Add(new Person("James Venus", "ms-appx:///TileView/Assets/Emp_06.png", 0.0, "Chief Executive Assistant", "Management", "27/09/1978", "California", "+800 9899 9931", "clarahvenus@syncfusion.com", "#A45378", "#883F64"));
            Images.Add(new Person("Maria Strauss", "ms-appx:///TileView/Assets/Emp_11.png", 0.0, "Executive Manager", "Operational Unit", "27/09/1970", "New York", "+800 9899 9932", "mariaveven@syncfusion.com", "#DA9545", "#BB7731"));
            Images.Add(new Person("Kate Zuen", "ms-appx:///TileView/Assets/Emp_13.png", 0.0, "Senior Executive", "Operational Unit", "27/09/1983", "Boston", "+800 9899 9933", "markrzuen@syncfusion.com", "#AC3832", "#8B2826"));
            Images.Add(new Person("Niko Rane", "ms-appx:///TileView/Assets/Emp_16.png", 0.0, "Manager", "Customer Service", "27/09/1985", "New Jersey", "+800 9899 9934", "robinxrane@syncfusion.com", "#31A1FF", "#2394E1"));
            Images.Add(new Person("Chris gayle", "ms-appx:///TileView/Assets/Emp_21.png", 0.0, "Team Manager", "Customer Service", "27/09/1963", "California", "+800 9899 9935", "chriswmarker@syncfusion.com", "#5B5BA9", "#484892"));
            Images.Add(new Person("Sloth Sum", "ms-appx:///TileView/Assets/Emp_23.png", 0.0, "Coordinator", "Customer Service", "27/09/1961", "New York", "+800 9899 9936", "seriaqsum@syncfusion.com", "#597C2A", "#46601D"));
            Images.Add(new Person("Thomas Fleming", "ms-appx:///TileView/Assets/Emp_25.png", 0.0, "Recruitment Manager", "Human Resource", "27/09/1986", "Boston", "+800 9899 9937", "mathewsfleming@syncfusion.com", "#BCCBD3", "#8BA0A9"));
        }
        public void Dispose()
        {
            if (Images != null)
            {
                Images.Clear();
            }

        }

    }
    public class Person
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public TimeSpan Interval { get; set; }

        public string Position { get; set; }

        public string OrganizationUnit { get; set; }

        public string DateOfBirth { get; set; }

        public string Location { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string TileColor { get; set; }

        public string HeaderColor { get; set; }

        public Person(string name, string image, double seconds, string position, string organizationunit, string dateofbirth, string location, string phone, string email, string color, string headercolor)
        {
            Name = name;
            Image = image;
            Interval = TimeSpan.FromSeconds(seconds);
            Position = position;
            OrganizationUnit = organizationunit;
            DateOfBirth = dateofbirth;
            Location = location;
            Phone = phone;
            Email = email;
            TileColor = color;
            HeaderColor = headercolor;
        }
    }
}
