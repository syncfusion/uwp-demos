#region Copyright Syncfusion Inc. 2001 - 2017
// Copyright Syncfusion Inc. 2001 - 2017. All rights reserved.
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
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI;
using System.Reflection;
using Syncfusion.Pdf.Interactive;
using Windows.UI.Popups;
using Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AutoTag : UserControl
    {
        public AutoTag()
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

        private async void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            Stream image1 = typeof(ImageInsertion).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.AutoTag.jpg");
            Stream image2 = typeof(ImageInsertion).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.autotagSmall.jpg");
            PdfColor blackColor = new PdfColor(System.Drawing.Color.FromArgb(255, 0, 0, 0));


            #region content string
            string toc = "\r\n What can I do with C#? ..................................................................................................................................... 3 \r\n \r\n What is .NET? .................................................................................................................................................... 3 \r\n \r\n Writing, Running, and Deploying a C# Program .............................................................................................. 3 \r\n \r\n Starting a New Program..................................................................................................................................... 3";
            string Csharp = "Welcome to C# Succinctly. True to the Succinctly series concept, this book is very focused on a single topic: the C# programming language. I might briefly mention some technologies that you can write with C# or explain how a feature fits into those technologies, but the whole of this book is about helping you become familiar with C# syntax. \r\n \r\n In this chapter, Ill start with some introductory information and then jump straight into a simple C# program.";
            string whatToD0 = "C# is a general purpose, object-oriented, component-based programming language. As a general purpose language, you have a number of ways to apply C# to accomplish many different tasks. You can build web applications with ASP.NET, desktop applications with Windows Presentation Foundation (WPF), or build mobile applications for Windows Phone. Other applications include code that runs in the cloud via Windows Azure, and iOS, Android, and Windows Phone support with the Xamarin platform. There might be times when you need a different language, like C or C++, to communicate with hardware or real-time systems. However, from a general programming perspective, you can do a lot with C#.";
            string dotnet = "The runtime is more formally named the Common Language Runtime (CLR). Programming languages that target the CLR compile to an Intermediate Language (IL). The CLR itself is a virtual machine that runs IL and provides many services such as memory management, garbage collection, exception management, security, and more.\r\n \r\n The Framework Class Library (FCL) is a set of reusable code that provides both general services and technology-specific platforms. The general services include essential types such as collections, cryptography, networking, and more. In addition to general classes, the FCL includes technology-specific platforms like ASP.NET, WPF, web services, and more. The value the FCL offers is to have common components available for reuse, saving time and money without needing to write that code yourself. \r\n \r\n There is a huge ecosystem of open-source and commercial software that relies on and supports .NET. If you visit CodePlex, GitHub, or any other open-source code repository site, you will see a multitude of projects written in C#. Commercial offerings include tools and services that help you build code, manage systems, and offer applications. Syncfusion is part of this ecosystem, offering reusable components for many of the .NET technologies I have mentioned.";
            string prog = "The previous section described plenty of great things you can do with C#, but most of them are so detailed that they require their own book. To stay focused on the C# programming language, the code in this book will be for the console application. A console application runs on the command line, which you will learn about in this section. You can write your code with any editor, but this book uses Visual Studio.";

            #endregion

            //Create a new PDF document.

            PdfDocument document = new PdfDocument();
            if (WTPDF.IsChecked == true)
            {
                document = new PdfDocument(PdfConformanceLevel.Pdf_A4);
                document.FileStructure.Version = PdfVersion.Version2_0;

            }
            else if (PDF_UA_2.IsChecked == true)
            {
                document.FileStructure.Version = PdfVersion.Version2_0;
            }
            //Auto Tag the document 

            document.AutoTag = true;
            document.DocumentInformation.Title = "AutoTag";
            #region page1
            //Add a page to the document.

            PdfPage page1 = document.Pages.Add();

            //Load the image from the disk.

            PdfBitmap image = new PdfBitmap(image1);

            //Draw the image

            page1.Graphics.DrawImage(image, 0, 0, page1.GetClientSize().Width, page1.GetClientSize().Height - 20);

            #endregion

            #region page2
            PdfPage page2 = document.Pages.Add();
            Stream fontStream = typeof(Conformance).GetTypeInfo().Assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.Pdf.Pdf.Assets.arial.ttf");
            PdfFont fontnormal = new PdfTrueTypeFont(fontStream, 9);
            PdfFont fontTitle = new PdfTrueTypeFont(fontStream, 22, PdfFontStyle.Bold);
            PdfFont fontHead = new PdfTrueTypeFont(fontStream, 10, PdfFontStyle.Bold);
            PdfFont fontHead2 = new PdfTrueTypeFont(fontStream, 16, PdfFontStyle.Bold);

            page2.Graphics.DrawString("Table of Contents", fontTitle, PdfBrushes.Black, new PointF(300, 0));
            page2.Graphics.DrawLine(new PdfPen(PdfBrushes.Black, 0.5f), new PointF(0, 40), new PointF(page2.GetClientSize().Width, 40));

            page2.Graphics.DrawString("Chapter 1 Introducing C# and .NET .............................................................................................................. 3 \r\n ", fontHead, PdfBrushes.Black, new PointF(0, 60));
            page2.Graphics.DrawString(toc, fontnormal, PdfBrushes.Black, new PointF(0, 80));

            #endregion

            #region page3

            PdfPage page3 = document.Pages.Add();

            page3.Graphics.DrawString("C# Succinctly", new PdfTrueTypeFont(fontStream, 32, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(160, 0));

            page3.Graphics.DrawLine(PdfPens.Black, new PointF(0, 40), new PointF(page3.GetClientSize().Width, 40));

            page3.Graphics.DrawString("Chapter 1 Introducing C# and .NET", fontTitle, PdfBrushes.Black, new PointF(160, 60));


            PdfTextElement element1 = new PdfTextElement(Csharp, fontnormal);
            element1.Brush = new PdfSolidBrush(blackColor);
            element1.Draw(page3, new RectangleF(0, 100, page3.GetClientSize().Width, 80));

            page3.Graphics.DrawString("What can I do with C#?", fontHead2, PdfBrushes.Black, new PointF(0, 180));
            PdfTextElement element2 = new PdfTextElement(whatToD0, fontnormal);
            element2.Brush = new PdfSolidBrush(blackColor);
            element2.Draw(page3, new RectangleF(0, 210, page3.GetClientSize().Width, 80));


            page3.Graphics.DrawString("What is .Net", fontHead2, PdfBrushes.Black, new PointF(0, 300));
            PdfTextElement element3 = new PdfTextElement(dotnet, fontnormal);
            element3.Brush = new PdfSolidBrush(blackColor);
            element3.Draw(page3, new RectangleF(0, 330, page3.GetClientSize().Width, 180));


            page3.Graphics.DrawString("Writing, Running, and Deploying a C# Program", fontHead2, PdfBrushes.Black, new PointF(0, 520));
            PdfTextElement element4 = new PdfTextElement(prog, fontnormal);
            element4.Brush = new PdfSolidBrush(blackColor);
            element4.Draw(page3, new RectangleF(0, 550, page3.GetClientSize().Width, 60));

            PdfBitmap img = new PdfBitmap(image2);
            page3.Graphics.DrawImage(img, new PointF(0, 630));
            page3.Graphics.DrawString("Note: The code samples in this book can be downloaded at", fontnormal, PdfBrushes.DarkBlue, new PointF(20, 630));
            page3.Graphics.DrawString("https://bitbucket.org/syncfusiontech/c-succinctly", fontnormal, PdfBrushes.Blue, new PointF(20, 640));
            SizeF linkSize = fontnormal.MeasureString("https://bitbucket.org/syncfusiontech/c-succinctly");
            RectangleF rectangle = new RectangleF(20, 640, linkSize.Width, linkSize.Height);

            //Creates a new Uri Annotation

            PdfUriAnnotation uriAnnotation = new PdfUriAnnotation(rectangle, "https://bitbucket.org/syncfusiontech/c-succinctly");
            uriAnnotation.Color = new PdfColor(255, 255, 255);
            uriAnnotation.Text = "annotation";
            //Adds this annotation to a new page
            page3.Annotations.Add(uriAnnotation);

            #endregion

      

            //Save the document and dispose it.

            MemoryStream stream = new MemoryStream();
            await document.SaveAsync(stream);
            document.Close(true);
            string outputFileName = WTPDF.IsChecked == true ? "WTPDFSample.pdf" : "AutotagSample.pdf";
            Save(stream, outputFileName);
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
