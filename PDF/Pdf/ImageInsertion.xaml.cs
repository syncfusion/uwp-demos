#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Reflection;
using Windows.UI;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Popups;
using Common;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>


    public sealed partial class ImageInsertion : UserControl
    {
        public ImageInsertion()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10, 10, 10, 10);
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

            Stream jpegImage = typeof(ImageInsertion).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Autumn Leaves.jpg");
            Stream pngImage = typeof(ImageInsertion).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.syncfusion_logo.png");
#if STORE_SB
            Stream tiffImage = typeof(ImageInsertion).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.image.tif");
#else
            Stream tiffImage = typeof(ImageInsertion).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.image.tiff");
#endif
            Stream gifImage = typeof(ImageInsertion).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Ani.gif");
          
          
            // Create a new instance of PdfDocument class.
            PdfDocument document = new PdfDocument();

            // Set font.
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 22, PdfFontStyle.Bold);

            // Create brush.
            PdfBrush textBrush = PdfBrushes.DarkBlue;

#region Jpeg image
            // Add a new section to the document.
            PdfSection section = document.Sections.Add();
            // Add a new page to the newly created section.
            PdfPage page = section.Pages.Add();
            PdfGraphics g = page.Graphics;

            g.DrawString("JPEG Image Drawing", font, textBrush, new PointF(10, 0));

            //Drawing a jpeg image 
            PdfImage image = new PdfBitmap(jpegImage);
            g.DrawImage(image, 0, 30);

#endregion

#region PNG Image
            page = section.Pages.Add();
            g = page.Graphics;
            g.DrawString("PNG Image drawing", font, textBrush, new PointF(10, 0));

            image = new PdfBitmap(pngImage);
            
			g.DrawImage(image, 0, 30);
#endregion

#region Multiframe Tiff
            PdfBitmap multiFrameImage = new PdfBitmap(tiffImage);

            int frameCount = multiFrameImage.FrameCount;

            for (int i = 0; i < frameCount; i++)
            {
                // Every frame in a new page.
                page = section.Pages.Add();
                section.PageSettings.Margins.All = 0;
                g = page.Graphics;

                // Set the acive frame.
                multiFrameImage.ActiveFrame = i;

                // Draw active frame.
                g.DrawImage(multiFrameImage,0,0,page.Size.Width,page.Size.Height);

                if (i == 0)
                {
#if STORE_SB
                    g.DrawString("Tiff Image", font, textBrush, new PointF(10, 10));
#else
                    g.DrawString("Multiframe Tiff Image", font, textBrush, new PointF(10, 10));
#endif
                }
            }
#endregion
#region Gif
            page = section.Pages.Add();
            g = page.Graphics;

            // Draw gif image.
            image = new PdfBitmap(gifImage);
            g.DrawImage(image, 0, 0, g.ClientSize.Width, image.Height);

            g.DrawString("Gif Image", font, textBrush, new PointF(10, 0));

#endregion

            MemoryStream stream = new MemoryStream();
            await document.SaveAsync(stream);
            document.Close(true);
            Save(stream, "Sample.pdf");
           
        }
#region Helper Methods
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
#endregion
    }
}
