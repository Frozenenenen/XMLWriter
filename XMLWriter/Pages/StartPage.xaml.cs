using System;
using System.Collections.Generic;
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

namespace XMLWriter.Pages
{
    /// <summary>
    /// Interaktionslogik für StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
            InitializeComponent();
            Language language = new Language();
            DataSet data = new DataSet();
            data.InitDataSet();
            language.InitLingo("Deutsch");

            inputType.ItemsSource = data.GetDataTypeChoice();
            inputType.Text = "rep";     //Aus irgendeinem Grund wird es nicht angezeigt, wenn gfs statt rep genutzt wird... Ich sag mal es reicht so
            //btnWeiter.Content = lingo.GetStringNext();
            btnWeiter.Content = "--->";


            inputLingo.ItemsSource = language.GetLingoChoises();
            inputLingo.Text = language.GetStringLingo();


        }

        private void Button_Next(object sender, RoutedEventArgs e)
        {
            //Ausgewähltes nächstes Fenster öffnen und dieses schließen
            RepPage rep = new RepPage();
            GfsPage gfs = new GfsPage();
            SavePage sP = new SavePage();
            DataSet data = new DataSet();
            MainWindow mW = new MainWindow();
            data.SetDataType(inputType.Text);


            if (inputType.Text == "rep")
            {
                NavigationService.Navigate(new RepPage());
            }
            else if (inputType.Text == "gfs")
            {
                NavigationService.Navigate(new GfsPage());
            }
            else
            {
                NavigationService.Navigate(new SavePage());
            }


        }


        private void btnSelectLingo_Click(object sender, RoutedEventArgs e)
        {
            Language language = new Language();
            language.InitLingo(inputLingo.Text); //Befüllt die Variablen mit den Wörtern  der jeweiligen Sprache
            textTitel.Content = language.GetStringCreateDataSet(); //Ausgabe der Überschrift "Create Data Set"
        }
    
    }
}
