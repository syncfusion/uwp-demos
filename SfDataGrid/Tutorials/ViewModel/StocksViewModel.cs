using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.ComponentModel;
using Syncfusion.Data.Extensions;
using System.Reflection;

namespace DataGrid
{
    /// <summary>
    /// This class represents the StocksViewModel
    /// </summary>
    public class StocksViewModel : IDisposable , INotifyPropertyChanged
    {
        #region Members

        ObservableCollection<StockData> data;
        Random r = new Random(123345345);
        Random p1 = new Random(123345345);
        internal DispatcherTimer timer;
        private bool enableTimer = false;
        private int noOfUpdates = 1000;
        List<string> StockSymbols = new List<string>();

        #endregion

        /// <summary>
        /// Gets the stocks.
        /// </summary>
        /// <value>The stocks.</value>
        public ObservableCollection<StockData> Stocks
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
                RaisePropertyChanged("Stocks");
            }
        }

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StocksViewModel"/> class.
        /// </summary>
        public StocksViewModel()
        {
            this.data = new ObservableCollection<StockData>();
            this.AddRows(200);
            this.timer = new DispatcherTimer();
            this.ResetRefreshFrequency(1000);
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += timer_Tick;

        }

        #endregion

        #region Timer and updating code

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void StartTimer()
        {
            if (!this.timer.IsEnabled)
            {
                this.timer.Start();
            }
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void StopTimer()
        {
            this.timer.Stop();
        }

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void timer_Tick(object sender, object e)
        {
            int startTime = DateTime.Now.Millisecond;
            ChangeRows(noOfUpdates);
        }

        /// <summary>
        /// Adds the rows.
        /// </summary>
        /// <param name="count">The count.</param>
        private void AddRows(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                var newRec = new StockData();
                newRec.Symbol = ChangeSymbol();
                newRec.Trade1 = Math.Round(r.NextDouble() * 30, 2);
                newRec.Trade2 = Math.Round(r.NextDouble() * 12, 2);
                newRec.Trade3 = Math.Round(r.NextDouble() * 34, 2);
                newRec.Trade4 = Math.Round(r.NextDouble() * 56, 2);
                newRec.Trade5 = Math.Round(r.NextDouble() * 76, 2);
                newRec.Trade6 = Math.Round(r.NextDouble() * 33, 2);
                newRec.Trade7 = Math.Round(r.NextDouble() * 76, 2);
                newRec.Trade8 = Math.Round(r.NextDouble() * 26, 2);
                newRec.Trade9 = Math.Round(r.NextDouble() * 25, 2);
                newRec.Trade10 = Math.Round(r.NextDouble() * 32, 2);
                newRec.Trade11 = Math.Round(r.NextDouble() * 46, 2);
                newRec.Trade12 = Math.Round(r.NextDouble() * 52, 2);
                newRec.Trade13 = Math.Round(r.NextDouble() * 76, 2);
                newRec.Trade14 = Math.Round(r.NextDouble() * 21, 2);
                newRec.Trade15 = Math.Round(r.NextDouble() * 32, 2);
                newRec.Trade16 = Math.Round(r.NextDouble() * 31, 2);
                newRec.Trade17 = Math.Round(r.NextDouble() * 23, 2);
                newRec.Trade18 = Math.Round(r.NextDouble() * 51, 2);
                newRec.Trade19 = Math.Round(r.NextDouble() * 20, 2);
                newRec.Trade20 = Math.Round(r.NextDouble() * 30, 2);
                newRec.Trade21 = Math.Round(r.NextDouble() * 26, 2);
                newRec.Trade22 = Math.Round(r.NextDouble() * 42, 2);
                newRec.Trade23 = Math.Round(r.NextDouble() * 43, 2);
                newRec.Trade24 = Math.Round(r.NextDouble() * 12, 2);
                newRec.Trade25 = Math.Round(r.NextDouble() * 16, 2);
                newRec.Trade26 = Math.Round(r.NextDouble() * 19, 2);
                newRec.Trade27 = Math.Round(r.NextDouble() * 49, 2);
                newRec.Trade28 = Math.Round(r.NextDouble() * 64, 2);
                newRec.Trade29 = Math.Round(r.NextDouble() * 13, 2);
                data.Add(newRec);
            }
        }

        /// <summary>
        /// Changes the symbol.
        /// </summary>
        /// <returns></returns>
        private String ChangeSymbol()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;

            do
            {
                builder = new StringBuilder();
                for (int i = 0; i < 4; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }

            } while (StockSymbols.Contains(builder.ToString()));

            StockSymbols.Add(builder.ToString());
            return builder.ToString();
        }


        /// <summary>
        /// Resets the refresh frequency.
        /// </summary>
        /// <param name="changesPerTick">The changes per tick.</param>
        public void ResetRefreshFrequency(int changesPerTick)
        {
            if (this.timer == null)
            {
                return;
            }

            if (!this.noOfUpdates.Equals(changesPerTick))
            {
                this.StopTimer();
                this.noOfUpdates = changesPerTick;
                if (enableTimer)
                    this.StartTimer();
            }
        }


        /// <summary>
        /// Changes the rows.
        /// </summary>
        /// <param name="count">The count.</param>
        private void ChangeRows(int count)
        {
             if (data.Count < count)
                count = data.Count;
            for (int i = 0; i < count; ++i)
            {
                int recNo = r.Next(data.Count);
                var properties = data[recNo].GetType().GetProperties();
                int propertycount = p1.Next(properties.Count());
                if (propertycount > 0 && propertycount < 30)
                {
                    properties[propertycount].SetValue((data[recNo] as StockData), Convert.ChangeType(Math.Round(r.NextDouble() * 33, 2), typeof(Double)), null);
                    properties[propertycount].SetValue((data[recNo] as StockData), Convert.ChangeType(Math.Round(r.NextDouble() * 43, 2), typeof(Double)),null);
                    properties[propertycount].SetValue((data[recNo] as StockData), Convert.ChangeType(Math.Round(r.NextDouble() * 3, 2), typeof(Double)), null);
                    properties[propertycount].SetValue((data[recNo] as StockData), Convert.ChangeType(Math.Round(r.NextDouble() * 12, 2), typeof(Double)), null);
                    properties[propertycount].SetValue((data[recNo] as StockData), Convert.ChangeType(Math.Round(r.NextDouble() * 64, 2), typeof(Double)), null);
                    properties[propertycount].SetValue((data[recNo] as StockData), Convert.ChangeType(Math.Round(r.NextDouble() * 66, 2), typeof(Double)), null);
                    properties[propertycount].SetValue((data[recNo] as StockData), Convert.ChangeType(Math.Round(r.NextDouble() * 44, 2), typeof(Double)), null);
                }
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (this.timer != null)
            {
                this.timer.Tick -= timer_Tick;
                this.StopTimer();
            }
        }

        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

    /// <summary>
    /// This class represents the PerformanceStocksViewModel
    /// </summary>
    public  class PerformanceStocksViewModel : IDisposable, INotifyPropertyChanged
    {
        #region Members

        ObservableCollection<PerformanceStockData> data;
        Random r = new Random(123345345);
        internal DispatcherTimer timer;
        private bool enableTimer = false;
        private DelegateCommand<object> btonClick;
        private double timerValue;
        private string _ButtonContnt;
        private int noOfUpdates = 500;
        List<string> StockSymbols = new List<string>();
        string[] accounts = new string[]{
            "AmericanFunds",
            "ChildrenCollegeSavings",
            "DayTrading",
            "RetirementSavings",
            "MountainRanges",
            "FidelityFunds",
            "Mortages",
            "HousingLoans"
        };
        
        #endregion

        /// <summary>
        /// Gets the stocks.
        /// </summary>
        /// <value>The stocks.</value>
        public ObservableCollection<PerformanceStockData> Stocks
        {
            get
            {
                return this.data;
            }
        }

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceStocksViewModel"/> class.
        /// </summary>
        public PerformanceStocksViewModel()
        {
            this.data = new ObservableCollection<PerformanceStockData>();
            this.AddRows(2000);
            this.timer = new DispatcherTimer();
            this.ResetRefreshFrequency(2500);
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += timer_Tick;
            btonClick = new DelegateCommand<object>(ButtonClicked, CanButtonClick);


        }

        #endregion

        #region Timer and updating code

        /// <summary>
        /// Sets the interval.
        /// </summary>
        /// <param name="time">The time.</param>
        public void SetInterval(TimeSpan time)
        {
            this.timer.Interval = time;
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void StartTimer()
        {
            if (!this.timer.IsEnabled)
            {
                this.timer.Start();
                this.ButtonContnt = "Stop Timer";
            }
        }

        /// <summary>
        /// Gets or sets the timer value.
        /// </summary>
        /// <value>The timer value.</value>
        public double TimeSpanValue
        {
            get
            {
                return timerValue;
            }
            set
            {
                timerValue = value;
                this.timer.Interval = TimeSpan.FromMilliseconds(timerValue);
                RaisePropertyChanged("TimeSpanValue");
            }
        }

        /// <summary>
        /// Gets or sets the button contnt.
        /// </summary>
        /// <value>The button contnt.</value>
        public string ButtonContnt
        {
            get
            {
                return _ButtonContnt;
            }
            set
            {
                _ButtonContnt = value;
                RaisePropertyChanged("ButtonContnt");
            }
        }

        /// <summary>
        /// Gets the bton click.
        /// </summary>
        /// <value>The bton click.</value>
        public DelegateCommand<object> BtonClick
        {
            get { return btonClick; }
        }

        /// <summary>
        /// Determines whether this instance [can button click].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance [can button click]; otherwise, <c>false</c>.
        /// </returns>
        bool CanButtonClick(object param)
        {
            return true;
        }

        /// <summary>
        /// Buttons the clicked.
        /// </summary>
        /// <param name="param">The param.</param>
        /// 
        void ButtonClicked(object param)
        {
            if (ButtonContnt.Equals("Start Timer"))
            {
                this.EnableTimer = true;

                this.StartTimer();
                ButtonContnt = "Stop Timer";
            }
            else
            {
                this.EnableTimer = false;

                this.StopTimer();
                ButtonContnt = "Start Timer";
            }
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void StopTimer()
        {
            this.timer.Stop();
        }

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void timer_Tick(object sender, object e)
        {
            int startTime = DateTime.Now.Millisecond;
            ChangeRows(noOfUpdates);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable timer].
        /// </summary>
        /// <value><c>true</c> if [enable timer]; otherwise, <c>false</c>.</value>
        public bool EnableTimer
        {
            get
            {
                return this.enableTimer;
            }
            set
            {
                this.enableTimer = value;
            }
        }

        /// <summary>
        /// Adds the rows.
        /// </summary>
        /// <param name="count">The count.</param>
        private void AddRows(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                var newRec = new PerformanceStockData();
                newRec.Symbol = ChangeSymbol();
                newRec.Account = ChangeAccount(i);
                newRec.Open = Math.Round(r.NextDouble() * 30, 2);
                newRec.LastTrade = Math.Round((1 + r.NextDouble() * 50));
                double d = r.NextDouble();
                if (d < .5)
                    newRec.Change = Math.Round(d, 2);
                else
                    newRec.Change = Math.Round(d, 2) * -1;

                newRec.PreviousClose = Math.Round(r.NextDouble() * 30, 2);
                newRec.Volume = r.Next();
                data.Add(newRec);
            }
        }

        /// <summary>
        /// Changes the symbol.
        /// </summary>
        /// <returns></returns>
        private String ChangeSymbol()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;

            do
            {
                builder = new StringBuilder();
                for (int i = 0; i < 4; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }

            } while (StockSymbols.Contains(builder.ToString()));

            StockSymbols.Add(builder.ToString());
            return builder.ToString();
        }

        /// <summary>
        /// Changes the account.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private String ChangeAccount(int index)
        {
            return accounts[index % accounts.Length];
        }

        /// <summary>
        /// Resets the refresh frequency.
        /// </summary>
        /// <param name="changesPerTick">The changes per tick.</param>
        public void ResetRefreshFrequency(int changesPerTick)
        {
            if (this.timer == null)
            {
                return;
            }

            if (!this.noOfUpdates.Equals(changesPerTick))
            {
                this.StopTimer();
                this.noOfUpdates = changesPerTick;
                if (enableTimer)
                    this.StartTimer();
            }
        }

        public object SelectedItem
        {
            get
            {
                return noOfUpdates;
            }
            set
            {
                noOfUpdates = int.Parse(((ComboBoxItem)value).Content.ToString());
                RaisePropertyChanged("SelectedItem");
            }
        }

        public List<int> ComboCollection
        {
            get
            {
                return new List<int> { 500, 5000, 50000, 500000 };
            }
        }

        /// <summary>
        /// Changes the rows.
        /// </summary>
        /// <param name="count">The count.</param>
        private void ChangeRows(int count)
        {
            if (data.Count < count)
                count = data.Count;
            for (int i = 0; i < count; ++i)
            {
                int recNo = r.Next(data.Count);
                PerformanceStockData recRow = data[recNo];

                data[recNo].LastTrade = Math.Round((1 + r.NextDouble() * 50));

                double d = r.NextDouble();
                if (d < .5)
                    data[recNo].Change = Math.Round(d, 2);
                else
                    data[recNo].Change = Math.Round(d, 2) * -1;
                data[recNo].Open = Math.Round(r.NextDouble() * 50, 2);
                data[recNo].PreviousClose = Math.Round(r.NextDouble() * 30, 2);
                data[recNo].Volume = r.Next();
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (this.timer != null)
            {
                this.timer.Tick -= timer_Tick;
                this.StopTimer();
            }
            this.data.Clear();
            this.data = null;
        }

        #endregion
    }
}
