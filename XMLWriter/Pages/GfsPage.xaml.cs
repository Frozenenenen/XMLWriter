﻿using System;
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
    /// Interaktionslogik für GfsPage.xaml
    /// </summary>
    public partial class GfsPage : Page
    {
        public GfsPage()
        {
            InitializeComponent();
            InitTextItems();
            InitValueItems();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            GUIMovement GUI = new GUIMovement();
            //data.SaveGfsSet(inputText.Text, inputAnim.Text, inputInstruction.Text);
            GUI.IncrementSteps();

            _ = NavigationService.Navigate(new GfsPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            GUIMovement GUI = new GUIMovement();

            if (data.GetStepCount() == 0)
            {
                MainWindow mW = new MainWindow();
                mW.Show();
            }
            else
            {
                GUI.DecrementSteps();
                _ = NavigationService.Navigate(new GfsPage());
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            GUIMovement GUI = new GUIMovement();
            //data.SaveGfsSet(, inputText.Text, inputAnim.Text, inputInstruction.Text);
            GUI.IncrementSteps();
            GUI.DecrementSteps(); //Entweder ich mach ne extra Funktion für die letzte Dateneingabe oder ich in- und decrementiere direkt nacheinander. i++ i--. Anonsten hab ich beim zurückgehen Probleme^^

            _ = NavigationService.Navigate(new SavePage());
        }

        private void InitTextItems()
        {
            DataSet data = new DataSet();
            Language language = new Language();

            //Inhalte linke Spalte
            textStep.Content = language.GetStringStep() + " " + (data.GetStepCount() + 1);
            textContentTitel.Content = language.GetStringContent();
            textAnimTitel.Content = language.GetStringAnim();
            textInstructionTitel.Content = language.GetStringSpecialStep();
            textTitel.Content = language.GetStringPleaseFill();
            if (data.GetStepCount() != 0)
            {
                btnBack.Content = language.GetStringBack();
            }
            else
            {
                btnBack.Content = language.GetStringReset();
            }
            btnNext.Content = language.GetStringNext();
            btnSave.Content = language.GetStringSave();
            inputAnim.Text = data.GetStepAnimsPos(data.GetStepCount());

            //Inhalte rechte Spalte
            inputPositiveID.Text = language.GetStringPosID();
            inputNegativeID.Text = language.GetStringNegID();
            inputPositiveResult.Text = language.GetStringPosResult();
            inputLastStep.Content = language.GetStringLastStep();
            inputRepXML.Text = language.GetStringRepXML();
            inputActuatorTest.Text = language.GetStringActuatorTest();
            inputReadData.Text = language.GetStringReadData();
            inputNextStep.Content = language.GetStringNextStep();


        }

        private void InitValueItems()
        {
            DataSet data = new DataSet();
            inputAnim.Text = data.GetStepAnimsPos(data.GetStepCount());
            inputText.Text = data.GetStepSpecialTextPos(data.GetStepCount());
            inputInstruction.Text = data.GetStepTextPos(data.GetStepCount());
        }
    
    }
}
