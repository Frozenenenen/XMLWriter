using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;


namespace XMLWriter.Classes.HelpClasses {
    internal class GfsPageHelper {
        
        UtilityFunctions utility = new UtilityFunctions();
        GUIMovementHelper guiHelper = new GUIMovementHelper();
        DataSetService dataSetService = new DataSetService();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        DropDownOptionLists dropDownList = new DropDownOptionLists();
        GfsPageInputHelper gfsInputHelper = new GfsPageInputHelper();
        

        /// --- Navigation --- ///
        public void PrepareNextPage() {
            dataSetService.InitNewDataSetWhereRequired();
            guiHelper.IncrementSteps();
        }
        public void PreparePreviousPage() {
            guiHelper.DecrementSteps();
        }
        public void DeleteCurrentSet() {
            dataSetService.DeleteDataSet();
            guiHelper.DecrementStepsMax();
        }
        public void InsertNewSet() {
            dataSetService.InsertNewDataSet();
        }

        /// ---- WriteToDataSet --- ///
        public string HandleToolChoiceAndResultingPositiveResult(ComboBox toolChoice, TextBox actuatorTest, TextBox RDID, TextBox smartTool, TextBox posResult_RDID, TextBox posResult_SM) {
            //Set all not used elemets false or empty and return the positiveResult value
            if (IsActuatorTest(toolChoice)) //AT 
            {
                smartTool.Text = "false";
                RDID.Text = "false";
                return "";
            }
            else if (IsSmartTool(toolChoice)) //SmT
            {
                actuatorTest.Text = "false";
                RDID.Text = "false";
                posResult_RDID.Text = "false";
                return posResult_SM.Text;
            }
            else if (IsRDID(toolChoice)) //RDID
            {
                smartTool.Text = "false";
                actuatorTest.Text = "false";
                posResult_SM.Text = "false";
                return posResult_RDID.Text;
            }
            return "";
        }
        public void SaveCurrentInput(TextBox stepName, TextBox text, TextBox anim, TextBox instruction, ComboBox positiveID, ComboBox negativeID, string positiveResult, TextBox repXML, TextBox actuatorTest, TextBox RDID, TextBox smartTool, CheckBox nextStep, CheckBox lastStep, ComboBox toolChoice) {

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
        }

        /// Visibility changes>

        public void ShowItemsAfterToolChoice(ComboBox inputToolChoice, DockPanel actuatorTest_Panel, DockPanel smartTool_Panel, DockPanel RDID_Panel) {
            if (IsActuatorTest(inputToolChoice)) //AT
            {
                HideAllItemsWithToggleVisibility(actuatorTest_Panel, smartTool_Panel, RDID_Panel);
                ShowActuatorTestPanel(actuatorTest_Panel);
            }
            else if (IsSmartTool(inputToolChoice))  //SmT
            {
                HideAllItemsWithToggleVisibility(actuatorTest_Panel, smartTool_Panel, RDID_Panel);
                ShowSmartToolPanel(smartTool_Panel);
            }
            else if (IsRDID(inputToolChoice)) //RDID
            {
                HideAllItemsWithToggleVisibility(actuatorTest_Panel, smartTool_Panel, RDID_Panel);
                ShowRDIDPanel(RDID_Panel);
            }
            else {
                HideAllItemsWithToggleVisibility(actuatorTest_Panel, smartTool_Panel, RDID_Panel);
            }
        }
        private void HideAllItemsWithToggleVisibility(DockPanel actuatorTest_Panel, DockPanel smartTool_Panel, DockPanel RDID_Panel) {
            RDID_Panel.Visibility = Visibility.Hidden;
            smartTool_Panel.Visibility = Visibility.Hidden;
            actuatorTest_Panel.Visibility = Visibility.Hidden;
        }




        ///ActuatorTest Init
        public void InitActuatorTestDropdowns(ComboBox inputECUChoice_AT_ComboBox, ComboBox inputToolChoice, ComboBox inputComponentChoice_AT_ComboBox, TextBox inputActuatorTest_TextBox) {
            //Für Verständnis siehe Beispiel bei RDID Init
            gfsInputHelper.InitECUChoiceDropDownDefaultValue(inputECUChoice_AT_ComboBox);
            if (IsActuatorTest(inputToolChoice)) {
                string[] positiveResultDupel = { "", "" };
                System.Diagnostics.Debug.WriteLine(dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest);
                if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest!="false") {
                    positiveResultDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest.Split('|');
                }
                System.Diagnostics.Debug.WriteLine(positiveResultDupel[0]);
                System.Diagnostics.Debug.WriteLine(positiveResultDupel[1]);
                gfsInputHelper.ChangeECUActiveElementTo(inputECUChoice_AT_ComboBox, dropDownList.GetDisplayPartOf(dropDownList.GetECUChoices(), positiveResultDupel[1]));
                gfsInputHelper.SetIOChoices(inputComponentChoice_AT_ComboBox,
                    dropDownList.GetIOChoices(inputECUChoice_AT_ComboBox.Text).Select(x => x.secondPart).ToArray(),
                    dropDownList.GetDisplayPartOf(dropDownList.GetIOChoices(inputECUChoice_AT_ComboBox.Text), positiveResultDupel[0]));
            }
            else {
                gfsInputHelper.SetIOChoices(inputComponentChoice_AT_ComboBox,
                    dropDownList.GetIOChoices(inputECUChoice_AT_ComboBox.Text).Select(x => x.secondPart).ToArray(),
                    dropDownList.GetIOChoices(inputECUChoice_AT_ComboBox.Text).ElementAt(0).secondPart);
            }
            gfsInputHelper.FillInputActuatorTestCombinedText(inputActuatorTest_TextBox, inputECUChoice_AT_ComboBox, inputComponentChoice_AT_ComboBox);

        }
        ///RDID Init
        public void InitReadDataDropdowns(ComboBox inputToolChoice, ComboBox ECUChoice_RDID_ComboBox, ComboBox RDIDChoice_RDID_ComboBox, TextBox inputPositiveResult_RDID, TextBox ReadData_TextBox) {
            ///Hier werden nacheinander
            ///Das ECU Dropdown mit Liste befüllt
            ///Geguckt, ob es sich beim aktuellen Datensatz bei ToolChoice um RDID handelt,
            ///Der Wert aus der Speocherliste aufgesplittet (zB "BCM|VehicleSpeed" -> "BCM" und "VehicleSpeed")
            ///Abhängig vom Gewählten ECU wird im ECU Dropdown das aktive Element angepasst. (Hier BCM)
            ///Abhängig vom genannten ECU wird dann das untere DropDown mit Liste und "VehicleSpeed" ausgewählt befüllt 
            ///danach wird der Positive Result befüllt. Dies würde nach dem einstellen der dropdowns darüber auch über die allgemeine befüllung gehen, aber hier wird der Wert aus der DatensatzListe genommen
            ///Dann wird der Wert, der oben auch aufgesplittet wird, in das entsprechende Textfeld zur visuellen Kontrolle eingefügt.
            ///Im else{} wird einfach das DropDown bei nicht nutzung mit default befüllt (erster wert)
            gfsInputHelper.InitECUChoiceDropDownDefaultValue(ECUChoice_RDID_ComboBox);
            if (IsRDID(inputToolChoice)) {
                string[] positiveResultDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID.Split('|');
                gfsInputHelper.ChangeECUActiveElementTo(ECUChoice_RDID_ComboBox, dropDownList.GetDisplayPartOf(dropDownList.GetECUChoices(), positiveResultDupel[1]));
                gfsInputHelper.SetRDIDChoices(RDIDChoice_RDID_ComboBox,
                    dropDownList.GetRDIDChoices(ECUChoice_RDID_ComboBox.Text).Select(x => x.secondPart).ToArray(),
                    dropDownList.GetDisplayPartOf(dropDownList.GetRDIDChoices(ECUChoice_RDID_ComboBox.Text), positiveResultDupel[0]));
                gfsInputHelper.InitPositiveResult(inputPositiveResult_RDID);
                gfsInputHelper.FillInputReadDataCombinedText(ReadData_TextBox, ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
            }
            else  //Wenn nicht vorhanden, dann zeig das erste Element an. 
            {//Tatsächlich würde vermutlich das initialisieren der Liste ausreichen und das setzen des aktiven Elements ist nicht so wichtig.
                gfsInputHelper.SetRDIDChoices(RDIDChoice_RDID_ComboBox,
                    dropDownList.GetRDIDChoices(ECUChoice_RDID_ComboBox.Text).Select(x => x.secondPart).ToArray(),
                    dropDownList.GetRDIDChoices(ECUChoice_RDID_ComboBox.Text).ElementAt(0).secondPart);
            }
        }
        ///SmartTool Init
        public void InitSmartToolDropdowns(ComboBox SmartTool_ComboBox, ComboBox inputToolChoice, TextBox inputSmartTool_TextBox, ComboBox Measurement_ComboBox, TextBox PositiveResult_SM_TextBox, TextBox PositiveResult_LowerLimit_TextBox, TextBox PositiveResult_UpperLimit_TextBox) {
            //Für Verständnis siehe Beispiel bei RDID Init
            gfsInputHelper.InitSmartToolDropDownDefaultValue(SmartTool_ComboBox);
            if (IsSmartTool(inputToolChoice)) {
                gfsInputHelper.InitSmartToolValue(inputSmartTool_TextBox);
                string[] positiveResultDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool.Split('|');
                gfsInputHelper.ChangeSmartToolActiveElementTo(SmartTool_ComboBox, dropDownList.GetDisplayPartOf(dropDownList.GetSmartToolChoices(), positiveResultDupel[0]));
                gfsInputHelper.SetMeasurementChoices(Measurement_ComboBox,
                    dropDownList.GetMeasurementChoices(SmartTool_ComboBox.Text).Select(x => x.secondPart).ToArray(),        //Measurement List of chosen SmartTool
                    dropDownList.GetDisplayPartOf(dropDownList.GetMeasurementChoices(SmartTool_ComboBox.Text), positiveResultDupel[1])); //Chosen Measurement
            }
            else //Wenn nicht vorhanden, dann zeig das erste Element an.
            {
                gfsInputHelper.SetMeasurementChoices(Measurement_ComboBox,
                    dropDownList.GetMeasurementChoices(SmartTool_ComboBox.Text).Select(x => x.secondPart).ToArray(), //Measurement List of chosen SmartTool
                    dropDownList.GetMeasurementChoices(SmartTool_ComboBox.Text).ElementAt(0).secondPart); //default Measurement of that list
            }
            gfsInputHelper.FillInputSmartToolCombinedText(inputSmartTool_TextBox, SmartTool_ComboBox, Measurement_ComboBox);

            if (IsSmartTool()) {
                InitSmartToolLimits(PositiveResult_SM_TextBox, PositiveResult_LowerLimit_TextBox, PositiveResult_UpperLimit_TextBox);
            }
        }
        private void InitSmartToolLimits(TextBox posRes_SM, TextBox posRes_Lower, TextBox posRes_Upper) {
            if (PositiveResultIsEmptyOrFalse()) {
                gfsInputHelper.WritePositiveResultDependingOnLowerAndUpperLimit(posRes_SM, posRes_Upper, posRes_Lower);
            }
            else {
                gfsInputHelper.InitPositiveResult(posRes_SM);
                if (PositiveResultContains('|')) { // | divides an upper or lower limit. e.g. 3|9. If this isnt used its either x;lower or x;upper which is checked in else if()
                    string[] PosResDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult.Split('|');
                    gfsInputHelper.SetPositiveResultLimit(posRes_Lower, PosResDupel[0]);
                    gfsInputHelper.SetPositiveResultLimit(posRes_Upper, PosResDupel[1]);

                }
                else if (PositiveResultContains(';')) {
                    string[] PosResDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult.Split(';');
                    if (IsLowerLimit(PosResDupel[1])) {
                        gfsInputHelper.SetPositiveResultLimit(posRes_Lower, PosResDupel[0]);
                    }
                    else if (IsUpperLimit(PosResDupel[1])) {
                        gfsInputHelper.SetPositiveResultLimit(posRes_Upper, PosResDupel[0]);
                    }
                }
            }
        } //fertig


        //Checks
        public void CheckForWhatToolHasBeenChosen(ComboBox inputToolChoice) {
            if (ActuatorTestIsNotEmptyNorFalse()) { //There is just one of the 3 tools filled, so as long as its not empty it is the one thats chosen
                gfsInputHelper.SetToolChoiceActiveElementToActuatorTest(inputToolChoice);
            }
            else if (SmartToolIsNotEmptyNorFalse()) {
                gfsInputHelper.SetToolChoiceActiveElementToSmartTool(inputToolChoice);
            }
            else if (RDIDIsNotEmptyNorFalse()) {
                gfsInputHelper.SetToolChoiceActiveElementToRDID(inputToolChoice);
            }
            else {
                System.Diagnostics.Debug.WriteLine("Leer oder Fehler bei CheckForWhatToolHasBeenChosen() in GfsPageHelper");
            }
        } //fertig
        private bool IsLowerLimit(string limit) {
            if (limit == "lower") {
                return true;
            }
            return false;
        }
        private bool IsUpperLimit(string limit) {
            if (limit == "upper") {
                return true;
            }
            return false;
        }
        private bool ActuatorTestIsNotEmptyNorFalse() {
            if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest != "" && dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest != "false") {
                return true;
            }
            return false;
        }
        private bool SmartToolIsNotEmptyNorFalse() {
            if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool != "" && dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool != "false") {
                return true;
            }
            return false;
        }
        private bool RDIDIsNotEmptyNorFalse() {
            if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID != "" && dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID != "false") {
                return true;
            }
            return false;
        }

        private bool IsSmartTool(ComboBox comboBox) {
            if (xamlHelper.IsActiveElementOf(comboBox, dropDownList.GetToolChoice()[2])) {
                return true;
            }
            return false;
        } //fertig
        private bool IsSmartTool() {
            if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).toolChoice == dropDownList.GetToolChoice()[2]) {
                return true;
            }
            return false;
        } //fertig
        private bool IsRDID(ComboBox inputToolChoice) {
            if (inputToolChoice.Text == dropDownList.GetToolChoice()[3]) {
                return true;
            }
            return false;
        } //fertig
        private bool IsActuatorTest(ComboBox inputToolChoice) {
            if (inputToolChoice.Text == dropDownList.GetToolChoice()[1]) {
                return true;
            }
            return false;
        }//fertig

        private bool PositiveResultIsEmptyOrFalse() {
            if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult == "" || dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult == "false") {
                return true;
            }
            return false;
        }//fertig
        private bool PositiveResultContains(char isolatingLink) {
            if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult.Contains(isolatingLink)) {
                return true;
            }
            return false;
        }//fertig
        public bool IsEmptyMeasure(ComboBox SmartTool_SM_ComboBox, ComboBox Measure_SM_ComboBox) {
            List<DropDownOptionTupel> measurementChoiceList = dropDownList.GetMeasurementChoices(SmartTool_SM_ComboBox.Text); //The Selected Item of the first DD determines the List of the second 
            if (dropDownList.GetKeyPartOf(measurementChoiceList, Measure_SM_ComboBox.Text) == "") {
                return true;
            }
            return false;
        } //fertig

        //Show and Hide Panels
        private void ShowActuatorTestPanel(DockPanel Panel) {
            Panel.Visibility = Visibility.Visible;

        }
        private void ShowSmartToolPanel(DockPanel Panel) {
            Panel.Visibility = Visibility.Visible;
        }
        private void ShowRDIDPanel(DockPanel Panel) {
            Panel.Visibility = Visibility.Visible;
        }
    }
}
