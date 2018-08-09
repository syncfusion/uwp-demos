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
    public sealed partial class Project_Tracker : SampleLayout
    {
        public Project_Tracker()
        {
            this.InitializeComponent();
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

    public class ProjectTrackerViewModel
    {
        public ProjectTrackerViewModel()
        {
            _resourceCollection = this.GetResources();
            _taskCollection = this.GetData();
            _striplineCollection = this.GetStriplines();
        }

        private ObservableCollection<TaskDetail> _taskCollection;

        /// <summary>
        /// Gets or sets the appointment item source.
        /// </summary>
        /// <value>The appointment item source.</value>
        public ObservableCollection<TaskDetail> TaskCollection
        {
            get { return _taskCollection; }
            set { _taskCollection = value; }
        }

        private GanttResourceCollection _resourceCollection;

        /// <summary>
        /// Gets or sets the gantt resources.
        /// </summary>
        /// <value>The gantt resources.</value>
        public GanttResourceCollection ResourceCollection
        {
            get { return _resourceCollection; }
            set { _resourceCollection = value; }
        }

        private GanttStriplineCollection _striplineCollection;

        /// <summary>
        /// Gets or sets the striplines.
        /// </summary>
        public GanttStriplineCollection StriplineCollection
        {
            get { return _striplineCollection; }
            set { _striplineCollection = value; }
        }

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <returns></returns>
        private GanttResourceCollection GetResources()
        {
            GanttResourceCollection Resources = new GanttResourceCollection();

            Resources.Add(new GanttResource { ID = "1", Name = "Planning" });
            Resources.Add(new GanttResource { ID = "2", Name = "Design" });
            Resources.Add(new GanttResource { ID = "3", Name = "Implementation Phase" });
            Resources.Add(new GanttResource { ID = "4", Name = "Integration" });
            Resources.Add(new GanttResource { ID = "5", Name = "Final Testing" });
            Resources.Add(new GanttResource { ID = "6", Name = "Final Delivery" });
            Resources.Add(new GanttResource { ID = "7", Name = "Project Manager" });
            Resources.Add(new GanttResource { ID = "8", Name = "Software Analyst" });
            Resources.Add(new GanttResource { ID = "9", Name = "Developer" });
            Resources.Add(new GanttResource { ID = "10", Name = "Testing Engineer" });

            return Resources;
        }

        /// <summary>
        /// Gets the striplines.
        /// </summary>
        /// <returns>The striplines</returns>
        private GanttStriplineCollection GetStriplines()
        {
            GanttStriplineCollection striplines = new GanttStriplineCollection();

            striplines.Add(new GanttStripline { Label = "Project  Start", Position = new DateTime(2014, 2, 3) });
            striplines.Add(new GanttStripline { Label = "Planning Complete", Position = new DateTime(2014, 2, 7) });
            striplines.Add(new GanttStripline { Label = "Design Complete", Position = new DateTime(2014, 2, 14) });
            striplines.Add(new GanttStripline { Label = "Implementation Complete", Position = new DateTime(2014, 2, 28) });
            striplines.Add(new GanttStripline { Label = "Final Delivery", Position = new DateTime(2014, 3, 6) });
            return striplines;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<TaskDetail> GetData()
        {
            ObservableCollection<TaskDetail> Schedule = new ObservableCollection<TaskDetail>();

            Schedule.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 3),
                FinishDate = new DateTime(2014, 3, 6),
                Name = "Project Schedule",
                ID = "1"
            });

            ObservableCollection<TaskDetail> ScheduleProcess = new ObservableCollection<TaskDetail>();
            ScheduleProcess.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 3),
                FinishDate = new DateTime(2014, 2, 7),
                Name = "Planning",
                ID = "2"
            });
            ScheduleProcess.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 10),
                FinishDate = new DateTime(2014, 2, 14),
                Name = "Design",
                ID = "7"
            });
            ScheduleProcess.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                FinishDate = new DateTime(2014, 2, 28),
                Name = "Implementation Phase",
                ID = "12"
            });
            ScheduleProcess.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 3),
                FinishDate = new DateTime(2014, 3, 4),
                Name = "Integration",
                ID = "37"
            });
            ScheduleProcess.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 5),
                FinishDate = new DateTime(2014, 3, 6),
                Name = "Final Testing",
                ID = "38"
            });
            ScheduleProcess.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 3, 6),
                FinishDate = new DateTime(2014, 3, 6),
                Name = "Final Delivery",
                ID = "39"
            });

            ScheduleProcess[3].Predecessors.Add(new TaskRelationship
            {
                ID = "20",
                Relationship = Relationship.FinishToStart
            });
            ScheduleProcess[3].Predecessors.Add(new TaskRelationship
            {
                ID = "28",
                Relationship = Relationship.FinishToStart
            });
            ScheduleProcess[3].Predecessors.Add(new TaskRelationship
            {
                ID = "36",
                Relationship = Relationship.FinishToStart
            });
            ScheduleProcess[4].Predecessors.Add(new TaskRelationship
            {
                ID = "37",
                Relationship = Relationship.FinishToStart
            });
            ScheduleProcess[5].Predecessors.Add(new TaskRelationship
            {
                ID = "38",
                Relationship = Relationship.FinishToStart
            });
            
            ScheduleProcess[3].Resources.Add("3");
            ScheduleProcess[4].Resources.Add("4");
            ScheduleProcess[5].Resources.Add("5");
            Schedule[0].Children = ScheduleProcess;

            ObservableCollection<TaskDetail> Planning = new ObservableCollection<TaskDetail>();
            Planning.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 3),
                FinishDate = new DateTime(2014, 2, 7),
                Name = "Plan timeline",
                ID = "3",
                Progress = 100
            });
            Planning.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 3),
                FinishDate = new DateTime(2014, 2, 7),
                Name = "Plan budget",
                ID = "4",
                Progress = 100
            });
            Planning.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 3),
                FinishDate = new DateTime(2014, 2, 7),
                Name = "Allocate resources",
                ID = "5",
                Progress = 100
            });
            Planning.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 7),
                FinishDate = new DateTime(2014, 2, 7),
                Name = "Planning complete",
                ID = "6",
                Progress = 100
            });
            Planning[3].Predecessors.Add(new TaskRelationship
            {
                ID = "3",
                Relationship = Relationship.FinishToStart
            });
            Planning[3].Predecessors.Add(new TaskRelationship
            {
                ID = "4",
                Relationship = Relationship.FinishToStart
            });
            Planning[3].Predecessors.Add(new TaskRelationship
            {
                ID = "5",
                Relationship = Relationship.FinishToStart
            });

            Planning[0].Resources.Add("6");
            Planning[1].Resources.Add("6");
            Planning[2].Resources.Add("6");

            ScheduleProcess[0].Children = Planning;

            ObservableCollection<TaskDetail> Design = new ObservableCollection<TaskDetail>();
            Design.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 10),
                FinishDate = new DateTime(2014, 2, 12),
                Name = "Software specification",
                ID = "8",
                Progress = 60
            });
            Design.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 10),
                FinishDate = new DateTime(2014, 2, 12),
                Name = "Develop prototype",
                ID = "9",
                Progress = 100
            });
            Design.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 13),
                FinishDate = new DateTime(2014, 2, 14),
                Name = "Get approval from customer",
                ID = "10",
                Progress = 100
            });
            Design.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 14),
                FinishDate = new DateTime(2014, 2, 14),
                Name = "Design complete",
                ID = "11"
            });
            Design[2].Predecessors.Add(new TaskRelationship
            {
                ID = "9",
                Relationship = Relationship.FinishToStart
            });
            Design[3].Predecessors.Add(new TaskRelationship
            {
                ID = "10",
                Relationship = Relationship.FinishToStart
            });

            Design[0].Resources.Add("7");
            Design[1].Resources.Add("8");
            Design[2].Resources.Add("6");

            ScheduleProcess[1].Children = Design;

            ObservableCollection<TaskDetail> ImplementationPhase = new ObservableCollection<TaskDetail>();
            ImplementationPhase.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                FinishDate = new DateTime(2014, 2, 27),
                Name = "Phase 1",
                ID = "13"
            });
            ImplementationPhase.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                FinishDate = new DateTime(2014, 2, 28),
                Name = "Phase 2",
                ID = "21"
            });
            ImplementationPhase.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                FinishDate = new DateTime(2014, 2, 27),
                Name = "Phase 3",
                ID = "29"
            });

            Planning[1].Predecessors.Add(new TaskRelationship
            {
                ID = "10",
                Relationship = Relationship.FinishToStart
            });
            Planning[2].Predecessors.Add(new TaskRelationship
            {
                ID = "11",
                Relationship = Relationship.FinishToStart
            });

            ScheduleProcess[2].Children = ImplementationPhase;

            ObservableCollection<TaskDetail> ImplementationModule1 = new ObservableCollection<TaskDetail>();
            ImplementationModule1.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                FinishDate = new DateTime(2014, 2, 27),
                Name = "Implementation module 1",
                Duration = 9,
                ID = "14"
            });

            ImplementationPhase[0].Children = ImplementationModule1;

            ObservableCollection<TaskDetail> ImplementiationModule1Child = new ObservableCollection<TaskDetail>();
            ImplementiationModule1Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                FinishDate = new DateTime(2014, 2, 19),
                Name = "Development task 1",
                ID = "15",
                Progress = 50
            });
            ImplementiationModule1Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                FinishDate = new DateTime(2014, 2, 19),
                Name = "Development task 2",
                ID = "16",
                Progress = 50
            });
            ImplementiationModule1Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 20),
                FinishDate = new DateTime(2014, 2, 21),
                Name = "Testing",
                ID = "17"
            });
            ImplementiationModule1Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 24),
                FinishDate = new DateTime(2014, 2, 25),
                Name = "Bug fix",
                ID = "18"
            });
            ImplementiationModule1Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 26),
                FinishDate = new DateTime(2014, 2, 27),
                Name = "Customer review meeting",
                ID = "19"
            });
            ImplementiationModule1Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 27),
                FinishDate = new DateTime(2014, 2, 27),
                Name = "Phase 1 complete",
                ID = "20"
            });

            ImplementiationModule1Child[1].Predecessors.Add(new TaskRelationship
            {
                ID = "15",
                Relationship = Relationship.FinishToStart
            });
            ImplementiationModule1Child[2].Predecessors.Add(new TaskRelationship
            {
                ID = "16",
                Relationship = Relationship.FinishToStart
            });
            ImplementiationModule1Child[3].Predecessors.Add(new TaskRelationship
            {
                ID = "17",
                Relationship = Relationship.FinishToStart
            });
            ImplementiationModule1Child[4].Predecessors.Add(new TaskRelationship
            {
                ID = "18",
                Relationship = Relationship.FinishToStart
            });
            ImplementiationModule1Child[5].Predecessors.Add(new TaskRelationship
            {
                ID = "19",
                Relationship = Relationship.FinishToStart
            });

            ImplementiationModule1Child[0].Resources.Add("8");
            ImplementiationModule1Child[1].Resources.Add("8");
            ImplementiationModule1Child[2].Resources.Add("9");
            ImplementiationModule1Child[3].Resources.Add("8");
            ImplementiationModule1Child[4].Resources.Add("6");

            ImplementationModule1[0].Children = ImplementiationModule1Child;

            ObservableCollection<TaskDetail> ImplementationModule2 = new ObservableCollection<TaskDetail>();
            ImplementationModule2.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                Name = "Implementation module 2",
                FinishDate = new DateTime(2014, 2, 28),
                ID = "22"
            });

            ImplementationPhase[1].Children = ImplementationModule2;

            ObservableCollection<TaskDetail> ImplementiationModule2Child = new ObservableCollection<TaskDetail>();
            ImplementiationModule2Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                FinishDate = new DateTime(2014, 2, 20),
                Name = "Development task 1",
                ID = "23",
                Progress = 50
            });
            ImplementiationModule2Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                FinishDate = new DateTime(2014, 2, 20),
                Name = "Development task 2",
                ID = "24",
                Progress = 50
            });
            ImplementiationModule2Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 21),
                FinishDate = new DateTime(2014, 2, 24),
                Name = "Testing",
                ID = "25"
            });
            ImplementiationModule2Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 25),
                FinishDate = new DateTime(2014, 2, 26),
                Name = "Bug fix",
                ID = "26"
            });
            ImplementiationModule2Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 27),
                FinishDate = new DateTime(2014, 2, 28),
                Name = "Customer review meeting",
                ID = "27"
            });
            ImplementiationModule2Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 28),
                FinishDate = new DateTime(2014, 2, 28),
                Name = "Phase 2 complete",
                ID = "28"
            });

            ImplementiationModule2Child[1].Predecessors.Add(new TaskRelationship
            {
                ID = "23",
                Relationship = Relationship.FinishToStart
            });
            ImplementiationModule2Child[2].Predecessors.Add(new TaskRelationship
            {
                ID = "24",
                Relationship = Relationship.FinishToStart
            });
            ImplementiationModule2Child[3].Predecessors.Add(new TaskRelationship
            {
                ID = "25",
                Relationship = Relationship.FinishToStart
            });
            ImplementiationModule2Child[4].Predecessors.Add(new TaskRelationship
            {
                ID = "26",
                Relationship = Relationship.FinishToStart
            });
            ImplementiationModule2Child[5].Predecessors.Add(new TaskRelationship
            {
                ID = "27",
                Relationship = Relationship.FinishToStart
            });

            ImplementiationModule2Child[0].Resources.Add("8");
            ImplementiationModule2Child[1].Resources.Add("8");
            ImplementiationModule2Child[2].Resources.Add("9");
            ImplementiationModule2Child[3].Resources.Add("8");
            ImplementiationModule2Child[4].Resources.Add("6");

            ImplementationModule2[0].Children = ImplementiationModule2Child;

            ObservableCollection<TaskDetail> ImplementationModule3 = new ObservableCollection<TaskDetail>();
            ImplementationModule3.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                FinishDate = new DateTime(2014, 2, 27),
                Name = "Implementation module 3",
                Duration = 9,
                ID = "30"
            });

            ImplementationPhase[2].Children = ImplementationModule3;

            ObservableCollection<TaskDetail> ImplementiationModule3Child = new ObservableCollection<TaskDetail>();
            ImplementiationModule3Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                FinishDate = new DateTime(2014, 2, 19),
                Name = "Development task 1",
                ID = "31",
                Progress = 50
            });
            ImplementiationModule3Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 17),
                FinishDate = new DateTime(2014, 2, 19),
                Name = "Development task 2",
                ID = "32",
                Progress = 50
            });
            ImplementiationModule3Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 20),
                FinishDate = new DateTime(2014, 2, 21),
                Name = "Testing",
                ID = "33"
            });
            ImplementiationModule3Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 24),
                FinishDate = new DateTime(2014, 2, 25),
                Name = "Bug fix",
                ID = "34"
            });
            ImplementiationModule3Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 26),
                FinishDate = new DateTime(2014, 2, 27),
                Name = "Customer review meeting",
                ID = "35"
            });
            ImplementiationModule3Child.Add(new TaskDetail
            {
                StartDate = new DateTime(2014, 2, 27),
                FinishDate = new DateTime(2014, 2, 27),
                Name = "Phase 3 complete",
                ID = "36"
            });

            ImplementiationModule3Child[1].Predecessors.Add(new TaskRelationship
            {
                ID = "31",
                Relationship = Relationship.FinishToStart
            });
            ImplementiationModule3Child[2].Predecessors.Add(new TaskRelationship
            {
                ID = "32",
                Relationship = Relationship.FinishToStart
            });
            ImplementiationModule3Child[3].Predecessors.Add(new TaskRelationship
            {
                ID = "33",
                Relationship = Relationship.FinishToStart
            });
            ImplementiationModule3Child[4].Predecessors.Add(new TaskRelationship
            {
                ID = "34",
                Relationship = Relationship.FinishToStart
            });
            ImplementiationModule3Child[5].Predecessors.Add(new TaskRelationship
            {
                ID = "35",
                Relationship = Relationship.FinishToStart
            });

            ImplementiationModule3Child[0].Resources.Add("8");
            ImplementiationModule3Child[1].Resources.Add("8");
            ImplementiationModule3Child[2].Resources.Add("9");
            ImplementiationModule3Child[3].Resources.Add("8");
            ImplementiationModule3Child[4].Resources.Add("6");

            ImplementationModule3[0].Children = ImplementiationModule3Child;

            return Schedule;
        }

        public void Dispose()
        {
            if (this.TaskCollection != null)
            {
                this.TaskCollection.Clear();
            }

            if (this.ResourceCollection != null)
            {
                this.ResourceCollection.Clear();
            }

            if (this.StriplineCollection != null)
            {
                this.StriplineCollection.Clear();
            }
        }
    }
}
