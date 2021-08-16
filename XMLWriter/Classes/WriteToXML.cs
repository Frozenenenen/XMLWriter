﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter
{
    class WriteToXML
    {
        //Todo: Pfadsuche für's Speichern
        //Der Ordner muss bestehen, damit das Speichern funktioniert!
        protected static readonly string pathVehicleID = @"eGolf";
        protected static readonly string pathLanguage = @"de";
        protected static readonly string fileExtension = ".txt"; //später .xml

        public string[] GetPathVehicleIDChoises()
        {

            string[] languageChoises = { "eGolf", "anderesFahrzeug", "DrittesFahrzeug" };
            return languageChoises;
        }
        public string[] GetPathLanguageChoises()
        {

            string[] languageChoises = { "de", "en", "es" };
            return languageChoises;
        }

        public virtual void FillList()
        {
        }
        public virtual void OutputToXML()
        {
        }

        public string SetFileName(string inputFileName)//, string dataType) //Damit keine vorherigen Daten überschrieben werden, wird der Dateiname iteriert, bis ein neuer Dateiname gefunden wurde.
        {
            /*string tempFileName;
            int i = 2;
            while (true)
            {

                if (!File.Exists(pathVehicleID + "/" + pathLanguage + "/" + inputFileName + "_" + pathLanguage + "" + fileExtension) && i == 2)
                {
                    return inputFileName;
                }
                else
                {
                    tempFileName = inputFileName + Convert.ToString(i); //adds a number to the fileName - incrementing with each go through
                    if (!File.Exists(pathVehicleID + "/" + pathLanguage + "/" + inputFileName + "_" + pathLanguage + "" + fileExtension))
                    {
                        return tempFileName;
                    }
                    else
                    {
                        i++;
                    }
                }
            }*/
            return inputFileName;
        }
    }
}
