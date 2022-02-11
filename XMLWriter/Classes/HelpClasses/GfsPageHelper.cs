using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Linq;
using XMLWriter.Classes;
using System.Collections;
using System.Windows.Documents;

namespace XMLWriter.Classes.HelpClasses {
    internal class GfsPageHelper {
        DataSetService dataSetService = new DataSetService();
        UtilityFunctions utility = new UtilityFunctions();
        Language language = new Language();
        GUIMovementHelper gui = new GUIMovementHelper();
        DropDownOptionLists dropDownList = new DropDownOptionLists();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
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
        public string handleToolChoiceAndResultingPositiveResult(ComboBox toolChoice, ComboBox actuatorTest, ComboBox RDID, ComboBox smartTool, TextBox posRes_RDID, TextBox posRes_SM, string positiveResult) {
            if (toolChoice.Text == dropDownList.GetToolChoice()[1]) //AT
            {
                smartTool.Text = "false";
                RDID.Text = "false";
                positiveResult = "";
            }
            else if (toolChoice.Text == dropDownList.GetToolChoice()[2]) //SmT
            {
                actuatorTest.Text = "false";
                RDID.Text = "false";
                posRes_RDID.Text = "false";
                positiveResult = posRes_SM.Text;
            }
            else if (toolChoice.Text == dropDownList.GetToolChoice()[3]) //RDID
            {
                smartTool.Text = "false";
                actuatorTest.Text = "false";
                posRes_SM.Text = "false";
                positiveResult = posRes_RDID.Text;
            }
            return positiveResult;
        }
        public void SaveCurrentInput(TextBox stepName, TextBox text, TextBox anim, TextBox instruction, ComboBox positiveID, ComboBox negativeID, string positiveResult, TextBox repXML, ComboBox actuatorTest, ComboBox RDID, ComboBox smartTool, CheckBox nextStep, CheckBox lastStep, ComboBox toolChoice) {
            
            utility.WriteStepNameToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), stepName.Text);
            utility.WriteTextToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), text.Text);
            utility.WriteAnimToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), anim.Text);
            utility.WriteInstructionToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), instruction.Text);
            utility.WritePositiveIDToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), positiveID.Text);
            utility.WriteNegativeIDToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), negativeID.Text);
            utility.WritePositiveResultToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), positiveResult);
            utility.WriteRepXMLToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), repXML.Text);
            utility.WriteActuatorTestToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), actuatorTest.Text);
            utility.WriteRDIDToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), RDID.Text);
            utility.WriteSmartToolToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), smartTool.Text);
            utility.WriteToolChoiceToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), toolChoice.Text);
            if (nextStep.IsChecked == true) {
                utility.WriteNextStepToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), true);
            }
            else {
                utility.WriteNextStepToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), false);
            }
            if (lastStep.IsChecked == true) {
                utility.WriteLastStepToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), true);
            }
            else {
                utility.WriteLastStepToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), false);
            }
            utility.WriteSpecialTextToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), "");
            consol.ConsoleShowDataSetOfIndex(dataSetService.GetDataSets().ElementAt(gui.GetIndex()), gui.GetIndex(), "Speichern");
        }
        /// <stuff>
        /// ---///
        /// </stuff>
        public void CheckForWhatCaseInSmartToolPositiveResult(TextBox inputPositiveResult_SM, TextBox inputPositiveResult_UpperLimit, TextBox inputPositiveResult_LowerLimit) {
            if (!string.IsNullOrWhiteSpace(inputPositiveResult_LowerLimit.Text) && string.IsNullOrWhiteSpace(inputPositiveResult_UpperLimit.Text)) {
                inputPositiveResult_SM.Text = inputPositiveResult_LowerLimit.Text + ";lower";
            }
            else if (string.IsNullOrWhiteSpace(inputPositiveResult_LowerLimit.Text) && !string.IsNullOrWhiteSpace(inputPositiveResult_UpperLimit.Text)) {
                inputPositiveResult_SM.Text = inputPositiveResult_UpperLimit.Text + ";upper";
            }
            else if (string.IsNullOrWhiteSpace(inputPositiveResult_LowerLimit.Text) && string.IsNullOrWhiteSpace(inputPositiveResult_UpperLimit.Text)) {
                inputPositiveResult_SM.Text = "";
            }
            else if (!string.IsNullOrWhiteSpace(inputPositiveResult_LowerLimit.Text) && !string.IsNullOrWhiteSpace(inputPositiveResult_UpperLimit.Text)) {
                inputPositiveResult_SM.Text = inputPositiveResult_LowerLimit.Text + "|" + inputPositiveResult_UpperLimit.Text;
            }
            else {
                inputPositiveResult_SM.Text = "";
                System.Diagnostics.Debug.Write("Dieser Pfad in ChechForWhatCaseInSmartToolPositiveResult-Method sollte nie erreicht werden: ");
            }
        }
        public void ShowItemsAfterToolChoice(ComboBox inputToolChoice, DockPanel actuatorTest, DockPanel smartTool, DockPanel RDID) {
            if (inputToolChoice.Text == dropDownList.GetToolChoice()[1]) //AT
            {
                HideAllItemsWithToggleVisibility(actuatorTest, smartTool, RDID);
                actuatorTest.Visibility = Visibility.Visible;
            }
            else if (inputToolChoice.Text == dropDownList.GetToolChoice()[2])  //SmT
            {
                HideAllItemsWithToggleVisibility(actuatorTest, smartTool, RDID);
                smartTool.Visibility = Visibility.Visible;
            }
            else if (inputToolChoice.Text == dropDownList.GetToolChoice()[3]) //RDID
            {
                HideAllItemsWithToggleVisibility(actuatorTest, smartTool, RDID);
                RDID.Visibility = Visibility.Visible;
            }
            else {
                HideAllItemsWithToggleVisibility(actuatorTest, smartTool, RDID);
            }
        }
        private void HideAllItemsWithToggleVisibility(DockPanel actuatorTest, DockPanel smartTool, DockPanel RDID) {
            RDID.Visibility = Visibility.Hidden;
            smartTool.Visibility = Visibility.Hidden;
            actuatorTest.Visibility = Visibility.Hidden;
        }
        
        ///---Inits und Sets---///
        //Label
            //linke Seite
        public void SetLabelStepName(Label step) {
            xamlHelper.SetTextFor(step, language.GetStringStepTitel() + ": " + gui.GetStepCount());
        }
        public void SetLabelContent(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringContent());
        }
        public void SetLabelAnimation(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringAnim());
        }
        public void SetLabelInstruction(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringInstruction());
        }
        public void SetLabelTitel(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringPleaseFill());
        }
        //rechte Seite
        public void SetLabelPositiveID(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringPosID());
        }
        public void SetLabelNegativeID(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringNegID());
        }
        public void SetLabelPositiveResult(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringPosResult());
        }
        public void SetLabelRepXML(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringRepXML());
        }
        public void SetLabelActuatorTesst(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringActuatorTest());
        }
        public void SetLabelRDID(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringReadData());
        }
        public void SetLabelSmartTool(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringSmartTool());
        }
        
        public void SetLabelSmartToolOption(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringOptional());
        }
        //Buttons
        public void SetButtonNext(Button next) {
            xamlHelper.SetTextFor(next, language.GetStringNext());
        }
        public void SetButtonSave(Button save) {
            xamlHelper.SetTextFor(save, language.GetStringSave());
        }
        public void SetButtonDelete(Button delete) {
            xamlHelper.SetTextFor(delete, language.GetStringReset());
        }
        public void SetButtonBack(Button back) {
            xamlHelper.SetTextFor(back, language.GetStringBack());
        }
        //Text Boxes
        public void SetStepNameValue(TextBox stepName) {
            if (string.IsNullOrEmpty(dataSetService.GetDataSets().ElementAt(gui.GetIndex()).stepName)) {
                xamlHelper.SetTextFor(stepName, "Schritt " + gui.GetStepCount());
            }
            else {
                xamlHelper.SetTextFor(stepName, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).stepName);
            }
        }
        public void SetTextValue(TextBox text) {
            xamlHelper.SetTextFor(text, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).text);
        }
        public void SetAnimValue(TextBox anim) {
            if (string.IsNullOrEmpty(dataSetService.GetDataSets().ElementAt(gui.GetIndex()).anim)) {
                xamlHelper.SetTextFor(anim, "Default");
            }
            else {
                xamlHelper.SetTextFor(anim, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).anim);
            }
        }
        public void SetInstructionValue(TextBox instr) {
            xamlHelper.SetTextFor(instr, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).instruction);
        }
        public void SetRepXMLValue(TextBox repXML
            ) {
            xamlHelper.SetTextFor(repXML, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).repXML);
        }
        //CheckBoxes
        public void SetNextStepValue(CheckBox inputNextStep) {
            xamlHelper.SetStateFor(inputNextStep, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).nextStep);
        }
        public void SetLastStepValue(CheckBox inputLastStep) {
            xamlHelper.SetStateFor(inputLastStep, dataSetService.GetDataSets().ElementAt(gui.GetIndex()).lastStep);
        }
        //CheckBoxText
        public void SetTextNextStep(CheckBox checky) {
            xamlHelper.SetTextFor(checky, language.GetStringNextStep());
        }
        public void SetTextLastStep(CheckBox checky) {
            xamlHelper.SetTextFor(checky, language.GetStringLastStep());
        }
        //DropDowns (Die festen, die immer da sind)
        public void SetPositiveID(ComboBox inputPositiveID) {
            xamlHelper.SetDropDownContent(inputPositiveID, dataSetService.GetStepNames(), dataSetService.GetDataSets().ElementAt(gui.GetIndex()).positiveID);
        }
        public void SetNegativeID(ComboBox inputPositiveID) {
            xamlHelper.SetDropDownContent(inputPositiveID, dataSetService.GetStepNames(), dataSetService.GetDataSets().ElementAt(gui.GetIndex()).negativeID);
        }
    }
}
