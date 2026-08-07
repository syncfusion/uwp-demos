using Common;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.Office;
using System;
using System.Collections.Generic;
using System.IO;
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
    public sealed partial class CreateEquationDemo : SampleLayout
    {
        public CreateEquationDemo()
        {
            InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                TextBlock4.Visibility = Visibility.Collapsed;
            }
        }
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
            DisposeTextBlock(TextBlock7);
            TextBlock7 = null;

            Button1.Click -= Button_Click_1;
            DisposeButton(Button1);
            Button1 = null;

            CreateEquation.ClearValue(Grid.BackgroundProperty);
            CreateEquation.ClearValue(Grid.PaddingProperty);
            CreateEquation.Children.Clear();
            CreateEquation.ColumnDefinitions.Clear();
            CreateEquation.RowDefinitions.Clear();
            CreateEquation = null;
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
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Creates a new word document instance
            WordDocument document = new WordDocument();
            //Adds new section to the document
            IWSection section = document.AddSection();
            //Sets page margins
            document.LastSection.PageSetup.Margins.All = 72;
            //Adds new paragraph to the section
            IWParagraph paragraph = section.AddParagraph();

            //Appends text to paragraph
            IWTextRange textRange = paragraph.AppendText("Mathematical equations");
            textRange.CharacterFormat.FontSize = 28;
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            paragraph.ParagraphFormat.AfterSpacing = 12;

            #region Sum to the power of n
            //Adds new paragraph to the section
            paragraph = AddParagraph(section, "This is an expansion of the sum (1+X) to the power of n.");
            //Creates an equation with sum to the power of N
            CreateSumToThePowerOfN(paragraph);
            #endregion

            #region Fourier series
            //Adds new paragraph to the section
            paragraph = AddParagraph(section, "This is a Fourier series for the function of period 2L");
            //Creates a Fourier series equation
            CreateFourierseries(paragraph);
            #endregion

            #region Triple scalar product
            //Adds new paragraph to the section
            paragraph = AddParagraph(section, "This is an expansion of triple scalar product");
            //Creates a triple scalar product equation
            CreateTripleScalarProduct(paragraph);
            #endregion

            #region Gamma function
            //Adds new paragraph to the section
            paragraph = AddParagraph(section, "This is an expansion of gamma function");
            //Creates a gamma function equation
            CreateGammaFunction(paragraph);
            #endregion

            #region Vector relation
            //Adds new paragraph to the section
            paragraph = AddParagraph(section, "This is an expansion of vector relation ");
            //Creates a vector relation equation
            CreateVectorRelation(paragraph);
            #endregion
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
                savePicker.SuggestedFileName = "Sample";
                stgFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stgFile = await local.CreateFileAsync("WordDocument.docx", CreationCollisionOption.ReplaceExisting);
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

        #region Helper Methods
        /// <summary>
        /// Adds new paragraph into the section
        /// </summary>
        /// <param name="section">Represents a section in Word document</param>
        /// <param name="text">Represents a text to append in paragraph</param>
        /// <returns>Returns a paragraph to add equation</returns>
        private IWParagraph AddParagraph(IWSection section, string text)
        {
            //Adds new paragraph to the section
            IWParagraph paragraph = section.AddParagraph();
            //Adds new paragraph to add text
            paragraph = section.AddParagraph();
            //Appends text to paragraph
            paragraph.AppendText(text);
            paragraph.ParagraphFormat.AfterSpacing = 12;
            paragraph.ParagraphFormat.BeforeSpacing = 12;
            //Adds new paragraph to add equation
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 12;
            return paragraph;
        }
        /// <summary>
        /// Creates an expansion of triple scalar product
        /// </summary>
        /// <param name="paragraph">Represents a paragraph to add MathML element</param>
        private void CreateTripleScalarProduct(IWParagraph paragraph)
        {
            WordDocument document = paragraph.Document;
            //Creates a MathML element
            WMath math = paragraph.AppendMath();
            //Adds an office math
            IOfficeMath officeMath = math.MathParagraph.Maths.Add();

            #region Math text
            //Unicode value of middle dot
            string middleDot = "\u00B7";
            string multiplicationSign = "\u00D7";
            string text = "A" + middleDot + "B" + multiplicationSign + "C";
            //Adds a math item
            IOfficeMathRunElement officeMathParaItem = AddMathText(document, officeMath, text);
            //Sets style for math text
            officeMathParaItem.MathFormat.Style = MathStyleType.Bold;

            //Adds math text
            officeMathParaItem = AddMathText(document, officeMath, "=");
            //Sets style for math text
            officeMathParaItem.MathFormat.Style = MathStyleType.Bold;

            //Adds math text
            text = "A" + multiplicationSign + "B" + middleDot + "C";
            officeMathParaItem = AddMathText(document, officeMath, text);
            //Sets style for math text
            officeMathParaItem.MathFormat.Style = MathStyleType.Bold;

            //Adds math text
            officeMathParaItem = AddMathText(document, officeMath, "=");
            #endregion

            #region Delimiter
            //Adds a delimiter 
            IOfficeMathDelimiter mathDelimiter = officeMath.Functions.Add(MathFunctionType.Delimiter) as IOfficeMathDelimiter;
            //Sets begin character for delimiter
            mathDelimiter.BeginCharacter = "|";
            //Sets end character for delimiter
            mathDelimiter.EndCharacter = "|";
            //Apply formats for delimiter
            mathDelimiter.ControlProperties = new WCharacterFormat(document);
            (mathDelimiter.ControlProperties as WCharacterFormat).Italic = true;

            //Adds arguments for delimiter
            officeMath = mathDelimiter.Equation.Add() as IOfficeMath;

            #region Matrix
            //Add matrix into delimiter
            IOfficeMathMatrix mathMatrix = officeMath.Functions.Add(MathFunctionType.Matrix) as IOfficeMathMatrix;
			//Add columns in matrix
            mathMatrix.Columns.Add();
            mathMatrix.Columns.Add();
            mathMatrix.Columns.Add();

            #region First row
            //Adds a  new row
            IOfficeMathMatrixRow mathMatrixRow = mathMatrix.Rows.Add();
            ///Add values to row
            AddMatrixRowValues(document, mathMatrixRow, "A");
            #endregion

            #region Second row
            //Adds a  new row
            mathMatrixRow = mathMatrix.Rows.Add();
            ///Add values to row
            AddMatrixRowValues(document, mathMatrixRow, "B");
            #endregion

            #region Third row
            //Adds a  new row
            mathMatrixRow = mathMatrix.Rows.Add();
            ///Add values to row
            AddMatrixRowValues(document, mathMatrixRow, "C");
            #endregion
            #endregion

            #endregion
        }
        /// <summary>
        /// Creates an expansion of vector relation
        /// </summary>
        /// <param name="paragraph">Represents a paragraph to add MathML element</param>
        private void CreateVectorRelation(IWParagraph paragraph)
        {
            WordDocument document = paragraph.Document;
            //Creates a MathML element
            WMath math = paragraph.AppendMath();

            IOfficeMath officeMath = math.MathParagraph.Maths.Add();
            //Adds an accent equation
            AddMathAccent(document, officeMath, 8407, "A");

            //Adds a math text
            string middleDot = "\u00B7";
            officeMath = math.MathParagraph.Maths.Add();
            IOfficeMathRunElement officeMathParaItem = AddMathText(document, officeMath, middleDot);

            //Adds an accent equation
            officeMath = math.MathParagraph.Maths.Add();
            AddMathAccent(document, officeMath, 8407, "B");

            //Adds a math text
            officeMath = math.MathParagraph.Maths.Add();
            string multiplicationSign = "\u00D7";
            officeMathParaItem = AddMathText(document, officeMath, multiplicationSign);

            //Adds an accent equation
            officeMath = math.MathParagraph.Maths.Add();
            AddMathAccent(document, officeMath, 8407, "C");

            //Adds a math text
            officeMath = math.MathParagraph.Maths.Add();
            officeMathParaItem = AddMathText(document, officeMath, "=");

            //Adds an accent equation
            officeMath = math.MathParagraph.Maths.Add();
            AddMathAccent(document, officeMath, 8407, "A");

            //Adds a math text
            officeMath = math.MathParagraph.Maths.Add();
            officeMathParaItem = AddMathText(document, officeMath, multiplicationSign);

            //Adds an accent equation
            officeMath = math.MathParagraph.Maths.Add();
            AddMathAccent(document, officeMath, 8407, "B");

            //Adds a math text
            officeMath = math.MathParagraph.Maths.Add();
            officeMathParaItem = AddMathText(document, officeMath, middleDot);

            //Adds an accent equation
            officeMath = math.MathParagraph.Maths.Add();
            AddMathAccent(document, officeMath, 8407, "C");
        }
        /// <summary>
        /// Converts short value to string
        /// </summary>
        /// <param name="value">Represents a short value</param>
        private static string ConvertShortToString(short value)
        {
            char chrValue = Convert.ToChar(value);
            string strValue = Convert.ToString(chrValue);
            return strValue;
        }
        /// <summary>
        /// Creates an equation with sum to the power of N
        /// </summary>
        /// <param name="paragraph">Represents a paragraph to add MathML element</param>
        private void CreateSumToThePowerOfN(IWParagraph paragraph)
        {
            WordDocument document = paragraph.Document;
            //Creates a new MathML element
            WMath math = paragraph.AppendMath();

            IOfficeMath officeMath = math.MathParagraph.Maths.Add();
            //Adds a math script element
            IOfficeMathScript mathScript = AddMathScript(officeMath, MathScriptType.Superscript);

            #region Delimiter equation
            //Adds a delimiter
            IOfficeMathDelimiter mathDelimiter = mathScript.Equation.Functions.Add(MathFunctionType.Delimiter) as IOfficeMathDelimiter;
            //Adds an office math in the delimiter
            officeMath = mathDelimiter.Equation.Add() as IOfficeMath;
            //Adds a math text
            IOfficeMathRunElement mathParaItem = AddMathText(document, officeMath, "1+x");
            //Adds a math text
            mathParaItem = AddMathText(document, mathScript.Script, "n");
            #endregion

            //Adds a math text
            officeMath = math.MathParagraph.Maths.Add(1);
            mathParaItem = AddMathText(document, officeMath, "=1+");

            #region Fraction equation
            //Adds a math fraction
            IOfficeMathFraction mathFraction = math.MathParagraph.Maths.Add().Functions.Add(MathFunctionType.Fraction) as IOfficeMathFraction;
            //Adds a numerator text
            AddMathText(document, mathFraction.Numerator, "nx");
            //Adds a denominator text
            AddMathText(document, mathFraction.Denominator, "1!");
            #endregion

            //Adds a math text
            officeMath = math.MathParagraph.Maths.Add();
            mathParaItem = AddMathText(document, officeMath, "+");

            #region Fraction equation
            //Adds a math fraction
            mathFraction = math.MathParagraph.Maths.Add().Functions.Add(MathFunctionType.Fraction) as IOfficeMathFraction;

            #region Numerator
            //Adds a numerator text
            AddMathText(document, mathFraction.Numerator, "n");

            //Adds a delimiter
            mathDelimiter = mathFraction.Numerator.Functions.Add(MathFunctionType.Delimiter) as IOfficeMathDelimiter;
            //Adds a math text for delimiter
            officeMath = mathDelimiter.Equation.Add() as IOfficeMath;
            AddMathText(document, officeMath, "n-1");

            //Adds a math script
            mathScript = mathFraction.Numerator.Functions.Add(MathFunctionType.SubSuperscript) as IOfficeMathScript;
            //Adds a math text for Superscript
            AddMathText(document, mathScript.Equation, "x");
            AddMathText(document, mathScript.Script, "2");
            #endregion

            #region Denominator
            //Adds a math text for denominator
            AddMathText(document, mathFraction.Denominator, "2!");
            #endregion
            #endregion

            //Adds a math text
            officeMath = math.MathParagraph.Maths.Add();
            AddMathText(document, officeMath, "+...");
        }
        /// <summary>
        /// Creates an expansion of Gamma function
        /// </summary>
        /// <param name="paragraph">Represents a paragraph to add MathML element</param>
        private void CreateGammaFunction(IWParagraph paragraph)
        {
            WordDocument document = paragraph.Document;
            //Creates a new MathML element
            WMath math = paragraph.AppendMath();

            //Adds a math text
            IOfficeMath officeMath = math.MathParagraph.Maths.Add();
            //Unicode value of capital gamma
            string capitalGamma = "\u0393";
            IOfficeMathRunElement officeMathParaItem = AddMathText(document, officeMath, capitalGamma);
            //Sets MathML style format for the text
            officeMathParaItem.MathFormat.Style = MathStyleType.Regular;

            //Adds a delimiter equation
            IOfficeMathDelimiter mathDelimiter = math.MathParagraph.Maths.Add().Functions.Add(MathFunctionType.Delimiter) as IOfficeMathDelimiter;
            //Adds a equation to the delimiter
            officeMath = mathDelimiter.Equation.Add();
            //Adds a math text
            officeMathParaItem = AddMathText(document, officeMath, "z");

            //Adds a math text
            officeMath = math.MathParagraph.Maths.Add();
            officeMathParaItem = AddMathText(document, officeMath, "=");

            //Adds an n array element
            IOfficeMathNArray mathNAry = math.MathParagraph.Maths.Add().Functions.Add(MathFunctionType.NArray) as IOfficeMathNArray;
            //Adds a math text
            AddMathText(document, mathNAry.Subscript, "0");

            //Adds a math text
            string infinitySymbol = "\u221E";
            AddMathText(document, mathNAry.Superscript, infinitySymbol);

            //Adds a math superscript
            IOfficeMathScript mathScript = AddMathScript(mathNAry.Equation, MathScriptType.Superscript);
            //Adds a math text for Superscript
            AddMathText(document, mathScript.Equation, "t");
            AddMathText(document, mathScript.Script, "z-1");
            //Adds a Superscript
            mathScript = AddMathScript(mathNAry.Equation, MathScriptType.Superscript);

            AddMathText(document, mathScript.Equation, "e");
            AddMathText(document, mathScript.Script, "-t");

            //Adds a math text in n Array equation
            AddMathText(document, mathNAry.Equation, "dt");

            //Adds a math text
            AddMathText(document, math.MathParagraph.Maths.Add(), "=");

            //Adds a fraction equation
            IOfficeMathFraction mathFraction = math.MathParagraph.Maths.Add().Functions.Add(MathFunctionType.Fraction) as IOfficeMathFraction;
            //Adds a math script
            mathScript = AddMathScript(mathFraction.Numerator, MathScriptType.Superscript);

            //Adds a math text for Superscript
            AddMathText(document, mathScript.Equation, "e");
            AddMathText(document, mathScript.Script, "-");
            //Unicode of small gamma
            string smallGamma = "\u03B3";
            AddMathText(document, mathScript.Script, smallGamma);
            AddMathText(document, mathScript.Script, "z");


            //Adds a math text for denominator
            AddMathText(document, mathFraction.Denominator, "z");

            //Adds an n-array element
            mathNAry = math.MathParagraph.Maths.Add().Functions.Add(MathFunctionType.NArray) as IOfficeMathNArray;
            //Unicode value of n-array product
            string symbol = "\u220F";
            //Adds a n-array character
            mathNAry.NArrayCharacter = symbol;
            //Adds a math text
            AddMathText(document, mathNAry.Subscript, "k=1");
            //Adds a math text
            AddMathText(document, mathNAry.Superscript, infinitySymbol);

            //Adds a math script
            mathScript = AddMathScript(mathNAry.Equation, MathScriptType.Superscript);
            //Adds a math delimiter element
            mathDelimiter = mathScript.Equation.Functions.Add(MathFunctionType.Delimiter) as IOfficeMathDelimiter;
            //Adds a equation to the delimiter equation collection
            officeMath = mathDelimiter.Equation.Add();
            //Adds a math text
            AddMathText(document, officeMath, "1+");

            //Adds a fraction element
            mathFraction = officeMath.Functions.Add(MathFunctionType.Fraction) as IOfficeMathFraction;
            //Adds a math text for numerator
            AddMathText(document, mathFraction.Numerator, "z");
            //Adds a math text for denominator
            AddMathText(document, mathFraction.Denominator, "k");

            //Adds a math text
            AddMathText(document, mathScript.Script, "-1");
            //Adds a Superscript equation
            mathScript = AddMathScript(mathNAry.Equation, MathScriptType.Superscript);
            //Adds a math text for Superscript
            AddMathText(document, mathScript.Equation, "e");
            AddMathText(document, mathScript.Script, "z");
            officeMathParaItem = AddMathText(document, mathScript.Script, "/");
            officeMathParaItem.MathFormat.HasLiteral = true;
            AddMathText(document, mathScript.Script, "k");

            //Adds a math text
            AddMathText(document, math.MathParagraph.Maths.Add(), ",");

            //Adds a math text
            officeMathParaItem = AddMathText(document, math.MathParagraph.Maths.Add(), "  ");
            //Sets style for math text
            officeMathParaItem.MathFormat.Style = MathStyleType.Regular;

            //Adds a math text
            AddMathText(document, math.MathParagraph.Maths.Add(), smallGamma);
            string text = "\u2248" + "0.577216";
            AddMathText(document, math.MathParagraph.Maths.Add(), text);
        }
        /// <summary>
        /// Creates a Fourier series equation
        /// </summary>
        /// <param name="paragraph">Represents a paragraph to add MathML element</param>
        private void CreateFourierseries(IWParagraph paragraph)
        {
            WordDocument document = paragraph.Document;
            //Creates a new MathML element
            WMath math = paragraph.AppendMath();

            //Adds a math
            IOfficeMath officeMath = math.MathParagraph.Maths.Add();
            //Adds a math text
            AddMathText(document, officeMath, "f");

            //Adds a math delimiter
            IOfficeMathDelimiter mathDelimiter = math.MathParagraph.Maths.Add().Functions.Add(MathFunctionType.Delimiter) as IOfficeMathDelimiter;
            //Adds a math in the delimiter
            officeMath = mathDelimiter.Equation.Add() as IOfficeMath;
            //Adds a math text
            AddMathText(document, officeMath, "x");
            AddMathText(document, math.MathParagraph.Maths.Add(), "=");
            //Adds a Subscript equation
            IOfficeMathScript mathScript = AddMathScript(math.MathParagraph.Maths.Add(), MathScriptType.Subscript);
            //Adds a math text
            AddMathText(document, mathScript.Equation, "a");
            AddMathText(document, mathScript.Script, "0");

            //Adds a math text
            AddMathText(document, math.MathParagraph.Maths.Add(), "+");

            //Adds a math n-array
            IOfficeMathNArray mathNAry = math.MathParagraph.Maths.Add().Functions.Add(MathFunctionType.NArray) as IOfficeMathNArray;
            //Unicode value of n-array summation
            string sigma = "\u2211";
            //Sets the value as the n-array character
            mathNAry.NArrayCharacter = sigma;
            mathNAry.HasGrow = true;
            //Adds a math text
            AddMathText(document, mathNAry.Subscript, "n=1");

            //Adds a math text
            string infinitySymbol = "\u221E";
            AddMathText(document, mathNAry.Superscript, infinitySymbol);
            //Adds a math delimiter
            mathDelimiter = mathNAry.Equation.Functions.Add(MathFunctionType.Delimiter) as IOfficeMathDelimiter;
            //Adds an math in the delimiter equation collection
            officeMath = mathDelimiter.Equation.Add() as IOfficeMath;
            //Adds a math script
            mathScript = AddMathScript(officeMath, MathScriptType.Subscript);

            //Adds a math text
            AddMathText(document, mathScript.Equation, "a");

            //Adds a math text
            AddMathText(document, mathScript.Script, "n");

            //Adds a math function
            IOfficeMathFunction mathFunction = officeMath.Functions.Add(MathFunctionType.Function) as IOfficeMathFunction;
            //Adds a math text
            IOfficeMathRunElement mathParaItem = AddMathText(document, mathFunction.FunctionName, "cos");
            mathParaItem.MathFormat.Style = MathStyleType.Regular;

            //Adds a math fraction
            IOfficeMathFraction mathFraction = mathFunction.Equation.Functions.Add(MathFunctionType.Fraction) as IOfficeMathFraction;
            //Adds a math text
            //Unicode value of PI
            string pi = "\uD835\uDF0B";
            string text = "n" + pi + "x";
            AddMathText(document, mathFraction.Numerator, text);
            AddMathText(document, mathFraction.Denominator, "L");

            //Adds a math text
            AddMathText(document, officeMath, "+");
            //Adds a math script
            mathScript = AddMathScript(officeMath, MathScriptType.Subscript);
            //Adds a math text
            AddMathText(document, mathScript.Equation, "b");
            AddMathText(document, mathScript.Script, "n");

            //Adds a function
            mathFunction = officeMath.Functions.Add(MathFunctionType.Function) as IOfficeMathFunction;
            //Adds a math text
            mathParaItem = AddMathText(document, mathFunction.FunctionName, "sin");
            mathParaItem.MathFormat.Style = MathStyleType.Regular;

            //Adds a math fraction element
            mathFraction = mathFunction.Equation.Functions.Add(MathFunctionType.Fraction) as IOfficeMathFraction;
            //Adds a math text for numerator
            AddMathText(document, mathFraction.Numerator, text);
            //Adds a math text for denominator
            AddMathText(document, mathFraction.Denominator, "L");
        }
        /// <summary>
        /// Adds a math text
        /// </summary>
        /// <param name="document">Represents a Word document to add math text</param>
        /// <param name="officeMath">Represents an office math to add math text</param>
        /// <param name="text">Represents the text to set for math item</param>
        private IOfficeMathRunElement AddMathText(WordDocument document, IOfficeMath officeMath, string text)
        {
            //Adds math text
            IOfficeMathRunElement officeMathParaItem = officeMath.Functions.Add(MathFunctionType.RunElement) as IOfficeMathRunElement;
            officeMathParaItem.Item = new WTextRange(document);
            //Set math text value
            (officeMathParaItem.Item as WTextRange).Text = text;
            return officeMathParaItem;
        }
        /// <summary>
        /// Adds a math Subscript or Superscript equation
        /// </summary>
        private IOfficeMathScript AddMathScript(IOfficeMath officeMath, MathScriptType mathScriptType)
        {
            IOfficeMathScript mathScript = officeMath.Functions.Add(MathFunctionType.SubSuperscript) as IOfficeMathScript;
            //Sets the script type as Subscript or Superscript
            mathScript.ScriptType = mathScriptType;
            return mathScript;
        }
        /// <summary>
        /// Adds a accent equation
        /// </summary>
        /// <param name="document">Represents a Word document</param>
        /// <param name="officeMath">Represents a office math to add accent equation</param>
        /// <param name="accentCharValue">Represents a accent character</param>
        /// <param name="text">Represents a text for accent equation</param>
        private void AddMathAccent(WordDocument document, IOfficeMath officeMath, short accentCharValue, string text)
        {
            IOfficeMathAccent mathAccent = officeMath.Functions.Add(MathFunctionType.Accent) as IOfficeMathAccent;
            //Sets the accent character from short value
            mathAccent.AccentCharacter = ConvertShortToString(accentCharValue);
            //Adds a math text
            IOfficeMathRunElement officeMathParaItem = AddMathText(document, mathAccent.Equation, text);
        }
        /// <summary>
        /// Add values in matrix row
        /// </summary>
        /// <param name="document">Represents a Word document to add matrix</param>
        /// <param name="mathMatrixRow">Represents a matrix row to add values</param>
        /// <param name="text">Represents a base text value for Subscript and Superscript equation</param>
        private void AddMatrixRowValues(WordDocument document, IOfficeMathMatrixRow mathMatrixRow, string text)
        {
            //Adds arguments for matrix row
            IOfficeMath officeMath = mathMatrixRow.Arguments[0];
            //Adds a Subscript
            IOfficeMathScript mathScript = AddMathScript(officeMath, MathScriptType.Subscript);
            //Adds a math text
            IOfficeMathRunElement officeMathParaItem = AddMathText(document, mathScript.Equation, text);
            //Adds math text
            officeMathParaItem = AddMathText(document, mathScript.Script, "x");

            //Adds arguments for matrix row
            officeMath = mathMatrixRow.Arguments[1];
            //Adds a script
            mathScript = AddMathScript(officeMath, MathScriptType.Subscript);
            //Adds math text
            officeMathParaItem = AddMathText(document, mathScript.Equation, text);
            //Adds math text
            officeMathParaItem = AddMathText(document, mathScript.Script, "y");

            //Adds arguments for matrix row
            officeMath = mathMatrixRow.Arguments[2];
            //Adds a script
            mathScript = AddMathScript(officeMath, MathScriptType.Subscript);
            //Adds math text
            officeMathParaItem = AddMathText(document, mathScript.Equation, text);
            //Adds math text
            officeMathParaItem = AddMathText(document, mathScript.Script, "z");
        }
        #endregion

    }
}
