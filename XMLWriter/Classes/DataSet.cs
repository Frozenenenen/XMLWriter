﻿using System;
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
namespace XMLWriter
{
    class DataSet
    {
        private static int stepCount = 0;
        private static int stepCountMax = 0;
        private static string dataType = "Fehler";
        private static string fileName = "Dateiname";

        private static readonly string[] dataTypeChoice = { "gfs", "rep" }; //Gehört eigentlich nicht in diese Klasse, aber bis mir ein besserer Ort einfällt bleibts wohl hier

        //Fahrzeugspezifische Variablen
        /*private static string vin = "";
        private static string model = "";*/




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



        //Getter
        public int GetStepCount() => stepCount;
        public int GetStepCountMax() => stepCountMax;

        public string[] GetDataTypeChoice() => dataTypeChoice;

        
        public string GetDataType() => dataType;
        public string GetFileName() => fileName;

        public string GetStepNamePos(int count)
        {
            try
            {
                return steps[count];
            }
            catch (SystemException)
            {
                System.Diagnostics.Debug.WriteLine("steps: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetStepAnimsPos(int count)
        {
            try
            {
                return stepAnims[count];
            }
            catch (SystemException)
            {
                System.Diagnostics.Debug.WriteLine("stepAnims: ArgumentOutOfRangeException");
                return "Fehler";
            }

        }
        public string GetStepTextPos(int count)
        {
            try
            {
                return stepTexts[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepTexts: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetStepSpecialTextPos(int count)
        {
            try
            {
                return stepSpecial[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepSpecialTexts: ArgumentOutOfRangeException");
                return "Fehler";
            }

        }
        public string GetStepInstructionPos(int count)
        {
            try
            {
                return stepInstruction[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepInstruction: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetStepPositiveIDPos(int count)
        {
            try
            {
                return stepPositiveID[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepPositiveID: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetNegativeIDPos(int count)
        {
            try
            {
                return stepNegativeID[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepNegativeID: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetPositiveResultPos(int count)
        {
            try
            {
                return stepPositiveResult[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepPositiveResult: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetRepXMLPos(int count)
        {
            try
            {
                return stepRepXML[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepRepXML: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public string GetActuatorTestPos(int count)
        {
            try
            {
                return stepActuatorTest[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepActuatorTest: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public bool? GetCheckActuatorTestPos(int count)
        {
            try
            {
                return checkStepActuatorTest[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("CheckStepActuatorTest: ArgumentOutOfRangeException");
                return true;
            }
        }
        public string GetRDBIPpos(int count)
        {
            try
            {
                return stepRDBI[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepRDBI: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public bool? GetCheckRDBIPos(int count)
        {
            try
            {
                return checkStepRDBI[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("CheckStepRDBIPos: ArgumentOutOfRangeException");
                return true; 
            }
        }
        public string GetSmartToolPos(int count)
        {
            try
            {
                return stepSmartTool[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepSmartTool: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }
        public bool? GetCheckSmartTool(int count)
        {
            try
            {
                return checkStepSmartTool[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("CheckStepSmartTool: ArgumentOutOfRangeException");
                return true; 
            }
        }    
        public bool? GetNextStepPos(int count)
        {
            try
            {
                return stepNextStep[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepNextStep: ArgumentOutOfRangeException");
                return true; //kA wie man hier dann nen Fehler erkennen soll
            }
        }
        public bool? GetLastStepPos(int count)
        {
            try
            {
                return stepLastStep[count];
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("stepLastStep: ArgumentOutOfRangeException");
                return true;
            }
        }

        //Setter 
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
        private void SetCheckActuatorTest(bool? inputCheckActuatorTest)
        {
            if (stepCountMax == stepCount)
            {
                checkStepActuatorTest.Insert(stepCount, inputCheckActuatorTest);
                checkStepActuatorTest[stepCount] = inputCheckActuatorTest;
            }
            else
            {
                checkStepActuatorTest[stepCount] = inputCheckActuatorTest;
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
        private void SetCheckRDBI(bool? inputCheckRDBI)
        {
            if (stepCountMax == stepCount)
            {
                checkStepRDBI.Insert(stepCount, inputCheckRDBI);
                checkStepRDBI[stepCount] = inputCheckRDBI;
            }
            else
            {
                checkStepRDBI[stepCount] = inputCheckRDBI;
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
        private void SetCheckSmartTool(bool? inputCheckSmartTool)
        {
            if (stepCountMax == stepCount)
            {
                checkStepSmartTool.Insert(stepCount, inputCheckSmartTool);
                checkStepSmartTool[stepCount] = inputCheckSmartTool;
            }
            else
            {
                checkStepSmartTool[stepCount] = inputCheckSmartTool;
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




        public void InitNewDataSet()       //Muss mit allen default-Daten befüllt werden, wenn eine "Weiteren Datensatz anlegen"-Funktion eingebaut wird
        {                               //Theoretisch könnte man hier zwischen Rep und GFS unterscheiden um sich mal was zu sparen, aber sein wir ehrlich... auf den Speicher kommts nicht an
            if (stepCount == stepCountMax)  //Das ist ne ZIEMLICH unsaubere Lösung :< 
            {
                //left side +rep
                steps.Insert(stepCount, "");
                stepAnims.Insert(stepCount, "default");
                stepTexts.Insert(stepCount, "");
                stepSpecial.Insert(stepCount, "");
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
                checkStepActuatorTest.Insert(stepCount, true);
                checkStepRDBI.Insert(stepCount, true);
                checkStepSmartTool.Insert(stepCount, true);
                stepNextStep.Insert(stepCount, false);
                stepLastStep.Insert(stepCount, false);
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
        
        public void SaveRepSet(string stepName, string text, string anim, string specialText)
        {
            SetStep(stepName);
            SetText(text);
            SetAnim(anim);
            SetSpecialText(specialText);

            System.Diagnostics.Debug.WriteLine(stepName + " Anim: " + anim + " Text: " + text + " SpText: " + specialText);
        }
        public void SaveGfsSet(string stepName, string text, string anim, string instructionText, string posID, string negID, string posResult, string repXML, string actuatorTest, bool? checkActuatorTest, string RDBI, bool? checkRDBI, string smartTool, bool? checkSmartTool, bool? nextStep, bool? lastStep)
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
            SetCheckActuatorTest(checkActuatorTest);
            SetRDBI(RDBI);
            SetCheckRDBI(checkRDBI);
            SetSmartTool(smartTool);
            SetCheckSmartTool(checkSmartTool);
            SetNextStep(nextStep);
            SetLastStep(lastStep);

            System.Diagnostics.Debug.WriteLine("step: " + stepName + " text: " + text + " Anim: " + anim + " Instr.: " + instructionText + " posID: " + posID + " negID: " + negID +" posRes: " + posResult + " repXML: " + repXML + "\nA-Test: " + actuatorTest + " A-chek: " + checkActuatorTest + " RDBI: " + RDBI + " checkRDBI: " + checkRDBI + " SmartTool: " + smartTool + " checkSmartTool: " + checkSmartTool + " NextST: " + nextStep + " LastSt: " + lastStep +"\n");
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
                    gfs.OutputToXML(stepCountMax, steps, stepTexts, stepAnims, stepInstruction, stepPositiveID, stepNegativeID, stepPositiveResult, stepRepXML, stepActuatorTest, checkStepActuatorTest, stepRDBI, checkStepRDBI, stepSmartTool, checkStepSmartTool, stepNextStep, stepLastStep, fileName, dataType);
                    break;
                default:
                    Console.WriteLine("Error in OutputToXML from DataSet");
                    break;
            }
        }
    }
}
