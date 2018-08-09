using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Virtulization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HeatMap
{
    public partial class Virtualization : SampleLayout
    {
        public Virtualization()
        {
            this.InitializeComponent();
            AddData();
            this.DataContext = dataFlat;
        }

        Random r = new Random();
        private ObservableCollection<DataModel> dataFlat = new ObservableCollection<DataModel>();

        private void AddData()
        {
            for (int i = 0; i < 1000; i++)
            {
                dataFlat.Add(new DataModel(i.ToString(), r.Next(0, 100), r.Next(0, 100), r.Next(0, 100), r.Next(0, 100), r.Next(0, 100)));
            }
        }
    }
}
