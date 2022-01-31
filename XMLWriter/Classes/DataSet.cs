using System;
using System.Collections.Generic;

/***********************
 * Mögliche Fehlerquelle:
 * Ich habe aufrgund mangelnder Ideen zur Umsetzung oftmals den Listenbefehl"Insert" auf eine Art und Weise benutzt, 
 * wo weitere Elemente hinzugefügt werden, obwohl diese damit dann über die maxcount zahl hinaus gehen.
 * Dies wurde gemacht, damit keine Zugriffe auf nicht schon erstellte Elemente stattfindet, was exceptions wirft.
 * Allerdings kann dies dazu führen, dass der letzte Speicherstand vielleicht Fehler verursacht. Also alle wo StepCount==StepCountMax.
 * Ich weiß aktuell noch keine bessere Lösung als bei neuen Datensätzen beim Insert auch ein direkt einspeichern dazu zu fügen.
 * Dies dürfte auch der Grund dafür sein, dass wenn man zurück und wieder vor springt und dann neue Datensätze eröffnet diese schon falsch befüllt sind.
 ***********************/
//Todo:prio4 Getter Setter mit Variable verkürzen
//Todo:prio3 Dataset Klasse aufteilen in GFSSet und REPSet Subklassen
//Todo:prio4 Umschreiben der Increment und Decrement
//Todo:prio2 Last step im letzten Schritt true
//Todo:prio4 Listenvariablen zu einer Objektliste

//System.Diagnostics.Debug.WriteLine("");
namespace XMLWriter
{
    class DataSet
    {
        private static int stepCount = 0;
        private static int stepCountMax = 0;
        private static string dataType = "Fehler";
        private static string fileName = "Dateiname";

        private static readonly string[] dataTypeChoice = { "gfs", "rep" }; //Gehört eigentlich nicht in diese Klasse, aber bis mir ein besserer Ort einfällt bleibts wohl hier
        //private static readonly string[] toolChoice = { "actuatorTest", "ReadData", "SmartTool" };


        //Variablen von Rep und Gfs
        private static List<string> steps = new List<string>();
        private static List<string> stepTexts = new List<string>();
        private static List<string> stepAnims = new List<string>();
        //Rep-spezifische Variable
        private static List<string> stepSpecial = new List<string>();
        //Gfs-spezifische Variablen
        private static List<string> stepInstruction = new List<string>();
        private static List<string> stepPositiveID = new List<string>();
        private static List<string> stepNegativeID = new List<string>();
        private static List<string> stepPositiveResult = new List<string>();
        private static List<string> stepRepXML = new List<string>();
        private static List<string> stepActuatorTest = new List<string>();
        private static List<bool?> checkStepActuatorTest = new List<bool?>();
        private static List<string> stepRDBI = new List<string>();
        private static List<bool?> checkStepRDBI = new List<bool?>();
        private static List<string> stepSmartTool = new List<string>();
        private static List<bool?> checkStepSmartTool = new List<bool?>();
        private static List<bool?> stepNextStep = new List<bool?>();
        private static List<bool?> stepLastStep = new List<bool?>();
        private static List<string> stepToolChoice = new List<string>();



        //Getter
        public int GetStepCount() => stepCount;
        public int GetStepCountMax() => stepCountMax;

        public string[] GetDataTypeChoice() => dataTypeChoice;
        //public string[] GetToolChoice() => toolChoice;
        public string[] GetStepNames()
        {
            string[] stepNames = new string[stepCountMax];
            for (int i = 0; i < stepCountMax; i++)
            {
                stepNames[i] = steps[i];
            }
            return stepNames;
        }


        public string GetDataType() => dataType;
        public string GetFileName() => fileName;

        public string GetStepNameOfIndex(int index)
        {
            try
            {
                return steps[index];
            }
            catch (SystemException)
            {
                System.Diagnostics.Debug.WriteLine("steps: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetStepAnimsOfIndex(int index)
        {
            try
            {
                return stepAnims[index];
            }
            catch (SystemException)
            {
                System.Diagnostics.Debug.WriteLine("stepAnims: ArgumentOutOfRangeException");
                return "Fehler";
            }

        }
        public string GetStepTextOfIndex(int index)
        {
            try
            {
                return stepTexts[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepTexts: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetStepSpecialTextOfIndex(int index)
        {
            try
            {
                return stepSpecial[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepSpecialTexts: ArgumentOutOfRangeException");
                return "Fehler";
            }

        }
        public string GetStepInstructionOfIndex(int index)
        {
            try
            {
                return stepInstruction[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepInstruction: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetStepPositiveIDOfIndex(int index)
        {
            try
            {
                return stepPositiveID[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepPositiveID: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetNegativeIDOfIndex(int index)
        {
            try
            {
                return stepNegativeID[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepNegativeID: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetPositiveResultOfIndex(int index)
        {
            try
            {
                return stepPositiveResult[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepPositiveResult: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetRepXMLOfIndex(int index)
        {
            try
            {
                return stepRepXML[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepRepXML: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetActuatorTestOfIndex(int index)
        {
            try
            {
                return stepActuatorTest[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepActuatorTest: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public bool? GetCheckActuatorTestOfIndex(int index)
        {
            try
            {
                return checkStepActuatorTest[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("CheckStepActuatorTest: ArgumentOutOfRangeException");
                return true;
            }
        }
        public string GetRDIDOfIndex(int index)
        {
            try
            {
                return stepRDBI[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepRDBI: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public bool? GetCheckRDBIOfIndex(int index)
        {
            try
            {
                return checkStepRDBI[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("CheckStepRDBIPos: ArgumentOutOfRangeException");
                return true; 
            }
        }
        public string GetSmartToolOfIndex(int index)
        {
            try
            {
                return stepSmartTool[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepSmartTool: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public bool? GetCheckSmartToolOfIndex(int index)
        {
            try
            {
                return checkStepSmartTool[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("CheckStepSmartTool: ArgumentOutOfRangeException");
                return true; 
            }
        }    
        public bool? GetNextStepOfIndex(int index)
        {
            try
            {
                return stepNextStep[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepNextStep: ArgumentOutOfRangeException");
                return true; //kA wie man hier dann nen Fehler erkennen soll
            }
        }
        public bool? GetLastStepOfIndex(int index)
        {
            try
            {
                return stepLastStep[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepLastStep: ArgumentOutOfRangeException");
                return true;
            }
        }
        public string GetToolChoiceOfIndex(int index)
        {
            try
            {
                return stepToolChoice[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepToolChoice: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }

        //Setter 
        public void SetStepCountMax(int inputStepCountMax) => stepCountMax = inputStepCountMax;
        public void SetStepCount(int inputStepCount) => stepCount = inputStepCount;
        public void SetDataType(string inputDataType) => dataType = inputDataType;
        //- private, weil diese nur für die Verständlichkeit des codes hier gesammelt werden
        private void SetStep(string inputStepCount)
        {
            if (stepCountMax == stepCount)
            {
                steps.Insert(stepCount, inputStepCount);
                steps[stepCount] = inputStepCount;
            }
            else
            {
                steps[stepCount] = inputStepCount;
            }
        }
        private void SetAnim(string inputStepAnim)
        {
            if (stepCountMax == stepCount)
            {
                stepAnims.Insert(stepCount, inputStepAnim);
                stepAnims[stepCount] = inputStepAnim;
            }
            else
            {
                stepAnims[stepCount] = inputStepAnim;
            }
        }
        private void SetText(string inputStepText)
        {
            if (stepCountMax == stepCount)
            {
                stepTexts.Insert(stepCount, inputStepText);
                stepTexts[stepCount] = inputStepText;
            }
            else
            {
                stepTexts[stepCount] = inputStepText;
            }
        }
        private void SetSpecialText(string inputStepSpecial)
        {
            if (stepCountMax == stepCount)
            {
                stepSpecial.Insert(stepCount, inputStepSpecial);
                stepSpecial[stepCount] = inputStepSpecial;
            }
            else
            {
                stepSpecial[stepCount] = inputStepSpecial;
            }
        }
        private void SetInstruction(string inputInstruction)
        {
            if (stepCountMax == stepCount)
            {
                stepInstruction.Insert(stepCount, inputInstruction);
                stepInstruction[stepCount] = inputInstruction;
            }
            else
            {
                stepInstruction[stepCount] = inputInstruction;
            }
        }
        private void SetPositiveID(string inputStepPositiveID)
        {
            if (stepCountMax == stepCount)
            {
                stepPositiveID.Insert(stepCount, inputStepPositiveID);
                stepPositiveID[stepCount] = inputStepPositiveID;
            }
            else
            {
                stepPositiveID[stepCount] = inputStepPositiveID;
            }
        }
        private void SetNegativeID(string inputStepNegativeID)
        {
            if (stepCountMax == stepCount)
            {
                stepNegativeID.Insert(stepCount, inputStepNegativeID);
                stepNegativeID[stepCount] = inputStepNegativeID;
            }
            else
            {
                stepNegativeID[stepCount] = inputStepNegativeID;
            }
        }
        private void SetPositiveResult(string inputPositiveResult)
        {
            if (stepCountMax == stepCount)
            {
                stepPositiveResult.Insert(stepCount, inputPositiveResult);
                stepPositiveResult[stepCount] = inputPositiveResult;
            }
            else
            {
                stepPositiveResult[stepCount] = inputPositiveResult;
            }
        }
        private void SetRepXML(string inputRepXML)
        {
            if (stepCountMax == stepCount)
            {
                stepRepXML.Insert(stepCount, inputRepXML);
                stepRepXML[stepCount] = inputRepXML;
            }
            else
            {
                stepRepXML[stepCount] = inputRepXML;
            }
        }
        private void SetActuatorTest(string inputActuatorTest)
        {
            if (stepCountMax == stepCount)
            {
                stepActuatorTest.Insert(stepCount, inputActuatorTest);
                stepActuatorTest[stepCount] = inputActuatorTest;
            }
            else
            {
                stepActuatorTest[stepCount] = inputActuatorTest;
            }
        }
        private void SetRDBI(string inputRDBI)
        {
            if (stepCountMax == stepCount)
            {
                stepRDBI.Insert(stepCount, inputRDBI);
                stepRDBI[stepCount] = inputRDBI;
            }
            else
            {
                stepRDBI[stepCount] = inputRDBI;
            }
        }
        private void SetSmartTool(string inputSmartTool)
        {
            if (stepCountMax == stepCount)
            {
                stepSmartTool.Insert(stepCount, inputSmartTool);
                stepSmartTool[stepCount] = inputSmartTool;
            }
            else
            {
                stepSmartTool[stepCount] = inputSmartTool;
            }
        }
        private void SetNextStep(bool? inputNextStep)
        {
            if (stepCountMax == stepCount)
            {
                stepNextStep.Insert(stepCount, inputNextStep);
                stepNextStep[stepCount] = inputNextStep;
            }
            else
            {
                stepNextStep[stepCount] = inputNextStep;
            }
        }
        private void SetLastStep(bool? inputLastStep)
        {
            if (stepCountMax == stepCount)
            {
                stepLastStep.Insert(stepCount, inputLastStep);
                stepLastStep[stepCount] = inputLastStep;
            }
            else
            {
                stepLastStep[stepCount] = inputLastStep;
            }
        }
        private void SetToolChoice(string inputToolChoice)
        {
            if (stepCountMax == stepCount)
            {
                stepToolChoice.Insert(stepCount, inputToolChoice);
                stepToolChoice[stepCount] = inputToolChoice;
            }
            else
            {
                stepToolChoice[stepCount] = inputToolChoice;
            }
        }




        public void InitNewDataSet()       //Muss mit allen default-Daten befüllt werden, wenn eine "Weiteren Datensatz anlegen"-Funktion eingebaut wird
        {                               //Theoretisch könnte man hier zwischen Rep und GFS unterscheiden um sich mal was zu sparen, aber sein wir ehrlich... auf den Speicher kommts nicht an
            if (stepCount == stepCountMax)  //Das ist ne ZIEMLICH unsaubere Lösung :< 
            {
                //left side +rep
                steps.Insert(stepCount, "");
                stepAnims.Insert(stepCount, "default");
                stepTexts.Insert(stepCount, "");
                if (dataType == "rep")
                {
                    stepSpecial.Insert(stepCount, "");
                }
                else if(dataType=="gfs")
                {
                    stepInstruction.Insert(stepCount, "");
                    //right side
                    stepPositiveID.Insert(stepCount, "");
                    stepNegativeID.Insert(stepCount, "");
                    stepPositiveResult.Insert(stepCount, "");
                    stepRepXML.Insert(stepCount, "");
                    stepActuatorTest.Insert(stepCount, "");
                    stepRDBI.Insert(stepCount, "");
                    stepSmartTool.Insert(stepCount, "");
                    //checks
                    /*checkStepActuatorTest.Insert(stepCount, true);
                    checkStepRDBI.Insert(stepCount, true);
                    checkStepSmartTool.Insert(stepCount, true);*/
                    stepNextStep.Insert(stepCount, false);
                    stepLastStep.Insert(stepCount, false);
                    stepToolChoice.Insert(stepCount, "");
                }

            }
        }

        public void IncrementStepCount()    //Increment und Decrement sollten wohl noch durch setter und getter ersetzt und nur in GUIMovement bearbeitet werden
        {
            stepCount++;
        }
        public void DecrementStepCount()
        {
            stepCount--;
        }
        public void IncrementMaxSteps()
        {
            stepCountMax++;
        }
        public void DecrementMaxSteps()
        {
            stepCountMax--;
        }

        public void ResetDataSet()
        {
            stepCountMax = 0;
            steps.Clear();
            stepTexts.Clear();
            stepAnims.Clear();
            stepSpecial.Clear();
            stepInstruction.Clear();
            stepPositiveID.Clear();
            stepNegativeID.Clear();
            stepPositiveResult.Clear();
            stepRepXML.Clear();
            stepActuatorTest.Clear();
            checkStepActuatorTest.Clear();
            stepRDBI.Clear();
            checkStepRDBI.Clear();
            stepSmartTool.Clear();
            checkStepSmartTool.Clear();
            stepNextStep.Clear();
            stepLastStep.Clear();
            InitNewDataSet();
        }
        
        
        public void SaveSet(string stepName, string text, string anim, string specialText)
        {
            SetStep(stepName);
            SetText(text);
            SetAnim(anim);
            SetSpecialText(specialText);

            System.Diagnostics.Debug.WriteLine(stepName + " Anim: " + anim + " Text: " + text + " SpText: " + specialText);
        }
        public void SaveSet(string toolChoice, string stepName, string text, string anim, string instructionText, string posID, string negID, string posResult, string repXML, string actuatorTest, string RDBI, string smartTool, bool? nextStep, bool? lastStep)
        {
            SetStep(stepName);
            SetText(text);
            SetAnim(anim);
            SetInstruction(instructionText);
            SetPositiveID(posID);
            SetNegativeID(negID);
            SetPositiveResult(posResult);
            SetRepXML(repXML);
            SetActuatorTest(actuatorTest);
            SetRDBI(RDBI);
            SetSmartTool(smartTool);
            SetNextStep(nextStep);
            SetLastStep(lastStep);
            SetToolChoice(toolChoice);

            //System.Diagnostics.Debug.WriteLine("Tool: " + toolChoice + " step: " + stepName + " text: " + text + " Anim: " + anim + " Instr.: " + instructionText + " posID: " + posID + " negID: " + negID +" posRes: " + posResult + " repXML: " + repXML + "\nA-Test: " + actuatorTest +  " RDBI: " + RDBI +  " SmartTool: " + smartTool +  " NextST: " + nextStep + " LastSt: " + lastStep +"\n");
            System.Diagnostics.Debug.WriteLine("Tool: " + toolChoice + " step: " + stepName + " \nposRes: " + posResult + " \nRDBI: " + RDBI + " \nSmartTool: " + smartTool + "\n");
        }

        public void SetFileName(string inputFileName) //Damit keine vorherigen Daten überschrieben werden, wird der Dateiname iteriert, bis ein neuer Dateiname gefunden wurde.
        {
            WriteToXML writer = new WriteToXML();
            writer.SetFileName(inputFileName);//, dataType);

        }

        public void OutputToXML() //Output to file
        {
            switch (dataType)
            {
               //WriteToXML writer = new WriteToXML(); //Es sollte irgendwie möglich sein den Aufruf nur über die Menge der Übergebenen Parameter zu machen statt einem Switch, der das vorher controlliert. Dann würde das hier zum 2-Zeiler
               case "rep":
                    WriteRepToXML rep = new WriteRepToXML();
                    rep.OutputToXML(stepCountMax, steps, stepTexts, stepAnims, stepSpecial, fileName, dataType);
                    break;
                case "gfs":
                    WriteGFSToXML gfs = new WriteGFSToXML();
                    gfs.OutputToXML(stepCountMax, stepToolChoice, steps, stepTexts, stepAnims, stepInstruction, stepPositiveID, stepNegativeID, stepPositiveResult, stepRepXML, stepActuatorTest, checkStepActuatorTest, stepRDBI, checkStepRDBI, stepSmartTool, checkStepSmartTool, stepNextStep, stepLastStep, fileName, dataType);
                    break;
                default:
                    Console.WriteLine("Error in OutputToXML from DataSet");
                    break;
            }
        }
    }
}
