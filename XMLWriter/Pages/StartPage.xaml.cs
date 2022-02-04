using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using XMLWriter.Classes;
using XMLWriter.Classes.StartPage;

namespace XMLWriter.Pages
{
    /// <summary>
    /// Interaktionslogik für StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        DataSets data = new DataSets();
        StartPageHelper startPageHelper = new StartPageHelper();
        ConsoleControl consol = new ConsoleControl();   
        public StartPage()
        {
            InitializeComponent();
            startPageHelper.InitLanguages();
            InitLabels();
            InitButtons();
            InitDropDowns();
        }
        private void InitLabels()
        {
            startPageHelper.SetTitelText(labelTitel);
            startPageHelper.SetLoadFileText(labelLoadFile);
            startPageHelper.SetDisplayStepsText(labelStepCount);
            startPageHelper.SetTxtOrDataBaseCheckBoxText(textUseDatabaseChecked, textUseDatabaseUnchecked);
        }
        private void InitButtons()
        {
            startPageHelper.SetStartButtonText(btnWeiter);
            startPageHelper.SetLoadButtonText(btnLoadFile);
            startPageHelper.SetResetButtonText(btnReset);
        }
        private void InitDropDowns()
        {
            startPageHelper.InitProcessTypeDropDown(dropDownProcesses);
            startPageHelper.InitLanguageSelectionDropDown(dropDownLanguage);
        }

        private void btnStart(object sender, RoutedEventArgs e)
        {
            startPageHelper.InitNewDataSet();
            startPageHelper.DatabaseOrTxtCheck(checkUseTxtOrDatabse.IsChecked);
            //Navigation
            if (startPageHelper.GetSelectedProcessType() == startPageHelper.GetProcessTypeList()[1]) //rep - Reparatur
            {
                _ = NavigationService.Navigate(new RepPage());
            }
            else if (dropDownProcesses.Text == startPageHelper.GetProcessTypeList()[0]) //gfs - geführte Fehlersuche
            {
                startPageHelper.LoadDropDownOptions();
                _ = NavigationService.Navigate(new GfsPage());
            }
            else
            {
                if(consol.showErrors) Console.WriteLine("Fehler in der gfs/rep-Wahl---                    BtnNext() aus StartPage");
            }
        }
        private void BtnLoadFile(object sender, RoutedEventArgs e)
        {
            
            startPageHelper.LoadDataFromFile();
            startPageHelper.ChangeProcessActiveElement(dropDownProcesses, dropDownProcesses.Text);
            startPageHelper.SetFilePathText(textBlockLoadFile);
            startPageHelper.SetDisplayStepsText(labelStepCount);
        }
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            startPageHelper.Reset(textBlockLoadFile);
        }
        private void dropDownLanguage_OnClosed(object sender, EventArgs e)
        {
            startPageHelper.SetLangauge(dropDownLanguage.Text);
            InitLabels();
            InitButtons();
        }
        private void dropDownProcesses_OnClosed(object sender, EventArgs e)
        {
            startPageHelper.ChangeProcessActiveElement(dropDownProcesses, dropDownProcesses.Text);
        }
    }
}
