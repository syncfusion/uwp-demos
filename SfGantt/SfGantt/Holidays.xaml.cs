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
using Windows.UI;
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
    public sealed partial class Holidays : SampleLayout
    {
        public Holidays()
        {
            this.InitializeComponent();           
        }

    }

    public class HolidaysViewModel
    {
        public ObservableCollection<TaskDetail> TaskCollection { get; set; }

        public ObservableCollection<GanttHoliday> Holidays { get; set; }

        public HolidaysViewModel()
        {
            TaskCollection = this.GetData();
            Holidays = GetHolidays();
        }


        public ObservableCollection<GanttHoliday> GetHolidays()
        {
            GanttHolidayCollection holidays = new GanttHolidayCollection();
            holidays.Add(new GanttHoliday() { Background = new SolidColorBrush(Colors.CadetBlue), Day = new DateTime(2018, 5, 28) });
            holidays.Add(new GanttHoliday() { Background = new SolidColorBrush(Colors.CadetBlue), Day = new DateTime(2018, 7, 4) });
            holidays.Add(new GanttHoliday() { Background = new SolidColorBrush(Colors.CadetBlue), Day = new DateTime(2018, 9,3 ) });            
            holidays.Add(new GanttHoliday() { Background = new SolidColorBrush(Colors.CadetBlue), Day = new DateTime(2018, 11, 11) });
            holidays.Add(new GanttHoliday() { Background = new SolidColorBrush(Colors.CadetBlue), Day = new DateTime(2018, 11, 12) });
            holidays.Add(new GanttHoliday() { Background = new SolidColorBrush(Colors.CadetBlue), Day = new DateTime(2018, 12, 25) });            
            return holidays;
        }

        public ObservableCollection<TaskDetail> GetData()
        {
            ObservableCollection<TaskDetail> Activities = new ObservableCollection<TaskDetail>();

            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 6, 1), FinishDate = new DateTime(2018, 6, 18), Name = "Analysing Market Scope",  ID = "1" });

            ObservableCollection<TaskDetail> MarketAnalysis = new ObservableCollection<TaskDetail>();
            MarketAnalysis.Add(new TaskDetail { StartDate = new DateTime(2018, 6, 1), FinishDate = new DateTime(2018, 6, 6), Name = "Current Market Review", ID = "2" });
            MarketAnalysis.Add(new TaskDetail { StartDate = new DateTime(2018, 6, 6), FinishDate = new DateTime(2018, 6, 9), Name = "Establish mislestone", ID = "3" });
            MarketAnalysis.Add(new TaskDetail { StartDate = new DateTime(2018, 6, 9), FinishDate = new DateTime(2018, 6, 10), Name = "Establish goals", ID = "4" });
            MarketAnalysis.Add(new TaskDetail { StartDate = new DateTime(2018, 6, 10), FinishDate = new DateTime(2018, 6, 13), Name = "Sales, marketing", ID = "5" });
            MarketAnalysis.Add(new TaskDetail { StartDate = new DateTime(2018, 6, 11), FinishDate = new DateTime(2018, 6, 14), Name = "Define product goals", ID = "6" });
            MarketAnalysis.Add(new TaskDetail { StartDate = new DateTime(2018, 6, 12), FinishDate = new DateTime(2018, 6, 17), Name = "Organization status review", ID = "7" });
            MarketAnalysis.Add(new TaskDetail { StartDate = new DateTime(2018, 6, 18), FinishDate = new DateTime(2018, 6, 18), Name = "Market Scope", ID = "8" });          
            Activities[0].Children = MarketAnalysis;


            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 6, 18), FinishDate = new DateTime(2018, 7, 14), Name = "Infrastructure", ID = "9" });
            ObservableCollection<TaskDetail> InfrastructureReq = new ObservableCollection<TaskDetail>();
            InfrastructureReq.Add(new TaskDetail { StartDate = new DateTime(2018, 6, 18), FinishDate = new DateTime(2018, 6, 24), Name = "Procedure for ideas", ID = "10" });
            InfrastructureReq.Add(new TaskDetail { StartDate = new DateTime(2018, 6, 24), FinishDate = new DateTime(2018, 7, 7), Name = "Process for idea", ID = "11" });
            InfrastructureReq.Add(new TaskDetail { StartDate = new DateTime(2018, 7, 7), FinishDate = new DateTime(2018, 7, 14), Name = "Product Planning", ID = "12" });            
            Activities[1].Children = InfrastructureReq;

            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 7, 14), FinishDate = new DateTime(2018, 8, 29), Name = "Product Definition Phase", ID = "13" });
            ObservableCollection<TaskDetail> Product = new ObservableCollection<TaskDetail>();
            Product.Add(new TaskDetail { StartDate = new DateTime(2018, 7, 14), FinishDate = new DateTime(2018, 7, 25), Name = "Identify product", ID = "14" });
            Product.Add(new TaskDetail { StartDate = new DateTime(2018, 7, 28), FinishDate = new DateTime(2018, 8, 1), Name = "Identify need ", ID = "15" });
            Product.Add(new TaskDetail { StartDate = new DateTime(2018, 8, 4), FinishDate = new DateTime(2018, 8, 8), Name = "Identify current trend", ID = "16" });
            Product.Add(new TaskDetail { StartDate = new DateTime(2018, 8, 4), FinishDate = new DateTime(2018, 8, 29), Name = "Define product use", ID = "17" });
            Product.Add(new TaskDetail { StartDate = new DateTime(2018, 8, 4), FinishDate = new DateTime(2018, 8, 8), Name = "Identify competitor product", ID = "18" });
            Product.Add(new TaskDetail { StartDate = new DateTime(2018, 8, 29), FinishDate = new DateTime(2018, 8, 29), Name = "Product Definition Complete", ID = "19" });
          
            Activities[2].Children = Product;

            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 2), FinishDate = new DateTime(2018, 9, 10), Name = "Analysing Customer Requirement", ID = "20" });
            ObservableCollection<TaskDetail> Customer = new ObservableCollection<TaskDetail>();
            Customer.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 2), FinishDate = new DateTime(2018, 9, 4), Name = "Identify Consumer", ID = "21" });
            Customer.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 3), FinishDate = new DateTime(2018, 9, 6), Name = "Identify Requirement", ID = "22" });
            Customer.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 5), FinishDate = new DateTime(2018, 9, 8), Name = "Analysing Requirement", ID = "23" });
            Customer.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 7), FinishDate = new DateTime(2018, 9, 10), Name = "Design", ID = "24" });
            Customer.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 10), FinishDate = new DateTime(2018, 9, 10), Name = "Analysis Complete", ID = "25" });
           
            Activities[3].Children = Customer;

            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 2), FinishDate = new DateTime(2018, 10, 10), Name = "Competitor Analysis", ID = "26" });
            ObservableCollection<TaskDetail> Competitor = new ObservableCollection<TaskDetail>();
            Competitor.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 2), FinishDate = new DateTime(2018, 9, 13), Name = "Competitor with similar Product", ID = "27" });
            Competitor.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 13), FinishDate = new DateTime(2018, 9, 20), Name = "Competitive advantage", ID = "28" });
            Competitor.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 22), FinishDate = new DateTime(2018, 9, 27), Name = "Identify competitive features", ID = "29" });
            Competitor.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 29), FinishDate = new DateTime(2018, 10, 10), Name = "Define how to build", ID = "30" });          

            Activities[4].Children = Competitor;

            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 9), FinishDate = new DateTime(2018, 9, 20), Name = "Defining Sucess Measure", ID = "31" });
            ObservableCollection<TaskDetail> Measure = new ObservableCollection<TaskDetail>();
            Measure.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 2), FinishDate = new DateTime(2018, 9, 6), Name = "Identify Risks", ID = "32" });
            Measure.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 2), FinishDate = new DateTime(2018, 9, 6), Name = "Define Key success measures", ID = "33" });
            Measure.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 7), FinishDate = new DateTime(2018, 9, 13), Name = "Address risks", ID = "34" });
            Measure.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 13), FinishDate = new DateTime(2018, 9, 20), Name = "Meet market position", ID = "35" });
            Measure.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 20), FinishDate = new DateTime(2018, 9, 20), Name = "Success Measure Defined", ID = "36" });         
            Activities[5].Children = Measure;

            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 23), FinishDate = new DateTime(2018, 10, 17), Name = "Defining Team", ID = "37" });
            ObservableCollection<TaskDetail> Team = new ObservableCollection<TaskDetail>();
            Team.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 23), FinishDate = new DateTime(2018, 9, 27), Name = "Define components", ID = "38" });
            Team.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 30), FinishDate = new DateTime(2018, 10, 3), Name = "Identify Key qualities", ID = "39" });
            Team.Add(new TaskDetail { StartDate = new DateTime(2018, 10, 6), FinishDate = new DateTime(2018, 10, 10), Name = "Define team members", ID = "40" });
            Team.Add(new TaskDetail { StartDate = new DateTime(2018, 10, 13), FinishDate = new DateTime(2018, 10, 17), Name = "Identify and address gaps", ID = "41" });
            Team.Add(new TaskDetail { StartDate = new DateTime(2018, 10, 17), FinishDate = new DateTime(2018, 10, 17), Name = "Team Defined", ID = "42" });

            Activities[6].Children = Team;

            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 2), FinishDate = new DateTime(2010, 9, 24), Name = "Budgeting in the Product", ID = "43" });
            ObservableCollection<TaskDetail> Budget = new ObservableCollection<TaskDetail>();
            Budget.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 2), FinishDate = new DateTime(2018, 9, 3), Name = "Define financial metrics", ID = "44" });
            Budget.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 3), FinishDate = new DateTime(2018, 9, 13), Name = "Estimate cost", ID = "45" });
            Budget.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 13), FinishDate = new DateTime(2018, 9, 15), Name = "Estimate time", ID = "46" });
            Budget.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 15), FinishDate = new DateTime(2018, 9, 20), Name = "Analyse resource cost", ID = "47" });
            Budget.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 20), FinishDate = new DateTime(2018, 9, 24), Name = "Define financial plan", ID = "48" });
            Budget.Add(new TaskDetail { StartDate = new DateTime(2018, 9, 24), FinishDate = new DateTime(2018, 9, 24), Name = "Product Budget defined", ID = "49" });
        
            Activities[7].Children = Budget;

            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 10, 20), FinishDate = new DateTime(2018, 11, 10), Name = "Product Development", ID = "50" });
            ObservableCollection<TaskDetail> Development = new ObservableCollection<TaskDetail>();
            Development.Add(new TaskDetail { StartDate = new DateTime(2018, 10, 20), FinishDate = new DateTime(2018, 10, 30), Name = "Implementation Pahse 1", ID = "51" });
            Development.Add(new TaskDetail { StartDate = new DateTime(2018, 10, 30), FinishDate = new DateTime(2018, 11, 10), Name = "Implementation Pahse 2", ID = "52" });
            Development.Add(new TaskDetail { StartDate = new DateTime(2018, 11, 10), FinishDate = new DateTime(2018, 11, 10), Name = "Product Developed", ID = "53" });

            Activities[8].Children = Development;

            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 11, 8), FinishDate = new DateTime(2018, 11, 13), Name = "Product Review", ID = "54" });
            Activities[9].Children.Add(new TaskDetail { StartDate = new DateTime(2018, 11, 8), FinishDate = new DateTime(2018, 11, 10), Name = "Techincal Review", ID = "55" });
            Activities[9].Children.Add(new TaskDetail { StartDate = new DateTime(2018, 11, 9), FinishDate = new DateTime(2018, 11, 13), Name = "Cost Review", ID = "56" });

            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 11, 15), FinishDate = new DateTime(2018, 11, 30), Name = "Beta Testing", ID = "57" });
            ObservableCollection<TaskDetail> Testing = new ObservableCollection<TaskDetail>();
            Testing.Add((new TaskDetail { StartDate = new DateTime(2018, 11, 15), FinishDate = new DateTime(2018, 11, 17), Name = "Disseminate completed product", ID = "58" }));
            Testing.Add((new TaskDetail { StartDate = new DateTime(2018, 11, 18), FinishDate = new DateTime(2018, 11, 20), Name = "Obtain feedback", ID = "59" }));
            Testing.Add((new TaskDetail { StartDate = new DateTime(2018, 11, 20), FinishDate = new DateTime(2018, 11, 25), Name = "Modification", ID = "60" }));
            Testing.Add((new TaskDetail { StartDate = new DateTime(2018, 11, 24), FinishDate = new DateTime(2018, 11, 30), Name = "Test", ID = "61" }));
            Testing.Add((new TaskDetail { StartDate = new DateTime(2018, 11, 30), FinishDate = new DateTime(2018, 11, 30), Name = "Testing Completed", ID = "62" }));     
           
            Activities[10].Children = Testing;

            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 11, 25), FinishDate = new DateTime(2018, 12, 06), Name = "Post Product Review", ID = "63" });
            ObservableCollection<TaskDetail> PostReview = new ObservableCollection<TaskDetail>();
            PostReview.Add((new TaskDetail { StartDate = new DateTime(2018, 11, 25), FinishDate = new DateTime(2018, 11, 27), Name = "Finalize cost analysis", ID = "64" }));
            PostReview.Add((new TaskDetail { StartDate = new DateTime(2018, 11, 27), FinishDate = new DateTime(2018, 11, 28), Name = "Analyze performance", ID = "65" }));
            PostReview.Add((new TaskDetail { StartDate = new DateTime(2018, 11, 29), FinishDate = new DateTime(2018, 12, 2), Name = "Archive files", ID = "66" }));
            PostReview.Add((new TaskDetail { StartDate = new DateTime(2018, 12, 2), FinishDate = new DateTime(2018, 12, 4), Name = "Document lessons", ID = "67" }));
            PostReview.Add((new TaskDetail { StartDate = new DateTime(2018, 12, 4), FinishDate = new DateTime(2018, 12, 6), Name = "Distribute to team members", ID = "68" }));
            PostReview.Add((new TaskDetail { StartDate = new DateTime(2018, 12, 6), FinishDate = new DateTime(2018, 12, 6), Name = "Post-project review complete", ID = "69" }));

            Activities[11].Children = PostReview;

            Activities.Add(new TaskDetail { StartDate = new DateTime(2018, 12, 10), FinishDate = new DateTime(2018, 12, 10), Name = "Product Released", ID = "70" });

            return Activities;
        }
    }
}
