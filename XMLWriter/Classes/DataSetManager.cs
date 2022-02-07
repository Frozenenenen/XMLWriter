using System.Collections.Generic;
using System.Linq;

using System.Text;
using XMLWriter.Classes.HelpClasses;

namespace XMLWriter.Classes {


    internal class DataSetManager {
        //ConsoleControl consol = new ConsoleControl();
        LoadHelper loadHelper = new LoadHelper();
        LoadDataHelper loadDataHelper = new LoadDataHelper();

        private static List<DataSet> dataSets = new List<DataSet>();
        private static int stepCount = 0;
        private static int stepCountMax = 0;
        private static string dataType = "rep";
        private static string fileName = "Dateiname"; //can include the path



        //Getter
        public List<DataSet> GetDataSets() => dataSets;
        public string GetDataType() => dataType;  // active rep or gfs
        public string GetFileName() => fileName; //Where it should be written to. Includes the Path

        public string[] GetStepNames() {
            string[] stepNames = new string[stepCountMax]; //foreach wäre eleganter
            for (int i = 0; i < stepCountMax; i++) {
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
            if (stepCount == stepCountMax) {
                dataSets.Add(_dataSet);
            }
            else {
                dataSets.Insert(stepCount, _dataSet);
            }
            //consol.ConsoleShowDataSetOfIndex(_dataSet, stepCount, "Speichern");
        }
        public void SetStepCountMax(int _inputStepCountMax) => stepCountMax = _inputStepCountMax;
        public void SetStepCount(int _inputStepCount) => stepCount = _inputStepCount;
        public void SetDataType(string _inputDataType) => dataType = _inputDataType;

        public void LoadDataFromFile() {
            loadHelper.LookForInitialDirectory();
            loadHelper.OpenFileDialog();

            if (loadDataHelper.IsFilePathValid()) {

                loadDataHelper.ReadXMLStreamAndWriteToDataSets();
            }
        }
        public void ResetDataSet() {
            dataSets.Clear();
            SetStepCount(0);
            SetStepCountMax(0);
            InitNewDataSet();
        }
        public void InitNewDataSet() {
            if (stepCount == stepCountMax) {
                dataSets.Add(new DataSet("", "", "", "default", "", "", "", "", "", "", "", "", false, false, ""));
            }
        }

        public void SetFileName(string inputFileName) //Damit keine vorherigen Daten überschrieben werden, wird der Dateiname iteriert, bis ein neuer Dateiname gefunden wurde.
        {
            DataSetToXMLWriter writer = new DataSetToXMLWriter();
            writer.SetFileName(inputFileName);//, dataType);

        }
        public void OutputToXML() //Output to file
        {
            switch (dataType) {
                case "rep":
                    RepToXMLWriter rep = new RepToXMLWriter();
                    rep.OutputToXML(stepCountMax, dataSets, fileName);
                    break;
                case "gfs":
                    GFSToXMLWriter gfs = new GFSToXMLWriter();
                    gfs.OutputToXML(stepCountMax, dataSets, fileName);
                    break;
                default:
                    //if (consol.showErrors) Console.WriteLine("Error in OutputToXML from DataSet                   ---DataSet.OutputToXML()");
                    break;
            }
        }

    }

}
