using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XMLWriter.Classes;

namespace XMLWriter.Pages
{
    /// <summary>
    /// Interaktionslogik für StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        DataSets data = new DataSets();
        Language language = new Language();
        LoadDataSet loadData = new LoadDataSet();
        DropDownOptionLists loadInput = new DropDownOptionLists();
        ConsoleControl consol = new ConsoleControl();   
        public StartPage()
        {
            data.InitNewSet();
            loadInput.LoadAllOptions(); //Ich lad das aktuell einfach 2 Mal. 1 Mal hier und 1 Mal bei btn_next. Ich weiß nicht, warum es nicht anders geht, aber es ist so natürlich eine Fehlerquelle
            InitializeComponent();
            language.InitLanguage("Deutsch");
            InitTextItems();
            InitValueItems();
            
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
            if (checkUseTxtOrDatabse.IsChecked==true)
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
            language.InitLanguage(inputLanguage.Text); //Befüllt die Variablen mit den Wörtern  der jeweiligen Sprache
            textTitel.Content = language.GetStringCreateDataSet(); //Ausgabe der Überschrift "Create Data Set"
        }
        private void BtnClose(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown(0);
        }
        private void BtnLoadFile(object sender, RoutedEventArgs e)
        {
            
            loadData.LoadDataFromFile();
            inputType.Text = loadData.GetDataType(); 
            inputLoadFile.Text = loadData.GetFileNameAndPath();
            textStepCount.Content = language.GetStringSteps() + " " + data.GetStepCountMax();
        }

        private void BtnReset(object sender, RoutedEventArgs e)
        {
            data.ResetDataSet();
            inputLoadFile.Text = "";
        }

        private void InitTextItems()
        {
            btnWeiter.Content = "--->";
            inputLanguage.ItemsSource = language.GetLanguageChoises();
            inputLanguage.Text = language.GetStringLanguage();
            inputType.ItemsSource = data.GetDataTypeChoice();
            textInstructions.Content = language.GetStringGeneralInstruction();
            textInstructions.ToolTip = language.GetStringGeneralInstructionText();
            inputType.Text = data.GetDataType();
            if(consol.showMiscStarPage) System.Diagnostics.Debug.WriteLine("Type (rep/gfs): " + inputType.Text + "                                     ---StartPage.InitTextItems()");
            textLoadFile.Content = language.GetStringFileNameTitel();
            btnLoadFile.Content = language.GetStringLoadFile();
            btnReset.Content = language.GetStringDeleteSet();   
            textStepCount.Content = language.GetStringSteps() + " " + data.GetStepCountMax();
        }
        private void InitValueItems()
        {
            //Ähm... ja
        }
    }
}
