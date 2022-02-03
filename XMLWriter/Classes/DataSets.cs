using System;
using System.Collections.Generic;
using System.Linq;
using XMLWriter.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using System.Text;

namespace XMLWriter.Classes
{
    

    internal class DataSets
    {
        ConsoleControl consol = new ConsoleControl();

        private static List<DataSet> dataSets = new List<DataSet>();
        private static int stepCount = 0;
        private static int stepCountMax = 0;
        private static string dataType = "rep";
        private static string fileName = "Dateiname"; //can include the path

        private static readonly string[] dataTypeChoice = { "gfs", "rep" }; //Gehört eigentlich nicht in diese Klasse, aber bis mir ein besserer Ort einfällt bleibts wohl hier
                                                                            //private static readonly string[] toolChoice = { "actuatorTest", "ReadData", "SmartTool" };

        //Getter
        public List<DataSet> GetDataSets() => dataSets;
        public string GetDataType() => dataType;  // active rep or gfs
        public string GetFileName() => fileName; //Where it should be written to. Includes the Path
        public string[] GetDataTypeChoice() => dataTypeChoice; //rep or gfs selection
        public string[] GetStepNames()
        {
            string[] stepNames = new string[stepCountMax]; //foreach wäre eleganter
            for (int i = 0; i < stepCountMax; i++)
            {
                stepNames[i] = dataSets.ElementAt(i).stepName;
            }
            return stepNames;
        } //This is the list of all entries of StepName used in a Set to use for the dropdown/textfields of postiveID and negativeID
        //DataSet data
        public int GetStepCountMax() => stepCountMax;
        public int GetStepCount() => stepCount; //Index


        //Setter 
        public void SetDataSet(DataSet _dataSet) //Ich habe kA warum ich nicht einfach das ganze Objekt übergeben kann.
        {
            if (stepCount == stepCountMax)
            {
                dataSets.Add(_dataSet);
            }
            else
            {
                dataSets.Insert(stepCount, _dataSet);
            }
        }  
        public void SetStepCountMax(int _inputStepCountMax) => stepCountMax = _inputStepCountMax;
        public void SetStepCount(int _inputStepCount) => stepCount = _inputStepCount;
        public void SetDataType(string _inputDataType) => dataType = _inputDataType;


        public void ResetDataSet()
        {
            dataSets.Clear();
            DataSet dataSet = new DataSet("","","","default","","","","","","","","",false,false,"");
            SetDataSet(dataSet);
        }
        public void InitNewSet()
        {
            if(stepCount == stepCountMax)
            {
                DataSet dataSet = new DataSet("", "", "", "default", "", "", "", "", "", "", "", "", false, false, "");
                dataSets.Add(dataSet);
            }
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
                case "rep":
                    WriteRepToXML rep = new WriteRepToXML();
                    rep.OutputToXML(stepCountMax, dataSets, fileName);
                    break;
                case "gfs":
                    WriteGFSToXML gfs = new WriteGFSToXML();
                    gfs.OutputToXML(stepCountMax, dataSets, fileName);
                    break;
                default:
                    if (consol.showErrors) Console.WriteLine("Error in OutputToXML from DataSet                   ---DataSet.OutputToXML()");
                    break;
            }
        }
        private void ConsoleShowGfsSaveContent(string toolChoice, string stepName, string text, string anim, string instructionText, string posID, string negID, string posResult, string repXML, string actuatorTest, string RDBI, string smartTool, bool? nextStep, bool? lastStep)
        {
            if (consol.showSaveStep)
            {
                System.Diagnostics.Debug.WriteLine("       vvvvvvvvvvvvv-Speichern-vvvvvvvvvvvvv---DataSet.SaveSet(gfs)");
                System.Diagnostics.Debug.WriteLine("Index:  " + stepCount);
                System.Diagnostics.Debug.WriteLine("Tool:   " + toolChoice);
                System.Diagnostics.Debug.WriteLine("step:   " + stepName);
                System.Diagnostics.Debug.WriteLine("Text:   " + text);
                System.Diagnostics.Debug.WriteLine("Anim:   " + anim);
                System.Diagnostics.Debug.WriteLine("Instr:  " + instructionText);
                System.Diagnostics.Debug.WriteLine("posID:  " + posID);
                System.Diagnostics.Debug.WriteLine("negID:  " + negID);
                System.Diagnostics.Debug.WriteLine("posRes: " + posResult);
                System.Diagnostics.Debug.WriteLine("repXML: " + repXML);
                System.Diagnostics.Debug.WriteLine("A-Test: " + actuatorTest);
                System.Diagnostics.Debug.WriteLine("SmT:    " + smartTool);
                System.Diagnostics.Debug.WriteLine("RDID:   " + RDBI);
                System.Diagnostics.Debug.WriteLine("NextST: " + nextStep);
                System.Diagnostics.Debug.WriteLine("LastSt: " + lastStep);
                System.Diagnostics.Debug.WriteLine("       ^^^^^^^^^^^^^-Speichern-^^^^^^^^^^^^^---DataSet.SaveSet(gfs)");
            }
        }
    }
    
}
