using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter.Classes.StartPage
{
    internal class StartPageData
    {
        private static readonly string[] dataTypeChoice = { "gfs", "rep" };
        private static string[] languageChoices = { "Deutsch", "English", "Espanol" };
        public string[] GetLanguageChoises()
        {//Attention! if the strings get altered they have to get altered down below in the switch aswell

            string[] languageChoises = languageChoices; //Kurze Version
            return languageChoises;
        }
        public string[] GetDataTypeChoice() => dataTypeChoice; //rep or gfs selection

    }
}
