using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XMLWriter.Classes
{
    class DropDownOptionLists
    {

        private static bool useDataBase = true;           //Hier ist noch ein Fehler
        private static string[] toolChoice = { "", "ActuatorTest", "SmartTool", "ReadDataByIdentifier" };
        private static string filePath = @"Files/";
        private static string databasePath = @"Files/";
        //Die Dateinamen sind MIT SICHERHEIT nicht final
        private static string[] fileNames = { @"ECU_List.txt", @"SmartTool_List.txt", @"IO_BCM.txt", @"IO_LWR.txt", @"IO_MSG.txt", @"Measure_URI.txt", @"Measure_Two.txt", @"Measure_Three.txt", @"RDID_BCM.txt", @"RDID_LWR.txt", @"RDID_MSG.txt" };
        private static string[] databaseQuerys = { @"", @"", @"", @"", @"", @"", @"", @"", @"", @"", @"" };






        //First Part is always the Save/load option and second part is the display option
        private static List<DropDownOptionTupel> ECU_List = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> SmartTool_List = new List<DropDownOptionTupel>();

        private static List<DropDownOptionTupel> IO_BCM = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> IO_LWR = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> IO_MSG = new List<DropDownOptionTupel>();

        private static List<DropDownOptionTupel> Measure_URI = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> Measure_Two = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> Measure_Three = new List<DropDownOptionTupel>();

        private static List<DropDownOptionTupel> RDID_BCM = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> RDID_LWR = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> RDID_MSG = new List<DropDownOptionTupel>();


        public void UseDataBase()
        {
            System.Diagnostics.Debug.WriteLine("Loads from database - Init");
            useDataBase = true;
        }
        public void DontUseDataBase()
        {
            System.Diagnostics.Debug.WriteLine("Loads from text file - Init");
            useDataBase = false;
        }
        public void LoadAllOptions()
        {
            FillAllLists();
        }
        private void FillAllLists()
        {
            FillList(ECU_List, fileNames[0], databaseQuerys[0]);
            FillList(SmartTool_List, fileNames[1], databaseQuerys[1]);
            FillList(IO_BCM, fileNames[2], databaseQuerys[2]);
            FillList(IO_LWR, fileNames[3], databaseQuerys[3]);
            FillList(IO_MSG, fileNames[4], databaseQuerys[4]);
            FillList(Measure_URI, fileNames[5], databaseQuerys[5]);
            FillList(Measure_Two, fileNames[6], databaseQuerys[6]);
            FillList(Measure_Three, fileNames[7], databaseQuerys[7]);
            FillList(RDID_BCM, fileNames[8], databaseQuerys[8]);
            FillList(RDID_LWR, fileNames[9], databaseQuerys[9]);
            FillList(RDID_MSG, fileNames[10], databaseQuerys[10]);
        }

        private void FillList(List<DropDownOptionTupel> list, string fileName, string databaseQuery)
        {
            string stream;
            if (useDataBase)
            {
                stream = LoadInputFromTxtFile(filePath + fileName);
            }
            else
            {
                stream = LoadInputFromDatabase(databasePath + databaseQuery);
            }
            string[] TupelString;
            TupelString = stream.Split('|');
            for (int i = 0; i < TupelString.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = TupelString[i].Split(';');
                list.Add(new DropDownOptionTupel(temp[0], temp[1]));
            }
        }

        public string GetDisplayPartOf(List<DropDownOptionTupel> list, string item)
        {
            if (list != null && item != "")
            {
                System.Diagnostics.Debug.WriteLine(item);
                int index = list.FindIndex(x => x.firstPart.Equals(item));
                if (index != -1)
                {
                    return list.ElementAt(index).secondPart;
                }
            }
            return "";
        }
       
        public string GetKeyPartOf(List<DropDownOptionTupel> list, string item)
        {
            if (list != null && item !="")
            {
                System.Diagnostics.Debug.WriteLine(item);
                int index = list.FindIndex(x => x.secondPart.Equals(item));
                if (index!=-1)
                {
                    return list.ElementAt(index).firstPart;
                }
            }
            return "";
        }
        

        public string[] GetToolChoice()
        {
            return toolChoice;
        }
        public List<DropDownOptionTupel> GetECUChoices()
        {
            return ECU_List; 
        }
        public List<DropDownOptionTupel> GetSmartToolChoices()
        {
            return SmartTool_List;
        }
        public List<DropDownOptionTupel> GetRDIDChoices(string chosenECU)
        {
            if (chosenECU == ECU_List.Select(x => x.secondPart).ToArray()[0])
            {
                return RDID_BCM;
            }
            else if (chosenECU == ECU_List.Select(x => x.secondPart).ToArray()[1])
            {
                return RDID_LWR;
            }
            else if (chosenECU == ECU_List.Select(x => x.secondPart).ToArray()[2])
            {
                return RDID_MSG;
            }
            else
            {
                return RDID_BCM; //Mangels besserer Ideen die Anzeige für einen fehlerhaften Inhalt
            }
        }
        public List<DropDownOptionTupel> GetIOChoices(string chosenECU)
        {
            if (chosenECU == ECU_List.Select(x => x.secondPart).ToArray()[0])
            {
                return IO_BCM;
            }
            else if (chosenECU == ECU_List.Select(x => x.secondPart).ToArray()[1])
            {
                return IO_LWR;
            }
            else if (chosenECU == ECU_List.Select(x => x.secondPart).ToArray()[2])
            {
                return IO_MSG;
            }
            else
            {
                return IO_BCM;
            }
        }
        public List<DropDownOptionTupel> GetMeasurementChoices(string chosenSmartTool)
        {
            if (chosenSmartTool == SmartTool_List.Select(x => x.secondPart).ToArray()[0])
            {
                return Measure_URI;
            }
            else if (chosenSmartTool == SmartTool_List.Select(x => x.secondPart).ToArray()[1])
            {
                return Measure_Two;
            }
            else if (chosenSmartTool == SmartTool_List.Select(x => x.secondPart).ToArray()[2]) //Wenn es nur 2 Smarttools gibt, wird hier wohl ein Fehler entstehen 
            {
                return Measure_Three;
            }
            else
            {
                return Measure_URI; 
            }
        }




        private string LoadInputFromTxtFile(string fileNameAndPath)
        {
            StreamReader sr = new StreamReader(fileNameAndPath);
            return sr.ReadLine();
        }
        private string LoadInputFromDatabase(string databaseQuery)
        {
            throw new NotImplementedException();
        }
        


    }
}
