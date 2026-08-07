#region Copyright Syncfusion Inc. 2001 - 2016
// Copyright Syncfusion Inc. 2001 - 2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Syncfusion.SampleBrowser.UWP.SfTreeGrid
{
    class PersonInfo : NotificationObject, INotifyDataErrorInfo
    {
        Random random = new Random(123123);
        #region Private Fields

        private static int _globalId = 0;
        private int _id;
        private string _firstName;
        private string _lastName;
        private bool _available;
        private string _cake = String.Empty;
        private DateTime _dob;
        private double _salary;
        private string email_Id;
        private ObservableCollection<PersonInfo> _children;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        public ObservableCollection<PersonInfo> Children
        {
            get
            {
                return _children;
            }
            set
            {
                _children = value;
            }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [likes cake].
        /// </summary>
        /// <value><c>true</c> if [likes cake]; otherwise, <c>false</c>.</value>
        public bool Availability
        {
            get
            {
                return _available;
            }
            set
            {
                _available = value;
                RaisePropertyChanged("Availability");
            }
        }

        /// <summary>
        /// Gets or sets the cake.
        /// </summary>
        /// <value>The cake.</value>
        public string Cake
        {
            get
            {
                return _cake;
            }
            set
            {
                _cake = value;
            }
        }

        /// <summary>
        /// Gets or sets the DOB.
        /// </summary>
        /// <value>The DOB.</value>
        public DateTime DOB
        {
            get
            {
                return _dob;
            }
            set
            {
                _dob = value; 
            }
        }

        /// <summary>
        /// Gets or sets the Salary.
        /// </summary>
        /// <value>The Salary.</value>
        [Range(0, 50000, ErrorMessage = "The “Salary” field can range from 0 to 50000.")]
        public double Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                _salary = value;
            }
        }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The Email Address.</value>
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(17, ErrorMessage = "Email ID is invalid")]
        [EmailAddress]
        public string Email_Id
        {
            get { return email_Id; }
            set { email_Id = value;               
            }
        }

        public bool HasErrors
        {
            get
            {
                if (LastName.Length > 8)
                    return true;
                return false;
            }
        }

        #endregion

        #region Constructors


        /// <summary>
        /// Initializes a new instance of the <see cref="PersonInfo"/> class.
        /// </summary>
        public PersonInfo()
            : this("Enter FirstName", "Enter LastName", 20000, new DateTime(2008, 10, 26), null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonInfo"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="salary">The salary.</param>
        /// <param name="dob">The dob.</param>
        /// <param name="maxGenerations">The max generations.</param>
        public PersonInfo(string firstName, string lastName, double salary, DateTime dob, ObservableCollection<PersonInfo> child)
        {
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            Availability = true;
            Cake = "Chocolate";
            DOB = dob;
            Id = _globalId++;
            Children = child;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == "LastName" && LastName.Length > 8)
            {                
                List<string> list = new List<string>();
                list.Add("LastName is invalid");
                NotifyErrorsChanged(propertyName);
                return list;
            }
            return null;
        }

        private void NotifyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
        #endregion Constructors
    }
}
