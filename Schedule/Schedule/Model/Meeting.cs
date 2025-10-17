#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Syncfusion.SampleBrowser.UWP.Schedule
{
    public class Meeting : INotifyPropertyChanged
    {
        #region EventName

        private string eventName;
        public string EventName
        {
            get
            {
                return eventName;
            }
            set
            {
                this.eventName = value;
                OnPropertyChanged("EventName");

            }
        }
        #endregion

        #region Organizer

        private string organizer;

        public string Organizer
        {
            get
            {
                return organizer;
            }
            set
            {
                this.organizer = value;
                OnPropertyChanged("Organizer");

            }
        }

        #endregion

        #region ContactID

        private string contactID;

        public string ContactID
        {
            get
            {
                return contactID;
            }
            set
            {
                this.contactID = value;
                OnPropertyChanged("ContactID");

            }
        }
        #endregion

        #region Capacity

        private int capacity;

        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                this.capacity = value;
                OnPropertyChanged("Capacity");

            }
        }
        #endregion

        #region From

        private DateTime from;

        public DateTime From
        {
            get
            {
                return from;
            }
            set
            {
                this.from = value;
                OnPropertyChanged("From");

            }
        }
        #endregion

        #region To

        private DateTime to;

        public DateTime To
        {
            get
            {
                return to;
            }
            set
            {
                this.to = value;
                OnPropertyChanged("To");

            }
        }
        #endregion

        #region AllDay

        private bool allday;

        public bool AllDay
        {
            get
            {
                return allday;
            }
            set
            {
                this.allday = value;
                OnPropertyChanged("AllDay");
            }
        }

        #endregion

        #region Color
        public Brush color { get; set; }
   
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));

        }
    }
}
