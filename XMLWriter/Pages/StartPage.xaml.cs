using System;
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
            xamlHelper.SetLabelTextFor(labelTitel, startPageData.GetTextTitel());
            xamlHelper.SetLabelTextFor(labelInstructions, startPageData.GetTextGeneralInstructionShort());
            xamlHelper.SetLabelTextFor(labelInstructions, startPageData.GetTextGeneralInstructionLong());
            xamlHelper.SetLabelTextFor(labelLoadFile, startPageData.GetTextFileNameTitel());
            xamlHelper.SetLabelTextFor(labelStepCount, startPageData.GetTextDisplayStep() + data.GetStepCountMax());
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

            if (checkUseTxtOrDatabse.IsChecked == true)
            {
                loadInput.UseDataBase();
            }
            else
            {
                loadInput.DontUseDataBase();
            }
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
            //language.InitLanguage(startPageData.GetSelectedLanguage()); //Befüllt die Variablen mit den Wörtern  der jeweiligen Sprache

            //labelTitel.Content = startPageData.GetStartPageTitel();
            xamlHelper.SetLabelTextFor(labelTitel, startPageData.GetTextTitel());
        }
        private void BtnClose(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown(0);
        }
        private void BtnLoadFile(object sender, RoutedEventArgs e)
        {

            loadData.LoadDataFromFile();
            dropDownProcesses.Text = data.GetDataType();
            textBoxLoadFile.Text = loadData.GetFileNameAndPath();
            labelStepCount.Content = startPageData.GetTextDisplayStep() + " " + data.GetStepCountMax();
            xamlHelper.SetLabelTextFor(labelStepCount, startPageData.GetTextDisplayStep() + data.GetStepCountMax());
        }

        private void BtnReset(object sender, RoutedEventArgs e)
        {
            data.ResetDataSet();
            textBoxLoadFile.Text = "";
        }
    }
}
