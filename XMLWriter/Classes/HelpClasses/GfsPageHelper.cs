using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Linq;
using XMLWriter.Classes;

namespace XMLWriter.Classes.HelpClasses {
    internal class GfsPageHelper {
        DataSetService dataSetService = new DataSetService();
        UtilityFunctions utility = new UtilityFunctions();
        Language language = new Language();
        GUIMovementHelper gui = new GUIMovementHelper();
        DropDownOptionLists dropDownList = new DropDownOptionLists();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        ConsoleControl consol = new ConsoleControl();

        /// --- Navigation --- ///
        public void PrepareNextPage() {
            dataSetService.InitNewDataSetWhereRequired();
            gui.IncrementSteps();
        }
        public void PreparePreviousPage() {
            gui.DecrementSteps();
        }
        public void DeleteCurrentSet() {
            gui.DecrementStepsMax();        //Unfertig
        }

        /// ---- WriteToDataSet --- ///
        public string handleToolChoiceAndResultingPositiveResult(ComboBox toolChoice, ComboBox actuatorTest, ComboBox RDID, ComboBox smartTool, TextBox posRes_RDID, TextBox posRes_SM, string positiveResult) {
            if (toolChoice.Text == dropDownList.GetToolChoice()[1]) //AT
            {
                smartTool.Text = "false";
                RDID.Text = "false";
                positiveResult = "";
            }
            else if (toolChoice.Text == dropDownList.GetToolChoice()[2]) //SmT
            {
                actuatorTest.Text = "false";
                RDID.Text = "false";
                posRes_RDID.Text = "false";
                positiveResult = posRes_SM.Text;
            }
            else if (toolChoice.Text == dropDownList.GetToolChoice()[3]) //RDID
            {
                smartTool.Text = "false";
                actuatorTest.Text = "false";
                posRes_SM.Text = "false";
                positiveResult = posRes_RDID.Text;
            }
            return positiveResult;
        }
        public void SaveCurrentInput(TextBox stepName, TextBox text, TextBox anim, TextBox instruction, ComboBox positiveID, ComboBox negativeID, string positiveResult, TextBox repXML, ComboBox actuatorTest, ComboBox RDID, ComboBox smartTool, CheckBox nextStep, CheckBox lastStep, ComboBox toolChoice) {
            
            utility.WriteStepNameToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), stepName.Text);
            utility.WriteTextToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), text.Text);
            utility.WriteAnimToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), anim.Text);
            utility.WriteInstructionToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), instruction.Text);
            utility.WritePositiveIDToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), positiveID.Text);
            utility.WriteNegativeIDToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), negativeID.Text);
            utility.WritePositiveResultToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), positiveResult);
            utility.WriteRepXMLToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), repXML.Text);
            utility.WriteActuatorTestToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), actuatorTest.Text);
            utility.WriteRDIDToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), RDID.Text);
            utility.WriteSmartToolToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), smartTool.Text);
            utility.WriteToolChoiceToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), toolChoice.Text);
            if (nextStep.IsChecked == true) {
                utility.WriteNextStepToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), true);
            }
            else {
                utility.WriteNextStepToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), false);
            }
            if (lastStep.IsChecked == true) {
                utility.WriteLastStepToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), true);
            }
            else {
                utility.WriteLastStepToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), false);
            }
            utility.WriteSpecialTextToCurrentDataSet(dataSetService.GetDataSets(), gui.GetIndex(), "");
            consol.ConsoleShowDataSetOfIndex(dataSetService.GetDataSets().ElementAt(gui.GetIndex()), gui.GetIndex(), "Speichern");
        }

    }
}
