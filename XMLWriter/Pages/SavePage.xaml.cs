using System;
using XMLWriter.Classes;
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
        DataSet data = new DataSet();
        GUIMovement gui = new GUIMovement();
        LoadDataSet loadData = new LoadDataSet();
        Language language = new Language();     

        public SavePage()
        {
            InitializeComponent();
            InitTextItems();
            InitValueItems();
            InitButtons();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
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
            _ = NavigationService.Navigate(new StartPage());
            //App.Current.Shutdown(0);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            gui.DecrementStepsForGoingBackFromSaving();
            if (data.GetDataType() == "rep")
            {
                _ = NavigationService.Navigate(new RepPage());
            }else if(data.GetDataType() == "gfs")
            {
                _ = NavigationService.Navigate(new GfsPage());
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
            textStepCount.Content = (data.GetStepCountMax()) + " " + language.GetStringSteps();
            btnBack.Content = language.GetStringBack();
            btnSave.Content = language.GetStringSave();
            inputVehicleID.Text = "eGolf";
            inputLanguage.Text = "de";

        }
        private void InitButtons()
        {
            btnLoadFile.Content = language.GetStringFilePathDialog();
            btnSave.Background = Brushes.Gray;
            btnSave.IsEnabled = false;
        }
        private void InitValueItems()
        {
            DataSet data = new DataSet();
            inputFileName.Text = data.GetFileName();
        }

        private void BtnLoadFile(object sender, RoutedEventArgs e)
        {
            btnLoadFile.Content = language.GetStringFilePathDialog();
            loadData.OpenFileDialog();
            inputFileName.Text = loadData.GetFileNameAndPath();
            textFileName.Text = loadData.GetFileNameAndPath();
            if (textFileName.Text=="")
            {
                btnSave.Background = Brushes.Gray;
                btnSave.IsEnabled = false;  
            }
            else
            {
                btnSave.Background = Brushes.White;
                btnSave.IsEnabled = true;
            }
        }
    }
}
