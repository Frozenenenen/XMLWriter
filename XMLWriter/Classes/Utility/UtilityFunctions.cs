using System.Collections.Generic;
using System.Linq;

namespace XMLWriter.Classes {
    internal class UtilityFunctions {

        public bool ArrayContainsElement(string[] list, string element) {
            foreach (var listElement in list) {
                if (listElement == element) {
                    return true;
                }
            }
            return false;
        }

        public void WriteStepNameToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).stepName = input;
            System.Diagnostics.Debug.WriteLine(input);
            System.Diagnostics.Debug.WriteLine(dataSets.ElementAt(index).stepName);
        }
        public void WriteTextToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (string.IsNullOrEmpty(input)) {
                dataSets.ElementAt(index).text = "";
            }
            else {
                dataSets.ElementAt(index).text = input;
            }
        }
        public void WriteAnimToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (string.IsNullOrEmpty(input)) {
                dataSets.ElementAt(index).anim = "default";
            }
            else {
                dataSets.ElementAt(index).anim = input;
            }
        }
        public void WriteSpecialTextToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (string.IsNullOrEmpty(input)) {
                dataSets.ElementAt(index).specialText = "false";
            }
            else {
                dataSets.ElementAt(index).specialText = input;
            }
        }
        public void WriteInstructionToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (string.IsNullOrEmpty(input)) {
                dataSets.ElementAt(index).instruction = "false";
            }
            else {
                dataSets.ElementAt(index).instruction = input;
            }
        }
        public void WritePositiveIDToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (string.IsNullOrEmpty(input)) {
                dataSets.ElementAt(index).positiveID = "false";
            }
            else {
                dataSets.ElementAt(index).positiveID = input;
            }
        }
        public void WriteNegativeIDToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (string.IsNullOrEmpty(input)) {
                dataSets.ElementAt(index).negativeID = "false";
            }
            else {
                dataSets.ElementAt(index).negativeID = input;
            }
        }
        public void WritePositiveResultToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (string.IsNullOrEmpty(input)) {
                dataSets.ElementAt(index).positiveResult = "false";
            }
            else {
                dataSets.ElementAt(index).positiveResult = input;
            }
        }
        public void WriteRepXMLToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (string.IsNullOrEmpty(input)) {
                dataSets.ElementAt(index).repXML = "false";
            }
            else {
                dataSets.ElementAt(index).repXML = input;
            }
        }
        public void WriteActuatorTestToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (string.IsNullOrEmpty(input)) {
                dataSets.ElementAt(index).actuatorTest = "false";
            }
            else {
                dataSets.ElementAt(index).actuatorTest = input;
            }
        }
        public void WriteRDIDToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (string.IsNullOrEmpty(input)) {
                dataSets.ElementAt(index).RDID = "false"; 
            }
            else {
                dataSets.ElementAt(index).RDID = input;
            }
        }
        public void WriteSmartToolToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (string.IsNullOrEmpty(input)) {
                dataSets.ElementAt(index).smartTool = "false";
            }
            else {
                dataSets.ElementAt(index).smartTool = input;
            }
        }
        public void WriteNextStepToCurrentDataSet(List<DataSet> dataSets, int index, bool input) {
            dataSets.ElementAt(index).nextStep = input;
        }
        public void WriteLastStepToCurrentDataSet(List<DataSet> dataSets, int index, bool input) {
            dataSets.ElementAt(index).lastStep = input;
        }
        public void WriteToolChoiceToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (string.IsNullOrEmpty(input)) {
                dataSets.ElementAt(index).toolChoice = "false";
            }
            else {
                dataSets.ElementAt(index).toolChoice = input;
            }
        }
    }
}
