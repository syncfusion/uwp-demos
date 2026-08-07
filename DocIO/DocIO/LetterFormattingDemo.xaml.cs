using Common;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialDocIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LetterFormattingDemo : SampleLayout
    {
        #region Fields
        XDocument customerXml;
        XElement element;
        List<Customer> Customers = new List<Customer>();
        Customer customerDetails;
        Assembly execAssm = typeof(LetterFormattingDemo).GetTypeInfo().Assembly;
        #endregion

        public LetterFormattingDemo()
        {
            this.InitializeComponent();
            InitializeCustomer();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                mailMerge.Margin = new Thickness(10, 10, 10, 10);
                stackPnlOptions.Visibility = Visibility.Collapsed;
                list.Orientation = Orientation.Vertical;
                saveas.Visibility = Visibility.Collapsed;
                text1.Visibility = Visibility.Collapsed;
                text2.Visibility = Visibility.Collapsed;
                ForAlignListBox.Visibility = Visibility.Collapsed;
                listBox1.Height = 100;
                WinRTText2.Visibility = Visibility.Collapsed;
            }
            else
            {
                listBox1.BorderThickness = new Thickness(1);
                stackPnlOptions.Visibility = Visibility.Visible;
                saveas.Visibility = Visibility.Visible;
                text1.Visibility = Visibility.Visible;
                text2.Visibility = Visibility.Visible;
                ForAlignListBox.Visibility = Visibility.Visible;
            }
        }
        public override void Dispose()
        {
            base.Dispose();
            
            DisposeTextBlock(TextBlock1);
            TextBlock1 = null;
            DisposeTextBlock(TextBlock2);
            TextBlock2 = null;
            DisposeTextBlock(TextBlock3);
            TextBlock3 = null;
            DisposeTextBlock(ForAlignListBox);
            ForAlignListBox = null;
            DisposeTextBlock(TextBlock5);
            TextBlock5 = null;
            DisposeTextBlock(text3);
            text3 = null;
            DisposeTextBlock(TextBlock7);
            TextBlock7 = null;
            DisposeTextBlock(WinRTText1);
            WinRTText1 = null;
            DisposeTextBlock(WinRTText2);
            WinRTText2 = null;
            DisposeTextBlock(saveas);
            saveas = null;
            DisposeTextBlock(text1);
            text1 = null;
            DisposeTextBlock(text2);
            text2 = null;
            DisposeTextBlock(text4);
            text4 = null;

            Button1.Click -= Button_Click_1;
            DisposeButton(Button1);
            Button1 = null;

            DisposeRadioButton(rdDoc);
            rdDoc = null;
            DisposeRadioButton(rdDOcx);
            rdDOcx = null;

            DisposeListBox(listBox1);
            listBox1 = null;

            stackPnlOptions.ClearValue(StackPanel.OrientationProperty);
            stackPnlOptions.ClearValue(StackPanel.HorizontalAlignmentProperty);
            stackPnlOptions = null;

            mailMerge.ClearValue(Grid.BackgroundProperty);
            mailMerge.ClearValue(Grid.PaddingProperty);
            mailMerge.Children.Clear();
            mailMerge.ColumnDefinitions.Clear();
            mailMerge.RowDefinitions.Clear();
            mailMerge = null;
        }
        public void DisposeListBox(ListBox listBox)
        {
            listBox.ClearValue(ListBox.SelectionModeProperty);
            listBox.ClearValue(ListBox.FontFamilyProperty);
            listBox.ClearValue(ListBox.FontSizeProperty);
            listBox.ClearValue(ListBox.HeightProperty);
            listBox.ClearValue(ListBox.WidthProperty);
            listBox.ClearValue(ListBox.ForegroundProperty);
        }
        public void DisposeRadioButton(RadioButton radioButton)
        {
            radioButton.ClearValue(RadioButton.GroupNameProperty);
            radioButton.ClearValue(RadioButton.ContentProperty);
            radioButton.ClearValue(RadioButton.FontFamilyProperty);
            radioButton.ClearValue(RadioButton.FontSizeProperty);
            radioButton.ClearValue(RadioButton.ForegroundProperty);
            radioButton.ClearValue(RadioButton.WidthProperty);
            radioButton.ClearValue(RadioButton.IsCheckedProperty);
        }
        public void DisposeTextBlock(TextBlock textBlock)
        {
            textBlock.ClearValue(TextBlock.FontFamilyProperty);
            textBlock.ClearValue(TextBlock.FontSizeProperty);
            textBlock.ClearValue(TextBlock.TextProperty);
            textBlock.ClearValue(TextBlock.TextWrappingProperty);
            textBlock.ClearValue(TextBlock.ForegroundProperty);
        }
        public void DisposeButton(Button button)
        {
            button.ClearValue(Button.FontFamilyProperty);
            button.ClearValue(Button.FontSizeProperty);
            button.ClearValue(Button.PaddingProperty);
            button.ClearValue(Button.ForegroundProperty);
            button.ClearValue(Button.BackgroundProperty);
            button.ClearValue(Button.ContentProperty);
            button.ClearValue(Button.HeightProperty);
            button.ClearValue(Button.WidthProperty);
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {

            WordDocument document;
            if (rdDoc.IsChecked == true)
            {
                Stream inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Letter Formatting.doc");
                document = new WordDocument();
                //Open Template document
                await document.OpenAsync(inputStream, FormatType.Doc);
                inputStream.Dispose();
            }
            else
            {
                Stream inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Letter Formatting.docx");
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                    inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Letter Formatting_WP.docx");

                document = new WordDocument();
                //Open Template document
                await document.OpenAsync(inputStream, FormatType.Word2013);
                inputStream.Dispose();
            }
            List<Customer> selectedCustomers = new List<Customer>();
            foreach (Customer customer in Customers)
            {
                if (listBox1.SelectedItems.Contains(customer.CustomerID))
                {
                    customerDetails = new Customer(customer.CustomerID, customer.CompanyName, customer.ContactName, customer.ContactTitle, customer.Address, customer.City, customer.PostalCode, customer.Country, customer.Phone, customer.Fax);
                    selectedCustomers.Add(customerDetails);
                }
            }
            document.MailMerge.Execute(selectedCustomers);
            //Add Text Watermark
            document.Watermark = new TextWatermark();
            (document.Watermark as TextWatermark).Text = "Demo";
            (document.Watermark as TextWatermark).Size = 144;
            Save(rdDoc.IsChecked == true, document);
        }
        private async void Save(bool isDocFormat, WordDocument document)
        {
            StorageFile stgFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as 
                if (isDocFormat)
                    savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".doc" });
                else
                    savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".docx" });
                // Default file name if the user does not type one in or select a file to replace 
                savePicker.SuggestedFileName = "Sample";
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("WordDocument.docx", CreationCollisionOption.ReplaceExisting);
            }
            if (stgFile != null)
            {
                MemoryStream stream = new MemoryStream();
                if (isDocFormat)
                    await document.SaveAsync(stream, FormatType.Doc);
                else
                    await document.SaveAsync(stream, FormatType.Docx);
                document.Close();
                stream.Position = 0;
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stgFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();
                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");
                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new UICommand("No");
                msgDialog.Commands.Add(noCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the retrieved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(stgFile);
                }
            }
        }
        /// <summary>
        /// Initialize Customer
        /// </summary>
        private void InitializeCustomer()
        {
            Customers.Clear();
            Stream stream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Customers.xml");
            customerXml = XDocument.Load(stream);
            element = customerXml.Descendants("Customers").First();
            var pc = from p in customerXml.Descendants("Customer")
                     select p;
            string CustomerID = string.Empty;
            string CompanyName = string.Empty;
            string ContactName = string.Empty;
            string ContactTitle = string.Empty;
            string Address = string.Empty;
            string City = string.Empty;
            string PostalCode = string.Empty;
            string Country = string.Empty;
            string Phone = string.Empty;
            string Fax = string.Empty;
            string Region = string.Empty;
            foreach (var dt in pc)
            {
                foreach (XElement el in dt.Descendants())
                {
                    string value = dt.Element(el.Name).Value;
                    string elementName = el.Name.ToString();
                    switch (elementName)
                    {
                        case "CustomerID":
                            CustomerID = value;
                            break;
                        case "CompanyName":
                            CompanyName = value;
                            break;
                        case "ContactName":
                            ContactName = value;
                            break;
                        case "ContactTitle":
                            ContactTitle = value;
                            break;
                        case "Address":
                            Address = value;
                            break;
                        case "City":
                            City = value;
                            break;
                        case "PostalCode":
                            PostalCode = value;
                            break;
                        case "Country":
                            Country = value;
                            break;
                        case "Phone":
                            Phone = value;
                            break;
                        case "Fax":
                            Fax = value;
                            break;
                    }
                }
                customerDetails = new Customer(CustomerID, CompanyName, ContactName, ContactTitle, Address, City, PostalCode, Country, Phone, Fax);
                Customers.Add(customerDetails);
            }

            listBox1.Items.Clear();
            foreach (Customer customer in Customers)
            {
                listBox1.Items.Add(customer.CustomerID);
            }
            listBox1.SelectedIndex = 0;
        }
    }
    public class Customers : IEnumerable
    {
        // When the foreach loop begins, this method is invoked 
        // so that the loop gets an enumerator to query.
        public IEnumerator GetEnumerator()
        {
            return new CustomerEnumerator();
        }
    }
    public class CustomerEnumerator : IEnumerator
    {
        private XmlReader reader;
        private bool prevTagWasElement = false;
        private string ElementName = string.Empty;
        public void Reset()
        {
            if (this.reader != null)
                this.reader.Dispose();
            Assembly execAssm = typeof(CustomerEnumerator).GetTypeInfo().Assembly;
            Stream stream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Customers.xml");
            this.reader = XmlReader.Create(stream, new XmlReaderSettings());
            //stream.Close();
        }

        public bool MoveNext()
        {
            // Call Reset the first time MoveNext is called 
            // instead of in the constructor 
            // so that we keep the stream open only as long as needed.
            if (this.reader == null)
                this.Reset();

            if (this.FindNextTextElement())
                return true;

            // If there are no more text elements in the XML file then
            // we have read in all of the data 
            // and the foreach loop should end.
            this.reader.Dispose();
            return false;
        }

        public object Current
        {
            get
            {
                // No need to call FindNextTextElement here
                // because it was called for us by MoveNext().
                string CustomerID = string.Empty;
                string CompanyName = string.Empty;
                string ContactName = string.Empty;
                string ContactTitle = string.Empty;
                string Address = string.Empty;
                string City = string.Empty;
                string PostalCode = string.Empty;
                string Country = string.Empty;
                string Phone = string.Empty;
                string Fax = string.Empty;
                ElementName = "CustomerID";
                do
                {
                    switch (ElementName)
                    {
                        case "CustomerID":
                            CustomerID = this.reader.Value;
                            break;
                        case "CompanyName":
                            CompanyName = this.reader.Value;
                            break;
                        case "ContactName":
                            ContactName = this.reader.Value;
                            break;
                        case "ContactTitle":
                            ContactTitle = this.reader.Value;
                            break;
                        case "Address":
                            Address = this.reader.Value;
                            break;
                        case "City":
                            City = this.reader.Value;
                            break;
                        case "PostalCode":
                            PostalCode = this.reader.Value;
                            break;
                        case "Country":
                            Country = this.reader.Value;
                            break;
                        case "Phone":
                            Phone = this.reader.Value;
                            break;
                        case "Fax":
                            Fax = this.reader.Value;
                            break;
                    }
                } while (this.FindNextTextElement());

                return new Customer(CustomerID, CompanyName, ContactName, ContactTitle, Address, City, PostalCode, Country, Phone, Fax);
            }
        }
        private bool FindNextTextElement()
        {
            bool readOn = this.reader.Read();
            prevTagWasElement = false;
            ElementName = string.Empty;
            while (readOn && this.reader.NodeType != XmlNodeType.Text)
            {
                // If the current element is empty, stop reading and return false.
                if (prevTagWasElement && this.reader.NodeType == XmlNodeType.EndElement)
                    readOn = false;
                prevTagWasElement = this.reader.NodeType == XmlNodeType.Element;
                ElementName = (this.reader.NodeType == XmlNodeType.Element) ? this.reader.LocalName : string.Empty;
                readOn = readOn && this.reader.Read();
                if (this.reader.LocalName == "Customer" && this.reader.NodeType == XmlNodeType.EndElement)
                    return false;
            }
            return readOn;
        }

    }
    public class Customer
    {

        #region fields
        private string m_customerID;
        private string m_companyName;
        private string m_contactName;
        private string m_contactTitle;
        private string m_address;
        private string m_city;
        private string m_postalCode;
        private string m_country;
        private string m_phone;
        private string m_fax;
        #endregion fields

        #region Properties
        public string CustomerID
        {
            get { return m_customerID; }
            set { m_customerID = value; }
        }
        public string CompanyName
        {
            get { return m_companyName; }
            set { m_companyName = value; }
        }
        public string ContactName
        {
            get { return m_contactName; }
            set { m_contactName = value; }
        }
        public string ContactTitle
        {
            get { return m_contactTitle; }
            set { m_contactTitle = value; }
        }
        public string Address
        {
            get { return m_address; }
            set { m_address = value; }
        }
        public string City
        {
            get { return m_city; }
            set { m_city = value; }
        }
        public string PostalCode
        {
            get { return m_postalCode; }
            set { m_postalCode = value; }
        }
        public string Country
        {
            get { return m_country; }
            set { m_country = value; }
        }
        public string Phone
        {
            get { return m_phone; }
            set { m_phone = value; }
        }
        public string Fax
        {
            get { return m_fax; }
            set { m_fax = value; }
        }
        #endregion Properties

        #region Constructors
        public Customer(string CustomerID, string CompanyName, string ContactName,
                        string ContactTitle, string Address, string City,
                        string PostalCode, string Country, string Phone, string Fax)
        {
            this.CustomerID = CustomerID;
            this.CompanyName = CompanyName;
            this.ContactName = ContactName;
            this.ContactTitle = ContactTitle;
            this.Address = Address;
            this.City = City;
            this.PostalCode = PostalCode;
            this.Country = Country;
            this.Phone = Phone;
            this.Fax = Fax;
        }
        #endregion
    }
}
