using System;
using System.Reflection;
using Windows.UI.Xaml;

namespace Syncfusion.SampleBrowser.UWP.RichTextEditor
{
    /// <summary>
    /// Utility class for centralizing resource path resolution logic.
    /// Handles assembly-based path construction for loading resources from correct locations.
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Resolves and returns the appropriate ms-appx URI path for a given relative resource path.
        /// 
        /// When running in Sample Browser (main app), paths include assembly name prefix.
        /// When running in separate assembly context, paths use standard ms-appx format.
        /// 
        /// It also ensures the input path is normalized by removing any leading slashes.
        /// </summary>
        /// <param name="relativePath">
        /// Relative resource path (e.g., "RTE/Assets/Images/sample.png")
        /// </param>
        /// <returns>
        /// Fully qualified ms-appx URI string suitable for resource loading.
        /// </returns>
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
                return $"ms-appx:///Syncfusion.SampleBrowser.UWP.RichTextEditor/{relativePath}";
            }
            else
            {
                // Running in separate assembly - use standard path format
                return $"ms-appx:///{relativePath}";
            }
        }
    }
}