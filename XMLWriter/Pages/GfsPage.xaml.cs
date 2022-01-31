using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Linq;
using XMLWriter.Classes;

namespace XMLWriter.Pages
{
    /// <summary>
    /// Interaktionslogik für GfsPage.xaml
    /// </summary>
    public partial class GfsPage : Page
    {
        DataSet data = new DataSet();
        Language language = new Language();
        GUIMovement GUI = new GUIMovement();
        DropDownOptionLists ddList = new DropDownOptionLists();
        private static string positiveResult;

        public GfsPage()
        {
            InitializeComponent();
            data.InitNewDataSet();
            InitTextItems();
            InitValueItems();
        }

        //buttons
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            SaveStep();
            GUI.IncrementSteps();

            _ = NavigationService.Navigate(new GfsPage());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (data.GetStepCount() == 0)
            {
                _ = NavigationService.Navigate(new StartPage());
                data.ResetDataSet();
            }
            else
            {
                SaveStep();
                GUI.DecrementSteps();
                _ = NavigationService.Navigate(new GfsPage());
            }
        }
        private void BtnBackDelete_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            GUIMovement GUI = new GUIMovement();

            if (data.GetStepCount() == 0)
            {

                _ = NavigationService.Navigate(new StartPage());
                data.ResetDataSet();
            }
            else
            {
                GUI.DecrementStepsMax();
                _ = NavigationService.Navigate(new GfsPage());
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            GUIMovement GUI = new GUIMovement();
            SaveStep();
            GUI.IncrementSteps();
            GUI.DecrementStepsForSaving(); //Entweder ich mach ne extra Funktion für die letzte Dateneingabe oder ich in- und decrementiere direkt nacheinander. i++ i--. Anonsten hab ich beim zurückgehen Probleme^^

            _ = NavigationService.Navigate(new SavePage());
        }
        private void SaveStep()
        {
            DifferntiatePositiveResultSourceBeforeSaving();
            data.SaveSet(inputToolChoice.Text, inputStepName.Text, inputText.Text, inputAnim.Text, inputInstruction.Text, inputPositiveID.Text, inputNegativeID.Text, positiveResult,
                            inputRepXML.Text, inputActuatorTest.Text, inputReadData.Text, inputSmartTool.Text, inputNextStep.IsChecked, inputLastStep.IsChecked);
        }

        //Comboboxes and Texboxes
        private void inputToolChoice_DropDownClosed(object sender, System.EventArgs e)
        {
            ShowItemsAfterToolChoice();
        }
        //Aktortest
        private void inputECUChoice_AT_DropDownClosed(object sender, System.EventArgs e)
        {
            FillInputActuatorTestText();
            UpdateComponentComboBox();
        }
        private void inputComponentChoice_AT_DropDownClosed(object sender, System.EventArgs e)
        {
            FillInputActuatorTestText();
        }
        //RDBI
        private void inputECUChoice_RDBI_DropDownClosed(object sender, System.EventArgs e)
        {
            FillInputReadDataText();
            UpdateRDBIComboBox();
        }
        private void inputRDBIChoice_RDBI_DropDownClosed(object sender, System.EventArgs e)
        {
            FillInputReadDataText();
        }
        //SmartTool
        private void inputSmartTool_SM_DropDownClosed(object sender, System.EventArgs e)
        {
            FillInputSmartToolText();
            UpdateIOComboboBox();
        }
        private void inputMeasure_SM_DropDownClosed(object sender, System.EventArgs e)
        {
            FillInputSmartToolText();
        }
        private void inputMeasure_SM_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (ddList.GetKeyPartOf(ddList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text) == "") //Is empty if FindIndex is 0. To get direct input ... well it has to be direct^^
            {
                inputSmartTool.Text = ddList.GetKeyPartOf(ddList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + inputMeasure_SM.Text;
            }
            else
            {
                FillInputSmartToolText();
            }
        }
        private void inputPositiveResult_UpperLimit_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckForWhatCaseInSmartToolPositiveResult();
        }
        private void inputPositiveResult_LowerLimit_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckForWhatCaseInSmartToolPositiveResult();
        }

        //Inits
        private void InitTextItems()
        {
            DataSet data = new DataSet();
            Language language = new Language();

            //Inhalte linke Spalte
            textStep.Content = language.GetStringStep() + " " + (data.GetStepCount() + 1);
            textContentTitel.Content = language.GetStringContent();
            textAnimTitel.Content = language.GetStringAnim();
            textInstructionTitel.Content = language.GetStringInstruction();
            textTitel.Content = language.GetStringPleaseFill();
            if (data.GetStepCount() != 0)
            {
                btnBack.Content = language.GetStringBack();
            }
            else
            {
                btnBack.Content = language.GetStringReset();
            }

            inputAnim.Text = data.GetStepAnimsOfIndex(data.GetStepCount());

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
            btnBack.Content = data.GetStepCount() != 0
                ? language.GetStringBack() 
                : language.GetStringReset();
        }
        private void InitValueItems()
        {
            InitLeftSideItems();
            InitFixedRightSideItems();
            InitFlexRightSideItems();
            ShowItemsAfterToolChoice();
        }
        private void InitLeftSideItems()
        {
            inputStepName.Text = data.GetStepNameOfIndex(data.GetStepCount()) == ""
               ? "Schritt " + (data.GetStepCount() + 1)
               : data.GetStepNameOfIndex(data.GetStepCount());
            inputText.Text = data.GetStepTextOfIndex(data.GetStepCount());
            inputAnim.Text = data.GetStepAnimsOfIndex(data.GetStepCount()) == ""
                ? "default"
                : data.GetStepAnimsOfIndex(data.GetStepCount());
            inputInstruction.Text = data.GetStepInstructionOfIndex(data.GetStepCount());
        }
        private void InitFixedRightSideItems()
        {
            inputPositiveID.ItemsSource = data.GetStepNames();
            inputPositiveID.Text = data.GetStepPositiveIDOfIndex(data.GetStepCount());
            inputNegativeID.Text = data.GetNegativeIDOfIndex(data.GetStepCount());
            inputNegativeID.ItemsSource = data.GetStepNames();
            
            inputRepXML.Text = data.GetRepXMLOfIndex(data.GetStepCount());

            //Check boxes
            inputNextStep.IsChecked = data.GetNextStepOfIndex(data.GetStepCount());
            inputLastStep.IsChecked = data.GetLastStepOfIndex(data.GetStepCount());
        }
        //Inits erste Dropdown Ebene
        private void InitFlexRightSideItems()
        {
            CheckForWhatToolHasBeenChosen();
            inputToolChoice.ItemsSource = ddList.GetToolChoice();
            System.Diagnostics.Debug.WriteLine("Sollte >" + inputToolChoice.Text + "< Dropdowns laden.");
            

            InitComponentComboBox();
            InitIOComboboBox();
            InitRDBIComboBox();

            if (inputToolChoice.Text == ddList.GetToolChoice()[1])
            {
                InitActuatorTest();
            }
            else if(inputToolChoice.Text == ddList.GetToolChoice()[2])
            {
                InitSmartTool();
            }
            else if(inputToolChoice.Text == ddList.GetToolChoice()[3])
            {
                InitReadData();
            }
            else
            {

            }
        }
        //Inits zweite Dropdown Ebene
        private void InitActuatorTest()
        {
            if (data.GetActuatorTestOfIndex(data.GetStepCount()) == "")
            {
                FillInputActuatorTestText();
            }
            else
            {
                inputActuatorTest.Text = ddList.GetDisplayNameOf(ddList.GetECUChoices() ,data.GetActuatorTestOfIndex(data.GetStepCount()));
            }
        }
        private void InitSmartTool()
        {
            if (data.GetSmartToolOfIndex(data.GetStepCount()) == "")
            {
                FillInputSmartToolText();
            }
            else
            {
                inputSmartTool.Text = ddList.GetDisplayNameOf(ddList.GetSmartToolChoices(), data.GetSmartToolOfIndex(data.GetStepCount()));
            }
            if (data.GetPositiveResultOfIndex(data.GetStepCount()) == "")
            {
                CheckForWhatCaseInSmartToolPositiveResult();
            }
            else
            {
                if (inputToolChoice.Text == ddList.GetToolChoice()[2]) ;
                inputPositiveResult_SM.Text = data.GetPositiveResultOfIndex(data.GetStepCount());
            }
            inputPositiveResult_UpperLimit.Text = "";
        }
        private void InitReadData()
        {
            if (string.IsNullOrEmpty(data.GetRDIDOfIndex(data.GetStepCount())))
            {
                FillInputReadDataText();
            }
            else
            {
                inputReadData.Text = data.GetRDIDOfIndex(data.GetStepCount());
            }
        }

        //Inits dritte Dropdown Eebene
        private void InitComponentComboBox()
        {
            inputECUChoice_AT.ItemsSource = ddList.GetECUChoices().Select(x => x.secondPart).ToArray();
            inputECUChoice_AT.Text = ddList.GetECUChoices().Select(x => x.secondPart).ToArray()[0];
            inputComponentChoice_AT.ItemsSource = ddList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray();
            inputComponentChoice_AT.Text = ddList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray()[0];
        }
        private void UpdateComponentComboBox()
        {
            inputECUChoice_AT.ItemsSource = ddList.GetECUChoices().Select(x => x.secondPart).ToArray();
            inputComponentChoice_AT.ItemsSource = ddList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray();
            inputComponentChoice_AT.Text = ddList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray()[0];
        }
        private void InitRDBIComboBox()
        {
            inputECUChoice_RDBI.ItemsSource = ddList.GetECUChoices().Select(x => x.secondPart).ToArray();
            inputECUChoice_RDBI.Text = ddList.GetECUChoices().Select(x => x.secondPart).ToArray()[0];
            inputRDBIChoice_RDBI.ItemsSource = ddList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray(); 
            inputRDBIChoice_RDBI.Text = ddList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray()[0];
        }
        private void UpdateRDBIComboBox()
        {
            inputECUChoice_RDBI.ItemsSource = ddList.GetECUChoices().Select(x => x.secondPart).ToArray();
            inputRDBIChoice_RDBI.ItemsSource = ddList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray();
            inputRDBIChoice_RDBI.Text = ddList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray()[0];
        }
        private void InitIOComboboBox()
        {
            inputSmartTool_SM.ItemsSource = ddList.GetSmartToolChoices().Select(x => x.secondPart).ToArray();
            inputSmartTool_SM.Text = ddList.GetSmartToolChoices().Select(x => x.secondPart).ToArray()[0];
            inputMeasure_SM.ItemsSource = ddList.GetMeasurementChoices(inputSmartTool_SM.Text).Select(x => x.secondPart).ToArray();
            inputMeasure_SM.Text = ddList.GetMeasurementChoices(inputSmartTool_SM.Text).Select(x => x.secondPart).ToArray()[0];
        }
        private void UpdateIOComboboBox()
        {
            inputSmartTool_SM.ItemsSource = ddList.GetSmartToolChoices().Select(x => x.secondPart).ToArray();
            inputMeasure_SM.ItemsSource = ddList.GetMeasurementChoices(inputSmartTool_SM.Text).Select(x => x.secondPart).ToArray();
            inputMeasure_SM.Text = ddList.GetMeasurementChoices(inputSmartTool_SM.Text).Select(x => x.secondPart).ToArray()[0];
        }

        //Anderes
        private void HideAllItemsWithToggleVisibility()
        {
            RDBI.Visibility = Visibility.Hidden;
            smartTool.Visibility = Visibility.Hidden;
            actuatorTest.Visibility = Visibility.Hidden;
        }
        private void ShowItemsAfterToolChoice()
        {
            if (inputToolChoice.Text == ddList.GetToolChoice()[1]) //AT
            {
                HideAllItemsWithToggleVisibility();
                actuatorTest.Visibility = Visibility.Visible;
            }
            else if(inputToolChoice.Text == ddList.GetToolChoice()[2])  //SmT
            {
                HideAllItemsWithToggleVisibility();
                smartTool.Visibility = Visibility.Visible;
            }
            else if(inputToolChoice.Text == ddList.GetToolChoice()[3]) //RDID
            {
                HideAllItemsWithToggleVisibility();
                RDBI.Visibility = Visibility.Visible;
            }
            else
            {
                HideAllItemsWithToggleVisibility();
            }
        }
        private void DifferntiatePositiveResultSourceBeforeSaving()
        {
            System.Diagnostics.Debug.WriteLine("Whatt, wo????"+inputToolChoice.Text);
            if (inputToolChoice.Text == ddList.GetToolChoice()[3]) //RDID
            {
                positiveResult = inputPositiveResult_RDBI.Text;
            }
            else if (inputToolChoice.Text == ddList.GetToolChoice()[2]) //SmT
            {
                positiveResult = inputPositiveResult_SM.Text;
            }
        }
        private void CheckForWhatCaseInSmartToolPositiveResult()
        {
            if (!string.IsNullOrWhiteSpace(inputPositiveResult_LowerLimit.Text) && string.IsNullOrWhiteSpace(inputPositiveResult_UpperLimit.Text))
            {
                inputPositiveResult_SM.Text = inputPositiveResult_LowerLimit.Text + ";lower";
            }
            else if (string.IsNullOrWhiteSpace(inputPositiveResult_LowerLimit.Text) && !string.IsNullOrWhiteSpace(inputPositiveResult_UpperLimit.Text))
            {
                inputPositiveResult_SM.Text = inputPositiveResult_UpperLimit.Text + ";upper";
            }
            else if (string.IsNullOrWhiteSpace(inputPositiveResult_LowerLimit.Text) && string.IsNullOrWhiteSpace(inputPositiveResult_UpperLimit.Text))
            {
                inputPositiveResult_SM.Text = "";
            }
            else if (!string.IsNullOrWhiteSpace(inputPositiveResult_LowerLimit.Text) && !string.IsNullOrWhiteSpace(inputPositiveResult_UpperLimit.Text))
            {
                inputPositiveResult_SM.Text = inputPositiveResult_LowerLimit.Text + "|" + inputPositiveResult_UpperLimit.Text;
            }
            else
            {
                inputPositiveResult_SM.Text = "";
                System.Diagnostics.Debug.Write("Dieser Pfad in ChechForWhatCaseInSmartToolPositiveResult-Method sollte nie erreicht werden: ");
            }
        }
        private void FillInputSmartToolText()
        {
            inputSmartTool.Text = ddList.GetKeyPartOf(ddList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + ddList.GetKeyPartOf(ddList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text);
        }
        private void FillInputReadDataText()
        {
            inputReadData.Text = ddList.GetKeyPartOf(ddList.GetRDIDChoices(inputECUChoice_RDBI.Text), inputRDBIChoice_RDBI.Text) + "|" + ddList.GetKeyPartOf(ddList.GetECUChoices(), inputECUChoice_RDBI.Text);
        }
        private void FillInputActuatorTestText()
        {
            inputActuatorTest.Text = ddList.GetKeyPartOf(ddList.GetIOChoices(inputECUChoice_AT.Text), inputComponentChoice_AT.Text) + "|" + ddList.GetKeyPartOf(ddList.GetECUChoices(), inputECUChoice_AT.Text);
        }
        private void CheckForWhatToolHasBeenChosen()
        {
            System.Diagnostics.Debug.WriteLine("AT: " + data.GetActuatorTestOfIndex(data.GetStepCount()));
            System.Diagnostics.Debug.WriteLine("SmT: " + data.GetSmartToolOfIndex(data.GetStepCount()));
            System.Diagnostics.Debug.WriteLine("RDID: " + data.GetRDIDOfIndex(data.GetStepCount()));
            
            if (data.GetActuatorTestOfIndex(data.GetStepCount())!="" && data.GetActuatorTestOfIndex(data.GetStepCount()) != "false")
            {
                inputToolChoice.Text = ddList.GetToolChoice()[1];
            }
            else if (data.GetSmartToolOfIndex(data.GetStepCount()) != "" && data.GetSmartToolOfIndex(data.GetStepCount()) != "false")
            {
                inputToolChoice.Text = ddList.GetToolChoice()[2];
            }
            else if (data.GetRDIDOfIndex(data.GetStepCount()) != "" && data.GetRDIDOfIndex(data.GetStepCount()) != "false")
            {
                inputToolChoice.Text = ddList.GetToolChoice()[3];
            }
            else
            {
                inputToolChoice.Text = language.GetStringPleaseChoose();
            }
        }

    }
}
