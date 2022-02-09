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
        DataSetService dataManager = new DataSetService();
        GUIMovementHelper gui = new GUIMovementHelper();

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
            xamlHelper.SetTextFor(button, stringDeleteSets);
        }
        //Init DropDowns
        public void InitProcessTypeDropDown(ComboBox comboBox) {
            System.Diagnostics.Debug.WriteLine("Prozesstyp: " + dataManager.GetDataType());
            xamlHelper.SetDropDownListFor(comboBox, processTypeList);
            xamlHelper.SetDropDownActiveELementFor(comboBox, dataManager.GetDataType());
        }
        public void InitLanguageSelectionDropDown(ComboBox comboBox) {
            xamlHelper.SetDropDownListFor(comboBox, languageList);
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
