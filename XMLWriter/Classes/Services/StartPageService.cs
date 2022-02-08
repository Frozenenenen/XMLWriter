using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Navigation;
using XMLWriter.Classes.HelpClasses;
using XMLWriter.Pages;

namespace XMLWriter.Classes.StartPage {
    /// <summary>
    /// Helferlogik für StartPage.xaml.cs
    /// </summary>
    internal class StartPageService {
        DropDownOptionLists dropDownLists = new DropDownOptionLists();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        DropDownOptionLists loadInput = new DropDownOptionLists();
        LoadHelper loadHelper = new LoadHelper();
        UtilityFunctions utility = new UtilityFunctions();
        Language language = new Language();
        DataSetService dataManager = new DataSetService();
        GUIMovementService gui = new GUIMovementService();

        private static readonly string[] processTypeList = { "gfs", "rep" };
        private static readonly string[] languageList = { "Deutsch", "English", "Espanol" };
        private string selectedLanguage = languageList[0];
        private string selectedProcessType = "rep";
        //Labels
        private static string stringCreateDataSet;
        private static string stringFilePath;
        private static string stringDisplaySteps;
        private static string stringChecked;
        private static string stringUnchecked;
        //Buttons
        private static string stringStart = "--->";
        private static string stringLoadFile;
        private static string stringDeleteSet;

        //----------------------Bearbeitungsbereich-----------------------

        //----------------------Bearbeitungsbereich-----------------------

        ///---Hilfsfunktionen---///
        public void DatabaseOrTxtCheck(bool? check) {
            if (check == true) {
                loadInput.SetUseDataBaseTrue();
            }
            else {
                loadInput.DontUseDataBase();
            }
        }
        public void Reset(TextBlock textBlock) {
            dataManager.ResetDataSet();
            textBlock.Text = "";
        }
        public void LoadDataFromFile() {
            LoadDataService loadDataService = new LoadDataService();
            loadDataService.LoadDataFromFile();
        }

        ///---Inits von Werten---///
        public void InitNewDataSet() {
            dataManager.InitNewDataSet();

        }
        public void LoadDropDownOptions() {//This load the objectlist for use in gfs dropdowns from text files or from a database
            dropDownLists.LoadAllDropDownOptionsFromTxtOrDatabase();
        }
        public void InitLanguages() {
            language.InitLanguage(selectedLanguage);
            InitDisplayText();
        }
        private void InitDisplayText() {
            //Labels
            stringCreateDataSet = language.GetStringCreateDataSet();
            stringFilePath = language.GetStringFilePath();
            stringDisplaySteps = language.GetStringSteps();
            stringChecked = language.GetStringUseDataBaseChecked();
            stringUnchecked = language.GetStringUseDataBaseUnchecked();
            //Buttons
            stringLoadFile = language.GetStringLoadFile();
            stringDeleteSet = language.GetStringDeleteSet();
        }

        ///---Dropdown Getter---///
        public void SetLangauge(string _language) {
            if (utility.ArrayContainsElement(languageList, _language)) {
                selectedLanguage = _language;
                language.InitLanguage(selectedLanguage);
                InitDisplayText();
            }

        }
        public string[] GetProcessTypeList() => processTypeList;
        public string GetSelectedProcessType() => selectedProcessType;

        ///---Inits bzw Sets von Display-Elementen---///
        //Set or Init Labels
        public void SetTitelText(Label labelTitel) {
            xamlHelper.SetTextFor(labelTitel, stringCreateDataSet);
        }
        public void SetLoadFileText(Label labelLoadFile) {
            xamlHelper.SetTextFor(labelLoadFile, stringFilePath);
        }
        public void SetDisplayStepsText(Label labelStepCount) {
            xamlHelper.SetTextFor(labelStepCount, stringDisplaySteps + ": " + (gui.GetStepCountMax() + 1));
        }
        //Init Buttons
        public void SetStartButtonText(Button button) {
            xamlHelper.SetTextFor(button, stringStart);
        }
        public void SetLoadButtonText(Button button) {
            xamlHelper.SetTextFor(button, stringLoadFile);
        }
        public void SetResetButtonText(Button button) {
            xamlHelper.SetTextFor(button, stringDeleteSet);
        }
        //Init DropDowns
        public void InitProcessTypeDropDown(ComboBox comboBox) {
            System.Diagnostics.Debug.WriteLine("Prozesstyp: " + dataManager.GetDataType());
            xamlHelper.SetDropdownListFor(comboBox, processTypeList);
            xamlHelper.SetDropDownActiveELementFor(comboBox, dataManager.GetDataType());
        }
        public void InitLanguageSelectionDropDown(ComboBox comboBox) {
            xamlHelper.SetDropdownListFor(comboBox, languageList);
            xamlHelper.SetDropDownActiveELementFor(comboBox, selectedLanguage);
        }
        public void ChangeProcessActiveElement(ComboBox comboBox, string selectedElement) {
            xamlHelper.SetDropDownActiveELementFor(comboBox, selectedElement);
            dataManager.SetDataType(selectedElement);
        }
        //Init CheckBoxes
        public void SetTxtOrDataBaseCheckBoxText(Label check, Label uncheck) {
            xamlHelper.SetTextFor(check, stringChecked);
            xamlHelper.SetTextFor(uncheck, stringUnchecked);
        }
        //Init TextBlock
        public void SetFilePathText(TextBlock text) {
            xamlHelper.SetTextFor(text, loadHelper.GetFileNameAndPath());
        }
    }
}
