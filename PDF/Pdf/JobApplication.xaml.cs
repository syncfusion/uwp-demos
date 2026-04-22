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

            // Build Job Application PDF (2 pages) similar to JobApplication.cshtml.cs
            CreateFirstPage();
            CreateSecondPage();
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
        /// Creates second page in the document.
        /// </summary>
        private void CreateSecondPage()
        {
            // Create second page per the sample (Employment Type, Declaration, Signature, T&Cs, Submit)
            PdfPage page2 = document.Pages.Add();
            PdfGraphics g2 = page2.Graphics;

            // Fonts
            PdfFont titleFont = new PdfStandardFont(PdfFontFamily.Helvetica, 17, PdfFontStyle.Regular);
            PdfFont subTitleFont = new PdfStandardFont(PdfFontFamily.Helvetica, 9, PdfFontStyle.Regular);
            PdfFont normalFont = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Regular);
            PdfFont boldFont = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);

            // Brushes and pens
            PdfSolidBrush tealBrush = new PdfSolidBrush(new PdfColor(0, 128, 128));
            PdfSolidBrush blackBrush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            PdfSolidBrush whiteBrush = new PdfSolidBrush(new PdfColor(255, 255, 255));
            PdfPen grayPen = new PdfPen(new PdfColor(102, 106, 109), 1);

            SizeF page2Size = page2.GetClientSize();
            float leftMargin = 20f;
            float rightMargin = 20f;
            float innerWidth = page2Size.Width - leftMargin - rightMargin;
            float fieldSpacing = 10f;
            float fieldWidth = innerWidth / 2f - 5f;
            float y2 = 20f;

            // Employment Type
            g2.DrawString("Employment Type", normalFont, blackBrush, new PointF(leftMargin, y2));
            y2 += 25f;
            PdfComboBoxField empType = new PdfComboBoxField(page2, "EmploymentType");
            empType.Bounds = new RectangleF(leftMargin, y2, fieldWidth, 20f);
            empType.Items.Add(new PdfListFieldItem("-- Select --", "Select"));
            empType.Items.Add(new PdfListFieldItem("Full-Time", "FT"));
            empType.Items.Add(new PdfListFieldItem("Part-Time", "PT"));
            empType.Items.Add(new PdfListFieldItem("Contract", "C"));
            empType.Items.Add(new PdfListFieldItem("Temporary", "T"));
            empType.SelectedIndex = 0;
            document.Form.Fields.Add(empType);
            y2 += 40f;

            // Declaration & Signature
            g2.DrawLine(grayPen, new PointF(leftMargin, y2), new PointF(page2Size.Width - rightMargin, y2));
            y2 += 20f;
            RectangleF sectionRect = new RectangleF(leftMargin, y2, innerWidth, 20f);
            g2.DrawRectangle(tealBrush, sectionRect);
            g2.DrawString("Declaration & Signature", boldFont, whiteBrush, new PointF(leftMargin + 5f, y2 + 3f));
            y2 += 35f;

            RectangleF declRect = new RectangleF(leftMargin, y2, innerWidth, 55f);
            PdfSolidBrush lightBlue = new PdfSolidBrush(new PdfColor(220, 240, 255));
            g2.DrawRectangle(lightBlue, declRect);
            g2.DrawString(
                "I declare that all information provided in this application is true, accurate, and complete. I understand that any misrepresentation or omission of information may result in disqualification from consideration or, if employed, dismissal. I consent to the collection and processing of my personal data for recruitment purposes.",
                normalFont,
                blackBrush,
                new RectangleF(leftMargin + 5f, y2 + 5f, innerWidth - 10f, 70f));
            y2 += 75f;

            // Signature and Date
            g2.DrawString("Applicant Signature *", boldFont, blackBrush, new PointF(leftMargin, y2));
            PdfSignatureField signatureField = new PdfSignatureField(page2, "ApplicantSignature");
            signatureField.Bounds = new RectangleF(leftMargin, y2 + 15f, fieldWidth, 40f);
            signatureField.BorderColor = new PdfColor(System.Drawing.Color.Black);
            signatureField.BorderWidth = 1f;
            signatureField.BorderStyle = PdfBorderStyle.Solid;
            document.Form.Fields.Add(signatureField);

            g2.DrawString("Date *", boldFont, blackBrush, new PointF(leftMargin + fieldWidth + fieldSpacing, y2));
            PdfTextBoxField dateField = new PdfTextBoxField(page2, "SignatureDate");
            dateField.Bounds = new RectangleF(leftMargin + fieldWidth + fieldSpacing, y2 + 15f, fieldWidth, 40f);
            document.Form.Fields.Add(dateField);
            y2 += 75f;

            // Separator
            g2.DrawLine(grayPen, new PointF(leftMargin, y2), new PointF(page2Size.Width - rightMargin, y2));
            y2 += 20f;

            // Additional Terms & Conditions
            sectionRect = new RectangleF(leftMargin, y2, innerWidth, 20f);
            g2.DrawRectangle(tealBrush, sectionRect);
            g2.DrawString("Additional Terms & Conditions", boldFont, whiteBrush, new PointF(leftMargin + 5f, y2 + 3f));
            y2 += 40f;

            PdfCheckBoxField t1 = new PdfCheckBoxField(page2, "TermsAgree");
            t1.Bounds = new RectangleF(leftMargin, y2, 15f, 15f);
            document.Form.Fields.Add(t1);
            g2.DrawString("I agree to the terms and conditions of employment", normalFont, blackBrush, new PointF(leftMargin + 20f, y2));
            y2 += 35f;

            PdfCheckBoxField t2 = new PdfCheckBoxField(page2, "BackgroundCheck");
            t2.Bounds = new RectangleF(leftMargin, y2, 15f, 15f);
            document.Form.Fields.Add(t2);
            g2.DrawString("I authorize background check and reference verification", normalFont, blackBrush, new PointF(leftMargin + 20f, y2));
            y2 += 35f;

            PdfCheckBoxField t3 = new PdfCheckBoxField(page2, "PrivacyPolicy");
            t3.Bounds = new RectangleF(leftMargin, y2, 15f, 15f);
            document.Form.Fields.Add(t3);
            g2.DrawString("I have read and accepted the Privacy Policy", normalFont, blackBrush, new PointF(leftMargin + 20f, y2));
            y2 += 35f;

            // Submission info and button
            g2.DrawLine(grayPen, new PointF(leftMargin, y2), new PointF(page2Size.Width - rightMargin, y2));
            y2 += 20f;
            g2.DrawString("* Required field", normalFont, blackBrush, new PointF(leftMargin, y2));
            y2 += 35f;

            PdfButtonField submit = new PdfButtonField(page2, "Submit");
            submit.Bounds = new RectangleF(350f, y2 + 6f, 130f, 15f);
            submit.Font = boldFont;
            submit.Text = "Submit Application";
            submit.BackColor = new PdfColor(34, 139, 34);
            submit.ForeColor = new PdfColor(255, 255, 255);
            submit.BorderColor = new PdfColor(34, 139, 34);
            PdfJavaScriptAction javaAction = new PdfJavaScriptAction(
            "address = app.response(\"Enter an e-mail address.\",\"SEND E-MAIL\",\"\");"
            + "if (address)" +


            "{ " +
            "cmdLine = \"mailto:\" + address;" +

            "this.submitForm(cmdLine,true,false,\"Remarks\");" +

            "}");

            submit.Actions.GotFocus = javaAction;
            document.Form.Fields.Add(submit);
            document.Form.SetDefaultAppearance(false);
        }

        /// <summary>
        /// Creates first page in the document.
        /// </summary>
        private void CreateFirstPage()
        {
            // Create first page per the sample (Header, Personal Info, Education, Employment History)
            PdfPage page1 = document.Pages.Add();
            PdfGraphics g = page1.Graphics;
            SizeF page1Size = page1.GetClientSize();

            // Fonts
            PdfFont titleFont = new PdfStandardFont(PdfFontFamily.Helvetica, 17, PdfFontStyle.Regular);
            PdfFont subTitleFont = new PdfStandardFont(PdfFontFamily.Helvetica, 9, PdfFontStyle.Regular);
            PdfFont normalFont = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Regular);
            PdfFont boldFont = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);

            // Brushes and pens
            PdfSolidBrush tealBrush = new PdfSolidBrush(new PdfColor(0, 128, 128));
            PdfSolidBrush blackBrush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            PdfSolidBrush whiteBrush = new PdfSolidBrush(new PdfColor(255, 255, 255));
            PdfSolidBrush navyBrush = new PdfSolidBrush(new PdfColor(19, 43, 66));
            PdfPen navyPen = new PdfPen(new PdfColor(19, 43, 66), 1);
            PdfPen grayPen = new PdfPen(new PdfColor(102, 106, 109), 1);

            float y = 20f;
            float pageWidth = page1Size.Width;
            float leftMargin = 20f;
            float rightMargin = 20f;
            float innerWidth = pageWidth - leftMargin - rightMargin;
            float fieldSpacing = 10f;
            float fieldWidth = innerWidth / 2f - 5f;

            // Header section (rounded rectangle)
            RectangleF headerRect = new RectangleF(leftMargin, y, innerWidth, 55f);
            PdfPath headerPath = RoundedRect(headerRect, 5);
            g.DrawPath(navyPen, navyBrush, headerPath);
            y += 10f;
            g.DrawString("Job Application", titleFont, whiteBrush, new PointF(leftMargin + 10f, y));
            y += 25f;
            g.DrawString(
                "We look forward to receiving your application. please fill in all required fields.",
                subTitleFont,
                tealBrush,
                new RectangleF(leftMargin + 10f, y, innerWidth - 20f, 30f));
            y += 35f;

            // Personal Information section header
            RectangleF sectionRect = new RectangleF(leftMargin, y, innerWidth, 20f);
            g.DrawRectangle(tealBrush, sectionRect);
            g.DrawString("Personal Information", boldFont, whiteBrush, new PointF(leftMargin + 5f, y + 3f));
            y += 35f;

            // First Name
            g.DrawString("First Name *", normalFont, blackBrush, new PointF(leftMargin, y));
            RectangleF firstNameBounds = new RectangleF(leftMargin, y + 15f, fieldWidth, 20f);
            PdfTextBoxField firstNameField = new PdfTextBoxField(page1, "FirstName");
            firstNameField.Bounds = firstNameBounds;
            document.Form.Fields.Add(firstNameField);

            // Last Name
            g.DrawString("Last Name *", normalFont, blackBrush, new PointF(leftMargin + fieldWidth + fieldSpacing, y));
            RectangleF lastNameBounds = new RectangleF(leftMargin + fieldWidth + fieldSpacing, y + 15f, fieldWidth, 20f);
            PdfTextBoxField lastNameField = new PdfTextBoxField(page1, "LastName");
            lastNameField.Bounds = lastNameBounds;
            document.Form.Fields.Add(lastNameField);
            y += 50f;

            // Email Address and Phone
            g.DrawString("Email Address *", normalFont, blackBrush, new PointF(leftMargin, y));
            RectangleF emailBounds = new RectangleF(leftMargin, y + 15f, fieldWidth, 20f);
            PdfTextBoxField emailField = new PdfTextBoxField(page1, "Email");
            emailField.Bounds = emailBounds;
            document.Form.Fields.Add(emailField);

            g.DrawString("Business Phone", normalFont, blackBrush, new PointF(leftMargin + fieldWidth + fieldSpacing, y));
            RectangleF phoneBounds = new RectangleF(leftMargin + fieldWidth + fieldSpacing, y + 15f, fieldWidth, 20f);
            PdfTextBoxField phoneField = new PdfTextBoxField(page1, "Phone");
            phoneField.Bounds = phoneBounds;
            document.Form.Fields.Add(phoneField);
            y += 50f;

            // Position Applied For
            g.DrawString("Position Applied For *", normalFont, blackBrush, new PointF(leftMargin, y));
            RectangleF positionBounds = new RectangleF(leftMargin, y + 15f, fieldWidth, 20f);
            PdfComboBoxField positionField = new PdfComboBoxField(page1, "Position");
            positionField.Bounds = positionBounds;
            positionField.Items.Add(new PdfListFieldItem("-- Select a role --", "SR"));
            positionField.Items.Add(new PdfListFieldItem("Software Engineer", "SE"));
            positionField.Items.Add(new PdfListFieldItem("Senior Developer", "SD"));
            positionField.Items.Add(new PdfListFieldItem("Project Manager", "PM"));
            positionField.Items.Add(new PdfListFieldItem("QA Engineer", "QA"));
            positionField.SelectedIndex = 0;
            document.Form.Fields.Add(positionField);
            y += 55f;

            // Divider
            g.DrawLine(grayPen, new PointF(leftMargin, y), new PointF(pageWidth - rightMargin, y));
            y += 20f;

            // Education section
            sectionRect = new RectangleF(leftMargin, y, innerWidth, 20f);
            g.DrawRectangle(tealBrush, sectionRect);
            g.DrawString("Education", boldFont, whiteBrush, new PointF(leftMargin + 5f, y + 3f));
            y += 35f;

            g.DrawString(
                "Please select the highest level of education you have completed.",
                normalFont,
                blackBrush,
                new PointF(leftMargin, y));
            y += 30f;

            // Education checkboxes - first row
            float checkY = y;
            float x1 = leftMargin;

            PdfCheckBoxField chk = new PdfCheckBoxField(page1, "AssociateDegree");
            chk.Bounds = new RectangleF(x1, checkY, 15f, 15f);
            document.Form.Fields.Add(chk);
            g.DrawString("Associate Degree", normalFont, blackBrush, new PointF(x1 + 20f, checkY));

            x1 = leftMargin + 190f;
            chk = new PdfCheckBoxField(page1, "BachelorDegree");
            chk.Bounds = new RectangleF(x1, checkY, 15f, 15f);
            document.Form.Fields.Add(chk);
            g.DrawString("Bachelor's Degree", normalFont, blackBrush, new PointF(x1 + 20f, checkY));

            x1 = leftMargin + 370f;
            chk = new PdfCheckBoxField(page1, "CollegeDiploma");
            chk.Bounds = new RectangleF(x1, checkY, 15f, 15f);
            document.Form.Fields.Add(chk);
            g.DrawString("College / Diploma", normalFont, blackBrush, new PointF(x1 + 20f, checkY));
            y += 35f;

            // Education checkboxes - second row
            x1 = leftMargin;
            chk = new PdfCheckBoxField(page1, "Postgraduate");
            chk.Bounds = new RectangleF(x1, y, 15f, 15f);
            document.Form.Fields.Add(chk);
            g.DrawString("Postgraduate (PG)", normalFont, blackBrush, new PointF(x1 + 20f, y));

            x1 = leftMargin + 190f;
            chk = new PdfCheckBoxField(page1, "MBA");
            chk.Bounds = new RectangleF(x1, y, 15f, 15f);
            document.Form.Fields.Add(chk);
            g.DrawString("MBA", normalFont, blackBrush, new PointF(x1 + 20f, y));
            y += 35f;

            // Divider
            g.DrawLine(grayPen, new PointF(leftMargin, y), new PointF(pageWidth - rightMargin, y));
            y += 20f;

            // Employment History section
            sectionRect = new RectangleF(leftMargin, y, innerWidth, 20f);
            g.DrawRectangle(tealBrush, sectionRect);
            g.DrawString("Employment History", boldFont, whiteBrush, new PointF(leftMargin + 5f, y + 3f));
            y += 35f;

            g.DrawString("List your most recent position first.", normalFont, blackBrush, new PointF(leftMargin, y));
            y += 25f;

            // Currently Employed checkbox
            chk = new PdfCheckBoxField(page1, "CurrentlyEmployed");
            chk.Bounds = new RectangleF(leftMargin, y, 15f, 15f);
            document.Form.Fields.Add(chk);
            g.DrawString("I am currently employed at this company", normalFont, blackBrush, new PointF(leftMargin + 20f, y));
            y += 35f;

            // Job Title and Employer
            g.DrawString("Job Title *", normalFont, blackBrush, new PointF(leftMargin, y));
            PdfTextBoxField jobTitleField = new PdfTextBoxField(page1, "JobTitle");
            jobTitleField.Bounds = new RectangleF(leftMargin, y + 15f, fieldWidth, 20f);
            document.Form.Fields.Add(jobTitleField);

            g.DrawString("Employer *", normalFont, blackBrush, new PointF(leftMargin + fieldWidth + fieldSpacing, y));
            PdfTextBoxField employerField = new PdfTextBoxField(page1, "Employer");
            employerField.Bounds = new RectangleF(leftMargin + fieldWidth + fieldSpacing, y + 15f, fieldWidth, 20f);
            document.Form.Fields.Add(employerField);
            y += 50f;

            // Salary and Reason for Leaving
            g.DrawString("Annual Salary", normalFont, blackBrush, new PointF(leftMargin, y));
            PdfTextBoxField salaryField = new PdfTextBoxField(page1, "Salary");
            salaryField.Bounds = new RectangleF(leftMargin, y + 15f, fieldWidth, 20f);
            document.Form.Fields.Add(salaryField);

            g.DrawString("Reason for Leaving", normalFont, blackBrush, new PointF(leftMargin + fieldWidth + fieldSpacing, y));
            PdfTextBoxField reasonField = new PdfTextBoxField(page1, "ReasonForLeaving");
            reasonField.Bounds = new RectangleF(leftMargin + fieldWidth + fieldSpacing, y + 15f, fieldWidth, 20f);
            document.Form.Fields.Add(reasonField);
            y += 50f;

            // Duties (multiline)
            g.DrawString("Key Duties & Responsibilities *", normalFont, blackBrush, new PointF(leftMargin, y));
            PdfTextBoxField dutiesField = new PdfTextBoxField(page1, "Duties");
            dutiesField.Bounds = new RectangleF(leftMargin, y + 15f, innerWidth, 60f);
            dutiesField.Multiline = true;
            document.Form.Fields.Add(dutiesField);
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
        /// <summary>
        /// Draws a rounded rectangle path matching the requested bounds and radius.
        /// </summary>
        private PdfPath RoundedRect(RectangleF bounds, int radius)
        {
            int diameter = radius * 2;
            SizeF size = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(bounds.Location, size);
            PdfPath path = new PdfPath();
            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }
            // top-left arc
            path.AddArc(arc, 180, 90);
            // top-right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            // bottom-right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            // bottom-left arc
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            // close figure
            path.CloseFigure();
            return path;
        }

        #endregion
    }
}
