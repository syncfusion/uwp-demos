using Common;
using Syncfusion.UI.Xaml.Controls.Notification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Notification.BusyIndicator
{
    public sealed partial class BusyIndicatorView : SampleLayout
    {
        public BusyIndicatorView()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public static bool IsMobileFamily()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                return true;
            }
            return false;
        }
        private AnimationTypes animation;

        public AnimationTypes Animation
        {
            get { return animation; }
            set { animation = value; }
        }

       
        public override void Dispose()
        {
            if (busy != null)
            {
                busy.Dispose();
                busy = null;
            }
            
            this.DataContext = null;
        }


        private void discard_Click(object sender, RoutedEventArgs e)
        {
            this.controlView.Visibility = Visibility.Visible;
          
        }
    }
}
