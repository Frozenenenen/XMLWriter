using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using XMLWriter.Classes.HelpClasses;

namespace XMLWriter.Pages {
    /// <summary>
    /// Interaktionslogik für RepPage.xaml
    /// </summary>
    public partial class RepPage : Page {
        GUIMovementHelper gui = new GUIMovementHelper();
        RepPageHelper repPageHelper = new RepPageHelper();

        public RepPage() {
            InitializeComponent();
            InitTextItems();
            InitValueItems();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e) {
            repPageHelper.SaveCurrentInput(inputStepName, inputText, inputSpecialText, inputAnim);
            repPageHelper.PrepareNextPage();
            _ = NavigationService.Navigate(new RepPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            repPageHelper.PreparePreviousPage();
            repPageHelper.SaveCurrentInput(inputStepName, inputText, inputSpecialText, inputAnim);
            if (gui.IsFirstPage()) {
                _ = NavigationService.Navigate(new StartPage());
            }
            else {
                _ = NavigationService.Navigate(new RepPage());
            }
        }
        private void BtnBackDelete_Click(object sender, RoutedEventArgs e) {
            repPageHelper.DeleteCurrentSet();
            if (!gui.IsFirstPage()) {
                repPageHelper.PreparePreviousPage();
            }
            _ = NavigationService.Navigate(new RepPage());
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            repPageHelper.SaveCurrentInput(inputStepName, inputText, inputSpecialText, inputAnim);
            repPageHelper.PrepareNextPage();
            _ = NavigationService.Navigate(new SavePage());
        }

        private void InitTextItems() {
            //Schritzüge

            repPageHelper.SetLabelStepText(textStep);
            repPageHelper.SetLabelContentTitelText(textContentTitel);
            repPageHelper.SetLabelAnimText(textAnimTitel);
            repPageHelper.SetLabelSpecialText(textSpecialContentTitel);
            repPageHelper.SetLabelTitelText(textTitel);

            //Buttons
            repPageHelper.SetButtonNextText(btnNext);
            repPageHelper.SetButtonBackText(btnBack);
            repPageHelper.SetButtonDeleteText(btnBackDelete);
            repPageHelper.SetButtonSaveText(btnSave);
            repPageHelper.SetButtonInsertText(btnInsert);
        }
        private void InitValueItems() {
            repPageHelper.SetBoxStepNameValue(inputStepName);
            repPageHelper.SetBoxTextValue(inputText);
            repPageHelper.SetBoxAnimValue(inputAnim);
            repPageHelper.SetBoxSpecialText(inputSpecialText);
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e) {
            repPageHelper.InsertNewSet();
        }
    }
}
