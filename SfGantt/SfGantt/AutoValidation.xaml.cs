using Common;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfGantt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AutoValidation : SampleLayout
    {
        public AutoValidation()
        {
            this.InitializeComponent();
        }
    }

    public class TaskViewModel
    {
        public ObservableCollection<TaskDetail> TaskCollection { get; set; }

        public TaskViewModel()
        {
            TaskCollection = GetData();
        }

        private ObservableCollection<TaskDetail> GetData()
        {
            var data = new ObservableCollection<TaskDetail>();
            data.Add(new TaskDetail() { ID = "1", Name = "Analysis/Planning", StartDate = new DateTime(2011, 7, 3), FinishDate = new DateTime(2011, 8, 14), Progress = 40d });
            data[0].Children.Add((new TaskDetail() { ID = "2", Name = "Identify Components", StartDate = new DateTime(2011, 7, 3), FinishDate = new DateTime(2011, 7, 5), Progress = 20d }));
            data[0].Children.Add((new TaskDetail() { ID = "3", Name = "Ensure file localizability", StartDate = new DateTime(2011, 7, 6), FinishDate = new DateTime(2011, 7, 7), Progress = 20d }));
            data[0].Children.Add((new TaskDetail() { ID = "4", Name = "Identify tools", StartDate = new DateTime(2011, 7, 10), FinishDate = new DateTime(2011, 7, 14), Progress = 10d }));
            data[0].Children.Add((new TaskDetail() { ID = "5", Name = "Test tools", StartDate = new DateTime(2011, 7, 14), FinishDate = new DateTime(2011, 7, 18), Progress = 10d }));
            data[0].Children.Add((new TaskDetail() { ID = "6", Name = "Develop delivery timeline", StartDate = new DateTime(2011, 7, 10), FinishDate = new DateTime(2011, 7, 20), Progress = 10d }));
            data[0].Children.Add((new TaskDetail() { ID = "7", Name = "Analysis Progress", StartDate = new DateTime(2011, 7, 14), FinishDate = new DateTime(2011, 7, 17), Progress = 10d }));

            data.Add(new TaskDetail() { ID = "8", Name = "Production", StartDate = new DateTime(2011, 7, 3), FinishDate = new DateTime(2011, 7, 14), Progress = 40d });
            data[1].Children.Add((new TaskDetail() { ID = "9", Name = "Software Components", StartDate = new DateTime(2011, 7, 3), FinishDate = new DateTime(2011, 7, 5), Progress = 20d, }));
            data[1].Children.Add((new TaskDetail() { ID = "10", Name = "Localization Component", StartDate = new DateTime(2011, 7, 6), FinishDate = new DateTime(2011, 7, 7), Progress = 20d }));
            data[1].Children.Add((new TaskDetail() { ID = "11", Name = "User Assistance Components", StartDate = new DateTime(2011, 7, 10), FinishDate = new DateTime(2011, 7, 14), Progress = 10d }));
            data[1].Children.Add((new TaskDetail() { ID = "12", Name = "Software components Progress", StartDate = new DateTime(2011, 7, 14), FinishDate = new DateTime(2011, 7, 18), Progress = 10d }));


            data.Add(new TaskDetail() { ID = "13", Name = "Quality Assurance", StartDate = new DateTime(2011, 7, 3), FinishDate = new DateTime(2011, 7, 12), Progress = 40d, });
            data[2].Children.Add((new TaskDetail() { ID = "14", Name = "Review project information", StartDate = new DateTime(2011, 7, 3), FinishDate = new DateTime(2011, 7, 15), Progress = 20d }));
            data[2].Children.Add((new TaskDetail() { ID = "15", Name = "Localization Component", StartDate = new DateTime(2011, 7, 6), FinishDate = new DateTime(2011, 7, 8), Progress = 20d }));
            data[2].Children.Add((new TaskDetail() { ID = "16", Name = "Localization Component", StartDate = new DateTime(2011, 7, 10), FinishDate = new DateTime(2011, 7, 14), Progress = 10d }));
            data[2].Children.Add((new TaskDetail() { ID = "17", Name = "Localization Component", StartDate = new DateTime(2011, 7, 14), FinishDate = new DateTime(2011, 7, 18), Progress = 10d }));

            data.Add(new TaskDetail() { ID = "18", Name = "Beta Testing", StartDate = new DateTime(2011, 7, 3), FinishDate = new DateTime(2011, 7, 14), Progress = 40d });
            data[3].Children.Add((new TaskDetail() { ID = "19", Name = "Disseminate completed product", StartDate = new DateTime(2011, 7, 3), FinishDate = new DateTime(2011, 7, 5), Progress = 20d }));
            data[3].Children.Add((new TaskDetail() { ID = "20", Name = "Obtain feedback", StartDate = new DateTime(2011, 7, 6), FinishDate = new DateTime(2011, 7, 7), Progress = 20d }));
            data[3].Children.Add((new TaskDetail() { ID = "21", Name = "Modify", StartDate = new DateTime(2011, 7, 10), FinishDate = new DateTime(2011, 7, 19), Progress = 10d }));
            data[3].Children.Add((new TaskDetail() { ID = "22", Name = "Test", StartDate = new DateTime(2011, 7, 14), FinishDate = new DateTime(2011, 7, 19), Progress = 10d }));
            data[3].Children.Add((new TaskDetail() { ID = "23", Name = "Progress", StartDate = new DateTime(2011, 7, 10), FinishDate = new DateTime(2011, 7, 19), Progress = 10d }));

            data.Add(new TaskDetail() { ID = "24", Name = "Post-Project Review", StartDate = new DateTime(2011, 7, 3), FinishDate = new DateTime(2011, 7, 14), Progress = 40d, });
            data[4].Children.Add((new TaskDetail() { ID = "25", Name = "Finalize cost analysis", StartDate = new DateTime(2011, 7, 3), FinishDate = new DateTime(2011, 7, 5), Progress = 20d }));
            data[4].Children.Add((new TaskDetail() { ID = "26", Name = "Analyze performance", StartDate = new DateTime(2011, 7, 6), FinishDate = new DateTime(2011, 7, 7), Progress = 20d }));
            data[4].Children.Add((new TaskDetail() { ID = "27", Name = "Archive files", StartDate = new DateTime(2011, 7, 10), FinishDate = new DateTime(2011, 7, 14), Progress = 10d }));
            data[4].Children.Add((new TaskDetail() { ID = "28", Name = "Document lessons learned", StartDate = new DateTime(2011, 7, 14), FinishDate = new DateTime(2011, 7, 18), Progress = 10d }));
            data[4].Children.Add((new TaskDetail() { ID = "29", Name = "Distribute to team members", StartDate = new DateTime(2011, 7, 10), FinishDate = new DateTime(2011, 7, 14), Progress = 10d }));
            data[4].Children.Add((new TaskDetail() { ID = "30", Name = "Post-project review Progress", StartDate = new DateTime(2011, 7, 10), FinishDate = new DateTime(2011, 7, 14), Progress = 10d }));

            data[0].Children[1].Predecessors.Add(new TaskRelationship() { ID = "2", Relationship = Relationship.StartToStart });
            data[0].Children[2].Predecessors.Add(new TaskRelationship() { ID = "3", Relationship = Relationship.StartToStart });
            data[0].Children[3].Predecessors.Add(new TaskRelationship() { ID = "3", Relationship = Relationship.StartToStart });

            data[1].Children[1].Predecessors.Add(new TaskRelationship() { ID = "9", Relationship = Relationship.FinishToStart });
            data[1].Children[2].Predecessors.Add(new TaskRelationship() { ID = "10", Relationship = Relationship.FinishToStart });
            data[1].Children[3].Predecessors.Add(new TaskRelationship() { ID = "7", Relationship = Relationship.StartToStart });

            data[2].Children[1].Predecessors.Add(new TaskRelationship() { ID = "12", Relationship = Relationship.FinishToFinish });
            data[2].Children[2].Predecessors.Add(new TaskRelationship() { ID = "12", Relationship = Relationship.FinishToFinish });
            data[2].Children[3].Predecessors.Add(new TaskRelationship() { ID = "12", Relationship = Relationship.FinishToFinish });

            data[3].Children[1].Predecessors.Add(new TaskRelationship() { ID = "18", Relationship = Relationship.StartToStart });
            data[3].Children[2].Predecessors.Add(new TaskRelationship() { ID = "18", Relationship = Relationship.StartToStart });
            data[3].Children[3].Predecessors.Add(new TaskRelationship() { ID = "19", Relationship = Relationship.StartToStart });

            data[4].Children[1].Predecessors.Add(new TaskRelationship() { ID = "25", Relationship = Relationship.StartToStart });
            data[4].Children[2].Predecessors.Add(new TaskRelationship() { ID = "28", Relationship = Relationship.StartToStart });
            data[4].Children[3].Predecessors.Add(new TaskRelationship() { ID = "30", Relationship = Relationship.StartToStart });
            data[4].Children[4].Predecessors.Add(new TaskRelationship() { ID = "27", Relationship = Relationship.StartToStart });

            return data;
        }
    }
}
