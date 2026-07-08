using Syncfusion.UI.Xaml.Gantt;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfGantt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Timescale : SampleLayout
    {
        public Timescale()
        {
            this.InitializeComponent();
            Combo_TopIntervalType.ItemsSource = Enum.GetValues(typeof(IntervalType));
            Combo_BottomIntervalType.ItemsSource = Enum.GetValues(typeof(IntervalType));
            Combo_WeekBeginsOn.ItemsSource = Enum.GetValues(typeof(DayOfWeek));
            GanttControl.VisibleGridColumns = TaskAttributes.Name | TaskAttributes.Duration | TaskAttributes.StartDate |
                                              TaskAttributes.FinishDate | TaskAttributes.Progress;
            Binding binding = new Binding();
            binding.Path = new PropertyPath("WeekBeginsOn");
            binding.Source = this.GanttControl;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(Combo_WeekBeginsOn, ComboBox.SelectedItemProperty, binding);
            binding = new Binding();
            binding.Path = new PropertyPath("IntervalType");
            binding.Source = this.GanttControl.TimescaleSettings.TopTier;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(Combo_TopIntervalType, ComboBox.SelectedItemProperty, binding);
            binding = new Binding();
            binding.Path = new PropertyPath("Interval");
            binding.Source = this.GanttControl.TimescaleSettings.TopTier;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(TextBox_TopInterval, TextBox.TextProperty, binding);
            binding = new Binding();
            binding.Path = new PropertyPath("IntervalType");
            binding.Source = this.GanttControl.TimescaleSettings.BottomTier;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(Combo_BottomIntervalType, ComboBox.SelectedItemProperty, binding);
            binding = new Binding();
            binding.Path = new PropertyPath("Interval");
            binding.Source = this.GanttControl.TimescaleSettings.BottomTier;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(TextBox_BottomInterval, TextBox.TextProperty, binding);
            binding = new Binding();
            binding.Path = new PropertyPath("CellSize");
            binding.Source = this.GanttControl.TimescaleSettings;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(Slider_CellSize, Slider.ValueProperty, binding);
        }

        public override void Dispose()
        {
            if (this.GridPanel != null)
            {
                this.GanttControl.ItemsSource = null;
                if (this.GanttControl.DataContext is TaskDetails)
                {
                    (this.GanttControl.DataContext as TaskDetails).Dispose();
                }

                if (this.GridPanel.DataContext != null)
                {
                    this.GridPanel.DataContext = null;
                }

                this.GanttControl.Dispose();
                this.GanttControl = null;
            }

            base.Dispose();
        }
    }
}
