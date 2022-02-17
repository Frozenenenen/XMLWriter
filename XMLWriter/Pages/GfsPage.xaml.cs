using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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
            positiveResult = gfsPageHelper.HandleToolChoiceAndResultingPositiveResult(
                ToolChoice_ComboBox,
                ComponentChoice_AT_ComboBox,
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
                ComponentChoice_AT_ComboBox,
                RDIDChoice_RDID_ComboBox,
                SmartTool_SM_ComboBox,
                inputNextStep,
                inputLastStep,
                ToolChoice_ComboBox);
            gfsPageHelper.PrepareNextPage();
            _ = NavigationService.Navigate(new GfsPage());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            positiveResult = gfsPageHelper.HandleToolChoiceAndResultingPositiveResult(
                ToolChoice_ComboBox,
                ComponentChoice_AT_ComboBox,
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
                ComponentChoice_AT_ComboBox,
                RDIDChoice_RDID_ComboBox,
                SmartTool_SM_ComboBox,
                inputNextStep,
                inputLastStep,
                ToolChoice_ComboBox);
            gfsPageHelper.PreparePreviousPage();
            if (guiHelper.IsFirstPage()) {
                _ = NavigationService.Navigate(new StartPage());
            }
            else {
                guiHelper.DecrementSteps();
                _ = NavigationService.Navigate(new GfsPage());
            }
        }
        private void BtnBackDelete_Click(object sender, RoutedEventArgs e) {
            gfsPageHelper.DeleteCurrentSet();
            if (guiHelper.IsFirstPage()) {
                _ = NavigationService.Navigate(new StartPage());
            }
            else {
                _ = NavigationService.Navigate(new GfsPage());
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            positiveResult = gfsPageHelper.HandleToolChoiceAndResultingPositiveResult(
                ToolChoice_ComboBox,
                ComponentChoice_AT_ComboBox,
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
                ComponentChoice_AT_ComboBox,
                RDIDChoice_RDID_ComboBox,
                SmartTool_SM_ComboBox,
                inputNextStep,
                inputLastStep,
                ToolChoice_ComboBox);
            _ = NavigationService.Navigate(new SavePage());
        }
        ///------------------///
        ///---Page Updates---///
        ///------------------///
        private void ToolChoice_DropDownClosed(object sender, System.EventArgs e) {
            gfsPageHelper.ShowItemsAfterToolChoice(ToolChoice_ComboBox, actuatorTest, smartTool, RDID);
        }
        //Aktortest
        private void ECUChoice_AT_DropDownClosed(object sender, System.EventArgs e) {
            gfsInputHelper.FillInputActuatorTestCombinedText(inputECUChoice_AT, ComponentChoice_AT_ComboBox, inputActuatorTest);
            gfsInputHelper.UpdateActuatorTestSecondComboBox(inputECUChoice_AT, ComponentChoice_AT_ComboBox);
        }
        private void ComponentChoice_AT_DropDownClosed(object sender, System.EventArgs e) {
            gfsInputHelper.FillInputActuatorTestCombinedText(inputECUChoice_AT, ComponentChoice_AT_ComboBox, inputActuatorTest);
        }
        //RDID
        private void ECUChoice_RDID_DropDownClosed(object sender, System.EventArgs e) {
            gfsInputHelper.FillInputReadDataCombinedText(ReadData_TextBox, ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
            gfsInputHelper.UpdateRDIDSecondComboBox(ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
        }
        private void RDIDChoice_RDID_DropDownClosed(object sender, System.EventArgs e) {
            gfsInputHelper.FillInputReadDataCombinedText(ReadData_TextBox, ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
        }
        //SmartTool
        private void SmartTool_SM_DropDownClosed(object sender, System.EventArgs e) {
            gfsInputHelper.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            gfsInputHelper.UpdateSmartToolSecondComboboBox(SmartTool_SM_ComboBox, Measure_SM_ComboBox);
        }
        private void Measure_SM_DropDownClosed(object sender, System.EventArgs e) {
            gfsInputHelper.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
        }
        private void Measure_SM_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            gfsInputHelper.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
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
            gfsPageHelper.InitActuatorTestDropdowns(inputECUChoice_AT, ToolChoice_ComboBox, ComponentChoice_AT_ComboBox, inputActuatorTest);
            gfsPageHelper.InitSmartToolDropdowns(SmartTool_SM_ComboBox, ToolChoice_ComboBox, SmartTool_TextBox, Measure_SM_ComboBox, PositiveResult_SM_TextBox, inputPositiveResult_LowerLimit, inputPositiveResult_UpperLimit);
            gfsPageHelper.InitReadDataDropdowns(ToolChoice_ComboBox, ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox, PositiveResult_RDID_TextBox, ReadData_TextBox);
            gfsPageHelper.ShowItemsAfterToolChoice(ToolChoice_ComboBox, actuatorTest, smartTool, RDID);
        }




    }
}
