using System;
using System.Reflection;
using Windows.UI.Xaml;

namespace Common
{
    public static class ResourcePathHelper
    {
        public static string GetResourcePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                throw new ArgumentNullException(nameof(relativePath));

            // Check if running in Sample Browser main application
            string assemblyName = (Application.Current.GetType().GetTypeInfo().Assembly as Assembly)?.GetName().Name ?? string.Empty;

            if (assemblyName == "Syncfusion.SampleBrowser.UWP")
            {
                // Running in main Sample Browser app - include assembly name in path
                return $"ms-appx:///Syncfusion.SampleBrowser.UWP.Editors/{relativePath}";
            }
            else
            {
                // Running in separate assembly - use standard path format
                return $"ms-appx:///{relativePath}";
            }
        }
    }
}
