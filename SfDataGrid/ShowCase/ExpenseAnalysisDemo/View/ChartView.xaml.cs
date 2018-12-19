using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ExpenseAnalysisDemo
{
    public sealed partial class ChartView : UserControl, IDisposable
    {
        public ChartView()
        {
            this.InitializeComponent();
            Chart.Loaded += (s, e) =>
            {
                (Chart.Annotations[0] as ImageAnnotation).Visibility = Visibility.Collapsed;
                (Chart.Annotations[0] as ImageAnnotation).X1 = (Chart.ActualWidth / 2) - 15;
                (Chart.Annotations[0] as ImageAnnotation).X2 = (Chart.ActualWidth / 2) + 15;
                (Chart.Annotations[0] as ImageAnnotation).Y1 = (Chart.ActualHeight / 2) - 15;
                (Chart.Annotations[0] as ImageAnnotation).Y2 = (Chart.ActualHeight / 2) + 15;
            };

            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                (Chart.Series[0] as PieSeries).AdornmentsInfo.ConnectorLineStyle = this.Resources["lineStyle_Wp"] as Style;
                (Chart.Series[0] as PieSeries).AdornmentsInfo.LabelTemplate = this.Resources["labelTemplate_Wp"] as DataTemplate;
                (Chart.Series[0] as PieSeries).AdornmentsInfo.ConnectorHeight = 10;
            }
            else
            {
                (Chart.Series[0] as PieSeries).AdornmentsInfo.ConnectorLineStyle = this.Resources["lineStyle"] as Style;
                (Chart.Series[0] as PieSeries).AdornmentsInfo.LabelTemplate = this.Resources["labelTemplate"] as DataTemplate;
            }

            Chart.DataContextChanged += Chart_DataContextChanged; ;
            Chart.SelectionChanged += Chart_SelectionChanged;
            chartadornment.AdornmentsPosition = AdornmentsPosition.Top;
            chartadornment.ConnectorHeight = 20;
            chartadornment.ConnectorRotationAngle = 90;
            chartadornment.SegmentLabelContent = LabelContent.LabelContentPath;
            chartadornment.ShowConnectorLine = true;
            chartadornment.ShowLabel = true;
            this.Unloaded += OnChartViewUnloaded;
        }

        private void OnChartViewUnloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void Chart_SelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            if (e.SelectedSeries == null)
                return;

            var viewmodel = e.SelectedSeries.DataContext as ViewModel;
            var category = (e.SelectedSegment.Item as CompanyExpense).Category;
            var selecteditemgroup = viewmodel.Expenses.GroupBy(x => x.CategoryName).FirstOrDefault(expense => expense.Key == category);

            if (selecteditemgroup == null)
                return;

            var subcategory = selecteditemgroup.GroupBy(ed => ed.SubCategory);
            Chart.Series[0].ItemsSource = GetCompanyExpense(subcategory);
            (Chart.Annotations[0] as ImageAnnotation).Visibility = Visibility.Visible;
        }

        private void Chart_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
           
            var viewModel = sender.DataContext as ViewModel;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
            Chart.Series[0].ItemsSource = GetPieExpense(viewModel);
        }

        public ObservableCollection<CompanyExpense> GetPieExpense(ViewModel viewModel)
        {
            var group = viewModel.Expenses.GroupBy(e => e.CategoryName);
            return GetCompanyExpense(group);
        }

        

        void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Expenses")
            {
                (Chart.Annotations[0] as ImageAnnotation).Visibility = Visibility.Collapsed;
                var viewModel = sender as ViewModel;
                Chart.Series[0].ItemsSource = GetPieExpense(viewModel);
            }
        }

        private ObservableCollection<CompanyExpense> GetCompanyExpense(IEnumerable<IGrouping<string, ExpenseData>> expenses)
        {
            var pieExpense = new ObservableCollection<CompanyExpense>();
            foreach (var item in expenses)
            {
                var companyExpense = new CompanyExpense();
                companyExpense.Category = item.Key;
                companyExpense.Amount = item.Sum(x => x.Amount);
                if (companyExpense.Category != "Financial obligations" && companyExpense.Category != "Income" && companyExpense.Category != "Dues/subscriptions")
                    pieExpense.Add(companyExpense);
            }
            return pieExpense;
        }

        public void Dispose()
        {
            this.Resources.Clear();
            (Chart.DataContext as ViewModel).PropertyChanged -= viewModel_PropertyChanged;
            Chart.DataContextChanged -= Chart_DataContextChanged;
            Chart.SelectionChanged -= Chart_SelectionChanged;
            if (this.Chart != null)
            {
                foreach (var series in this.Chart.Series)
                {
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                }
            }
            Chart.Annotations.Clear();
            this.Unloaded -= OnChartViewUnloaded;
            if (DataContext != null)
                (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            this.Content = null;
        }
    }

    public class CustomImageAnnotation : ImageAnnotation
    {
        public SfChart ParentChart
        {
            get { return (SfChart)GetValue(ParentChartProperty); }
            set { SetValue(ParentChartProperty, value); }
        }

        public static readonly DependencyProperty ParentChartProperty =
            DependencyProperty.Register("ParentChart", typeof(SfChart), typeof(CustomImageAnnotation), new PropertyMetadata(null));

        public override void UpdateAnnotation()
        {
            base.UpdateAnnotation();
            var elements = this.AnnotationElement;
            if (AnnotationElement.Children.Count > 0)
            {
                Image image = AnnotationElement.Children[0] as Image;
                image.PointerReleased += image_PointerReleased;
            }
        }

        void image_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
			if (Parent == null) return;
            ParentChart.Series[0].ItemsSource = GetPieExpense(ParentChart.DataContext as ViewModel);
            (ParentChart.Annotations[0] as ImageAnnotation).Visibility = Visibility.Collapsed;
        }

        public ObservableCollection<CompanyExpense> GetPieExpense(ViewModel viewModel)
        {
            var group = viewModel.Expenses.GroupBy(e => e.CategoryName);
            return GetCompanyExpense(group);
        }

        private ObservableCollection<CompanyExpense> GetCompanyExpense(IEnumerable<IGrouping<string, ExpenseData>> expenses)
        {
            var pieExpense = new ObservableCollection<CompanyExpense>();
            foreach (var item in expenses)
            {
                var companyExpense = new CompanyExpense();
                companyExpense.Category = item.Key;
                companyExpense.Amount = item.Sum(x => x.Amount);
                if (companyExpense.Category != "Financial obligations" && companyExpense.Category != "Income" && companyExpense.Category != "Dues/subscriptions")
                    pieExpense.Add(companyExpense);
            }
            return pieExpense;
        }

    }

}
