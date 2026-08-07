using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input.Picker
{
	public class CustomTimePicker : SfPicker
	{
		public ObservableCollection<object> Time { get; set; }
		public ObservableCollection<object> Minute;
		public ObservableCollection<object> Hour;
		public ObservableCollection<object> Format;

		public ObservableCollection<string> Headers { get; set; }
        public ObservableCollection<object> SelectedTime { get; set; }
        public CustomTimePicker()
		{
			Time = new ObservableCollection<object>();
			Hour = new ObservableCollection<object>();
			Minute = new ObservableCollection<object>();
			Format = new ObservableCollection<object>();
			Headers = new ObservableCollection<string>();
            SelectedTime = new ObservableCollection<object>();

			Headers.Add("Hour");
			Headers.Add("Minute");
			Headers.Add("Format");
			PopulateTimeCollection();

			this.ItemsSource = Time;
			this.ColumnHeaderText = Headers;

            int hour = int.Parse(DateTime.Now.ToString("hh"));
            SelectedTime.Add(hour.ToString());
            SelectedTime.Add(DateTime.Now.ToString("mm"));
            SelectedTime.Add(DateTime.Now.ToString("tt"));

            this.SelectedItem = SelectedTime;
        }

		private void PopulateTimeCollection()
		{
			for (int i = 1; i <= 12; i++)
			{
				Hour.Add(i.ToString());
			}
			for (int j = 0; j < 60; j++)
			{
				if (j < 10)
				{
					Minute.Add("0" + j);
				}
				else
					Minute.Add(j.ToString());
			}

			Format.Add("AM");
			Format.Add("PM");

			Time.Add(Hour);
			Time.Add(Minute);
			Time.Add(Format);
		}
	}
}
