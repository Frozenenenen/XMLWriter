﻿using Microsoft.Win32;
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
        public StartPage()
        {
            InitializeComponent();
            Language lingo = new Language();
            DataSet data = new DataSet();
            data.InitNewDataSet();
            lingo.InitLanguage("Deutsch");
            InitTextItems();
            InitValueItems();
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
        private void BtnClose(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown(0);
        }
        private void BtnLoadFile(object sender, RoutedEventArgs e)
        {
            LoadDataSet loadData = new LoadDataSet();
            loadData.LoadDataFromFile();
            inputLoadFile.Text = loadData.GetFileNameAndPath();

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
            textLoadFile.Content = language.GetStringFileNameTitel();
            btnLoadFile.Content = language.GetStringLoadFile();
        }
        private void InitValueItems()
        {
            //Ähm... ja
        }

        private void BtnReset(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            data.ResetDataSet();
            inputLoadFile.Text = "";
        }
    }
}
