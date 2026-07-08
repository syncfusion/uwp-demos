
using Common;
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Input.Picker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PopupPicker : SampleLayout
    {
        bool isvaluechange;
        ObservableCollection<object> todaycollection = new ObservableCollection<object>();
        public PopupPicker()
        {
            this.InitializeComponent();
            startdate.Closed += Startdate_OnPopUpClosed;
            enddate.Closed += Enddate_OnPopUpClosed;
            startdate.Opened += Startdate_DialogOpened;
            enddate.Opened += Startdate_DialogOpened;
            startdate.CancelButtonClicked += Startdate_CancelButtonClicked;
            enddate.CancelButtonClicked += Enddate_CancelButtonClicked;
            startdate.OkButtonClicked += startdate_OkButtonClicked;
            enddate.OkButtonClicked += enddate_OkButtonClicked;
            enddate.CancelButtonClicked += Enddate_CancelButtonClicked;
            this.DataContext = new ViewModel();

            startdatetxt.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Date.Month).Substring(0, 3) + " " + DateTime.Now.Day.ToString() + " " + DateTime.Now.Year.ToString();
            enddatetxt.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Date.Month).Substring(0, 3) + " " + DateTime.Now.Day.ToString() + " " + DateTime.Now.Year.ToString();
            //Select today dates
            todaycollection.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Date.Month).Substring(0, 3));
            if (DateTime.Now.Date.Day < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Day);
            else
                todaycollection.Add(DateTime.Now.Date.Day.ToString());
            todaycollection.Add(DateTime.Now.Date.Year.ToString());

            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                stack.HorizontalAlignment = HorizontalAlignment.Center;
                startdate.Width = 280;
                startdate.Height = 300;
                enddate.Width = 280;
                enddate.Height = 300;
            }

            //startdatetxt.Text = GetStringfromCollection((this.DataContext as ViewModel).StartDate);
            //enddatetxt.Text = GetStringfromCollection((this.DataContext as ViewModel).EndDate);
        }

        private void Startdate_DialogOpened(object sender, RoutedEventArgs e)
        {
            isvaluechange = false;
        }

        private void Enddate_CancelButtonClicked(object source, SelectionChangedEventArgs args)
        {
            isvaluechange = true;
            (this.DataContext as ViewModel).EndDate = GetCollectionfromstring(enddatetxt.Text);
        }

        private void Startdate_CancelButtonClicked(object source, SelectionChangedEventArgs args)
        {
            isvaluechange = true;
            (this.DataContext as ViewModel).StartDate = GetCollectionfromstring(startdatetxt.Text);
        }

        private void Startdate_OnPopUpClosed(object sender, RoutedEventArgs e)
        {
            if (!isvaluechange)
            {
                if (string.IsNullOrEmpty(startdatetxt.Text))
                {
                    (this.DataContext as ViewModel).StartDate = todaycollection;
                }
                else
                {
                    (this.DataContext as ViewModel).StartDate = GetCollectionfromstring(startdatetxt.Text);
                }
            }
        }

        private void Enddate_OnPopUpClosed(object sender, RoutedEventArgs e)
        {
            if (!isvaluechange)
            {
                if (string.IsNullOrEmpty(enddatetxt.Text))
                {
                    (this.DataContext as ViewModel).EndDate = todaycollection;
                }
                else
                {
                    (this.DataContext as ViewModel).EndDate = GetCollectionfromstring(enddatetxt.Text);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            startdate.IsOpen = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            enddate.IsOpen = true;
        }

        public override void Dispose()
        {
            (this.DataContext as ViewModel).StartDate.Clear();
            (this.DataContext as ViewModel).StartDate = null;
            (this.DataContext as ViewModel).EndDate.Clear();
            (this.DataContext as ViewModel).StartDate = null;
            todaycollection.Clear();
            todaycollection = null;
            startdate.Closed -= Startdate_OnPopUpClosed;
            enddate.Closed -= Enddate_OnPopUpClosed;
            startdate.CancelButtonClicked -= Startdate_CancelButtonClicked;
            enddate.CancelButtonClicked -= Enddate_CancelButtonClicked;
            startdate.OkButtonClicked -= startdate_OkButtonClicked;
            enddate.OkButtonClicked -= enddate_OkButtonClicked;
            enddate.CancelButtonClicked -= Enddate_CancelButtonClicked;
            startdate.Dispose();
            enddate.Dispose();
            this.DataContext = null;
            startdate = null;
            enddate = null;
            base.Dispose();
        }

        #region get Collections and String
        ObservableCollection<object> GetCollectionfromList(IList dates)
        {
            ObservableCollection<object> items = new ObservableCollection<object>();
            foreach (var item in dates)
            {
                items.Add(item);
            }

            return items;
        }

        string GetStringfromCollection(ICollection collection)
        {
            string dates = string.Empty;
            foreach (var item in collection)
            {
                dates += item + " ";
            }
            return dates;
        }

        ObservableCollection<object> GetCollectionfromstring(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var str = text.Split(' ').Where(s => !string.IsNullOrEmpty(s));

                ObservableCollection<object> items = new ObservableCollection<object>();
                foreach (var item in str)
                {
                    items.Add(item);
                }
                return items;
            }
            return todaycollection;
        }

        #endregion

        private void startdate_OkButtonClicked(object source, SelectionChangedEventArgs args)
        {
            isvaluechange = true;
            //(this.DataContext as ViewModel).StartDate = GetCollectionfromList(args.AddedItems[0] as IList);
            startdatetxt.Text = GetStringfromCollection((this.DataContext as ViewModel).StartDate);
        }

        private void enddate_OkButtonClicked(object source, SelectionChangedEventArgs args)
        {
            isvaluechange = true;
            //(this.DataContext as ViewModel).EndDate = GetCollectionfromList(args.AddedItems[0] as IList);
            enddatetxt.Text = GetStringfromCollection((this.DataContext as ViewModel).EndDate);
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "Success";
            dialog.Content = "Your vacation has been updated from "+startdatetxt.Text.ToString()+" to "+enddatetxt.Text.ToString();
            dialog.PrimaryButtonText = "Ok";
            await dialog.ShowAsync();
            startdatetxt.Text = GetStringfromCollection(todaycollection);
            enddatetxt.Text = GetStringfromCollection(todaycollection);
        }
    }


    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<object> _startdate;

        public ObservableCollection<object> StartDate
        {
            get { return _startdate; }
            set { _startdate = value; RaisePropertyChanged("StartDate"); }
        }

        private ObservableCollection<object> _enddate;

        public ObservableCollection<object> EndDate
        {
            get { return _enddate; }
            set { _enddate = value; RaisePropertyChanged("EndDate"); }
        }

        void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            ObservableCollection<object> todaycollection = new ObservableCollection<object>();

            //Select today dates
            todaycollection.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Date.Month).Substring(0, 3));
            if (DateTime.Now.Date.Day < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Day);
            else
                todaycollection.Add(DateTime.Now.Date.Day.ToString());
            todaycollection.Add(DateTime.Now.Date.Year.ToString());

            this.StartDate = todaycollection;
            this.EndDate = todaycollection;
        }
    }
}
