using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XMLWriter.Classes
{
    class LoadInputOptions
    {
        //private static string filePathECU = @"D:\Projekte\Studium\XMLWriter\XMLWriter\XMLWriter\bin\Debug\netcoreapp3.1\Files\ECU_";
        private static string filePathECU = @"Files/ECU_List.txt";
        private static string filePathIO = "Files/IO_BCM.txt";
        private static string filePathRDID = "Files/RDID_BCM.txt";
        private static string filePathMeasure = @"Files/Measure_BCM.txt";
        private static string[] toolChoice = { "", "ActuatorTest", "SmartTool", "ReadDataByIdentifier" };
        /*private static List<string> fullECUName;*/
        private static List<string> shortECUName = new List<string>();
        private static List<string> longECUName = new List<string>();
        private static string[] fullECUName;
        private static List<string> componentNameDE = new List<string>();
        private static List<string> componentNameEN = new List<string>();
        private static List<string> readDataID_DE = new List<string>();
        private static List<string> readDataID_EN = new List<string>();
        private static string[] measurements;
        /*private static string[] shortECUName;
        private static string[] longECUName;*/

        public void LoadAllOptions()
        {
            LoadECUOptions();
            LoadIOOptions();
            LoadRDIDOptions();
            LoadMeasurementOptions();
        }
        public string[] GetToolChoice()
        {
            return toolChoice;
        }
        public string[] GetECUChoices()
        {
            //return fullECUName;
            return longECUName.ToArray();
        }
        public string[] GetIOChoices()
        {
            Language language = new Language();
            string[] IOs;
            switch (language.GetStringLanguage())
            {
                case "Deutsch":
                    IOs = componentNameDE.ToArray();
                    break;
                case "English":
                    IOs = componentNameEN.ToArray();
                    break;
                default:
                    IOs = componentNameEN.ToArray();
                    break;
            }
            return IOs;
        }
        public string[] GetRDIDChoices()
        {
            Language language = new Language();
            string[] RDIDs;
            switch (language.GetStringLanguage())
            {
                case "Deutsch":
                    RDIDs = readDataID_DE.ToArray();
                    break;
                case "English":
                    RDIDs = readDataID_EN.ToArray();
                    break;
                default:
                    RDIDs = readDataID_EN.ToArray();
                    break;
            }
            return RDIDs;
        }
        public string[] GetMeasurementChoices()
        {
            return measurements;
        }

        private string LoadInputFromTxtFile(string fileNameAndPath)
        {
            StreamReader sr = new StreamReader(fileNameAndPath);
            return sr.ReadLine();
        }
        private string LoadInputFromDatabase()
        {
            throw new NotImplementedException();
        }
        private void LoadECUOptions()
        {
            string stream = LoadInputFromTxtFile(filePathECU);
            fullECUName = stream.Split('|');
            for (int i = 0; i < fullECUName.Length-1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = fullECUName[i].Split(';');
                shortECUName.Add(temp[0]);
                longECUName.Add(temp[1]);
                System.Diagnostics.Debug.WriteLine(shortECUName[i] + " " + longECUName[i]);
            }
        }
        private void LoadIOOptions()
        {
            string[] componentNames;
            string stream = LoadInputFromTxtFile(filePathIO);
            componentNames = stream.Split('|');
            for (int i = 0; i < componentNames.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = componentNames[i].Split(';');
                componentNameEN.Add(temp[0]);
                componentNameDE.Add(temp[1]);
            }
        }
        private void LoadRDIDOptions()
        {
            string[] readDataIDNames;
            string stream = LoadInputFromTxtFile(filePathRDID);
            readDataIDNames = stream.Split('|');
            for (int i = 0; i < readDataIDNames.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = readDataIDNames[i].Split(';');
                readDataID_EN.Add(temp[0]);
                readDataID_DE.Add(temp[1]);
            }
        }
        private void LoadMeasurementOptions()
        {
            string stream = LoadInputFromTxtFile(filePathMeasure);
            measurements = stream.Split('|');
        }









    }
}
