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
        DataSetService dataSetService = new DataSetService();
        GfsPageHelper gfs = new GfsPageHelper();
        Language language = new Language();
        GUIMovementHelper guiHelper = new GUIMovementHelper();
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
            positiveResult = gfs.handleToolChoiceAndResultingPositiveResult(
                inputToolChoice, 
                inputComponentChoice_AT, 
                RDBIChoice_RDBI_ComboBox, 
                SmartTool_SM_ComboBox, 
                inputPositiveResult_RDBI, 
                inputPositiveResult_SM, 
                positiveResult);
            gfs.SaveCurrentInput(
                inputStepName, 
                inputText, 
                inputAnim, 
                inputInstruction, 
                inputPositiveID, 
                inputNegativeID, 
                positiveResult, 
                inputRepXML, 
                inputComponentChoice_AT, 
                RDBIChoice_RDBI_ComboBox, 
                SmartTool_SM_ComboBox, 
                inputNextStep, 
                inputLastStep, 
                inputToolChoice);
            gfs.PrepareNextPage();
            _ = NavigationService.Navigate(new GfsPage());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            positiveResult = gfs.handleToolChoiceAndResultingPositiveResult(
                inputToolChoice, 
                inputComponentChoice_AT, 
                RDBIChoice_RDBI_ComboBox, 
                SmartTool_SM_ComboBox, 
                inputPositiveResult_RDBI, 
                inputPositiveResult_SM, 
                positiveResult);
            gfs.SaveCurrentInput(
                inputStepName,
                inputText,
                inputAnim,
                inputInstruction,
                inputPositiveID,
                inputNegativeID,
                positiveResult,
                inputRepXML,
                inputComponentChoice_AT,
                RDBIChoice_RDBI_ComboBox,
                SmartTool_SM_ComboBox,
                inputNextStep,
                inputLastStep,
                inputToolChoice);
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
            positiveResult = gfs.handleToolChoiceAndResultingPositiveResult(
                inputToolChoice,
                inputComponentChoice_AT,
                RDBIChoice_RDBI_ComboBox,
                SmartTool_SM_ComboBox,
                inputPositiveResult_RDBI,
                inputPositiveResult_SM,
                positiveResult);
            gfs.SaveCurrentInput(
                inputStepName,
                inputText,
                inputAnim,
                inputInstruction,
                inputPositiveID,
                inputNegativeID,
                positiveResult,
                inputRepXML,
                inputComponentChoice_AT,
                RDBIChoice_RDBI_ComboBox,
                SmartTool_SM_ComboBox,
                inputNextStep,
                inputLastStep,
                inputToolChoice);
            _ = NavigationService.Navigate(new SavePage());
        }

        //Comboboxes and Texboxes
        private void inputToolChoice_DropDownClosed(object sender, System.EventArgs e) {
            gfs.ShowItemsAfterToolChoice(inputToolChoice, actuatorTest, smartTool, RDID);
        }
        //Aktortest
        private void inputECUChoice_AT_DropDownClosed(object sender, System.EventArgs e) {
            gfs.FillInputActuatorTestText(inputECUChoice_AT, inputToolChoice, inputComponentChoice_AT, inputActuatorTest);
            UpdateActuatorTestComboBox();
        }
        private void inputComponentChoice_AT_DropDownClosed(object sender, System.EventArgs e) {
            gfs.FillInputActuatorTestText(inputECUChoice_AT, inputToolChoice, inputComponentChoice_AT, inputActuatorTest);
        }
        //RDBI
        private void inputECUChoice_RDBI_DropDownClosed(object sender, System.EventArgs e) {
            gfs.FillInputReadDataCombinedText(ReadData_TextBox, ECUChoice_RDBI_ComboBox, RDBIChoice_RDBI_ComboBox);
            UpdateRDBIComboBox();
        }
        private void inputRDBIChoice_RDBI_DropDownClosed(object sender, System.EventArgs e) {
            gfs.FillInputReadDataCombinedText(ReadData_TextBox, ECUChoice_RDBI_ComboBox, RDBIChoice_RDBI_ComboBox);
        }
        //SmartTool
        private void inputSmartTool_SM_DropDownClosed(object sender, System.EventArgs e) {
            gfs.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            gfs.UpdateSmartToolComboboBox(SmartTool_SM_ComboBox, Measure_SM_ComboBox);
        }
        private void inputMeasure_SM_DropDownClosed(object sender, System.EventArgs e) {
            gfs.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
        }
        private void inputMeasure_SM_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (dropDownList.GetKeyPartOf(dropDownList.GetMeasurementChoices(SmartTool_SM_ComboBox.Text), Measure_SM_ComboBox.Text) == "") //Is empty if FindIndex is 0. To get direct input ... well it has to be direct^^
            {
                gfs.SetSmartToolCombinedValue(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            }
            else {
                gfs.FillInputSmartToolCombinedText(SmartTool_TextBox, SmartTool_SM_ComboBox, Measure_SM_ComboBox);
            }
        }
        private void inputPositiveResult_UpperLimit_TextChanged(object sender, TextChangedEventArgs e) {
            gfs.CheckForWhatCaseInSmartToolPositiveResult(inputPositiveResult_SM, inputPositiveResult_UpperLimit, inputPositiveResult_LowerLimit);
        }
        private void inputPositiveResult_LowerLimit_TextChanged(object sender, TextChangedEventArgs e) {
            gfs.CheckForWhatCaseInSmartToolPositiveResult(inputPositiveResult_SM, inputPositiveResult_UpperLimit, inputPositiveResult_LowerLimit);
        }

        //Inits
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
            gfs.SetLabelPositiveResult(textPositiveResult_RDBI);
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
            gfs.ShowItemsAfterToolChoice(inputToolChoice, actuatorTest, smartTool, RDID);
        }
        private void InitLeftSideItems() {
            gfs.SetStepNameValue(inputStepName);
            gfs.SetTextValue(inputText);
            gfs.SetAnimValue(inputAnim);
            gfs.SetInstructionValue(inputInstruction);
        } //fertig
        private void InitFixedRightSideItems() {
            gfs.SetPositiveID(inputPositiveID);
            gfs.SetNegativeID(inputNegativeID);
            gfs.SetRepXMLValue(inputRepXML);
            //Check boxes
            gfs.SetNextStepValue(inputNextStep);
            gfs.SetLastStepValue(inputLastStep);
            
        } //fertig
        //Inits erste Dropdown Ebene
        private void InitFlexRightSideItems() {
            gfs.CheckForWhatToolHasBeenChosen(inputToolChoice);
            inputToolChoice.ItemsSource = dropDownList.GetToolChoice();
            xamlHelper.SetDropDownActiveELementFor(inputToolChoice, dataSetService.toolChoice);
            gfs.InitActuatorTestDropdowns(inputECUChoice_AT, inputToolChoice, inputComponentChoice_AT, inputActuatorTest);
            gfs.InitSmartToolDropdowns(SmartTool_SM_ComboBox, inputToolChoice, SmartTool_TextBox, Measure_SM_ComboBox, inputPositiveResult_SM, inputPositiveResult_LowerLimit, inputPositiveResult_UpperLimit);
            gfs.InitReadDataDropdowns(inputToolChoice, ECUChoice_RDBI_ComboBox, RDBIChoice_RDBI_ComboBox, inputPositiveResult_RDBI, ReadData_TextBox);
        }

        //Inits zweite und dritte Dropdown Eebene
       
        private void UpdateActuatorTestComboBox() {
            inputECUChoice_AT.ItemsSource = dropDownList.GetECUChoices().Select(x => x.secondPart).ToArray();
            inputComponentChoice_AT.ItemsSource = dropDownList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray();
            inputComponentChoice_AT.Text = dropDownList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray()[0];
        }
        private void UpdateRDBIComboBox() {
            ECUChoice_RDBI_ComboBox.ItemsSource = dropDownList.GetECUChoices().Select(x => x.secondPart).ToArray();
            RDBIChoice_RDBI_ComboBox.ItemsSource = dropDownList.GetRDIDChoices(ECUChoice_RDBI_ComboBox.Text).Select(x => x.secondPart).ToArray();
            RDBIChoice_RDBI_ComboBox.Text = dropDownList.GetRDIDChoices(ECUChoice_RDBI_ComboBox.Text).Select(x => x.secondPart).ToArray()[0];
        }
        
        
        
    }
}
