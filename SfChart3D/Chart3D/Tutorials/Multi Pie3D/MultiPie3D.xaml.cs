using Common;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.SfChart3D
{
    public sealed partial class MultiPie3D : SampleLayout
    {
        public MultiPie3D()
        {
            this.InitializeComponent();
        }
    }

    public class Populations
    {
        public string Continent { get; set; }

        public string Countries { get; set; }

        public string States { get; set; }

        public double PopulationinStates { get; set; }

        public double PopulationinCountries { get; set; }

        public double PopulationinContinents { get; set; }
    }


    public class PieChartViewModel : IDisposable
    {
        public PieChartViewModel()
        {
            this.Population = new List<Populations>();

            Population.Add(new Populations() { Continent = "Asia", Countries = "China", States = "Taiwan", PopulationinContinents = 50.02, PopulationinCountries = 26.02, PopulationinStates = 18.02 });
            Population.Add(new Populations() { Continent = "Africa", Countries = "India", States = "Shandong", PopulationinContinents = 20.81, PopulationinCountries = 24, PopulationinStates = 8 });
            Population.Add(new Populations() { Continent = "Europe", Countries = "Nigeria", States = "Uttar Pradesh", PopulationinContinents = 15.37, PopulationinCountries = 12.81, PopulationinStates = 14.5 });
            Population.Add(new Populations() { Countries = "Ehiopia", States = "Maharashtra", PopulationinCountries = 8, PopulationinStates = 9.5 });
            Population.Add(new Populations() { Countries = "Germany", States = "Kano", PopulationinCountries = 8.37, PopulationinStates = 7.81 });
            Population.Add(new Populations() { Countries = "Turkey", States = "Lagos", PopulationinCountries = 7, PopulationinStates = 5 });
            Population.Add(new Populations() { States = "Oromia", PopulationinStates = 5 });
            Population.Add(new Populations() { States = "Amhara", PopulationinStates = 3 });
            Population.Add(new Populations() { States = "Hessen", PopulationinStates = 5.37 });
            Population.Add(new Populations() { States = "Bayern", PopulationinStates = 3 });
            Population.Add(new Populations() { States = "Istanbul", PopulationinStates = 4.5 });
            Population.Add(new Populations() { States = "Ankara", PopulationinStates = 2.5 });
        }

        public IList<Populations> Population { get; set; }

        public void Dispose()
        {
            if (this.Population != null)
                this.Population.Clear();
        }
    }

    public class PopulationLabelConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartAdornment pieAdornment = value as ChartAdornment;
            if ((pieAdornment.Item as Populations).Continent != null)
            {
                return String.Format((pieAdornment.Item as Populations).Continent);
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class PopulationLabelConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartAdornment pieAdornment = value as ChartAdornment;
            if ((pieAdornment.Item as Populations).Countries != null)
            {
                return String.Format((pieAdornment.Item as Populations).Countries);
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class PopulationLabelConverter3 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartAdornment pieAdornment = value as ChartAdornment;
            return String.Format((pieAdornment.Item as Populations).States);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
