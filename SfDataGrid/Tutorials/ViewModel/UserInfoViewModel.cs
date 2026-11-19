using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Collections.ObjectModel;
using Common;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Grid;

namespace DataGrid
{
    /// <summary>
    /// This class represents the UserInfoViewModel
    /// </summary>
    public class UserInfoViewModel :NotificationObject,IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        public UserInfoViewModel()
        {
            UserDetails = new UserInfoRepository().GetUserDetails(100);
        }

        private ObservableCollection<UserInfo> userDetails;
        /// <summary>
        /// Gets or sets the UserDetails.
        /// </summary>
        /// <value>The UserDetails.</value>
        public ObservableCollection<UserInfo> UserDetails
        {
            get { return userDetails; }
            set { userDetails = value; RaisePropertyChanged("UserDetails"); }
        }
        
        private void OnCellValidating(object pram)
        {
            var args = pram as CurrentCellValidatingEventArgs;
            if (args!= null && args.Column.MappingName.Equals("DateofBirth"))
            {
                if ((Convert.ToDateTime(args.NewValue)).Year > 1980)
                {
                    args.ErrorMessage = "Birth Year should be before 1980";
                    args.IsValid = false;
                }

            }
        }
        
        private void OnRowValidating(object pram)
        {
            var args = pram as RowValidatingEventArgs;
            if (args != null)
            {
                var data = args.RowData as UserInfo;
                if (!data.Name.Equals(data.EMail.Split('@').FirstOrDefault()))
                {
                    args.ErrorMessages.Add("EMail", "Email does not match with User Name");
                    args.IsValid = false;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (UserDetails != null)
                UserDetails.Clear();
        }
    }
}
