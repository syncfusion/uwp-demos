#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
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
    public sealed partial class StampDocument : UserControl
    {
        public StampDocument()
        {
            this.InitializeComponent();
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                this.grdMain.Margin = new Thickness(10, 10, 10, 10);
                this.grdMain.Padding = new Thickness(10, 0, 10, 0);
            }
        }      

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Stream docStream = typeof(StampDocument).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Essential_Studio.pdf");
            PdfLoadedDocument ldoc = new PdfLoadedDocument(docStream);
          
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 36f);
            foreach (PdfPageBase lPage in ldoc.Pages)
            {
                PdfGraphics g = lPage.Graphics;
                PdfGraphicsState state = g.Save();
                g.SetTransparency(0.25f);
                g.RotateTransform(-40);
                g.DrawString(stampText.Text, font, PdfPens.Red, PdfBrushes.Red, new PointF(-150, 450));

                if (imagewatermark.IsChecked.Value)
                {
                    Stream imagestream = typeof(StampDocument).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.Ani.gif");
                    PdfImage image = new PdfBitmap(imagestream);
                    g.Restore(state);
                    g.SetTransparency(0.25f);
                    g.DrawImage(image, 0, 0, lPage.Graphics.ClientSize.Width, lPage.Graphics.ClientSize.Height);
                    imagestream.Dispose();
                }
            }
            MemoryStream stream = new MemoryStream();
            await ldoc.SaveAsync(stream);
            ldoc.Close(true);
            Save(stream, "StampDocument.pdf");        
          
            docStream.Dispose();
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
