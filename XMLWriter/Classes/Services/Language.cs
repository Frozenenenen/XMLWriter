
namespace XMLWriter
{
    class Language
    {
        //startPage
        private static string createDataSet = "";
        private static string useDataBaseChecked;
        private static string useDataBaseUnchecked;
        private static string reset;
        private static string deleteSet;

        //StartPage & SavePage
        private static string loadFile;
        private static string filePath;
        //General UI
        private static string filePathDialog;
        private static string saveFile;
        private static string next;
        private static string back;
        private static string pleaseFill;
        private static string stepTitel;
        private static string steps;
        private static string summary;
        private static string pleaseChoose;
        private static string optional;
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


        //Used in 3 or more
        public string GetStringSave() => saveFile;
        public string GetStringBack() => back;
        //StartPage & SavePage
        public string GetStringSteps() => steps;
        public string GetStringFilePath() => filePath;
        //StartPage
        public string GetStringCreateDataSet() => createDataSet;
        public string GetStringLoadFile() => loadFile;
        public string GetStringUseDataBaseChecked() => useDataBaseChecked;
        public string GetStringUseDataBaseUnchecked() => useDataBaseUnchecked;
        public string GetStringDeleteSet() => deleteSet;

        //SavePage
        public string GetStringFilePathDialog() => filePathDialog;
        //gfs/rep spec
        public string GetStringStepTitel() => stepTitel;
        public string GetStringPleaseFill() => pleaseFill;
        public string GetStringContent() => description;
        public string GetStringAnim() => anim;
        public string GetStringReset() => reset;
        public string GetStringNext() => next;
        //rep only
        public string GetStringSpecialStep() => specialStep;
        //gfs only
        public string GetStringOptional() => optional;
        public string GetStringPleaseChoose() => pleaseChoose;
        public string GetStringInstruction() => instructions;
        public string GetStringSummary() => summary;
        public string GetStringPosID() => posID;
        public string GetStringNegID() => negID;
        public string GetStringPosResult() => posResult;
        public string GetStringRepXML() => repXML;
        public string GetStringActuatorTest() => actuatorTest;
        public string GetStringReadData() => readData;
        public string GetStringSmartTool() => smartTool;
        public string GetStringNextStep() => nextStep;
        public string GetStringLastStep() => lastStep;
        

        public void InitLanguage(string languageChoice) //Method to fill the variables with the language specific words
        {
            switch (languageChoice)
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
        private void InitGerman()
        {
            loadFile = "Datei Laden?";
            filePathDialog = "Dateibrowser";
            saveFile = "Speichern";
            next = "Weiter";
            back = "Zurück";
            pleaseFill = "Bitte ausfüllen";
            createDataSet = "Datensatz anlegen oder laden";
            pleaseChoose = "Bitte wählen";
            optional = "Optional: direkter input. Priorisiert, falls befüllt!";
            stepTitel = "Schrittbezeichnung";
            steps = "Schritte";
            description = "Beschreibung";
            specialStep = "Sonderschritt";
            anim = "Animation";
            summary = "Übersicht";
            reset = "löschen";
            deleteSet = "Datensatz löschen!";
            instructions = "Arbeitsanweisungen";
            posID = "Positive ID";
            negID = "Negative ID";
            posResult = "Erwarteter Wert";
            repXML = "RepXML";
            actuatorTest = "Aktortest";
            readData = "Identifier (RDBI)";
            smartTool = "SmartTool";
            nextStep = "Nächster Schritt";
            lastStep = "Letzter Schritt";
            filePath = "Dateipfad:";
            useDataBaseChecked = "Checked - Daten aus Datenbank";
            useDataBaseUnchecked = "Unchecked - Daten aus Text-File";
        }
        private void InitEnglish()
        {
            loadFile = "Load file?";
            filePathDialog = "Open Dialogwindow";
            saveFile = "Save";
            next = "Next";
            back = "Back";
            pleaseFill = "Please Fill";
            createDataSet = "Create Data Set";
            pleaseChoose = "Please choose";
            optional = "Optional: direct input. Prioritized if filled";
            stepTitel = "Step";
            steps = "Steps";
            description = "Description";
            specialStep = "Special step";
            anim = "Animation";
            summary = "Summary";
            reset = "Reset";
            deleteSet = "Delete dataset!";
            instructions = "Instructions";
            posID = "Positive ID";
            negID = "Negative ID";
            posResult = "Positives result";
            lastStep = "Last Step";
            repXML = "RepXML";
            actuatorTest = "Actuator test";
            readData = "Identifier (RDBI)";
            smartTool = "SmartTool";
            nextStep = "Next Step";
            filePath = "Filepath:";
            useDataBaseChecked = "Checked - Data from data base";
            useDataBaseUnchecked = "Unchecked - Data from Text-File";
        }
        private void InitEspanol()
        {
            //Meinem Spanisch sollte man nicht trauen! Das ist hauptsächlich zu Test- und Vorführzwecken hier
            loadFile = "Load file?";
            filePathDialog = "Open Dialog";
            saveFile = "Grabar algo";
            next = "Próximo";
            back = "Atrás";
            pleaseFill = "Llenar algo, por favor";
            createDataSet = "Crear expediente";
            pleaseChoose = "Please choose";
            optional = "Optional: direct input. Prioritized if filled";
            stepTitel = "Paso";
            steps = "Pasos";
            description = "Descripción";
            specialStep = "especial paso";
            anim = "Animación";
            summary = "Resumen";
            reset = "Reset";
            deleteSet = "Cancelar  expediente!";
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
            filePath = "File Name with or without path";
            useDataBaseChecked = "Checked - Data from data base";
            useDataBaseUnchecked = "Unchecked - Data from Text-File";
        }
    }
}
