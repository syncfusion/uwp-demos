#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
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
using System.Drawing;
using Windows.Storage.Pickers;
using Windows.Storage;
using Common;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI;
using System.Reflection;
using Syncfusion.Pdf.Interactive;
using Windows.UI.Popups;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class JobApplication : UserControl
    {
        private PdfDocument document;
        private PdfFont pdfFont;
        private PdfBrush solidBrush;
        private PdfImage img;
        private RectangleF bounds;
        private SizeF pageSize;
        private PdfPage page;
        private PdfComboBoxField comboBox;
        private float x;
        public JobApplication()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10, 10, 10, 10);
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
            }
            x = 25;
        }             

        private async void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            Stream imageStream = typeof(JobApplication).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Careers.png");             

            // Create a new instance of PdfDocument class.
            document = new PdfDocument();

            // Set viewer preferences for the reader 
            document.ViewerPreferences.HideMenubar = true;
            document.ViewerPreferences.HideWindowUI = true;
            document.ViewerPreferences.HideToolbar = true;
            document.ViewerPreferences.FitWindow = true;

            document.ViewerPreferences.PageLayout = PdfPageLayout.SinglePage;
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 0;

            // Set coordinates to draw form fields
            bounds = new RectangleF(180, 65, 156, 15);
            img = new PdfBitmap(imageStream);

            // Set page size
            pageSize = new SizeF(500, 310);
            document.PageSettings.Width = pageSize.Width;
            document.PageSettings.Height = pageSize.Height;

            // Initialize font and brush          
            pdfFont = new PdfStandardFont(PdfFontFamily.Helvetica,12f,PdfFontStyle.Bold);
            solidBrush = new PdfSolidBrush(new PdfColor(124, 143, 166));

            CreateFirstPage();
            CreateSecondPage();
            CreateThirdPage();
            MemoryStream stream = new MemoryStream();
            await document.SaveAsync(stream);
            document.Close(true);
            Save(stream, "StampDocument.pdf");         
        }
        async void Save(Stream stream, string filename)
        {

            stream.Position = 0;
            StorageFile stFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.DefaultFileExtension = ".pdf";
                savePicker.SuggestedFileName = "Sample";
                savePicker.FileTypeChoices.Add("Adobe PDF Document", new List<string>() { ".pdf" });
                stFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stFile = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            }

            if (stFile != null)
            {
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();
                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File created.");
                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new UICommand("No");
                msgDialog.Commands.Add(noCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the retrieved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(stFile);
                }
            }


        }

        #region Helpher Methods

        /// <summary>
        /// Creates third page in the document
        /// </summary>
        private void CreateThirdPage()
        {
            // Access third page.
            page = document.Pages[2];
            page.Graphics.DrawImage(img, 0, 0, pageSize.Width, pageSize.Height);

            pdfFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12f, PdfFontStyle.Bold);

            page.Graphics.DrawString("Thank You", pdfFont, new PdfSolidBrush(new PdfColor(213, 123, 19)), x, 80);
         
            pdfFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12f, PdfFontStyle.Regular);

            page.Graphics.DrawString("Thanks for taking the time to complete this form.\nWe will be in contact with you shortly.", pdfFont, solidBrush, x, 110);

            // Send email during button click.
            PdfButtonField submitButton1 = new PdfButtonField(page, "submitButton1");
            submitButton1.Bounds = new RectangleF(x, 160, 100, 20);
            submitButton1.Font = pdfFont;
            submitButton1.Text = "Apply";
            submitButton1.BorderStyle = PdfBorderStyle.Beveled;
            submitButton1.BackColor = new PdfColor(181, 191, 203);

            // Create a javascript action.
            PdfJavaScriptAction javaAction = new PdfJavaScriptAction("address = app.response(\"Enter an e-mail address.\",\"SEND E-MAIL\",\"\");"
                + "if (address){ cmdLine = \"mailto:\" + address;" +
                "this.submitForm(cmdLine,true,false,\"Remarks\");}");

            submitButton1.Actions.MouseUp = javaAction;

            // Add button to the form.
            document.Form.Fields.Add(submitButton1);

            document.Form.SetDefaultAppearance(false);
        }

        /// <summary>
        /// Creates second page in the document.
        /// </summary>
        private void CreateSecondPage()
        {
            //Access second page.
            page = document.Pages[1];

            page.Graphics.DrawImage(img, new PointF(0, 0), new SizeF(pageSize.Width, pageSize.Height));

            page.Graphics.DrawString("Current position", new PdfStandardFont(PdfFontFamily.TimesRoman, 10, PdfFontStyle.Bold), new PdfSolidBrush(new PdfColor(213, 123, 19)), x, 40);

            bounds.X = x; bounds.Y = 65;

            // Add fields in second page.
            // Add check box for employment status.
            CreateCheckBox(page, "Cemp", "Employment Status");
            page.Graphics.DrawString("I am not currently employed", pdfFont, solidBrush, bounds.X, bounds.Y);
            bounds.X += 90;

            page.Graphics.DrawString("Job Title", pdfFont, solidBrush, x, 85);

            bounds.X = 175;
            bounds.Y = 85;
            bounds.Width = 150;
            bounds.Height = 16;

            //Create text box for Job title.
            CreateTextBox(page, "Jtitle", "Job title", pdfFont, bounds);

            page.Graphics.DrawString("Employer:", pdfFont, solidBrush, x, 103);
            bounds.Y = bounds.Y + 18;
            //Create text box for Employer.
            CreateTextBox(page, "Employer", "Employer", pdfFont, bounds);

            page.Graphics.DrawString("Reason for leaving:", pdfFont, solidBrush, x, 123);
            bounds.Y = bounds.Y + 18;
            // Create text box for Reason.
            CreateTextBox(page, "Reason", "Reason for leaving", pdfFont, bounds);

            page.Graphics.DrawString("Total Annual salary:", pdfFont, solidBrush, x, 143);
            bounds.Y = bounds.Y + 18;
            //Create text box for annual salary.
            CreateTextBox(page, "Asalary", "Annual salary", pdfFont, bounds);

            page.Graphics.DrawString("Duties:", pdfFont, solidBrush, x, 168);

            bounds.Y = bounds.Y + 50;
            bounds.X = x;
            bounds.Height = bounds.Height * 4;

            //Creates textbox for Duties
            CreateTextBox(page, "Duties", "Duties", pdfFont, bounds);
            page.Graphics.DrawString("Employment type:", pdfFont, solidBrush, x, 268);

            #region Create ComboBox

            //Set position to draw Combo box.
            bounds.Y = bounds.Y + 74;
            bounds.Height = 20;
            bounds.X = 175;

            //Create a combo box.
            comboBox = new PdfComboBoxField(page, "EmpType");
            comboBox.Bounds = bounds;

            comboBox.BorderColor = new PdfColor(System.Drawing.Color.FromArgb(255, 128, 128, 128));
            comboBox.Font = pdfFont;
            comboBox.ToolTip = "Employment type";

            // Add list items.
            comboBox.Items.Add(new PdfListFieldItem("Full time", "ft"));
            comboBox.Items.Add(new PdfListFieldItem("Part time", "pt"));

            // Add combo box to the form.
            document.Form.Fields.Add(comboBox);
            #endregion

            // Add navigation button for the third page.
            NavigateToNextPage(page, "Apply");
        }

        /// <summary>
        /// Adds navigation button.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="buttonText"></param>
        private void NavigateToNextPage(PdfPage currentPage, string buttonText)
        {
            // Create a Button field.
            PdfButtonField submitButton = new PdfButtonField(page, buttonText);
            submitButton.Bounds = new RectangleF(page.GetClientSize().Width - 30, page.GetClientSize().Height - x, 25, 15);
            submitButton.Font = pdfFont;
            submitButton.ToolTip = buttonText;

            // Add a new page.
            page = document.Pages.Add();

            // Create an instance of PdfDestination.
            PdfDestination dest = new PdfDestination(page, new PointF(0, 100));

            // Create an instance of GoTo action.
            PdfGoToAction goToAction = new PdfGoToAction(page);

            // Update the destination for the action.
            goToAction.Destination = dest;

            // Set action for the field.
            submitButton.Actions.GotFocus = goToAction;
        }

        /// <summary>
        /// Creates first page in the document.
        /// </summary>
        private void CreateFirstPage()
        {
            // Add a new page.
            page = document.Pages.Add();

            page.Graphics.DrawImage(img, 0, 0, pageSize.Width, pageSize.Height);
            page.Graphics.DrawString("General Information", pdfFont, new PdfSolidBrush(new PdfColor(213, 123, 19)), x, 40);
            page.Graphics.DrawString("Education Grade", pdfFont, new PdfSolidBrush(new PdfColor(213, 123, 19)), x, 190);

            // Update font         
            pdfFont = new PdfStandardFont(PdfFontFamily.Helvetica,10f,PdfFontStyle.Regular);

            // Create fields for first page.
            page.Graphics.DrawString("First Name:", pdfFont, solidBrush, x, 65);

            //Create text box for FirstName.
            CreateTextBox(page, "FirstName", "First Name", pdfFont, bounds);

            page.Graphics.DrawString("Last Name:", pdfFont, solidBrush, x, 83);
            bounds.Y += 18;

            //Create text box for LastName.
            CreateTextBox(page, "LastName", "Last Name", pdfFont, bounds);

            page.Graphics.DrawString("Email:", pdfFont, solidBrush, x, 103);
            bounds.Y += 18;

            //Create text box for Email.
            CreateTextBox(page, "Email", "Email id", pdfFont, bounds);

            page.Graphics.DrawString("Business Phone:", pdfFont, solidBrush, x, 123);
            bounds.Y += 18;

            //Create text box for Business phone.
            CreateTextBox(page, "Business", "Business phone", pdfFont, bounds);

            page.Graphics.DrawString("Which position are\nyou applying for?", pdfFont, solidBrush, x, 143);

            bounds.Y += 24;

            #region Create ComboBox

            // Create a combo box for the first page.
            PdfComboBoxField comboBox = new PdfComboBoxField(page, "JobTitle");
            comboBox.Bounds = bounds;
            comboBox.BorderWidth = 1;
            comboBox.BorderColor = new PdfColor(System.Drawing.Color.FromArgb(255, 128, 128, 128));

            comboBox.Font = pdfFont;

            // Set tooltip
            comboBox.ToolTip = "Job Title";

            // Add list items.
            comboBox.Items.Add(new PdfListFieldItem("Development", "accounts"));
            comboBox.Items.Add(new PdfListFieldItem("Support", "advertise"));
            comboBox.Items.Add(new PdfListFieldItem("Documentation", "agri"));

            // Add combo box to the form.
            document.Form.Fields.Add(comboBox);
            #endregion

            page.Graphics.DrawString("Highest qualification", pdfFont, solidBrush, x, 217);

            #region Create CheckBox
            //Set position to draw Checkbox
            bounds.Y = 239;
            bounds.X = x;
            bounds.Width = 10;

            bounds.Height = 10;

            // Create check box for Associate Degree.
            CreateCheckBox(page, "Adegree", "Degree");
            page.Graphics.DrawString("Associate degree", pdfFont, solidBrush, bounds.X, bounds.Y);
            bounds.X += 90;

            // Create check box for Bachelor Degree.
            CreateCheckBox(page, "Bdegree", "Degree");
            page.Graphics.DrawString("Bachelor degree", pdfFont, solidBrush, bounds.X, bounds.Y);
            bounds.X += 90;

            // Create check box for College.
            CreateCheckBox(page, "college", "College");
            page.Graphics.DrawString("College", pdfFont, solidBrush, bounds.X, bounds.Y);

            bounds.Y += 20;
            bounds.X = x;

            // Create check box for PG.
            CreateCheckBox(page, "pg", "PG");
            page.Graphics.DrawString("Post Graduate", pdfFont, solidBrush, bounds.X, bounds.Y);
            bounds.X += 90;

            // Create check box for MBA.
            CreateCheckBox(page, "mba", "MBA");
            page.Graphics.DrawString("MBA", pdfFont, solidBrush, bounds.X, bounds.Y);

            #endregion

            // Add navigation button for the second page.
            NavigateToNextPage(page, "Next");
        }

        /// <summary>
        /// Creates check box and adds it to the form.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="name"></param>
        /// <param name="tooltip"></param>
        private void CreateCheckBox(PdfPage page, string name, string tooltip)
        {
            // Create a Check box field.
            PdfCheckBoxField chb = new PdfCheckBoxField(page, name);

            // Set check box properties.
            chb.Font = pdfFont;
            chb.ToolTip = tooltip;
            chb.Bounds = bounds;
            chb.BorderColor = new PdfColor(System.Drawing.Color.FromArgb(255, 128, 128, 128));
            bounds.X += chb.Bounds.Height + 10;

            document.Form.Fields.Add(chb);
        }

        /// <summary>
        /// Creates textbox and adds it in the form.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="text"></param>
        /// <param name="tooltip"></param>
        /// <param name="f"></param>
        /// <param name="bounds"></param>
        private void CreateTextBox(PdfPage page, string text, string tooltip, PdfFont f, RectangleF bounds)
        {
            // Create a Text box field.
            PdfTextBoxField textBoxField = new PdfTextBoxField(page, text);        

            //Set properties to the textbox.
            textBoxField.Font = f;
            textBoxField.BorderColor = new PdfColor(System.Drawing.Color.FromArgb(255, 128, 128, 128));
            textBoxField.BorderStyle = PdfBorderStyle.Beveled;
            textBoxField.Bounds = bounds;
            textBoxField.ToolTip = tooltip;

            document.Form.Fields.Add(textBoxField);
        }     
      
        #endregion
    }
}
