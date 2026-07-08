using DiagramBuilder.Utility;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace DiagramBuilder.ViewModel
{
    public interface IPropertyEditor : INotifyPropertyChanged
    {
        string Title { get; set; }
        string Icon { get; set; }
        UIElement Content { get; set; }
        ICommand Select { get; set; }
        bool IsSelected { get; set; }
        //string Name { get; set; }
    }

    public class PropertyEditorVM : DiagramElementViewModel, IPropertyEditor
    {
        private string _mTitle;
        private string _mIcon;
        private UIElement _mContent;
        private ICommand _mSelect;
        private bool _mIsSelected;
        //private string _mname;
        public PropertyEditorVM()
        {
            Select = new Command(param => IsSelected = true);
        }

        public string Title
        {
            get { return _mTitle; }
            set
            {
                if (_mTitle != value)
                {
                    _mTitle = value; OnPropertyChanged("Title");
                }
            }
        }

        public string Icon
        {
            get
            {
                return _mIcon;
            }
            set
            {
                if (_mIcon != value)
                {
                    _mIcon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }

        public UIElement Content
        {
            get
            {
                return _mContent;
            }
            set
            {
                if (_mContent != value)
                {
                    _mContent = value;
                    OnPropertyChanged("Content");
                }
            }
        }

        public ICommand Select
        {
            get
            {
                return _mSelect;
            }
            set
            {
                if (_mSelect != value)
                {
                    _mSelect = value;
                    OnPropertyChanged("Select");
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return _mIsSelected;
            }
            set
            {
                if (_mIsSelected != value)
                {
                    _mIsSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        //public string Name
        //{
        //    get { return _mname; }
        //    set
        //    {
        //        if (_mname != value)
        //        {
        //            _mname = value; OnPropertyChanged("Name");
        //        }
        //    }
        //}
    }
}
