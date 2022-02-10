using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using XMLWriter.Pages;

namespace XMLWriter.Classes.HelpClasses {
    internal class RepPageHelper {
        DataSetService dataSetService = new DataSetService();
        XAMLHelperFunctions xaml = new XAMLHelperFunctions();
        GUIMovementHelper gui = new GUIMovementHelper();
        UtilityFunctions utility = new UtilityFunctions();
        DataSet dataSet;
        Language language = new Language();
        ConsoleControl consol = new ConsoleControl();

        

        /// --- Navigation --- ///
        public void PrepareNextPage() {
            dataSetService.InitNewDataSetWhereRequired();
            gui.IncrementSteps();            
        }
        public void PreparePreviousPage() {
            gui.DecrementSteps();
        }
        public void DeleteCurrentSet() {
            gui.DecrementStepsMax();        //Unfertig
        }

        /// ---- WriteToDataSet --- ///
        public void SaveCurrentInput(TextBox stepName, TextBox text, TextBox specialText, TextBox anim) {
            WriteInputToDataSet(new string[] { stepName.Text, text.Text, specialText.Text, anim.Text });
            consol.ConsoleShowDataSetOfIndex(dataSetService.GetDataSets().ElementAt(gui.GetIndex()), gui.GetIndex(), "Speichern");
        }
        private void WriteInputToDataSet(string[] input) {
            utility.WriteStepNameToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), input[0]);
            utility.WriteTextToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), input[1]);
            utility.WriteAnimToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), input[2]);
            utility.WriteSpecialTextToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), input[3]);
            utility.WriteInstructionToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), "");
            utility.WritePositiveIDToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), "");
            utility.WriteNegativeIDToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), "");
            utility.WritePositiveResultToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), "");
            utility.WriteRepXMLToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), "");
            utility.WriteActuatorTestToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), "");
            utility.WriteRDIDToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), "");
            utility.WriteSmartToolToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), "");
            utility.WriteNextStepToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), false);
            utility.WriteLastStepToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), false);
            utility.WriteToolChoiceToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), "");
        }

        ///---Inits---///
        //Init TextBoxes (Value Items)
        public void SetBoxStepNameValue(TextBox textBox) {
            if (string.IsNullOrEmpty(dataSetService.GetDataSets().ElementAt(gui.GetIndex()).stepName)) {
                xaml.SetTextFor(textBox, language.GetStringStep() + ": " + (gui.GetStepCount()));
            }
            else {
                xaml.SetTextFor(textBox, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).stepName);
            }
        }
        public void SetBoxTextValue(TextBox textBox) {
            xaml.SetTextFor(textBox, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).text);
        }
        public void SetBoxAnimValue(TextBox textBox) {
            if (string.IsNullOrEmpty(dataSetService.GetDataSets().ElementAt(gui.GetIndex()).anim)){
                xaml.SetTextFor(textBox, "default");
            }
            else {
                xaml.SetTextFor(textBox, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).anim);
            }
        }
        public void SetBoxSpecialText(TextBox textBox) {
            xaml.SetTextFor(textBox, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).specialText);
        }
        //Init Labels (Text Items)
        public void SetLabelStepText(Label label) {
            xaml.SetTextFor(label, language.GetStringStepTitel() + ": " + (gui.GetStepCount())
        }
        public void SetLabelContentTitelText(Label label) {
            xaml.SetTextFor(label, language.GetStringContent());
        }
        public void SetLabelAnimText(Label label) {
            xaml.SetTextFor(label, language.GetStringAnim());
        }
        public void SetLabelSpecialText(Label label) {
            xaml.SetTextFor(label, language.GetStringSpecialStep());
        }
        public void SetLabelTitelText(Label label) {
            xaml.SetTextFor(label, language.GetStringPleaseFill());
        }
        //Init Buttons
        public void SetButtonNextText(Button button) {
            xaml.SetTextFor(button, language.GetStringNext());
        }
        public void SetButtonBackText(Button button) {
            xaml.SetTextFor(button, language.GetStringBack());
        }
        public void SetButtonDeleteText(Button button) {
            xaml.SetTextFor(button, language.GetStringReset());
        }
        public void SetButtonSaveText(Button button) {
            xaml.SetTextFor(button, language.GetStringSave());
        }
    }
}
