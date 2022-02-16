using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter.Classes.StartPage
{
    internal class SavePageHelper
    {
        DataSetService data = new DataSetService();
        GUIMovementHelper gui = new GUIMovementHelper();
        Language language = new Language();
        LoadHelper loadHelper = new LoadHelper();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        SavePageHelper savePageHelper = new SavePageHelper();
        Language language = new Language();
        private static string TextFileNameTitel;
        private static string TextStringSteps;

        public void InitSavePageData()
        {
            TextFileNameTitel = language.GetStringFilePath();
            TextStringSteps = language.GetStringSteps() + " ";
        }

        public string GetTextFileNameTitel() => TextFileNameTitel;
        public string GetTextSteps() => TextStringSteps;
    }
}
