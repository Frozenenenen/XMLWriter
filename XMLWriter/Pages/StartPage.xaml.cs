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
            InitTextItems();
            InitValueItems();
            
        }



        private void InitTextItems()
        {
            btnWeiter.Content = "--->";
            textInstructions.Content = language.GetStringGeneralInstruction();
            textInstructions.ToolTip = language.GetStringGeneralInstructionText();
            textLoadFile.Content = language.GetStringFileNameTitel();
            btnLoadFile.Content = language.GetStringLoadFile();
            btnReset.Content = language.GetStringDeleteSet();   
            textStepCount.Content = language.GetStringSteps() + " " + data.GetStepCountMax();
        }


        private void InitValueItems()
        {
            inputType.ItemsSource = startPageData.GetProcessTypeList();
            xamlHelper.SetActiveELementFor(inputType, data.GetDataType());
            if (consol.showMiscStarPage) System.Diagnostics.Debug.WriteLine("Type (rep/gfs): " + inputType.Text + "                                     ---StartPage.InitTextItems()");
            //language init
            inputLanguage.ItemsSource = startPageData.GetLanguageList();
            xamlHelper.SetActiveELementFor(inputLanguage, startPageData.GetSelectedLanguage());
        }
        private void BtnNext(object sender, RoutedEventArgs e)
        {
            data.SetDataType(inputType.Text);
            data.SetStepCount(0);   //has to be reset to 0 coz of loading

            if (inputType.Text == "rep")
            {

                _ = NavigationService.Navigate(new RepPage());
            }
            else if (inputType.Text == "gfs")
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
            startPageData.SetLangauge(inputLanguage.Text);
            //language.InitLanguage(startPageData.GetSelectedLanguage()); //Befüllt die Variablen mit den Wörtern  der jeweiligen Sprache

            textTitel.Content = startPageData.GetStartPageTitel();
        }
        private void BtnClose(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown(0);
        }
        private void BtnLoadFile(object sender, RoutedEventArgs e)
        {

            loadData.LoadDataFromFile();
            inputType.Text = data.GetDataType();
            inputLoadFile.Text = loadData.GetFileNameAndPath();
            textStepCount.Content = language.GetStringSteps() + " " + data.GetStepCountMax();
        }

        private void BtnReset(object sender, RoutedEventArgs e)
        {
            data.ResetDataSet();
            inputLoadFile.Text = "";
        }
    }
}
