using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter
{
    class WriteToXML
    {
        //Hier muss ich wohl drigend nochmal nachgucken, wie man eine Pfadsuche öffnet, wie man das normalerweise so macht
        //Der Ordner muss bestehe, damit das funktioniert!
        protected static readonly string pathVehicleID = @"eGolf";
        protected static readonly string pathLanguage = @"de";
        protected static readonly string fileExtension = ".txt"; //später .xml

        public string[] GetPathVehicleIDChoises()
        {//Attention! if the strings get altered they have to get altered down below in the switch aswell

            string[] languageChoises = { "eGolf", "anderesFahrzeug", "DrittesFahrzeug" }; //Kurze Version
            return languageChoises;
        }
        public string[] GetPathLanguageChoises()
        {//Attention! if the strings get altered they have to get altered down below in the switch aswell

            string[] languageChoises = { "de", "en", "es" }; //Kurze Version
            return languageChoises;
        }

        public virtual void FillList()
        {
        }
        public virtual void OutputToXML()
        {
        }

        public string SetFileName(string inputFileName, string dataType) //Damit keine vorherigen Daten überschrieben werden, wird der Dateiname iteriert, bis ein neuer Dateiname gefunden wurde.
        {
            string tempFileName;
            int i = 2;
            while (true)
            {

                if (!File.Exists(pathVehicleID + "/" + pathLanguage + "/" + inputFileName + "_" + pathLanguage + fileExtension) && i == 2)
                {
                    return inputFileName;
                }
                else
                {
                    tempFileName = inputFileName + Convert.ToString(i);
                    if (!File.Exists(pathVehicleID + "/" + pathLanguage + "/" + inputFileName + "_" + pathLanguage + fileExtension))
                    {
                        return tempFileName;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
    }
}
