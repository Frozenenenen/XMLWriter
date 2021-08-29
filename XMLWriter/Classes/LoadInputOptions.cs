using System;
using System.Collections.Generic;
using System.IO;

namespace XMLWriter.Classes
{
    class LoadInputOptions
    {
        Language language = new Language();
        private static bool DataBaseOrTxtFile; //True=Database, False=txt
        private static readonly string path = @"Files/";
        private static readonly string[] toolChoice = { "", "ActuatorTest", "SmartTool", "ReadDataByIdentifier" };

        public void SetDataBase(bool? check)
        {
            if (check == true)
            {
                DataBaseOrTxtFile = true;
            }
            else
            {
                DataBaseOrTxtFile = false;
            }
        }
        public bool? GetDataBase() => DataBaseOrTxtFile;
        public string[] GetToolChoice() => toolChoice;
        public string[] GetECUChoices()
        {//Loads long part of ECU_List
            string filePathAndName = CreatePathString("ECU");
            return LoadVariableOptions("long", filePathAndName);
        }
        public string[] GetSmartToolChoices()
        {//Loads german part of SmartTools_List
            string filePathAndName = CreatePathString("SmartTool");
            return LoadVariableOptions(language.GetStringLanguage(), filePathAndName);
        }
        public string[] GetIOChoices_ActuatorTest(string longSpecificECU)
        {//Takes long string of ECU an looks up short version. Then loads languagespecific List of selected ECU
            string shortSpecificECU = GetFirstPartOfTupel("ECU", longSpecificECU);
            //return LoadVariableOptions(language.GetStringLanguage(), toolChoice[1], shortSpecificECU);
            string filePathAndName = CreatePathAndNameString(toolChoice[1], shortSpecificECU);
            return LoadVariableOptions(language.GetStringLanguage(), filePathAndName);
        }
        public string[] GetMeasureValueChoices_ReadData(string longSpecificECU)
        {//Takes long string of ECU an looks up short version. Then loads languagespecific List of selected ECU
            string shortSpecificECU = GetFirstPartOfTupel("ECU", longSpecificECU);
            string filePathAndName = CreatePathAndNameString(toolChoice[3], shortSpecificECU);
            return LoadVariableOptions(language.GetStringLanguage(), shortSpecificECU); ;
        }
        public string[] GetMeasurementChoices_SmartTool( string specificSmartTool)
        {
            System.Diagnostics.Debug.WriteLine("SM_Tupelwechsel von " + specificSmartTool);
            string shortSpecificECU = GetFirstPartOfTupel("SmartTool", specificSmartTool);
            System.Diagnostics.Debug.WriteLine(" zu " + shortSpecificECU);
            string filePathAndName = CreatePathAndNameString(toolChoice[2], shortSpecificECU);
            return LoadVariableOptions(language.GetStringLanguage(), shortSpecificECU); ;
        }

        private string[] LoadVariableOptions(string languageORLengthChoice, string fileNameAndPath)
        {//Depending on Chosen input Loads String and splits it apart. That gives it to another method that splits even further 
            string[] fileContentPart_EN_DE;
            if (DataBaseOrTxtFile)
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
        {
            string filePathAndName;
            if (toolChoiceFromDropDown == toolChoice[1])
            {
                filePathAndName = path + "IO_" + smartToolOrECU + ".txt";
            }
            else if (toolChoiceFromDropDown == toolChoice[2])
            {
                filePathAndName = path + "Measure_" + smartToolOrECU + ".txt";
            }
            else if (toolChoiceFromDropDown == toolChoice[3])
            {
                filePathAndName = path + "RDID_" + smartToolOrECU + ".txt";
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Fehler in LoadInputOptions.CreatePathString");
                filePathAndName = "";
            }
            System.Diagnostics.Debug.WriteLine("In CreatePath V1: " + filePathAndName);
            return filePathAndName;
        }
        private string CreatePathString(string toolChoiceFromDropDown)
        {
            string filePathAndName;
            if (toolChoiceFromDropDown == "ECU")
            {
                filePathAndName = path + "ECU_List.txt";
            }
            else if (toolChoiceFromDropDown == "ECU")
            {
                filePathAndName = path + "ECU_List" + ".txt";
            }
            else if (toolChoiceFromDropDown == "SmartTool")
            {
                filePathAndName = path + "SmartTool_List.txt";
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Fehler in LoadInputOptions.CreatePathString");
                filePathAndName = "";
            }
            System.Diagnostics.Debug.WriteLine("In CreatePath V2: " + filePathAndName);
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
            string filePath = CreatePathAndNameString(toolChoice, wantedElement);
            int index = GetIndexOfElementFromFile(wantedElement,filePath,"second");
            string firstPart = LoadVariableOptions("first", toolChoice)[index];
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
                temp = LoadVariableOptions("first", toolChoice, specificTool)[index];
            
            if (index == -1)
            {
                System.Diagnostics.Debug.WriteLine("->->->->->-> " + index + " <-<-<-<-<-<-");
                System.Diagnostics.Debug.WriteLine("-> " + toolChoice + " " + smartToolOrECU + " <-");
                for (int i = 0; i < LoadVariableOptions("second", toolChoice, smartToolOrECU).Length; i++)
                {
                    System.Diagnostics.Debug.WriteLine("-> " + LoadVariableOptions("second", toolChoice, smartToolOrECU)[i] + " <-");
                }
            }//Errormessage
            return temp;
        }
        public int GetIndexOfElementFromFile(string Element, string fileName, string firstOrSecondPart)
        {
            int index;
            string[] elements;
            elements = LoadVariableOptions(firstOrSecondPart, fileName);
            index = Array.IndexOf(elements,fileName);
            if (index == -1)
            {
                System.Diagnostics.Debug.WriteLine("->->->->->-> " + index + " <-<-<-<-<-<-");
                System.Diagnostics.Debug.WriteLine("-> " + toolChoice + " " + smartToolOrECU + " <-");
                for (int i = 0; i < LoadVariableOptions("second", toolChoice, smartToolOrECU).Length; i++)
                {
                    System.Diagnostics.Debug.WriteLine("-> " + LoadVariableOptions("second", toolChoice, smartToolOrECU)[i] + " <-");
                }
            }//Errormessage
            return index;
        }
        private string LoadInputFromTxtFile(string fileNameAndPath)
        {
            //System.Diagnostics.Debug.WriteLine("Load File: " + fileNameAndPath);
            StreamReader sr = new StreamReader(fileNameAndPath);
            return sr.ReadLine();
        }
    }
}
