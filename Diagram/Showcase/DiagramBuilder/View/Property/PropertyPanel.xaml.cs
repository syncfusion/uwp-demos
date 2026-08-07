using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DiagramBuilder.ViewModel;
using Windows.UI.Xaml.Media.Animation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DiagramBuilder.View
{
    public sealed partial class PropertyPanel : UserControl
    {
        public PropertyPanel()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
         private void path1_Click_1(object sender, RoutedEventArgs e)
        {                      

          if((((this.DataContext) as DiagramBuilderVM).EditorPickerVisibility)==Visibility.Visible)
            {
                DoubleAnimationUsingKeyFrames animate = new DoubleAnimationUsingKeyFrames();
                EasingDoubleKeyFrame easing = new EasingDoubleKeyFrame();
                EasingDoubleKeyFrame easing1 = new EasingDoubleKeyFrame();
                Storyboard story = new Storyboard();
                easing.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
                easing.Value = 0;
                animate.KeyFrames.Add(easing);
                easing1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3));
                easing1.Value = 180;
                animate.KeyFrames.Add(easing1);
                story.Children.Add(animate);              
                Storyboard.SetTarget(animate,(sender as Button));
                Storyboard.SetTargetProperty(animate, ("(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)"));
                story.Begin();
            }
            else
            {
                DoubleAnimationUsingKeyFrames animate = new DoubleAnimationUsingKeyFrames();
                EasingDoubleKeyFrame easing = new EasingDoubleKeyFrame();
                EasingDoubleKeyFrame easing1 = new EasingDoubleKeyFrame();
                Storyboard story = new Storyboard();
                easing.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
                easing.Value = 0;
                animate.KeyFrames.Add(easing);
                easing1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3));
                easing1.Value = 0;
                animate.KeyFrames.Add(easing1);
                story.Children.Add(animate);                
                Storyboard.SetTarget(animate, (sender as Button));
                Storyboard.SetTargetProperty(animate, ("(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)"));
                story.Begin();
            }
        } 
    }

    public class PanelControl : ContentControl
    {
        public PanelControl()
        {
            DefaultStyleKey = typeof(PanelControl);
        }

        public Visibility PanelVisibility
        {
            get { return (Visibility)GetValue(PanelVisibilityProperty); }
            set { SetValue(PanelVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PanelVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PanelVisibilityProperty =
            DependencyProperty.Register("PanelVisibility", typeof(Visibility), typeof(PanelControl), new PropertyMetadata(Visibility.Visible,OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PanelControl p = d as PanelControl;
            if (p.PanelVisibility == Visibility.Visible)
            {
                p.Visibility = Visibility.Visible;
                p.GoToState(true, "Expanded");
            }
            else
            {
                p.Visibility = Visibility.Collapsed;
                p.GoToState(true, "Collapsed");
            }
        }
        void GoToState(bool useTransitions, string name)
        {
            VisualStateManager.GoToState(this, name, useTransitions);
        }
    }
}
