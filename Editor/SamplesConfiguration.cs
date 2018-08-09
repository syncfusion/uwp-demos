using Common;
using SampleBrowser.Editors.Controls.AutoComplete;
using SampleBrowser.Editors.Controls.Calculator;
using SampleBrowser.Editors.Controls.ComboBox;
using SampleBrowser.Editors.Controls.DataValidation;
using SampleBrowser.Editors.Controls.DomainUpDown;
using SampleBrowser.Editors.Controls.DropDown;
using SampleBrowser.Editors.Controls.MaskedEdit;
using SampleBrowser.Editors.Controls.NumericTextBox;
using SampleBrowser.Editors.Controls.NumericUpDown;
using SampleBrowser.Editors.Controls.RangeSlider;
using SampleBrowser.Editors.Controls.RatingControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml; 

namespace Syncfusion.SampleBrowser.UWP.Editors
{

    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {



            #region Calendar Sample
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "CellTemplate",
                Category = Categories.Editors,
                Product = "Calendar",
                ProductIcons = "Icons/Calendar.png",
                SampleView = typeof(Input.Calendar.CellTemplateView).AssemblyQualifiedName,
                Tag = Tags.None,
                SearchKeys = new string[] { "Calendar", "started","Template","Customization" }
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "GettingStarted",
                Category = Categories.Editors,
                Product = "Calendar",
                ProductIcons = "Icons/Calendar.png",
                SampleView = typeof(Input.Calendar.CalendarView).AssemblyQualifiedName,
                Tag = Tags.None,
                SearchKeys = new string[] { "Calendar", "started" }
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Calendar Appointment",
                Category = Categories.Editors,
                Product = "Calendar",
                ProductIcons = "Icons/Calendar.png",
                SampleView = typeof(Input.Calendar.AppointmentView).AssemblyQualifiedName,
                Tag = Tags.None,
                SearchKeys = new string[] { "Calendar", "started","Appointment","customization" }
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Input.Calendar.BlackOutDatesView).AssemblyQualifiedName,
                Header = "BlackOutDates",
                Product = "Calendar",
                ProductIcons = "Icons/Calendar.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Calendar", "Black","Out","Date" }
            });
            #endregion
            #region DateTimePicker Sample
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Input.DateTimePickers.DatePickerView).AssemblyQualifiedName,
                Header = "DatePicker",
                Product = "Date Time Pickers",
                ProductIcons = "Icons/Date Time Pickers.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "DateTime", "Editor", "started", "Picker" }

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Input.DateTimePickers.TimePickerView).AssemblyQualifiedName,
                Header = "TimePicker",
                Product = "Date Time Pickers",
                ProductIcons = "Icons/Date Time Pickers.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "DateTime", "Editor", "started", "Picker" }

            });
       #if (!WINDOWS_STORE)
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Input.DateTimePickers.DateSelectorView).AssemblyQualifiedName,
                Header = "DateSelector",
                Product = "Date Time Pickers",
                ProductIcons = "Icons/Date Time Pickers.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "DateTime", "Editor", "started", "Picker" }
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Input.DateTimePickers.TimeSelectorView).AssemblyQualifiedName,
                Header = "TimeSelector",
                Category = Categories.Editors,
                Product = "Date Time Pickers",
                ProductIcons = "Icons/Date Time Pickers.png",
                SearchKeys = new string[] { "DateTime", "Editor", "started", "Picker" }
            });
     #endif
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Input.DateTimePickers.CellTemplateView).AssemblyQualifiedName,
                Header = "CellTemplate",
                Product = "Date Time Pickers",
                ProductIcons = "Icons/Date Time Pickers.png",
                SampleCategory = "Customize Date Time Picker",
                Category = Categories.Editors,
                Tag = Tags.None,
                Description = " The Calendar control provides an API named as CellTemplate , By using Cell Template users can edit the cell templates in the SfCalendar control.",
                DesktopImage = "ms-appx:///WhatsNewImage/2.png",
                MobileImage = "ms-appx:///WhatsNewImage/2.png",
                SearchKeys = new string[] { "DateTime", "Editor", "started", "Picker" }
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Input.DateTimePickers.DateTimeComboView).AssemblyQualifiedName,
                Header = "DateTimeCombo",
                Tag = Tags.None,
                Description = " The DateTimeCombo control displays each part of a date or time in a separate DateTimeItem drop-down list which can be edited.",

                Product = "Date Time Pickers",
                ProductIcons = "Icons/Date Time Pickers.png",
                Category = Categories.Editors,
                DesktopImage = "ms-appx:///WhatsNewImage/1.png",
                MobileImage = "ms-appx:///WhatsNewImage/1.png",
                SearchKeys = new string[] { "DateTimeCombo", "Editor", "started", "Picker" }

            });
#endregion
#region Showcase
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(UnitConverter.SamplePage).AssemblyQualifiedName,
                Header = "Unit Converter",
                Product = "Editor",
                SampleType = SampleType.Showcase,
                Description = "This sample showcases all the major features available in the editor controls.",
                Category = Categories.Editors,
                DesktopImage = "ms-appx:///Showcase/unit.jpg",
                MobileImage = "ms-appx:///Showcase/unit.jpg",
                SearchKeys = new string[] { "DateTime", "Editor", "started", "Picker" }
            });
#endregion
#region ColorPicker Sample
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Media.ColorPickers.ColorPickerView).AssemblyQualifiedName,
                Header = "ColorPicker",
                Tag = Tags.None,
                Description = " The ColorPicker control exposes color selection through a touch friendly interface. All settings including the RGB values can be manipulated purely using touch.",
                Product = "Color Pickers",
                ProductIcons = "Icons/Color Pickers.png",
                Category = Categories.Editors,
                HasOptions = false,
                SearchKeys = new string[] { "ColorPicker", "Editor", "started", "Picker" }
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Media.ColorPickers.ColorPaletteView).AssemblyQualifiedName,
                Header = "ColorPalette",
                Product = "Color Pickers",
                ProductIcons = "Icons/Color Pickers.png",
                Category = Categories.Editors,
                HasOptions = false,
                SearchKeys = new string[] { "ColorPalette", "Editor", "started","Picker" }
            });
#endregion
#region Ediotrs Sample
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(EditAutoCompleteView).AssemblyQualifiedName,
                Header = "Auto Complete",
                Tag = Tags.None,
                Description = "The AutoComplete control simplifies data input by providing end users with possible matches as soon as they start typing. There are several modes available to assist data entry like Suggest, Append or both.",
                Product = "Auto Complete",
                ProductIcons = "Icons/Auto Complete.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Getting", "Editor", "started","AutoComplete" }

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(ComboBox).AssemblyQualifiedName,
                Header = "Getting Started",
                Tag = Tags.None,
                Product = "ComboBox",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Getting", "Editor", "started","ComboBox" }

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(DataValidationDemo).AssemblyQualifiedName,
                Header = "Getting Started",
                Tag = Tags.None,
                Product = "TextBoxExt",
                ProductIcons = "Icons/TextBoxExt.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Getting", "Editor", "started","Custom","Label","CustomLabel" }

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(RangeSliderDemo).AssemblyQualifiedName,
                Header = "Getting Started",
                Tag = Tags.None,
                Product = "RangeSlider",
                ProductIcons = "Icons/RangeSlider.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Getting", "Editor", "started", "Selection", "range", "rangeSlider" }

            });
#if (!WINDOWS_STORE)
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CustomLabelDemo).AssemblyQualifiedName,
                Header = "Custom Label",
                Tag = Tags.None,
                Product = "RangeSlider",
                ProductIcons = "Icons/RangeSlider.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Getting", "Editor", "started","Validation","Data","DataValidation" }

            });
#endif
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(DomainUpDown).AssemblyQualifiedName,
                Header = "Getting Started",
                Tag = Tags.None,
                Product = "DomainUpDown",
                ProductIcons = "Icons/DomainUpDown.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Getting stater", "Editor", "started" }

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(DropDownDemo).AssemblyQualifiedName,
                Header = "Getting Started",
                Tag = Tags.None,
                Product = "DropDownButton",
                ProductIcons = "Icons/DropDownButton.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Getting stater", "Editor", "started" }

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(MaskedEditDemo).AssemblyQualifiedName,
                Header = "Getting Started",
                Tag = Tags.Updated,
                Product = "MaskedEdit",
                ProductIcons = "Icons/MaskedEdit.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Getting", "Editor", "started","MaskedEdit" }

            });
         
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(NumericTextBoxDemo).AssemblyQualifiedName,
                Header = "Getting Started",
                Tag = Tags.None,
                Product = "NumericTextBox",
                ProductIcons = "Icons/NumericTextBox.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Getting stater", "Editor", "started" }

            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(NumericUpDownDemo).AssemblyQualifiedName,
                Header = "Getting Started",
                Tag = Tags.None,
                Product = "NumericUpDown",
                ProductIcons = "Icons/NumericUpDown.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Getting", "Editor", "started","Numeric","UpDon","NumericuPdOWN" }

            });
            
            //SampleHelper.SampleViews.Add(new SampleInfo()
            //{
            //    SampleView = typeof(Input.Editors.RatingControl.RatingView).AssemblyQualifiedName,
            //    Header = "Getting Started",
            //    Product = "Rating",
            //    ProductIcons = "Icons/Rating.png",
            //    Category = Categories.Editors,
            //    SearchKeys = new string[] { "Rating", "Editor", "started" }
            //});

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CalculatorSample).AssemblyQualifiedName,
                Header = "Getting Started",
                Product = "Calculator",
                ProductIcons = "Icons/Calculator.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Calcuator", "Editor", "started" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(RatingView).AssemblyQualifiedName,
                Header = "Getting Started",
                Product = "Rating",
                ProductIcons = "Icons/Rating.png",
                Category = Categories.Editors,
                SearchKeys = new string[] { "Rating", "Editor", "started" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Input.SpellChecker.SpellCheckerView).AssemblyQualifiedName,
                Header = "Getting Started",
                Product = "SpellChecker",
                ProductIcons = "Icons/SpellChecker.png",
                Category = Categories.Miscellaneous,
                Description = "This sample demonstrates how the SpellChecker control provides suggestions for the misspelt words. ",
            
            SearchKeys = new string[] { "SpellChecker", "Editor", "started", "Spell", "Checker", "AutoCorrect", "Correct", "Word" }
            });
#endregion

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Category = Categories.Editors,
                Product = "Picker",
                Description= "The picker control allows user to pick an item among a list of Items that can be customized or templated with custom view. This control can be opened as dialog. Its rich feature set includes functionalities like item template, data binding, multi column, header / footer, custom view on header / footer and default validation buttons",
                SampleView = typeof(Input.Picker.GettingStarted).AssemblyQualifiedName,
                Tag = Tags.None,
                SearchKeys = new string[] { "Picker", "started", "Getting", "SfPicker" },
                  DesktopImage = "ms-appx:///WhatsNewImage/getting.png",
                MobileImage = "ms-appx:///WhatsNewImage/getting-mobile.png",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Popup Picker",
                Category = Categories.Editors,
                Product = "Picker",
                SampleView = typeof(Input.Picker.PopupPicker).AssemblyQualifiedName,
                Tag = Tags.None,
                SearchKeys = new string[] { "Picker", "Popup", "Popuppicker", "SfPicker" },
                  DesktopImage = "ms-appx:///WhatsNewImage/popup.png",
                MobileImage = "ms-appx:///WhatsNewImage/popup-mobile.png",
            });

            SampleHelper.SetTagsForProduct("MaskedEdit", Tags.Updated);
        }
    }
}
