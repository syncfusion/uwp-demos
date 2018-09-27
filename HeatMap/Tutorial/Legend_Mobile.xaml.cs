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
    public partial class Legend_Mobile : SampleLayout
    {
        public Legend_Mobile()
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

     
        public override void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }
}
