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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfKanban
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Swimlane : SampleLayout
    {
        public Swimlane()
        {
            this.InitializeComponent();
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
            
            base.Dispose();
        }
    }

    public class SwimlaneViewModel
    {
        public SwimlaneViewModel()
        {
            Tasks = new ObservableCollection<KanbanModel>();

            KanbanModel task = new KanbanModel();
            task.Title = "Application performance";
            task.ID = "2";
            task.Assignee = "Andrew Fuller";
            task.Description = "Improve application performance.";
            task.Category = "Backlog";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image2.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Analysis";
            task.ID = "18";
            task.Assignee = "Andrew Fuller";
            task.Description = "Analyze SQL server 2008 connection.";
            task.Category = "In Progress";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image2.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Editing";
            task.ID = "25";
            task.Assignee = "Andrew Fuller";
            task.Description = "Enhance editing functionality.";
            task.Category = "Testing";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image2.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Editing";
            task.ID = "37";
            task.Assignee = "Andrew Fuller";
            task.Description = "Add input validation for editing.";
            task.Category = "Testing";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image2.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Customer meeting";
            task.ID = "42";
            task.Assignee = "Andrew Fuller";
            task.Description = "Arrange a web meeting with the customer to show filtering demo.";
            task.Category = "Done";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image2.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Filtering issue";
            task.ID = "45";
            task.Assignee = "Andrew Fuller";
            task.Description = "Fix the filtering issues reported in Safari.";
            task.Category = "Done";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image2.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Filtering feature";
            task.ID = "49";
            task.Assignee = "Andrew Fuller";
            task.Description = "Enhance filtering feature.";
            task.Category = "Done";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image2.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Customer meeting";
            task.ID = "3";
            task.Assignee = "Janet Leverling";
            task.Description = "Arrange a web meeting with the customer to get new requirements.";
            task.Category = "Backlog";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "IE browser issues";
            task.ID = "4";
            task.Assignee = "Janet Leverling";
            task.Description = "Fix the issues reported in the IE browser.";
            task.Category = "In Progress";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "SQL error";
            task.ID = "16";
            task.Assignee = "Janet Leverling";
            task.Description = "Fix cannot open user's default database SQL error.";
            task.Category = "In Progress";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Chrome issue";
            task.ID = "28";
            task.Assignee = "Janet Leverling";
            task.Description = "Fix editing issues reported in chrome.";
            task.Category = "In Progress";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Data binding issues";
            task.ID = "17";
            task.Assignee = "Janet Leverling";
            task.Description = "Fix the issues reported in data binding.";
            task.Category = "Testing";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Customer issues";
            task.ID = "29";
            task.Assignee = "Janet Leverling";
            task.Description = "Fix the editing issues reported by customer.";
            task.Category = "Testing";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Analysis";
            task.ID = "22";
            task.Assignee = "Janet Leverling";
            task.Description = "Analyze stored procedures.";
            task.Category = "Done";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Editing feature";
            task.ID = "34";
            task.Assignee = "Janet Leverling";
            task.Description = "Test editing feature in the IE browser.";
            task.Category = "Done";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Filtering feature";
            task.ID = "46";
            task.Assignee = "Janet Leverling";
            task.Description = "Test filtering in the IE browser.";
            task.Category = "Done";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Responsive support";
            task.ID = "14";
            task.Assignee = "Laura Callahan";
            task.Description = "Add responsive support to application.";
            task.Category = "In Progress";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image1.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Filtering feature";
            task.ID = "48";
            task.Assignee = "Laura Callahan";
            task.Description = "Check filtering validation.";
            task.Category = "Testing";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image1.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Login validation";
            task.ID = "8";
            task.Assignee = "Laura Callahan";
            task.Description = "Login page validation.";
            task.Category = "Done";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image1.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Data in Grid";
            task.ID = "15";
            task.Assignee = "Margaret Hamilt";
            task.Description = "Show the retrieved data from the server in grid control.";
            task.Category = "Backlog";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image3.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Filtering issues";
            task.ID = "40";
            task.Assignee = "Margaret Hamilt";
            task.Description = "Fix filtering issues reported in the IE browser.";
            task.Category = "In Progress";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image3.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Testing";
            task.ID = "10";
            task.Assignee = "Margaret Hamilt";
            task.Description = "Test the application in the IE browser.";
            task.Category = "Done";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image3.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Analysis";
            task.ID = "20";
            task.Assignee = "Margaret Hamilt";
            task.Description = "Analyze grid control.";
            task.Category = "Done";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image3.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Customer meeting";
            task.ID = "39";
            task.Assignee = "Michael Suyama";
            task.Description = "Arrange a web meeting with the customer to get new filtering requirements.";
            task.Category = "Backlog";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image5.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Testing";
            task.ID = "12";
            task.Assignee = "Michael Suyama";
            task.Description = "Check login page validation.";
            task.Category = "Testing";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image5.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Customer meeting";
            task.ID = "6";
            task.Assignee = "Michael Suyama";
            task.Description = "Arrange a web meeting with the customer to get the login page requirements.";
            task.Category = "Done";
            task.ColorKey = "Normal";
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image5.png");
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
