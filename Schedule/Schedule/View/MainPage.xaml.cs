using Common;
using SampleBrowser;
using SampleBrowser.UWP.Schedule.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleBrowser.UWP.Schedule.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    //public sealed partial class MainPage : SampleLayout
    //{
    //    public MainPage()
    //    {
    //        this.InitializeComponent();
    //        //HardwareButtons.BackPressed += HardwareButtons_BackPressed;
    //    }

    //    //void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
    //    //{
    //    //    ApplicationLanguages.PrimaryLanguageOverride = "en-US";
    //    //}       
    //}

    //public class IndexFile
    //{
    //    public int Index { get; set; }
    //    public string FilePath { get; set; }
    //    public string ImagePath { get; set; }
    //    public string NewTagPath { get; set; }
    //}
    //public class StaticConst
    //{
    //    public static int stage = 0;
    //}

    public sealed partial class MainPage : Page
    {
        #region Public Properties

        public List<IndexFile> FilePath
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public MainPage()
        {
            this.InitializeComponent();
            this.LoadPath();
             this.DataContext = this;
        }

        #endregion

        #region Events

        void grdvItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            StaticConst.stage = 0;
            int i = (int)(((IndexFile)e.ClickedItem).Index);
            switch (i)
            {
                case 1:
                    {
                      //  NavigationService.Navigate(typeof(SampleBrowser.Schedule.View.GettingStarted_WinRT), ((IndexFile)e.ClickedItem).Index);
                        break;
                    }

                case 2:
                    {
                        //NavigationService.Navigate(typeof(RecurrenceAppointment_WinRT), ((IndexFile)e.ClickedItem).Index);
                        break;
                    }

                default:

                    {
                        //NavigationService.Navigate(typeof(CustomizationDemo_WinRT), ((IndexFile)e.ClickedItem).Index);
                        break;
                    }
                
                  

            }
        }

        #endregion

        #region Methods

        void LoadPath()
        {
            if (FilePath == null)
            {
                this.FilePath = new List<IndexFile>();
            }
            this.FilePath.Add(new IndexFile() { Index = 1, FilePath = "GettingStarted", ImagePath = "ms-appx:///Schedule/Assets/GettingStarted.png" });
            this.FilePath.Add(new IndexFile() { Index = 2, FilePath = "RecurrenceAppointment", ImagePath = "ms-appx:///Schedule/Assets/Recurrence.png" });
            this.FilePath.Add(new IndexFile() { Index = 3, FilePath = "CustomizationDemo", ImagePath = "ms-appx:///Schedule/Assets/CustomizationDemo.png" });
            //this.FilePath.Add(new IndexFile() { Index = 3, FilePath = "Localization", ImagePath = "ms-appx:///Schedule/Assets/Localization.png" });
          

        }

        #endregion

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame != null)
            {
                Frame.GoBack();
            }
        }
    }

    public class IndexFile
    {
        public int Index { get; set; }
        public string FilePath { get; set; }
        public string ImagePath { get; set; }
        public string NewTagPath { get; set; }
    }
    public class StaticConst
    {
        public static int stage = 0;
    }
}