using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using XMLWriter.Classes;
using XMLWriter.Classes.HelpClasses;
using System.Linq;

namespace XMLWriter.Pages {
    /// <summary>
    /// Interaktionslogik für RepPage.xaml
    /// </summary>
    public partial class RepPage : Page {
        GUIMovementHelper gui = new GUIMovementHelper();
        RepPageHelper repPageHelper = new RepPageHelper();
        DataSetService dataSets = new DataSetService();

        public RepPage() {
            InitializeComponent();
            InitTextItems();
            InitValueItems();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e) {
            WriteDataSetsToConsole();
            repPageHelper.SaveCurrentInput(inputStepName, inputText, inputSpecialText, inputAnim);
            repPageHelper.PrepareNextPage();
            _ = NavigationService.Navigate(new RepPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            WriteDataSetsToConsole();
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
            WriteDataSetsToConsole();
            repPageHelper.DeleteCurrentSet();
            if (!gui.IsFirstPage()) {
                repPageHelper.PreparePreviousPage();
            }
            _ = NavigationService.Navigate(new RepPage());
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            WriteDataSetsToConsole();
            repPageHelper.SaveCurrentInput(inputStepName, inputText, inputSpecialText, inputAnim);
            repPageHelper.PrepareNextPage();
            _ = NavigationService.Navigate(new SavePage());
        }
        private void BtnInsert_Click(object sender, RoutedEventArgs e) {
            WriteDataSetsToConsole();
            repPageHelper.InsertNewSet();
            _ = NavigationService.Navigate(new RepPage());
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

        
        public void WriteDataSetsToConsole() {
            for (int i = 0; i < dataSets.GetDataSets().Count; i++) {
                System.Diagnostics.Debug.WriteLine("Index: " + i);
                System.Diagnostics.Debug.WriteLine("Step: " + dataSets.GetDataSets().ElementAt(i).stepName);
                System.Diagnostics.Debug.WriteLine("Text: " + dataSets.GetDataSets().ElementAt(i).text);
                System.Diagnostics.Debug.WriteLine("Anim: " + dataSets.GetDataSets().ElementAt(i).anim);
                System.Diagnostics.Debug.WriteLine("Spec: " + dataSets.GetDataSets().ElementAt(i).specialText);
                /*System.Diagnostics.Debug.WriteLine("Instr: " + dataSets.GetDataSets().ElementAt(i).instruction);
                System.Diagnostics.Debug.WriteLine("posID: " + dataSets.GetDataSets().ElementAt(i).positiveID);
                System.Diagnostics.Debug.WriteLine("negID: " + dataSets.GetDataSets().ElementAt(i).negativeID);
                System.Diagnostics.Debug.WriteLine("PosRe: " + dataSets.GetDataSets().ElementAt(i).positiveResult);
                System.Diagnostics.Debug.WriteLine("rXML " + dataSets.GetDataSets().ElementAt(i).repXML);
                System.Diagnostics.Debug.WriteLine("AcTe: " + dataSets.GetDataSets().ElementAt(i).actuatorTest);
                System.Diagnostics.Debug.WriteLine("RDID: " + dataSets.GetDataSets().ElementAt(i).RDID);
                System.Diagnostics.Debug.WriteLine("SmT: " + dataSets.GetDataSets().ElementAt(i).smartTool);
                System.Diagnostics.Debug.WriteLine("Next: " + dataSets.GetDataSets().ElementAt(i).nextStep);
                System.Diagnostics.Debug.WriteLine("Last: " + dataSets.GetDataSets().ElementAt(i).lastStep);
                System.Diagnostics.Debug.WriteLine("Tool: " + dataSets.GetDataSets().ElementAt(i).toolChoice);*/
                System.Diagnostics.Debug.WriteLine("\n\n");
            }
        }
    }
}
