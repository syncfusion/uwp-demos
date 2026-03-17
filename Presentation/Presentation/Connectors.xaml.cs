#region Copyright Syncfusion Inc. 2001 - 2016
// Copyright Syncfusion Inc. 2001 - 2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Presentation;
using Common;
using System;
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
    public sealed partial class ConnectorsPresentation : SampleLayout
    {
        #region Fields
        Assembly execAssm = typeof(Common.MainPage).GetTypeInfo().Assembly;
        #endregion

        public ConnectorsPresentation()
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
            //Create an instance for PowerPoint
            IPresentation presentation = Presentation.Create();

            //Add a blank slide to Presentation
            ISlide slide = presentation.Slides.Add(SlideLayoutType.Blank);

            //Add header shape
            IShape headerTextBox = slide.Shapes.AddTextBox(58.44, 53.85, 221.93, 81.20);
            //Add a paragraph into the text box
            IParagraph paragraph = headerTextBox.TextBody.AddParagraph("Flow chart with ");
            //Add a textPart
            ITextPart textPart = paragraph.AddTextPart("Connector");
            //Change the color of the font
            textPart.Font.Color = ColorObject.FromArgb(44, 115, 230);
            //Make the textpart bold
            textPart.Font.Bold = true;
            //Set the font size of the paragraph
            paragraph.Font.FontSize = 28;

            //Add start shape to slide
            IShape startShape = slide.Shapes.AddShape(AutoShapeType.FlowChartTerminator, 420.45, 36.35, 133.93, 50.39);
            //Add a paragraph into the start shape text body
            AddParagraph(startShape, "Start", ColorObject.FromArgb(255, 149, 34));

            //Add alarm shape to slide
            IShape alarmShape = slide.Shapes.AddShape(AutoShapeType.FlowChartProcess, 420.45, 126.72, 133.93, 50.39);
            //Add a paragraph into the alarm shape text body
            AddParagraph(alarmShape, "Alarm Rings", ColorObject.FromArgb(255, 149, 34));

            //Add condition shape to slide
            IShape conditionShape = slide.Shapes.AddShape(AutoShapeType.FlowChartDecision, 420.45, 222.42, 133.93, 97.77);
            //Add a paragraph into the condition shape text body
            AddParagraph(conditionShape, "Ready to Get Up ?", ColorObject.FromArgb(44, 115, 213));

            //Add wake up shape to slide
            IShape wakeUpShape = slide.Shapes.AddShape(AutoShapeType.FlowChartProcess, 420.45, 361.52, 133.93, 50.39);
            //Add a paragraph into the wake up shape text body
            AddParagraph(wakeUpShape, "Wake Up", ColorObject.FromArgb(44, 115, 213));

            //Add end shape to slide
            IShape endShape = slide.Shapes.AddShape(AutoShapeType.FlowChartTerminator, 420.45, 453.27, 133.93, 50.39);
            //Add a paragraph into the end shape text body
            AddParagraph(endShape, "End", ColorObject.FromArgb(44, 115, 213));

            //Add snooze shape to slide
            IShape snoozeShape = slide.Shapes.AddShape(AutoShapeType.FlowChartProcess, 624.85, 245.79, 159.76, 50.02);
            //Add a paragraph into the snooze shape text body
            AddParagraph(snoozeShape, "Hit Snooze button", ColorObject.FromArgb(255, 149, 34));

            //Add relay shape to slide
            IShape relayShape = slide.Shapes.AddShape(AutoShapeType.FlowChartDelay, 624.85, 127.12, 159.76, 49.59);
            //Add a paragraph into the relay shape text body
            AddParagraph(relayShape, "Relay", ColorObject.FromArgb(255, 149, 34));

            //Connect the start shape with alarm shape using connector
            IConnector connector1 = slide.Shapes.AddConnector(ConnectorType.Straight, startShape, 2, alarmShape, 0);
            //Set the arrow style for the connector
            connector1.LineFormat.EndArrowheadStyle = ArrowheadStyle.Arrow;

            //Connect the alarm shape with condition shape using connector
            IConnector connector2 = slide.Shapes.AddConnector(ConnectorType.Straight, alarmShape, 2, conditionShape, 0);
            //Set the arrow style for the connector
            connector2.LineFormat.EndArrowheadStyle = ArrowheadStyle.Arrow;

            //Connect the condition shape with snooze shape using connector
            IConnector connector3 = slide.Shapes.AddConnector(ConnectorType.Straight, conditionShape, 3, snoozeShape, 1);
            //Set the arrow style for the connector
            connector3.LineFormat.EndArrowheadStyle = ArrowheadStyle.Arrow;

            //Connect the snooze shape with relay shape using connector
            IConnector connector4 = slide.Shapes.AddConnector(ConnectorType.Straight, snoozeShape, 0, relayShape, 2);
            //Set the arrow style for the connector
            connector4.LineFormat.EndArrowheadStyle = ArrowheadStyle.Arrow;

            //Connect the relay shape with alarm shape using connector
            IConnector connector5 = slide.Shapes.AddConnector(ConnectorType.Straight, relayShape, 1, alarmShape, 3);
            //Set the arrow style for the connector
            connector5.LineFormat.EndArrowheadStyle = ArrowheadStyle.Arrow;

            //Connect the condition shape with wake up shape using connector
            IConnector connector6 = slide.Shapes.AddConnector(ConnectorType.Straight, conditionShape, 2, wakeUpShape, 0);
            //Set the arrow style for the connector
            connector6.LineFormat.EndArrowheadStyle = ArrowheadStyle.Arrow;

            //Connect the wake up shape with end shape using connector
            IConnector connector7 = slide.Shapes.AddConnector(ConnectorType.Straight, wakeUpShape, 2, endShape, 0);
            //Set the arrow style for the connector
            connector7.LineFormat.EndArrowheadStyle = ArrowheadStyle.Arrow;

            //Add No textbox to slide
            IShape noTextBox = slide.Shapes.AddTextBox(564.02, 245.43, 51.32, 26.22);
            //Add a paragraph into the text box
            noTextBox.TextBody.AddParagraph("No");

            //Add Yes textbox to slide
            IShape yesTextBox = slide.Shapes.AddTextBox(487.21, 327.99, 50.09, 26.23);
            //Add a paragraph into the text box
            yesTextBox.TextBody.AddParagraph("Yes");

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
                savePicker.SuggestedFileName = "ConnectorsSample";
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

        /// <summary>
        /// Add a paragraph into the specified shape with specified text
        /// </summary>
        /// <param name="shape">Represent the shape</param>
        /// <param name="text">Represent the text to be added</param>
        /// <param name="fillColor">Represent the color to fill the shape</param>
        private void AddParagraph(IShape shape, string text, IColor fillColor)
        {
            //set the fill type as solid
            shape.Fill.FillType = FillType.Solid;
            //Set the color of the solid fill
            shape.Fill.SolidFill.Color = fillColor;
            //set the fill type of line format as solid
            shape.LineFormat.Fill.FillType = FillType.Solid;
            //set the fill color of line format
            if (fillColor.R == 255)
                shape.LineFormat.Fill.SolidFill.Color = ColorObject.FromArgb(190, 100, 39);
            else
                shape.LineFormat.Fill.SolidFill.Color = ColorObject.FromArgb(54, 91, 157);
            //Add a paragraph into the specified shape with specified text
            IParagraph paragraph = shape.TextBody.AddParagraph(text);
            //Set the vertical alignment as center
            shape.TextBody.VerticalAlignment = VerticalAlignmentType.Middle;
            //Set horizontal alignment as center
            paragraph.HorizontalAlignment = HorizontalAlignmentType.Center;
            //Set font color as white
            paragraph.Font.Color = ColorObject.White;
            //Change the font size
            paragraph.Font.FontSize = 16;
        }
    }
}
