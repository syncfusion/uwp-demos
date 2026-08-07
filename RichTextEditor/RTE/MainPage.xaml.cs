using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SampleBrowser;
using Windows.Phone.UI.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RTEDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Properties
        /// <summary>
        /// Gets or sets the demo files.
        /// </summary>
        /// <value>
        /// The demo files.
        /// </value>
        public List<DemoFile> DemoFiles
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            this.LoadDemoFiles();
            this.DataContext = this;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            Unloaded += MainPage_Unloaded;
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationService service = new NavigationService(this.Frame);
        }
        /// <summary>
        /// Invoked immediately after the Page is unloaded and is no longer the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the navigation that has unloaded the current Page.</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the ItemClick event of the gridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
        void gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            int i = (int)(((DemoFile)e.ClickedItem).Index);
            Type type;
            switch (i)
            {
                case 2:
                    type = typeof(DocumentEditor);
                    break;
                case 3:
                    type = typeof(Forum);
                    break;
                default:
                    type = typeof(CustomizedEditor);
                    break;
            }
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                string frameType = type.ToString();
                var assemblyinfo = this.GetType().AssemblyQualifiedName.Split(',');
                if (assemblyinfo.Count() > 1 && assemblyinfo[1] != null)
                    type = Type.GetType(type.ToString() + ", " + assemblyinfo[1] + ", " + "Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            }
            NavigationService.Navigate(type, ((DemoFile)e.ClickedItem).Index);
        }
        /// <summary>
        /// Called when hardware buttons back pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            NavigationService.GoBack();
        }
        /// <summary>
        /// Called when main page unloaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Unloaded -= MainPage_Unloaded;
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Loads the demo files.
        /// </summary>
        void LoadDemoFiles()
        {
            if (DemoFiles == null)
                this.DemoFiles = new List<DemoFile>();
            this.DemoFiles.Add(new DemoFile() { Index = 2, DemoName = "Document Editor", ImagePath = "ms-appx:///RTE/Assets/DocumentEditor.png" });
            this.DemoFiles.Add(new DemoFile() { Index = 1, DemoName = "Customized Editor", ImagePath = "ms-appx:///RTE/Assets/CustomizedEditor.png" });
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                this.DemoFiles.Add(new DemoFile() { Index = 3, DemoName = "Forum", ImagePath = "ms-appx:///RTE/Assets/Forum.png" });
        }
        #endregion
    }

    /// <summary>
    /// Represents the DemoFile class.
    /// </summary>
    public class DemoFile
    {
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get; set; }
        /// <summary>
        /// Gets or sets the name of the demo.
        /// </summary>
        /// <value>
        /// The name of the demo.
        /// </value>
        public string DemoName { get; set; }
        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        /// <value>
        /// The image path.
        /// </value>
        public string ImagePath { get; set; }
        /// <summary>
        /// Gets or sets the new tag path.
        /// </summary>
        /// <value>
        /// The new tag path.
        /// </value>
        public string NewTagPath { get; set; }
    }
}
