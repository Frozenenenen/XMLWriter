using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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
        LoadInputOptions input = new LoadInputOptions();
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
            DifferntiatePositiveResultSourceBeforeSaving();
            data.SaveSet(inputToolChoice.Text, inputStepName.Text, inputText.Text, inputAnim.Text, inputInstruction.Text, inputPositiveID.Text, inputNegativeID.Text, positiveResult,
                            inputRepXML.Text, inputActuatorTest.Text, inputReadData.Text, inputSmartTool.Text, inputNextStep.IsChecked, inputLastStep.IsChecked);
            GUI.IncrementSteps();

            _ = NavigationService.Navigate(new GfsPage());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            DifferntiatePositiveResultSourceBeforeSaving();
            if (data.GetStepCount() == 0)
            {
                _ = NavigationService.Navigate(new StartPage());
                data.ResetDataSet();
            }
            else
            {
                data.SaveSet(inputToolChoice.Text, inputStepName.Text, inputText.Text, inputAnim.Text, inputInstruction.Text, inputPositiveID.Text, inputNegativeID.Text, positiveResult,
                                inputRepXML.Text, inputActuatorTest.Text, inputReadData.Text, inputSmartTool.Text, inputNextStep.IsChecked, inputLastStep.IsChecked);
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
            data.SaveSet(inputToolChoice.Text, inputStepName.Text, inputText.Text, inputAnim.Text, inputInstruction.Text, inputPositiveID.Text, inputNegativeID.Text, positiveResult,
                            inputRepXML.Text, inputActuatorTest.Text, inputReadData.Text, inputSmartTool.Text, inputNextStep.IsChecked, inputLastStep.IsChecked);
            GUI.IncrementSteps();
            GUI.DecrementStepsForSaving(); //Entweder ich mach ne extra Funktion für die letzte Dateneingabe oder ich in- und decrementiere direkt nacheinander. i++ i--. Anonsten hab ich beim zurückgehen Probleme^^

            _ = NavigationService.Navigate(new SavePage());
        }

        //Comboboxes and Texboxes
        private void inputToolChoice_DropDownClosed(object sender, System.EventArgs e)
        {
            ShowItemsAfterToolChoice();
        }
        //Aktortest
        private void inputECUChoice_AT_DropDownClosed(object sender, System.EventArgs e)
        {
            inputActuatorTest.Text = inputComponentChoice_AT.Text + "|" + inputECUChoice_AT.Text;
            InitComponentComboBox();
        }
        private void inputComponentChoice_AT_DropDownClosed(object sender, System.EventArgs e)
        {
            inputActuatorTest.Text = inputComponentChoice_AT.Text + "|" + inputECUChoice_AT.Text;
        }
        //RDBI
        private void inputECUChoice_RDBI_DropDownClosed(object sender, System.EventArgs e)
        {
            inputReadData.Text = inputRDBIChoice_RDBI.Text + "|" + inputECUChoice_RDBI.Text;
            InitRDBIComboBox();
        }
        private void inputRDBIChoice_RDBI_DropDownClosed(object sender, System.EventArgs e)
        {
            inputReadData.Text = inputRDBIChoice_RDBI.Text + "|" + inputECUChoice_RDBI.Text;
        }
        //SmartTool
        private void inputSmartTool_SM_DropDownClosed(object sender, System.EventArgs e)
        {
            inputSmartTool.Text = inputSmartTool_SM.Text + "|" + inputMeasure_SM.Text;
            InitIOComboboBox();
        }
        private void inputMeasure_SM_DropDownClosed(object sender, System.EventArgs e)
        {
            inputSmartTool.Text = inputSmartTool_SM.Text + "|" + inputMeasure_SM.Text;
        }
        private void inputMeasure_SM_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            inputSmartTool.Text = inputSmartTool_SM.Text + "|" + inputMeasure_SM.Text;
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

            inputAnim.Text = data.GetStepAnimsPos(data.GetStepCount());

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
            inputToolChoice.Text = language.GetStringToolChoise();
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
        //Inits erste Unterbene
        private void InitLeftSideItems()
        {
            inputStepName.Text = data.GetStepNamePos(data.GetStepCount()) == ""
               ? "Schritt " + (data.GetStepCount() + 1)
               : data.GetStepNamePos(data.GetStepCount());
            inputText.Text = data.GetStepTextPos(data.GetStepCount());
            inputAnim.Text = data.GetStepAnimsPos(data.GetStepCount()) == ""
                ? "default"
                : data.GetStepAnimsPos(data.GetStepCount());
            inputInstruction.Text = data.GetStepInstructionPos(data.GetStepCount());
        }
        private void InitFixedRightSideItems()
        {
            inputPositiveID.ItemsSource = data.GetStepNames();
            inputPositiveID.Text = data.GetStepPositiveIDPos(data.GetStepCount());
            inputNegativeID.Text = data.GetNegativeIDPos(data.GetStepCount());
            inputNegativeID.ItemsSource = data.GetStepNames();
            
            inputRepXML.Text = data.GetRepXMLPos(data.GetStepCount());

            //Check boxes
            inputNextStep.IsChecked = data.GetNextStepPos(data.GetStepCount());
            inputLastStep.IsChecked = data.GetLastStepPos(data.GetStepCount());
        }
        private void InitFlexRightSideItems()
        {
            inputToolChoice.ItemsSource = input.GetToolChoice();
            inputToolChoice.Text = data.GetToolChoice(data.GetStepCount());
            InitActuatorTest();
            InitReadData();
            //SmartTool
            InitSmartTool();
        }
        //Inits zweite Unterbene
        private void InitActuatorTest()
        {
            InitComponentComboBox();

            if (data.GetActuatorTestPos(data.GetStepCount()) == "")
            {
                inputActuatorTest.Text = inputComponentChoice_AT.Text + "|" + inputECUChoice_AT.Text;
            }
            else
            {
                inputActuatorTest.Text = data.GetActuatorTestPos(data.GetStepCount());
            }
        }
        private void InitReadData()
        {
            InitRDBIComboBox();
            if (string.IsNullOrEmpty(data.GetRDBIPpos(data.GetStepCount())))
            {
                inputReadData.Text = inputRDBIChoice_RDBI.Text + "|" + inputECUChoice_RDBI.Text;
            }
            else
            {
                inputReadData.Text = data.GetRDBIPpos(data.GetStepCount());
            }
            if (inputToolChoice.Text == input.GetToolChoice()[3])
            {
                inputPositiveResult_RDBI.Text = data.GetPositiveResultPos(data.GetStepCount());
            }
            else
            {
                inputPositiveResult_RDBI.Text = "";
            }
        }
        private void InitSmartTool()
        {
            InitIOComboboBox();
            if(data.GetSmartToolPos(data.GetStepCount()) == "")
            {
                inputSmartTool.Text = inputSmartTool_SM.Text + "|" + inputMeasure_SM.Text;
            }
            else
            {
                inputSmartTool.Text = data.GetSmartToolPos(data.GetStepCount());
            }
            if (data.GetPositiveResultPos(data.GetStepCount()) == "")
            {
                CheckForWhatCaseInSmartToolPositiveResult();
            }
            else
            {
                if (inputToolChoice.Text == input.GetToolChoice()[2])
                    inputPositiveResult_SM.Text = data.GetPositiveResultPos(data.GetStepCount());
            }
            inputPositiveResult_UpperLimit.Text = "";
        }
        //Inits dritte Unterebene
        private void InitComponentComboBox()
        {
            System.Diagnostics.Debug.WriteLine("GFS-Init-ECU");
            inputECUChoice_AT.ItemsSource = input.GetECUChoices();
            inputECUChoice_AT.Text = input.GetECUChoices()[0];
            System.Diagnostics.Debug.WriteLine("GFS-Init-ECUsub");
            inputComponentChoice_AT.ItemsSource = input.Get_AT_IOChoices(language.GetLanguageChoises()[0], inputECUChoice_AT.Text);
            inputComponentChoice_AT.Text = input.Get_AT_IOChoices(language.GetLanguageChoises()[0], inputECUChoice_AT.Text)[0];
        }
        private void InitRDBIComboBox()
        {
            inputECUChoice_RDBI.ItemsSource = input.GetECUChoices();
            inputECUChoice_RDBI.Text = input.GetECUChoices()[0];
            inputRDBIChoice_RDBI.ItemsSource = input.GetRDIDChoices(language.GetLanguageChoises()[0], inputECUChoice_RDBI.Text);
            inputRDBIChoice_RDBI.Text = input.GetRDIDChoices(language.GetLanguageChoises()[0], inputECUChoice_RDBI.Text)[0];
        }
        private void InitIOComboboBox()
        {
            System.Diagnostics.Debug.WriteLine("\nAnfang IO");
            inputMeasure_SM.ItemsSource = input.GetSmartToolChoices();
            System.Diagnostics.Debug.WriteLine("\nIO 2");
            inputMeasure_SM.Text = input.GetSmartToolChoices()[0];
            System.Diagnostics.Debug.WriteLine("\nIO 3");
            inputSmartTool_SM.ItemsSource = input.GetMeasurementChoices(language.GetLanguageChoises()[0], inputMeasure_SM.Text);
            System.Diagnostics.Debug.WriteLine("\nIO 4");
            inputSmartTool_SM.Text = input.GetMeasurementChoices(language.GetLanguageChoises()[0], inputMeasure_SM.Text)[0];
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
            switch (inputToolChoice.Text)
            {
                case "ActuatorTest":
                    HideAllItemsWithToggleVisibility();
                    actuatorTest.Visibility = Visibility.Visible;

                    break;

                case "SmartTool":
                    HideAllItemsWithToggleVisibility();
                    smartTool.Visibility = Visibility.Visible;
                    break;

                case "ReadDataByIdentifier":
                    HideAllItemsWithToggleVisibility();
                    RDBI.Visibility = Visibility.Visible;
                    break;

                default:
                    HideAllItemsWithToggleVisibility();
                    break;
            }
        }
        private void DifferntiatePositiveResultSourceBeforeSaving()
        {
            System.Diagnostics.Debug.WriteLine(inputToolChoice.Text);
            if (inputToolChoice.Text == "ReadDataByIdentifier")
            {
                positiveResult = inputPositiveResult_RDBI.Text;
            }
            else if (inputToolChoice.Text == "SmartTool")
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
    }
}
