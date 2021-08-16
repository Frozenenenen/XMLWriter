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
    /// Interaktionslogik für SavePage.xaml
    /// </summary>
    public partial class SavePage : Page
    {
        public SavePage()
        {
            InitializeComponent();
            InitTextItems();
            InitValueItems();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            
            data.SetFileName(inputFileName.Text);
            if (data.GetDataType() == "rep")
            {
                System.Diagnostics.Debug.WriteLine("Rep Speichern");
                data.OutputToXML();
            }else if(data.GetDataType() == "gfs")
            {
                data.OutputToXML();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error with DataType - Savepage Savebutton");
            }
            
            App.Current.Shutdown(0);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            if (data.GetDataType() == "rep")
            {
                _ = NavigationService.Navigate(new RepPage());
            }else if(data.GetDataType() == "gfs")
            {
                _ = NavigationService.Navigate(new RepPage());
            }
            else
            {
                Console.WriteLine("Fehler beim btnBack mit Datatype");
            }
            
            
        }

        private void InitTextItems()
        {
            Language language = new Language();
            DataSet data = new DataSet();
            WriteToXML writer = new WriteToXML();
            inputVehicleID.ItemsSource = writer.GetPathVehicleIDChoises();
            inputLanguage.ItemsSource = writer.GetPathLanguageChoises();
            textTitel.Content = language.GetStringSummary();
            textFileNameTitel.Content = language.GetStringFileNameTitel();
            textStepCount.Content = (data.GetStepCountMax() + 1) + " " + language.GetStringSteps();
            btnBack.Content = language.GetStringBack();
            btnSave.Content = language.GetStringSave();
            inputVehicleID.Text = "eGolf";
            inputLanguage.Text = "de";

        }
        private void InitValueItems()
        {
            DataSet data = new DataSet();
            inputFileName.Text = data.GetFileName();
        }

    
    }
}
