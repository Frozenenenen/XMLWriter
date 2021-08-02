using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter
{
    class Language
    {
        //General UI
        private static string language; 
        private static string defaultNotice;
        private static string save;
        private static string next;
        private static string back;
        private static string pleaseFill;
        private static string createDataSet;
        private static string step;
        private static string steps;
        private static string summary;
        private static string reset;
        private static string fileNameTitel;
        /*private static string model; //Vermutlich nicht mehr drin
        private static string vin;*/
        //Rep- and GFS-specific
        private static string description;
        private static string anim;
        //rep-specific
        private static string specialStep;
        //GFS-Specific
        private static string instructions;
        private static string posID;
        private static string negID;
        private static string posResult;
        private static string repXML;
        private static string actuatorTest;
        private static string readData;
        private static string smartTool;
        private static string nextStep;
        private static string lastStep;

        private static string rep; //Noch drin?
        
        







        public string GetStringLingo() => language;
        public string GetInstructionNotice() => defaultNotice;
        public string GetStringSave() => save;
        public string GetStringNext() => next;
        public string GetStringBack() => back;
        /*public string GetStringModel() => model;
        public string GetStringVin() => vin;*/
        public string GetStringPleaseFill() => pleaseFill;
        public string GetStringCreateDataSet() => createDataSet;
        public string GetStringStep() => step;
        public string GetStringSteps() => steps;
        public string GetStringContent() => description;
        public string GetStringSpecialStep() => specialStep;
        public string GetStringInstructions() => instructions;
        public string GetStringAnim() => anim;
        public string GetStringSummary() => summary;
        public string GetStringReset() => reset;
        public string GetStringRep() => rep;
        public string GetStringPosID() => posID;
        public string GetStringNegID() => negID;
        public string GetStringPosResult() => posResult;
        public string GetStringRepXML() => repXML;
        public string GetStringActuatorTest() => actuatorTest;
        public string GetStringReadData() => readData;
        public string GetSmartTool() => smartTool;
        public string GetStringNextStep() => nextStep;
        public string GetStringLastStep() => lastStep;
        public string GetStringFileNameTitel() => fileNameTitel;





        //public void SetLingo(string inputLanguage) => language = inputLanguage;

        //public string[] GetLingoChoises() => File.ReadAllLines(@"Testgelände\languages.txt"); //Falls mal die SPrache aus einer Textdatei gezogen werden soll
        public string[] GetLingoChoises()
        {//Attention! if the strings get altered they have to get altered down below in the switch aswell

            string[] lingoChoises = { "Deutsch", "English", "Espanol" }; //Kurze Version
            return lingoChoises;
        }


        public void InitLingo(string lingoChoice) //Method to fill the variables with the language specific words
        {
            switch (lingoChoice)
            {
                case "Deutsch":
                    InitGerman();

                    break;

                case "English":
                    InitEnglish();
                    break;

                case "Espanol":
                    InitEspanol();
                    break;

                default:
                    InitGerman();
                    break;

            }
        }
        void InitGerman()
        {
            language = "Deutsch";
            defaultNotice = "Leere Felder werden automatisch mit defaultwert befüllt.";
            save = "Sichern";
            next = "Weiter";
            back = "Zurück";
            /*model = "Modell";
            vin = "FIN";*/
            pleaseFill = "Bitte ausfüllen";
            createDataSet = "Datensatz anlegen";
            step = "Schritt";
            steps = "Schritte";
            description = "Beschreibung";
            specialStep = "Sonderschritt";
            anim = "Animation";
            summary = "Übersicht";
            reset = "löschen";
            rep = "Reparatur";
            instructions = "Arbeitsanweisungen";
            posID = "Positive ID";
            negID = "Negative ID";
            posResult = "Positives Ergebnis";
            repXML = "RepXML";
            actuatorTest = "Aktortest";
            readData = "Daten Lesen (RDBI)";
            smartTool = "SmartTool";
            nextStep = "Nächster Schritt";
            lastStep = "Letzter Schritt";
            fileNameTitel = "Dateiname. ggf. mit Pfad.";
        }
        void InitEnglish()
        {
            language = "English";
            defaultNotice = "Empty boxes will be automatically filled with default values.";
            save = "Save";
            next = "Next";
            back = "Back";
            /*model = "Model";
            vin = "VIN";*/
            pleaseFill = "Please Fill";
            createDataSet = "Create Data Set";
            step = "Step";
            steps = "Steps";
            description = "Description";
            specialStep = "Special step";
            anim = "Animation";
            summary = "Summary";
            reset = "Reset";
            rep = "Repair";
            instructions = "Instructions";
            posID = "Positive ID?";
            negID = "Negative ID?";
            posResult = "Positives result?";
            lastStep = "Last Step?";
            repXML = "RepXML";
            actuatorTest = "Actuator test?";
            readData = "Read data?";
            smartTool = "SmartTool";
            nextStep = "Next Step?";
            fileNameTitel = "File Name with or without path";
        }
        void InitEspanol()
        {
            language = "Espanol"; //Meinem Spanisch sollte man nicht trauen! Das ist hauptsächlich zu Test- und Vorführzwecken hier
            defaultNotice = "Empty boxes will be automatically filled with default values.";
            save = "Grabar algo";
            next = "Próximo";
            back = "Atrás";
            /*model = "Modelo";
            vin = "VIN";*/
            pleaseFill = "Llenar algo, por favor";
            createDataSet = "Crear expediente";
            step = "Paso";
            steps = "Pasos";
            description = "Descripción";
            specialStep = "especial paso";
            anim = "Animación";
            summary = "Resumen";
            reset = "Reset";
            rep = "Reparo";
            instructions = "Ensenanza";
            posID = "Positive ID?";
            negID = "Negative ID?";
            posResult = "Positives Ergebnis?";
            repXML = "RepXML";
            actuatorTest = "Aktortest?";
            readData = "Daten Lesen (RDBI?)";
            smartTool = "SmartTool";
            nextStep = "Nächster Schritt?";
            lastStep = "last Step?";
            fileNameTitel = "File Name with or without path";
        }
    }
}
