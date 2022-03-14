using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using XMLWriter.Classes.HelpClasses;

namespace XMLWriter.Classes.StartPage
{
    internal class SavePageHelper
    {
        DataSetService dataSetService = new DataSetService();
        Language language = new Language();
        LoadHelper loadHelper = new LoadHelper();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        StartPageHelper startPageHelper = new StartPageHelper();

        //Init & Set
        //Labels
        public void InitLabelTitel(Label titel) {
            xamlHelper.SetTextFor(titel, language.GetStringSummary());
        }
        public void InitLabelFileName(Label fielName) {
            xamlHelper.SetTextFor(fielName, language.GetStringFilePath());
        }
        public void InitLabelStepCount(Label stepCount) {
            xamlHelper.SetTextFor(stepCount, dataSetService.GetDataSets().Count + " " + language.GetStringSteps());
        }
        //Buttons
        public void InitButtonBack(Button back) {
            xamlHelper.SetTextFor(back, language.GetStringBack());
        }
        public void InitButtonSave(Button btnSave) {
            xamlHelper.SetTextFor(btnSave, language.GetStringSave());
            if (PathIsSet()) {
                SetSaveButtonActive(btnSave);
            }
            else {
                SetSaveButtonInactive(btnSave);
            }
            
        }
        public void InitButtonOpenFile(Button open) {
            xamlHelper.SetTextFor(open, language.GetStringFilePathDialog());
        }
        //TextBlock
        public void InitTextFilePath(TextBlock textBlock) {
            xamlHelper.SetTextFor(textBlock, loadHelper.GetFileNameAndPath());
        }
        
        //Save Button Change
        public void SetSaveButtonInactive(Button btnSave) {
            btnSave.Background = Brushes.Gray;
            btnSave.IsEnabled = false;
        }
        public void SetSaveButtonActive(Button btnSave) {
            btnSave.Background = Brushes.Green;
            btnSave.IsEnabled = true;
        }

        public void StartXMLWriting() {
            dataSetService.OutputToXML(startPageHelper.GetSelectedProcessType());
        }
        //Checker
        public bool IsGfs() {
            if (startPageHelper.GetSelectedProcessType() == "gfs") {
                return true;
            }
            return false;
        }
        public bool IsRep() {
            if (startPageHelper.GetSelectedProcessType() == "rep") {
                return true;
            }
            return false;
        }
        public bool PathIsSet() {
            if (loadHelper.GetFileNameAndPath() != "") {
                return true;    
            }
            return false;
        }
    }
}
