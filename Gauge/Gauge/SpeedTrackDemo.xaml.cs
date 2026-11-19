using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Common;
using Syncfusion.UI.Xaml.Gauges;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GaugeUWP_Samples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SpeedTrackDemo :Page,IDisposable
    {
      
        public Page mainPage;
        public Frame frame;
         Random r = new Random();
         public bool reverse = false;
        private DispatcherTimer timer;
        public double _Speed = 0;
        public double gear = 1;
        public double _Rpm = 0;
        public SpeedTrackDemo()
        {
            this.InitializeComponent();
            this.Unloaded += SpeedTrackDemo_Unloaded;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
               // HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
                timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0,50);
            timer.Tick += timer_Tick;
            Fuel = 100;
            Speed = 0;
            RPM = 0;
            Temperature = 0;
            _torque = 0.3;
            this.DataContext = this;
        }

        private void SpeedTrackDemo_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Dispose();
        }



        /// <summary>
        /// Handles the BackPressed event of the HardwareButtons control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BackPressedEventArgs"/> instance containing the event data.</param>
        //void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        //{
        //    e.Handled = true;
        //    SampleBrowser.NavigationService.GoBack();
        //}
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer = null;
            }
                base.OnNavigatedFrom(e);
        }

        void ReverseTimer_Tick()
        {
            _Speed -= .25 * gear*2;
            Speed = _Speed;
            if (Speed <=80)
            {
                reverse = false;
                gear = 2;
            }

        }
        static double GenRand(double one, double two)
        {
            Random rand = new Random();
            return one + rand.NextDouble() * (two - one);
        }
      
        void timer_Tick(object sender, object e)
        {
            if (Fuel > 0 && Temperature < 80)
            {
                if (Temperature > 35)
                {
                    Temperature += .05;
                }
                else
                {
                    Temperature += 1;
                }

                if (RPM >= 6.2)
                {
                    if (gear < 5)
                    {
                        gear += 1;
                        _Rpm -= 3;
                        RPM = _Rpm;
                    }
                    else if (RPM > 6.5)
                    {
                        _Rpm = 6.2;
                        RPM = _Rpm;
                    }
                    else
                    {
                        _Rpm += 0.1;
                        RPM = _Rpm;
                    }
                }
                else
                {
                    _Rpm += 0.1;
                    RPM = _Rpm;
                }
               
                    
                if (Speed >= 80)
                {

                    if (Speed >= 140 || reverse == true)
                    {
                        //Speed = 80;
                        reverse = true;
                        ReverseTimer_Tick();
                        
                    }
                    else
                    {
                        _Speed += .25 * gear;
                        Speed = _Speed;
                    }
                }
                else if(!reverse)
                {
                    _Speed += .25 * gear;
                    Speed = _Speed;
                }
               
                
            }
            else
            {
                _Speed = 0;
                Speed = 0;
                _Rpm = 0;
                RPM = 0;
                gear = 1;
                Temperature = 0;
                Torque = 0;
                reverse = false;
                if (Fuel == 0 || Temperature == 85)
                {
                    Fuel = 100;
                    Temperature = 0;
                }
            }
            Fuel -= .25;
            int TorqueMedium = r.Next(40, 50);
            _torque = Speed / (2*TorqueMedium);
            Torque = Math.Round((double)_torque, 1, MidpointRounding.AwayFromZero);
        }

        public double? _speed=0;
        public double? Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
                this.onPropertyChanged(this, "Speed");
            }
        }



        public double? _RPM=0;
        public double? RPM
        {
            get
            {
                return _RPM;
            }
            set
            {
                _RPM = value;
                this.onPropertyChanged(this, "RPM");
            }
        }

        public double? _temperature=0;
        public double? Temperature
        {
            get
            {
                return _temperature;
            }
            set
            {
                _temperature = value;
                this.onPropertyChanged(this, "Temperature");
            }
        }

        public double? _fuel=0;
        public double? Fuel
        {
            get
            {
                return _fuel;
            }
            set
            {
                _fuel = value;
                this.onPropertyChanged(this, "Fuel");
            }
        }

        public double? _torque=0;
        public double? Torque
        {
            get
            {
                return _torque;
            }
            set
            {
                _torque = value;
                this.onPropertyChanged(this, "Torque");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(object sender, string propertyName)
        {
            if (Speed != null)
            {
                SpeedGauge.Scales[0].Pointers[0].Value = (double)Speed;
                SpeedGauge.Scales[0].Pointers[1].Value = (double)Speed;
                RpmGauge.Scales[0].Pointers[0].Value = (double)RPM;
                RpmGauge.Scales[0].Pointers[1].Value = (double)RPM;
                TempGauge.Scales[0].Pointers[0].Value = (double)Temperature;
                TempTextBlock.Text = Math.Round((double)Temperature).ToString();
                FuelGauge.Scales[0].Pointers[1].Value = (double)Fuel;
                TorqueGauge.Scales[0].Pointers[0].Value = (double)Torque;
                TorqueTextBlock.Text = ((double)Torque).ToString();
                if (FuelTextBlock is SfDigitalGauge)
                {
                    (FuelTextBlock as SfDigitalGauge).Value = Math.Round((double)Fuel).ToString();
                }
                else
                {
                    (FuelTextBlock as TextBlock).Text = Math.Round((double)Fuel).ToString();
                }

                if (this.PropertyChanged != null)
                {
                    PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //await Task.Delay(100);
            //timer.Start();
            base.OnNavigatedTo(e);
        }

        private void TorqueGauge_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void GoBack_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
        public void Dispose()
        {
            timer.Stop();
            timer.Tick -= timer_Tick;

            if (SpeedGauge != null)
            {
                SpeedGauge.Dispose();
                SpeedGauge = null;
            }
            if (RpmGauge != null)
            {
                RpmGauge.Dispose();
                RpmGauge = null;
            }
            if (TempGauge != null)
            {
                TempGauge.Dispose();
                TempGauge = null;
            }
            if (FuelGauge != null)
            {
                FuelGauge.Dispose();
                FuelGauge = null;
            }
            if (TorqueGauge != null)
            {
                TorqueGauge.Dispose();
                TorqueGauge = null;
            }
            if (Speed != null)
            {
                Speed = null;
            }
            if (Temperature != null)
            {
                Temperature = null;
            }
            if (RPM != null)
            {
                RPM = null;
            }
            if (Fuel != null)
            {
                Fuel = null;
            }
            if (Torque != null)
            {
                Torque = null;
            }
        }

        private void WP_SpeedGauge_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
    }


