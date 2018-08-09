#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Reflection;
using Windows.UI.Popups;
using Common;
using System.Drawing;
using Syncfusion.Pdf.Xfa;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class XFAFormFilling : UserControl
    {
        public XFAFormFilling()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10, 10, 10, 10);
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
            }
        }

        private async void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            Stream file = typeof(XFAFormFilling).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.xfaTemplate.pdf");

            //Load the existing XFA document.
            PdfLoadedXfaDocument ldoc = new PdfLoadedXfaDocument(file);

            //Loaded the existing XFA form.
            PdfLoadedXfaForm lform = ldoc.XfaForm;

            //Fill the XFA form fields.
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["title[0]"] as PdfLoadedXfaComboBoxField).SelectedIndex = 0;
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["fn[0]"] as PdfLoadedXfaTextBoxField).Text = "Simons";
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["ln[0]"] as PdfLoadedXfaTextBoxField).Text = "Bistro";
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["dob[0]"] as PdfLoadedXfaDateTimeField).Value = new DateTime(1989, 5, 21);
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["company[0]"] as PdfLoadedXfaTextBoxField).Text = "XYZ Pvt Ltd";
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["jt[0]"] as PdfLoadedXfaTextBoxField).Text = "Developer";
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["jd[0]"] as PdfLoadedXfaTextBoxField).Text = "Develop and maintain components and applications for the web, desktop and mobile platforms. \nWork with some of the best UI/UX designers in the world to craft Stunning user interfaces. \nRegular appraisals to ensure quick growth if you deliver on the job.";
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["st[0]"] as PdfLoadedXfaTextBoxField).Text = "Vinbaeltet 34";
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["ad1[0]"] as PdfLoadedXfaTextBoxField).Text = "Kobenhaven";
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["city[0]"] as PdfLoadedXfaTextBoxField).Text = "Denmark";
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["state[0]"] as PdfLoadedXfaComboBoxField).SelectedIndex = 1;
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["zip[0]"] as PdfLoadedXfaNumericField).NumericValue = 24534;
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["country[0]"] as PdfLoadedXfaTextBoxField).Text = "US";
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["em[0]"] as PdfLoadedXfaTextBoxField).Text = "simonsbistro@outlook.com";
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["phone[0]"] as PdfLoadedXfaNumericField).NumericValue = 8765456789;
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["sdn[0]"] as PdfLoadedXfaListBoxField).SelectedItems = new string[] { "Vegan", "Diary Free" };
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["san[0]"] as PdfLoadedXfaListBoxField).SelectedIndex = 0;
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["email[0]"] as PdfLoadedXfaCheckBoxField).IsChecked = true;
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["phone[1]"] as PdfLoadedXfaCheckBoxField).IsChecked = true;
            (((lform.Fields["subform1[0]"] as PdfLoadedXfaForm).Fields["subform3[0]"] as PdfLoadedXfaForm).Fields["group1[0]"] as PdfLoadedXfaRadioButtonGroup).Fields[1].IsChecked = true;

            MemoryStream ms = new MemoryStream();

            ldoc.Save(ms);

            ldoc.Close();

            Save(ms, "XFAForm.pdf");
        }
        async void Save(Stream stream, string filename)
        {
            stream.Position = 0;

            StorageFile stFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.DefaultFileExtension = ".pdf";
                savePicker.SuggestedFileName = "XFAForm";
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
    }
}
