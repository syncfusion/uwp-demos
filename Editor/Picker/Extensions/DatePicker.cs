using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input.Picker
{
	public class CustomDatePicker : SfPicker
	{
		public ObservableCollection<object> Date { get; set; }

		public ObservableCollection<object> Day;
		public ObservableCollection<object> Month;
		public ObservableCollection<object> Year;
        public static Dictionary<string, string> months;
        public ObservableCollection<string> Headers { get; set; }
        
		public CustomDatePicker()
		{
            months = new Dictionary<string, string>();
            Date = new ObservableCollection<object>();
			Day = new ObservableCollection<object>();
			Month = new ObservableCollection<object>();
			Year = new ObservableCollection<object>();
			Headers = new ObservableCollection<string>();
            

			Headers.Add("Month");
			Headers.Add("Day");
			Headers.Add("Year");
			PopulateDateCollection();
			this.ItemsSource = Date;
			this.ColumnHeaderText = Headers;
            
            this.SelectionChanged += CustomDatePicker_SelectionChanged;
        }

        private void CustomDatePicker_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            UpdateDays(Date, e);
        }

        private void PopulateDateCollection()
		{
            //populate months

            for (int i = 1; i < 13; i++)
            {
                months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                Month.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3));
            }

            //populate year
            for (int i = 1990; i < 2050; i++)
            {
                Year.Add(i.ToString());
            }

            //populate Days
            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            {
                if (i < 10)
                {
                    Day.Add("0" + i);
                }
                else
                    Day.Add(i.ToString());
            }

            Date.Add(Month);
            Date.Add(Day);
            Date.Add(Year);
        }

        public static void UpdateDays(ObservableCollection<object> Date, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
                try
                {
                    bool isupdate = false;
                    if (e.RemovedItems[0] != null && e.AddedItems[0] != null)
                    {
                        if ((e.RemovedItems[0] as IList)[0] != (e.AddedItems[0] as IList)[0])
                        {
                            isupdate = true;
                        }

                        if ((e.RemovedItems[0] as IList)[2] != (e.AddedItems[0] as IList)[2])
                        {
                            isupdate = true;
                        }
                    }

                    if (isupdate)
                    {
                        ObservableCollection<object> days = new ObservableCollection<object>();
                        int month = DateTime.ParseExact(CustomDatePicker.months[(e.AddedItems[0] as IList)[0].ToString()], "MMMM", CultureInfo.InvariantCulture).Month;
                    int year = int.Parse((e.AddedItems[0] as IList)[2].ToString());
                        for (int j = 1; j <= DateTime.DaysInMonth(year, month); j++)
                        {
                            if (j < 10)
                            {
                                days.Add("0" + j);
                            }
                            else
                                days.Add(j.ToString());
                        }

                        if (days.Count > 0)
                        {
                            Date.RemoveAt(1);
                            Date.Insert(1, days);
                        }
                    }
                }
                catch
                {

                }

        }

        public void Dispose()
        {
            Date.Clear();
            Day.Clear();
            Month.Clear();
            months.Clear();
            Year.Clear();
            Headers.Clear();
            Date = null;
            Day = null;
            months = null;
            Month = null;
            Year = null;
            Headers = null;
            this.SelectionChanged -= CustomDatePicker_SelectionChanged;
            base.Dispose();
        }
}
}
