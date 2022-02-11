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
        GUIMovementHelper guiHelper = new GUIMovementHelper();
        DropDownOptionLists dropDownList = new DropDownOptionLists();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        ConsoleControl consol = new ConsoleControl();

        /// --- Navigation --- ///
        public void PrepareNextPage() {
            dataSetService.InitNewDataSetWhereRequired();
            guiHelper.IncrementSteps();
        }
        public void PreparePreviousPage() {
            guiHelper.DecrementSteps();
        }
        public void DeleteCurrentSet() {
            guiHelper.DecrementStepsMax();        //Unfertig
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

            utility.WriteStepNameToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), stepName.Text);
            utility.WriteTextToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), text.Text);
            utility.WriteAnimToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), anim.Text);
            utility.WriteInstructionToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), instruction.Text);
            utility.WritePositiveIDToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), positiveID.Text);
            utility.WriteNegativeIDToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), negativeID.Text);
            utility.WritePositiveResultToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), positiveResult);
            utility.WriteRepXMLToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), repXML.Text);
            utility.WriteActuatorTestToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), actuatorTest.Text);
            utility.WriteRDIDToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), RDID.Text);
            utility.WriteSmartToolToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), smartTool.Text);
            utility.WriteToolChoiceToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), toolChoice.Text);
            if (nextStep.IsChecked == true) {
                utility.WriteNextStepToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), true);
            }
            else {
                utility.WriteNextStepToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), false);
            }
            if (lastStep.IsChecked == true) {
                utility.WriteLastStepToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), true);
            }
            else {
                utility.WriteLastStepToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), false);
            }
            utility.WriteSpecialTextToCurrentDataSet(dataSetService.GetDataSets(), guiHelper.GetIndex(), "");
            consol.ConsoleShowDataSetOfIndex(dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()), guiHelper.GetIndex(), "Speichern");
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
            xamlHelper.SetTextFor(step, language.GetStringStepTitel() + ": " + guiHelper.GetStepCount());
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
            if (string.IsNullOrEmpty(dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).stepName)) {
                xamlHelper.SetTextFor(stepName, "Schritt " + guiHelper.GetStepCount());
            }
            else {
                xamlHelper.SetTextFor(stepName, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).stepName);
            }
        }
        public void SetTextValue(TextBox text) {
            xamlHelper.SetTextFor(text, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).text);
        }
        public void SetAnimValue(TextBox anim) {
            if (string.IsNullOrEmpty(dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).anim)) {
                xamlHelper.SetTextFor(anim, "Default");
            }
            else {
                xamlHelper.SetTextFor(anim, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).anim);
            }
        }
        public void SetInstructionValue(TextBox instr) {
            xamlHelper.SetTextFor(instr, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).instruction);
        }
        public void SetRepXMLValue(TextBox repXML) {
            xamlHelper.SetTextFor(repXML, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).repXML);
        }
        public void SetSmartToolCombinedValue(TextBox smartTool, ComboBox inputSmartTool_SM, ComboBox inputMeasure_SM) {
            xamlHelper.SetTextFor(smartTool, dropDownList.GetKeyPartOf(dropDownList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + inputMeasure_SM.Text);
        }
        public void SetSmartToolValue(TextBox SmartTool_TextBox) {
            xamlHelper.SetTextFor(SmartTool_TextBox, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool);
        }
        //CheckBoxes
        public void SetNextStepValue(CheckBox inputNextStep) {
            xamlHelper.SetStateFor(inputNextStep, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).nextStep);
        }
        public void SetLastStepValue(CheckBox inputLastStep) {
            xamlHelper.SetStateFor(inputLastStep, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).lastStep);
        }
        //CheckBoxText
        public void SetTextNextStep(CheckBox checky) {
            xamlHelper.SetTextFor(checky, language.GetStringNextStep());
        }
        public void SetTextLastStep(CheckBox checky) {
            xamlHelper.SetTextFor(checky, language.GetStringLastStep());
        }
        //DropDowns
        public void SetPositiveID(ComboBox inputPositiveID) {
            xamlHelper.SetDropDownContent(inputPositiveID, dataSetService.GetStepNames(), dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveID);
        }
        public void SetNegativeID(ComboBox inputPositiveID) {
            xamlHelper.SetDropDownContent(inputPositiveID, dataSetService.GetStepNames(), dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).negativeID);
        }
        ///ActuatorTest Init
        public void InitActuatorTestDropdowns(ComboBox inputECUChoice_AT, ComboBox inputToolChoice, ComboBox inputComponentChoice_AT, TextBox inputActuatorTest) {
            xamlHelper.SetDropDownListFor(inputECUChoice_AT, dropDownList.GetECUChoices().Select(x => x.secondPart).ToArray());
            if (inputToolChoice.Text == dropDownList.GetToolChoice()[1]) {
                string[] positiveResultDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest.Split('|');
                xamlHelper.SetDropDownActiveELementFor(inputECUChoice_AT, dropDownList.GetDisplayPartOf(dropDownList.GetECUChoices(), positiveResultDupel[1]));
                xamlHelper.SetDropDownContent(inputComponentChoice_AT,
                    dropDownList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray(),
                    positiveResultDupel[0]);
            }
            else {
                xamlHelper.SetDropDownActiveELementFor(inputECUChoice_AT, dropDownList.GetECUChoices().ElementAt(0).secondPart);
                xamlHelper.SetDropDownContent(inputComponentChoice_AT,
                    dropDownList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray(),
                    dropDownList.GetIOChoices(inputECUChoice_AT.Text).ElementAt(0).secondPart);
            }
            FillInputActuatorTestText(inputECUChoice_AT, inputToolChoice, inputComponentChoice_AT, inputActuatorTest);

        }
        ///RDID Init
        public void InitReadDataDropdowns(ComboBox inputToolChoice, ComboBox ECUChoice_RDBI_ComboBox, ComboBox RDBIChoice_RDBI_ComboBox, TextBox inputPositiveResult_RDBI, TextBox ReadData_TextBox) {
        ECUChoice_RDBI_ComboBox.ItemsSource = dropDownList.GetECUChoices().Select(x => x.secondPart).ToArray();

            if (inputToolChoice.Text == dropDownList.GetToolChoice()[3]) {
                string[] positiveResultDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID.Split('|');
    ECUChoice_RDBI_ComboBox.Text = dropDownList.GetDisplayPartOf(dropDownList.GetECUChoices(), positiveResultDupel[1]);
                RDBIChoice_RDBI_ComboBox.ItemsSource = dropDownList.GetRDIDChoices(ECUChoice_RDBI_ComboBox.Text).Select(x => x.secondPart).ToArray();
    RDBIChoice_RDBI_ComboBox.Text = dropDownList.GetDisplayPartOf(dropDownList.GetRDIDChoices(ECUChoice_RDBI_ComboBox.Text), positiveResultDupel[0]);
                inputPositiveResult_RDBI.Text = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult;
                ReadData_TextBox.Text = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID;
            }
            else  //Wenn nicht vorhanden, dann zeig das erste Element an.
            {
    ECUChoice_RDBI_ComboBox.Text = dropDownList.GetECUChoices().ElementAt(0).secondPart;
    RDBIChoice_RDBI_ComboBox.ItemsSource = dropDownList.GetRDIDChoices(ECUChoice_RDBI_ComboBox.Text).Select(x => x.secondPart).ToArray();
    RDBIChoice_RDBI_ComboBox.Text = dropDownList.GetRDIDChoices(ECUChoice_RDBI_ComboBox.Text).ElementAt(0).secondPart;
}
        }
        ///SmartTool Init
        public void InitSmartToolDropdowns(ComboBox SmartTool_ComboBox, ComboBox inputToolChoice, TextBox SmartTool_TextBox, ComboBox Measurement_ComboBox, TextBox PositiveResult_SM_TextBox, TextBox PositiveResult_LowerLimit_TextBox, TextBox PositiveResult_UpperLimit_TextBox) {
            InitSmartToolDropDownDefaultValue(SmartTool_ComboBox);

            if (IsSmartTool(inputToolChoice)) {
                SetSmartToolValue(SmartTool_TextBox);
                string[] positiveResultDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool.Split('|');
                ChangeSmartToolActiveElementTo(SmartTool_ComboBox, dropDownList.GetDisplayPartOf(dropDownList.GetSmartToolChoices(), positiveResultDupel[0]));
                SetMeasurementChoices(Measurement_ComboBox,
                    dropDownList.GetMeasurementChoices(SmartTool_ComboBox.Text).Select(x => x.secondPart).ToArray(),
                    dropDownList.GetDisplayPartOf(dropDownList.GetMeasurementChoices(SmartTool_ComboBox.Text), positiveResultDupel[1]));
            }
            else //Wenn nicht vorhanden, dann zeig das erste Element an.
            {
                SetMeasurementChoices(Measurement_ComboBox,
                    dropDownList.GetMeasurementChoices(SmartTool_ComboBox.Text).Select(x => x.secondPart).ToArray(),
                    dropDownList.GetMeasurementChoices(SmartTool_ComboBox.Text).ElementAt(0).secondPart);
            }
            FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_ComboBox, Measurement_ComboBox);

            if (IsSmartTool(dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).toolChoice)) {
                InitSmartToolLimits(PositiveResult_SM_TextBox, PositiveResult_LowerLimit_TextBox, PositiveResult_UpperLimit_TextBox);
            }
        }
        private void SetMeasurementChoices(ComboBox comboBox, string[] list, string text) {
            xamlHelper.SetDropDownContent(comboBox, list, text);
        }
        private void InitSmartToolDropDownDefaultValue(ComboBox smartTool_DD) {
            xamlHelper.SetDropDownContent(smartTool_DD,
                dropDownList.GetSmartToolChoices().Select(x => x.secondPart).ToArray(),
                dropDownList.GetSmartToolChoices().ElementAt(0).secondPart);
        }
        private void ChangeSmartToolActiveElementTo(ComboBox SmartTool_DD, string text) {
            xamlHelper.SetDropDownActiveELementFor(SmartTool_DD, text);
        }





        

        /// <summary>
        /// Others
        /// </summary>
        /// 
        public void FillInputActuatorTestText(ComboBox inputECUChoice_AT, ComboBox inputToolChoice, ComboBox inputComponentChoice_AT, TextBox inputActuatorTest) {
            inputActuatorTest.Text = dropDownList.GetKeyPartOf(dropDownList.GetIOChoices(inputECUChoice_AT.Text), inputComponentChoice_AT.Text) + "|" + dropDownList.GetKeyPartOf(dropDownList.GetECUChoices(), inputECUChoice_AT.Text);
        }
        public void UpdateSmartToolComboboBox(ComboBox SmartTool_SM_ComboBox, ComboBox Measure_SM_ComboBox) {
            xamlHelper.SetDropDownListFor(SmartTool_SM_ComboBox, dropDownList.GetSmartToolChoices().Select(x => x.secondPart).ToArray());
            xamlHelper.SetDropDownContent(Measure_SM_ComboBox,
                dropDownList.GetMeasurementChoices(SmartTool_SM_ComboBox.Text).Select(x => x.secondPart).ToArray(),
                dropDownList.GetMeasurementChoices(SmartTool_SM_ComboBox.Text).Select(x => x.secondPart).ToArray()[0]);
        }
        public void CheckForWhatToolHasBeenChosen(ComboBox inputToolChoice) {
            if (consol.showMiscGfs) {
                System.Diagnostics.Debug.WriteLine("AT: " + dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest + "                                                      GfsPage(cs).CheckForWhatToolHasBeenChosen()");
                System.Diagnostics.Debug.WriteLine("SmT: " + dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool);
                System.Diagnostics.Debug.WriteLine("RDID: " + dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID);
                System.Diagnostics.Debug.WriteLine("PosRes: " + dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult + "\n");
            }

            if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest != "" && dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest != "false") {
                inputToolChoice.Text = dropDownList.GetToolChoice()[1];
            }
            else if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool != "" && dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool != "false") {
                inputToolChoice.Text = dropDownList.GetToolChoice()[2];
            }
            else if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID != "" && dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID != "false") {
                inputToolChoice.Text = dropDownList.GetToolChoice()[3];
            }
            else {
                inputToolChoice.Text = language.GetStringPleaseChoose();
            }
        }
        public void FillInputSmartToolCombinedText(TextBox inputSmartTool, ComboBox inputSmartTool_SM, ComboBox inputMeasure_SM) {
            xamlHelper.SetTextFor(inputSmartTool, dropDownList.GetKeyPartOf(dropDownList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + dropDownList.GetKeyPartOf(dropDownList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text));
        }
        public void FillInputReadDataCombinedText(TextBox ReadData_ComboBox, ComboBox ECUChoice_RDBI_ComboBox, ComboBox RDBIChoice_RDBI_ComboBox) {
            xamlHelper.SetTextFor(ReadData_ComboBox, dropDownList.GetKeyPartOf(dropDownList.GetRDIDChoices(ECUChoice_RDBI_ComboBox.Text), RDBIChoice_RDBI_ComboBox.Text) + "|" + dropDownList.GetKeyPartOf(dropDownList.GetECUChoices(), ECUChoice_RDBI_ComboBox.Text));
        }
        public void InitSmartToolLimits(TextBox posRes_SM, TextBox posRes_Lower, TextBox posRes_Upper) {
            if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult == "" || dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult == "false") {
                CheckForWhatCaseInSmartToolPositiveResult(posRes_SM, posRes_Upper, posRes_Lower);
            }
            else {
                if (consol.showMiscGfs) System.Diagnostics.Debug.WriteLine("SmT - PosRes: >" + dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult + "<                          ---InitSmartToolLimits()");
                posRes_SM.Text = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult;
                if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult.Contains('|')) {
                    string[] PosResDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult.Split('|');
                    posRes_Upper.Text = PosResDupel[1];
                    posRes_Lower.Text = PosResDupel[0];
                }
                else if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult.Contains(';')) {
                    string[] PosResDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult.Split(';');
                    if (PosResDupel[1] == "lower") {
                        posRes_Lower.Text = PosResDupel[0];
                    }
                    else if (PosResDupel[1] == "upper") {
                        posRes_Upper.Text = PosResDupel[0];
                    }
                }
            }
        }
        ///Checker
        ///
        public bool IsSmartTool(ComboBox comboBox) {
            if (xamlHelper.IsActiveElementOf(comboBox, dropDownList.GetToolChoice()[2])) {
                return true;
            }
            return false;
        }
        public bool IsSmartTool(string text) {
            if (text == dropDownList.GetToolChoice()[2]) {
                return true;
            }
            return false;
        }

    }
}

