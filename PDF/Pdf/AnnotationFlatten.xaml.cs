#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
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
using Syncfusion.Pdf.Parsing;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>


    public sealed partial class AnnotationFlatten : UserControl
    {        
        public AnnotationFlatten()
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
            //Create a new PDF document
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            document.PageSettings.SetMargins(0);

            RectangleF bounds = new RectangleF(350, 50, 80, 80);
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 10f);
            PdfBrush brush = new PdfSolidBrush(System.Drawing.Color.FromArgb(255,0,0,0));
            PdfColor yellow = new PdfColor(System.Drawing.Color.FromArgb(255,255,255,0));
            PdfColor red= new PdfColor(System.Drawing.Color.FromArgb(255, 255, 0, 0));
            PdfColor green = new PdfColor(System.Drawing.Color.FromArgb(255, 0, 255, 0));
            PdfColor blue = new PdfColor(System.Drawing.Color.FromArgb(255, 0, 0, 255));

            //Creating circle annotation
            PdfCircleAnnotation circleannotation = new PdfCircleAnnotation(bounds);
            circleannotation.InnerColor = yellow;
            circleannotation.Color = red;
            circleannotation.AnnotationFlags = PdfAnnotationFlags.Default;
            circleannotation.Author = "Syncfusion";
            circleannotation.Subject = "CircleAnnotation";
            circleannotation.ModifiedDate = new DateTime(2015, 1, 18);
            page.Annotations.Add(circleannotation);
            page.Graphics.DrawString("Circle Annotation", font, brush, new PointF(350, 10));

            //Creating ellipse annotation
            PdfEllipseAnnotation ellipseannotation = new PdfEllipseAnnotation(new RectangleF(30, 30, 50, 100), "Ellipse Annotation");
            ellipseannotation.Color = yellow;
            ellipseannotation.InnerColor = red;
            page.Graphics.DrawString("Ellipse Annotation", font, brush, new PointF(30, 10));
            page.Annotations.Add(ellipseannotation);

            //Creating square annotation
            PdfSquareAnnotation squareannotation = new PdfSquareAnnotation(new RectangleF(30, 200, 80, 80));
            squareannotation.Text = "SquareAnnotation";
            squareannotation.InnerColor = red;
            squareannotation.Color = yellow;
            page.Graphics.DrawString("Square Annotation", font, brush, new PointF(30, 180));
            page.Annotations.Add(squareannotation);

            //Creating rectangle annotation
            RectangleF rectangle1 = new RectangleF(350, 220, 100, 50);
            PdfRectangleAnnotation rectangleannotation = new PdfRectangleAnnotation(rectangle1, "RectangleAnnotation");
            rectangleannotation.InnerColor = red;
            rectangleannotation.Color = yellow;
            page.Graphics.DrawString("Rectangle Annotation", font, brush, new PointF(350, 180));
            page.Annotations.Add(rectangleannotation);

            //Creating line annotation
            int[] points = new int[] { 400, 450, 550, 450 };
            PdfLineAnnotation lineAnnotation = new PdfLineAnnotation(points, "Line Annoation is the one of the annotation type...");
            lineAnnotation.Author = "Syncfusion";
            lineAnnotation.Subject = "LineAnnotation";
            lineAnnotation.ModifiedDate = new DateTime(2015, 1, 18);
            lineAnnotation.Text = "PdfLineAnnotation";
            lineAnnotation.BackColor = red;
            page.Graphics.DrawString("Line Annotation", font, brush, new PointF(400, 320));
            page.Annotations.Add(lineAnnotation);

            //Creating polygon annotation
            int[] points1 = new int[] { 50, 398, 100, 425, 200, 455, 300, 330, 180, 330 };
            PdfPolygonAnnotation polygonannotation = new PdfPolygonAnnotation(points1, "Polygon Annotation");
            polygonannotation.Bounds = new RectangleF(30, 350, 300, 200);
            PdfPen pen = new PdfPen(System.Drawing.Color.FromArgb(255,255,0,0));
            polygonannotation.Text = "polygon";
            polygonannotation.Color = red;
            polygonannotation.InnerColor = yellow;
            page.Graphics.DrawString("PolygonAnnotation", font, brush, new PointF(50, 320));
            page.Annotations.Add(polygonannotation);


            //Creating text markup annotation
            string s = "This is TextMarkup annotation!!!";
            RectangleF Textmarkuprect = new RectangleF(30, 540, 200, 20);
            page.Graphics.DrawString(s, font, brush, new PointF(30, 540));
            PdfTextMarkupAnnotation textannot = new PdfTextMarkupAnnotation("sample", "Highlight", s, new PointF(70, 580), font);
            textannot.Author = "Annotation";
            textannot.Opacity = 1.0f;
            textannot.Subject = "pdftextmarkupannotation";
            textannot.ModifiedDate = new DateTime(2015, 1, 18);
            textannot.TextMarkupAnnotationType = PdfTextMarkupAnnotationType.StrikeOut;
            textannot.TextMarkupColor = yellow;
            textannot.InnerColor = red;
            textannot.Color = yellow;
            page.Graphics.DrawString("Text Markup Annotation", font, brush, new PointF(30, 510));
            page.Annotations.Add(textannot);

            //Creates a new Ink annotation.
            float[] inkPoints = new float[] { 72.919f, 136.376f, 72.264f, 136.376f, 62.446f, 142.922f, 61.137f, 142.922f, 55.901f, 139.649f, 55.246f, 138.34f, 54.592f, 132.449f, 54.592f, 127.867f, 55.901f, 125.904f, 59.828f, 121.976f, 63.101f, 121.322f, 65.719f, 122.631f, 68.992f, 125.249f, 70.301f, 130.485f, 71.61f, 133.104f, 72.264f, 136.376f, 72.919f, 140.304f, 74.883f, 144.885f, 76.192f, 150.776f, 76.192f, 151.431f, 76.192f, 152.085f, 76.192f, 158.631f, 76.192f, 159.94f, 75.537f, 155.358f, 74.228f, 150.122f, 74.228f, 146.195f, 73.574f, 141.613f, 73.574f, 137.685f, 74.228f, 132.449f, 74.883f, 128.522f, 75.537f, 124.594f, 76.192f, 123.285f, 76.846f, 122.631f, 80.774f, 122.631f, 82.737f, 123.285f, 85.355f, 125.249f, 88.628f, 129.831f, 89.283f, 133.104f, 89.937f, 137.031f, 90.592f, 140.958f, 89.937f, 142.267f, 86.665f, 141.613f, 85.355f, 140.304f, 84.701f, 138.34f, 84.701f, 137.685f, 85.355f, 137.031f, 87.974f, 135.722f, 90.592f, 136.376f, 92.555f, 137.031f, 96.483f, 139.649f, 98.446f, 140.958f, 101.719f, 142.922f, 103.028f, 142.922f, 100.41f, 138.34f, 99.756f, 134.413f, 99.101f, 131.14f, 99.101f, 128.522f, 99.756f, 127.213f, 101.065f, 125.904f, 102.374f, 123.94f, 103.683f, 123.94f, 107.61f, 125.904f, 110.228f, 129.831f, 114.156f, 135.067f, 117.428f, 140.304f, 119.392f, 143.576f, 121.356f, 144.231f, 122.665f, 144.231f, 123.974f, 142.267f, 126.592f, 139.649f, 127.247f, 140.304f, 126.592f, 142.922f, 124.628f, 143.576f, 122.01f, 142.922f, 118.083f, 141.613f, 114.81f, 136.376f, 114.81f, 131.14f, 113.501f, 127.213f, 114.156f, 125.904f, 118.083f, 125.904f, 120.701f, 126.558f, 123.319f, 130.485f, 125.283f, 136.376f, 125.937f, 140.304f, 125.937f, 142.922f, 126.592f, 143.576f, 125.937f, 135.722f, 125.937f, 131.794f, 125.937f, 131.14f, 127.247f, 129.176f, 129.21f, 127.213f, 131.828f, 127.213f, 134.447f, 128.522f, 136.41f, 136.376f, 139.028f, 150.122f, 141.647f, 162.558f, 140.992f, 163.213f, 138.374f, 160.595f, 135.756f, 153.395f, 135.101f, 148.158f, 134.447f, 140.304f, 134.447f, 130.485f, 133.792f, 124.594f, 133.792f, 115.431f, 133.792f, 110.194f, 133.792f, 105.612f, 134.447f, 105.612f, 137.065f, 110.194f, 137.719f, 116.74f, 139.028f, 120.013f, 139.028f, 123.94f, 137.719f, 127.213f, 135.756f, 130.485f, 134.447f, 130.485f, 133.792f, 130.485f, 137.719f, 131.794f, 141.647f, 135.722f, 146.883f, 142.922f, 152.774f, 153.395f, 153.428f, 159.286f, 150.156f, 159.94f, 147.537f, 156.667f, 146.883f, 148.813f, 146.883f, 140.958f, 146.883f, 134.413f, 146.883f, 125.904f, 145.574f, 118.703f, 145.574f, 114.776f, 145.574f, 112.158f, 146.228f, 111.503f, 147.537f, 111.503f, 148.192f, 112.158f, 150.156f, 112.812f, 150.81f, 113.467f, 152.119f, 114.776f, 154.083f, 117.394f, 155.392f, 119.358f, 156.701f, 120.667f, 157.356f, 121.976f, 156.701f, 121.322f, 156.047f, 120.013f, 155.392f, 119.358f, 154.083f, 117.394f, 154.083f, 116.74f, 152.774f, 114.776f, 152.119f, 114.121f, 150.81f, 113.467f, 149.501f, 113.467f, 147.537f, 112.158f, 146.883f, 112.158f, 145.574f, 111.503f, 144.919f, 112.158f, 144.265f, 114.121f, 144.265f, 115.431f, 144.265f, 116.74f, 144.265f, 117.394f, 144.265f, 118.049f, 144.919f, 118.703f, 145.574f, 120.667f, 146.228f, 122.631f, 147.537f, 123.285f, 147.537f, 124.594f, 148.192f, 125.904f, 147.537f, 128.522f, 147.537f, 129.176f, 147.537f, 130.485f, 147.537f, 132.449f, 147.537f, 134.413f, 147.537f, 136.376f, 147.537f, 138.34f, 147.537f, 138.994f, 145.574f, 138.994f, 142.956f, 138.252f };
            List<float> linePoints = new List<float>(inkPoints);
            RectangleF rectangle = new RectangleF(30, 580, 300, 400);
            PdfInkAnnotation inkAnnotation = new PdfInkAnnotation(rectangle, linePoints);
            inkAnnotation.Bounds = rectangle;
            inkAnnotation.Color = red;
            page.Graphics.DrawString("Ink Annotation", font, brush, new PointF(30, 610));
            page.Annotations.Add(inkAnnotation);

            //Creates a new popup annotation.
            RectangleF popupRect = new RectangleF(430, 670, 30, 30);
            PdfPopupAnnotation popupAnnotation = new PdfPopupAnnotation();
            popupAnnotation.Border.Width = 4;
            popupAnnotation.Border.HorizontalRadius = 20;
            popupAnnotation.Border.VerticalRadius = 30;
            popupAnnotation.Opacity = 1;
            popupAnnotation.Open = true;
            popupAnnotation.Text = "Popup Annotation";
            popupAnnotation.Color = green;
            popupAnnotation.InnerColor = blue;
            popupAnnotation.Bounds = popupRect;
            if (flatten.IsChecked == true)
            {
                popupAnnotation.FlattenPopUps = true;
                popupAnnotation.Flatten = true;
            }
            page.Graphics.DrawString("Popup Annotation", font, brush, new PointF(410, 610));
            page.Annotations.Add(popupAnnotation);

            //Saving the document
            MemoryStream SourceStream = new MemoryStream();
            document.Save(SourceStream);
            document.Close(true);

            //Loading and flatten the  annotation
            PdfLoadedDocument lDoc = new PdfLoadedDocument(SourceStream);
            PdfLoadedPage lpage = lDoc.Pages[0] as PdfLoadedPage;
            if(flatten.IsChecked==true)
            lpage.Annotations.Flatten = true;
            MemoryStream stream = new MemoryStream();
            await lDoc.SaveAsync(stream);
            lDoc.Close(true);

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
#endregion
    }
}
