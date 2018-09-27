using Common;
using Syncfusion.UI.Xaml.Kanban;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfKanban
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Getting_Started : SampleLayout
    {
        public Getting_Started()
        {
            this.InitializeComponent();

            var workflow = new WorkflowCollection();
            workflow.Add(new KanbanWorkflow() { Category = "Open", AllowedTransitions = { "InProgress", "Closed", "Closed NoChanges", "Won't Fix" } });
            workflow.Add(new KanbanWorkflow() { Category = "Postponed", AllowedTransitions = { "Open", "InProgress", "Closed", "Closed NoChanges", "Won't Fix" } });
            workflow.Add(new KanbanWorkflow() { Category = "Review", AllowedTransitions = { "InProgress", "Closed", "Postponed" } });

            workflow.Add(new KanbanWorkflow() { Category = "InProgress", AllowedTransitions = { "Review", "Postponed" } });

            Kanban.Workflows = workflow;
        }

        public override void Dispose()
        {
            if (this.grid.DataContext != null)
                this.grid.DataContext = null;

            if (this.Kanban != null)
            {
                this.Kanban.ItemsSource = null;
                if (this.Kanban.DataContext is TaskDetails)
                    (this.Kanban.DataContext as TaskDetails).Dispose();
                this.Kanban = null;
            }

            this.grid.Resources = null;
            base.Dispose();
        }
    }

    public class TaskDetails
    {
        public TaskDetails()
        {
            Tasks = new ObservableCollection<KanbanModel>();

            KanbanModel task = new KanbanModel();
            task.Title = "UWP Issue";
            task.ID = "6593";
            task.Description = "Sorting is not working properly in DateTimeAxis";
            task.Category = "Postponed";
            task.ColorKey = "High";
            task.Tags = new string[] { "Bug Fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "New Feature";
            task.ID = "6593";
            task.Description = "Need to create code base for Gantt control";
            task.Category = "Postponed";
            task.ColorKey = "Low";
            task.Tags = new string[] { "GanttControl UWP" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image1.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "UG";
            task.ID = "6516";
            task.Description = "Need to do post processing work for closed incidents";
            task.Category = "Postponed";
            task.ColorKey = "Normal";
            task.Tags = new string[] { "Post processing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image2.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "UWP Issue";
            task.ID = "651";
            task.Description = "Crosshair label template not visible in UWP.";
            task.Category = "Open";
            task.ColorKey = "High";
            task.Tags = new string[] { "Bug Fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image3.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "UWP Issue";
            task.ID = "646";
            task.Description = "AxisLabel cropped when rotate the axis label.";
            task.Category = "Open";
            task.ColorKey = "Low";
            task.Tags = new string[] { "Bug Fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image4.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "WPF Issue";
            task.ID = "23822";
            task.Description = "Need to implement tooltip support for histogram series.";
            task.Category = "Open";
            task.ColorKey = "High";
            task.Tags = new string[] { "Bug Fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Kanban Feature";
            task.ID = "25678";
            task.Description = "Need to prepare SampleBrowser sample";
            task.Category = "InProgress";
            task.ColorKey = "Low";
            task.Tags = new string[] { "New control" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image5.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "WP Issue";
            task.ID = "1254";
            task.Description = "HorizontalAlignment for tooltip is not working";
            task.Category = "InProgress";
            task.ColorKey = "High";
            task.Tags = new string[] { "Bug fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image2.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "WPF Issue";
            task.ID = "28066";
            task.Description = "In minimized state, first and last segment have incorrect spacing";
            task.Category = "Review";
            task.ColorKey = "Normal";
            task.Tags = new string[] { "Bug Fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image1.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "WPF Issue";
            task.ID = "29477";
            task.Description = "Null reference exception thrown in line chart";
            task.Category = "Review";
            task.ColorKey = "Normal";
            task.Tags = new string[] { "Bug Fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image7.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "WPF Issue";
            task.ID = "29574";
            task.Description = "Minimum and maximum property are not working in dynamic update";
            task.Category = "Review";
            task.ColorKey = "High";
            task.Tags = new string[] { "Bug Fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);
            
            task = new KanbanModel();
            task.Title = "Kanban Feature";
            task.ID = "29574";
            task.Description = "Need to implement tooltip support for SfKanban";
            task.Category = "Review";
            task.ColorKey = "High";
            task.Tags = new string[] { "New Control" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image4.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "New Feature";
            task.ID = "29574";
            task.Description = "Dragging events support for SfKanban";
            task.Category = "Closed";
            task.ColorKey = "Normal";
            task.Tags = new string[] { "New Control" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image5.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "New Feature";
            task.ID = "29574";
            task.Description = "Swimlane support for SfKanban";
            task.Category = "Open";
            task.ColorKey = "Low";
            task.Tags = new string[] { "New Control" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image8.png");
            Tasks.Add(task);
        }
        public ObservableCollection<KanbanModel> Tasks { get; set; }

        public void Dispose()
        {
            if (Tasks != null)
                Tasks.Clear();
        }
    }
}