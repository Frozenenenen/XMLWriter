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
            InitTextItems();
        }

        private void BtnNext(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            data.SetDataType(inputType.Text);

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
                Console.WriteLine("Fehler in der gfs/rep-Wahl");
            }
        }


        private void BtnSelectLanguage_Click(object sender, RoutedEventArgs e)
        {
            Language language = new Language();
            language.InitLanguage(inputLanguage.Text); //Befüllt die Variablen mit den Wörtern  der jeweiligen Sprache
            textTitel.Content = language.GetStringCreateDataSet(); //Ausgabe der Überschrift "Create Data Set"
        }
        private void InitTextItems()
        {
            Language language = new Language();
            DataSet data = new DataSet();

            btnWeiter.Content = "--->";
            inputLanguage.ItemsSource = language.GetLanguageChoises();
            inputLanguage.Text = language.GetStringLanguage();
            inputType.ItemsSource = data.GetDataTypeChoice();
            textInstructions.Content = language.GetStringGeneralInstruction();
            textInstructions.ToolTip = language.GetStringGeneralInstructionText();
            inputType.Text = "rep";     //Aus irgendeinem Grund wird es nicht angezeigt, wenn gfs statt rep genutzt wird... Ich sag mal es reicht so
        }

        private void BtnClose(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown(0);
        }
    }
}
