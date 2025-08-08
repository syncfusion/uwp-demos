#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public sealed partial class AnnotationDemo : SampleLayout
    {
        public AnnotationDemo()
        {
            Resources.MergedDictionaries.Add(SfChart.Resources.ColorModelResource.Resource);
            this.InitializeComponent();
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                scatterChart1.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
        }

        public override void Dispose()
        {
            if (this.scatterChart1 != null)
            {
                foreach (var series in this.scatterChart1.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.scatterChart1 = null;
            }

            if (this.lineChart != null)
            {
                foreach (var series in this.lineChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.lineChart = null;
            }

            this.MainGrid.Resources = null;

            base.Dispose();
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox != null)
            {
                if (lineChart == null) return;
                if (comboBox.SelectedIndex == 0)
                {
                    lineChart.Visibility = Visibility.Visible;
                    scatterChart1.Visibility = Visibility.Collapsed;

                }
                else if (comboBox.SelectedIndex == 1)
                {
                    lineChart.Visibility = Visibility.Collapsed;
                    scatterChart1.Visibility = Visibility.Visible;

                }
            }
        }
    }

    public class AnnotationDataModel
    {
        private DateTime timestamp;

        public DateTime TimeStamp
        {
            get
            {
                return timestamp;
            }
            set
            {
                timestamp = value;
            }
        }
        private double rainfall;

        public double RainFall
        {
            get
            {
                return rainfall;
            }
            set
            {
                rainfall = value;
            }
        }

        public DateTime Year { get; set; }
        public double Population { get; set; }
    }

    public class DataViewModel : ObservableCollection<AnnotationDataModel>, INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChangedEvent;

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChangedEvent != null)
                PropertyChangedEvent(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<AnnotationDataModel> power
        {
            get;
            set;
        }

        public DataViewModel()
        {
            this.power = new ObservableCollection<AnnotationDataModel>();
            DateTime yr = new DateTime(2002, 5, 1);

            power.Add(new AnnotationDataModel() { Year = yr.AddYears(1), Population = 36 });
            power.Add(new AnnotationDataModel() { Year = yr.AddYears(2), Population = 32 });
            power.Add(new AnnotationDataModel() { Year = yr.AddYears(3), Population = 34 });
            power.Add(new AnnotationDataModel() { Year = yr.AddYears(4), Population = 41 });
            power.Add(new AnnotationDataModel() { Year = yr.AddYears(5), Population = 44 });
            power.Add(new AnnotationDataModel() { Year = yr.AddYears(6), Population = 48 });
            power.Add(new AnnotationDataModel() { Year = yr.AddYears(7), Population = 45 });
        }

        public void Dispose()
        {
            if (this.power != null)
                this.power.Clear();
        }
    }

    public class AnnotationViewModel : IDisposable
    {
        public AnnotationViewModel()
        {
            DataModel = new ObservableCollection<AnnotationModel>();
            DataModel.Add(new AnnotationModel() { Over = 1, Runs = 4 });
            DataModel.Add(new AnnotationModel() { Over = 2, Runs = 8, PlayerName = "CL White", Score = "10 (6)" });
            DataModel.Add(new AnnotationModel() { Over = 3, Runs = 12 });
            DataModel.Add(new AnnotationModel() { Over = 4, Runs = 3 });
            DataModel.Add(new AnnotationModel() { Over = 5, Runs = 10 });
            DataModel.Add(new AnnotationModel() { Over = 6, Runs = 6 });
            DataModel.Add(new AnnotationModel() { Over = 7, Runs = 11, PlayerName = "AJ Finch", Score = "30 (24)" });
            DataModel.Add(new AnnotationModel() { Over = 8, Runs = 5 });
            DataModel.Add(new AnnotationModel() { Over = 9, Runs = 4 });
            DataModel.Add(new AnnotationModel() { Over = 10, Runs = 12 });
            DataModel.Add(new AnnotationModel() { Over = 11, Runs = 8 });
            DataModel.Add(new AnnotationModel() { Over = 12, Runs = 14, PlayerName = "GJ Smith", Score = "25 (14)" });
            DataModel.Add(new AnnotationModel() { Over = 13, Runs = 12 });
            DataModel.Add(new AnnotationModel() { Over = 14, Runs = 15 });
            DataModel.Add(new AnnotationModel() { Over = 15, Runs = 10 });
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                DataModel.Add(new AnnotationModel() { Over = 16, Runs = 14 });
                DataModel.Add(new AnnotationModel() { Over = 17, Runs = 16 });
                DataModel.Add(new AnnotationModel() { Over = 18, Runs = 9 });
                DataModel.Add(new AnnotationModel() { Over = 19, Runs = 10, PlayerName = "GJ Bailey", Score = "78 (40)" });
                DataModel.Add(new AnnotationModel() { Over = 20, Runs = 18 });
            }
        }
        public ObservableCollection<AnnotationModel> DataModel
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.DataModel != null)
                this.DataModel.Clear();
        }
    }

    public class AnnotationModel
    {
        public double Over
        {
            get;
            set;
        }

        public double Runs
        {
            get;
            set;
        }

        public string PlayerName
        {
            get;
            set;
        }

        public string Score
        {
            get;
            set;
        }
    }

    public class AnnotationDataModel1
    {
        public DateTime TimeStamp
        {
            get;
            set;
        }
        public double RainFall
        {
            get;
            set;
        }

    }

    public class DataViewModel1 : ObservableCollection<AnnotationDataModel1>
    {
        public List<AnnotationDataModel1> Datalist
        {
            get;
            set;
        }

        public DataViewModel1()
        {
            Datalist = new List<AnnotationDataModel1>();
            Datalist = this.GetPricesFromCSVFile();
        }

        public List<AnnotationDataModel1> GetPricesFromCSVFile()
        {
            List<AnnotationDataModel1> list = new List<AnnotationDataModel1>();
            Random rand = new Random();
            DateTime now = new DateTime(2007, 1, 1);
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 120 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 150 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 170 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 176 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 180 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 200 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 210 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 200 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 240 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 230 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 200 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 190 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 220 });
            list.Add(new AnnotationDataModel1() { TimeStamp = now.AddMonths(list.Count()), RainFall = 200 });
            return list;
        }

    }

    public class AnnotationViewModel1
    {
        public AnnotationViewModel1()
        {
            DataModel = new ObservableCollection<AnnotationModel1>();
            DataModel.Add(new AnnotationModel1() { Over = 1, Runs = 4 });
            DataModel.Add(new AnnotationModel1() { Over = 2, Runs = 8, PlayerName = "CL James", Score = "10 (6)" });
            DataModel.Add(new AnnotationModel1() { Over = 3, Runs = 12 });
            DataModel.Add(new AnnotationModel1() { Over = 4, Runs = 3 });
            DataModel.Add(new AnnotationModel1() { Over = 5, Runs = 10 });
            DataModel.Add(new AnnotationModel1() { Over = 6, Runs = 6 });
            DataModel.Add(new AnnotationModel1() { Over = 7, Runs = 11, PlayerName = "AJ Erich", Score = "30 (24)" });
            DataModel.Add(new AnnotationModel1() { Over = 8, Runs = 5 });
            DataModel.Add(new AnnotationModel1() { Over = 9, Runs = 4 });
            DataModel.Add(new AnnotationModel1() { Over = 10, Runs = 12 });
            DataModel.Add(new AnnotationModel1() { Over = 11, Runs = 8 });
            DataModel.Add(new AnnotationModel1() { Over = 12, Runs = 14, PlayerName = "GJ Louis", Score = "25 (14)" });
            DataModel.Add(new AnnotationModel1() { Over = 13, Runs = 12 });
            DataModel.Add(new AnnotationModel1() { Over = 14, Runs = 15 });
            DataModel.Add(new AnnotationModel1() { Over = 15, Runs = 10 });
        }
        public ObservableCollection<AnnotationModel1> DataModel
        {
            get;
            set;
        }
    }

    public class AnnotationModel1
    {
        public double Over
        {
            get;
            set;
        }

        public double Runs
        {
            get;
            set;
        }

        public string PlayerName
        {
            get;
            set;
        }

        public string Score
        {
            get;
            set;
        }
    }

    public class InteractionModel
    {
        public DateTime XValue { get; set; }
        public double YValue { get; set; }
    }
    public class InteractionViewModel : ObservableCollection<InteractionModel>
    {
        public InteractionViewModel()
        {
            double value = 100;
            int count = 500;
            Random random = new Random();
            DateTime now = DateTime.Now;
            for (int i = 0; i < count; i++)
            {
                if (random.NextDouble() > .6)
                {
                    value += random.NextDouble();
                }
                else if (value >= 80)
                {
                    value -= random.NextDouble();
                }
                else
                {
                    value += random.NextDouble();
                }
                this.Add(new InteractionModel() { XValue = now.AddHours(i), YValue = value });
            }
        }
    }
}
