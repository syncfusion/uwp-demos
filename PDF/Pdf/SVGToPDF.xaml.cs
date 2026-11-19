#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using Common;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SVGToPDF : UserControl
    {
        public SVGToPDF()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10, 10, 10, 10);
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
            }
        }

        private Stream svgStream = null;

        private async void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.FileTypeFilter.Add(".svg");

            StorageFile stFile = await openPicker.PickSingleFileAsync();

            if (stFile != null)
            {
                var basicProperties = await stFile.GetBasicPropertiesAsync();
                if (basicProperties.Size > 10 * 1024 * 1024)
                {
                    MessageDialog messageDialog = new MessageDialog("The selected file exceeds the 10MB size limit. Please choose a file smaller than 10MB.", "File Size Limit Exceeded");
                    await messageDialog.ShowAsync();
                    return;
                }

                SourceSVG.Text = stFile.Name;
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.Read);
                svgStream = fileStream.AsStreamForRead();
            }
        }
       
        private async void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            Stream svgFile = svgStream;
            if (svgFile == null)
            {
                svgFile = typeof(SVGToPDF).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.SVGtoPDF.svg");
            }
            else
            {
                svgFile.Position = 0;
            }

            //Create SVG to PDF converter
            SvgConverter converter = new SvgConverter();

            //Convert the SVG document to PDF
            PdfTemplate temp = converter.Convert(svgFile);
            PdfDocument document = new PdfDocument();
            document.PageSettings.Margins.All = 0;
            document.PageSettings.Size = new SizeF(temp.Width, temp.Height);

            PdfPage page = document.Pages.Add();
            page.Graphics.DrawPdfTemplate(temp, new PointF(0, 0), new SizeF(temp.Width, temp.Height));

            //Save the PDF document
            MemoryStream stream = new MemoryStream();
            await document.SaveAsync(stream);

            //Close the PDF document
            document.Close(true);

            Save(stream, "SVGToPDF.pdf");
        }
        async void Save(Stream stream, string filename)
        {

            stream.Position = 0;

            StorageFile stFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.DefaultFileExtension = ".pdf";
                savePicker.SuggestedFileName = "SVGToPDF";
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
