using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Linq;
using XMLWriter.Classes;

namespace XMLWriter.Pages {
    /// <summary>
    /// Interaktionslogik für GfsPage.xaml
    /// </summary>
    public partial class GfsPage : Page {
        DataSetService data = new DataSetService();
        Language language = new Language();
        GUIMovementHelper gui = new GUIMovementHelper();
        DropDownOptionLists ddList = new DropDownOptionLists();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        ConsoleControl consol = new ConsoleControl();

        private static string positiveResult;
        DataSet dataSet;

        public GfsPage() {
            dataSet = data.GetDataSets().ElementAt(gui.GetIndex());
            consol.ConsoleShowDataSetOfIndex(dataSet, gui.GetIndex(), "Beim Start");
            InitializeComponent();
            InitTextItems();
            InitValueItems();
        }

        //buttons
        private void BtnNext_Click(object sender, RoutedEventArgs e) {
            data.InitNewDataSetWhereRequired();

            SaveStep();
            consol.ConsoleShowDataSetOfIndex(dataSet, gui.GetStepCount(), "Vorm Speichern");
            data.SetDataSet(dataSet);
            gui.IncrementSteps();
            if (consol.showBtn) System.Diagnostics.Debug.WriteLine("\n - - - BtnNext Gfs - - - \n - - - Nächster Schritt " + (gui.GetStepCount()) + " - - - \n - - - BtnNext Gf - - - ");
            _ = NavigationService.Navigate(new GfsPage());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            SaveStep();
            data.SetDataSet(dataSet);
            if (gui.IsFirstPage()) {
                _ = NavigationService.Navigate(new StartPage());
            }
            else {
                gui.DecrementSteps();
                _ = NavigationService.Navigate(new GfsPage());
            }
            if (consol.showBtn) System.Diagnostics.Debug.WriteLine("\n - - - BtnBack Gfs - - - \n  - - - Nächster Schritt " + (gui.GetIndex()) + " - - - \n - - - BtnBack Gf - - - ");
        }
        private void BtnBackDelete_Click(object sender, RoutedEventArgs e) {
            if (gui.IsFirstPage()) {
                _ = NavigationService.Navigate(new StartPage());
                data.ResetDataSet();
            }
            else {
                gui.DecrementStepsMax(); //decreases normal stepcount aswell
                _ = NavigationService.Navigate(new GfsPage());
            }
            if (consol.showBtn) System.Diagnostics.Debug.WriteLine("\n - - - BtnDel Gfs - - - \n  - - - Nächster Schritt " + (gui.GetIndex()) + " - - - \n - - - BtnDel Gf - - - ");
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            if (consol.showBtn) System.Diagnostics.Debug.WriteLine("\n - - - BtnSave Gfs - - - \n - - - BtnSave Gf - - - ");
            SaveStep();
            data.SetDataSet(dataSet);
            gui.IncrementSteps();

            _ = NavigationService.Navigate(new SavePage());
        }
        private void SaveStep() {
            if (inputToolChoice.Text == ddList.GetToolChoice()[1]) //AT
            {
                inputSmartTool.Text = "false";
                inputReadData.Text = "false";
                positiveResult = "";
            }
            else if (inputToolChoice.Text == ddList.GetToolChoice()[2]) //SmT
            {
                inputActuatorTest.Text = "false";
                inputReadData.Text = "false";
                inputPositiveResult_RDBI.Text = "false";
                positiveResult = inputPositiveResult_SM.Text;
            }
            else if (inputToolChoice.Text == ddList.GetToolChoice()[3]) //RDID
            {
                inputSmartTool.Text = "false";
                inputActuatorTest.Text = "false";
                inputPositiveResult_SM.Text = "false";
                positiveResult = inputPositiveResult_RDBI.Text;
            }
            WriteInputToDataSet();

        }
        private void WriteInputToDataSet() {
            dataSet.toolChoice = inputToolChoice.Text;
            dataSet.stepName = inputStepName.Text;
            dataSet.text = inputText.Text;
            dataSet.anim = inputAnim.Text;
            dataSet.instruction = inputInstruction.Text;
            dataSet.positiveID = inputPositiveID.Text;
            dataSet.negativeID = inputNegativeID.Text;
            dataSet.positiveResult = positiveResult;
            dataSet.repXML = inputRepXML.Text;
            dataSet.actuatorTest = inputActuatorTest.Text;
            dataSet.smartTool = inputSmartTool.Text;
            dataSet.RDID = inputReadData.Text;
            dataSet.nextStep = inputNextStep.IsChecked;
            dataSet.lastStep = inputLastStep.IsChecked;
        }

        //Comboboxes and Texboxes
        private void inputToolChoice_DropDownClosed(object sender, System.EventArgs e) {
            ShowItemsAfterToolChoice();
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
            if (ddList.GetKeyPartOf(ddList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text) == "") //Is empty if FindIndex is 0. To get direct input ... well it has to be direct^^
            {
                inputSmartTool.Text = ddList.GetKeyPartOf(ddList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + inputMeasure_SM.Text;
            }
            else {
                FillInputSmartToolText();
            }
        }
        private void inputPositiveResult_UpperLimit_TextChanged(object sender, TextChangedEventArgs e) {
            CheckForWhatCaseInSmartToolPositiveResult();
        }
        private void inputPositiveResult_LowerLimit_TextChanged(object sender, TextChangedEventArgs e) {
            CheckForWhatCaseInSmartToolPositiveResult();
        }

        //Inits
        private void InitTextItems() {
            if (consol.showStep) System.Diagnostics.Debug.WriteLine("\n - - - Index Start Gfs - - - \n              " + gui.GetIndex() + "\n - - - Index Start Gfs - - -                                      ---GfsPage.InitTextItems()");

            //Inhalte linke Spalte
            //>´wieder einbauen!         //textStep.Content = language.GetStringStep() + " " + (data.GetStepCount() + 1);
            textContentTitel.Content = language.GetStringContent();
            textAnimTitel.Content = language.GetStringAnim();
            textInstructionTitel.Content = language.GetStringInstruction();
            textTitel.Content = language.GetStringPleaseFill();

            //Inhalte rechte Spalte
            textPositiveID.Content = language.GetStringPosID();
            textNegativeID.Content = language.GetStringNegID();
            textPositiveResult_SM.Content = language.GetStringPosResult();
            textPositiveResult_RDBI.Content = language.GetStringPosResult();
            textRepXML.Content = language.GetStringRepXML();
            textActuatorTest.Content = language.GetStringActuatorTest();
            textReadData.Content = language.GetStringReadData();
            textSmartTool.Content = language.GetStringSmartTool();
            inputNextStep.Content = language.GetStringNextStep();
            inputLastStep.Content = language.GetStringLastStep();
            //textRDBIOptional.Content = language.GetStringOptional();
            textSmartToolOptional.Content = language.GetStringOptional();

            //Buttons
            btnNext.Content = language.GetStringNext();
            btnSave.Content = language.GetStringSave();
            btnBackDelete.Content = language.GetStringReset();
            btnBack.Content = language.GetStringBack();
        }
        private void InitValueItems() {
            InitLeftSideItems();
            InitFixedRightSideItems();
            InitFlexRightSideItems();
            ShowItemsAfterToolChoice();
            if (consol.showStep) System.Diagnostics.Debug.WriteLine(" - - - Index Ende Gfs - - - \n              " + gui.GetIndex() + "\n - - - Index Ende Gfs - - - \n");
        }
        private void InitLeftSideItems() {
            inputStepName.Text = dataSet.stepName == ""
               ? "Schritt " + (gui.GetStepCount())
               : dataSet.stepName;
            inputText.Text = dataSet.text;
            inputAnim.Text = dataSet.anim == ""
                ? "default"
                : dataSet.anim;
            inputInstruction.Text = dataSet.instruction;
        }
        private void InitFixedRightSideItems() {
            inputPositiveID.ItemsSource = data.GetStepNames();
            inputPositiveID.Text = dataSet.positiveID;
            inputNegativeID.ItemsSource = data.GetStepNames();
            inputNegativeID.Text = dataSet.negativeID;
            inputRepXML.Text = dataSet.repXML;

            //Check boxes
            inputNextStep.IsChecked = dataSet.nextStep;
            inputLastStep.IsChecked = dataSet.lastStep;
        }
        //Inits erste Dropdown Ebene
        private void InitFlexRightSideItems() {
            CheckForWhatToolHasBeenChosen();
            inputToolChoice.ItemsSource = ddList.GetToolChoice();
            xamlHelper.SetDropDownActiveELementFor(inputToolChoice, dataSet.toolChoice);
            InitActuatorTextDropdowns();
            InitSmartToolDropdowns();
            InitReadDataDropdowns();
        }

        //Inits zweite und dritte Dropdown Eebene
        private void InitActuatorTextDropdowns() {
            inputECUChoice_AT.ItemsSource = ddList.GetECUChoices().Select(x => x.secondPart).ToArray();
            if (inputToolChoice.Text == ddList.GetToolChoice()[1]) {
                string[] positiveResultDupel = dataSet.actuatorTest.Split('|');
                inputECUChoice_AT.Text = ddList.GetDisplayPartOf(ddList.GetECUChoices(), positiveResultDupel[1]);
                inputComponentChoice_AT.ItemsSource = ddList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray();
                inputComponentChoice_AT.Text = ddList.GetDisplayPartOf(ddList.GetIOChoices(inputECUChoice_AT.Text), positiveResultDupel[0]);
            }
            else {
                inputECUChoice_AT.Text = ddList.GetECUChoices().ElementAt(0).secondPart;
                inputComponentChoice_AT.ItemsSource = ddList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray();
                inputComponentChoice_AT.Text = ddList.GetIOChoices(inputECUChoice_AT.Text).ElementAt(0).secondPart;
            }
            FillInputActuatorTestText();

        }
        private void UpdateActuatorTestComboBox() {
            inputECUChoice_AT.ItemsSource = ddList.GetECUChoices().Select(x => x.secondPart).ToArray();
            inputComponentChoice_AT.ItemsSource = ddList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray();
            inputComponentChoice_AT.Text = ddList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray()[0];
        }
        private void InitReadDataDropdowns() {
            inputECUChoice_RDBI.ItemsSource = ddList.GetECUChoices().Select(x => x.secondPart).ToArray();

            if (inputToolChoice.Text == ddList.GetToolChoice()[3]) {
                string[] positiveResultDupel = dataSet.RDID.Split('|');
                if (consol.showMiscGfs) {
                    System.Diagnostics.Debug.WriteLine("PosRes: " + dataSet.RDID + "                                                 ---GfsPage(cs).InitReadDataDropDowns()");
                    System.Diagnostics.Debug.WriteLine("ECU: " + positiveResultDupel[1] + "                                                      ---GfsPage(cs).InitReadDataDropDowns()");
                    System.Diagnostics.Debug.WriteLine("RDID: " + positiveResultDupel[0] + "                                                     ---GfsPage(cs).InitReadDataDropDowns()\n");
                }
                inputECUChoice_RDBI.Text = ddList.GetDisplayPartOf(ddList.GetECUChoices(), positiveResultDupel[1]);
                inputRDBIChoice_RDBI.ItemsSource = ddList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray();
                inputRDBIChoice_RDBI.Text = ddList.GetDisplayPartOf(ddList.GetRDIDChoices(inputECUChoice_RDBI.Text), positiveResultDupel[0]);
                inputPositiveResult_RDBI.Text = dataSet.positiveResult;
                inputReadData.Text = dataSet.RDID;
            }
            else  //Wenn nicht vorhanden, dann zeig das erste Element an.
            {
                inputECUChoice_RDBI.Text = ddList.GetECUChoices().ElementAt(0).secondPart;
                inputRDBIChoice_RDBI.ItemsSource = ddList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray();
                inputRDBIChoice_RDBI.Text = ddList.GetRDIDChoices(inputECUChoice_RDBI.Text).ElementAt(0).secondPart;
            }
        }

        private void UpdateRDBIComboBox() {
            inputECUChoice_RDBI.ItemsSource = ddList.GetECUChoices().Select(x => x.secondPart).ToArray();
            inputRDBIChoice_RDBI.ItemsSource = ddList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray();
            inputRDBIChoice_RDBI.Text = ddList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray()[0];
        }
        private void InitSmartToolDropdowns() {
            inputSmartTool_SM.ItemsSource = ddList.GetSmartToolChoices().Select(x => x.secondPart).ToArray();

            if (inputToolChoice.Text == ddList.GetToolChoice()[2]) {
                inputSmartTool.Text = dataSet.smartTool;
                if (consol.showMiscGfs) System.Diagnostics.Debug.WriteLine("Init SM: " + dataSet.smartTool + "                              ----GfsPage(cs).InitSmartToolDropdowns()");
                if (consol.showMiscGfs) System.Diagnostics.Debug.WriteLine("Init SM: " + inputSmartTool.Text);
                string[] positiveResultDupel = dataSet.smartTool.Split('|');
                inputSmartTool_SM.Text = ddList.GetDisplayPartOf(ddList.GetSmartToolChoices(), positiveResultDupel[0]);
                inputMeasure_SM.ItemsSource = ddList.GetMeasurementChoices(inputSmartTool_SM.Text).Select(x => x.secondPart).ToArray();
                inputMeasure_SM.Text = ddList.GetDisplayPartOf(ddList.GetMeasurementChoices(inputSmartTool_SM.Text), positiveResultDupel[1]);
            }
            else //Wenn nicht vorhanden, dann zeig das erste Element an.
            {
                inputSmartTool_SM.Text = ddList.GetSmartToolChoices().ElementAt(0).secondPart;
                inputMeasure_SM.ItemsSource = ddList.GetMeasurementChoices(inputSmartTool_SM.Text).Select(x => x.secondPart).ToArray();
                inputMeasure_SM.Text = ddList.GetMeasurementChoices(inputSmartTool_SM.Text).ElementAt(0).secondPart;
            }
            FillInputSmartToolText();
            if (dataSet.toolChoice == ddList.GetToolChoice()[2]) {
                if (consol.showMiscGfs) System.Diagnostics.Debug.WriteLine("SmartTool not Epty or false check: >" + inputSmartTool.Text + "<                                 ---GfsPage(cs).FillInputSmartToolText()");
                InitSmartToolLimits();
            }
        }
        private void InitSmartToolLimits() {
            if (dataSet.positiveResult == "" || dataSet.positiveResult == "false") {
                CheckForWhatCaseInSmartToolPositiveResult();
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
            inputSmartTool_SM.ItemsSource = ddList.GetSmartToolChoices().Select(x => x.secondPart).ToArray();
            inputMeasure_SM.ItemsSource = ddList.GetMeasurementChoices(inputSmartTool_SM.Text).Select(x => x.secondPart).ToArray();
            inputMeasure_SM.Text = ddList.GetMeasurementChoices(inputSmartTool_SM.Text).Select(x => x.secondPart).ToArray()[0];
        }

        //Anderes
        private void HideAllItemsWithToggleVisibility() {
            RDBI.Visibility = Visibility.Hidden;
            smartTool.Visibility = Visibility.Hidden;
            actuatorTest.Visibility = Visibility.Hidden;
        }
        private void ShowItemsAfterToolChoice() {
            if (inputToolChoice.Text == ddList.GetToolChoice()[1]) //AT
            {
                HideAllItemsWithToggleVisibility();
                actuatorTest.Visibility = Visibility.Visible;
            }
            else if (inputToolChoice.Text == ddList.GetToolChoice()[2])  //SmT
            {
                HideAllItemsWithToggleVisibility();
                smartTool.Visibility = Visibility.Visible;
            }
            else if (inputToolChoice.Text == ddList.GetToolChoice()[3]) //RDID
            {
                HideAllItemsWithToggleVisibility();
                RDBI.Visibility = Visibility.Visible;
            }
            else {
                HideAllItemsWithToggleVisibility();
            }
        }

        private void CheckForWhatCaseInSmartToolPositiveResult() {
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
        private void FillInputSmartToolText() {
            inputSmartTool.Text = ddList.GetKeyPartOf(ddList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + ddList.GetKeyPartOf(ddList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text);
        }
        private void FillInputReadDataText() {
            inputReadData.Text = ddList.GetKeyPartOf(ddList.GetRDIDChoices(inputECUChoice_RDBI.Text), inputRDBIChoice_RDBI.Text) + "|" + ddList.GetKeyPartOf(ddList.GetECUChoices(), inputECUChoice_RDBI.Text);
        }
        private void FillInputActuatorTestText() {
            inputActuatorTest.Text = ddList.GetKeyPartOf(ddList.GetIOChoices(inputECUChoice_AT.Text), inputComponentChoice_AT.Text) + "|" + ddList.GetKeyPartOf(ddList.GetECUChoices(), inputECUChoice_AT.Text);
        }
        private void CheckForWhatToolHasBeenChosen() {
            if (consol.showMiscGfs) {
                System.Diagnostics.Debug.WriteLine("AT: " + dataSet.actuatorTest + "                                                      GfsPage(cs).CheckForWhatToolHasBeenChosen()");
                System.Diagnostics.Debug.WriteLine("SmT: " + dataSet.smartTool);
                System.Diagnostics.Debug.WriteLine("RDID: " + dataSet.RDID);
                System.Diagnostics.Debug.WriteLine("PosRes: " + dataSet.positiveResult + "\n");
            }

            if (dataSet.actuatorTest != "" && dataSet.actuatorTest != "false") {
                inputToolChoice.Text = ddList.GetToolChoice()[1];
            }
            else if (dataSet.smartTool != "" && dataSet.smartTool != "false") {
                inputToolChoice.Text = ddList.GetToolChoice()[2];
            }
            else if (dataSet.RDID != "" && dataSet.RDID != "false") {
                inputToolChoice.Text = ddList.GetToolChoice()[3];
            }
            else {
                inputToolChoice.Text = language.GetStringPleaseChoose();
            }
        }


    }
}
