using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XMLWriter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Language language = new Language();
        DataSet data = new DataSet();
        public MainWindow()
        {
            InitializeComponent();
            
            data.InitDataSet();
            language.InitLanguage("Deutsch");

            inputType.ItemsSource = data.GetDataTypeChoice();
            inputType.Text = "rep";     //Aus irgendeinem Grund wird es nicht angezeigt, wenn gfs statt rep genutzt wird... Ich sag mal es reicht so
            //btnWeiter.Content = language.GetStringNext();
            btnWeiter.Content = "--->";


            inputLanguage.ItemsSource = language.GetLanguageChoises();
            inputLanguage.Text = language.GetStringLanguage();


        }

        private void Button_Next(object sender, RoutedEventArgs e)
        {
            //Ausgewähltes nächstes Fenster öffnen und dieses schließen
            Rep rep = new Rep();
            Gfs gfs = new Gfs();
            data.SetDataType(inputType.Text);


            if (inputType.Text == "rep")
            {
                rep.Show();
            }
            else if (inputType.Text == "gfs")
            {
                gfs.Show();
            }



            Close();

        }


        private void btnSelectLanguage_Click(object sender, RoutedEventArgs e)
        {
            language.InitLanguage(inputLanguage.Text); //Befüllt die Variablen mit den Wörtern  der jeweiligen Sprache
            textTitel.Content = language.GetStringCreateDataSet(); //Ausgabe der Überschrift "Create Data Set"
        }
    }
}
