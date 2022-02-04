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
        //Labels
        private static string stringTitel;
        private static string stringGeneralInstruction;
        private static string stringGeneralInstructionText;
        private static string stringFileNameTitel;
        private static string stringDisplayStep;
        //Buttons
        private static string stringLoadFile;
        private static string stringDeleteSet;



        public void InitStartPageDataStrings()
        {
            stringTitel = language.GetStringCreateDataSet();
            stringGeneralInstruction = language.GetStringGeneralInstruction();
            stringGeneralInstructionText = language.GetStringGeneralInstructionText();
            stringFileNameTitel = language.GetStringFileNameTitel();
            stringDisplayStep = language.GetStringSteps() + " ";
        }

        //Getter for Displaying Labels
        public string GetTextTitel() => stringTitel;
        public string GetTextGeneralInstruction() => stringGeneralInstruction;
        public string GetTextGeneralInstructionText() => stringGeneralInstructionText;
        public string GetTextFileNameTitel() => stringFileNameTitel;
        public string GetTextDisplayStep() => stringDisplayStep;
        //Getter for Displaying Buttons
        public string GetTextLoadFile() => stringLoadFile;
        public string GetTextDeleteSet() => stringDeleteSet;

        //Variable Getter
        public string[] GetLanguageList() => languageList;
        public string GetSelectedLanguage() => selectedLanguage;
        public void SetLangauge(string _language)
        {
            if (utility.ArrayContainsElement(languageList, _language))
            {
                selectedLanguage = _language;
                language.InitLanguage(selectedLanguage);
                stringTitel = language.GetStringCreateDataSet();
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
