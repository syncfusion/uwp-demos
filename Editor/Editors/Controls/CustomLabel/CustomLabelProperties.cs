using Common;
using Syncfusion.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;



namespace Editors
{
    public class CustomLabelProperties : NotificationObject , IDisposable
    {

        public CustomLabelProperties()
        {
            ItemsCollection.Add(new Items() { value = 10, label = "Min" });
            ItemsCollection.Add(new Items() { value = 100, label = "Max" });
        }
        private Orientation orientation = Orientation.Horizontal;
        private Orientation _labelOrientation = Orientation.Horizontal;
        private bool showCustomLabels = true;
        private bool showValueLabels = true;
        private Syncfusion.UI.Xaml.Controls.Input.TickPlacement tickplacement = Syncfusion.UI.Xaml.Controls.Input.TickPlacement.BottomRight;
        private Syncfusion.UI.Xaml.Controls.Input.ValuePlacement valueplacement = Syncfusion.UI.Xaml.Controls.Input.ValuePlacement.BottomRight;
        private Syncfusion.UI.Xaml.Controls.Input.LabelPlacement labelplacement = Syncfusion.UI.Xaml.Controls.Input.LabelPlacement.TopLeft;

        public Orientation Orientation
        {
            get { return orientation; }
            set { orientation = value; RaisePropertyChanged("Orientation"); }
        }


        public Orientation LabelOrientation
        {
            get { return _labelOrientation; }
            set { _labelOrientation = value; RaisePropertyChanged("LabelOrientation"); }
        }

        public bool ShowCustomLabels
        {
            get { return showCustomLabels; }
            set { showCustomLabels = value; RaisePropertyChanged("ShowCustomLabels"); }
        }

        public bool ShowValueLabels
        {
            get { return showValueLabels; }
            set { showValueLabels = value; RaisePropertyChanged("ShowValueLabels"); }
        }

        public TickPlacement TickPlacement  
        {
            get { return tickplacement; }
            set { tickplacement = value; RaisePropertyChanged("TickPlacement"); }
        }

        public ValuePlacement ValuePlacement
        {
            get { return valueplacement; }
            set { valueplacement = value; RaisePropertyChanged("ValuePlacement"); }
        }

        public LabelPlacement LabelPlacement
        {
            get { return labelplacement; }
            set { labelplacement = value; RaisePropertyChanged("LabelPlacement"); }
        }


        private ObservableCollection<Items> itemsCollection = new ObservableCollection<Items>();

        public ObservableCollection<Items> ItemsCollection
        {
            get { return itemsCollection; }
            set
            {
                itemsCollection = value;
                RaisePropertyChanged("ItemsCollection");
            }
        }
        public void Dispose()
        {
            if (ItemsCollection != null)
            {
                ItemsCollection.Clear();
            }

        }

    }

}
