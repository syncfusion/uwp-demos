#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer
{
    public sealed partial class SignatureView : UserControl
    {
        internal UWP_9496 m_mainPage;
        PdfLoadedDocument m_loadedDocument;
        PdfLoadedDocument m_documentToBeSaved = null;
        InkDrawingAttributes m_drawingAttributes;
        int m_currentZoom;
        float m_horizontalOffset = 0;
        float m_verticalOffset = 0;
        Stream m_savedstream;
        string m_dateValue;
        public SignatureView()
        {
            this.InitializeComponent();

            Background = new SolidColorBrush(Color.FromArgb(55, 0, 0, 0));
            signatureCanvas.InkPresenter.InputDeviceTypes =
        CoreInputDeviceTypes.Mouse |
        CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Touch;

            // Set initial ink stroke attributes.
            m_drawingAttributes = new InkDrawingAttributes();
            m_drawingAttributes.Color = Colors.Red;
            m_drawingAttributes.IgnorePressure = false;
            m_drawingAttributes.FitToCurve = true;
            signatureCanvas.InkPresenter.UpdateDefaultDrawingAttributes(m_drawingAttributes);
            
        }

        List<Point> m_inkStrokes;
        bool m_isDataButtonClicked;
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
                m_inkStrokes = new List<Point>();
                var strokes = signatureCanvas.InkPresenter.StrokeContainer.GetStrokes();
                foreach (InkStroke stroke in strokes)
                {
                    var drawPoints = stroke.GetInkPoints();

                    foreach (InkPoint point in drawPoints)
                    {
                        m_inkStrokes.Add(point.Position);
                    }
                }
                if (m_inkStrokes.Count > 0)
                {
                    double lastY = m_inkStrokes[0].Y;
                    for (int i = 0; i < m_inkStrokes.Count; i++)
                    {
                        if (lastY > m_inkStrokes[i].Y)
                        {
                            lastY = m_inkStrokes[i].Y;
                        }
                    }

                }
            m_mainPage.m_pdfView.PagePointerPressed += PdfView_PagePointerPressed;

            this.Visibility = Visibility.Collapsed;
            dateTextBox.Text = "";
            m_isDataButtonClicked = false;
        }

        private async void PdfView_PagePointerPressed(object sender, Syncfusion.Windows.PdfViewer.PagePointerEventArgs e)
        {
            m_savedstream = new MemoryStream();
            m_currentZoom = m_mainPage.m_pdfView.Zoom;
            m_horizontalOffset = m_mainPage.m_pdfView.HorizontalOffset;
            m_verticalOffset = m_mainPage.m_pdfView.VerticalOffset;
            m_loadedDocument = m_mainPage.m_pdfView.LoadedDocument;
            m_documentToBeSaved = m_loadedDocument;
            float xOffset = 0;
            float yOffset = 0;
            float scaleX = 1;
            float scaleY = 1;
            PdfUnitConvertor converter = new PdfUnitConvertor();
            List<float> inkPoints = new List<float>();
            for (int l = 0; l < m_inkStrokes.Count; l++)
            {
                float inkpoint = converter.ConvertFromPixels((float)((m_inkStrokes[l].X + e.X) * scaleX) + xOffset, PdfGraphicsUnit.Point);
                if (m_loadedDocument.Pages[e.CurrentPageIndex].Rotation == PdfPageRotateAngle.RotateAngle90)
                    inkpoint = converter.ConvertFromPixels((float)((m_inkStrokes[l].Y + e.Y) * scaleY) + yOffset, PdfGraphicsUnit.Point);
                else if (m_loadedDocument.Pages[e.CurrentPageIndex].Rotation == PdfPageRotateAngle.RotateAngle180)
                    inkpoint = converter.ConvertFromPixels((float)((m_inkStrokes[l].X + e.X) * scaleX) + xOffset, PdfGraphicsUnit.Point);
                else if (m_loadedDocument.Pages[e.CurrentPageIndex].Rotation == PdfPageRotateAngle.RotateAngle270)
                    inkpoint = (float)m_documentToBeSaved.Pages[e.CurrentPageIndex].Size.Width - converter.ConvertFromPixels((float)((m_inkStrokes[l].Y + e.Y) * scaleY) + yOffset, PdfGraphicsUnit.Point);
                inkPoints.Add(inkpoint);
                inkpoint = (float)m_documentToBeSaved.Pages[e.CurrentPageIndex].Size.Height - converter.ConvertFromPixels((float)((m_inkStrokes[l].Y + e.Y) * scaleY + yOffset), PdfGraphicsUnit.Point);
                if (m_loadedDocument.Pages[e.CurrentPageIndex].Rotation == PdfPageRotateAngle.RotateAngle90)
                    inkpoint = (float)converter.ConvertFromPixels((float)((m_inkStrokes[l].X + e.X) * scaleX + xOffset), PdfGraphicsUnit.Point);
                else if (m_loadedDocument.Pages[e.CurrentPageIndex].Rotation == PdfPageRotateAngle.RotateAngle180)
                    inkpoint = (float)m_documentToBeSaved.Pages[e.CurrentPageIndex].Size.Height - converter.ConvertFromPixels((float)((m_inkStrokes[l].Y + e.Y) * scaleY + yOffset), PdfGraphicsUnit.Point);
                else if (m_loadedDocument.Pages[e.CurrentPageIndex].Rotation == PdfPageRotateAngle.RotateAngle270)
                    inkpoint = (float)m_documentToBeSaved.Pages[e.CurrentPageIndex].Size.Height - converter.ConvertFromPixels((float)((m_inkStrokes[l].X + e.X) * scaleX + xOffset), PdfGraphicsUnit.Point);

                inkPoints.Add(inkpoint);
            }
            System.Drawing.RectangleF rectangle = new System.Drawing.RectangleF((float)e.X, (float)e.Y, (float)100, (float)100);
            PdfInkAnnotation inkAnnotation = new PdfInkAnnotation(rectangle, inkPoints);
            SolidColorBrush brush = new SolidColorBrush(m_drawingAttributes.Color);
            inkAnnotation.Color = new PdfColor((brush).Color.R, (brush).Color.G, (brush).Color.B);
            inkAnnotation.BorderWidth = 2;
            inkAnnotation.Opacity = (float)((float)brush.Color.A / (float)255);
            m_documentToBeSaved.Pages[e.CurrentPageIndex].Annotations.Add(inkAnnotation);
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 15);
            if (inkPoints.Count > 0)
            {
                float lastY = inkPoints[1];
                //Find Y position for Date
                for (int i = 1; i < inkPoints.Count; i++)
                {
                    if (lastY > inkPoints[i])
                    {
                        lastY = inkPoints[i];
                    }
                    i++;
                }
                if (m_dateValue != null)
                {
                    //Draw date as text.
                    m_documentToBeSaved.Pages[e.CurrentPageIndex].Graphics.DrawString(m_dateValue, font, PdfBrushes.Red, new System.Drawing.PointF(rectangle.X, m_documentToBeSaved.Pages[e.CurrentPageIndex].Size.Height - (lastY)));
                }
            }
            m_mainPage.m_loadingIndicator.IsActive = true;
            m_documentToBeSaved.Save(m_savedstream);
            if (inkPoints != null && inkPoints.Count > 0)
                inkPoints.Clear();
            m_documentToBeSaved.Dispose();
            for (int i = 1; i == 1; i--) // Keep running progress Ring for 1 seconds  
            {
                await Task.Delay(100);  
            }
            m_savedstream.Position = 0;
            m_mainPage.m_pdfView.LoadDocument(m_savedstream);
            m_mainPage.m_pdfView.ZoomTo(m_currentZoom);
            m_mainPage.m_pdfView.GotoPage(e.CurrentPageIndex + 1);
            m_mainPage.m_pdfView.ScrollToHorizontalOffset(m_horizontalOffset);
            m_mainPage.m_pdfView.ScrollToVerticalOffset(m_verticalOffset);
            m_mainPage.m_loadingIndicator.IsActive = false;
            m_mainPage.m_pdfView.PagePointerPressed -= PdfView_PagePointerPressed;
            m_isDataButtonClicked = false;
            m_dateValue = "";
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            signatureCanvas.InkPresenter.StrokeContainer.Clear();
            dateTextBox.Text = string.Empty;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            signatureCanvas.InkPresenter.StrokeContainer.Clear();
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            if(!m_isDataButtonClicked)
            signatureCanvas.InkPresenter.StrokeContainer.Clear();
        }
        
        private void Date_Click(object sender, RoutedEventArgs e)
        {
            dateTextBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
            m_dateValue = dateTextBox.Text;
            dateTextBox.FontSize = 15;
            m_isDataButtonClicked = true;
        }
    }
}
