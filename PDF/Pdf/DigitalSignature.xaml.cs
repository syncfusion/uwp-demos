using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DigitalSignature : UserControl
    {
        public DigitalSignature()
        {
            this.InitializeComponent();
        }
        Stream documentStream = null;
        Stream certificateStream = null;
        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            StorageFile stFile = null;
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.FileTypeFilter.Add(".pfx");

            stFile = await openPicker.PickSingleFileAsync();

            Certificate.Text = stFile.Name;
            if (stFile != null)
            {
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.ReadWrite);
                certificateStream = fileStream.AsStreamForWrite();
            }
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            StorageFile stFile = null;
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.FileTypeFilter.Add(".pdf");

            stFile = await openPicker.PickSingleFileAsync();

            SourcePDF.Text = stFile.Name;
            if (stFile != null)
            {
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.ReadWrite);
                documentStream = fileStream.AsStreamForWrite();
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
		   if(documentStream==null)
		   {
		     documentStream = typeof(DigitalSignature).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Essential_Studio.pdf");
		   }
		   
		   //Load the selected PDF document.
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(documentStream);                        

            //Get the .pfx certificate file stream.
            if(certificateStream==null)
                certificateStream = typeof(DigitalSignature).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.PDF.pfx");
            
            //Create PdfCertificate using certificate stream and password.
            PdfCertificate pdfCert = new PdfCertificate(certificateStream, txtOwnerPassword.Password);

            //Add certificate to document first page.
            PdfSignature signature = new PdfSignature(loadedDocument, loadedDocument.Pages[0], pdfCert, "Signature");

            signature.Bounds = new System.Drawing.RectangleF(5, 5, 300, 300);

            MemoryStream stream = new MemoryStream();
            loadedDocument.Save(stream);
            loadedDocument.Close(true);
            SaveFile(stream, "output.PDF");
        }
        async void SaveFile(Stream stream, string filename)
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
    }
}
