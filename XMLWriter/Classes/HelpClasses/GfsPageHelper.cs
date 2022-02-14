using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;


namespace XMLWriter.Classes.HelpClasses {
    internal class GfsPageHelper {
        DataSetService dataSetService = new DataSetService();
        UtilityFunctions utility = new UtilityFunctions();
        GfsPageInputHelper inputHelper = new GfsPageInputHelper();
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
        public string HandleToolChoiceAndResultingPositiveResult(ComboBox toolChoice, ComboBox actuatorTest, ComboBox RDID, ComboBox smartTool, TextBox posResult_RDID, TextBox posResult_SM) {
            if (toolChoice.Text == dropDownList.GetToolChoice()[1]) //AT
            {
                smartTool.Text = "false";
                RDID.Text = "false";
                return "";
            }
            else if (toolChoice.Text == dropDownList.GetToolChoice()[2]) //SmT
            {
                actuatorTest.Text = "false";
                RDID.Text = "false";
                posResult_RDID.Text = "false";
                return posResult_SM.Text;
            }
            else if (toolChoice.Text == dropDownList.GetToolChoice()[3]) //RDID
            {
                smartTool.Text = "false";
                actuatorTest.Text = "false";
                posResult_SM.Text = "false";
                return posResult_RDID.Text;
            }
            return "";
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

        /// Visibility changes>
        
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

        
        
       
        ///ActuatorTest Init
        public void InitActuatorTestDropdowns(ComboBox inputECUChoice_AT_ComboBox, ComboBox inputToolChoice, ComboBox inputComponentChoice_AT, TextBox inputActuatorTest) {
            //Für Verständnis siehe Beispiel bei RDID Init
            inputHelper.InitECUChoiceDropDownDefaultValue(inputECUChoice_AT_ComboBox);
            if (IsActuatorTest(inputToolChoice)) {
                string[] positiveResultDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest.Split('|');
                xamlHelper.SetDropDownActiveELementFor(inputECUChoice_AT_ComboBox, dropDownList.GetDisplayPartOf(dropDownList.GetECUChoices(), positiveResultDupel[1]));
                inputHelper.SetIOChoices(inputComponentChoice_AT,
                    dropDownList.GetIOChoices(inputECUChoice_AT_ComboBox.Text).Select(x => x.secondPart).ToArray(),
                    positiveResultDupel[0]);
            }
            else {
                inputHelper.SetIOChoices(inputComponentChoice_AT,
                    dropDownList.GetIOChoices(inputECUChoice_AT_ComboBox.Text).Select(x => x.secondPart).ToArray(),
                    dropDownList.GetIOChoices(inputECUChoice_AT_ComboBox.Text).ElementAt(0).secondPart);
            }
            inputHelper.FillInputActuatorTestCombinedText(inputECUChoice_AT_ComboBox, inputComponentChoice_AT, inputActuatorTest);

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
            inputHelper.InitECUChoiceDropDownDefaultValue(ECUChoice_RDID_ComboBox);
            if (IsRDID(inputToolChoice)) {
                string[] positiveResultDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID.Split('|');
                inputHelper.ChangeECUActiveElementTo(ECUChoice_RDID_ComboBox, dropDownList.GetDisplayPartOf(dropDownList.GetECUChoices(), positiveResultDupel[1]));
                inputHelper.SetRDIDChoices(RDIDChoice_RDID_ComboBox,
                    dropDownList.GetRDIDChoices(ECUChoice_RDID_ComboBox.Text).Select(x => x.secondPart).ToArray(),
                    dropDownList.GetDisplayPartOf(dropDownList.GetRDIDChoices(ECUChoice_RDID_ComboBox.Text), positiveResultDupel[0]));
                inputHelper.InitPositiveResult(inputPositiveResult_RDID);
                inputHelper.InitReadData(ReadData_TextBox);
            }
            else  //Wenn nicht vorhanden, dann zeig das erste Element an. 
            {//Tatsächlich würde vermutlich das initialisieren der Liste ausreichen und das setzen des aktiven Elements ist nicht so wichtig.
                inputHelper.SetRDIDChoices(RDIDChoice_RDID_ComboBox,
                    dropDownList.GetRDIDChoices(ECUChoice_RDID_ComboBox.Text).Select(x => x.secondPart).ToArray(),
                    dropDownList.GetRDIDChoices(ECUChoice_RDID_ComboBox.Text).ElementAt(0).secondPart);
            }
        }
        ///SmartTool Init
        public void InitSmartToolDropdowns(ComboBox SmartTool_ComboBox, ComboBox inputToolChoice, TextBox SmartTool_TextBox, ComboBox Measurement_ComboBox, TextBox PositiveResult_SM_TextBox, TextBox PositiveResult_LowerLimit_TextBox, TextBox PositiveResult_UpperLimit_TextBox) {
            //Für Verständnis siehe Beispiel bei RDID Init
            inputHelper.InitSmartToolDropDownDefaultValue(SmartTool_ComboBox);
            if (IsSmartTool(inputToolChoice)) {
                inputHelper.InitSmartToolValue(SmartTool_TextBox);
                string[] positiveResultDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool.Split('|');
                inputHelper.ChangeSmartToolActiveElementTo(SmartTool_ComboBox, dropDownList.GetDisplayPartOf(dropDownList.GetSmartToolChoices(), positiveResultDupel[0]));
                inputHelper.SetMeasurementChoices(Measurement_ComboBox,
                    dropDownList.GetMeasurementChoices(SmartTool_ComboBox.Text).Select(x => x.secondPart).ToArray(),        //Measurement List of chosen SmartTool
                    dropDownList.GetDisplayPartOf(dropDownList.GetMeasurementChoices(SmartTool_ComboBox.Text), positiveResultDupel[1])); //Chosen Measurement
            }
            else //Wenn nicht vorhanden, dann zeig das erste Element an.
            {
                inputHelper.SetMeasurementChoices(Measurement_ComboBox,
                    dropDownList.GetMeasurementChoices(SmartTool_ComboBox.Text).Select(x => x.secondPart).ToArray(), //Measurement List of chosen SmartTool
                    dropDownList.GetMeasurementChoices(SmartTool_ComboBox.Text).ElementAt(0).secondPart); //default Measurement of that list
            }
            inputHelper.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_ComboBox, Measurement_ComboBox);

            if (IsSmartTool()) {
                InitSmartToolLimits(PositiveResult_SM_TextBox, PositiveResult_LowerLimit_TextBox, PositiveResult_UpperLimit_TextBox);
            }
        }
        
        
        
        private void InitSmartToolLimits(TextBox posRes_SM, TextBox posRes_Lower, TextBox posRes_Upper) {
            if (PositiveResultIsEmptyOrFalse()) {
                inputHelper.WritePositiveResultDependingOnLowerAndUpperLimit(posRes_SM, posRes_Upper, posRes_Lower);
            }
            else {
                xamlHelper.SetTextFor(posRes_SM, dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult);
                if (PositiveResultContains('|')) {
                    string[] PosResDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult.Split('|');
                    xamlHelper.SetTextFor(posRes_Lower, PosResDupel[0]);
                    xamlHelper.SetTextFor(posRes_Upper, PosResDupel[1]);
                 
                }
                else if (PositiveResultContains(';')) {
                    string[] PosResDupel = dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult.Split(';');
                    if (PosResDupel[1] == "lower") {
                        xamlHelper.SetTextFor(posRes_Lower, PosResDupel[0]);
                    }
                    else if (PosResDupel[1] == "upper") {
                        xamlHelper.SetTextFor(posRes_Upper, PosResDupel[0]);
                    }
                }
            }
        } //fertig


        //Checks
        public void CheckForWhatToolHasBeenChosen(ComboBox inputToolChoice) {
            if (consol.showMiscGfs) {
                System.Diagnostics.Debug.WriteLine("AT: " + dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest + "                                                      GfsPage(cs).CheckForWhatToolHasBeenChosen()");
                System.Diagnostics.Debug.WriteLine("SmT: " + dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool);
                System.Diagnostics.Debug.WriteLine("RDID: " + dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID);
                System.Diagnostics.Debug.WriteLine("PosRes: " + dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).positiveResult + "\n");
            }

            if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest != "" && dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).actuatorTest != "false") {
                xamlHelper.SetDropDownActiveELementFor(inputToolChoice, dropDownList.GetToolChoice()[1]);
            }
            else if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool != "" && dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).smartTool != "false") {
                xamlHelper.SetDropDownActiveELementFor(inputToolChoice, dropDownList.GetToolChoice()[2]);
            }
            else if (dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID != "" && dataSetService.GetDataSets().ElementAt(guiHelper.GetIndex()).RDID != "false") {
                xamlHelper.SetDropDownActiveELementFor(inputToolChoice, dropDownList.GetToolChoice()[3]);
            }
            else {
                inputToolChoice.Text = language.GetStringPleaseChoose(); //Not sure if this works whatsoever
            }
        } //fertig
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
        }
        private bool IsActuatorTest(ComboBox inputToolChoice) {
            if (inputToolChoice.Text == dropDownList.GetToolChoice()[1]) {
                return true;
            }
            return false;
        }
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
    }
}

