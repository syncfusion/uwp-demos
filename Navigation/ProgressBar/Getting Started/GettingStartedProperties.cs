using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace SampleBrowser.ProgressBar
{
    public class GettingStartedProperties : NotificationObject
    {
        DispatcherTimer timer;

        private double value1 = 0.0;
        public double Value1
        {
            get { return value1; }
            set
            {
                value1 = value;
                RaisePropertyChanged("Value1");
            }
        }

        private double value2 = 25.0;
        public double Value2
        {
            get { return value2; }
            set
            {
                value2 = value;
                RaisePropertyChanged("Value2");
            }
        }

        private TimeSpan interval = TimeSpan.FromSeconds(0.01);
        public TimeSpan Interval
        {
            get { return interval; }
            set
            {
                interval = value;
                timer.Interval = interval;
                RaisePropertyChanged("Interval");
            }
        }

        private ICommand start;
        public ICommand Start
        {
            get { return new DelegateCommand<object>(StartProgress, args => true); }
            set { start = value; RaisePropertyChanged("Start"); }
        }

        private ICommand pause;
        public ICommand Pause
        {
            get { return new DelegateCommand<object>(PauseProgress, args => true); }
            set { pause = value; RaisePropertyChanged("Pause"); }
        }

        private ICommand resume;

        public ICommand Resume
        {
            get { return new DelegateCommand<object>(ResumeProgress, args => true); }
            set { resume = value; RaisePropertyChanged("Resume"); }
        }

        public GettingStartedProperties()
        {
            timer = new DispatcherTimer() { Interval = Interval };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            TimerTick(timer);
        }

        private void TimerTick(object param)
        {
            if (Value2 == 125.0)
            {
                (param as DispatcherTimer).Stop();
            }
            else
            {
                if (Value1 == 100.0)
                {
                    Value2 += 25.0;
                    if (Value2 != 125.0)
                    {
                        Value1 = 0.0;
                    }
                    if (Value2 == 25)
                        timer.Interval = TimeSpan.FromSeconds(0.02);
                    if (Value2 == 50)
                        timer.Interval = TimeSpan.FromSeconds(0.01);
                    if (Value2 == 75)
                        timer.Interval = TimeSpan.FromSeconds(0.05);
                    if(Value2 == 100)
                        timer.Interval = TimeSpan.FromSeconds(0.01);
                }
                else
                {
                   
                    Value1 += 1.0;
                }
            }
        }

        private void StartProgress(object param)
        {
            timer.Stop();
            Value1 = 0.0;
            Value2 = 25.0;
            timer.Interval = TimeSpan.FromSeconds(0.02);
            timer.Start();
        }
        private void PauseProgress(object param)
        {
            timer.Stop();
        }
        private void ResumeProgress(object param)
        {
            timer.Start();
        }

        internal void Dispose()
        {
            timer.Stop();
            timer.Tick -= Timer_Tick;
            timer = null;
        }
    }
}
