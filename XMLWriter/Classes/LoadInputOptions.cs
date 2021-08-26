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
            return LoadVariableOptions("long", "ECU"); //Es dürfte egal sein, was in dem dritten Parameter steht.
        }
        public string[] GetSmartToolChoices()
        {//Loads german part of SmartTools_List
            return LoadVariableOptions(language.GetLanguageChoises()[0], "SmartTool");
        }


        public string[] GetIOChoices_ActuatorTest(string language, string longSpecificECU)
        {//Takes long string of ECU an looks up short version. Then loads languagespecific List of selected ECU
            string shortSpecificECU = GetFirstPartOfTupel("ECU", longSpecificECU);
            return LoadVariableOptions(language, toolChoice[1], shortSpecificECU); ;
        }
        public string[] GetMeasureValueChoices_ReadData(string language, string longSpecificECU)
        {//Takes long string of ECU an looks up short version. Then loads languagespecific List of selected ECU
            string shortSpecificECU = GetFirstPartOfTupel("ECU", longSpecificECU);
            return LoadVariableOptions(language, toolChoice[3], shortSpecificECU); ;
        }
        public string[] GetMeasurementChoices_SmartTool(string language, string specificSmartTool)
        {
            System.Diagnostics.Debug.WriteLine("SM_Tupelwechsel von " + specificSmartTool);
            string englishName = GetFirstPartOfTupel("SmartTool", specificSmartTool);
            System.Diagnostics.Debug.WriteLine(" zu " + englishName);
            return LoadVariableOptions(language, toolChoice[2], englishName); ;
        }

        private string[] LoadVariableOptions(string languageORLengthChoice, string toolChoice, string smartToolOrECU)
        {//Depending on Chosen input Loads String and splits it apart. That gives it to another method that splits even further 
            string[] fileContentPart_EN_DE;
            if (DataBaseOrTxtFile)
            {
                string stream = LoadInputFromDatabase();
                fileContentPart_EN_DE = stream.Split('|');
            }
            else
            {
                string filePath = CreatePathString(toolChoice, smartToolOrECU);
                string stream = LoadInputFromTxtFile(filePath);
                fileContentPart_EN_DE = stream.Split('|');
            }
            return GetFirstOrSecondPartOfSubString(languageORLengthChoice, fileContentPart_EN_DE);
        }
        private string[] LoadVariableOptions(string languageORLengthChoice, string toolChoice)
        {//Depending on Chosen input Loads String and splits it apart. That gives it to another method that splits even further 
            string[] fileContentPart_EN_DE;
            if (DataBaseOrTxtFile)
            {
                string stream = LoadInputFromDatabase();
                fileContentPart_EN_DE = stream.Split('|');
            }
            else
            {
                string filePath = CreatePathString(toolChoice);
                string stream = LoadInputFromTxtFile(filePath);
                fileContentPart_EN_DE = stream.Split('|');
            }
            System.Diagnostics.Debug.WriteLine("LoadVar: " + fileContentPart_EN_DE);
            return GetFirstOrSecondPartOfSubString(languageORLengthChoice, fileContentPart_EN_DE);
        }
        private string CreatePathString(string toolChoiceFromDropDown, string smartToolOrECU)
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
            else if (toolChoiceFromDropDown == "ECU")
            {
                filePathAndName = path + "ECU_List" + ".txt";
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

        private string LoadInputFromTxtFile(string fileNameAndPath)
        {
            //System.Diagnostics.Debug.WriteLine("Load File: " + fileNameAndPath);
            StreamReader sr = new StreamReader(fileNameAndPath);
            return sr.ReadLine();
        }
        private string LoadInputFromDatabase()
        {
            throw new NotImplementedException();
        }


        public string GetFirstPartOfTupel(string toolChoice, string smartToolOrECU) //Man ey, das werden so verdammt viele Zugriffe ;<
        {//Gets Index of the item in the List it derives from and returns the opposite Part of the Tupel

            int index;
            string temp;
            if (toolChoice == "SmartTool")
            {
                index = Array.IndexOf(LoadVariableOptions("second", toolChoice), smartToolOrECU);
                System.Diagnostics.Debug.WriteLine("FirstOfTupel_SmartTool");
                temp = LoadVariableOptions("first", toolChoice)[index];
            }
            else
            {
                index = Array.IndexOf(LoadVariableOptions("second", toolChoice, smartToolOrECU), smartToolOrECU);
                System.Diagnostics.Debug.WriteLine("FirstOfTupel_Nicht-SmartTool");
                temp = LoadVariableOptions("first", toolChoice, smartToolOrECU)[index];
            }
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
        public string GetSecondPartOfTupel(string toolChoice, string smartToolOrECU)
        {//Gets Index of the item in the List it derives from and returns the opposite Part of the Tupel
            int index = Array.IndexOf(LoadVariableOptions("first", toolChoice, smartToolOrECU), smartToolOrECU);
            return LoadVariableOptions("second", toolChoice, smartToolOrECU)[index];
        }
    }
}
