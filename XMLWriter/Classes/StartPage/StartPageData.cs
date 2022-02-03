using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter.Classes.StartPage
{
    internal class StartPageData
    {
        UtilityFunctions utility = new UtilityFunctions();
        Language language = new Language();

        private static string startPageTitel = Language.GetStringCreateDataSet();
        private static readonly string[] processTypeList = { "gfs", "rep" };
        private static readonly string[] languageList = { "Deutsch", "English", "Espanol" };
        private string selectedLanguage = languageList[0];
        private string selectedProcessType = "rep";

        public string GetStartPageTitel() => startPageTitel;
        public string[] GetLanguageList() => languageList;
        public string GetSelectedLanguage() => selectedLanguage;
        public void SetLangauge(string _language)
        {
            if (utility.ArrayContainsElement(languageList, _language))
            {
                selectedLanguage = _language;
                language.InitLanguage(selectedLanguage);
                startPageTitel = Language.GetStringCreateDataSet();
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
