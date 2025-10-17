#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Common
{
    public partial class SampleView : UserControl
    {
        public SampleView()
        {
            this.InitializeComponent();
            CloseOptionsButton.Click += CloseOptionsButton_Click;          
            this.Unloaded += SampleView_Unloaded;
        }

        void SampleView_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded-=SampleView_Unloaded;
            if(sender is IDisposable)
            {
                (sender as IDisposable).Dispose();
            }
        }

        bool isSettingsPanelHide;

        void CloseOptionsButton_Click(object sender, RoutedEventArgs e)
        {
           IsSettingsOpen = false;
        }

        public object SampleContent
        {
            get { return (object)GetValue(SampleContentProperty); }
            set { SetValue(SampleContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SampleContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SampleContentProperty =
            DependencyProperty.Register("SampleContent", typeof(object), typeof(SampleView), new PropertyMetadata(null,new PropertyChangedCallback(OnSampleContentChanged)));


        private SampleLayout sampleLayout;

        public SampleLayout SampleLayout
        {
            get { return sampleLayout; }
            set { sampleLayout = value; }
        }
        

        public object SettingsContent
        {
            get { return (object)GetValue(SettingsContentProperty); }
            set { SetValue(SettingsContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SettingsContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingsContentProperty =
            DependencyProperty.Register("SettingsContent", typeof(object), typeof(SampleView), new PropertyMetadata(null));



        public bool IsSettingsOpen
        {
            get { return (bool)GetValue(IsSettingsOpenProperty); }
            set { SetValue(IsSettingsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSettingsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSettingsOpenProperty =
            DependencyProperty.Register("IsSettingsOpen", typeof(bool), typeof(SampleView), new PropertyMetadata(false,new PropertyChangedCallback(OnIsSettingsOpenChanged)));

        static void OnSampleContentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            (sender as SampleView).IsSettingsOpen = false;
        }
        static void OnIsSettingsOpenChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            (sender as SampleView).OnIsSettinsOpenChanged(args);
        }

        public void HideSettingsPanel()
        {
            SampleLayout.RenderTransform = new TranslateTransform() { X = 0 };
            isSettingsPanelHide = true;
            IsSettingsOpen = false;
            isSettingsPanelHide = false;
        }


        private void OnIsSettinsOpenChanged(DependencyPropertyChangedEventArgs args)
        {
           if(SampleLayout != null && SettingsContent != null && !isSettingsPanelHide)
            {
                SampleLayout.RenderTransform = new CompositeTransform();
                if (IsSettingsOpen)
                {
                    SettingsIn.Stop();
                    Storyboard.SetTarget(SettingsIn, SampleLayout);
                    SettingsIn.Begin();
                }
                else
                {
                    SettingsOut.Stop();
                    Storyboard.SetTarget(SettingsOut, SampleLayout);
                    SettingsOut.Begin();
                }
                    
            }         
        }   
        
    }
}
