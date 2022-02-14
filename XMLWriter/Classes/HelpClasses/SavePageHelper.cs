using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter.Classes.StartPage
{
    internal class SavePageHelper
    {
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
