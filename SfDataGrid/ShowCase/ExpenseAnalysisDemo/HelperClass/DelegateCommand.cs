using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ExpenseAnalysisDemo
{
    public class DelegateCommamd : ICommand
    {
        Predicate<object> canExecute;
        Action<object> execute;

        public DelegateCommamd(Predicate<object> CanExecute, Action<object> Execute)
        {
            this.canExecute = CanExecute;
            this.execute = Execute;
        }

        public DelegateCommamd(Action<object> Execute)
        {
            this.execute = Execute;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute(parameter);
            return true;
        }

        public void RaiseCanExecuteChanged()
        {
            var CanExecuteChanged = this.CanExecuteChanged;
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (execute != null)
                execute(parameter);
        }
    }
}
