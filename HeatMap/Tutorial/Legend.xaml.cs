using Common;
using Syncfusion.UI.Xaml.HeatMap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HeatMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Legend : SampleLayout
    {
        public Legend()
        {
            InitializeComponent();
            GetData();
            this.DataContext = List;
        }

        private ObservableCollection<EmployeeInfoModel> List = new ObservableCollection<EmployeeInfoModel>();

        private void GetData()
        {
            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                EmployeeInfoModel Info = new EmployeeInfoModel();
                Info.EmployeeName = employeeName[i];
                Info.January = r.Next(1, 11);
                Info.February = r.Next(1, 11);
                Info.March = r.Next(1, 11);
                Info.April = r.Next(1, 11);
                Info.May = r.Next(1, 11);
                Info.June = r.Next(1, 11);
                Info.July = r.Next(1, 11);
                Info.August = r.Next(1, 11);
                Info.September = r.Next(1, 11);
                Info.October = r.Next(1, 11);
                Info.November = r.Next(1, 11);
                Info.December = r.Next(1, 11);
                this.List.Add(Info);
            }
        }

        string[] employeeName = new string[]
        {
            "Peter Scott",
            "Maria Anders",
            "John Camino",
            "Philips Cramer",
            "Robert King",
            "Simon Crowther",
        };

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            if(gradient.IsChecked == true)
            {
                Legends.LegendMode = LegendMode.Gradient;
                if(Legends.Orientation == Orientation.Horizontal)
                {
                    Legends.Width = 400;
                    Legends.Height = double.NaN;
                    Grid.SetRow(Legends, 2);
                }
                else
                {
                    Legends.Width = double.NaN;
                    Legends.Height = 250;
                    Grid.SetRow(Legends, 1);
                }
            }
            else if(list.IsChecked == true)
            {
                Legends.LegendMode = LegendMode.List;
                if (Legends.Orientation == Orientation.Horizontal)
                {                    
                    Grid.SetRow(Legends, 2);
                    Grid.SetColumn(Legends, 0);
                }
                else
                {
                    Grid.SetColumn(Legends, 1);
                    Grid.SetRow(Legends, 1);
                }
            }
        }

        private void RadioButton_Checked1(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            if (horizontal.IsChecked == true)
            {
                Legends.Orientation = Orientation.Horizontal;
                Legends.Width = 400;
                Legends.Height = double.NaN;
                Grid.SetRow(Legends, 2);
                Grid.SetColumn(Legends, 0);
                Legends.Margin = new Thickness(10, 30, 10, 10);
            }
            else if (vertical.IsChecked == true)
            {
                Legends.Orientation = Orientation.Vertical;
                Legends.Width = double.NaN;
                Legends.Height = 250;
                Grid.SetRow(Legends, 1);
                Grid.SetColumn(Legends, 1);
                Legends.Margin = new Thickness(30, 10, 10, 10);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }
}
