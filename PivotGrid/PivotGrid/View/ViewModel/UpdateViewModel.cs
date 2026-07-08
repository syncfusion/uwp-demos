#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace BI.PivotGrid.Tutorials.PivotGridSamples.ViewModel
{
    using Model;
    using System;
    using System.Windows.Input;
    using Windows.UI.Xaml;
    /// <summary>
    /// This class contains the view model informations with respect to bind them into SfPivotGrid.
    /// </summary>
    public class UpdateViewModel
    {
        #region Fields

        /// <summary>
        /// Initializes the timer function.
        /// </summary>
        private DispatcherTimer timer;
        /// <summary>
        /// Initializes the integer value to indicate update rate.
        /// </summary>
        int updateRate = 200; //m secs
        /// <summary>
        /// Initializes the integer value to indicate update count.
        /// </summary>
        int updateCount = 20; //updates per tick event
        /// <summary>
        /// Initializes the random value.
        /// </summary>
        private Random rand = new Random(123123);
        /// <summary>
        /// Gets or sets the collection of objects.
        /// </summary>
        private ChangingProductSales.ProductSalesCollection productSalesData;
        /// <summary>
        /// Gets the delegate to update the source.
        /// </summary>
        private DelegateCommand<object> updateSourceCommand;
        /// <summary>
        /// Gets the delegate to activate the timer.
        /// </summary>
        private DelegateCommand<object> timerActivationCommand;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the product sales data.
        /// </summary>
        /// <value>The product sales data.</value>
        public ChangingProductSales.ProductSalesCollection ProductSalesData
        {
            get
            {
                this.productSalesData = this.productSalesData ?? ChangingProductSales.GetSalesData();
                return this.productSalesData;
            }
            set
            {
                this.productSalesData = value;
                //RaisePropertyChanged("ProductSalesData");
            }
        }

        /// <summary>
        /// Gets or sets the delegate to update the source.
        /// </summary>
        public DelegateCommand<object> UpdateSourceCommand
        {
            get
            {
                this.updateSourceCommand = this.updateSourceCommand ?? new DelegateCommand<object>(this.UpdateItemsSource);
                return this.updateSourceCommand;
            }
            set { this.updateSourceCommand = value; }
        }
        /// <summary>
        /// Gets or sets the delegate to activate the timer function.
        /// </summary>
        public DelegateCommand<object> TimerActivationCommand
        {
            get
            {
                this.timerActivationCommand = this.timerActivationCommand ?? new DelegateCommand<object>(this.DoTimerActivate);
                return this.timerActivationCommand;
            }
            set { this.timerActivationCommand = value; }
        }
        /// <summary>
        /// Method is used to update the item source.
        /// </summary>
        /// <param name="parm"></param>
        private void UpdateItemsSource(object parm)
        {
            ChangingProductSales dr = null;
            switch (parm.ToString())
            {
                case "Add at Top":
                    dr = new ChangingProductSales()
                    {
                        Country = "Canada",
                        State = "Alberta",
                        Product = "Bike",
                        Date = "FY 2003",
                        Quantity = 1,
                        Amount = 100d
                    };
                    break;
                case "Add at Middle":
                    dr = new ChangingProductSales()
                    {
                        Country = "Canada",
                        State = "Alberta",
                        Product = "Bike",
                        Date = "FY 2007",
                        Quantity = 2,
                        Amount = 200d
                    };
                    break;
                case "Add at Bottom":
                    dr = new ChangingProductSales()
                    {
                        Country = "Canada",
                        State = "Alberta",
                        Product = "Bike",
                        Date = "FY 2009",
                        Quantity = 3,
                        Amount = 300d
                    };
                    break;
            }
            this.productSalesData.Add(dr);
        }
        /// <summary>
        /// This method is used to activate the timer action with respect to the updating values.
        /// </summary>
        /// <param name="parm">The object.</param>
        private void DoTimerActivate(object parm)
        {
            if (parm is bool)
            {
                if (this.timer == null)
                {
                    this.timer = new DispatcherTimer();
                    this.timer.Tick += new EventHandler<object>(this.timer_Tick);
                    this.timer.Interval = TimeSpan.FromMilliseconds(this.updateRate);
                }

                if ((bool)parm)
                {
                    this.timer.Start();
                }
                else
                {
                    this.timer.Stop();
                }
            }
        }
        /// <summary>
        /// The event handler method is invoked when updating the values.
        /// </summary>
        /// <param name="sender">The timer.</param>
        /// <param name="e">The event argument</param>
        void timer_Tick(object sender, object e)
        {
            for (int i = 0; i < this.updateCount; ++i)
            {
                this.ChangeOneValue(i);
            }
        }
        /// <summary>
        /// This method is used to update the value in particular index.
        /// </summary>
        /// <param name="loc">The index.</param>
        private void ChangeOneValue(int loc)
        {
            double old = (double)this.productSalesData[loc].Amount;
            this.productSalesData[loc].Amount = this.rand.Next(1000);
        }
        #endregion

        #region IDisposable Method

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Method is used to freeing the memory with clearing instances, events and objects.
        /// </summary>
        /// <param name="isDisposing">The boolean value</param>
        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                ProductSalesData = null;
                UpdateSourceCommand = null;
                TimerActivationCommand = null;
                if (timer != null)
                    timer.Tick -= timer_Tick;
                timer = null;
            }
        }

    }
    /// <summary>
    /// This class that defines all the necessary actions to be performed with respect to the delegate.
    /// </summary>
    /// <typeparam name="T">The target type.</typeparam>
    public class DelegateCommand<T> : ICommand
    {
        /// <summary>
        /// Initialize the new execute action.
        /// </summary>
        private readonly Action<object> isExecute = null;
        /// <summary>
        /// Initializes the new predicate to check the updating can execute.
        /// </summary>
        private readonly Predicate<object> isCanExecute = null;

        #region Constructors       
        /// <summary>
        /// Initializes the new instance of <see cref="DelegateCommand{T}">DelegaetCommand.</see>/>
        /// </summary>
        /// <param name="execute">The action event.</param>
        public DelegateCommand(Action<object> execute)
        : this(execute, null) { }
        /// <summary>
        /// Initializes the new instance of <see cref="DelegateCommand{T}">DelegateCommand.</see>/>
        /// </summary>
        /// <param name="execute">The action event.</param>
        /// <param name="canExecute">The boolean value.</param>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.isExecute = execute;
            this.isCanExecute = canExecute;
        }

        #endregion
        /// <summary>
        /// Gets or sets the event handler for execute changed event.
        /// </summary>
        public event EventHandler CanExecuteChanged;
        #region ICommand Members
        /// <summary>
        /// This method is used to check whether the updating can execute.
        /// </summary>
        /// <param name="parameter">The object.</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return this.isCanExecute != null ? this.isCanExecute(parameter) : true;
        }
        /// <summary>
        /// This method is used to execute the updating.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (this.isExecute != null)
                this.isExecute(parameter);
        }
        /// <summary>
        /// This property changed call back method is called when the property was changed.
        /// </summary>
        public void OnCanExecuteChanged()
        {
            this.CanExecuteChanged(this, EventArgs.Empty);
        }

        #endregion


    }
}
