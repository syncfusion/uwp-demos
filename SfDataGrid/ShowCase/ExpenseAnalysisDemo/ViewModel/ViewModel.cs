using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using System.Collections.ObjectModel;
using Syncfusion.UI.Xaml.Grid;

namespace ExpenseAnalysisDemo
{
    public class ViewModel : NotificationObject, IDisposable
    {
        DelegateCommamd _filterCommand;
        DelegateCommamd _exportCommand;

        public ViewModel()
        {
            _filterCommand = new DelegateCommamd(ApplyFilter);
            _exportCommand = new DelegateCommamd(ExecuteExportCommand);
            Expenses = (new ExpenseData()).GenerateExpenseData(new DateTime(2013, 1, 1), new DateTime(2013, 12, 31), 2);
            TotalExpenses = Expenses;
            UpdateTransactionDetails();
            Months = new ObservableCollection<string>()
            {
               "All","January","February","March","April","May","June","July","August","September","October","November","December"
            };
        }

        public string _selectedMonth;
        public string SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                _selectedMonth = value;
                RaisePropertyChanged("SelectedMonth");
                ApplyFilter(_selectedMonth);
            }
        }

        public ObservableCollection<string> Months { get; set; }

        private void UpdateTransactionDetails()
        {
            PositiveAmount = Expenses.Where(x => x.AccountType == AccountType.Positve).Sum(x => x.Amount);
            NegativeAmount = Expenses.Where(x => x.AccountType == AccountType.Negative).Sum(x => x.Amount);
            BalanceAmount = PositiveAmount - NegativeAmount;
            NoPositiveTransactions = Expenses.Where(x => x.AccountType == AccountType.Positve).Count();
            NoNegativeTransactions = Expenses.Where(x => x.AccountType == AccountType.Negative).Count();
            NoTotalTransactions = NoPositiveTransactions + NoNegativeTransactions;
        }

        private ObservableCollection<ExpenseData> TotalExpenses;
        private ObservableCollection<ExpenseData> _expenses;
        public ObservableCollection<ExpenseData> Expenses
        {
            get { return _expenses; }
            set { _expenses = value; RaisePropertyChanged("Expenses"); }
        }

        private double _positiveAmount;
        public double PositiveAmount
        {
            get { return _positiveAmount; }
            set { _positiveAmount = value; RaisePropertyChanged("PositiveAmount"); }
        }

        private double _negativeAmount;
        public double NegativeAmount
        {
            get { return _negativeAmount; }
            set { _negativeAmount = value; RaisePropertyChanged("NegativeAmount"); }
        }

        private double _balanceAmount;
        public double BalanceAmount
        {
            get { return _balanceAmount; }
            set { _balanceAmount = value; RaisePropertyChanged("BalanceAmount"); }
        }

        private int _noPositiveTransactions;
        public int NoPositiveTransactions
        {
            get { return _noPositiveTransactions; }
            set { _noPositiveTransactions = value; RaisePropertyChanged("NoPositiveTransactions"); }
        }

        private int _noNegativeTransactions;
        public int NoNegativeTransactions
        {
            get { return _noNegativeTransactions; }
            set { _noNegativeTransactions = value; RaisePropertyChanged("NoNegativeTransactions"); }
        }

        private int _noTotalTransactions;
        public int NoTotalTransactions
        {
            get { return _noTotalTransactions; }
            set { _noTotalTransactions = value; RaisePropertyChanged("NoTotalTransactions"); }
        }

        public DelegateCommamd FilterCommand
        {
            get { return _filterCommand; }
        }

        public DelegateCommamd ExportCommand
        {
            get { return _exportCommand; }
        }

        public Action ExportAction { get; set; }

        private void ApplyFilter(object param)
        {
            var filterString = param.ToString();
            if(filterString == "All")
            {
                Expenses = TotalExpenses;
            }
            else
            {
                Expenses = new ObservableCollection<ExpenseData>(TotalExpenses.Where(item => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.DateTime.Month).ToLower() == filterString.ToLower()));
            }
            UpdateTransactionDetails();
        }

        private void ExecuteExportCommand(object param)
        {
            if (ExportAction != null)
                ExportAction();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (Months != null)
                this.Months.Clear();
            if (this.TotalExpenses != null)
                this.TotalExpenses.Clear();
            if (this.Expenses != null)
                this.Expenses.Clear();
        }
    }

    public class CompanyExpense
    {
        public string Category { get; set; }
        public double Amount { get; set; }
    }

    
}
