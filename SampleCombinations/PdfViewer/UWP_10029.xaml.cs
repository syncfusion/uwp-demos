#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UWP_10029 : Page
    {
        public UWP_10029()
        {
            this.InitializeComponent();
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var document = pdfViewer.LoadedDocument;
            var pdfSecurity = document.Security;

            pdfSecurity.Algorithm = Syncfusion.Pdf.Security.PdfEncryptionAlgorithm.AES;
            pdfSecurity.KeySize = Syncfusion.Pdf.Security.PdfEncryptionKeySize.Key256Bit;
            pdfSecurity.Permissions = Syncfusion.Pdf.Security.PdfPermissionsFlags.Default;

            pdfSecurity.UserPassword = "syncfusion";
            MemoryStream stream = new MemoryStream();
            await document.SaveAsync(stream);
            Save(stream, "outputPassword.pdf");
        }

        async void Save(Stream stream, string filename)
        {

            stream.Position = 0;

            FileSavePicker savePicker = new FileSavePicker();
            savePicker.DefaultFileExtension = ".pdf";
            savePicker.SuggestedFileName = filename;
            savePicker.FileTypeChoices.Add("Adobe PDF Document", new List<string>() { ".pdf" });
            StorageFile stFile = await savePicker.PickSaveFileAsync();
            if (stFile != null)
            {
                IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();
                MessageDialog msgDialog = new MessageDialog("File has been saved successfully.");
                IUICommand cmd = await msgDialog.ShowAsync();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream fileStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.UWP_10029.pdf");
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            PdfLoadedDocument ldoc = new PdfLoadedDocument(buffer);
            pdfViewer.LoadDocument(ldoc);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.ViewMode = PickerViewMode.List;
            picker.FileTypeFilter.Add(".pdf");
            var file = await picker.PickSingleFileAsync();
            if (file == null) return;
            var stream = await file.OpenAsync(FileAccessMode.Read);
            Stream fileStream = stream.AsStreamForRead();
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            PdfLoadedDocument ldoc = new PdfLoadedDocument(buffer, "syncfusion");
            pdfViewer.LoadDocument(ldoc);
        }
    }
}
