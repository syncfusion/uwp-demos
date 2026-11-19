namespace MapControlUWP_Samples
{
    using Syncfusion.UI.Xaml.Maps;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;
    public class DoubleConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return double.Parse(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
    public class SelectedIndexToUri : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var uri = value.ToString();
            int index = 0;
            switch (uri)
            {
                case "SampleBrowser.Assets.ShapeFiles.states.shp":
                    index = 0;
                    break;
                case "SampleBrowser.Assets.ShapeFiles.Argentina.shp":
                    index = 1;
                    break;
                case "SampleBrowser.Assets.ShapeFiles.Brazil.shp":
                    index = 2;
                    break;
                case "SampleBrowser.Assets.ShapeFiles.WorldMap.shp":
                    index = 3;
                    break;
            }
            return index;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var index = (int)value;
            var uri = "SampleBrowser.Assets.ShapeFiles.states.shp";
            switch (index)
            {
                case 0:
                    uri = "SampleBrowser.Assets.ShapeFiles.states.shp";
                    break;
                case 1:
                    uri = "SampleBrowser.Assets.ShapeFiles.Argentina.shp";
                    break;
                case 2:
                    uri = "SampleBrowser.Assets.ShapeFiles.Brazil.shp";
                    break;
                case 3:
                    uri = "SampleBrowser.Assets.ShapeFiles.WorldMap.shp";
                    break;
            }
            return uri;
        }
    }
    public class SelectedIndexToBrushConvertor : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var index = (int)value;
            var color = new SolidColorBrush(Colors.Black);
            switch (index)
            {
                case 0:
                    color = new SolidColorBrush(Colors.White);
                    break;
                case 1:
                    color = new SolidColorBrush(Colors.Black);
                    break;
                case 2:
                    color = new SolidColorBrush(Colors.Cyan);
                    break;
                case 3:
                    color = new SolidColorBrush(Colors.Blue);
                    break;
                case 4:
                    color = new SolidColorBrush(Colors.Red);
                    break;
                case 5:
                    color = new SolidColorBrush(Colors.Transparent);
                    break;
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var color = (value as SolidColorBrush).Color.ToString();
            int index = 0;
            switch (color)
            {

                case "#FFFFFFFF":
                    index = 0;
                    break;
                case "#FF000000":
                    index = 1;
                    break;
                case "#FF00FFFF":
                    index = 2;
                    break;
                case "#FF0000FF":
                    index = 3;
                    break;
                case "#FFFF0000":
                    index = 4;
                    break;
                case "#00FFFFFF":
                    index = 5;
                    break;
            }
            return index;


        }
    }
}
