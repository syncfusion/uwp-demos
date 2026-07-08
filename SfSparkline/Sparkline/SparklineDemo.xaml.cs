#region Copyright Syncfusion Inc. 2001-2017.
// Copyright Syncfusion Inc. 2001-2017. All rights reserved.
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

namespace Syncfusion.SampleBrowser.UWP.SfSparkline
{
    public sealed partial class SparklineDemo : SampleLayout
    {
        public SparklineDemo()
        {
            this.InitializeComponent();
            this.DataContext = new SparkViewModel();
        }
        public override void Dispose()
        {
            if(LayoutRoot != null)
            {
                var dataContext = LayoutRoot.DataContext as SparkViewModel;
                if (dataContext != null)
                    dataContext.Dispose();
                if (Column != null)
                    Column.ClearValue(SparklineBase.ItemsSourceProperty);
                Column = null;
                if (Line != null)
                    Line.ClearValue(SparklineBase.ItemsSourceProperty);
                Line = null;
                if (Area != null)
                    Area.ClearValue(SparklineBase.ItemsSourceProperty);
                Area = null;
                if (WinLoss != null)
                    WinLoss.ClearValue(SparklineBase.ItemsSourceProperty);
                WinLoss = null;
            }
            
            base.Dispose();
        }
    }

    public class DataModel
    {
        private double day;

        public double Day
        {
            get
            {
                return day;
            }
            set
            {
                this.day = value;
            }
        }

        private double shareHolders;

        public double ShareHolders
        {
            get
            {
                return shareHolders;
            }
            set
            {
                this.shareHolders = value;
            }
        }


        private double yearPerformance;

        public double YearPerformance
        {
            get
            {
                return yearPerformance;
            }
            set
            {
                this.yearPerformance = value;
            }
        }
    }

    public class SparkModel
    {
        public SparkModel()
        {
            this.DayActivity = new ObservableCollection<DataModel>();
            this.Transaction = new ObservableCollection<DataModel>();
            this.OneYearPerformance = new ObservableCollection<DataModel>();
        }

        private string companyName;

        public string CompanyName
        {
            get
            {
                return this.companyName;
            }
            set
            {
                this.companyName = value;
            }
        }

        private double high;

        public double High
        {
            get
            {
                return this.high;
            }
            set
            {
                this.high = value;
            }
        }

        private DateTime date;

        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
            }
        }

        private double low;

        public double Low
        {
            get
            {
                return this.low;
            }
            set
            {
                this.low = value;
            }
        }

        private double marketCap;

        public double MarketCap
        {
            get
            {
                return this.marketCap;
            }
            set
            {
                this.marketCap = value;
            }
        }

        private double performance;

        public double Performance
        {
            get
            {
                return this.performance;
            }
            set
            {
                this.performance = value;
            }
        }

        private ObservableCollection<DataModel> dayActivity;

        public ObservableCollection<DataModel> DayActivity
        {
            get
            {
                return this.dayActivity;
            }
            set
            {
                this.dayActivity = value;
            }
        }

        private ObservableCollection<DataModel> transaction;

        public ObservableCollection<DataModel> Transaction
        {
            get
            {
                return this.transaction;
            }
            set
            {
                this.transaction = value;
            }
        }

        private ObservableCollection<DataModel> oneYearPerformance;

        public ObservableCollection<DataModel> OneYearPerformance
        {
            get
            {
                return this.oneYearPerformance;
            }
            set
            {
                this.oneYearPerformance = value;
            }
        }
        private double start;

        public double Start
        {
            get
            {
                return start;
            }
            set
            {
                this.start = value;
            }
        }
        private double end;

        public double End
        {
            get
            {
                return end;
            }
            set
            {
                this.end = value;
            }
        }
    }

    public class SparkViewModel
    {
        Random rand = new Random();
        public SparkViewModel()
        {
            ViewModel = new ObservableCollection<SparkModel>();
            string[] cmpName = { "Daren Sys", "ICS Corp", "Ashe Group", "Crane Corp", "Infysi System", "Global Info",
                                 "Alfred Infomartics", "Rany Pharmaticals", "Exos Informatic System", "CISTED Inc", "EDS Sys",
                                 "WestWoods Corp", "Ramiret Group", "JP Foundations", "Missy Master Group", "Jason System",
                                 "Electomatics", "Mac System", "Intellect Corp", "Aksa Group of companies", "SannSystem",
                                 "Daren Sys", "Infysi System", "ICS Corp", "Ashe Group", "Aqua Liquids",  "Ashe Group",
                                 "Crane Corp","Fressi Big Market", "SYSCORP","Swan System","Deck Stocks", "Saun Machines",
                                 "Lincoln Loss System", "Jason System", "Exos Informatic System", "Mac System", "Intellect Corp",
                                 "Aksa Group of companies","Daren Sys", "ICS Corp", "Ashe Group",
                                 "WestWoods Corp", "Ramiret Group","JP Foundations", "Missy Master Group",
                                 "Jason System","Jason System", "Electomatics", "Mac System", "Global Info", "Alfred Infomartics",
                                 "Fressi Big Market", "SYSCORP","Swan System","ICS Corp", "Ashe Group", "Aqua Liquids"};

            for (int i = 1; i < 50; i++)
            {
                SparkModel model = new SparkModel();
                model.CompanyName = cmpName[i];
                model.High = rand.Next(2500, 10000);
                model.Low = rand.Next(1000, 2500);
                model.MarketCap = rand.Next(125, 300);
                model.Date = DateTime.Today.Date;
                model.Performance = rand.Next(10, 100);
                model.Start = rand.Next(0, 600);
                model.End = rand.Next(0, 600);
                model.Transaction.Add(new DataModel() { Day = rand.Next(1000, 8500) });
                model.Transaction.Add(new DataModel() { Day = rand.Next(2000, 9000) });
                model.Transaction.Add(new DataModel() { Day = rand.Next(3000, 8500) });
                model.Transaction.Add(new DataModel() { Day = rand.Next(4000, 9000) });
                model.Transaction.Add(new DataModel() { Day = rand.Next(5000, 8000) });
                model.Transaction.Add(new DataModel() { Day = rand.Next(6000, 13000) });
                model.Transaction.Add(new DataModel() { Day = rand.Next(1120, 9000) });
                model.Transaction.Add(new DataModel() { Day = rand.Next(2500, 12000) });
                model.Transaction.Add(new DataModel() { Day = rand.Next(500, 3000) });
                model.Transaction.Add(new DataModel() { Day = rand.Next(1000, 12000) });
                model.Transaction.Add(new DataModel() { Day = rand.Next(3000, 9000) });

                model.DayActivity.Add(new DataModel() { ShareHolders = rand.Next(12, 580) });
                model.DayActivity.Add(new DataModel() { ShareHolders = rand.Next(0, 350) });
                model.DayActivity.Add(new DataModel() { ShareHolders = rand.Next(30, 450) });
                model.DayActivity.Add(new DataModel() { ShareHolders = rand.Next(40, 350) });
                model.DayActivity.Add(new DataModel() { ShareHolders = rand.Next(10, 600) });
                model.DayActivity.Add(new DataModel() { ShareHolders = rand.Next(20, 590) });
                model.DayActivity.Add(new DataModel() { ShareHolders = rand.Next(15, 450) });
                model.DayActivity.Add(new DataModel() { ShareHolders = rand.Next(11, 440) });
                model.DayActivity.Add(new DataModel() { ShareHolders = rand.Next(10, 600) });
                model.DayActivity.Add(new DataModel() { ShareHolders = rand.Next(20, 590) });
                model.DayActivity.Add(new DataModel() { ShareHolders = rand.Next(15, 450) });
                model.DayActivity.Add(new DataModel() { ShareHolders = rand.Next(11, 440) });

                model.OneYearPerformance.Add(new DataModel() { YearPerformance = (double)rand.Next(-20, 100) });
                model.OneYearPerformance.Add(new DataModel() { YearPerformance = (double)rand.Next(-20, 60) });
                model.OneYearPerformance.Add(new DataModel() { YearPerformance = (double)rand.Next(-30, 150) });
                model.OneYearPerformance.Add(new DataModel() { YearPerformance = (double)rand.Next(-20, 100) });
                model.OneYearPerformance.Add(new DataModel() { YearPerformance = (double)rand.Next(-20, 100) });
                model.OneYearPerformance.Add(new DataModel() { YearPerformance = (double)rand.Next(-20, 100) });
                model.OneYearPerformance.Add(new DataModel() { YearPerformance = (double)rand.Next(0, 200) });
                model.OneYearPerformance.Add(new DataModel() { YearPerformance = (double)rand.Next(-20, 300) });
                model.OneYearPerformance.Add(new DataModel() { YearPerformance = (double)rand.Next(-20, 100) });
                model.OneYearPerformance.Add(new DataModel() { YearPerformance = (double)rand.Next(-30, 150) });
                model.OneYearPerformance.Add(new DataModel() { YearPerformance = (double)rand.Next(-20, 300) });

                ViewModel.Add(model);
            }
        }

        public void Dispose()
        {
            if (ViewModel != null)
                ViewModel.Clear();
        }
        public ObservableCollection<SparkModel> ViewModel
        {
            get;
            set;
        }

    }
}
