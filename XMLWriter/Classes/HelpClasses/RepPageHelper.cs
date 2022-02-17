using System.Linq;
using System.Windows.Controls;

namespace XMLWriter.Classes.HelpClasses {
    internal class RepPageHelper {
        DataSetService dataSetService = new DataSetService();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        GUIMovementHelper gui = new GUIMovementHelper();
        UtilityFunctions utility = new UtilityFunctions();
        Language language = new Language();

        /// --- Navigation --- ///
        public void PrepareNextPage() {
            dataSetService.InitNewDataSetWhereRequired();
            gui.IncrementSteps();            
        }
        public void PreparePreviousPage() {
            gui.DecrementSteps();
        }
        public void DeleteCurrentSet() {
            dataSetService.DeleteDataSet();
            gui.DecrementStepsMax();        //Unfertig
        }
        public void InsertNewSet() {
            dataSetService.InsertNewDataSet();
        }

        /// ---- WriteToDataSet --- ///
        public void SaveCurrentInput(TextBox stepName, TextBox text, TextBox specialText, TextBox anim) {
            System.Diagnostics.Debug.WriteLine(stepName.Text + ", " + text + ", " + specialText + ", " + anim);
            WriteInputToDataSet(new string[] { stepName.Text, text.Text, specialText.Text, anim.Text });
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
                xamlHelper.SetTextFor(textBox, language.GetStringStep() + ": " + (gui.GetStepCount()));
            }
            else {
                xamlHelper.SetTextFor(textBox, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).stepName);
            }
        }
        public void SetBoxTextValue(TextBox textBox) {
            xamlHelper.SetTextFor(textBox, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).text);
        }
        public void SetBoxAnimValue(TextBox textBox) {
            if (string.IsNullOrEmpty(dataSetService.GetDataSets().ElementAt(gui.GetIndex()).anim)){
                xamlHelper.SetTextFor(textBox, "default");
            }
            else {
                xamlHelper.SetTextFor(textBox, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).anim);
            }
        }
        public void SetBoxSpecialText(TextBox textBox) {
            xamlHelper.SetTextFor(textBox, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).specialText);
        }
        //Init Labels (Text Items)
        public void SetLabelStepText(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringStepTitel() + ": " + gui.GetStepCount());
        }
        public void SetLabelContentTitelText(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringContent());
        }
        public void SetLabelAnimText(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringAnim());
        }
        public void SetLabelSpecialText(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringSpecialStep());
        }
        public void SetLabelTitelText(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringPleaseFill());
        }
        //Init Buttons
        public void SetButtonNextText(Button button) {
            xamlHelper.SetTextFor(button, language.GetStringNext());
        }
        public void SetButtonBackText(Button button) {
            xamlHelper.SetTextFor(button, language.GetStringBack());
        }
        public void SetButtonDeleteText(Button button) {
            xamlHelper.SetTextFor(button, language.GetStringReset());
        }
        public void SetButtonSaveText(Button button) {
            xamlHelper.SetTextFor(button, language.GetStringSave());
        }
        public void SetButtonInsertText(Button button) {
            xamlHelper.SetTextFor(button, language.GetStringInsert());
        }
    }
}
