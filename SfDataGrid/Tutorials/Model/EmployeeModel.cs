using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid
{
    /// <summary>
    /// This class represents the Employees
    /// </summary>
    public class Employees : NotificationObject
    {
        private int _EmployeeID;
        private string _Name;
        private long _NationalIDNumber;
        private int _ContactID;
        private string _LoginID;
        private int _ManagerID;
        private string _Title;
        private DateTime _BirthDate;
        private string _MaritalStatus;
        private string _Gender;
        private DateTime _HireDate;
        private int _SickLeaveHours;
        private double _Salary;
        private bool employeeStatus;
        private int _rating;
        
        
        /// <summary>
        /// Gets or sets the employee ID.
        /// </summary>
        /// <value>The employee ID.</value>
        public int EmployeeID
        {
            get
            {
                return this._EmployeeID;
            }
            set
            {
                this._EmployeeID = value;
                this.RaisePropertyChanged("EmployeeID");
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
                this.RaisePropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public long NationalIDNumber
        {
            get
            {
                return this._NationalIDNumber;
            }
            set
            {
                this._NationalIDNumber = value;
                this.RaisePropertyChanged("NationalIDNumber");
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
                this.RaisePropertyChanged("Title");
            }
        }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public int ContactID
        {
            get
            {
                return this._ContactID;
            }
            set
            {
                this._ContactID = value;
                this.RaisePropertyChanged("ContactID");
            }
        }

        /// <summary>
        /// Gets or sets the BirthDate.
        /// </summary>
        /// <value>The BirthDate.</value>
        public DateTime BirthDate
        {
            get
            {
                return this._BirthDate;
            }
            set
            {
                this._BirthDate = value;
                this.RaisePropertyChanged("BirthDate");
            }
        }

        /// <summary>
        /// Gets or sets the MaritalStatus.
        /// </summary>
        /// <value>The MaritalStatus.</value>
        public string MaritalStatus
        {
            get
            {
                return this._MaritalStatus;
            }
            set
            {
                this._MaritalStatus = value;
                this.RaisePropertyChanged("MaritalStatus");
            }
        }

        /// <summary>
        /// Gets or sets the Gender.
        /// </summary>
        /// <value>The Gender.</value>
        public string Gender
        {
            get
            {
                return this._Gender;
            }
            set
            {
                this._Gender = value;
                this.RaisePropertyChanged("Gender");
            }
        }

        /// <summary>
        /// Gets or sets the HireDate.
        /// </summary>
        /// <value>The HireDate.</value>
        public DateTime HireDate
        {
            get
            {
                return this._HireDate;
            }
            set
            {
                this._HireDate = value;
                this.RaisePropertyChanged("HireDate");
            }
        }

        /// <summary>
        /// Gets or sets the SickLeaveHours.
        /// </summary>
        /// <value>The SickLeaveHours.</value>
        public int SickLeaveHours
        {
            get
            {
                return this._SickLeaveHours;
            }
            set
            {
                this._SickLeaveHours = value;
                this.RaisePropertyChanged("SickLeaveHours");
            }
        }

        /// <summary>
        /// Gets or sets the Salary.
        /// </summary>
        /// <value>The Salary.</value>
        public double Salary
        {
            get
            {
                return this._Salary;
            }
            set
            {
                this._Salary = value;
                this.RaisePropertyChanged("Salary");
            }
        }

        /// <summary>
        /// Gets or sets the LoginID.
        /// </summary>
        /// <value>The LogID.</value>
        public string LoginID
        {
            get
            {
                return _LoginID;
            }
            set
            {
                _LoginID = value;
                this.RaisePropertyChanged("LoginID");
            }
        }

        /// <summary>
        /// Gets or sets the ManagerID.
        /// </summary>
        /// <value>The ManagerID.</value>
        public int ManagerID
        {
            get
            {
                return _ManagerID;
            }
            set
            {
                _ManagerID = value;
                this.RaisePropertyChanged("ManagerID");
            }
        }

        /// <summary>
        /// Gets or sets the EmployeeStatus.
        /// </summary>
        /// <value>The EmployeeStatus.</value>
        public bool EmployeeStatus
        {
            get
            {
                return employeeStatus;
            }
            set
            {
                employeeStatus = value;
                this.RaisePropertyChanged("EmployeeStatus");
            }
        }

        /// <summary>
        /// Gets or sets the Rating.
        /// </summary>
        /// <value>The Rating.</value>
        public int Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                this.RaisePropertyChanged("Rating");
            }
        }
    }
}
