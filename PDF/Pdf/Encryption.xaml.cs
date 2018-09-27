#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Security;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Encryption : UserControl
    {
        public Encryption()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10.0, 0, 0, 0);
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //}

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PdfDocument document = new PdfDocument();

            PdfPageBase page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 20f, PdfFontStyle.Bold);
            PdfBrush brush = PdfBrushes.Black;
            PdfForm form = document.Form;

            //Document security
            PdfSecurity security = document.Security;

            //Specify key size and encryption algorithm
            if (encryptionTechnique.SelectionBoxItem.ToString () == "40-Bit RC4")
            {
                //use 40 bits key in RC4 mode
                security.KeySize = PdfEncryptionKeySize.Key40Bit;
                security.Algorithm = PdfEncryptionAlgorithm.RC4;
            }
            else if (encryptionTechnique.SelectionBoxItem.ToString() == "128-Bit RC4")
            {
                //use 128 bits key in RC4 mode
                security.KeySize = PdfEncryptionKeySize.Key128Bit;
                security.Algorithm = PdfEncryptionAlgorithm.RC4;
            }
            else if (encryptionTechnique.SelectionBoxItem.ToString() == "128-Bit AES")
            {
                //use 128 bits key in RC4 mode
                security.KeySize = PdfEncryptionKeySize.Key128Bit;
                security.Algorithm = PdfEncryptionAlgorithm.AES;
            }
            else if (encryptionTechnique.SelectionBoxItem.ToString() == "256-Bit AES")
            {
                //use 256 bits key in AES mode
                security.KeySize = PdfEncryptionKeySize.Key256Bit;
                security.Algorithm = PdfEncryptionAlgorithm.AES;
            }
			else if (encryptionTechnique.SelectionBoxItem.ToString() == "256-Bit AES Revision 6")
            {
                //use 256 bits key in AES mode with Revision 6
                security.KeySize = PdfEncryptionKeySize.Key256BitRevision6;
                security.Algorithm = PdfEncryptionAlgorithm.AES;
            }
            security.OwnerPassword = txtOwnerPassword.Password;
            security.Permissions = PdfPermissionsFlags.Print | PdfPermissionsFlags.FullQualityPrint;
            security.UserPassword = txtUserPassword.Password;

            string text = "Security options:\n\n" + String.Format("KeySize: {0}\n\nEncryption Algorithm: {4}\n\nOwner Password: {1}\n\nPermissions: {2}\n\n" +
                "UserPassword: {3}", security.KeySize, security.OwnerPassword, security.Permissions, security.UserPassword, security.Algorithm);
			if (encryptionTechnique.SelectionBoxItem.ToString() == "256-Bit AES Revision 6")
			{
				text += String.Format("\n\nRevision: {0}", "Revision6");
			}
			else if (encryptionTechnique.SelectionBoxItem.ToString() == "256-Bit AES")
			{
				text += String.Format("\n\nRevision: {0}", "Revision5");
			}
				
            graphics.DrawString("Document is Encrypted with following settings", font, brush, PointF.Empty);
            font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16f, PdfFontStyle.Bold);
            graphics.DrawString(text, font, brush, new PointF(0, 40));

            MemoryStream stream = new MemoryStream();
            await document.SaveAsync(stream);
            document.Close(true);
            Save(stream, "FormFilling.pdf");
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
    }
}
