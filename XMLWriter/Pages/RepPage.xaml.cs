using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace XMLWriter.Pages
{
    /// <summary>
    /// Interaktionslogik für RepPage.xaml
    /// </summary>
    public partial class RepPage : Page
    {
        public RepPage()
        {
            InitializeComponent();
            InitTextItems();
            InitValueItems();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            GUIMovement GUI = new GUIMovement();
            data.SaveSet(inputStepName.Text, inputText.Text, inputAnim.Text, inputSpecialText.Text);
            GUI.IncrementSteps();

            _ = NavigationService.Navigate(new RepPage());
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
                data.SaveSet(inputStepName.Text, inputText.Text, inputAnim.Text, inputSpecialText.Text);
                GUI.DecrementSteps();
                _ = NavigationService.Navigate(new RepPage());
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
                _ = NavigationService.Navigate(new RepPage());
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            GUIMovement GUI = new GUIMovement();
            data.SaveSet(inputStepName.Text, inputText.Text, inputAnim.Text, inputSpecialText.Text);
            GUI.IncrementSteps();
            GUI.DecrementStepsForSaving();

            _ = NavigationService.Navigate(new SavePage());
        }

        private void InitTextItems()
        {
            DataSet data = new DataSet();
            Language language = new Language();

            //Schritzüge
            textStep.Content = language.GetStringStep() + " " + (data.GetStepCount() + 1);
            textContentTitel.Content = language.GetStringContent();
            textAnimTitel.Content = language.GetStringAnim();
            textSpecialContentTitel.Content = language.GetStringSpecialStep();
            textTitel.Content = language.GetStringPleaseFill();
            
            //Buttons
            btnBack.Content = data.GetStepCount() != 0
                ? language.GetStringBack()
                : language.GetStringReset();
            btnBackDelete.Content = language.GetStringReset();
            btnNext.Content = language.GetStringNext();
            btnSave.Content = language.GetStringSave();
        }
        private void InitValueItems()
        {
            DataSet data = new DataSet();

            inputStepName.Text = data.GetStepNamePos(data.GetStepCount()) == ""
                ? "Schritt " + (data.GetStepCount() + 1)
                : data.GetStepNamePos(data.GetStepCount());
            inputText.Text = data.GetStepTextPos(data.GetStepCount());
            inputAnim.Text = data.GetStepAnimsPos(data.GetStepCount()) == ""
                ? "default"
                : data.GetStepAnimsPos(data.GetStepCount());
            inputSpecialText.Text = data.GetStepSpecialTextPos(data.GetStepCount());

            Console.WriteLine("Ausgabe: Schritt: " + (data.GetStepCount() + 1) + " Anim: " + data.GetStepAnimsPos(data.GetStepCount()) + " Text: " + data.GetStepTextPos(data.GetStepCount()) + " SpText: " + data.GetStepSpecialTextPos(data.GetStepCount()));
        }
    }
}
