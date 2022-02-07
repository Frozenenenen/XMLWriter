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
        DataSetManager data = new DataSetManager();
        GUIMovement gui = new GUIMovement();
        Language language = new Language();
        LoadHelper loadHelper = new LoadHelper();   
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        SavePageHelper savePageData = new SavePageHelper();
        LoadDataHelper loadDataHelper = new LoadDataHelper();

        public SavePage() {
            InitializeComponent();
            InitTextItems();
            InitValueItems();
            InitButtons();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) {

            data.SetFileName(inputFileName.Text);
            if (data.GetDataType() == "rep") {
                System.Diagnostics.Debug.WriteLine("Rep Speichern");
                data.OutputToXML();
            }
            else if (data.GetDataType() == "gfs") {
                data.OutputToXML();
            }
            else {
                System.Diagnostics.Debug.WriteLine("Error with DataType - Savepage Savebutton");
            }
            _ = NavigationService.Navigate(new StartPage());
            //App.Current.Shutdown(0);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) {
            gui.DecrementStepsForGoingBackFromSaving();
            if (data.GetDataType() == "rep") {
                _ = NavigationService.Navigate(new RepPage());
            }
            else if (data.GetDataType() == "gfs") {
                _ = NavigationService.Navigate(new GfsPage());
            }
            else {
                Console.WriteLine("Fehler beim btnBack mit Datatype");
            }


        }

        private void InitTextItems() {
            DataSetToXMLWriter writer = new DataSetToXMLWriter();
            inputVehicleID.ItemsSource = writer.GetPathVehicleIDChoises();
            inputLanguage.ItemsSource = writer.GetPathLanguageChoises();
            textTitel.Content = language.GetStringSummary();
            xamlHelper.SetTextFor(labelFileNameTitel, savePageData.GetStringFileNameTitel());
            //labelFileNameTitel.Content = language.GetStringFileNameTitel();
            textStepCount.Content = (data.GetStepCountMax()) + " " + savePageData.GetStringSteps();
            btnBack.Content = language.GetStringBack();
            btnSave.Content = language.GetStringSave();
            inputVehicleID.Text = "eGolf";
            inputLanguage.Text = "de";

        }
        private void InitButtons() {
            btnLoadFile.Content = language.GetStringFilePathDialog();
            btnSave.Background = Brushes.Gray;
            btnSave.IsEnabled = false;
        }
        private void InitValueItems() {
            inputFileName.Text = data.GetFileName();
        }

        private void BtnLoadFile(object sender, RoutedEventArgs e) {
            btnLoadFile.Content = language.GetStringFilePathDialog();
            loadHelper.OpenFileDialog();
            inputFileName.Text = loadDataHelper.GetFileNameAndPath();
            textFileName.Text = loadDataHelper.GetFileNameAndPath();
            if (textFileName.Text == "") {
                btnSave.Background = Brushes.Gray;
                btnSave.IsEnabled = false;
            }
            else {
                btnSave.Background = Brushes.Green;
                btnSave.IsEnabled = true;
            }
        }
    }
}
