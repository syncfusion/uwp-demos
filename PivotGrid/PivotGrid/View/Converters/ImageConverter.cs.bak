#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace BI.PivotGrid.Tutorials.PivotGridSamples.Converters
{
    using Syncfusion.Olap.UWP.Engine;
    using System;
    using Windows.UI.Xaml.Data;
    /// <summary>
    /// Defines the image converter which is used to convert country flags used in the application.
    /// </summary>
    public class ImageConverter : IValueConverter
    {
        #region IValueConverter Members
        /// <summary>
        /// Converts the given input into object
        /// </summary>
        /// <param name="value">object</param>
        /// <param name="targetType">Type</param>
        /// <param name="parameter">object</param>
        /// <param name="culture">culture</param>
        /// <returns>object</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string imageSrc = "";
            PivotCellDescriptor cellInfo = value as PivotCellDescriptor;
            if (cellInfo != null)
            {
                switch (cellInfo.CellValue)
                {
                    case "Australia":
                        imageSrc = "ms-appx:///PivotGrid/View/Assets/Images/CountryFlags/Australia.png";
                        return imageSrc;
                    case "Canada":
                        imageSrc = "ms-appx:///PivotGrid/View/Assets/Images/CountryFlags/Canada.png";
                        return imageSrc;
                    case "France":
                        imageSrc = "ms-appx:///PivotGrid/View/Assets/Images/CountryFlags/France.png";
                        return imageSrc;
                    case "Germany":
                        imageSrc = "ms-appx:///PivotGrid/View/Assets/Images/CountryFlags/Germany.png";
                        return imageSrc;
                    case "United Kingdom":
                        imageSrc = "ms-appx:///PivotGrid/View/Assets/Images/CountryFlags/UK.png";
                        return imageSrc;
                    case "United States":
                        imageSrc = "ms-appx:///PivotGrid/View/Assets/Images/CountryFlags/USA.png";
                        return imageSrc;
                }
            }

            return imageSrc;
        }
        /// <summary>
        /// Converts the given input into object
        /// </summary>
        /// <param name="value">object</param>
        /// <param name="targetType">Type</param>
        /// <param name="parameter">object</param>
        /// <param name="language">culture</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
