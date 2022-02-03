using System.Collections.Generic;
using System.Linq;

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

        

        //Getter
        public List<DataSet> GetDataSets() => dataSets;
        public string GetDataType() => dataType;  // active rep or gfs
        public string GetFileName() => fileName; //Where it should be written to. Includes the Path
        
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
            ShowAllDataInConsole(_dataSet);
        }  
        public void SetStepCountMax(int _inputStepCountMax) => stepCountMax = _inputStepCountMax;
        public void SetStepCount(int _inputStepCount) => stepCount = _inputStepCount;
        public void SetDataType(string _inputDataType) => dataType = _inputDataType;


        public void ResetDataSet()
        {
            dataSets.Clear();
            SetStepCount(0);
            SetStepCountMax(0);
            InitNewSet();
        }
        public void InitNewSet()
        {
            if(stepCount == stepCountMax)
            {
                dataSets.Add(new DataSet("", "", "", "default", "", "", "", "", "", "", "", "", false, false, ""));
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
                    //if (consol.showErrors) Console.WriteLine("Error in OutputToXML from DataSet                   ---DataSet.OutputToXML()");
                    break;
            }
        }
        private void ShowAllDataInConsole(DataSet dataSet)
        {
            if (consol.showSaveStep)
            {
                System.Diagnostics.Debug.WriteLine("vvvvvvvvvvvvvvvvv Speichern vvvvvvvvvvvvvvvvv");
                System.Diagnostics.Debug.WriteLine("Index:  " + stepCount);
                System.Diagnostics.Debug.WriteLine("Tool:   " + dataSet.toolChoice);
                System.Diagnostics.Debug.WriteLine("Step:   " + dataSet.stepName);
                System.Diagnostics.Debug.WriteLine("Text:   " + dataSet.text);
                System.Diagnostics.Debug.WriteLine("Anim:   " + dataSet.anim);
                System.Diagnostics.Debug.WriteLine("instr:  " + dataSet.instruction);
                System.Diagnostics.Debug.WriteLine("posID:  " + dataSet.positiveID);
                System.Diagnostics.Debug.WriteLine("negID:  " + dataSet.negativeID);
                System.Diagnostics.Debug.WriteLine("posRes: " + dataSet.positiveResult);
                System.Diagnostics.Debug.WriteLine("repXML: " + dataSet.repXML);
                System.Diagnostics.Debug.WriteLine("A-Test: " + dataSet.actuatorTest);
                System.Diagnostics.Debug.WriteLine("SmarT:  " + dataSet.smartTool);
                System.Diagnostics.Debug.WriteLine("RDID:   " + dataSet.RDID);
                System.Diagnostics.Debug.WriteLine("Next:   " + dataSet.nextStep);
                System.Diagnostics.Debug.WriteLine("Last:   " + dataSet.lastStep);
                System.Diagnostics.Debug.WriteLine("^^^^^^^^^^^^^^^^^ Speichern ^^^^^^^^^^^^^^^^^");
            }

        }
    }
    
}
