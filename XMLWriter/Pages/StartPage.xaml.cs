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
            InitLabels();
            InitButtons();
            InitDropDowns();
        }
        private void InitLabels()
        {
            startPageData.SetLabelContent(labelTitel, startPageData.GetTextFileNameTitel());
            startPageData.SetLabelContent(labelLoadFile, startPageData.GetTextLoadFile());
            startPageData.SetLabelContent(labelStepCount, startPageData.GetTextDisplayStep() + data.GetStepCount());
        }
        private void InitButtons()
        {

            startPageData.SetButtonContent(btnWeiter,"--->");
            startPageData.SetButtonContent(btnLoadFile, startPageData.GetTextLoadFile());
            startPageData.SetButtonContent(btnReset, startPageData.GetTextDeleteSet());
        }
        private void InitDropDowns()
        {
            startPageData.SetDropDownContent(dropDownProcesses, startPageData.GetProcessTypeList(), data.GetDataType());
            startPageData.SetDropDownContent(dropDownLanguage, startPageData.GetLanguageList(), startPageData.GetSelectedLanguage());
        }
        private void btnStart(object sender, RoutedEventArgs e)
        {
            startPageData.InitNewDataSet();
            startPageData.LoadDropDownOptions();
            data.SetStepCount(0);   // This should rather be in Load
            startPageData.DatabaseOrTxtCheck(checkUseTxtOrDatabse.IsChecked);

            if (startPageData.GetSelectedProcessType() == "rep")
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
            startPageData.SetLabelContent(labelTitel, startPageData.GetTextTitel());
        }
 
        private void BtnLoadFile(object sender, RoutedEventArgs e)
        {
            loadData.LoadDataFromFile();
            startPageData.SetProcessActiveElement(dropDownProcesses);
            //xamlHelper.SetDropDownActiveELementFor(dropDownProcesses, data.GetDataType());
            startPageData.SetTextBlockContent(textBlockLoadFile, loadData.GetFileNameAndPath());
            startPageData.SetLabelContent(labelStepCount, startPageData.GetTextDisplayStep() + data.GetStepCountMax());
        }

        private void BtnReset(object sender, RoutedEventArgs e)
        {
            startPageData.Reset(textBlockLoadFile);
        }

        private void dropDownProcesses_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            startPageData.ChangeDropDownContentActiveElement(dropDownProcesses, dropDownProcesses.Text);
        }
    }
}
