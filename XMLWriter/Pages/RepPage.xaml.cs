using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            GUIMovement GUI = new GUIMovement();
            MainWindow main = new MainWindow();
            data.SaveRepSet(inputStepName.Text, inputText.Text, inputAnim.Text, inputSpecialText.Text);
            GUI.IncrementSteps();
            _ = NavigationService.Navigate(new RepPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            GUIMovement GUI = new GUIMovement();
            MainWindow main = new MainWindow();

            if (data.GetStepCount() == 0)
            {
                _ = NavigationService.Navigate(new StartPage());
            }
            else
            {
                data.SaveRepSet(inputStepName.Text, inputText.Text, inputAnim.Text, inputSpecialText.Text);
                GUI.DecrementSteps();
                _ = NavigationService.Navigate(new RepPage());
            }

        }
        private void btnBackDelete_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            GUIMovement GUI = new GUIMovement();
            MainWindow main = new MainWindow();

            if (data.GetStepCount() == 0)
            {

                _ = NavigationService.Navigate(new StartPage());
            }
            else
            {
                GUI.DecrementStepsMax();
                _ = NavigationService.Navigate(new RepPage());
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            GUIMovement GUI = new GUIMovement();
            data.SaveRepSet(inputStepName.Text, inputText.Text, inputAnim.Text, inputSpecialText.Text);
            GUI.IncrementSteps();
            GUI.DecrementStepsForSaving();

            _ = NavigationService.Navigate(new SavePage());
        }

        private void InitTextItems()
        {
            DataSet data = new DataSet();
            Language language = new Language();

            textStep.Content = language.GetStringStep() + " " + (data.GetStepCount() + 1);
            textContentTitel.Content = language.GetStringContent();
            textAnimTitel.Content = language.GetStringAnim();
            textSpecialContentTitel.Content = language.GetStringSpecialStep();
            textTitel.Content = language.GetStringPleaseFill();
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
            inputAnim.Text = data.GetStepAnimsPos(data.GetStepCount()) == ""
                ? "default"
                : data.GetStepAnimsPos(data.GetStepCount());
            inputStepName.Text = data.GetStepNamePos(data.GetStepCount()) == ""
                ? "Schritt " + (data.GetStepCount() + 1)
                : data.GetStepNamePos(data.GetStepCount());
            inputText.Text = data.GetStepTextPos(data.GetStepCount());
            inputSpecialText.Text = data.GetStepSpecialTextPos(data.GetStepCount());

            Console.WriteLine("Ausgabe: Schritt: " + (data.GetStepCount() + 1) + " Anim: " + data.GetStepAnimsPos(data.GetStepCount()) + " Text: " + data.GetStepTextPos(data.GetStepCount()) + " SpText: " + data.GetStepSpecialTextPos(data.GetStepCount()));
        }


    }
}
