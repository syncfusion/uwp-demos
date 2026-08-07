using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DataGrid
{
    /// <summary>
    /// This class represents the UserInfo
    /// </summary>
    public class UserInfo : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        #region Fields
        private int userId;
        private string name;
        private DateTime dateofBirth;
        private string email;
        private string contactNo;
        private string city;
        private int salary;
        Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        /// <value>The UserId.</value>
        public int UserId
        {
            get { return userId; }
            set { userId = value; OnPropertyChanged("UserId"); }
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// Gets or sets the DateofBirth.
        /// </summary>
        /// <value>The DateofBirth.</value>
        public DateTime DateofBirth
        {
            get { return dateofBirth; }
            set { dateofBirth = value; OnPropertyChanged("DateofBirth"); }
        }

        /// <summary>
        /// Gets or sets the EMail.
        /// </summary>
        /// <value>The EMail.</value>
        public string EMail
        {
            get { return email; }
            set { email = value; OnPropertyChanged("EMail"); }
        }

        /// <summary>
        /// Gets or sets the ContactNo.
        /// </summary>
        /// <value>The ContactNo.</value>
        [StringLength(14, ErrorMessage = "The “ContactNo” field must be a string with a maximum length of 14.")]
        public string ContactNo
        {
            get { return contactNo; }
            set { contactNo = value; OnPropertyChanged("ContactNo"); }
        }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        /// <value>The City.</value>
        public string City
        {
            get { return city; }
            set { city = value; OnPropertyChanged("City"); }
        }


        /// <summary>
        /// Gets or sets the Salary.
        /// </summary>
        /// <value>The Salary.</value>
        [Range(10000, 30000, ErrorMessage = "The “Salary” field can range from 10000 through 30000.")]
        public int Salary
        {
            get { return salary; }
            set { salary = value; OnPropertyChanged("Salary"); }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region INotifyDataErrorInfo

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == "EMail")
            {
                if (!emailRegex.IsMatch(this.EMail))
                {
                    List<string> errorList = new List<string>();
                    errorList.Add("Email ID is invalid!");
                    NotifyErrorsChanged(propertyName);
                    return errorList;
                }
            }
            return null;
        }

        private void NotifyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public bool HasErrors
        {
            get { return !emailRegex.IsMatch(this.EMail); }
        }

        #endregion

    }
}