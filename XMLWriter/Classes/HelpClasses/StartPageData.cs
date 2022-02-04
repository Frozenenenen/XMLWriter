using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter.Classes.StartPage
{
    internal class StartPageData
    {
        UtilityFunctions utility = new UtilityFunctions();
        Language language = new Language();
        DataSets data = new DataSets();

        private static readonly string[] processTypeList = { "gfs", "rep" };
        private static readonly string[] languageList = { "Deutsch", "English", "Espanol" };
        private string selectedLanguage = languageList[0];
        private string selectedProcessType = "rep";

        private static string startPageTitel;
        private static string startPageGeneralInstruction;
        private static string startPageGeneralInstructionText;
        private static string startPageFileNameTitel;
        private static string startPageDisplayStep;

        

        public void InitStartPageDataStrings()
        {
            startPageTitel = language.GetStringCreateDataSet();
            startPageGeneralInstruction = language.GetStringGeneralInstruction();
            startPageGeneralInstructionText = language.GetStringGeneralInstructionText();
            startPageFileNameTitel = language.GetStringFileNameTitel();
            startPageDisplayStep = language.GetStringSteps() + " ";
        }

        //Getter for Displaying Text
        public string GetStringTitel() => startPageTitel;
        public string GetStringGeneralInstruction() => startPageGeneralInstruction;
        public string GetStringGeneralInstructionText() => startPageGeneralInstructionText;
        public string GetStringFileNameTitel() => startPageFileNameTitel;
        public string GetStringDisplayStep() => startPageDisplayStep;

        //Variable Getter
        public string[] GetLanguageList() => languageList;
        public string GetSelectedLanguage() => selectedLanguage;
        public void SetLangauge(string _language)
        {
            if (utility.ArrayContainsElement(languageList, _language))
            {
                selectedLanguage = _language;
                language.InitLanguage(selectedLanguage);
                startPageTitel = language.GetStringCreateDataSet();
            }

        } 
        
        
        public string[] GetProcessTypeList() => processTypeList;
        public string GetSelectedProcessType() => selectedProcessType;
        public void SetProcessType(string _processType)
        {
            if (utility.ArrayContainsElement(processTypeList, _processType))
            {
                selectedProcessType = _processType;
            }
        }

        
    }
}
