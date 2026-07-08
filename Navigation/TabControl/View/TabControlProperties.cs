using Common;
using Syncfusion.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Controls.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.TabControl
{
    public class TabControlProperties:NotificationObject , IDisposable
    {
        public TabControlProperties()
        {
            Images = new ObservableCollection<Person>();
            Images.Add(new Person("James", GetResourcePath("TabControl/Assets/1.jpg")));
            Images.Add(new Person("Peaches", GetResourcePath("TabControl/Assets/2.jpg")));
            Images.Add(new Person("Linda", GetResourcePath("TabControl/Assets/3.jpg")));
            Images.Add(new Person("Carl", GetResourcePath("TabControl/Assets/4.jpg")));
            Images.Add(new Person("Niko", GetResourcePath("TabControl/Assets/5.jpg")));
            Images.Add(new Person("Eric", GetResourcePath("TabControl/Assets/Emp_02.png")));
            Images.Add(new Person("Paul", GetResourcePath("TabControl/Assets/Emp_04.png")));
            Images.Add(new Person("Clara", GetResourcePath("TabControl/Assets/Emp_06.png")));
            Images.Add(new Person("Maria", GetResourcePath("TabControl/Assets/Emp_11.png")));
            Images.Add(new Person("Mark", GetResourcePath("TabControl/Assets/Emp_13.png")));
            Images.Add(new Person("Robin", GetResourcePath("TabControl/Assets/Emp_16.png")));
            Images.Add(new Person("Chris", GetResourcePath("TabControl/Assets/Emp_21.png")));
            Images.Add(new Person("Seria", GetResourcePath("TabControl/Assets/Emp_23.png")));
            Images.Add(new Person("Mathew", GetResourcePath("TabControl/Assets/Emp_25.png")));
        }

        private ObservableCollection<Person> images;

        public ObservableCollection<Person> Images
        {
            get { return images; }
            set { images = value; }
        }

        private TabStripPlacement tabPlacement = TabStripPlacement.Top;

        public TabStripPlacement TabPlacement
        {
            get { return tabPlacement; }
            set { tabPlacement = value; RaisePropertyChanged("TabPlacement"); }
        }

        private CloseButtonType closeType = CloseButtonType.Hide;

        public CloseButtonType CloseType
        {
            get { return closeType; }
            set { closeType = value; RaisePropertyChanged("CloseType"); }
        }

        private TabScrollButtonVisibility scrollButtonVisibility = TabScrollButtonVisibility.Auto;

        public TabScrollButtonVisibility ScrollButtonVisibility
        {
            get { return scrollButtonVisibility; }
            set { scrollButtonVisibility = value; RaisePropertyChanged("ScrollButtonVisibility"); }
        }
        public void Dispose()
        {
            if (Images != null)
            {
                Images.Clear();
                Images = null;
            }

        }
        public string GetResourcePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                throw new ArgumentNullException(nameof(relativePath));

            // Check if running in Sample Browser main application
            string assemblyName = (Application.Current.GetType().GetTypeInfo().Assembly as Assembly)?.GetName().Name ?? string.Empty;

            if (assemblyName == "Syncfusion.SampleBrowser.UWP")
            {
                // Running in main Sample Browser app - include assembly name in path
                return $"ms-appx:///Syncfusion.SampleBrowser.UWP.Navigation/{relativePath}";
            }
            else
            {
                // Running in separate assembly - use standard path format
                return $"ms-appx:///{relativePath}";
            }
        }

    }

    public class Person
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public Person(string name, string image)
        {
            Name = name;
            Image = image;
        }
    }
}
