using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XMLWriter.Classes
{
    class DropDownOptionLists
    {
        
        private static bool useDataBase=true;
        private static string[] toolChoice = { "", "ActuatorTest", "SmartTool", "ReadDataByIdentifier" };
        private static string filePath = @"Files/";
        private static string databasePath = @"Files/";
        //private static string filePathECU = @"D:\Projekte\Studium\XMLWriter\XMLWriter\XMLWriter\bin\Debug\netcoreapp3.1\Files\ECU_";
        //Die Dateinamen sind MIT SICHERHEIT nicht final
        private static string fileNameECU = @"ECU_List.txt";
        private static string databaseQueryECU = @"";
        private static string fileNameSmartTool = @"SmartTool_List.txt";
        private static string databaseQuerySmartTool = @"";

        private static string fileNameIO_BCM = "IO_BCM.txt";
        private static string databaseQueryIO_BCM = "";
        private static string fileNameIO_LWR = "IO_LWR.txt";
        private static string databaseQueryIO_LWR = "";
        private static string fileNameIO_MSG = "IO_MSG.txt";
        private static string databaseQueryIO_MSG = "";

        private static string fileNameRDID_BCM = "RDID_BCM.txt";
        private static string databaseQueryRDID_BCM = "";
        private static string fileNameRDID_LWR = "RDID_LWR.txt";
        private static string databaseQueryRDID_LWR = "";
        private static string fileNameRDID_MSG = "RDID_MSG.txt";
        private static string databaseQueryRDID_MSG = "";

        private static string fileNameMeasure_URI = @"Measure_URI.txt";
        private static string databaseQueryMeasure_URI = @"";
        private static string fileNameMeasure_Two = @"Measure_Two.txt";
        private static string databaseQueryMeasure_Two = @"";
        private static string fileNameMeasure_Three = @"Measure_Three.txt";
        private static string databaseQueryMeasure_Three = @"";

        

        //First Part is always the Save/load option and second part is the display option
        private static List<DropDownOptionTupel> ECU_List = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> SmartTool_List = new List<DropDownOptionTupel>();

        private static List<DropDownOptionTupel> IO_BCM = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> IO_LWR = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> IO_MSG = new List<DropDownOptionTupel>();

        private static List<DropDownOptionTupel> Measure_URI = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> Measure_Two = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> Measure_Three= new List<DropDownOptionTupel>();

        private static List<DropDownOptionTupel> RDID_BCM= new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> RDID_LWR = new List<DropDownOptionTupel>();
        private static List<DropDownOptionTupel> RDID_MSG = new List<DropDownOptionTupel>();
        

        public void UseDataBase()
        {
            System.Diagnostics.Debug.WriteLine("Loads from database");
            useDataBase = true;
        }
        public void DontUseDataBase()
        {
            System.Diagnostics.Debug.WriteLine("Loads from text file");
            useDataBase = false;
        }
        public void LoadAllOptions()
        {
            FillAllLists();
        }
        private void FillAllLists()
        {
            FillECU_List();
            FillSmartTool_List();
            FillIO_BCM();
            FillIO_LWR();
            FillIO_MSG();
            FillRDID_BCM();
            FillRDID_LWR();
            FillRDID_MSG();
            FillMeasure_URI(); 
            FillMeasure_two();      //Hier gibts sicherlich Bedarf an Umbenennung
            FillMeasure_three();    //Hier gibts sicherlich Bedarf an Umbenennung
        }
        private void FillECU_List()
        {
            string stream;
            System.Diagnostics.Debug.WriteLine(filePath + fileNameECU);
            if (useDataBase)
            {
                stream = LoadInputFromTxtFile(filePath + fileNameECU);
            }
            else
            {
                stream = LoadInputFromDatabase(databasePath + databaseQueryECU);
            }
            
            string[] TupelString;
            TupelString = stream.Split('|');
            for (int i = 0; i < TupelString.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = TupelString[i].Split(';');
                ECU_List.Add(new DropDownOptionTupel(temp[0], temp[1]));
                System.Diagnostics.Debug.WriteLine(temp[0] + " " + temp[1]);
            }
        }
        private void FillSmartTool_List()
        {
            string stream;
            if (useDataBase)
            {
                stream = LoadInputFromTxtFile(filePath + fileNameSmartTool);
            }
            else
            {
                stream = LoadInputFromDatabase(databasePath + databaseQuerySmartTool);
            }

            string[] TupelString;
            TupelString = stream.Split('|');
            for (int i = 0; i < TupelString.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = TupelString[i].Split(';');
                SmartTool_List.Add(new DropDownOptionTupel(temp[0], temp[1]));
                System.Diagnostics.Debug.WriteLine(temp[0] + " " + temp[1]);
            }
        }
        private void FillIO_BCM()
        {
            string stream;
            if (useDataBase)
            {
                stream = LoadInputFromTxtFile(filePath + fileNameIO_BCM);
            }
            else
            {
                stream = LoadInputFromDatabase(databasePath + databaseQueryIO_BCM);
            }

            string[] TupelString;
            TupelString = stream.Split('|');
            for (int i = 0; i < TupelString.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = TupelString[i].Split(';');
                IO_BCM.Add(new DropDownOptionTupel(temp[0], temp[1]));
                System.Diagnostics.Debug.WriteLine(temp[0] + " " + temp[1]);
            }
        }
        private void FillIO_LWR()
        {
            string stream;
            if (useDataBase)
            {
                stream = LoadInputFromTxtFile(filePath + fileNameIO_LWR);
            }
            else
            {
                stream = LoadInputFromDatabase(databasePath + databaseQueryIO_LWR);
            }

            string[] TupelString;
            TupelString = stream.Split('|');
            for (int i = 0; i < TupelString.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = TupelString[i].Split(';');
                IO_LWR.Add(new DropDownOptionTupel(temp[0], temp[1]));
                System.Diagnostics.Debug.WriteLine(temp[0] + " " + temp[1]);
            }
        }
        private void FillIO_MSG()
        {
            string stream;
            if (useDataBase)
            {
                stream = LoadInputFromTxtFile(filePath + fileNameIO_MSG);
            }
            else
            {
                stream = LoadInputFromDatabase(databasePath + databaseQueryIO_MSG);
            }

            string[] TupelString;
            TupelString = stream.Split('|');
            for (int i = 0; i < TupelString.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = TupelString[i].Split(';');
                IO_MSG.Add(new DropDownOptionTupel(temp[0], temp[1]));
                System.Diagnostics.Debug.WriteLine(temp[0] + " " + temp[1]);
            }
        }
        private void FillRDID_BCM()
        {
            string stream;
            if (useDataBase)
            {
                stream = LoadInputFromTxtFile(filePath + fileNameRDID_BCM);
            }
            else
            {
                stream = LoadInputFromDatabase(databasePath + databaseQueryRDID_BCM);
            }

            string[] TupelString;
            TupelString = stream.Split('|');
            for (int i = 0; i < TupelString.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = TupelString[i].Split(';');
                RDID_BCM.Add(new DropDownOptionTupel(temp[0], temp[1]));
                System.Diagnostics.Debug.WriteLine(temp[0] + " " + temp[1]);
            }
        }
        private void FillRDID_LWR()
        {
            string stream;
            if (useDataBase)
            {
                stream = LoadInputFromTxtFile(filePath + fileNameRDID_LWR);
            }
            else
            {
                stream = LoadInputFromDatabase(databasePath + databaseQueryRDID_LWR);
            }

            string[] TupelString;
            TupelString = stream.Split('|');
            for (int i = 0; i < TupelString.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = TupelString[i].Split(';');
                RDID_LWR.Add(new DropDownOptionTupel(temp[0], temp[1]));
                System.Diagnostics.Debug.WriteLine(temp[0] + " " + temp[1]);
            }
        }
        private void FillRDID_MSG()
        {
            string stream;
            if (useDataBase)
            {
                stream = LoadInputFromTxtFile(filePath + fileNameRDID_MSG);
            }
            else
            {
                stream = LoadInputFromDatabase(databasePath + databaseQueryRDID_MSG);
            }

            string[] TupelString;
            TupelString = stream.Split('|');
            for (int i = 0; i < TupelString.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = TupelString[i].Split(';');
                RDID_MSG.Add(new DropDownOptionTupel(temp[0], temp[1]));
                System.Diagnostics.Debug.WriteLine(temp[0] + " " + temp[1]);
            }
        }
        private void FillMeasure_URI()
        {
            string stream;
            if (useDataBase)
            {
                stream = LoadInputFromTxtFile(filePath + fileNameMeasure_URI);
            }
            else
            {
                stream = LoadInputFromDatabase(databasePath + databaseQueryMeasure_URI);
            }

            string[] TupelString;
            TupelString = stream.Split('|');
            for (int i = 0; i < TupelString.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = TupelString[i].Split(';');
                Measure_URI.Add(new DropDownOptionTupel(temp[0], temp[1]));
                System.Diagnostics.Debug.WriteLine(temp[0] + " " + temp[1]);
            }
        }
        private void FillMeasure_two()
        {
            string stream;
            if (useDataBase)
            {
                stream = LoadInputFromTxtFile(filePath + fileNameMeasure_Two);
            }
            else
            {
                stream = LoadInputFromDatabase(databasePath + databaseQueryMeasure_Two);
            }

            string[] TupelString;
            TupelString = stream.Split('|');
            for (int i = 0; i < TupelString.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = TupelString[i].Split(';');
                Measure_Two.Add(new DropDownOptionTupel(temp[0], temp[1]));
                System.Diagnostics.Debug.WriteLine(temp[0] + " " + temp[1]);
            }
        }
        private void FillMeasure_three()
        {
            string stream;
            if (useDataBase)
            {
                stream = LoadInputFromTxtFile(filePath + fileNameMeasure_Three);
            }
            else
            {
                stream = LoadInputFromDatabase(databasePath + databaseQueryMeasure_Three);
            }

            string[] TupelString;
            TupelString = stream.Split('|');
            for (int i = 0; i < TupelString.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                string[] temp = TupelString[i].Split(';');
                Measure_Three.Add(new DropDownOptionTupel(temp[0], temp[1]));
                System.Diagnostics.Debug.WriteLine(temp[0] + " " + temp[1]);
            }
        }
        

        //nullte Dropdownebene
        public string[] GetToolChoice()
        {
            return toolChoice;
        }
        //erste Dropdownebene
        public string[] GetECUChoices()
        {
            return ECU_List.Select(x => x.secondPart).ToArray();
        }
        public string[] GetSmartToolChoices()
        {
            return SmartTool_List.Select(x => x.secondPart).ToArray();
        }
        //zweite Dropdownebene
        public string[] GetRDIDChoices(string chosenECU)
        {
            switch (chosenECU)
            {//Inhalt hier ist redundant. Ich weiß noch nicht, in welcher Art chosenECU übergeben werden soll
                case "BCM":
                    return RDID_BCM.Select(x => x.secondPart).ToArray();
                case "LWR":
                    return RDID_LWR.Select(x => x.secondPart).ToArray();
                case "MSG":
                    return RDID_MSG.Select(x => x.secondPart).ToArray();
                case "Bordnetz Steuergeraet":
                    return RDID_BCM.Select(x => x.secondPart).ToArray();
                case "Leuchtweitenregulierung":
                    return RDID_LWR.Select(x => x.secondPart).ToArray();
                case "Motorsteuergeraet":
                    return RDID_MSG.Select(x => x.secondPart).ToArray();
                default:
                    return toolChoice; //Mangels besserer Ideen die Anzeige für einen fehlerhaften Inhalt
            }
        }
        public string[] GetMeasurementChoices(string chosenECU)
        {
            switch (chosenECU)
            {//Inhalt hier ist redundant. Ich weiß noch nicht, in welcher Art chosenECU übergeben werden soll
                case "BCM":
                    return IO_BCM.Select(x => x.secondPart).ToArray();
                case "LWR":
                    return IO_LWR.Select(x => x.secondPart).ToArray();
                case "MSG":
                    return IO_MSG.Select(x => x.secondPart).ToArray();
                case "Bordnetz Steuergeraet":
                    return IO_BCM.Select(x => x.secondPart).ToArray();
                case "Leuchtweitenregulierung":
                    return IO_LWR.Select(x => x.secondPart).ToArray();
                case "Motorsteuergeraet":
                    return IO_MSG.Select(x => x.secondPart).ToArray();
                default:
                    return toolChoice; //Mangels besserer Ideen die Anzeige für einen fehlerhaften Inhalt
            }
        }
        public string[] GetMeasurementChoices()
        {
            return Measure_URI.Select(x => x.secondPart).ToArray();
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
