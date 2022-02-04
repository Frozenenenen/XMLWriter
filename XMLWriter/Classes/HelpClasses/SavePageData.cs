using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter.Classes.StartPage
{
    internal class SavePageData
    {
        Language language = new Language();
        private static string savePageStringFileNameTitel;
        private static string savePageStringSteps;

        public void InitSavePageData()
        {
            savePageStringFileNameTitel = language.GetStringFileNameTitel();
            savePageStringSteps = language.GetStringSteps() + " ";
        }

        public string GetStringFileNameTitel() => savePageStringFileNameTitel;
        public string GetStringSteps() => savePageStringSteps;
    }
}
