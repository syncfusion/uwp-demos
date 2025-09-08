#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid
{
    /// <summary>
    /// This class represents the CountryInfoViewModel
    /// </summary>
    class CountryInfoViewModel : INotifyPropertyChanged,IDisposable
    {

        public CountryInfoViewModel()
        {
            CountryDetails = new Countries();
        }


        private ObservableCollection<CountriesList> countryDetails;

        /// <summary>
        /// Gets or sets the CountryDetails.
        /// </summary>
        /// <value>The CountryDetails.</value>
        public ObservableCollection<CountriesList> CountryDetails
        {
            get
            {
                return countryDetails;
            }
            set
            {
                countryDetails = value;
                OnPropertyChanged("CountryDetails");
            }
        }


        #region INotifyEventChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            CountryDetails.Clear();
        }
    }
}
