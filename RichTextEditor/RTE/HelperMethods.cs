using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Syncfusion.SampleBrowser.UWP.RichTextEditor
{
    internal static class HelperMethods
    {
        /// <summary>
        /// Dispose the Button properties.
        /// </summary>
        /// <param name="button">the button</param>
        internal static void DisposeButton(Button button)
        {
            button.ClearValue(Button.BackgroundProperty);
            button.ClearValue(Button.BorderBrushProperty);
            button.ClearValue(Button.BorderThicknessProperty);
            button.ClearValue(Button.CommandParameterProperty);
            button.ClearValue(Button.CommandProperty);
            button.ClearValue(Button.ContentProperty);
            button.ClearValue(Button.FontFamilyProperty);
            button.ClearValue(Button.FontSizeProperty);
            button.ClearValue(Button.FontStretchProperty);
            button.ClearValue(Button.FontStyleProperty);
            button.ClearValue(Button.FontWeightProperty);
            button.ClearValue(Button.ForegroundProperty);
            button.ClearValue(Button.HeightProperty);
            button.ClearValue(Button.HorizontalAlignmentProperty);
            button.ClearValue(Button.HorizontalContentAlignmentProperty);
            button.ClearValue(Button.IsEnabledProperty);
            button.ClearValue(Button.IsTabStopProperty);
            button.ClearValue(Button.IsTapEnabledProperty);
            button.ClearValue(Button.LanguageProperty);
            button.ClearValue(Button.MarginProperty);
            button.ClearValue(Button.MaxHeightProperty);
            button.ClearValue(Button.MaxWidthProperty);
            button.ClearValue(Button.MinHeightProperty);
            button.ClearValue(Button.MinWidthProperty);
            button.ClearValue(Button.NameProperty);
            button.ClearValue(Button.OpacityProperty);
            button.ClearValue(Button.PaddingProperty);
            button.ClearValue(Button.StyleProperty);
            button.ClearValue(Button.VerticalAlignmentProperty);
            button.ClearValue(Button.VerticalContentAlignmentProperty);
            button.ClearValue(Button.VisibilityProperty);
            button.ClearValue(Button.WidthProperty);
        }
    }
}
