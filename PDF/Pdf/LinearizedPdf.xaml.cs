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
using Syncfusion.Pdf;
using Common;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using Syncfusion.Pdf.Parsing;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LinearizedPdf : UserControl
    {
        private Stream selectedDocumentStream;
        private string selectedFileName = "Essential_Studio.pdf";

        public LinearizedPdf()
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

            // FILE SIZE LIMIT (10 MB)
            const long MAX_FILE_SIZE = 10 * 1024 * 1024;
            if (selectedDocumentStream != null && selectedDocumentStream.Length > MAX_FILE_SIZE)
            {
                await new MessageDialog(string.Format("The selected PDF file exceeds the maximum allowed size of 10 MB. File size: {0:F2} MB.", selectedDocumentStream.Length / (1024.0 * 1024.0)), "File too large").ShowAsync();
                return;
            }

            Stream pdfFile = selectedDocumentStream ?? typeof(XPSToPDF).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Essential_Studio.pdf");

            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(pdfFile);

            // Step 3: Apply linearization to the loaded document
            loadedDocument.FileStructure.IsLinearized = true;

            // Step 4: Save the linearized PDF
            MemoryStream linearizedStream = new MemoryStream();
            loadedDocument.Save(linearizedStream);

            // Close the loaded document
            loadedDocument.Close(true);

            // Save the linearized PDF to file
            Save(linearizedStream, selectedFileName);
        }

        private async void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.FileTypeFilter.Add(".pdf");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                selectedDocumentStream?.Dispose();
                Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                selectedDocumentStream = fileStream.AsStreamForRead();
                selectedFileName = file.Name;
                SourcePdf.Text = selectedFileName;
            }
        }

        async void Save(Stream stream, string filename)
        {
            stream.Position = 0;

            StorageFile stFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.DefaultFileExtension = ".pdf";
                savePicker.SuggestedFileName = Path.GetFileNameWithoutExtension(filename);
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
