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

            data.SaveSet(inputStepName.Text, inputText.Text, inputAnim.Text, inputSpecialText.Text);

            if (data.GetStepCount() == 0)
            {
                _ = NavigationService.Navigate(new StartPage());
            }
            else
            {
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
            btnBack.Content = language.GetStringBack();
            btnBackDelete.Content = language.GetStringReset();
            btnNext.Content = language.GetStringNext();
            btnSave.Content = language.GetStringSave();
        }
        private void InitValueItems()
        {
            DataSet data = new DataSet();

            inputStepName.Text = data.GetStepNameOfIndex(data.GetStepCount()) == ""
                ? "Schritt " + (data.GetStepCount() + 1)
                : data.GetStepNameOfIndex(data.GetStepCount());
            inputText.Text = data.GetStepTextOfIndex(data.GetStepCount());
            inputAnim.Text = data.GetStepAnimsOfIndex(data.GetStepCount()) == ""
                ? "default"
                : data.GetStepAnimsOfIndex(data.GetStepCount());
            inputSpecialText.Text = data.GetStepSpecialTextOfIndex(data.GetStepCount());

            Console.WriteLine("Ausgabe: Schritt: " + (data.GetStepCount() + 1) + " Anim: " + data.GetStepAnimsOfIndex(data.GetStepCount()) + " Text: " + data.GetStepTextOfIndex(data.GetStepCount()) + " SpText: " + data.GetStepSpecialTextOfIndex(data.GetStepCount()));
        }
    }
}
