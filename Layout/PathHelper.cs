using System;
using System.Reflection;
using Windows.UI.Xaml;

namespace Layout
{
    /// <summary>
    /// Utility class for centralizing resource path resolution logic.
    /// Handles assembly-based path construction for loading resources from correct locations.
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Gets the resolved resource path based on the current assembly context.
        /// 
        /// When running in Sample Browser (main app), paths include assembly name prefix.
        /// When running in separate assembly context, paths use standard ms-appx format.
        /// </summary>
        /// <param name="relativePath">Relative resource path (e.g., "TileView/Assets/Emp_02.png")</param>
        /// <returns>Full ms-appx URI path</returns>
        public static string GetResourcePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                throw new ArgumentNullException(nameof(relativePath));

            // Remove leading slashes if present
            relativePath = relativePath.TrimStart('/');

            // Check if running in Sample Browser main application
            string assemblyName = (Application.Current.GetType().GetTypeInfo().Assembly as Assembly)?.GetName().Name ?? string.Empty;

            if (assemblyName == "Syncfusion.SampleBrowser.UWP")
            {
                // Running in main Sample Browser app - include assembly name in path
                return $"ms-appx:///Syncfusion.SampleBrowser.UWP.Layout/{relativePath}";
            }
            else
            {
                // Running in separate assembly - use standard path format
                return $"ms-appx:///{relativePath}";
            }
        }
    }
}
