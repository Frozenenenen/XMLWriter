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
            Language lingo = new Language();
            DataSet data = new DataSet();
            data.InitDataSet();
            lingo.InitLanguage("Deutsch");

            inputType.ItemsSource = data.GetDataTypeChoice();
            inputType.Text = "rep";     //Aus irgendeinem Grund wird es nicht angezeigt, wenn gfs statt rep genutzt wird... Ich sag mal es reicht so
            //btnWeiter.Content = lingo.GetStringNext();
            btnWeiter.Content = "--->";


            inputLanguage.ItemsSource = lingo.GetLanguageChoises();
            inputLanguage.Text = lingo.GetStringLanguage();


        }

        private void Button_Next(object sender, RoutedEventArgs e)
        {
            //Ausgewähltes nächstes Fenster öffnen und dieses schließen
            MainWindow main = new MainWindow();
            RepPage rep = new RepPage();
            GfsPage gfs1 = new GfsPage();
            SavePage sW = new SavePage();
            DataSet data = new DataSet();
            data.SetDataType(inputType.Text);
            Console.WriteLine("bla");
            main.Main.Content = new RepPage();
            if (inputType.Text == "rep")
            {
                main.Main.Content = new RepPage();
            }
            else if (inputType.Text == "gfs")
            {
                main.Main.Content = new GfsPage();
            }
            else
            {
                Console.WriteLine("Fehler in der gfs/rep-Wahl");
            }


            //Close();

        }


        private void btnSelectLanguage_Click(object sender, RoutedEventArgs e)
        {
            Language language = new Language();
            language.InitLanguage(inputLanguage.Text); //Befüllt die Variablen mit den Wörtern  der jeweiligen Sprache
            textTitel.Content = language.GetStringCreateDataSet(); //Ausgabe der Überschrift "Create Data Set"
        }
    }
}
