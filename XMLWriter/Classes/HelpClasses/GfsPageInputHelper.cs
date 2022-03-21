using System.Windows.Controls;
using System.Linq;


namespace XMLWriter.Classes.HelpClasses {
    internal class GfsPageInputHelper {
        DataSetService dataSetService = new DataSetService();
        GUIMovementHelper guiHelper = new GUIMovementHelper();
        DropDownOptionLists dropDownList = new DropDownOptionLists();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();

        ///---Inits---///
        ///---Inits---///
        ///---Inits---///

        //Text Boxes
        public void InitStepNameValue(TextBox stepName) {
            if (string.IsNullOrEmpty(dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).stepName)) {
                xamlHelper.SetTextFor(stepName, "Schritt " + guiHelper.GetStepCount());
            }
            else {
                xamlHelper.SetTextFor(stepName, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).stepName);
            }
        }
        public void InitTextValue(TextBox text) {
            xamlHelper.SetTextFor(text, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).text);
        }
        public void InitAnimValue(TextBox anim) {
            if (string.IsNullOrEmpty(dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).anim)) {
                xamlHelper.SetTextFor(anim, "Default");
            }
            else {
                xamlHelper.SetTextFor(anim, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).anim);
            }
        }
        public void InitInstructionValue(TextBox instr) {
            xamlHelper.SetTextFor(instr, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).instruction);
        }
        public void InitRepXMLValue(TextBox repXML) {
            xamlHelper.SetTextFor(repXML, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).repXML);
        }
        public void InitSmartToolValue(TextBox SmartTool_TextBox) {
            xamlHelper.SetTextFor(SmartTool_TextBox, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool);
        }
        public void InitPositiveResult(TextBox posRes) {
            xamlHelper.SetTextFor(posRes, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult);
        }
        public void InitReadDataValue(TextBox readData) {
            xamlHelper.SetTextFor(readData, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID);
        }
        //CheckBoxes
        public void InitNextStepValue(CheckBox inputNextStep) {
            xamlHelper.SetStateFor(inputNextStep, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).nextStep);
        }
        public void InitLastStepValue(CheckBox inputLastStep) {
            xamlHelper.SetStateFor(inputLastStep, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).lastStep);
        }
        //DropDowns
        public void InitPositiveID_DD(ComboBox inputPositiveID) {
            xamlHelper.SetDropDownContent(inputPositiveID, dataSetService.GetStepNames(), dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveID);
        }
        public void InitNegativeID_DD(ComboBox inputPositiveID) {
            xamlHelper.SetDropDownContent(inputPositiveID, dataSetService.GetStepNames(), dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).negativeID);
        }
        public void InitToolChoiceDropDown(ComboBox toolChoice) {
            xamlHelper.SetDropDownContent(toolChoice, dropDownList.GetToolChoice(), dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).toolChoice);
        }
        public void InitECUChoiceDropDownDefaultValue(ComboBox ECUChoice_ComboBox) {
            xamlHelper.SetDropDownContent(ECUChoice_ComboBox,
                dropDownList.GetECUChoices().Select(x => x.secondPart).ToArray(),
                dropDownList.GetECUChoices().ElementAt(0).secondPart);
        }
        public void InitSmartToolDropDownDefaultValue(ComboBox smartTool_DD) {
            xamlHelper.SetDropDownContent(smartTool_DD,
                dropDownList.GetSmartToolChoices().Select(x => x.secondPart).ToArray(),
                dropDownList.GetSmartToolChoices().ElementAt(0).secondPart);
        }//fertig

        ///---Sets---/// (real time changes)
        //activeElements
        public void SetToolChoiceActiveElementToActuatorTest(ComboBox ToolChoice_ComboBox) {
            xamlHelper.SetDropDownActiveELementFor(ToolChoice_ComboBox, dropDownList.GetToolChoice()[1]);
        }
        public void SetToolChoiceActiveElementToSmartTool(ComboBox ToolChoice_ComboBox) {
            xamlHelper.SetDropDownActiveELementFor(ToolChoice_ComboBox, dropDownList.GetToolChoice()[2]);
        }
        public void SetToolChoiceActiveElementToRDID(ComboBox ToolChoice_ComboBox) {
            xamlHelper.SetDropDownActiveELementFor(ToolChoice_ComboBox, dropDownList.GetToolChoice()[3]);
        }
        public void ChangeSmartToolActiveElementTo(ComboBox SmartTool, string text) {
            xamlHelper.SetDropDownActiveELementFor(SmartTool, text);
        }//fertig
        public void ChangeECUActiveElementTo(ComboBox ECU_Choice, string text) {
            xamlHelper.SetDropDownActiveELementFor(ECU_Choice, text);
        }




        //---
        public void SetMeasurementChoices(ComboBox comboBox, string[] list, string text) {
            xamlHelper.SetDropDownContent(comboBox, list, text);
        }//fertig
        public void SetIOChoices(ComboBox comboBox, string[] list, string text) {
            xamlHelper.SetDropDownContent(comboBox, list, text);
        }
        public void SetRDIDChoices(ComboBox comboBox, string[] list, string text) {
            xamlHelper.SetDropDownContent(comboBox, list, text);
        }
        public void WritePositiveResultDependingOnLowerAndUpperLimit(TextBox inputPositiveResult_SM, TextBox inputPositiveResult_UpperLimit, TextBox inputPositiveResult_LowerLimit) {
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



        //Sets with dependencies - Filling the TextBoxes depending on DropDownChoices
        //Sollte es Probleme beim laden von dem deaktivierten zusammengesetzten Textfeld geben, wenn nur eine Seite ausgewählt und abgespeichert wurde, braucht es hier ggf noch ein paar ABfangversuche à la: Wenn leer, dann " " - das wiederrum würde beim laden von files vermutlich wieder zu Problemen führen, die wiederrum abgefangen werden müssten
        public void FillInputReadDataCombinedText(TextBox inputReadData_TextBox, ComboBox ECUChoice_RDID_ComboBox, ComboBox RDIDChoice_RDID_ComboBox) {
            string shortECUName = dropDownList.GetKeyPartOf(dropDownList.GetRDIDChoices(ECUChoice_RDID_ComboBox.Text), RDIDChoice_RDID_ComboBox.Text);
            string englishRDIDName = dropDownList.GetKeyPartOf(dropDownList.GetECUChoices(), ECUChoice_RDID_ComboBox.Text);
            xamlHelper.SetTextFor(inputReadData_TextBox, shortECUName + "|" + englishRDIDName);
        } //fertig
        public void FillInputSmartToolCombinedText(TextBox inputSmartTool_TextBox, ComboBox inputSmartTool_SM, ComboBox inputMeasure_SM) {
            string englishSmartToolChoice = dropDownList.GetKeyPartOf(dropDownList.GetSmartToolChoices(), inputSmartTool_SM.Text);
            string englishMeasurementChoice = dropDownList.GetKeyPartOf(dropDownList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text);
            xamlHelper.SetTextFor(inputSmartTool_TextBox, englishSmartToolChoice + "|" + englishMeasurementChoice);
        } //fertig
        public void FillInputActuatorTestCombinedText(TextBox inputActuatorTest_TextBox, ComboBox inputECUChoice_AT, ComboBox inputComponentChoice_AT) {
            string shortECUName = dropDownList.GetKeyPartOf(dropDownList.GetIOChoices(inputECUChoice_AT.Text), inputComponentChoice_AT.Text);
            string englishComponentName = dropDownList.GetKeyPartOf(dropDownList.GetECUChoices(), inputECUChoice_AT.Text);
            xamlHelper.SetTextFor(inputActuatorTest_TextBox, shortECUName + "|" + englishComponentName);
            //actuatorTest.Text = shortECUName + "|" + englishComponentName;
        } //fertig
        //upper and lower limit in SmartTool
        public void SetPositiveResultLimit(TextBox TextBox, string limit) {
            xamlHelper.SetTextFor(TextBox, limit);
        }
        //Updates 
        public void UpdateActuatorTestSecondComboBox(ComboBox inputECUChoice_AT, ComboBox inputComponentChoice_AT) {
            xamlHelper.SetDropDownContent(inputECUChoice_AT, dropDownList.GetECUChoices().Select(x => x.secondPart).ToArray());
            xamlHelper.SetDropDownContent(inputComponentChoice_AT,
                dropDownList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray(), //IO_List
                dropDownList.GetIOChoices(inputECUChoice_AT.Text).ElementAt(0).secondPart); //default IO_Element
        }//fertig
        public void UpdateRDIDSecondComboBox(ComboBox ECUChoice_RDID_ComboBox, ComboBox RDIDChoice_RDID_ComboBox) {
            xamlHelper.SetDropDownContent(ECUChoice_RDID_ComboBox, dropDownList.GetECUChoices().Select(x => x.secondPart).ToArray());
            xamlHelper.SetDropDownContent(RDIDChoice_RDID_ComboBox,
                dropDownList.GetRDIDChoices(ECUChoice_RDID_ComboBox.Text).Select(x => x.secondPart).ToArray(), //Identifier_List
                dropDownList.GetRDIDChoices(ECUChoice_RDID_ComboBox.Text).ElementAt(0).secondPart); //default Identifier
        }//fertig
        public void UpdateSmartToolSecondComboboBox(ComboBox SmartTool_SM_ComboBox, ComboBox Measure_SM_ComboBox) {
            xamlHelper.SetDropDownContent(SmartTool_SM_ComboBox, dropDownList.GetSmartToolChoices().Select(x => x.secondPart).ToArray());
            xamlHelper.SetDropDownContent(Measure_SM_ComboBox,
                dropDownList.GetMeasurementChoices(SmartTool_SM_ComboBox.Text).Select(x => x.secondPart).ToArray(), //SmartTool_List
                dropDownList.GetMeasurementChoices(SmartTool_SM_ComboBox.Text).ElementAt(0).secondPart);  //default MeasurementTool
        } //fertig

    }
}
