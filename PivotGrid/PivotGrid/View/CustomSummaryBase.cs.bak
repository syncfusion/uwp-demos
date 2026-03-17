#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace BI.PivotGrid
{
    using Syncfusion.PivotAnalysis.UWP;

    /// <summary>
    /// This class that defines the necessary functionality to do PivotCalculations.
    /// It derives from <see cref="SummaryBase">SumamryBase</see>/>
    /// </summary>
    public class MyCustomSummaryBase1 : SummaryBase
    {
        #region Private Fields

        /// <summary>
        /// Gets or sets the summary value.
        /// </summary>
        private double mTotalValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the new instance of <see cref="MyCustomSummaryBase1">MyCustomSummaryBase1.</see>/>
        /// </summary>
        public MyCustomSummaryBase1()
        {

        }

        #endregion

        #region Overridden Methods

        /// <summary>
        /// This method is used to combine the objects.
        /// </summary>
        /// <param name="other">The object</param>
        public override void Combine(object other)
        {
            mTotalValue += (double)other;
        }

        /// <summary>
        /// This method is used to combine the summary values of caluculation item.
        /// </summary>
        /// <param name="other">The object.</param>
        public override void CombineSummary(SummaryBase other)
        {
            MyCustomSummaryBase1 dpsb = other as MyCustomSummaryBase1;

            if (null != dpsb)
            {
                mTotalValue += dpsb.mTotalValue;
            }
        }

        /// <summary>
        /// Returns the new instance of <see cref="MyCustomSummaryBase1">MyCustomSummaryBase.</see>/>
        /// </summary>
        /// <returns></returns>
        public override SummaryBase GetInstance()
        {
            return new MyCustomSummaryBase1();
        }

        /// <summary>
        /// Return the result value for given calculation item.
        /// </summary>
        /// <returns></returns>
        public override object GetResult()
        {
            return mTotalValue / 3.33333;
        }

        /// <summary>
        /// Resets the calculated value.
        /// </summary>
        public override void Reset()
        {
            mTotalValue = 0;
        }

        #endregion
    }

    /// <summary>
    /// This class that defines the necessary functionality to do PivotCalculations.
    /// It derives from <see cref="SummaryBase">SumamryBase</see>/>
    /// </summary>
    public class MyCustomSummaryBase2 : SummaryBase
    {
        #region Private Fields

        /// <summary>
        /// Gets or sets the summary value.
        /// </summary>
        private double mTotalValue;

        #endregion

        #region Constructor

        /// <summary>
        /// This method is used to combine the objects.
        /// </summary>
        /// <param name="other">The object</param>
        public override void Combine(object other)
        {
            mTotalValue += (double)other;
        }

        #endregion

        #region Overridden Methods

        /// <summary>
        /// This method is used to combine the summary values of caluculation item.
        /// </summary>
        /// <param name="other">The object.</param>
        public override void CombineSummary(SummaryBase other)
        {
            MyCustomSummaryBase2 dpsb = other as MyCustomSummaryBase2;

            if (null != dpsb)
            {
                mTotalValue += dpsb.mTotalValue;
            }
        }

        /// <summary>
        /// Returns the new instance of <see cref="MyCustomSummaryBase2">MyCustomSummaryBase.</see>/>
        /// </summary>
        /// <returns></returns>
        public override SummaryBase GetInstance()
        {
            return new MyCustomSummaryBase2();
        }

        /// <summary>
        /// Return the result value for given calculation item.
        /// </summary>
        /// <returns></returns>
        public override object GetResult()
        {
            return mTotalValue / 5.5555;
        }

        /// <summary>
        /// Resets the calculated value.
        /// </summary>
        public override void Reset()
        {
            mTotalValue = 0;
        }

        #endregion
    }
}
