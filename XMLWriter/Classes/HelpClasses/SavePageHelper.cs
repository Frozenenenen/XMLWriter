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
        DataSetService data = new DataSetService();
        GUIMovementHelper gui = new GUIMovementHelper();
        Language language = new Language();
        LoadHelper loadHelper = new LoadHelper();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();

        //Init & Set
        //Labels
        public void InitLabelTitel(Label titel) {
            xamlHelper.SetTextFor(titel, language.GetStringSummary());
        }
        public void InitLabelFileName(Label fielName) {
            xamlHelper.SetTextFor(fielName, language.GetStringFilePath());
        }
        public void InitLabelStepCount(Label stepCount) {
            xamlHelper.SetTextFor(stepCount, gui.GetStepCountMax() + " " + language.GetStringSteps());
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
        public void InitTextFilePath(TextBlock text) {
            xamlHelper.SetTextFor(text, loadHelper.GetFileNameAndPath());
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
            if (IsRep()) {
                System.Diagnostics.Debug.WriteLine("Rep Speichern");
                data.OutputToXML();
            }
            else if (IsGfs()) {
                data.OutputToXML();
            }
            else {
                System.Diagnostics.Debug.WriteLine("Error with DataType - Savepage Savebutton");
            }
        }
        //Checker
        public bool IsGfs() {
            if (data.GetDataType() == "gfs") {
                return true;
            }
            return false;
        }
        public bool IsRep() {
            if (data.GetDataType() == "rep") {
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
