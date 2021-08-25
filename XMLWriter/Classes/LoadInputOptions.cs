using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XMLWriter.Classes
{
    class LoadInputOptions
    {
        private static bool DataBaseOrTxtFile; //True=Database, False=txt
        Language language = new Language();
        //private static string filePathECU = @"D:\Projekte\Studium\XMLWriter\XMLWriter\XMLWriter\bin\Debug\netcoreapp3.1\Files\ECU_";
        private static readonly string path = @"Files/";
        private static readonly string[] toolChoice = { "", "ActuatorTest", "SmartTool", "ReadDataByIdentifier" };

        public void SetDataBase(bool? check)
        {
            if (check==true)
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
        {
            return LoadVariableOptions("long", "ECU"); //Es dürfte egal sein, was in dem dritten Parameter steht.
        }
        public string[] GetSmartToolChoices()
        {
            return LoadVariableOptions("long", "SmartTool");
        }


        //Get Acturtortest 2nd Dropdown Options 
        public string[] Get_AT_IOChoices(string language, string longSpecificECU) 
        {
            System.Diagnostics.Debug.WriteLine("\nIO -1");
            string shortSpecificECU = GetFirstPartOfTupel("ECU", longSpecificECU);
            System.Diagnostics.Debug.WriteLine("\nIO -2");
            System.Diagnostics.Debug.WriteLine(shortSpecificECU);
            
            return LoadVariableOptions(language, toolChoice[1], shortSpecificECU); ;
        }
        //Get ReadData 2nd Dropdown Options 
        public string[] GetRDIDChoices(string language, string longSpecificECU) 
        {
            string shortSpecificECU = GetFirstPartOfTupel("ECU",longSpecificECU);
            return LoadVariableOptions(language, toolChoice[3], shortSpecificECU); ;
        }
        //Get SmartTool 2nd Dropdown Options 
        public string[] GetMeasurementChoices(string language, string specificSmartTool) 
        {
            System.Diagnostics.Debug.WriteLine("hier?");
            return LoadVariableOptions(language, toolChoice[2], specificSmartTool); ;
        }

        private string[] LoadVariableOptions(string languageORLengthChoice, string toolChoice, string smartToolOrECU)
        {
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
            System.Diagnostics.Debug.WriteLine("LoadVar: " + fileContentPart_EN_DE);
            return GetFirstOrSecondPartOfSubString(languageORLengthChoice, fileContentPart_EN_DE);
        }
        private string[] LoadVariableOptions(string languageORLengthChoice, string toolChoice)
        {
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
        private string[] GetFirstOrSecondPartOfSubString(string languageORLengthChoice, string[] fileContentPart)
        {
            string[] output;
            List<string> tempOutput = new List<string>();
            string[] fileContentSubPart;
            for (int i = 0; i < fileContentPart.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                fileContentSubPart = fileContentPart[i].Split(';');

                if (languageORLengthChoice == language.GetLanguageChoises()[1] || languageORLengthChoice == "first" || languageORLengthChoice == "short") //"English"
                {
                    tempOutput.Add(fileContentSubPart[0]);
                }
                else if (languageORLengthChoice == language.GetLanguageChoises()[0] || languageORLengthChoice == "second" || languageORLengthChoice == "long") //"Deutsch"
                {
                    tempOutput.Add(fileContentSubPart[1]);
                }
                else
                {
                    tempOutput.Add("Fehler in LoadInputOptions.LoadInputFromTxtFile");
                    System.Diagnostics.Debug.WriteLine("Fehler in LoadInputOptions.GetFirstOrSecondPartOfSubString");
                }
            }
            System.Diagnostics.Debug.WriteLine("GetFirstOrSecondPartOfSubString: " + tempOutput);
            output = tempOutput.ToArray();
            return output;
    }
        private string CreatePathString(string toolChoiceFromDropDown, string SmartToolOrECU)
        {
            string filePathAndName;
            if (toolChoiceFromDropDown == toolChoice[1])
            {
                filePathAndName = path + "IO_" + SmartToolOrECU + ".txt";
            }
            else if(toolChoiceFromDropDown== toolChoice[2])
            {
                filePathAndName = path + "Measure_" + SmartToolOrECU + ".txt";
            }
            else if (toolChoiceFromDropDown == toolChoice[3])
            {
                filePathAndName = path + "RDID_" + SmartToolOrECU + ".txt";
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
            return filePathAndName;
        }
        private string CreatePathString(string toolChoiceFromDropDown)
        {
            System.Diagnostics.Debug.WriteLine("CreatePathString: " + toolChoiceFromDropDown + "---");
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
            return filePathAndName;
        }
        private string LoadInputFromTxtFile(string fileNameAndPath)
        {
            System.Diagnostics.Debug.WriteLine("Bla: " + fileNameAndPath);
            System.Diagnostics.Debug.WriteLine(fileNameAndPath);
            System.Diagnostics.Debug.WriteLine(fileNameAndPath);
            System.Diagnostics.Debug.WriteLine(fileNameAndPath);
            System.Diagnostics.Debug.WriteLine(fileNameAndPath);
            StreamReader sr = new StreamReader(fileNameAndPath);
            return sr.ReadLine();
        }
        private string LoadInputFromDatabase()
        {
            throw new NotImplementedException();
        }

        
        //Firt Part is Short ECU or english Name
        public string GetFirstPartOfTupel(string toolChoice, string smartToolOrECU) //Man ey, das werden so verdammt viele Zugriffe ;<
        {//kA wie ich das erklären soll. Ich stig ja selbst kaum noch durch.
            System.Diagnostics.Debug.WriteLine("getFirstPartOfTupel: " + toolChoice + " " + smartToolOrECU);
            int index = Array.IndexOf(LoadVariableOptions("second", toolChoice, smartToolOrECU), smartToolOrECU);
            System.Diagnostics.Debug.WriteLine("getFirstPartOfTupel: " + toolChoice + " " + smartToolOrECU + " " + index);
            return LoadVariableOptions("first", toolChoice, smartToolOrECU)[index];
        }
        public string GetSecondPartOfTupel(string toolChoice, string smartToolOrECU) 
        {
            int index = Array.IndexOf(LoadVariableOptions("first", toolChoice, smartToolOrECU), smartToolOrECU);
            return LoadVariableOptions("second", toolChoice, smartToolOrECU)[index];
        }








    }
}
