using Syncfusion.PMML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace SampleBrowser.UWP.PredictiveAnalytics
{
    public class AuditDemoViewModel
    {
        #region Properties

        private List<string> genderCollection = new List<string>();

        public List<string> GenderCollection
        {
            get
            {
                return genderCollection;
            }
            set
            {
                genderCollection = value;
                OnPropertyChanged("GenderCollection");
            }
        }

        private List<string> employmentCollection = new List<string>();

        public List<string> EmploymentCollection
        {
            get
            {
                return employmentCollection;
            }
            set
            {
                employmentCollection = value;
                OnPropertyChanged("EmploymentCollection");
            }
        }

        private List<string> educationCollection = new List<string>();

        public List<string> EducationCollection
        {
            get
            {
                return educationCollection;
            }
            set
            {
                educationCollection = value;
                OnPropertyChanged("EducationCollection");
            }
        }

        private List<string> occupationCollection = new List<string>();

        public List<string> OccupationCollection
        {
            get
            {
                return occupationCollection;
            }
            set
            {
                occupationCollection = value;
                OnPropertyChanged("OccupationCollection");
            }
        }

        private List<string> maritalCollection = new List<string>();

        public List<string> MaritalCollection
        {
            get
            {
                return maritalCollection;
            }
            set
            {
                maritalCollection = value;
                OnPropertyChanged("MaritalCollection");
            }
        }

        private List<string> accountsCollection = new List<string>();

        public List<string> AccountsCollection
        {
            get
            {
                return accountsCollection;
            }
            set
            {
                accountsCollection = value;
                OnPropertyChanged("AccountsCollection");
            }
        }

        private string genderSelectedValue = "Female";

        public string GenderSelectedValue
        {
            get
            {
                return genderSelectedValue;
            }
            set
            {
                genderSelectedValue = value;
                OnPropertyChanged("GenderSelectedValue");
            }
        }
        
        private string employmentSelectedValue="Private";

        public string EmploymentSelectedValue
        {
            get
            {
                return employmentSelectedValue;
            }
            set
            {
                employmentSelectedValue = value;
                OnPropertyChanged("EmploymentSelectedValue");
            }
        }

        private string educationSelectedValue ="College";

        public string EducationSelectedValue
        {
            get
            {
                return educationSelectedValue;
            }
            set
            {
                educationSelectedValue = value;
                OnPropertyChanged("EducationSelectedValue");
            }
        }

        private string occupationSelectedValue="Cleaner";

        public string OccupationSelectedValue
        {
            get
            {
                return occupationSelectedValue;
            }
            set
            {
                occupationSelectedValue = value;
                OnPropertyChanged("OccupationSelectedValue");
            }
        }

        private string maritalSelectedValue="Married";

        public string MaritalSelectedValue
        {
            get
            {
                return maritalSelectedValue;
            }
            set
            {
                maritalSelectedValue = value;
                OnPropertyChanged("MaritalSelectedValue");
            }
        }

        private string accountsSelectedValue="Canada";

        public string AccountsSelectedValue
        {
            get
            {
                return accountsSelectedValue;
            }
            set
            {
                accountsSelectedValue = value;
                OnPropertyChanged("AccountsSelectedValue");
            }
        }

        private int ageTextValue = 20;

        public int AgeTextValue
        {
            get
            {
                return ageTextValue;
            }
            set
            {
                ageTextValue = value;
                OnPropertyChanged("AgeTextValue");
            }
        }

        private int incomeTextValue = 20000;

        public int IncomeTextValue
        {
            get
            {
                return incomeTextValue;
            }
            set
            {
                incomeTextValue = value;
                OnPropertyChanged("IncomeTextValue");
            }
        }

        private int deductionTextValue = 10000;

        public int DeductionTextValue
        {
            get
            {
                return deductionTextValue;
            }
            set
            {
                deductionTextValue = value;
                OnPropertyChanged("DeductionTextValue");
            }
        }

        private int hoursTextValue = 12;

        public int HoursTextValue
        {
            get
            {
                return hoursTextValue;
            }
            set
            {
                hoursTextValue = value;
                OnPropertyChanged("HoursTextValue");
            }
        }

        private string auditPredicted;

        public string AuditPredicted
        {
            get
            {
                return auditPredicted;
            }
            set
            {
                auditPredicted = value;
                OnPropertyChanged("AuditPredicted");
            }
        }

        private double auditProbability0;

        public double AuditProbability0
        {
            get
            {
                return auditProbability0;
            }
            set
            {
                auditProbability0 = value;
                OnPropertyChanged("AuditProbability0");
            }
        }

        private double auditProbability1;

        public double AuditProbability1
        {
            get
            {
                return auditProbability1;
            }
            set
            {
                auditProbability1 = value;
                OnPropertyChanged("AuditProbability1");
            }
        }

        private string predictedText;

        public string PredictedText
        {
            get
            {
                return predictedText;
            }
            set
            {
                predictedText = value;
                OnPropertyChanged("PredictedText");
            }
        }

        private ImageSource imagePath;

        public ImageSource ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }


        private ImageSource iconImagePath;

        public ImageSource IconImagePath
        {
            get
            {
                return iconImagePath;
            }
            set
            {
                iconImagePath = value;
                OnPropertyChanged("IconImagePath");
            }
        }

        #endregion Properties      

        #region Constructor

        public AuditDemoViewModel()
        {
            GetComboBoxCollection();
            var Iconpath = Path.Combine("Images\\Icon.ico");
            var IPath = Path.Combine(Package.Current.InstalledLocation.Path, Iconpath);
            Image Ipathimg = new Image();
            Ipathimg.Source = new BitmapImage(new Uri(IPath));
            IconImagePath = Ipathimg.Source;
          
        }

        #endregion Constructor

        #region Methods

        public string PredictMethod()
        {
            //image path
            string imagePath = "ms-appx:///PredictiveAnalytics/ShowCase/AuditDemo/Images";
            
            //Get PMML Evaluator instance
            PMMLEvaluator evaluator = new PMMLEvaluatorFactory().
                 GetPMMLEvaluatorInstance(new ViewModel().
                 GetPMMLPath("ms-appx:///PredictiveAnalytics/ShowCase/AuditDemo/Data/Audit.pmml"));            

            //Create and anonymous type for audit record
            var audit = new
            {
                ID = 0,
                Age = AgeTextValue,
                Employment = EmploymentSelectedValue,
                Education = EducationSelectedValue,
                Marital = MaritalSelectedValue,
                Occupation = OccupationSelectedValue,
                Income = IncomeTextValue,
                Sex = GenderSelectedValue,
                Deductions = DeductionTextValue,
                Hours = HoursTextValue,
                Accounts = AccountsSelectedValue,
                Adjustment = 0
            };

            //Get predicted result
            PredictedResult predictedResult = evaluator.GetResult(audit, null);

            //Get predicted category 0 or 1
            string predicted = (predictedResult.PredictedValue != null) ? predictedResult.PredictedValue.ToString() : "-";
                                                
            if (predicted.Equals("0"))
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(imagePath + "/thumb_yes.png"));
                ImagePath = img.Source;
            }
            else 
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(imagePath + "/thumb_no.png"));
                ImagePath = img.Source;
            }
            
            AuditPredicted = predicted == "0" ? "YES!" : "NO!";
            PredictedText = predicted == "0" ? "Your audit risk is low." : "Your audit risk is high.";

            return PredictedText;
        }

        private void GetComboBoxCollection()
        {
            GenderCollection.Add("Male");
            GenderCollection.Add("Female");

            EmploymentCollection.Add("Consultant");
            EmploymentCollection.Add("Private");
            EmploymentCollection.Add("PSFederal");
            EmploymentCollection.Add("PSLocal");
            EmploymentCollection.Add("PSState");
            EmploymentCollection.Add("SelfEmp");
            EmploymentCollection.Add("Volunteer");

            EducationCollection.Add("Associate");
            EducationCollection.Add("Bachelor");
            EducationCollection.Add("College");
            EducationCollection.Add("Doctorate");
            EducationCollection.Add("HSgrad");
            EducationCollection.Add("Master");
            EducationCollection.Add("Preschool");
            EducationCollection.Add("Professional");
            EducationCollection.Add("Vocational");
            EducationCollection.Add("Yr9");
            EducationCollection.Add("Yr10");
            EducationCollection.Add("Yr11");
            EducationCollection.Add("Yr12");
            EducationCollection.Add("Yr1t4");
            EducationCollection.Add("Yr5t6");
            EducationCollection.Add("Yr7t8");

            MaritalCollection.Add("Absent");
            MaritalCollection.Add("Divorced");
            MaritalCollection.Add("Married");
            MaritalCollection.Add("Married-spouse-absent");
            MaritalCollection.Add("Unmarried");
            MaritalCollection.Add("Widowed");

            OccupationCollection.Add("Cleaner");
            OccupationCollection.Add("Clerical");
            OccupationCollection.Add("Executive");
            OccupationCollection.Add("Farming");
            OccupationCollection.Add("Home");
            OccupationCollection.Add("Machinist");
            OccupationCollection.Add("Military");
            OccupationCollection.Add("Professional");
            OccupationCollection.Add("Protective");
            OccupationCollection.Add("Repair");
            OccupationCollection.Add("Sales");
            OccupationCollection.Add("Service");
            OccupationCollection.Add("Support");
            OccupationCollection.Add("Transport");

            AccountsCollection.Add("Canada");
            AccountsCollection.Add("China");
            AccountsCollection.Add("Columbia");
            AccountsCollection.Add("Cuba");
            AccountsCollection.Add("Ecuador");
            AccountsCollection.Add("England");
            AccountsCollection.Add("Fiji");
            AccountsCollection.Add("Germany");
            AccountsCollection.Add("Greece");
            AccountsCollection.Add("Guatemala");
            AccountsCollection.Add("Hong");
            AccountsCollection.Add("Hungary");
            AccountsCollection.Add("India");
            AccountsCollection.Add("Indonesia");
            AccountsCollection.Add("Iran");
            AccountsCollection.Add("Ireland");
            AccountsCollection.Add("Italy");
            AccountsCollection.Add("Jamaica");
            AccountsCollection.Add("Japan");
            AccountsCollection.Add("Malaysia");
            AccountsCollection.Add("Mexico");
            AccountsCollection.Add("NewZealand");
            AccountsCollection.Add("Nicaragua");
            AccountsCollection.Add("Philippines");
            AccountsCollection.Add("Poland");
            AccountsCollection.Add("Portugal");
            AccountsCollection.Add("Scotland");
            AccountsCollection.Add("Singapore");
            AccountsCollection.Add("Taiwan");
            AccountsCollection.Add("UnitedStates");
            AccountsCollection.Add("Vietnam");
            AccountsCollection.Add("Yugoslavia");
        }
       

        #endregion Methods

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);

                handler(this, e);
            }
        }
        #endregion INotifyPropertyChanged  

        //Dispose method
        public void Dispose()
        {
            if (GenderCollection != null)
                GenderCollection.Clear();
            if (EmploymentCollection != null)
                EmploymentCollection.Clear();
            if (EducationCollection != null)
                EducationCollection.Clear();
            if (MaritalCollection != null)
                MaritalCollection.Clear();
            if (OccupationCollection != null)
                OccupationCollection.Clear();
            if (AccountsCollection != null)
                AccountsCollection.Clear();
        }
    }
}
        

 