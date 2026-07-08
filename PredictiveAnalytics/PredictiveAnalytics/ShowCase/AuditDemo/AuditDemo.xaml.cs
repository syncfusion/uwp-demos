using System;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleBrowser.UWP.PredictiveAnalytics
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AuditDemo : Page, IDisposable
    {
        #region Private properties
        private bool ageInvalid = false;
        private bool hoursInvalid = false;
        private bool incomeInvalid = false;
        private bool deductionInvalid = false;        
        #endregion

        public AuditDemo()
        {
            this.InitializeComponent();
            this.Unloaded += AuditDemo_Unloaded;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var value = (this.DataContext as AuditDemoViewModel).PredictMethod();
            var path = (this.DataContext as AuditDemoViewModel).ImagePath;
            ImagePath.Source = path;

            var predict = (this.DataContext as AuditDemoViewModel).AuditPredicted;
            AuditPredicted.Text = predict;
            PredictedText.Text = value;
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                this.inputGrid.Visibility = Visibility.Collapsed;
                this.Prediction.Visibility = Visibility.Collapsed;
                this.resultGrid.Visibility = Visibility.Visible;
            }
        }
        private void Previous_Button(object sender, RoutedEventArgs e)
        {
            this.resultGrid.Visibility = Visibility.Collapsed;
            this.Prediction.Visibility = Visibility.Visible;
            this.inputGrid.Visibility = Visibility.Visible;
        }
       
        #region Validation event
        private void InputValidation(object sender, RoutedEventArgs e)
        {
            //Selected item
            TextBox selectedItem = e.OriginalSource as TextBox;
            this.PredictButton.IsEnabled = false;
            //Validates selected item
            if (selectedItem != null)
            {
                switch (selectedItem.Name)
                {
                    case "age":
                        {
                           ageInvalid = (string.IsNullOrEmpty(selectedItem.Text) || (Convert.ToDouble(selectedItem.Text) < 1) ||
                          (Convert.ToDouble(selectedItem.Text) > 99)) ? true : false;
                            selectedItem.BorderBrush = ageInvalid ? new SolidColorBrush(Windows.UI.Colors.Red) :
                                new SolidColorBrush(Windows.UI.Colors.LightGray);
                            ageInvalidText.Visibility = ageInvalid ? Visibility.Visible : Visibility.Collapsed;
                        }
                        break;
                    case "income":
                        {
                            incomeInvalid = ((string.IsNullOrEmpty(selectedItem.Text) || Convert.ToDouble(selectedItem.Text) < 0)) ? true : false;
                            selectedItem.BorderBrush = incomeInvalid ? new SolidColorBrush(Windows.UI.Colors.Red) :
                                new SolidColorBrush(Windows.UI.Colors.LightGray);
                            incomeInvalidText.Visibility = (selectedItem.Name == "income" && incomeInvalid) ? Visibility.Visible : Visibility.Collapsed;
                        }
                        break;
                    case "deductions":
                        {
                            deductionInvalid = ((string.IsNullOrEmpty(selectedItem.Text) || Convert.ToDouble(selectedItem.Text) < 0)) ? true : false;
                            selectedItem.BorderBrush = deductionInvalid ? new SolidColorBrush(Windows.UI.Colors.Red) :
                                new SolidColorBrush(Windows.UI.Colors.LightGray);
                            deductionsInvalidText.Visibility = (selectedItem.Name == "deductions" && deductionInvalid) ? Visibility.Visible : Visibility.Collapsed;
                        }
                        break;
                    case "hours":
                         {
                             hoursInvalid = ((string.IsNullOrEmpty(selectedItem.Text) || Convert.ToDouble(selectedItem.Text) < 0)) ? true : false;
                            selectedItem.BorderBrush = hoursInvalid ? new SolidColorBrush(Windows.UI.Colors.Red) :
                                new SolidColorBrush(Windows.UI.Colors.LightGray);
                            hoursInvalidText.Visibility = (selectedItem.Name == "hours" && hoursInvalid) ? Visibility.Visible : Visibility.Collapsed;
                        }
                        break;
                 }
            }
            if (!ageInvalid && !hoursInvalid && !incomeInvalid && !deductionInvalid)
            {
                age.BorderBrush = income.BorderBrush = deductions.BorderBrush = hours.BorderBrush = new SolidColorBrush(Windows.UI.Colors.LightGray);
                ageInvalidText.Visibility = incomeInvalidText.Visibility = deductionsInvalidText.Visibility = hoursInvalidText.Visibility = Visibility.Collapsed;
                this.PredictButton.IsEnabled = true;
            }
        }
        #endregion

        #region View Unload event
        private void AuditDemo_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Dispose();
        }
        #endregion

        #region Dispose method
        public void Dispose()
        {
            this.Resources.Clear();
            AuditDemoViewModel viewModel = this.DataContext as AuditDemoViewModel;
            viewModel.Dispose();
            if(this.OccupationComboBox != null)
                this.OccupationComboBox.SelectedItem = null;
            if (this.EmploymentComboBox != null)
                this.EmploymentComboBox.SelectedItem = null;
            if (this.EducationComboBox != null)
                this.EducationComboBox.SelectedItem = null;
            if (this.MaritalComboBox != null)
                this.MaritalComboBox.SelectedItem = null;
            if (this.GenderComboBox != null)
                this.GenderComboBox.SelectedItem = null;
            if (this.AccountsComboBox != null)
                this.AccountsComboBox.SelectedItem = null;
            viewModel = null;
        }
        #endregion
    }
}
