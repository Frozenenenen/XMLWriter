using System;
using XMLWriter.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using XMLWriter.Classes.StartPage;
using XMLWriter.Classes.HelpClasses;

namespace XMLWriter.Pages {
    /// <summary>
    /// Interaktionslogik für SavePage.xaml
    /// </summary>
    public partial class SavePage : Page {
        DataSetService data = new DataSetService();
        GUIMovementHelper gui = new GUIMovementHelper();
        Language language = new Language();
        LoadHelper loadHelper = new LoadHelper();   
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        SavePageHelper savePageHelper = new SavePageHelper();

        public SavePage() {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("Start SavePage");
            InitTextItems();
            InitValueItems();
            InitButtons();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            savePageHelper.StartXMLWriting();
            _ = NavigationService.Navigate(new StartPage());
            //App.Current.Shutdown(0);
        } //fertig

        private void btnBack_Click(object sender, RoutedEventArgs e) {
            gui.DecrementStepsForGoingBackFromSaving();
            if (savePageHelper.IsRep()) {
                _ = NavigationService.Navigate(new RepPage());
            }
            else if (savePageHelper.IsGfs()) {
                _ = NavigationService.Navigate(new GfsPage());
            }
            else {
                Console.WriteLine("Fehler beim btnBack mit Datatype");
            }
        } //fertig

        private void InitTextItems() {
            savePageHelper.InitLabelTitel(textTitel_Label);
            savePageHelper.InitLabelFileName(fileNameTitel_Label);
            //labelFileNameTitel.Content = language.GetStringFileNameTitel();
            savePageHelper.InitLabelStepCount(textStepCount);
        }
        private void InitButtons() {
            savePageHelper.InitButtonBack(btnBack);
            savePageHelper.InitButtonSave(btnSave);
            savePageHelper.InitButtonOpenFile(btnOpenFile);
        }
        private void InitValueItems() {
            savePageHelper.InitTextFilePath(textFileName);
        }

        private void BtnOpenFile(object sender, RoutedEventArgs e) {
            loadHelper.OpenFileDialog();
            if (savePageHelper.PathIsSet()) {
                savePageHelper.SetSaveButtonActive(btnSave);
            }
            else {
                savePageHelper.SetSaveButtonInactive(btnSave);
            }
        }
    }
}
