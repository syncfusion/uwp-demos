#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
using Common;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfGantt
{
    using System.Collections.ObjectModel;
    using global::Windows.UI;
    using Syncfusion.UI.Xaml.Gantt;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TemplateSupport : SampleLayout
    {
        public TemplateSupport()
        {
            this.InitializeComponent();
        }
    }

    public class TemplateSupportModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CriticalPathModel"/> class.
        /// </summary>
        public TemplateSupportModel()
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
            var data = new ObservableCollection<TaskDetail>();
            data.Add(new TaskDetail() { ID = "1", Name = "Analysis/Planning", StartDate = new DateTime(2011, 7, 3), FinishDate = new DateTime(2011, 7, 14), Progress = 40d });
            data[0].Children.Add((new TaskDetail() { ID = "2", Name = "Identify Components to be Localized", StartDate = new DateTime(2011, 7, 3), FinishDate = new DateTime(2011, 7, 10), Progress = 50d }));
            data[0].Children.Add((new TaskDetail() { ID = "3", Name = "Ensure file localizability", StartDate = new DateTime(2011, 7, 6), FinishDate = new DateTime(2011, 7, 9), Progress = 50d }));
            data[0].Children.Add((new TaskDetail() { ID = "4", Name = "Identify tools", StartDate = new DateTime(2011, 7, 8), FinishDate = new DateTime(2011, 7, 11), Progress = 60d }));
            data[0].Children.Add((new TaskDetail() { ID = "5", Name = "Develop delivery timeline", StartDate = new DateTime(2011, 7, 9), FinishDate = new DateTime(2011, 7, 14), Progress = 40d }));
            data[0].Children.Add((new TaskDetail() { ID = "6", Name = "Analysis Complete", StartDate = new DateTime(2011, 7, 10), FinishDate = new DateTime(2011, 7, 10), Progress = 80d }));

            data.Add(new TaskDetail() { ID = "13", Name = "Quality Assurance", StartDate = new DateTime(2011, 7, 12), FinishDate = new DateTime(2011, 7, 28), Progress = 40d, });
            data[1].Children.Add((new TaskDetail() { ID = "14", Name = "Review project information", StartDate = new DateTime(2011, 7, 12), FinishDate = new DateTime(2011, 7, 18), Progress = 50d }));
            data[1].Children.Add((new TaskDetail() { ID = "15", Name = "Localization Component", StartDate = new DateTime(2011, 7, 15), FinishDate = new DateTime(2011, 7, 19), Progress = 40d }));
            data[1].Children.Add((new TaskDetail() { ID = "16", Name = "User Assistance Components", StartDate = new DateTime(2011, 7, 12), FinishDate = new DateTime(2011, 7, 18), Progress = 60d }));
            data[1].Children.Add((new TaskDetail() { ID = "17", Name = "Software components complete", StartDate = new DateTime(2011, 7, 15), FinishDate = new DateTime(2011, 7, 20), Progress = 60d }));

            data.Add(new TaskDetail() { ID = "24", Name = "Post-Project Review", StartDate = new DateTime(2011, 7, 20), FinishDate = new DateTime(2011, 8, 4), Progress = 40d, });
            data[2].Children.Add((new TaskDetail() { ID = "25", Name = "Finalize cost analysis", StartDate = new DateTime(2011, 7, 20), FinishDate = new DateTime(2011, 7,25), Progress = 70d }));
            data[2].Children.Add((new TaskDetail() { ID = "26", Name = "Analyze performance", StartDate = new DateTime(2011, 7, 19), FinishDate = new DateTime(2011, 7,28), Progress = 80d }));
            data[2].Children.Add((new TaskDetail() { ID = "27", Name = "Archive files", StartDate = new DateTime(2011, 7, 22), FinishDate = new DateTime(2011, 7, 29), Progress = 60d }));

            data[0].Children[1].Predecessors.Add(new TaskRelationship(){ID = "2",Relationship = Relationship.FinishToStart});
            data[0].Children[2].Predecessors.Add(new TaskRelationship() { ID = "3", Relationship = Relationship.FinishToStart });
            data[0].Children[3].Predecessors.Add(new TaskRelationship() { ID = "4", Relationship = Relationship.FinishToStart });
            data[0].Children[4].Predecessors.Add(new TaskRelationship() { ID = "5", Relationship = Relationship.FinishToStart });

            data[1].Children[3].Predecessors.Add(new TaskRelationship() { ID = "14", Relationship = Relationship.FinishToStart });
            data[1].Children[3].Predecessors.Add(new TaskRelationship() { ID = "15", Relationship = Relationship.FinishToStart });
            data[1].Children[3].Predecessors.Add(new TaskRelationship() { ID = "16", Relationship = Relationship.FinishToStart });

            data[2].Children[1].Predecessors.Add(new TaskRelationship() { ID = "25", Relationship = Relationship.FinishToStart });
            data[2].Children[2].Predecessors.Add(new TaskRelationship() { ID = "26", Relationship = Relationship.FinishToStart });

            return data;

        }
    }

    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartItemsPanel chartItemsPanel = value as ChartItemsPanel;
            TaskDetail taskDetail = chartItemsPanel.Item as TaskDetail;
            if (taskDetail != null)
            {
                if (int.Parse(taskDetail.ID) <= 7)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 119, 166, 238));
                }
                else if (int.Parse(taskDetail.ID) <= 17)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 175, 148, 223));
                }
                else
                {
                    return new SolidColorBrush(Color.FromArgb(255, 101, 193, 111));
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class ProgressColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartItemsPanel chartItemsPanel = value as ChartItemsPanel;
            TaskDetail taskDetail = chartItemsPanel.Item as TaskDetail;
            if (taskDetail != null)
            {
                if (int.Parse(taskDetail.ID) <= 7)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 71, 115, 181));
                }
                else if (int.Parse(taskDetail.ID) <= 17)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 127, 102, 173));
                }
                else
                {
                    return new SolidColorBrush(Color.FromArgb(255, 77, 159, 85));
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class TextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartItemsPanel chartItemsPanel = value as ChartItemsPanel;
            TaskDetail taskDetail = chartItemsPanel.Item;
            if (taskDetail.IsHeader)
            {
                return taskDetail.Name;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
