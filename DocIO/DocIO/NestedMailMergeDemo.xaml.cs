using Common;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
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
    public sealed partial class NestedMailMergeDemo : SampleLayout
    {
        #region Fields
        Assembly execAssm = typeof(NestedMailMergeDemo).GetTypeInfo().Assembly;
        #endregion

        public NestedMailMergeDemo()
        {
            InitializeComponent();

#if STORE_SB
            TemplateTextBlock.Visibility = Visibility.Collapsed;
            rdReport.Visibility = Visibility.Collapsed;
            rdLetter.Visibility = Visibility.Collapsed;
            Align1.Visibility = Visibility.Collapsed;
            Align2.Visibility = Visibility.Collapsed;
#endif
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                mailMerge.Margin = new Thickness(10, 10, 10, 10);
                stackPnlOptions.Visibility = Visibility.Collapsed;
                saveas.Visibility = Visibility.Collapsed;
                text1.Visibility = Visibility.Collapsed;
                text2.Visibility = Visibility.Collapsed;
                TemplateTextBlock.Visibility = Visibility.Collapsed;
                rdReport.Visibility = Visibility.Collapsed;
                rdLetter.Visibility = Visibility.Collapsed;
                Align1.Visibility = Visibility.Collapsed;
                Align2.Visibility = Visibility.Collapsed;
                rdImplicit.Margin = new Thickness(0, 0, 13, 0);
                rdExplicit.Margin = new Thickness(0, 0, 13, 0);
                WinRTText2.Visibility = Visibility.Collapsed;
            }
            else
            {
                stackPnlOptions.Visibility = Visibility.Visible;
                stackTemplateOptions.Visibility = Visibility.Visible;
                Data.Visibility = Visibility.Visible;
                saveas.Visibility = Visibility.Visible;
                text1.Visibility = Visibility.Visible;
                text2.Visibility = Visibility.Visible;
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
            DisposeTextBlock(TextBlock4);
            TextBlock4 = null;
            DisposeTextBlock(WinRTText1);
            WinRTText1 = null;
            DisposeTextBlock(Align1);
            Align1 = null;
            DisposeTextBlock(TemplateTextBlock);
            TemplateTextBlock = null;
            DisposeTextBlock(Align2);
            Align2 = null;
            DisposeTextBlock(Data);
            Data = null;
            DisposeTextBlock(Align3);
            Align3 = null;
            DisposeTextBlock(WinRTText2);
            WinRTText2 = null;
            DisposeTextBlock(text1);
            text1 = null;
            DisposeTextBlock(saveas);
            saveas = null;
            DisposeTextBlock(text2);
            text2 = null;

            Button1.Click -= Button_Click_1;
            DisposeButton(Button1);
            Button1 = null;

            DisposeRadioButton(rdDoc);
            rdDoc = null;
            DisposeRadioButton(rdDocx);
            rdDocx = null;
            DisposeRadioButton(rdExplicit);
            rdExplicit = null;
            DisposeRadioButton(rdImplicit);
            rdImplicit = null;
            DisposeRadioButton(rdLetter);
            rdLetter = null;
            DisposeRadioButton(rdReport);
            rdReport = null;

            DisposeStackPanel(stackPnlOptions);
            stackPnlOptions = null;
            DisposeStackPanel(stackRekationOptions);
            stackRekationOptions = null;
            DisposeStackPanel(stackTemplateOptions);
            stackTemplateOptions = null;

            mailMerge.ClearValue(Grid.BackgroundProperty);
            mailMerge.ClearValue(Grid.PaddingProperty);
            mailMerge.Children.Clear();
            mailMerge.ColumnDefinitions.Clear();
            mailMerge.RowDefinitions.Clear();
            mailMerge = null;
        }
        public void DisposeStackPanel(StackPanel stackPanel)
        {
            stackPanel.ClearValue(StackPanel.OrientationProperty);
            stackPanel.ClearValue(StackPanel.HorizontalAlignmentProperty);
            stackPanel = null;
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

            WordDocument document = new WordDocument();
            
#if STORE_SB
            Stream inputStream = inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Template_Report.doc");
            //Open Template document
            await document.OpenAsync(inputStream, FormatType.Word2013);
            inputStream.Dispose();
#else
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {

                Stream inputStream = inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Template_Letter_WP.doc");

                await document.OpenAsync(inputStream, FormatType.Doc);
                inputStream.Dispose();
            }
            else
            {
                Stream inputStream;
                if (rdReport.IsChecked == true)

                    inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Template_Report.doc");

                else

                    inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Template_Letter.doc");

                //Open Template document
                await document.OpenAsync(inputStream, FormatType.Word2013);
                inputStream.Dispose();
            }
#endif
            #region Execute Mail merge
            if (rdExplicit.IsChecked.Value)
            {
                MailMergeDataSet dataSet = GetMailMergeDataSet();
                List<DictionaryEntry> commands = new List<DictionaryEntry>();
                //DictionaryEntry contain "Source table" (KEY) and "Command" (VALUE)
                DictionaryEntry entry = new DictionaryEntry("Employees", string.Empty);
                commands.Add(entry);

                // To retrive customer details
                entry = new DictionaryEntry("Customers", "EmployeeID = %Employees.EmployeeID%");
                commands.Add(entry);

                // To retrieve order details
                entry = new DictionaryEntry("Orders", "CustomerID = %Customers.CustomerID%");
                commands.Add(entry);

                //Executes nested Mail merge using explicit relational data.
                document.MailMerge.ExecuteNestedGroup(dataSet, commands);
            }
            else
            {
                MailMergeDataTable dataTable = GetMailMergeDataTable();
                //Executes nested Mail merge using implicit relational data.
                document.MailMerge.ExecuteNestedGroup(dataTable);
            }
#endregion
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
#region Mail merge data
        /// <summary>
        /// Gets the mail merge data set.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">reader</exception>
        /// <exception cref="XmlException">Unexpected xml tag  + reader.LocalName</exception>
        private MailMergeDataSet GetMailMergeDataSet()
        {
            List<EmployeeDetails> employees = new List<EmployeeDetails>();
            List<CustomerDetails> customers = new List<CustomerDetails>();
            List<OrderDetails> orders = new List<OrderDetails>();

            Stream stream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Employees.xml");

            XmlReader reader = XmlReader.Create(stream);

            if (reader == null)
                throw new Exception("reader");

            while (reader.NodeType != XmlNodeType.Element)
                reader.Read();

            if (reader.LocalName != "EmployeesList")
                throw new XmlException("Unexpected xml tag " + reader.LocalName);

            reader.Read();

            while (reader.NodeType == XmlNodeType.Whitespace)
                reader.Read();

            while (reader.LocalName != "EmployeesList")
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.LocalName)
                    {
                        case "Employees":
                            employees.Add(GetEmployee(reader, customers, orders));
                            break;
                    }
                }
                else
                {
                    reader.Read();
                    if ((reader.LocalName == "EmployeesList") && reader.NodeType == XmlNodeType.EndElement)
                        break;
                }
            }
            reader.Dispose();
            stream.Dispose();
            MailMergeDataSet dataSet = new MailMergeDataSet();
            dataSet.Add(new MailMergeDataTable("Employees", employees));
            dataSet.Add(new MailMergeDataTable("Customers", customers));
            dataSet.Add(new MailMergeDataTable("Orders", orders));
            return dataSet;
        }
        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="customers">The customers.</param>
        /// <param name="orders">The orders.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">reader</exception>
        /// <exception cref="XmlException">Unexpected xml tag  + reader.LocalName</exception>
        private EmployeeDetails GetEmployee(XmlReader reader, List<CustomerDetails> customers, List<OrderDetails> orders)
        {
            if (reader == null)
                throw new Exception("reader");

            while (reader.NodeType != XmlNodeType.Element)
                reader.Read();

            if (reader.LocalName != "Employees")
                throw new XmlException("Unexpected xml tag " + reader.LocalName);

            reader.Read();

            while (reader.NodeType == XmlNodeType.Whitespace)
                reader.Read();

            EmployeeDetails employee = new EmployeeDetails();
            while (reader.LocalName != "Employees")
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.LocalName)
                    {
                        case "EmployeeID":
                            employee.EmployeeID = reader.ReadElementContentAsString();
                            break;
                        case "LastName":
                            employee.LastName = reader.ReadElementContentAsString();
                            break;
                        case "FirstName":
                            employee.FirstName = reader.ReadElementContentAsString();
                            break;
                        case "Address":
                            employee.Address = reader.ReadElementContentAsString();
                            break;
                        case "City":
                            employee.City = reader.ReadElementContentAsString();
                            break;
                        case "Country":
                            employee.Country = reader.ReadElementContentAsString();
                            break;
                        case "Extension":
                            employee.Extension = reader.ReadElementContentAsString();
                            break;
                        case "Customers":
                            customers.Add(GetCustomer(reader, orders));
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
                else
                {
                    reader.Read();
                    if ((reader.LocalName == "Employees") && reader.NodeType == XmlNodeType.EndElement)
                        break;
                }
            }
            return employee;
        }
        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="orders">The orders.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">reader</exception>
        /// <exception cref="XmlException">Unexpected xml tag  + reader.LocalName</exception>
        private CustomerDetails GetCustomer(XmlReader reader, List<OrderDetails> orders)
        {
            if (reader == null)
                throw new Exception("reader");

            while (reader.NodeType != XmlNodeType.Element)
                reader.Read();

            if (reader.LocalName != "Customers")
                throw new XmlException("Unexpected xml tag " + reader.LocalName);

            reader.Read();

            while (reader.NodeType == XmlNodeType.Whitespace)
                reader.Read();

            CustomerDetails customer = new CustomerDetails();
            while (reader.LocalName != "Customers")
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.LocalName)
                    {
                        case "EmployeeID":
                            customer.EmployeeID = reader.ReadElementContentAsString();
                            break;
                        case "CustomerID":
                            customer.CustomerID = reader.ReadElementContentAsString();
                            break;
                        case "CompanyName":
                            customer.CompanyName = reader.ReadElementContentAsString();
                            break;
                        case "ContactName":
                            customer.ContactName = reader.ReadElementContentAsString();
                            break;
                        case "City":
                            customer.City = reader.ReadElementContentAsString();
                            break;
                        case "Country":
                            customer.Country = reader.ReadElementContentAsString();
                            break;
                        case "Orders":
                            orders.Add(GetOrders(reader));
                            break;
                    }
                    reader.Read();
                }
                else
                {
                    reader.Read();
                    if ((reader.LocalName == "Customers") && reader.NodeType == XmlNodeType.EndElement)
                        break;
                }
            }
            return customer;
        }

        /// <summary>
        /// Gets the mail merge data table.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">reader</exception>
        /// <exception cref="XmlException">Unexpected xml tag  + reader.LocalName</exception>
        private MailMergeDataTable GetMailMergeDataTable()
        {
            List<EmployeeDetailsImplicit> employees = new List<EmployeeDetailsImplicit>();

            Stream stream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.Employees.xml");

            XmlReader reader = XmlReader.Create(stream);

            if (reader == null)
                throw new Exception("reader");

            while (reader.NodeType != XmlNodeType.Element)
                reader.Read();

            if (reader.LocalName != "EmployeesList")
                throw new XmlException("Unexpected xml tag " + reader.LocalName);

            reader.Read();

            while (reader.NodeType == XmlNodeType.Whitespace)
                reader.Read();

            while (reader.LocalName != "EmployeesList")
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.LocalName)
                    {
                        case "Employees":
                            employees.Add(GetEmployee(reader));
                            break;
                    }
                }
                else
                {
                    reader.Read();
                    if ((reader.LocalName == "EmployeesList") && reader.NodeType == XmlNodeType.EndElement)
                        break;
                }
            }
            reader.Dispose();
            stream.Dispose();
            MailMergeDataTable dataTable = new MailMergeDataTable("Employees", employees);
            return dataTable;
        }
        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">reader</exception>
        /// <exception cref="XmlException">Unexpected xml tag  + reader.LocalName</exception>
        private EmployeeDetailsImplicit GetEmployee(XmlReader reader)
        {
            if (reader == null)
                throw new Exception("reader");

            while (reader.NodeType != XmlNodeType.Element)
                reader.Read();

            if (reader.LocalName != "Employees")
                throw new XmlException("Unexpected xml tag " + reader.LocalName);

            reader.Read();

            while (reader.NodeType == XmlNodeType.Whitespace)
                reader.Read();

            EmployeeDetailsImplicit employee = new EmployeeDetailsImplicit();
            while (reader.LocalName != "Employees")
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.LocalName)
                    {
                        case "EmployeeID":
                            employee.EmployeeID = reader.ReadElementContentAsString();
                            break;
                        case "LastName":
                            employee.LastName = reader.ReadElementContentAsString();
                            break;
                        case "FirstName":
                            employee.FirstName = reader.ReadElementContentAsString();
                            break;
                        case "Address":
                            employee.Address = reader.ReadElementContentAsString();
                            break;
                        case "City":
                            employee.City = reader.ReadElementContentAsString();
                            break;
                        case "Country":
                            employee.Country = reader.ReadElementContentAsString();
                            break;
                        case "Extension":
                            employee.Extension = reader.ReadElementContentAsString();
                            break;
                        case "Customers":
                            employee.Customers.Add(GetCustomer(reader));
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
                else
                {
                    reader.Read();
                    if ((reader.LocalName == "Employees") && reader.NodeType == XmlNodeType.EndElement)
                        break;
                }
            }
            return employee;
        }
        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">reader</exception>
        /// <exception cref="XmlException">Unexpected xml tag  + reader.LocalName</exception>
        private CustomerDetailsImplicit GetCustomer(XmlReader reader)
        {
            if (reader == null)
                throw new Exception("reader");

            while (reader.NodeType != XmlNodeType.Element)
                reader.Read();

            if (reader.LocalName != "Customers")
                throw new XmlException("Unexpected xml tag " + reader.LocalName);

            reader.Read();

            while (reader.NodeType == XmlNodeType.Whitespace)
                reader.Read();

            CustomerDetailsImplicit customer = new CustomerDetailsImplicit();
            while (reader.LocalName != "Customers")
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.LocalName)
                    {
                        case "EmployeeID":
                            customer.EmployeeID = reader.ReadElementContentAsString();
                            break;
                        case "CustomerID":
                            customer.CustomerID = reader.ReadElementContentAsString();
                            break;
                        case "CompanyName":
                            customer.CompanyName = reader.ReadElementContentAsString();
                            break;
                        case "ContactName":
                            customer.ContactName = reader.ReadElementContentAsString();
                            break;
                        case "City":
                            customer.City = reader.ReadElementContentAsString();
                            break;
                        case "Country":
                            customer.Country = reader.ReadElementContentAsString();
                            break;
                        case "Orders":
                            customer.Orders.Add(GetOrders(reader));
                            break;
                    }
                    reader.Read();
                }
                else
                {
                    reader.Read();
                    if ((reader.LocalName == "Customers") && reader.NodeType == XmlNodeType.EndElement)
                        break;
                }
            }
            return customer;
        }
        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">reader</exception>
        /// <exception cref="XmlException">Unexpected xml tag  + reader.LocalName</exception>
        private OrderDetails GetOrders(XmlReader reader)
        {
            if (reader == null)
                throw new Exception("reader");

            while (reader.NodeType != XmlNodeType.Element)
                reader.Read();

            if (reader.LocalName != "Orders")
                throw new XmlException("Unexpected xml tag " + reader.LocalName);

            reader.Read();

            while (reader.NodeType == XmlNodeType.Whitespace)
                reader.Read();

            OrderDetails order = new OrderDetails();
            while (reader.LocalName != "Orders")
            {
                if (reader.NodeType != XmlNodeType.EndElement)
                {
                    switch (reader.LocalName)
                    {
                        case "OrderID":
                            order.OrderID = reader.ReadElementContentAsString();
                            break;
                        case "CustomerID":
                            order.CustomerID = reader.ReadElementContentAsString();
                            break;
                        case "OrderDate":
                            order.OrderDate = reader.ReadElementContentAsString();
                            break;
                        case "RequiredDate":
                            order.RequiredDate = reader.ReadElementContentAsString();
                            break;
                        case "ShippedDate":
                            order.ShippedDate = reader.ReadElementContentAsString();
                            break;
                    }
                    reader.Read();
                }
                else
                {
                    reader.Read();
                    if ((reader.LocalName == "Orders") && reader.NodeType == XmlNodeType.EndElement)
                        break;
                }
            }
            return order;
        }
#endregion
    }
#region Mailmerge data objects
    public class EmployeeDetailsImplicit:IDisposable
    {
#region Fields
        private string m_employeeID;
        private string m_lastName;
        private string m_firstName;
        private string m_address;
        private string m_city;
        private string m_country;
        private string m_extension;
        private List<CustomerDetailsImplicit> m_customers;
#endregion

#region Properties
        public string EmployeeID
        {
            get { return m_employeeID; }
            set { m_employeeID = value; }
        }
        public string LastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }
        public string FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value; }
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
        public string Country
        {
            get { return m_country; }
            set { m_country = value; }
        }
        public string Extension
        {
            get { return m_extension; }
            set { m_extension = value; }
        }
        public List<CustomerDetailsImplicit> Customers
        {
            get
            {
                if (m_customers == null)
                    m_customers = new List<CustomerDetailsImplicit>();
                return m_customers;
            }
            set
            {
                m_customers = value;
            }
        }
#endregion

#region Constructor
        public EmployeeDetailsImplicit()
        {
            m_customers = new List<CustomerDetailsImplicit>();
        }

        public void Dispose()
        {
            EmployeeID = null;
            m_lastName = null;
            m_firstName = null;
            m_address = null;
            m_city = null;
            m_country = null;
            m_extension = null;
            m_customers.Clear();
        }
#endregion
    }
    public class CustomerDetailsImplicit:IDisposable
    {
#region Fields
        private string m_employeeID;
        private string m_customerID;
        private string m_companyName;
        private string m_city;
        private string m_country;
        private List<OrderDetails> m_orders;
#endregion

#region Properties
        public List<OrderDetails> Orders
        {
            get
            {
                if (m_orders == null)
                    m_orders = new List<OrderDetails>();
                return m_orders;
            }
            set
            {
                m_orders = value;
            }
        }
        public string EmployeeID
        {
            get { return m_employeeID; }
            set { m_employeeID = value; }
        }
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
            get { return m_companyName; }
            set { m_companyName = value; }
        }
        public string City
        {
            get { return m_city; }
            set { m_city = value; }
        }
        public string Country
        {
            get { return m_country; }
            set { m_country = value; }
        }
#endregion

#region Constructor
        public CustomerDetailsImplicit()
        {
            m_orders = new List<OrderDetails>();
        }

        public void Dispose()
        {
            Orders.Clear();
            m_employeeID = null;
            CustomerID = null;
            m_companyName = null;
            m_city = null;
            m_country = null;
        }
#endregion
    }
    public class EmployeeDetails:IDisposable
    {
#region Fields
        private string m_employeeID;
        private string m_lastName;
        private string m_firstName;
        private string m_address;
        private string m_city;
        private string m_country;
        private string m_extension;
#endregion

#region Properties
        public string EmployeeID
        {
            get { return m_employeeID; }
            set { m_employeeID = value; }
        }
        public string LastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }
        public string FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value; }
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
        public string Country
        {
            get { return m_country; }
            set { m_country = value; }
        }
        public string Extension
        {
            get { return m_extension; }
            set { m_extension = value; }
        }

        public void Dispose()
        {
            m_employeeID = null;
            m_lastName = null;
            m_firstName = null;
            m_address = null;
            m_city = null;
            m_country = null;
            m_extension = null;
        }
#endregion
    }
    public class CustomerDetails:IDisposable
    {
#region Fields
        private string m_employeeID;
        private string m_customerID;
        private string m_companyName;
        private string m_city;
        private string m_country;
#endregion

#region Properties
        public string EmployeeID
        {
            get { return m_employeeID; }
            set { m_employeeID = value; }
        }
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
            get { return m_companyName; }
            set { m_companyName = value; }
        }
        public string City
        {
            get { return m_city; }
            set { m_city = value; }
        }
        public string Country
        {
            get { return m_country; }
            set { m_country = value; }
        }

        public void Dispose()
        {
            m_employeeID = null;
            m_customerID = null;
            m_companyName = null;
            m_city = null;
            m_country = null;
        }
#endregion
    }
    public class OrderDetails:IDisposable
    {
#region Fields
        private string m_orderID;
        private string m_customerID;
        private string m_orderDate;
        private string m_requiredDate;
        private string m_shippedDate;
#endregion

#region Properties
        public string OrderID
        {
            get { return m_orderID; }
            set { m_orderID = value; }
        }
        public string CustomerID
        {
            get { return m_customerID; }
            set { m_customerID = value; }
        }
        public string OrderDate
        {
            get { return m_orderDate; }
            set { m_orderDate = value; }
        }
        public string RequiredDate
        {
            get { return m_requiredDate; }
            set { m_requiredDate = value; }
        }
        public string ShippedDate
        {
            get { return m_shippedDate; }
            set { m_shippedDate = value; }
        }

        public void Dispose()
        {
            m_orderID = null;
            m_customerID = null;
            m_orderDate = null;
            m_requiredDate = null;
            m_shippedDate = null;
        }
#endregion
    }
#endregion
}
