#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using Syncfusion.UI.Xaml.Gantt;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfGantt
{
    using System.Collections.ObjectModel;

    using Common;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CriticalPath : SampleLayout
    {
        public CriticalPath()
        {
            this.InitializeComponent();
        }
    }

    public class CriticalPathModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CriticalPathModel"/> class.
        /// </summary>
        public CriticalPathModel()
        {
            this.TaskDetail = this.GetTaskDetail();
        }

        /// <summary>
        /// Gets or sets the task collection.
        /// </summary>
        /// <value>The task collection.</value>
        public ObservableCollection<TaskDetail> TaskDetail { get; set; }

        /// <summary>
        /// Gets the task details.
        /// </summary>
        /// <returns></returns>
        ObservableCollection<TaskDetail> GetTaskDetail()
        {
            // Adding Tasks
            ObservableCollection<TaskDetail> task = new ObservableCollection<TaskDetail>();
            task.Add(
                new TaskDetail
                    {
                        ID = "1",
                        Name = "Project Schedule",
                        StartDate = new DateTime(2014, 2, 3),
                        FinishDate = new DateTime(2014, 2, 23),
                        Progress = 57
                    });
            task[0].Children.Add(
                new TaskDetail
                    {
                        ID = "2",
                        Name = "Planning",
                        StartDate = new DateTime(2014, 2, 3),
                        FinishDate = new DateTime(2014, 2, 12),
                        Progress = 77d
                    });
            task[0].Children[0].Children.Add(
                new TaskDetail
                    {
                        ID = "3",
                        Name = "Planning timeline",
                        StartDate = new DateTime(2014, 2, 3),
                        FinishDate = new DateTime(2014, 2, 7),
                        Progress = 80d
                    });
            task[0].Children[0].Children.Add(
                new TaskDetail
                    {
                        ID = "4",
                        Name = "Plan budget",
                        StartDate = new DateTime(2014, 2, 8),
                        FinishDate = new DateTime(2014, 2, 12),
                        Progress = 70d
                    });
            task[0].Children[0].Children.Add(
                new TaskDetail
                    {
                        ID = "5",
                        Name = "Allocate resources",
                        StartDate = new DateTime(2014, 2, 8),
                        FinishDate = new DateTime(2014, 2, 10),
                        Progress = 80d
                    });
            task[0].Children[0].Children.Add(
                new TaskDetail
                    {
                        ID = "6",
                        Name = "Planning complete",
                        StartDate = new DateTime(2014, 2, 13),
                        FinishDate = new DateTime(2014, 2, 13),
                        Progress = 0d
                    });

            task[0].Children.Add(
                new TaskDetail
                    {
                        ID = "7",
                        Name = "Design",
                        StartDate = new DateTime(2014, 2, 13),
                        FinishDate = new DateTime(2014, 2, 23),
                        Progress = 39d
                    });
            task[0].Children[1].Children.Add(
                new TaskDetail
                    {
                        ID = "8",
                        Name = "Software Specification",
                        StartDate = new DateTime(2014, 2, 14),
                        FinishDate = new DateTime(2014, 2, 20),
                        Progress = 60d
                    });
            task[0].Children[1].Children.Add(
                new TaskDetail
                    {
                        ID = "9",
                        Name = "Develop prototype",
                        StartDate = new DateTime(2014, 2, 14),
                        FinishDate = new DateTime(2014, 2, 16),
                        Progress = 40d
                    });
            task[0].Children[1].Children.Add(
                new TaskDetail
                    {
                        ID = "10",
                        Name = "Get approval from customer",
                        StartDate = new DateTime(2014, 2, 17),
                        FinishDate = new DateTime(2014, 2, 21),
                        Progress = 50d
                    });
            task[0].Children[1].Children.Add(
                new TaskDetail
                    {
                        ID = "11",
                        Name = "Design complete",
                        StartDate = new DateTime(2014, 2, 22),
                        FinishDate = new DateTime(2014, 2, 24),
                        Progress = 0d
                    });

            task[0].Children.Add(
                new TaskDetail
                    {
                        ID = "12",
                        Name = "Implementation",
                        StartDate = new DateTime(2014, 2, 13),
                        FinishDate = new DateTime(2014, 2, 23),
                        Progress = 39d
                    });
            task[0].Children[2].Children.Add(
                new TaskDetail
                    {
                        ID = "13",
                        Name = "Develop prototype",
                        StartDate = new DateTime(2014, 2, 25),
                        FinishDate = new DateTime(2014, 2, 27),
                        Progress = 60d
                    });
            task[0].Children[2].Children.Add(
                new TaskDetail
                    {
                        ID = "14",
                        Name = "Divide modules",
                        StartDate = new DateTime(2014, 2, 28),
                        FinishDate = new DateTime(2014, 3, 2),
                        Progress = 40d
                    });
            task[0].Children[2].Children.Add(
                new TaskDetail
                    {
                        ID = "15",
                        Name = "Allocate resources",
                        StartDate = new DateTime(2014, 3, 3),
                        FinishDate = new DateTime(2014, 3, 7),
                        Progress = 50d
                    });
            task[0].Children[2].Children.Add(
                new TaskDetail
                    {
                        ID = "16",
                        Name = "Optimization",
                        StartDate = new DateTime(2014, 3, 8),
                        FinishDate = new DateTime(2014, 3, 10),
                        Progress = 0d
                    });

            task[0].Children.Add(
                new TaskDetail
                    {
                        ID = "17",
                        Name = "Testing",
                        StartDate = new DateTime(2014, 2, 13),
                        FinishDate = new DateTime(2014, 2, 23),
                        Progress = 39d
                    });
            task[0].Children[3].Children.Add(
                new TaskDetail
                    {
                        ID = "18",
                        Name = "Manual testing",
                        StartDate = new DateTime(2014, 3, 11),
                        FinishDate = new DateTime(2014, 3, 13),
                        Progress = 60d
                    });
            task[0].Children[3].Children.Add(
                new TaskDetail
                    {
                        ID = "19",
                        Name = "Develop scripts for testing",
                        StartDate = new DateTime(2014, 3, 14),
                        FinishDate = new DateTime(2014, 3, 16),
                        Progress = 40d
                    });
            task[0].Children[3].Children.Add(
                new TaskDetail
                    {
                        ID = "20",
                        Name = "Automation",
                        StartDate = new DateTime(2014, 3, 17),
                        FinishDate = new DateTime(2014, 3, 21),
                        Progress = 50d
                    });
            task[0].Children[3].Children.Add(
                new TaskDetail
                    {
                        ID = "21",
                        Name = "Release beta version",
                        StartDate = new DateTime(2014, 3, 22),
                        FinishDate = new DateTime(2014, 3, 22),
                        Progress = 0d
                    });

            //Adding Predecessors
            task[0].Children[0].Children[1].Predecessors.Add(
                new TaskRelationship() { ID = "3", Relationship = Relationship.FinishToStart });
            task[0].Children[0].Children[2].Predecessors.Add(
                new TaskRelationship() { ID = "4", Relationship = Relationship.StartToStart });
            task[0].Children[0].Children[3].Predecessors.Add(
                new TaskRelationship() { ID = "4", Relationship = Relationship.FinishToStart });

            task[0].Children[1].Children[0].Predecessors.Add(
                new TaskRelationship() { ID = "6", Relationship = Relationship.FinishToStart });
            task[0].Children[1].Children[1].Predecessors.Add(
                new TaskRelationship() { ID = "6", Relationship = Relationship.FinishToStart });
            task[0].Children[1].Children[2].Predecessors.Add(
                new TaskRelationship() { ID = "9", Relationship = Relationship.FinishToStart });
            task[0].Children[1].Children[3].Predecessors.Add(
                new TaskRelationship() { ID = "10", Relationship = Relationship.FinishToStart });

            task[0].Children[2].Children[0].Predecessors.Add(
                new TaskRelationship() { ID = "11", Relationship = Relationship.FinishToStart });
            task[0].Children[2].Children[1].Predecessors.Add(
                new TaskRelationship() { ID = "13", Relationship = Relationship.FinishToStart });
            task[0].Children[2].Children[2].Predecessors.Add(
                new TaskRelationship() { ID = "14", Relationship = Relationship.FinishToStart });
            task[0].Children[2].Children[3].Predecessors.Add(
                new TaskRelationship() { ID = "15", Relationship = Relationship.FinishToStart });

            task[0].Children[3].Children[0].Predecessors.Add(
                new TaskRelationship() { ID = "16", Relationship = Relationship.FinishToStart });
            task[0].Children[3].Children[1].Predecessors.Add(
                new TaskRelationship() { ID = "18", Relationship = Relationship.FinishToStart });
            task[0].Children[3].Children[2].Predecessors.Add(
                new TaskRelationship() { ID = "19", Relationship = Relationship.FinishToStart });
            task[0].Children[3].Children[3].Predecessors.Add(
                new TaskRelationship() { ID = "20", Relationship = Relationship.FinishToStart });

            return task;

        }
    }
}
