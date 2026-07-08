using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DataGrid
{
    /// <summary>
    /// This class represents the DelegateCommand
    /// </summary>
    public class DelegateCommand : ICommand
    {

        Func<object, bool> canExecute;

        Action<object> executeAction;



        Func<bool> canExecuteSimple;

        Action executeActionSimple;



        public DelegateCommand(Action<object> executeAction)

            : this(executeAction, null)
        {

        }



        public DelegateCommand(Action executeAction)

            : this(executeAction, null)
        {

        }



        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {

            if (executeAction == null)
            {

                throw new ArgumentNullException("executeAction");

            }

            this.executeAction = executeAction;

            this.canExecute = canExecute;

        }



        public DelegateCommand(Action executeAction, Func<bool> canExecute)
        {

            if (executeAction == null)
            {

                throw new ArgumentNullException("executeAction");

            }

            this.executeActionSimple = executeAction;

            this.canExecuteSimple = canExecute;

        }



        public bool CanExecute(object parameter)
        {

            bool result = true;

            Func<object, bool> canExecuteHandler = this.canExecute;

            if (canExecuteHandler != null)
            {

                result = canExecuteHandler(parameter);

                return result;

            }



            Func<bool> canExecuteHandlerSimple = this.canExecuteSimple;

            if (canExecuteHandlerSimple != null)
            {

                result = canExecuteHandlerSimple();

            }



            return result;

        }



        public bool CanExecute()
        {

            bool result = true;

            Func<bool> canExecuteHandler = this.canExecuteSimple;

            if (canExecuteHandler != null)
            {

                result = canExecuteHandler();

            }



            return result;

        }





        public event EventHandler CanExecuteChanged;



        public void RaiseCanExecuteChanged()
        {

            EventHandler handler = this.CanExecuteChanged;

            if (handler != null)
            {

                handler(this, new EventArgs());

            }

        }



        public void Execute(object parameter)
        {

            // Default to action that takes parameter.

            if (this.executeAction != null)
            {

                this.executeAction(parameter);

                return;

            }



            // Fallback to parameterless delegate.

            if (this.executeActionSimple != null)
            {

                this.executeActionSimple();

                return;

            }

        }



        public void Execute()
        {

            this.executeActionSimple();

        }

    }

    /// <summary>
    /// This class represents the ToggleEx
    /// </summary>
    public class ToggleEx
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(ToggleEx), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ToggleSwitch toggleSwitch = depObj as ToggleSwitch;
            if (toggleSwitch != null)
                toggleSwitch.Toggled += toggleSwitch_Toggled;
        }

        static void toggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch Switch = (sender as ToggleSwitch);
            if (Switch != null)
            {
                ICommand command = Switch.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(Switch.IsOn);
                }
            }
        }


        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }
}
