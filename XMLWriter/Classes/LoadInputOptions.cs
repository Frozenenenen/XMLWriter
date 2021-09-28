using System;
using System.Collections.Generic;
using System.IO;

namespace XMLWriter.Classes
{
    class LoadInputOptions
    {
        Language language = new Language();
        private static bool useDataBase; //True=Database, False=txt
        private static readonly string path = @"Files/";
        private static readonly string[] toolChoice = { "", "ActuatorTest", "SmartTool", "ReadDataByIdentifier" };

        public void SetDataBase(bool? check)
        {
            if (check == true)
            {
                useDataBase = true;
            }
            else
            {
                useDataBase = false;
            }
        }
        public bool? GetDataBaseStatus() => useDataBase;
        public string[] GetToolChoice() => toolChoice;
        public string[] GetECUChoices()
        {//Loads long part of ECU_List
            if (true)
            {
                string filePathAndName = CreatePathAndNameString("ECU");
                System.Diagnostics.Debug.WriteLine("ECU Filename: " + filePathAndName);
                return LoadVariableOptions("long", filePathAndName);
            }
            else
            {

            }
        }
        public string[] GetSmartToolChoices()
        {//Loads german part of SmartTools_List
            if (true)
            {
                string filePathAndName = CreatePathAndNameString("SmartTool");
                System.Diagnostics.Debug.WriteLine("SmartTool FileName: " + filePathAndName);
                return LoadVariableOptions(language.GetStringLanguage(), filePathAndName);
            }
            else
            {
                string[] test = { "1", "2" };
                return test;
            }
        }
        public string[] GetIOChoices_ActuatorTest(string longSpecificECU)
        {//Takes long string of ECU an looks up short version. Then loads languagespecific List of selected ECU
            if (true)
            {
                System.Diagnostics.Debug.WriteLine("GetIOChoices_Aktortest Start");
                string shortSpecificECU = GetFirstPartOfTupel("ECU", longSpecificECU); //Soll aus der ECU_List den longspecific  ECU Index rausholen und dadurch dann die shortspecific
                //return LoadVariableOptions(language.GetStringLanguage(), toolChoice[1], shortSpecificECU);
                string filePathAndName = CreatePathAndNameString(toolChoice[1], shortSpecificECU);
                System.Diagnostics.Debug.WriteLine("Aktortest FileName: >" + filePathAndName + "< - >" + shortSpecificECU);
                return LoadVariableOptions(language.GetStringLanguage(), filePathAndName);
            }
            else
            {
                string[] test = { "1", "2" };
                return test;
            }

        }
        public string[] GetMeasureValueChoices_ReadData(string longSpecificECU)
        {//Takes long string of ECU an looks up short version. Then loads languagespecific List of selected ECU
            if (true)
            {
                System.Diagnostics.Debug.WriteLine("GetMeasureValueChoices_ReadData Start");
                string shortSpecificECU = GetFirstPartOfTupel("ECU", longSpecificECU);
                System.Diagnostics.Debug.WriteLine("GetMeasureValueChoices: Nach GetFirstPartofTupel");
                string filePathAndName = CreatePathAndNameString(toolChoice[3], shortSpecificECU);
                System.Diagnostics.Debug.WriteLine("GetMeasureValueChoices: Nach CreatePath");
                System.Diagnostics.Debug.WriteLine("ReadData Filename: " + filePathAndName);
                return LoadVariableOptions(language.GetStringLanguage(), filePathAndName); ;
            }
            else
            {
                string[] test = { "1", "2" };
                return test;
            }
        }
        public string[] GetMeasurementChoices_SmartTool( string specificSmartTool)
        {
            if (true)
            {
                System.Diagnostics.Debug.WriteLine("GetMeasureValueChoices_SmartTool Start");
                System.Diagnostics.Debug.WriteLine("SM_Tupelwechsel von " + specificSmartTool);
                string shortSpecificECU = GetFirstPartOfTupel("SmartTool", specificSmartTool);
                System.Diagnostics.Debug.WriteLine(" zu " + shortSpecificECU);
                string filePathAndName = CreatePathAndNameString(toolChoice[2], shortSpecificECU);
                System.Diagnostics.Debug.WriteLine("Measure Filename: " + filePathAndName);
                return LoadVariableOptions(language.GetStringLanguage(), filePathAndName); ;
            }
            else
            {
                string[] test = { "1", "2" };
                return test;
            }
            
        }

        private string[] LoadVariableOptions(string languageORLengthChoice, string fileNameAndPath)
        {//Depending on Chosen input Loads String and splits it apart. That gives it to another method that splits even further 
            string[] fileContentPart_EN_DE;
            if (useDataBase)
            {
                string stream = LoadInputFromDatabase();
                fileContentPart_EN_DE = stream.Split('|');
            }
            else
            {
                string stream = LoadInputFromTxtFile(fileNameAndPath);
                fileContentPart_EN_DE = stream.Split('|');
            }
            return GetFirstOrSecondPartOfSubString(languageORLengthChoice, fileContentPart_EN_DE);
        }

        private string CreatePathAndNameString(string toolChoiceFromDropDown, string smartToolOrECU)
        { //Create path for the lower dropdown choices
            System.Diagnostics.Debug.WriteLine("In CreatePath... mit 2 Var. Inputs: >>" + toolChoiceFromDropDown + "<< >>" + smartToolOrECU + "<<");
            string filePathAndName;
            if (toolChoiceFromDropDown == toolChoice[1]) //"ActuatorTest" 
            {
                filePathAndName = path + "IO_" + smartToolOrECU + ".txt";
            }
            else if (toolChoiceFromDropDown == toolChoice[2]) //"SmartTool" 
            {
                filePathAndName = path + "Measure_" + smartToolOrECU + ".txt";
            }
            else if (toolChoiceFromDropDown == toolChoice[3]) //"ReadDataByIdentifier"
            {
                filePathAndName = path + "RDID_" + smartToolOrECU + ".txt";
            }
            else
            {
                if (toolChoiceFromDropDown == "")
                {
                    System.Diagnostics.Debug.WriteLine("In CreatePath... mit 2 Var: >>" + "toolChoiceFromDropDown ist Empty" + "<<");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("In CreatePath... mit 2 Var: >>" + toolChoiceFromDropDown + "<<");
                }
                System.Diagnostics.Debug.WriteLine("Fehler in LoadInputOptions.CreatePathString mit 2var");
                filePathAndName = "";
            }
            System.Diagnostics.Debug.WriteLine("In CreatePath mit 2 Var: >>" + filePathAndName + "<< - also für unteres Dropdown");
            return filePathAndName;
        }
        private string CreatePathAndNameString(string toolChoiceFromDropDown)
        { //Create path for the upper dropdown choice

            string filePathAndName;
            if (toolChoiceFromDropDown == "ECU")
            {
                filePathAndName = path + "ECU_List.txt";
            }
            else if (toolChoiceFromDropDown == "SmartTool")
            {
                filePathAndName = path + "SmartTool_List.txt";
            }
            else
            {
                if (toolChoiceFromDropDown == "")
                {
                    System.Diagnostics.Debug.WriteLine("In CreatePath mit 1 Var: >>" + "toolChoiceFromDropDown ist Empty" + "<<");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("In CreatePath mit 1 Var: >>" + toolChoiceFromDropDown + "<<");
                }
                
                System.Diagnostics.Debug.WriteLine("Fehler in LoadInputOptions.CreatePathString mit 1 var");
                filePathAndName = "";
            }
            System.Diagnostics.Debug.WriteLine("In CreatePath mit 1 Var: >>" + filePathAndName + "<< - also für oberes Dropdown");
            return filePathAndName;
        }
        private string[] GetFirstOrSecondPartOfSubString(string languageORLengthChoice, string[] fileContentPart)
        {//Splits the Tupel into two and gives only the first or the second parts in a new List
            List<string> tempOutput = new List<string>();
            string[] fileContentSubPart;
            for (int i = 0; i < fileContentPart.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                fileContentSubPart = fileContentPart[i].Split(';');
                if (languageORLengthChoice == language.GetLanguageChoises()[1] || languageORLengthChoice == "first" || languageORLengthChoice == "short")
                {//"English" || "first" || "short"
                    tempOutput.Add(fileContentSubPart[0]);
                }
                else if (languageORLengthChoice == language.GetLanguageChoises()[0] || languageORLengthChoice == "second" || languageORLengthChoice == "long")
                {//"German" || "second" || "long"
                    tempOutput.Add(fileContentSubPart[1]);
                }
                else
                {
                    tempOutput.Add("Fehler in LoadInputOptions.LoadInputFromTxtFile");
                    System.Diagnostics.Debug.WriteLine("Fehler in LoadInputOptions.GetFirstOrSecondPartOfSubString");
                }
            }
            string[] output = tempOutput.ToArray();
            return output;
        }

        
        private string LoadInputFromDatabase()
        {
            throw new NotImplementedException();
        }
        public string GetFirstPartOfTupel(string toolChoice, string wantedElement) //Man ey, das werden so verdammt viele Zugriffe ;<
        {//Gets Index of the item in the List it derives from and returns the opposite Part of the Tupel
            string filePath = CreatePathAndNameString(toolChoice);
            System.Diagnostics.Debug.WriteLine("In GetFirstPartOfTupel toolChoice: >>" + toolChoice + "<< wanted Element: >>" + wantedElement + "<<");
            int index = GetIndexOfElementFromFile(wantedElement,filePath,"second"); 
            string firstPart = LoadVariableOptions("first", filePath)[index];
            /*int index;
            string firstPart;
            if (toolChoice == "SmartTool")
            {
                index = Array.IndexOf(LoadVariableOptions("second", toolChoice), smartToolOrECU);
                System.Diagnostics.Debug.WriteLine("FirstOfTupel_SmartTool");
                
            }
            else
            {
                index = Array.IndexOf(LoadVariableOptions("second", toolChoice, smartToolOrECU), smartToolOrECU);
                System.Diagnostics.Debug.WriteLine("FirstOfTupel_Nicht-SmartTool");
                firstPart = LoadVariableOptions("first", toolChoice, smartToolOrECU)[index];
            }*/

            return firstPart;
        }
        public string GetFirstPartOfTupel(string toolChoice, string smartToolOrECU, string specificTool) 
        {//Gets Index of the item in the List it derives from and returns the opposite Part of the Tupel

            int index;
            string temp;
            string SmartToolOrECU_en = GetFirstPartOfTupel("ECU", smartToolOrECU);
            System.Diagnostics.Debug.WriteLine(SmartToolOrECU_en);
            string fileName = CreatePathAndNameString(toolChoice, smartToolOrECU);
                index = Array.IndexOf(LoadVariableOptions("second", fileName), specificTool);
                System.Diagnostics.Debug.WriteLine("FirstOfTupel_Nicht-SmartTool");
            string filePathAndName = CreatePathAndNameString(toolChoice, specificTool);
            temp = LoadVariableOptions("first", filePathAndName)[index];
            
            if (index == -1)
            {
                System.Diagnostics.Debug.WriteLine("->->->->->-> " + index + " <-<-<-<-<-<-");
                System.Diagnostics.Debug.WriteLine("-> " + toolChoice + " " + smartToolOrECU + " <-");
                for (int i = 0; i < LoadVariableOptions("second", filePathAndName).Length; i++)
                {
                    System.Diagnostics.Debug.WriteLine("-> " + LoadVariableOptions("second", filePathAndName)[i] + " <-");
                }
            }//Errormessage
            return temp;
        }
        public int GetIndexOfElementFromFile(string Element, string fileName, string firstOrSecondPart)
        {
            int index;
            string[] elements;
            elements = LoadVariableOptions(firstOrSecondPart, fileName);
            System.Diagnostics.Debug.WriteLine("In GetIndexOfElementFromFile: Suche Index von " + Element + " aus File: " + fileName);
            index = Array.IndexOf(elements,Element);
            System.Diagnostics.Debug.WriteLine("In GetIndexOfElementFromFile: Ergebnis: " + index);
            if (index == -1)
            {//Errormessage
                System.Diagnostics.Debug.WriteLine("->->->->->-> " + index + " <-<-<-<-<-<-");
                System.Diagnostics.Debug.WriteLine("-> " + toolChoice + " " + fileName + " <-");
                for (int i = 0; i < LoadVariableOptions("second", fileName).Length; i++)
                {
                    System.Diagnostics.Debug.WriteLine("-> " + LoadVariableOptions("second", fileName) + " <-");
                }
            }
            return index;
        }
        private string LoadInputFromTxtFile(string fileNameAndPath)
        {
            System.Diagnostics.Debug.WriteLine("Load File: >>" + fileNameAndPath + "<<");
            StreamReader sr = new StreamReader(fileNameAndPath);
            return sr.ReadLine();
        }
    }
}
