using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.UWP.PredictiveAnalytics
{
    public class RecommendedGroceries
    {
        public string Item
        {
            get; set;
        }
        public decimal Confidence
        {
            get; set;
        }
    }

    public class Titanic
    {
        public string Status
        {
            get; set;
        }
        public double Died_probability
        {
            get; set;
        }
        public double Survived_probability
        {
            get; set;
        }
    }


    public class Iris
    {
        public string Species
        {
            get; set;
        }
        public double Setosa_probability
        {
            get; set;
        }
        public double Versicolor_probability
        {
            get; set;
        }
        public double Virginica_probability
        {
            get; set;
        }
    }

    public class BreastCancer
    {
        public string Status
        {
            get; set;
        }
        public double Censored_probability
        {
            get; set;
        }
        public double Event_probability
        {
            get; set;
        }
    }

    public class Audit
    {
        public string Status
        {
            get; set;
        }
        public double Adjustable_probability
        {
            get; set;
        }
        public double NonAdjustable_probability
        {
            get; set;
        }
    }

    public class Wine
    {
        public string Type
        {
            get; set;
        }
        public double Wine1_probability
        {
            get; set;
        }
        public double Wine2_probability
        {
            get; set;
        }
        public double Wine3_probability
        {
            get; set;
        }
    }

    public class Glass
    {
        public string Field
        {
            get; set;
        }
        public string Input_Value
        {
            get; set;
        }

        public string Centroid_Value
        {
            get; set;
        }
    }

    public class Bfeed
    {
        public string Observation
        {
            get; set;
        }
        public double Predicted_Survival
        {
            get; set;
        }
    }

    public class ClusterDetails
    {
        public string ID
        {
            get;
            set;
        }
        public int Size
        {
            get;
            set;
        }
    }
}
