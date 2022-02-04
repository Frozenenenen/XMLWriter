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
            //---------------------------------------------
            System.Diagnostics.Debug.WriteLine("!!!Start!!!");
            data.InitNewSet();
            System.Diagnostics.Debug.WriteLine("!!!Nach Init!!!");
            loadInput.LoadAllOptions(); //Ich lad das aktuell einfach 2 Mal. 1 Mal hier und 1 Mal bei btn_next. Ich weiß nicht, warum es nicht anders geht, aber es ist so natürlich eine Fehlerquelle
            System.Diagnostics.Debug.WriteLine("!!!Nach Laden der .txts!!!");
            InitializeComponent();
            language.InitLanguage(startPageData.GetSelectedLanguage());
            startPageData.InitStartPageDataStrings();
            InitLabels();
            InitButtons();
            InitDropDowns();
            
        }



        private void InitLabels()
        {
            xamlHelper.SetLabelTextFor(labelTitel, startPageData.GetStringTitel());
            System.Diagnostics.Debug.WriteLine(">>>Überschrift: " + startPageData.GetStringTitel());
            xamlHelper.SetLabelTextFor(labelInstructions, startPageData.GetStringGeneralInstruction());
            xamlHelper.SetLabelTextFor(labelInstructions, startPageData.GetStringGeneralInstructionText());
            xamlHelper.SetLabelTextFor(labelLoadFile, startPageData.GetStringFileNameTitel());
            xamlHelper.SetLabelTextFor(labelStepCount, startPageData.GetStringDisplayStep() + data.GetStepCountMax());
        }
        private void InitButtons()
        {
            btnWeiter.Content = "--->";
            btnLoadFile.Content = language.GetStringLoadFile();
            btnReset.Content = language.GetStringDeleteSet();
        }


        private void InitDropDowns()
        {
            xamlHelper.SetListForDropDown(dropDownProcesses, startPageData.GetProcessTypeList());
            xamlHelper.SetActiveELementForDropDown(dropDownProcesses, data.GetDataType());
            //language init
            xamlHelper.SetListForDropDown(dropDownLanguage, startPageData.GetLanguageList());
            xamlHelper.SetActiveELementForDropDown(dropDownLanguage, startPageData.GetSelectedLanguage());
        }
        private void BtnNext(object sender, RoutedEventArgs e)
        {
            data.SetDataType(dropDownProcesses.Text);
            data.SetStepCount(0);   //has to be reset to 0 coz of loading

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
                Console.WriteLine("Fehler in der gfs/rep-Wahl---                    BtnNext() aus StartPage");
            }
            if (checkUseTxtOrDatabse.IsChecked == true)
            {
                loadInput.UseDataBase();
            }
            else
            {
                loadInput.DontUseDataBase();
            }
            loadInput.LoadAllOptions();    //Ich lad das aktuell einfach 2 Mal. 1 Mal hier und 1 Mal beim Initialisieren. Ich weiß nicht, warum es nicht anders geht, aber es ist so natürlich eine Fehlerquelle

        }
        private void BtnSelectLanguage_Click(object sender, RoutedEventArgs e)
        {
            startPageData.SetLangauge(dropDownLanguage.Text);
            //language.InitLanguage(startPageData.GetSelectedLanguage()); //Befüllt die Variablen mit den Wörtern  der jeweiligen Sprache

            //labelTitel.Content = startPageData.GetStartPageTitel();
            xamlHelper.SetLabelTextFor(labelTitel, startPageData.GetStringTitel());
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
            labelStepCount.Content = startPageData.GetStringDisplayStep() + " " + data.GetStepCountMax();
            xamlHelper.SetLabelTextFor(labelStepCount, startPageData.GetStringDisplayStep() + data.GetStepCountMax());
        }

        private void BtnReset(object sender, RoutedEventArgs e)
        {
            data.ResetDataSet();
            textBoxLoadFile.Text = "";
        }
    }
}
