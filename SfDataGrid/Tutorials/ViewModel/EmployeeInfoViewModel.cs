using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace DataGrid
{
    /// <summary>
    /// This class represents the EmployeeInfoViewModel
    /// </summary>
    class EmployeeInfoViewModel : INotifyPropertyChanged,IDisposable
    {
        public EmployeeInfoViewModel()
        {
            EmployeeDetails = new EmployeeInfoRepository().GetEmployeesDetails(2000);
            EmployeeDetails200 = new EmployeeInfoRepository().GetEmployeesDetails(200);
#if !SFDATAGRID_STORE
            _comboBoxItemsSource.Add("Male");
            _comboBoxItemsSource.Add("Female");
#endif
        }
#if !SFDATAGRID_STORE

        private List<string> _comboBoxItemsSource = new List<string>();

        /// <summary>
        /// Get or set the ComboBoxItemSource
        /// </summary>
        public List<string> ComboBoxItemsSource
        {
            get { return _comboBoxItemsSource; }
            set { _comboBoxItemsSource = value; }
        }
        

        internal FilterChanged filterChanged;
        private ICommand textchanged;

        public ICommand TextChanged
        {
            get { return new DelegateCommand<object>(TextChangeEvent, args => true); }
            set { textchanged = value; OnPropertyChanged("TextChanged"); }
        }

        public ICommand ComboChanged
        {
            get { return new DelegateCommand<object>(ComboxChangedEvent, args => { return true; }); }
        }

        public ICommand FilterComboChanged
        {
            get { return new DelegateCommand<object>(FilterComboxChangedEvent, args => { return true; }); }
        }

        /// <summary>
        /// Occurs when the ComboBox item is changed.
        /// </summary>
        /// <param name="pram"></param>
        private void FilterComboxChangedEvent(object pram)
        {
            if (pram != null)
            {
                this.FilterOption = (pram as ComboBoxItem).Content.ToString();
                filterChanged();
            }
        }


        
        /// <summary>
        /// Occurs when comboBox item is changed
        /// </summary>
        /// <param name="pram"></param>
        private void ComboxChangedEvent(object pram)
        {
            if (pram != null)
            {
                this.FilterCondition = (pram as ComboBoxItem).Content.ToString();
                filterChanged();
            }
        }


        /// <summary>
        /// Occurs when the Text is changed
        /// </summary>
        /// <param name="pram"></param>
        private void TextChangeEvent(object pram)
        {
            if (pram != null)
            {
                this.FilterText = pram.ToString();
                filterChanged();
            }
        }

        private bool MakeStringFilter(Employees o, string option, string condition)
        {
            var value = (o.GetType().GetTypeInfo()).GetDeclaredProperty(option);
            var exactValue = value.GetValue(o);
            exactValue = exactValue.ToString().ToLower();
            string text = FilterText.ToLower();
            var methods = (typeof(string).GetTypeInfo()).GetDeclaredMethods(condition);
            if (methods.Count() != 0)
            {
                var methodInfo = methods.FirstOrDefault();
                bool result1 = (bool)methodInfo.Invoke(exactValue, new object[] { text });
                return result1;
            }
            else
                return false;
        }

        private bool MakeNumericFilter(Employees o, string option, string condition)
        {
            var value = (o.GetType().GetTypeInfo()).GetDeclaredProperty(option);
            var exactValue = value.GetValue(o);
            double res;
            bool checkNumeric = double.TryParse(exactValue.ToString(), out res);
            if (checkNumeric)
            {
                switch (condition)
                {
                    case "Equals":
                        if (Convert.ToDouble(exactValue) == (Convert.ToDouble(FilterText)))
                            return true;
                        break;
                    case "GreaterThan":
                        if (Convert.ToDouble(exactValue) > Convert.ToDouble(FilterText))
                            return true;
                        break;
                    case "LessThan":
                        if (Convert.ToDouble(exactValue) < Convert.ToDouble(FilterText))
                            return true;
                        break;
                    case "NotEquals":
                        if (Convert.ToDouble(FilterText) != Convert.ToDouble(exactValue))
                            return true;
                        break;
                }
            }
            return false;
        }

        public bool FilerRecords(object o)
        {
            double res;
            bool checkNumeric = double.TryParse(FilterText, out res);
            var item = o as Employees;
            if (item != null && FilterText.Equals(""))
            {
                return true;
            }
            else
            {
                if (item != null)
                {
                    if (checkNumeric && !FilterOption.Equals("All Columns"))
                    {
                        if (FilterCondition == null || FilterCondition.Equals("Contains") || FilterCondition.Equals("StartsWith") || FilterCondition.Equals("EndsWith"))
                            FilterCondition = "Equals";
                        bool result = MakeNumericFilter(item, FilterOption, FilterCondition);
                        return result;
                    }
                    else if (FilterOption.Equals("All Columns"))
                    {
                        if (item.Name.ToLower().Contains(FilterText.ToLower()) ||
                            item.Title.ToLower().Contains(FilterText.ToLower()) ||
                            item.Salary.ToString().ToLower().Contains(FilterText.ToLower()) || item.EmployeeID.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.Gender.ToLower().Contains(FilterText.ToLower()) )
                            return true;
                        return false;
                    }
                    else
                    {
                        if (FilterCondition == null || FilterCondition.Equals("Equals") || FilterCondition.Equals("LessThan") || FilterCondition.Equals("GreaterThan") || FilterCondition.Equals("NotEquals"))
                            FilterCondition = "Contains";
                        bool result = MakeStringFilter(item, FilterOption, FilterCondition);
                        return result;
                    }
                }
            }
            return false;
        }

        private string filterOption = "All Columns";

        /// <summary>
        /// Get or set the FilterOption
        /// </summary>
        public string FilterOption
        {
            get { return filterOption; }
            set
            {
                filterOption = value;
                OnPropertyChanged("FilterOption");
            }
        }

        private string filterText = string.Empty;
        /// <summary>
        /// Get or set the FilterText
        /// </summary>
        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                OnPropertyChanged("FilterText");
            }
        }

        private string filterCondition;
        /// <summary>
        /// Get or set the FilterCondition
        /// </summary>
        public string FilterCondition
        {
            get { return filterCondition; }
            set
            {
                filterCondition = value;
                OnPropertyChanged("FilterCondition");
            }
        }
#endif
        private ObservableCollection<Employees> employeeDetails;
        /// <summary>
        /// Get or set the EmployeeDetails
        /// </summary>
        public ObservableCollection<Employees> EmployeeDetails
        {
            get
            {
                return employeeDetails;
            }
            set
            {
                employeeDetails = value;
                OnPropertyChanged("EmployeeDetails");
            }
        }

        private ObservableCollection<Employees> employeeDetails200;
        /// <summary>
        /// Get or set the EmployeeDetails200
        /// </summary>
        public ObservableCollection<Employees> EmployeeDetails200
        {
            get
            {
                return employeeDetails200;
            }
            set
            {
                employeeDetails200 = value;
                OnPropertyChanged("EmployeeDetails200");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (EmployeeDetails != null)
            {
                EmployeeDetails.Clear();
            }
        }
    }
    internal delegate void FilterChanged();
}
