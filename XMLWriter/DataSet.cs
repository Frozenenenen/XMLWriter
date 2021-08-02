using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter
{
    class DataSet
    {
        private static int stepCount = 0;
        private static int stepCountMax = 0;
        private static string dataType = "Fehler";
        private static string fileName = "Dateiname";

        private static readonly string[] dataTypeChoice = { "gfs", "rep" }; //Gehört eigentlich nicht in diese Klasse, aber bis mir ein besserer Ort einfällt bleibts wohl hier

        private static readonly string step = "Schritt";
        private static readonly string stepGfs = "Arbeitsschritt";

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
        private static List<string> stepRDBI = new List<string>();
        private static List<string> stepSmartTool = new List<string>();
        private static List<string> stepNextStep = new List<string>();
        private static List<string> stepLastStep = new List<string>();



        //Getter
        public int GetStepCount() => stepCount;
        public int GetStepCountMax() => stepCountMax;

        public string[] GetDataTypeChoice() => dataTypeChoice;

        public void SetDataType(string inputDataType) => dataType = inputDataType;

        /* 
         public string GetDataType() => dataType;
         public string[] GetModelChoices() => File.ReadAllLines(@"Testgelände\models.txt");
         public string GetVIN() => vin;
         public void SetVIN(string inputVin) => vin = inputVin;

         public string GetModel() => model;
         public void SetModel(string inputName) => model = inputName;*/

        public string GetFileName() => fileName;

        public string GetStepAnimsPos(int Count)
        {
            try
            {
                return stepAnims[Count];
            }
            catch (SystemException)
            {
                Console.WriteLine("stepAnims: ArgumentOutOfRangeException");
                return "Fehler";
            }

        }

        public string GetStepTextPos(int Count)
        {
            try
            {
                return stepTexts[Count];
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("stepTexts: ArgumentOutOfRangeException");
                return "Fehler";
            }
        }

        public string GetStepSpecialTextPos(int Count)
        {
            try
            {
                return stepSpecial[Count];
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("stepSpecialTexts: ArgumentOutOfRangeException");
                return "Fehler";
            }

        }

        //Setter - private, weil diese nur für die Verständlichkeit des codes hier gesammelt werden
        private void SetStep(string inputStepCount)
        {
            if (stepCountMax == stepCount)
            {
                steps.Insert(stepCount, step + " " + (inputStepCount));
            }
            else
            {
                steps[stepCount] = step + " " + (inputStepCount);
            }
        }
        private void SetAnim(string inputStepAnim)
        {
            if (stepCountMax == stepCount)
            {
                stepAnims.Insert(stepCount, inputStepAnim);
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
            }
            else
            {
                stepSmartTool[stepCount] = inputSmartTool;
            }
        }
        private void SetNextStep(string inputNextStep)
        {
            if (stepCountMax == stepCount)
            {
                stepNextStep.Insert(stepCount, inputNextStep);
            }
            else
            {
                stepNextStep[stepCount] = inputNextStep;
            }
        }
        private void SetLastStep(string inputLastStep)
        {
            if (stepCountMax == stepCount)
            {
                stepLastStep.Insert(stepCount, inputLastStep);
            }
            else
            {
                stepLastStep[stepCount] = inputLastStep;
            }
        }




        public void InitDataSet()       //Muss mit allen default-Daten befüllt werden, wenn eine "Weiteren Datensatz anlegen"-Funktion eingebaut wird
        {
            steps.Insert(stepCount, "Schritt 1");
            stepAnims.Insert(stepCount, "default");
            stepTexts.Insert(stepCount, "");
            stepSpecial.Insert(stepCount, "");

        }

        public void SaveRepSet(string stepName, string text, string anim, string specialText)
        {
            SetStep(stepName);
            SetText(text);
            SetAnim(anim);
            SetSpecialText(specialText);

            Console.WriteLine(stepName + " Anim: " + anim + " Text: " + text + " SpText: " + specialText);
        }

        public void SaveGfsSet(string stepName, string text, string anim, string instructionText, string posID, string negID, string posResult, string repXML, string actuatorTest, string RDBI, string smartTool, string nextStep, string lastStep)
        {
            SetStep(stepName);
            SetText(text);
            SetAnim(anim);
            SetInstruction(instructionText);
            SetPositiveID(posID);
            SetNegativeID(posID);
            SetPositiveResult(posResult);
            SetRepXML(repXML);
            SetActuatorTest(actuatorTest);
            SetRDBI(RDBI);
            SetSmartTool(smartTool);
            SetNextStep(nextStep);
            SetLastStep(lastStep);
        }
        public void IncrementSteps()
        {
            //Wenn man beim höchsten Datanschritt ist, soll bei incrementieren auch das Maximum incrementiert werden. Reihenfolge wichtig!
            if (stepCount == stepCountMax)
            {
                stepCountMax++;
            }
            stepCount++;
            //Console.Write("Increment: " + "Schritt: " + stepCount + " -  Max: " + stepCountMax);
        }
        public void DecrementStepsForSaving()
        {
            if (stepCount == stepCountMax)
            {
                stepCountMax--;
            }
            stepCount--;
            Console.Write("Decrement'save': " + "Schritt: " + stepCount + " -  Max: " + stepCountMax);
        }
        public void DecrementSteps()
        {
            stepCount--;
            //Console.Write("Decrement: " +  "Schritt: " + stepCount + " -  Max: " + stepCountMax);
        }
        public void DecrementStepsMax() //In a way it removes the last Inputwindow. It not acutally does, but it wont be put out into file
        {
            stepCount--;
            stepCountMax--;
        }

        public void SetFileName(string inputFileName) //Damit keine vorherigen Daten überschrieben werden, wird der Dateiname iteriert, bis ein neuer Dateiname gefunden wurde.
        {
            WriteToXML writer = new WriteToXML();
            writer.SetFileName(inputFileName, dataType);

        }

        public void OutputToXML() //Output to file
        {
            switch (dataType)
            {
                case "rep":
                    WriteRepToXML rep = new WriteRepToXML();
                    rep.OutputToXML(stepCountMax, steps, stepTexts, stepAnims, stepSpecial, fileName, dataType);
                    break;
                case "gfs":
                    WriteGFSToXML gfs = new WriteGFSToXML();
                    gfs.OutputToXML(stepCountMax, steps, stepTexts, stepAnims, stepInstruction, stepPositiveID, stepNegativeID, stepPositiveResult, stepRepXML, stepActuatorTest, stepRDBI, stepSmartTool, stepNextStep, stepLastStep, fileName, dataType);
                    break;
                default:
                    Console.WriteLine("Error in OutputToXML from DataSet");                  
                    break;
            }
        }
    }
}
