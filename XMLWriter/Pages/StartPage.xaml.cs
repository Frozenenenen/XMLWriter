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
        StartPageHelper startPageData = new StartPageHelper();
        Language language = new Language();
        LoadDataSet loadData = new LoadDataSet();
        DropDownOptionLists loadInput = new DropDownOptionLists();
        ConsoleControl consol = new ConsoleControl();   
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        public StartPage()
        {
            InitializeComponent();
            startPageData.InitLanguages();
            InitLabels();
            InitButtons();
            InitDropDowns();
        }
        private void InitLabels()
        {
            startPageData.SetTitelText(labelTitel);
            startPageData.SetLoadFileText(labelLoadFile);
            startPageData.SetDisplayStepText(labelStepCount);
            startPageData.SetTxtOrDataBaseCheckBoxText(textUseDatabaseChecked, textUseDatabaseUnchecked);
        }
        private void InitButtons()
        {
            startPageData.SetStartButtonText(btnWeiter);
            startPageData.SetLoadButtonText(btnLoadFile);
            startPageData.SetResetButtonText(btnReset);
        }
        private void InitDropDowns()
        {
            startPageData.InitProcessTypeDropDown(dropDownProcesses);
            startPageData.InitLanguageSelectionDropDown(dropDownLanguage);
        }

        private void btnStart(object sender, RoutedEventArgs e)
        {
            startPageData.InitNewDataSet();
            data.SetStepCount(0);   // This should rather be in Load
            startPageData.DatabaseOrTxtCheck(checkUseTxtOrDatabse.IsChecked);

            if (startPageData.GetSelectedProcessType() == startPageData.GetProcessTypeList()[1]) //rep - Reparatur
            {
                _ = NavigationService.Navigate(new RepPage());
            }
            else if (dropDownProcesses.Text == startPageData.GetProcessTypeList()[0]) //gfs - geführte Fehlersuche
            {
                startPageData.LoadDropDownOptions();
                _ = NavigationService.Navigate(new GfsPage());
            }
            else
            {
                if(consol.showErrors) Console.WriteLine("Fehler in der gfs/rep-Wahl---                    BtnNext() aus StartPage");
            }
        }
        private void BtnLoadFile(object sender, RoutedEventArgs e)
        {
            loadData.LoadDataFromFile();
            startPageData.ChangeDropDownContentActiveElement(dropDownProcesses, dropDownProcesses.Text);
            startPageData.SetTextBlockContent(textBlockLoadFile, loadData.GetFileNameAndPath());
            startPageData.SetLabelContent(labelStepCount, startPageData.GetTextDisplayStep() + data.GetStepCountMax());
        }
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            startPageData.Reset(textBlockLoadFile);
        }
        private void dropDownLanguage_OnClosing(object sender, EventArgs e)
        {
            startPageData.SetLangauge(dropDownLanguage.Text);
            InitLabels();
            InitButtons();
        }
        private void dropDownProcesses_DropDownClosed(object sender, EventArgs e)
        {
            startPageData.ChangeDropDownContentActiveElement(dropDownProcesses, dropDownProcesses.Text);

        }
    }
}
