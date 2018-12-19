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
    public sealed partial class Getting_Started : SampleLayout
    {
        public Getting_Started()
        {
            this.InitializeComponent();
            GanttControl.VisibleGridColumns = TaskAttributes.Name | TaskAttributes.Duration | TaskAttributes.StartDate |
                                              TaskAttributes.FinishDate | TaskAttributes.Progress;
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

    public class TaskDetails
    {
        public ObservableCollection<TaskDetail> TaskCollection { get; set; }

        public TaskDetails()
        {
            TaskCollection = new ObservableCollection<TaskDetail>();
            TaskCollection.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 23),
                FinishDate = new DateTime(2014, 2, 27),
                Name = "Parent Task 1",
                ID = "1",
                Progress = 40
            });

            ObservableCollection<TaskDetail> MarketAnalysis = new ObservableCollection<TaskDetail>();
            MarketAnalysis.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 23),
                FinishDate = new DateTime(2014, 2, 27),
                Name = "Child Task 1",
                ID = "2",
                Progress = 40
            });
            MarketAnalysis.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 23),
                FinishDate = new DateTime(2014, 2, 27),
                Name = "Child Task 2",
                ID = "3",
                Progress = 40
            });
            MarketAnalysis.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 23),
                FinishDate = new DateTime(2014, 2, 27),
                Name = "Child Task 3",
                ID = "4",
                Progress = 40
            });

            TaskCollection[0].Children = MarketAnalysis;

            TaskCollection.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 2),
                FinishDate = new DateTime(2014, 3, 6),
                Name = "Parent Task 2",
                ID = "5",
                Progress = 40
            });
            ObservableCollection<TaskDetail> InfrastructureReq = new ObservableCollection<TaskDetail>();
            InfrastructureReq.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 2),
                FinishDate = new DateTime(2014, 3, 6),
                Name = "Child Task 1",
                ID = "6",
                Progress = 40
            });
            InfrastructureReq.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 2),
                FinishDate = new DateTime(2014, 3, 6),
                Name = "Child Task 2",
                ID = "7",
                Progress = 40
            });
            InfrastructureReq.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 2),
                FinishDate = new DateTime(2014, 3, 6),
                Name = "Child Task 3",
                ID = "8",
                Progress = 40
            });
            InfrastructureReq.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 2),
                FinishDate = new DateTime(2014, 3, 6),
                Name = "Child Task 4",
                ID = "9",
                Progress = 40
            });

            TaskCollection[1].Children = InfrastructureReq;

            TaskCollection.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 9),
                FinishDate = new DateTime(2014, 3, 13),
                Name = "Parent Task 3",
                ID = "10",
                Progress = 40
            });
            ObservableCollection<TaskDetail> Product = new ObservableCollection<TaskDetail>();
            Product.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 9),
                FinishDate = new DateTime(2014, 3, 13),
                Name = "Child Task 1",
                ID = "11",
                Progress = 40
            });
            Product.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 9),
                FinishDate = new DateTime(2014, 3, 13),
                Name = "Child Task 2",
                ID = "12",
                Progress = 40
            });
            Product.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 9),
                FinishDate = new DateTime(2014, 3, 13),
                Name = "Child Task 3",
                ID = "13",
                Progress = 40
            });
            Product.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 9),
                FinishDate = new DateTime(2014, 3, 13),
                Name = "Child Task 4",
                ID = "14",
                Progress = 40
            });
            Product.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 9),
                FinishDate = new DateTime(2014, 3, 13),
                Name = "Child Task 5",
                ID = "15",
                Progress = 40
            });

            TaskCollection[2].Children = Product;
        }

        public void Dispose()
        {
            if (TaskCollection != null)
            {
                TaskCollection.Clear();
            }
        }
    }
}
