using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace WorkFlowEditor
{
    public class ProcessAutomationNode : NodeViewModel
    {
        public ProcessAutomationNode()
        {
            ID = Guid.NewGuid().ToString();
            //ContentTemplate = App.Current.Resources["nodetemplate"] as DataTemplate;
        }

        private string type;

         [DataMember]
        public string NodeType
        {
            get
            {
                return type;
            }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("NodeType");
                }
            }
        }
        private NodeContent _content;

        [DataMember]
        public NodeContent NodeContent
        {
            get
            {
                return _content;
            }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged("NodeContent");
                }
            }
        }       

        public Node AnimationNode
        {
            get;
            set;
        }
    }

    [DataContract]
    public class NodeContent : INotifyPropertyChanged, IDisposable
    {
        public NodeContent()
        {
            Properties = new ObservableCollection<Property>();
            AddProperty = new DelegateCommand<object>(OnAddProperty, args => { return true; });
            DeleteProperty = new DelegateCommand<object>(OnDeleteProperty, args => { return true; });
        }

        private void OnDeleteProperty(object exsist)
        {
            int index = Properties.IndexOf(exsist as Property);
            Properties.RemoveAt(index);
        }



        private bool _isdecison=true;

        

        [DataMember]
        public bool IsNotDecision
        {
            get
            {
                return _isdecison;
            }
            set
            {
                if (_isdecison != value)
                {
                    _isdecison = value;
                    OnPropertyChanged("IsNotDecision");
                }
            }
        }
        private bool _propertyenabled=false;

        public bool IsPropertiesEnabled
        {
            get
            {
                return _propertyenabled;
            }
            set
            {
                if (_propertyenabled != value)
                {
                    _propertyenabled = value;
                    OnPropertyChanged("IsPropertiesEnabled");
                }
            }
        }


        private void OnAddProperty(object newproperty)
        {
                Properties.Add(new Property() { Name = "Property+1", Type = "String" });
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(Name));
            }
        }

        private SolidColorBrush _bbrush = new SolidColorBrush(Colors.Transparent);

        public SolidColorBrush BorderBrush
        {
            get
            {
                return _bbrush;
            }
            set
            {
                if (_bbrush != value)
                {
                    _bbrush = value;
                    OnPropertyChanged("BorderBrush");
                }
            }
        }

        private Thickness _bthich = new Thickness(0);

        public Thickness BorderThick
        {
            get
            {
                return _bthich;
            }
            set
            {
                if (_bthich != value)
                {
                    _bthich = value;
                    OnPropertyChanged("BorderThick");
                }
            }
        }



        private SolidColorBrush _fill=new SolidColorBrush(Colors.Transparent);
       
        public SolidColorBrush FillBrush
        {
            get
            {
                return _fill;
            }
            set
            {
                if (_fill != value)
                {
                    _fill = value;
                    OnPropertyChanged("FillBrush");
                }
            }
        }

        private bool istask = false;

        [DataMember]
        public bool IsTask
        {
            get
            {
                return istask;
            }
            set
            {
                if (istask != value)
                {
                    istask = value;
                    OnPropertyChanged("IsTask");
                }
            }

        }
        private string _text;

        [DataMember]
        public string DispalyText
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged("DispalyText");
                }
            }
        }
        private ObservableCollection<Property> m_Properties;

        //private ObservableCollection<Property> m_Functions;

        [DataMember]
        public ObservableCollection<Property> Properties
        {
            get { return m_Properties; }
            set
            {
                if (m_Properties != value)
                {
                    m_Properties = value;
                    OnPropertyChanged("Properties");
                }
            }
        }

        // [DataMember]
        //public ObservableCollection<Property> Type
        //{
        //    get { return m_Functions; }
        //    set
        //    {
        //        if (m_Functions != value)
        //        {
        //            m_Functions = value;
        //            OnPropertyChanged("Funtions");
        //        }
        //    }
        //}

        private ICommand m_addproperty;

        [IgnoreDataMember]
        public ICommand AddProperty
        {
            get
            {
                return m_addproperty;
            }
            set
            {
                if (m_addproperty != value)
                {
                    m_addproperty = value;
                    OnPropertyChanged("AddProperty");
                }
            }
        }

        private ICommand m_deleteproperty;

        [IgnoreDataMember]
        public ICommand DeleteProperty
        {
            get
            {
                return m_deleteproperty;
            }
            set
            {
                if (m_deleteproperty != value)
                {
                    m_deleteproperty = value;
                    OnPropertyChanged("DeleteProperty");
                }
            }
        }

        private ICommand _save;
        public ICommand Save
        {
            get
            {
                return _save;
            }
            set
            {
                if (_save != value)
                {
                    _save = value;
                    OnPropertyChanged("Save");
                }
            }
        }

        private ICommand _openeditor;
        public ICommand OpenEditor
        {
            get
            {
                return _openeditor;
            }
            set
            {
                if (_openeditor != value)
                {
                    _openeditor = value;
                    OnPropertyChanged("OpenEditor");
                }
            }
        }

        private ICommand _done;

        public ICommand Done
        {
            get
            {
                return _done;
            }
            set
            {
                if (_done != value)
                {
                    _done = value;
                    OnPropertyChanged("Done");
                }
            }
        }



        public void Dispose()
        {
            Save = null;
            Done = null;
            OpenEditor = null;
            AddProperty = null;
            Properties = null;
            PropertyChanged = null;
        }
    }

    [DataContract]
    public class Property : INotifyPropertyChanged
    {
        private string m_Name;

        [DataMember]
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string m_propertyvalue;
        [DataMember]
        public string PropertyValue
        {
            get
            {
                return m_propertyvalue;
            }
            set
            {
                if (m_propertyvalue != value)
                {
                    m_propertyvalue = value;
                    OnPropertyChanged("PropertyValue");
                }
            }
        }
        private string m_type;
        [DataMember]
        public string Type
        {
            get
            {
                return m_type;
            }
            set
            {
                if (m_type != value)
                {
                    if (value != null)
                    {
                        m_type = value;
                        OnPropertyChanged("Type");
                    }
                }
            }
        }
        [IgnoreDataMember]
        public ICommand Checked
        {
            get
            {
                return new DelegateCommand<object>(Onchecked, args => { return true; });
            }
        }

        private void Onchecked(object obj)
        {
            this.Type = obj.ToString();
        }

        [DataMember]
        private ObservableCollection<string> types = new ObservableCollection<string>()
       {
          "Double",
          "DatePicker",
          "ColorPicker",
          "Rating",
          "TimePicker"
       };

        [DataMember]
        public ObservableCollection<string> ValueType
        {
            get
            {
                return types;
            }
            set
            {
                if (types != null)
                {
                    types = value;
                    OnPropertyChanged("ValueType");
                }
            }

        }
        protected virtual void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
   
    public class ProcessAutomationConnector : ConnectorViewModel
    {
       
        public ProcessAutomationConnector()
        {
            this.SegmentDecorators = new ObservableCollection<SegmentDecorator>();
            SegmentDecorator s = new SegmentDecorator()
            {
                Length = 0.5,                               
                Shape = Shapes.Decorator.ToGeometry(),
            };
            this.SegmentDecoratorStyle = GetDecoratorStyle();
            (this.SegmentDecorators as ObservableCollection<SegmentDecorator>).Add(s);
        }

        private Style GetDecoratorStyle()
        {
            Windows.UI.Xaml.Style s = new Windows.UI.Xaml.Style(typeof(Windows.UI.Xaml.Shapes.Path));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeProperty, new SolidColorBrush(Colors.Black)));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.WidthProperty, 10));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.HeightProperty, 10));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StrokeThicknessProperty, 1d));
            s.Setters.Add(new Setter(Windows.UI.Xaml.Shapes.Path.StretchProperty, Stretch.Fill));
            return s;
        }
        private bool isanimation=false;
        [DataMember]
        public bool IsAnimationApplied
        {
            get
            {
                return isanimation;
            }
            set
            {
                if (isanimation != value)
                {
                    isanimation = value;
                    OnPropertyChanged("IsAnimationApplied");
                }
            }
        }

        private bool _mUseforDecisionAnimation = false;

        [DataMember]
        public bool UseforDecisionAnimation
        {
            get
            {
                return _mUseforDecisionAnimation;
            }
            set
            {
                if (_mUseforDecisionAnimation != value)
                {
                    _mUseforDecisionAnimation = value;
                    OnPropertyChanged("UseforDecisionAnimation");
                }
            }
        }

    }

    public class DelegateCommand<T> : ICommand
    {
        private Predicate<T> _canExecute;
        private Action<T> _method;
        bool _canExecuteCache = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        public DelegateCommand(Action<T> method)
            : this(method, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="canExecute">The can execute.</param>
        public DelegateCommand(Action<T> method, Predicate<T> canExecute)
        {
            _method = method;
            _canExecute = canExecute;
        }

        public DelegateCommand(ICommand Clicked, Predicate<object> predicate)
        {
            // TODO: Complete member initialization
            this.Clicked = Clicked;
            this.predicate = predicate;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                bool tempCanExecute = _canExecute((T)parameter);

                if (_canExecuteCache != tempCanExecute)
                {
                    _canExecuteCache = tempCanExecute;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, new EventArgs());
                    }
                }
            }

            return _canExecuteCache;

        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            if (_method != null)
                _method.Invoke((T)parameter);
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged;
        private ICommand Clicked;
        private Predicate<object> predicate;

        #endregion
    }

    public class ColortoBrushConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                SolidColorBrush s = value as SolidColorBrush;
                return s.Color;
            }
            else
                return null;
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Color c = (Color)value;
            return new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B));
        }

    }
}
