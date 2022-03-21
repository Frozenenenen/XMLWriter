using System.Windows.Controls;
using XMLWriter.Classes.HelpClasses;

namespace XMLWriter.Classes.StartPage {
    /// <summary>
    /// Helferlogik für StartPage.xaml.cs
    /// </summary>
    internal class StartPageHelper {
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        DropDownOptionLists dropDownLists = new DropDownOptionLists();
        LoadHelper loadHelper = new LoadHelper();
        UtilityFunctions utility = new UtilityFunctions();
        Language language = new Language();
        DataSetService dataSetService = new DataSetService();
        GUIMovementHelper guiMovementHelper = new GUIMovementHelper();

        private static readonly string[] processTypeList = { "gfs", "rep" };
        private static readonly string[] languageList = { "Deutsch", "English", "Espanol" };
        private static string selectedLanguage = languageList[0];
        private static string selectedProcessType = "rep";
        private static bool dataBaseisCheched = false;
        //Labels
        private static string stringCreateDataSet;
        private static string stringFilePath;
        private static string stringDisplaySteps;
        private static string stringChecked;
        private static string stringUnchecked;
        //Buttons
        private static string stringStart = "--->";
        private static string stringLoadFile;
        private static string stringDeleteSets;

        //----------------------Bearbeitungsbereich-----------------------

        //----------------------Bearbeitungsbereich-----------------------

        ///---Hilfsfunktionen---///
        public void CheckForDataBaseOrTxt(bool? check) {
            if (check == true) {
                dropDownLists.SetUseDataBaseTrue();
            }
            else {
                dropDownLists.SetUseDataBaseFalse();
            }
        }
        public void Reset(TextBlock textBlock) {
            dataSetService.ResetDataSet();
            xamlHelper.SetTextFor(textBlock, "");
        }
        public void LoadDataFromFile() {
            LoadDataService loadDataService = new LoadDataService();
            loadDataService.LoadDataFromFile();
        }

        ///---Inits von Werten---///
        public void InitNewDataSet() {
            guiMovementHelper.ResetStepCount(); //redundant
            
            dataSetService.InitNewDataSetWhereRequired();
        }
        public void LoadDropDownOptions() {//This load the objectlist for use in gfs dropdowns from text files or from a database
            dropDownLists.LoadAllDropDownOptionsFromTxtOrDataBase();
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
            stringDeleteSets = language.GetStringDeleteSet();
        }

        ///---Dropdown Getter---///
        public void SetLangauge(string _language) {
            if (utility.ArrayContainsElement(languageList, _language)) {
                selectedLanguage = _language;
                language.InitLanguage(selectedLanguage);
                InitDisplayText();
            }
        }
        public string GetSelectedProcessType() => selectedProcessType;
        public string SetSelectedProcessType(string _selectedProcessType) => selectedProcessType = _selectedProcessType;

        ///---Inits bzw Sets von Display-Elementen---///
        //Set or Init Labels
        public void SetTitelText(Label labelTitel) {
            xamlHelper.SetTextFor(labelTitel, stringCreateDataSet);
        }
        public void SetLoadFileText(Label labelLoadFile) {
            xamlHelper.SetTextFor(labelLoadFile, stringFilePath);
        }
        public void SetDisplayStepsText(Label labelStepCount) {
            xamlHelper.SetTextFor(labelStepCount, stringDisplaySteps + ": " + (dataSetService.GetDataSets().Count));
        }
        //Init Buttons
        public void SetStartButtonText(Button button) {
            xamlHelper.SetTextFor(button, stringStart);
        }
        public void SetLoadButtonText(Button button) {
            xamlHelper.SetTextFor(button, stringLoadFile);
        }
        public void SetResetButtonText(Button button) {
            xamlHelper.SetTextFor(button, stringDeleteSets);
        }
        //Init DropDowns
        public void InitProcessTypeDropDown(ComboBox comboBox) {
            xamlHelper.SetDropDownContent(comboBox, processTypeList);
            xamlHelper.SetDropDownActiveELementFor(comboBox, selectedProcessType);
        }
        public void InitLanguageSelectionDropDown(ComboBox comboBox) {
            xamlHelper.SetDropDownContent(comboBox, languageList);
            xamlHelper.SetDropDownActiveELementFor(comboBox, selectedLanguage);
        }
        public void ChangeProcessActiveElement(ComboBox comboBox, string selectedElement) {
            xamlHelper.SetDropDownActiveELementFor(comboBox, selectedElement);
            SetSelectedProcessType(xamlHelper.GetActiveElementOf(comboBox));
        }
        //Init CheckBoxes
        public void SetTxtOrDataBaseCheckBoxText(Label check, Label uncheck) {
            xamlHelper.SetTextFor(check, stringChecked);
            xamlHelper.SetTextFor(uncheck, stringUnchecked);
        }
        public void InitDataBaseCheckBox(CheckBox checkBox) {
            if (dropDownLists.GetUseDataBase()) {
                checkBox.IsChecked = true;
            }
            else {
                checkBox.IsChecked = false;
            }
            
        }
        //Init TextBlock
        public void SetFilePathText(TextBlock text) {
            xamlHelper.SetTextFor(text, loadHelper.GetFileNameAndPath());
        }
        //Checks
        public bool IsRepSelected() {
            if (selectedProcessType == processTypeList[1]) {
                return true;
            }
            return false;
        }
        public bool IsGfsSelected() {
            if (selectedProcessType == processTypeList[0]) {
                return true;
            }
            return false;
        }
    }
}
