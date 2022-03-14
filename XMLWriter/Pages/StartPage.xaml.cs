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
        StartPageHelper startPageHelper = new StartPageHelper();
        public StartPage() {
            InitializeComponent();
            startPageHelper.InitLanguages();
            InitLabels();
            InitButtons();
            InitDropDowns();
        }
        ///---Inits---///
        private void InitLabels() {
            startPageHelper.SetTitelText(labelTitel);
            startPageHelper.SetLoadFileText(labelLoadFile);
            startPageHelper.SetDisplayStepsText(labelStepCount);
            startPageHelper.SetTxtOrDataBaseCheckBoxText(textUseDatabaseChecked, textUseDatabaseUnchecked);
        }
        private void InitButtons() {
            startPageHelper.SetStartButtonText(btnWeiter);
            startPageHelper.SetLoadButtonText(btnLoadFile);
            startPageHelper.SetResetButtonText(btnReset);
        }
        private void InitDropDowns() {
            startPageHelper.InitProcessTypeDropDown(dropDownProcesses);
            startPageHelper.InitLanguageSelectionDropDown(dropDownLanguage);
        }
        ///---Buttons---///
        private void BtnStart(object sender, RoutedEventArgs e) {
            startPageHelper.InitNewDataSet();
            startPageHelper.CheckForDataBaseOrTxt(checkUseTxtOrDatabse.IsChecked);
            //Navigation
            if (startPageHelper.IsRepSelected()) //rep - Reparatur
            {
                _ = NavigationService.Navigate(new RepPage());
            }
            else if (startPageHelper.IsGfsSelected()) //gfs - geführte Fehlersuche
            {
                startPageHelper.LoadDropDownOptions();
                _ = NavigationService.Navigate(new GfsPage());
            }
            else {
                System.Diagnostics.Debug.WriteLine("Fehler in der gfs/rep-Wahl---                    BtnNext() aus StartPage");
            }
        }
        private void BtnLoadFile(object sender, RoutedEventArgs e) {
            startPageHelper.Reset(textBlockLoadFile);
            startPageHelper.LoadDataFromFile();
            startPageHelper.ChangeProcessActiveElement(dropDownProcesses, startPageHelper.GetSelectedProcessType());
            startPageHelper.SetFilePathText(textBlockLoadFile);
            startPageHelper.SetDisplayStepsText(labelStepCount);
        }
        private void BtnReset(object sender, RoutedEventArgs e) {
            startPageHelper.Reset(textBlockLoadFile);
        }
        private void DropDownLanguage_OnClosed(object sender, EventArgs e) {
            startPageHelper.SetLangauge(dropDownLanguage.Text);
            InitLabels();
            InitButtons();
        }
        private void DropDownProcesses_OnClosed(object sender, EventArgs e) {
            startPageHelper.ChangeProcessActiveElement(dropDownProcesses, dropDownProcesses.Text);
        }
    }
}
