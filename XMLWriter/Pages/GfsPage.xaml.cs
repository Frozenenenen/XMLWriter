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
        GfsPageHelper gfs = new GfsPageHelper();
        GUIMovementHelper guiHelper = new GUIMovementHelper();

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
            positiveResult = gfs.HandleToolChoiceAndResultingPositiveResult(
                ToolChoice_ComboBox, 
                ComponentChoice_AT_ComboBox, 
                RDIDChoice_RDID_ComboBox, 
                SmartTool_SM_ComboBox, 
                PositiveResult_RDID_TextBox, 
                PositiveResult_SM_TextBox);
            gfs.SaveCurrentInput(
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
            gfs.PrepareNextPage();
            _ = NavigationService.Navigate(new GfsPage());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            positiveResult = gfs.HandleToolChoiceAndResultingPositiveResult(
                ToolChoice_ComboBox, 
                ComponentChoice_AT_ComboBox, 
                RDIDChoice_RDID_ComboBox, 
                SmartTool_SM_ComboBox, 
                PositiveResult_RDID_TextBox, 
                PositiveResult_SM_TextBox);
            gfs.SaveCurrentInput(
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
            gfs.PreparePreviousPage();
            if (guiHelper.IsFirstPage()) {
                _ = NavigationService.Navigate(new StartPage());
            }
            else {
                guiHelper.DecrementSteps();
                _ = NavigationService.Navigate(new GfsPage());
            }
        }
        private void BtnBackDelete_Click(object sender, RoutedEventArgs e) {
            gfs.DeleteCurrentSet();
            if (guiHelper.IsFirstPage()) {
                _ = NavigationService.Navigate(new StartPage());
            }
            else {
                _ = NavigationService.Navigate(new GfsPage());
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            positiveResult = gfs.HandleToolChoiceAndResultingPositiveResult(
                ToolChoice_ComboBox,
                ComponentChoice_AT_ComboBox,
                RDIDChoice_RDID_ComboBox,
                SmartTool_SM_ComboBox,
                PositiveResult_RDID_TextBox,
                PositiveResult_SM_TextBox);
            gfs.SaveCurrentInput(
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
            gfs.ShowItemsAfterToolChoice(ToolChoice_ComboBox, actuatorTest, smartTool, RDID);
        }
        //Aktortest
        private void ECUChoice_AT_DropDownClosed(object sender, System.EventArgs e) {
            gfs.FillInputActuatorTestCombinedText(inputECUChoice_AT, ComponentChoice_AT_ComboBox, inputActuatorTest);
            gfs.UpdateActuatorTestSecondComboBox(inputECUChoice_AT, ComponentChoice_AT_ComboBox);
        }
        private void ComponentChoice_AT_DropDownClosed(object sender, System.EventArgs e) {
            gfs.FillInputActuatorTestCombinedText(inputECUChoice_AT, ComponentChoice_AT_ComboBox, inputActuatorTest);
        }
        //RDID
        private void ECUChoice_RDID_DropDownClosed(object sender, System.EventArgs e) {
            gfs.FillInputReadDataCombinedText(ReadData_TextBox, ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
            gfs.UpdateRDIDSecondComboBox(ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
        }
        private void RDIDChoice_RDID_DropDownClosed(object sender, System.EventArgs e) {
            gfs.FillInputReadDataCombinedText(ReadData_TextBox, ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox);
        }
        //SmartTool
        private void SmartTool_SM_DropDownClosed(object sender, System.EventArgs e) {
            gfs.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            gfs.UpdateSmartToolSecondComboboBox(SmartTool_SM_ComboBox, Measure_SM_ComboBox);
        }
        private void Measure_SM_DropDownClosed(object sender, System.EventArgs e) {
            gfs.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
        }
        private void Measure_SM_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if(gfs.IsEmptyMeasure(SmartTool_SM_ComboBox, Measure_SM_ComboBox))
            {
                gfs.SetSmartToolCombinedValue(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            }
            else {
                gfs.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            }
        }
        private void PositiveResult_UpperLimit_TextChanged(object sender, TextChangedEventArgs e) {
            gfs.WritePositiveResultDependingOnLowerAndUpperLimit(PositiveResult_SM_TextBox, inputPositiveResult_UpperLimit, inputPositiveResult_LowerLimit);
        }
        private void PositiveResult_LowerLimit_TextChanged(object sender, TextChangedEventArgs e) {
            gfs.WritePositiveResultDependingOnLowerAndUpperLimit(PositiveResult_SM_TextBox, inputPositiveResult_UpperLimit, inputPositiveResult_LowerLimit);
        }
        
        ///-----------///
        ///---Inits---///
        ///-----------///
        private void InitTextItems() {

            //Inhalte linke Spalte
            gfs.SetLabelStepName(textStep);
            gfs.SetLabelContent(textContentTitel);
            gfs.SetLabelAnimation(textAnimTitel);
            gfs.SetLabelInstruction(textInstructionTitel);
            gfs.SetLabelTitel(textTitel);

            //Inhalte rechte Spalte
            gfs.SetLabelPositiveID(textPositiveID);
            gfs.SetLabelNegativeID(textNegativeID);
            gfs.SetLabelPositiveResult(textPositiveResult_SM);
            gfs.SetLabelPositiveResult(textPositiveResult_RDID);
            gfs.SetLabelRepXML(textRepXML);
            gfs.SetLabelActuatorTesst(textActuatorTest);
            gfs.SetLabelRDID(textReadData);
            gfs.SetLabelSmartTool(textSmartTool);
            gfs.SetLabelSmartTool(textSmartTool);
            //CheckBoxText
            gfs.SetTextNextStep(inputNextStep);
            gfs.SetTextLastStep(inputLastStep);

            //Buttons
            gfs.SetButtonNext(btnNext);
            gfs.SetButtonDelete(btnBackDelete);
            gfs.SetButtonSave(btnSave);
            gfs.SetButtonBack(btnBack);
        }
        private void InitValueItems() { 
            InitLeftSideItems();
            InitFixedRightSideItems();
            InitFlexRightSideItems();
        }
        private void InitLeftSideItems() {
            gfs.SetStepNameValue(inputStepName);
            gfs.SetTextValue(inputText);
            gfs.SetAnimValue(inputAnim);
            gfs.SetInstructionValue(inputInstruction);
        }
        private void InitFixedRightSideItems() {
            gfs.SetPositiveID(inputPositiveID);
            gfs.SetNegativeID(inputNegativeID);
            gfs.SetRepXMLValue(inputRepXML);
            gfs.SetNextStepValue(inputNextStep);
            gfs.SetLastStepValue(inputLastStep);
            
        } 
        private void InitFlexRightSideItems() {
            gfs.CheckForWhatToolHasBeenChosen(ToolChoice_ComboBox);
            gfs.InitToolChoiceDropDown(ToolChoice_ComboBox);
            gfs.InitActuatorTestDropdowns(inputECUChoice_AT, ToolChoice_ComboBox, ComponentChoice_AT_ComboBox, inputActuatorTest);
            gfs.InitSmartToolDropdowns(SmartTool_SM_ComboBox, ToolChoice_ComboBox, SmartTool_TextBox, Measure_SM_ComboBox, PositiveResult_SM_TextBox, inputPositiveResult_LowerLimit, inputPositiveResult_UpperLimit);
            gfs.InitReadDataDropdowns(ToolChoice_ComboBox, ECUChoice_RDID_ComboBox, RDIDChoice_RDID_ComboBox, PositiveResult_RDID_TextBox, ReadData_TextBox);
            gfs.ShowItemsAfterToolChoice(ToolChoice_ComboBox, actuatorTest, smartTool, RDID);
        }




    }
}
