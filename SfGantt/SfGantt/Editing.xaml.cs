using Common;
using Syncfusion.UI.Xaml.Gantt;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfGantt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Editing : SampleLayout
    {
        public Editing()
        {
            this.InitializeComponent();
            GanttControl.VisibleGridColumns = TaskAttributes.Name | TaskAttributes.Duration | TaskAttributes.StartDate |
                                              TaskAttributes.FinishDate | TaskAttributes.Progress |
                                              TaskAttributes.Predecessors;
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
