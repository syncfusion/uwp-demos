#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using Common;
using Syncfusion.UI.Xaml.Charts;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MultiLevelLabels : SampleLayout
    {
        public MultiLevelLabels()
        {
            this.InitializeComponent();

           

        }

        private void borderColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            switch (combo.SelectedIndex)
            {
                case 0:
                    {
                        xAxis.LabelBorderBrush = new SolidColorBrush(Colors.Gray);
                        yAxis.LabelBorderBrush = new SolidColorBrush(Colors.Gray);
                    }
                    break;
                case 1:
                    {
                        xAxis.LabelBorderBrush = new SolidColorBrush(Colors.Black);
                        yAxis.LabelBorderBrush = new SolidColorBrush(Colors.Black);
                    }
                    break;
                case 2:
                    {
                        xAxis.LabelBorderBrush = new SolidColorBrush(Colors.Red);
                        yAxis.LabelBorderBrush = new SolidColorBrush(Colors.Red);
                    }
                    break;
                case 3:
                    {
                        xAxis.LabelBorderBrush = new SolidColorBrush(Colors.Brown);
                        yAxis.LabelBorderBrush = new SolidColorBrush(Colors.Brown);
                    }
                    break;
                default:
                    break;

            }
        }

        private void borderStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            switch (combo.SelectedIndex)
            {
                case 0:
                    {
                        xAxis.MultiLevelLabelsBorderType = BorderType.Brace;
                        yAxis.MultiLevelLabelsBorderType = BorderType.Brace;
                    }
                    break;
                case 1:
                    {
                        xAxis.MultiLevelLabelsBorderType = BorderType.None;
                        yAxis.MultiLevelLabelsBorderType = BorderType.None;
                    }
                    break;
                case 2:
                    {
                        xAxis.MultiLevelLabelsBorderType = BorderType.Rectangle;
                        yAxis.MultiLevelLabelsBorderType = BorderType.Rectangle;
                    }
                    break;
                case 3:
                    {
                        xAxis.MultiLevelLabelsBorderType = BorderType.WithoutTopAndBottomBorder;
                        yAxis.MultiLevelLabelsBorderType = BorderType.WithoutTopAndBottomBorder;
                    }
                    break;
                default:
                    break;
            }
        }
        private void labelAlignment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            switch (combo.SelectedIndex)
            {
                case 0:
                    {
                        foreach (var label in xAxis.MultiLevelLabels)
                            label.LabelAlignment = LabelAlignment.Center;
                        foreach (var label in yAxis.MultiLevelLabels)
                            label.LabelAlignment = LabelAlignment.Center;
                    }
                    break;
                case 1:
                    {
                        foreach (var label in xAxis.MultiLevelLabels)
                            label.LabelAlignment = LabelAlignment.Far;
                        foreach (var label in yAxis.MultiLevelLabels)
                            label.LabelAlignment = LabelAlignment.Far;
                    }
                    break;
                case 2:
                    {
                        foreach (var label in xAxis.MultiLevelLabels)
                            label.LabelAlignment = LabelAlignment.Near;
                        foreach (var label in yAxis.MultiLevelLabels)
                            label.LabelAlignment = LabelAlignment.Near;
                    }
                    break;
                default:
                    break;
            }
        }

        private void chart_Loaded(object sender, global::Windows.UI.Xaml.RoutedEventArgs e)
        {
            ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo();
            adornmentInfo.SymbolTemplate = grid.Resources["adornment"] as DataTemplate;
            series.AdornmentsInfo = adornmentInfo;
        }
    }
    public class MultiLevelLabelModel
    {
        public string Month { get; set; }
        public double Temperature { get; set; }
    }

    public class MultiLevelLabelViewModel : IDisposable
    {
        public ObservableCollection<MultiLevelLabelModel> Collection { get; set; }

        public ChartAdornmentInfo AdornmentInfo { get; set; }

        public ResourceFactory ResourceFac { get; set; }

        public MultiLevelLabelViewModel()
        {
            Collection = new ObservableCollection<MultiLevelLabelModel>();

            Collection.Add(new MultiLevelLabelModel() { Month = "Jan", Temperature = 33 });
            Collection.Add(new MultiLevelLabelModel() { Month = "Feb", Temperature = 37 });
            Collection.Add(new MultiLevelLabelModel() { Month = "Mar", Temperature = 39 });
            Collection.Add(new MultiLevelLabelModel() { Month = "Apr", Temperature = 43 });
            Collection.Add(new MultiLevelLabelModel() { Month = "May", Temperature = 45 });
            Collection.Add(new MultiLevelLabelModel() { Month = "Jun", Temperature = 43 });
            Collection.Add(new MultiLevelLabelModel() { Month = "Jul", Temperature = 41 });
            Collection.Add(new MultiLevelLabelModel() { Month = "Aug", Temperature = 40 });
            Collection.Add(new MultiLevelLabelModel() { Month = "Sep", Temperature = 39 });
            Collection.Add(new MultiLevelLabelModel() { Month = "Oct", Temperature = 39 });
            Collection.Add(new MultiLevelLabelModel() { Month = "Nov", Temperature = 34 });
            Collection.Add(new MultiLevelLabelModel() { Month = "Dec", Temperature = 33 });

            ResourceFac = new ResourceFactory();
            AdornmentInfo = new ChartAdornmentInfo();
            AdornmentInfo.SymbolTemplate = ResourceFac.labelTemplate25;
        }

        public void Dispose()
        {
            if (Collection != null)
                Collection.Clear();
        }
    }
}