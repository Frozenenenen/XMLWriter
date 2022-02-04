using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Navigation;
using XMLWriter.Pages;

namespace XMLWriter.Classes.StartPage
{
    internal class StartPageHelper
    {
        UtilityFunctions utility = new UtilityFunctions();
        Language language = new Language();
        DropDownOptionLists loadInput = new DropDownOptionLists();
        DataSets data = new DataSets();
        DropDownOptionLists dropDownLists = new DropDownOptionLists();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();


        private static readonly string[] processTypeList = { "gfs", "rep" };
        private static readonly string[] languageList = { "Deutsch", "English", "Espanol" };
        private string selectedLanguage = languageList[0];
        private string selectedProcessType;
        //Labels
        private static string stringCreateDataSet;
        private static string strinfilePath;
        private static string stringDisplaySteps;
        private static string stringChecked;
        private static string stringUnchecked;
        //Buttons
        private static string stringStart ="--->";
        private static string stringSelect = "Select";
        private static string stringLoadFile;
        private static string stringDeleteSet;
        //TextBox

        //private static string stringSaveSet;


        //----------------------Bearbeitungsbereich-----------------------
        //----------------------Bearbeitungsbereich-----------------------
        //----------------------Bearbeitungsbereich-----------------------


        //----------------------Bearbeitungsbereich-----------------------
        //----------------------Bearbeitungsbereich-----------------------
        //----------------------Bearbeitungsbereich-----------------------


        private void InitDisplayText()
        {
            stringCreateDataSet = language.GetStringCreateDataSet();
            strinfilePath = language.GetStringFilePath();
            stringDisplaySteps = language.GetStringSteps();
            stringChecked = language.GetStringUseDataBaseChecked();
            stringUnchecked = language.GetStringUseDataBaseUnchecked();
            //Buttons
            stringLoadFile = language.GetStringLoadFile();
            stringDeleteSet = language.GetStringDeleteSet();
    }
        public void SetLabelContent(Label label, string text)
        {
            xamlHelper.SetTextFor(label, text);
        }
        public void SetButtonContent(Button button, string text)
        {
            xamlHelper.SetTextFor(button, text);
        }
        public void SetTextBlockContent(TextBlock textBlock, string text)
        {
            xamlHelper.SetTextFor(textBlock, text);
        }
        public void SetDropDownContent(ComboBox comboBox, string[] array, string selectedElement)
        {
            xamlHelper.SetDropdownListFor(comboBox, array);
            xamlHelper.SetDropDownActiveELementFor(comboBox, selectedElement);
        }
        public void ChangeDropDownContentActiveElement(ComboBox comboBox, string selectedElement)
        {
            xamlHelper.SetDropDownActiveELementFor(comboBox, selectedElement);
        }

         //Getter for Displaying Labels
        public string GetTextTitel() => stringCreateDataSet;
        public string GetTextFileNameTitel() => strinfilePath;
        public string GetTextDisplayStep() => stringDisplaySteps;
        //Getter for Displaying Buttons
        public string GetTextLoadFile() => stringLoadFile;
        public string GetTextDeleteSet() => stringDeleteSet;

        //Dropdown Getter & Setter
        public string[] GetLanguageList() => languageList;
        public string GetSelectedLanguage() => selectedLanguage;
        public void SetLangauge(string _language)
        {
            if (utility.ArrayContainsElement(languageList, _language))
            {
                selectedLanguage = _language;
                language.InitLanguage(selectedLanguage);
                InitDisplayText();
            }

        } 
        public string[] GetProcessTypeList() => processTypeList;
        public string GetSelectedProcessType() => selectedProcessType;

        public void InitNewDataSet()
        {
            data.InitNewDataSet();

        }
        public void LoadDropDownOptions()
        {//This load the objectlist for use in gfs dropdowns from text files or from a database
            dropDownLists.LoadAllDropDownOptionsFromTxtOrDatabase();
        }
        public void InitLanguages()
        {
            language.InitLanguage(selectedLanguage);
            InitDisplayText();
        }

        public void DatabaseOrTxtCheck(bool? check)
        {
            if (check == true)
            {
                loadInput.UseDataBase();
            }
            else
            {
                loadInput.DontUseDataBase();
            }
        }
        public void Reset(TextBlock textBlock)
        {
            data.ResetDataSet();
            textBlock.Text = "";
        }


        //Set or Init Labels
        public void SetTitelText(Label labelTitel)
        {
            xamlHelper.SetTextFor(labelTitel, stringCreateDataSet);
        }
        public void SetLoadFileText(Label labelLoadFile)
        {
            xamlHelper.SetTextFor(labelLoadFile, stringLoadFile);
        }
        public void SetDisplayStepText(Label labelStepCount)
        {
            xamlHelper.SetTextFor(labelStepCount, stringDisplaySteps + data.GetStepCount());
        }
        //Init Buttons
        public void SetStartButtonText(Button button)
        {
            xamlHelper.SetTextFor(button, stringStart);
        }
        public void SetLoadButtonText(Button button)
        {
            xamlHelper.SetTextFor(button, stringLoadFile);
        }
        public void SetLanguageSelectButtonText(Button button)
        {
            xamlHelper.SetTextFor(button, stringSelect);
        }
        public void SetResetButtonText(Button button)
        {
            xamlHelper.SetTextFor(button, stringDeleteSet);
        }
        //Init DropDowns
        public void InitProcessTypeDropDown(ComboBox comboBox)
        {
            xamlHelper.SetDropdownListFor(comboBox, processTypeList);
            xamlHelper.SetDropDownActiveELementFor(comboBox, data.GetDataType());
        }
        public void InitLanguageSelectionDropDown(ComboBox comboBox)
        {
            xamlHelper.SetDropdownListFor(comboBox, languageList);
            xamlHelper.SetDropDownActiveELementFor(comboBox, selectedLanguage);
        }
        //Init CheckBoxes
        public void SetTxtOrDataBaseCheckBoxText(Label check, Label uncheck)
        {
            xamlHelper.SetTextFor(check, stringChecked);
            xamlHelper.SetTextFor(uncheck, stringUnchecked);
        }
        //Init TextBlock - 
    }
}
