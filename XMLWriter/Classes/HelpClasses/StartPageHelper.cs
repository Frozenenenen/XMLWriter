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
        DataSets data = new DataSets();
        Language language = new Language();
        LoadDataSet loadData = new LoadDataSet();
        UtilityFunctions utility = new UtilityFunctions();
        DropDownOptionLists loadInput = new DropDownOptionLists();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        DropDownOptionLists dropDownLists = new DropDownOptionLists();


        private static readonly string[] processTypeList = { "gfs", "rep" };
        private static readonly string[] languageList = { "Deutsch", "English", "Espanol" };
        private string selectedLanguage = languageList[0];
        private string selectedProcessType="rep";
        //Labels
        private static string stringCreateDataSet;
        private static string strinfilePath;
        private static string stringDisplaySteps;
        private static string stringChecked;
        private static string stringUnchecked;
        //Buttons
        private static string stringStart ="--->";
        private static string stringLoadFile;
        private static string stringDeleteSet;
        //TextBox

        //private static string stringSaveSet;


        //----------------------Bearbeitungsbereich-----------------------
        //----------------------Bearbeitungsbereich-----------------------
        //----------------------Bearbeitungsbereich-----------------------
        public void LoadDataFromFile()
        {
            loadData.LoadDataFromFile();
        }

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
        public void ChangeDropDownContentActiveElement(ComboBox comboBox, string selectedElement)
        {
            xamlHelper.SetDropDownActiveELementFor(comboBox, selectedElement);
            data.SetDataType(selectedElement);
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
        public void SetDisplayStepsText(Label labelStepCount)
        {
            xamlHelper.SetTextFor(labelStepCount, stringDisplaySteps + ": " + (data.GetStepCountMax() + 1));
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
        public void SetResetButtonText(Button button)
        {
            xamlHelper.SetTextFor(button, stringDeleteSet);
        }
        //Init DropDowns
        public void InitProcessTypeDropDown(ComboBox comboBox)
        {
            System.Diagnostics.Debug.WriteLine("Prozesstyp: " + data.GetDataType());
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
        //Init TextBlock
        public void SetFilePathText(TextBlock text)
        {
            xamlHelper.SetTextFor(text, loadData.GetFileNameAndPath());
        }
    }
}
