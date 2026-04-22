#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using Syncfusion.Office;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialDocIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateInkDemo : SampleLayout
    {
        public override void Dispose()
        {
            base.Dispose();

            DisposeTextBlock(TextBlock1);
            TextBlock1 = null;
            DisposeTextBlock(TextBlock2);
            TextBlock2 = null;
            DisposeTextBlock(TextBlock3);
            TextBlock3 = null;
            DisposeTextBlock(TextBlock4);
            TextBlock4 = null;
            DisposeTextBlock(TextBlock5);
            TextBlock5 = null;
            DisposeTextBlock(TextBlock6);
            TextBlock6 = null;
            

            Button1.Click -= Button_Click_1;
            DisposeButton(Button1);
            Button1 = null;

            Button2.Click -= Button_Click_2;
            DisposeButton(Button2);
            Button2 = null;

            CreateInk.ClearValue(Grid.BackgroundProperty);
            CreateInk.ClearValue(Grid.PaddingProperty);
            CreateInk.Children.Clear();
            CreateInk.ColumnDefinitions.Clear();
            CreateInk.RowDefinitions.Clear();
            CreateInk = null;
        }
        public void DisposeTextBlock(TextBlock textBlock)
        {
            textBlock.ClearValue(TextBlock.FontFamilyProperty);
            textBlock.ClearValue(TextBlock.FontSizeProperty);
            textBlock.ClearValue(TextBlock.TextProperty);
            textBlock.ClearValue(TextBlock.TextWrappingProperty);
            textBlock.ClearValue(TextBlock.ForegroundProperty);
        }
        public void DisposeButton(Button button)
        {
            button.ClearValue(Button.FontFamilyProperty);
            button.ClearValue(Button.FontSizeProperty);
            button.ClearValue(Button.PaddingProperty);
            button.ClearValue(Button.ForegroundProperty);
            button.ClearValue(Button.BackgroundProperty);
            button.ClearValue(Button.ContentProperty);
            button.ClearValue(Button.HeightProperty);
            button.ClearValue(Button.WidthProperty);
        }
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Assembly execAssm = typeof(CreateInkDemo).GetTypeInfo().Assembly;
            Stream inputStream = execAssm.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.CreateInkInput.docx");
            MemoryStream stream = new MemoryStream();
            inputStream.CopyTo(stream);
            inputStream.Dispose();
            stream.Position = 0;
            ViewTemplate(stream);
        }
        public CreateInkDemo()
        {
            InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                CreateInk.Margin = new Thickness(10, 10, 10, 10);
                TextBlock5.Visibility = Visibility.Collapsed;
                TextBlock6.Visibility = Visibility.Collapsed;
                TextBlock4.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBlock6.Visibility = Visibility.Visible;
                TextBlock5.Visibility = Visibility.Visible;
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Assembly execAssem = typeof(CreateInkDemo).GetTypeInfo().Assembly;
            //Load an existing Word document.
            WordDocument document = new WordDocument();
            Stream inputStream = execAssem.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.DocIO.DocIO.Assets.CreateInkInput.docx");
            // Open an existing template document.
            await document.OpenAsync(inputStream, FormatType.Docx);
            inputStream.Dispose();

            // Get the last paragraph of document.
            WParagraph paragraph = document.LastParagraph;
            // Append an ink object to the paragraph.
            WInk inkObj = paragraph.AppendInk(80, 20);
            // Get the traces collection from the ink object (traces represent the drawing strokes).
            IOfficeInkTraces traces = inkObj.Traces;
            // Retrieve an array of points that define the path of the ink stroke.
            PointF[] tracePoints = GetPoints();
            // Add a new trace (stroke) to the traces collection using the retrieved points.
            IOfficeInkTrace trace = traces.Add(tracePoints);

            // Configure the appearance of the ink.
            // Get the brush object associated with the trace to configure its appearance.
            IOfficeInkBrush brush = trace.Brush;
            // Set the ink effect type to None (Pen effect applied).
            brush.InkEffect = OfficeInkEffectType.None;
            // Set the color of the ink stroke.
            brush.Color = Color.Black;
            // Set the size (thickness) of the ink stroke to 1.5 units.
            brush.Size = new SizeF(1.5f, 1.5f);

            SaveDocx(document);
        }
        /// <summary>
        /// Save as Docx Format
        /// </summary>
        /// <param name="doc"></param>
        private async void SaveDocx(WordDocument doc)
        {
            StorageFile stgFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as 
                savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".docx" });
                // Default file name if the user does not type one in or select a file to replace 
                savePicker.SuggestedFileName = "CreateInk";
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("CreateInk.docx", CreationCollisionOption.ReplaceExisting);
            }
            if (stgFile != null)
            {
                MemoryStream stream = new MemoryStream();
                await doc.SaveAsync(stream, FormatType.Docx);
                doc.Close();
                stream.Position = 0;
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stgFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();

                //Save as Docx Format

                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");
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
        private async void ViewTemplate(MemoryStream stream)
        {
            StorageFile stgFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as 
                savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".docx" });
                // Default file name if the user does not type one in or select a file to replace 
                savePicker.SuggestedFileName = "CreateInkInput";
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("CreateInkInput.docx", CreationCollisionOption.ReplaceExisting);
            }
            if (stgFile != null)
            {
                stream.Position = 0;
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stgFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();
                //Save as Docx Format
                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");
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
        #region Helper Methods
        /// <summary>
        /// Gets the array of trace points defining the signature.
        /// </summary>
        /// <returns>Array of <see cref="PointF"/> representing the path coordinates.</returns>
        public PointF[] GetPoints()
        {
            PointF[] tracePoints = new PointF[95];

            tracePoints[0] = new PointF(15f, 35f);
            tracePoints[1] = new PointF(18f, 28f);
            tracePoints[2] = new PointF(22f, 22f);
            tracePoints[3] = new PointF(27f, 17f);
            tracePoints[4] = new PointF(32f, 14f);
            tracePoints[5] = new PointF(37f, 12f);
            tracePoints[6] = new PointF(42f, 12f);
            tracePoints[7] = new PointF(46f, 14f);
            tracePoints[8] = new PointF(49f, 18f);
            tracePoints[9] = new PointF(51f, 23f);
            tracePoints[10] = new PointF(52f, 28f);
            tracePoints[11] = new PointF(52f, 33f);
            tracePoints[12] = new PointF(51f, 38f);
            tracePoints[13] = new PointF(49f, 42f);
            tracePoints[14] = new PointF(46f, 45f);
            tracePoints[15] = new PointF(42f, 47f);
            tracePoints[16] = new PointF(38f, 48f);
            tracePoints[17] = new PointF(34f, 47f);
            tracePoints[18] = new PointF(31f, 45f);
            tracePoints[19] = new PointF(35f, 42f);
            tracePoints[20] = new PointF(40f, 39f);
            tracePoints[21] = new PointF(46f, 37f);
            tracePoints[22] = new PointF(52f, 36f);
            tracePoints[23] = new PointF(58f, 36f);
            tracePoints[24] = new PointF(63f, 37f);
            tracePoints[25] = new PointF(67f, 40f);
            tracePoints[26] = new PointF(69f, 44f);
            tracePoints[27] = new PointF(69f, 48f);
            tracePoints[28] = new PointF(67f, 51f);
            tracePoints[29] = new PointF(64f, 53f);
            tracePoints[30] = new PointF(60f, 54f);
            tracePoints[31] = new PointF(56f, 53f);
            tracePoints[32] = new PointF(54f, 51f);
            tracePoints[33] = new PointF(53f, 48f);
            tracePoints[34] = new PointF(54f, 45f);
            tracePoints[35] = new PointF(57f, 43f);
            tracePoints[36] = new PointF(61f, 42f);
            tracePoints[37] = new PointF(65f, 42f);
            tracePoints[38] = new PointF(69f, 43f);
            tracePoints[39] = new PointF(73f, 44f);
            tracePoints[40] = new PointF(77f, 43f);
            tracePoints[41] = new PointF(81f, 40f);
            tracePoints[42] = new PointF(84f, 44f);
            tracePoints[43] = new PointF(86f, 48f);
            tracePoints[44] = new PointF(88f, 52f);
            tracePoints[45] = new PointF(90f, 55f);
            tracePoints[46] = new PointF(92f, 52f);
            tracePoints[47] = new PointF(94f, 48f);
            tracePoints[48] = new PointF(96f, 44f);
            tracePoints[49] = new PointF(98f, 40f);
            tracePoints[50] = new PointF(100f, 37f);
            tracePoints[51] = new PointF(104f, 36f);
            tracePoints[52] = new PointF(107f, 38f);
            tracePoints[53] = new PointF(109f, 42f);
            tracePoints[54] = new PointF(110f, 46f);
            tracePoints[55] = new PointF(110f, 50f);
            tracePoints[56] = new PointF(109f, 53f);
            tracePoints[57] = new PointF(112f, 51f);
            tracePoints[58] = new PointF(116f, 48f);
            tracePoints[59] = new PointF(120f, 46f);
            tracePoints[60] = new PointF(125f, 45f);
            tracePoints[61] = new PointF(130f, 46f);
            tracePoints[62] = new PointF(134f, 48f);
            tracePoints[63] = new PointF(137f, 51f);
            tracePoints[64] = new PointF(138f, 54f);
            tracePoints[65] = new PointF(137f, 57f);
            tracePoints[66] = new PointF(134f, 58f);
            tracePoints[67] = new PointF(130f, 58f);
            tracePoints[68] = new PointF(126f, 56f);
            tracePoints[69] = new PointF(124f, 53f);
            tracePoints[70] = new PointF(123f, 49f);
            tracePoints[71] = new PointF(125f, 45f);
            tracePoints[72] = new PointF(127f, 40f);
            tracePoints[73] = new PointF(129f, 35f);
            tracePoints[74] = new PointF(131f, 30f);
            tracePoints[75] = new PointF(133f, 25f);
            tracePoints[76] = new PointF(135f, 20f);
            tracePoints[77] = new PointF(140f, 25f);
            tracePoints[78] = new PointF(148f, 32f);
            tracePoints[79] = new PointF(158f, 38f);
            tracePoints[80] = new PointF(170f, 43f);
            tracePoints[81] = new PointF(182f, 46f);
            tracePoints[82] = new PointF(190f, 47f);
            tracePoints[83] = new PointF(185f, 48f);
            tracePoints[84] = new PointF(175f, 50f);
            tracePoints[85] = new PointF(160f, 52f);
            tracePoints[86] = new PointF(140f, 54f);
            tracePoints[87] = new PointF(115f, 55f);
            tracePoints[88] = new PointF(85f, 56f);
            tracePoints[89] = new PointF(60f, 56f);
            tracePoints[90] = new PointF(40f, 55f);
            tracePoints[91] = new PointF(25f, 53f);
            tracePoints[92] = new PointF(15f, 50f);
            tracePoints[93] = new PointF(10f, 47f);
            tracePoints[94] = new PointF(8f, 44f);

            return tracePoints;
        }
		# endregion
    }
}
