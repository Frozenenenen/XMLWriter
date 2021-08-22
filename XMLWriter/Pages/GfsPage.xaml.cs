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
        public GfsPage()
        {
            InitializeComponent();
            data.InitNewDataSet();
            InitTextItems();
            InitValueItems();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            data.SaveGfsSet(inputStepName.Text, inputText.Text, inputAnim.Text, inputInstruction.Text, inputPositiveID.Text, inputNegativeID.Text, inputPositiveResult.Text,
                            inputRepXML.Text, inputActuatorTest.Text, inputReadData.Text, inputSmartTool.Text, inputNextStep.IsChecked, inputLastStep.IsChecked);
            GUI.IncrementSteps();

            _ = NavigationService.Navigate(new GfsPage());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
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
                data.SaveGfsSet(inputStepName.Text, inputText.Text, inputAnim.Text, inputInstruction.Text, inputPositiveID.Text, inputNegativeID.Text, inputPositiveResult.Text,
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
            data.SaveGfsSet(inputStepName.Text, inputText.Text, inputAnim.Text, inputInstruction.Text, inputPositiveID.Text, inputNegativeID.Text, inputPositiveResult.Text,
                            inputRepXML.Text, inputActuatorTest.Text, inputReadData.Text, inputSmartTool.Text, inputNextStep.IsChecked, inputLastStep.IsChecked);
            GUI.IncrementSteps();
            GUI.DecrementStepsForSaving(); //Entweder ich mach ne extra Funktion für die letzte Dateneingabe oder ich in- und decrementiere direkt nacheinander. i++ i--. Anonsten hab ich beim zurückgehen Probleme^^

            _ = NavigationService.Navigate(new SavePage());
        }
        private void btnToolChoice_Click(object sender, RoutedEventArgs e)
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
                    positiveResult.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
        }

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
            textPositiveResult.Content = language.GetStringPosResult();
            textRepXML.Content = language.GetStringRepXML();
            textActuatorTest.Content = language.GetStringActuatorTest();
            textReadData.Content = language.GetStringReadData();
            textSmartTool.Content = language.GetStringSmartTool();
            inputNextStep.Content = language.GetStringNextStep();
            inputLastStep.Content = language.GetStringLastStep();
            inputToolChoice.Text = language.GetStringToolChoise();
            textActuatorOptional.Content = language.GetStringOptional();
            textRDBIOptional.Content = language.GetStringOptional();
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

        }
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
            inputPositiveResult.Text = data.GetPositiveResultPos(data.GetStepCount());
            //Abhängig von Auswahl
            // Aktortest
            inputECUChoice.ItemsSource = input.GetECUOptions();
            inputECUChoice.Text = input.GetECUOptions()[0];
            inputComponentChoice.ItemsSource = input.GetIOOptions();
            inputComponentChoice.Text = input.GetIOOptions()[0];
            inputActuatorTest.Text = data.GetActuatorTestPos(data.GetStepCount());

            inputReadData.Text = data.GetRDBIPpos(data.GetStepCount());
            inputSmartTool.Text = data.GetSmartToolPos(data.GetStepCount());
            inputToolChoice.ItemsSource = input.GetToolChoice();
        }

        
        private void HideAllItemsWithToggleVisibility()
        {
            RDBI.Visibility = Visibility.Hidden;
            smartTool.Visibility = Visibility.Hidden;
            actuatorTest.Visibility = Visibility.Hidden;
            positiveResult.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            inputActuatorTest.Text = inputComponentChoice.Text + "|" + inputECUChoice.Text;
        }
    }
}
