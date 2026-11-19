using Common;
using Syncfusion.UI.Xaml.Controls.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SampleBrowser.ProgressBar
{
    public class ProgressBarCustomProperties : NotificationObject
    {
        DispatcherTimer timer;

        ResourceDictionary dictionary;

        private ProgressBarCustomObjects customObject = ProgressBarCustomObjects.Loading;
        public ProgressBarCustomObjects CustomShape
        {
            get { return customObject; }
            set
            {
                customObject = value;
                UpdateTemplate();
                RaisePropertyChanged("CustomShape");
            }
        }

        private Directions fillDirection = Directions.Horizontal;
        public Directions FillDirection
        {
            get { return fillDirection; }
            set
            {
                fillDirection = value;
                RaisePropertyChanged("FillDirection");
            }
        }

        private ControlTemplate template;
        public ControlTemplate SelectedTemplate
        {
            get { return template; }
            set { template = value;  RaisePropertyChanged("SelectedTemplate"); }
        }

        private double _value = 0.0;
        public double Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged("Value"); }
        }

        public ProgressBarCustomProperties()
        {
            timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.1) };
            timer.Tick += Timer_Tick;
            timer.Start();
            UpdateTemplate();
        }

        private void Timer_Tick(object sender, object e)
        {            
            Value += 1.0;
            if (Value == 100.0)
                Value = 0.0;
        }

        private void UpdateTemplate()
        {
            timer.Stop();
            Value = 0.0;
            if (CustomShape == ProgressBarCustomObjects.Mobile)
                FillDirection = Directions.Vertical;
            else
                FillDirection = Directions.Horizontal;
            if(dictionary == null)
            {
                dictionary = new ResourceDictionary();
                dictionary.Source = new Uri(GetResourcePath("ProgressBar/Custom/ProgressBarCustomTemplates.xaml"), UriKind.RelativeOrAbsolute);
            }
            SelectedTemplate = dictionary[CustomShape.ToString() + "Template"] as ControlTemplate;
            timer.Start();
        }

        public void Dispose()
        {
            timer.Stop();
            timer.Tick -= Timer_Tick;
            timer = null;
        }

        public string GetResourcePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                throw new ArgumentNullException(nameof(relativePath));

            // Check if running in Sample Browser main application
            string assemblyName = (Application.Current.GetType().GetTypeInfo().Assembly as Assembly)?.GetName().Name ?? string.Empty;

            if (assemblyName == "Syncfusion.SampleBrowser.UWP")
            {
                // Running in main Sample Browser app - include assembly name in path
                return $"ms-appx:///Syncfusion.SampleBrowser.UWP.Navigation/{relativePath}";
            }
            else
            {
                // Running in separate assembly - use standard path format
                return $"ms-appx:///{relativePath}";
            }
        }

    }
}
