using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mockup.Utility
{
    internal class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Func<object, bool> canExecute;
        Action<object> executeAction;
        bool canExecuteCache;

        public Command(Action<object> executeAction,
                               Func<object, bool> canExecute = null)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter == null || canExecute == null)
            {
                return true;
            }
            bool tempCanExecute = canExecute(parameter);

            if (canExecuteCache != tempCanExecute)
            {
                canExecuteCache = tempCanExecute;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }

            return canExecuteCache;
        }
        

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
