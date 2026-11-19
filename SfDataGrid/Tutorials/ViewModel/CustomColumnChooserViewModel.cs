using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid
{
    /// <summary>
    /// This class represents the CustomColumnChooserViewModel
    /// </summary>
    public class CustomColumnChooserViewModel : NotificationObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomColumnChooserViewModel"/> class.
        /// </summary>
        /// <param name="totalColumns">The total columns.</param>
        public CustomColumnChooserViewModel(ObservableCollection<ColumnChooserItems> totalColumns)
        {
            ColumnCollection = totalColumns;
        }

        /// <summary>
        /// Gets or sets the column collection.
        /// </summary>
        /// <value>The column collection.</value>
        public ObservableCollection<ColumnChooserItems> ColumnCollection
        {
            get;
            set;
        }

        #region Delegate Command

        public DelegateCommand<object> _ColumnChooserCommand;

        /// <summary>
        /// Gets the column chooser command.
        /// </summary>
        /// <value>The column chooser command.</value>
        public DelegateCommand<object> ColumnChooserCommand
        {
            get { return _ColumnChooserCommand; }
            set
            {
                _ColumnChooserCommand = value;
                RaisePropertyChanged("ColumnChooserCommand");
            }
        }
        #endregion
    }
}
