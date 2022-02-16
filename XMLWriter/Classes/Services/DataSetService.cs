﻿using System.Collections.Generic;
using System.Linq;

namespace XMLWriter.Classes {


    internal class DataSetService {
        GUIMovementHelper gui = new GUIMovementHelper();
        ConsoleControl consol = new ConsoleControl();
        private static List<DataSet> dataSets = new List<DataSet>();

        private static string dataType = "rep";


        //Getter
        public List<DataSet> GetDataSets() => dataSets;
        public string GetDataType() => dataType;  // active rep or gfs

        public string[] GetStepNames() {
            string[] stepNames = new string[gui.GetStepCountMax()]; //foreach wäre eleganter
            for (int i = 0; i < gui.GetStepCountMax(); i++) {
                stepNames[i] = dataSets.ElementAt(i).stepName;
            }
            return stepNames;
        } //This is the list of all entries of StepName used in a Set to use for the dropdown/textfields of postiveID and negativeID
        //DataSet data


        //Setter 
        public void SetDataSet(DataSet _dataSet) //Zu löschen.
        {
            dataSets.Insert(gui.GetIndex(), _dataSet);
        }
        public void SetDataType(string _inputDataType) => dataType = _inputDataType;

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
            if (gui.GetIndex() == gui.GetIndexMax()) {
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
                    rep.OutputToXML(gui.GetIndexMax(), dataSets, fileName);
                    break;
                case "gfs":
                    GFSToXMLWriter gfs = new GFSToXMLWriter();
                    gfs.OutputToXML(gui.GetIndexMax(), dataSets, fileName);
                    break;
                default:
                    if (consol.showErrors) System.Diagnostics.Debug.WriteLine("Error in OutputToXML from DataSet                   ---DataSet.OutputToXML()");
                    break;
            }
        }

    }

}
