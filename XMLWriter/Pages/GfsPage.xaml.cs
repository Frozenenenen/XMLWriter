using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using XMLWriter.Classes;
using XMLWriter.Classes.HelpClasses;

namespace XMLWriter.Pages {
    /// <summary>
    /// Interaktionslogik für GfsPage.xaml
    /// </summary>
    public partial class GfsPage : Page {
        GfsPageHelper gfsPageHelper = new GfsPageHelper();
        GfsPageTextHelper gfsTextHelper = new GfsPageTextHelper();
        GUIMovementHelper guiHelper = new GUIMovementHelper();
        GfsPageInputHelper gfsInputHelper = new GfsPageInputHelper();
        DataSetService dataSets = new DataSetService();

        private static string positiveResult;

        public GfsPage() {
            InitializeComponent();
            InitTextItems();
            InitValueItems();
        }
        ///-------------------------///
        ///---guiMovement buttons---///
        ///-------------------------///
        private void BtnNext_Click(object sender, RoutedEventArgs e) {
            WriteDataSetsToConsole();
            positiveResult = gfsPageHelper.HandleToolChoiceAndResultingPositiveResult(
                ToolChoice_ComboBox,
                inputComponentChoice_AT_ComboBox,
                RDIDChoice_RDID_ComboBox,
                SmartTool_SM_ComboBox,
                PositiveResult_RDID_TextBox,
                PositiveResult_SM_TextBox);
            gfsPageHelper.SaveCurrentInput(
                inputStepName,
                inputText,
                inputAnim,
                inputInstruction,
                inputPositiveID,
                inputNegativeID,
                positiveResult,
                inputRepXML,
                inputActuatorTest_TextBox,
                inputReadData_TextBox,
                inputSmartTool_TextBox,
                inputNextStep,
                inputLastStep,
                ToolChoice_ComboBox);
            gfsPageHelper.PrepareNextPage();
            _ = NavigationService.Navigate(new GfsPage());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            WriteDataSetsToConsole();
            positiveResult = gfsPageHelper.HandleToolChoiceAndResultingPositiveResult(
                ToolChoice_ComboBox,
                inputComponentChoice_AT_ComboBox,
                RDIDChoice_RDID_ComboBox,
                SmartTool_SM_ComboBox,
                PositiveResult_RDID_TextBox,
                PositiveResult_SM_TextBox);
            gfsPageHelper.SaveCurrentInput(
                inputStepName,
                inputText,
                inputAnim,
                inputInstruction,
                inputPositiveID,
                inputNegativeID,
                positiveResult,
                inputRepXML,
                inputActuatorTest_TextBox,
                inputReadData_TextBox,
                inputSmartTool_TextBox,
                inputNextStep,
                inputLastStep,
                ToolChoice_ComboBox);
            
            if (guiHelper.IsFirstPage()) {
                _ = NavigationService.Navigate(new StartPage());
            }
            else {
                gfsPageHelper.PreparePreviousPage();
                _ = NavigationService.Navigate(new GfsPage());
            }
        }
        private void BtnBackDelete_Click(object sender, RoutedEventArgs e) {
            WriteDataSetsToConsole();
            if (guiHelper.IsFirstPage()) {
                gfsPageHelper.DeleteCurrentSet();
                _ = NavigationService.Navigate(new StartPage());
            }
            else {
                gfsPageHelper.DeleteCurrentSet();
                _ = NavigationService.Navigate(new GfsPage());
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            WriteDataSetsToConsole();
            positiveResult = gfsPageHelper.HandleToolChoiceAndResultingPositiveResult(
                ToolChoice_ComboBox,
                inputComponentChoice_AT_ComboBox,
                RDIDChoice_RDID_ComboBox,
                SmartTool_SM_ComboBox,
                PositiveResult_RDID_TextBox,
                PositiveResult_SM_TextBox);
            gfsPageHelper.SaveCurrentInput(
                inputStepName,
                inputText,
                inputAnim,
                inputInstruction,
                inputPositiveID,
                inputNegativeID,
                positiveResult,
                inputRepXML,
                inputActuatorTest_TextBox,
                inputReadData_TextBox,
                inputSmartTool_TextBox,
                inputNextStep,
                inputLastStep,
                ToolChoice_ComboBox);
            _ = NavigationService.Navigate(new SavePage());
        }
        private void BtnInsert_Click(object sender, RoutedEventArgs e) {
            WriteDataSetsToConsole();
            gfsPageHelper.InsertNewSet();
            _ = NavigationService.Navigate(new GfsPage());
        }
        ///------------------///
        ///---Page Updates---///
        ///------------------///
        private void ToolChoice_DropDownClosed(object sender, System.EventArgs e) {
            gfsPageHelper.ShowItemsAfterToolChoice(ToolChoice_ComboBox, actuatorTest, smartTool, RDID);
        }
        //Aktortest
        private void ECUChoice_AT_DropDownClosed(object sender, System.EventArgs e) {
            gfsInputHelper.FillInputActuatorTestCombinedText(inputActuatorTest_TextBox, inputECUChoice_AT, inputComponentChoice_AT_ComboBox);
            gfsInputHelper.UpdateActuatorTestSecondComboBox(inputECUChoice_AT, inputComponentChoice_AT_ComboBox);
        }
        private void ComponentChoice_AT_DropDownClosed(object sender, System.EventArgs e) {
            gfsInputHelper.FillInputActuatorTestCombinedText(inputActuatorTest_TextBox, inputECUChoice_AT, inputComponentChoice_AT_ComboBox);
        }
        //RDID
        private void ECUChoice_RDID_DropDownClosed(object sender, System.EventArgs e) {
            gfsInputHelper.FillInputReadDataCombinedText(inputReadData_TextBox, ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
            gfsInputHelper.UpdateRDIDSecondComboBox(ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
        }
        private void RDIDChoice_RDID_DropDownClosed(object sender, System.EventArgs e) {
            gfsInputHelper.FillInputReadDataCombinedText(inputReadData_TextBox, ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
        }
        //SmartTool
        private void SmartTool_SM_DropDownClosed(object sender, System.EventArgs e) {
            gfsInputHelper.FillInputSmartToolCombinedText(inputSmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            gfsInputHelper.UpdateSmartToolSecondComboboBox(SmartTool_SM_ComboBox, Measure_SM_ComboBox);
        }
        private void Measure_SM_DropDownClosed(object sender, System.EventArgs e) {
            gfsInputHelper.FillInputSmartToolCombinedText(inputSmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
        }
        private void Measure_SM_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            gfsInputHelper.FillInputSmartToolCombinedText(inputSmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            /*if (gfsPageHelper.IsEmptyMeasure(SmartTool_SM_ComboBox, Measure_SM_ComboBox)) {
                inputHelper.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox); //Wenn OutOfElemets fehler wieder SetSmartToolCombinedValue(); einsetzen
            }
            else {
                inputHelper.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            }*/
        }
        private void PositiveResult_UpperLimit_TextChanged(object sender, TextChangedEventArgs e) {
            gfsInputHelper.WritePositiveResultDependingOnLowerAndUpperLimit(PositiveResult_SM_TextBox, inputPositiveResult_UpperLimit, inputPositiveResult_LowerLimit);
        }
        private void PositiveResult_LowerLimit_TextChanged(object sender, TextChangedEventArgs e) {
            gfsInputHelper.WritePositiveResultDependingOnLowerAndUpperLimit(PositiveResult_SM_TextBox, inputPositiveResult_UpperLimit, inputPositiveResult_LowerLimit);
        }

        ///-----------///
        ///---Inits---///
        ///-----------///
        private void InitTextItems() {

            //Inhalte linke Spalte
            gfsTextHelper.SetLabelStepName(textStep);
            gfsTextHelper.SetLabelContent(textContentTitel);
            gfsTextHelper.SetLabelAnimation(textAnimTitel);
            gfsTextHelper.SetLabelInstruction(textInstructionTitel);
            gfsTextHelper.SetLabelTitel(textTitel);

            //Inhalte rechte Spalte
            gfsTextHelper.SetLabelPositiveID(textPositiveID);
            gfsTextHelper.SetLabelNegativeID(textNegativeID);
            gfsTextHelper.SetLabelPositiveResult(textPositiveResult_SM);
            gfsTextHelper.SetLabelPositiveResult(textPositiveResult_RDID);
            gfsTextHelper.SetLabelRepXML(textRepXML);
            gfsTextHelper.SetLabelActuatorTesst(textActuatorTest);
            gfsTextHelper.SetLabelRDID(textReadData);
            gfsTextHelper.SetLabelSmartTool(textSmartTool);
            //CheckBoxText
            gfsTextHelper.SetTextNextStep(inputNextStep);
            gfsTextHelper.SetTextLastStep(inputLastStep);

            //Buttons
            gfsTextHelper.SetButtonNext(btnNext);
            gfsTextHelper.SetButtonDelete(btnBackDelete);
            gfsTextHelper.SetButtonSave(btnSave);
            gfsTextHelper.SetButtonBack(btnBack);
            gfsTextHelper.SetButtonInsert(btnInsert);
        }
        private void InitValueItems() {
            InitLeftSideItems();
            InitFixedRightSideItems();
            InitFlexRightSideItems();
        }
        private void InitLeftSideItems() {
            gfsInputHelper.InitStepNameValue(inputStepName);
            gfsInputHelper.InitTextValue(inputText);
            gfsInputHelper.InitAnimValue(inputAnim);
            gfsInputHelper.InitInstructionValue(inputInstruction);
        }
        private void InitFixedRightSideItems() {
            gfsInputHelper.InitPositiveID_DD(inputPositiveID);
            gfsInputHelper.InitNegativeID_DD(inputNegativeID);
            gfsInputHelper.InitRepXMLValue(inputRepXML);
            gfsInputHelper.InitNextStepValue(inputNextStep);
            gfsInputHelper.InitLastStepValue(inputLastStep);
            gfsInputHelper.InitToolChoiceDropDown(ToolChoice_ComboBox);

        }
        private void InitFlexRightSideItems() {
            gfsPageHelper.CheckForWhatToolHasBeenChosen(ToolChoice_ComboBox);
            gfsPageHelper.InitActuatorTestDropdowns(inputECUChoice_AT, ToolChoice_ComboBox, inputComponentChoice_AT_ComboBox, inputActuatorTest_TextBox);
            gfsPageHelper.InitSmartToolDropdowns(SmartTool_SM_ComboBox, ToolChoice_ComboBox, inputSmartTool_TextBox, Measure_SM_ComboBox, PositiveResult_SM_TextBox, inputPositiveResult_LowerLimit, inputPositiveResult_UpperLimit);
            gfsPageHelper.InitReadDataDropdowns(ToolChoice_ComboBox, ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox, PositiveResult_RDID_TextBox, inputReadData_TextBox);
            gfsPageHelper.ShowItemsAfterToolChoice(ToolChoice_ComboBox, actuatorTest, smartTool, RDID);
        }

        public void WriteDataSetsToConsole() {
            for (int i = 0; i < dataSets.GetDataSets().Count ; i++) {
                System.Diagnostics.Debug.WriteLine("Index: " + i);
                System.Diagnostics.Debug.WriteLine("Step: " + dataSets.GetDataSets().ElementAt(i).stepName);
                System.Diagnostics.Debug.WriteLine("Text: " + dataSets.GetDataSets().ElementAt(i).text);
                System.Diagnostics.Debug.WriteLine("Anim: " + dataSets.GetDataSets().ElementAt(i).anim);
                //System.Diagnostics.Debug.WriteLine("Spec: " + dataSets.GetDataSets().ElementAt(i).specialText);
                System.Diagnostics.Debug.WriteLine("Instr: " + dataSets.GetDataSets().ElementAt(i).instruction);
                System.Diagnostics.Debug.WriteLine("posID: " + dataSets.GetDataSets().ElementAt(i).positiveID);
                System.Diagnostics.Debug.WriteLine("negID: " + dataSets.GetDataSets().ElementAt(i).negativeID);
                System.Diagnostics.Debug.WriteLine("PosRe: " + dataSets.GetDataSets().ElementAt(i).positiveResult);
                System.Diagnostics.Debug.WriteLine("rXML " + dataSets.GetDataSets().ElementAt(i).repXML);
                System.Diagnostics.Debug.WriteLine("AcTe: " + dataSets.GetDataSets().ElementAt(i).actuatorTest);
                System.Diagnostics.Debug.WriteLine("RDID: " + dataSets.GetDataSets().ElementAt(i).RDID);
                System.Diagnostics.Debug.WriteLine("SmT: " + dataSets.GetDataSets().ElementAt(i).smartTool);
                System.Diagnostics.Debug.WriteLine("Next: " + dataSets.GetDataSets().ElementAt(i).nextStep);
                System.Diagnostics.Debug.WriteLine("Last: " + dataSets.GetDataSets().ElementAt(i).lastStep);
                System.Diagnostics.Debug.WriteLine("Tool: " + dataSets.GetDataSets().ElementAt(i).toolChoice);
                System.Diagnostics.Debug.WriteLine("\n\n");
            }
        }
    }
}
