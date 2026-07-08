using System;
using System.Reflection;
using Windows.UI.Xaml;

namespace Syncfusion.SampleBrowser.UWP.PivotClient
{
    /// <summary>
    /// Utility class for centralizing resource path resolution logic.
    /// Handles assembly-based path construction for loading resources from correct locations.
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Gets the resolved resource path based on the current assembly context.
        /// </summary>
        /// <param name="relativePath">Relative resource path</param>
        /// <returns>Full ms-appx URI path</returns>
        public static string GetResourcePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                throw new ArgumentNullException(nameof(relativePath));

            relativePath = relativePath.TrimStart('/');

            string assemblyName = (Application.Current.GetType().GetTypeInfo().Assembly as Assembly)?.GetName().Name ?? string.Empty;

            if (assemblyName == "Syncfusion.SampleBrowser.UWP")
            {
                return $"ms-appx:///Syncfusion.SampleBrowser.UWP.SfPivotClient/{relativePath}";
            }
            else
            {
                return $"ms-appx:///{relativePath}";
            }
        }
    }
}
