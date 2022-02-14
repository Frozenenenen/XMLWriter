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
        GfsPageHelper gfsPageHelper = new GfsPageHelper();
        GfsPageTextlHelper textHelper = new GfsPageTextlHelper();
        GUIMovementHelper guiHelper = new GUIMovementHelper();
        GfsPageInputHelper inputHelper = new GfsPageInputHelper();

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
            inputHelper.FillInputActuatorTestCombinedText(inputECUChoice_AT, ComponentChoice_AT_ComboBox, inputActuatorTest);
            inputHelper.UpdateActuatorTestSecondComboBox(inputECUChoice_AT, ComponentChoice_AT_ComboBox);
        }
        private void ComponentChoice_AT_DropDownClosed(object sender, System.EventArgs e) {
            inputHelper.FillInputActuatorTestCombinedText(inputECUChoice_AT, ComponentChoice_AT_ComboBox, inputActuatorTest);
        }
        //RDID
        private void ECUChoice_RDID_DropDownClosed(object sender, System.EventArgs e) {
            inputHelper.FillInputReadDataCombinedText(ReadData_TextBox, ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
            inputHelper.UpdateRDIDSecondComboBox(ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
        }
        private void RDIDChoice_RDID_DropDownClosed(object sender, System.EventArgs e) {
            inputHelper.FillInputReadDataCombinedText(ReadData_TextBox, ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
        }
        //SmartTool
        private void SmartTool_SM_DropDownClosed(object sender, System.EventArgs e) {
            inputHelper.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            inputHelper.UpdateSmartToolSecondComboboBox(SmartTool_SM_ComboBox, Measure_SM_ComboBox);
        }
        private void Measure_SM_DropDownClosed(object sender, System.EventArgs e) {
            inputHelper.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
        }
        private void Measure_SM_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            inputHelper.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            /*if (gfsPageHelper.IsEmptyMeasure(SmartTool_SM_ComboBox, Measure_SM_ComboBox)) {
                inputHelper.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox); //Wenn OutOfElemets fehler wieder SetSmartToolCombinedValue(); einsetzen
            }
            else {
                inputHelper.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            }*/
        }
        private void PositiveResult_UpperLimit_TextChanged(object sender, TextChangedEventArgs e) {
            inputHelper.WritePositiveResultDependingOnLowerAndUpperLimit(PositiveResult_SM_TextBox, inputPositiveResult_UpperLimit, inputPositiveResult_LowerLimit);
        }
        private void PositiveResult_LowerLimit_TextChanged(object sender, TextChangedEventArgs e) {
            inputHelper.WritePositiveResultDependingOnLowerAndUpperLimit(PositiveResult_SM_TextBox, inputPositiveResult_UpperLimit, inputPositiveResult_LowerLimit);
        }

        ///-----------///
        ///---Inits---///
        ///-----------///
        private void InitTextItems() {

            //Inhalte linke Spalte
            textHelper.SetLabelStepName(textStep);
            textHelper.SetLabelContent(textContentTitel);
            textHelper.SetLabelAnimation(textAnimTitel);
            textHelper.SetLabelInstruction(textInstructionTitel);
            textHelper.SetLabelTitel(textTitel);

            //Inhalte rechte Spalte
            textHelper.SetLabelPositiveID(textPositiveID);
            textHelper.SetLabelNegativeID(textNegativeID);
            textHelper.SetLabelPositiveResult(textPositiveResult_SM);
            textHelper.SetLabelPositiveResult(textPositiveResult_RDID);
            textHelper.SetLabelRepXML(textRepXML);
            textHelper.SetLabelActuatorTesst(textActuatorTest);
            textHelper.SetLabelRDID(textReadData);
            textHelper.SetLabelSmartTool(textSmartTool);
            //CheckBoxText
            textHelper.SetTextNextStep(inputNextStep);
            textHelper.SetTextLastStep(inputLastStep);

            //Buttons
            textHelper.SetButtonNext(btnNext);
            textHelper.SetButtonDelete(btnBackDelete);
            textHelper.SetButtonSave(btnSave);
            textHelper.SetButtonBack(btnBack);
        }
        private void InitValueItems() {
            InitLeftSideItems();
            InitFixedRightSideItems();
            InitFlexRightSideItems();
        }
        private void InitLeftSideItems() {
            inputHelper.InitStepNameValue(inputStepName);
            inputHelper.InitTextValue(inputText);
            inputHelper.InitAnimValue(inputAnim);
            inputHelper.InitInstructionValue(inputInstruction);
        }
        private void InitFixedRightSideItems() {
            inputHelper.InitPositiveID(inputPositiveID);
            inputHelper.InitNegativeID(inputNegativeID);
            inputHelper.InitRepXMLValue(inputRepXML);
            inputHelper.InitNextStepValue(inputNextStep);
            inputHelper.InitLastStepValue(inputLastStep);
            inputHelper.InitToolChoiceDropDown(ToolChoice_ComboBox);

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
