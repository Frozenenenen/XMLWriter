using System.Collections.Generic;
using System.Linq;
using XMLWriter.Classes.HelpClasses;
using XMLWriter.Classes.StartPage;

namespace XMLWriter.Classes {


    internal class DataSetService {
        GUIMovementHelper gui = new GUIMovementHelper();
        LoadHelper loadHelper = new LoadHelper();

        private static List<DataSet> dataSets = new List<DataSet>();

        //Getter
        public List<DataSet> GetDataSets() => dataSets;
        public string[] GetStepNames() {
            string[] stepNames = new string[dataSets.Count]; //foreach wäre eleganter
            for (int i = 0; i < dataSets.Count; i++) {
                stepNames[i] = dataSets.ElementAt(i).stepName;
            }
            return stepNames;
        } //This is the list of all entries of StepName used in a Set to use for the dropdown/textfields of postiveID and negativeID
        //DataSet data


        //Setter 
        public void TransmitDataSetListFromLoadToDataSetService(List<DataSet> _dataSets) {
            dataSets  = _dataSets;
        }
        public void ResetDataSet() {
            dataSets.Clear();
            gui.ResetStepCount();
            gui.ResetStepCountMax();
            InitNewDataSetWhereRequired();
        }
        public void InitNewDataSetWhereRequired() {
            if (dataSets.Count==gui.GetIndex()) {
                dataSets.Add(new DataSet("", "", "", "default", "", "", "", "", "", "", "", "", false, false, ""));
            }
        }
        public void InsertNewDataSet() {
            dataSets.Insert(gui.GetIndex() ,new DataSet("", "", "", "default", "", "", "", "", "", "", "", "", false, false, ""));
        }
        public void DeleteDataSet() {
            dataSets.RemoveAt(gui.GetIndex());
        }
        public void SetFileName(string inputFileName) //Damit keine vorherigen Daten überschrieben werden, wird der Dateiname iteriert, bis ein neuer Dateiname gefunden wurde.
        {
            DataSetToXMLWriter writer = new DataSetToXMLWriter();
            writer.SetFileName(inputFileName);//, dataType);

        }
        public void OutputToXML(string processType) //Output to file
        {
            System.Diagnostics.Debug.WriteLine("Prozesstyp in OutpoutToXML: " + processType);
            switch (processType) {
                case "rep":
                    RepToXMLWriter rep = new RepToXMLWriter();
                    rep.OutputToXML((dataSets.Count), dataSets, loadHelper.GetFileNameAndPath());
                    break;
                case "gfs":
                    GFSToXMLWriter gfs = new GFSToXMLWriter();
                    gfs.OutputToXML((dataSets.Count), dataSets, loadHelper.GetFileNameAndPath());
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("Error in OutputToXML from DataSet                   ---DataSet.OutputToXML()");
                    break;
            }
        }
        
    }

}
