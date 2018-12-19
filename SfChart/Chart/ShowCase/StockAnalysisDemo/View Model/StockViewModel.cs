#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Charts;
using Syncfusion.UI.Xaml.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public class StockViewModels: INotifyPropertyChanged
    {
        private List<Stocks> stocks;

        private string[] stockNames = { "GOOG", "MSFT", "AAPL", "NOK","SNE","IBM","INTC"};

        public StockViewModels()
        {
            LoadData();
        }

        async void LoadData()
        {
            Stocks = await GenerateStocks();
            await Task.Delay(50);
            SelectedIndex = 0;
        }

        private int selectedIndex = -1;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        private Visibility chartVisibility = Visibility.Visible;

        public Visibility ChartVisibility
        {
            get { return chartVisibility; }
            set 
            { 
                chartVisibility = value;
                OnPropertyChanged("ChartVisibility");
            }
        }

        private Visibility gridVisibility = Visibility.Collapsed;

        public Visibility GridVisibility
        {
            get { return gridVisibility; }
            set 
            { 
                gridVisibility = value;
                OnPropertyChanged("GridVisibility");
            }
        }

    
        public List<Stocks> Stocks
        {
            get
            {
                return stocks;
            }
            set
            {
                stocks = value;
                OnPropertyChanged("Stocks");
            }
        }


        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        async Task<List<Stocks>> GenerateStocks()
        {
            List<Stocks> stocks = new List<Stocks>();

            foreach (string stockName in stockNames)
            {
                Stocks stock = new Stocks();
                stock.StockName = stockName;

                var Path = "Syncfusion.SampleBrowser.UWP.SfChart.Chart.ShowCase.StockAnalysisDemo.Data." + stockName + ".txt";
                
                stock.OrgDatas = await Task.Run(()=> GetDatas(Path));
                stock.Datas = await Task.Run(() => GetDatas(Path));
                int count=stock.Datas.Count;
                stock.CurrentHigh = stock.Datas[count - 1].High;
                stock.CurrentLow = stock.Datas[count - 1].Low;
                stock.CurrentClose = stock.Datas[count - 1].Last;
                stock.PreviousClose = stock.Datas[count - 2].Last;
                if (stockName == "GOOG")
                {
                    stock.Color = new SolidColorBrush(Color.FromArgb(255, 139, 197, 63));
                    stock.Color1 = new SolidColorBrush(Color.FromArgb(255, 107, 142, 47));
                }
                if (stockName == "MSFT")
                {
                    stock.Color = new SolidColorBrush(Color.FromArgb(255, 206, 36, 43));
                    stock.Color1 = new SolidColorBrush(Color.FromArgb(255, 56, 56, 56));
                }
                if (stockName == "AAPL")
                {
                    stock.Color = new SolidColorBrush(Color.FromArgb(255, 75, 91, 82));
                    stock.Color1 = new SolidColorBrush(Color.FromArgb(255, 31, 35, 34));
                }
                if (stockName == "NOK")
                {
                    stock.Color = new SolidColorBrush(Color.FromArgb(255, 0, 166, 156));
                    stock.Color1 = new SolidColorBrush(Color.FromArgb(255, 56, 56, 56));
                }
                if (stockName == "SNE")
                {
                    stock.Color = new SolidColorBrush(Color.FromArgb(255, 0, 164, 228));
                    stock.Color1 = new SolidColorBrush(Color.FromArgb(255, 56, 56, 56));
                }
                if (stockName == "IBM")
                {
                    stock.Color = new SolidColorBrush(Color.FromArgb(255, 125, 106, 85));
                    stock.Color1 = new SolidColorBrush(Color.FromArgb(255, 91, 71, 55));
                }
                if (stockName == "INTC")
                {
                    stock.Color = new SolidColorBrush(Color.FromArgb(255, 195, 110, 14));
                    stock.Color1 = new SolidColorBrush(Color.FromArgb(255, 132, 68, 11));
                }
                stocks.Add(stock);
            }
            return stocks;
        }

        public static List<StockDatas> GetDatas(string fileName)
        {
            char[] comma = new char[] { ',' };
            char[] slashN = new char[] { '\n' };
            List<StockDatas> list = new List<StockDatas>();

            //var Folder = global::Windows.ApplicationModel.Package.Current.InstalledLocation;
            //var file = await Folder.GetFileAsync(fileName);
            //StorageFile file = await StorageFile.GetFileFromPathAsync(Windows.ApplicationModel.Package.Current.InstalledLocation.Path + fileName);
            //IList<string> lines = await global::Windows.Storage.FileIO.ReadLinesAsync(file);

            IList<string> lines = new List<string>();
            var assembly = typeof(StockViewModel).GetTypeInfo().Assembly;
            var fileStream = assembly.GetManifestResourceStream(fileName);
            StreamReader reader = new StreamReader(fileStream);
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }

            bool firstLine = true;
            string[] values;
            int count = lines.Count() - 2;
            StockDatas priceInfo;
            int index = 0;
            foreach (string line in lines)
            {
                if (count != -1 && index >= count)
                    break;
                if (!firstLine)
                {
                    values = line.Split(comma);
                    if (values.GetLength(0) > 5)
                    {
                        priceInfo = new StockDatas()
                        {
                            TimeStamp = DateTime.Parse(values[0], CultureInfo.InvariantCulture),
                            Open = double.Parse(values[1]),
                            High = double.Parse(values[2]),
                            Low = double.Parse(values[3]),
                            Last = double.Parse(values[4]),
                            Volume = double.Parse(values[5])
                        };
                        list.Insert(index, priceInfo);
                        index++;
                    }
                }
                else
                {
                    firstLine = false;
                }
            }
            return list;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Stocks : INotifyPropertyChanged
    {
        public Stocks()
        {

        }

        public string StockName { get; set; }

        public List<StockDatas> OrgDatas
        {
            get;
            set;
        }

        List<StockDatas> datas;

        public List<StockDatas> Datas
        {
            get
            {
                return datas;
            }
            set
            {
                datas = value;
                selectedStock = this;
                OnPropertyChanged("Datas");
            }
        }

        public SolidColorBrush Color { get; set; }

        public SolidColorBrush Color1 { get; set; }

        Stocks selectedStock;
        public Stocks SelectedStock
        {
            get
            {
                return selectedStock;
            }
            set
            {
                selectedStock = value;
                OnPropertyChanged("SelectedStock");
            }
        }

        private double currentLow;

        private double currentClose;

        private double currentHigh;

        public double CurrentLow
        {
            get
            {
                return currentLow;
            }
            set
            {
                currentLow = value;
                OnPropertyChanged("CurrentLow");
            }
        }

        public double CurrentHigh
        {
            get
            {
                return currentHigh;
            }
            set
            {
                currentHigh = value;
                OnPropertyChanged("CurrentHigh");
            }
        }

        public double CurrentClose
        {
            get
            {
                return currentClose;
            }
            set
            {
                currentClose = value;
                OnPropertyChanged("TodayClose");
            }
        }

        private double previousClose;

        public double PreviousClose
        {
            get
            {
                return previousClose;
            }
            set
            {
                previousClose = value;
                OnPropertyChanged("PreviousClose");
            }
        }


        ICommand updatePeriod;

        public ICommand UpdatePeriod
        {
            get
            {
                if (updatePeriod == null)
                {
                    updatePeriod = new UpdatePeriodCommand(this, Datas);
                    this.Datas = Datas;
                }
                return updatePeriod;
            }
        }

        private ObservableCollection<ChartSeries> indicatorsColln = new ObservableCollection<ChartSeries>();

        public ObservableCollection<ChartSeries> IndicatorsCollection
        {

            get
            {
                return indicatorsColln;
            }
            set
            {
                indicatorsColln = value;
                OnPropertyChanged("IndicatorsCollection");
            }
        }

        private string selindicator;

        public string SelectedIndicator
        {

            get
            {
                return selindicator;
            }
            set
            {
                selindicator = value;
                OnPropertyChanged("SelectedIndicator");
            }
        }



        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ConcatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartCustomInfo obj = value as ChartCustomInfo;
            return  obj.LabelX +" "+"|"+" "+ obj.ValueX.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class UpdatePeriodCommand : ICommand,INotifyPropertyChanged
    {

        List<StockDatas> datas;

        public List<StockDatas> Datas
        {
            get
            {
                return datas;
            }
            set
            {
                datas = value;
                Stock1.Datas = value;
                OnPropertyChanged("Datas");
            }
        }

        Stocks Stock1;

        public UpdatePeriodCommand( Stocks stock,List<StockDatas> datas)
        {
            Stock1 = stock;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var CanExecuteChanged = this.CanExecuteChanged;
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            var value = parameter as string;

            if (value == "5")
            {
                int count = Stock1.OrgDatas.Count;
                DateTime start = Stock1.OrgDatas[count - 1].TimeStamp;
                DateTime end = start.AddYears(-5);
                Datas = Stock1.OrgDatas.Where(x => x.TimeStamp < start && x.TimeStamp > end).ToList();
            }
            else if (value == "3")
            {
                int count = Stock1.OrgDatas.Count;
                DateTime start = Stock1.OrgDatas[count - 1].TimeStamp;
                DateTime end = start.AddYears(-3);
                Datas = Stock1.OrgDatas.Where(x => x.TimeStamp < start && x.TimeStamp > end).ToList();
            }
            else if (value == "2")
            {
                int count = Stock1.OrgDatas.Count;
                DateTime start = Stock1.OrgDatas[count - 1].TimeStamp;
                DateTime end = start.AddYears(-2);
                Datas = Stock1.OrgDatas.Where(x => x.TimeStamp < start && x.TimeStamp > end).ToList();
            }
            else
            {
                int count = Stock1.OrgDatas.Count;
                DateTime start = Stock1.OrgDatas[count - 1].TimeStamp;
                DateTime end = start.AddYears(1);
                Datas = Stock1.OrgDatas.Where(x => x.TimeStamp < start && x.TimeStamp > end).ToList();
            }
        }
        
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }

    public class FillConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter == null)
            {
                Stocks data = value as Stocks;
                if (data.CurrentClose < data.PreviousClose)
                    return new SolidColorBrush(Colors.Red);
                else
                    return new SolidColorBrush(Colors.Green);
            }
            else
            { 
              if(parameter.ToString().Equals("High"))
                  return new SolidColorBrush(Colors.Green);
              else
                  return new SolidColorBrush(Colors.Red);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    public class RotateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Stocks data = value as Stocks;
            if (data.CurrentClose < data.PreviousClose)
                return 0;
            else
                return 180;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ButtonVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
                return Visibility.Visible;
            else 
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeStampConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                string time = ((DateTime)value).ToString(@"dd MMMM yyyy");
                return time;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    public class DoubleValueConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var doubleValue = ((double)value).ToString("N2");
                return doubleValue;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class SelectionChangedConverter : DependencyObject, IValueConverter
    {
        public UI.Xaml.Charts.SfChart Chart
        {
            get { return (UI.Xaml.Charts.SfChart)GetValue(ChartProperty); }
            set { SetValue(ChartProperty, value); }
        }

        public static readonly DependencyProperty ChartProperty =
            DependencyProperty.Register("Chart", typeof(UI.Xaml.Charts.SfChart), typeof(SelectionChangedConverter), new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (Chart != null)
            {
                ChartTechnicalIndicatorBehavior behavior = Chart.Behaviors[1] as ChartTechnicalIndicatorBehavior;
                CustomCrossHairbehavior crossHair = Chart.Behaviors[0] as CustomCrossHairbehavior;
                crossHair.ResetElements();
                behavior.IndicatorSource2.SelectedIndex = 0;
                Chart.TechnicalIndicators.Clear();
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((DateTime)value).ToString(@" MM'/'dd'/'yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double val = (Double)value;
            return Math.Round(val, 2, MidpointRounding.AwayFromZero);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class HeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            List<StockDatas> datas = value as List<StockDatas>;
            if (datas.Count == 0)
                return value;
            return datas[datas.Count - 1].Last;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class NameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            List<StockDatas> datas = value as List<StockDatas>;
            if (datas.Count == 0)
                return value;
            return datas[datas.Count - 1].Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
