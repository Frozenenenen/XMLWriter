﻿using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Linq;
using XMLWriter.Classes;

namespace XMLWriter.Pages
{
    /// <summary>
    /// Interaktionslogik für RepPage.xaml
    /// </summary>
    public partial class RepPage : Page
    {
        DataSetService dataSetService = new DataSetService();
        GUIMovementHelper gui = new GUIMovementHelper();
        DataSet dataSet;
        Language language = new Language();
        ConsoleControl consol = new ConsoleControl();

        public RepPage()
        {
            dataSet = dataSetService.GetDataSets().ElementAt(gui.GetIndex());
            InitializeComponent();
            InitTextItems();
            InitValueItems();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            dataSetService.InitNewDataSet();

            WriteInputToDataSet();
            dataSetService.SetDataSet(dataSet);
            gui.IncrementSteps();
            
            _ = NavigationService.Navigate(new RepPage());

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            WriteInputToDataSet();
            dataSetService.SetDataSet(dataSet);
            if (gui.IsFirstPage())
            {
                _ = NavigationService.Navigate(new StartPage());
            }
            else
            {
                gui.DecrementSteps();
                _ = NavigationService.Navigate(new RepPage());
            }
        }
        private void BtnBackDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gui.IsFirstPage())
            {

                _ = NavigationService.Navigate(new StartPage());
                dataSetService.ResetDataSet();
            }
            else
            {
                gui.DecrementStepsMax();
                _ = NavigationService.Navigate(new RepPage());
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            WriteInputToDataSet();
            dataSetService.SetDataSet(dataSet);
            gui.IncrementSteps();
            _ = NavigationService.Navigate(new SavePage());
        }

        private void InitTextItems()
        {
            //Schritzüge
//Wichtig, wieder einbauen!            //textStep.Content = language.GetStringStep() + " " + (data.GetStepCount() + 1);
            textContentTitel.Content = language.GetStringContent();
            textAnimTitel.Content = language.GetStringAnim();
            textSpecialContentTitel.Content = language.GetStringSpecialStep();
            textTitel.Content = language.GetStringPleaseFill();

            //Buttons
            btnBack.Content = language.GetStringBack();
            btnBackDelete.Content = language.GetStringReset();
            btnNext.Content = language.GetStringNext();
            btnSave.Content = language.GetStringSave();
        }
        private void InitValueItems()
        {
            inputStepName.Text = dataSet.stepName == ""
                ? "Schritt " + (gui.GetStepCount())
                : dataSet.stepName;
            inputText.Text = dataSet.text;
            inputAnim.Text = dataSet.anim == ""
                ? "default"
                : dataSet.anim;
            inputSpecialText.Text = dataSet.specialText;

            if(consol.showMiscRep) Console.WriteLine("Ausgabe: Schritt: " + (gui.GetStepCount()) + " Anim: " + dataSet.anim + " Text: " + dataSet.text + " SpText: " + dataSet.specialText);
        }
        private void WriteInputToDataSet()
        {
            dataSet.stepName = inputStepName.Text;
            dataSet.text = inputText.Text;
            dataSet.anim = inputAnim.Text;
            dataSet.specialText = inputSpecialText.Text;
        }
    }
}
