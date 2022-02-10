using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLWriter.Classes
{
    internal class UtilityFunctions
    {
        Language language = new Language();

        public bool ArrayContainsElement(string[] list, string element)
        {
            foreach (var listElement in list)
            {
                if (listElement == element)
                {
                    return true;
                }
            }
            return false;
        }
        public void WriteRepToCurrentDataSet(List<DataSet> dataSets, int index, string[] input) {
            WriteStepNameToCurrentDataSet(dataSets, index, input[0]);
            WriteTextToCurrentDataSet(dataSets, index, input[1]);
            WriteAnimToCurrentDataSet(dataSets, index, input[2]);
            WriteSpecialTextToCurrentDataSet(dataSets, index, input[3]);
            WriteInstructionToCurrentDataSet(dataSets, index, "");
            WritePositiveIDToCurrentDataSet(dataSets, index, "");
            WriteNegativeIDToCurrentDataSet(dataSets, index, "");
            WritePositiveResultToCurrentDataSet(dataSets, index, "");
            WriteRepXMLToCurrentDataSet(dataSets, index, "");
            WriteActuatorTestToCurrentDataSet(dataSets, index, "");
            WriteRDIDToCurrentDataSet(dataSets, index, "");
            WriteSmartToolToCurrentDataSet(dataSets, index, "");
            WriteNextStepToCurrentDataSet(dataSets, index, "");
            WriteLastStepToCurrentDataSet(dataSets, index, "");
            WriteToolChoiceToCurrentDataSet(dataSets, index, "");
        }
        public void WriteGfsToCurrentDataSet(List<DataSet> dataSets, int index, string[] input) {
            WriteStepNameToCurrentDataSet(dataSets, index, input[0]);
            WriteTextToCurrentDataSet(dataSets, index, input[1]);
            WriteAnimToCurrentDataSet(dataSets, index, input[2]);
            WriteSpecialTextToCurrentDataSet(dataSets, index, input[3]);
            WriteInstructionToCurrentDataSet(dataSets, index, input);
            WritePositiveIDToCurrentDataSet(dataSets, index, input);
            WriteNegativeIDToCurrentDataSet(dataSets, index, input);
            WritePositiveResultToCurrentDataSet(dataSets, index, input);
            WriteRepXMLToCurrentDataSet(dataSets, index, input);
            WriteActuatorTestToCurrentDataSet(dataSets, index, input);
            WriteRDIDToCurrentDataSet(dataSets, index, input);
            WriteSmartToolToCurrentDataSet(dataSets, index, input);
            WriteNextStepToCurrentDataSet(dataSets, index, input);
            WriteLastStepToCurrentDataSet(dataSets, index, input);
            WriteToolChoiceToCurrentDataSet(dataSets, index, input);
        }
        private void WriteStepNameToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (input != "") {
                dataSets.ElementAt(index).stepName = input;
            }
            else {
                dataSets.ElementAt(index).stepName = language.GetStringSteps() + " " + index;
            }
        }
        private void WriteTextToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            if (input != "") {
                dataSets.ElementAt(index).stepName = input;
            }
            else {
                dataSets.ElementAt(index).stepName = "false";
            }
            dataSets.ElementAt(index).text = input;
        }
        private void WriteAnimToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).anim = input;
        }
        private void WriteSpecialTextToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).specialText = input;
        }
        private void WriteInstructionToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).instruction = input;
        }
        private void WritePositiveIDToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).positiveID = input;
        }
        private void WriteNegativeIDToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).negativeID = input;
        }
        private void WritePositiveResultToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).positiveResult = input;
        }
        private void WriteRepXMLToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).repXML = input;
        }
        private void WriteActuatorTestToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).actuatorTest = input;
        }
        private void WriteRDIDToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).RDID = input;
        }
        private void WriteSmartToolToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).smartTool = input;
        }
        private void WriteNextStepToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).nextStep = input;
        }
        private void WriteLastStepToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).lastStep = input;
    
        }
        private void WriteToolChoiceToCurrentDataSet(List<DataSet> dataSets, int index, string input) {
            dataSets.ElementAt(index).toolChoice = input;

        }
    }
}
