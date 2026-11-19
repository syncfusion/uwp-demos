#region Copyright Syncfusion Inc. 2001 - 2016
// Copyright Syncfusion Inc. 2001 - 2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Presentation;
using System;
using Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Reflection;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialPresentation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SlidesPresentation : SampleLayout
    {
        #region Fields
        Assembly execAssm = typeof(Common.MainPage).GetTypeInfo().Assembly;
        #endregion

        public SlidesPresentation()
        {
            this.InitializeComponent();
        }

        public override void Dispose()
        {
            base.Dispose();

            DisposeTextBlock(ref emptyTextBlock1);
            emptyTextBlock1 = null;

            DisposeTextBlock(ref description);
            description = null;

            DisposeTextBlock(ref emptyTextBlock2);
            emptyTextBlock2 = null;

            DisposeTextBlock(ref btn_Description);
            btn_Description = null;

            DisposeTextBlock(ref emptyTextBlock3);
            emptyTextBlock3 = null;

            DisposeTextBlock(ref emptyTextBlock4);
            emptyTextBlock4 = null;

            Generate.Click -= Button_Click_1;
            DisposeButton(ref Generate);
            Generate = null;

            DisposeTextBlock(ref emptyTextBlock5);
            emptyTextBlock5 = null;
        }

        private void DisposeTextBlock(ref TextBlock textBlock)
        {
            //Clear the dependency properties of TextBlock
            textBlock.ClearValue(TextBlock.TextProperty);
            textBlock.ClearValue(TextBlock.TextWrappingProperty);
            textBlock.ClearValue(TextBlock.FontFamilyProperty);
            textBlock.ClearValue(TextBlock.FontSizeProperty);
            textBlock.ClearValue(TextBlock.ForegroundProperty);
            textBlock.ClearValue(TextBlock.VisibilityProperty);
        }

        private void DisposeButton(ref Button button)
        {
            //Clear the dependency properties of Button
            button.ClearValue(Button.ContentProperty);
            button.ClearValue(Button.PaddingProperty);
            button.ClearValue(Button.FontFamilyProperty);
            button.ClearValue(Button.FontSizeProperty);
            button.ClearValue(Button.ForegroundProperty);
            button.ClearValue(Button.BackgroundProperty);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(SlidesPresentation).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.Slides.pptx";
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);

            IPresentation presentation = await Presentation.OpenAsync(fileStream);


            #region Slide1
            ISlide slide1 = presentation.Slides[0];
            IShape shape1 = slide1.Shapes[0] as IShape;
            shape1.Left = 1.5 * 72;
            shape1.Top = 1.94 * 72;
            shape1.Width = 10.32 * 72;
            shape1.Height = 2 * 72;

            ITextBody textFrame1 = shape1.TextBody;
            IParagraphs paragraphs1 = textFrame1.Paragraphs;
            IParagraph paragraph1 = paragraphs1.Add();
            ITextPart textPart1 = paragraph1.AddTextPart();
            paragraphs1[0].IndentLevelNumber = 0;
            textPart1.Text = "ESSENTIAL PRESENTATION";
            textPart1.Font.FontName = "HelveticaNeue LT 65 Medium";
            textPart1.Font.FontSize = 48;
            textPart1.Font.Bold = true;
            slide1.Shapes.RemoveAt(1);
            #endregion

            #region Slide2
            ISlide slide2 = presentation.Slides.Add(SlideLayoutType.SectionHeader);
            shape1 = slide2.Shapes[0] as IShape;
            shape1.Left = 0.77 * 72;
            shape1.Top = 0.32 * 72;
            shape1.Width = 7.96 * 72;
            shape1.Height = 0.99 * 72;

            textFrame1 = shape1.TextBody;

            //Instance to hold paragraphs in textframe
            paragraphs1 = textFrame1.Paragraphs;
            paragraph1 = paragraphs1.Add();
            ITextPart textpart1 = paragraph1.AddTextPart();
            paragraphs1[0].HorizontalAlignment = HorizontalAlignmentType.Left;
            textpart1.Text = "Slide with simple text";
            textpart1.Font.FontName = "Helvetica CE 35 Thin";
            textpart1.Font.FontSize = 40;

            IShape shape2 = slide2.Shapes[1] as IShape;
            shape2.Left = 1.21 * 72;
            shape2.Top = 1.66 * 72;
            shape2.Width = 10.08 * 72;
            shape2.Height = 4.93 * 72;

            ITextBody textFrame2 = shape2.TextBody;

            //Instance to hold paragraphs in textframe
            IParagraphs paragraphs2 = textFrame2.Paragraphs;
            IParagraph paragraph2 = paragraphs2.Add();
            paragraph2.HorizontalAlignment = HorizontalAlignmentType.Left;
            ITextPart textpart2 = paragraph2.AddTextPart();
            textpart2.Text = "Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.";
            textpart2.Font.FontName = "Calibri (Body)";
            textpart2.Font.FontSize = 15;
            textpart2.Font.Color = ColorObject.Black;

            //Instance to hold paragraphs in textframe
            IParagraph paragraph3 = paragraphs2.Add();
            paragraph3.HorizontalAlignment = HorizontalAlignmentType.Left;
            textpart1 = paragraph3.AddTextPart();
            textpart1.Text = "Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.";
            textpart1.Font.FontName = "Calibri (Body)";
            textpart1.Font.FontSize = 15;
            textpart1.Font.Color = ColorObject.Black;

            //Instance to hold paragraphs in textframe
            IParagraph paragraph4 = paragraphs2.Add();
            paragraph4.HorizontalAlignment = HorizontalAlignmentType.Left;
            textpart1 = paragraph4.AddTextPart();
            textpart1.Text = "Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula.";
            textpart1.Font.FontName = "Calibri (Body)";
            textpart1.Font.FontSize = 15;
            textpart1.Font.Color = ColorObject.Black;

            //Instance to hold paragraphs in textframe
            IParagraph paragraph5 = paragraphs2.Add();
            paragraph5.HorizontalAlignment = HorizontalAlignmentType.Left;
            textpart1 = paragraph5.AddTextPart();
            textpart1.Text = "Vestibulum duis integer diam mi libero felis, sollicitudin id dictum etiam blandit lacus, ac condimentum magna dictumst interdum et,";
            textpart1.Font.FontName = "Calibri (Body)";
            textpart1.Font.FontSize = 15;
            textpart1.Font.Color = ColorObject.Black;

            //Instance to hold paragraphs in textframe
            IParagraph paragraph6 = paragraphs2.Add();
            paragraph6.HorizontalAlignment = HorizontalAlignmentType.Left;
            ITextPart textpart3 = paragraph6.AddTextPart();
            textpart1.Text = "nam commodo mi habitasse enim fringilla nunc, amet aliquam sapien per tortor luctus. Conubia voluptates at nunc, congue lectus, malesuada nulla.";
            textpart1.Font.Color = ColorObject.White;
            textpart1.Font.FontName = "Calibri (Body)";
            textpart1.Font.FontSize = 15;
            textpart1.Font.Color = ColorObject.Black;

            //Instance to hold paragraphs in textframe
            IParagraph paragraph7 = paragraphs2.Add();
            paragraph7.HorizontalAlignment = HorizontalAlignmentType.Left;
            textpart1 = paragraph7.AddTextPart();
            textpart1.Text = "Rutrum quo morbi, feugiat sed mi turpis, ac cursus integer ornare dolor. Purus dui in et tincidunt, sed eros pede adipiscing tellus, est suscipit nulla,";
            textpart1.Font.FontName = "Calibri (Body)";
            textpart1.Font.FontSize = 15;
            textpart1.Font.Color = ColorObject.Black;

            //Instance to hold paragraphs in textframe
            IParagraph paragraph8 = paragraphs2.Add();
            paragraph8.HorizontalAlignment = HorizontalAlignmentType.Left;
            textpart1 = paragraph8.AddTextPart();
            textpart1.Text = "Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula.";
            textpart1.Font.FontName = "Calibri (Body)";
            textpart1.Font.FontSize = 15;
            textpart1.Font.Color = ColorObject.Black;

            //Instance to hold paragraphs in textframe
            IParagraph paragraph9 = paragraphs2.Add();
            paragraph9.HorizontalAlignment = HorizontalAlignmentType.Left;
            textpart1 = paragraph9.AddTextPart();
            textpart1.Text = "arcu nec fringilla vel aliquam, mollis lorem rerum hac vestibulum ante nullam. Volutpat a lectus, lorem pulvinar quis. Lobortis vehicula in imperdiet orci urna.";
            textpart1.Font.FontName = "Calibri (Body)";
            textpart1.Font.Color = ColorObject.Black;
            textpart1.Font.FontSize = 15;
            #endregion

            #region Slide3
            slide2 = presentation.Slides.Add(SlideLayoutType.TwoContent);
            shape1 = slide2.Shapes[0] as IShape;
            shape1.Left = 0.36 * 72;
            shape1.Top = 0.51 * 72;
            shape1.Width = 11.32 * 72;
            shape1.Height = 1.06 * 72;

            //Adds textframe in shape
            textFrame1 = shape1.TextBody;

            //Instance to hold paragraphs in textframe
            paragraphs1 = textFrame1.Paragraphs;
            paragraphs1.Add();
            paragraph1 = paragraphs1[0];
            textpart1 = paragraph1.AddTextPart();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Left;

            //Assigns value to textpart
            textpart1.Text = "Slide with Image";
            textpart1.Font.FontName = "Helvetica CE 35 Thin";

            //Adds shape in slide
            shape2 = slide2.Shapes[1] as IShape;
            shape2.Left = 8.03 * 72;
            shape2.Top = 1.96 * 72;
            shape2.Width = 4.39 * 72;
            shape2.Height = 4.53 * 72;
            textFrame2 = shape2.TextBody;

            //Instance to hold paragraphs in textframe
            paragraphs2 = textFrame2.Paragraphs;
            paragraph2 = paragraphs2.Add();
            textpart2 = paragraph2.AddTextPart();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Left;

            textpart2.Text = "Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.";
            textpart2.Font.FontName = "Helvetica CE 35 Thin";
            textpart2.Font.FontSize = 16;

            paragraph3 = paragraphs2.Add();
            textpart2 = paragraph3.AddTextPart();
            paragraph3.HorizontalAlignment = HorizontalAlignmentType.Left;

            textpart2.Text = "Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.";
            textpart2.Font.FontName = "Helvetica CE 35 Thin";
            textpart2.Font.FontSize = 16;


            paragraph4 = paragraphs2.Add();
            textpart2 = paragraph4.AddTextPart();
            paragraph4.HorizontalAlignment = HorizontalAlignmentType.Left;

            textpart2.Text = "Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula.";
            textpart2.Font.FontName = "Helvetica CE 35 Thin";
            textpart2.Font.FontSize = 16;

            IShape shape3 = (IShape)slide2.Shapes[2];
            slide2.Shapes.RemoveAt(2);

            //Adds picture in the shape
            resourcePath = "Syncfusion.SampleBrowser.UWP.Presentation.Presentation.Assets.tablet.jpg";
            fileStream = assembly.GetManifestResourceStream(resourcePath);
            IPicture picture1 = slide2.Shapes.AddPicture(fileStream, 0.81 * 72, 1.96 * 72, 6.63 * 72, 4.43 * 72);
            fileStream.Dispose();
            #endregion

            #region Slide4
            ISlide slide4 = presentation.Slides.Add(SlideLayoutType.TwoContent);
            shape1 = slide4.Shapes[0] as IShape;
            shape1.Left = 0.51 * 72;
            shape1.Top = 0.34 * 72;
            shape1.Width = 11.32 * 72;
            shape1.Height = 1.06 * 72;

            textFrame1 = shape1.TextBody;

            //Instance to hold paragraphs in textframe
            paragraphs1 = textFrame1.Paragraphs;
            paragraphs1.Add();
            paragraph1 = paragraphs1[0];
            textpart1 = paragraph1.AddTextPart();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Left;

            //Assigns value to textpart
            textpart1.Text = "Slide with Table";
            textpart1.Font.FontName = "Helvetica CE 35 Thin";

            shape2 = slide4.Shapes[1] as IShape;
            slide4.Shapes.Remove(shape2);

            ITable table = (ITable)slide4.Shapes.AddTable(6, 6, 0.81 * 72, 2.14 * 72, 11.43 * 72, 3.8 * 72);
            table.Rows[0].Height = 0.85 * 72;
            table.Rows[1].Height = 0.42 * 72;
            table.Rows[2].Height = 0.85 * 72;
            table.Rows[3].Height = 0.85 * 72;
            table.Rows[4].Height = 0.85 * 72;
            table.Rows[5].Height = 0.85 * 72;
            table.HasBandedRows = true;
            table.HasHeaderRow = true;
            table.HasBandedColumns = false;
            table.BuiltInStyle = BuiltInTableStyle.MediumStyle2Accent1;

            ICell cell1 = table.Rows[0].Cells[0];
            textFrame2 = cell1.TextBody;
            paragraph2 = textFrame2.Paragraphs.Add();
            paragraph2.HorizontalAlignment = HorizontalAlignmentType.Center;
            ITextPart textPart2 = paragraph2.AddTextPart();
            textPart2.Text = "ID";

            ICell cell2 = table.Rows[0].Cells[1];
            ITextBody textFrame3 = cell2.TextBody;
            paragraph3 = textFrame3.Paragraphs.Add();
            paragraph3.HorizontalAlignment = HorizontalAlignmentType.Center;
            ITextPart textPart3 = paragraph3.AddTextPart();
            textPart3.Text = "Company Name";

            ICell cell3 = table.Rows[0].Cells[2];
            ITextBody textFrame4 = cell3.TextBody;
            paragraph4 = textFrame4.Paragraphs.Add();
            paragraph4.HorizontalAlignment = HorizontalAlignmentType.Center;
            ITextPart textPart4 = paragraph4.AddTextPart();
            textPart4.Text = "Contact Name";

            ICell cell4 = table.Rows[0].Cells[3];
            ITextBody textFrame5 = cell4.TextBody;
            paragraph5 = textFrame5.Paragraphs.Add();
            paragraph5.HorizontalAlignment = HorizontalAlignmentType.Center;
            ITextPart textPart5 = paragraph5.AddTextPart();
            textPart5.Text = "Address";

            ICell cell5 = table.Rows[0].Cells[4];
            ITextBody textFrame6 = cell5.TextBody;
            paragraph6 = textFrame6.Paragraphs.Add();
            paragraph6.HorizontalAlignment = HorizontalAlignmentType.Center;
            ITextPart textPart6 = paragraph6.AddTextPart();
            textPart6.Text = "City";

            ICell cell6 = table.Rows[0].Cells[5];
            ITextBody textFrame7 = cell6.TextBody;
            paragraph7 = textFrame7.Paragraphs.Add();
            paragraph7.HorizontalAlignment = HorizontalAlignmentType.Center;
            ITextPart textPart7 = paragraph7.AddTextPart();
            textPart7.Text = "Country";

            cell1 = table.Rows[1].Cells[0];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "1";

            cell1 = table.Rows[1].Cells[1];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "New Orleans Cajun Delights";

            cell1 = table.Rows[1].Cells[2];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Shelley Burke";

            cell1 = table.Rows[1].Cells[3];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "P.O. Box 78934";

            cell1 = table.Rows[1].Cells[4];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "New Orleans";

            cell1 = table.Rows[1].Cells[5];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "USA";

            cell1 = table.Rows[2].Cells[0];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "2";

            cell1 = table.Rows[2].Cells[1];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Cooperativa de Quesos 'Las Cabras";

            cell1 = table.Rows[2].Cells[2];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Antonio del Valle Saavedra";

            cell1 = table.Rows[2].Cells[3];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Calle del Rosal 4";

            cell1 = table.Rows[2].Cells[4];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Oviedo";

            cell1 = table.Rows[2].Cells[5];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Spain";

            cell1 = table.Rows[3].Cells[0];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "3";

            cell1 = table.Rows[3].Cells[1];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Mayumi";

            cell1 = table.Rows[3].Cells[2];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Mayumi Ohno";

            cell1 = table.Rows[3].Cells[3];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "92 Setsuko Chuo-ku";

            cell1 = table.Rows[3].Cells[4];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Osaka";

            cell1 = table.Rows[3].Cells[5];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Japan";

            cell1 = table.Rows[4].Cells[0];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "4";

            cell1 = table.Rows[4].Cells[1];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Pavlova, Ltd.";

            cell1 = table.Rows[4].Cells[2];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Ian Devling";

            cell1 = table.Rows[4].Cells[3];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "74 Rose St. Moonie Ponds";

            cell1 = table.Rows[4].Cells[4];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Melbourne";

            cell1 = table.Rows[4].Cells[5];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Australia";

            cell1 = table.Rows[5].Cells[0];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "5";

            cell1 = table.Rows[5].Cells[1];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Specialty Biscuits, Ltd.";

            cell1 = table.Rows[5].Cells[2];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Peter Wilson";

            cell1 = table.Rows[5].Cells[3];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "29 King's Way";

            cell1 = table.Rows[5].Cells[4];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "Manchester";

            cell1 = table.Rows[5].Cells[5];
            textFrame1 = cell1.TextBody;
            paragraph1 = textFrame1.Paragraphs.Add();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Center;
            textpart1 = paragraph1.AddTextPart();
            textpart1.Text = "UK";

            slide4.Shapes.RemoveAt(1);
            #endregion

            MemoryStream ms = new MemoryStream();

            SavePPTX(presentation);
        }
        /// <summary>
        /// Save as PPTX Format
        /// </summary>
        /// <param name="presentation"></param>
        private async void SavePPTX(IPresentation presentation)
        {
            StorageFile stgFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as 
                savePicker.FileTypeChoices.Add("Presentation", new List<string>() { ".pptx" });
                // Default file name if the user does not type one in or select a file to replace 
                savePicker.SuggestedFileName = "SlidesSample";
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("PowerPointPresentation.pptx", CreationCollisionOption.ReplaceExisting);
            }
            if (stgFile != null)
            {
                //Save as PPTX Format
                await presentation.SaveAsync(stgFile);
                MessageDialog msgDialog = new MessageDialog("Do you want to view the Presentation?", "File has been created successfully.");
                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new UICommand("No");
                msgDialog.Commands.Add(noCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the retrieved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(stgFile);
                }
            }
        }

    }
}
