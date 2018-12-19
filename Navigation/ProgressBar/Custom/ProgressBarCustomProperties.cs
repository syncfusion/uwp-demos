#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.UI.Xaml.Controls.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
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
                dictionary.Source = new Uri("ms-appx:///ProgressBar/Custom/ProgressBarCustomTemplates.xaml", UriKind.RelativeOrAbsolute);
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

    }
}
