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
            inputActuatorTest.Text = ddList.GetKeyPartOf(ddList.GetIOChoices(inputECUChoice_AT.Text), inputComponentChoice_AT.Text) + "|" + ddList.GetKeyPartOf(ddList.GetECUChoices(), inputECUChoice_AT.Text);
            InitComponentComboBox();
        }
        private void inputComponentChoice_AT_DropDownClosed(object sender, System.EventArgs e)
        {
            inputActuatorTest.Text = ddList.GetKeyPartOf(ddList.GetIOChoices(inputECUChoice_AT.Text), inputComponentChoice_AT.Text) + "|" + ddList.GetKeyPartOf(ddList.GetECUChoices(), inputECUChoice_AT.Text);
        }
        //RDBI
        private void inputECUChoice_RDBI_DropDownClosed(object sender, System.EventArgs e)
        {
            inputReadData.Text = ddList.GetKeyPartOf(ddList.GetRDIDChoices(inputComponentChoice_AT.Text), inputRDBIChoice_RDBI.Text) + "|" + ddList.GetKeyPartOf(ddList.GetECUChoices(), inputECUChoice_RDBI.Text);
            InitRDBIComboBox();
        }
        private void inputRDBIChoice_RDBI_DropDownClosed(object sender, System.EventArgs e)
        {
            inputReadData.Text = ddList.GetKeyPartOf(ddList.GetRDIDChoices(inputComponentChoice_AT.Text), inputRDBIChoice_RDBI.Text) + "|" + ddList.GetKeyPartOf(ddList.GetECUChoices(), inputECUChoice_RDBI.Text);
        }
        //SmartTool
        private void inputSmartTool_SM_DropDownClosed(object sender, System.EventArgs e)
        {
            inputSmartTool.Text = ddList.GetKeyPartOf(ddList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + ddList.GetKeyPartOf(ddList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text);
            InitIOComboboBox();
        }
        private void inputMeasure_SM_DropDownClosed(object sender, System.EventArgs e)
        {
            inputSmartTool.Text = ddList.GetKeyPartOf(ddList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + ddList.GetKeyPartOf(ddList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text);
        }
        private void inputMeasure_SM_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (ddList.GetKeyPartOf(ddList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text) == "") //Is empty if FindIndex is 0. To get direct input ... well it has to be direct^^
            {
                inputSmartTool.Text = ddList.GetKeyPartOf(ddList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + inputMeasure_SM.Text;
            }
            else
            {
                inputSmartTool.Text = ddList.GetKeyPartOf(ddList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + ddList.GetKeyPartOf(ddList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text);
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
        //Inits erste Unterebene
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
            inputToolChoice.ItemsSource = ddList.GetToolChoice();
            inputToolChoice.Text = data.GetToolChoice(data.GetStepCount());
            InitActuatorTest();
            InitReadData();
            InitSmartTool();
        }


        //Inits zweite Unterebene
        private void InitActuatorTest()
        {
            InitComponentComboBox();

            if (data.GetActuatorTestPos(data.GetStepCount()) == "")
            {
                inputActuatorTest.Text = ddList.GetKeyPartOf(ddList.GetIOChoices(inputECUChoice_AT.Text), inputComponentChoice_AT.Text) + "|" + ddList.GetKeyPartOf(ddList.GetECUChoices(), inputECUChoice_AT.Text);
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
                inputReadData.Text = ddList.GetKeyPartOf(ddList.GetRDIDChoices(inputComponentChoice_AT.Text), inputRDBIChoice_RDBI.Text) + "|" + ddList.GetKeyPartOf(ddList.GetECUChoices(), inputECUChoice_RDBI.Text);
            }
            else
            {
                inputReadData.Text = data.GetRDBIPpos(data.GetStepCount());
            }
            if (inputToolChoice.Text == ddList.GetToolChoice()[3])
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
                inputSmartTool.Text = ddList.GetKeyPartOf(ddList.GetSmartToolChoices(), inputSmartTool_SM.Text) + "|" + ddList.GetKeyPartOf(ddList.GetMeasurementChoices(inputSmartTool_SM.Text), inputMeasure_SM.Text);
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
                if (inputToolChoice.Text == ddList.GetToolChoice()[0]);             //Hier wurde ein Index entfernt. leider kA was vorher drin stand. 0 eigefügt
                    inputPositiveResult_SM.Text = data.GetPositiveResultPos(data.GetStepCount());
            }
            inputPositiveResult_UpperLimit.Text = "";
            
        }

        


        //Inits dritte Unterebene
        private void InitComponentComboBox()
        {
            inputECUChoice_AT.ItemsSource = ddList.GetECUChoices().Select(x => x.secondPart).ToArray();
            //inputECUChoice_AT.Text = input.GetECUChoices()[0]; //Wird erst wieder gebraucht, wenn der Wert vorher aus XML geladen wurde. Muss ggf Init von Aktualisierung getrennt werden

            inputComponentChoice_AT.ItemsSource = ddList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray();
            inputComponentChoice_AT.Text = ddList.GetIOChoices(inputECUChoice_AT.Text).Select(x => x.secondPart).ToArray()[0];

        }
        private void InitRDBIComboBox()
        {
            inputECUChoice_RDBI.ItemsSource = ddList.GetECUChoices().Select(x => x.secondPart).ToArray();
            //inputECUChoice_RDBI.Text = input.GetECUChoices()[0]; //Wird erst wieder gebraucht, wenn der Wert vorher aus XML geladen wurde. Muss ggf Init von Aktualisierung getrennt werden

            inputRDBIChoice_RDBI.ItemsSource = ddList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray(); 
            inputRDBIChoice_RDBI.Text = ddList.GetRDIDChoices(inputECUChoice_RDBI.Text).Select(x => x.secondPart).ToArray()[0];
        }
        private void InitIOComboboBox()
        {
         inputSmartTool_SM.ItemsSource = ddList.GetSmartToolChoices().Select(x => x.secondPart).ToArray();
          //inputSmartTool_SM.Text = input.GetSmartToolChoices()[0]; //Wird erst wieder gebraucht, wenn der Wert vorher aus XML geladen wurde. Muss ggf Init von Aktualisierung getrennt werden
          
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
