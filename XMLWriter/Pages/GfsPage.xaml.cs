using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Linq;
using XMLWriter.Classes;
using XMLWriter.Classes.HelpClasses;

namespace XMLWriter.Pages {
    /// <summary>
    /// Interaktionslogik für GfsPage.xaml
    /// </summary>
    public partial class GfsPage : Page {
        DataSetService dataSet = new DataSetService();
        GfsPageHelper gfsPageHelper = new GfsPageHelper();
        Language language = new Language();
        GUIMovementHelper gui = new GUIMovementHelper();
        DropDownOptionLists dropDownList = new DropDownOptionLists();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        ConsoleControl consol = new ConsoleControl();

        private static string positiveResult;

        public GfsPage() {
            InitializeComponent();
            InitTextItems();
            InitValueItems();
        }

        //buttons
        private void BtnNext_Click(object sender, RoutedEventArgs e) {
            positiveResult = gfsPageHelper.handleToolChoiceAndResultingPositiveResult(
                inputToolChoice, 
                inputComponentChoice_AT, 
                inputRDBIChoice_RDBI, 
                inputSmartTool_SM, 
                inputPositiveResult_RDBI, 
                inputPositiveResult_SM, 
                positiveResult);
            gfsPageHelper.SaveCurrentInput(
                inputStepName, 
                inputText, 
                inputAnim, 
                inputInstruction, 
                inputPositiveID, 
                inputNegativeID, 
                positiveResult, 
                inputRepXML, 
                inputComponentChoice_AT, 
                inputRDBIChoice_RDBI, 
                inputSmartTool_SM, 
                inputNextStep, 
                inputLastStep, 
                inputToolChoice);
            gfsPageHelper.PrepareNextPage();
            _ = NavigationService.Navigate(new GfsPage());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            positiveResult = gfsPageHelper.handleToolChoiceAndResultingPositiveResult(
                inputToolChoice, 
                inputComponentChoice_AT, 
                inputRDBIChoice_RDBI, 
                inputSmartTool_SM, 
                inputPositiveResult_RDBI, 
                inputPositiveResult_SM, 
                positiveResult);
            gfsPageHelper.SaveCurrentInput(
                inputStepName,
                inputText,
                inputAnim,
                inputInstruction,
                inputPositiveID,
                inputNegativeID,
                positiveResult,
                inputRepXML,
                inputComponentChoice_AT,
                inputRDBIChoice_RDBI,
                inputSmartTool_SM,
                inputNextStep,
                inputLastStep,
                inputToolChoice);
            gfsPageHelper.PreparePreviousPage();
            if (gui.IsFirstPage()) {
                _ = NavigationService.Navigate(new StartPage());
            }
            else {
                gui.DecrementSteps();
                _ = NavigationService.Navigate(new GfsPage());
            }
        }
        private void BtnBackDelete_Click(object sender, RoutedEventArgs e) {
            gfsPageHelper.DeleteCurrentSet();
            if (gui.IsFirstPage()) {
                _ = NavigationService.Navigate(new StartPage());
            }
            else {
                _ = NavigationService.Navigate(new GfsPage());
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            positiveResult = gfsPageHelper.handleToolChoiceAndResultingPositiveResult(
                inputToolChoice,
                inputComponentChoice_AT,
                inputRDBIChoice_RDBI,
                inputSmartTool_SM,
                inputPositiveResult_RDBI,
                inputPositiveResult_SM,
                positiveResult);
            gfsPageHelper.SaveCurrentInput(
                inputStepName,
                inputText,
                inputAnim,
                inputInstruction,
                inputPositiveID,
                inputNegativeID,
                positiveResult,
                inputRepXML,
                inputComponentChoice_AT,
                inputRDBIChoice_RDBI,
                inputSmartTool_SM,
                inputNextStep,
                inputLastStep,
                inputToolChoice);
            _ = NavigationService.Navigate(new SavePage());
        }

        //Comboboxes and Texboxes
        private void inputToolChoice_DropDownClosed(object sender, System.EventArgs e) {
            gfsPageHelper.ShowItemsAfterToolChoice(inputToolChoice, actuatorTest, smartTool, RDID);
        }
        //Aktortest
        private void inputECUChoice_AT_DropDownClosed(object sender, System.EventArgs e) {
            FillInputActuatorTestText();
            UpdateActuatorTestComboBox();
        }
        private void inputComponentChoice_AT_DropDownClosed(object sender, System.EventArgs e) {
            FillInputActuatorTestText();
        }
        //RDBI
        private void inputECUChoice_RDBI_DropDownClosed(object sender, System.EventArgs e) {
            FillInputReadDataText();
            UpdateRDBIComboBox();
        }
        private void inputRDBIChoice_RDBI_DropDownClosed(object sender, System.EventArgs e) {
            FillInputReadDataText();
        }
        //SmartTool
        private void inputSmartTool_SM_DropDownClosed(object sender, System.EventArgs e) {
            FillInputSmartToolText();
            UpdateSmartToolComboboBox();
        }
        private void inputMeasure_SM_DropDownClosed(object sender, System.EventArgs e) {
            FillInputSmartToolText();
        }
        private void inputMeasure_SM_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (dropDownList.GetKeyPartOf(dropDownList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text) == "") //Is empty if FindIndex is 0. To get direct input ... well it has to be direct^^
            {
                gfsPageHelper.
                inputSmartTool.Text = dropDownList.GetKeyPartOf(dropDownList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + inputMeasure_SM.Text;
            }
            else {
                FillInputSmartToolText();
            }
        }
        private void inputPositiveResult_UpperLimit_TextChanged(object sender, TextChangedEventArgs e) {
            gfsPageHelper.CheckForWhatCaseInSmartToolPositiveResult(inputPositiveResult_SM, inputPositiveResult_UpperLimit, inputPositiveResult_LowerLimit);
        }
        private void inputPositiveResult_LowerLimit_TextChanged(object sender, TextChangedEventArgs e) {
            gfsPageHelper.CheckForWhatCaseInSmartToolPositiveResult(inputPositiveResult_SM, inputPositiveResult_UpperLimit, inputPositiveResult_LowerLimit);
        }

        //Inits
        private void InitTextItems() {

            //Inhalte linke Spalte
            gfsPageHelper.SetLabelStepName(textStep);
            gfsPageHelper.SetLabelContent(textContentTitel);
            gfsPageHelper.SetLabelAnimation(textAnimTitel);
            gfsPageHelper.SetLabelInstruction(textInstructionTitel);
            gfsPageHelper.SetLabelTitel(textTitel);

            //Inhalte rechte Spalte
            gfsPageHelper.SetLabelPositiveID(textPositiveID);
            gfsPageHelper.SetLabelNegativeID(textNegativeID);
            gfsPageHelper.SetLabelPositiveResult(textPositiveResult_SM);
            gfsPageHelper.SetLabelPositiveResult(textPositiveResult_RDBI);
            gfsPageHelper.SetLabelRepXML(textRepXML);
            gfsPageHelper.SetLabelActuatorTesst(textActuatorTest);
            gfsPageHelper.SetLabelRDID(textReadData);
            gfsPageHelper.SetLabelSmartTool(textSmartTool);
            gfsPageHelper.SetLabelSmartTool(textSmartTool);
            //CheckBoxText
            gfsPageHelper.SetTextNextStep(inputNextStep);
            gfsPageHelper.SetTextLastStep(inputLastStep);

            //Buttons
            gfsPageHelper.SetButtonNext(btnNext);
            gfsPageHelper.SetButtonDelete(btnBackDelete);
            gfsPageHelper.SetButtonSave(btnSave);
            gfsPageHelper.SetButtonBack(btnBack);
        }
        private void InitValueItems() {
            InitLeftSideItems();
            InitFixedRightSideItems();
            InitFlexRightSideItems();
            gfsPageHelper.ShowItemsAfterToolChoice(inputToolChoice, actuatorTest, smartTool, RDID);
        }
        private void InitLeftSideItems() {
            gfsPageHelper.SetStepNameValue(inputStepName);
            gfsPageHelper.SetTextValue(inputText);
            gfsPageHelper.SetAnimValue(inputAnim);
            gfsPageHelper.SetInstructionValue(inputInstruction);
        }
        private void InitFixedRightSideItems() {
            gfsPageHelper.SetPositiveID(inputPositiveID);
            gfsPageHelper.SetNegativeID(inputNegativeID);
            gfsPageHelper.SetRepXMLValue(inputRepXML);
            //Check boxes
            gfsPageHelper.SetNextStepValue(inputNextStep);
            gfsPageHelper.SetLastStepValue(inputLastStep);
            
        }
        //Inits erste Dropdown Ebene
        private void InitFlexRightSideItems() {
            CheckForWhatToolHasBeenChosen();
            inputToolChoice.ItemsSource = dropDownList.GetToolChoice();
            xamlHelper.SetDropDownActiveELementFor(inputToolChoice, dataSet.toolChoice);
            InitActuatorTextDropdowns();
            InitSmartToolDropdowns();
            InitReadDataDropdowns();
        }

        //Inits zweite und dritte Dropdown Eebene
        private void InitActuatorTextDropdowns() {
            inputECUChoice_AT.ItemsSource = dropDownList.GetECUChoices().Select(x => x.secondPart).ToArray();
            if (inputToolChoice.Text == dropDownList.GetToolChoice()[1]) {
                string[] positiveResultDupel = dataSet.actuatorTest.Split('|');
                inputECUChoice_AT.Text = dropDownList.GetDisplayPartOf(dropDownList.GetECUChoices(), positiveResultDupel[1]);
                inputComponentChoice_AT.ItemsSource = dropDownList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray();
                inputComponentChoice_AT.Text = dropDownList.GetDisplayPartOf(dropDownList.GetIOChoices(inputECUChoice_AT.Text), positiveResultDupel[0]);
            }
            else {
                inputECUChoice_AT.Text = dropDownList.GetECUChoices().ElementAt(0).secondPart;
                inputComponentChoice_AT.ItemsSource = dropDownList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray();
                inputComponentChoice_AT.Text = dropDownList.GetIOChoices(inputECUChoice_AT.Text).ElementAt(0).secondPart;
            }
            FillInputActuatorTestText();

        }
        private void UpdateActuatorTestComboBox() {
            inputECUChoice_AT.ItemsSource = dropDownList.GetECUChoices().Select(x => x.secondPart).ToArray();
            inputComponentChoice_AT.ItemsSource = dropDownList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray();
            inputComponentChoice_AT.Text = dropDownList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray()[0];
        }
        private void InitReadDataDropdowns() {
            inputECUChoice_RDBI.ItemsSource = dropDownList.GetECUChoices().Select(x => x.secondPart).ToArray();

            if (inputToolChoice.Text == dropDownList.GetToolChoice()[3]) {
                string[] positiveResultDupel = dataSet.RDID.Split('|');
                if (consol.showMiscGfs) {
                    System.Diagnostics.Debug.WriteLine("PosRes: " + dataSet.RDID + "                                                 ---GfsPage(cs).InitReadDataDropDowns()");
                    System.Diagnostics.Debug.WriteLine("ECU: " + positiveResultDupel[1] + "                                                      ---GfsPage(cs).InitReadDataDropDowns()");
                    System.Diagnostics.Debug.WriteLine("RDID: " + positiveResultDupel[0] + "                                                     ---GfsPage(cs).InitReadDataDropDowns()\n");
                }
                inputECUChoice_RDBI.Text = dropDownList.GetDisplayPartOf(dropDownList.GetECUChoices(), positiveResultDupel[1]);
                inputRDBIChoice_RDBI.ItemsSource = dropDownList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray();
                inputRDBIChoice_RDBI.Text = dropDownList.GetDisplayPartOf(dropDownList.GetRDIDChoices(inputECUChoice_RDBI.Text), positiveResultDupel[0]);
                inputPositiveResult_RDBI.Text = dataSet.positiveResult;
                inputReadData.Text = dataSet.RDID;
            }
            else  //Wenn nicht vorhanden, dann zeig das erste Element an.
            {
                inputECUChoice_RDBI.Text = dropDownList.GetECUChoices().ElementAt(0).secondPart;
                inputRDBIChoice_RDBI.ItemsSource = dropDownList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray();
                inputRDBIChoice_RDBI.Text = dropDownList.GetRDIDChoices(inputECUChoice_RDBI.Text).ElementAt(0).secondPart;
            }
        }

        private void UpdateRDBIComboBox() {
            inputECUChoice_RDBI.ItemsSource = dropDownList.GetECUChoices().Select(x => x.secondPart).ToArray();
            inputRDBIChoice_RDBI.ItemsSource = dropDownList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray();
            inputRDBIChoice_RDBI.Text = dropDownList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray()[0];
        }
        private void InitSmartToolDropdowns() {
            inputSmartTool_SM.ItemsSource = dropDownList.GetSmartToolChoices().Select(x => x.secondPart).ToArray();

            if (inputToolChoice.Text == dropDownList.GetToolChoice()[2]) {
                inputSmartTool.Text = dataSet.smartTool;
                if (consol.showMiscGfs) System.Diagnostics.Debug.WriteLine("Init SM: " + dataSet.smartTool + "                              ----GfsPage(cs).InitSmartToolDropdowns()");
                if (consol.showMiscGfs) System.Diagnostics.Debug.WriteLine("Init SM: " + inputSmartTool.Text);
                string[] positiveResultDupel = dataSet.smartTool.Split('|');
                inputSmartTool_SM.Text = dropDownList.GetDisplayPartOf(dropDownList.GetSmartToolChoices(), positiveResultDupel[0]);
                inputMeasure_SM.ItemsSource = dropDownList.GetMeasurementChoices(inputSmartTool_SM.Text).Select(x => x.secondPart).ToArray();
                inputMeasure_SM.Text = dropDownList.GetDisplayPartOf(dropDownList.GetMeasurementChoices(inputSmartTool_SM.Text), positiveResultDupel[1]);
            }
            else //Wenn nicht vorhanden, dann zeig das erste Element an.
            {
                inputSmartTool_SM.Text = dropDownList.GetSmartToolChoices().ElementAt(0).secondPart;
                inputMeasure_SM.ItemsSource = dropDownList.GetMeasurementChoices(inputSmartTool_SM.Text).Select(x => x.secondPart).ToArray();
                inputMeasure_SM.Text = dropDownList.GetMeasurementChoices(inputSmartTool_SM.Text).ElementAt(0).secondPart;
            }
            FillInputSmartToolText();
            if (dataSet.toolChoice == dropDownList.GetToolChoice()[2]) {
                if (consol.showMiscGfs) System.Diagnostics.Debug.WriteLine("SmartTool not Epty or false check: >" + inputSmartTool.Text + "<                                 ---GfsPage(cs).FillInputSmartToolText()");
                InitSmartToolLimits();
            }
        }
        private void InitSmartToolLimits() {
            if (dataSet.positiveResult == "" || dataSet.positiveResult == "false") {
                gfsPageHelper.CheckForWhatCaseInSmartToolPositiveResult(inputPositiveResult_SM, inputPositiveResult_UpperLimit, inputPositiveResult_LowerLimit);
            }
            else {
                if (consol.showMiscGfs) System.Diagnostics.Debug.WriteLine("SmT - PosRes: >" + dataSet.positiveResult + "<                          ---InitSmartToolLimits()");
                inputPositiveResult_SM.Text = dataSet.positiveResult;
                if (dataSet.positiveResult.Contains('|')) {
                    string[] PosResDupel = dataSet.positiveResult.Split('|');
                    inputPositiveResult_UpperLimit.Text = PosResDupel[1];
                    inputPositiveResult_LowerLimit.Text = PosResDupel[0];
                }
                else if (dataSet.positiveResult.Contains(';')) {
                    string[] PosResDupel = dataSet.positiveResult.Split(';');
                    if (PosResDupel[1] == "lower") {
                        inputPositiveResult_LowerLimit.Text = PosResDupel[0];
                    }
                    else if (PosResDupel[1] == "upper") {
                        inputPositiveResult_UpperLimit.Text = PosResDupel[0];
                    }


                }

            }

        }
        private void UpdateSmartToolComboboBox() {
            inputSmartTool_SM.ItemsSource = dropDownList.GetSmartToolChoices().Select(x => x.secondPart).ToArray();
            inputMeasure_SM.ItemsSource = dropDownList.GetMeasurementChoices(inputSmartTool_SM.Text).Select(x => x.secondPart).ToArray();
            inputMeasure_SM.Text = dropDownList.GetMeasurementChoices(inputSmartTool_SM.Text).Select(x => x.secondPart).ToArray()[0];
        }

        //Anderes
        private void HideAllItemsWithToggleVisibility() {
            RDID.Visibility = Visibility.Hidden;
            smartTool.Visibility = Visibility.Hidden;
            actuatorTest.Visibility = Visibility.Hidden;
        }
        private void ShowItemsAfterToolChoice() {

        }

        private void FillInputSmartToolText() {
            inputSmartTool.Text = dropDownList.GetKeyPartOf(dropDownList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + dropDownList.GetKeyPartOf(dropDownList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text);
        }
        private void FillInputReadDataText() {
            inputReadData.Text = dropDownList.GetKeyPartOf(dropDownList.GetRDIDChoices(inputECUChoice_RDBI.Text), inputRDBIChoice_RDBI.Text) + "|" + dropDownList.GetKeyPartOf(dropDownList.GetECUChoices(), inputECUChoice_RDBI.Text);
        }
        private void FillInputActuatorTestText() {
            inputActuatorTest.Text = dropDownList.GetKeyPartOf(dropDownList.GetIOChoices(inputECUChoice_AT.Text), inputComponentChoice_AT.Text) + "|" + dropDownList.GetKeyPartOf(dropDownList.GetECUChoices(), inputECUChoice_AT.Text);
        }
        private void CheckForWhatToolHasBeenChosen() {
            if (consol.showMiscGfs) {
                System.Diagnostics.Debug.WriteLine("AT: " + dataSet.actuatorTest + "                                                      GfsPage(cs).CheckForWhatToolHasBeenChosen()");
                System.Diagnostics.Debug.WriteLine("SmT: " + dataSet.smartTool);
                System.Diagnostics.Debug.WriteLine("RDID: " + dataSet.RDID);
                System.Diagnostics.Debug.WriteLine("PosRes: " + dataSet.positiveResult + "\n");
            }

            if (dataSet.actuatorTest != "" && dataSet.actuatorTest != "false") {
                inputToolChoice.Text = dropDownList.GetToolChoice()[1];
            }
            else if (dataSet.smartTool != "" && dataSet.smartTool != "false") {
                inputToolChoice.Text = dropDownList.GetToolChoice()[2];
            }
            else if (dataSet.RDID != "" && dataSet.RDID != "false") {
                inputToolChoice.Text = dropDownList.GetToolChoice()[3];
            }
            else {
                inputToolChoice.Text = language.GetStringPleaseChoose();
            }
        }


    }
}
