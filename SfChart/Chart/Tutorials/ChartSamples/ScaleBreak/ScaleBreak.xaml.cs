#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Syncfusion.UI.Xaml.Charts;
using Syncfusion.UI.Xaml.Controls.Input;
using Windows.UI;
using Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScaleBreak : SampleLayout
    {
        public ScaleBreak()
        {
            this.InitializeComponent();
        }   

        private void brk_position_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo.SelectedIndex == 2)
            {
                axis2.BreakPosition = ScaleBreakPosition.Percent;
                brk_percent.Visibility = Visibility.Visible;
                brk_percentText.Visibility = Visibility.Visible;
                brk_percent1.Visibility = Visibility.Visible;
                brk_percentText1.Visibility = Visibility.Visible;
            }
            else if (combo.SelectedIndex == 1)
            {
                axis2.BreakPosition = ScaleBreakPosition.Scale;
                if (brk_percent != null && brk_percentText != null)
                {
                    brk_percent.Visibility = Visibility.Collapsed;
                    brk_percentText.Visibility = Visibility.Collapsed;
                    brk_percent1.Visibility = Visibility.Collapsed;
                    brk_percentText1.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                axis2.BreakPosition = ScaleBreakPosition.DataCount;
                if (brk_percent != null && brk_percentText != null)
                {
                    brk_percent.Visibility = Visibility.Collapsed;
                    brk_percentText.Visibility = Visibility.Collapsed;
                    brk_percent1.Visibility = Visibility.Collapsed;
                    brk_percentText1.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void Fill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;

            if (ScaleBreakChart != null)
            {
                if (select.SelectedIndex == 0)
                {
                    axis2.AxisScaleBreaks[0].Fill = new SolidColorBrush(Colors.White);
                }
                else if (select.SelectedIndex == 1)
                {
                    axis2.AxisScaleBreaks[0].Fill = new SolidColorBrush(Colors.Orange);
                }
                else if (select.SelectedIndex == 2)
                {
                    axis2.AxisScaleBreaks[0].Fill = new SolidColorBrush(Colors.Red);
                }
                else if (select.SelectedIndex == 3)
                {
                    axis2.AxisScaleBreaks[0].Fill = new SolidColorBrush(Colors.Green);
                }
            }
        }

        private void Fill_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;
            if (ScaleBreakChart != null)
            {
                if (select.SelectedIndex == 0)
                {
                    axis2.AxisScaleBreaks[1].Fill = new SolidColorBrush(Colors.White);
                }
                else if (select.SelectedIndex == 1)
                {
                    axis2.AxisScaleBreaks[1].Fill = new SolidColorBrush(Colors.Orange);
                }
                else if (select.SelectedIndex == 2)
                {
                    axis2.AxisScaleBreaks[1].Fill = new SolidColorBrush(Colors.Red);
                }
                else if (select.SelectedIndex == 3)
                {
                    axis2.AxisScaleBreaks[1].Fill = new SolidColorBrush(Colors.Green);
                }
            }
        }

        private void linetype2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo.SelectedIndex == 0)
                axis2.AxisScaleBreaks[1].LineType = BreakLineType.StraightLine;
            else
                axis2.AxisScaleBreaks[1].LineType = BreakLineType.Wave;
        }

        private void linetype1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo.SelectedIndex == 0)
                axis2.AxisScaleBreaks[0].LineType = BreakLineType.StraightLine;
            else
                axis2.AxisScaleBreaks[0].LineType = BreakLineType.Wave;
        }
      
    }
    public class ScalebreakViewModel
    {
        public ObservableCollection<ScalebreakModel> ScalebreakDatas { get; set; }
        public ScalebreakViewModel()
        {
            ScalebreakDatas = new ObservableCollection<ScalebreakModel>();
            ScalebreakDatas.Add(new ScalebreakModel { XData = "Britton Hill", YData = 105 });
            ScalebreakDatas.Add(new ScalebreakModel { XData = "Yeomposan", YData = 203 });
            ScalebreakDatas.Add(new ScalebreakModel { XData = "Mount Everest", YData = 8848 });
            ScalebreakDatas.Add(new ScalebreakModel { XData = "Diamond Head", YData = 232 });
            ScalebreakDatas.Add(new ScalebreakModel { XData = "Nanda Devi", YData = 7816 });
            ScalebreakDatas.Add(new ScalebreakModel { XData = "Hwajangsan", YData = 285 });
            ScalebreakDatas.Add(new ScalebreakModel { XData = "Arma Konda", YData = 1680 });
        }
    }
    public class ScalebreakModel
    {
        private string xData;
        public string XData
        {
            get { return xData; }
            set { xData = value; }
        }

        private double yData;
        public double YData
        {
            get { return yData; }
            set { yData = value; }
        }
    }
}
