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
        StartPageData startPageData = new StartPageData();
        Language language = new Language();
        LoadDataSet loadData = new LoadDataSet();
        DropDownOptionLists loadInput = new DropDownOptionLists();
        ConsoleControl consol = new ConsoleControl();   
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        public StartPage()
        {
            InitializeComponent();
            startPageData.InitLanguages();
            startPageData.InitTextStrings();
            InitLabels();
            InitButtons();
            InitDropDowns();
            
        }
        private void InitLabels()
        {
            List<Label> labelList = new List<Label>{ labelTitel, labelLoadFile, labelStepCount };
            List<string> textList = new List<string> { startPageData.GetTextTitel(), startPageData.GetTextFileNameTitel(), startPageData.GetTextDisplayStep() + data.GetStepCountMax() };
            xamlHelper.InitTextItems(labelList, textList);
        }
        private void InitButtons()
        {
            xamlHelper.SetButtonFor(btnWeiter,"--->");
            xamlHelper.SetButtonFor(btnLoadFile, startPageData.GetTextLoadFile());
            xamlHelper.SetButtonFor(btnReset, startPageData.GetTextDeleteSet());
        }
        private void InitDropDowns()
        {
            //ProcessType init (gfs/rep)
            xamlHelper.SetDropdownListFor(dropDownProcesses, startPageData.GetProcessTypeList());
            xamlHelper.SetDropDownActiveELementFor(dropDownProcesses, data.GetDataType());
            //language init
            xamlHelper.SetDropdownListFor(dropDownLanguage, startPageData.GetLanguageList());
            xamlHelper.SetDropDownActiveELementFor(dropDownLanguage, startPageData.GetSelectedLanguage());
        }
        private void btnStart(object sender, RoutedEventArgs e)
        {
            startPageData.InitNewDataSet();
            startPageData.LoadDropDownOptions();
            data.SetStepCount(0);   // This should rather be in Load
            startPageData.DatabaseOrTxtCheck(checkUseTxtOrDatabse.IsChecked);
            if (dropDownProcesses.Text == "rep")
            {
                _ = NavigationService.Navigate(new RepPage());
            }
            else if (dropDownProcesses.Text == "gfs")
            {
                _ = NavigationService.Navigate(new GfsPage());
            }
            else
            {
                if(consol.showErrors) Console.WriteLine("Fehler in der gfs/rep-Wahl---                    BtnNext() aus StartPage");
            }
        }
        private void BtnSelectLanguage_Click(object sender, RoutedEventArgs e)
        {
            startPageData.SetLangauge(dropDownLanguage.Text);
            xamlHelper.SetTextFor(labelTitel, startPageData.GetTextTitel());
        }
 
        private void BtnLoadFile(object sender, RoutedEventArgs e)
        {
            loadData.LoadDataFromFile();
            startPageData.SetProcessActiveElement(dropDownProcesses);
            //xamlHelper.SetDropDownActiveELementFor(dropDownProcesses, data.GetDataType());
            xamlHelper.SetTextFor(textBlockLoadFile, loadData.GetFileNameAndPath());
            xamlHelper.SetTextFor(labelStepCount, startPageData.GetTextDisplayStep() + data.GetStepCountMax());
        }

        private void BtnReset(object sender, RoutedEventArgs e)
        {
            startPageData.Reset(textBlockLoadFile);
        }
    }
}
