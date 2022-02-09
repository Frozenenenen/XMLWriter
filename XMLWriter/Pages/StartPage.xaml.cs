using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using XMLWriter.Classes;
using XMLWriter.Classes.StartPage;

namespace XMLWriter.Pages {
    /// <summary>
    /// Interaktionslogik für StartPage.xaml
    /// </summary>
    public partial class StartPage : Page {
        StartPageHelper startPageService = new StartPageHelper();
        ConsoleControl consol = new ConsoleControl();
        public StartPage() {
            InitializeComponent();
            startPageService.InitLanguages();
            InitLabels();
            InitButtons();
            InitDropDowns();
        }
        ///---Inits---///
        private void InitLabels() {
            startPageService.SetTitelText(labelTitel);
            startPageService.SetLoadFileText(labelLoadFile);
            startPageService.SetDisplayStepsText(labelStepCount);
            startPageService.SetTxtOrDataBaseCheckBoxText(textUseDatabaseChecked, textUseDatabaseUnchecked);
        }
        private void InitButtons() {
            startPageService.SetStartButtonText(btnWeiter);
            startPageService.SetLoadButtonText(btnLoadFile);
            startPageService.SetResetButtonText(btnReset);
        }
        private void InitDropDowns() {
            startPageService.InitProcessTypeDropDown(dropDownProcesses);
            startPageService.InitLanguageSelectionDropDown(dropDownLanguage);
        }
        ///---Buttons---///
        private void BtnStart(object sender, RoutedEventArgs e) {
            startPageService.InitNewDataSet();
            startPageService.DatabaseOrTxtCheck(checkUseTxtOrDatabse.IsChecked);
            //Navigation
            if (startPageService.GetSelectedProcessType() == startPageService.GetProcessTypeList()[1]) //rep - Reparatur
            {
                _ = NavigationService.Navigate(new RepPage());
            }
            else if (dropDownProcesses.Text == startPageService.GetProcessTypeList()[0]) //gfs - geführte Fehlersuche
            {
                startPageService.LoadDropDownOptions();
                _ = NavigationService.Navigate(new GfsPage());
            }
            else {
                if (consol.showErrors) Console.WriteLine("Fehler in der gfs/rep-Wahl---                    BtnNext() aus StartPage");
            }
        }
        private void BtnLoadFile(object sender, RoutedEventArgs e) {

            startPageService.LoadDataFromFile();
            startPageService.ChangeProcessActiveElement(dropDownProcesses, dropDownProcesses.Text);
            startPageService.SetFilePathText(textBlockLoadFile);
            startPageService.SetDisplayStepsText(labelStepCount);
        }
        private void BtnReset(object sender, RoutedEventArgs e) {
            startPageService.Reset(textBlockLoadFile);
        }
        private void DropDownLanguage_OnClosed(object sender, EventArgs e) {
            startPageService.SetLangauge(dropDownLanguage.Text);
            InitLabels();
            InitButtons();
        }
        private void DropDownProcesses_OnClosed(object sender, EventArgs e) {
            startPageService.ChangeProcessActiveElement(dropDownProcesses, dropDownProcesses.Text);
        }
    }
}
